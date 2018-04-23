using BitMEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Repository.Hierarchy;

namespace BitMexSampleBot
{
    public partial class Form1 : Form
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Constants

        private const int CONST_ORDER_BOOK_DEPTH = 50;
        private const int CONST_MAX_TOTAL_TAKE_SHOW_ORDERS = 8;
        private const int CONST_MAX_TOTAL_TAKE_SHOW_POSITIONS = 8;

        #endregion

        // IMPORTANT - Enter your API Key information below

        //TEST NET - NEW
        //     private static string TestbitmexKey = "";
        //     private static string TestbitmexSecret = "";
        private static string TestbitmexDomain = "https://testnet.bitmex.com";

        //REAL NET        
      //  private static string bitmexKey = "X6_egTD7fQFUZcmT4Dk3H9Gl";
     //   private static string bitmexSecret = "v7xCkLn6Ee9noeY7oVhAsa5Jz-DVFFLAAVezI4AKl5AdOW00";
        private static string bitmexDomain = "https://www.bitmex.com";


        BitMEXApi bitmex;
        List<OrderBook> CurrentBook = new List<OrderBook>();
        List<OrderBook> CurrentBookSell = new List<OrderBook>();
        List<OrderBook> CurrentBookBuy = new List<OrderBook>();
        List<Instrument> ActiveInstruments = new List<Instrument>();
        Instrument ActiveInstrument = new Instrument();
        List<Candle> Candles = new List<Candle>();

        bool Running = false;
        string Mode = "Wait";
        List<Position> OpenPositions = new List<Position>();
        List<Quote> Quote = new List<Quote>();
        List<Order> OpenOrders = new List<Order>();

        // For BBand Indicator Info, 20, close 2
        int BBLength = 20;
        double BBMultiplier = 2;

        // For EMA Indicator Periods, also used in MACD
        int EMA1Period = 26;  // Slow MACD EMA
        int EMA2Period = 12;  // Fast MACD EMA
        int EMA3Period = 9;

        // For MACD
        int MACDEMAPeriod = 9;  // MACD smoothing period

        // For checking API validity before attempting orders/account specific moves
        bool APIValid = false;
        double WalletBalance = 0;

        // For ATR
        int ATR1Period = 7;
        int ATR2Period = 20;

        // For Over Time
        int OTContractsPer = 0;
        int OTIntervalSeconds = 0;
        int OTIntervalCount = 0;
        int OTTimerCount = 0;
        string OTSide = "Buy";

        // For RSI
        int RSIPeriod = 14;

        // NEW - For Stochastic (STOCH)
        int STOCHLookbackPeriod = 14;
        int STOCHDPeriod = 3;

        //saves the last setbotmode executed by bot
        private string _lastMode = string.Empty;

        #region properties

        //CHANGE 2: Price set from UI
        public double InputPriceBuy { get; set; }
        public double InputPriceSell { get; set; }

        public int BuyElementsToTake { get; set; }
        public int SellElementsToTake { get; set; }

        public double PriceBuyDividend { get; set; }
        public double PriceSellDividend { get; set; }

        public double InputVolume24h { get; set; }

        public double SumSellFirstItems { get; set; }
        public double SumBuyFirstItems { get; set; }

        public double InputSellSTOCHK { get; set; }
        public double InputBuySTOCHK { get; set; }
        public bool AllowCalculateBBMiddle { get; set; }

        double? SellPumpPrice { get; set; }
        double? BuyDumpPrice { get; set; }
        public bool AllowCalculateOrder { get; set; }
        public double? percent { get; set; }
        public double? BuyPriceOld  { get; set; }
        public double? SellPriceOld { get; set; }
        public double? BuyDumpPriceOld { get; set; }
        public double? SellPumpPriceOld { get; set; }
        public double? OrderPrice { get; set; }
        public int? PositionDifference { get; set; }
        public double? STOCHK { get; set; }


        #endregion

        public Form1()
        {
            InitializeComponent();
            InitializeDropdownsAndSettings();
            InitializeAPI();
            InitializeCandleArea();
            InitializeOverTime();
            InitializeBuySellStochk();
            InitializeParameterSettings();
            InitializeColors();
            //var rootAppender = ((Hierarchy)LogManager.GetRepository())
            //    .Root.Appenders.OfType<TextBoxAppender>()
            //    .FirstOrDefault();
            var appender = LogManager.GetRepository().GetAppenders().FirstOrDefault(a => a.Name == "TextBoxAppender");
            if (appender != null)
                ((TextBoxAppender)appender).AppenderTextBox = this.LoggingTextBox;
        }

        private void InitializeColors()
        {
            //form background und foreground
            this.BackColor = System.Drawing.Color.Black;
            this.ForeColor = System.Drawing.Color.Yellow;

            //grid header
            this.dgvCandles.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            this.dgvCandles.ColumnHeadersDefaultCellStyle.ForeColor = Color.Yellow;
            this.dgvCandles.EnableHeadersVisualStyles = false;

            //grid open orders
            this.dgvOpenOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            this.dgvOpenOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.Yellow;
            this.dgvOpenOrders.EnableHeadersVisualStyles = false;

            //grid open positions
            this.dgvOpenPositions.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            this.dgvOpenPositions.ColumnHeadersDefaultCellStyle.ForeColor = Color.Yellow;
            this.dgvOpenPositions.EnableHeadersVisualStyles = false;

            this.gbAutomatedTrading.ForeColor = Color.Yellow;
            this.gbOpenOrders.ForeColor = Color.Yellow;
            this.gbOpenPositions.ForeColor = Color.Yellow;
            this.gbBuy.ForeColor = Color.Yellow;
            this.gbSell.ForeColor = Color.Yellow;
            this.gbCandles.ForeColor = Color.Yellow;
            this.btnSetPrice.ForeColor = Color.Black;
            this.btnAutomatedTrading.ForeColor = Color.Black;
            this.ddlAutoOrderType.ForeColor = Color.Black;
            this.ddlAutoOrderType.ForeColor = Color.Black;
            this.ddlCandleTimes.ForeColor = Color.Black;
            this.btnBuy.ForeColor = Color.Black;
            this.btnSell.ForeColor = Color.Black;
            this.btnCancelOpenOrders.ForeColor = Color.Black;
            this.btnManualSetStop.ForeColor = Color.Black;
            this.btnBuyOverTimeOrder.ForeColor = Color.Black;
            this.btnSellOverTimeOrder.ForeColor = Color.Black;
            this.btnOverTimeStop.ForeColor = Color.Black;
            this.btnAccountBalance.ForeColor = Color.Black;

        }

        private void InitializeDropdownsAndSettings()
        {
            ddlNetwork.SelectedIndex = 0;
            ddlOrderType.SelectedIndex = 0;
            ddlCandleTimes.SelectedIndex = 0;
            ddlAutoOrderType.SelectedIndex = 0;

            LoadAPISettings();
        }

        private void InitializeBuySellStochk()
        {
            InputSellSTOCHK = Convert.ToDouble(nudSellStochk.Value);
            InputBuySTOCHK = Convert.ToDouble(nudBuyStochk.Value);
        }

        private void InitializeParameterSettings()
        {
            InputPriceBuy = decimal.ToDouble(nudPriceBuy.Value);
            InputPriceSell = decimal.ToDouble(nudPriceSell.Value);
            BuyElementsToTake = decimal.ToInt32(nudBuyElementsToTake.Value);
            SellElementsToTake = decimal.ToInt32(nudSellElementsToTake.Value);
            PriceBuyDividend = decimal.ToDouble(nudConstantBuyDividend.Value);
            PriceSellDividend = decimal.ToDouble(nudConstantSellDividend.Value);
            InputVolume24h = decimal.ToDouble(nudVolume24h.Value);
            AllowCalculateBBMiddle = true;
            AllowCalculateOrder = true;
            log.InfoFormat("Set Input Setting: InputPriceBuy = {0}, InputPriceSell = {1}, BuyElementsToTake = {2}, " +
                "SellElementsToTake={3}, PriceBuyDividend={4}, PriceSellDividend={5}, InputVolume24h={6}", InputPriceBuy, InputPriceSell,
                BuyElementsToTake, SellElementsToTake, PriceBuyDividend, PriceSellDividend, InputVolume24h);
        }

        private void LoadAPISettings()
        {
            switch (ddlNetwork.SelectedItem.ToString())
            {
                case "TestNet":
                    txtAPIKey.Text = Properties.Settings.Default.TestAPIKey;
                    txtAPISecret.Text = Properties.Settings.Default.TestAPISecret;
                    break;
                case "RealNet":
                    txtAPIKey.Text = Properties.Settings.Default.APIKey;
                    txtAPISecret.Text = Properties.Settings.Default.APISecret;
                    break;
            }
        }

        private void InitializeOverTime() // NEW - Just updates the summary
        {
            UpdateOverTimeSummary();
        }

        private void InitializeCandleArea()
        {
            tmrCandleUpdater.Start();
        }

        private void InitializeAPI()
        {
            switch (ddlNetwork.SelectedItem.ToString())
            {
                case "TestNet":
                    bitmex = new BitMEXApi(txtAPIKey.Text, txtAPISecret.Text, TestbitmexDomain);
                    break;
                case "RealNet":
                    bitmex = new BitMEXApi(txtAPIKey.Text, txtAPISecret.Text, bitmexDomain);
                    break;
            }

            // We must do this in case symbols are different on test and real net
            GetAPIValidity(); // Validate API keys by checking and displaying account balance.
            InitializeSymbolInformation();
        }

        private void InitializeSymbolInformation()
        {
            ActiveInstruments = bitmex.GetActiveInstruments().OrderByDescending(a => a.Volume24H).ToList();
            ddlSymbol.DataSource = ActiveInstruments;
            ddlSymbol.DisplayMember = "Symbol";
            ddlSymbol.SelectedIndex = 0;
            ActiveInstrument = ActiveInstruments[0];
        }
        public double CalculateMakerOrderPrice(string Side) 
        {
         

            //every 10 min AllowCalculateBBMiddle is true and then  BuyPumpPrice and BuyDumpPrice are recalculated
            if (AllowCalculateOrder == true)
            {
                AllowCalculateOrder = false;

            }
            if (AllowCalculateBBMiddle)
            {
                AllowCalculateBBMiddle = false;
               //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());
               double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
              SellPumpPrice = OldBBMiddle + 37;
               BuyDumpPrice = OldBBMiddle - 45;
            }
            Candle currentCandle = Candles.Any() ? Candles[0] : null;
            CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);
            OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
            double? SellPrice = CalculateSellPrice(CurrentBook);
            SellPrice = SellPrice + 1;
            log.InfoFormat("CalculateMakerOrderPrice - SellPrice: {0}", SellPrice);
            double? BuyPrice = CalculateBuyPrice(CurrentBook);
            //immer +1 usd addieren
            BuyPrice = BuyPrice - 2;
            
            
            log.InfoFormat("CalculateMakerOrderPrice - BuyPrice: {0}", BuyPrice);
            //lblAutoUnrealizedROEPercent.Text = Math.Round((Convert.ToDouble(OpenPositions[0].UnrealisedRoePcnt * 100)), 2).ToString();
            
