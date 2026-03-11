interface JSBridge {
    ping(): Promise<string>;
    showMessage(message: string): Promise<void>;
}

interface Chrome {
    webview: {
        hostObjects: {
            jsBridge: JSBridge;
        }
    }
}

declare const chrome: Chrome;