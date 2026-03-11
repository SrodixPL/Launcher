import './LoadingScreen.css'

interface Props {
    closing: boolean
    onAnimationDone: () => void
    onCloseDone: () => void
}

export default function LoadingScreen({ closing, onAnimationDone, onCloseDone }: Props) {
    return (
        <div className="loading-root">
            <h1 className="loading-title">Launcher</h1>

            <div
                className={`curtain curtain-left ${closing ? 'hide' : 'open'}`}
                onAnimationEnd={!closing ? onAnimationDone : undefined}
            />
            <div
                className={`curtain curtain-right ${closing ? 'hide' : 'open'}`}
            />

            {closing && (
                <>
                    <div className="curtain curtain-top" />
                    <div
                        className="curtain curtain-bottom"
                        onAnimationEnd={onCloseDone}
                    />
                </>
            )}
        </div>
    )
}