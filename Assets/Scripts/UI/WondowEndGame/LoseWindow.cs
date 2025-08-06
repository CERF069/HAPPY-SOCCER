public class LoseWindow : GameEndWindow, ILoseGameListener, IStartGameListener
{
    public void OnLoseGame()
    {
        ShowWindow();
    }
    public void OnStartGame()
    {
        HideWindow();
    }
}