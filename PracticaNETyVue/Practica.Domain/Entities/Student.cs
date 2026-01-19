using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica.Domain.Entities;

public class Student
{
    [Key] // Define que este es la llave primaria
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Indica que NO es autoincremental
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;

    // Relación con el Usuario (Opcional pero recomendada)
    [ForeignKey("Id")]
    public virtual User User { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}