using System;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Blazor.Extensions.Canvas.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Blazor.Extensions.Canvas.Components;

public partial class BECanvas : ComponentBase
{

    [Inject]
    internal IJSRuntime? JSRuntime { get; set; }

    public readonly string Id = Guid.NewGuid().ToString();

    /// <summary>
    ///     Null until after render when we initialize it from the beCanvas reference
    /// </summary>
    private Canvas2DContext? _canvas2DContext;

    protected ElementReference _canvasRef;

    [Parameter] public long Height { get; set; }

    [Parameter] public long Width { get; set; }

    public ElementReference CanvasReference => this._canvasRef;

    public bool CanvasInitialized => this._canvas2DContext is { };

    [Parameter] public EventCallback OnLoaded { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            this._canvas2DContext = await this.CreateCanvas2DAsync();

            await this.OnLoaded.InvokeAsync(this._canvas2DContext);

        }

        await base.OnAfterRenderAsync(firstRender: firstRender);
    }

    public async Task<object?> DrawBufferToPng()
    {
        if (!this.CanvasInitialized) return null;
        return await this.JSRuntime!.InvokeAsync<object>(identifier: "canvasToPng");
    }
}