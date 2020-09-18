using ShipGame.Common;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ShipGame.UI
{
    public class ConsoleUI : IUserInterface
    {
        private readonly Regex _regex = new Regex(@"([a-zA-Z]+)(\d+)");
        private char[,] _battleMap;

        public ConsoleUI()
        {
            RestartMap();
        }

        [Conditional("DEBUG")]
        private void DebugVersion() => Console.WriteLine(Constants.DebugVersionMessage);

        public void ShowIntroduction()
        {
            DebugVersion();
            Console.WriteLine(Constants.IntroductionMessage);
        }

        public void ShowHitMessage(string shipName, int shipHealth)
        {
            Console.WriteLine(Constants.UserHitMessage(shipName, shipHealth));
        }

        public void ShowMissedCount(int missedCount)
        {
            Console.WriteLine(Constants.UserMissedMessage(missedCount));
        }

        public void ShowSummary(int missedCount)
        {
            Console.Clear();
            ShowBattleMap();
            Console.WriteLine(Constants.SummaryMessage(missedCount));
        }

        public void ShowEndCredits()
        {
            DebugVersion();
            Console.WriteLine(Constants.EndCreditsMessage);
        }

        public void RestartMap()
        {
            _battleMap = new char[Constants.MapSize, Constants.MapSize];
        }

        public void MarkShipLocationOnMap(int x, int y)
        {
            _battleMap[x, y] = '@';
        }

        public void MarkHitLocationOnMap(int x, int y)
        {
            _battleMap[x, y] = 'X';
        }

        public void MarkMissLocationOnMap(int x, int y)
        {
            _battleMap[x, y] = '#';
        }

        public void ShowBattleMap()
        {
            int rowLength = _battleMap.GetLength(0);
            int colLength = _battleMap.GetLength(1);
            for (int rowIdx = 0; rowIdx < rowLength; rowIdx++)
            {
                if (rowIdx == 0)
                {
                    Console.Write(" ");
                    for (int colIdx = 0; colIdx < colLength; colIdx++)
                    {
                        Console.Write($" {colIdx + 1}");
                    }
                    Console.Write(Environment.NewLine + Environment.NewLine);
                }
                for (int colIdx = 0; colIdx < colLength; colIdx++)
                {
                    var spaces = new String(' ', (colIdx + 1).ToString().Length);
                    if (colIdx == 0)
                    {
                        Console.Write($"{Convert.ToChar('A' + rowIdx)}{spaces}");
                    }
                    Console.Write($"{(_battleMap[colIdx, rowIdx] != '\0' ? _battleMap[colIdx, rowIdx] : '~')}{spaces}");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        public (int x, int y) GetUserCoordinates()
        {
            int x = -1, y = -1;
            do
            {
                Console.Write(Constants.EnterNewCoordinatesMessage);
                var inputLine = Console.ReadLine();
                var result = _regex.Match(inputLine);

                var alphaPart = result.Groups[1].Value.ToUpper();
                if (!string.IsNullOrEmpty(alphaPart) && alphaPart.Length == 1)
                {
                    var alphaPartValue = Convert.ToInt32(Convert.ToChar(alphaPart)) - 'A';
                    if (alphaPartValue >= 0 && alphaPartValue < Constants.MapSize)
                    {
                        y = alphaPartValue;
                    }
                }
                var numberPart = result.Groups[2].Value;
                if (!string.IsNullOrEmpty(numberPart))
                {
                    var numberPartValue = Convert.ToInt32(numberPart) - 1;
                    if (numberPartValue >= 0 && numberPartValue < Constants.MapSize)
                    {
                        x = numberPartValue;
                    }
                }
            } while (x < 0 || y < 0);
            Console.Clear();
            return (x, y);
        }

        public bool GetUserRestartGameDecision()
        {
            Console.Write(Constants.EnterRestartGameDecision);
            var result = Console.ReadLine().ToUpper() == "Y";
            Console.Clear();
            return result;
        }
    }
}
