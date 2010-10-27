using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GalconBotTestingArena
{
    public class BotFight
    {
        public BotFight(int fightId, Map fightMap, Bot player1, Bot player2, int turnAmount, int timeAmount)
        {
            this.fightId = fightId;
            this.fightMap = fightMap;
            this.player1 = player1;
            this.player2 = player2;
            this.turnAmount = turnAmount;
            this.timeAmount = timeAmount;
            this.log = null ;
            this.result = null;
            this.viewerInput = null;
            this.status = 0;
            this.command = string.Empty;
        }

        public BotFight(int fightId, Map fightMap, Bot player1, Bot player2, int turnAmount, int timeAmount, FileInfo log, FileInfo result, FileInfo viewerInput, int status, string command)
        {
            this.fightId = fightId;
            this.fightMap = fightMap;
            this.player1 = player1;
            this.player2 = player2;
            this.turnAmount = turnAmount;
            this.timeAmount = timeAmount;
            this.log = log;
            this.result = result;
            this.viewerInput = viewerInput;
            this.status = status;
            this.command = command;
        }

        public int FightId()
        {
            return fightId;
        }

        public Map FightMap()
        {
            return fightMap;
        }

        public Bot Player1()
        {
            return player1;
        }

        public Bot Player2()
        {
            return player2;
        }

        public FileInfo Log()
        {
            return log;
        }

        public FileInfo Result()
        {
            return result;
        }

        public FileInfo ViewerInput()
        {
            return viewerInput;
        }

        public int TurnAmount()
        {
            return turnAmount;
        }

        public int TimeAmount()
        {
            return timeAmount;
        }

        public int Status()
        {
            return status;
        }

        public string Command()
        {
            return command;
        }


        public void FightMap(Map newFightMap)
        {
            this.fightMap = newFightMap;
        }

        public void Player1(Bot newPlayer1)
        {
            this.player1 = newPlayer1;
        }

        public void Player2(Bot newPlayer2)
        {
            this.player2 = newPlayer2;
        }

        public void Log(FileInfo newLog)
        {
            this.log = newLog;
        }

        public void Result(FileInfo newResult)
        {
            this.result = newResult;
        }

        public void ViewerInput(FileInfo newViewerInput)
        {
            this.viewerInput = newViewerInput;
        }

        public void TurnAmount(int newTurnAmount)
        {
            this.turnAmount = newTurnAmount;
        }

        public void TimeAmount(int newTimeAmount)
        {
            this.timeAmount = newTimeAmount;
        }

        public void Status(int newStatus)
        {
            this.status = newStatus;
        }

        public void Command(string newCommand)
        {
            this.command = newCommand;
        }

        // Returns winner and two ref values - errors and turns
        public int IsWinner(out string lastTurn, out string errors)
        {
            int isWinnerFlag = 0;
            lastTurn = string.Empty;
            errors = string.Empty;
            if (result != null)
            {
                StreamReader streamReader = new StreamReader(result.FullName);
                while (streamReader.Peek() > 0)
                {
                    string line = streamReader.ReadLine();
                    if (line.IndexOf("Turn") == 0)
                    {
                        lastTurn = line;
                    }
                    else if (line.IndexOf("WARNING") == 0)
                    {
                        errors = line;
                    }
                    else if (line.IndexOf("Player") == 0)
                    {
                        if ((line.Contains("Player 1 Wins!") && (player1.IsMyBot())) || (line.Contains("Player 2 Wins!") && (player2.IsMyBot())))
                        {
                            isWinnerFlag = 1;
                        }
                        else
                        {
                            isWinnerFlag = -1;
                        }
                    }

                }
                streamReader.Close();
            }
            else
                errors = "Can't find result_fight" + fightId.ToString() + ".txt";
            return isWinnerFlag;
        }

        private int fightId; // Starts from 1 (not from zero)
        private Map fightMap;
        private Bot player1;
        private Bot player2;
        private FileInfo log;
        private FileInfo result;
        private FileInfo viewerInput;
        private int turnAmount;
        private int timeAmount;
        private int status;
        private string command;

    }
}
