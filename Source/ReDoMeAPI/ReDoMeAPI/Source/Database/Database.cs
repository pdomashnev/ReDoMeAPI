using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ReDoMeAPI
{
    public class Database
    {
        //---------------------------------------------
        static public Salon getSalonByBarber(string _barber_vk_id)
        {
            Salon salon = null;
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                //string sqlExpression = "SELECT B.[BRA_ID], B.[BRA_NAME] FROM [WORKER_DOCTOR] DW, [WORKER_BRANCH] WB, [BRANCH] B WHERE DW.[DOCT_ID] = @DOCT_ID AND DW.[WORK_ID] = WB.[WORK_ID] AND WB.[BRA_ID] = B.[BRA_ID] AND DW.[MEDORG_ID] = @MEDORG_ID AND DW.[MEDORG_ID] = B.[MEDORG_ID] AND WB.[MEDORG_ID] = B.[MEDORG_ID] AND [TIME_PER_ID] IS NOT NULL GROUP BY B.[BRA_ID], B.[BRA_NAME] ";
                string sqlExpression =
                    @"SELECT s.Sal_ID, Sal_VK_ID, Sal_Name, Sal_City, Sal_Address, Sal_Phone, Sal_Raiting
                          FROM Salon s inner join
	                        Barber b on s.Sal_ID = b.Sal_ID
	                        WHERE b.Bar_VK_ID = @BAR_VK_ID";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("BAR_VK_ID", _barber_vk_id));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        salon = new Salon();
                        salon.id = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            salon.vk_id = reader.GetString(1);
                        salon.name = reader.GetString(2);
                        salon.city = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            salon.address = reader.GetString(4);
                        salon.phone = reader.GetString(5);
                        salon.raiting = reader.GetInt32(6);
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                return null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return salon;
        }
        //---------------------------------------------
        static public Salon getSalonByID(int _id)
        {
            Salon salon = null;
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                //string sqlExpression = "SELECT B.[BRA_ID], B.[BRA_NAME] FROM [WORKER_DOCTOR] DW, [WORKER_BRANCH] WB, [BRANCH] B WHERE DW.[DOCT_ID] = @DOCT_ID AND DW.[WORK_ID] = WB.[WORK_ID] AND WB.[BRA_ID] = B.[BRA_ID] AND DW.[MEDORG_ID] = @MEDORG_ID AND DW.[MEDORG_ID] = B.[MEDORG_ID] AND WB.[MEDORG_ID] = B.[MEDORG_ID] AND [TIME_PER_ID] IS NOT NULL GROUP BY B.[BRA_ID], B.[BRA_NAME] ";
                string sqlExpression =
                    @"SELECT s.Sal_ID, Sal_VK_ID, Sal_Name, Sal_City, Sal_Address, Sal_Phone, Sal_Raiting
                          FROM Salon s
	                        WHERE s.Sal_ID = @SAL_ID";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("SAL_ID", _id));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        salon = new Salon();
                        salon.id = reader.GetInt32(0);
                        if (!reader.IsDBNull(1))
                            salon.vk_id = reader.GetString(1);
                        salon.name = reader.GetString(2);
                        salon.city = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            salon.address = reader.GetString(4);
                        salon.phone = reader.GetString(5);
                        salon.raiting = reader.GetInt32(6);
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                return null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return salon;
        }
        //---------------------------------------------
        static public Barber getBarber(string _barber_vk_id)
        {
            Barber barber = null;
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                //string sqlExpression = "SELECT B.[BRA_ID], B.[BRA_NAME] FROM [WORKER_DOCTOR] DW, [WORKER_BRANCH] WB, [BRANCH] B WHERE DW.[DOCT_ID] = @DOCT_ID AND DW.[WORK_ID] = WB.[WORK_ID] AND WB.[BRA_ID] = B.[BRA_ID] AND DW.[MEDORG_ID] = @MEDORG_ID AND DW.[MEDORG_ID] = B.[MEDORG_ID] AND WB.[MEDORG_ID] = B.[MEDORG_ID] AND [TIME_PER_ID] IS NOT NULL GROUP BY B.[BRA_ID], B.[BRA_NAME] ";
                string sqlExpression =
                    @"SELECT bar_vk_id, sal_id, bar_name, bar_spec,
                        bar_city, bar_address, bar_phone, bar_about, bar_certs,
                        bar_raiting
                        FROM barber
                        WHERE bar_vk_id = @BAR_VK_ID";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("BAR_VK_ID", _barber_vk_id));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        barber = new Barber();
                        barber.vk_id = reader.GetString(0);
                        barber.sal_id = reader.GetInt32(1);
                        barber.name = reader.GetString(2);
                        barber.spec = reader.GetString(3);
                        barber.city = reader.GetString(4);
                        if (!reader.IsDBNull(5))
                            barber.address = reader.GetString(5);
                        if (!reader.IsDBNull(6))
                            barber.phone = reader.GetString(6);
                        if (!reader.IsDBNull(7))
                            barber.about = reader.GetString(7);
                        if (!reader.IsDBNull(8))
                            barber.certs = reader.GetString(8);
                        barber.raiting = reader.GetInt32(9);
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                return null;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return barber;
        }
        //---------------------------------------------
        static public PhotoList getSalonPhotos(int _sal_id, PhotoType _photoType)
        {
            PhotoList photoList = new PhotoList();
            photoList.listType = _photoType;
            photoList.items = new List<Photo>();
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                //string sqlExpression = "SELECT B.[BRA_ID], B.[BRA_NAME] FROM [WORKER_DOCTOR] DW, [WORKER_BRANCH] WB, [BRANCH] B WHERE DW.[DOCT_ID] = @DOCT_ID AND DW.[WORK_ID] = WB.[WORK_ID] AND WB.[BRA_ID] = B.[BRA_ID] AND DW.[MEDORG_ID] = @MEDORG_ID AND DW.[MEDORG_ID] = B.[MEDORG_ID] AND WB.[MEDORG_ID] = B.[MEDORG_ID] AND [TIME_PER_ID] IS NOT NULL GROUP BY B.[BRA_ID], B.[BRA_NAME] ";
                string sqlExpression =
                    @"select photo_id, Photo_Goal, Photo_VK_Link, Photo_Content, Photo_Comment
                            from photo p
                            where p.sal_id = @SAL_ID AND
	                            p.photo_goal = @PHOTO_TYPE";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("SAL_ID", _sal_id));
                command.Parameters.Add(new SqlParameter("PHOTO_TYPE", (int)_photoType));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Photo item = new Photo();
                        item.id = reader.GetInt64(0);
                        item.type = (PhotoType) reader.GetInt16(1);
                        if (!reader.IsDBNull(2))
                            item.vk_link = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            item.content = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            item.comment = reader.GetString(4);
                        photoList.items.Add(item);
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                throw e;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return photoList;
        }
        //---------------------------------------------
        static public PhotoList getBarberPhotos(string _barber_vk_id, PhotoType _photoType)
        {
            PhotoList photoList = new PhotoList();
            photoList.listType = _photoType;
            photoList.items = new List<Photo>();
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                //string sqlExpression = "SELECT B.[BRA_ID], B.[BRA_NAME] FROM [WORKER_DOCTOR] DW, [WORKER_BRANCH] WB, [BRANCH] B WHERE DW.[DOCT_ID] = @DOCT_ID AND DW.[WORK_ID] = WB.[WORK_ID] AND WB.[BRA_ID] = B.[BRA_ID] AND DW.[MEDORG_ID] = @MEDORG_ID AND DW.[MEDORG_ID] = B.[MEDORG_ID] AND WB.[MEDORG_ID] = B.[MEDORG_ID] AND [TIME_PER_ID] IS NOT NULL GROUP BY B.[BRA_ID], B.[BRA_NAME] ";
                string sqlExpression =
                    @"select photo_id, Photo_Goal, Photo_VK_Link, Photo_Content, Photo_Comment
                            from photo p
                            where p.Bar_VK_ID = @BAR_VK_ID AND
	                            p.photo_goal = @PHOTO_TYPE";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("BAR_VK_ID", _barber_vk_id));
                command.Parameters.Add(new SqlParameter("PHOTO_TYPE", (int)_photoType));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Photo item = new Photo();
                        item.id = reader.GetInt64(0);
                        item.type = (PhotoType)reader.GetInt16(1);
                        if (!reader.IsDBNull(2))
                            item.vk_link = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            item.content = reader.GetString(3);
                        if (!reader.IsDBNull(4))
                            item.comment = reader.GetString(4);
                        photoList.items.Add(item);
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                throw e;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return photoList;
        }
        //---------------------------------------------
        static public Int64 createRequest(Request _request)
        {
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                CreateRequestCommand command = new CreateRequestCommand(connection);
                Int64 req_id = command.Execute(_request);
                connection.Close();
                return req_id;
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                throw e;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }
        //---------------------------------------------
        static public bool addPhotosToRequest(Int64 _req_ID, PhotoList _photos)
        {
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                AddPhotoCommand command = new AddPhotoCommand(connection);
                foreach (Photo photo in _photos.items)
                {
                    command.AddToRequest(_req_ID, photo);
                }
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                throw e;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }
        //---------------------------------------------
        static public Int64 createOffer(Offer _offer)
        {
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                CreateOfferCommand command = new CreateOfferCommand(connection);
                Int64 offer_id = command.Execute(_offer);
                connection.Close();
                return offer_id;
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                throw e;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }
        //---------------------------------------------
        static public bool addPhotosToOffer(Int64 _offer_ID, PhotoList _photos)
        {
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                AddPhotoCommand command = new AddPhotoCommand(connection);
                foreach (Photo photo in _photos.items)
                {
                    command.AddToOffer(_offer_ID, photo);
                }
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                throw e;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }
        //---------------------------------------------
        static public OfferList getOffersForRequest(int _req_id)
        {
            OfferList offerList = new OfferList();
            offerList.items = new List<Offer>();
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                //string sqlExpression = "SELECT B.[BRA_ID], B.[BRA_NAME] FROM [WORKER_DOCTOR] DW, [WORKER_BRANCH] WB, [BRANCH] B WHERE DW.[DOCT_ID] = @DOCT_ID AND DW.[WORK_ID] = WB.[WORK_ID] AND WB.[BRA_ID] = B.[BRA_ID] AND DW.[MEDORG_ID] = @MEDORG_ID AND DW.[MEDORG_ID] = B.[MEDORG_ID] AND WB.[MEDORG_ID] = B.[MEDORG_ID] AND [TIME_PER_ID] IS NOT NULL GROUP BY B.[BRA_ID], B.[BRA_NAME] ";
                string sqlExpression =
                    @"SELECT offer_id, req_id, bar_vk_id, sal_id, offer_cost, offer_fordate, offer_selected, offer_comment
                            FROM offer
                            WHERE req_id = @REQ_ID";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("REQ_ID", _req_id));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Offer item = new Offer();
                        item.id = reader.GetInt64(0);
                        item.req_id= reader.GetInt64(1);
                        if (!reader.IsDBNull(2))
                            item.bar_vk_id = reader.GetString(2);
                        if (!reader.IsDBNull(3))
                            item.sal_id = reader.GetInt32(3);
                        item.cost = reader.GetDouble(4);
                        item.date = reader.GetDateTime(5);
                        item.selected = reader.GetBoolean(6);
                        if (!reader.IsDBNull(7))
                            item.comment = reader.GetString(7);
                        offerList.items.Add(item);
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                throw e;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return offerList;
        }
        //---------------------------------------------
        static public bool acceptOffer(Int64 _Req_ID, Int64 _offer_ID)
        {
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                AcceptOfferCommand command = new AcceptOfferCommand(connection);
                bool bResult = command.Execute(_Req_ID, _offer_ID);
                connection.Close();
                return bResult;
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                throw e;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }
        //---------------------------------------------
        static public RequestList getActiveRequests()
        {
            RequestList requestList = new RequestList();
            requestList.items = new List<Request>();
            SqlConnection connection = new SqlConnection(Options.MainOptions.ConnectionString);
            try
            {
                connection.Open();
                //string sqlExpression = "SELECT B.[BRA_ID], B.[BRA_NAME] FROM [WORKER_DOCTOR] DW, [WORKER_BRANCH] WB, [BRANCH] B WHERE DW.[DOCT_ID] = @DOCT_ID AND DW.[WORK_ID] = WB.[WORK_ID] AND WB.[BRA_ID] = B.[BRA_ID] AND DW.[MEDORG_ID] = @MEDORG_ID AND DW.[MEDORG_ID] = B.[MEDORG_ID] AND WB.[MEDORG_ID] = B.[MEDORG_ID] AND [TIME_PER_ID] IS NOT NULL GROUP BY B.[BRA_ID], B.[BRA_NAME] ";
                string sqlExpression =
                    @"SELECT *
                            FROM
                            (
                            SELECT r.req_id, req_vk_id, req_clientname, req_city, req_type, req_status, work_score, req_comment, ISNULL(o.offer_selected, 0) offer
                            FROM Request r left join
	                            (
		                            SELECT offer_id, req_id, Offer_Selected
			                            FROM Offer o 
			                            WHERE o.Offer_Selected = 1
	                            ) o on r.req_id = o.req_id
                            ) A
                            WHERE offer = 0";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Request item = new Request();
                        item.id = reader.GetInt64(0);
                        if (!reader.IsDBNull(1))
                            item.client_vk_id = reader.GetString(1);
                        item.client_name = reader.GetString(2);
                        item.city = reader.GetString(3);
                        item.type = (RequestType)reader.GetInt16(4);
                        item.state = (ReqeustState)reader.GetInt16(5);
                        if (!reader.IsDBNull(6))
                            item.score = reader.GetInt16(6);
                        if (!reader.IsDBNull(7))
                            item.comment = reader.GetString(7);
                        requestList.items.Add(item);
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                SendLogMessage(e.Message, System.Diagnostics.EventLogEntryType.Error, e);
                throw e;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return requestList;
        }
        //---------------------------------------------
        static protected void SendLogMessage(string _Message, System.Diagnostics.EventLogEntryType _Type, Exception _Exception = null)
        {
            Logger.AddMessageToLog(new LogMessage(_Message, "Database", _Type, _Exception));
        }
        //---------------------------------------------
    }
}
