using Practica.Application.DTOs.Course;
using Practica.Application.DTOs.Enrollment;
using Practica.Application.DTOs.Student;

namespace Practica.Application.Interfaces;

public interface IEnrollmentService
{
    Task EnrollAsync(EnrollmentInputDto dto);
    Task UnenrollAsync(EnrollmentInputDto dto);

    Task<IEnumerable<CourseResponseDto>> GetCoursesByStudentAsync(int studentId);
    Task<IEnumerable<StudentResponseDto>> GetStudentsByCourseAsync(int courseId);
}