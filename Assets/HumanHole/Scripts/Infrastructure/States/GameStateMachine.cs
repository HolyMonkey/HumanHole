using System;
using System.Collections.Generic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;

public class GameStateMachine : IGameStateMachine
    {
        private  readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, AllServices services)
        {
            SceneLoader sceneLoader = new SceneLoader(coroutineRunner);
            
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, coroutineRunner, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain),
                [typeof(LoadProgressState)] = new LoadProgressState(this, 
                    services.Single<IPersistentProgressService>(), 
                    services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload1, TPayload2>(TPayload1 payload1,TPayload2 payload2) where TState : class, IPayloadedState<TPayload1,TPayload2>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload1, payload2);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            var result = _states[typeof(TState)] as TState;
            return result;
        }
    }