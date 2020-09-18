using System.Collections.Generic;

namespace ShipGame.Common
{
    /// <summary>
    /// Game constants
    /// </summary>
    public static class Constants
    {
        public const int MapSize = 10;
        public const int BattleShipSize = 5;
        public const int BattleShipCount = 1;
        public const int DestroyerShipSize = 4;
        public const int DestroyerShipCount = 2;

        public const string DebugVersionMessage = "DEBUG VERSION";

        public const string IntroductionMessage = "+++ Wecome in BattleShip game +++";

        public const string EnterNewCoordinatesMessage = "Enter coordinates for new shot: ";

        public const string EnterRestartGameDecision = "Do you want to try again?(Y/N): ";

        public const string EndCreditsMessage = "Thank you for your time!\n+++ THE END +++";

        public static string SummaryMessage(int missedCount) =>
            $"Congratulations!\nYou have WON{(missedCount > 0 ? $" and you missed only {missedCount} times" : string.Empty)}.";

        public static string UserMissedMessage(int missedCount) =>
            $"You have missed your target {missedCount} times. Try again...";

        public static string ArgumentExceptionMessage(string nameOfInstance) =>
            $"Parameter must be set to instance of {nameOfInstance}.";

        public static string UserHitMessage(string shipName, int shipHealth) =>
            $"You scored a hit on '{shipName}' {(shipHealth == 0 ? "nice work the ship is sinking" : $"the ship is still floating with {shipHealth} parts intact")}.";

        public static string InvalidShipTypeException(string shipType) =>
            $"Unable to create {shipType} ship.";

        public static string UnhandledTriggerMessage(string state, string trigger) =>
            $"Unhandled: '{state}' state, '{trigger}' trigger!";

        public static List<string> ExampleShipNames = new List<string>
        {
            "Scharnhorst",
            "Gneisenau",
            "Bismarck",
            "Tirpitz",
            "SMS Bayern",
            "SMS Baden",
            "SMS Sachsen",
            "SMS Württemberg"
        };
    }
}