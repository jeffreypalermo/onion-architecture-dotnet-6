using Lamar.Microsoft.DependencyInjection;
using ProgrammingWithPalermo.ChurchBulletin.Core.Queries;
using ProgrammingWithPalermo.ChurchBulletin.UI.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLamar(registry => { registry.IncludeRegistry(new UIServiceRegistry()); });
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = StartupCheck(builder);
//Configure the HTTP request pipeline.
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

WebApplication StartupCheck(WebApplicationBuilder webApplicationBuilder)
{
    var webApplication = webApplicationBuilder.Build();
    var items = webApplication.Services.CreateScope().ServiceProvider
        .GetRequiredService<IChurchBulletinItemByDateHandler>().Handle(
            new ChurchBulletinItemByDateAndTimeQuery(new DateTime(2000, 1, 1)));
    Console.WriteLine(items.Count());
    webApplication.Logger.LogInformation("starting the app");
    return webApplication;
}