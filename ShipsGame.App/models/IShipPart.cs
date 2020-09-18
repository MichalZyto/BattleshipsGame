using static ShipsGame.App.models.ShipPart;

namespace ShipsGame.App.models
{
    /// <summary>
    /// Instance of ship component
    /// </summary>
    public interface IShipPart
    {
        /// <summary>
        /// Return this particular ship component <see cref="IMapLocation"/>
        /// </summary>
        IMapLocation Location { get; }

        /// <summary>
        /// Return <see cref="IBaseShip"/> reference to which this particular component belongs
        /// </summary>
        IBaseShip Ship { get; }

        /// <summary>
        /// Return current component <see cref="PartStatus"/>
        /// </summary>
        PartStatus Status { get; }

        /// <summary>
        /// Method called when this particular component takes fire
        /// </summary>
        /// <returns>If components takes damage it return true</returns>
        bool Hit();
    }
}