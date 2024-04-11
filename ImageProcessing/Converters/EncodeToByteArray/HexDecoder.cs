namespace PictureProcessing.Converters.EncodeToByteArray;

/// <summary>
/// Provides functionality to decode image data encoded in hexadecimal format.
/// </summary>
/// <remarks>
/// This class implements the <c>IDecodeToByteArr</c> interface to decode image data encoded in hexadecimal format into byte arrays.
/// </remarks>
public class HexDecoder : IDecodeToByteArr
{
    /// <summary>
    /// Checks whether the provided image data can be decoded from hexadecimal format.
    /// </summary>
    /// <param name="image">The image data to check for decoding.</param>
    /// <returns>True if the image data can be decoded from hexadecimal format; otherwise, false.</returns>
    /// <remarks>
    /// This method attempts to decode the provided image data from hexadecimal format.
    /// If the decoding is successful, it returns true; otherwise, it returns false.
    /// </remarks>
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

    /// <summary>
    /// Decodes the provided image data from hexadecimal format into a byte array.
    /// </summary>
    /// <param name="image">The image data to decode from hexadecimal format.</param>
    /// <returns>A byte array representing the decoded image data.</returns>
    /// <remarks>
    /// This method decodes the provided image data from hexadecimal format into a byte array.
    /// </remarks>
    public byte[] Decode(string image)
    {
        return Convert.FromHexString(image);
    }
}