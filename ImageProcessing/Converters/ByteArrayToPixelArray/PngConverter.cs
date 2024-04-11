﻿using PictureProcessing.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PictureProcessing.Converters.ByteArrayToPixelArray;

/// <summary>
/// Provides functionality to convert between byte arrays and pixel arrays for PNG images.
/// </summary>
/// <remarks>
/// This class inherits from BaseByteArrayPixelArrayConverter and implements methods to create images from byte arrays and vice versa for PNG images.
/// </remarks>
public class PngConverter : BaseByteArrayPixelArrayConverter
{
    /// <summary>
    /// Initializes a new instance of the PngConverter class with the PNG encoding type.
    /// </summary>
    /// <remarks>
    /// This constructor initializes a new instance of the PngConverter class with the PNG encoding type specified.
    /// </remarks>
    public PngConverter() : base(EncodingType.PNG)
    {
    }

    /// <summary>
    /// Creates an RGBA32 image from the provided byte array representing PNG image data.
    /// </summary>
    /// <param name="imgBytes">The byte array representing the PNG image data.</param>
    /// <returns>An RGBA32 image object representing the decoded image.</returns>
    /// <remarks>
    /// This method loads the PNG image data from the provided byte array into an RGBA32 image.
    /// </remarks>
    protected override Image<Rgba32> CreateImage(byte[] imgBytes)
    {
        return Image.Load<Rgba32>(imgBytes);
    }

    /// <summary>
    /// Creates a byte array representing PNG image data from the provided RGBA image.
    /// </summary>
    /// <param name="image">The RGBA32 image object.</param>
    /// <returns>A memory stream containing the byte array representing the PNG image data.</returns>
    /// <remarks>
    /// This method saves the RGBA image as a PNG image into a memory stream and returns the memory stream containing the byte array representing the PNG image data.
    /// </remarks>
    protected override MemoryStream CreateImgBytesFromRgba(Image<Rgba32> image)
    {
        MemoryStream st = new MemoryStream();
        image.SaveAsPng(st);
        st.Seek(0, SeekOrigin.Begin);
        return st;
    }
}