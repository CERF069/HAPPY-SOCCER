using UnityEngine;
using Zenject;
public class LevelManager : ILevelInteraction, IWonGameListener
{
    private LevelButton[] _levelButtons;
    private int _unlockedLevelIndex = 0;
    private int _selectedLevelIndex = 0;

    private const string UnlockedLevelKey = "UnlockedLevel";
    private const string SelectedLevelKey = "SelectedLevel";
    
    public LevelManager()
    {
        LoadProgress();
    }

    public void SetButtons(LevelButton[] buttons)
    {
        _levelButtons = buttons;

        for (int i = 0; i < _levelButtons.Length; i++)
        {
            int index = i;
            _levelButtons[i].Setup(index, this);
        }

        UpdateRender();
    }

    public void UpdateRender()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            bool isUnlocked = i <= _unlockedLevelIndex;
            _levelButtons[i].SetInteractable(isUnlocked);

            bool isSelected = i == _selectedLevelIndex;
            _levelButtons[i].SetSelected(isSelected);
        }
    }

    public void CompleteLevel(int levelIndex)
    {
        if (levelIndex == _unlockedLevelIndex && levelIndex + 1 < _levelButtons.Length)
        {
            _unlockedLevelIndex++;
            SaveProgress();
            UpdateRender();
        }
    }

    public void OnLevelSelected(int levelIndex)
    {
        _selectedLevelIndex = levelIndex;
        SaveProgress();
        UpdateRender();

        Debug.Log($"Selected Level: {levelIndex + 1}");
        // Можно сюда добавить переход на сцену, загрузку и т.п.
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt(UnlockedLevelKey, _unlockedLevelIndex);
        PlayerPrefs.SetInt(SelectedLevelKey, _selectedLevelIndex);
        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        Debug.Log("Loading progress");
        _unlockedLevelIndex = PlayerPrefs.GetInt(UnlockedLevelKey, 0);
        _selectedLevelIndex = PlayerPrefs.GetInt(SelectedLevelKey, 0);
    }

    public void OnWonGame()
    {
        CompleteLevel(_unlockedLevelIndex);
        Debug.LogWarning($"Won Level: {_unlockedLevelIndex + 1}");
    }

}