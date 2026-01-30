#include "Menu.h"
#include "Fundamentals.h"
#include "Helpers.h"
#include <functional>

Menu::Menu()
{
  _items =
  {
    MenuItem("1. Hello, World", []() { return Fundamentals::HelloWorld(); }),
    MenuItem("2. ", []() { return "1"; }),
    MenuItem("3. ", []() { return "2"; }),
  };

  _runDemo = false;
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
      _runDemo = true;
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

  int demoIndex = -1;

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
      demoIndex = i;

      if (_runDemo || !_demoResult.empty())
      {
        std::cout << " 👀";
      }
    }

    std::cout << "\n";
  }

  std::cout << "\n";

  if (_runDemo)
  {
    _runDemo = false;
    _demoResult = _items[demoIndex].Action();
  }

  std::cout << _demoResult << std::endl;
}
