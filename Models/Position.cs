using System.Text.RegularExpressions;
using System.Globalization;

namespace EPubBlazor.Models;

public record Position(int[] NavigationItemIndex, decimal ScrollPosition)
{
    public decimal ScrollPosition {get;set;} = ScrollPosition;

    public static Position Parse(string input)
    {
        var match = Regex.Match(input, @"^\((?:,?([0-9]+))+\|(.*)\)$");
        if (match != null && match.Success && match.Groups != null && match.Groups.Count == 3)
        {
            var idx = match.Groups[1].Captures.Select(c => { int.TryParse(c.Value, CultureInfo.InvariantCulture, out var id); return id; }).ToArray();
            decimal.TryParse(match.Groups[2].Value, CultureInfo.InvariantCulture, out var pos);
            return new Position(idx, pos);
        }

        return default!;
    }

    public string ToQueryString()
    {

        return $"({String.Join(',', this.NavigationItemIndex)}|{ScrollPosition.ToString("0.00", CultureInfo.InvariantCulture)})";
    }
}