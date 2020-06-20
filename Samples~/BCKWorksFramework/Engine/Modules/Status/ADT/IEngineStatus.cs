using BCKWorks.Engine.Defines;

namespace BCKWorks.Engine.Modules.Status
{
    public interface IEngineStatus
    {
        EngineStateType State { get; set; }
        EngineStateReactiveProperty StateRP { get; }
    }
}
