using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReDoMeAPI
{
    public class Offer
    {
        public string ToJson() => JsonConvert.SerializeObject(this);

        public static Offer FromJson(string json) => JsonConvert.DeserializeObject<Offer>(json);
    }
}
