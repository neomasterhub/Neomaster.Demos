#include "Menu.h"
#include "Fundamentals.h"
#include "Helpers.h"

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

void Menu::Show()
{
  ShowCommands();
  int key = Helpers::GetKey();

  while (key != 27)
  {
    switch (key)
    {
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
