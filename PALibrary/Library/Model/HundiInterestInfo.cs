using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class HundiInterestInfo
    {
        public const string PARAM_RECORD_ID = DBConstant.DB_PARAM + "Record_id";
        public const string PARAM_HL_LOANNO = DBConstant.DB_PARAM + "Hl_loanno";
        public const string PARAM_INTEREST_AMOUNT = DBConstant.DB_PARAM + "Interest_amount";
        public const string PARAM_RECEIPT_NO = DBConstant.DB_PARAM + "Receipt_no";
        public const string PARAM_PAID_DATE = DBConstant.DB_PARAM + "Paid_date";
        public const string PARAM_INTEREST_UPTO = DBConstant.DB_PARAM + "Interest_upto";

        public const string TABLE_NAME = "hundiinterest";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Record_id,Hl_loanno,Interest_amount,Receipt_no,Paid_date,Interest_upto) VALUES (" + PARAM_RECORD_ID + "," + PARAM_HL_LOANNO + "," + PARAM_INTEREST_AMOUNT + "," + PARAM_RECEIPT_NO + "," + PARAM_PAID_DATE + "," + PARAM_INTEREST_UPTO + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Hl_loanno=" + PARAM_HL_LOANNO + ",Interest_amount=" + PARAM_INTEREST_AMOUNT + ",Receipt_no=" + PARAM_RECEIPT_NO + ",Paid_date=" + PARAM_PAID_DATE + ",Interest_upto=" + PARAM_INTEREST_UPTO + " WHERE Record_id=" + PARAM_RECORD_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Record_id=" + PARAM_RECORD_ID;

        public const string QUERY_SEARCH = "SELECT Record_id,Hl_loanno,Interest_amount,Receipt_no,Paid_date,Interest_upto FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Record_id,Hl_loanno,Interest_amount,Receipt_no,Paid_date,Interest_upto FROM " + TABLE_NAME + " WHERE Record_id=" + PARAM_RECORD_ID;
        public const string QUERY_SELECT_ALL = "SELECT Record_id,Hl_loanno,Interest_amount,Receipt_no,Paid_date,Interest_upto FROM " + TABLE_NAME + " WHERE Hl_loanno=" + PARAM_HL_LOANNO;

        public const string QUERY_SELECT_OPENING = "SELECT Sum(interest_amount) AS Amount FROM " + TABLE_NAME + " WHERE paid_date<" + PARAM_PAID_DATE;
        public const string QUERY_SELECT_PERIOD = "SELECT i.Record_id,i.Hl_loanno,i.Paid_date,i.Receipt_no,i.Interest_amount,h.hl_loanno FROM " + TABLE_NAME + " i,hundiloans h WHERE i.hl_loanno=h.hl_loanno AND i.paid_date>=@fromDate AND i.paid_date<=@toDate";

        private int recordID;
        private string hlLoanno;
        private decimal interestAmount;
        private string receiptNO;
        private DateTime paidDate;
        private DateTime interestUpto;

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

        public decimal InterestAmount
        {
            get { return interestAmount; }
            set { interestAmount = value; }
        }

        public string ReceiptNO
        {
            get { return receiptNO; }
            set { receiptNO = value; }
        }

        public DateTime PaidDate
        {
            get { return paidDate; }
            set { paidDate = value; }
        }

        public DateTime InterestUpto
        {
            get { return interestUpto; }
            set { interestUpto = value; }
        }


        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_RECORD_ID, recordID));
            parameters.Add(DBManager.GetParameter(PARAM_HL_LOANNO, hlLoanno));
            parameters.Add(DBManager.GetParameter(PARAM_INTEREST_AMOUNT, interestAmount));
            parameters.Add(DBManager.GetParameter(PARAM_RECEIPT_NO, receiptNO));
            parameters.Add(DBManager.GetParameter(PARAM_PAID_DATE, paidDate));
            parameters.Add(DBManager.GetParameter(PARAM_INTEREST_UPTO, interestUpto));

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
                    case "Interest_amount":
                        interestAmount = DBUtils.ConvertDecimal(reader["Interest_amount"]);
                        break;
                    case "Receipt_no":
                        receiptNO = DBUtils.ConvertString(reader["Receipt_no"]);
                        break;
                    case "Paid_date":
                        paidDate = DBUtils.ConvertDate(reader["Paid_date"]);
                        break;
                    case "Interest_upto":
                        interestUpto = DBUtils.ConvertDate(reader["Interest_upto"]);
                        break;

                }
            }
        }
    }
}