using BCKWorks.Engine.Defines;
using System;

namespace BCKWorks.Engine.Modules.Status
{
    public class EngineStatus : IEngineStatus, IDisposable
    {
        public EngineStatus()
        {
            StateRP = new EngineStateReactiveProperty(EngineStateType.None);
        }

        public void Dispose()
        {
            StateRP.Dispose();
        }

        public EngineStateType State
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

        public EngineStateReactiveProperty StateRP { get; private set; }
    }
}
