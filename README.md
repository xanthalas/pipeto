# pipeto
Utility for opening output from STDOUT in any application

This utility is used by piping the output from a command into it and passing the name of an application in which to open that output.

It does so by reading the data from STDIN and writing it to a temporary file. This file is then opened in the program given.

Usage: *command* | pipeto application

For example: dir | pipeto notepad
