using ShipGame.Common;
using ShipsGame.App.buisnesLogic;
using System;

namespace ShipsGame.App
{
    public class Game : IGame
    {
        private readonly IGameMachine _gameMachine;

        public Game(IGameMachine gameMachine)
        {
            _gameMachine = gameMachine ?? throw new ArgumentException(
                                            Constants.ArgumentExceptionMessage(nameof(IGameMachine)),
                                            nameof(gameMachine));
        }

        public void Run()
        {
            _gameMachine.RunGameTrigger();
        }
    }
}