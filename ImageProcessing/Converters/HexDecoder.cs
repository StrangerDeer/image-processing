using System.Text.RegularExpressions;

namespace PictureProcessing.Converters;

public class HexDecoder : IDecodeToByteArr
{
    public bool IsDecode(string image)
    {
        try
        {
            var valid = Convert.FromHexString(image);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public byte[] Decode(string image)
    {
        return Convert.FromHexString(image);
    }
}