using BCKWorks.Engine.Plugins.EngineState.Systems;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Plugins;
using EcsRx.Infrastructure.Extensions;
using EcsRx.Systems;
using System;
using System.Collections.Generic;

namespace BCKWorks.Engine.Plugins.EngineState
{
    public class EngineStatePlugin : IEcsRxPlugin
    {
        public string Name => "Engine State Plugin";
        public Version Version => new Version(0, 0, 1);

        public void SetupDependencies(IDependencyContainer container)
        {
            container.Bind<EngineStateSystem>();
        }

        public IEnumerable<ISystem> GetSystemsForRegistration(IDependencyContainer container)
        {
            return new[]
            {
                container.Resolve<EngineStateSystem>() as ISystem
            };
        }
    }
}
