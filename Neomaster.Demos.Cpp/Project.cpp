#include "Menu.h"
#include "Introduction.h"
#include <windows.h>

void main()
{
  SetConsoleOutputCP(CP_UTF8);

  Menu menu = Menu(
    "Introduction",
    {
      MenuItem("Hello, World; std:cout <<", []() { return Introduction::HelloWorld(); }),
      MenuItem("std::endl as arg", []() { return Introduction::StdEndl_AsArg(); }),
      MenuItem("std::endl as function", []() { return Introduction::StdEndl_AsFunc(); }),
      MenuItem("std::ends", []() { return Introduction::StdEnds(); }),
      MenuItem("std::cin.get", []() { return Introduction::CinGet(); }),
      MenuItem("std::getline", []() { return Introduction::StdGetLine(); }),
      MenuItem("std::cin >>", []() { return Introduction::CinNextWordToVar(); }),
      MenuItem("string constructors", []() { return Introduction::StringConstructors(); }),
      MenuItem("string item accessors: [], at()", []() { return Introduction::StringItemAccessors(); }),
      MenuItem("#pragma once", []() { return Introduction::PragmaOnce(); }),
      MenuItem("Get type name", []() { return Introduction::GetTypeName(); }),
      MenuItem("Pointer", []() { return Introduction::Pointer(); }),
      MenuItem("Pointer arg", []() { return Introduction::PointerArg(); }),
      MenuItem("nullptr", []() { return Introduction::Nullptr(); }),
    });
  menu.Show();
}
