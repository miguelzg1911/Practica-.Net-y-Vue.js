using Practica.Domain.Entities;

namespace Practica.Domain.Interfaces;

public interface IEnrollmentRepository
{
    Task AddAsync(Enrollment enrollment);
    Task DeleteAsync(int studentId, int courseId);
    
    Task<bool> ExistsAsync(int studentId, int courseId);
    
    Task<IEnumerable<Course>> GetCoursesByStudentAsync(int studentId);
    Task<IEnumerable<Student>> GetStudentsByCourseAsync(int courseId);
    
    Task SaveChangesAsync();
}