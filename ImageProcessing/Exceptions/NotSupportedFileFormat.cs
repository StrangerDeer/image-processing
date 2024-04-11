using PictureProcessing.Enums;

namespace PictureProcessing.Exceptions;

/// <summary>
/// Represents an exception thrown when an unsupported file format is encountered.
/// </summary>
/// <remarks>
/// This exception is thrown when an operation encounters a file format that is not supported.
/// </remarks>
public class NotSupportedFileFormat : Exception
{
    /// <summary>
    /// Initializes a new instance of the NotSupportedFileFormat class with the specified encoding type.
    /// </summary>
    /// <param name="encode">The unsupported encoding type.</param>
    /// <remarks>
    /// This constructor initializes a new instance of the NotSupportedFileFormat class with a custom error message indicating that the specified encoding type is not supported.
    /// </remarks>
    public NotSupportedFileFormat(EncodingType encode) : base($"{encode} is not supported format!"){}
}