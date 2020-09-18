using ShipGame.Common;

namespace ShipsGame.App.models
{
    public class DestroyerShip : BaseShip
    {
        public DestroyerShip(string name) : base(name, Constants.DestroyerShipSize)
        {
        }
    }
}