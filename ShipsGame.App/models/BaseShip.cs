using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShipsGame.App.models
{
    public abstract class BaseShip : Collection<IShipPart>, IBaseShip
    {
        private readonly string _name;
        private readonly int _length;

        public string Name => _name;

        public int Length => _length;

        public int Health => (new List<IShipPart>(this))
                                .Where(x => x.Status == ShipPart.PartStatus.Intact)
                                .Count();

        public BaseShip(string name, int shipLength)
        {
            _name = name;
            _length = shipLength;
        }
    }
}