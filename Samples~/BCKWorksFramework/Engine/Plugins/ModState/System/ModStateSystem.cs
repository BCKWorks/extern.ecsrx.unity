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

namespace BCKWorks.Engine.Plugins.ModState.Systems
{
    public class ModStateSystem : IManualSystem
    {
        public IGroup Group => new EmptyGroup();

        private readonly IList<IDisposable> subscriptions = new List<IDisposable>();
        private readonly IModStatus modStatus;
        private readonly IEventSystem eventSystem;

        public ModStateSystem(IModStatus modStatus,
            IEventSystem eventSystem)
        {
            this.modStatus = modStatus;
            this.eventSystem = eventSystem;
        }

        public void StartSystem(IObservableGroup observableGroup)
        {
            modStatus.StateRP.DistinctUntilChanged().Subscribe(stateIn).AddTo(subscriptions);
            Observable.EveryUpdate().Subscribe(stateUpdate).AddTo(subscriptions);

            eventSystem.Receive<ModCheckEvent>()
                .Subscribe(evt =>
                {
                    modStatus.State = ModStateType.Preparing;
                })
                .AddTo(subscriptions);
        }

        public void StopSystem(IObservableGroup observableGroup)
        {
            subscriptions.DisposeAll();
        }

        void stateIn(ModStateType state)
        {
            if (state == ModStateType.None)
            {

            }
            else if (state == ModStateType.Preparing)
            {
                Debug.Log("Mod State is preparing");

                modStatus.State = ModStateType.Identifying;
            }
            else if (state == ModStateType.Identifying)
            {
                Debug.Log("Mod State is identifying");

                modStatus.State = ModStateType.Networking;
            }
            else if (state == ModStateType.Networking)
            {
                Debug.Log("Mod State is networking");

                modStatus.State = ModStateType.Ready;
            }
            else if (state == ModStateType.Ready)
            {
                Debug.Log("Mod State is ready");

                eventSystem.Publish(new ModConfirmedEvent());
            }
        }

        void stateUpdate(long updateCount)
        {
        }
    }
}
