#include "Fundamentals.h"
#include <iostream>
#include <sstream>

/// <summary>
/// Prints "Hello World! 123" to the standard output.
/// </summary>
void Fundamentals::HelloWorld()
{
  std::cout << "Hello " << "World! " << 123;
}

/// <summary>
/// Inserts a newline and flushes the output buffer.
/// Used as an argument to the output stream operator.
/// </summary>
void Fundamentals::StdEndl_AsArg()
{
  std::cout << "Line 1" << std::endl;
  std::cout << "Line 2" << std::endl;
  // Line 1
  // Line 2
  //

  std::ostringstream sout;
  sout << "Line 3" << std::endl;
  sout << "Line 4" << std::endl;
  std::cout << sout.str();
  // Line 3
  // Line 4
  //
}

/// <summary>
/// Inserts a newline and flushes the output buffer.
/// Used as a function taking the output stream as an argument.
/// </summary>
void Fundamentals::StdEndl_AsFunc()
{
  std::cout << "Line 1";
  std::endl(std::cout);
  std::cout << "Line 2";

  // Line 1
  // Line 2
}
