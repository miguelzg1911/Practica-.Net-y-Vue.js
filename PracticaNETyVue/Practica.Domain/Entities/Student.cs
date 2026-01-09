namespace Practica.Domain.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}