#pragma once
#include "MenuItem.h"
#include <vector>
class Menu
{
private:

  int _curY;
  int _curYMin;
  int _curYMax;
  int _itemCount;
  int _selectedY;
  bool _runDemo;

  std::vector<MenuItem> _items;

  void ShowCommands();

public:

  Menu();
  void Show();
};
