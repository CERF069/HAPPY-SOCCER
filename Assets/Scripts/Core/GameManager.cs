using UnityEngine;
using System.Collections.Generic;

public sealed class GameManager : IGameController, IGameStateProvider
{
    private GameState _state = GameState.NONE;
    public GameState CurrentState => _state;

    private readonly List<IGameListener> _listeners = new();

    public void AddListener(IGameListener listener) => _listeners.Add(listener);
    public void RemoveListener(IGameListener listener) => _listeners.Remove(listener);

    public void StartGame()
    {
        _state = GameState.PLAY;
        Time.timeScale = 1;
        foreach (var it in _listeners)
            if (it is IStartGameListener startListener)
                startListener.OnStartGame();
    }

    public void PauseGame()
    {
        _state = GameState.NONE;
        foreach (var it in _listeners)
            if (it is IPauseGameListener pauseListener)
                pauseListener.OnPauseGame();
    }

    public void ResumeGame()
    {
        _state = GameState.PLAY;
        Time.timeScale = 1;
    }

    public void LoseGame()
    {
        _state = GameState.NONE;
        Time.timeScale = 0;
        foreach (var it in _listeners)
            if (it is ILoseGameListener loseListener)
                loseListener.OnLoseGame();
    }

    public void WonGame()
    {
        _state = GameState.NONE;
        foreach (var it in _listeners)
            if (it is IWonGameListener wonListener)
                wonListener.OnWonGame();
        
        Debug.Log("Game Over");
    }
}
