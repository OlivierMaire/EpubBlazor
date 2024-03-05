using EPubBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using VersOne.Epub;
using System.Runtime.InteropServices;

namespace EPubBlazor.Services;

public class EPubNavigationService(NavigationManager navigationManager)
{
    private readonly NavigationManager navigationManager = navigationManager;

    public event EventHandler<Position> PositionChanged;

    List<ChapterReadingOrderItem> NavigationTree { get; set; } = [];
    Dictionary<int, string> PageToLevelDictionary = [];


    public Position GetPosition()
    {
        var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

        QueryHelpers.ParseQuery(uri.Query).TryGetValue("p", out var fragment);
        return Position.Parse(fragment.FirstOrDefault() ?? string.Empty);
    }

    public void SetPosition(Position position)
    {
        string uri = GetUrlForPosition(position);
        navigationManager.NavigateTo(uri);
        PositionChanged.Invoke(null, position);
    }

    public string GetUrlForPosition(Position position)
    {
        var baseUri = navigationManager.ToAbsoluteUri(navigationManager.Uri).GetLeftPart(UriPartial.Path);
        return baseUri + $"?p={position.ToQueryString()}";
    }

    public void GoToNext()
    {
        var pos = GetPosition();
        // pos.NavigationItemIndex[pos.NavigationItemIndex.Length - 1] += 1;
        pos.ReadOrder += 1;
        pos.NavigationLevel = PageToLevelDictionary.ContainsKey(pos.ReadOrder)?  PageToLevelDictionary[pos.ReadOrder] : string.Empty;
        pos.ScrollPosition = 0;
        SetPosition(pos);

    }
    public void GoToPrevious()
    {
        var pos = GetPosition();
        // pos.NavigationItemIndex[pos.NavigationItemIndex.Length - 1] -= 1;
        pos.ReadOrder -= 1;
        pos.NavigationLevel = PageToLevelDictionary.ContainsKey(pos.ReadOrder)?  PageToLevelDictionary[pos.ReadOrder] : string.Empty;
        pos.ScrollPosition = -1;
        SetPosition(pos);

    }

    public void GenerateNavigationTree(EpubBook book)
    {
        NavigationTree = GenerateNavigationTree(book, book.Navigation, string.Empty);
        PageToLevelDictionary = NavigationTree.SelectMany(n => n.Page, (n, p) =>
 new { PageId = p, Level = n.Level })
 .ToDictionary(d => d.PageId, d => d.Level);
        // PageToLevelDictionary = NavigationTree.SelectMany(n => n.Page, (n, p) =>
        //  new { PageId = p, Level = n.Level.Split(",").Select(i => int.Parse(i)).ToArray() })
        //  .ToDictionary(d => d.PageId, d => d.Level);
    }

    private List<ChapterReadingOrderItem> GenerateNavigationTree(EpubBook book, List<EpubNavigationItem> navigation, string level)
    {
        var list = new List<ChapterReadingOrderItem>();

        int j = 0;


        for (int i = 0; i < navigation.Count; i++)
        {
            var nextLevel = string.IsNullOrEmpty(level) ? i.ToString() : $"{level},{i}";

            var nav1 = navigation[i];

            var nav2 = i + 1 < navigation.Count ? navigation[i + 1] : null;

            List<ChapterReadingOrderItem>? nested = null;

            if (nav1.NestedItems != null && nav1.NestedItems.Count > 0)
                list.AddRange( GenerateNavigationTree(book, nav1.NestedItems, nextLevel) );

            List<int> roList = new();
            bool chapterFound = false;
            for (; j < book.ReadingOrder.Count; j++)
            // foreach (var ro in book.ReadingOrder)
            {

                if (book.ReadingOrder[j].FilePath == nav1.Link?.ContentFilePath)
                    chapterFound = true;

                if (nav2 != null && book.ReadingOrder[j].FilePath == nav2.Link?.ContentFilePath)
                    break;

                if (chapterFound)
                    roList.Add(j);
            }

            list.Add(new ChapterReadingOrderItem(nextLevel, roList.ToArray()));
        }

        return list;

    }

    public record ChapterReadingOrderItem(string Level, int[] Page);

}