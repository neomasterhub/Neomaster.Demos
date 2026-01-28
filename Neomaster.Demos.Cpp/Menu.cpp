#include "Menu.h"
#include "Fundamentals.h"

Menu::Menu()
{
  _items =
  {
    MenuItem("1. Hello, World", []() { Fundamentals::HelloWorld(); }),
  };

  _curYMin = 8;
  _curYMax = _curYMin + _items.size() - 1;
  _curY = _curYMin;
}

void Menu::ShowCommands()
{
  // Clear screen
  std::cout << "\033[2J\033[H";
}
