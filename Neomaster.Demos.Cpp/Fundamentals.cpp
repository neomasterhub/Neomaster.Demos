#include "Fundamentals.h"
#include <iostream>
#include <sstream>

std::string Fundamentals::HelloWorld()
{
  std::cout << "Hello " << "World! " << 123;

  return "";
}

std::string Fundamentals::StdEndl_AsArg()
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

  return "";
}

std::string Fundamentals::StdEndl_AsFunc()
{
  std::cout << "Line 1";
  std::endl(std::cout);
  std::cout << "Line 2";

  // Line 1
  // Line 2

  return "";
}

std::string Fundamentals::StdEnds()
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

  return "";
}

std::string Fundamentals::CinGet()
{
  std::cout << "Press 'q', 'w', 'e', 'Enter'.\n";

  std::cout << (char)std::cin.get(); // q
  std::cout << (char)std::cin.get(); // w
  std::cout << (char)std::cin.get(); // e
  std::cout << std::cin.get(); // 10

  // qwe
  // (Enter) sends the typed line to the input buffer.
  // qwe10

  return "";
}

std::string Fundamentals::StdGetLine()
{
  std::string line;
  std::cout << "Press 'q', 'w', 'e', 'Enter'.\n";

  std::getline(std::cin, line);
  std::cout << line.length() << '\n';
  std::cout << line << '.';

  // 3
  // qwe.

  return "";
}

std::string Fundamentals::CinNextWordToVar()
{
  std::string word1;
  std::string word2;
  std::cout << "Print \" ab  cd \"\n";

  std::cin >> word1;
  std::cin >> word2;

  std::cout << word1 << "\n";
  std::cout << word2 << "\n";

  // ab
  // cd

  return "";
}

std::string Fundamentals::StringConstructors()
{
  std::string s1;
  std::string s2 = std::string();
  std::string s3 = std::string("Hello, World!!!");
  std::string s4 = std::string(s3);
  std::string s5 = std::string(s3, 7);
  std::string s6 = std::string(s3, 7, 5);
  std::string s7 = std::string(2, 'A');
  std::string s8 = std::string(2, 97);

  std::cout << "s1: \"" << s1 << "\"\n"; // ""
  std::cout << "s2: \"" << s2 << "\"\n"; // ""
  std::cout << "s3: \"" << s3 << "\"\n"; // "Hello, World!!!"
  s3 = "";
  std::cout << "s4: \"" << s4 << "\"\n"; // "Hello, World!!!"
  std::cout << "s5: \"" << s5 << "\"\n"; // "World!!!"
  std::cout << "s6: \"" << s6 << "\"\n"; // "World"
  std::cout << "s7: \"" << s7 << "\"\n"; // "AA"
  std::cout << "s8: \"" << s8 << "\"\n"; // "aa"

  return "";
}
