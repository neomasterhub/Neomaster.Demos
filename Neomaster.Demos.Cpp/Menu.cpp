#include "Menu.h"
#include "Fundamentals.h"
#include "Helpers.h"

Menu::Menu()
{
  _items =
  {
    MenuItem("1. Hello, World", []() { Fundamentals::HelloWorld(); }),
    MenuItem("2. ", []() {}),
    MenuItem("3. ", []() {}),
  };

  _selectedY = -1;
  _itemCount = _items.size();
  _curYMin = 0;
  _curYMax = _curYMin + _itemCount - 1;
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

    case 13:
      _selectedY = _curY;
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

  for (size_t i = 0; i < _itemCount; i++)
  {
    if (i == _curY)
    {
      std::cout << "👉 ";
    }
    else
    {
      std::cout << "   ";
    }

    std::cout << _items[i].Text;

    if (i == _selectedY)
    {
      std::cout << " 👀";
    }

    std::cout << "\n";
  }
}
