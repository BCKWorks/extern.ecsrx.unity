using BCKWorks.Engine.Defines;

namespace BCKWorks.Engine.Modules.Status
{
    public interface IModStatus
    {
        ModStateType State { get; set; }
        ModStateReactiveProperty StateRP { get; }
    }
}
