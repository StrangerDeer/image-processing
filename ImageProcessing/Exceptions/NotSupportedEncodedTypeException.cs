namespace PictureProcessing.Exceptions;

/// <summary>
/// Represents an exception thrown when an unsupported encoding type is encountered.
/// </summary>
/// <remarks>
/// This exception is thrown when an operation encounters an encoding type that is not supported.
/// </remarks>
public class NotSupportedEncodedTypeException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <c>NotSupportedEncodedTypeException</c> class.
    /// </summary>
    /// <remarks>
    /// This constructor initializes a new instance of the <c>NotSupportedEncodedTypeException</c> class with a default error message indicating that the encoding type is not supported.
    /// </remarks>
    public NotSupportedEncodedTypeException() : base ("Not supported encode type"){}
}