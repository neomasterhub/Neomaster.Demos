#include "Menu.h"
#include <windows.h>

void main()
{
  SetConsoleOutputCP(CP_UTF8);

  Menu menu = Menu();
  menu.Show();
}
