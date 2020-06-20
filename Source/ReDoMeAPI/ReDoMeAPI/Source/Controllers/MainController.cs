using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Extensions;
using System.Text;
using System.IO;

namespace ReDoMeAPI
{
    public class MainControllerModule : NancyModule
    {
        public MainControllerModule()
        {
            Get["ReDoMeApi/GetSalon"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/GetSalon", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.barber.HasValue)
                    {
                        throw new Exception("Missing parameter Barber");
                    }

                    string barberVkId = this.Request.Query.barber;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    Salon salon = Database.getSalonByBarber(barberVkId);
                    if (salon == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("salon not found");
                        return CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return CreateResponse(salon.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error GetSalon: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
            };

        }
        //---------------------------------------------
        protected Response CreateResponse(string _response, HttpStatusCode _status)
        {
            return new Response
            {
                ContentType = "Text",
                Contents = stream =>
                {
                    TextWriter writer = new StreamWriter(stream);
                    writer.WriteLine(_response);
                    writer.Close();
                },
                StatusCode = _status
            };
        }
        //---------------------------------------------
        protected void SendLogMessage(string _Message, System.Diagnostics.EventLogEntryType _Type, Exception _Exception = null)
        {
            Logger.AddMessageToLog(new LogMessage(_Message, "MainController", _Type, _Exception));
        }
        //---------------------------------------------
    }
}