            double? AvgEntryPrice = 0;
            double TrendPrice = 0;
            if ((BuyDumpPrice < BuyDumpPriceOld) & (SellPumpPrice >SellPumpPriceOld))
            {

                bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }

            switch (Side)
            {
                case "Buy":

                    if (BuyPriceOld > BuyPrice)
                    {

                        bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    }
                    else if ((OpenPositions.Count > 0 ) && (currentCandle != null) && (currentCandle.STOCHK <= InputBuySTOCHK))
                    {
                        AvgEntryPrice = OpenPositions[0].AvgEntryPrice ?? BuyPrice;
                        
                        if ((BuyPrice<BuyDumpPrice) & (AllowCalculateBBMiddle == true) & (currentCandle.STOCHK <= InputBuySTOCHK))
                        {
                            if (BuyPrice<AvgEntryPrice)
                            {
                                OrderPrice = BuyPrice;
                            }
                            else
                            {
                                OrderPrice = BuyDumpPrice;
                            }
                        }
                        else if ((TrendPrice<BuyPrice) & (TrendPrice<AvgEntryPrice) & (currentCandle.STOCHK <= InputBuySTOCHK))
                        {
                            //BuyPrice = Convert.ToDouble(OpenPositions[0].AvgEntryPrice ?? BuyPrice);
                            OrderPrice = TrendPrice;
                        }
                        else if ((SumBuyFirstItems > SumSellFirstItems) & (currentCandle.STOCHK >= InputSellSTOCHK))
                        {
                            OrderPrice = BuyPrice;
                            // OrderPrice = BuyDumpPrice;
                            
                        }
                        log.InfoFormat("CalculateMakerOrderPrice - SellPrice: {0}, AvgEntryPrice:{1} => OrderPrice: {2}", BuyPrice, TrendPrice, BuyDumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                    }
              
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    BuyPriceOld = OrderPrice;
                    BuyDumpPriceOld = BuyDumpPrice;
               
                    break;

                case "Sell":

                    if ((SellPriceOld < SellPrice) & (currentCandle.STOCHK >= InputSellSTOCHK))
                    {

                        bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    }
                    else if ((OpenPositions.Count > 0) & (currentCandle.STOCHK >= InputSellSTOCHK))
                    {
                        AvgEntryPrice = OpenPositions[0].AvgEntryPrice ?? SellPrice;
                        AvgEntryPrice = (AvgEntryPrice + 20);

                        if ((SellPrice<SellPumpPrice) && (AllowCalculateBBMiddle == true) & (currentCandle.STOCHK >= InputSellSTOCHK))
                        {
                            if (SellPrice > AvgEntryPrice)
                            {
                                OrderPrice = SellPrice;
                            }
                            else if (TrendPrice > SellPrice)
                            {
                                OrderPrice = TrendPrice;
                            }
                        }
                     }
                     else
                     {
                           // OrderPrice = SellPumpPrice;

                          //  AllowCalculateBBMiddle = false;
                     }
                    log.InfoFormat("CalculateMakerOrderPrice - SellPrice: {0}, AvgEntryPrice:{1} => OrderPrice: {2}", SellPrice, TrendPrice, SellPumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    SellPriceOld = OrderPrice;
                    SellPumpPriceOld = SellPumpPrice;
                  
                    break;

            }

                AllowCalculateOrder = true;
                return OrderPrice!= null ? (long) OrderPrice : 0;
        }
        public double CalcNormalOrder(string Side)
        {
         

            //every 10 min AllowCalculateBBMiddle is true and then  BuyPumpPrice and BuyDumpPrice are recalculated
            if (AllowCalculateOrder == true)
            {
                AllowCalculateOrder = false;

            }
            if (AllowCalculateBBMiddle)
            {
                AllowCalculateBBMiddle = false;
               //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());
               double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
               SellPumpPrice = OldBBMiddle + 37;
               BuyDumpPrice = OldBBMiddle - 45;
            }
            Candle currentCandle = Candles.Any() ? Candles[0] : null;
            CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);
            OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
            double? SellPrice = CalculateSellPrice(CurrentBook);
            SellPrice = SellPrice + 1;
            log.InfoFormat("CalcNormalOrder - SellPrice: {0}", SellPrice);
            double? BuyPrice = CalculateBuyPrice(CurrentBook);
            //immer +1 usd addieren
            BuyPrice = BuyPrice - 2;
            
            
            log.InfoFormat("CalcNormalOrder - BuyPrice: {0}", BuyPrice);
            //lblAutoUnrealizedROEPercent.Text = Math.Round((Convert.ToDouble(OpenPositions[0].UnrealisedRoePcnt * 100)), 2).ToString();
            
            double? AvgEntryPrice = 0;
            double TrendPrice = 0;
            if ((BuyDumpPrice < BuyDumpPriceOld) || (SellPumpPrice < SellPumpPriceOld))
            {

                bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }

            switch (Side)
            {
                case "Buy":

                    if (BuyPriceOld > BuyPrice)
                    {

                        bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    }
                    else if ((OpenPositions.Count > 0 ) && (currentCandle != null) && (currentCandle.STOCHK <= InputBuySTOCHK))
                    {
                        AvgEntryPrice = OpenPositions[0].AvgEntryPrice ?? BuyPrice;
                        
                        if ((BuyPrice < BuyDumpPrice) & (AllowCalculateBBMiddle == false) & (currentCandle.STOCHK <= InputBuySTOCHK))
                        {
                            if (BuyPrice < AvgEntryPrice)
                            {
                                OrderPrice = BuyPrice;
                            }
                            else
                            {
                                OrderPrice = BuyDumpPrice;
                            }
                        }
                        else if ((TrendPrice < BuyPrice) & (TrendPrice < AvgEntryPrice) & (currentCandle.STOCHK <= InputBuySTOCHK))
                        {
                            //BuyPrice = Convert.ToDouble(OpenPositions[0].AvgEntryPrice ?? BuyPrice);
                            OrderPrice = TrendPrice;
                        }
                        else if ((SumBuyFirstItems > SumSellFirstItems) & (currentCandle.STOCHK >= InputSellSTOCHK))
                        {
                            OrderPrice = BuyPrice;
                            // OrderPrice = BuyDumpPrice;
                            
                        }
                        else
                        {
                            OrderPrice = BuyDumpPrice;
                            BuyPriceOld = OrderPrice;

                            AllowCalculateBBMiddle = false;
                        }
                        log.InfoFormat("CalcNormalOrder - BuyPrice: {0}, TrendPrice:{1}, BuyDumpPrice: {2}, => OrderPrice: {3}", BuyPrice, TrendPrice, BuyDumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                    }
              
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    BuyPriceOld = BuyPrice;
                    BuyDumpPriceOld = BuyDumpPrice;
               
                    break;

                case "Sell":

                    if ((SellPriceOld < SellPrice) & (currentCandle.STOCHK >= InputSellSTOCHK))
                    {

                        bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    }
                    else if ((OpenPositions.Count > 0) & (currentCandle.STOCHK >= InputSellSTOCHK))
                    {
                        AvgEntryPrice = OpenPositions[0].AvgEntryPrice ?? SellPrice;
                        AvgEntryPrice = (AvgEntryPrice + 20);

                        if ((SellPrice < SellPumpPrice) && (AllowCalculateBBMiddle == true) & (currentCandle.STOCHK >= InputSellSTOCHK))
                        {
                            if (SellPrice > AvgEntryPrice)
                            {
                                OrderPrice = SellPrice;
                            }
                            else if (TrendPrice > SellPrice)
                            {
                                OrderPrice = TrendPrice;
                            }
                        }
                     }
                     else
                     {
                            OrderPrice = SellPumpPrice;
                          SellPriceOld = OrderPrice;

                        AllowCalculateBBMiddle = false;
                     }
                    log.InfoFormat("CalcNormalOrder - SellPrice: {0},TrendPrice:{1}, SellPumpPrice: {2}, => OrderPrice: {3}", SellPrice, TrendPrice, SellPumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    SellPriceOld = SellPrice;
                    SellPumpPriceOld = SellPumpPrice;
                  
                    break;

            }

            AllowCalculateOrder = true;
            return OrderPrice != null ? (long)OrderPrice : 0;
        }
        
        public double CalcTakeProfit(string Side)
        {
            
            if (AllowCalculateOrder == true)
            {
                AllowCalculateOrder = false;

            }
            
            //every 10 min AllowCalculateBBMiddle is true and then  BuyPumpPrice and BuyDumpPrice are recalculated
            if (AllowCalculateBBMiddle)
            {
                AllowCalculateBBMiddle = false;
                //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());
                double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
                SellPumpPrice = OldBBMiddle + 37;
                BuyDumpPrice = OldBBMiddle - 45;
            }
            Candle currentCandle = Candles.Any() ? Candles[0] : null;
            CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);

            double? SellPrice = CalculateSellPrice(CurrentBook);
           SellPrice = SellPrice + 3;
            log.InfoFormat("CalcTakeProfit - SellPrice: {0}", SellPrice);
            double? BuyPrice = CalculateBuyPrice(CurrentBook);
            //immer +1 usd addieren
             BuyPrice = BuyPrice-2;
            log.InfoFormat("CalcTakeProfit - BuyPrice: {0}", BuyPrice);

            
            //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());
            Candles = Candles.OrderBy(a => a.TimeStamp).ToList();
            double? AvgEntryPrice = Candles.Any() ? Candles[0].Open : 0d;
            double Open = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;

            /*if ((BuyDumpPrice != BuyDumpPriceOld) & (SellPumpPrice != SellPumpPriceOld))
            {

                bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }
			*/


            switch (Side)
            {
                case "Buy":
                    if ((currentCandle.STOCHK < InputBuySTOCHK) && (currentCandle.STOCHK > InputSellSTOCHK) ) 
                    {

                      //  bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    }
                   
                  
                    
                    
                    else if ((OpenPositions.Count < 6) & (OpenOrders.Count < 6)) //& (currentCandle.STOCHK < InputBuySTOCHK))
                    {
                         OrderPrice = BuyPrice;
                    }
                     else
                    {
                       // OrderPrice = BuyDumpPrice;
                    }
                    log.InfoFormat("CalcTakeProfit - Buy((OpenPositions.Count < 0) & (OpenOrders.Count < 1)),  Open + 1  OrderPrice: {3}", BuyPrice, BuyDumpPrice, OpenPositions.Any() ? OpenPositions[0].AvgEntryPrice : 0, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
            
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    BuyPriceOld = BuyPrice;
                   
                    break;

                case "Sell":
                    if (SellPriceOld > SellPrice)
                    {

                        bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    }
                    //  bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    if ((OpenPositions.Count < 6) & (OpenOrders.Count < 6) & (SumBuyFirstItems < SumSellFirstItems) & (currentCandle.STOCHK < InputBuySTOCHK))
                    {
                        if ((Open > SellPrice) && (AllowCalculateBBMiddle == false))
                        {
                            if (SellPumpPrice > Open)
                            {
                                OrderPrice = SellPumpPrice;
                                SellPumpPriceOld = SellPumpPrice;
                            }
                            else
                            {
                                OrderPrice = Open;
                            }
                        }
                        else if (Open > SellPrice)
                        {
                            OrderPrice = Open;
                        }
                        else
                        {
                            OrderPrice = SellPrice;
                        }
                    }
                    else
                    {
                        OrderPrice = SellPrice;
                    }
                    log.InfoFormat("CalcTakeProfit - SellPrice: {0}, SellPumpPrice: {1}, Open:{2}, OrderPrice: {3}", SellPrice, SellPumpPrice, Open, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    SellPriceOld = SellPrice;
                   
                    break;
            }
            AllowCalculateOrder = true;
            return OrderPrice != null ? (long)OrderPrice : 0;
        }
        public double CalcFirstOrder(string Side)
        {
           

            if (AllowCalculateOrder == true)
            {
                AllowCalculateOrder = false;

            }
            Candle currentCandle = Candles.Any() ? Candles[0] : null;
            //every 10 min AllowCalculateBBMiddle is true and then  BuyPumpPrice and BuyDumpPrice are recalculated
            if (AllowCalculateBBMiddle)
            {
                AllowCalculateBBMiddle = false;
                //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());
                double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
                SellPumpPrice = OldBBMiddle + 37;
                BuyDumpPrice = OldBBMiddle - 45;
            }

            CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);

            double? SellPrice = CalculateSellPrice(CurrentBook);
            SellPrice = SellPrice + 3;
            log.InfoFormat("CalcFirstOrder - SellPrice: {0}", SellPrice);
            double? BuyPrice = CalculateBuyPrice(CurrentBook);
            //immer +1 usd addieren
            // BuyPrice = BuyPrice;
            log.InfoFormat("CalcFirstOrder - BuyPrice: {0}", BuyPrice);

            
            //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());
            Candles = Candles.OrderBy(a => a.TimeStamp).ToList();
            double? AvgEntryPrice = Candles.Any() ? Candles[0].Open : 0d;
            double Open = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
          /*  if ((BuyDumpPrice != BuyDumpPriceOld) || (SellPumpPrice != SellPumpPriceOld))
            {

                bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }
          */



            switch (Side)
            {
                case "Buy":
                    if (BuyPriceOld > BuyPrice)
                    {

                        bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    }
                    
                    else  if (((OpenPositions.Count < 6)&(OpenOrders.Count < 6)& (currentCandle.STOCHK <= InputBuySTOCHK)))
                    {

                          if (Open < BuyPrice)
                         {
                             if ((BuyDumpPrice < Open) & (AllowCalculateBBMiddle == false))
                             {
                                 OrderPrice = BuyDumpPrice;
                                BuyDumpPriceOld = BuyDumpPrice;
                            }
                             else
                             {
                                 OrderPrice = BuyPrice + 1;
                             }
                         }
                         else if (Open < BuyDumpPrice)
                         {

                             OrderPrice = Open;
                         }
                         else
                         {
                             OrderPrice = BuyPrice+1;
                         }
                    }
                    else
                    {
                         OrderPrice = BuyPrice+1;
                    }

                    log.InfoFormat("CalcFirstOrder - BuyPrice: {0}, BuyDumpPrice: {1}, Open:{2}, OrderPrice: {3}", BuyPrice, BuyDumpPrice, Open, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                   
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    BuyPriceOld = BuyPrice;
                   // BuyDumpPriceOld = BuyDumpPrice;
                    break;

                case "Sell":
                    if (SellPriceOld < SellPrice)
                    {

                        bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    }
                    //  bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    else if ((OpenPositions.Count < 6) & (OpenOrders.Count < 6) & (currentCandle.STOCHK >= InputSellSTOCHK))
                    {
                            if ((Open > SellPrice) && (AllowCalculateBBMiddle == false))
                        {
                            if (SellPumpPrice > Open)
                            {
                                OrderPrice = SellPumpPrice;
                                SellPumpPriceOld = SellPumpPrice;
                            }
                            else
                            {
                                OrderPrice = Open;
                            }
                        }
                        else if (Open > SellPrice)
                        {
                            OrderPrice = Open;
                        }
                        else
                        {
                            OrderPrice = SellPrice;
                        }
                    }
                    else if (SellPrice> SellPumpPrice)
                    {
                       OrderPrice = SellPrice;
                    }
                    else
                    {
                        OrderPrice = SellPumpPrice;
                        SellPumpPriceOld = SellPumpPrice;
                    }
               
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    log.InfoFormat("CalcFirstOrder - SellPrice: {0}, SellPumpPrice: {1}, Open:{2}, OrderPrice: {3}", SellPrice, SellPumpPrice, Open, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                    SellPriceOld = SellPrice;
                    //SellPumpPriceOld = SellPumpPrice;
                    break;
            }
            AllowCalculateOrder = true;
            return OrderPrice != null ? (long)OrderPrice : 0;
        }
        public double CalcDumpandPump(string Side)
        {
          //  Thread.Sleep(10000);
            //every 10 min AllowCalculateBBMiddle is true and then  BuyPumpPrice and BuyDumpPrice are recalculated
            if (AllowCalculateOrder == true)
            {
                AllowCalculateOrder = false;

            }
            if (AllowCalculateBBMiddle)
            {
                AllowCalculateBBMiddle = false;
                //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());
                double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
                SellPumpPrice = OldBBMiddle + 37;
                BuyDumpPrice = OldBBMiddle - 45;
            }
            Candle currentCandle = Candles.Any() ? Candles[0] : null;
            CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);
            OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
            double? SellPrice = CalculateSellPrice(CurrentBook);
            SellPrice = SellPrice + 1;
            log.InfoFormat("CalcDumpandPump - SellPrice: {0}", SellPrice);
            double? BuyPrice = CalculateBuyPrice(CurrentBook);
            //immer +1 usd addieren
            BuyPrice = BuyPrice - 2;
            log.InfoFormat("CalcDumpandPump - BuyPrice: {0}", BuyPrice);
            //lblAutoUnrealizedROEPercent.Text = Math.Round((Convert.ToDouble(OpenPositions[0].UnrealisedRoePcnt * 100)), 2).ToString();
            

            double TrendPrice = 0;


            if ((OpenPositions.Count < 10) & (OpenOrders.Count < 8) & (currentCandle.STOCHK <= InputBuySTOCHK) & (currentCandle.STOCHK >= InputSellSTOCHK))
            { 
                bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }
            //      BuyDumpPriceOld = BuyDumpPriceOld;
            //      SellPumpPriceOld = SellPumpPriceOld;
            /*   if ((BuyDumpPrice != BuyDumpPriceOld) || (SellPumpPrice != SellPumpPriceOld))
  {

      bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
  }
            */
            switch (Side)
            {
                case "Buy":

                    
                   if ((OpenPositions.Count < 6) & (OpenOrders.Count < 6) & (currentCandle.STOCHK <= InputBuySTOCHK) & (currentCandle.STOCHK >= InputSellSTOCHK))
                    {
                       // bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);

                        if (BuyPrice < BuyDumpPrice)
                        {

                            OrderPrice = BuyPrice;
                        }
                        else
                        {
                            BuyDumpPriceOld = BuyDumpPrice;
                            OrderPrice = BuyDumpPrice;
                            
                        }
                    }

            
                    else
                    {
                      {
                        if (BuyDumpPrice < BuyDumpPriceOld)
                         {

                        bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                         }
                         // bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);

                        else
                        {

                                BuyDumpPriceOld = BuyDumpPrice;
                                OrderPrice = BuyDumpPrice;
                                
                        }


                      }
                        BuyDumpPriceOld = BuyDumpPrice;
                        OrderPrice = BuyDumpPrice;
                    }
              

                    Mode = "Dump";
             
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    log.InfoFormat("CalcDumpandPump - BuyPrice: {0},BuyDumpPrice: {1},OrderPrice: {2}" , BuyPrice, BuyDumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                   
                    break;

                case "Sell":
                    OrderPrice = SellPumpPrice;
                    Mode = "Pump";
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    log.InfoFormat("CalcDumpandPump - SellPrice: {0}, TrendPrice: {1} => SellDumpPrice: {2}OrderPrice: {3}", SellPrice, TrendPrice, SellPumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)


                    if (((OpenPositions.Count < 6) & (OpenOrders.Count < 6) & (currentCandle.STOCHK >= InputSellSTOCHK)))
                    {
                        

                        if (SellPrice > SellPumpPrice)
                        {

                            OrderPrice = SellPrice;
                        }
                        else
                        {
                            SellPumpPriceOld = SellPumpPrice;
                            OrderPrice = SellPumpPrice;
                            
                        }
                    }
                    else
                    {
                        {
                          if (SellPumpPrice > SellPumpPriceOld)
                          {

                            bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                          }
                            // bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);

                            else
                            {
                              
                            }
                            SellPumpPriceOld = SellPumpPrice;
                            OrderPrice = SellPumpPrice;
                        }

                    }
                    
                    break;
            }

            AllowCalculateOrder = true;
            return OrderPrice != null ? (long)OrderPrice : 0;
        }

        private void MakeOrder(string Side, int Qty, double Price = 0)
        {
            
            if (chkCancelWhileOrdering.Checked)
            {
               
               bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }
            //CHANGE 1: Alle Buy Methoden nur mit "Limit Post Only" ausgeführt
            switch (Side)
            {
                case "Buy":
                    DoLimitPostOnlyMakeOrder(Side, Qty, Price);
                    break;
                case "Sell":
                    switch (ddlOrderType.SelectedItem)
                    {
                        case "Limit Post Only":
                            DoLimitPostOnlyMakeOrder(Side, Qty, Price);
                            break;
                        case "Market":
                            DoLimitPostOnlyMakeOrder(Side, Qty, Price);
                            break;
                    }
                    break;
            }
        }

        private void NormalOrder(string Side, int Qty, double Price = 0)
        {
            if (chkCancelWhileOrdering.Checked)
            {
              //  bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }
            //CHANGE 1: Alle Buy Methoden nur mit "Limit Post Only" ausgeführt
        
            switch (Side)
            {
                case "Buy":
                    switch (ddlAutoOrderType.SelectedItem)
                    {
                        case "Limit Post Only":
                            DoLimitPostOnly(Side, Qty, Price);
                            break;
                        case "Market":
                            DoLimitPostOnly(Side, Qty, Price);
                            break;
                    }
                   
                    break;
                case "Sell":
                    switch (ddlAutoOrderType.SelectedItem)
                    {
                        case "Limit Post Only":
                            DoLimitPostOnly(Side, Qty, Price);
                           break;
                        case "Market":
                            DoLimitPostOnly(Side, Qty, Price);
                            break;
                    }
                    break;
            }
        }
        private void TakeProfit(string Side, int Qty, double Price = 0)
        {
            if (chkCancelWhileOrdering.Checked)
            {
                //  bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }
            //CHANGE 1: Alle Buy Methoden nur mit "Limit Post Only" ausgeführt
           
            switch (Side)
            {
                case "Buy":
                    switch (ddlAutoOrderType.SelectedItem)
                    {
                        case "Limit Post Only":
                            DoLimitPostOnly2(Side, Qty, Price);
                            break;
                        case "Market":
                            DoLimitPostOnly2(Side, Qty, Price);
                            break;
                    }

                    break;
                case "Sell":
                    switch (ddlAutoOrderType.SelectedItem)
                    {
                        case "Limit Post Only":
                            DoLimitPostOnly2(Side, Qty, Price);
                            break;
                        case "Market":
                            DoLimitPostOnly2(Side, Qty, Price);
                            break;
                    }
                    break;
            }
        }
        private void FirstOrder(string Side, int Qty, double Price = 0)
        {
            if (chkCancelWhileOrdering.Checked)
            {
                //  bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }
            //CHANGE 1: Alle Buy Methoden nur mit "Limit Post Only" ausgeführt
            
            switch (Side)
            {
                case "Buy":
                    switch (ddlAutoOrderType.SelectedItem)
                    {
                        case "Limit Post Only":
                            DoLimitPostOnly3(Side, Qty, Price);
                            break;
                        case "Market":
                            DoLimitPostOnly3(Side, Qty, Price);
                            break;
                    }

                    break;
                case "Sell":
                    switch (ddlAutoOrderType.SelectedItem)
                    {
                        case "Limit Post Only":
                            DoLimitPostOnly3(Side, Qty, Price);
                            break;
                        case "Market":
                            DoLimitPostOnly3(Side, Qty, Price);
                            break;
                    }
                    break;
            }
        }
        private void DumpandPump(string Side, int Qty, double Price = 0)
        {
            if (chkCancelWhileOrdering.Checked)
            {
                //  bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }
            //CHANGE 1: Alle Buy Methoden nur mit "Limit Post Only" ausgeführt

            switch (Side)
            {
                case "Buy":
                    switch (ddlAutoOrderType.SelectedItem)
                    {
                        case "Limit Post Only":
                            DoLimitPostOnly4(Side, Qty, Price);
                            break;
                        case "Market":
                            DoLimitPostOnly4(Side, Qty, Price);
                            break;
                    }

                    break;
                case "Sell":
                    switch (ddlAutoOrderType.SelectedItem)
                    {
                        case "Limit Post Only":
                            DoLimitPostOnly4(Side, Qty, Price);
                            break;
                        case "Market":
                            DoLimitPostOnly4(Side, Qty, Price);
                            break;
                    }
                    break;
            }
        }
        //CHANGE 1: Alle Buy Methoden nur mit "Limit Post Only" ausgeführt
        private string DoLimitPostOnlyMakeOrder(string Side, int Qty, double Price = 0)
        {
            if (Price == 0)
            {
                Price = CalculateMakerOrderPrice(Side);
            }
            return bitmex.PostOrderPostOnly(ActiveInstrument.Symbol, Side, Price, Qty);
        }
        private string DoLimitPostOnly(string Side, int Qty, double Price = 0)
        {
            if (Price == 0)
            {
                Price = CalcNormalOrder(Side);
            }
            return bitmex.PostOrderPostOnly(ActiveInstrument.Symbol, Side, Price, Qty);
        }

        private string DoLimitPostOnly2(string Side, int Qty, double Price = 0)
        {
            if (Price == 0)
            {
                Price = CalcTakeProfit(Side);
            }
            return bitmex.PostOrderPostOnly(ActiveInstrument.Symbol, Side, Price, Qty);
        }
        private string DoLimitPostOnly3(string Side, int Qty, double Price = 0)
        {
            if (Price == 0)
            {
                Price = CalcFirstOrder(Side);
            }
            return bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
        }
        private string DoLimitPostOnly4(string Side, int Qty, double Price = 0)
        {
            if (Price == 0)
            {
                Price = CalcDumpandPump(Side);
            }
            return bitmex.PostOrderPostOnly(ActiveInstrument.Symbol, Side, Price, Qty);
        }
        private void btnBuy_Click(object sender, EventArgs e)
        {
            MakeOrder("Buy", Convert.ToInt32(nudQty.Value));
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            MakeOrder("Sell", Convert.ToInt32(nudQty.Value));
        }

        private void btnCancelOpenOrders_Click(object sender, EventArgs e)
        {
            bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
        }

        private void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAPISettings();
            InitializeAPI();
        }

        private void ddlSymbol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveInstrument = bitmex.GetInstrument(((Instrument)ddlSymbol.SelectedItem).Symbol)[0];
        }

        public void UpdateCandles()
        {
            // Get candles
            Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());

            Candles = Candles.OrderBy(a => a.TimeStamp).ToList();

            // Set Indicator Info
            foreach (Candle c in Candles)
            {
                c.PCC = Candles.Count(a => a.TimeStamp < c.TimeStamp);

                int MA1Period = Convert.ToInt32(nudMA1.Value);
                int MA2Period = Convert.ToInt32(nudMA2.Value);

                if (c.PCC >= MA1Period)
                {
                    // Get the moving average over the last X periods using closing -- INCLUDES CURRENT CANDLE <=
                    c.MA1 = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(MA1Period).Average(a => a.Close);
                } // With not enough candles, we don't set to 0, we leave it null.

                if (c.PCC >= MA2Period)
                {
                    // Get the moving average over the last X periods using closing -- INCLUDES CURRENT CANDLE <=
                    c.MA2 = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(MA2Period).Average(a => a.Close);
                } // With not enough candles, we don't set to 0, we leave it null.

                if (c.PCC >= BBLength) // Bollinger Bands
                {
                    // BBand calculation available on trading view wiki: https://www.tradingview.com/wiki/Bollinger_Bands_(BB)
                    // You might need to also google how to calculate standard deviation as well: https://stackoverflow.com/questions/14635735/how-to-efficiently-calculate-a-moving-standard-deviation

                    // BBMiddle is just 20 period moving average
                    c.BBMiddle = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(BBLength).Average(a => a.Close);

                    // Calculating the std deviation is important, and the hard part.
                    double total_squared = 0;
                    double total_for_average = Convert.ToDouble(Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(BBLength).Sum(a => a.Close));
                    foreach (Candle cb in Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(BBLength).ToList())
                    {
                        total_squared += Math.Pow(Convert.ToDouble(cb.Close), 2);
                    }
                    double stdev = Math.Sqrt((total_squared - Math.Pow(total_for_average, 2) / BBLength) / BBLength);
                    c.BBUpper = c.BBMiddle + (stdev * BBMultiplier);
                    c.BBLower = c.BBMiddle - (stdev * BBMultiplier);
                }


                // EMA
                if (c.PCC >= EMA1Period)
                {
                    double p1 = EMA1Period + 1;
                    double EMAMultiplier = Convert.ToDouble(2 / p1);

                    if (c.PCC == EMA1Period)
                    {
                        // This is our seed EMA, using SMA of EMA1 Period for EMA 1
                        c.EMA1 = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(EMA1Period).Average(a => a.Close);
                    }
                    else
                    {
                        double? LastEMA = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().EMA1;
                        c.EMA1 = ((c.Close - LastEMA) * EMAMultiplier) + LastEMA;
                    }
                }

                if (c.PCC >= EMA2Period)
                {
                    double p1 = EMA2Period + 1;
                    double EMAMultiplier = Convert.ToDouble(2 / p1);

                    if (c.PCC == EMA2Period)
                    {
                        // This is our seed EMA, using SMA
                        c.EMA2 = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(EMA2Period).Average(a => a.Close);
                    }
                    else
                    {
                        double? LastEMA = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().EMA2;
                        c.EMA2 = ((c.Close - LastEMA) * EMAMultiplier) + LastEMA;
                    }
                }

                if (c.PCC >= EMA3Period)
                {
                    double p1 = EMA3Period + 1;
                    double EMAMultiplier = Convert.ToDouble(2 / p1);

                    if (c.PCC == EMA3Period)
                    {
                        // This is our seed EMA, using SMA
                        c.EMA3 = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(EMA3Period).Average(a => a.Close);
                    }
                    else
                    {
                        double? LastEMA = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().EMA3;
                        c.EMA3 = ((c.Close - LastEMA) * EMAMultiplier) + LastEMA;
                    }
                }

                // MACD
                // We can only do this if we have the longest EMA period, EMA1
                if (c.PCC >= EMA1Period)
                {

                    double p1 = MACDEMAPeriod + 1;
                    double MACDEMAMultiplier = Convert.ToDouble(2 / p1);

                    c.MACDLine = (c.EMA2 - c.EMA1); // default is 12EMA - 26EMA
                    if (c.PCC == EMA1Period + MACDEMAPeriod - 1)
                    {
                        // Set this to SMA of MACDLine to seed it
                        c.MACDSignalLine = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(MACDEMAPeriod).Average(a => (a.MACDLine));
                    }
                    else if (c.PCC > EMA1Period + MACDEMAPeriod - 1)
                    {
                        // We can calculate this EMA based off past candle EMAs
                        double? LastMACDSignalLine = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().MACDSignalLine;
                        c.MACDSignalLine = ((c.MACDLine - LastMACDSignalLine) * MACDEMAMultiplier) + LastMACDSignalLine;
                    }
                    c.MACDHistorgram = c.MACDLine - c.MACDSignalLine;
                }

                // ATR, setting TR
                if (c.PCC == 0)
                {
                    c.SetTR(c.High);
                }
                else if (c.PCC > 0)
                {
                    c.SetTR(Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().Close);
                }

                // Setting ATRs
                if (c.PCC == ATR1Period - 1)
                {
                    c.ATR1 = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(ATR1Period).Average(a => a.TR);
                }
                else if (c.PCC > ATR1Period - 1)
                {
                    double p1 = ATR1Period + 1;
                    double ATR1Multiplier = Convert.ToDouble(2 / p1);
                    double? LastATR1 = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().ATR1;
                    c.ATR1 = ((c.TR - LastATR1) * ATR1Multiplier) + LastATR1;
                }

                if (c.PCC == ATR2Period - 1)
                {
                    c.ATR2 = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(ATR2Period).Average(a => a.TR);
                }
                else if (c.PCC > ATR2Period - 1)
                {
                    double p1 = ATR2Period + 1;
                    double ATR2Multiplier = Convert.ToDouble(2 / p1);
                    double? LastATR2 = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().ATR2;
                    c.ATR2 = ((c.TR - LastATR2) * ATR2Multiplier) + LastATR2;
                }

                // For RSI
                if (c.PCC == RSIPeriod - 1)
                {
                    // AVG Gain is average of just gains, for all periods, (14), not just periods with gains.  Same goes for losses but with losses.
                    c.AVGGain = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Where(a => a.GainOrLoss > 0).Take(RSIPeriod).Sum(a => a.GainOrLoss) / RSIPeriod;
                    c.AVGLoss = (Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Where(a => a.GainOrLoss < 0).Take(RSIPeriod).Sum(a => a.GainOrLoss) / RSIPeriod) * -1;

                    c.RS = c.AVGGain / c.AVGLoss; // Only like this on first one (seeding it)
                    c.RSI = 100 - (100 / (1 + c.RS));
                }
                else if (c.PCC > RSIPeriod - 1)
                {
                    // AVG Gain is average of just gains, for all periods, (14), not just periods with gains.  Same goes for losses but with losses.
                    c.AVGGain = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Where(a => a.GainOrLoss > 0).Take(RSIPeriod).Sum(a => a.GainOrLoss) / RSIPeriod;
                    c.AVGLoss = (Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Where(a => a.GainOrLoss < 0).Take(RSIPeriod).Sum(a => a.GainOrLoss) / RSIPeriod) * -1;

                    double? LastAVGGain = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().AVGGain;
                    double? LastAVGLoss = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().AVGLoss;
                    double? Gain = 0;
                    double? Loss = 0;

                    if (c.GainOrLoss > 0)
                    {
                        Gain = c.GainOrLoss;
                    }
                    else if (c.GainOrLoss < 0)
                    {
                        Loss = c.GainOrLoss;
                    }

                    c.RS = (((LastAVGGain * (RSIPeriod - 1)) + Gain) / RSIPeriod) / (((LastAVGLoss * (RSIPeriod - 1)) + Loss) / RSIPeriod);
                    c.RSI = 100 - (100 / (1 + c.RS));
                }

                // For STOCH
                if (c.PCC >= STOCHLookbackPeriod - 1)
                {
                    double? HighInLookback = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(STOCHLookbackPeriod).Max(a => a.High);
                    double? LowInLookback = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(STOCHLookbackPeriod).Min(a => a.Low);

                    c.STOCHK = ((c.Close - LowInLookback) / (HighInLookback - LowInLookback)) * 100;
                }
                if (c.PCC >= STOCHLookbackPeriod - 1 + STOCHDPeriod) // difference of -1 and 2 is 3, to allow for the 3 period SMA required for STOCH
                {
                    c.STOCHD = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(STOCHDPeriod).Average(a => a.STOCHK);
                }
            }

            Candles = Candles.OrderByDescending(a => a.TimeStamp).ToList();

            // Show Candles
            dgvCandles.DataSource = Candles;

            // This is where we are going to determine the "mode" of the bot based on MAs, trades happen on another timer
            if (Running)//We could set this up to also ignore setting bot mode if we've already reviewed current candles
                        //  However, if you wanted to use info from the most current candle, that wouldn't work well
            {
                // We really only need to set bot mode if the bot is running
                btnAutomatedTrading.Text = "Stop - " + Mode;// so we can see what the mode of the bot is while running
                                                            /* }
                                                             else
                                                             {
                                                                 
                                                             }
                                                             */
                SetBotMode();

                

            }
        }


        private void SetBotMode()
        {


            Candle currentCandle = Candles.Any() ? Candles[0] : null;
            CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);
            if (currentCandle == null)
            {
                //   Mode = "Wait";
                // wenn "Peak kommt" dann bevor er prüft ob er buy oder sell ausführen soll 
                // müssen alle orders gelöscht werden
                bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }
            else
            {

                if (rdoBuy.Checked)
                {
                    //wenn es keine OpenPosition und keine OpenOrder gibt, dann darf auch buy machen
                    // Nach Buy kommt  Close and wait
                    // c.STOCHK unter 20%
                    if (currentCandle.STOCHK <= InputBuySTOCHK)  //|| (totalOpenPositions == 0 && totalOpenOrders == 0))
                    {
                        Mode = "Buy";
                    }
                    // c.STOCHK über 10%
                    else if ((currentCandle.STOCHK > InputBuySTOCHK) || (currentCandle.STOCHK < InputSellSTOCHK))
                    {
                        Mode = "Wait";
                    }
                }
                else if (rdoSell.Checked)
                {
                    // Nach Sell kommt  Close and wait

                    if (currentCandle.STOCHK >= InputSellSTOCHK)
                    {
                        Mode = "Sell";
                    }
                    else
                    {
                        Mode = "Wait";
                    }
                }
                else if (rdoSwitch.Checked)
                {
                  
                   AllowCalculateOrder = true;
                    {
                        currentCandle = Candles.Any() ? Candles[0] : null;
                       
                        double? STOCHK = Candles[0].STOCHK;
                        // Ist Summe Buyorders < Sellorders
                        if ((SumBuyFirstItems < SumSellFirstItems) & (STOCHK >= InputSellSTOCHK) & Mode != "Buy")
                        {
                            if (OpenOrders.Count > 6)
                            {
                                bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                            }
                            if ((OpenPositions.Count < 3) & (OpenOrders.Count < 3))
                            {
                                AllowCalculateOrder = false;
                                Mode = "Sell";
                                NormalOrder("Sell", Convert.ToInt32(nudQty.Value));

                            }
                            else
                            {
                                if (OpenOrders.Count == 0)
                                {
                                    AllowCalculateOrder = false;
                                    int Qty = 1;
                                    String Side = "Sell";
                                    bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                                }
                            }

                        }
                        else if ((SumBuyFirstItems > SumSellFirstItems) & (STOCHK < InputBuySTOCHK) & Mode != "Sell")
                        {
                            if (OpenOrders.Count > 6)
                            {
                                bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                            }
                            if ((OpenPositions.Count < 3) & (OpenOrders.Count < 3))
                            {
                                AllowCalculateOrder = false;
                                Mode = "Buy";
                                NormalOrder("Buy", Convert.ToInt32(nudQty.Value));

                            }
                            else
                            {
                                if (OpenOrders.Count == 0)
                                {
                                    AllowCalculateOrder = false;
                                    int Qty = 1;
                                    String Side = "Buy";
                                    bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                                }
                            }


                        }

                          else if ((STOCHK > InputBuySTOCHK) & (STOCHK < InputSellSTOCHK) & Mode != "Dump" & Mode != "Sell" & Mode != "Buy") // && (OpenPositions.Count > 0) && (OpenOrders.Count != 0))
                        {
                           
                            if (OpenOrders.Count > 3)
                            {
                                bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                            }
                            else if (OpenOrders.Count > 1)
                            {
                                AllowCalculateOrder = false;
                                Mode = "Pump";
                                int Quantity = 1;
                                DumpandPump("Sell", Quantity);

                                log.InfoFormat("Setmode Wait (currentCandle.STOCHK > InputBuySTOCHK) & (currentCandle.STOCHK < InputSellSTOCHK) FirstOrderSell DumpandPump Buy DumpandPump Sell ");

                            }
                            else
                            {

                            }
                        }


                        else if ((STOCHK > InputBuySTOCHK) & (STOCHK < InputSellSTOCHK) & Mode != "Pump" & Mode != "Sell" & Mode != "Buy") // && (OpenPositions.Count > 0) && (OpenOrders.Count != 0))
                        {
                           

                            if (OpenOrders.Count > 3)
                            {
                                bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                            }
                           else if (OpenOrders.Count > 1)
                            {
                                AllowCalculateOrder = false;
                                Mode = "Dump";
                                int Quantity2 = 1;
                                DumpandPump("Buy", Quantity2);

                                log.InfoFormat("Setmode Wait (currentCandle.STOCHK > InputBuySTOCHK) & (currentCandle.STOCHK < InputSellSTOCHK) FirstOrderSell DumpandPump Buy DumpandPump Sell ");
                            }
                            else
                            {

                            }
                        }
                        else if ((STOCHK > InputSellSTOCHK) & (OpenPositions.Count == 0) &(OpenOrders.Count != 0) & Mode != "FirstOrder Sell")
                        {

                            AllowCalculateOrder = false;
                            Mode = "FirstOrder Sell"; 
                            int Quantity2 = 1;

                            FirstOrder("Sell", Quantity2);
                            log.InfoFormat("Setmode Wait (currentCandle.STOCHK > InputBuySTOCHK) & (currentCandle.STOCHK < InputSellSTOCHK) FirstOrderSell Sell ");
                        }
                        else if ((STOCHK < InputBuySTOCHK) & (OpenPositions.Count == 0) && (OpenOrders.Count != 0) & Mode != "FirstOrder Buy")
                        {

                            AllowCalculateOrder = false;
                            Mode = "FirstOrder Buy";
                            int Quantity2 = 1;

                            FirstOrder("Buy", Quantity2);
                            log.InfoFormat("Setmode Wait (currentCandle.STOCHK > InputBuySTOCHK) & (currentCandle.STOCHK < InputSellSTOCHK) FirstOrderSell Buy ");
                        }
                        /*
                        else if (OpenOrders.Count < 2)

                        {
                            int Qty = 1;
                            String Side = "Sell";
                            bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                            //FirstOrder("Sell", Quantity);
                            log.InfoFormat("tmrAutoTradeExecution_Tick (OpenPositions[0].CurrentQty < 0) First Order Sell");
                        } */
                        else
                        {
                            /*      Mode = "Waitelse";
                                  int Quantity = 1;
                                  FirstOrder("Sell", Quantity);
                                  int Quantity2 = 1;
                                  DumpandPump("Buy", Quantity2);
                                  int Quantity3 = 1;
                                  DumpandPump("Sell", Quantity3);
                                  log.InfoFormat("Setmode Wait (OpenOrders.Count == 0) FirstOrder Sell  FirstOrder Buy  FirstOrder Sell else");
                          */
                        }




                    }
                        log.InfoFormat("SetBotMode - Mode: {0}, _lastMode: {1}", Mode, _lastMode);
                        _lastMode = Mode;
                    }
                }
            }
        
    
        
        private void tmrCandleUpdater_Tick(object sender, EventArgs e)
        {
            if(chkUpdateCandles.Checked) //&& Mode == "Wait" && Mode == "Buy" && Mode == "Sell")
            {
                UpdateCandles();
                
            }
            //else
            //{
            //    UpdateCandles();
            //}
        }

        private void chkUpdateCandles_CheckedChanged(object sender, EventArgs e)
        {
            if(chkUpdateCandles.Checked)
            {
                tmrCandleUpdater.Start();
            }
            else
            {
                tmrCandleUpdater.Stop();
            }
        }

        private void ddlCandleTimes_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCandles();
        }

        private void btnAutomatedTrading_Click(object sender, EventArgs e)
        {
            if(btnAutomatedTrading.Text == "Start")
            {
                tmrAutoTradeExecution.Start();
                tmrUpdateBuySellFirstPriceOrders.Start();
                tmrUpdateBBMiddle.Start();
                btnAutomatedTrading.Text = "Stop - " + Mode;
                btnAutomatedTrading.BackColor = Color.Red;
                Running = true;
                rdoBuy.Enabled = false;
                rdoSell.Enabled = false;
                rdoSwitch.Enabled = false;
            }
            else
            {
                tmrAutoTradeExecution.Stop();
                tmrUpdateBuySellFirstPriceOrders.Stop();
                tmrUpdateBBMiddle.Stop();
                btnAutomatedTrading.Text = "Start";
                btnAutomatedTrading.BackColor = Color.LightGreen;
                Running = false;
                rdoBuy.Enabled = true;
                rdoSell.Enabled = true;
                rdoSwitch.Enabled = true; 
            }            
        }

      private void tmrAutoTradeExecution_Tick(object sender, EventArgs e)
        {
            OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
            OpenOrders = bitmex.GetOpenOrders(ActiveInstrument.Symbol);
            Candle currentCandle = Candles.Any() ? Candles[0] : null;
           
            int Quantity2 = 1;
            DumpandPump("Buy", Quantity2);
            int Quantity3 = 1;
            DumpandPump("Sell", Quantity3);
            log.InfoFormat("tmrAutoTradeExecution_Tick Dump Pump ");
            if (OpenPositions.Any())
            // if (OpenOrders.Count > 0) //(OpenPositions.Count > 0) 
            {
                
               

                //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());

                log.InfoFormat("tmrAutoTradeExecution_Tick + SetBotMode();");
                double? averageEntryPrice = OpenPositions[0].AvgEntryPrice ?? 0;
                if (OpenPositions[0].CurrentQty == 0)
                {
                    averageEntryPrice = 0;
                }
                double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
                double? part = (OldBBMiddle / averageEntryPrice) * 100;
                percent = part - 100;
                percent = percent * -100;
                //        var InputAutoWin = Convert.ToDouble(nudAutoMarketTakeProfitPercent.Value);
                if (currentCandle == null)
                {
                    //   Mode = "Wait";
                    // wenn "Peak kommt" dann bevor er prüft ob er buy oder sell ausführen soll 
                    // müssen alle orders gelöscht werden
                    bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    log.InfoFormat("tmrAutoTradeExecution_Tick candle Null Cancel Order");
                }
                
            }

            /* else if (OpenPositions[0].CurrentQty < 0)
             {

                 double? averageEntryPrice = OpenPositions[0].AvgEntryPrice;
                 double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
                 double? part = (OldBBMiddle / averageEntryPrice) * 100;
                 var percent = part - 100;
                 percent = percent * 100;
                 var InputAutoWin = Convert.ToDouble(nudAutoMarketTakeProfitPercent.Value);
             }
             */

            // See if we are taking profits on open positions, and have positions open and we aren't in our buy or sell periods

            if ((chkAutoMarketTakeProfits.Checked) && (OpenPositions.Any())) //&& Mode != "Wait" && Mode != "Waitelse")) //&& ((OpenPositions.Any()) && ((OpenPositions[0].CurrentQty > 0) || (OpenPositions[0].CurrentQty < 0))))
            {
                // Thread.Sleep(4000);

                if (AllowCalculateOrder == true)
                {
                    AllowCalculateOrder = false;

                }

                //double? OldBBMiddle = Open; //Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;// Candles.OrderByDescending(a => a.TimeStamp).Take(BBLength).Average(a => a.Close);
                // OldBBMiddle = OldBBMiddle + 5;
                double? averageEntryPrice = OpenPositions[0].AvgEntryPrice ?? 0;
                if (OpenPositions[0].CurrentQty == 0)
                {
                    averageEntryPrice = 0;
                }
                double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
                double? part = (OldBBMiddle / averageEntryPrice) * 100;
                percent = part - 100;
                percent = percent * -100;

                double InputAutoWin = Convert.ToDouble(nudAutoMarketTakeProfitPercent.Value);
                log.InfoFormat("(chkAutoMarketTakeProfits.Checked && Mode != Sell && Mode != Buy && OpenPositions.Any() && ((OpenPositions[0].CurrentQty > 0) || (OpenPositions[0].CurrentQty < 0)))");
                lblAutoUnrealizedROEPercent.Text = percent.ToString();
            
            //     bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);

             if ((percent >= InputAutoWin) || (percent <= -20))
            {
                int PositionDifference = Convert.ToInt32(nudAutoQuantity.Value - OpenPositions[0].CurrentQty);
                // Make a market order to close out the position, also cancel all orders so nothing else fills if we had unfilled limit orders still open.
                if (OpenPositions[0].CurrentQty < 0)
                {
                    //       bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    //TODO CurrentQty ?????
                    //  String Side = "Sell";
                    // Convert.ToInt32(OpenPositions[0].CurrentQty);
                    AllowCalculateOrder = true;
                    //  bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                    TakeProfit("Buy", Convert.ToInt32(OpenPositions[0].CurrentQty));
                    log.InfoFormat("tmrAutoTradeExecution_Tick (OpenPositions[0].CurrentQty > 0) Sell");
                }
                else if (OpenPositions[0].CurrentQty > 0)
                {
                    //       bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    //TODO Quantity ?????
                    //     String Side = "Buy";
                    // Convert.ToInt32(OpenPositions[0].CurrentQty * -1);
                    AllowCalculateOrder = true;
                    //   bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                    TakeProfit("Sell", Convert.ToInt32(OpenPositions[0].CurrentQty));
                    log.InfoFormat("tmrAutoTradeExecution_Tick (OpenPositions[0].CurrentQty < 0) Buy");
                }
                // Get our positions and orders again to be able to process rest of logic with new information.





                OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
                OpenOrders = bitmex.GetOpenOrders(ActiveInstrument.Symbol);
                SetBotMode();
            }
            }
            
            /*
            else if (rdoBuy.Checked)
            {

                switch (Mode)
                {

                    case "Buy":
                        // See if we already have a position open
                        if (OpenPositions.Any())
                        {
                            // We have an open position, is it at our desired quantity?
                            if (OpenPositions[0].CurrentQty < nudAutoQuantity.Value)
                            {
                                // If we have an open order, edit it
                                if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // We still have an open sell order, cancel that order, make a new buy order
                                    //   string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Buy", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoBuy.Checked) Buy");
                                }
                                else if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Sell", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoBuy.Checked) Sell");
                                }

                            } // No else, it is filled to where we want.
                        }
                        else
                        {
                            if (OpenOrders.Any())
                            {
                                // If we have an open order, edit it
                                if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // We still have an open sell order, cancel that order, make a new buy order
                                    //  string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    //todo OpenPositions[0] knallt!!! wir haben kein OpenPosition
                                    //NormalOrder("Buy", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                    int Quantity = 1;
                                    NormalOrder("Buy", Quantity);
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoBuy.Checked) (OpenOrders.Any()) Buy");
                                }
                                else if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    int Quantity = -1;
                                    NormalOrder("Sell", Quantity);
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoBuy.Checked) (OpenOrders.Any()) Sell");
                                }
                            }
                            else
                            {
                                NormalOrder("Buy", Convert.ToInt32(nudAutoQuantity.Value));
                                log.InfoFormat("tmrAutoTradeExecution_Tick (rdoBuy.Checked) (OpenOrders.Any()) Buy Else");
                            }
                        }
                        break;
                    case "CloseAndWait":
                        // See if we have open positions, if so, close them
                        if (OpenPositions.Any())
                        {
                            // Now, do we have open orders?  If so, we want to make sure they are at the right price
                            if (OpenOrders.Any())
                            {
                                if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // We still have an open buy order, cancel that order, make a new sell order
                                    //  string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Sell", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                }
                                else if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Buy", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                }

                            }
                            else
                            {
                                // No open orders, need to make an order to sell
                                NormalOrder("Sell", Convert.ToInt32(OpenPositions[0].CurrentQty));
                            }
                        }
                        else if (OpenOrders.Any())
                        {
                            // We don't have an open position, but we do have an open order, close that order, we don't want to open any position here.
                            // string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                        }
                        break;
                    case "Wait":
                        // We are in wait mode, no new buying or selling - close open orders
                        if (OpenOrders.Any())
                        {
                            //  string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                            log.InfoFormat("tmrAutoTradeExecution_Tick (rdoBuy.Checked) (OpenOrders.Any()) CloseandWait Wait do nothing");
                        }
                        break;
                }
            }
            else if (rdoSell.Checked)
            {
                switch (Mode)
                {
                    case "Sell":
                        // See if we already have a position open
                        if (OpenPositions.Any())
                        {
                            // We have an open position, is it at our desired quantity?
                            if (OpenPositions[0].CurrentQty < nudAutoQuantity.Value)
                            {
                                // If we have an open order, edit it
                                if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // We still have an open Buy order, cancel that order, make a new Sell order
                                    //  string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Sell", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSell.Checked) Sell");
                                }
                                else if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Buyl", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSell.Checked) Buy");
                                }

                            } // No else, it is filled to where we want.
                        }
                        else
                        {
                            if (OpenOrders.Any())
                            {
                                // If we have an open order, edit it
                                if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // We still have an open buy order, cancel that order, make a new sell order
                                    // string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    //TODO OpenPositions[0] knallt: wir haben kein OpenPosition
                                    int Quantity = 1;
                                    NormalOrder("Sell", Quantity);
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSell.Checked) (OpenOrders.Any())Sell");


                                    //NormalOrder("Sell", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                }
                                else if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    int Quantity = 1;
                                    NormalOrder("Buy", Quantity);
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSell.Checked) (OpenOrders.Any())Buy");

                                }
                            }
                        }
                        break;
                    case "CloseAndWait":
                        // See if we have open positions, if so, close them
                        if (OpenPositions.Any())
                        {
                            // Now, do we have open orders?  If so, we want to make sure they are at the right price
                            if (OpenOrders.Any())
                            {
                                if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // We still have an open sell order, cancel that order, make a new buy order
                                    //  string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Buy", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                }
                                else if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Sell", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                }

                            }
                            else
                            {
                                // No open orders, need to make an order to sell
                                //MakeOrder("Sell, Convert.ToInt32(OpenPositions[0].CurrentQty));
                            }
                        }
                        else if (OpenOrders.Any())
                        {
                            // We don't have an open position, but we do have an open order, close that order, we don't want to open any position here.
                            //  string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                        }
                        break;
                    case "Wait":
                        // We are in wait mode, no new buying or selling - close open orders
                        if (OpenOrders.Any())
                        {
                            //   string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                            log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSell.Checked) (OpenOrders.Any()) CloseandWait Wait do nothing");
                        }
                        break;
                }
            }
            else if (rdoSwitch.Checked)
            {

                switch (Mode)
                {

                    case "Buy":
                      
                        if (OpenPositions.Any() && (currentCandle.STOCHK < InputBuySTOCHK))
                        {
                            currentCandle = Candles.Any() ? Candles[0] : null;
                            int PositionDifference = Convert.ToInt32(nudAutoQuantity.Value+1);

                            if (OpenOrders.Any())
                            {
                                // If we have an open order, edit it
                                if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // We still have an open Sell order, cancel that order, make a new Buy order
                                    //    string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                             
                                    NormalOrder("Buy", Convert.ToInt32(PositionDifference+1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Buy NormalOrder");
                                }
                                else if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                  
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Sell", Convert.ToInt32(PositionDifference+1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Buy NormalOrder");
                                }
                            }
                            else
                            {
                                // No open orders, make one for the difference
                                if (PositionDifference != 0)
                                {
                                    Mode = "FirstOrder";
                                     Quantity2 = 1;

                                    FirstOrder("Sell", Quantity2);
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Buy Else FirstOrder");
                                }

                            }

                        }
                        else
                        {
                            if  (currentCandle.STOCHK < InputSellSTOCHK)// ((OpenOrders.Any() &&))
                            {
                                // If we have an open order, edit it
                                if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // We still have an open Sell order, cancel that order, make a new Buy order
                                    //     string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Buy", Convert.ToInt32(PositionDifference+1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked)(OpenOrders.Any()) Buy NormalOrder");
                                }
                                else if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Sell", Convert.ToInt32(PositionDifference+1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked)(OpenOrders.Any()) Sell NormalOrder");
                                }
                            }
                            else
                            {
                                NormalOrder("Sell", Convert.ToInt32(nudQty.Value));
                                log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked)(OpenOrders.Any()) Sell NormalOrder Else NormalOrder");
                            }
                        }
                        break;
                    case "Sell":
                        currentCandle = Candles.Any() ? Candles[0] : null;
                        if (currentCandle.STOCHK < InputSellSTOCHK) // (OpenPositions.Any() && 
                        {

                            int PositionDifference = Convert.ToInt32(nudAutoQuantity.Value + OpenPositions[0].CurrentQty );

                            if (OpenOrders.Any())
                            {
                                // If we have an open order, edit it
                                if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // We still have an open Sell order, cancel that order, make a new Buy order
                                    string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Sell", Convert.ToInt32(nudQty.Value));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Sell NormalOrder");
                                }
                                else if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Buy", Convert.ToInt32(nudQty.Value));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Sell NormalOrder");
                                }
                            }
                            else
                            {

                                // No open orders, make one for the difference
                                if (PositionDifference != 0)
                                {

                                  //  string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);

                                    NormalOrder("Sell", Convert.ToInt32(nudQty.Value));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) (PositionDifference != 0) Sell NormalOrder");
                                }

                            }

                        }
                        else
                        {
                            if (OpenOrders.Any())
                            {
                                // If we have an open order, edit it
                                if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // We still have an open Sell order, cancel that order, make a new Buy order
                                    string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Sell", Convert.ToInt32(nudQty.Value));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) (OpenOrders.Any()) Sell NormalOrder");

                                }
                                else if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Buy", Convert.ToInt32(nudQty.Value));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) (OpenOrders.Any()) Buy NormalOrder");
                                }
                            }
                            else
                            {
                                // string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                //  NormalOrder("Sell", Convert.ToInt32(nudQty.Value));
                                //      string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                // FirstOrder("Sell", Convert.ToInt32(nudQty.Value));
                                int Qty = 1;
                                String Side = "Sell";
                                bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                                log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) (OpenOrders.Any()) Sell Else  FirstOrder Sell ");
                            }
                        }
                        break;
                    case "Waitelse":
                        currentCandle = Candles.Any() ? Candles[0] : null;
                        if (OpenPositions.Any())
                        {
                            // Now, do we have open orders?  If so, we want to make sure they are at the right price
                            if (OpenOrders.Any())
                            {
                                if ((OpenOrders.Any(a => a.Side == "Buy")) && (currentCandle.STOCHK < InputBuySTOCHK))
                                {
                                    // We still have an open buy order, cancel that order, make a new sell order
                                    string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    int Qty = 1;
                                    String Side = "Sell";
                                    bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                                    //FirstOrder("Sell", Convert.ToInt32(nudQty.Value));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Wait (OpenOrders.Any()) Sell  FirstOrder");
                                }
                                else if ((OpenOrders.Any(a => a.Side == "Sell")) && (currentCandle.STOCHK < InputSellSTOCHK))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    int Qty = 1;
                                    String Side = "Buy";
                                    bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Wait (OpenOrders.Any()) Buy  FirstOrder");
                                }

                            }
                            else if ((OpenPositions[0].CurrentQty < 2) && (currentCandle.STOCHK < InputSellSTOCHK))
                            {
                                // No open orders, need to make an order to sell
                                string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                int Qty = 1;
                                String Side = "Sell";
                                bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                                log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Wait (OpenPositions[0].CurrentQty < 2) Sell  FirstOrder");
                            }
                        }
                        else if (OpenOrders.Any())
                        {
                            // We don't have an open position, but we do have an open order, close that order, we don't want to open any position here.
                            //    string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                            log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Wait (OpenOrders.Any()) do Nothing SetBotMode();");
                        }
                        SetBotMode();
                        break;
                    case "CloseShortsAndWait":
                        // Close any open orders, close any open shorts, we've missed our chance to long.
                        if (OpenPositions.Any())
                        {
                            // Now, do we have open orders?  If so, we want to make sure they are at the right price
                            if (OpenOrders.Any())
                            {
                                if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // We still have an open sell order, cancel that order, make a new buy order
                                    //   string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Buy", Convert.ToInt32(OpenPositions[0].CurrentQty));
                                }
                                else if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    string result = bitmex.EditOrderPrice(OpenOrders[0].OrderId, CalculateMakerOrderPrice("Buy"));
                                }

                            }
                            else if (OpenPositions[0].CurrentQty < 0)
                            {
                                // No open orders, need to make an order to sell
                                NormalOrder("Buy", Convert.ToInt32(OpenPositions[0].CurrentQty));
                            }
                        }
                        break;


                }
            }
            */
            else if (OpenOrders.Any())
            {
                SetBotMode();
                // We don't have an open position, but we do have an open order, close that order, we don't want to open any position here.
                // string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            }
      }

            

        
    
        

        // Check account balance/validity
        private void GetAPIValidity()
        {
            try // Code is simple, if we get our account balance without an error the API is valid, if not, it will throw an error and API will be marked not valid.
            {
                
                WalletBalance = bitmex.GetAccountBalance();
                if (WalletBalance >= 0)
                {
                     APIValid = true;
                    stsAPIValid.Text = "API keys are valid";
                    stsAccountBalance.Text = "Balance: " + WalletBalance.ToString();
                }
                else
                {
                    APIValid = false;
                    stsAPIValid.Text = "API keys are invalid";
                    stsAccountBalance.Text = "Balance: 0";
                }
            }
            catch (Exception ex)
            {
                log.Error("API keys are invalid", ex);
                APIValid = false;
                stsAPIValid.Text = "API keys are invalid";
                stsAccountBalance.Text = "Balance: 0";
            }
        }

        // Update balances
        private void btnAccountBalance_Click(object sender, EventArgs e)
        {
            GetAPIValidity();
        }

        // Set Market Stops
        private void btnManualSetStop_Click(object sender, EventArgs e)
        {
            OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);

            if(OpenPositions.Any()) // Only set stops if we have open positions
            {
                // Now determine what kind of stop to set
                if(OpenPositions[0].CurrentQty > 0)
                {
                    double? candleClose = Candles.Any() ? Candles[0].Close : 0;
                    // Determine stop price, x percent below current price.
                    double PercentPriceDifference = Convert.ToDouble(candleClose) * (Convert.ToDouble(nudStopPercent.Value) / 100);
                    double StopPrice = Convert.ToDouble(candleClose) - PercentPriceDifference;
                    // Round the Stop Price down to the tick size so the price is valid
                    StopPrice = StopPrice - (StopPrice % ActiveInstrument.TickSize);
                    // Set a stop to sell
                    bitmex.MarketStop(ActiveInstrument.Symbol, "Sell", StopPrice, Convert.ToInt32(OpenPositions[0].CurrentQty), true, ddlCandleTimes.SelectedItem.ToString());
                }
                else if(OpenPositions[0].CurrentQty < 0)
                {
                    double? candleClose = Candles.Any() ? Candles[0].Close : 0;
                    // Determine stop price, x percent below current price.
                    double PercentPriceDifference = Convert.ToDouble(candleClose) * (Convert.ToDouble(nudStopPercent.Value) / 100);
                    double StopPrice = Convert.ToDouble(candleClose) + PercentPriceDifference;
                    // Round the Stop Price down to the tick size so the price is valid
                    StopPrice = StopPrice - (StopPrice % ActiveInstrument.TickSize);
                    // Set a stop to sell
                    bitmex.MarketStop(ActiveInstrument.Symbol, "Buy", StopPrice, (Convert.ToInt32(OpenPositions[0].CurrentQty) * -1), true, ddlCandleTimes.SelectedItem.ToString());
                }
            }
        }

        private void txtAPIKey_TextChanged(object sender, EventArgs e)
        {
            switch (ddlNetwork.SelectedItem.ToString())
            {
                case "TestNet":
                    Properties.Settings.Default.TestAPIKey = txtAPIKey.Text;
                    break;
                case "RealNet":
                    Properties.Settings.Default.APIKey = txtAPIKey.Text;
                    break;
            }
            SaveSettings();
            InitializeAPI();
        }

        private void txtAPISecret_TextChanged(object sender, EventArgs e)
        {
            switch (ddlNetwork.SelectedItem.ToString())
            {
                case "TestNet":
                    Properties.Settings.Default.TestAPISecret = txtAPISecret.Text;
                    break;
                case "RealNet":
                    Properties.Settings.Default.APISecret = txtAPISecret.Text;
                    break;
            }
            SaveSettings();
            InitializeAPI();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        // NEW - Over Time ordering
        private void UpdateOverTimeSummary()
        {
            OTContractsPer = Convert.ToInt32(nudOverTimeContracts.Value);
            OTIntervalSeconds = Convert.ToInt32(nudOverTimeInterval.Value);
            OTIntervalCount = Convert.ToInt32(nudOverTimeIntervalCount.Value);
        }

        private void nudOverTimeContracts_ValueChanged(object sender, EventArgs e)
        {
            UpdateOverTimeSummary();
        }

        private void nudOverTimeInterval_ValueChanged(object sender, EventArgs e)
        {
            UpdateOverTimeSummary();
        }

        private void nudOverTimeIntervalCount_ValueChanged(object sender, EventArgs e)
        {
            UpdateOverTimeSummary();
        }

        private void btnBuyOverTimeOrder_Click(object sender, EventArgs e)
        {
            UpdateOverTimeSummary(); // Makes sure our variables are current.

            OTSide = "Buy";

            tmrTradeOverTime.Interval = OTIntervalSeconds * 1000; // Must multiply by 1000, because timers operate in milliseconds.
            tmrTradeOverTime.Start(); // Start the timer.
            stsOTProgress.Value = 0;
            stsOTProgress.Visible = true;
        }

        private void btnSellOverTimeOrder_Click(object sender, EventArgs e)
        {
            UpdateOverTimeSummary(); // Makes sure our variables are current.

            OTSide = "Sell";

            tmrTradeOverTime.Interval = OTIntervalSeconds * 1000; // Must multiply by 1000, because timers operate in milliseconds.
            tmrTradeOverTime.Start(); // Start the timer.
            stsOTProgress.Value = 0;
            stsOTProgress.Visible = true;
        }

        private void tmrTradeOverTime_Tick(object sender, EventArgs e)
        {
            OTTimerCount++;
            bitmex.MarketOrder(ActiveInstrument.Symbol, OTSide, OTContractsPer);

            double Percent = ((double)OTTimerCount / (double)OTIntervalCount) * 100;
            stsOTProgress.Value = Convert.ToInt32(Math.Round(Percent));

            if(OTTimerCount == OTIntervalCount)
            {
                OTTimerCount = 0;
                tmrTradeOverTime.Stop();
                stsOTProgress.Value = 0;
                stsOTProgress.Visible = false;
                
            }
        }

        private void btnOverTimeStop_Click(object sender, EventArgs e)
        {
            OTTimerCount = 0;
            stsOTProgress.Value = 0;
            stsOTProgress.Visible = false;
            tmrTradeOverTime.Stop();
        }

        //CHANGE 2: Price set from UI
        private void btnSetPrice_Click(object sender, EventArgs e)
        {
            InitializeParameterSettings();
        }

        //Berechnung der Preis für Buy
        private double CalculateBuyPrice(List<OrderBook> orderBooks)
        {
            //Aus der OrderBook nehme ich Buy Orders und sortiere ich sie (erstes element => das mit dem höchsten Preis)
            CurrentBookBuy = orderBooks.Where(item => item.Side == "Buy").OrderByDescending(item => item.Price).ToList();
            // addieren die erste *ElementsToTake* (5) elementen
            double sumFirstItems = CurrentBookBuy.Take(BuyElementsToTake).Sum(item => item.Size);
            SumBuyFirstItems = sumFirstItems;
            // addiere der Preis von allen "Buy" Elementen
            double summAllBuyItems = CurrentBookBuy.Sum(item => item.Price);
            // grenze herausfinden aus DIVISION Total Buy Preis durch *PriceDividend* (15)
            double sizeLimit = summAllBuyItems / PriceBuyDividend;
       //     log.InfoFormat("CalculateBuyPrice - SumSellFirstItems der ersten {0} OrderBooks: {1}, sizeLimit: {2}", BuyElementsToTake, SumSellFirstItems, sizeLimit);
            RefreshDataUi("Buy", sizeLimit, CurrentBookBuy.Any()? CurrentBookBuy[0].Size : 0);
            //Price durch die Grenze festlegen in Buy Mode
            return SelectPrice(CurrentBookBuy, sizeLimit, "Buy");
        }

        //Berechnung der Preis für Sell
        private double CalculateSellPrice(List<OrderBook> orderBooks)
        {
            //Aus der OrderBook nehme ich Sell Orders und sortiere ich sie (erstes element => das mit dem niedrigsten Preis)
            CurrentBookSell = orderBooks.Where(item => item.Side == "Sell").OrderBy(item => item.Price).ToList();
            // addieren die erste *ElementsToTake* (5) elementen
            double sumFirstItems = CurrentBookSell.Take(SellElementsToTake).Sum(item => item.Size);
            SumSellFirstItems = sumFirstItems;
            // addiere der Preis von allen "Sell" Elementen
            double summAllSellItems = CurrentBookSell.Sum(item => item.Price);
            // grenze herausfinden aus DIVISION Total Buy Preis durch *PriceDividend* (15)
            double sizeLimit = summAllSellItems / PriceSellDividend;

          //  log.InfoFormat("CalculatedSellPrice - SumSellFirstItems der ersten {0} OrderBooks: {1}, sizeLimit: {2}", SellElementsToTake, SumSellFirstItems, sizeLimit);
            RefreshDataUi("Sell", sizeLimit, CurrentBookSell.Any() ? CurrentBookSell[0].Size : 0);
            //Price durch die Grenze festlegen in Sell Mode
            return SelectPrice(CurrentBookSell, sizeLimit, "Sell"); 
        }

        private double SelectPrice(List<OrderBook> orderBooks, double sizeLimit, string side)
        {
            //Note: in Sell mode die Parameter Liste orderBooks kommt rein sortiert von klein zu groß
            //Note: in Buy mode der Parameter Liste orderBooks kommt rein sortiert von groß zu klein
            double sum = 0;
            OrderBook lastOrderBook = null;
            OrderBook currentOrderBook = null;

            foreach (var orderBook in orderBooks)
            {   
                currentOrderBook = orderBook;

                sum += orderBook.Size;
            //    log.InfoFormat("SelectPrice - orderBook.Size: {0}, sum: {1}, sizeLimit: {2}", orderBook.Size, sum, sizeLimit);
                if (sum > sizeLimit)
                {
                    break;
                }
                lastOrderBook = orderBook;
            }

            if (("Sell".Equals(side) && (lastOrderBook == null || lastOrderBook.Price < 0)))
            {
                return orderBooks.Any() ? orderBooks[0].Price : default(double);
            }
            if ("Buy".Equals(side) && (currentOrderBook == null || currentOrderBook.Price < 0))
            {
                return orderBooks.Any() ? orderBooks[0].Price : default(double);
            }
            //in Sell mode nehme ich den sofort unter der Grenze
            //in Buy mode nehme ich den sofort über der Grenze
            if ("Sell".Equals(side))
            {
                return lastOrderBook != null ? lastOrderBook.Price : 0;
            }
            //hier sind in buy
            return currentOrderBook != null ? currentOrderBook.Price : 0;
        }

        private void RefreshDataUi(string side, double sizeLimit, double sizeFirstOrder)
        {
            if ("Buy".Equals(side))
            {
                this.txtBuyElementsDivisionOutput.Text = sizeLimit.ToString();
                this.txtFirstBuyOrderOutput.Text = sizeFirstOrder.ToString();
            }
            else if ("Sell".Equals(side))
            {
                this.txtSellElementsDivisionOutput.Text = sizeLimit.ToString();
                this.txtFirstSellOrderOutput.Text = sizeFirstOrder.ToString();
            }

        }

        private void nudSellStochk_ValueChanged(object sender, EventArgs e)
        {
            InputSellSTOCHK = Convert.ToDouble(nudSellStochk.Value);
        }

        private void nudBuyStochk_ValueChanged(object sender, EventArgs e)
        {
            InputBuySTOCHK = Convert.ToDouble(nudBuyStochk.Value);
        }

        private void dgvCandles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridCellFormating(e);
        }

        private void tmrUpdateBBMiddle_Tick(object sender, EventArgs e)
        {
            AllowCalculateBBMiddle = true;
        }

        private void tmrUpdateBuySellFirstPriceOrders_Tick(object sender, EventArgs e)
        {
            //   //every second these values are going to be updated
            CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);
            CalculateBuyPrice(CurrentBook);
            CalculateSellPrice(CurrentBook);
            RefreshGridOpenOrdersAndPositions();
        }

        private void RefreshGridOpenOrdersAndPositions()
        {
            this.dgvOpenOrders.DataSource = OpenOrders.Take(CONST_MAX_TOTAL_TAKE_SHOW_ORDERS).ToList();
            this.dgvOpenPositions.DataSource = OpenPositions.Take(CONST_MAX_TOTAL_TAKE_SHOW_POSITIONS).ToList();
       }

        private void readCoins()
        {
            using (StreamReader r = new StreamReader("json/coins.json"))
            {
                string json = r.ReadToEnd();
                Coin fileCoin = new Coin(json);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TextBoxAppender.ConfigureTextBoxAppender(LoggingTextBox);
        }

        private void dgvOpenOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridCellFormating(e);
        }

        private void dgvOpenPositions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridCellFormating(e);
        }

        private void DataGridCellFormating(DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.BackColor = Color.Black;
            e.CellStyle.ForeColor = Color.Yellow;
        }
    }
}
