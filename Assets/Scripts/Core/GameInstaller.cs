using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer _audioMixer;
    
    [SerializeField] private SkinData _skinData;
    
    [SerializeField] private LevelDisplayTMP _levelDisplayPrefab;
    
    [SerializeField]
    private Player _player;

    [SerializeField]
    private Spike _spikePrefab;

    public override void InstallBindings()
    {
        BindSettingsManager();
        BindGameTimer();
        
        BindAudioSettings();
        
        BindServices();
        DeclareSkinsSignals();
        
        
        GameManagerBinding();
        PlayerBinding();
        SpikeBinding();

        BindFpsUnlocker();
    }

    private void GameManagerBinding()
    {
        Container.Bind<GameManager>().AsSingle();

        Container.Bind<IGameStateProvider>().To<GameManager>().FromResolve();
        Container.Bind<IGameController>().To<GameManager>().FromResolve();
    }

    private void BindGameTimer()
    {
        Container.BindInterfacesAndSelfTo<GameTimer>()
            .AsSingle()
            .WithArguments(1f* (PlayerPrefs.GetInt("SelectedLevel")+1));
        
        Container
            .BindInterfacesAndSelfTo<GameTimerProgressBar>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container
            .BindInterfacesTo<LevelDisplayTMP>()
            .FromComponentInNewPrefab(_levelDisplayPrefab)
            .AsSingle();
        
        Container.BindInterfacesTo<CoinsRewarder>()
            .AsSingle();
    }
    
    [SerializeField] private AudioSource switchAudioSource;
    [SerializeField] private AudioClip[] switchClips;
    
    private void PlayerBinding()
    {
        // биндим PlayerInput
        Container.BindInterfacesTo<PlayerInput>().AsSingle().NonLazy();

        // биндим Player
        Container.Bind<Player>().FromInstance(_player).AsSingle();

        // биндим плеер звуков переключения
        Container.Bind<SwitchSoundPlayer>()
            .AsSingle()
            .WithArguments(switchAudioSource, switchClips);
    }
    
    [SerializeField] private ObjectSpawner _objectSpawnerPrefab;
    private void SpikeBinding()
    {
        Container.BindFactory<Spike, SpikeFactory>()
            .FromComponentInNewPrefab(_spikePrefab)
            .AsTransient();
        
        Container
            .BindInterfacesTo<ObjectSpawner>()
            .FromComponentInNewPrefab(_objectSpawnerPrefab)
            .AsSingle();
        
        Container.Bind<ISpriteRenderer>()
            .To<SpikeRenderer>()
            .AsTransient();
    }

    /*private void UIWindowBinding()
    {
        // Предположим, GameManager уже есть
        Container.Bind<GameManager>().AsSingle();

        // Получаем все компоненты GameEndWindow в сцене
        var windows = GameObject.FindObjectsOfType<GameEndWindow>(true); // true — чтобы найти даже неактивные

        foreach (var window in windows)
        {
            // Получаем все интерфейсы, которые реализует окно
            var interfaces = window.GetType().GetInterfaces();

            foreach (var iface in interfaces)
            {
                // Проверяем, что интерфейс — это один из наших слушателей
                if (iface == typeof(IWonGameListener) || iface == typeof(ILoseGameListener)
                                                      || iface == typeof(IStartGameListener) || iface == typeof(IPauseGameListener)
                                                      || iface == typeof(IResumeGameListener))
                {
                    // Регистрируем интерфейс и конкретный экземпляр (уже созданный в сцене)
                    Container.Bind(iface).FromInstance(window).AsSingle();
                }
            }
        }
    }*/
    
    
    private void BindSettingsManager()
    {
        Container
            .Bind<SettingsManager>()
            .AsSingle();
    }
    private void BindAudioSettings()
    {
        var audioSettings = new AudioSettings();
        audioSettings.SetAudioMixer(_audioMixer);
        audioSettings.Load();

        Container
            .Bind<AudioSettings>()
            .FromInstance(audioSettings)
            .AsSingle();

        Container
            .Bind<ISettingsModule>()
            .To<AudioSettings>()
            .FromResolve();
    }

    private void BindFpsUnlocker()
    {
        Container.Bind<FpsUnlocker>()
            .AsSingle()
            .NonLazy(); // сразу выполнится в конструкторе
    }
    
    private void BindServices()
    {
        BindSkinService();
        BindCurrencyService();
        BindLevelSystem();
    }
    
    private void BindSkinService()
    {
        Container
            .Bind<SkinData>()
            .FromInstance(_skinData)
            .AsSingle();
        
        Container
            .Bind<ISkinService>()
            .To<SkinSelector>()
            .AsSingle();
    }
    private void DeclareSkinsSignals()
    {
        SignalBusInstaller
            .Install(Container);

        Container
            .DeclareSignal<SkinSelectedSignal>();
    }

    private void BindCurrencyService()
    {
        Container
            .Bind<ICurrencyService>()
            .To<CurrencyService>()
            .AsSingle();
    }
    
    private void BindLevelSystem()
    {
        Container.BindInterfacesAndSelfTo<LevelManager>()
            .AsSingle();
        
        
    }
}
