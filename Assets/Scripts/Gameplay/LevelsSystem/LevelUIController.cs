using UnityEngine;
using Zenject;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private LevelButton[] _levelButtons;

    private ILevelInteraction _interaction;

    [Inject]
    public void Construct(ILevelInteraction interaction)
    {
        _interaction = interaction;
    }

    private void Start()
    {
        _interaction.SetButtons(_levelButtons);
        Debug.Log(PlayerPrefs.GetInt("UnlockedLevel"));
        Debug.Log(PlayerPrefs.GetInt("SelectedLevel"));
    }
    public void OnLevelPassed(int index)
    {
        _interaction.CompleteLevel(index);
    }
}