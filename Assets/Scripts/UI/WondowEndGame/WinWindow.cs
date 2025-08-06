public class WinWindow : GameEndWindow, IWonGameListener
{
    public void OnWonGame()
    {
        ShowWindow();
    }
}