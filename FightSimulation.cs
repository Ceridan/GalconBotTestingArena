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
        public static bool SingleFight(BotFight botFight, string starterPackagePath, string applicationPath)
        {
            bool fightSuccess = false;
            try
            {
                //java -jar tools/PlayGame.jar maps/map43.txt 1000 1000 log.txt "java -jar example_bots/DualBot.jar" "bin/Debug/DefenderBot.exe" 1> stdout.txt 2> stderr.txt

                FileInfo logPath = new FileInfo(applicationPath + "/logs/log_fight" + botFight.FightId().ToString() + ".txt");
                botFight.Log(logPath);
                FileInfo viewerInputPath = new FileInfo(applicationPath + "/viewer_inputs/input_fight" + botFight.FightId().ToString() + ".txt");
                botFight.ViewerInput(viewerInputPath);
                FileInfo resultPath = new FileInfo(applicationPath + "/results/result_fight" + botFight.FightId().ToString() + ".txt");
                botFight.Result(resultPath);

                string playGamePath = starterPackagePath + "/tools/PlayGame.jar";

                string player1Cmd = (botFight.Player1().isJar())
                    ? ("\"java -jar \\\"" + botFight.Player1().BotFileInfo().FullName + "\\\"\"")
                    : ("\"" + botFight.Player1().BotFileInfo().FullName + "\"");
                string player2Cmd = (botFight.Player2().isJar())
                    ? ("\"java -jar \\\"" + botFight.Player2().BotFileInfo().FullName + "\\\"\"")
                    : ("\"" + botFight.Player2().BotFileInfo().FullName + "\"");

                /*string fightCmd = " -jar " + "\"" + playGamePath + "\" "
                    + "\"" + botFight.FightMap().MapFileInfo().FullName + "\" "
                    + botFight.TimeAmount().ToString() + " "
                    + botFight.TurnAmount().ToString() + " "
                    + "\"" + applicationPath + "/logs/log_fight" + botFight.FightId().ToString() + ".txt\" "
                    + player1Cmd + " "
                    + player2Cmd;/* +" "
                    + "1> \"" + applicationPath + "/viewer_inputs/input_fight" + botFight.FightId().ToString() + ".txt\" "
                    + "2> \"" + applicationPath + "/results/result_fight" + botFight.FightId().ToString() + ".txt\"";*/

                string fightCmd = " -jar " + "\"" + playGamePath + "\" "
                    + "\"" + botFight.FightMap().MapFileInfo().FullName + "\" "
                    + botFight.TimeAmount().ToString() + " "
                    + botFight.TurnAmount().ToString() + " "
                    + "\"" + botFight.Log().FullName + "\" "
                    + player1Cmd + " "
                    + player2Cmd;

                Process process = new Process();
                process.StartInfo.FileName = "java";
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
            catch (Exception)
            {
                throw;
            }
            return fightSuccess;
        }

        public static bool MassFights(List<BotFight> botFights, string starterPackagePath, string applicationPath)
        {
            bool fightsSuccess = true;
            foreach (BotFight botFight in botFights)
            {
                if (!SingleFight(botFight, starterPackagePath, applicationPath))
                    fightsSuccess = false;
            }
            return fightsSuccess;
        }

        public static void ViewGame(BotFight botFight, string starterPackagePath)
        {
            StreamReader streamReader = new StreamReader(botFight.ViewerInput().FullName);
            string viewerInput = streamReader.ReadToEnd();
            streamReader.Close();

            string viewGamePath = starterPackagePath + "/tools/ShowGame.jar";
            string viewCmd = " -jar " + "\"" + viewGamePath + "\"";

            Process process = new Process();
            process.StartInfo.FileName = "java";
            process.StartInfo.Arguments = viewCmd;
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
}
