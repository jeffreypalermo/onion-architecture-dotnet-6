using ProgrammingWithPalermo.ChurchBulletin.UI.Server;
var builder = WebApplication.CreateBuilder(args);
var startup = new UIStartup();
startup.ConfigureBuilder(builder);

var app = builder.Build();
startup.ConfigureApp(app);
app.Run();