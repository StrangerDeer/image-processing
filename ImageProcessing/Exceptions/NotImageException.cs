namespace PictureProcessing.Exceptions;

public class NotImageException : Exception
{
    public NotImageException() : base("This encode does not an image!"){}
}