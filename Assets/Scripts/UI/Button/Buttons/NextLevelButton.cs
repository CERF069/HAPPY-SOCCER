using UnityEngine;
using Zenject;

public class NextLevelButton : GameButtonBase
{
    [Inject] private LevelManager _levelManager;
    protected override void OnClick()
    {
        _levelManager.OnLevelSelected(PlayerPrefs.GetInt("SelectedLevel") + 1);
        GameController.StartGame();
    }
}