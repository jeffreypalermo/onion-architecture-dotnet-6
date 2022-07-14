using Lamar.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProgrammingWithPalermo.ChurchBulletin.Core;
using ProgrammingWithPalermo.ChurchBulletin.Core.Queries;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Handlers;
using ProgrammingWithPalermo.ChurchBulletin.DataAccess.Mappings;
using ProgrammingWithPalermo.ChurchBulletin.UI.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLamar((context, registry) =>
{
    registry.Scan(scanner =>
    {
        scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(assembly => 
            assembly.FullName.Contains("palermo", StringComparison.InvariantCultureIgnoreCase));
        scanner.TheCallingAssembly();
        scanner.AssemblyContainingType<UI.Client.App>();
        scanner.LookForRegistries();
    });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<IDatabaseConfiguration, DatabaseConfiguration>();
builder.Services.AddTransient<IChurchBulletinItemByDateHandler, ChurchBulletinItemByDateHandler>();
builder.Services.AddScoped<DbContext, DataContext>();
builder.Services.AddDbContextFactory<DataContext>();
builder.Services.AddDbContextFactory<DbContext>();


var app = builder.Build();
app.Logger.LogInformation($"starting the app");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});
app.Run();