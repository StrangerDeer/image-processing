namespace PictureProcessing.Services;

public interface IImageProcessingService
{
    Task<byte[]> ProcessImage(string image);
}