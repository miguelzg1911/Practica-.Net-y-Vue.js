using Microsoft.AspNetCore.Http;

namespace Practica.Application.Interfaces;

public interface IPhotoService
{
    Task<string> UploadPhotoAsync(IFormFile file);
}