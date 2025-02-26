﻿using Microsoft.AspNetCore.Mvc;
using s3_storage_api.Consants;

namespace s3_storage_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StorageController : ControllerBase
{
    private readonly string _storagePath;

    public StorageController(IWebHostEnvironment env)
    {
        _storagePath = Path.Combine(env.ContentRootPath, AppConstants.StorageDirectory);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");

        if (file.Length > AppConstants.MaxFileSize)
            return BadRequest($"File size exceeds the maximum limit of {AppConstants.MaxFileSizeInMegabytes}MB.");

        var uuid = Guid.NewGuid().ToString();
        var fileNameModified = $"{uuid}___{file.FileName}";
        
        var filePath = Path.Combine(_storagePath, fileNameModified);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var encodedFileName = Uri.EscapeDataString(fileNameModified);
        return Ok(new { Message = "File uploaded successfully", FilePath = encodedFileName });
    }

    [HttpGet("download/{fileName}")]
    public IActionResult Download(string fileName)
    {
        var decodedFileName = Uri.UnescapeDataString(fileName);
        var filePath = Path.Combine(_storagePath, decodedFileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "application/octet-stream", fileName);
    }

    [HttpGet("list")]
    public IActionResult List()
    {
        var files = Directory.GetFiles(_storagePath).Select(Path.GetFileName);
        return Ok(files);
    }
}
