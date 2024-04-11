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
   ```sh
   dotnet restore
   ```

   if it is not working:

  Download from NuGet:
    - SixLabors.ImageSharp (v.: 3.1.3)
    - Microsoft.AspNet.WebApi.Core (v.: 5.3.0)
    - Swashbuckle.AspNetCore (v.: 6.5.0)

3.  If you want to create swagger, the swagger xml name must named by MyApi. (MyApi.xml)
   
4. Build!
5. If you built release version, just copy all dlls from 01-importan-bin to the C# project build folder.
6.  Run!

If you want to build dll from cpp source:

In Clion:

1. File -> Settings -> Build, Execution, Deployment -> Toolchains -> First must be Visual Studio, and compiler must be cl (You can find window right side, below Build Tool field).
2. Add vcpkg
3. Add opencv 4.8.0#1 and opencv 4 4.8.0#15; run CMake
((If vcpkg throw error:
  1. Open vcpkg/ports/liblzma/portfile.cmake
  2. Push ctrl+f -> REPO
  3. You can see this line : REPO tukaani-project/xz
  4. Write over for this line : REPO bminor/xz)) -> this problem solved today(2024.04.11), but I left it here, if it will be happen again.
5. File -> Settings -> Build, Execution, Deployment -> CMake -> Debug (or Release) -> Cache variables (it is an tabbed panel) and check the box (Show advenced)
6.  OPENCV_DIR's value "{personal location}\vcpkg\installed\x64-windows\share\opencv4"
   Apply, OK

   and now we reapet that, just different valeus:
    Protobuf_DIR: {personal}\vcpkg\installed\x64-windows\share\protobuf
    quirc_DIR: {personal}\vcpkg\installed\x64-windows\share\quirc
    TIFF_INCLUDE_DIR:  {personal}\vcpkg\installed\x64-windows\share\tiff
    TIFF_LIBRARY_DEBUG:  {personal}\vcpkg\installed\x64-windows\share\tiff
    TIFF_LIBRARY_RELEASE:  {personal}\vcpkg\installed\x64-windows\share\tiff
   
7. Now you can build the dll.

8. If you run app with your own built dll:
  1. cmake-build-debug (or cmake-build-release) folder includes the built dll: imagesettingcpp.dll -> that you copy c# build folder.
  2. {personal}\vcpkg\installed\x64-windows\bin -> copy all dll from this directory to c# build folder.
  3. if you want to run debug version, you need to copy all dll to c# build folder from {personal}\vcpkg\installed\x64-windows\debug\bin

## Contributor

[StrangerDeer](https://github.com/StrangerDeer)
