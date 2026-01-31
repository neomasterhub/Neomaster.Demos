#include "Menu.h"
#include "Helpers.h"
#include <functional>

Menu::Menu(std::vector<MenuItem> items)
{
  _items = items;
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

    case 32:
      system("cls");
      break;
    }

    ShowCommands();
    key = Helpers::GetKey();
  }
}

void Menu::ShowCommands()
{
  system("cls");

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

    std::cout << i + 1 << ". " << _items[i].Text;

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
