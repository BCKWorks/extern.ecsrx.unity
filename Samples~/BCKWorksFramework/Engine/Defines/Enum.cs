namespace BCKWorks.Engine.Defines
{
    public enum LoadingStateType
    {
        None,
        Preparing,
        SceneClean,
        SceneCleanConfirmed,
        EngineCheck,
        EngineConfirmed,
        ModCheck,
        ModConfirmed,
        SceneCheck,
        SceneConfirmed,
        Snapshot,
    }

    public enum EngineStateType
    {
        None,
        Preparing,
        Ready
    }

    public enum ModStateType
    {
        None,
        Preparing,
        Identifying,
        Networking,
        Ready
    }

    public enum SceneStateType
    {
        None,
        Preparing,
        Ready,
        Playing,
        ShuttingDown,
        Shutdown
    }
}
