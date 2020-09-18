namespace ShipGame.UI
{
    public interface IUserInterface
    {
        /// <summary>
        /// Show current battle map
        /// </summary>
        void ShowBattleMap();

        /// <summary>
        /// Shows user introduction
        /// </summary>
        void ShowIntroduction();

        /// <summary>
        /// Show hit message
        /// </summary>
        /// <param name="shipName">Ship name that was hit</param>
        /// <param name="shipHealth">Ship health after hit</param>
        void ShowHitMessage(string shipName, int shipHealth);

        /// <summary>
        /// Show user missed count
        /// </summary>
        /// <param name="missedCount">Miss count</param>
        void ShowMissedCount(int missedCount);

        /// <summary>
        /// Shows summary wit miss count
        /// </summary>
        void ShowSummary(int missedCount);

        /// <summary>
        /// Shows user end credits
        /// </summary>
        void ShowEndCredits();

        /// <summary>
        /// Mark ship part location on map
        /// </summary>
        void MarkShipLocationOnMap(int x, int y);

        /// <summary>
        /// Mark ship damaged part location on map
        /// </summary>
        void MarkHitLocationOnMap(int x, int y);

        /// <summary>
        /// Mark miss location on map
        /// </summary>
        void MarkMissLocationOnMap(int x, int y);

        /// <summary>
        /// Get X and Y coordinates from user
        /// </summary>
        /// <returns>Return X and Y coordinates</returns>
        (int x, int y) GetUserCoordinates();

        /// <summary>
        /// Get user decision if he wants to restart the game
        /// </summary>
        /// <returns>Decision</returns>
        bool GetUserRestartGameDecision();

        /// <summary>
        /// Clear map
        /// </summary>
        void RestartMap();
    }
}