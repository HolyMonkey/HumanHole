using HumanHole.Scripts.Infrastructure.Services;

namespace HumanHole.Scripts.Infrastructure.States
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload1,TPayload2>(TPayload1 payload1, TPayload2 payload2) where TState : class, IPayloadedState<TPayload1,TPayload2>;
    }
}