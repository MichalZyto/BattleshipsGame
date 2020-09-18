using ShipGame.Common;

namespace ShipsGame.App.models
{
    public class BattleShip : BaseShip
    {
        public BattleShip(string name) : base(name, Constants.BattleShipSize)
        {
        }
    }
}