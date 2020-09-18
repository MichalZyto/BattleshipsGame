namespace ShipsGame.App.buisnesLogic
{
    /// <summary>
    /// Instance for game state machine
    /// </summary>
    public interface IGameMachine
    {
        /// <summary>
        /// Initiates gameloop by firing Trigger.Prepare
        /// </summary>
        void RunGameTrigger();
    }
}