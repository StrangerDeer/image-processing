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
        
        foreach (IDecodeToByteArr decode in _decoders)
        {
            if (decode.IsDecode(image))
            {
                imageBytes = decode.Decode(image);
                break;
            }
        }

        try
        {
            using (Image<Rgba32> img = Image.Load<Rgba32>(imageBytes))
            {

            }
        }
        catch
        {
            throw new NotSupportedEncodedTypeException();
        }

        if (imageBytes.Length == 0)
        {
            throw new NotSupportedEncodedTypeException();
        }
        
        return Task.FromResult(imageBytes);
    }

    private void FillDecoders()
    {
        _decoders.Add(new Base64Decoder());
    }
}