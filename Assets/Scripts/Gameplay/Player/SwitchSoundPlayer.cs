using UnityEngine;

public class SwitchSoundPlayer
{
    private readonly AudioSource _audioSource;
    private readonly AudioClip[] _clips;
    private int _currentClipIndex;

    public SwitchSoundPlayer(AudioSource audioSource, AudioClip[] clips)
    {
        _audioSource = audioSource;
        _clips = clips;
    }

    public void PlayNext()
    {
        if (_clips == null || _clips.Length == 0) return;

        _audioSource.PlayOneShot(_clips[_currentClipIndex]);
        _currentClipIndex = (_currentClipIndex + 1) % _clips.Length;
    }
}