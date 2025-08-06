public class PauseButton : GameButtonBase
{
    protected override void OnClick() => GameController.PauseGame();
}