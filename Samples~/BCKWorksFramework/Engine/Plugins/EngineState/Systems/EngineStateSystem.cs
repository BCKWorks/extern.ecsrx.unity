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

namespace BCKWorks.Engine.Plugins.EngineState.Systems
{
    public class EngineStateSystem : IManualSystem
    {
        public IGroup Group => new EmptyGroup();

        private readonly IList<IDisposable> subscriptions = new List<IDisposable>();
        private readonly IEngineStatus engineStatus;
        private readonly IEventSystem eventSystem;

        public EngineStateSystem(IEngineStatus engineStatus,
            IEventSystem eventSystem)
        {
            this.engineStatus = engineStatus;
            this.eventSystem = eventSystem;
        }

        public void StartSystem(IObservableGroup observableGroup)
        {
            engineStatus.StateRP.DistinctUntilChanged().Subscribe(stateIn).AddTo(subscriptions);
            Observable.EveryUpdate().Subscribe(stateUpdate).AddTo(subscriptions);

            eventSystem.Receive<EngineCheckEvent>()
                .Subscribe(evt =>
                {
                    engineStatus.State = EngineStateType.Preparing;
                })
                .AddTo(subscriptions);

        }

        public void StopSystem(IObservableGroup observableGroup)
        {
            subscriptions.DisposeAll();
        }

        void stateIn(EngineStateType state)
        {
            if (state == EngineStateType.None)
            {

            }
            else if (state == EngineStateType.Preparing)
            {
                Debug.Log("Engine State is preparing");

                engineStatus.State = EngineStateType.Ready;
            }
            else if (state == EngineStateType.Ready)
            {
                Debug.Log("Engine State is ready");

                eventSystem.Publish(new EngineConfirmedEvent());
            }
        }

        void stateUpdate(long updateCount)
        {
        }
    }
}
