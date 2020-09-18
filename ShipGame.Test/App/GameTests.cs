using Moq;
using NUnit.Framework;
using ShipsGame.App;
using ShipsGame.App.buisnesLogic;

namespace ShipGame.Test.App
{
    public class GameTests
    {
        private IGame _game;
        private Mock<IGameMachine> _gameMachineMock;

        [SetUp]
        public void Setup()
        {
            _gameMachineMock = new Mock<IGameMachine>();
            _game = new Game(_gameMachineMock.Object);
        }

        [Test]
        public void GameRunShouldCallRunGameTrigger()
        {
            //act
            _game.Run();

            //assert
            _gameMachineMock.Verify(x => x.RunGameTrigger(), Times.Once,
                $"Method {nameof(IGame.Run)} of interface {nameof(IGame)} should call {nameof(IGameMachine.RunGameTrigger)} from interface {nameof(IGameMachine)}");
        }
    }
}