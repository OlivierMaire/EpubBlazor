using EPubBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace EPubBlazor.Services;

public class EPubNavigationService(NavigationManager navigationManager)
{
    private readonly NavigationManager navigationManager = navigationManager;

    public event EventHandler<Position> PositionChanged;

    public Position GetPosition()
    {
        var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

        QueryHelpers.ParseQuery(uri.Query).TryGetValue("p", out var fragment);
        return Position.Parse(fragment.FirstOrDefault() ?? string.Empty);
    }

    public void SetPosition(Position position)
{
    var baseUri = navigationManager.ToAbsoluteUri(navigationManager.Uri).GetLeftPart(UriPartial.Path);
    baseUri += $"?p={position.ToQueryString()}";
    navigationManager.NavigateTo(baseUri);
    PositionChanged.Invoke(null,position);
}

public void GoToNext()
{
    var pos = GetPosition();
    pos.NavigationItemIndex[pos.NavigationItemIndex.Length-1] +=1;
    pos.ScrollPosition = 0;
   SetPosition(pos);
   
}
public void GoToPrevious()
{
    var pos = GetPosition();
    pos.NavigationItemIndex[pos.NavigationItemIndex.Length-1] -=1;
    pos.ScrollPosition = -1;
   SetPosition(pos);
   
}
}