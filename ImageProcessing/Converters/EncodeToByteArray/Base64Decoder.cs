
namespace PictureProcessing.Converters.EncodeToByteArray;

/// <summary>
/// Provides functionality to decode image data encoded in Base64 format.
/// </summary>
/// <remarks>
/// This class implements the IDecodeToByteArr interface to decode image data encoded in Base64 format into byte arrays.
/// </remarks>
public class Base64Decoder : IDecodeToByteArr
{
    /// <summary>
    /// Checks whether the provided image data can be decoded from Base64 format.
    /// </summary>
    /// <param name="image">The image data to check for decoding.</param>
    /// <returns>True if the image data can be decoded from Base64 format; otherwise, false.</returns>
    /// <remarks>
    /// This method attempts to decode the provided image data from Base64 format.
    /// If the decoding is successful, it returns true; otherwise, it returns false.
    /// </remarks>
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

    /// <summary>
    /// Decodes the provided image data from Base64 format into a byte array.
    /// </summary>
    /// <param name="image">The image data to decode from Base64 format.</param>
    /// <returns>A byte array representing the decoded image data.</returns>
    /// <remarks>
    /// This method decodes the provided image data from Base64 format into a byte array.
    /// </remarks>
    public byte[] Decode(string image)
    {
        byte[] imageData = Convert.FromBase64String(image);
        
        return imageData;

    }
}