using NUnit.Framework;
using ShipsGame.App.businessLogic;
using ShipsGame.App.models;
using Constants = ShipGame.Common.Constants;

namespace ShipGame.Test.App.businessLogic
{
    public class ShipFactoryTests
    {
        private readonly string _exampleShipName = "TestShip";
        private IShipFactory _shipFactory;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetWhenExpectedBattleshipShouldReturnInstanceOfBattleship()
        {
            //arrange
            _shipFactory = new ShipFactory(ShipFactory.ShipType.Battleship);

            //act
            var ship = _shipFactory.Get(_exampleShipName) as BattleShip;

            //assert
            Assert.IsNotNull(ship,
                $"Method {nameof(ShipFactory.Get)} of class {nameof(ShipFactory)} should return instance of {nameof(BattleShip)}");
            Assert.AreEqual(Constants.BattleShipSize, ship.Length,
                $"Method {nameof(ShipBuilder.Build)} of class {nameof(ShipBuilder)} should return ship length of {Constants.BattleShipSize}");
            Assert.AreEqual(_exampleShipName, ship.Name,
                $"Method {nameof(ShipBuilder.Build)} of class {nameof(ShipBuilder)} should return ship with name {_exampleShipName}");
        }

        [Test]
        public void GetWhenExpectedDestroyerShouldReturnInstanceOfDestroyer()
        {
            //arrange
            _shipFactory = new ShipFactory(ShipFactory.ShipType.Destroyer);

            //act
            var ship = _shipFactory.Get(_exampleShipName) as DestroyerShip;

            //assert
            Assert.IsNotNull(ship,
                $"Method {nameof(ShipFactory.Get)} of class {nameof(ShipFactory)} should return instance of {nameof(DestroyerShip)}");
            Assert.AreEqual(Constants.DestroyerShipSize, ship.Length,
                $"Method {nameof(ShipBuilder.Build)} of class {nameof(ShipBuilder)} should return ship length of {Constants.DestroyerShipSize}");
        }
    }
}