namespace ShipsGame.App.models
{
    /// <summary>
    /// Instance of location on the 2D plane
    /// </summary>
    public interface IMapLocation
    {
        /// <summary>
        /// X position
        /// </summary>
        int X { get; }

        /// <summary>
        /// Y position
        /// </summary>
        int Y { get; }

        /// <summary>
        /// Compares two locations
        /// </summary>
        /// <returns>If <see cref="X"/> and <see cref="Y"/> values are equal then return true</returns>
        bool Equals(object obj);
    }
}