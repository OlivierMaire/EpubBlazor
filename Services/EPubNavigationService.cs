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

    public event Action<Position>? PositionChanged;

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
        PositionChanged?.Invoke(position);
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
        pos.NavigationLevel = PageToLevelDictionary.ContainsKey(pos.ReadOrder) ? PageToLevelDictionary[pos.ReadOrder] : string.Empty;
        pos.ScrollPosition = 0;
        SetPosition(pos);

    }
    public void GoToPrevious()
    {
        var pos = GetPosition();
        // pos.NavigationItemIndex[pos.NavigationItemIndex.Length - 1] -= 1;
        pos.ReadOrder -= 1;
        pos.NavigationLevel = PageToLevelDictionary.ContainsKey(pos.ReadOrder) ? PageToLevelDictionary[pos.ReadOrder] : string.Empty;
        pos.ScrollPosition = -1;
        SetPosition(pos);

    }

    public void GenerateNavigationTree(EpubBook book)
    {
        if (book.Navigation != null)
            NavigationTree = GenerateNavigationTree(book, book.Navigation, string.Empty);
        PageToLevelDictionary = NavigationTree.SelectMany(n => n.Page, (n, p) =>
            new { PageId = p, Level = n.Level }).ToDictionary(d => d.PageId, d => d.Level);
    }

    private List<ChapterReadingOrderItem> GenerateNavigationTree(EpubBook book, List<EpubNavigationItem> navigation, string level)
    {
        var list = new List<ChapterReadingOrderItem>();

        for (int i = 0; i < navigation.Count; i++)
        {
            var nextLevel = string.IsNullOrEmpty(level) ? i.ToString() : $"{level},{i}";

            var nav1 = navigation[i];

            list.Add(new ChapterReadingOrderItem(nextLevel));

            if (nav1.NestedItems != null && nav1.NestedItems.Count > 0)
                list.AddRange(GenerateNavigationTree(book, nav1.NestedItems, nextLevel));
        }

        int pageCount = 0;

        for (int i = 0; i < list.Count; i++)
        {
            list[i].Page = GenerateNavigationPages(list[i].Level, i + 1 < list.Count ? list[i + 1].Level : null, book, pageCount);
        }

        return list;

    }

    private int[] GenerateNavigationPages(string fromLevel, string? toLevel, EpubBook book, int pageCount)
    {
        var fromNav = GetNavigationItem(fromLevel, book);
        var toNav = GetNavigationItem(toLevel, book);


        List<int> roList = [];
        bool chapterFound = false;
        for (; pageCount < book.ReadingOrder.Count; pageCount++)
        {

            if (book.ReadingOrder[pageCount].FilePath == fromNav?.Link?.ContentFilePath)
                chapterFound = true;

            if (toNav != null && book.ReadingOrder[pageCount].FilePath == toNav.Link?.ContentFilePath)
                break;

            if (chapterFound)
                roList.Add(pageCount);
        }

        return roList.ToArray();

    }

    private EpubNavigationItem? GetNavigationItem(string? level, EpubBook book)
    {
        if (string.IsNullOrEmpty(level)) return null;

        var levels = level.Split(',');
        var nav = book.Navigation[int.Parse(levels[0])];
        for (int l = 1; l < levels.Length; l++)
        {
            nav = nav.NestedItems[int.Parse(levels[l])];
        }
        return nav;
    }

    public record ChapterReadingOrderItem(string Level)
    {
        public int[] Page { get; internal set; } = [];
    }

}