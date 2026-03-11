import { useEffect, useState } from 'react'
import LoadingScreen from './components/LoadingScreen'
import WelcomeScreen from './components/WelcomeScreen'

function App() {
    const [appReady, setAppReady] = useState(false)
    const [animDone, setAnimDone] = useState(false)
    const [closing, setClosing] = useState(false)
    const [visible, setVisible] = useState(true)

    useEffect(() => {
        async function init() {
            // await CefSharp.BindObjectAsync("jsBridge")
            setAppReady(true)
        }
        init()
    }, [])

    useEffect(() => {
        if (appReady && animDone) {
            setTimeout(() => setClosing(true), 500)
        }
    }, [appReady, animDone])

    return visible
        ? <LoadingScreen
            closing={closing}
            onAnimationDone={() => setAnimDone(true)}
            onCloseDone={() => setTimeout(() => setVisible(false), 500)}
          />
        : <WelcomeScreen />
}

export default App