using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReDoMeAPI
{
    public class SalonList
    {
        public List<Salon> items;

        public string ToJson() => JsonConvert.SerializeObject(this);

        public static SalonList FromJson(string json) => JsonConvert.DeserializeObject<SalonList>(json);
    }
    public class Salon
    {

        public int id { set; get; }
        public string vk_id { set; get; }
        public string name { set; get; }
        public string city { set; get; }
        public string address { set; get; }
        public string phone { set; get; }
        public int raiting { set; get; }

        public string ToJson() => JsonConvert.SerializeObject(this);

        public static Salon FromJson(string json) => JsonConvert.DeserializeObject<Salon>(json);
    }
}
