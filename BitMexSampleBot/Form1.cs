using BitMEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitMexSampleBot.tradesking;
using log4net;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using BitMexSampleBot.Util;

namespace BitMexSampleBot
{
    public partial class Form1 : Form
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Constants

        private const int CONST_ORDER_BOOK_DEPTH = 50;
        private const int CONST_MAX_TOTAL_TAKE_SHOW_ORDERS = 8;
        private const int CONST_MAX_TOTAL_TAKE_SHOW_POSITIONS = 8;
        private const string CONST_URL_TRADES_KING_JSON = "http://www.trades-king.com/coins.json"; 
        #endregion

        // IMPORTANT - Enter your API Key information below

        //TEST NET - NEW
        //     private static string TestbitmexKey = "";
        //     private static string TestbitmexSecret = "";
        private static string TestbitmexDomain = "https://testnet.bitmex.com";

        //REAL NET        
        //  private static string bitmexKey = "E0P-FzC-yZD3DtxCdvrtL_Nh";
        //   private static string bitmexSecret = "aHpdfNAegp1Xepg-rHHbE1Pg3A8ra7QbZsw5e__05nEaae1a";

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

		private bool RUNBOT = false;

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
 

        //   double? SellPumpPrice { get; set; }
        //   double? BuyDumpPrice { get; set; }

        public bool AllowCalculateOrder { get; set; }
        public double? percent { get; set; }
    //    public double? BuyPriceOld  { get; set; }
      //  public double? SellPriceOld { get; set; }
        public double? BuyDumpPriceOld { get; set; }
        public double? SellPumpPriceOld { get; set; }
        public double? OrderPrice { get; set; }
        public int? PositionDifference { get; set; }
        public double? STOCHK { get; set; }
        public double Trend1h { get; set; }
        public double Trend24h { get; set; }
        public double Trendallover { get; set; }
        public double InputDump { get; set; }
        public double InputPump { get; set; }
        public double BalanceExitNew { get; set; }
        public double? AvgEntryPriceBuyPrice { get; set; }
        public double? AvgEntryPriceSellPrice { get; set; }
        public double? OldBuyPriceMakerOrder { get; set; }
        public double? OldSellPriceMakerOrder { get; set; }
        public double? OldBuyPriceNormalOrder { get; set; }
        public double? OldSellPriceNormalOrder { get; set; }
        public double? OldBuyPriceCalcTakeProfit { get; set; }
        public double? OldSellPriceCalcTakeProfit { get; set; }
        public double? OldBuyPriceCalcFirstOrder { get; set; }
        public double? OldSellPriceCalcFirstOrder { get; set; }
        public double? AvailableMargin { get; set; }




        #endregion

        public Form1()
        {
			using (LoginForm login = new LoginForm())
			{
				if(login.ShowDialog() == DialogResult.OK)
				{
                    RUNBOT = true;
                }
            }

            InitializeComponent();
            InitializeDropdownsAndSettings();
            InitializeAPI();
            InitializeCandleArea();
            InitializeOverTime();
            InitializeBuySellStochk();
            InitializeParameterSettings();
            InitializeColors();
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
            ddlNetwork.SelectedIndex = 1;
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
            InputDump = decimal.ToDouble(DumpInput.Value);
            InputPump = decimal.ToDouble(PumpInput.Value);
            BalanceExitNew = decimal.ToDouble(BalanceExit.Value);
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
            {
                AllowCalculateBBMiddle = false;
                //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());
            }
            //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());
         
            double BBLower = Convert.ToDouble(Candles.Any() && Candles[0].BBLower.HasValue ? Candles[0].BBLower.Value : 0d);
            double BBUpper = Convert.ToDouble(Candles.Any() && Candles[0].BBUpper.HasValue ? Candles[0].BBUpper.Value : 0d);
            double? BuyDumpPrice = 0d;
            double? SellPumpPrice = 0d;
            SellPumpPrice = BBLower + InputPump;
            BuyDumpPrice = BBLower - InputDump;
            Candle currentCandle = Candles.Any() ? Candles[0] : null;
            CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);
            OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
            double? BuyPrice = CalculateBuyPrice(CurrentBook);
            double? SellPrice = CalculateSellPrice(CurrentBook);
            BuyPrice = BuyPrice - 2;
            SellPrice = SellPrice + 1;
            double? AvgEntryPrice = OpenPositions[0].AvgEntryPrice; // ?? BuyPrice;
            log.InfoFormat("CalculateMakerOrderPrice - SellPrice: {0}", SellPrice);
            
            log.InfoFormat("CalculateMakerOrderPrice - BuyPrice: {0}", BuyPrice);
            //lblAutoUnrealizedROEPercent.Text = Math.Round((Convert.ToDouble(OpenPositions[0].UnrealisedRoePcnt * 100)), 2).ToString();

