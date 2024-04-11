cmake_minimum_required(VERSION 3.10)
project(imagesettingcpp)

set(CMAKE_CXX_STANDARD 17)
SET(CMAKE_WINDOWS_EXPORT_ALL_SYMBOLS ON)

find_package(OpenCV CONFIG REQUIRED)

include_directories(${CMAKE_CURRENT_SOURCE_DIR})

add_library(imagesettingcpp SHARED library.h library.cpp)

target_link_libraries(imagesettingcpp
        PRIVATE ${OpenCV_LIBS})