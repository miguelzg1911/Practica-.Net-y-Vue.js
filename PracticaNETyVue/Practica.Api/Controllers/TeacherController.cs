using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practica.Application.DTOs.Teacher;
using Practica.Application.Interfaces;

namespace Practica.Api.Controllers;

[ApiController]
[Route("api/teachers")]
[Authorize]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _teacherService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var teacher = await _teacherService.GetByIdAsync(id);
        if (teacher == null)
            return NotFound();

        return Ok(teacher);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TeacherInputDto dto)
    {
        var result = await _teacherService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TeacherInputDto dto)
    {
        await _teacherService.UpdateAsync(id, dto);
        return NoContent();
    }

    [Authorize(Roles = "Teacher")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _teacherService.DeleteAsync(id);
        return NoContent();
    }
}