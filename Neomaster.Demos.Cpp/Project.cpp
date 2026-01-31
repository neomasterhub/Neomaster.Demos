#include "Menu.h"
#include "Fundamentals.h"
#include <windows.h>

void main()
{
  SetConsoleOutputCP(CP_UTF8);

  Menu menu = Menu(
    {
      MenuItem("Hello, World; std:cout <<", []() { return Fundamentals::HelloWorld(); }),
      MenuItem("std::endl as arg", []() { return Fundamentals::StdEndl_AsArg(); }),
      MenuItem("std::endl as function", []() { return Fundamentals::StdEndl_AsFunc(); }),
      MenuItem("std::ends", []() { return Fundamentals::StdEnds(); }),
      MenuItem("std::cin.get", []() { return Fundamentals::CinGet(); }),
      MenuItem("std::getline", []() { return Fundamentals::StdGetLine(); }),
      MenuItem("std::cin >>", []() { return Fundamentals::CinNextWordToVar(); }),
      MenuItem("string constructors", []() { return Fundamentals::StringConstructors(); }),
    });
  menu.Show();
}
