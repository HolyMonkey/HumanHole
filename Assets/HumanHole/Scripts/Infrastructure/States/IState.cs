namespace HumanHole.Scripts.Infrastructure.States
{
    public interface IExitableState
    {
        void Exit();
    }

    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayload1,TPayload2> : IExitableState
    {
        void Enter(TPayload1 payload1, TPayload2 payload2);
    }
}