using System.Linq;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Blazor.Extensions.Canvas.Test.ServerSide.Pages
{
    public class IndexComponent : ComponentBase
    {
        private Canvas2DContext _context;

        protected BECanvasComponent _canvasReference;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            this._context = await this._canvasReference.CreateCanvas2DAsync();
            await this._context.SetFillStyleAsync("green");

            await this._context.FillRectAsync(10, 100, 100, 100);

            await this._context.SetFontAsync("48px serif");
            await this._context.StrokeTextAsync("Hello Blazor!!!", 10, 100);

            var imageData = await this._context.GetImageDataAsync(10, 100, 75, 75);
            var data = imageData.Data.ToArray();
            for (var i = 0; i < data.Length; i += 4)
            {
                data[i] = (byte)(255 - data[i]);     // red
                data[i + 1] = (byte)(255 - data[i + 1]); // green
                data[i + 2] = (byte)(255 - data[i + 2]); // blue
            }
            imageData.Data = data;
            await this._context.PutImageDataAsync(imageData, 10, 100);
        }
    }
}
