using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practica.Application.Interfaces;

namespace Practica.Api.Controllers;

[ApiController]
[Route("api/upload")]
[Authorize]
public class UploadController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public UploadController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpPost("image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No se ha proporcionado un archivo válido.");

        try 
        {
            // Subimos a Cloudinary usando nuestro servicio centralizado
            var url = await _photoService.UploadPhotoAsync(file);
            return Ok(new { url });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al subir imagen: {ex.Message}");
        }
    }
}