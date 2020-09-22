using ShipsGame.App.models;

namespace ShipsGame.App.businessLogic
{
    /// <summary>
    /// Instance of ship factory
    /// </summary>
    public interface IShipFactory
    {
        /// <summary>
        /// Creates new ship instance
        /// </summary>
        IBaseShip Get(string name);
    }
}