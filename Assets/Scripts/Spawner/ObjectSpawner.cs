using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour,
    IStartGameListener,
    IWonGameListener,
    ILoseGameListener
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private int _maxObjects = 10;

    [Inject] private SpikeFactory _factory;
    [Inject] private IGameTimer _gameTimer;

    private readonly Queue<Spike> _pool = new();
    private readonly List<Spike> _activeObjects = new();

    private float _spawnTimer;
    private Camera _mainCamera;
    private bool _isSpawning;

    private void Awake()
    {
        _mainCamera = Camera.main;

        if (_spawnPoints == null || _spawnPoints.Length == 0)
            Debug.LogError("[Spawner] Точки спауна не заданы.");
    }

    private void InitializePool()
    {
        _pool.Clear();
        _activeObjects.Clear();

        for (int i = 0; i < _maxObjects; i++)
        {
            var spike = _factory.Create();
            spike.gameObject.SetActive(false);
            _pool.Enqueue(spike);
        }
    }

    private void Update()
    {
        if (_isSpawning && _gameTimer.SpawnDuration > 0f)
        {
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0f)
            {
                SpawnObject();
                _spawnTimer = _spawnInterval;
            }
        }

        RecycleOffscreenObjects();
    }

    private void SpawnObject()
    {
        if (_pool.Count == 0) return;

        var spike = _pool.Dequeue();
        var point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        spike.transform.position = point.position;
        spike.gameObject.SetActive(true);
        _activeObjects.Add(spike);

        if (spike is ISpawn spawnable)
            spawnable.OnSpawn();
    }

    private void RecycleOffscreenObjects()
    {
        for (int i = _activeObjects.Count - 1; i >= 0; i--)
        {
            var spike = _activeObjects[i];
            var viewport = _mainCamera.WorldToViewportPoint(spike.transform.position);

            if (viewport.y < 0 && viewport.z > 0)
            {
                spike.gameObject.SetActive(false);
                _pool.Enqueue(spike);
                _activeObjects.RemoveAt(i);
            }
        }
    }

    private void StopSpawning()
    {
        _isSpawning = false;

        for (int i = _activeObjects.Count - 1; i >= 0; i--)
        {
            var spike = _activeObjects[i];
            spike.gameObject.SetActive(false);
            _pool.Enqueue(spike);
            _activeObjects.RemoveAt(i);
        }
    }

    public void OnStartGame()
    {
        InitializePool();
        _spawnTimer = 0f;
        _isSpawning = true;
    }

    public void OnWonGame() => StopSpawning();
    public void OnLoseGame() => StopSpawning();
}
