using System;
using UniRx;

namespace BCKWorks.Engine.Defines
{
    [Serializable]
    public class SceneStateReactiveProperty : ReactiveProperty<SceneStateType>
    {
        public SceneStateReactiveProperty()
            : base()
        {

        }

        public SceneStateReactiveProperty(SceneStateType initialValue)
            : base(initialValue)
        {

        }
    }
}
