#include "Menu.h"
#include "Fundamentals.h"
#include "Helpers.h"

Menu::Menu()
{
  _items =
  {
    MenuItem("1. Hello, World", []() { Fundamentals::HelloWorld(); }),
    MenuItem("2. ", []() { }),
    MenuItem("3. ", []() { }),
  };

  _curYMin = 8;
  _curYMax = _curYMin + _items.size() - 1;
  _curY = _curYMin;
}

void Menu::Show()
{
  ShowCommands();
  int key = Helpers::GetKey();

  while (key != 27)
  {
    switch (key)
    {
    case 72:
      if (_curY > _curYMin)
      {
        _curY--;
      }
      else
      {
        _curY = _curYMax;
      }
      break;

    case 80:
      if (_curY < _curYMax)
      {
        _curY++;
      }
      else
      {
        _curY = _curYMin;
      }
      break;
    }

    ShowCommands();
    key = Helpers::GetKey();
  }
}

void Menu::ShowCommands()
{
  // Clear screen
  std::cout << "\033[2J\033[H";

  std::cout << _curY;
}
