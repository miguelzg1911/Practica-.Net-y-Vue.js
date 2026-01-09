namespace Practica.Domain.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string Subject { get; set; }
    public string Email { get; set; }
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}