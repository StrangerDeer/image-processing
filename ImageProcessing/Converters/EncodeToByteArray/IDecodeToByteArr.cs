namespace PictureProcessing.Converters.EncodeToByteArray;

/// <summary>
/// Represents an interface for decoding image data to byte arrays.
/// </summary>
/// <remarks>
/// This interface defines methods for checking if the provided data can be decoded and for decoding the data into byte arrays.
/// </remarks>
public interface IDecodeToByteArr
{
    /// <summary>
    /// Checks whether the provided image data can be decoded.
    /// </summary>
    /// <param name="image">The image data to check for decoding.</param>
    /// <returns>True if the image data can be decoded; otherwise, false.</returns>
    /// <remarks>
    /// This method checks whether the provided image data can be decoded into byte arrays.
    /// </remarks>
    bool IsDecode(string image);
    
    /// <summary>
    /// Decodes the provided image data into a byte array.
    /// </summary>
    /// <param name="image">The image data to decode.</param>
    /// <returns>A byte array representing the decoded image data.</returns>
    /// <remarks>
    /// This method decodes the provided image data into a byte array.
    /// </remarks>
    byte[] Decode(string image);
}