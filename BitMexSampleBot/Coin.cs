using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMexSampleBot
{
    public class Coin
    {
        public string name { get; set; }
        public string teamname { get; set; }
        public string email { get; set; }
        public Array players { get; set; }

        public Coin(string json)
        {
            JObject jObject = JObject.Parse(json);
            /*
            JToken jCoin = jObject["user"];
            name = (string)jCoin["name"];
            teamname = (string)jCoin["teamname"];
            email = (string)jCoin["email"];
            players = jCoin["players"].ToArray();*/
        }

    }
}