            switch (Side)
                {
                    case "Buy":
                        if ((BuyPrice == OldBuyPriceMakerOrder) && (OpenOrders.Any()))
                        {
                            OrderPrice = BuyPrice;
                            OldBuyPriceMakerOrder = OrderPrice;
                        return OrderPrice != null ? (long)OrderPrice : 0;
                    }
                    else if ((OpenPositions.Count > 0) && (currentCandle != null) && (currentCandle.STOCHK <= InputBuySTOCHK))
                    {
                        

                        if  (currentCandle.STOCHK <= InputBuySTOCHK)
                        {
                            if (BuyPrice < AvgEntryPrice)
                            {
                                OrderPrice = BuyPrice;
                                OldBuyPriceMakerOrder = OrderPrice;
                            }
                            else
                            {
                                OrderPrice = BuyDumpPrice;
                                OldBuyPriceMakerOrder = OrderPrice;
                            }
                        }
                        else if ((Trendallover < BuyPrice) & (Trendallover < AvgEntryPrice) & (currentCandle.STOCHK <= InputBuySTOCHK))
                        {
                            //BuyPrice = Convert.ToDouble(OpenPositions[0].AvgEntryPrice ?? BuyPrice);
                            OrderPrice = Trendallover;
                            OldBuyPriceMakerOrder = OrderPrice;
                        }
                        else if ((SumBuyFirstItems > SumSellFirstItems) & (currentCandle.STOCHK >= InputSellSTOCHK))
                        {
                            OrderPrice = BuyPrice;
                            OldBuyPriceMakerOrder = OrderPrice;

                        }
                        log.InfoFormat("CalculateMakerOrderPrice - SellPrice: {0}, AvgEntryPrice:{1} => OrderPrice: {2}", BuyPrice, Trendallover, BuyDumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                    }

                    btnAutomatedTrading.Text = "Stop - " + Mode;
                   

                    break;

                case "Sell":

                    if (OpenPositions.Count > 0) //& (currentCandle.STOCHK >= InputSellSTOCHK))
                        {
                            if ((SellPrice == OldSellPriceMakerOrder) && (OpenOrders.Any()))
                            {
                                OrderPrice = SellPrice;
                                OldSellPriceMakerOrder = OrderPrice;
                            return OrderPrice != null ? (long)OrderPrice : 0;
                        }
                            else if ((SellPrice < AvgEntryPriceSellPrice) && (AllowCalculateBBMiddle == true)) // & (currentCandle.STOCHK >= InputSellSTOCHK))
                            {
                                if (SellPrice > AvgEntryPriceSellPrice)
                                {
                                    OrderPrice = SellPrice;
                                    OldSellPriceMakerOrder = OrderPrice;
                                }
                                else if (Trendallover > SellPrice)
                                {
                                    OrderPrice = SellPrice;
                                    OldSellPriceMakerOrder = OrderPrice;
                                }
                            }
                            else
                            {
                                OrderPrice = SellPrice;
                                OldSellPriceMakerOrder = OrderPrice;
                            }
                        }
                        else
                        {
                            OrderPrice = SellPrice;
                            OldSellPriceMakerOrder = OrderPrice;
                            //  AllowCalculateBBMiddle = false;
                        }
                        log.InfoFormat("CalculateMakerOrderPrice - SellPrice: {0}, Trendallover:{1} => OrderPrice: {2}, OldSellPriceMakerOrder: {3}", SellPrice, Trendallover, OrderPrice, OldSellPriceMakerOrder); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                
                        btnAutomatedTrading.Text = "Stop - " + Mode;
                      
                       
                        break;
                }

            AllowCalculateOrder = true;
            return OrderPrice != null ? (long)OrderPrice : 0;
        }

