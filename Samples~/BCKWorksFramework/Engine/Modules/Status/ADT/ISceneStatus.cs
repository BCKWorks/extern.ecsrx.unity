using BCKWorks.Engine.Defines;

namespace BCKWorks.Engine.Modules.Status
{
    public interface ISceneStatus
    {
        SceneStateType State { get; set; }
        SceneStateReactiveProperty StateRP { get; }
    }
}
