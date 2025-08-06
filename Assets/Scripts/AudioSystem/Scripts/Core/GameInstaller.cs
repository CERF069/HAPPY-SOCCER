/*
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

/// <summary>
/// Installer class responsible for binding game-related dependencies using Zenject.
/// </summary>
public class GameInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private SkinData _skinData;

    /// <summary>
    /// Entry point for dependency binding. 
    /// Calls individual binding methods to register all required services as singletons.
    /// </summary>
    public override void InstallBindings()
    {
        BindSettingsManager();

        BindAudioSettings();

        BindGameManager();

        BindServices();

        DeclareSkinsSignals();
    }


    /// <summary>
    /// Binds <see cref="GameManager"/> to all its interfaces as a singleton.
    /// </summary>
    private void BindGameManager()
    {
        Container
            .BindInterfacesTo<GameManager>()
            .AsSingle();
    }

    /// <summary>
    /// Binds <see cref="SettingsManager"/> as a singleton.
    /// </summary>
    private void BindSettingsManager()
    {
        Container
            .Bind<SettingsManager>()
            .AsSingle();
    }

    /// <summary>
    /// Binds both the concrete <see cref="AudioSettings"/> and its interface <see cref="ISettingsModule"/> as singletons.
    /// </summary>
    private void BindAudioSettings()
    {
        var audioSettings = new AudioSettings();
        audioSettings.SetAudioMixer(_audioMixer);

        Container
            .Bind<AudioSettings>()
            .FromInstance(audioSettings) // 🟢 important: bind the existing configured instance
            .AsSingle();

        Container
            .Bind<ISettingsModule>()
            .To<AudioSettings>()
            .FromResolve(); // reuse the same instance
    }




    /// <summary>
    /// Binds all application-level services to the Zenject dependency injection container.
    /// Call this method inside your installer (e.g., ProjectInstaller) to register service bindings.
    /// </summary>
    private void BindServices()
    {
        BindSkinService();
    }

    /// <summary>
    /// Registers the skin-related services into the Zenject container:
    /// - Binds a SkinData instance (ScriptableObject containing all skins)
    /// - Binds the ISkinService interface to the SkinSelector implementation
    /// </summary>
    private void BindSkinService()
    {
        // Bind SkinData instance to allow access to skin definitions
        Container
            .Bind<SkinData>()
            .FromInstance(_skinData)
            .AsSingle();

        // Bind ISkinService to SkinSelector as a singleton service
        Container
            .Bind<ISkinService>()
            .To<SkinSelector>()
            .AsSingle();
    }


    /// <summary>
    /// Declares the signals related to skin selection and installs the SignalBus into the container.
    /// This enables event-driven communication for skin selection within the application.
    /// </summary>
    private void DeclareSkinsSignals()
    {
        SignalBusInstaller
            .Install(Container);

        Container
            .DeclareSignal<SkinSelectedSignal>();
    }


}
*/
