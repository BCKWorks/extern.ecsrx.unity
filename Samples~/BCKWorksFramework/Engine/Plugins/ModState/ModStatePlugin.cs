using BCKWorks.Engine.Plugins.ModState.Systems;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Plugins;
using EcsRx.Infrastructure.Extensions;
using EcsRx.Systems;
using System;
using System.Collections.Generic;

namespace BCKWorks.Engine.Plugins.ModState
{
    public class ModStatePlugin : IEcsRxPlugin
    {
        public string Name => "Mod State Plugin";
        public Version Version => new Version(0, 0, 1);

        public void SetupDependencies(IDependencyContainer container)
        {
            container.Bind<ModStateSystem>();
        }

        public IEnumerable<ISystem> GetSystemsForRegistration(IDependencyContainer container)
        {
            return new[]
            {
                container.Resolve<ModStateSystem>() as ISystem
            };
        }
    }
}
