using ShipsGame.App.models;
using System;

namespace ShipsGame.App.buisnesLogic
{
    /// <summary>
    /// Instance of ship builder
    /// </summary>
    public interface IShipBuilder
    {
        /// <summary>
        /// Builds ship in random location
        /// <param name="collisionDetectionFunc">Collision detection function</param>
        /// </summary>
        IBaseShip Build(Func<IMapLocation, bool> collisionDetectionFunc);
    }
}