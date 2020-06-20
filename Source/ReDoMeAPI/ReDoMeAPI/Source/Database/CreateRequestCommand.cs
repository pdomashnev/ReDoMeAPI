using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ReDoMeAPI
{
    public class CreateRequestCommand
    {
        SqlConnection m_Connection;
        SqlCommand m_CreateRecordCommand;
        public CreateRequestCommand(SqlConnection _Connection)
        {
            m_Connection = _Connection;

            m_CreateRecordCommand = new SqlCommand("CreateRequest", _Connection);
            m_CreateRecordCommand.CommandType = System.Data.CommandType.StoredProcedure;

            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@REQ_VK_ID", SqlDbType.VarChar, 30));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@REQ_CLIENTNAME", SqlDbType.VarChar, 50));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@REQ_CITY", SqlDbType.VarChar, 30));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@REQ_TYPE", SqlDbType.SmallInt));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@REQ_STATUS", SqlDbType.SmallInt));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@WORK_SCORE", SqlDbType.SmallInt));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@REQ_COMMENT", SqlDbType.VarChar, 1000));

            SqlParameter paramREQ_ID = new SqlParameter
            {
                ParameterName = "@O_REQ_ID",
                DbType = System.Data.DbType.Int64,
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };
            m_CreateRecordCommand.Parameters.Add(paramREQ_ID);
        }

        public Int64 Execute(Request _Request)
        {
            m_CreateRecordCommand.Parameters["@REQ_VK_ID"].Value = _Request.client_vk_id;
            m_CreateRecordCommand.Parameters["@REQ_CLIENTNAME"].Value = _Request.client_name;
            m_CreateRecordCommand.Parameters["@REQ_CITY"].Value = _Request.city;
            m_CreateRecordCommand.Parameters["@REQ_TYPE"].Value = (Int16)_Request.type;
            m_CreateRecordCommand.Parameters["@REQ_STATUS"].Value = (Int16)_Request.state;
            if(_Request.score.HasValue)
                m_CreateRecordCommand.Parameters["@WORK_SCORE"].Value = _Request.score;
            else
                m_CreateRecordCommand.Parameters["@WORK_SCORE"].Value = DBNull.Value;
            if(!String.IsNullOrEmpty(_Request.comment))
                m_CreateRecordCommand.Parameters["@REQ_COMMENT"].Value = _Request.comment;
            else
                m_CreateRecordCommand.Parameters["@REQ_COMMENT"].Value = DBNull.Value;

            m_CreateRecordCommand.ExecuteNonQuery();

            Int64 Req_ID = (Int64)m_CreateRecordCommand.Parameters["@O_REQ_ID"].Value;
            _Request.id = Req_ID;

            return Req_ID;
        }
    }
}
