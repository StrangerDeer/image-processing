using Microsoft.AspNetCore.Http.Features;
using PictureProcessing.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IImageProcessingService, ImageProcessingService>();

builder.Services.Configure<FormOptions>(o => {  
    o.ValueLengthLimit = int.MaxValue;  
    o.MultipartBodyLengthLimit = long.MaxValue;  
    o.MemoryBufferThreshold = int.MaxValue;  
});

builder.Services.AddMvc();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
//app.UseCors("AllowAllHeaders");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();