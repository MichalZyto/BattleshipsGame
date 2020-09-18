using ShipGame.Common;
using ShipsGame.App.models;
using System;

namespace ShipsGame.App.buisnesLogic
{
    public class ShipFactory : IShipFactory
    {
        public enum ShipType
        {
            Battleship,
            Destroyer
        }

        private readonly ShipType _shipType;

        public ShipFactory(ShipType shipType)
        {
            _shipType = shipType;
        }

        public IBaseShip Get(string name)
        {
            return _shipType switch
            {
                ShipType.Battleship => new BattleShip(name),
                ShipType.Destroyer => new DestroyerShip(name),
                _ => throw new InvalidOperationException(Constants.InvalidShipTypeException(_shipType.ToString())),
            };
        }
    }
}