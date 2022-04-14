using Blazor.Extensions.Canvas.Canvas2D;
using Blazor.Extensions.Canvas.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using static Blazor.Extensions.Canvas2dContextConstants;

namespace Blazor.Extensions
{
    public class Canvas2dContextAsync : IDisposable
    {
        #region Properties

        public IAsyncProperty<string> FillStyle => this.CreateAsyncProperty<string>(FILL_STYLE_PROPERTY);

        public IAsyncProperty<string> StrokeStyle => this.CreateAsyncProperty<string>(STROKE_STYLE_PROPERTY);

        public IAsyncProperty<string> Font => this.CreateAsyncProperty<string>(FONT_PROPERTY);

        public IAsyncProperty<TextAlign> TextAlign => this.CreateEnumAsyncProperty<TextAlign>(TEXT_ALIGN_PROPERTY);

        public IAsyncProperty<TextDirection> Direction => this.CreateEnumAsyncProperty<TextDirection>(DIRECTION_PROPERTY);

        public IAsyncProperty<TextBaseline> TextBaseline => this.CreateEnumAsyncProperty<TextBaseline>(TEXT_BASELINE_PROPERTY);

        public IAsyncProperty<float> LineWidth => this.CreateAsyncProperty<float>(LINE_WIDTH_PROPERTY);

        public IAsyncProperty<LineCap> LineCap => this.CreateEnumAsyncProperty<LineCap>(LINE_CAP_PROPERTY);

        public IAsyncProperty<LineJoin> LineJoin => this.CreateEnumAsyncProperty<LineJoin>(LINE_JOIN_PROPERTY);

        public IAsyncProperty<float> MiterLimit => this.CreateAsyncProperty<float>(MITER_LIMIT_PROPERTY);

        public IAsyncProperty<float> LineDashOffset => this.CreateAsyncProperty<float>(LINE_DASH_OFFSET_PROPERTY);

        public IAsyncProperty<float> ShadowBlur => this.CreateAsyncProperty<float>(SHADOW_BLUR_PROPERTY);

        public IAsyncProperty<string> ShadowColor => this.CreateAsyncProperty<string>(SHADOW_COLOR_PROPERTY);

        public IAsyncProperty<float> ShadowOffsetX => this.CreateAsyncProperty<float>(SHADOW_OFFSET_X_PROPERTY);

        public IAsyncProperty<float> ShadowOffsetY => this.CreateAsyncProperty<float>(SHADOW_OFFSET_Y_PROPERTY);

        public IAsyncProperty<float> GlobalAlpha => this.CreateAsyncProperty<float>(GLOBAL_ALPHA_PROPERTY);

        public ElementReference Canvas { get; private set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }

        #endregion

        internal Canvas2dContextAsync(BECanvasComponent canvasReference) => this.Canvas = canvasReference.CanvasReference;

        internal async Task<Canvas2dContextAsync> AddCanvasAsync()
        {
            await this.JSRuntime.InvokeAsync<object>(ADD_CANVAS_ACTION, this.Canvas);
            return this;
        }

