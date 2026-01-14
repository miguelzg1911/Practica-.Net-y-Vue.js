using Practica.Application.DTOs.Course;
using Practica.Application.DTOs.Enrollment;
using Practica.Application.DTOs.Student;
using Practica.Application.Interfaces;
using Practica.Domain.Entities;
using Practica.Domain.Interfaces;

namespace Practica.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;

    public EnrollmentService(
        IEnrollmentRepository enrollmentRepository,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository)
    { 
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
    }

    public async Task EnrollAsync(EnrollmentInputDto dto)
    {
        var student = await _studentRepository.GetByIdAsync(dto.StudentId);
        if (student == null)
            throw new Exception("Student not found");

        var course = await _courseRepository.GetByIdAsync(dto.CourseId);
        if (course == null)
            throw new Exception("Course not found");
        
        var alredyEnrolled = await _enrollmentRepository.ExistsAsync(dto.StudentId, dto.CourseId);
        
        if (alredyEnrolled)
            throw new Exception("Student already enrolled in this course");

        var enrollment = new Enrollment
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId,
        };
        
        await _enrollmentRepository.AddAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();
    }

    public async Task UnenrollAsync(EnrollmentInputDto dto)
    {
        await _enrollmentRepository.DeleteAsync(dto.StudentId, dto.CourseId);
        await _enrollmentRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<CourseResponseDto>> GetCoursesByStudentAsync(int studentId)
    {
        var studentExists = await _studentRepository.GetByIdAsync(studentId);
        if (studentExists == null)
            throw new Exception("Student not found");

        var courses = await _enrollmentRepository
            .GetCoursesByStudentAsync(studentId);

        return courses.Select(c => new CourseResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Status = c.Status
        });
    }

    public async Task<IEnumerable<StudentResponseDto>> GetStudentsByCourseAsync(int courseId)
    {
        var courseExists = await _courseRepository.GetByIdAsync(courseId);
        if (courseExists == null)
            throw new Exception("Course not found");

        var students = await _enrollmentRepository
            .GetStudentsByCourseAsync(courseId);

        return students.Select(s => new StudentResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Document = s.Document
        });
    }
}