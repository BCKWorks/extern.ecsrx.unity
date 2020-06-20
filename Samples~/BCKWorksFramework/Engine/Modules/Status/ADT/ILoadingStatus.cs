using BCKWorks.Engine.Defines;

namespace BCKWorks.Engine.Modules.Status
{
    public interface ILoadingStatus
    {
        LoadingStateType State { get; set; }
        LoadingStateReactiveProperty StateRP { get; }
    }
}
