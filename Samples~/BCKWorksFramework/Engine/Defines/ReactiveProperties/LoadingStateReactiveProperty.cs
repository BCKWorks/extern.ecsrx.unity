using System;
using UniRx;

namespace BCKWorks.Engine.Defines
{
    [Serializable]
    public class LoadingStateReactiveProperty : ReactiveProperty<LoadingStateType>
    {
        public LoadingStateReactiveProperty()
            : base()
        {

        }

        public LoadingStateReactiveProperty(LoadingStateType initialValue)
            : base(initialValue)
        {

        }
    }
}
