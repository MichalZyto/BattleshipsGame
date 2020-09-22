using ShipGame.Common;
using ShipsGame.App.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShipsGame.App.businessLogic
{
    public class ShipBuilder : IShipBuilder
    {
        public enum Direction
        {
            Horizontal,
            Vertical
        }

        private readonly IBaseShip _ship;

        private static readonly Random Randomiser = new Random();

        private static string GetRandomName => Constants.ExampleShipNames[Randomiser.Next(Constants.ExampleShipNames.Count)];

        private static Direction GetRandomDirection =>
            Randomiser.Next(Enum.GetNames(typeof(Direction)).Length) % 2 == 0
            ? Direction.Horizontal
            : Direction.Vertical;

        private List<MapLocation> GetRandomLocation(Func<IMapLocation, bool> collisionDetectionFunc)
        {
            var shipLocation = new List<MapLocation>();
            do
            {
                shipLocation.Clear();
                var direction = GetRandomDirection;
                if (direction == Direction.Horizontal)
                {
                    shipLocation.Add(new MapLocation(Randomiser.Next(Constants.MapSize - _ship.Length),
                                                     Randomiser.Next(Constants.MapSize)));
                }
                else
                {
                    shipLocation.Add(new MapLocation(Randomiser.Next(Constants.MapSize),
                                                     Randomiser.Next(Constants.MapSize - _ship.Length)));
                }
                for (int i = 1; i < _ship.Length; i++)
                {
                    if (direction == Direction.Horizontal)
                    {
                        shipLocation.Add(new MapLocation(shipLocation[0].X + i, shipLocation[0].Y));
                    }
                    else
                    {
                        shipLocation.Add(new MapLocation(shipLocation[0].X, shipLocation[0].Y + i));
                    }
                }
            }
            while (shipLocation.Any(x => collisionDetectionFunc.Invoke(x)));
            return shipLocation;
        }

        private ShipBuilder(IShipFactory shipFactory, string name)
        {
            _ship = shipFactory.Get(name);
        }

        private ShipBuilder(IShipFactory shipFactory) : this(shipFactory, GetRandomName)
        {
        }

        /// <summary>
        /// Creates new ship using <see cref="IShipFactory"/>
        /// </summary>
        /// <param name="shipFactory">Instance of ship factory</param>
        /// <returns>Instance of ship builder</returns>
        public static IShipBuilder Create(IShipFactory shipFactory) => new ShipBuilder(shipFactory);

        private IBaseShip SetRandomShipLocation(Func<IMapLocation, bool> collisionDetectionFunc)
        {
            GetRandomLocation(collisionDetectionFunc).ForEach(x => _ship.Add(new ShipPart(_ship, x)));
            return _ship;
        }

        public IBaseShip Build(Func<IMapLocation, bool> collisionDetectionFunc) => SetRandomShipLocation(collisionDetectionFunc);
    }
}