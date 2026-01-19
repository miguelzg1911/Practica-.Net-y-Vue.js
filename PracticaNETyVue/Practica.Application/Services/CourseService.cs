using Practica.Application.DTOs.Course;
using Practica.Application.Interfaces;
using Practica.Domain.Entities;
using Practica.Domain.Interfaces;

namespace Practica.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ITeacherRepository _teacherRepository;

    public CourseService(ICourseRepository courseRepository, ITeacherRepository teacherRepository)
    {
        _courseRepository = courseRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<IEnumerable<CourseResponseDto>> GetAllAsync()
    {
        var courses = await _courseRepository.GetAllAsync();

        return courses.Select(c => new CourseResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Status = c.Status,
            TeacherId = c.TeacherId,
            TeacherName = c.Teacher?.Name ?? string.Empty,
            ImageUrl = c.ImageUrl // <-- Mapeo de imagen
        });
    }

    public async Task<CourseResponseDto?> GetByIdAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null) return null;

        return new CourseResponseDto
        {
            Id = course.Id,
            Name = course.Name,
            Status = course.Status,
            TeacherId = course.TeacherId,
            TeacherName = course.Teacher?.Name ?? string.Empty,
            ImageUrl = course.ImageUrl // <-- Mapeo de imagen
        };
    }

    public async Task<CourseResponseDto> CreateAsync(CourseInputDto dto)
    {
        var teacher = await _teacherRepository.GetByIdAsync(dto.TeacherId);
        if (teacher == null) throw new Exception("Teacher not found");

        var existingCourse = await _courseRepository.GetByNameAsync(dto.Name);
        if (existingCourse != null) throw new Exception("Course already exists");

        var course = new Course
        {
            Name = dto.Name,
            Status = dto.Status,
            TeacherId = dto.TeacherId,
            ImageUrl = dto.ImageUrl // <-- Guardamos la URL de Cloudinary
        };

        await _courseRepository.AddAsync(course);
        await _courseRepository.SaveChangesAsync();

        return new CourseResponseDto
        {
            Id = course.Id,
            Name = course.Name,
            Status = course.Status,
            TeacherId = teacher.Id,
            TeacherName = teacher.Name,
            ImageUrl = course.ImageUrl
        };
    }

    public async Task UpdateAsync(int id, CourseInputDto dto)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null) throw new Exception("Course not found");

        var teacher = await _teacherRepository.GetByIdAsync(dto.TeacherId);
        if (teacher == null) throw new Exception("Teacher not found");

        course.Name = dto.Name;
        course.Status = dto.Status;
        course.TeacherId = dto.TeacherId;
        course.ImageUrl = dto.ImageUrl; // <-- Actualizamos la URL de la imagen

        await _courseRepository.UpdateAsync(course);
        await _courseRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
            throw new Exception("Course not found");

        await _courseRepository.DeleteAsync(course);
        await _courseRepository.SaveChangesAsync();
    }
}
