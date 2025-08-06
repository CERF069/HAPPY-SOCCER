public class LoseButton : GameButtonBase
{
    protected override void OnClick() => GameController.LoseGame();
}