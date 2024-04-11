## About the program

The program is a .Net OpenAPI. Currently, it has one endpoint (/api/image/processing) that can accept POST requests.
When you start the server, you need to provide two keys in Form Data:

- Image: This is where the encoded image string goes (currently, it can only handle Base64 and Hex encoding). 
You don't need to send it a string; you can also send it an image file, but I couldn't configure Swagger to accept images. 
I was able to send it an image file using Postman, and it was able to process it.

- Encoding Type: JPEG or PNG, it can currently handle these two.
The way it works is that you need to specify here whether the image you're sending is JPEG or PNG.
It will handle it accordingly and return it in this format. Make sure not to specify "JPG", but rather "JPEG".
If you're using Swagger:
0 - PNG
1 - JPEG

## About the technologies

- Server: .Net Core, and use SixLabors.ImageSharp, Swashbuckle.AspnetCore

Image processing made by C++, and uses OpenCV library. The connect, between Server and Image processing, is provided by dll file.

## Setup

1. Clone the repo:
  ```sh
  git clone https://github.com/StrangerDeer/image-processing.git
```

2. Setup Server

sh```
 dotnet restore
```
if it is not working:

Download from NuGet:
  - SixLabors.ImageSharp (v.: 3.1.3)
  - Microsoft.AspNet.WebApi.Core (v.: 5.3.0)
  - Swashbuckle.AspNetCore (v.: 6.5.0)
3. 
