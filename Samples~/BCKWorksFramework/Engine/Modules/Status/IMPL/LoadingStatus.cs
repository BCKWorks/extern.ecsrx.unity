using BCKWorks.Engine.Defines;
using System;

namespace BCKWorks.Engine.Modules.Status
{
    public class LoadingStatus : ILoadingStatus, IDisposable
    {
        public LoadingStatus()
        {
            StateRP = new LoadingStateReactiveProperty(LoadingStateType.None);
        }

        public void Dispose()
        {
            StateRP.Dispose();
        }

        public LoadingStateType State
        {
            get
            {
                return StateRP.Value;
            }
            set
            {
                StateRP.Value = value;
            }
        }

        public LoadingStateReactiveProperty StateRP { get; private set; }
    }
}
