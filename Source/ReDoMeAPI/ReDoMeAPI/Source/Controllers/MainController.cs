using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Extensions;
using System.IO;

namespace ReDoMeAPI
{
    public class MainControllerModule : NancyModule
    {
        //---------------------------------------------
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
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(salon.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error GetSalon: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/GetSalon", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/GetSalonById"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/GetSalonById", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.sal_id.HasValue)
                    {
                        throw new Exception("Missing parameter sal_id");
                    }

                    int sal_ID = this.Request.Query.sal_id;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    Salon salon = Database.getSalonByID(sal_ID);
                    if (salon == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("salon not found");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(salon.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error GetSalonById: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/GetSalonById", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/GetAllSalons"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/GetAllSalons", System.Diagnostics.EventLogEntryType.SuccessAudit);


                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    SalonList salonList = Database.getSalons();
                    if (salonList == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(salonList.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error GetAllSalons: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/GetAllSalons", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/GetSalonPortfolio"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/GetSalonPortfolio", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.sal_id.HasValue)
                    {
                        throw new Exception("Missing parameter sal_id");
                    }

                    int sal_ID = this.Request.Query.sal_id;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    PhotoList photos = Database.getSalonPhotos(sal_ID, PhotoType.PortfolioSalon);
                    if (photos == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(photos.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error GetSalonPortfolio: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/GetSalonPortfolio", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/GetAllBarbers"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/GetAllBarbers", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    BarberList barberList = Database.getBarbers();
                    if (barberList == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(barberList.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error GetAllBarbers: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/GetAllBarbers", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/GetBarber"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/GetBarber", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.barber.HasValue)
                    {
                        throw new Exception("Missing parameter barber");
                    }

                    string barber_vk_id = this.Request.Query.barber;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    Barber barber = Database.getBarber(barber_vk_id);
                    if (barber == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("barber not found");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(barber.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error GetBarber: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/GetBarber", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/GetBarberPortfolio"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/GetBarberPortfolio", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.barber.HasValue)
                    {
                        throw new Exception("Missing parameter barber");
                    }

                    string barber_vk_id = this.Request.Query.barber;

                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    PhotoList photos = Database.getBarberPhotos(barber_vk_id, PhotoType.PortfolioMaster);
                    if (photos == null)
                    {
                        ErrorAnswer answer = new ErrorAnswer("server error");
                        return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    }
                    return ReDoMeAPIResponse.CreateResponse(photos.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error GetBarberPortfolio: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/GetBarberPortfolio", System.Diagnostics.EventLogEntryType.SuccessAudit);
                }
            };
            Get["ReDoMeApi/getStarByFace"] = parameters =>
            {
                try
                {
                    SendLogMessage("called ReDoMeApi/getStarByFace", System.Diagnostics.EventLogEntryType.SuccessAudit);

                    if (!this.Request.Query.photo_link.HasValue)
                    {
                        throw new Exception("Missing parameter photo_link");
                    }

                    string photo_link = this.Request.Query.photo_link;


                    StarByFaceList faceList = StarByFaceClient.GetStarsList(photo_link);
                    //if (User != Tracking.Options.MainOptions.WEBAPIUser || Password != Tracking.Options.MainOptions.WEBAPIPassword)
                    //    throw new Exception("Invalid password or login");

                    //PhotoList photos = Database.getBarberPhotos(barber_vk_id, PhotoType.PortfolioMaster);
                    //if (photos == null)
                    //{
                    //    ErrorAnswer answer = new ErrorAnswer("server error");
                    //    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                    //}
                    return ReDoMeAPIResponse.CreateResponse(faceList.ToJson(), HttpStatusCode.OK);
                }
                catch (Exception exc)
                {
                    string Err = $"Error getStarByFace: {exc.Message}";
                    SendLogMessage(Err, System.Diagnostics.EventLogEntryType.Error);
                    ErrorAnswer answer = new ErrorAnswer(exc.Message);
                    return ReDoMeAPIResponse.CreateResponse(answer.ToJson(), HttpStatusCode.OK);
                }
                finally
                {
                    SendLogMessage("ended ReDoMeApi/getStarByFace", System.Diagnostics.EventLogEntryType.SuccessAudit);
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
