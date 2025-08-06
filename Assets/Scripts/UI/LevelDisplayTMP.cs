using UnityEngine;
using TMPro;

public class LevelDisplayTMP : MonoBehaviour,
    IStartGameListener,
    IWonGameListener,
    ILoseGameListener
{
    [SerializeField] private TMP_Text[] _levelTexts;

    private void Awake()
    {
        if (_levelTexts == null || _levelTexts.Length == 0)
            _levelTexts = GetComponentsInChildren<TMP_Text>();
    }

    public void OnStartGame()
    {
        int level = PlayerPrefs.GetInt("SelectedLevel", 0) + 1;

        // Обновляем и показываем все тексты
        foreach (var text in _levelTexts)
        {
            if (text != null)
            {
                text.text = $"Level {level}";
            }
        }
    }

    public void OnWonGame()
    {
        int level = PlayerPrefs.GetInt("SelectedLevel", 0) + 1;

        // Обновляем и показываем все тексты
        foreach (var text in _levelTexts)
        {
            if (text != null)
            {
                text.text = $"Level {level}";
            }
        }
    }

    public void OnLoseGame()
    {
        int level = PlayerPrefs.GetInt("SelectedLevel", 0) + 1;

        // Обновляем и показываем все тексты
        foreach (var text in _levelTexts)
        {
            if (text != null)
            {
                text.text = $"Level {level}";
            }
        }
    }
}