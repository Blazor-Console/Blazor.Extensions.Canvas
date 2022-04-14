using Blazor.Extensions.Canvas.Canvas2D;
using Blazor.Extensions.Canvas.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using static Blazor.Extensions.Canvas2dContextConstants;

namespace Blazor.Extensions
{
    public class Canvas2dContext : IDisposable
    {
        #region Properties
        private string _fillStyle = "#000";

        public string FillStyle
        {
            get => this._fillStyle;
            set
            {
                this._fillStyle = value;

                this.SetProperty(FILL_STYLE_PROPERTY, value);
            }
        }

        private string _strokeStyle = "#000";

        public string StrokeStyle
        {
            get => this._strokeStyle;
            set
            {
                this._strokeStyle = value;
                this.SetProperty(STROKE_STYLE_PROPERTY, value);
            }
        }

        private string _font = "10px sans-serif";

        public string Font
        {
            get => this._font;
            set
            {
                this._font = value;
                this.SetProperty(FONT_PROPERTY, value);
            }
        }

        private TextAlign _textAlign;

        public TextAlign TextAlign
        {
            get => this._textAlign;
            set
            {
                this._textAlign = value;
                this.SetProperty(TEXT_ALIGN_PROPERTY, value.ToString().ToLowerInvariant());
            }
        }

        private TextDirection _direction;

        public TextDirection Direction
        {
            get => this._direction;
            set
            {
                this._direction = value;
                this.SetProperty(DIRECTION_PROPERTY, value.ToString().ToLowerInvariant());
            }
        }

        private TextBaseline _textBaseline;

        public TextBaseline TextBaseline
        {
            get => this._textBaseline;
            set
            {
                this._textBaseline = value;
                this.SetProperty(TEXT_BASELINE_PROPERTY, value.ToString().ToLowerInvariant());
            }
        }

        private float _lineWidth = 1.0f;

        public float LineWidth
        {
            get => this._lineWidth;
            set
            {
                this._lineWidth = value;
                this.SetProperty(LINE_WIDTH_PROPERTY, value);
            }
        }

        private LineCap _lineCap;

        public LineCap LineCap
        {
            get => this._lineCap;
            set
            {
                this._lineCap = value;
                this.SetProperty(LINE_CAP_PROPERTY, value.ToString().ToLowerInvariant());
            }
        }

        private LineJoin _lineJoin;

        public LineJoin LineJoin
        {
            get => this._lineJoin;
            set
            {
                this._lineJoin = value;
                this.SetProperty(LINE_JOIN_PROPERTY, value.ToString().ToLowerInvariant());
            }
        }

        private float _miterLimit = 10;

        public float MiterLimit
        {
            get => this._miterLimit;
            set
            {
                this._miterLimit = value;
                this.SetProperty(MITER_LIMIT_PROPERTY, value);
            }
        }

        private float _lineDashOffset;

        public float LineDashOffset
        {
            get => this._lineDashOffset;
            set
            {
                this._lineDashOffset = value;
                this.SetProperty(LINE_DASH_OFFSET_PROPERTY, value);
            }
        }

        private float _shadowBlur;

        public float ShadowBlur
        {
            get => this._shadowBlur;
            set
            {
                this._shadowBlur = value;
                this.SetProperty(SHADOW_BLUR_PROPERTY, value);
            }
        }

        private string _shadowColor = "black";

        public string ShadowColor
        {
            get => this._shadowColor;
            set
            {
                this._shadowColor = value;
                this.SetProperty(SHADOW_COLOR_PROPERTY, value);
            }
        }

        private float _shadowOffsetX;

        public float ShadowOffsetX
        {
            get => this._shadowOffsetX;
            set
            {
                this._shadowOffsetX = value;
                this.SetProperty(SHADOW_OFFSET_X_PROPERTY, value);
            }
        }

        private float _shadowOffsetY;

        public float ShadowOffsetY
        {
            get => this._shadowOffsetY;
            set
            {
                this._shadowOffsetY = value;
                this.SetProperty(SHADOW_OFFSET_Y_PROPERTY, value);
            }
        }

        private float _globalAlpha = 1.0f;

        public float GlobalAlpha
        {
            get => this._globalAlpha;
            set
            {
                this._globalAlpha = value;
                this.SetProperty(GLOBAL_ALPHA_PROPERTY, value);
            }
        }

        public ElementReference Canvas { get; private set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }
        #endregion

        internal Canvas2dContext(BECanvasComponent canvasReference)
        {
            this.Canvas = canvasReference.CanvasReference;
            this.JSRuntime.InvokeAsync<object>(ADD_CANVAS_ACTION, this.Canvas);
        }

