using System.Diagnostics;
using PictureProcessing.Converters;
using PictureProcessing.Exceptions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PictureProcessing.Services;

public class ImageProcessingService : IImageProcessingService
{
    private readonly List<IDecodeToByteArr> _decoders = new();

    public ImageProcessingService()
    {
        FillDecoders();
    }
    public Task<byte[]> ProcessImage(string image)
    {
        byte[] imageBytes = Array.Empty<byte>();
        Stopwatch w = new Stopwatch();
        w.Start();
        
        foreach (IDecodeToByteArr decode in _decoders)
        {
            if (decode.IsDecode(image))
            {
                imageBytes = decode.Decode(image);
                break;
            }
        }
        
        w.Stop();
        Console.WriteLine(w.ElapsedMilliseconds);

        if (imageBytes.Length == 0)
        {
            throw new NotSupportedEncodedTypeException();
        }
        
        return Task.FromResult(imageBytes);
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