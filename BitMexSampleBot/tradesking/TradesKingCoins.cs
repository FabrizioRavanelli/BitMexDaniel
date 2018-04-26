using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitMEX;

namespace BitMexSampleBot.tradesking
{
    public interface ITradesKingCoin
    {
        string coin { get; set; }
        string value_fiat { get; set; }
        string value_btc { get; set; }
        string price_fiat { get; set; }
        string price_btc { get; set; }
        double change1h { get; set; }
        double change24h { get; set; }
        double change7d { get; set; }
        double change30h { get; set; }

    }

    public class ABY: ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class ADA : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class ADX : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class AEON : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class AMP : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class ANT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class ARK : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class BAT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class BAY : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class BCH : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class BLK : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class BNT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class BSD : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class BTC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class BTG : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class BURST : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class CANN : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class CLUB : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class CPC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class CVC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class DASH : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class DCR : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class DCT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class DGB : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class DOGE : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class EBST : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class EDG : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class EFL : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class EMC2 : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class ENG : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class ETC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class ETH : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class EXCL : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class FCT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class FTC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class FUN : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class GAME : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class GBG : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class GCR : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class GNO : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class GNT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class GRS : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class GUP : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class HMQ : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class INCNT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class IOC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class ION : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class IOP : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class KMD : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class KORE : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class LBC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class LMC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class LSK : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class LTC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class MANA : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class MEME : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class MER : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class MONA : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class NAV : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class NEO : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class NEOS : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class NMR : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class NXC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class NXT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class OK : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class OMNI : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class PIVX : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class POWR : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class PPC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class PTC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class QTUM : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class RDD : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class RISE : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class SAFEX : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class SALT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class SBD : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class SC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class SPR : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class START : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class STORJ : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class STRAT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class SYS : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class THC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class TRIG : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class TX : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class UBQ : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class UNO : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class USDT : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class VIB : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class VTC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class WAVES : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class XCP : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class XEL : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class XLM : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class XRP : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class XVC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class XWC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class XZC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class ZEC : ITradesKingCoin
    {
        public string coin { get; set; }
        public string value_fiat { get; set; }
        public string value_btc { get; set; }
        public string price_fiat { get; set; }
        public string price_btc { get; set; }
        public double change1h { get; set; }
        public double change24h { get; set; }
        public double change7d { get; set; }
        public double change30h { get; set; }
    }

    public class Details
    {
        public ABY ABY { get; set; }
        public ADA ADA { get; set; }
        public ADX ADX { get; set; }
        public AEON AEON { get; set; }
        public AMP AMP { get; set; }
        public ANT ANT { get; set; }
        public ARK ARK { get; set; }
        public BAT BAT { get; set; }
        public BAY BAY { get; set; }
        public BCH BCH { get; set; }
        public BLK BLK { get; set; }
        public BNT BNT { get; set; }
        public BSD BSD { get; set; }
        public BTC BTC { get; set; }
        public BTG BTG { get; set; }
        public BURST BURST { get; set; }
        public CANN CANN { get; set; }
        public CLUB CLUB { get; set; }
        public CPC CPC { get; set; }
        public CVC CVC { get; set; }
        public DASH DASH { get; set; }
        public DCR DCR { get; set; }
        public DCT DCT { get; set; }
        public DGB DGB { get; set; }
        public DOGE DOGE { get; set; }
        public EBST EBST { get; set; }
        public EDG EDG { get; set; }
        public EFL EFL { get; set; }
        public EMC2 EMC2 { get; set; }
        public ENG ENG { get; set; }
        public ETC ETC { get; set; }
        public ETH ETH { get; set; }
        public EXCL EXCL { get; set; }
        public FCT FCT { get; set; }
        public FTC FTC { get; set; }
        public FUN FUN { get; set; }
        public GAME GAME { get; set; }
        public GBG GBG { get; set; }
        public GCR GCR { get; set; }
        public GNO GNO { get; set; }
        public GNT GNT { get; set; }
        public GRS GRS { get; set; }
        public GUP GUP { get; set; }
        public HMQ HMQ { get; set; }
        public INCNT INCNT { get; set; }
        public IOC IOC { get; set; }
        public ION ION { get; set; }
        public IOP IOP { get; set; }
        public KMD KMD { get; set; }
        public KORE KORE { get; set; }
        public LBC LBC { get; set; }
        public LMC LMC { get; set; }
        public LSK LSK { get; set; }
        public LTC LTC { get; set; }
        public MANA MANA { get; set; }
        public MEME MEME { get; set; }
        public MER MER { get; set; }
        public MONA MONA { get; set; }
        public NAV NAV { get; set; }
        public NEO NEO { get; set; }
        public NEOS NEOS { get; set; }
        public NMR NMR { get; set; }
        public NXC NXC { get; set; }
        public NXT NXT { get; set; }
        public OK OK { get; set; }
        public OMNI OMNI { get; set; }
        public PIVX PIVX { get; set; }
        public POWR POWR { get; set; }
        public PPC PPC { get; set; }
        public PTC PTC { get; set; }
        public QTUM QTUM { get; set; }
        public RDD RDD { get; set; }
        public RISE RISE { get; set; }
        public SAFEX SAFEX { get; set; }
        public SALT SALT { get; set; }
        public SBD SBD { get; set; }
        public SC SC { get; set; }
        public SPR SPR { get; set; }
        public START START { get; set; }
        public STORJ STORJ { get; set; }
        public STRAT STRAT { get; set; }
        public SYS SYS { get; set; }
        public THC THC { get; set; }
        public TRIG TRIG { get; set; }
        public TX TX { get; set; }
        public UBQ UBQ { get; set; }
        public UNO UNO { get; set; }
        public USDT USDT { get; set; }
        public VIB VIB { get; set; }
        public VTC VTC { get; set; }
        public WAVES WAVES { get; set; }
        public XCP XCP { get; set; }
        public XEL XEL { get; set; }
        public XLM XLM { get; set; }
        public XRP XRP { get; set; }
        public XVC XVC { get; set; }
        public XWC XWC { get; set; }
        public XZC XZC { get; set; }
        public ZEC ZEC { get; set; }
    }

    public class TradesKingCoinsRootObject
    {
        public int success { get; set; }
        public string method { get; set; }
        public string account_currency { get; set; }
        public Details details { get; set; }

        public ITradesKingCoin GetCurrentTradesKingCoin(string bitmexCoin)
        {
            switch (bitmexCoin.ToLower())
            {
                case "adam18":
                    return details.ADA;
                case "xbtusd":
                    return details.BTC;
                case "bchm18":
                    return details.BTG;
                case "ethm18":
                    return details.ETH;
                case "ltcm18":
                    return details.LTC;
                case "xrpm18":
                    return details.XRP;
                default:
                    //unbekannte coin - ohne mapping
                    return null;
            }
        }
    }
}
