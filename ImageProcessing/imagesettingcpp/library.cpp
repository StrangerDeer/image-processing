#include "library.h"

#include <omp.h>
#include <opencv2/opencv.hpp>

void gaussianBlur(uint8_t* image, int width, int height, int blurValue, uint8_t* result) {

  cv::Mat inputImage(height, width, CV_8UC4, image);

  cv::Mat gaussian(height, width, CV_8UC4);

#pragma omp parallel for
  for (int i = 0; i < height; ++i) {
    cv::Mat rowInputImage = inputImage.row(i);
    cv::Mat rowGaussian = gaussian.row(i);
    cv::GaussianBlur(rowInputImage, rowGaussian, cv::Size(blurValue, blurValue), 0, 0);
  }

  size_t size = gaussian.rows * gaussian.cols * gaussian.channels();
  memcpy(reinterpret_cast<void *>(result), gaussian.data, size);

}