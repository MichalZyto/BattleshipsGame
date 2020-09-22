using Moq;
using NUnit.Framework;
using ShipsGame.App.businessLogic;
using ShipsGame.App.models;
using System;
using Constants = ShipGame.Common.Constants;

namespace ShipGame.Test.App.businessLogic
{
    public class ShipBuilderTests
    {
        private Mock<IShipFactory> _shipFactoryMock;
        private Func<IMapLocation, bool> _collisionDetectionFunc;

        [SetUp]
        public void Setup()
        {
            _shipFactoryMock = new Mock<IShipFactory>();
            _shipFactoryMock.Setup(x => x.Get(It.IsAny<string>())).Returns<string>(x => new BattleShip(x));
            _collisionDetectionFunc = (IMapLocation mapLocation) => false;
        }

        [Test]
        public void CreateShouldReturnInstanceOfBuilder()
        {
            //act
            var builder = ShipBuilder.Create(_shipFactoryMock.Object);

            //assert
            Assert.IsNotNull(builder,
                $"Method {nameof(ShipBuilder.Create)} of class {nameof(ShipBuilder)} should return instance of {nameof(IShipBuilder)}");
        }

        [Test]
        public void BuildShouldReturnInstanceOfShip()
        {
            //arrange
            var builder = ShipBuilder.Create(_shipFactoryMock.Object);

            //act
            var ship = builder.Build(_collisionDetectionFunc);

            //assert
            Assert.IsNotNull(ship,
                $"Method {nameof(ShipBuilder.Build)} of class {nameof(ShipBuilder)} should return instance of {nameof(IBaseShip)}");
            Assert.AreEqual(Constants.BattleShipSize, (ship as BaseShip).Count,
                $"Method {nameof(ShipBuilder.Build)} of class {nameof(ShipBuilder)} should return collection of {Constants.BattleShipSize} {nameof(IShipPart)}s");
        }
    }
}