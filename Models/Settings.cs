namespace EPubBlazor.Models;

public record Settings{

    public string SelectedTheme {get;set;} = "Light";

    public int FontSize {get;set;} = 16;

}