namespace PictureProcessing.Converters;

public interface IDecodeToByteArr
{
    bool IsDecode(string image);
    byte[] Decode(string image);
}