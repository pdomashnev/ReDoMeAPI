using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReDoMeAPI
{
    public class OfferList
    {
        public List<Offer> items;

        public string ToJson() => JsonConvert.SerializeObject(this);

        public static OfferList FromJson(string json) => JsonConvert.DeserializeObject<OfferList>(json);
    }
    public class Offer
    {
        public Int64 id;
        public string bar_vk_id { set; get; }
        public int? sal_id { set; get; }
        public Int64 req_id { set; get; }
        public double cost { set; get; }
        public DateTime date { set; get; }
        public string comment { set; get; }
        public bool selected { set; get; }
        public PhotoList photos { set; get; }

        public string ToJson() => JsonConvert.SerializeObject(this);

        public static Offer FromJson(string json) => JsonConvert.DeserializeObject<Offer>(json);
    }
}
