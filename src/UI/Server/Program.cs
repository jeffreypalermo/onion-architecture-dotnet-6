using System.Diagnostics;
using System.Reflection;
using Lamar;
using Lamar.Microsoft.DependencyInjection;
using ProgrammingWithPalermo.ChurchBulletin.UI.Server;

string startupDll = "ProgrammingWithPalermo.ChurchBulletin.UI.Startup.dll";
var currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"bin\Debug\net6.0\");
Console.WriteLine(currentDirectory);
var fullPath = Path.Combine(currentDirectory, startupDll);
Debug.Assert(File.Exists(fullPath));
var assemblyName = AssemblyName.GetAssemblyName(fullPath);
PluginLoadContext loadContext = new PluginLoadContext(fullPath);
Assembly assembly = loadContext.LoadFromAssemblyName(assemblyName);


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLamar(registry =>
{
    registry.IncludeRegistry(new UIServiceRegistry(assembly));
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



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