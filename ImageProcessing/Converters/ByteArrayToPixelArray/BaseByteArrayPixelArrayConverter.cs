using PictureProcessing.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PictureProcessing.Converters.ByteArrayToPixelArray;

/// <summary>
/// Provides a base class for converting between byte arrays and pixel arrays for image processing.
/// </summary>
/// <remarks>
/// This abstract class defines methods for creating images from byte arrays,
/// creating byte arrays from pixel arrays,
/// and converting between pixel arrays and byte arrays.
/// This class store the encoding type.
/// </remarks>

public abstract class BaseByteArrayPixelArrayConverter
{
    private readonly EncodingType _encode;
    
    /// <summary>
    /// Initializes a new instance of the <c>BaseByteArrayPixelArrayConverter</c> class with the specified encoding type.
    /// </summary>
    /// <param name="encode">The encoding type used for image data.</param>
    /// <remarks>
    /// This constructor initializes a new instance of the <c>BaseByteArrayPixelArrayConverter</c> class with the specified encoding type.
    /// </remarks>

    protected BaseByteArrayPixelArrayConverter(EncodingType encode)
    {
        _encode = encode;
    }

    /// <summary>
    /// Creates an image from the provided byte array.
    /// </summary>
    /// <param name="imgBytes">The byte array representing the image data.</param>
    /// <returns>An image object, in Rgba 32 bit, representing the decoded image.</returns>
    /// <remarks>
    /// This method is abstract and must be implemented by derived classes to create an image from the provided byte array.
    /// </remarks>
    protected abstract Image<Rgba32> CreateImage(byte[] imgBytes);
    
    /// <summary>
    /// Creates a byte array representing image data from the provided RGBA32 image.
    /// </summary>
    /// <param name="image">The RGBA image object.</param>
    /// <returns>A memory stream containing the byte array representing the image data.</returns>
    /// <remarks>
    /// This method is abstract and must be implemented by derived classes to create a byte array representing image data from the provided RGBA32 image.
    /// </remarks>
    protected abstract MemoryStream CreateImgBytesFromRgba(Image<Rgba32> image);
    
    /// <summary>
    /// Checks whether the specified encoding type matches the encoding type used for image data.
    /// </summary>
    /// <param name="encode">The encoding type to compare.</param>
    /// <returns>True if the specified encoding type matches the encoding type used for image data; otherwise, false.</returns>
    /// <remarks>
    /// This method compares the specified encoding type with the encoding type used for image data and returns true if they match,
    /// indicating that the specified encoding type is compatible with the image data encoding type.
    /// </remarks>
    public bool IsImgEncodeType(EncodingType encode)
    {
        return _encode.Equals(encode);
    }
    
    /// <summary>
    /// Converts the provided byte array representing image data into a pixel array (in Rgba32).
    /// </summary>
    /// <param name="imgBytes">The byte array representing the image data.</param>
    /// <param name="imgWidth">The width of the image.</param>
    /// <param name="imgHeight">The height of the image.</param>
    /// <returns>A byte array representing the pixel data of the image.</returns>
    /// <remarks>
    /// This method creates an image from the provided byte array, extracts the pixel data, and stores it in a new byte array representing
    /// the pixel array of the image (in Rgba32).
    /// The method also updates the provided image width and height parameters with the dimensions of the image.
    /// </remarks>
    public byte[] UnCompressToPixelArr(byte[] imgBytes, ref int imgWidth, ref int imgHeight)
    {
        var img = CreateImage(imgBytes);
        
        imgWidth = img.Width;
        imgHeight = img.Height;
        
        byte[] pixelData = new byte[imgWidth * imgHeight * 4];

        int index = 0;
        for (int y = 0; y < imgHeight; y++)
        {
            for (int x = 0; x < imgWidth; x++)
            {
                Rgba32 pixel = img[x, y];
                pixelData[index++] = pixel.R;
                pixelData[index++] = pixel.G;
                pixelData[index++] = pixel.B;
                pixelData[index++] = pixel.A;
            }
        }

        return pixelData;
    }
    
    /// <summary>
    /// Converts the provided pixel array (in Rgba32) into a byte array representing image data.
    /// </summary>
    /// <param name="pixels">The byte array representing the pixel data of the image.</param>
    /// <param name="imgWidth">The width of the image.</param>
    /// <param name="imgHeight">The height of the image.</param>
    /// <returns>A byte array representing the image data.</returns>
    /// <remarks>
    /// This method loads the pixel data from the provided byte array into an RGBA image with the specified dimensions.
    /// It then creates a memory stream containing the byte representation of the RGBA image and returns the byte array representation of the image data.
    /// </remarks>
    public byte[] CompressPixelArray(byte[] pixels, int imgWidth, int imgHeight)
    {
        var img = Image.LoadPixelData<Rgba32>(pixels, imgWidth, imgHeight);
        MemoryStream stream = CreateImgBytesFromRgba(img);
        
        return stream.ToArray();
        
    }
}