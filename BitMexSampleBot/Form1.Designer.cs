﻿namespace BitMexSampleBot
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnBuy = new System.Windows.Forms.Button();
            this.btnSell = new System.Windows.Forms.Button();
            this.nudQty = new System.Windows.Forms.NumericUpDown();
            this.chkCancelWhileOrdering = new System.Windows.Forms.CheckBox();
            this.btnCancelOpenOrders = new System.Windows.Forms.Button();
            this.ddlOrderType = new System.Windows.Forms.ComboBox();
            this.ddlNetwork = new System.Windows.Forms.ComboBox();
            this.ddlSymbol = new System.Windows.Forms.ComboBox();
            this.dgvCandles = new System.Windows.Forms.DataGridView();
            this.ddlCandleTimes = new System.Windows.Forms.ComboBox();
            this.gbCandles = new System.Windows.Forms.GroupBox();
            this.lblMA2 = new System.Windows.Forms.Label();
            this.nudMA2 = new System.Windows.Forms.NumericUpDown();
            this.lblMA1 = new System.Windows.Forms.Label();
            this.nudMA1 = new System.Windows.Forms.NumericUpDown();
            this.chkUpdateCandles = new System.Windows.Forms.CheckBox();
            this.txtFirstSellOrderOutput = new System.Windows.Forms.TextBox();
            this.lblFirstSellOrder = new System.Windows.Forms.Label();
            this.txtFirstBuyOrderOutput = new System.Windows.Forms.TextBox();
            this.lblFirstBuyOrder = new System.Windows.Forms.Label();
            this.lblVolume24h = new System.Windows.Forms.Label();
            this.nudVolume24h = new System.Windows.Forms.NumericUpDown();
            this.gbSell = new System.Windows.Forms.GroupBox();
            this.txtSellElementsDivisionOutput = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nudConstantSellDividend = new System.Windows.Forms.NumericUpDown();
            this.lblSellElementsToTake = new System.Windows.Forms.Label();
            this.nudSellElementsToTake = new System.Windows.Forms.NumericUpDown();
            this.gbBuy = new System.Windows.Forms.GroupBox();
            this.txtBuyElementsDivisionOutput = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDividend = new System.Windows.Forms.Label();
            this.lblBuyElementsToTake = new System.Windows.Forms.Label();
            this.nudConstantBuyDividend = new System.Windows.Forms.NumericUpDown();
            this.nudBuyElementsToTake = new System.Windows.Forms.NumericUpDown();
            this.tmrCandleUpdater = new System.Windows.Forms.Timer(this.components);
            this.rdoBuy = new System.Windows.Forms.RadioButton();
            this.rdoSell = new System.Windows.Forms.RadioButton();
            this.rdoSwitch = new System.Windows.Forms.RadioButton();
            this.gbAutomatedTrading = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsAPIValid = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsAccountBalance = new System.Windows.Forms.ToolStripStatusLabel();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.BalanceExit = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudSellStochk = new System.Windows.Forms.NumericUpDown();
            this.nudBuyStochk = new System.Windows.Forms.NumericUpDown();
            this.lblAutoUnrealizedROEPercent = new System.Windows.Forms.Label();
            this.nudAutoMarketTakeProfitPercent = new System.Windows.Forms.NumericUpDown();
            this.chkAutoMarketTakeProfits = new System.Windows.Forms.CheckBox();
            this.ddlAutoOrderType = new System.Windows.Forms.ComboBox();
            this.nudAutoQuantity = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetPrice = new System.Windows.Forms.Button();
            this.btnAutomatedTrading = new System.Windows.Forms.Button();
            this.tmrAutoTradeExecution = new System.Windows.Forms.Timer(this.components);
            this.btnAccountBalance = new System.Windows.Forms.Button();
            this.nudStopPercent = new System.Windows.Forms.NumericUpDown();
            this.btnManualSetStop = new System.Windows.Forms.Button();
            this.txtAPIKey = new System.Windows.Forms.TextBox();
            this.txtAPISecret = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuyOverTimeOrder = new System.Windows.Forms.Button();
            this.btnSellOverTimeOrder = new System.Windows.Forms.Button();
            this.tmrTradeOverTime = new System.Windows.Forms.Timer(this.components);
            this.nudOverTimeContracts = new System.Windows.Forms.NumericUpDown();
            this.nudOverTimeInterval = new System.Windows.Forms.NumericUpDown();
            this.nudOverTimeIntervalCount = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOverTimeStop = new System.Windows.Forms.Button();
            this.lblPriceBuy = new System.Windows.Forms.Label();
            this.nudPriceBuy = new System.Windows.Forms.NumericUpDown();
            this.nudPriceSell = new System.Windows.Forms.NumericUpDown();
            this.lblPriceSell = new System.Windows.Forms.Label();
            this.tmrUpdateBBMiddle = new System.Windows.Forms.Timer(this.components);
            this.tmrUpdateBuySellFirstPriceOrders = new System.Windows.Forms.Timer(this.components);
            this.LoggingTextBox = new System.Windows.Forms.TextBox();
            this.gbOpenOrders = new System.Windows.Forms.GroupBox();
            this.dgvOpenOrders = new System.Windows.Forms.DataGridView();
            this.gbOpenPositions = new System.Windows.Forms.GroupBox();
            this.dgvOpenPositions = new System.Windows.Forms.DataGridView();
            this.tmrTradesKingCoinsReader = new System.Windows.Forms.Timer(this.components);
            this.lbTrend24h = new System.Windows.Forms.Label();
            this.lbTrend1h = new System.Windows.Forms.Label();
            this.txtTrend24h = new System.Windows.Forms.TextBox();
            this.txtTrend1h = new System.Windows.Forms.TextBox();
            this.DumpInput = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.PumpInput = new System.Windows.Forms.NumericUpDown();
            this.KillAll = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCandles)).BeginInit();
            this.gbCandles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMA2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMA1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolume24h)).BeginInit();
            this.gbSell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudConstantSellDividend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSellElementsToTake)).BeginInit();
            this.gbBuy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudConstantBuyDividend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuyElementsToTake)).BeginInit();
            this.gbAutomatedTrading.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BalanceExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSellStochk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuyStochk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAutoMarketTakeProfitPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAutoQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOverTimeContracts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOverTimeInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOverTimeIntervalCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriceBuy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriceSell)).BeginInit();
            this.gbOpenOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenOrders)).BeginInit();
            this.gbOpenPositions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenPositions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DumpInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PumpInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KillAll)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBuy
            // 
            this.btnBuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuy.Location = new System.Drawing.Point(13, 32);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(75, 23);
            this.btnBuy.TabIndex = 0;
            this.btnBuy.Text = "Buy";
            this.btnBuy.UseVisualStyleBackColor = false;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // btnSell
            // 
            this.btnSell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSell.Location = new System.Drawing.Point(197, 32);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(75, 23);
            this.btnSell.TabIndex = 1;
            this.btnSell.Text = "Sell";
            this.btnSell.UseVisualStyleBackColor = false;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // nudQty
            // 
            this.nudQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudQty.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudQty.Location = new System.Drawing.Point(117, 34);
            this.nudQty.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQty.Name = "nudQty";
            this.nudQty.Size = new System.Drawing.Size(67, 19);
            this.nudQty.TabIndex = 2;
            this.nudQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQty.ValueChanged += new System.EventHandler(this.nudQty_ValueChanged);
            // 
            // chkCancelWhileOrdering
            // 
            this.chkCancelWhileOrdering.AutoSize = true;
            this.chkCancelWhileOrdering.Checked = true;
            this.chkCancelWhileOrdering.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCancelWhileOrdering.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCancelWhileOrdering.Location = new System.Drawing.Point(13, 58);
            this.chkCancelWhileOrdering.Name = "chkCancelWhileOrdering";
            this.chkCancelWhileOrdering.Size = new System.Drawing.Size(132, 17);
            this.chkCancelWhileOrdering.TabIndex = 3;
            this.chkCancelWhileOrdering.Text = "Cancel While Ordering";
            this.chkCancelWhileOrdering.UseVisualStyleBackColor = true;
            // 
            // btnCancelOpenOrders
            // 
            this.btnCancelOpenOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCancelOpenOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelOpenOrders.Location = new System.Drawing.Point(197, 58);
            this.btnCancelOpenOrders.Name = "btnCancelOpenOrders";
            this.btnCancelOpenOrders.Size = new System.Drawing.Size(75, 23);
            this.btnCancelOpenOrders.TabIndex = 4;
            this.btnCancelOpenOrders.Text = "Cancel";
            this.btnCancelOpenOrders.UseVisualStyleBackColor = false;
            this.btnCancelOpenOrders.Click += new System.EventHandler(this.btnCancelOpenOrders_Click);
            // 
            // ddlOrderType
            // 
            this.ddlOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlOrderType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlOrderType.FormattingEnabled = true;
            this.ddlOrderType.Items.AddRange(new object[] {
            "Limit Post Only",
            "Market"});
            this.ddlOrderType.Location = new System.Drawing.Point(13, 5);
            this.ddlOrderType.Name = "ddlOrderType";
            this.ddlOrderType.Size = new System.Drawing.Size(98, 21);
            this.ddlOrderType.TabIndex = 5;
            // 
            // ddlNetwork
            // 
            this.ddlNetwork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlNetwork.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlNetwork.FormattingEnabled = true;
            this.ddlNetwork.Items.AddRange(new object[] {
            "TestNet",
            "RealNet"});
            this.ddlNetwork.Location = new System.Drawing.Point(117, 5);
            this.ddlNetwork.Name = "ddlNetwork";
            this.ddlNetwork.Size = new System.Drawing.Size(73, 21);
            this.ddlNetwork.TabIndex = 6;
            this.ddlNetwork.SelectedIndexChanged += new System.EventHandler(this.ddlNetwork_SelectedIndexChanged);
            // 
            // ddlSymbol
            // 
            this.ddlSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSymbol.FormattingEnabled = true;
            this.ddlSymbol.Location = new System.Drawing.Point(199, 5);
            this.ddlSymbol.Name = "ddlSymbol";
            this.ddlSymbol.Size = new System.Drawing.Size(73, 21);
            this.ddlSymbol.TabIndex = 7;
            this.ddlSymbol.SelectedIndexChanged += new System.EventHandler(this.ddlSymbol_SelectedIndexChanged);
            // 
            // dgvCandles
            // 
            this.dgvCandles.AllowUserToAddRows = false;
            this.dgvCandles.AllowUserToDeleteRows = false;
            this.dgvCandles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCandles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCandles.Location = new System.Drawing.Point(13, 232);
            this.dgvCandles.Name = "dgvCandles";
            this.dgvCandles.ReadOnly = true;
            this.dgvCandles.RowHeadersVisible = false;
            this.dgvCandles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCandles.Size = new System.Drawing.Size(943, 58);
            this.dgvCandles.TabIndex = 8;
            this.dgvCandles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCandles_CellFormatting);
            // 
            // ddlCandleTimes
            // 
            this.ddlCandleTimes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCandleTimes.FormattingEnabled = true;
            this.ddlCandleTimes.Items.AddRange(new object[] {
            "1m",
            "5m",
            "1h",
            "1d"});
            this.ddlCandleTimes.Location = new System.Drawing.Point(609, 9);
            this.ddlCandleTimes.Name = "ddlCandleTimes";
            this.ddlCandleTimes.Size = new System.Drawing.Size(46, 21);
            this.ddlCandleTimes.TabIndex = 9;
            this.ddlCandleTimes.SelectedIndexChanged += new System.EventHandler(this.ddlCandleTimes_SelectedIndexChanged);
            // 
            // gbCandles
            // 
            this.gbCandles.Controls.Add(this.lblMA2);
            this.gbCandles.Controls.Add(this.nudMA2);
            this.gbCandles.Controls.Add(this.lblMA1);
            this.gbCandles.Controls.Add(this.nudMA1);
            this.gbCandles.Controls.Add(this.chkUpdateCandles);
            this.gbCandles.Controls.Add(this.ddlCandleTimes);
            this.gbCandles.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCandles.Location = new System.Drawing.Point(19, 297);
            this.gbCandles.Name = "gbCandles";
            this.gbCandles.Size = new System.Drawing.Size(937, 12);
            this.gbCandles.TabIndex = 10;
            this.gbCandles.TabStop = false;
            this.gbCandles.Text = "Candles";
            // 
            // lblMA2
            // 
            this.lblMA2.AutoSize = true;
            this.lblMA2.Location = new System.Drawing.Point(858, 11);
            this.lblMA2.Name = "lblMA2";
            this.lblMA2.Size = new System.Drawing.Size(29, 13);
            this.lblMA2.TabIndex = 16;
            this.lblMA2.Text = "MA2";
            // 
            // nudMA2
            // 
            this.nudMA2.Location = new System.Drawing.Point(891, 9);
            this.nudMA2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMA2.Name = "nudMA2";
            this.nudMA2.Size = new System.Drawing.Size(42, 19);
            this.nudMA2.TabIndex = 15;
            this.nudMA2.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblMA1
            // 
            this.lblMA1.AutoSize = true;
            this.lblMA1.Location = new System.Drawing.Point(777, 11);
            this.lblMA1.Name = "lblMA1";
            this.lblMA1.Size = new System.Drawing.Size(29, 13);
            this.lblMA1.TabIndex = 14;
            this.lblMA1.Text = "MA1";
            // 
            // nudMA1
            // 
            this.nudMA1.Location = new System.Drawing.Point(810, 9);
            this.nudMA1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMA1.Name = "nudMA1";
            this.nudMA1.Size = new System.Drawing.Size(42, 19);
            this.nudMA1.TabIndex = 13;
            this.nudMA1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // chkUpdateCandles
            // 
            this.chkUpdateCandles.AutoSize = true;
            this.chkUpdateCandles.Checked = true;
            this.chkUpdateCandles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateCandles.Location = new System.Drawing.Point(664, 10);
            this.chkUpdateCandles.Name = "chkUpdateCandles";
            this.chkUpdateCandles.Size = new System.Drawing.Size(111, 17);
            this.chkUpdateCandles.TabIndex = 12;
            this.chkUpdateCandles.Text = "Update Every 10s";
            this.chkUpdateCandles.UseVisualStyleBackColor = true;
            this.chkUpdateCandles.CheckedChanged += new System.EventHandler(this.chkUpdateCandles_CheckedChanged);
            // 
            // txtFirstSellOrderOutput
            // 
            this.txtFirstSellOrderOutput.Location = new System.Drawing.Point(634, 207);
            this.txtFirstSellOrderOutput.Margin = new System.Windows.Forms.Padding(2);
            this.txtFirstSellOrderOutput.Name = "txtFirstSellOrderOutput";
            this.txtFirstSellOrderOutput.ReadOnly = true;
            this.txtFirstSellOrderOutput.Size = new System.Drawing.Size(76, 20);
            this.txtFirstSellOrderOutput.TabIndex = 55;
            // 
            // lblFirstSellOrder
            // 
            this.lblFirstSellOrder.AutoSize = true;
            this.lblFirstSellOrder.Location = new System.Drawing.Point(535, 209);
            this.lblFirstSellOrder.Name = "lblFirstSellOrder";
            this.lblFirstSellOrder.Size = new System.Drawing.Size(75, 13);
            this.lblFirstSellOrder.TabIndex = 54;
            this.lblFirstSellOrder.Text = "First Order Sell";
            // 
            // txtFirstBuyOrderOutput
            // 
            this.txtFirstBuyOrderOutput.Location = new System.Drawing.Point(634, 188);
            this.txtFirstBuyOrderOutput.Margin = new System.Windows.Forms.Padding(2);
            this.txtFirstBuyOrderOutput.Name = "txtFirstBuyOrderOutput";
            this.txtFirstBuyOrderOutput.ReadOnly = true;
            this.txtFirstBuyOrderOutput.Size = new System.Drawing.Size(76, 20);
            this.txtFirstBuyOrderOutput.TabIndex = 53;
            // 
            // lblFirstBuyOrder
            // 
            this.lblFirstBuyOrder.AutoSize = true;
            this.lblFirstBuyOrder.Location = new System.Drawing.Point(535, 190);
            this.lblFirstBuyOrder.Name = "lblFirstBuyOrder";
            this.lblFirstBuyOrder.Size = new System.Drawing.Size(76, 13);
            this.lblFirstBuyOrder.TabIndex = 52;
            this.lblFirstBuyOrder.Text = "First Order Buy";
            // 
            // lblVolume24h
            // 
            this.lblVolume24h.AutoSize = true;
            this.lblVolume24h.Location = new System.Drawing.Point(535, 172);
            this.lblVolume24h.Name = "lblVolume24h";
            this.lblVolume24h.Size = new System.Drawing.Size(60, 13);
            this.lblVolume24h.TabIndex = 48;
            this.lblVolume24h.Text = "Volume24h";
            // 
            // nudVolume24h
            // 
            this.nudVolume24h.Location = new System.Drawing.Point(634, 171);
            this.nudVolume24h.Maximum = new decimal(new int[] {
            5000000,
            0,
            0,
            0});
            this.nudVolume24h.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVolume24h.Name = "nudVolume24h";
            this.nudVolume24h.Size = new System.Drawing.Size(76, 20);
            this.nudVolume24h.TabIndex = 47;
            this.nudVolume24h.Value = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.nudVolume24h.ValueChanged += new System.EventHandler(this.nudVolume24h_ValueChanged);
            // 
            // gbSell
            // 
            this.gbSell.Controls.Add(this.txtSellElementsDivisionOutput);
            this.gbSell.Controls.Add(this.label12);
            this.gbSell.Controls.Add(this.label10);
            this.gbSell.Controls.Add(this.nudConstantSellDividend);
            this.gbSell.Controls.Add(this.lblSellElementsToTake);
            this.gbSell.Controls.Add(this.nudSellElementsToTake);
            this.gbSell.Location = new System.Drawing.Point(286, 166);
            this.gbSell.Margin = new System.Windows.Forms.Padding(2);
            this.gbSell.Name = "gbSell";
            this.gbSell.Padding = new System.Windows.Forms.Padding(2);
            this.gbSell.Size = new System.Drawing.Size(245, 55);
            this.gbSell.TabIndex = 46;
            this.gbSell.TabStop = false;
            this.gbSell.Text = "Sell";
            // 
            // txtSellElementsDivisionOutput
            // 
            this.txtSellElementsDivisionOutput.Location = new System.Drawing.Point(9, 35);
            this.txtSellElementsDivisionOutput.Margin = new System.Windows.Forms.Padding(2);
            this.txtSellElementsDivisionOutput.Name = "txtSellElementsDivisionOutput";
            this.txtSellElementsDivisionOutput.ReadOnly = true;
            this.txtSellElementsDivisionOutput.Size = new System.Drawing.Size(76, 20);
            this.txtSellElementsDivisionOutput.TabIndex = 51;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 13);
            this.label12.TabIndex = 50;
            this.label12.Text = "Elements/Dividend";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(140, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 47;
            this.label10.Text = "Dividend";
            // 
            // nudConstantSellDividend
            // 
            this.nudConstantSellDividend.Location = new System.Drawing.Point(194, 37);
            this.nudConstantSellDividend.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudConstantSellDividend.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudConstantSellDividend.Name = "nudConstantSellDividend";
            this.nudConstantSellDividend.Size = new System.Drawing.Size(41, 20);
            this.nudConstantSellDividend.TabIndex = 46;
            this.nudConstantSellDividend.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudConstantSellDividend.ValueChanged += new System.EventHandler(this.nudConstantSellDividend_ValueChanged);
            // 
            // lblSellElementsToTake
            // 
            this.lblSellElementsToTake.AutoSize = true;
            this.lblSellElementsToTake.Location = new System.Drawing.Point(104, 18);
            this.lblSellElementsToTake.Name = "lblSellElementsToTake";
            this.lblSellElementsToTake.Size = new System.Drawing.Size(90, 13);
            this.lblSellElementsToTake.TabIndex = 45;
            this.lblSellElementsToTake.Text = "Elements to Take";
            // 
            // nudSellElementsToTake
            // 
            this.nudSellElementsToTake.Location = new System.Drawing.Point(194, 17);
            this.nudSellElementsToTake.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudSellElementsToTake.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSellElementsToTake.Name = "nudSellElementsToTake";
            this.nudSellElementsToTake.Size = new System.Drawing.Size(40, 20);
            this.nudSellElementsToTake.TabIndex = 44;
            this.nudSellElementsToTake.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudSellElementsToTake.ValueChanged += new System.EventHandler(this.nudSellElementsToTake_ValueChanged);
            // 
            // gbBuy
            // 
            this.gbBuy.Controls.Add(this.txtBuyElementsDivisionOutput);
            this.gbBuy.Controls.Add(this.label11);
            this.gbBuy.Controls.Add(this.lblDividend);
            this.gbBuy.Controls.Add(this.lblBuyElementsToTake);
            this.gbBuy.Controls.Add(this.nudConstantBuyDividend);
            this.gbBuy.Controls.Add(this.nudBuyElementsToTake);
            this.gbBuy.Location = new System.Drawing.Point(13, 166);
            this.gbBuy.Margin = new System.Windows.Forms.Padding(2);
            this.gbBuy.Name = "gbBuy";
            this.gbBuy.Padding = new System.Windows.Forms.Padding(2);
            this.gbBuy.Size = new System.Drawing.Size(240, 55);
            this.gbBuy.TabIndex = 45;
            this.gbBuy.TabStop = false;
            this.gbBuy.Text = "Buy";
            // 
            // txtBuyElementsDivisionOutput
            // 
            this.txtBuyElementsDivisionOutput.Location = new System.Drawing.Point(4, 33);
            this.txtBuyElementsDivisionOutput.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuyElementsDivisionOutput.Name = "txtBuyElementsDivisionOutput";
            this.txtBuyElementsDivisionOutput.ReadOnly = true;
            this.txtBuyElementsDivisionOutput.Size = new System.Drawing.Size(76, 20);
            this.txtBuyElementsDivisionOutput.TabIndex = 49;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "Elements/Dividend";
            // 
            // lblDividend
            // 
            this.lblDividend.AutoSize = true;
            this.lblDividend.Location = new System.Drawing.Point(140, 34);
            this.lblDividend.Name = "lblDividend";
            this.lblDividend.Size = new System.Drawing.Size(49, 13);
            this.lblDividend.TabIndex = 44;
            this.lblDividend.Text = "Dividend";
            // 
            // lblBuyElementsToTake
            // 
            this.lblBuyElementsToTake.AutoSize = true;
            this.lblBuyElementsToTake.Location = new System.Drawing.Point(99, 15);
            this.lblBuyElementsToTake.Name = "lblBuyElementsToTake";
            this.lblBuyElementsToTake.Size = new System.Drawing.Size(90, 13);
            this.lblBuyElementsToTake.TabIndex = 43;
            this.lblBuyElementsToTake.Text = "Elements to Take";
            // 
            // nudConstantBuyDividend
            // 
            this.nudConstantBuyDividend.Location = new System.Drawing.Point(194, 32);
            this.nudConstantBuyDividend.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudConstantBuyDividend.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudConstantBuyDividend.Name = "nudConstantBuyDividend";
            this.nudConstantBuyDividend.Size = new System.Drawing.Size(41, 20);
            this.nudConstantBuyDividend.TabIndex = 42;
            this.nudConstantBuyDividend.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudConstantBuyDividend.ValueChanged += new System.EventHandler(this.nudConstantBuyDividend_ValueChanged);
            // 
            // nudBuyElementsToTake
            // 
            this.nudBuyElementsToTake.Location = new System.Drawing.Point(194, 14);
            this.nudBuyElementsToTake.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudBuyElementsToTake.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBuyElementsToTake.Name = "nudBuyElementsToTake";
            this.nudBuyElementsToTake.Size = new System.Drawing.Size(40, 20);
            this.nudBuyElementsToTake.TabIndex = 41;
            this.nudBuyElementsToTake.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudBuyElementsToTake.ValueChanged += new System.EventHandler(this.nudBuyElementsToTake_ValueChanged);
            // 
            // tmrCandleUpdater
            // 
            this.tmrCandleUpdater.Interval = 10000;
            this.tmrCandleUpdater.Tick += new System.EventHandler(this.tmrCandleUpdater_Tick);
            // 
            // rdoBuy
            // 
            this.rdoBuy.AutoSize = true;
            this.rdoBuy.Checked = true;
            this.rdoBuy.Location = new System.Drawing.Point(7, 19);
            this.rdoBuy.Name = "rdoBuy";
            this.rdoBuy.Size = new System.Drawing.Size(43, 17);
            this.rdoBuy.TabIndex = 11;
            this.rdoBuy.TabStop = true;
            this.rdoBuy.Text = "Buy";
            this.rdoBuy.UseVisualStyleBackColor = true;
            // 
            // rdoSell
            // 
            this.rdoSell.AutoSize = true;
            this.rdoSell.Location = new System.Drawing.Point(52, 20);
            this.rdoSell.Name = "rdoSell";
            this.rdoSell.Size = new System.Drawing.Size(42, 17);
            this.rdoSell.TabIndex = 12;
            this.rdoSell.Text = "Sell";
            this.rdoSell.UseVisualStyleBackColor = true;
            // 
            // rdoSwitch
            // 
            this.rdoSwitch.AutoSize = true;
            this.rdoSwitch.Location = new System.Drawing.Point(39, 38);
            this.rdoSwitch.Name = "rdoSwitch";
            this.rdoSwitch.Size = new System.Drawing.Size(57, 17);
            this.rdoSwitch.TabIndex = 13;
            this.rdoSwitch.Text = "Switch";
            this.rdoSwitch.UseVisualStyleBackColor = true;
            // 
            // gbAutomatedTrading
            // 
            this.gbAutomatedTrading.Controls.Add(this.statusStrip1);
            this.gbAutomatedTrading.Controls.Add(this.label15);
            this.gbAutomatedTrading.Controls.Add(this.label9);
            this.gbAutomatedTrading.Controls.Add(this.BalanceExit);
            this.gbAutomatedTrading.Controls.Add(this.label8);
            this.gbAutomatedTrading.Controls.Add(this.label7);
            this.gbAutomatedTrading.Controls.Add(this.nudSellStochk);
            this.gbAutomatedTrading.Controls.Add(this.nudBuyStochk);
            this.gbAutomatedTrading.Controls.Add(this.lblAutoUnrealizedROEPercent);
            this.gbAutomatedTrading.Controls.Add(this.nudAutoMarketTakeProfitPercent);
            this.gbAutomatedTrading.Controls.Add(this.chkAutoMarketTakeProfits);
            this.gbAutomatedTrading.Controls.Add(this.ddlAutoOrderType);
            this.gbAutomatedTrading.Controls.Add(this.nudAutoQuantity);
            this.gbAutomatedTrading.Controls.Add(this.rdoSell);
            this.gbAutomatedTrading.Controls.Add(this.rdoSwitch);
            this.gbAutomatedTrading.Controls.Add(this.label1);
            this.gbAutomatedTrading.Controls.Add(this.rdoBuy);
            this.gbAutomatedTrading.Controls.Add(this.btnSetPrice);
            this.gbAutomatedTrading.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAutomatedTrading.Location = new System.Drawing.Point(538, 15);
            this.gbAutomatedTrading.Name = "gbAutomatedTrading";
            this.gbAutomatedTrading.Size = new System.Drawing.Size(314, 154);
            this.gbAutomatedTrading.TabIndex = 14;
            this.gbAutomatedTrading.TabStop = false;
            this.gbAutomatedTrading.Text = "Automated Trading";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsAPIValid,
            this.stsAccountBalance});
            this.statusStrip1.Location = new System.Drawing.Point(3, 129);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(308, 22);
            this.statusStrip1.TabIndex = 68;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsAPIValid
            // 
            this.stsAPIValid.Name = "stsAPIValid";
            this.stsAPIValid.Size = new System.Drawing.Size(100, 17);
            this.stsAPIValid.Text = "API keys are invalid";
            // 
            // stsAccountBalance
            // 
            this.stsAccountBalance.Name = "stsAccountBalance";
            this.stsAccountBalance.Size = new System.Drawing.Size(58, 17);
            this.stsAccountBalance.Text = "Balance: 0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 98);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(207, 13);
            this.label15.TabIndex = 65;
            this.label15.Text = "Stop trading and close Position at Balance";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(104, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "Pieses";
            // 
            // BalanceExit
            // 
            this.BalanceExit.DecimalPlaces = 4;
            this.BalanceExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BalanceExit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.BalanceExit.Location = new System.Drawing.Point(7, 114);
            this.BalanceExit.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.BalanceExit.Name = "BalanceExit";
            this.BalanceExit.Size = new System.Drawing.Size(62, 19);
            this.BalanceExit.TabIndex = 66;
            this.BalanceExit.Value = new decimal(new int[] {
            135,
            0,
            0,
            262144});
            this.BalanceExit.ValueChanged += new System.EventHandler(this.BalanceExit_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(104, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Sell StochK";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(103, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Buy StochK";
            // 
            // nudSellStochk
            // 
            this.nudSellStochk.Location = new System.Drawing.Point(176, 37);
            this.nudSellStochk.Name = "nudSellStochk";
            this.nudSellStochk.Size = new System.Drawing.Size(41, 19);
            this.nudSellStochk.TabIndex = 41;
            this.nudSellStochk.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.nudSellStochk.ValueChanged += new System.EventHandler(this.nudSellStochk_ValueChanged);
            // 
            // nudBuyStochk
            // 
            this.nudBuyStochk.Location = new System.Drawing.Point(176, 16);
            this.nudBuyStochk.Name = "nudBuyStochk";
            this.nudBuyStochk.Size = new System.Drawing.Size(41, 19);
            this.nudBuyStochk.TabIndex = 39;
            this.nudBuyStochk.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudBuyStochk.ValueChanged += new System.EventHandler(this.nudBuyStochk_ValueChanged);
            // 
            // lblAutoUnrealizedROEPercent
            // 
            this.lblAutoUnrealizedROEPercent.AutoSize = true;
            this.lblAutoUnrealizedROEPercent.ForeColor = System.Drawing.Color.Aqua;
            this.lblAutoUnrealizedROEPercent.Location = new System.Drawing.Point(237, 115);
            this.lblAutoUnrealizedROEPercent.Name = "lblAutoUnrealizedROEPercent";
            this.lblAutoUnrealizedROEPercent.Size = new System.Drawing.Size(0, 13);
            this.lblAutoUnrealizedROEPercent.TabIndex = 21;
            // 
            // nudAutoMarketTakeProfitPercent
            // 
            this.nudAutoMarketTakeProfitPercent.DecimalPlaces = 2;
            this.nudAutoMarketTakeProfitPercent.Location = new System.Drawing.Point(132, 79);
            this.nudAutoMarketTakeProfitPercent.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudAutoMarketTakeProfitPercent.Name = "nudAutoMarketTakeProfitPercent";
            this.nudAutoMarketTakeProfitPercent.Size = new System.Drawing.Size(60, 19);
            this.nudAutoMarketTakeProfitPercent.TabIndex = 19;
            this.nudAutoMarketTakeProfitPercent.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudAutoMarketTakeProfitPercent.ValueChanged += new System.EventHandler(this.nudAutoMarketTakeProfitPercent_ValueChanged);
            // 
            // chkAutoMarketTakeProfits
            // 
            this.chkAutoMarketTakeProfits.AutoSize = true;
            this.chkAutoMarketTakeProfits.Checked = true;
            this.chkAutoMarketTakeProfits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoMarketTakeProfits.Location = new System.Drawing.Point(7, 81);
            this.chkAutoMarketTakeProfits.Name = "chkAutoMarketTakeProfits";
            this.chkAutoMarketTakeProfits.Size = new System.Drawing.Size(126, 17);
            this.chkAutoMarketTakeProfits.TabIndex = 16;
            this.chkAutoMarketTakeProfits.Text = "Market take profits at";
            this.chkAutoMarketTakeProfits.UseVisualStyleBackColor = true;
            // 
            // ddlAutoOrderType
            // 
            this.ddlAutoOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAutoOrderType.FormattingEnabled = true;
            this.ddlAutoOrderType.Items.AddRange(new object[] {
            "Limit Post Only",
            "Market"});
            this.ddlAutoOrderType.Location = new System.Drawing.Point(7, 59);
            this.ddlAutoOrderType.Name = "ddlAutoOrderType";
            this.ddlAutoOrderType.Size = new System.Drawing.Size(98, 21);
            this.ddlAutoOrderType.TabIndex = 15;
            this.ddlAutoOrderType.SelectedIndexChanged += new System.EventHandler(this.ddlAutoOrderType_SelectedIndexChanged);
            // 
            // nudAutoQuantity
            // 
            this.nudAutoQuantity.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudAutoQuantity.Location = new System.Drawing.Point(146, 58);
            this.nudAutoQuantity.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudAutoQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAutoQuantity.Name = "nudAutoQuantity";
            this.nudAutoQuantity.Size = new System.Drawing.Size(71, 19);
            this.nudAutoQuantity.TabIndex = 15;
            this.nudAutoQuantity.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudAutoQuantity.ValueChanged += new System.EventHandler(this.nudAutoQuantity_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(71, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Unrealized Mark ROE %";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnSetPrice
            // 
            this.btnSetPrice.BackColor = System.Drawing.Color.Aqua;
            this.btnSetPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetPrice.Location = new System.Drawing.Point(223, 16);
            this.btnSetPrice.Name = "btnSetPrice";
            this.btnSetPrice.Size = new System.Drawing.Size(85, 90);
            this.btnSetPrice.TabIndex = 35;
            this.btnSetPrice.Text = "Set settings";
            this.btnSetPrice.UseVisualStyleBackColor = false;
            this.btnSetPrice.Click += new System.EventHandler(this.btnSetPrice_Click);
            // 
            // btnAutomatedTrading
            // 
            this.btnAutomatedTrading.BackColor = System.Drawing.Color.Aqua;
            this.btnAutomatedTrading.Location = new System.Drawing.Point(857, 15);
            this.btnAutomatedTrading.Name = "btnAutomatedTrading";
            this.btnAutomatedTrading.Size = new System.Drawing.Size(95, 210);
            this.btnAutomatedTrading.TabIndex = 14;
            this.btnAutomatedTrading.Text = "Start";
            this.btnAutomatedTrading.UseVisualStyleBackColor = false;
            this.btnAutomatedTrading.Click += new System.EventHandler(this.btnAutomatedTrading_Click);
            // 
            // tmrAutoTradeExecution
            // 
            this.tmrAutoTradeExecution.Interval = 20000;
            this.tmrAutoTradeExecution.Tick += new System.EventHandler(this.tmrAutoTradeExecution_Tick);
            // 
            // btnAccountBalance
            // 
            this.btnAccountBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccountBalance.Location = new System.Drawing.Point(13, 79);
            this.btnAccountBalance.Name = "btnAccountBalance";
            this.btnAccountBalance.Size = new System.Drawing.Size(100, 23);
            this.btnAccountBalance.TabIndex = 16;
            this.btnAccountBalance.Text = "Update Balance";
            this.btnAccountBalance.UseVisualStyleBackColor = true;
            this.btnAccountBalance.Click += new System.EventHandler(this.btnAccountBalance_Click);
            // 
            // nudStopPercent
            // 
            this.nudStopPercent.DecimalPlaces = 2;
            this.nudStopPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudStopPercent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudStopPercent.Location = new System.Drawing.Point(117, 82);
            this.nudStopPercent.Name = "nudStopPercent";
            this.nudStopPercent.Size = new System.Drawing.Size(40, 19);
            this.nudStopPercent.TabIndex = 17;
            this.nudStopPercent.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStopPercent.ValueChanged += new System.EventHandler(this.nudStopPercent_ValueChanged);
            // 
            // btnManualSetStop
            // 
            this.btnManualSetStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManualSetStop.Location = new System.Drawing.Point(197, 83);
            this.btnManualSetStop.Name = "btnManualSetStop";
            this.btnManualSetStop.Size = new System.Drawing.Size(75, 23);
            this.btnManualSetStop.TabIndex = 18;
            this.btnManualSetStop.Text = "Set Stop";
            this.btnManualSetStop.UseVisualStyleBackColor = true;
            this.btnManualSetStop.Click += new System.EventHandler(this.btnManualSetStop_Click);
            // 
            // txtAPIKey
            // 
            this.txtAPIKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAPIKey.Location = new System.Drawing.Point(286, 81);
            this.txtAPIKey.Name = "txtAPIKey";
            this.txtAPIKey.Size = new System.Drawing.Size(159, 19);
            this.txtAPIKey.TabIndex = 19;
            this.txtAPIKey.UseSystemPasswordChar = true;
            this.txtAPIKey.TextChanged += new System.EventHandler(this.txtAPIKey_TextChanged);
            // 
            // txtAPISecret
            // 
            this.txtAPISecret.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAPISecret.Location = new System.Drawing.Point(286, 102);
            this.txtAPISecret.Name = "txtAPISecret";
            this.txtAPISecret.Size = new System.Drawing.Size(159, 19);
            this.txtAPISecret.TabIndex = 20;
            this.txtAPISecret.UseSystemPasswordChar = true;
            this.txtAPISecret.TextChanged += new System.EventHandler(this.txtAPISecret_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(450, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Key";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(450, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Secret";
            // 
            // btnBuyOverTimeOrder
            // 
            this.btnBuyOverTimeOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuyOverTimeOrder.Location = new System.Drawing.Point(286, 2);
            this.btnBuyOverTimeOrder.Name = "btnBuyOverTimeOrder";
            this.btnBuyOverTimeOrder.Size = new System.Drawing.Size(89, 23);
            this.btnBuyOverTimeOrder.TabIndex = 24;
            this.btnBuyOverTimeOrder.Text = "Buy Over Time";
            this.btnBuyOverTimeOrder.UseVisualStyleBackColor = true;
            this.btnBuyOverTimeOrder.Click += new System.EventHandler(this.btnBuyOverTimeOrder_Click);
            // 
            // btnSellOverTimeOrder
            // 
            this.btnSellOverTimeOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSellOverTimeOrder.Location = new System.Drawing.Point(286, 32);
            this.btnSellOverTimeOrder.Name = "btnSellOverTimeOrder";
            this.btnSellOverTimeOrder.Size = new System.Drawing.Size(89, 23);
            this.btnSellOverTimeOrder.TabIndex = 25;
            this.btnSellOverTimeOrder.Text = "Sell Over Time";
            this.btnSellOverTimeOrder.UseVisualStyleBackColor = true;
            this.btnSellOverTimeOrder.Click += new System.EventHandler(this.btnSellOverTimeOrder_Click);
            // 
            // tmrTradeOverTime
            // 
            this.tmrTradeOverTime.Tick += new System.EventHandler(this.tmrTradeOverTime_Tick);
            // 
            // nudOverTimeContracts
            // 
            this.nudOverTimeContracts.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudOverTimeContracts.Location = new System.Drawing.Point(377, 6);
            this.nudOverTimeContracts.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudOverTimeContracts.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOverTimeContracts.Name = "nudOverTimeContracts";
            this.nudOverTimeContracts.Size = new System.Drawing.Size(67, 19);
            this.nudOverTimeContracts.TabIndex = 26;
            this.nudOverTimeContracts.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudOverTimeContracts.ValueChanged += new System.EventHandler(this.nudOverTimeContracts_ValueChanged);
            // 
            // nudOverTimeInterval
            // 
            this.nudOverTimeInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudOverTimeInterval.Location = new System.Drawing.Point(377, 36);
            this.nudOverTimeInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudOverTimeInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOverTimeInterval.Name = "nudOverTimeInterval";
            this.nudOverTimeInterval.Size = new System.Drawing.Size(47, 19);
            this.nudOverTimeInterval.TabIndex = 27;
            this.nudOverTimeInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudOverTimeInterval.ValueChanged += new System.EventHandler(this.nudOverTimeInterval_ValueChanged);
            // 
            // nudOverTimeIntervalCount
            // 
            this.nudOverTimeIntervalCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudOverTimeIntervalCount.Location = new System.Drawing.Point(377, 64);
            this.nudOverTimeIntervalCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudOverTimeIntervalCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOverTimeIntervalCount.Name = "nudOverTimeIntervalCount";
            this.nudOverTimeIntervalCount.Size = new System.Drawing.Size(47, 19);
            this.nudOverTimeIntervalCount.TabIndex = 28;
            this.nudOverTimeIntervalCount.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudOverTimeIntervalCount.ValueChanged += new System.EventHandler(this.nudOverTimeIntervalCount_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(450, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Contracts Per";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(450, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Seconds";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(450, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "X Times";
            // 
            // btnOverTimeStop
            // 
            this.btnOverTimeStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOverTimeStop.Location = new System.Drawing.Point(286, 58);
            this.btnOverTimeStop.Name = "btnOverTimeStop";
            this.btnOverTimeStop.Size = new System.Drawing.Size(89, 23);
            this.btnOverTimeStop.TabIndex = 31;
            this.btnOverTimeStop.Text = "Stop";
            this.btnOverTimeStop.UseVisualStyleBackColor = true;
            this.btnOverTimeStop.Click += new System.EventHandler(this.btnOverTimeStop_Click);
            // 
            // lblPriceBuy
            // 
            this.lblPriceBuy.AutoSize = true;
            this.lblPriceBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceBuy.Location = new System.Drawing.Point(93, 117);
            this.lblPriceBuy.Name = "lblPriceBuy";
            this.lblPriceBuy.Size = new System.Drawing.Size(52, 13);
            this.lblPriceBuy.TabIndex = 33;
            this.lblPriceBuy.Text = "Price Buy";
            // 
            // nudPriceBuy
            // 
            this.nudPriceBuy.DecimalPlaces = 2;
            this.nudPriceBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPriceBuy.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudPriceBuy.Location = new System.Drawing.Point(13, 114);
            this.nudPriceBuy.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudPriceBuy.Name = "nudPriceBuy";
            this.nudPriceBuy.Size = new System.Drawing.Size(79, 19);
            this.nudPriceBuy.TabIndex = 34;
            this.nudPriceBuy.Value = new decimal(new int[] {
            6200,
            0,
            0,
            0});
            this.nudPriceBuy.ValueChanged += new System.EventHandler(this.nudPriceBuy_ValueChanged);
            // 
            // nudPriceSell
            // 
            this.nudPriceSell.DecimalPlaces = 2;
            this.nudPriceSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPriceSell.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudPriceSell.Location = new System.Drawing.Point(148, 115);
            this.nudPriceSell.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudPriceSell.Name = "nudPriceSell";
            this.nudPriceSell.Size = new System.Drawing.Size(79, 19);
            this.nudPriceSell.TabIndex = 36;
            this.nudPriceSell.Value = new decimal(new int[] {
            8500,
            0,
            0,
            0});
            this.nudPriceSell.ValueChanged += new System.EventHandler(this.nudPriceSell_ValueChanged);
            // 
            // lblPriceSell
            // 
            this.lblPriceSell.AutoSize = true;
            this.lblPriceSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceSell.Location = new System.Drawing.Point(229, 117);
            this.lblPriceSell.Name = "lblPriceSell";
            this.lblPriceSell.Size = new System.Drawing.Size(51, 13);
            this.lblPriceSell.TabIndex = 37;
            this.lblPriceSell.Text = "Price Sell";
            // 
            // tmrUpdateBBMiddle
            // 
            this.tmrUpdateBBMiddle.Interval = 20000;
            this.tmrUpdateBBMiddle.Tick += new System.EventHandler(this.tmrUpdateBBMiddle_Tick);
            // 
            // tmrUpdateBuySellFirstPriceOrders
            // 
            this.tmrUpdateBuySellFirstPriceOrders.Interval = 5000;
            this.tmrUpdateBuySellFirstPriceOrders.Tick += new System.EventHandler(this.tmrUpdateBuySellFirstPriceOrders_Tick);
            // 
            // LoggingTextBox
            // 
            this.LoggingTextBox.Location = new System.Drawing.Point(13, 314);
            this.LoggingTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.LoggingTextBox.Multiline = true;
            this.LoggingTextBox.Name = "LoggingTextBox";
            this.LoggingTextBox.ReadOnly = true;
            this.LoggingTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LoggingTextBox.Size = new System.Drawing.Size(942, 55);
            this.LoggingTextBox.TabIndex = 56;
            // 
            // gbOpenOrders
            // 
            this.gbOpenOrders.Controls.Add(this.dgvOpenOrders);
            this.gbOpenOrders.Location = new System.Drawing.Point(13, 446);
            this.gbOpenOrders.Margin = new System.Windows.Forms.Padding(2);
            this.gbOpenOrders.Name = "gbOpenOrders";
            this.gbOpenOrders.Padding = new System.Windows.Forms.Padding(2);
            this.gbOpenOrders.Size = new System.Drawing.Size(940, 234);
            this.gbOpenOrders.TabIndex = 58;
            this.gbOpenOrders.TabStop = false;
            this.gbOpenOrders.Text = "Open Orders";
            // 
            // dgvOpenOrders
            // 
            this.dgvOpenOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpenOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOpenOrders.Location = new System.Drawing.Point(2, 15);
            this.dgvOpenOrders.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOpenOrders.Name = "dgvOpenOrders";
            this.dgvOpenOrders.ReadOnly = true;
            this.dgvOpenOrders.RowTemplate.Height = 24;
            this.dgvOpenOrders.Size = new System.Drawing.Size(936, 217);
            this.dgvOpenOrders.TabIndex = 58;
            this.dgvOpenOrders.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOpenOrders_CellFormatting);
            // 
            // gbOpenPositions
            // 
            this.gbOpenPositions.Controls.Add(this.dgvOpenPositions);
            this.gbOpenPositions.Location = new System.Drawing.Point(13, 374);
            this.gbOpenPositions.Margin = new System.Windows.Forms.Padding(2);
            this.gbOpenPositions.Name = "gbOpenPositions";
            this.gbOpenPositions.Padding = new System.Windows.Forms.Padding(2);
            this.gbOpenPositions.Size = new System.Drawing.Size(943, 70);
            this.gbOpenPositions.TabIndex = 59;
            this.gbOpenPositions.TabStop = false;
            this.gbOpenPositions.Text = "Open Positions";
            // 
            // dgvOpenPositions
            // 
            this.dgvOpenPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpenPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOpenPositions.Location = new System.Drawing.Point(2, 15);
            this.dgvOpenPositions.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOpenPositions.Name = "dgvOpenPositions";
            this.dgvOpenPositions.ReadOnly = true;
            this.dgvOpenPositions.RowTemplate.Height = 24;
            this.dgvOpenPositions.Size = new System.Drawing.Size(939, 53);
            this.dgvOpenPositions.TabIndex = 59;
            this.dgvOpenPositions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOpenPositions_CellFormatting);
            // 
            // tmrTradesKingCoinsReader
            // 
            this.tmrTradesKingCoinsReader.Interval = 20000;
            this.tmrTradesKingCoinsReader.Tick += new System.EventHandler(this.tmrTradesKingCoinsReader_Tick);
            // 
            // lbTrend24h
            // 
            this.lbTrend24h.AutoSize = true;
            this.lbTrend24h.Location = new System.Drawing.Point(723, 209);
            this.lbTrend24h.Name = "lbTrend24h";
            this.lbTrend24h.Size = new System.Drawing.Size(53, 13);
            this.lbTrend24h.TabIndex = 61;
            this.lbTrend24h.Text = "Trend24h";
            // 
            // lbTrend1h
            // 
            this.lbTrend1h.AutoSize = true;
            this.lbTrend1h.Location = new System.Drawing.Point(723, 190);
            this.lbTrend1h.Name = "lbTrend1h";
            this.lbTrend1h.Size = new System.Drawing.Size(47, 13);
            this.lbTrend1h.TabIndex = 60;
            this.lbTrend1h.Text = "Trend1h";
            // 
            // txtTrend24h
            // 
            this.txtTrend24h.Location = new System.Drawing.Point(777, 207);
            this.txtTrend24h.Margin = new System.Windows.Forms.Padding(2);
            this.txtTrend24h.Name = "txtTrend24h";
            this.txtTrend24h.ReadOnly = true;
            this.txtTrend24h.Size = new System.Drawing.Size(76, 20);
            this.txtTrend24h.TabIndex = 63;
            // 
            // txtTrend1h
            // 
            this.txtTrend1h.Location = new System.Drawing.Point(777, 188);
            this.txtTrend1h.Margin = new System.Windows.Forms.Padding(2);
            this.txtTrend1h.Name = "txtTrend1h";
            this.txtTrend1h.ReadOnly = true;
            this.txtTrend1h.Size = new System.Drawing.Size(76, 20);
            this.txtTrend1h.TabIndex = 62;
            // 
            // DumpInput
            // 
            this.DumpInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DumpInput.Location = new System.Drawing.Point(221, 137);
            this.DumpInput.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.DumpInput.Name = "DumpInput";
            this.DumpInput.Size = new System.Drawing.Size(51, 19);
            this.DumpInput.TabIndex = 43;
            this.DumpInput.Value = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.DumpInput.ValueChanged += new System.EventHandler(this.DumpInput_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(37, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(166, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "Dump how much under BBLower ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(283, 139);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(156, 13);
            this.label14.TabIndex = 64;
            this.label14.Text = "Pump how much over BBUpper";
            // 
            // PumpInput
            // 
            this.PumpInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PumpInput.Location = new System.Drawing.Point(453, 137);
            this.PumpInput.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.PumpInput.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.PumpInput.Name = "PumpInput";
            this.PumpInput.Size = new System.Drawing.Size(51, 19);
            this.PumpInput.TabIndex = 65;
            this.PumpInput.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PumpInput.ValueChanged += new System.EventHandler(this.PumpInput_ValueChanged);
            // 
            // KillAll
            // 
            this.KillAll.ForeColor = System.Drawing.Color.Red;
            this.KillAll.Location = new System.Drawing.Point(813, 169);
            this.KillAll.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.KillAll.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.KillAll.Name = "KillAll";
            this.KillAll.Size = new System.Drawing.Size(40, 20);
            this.KillAll.TabIndex = 66;
            this.KillAll.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.KillAll.ValueChanged += new System.EventHandler(this.KillAll_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(719, 173);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 13);
            this.label16.TabIndex = 67;
            this.label16.Text = "Safe money @ %";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 609);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.KillAll);
            this.Controls.Add(this.PumpInput);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.DumpInput);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtTrend24h);
            this.Controls.Add(this.txtTrend1h);
            this.Controls.Add(this.lbTrend24h);
            this.Controls.Add(this.lbTrend1h);
            this.Controls.Add(this.gbOpenPositions);
            this.Controls.Add(this.gbOpenOrders);
            this.Controls.Add(this.LoggingTextBox);
            this.Controls.Add(this.txtFirstSellOrderOutput);
            this.Controls.Add(this.lblPriceSell);
            this.Controls.Add(this.lblFirstSellOrder);
            this.Controls.Add(this.nudPriceSell);
            this.Controls.Add(this.btnAutomatedTrading);
            this.Controls.Add(this.txtFirstBuyOrderOutput);
            this.Controls.Add(this.lblFirstBuyOrder);
            this.Controls.Add(this.lblVolume24h);
            this.Controls.Add(this.nudPriceBuy);
            this.Controls.Add(this.nudVolume24h);
            this.Controls.Add(this.lblPriceBuy);
            this.Controls.Add(this.btnOverTimeStop);
            this.Controls.Add(this.gbSell);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gbBuy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvCandles);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudOverTimeIntervalCount);
            this.Controls.Add(this.nudOverTimeInterval);
            this.Controls.Add(this.nudOverTimeContracts);
            this.Controls.Add(this.btnSellOverTimeOrder);
            this.Controls.Add(this.btnBuyOverTimeOrder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAPISecret);
            this.Controls.Add(this.txtAPIKey);
            this.Controls.Add(this.btnManualSetStop);
            this.Controls.Add(this.nudStopPercent);
            this.Controls.Add(this.btnAccountBalance);
            this.Controls.Add(this.gbAutomatedTrading);
            this.Controls.Add(this.gbCandles);
            this.Controls.Add(this.ddlSymbol);
            this.Controls.Add(this.ddlNetwork);
            this.Controls.Add(this.ddlOrderType);
            this.Controls.Add(this.btnCancelOpenOrders);
            this.Controls.Add(this.chkCancelWhileOrdering);
            this.Controls.Add(this.nudQty);
            this.Controls.Add(this.btnSell);
            this.Controls.Add(this.btnBuy);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Snipers Bot";
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCandles)).EndInit();
            this.gbCandles.ResumeLayout(false);
            this.gbCandles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMA2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMA1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolume24h)).EndInit();
            this.gbSell.ResumeLayout(false);
            this.gbSell.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudConstantSellDividend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSellElementsToTake)).EndInit();
            this.gbBuy.ResumeLayout(false);
            this.gbBuy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudConstantBuyDividend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuyElementsToTake)).EndInit();
            this.gbAutomatedTrading.ResumeLayout(false);
            this.gbAutomatedTrading.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BalanceExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSellStochk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuyStochk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAutoMarketTakeProfitPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAutoQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOverTimeContracts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOverTimeInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOverTimeIntervalCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriceBuy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriceSell)).EndInit();
            this.gbOpenOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenOrders)).EndInit();
            this.gbOpenPositions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenPositions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DumpInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PumpInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KillAll)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.NumericUpDown nudQty;
        private System.Windows.Forms.CheckBox chkCancelWhileOrdering;
        private System.Windows.Forms.Button btnCancelOpenOrders;
        private System.Windows.Forms.ComboBox ddlOrderType;
        private System.Windows.Forms.ComboBox ddlNetwork;
        private System.Windows.Forms.ComboBox ddlSymbol;
        private System.Windows.Forms.DataGridView dgvCandles;
        private System.Windows.Forms.ComboBox ddlCandleTimes;
        private System.Windows.Forms.GroupBox gbCandles;
        private System.Windows.Forms.Timer tmrCandleUpdater;
        private System.Windows.Forms.CheckBox chkUpdateCandles;
        private System.Windows.Forms.Label lblMA2;
        private System.Windows.Forms.NumericUpDown nudMA2;
        private System.Windows.Forms.Label lblMA1;
        private System.Windows.Forms.NumericUpDown nudMA1;
        private System.Windows.Forms.RadioButton rdoBuy;
        private System.Windows.Forms.RadioButton rdoSell;
        private System.Windows.Forms.RadioButton rdoSwitch;
        private System.Windows.Forms.GroupBox gbAutomatedTrading;
        private System.Windows.Forms.Button btnAutomatedTrading;
        private System.Windows.Forms.ComboBox ddlAutoOrderType;
        private System.Windows.Forms.NumericUpDown nudAutoQuantity;
        private System.Windows.Forms.Timer tmrAutoTradeExecution;
        private System.Windows.Forms.Button btnAccountBalance;
        private System.Windows.Forms.NumericUpDown nudStopPercent;
        private System.Windows.Forms.Button btnManualSetStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudAutoMarketTakeProfitPercent;
        private System.Windows.Forms.CheckBox chkAutoMarketTakeProfits;
        private System.Windows.Forms.Label lblAutoUnrealizedROEPercent;
        private System.Windows.Forms.TextBox txtAPIKey;
        private System.Windows.Forms.TextBox txtAPISecret;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBuyOverTimeOrder;
        private System.Windows.Forms.Button btnSellOverTimeOrder;
        private System.Windows.Forms.Timer tmrTradeOverTime;
        private System.Windows.Forms.NumericUpDown nudOverTimeContracts;
        private System.Windows.Forms.NumericUpDown nudOverTimeInterval;
        private System.Windows.Forms.NumericUpDown nudOverTimeIntervalCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOverTimeStop;
        private System.Windows.Forms.Label lblPriceBuy;
        private System.Windows.Forms.NumericUpDown nudPriceBuy;
        private System.Windows.Forms.Button btnSetPrice;
        private System.Windows.Forms.NumericUpDown nudPriceSell;
        private System.Windows.Forms.Label lblPriceSell;
        private System.Windows.Forms.NumericUpDown nudSellStochk;
        private System.Windows.Forms.NumericUpDown nudBuyStochk;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gbBuy;
        private System.Windows.Forms.Label lblDividend;
        private System.Windows.Forms.Label lblBuyElementsToTake;
        private System.Windows.Forms.NumericUpDown nudConstantBuyDividend;
        private System.Windows.Forms.NumericUpDown nudBuyElementsToTake;
        private System.Windows.Forms.GroupBox gbSell;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudConstantSellDividend;
        private System.Windows.Forms.Label lblSellElementsToTake;
        private System.Windows.Forms.NumericUpDown nudSellElementsToTake;
        private System.Windows.Forms.Label lblVolume24h;
        private System.Windows.Forms.NumericUpDown nudVolume24h;
        private System.Windows.Forms.TextBox txtSellElementsDivisionOutput;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBuyElementsDivisionOutput;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFirstSellOrderOutput;
        private System.Windows.Forms.Label lblFirstSellOrder;
        private System.Windows.Forms.TextBox txtFirstBuyOrderOutput;
        private System.Windows.Forms.Label lblFirstBuyOrder;
        private System.Windows.Forms.Timer tmrUpdateBBMiddle;
        private System.Windows.Forms.Timer tmrUpdateBuySellFirstPriceOrders;
        private System.Windows.Forms.TextBox LoggingTextBox;
        private System.Windows.Forms.GroupBox gbOpenOrders;
        private System.Windows.Forms.DataGridView dgvOpenOrders;
        private System.Windows.Forms.GroupBox gbOpenPositions;
        private System.Windows.Forms.DataGridView dgvOpenPositions;
        private System.Windows.Forms.Timer tmrTradesKingCoinsReader;
        private System.Windows.Forms.Label lbTrend24h;
        private System.Windows.Forms.Label lbTrend1h;
        private System.Windows.Forms.TextBox txtTrend24h;
        private System.Windows.Forms.TextBox txtTrend1h;
        private System.Windows.Forms.NumericUpDown DumpInput;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown PumpInput;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown BalanceExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsAPIValid;
        private System.Windows.Forms.ToolStripStatusLabel stsAccountBalance;
        private System.Windows.Forms.NumericUpDown KillAll;
        private System.Windows.Forms.Label label16;
    }
}

