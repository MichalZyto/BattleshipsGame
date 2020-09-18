using ShipGame.Common;
using ShipGame.UI;
using ShipsGame.App.models;
using System.Collections.Generic;
using System.Linq;
using static ShipsGame.App.buisnesLogic.ShipFactory;
using static ShipsGame.App.models.ShipPart;

namespace ShipsGame.App.buisnesLogic
{
    public class GameLogic : IGameLogic
    {
        private readonly IUserInterface _userInterface;
        private readonly List<IBaseShip> _enemyFleet;

        private int _currentMissCounter;

        private void ResetFleet() => _enemyFleet.Clear();

        private void AddToFleet(IBaseShip ship) => _enemyFleet.Add(ship);

        private void RestartMissCounterAndMap()
        {
            _currentMissCounter = 0;
            _userInterface.RestartMap();
        }

        public GameLogic(IUserInterface userInterface)
        {
            _userInterface = userInterface;
            _enemyFleet = new List<IBaseShip>();
        }

        private void CreateEnemyFleet()
        {
            ResetFleet();
            CreateAndAddShipTypeToFleet(ShipType.Battleship, Constants.BattleShipCount);
            CreateAndAddShipTypeToFleet(ShipType.Destroyer, Constants.DestroyerShipCount);
#if DEBUG
            _userInterface.ShowBattleMap();
#endif
        }

        private void CreateAndAddShipTypeToFleet(ShipType shipType, int shipCount)
        {
            var shipFactory = new ShipFactory(shipType);
            for (int i = 0; i < shipCount; i++)
            {
                CreateAndAddShipToFleet(shipFactory);
            }
        }

        private void CreateAndAddShipToFleet(IShipFactory shipFactory)
        {
            var createdShip = ShipBuilder.Create(shipFactory).Build((location) => CollisionDetection(location) != null);
            AddToFleet(createdShip);
            AddShipLocationToUi(createdShip);
        }

        private IShipPart CollisionDetection(IMapLocation location)
        {
            return _enemyFleet.Select(x => new List<IShipPart>(x as BaseShip))
                                                    .FirstOrDefault(x => x.Any(y => y.Location.Equals(location)))?
                                                    .FirstOrDefault(x => x.Location.Equals(location));
        }

        private void AddShipLocationToUi(IBaseShip ship)
        {
            var shipParts = new List<IShipPart>((ship as BaseShip));
            shipParts.ForEach(x => AddShipPartLocation(x));

            void AddShipPartLocation(IShipPart shipPart)
            {
                AddNewLocationToUi(shipPart.Location, shipPart.Status);
            }
        }

        private void AddNewLocationToUi(IMapLocation mapLocationm, PartStatus? partStatus)
        {
            if (partStatus.HasValue)
            {
                if (partStatus == PartStatus.Intact)
                {
                    _userInterface.MarkShipLocationOnMap(mapLocationm.X, mapLocationm.Y);
                }
                else
                {
                    _userInterface.MarkHitLocationOnMap(mapLocationm.X, mapLocationm.Y);
                }
            }
            else
            {
                _userInterface.MarkMissLocationOnMap(mapLocationm.X, mapLocationm.Y);
            }
        }

        public void Initialization()
        {
            RestartMissCounterAndMap();
            CreateEnemyFleet();
        }

        public bool Shoot(int x, int y)
        {
            var mapLocation = new MapLocation(x, y);
            var shipPart = CollisionDetection(mapLocation);
            var result = false;
            if (shipPart?.Hit() == true)
            {
                _userInterface.ShowHitMessage(shipPart.Ship.Name, shipPart.Ship.Health);
                result = true;
            }
            AddNewLocationToUi(mapLocation, shipPart?.Status);
            return result;
        }

        public void Missed()
        {
#if DEBUG
            _userInterface.ShowBattleMap();
#endif
            _userInterface.ShowMissedCount(++_currentMissCounter);
        }

        public bool EnemyTargesPresent()
        {
#if DEBUG
            _userInterface.ShowBattleMap();
#endif
            return _enemyFleet.Count() > 0
                    && _enemyFleet.Any(x => (new List<IShipPart>((x as BaseShip)))
                                             .Any(y => y.Status == PartStatus.Intact));
        }

        public void ShowIntroduction() => _userInterface.ShowIntroduction();

        public void ShowSummary()
        {
            _userInterface.ShowSummary(_currentMissCounter);
        }

        public void ShowEndCredits() => _userInterface.ShowEndCredits();

        public (int X, int Y) GetCoordinates() => _userInterface.GetUserCoordinates();

        public bool GetUserRestartGameDecision() => _userInterface.GetUserRestartGameDecision();
    }
}