        public double CalcNormalOrder(string Side)
            {
                double? OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;

            //every 10 min AllowCalculateBBMiddle is true and then  BuyPumpPrice and BuyDumpPrice are recalculated
            if (AllowCalculateOrder == true)
            {



                if (AllowCalculateBBMiddle)
                {
                    AllowCalculateBBMiddle = false;
                    //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());

                }
              
                
                double BBLower = Convert.ToDouble(Candles.Any() && Candles[0].BBLower.HasValue ? Candles[0].BBLower.Value : 0d);
                double BBUpper = Convert.ToDouble(Candles.Any() && Candles[0].BBUpper.HasValue ? Candles[0].BBUpper.Value : 0d);
                double? BuyDumpPrice = 0d;
                double? SellPumpPrice = 0d;
                SellPumpPrice = BBLower + InputPump;
                BuyDumpPrice = BBLower - InputDump;
                Candle currentCandle = Candles.Any() ? Candles[0] : null;
                CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);
                OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
                double? SellPrice = CalculateSellPrice(CurrentBook);
                SellPrice = SellPrice + 10;
                log.InfoFormat("CalcNormalOrder - SellPrice: {0}", SellPrice);
                double? BuyPrice = CalculateBuyPrice(CurrentBook);
                if (OpenPositions.Any())
               
                //    double? AvgEntryPrice = OpenPositions[0].AvgEntryPrice ?? BuyPrice;
                //immer +1 usd addieren
                  BuyPrice = BuyPrice - 10;


                log.InfoFormat("CalcNormalOrder - BuyPrice: {0}", BuyPrice);
                //lblAutoUnrealizedROEPercent.Text = Math.Round((Convert.ToDouble(OpenPositions[0].UnrealisedRoePcnt * 100)), 2).ToString();

                double InputAutoWin = Convert.ToDouble(nudAutoMarketTakeProfitPercent.Value);


                switch (Side)

                {
                    case "Buy":

                        
                        if ((BuyPrice == OldBuyPriceNormalOrder) && (OpenOrders.Any()) & (percent >= InputAutoWin))


                        {

                            OrderPrice = 0;
                            OldBuyPriceNormalOrder = OrderPrice;

                            return OrderPrice != null ? (long)OrderPrice : 0;

                        }
                        if ((OpenPositions.Count > 0) && (currentCandle != null) && (currentCandle.STOCHK <= InputBuySTOCHK) & (Trend1h > OldBBMiddle) & (Trend1h > AvgEntryPriceBuyPrice)& (percent >= InputAutoWin))
                        {
                            AvgEntryPriceBuyPrice = OpenPositions[0].AvgEntryPrice ?? BuyPrice;

                            if ((BuyPrice < BuyDumpPrice) & (AllowCalculateBBMiddle == false) & (currentCandle.STOCHK <= InputBuySTOCHK))
                            {
                                if (BuyPrice < AvgEntryPriceBuyPrice)
                                {
                                    OrderPrice = BuyPrice;
                                    OldBuyPriceNormalOrder = OrderPrice;
                                }
                                else if ((Trendallover < BuyDumpPrice) & (Trendallover < AvgEntryPriceBuyPrice))
                                {
                                    OrderPrice = Trendallover;
                                    OldBuyPriceNormalOrder = OrderPrice;
                                }
                                else if ((BuyDumpPrice < AvgEntryPriceBuyPrice) & (Trendallover < AvgEntryPriceBuyPrice))
                                {
                                    OrderPrice = BuyDumpPrice;
                                    OldBuyPriceNormalOrder = OrderPrice;
                                }

                                else if (((Trendallover > BuyPrice) & (currentCandle.STOCHK <= InputBuySTOCHK) & (Trendallover > AvgEntryPriceBuyPrice)))
                                {
                                    //BuyPrice = Convert.ToDouble(OpenPositions[0].AvgEntryPrice ?? BuyPrice);
                                    OrderPrice = Trendallover;
                                    OldBuyPriceNormalOrder = OrderPrice;
                                }
                                else if (((SumBuyFirstItems > SumSellFirstItems) & (currentCandle.STOCHK >= InputSellSTOCHK) & (Trendallover > AvgEntryPriceBuyPrice)))
                                {
                                    OrderPrice = Trendallover;
                                    OldBuyPriceNormalOrder = OrderPrice;
                                    // OrderPrice = BuyDumpPrice;

                                }
                            

                        }
                        }
                        else
                        {
                            OrderPrice = BuyDumpPrice;
                            OldBuyPriceNormalOrder = OrderPrice;



                        }
                        log.InfoFormat("CalcNormalOrder - BuyPrice: {0}, Trendallover:{1}, BuyDumpPrice: {2}, => OrderPrice: {3}", BuyPrice, Trendallover, BuyDumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                        OrderPrice = BuyPrice;
                        Mode = "Normal Buy";
                        btnAutomatedTrading.Text = "Stop - " + Mode;
                        break;

                    case "Sell":

                        if ((SellPrice == OldSellPriceNormalOrder) && (OpenOrders.Any()) & (percent >= InputAutoWin))
                        {
                            OrderPrice = 0;
                            OldSellPriceNormalOrder = OrderPrice;
                            return OrderPrice != null ? (long)OrderPrice : 0;
                        }
                       

                        if ((OpenPositions.Count > 0) & (currentCandle.STOCHK >= InputSellSTOCHK) & (Trendallover > AvgEntryPriceSellPrice)& (percent >= InputAutoWin))
                        {
                        

                           if ((SellPrice < SellPumpPrice) && (AllowCalculateBBMiddle == true) & (currentCandle.STOCHK >= InputSellSTOCHK) & (percent >= InputAutoWin))
                           {
                              if (SellPrice > AvgEntryPriceSellPrice) 
                            {
                                OrderPrice = SellPrice;
                                OldSellPriceNormalOrder = OrderPrice;
                                }
                              else if ((Trendallover > SellPrice) & (Trendallover > AvgEntryPriceSellPrice))
                            {
                                OrderPrice = Trendallover;
                                    OldSellPriceNormalOrder = OrderPrice;
                                }
                           }
                        }
                           else if ((Trendallover > SellPumpPrice) & (Trendallover > AvgEntryPriceSellPrice) & (percent >= InputAutoWin))
                        {
                            OrderPrice = Trendallover;
                            OldSellPriceNormalOrder = OrderPrice;
                        }
                        else if ((Trendallover > SellPrice) & (SellPrice> AvgEntryPriceSellPrice) & (percent >= InputAutoWin))
                        {
                            OrderPrice = SellPrice;
                            OldSellPriceNormalOrder = OrderPrice;
                        }
                         
                       else
                       {
                        OrderPrice = SellPumpPrice;
                            OldSellPriceNormalOrder = OrderPrice;



                        }

                          log.InfoFormat("CalcNormalOrder - SellPrice: {0},Trendallover:{1}, SellPumpPrice: {2}, => OrderPrice: {3}", SellPrice, Trendallover, SellPumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                          Mode = "Normal Sell";
                          btnAutomatedTrading.Text = "Stop - " + Mode;
                        
                   
                    break;
                }


            AllowCalculateOrder = true;
            return OrderPrice != null ? (long)OrderPrice : 0;
        }
            else
            {
                return OrderPrice != null ? (long)OrderPrice : 0;
            }
        }
        public double CalcTakeProfit(string Side)
        {
            double? OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
            if (AllowCalculateOrder == true)
            {
                AllowCalculateOrder = false;

            }

            //every 10 min AllowCalculateBBMiddle is true and then  BuyPumpPrice and BuyDumpPrice are recalculated
           
            Candle currentCandle = Candles.Any() ? Candles[0] : null;
            CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);

            double? SellPrice = CalculateSellPrice(CurrentBook);
            SellPrice = SellPrice + 10;
            log.InfoFormat("CalcTakeProfit - SellPrice: {0}", SellPrice);
            double? BuyPrice = CalculateBuyPrice(CurrentBook);
            //immer +1 usd addieren
            BuyPrice = BuyPrice - 10;
            log.InfoFormat("CalcTakeProfit - BuyPrice: {0}", BuyPrice);


            //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());&& (currentCandle.STOCHK > InputSellSTOCHK)
            Candles = Candles.OrderBy(a => a.TimeStamp).ToList();
            double? averageEntryPrice = OpenPositions[0].AvgEntryPrice;
            double Open = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
            double InputAutoWin = Convert.ToDouble(nudAutoMarketTakeProfitPercent.Value);
           



            switch (Side)
            {
                case "Buy":
                    if (averageEntryPrice > BuyPrice) //& (averageEntryPrice > Trend1h ))
                    {
                        OrderPrice = BuyPrice;

                        
                        log.InfoFormat("CalcTakeProfit - Buy((OpenPositions.Count < 0) & (OpenOrders.Count < 1)), BuyPrice: {0}, OrderPrice: {1}", BuyPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                        Mode = "CalcTakeProfit Buy";
                        btnAutomatedTrading.Text = "Stop - " + Mode;
                    }
                    else
                    {
                        OrderPrice = BuyPrice;
                    }
                    break;
                case "Sell":
                    if ((SellPrice > averageEntryPrice) & (Trend1h < averageEntryPrice))
                    {
                        
                        //  bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                        if ((OpenPositions.Count < 2) & (OpenOrders.Count < 2) & (SumBuyFirstItems < SumSellFirstItems) & (currentCandle.STOCHK < InputBuySTOCHK & (Trend1h < OldBBMiddle) & (percent >= InputAutoWin) || (Trend1h <= averageEntryPrice) || (Trend24h <= averageEntryPrice) || (percent >= -10)))
                        {
                            
                                    OrderPrice = SellPrice;
                                
                        }
                        else
                        {
                            OrderPrice = SellPrice;
                        }
                        log.InfoFormat("CalcTakeProfit - SellPrice: {0}, OrderPrice: {1}", SellPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                        Mode = "CalcTakeProfit Sell";
                        btnAutomatedTrading.Text = "Stop - " + Mode;
                       
                    }
                    else
                    {
                        OrderPrice = SellPrice;
                    }
                    break;
            }
            AllowCalculateOrder = true;

            return OrderPrice != null ? (long)OrderPrice : 0;
        }
        public double CalcFirstOrder(string Side)
        {
            double BBLower = Convert.ToDouble(Candles.Any() && Candles[0].BBLower.HasValue ? Candles[0].BBLower.Value : 0d);
            double BBUpper = Convert.ToDouble(Candles.Any() && Candles[0].BBUpper.HasValue ? Candles[0].BBUpper.Value : 0d);
            double? BuyDumpPrice = 0d;
            double? SellPumpPrice = 0d;
            SellPumpPrice = BBLower + InputPump;
            BuyDumpPrice = BBLower - InputDump;
            double? OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
            AllowCalculateOrder = true;
          
            
            Candle currentCandle = Candles.Any() ? Candles[0] : null;
            //every 10 min AllowCalculateBBMiddle is true and then  BuyPumpPrice and BuyDumpPrice are recalculated
            if (AllowCalculateBBMiddle)
            {
                AllowCalculateBBMiddle = false;
                //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());

               
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

 
            double Open = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
            bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
            switch (Side)

            {
                case "Buy":

                    if ((BuyPrice == OldBuyPriceCalcFirstOrder) && (OpenOrders.Any()))


                    {

                        OrderPrice = 0;
                        OldBuyPriceCalcFirstOrder = OrderPrice;
                        return OrderPrice != null ? (long)OrderPrice : 0;

                    }
                    else if (((currentCandle.STOCHK <= InputBuySTOCHK) & (Trend1h > OldBBMiddle)))
                    {

                        if (Open < BuyPrice)
                        {
                            if ((BuyDumpPrice < Open) & (AllowCalculateBBMiddle == false))
                            {
                                OrderPrice = BuyDumpPrice;
                                OldBuyPriceCalcFirstOrder = OrderPrice;
                            }
                            else
                            {
                                OrderPrice = BuyPrice + 1;
                                OldBuyPriceCalcFirstOrder = OrderPrice;
                            }
                        }
                        else if (Open < BuyDumpPrice)
                        {

                            OrderPrice = Open;
                            OldBuyPriceCalcFirstOrder = OrderPrice;
                        }
                        else if (Trendallover < BuyDumpPrice)
                        {
                            OrderPrice = Trendallover;
                            OldBuyPriceCalcFirstOrder = OrderPrice;
                        }
                    }
                    else
                    {
                        OrderPrice = BuyPrice + 1;
                        OldBuyPriceCalcFirstOrder = OrderPrice;
                    }

                    log.InfoFormat("CalcFirstOrder - BuyPrice: {0}, BuyDumpPrice: {1}, Open:{2}, OrderPrice: {3}", BuyPrice, BuyDumpPrice, Open, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)

                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    
                    // BuyDumpPriceOld = BuyDumpPrice;
                    break;



                case "Sell":

                    if ((SellPrice == OldSellPriceCalcFirstOrder) && (OpenOrders.Any()))
                    {
                        OrderPrice = 0;
                        OldSellPriceCalcFirstOrder = OrderPrice;
                        return OrderPrice != null ? (long)OrderPrice : 0;
                    }


                    else if ( (currentCandle.STOCHK >= InputSellSTOCHK) & (Trend1h > OldBBMiddle))
                    {
                        if ((Open > SellPrice) && (AllowCalculateBBMiddle == false))
                        {
                            if (SellPumpPrice > Open)
                            {
                                OrderPrice = SellPumpPrice;
                                OldSellPriceCalcFirstOrder = OrderPrice;

                            }
                            else
                            {
                                OrderPrice = Open;
                                OldSellPriceCalcFirstOrder = OrderPrice;

                            }
                        }
                        else if (Open > SellPrice)
                        {
                            OrderPrice = Open;
                            OldSellPriceCalcFirstOrder = OrderPrice;
                        }
                        else
                        {
                            OrderPrice = SellPrice;
                            OldSellPriceCalcFirstOrder = OrderPrice;
                        }
                    }
                    else if (SellPrice > SellPumpPrice)
                    {
                        OrderPrice = SellPrice;
                        OldSellPriceCalcFirstOrder = OrderPrice;
                    }
                    else if (Trendallover > SellPumpPrice)
                    {
                        OrderPrice = Trendallover;
                        OldSellPriceCalcFirstOrder = OrderPrice;
                    }

                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    log.InfoFormat("CalcFirstOrder - SellPrice: {0}, SellPumpPrice: {1}, Open:{2}, OrderPrice: {3}", SellPrice, SellPumpPrice, Open, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
                  
                    //SellPumpPriceOld = SellPumpPrice;
                    break;
            }
            AllowCalculateOrder = true;
            return OrderPrice != null ? (long)OrderPrice : 0;
        }

        public double CalcDumpandPump(string Side)
        {
            double? OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
            double BBLower = Convert.ToDouble (Candles.Any() && Candles[0].BBLower.HasValue ? Candles[0].BBLower.Value : 0d);
            double BBUpper = Convert.ToDouble(Candles.Any() && Candles[0].BBUpper.HasValue ? Candles[0].BBUpper.Value : 0d);
            double? BuyDumpPrice = 0d;
            double? SellPumpPrice = 0d;

            //  Thread.Sleep(10000);
            //every 10 min AllowCalculateBBMiddle is true and then  BuyPumpPrice and BuyDumpPrice are recalculated
            if (AllowCalculateOrder == true)
            {
                AllowCalculateOrder = false;
            }   
                Candle currentCandle = Candles.Any() ? Candles[0] : null;
                CurrentBook = bitmex.GetOrderBook(ActiveInstrument.Symbol, CONST_ORDER_BOOK_DEPTH);
                OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
                double? SellPrice = CalculateSellPrice(CurrentBook);

                log.InfoFormat("CalcDumpandPump - SellPrice: {0}", SellPrice);
                double? BuyPrice = CalculateBuyPrice(CurrentBook);
                //immer +1 usd addieren

                double? OldBuyPrice = BuyPrice;
                double? OldSellPrice = SellPrice;

                if (AllowCalculateBBMiddle)
                {
                    AllowCalculateBBMiddle = false;
                    //Candles = bitmex.GetCandleHistory(ActiveInstrument.Symbol, 500, ddlCandleTimes.SelectedItem.ToString());
                   
                }
                BuyDumpPrice = BBLower + InputDump;
                SellPumpPrice = BBUpper + InputPump;
            switch (Side)
            {
                case "Buy":


                    if ((OpenPositions.Count < 6) & (OpenOrders.Count < 6) & (currentCandle.STOCHK <= InputBuySTOCHK) & (currentCandle.STOCHK >= InputSellSTOCHK) & (Trendallover > OldBBMiddle))
                    {
                        // bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);

                        if (BuyPrice < BuyDumpPrice)
                        {

                            OrderPrice = BuyPrice;
                            BuyDumpPriceOld = OrderPrice;
                        }
                        else if (Trendallover < BuyDumpPrice)
                        {
                            OrderPrice = Trendallover;
                            BuyDumpPriceOld = OrderPrice;
                        }
                        else
                        {
                           
                            OrderPrice = BuyDumpPrice;
                            BuyDumpPriceOld = OrderPrice;
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

                                
                                OrderPrice = BuyDumpPrice;
                                BuyDumpPriceOld = OrderPrice;
                            }


                        }
                        OrderPrice = BuyDumpPrice;
                        BuyDumpPriceOld = OrderPrice;
                        
                    }


                    Mode = "Dump";

                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    log.InfoFormat("CalcDumpandPump - BuyPrice: {0}, Trendallover: {1}, BuyDumpPrice: {2}, OrderPrice: {3}", BuyPrice, Trendallover, BuyDumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)

                    break;

                case "Sell":



                    if (((OpenPositions.Count < 6) & (OpenOrders.Count < 6) & (currentCandle.STOCHK >= InputSellSTOCHK) & (Trendallover < OldBBMiddle)))
                    {


                        if (SellPrice > SellPumpPrice)
                        {

                            OrderPrice = SellPrice;
                            SellPumpPriceOld = OrderPrice;
                        }
                        else if (Trendallover > SellPumpPrice)
                        {
                            OrderPrice = Trendallover;
                            SellPumpPriceOld = OrderPrice;
                        }
                        else
                        {
                           
                            OrderPrice = SellPumpPrice;
                            SellPumpPriceOld = OrderPrice;

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
                           
                            OrderPrice = SellPumpPrice;
                            SellPumpPriceOld = OrderPrice;
                        }

                    }
                  
                    Mode = "Pump";
                    btnAutomatedTrading.Text = "Stop - " + Mode;
                    log.InfoFormat("CalcDumpandPump - SellPrice: {0}, Trendallover: {1} => SellDumpPrice: {2}, OrderPrice: {3}", SellPrice, Trendallover, SellPumpPrice, OrderPrice); //TODO: better check for null values in BreakEvenPrice-Setter method (bitmexApi)
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
            switch (ddlOrderType.SelectedItem)
            {
                case "Limit Post Only":
                    if (Price == 0)
                    {
                        Price = CalculateMakerOrderPrice(Side);
                    }
                    var MakerBuy = bitmex.PostOrderPostOnly(ActiveInstrument.Symbol, Side, Price, Qty);
                    break;
                case "Market":
                    bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
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
                     STOCHK = c.STOCHK;
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
                                 }*/
              

                double BalanceNew =Convert.ToDouble(WalletBalance);
                double BalanceExitNew = decimal.ToDouble(BalanceExit.Value); 
                if (BalanceNew < BalanceExitNew)
                {
                
                    OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
                        

                    if (OpenPositions.Any() && OpenPositions[0].CurrentQty < 0)
                    {
                        //       bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                        //TODO CurrentQty ?????
                         String Side = "Buy";
                        int Qty=  Convert.ToInt32(OpenPositions[0].CurrentQty);
                        AllowCalculateOrder = true;
                         bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                       // MakeOrder("Buy", Convert.ToInt32(PositionDifference));
                        log.InfoFormat("10 sec Update Kill all buy safe money");


                    }
                    if (OpenPositions.Any() && OpenPositions[0].CurrentQty > 0)
                    {
                       
                        String Side = "Sell";
                       int Qty= Convert.ToInt32(OpenPositions[0].CurrentQty * -1);
                        AllowCalculateOrder = true;
                        bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                       // MakeOrder("Sell", Convert.ToInt32(PositionDifference));
                        log.InfoFormat("10 sec Update Kill all sell safe money");
                        // Get our positions and orders again to be able to process rest of logic with new information.
                        OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
                        //  OpenOrders = bitmex.GetOpenOrders(ActiveInstrument.Symbol);

                    }
                    else
                    {
                        bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                        //wenn es kein openorders gibt , dann stopp
                        log.InfoFormat("Wallet minimum reached");
                        MessageBox.Show("Wallet minimum reached");
                        btnAutomatedTrading.Text = "Stop - " + Mode;
                        btnAutomatedTrading_Click(null, null);
                        //Application.Exit();
                        //Environment.Exit(0);
                        //this.Close();
                    }
                }
                else
                {
                            SetBotMode();
                }
            }
        }

        private void SetBotMode()
        {
            if ((OpenOrders.Count < 6) || (OpenOrders.Count > -6))
            {
                int OpenOrdersSum = Convert.ToInt32(OpenOrders.Sum(order => order.OrderQty ?? 0));
                int datainput = Convert.ToInt32(nudAutoQuantity.Value);
               
                if (OpenOrdersSum > datainput)
                {
                    bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                }

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
                        currentCandle = Candles.Any() ? Candles[0] : null;
                        double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;

                        double Qty = Convert.ToInt32(nudQty.Value);




                        if ((SumBuyFirstItems > SumSellFirstItems) & (STOCHK < InputBuySTOCHK) & (Trendallover > OldBBMiddle) & Mode != "Buy")
                            {

                                
                                if ((OpenPositions.Count < 2) & (OpenOrders.Count < 2))
                                {
                                 
                                    Mode = "Buy";
                                    NormalOrder("Buy", datainput);

                                }
                               /* else
                                {
                                    if (OpenOrders.Count == 0)
                                    {

                                        int Qty = Convert.ToInt32(nudAutoQuantity.Value);
                                        String Side = "Buy";
                                        bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                                    }
                                }
                                */

                            }


                            else if ((STOCHK < InputBuySTOCHK) & (OpenPositions.Count == 0) & (Trendallover > OldBBMiddle) & Mode != "FirstOrder Buy")  
                        {


                                Mode = "FirstOrder Buy";
                                int Quantity2 = Convert.ToInt32(nudAutoQuantity.Value);

                                FirstOrder("Buy", Quantity2);
                                log.InfoFormat("Setmode Wait (currentCandle.STOCHK > InputBuySTOCHK) & (currentCandle.STOCHK < InputSellSTOCHK) FirstOrderSell Buy ");
                            }


                            



                        
                        
                            
                            else if ((SumBuyFirstItems < SumSellFirstItems) & (STOCHK >= InputSellSTOCHK)  & Mode != "Sell")
                            {
                            currentCandle = Candles.Any() ? Candles[0] : null;
                            
                                if ((OpenPositions.Count < 2) & (OpenOrders.Count < 2))
                                {

                                    Mode = "Sell";
                                    NormalOrder("Sell", datainput);

                            }
                               /* else
                                {
                                    if (OpenOrders.Count == 0)
                                    {

                                        int Qty = Convert.ToInt32(nudAutoQuantity.Value);
                                        String Side = "Sell";
                                        bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                                    }
                                }
                                */
                            }

                            else if ((STOCHK > InputSellSTOCHK) & (OpenPositions.Count == 0)  & ( OldBBMiddle > Trendallover) & Mode != "FirstOrder Sell") //& (OpenPositions.Count == 0)
                        {

                                Mode = "FirstOrder Sell";
                                int Quantity2 = Convert.ToInt32(nudAutoQuantity.Value);

                                FirstOrder("Sell", Quantity2);
                                log.InfoFormat("Setmode Wait (currentCandle.STOCHK > InputBuySTOCHK) & (currentCandle.STOCHK < InputSellSTOCHK) FirstOrderSell Sell ");
                            }

                            else

                            {
                                Mode = "Wait";



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
                tmrTradesKingCoinsReader.Start();
                tmrTradesKingCoinsReader_Tick(null, null);
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
                tmrTradesKingCoinsReader.Stop();
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

            if (!OpenPositions.Any())

            {
                percent = 0;

            }

                if (OpenOrders.Count < 4)
            {
                int Quantity2 = Convert.ToInt32 (nudAutoQuantity.Value) / 2;
                DumpandPump("Buy", Quantity2);
                int Quantity3 = Convert.ToInt32 (nudAutoQuantity.Value) / 2;
                DumpandPump("Sell", Quantity3);
            }
            log.InfoFormat("tmrAutoTradeExecution_Tick Dump Pump ");
            if (OpenPositions.Any())
            {
                double? averageEntryPrice = OpenPositions[0].AvgEntryPrice;

                OpenPositions[0].MarkPrice = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
                double? percent = ((((OpenPositions[0].MarkPrice / OpenPositions[0].AvgEntryPrice) * 100) - 100) * -100);
                if (percent < 0)
                {
                    percent = percent * -1;
                }



                log.InfoFormat("tmrAutoTradeExecution_Tick + SetBotMode(), averageEntryPrice: {0},percent: {1}", averageEntryPrice,percent);
                if (currentCandle == null)
                {
                    //   Mode = "Wait";
                    // wenn "Peak kommt" dann bevor er prüft ob er buy oder sell ausführen soll 
                    // müssen alle orders gelöscht werden
                    bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                    log.InfoFormat("tmrAutoTradeExecution_Tick candle Null Cancel Order");
                }
            }
            // See if we are taking profits on open positions, and have positions open and we aren't in our buy or sell periods
            if ((chkAutoMarketTakeProfits.Checked) && (OpenPositions.Any())) //&& Mode != "Wait" && Mode != "Waitelse")) //&& ((OpenPositions.Any()) && ((OpenPositions[0].CurrentQty > 0) || (OpenPositions[0].CurrentQty < 0))))
            {
                if (AllowCalculateOrder == true)
                {
                    AllowCalculateOrder = false;
                }
                //double? OldBBMiddle = Open; //Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;// Candles.OrderByDescending(a => a.TimeStamp).Take(BBLength).Average(a => a.Close);
                // OldBBMiddle = OldBBMiddle + 5;
                double? AvgEntryPrice = OpenPositions[0].AvgEntryPrice;
                double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
                double? part = (OldBBMiddle / AvgEntryPrice) * 100;
                double? percent = part - 100;
                percent = percent * 100;

                if (percent < 0)
                {
                    percent = percent * -1;
                }
                OpenPositions[0].MarkPrice = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
                double? percent2 = ((((OpenPositions[0].MarkPrice / OpenPositions[0].AvgEntryPrice) * 100)-100)*-100);
              if (percent2 < 0)
                {
                    percent2 = percent * -1;
                }

                double InputAutoWin = Convert.ToDouble(nudAutoMarketTakeProfitPercent.Value);
                double KillAllnew = Convert.ToDouble(KillAll.Value);
               
                log.InfoFormat("(chkAutoMarketTakeProfits.Checked && Mode != Sell && Mode != Buy && OpenPositions.Any() && ((OpenPositions[0].CurrentQty > 0) || (OpenPositions[0].CurrentQty < 0)))");
                if (!OpenPositions.Any())

                {
                    percent = 0;

                }
                lblAutoUnrealizedROEPercent.Text = percent.ToString();

                if (percent >= InputAutoWin)
                {
                    int PositionDifference = Convert.ToInt32(OpenPositions[0].CurrentQty);
                    // Make a market order to close out the position, also cancel all orders so nothing else fills if we had unfilled limit orders still open.
                    if (OpenPositions[0].CurrentQty > 0)
                    {
                        //       bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                        //TODO CurrentQty ?????
                        //  String Side = "Sell";
                        // Convert.ToInt32(OpenPositions[0].CurrentQty);
                        AllowCalculateOrder = true;
                        //  bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                        TakeProfit("Sell", Convert.ToInt32(PositionDifference));
                        log.InfoFormat("tmrAutoTradeExecution_Tick (OpenPositions[0].CurrentQty > 0) Sell");
                    }
                    else if (OpenPositions[0].CurrentQty < 0)
                    {
                        //       bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                        //TODO Quantity ?????
                        //     String Side = "Buy";
                        // Convert.ToInt32(OpenPositions[0].CurrentQty * -1);
                        AllowCalculateOrder = true;
                        //   bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                        TakeProfit("Buy", Convert.ToInt32(PositionDifference));
                        log.InfoFormat("tmrAutoTradeExecution_Tick (OpenPositions[0].CurrentQty < 0) Buy");
                    }
                    // Get our positions and orders again to be able to process rest of logic with new information.
                 //   OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
                 //   OpenOrders = bitmex.GetOpenOrders(ActiveInstrument.Symbol);
                    SetBotMode();
                }
            
                if (percent >= KillAllnew)
                {
                    int PositionDifference = Convert.ToInt32(OpenPositions[0].CurrentQty);
                    // Make a market order to close out the position, also cancel all orders so nothing else fills if we had unfilled limit orders still open.
                    

                        if (OpenPositions[0].CurrentQty > 0)
                        {
                         String   Side = "Sell";
                            log.InfoFormat("tmrAutoTradeExecution_Tick Kill all buy safe money Buy");
                            int Qty = Convert.ToInt32(OpenPositions[0].CurrentQty); // Convert.ToInt32(OpenPositions[0].CurrentQty);
                        bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                    }
                        else if (OpenPositions[0].CurrentQty < 0)
                        {
                           String Side = "Buy";
                            log.InfoFormat("tmrAutoTradeExecution_Tick Kill all buy safe money Buy");
                           int Qty = Convert.ToInt32(OpenPositions[0].CurrentQty)* -1; //Convert.ToInt32(OpenPositions[0].CurrentQty) * -1;
                        bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                    }
                       
                    
                  
                }





                else
                {
                    SetBotMode();
                }




                
            }
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
                                    NormalOrder("Buy", Convert.ToInt32(PositionDifference));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoBuy.Checked) Buy");
                                }
                                else if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Sell", Convert.ToInt32(PositionDifference));
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
                                    //NormalOrder("Buy", Convert.ToInt32(PositionDifference));
                                    int Quantity = Convert.ToInt32 (nudAutoQuantity.Value);
                                    NormalOrder("Buy", Quantity);
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoBuy.Checked) (OpenOrders.Any()) Buy");
                                }
                                else if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    int Quantity = Convert.ToInt32(PositionDifference);
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
                                    NormalOrder("Sell", Convert.ToInt32(PositionDifference));
                                }
                                else if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Buy", Convert.ToInt32(PositionDifference));
                                }

                            }
                            else
                            {
                                // No open orders, need to make an order to sell
                                NormalOrder("Sell", Convert.ToInt32(PositionDifference));
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
                                    NormalOrder("Sell", Convert.ToInt32(PositionDifference));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSell.Checked) Sell");
                                }
                                else if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Buyl", Convert.ToInt32(PositionDifference));
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
                                    int Quantity = Convert.ToInt32 (nudAutoQuantity.Value);
                                    NormalOrder("Sell", Quantity);
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSell.Checked) (OpenOrders.Any())Sell");


                                    //NormalOrder("Sell", Convert.ToInt32(PositionDifference));
                                }
                                else if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    int Quantity = Convert.ToInt32 (nudAutoQuantity.Value);
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
                                    NormalOrder("Buy", Convert.ToInt32(PositionDifference));
                                }
                                else if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Sell", Convert.ToInt32(PositionDifference));
                                }

                            }
                            else
                            {
                                // No open orders, need to make an order to sell
                                //MakeOrder("Sell, Convert.ToInt32(PositionDifference));
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

                    case 
                    "Buy":
                      
                        if (OpenPositions.Any() && (currentCandle.STOCHK < InputBuySTOCHK) & Mode != "Buy")
                        {
                            currentCandle = Candles.Any() ? Candles[0] : null;
                            int PositionDifference = Convert.ToInt32(nudAutoQuantity.Value+ 1);

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
                                      int Quantity2 = Convert.ToInt32 (PositionDifference + 1);

                                    FirstOrder("Sell", Quantity2);
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Buy Else FirstOrder");
                                }

                            }

                        }
                        else
                        {
                            if  ((currentCandle.STOCHK < InputSellSTOCHK) & Mode != "Buy")// ((OpenOrders.Any() &&))
                            {
                                // If we have an open order, edit it
                                if (OpenOrders.Any(a => a.Side == "Sell"))
                                {
                                    // We still have an open Sell order, cancel that order, make a new Buy order
                                    //     string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Buy", Convert.ToInt32(PositionDifference + 1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked)(OpenOrders.Any()) Buy NormalOrder");
                                }
                                else if (OpenOrders.Any(a => a.Side == "Buy"))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                    NormalOrder("Sell", Convert.ToInt32(PositionDifference + 1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked)(OpenOrders.Any()) Sell NormalOrder");
                                }
                            }
                            else
                            {
                                NormalOrder("Sell", Convert.ToInt32(PositionDifference + 1));
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
                                if ((OpenOrders.Any(a => a.Side == "Buy")) & Mode != "Sell")
                                {
                                    // We still have an open Sell order, cancel that order, make a new Buy order
                                 //   string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Sell", Convert.ToInt32(PositionDifference + 1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Sell NormalOrder");
                                }
                                else if ((OpenOrders.Any(a => a.Side == "Sell")) & Mode != "Buy")
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                   // string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Buy", Convert.ToInt32(PositionDifference + 1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Sell NormalOrder");
                                }
                            }
                            else
                            {

                                // No open orders, make one for the difference
                                if (PositionDifference != 0)
                                {

                                  //  string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);

                                    NormalOrder("Sell", Convert.ToInt32(PositionDifference + 1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) (PositionDifference != 0) Sell NormalOrder");
                                }

                            }

                        }
                        else
                        {
                            if (OpenOrders.Any())
                            {
                                // If we have an open order, edit it

                                if ((OpenOrders.Any(a => a.Side == "Buy")) & Mode != "Sell")
                                {
                                    // We still have an open Sell order, cancel that order, make a new Buy order
                                   // string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Sell", Convert.ToInt32(PositionDifference + 1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) (OpenOrders.Any()) Sell NormalOrder");

                                }
                                else if ((OpenOrders.Any(a => a.Side == "Buy")) & Mode != "Buy")
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                  //  string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    NormalOrder("Buy", Convert.ToInt32(PositionDifference + 1));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) (OpenOrders.Any()) Buy NormalOrder");
                                }
                            }
                            else
                            {
                                // string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                //  NormalOrder("Sell", Convert.ToInt32(nudQty.Value));
                                //      string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                // FirstOrder("Sell", Convert.ToInt32(nudQty.Value));
                                int Qty = Convert.ToInt32(PositionDifference + 1);
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
                                 //   string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    int Qty = Convert.ToInt32 (nudAutoQuantity.Value);
                                    String Side = "Sell";
                                    bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                                    //FirstOrder("Sell", Convert.ToInt32(nudQty.Value));
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Wait (OpenOrders.Any()) Sell  FirstOrder");
                                }
                                else if ((OpenOrders.Any(a => a.Side == "Sell")) && (currentCandle.STOCHK < InputSellSTOCHK))
                                {
                                    // Edit our only open order, code should not allow for more than 1 at a time for now
                                  //  string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                    int Qty = Convert.ToInt32 (nudAutoQuantity.Value);
                                    String Side = "Buy";
                                    bitmex.MarketOrder(ActiveInstrument.Symbol, Side, Qty);
                                    log.InfoFormat("tmrAutoTradeExecution_Tick (rdoSwitch.Checked) Wait (OpenOrders.Any()) Buy  FirstOrder");
                                }

                            }
                            else if ((OpenPositions[0].CurrentQty < 2) && (currentCandle.STOCHK < InputSellSTOCHK))
                            {
                                // No open orders, need to make an order to sell
                             //   string result = bitmex.CancelAllOpenOrders(ActiveInstrument.Symbol);
                                int Qty = Convert.ToInt32 (nudAutoQuantity.Value);
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
                                    NormalOrder("Buy", Convert.ToInt32(PositionDifference));
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
                                NormalOrder("Buy", Convert.ToInt32(PositionDifference));
                            }
                        }
                        break;


                }
            }
            
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
            MessageBox.Show("If you change also save allways settings");
        }

        private void nudOverTimeInterval_ValueChanged(object sender, EventArgs e)
        {
            UpdateOverTimeSummary();
            MessageBox.Show("If you change also save allways settings");
        }

        private void nudOverTimeIntervalCount_ValueChanged(object sender, EventArgs e)
        {
            UpdateOverTimeSummary();
            MessageBox.Show("If you change also save allways settings");
        }

        private void btnBuyOverTimeOrder_Click(object sender, EventArgs e)
        {
            UpdateOverTimeSummary(); // Makes sure our variables are current.

            OTSide = "Buy";

            tmrTradeOverTime.Interval = OTIntervalSeconds * 1000; // Must multiply by 1000, because timers operate in milliseconds.
            tmrTradeOverTime.Start(); // Start the timer.
            
        }

        private void btnSellOverTimeOrder_Click(object sender, EventArgs e)
        {
            UpdateOverTimeSummary(); // Makes sure our variables are current.

            OTSide = "Sell";

            tmrTradeOverTime.Interval = OTIntervalSeconds * 1000; // Must multiply by 1000, because timers operate in milliseconds.
            tmrTradeOverTime.Start(); // Start the timer.
            
        }

        private void tmrTradeOverTime_Tick(object sender, EventArgs e)
        {
            OTTimerCount++;
            bitmex.MarketOrder(ActiveInstrument.Symbol, OTSide, OTContractsPer);

            double Percent = ((double)OTTimerCount / (double)OTIntervalCount) * 100;
          

            if(OTTimerCount == OTIntervalCount)
            {
                OTTimerCount = 0;
                tmrTradeOverTime.Stop();
               
                
            }
        }

        private void btnOverTimeStop_Click(object sender, EventArgs e)
        {
            OTTimerCount = 0;
            
            tmrTradeOverTime.Stop();
        }

        //CHANGE 2: Price set from UI
        private void btnSetPrice_Click(object sender, EventArgs e)
        {
            InitializeParameterSettings();
            MessageBox.Show("Dont use if btc make between min and max of a step more then 40 Usd in biggest dumps pumps last hours. You accept our Terms of Use if you run the bot");
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
           // log.InfoFormat("CalculateBuyPrice - SumSellFirstItems der ersten {0} OrderBooks: {1}, sizeLimit: {2}", BuyElementsToTake, SumSellFirstItems, sizeLimit);
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

     //    log.InfoFormat("CalculatedSellPrice - SumSellFirstItems der ersten {0} OrderBooks: {1}, sizeLimit: {2}", SellElementsToTake, SumSellFirstItems, sizeLimit);
     //    log.InfoFormat("CalculatedSellPrice - SumSellFirstItems der ersten {0} OrderBooks: {1}, sizeLimit: {2}", SellElementsToTake, SumSellFirstItems, sizeLimit);
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
            MessageBox.Show("If you change also save allways settings");
        }

        private void nudBuyStochk_ValueChanged(object sender, EventArgs e)
        {
            InputBuySTOCHK = Convert.ToDouble(nudBuyStochk.Value);
            MessageBox.Show("If you change also save allways settings");
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

        private void tmrTradesKingCoinsReader_Tick(object sender, EventArgs e)
        {
            using (WebClientWithTimeout wc = new WebClientWithTimeout())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/json");
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                
                try
                {
                    var json = wc.DownloadString(CONST_URL_TRADES_KING_JSON);
                    TradesKingCoinsRootObject rootTradesKing = JsonConvert.DeserializeObject<TradesKingCoinsRootObject>(json);
                    ITradesKingCoin coin = rootTradesKing.GetCurrentTradesKingCoin(ActiveInstrument.Symbol);
                    double OldBBMiddle = Candles.Any() && Candles[0].Open.HasValue ? Candles[0].Open.Value : 0d;
                    Trend1h = ((OldBBMiddle * (coin.change1h / 1000))+ OldBBMiddle);
                    Trend24h = ((OldBBMiddle * (coin.change24h / 100)) + OldBBMiddle);
                    txtTrend1h.Text = Trend1h.ToString(CultureInfo.InvariantCulture);
                    txtTrend24h.Text = Trend24h.ToString(CultureInfo.InvariantCulture);
                    if (Trend1h > Trend24h)
                    {
                        Trendallover = Trend1h;

                    }

                    else if (Trend24h > Trend1h)
                    {
                        Trendallover = Trend24h;
                    }

                    log.InfoFormat("tmrTradesKingCoinsReader_Tick - OldBBMiddle: {0} Trend1h: {1} Trend24h: {2} Trendallover: {3} ", OldBBMiddle, Trend1h, Trend24h, Trendallover);
                }
                catch (Exception exception)
                {
                    log.Error("Exception bei tmrTradesKingCoinsReader_Tick: ", exception);
                }
            }
        }

    
        private void nudAutoQuantity_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
            MessageBox.Show("If you change quantity change, also change manually your position to set size (buy or sell more). And also save allways settings");
        }

        private void nudAutoMarketTakeProfitPercent_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
            
        }

        private void PumpInput_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
            
        }

        private void DumpInput_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
           
        }

        private void nudBuyElementsToTake_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
       
        }

        private void nudConstantBuyDividend_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
          
        }

        private void nudSellElementsToTake_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
           
        }

        private void nudConstantSellDividend_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
        
        }

        private void nudVolume24h_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
          
        }

        private void nudQty_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
          
        }

        private void nudStopPercent_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
           
        }

        private void nudPriceBuy_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
           
        }

        private void nudPriceSell_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
           
        }

        private void ddlAutoOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
          
        }

        private void BalanceExit_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            InitializeParameterSettings();
            MessageBox.Show(Convert.ToString (percent));
        }

        private void KillAll_ValueChanged(object sender, EventArgs e)
        {
            InitializeParameterSettings();
        }
    }
}
