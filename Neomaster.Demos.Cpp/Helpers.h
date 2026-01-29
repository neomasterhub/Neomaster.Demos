#pragma once
#include <conio.h>
class Helpers
{
public:
  static int GetKey()
  {
    int key = _getch();

    if (key == 0 || key == 224)
    {
      key = _getch();
    }

    return key;
  }
};
