
using System.Runtime.CompilerServices;
using MudBlazor;
using MudBlazor.Utilities;

namespace EPubBlazor.Components;

public static class EPubBlazorTheme
{
    public static MudTheme Light = new MudTheme()
    {
        Palette = new PaletteLight()
        {
            AppbarText = Colors.Grey.Default,
            AppbarBackground = Colors.Grey.Lighten5,
            PrimaryContrastText = Colors.Grey.Default, // --mud-palette-primary-text
            ActionDefault = Colors.Grey.Default,

            Primary = Colors.Grey.Lighten5,
            TextPrimary = Colors.Grey.Darken4,

            Background = Colors.Shades.White,
            BackgroundGrey = Colors.Grey.Darken1,

            DrawerBackground = Colors.Grey.Darken3,
            DrawerText = Colors.Grey.Lighten5,

        }
    };

    public static MudTheme Dark = new MudTheme()
    {
        Palette = new PaletteLight()
        {
            AppbarText = Colors.Grey.Darken3,
            AppbarBackground = Colors.Shades.Black,
            PrimaryContrastText = Colors.Grey.Darken3, // --mud-palette-primary-text
            ActionDefault = Colors.Grey.Darken3,

            Primary = Colors.Grey.Darken2,
            TextPrimary = Colors.Grey.Darken2,

            Background = Colors.Grey.Darken4,
            BackgroundGrey = Colors.Grey.Darken4,

            DrawerBackground = Colors.Shades.Black,
            DrawerText = Colors.Grey.Darken3,

        }
    };

    public static MudTheme Sepia = new MudTheme()
    {
        Palette = new PaletteLight()
        {
            AppbarText = Colors.Amber.Accent4,
            AppbarBackground = Colors.Amber.Lighten4,
            PrimaryContrastText = Colors.Amber.Accent4, // --mud-palette-primary-text
            ActionDefault = Colors.Amber.Accent4,

            Primary = Colors.Brown.Darken4,
            TextPrimary = Colors.Brown.Darken4,

            Background = new MudColor("#ebe1cb"), // Colors.Amber.Lighten4,
            BackgroundGrey = Colors.Grey.Darken1,

            DrawerBackground = Colors.BlueGrey.Darken4,
            DrawerText = Colors.BlueGrey.Lighten5,

        }
    };

    public static MudTheme HighContrast = new MudTheme()
    {
        Palette = new PaletteLight()
        {
            AppbarText = Colors.Shades.White,
            AppbarBackground = Colors.Shades.Black,
            PrimaryContrastText = Colors.Shades.White, // --mud-palette-primary-text
            ActionDefault = Colors.Shades.White,

            Primary = Colors.Grey.Darken2,
            TextPrimary = Colors.Shades.White,

            Background = Colors.Shades.Black,
            BackgroundGrey = Colors.Grey.Darken4,

            DrawerBackground = Colors.Shades.Black,
            DrawerText = Colors.Shades.White,



        }
    };

    public static MudTheme Retro = new MudTheme()
    {
        Palette = new PaletteLight()
        {
            AppbarText = Colors.LightGreen.Accent3,
            AppbarBackground = Colors.Shades.Black,
            PrimaryContrastText = Colors.LightGreen.Accent3, // --mud-palette-primary-text
            ActionDefault = Colors.Green.Accent3,

            Primary = Colors.Lime.Accent4,
            TextPrimary = Colors.Lime.Accent4,

            Background = Colors.Grey.Darken4,
            BackgroundGrey = Colors.Grey.Darken4,

            DrawerBackground = Colors.Shades.Black,
            DrawerText = Colors.LightGreen.Accent3,

        }
    };




    public static MudTheme GetDefaultTheme()
    {
        var theme = EPubBlazorTheme.Light;
        return theme;
    }

}