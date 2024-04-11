using System.Runtime.InteropServices;
using PictureProcessing.Converters.EncodeToByteArray;
using PictureProcessing.Converters.ByteArrayToPixelArray;
using PictureProcessing.Enums;
using PictureProcessing.Exceptions;

namespace PictureProcessing.Services;
/// <summary>
/// The <c>ImageProcessingService</c> class processes images.
/// Import <c>imagesettingcpp.dll</c>.
/// Currently, it has one method, <c>GaussianBlurImage</c>, which can blur an image encoded in a string.
/// The blur value defines in this class. Blur value now 11.
/// </summary>
public class ImageProcessingService : IImageProcessingService
{
    [DllImport("imagesettingcpp.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    private static extern void gaussianBlur([In, Out] [MarshalAs(UnmanagedType.LPArray)] byte[] image, int width, int height, int blurValue, byte[] result);

    private readonly int _blurValue = 11;
    private int _imgWidth, _imgHeight;
    private BaseByteArrayPixelArrayConverter _byteAndPixelArray;
    private readonly List<IDecodeToByteArr> _encodeDecoders = new();
    private readonly List<BaseByteArrayPixelArrayConverter> _byteArrayDecoders = new();

    /// <summary>
    /// Initializes a new instance of the <c>ImageProcessingService</c> class.
    /// </summary>
    /// <remarks>
    /// This constructor initializes the ImageProcessingService instance by filling its encode decoders and byte array converters.
    /// </remarks>
    public ImageProcessingService()
    {
        FillEncodeDecoders();
        FillByteArrayConverters();
    }
    
    /// <summary>
    /// Applies Gaussian blur effect to the provided image.
    /// </summary>
    /// <param name="image">The encoded image data to be processed. The encode only base64 or hex.</param>
    /// <param name="encodingType">The encoding type of the image data. The encode type only PNG or JPEG</param>
    /// <returns>A task representing the asynchronous operation that returns the blurred image as a byte array.</returns>
    /// <remarks>
    /// This method decodes the provided image data, applies Gaussian blur effect to it, and returns the resulting blurred image as a byte array.
    /// </remarks>
    public Task<byte[]> GaussianBlurImage(string image, EncodingType encodingType)
    {
        byte[] imageBytes = RunEncodeDecoders(image);
        imageBytes = RunByteArrayDecoders(imageBytes, encodingType);

        byte[] newImgBytes = new byte[imageBytes.Length];
        
        gaussianBlur(imageBytes, _imgWidth, _imgHeight, _blurValue, newImgBytes);
        
        byte[] result = _byteAndPixelArray.CompressPixelArray(newImgBytes, _imgWidth, _imgHeight);
        
        return Task.FromResult(result);
    }
    
    /// <summary>
    /// Fills the list of encode decoders with HexDecoder and Base64Decoder instances.
    /// </summary>
    /// <remarks>
    /// This method initializes the list of encode decoders used for decoding different encoding types such as hex and base64.
    /// If you want to add new encoder, pay attention to the order of additions.
    /// For example, if we swap the base64 decoder with the hex decoder,
    /// due to character collisions, the hex code is interpreted as base64,
    /// causing the program to run incorrectly afterward.
    /// Since base64 uses a-zA-Z0-9 and hex uses A-F0-9,
    /// I've observed that the converter incorrectly converts the hex code to base64
    /// </remarks>
    private void FillEncodeDecoders()
    {
        _encodeDecoders.Add(new HexDecoder());
        _encodeDecoders.Add(new Base64Decoder());
    }
    
    /// <summary>
    /// Fills the list of byte array converters with PngConverter and JpgConverter instances.
    /// </summary>
    /// <remarks>
    /// This method initializes the list of byte array converters used for converting pixel arrays to PNG or JPEG byte arrays.
    /// </remarks>
    private void FillByteArrayConverters()
    {
        _byteArrayDecoders.Add(new PngConverter());
        _byteArrayDecoders.Add(new JpegConverter());
    }
    
    /// <summary>
    /// Runs the encode decoders on the provided encoded image data and returns the decoded byte array.
    /// </summary>
    /// <param name="image">The encoded image data to be decoded. It is only in Base64 or Hex encode.</param>
    /// <returns>The decoded byte array representing the image data.</returns>
    /// <exception cref="NotSupportedEncodedTypeException">Thrown when the provided encoding type is not supported.</exception>
    /// <remarks>
    /// This method iterates through the list of encode decoders and applies each one to the encoded image data until a decoder is found that can decode it. 
    /// It then returns the decoded byte array. If no decoder is found for the provided encoding type, a <c>NotSupportedEncodedTypeException</c> is thrown.
    /// </remarks>
    private byte[] RunEncodeDecoders(string image)
    {
        
        foreach (IDecodeToByteArr decoder in _encodeDecoders)
        {
            if (decoder.IsDecode(image))
            {
                return decoder.Decode(image);
            }
        }

        throw new NotSupportedEncodedTypeException();
    }
    
    /// <summary>
    /// Runs the byte array decoders on the provided image byte array based on the specified encoding type and returns the resulting pixel array.
    /// </summary>
    /// <param name="imgBytes">The image byte array to be decoded.</param>
    /// <param name="encodingType">The encoding type of the image data. This is only in PNG or JPEG</param>
    /// <returns>The resulting pixel array after decoding the image byte array. Pixel array is Rgba form, so it includes 4 value.</returns>
    /// <exception cref="NotSupportedFileFormat">Thrown when the specified encoding type is not supported.</exception>
    /// <remarks>
    /// This method iterates through the list of byte array decoders and applies each one to the image byte array until a decoder is found that supports the specified encoding type. 
    /// It then converts the byte array to a pixel array and returns the resulting pixel array in Rgba format.
    /// If no decoder supports the specified encoding type, a NotSupportedFileFormat exception is thrown.
    /// </remarks>
    private byte[] RunByteArrayDecoders(byte[] imgBytes, EncodingType encodingType)
    {
        foreach (var decoder in _byteArrayDecoders)
        {
            if (decoder.IsImgEncodeType(encodingType))
            {
                _byteAndPixelArray = decoder;
                return decoder.UnCompressToPixelArr(imgBytes, ref _imgWidth, ref _imgHeight);
            }
        }

        throw new NotSupportedFileFormat(encodingType);
    }
    
}