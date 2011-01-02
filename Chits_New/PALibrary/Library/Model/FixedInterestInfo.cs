using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class FixedInterestInfo
    {
        public const string PARAM_RECORD_ID = DBConstant.DB_PARAM + "Record_ID";
        public const string PARAM_FD_NO = DBConstant.DB_PARAM + "FD_No";
        public const string PARAM_PAID_DATE = DBConstant.DB_PARAM + "Paid_Date";
        public const string PARAM_INTEREST_AMOUNT = DBConstant.DB_PARAM + "Interest_Amount";
        public const string PARAM_VOUCHER_NO = DBConstant.DB_PARAM + "Voucher_No";
        public const string PARAM_INTEREST_UPTO = DBConstant.DB_PARAM + "Interest_Upto";

        public const string TABLE_NAME = "fixedinterest";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Record_ID,FD_No,Paid_Date,Interest_Amount,Voucher_No,Interest_Upto) VALUES (" + PARAM_RECORD_ID + "," + PARAM_FD_NO + "," + PARAM_PAID_DATE + "," + PARAM_INTEREST_AMOUNT + "," + PARAM_VOUCHER_NO + "," + PARAM_INTEREST_UPTO + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET FD_No=" + PARAM_FD_NO + ",Paid_Date=" + PARAM_PAID_DATE + ",Interest_Amount=" + PARAM_INTEREST_AMOUNT + ",Voucher_No=" + PARAM_VOUCHER_NO + ",Interest_Upto=" + PARAM_INTEREST_UPTO + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;

        public const string QUERY_SEARCH = "SELECT Record_ID,FD_No,Paid_Date,Interest_Amount,Voucher_No,Interest_Upto FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Record_ID,FD_No,Paid_Date,Interest_Amount,Voucher_No,Interest_Upto FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_SELECT_ALL = "SELECT Record_ID,FD_No,Paid_Date,Interest_Amount,Voucher_No,Interest_Upto FROM " + TABLE_NAME + " WHERE FD_No=" + PARAM_FD_NO;

        public const string QUERY_SELECT_OPENING_INTEREST = "SELECT Sum(Interest_Amount) AS Amount FROM " + TABLE_NAME + " WHERE paid_date<" + PARAM_PAID_DATE;
        //public const string QUERY_SELECT_OPENING_INTEREST_CASH = "SELECT Sum(Interest_Amount) AS Amount FROM " + TABLE_NAME + " WHERE paid_date<" + PARAM_PAID_DATE + " AND pay_mode=@pay_mode";
        //public const string QUERY_SELECT_OPENING_INTEREST_BANK = "SELECT Sum(Interest_Amount) AS Amount FROM " + TABLE_NAME + " p,bank b WHERE paid_date<@paid_date AND pay_mode=@pay_mode AND p.bank_id=b.bank_id AND b.bank_name=@customer_name";

        public const string QUERY_SELECT_PERIOD_INTEREST = "SELECT Interest_Amount,Paid_Date,fi.FD_No,Voucher_No,c.customer_name FROM " + TABLE_NAME + " fi, fixeddeposit f, customers c WHERE f.customer_id=c.customer_id AND fi.FD_No=f.FD_No AND paid_date>=@fromDate AND paid_date<=@toDate";
        //public const string QUERY_SELECT_PERIOD_INTEREST = "SELECT amount,interest,voucher_no,paid_date,pay_mode,cheque_no,bank_id,p.pigmy_acno,c.customer_name FROM " + TABLE_NAME + " py, pigmy_account p, customers c WHERE p.customer_id=c.customer_id AND py.pigmy_recordid=p.record_id AND paid_date>=@fromDate AND paid_date<=@toDate";
        //public const string QUERY_SELECT_PERIOD_INTEREST_CASH = "SELECT amount,interest,voucher_no,paid_date,pay_mode,cheque_no,bank_id,p.pigmy_acno,c.customer_name FROM " + TABLE_NAME + " py, pigmy_account p, customers c WHERE p.customer_id=c.customer_id AND py.pigmy_recordid=p.record_id AND paid_date>=@fromDate AND paid_date<=@toDate";
        //public const string QUERY_SELECT_PERIOD_INTEREST_BANK = "SELECT amount,interest,voucher_no,paid_date,pay_mode,cheque_no,bank_id,p.pigmy_acno,c.customer_name FROM " + TABLE_NAME + " py, pigmy_account p, customers c WHERE p.customer_id=c.customer_id AND py.pigmy_recordid=p.record_id AND paid_date>=@fromDate AND paid_date<=@toDate";

        private int recordID;
        private string fDNO;
        private DateTime paidDate;
        private decimal interestAmount;
        private string voucherNO;
        private DateTime interestUpto;

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

        public decimal InterestAmount
        {
            get { return interestAmount; }
            set { interestAmount = value; }
        }

        public string VoucherNO
        {
            get { return voucherNO; }
            set { voucherNO = value; }
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
            parameters.Add(DBManager.GetParameter(PARAM_FD_NO, fDNO));
            parameters.Add(DBManager.GetParameter(PARAM_PAID_DATE, paidDate));
            parameters.Add(DBManager.GetParameter(PARAM_INTEREST_AMOUNT, interestAmount));
            parameters.Add(DBManager.GetParameter(PARAM_VOUCHER_NO, voucherNO));
            parameters.Add(DBManager.GetParameter(PARAM_INTEREST_UPTO, interestUpto));

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
                    case "Interest_Amount":
                        interestAmount = DBUtils.ConvertDecimal(reader["Interest_Amount"]);
                        break;
                    case "Voucher_No":
                        voucherNO = DBUtils.ConvertString(reader["Voucher_No"]);
                        break;
                    case "Interest_Upto":
                        interestUpto = DBUtils.ConvertDate(reader["Interest_Upto"]);
                        break;
                }
            }
        }
    }
}