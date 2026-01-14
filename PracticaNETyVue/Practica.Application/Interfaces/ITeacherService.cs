using Practica.Application.DTOs.Teacher;

namespace Practica.Application.Interfaces;

public interface ITeacherService
{
    Task<IEnumerable<TeacherResponseDto>> GetAllAsync();
    Task<TeacherResponseDto?> GetByIdAsync(int id);
    Task<TeacherResponseDto> CreateAsync(TeacherInputDto dto);
    Task UpdateAsync(int id, TeacherInputDto dto);
    Task DeleteAsync(int id);
}