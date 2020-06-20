using BCKWorks.Engine.Defines;
using System;

namespace BCKWorks.Engine.Modules.Status
{
    public class ModStatus : IModStatus, IDisposable
    {
        public ModStatus()
        {
            StateRP = new ModStateReactiveProperty(ModStateType.None);
        }

        public void Dispose()
        {
            StateRP.Dispose();
        }

        public ModStateType State
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

        public ModStateReactiveProperty StateRP { get; private set; }
    }
}
