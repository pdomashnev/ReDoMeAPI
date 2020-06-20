using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ReDoMeAPI
{
    public class AddPhotoCommand
    {
        SqlConnection m_Connection;
        SqlCommand m_CreateRecordCommand;
        public AddPhotoCommand(SqlConnection _Connection)
        {
            m_Connection = _Connection;

            m_CreateRecordCommand = new SqlCommand("AddPhoto", _Connection);
            m_CreateRecordCommand.CommandType = System.Data.CommandType.StoredProcedure;

            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@REQ_ID", SqlDbType.BigInt));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@BAR_VK_ID", SqlDbType.VarChar, 30));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@OFFER_ID", SqlDbType.BigInt));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@SAL_ID", SqlDbType.BigInt));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@PHOTO_TYPE", SqlDbType.SmallInt));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@PHOTO_VK_LINK", SqlDbType.VarChar, 1000));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@PHOTO_CONTENT", SqlDbType.VarChar));
            m_CreateRecordCommand.Parameters.Add(new SqlParameter("@PHOTO_COMMENT", SqlDbType.VarChar, 1000));

            SqlParameter paramPHOTO_ID = new SqlParameter
            {
                ParameterName = "@O_PHOTO_ID",
                DbType = System.Data.DbType.Int64,
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };
            m_CreateRecordCommand.Parameters.Add(paramPHOTO_ID);
        }

        public bool AddToRequest(Int64 _Req_ID, Photo _Photo)
        {
            m_CreateRecordCommand.Parameters["@REQ_ID"].Value = _Req_ID;
            m_CreateRecordCommand.Parameters["@BAR_VK_ID"].Value = DBNull.Value;
            m_CreateRecordCommand.Parameters["@OFFER_ID"].Value = DBNull.Value;
            m_CreateRecordCommand.Parameters["@SAL_ID"].Value = DBNull.Value;
            m_CreateRecordCommand.Parameters["@PHOTO_TYPE"].Value = (Int16)_Photo.type;
            if (!String.IsNullOrEmpty(_Photo.vk_link))
                m_CreateRecordCommand.Parameters["@PHOTO_VK_LINK"].Value = _Photo.vk_link;
            else
                m_CreateRecordCommand.Parameters["@PHOTO_VK_LINK"].Value = DBNull.Value;
            if (!String.IsNullOrEmpty(_Photo.content))
                m_CreateRecordCommand.Parameters["@PHOTO_CONTENT"].Value = _Photo.content;
            else
                m_CreateRecordCommand.Parameters["@PHOTO_CONTENT"].Value = DBNull.Value;
            m_CreateRecordCommand.Parameters["@PHOTO_COMMENT"].Value = _Photo.comment;

            m_CreateRecordCommand.ExecuteNonQuery();

            Int64 photo_ID = (Int64)m_CreateRecordCommand.Parameters["@O_PHOTO_ID"].Value;
            _Photo.id = photo_ID;
            return true;
        }
        public bool AddToOffer(Int64 _Offer_ID, Photo _Photo)
        {
            m_CreateRecordCommand.Parameters["@REQ_ID"].Value = DBNull.Value;
            m_CreateRecordCommand.Parameters["@BAR_VK_ID"].Value = DBNull.Value;
            m_CreateRecordCommand.Parameters["@OFFER_ID"].Value = _Offer_ID;
            m_CreateRecordCommand.Parameters["@SAL_ID"].Value = DBNull.Value;
            m_CreateRecordCommand.Parameters["@PHOTO_TYPE"].Value = (Int16)_Photo.type;
            if (!String.IsNullOrEmpty(_Photo.vk_link))
                m_CreateRecordCommand.Parameters["@PHOTO_VK_LINK"].Value = _Photo.vk_link;
            else
                m_CreateRecordCommand.Parameters["@PHOTO_VK_LINK"].Value = DBNull.Value;
            if (!String.IsNullOrEmpty(_Photo.content))
                m_CreateRecordCommand.Parameters["@PHOTO_CONTENT"].Value = _Photo.content;
            else
                m_CreateRecordCommand.Parameters["@PHOTO_CONTENT"].Value = DBNull.Value;
            m_CreateRecordCommand.Parameters["@PHOTO_COMMENT"].Value = _Photo.comment;

            m_CreateRecordCommand.ExecuteNonQuery();

            Int64 photo_ID = (Int64)m_CreateRecordCommand.Parameters["@O_PHOTO_ID"].Value;
            _Photo.id = photo_ID;
            return true;
        }
    }
}
