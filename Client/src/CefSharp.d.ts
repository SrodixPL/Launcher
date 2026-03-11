interface CefSharp {
    BindObjectAsync(...names: string[]): Promise<void>;
}

interface JsBridge {
    ping(): Promise<string>;
    showMessage(message: string): Promise<void>;
}

declare const CefSharp: CefSharp;
declare const jsBridge: JsBridge;