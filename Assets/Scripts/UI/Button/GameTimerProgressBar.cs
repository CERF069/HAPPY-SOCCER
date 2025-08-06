using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameTimerProgressBar : MonoBehaviour, ITickable, IStartGameListener, IWonGameListener, ILoseGameListener
{
    private IGameTimer _gameTimer;
    [SerializeField] private Slider _slider;
    private float _maxDuration = 1f;

    [Inject]
    public void Construct(IGameTimer gameTimer)
    {
        _gameTimer = gameTimer;
    }

    private void Awake()
    {
        if (_slider == null)
            _slider = GetComponent<Slider>();

        _slider.minValue = 0f;
        _slider.maxValue = 1f;
        _slider.value = 0f;

        // Прячем сам слайдер при запуске игры
        _slider.gameObject.SetActive(false);
    }

    public void Tick()
    {
        if (_maxDuration <= 0) return;

        float progress = 1f - (_gameTimer.SpawnDuration / _maxDuration);
        _slider.value = Mathf.Clamp01(progress);
    }

    public void OnStartGame()
    {
        // Показываем слайдер
        _slider.gameObject.SetActive(true);

        // Обновляем максимальную длительность при старте игры
        _maxDuration = (_gameTimer as GameTimer)?.TotalDuration ?? 1f;

        // Сбрасываем прогрессбар в начало
        _slider.value = 0f;
    }

    public void OnWonGame()
    {
        // Скрываем слайдер при победе
        _slider.gameObject.SetActive(false);
    }

    public void OnLoseGame()
    {
        // Скрываем слайдер при проигрыше
        _slider.gameObject.SetActive(false);
    }
}