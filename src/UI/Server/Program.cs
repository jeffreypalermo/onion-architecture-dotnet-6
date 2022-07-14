using Lamar.Microsoft.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
builder.Host.UseLamar((context, registry) =>
{
    registry.Scan(scanner =>
    {
        scanner.AssembliesFromApplicationBaseDirectory();
        scanner.LookForRegistries();
    });
});

var app = builder.Build();

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