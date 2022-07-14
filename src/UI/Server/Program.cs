using ProgrammingWithPalermo.ChurchBulletin.UI.Server;

var fullPath = Path.GetFullPath("../Server");
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    ContentRootPath = fullPath
});

var startup = new UIStartup();
startup.ConfigureBuilder(builder);


var app = builder.Build();
app.Logger.LogInformation($"starting from {fullPath}");
startup.ConfigureApp(app);
app.Run();