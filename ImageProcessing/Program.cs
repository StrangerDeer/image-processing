using System.Reflection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using PictureProcessing;
using PictureProcessing.Services;

//BUILD
//def build
var builder = WebApplication.CreateBuilder(args);

//Builder add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger",
        Description = "New swag",
        Version = "v1"
    });
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyApi.xml");
    o.IncludeXmlComments(filePath);
});

//Builder add controllers
builder.Services.AddControllersWithViews();

//Dependency inversion
builder.Services.AddTransient<IImageProcessingService, ImageProcessingService>();

//Allows the backend to handle large image uploads
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueCountLimit = int.MaxValue;
    o.ValueLengthLimit = int.MaxValue;  
    o.MultipartBodyLengthLimit = long.MaxValue;  
    o.MemoryBufferThreshold = int.MaxValue;  
});

//Add Mvc and AddControllers
builder.Services.AddMvc();
builder.Services.AddControllers();

//APP
//Def app
var app = builder.Build();

//Enables routing for handling incoming requests
app.UseRouting();

//Add middlewares to the app
app.UseMiddleware<ImageProcessingMiddleware>();

//Swagger
app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.SwaggerEndpoint("/swagger/v1/swagger.json","v1");
    o.RoutePrefix = string.Empty;
    o.DocumentTitle = "Image Processing";
});

//Maps incoming requests to controller actions based on route patterns
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();