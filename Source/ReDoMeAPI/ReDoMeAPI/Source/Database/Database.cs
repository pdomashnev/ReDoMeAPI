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
        static public int createRequest(Request _request)
        {
            try
            { 
                sqlExpression =
                    "INSERT INTO [RECEPTION_RECORD] ([MEDORG_ID], [REC_REC_ID], [BRA_ID], [WORK_ID], [DOCT_ID], [REC_REC_DATE], [REC_REC_TIME], [REC_REC_DURATION], [DIS_CARD_NUMBER], [REC_REC_GUID], [REC_CONF_URL]) VALUES (@MEDORG_ID, @REC_REC_ID, @BRA_ID, @WORK_ID, @DOCT_ID, @REC_REC_DATE, @REC_REC_TIME, @REC_REC_DURATION, @DIS_CARD_NUMBER, @REC_REC_GUID, @REC_CONF_URL)";
                command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("MEDORG_ID", PostData.MEDORG_ID));
                command.Parameters.Add(new SqlParameter("REC_REC_ID", PostData.POST_Table[i].REC_REC_ID));
                command.Parameters.Add(new SqlParameter("BRA_ID", PostData.POST_Table[i].BRA_ID));
                command.Parameters.Add(new SqlParameter("WORK_ID", PostData.POST_Table[i].WORK_ID));
                command.Parameters.Add(new SqlParameter("DOCT_ID", PostData.POST_Table[i].DOCT_ID));
                command.Parameters.Add(new SqlParameter("REC_REC_DATE", PostData.POST_Table[i].REC_REC_DATE));
                command.Parameters.Add(new SqlParameter("REC_REC_TIME", PostData.POST_Table[i].REC_REC_TIME));
                command.Parameters.Add(new SqlParameter("REC_REC_DURATION", PostData.POST_Table[i].REC_REC_DURATION));
                command.Parameters.Add(new SqlParameter("DIS_CARD_NUMBER", (PostData.POST_Table[i].DIS_CARD_NUMBER != null) ? PostData.POST_Table[i].DIS_CARD_NUMBER : ""));
                param = new SqlParameter("REC_REC_GUID", System.Data.SqlDbType.VarChar, 50);
                param.IsNullable = true;
                if (!String.IsNullOrEmpty(PostData.POST_Table[i].INT_REC_GUID))
                    param.Value = PostData.POST_Table[i].INT_REC_GUID;
                else
                    param.Value = DBNull.Value;
                command.Parameters.Add(param);
                param = new SqlParameter("REC_CONF_URL", System.Data.SqlDbType.VarChar, 1024);
                param.IsNullable = true;
                if (!String.IsNullOrEmpty(PostData.POST_Table[i].REC_CONF_URL))
                    param.Value = PostData.POST_Table[i].REC_CONF_URL;
                else
                    param.Value = DBNull.Value;
                command.Parameters.Add(param);
                command.ExecuteNonQuery();
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

        }
        //---------------------------------------------
        static protected void SendLogMessage(string _Message, System.Diagnostics.EventLogEntryType _Type, Exception _Exception = null)
        {
            Logger.AddMessageToLog(new LogMessage(_Message, "Database", _Type, _Exception));
        }
        //---------------------------------------------
    }
}
