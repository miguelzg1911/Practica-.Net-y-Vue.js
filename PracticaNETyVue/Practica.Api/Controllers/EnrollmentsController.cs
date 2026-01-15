using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practica.Application.DTOs.Enrollment;
using Practica.Application.Interfaces;

namespace Practica.Api.Controllers;

[ApiController]
[Route("api/enrollments")]
[Authorize]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [Authorize(Roles = "Student")]
    [HttpPost]
    public async Task<IActionResult> Enroll(EnrollmentInputDto dto)
    {
        await _enrollmentService.EnrollAsync(dto);
        return Ok();
    }

    [Authorize(Roles = "Student")]
    [HttpDelete]
    public async Task<IActionResult> Unenroll(EnrollmentInputDto dto)
    {
        await _enrollmentService.UnenrollAsync(dto);
        return NoContent();
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetCoursesByStudent(int studentId)
    {
        return Ok(await _enrollmentService.GetCoursesByStudentAsync(studentId));
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetStudentsByCourse(int courseId)
    {
        return Ok(await _enrollmentService.GetStudentsByCourseAsync(courseId));
    }
}