using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class IGameListenerComposite: MonoBehaviour,
    IStartGameListener,
    IPauseGameListener,
    IResumeGameListener,
    IWonGameListener,
    ILoseGameListener
{
    [Inject] private GameManager _gameManager;
    
    [InjectLocal]
    private List<IGameListener> _listeners;

    private IGameListener _loseGameListenerImplementation;

    private void Start()
    {
        _gameManager.AddListener(this);
        if (_listeners == null || _listeners.Count == 0)
        {
            Debug.LogWarning("[IGameListenerComposite] Слушателей не найдено!");
            return;
        }

        Debug.Log($"[IGameListenerComposite] Найдено {_listeners.Count} слушателей:");
        foreach (var listener in _listeners)
        {
            Debug.Log($" - {listener} (тип: {listener.GetType().FullName})", listener as Object);
        }
    }

    private void OnDestroy()
    {
        _gameManager.RemoveListener(this);
    }

    public void OnLoseGame()
    {
        Time.timeScale = 0;
        foreach (var it in _listeners)
            if (it is ILoseGameListener loseListener)
                loseListener.OnLoseGame();
    }

    public void OnStartGame()
    {
        foreach (var it in _listeners)
            if (it is IStartGameListener startListener)
                startListener.OnStartGame();
    }

    public void OnPauseGame()
    {
        foreach (var it in _listeners)
            if (it is IPauseGameListener pauseListener)
                pauseListener.OnPauseGame();
    }

    public void OnResumeGame()
    {
        throw new System.NotImplementedException();
    }

    public void OnWonGame()
    {
        foreach (var it in _listeners)
            if (it is IWonGameListener wonListener)
                wonListener.OnWonGame();
    }
}