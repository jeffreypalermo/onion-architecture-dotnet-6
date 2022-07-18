using System.Diagnostics;
using System.Reflection;
using Lamar.Microsoft.DependencyInjection;
using ProgrammingWithPalermo.ChurchBulletin.UI.Server;

var assembly = GetStartupAssembly();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLamar(registry => { registry.IncludeRegistry(new UIServiceRegistry(assembly)); });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();
// app.Logger.LogInformation($"starting the app");
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

Assembly GetStartupAssembly()
{
    var startupDll = "ProgrammingWithPalermo.ChurchBulletin.UI.Startup.dll";
    var currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"bin\Debug\net6.0\");
    Console.WriteLine(currentDirectory);
    var fullPath = Path.Combine(currentDirectory, startupDll);
    Debug.Assert(File.Exists(fullPath));
    var assemblyName = AssemblyName.GetAssemblyName(fullPath);
    var loadContext = new PluginLoadContext(fullPath);
    var assembly1 = loadContext.LoadFromAssemblyName(assemblyName);
    return assembly1;
}