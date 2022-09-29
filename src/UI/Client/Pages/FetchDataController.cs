using System.Diagnostics;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Palermo.BlazorMvc;
using ProgrammingWithPalermo.ChurchBulletin.Core.Model;
using static System.Net.WebRequestMethods;

namespace UI.Client.Pages;

[Route("/fetchdata")]
public class FetchDataController : ControllerComponentBase<FetchDataView>
{
    private WeatherForecast[]? _forecasts;
    [Inject] public HttpClient? Http { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Debug.Assert(Http != null, nameof(Http) + " != null");
        _forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
        View.Model = _forecasts;
    }
}