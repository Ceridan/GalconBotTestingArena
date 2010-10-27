using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Threading;
using Ini;

namespace GalconBotTestingArena
{
    public partial class FormMain : Form
    {
        // Settings in ini file
        private IniFile SettingsIni;

        //Paths
        private string starterPackagePath;
        private string mapsPath;
        private string toolsPath;
        private string exampleBotsPath;
        private string myBotsPath;

        //Bots
        private string myBotName;
        private string opponentBotName;
        private string currentMap;

        //Error
        private string errorString;

        // Turns and time
        private int turnAmount;
        private int timeAmount;
        
        //Fights info
        Bot myBot;
        List<BotFight> botFights;
        List<BotExtension> botExtensions;
        bool isPossibleFightStart;
        bool isBackground;

        // Threads
        private int threadCount;
        private bool threadStopFlag;
        private ManualResetEvent[] doneEvents;
        private ThreadRunner[] threadRunners;
        private Object locker;


        // Tools
        private string playGameCmd;
        private string playGameFile;
        private string showGameCmd;
        private string showGameFile;

        // Total
        private int winCount;
        private int loseCount;
        private int drawCount;
        private double winPercent;
        private string formTitleStr;
        private int fightCount;
        private int totalTurnNumbers;



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
            //btnFight.Enabled = false;
            if (ExecChecker())
            {
                //CultureInfo MyCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                locker = new Object();

                formTitleStr = this.Text;
                errorString = string.Empty;

                starterPackagePath = string.Empty;
                mapsPath = string.Empty;
                toolsPath = string.Empty;
                exampleBotsPath = string.Empty;
                myBotsPath = string.Empty;

                myBotName = string.Empty;
                opponentBotName = string.Empty;
                currentMap = string.Empty;

                playGameCmd = string.Empty;
                playGameFile = string.Empty;
                showGameCmd = string.Empty;
                showGameFile = string.Empty;

                threadCount = Environment.ProcessorCount;
                lblThreads.Text = String.Format("There are {0} CPUs detected. Recommend number of threads are equal to CPUs number. How many threads do you want to use to run your bots", threadCount);
                turnAmount = 1000;
                timeAmount = 1000;
                isPossibleFightStart = false;
                isBackground = false;

                DataGridViewPrepare(dgvResultGrid);
                DataGridViewPrepare(dgvTools);
                DataGridViewPrepare(dgvBots);

                SettingsIni = new IniFile(Application.StartupPath + "/GalconBotTestingArena.ini");
                SmartReadSettingsFromIni();

                PanelsTabResizing();
                DataGridViewToolsFill();
                DataGridViewBotsFill();
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
            if (di.Exists)
            {
                FileInfo[] fileInfos = di.GetFiles("*", SearchOption.AllDirectories);
                for (int i = 0; i < fileInfos.Length; i++)
                {
                    FileInfo fi = fileInfos[i];
                    if (!fi.Name.Contains("vshost.exe"))
                    {
                        foreach (BotExtension botExt in botExtensions)
                        {
                            if (botExt.BotExt() == fi.Extension)
                            {
                                cmb.Items.Add(fi);
                                if (fi.Name == chosenBot)
                                    selected = botCounter;
                                botCounter++;
                                break;
                            }
                        }
                    }
                }
            }

            if (!isMyBots)
            {
                di = new DirectoryInfo(exampleBotsPath);
                if (di.Exists)
                {
                    FileInfo[] fileInfos = di.GetFiles("*", SearchOption.AllDirectories);
                    for (int i = 0; i < fileInfos.Length; i++)
                    {
                        FileInfo fi = fileInfos[i];
                        if (!fi.Name.Contains("vshost.exe"))
                        {
                            foreach (BotExtension botExt in botExtensions)
                            {
                                if (botExt.BotExt() == fi.Extension)
                                {
                                    cmb.Items.Add(fi);
                                    if (fi.Name == chosenBot)
                                        selected = botCounter;
                                    botCounter++;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (cmb.Items.Count > selected)
                cmb.SelectedIndex = selected;
        }

        // Maps fill
        private void FillMaps (ComboBox cmb, string mapPath, string chosenMap)
        {
            int selected = 0;
            cmb.Items.Clear();
            cmb.Items.Add("ALL");
            int mapsCounter = 1;
            DirectoryInfo di = new DirectoryInfo(mapPath);
            if (di.Exists)
            {
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
            /*try
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
            }*/

            dlgPathFolder.SelectedPath = string.Empty;
            try
            {
                if (DialogResult.OK == dlgPathFolder.ShowDialog())
                {
                    string path = dlgPathFolder.SelectedPath.TrimEnd('\\').TrimEnd('/');
                    tbStarterPackagePath.Text = path;
                    tbMaps.Text = path + "\\maps";
                    tbTools.Text = path + "\\tools";
                    tbExampleBots.Text = path + "\\example_bots";
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void btnMyBotsPath_Click(object sender, EventArgs e)
        {
            /*try
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
            }*/
            try
            {
                dlgPathFolder.SelectedPath = string.Empty;
                if (DialogResult.OK == dlgPathFolder.ShowDialog())
                {
                    string path = dlgPathFolder.SelectedPath.TrimEnd('\\').TrimEnd('/');
                    tbMyBotsPath.Text = path;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnFight_Click(object sender, EventArgs e)
        {
            if ((isPossibleFightStart) && (!backgroundWorker.IsBusy))
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

                WriteSettingsToIni(tabControlMain.SelectedIndex);
                if (TestFightsPrepare())
                {
                    threadCount = 1;
                    FightStart();
                    //dgvResultGrid.ScrollBars = ScrollBars.None;
                    //FightSimulation.MassFights(botFights, starterPackagePath, Application.StartupPath);
                    //FightProgress();
                    backgroundWorker.WorkerReportsProgress = true;
                    backgroundWorker.RunWorkerAsync(formTitleStr);
                    //DataGridViewResultFill();
                }
            }
            else 
            {
                if (!isPossibleFightStart)
                    MessageBox.Show("There are some missing settings. Fights could not be started.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (isBackground)
                    MessageBox.Show("Another process is already running", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        // Background DoWork
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            //string arg = (string)e.Argument;
            //FightProgress();

            e.Result = FightThreadCalculation(bw, e);

            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }

            return;
        }

        // Background event handler
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                // The user canceled the operation.
                MessageBox.Show("Operation was canceled");
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
            {
                // The operation completed normally.
                string msg = String.Format("Done!");
                //MessageBox.Show(msg);
            }

            FightFinish();
        }

        private int FightThreadCalculation(BackgroundWorker bw, DoWorkEventArgs e)
        {
            if (threadCount > 0)
            {
                //ThreadPool.SetMinThreads(threadCount, threadCount);
                ThreadPool.SetMaxThreads(threadCount, threadCount);
                doneEvents = new ManualResetEvent[threadCount];
                threadRunners = new ThreadRunner[threadCount];
                threadStopFlag = false;

                for (int i = 0; i < threadCount; i++)
                {
                    doneEvents[i] = new ManualResetEvent(true);
                }

                int fightCount = 0;
                int fightReturn = 0;

                //dgvResultGrid.Rows.Clear();
                while (fightReturn < botFights.Count)
                {
                    int doneIndex = WaitHandle.WaitAny(doneEvents);
                    if ((!threadStopFlag) && (!bw.CancellationPending))
                    {
                        //lock (locker)
                        {
                            BotFight bf = null;
                            //int tempFightCount = fightCount;
                            if (threadRunners[doneIndex] != null)
                            {
                                fightReturn++;
                                bf = threadRunners[doneIndex].GetBotFight();
                                int percentComplete =
                                    (int)((float)fightReturn / (float)botFights.Count * 100);
                                backgroundWorker.ReportProgress(percentComplete, bf.FightId());
                            }
                            if (fightCount < botFights.Count)
                            {
                                doneEvents[doneIndex] = new ManualResetEvent(false);
                                threadRunners[doneIndex] = new ThreadRunner(botFights[fightCount], doneEvents[doneIndex]);
                                ThreadPool.QueueUserWorkItem(threadRunners[doneIndex].ThreadPoolCallback, doneIndex);
                                fightCount++;
                                //if (bf != null)
                                //{
                                //    int percentComplete =
                                //        (int) ((float) tempFightCount/(float) botFights.Count*100);
                                //    backgroundWorker.ReportProgress(percentComplete, bf.FightId());
                                //}
                            }
                            else
                            {
                                threadRunners[doneIndex] = null;
                                doneEvents[doneIndex] = new ManualResetEvent(false);
                                //WaitHandle.WaitAll(doneEvents);
                                //break;
                            }

                            /*if (bf != null)
                            {
                                //lock (locker)
                                {
                                    DataGridViewResultOnFly(bf);
                                    this.Text = dgvResultGrid.Rows.Count.ToString() + " of " +
                                                botFights.Count.ToString() + " - " + formTitle;
                                }
                            }*/
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        break;
                    }
                }

                //for (int i = 0; i < threadRunners.Length; i++)
                //{
                //    int tempFightCount = botFights.Count - threadRunners.Length + i + 1;
                //    BotFight bf = threadRunners[i].GetBotFight();
                //    int percentComplete =
                //        (int)((float)tempFightCount / (float)botFights.Count * 100);
                //    backgroundWorker.ReportProgress(percentComplete, bf.FightId());
                //}
                //WaitHandle.WaitAll(doneEvents);
            }
            //else
            //{
            //    foreach (BotFight botFight in botFights)
            //    {
            //        bool fightResult = FightSimulation.SingleFight(botFight);
            //        botFight.Status(Convert.ToInt32(fightResult));
            //        DataGridViewResultOnFly(botFight);
            //        this.Text = dgvResultGrid.Rows.Count.ToString() + " of " + botFights.Count.ToString() + " - " + formTitle;
            //    }
            //}
            return 1;
        }

        //FightProgress
        //private void FightProgress()
        //{
        //        DateTime dtStart = DateTime.Now;
        //        winCount = 0;
        //        loseCount = 0;
        //        winPercent = 0;

        //        progressBar.Visible = true;
        //        progressBar.Minimum = 1;
        //        progressBar.Maximum = botFights.Count + 1;
        //        progressBar.Value = 1;
        //        progressBar.Step = 1;

        //        string formTitle = this.Text;
        //        this.Text = "0 of " + botFights.Count.ToString() + " - " + formTitle;

        //        FightThreadCalculation(formTitle);


        //        this.Text = formTitle;
        //        winPercent = ((double)winCount / (double)botFights.Count) * 100;
        //        lblTotal.Text = String.Format("Total:    Wins/Games: {0}/{1},    Win %: {2:N}", winCount, botFights.Count, winPercent);
        //        DateTime dtEnd = DateTime.Now;
        //        //MessageBox.Show("Calc time " + ((TimeSpan)(dtEnd - dtStart)).ToString());
            
        //}

        private void FightStart()
        {
            //dgvResultGrid.ScrollBars = ScrollBars.None;
            dgvResultGrid.Rows.Clear();
            isBackground = true;
            winCount = 0;
            drawCount = 0;
            loseCount = 0;
            winPercent = 0;
            fightCount = 0;
            totalTurnNumbers = 0;

            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Maximum = botFights.Count + 1;
            progressBar.Value = 1;
            progressBar.Step = 1;

            lblTotal.Text = "Total: ";

            //string formTitle = this.Text;
            this.Text = "0 of " + botFights.Count.ToString() + " - " + formTitleStr;
        }

        private void FightFinish()
        {
            /*dgvResultGrid.ScrollBars = ScrollBars.Both;
            dgvResultGrid.Dock = DockStyle.None;
            dgvResultGrid.Dock = DockStyle.Fill;*/
            isBackground = false;
            this.Text = formTitleStr;
            //winPercent = ((double)winCount / (double)botFights.Count) * 100;
            int avgTurnNumbers = 0;
            if (fightCount > 0)
            {
                winPercent = ((double)winCount / (double)fightCount) * 100;
                avgTurnNumbers = (int)Math.Ceiling((double)totalTurnNumbers / (double)fightCount);
            }
            lblTotal.Text = String.Format("Wins: {0}, Loses: {1}, Draws: {2}    |    Total:  Wins/Games: {3}/{4},    Win %: {5:N}    |    Avg. turn numbers: {6}", winCount, loseCount, drawCount, winCount, fightCount, winPercent, avgTurnNumbers);
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
                    //if ((cbExampleBots.Checked) && (fi.DirectoryName == (starterPackagePath + "\\example_bots")))
                    //if ((cbExampleBots.Checked) && (fi.DirectoryName == (Path.GetFullPath(Path.Combine(starterPackagePath, "example_bots")))))
                    if ((cbExampleBots.Checked) && (fi.DirectoryName == Path.GetFullPath(exampleBotsPath)))
                    {
                        Bot opponentBot = new Bot(botId, fi);
                        opponentBots.Add(opponentBot);
                        botId++;
                    }

                    //if ((cbOtherMyBots.Checked) && (fi.DirectoryName == myBotsPath))                    
                    if ((cbOtherMyBots.Checked) && (fi.DirectoryName == Path.GetFullPath(myBotsPath)))
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
        private void WriteSettingsToIni(int tabNum)
        {
            switch (tabNum)
            {
                case 0:
                    SettingsIni.IniWriteValue("PathSettings", "StarterPackagePath", starterPackagePath);
                    SettingsIni.IniWriteValue("PathSettings", "MapsPath", mapsPath);
                    SettingsIni.IniWriteValue("PathSettings", "ToolsPath", toolsPath);
                    SettingsIni.IniWriteValue("PathSettings", "ExampleBotsPath", exampleBotsPath);
                    SettingsIni.IniWriteValue("PathSettings", "MyBotsPath", myBotsPath);
                    SettingsIni.IniWriteValue("CPU", "CPUNumber", threadCount.ToString());
                    break;

                case 1:
                    SettingsIni.IniWriteValue("ToolsCommandLine", "PlayGameCommandLine", playGameCmd);
                    SettingsIni.IniWriteValue("ToolsCommandLine", "PlayGameFile", playGameFile);
                    SettingsIni.IniWriteValue("ToolsCommandLine", "ShowGameCommandLine", showGameCmd);
                    SettingsIni.IniWriteValue("ToolsCommandLine", "ShowGameFile", showGameFile);
                    break;

                case 2:
                    myBotName = cmbChooseMyBot.Text;
                    SettingsIni.IniWriteValue("FightSettings", "MyBotName", myBotName);
                    opponentBotName = cmbChooseOpponentBot.Text;
                    SettingsIni.IniWriteValue("FightSettings", "OpponentBotName", opponentBotName);
                    SettingsIni.IniWriteValue("FightSettings", "FightWithExampleBots", Convert.ToInt32(cbExampleBots.Checked).ToString());
                    SettingsIni.IniWriteValue("FightSettings", "FightWithOtherBots", Convert.ToInt32(cbOtherMyBots.Checked).ToString());
                    SettingsIni.IniWriteValue("FightSettings", "MirrorFight", Convert.ToInt32(cbMirror.Checked).ToString());
                    SettingsIni.IniWriteValue("FightSettings", "SwapPlayerNumbers", Convert.ToInt32(cbSwapPlayers.Checked).ToString());
                    currentMap = cmbMap.Text;
                    SettingsIni.IniWriteValue("FightSettings", "Map", currentMap);
                    SettingsIni.IniWriteValue("FightSettings", "TurnAmount", turnAmount.ToString());
                    SettingsIni.IniWriteValue("FightSettings", "TimeAmount", timeAmount.ToString());
                    SettingsIni.IniWriteValue("LogSettings", "ClearLogFilesOnClose", Convert.ToInt32(cbClearLogs.Checked).ToString());
                    break;

                default:
                    break;
            }
        }

        private void SmartReadSettingsFromIni()
        {
            try
            {
                starterPackagePath = SettingsIni.IniReadValue("PathSettings", "StarterPackagePath");
                tbStarterPackagePath.Text = starterPackagePath;
                myBotsPath = SettingsIni.IniReadValue("PathSettings", "MyBotsPath");
                tbMyBotsPath.Text = myBotsPath;
                mapsPath = SettingsIni.IniReadValue("PathSettings", "MapsPath");
                tbMaps.Text = mapsPath;
                toolsPath = SettingsIni.IniReadValue("PathSettings", "ToolsPath");
                tbTools.Text = toolsPath;
                exampleBotsPath = SettingsIni.IniReadValue("PathSettings", "ExampleBotsPath");
                tbExampleBots.Text = exampleBotsPath;
                string threadCountsStr = SettingsIni.IniReadValue("CPU", "CPUNumber");
                if (!string.IsNullOrEmpty(threadCountsStr))
                    threadCount = Convert.ToInt32(threadCountsStr);
                tbThreads.Text = threadCount.ToString();

                myBotName = SettingsIni.IniReadValue("FightSettings", "MyBotName");
                opponentBotName = SettingsIni.IniReadValue("FightSettings", "OpponentBotName");
                currentMap = SettingsIni.IniReadValue("FightSettings", "Map");
                string exampleBotCheckStr = SettingsIni.IniReadValue("FightSettings", "FightWithExampleBots");
                cbExampleBots.Checked = (!string.IsNullOrEmpty(exampleBotCheckStr)) ? Convert.ToBoolean(Convert.ToInt32(exampleBotCheckStr)) : true;
                string otherBotCheckStr = SettingsIni.IniReadValue("FightSettings", "FightWithOtherBots");
                cbOtherMyBots.Checked = (!string.IsNullOrEmpty(otherBotCheckStr)) ? Convert.ToBoolean(Convert.ToInt32(otherBotCheckStr)) : true;
                string mirrorCheckStr = SettingsIni.IniReadValue("FightSettings", "MirrorFight");
                cbMirror.Checked = (!string.IsNullOrEmpty(mirrorCheckStr)) ? Convert.ToBoolean(Convert.ToInt32(mirrorCheckStr)) : true;
                string swapPlayerCheckStr = SettingsIni.IniReadValue("FightSettings", "SwapPlayerNumbers");
                cbSwapPlayers.Checked = (!string.IsNullOrEmpty(swapPlayerCheckStr)) ? Convert.ToBoolean(Convert.ToInt32(swapPlayerCheckStr)) : true;
                string turnAmountStr = SettingsIni.IniReadValue("FightSettings", "TurnAmount");
                if (!string.IsNullOrEmpty(turnAmountStr))
                    turnAmount = Convert.ToInt32(turnAmountStr);
                tbTurnAmount.Text = turnAmount.ToString();
                string timeAmountStr = SettingsIni.IniReadValue("FightSettings", "TimeAmount");
                if (!string.IsNullOrEmpty(timeAmountStr))
                    timeAmount = Convert.ToInt32(timeAmountStr);
                tbTimeAmount.Text = timeAmount.ToString();
                string clearLogsStr = SettingsIni.IniReadValue("LogSettings", "ClearLogFilesOnClose");
                cbClearLogs.Checked = (!string.IsNullOrEmpty(clearLogsStr)) ? Convert.ToBoolean(Convert.ToInt32(clearLogsStr)) : true;
            }
            catch (Exception)
            {
            }
        }

        // DataGridView Prepare
        private void DataGridViewPrepare(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AutoSize = true;
        }

        // DataGridViewTools Fill
        private void DataGridViewToolsFill()
        {
            dgvTools.Rows.Clear();

            string playCmd = string.Empty;
            string playFile = string.Empty;
            string showCmd = string.Empty;
            string showFile = string.Empty;

            try
            {
                playCmd = SettingsIni.IniReadValue("ToolsCommandLine", "PlayGameCommandLine");
                playFile = SettingsIni.IniReadValue("ToolsCommandLine", "PlayGameFile");
                showCmd = SettingsIni.IniReadValue("ToolsCommandLine", "ShowGameCommandLine");
                showFile = SettingsIni.IniReadValue("ToolsCommandLine", "ShowGameFile");
            }
            catch (Exception)
            {
            }

            dgvTools.Rows.Add();
            dgvTools.Rows[0].Cells["colToolsCommandLine"].Value = (string.IsNullOrEmpty(playGameCmd)) ? "java -jar" : playCmd;
            playGameCmd = dgvTools.Rows[0].Cells["colToolsCommandLine"].Value.ToString();
            dgvTools.Rows[0].Cells["colToolsFileName"].Value = (string.IsNullOrEmpty(playGameFile)) ? "PlayGame.jar" : playFile;
            playGameFile = dgvTools.Rows[0].Cells["colToolsFileName"].Value.ToString();
            dgvTools.Rows.Add();
            dgvTools.Rows[1].Cells["colToolsCommandLine"].Value = (string.IsNullOrEmpty(showGameCmd)) ? "java -jar" : showCmd;
            showGameCmd = dgvTools.Rows[1].Cells["colToolsCommandLine"].Value.ToString();
            dgvTools.Rows[1].Cells["colToolsFileName"].Value = (string.IsNullOrEmpty(showGameFile)) ? "ShowGame.jar" : showFile;
            showGameFile = dgvTools.Rows[1].Cells["colToolsFileName"].Value.ToString();
        }

        // DataGridViewBots Fill
        private void DataGridViewBotsFill()
        {
            dgvBots.Rows.Clear();
            bool exeFlag = false;
            bool jarFlag = false;
            int i = 0;

            FileInfo fi = new FileInfo(Application.StartupPath + "/" + "BotsList.txt");
            if (fi.Exists)
            {
                StreamReader streamReader = new StreamReader(fi.FullName);
                while (streamReader.Peek() > 0)
                {
                    string line = streamReader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        int pos = line.LastIndexOf("|");
                        if ((pos >= 0)  && (line.Length > 2))
                        {
                            dgvBots.Rows.Add();
                            if (pos > 0)
                            {
                                dgvBots.Rows[i].Cells["colBotsCommandLine"].Value = line.Substring(0, pos - 1);
                            }
                            string ext = line.Substring(pos + 2);
                            dgvBots.Rows[i].Cells["colBotsExtensions"].Value = ext;
                            if (ext == ".exe")
                                exeFlag = true;
                            if (ext == ".jar")
                                jarFlag = true;
                            ++i;
                        }
                    }
                }
                streamReader.Close();
            }
            if (!exeFlag)
            {
                dgvBots.Rows.Add();
                dgvBots.Rows[i].Cells["colBotsExtensions"].Value = ".exe";
                ++i;
            }
            if (!jarFlag)
            {
                dgvBots.Rows.Add();
                dgvBots.Rows[i].Cells["colBotsCommandLine"].Value = "java -jar";
                dgvBots.Rows[i].Cells["colBotsExtensions"].Value = ".jar";
                ++i;
            }
            FillBotExtensions();
        }


        // DataGRidViewResult on fly
        private void DataGridViewResultOnFly(BotFight botFight)
        {
            int i = dgvResultGrid.Rows.Count;
            string lastTurn = string.Empty;
            string errors = string.Empty;
            int isWinner = botFight.IsWinner(out lastTurn, out errors);

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
            switch (isWinner)
            {
                case -1:
                    dgvResultGrid.Rows[i].Cells["colWinner"].Value = "Lose";
                    dgvResultGrid.Rows[i].Cells["colWinner"].Style.BackColor = Color.LightPink;
                    loseCount++;
                    break;

                case 0:
                    dgvResultGrid.Rows[i].Cells["colWinner"].Value = "Draw";
                    dgvResultGrid.Rows[i].Cells["colWinner"].Style.BackColor = Color.LightBlue;
                    drawCount++;
                    break;

                case 1:
                    dgvResultGrid.Rows[i].Cells["colWinner"].Value = "Win";
                    dgvResultGrid.Rows[i].Cells["colWinner"].Style.BackColor = Color.LightGreen;
                    winCount++;
                    break;

                default:
                    break;
            }
            dgvResultGrid.Rows[i].Cells["colErrors"].Value = errors;
            dgvResultGrid.Rows[i].Cells["colErrors"].Style.BackColor = cellsColor;
            dgvResultGrid.Rows[i].Cells["colTurns"].Value = lastTurn;
            dgvResultGrid.Rows[i].Cells["colTurns"].Style.BackColor = cellsColor;
            dgvResultGrid.Rows[i].Cells["colViewGame"].Style.BackColor = cellsColor;
            dgvResultGrid.Rows[i].Cells["colCommand"].Value = botFight.Command();            

            fightCount++;
            totalTurnNumbers += Convert.ToInt32(lastTurn.Substring(5));
            int avgTurnNumbers = 0;
            if (fightCount > 0)
            {
                winPercent = ((double)winCount / (double)fightCount) * 100;
                avgTurnNumbers = (int)Math.Ceiling((double)totalTurnNumbers / (double)fightCount);
            }
            lblTotal.Text = String.Format("Wins: {0}, Loses: {1}, Draws: {2}    |    Total:  Wins/Games: {3}/{4},    Win %: {5:N}    |    Avg. turn numbers: {6}", winCount, loseCount, drawCount, winCount, fightCount, winPercent, avgTurnNumbers);

            progressBar.PerformStep();
        }

        // DataGridViewResult Fill
        private void DataGridViewResultFill()
        {
            dgvResultGrid.Rows.Clear();
            winCount = 0;
            drawCount = 0;
            loseCount = 0;
            int i = 0;
            foreach (BotFight botFight in botFights)
            {
                string lastTurn = string.Empty;
                string errors = string.Empty;
                int isWinner = botFight.IsWinner(out lastTurn, out errors);

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
                switch (isWinner)
                {
                    case -1:
                        dgvResultGrid.Rows[i].Cells["colWinner"].Value = "Lose";
                        dgvResultGrid.Rows[i].Cells["colWinner"].Style.BackColor = Color.LightPink;
                        loseCount++;
                        break;

                    case 0:
                        dgvResultGrid.Rows[i].Cells["colWinner"].Value = "Draw";
                        dgvResultGrid.Rows[i].Cells["colWinner"].Style.BackColor = Color.LightBlue;
                        drawCount++;
                        break;

                    case 1:
                        dgvResultGrid.Rows[i].Cells["colWinner"].Value = "Win";
                        dgvResultGrid.Rows[i].Cells["colWinner"].Style.BackColor = Color.LightGreen;
                        winCount++;
                        break;

                    default:
                        break;
                }
                dgvResultGrid.Rows[i].Cells["colErrors"].Value = errors;
                dgvResultGrid.Rows[i].Cells["colErrors"].Style.BackColor = cellsColor;
                dgvResultGrid.Rows[i].Cells["colTurns"].Value = lastTurn;
                dgvResultGrid.Rows[i].Cells["colTurns"].Style.BackColor = cellsColor;
                dgvResultGrid.Rows[i].Cells["colViewGame"].Style.BackColor = cellsColor;

                i++;
            }
            winPercent = ((double)winCount / (double)i) * 100;
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
                        Thread t = new Thread(new ParameterizedThreadStart(ViewGame));
                        t.Start(botFight);
                        //FightSimulation.ViewGame(botFight);
                        break;
                    }
                }
            }
        }

        // Fight simulation view game thread start
        private void ViewGame(object botFightObj)
        {
            FightSimulation.ViewGame((BotFight)botFightObj);
        }

        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {
            PanelsTabResizing();
        }

        private void PanelsTabResizing()
        {
            if (tabControlMain.SelectedIndex < 2)
            {
                pnlResult.Dock = DockStyle.None;
                Size size = new System.Drawing.Size(pnlResult.Size.Width, 0);
                pnlResult.Size = size;
                pnlSetup.Dock = DockStyle.Fill;
                progressBar.Visible = false;
                btnSave.Visible = true;
                lblErrors.Visible = false;
            }
            else
            {
                pnlSetup.Dock = DockStyle.Top;
                Size size = new System.Drawing.Size(pnlSetup.Size.Width, 250);
                pnlSetup.Size = size;
                pnlResult.Dock = DockStyle.Fill;
                progressBar.Visible = true;
                btnSave.Visible = false;
                lblErrors.Visible = false;
                isPossibleFightStart = CheckSettingsForTestingArenaTab();
            }
        }

        // Check settings for testing arena tab
        private bool CheckSettingsForTestingArenaTab()
        {
            bool resFlag = true;

            errorString = string.Empty;

            try
            {
                DirectoryInfo di = new DirectoryInfo(toolsPath);
                if (di.Exists)
                {
                    FileInfo fi = new FileInfo(di.FullName + "/" + playGameFile);
                    if (!fi.Exists)
                    {
                        errorString += "Can't find PlayGame file.\n";
                        resFlag = false;
                    }
                    fi = new FileInfo(di.FullName + "/" + showGameFile);
                    if (!fi.Exists)
                    {
                        errorString += "Can't find ShowGame file.\n";
                        resFlag = false;
                    }
                }
                else
                {
                    errorString += "Can't find tools folder.\n";
                    resFlag = false;
                }

                if (resFlag)
                {
                    di = new DirectoryInfo(myBotsPath);
                    if (di.Exists)
                    {
                        FillBots(cmbChooseMyBot, myBotsPath, myBotName);
                        if (cmbChooseMyBot.Items.Count == 0)
                        {
                            errorString += "Can't find any your bot.\n";
                            resFlag = false;
                        }
                    }
                    else
                    {
                        errorString += "Can't find folder with your bots.\n";
                        resFlag = false;
                    }

                    di = new DirectoryInfo(exampleBotsPath);
                    if (di.Exists)
                    {
                        FillBots(cmbChooseOpponentBot, myBotsPath, exampleBotsPath, opponentBotName);
                        if (cmbChooseOpponentBot.Items.Count <= 1)
                        {
                            errorString += "Can't find any example or your bot.\n";
                            resFlag = false;
                        }
                    }
                    else
                    {
                        errorString += "Can't find folder with example bots.\n";
                    }

                    di = new DirectoryInfo(mapsPath);
                    if (di.Exists)
                    {
                        FillMaps(cmbMap, mapsPath, currentMap);
                        if (cmbMap.Items.Count == 0)
                        {
                            errorString += "Can't find any map.\n";
                            resFlag = false;
                        }
                    }
                    else
                    {
                        errorString += "Can't find folder maps folder.\n";
                        resFlag = false;
                    }
                }

                if (resFlag)
                {
                    FightSimulation.Init(Application.StartupPath, toolsPath, playGameFile, playGameCmd, showGameFile, showGameCmd, botExtensions);
                }

                if (!resFlag)
                    MessageBox.Show(errorString, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception e)
            {
                resFlag = false;
                MessageBox.Show("There are error occurs during Testing Arena prepare. Please check all path settings first.\n\nError stack:\n" + e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return resFlag;
        }

        private void btnAddBotRow_Click(object sender, EventArgs e)
        {
            dgvBots.Rows.Add();
        }

        private void btnDelBotRow_Click(object sender, EventArgs e)
        {
            if (dgvBots.SelectedRows.Count > 0)
            {
                while (dgvBots.SelectedRows.Count > 0)
                {
                    for (int i = 0; i < dgvBots.Rows.Count; i++)
                        if (dgvBots.Rows[i].Selected)
                            dgvBots.Rows.RemoveAt(i);
                }
            }
        }

        // Save settings changes
        private void SaveSettingsChanges(int tabNum, bool isShowLabel)
        {
            errorString = string.Empty;
            DirectoryInfo di = null;
            lblErrors.Text = string.Empty;
            lblErrors.Visible = true;
            switch (tabNum)
            {
                case 0:
                    di = new DirectoryInfo(tbStarterPackagePath.Text.Trim('\\').Trim('/'));
                    if (di.Exists)
                        starterPackagePath = tbStarterPackagePath.Text.Trim('\\').Trim('/');
                    di = new DirectoryInfo(tbMaps.Text.Trim('\\').Trim('/'));
                    if (di.Exists)
                        mapsPath = tbMaps.Text.Trim('\\').Trim('/');
                    else
                        errorString += "Maps path not found. ";
                    di = new DirectoryInfo(tbTools.Text.Trim('\\').Trim('/'));
                    if (di.Exists)
                        toolsPath = tbTools.Text.Trim('\\').Trim('/');
                    else
                        errorString += "Tools path not found. ";
                    di = new DirectoryInfo(tbExampleBots.Text.Trim('\\').Trim('/'));
                    if (di.Exists)
                        exampleBotsPath = tbExampleBots.Text.Trim('\\').Trim('/');
                    else
                        errorString += "Example bots path not found. ";
                    di = new DirectoryInfo(tbMyBotsPath.Text.Trim('\\').Trim('/'));
                    if (di.Exists)
                        myBotsPath = tbMyBotsPath.Text.Trim('\\').Trim('/');
                    else
                        errorString += "Your bots path not found. ";

                    int q = 0;
                    try
                    {
                        q = Convert.ToInt32(tbThreads.Text);
                    }
                    catch (Exception)
                    {
                    }
                    if (q > 0)
                        threadCount = q;
                    break;

                case 1:
                    string playFile = dgvTools.Rows[0].Cells["colToolsFileName"].Value.ToString();
                    if (!string.IsNullOrEmpty(playFile))
                        playGameFile = playFile;
                    else
                        lblErrors.Text = "Can't find PlayGame file. ";
                    playGameCmd = dgvTools.Rows[0].Cells["colToolsCommandLine"].Value.ToString();
                    string showFile = dgvTools.Rows[1].Cells["colToolsFileName"].Value.ToString();
                    if (!string.IsNullOrEmpty(showFile))
                        showGameFile = showFile;
                    else
                        lblErrors.Text = "Can't find ShowGame file. ";
                    showGameCmd = dgvTools.Rows[1].Cells["colToolsCommandLine"].Value.ToString();

                    // Save bot extensions to BotsList.txt
                    FillBotExtensions();
                    if (botExtensions.Count > 0)
                    {
                        StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "/BotsList.txt");
                        foreach (BotExtension be in botExtensions)
                        {
                            streamWriter.WriteLine(be.BotCommandLine() + " | " + be.BotExt());
                        }
                        streamWriter.Close();
                    }
                    break;

                default:
                    break;
            }
            lblErrors.Text = errorString;
            WriteSettingsToIni(tabNum);
            //backgroundWorker.WorkerReportsProgress
        }

        // Fill bot extensions
        private void FillBotExtensions()
        {
            botExtensions = new List<BotExtension>();
            int i = 0;
            foreach (DataGridViewRow row in dgvBots.Rows)
            {
                if (row.Cells["colBotsExtensions"].Value != null)
                {
                    if (!string.IsNullOrEmpty(row.Cells["colBotsExtensions"].Value.ToString()))
                    {
                        string cmd = (row.Cells["colBotsCommandLine"].Value != null) ? row.Cells["colBotsCommandLine"].Value.ToString() : string.Empty;
                        string ext = "." + row.Cells["colBotsExtensions"].Value.ToString().TrimStart('.').TrimEnd(' ');
                        BotExtension be = new BotExtension(i, cmd, ext);
                        botExtensions.Add(be);
                        ++i;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettingsChanges(tabControlMain.SelectedIndex, true);
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            try
            {
                dlgPathFolder.SelectedPath = string.Empty;
                if (DialogResult.OK == dlgPathFolder.ShowDialog())
                {
                    string path = dlgPathFolder.SelectedPath.TrimEnd('\\').TrimEnd('/');
                    tbTools.Text = path;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnMaps_Click(object sender, EventArgs e)
        {
            try
            {
                dlgPathFolder.SelectedPath = string.Empty;
                if (DialogResult.OK == dlgPathFolder.ShowDialog())
                {
                    string path = dlgPathFolder.SelectedPath.TrimEnd('\\').TrimEnd('/');
                    tbMaps.Text = path;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnExampleBots_Click(object sender, EventArgs e)
        {
            try
            {
                dlgPathFolder.SelectedPath = string.Empty;
                if (DialogResult.OK == dlgPathFolder.ShowDialog())
                {
                    string path = dlgPathFolder.SelectedPath.TrimEnd('\\').TrimEnd('/');
                    tbExampleBots.Text = path;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.CancelAsync();
            threadStopFlag = true;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DataGridViewResultOnFly(botFights[(int)e.UserState - 1]);
            this.Text = dgvResultGrid.Rows.Count.ToString() + " of " +
                        botFights.Count.ToString() + " - " + formTitleStr;
        }

        private void dgvResultGrid_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if ((e.Column == dgvResultGrid.Columns["colID"]) || (e.Column == dgvResultGrid.Columns["colTurns"]))
            {
                if ((e.CellValue1 == null) || (e.CellValue1.ToString() == ""))
                {
                    if ((e.CellValue2 == null) || (e.CellValue2.ToString() == ""))
                        e.SortResult = 0;
                    else
                        e.SortResult = -1;
                }
                else
                {
                    if ((e.CellValue2 == null) || (e.CellValue2.ToString() == ""))
                        e.SortResult = 1;
                    else
                    {
                        string value1 = string.Empty;
                        string value2 = string.Empty;
                        if (e.Column == dgvResultGrid.Columns["colTurns"])
                        {
                            value1 = e.CellValue1.ToString().Substring(5);
                            value2 = e.CellValue2.ToString().Substring(5);
                        }
                        else
                        {
                            value1 = e.CellValue1.ToString();
                            value2 = e.CellValue2.ToString();
                        }

                        int p1 = Convert.ToInt32(value1);
                        int p2 = Convert.ToInt32(value2);
                        if (p1 < p2)
                            e.SortResult = -1;
                        else if (p1 > p2)
                            e.SortResult = 1;
                        else
                            e.SortResult = 0;
                    }
                }
                e.Handled = true;
            }

        }

        private void pnlSetupBottom_MouseMove(object sender, MouseEventArgs e)
        {
            if ((Math.Abs(e.Y -  pnlSetupBottom.Size.Height) <= 5) && (tabControlMain.SelectedIndex == 2))
            {
                this.Cursor = Cursors.HSplit;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void pnlSetupBottom_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pnlSetupBottom_MouseUp(object sender, MouseEventArgs e)
        {
            int prevY = pnlSetup.Size.Height;
            Size size = new System.Drawing.Size(pnlSetup.Size.Width, e.Y - pnlSetupBottom.Size.Height + prevY);
            pnlSetup.Size = size;
        }
    }
}
