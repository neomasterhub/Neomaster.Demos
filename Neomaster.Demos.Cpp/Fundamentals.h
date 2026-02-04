#pragma once
#include <iostream>
class Fundamentals
{
public:

  /// <summary>
  /// Prints "Hello World! 123" to the standard output.
  /// </summary>
  static std::string HelloWorld();

  /// <summary>
  /// Inserts a newline and flushes the output buffer.
  /// Used as an argument to the output stream operator.
  /// </summary>
  static std::string StdEndl_AsArg();

  /// <summary>
  /// Inserts a newline and flushes the output buffer.
  /// Used as a function taking the output stream as an argument.
  /// </summary>
  static std::string StdEndl_AsFunc();

  /// <summary>
  /// Inserts a null character ('\0') into the output stream.
  /// </summary>
  static std::string StdEnds();

  /// <summary>
  /// Waits and reads the first character from the standard input.
  /// </summary>
  static std::string CinGet();

  /// <summary>
  /// Waits and reads the typed line without '\n' from the standard input.
  /// </summary>
  static std::string StdGetLine();

  /// <summary>
  /// Waits and writes the next sequence of printable characters into a variable.
  /// </summary>
  static std::string CinNextWordToVar();

  static std::string StringConstructors();

  static std::string StringItemAccessors();

  static std::string PragmaOnce();

  static std::string GetTypeName();

  static std::string Pointer();
};
