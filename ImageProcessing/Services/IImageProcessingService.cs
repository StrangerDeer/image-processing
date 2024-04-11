
using PictureProcessing.Enums;

namespace PictureProcessing.Services;
/// <summary>
/// <c>IImageProcessingService</c> is an interface implemented by the <c>ImageProcessingService</c> class.
/// </summary>
public interface IImageProcessingService
{
    /// <summary>
    /// Implemented in <c>ImageProcessingService</c> class.
    /// </summary>
    Task<byte[]> GaussianBlurImage(string image, EncodingType encodingType);
}