namespace ShipsGame.App.businessLogic
{
    /// <summary>
    /// Instance of game logic
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// Checks if there are still more enemy ships to shoot
        /// </summary>
        /// <returns>Returns true as long as there are enemy ships on the map</returns>
        bool EnemyTargesPresent();

        /// <summary>
        /// Get X and Y coordinates from user
        /// </summary>
        /// <returns>Return X and Y coordinates</returns>
        (int X, int Y) GetCoordinates();

        /// <summary>
        /// Get user decision if he wants to restart the game
        /// </summary>
        /// <returns>Decision</returns>
        bool GetUserRestartGameDecision();

        /// <summary>
        /// Game initialisation
        /// </summary>
        void Initialization();

        /// <summary>
        /// Indicates to that the shot was a miss
        /// </summary>
        void Missed();

        /// <summary>
        /// Shot is fired on specific location
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <returns>Return true if it hits</returns>
        bool Shoot(int x, int y);

        /// <summary>
        /// Shows user end credits
        /// </summary>
        void ShowEndCredits();

        /// <summary>
        /// Shows user game introduction
        /// </summary>
        void ShowIntroduction();

        /// <summary>
        /// Shows summary
        /// </summary>
        void ShowSummary();
    }
}