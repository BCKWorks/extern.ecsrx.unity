using BCKWorks.Engine.Plugins.EngineState;
using BCKWorks.Engine.Plugins.ModState;
using BCKWorks.Engine.Plugins.SceneState;
using BCKWorks.Engine.Plugins.LoadingState;
using BCKWorks.Engine.Modules.Status;
using EcsRx.Zenject;
using EcsRx.Infrastructure.Extensions;
using UniRx;
using UnityEngine;
using System;
using BCKWorks.Engine.Events;

namespace BCKWorks.Mods.Default
{
    public class DefaultBootstrap : EcsRxApplicationBehaviour
    {
        protected override void LoadModules()
        {
            base.LoadModules();

            Container.LoadModule<StatusModule>();
        }

        protected override void LoadPlugins()
        {
            base.LoadPlugins();

            RegisterPlugin(new LoadingStatePlugin());
            RegisterPlugin(new EngineStatePlugin());
            RegisterPlugin(new ModStatePlugin());
            RegisterPlugin(new SceneStatePlugin());
        }

        IDisposable fireDisposable;

        protected override void ApplicationStarted()
        {
            fireDisposable = Observable.EveryUpdate().Subscribe(x =>
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    EventSystem.Publish(new LoadingPreparingEvent() { ChangeToSceneName = "" });
                    fireDisposable.Dispose();
                }
            });
        }

        private void OnDestroy()
        {
            StopAndUnbindAllSystems();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause == false)
            {
            }
        }

        private void OnApplicationFocus(bool focus)
        {

        }
    }
}
