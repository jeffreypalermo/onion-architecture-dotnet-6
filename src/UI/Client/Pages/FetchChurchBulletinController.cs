using System.Diagnostics;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Palermo.BlazorMvc;
using ProgrammingWithPalermo.ChurchBulletin.Core.Model;

namespace UI.Client.Pages;

[Route("/fetchchurchbulletin")]
public class FetchChurchBulletinController : ControllerComponentBase<FetchChurchBulletinView>
{
    private ChurchBulletinItem[]? _bulletins;
    [Inject] public HttpClient? Http { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Debug.Assert(Http != null, nameof(Http) + " != null");
        _bulletins = await Http.GetFromJsonAsync<ChurchBulletinItem[]>("ChurchBulletinItem");
        View.Model = _bulletins;
    }
}