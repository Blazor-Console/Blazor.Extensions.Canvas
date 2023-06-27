import { ContextManager } from './CanvasContextManager';

namespace Canvas {
  const haccBlazorExtensions: string = 'HACCBlazorExtensions';
  // define what this extension adds to the window object inside HACCBlazorExtensions
  const extensionObject = {
    Canvas2d: new ContextManager("2d"),
    WebGL: new ContextManager("webgl")
  };

  export function initialize(): void {
    if (typeof window !== 'undefined' && !window[haccBlazorExtensions]) {
      // when the library is loaded in a browser via a <script> element, make the
      // following APIs available in global scope for invocation from JS
      window[haccBlazorExtensions] = {
        ...extensionObject
      };
    } else {
      window[haccBlazorExtensions] = {
        ...window[haccBlazorExtensions],
        ...extensionObject
      };
    }
  }
}

Canvas.initialize();
