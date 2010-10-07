using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
//using System.Threading;

namespace GalconBotTestingArena
{
    public static class FightSimulation
    {
        private static string[] ExecFileArgsMaker(string path, string gameFile, string gameCmd)
        {
            string[] resExecArr = new string[2];

            string gamePath = (gameFile.Contains("\"")) ? (gameFile) : ("\"" + path + "/" + gameFile + "\"");
            string execFile = string.Empty;
            string execArgs = string.Empty;
            if (!string.IsNullOrEmpty(gameCmd))
            {
                if (gameCmd.Contains("\""))
                {
                    int pos1 = gameCmd.IndexOf("\"");
                    int pos2 = gameCmd.IndexOf("\"", pos1 + 1);

                    if (pos2 > pos1)
                    {
                        execFile = gameCmd.Substring(pos1, pos2 + 1);
                        execArgs = gameCmd.Substring(pos2 + 1) + " " + gamePath + " ";
                    }
                    else
                        execFile = "java";
                }
                else
                {
                    execFile = execFile.Trim();
                    int pos = gameCmd.IndexOf(" ");
                    if (pos > 0)
                    {
                        execFile = gameCmd.Substring(0, pos);
                        execArgs = gameCmd.Substring(pos) + " " + gamePath + " ";
                    }
                    else
                        execFile = "java";
                }
            }
            else
            {
                execFile = gamePath;
            }

            resExecArr[0] = execFile;
            resExecArr[1] = execArgs;
            return resExecArr;
        }

        // Playe command string
        private static string PlayerCommand(Bot bot)
        {
            string playerCmd = string.Empty;
            string ext = bot.BotFileInfo().Extension;
            foreach (BotExtension be in botExtensions)
            {
                string beExt = be.BotExt();
                string beCmd = be.BotCommandLine();

                if (beExt == ext)
                {
                    if (!string.IsNullOrEmpty(beCmd))
                    {
                        playerCmd = "\"" + beCmd + " \\\"" + bot.BotFileInfo().FullName + "\\\"\"";
                    }
                    else
                    {
                        playerCmd = "\"" + bot.BotFileInfo().FullName + "\"";
                    }

                    break;
                }
            }
            return playerCmd;
        }

