using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReDoMeAPI
{
    public class Barber
    {
        public int id { set; get; }
        public string vk_id { set; get; }
        public int sal_id { set; get; }
        public string name { set; get; }
        public string spec { set; get; }
        public string master { set; get; }
        public string address { set; get; }
        public string phone { set; get; }
        public string about { set; get; }
        public string certs { set; get; }
        public string raiting { set; get; }

        public string ToJson() => JsonConvert.SerializeObject(this);

        public static Barber FromJson(string json) => JsonConvert.DeserializeObject<Barber>(json);
    }
}
