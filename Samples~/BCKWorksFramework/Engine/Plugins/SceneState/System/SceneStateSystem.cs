using BCKWorks.Engine.Defines;
using BCKWorks.Engine.Events;
using BCKWorks.Engine.Modules.Status;
using EcsRx.Events;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Groups.Observable;
using EcsRx.Systems;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace BCKWorks.Engine.Plugins.SceneState.Systems
{
    public class SceneStateSystem : IManualSystem
    {
        public IGroup Group { get; } = new EmptyGroup();

        private readonly IList<IDisposable> subscriptions = new List<IDisposable>();
        private readonly ISceneStatus sceneStatus;
        private readonly IEventSystem eventSystem;

        public SceneStateSystem(ISceneStatus sceneStatus,
            IEventSystem eventSystem)
        {
            this.sceneStatus = sceneStatus;
            this.eventSystem = eventSystem;
        }

        public void StartSystem(IObservableGroup observableGroup)
        {
            sceneStatus.StateRP.DistinctUntilChanged().Subscribe(stateIn).AddTo(subscriptions);
            Observable.EveryUpdate().Subscribe(stateUpdate).AddTo(subscriptions);

            eventSystem.Receive<LoadingPreparingEvent>()
                .Subscribe(evt =>
                {
                    sceneStatus.State = SceneStateType.None;
                });

            eventSystem.Receive<SceneCheckEvent>()
                .Subscribe(evt =>
                {
                    if (evt.SceneName == "Default")
                    {
                        Debug.Log("Scene Check Event to default preparing");
                        sceneStatus.State = SceneStateType.Preparing;
                    }
                })
                .AddTo(subscriptions);

            eventSystem.Receive<SceneCleanEvent>()
                .Subscribe(evt =>
                {
                    if (sceneStatus.State == SceneStateType.ShuttingDown)
                    {
                        sceneStatus.State = SceneStateType.Shutdown;
                    }
                })
                .AddTo(subscriptions);

            eventSystem.Receive<LoadingFinishedEvent>()
                .Subscribe(evt =>
                {
                    if (evt.TargetSceneName == "Default")
                    {
                        Debug.Log($"Loading Finished Event for {evt.TargetSceneName}");

                        sceneStatus.State = SceneStateType.Playing;
                    }
                })
                .AddTo(subscriptions);
        }

        public void StopSystem(IObservableGroup observableGroup)
        {
            subscriptions.DisposeAll();
        }

        void stateIn(SceneStateType state)
        {
            if (state == SceneStateType.None)
            {

            }
            else if (state == SceneStateType.Preparing)
            {
                Debug.Log("Scene State is preparing");

                sceneStatus.State = SceneStateType.Ready;
            }
            else if (state == SceneStateType.Ready)
            {
                Debug.Log("Scene State is ready");

                eventSystem.Publish(new SceneConfirmedEvent());
            }
            else if (state == SceneStateType.Playing)
            {
                Debug.Log("Scene State has been playing");
            }
            else if (state == SceneStateType.Shutdown)
            {
                eventSystem.Publish(new SceneCleanConfirmedEvent());

                sceneStatus.State = SceneStateType.None;
            }
        }

        void stateUpdate(long updateCount)
        {
        }
    }
}
