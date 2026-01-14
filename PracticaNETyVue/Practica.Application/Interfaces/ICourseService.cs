using Practica.Application.DTOs.Course;

namespace Practica.Application.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<CourseResponseDto>> GetAllAsync();
    Task<CourseResponseDto?> GetByIdAsync(int id);
    Task<CourseResponseDto> CreateAsync(CourseInputDto dto);
    Task UpdateAsync(int id, CourseInputDto dto);
    Task DeleteAsync(int id);
}