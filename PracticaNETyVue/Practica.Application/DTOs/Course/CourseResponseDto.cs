namespace Practica.Application.DTOs.Course;

public class CourseResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
}