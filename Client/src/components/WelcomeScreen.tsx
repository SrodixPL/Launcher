import { useState } from "react"

export default function WelcomeScreen() {
    const [result, setResult] = useState<string>("")

    async function testBridge() {
        const jsBridge = chrome.webview.hostObjects.jsBridge
        const response = await jsBridge.ping()
        setResult(response)
        await jsBridge.showMessage("I like pp")
    }

    return (
        <div className="fade-in">
            <button onClick={testBridge}>Click to test bridge</button>
            {result}
        </div>
    )
}