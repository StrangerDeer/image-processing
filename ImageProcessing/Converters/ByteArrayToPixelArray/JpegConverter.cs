using PictureProcessing.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PictureProcessing.Converters.ByteArrayToPixelArray;

/// <summary>
/// Provides functionality to convert between byte arrays and pixel arrays for JPEG images.
/// </summary>
/// <remarks>
/// This class inherits from BaseByteArrayPixelArrayConverter and implements methods to create images from byte arrays and vice versa for JPEG images.
/// </remarks>
public class JpegConverter : BaseByteArrayPixelArrayConverter
{
    /// <summary>
    /// Initializes a new instance of the JpgConverter class with the JPEG encoding type.
    /// </summary>
    /// <remarks>
    /// This constructor initializes a new instance of the JpgConverter class with the JPEG encoding type specified.
    /// </remarks>
    public JpegConverter() : base(EncodingType.JPEG)
    {}

    /// <summary>
    /// Creates an RGBA32 image from the provided byte array representing JPEG image data.
    /// </summary>
    /// <param name="imgBytes">The byte array representing the JPEG image data.</param>
    /// <returns>An RGBA image object representing the decoded image.</returns>
    /// <remarks>
    /// This method loads the JPEG image data from the provided byte array into an RGBA32 image, then converts it to an RGBA image.
    /// </remarks>
    protected override Image<Rgba32> CreateImage(byte[] imgBytes)
    {
        MemoryStream stream = new MemoryStream(imgBytes);
        Image<Rgb24> rgbImg = Image.Load<Rgb24>(stream);

        return rgbImg.CloneAs<Rgba32>();
    }

    /// <summary>
    /// Creates a byte array representing JPEG image data from the provided RGBA32 image.
    /// </summary>
    /// <param name="image">The RGBA32 image object.</param>
    /// <returns>A memory stream containing the byte array representing the JPEG image data.</returns>
    /// <remarks>
    /// This method saves the RGBA32 image as a JPEG image into a memory stream and returns the memory stream containing the byte array representing the JPEG image data.
    /// </remarks>
    protected override MemoryStream CreateImgBytesFromRgba(Image<Rgba32> image)
    {
        MemoryStream st = new MemoryStream();
        image.SaveAsJpeg(st);
        st.Seek(0, SeekOrigin.Begin);
        return st;
    }
}