        #region Methods
        public ValueTask<object> FillRectAsync(double x, double y, double width, double height) => this.CallMethodAsync<object>(FILL_RECT_METHOD, new object[] { x, y, width, height });
        public ValueTask<object> ClearRectAsync(double x, double y, double width, double height) => this.CallMethodAsync<object>(CLEAR_RECT_METHOD, new object[] { x, y, width, height });
        public ValueTask<object> StrokeRectAsync(double x, double y, double width, double height) => this.CallMethodAsync<object>(STROKE_RECT_METHOD, new object[] { x, y, width, height });
        public ValueTask<object> FillTextAsync(string text, double x, double y, double? maxWidth = null) => this.CallMethodAsync<object>(FILL_TEXT_METHOD, maxWidth.HasValue ? new object[] { text, x, y, maxWidth.Value } : new object[] { text, x, y });
        public ValueTask<object> StrokeTextAsync(string text, double x, double y, double? maxWidth = null) => this.CallMethodAsync<object>(STROKE_TEXT_METHOD, maxWidth.HasValue ? new object[] { text, x, y, maxWidth.Value } : new object[] { text, x, y });
        public ValueTask<TextMetrics> MeasureTextAsync(string text) => this.CallMethodAsync<TextMetrics>(MEASURE_TEXT_METHOD, new object[] { text });
        public ValueTask<float[]> GetLineDashAsync() => this.CallMethodAsync<float[]>(GET_LINE_DASH_METHOD);
        public ValueTask<object> SetLineDashAsync(float[] segments) => this.CallMethodAsync<object>(SET_LINE_DASH_METHOD, new object[] { segments });
        public ValueTask<object> BeginPathAsync() => this.CallMethodAsync<object>(BEGIN_PATH_METHOD);
        public ValueTask<object> ClosePathAsync() => this.CallMethodAsync<object>(CLOSE_PATH_METHOD);
        public ValueTask<object> MoveToAsync(double x, double y) => this.CallMethodAsync<object>(MOVE_TO_METHOD, new object[] { x, y });
        public ValueTask<object> LineToAsync(double x, double y) => this.CallMethodAsync<object>(LINE_TO_METHOD, new object[] { x, y });
        public ValueTask<object> BezierCurveToAsync(double cp1x, double cp1y, double cp2x, double cp2y, double x, double y) => this.CallMethodAsync<object>(BEZIER_CURVE_TO_METHOD, new object[] { cp1x, cp1y, cp2x, cp2y, x, y });
        public ValueTask<object> QuadraticCurveToAsync(double cpx, double cpy, double x, double y) => this.CallMethodAsync<object>(QUADRATIC_CURVE_TO_METHOD, new object[] { cpx, cpy, x, y });
        public ValueTask<object> ArcAsync(double x, double y, double radius, double startAngle, double endAngle, bool? anticlockwise = null) => this.CallMethodAsync<object>(ARC_METHOD, anticlockwise.HasValue ? new object[] { x, y, radius, startAngle, endAngle, anticlockwise.Value } : new object[] { x, y, radius, startAngle, endAngle });
        public ValueTask<object> ArcToAsync(double x1, double y1, double x2, double y2, double radius) => this.CallMethodAsync<object>(ARC_TO_METHOD, new object[] { x1, y1, x2, y2, radius });
        public ValueTask<object> RectAsync(double x, double y, double width, double height) => this.CallMethodAsync<object>(RECT_METHOD, new object[] { x, y, width, height });
        public ValueTask<object> FillAsync() => this.CallMethodAsync<object>(FILL_METHOD);
        public ValueTask<object> StrokeAsync() => this.CallMethodAsync<object>(STROKE_METHOD);
        public ValueTask<object> DrawFocusIfNeededAsync(ElementReference elementReference) => this.CallMethodAsync<object>(DRAW_FOCUS_IF_NEEDED_METHOD, new object[] { elementReference });
        public ValueTask<object> ScrollPathIntoViewAsync() => this.CallMethodAsync<object>(SCROLL_PATH_INTO_VIEW_METHOD);
        public ValueTask<object> ClipAsync() => this.CallMethodAsync<object>(CLIP_METHOD);
        public ValueTask<bool> IsPointInPathAsync(double x, double y) => this.CallMethodAsync<bool>(IS_POINT_IN_PATH_METHOD, new object[] { x, y });
        public ValueTask<bool> IsPointInStrokeAsync(double x, double y) => this.CallMethodAsync<bool>(IS_POINT_IN_STROKE_METHOD, new object[] { x, y });
        public ValueTask<object> RotateAsync(float angle) => this.CallMethodAsync<object>(ROTATE_METHOD, new object[] { angle });
        public ValueTask<object> ScaleAsync(double x, double y) => this.CallMethodAsync<object>(SCALE_METHOD, new object[] { x, y });
        public ValueTask<object> TranslateAsync(double x, double y) => this.CallMethodAsync<object>(TRANSLATE_METHOD, new object[] { x, y });
        public ValueTask<object> TransformAsync(double m11, double m12, double m21, double m22, double dx, double dy) => this.CallMethodAsync<object>(TRANSFORM_METHOD, new object[] { m11, m12, m21, m22, dx, dy });
        public ValueTask<object> SetTransformAsync(double m11, double m12, double m21, double m22, double dx, double dy) => this.CallMethodAsync<object>(SET_TRANSFORM_METHOD, new object[] { m11, m12, m21, m22, dx, dy });
        public ValueTask<object> SaveAsync() => this.CallMethodAsync<object>(SAVE_METHOD);
        public ValueTask<object> RestoreAsync() => this.CallMethodAsync<object>(RESTORE_METHOD);
        #endregion

        #region Private Methods
        private ValueTask<T> GetPropertyAsync<T>(string property) => this.JSRuntime.InvokeAsync<T>(GET_CANVAS_PROPERTY_ACTION, this.Canvas, property);

        private ValueTask<object> SetPropertyAsync(string property, object value) => this.JSRuntime.InvokeAsync<object>(SET_CANVAS_PROPERTY_ACTION, this.Canvas, property, value);

        private ValueTask<T> CallMethodAsync<T>(string method, params object[] values) => this.JSRuntime.InvokeAsync<T>(CALL_CANVAS_METHOD_ACTION, this.Canvas, method, values);

        private IAsyncProperty<TNet> CreateEnumAsyncProperty<TNet>(string propertyName) where TNet : Enum => new CanvasAsyncProperty<TNet, string>(this.Canvas, propertyName, this.ToLowerInvariantString, this.ParseEnum<TNet>);

        private IAsyncProperty<T> CreateAsyncProperty<T>(string propertyName) => new CanvasAsyncProperty<T, T>(this.Canvas, propertyName, v => v, v => v);
        
        private T ParseEnum<T>(string value) where T : Enum => (T)Enum.Parse(typeof(T), value);

        private string ToLowerInvariantString<T>(T value) where T : Enum => value.ToString().ToLowerInvariant();

        public void Dispose() => Task.Run(() => this.JSRuntime.InvokeAsync<object>(REMOVE_CANVAS_ACTION, this.Canvas));
        #endregion
    }
}
