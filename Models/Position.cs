using System.Text.RegularExpressions;
using System.Globalization;

namespace EPubBlazor.Models;

public record Position(string NavigationLevel, int ReadOrder, decimal ScrollPosition)
{
    public string NavigationLevel {get;set;} = NavigationLevel;
    public int ReadOrder {get;set;} = ReadOrder;
    public decimal ScrollPosition {get;set;} = ScrollPosition;
 

    public static Position Parse(string input)
    {
        var match = Regex.Match(input, @"^\(((?:[0-9]+,?)+)\|([0-9]+)\|(.*)\)$");
        if (match != null && match.Success && match.Groups != null && match.Groups.Count == 4)
        {
            var nav = match.Groups[1].Value;
            int.TryParse(match.Groups[2].Value, CultureInfo.InvariantCulture, out var rOrder);
            decimal.TryParse(match.Groups[3].Value, CultureInfo.InvariantCulture, out var pos);
            return new Position(nav, rOrder, pos);
        }

        return new Position("0",0,0m);
    }

    public string ToQueryString()
    {

        return $"({NavigationLevel}|{ReadOrder}|{ScrollPosition.ToString("0.00", CultureInfo.InvariantCulture)})";
    }
}