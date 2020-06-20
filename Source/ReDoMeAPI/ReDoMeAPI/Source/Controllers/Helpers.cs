using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Extensions;
using System.IO;

namespace ReDoMeAPI
{
    class ReDoMeAPIResponse
    {
        static public Response CreateResponse(string _response, HttpStatusCode _status)
        {
            Response resp = new Response
            {
                ContentType = "application/json",
                
                Contents = stream =>
                {
                    TextWriter writer = new StreamWriter(stream);
                    writer.WriteLine(_response);
                    writer.Close();
                },
                StatusCode = _status
            };
            resp.Headers["Access-Control-Allow-Origin"] = "*";
            return resp;
        }
        //---------------------------------------------
    }
}
