using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReDoMeAPI
{
    public enum RequestType
    {
        //1 - только по моему фото
        MyPhoto = 1,
        //2 - по моему и желаемому фото
        MyAndOtherPhoto = 2,
        //3 - хочу как звезда
        AsStart = 3
    }

    public enum ReqeustState
    {
        //1 - Новая
        New = 1,
        //2 - На исполнении
        InProcess = 2 ,
        //3 - Завершено
        Finished = 3
    }
    public class Request
    {
        public string client_vk_id { set; get; }
        public string client_name { set; get; }
        public string city { set; get; }
        public string comment { set; get; }

        public RequestType type { set; get; }
        public ReqeustState state { set; get; }
        public int score { set; get; }
        public string ToJson() => JsonConvert.SerializeObject(this);

        public static Request FromJson(string json) => JsonConvert.DeserializeObject<Request>(json);
    }
}
