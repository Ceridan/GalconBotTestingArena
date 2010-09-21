namespace GalconBotTestingArena
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSetup = new System.Windows.Forms.Panel();
            this.pnlSetupBody = new System.Windows.Forms.Panel();
            this.pnlSetupBodyFightSettings = new System.Windows.Forms.Panel();
            this.tbTimeAmount = new System.Windows.Forms.TextBox();
            this.lblTimeAmount = new System.Windows.Forms.Label();
            this.tbTurnAmount = new System.Windows.Forms.TextBox();
            this.lblTurnAmount = new System.Windows.Forms.Label();
            this.cmbChooseOpponentBot = new System.Windows.Forms.ComboBox();
            this.lblChooseOpponentBot = new System.Windows.Forms.Label();
            this.gbFight = new System.Windows.Forms.GroupBox();
            this.btnFight = new System.Windows.Forms.Button();
            this.cmbMap = new System.Windows.Forms.ComboBox();
            this.lblMap = new System.Windows.Forms.Label();
            this.gbTestOptions = new System.Windows.Forms.GroupBox();
            this.cbSwapPlayers = new System.Windows.Forms.CheckBox();
            this.cbMirror = new System.Windows.Forms.CheckBox();
            this.cbOtherMyBots = new System.Windows.Forms.CheckBox();
            this.cbExampleBots = new System.Windows.Forms.CheckBox();
            this.cmbChooseMyBot = new System.Windows.Forms.ComboBox();
            this.lblChooseMyBot = new System.Windows.Forms.Label();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.pnlSetupBodyPathSettings = new System.Windows.Forms.Panel();
            this.btnMyBotsPath = new System.Windows.Forms.Button();
            this.tbMyBotsPath = new System.Windows.Forms.TextBox();
            this.lblMyBotsPath = new System.Windows.Forms.Label();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.tbStarterPackagePath = new System.Windows.Forms.TextBox();
            this.lblStarterPackagePath = new System.Windows.Forms.Label();
            this.btnStarterPackagePath = new System.Windows.Forms.Button();
            this.pnlSetupBottom = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pnlSetupHeader = new System.Windows.Forms.Panel();
            this.lblSetupHeader = new System.Windows.Forms.Label();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.pnlResultBody = new System.Windows.Forms.Panel();
            this.dgvResultGrid = new System.Windows.Forms.DataGridView();
            this.pnlResultBottom = new System.Windows.Forms.Panel();
            this.cbClearLogs = new System.Windows.Forms.CheckBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlResultHeader = new System.Windows.Forms.Panel();
            this.lblResultHeader = new System.Windows.Forms.Label();
            this.dlgPathFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlayer1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlayer2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWinner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colErrors = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTurns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colViewGame = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlSetup.SuspendLayout();
            this.pnlSetupBody.SuspendLayout();
            this.pnlSetupBodyFightSettings.SuspendLayout();
            this.gbFight.SuspendLayout();
            this.gbTestOptions.SuspendLayout();
            this.pnlSetupBodyPathSettings.SuspendLayout();
            this.pnlSetupBottom.SuspendLayout();
            this.pnlSetupHeader.SuspendLayout();
            this.pnlResult.SuspendLayout();
            this.pnlResultBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultGrid)).BeginInit();
            this.pnlResultBottom.SuspendLayout();
            this.pnlResultHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSetup
            // 
            this.pnlSetup.Controls.Add(this.pnlSetupBody);
            this.pnlSetup.Controls.Add(this.pnlSetupBottom);
            this.pnlSetup.Controls.Add(this.pnlSetupHeader);
            this.pnlSetup.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSetup.Location = new System.Drawing.Point(0, 0);
            this.pnlSetup.Name = "pnlSetup";
            this.pnlSetup.Size = new System.Drawing.Size(732, 260);
            this.pnlSetup.TabIndex = 0;
            // 
            // pnlSetupBody
            // 
            this.pnlSetupBody.Controls.Add(this.pnlSetupBodyFightSettings);
            this.pnlSetupBody.Controls.Add(this.pnlSetupBodyPathSettings);
            this.pnlSetupBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSetupBody.Location = new System.Drawing.Point(0, 25);
            this.pnlSetupBody.Name = "pnlSetupBody";
            this.pnlSetupBody.Size = new System.Drawing.Size(732, 210);
            this.pnlSetupBody.TabIndex = 2;
            // 
            // pnlSetupBodyFightSettings
            // 
            this.pnlSetupBodyFightSettings.Controls.Add(this.tbTimeAmount);
            this.pnlSetupBodyFightSettings.Controls.Add(this.lblTimeAmount);
            this.pnlSetupBodyFightSettings.Controls.Add(this.tbTurnAmount);
            this.pnlSetupBodyFightSettings.Controls.Add(this.lblTurnAmount);
            this.pnlSetupBodyFightSettings.Controls.Add(this.cmbChooseOpponentBot);
            this.pnlSetupBodyFightSettings.Controls.Add(this.lblChooseOpponentBot);
            this.pnlSetupBodyFightSettings.Controls.Add(this.gbFight);
            this.pnlSetupBodyFightSettings.Controls.Add(this.cmbMap);
            this.pnlSetupBodyFightSettings.Controls.Add(this.lblMap);
            this.pnlSetupBodyFightSettings.Controls.Add(this.gbTestOptions);
            this.pnlSetupBodyFightSettings.Controls.Add(this.cmbChooseMyBot);
            this.pnlSetupBodyFightSettings.Controls.Add(this.lblChooseMyBot);
            this.pnlSetupBodyFightSettings.Controls.Add(this.lblStep2);
            this.pnlSetupBodyFightSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSetupBodyFightSettings.Location = new System.Drawing.Point(0, 82);
            this.pnlSetupBodyFightSettings.Name = "pnlSetupBodyFightSettings";
            this.pnlSetupBodyFightSettings.Size = new System.Drawing.Size(732, 128);
            this.pnlSetupBodyFightSettings.TabIndex = 5;
            // 
            // tbTimeAmount
            // 
            this.tbTimeAmount.Location = new System.Drawing.Point(455, 97);
            this.tbTimeAmount.Name = "tbTimeAmount";
            this.tbTimeAmount.Size = new System.Drawing.Size(74, 20);
            this.tbTimeAmount.TabIndex = 19;
            this.tbTimeAmount.Text = "1000";
            this.tbTimeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTimeAmount
            // 
            this.lblTimeAmount.AutoSize = true;
            this.lblTimeAmount.Location = new System.Drawing.Point(356, 100);
            this.lblTimeAmount.Name = "lblTimeAmount";
            this.lblTimeAmount.Size = new System.Drawing.Size(93, 13);
            this.lblTimeAmount.TabIndex = 18;
            this.lblTimeAmount.Text = "Time amount (ms):";
            // 
            // tbTurnAmount
            // 
            this.tbTurnAmount.Location = new System.Drawing.Point(276, 97);
            this.tbTurnAmount.Name = "tbTurnAmount";
            this.tbTurnAmount.Size = new System.Drawing.Size(74, 20);
            this.tbTurnAmount.TabIndex = 17;
            this.tbTurnAmount.Text = "1000";
            this.tbTurnAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTurnAmount
            // 
            this.lblTurnAmount.AutoSize = true;
            this.lblTurnAmount.Location = new System.Drawing.Point(195, 100);
            this.lblTurnAmount.Name = "lblTurnAmount";
            this.lblTurnAmount.Size = new System.Drawing.Size(75, 13);
            this.lblTurnAmount.TabIndex = 16;
            this.lblTurnAmount.Text = "Turns amount:";
            // 
            // cmbChooseOpponentBot
            // 
            this.cmbChooseOpponentBot.FormattingEnabled = true;
            this.cmbChooseOpponentBot.Location = new System.Drawing.Point(547, 22);
            this.cmbChooseOpponentBot.Name = "cmbChooseOpponentBot";
            this.cmbChooseOpponentBot.Size = new System.Drawing.Size(169, 21);
            this.cmbChooseOpponentBot.TabIndex = 15;
            // 
            // lblChooseOpponentBot
            // 
            this.lblChooseOpponentBot.AutoSize = true;
            this.lblChooseOpponentBot.Location = new System.Drawing.Point(417, 25);
            this.lblChooseOpponentBot.Name = "lblChooseOpponentBot";
            this.lblChooseOpponentBot.Size = new System.Drawing.Size(112, 13);
            this.lblChooseOpponentBot.TabIndex = 14;
            this.lblChooseOpponentBot.Text = "Choose opponent bot:";
            // 
            // gbFight
            // 
            this.gbFight.Controls.Add(this.btnFight);
            this.gbFight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbFight.Location = new System.Drawing.Point(547, 49);
            this.gbFight.Name = "gbFight";
            this.gbFight.Size = new System.Drawing.Size(169, 69);
            this.gbFight.TabIndex = 13;
            this.gbFight.TabStop = false;
            this.gbFight.Text = "Step 3. Fight!";
            // 
            // btnFight
            // 
            this.btnFight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFight.Location = new System.Drawing.Point(6, 19);
            this.btnFight.Name = "btnFight";
            this.btnFight.Size = new System.Drawing.Size(157, 38);
            this.btnFight.TabIndex = 11;
            this.btnFight.Text = "FIGHT!";
            this.btnFight.UseVisualStyleBackColor = true;
            this.btnFight.Click += new System.EventHandler(this.btnFight_Click);
            // 
            // cmbMap
            // 
            this.cmbMap.FormattingEnabled = true;
            this.cmbMap.Location = new System.Drawing.Point(79, 97);
            this.cmbMap.Name = "cmbMap";
            this.cmbMap.Size = new System.Drawing.Size(102, 21);
            this.cmbMap.TabIndex = 9;
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Location = new System.Drawing.Point(4, 100);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(69, 13);
            this.lblMap.TabIndex = 8;
            this.lblMap.Text = "Choose map:";
            // 
            // gbTestOptions
            // 
            this.gbTestOptions.Controls.Add(this.cbSwapPlayers);
            this.gbTestOptions.Controls.Add(this.cbMirror);
            this.gbTestOptions.Controls.Add(this.cbOtherMyBots);
            this.gbTestOptions.Controls.Add(this.cbExampleBots);
            this.gbTestOptions.Location = new System.Drawing.Point(6, 49);
            this.gbTestOptions.Name = "gbTestOptions";
            this.gbTestOptions.Size = new System.Drawing.Size(523, 45);
            this.gbTestOptions.TabIndex = 7;
            this.gbTestOptions.TabStop = false;
            this.gbTestOptions.Text = "Check test options (Options will work only if you choose \'ALL\' opponent bots exce" +
                "pt \'Swap player\' options)";
            // 
            // cbSwapPlayers
            // 
            this.cbSwapPlayers.AutoSize = true;
            this.cbSwapPlayers.Checked = true;
            this.cbSwapPlayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSwapPlayers.Location = new System.Drawing.Point(388, 19);
            this.cbSwapPlayers.Name = "cbSwapPlayers";
            this.cbSwapPlayers.Size = new System.Drawing.Size(127, 17);
            this.cbSwapPlayers.TabIndex = 3;
            this.cbSwapPlayers.Text = "Swap player numbers";
            this.cbSwapPlayers.UseVisualStyleBackColor = true;
            // 
            // cbMirror
            // 
            this.cbMirror.AutoSize = true;
            this.cbMirror.Checked = true;
            this.cbMirror.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMirror.Location = new System.Drawing.Point(298, 19);
            this.cbMirror.Name = "cbMirror";
            this.cbMirror.Size = new System.Drawing.Size(84, 17);
            this.cbMirror.TabIndex = 2;
            this.cbMirror.Text = "Mirror match";
            this.cbMirror.UseVisualStyleBackColor = true;
            // 
            // cbOtherMyBots
            // 
            this.cbOtherMyBots.AutoSize = true;
            this.cbOtherMyBots.Checked = true;
            this.cbOtherMyBots.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOtherMyBots.Location = new System.Drawing.Point(148, 19);
            this.cbOtherMyBots.Name = "cbOtherMyBots";
            this.cbOtherMyBots.Size = new System.Drawing.Size(144, 17);
            this.cbOtherMyBots.TabIndex = 1;
            this.cbOtherMyBots.Text = "Fight with other your bots";
            this.cbOtherMyBots.UseVisualStyleBackColor = true;
            // 
            // cbExampleBots
            // 
            this.cbExampleBots.AutoSize = true;
            this.cbExampleBots.Checked = true;
            this.cbExampleBots.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbExampleBots.Location = new System.Drawing.Point(6, 19);
            this.cbExampleBots.Name = "cbExampleBots";
            this.cbExampleBots.Size = new System.Drawing.Size(136, 17);
            this.cbExampleBots.TabIndex = 0;
            this.cbExampleBots.Text = "Fight with example bots";
            this.cbExampleBots.UseVisualStyleBackColor = true;
            // 
            // cmbChooseMyBot
            // 
            this.cmbChooseMyBot.FormattingEnabled = true;
            this.cmbChooseMyBot.Location = new System.Drawing.Point(129, 22);
            this.cmbChooseMyBot.Name = "cmbChooseMyBot";
            this.cmbChooseMyBot.Size = new System.Drawing.Size(169, 21);
            this.cmbChooseMyBot.TabIndex = 6;
            // 
            // lblChooseMyBot
            // 
            this.lblChooseMyBot.AutoSize = true;
            this.lblChooseMyBot.Location = new System.Drawing.Point(4, 25);
            this.lblChooseMyBot.Name = "lblChooseMyBot";
            this.lblChooseMyBot.Size = new System.Drawing.Size(119, 13);
            this.lblChooseMyBot.TabIndex = 5;
            this.lblChooseMyBot.Text = "Choose your bot to test:";
            // 
            // lblStep2
            // 
            this.lblStep2.AutoSize = true;
            this.lblStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStep2.Location = new System.Drawing.Point(3, 3);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(128, 13);
            this.lblStep2.TabIndex = 4;
            this.lblStep2.Text = "Step 2. Fight settings";
            // 
            // pnlSetupBodyPathSettings
            // 
            this.pnlSetupBodyPathSettings.Controls.Add(this.btnMyBotsPath);
            this.pnlSetupBodyPathSettings.Controls.Add(this.tbMyBotsPath);
            this.pnlSetupBodyPathSettings.Controls.Add(this.lblMyBotsPath);
            this.pnlSetupBodyPathSettings.Controls.Add(this.lblStep1);
            this.pnlSetupBodyPathSettings.Controls.Add(this.tbStarterPackagePath);
            this.pnlSetupBodyPathSettings.Controls.Add(this.lblStarterPackagePath);
            this.pnlSetupBodyPathSettings.Controls.Add(this.btnStarterPackagePath);
            this.pnlSetupBodyPathSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSetupBodyPathSettings.Location = new System.Drawing.Point(0, 0);
            this.pnlSetupBodyPathSettings.Name = "pnlSetupBodyPathSettings";
            this.pnlSetupBodyPathSettings.Size = new System.Drawing.Size(732, 82);
            this.pnlSetupBodyPathSettings.TabIndex = 4;
            // 
            // btnMyBotsPath
            // 
            this.btnMyBotsPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMyBotsPath.Location = new System.Drawing.Point(678, 54);
            this.btnMyBotsPath.Name = "btnMyBotsPath";
            this.btnMyBotsPath.Size = new System.Drawing.Size(38, 20);
            this.btnMyBotsPath.TabIndex = 6;
            this.btnMyBotsPath.Text = "…";
            this.btnMyBotsPath.UseVisualStyleBackColor = true;
            this.btnMyBotsPath.Click += new System.EventHandler(this.btnMyBotsPath_Click);
            // 
            // tbMyBotsPath
            // 
            this.tbMyBotsPath.Location = new System.Drawing.Point(177, 55);
            this.tbMyBotsPath.Name = "tbMyBotsPath";
            this.tbMyBotsPath.ReadOnly = true;
            this.tbMyBotsPath.Size = new System.Drawing.Size(495, 20);
            this.tbMyBotsPath.TabIndex = 5;
            // 
            // lblMyBotsPath
            // 
            this.lblMyBotsPath.AutoSize = true;
            this.lblMyBotsPath.Location = new System.Drawing.Point(3, 58);
            this.lblMyBotsPath.Name = "lblMyBotsPath";
            this.lblMyBotsPath.Size = new System.Drawing.Size(116, 13);
            this.lblMyBotsPath.TabIndex = 4;
            this.lblMyBotsPath.Text = "Input path to your bots:";
            // 
            // lblStep1
            // 
            this.lblStep1.AutoSize = true;
            this.lblStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStep1.Location = new System.Drawing.Point(3, 12);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(132, 13);
            this.lblStep1.TabIndex = 3;
            this.lblStep1.Text = "Step 1. Paths settings";
            // 
            // tbStarterPackagePath
            // 
            this.tbStarterPackagePath.Location = new System.Drawing.Point(177, 31);
            this.tbStarterPackagePath.Name = "tbStarterPackagePath";
            this.tbStarterPackagePath.ReadOnly = true;
            this.tbStarterPackagePath.Size = new System.Drawing.Size(495, 20);
            this.tbStarterPackagePath.TabIndex = 2;
            // 
            // lblStarterPackagePath
            // 
            this.lblStarterPackagePath.AutoSize = true;
            this.lblStarterPackagePath.Location = new System.Drawing.Point(3, 34);
            this.lblStarterPackagePath.Name = "lblStarterPackagePath";
            this.lblStarterPackagePath.Size = new System.Drawing.Size(168, 13);
            this.lblStarterPackagePath.TabIndex = 0;
            this.lblStarterPackagePath.Text = "Input path to the starter package: ";
            // 
            // btnStarterPackagePath
            // 
            this.btnStarterPackagePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStarterPackagePath.Location = new System.Drawing.Point(678, 30);
            this.btnStarterPackagePath.Name = "btnStarterPackagePath";
            this.btnStarterPackagePath.Size = new System.Drawing.Size(38, 20);
            this.btnStarterPackagePath.TabIndex = 1;
            this.btnStarterPackagePath.Text = "…";
            this.btnStarterPackagePath.UseVisualStyleBackColor = true;
            this.btnStarterPackagePath.Click += new System.EventHandler(this.btnStarterPackagePath_Click);
            // 
            // pnlSetupBottom
            // 
            this.pnlSetupBottom.Controls.Add(this.progressBar);
            this.pnlSetupBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSetupBottom.Location = new System.Drawing.Point(0, 235);
            this.pnlSetupBottom.Name = "pnlSetupBottom";
            this.pnlSetupBottom.Size = new System.Drawing.Size(732, 25);
            this.pnlSetupBottom.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 1);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(732, 23);
            this.progressBar.TabIndex = 0;
            // 
            // pnlSetupHeader
            // 
            this.pnlSetupHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSetupHeader.Controls.Add(this.lblSetupHeader);
            this.pnlSetupHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSetupHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSetupHeader.Name = "pnlSetupHeader";
            this.pnlSetupHeader.Size = new System.Drawing.Size(732, 25);
            this.pnlSetupHeader.TabIndex = 0;
            // 
            // lblSetupHeader
            // 
            this.lblSetupHeader.AutoSize = true;
            this.lblSetupHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSetupHeader.Location = new System.Drawing.Point(3, 6);
            this.lblSetupHeader.Name = "lblSetupHeader";
            this.lblSetupHeader.Size = new System.Drawing.Size(227, 16);
            this.lblSetupHeader.TabIndex = 0;
            this.lblSetupHeader.Text = "Galcon Bot Testing Arena setup";
            // 
            // pnlResult
            // 
            this.pnlResult.Controls.Add(this.pnlResultBody);
            this.pnlResult.Controls.Add(this.pnlResultBottom);
            this.pnlResult.Controls.Add(this.pnlResultHeader);
            this.pnlResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResult.Location = new System.Drawing.Point(0, 260);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(732, 237);
            this.pnlResult.TabIndex = 1;
            // 
            // pnlResultBody
            // 
            this.pnlResultBody.Controls.Add(this.dgvResultGrid);
            this.pnlResultBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultBody.Location = new System.Drawing.Point(0, 25);
            this.pnlResultBody.Name = "pnlResultBody";
            this.pnlResultBody.Size = new System.Drawing.Size(732, 162);
            this.pnlResultBody.TabIndex = 2;
            // 
            // dgvResultGrid
            // 
            this.dgvResultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colMap,
            this.colPlayer1,
            this.colPlayer2,
            this.colWinner,
            this.colErrors,
            this.colTurns,
            this.colViewGame});
            this.dgvResultGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvResultGrid.Name = "dgvResultGrid";
            this.dgvResultGrid.Size = new System.Drawing.Size(732, 162);
            this.dgvResultGrid.TabIndex = 0;
            this.dgvResultGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResultGrid_CellContentClick);
            // 
            // pnlResultBottom
            // 
            this.pnlResultBottom.Controls.Add(this.cbClearLogs);
            this.pnlResultBottom.Controls.Add(this.lblTotal);
            this.pnlResultBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlResultBottom.Location = new System.Drawing.Point(0, 187);
            this.pnlResultBottom.Name = "pnlResultBottom";
            this.pnlResultBottom.Size = new System.Drawing.Size(732, 50);
            this.pnlResultBottom.TabIndex = 1;
            // 
            // cbClearLogs
            // 
            this.cbClearLogs.AutoSize = true;
            this.cbClearLogs.Checked = true;
            this.cbClearLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbClearLogs.Location = new System.Drawing.Point(6, 30);
            this.cbClearLogs.Name = "cbClearLogs";
            this.cbClearLogs.Size = new System.Drawing.Size(131, 17);
            this.cbClearLogs.TabIndex = 1;
            this.cbClearLogs.Text = "Clear log files on close";
            this.cbClearLogs.UseVisualStyleBackColor = true;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotal.Location = new System.Drawing.Point(9, 3);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 13);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total:";
            // 
            // pnlResultHeader
            // 
            this.pnlResultHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlResultHeader.Controls.Add(this.lblResultHeader);
            this.pnlResultHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResultHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlResultHeader.Name = "pnlResultHeader";
            this.pnlResultHeader.Size = new System.Drawing.Size(732, 25);
            this.pnlResultHeader.TabIndex = 0;
            // 
            // lblResultHeader
            // 
            this.lblResultHeader.AutoSize = true;
            this.lblResultHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResultHeader.Location = new System.Drawing.Point(3, 6);
            this.lblResultHeader.Name = "lblResultHeader";
            this.lblResultHeader.Size = new System.Drawing.Size(227, 16);
            this.lblResultHeader.TabIndex = 0;
            this.lblResultHeader.Text = "Galcon Bot Testing Arena result";
            // 
            // colID
            // 
            this.colID.FillWeight = 30F;
            this.colID.HeaderText = "ID";
            this.colID.MinimumWidth = 30;
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Width = 50;
            // 
            // colMap
            // 
            this.colMap.FillWeight = 60F;
            this.colMap.HeaderText = "Map";
            this.colMap.MinimumWidth = 60;
            this.colMap.Name = "colMap";
            this.colMap.ReadOnly = true;
            this.colMap.Width = 60;
            // 
            // colPlayer1
            // 
            this.colPlayer1.HeaderText = "Player 1";
            this.colPlayer1.MinimumWidth = 100;
            this.colPlayer1.Name = "colPlayer1";
            this.colPlayer1.ReadOnly = true;
            // 
            // colPlayer2
            // 
            this.colPlayer2.HeaderText = "Player 2";
            this.colPlayer2.MinimumWidth = 100;
            this.colPlayer2.Name = "colPlayer2";
            this.colPlayer2.ReadOnly = true;
            // 
            // colWinner
            // 
            this.colWinner.HeaderText = "Result";
            this.colWinner.MinimumWidth = 100;
            this.colWinner.Name = "colWinner";
            this.colWinner.ReadOnly = true;
            // 
            // colErrors
            // 
            this.colErrors.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colErrors.HeaderText = "Errors";
            this.colErrors.MinimumWidth = 100;
            this.colErrors.Name = "colErrors";
            this.colErrors.ReadOnly = true;
            // 
            // colTurns
            // 
            this.colTurns.FillWeight = 60F;
            this.colTurns.HeaderText = "Turns";
            this.colTurns.MinimumWidth = 60;
            this.colTurns.Name = "colTurns";
            this.colTurns.ReadOnly = true;
            // 
            // colViewGame
            // 
            this.colViewGame.FillWeight = 50F;
            this.colViewGame.HeaderText = "View Game";
            this.colViewGame.Name = "colViewGame";
            this.colViewGame.ReadOnly = true;
            this.colViewGame.Width = 50;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 497);
            this.Controls.Add(this.pnlResult);
            this.Controls.Add(this.pnlSetup);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Galcon Bot Testing Arena - version 1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlSetup.ResumeLayout(false);
            this.pnlSetupBody.ResumeLayout(false);
            this.pnlSetupBodyFightSettings.ResumeLayout(false);
            this.pnlSetupBodyFightSettings.PerformLayout();
            this.gbFight.ResumeLayout(false);
            this.gbTestOptions.ResumeLayout(false);
            this.gbTestOptions.PerformLayout();
            this.pnlSetupBodyPathSettings.ResumeLayout(false);
            this.pnlSetupBodyPathSettings.PerformLayout();
            this.pnlSetupBottom.ResumeLayout(false);
            this.pnlSetupHeader.ResumeLayout(false);
            this.pnlSetupHeader.PerformLayout();
            this.pnlResult.ResumeLayout(false);
            this.pnlResultBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultGrid)).EndInit();
            this.pnlResultBottom.ResumeLayout(false);
            this.pnlResultBottom.PerformLayout();
            this.pnlResultHeader.ResumeLayout(false);
            this.pnlResultHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSetup;
        private System.Windows.Forms.Panel pnlSetupBottom;
        private System.Windows.Forms.Panel pnlSetupHeader;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Panel pnlResultBottom;
        private System.Windows.Forms.Panel pnlResultHeader;
        private System.Windows.Forms.Label lblSetupHeader;
        private System.Windows.Forms.Label lblResultHeader;
        private System.Windows.Forms.Panel pnlSetupBody;
        private System.Windows.Forms.Panel pnlSetupBodyFightSettings;
        private System.Windows.Forms.Panel pnlSetupBodyPathSettings;
        private System.Windows.Forms.TextBox tbStarterPackagePath;
        private System.Windows.Forms.Label lblStarterPackagePath;
        private System.Windows.Forms.Button btnStarterPackagePath;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.Label lblMyBotsPath;
        private System.Windows.Forms.Button btnMyBotsPath;
        private System.Windows.Forms.TextBox tbMyBotsPath;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.GroupBox gbTestOptions;
        private System.Windows.Forms.CheckBox cbOtherMyBots;
        private System.Windows.Forms.CheckBox cbExampleBots;
        private System.Windows.Forms.ComboBox cmbChooseMyBot;
        private System.Windows.Forms.Label lblChooseMyBot;
        private System.Windows.Forms.Panel pnlResultBody;
        private System.Windows.Forms.CheckBox cbMirror;
        private System.Windows.Forms.Button btnFight;
        private System.Windows.Forms.ComboBox cmbMap;
        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.GroupBox gbFight;
        private System.Windows.Forms.CheckBox cbSwapPlayers;
        private System.Windows.Forms.DataGridView dgvResultGrid;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.CheckBox cbClearLogs;
        private System.Windows.Forms.FolderBrowserDialog dlgPathFolder;
        private System.Windows.Forms.ComboBox cmbChooseOpponentBot;
        private System.Windows.Forms.Label lblChooseOpponentBot;
        private System.Windows.Forms.TextBox tbTimeAmount;
        private System.Windows.Forms.Label lblTimeAmount;
        private System.Windows.Forms.TextBox tbTurnAmount;
        private System.Windows.Forms.Label lblTurnAmount;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlayer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlayer2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWinner;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErrors;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTurns;
        private System.Windows.Forms.DataGridViewButtonColumn colViewGame;
    }
}

