using System;
using UniRx;

namespace BCKWorks.Engine.Defines
{
    [Serializable]
    public class ModStateReactiveProperty : ReactiveProperty<ModStateType>
    {
        public ModStateReactiveProperty()
            : base()
        {

        }

        public ModStateReactiveProperty(ModStateType initialValue)
            : base(initialValue)
        {

        }
    }
}
