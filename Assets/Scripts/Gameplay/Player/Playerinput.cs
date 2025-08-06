using UnityEngine;
using Zenject;

public class PlayerInput : ITickable
{
    [Inject] private Player _player;
    [Inject] private IGameStateProvider _gameStateProvider;

    public void Tick()
    {
        if (_gameStateProvider.CurrentState != GameState.PLAY)
            return;

        if (Input.GetMouseButtonDown(0) || 
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            _player.SwitchTurn();
        }
    }
}