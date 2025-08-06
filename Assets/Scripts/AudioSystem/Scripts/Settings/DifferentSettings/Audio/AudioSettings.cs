using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Implementation of the audio settings module.
/// </summary>
public class AudioSettings : ISettingsModule
{
    // Store volumes by type in a dictionary
    private readonly Dictionary<AudioVolumeType, float> _volumes = new Dictionary<AudioVolumeType, float>()
    {
        { AudioVolumeType.Master, 1f },
        { AudioVolumeType.Music, 1f },
        { AudioVolumeType.SFX, 1f },
        { AudioVolumeType.Voice, 1f }
    };

    /// <summary>
    /// Gets the current volume for the specified type.
    /// </summary>
    public float GetVolume(AudioVolumeType volumeType)
    {
        if (_volumes.TryGetValue(volumeType, out var volume))
            return volume;
        return 1f;
    }

    /// <summary>
    /// Sets the volume level for the specified audio type.
    /// </summary>
    /// <param name="volumeType">The type of audio volume, e.g., Master or Music.</param>
    /// <param name="volume">The volume level, ranging from 0.0 to 1.0.</param>
    public void SetVolume(AudioVolumeType volumeType, float volume)
    {
        volume = Mathf.Clamp01(volume);

        _volumes[volumeType] = volume;

        ApplyVolumeToMixer(volumeType);
    }




    /// <summary>
    /// Handles applying individual volume levels to exposed parameters
    /// in the Unity AudioMixer based on <see cref="AudioVolumeType"/>.
    /// </summary>
    private AudioMixer _audioMixer;

    public void SetAudioMixer(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }

    /// <summary>
    /// Maps each <see cref="AudioVolumeType"/> to its corresponding exposed AudioMixer parameter name.
    /// </summary>
    private readonly Dictionary<AudioVolumeType, string> _mixerParameterNames = new()
{
    { AudioVolumeType.Master, "MasterVolume" },
    { AudioVolumeType.Music, "MusicVolume" },
/*    { AudioVolumeType.SFX, "SFXVolume" },
    { AudioVolumeType.Voice, "VoiceVolume" }*/
};

    /// <summary>
    /// Applies the current volume level of the specified <see cref="AudioVolumeType"/> 
    /// to the corresponding AudioMixer parameter in decibels.
    ///
    /// Unity AudioMixer expects volume values in decibels (ranging from -80 dB to 0 dB),
    /// so this method converts the 0.0–1.0 linear volume to logarithmic scale.
    /// </summary>
    /// <param name="volumeType">The audio volume type to apply (e.g., Master, Music).</param>
    private void ApplyVolumeToMixer(AudioVolumeType volumeType)
    {
        if (!_mixerParameterNames.TryGetValue(volumeType, out var paramName))
            return;

        float linearVolume = _volumes[volumeType];

        // Convert linear volume [0.0, 1.0] to logarithmic decibels [-80, 0]
        float decibels = Mathf.Log10(Mathf.Clamp(linearVolume, 0.0001f, 1f)) * 20f;

        _audioMixer.SetFloat(paramName, decibels);

        //Debug.Log($"[{volumeType}] dB={decibels}");
    }






    private const string VolumePrefsKeyPrefix = "AudioVolume_";

    /// <summary>
    /// Loads saved volume settings from PlayerPrefs and applies them to the AudioMixer.
    /// </summary>
    public void Load()
    {
        Debug.Log("Loading audio settings...");

        foreach (var type in _volumes.Keys.ToList())
        {
            string key = VolumePrefsKeyPrefix + type.ToString();

            if (PlayerPrefs.HasKey(key))
            {
                float savedVolume = PlayerPrefs.GetFloat(key, 1f);
                _volumes[type] = Mathf.Clamp01(savedVolume);
            }

            ApplyVolumeToMixer(type);
        }
    }

    /// <summary>
    /// Saves current volume settings to PlayerPrefs.
    /// </summary>
    public void Save()
    {
        Debug.Log("Saving audio settings...");

        foreach (var kvp in _volumes)
        {
            string key = VolumePrefsKeyPrefix + kvp.Key.ToString();
            PlayerPrefs.SetFloat(key, kvp.Value);
        }

        PlayerPrefs.Save();
    }
}
