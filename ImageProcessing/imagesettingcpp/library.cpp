#include "library.h"

#include <opencv2/opencv.hpp>

void gaussianBlur(unsigned char* image, int width, int height) {
  cv::Mat inputImage(height, width, CV_8UC3, image);

  cv::Mat gaussian;

  cv::GaussianBlur(inputImage, gaussian, cv::Size(3,3), 0,0);

  int size = gaussian.rows * gaussian.cols * gaussian.channels();

  cv::current
}