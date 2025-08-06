using UnityEngine;
using Zenject;


public class SpikeFactory : PlaceholderFactory<Spike> { }


public class Spike : MonoBehaviour, ISpawn
{
    private GameManager _gameManager;
    
    private ISpriteRenderer _rendererLogic;
    [SerializeField] private SpriteRenderer _spriteRenderer; 

    [Inject]
    public void Construct(GameManager gameManager, ISpriteRenderer spriteRenderer)
    {
        _gameManager = gameManager;
        _rendererLogic = spriteRenderer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { _gameManager?.LoseGame(); }
    }

    public void OnSpawn()
    {
        _rendererLogic.UpdateRenderer(_spriteRenderer,transform);
    }
}
