using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Extensions;

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
                    if(salon == null)
                        return "salon not found"

                    MIIProductStatus productStatus = new MIIProductStatus
                    {
                        Code = SN,
                        Plant = Plant,
                        Status = Status,
                        Grade = Grade
                    };
                    AutoResetEvent res = new AutoResetEvent(false);
                    Tracking.WebService.WebServiceCallback callback = new WebService.WebServiceCallback
                    {
                        callbackEvent = res
                    };
                    Singleton.Agent.Notify(new SRequestInfo((uint)Messages.SET_PRODUCT_STATUS, productStatus, callback));
                    res.WaitOne();
                    if (!callback.Result)
                        throw (Exception)callback.Data;
                    WebService.Answer answer = new WebService.Answer("OK");

                    SendLogMessage("processed TrackingApi/SetSNStatus", System.Diagnostics.EventLogEntryType.SuccessAudit);
                    return new Response
                    {
                        ContentType = "Text",
                        Contents = stream =>
                        {
                            TextWriter writer = new StreamWriter(stream);
                            writer.WriteLine(answer.ToXML());
                            writer.Close();
                        },
                        StatusCode = HttpStatusCode.OK
                    };

                    //                    return answer.ToXML();
                }
                catch (Exception exc)
                {
                    string Err = $"Error SetSNStatus: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);

                    WebService.Answer answer = new WebService.Answer("Error", exc.Message);

                    return new Response
                    {
                        ContentType = "Text",
                        Contents = stream =>
                        {
                            TextWriter writer = new StreamWriter(stream);
                            writer.WriteLine(answer.ToXML());
                            writer.Close();
                        },
                        StatusCode = HttpStatusCode.OK
                    };
                }
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
