using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    private Button _button;
    private int _levelIndex;
    private ILevelInteraction _interaction;

    [SerializeField] private GameObject _lockedOverlay;
    [SerializeField] private GameObject _selectedHighlight;
    [SerializeField] private TextMeshProUGUI _levelNumberText;

    public void Setup(int levelIndex, ILevelInteraction interaction)
    {
        _levelIndex = levelIndex;
        _interaction = interaction;

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);

        if (_levelNumberText != null)
            _levelNumberText.text = (levelIndex + 1).ToString();
    }

    public void SetInteractable(bool interactable)
    {
        _button.interactable = interactable;
        if (_lockedOverlay != null)
            _lockedOverlay.SetActive(!interactable);
    }

    public void SetSelected(bool isSelected)
    {
        if (_selectedHighlight != null)
            _selectedHighlight.SetActive(isSelected);
    }

    private void OnClick()
    {
        _interaction?.OnLevelSelected(_levelIndex);
    }
}

