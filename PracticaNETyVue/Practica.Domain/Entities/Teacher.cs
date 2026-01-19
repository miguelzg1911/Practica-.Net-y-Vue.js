using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica.Domain.Entities;

public class Teacher
{
    [Key] // Define que este es la llave primaria
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Indica que NO es autoincremental
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}