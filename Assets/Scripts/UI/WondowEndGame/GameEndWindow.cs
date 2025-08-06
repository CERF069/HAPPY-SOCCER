using UnityEngine;
using Zenject;

public abstract class GameEndWindow : MonoBehaviour
{
    [SerializeField] private GameObject window;

    [Inject] private GameManager _gameManager;

    protected virtual void Awake()
    {
        if (window != null)
            window.SetActive(false);
        
        _gameManager.AddListener(this as IGameListener);
    }

    private void OnDestroy()
    {
        _gameManager.RemoveListener(this as IGameListener);
    }

    protected void ShowWindow()
    {
        if (window != null)
            window.SetActive(true);
    }

    protected void HideWindow()
    {
        if (window != null)
            window.SetActive(false);
    }
}