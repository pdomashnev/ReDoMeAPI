using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReDoMeAPI
{
    public class RequestWithOfferList
    {
        public List<RequestWithOffer> items;

        public string ToJson() => JsonConvert.SerializeObject(this);
        public static RequestWithOfferList FromJson(string json) => JsonConvert.DeserializeObject<RequestWithOfferList>(json);
    }
    public class RequestWithOffer
    {
        public Request request;
        public Offer offer;
        public string ToJson() => JsonConvert.SerializeObject(this);
        public static RequestWithOffer FromJson(string json) => JsonConvert.DeserializeObject<RequestWithOffer>(json);
    }
}
