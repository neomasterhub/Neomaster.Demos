using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Neomaster.Demos.Cmd.Menus;

var builder = CoconaApp.CreateBuilder();
var configuration = builder.Configuration;
builder.Services.AddSingleton<MenuMain>();
builder.Services.AddSingleton<MenuRabbitMQ>();

var app = builder.Build();
app.AddCommand((MenuMain menu) => menu.Show());
app.Run();
