using Microsoft.EntityFrameworkCore;
using ProgrammingWithPalermo.ChurchBulletin.Core.Queries;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Handlers;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;
using ProgrammingWithPalermo.ChurchBulletin.UI.Server;

var fullPath = Path.GetFullPath("../Server");
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    ContentRootPath = fullPath
});

var startup = new UIStartup();
startup.ConfigureBuilder(builder);
builder.Services.AddTransient<IChurchBulletinItemByDateHandler, ChurchBulletinItemByDateHandler>();
builder.Services.AddScoped<DbContext, DataContext>();
builder.Services.AddDbContextFactory<DataContext>();
builder.Services.AddDbContextFactory<DbContext>();

var app = builder.Build();
app.Logger.LogInformation($"starting from {fullPath}");
startup.ConfigureApp(app);
app.Run();