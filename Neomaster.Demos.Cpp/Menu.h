#pragma once
#include "MenuItem.h"
#include <vector>
class Menu
{
private:
  int _curY;
  int _curYMin;
  int _curYMax;
  std::vector<MenuItem> _items;
public:
  Menu();
};
