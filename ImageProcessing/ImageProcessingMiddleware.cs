using System.Collections.Specialized;
using Microsoft.Extensions.Primitives;

namespace PictureProcessing;

public class ImageProcessingMiddleware
{
    private readonly RequestDelegate _next;

    public ImageProcessingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.HasFormContentType)
        {
            var form = await context.Request.ReadFormAsync();
            var file = form.Files["Image"];

            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    string base64Image = Convert.ToBase64String(fileBytes);
                    
                    var dictValues = new Dictionary<string, StringValues>(form);
                
                    dictValues["Image"] = new StringValues(base64Image);

                    var modifiedForm = new FormCollection(dictValues, form.Files);

                    context.Request.Form = modifiedForm;
                }
            }
        }

        await _next(context);
    }
}