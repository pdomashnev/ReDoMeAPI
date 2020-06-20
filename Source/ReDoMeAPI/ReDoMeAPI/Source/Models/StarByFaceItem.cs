using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReDoMeAPI
{
    public class StarByFaceList
    {
        public List<StarByFaceItem> items;

        public string ToJson() => JsonConvert.SerializeObject(this);

        public static StarByFaceList FromJson(string json) => JsonConvert.DeserializeObject<StarByFaceList>(json);
    }
    public class StarByFaceItem
    {
        public string name { set; get; }
        public string url { set; get; }
        public int percent { set; get; }

    }
}
