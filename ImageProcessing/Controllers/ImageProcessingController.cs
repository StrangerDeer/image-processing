using Microsoft.AspNetCore.Mvc;
using PictureProcessing.Enums;
using PictureProcessing.Services;

namespace PictureProcessing.Controllers;

///<summary>
/// Class <c>ImageProcessingController</c> handles requests to the <c>api/image</c> endpoint.
/// Currently, it has a single <c>POST</c> request located <c>api/image/processing</c>
/// </summary>
[ApiController]
[Route("api/image")]

public class ImageProcessingController : ControllerBase
{
    private const string ImageProcEndPointName = "processing";
    private const string ImageFileDownloadingName = "image";
    private const string ImageKeyFormName = "Image";
    private const string EncodeKeyFormName = "Encoding Type";
    private readonly IImageProcessingService _imageService;

    /// <summary>
    /// Initializes a new instance of the ImageProcessingController class.
    /// </summary>
    /// <param name="imageProcessingService">The service responsible for image processing.</param>
    public ImageProcessingController(IImageProcessingService imageProcessingService)
    {
        _imageService = imageProcessingService;
    }
    
    ///<summary>
    /// This endpoint receives POST requests to the <c>/api/image/processing</c> endpoint.
    /// You can send an image to this endpoint, and it will return the processed image for download.
    /// It works with Form Data.
    /// Its parameters are:
    /// </summary>
    /// <param name="image">
    /// This is an encoded image.
    /// It can be sent with the form key <c>"Image"</c>.
    /// The endpoint currently supports (base64 and hex encoding) and accepts only (PNG or JPEG) file formats.
    /// Although the parameter is a string, you can also send a pure image file.
    /// If a client sends an image file to the endpoint, an ImageProcessingMiddleware class processes the image as middleware and converts it to base64 encoding.
    /// Disclaimer:
    /// I couldn't implement image uploading on the Swagger page, but I could successfully send images using Postman.
    /// </param>
    /// <param name="outputEncoding">
    /// This is an enum that allows the acceptance and return of (PNG or JPEG) files.
    /// The parameter is represented on the Swagger page as:
    /// <c>0: PNG</c>
    /// <c>1: JPEG</c>
    /// In Postman, you can achieve this by simply typing "PNG" or "JPEG".
    /// The form key is <c>Encoding Type"</c>.
    /// Be careful not to specify "JPG" when sending from Postman, as it will result in a NotSupportedFileFormatException.
    /// </param>
    /// <returns>An image in (PNG or JPEG).</returns>;
    [HttpPost]
    [Route(ImageProcEndPointName)]
  
    public async Task<IActionResult> image_processing(
        [FromForm(Name = ImageKeyFormName)] string image,
        [FromForm(Name = EncodeKeyFormName)] EncodingType outputEncoding)
    {
        try
        {
            byte[] imageBytes = await _imageService.GaussianBlurImage(image, outputEncoding);
            
            var stream = await CreateImage(imageBytes, outputEncoding);
            return File(stream,
                $"image/{outputEncoding.ToString().ToLower()}",
                $"{ImageFileDownloadingName}.{outputEncoding.ToString().ToLower()}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
    
    /// <summary>
    /// Creates an image file from the provided byte array and returns a FileStream object for the created image.
    /// </summary>
    /// <param name="imageBytes">The byte array representing the image data.</param>
    /// <param name="outputEncoding">The encoding type for the output image file (PNG or JPEG).</param>
    /// <returns>A FileStream object representing the created image file.</returns>
    [NonAction]
    private async Task<FileStream> CreateImage(byte[] imageBytes, EncodingType outputEncoding)
    {
        var imagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + $@".{outputEncoding.ToString().ToLower()}");
        await System.IO.File.WriteAllBytesAsync(imagePath, imageBytes);

        var stream = new FileStream(imagePath, FileMode.Open);

        return stream;
    }
}