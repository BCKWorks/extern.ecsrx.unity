using BCKWorks.Engine.Defines;
using BCKWorks.Engine.Events;
using BCKWorks.Engine.Modules.Status;
using EcsRx.Events;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Groups.Observable;
using EcsRx.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BCKWorks.Engine.Plugins.LoadingState.Systems
{
    public class LoadingStateSystem : IManualSystem
    {
        public IGroup Group => new EmptyGroup();

        private readonly IList<IDisposable> subscriptions = new List<IDisposable>();
        private readonly ILoadingStatus loadingStatus;
        private readonly IEventSystem eventSystem;
        string changeSceneToName = "";

        public LoadingStateSystem(ILoadingStatus loadingStatus,
            IEventSystem eventSystem)
        {
            this.loadingStatus = loadingStatus;
            this.eventSystem = eventSystem;
        }

        public void StartSystem(IObservableGroup observableGroup)
        {
            loadingStatus.StateRP.DistinctUntilChanged().Subscribe(stateIn).AddTo(subscriptions);
            Observable.EveryUpdate().Subscribe(stateUpdate).AddTo(subscriptions);

            eventSystem.Receive<LoadingPreparingEvent>()
                .Subscribe(evt =>
                {
                    changeSceneToName = evt.ChangeToSceneName;
                    Debug.Log($"Change scene event {changeSceneToName}");
                    loadingStatus.State = LoadingStateType.Preparing;
                })
                .AddTo(subscriptions);

            eventSystem.Receive<SceneCleanConfirmedEvent>()
                .Subscribe(evt =>
                {
                    loadingStatus.State = LoadingStateType.SceneCleanConfirmed;
                })
                .AddTo(subscriptions);

            eventSystem.Receive<EngineConfirmedEvent>()
                .Subscribe(evt =>
                {
                    loadingStatus.State = LoadingStateType.EngineConfirmed;
                })
                .AddTo(subscriptions);

            eventSystem.Receive<ModConfirmedEvent>()
                .Subscribe(evt =>
                {
                    loadingStatus.State = LoadingStateType.ModConfirmed;
                })
                .AddTo(subscriptions);

            eventSystem.Receive<SceneConfirmedEvent>()
                .Subscribe(evt =>
                {
                    loadingStatus.State = LoadingStateType.SceneConfirmed;
                })
                .AddTo(subscriptions);
        }

        public void StopSystem(IObservableGroup observableGroup)
        {
            subscriptions.DisposeAll();
        }

        float stateInSnapshot;

        void stateIn(LoadingStateType state)
        {
            stateInSnapshot = Time.realtimeSinceStartup;
            if (state == LoadingStateType.None)
            {
            }
            else if (state == LoadingStateType.Preparing)
            {
                if (string.IsNullOrEmpty(changeSceneToName))
                {
                    loadingStatus.State = LoadingStateType.EngineCheck;
                }
                else
                {
                    loadingStatus.State = LoadingStateType.SceneClean;
                }
            }
            else if (state == LoadingStateType.SceneClean)
            {
                eventSystem.Publish(new SceneCleanEvent());
            }
            else if (state == LoadingStateType.SceneCleanConfirmed)
            {
                loadingStatus.State = LoadingStateType.EngineCheck;
            }
            else if (state == LoadingStateType.EngineCheck)
            {
                eventSystem.Publish(new EngineCheckEvent());
            }
            else if (state == LoadingStateType.EngineConfirmed)
            {
                loadingStatus.State = LoadingStateType.ModCheck;
            }
            else if (state == LoadingStateType.ModCheck)
            {
                eventSystem.Publish(new ModCheckEvent());
            }
            else if (state == LoadingStateType.ModConfirmed)
            {
                loadingStatus.State = LoadingStateType.SceneCheck;
            }
            else if (state == LoadingStateType.SceneCheck)
            {
                if (!string.IsNullOrEmpty(changeSceneToName))
                {
                    MainThreadDispatcher.StartCoroutine(loadSceneCO());
                }
                else
                {
                    changeSceneToName = SceneManager.GetActiveScene().name;
                    eventSystem.Publish(new SceneCheckEvent() { SceneName = changeSceneToName });
                }
            }
            else if (state == LoadingStateType.SceneConfirmed)
            {
            }
            else if (state == LoadingStateType.Snapshot)
            {
            }
        }

        IEnumerator loadSceneCO()
        {
            yield return null;

            string sceneName = changeSceneToName;

            //Begin to load the Scene you specify
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            //Don't let the Scene activate until you allow it to
            asyncOperation.allowSceneActivation = false;
            //When the load is still in progress, output the Text and progress bar
            while (!asyncOperation.isDone)
            {
                // Check if the load has finished
                if (asyncOperation.allowSceneActivation == false && asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }

            while (SceneManager.GetActiveScene().name != sceneName)
                yield return null;

            eventSystem.Publish(new SceneCheckEvent() { SceneName = sceneName });
        }

        void stateUpdate(long updateCount)
        {
            if (loadingStatus.State == LoadingStateType.SceneConfirmed)
            {
                var elapsed = Time.realtimeSinceStartup - stateInSnapshot;
                if (elapsed > 1)
                {
                    loadingStatus.State = LoadingStateType.Snapshot;
                }
            }
        }
    }
}
