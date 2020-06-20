using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReDoMeAPI
{
    public enum PhotoType
    {
        Any = 0,
        /// <summary>
        ///         1 - Портфолио салона
        /// </summary>
        PortfolioSalon = 1,
        /// <summary>
        /// 2 - Портфолио мастера
        /// </summary>
        PortfolioMaster = 2,
        /// <summary>
        /// 3 - Клиент
        /// </summary>
        Client,
        /// <summary>
        /// 4 - Желание клиента
        /// </summary>
        ClientGoal,
        /// <summary>
        /// 5 - Предложение
        /// </summary>
        OfferMaster,
        /// <summary>
        /// 6 - Фото после
        /// </summary>
        ClientAfter
    }
    public class PhotoList
    {
        public List<Photo> items;
        public PhotoType listType;
        public string ToJson() => JsonConvert.SerializeObject(this);

        public static PhotoList FromJson(string json) => JsonConvert.DeserializeObject<PhotoList>(json);
    }
    public class Photo
    {
        public Int64 id { set; get; }
        public PhotoType type { set; get; }
        public string vk_link { set; get; }
        public string content { set; get; }
        public string comment { set; get; }

        public string ToJson() => JsonConvert.SerializeObject(this);

        public static Photo FromJson(string json) => JsonConvert.DeserializeObject<Photo>(json);
    }
}
