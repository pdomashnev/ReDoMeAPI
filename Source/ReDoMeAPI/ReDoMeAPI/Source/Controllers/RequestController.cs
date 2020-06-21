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
                    request.state = RequestState.New;
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
            Get["ReDoMeApi/Request/GetPhotos"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Request/GetPhotos", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.req_id.HasValue)
                    {
                        throw new Exception("Missing parameter req_id");
                    }

                    int Req_ID = this.Request.Query.req_id;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    PhotoList photos = Database.getRequestPhotos(Req_ID);
                    if (photos == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(photos.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Request/GetPhotos: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Request/GetPhotos", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/Request/SetScore"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Request/SetScore", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.req_id.HasValue)
                    {
                        throw new Exception("Missing parameter req_id");
                    }
                    if (!this.Request.Query.score.HasValue)
                    {
                        throw new Exception("Missing parameter score");
                    }

                    int Req_ID = this.Request.Query.req_id;
                    int Score = this.Request.Query.score;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    if (!Database.setScoreForRequest(Req_ID, Score))
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse("OK", HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Request/SetScore: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Request/SetScore", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };

            Get["ReDoMeApi/Request/GetAllNew"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Request/GetAllNew", System.Diagnostics.EventLogEntryType.SuccessAudit);


                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    RequestList requests = Database.getRequests(RequestState.New);
                    if (requests == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(requests.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Request/GetAllNew: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Request/GetAllNew", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };

            Get["ReDoMeApi/Request/GetAll"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Request/GetAll", System.Diagnostics.EventLogEntryType.SuccessAudit);


                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    RequestList requests = Database.getRequests(RequestState.Any);
                    if (requests == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(requests.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Request/GetAll: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Request/GetAll", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/Request/GetByClient"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Request/GetByClient", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.client.HasValue)
                    {
                        throw new Exception("Missing parameter Client");
                    }

                    string clientVkId = this.Request.Query.client;


                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    RequestList requests = Database.getRequestsByClient(clientVkId);
                    if (requests == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(requests.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Request/GetByClient: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Request/GetByClient", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/Request/GetByBarber"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Request/GetByBarber", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.barber.HasValue)
                    {
                        throw new Exception("Missing parameter Barber");
                    }

                    string barberVkId = this.Request.Query.barber;


                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    RequestList requests = Database.getRequestsByMaster(barberVkId);
                    if (requests == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(requests.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Request/GetByBarber: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Request/GetByBarber", System.Diagnostics.EventLogEntryType.SuccessAudit);
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

