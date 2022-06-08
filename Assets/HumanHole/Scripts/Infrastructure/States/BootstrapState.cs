using System;
using System.Collections;
using System.Threading.Tasks;
using Agava.YandexGames;
using HumanHole.Scripts.Infrastructure.Services;
using HumanHole.Scripts.Infrastructure.Services.Ads;
using HumanHole.Scripts.Infrastructure.Services.Analytics;
using HumanHole.Scripts.Infrastructure.Services.AssetsManagement;
using HumanHole.Scripts.Infrastructure.Services.Authorization;
using HumanHole.Scripts.Infrastructure.Services.DeviceDetection;
using HumanHole.Scripts.Infrastructure.Services.Download;
using HumanHole.Scripts.Infrastructure.Services.Factory;
using HumanHole.Scripts.Infrastructure.Services.LeaderBoard;
using HumanHole.Scripts.Infrastructure.Services.Localization;
using HumanHole.Scripts.Infrastructure.Services.PersistentProgress;
using HumanHole.Scripts.Infrastructure.Services.Profile;
using HumanHole.Scripts.Infrastructure.Services.SaveLoad;

namespace HumanHole.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, ICoroutineRunner coroutineRunner,
            AllServices services)
        {
            _coroutineRunner = coroutineRunner;
            _stateMachine = stateMachine;
            _services = services;
        
            RegisterServices();
        }

        public void Enter() => 
            OnEntered();

        private async void OnEntered()
        {
            _coroutineRunner.StartCoroutine(InitializeYandexSdk());
            ILocalizationService localizationService = _services.Single<ILocalizationService>();
            await localizationService.Initialize();
            _stateMachine.Enter<LoadProgressState>();
        }

        private IEnumerator InitializeYandexSdk()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        yield return YandexGamesSdk.WaitForInitialization();
#endif
            IAuthorizationService authorizationService = _services.Single<IAuthorizationService>();
            _coroutineRunner.StartCoroutine(authorizationService.Authorize());
            IProfileDataService profileDataService = _services.Single<IProfileDataService>();
            profileDataService.Initialize();
            
#if UNITY_EDITOR
            yield return null;
#endif
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<ILocalizationService>(new LocalizationService());
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IAdsService>(new AdsService());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<IAuthorizationService>(new AuthorizationService());
            _services.RegisterSingle<IProfileDataService>(new ProfileDataService(_services.Single<IAuthorizationService>()));
            _services.RegisterSingle<ILeaderBoardService>(new LeaderBoardService());
            _services.RegisterSingle<IRenderTextureService>(new RenderTextureService());
            _services.RegisterSingle<IAnalyticsService>(new AnalyticsService());
            _services.RegisterSingle<IDownloadService>(new DownloadService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IFactoryService>(new FactoryService(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<IDeviceDetectionService>(new DeviceDetectionService());
        }
    }
}