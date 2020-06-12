using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Extensions;

namespace BCKWorks.Engine.Modules.Status
{
    public class StatusModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<ILoadingStatus, LoadingStatus>();
            container.Bind<IEngineStatus, EngineStatus>();
            container.Bind<IModStatus, ModStatus>();
            container.Bind<ISceneStatus, SceneStatus>();
        }
    }
}
