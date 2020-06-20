using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ReDoMeAPI
{
    public class CreateOfferCommand
    {
        SqlConnection m_Connection;
        SqlCommand m_CreateRecordCommand;
        public CreateOfferCommand(SqlConnection _Connection)
        {
            m_Connection = _Connection;

            m_CreateRecordCommand = new SqlCommand("CreateOffer", _Connection);
            m_CreateRecordCommand.CommandType = System.Data.CommandType.StoredProcedure;

            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@REQ_ID", SqlDbType.BigInt));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@BAR_VK_ID", SqlDbType.VarChar, 30));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@SAL_ID", SqlDbType.Int));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@OFFER_COST", SqlDbType.Float));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@OFFER_FOR_DATE", SqlDbType.DateTime));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@OFFER_SELECTED", SqlDbType.Bit));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@OFFER_COMMENT", SqlDbType.VarChar, 1000));

            SqlParameter paramOFFER_ID = new SqlParameter
            {
                ParameterName = "@O_OFFER_ID",
                DbType = System.Data.DbType.Int64,
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };
            m_CreateRecordCommand.Parameters.Add(paramOFFER_ID);
        }

        public Int64 Execute(Offer _Offer)
        {
            m_CreateRecordCommand.Parameters["@REQ_ID"].Value = _Offer.req_id;
            m_CreateRecordCommand.Parameters["@BAR_VK_ID"].Value = _Offer.bar_vk_id;
            if(_Offer.sal_id.HasValue && _Offer.sal_id.Value != 0)
                m_CreateRecordCommand.Parameters["@SAL_ID"].Value = _Offer.sal_id;
            else
                m_CreateRecordCommand.Parameters["@SAL_ID"].Value = DBNull.Value;
            m_CreateRecordCommand.Parameters["@OFFER_COST"].Value = _Offer.cost;
            m_CreateRecordCommand.Parameters["@OFFER_FOR_DATE"].Value = _Offer.date;
            m_CreateRecordCommand.Parameters["@OFFER_SELECTED"].Value = _Offer.selected;
            if (!String.IsNullOrEmpty(_Offer.comment))
                m_CreateRecordCommand.Parameters["@OFFER_COMMENT"].Value = _Offer.comment;
            else
                m_CreateRecordCommand.Parameters["@OFFER_COMMENT"].Value = DBNull.Value;

            m_CreateRecordCommand.ExecuteNonQuery();

            Int64 Offer_ID = (Int64)m_CreateRecordCommand.Parameters["@O_OFFER_ID"].Value;
            _Offer.id = Offer_ID;

            return Offer_ID;
        }
    }
}
