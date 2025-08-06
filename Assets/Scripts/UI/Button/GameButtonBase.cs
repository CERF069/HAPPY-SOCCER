using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class GameButtonBase : MonoBehaviour
{
    [Inject] protected IGameController GameController;
    [SerializeField] private Button _button;

    protected virtual void Awake()
    {
        if (_button == null)
            _button = GetComponent<Button>();

        _button?.onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();
}