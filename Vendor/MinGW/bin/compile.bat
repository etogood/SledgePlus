@setlocal
@set ORIGINAL_PATH_VARIABLE=%PATH%
@set PATH=%CD%\MinGW\bin;%ORIGINAL_PATH_VARIABLE%

MinGW\bin\g++.exe -std=gnu++11 .\__temp_code.cpp -o MinGW\bin\__temp_program.exe

start MinGW\bin\__temp_program.exe