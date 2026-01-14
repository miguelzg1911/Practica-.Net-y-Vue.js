using Practica.Application.DTOs.Student;

namespace Practica.Application.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentResponseDto>> GetAllAsync();
    Task<StudentResponseDto> GetByIdAsync(int id);
    Task<StudentResponseDto> CreateAsync(StudentInputDto dto);
    Task UpdateAsync(int id, StudentInputDto dto);
    Task DeleteAsync(int id);
}