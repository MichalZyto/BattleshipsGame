using Moq;
using NUnit.Framework;
using ShipsGame.App.buisnesLogic;

namespace ShipGame.Test.App.buisnesLogic
{
    public class GameMachineTests
    {
        private readonly int _exampleX = 2;
        private readonly int _exampleY = 5;
        private IGameMachine _gameMachine;
        private Mock<IGameLogic> _gameLogicMock;
        private bool _shootWasAHit, _enemyAlive, _userDecision;

        public bool ToggleHit()
        {
            _shootWasAHit = !_shootWasAHit;
            return _shootWasAHit;
        }

        public bool ToggleEnemy()
        {
            _enemyAlive = !_enemyAlive;
            return _enemyAlive;
        }

        public bool ToggleDecision()
        {
            _userDecision = !_userDecision;
            return _userDecision;
        }

        [SetUp]
        public void Setup()
        {
            _gameLogicMock = new Mock<IGameLogic>();
            _gameLogicMock.Setup(x => x.ShowIntroduction());
            _gameLogicMock.Setup(x => x.Initialization());
            _gameLogicMock.Setup(x => x.GetCoordinates()).Returns((_exampleX, _exampleY));
            _shootWasAHit = true;
            _gameLogicMock.Setup(x => x.Shoot(It.Is<int>(x => x == _exampleX), It.Is<int>(x => x == _exampleY)))
                .Returns(() => ToggleHit());
            _gameLogicMock.Setup(x => x.Missed());
            _gameLogicMock.Setup(x => x.EnemyTargesPresent()).Returns(() => ToggleEnemy());
            _gameLogicMock.Setup(x => x.ShowSummary());
            _gameLogicMock.Setup(x => x.GetUserRestartGameDecision()).Returns(() => ToggleDecision());
            _gameLogicMock.Setup(x => x.ShowEndCredits());
            _gameMachine = new GameMachine(_gameLogicMock.Object);
        }

        [Test]
        public void RunGameTriggerShouldIterateThruEntireStateMachine()
        {
            //act
            _gameMachine.RunGameTrigger();

            //assert
            _gameLogicMock.Verify(x => x.ShowIntroduction(), Times.Once,
                $"Method {nameof(IGameLogic.ShowIntroduction)} of interface {nameof(IGameLogic)} should be called 1 time");
            _gameLogicMock.Verify(x => x.Initialization(), Times.Exactly(2),
                $"Method {nameof(IGameLogic.Initialization)} of interface {nameof(IGameLogic)} should be called 2 times");
            _gameLogicMock.Verify(x => x.GetCoordinates(), Times.Exactly(8),
                $"Method {nameof(IGameLogic.GetCoordinates)} of interface {nameof(IGameLogic)} should be called 8 times");
            _gameLogicMock.Verify(x => x.Shoot(It.Is<int>(x => x == _exampleX), It.Is<int>(x => x == _exampleY)), Times.Exactly(8),
                $"Method {nameof(IGameLogic.Shoot)} of interface {nameof(IGameLogic)} should be called 8 times");
            _gameLogicMock.Verify(x => x.EnemyTargesPresent(), Times.Exactly(4),
                $"Method {nameof(IGameLogic.EnemyTargesPresent)} of interface {nameof(IGameLogic)} should be called 4 times");
            _gameLogicMock.Verify(x => x.GetUserRestartGameDecision(), Times.Exactly(2),
                $"Method {nameof(IGameLogic.GetUserRestartGameDecision)} of interface {nameof(IGameLogic)} should be called 2 times");
            _gameLogicMock.Verify(x => x.ShowEndCredits(), Times.Once,
                $"Method {nameof(IGameLogic.ShowEndCredits)} of interface {nameof(IGameLogic)} should be called 1 time");
        }
    }
}