using Moq;
using NUnit.Framework;
using ShipGame.UI;
using ShipsGame.App.buisnesLogic;

namespace ShipGame.Test.App.buisnesLogic
{
    public class GameLogicTests
    {
        private readonly int _exampleX = 2;
        private readonly int _exampleY = 5;
        private IGameLogic _gameLogic;
        private Mock<IUserInterface> _userInterfaceMock;

        [SetUp]
        public void Setup()
        {
            _userInterfaceMock = new Mock<IUserInterface>();
            _userInterfaceMock.Setup(x => x.RestartMap());
            _userInterfaceMock.Setup(x => x.MarkShipLocationOnMap(It.IsAny<int>(), It.IsAny<int>()));
            _userInterfaceMock.Setup(x => x.ShowBattleMap(It.IsAny<bool>()));
            _userInterfaceMock.Setup(x => x.GetUserCoordinates()).Returns((_exampleX, _exampleY));
            _userInterfaceMock.Setup(x => x.GetUserRestartGameDecision()).Returns(true);
            _userInterfaceMock.Setup(x => x.ShowMissedCount(It.IsAny<int>()));
            _gameLogic = new GameLogic(_userInterfaceMock.Object);
            _gameLogic.Initialization();
        }

        [Test]
        public void EnemyTargesPresentShouldReturnTrue()
        {
            //act
            var result = _gameLogic.EnemyTargesPresent();

            //assert
            Assert.IsTrue(result, $"Method {nameof(IGameLogic.EnemyTargesPresent)} of interface {nameof(IGameLogic)} should return true");
        }

        [Test]
        public void GetCoordinatesShouldReturnXY()
        {
            //act
            var result = _gameLogic.GetCoordinates();

            //assert
            Assert.IsNotNull(result, $"Method {nameof(IGameLogic.GetCoordinates)} of interface {nameof(IGameLogic)} should return coordinates X and Y");
            Assert.AreEqual(_exampleX, result.X, $"Method {nameof(IGameLogic.GetCoordinates)} of interface {nameof(IGameLogic)} should return coordinates X equal to {_exampleX}");
            Assert.AreEqual(_exampleY, result.Y, $"Method {nameof(IGameLogic.GetCoordinates)} of interface {nameof(IGameLogic)} should return coordinates Y equal to {_exampleY}");
            _userInterfaceMock.Verify(x => x.ShowBattleMap(false), Times.Once,
                $"Method {nameof(IGameLogic.GetCoordinates)} of interface {nameof(IGameLogic)} should call {nameof(IUserInterface.ShowBattleMap)} method from interface {nameof(IUserInterface)} with default parameter false");
        }

        [Test]
        public void GetUserRestartGameDecisionShouldReturnDecision()
        {
            //act
            var result = _gameLogic.GetUserRestartGameDecision();

            //assert
            Assert.IsTrue(result, $"Method {nameof(IGameLogic.GetUserRestartGameDecision)} of interface {nameof(IGameLogic)} should return decision value true");
        }

        [Test]
        public void MissedShouldCallShowMissedCountWithCorrectParameter()
        {
            //act
            _gameLogic.Missed();

            //assert
            _userInterfaceMock.Verify(x => x.ShowMissedCount(1), Times.Once,
                $"Method {nameof(IGameLogic.Missed)} of interface {nameof(IGameLogic)} should call {nameof(IUserInterface.ShowMissedCount)} method from interface {nameof(IUserInterface)} with parameter 1");
        }

        [Test]
        public void ShowEndCreditsShouldCallShowEndCredits()
        {
            //act
            _gameLogic.ShowEndCredits();

            //assert
            _userInterfaceMock.Verify(x => x.ShowEndCredits(), Times.Once,
                $"Method {nameof(IGameLogic.ShowEndCredits)} of interface {nameof(IGameLogic)} should call {nameof(IUserInterface.ShowEndCredits)} from interface {nameof(IUserInterface)}");
        }

        [Test]
        public void ShowIntroductionShouldCallShowIntroduction()
        {
            //act
            _gameLogic.ShowIntroduction();

            //assert
            _userInterfaceMock.Verify(x => x.ShowIntroduction(), Times.Once,
                $"Method {nameof(IGameLogic.ShowIntroduction)} of interface {nameof(IGameLogic)} should call {nameof(IUserInterface.ShowIntroduction)} from interface {nameof(IUserInterface)}");
        }

        [Test]
        public void ShowSummaryShouldCallShowSummary()
        {
            //act
            _gameLogic.ShowSummary();

            //assert
            _userInterfaceMock.Verify(x => x.ShowSummary(It.IsAny<int>()), Times.Once,
                $"Method {nameof(IGameLogic.ShowSummary)} of interface {nameof(IGameLogic)} should call {nameof(IUserInterface.ShowSummary)} from interface {nameof(IUserInterface)}");
        }
    }
}