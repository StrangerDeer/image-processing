namespace PictureProcessing.Exceptions;

public class NotSupportedEncodedTypeException : Exception
{
    public NotSupportedEncodedTypeException() : base ("Not supported encode type"){}
}