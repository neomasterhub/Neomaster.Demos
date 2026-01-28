#pragma once
class Fundamentals
{
public:

  /// <summary>
  /// Prints "Hello World! 123" to the standard output.
  /// </summary>
  static void HelloWorld();

  /// <summary>
  /// Inserts a newline and flushes the output buffer.
  /// Used as an argument to the output stream operator.
  /// </summary>
  static void StdEndl_AsArg();

  /// <summary>
  /// Inserts a newline and flushes the output buffer.
  /// Used as a function taking the output stream as an argument.
  /// </summary>
  static void StdEndl_AsFunc();

  /// <summary>
  /// Inserts a null character ('\0') into the output stream.
  /// </summary>
  static void StdEnds();

  /// <summary>
  /// Waits and reads the first character from the standard input.
  /// </summary>
  static void CinGet();

  /// <summary>
  /// Waits and reads the typed line without '\n' from the standard input.
  /// </summary>
  static void StdGetLine();

  /// <summary>
  /// Waits and writes the next sequence of printable characters into a variable.
  /// </summary>
  static void CinToVar();
};
