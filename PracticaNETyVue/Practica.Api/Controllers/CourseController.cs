using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practica.Application.DTOs.Course;
using Practica.Application.Interfaces;

namespace Practica.Api.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _courseService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _courseService.GetByIdAsync(id));
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> Create(CourseInputDto dto)
    {
        var course = await _courseService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CourseInputDto dto)
    {
        await _courseService.UpdateAsync(id, dto);
        return NoContent();
    }

    [Authorize(Roles = "Teacher")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _courseService.DeleteAsync(id);
        return NoContent();
    }
}