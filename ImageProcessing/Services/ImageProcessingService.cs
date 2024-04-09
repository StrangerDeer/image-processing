using System.Diagnostics;
using System.Runtime.InteropServices;
using PictureProcessing.Converters;
using PictureProcessing.Exceptions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PictureProcessing.Services;

public class ImageProcessingService : IImageProcessingService
{
    [DllImport("imagesettingcpp.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void gaussianBlur(ref byte[] image, int width, int height, ref byte[] result);
    
    private readonly List<IDecodeToByteArr> _decoders = new();

    public ImageProcessingService()
    {
        FillDecoders();
    }
    public Task<byte[]> ProcessImage(string image)
    {
        int width = 200;
        int heigth = 200;
        byte[] imageBytes = Array.Empty<byte>();
        byte[] result = new byte[width * heigth];
        
        foreach (IDecodeToByteArr decode in _decoders)
        {
            if (decode.IsDecode(image))
            {
                imageBytes = decode.Decode(image);
                break;
            }
        }

        if (imageBytes.Length == 0)
        {
            throw new NotSupportedEncodedTypeException();
        }
        
        gaussianBlur(ref imageBytes, width, heigth, ref result);
        
        return Task.FromResult(result);
    }

    private void FillDecoders()
    {
        _decoders.Add(new HexDecoder());
        _decoders.Add(new Base64Decoder());
    }

    private bool IsImage(byte[] imageBytes)
    {
        try
        {
            Image<Rgba32> img = Image.Load<Rgba32>(imageBytes);
            return true;
        }
        catch
        {
            return false;
        }
    }
}