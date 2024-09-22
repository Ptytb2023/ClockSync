using Infrastructure.Services.Coroutines;
using Infrastructure.Services.JSON;
using Infrastructure.Services.Times;
using Infrastructure.Services.Web;
using TimeSynchronizer;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineRuner _coroutineRuner;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CoroutineService>().AsSingle().WithArguments(_coroutineRuner).NonLazy();
            Container.BindInterfacesTo<TickService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<JsonNetService>().AsCached().NonLazy();
            Container.BindInterfacesTo<WebService>().AsCached().NonLazy();
            Container.BindInterfacesTo<TimeService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WebTimeService>().AsSingle().NonLazy();

            Container.Bind<IWebTimeSynchronizer>().To<WebTimeSynchronizer>().AsSingle().NonLazy();
        }
    }
}
