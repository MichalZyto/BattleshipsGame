namespace ShipsGame.App
{
    /// <summary>
    /// Main BattleShip game instance
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Initiates gameloop by calling Trigger.Prepare
        /// </summary>
        void Run();
    }
}