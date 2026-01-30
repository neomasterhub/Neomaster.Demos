#pragma once
#include <iostream>
#include <functional>
class MenuItem
{
public:
  std::string Text;
  std::function<std::string()> Action;

  MenuItem(
    std::string text,
    std::function<std::string()> action)
  {
    Text = text;
    Action = action;
  }
};
