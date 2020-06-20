using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ReDoMeAPI
{
    public class ErrorAnswer
    {
        public string errorMessage { set; get; }

        public ErrorAnswer(string _errorMessage)
        {
            errorMessage = _errorMessage;
        }

        public string ToJson() => JsonConvert.SerializeObject(this);

        public static ErrorAnswer FromJson(string json) => JsonConvert.DeserializeObject<ErrorAnswer>(json);
    }
}
