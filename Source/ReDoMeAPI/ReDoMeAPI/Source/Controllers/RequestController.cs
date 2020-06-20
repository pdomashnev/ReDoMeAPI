using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Extensions;

namespace ReDoMeAPI
{
    public class RequestControllerModule : NancyModule
    {
        public RequestControllerModule()
        {
            Post["ReDoMeApi/Request/Create"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Request/Create", System.Diagnostics.EventLogEntryType.SuccessAudit);
                    var jsonString = this.Request.Body.AsString();
                    Request request = ReDoMeAPI.Request.FromJson(jsonString);
                    request.state = ReqeustState.New;
                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    Int64 Req_ID = Database.createRequest(request);
                    if (Req_ID == 0)
                    {
                        ErrorAnswer answer = new ErrorAnswer("creating request error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(Req_ID.ToString(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Request/Create: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Request/Create", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Post["ReDoMeApi/Request/AddPhotos"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Request/AddPhotos", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.req_id.HasValue)
                    {
                        throw new Exception("Missing parameter req_id");
                    }

                    Int64 Req_ID = this.Request.Query.req_id;

                    var jsonString = this.Request.Body.AsString();
                    PhotoList photos = ReDoMeAPI.PhotoList.FromJson(jsonString);
                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    if (!Database.addPhotosToRequest(Req_ID, photos))
                    {
                        ErrorAnswer answer = new ErrorAnswer("adding photos error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse("OK", HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Request/AddPhotos: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Request/AddPhotos", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/Request/GetOffers"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Request/GetOffers", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.req_id.HasValue)
                    {
                        throw new Exception("Missing parameter req_id");
                    }

                    int Req_ID = this.Request.Query.req_id;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    OfferList offers = Database.getOffersForRequest(Req_ID);
                    if (offers == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(offers.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Request/GetOffers: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Request/GetOffers", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/Request/GetAllActive"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Request/GetAll", System.Diagnostics.EventLogEntryType.SuccessAudit);


                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    RequestList requests = Database.getActiveRequests();
                    if (requests == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(requests.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Request/GetAllActive: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Request/GetAllActive", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };

        }
        //---------------------------------------------
        protected void SendLogMessage(string _Message, System.Diagnostics.EventLogEntryType _Type, Exception _Exception = null)
        {
            Logger.AddMessageToLog(new LogMessage(_Message, "RequestController", _Type, _Exception));
        }
        //---------------------------------------------
    }

}

