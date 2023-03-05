@setlocal
@set ORIGINAL_PATH_VARIABLE=%PATH%
@set PATH=%CD%\MinGW\bin_n;%ORIGINAL_PATH_VARIABLE%

MinGW\bin_n\g++.exe -std=gnu++11 .\__temp_code.cpp -o MinGW\bin_n\__temp_program.exe

start MinGW\bin_n\__temp_program.exe