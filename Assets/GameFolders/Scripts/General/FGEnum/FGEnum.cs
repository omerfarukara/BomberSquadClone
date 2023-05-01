
namespace GameFolders.Scripts.General.FGEnum
{
    public enum GameState
    {
        TakeOff,
        Idle,
        Play,
        Finish,
        Lose
    }

    public enum PlaneState
    {
        OnRunaway,
        OnFly
    }

    public enum DifficultyLevel
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
    }
    
    public enum ValueType
    {
        Constant,
        Range
    }

    public enum BorderType
    {
        Rectangle,
        Circle,
        HallowCircle
    }

    public enum OrderType
    {
        Ordered,
        Random
    }
}
