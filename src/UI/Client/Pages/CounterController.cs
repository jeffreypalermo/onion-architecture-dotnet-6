using Microsoft.AspNetCore.Components;
using Palermo.BlazorMvc;

namespace UI.Client.Pages;

[Route("/counter")]
public class CounterController : ControllerComponentBase<CounterView>
{
    private int _currentCount;

    protected override void OnViewInitialized()
    {
        View.Model = _currentCount;
        View.OnIncrement = IncrementCount;
    }

    private void IncrementCount()
    {
        _currentCount++;
        View.Model = _currentCount;
    }
}