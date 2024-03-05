using EPubBlazor.Models;

namespace EPubBlazor.Services;

public class EPubSettingsService
{

    private Settings Settings { get; set; } = new();

    public event EventHandler<Settings> SettingsChanged;


    public Settings GetSettings()
    {

        return Settings;
    }

    public void SetSettings(Settings settings)
    {
        Settings = settings;
        SettingsChanged.Invoke(null, Settings);
    }

}