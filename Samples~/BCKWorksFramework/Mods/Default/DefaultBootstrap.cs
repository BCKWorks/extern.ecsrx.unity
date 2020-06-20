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
<<<<<<< HEAD

namespace BCKWorks.Mods.Default
=======
using Mods.Default.Installer;
using BCKWorks.Installer;

namespace Mods.Default
>>>>>>> a4b5f752f0085a60dfd6ca0ef368e041c224e41b
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

<<<<<<< HEAD
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
=======
        protected override void ApplicationStarted()
        {
            var settings = Container.Resolve<DefaultInstaller.Settings>();
            var bckworksSettings = Container.Resolve<BCKWorksInstaller.Settings>();
            Debug.Log($"settings.Name is {settings.Name} in {bckworksSettings.Name}");

            Observable.Interval(TimeSpan.FromSeconds(1))
                .First()
                .Subscribe(x =>
            {
                EventSystem.Publish(new LoadingPreparingEvent() { ChangeToSceneName = "" });
>>>>>>> a4b5f752f0085a60dfd6ca0ef368e041c224e41b
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
