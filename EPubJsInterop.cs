using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EPubBlazor;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class EPubJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public EPubJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/EPubBlazor/EPubJsInterop.js").AsTask());
    }

    // public async ValueTask<string> Prompt(string message)
    // {
    //     var module = await moduleTask.Value;
    //     return await module.InvokeAsync<string>("showPrompt", message);
    // }

    public async ValueTask InitAsync(ElementReference elem1,ElementReference elem2)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("epubInterop.setElems", elem1, elem2);
    }
    public async ValueTask<System.Drawing.Size> GetIframeSizeAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<System.Drawing.Size>("epubInterop.getIFrameSize");
    }

    public async ValueTask<int> GetScrollWidth()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<int>("epubInterop.getScrollWidth");
    }

    public async ValueTask ScrollTo(decimal x)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("epubInterop.scrollTo", x);

    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
