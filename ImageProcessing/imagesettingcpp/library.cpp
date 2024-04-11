#include "library.h"

#include <omp.h>
#include <opencv2/imgproc.hpp>

///<summary>
/// Slices the input image into multiple segments for parallel processing.
///</summary>
///<param name="inputImage">Input image to be sliced.</param>
///<param name="height">Height of the input image.</param>
///<param name="numThread">Number of threads used for parallel processing.</param>
///<param name="colsPerThread">Number of columns per thread.</param>
///<param name="remainingCols">Number of remaining columns after evenly distributing columns per thread.</param>
///<returns>Vector of image slices representing segments of the input image.</returns>
///<remarks>
/// This function divides the input image into multiple segments, each representing a portion of the input image.
/// The number of segments is determined by the numThread parameter, and each segment contains an equal number of columns,
/// except for the last segment, which may have additional columns if the total number of columns is not divisible evenly.
/// The height of each segment is equal to the height of the input image.
///</remarks>
std::vector<cv::Mat> sliceImage(const cv::Mat& inputImage, int height, int numThread, int colsPerThread, int remainingCols) {
  std::vector<cv::Mat> imageSlices;

  for(int i = 0; i < numThread; i++){
    int startCol ;
    int endCol;

    if(i == 0){
      startCol = 0;
    } else {
      startCol = i * colsPerThread;
    }

    if(i == numThread - 1 && remainingCols > 0){
      endCol = (i + 1) * colsPerThread + remainingCols;
    } else {
      endCol = (i + 1) * colsPerThread;
    }

    cv::Mat slice = inputImage(cv::Rect(startCol, 0, endCol - startCol, height));
    imageSlices.push_back(slice);
  }

  return imageSlices;
}

///<summary>
/// Applies Gaussian blur to each segment of the input image slices and accumulates the results into the output image using OpenMP for parallel processing.
///</summary>
///<param name="imageSlices">Vector of input image slices.</param>
///<param name="gaussian">Output image where the blurred image slices will be accumulated.</param>
///<param name="colsPerThread">Number of columns per thread.</param>
///<param name="height">Height of the input image.</param>
///<param name="blurValue">Value determining the intensity of the blur.</param>
///<remarks>
/// This function applies Gaussian blur to each segment of the input image slices using OpenMP to parallelize the process.
/// The resulting blurred image slices are accumulated into the output image.
/// The blur intensity is determined by the blurValue parameter.
///</remarks>
void makeGaussianBlur(const std::vector<cv::Mat>& imageSlices, cv::Mat& gaussian, int colsPerThread, int height, int blurValue) {
#pragma omp parallel for
  for (int i = 0; i < imageSlices.size(); ++i) {
    cv::Mat blurredSlice;
    cv::GaussianBlur(imageSlices[i], blurredSlice, cv::Size(blurValue, blurValue), 0, 0);

#pragma omp critical
    {
      gaussian(cv::Rect(i * colsPerThread, 0, imageSlices[i].cols, height)) += blurredSlice;
    }
  }
}

void gaussianBlur(uint8_t* image, int width, int height, int blurValue, uint8_t* result) {
  cv::Mat inputImage(height, width, CV_8UC4, image);
  cv::Mat gaussian(height, width, CV_8UC4);

  int numThread = omp_get_num_procs();

  int colsPerThread = width / numThread;
  int remainingCols = width % numThread;

  std::vector<cv::Mat> imageSlices = sliceImage(inputImage, height, numThread, colsPerThread, remainingCols);

  makeGaussianBlur(imageSlices, gaussian, colsPerThread, height, blurValue);

  size_t size = gaussian.rows * gaussian.cols * gaussian.channels();
  memcpy(reinterpret_cast<void *>(result), gaussian.data, size);
}

