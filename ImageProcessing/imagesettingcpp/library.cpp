#include "library.h"

#include <opencv2/opencv.hpp>

void gaussianBlur(unsigned char* image, int width, int height, unsigned char* result) {

  cv::Mat inputImage(height, width, CV_8UC3, image);

  cv::Mat gaussian(height, width, CV_8UC3);

  cv::GaussianBlur(inputImage, gaussian, cv::Size(3,3), 0,0);

  memcpy(result, gaussian.data, gaussian.rows * gaussian.cols * gaussian.channels());

}