using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ReDoMeAPI
{
    public class AcceptOfferCommand
    {
        SqlConnection m_Connection;
        SqlCommand m_CreateRecordCommand;
        public AcceptOfferCommand(SqlConnection _Connection)
        {
            m_Connection = _Connection;

            m_CreateRecordCommand = new SqlCommand("AcceptOffer", _Connection);
            m_CreateRecordCommand.CommandType = System.Data.CommandType.StoredProcedure;

            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@REQ_ID", SqlDbType.BigInt));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@OFFER_ID", SqlDbType.BigInt));

            SqlParameter paramRESULT = new SqlParameter
            {
                ParameterName = "@O_RESULT",
                DbType = System.Data.DbType.Boolean,
                SqlDbType = System.Data.SqlDbType.Bit,
                Direction = ParameterDirection.Output
            };
            m_CreateRecordCommand.Parameters.Add(paramRESULT);
        }

        public bool Execute(Int64 _Req_ID, Int64 _Offer_ID)
        {
            m_CreateRecordCommand.Parameters["@REQ_ID"].Value = _Req_ID;
            m_CreateRecordCommand.Parameters["@OFFER_ID"].Value = _Offer_ID;
            m_CreateRecordCommand.ExecuteNonQuery();

            bool bResult = (bool)m_CreateRecordCommand.Parameters["@O_RESULT"].Value;

            return bResult;
        }
    }
}
