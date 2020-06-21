using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Extensions;

namespace ReDoMeAPI
{
    public class OfferControllerModule : NancyModule
    {
        public OfferControllerModule()
        {
            Post["ReDoMeApi/Offer/Create"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Offer/Create", System.Diagnostics.EventLogEntryType.SuccessAudit);
                    var jsonString = this.Request.Body.AsString();
                    Offer offer = ReDoMeAPI.Offer.FromJson(jsonString);
                    offer.selected = false;
                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    Int64 Offer_ID = Database.createOffer(offer);
                    if (Offer_ID == 0)
                    {
                        ErrorAnswer answer = new ErrorAnswer("creating offer error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(Offer_ID.ToString(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Offer/Create: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Offer/Create", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Post["ReDoMeApi/Offer/AddPhotos"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Offer/AddPhotos", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.offer_id.HasValue)
                    {
                        throw new Exception("Missing parameter offer_id");
                    }

                    Int64 Offer_ID = this.Request.Query.offer_id;

                    var jsonString = this.Request.Body.AsString();
                    PhotoList photos = ReDoMeAPI.PhotoList.FromJson(jsonString);
                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    if (!Database.addPhotosToOffer(Offer_ID, photos))
                    {
                        ErrorAnswer answer = new ErrorAnswer("adding photos error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse("OK", HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Offer/AddPhotos: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Offer/AddPhotos", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Put["ReDoMeApi/Offer/Accept"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Offer/Accept", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.offer_id.HasValue)
                    {
                        throw new Exception("Missing parameter offer_id");
                    }
                    if (!this.Request.Query.req_id.HasValue)
                    {
                        throw new Exception("Missing parameter req_id");
                    }

                    int Req_ID = this.Request.Query.req_id;
                    int Offer_ID = this.Request.Query.offer_id;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    bool bRes = Database.acceptOffer(Req_ID, Offer_ID);
                    if (!bRes)
                    {
                        ErrorAnswer answer = new ErrorAnswer("offer not found");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse("OK", HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Offer/Accept: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Offer/Accept", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/Offer/Delete"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Offer/Delete", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.offer_id.HasValue)
                    {
                        throw new Exception("Missing parameter offer_id");
                    }

                    int Offer_ID = this.Request.Query.offer_id;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    if (!Database.deleteOffer(Offer_ID))
                    {
                        ErrorAnswer answer = new ErrorAnswer("offer not found");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse("OK", HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Offer/Delete: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Offer/Delete", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/Offer/GetStatusesByBarber"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Offer/GetStatusesByBarber", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.barber.HasValue)
                    {
                        throw new Exception("Missing parameter Barber");
                    }

                    string barberVkId = this.Request.Query.barber;
                    RequestState state = RequestState.Any;
                    if (this.Request.Query.state.HasValue)
                        state = (RequestState)((Int16)this.Request.Query.state);


                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    RequestWithOfferList offers = Database.getOffersState(barberVkId, state);
                    if (offers == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(offers.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Offer/GetStatusesByBarber: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Offer/GetStatusesByBarber", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/Offer/GetPhotos"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/Offer/GetPhotos", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.offer_id.HasValue)
                    {
                        throw new Exception("Missing parameter offer_id");
                    }

                    int Offer_ID = this.Request.Query.offer_id;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    PhotoList photos = Database.getOfferPhotos(Offer_ID);
                    if (photos == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(photos.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error Offer/GetPhotos: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/Offer/GetPhotos", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };

        }
        //---------------------------------------------
        protected void SendLogMessage(string _Message, System.Diagnostics.EventLogEntryType _Type, Exception _Exception = null)
        {
            Logger.AddMessageToLog(new LogMessage(_Message, "OfferController", _Type, _Exception));
        }
        //---------------------------------------------
    }
}
