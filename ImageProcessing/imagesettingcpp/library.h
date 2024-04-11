#pragma once

#include <cstdint>
#include <vector>
#include <opencv2/core.hpp>

/// <summary>
/// Applies Gaussian blur to an RGBA image.
/// </summary>
/// <param name="image">Pointer to the RGBA image data.</param>
/// <param name="width">Width of the image.</param>
/// <param name="height">Height of the image.</param>
/// <param name="blurValue">Size of the Gaussian kernel for blurring.
/// The value must be an odd number.  </param>
/// <param name="result">Pointer to store the resulting blurred image data.
/// The length of the result has to be the same size as the image pointer</param>
/// <remarks>
/// This function applies Gaussian blur to an RGBA image represented by the input image data.
/// It uses OpenCV's GaussianBlur function to perform the blurring operation.
/// The OpenCV GaussianBlur function appears to run in multithreading mode.
/// I measured its runtime.
/// When I use OMP parallelization, the runtime is longer compared to when I only call the GaussianBlur function.
/// </remarks>
extern "C" __declspec(dllexport) void gaussianBlur(uint8_t* image, int width, int height, int blurValue, uint8_t* result);

