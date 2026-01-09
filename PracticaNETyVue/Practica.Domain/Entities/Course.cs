namespace Practica.Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    
    public int StudentId { get; set; }
    public Student Student { get; set; }
}