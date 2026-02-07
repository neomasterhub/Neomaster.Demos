#pragma once
#include <iostream>
class Introduction
{
public:

  /// <summary>
  /// DN: Hello, World; `std:cout <<`
  /// Prints "Hello World! 123" to the standard output.
  /// </summary>
  static std::string HelloWorld();

  /// <summary>
  /// DN: `std::endl` as arg
  /// Inserts a newline and flushes the output buffer.
  /// Used as an argument to the output stream operator.
  /// </summary>
  static std::string StdEndl_AsArg();

  /// <summary>
  /// DN: `std::endl` as function
  /// Inserts a newline and flushes the output buffer.
  /// Used as a function taking the output stream as an argument.
  /// </summary>
  static std::string StdEndl_AsFunc();

  /// <summary>
  /// DN: `std::ends`
  /// Inserts a null character ('\0') into the output stream.
  /// </summary>
  static std::string StdEnds();

  /// <summary>
  /// DN: `std::cin.get`
  /// Waits and reads the first character from the standard input.
  /// </summary>
  static std::string CinGet();

  /// <summary>
  /// DN: `std::getline`
  /// Waits and reads the typed line without '\n' from the standard input.
  /// </summary>
  static std::string StdGetLine();

  /// <summary>
  /// DN: `std::cin >>`
  /// Waits and writes the next sequence of printable characters into a variable.
  /// </summary>
  static std::string CinNextWordToVar();

  /// <summary>
  /// DN: string constructors
  /// </summary>
  static std::string StringConstructors();

  /// <summary>
  /// DN: string item accessors: `[]`, `at()`
  /// </summary>
  static std::string StringItemAccessors();

  /// <summary>
  /// DN: `#pragma once`
  /// </summary>
  static std::string PragmaOnce();

  /// <summary>
  /// DN: Get type name
  /// </summary>
  static std::string GetTypeName();

  /// <summary>
  /// DN: Pointer
  /// </summary>
  static std::string Pointer();

  /// <summary>
  /// DN: Pointer arg
  /// </summary>
  static std::string PointerArg();

  /// <summary>
  /// DN: `nullptr`
  /// </summary>
  static std::string Nullptr();

  /// <summary>
  /// DN: Operator `->`
  /// </summary>
  static std::string OpArrow();
};
