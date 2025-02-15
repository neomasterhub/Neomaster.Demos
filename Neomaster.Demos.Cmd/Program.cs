using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Neomaster.Demos.Cmd;

var builder = CoconaApp.CreateBuilder();
var configuration = builder.Configuration;
builder.Services.AddSingleton<Menu>();

var app = builder.Build();
app.AddCommand((Menu menu) => menu.Show());
app.Run();
