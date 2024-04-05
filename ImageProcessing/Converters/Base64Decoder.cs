using System.Buffers.Text;

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
        return Convert.FromBase64String(image);
    }
}