        public static bool SingleFight(BotFight botFight)
        {
            bool fightSuccess = false;
            try
            {
                if (isInit)
                {
                    //java -jar tools/PlayGame.jar maps/map43.txt 1000 1000 log.txt "java -jar example_bots/DualBot.jar" "bin/Debug/DefenderBot.exe" 1> stdout.txt 2> stderr.txt

                    FileInfo logPath = new FileInfo(applicationExecPath + "/logs/log_fight" + botFight.FightId().ToString() + ".txt");
                    botFight.Log(logPath);
                    FileInfo viewerInputPath = new FileInfo(applicationExecPath + "/viewer_inputs/input_fight" + botFight.FightId().ToString() + ".txt");
                    botFight.ViewerInput(viewerInputPath);
                    FileInfo resultPath = new FileInfo(applicationExecPath + "/results/result_fight" + botFight.FightId().ToString() + ".txt");
                    botFight.Result(resultPath);

                    //string[] execFileArgs = ExecFileArgsMaker(toolsPath, playGameFile, playGameCmd);
                    //string execFile = execFileArgs[0];
                    //string execArgs = execFileArgs[1];

                    /*string player1Cmd = (botFight.Player1().isJar())
                        ? ("\"java -jar \\\"" + botFight.Player1().BotFileInfo().FullName + "\\\"\"")
                        : ("\"" + botFight.Player1().BotFileInfo().FullName + "\"");
                    string player2Cmd = (botFight.Player2().isJar())
                        ? ("\"java -jar \\\"" + botFight.Player2().BotFileInfo().FullName + "\\\"\"")
                        : ("\"" + botFight.Player2().BotFileInfo().FullName + "\"");*/

                    string player1Cmd = PlayerCommand(botFight.Player1());
                    string player2Cmd = PlayerCommand(botFight.Player2());

                    /*string fightCmd = " -jar " + "\"" + playGamePath + "\" "
                        + "\"" + botFight.FightMap().MapFileInfo().FullName + "\" "
                        + botFight.TimeAmount().ToString() + " "
                        + botFight.TurnAmount().ToString() + " "
                        + "\"" + applicationPath + "/logs/log_fight" + botFight.FightId().ToString() + ".txt\" "
                        + player1Cmd + " "
                        + player2Cmd;/* +" "
                        + "1> \"" + applicationPath + "/viewer_inputs/input_fight" + botFight.FightId().ToString() + ".txt\" "
                        + "2> \"" + applicationPath + "/results/result_fight" + botFight.FightId().ToString() + ".txt\"";*/

                    //string fightCmd = " -jar" + " \"" + playGamePath + "\" "
                    string fightCmd = playGameExecArgs
                        + "\"" + botFight.FightMap().MapFileInfo().FullName + "\" "
                        + botFight.TimeAmount().ToString() + " "
                        + botFight.TurnAmount().ToString() + " "
                        + "\"" + botFight.Log().FullName + "\" "
                        + player1Cmd + " "
                        + player2Cmd;
                    Process process = new Process();
                    process.StartInfo.FileName = playGameExecFile;
                    process.StartInfo.Arguments = fightCmd;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.Start();
                    string stdout = process.StandardOutput.ReadToEnd();
                    string stderr = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    StreamWriter streamWriter = new StreamWriter(botFight.ViewerInput().FullName);
                    streamWriter.WriteLine(stdout);
                    streamWriter.Close();

                    streamWriter = new StreamWriter(botFight.Result().FullName);
                    streamWriter.WriteLine(stderr);
                    streamWriter.Close();

                    fightSuccess = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fightSuccess;
        }

        public static bool MassFights(List<BotFight> botFights)
        {
            bool fightsSuccess = true;
            foreach (BotFight botFight in botFights)
            {
                if (!SingleFight(botFight))
                    fightsSuccess = false;
            }
            return fightsSuccess;
        }

        public static void ViewGame(BotFight botFight)
        {
            ///string viewGamePath = starterPackagePath + "/tools/ShowGame.jar";
            //string viewCmd = " -jar " + "\"" + viewGamePath + "\"";

            //string[] execFileArgs = ExecFileArgsMaker(toolsPath, showGameFile, showGameCmd);
            //string execFile = execFileArgs[0];
            //string execArgs = execFileArgs[1];

            if (isInit)
            {
                StreamReader streamReader = new StreamReader(botFight.ViewerInput().FullName);
                string viewerInput = streamReader.ReadToEnd();
                streamReader.Close();

                Process process = new Process();
                process.StartInfo.FileName = showGameExecFile;
                process.StartInfo.Arguments = showGameExecArgs;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();

                StreamWriter streamWriter = process.StandardInput;
                streamWriter.WriteLine(viewerInput);
                streamWriter.Close();

                process.WaitForExit();
            }
        }

        public static void Init(string applicationPath, string toolsPath, string playGameFile, string playGameCmd, string showGameFile, string showGameCmd, List<BotExtension> botExts)
        {
            applicationExecPath = applicationPath;
            toolsExecPath = toolsPath;
            string[] execFileArgs = ExecFileArgsMaker(toolsPath, playGameFile, playGameCmd);
            playGameExecFile = execFileArgs[0];
            playGameExecArgs = execFileArgs[1];
            execFileArgs = ExecFileArgsMaker(toolsPath, showGameFile, showGameCmd);
            showGameExecFile = execFileArgs[0];
            showGameExecArgs = execFileArgs[1];
            botExtensions = botExts;
            isInit = true;
        }

        private static bool isInit = false;
        private static string applicationExecPath;
        private static string toolsExecPath;
        private static string playGameExecFile;
        private static string playGameExecArgs;
        private static string showGameExecFile;
        private static string showGameExecArgs;
        private static List<BotExtension> botExtensions;
    }
}
