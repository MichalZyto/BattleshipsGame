namespace ShipsGame.App.models
{
    public class ShipPart : IShipPart
    {
        public enum PartStatus
        {
            Damaged,
            Intact
        }

        private readonly IMapLocation _location;
        private readonly IBaseShip _baseShip;
        private PartStatus _status;

        public IMapLocation Location => _location;

        public PartStatus Status => _status;

        public IBaseShip Ship => _baseShip;

        public ShipPart(IBaseShip baseShip, IMapLocation location)
        {
            _baseShip = baseShip;
            _location = location;
            _status = PartStatus.Intact;
        }

        public bool Hit()
        {
            if (_status == PartStatus.Intact)
            {
                _status = PartStatus.Damaged;
                return true;
            }
            return false;
        }
    }
}