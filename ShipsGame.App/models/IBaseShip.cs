namespace ShipsGame.App.models
{
    /// <summary>
    /// Instance of Ship in the game
    /// </summary>
    public interface IBaseShip
    {
        /// <summary>
        /// Current ship health (how many components are intact)
        /// </summary>
        int Health { get; }

        /// <summary>
        /// Ship estimated length
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Ship name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Include component as an integral part of this ship
        /// </summary>
        void Add(IShipPart shipPart);
    }
}