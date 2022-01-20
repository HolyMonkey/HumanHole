using CodeBase.Infrastructure;

public class Game
{
    public static Game Instance { get; private set; }
    public GameStateMachine StateMachine { get; private set; }
    public AllServices AllServices { get; private set; }
    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
    {
        Instance ??= this;
        
        AllServices = new AllServices();
        StateMachine = new GameStateMachine(coroutineRunner, curtain, AllServices);
    }
}