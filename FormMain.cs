using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ini;

namespace GalconBotTestingArena
{
    public partial class FormMain : Form
    {
        // Settings in ini file
        private IniFile SettingsIni;

        //Paths
        private string starterPackagePath;
        private string myBotsPath;

        //Error
        private string errorString;

        // Turns and time
        private int turnAmount;
        private int timeAmount;
        
        //Fights info
        Bot myBot;
        List<BotFight> botFights;

        private enum FoundFlags
        {
            allFlags,
            isToolsFound,
            isMapsFound,
            isExampleBotsFound,
            isMyBotsFound
        }

        public FormMain()
        {
            InitializeComponent();
        }

        // Initialization config
        private void Init()
        {
            btnFight.Enabled = false;
            if (ExecChecker())
            {
                errorString = string.Empty;

                starterPackagePath = string.Empty;
                myBotsPath = string.Empty;

                DataGridViewPrepare();

                SettingsIni = new IniFile(Application.StartupPath + "/GalconBotTestingArena.ini");
                SmartReadSettingsFromIni();
            }
            else
            {
                errorString = "Program can't work with problems listed below:\n" + errorString;
                MessageBox.Show(errorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        //Exec checker
        private bool ExecChecker()
        {
            /*errorString = string.Empty;
            bool checkResult = false;
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "/exec");
            if (di.Exists)
            {
                bool isCmd = false;
                foreach (FileInfo fi in di.GetFiles())
                {
                    if (fi.Name == "cmd.exe")
                        isCmd = true;
                }

                if (isCmd)
                {
                    checkResult = true;
                }
                else
                {
                    if (!isCmd)
                        errorString += "Can't find cmd.exe in subfolder 'exec'.\n";
                }
            }
            else
            {
                errorString += "Can't find subfolder 'exec'.\n";
            }
            return checkResult;*/
            return true;
        }

        // Dependency checker
        private bool PathChecker (FoundFlags flag)
        {
            bool checkResult = false;
            switch (flag)
            {
                case FoundFlags.isToolsFound:
                    string path = tbStarterPackagePath.Text.TrimEnd('/');
                    DirectoryInfo di = new DirectoryInfo(path);
                    if (di.Exists)
                    {
                        bool isToolsFolder = false;
                        foreach (DirectoryInfo subDir in di.GetDirectories())
                        {
                            if (subDir.Name == "tools")
                            {
                                isToolsFolder = true;
                                bool isPlayGame = false;
                                bool isShowGame = false;
                                foreach (FileInfo fi in subDir.GetFiles())
                                {
                                    if (fi.Name == "PlayGame.jar")
                                        isPlayGame = true;
                                    else if (fi.Name == "ShowGame.jar")
                                        isShowGame = true;
                                }

                                if ((isPlayGame) && (isShowGame))
                                {
                                    checkResult = true;
                                }
                                else
                                {
                                    if (!isPlayGame)
                                        errorString += "Can't find PlayGame.jar in subfolder 'tools' in starter package folder.\n";
                                    if (!isShowGame)
                                        errorString += "Can't find ShowGame.jar in subfolder 'tools' in starter package folder.\n";   
                                }
                                break;
                            }
                        }
                        if (!isToolsFolder)
                            errorString += "Can't find 'tools' folder in starter package folder.\n";
                    }
                    else
                    {
                        errorString += "Can't find starter package folder.\n";
                    }
                    break;

                case FoundFlags.isMapsFound:
                    path = tbStarterPackagePath.Text.TrimEnd('/');
                    di = new DirectoryInfo(path);
                    if (di.Exists)
                    {
                        bool isMapsFolder = false;
                        foreach (DirectoryInfo subDir in di.GetDirectories())
                        {
                            if (subDir.Name == "maps")
                            {
                                isMapsFolder = true;
                                if (subDir.GetFiles().Length > 0)
                                {
                                    checkResult = true;
                                }
                                else
                                {
                                    errorString += "'maps' subfolder is empty in starter package folder.\n";
                                }
                                break;
                            }
                        }
                        if (!isMapsFolder)
                            errorString += "Can't find 'maps' folder in starter package folder.\n";
                    }
                    else
                    {
                        errorString += "Can't find starter package folder.\n";
                    }
                    break;

                case FoundFlags.isExampleBotsFound:
                    path = tbStarterPackagePath.Text.TrimEnd('/');
                    di = new DirectoryInfo(path);
                    if (di.Exists)
                    {
                        bool isExampleBots = false;
                        foreach (DirectoryInfo subDir in di.GetDirectories())
                        {
                            if (subDir.Name == "example_bots")
                            {
                                isExampleBots = true;
                                if (subDir.GetFiles().Length > 0)
                                {
                                    checkResult = true;
                                }
                                else
                                {
                                    errorString += "'example_bots' subfolder is empty in starter package folder.\n";
                                }
                                break;
                            }
                        }
                        if (!isExampleBots)
                            errorString += "Can't find 'example_bots' folder in starter package folder.\n";
                    }
                    else
                    {
                        errorString += "Can't find starter package folder.\n";
                    }
                    break;

                case FoundFlags.isMyBotsFound:
                    path = tbMyBotsPath.Text.TrimEnd('/');
                    di = new DirectoryInfo(path);
                    if (di.Exists)
                    {
                        if (di.GetFiles().Length > 0)
                        {
                            checkResult = true;
                        }
                        else
                        {
                            errorString += "Folder with your bots is empty in starter package folder.\n";
                        }
                    }
                    else
                    {
                        errorString += "Can't find folder with your bots.\n";
                    }
                    break;

                case FoundFlags.allFlags:
                default:
                    checkResult = ((PathChecker(FoundFlags.isToolsFound)) &&
                    (PathChecker(FoundFlags.isMapsFound)) &&
                    (PathChecker(FoundFlags.isExampleBotsFound)) &&
                    (PathChecker(FoundFlags.isMyBotsFound)));
                    break;
            }
            return checkResult;
        }

        // My Bots fill
        private void FillBots (ComboBox cmb, string myBotsPath, string chosenBot)
        {
            FillBots(cmb, myBotsPath, string.Empty, chosenBot);
        }

        // Opponent Bots fill
        private void FillBots (ComboBox cmb, string myBotsPath, string exampleBotsPath, string chosenBot)
        {
            bool isMyBots = string.IsNullOrEmpty(exampleBotsPath);
            int selected = 0;
            cmb.Items.Clear();
            int botCounter = 0;
            if (!isMyBots)
            {
                cmb.Items.Add("ALL");
                botCounter++;
            }
            DirectoryInfo di = new DirectoryInfo(myBotsPath);
            FileInfo[] fileInfos = di.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < fileInfos.Length; i++)
            {
                FileInfo fi = fileInfos[i];
                if (((fi.Extension == ".exe") || (fi.Extension == ".jar")) && (!fi.Name.Contains("vshost.exe")))
                {
                    cmb.Items.Add(fi);
                    if (fi.Name == chosenBot)
                        selected = botCounter;
                    botCounter++;
                }
            }

            if (!isMyBots)
            {
                di = new DirectoryInfo(exampleBotsPath);
                fileInfos = di.GetFiles("*", SearchOption.AllDirectories);
                for (int i = 0; i < fileInfos.Length; i++)
                {
                    FileInfo fi = fileInfos[i];
                    if (((fi.Extension == ".exe") || (fi.Extension == ".jar")) && (!fi.Name.Contains("vshost.exe")))
                    {
                        cmb.Items.Add(fi);
                        if (fi.Name == chosenBot)
                            selected = botCounter;
                        botCounter++;
                    }
                }
            }

            if (cmb.Items.Count > selected)
                cmb.SelectedIndex = selected;
        }

        // Maps fill
        private void FillMaps (ComboBox cmb, string mapsPath, string chosenMap)
        {
            int selected = 0;
            cmb.Items.Clear();
            cmb.Items.Add("ALL");
            int mapsCounter = 1;
            DirectoryInfo di = new DirectoryInfo(mapsPath);
            FileInfo[] fileInfos = di.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < fileInfos.Length; i++)
            {
                FileInfo fi = fileInfos[i];
                if (fi.Extension == ".txt")
                {
                    cmb.Items.Add(fi);
                    if (fi.Name == chosenMap)
                        selected = mapsCounter;
                    mapsCounter++;
                }
            }

            if (cmb.Items.Count > selected)
                cmb.SelectedIndex = selected;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void btnStarterPackagePath_Click(object sender, EventArgs e)
        {
            try
            {
                dlgPathFolder.SelectedPath = string.Empty;
                if (DialogResult.OK == dlgPathFolder.ShowDialog())
                {
                    tbStarterPackagePath.Text = dlgPathFolder.SelectedPath;

                    errorString = string.Empty;
                    if ((PathChecker(FoundFlags.isToolsFound)) && (PathChecker(FoundFlags.isMapsFound)) && (PathChecker(FoundFlags.isExampleBotsFound)))
                    {
                        starterPackagePath = tbStarterPackagePath.Text.TrimEnd('\\');
                        FillMaps(cmbMap, starterPackagePath + "/maps", string.Empty);                        
                    }
                    else
                    {
                        MessageBox.Show(errorString, "Pathfinding error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void btnMyBotsPath_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(starterPackagePath))
                {
                    dlgPathFolder.SelectedPath = string.Empty;
                    if (DialogResult.OK == dlgPathFolder.ShowDialog())
                    {
                        tbMyBotsPath.Text = dlgPathFolder.SelectedPath;

                        errorString = string.Empty;
                        if ((PathChecker(FoundFlags.isExampleBotsFound)) && (PathChecker(FoundFlags.isMyBotsFound)))
                        {
                            myBotsPath = tbMyBotsPath.Text.TrimEnd('\\');
                            FillBots(cmbChooseMyBot, myBotsPath, string.Empty);
                            FillBots(cmbChooseOpponentBot, myBotsPath, starterPackagePath + "/example_bots", string.Empty);
                            btnFight.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show(errorString, "Pathfinding error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                    MessageBox.Show("You have to choose path to starter package first!", "Starter package path is required...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnFight_Click(object sender, EventArgs e)
        {
            try
            {
                turnAmount = Convert.ToInt32(tbTurnAmount.Text);
            }
            catch (Exception)
            {
                turnAmount = 1000;
            }

            try
            {
                timeAmount = Convert.ToInt32(tbTimeAmount.Text);
            }
            catch (Exception)
            {
                timeAmount = 1000;
            }

            WriteSettingsToIni();
            if (TestFightsPrepare())
            {
                //FightSimulation.MassFights(botFights, starterPackagePath, Application.StartupPath);
                FightProgress();
                DataGridViewFill();
            }
        }

        //FightProgress
        private void FightProgress()
        {
            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Maximum = botFights.Count + 1;
            progressBar.Value = 1;
            progressBar.Step = 1;

            string formTitle = this.Text;
            int stepPercent = Convert.ToInt32(Math.Ceiling(100 / (double)botFights.Count));
            if (stepPercent == 0)
                stepPercent = 1;
            int tempPercent = 0;
            this.Text = "0% - " + formTitle; 

            foreach (BotFight botFight in botFights)
            {
                bool fightResult = FightSimulation.SingleFight(botFight, starterPackagePath, Application.StartupPath);
                botFight.Status(Convert.ToInt32(fightResult));
                progressBar.PerformStep();
                tempPercent += stepPercent;
                if (tempPercent > 100)
                    tempPercent = 100;
                this.Text = tempPercent.ToString() + "% - " + formTitle;
            }
            this.Text = formTitle;
        }

        // Test fights
        private bool TestFightsPrepare()
        {
            bool prepareFlag = true;
            string logsPath = Application.StartupPath + "/logs";
            string resultsPath = Application.StartupPath + "/results";
            string viewerInputPath = Application.StartupPath + "/viewer_inputs";
            if (!Directory.Exists(logsPath))
                Directory.CreateDirectory(logsPath);
            if (!Directory.Exists(resultsPath))
                Directory.CreateDirectory(resultsPath);
            if (!Directory.Exists(viewerInputPath))
                Directory.CreateDirectory(viewerInputPath);

            //Prepare fight
            myBot = new Bot(0, (FileInfo)cmbChooseMyBot.SelectedItem, true);
            int botId = 1;
            List<Bot> opponentBots = new List<Bot>();
            if (cmbChooseOpponentBot.SelectedIndex == 0)
            {
                for (int i = 1; i < cmbChooseOpponentBot.Items.Count; i++)
                {
                    FileInfo fi = (FileInfo)cmbChooseOpponentBot.Items[i];
                    if ((cbExampleBots.Checked) && (fi.DirectoryName == (starterPackagePath + "\\example_bots")))
                    {
                        Bot opponentBot = new Bot(botId, fi);
                        opponentBots.Add(opponentBot);
                        botId++;
                    }

                    if ((cbOtherMyBots.Checked) && (fi.DirectoryName == myBotsPath))
                    {
                        if ((cbMirror.Checked) || ((!cbMirror.Checked) && (fi.Name != myBot.BotFileInfo().Name)))
                        {
                            Bot opponentBot = new Bot(botId, fi);
                            opponentBots.Add(opponentBot);
                            botId++;
                        }
                    }
                }
            }
            else
            {
                Bot opponentBot = new Bot(botId, (FileInfo)cmbChooseOpponentBot.SelectedItem);
                opponentBots.Add(opponentBot);
            }

            List<Map> maps = new List<Map>();
            int mapId = 0;
            if (cmbMap.SelectedIndex == 0)
            {
                for (int i = 1; i < cmbMap.Items.Count; i++)
                {
                    FileInfo fi = (FileInfo)cmbMap.Items[i];
                    Map map = new Map(mapId, fi);
                    maps.Add(map);
                    mapId++;
                }
            }
            else
            {
                Map map = new Map(mapId, (FileInfo)cmbMap.SelectedItem);
                maps.Add(map);
            }

            //Fights
            if ((opponentBots.Count > 0) && (maps.Count > 0))
            {
                botFights = new List<BotFight>();
                int fightId = 1;
                for (int i = 0; i < opponentBots.Count; i++)
                {
                    for (int j = 0; j < maps.Count; j++)
                    {
                        BotFight botFight = new BotFight(fightId, maps[j], myBot, opponentBots[i], turnAmount, timeAmount);
                        botFights.Add(botFight);
                        fightId++;
                        if (cbSwapPlayers.Checked)
                        {
                            BotFight botFight2 = new BotFight(fightId, maps[j], opponentBots[i], myBot, turnAmount, timeAmount);
                            botFights.Add(botFight2);
                            fightId++;
                        }
                    }
                }
            }
            else
            {
                errorString = string.Empty;
                if (opponentBots.Count == 0)
                    errorString += "There no opponent bots in your current fight settings\n";

                if (maps.Count == 0)
                    errorString += "There no maps in your current fight settings.\n";
                prepareFlag = false;
                MessageBox.Show(errorString, "Fight Settings warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return prepareFlag;
        }

        // Settings Ini file I/O
        private void WriteSettingsToIni()
        {
            SettingsIni.IniWriteValue("PathSettings", "StarterPackagePath", starterPackagePath);
            SettingsIni.IniWriteValue("PathSettings", "MyBotsPath", myBotsPath);
            SettingsIni.IniWriteValue("FightSettings", "MyBotName", cmbChooseMyBot.Text);
            SettingsIni.IniWriteValue("FightSettings", "OpponentBotName", cmbChooseOpponentBot.Text);
            SettingsIni.IniWriteValue("FightSettings", "FightWithExampleBots", Convert.ToInt32(cbExampleBots.Checked).ToString());
            SettingsIni.IniWriteValue("FightSettings", "FightWithOtherBots", Convert.ToInt32(cbOtherMyBots.Checked).ToString());
            SettingsIni.IniWriteValue("FightSettings", "MirrorFight", Convert.ToInt32(cbMirror.Checked).ToString());
            SettingsIni.IniWriteValue("FightSettings", "SwapPlayerNumbers", Convert.ToInt32(cbSwapPlayers.Checked).ToString());
            SettingsIni.IniWriteValue("FightSettings", "Map", cmbMap.Text);
            SettingsIni.IniWriteValue("FightSettings", "TurnAmount", turnAmount.ToString());
            SettingsIni.IniWriteValue("FightSettings", "TimeAmount", timeAmount.ToString());
            SettingsIni.IniWriteValue("LogSettings", "ClearLogFilesOnClose", Convert.ToInt32(cbClearLogs.Checked).ToString());
        }

        private void SmartReadSettingsFromIni()
        {
            try
            {
                starterPackagePath = SettingsIni.IniReadValue("PathSettings", "StarterPackagePath");
                if (!string.IsNullOrEmpty(starterPackagePath))
                {
                    tbStarterPackagePath.Text = starterPackagePath;
                    if ((PathChecker(FoundFlags.isToolsFound)) && (PathChecker(FoundFlags.isMapsFound)) && (PathChecker(FoundFlags.isExampleBotsFound)))
                    {
                        string checkedMap = SettingsIni.IniReadValue("FightSettings", "Map");
                        FillMaps(cmbMap, starterPackagePath + "/" + "maps", checkedMap);

                        myBotsPath = SettingsIni.IniReadValue("PathSettings", "MyBotsPath");
                        if (!string.IsNullOrEmpty(myBotsPath))
                        {
                            tbMyBotsPath.Text = myBotsPath;
                            if (PathChecker(FoundFlags.isMyBotsFound))
                            {
                                string checkedMyBotName = SettingsIni.IniReadValue("FightSettings", "MyBotName");
                                string checkedOpponentBotName = SettingsIni.IniReadValue("FightSettings", "OpponentBotName");
                                FillBots(cmbChooseMyBot, myBotsPath, checkedMyBotName);
                                FillBots(cmbChooseOpponentBot, myBotsPath, starterPackagePath + "/" + "example_bots", checkedOpponentBotName);
                                btnFight.Enabled = true;
                            }
                            else
                            {
                                tbMyBotsPath.Text = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        tbStarterPackagePath.Text = string.Empty;
                    }
                }

                cbExampleBots.Checked = Convert.ToBoolean(Convert.ToInt32(SettingsIni.IniReadValue("FightSettings", "FightWithExampleBots")));
                cbOtherMyBots.Checked = Convert.ToBoolean(Convert.ToInt32(SettingsIni.IniReadValue("FightSettings", "FightWithOtherBots")));
                cbMirror.Checked = Convert.ToBoolean(Convert.ToInt32(SettingsIni.IniReadValue("FightSettings", "MirrorFight")));
                cbSwapPlayers.Checked = Convert.ToBoolean(Convert.ToInt32(SettingsIni.IniReadValue("FightSettings", "SwapPlayerNumbers")));
                tbTurnAmount.Text = SettingsIni.IniReadValue("FightSettings", "TurnAmount");
                tbTimeAmount.Text = SettingsIni.IniReadValue("FightSettings", "TimeAmount");
                cbClearLogs.Checked = Convert.ToBoolean(Convert.ToInt32(SettingsIni.IniReadValue("LogSettings", "ClearLogFilesOnClose")));
            }
            catch (Exception)
            {
            }
        }

        // DataGridView Prepare
        private void DataGridViewPrepare()
        {
            dgvResultGrid.Rows.Clear();
            dgvResultGrid.AllowUserToAddRows = false;
            dgvResultGrid.AllowUserToDeleteRows = false;
            dgvResultGrid.AutoSize = true;
        }

        // DataGridView Fill
        private void DataGridViewFill()
        {
            dgvResultGrid.Rows.Clear();
            int winCount = 0;
            int loseCount = 0;
            int i = 0;
            foreach (BotFight botFight in botFights)
            {
                string lastTurn = string.Empty;
                string errors = string.Empty;
                bool isWinner = botFight.IsWinner(out lastTurn, out errors);

                Color cellsColor = (i % 2 == 0) ? (Color.White) : (Color.LightGray);
                
                dgvResultGrid.Rows.Add();
                dgvResultGrid.Rows[i].Cells["colID"].Value = botFight.FightId().ToString();
                dgvResultGrid.Rows[i].Cells["colID"].Style.BackColor = cellsColor;
                dgvResultGrid.Rows[i].Cells["colMap"].Value = botFight.FightMap().MapFileInfo().Name;
                dgvResultGrid.Rows[i].Cells["colMap"].Style.BackColor = cellsColor;
                dgvResultGrid.Rows[i].Cells["colPlayer1"].Value = botFight.Player1().BotFileInfo().Name;
                dgvResultGrid.Rows[i].Cells["colPlayer1"].Style.BackColor = cellsColor;
                dgvResultGrid.Rows[i].Cells["colPlayer2"].Value = botFight.Player2().BotFileInfo().Name;
                dgvResultGrid.Rows[i].Cells["colPlayer2"].Style.BackColor = cellsColor;
                dgvResultGrid.Rows[i].Cells["colWinner"].Value = (isWinner) ? ("Win") : ("Lose");
                dgvResultGrid.Rows[i].Cells["colWinner"].Style.BackColor = (isWinner) ? (Color.LightGreen) : (Color.LightPink);
                dgvResultGrid.Rows[i].Cells["colErrors"].Value = errors;
                dgvResultGrid.Rows[i].Cells["colErrors"].Style.BackColor = cellsColor;
                dgvResultGrid.Rows[i].Cells["colTurns"].Value = lastTurn;
                dgvResultGrid.Rows[i].Cells["colTurns"].Style.BackColor = cellsColor;
                dgvResultGrid.Rows[i].Cells["colViewGame"].Style.BackColor = cellsColor;

                if (isWinner)
                    winCount++;
                else
                    loseCount++;
                i++;
            }
            double winPercent = ((double)winCount / (double)i) * 100;
            lblTotal.Text = String.Format("Total:    Wins/Games: {0}/{1},    Win %: {2:N}", winCount, i, winPercent);
            
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cbClearLogs.Checked)
            {
                try
                {
                    string logsPath = Application.StartupPath + "/logs";
                    string resultsPath = Application.StartupPath + "/results";
                    string viewerInputPath = Application.StartupPath + "/viewer_inputs";
                    DirectoryInfo di = new DirectoryInfo(logsPath);
                    di.Delete(true);
                    Directory.CreateDirectory(logsPath);
                    di = new DirectoryInfo(resultsPath);
                    di.Delete(true);
                    Directory.CreateDirectory(resultsPath);
                    di = new DirectoryInfo(viewerInputPath);
                    di.Delete(true);
                    Directory.CreateDirectory(viewerInputPath);
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Program can't clear logs. It is being use by another process. You may delete logs manualy from '/logs', '/results' and '/viewer_inputs' folders.\n",
                        "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void dgvResultGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvResultGrid.Columns["colViewGame"].Index)
            {
                foreach (BotFight botFight in botFights)
                {
                    if (botFight.FightId() == Convert.ToInt32(dgvResultGrid.Rows[e.RowIndex].Cells["colID"].Value.ToString()))
                    {
                        FightSimulation.ViewGame(botFight, starterPackagePath);
                        break;
                    }
                }
            }
        }
    }
}
