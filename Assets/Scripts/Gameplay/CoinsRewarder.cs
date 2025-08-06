using UnityEngine;
using Zenject;

public class CoinsRewarder : IStartGameListener, IWonGameListener
{
    private readonly IGameTimer _gameTimer;
    private float _levelDuration;

    [Inject]
    public CoinsRewarder(IGameTimer gameTimer)
    {
        _gameTimer = gameTimer;
    }

    public void OnStartGame()
    {
        // Запоминаем изначальную длительность таймера
        _levelDuration = _gameTimer.SpawnDuration;
        Debug.Log($"[CoinsRewarder] Старт игры. Длительность уровня: {_levelDuration} сек.");
    }

    public void OnWonGame()
    {
        // Награда = длительность уровня * коэффициент
        int reward = Mathf.RoundToInt(_levelDuration * 2);

        int currentCoins = PlayerPrefs.GetInt("Coins", 0);
        currentCoins += reward;

        PlayerPrefs.SetInt("Coins", currentCoins);
        PlayerPrefs.Save();

        Debug.Log($"[CoinsRewarder] Победа! Добавлено {reward} монет. Теперь у игрока {currentCoins} монет.");
    }
}