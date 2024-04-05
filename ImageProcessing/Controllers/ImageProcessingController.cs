using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PictureProcessing.Services;

namespace PictureProcessing.Controllers;

[ApiController]
[Route("api/image")]
//[Produces("application/json")] 
//[EnableCors("AllowAllHeaders")]

public class ImageProcessingController : ControllerBase
{
    private readonly IImageProcessingService _imageService;

    public ImageProcessingController(IImageProcessingService imageProcessingService)
    {
        _imageService = imageProcessingService;
    }

    [Route("processing")]
    [HttpPost]
    public async Task<IActionResult> image_processing([FromForm(Name = "Image")] string image)
    {
        try
        {
            byte[] imageBytes = Array.Empty<byte>();
            imageBytes = await _imageService.ProcessImage(image);
            
            var stream = await CreateImage(imageBytes);
            return File(stream, "image/png");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [NonAction]
    private async Task<FileStream> CreateImage(byte[] imageBytes)
    {
        var imagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
        await System.IO.File.WriteAllBytesAsync(imagePath, imageBytes);

        var stream = new FileStream(imagePath, FileMode.Open);

        return stream;
    }
}