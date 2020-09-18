using ShipGame.Common;
using Stateless;
using System;

namespace ShipsGame.App.buisnesLogic
{
    public class GameMachine : IGameMachine
    {
        public enum Trigger
        {
            Prepare,
            Ready,
            Miss,
            Hit,
            Score,
            Finish
        }

        public enum State
        {
            New,
            Initialize,
            ReadyToAttack,
            MissTarget,
            HitTarget,
            Summary,
            End
        }

        private readonly IGameLogic _gameLogic;

        #region State Machine Setting

        private readonly StateMachine<State, Trigger> _stateMachine;

        public GameMachine(IGameLogic gameLogic)
        {
            _gameLogic = gameLogic ?? throw new ArgumentException(
                                        Constants.ArgumentExceptionMessage(nameof(IGameLogic)),
                                        nameof(gameLogic));

            _stateMachine = SetCreateStateMachine();
        }

        public void RunGameTrigger() => _stateMachine.Fire(Trigger.Prepare);

        private void ReadyToShootTrigger() => _stateMachine.Fire(Trigger.Ready);

        private void MissedAShotTrigger() => _stateMachine.Fire(Trigger.Miss);

        private void ShipHitTrigger() => _stateMachine.Fire(Trigger.Hit);

        private void GameScoreTrigger() => _stateMachine.Fire(Trigger.Score);

        private void FinishGameTrigger() => _stateMachine.Fire(Trigger.Finish);

        private StateMachine<State, Trigger> SetCreateStateMachine()
        {
            StateMachine<State, Trigger> stateMachine = new StateMachine<State, Trigger>(State.New);
            stateMachine.Configure(State.New)
                .OnExit(() => _gameLogic.ShowIntroduction())
                .Permit(Trigger.Prepare, State.Initialize);

            stateMachine.Configure(State.Initialize)
                .OnEntry(s => Initialization())
                .Permit(Trigger.Ready, State.ReadyToAttack);

            stateMachine.Configure(State.ReadyToAttack)
                .OnEntry(s => ReadAttackCoordinates())
                .Permit(Trigger.Hit, State.HitTarget)
                .Permit(Trigger.Miss, State.MissTarget);

            stateMachine.Configure(State.MissTarget)
                .OnEntry(s => AfterMissed())
                .Permit(Trigger.Ready, State.ReadyToAttack)
                .Permit(Trigger.Finish, State.Summary);

            stateMachine.Configure(State.HitTarget)
                .OnEntry(s => AfterHit())
                .Permit(Trigger.Ready, State.ReadyToAttack)
                .Permit(Trigger.Score, State.Summary);

            stateMachine.Configure(State.Summary)
                .OnEntry(s => ShowSummary())
                .Permit(Trigger.Finish, State.End)
                .Permit(Trigger.Prepare, State.Initialize);

            stateMachine.Configure(State.End)
                .OnEntry(s => _gameLogic.ShowEndCredits());

            stateMachine.OnUnhandledTrigger((state, trigger) => Constants.UnhandledTriggerMessage(state.ToString(), trigger.ToString()));

            return stateMachine;
        }

        #endregion State Machine Setting

        private void Initialization()
        {
            _gameLogic.Initialization();
            ReadyToShootTrigger();
        }

        private void ReadAttackCoordinates()
        {
            var (X, Y) = _gameLogic.GetCoordinates();
            if (_gameLogic.Shoot(X, Y))
            {
                ShipHitTrigger();
            }
            else
            {
                MissedAShotTrigger();
            }
        }

        private void AfterHit()
        {
            if (_gameLogic.EnemyTargesPresent())
            {
                ReadyToShootTrigger();
            }
            else
            {
                GameScoreTrigger();
            }
        }

        private void AfterMissed()
        {
            _gameLogic.Missed();
            ReadyToShootTrigger();
        }

        private void ShowSummary()
        {
            _gameLogic.ShowSummary();
            if (_gameLogic.GetUserRestartGameDecision())
            {
                RunGameTrigger();
            }
            else
            {
                FinishGameTrigger();
            }
        }
    }
}