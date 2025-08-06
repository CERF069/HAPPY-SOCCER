using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// Controls a UI slider to adjust the volume level for a specific audio type.
/// </summary>
internal class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioVolumeType _volumeType;
    [SerializeField] private Slider _slider;

    private AudioSettings _audioSettings;

    /// <summary>
    /// Injects the <see cref="AudioSettings"/> dependency via Zenject.
    /// </summary>
    /// <param name="audioSettings">The audio settings service.</param>
    [Inject]
    public void Construct(AudioSettings audioSettings)
    {
        _audioSettings = audioSettings;
    }

    /// <summary>
    /// Initializes the slider value from the current audio settings and registers listener for value changes.
    /// </summary>
    private void Start()
    {
        _slider.value = _audioSettings.GetVolume(_volumeType);
        _slider.onValueChanged.AddListener(OnSliderChanged);
    }

    /// <summary>
    /// Called when the slider value changes. Updates and saves the new volume level.
    /// </summary>
    /// <param name="value">The new volume value from the slider (0.0 to 1.0).</param>
    private void OnSliderChanged(float value)
    {
        _audioSettings.SetVolume(_volumeType, value);

        _audioSettings.Save();
    }

    /// <summary>
    /// Removes the listener from the slider to prevent memory leaks.
    /// </summary>
    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnSliderChanged);
    }
}
