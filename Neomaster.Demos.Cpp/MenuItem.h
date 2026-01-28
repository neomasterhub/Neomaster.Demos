#pragma once
#include <iostream>
#include <functional>
class MenuItem
{
public:
  std::string Text;
  std::function<void()> Action;

  MenuItem(
    std::string text,
    std::function<void()> action)
  {
    Text = text;
    Action = action;
  }
};
