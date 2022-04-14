using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using static Blazor.Extensions.Canvas2dContextConstants;

namespace Blazor.Extensions
{
    internal class CanvasAsyncProperty<TNet, TJs> : IAsyncProperty<TNet>
    {
        private readonly ElementReference _canvas;
        private readonly string _propertyName;
        private readonly Func<TNet, TJs> _toJs;
        private readonly Func<TJs, TNet> _toNet;

        [Inject] public IJSRuntime JSRuntime { get; set; }

        internal CanvasAsyncProperty(ElementReference canvas, string propertyName, Func<TNet, TJs> toJs, Func<TJs, TNet> toNet)
        {
            this._canvas = canvas;
            this._propertyName = propertyName;
            this._toJs = toJs;
            this._toNet = toNet;
        }

        public async ValueTask<TNet> GetAsync() => this._toNet(await this.JSRuntime.InvokeAsync<TJs>(GET_CANVAS_PROPERTY_ACTION, this._canvas, this._propertyName));

        public ValueTask SetAsync(TNet value) => this.JSRuntime.InvokeVoidAsync(SET_CANVAS_PROPERTY_ACTION, this._canvas, this._propertyName, this._toJs(value));
        }
}
