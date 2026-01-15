using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practica.Application.DTOs.Student;
using Practica.Application.Interfaces;

namespace Practica.Api.Controllers;

[ApiController]
[Route("api/students")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _studentService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null)
            return NotFound();

        return Ok(student);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudentInputDto dto)
    {
        var result = await _studentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentInputDto dto)
    {
        await _studentService.UpdateAsync(id, dto);
        return NoContent();
    }

    [Authorize(Roles = "Teacher")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteAsync(id);
        return NoContent();
    }
}