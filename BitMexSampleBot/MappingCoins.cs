using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMexSampleBot
{
    public static class MappingCoins
    {
        public static Dictionary<string, string> Get()
        {
            var mapping = new Dictionary<string, string>
            {
                {"ada", "adam18"},
                {"btc", "xbtusd"},
                {"btg", "bchm18"},
                {"eth", "ethm18"},
                {"ltc", "ltcm18"},
                {"xrp", "xrpm18"}
                //TODO Fehlen mappings für alle symbolen
            };
            return mapping;
        }
    }
}