        #region Methods
        public async Task FillRectAsync(double x, double y, double width, double height) => await this.CallMethod<object>(FILL_RECT_METHOD, new object[] { x, y, width, height });
        public async Task ClearRectAsync(double x, double y, double width, double height) => await this.CallMethod<object>(CLEAR_RECT_METHOD, new object[] { x, y, width, height });
        public async void StrokeRectAsync(double x, double y, double width, double height) => await this.CallMethod<object>(STROKE_RECT_METHOD, new object[] { x, y, width, height });
        public async void FillTextAsync(string text, double x, double y, double? maxWidth = null) => await this.CallMethod<object>(FILL_TEXT_METHOD, maxWidth.HasValue ? new object[] { text, x, y, maxWidth.Value } : new object[] { text, x, y });
        public async void StrokeTextAsync(string text, double x, double y, double? maxWidth = null) => await this.CallMethod<object>(STROKE_TEXT_METHOD, maxWidth.HasValue ? new object[] { text, x, y, maxWidth.Value } : new object[] { text, x, y });
        public async ValueTask<TextMetrics> MeasureTextAsync(string text) => await this.CallMethod<TextMetrics>(MEASURE_TEXT_METHOD, new object[] { text });
        public async ValueTask<float[]> GetLineDashAsync() => await this.CallMethod<float[]>(GET_LINE_DASH_METHOD);
        public async void SetLineDashAsync(float[] segments) => await this.CallMethod<object>(SET_LINE_DASH_METHOD, new object[] { segments });
        public async void BeginPathAsync() => await this.CallMethod<object>(BEGIN_PATH_METHOD);
        public async void ClosePathAsync() => await this.CallMethod<object>(CLOSE_PATH_METHOD);
        public async void MoveToAsync(double x, double y) => await this.CallMethod<object>(MOVE_TO_METHOD, new object[] { x, y });
        public async void LineToAsync(double x, double y) => await this.CallMethod<object>(LINE_TO_METHOD, new object[] { x, y });
        public async void BezierCurveToAsync(double cp1x, double cp1y, double cp2x, double cp2y, double x, double y) => await this.CallMethod<object>(BEZIER_CURVE_TO_METHOD, new object[] { cp1x, cp1y, cp2x, cp2y, x, y });
        public async void QuadraticCurveToAsync(double cpx, double cpy, double x, double y) => await this.CallMethod<object>(QUADRATIC_CURVE_TO_METHOD, new object[] { cpx, cpy, x, y });
        public async void ArcAsync(double x, double y, double radius, double startAngle, double endAngle, bool? anticlockwise = null) => await this.CallMethod<object>(ARC_METHOD, anticlockwise.HasValue ? new object[] { x, y, radius, startAngle, endAngle, anticlockwise.Value } : new object[] { x, y, radius, startAngle, endAngle });
        public async void ArcToAsync(double x1, double y1, double x2, double y2, double radius) => await this.CallMethod<object>(ARC_TO_METHOD, new object[] { x1, y1, x2, y2, radius });
        public async void RectAsync(double x, double y, double width, double height) => await this.CallMethod<object>(RECT_METHOD, new object[] { x, y, width, height });
        public async void FillAsync() => await this.CallMethod<object>(FILL_METHOD);
        public async void StrokeAsync() => await this.CallMethod<object>(STROKE_METHOD);
        public async void DrawFocusIfNeededAsync(ElementReference elementReference) => await this.CallMethod<object>(DRAW_FOCUS_IF_NEEDED_METHOD, new object[] { elementReference });
        public async void ScrollPathIntoViewAsync() => await this.CallMethod<object>(SCROLL_PATH_INTO_VIEW_METHOD);
        public async void ClipAsync() => await this.CallMethod<object>(CLIP_METHOD);
        public async ValueTask<bool> IsPointInPathAsync(double x, double y) => await this.CallMethod<bool>(IS_POINT_IN_PATH_METHOD, new object[] { x, y });
        public async ValueTask<bool> IsPointInStrokeAsync(double x, double y) => await this.CallMethod<bool>(IS_POINT_IN_STROKE_METHOD, new object[] { x, y });
        public async void RotateAsync(float angle) => await this.CallMethod<object>(ROTATE_METHOD, new object[] { angle });
        public async void ScaleAsync(double x, double y) => await this.CallMethod<object>(SCALE_METHOD, new object[] { x, y });
        public async void TranslateAsync(double x, double y) => await this.CallMethod<object>(TRANSLATE_METHOD, new object[] { x, y });
        public async void TransformAsync(double m11, double m12, double m21, double m22, double dx, double dy) => await this.CallMethod<object>(TRANSFORM_METHOD, new object[] { m11, m12, m21, m22, dx, dy });
        public async void SetTransformAsync(double m11, double m12, double m21, double m22, double dx, double dy) => await this.CallMethod<object>(SET_TRANSFORM_METHOD, new object[] { m11, m12, m21, m22, dx, dy });
        public async void SaveAsync() => await this.CallMethod<object>(SAVE_METHOD);
        public async void RestoreAsync() => await this.CallMethod<object>(RESTORE_METHOD);
        #endregion

        #region Private Methods
        private void SetProperty(string property, object value)
        {
            this.JSRuntime.InvokeAsync<object>(SET_CANVAS_PROPERTY_ACTION, this.Canvas, property, value);
        }

        private ValueTask<T> CallMethod<T>(string method)
        {
            return this.JSRuntime.InvokeAsync<T>(CALL_CANVAS_METHOD_ACTION, this.Canvas, method);
        }

        private ValueTask<T> CallMethod<T>(string method, object value)
        {
            return this.JSRuntime.InvokeAsync<T>(CALL_CANVAS_METHOD_ACTION, this.Canvas, method, value);
        }

        public void Dispose()
        {
            this.JSRuntime.InvokeAsync<object>(REMOVE_CANVAS_ACTION, this.Canvas);
        } 
        #endregion
    }
}
