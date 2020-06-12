using System;
using UniRx;

namespace BCKWorks.Engine.Defines
{
    [Serializable]
    public class EngineStateReactiveProperty : ReactiveProperty<EngineStateType>
    {
        public EngineStateReactiveProperty()
            : base()
        {

        }

        public EngineStateReactiveProperty(EngineStateType initialValue)
            : base(initialValue)
        {

        }
    }
}
