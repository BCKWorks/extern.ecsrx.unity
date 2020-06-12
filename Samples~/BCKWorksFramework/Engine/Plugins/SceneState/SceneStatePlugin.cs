using BCKWorks.Engine.Plugins.SceneState.Systems;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Plugins;
using EcsRx.Infrastructure.Extensions;
using EcsRx.Systems;
using System;
using System.Collections.Generic;

namespace BCKWorks.Engine.Plugins.SceneState
{
    public class SceneStatePlugin : IEcsRxPlugin
    {
        public string Name => "Scene State Plugin";
        public Version Version => new Version(0, 0, 1);

        public void SetupDependencies(IDependencyContainer container)
        {
            container.Bind<SceneStateSystem>();
        }

        public IEnumerable<ISystem> GetSystemsForRegistration(IDependencyContainer container)
        {
            return new[]
            {
                container.Resolve<SceneStateSystem>() as ISystem
            };
        }
    }
}
