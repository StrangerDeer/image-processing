cmake_minimum_required(VERSION 3.10)
project(imagesettingcpp)

set(CMAKE_CXX_STANDARD 17)
SET(CMAKE_WINDOWS_EXPORT_ALL_SYMBOLS ON)

find_package(OpenCV CONFIG REQUIRED)

include_directories(${CMAKE_CURRENT_SOURCE_DIR})

set(EXPORT_DIR "../bin/Debug/net7.0")
file(MAKE_DIRECTORY ${EXPORT_DIR})
file(COPY ${CMAKE_CURRENT_BINARY_DIR}/imagesettingcpp.dll DESTINATION ${EXPORT_DIR})

add_library(imagesettingcpp SHARED library.h library.cpp)

target_link_libraries(imagesettingcpp
        PRIVATE opencv_ml opencv_dnn opencv_core opencv_flann)