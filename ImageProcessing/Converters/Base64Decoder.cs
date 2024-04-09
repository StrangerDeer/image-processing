using System.Buffers.Text;
using System.Drawing;
using SixLabors.ImageSharp;

namespace PictureProcessing.Converters;

public class Base64Decoder : IDecodeToByteArr
{
    public bool IsDecode(string image)
    {
        try
        {
            var valid = Convert.FromBase64String(image);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public byte[] Decode(string image)
    {
        byte[] imageData = Convert.FromBase64String(image);

        return imageData;
    }
}