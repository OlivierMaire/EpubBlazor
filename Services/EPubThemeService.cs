using System.Collections.ObjectModel;
using EPubBlazor.Components;
using EPubBlazor.Models;
using MudBlazor;

namespace EPubBlazor.Services;

public class EPubThemeService
{

    private readonly ReadOnlyDictionary<string, MudTheme> Themes = new(
                        new Dictionary<string, MudTheme> {
                            { "Light", EPubBlazorTheme.Light },
                            { "Dark", EPubBlazorTheme.Dark },
                            { "High Contrast", EPubBlazorTheme.HighContrast },
                            { "Sepia", EPubBlazorTheme.Sepia },
                            { "Retro", EPubBlazorTheme.Retro },
                            });

    public List<ThemeDisplay> GetAvailableThemes()
    {

        var themes = Themes.Select(t => new ThemeDisplay
        {

            Name = t.Key,
            BgColor = t.Value.Palette.Background.Value,
            Color = t.Value.Palette.TextPrimary.Value
        }).ToList();

        return themes;
    }

    public MudTheme GetTheme(string name) => Themes[name];
    
}