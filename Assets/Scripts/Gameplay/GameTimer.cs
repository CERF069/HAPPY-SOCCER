using UnityEngine;
using Zenject;
public class GameTimer : IGameTimer,
    IStartGameListener,
    IWonGameListener,
    ILoseGameListener,
    ITickable
{
    private readonly IGameController _gameController;
    private float _spawnDuration;  // НЕ readonly

    private float _elapsedTime;
    private bool _isRunning;
    
    public float TotalDuration => _spawnDuration;
    public float SpawnDuration => Mathf.Max(_spawnDuration - _elapsedTime, 0);

    [Inject]
    public GameTimer(IGameController gameController, float spawnDuration)
    {
        _gameController = gameController;
        _spawnDuration = spawnDuration;
    }

    public void OnStartGame()
    {
        // Обновляем длительность таймера при каждом старте
        _spawnDuration = 6f * (PlayerPrefs.GetInt("SelectedLevel") + 1);

        _elapsedTime = 0f;
        _isRunning = true;
    }

    public void OnWonGame() => Stop();
    public void OnLoseGame() => Stop();

    private void Stop() => _isRunning = false;

    public void Tick()
    {
        if (!_isRunning) return;

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _spawnDuration)
        {
            _isRunning = false;
            Debug.Log("Time is up!");
            _gameController.WonGame();
        }
        else
        {
            Debug.Log($"[GameTimer] Осталось времени: {SpawnDuration:0.00} сек");
        }
    }
}
