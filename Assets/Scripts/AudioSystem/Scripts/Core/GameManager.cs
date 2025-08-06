/*
using UnityEngine;
using Zenject;

/// <summary>
/// The core entry point for initializing game-wide systems during startup.
/// This class is automatically called by Zenject as part of the IInitializable lifecycle.
/// </summary>
internal class GameManager : IInitializable
{
    private readonly SettingsManager _settingsManager;

    /// <summary>
    /// Constructs a new instance of the <see cref="GameManager"/> class with injected dependencies.
    /// </summary>
    /// <param name="settingsManager">The manager responsible for handling all game settings modules.</param>
    public GameManager(SettingsManager settingsManager)
    {
        _settingsManager = settingsManager;
    }

    /// <summary>
    /// Called by Zenject during the initialization phase.
    /// Loads and applies all necessary game settings, such as audio configuration.
    /// </summary>
    public void Initialize()
    {
        var audioSettings = _settingsManager.GetModule<AudioSettings>();

        if (audioSettings != null)
        {
            _settingsManager.LoadNecessarySettings(audioSettings);
        }
        else
        {
            Debug.LogWarning("[GameManager] AudioSettings module not found in SettingsManager.");
        }
    }
}
*/
