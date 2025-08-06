using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, ISwitchPlayer
{
    [Inject] private SwitchSoundPlayer _soundPlayer;

    public void SwitchTurn()
    {
        transform.position = new Vector3(
            -transform.position.x,
            transform.position.y,
            transform.position.z
        );

        _soundPlayer.PlayNext();
    }

}
