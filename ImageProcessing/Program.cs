using Microsoft.AspNetCore.Http.Features;
using PictureProcessing;
using PictureProcessing.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IImageProcessingService, ImageProcessingService>();

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueCountLimit = int.MaxValue;
    o.ValueLengthLimit = int.MaxValue;  
    o.MultipartBodyLengthLimit = long.MaxValue;  
    o.MemoryBufferThreshold = int.MaxValue;  
});

builder.Services.AddMvc();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseMiddleware<ImageProcessingMiddleware>();
//app.UseCors("AllowAllHeaders");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();