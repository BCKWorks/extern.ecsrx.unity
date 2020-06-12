using BCKWorks.Engine.Defines;
using System;

namespace BCKWorks.Engine.Modules.Status
{
    public class SceneStatus : ISceneStatus, IDisposable
    {
        public SceneStatus()
        {
            StateRP = new SceneStateReactiveProperty(SceneStateType.None);
        }

        public void Dispose()
        {
            StateRP.Dispose();
        }

        public SceneStateType State
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

        public SceneStateReactiveProperty StateRP { get; private set; }
    }
}
