using System;

namespace ShipsGame.App.models
{
    public class MapLocation : IMapLocation
    {
        private readonly int _x, _y;

        public int X => _x;
        public int Y => _y;

        public MapLocation(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override bool Equals(Object obj)
        {
            if (obj is MapLocation location)
            {
                return (_x == location.X) && (_y == location.Y);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (_x << 2) ^ _y;
        }

        public override string ToString()
        {
            return $"{nameof(MapLocation)} X:{_x} Y:{_y}";
        }
    }
}