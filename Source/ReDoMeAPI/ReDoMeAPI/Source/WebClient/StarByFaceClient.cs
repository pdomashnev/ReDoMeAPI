using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.IO;

namespace ReDoMeAPI
{
    public class StarByFaceClient
    {
        private static string starByFaceLink = @"https://starbyface.com/Home/LooksLike";

        protected static XElement Exec(string _photo_link)
        {
            string url = starByFaceLink + "?url=" + _photo_link;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.ContentLength = 0;
//            request.ContentType = "application/json; charset=utf-8";
//            request.Accept = "application/json, text/javascript, */*";
            request.Method = "POST";

            WebResponse resp = request.GetResponse();
            Stream stream = resp.GetResponseStream();
            string answer = "";
            using (StreamReader sr = new StreamReader(stream))
            {
                answer = sr.ReadToEnd();
            }

            XElement xResp = XElement.Parse("<root>" + answer + "</root>");

            return xResp;
        }

        public static StarByFaceList GetStarsList(string _photo_link)
        {
            StarByFaceList resList = new StarByFaceList();
            resList.items = new List<StarByFaceItem>();
            XElement xelem = Exec(_photo_link);
            IEnumerable<XElement> rootDivs = xelem.Elements("div");
            foreach (XElement rootDiv in rootDivs)
            {
                if (rootDiv.Attribute("id").Value == "best-pair-result")
                    continue;
                IEnumerable<XElement> items = rootDiv.Elements("div");
                foreach (XElement elem in items)
                {
                    StarByFaceItem item = new StarByFaceItem();
                    XElement textCenterDiv = elem.Element("div");
                    XElement progressProgressStripedDiv = textCenterDiv.Descendants("div")
                        .FirstOrDefault(el => el.Attribute("class")?.Value == "progress progress-striped");
                    if (progressProgressStripedDiv != null)
                    {
                        string percentStr = progressProgressStripedDiv.Element("div").Attribute("similarity").Value;
                        int val = 0;
                        Int32.TryParse(percentStr, out val);
                        item.percent = val;
                    }
                    XElement candidateRealDiv = textCenterDiv.Descendants("div")
                        .FirstOrDefault(el => el.Attribute("class")?.Value == "candidate-real");
                    if (candidateRealDiv != null)
                    {
                        item.url = candidateRealDiv.Element("img").Attribute("src").Value;
                        XElement p = candidateRealDiv.Element("div").Element("p");
                        item.name = p.Value;
                    }

                    resList.items.Add(item);
                }
            }
            return resList;
        }
    }
}
