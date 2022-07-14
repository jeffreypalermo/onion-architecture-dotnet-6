// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();
//
// app.MapGet("/", () => "Hello World!");
//
// app.Run();


using ProgrammingWithPalermo.ChurchBulletin.UI.Server;

var fullPath = Path.GetFullPath("../Server");
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    ContentRootPath = fullPath
});

var startup = new UIStartup();
startup.ConfigureBuilder(builder);

var app = builder.Build();
startup.ConfigureApp(app);
app.Run();