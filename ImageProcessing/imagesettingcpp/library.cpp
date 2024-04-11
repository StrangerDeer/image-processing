#include "library.h"

#include <omp.h>
#include <opencv2/imgproc.hpp>

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

