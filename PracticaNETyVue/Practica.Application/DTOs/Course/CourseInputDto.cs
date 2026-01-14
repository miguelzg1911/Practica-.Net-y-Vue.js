namespace Practica.Application.DTOs.Course;

public class CourseInputDto
{
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public int StudentId { get; set; }
}