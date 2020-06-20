using BCKWorks.Engine.Plugins.LoadingState.Systems;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Plugins;
using EcsRx.Infrastructure.Extensions;
using EcsRx.Systems;
using System;
using System.Collections.Generic;

namespace BCKWorks.Engine.Plugins.LoadingState
{
    public class LoadingStatePlugin : IEcsRxPlugin
    {
        public string Name => "Loading State Plugin";
        public Version Version => new Version(0, 0, 1);

        public void SetupDependencies(IDependencyContainer container)
        {
            container.Bind<LoadingStateSystem>();
        }

        public IEnumerable<ISystem> GetSystemsForRegistration(IDependencyContainer container)
        {
            return new[]
            {
                container.Resolve<LoadingStateSystem>() as ISystem
            };
        }
    }
}
