using Microsoft.Extensions.Primitives;

namespace PictureProcessing;

/// <summary>
/// Middleware for processing image files in HTTP requests.
/// </summary>
/// <remarks>
/// This middleware intercepts incoming HTTP requests and processes image files sent as form data. 
/// If an image is detected in the form data, it converts the image to base64 encoding and modifies the request form accordingly.
/// </remarks>
public class ImageProcessingMiddleware
{
    
    private const string ImgKeyName = "Image";
    private readonly RequestDelegate _next;
    
    /// <summary>
    /// Initializes a new instance of the ImageProcessingMiddleware class.
    /// </summary>
    /// <param name="next">The delegate representing the next middleware in the HTTP request pipeline.</param>
    /// <remarks>
    /// This constructor initializes a new instance of the ImageProcessingMiddleware class with the specified request delegate
    /// representing the next middleware in the HTTP request pipeline.
    /// </remarks>
    public ImageProcessingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Processes the incoming HTTP request to handle image data in form content.
    /// </summary>
    /// <param name="context">The HttpContext representing the current HTTP request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method intercepts incoming HTTP requests and checks if they contain form content.
    /// If form content is found and it contains an image file, the method converts the image to base64 encoding 
    /// and modifies the request form accordingly before passing it to the next middleware in the pipeline.
    /// </remarks>
    public async Task Invoke(HttpContext context)
    {
        if (context.Request.HasFormContentType)
        {
            var form = await context.Request.ReadFormAsync();
            var file = form.Files[ImgKeyName];

            if (file != null)
            {
                byte[] fileBytes = await CreateFileBytes(file);
                
                string base64Image = Convert.ToBase64String(fileBytes);
                
                var modifiedForm = CreateNewForm(form, base64Image);

                context.Request.Form = modifiedForm;
                
            }
        }

        await _next(context);
    }

    /// <summary>
    /// Reads the content of the provided file into a byte array asynchronously.
    /// </summary>
    /// <param name="file">The file to read.</param>
    /// <returns>A byte array containing the content of the file.</returns>
    /// <remarks>
    /// This method asynchronously copies the content of the provided file into a memory stream 
    /// and reads the memory stream into a byte array before returning it.
    /// </remarks>
    private async Task<byte[]> CreateFileBytes(IFormFile file)
    {
        var memoryStream = new MemoryStream();
        
        await file.CopyToAsync(memoryStream);
        
        return memoryStream.ToArray();
    }

    /// <summary>
    /// Creates a new form collection with the provided image data added.
    /// </summary>
    /// <param name="form">The original form collection.</param>
    /// <param name="base64Image">The base64 encoded image data to add to the form.</param>
    /// <returns>A new form collection with the image data added.</returns>
    /// <remarks>
    /// This method creates a new dictionary of form values based on the original form collection, 
    /// replaces the existing image data with the provided base64 encoded image data, and constructs a new FormCollection instance with the updated values.
    /// </remarks>
    private FormCollection CreateNewForm(IFormCollection form, string base64Image)
    {
        var dictValues = new Dictionary<string, StringValues>(form);
                
        dictValues[ImgKeyName] = new StringValues(base64Image);

        return new FormCollection(dictValues, form.Files);
    }
}