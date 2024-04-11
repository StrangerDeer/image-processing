#pragma once

#include <cstdint>
#include <vector>
#include <opencv2/core.hpp>

///<summary>
/// Applies Gaussian blur to an input image using OpenMP for parallel processing.
///</summary>
///<param name="image">Pointer to the input image data.</param>
///<param name="width">Width of the input image.</param>
///<param name="height">Height of the input image.</param>
///<param name="blurValue">Value determining the intensity of the blur.</param>
///<param name="result">Pointer to the buffer where the blurred image data will be stored.
/// The length of the result has to be the same size as the image pointer</param>
///<remarks>
/// This function takes an input image represented as a pointer to image data in memory and its dimensions.
/// It applies Gaussian blur to the input image using OpenMP to parallelize the process.
/// The blur intensity is determined by the blurValue parameter.
/// The resulting blurred image data is stored in the result buffer.
///</remarks>
extern "C" __declspec(dllexport) void gaussianBlur(uint8_t* image, int width, int height, int blurValue, uint8_t* result);

