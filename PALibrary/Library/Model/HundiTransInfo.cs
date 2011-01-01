using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class HundiTransInfo
    {
        public const string PARAM_RECORD_ID = DBConstant.DB_PARAM + "Record_id";
        public const string PARAM_HL_LOANNO = DBConstant.DB_PARAM + "Hl_loanno";
        public const string PARAM_PAID_DATE = DBConstant.DB_PARAM + "Paid_date";
        public const string PARAM_RECEIPT_NO = DBConstant.DB_PARAM + "Receipt_no";
        public const string PARAM_AMOUNT = DBConstant.DB_PARAM + "Amount";

        public const string TABLE_NAME = "hunditrans";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Record_id,Hl_loanno,Paid_date,Receipt_no,Amount) VALUES (" + PARAM_RECORD_ID + "," + PARAM_HL_LOANNO + "," + PARAM_PAID_DATE + "," + PARAM_RECEIPT_NO + "," + PARAM_AMOUNT + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Hl_loanno=" + PARAM_HL_LOANNO + ",Paid_date=" + PARAM_PAID_DATE + ",Receipt_no=" + PARAM_RECEIPT_NO + ",Amount=" + PARAM_AMOUNT + " WHERE Record_id=" + PARAM_RECORD_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Record_id=" + PARAM_RECORD_ID;

        public const string QUERY_SEARCH = "SELECT Record_id,Hl_loanno,Paid_date,Receipt_no,Amount FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Record_id,Hl_loanno,Paid_date,Receipt_no,Amount FROM " + TABLE_NAME + " WHERE Record_id=" + PARAM_RECORD_ID;
        public const string QUERY_SELECT_ALL = "SELECT Record_id,Hl_loanno,Paid_date,Receipt_no,Amount FROM " + TABLE_NAME + " WHERE Hl_loanno=" + PARAM_HL_LOANNO;

        public const string QUERY_SELECT_OPENING = "SELECT Sum(amount) AS Amount FROM " + TABLE_NAME + " WHERE paid_date<" + PARAM_PAID_DATE;
        public const string QUERY_SELECT_OPENING_CUSTOMER = "SELECT Sum(amount) AS Amount FROM " + TABLE_NAME + " t,hundiloans h,customers c WHERE t.Hl_loanno=h.Hl_loanno AND h.customer_id=c.customer_id AND t.paid_date<" + PARAM_PAID_DATE + " AND c.customer_name=@customer_name";
        public const string QUERY_SELECT_PERIOD = "SELECT t.Paid_date,t.Receipt_no,t.Amount,h.Hl_loanno,c.customer_name FROM hunditrans t,hundiloans h,customers c WHERE t.Hl_loanno=h.Hl_loanno AND h.customer_id=c.customer_id AND t.paid_date>=@fromDate AND t.paid_date<=@toDate";
        public const string QUERY_SELECT_LEDGER = "SELECT t.Paid_date,t.Receipt_no,t.Amount,h.Hl_loanno,c.customer_name FROM hunditrans t,hundiloans h,customers c WHERE t.Hl_loanno=h.Hl_loanno AND h.customer_id=c.customer_id AND t.paid_date>=@fromDate AND t.paid_date<=@toDate AND c.customer_name=@customer_name";

        private int recordID;
        private string hlLoanno;
        private DateTime paidDate;
        private string receiptNO;
        private decimal amount;

        private decimal balance;

        public int RecordID
        {
            get { return recordID; }
            set { recordID = value; }
        }

        public string HlLoanno
        {
            get { return hlLoanno; }
            set { hlLoanno = value; }
        }

        public DateTime PaidDate
        {
            get { return paidDate; }
            set { paidDate = value; }
        }

        public string ReceiptNO
        {
            get { return receiptNO; }
            set { receiptNO = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
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
            parameters.Add(DBManager.GetParameter(PARAM_HL_LOANNO, hlLoanno));
            parameters.Add(DBManager.GetParameter(PARAM_PAID_DATE, paidDate));
            parameters.Add(DBManager.GetParameter(PARAM_RECEIPT_NO, receiptNO));
            parameters.Add(DBManager.GetParameter(PARAM_AMOUNT, amount));

            return parameters;
        }
        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Record_id":
                        recordID = DBUtils.ConvertInt(reader["Record_id"]);
                        break;
                    case "Hl_loanno":
                        hlLoanno = DBUtils.ConvertString(reader["Hl_loanno"]);
                        break;
                    case "Paid_date":
                        paidDate = DBUtils.ConvertDate(reader["Paid_date"]);
                        break;
                    case "Receipt_no":
                        receiptNO = DBUtils.ConvertString(reader["Receipt_no"]);
                        break;
                    case "Amount":
                        amount = DBUtils.ConvertDecimal(reader["Amount"]);
                        break;

                }
            }
        }
    }
}