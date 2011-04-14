using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class ChitsTransInfo
    {
        public const string PARAM_RECORD_ID = DBConstant.DB_PARAM + "Record_ID";
        public const string PARAM_CHIT_NO = DBConstant.DB_PARAM + "Chit_No";
        public const string PARAM_CUSTOMER_ID = DBConstant.DB_PARAM + "Customer_ID";
        public const string PARAM_INSTALLMENT_NO = DBConstant.DB_PARAM + "Installment_No";
        public const string PARAM_DATE = DBConstant.DB_PARAM + "Date";

        public const string TABLE_NAME = "chits_trans";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Record_ID,Chit_No,Customer_ID,Installment_No,Date) VALUES (" + PARAM_RECORD_ID + "," + PARAM_CHIT_NO + "," + PARAM_CUSTOMER_ID + "," + PARAM_INSTALLMENT_NO + "," + PARAM_DATE + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Chit_No=" + PARAM_CHIT_NO + ",Customer_ID=" + PARAM_CUSTOMER_ID + ",Installment_No=" + PARAM_INSTALLMENT_NO + ",Date=" + PARAM_DATE + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;

        public const string QUERY_SEARCH = "SELECT Record_ID,Chit_No,Customer_ID,Installment_No,Date FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Record_ID,Chit_No,Customer_ID,Installment_No,Date FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_SELECT_ALL = "SELECT Record_ID,Chit_No,Customer_ID,Installment_No,Date FROM " + TABLE_NAME + " WHERE Chit_No=" + PARAM_CHIT_NO;

        private int recordID;
        private string chitNO;
        private int customerID;
        private int installmentNO;
        private DateTime date;

        private string customerName;
        private string customerAddress;

        public int RecordID
        {
            get { return recordID; }
            set { recordID = value; }
        }

        public string ChitNO
        {
            get { return chitNO; }
            set { chitNO = value; }
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public int InstallmentNO
        {
            get { return installmentNO; }
            set { installmentNO = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string CustomerAddress
        {
            get { return customerAddress; }
            set { customerAddress = value; }
        }


        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_RECORD_ID, recordID));
            parameters.Add(DBManager.GetParameter(PARAM_CHIT_NO, chitNO));
            parameters.Add(DBManager.GetParameter(PARAM_CUSTOMER_ID, customerID));
            parameters.Add(DBManager.GetParameter(PARAM_INSTALLMENT_NO, installmentNO));
            parameters.Add(DBManager.GetParameter(PARAM_DATE, date));

            return parameters;
        }
        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Record_ID":
                        recordID = DBUtils.ConvertInt(reader["Record_ID"]);
                        break;
                    case "Chit_No":
                        chitNO = DBUtils.ConvertString(reader["Chit_No"]);
                        break;
                    case "Customer_ID":
                        customerID = DBUtils.ConvertInt(reader["Customer_ID"]);
                        break;
                    case "Installment_No":
                        installmentNO = DBUtils.ConvertInt(reader["Installment_No"]);
                        break;
                    case "Date":
                        date = DBUtils.ConvertDate(reader["Date"]);
                        break;

                }
            }
        }
    }
}