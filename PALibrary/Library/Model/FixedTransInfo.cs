using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class FixedTransInfo
    {
        public const string PARAM_RECORD_ID = DBConstant.DB_PARAM + "Record_ID";
        public const string PARAM_FD_NO = DBConstant.DB_PARAM + "FD_No";
        public const string PARAM_PAID_DATE = DBConstant.DB_PARAM + "Paid_Date";
        public const string PARAM_AMOUNT = DBConstant.DB_PARAM + "Amount";
        public const string PARAM_RECEIPT_NO = DBConstant.DB_PARAM + "Receipt_No";

        public const string TABLE_NAME = "fixedtrans";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Record_ID,FD_No,Paid_Date,Amount,Receipt_No) VALUES (" + PARAM_RECORD_ID + "," + PARAM_FD_NO + "," + PARAM_PAID_DATE + "," + PARAM_AMOUNT + "," + PARAM_RECEIPT_NO + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET FD_No=" + PARAM_FD_NO + ",Paid_Date=" + PARAM_PAID_DATE + ",Amount=" + PARAM_AMOUNT + ",Receipt_No=" + PARAM_RECEIPT_NO + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;

        public const string QUERY_SEARCH = "SELECT Record_ID,FD_No,Paid_Date,Amount,Receipt_No FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Record_ID,FD_No,Paid_Date,Amount,Receipt_No FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_SELECT_ALL = "SELECT Record_ID,FD_No,Paid_Date,Amount,Receipt_No FROM " + TABLE_NAME;
        public const string QUERY_SELECT_FD = "SELECT Record_ID,FD_No,Paid_Date,Amount,Receipt_No FROM " + TABLE_NAME + " WHERE FD_No=" + PARAM_FD_NO + " ORDER BY Record_ID";

        public const string QUERY_SELECT_OPENING = "SELECT Sum(amount) AS Amount FROM " + TABLE_NAME + " WHERE paid_date<" + PARAM_PAID_DATE;
        public const string QUERY_SELECT_OPENING_CUSTOMER = "SELECT Sum(amount) AS Amount FROM " + TABLE_NAME + " t,fixeddeposit f,customers c WHERE t.FD_No=f.FD_No AND f.customer_id=c.customer_id AND t.paid_date<" + PARAM_PAID_DATE + " AND c.customer_name=@customer_name";
        public const string QUERY_SELECT_PERIOD = "SELECT t.Paid_date,t.Receipt_no,t.Amount,f.FD_No,c.customer_name FROM fixedtrans t,fixeddeposit f,customers c WHERE t.FD_No=f.FD_No AND f.customer_id=c.customer_id AND t.paid_date>=@fromDate AND t.paid_date<=@toDate";
        public const string QUERY_SELECT_LEDGER = "SELECT t.Paid_date,t.Receipt_no,t.Amount,f.FD_No,c.customer_name FROM fixedtrans t,fixeddeposit f,customers c WHERE t.FD_No=f.FD_No AND f.customer_id=c.customer_id AND t.paid_date>=@fromDate AND t.paid_date<=@toDate AND c.customer_name=@customer_name";

        private int recordID;
        private string fDNO;
        private DateTime paidDate;
        private decimal amount;
        private string receiptNO;
        private decimal balance;

        public int RecordID
        {
            get { return recordID; }
            set { recordID = value; }
        }

        public string FDNO
        {
            get { return fDNO; }
            set { fDNO = value; }
        }

        public DateTime PaidDate
        {
            get { return paidDate; }
            set { paidDate = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string ReceiptNO
        {
            get { return receiptNO; }
            set { receiptNO = value; }
        }

        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_RECORD_ID, recordID));
            parameters.Add(DBManager.GetParameter(PARAM_FD_NO, fDNO));
            parameters.Add(DBManager.GetParameter(PARAM_PAID_DATE, paidDate));
            parameters.Add(DBManager.GetParameter(PARAM_AMOUNT, amount));
            parameters.Add(DBManager.GetParameter(PARAM_RECEIPT_NO, receiptNO));

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
                    case "FD_No":
                        fDNO = DBUtils.ConvertString(reader["FD_No"]);
                        break;
                    case "Paid_Date":
                        paidDate = DBUtils.ConvertDate(reader["Paid_Date"]);
                        break;
                    case "Amount":
                        amount = DBUtils.ConvertDecimal(reader["Amount"]);
                        break;
                    case "Receipt_No":
                        receiptNO = DBUtils.ConvertString(reader["Receipt_No"]);
                        break;

                }
            }
        }
    }
}