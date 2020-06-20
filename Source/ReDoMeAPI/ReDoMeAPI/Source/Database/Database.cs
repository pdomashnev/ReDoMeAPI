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
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return salon;
        }
        static protected void SendLogMessage(string _Message, System.Diagnostics.EventLogEntryType _Type, Exception _Exception = null)
        {
            Logger.AddMessageToLog(new LogMessage(_Message, "Database", _Type, _Exception));
        }
        //---------------------------------------------
    }
}
