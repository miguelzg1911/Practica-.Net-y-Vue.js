namespace Practica.Domain.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}