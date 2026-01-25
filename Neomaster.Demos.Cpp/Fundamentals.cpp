#include "Fundamentals.h"
#include <iostream>
#include <sstream>

void Fundamentals::HelloWorld()
{
  std::cout << "Hello " << "World! " << 123;
}

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

void Fundamentals::StdEndl_AsFunc()
{
  std::cout << "Line 1";
  std::endl(std::cout);
  std::cout << "Line 2";

  // Line 1
  // Line 2
}

void Fundamentals::StdEnds()
{
  std::ostringstream sout;
  sout << "1" << std::ends << "2";

  for (char c : sout.str())
  {
    std::cout << int(c) << '\n';
  }

  // 49
  // 0
  // 50
}

void Fundamentals::CinGet()
{
  std::cout << "Press 'q', 'w', 'e', 'Enter'.\n";

  std::cout << (char)std::cin.get(); // q
  std::cout << (char)std::cin.get(); // w
  std::cout << (char)std::cin.get(); // e
  std::cout << std::cin.get(); // 10

  // qwe
  // (Enter) sends the typed line to the input buffer.
  // qwe10
}

void Fundamentals::StdGetLine()
{
  std::string line;
  std::cout << "Press 'q', 'w', 'e', 'Enter'.\n";

  std::getline(std::cin, line);
  std::cout << line.length() << '\n';
  std::cout << line << '.';

  // 3
  // qwe.
}
