using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class ChitsBiddingInfo
    {
        public const string PARAM_RECORD_ID = DBConstant.DB_PARAM + "Record_ID";
        public const string PARAM_CHIT_NO = DBConstant.DB_PARAM + "Chit_No";
        public const string PARAM_INSTALLMENT_NO = DBConstant.DB_PARAM + "Installment_No";
        public const string PARAM_PAID_AMOUNT = DBConstant.DB_PARAM + "Paid_Amount";
        public const string PARAM_BID_DATE = DBConstant.DB_PARAM + "Bid_Date";
        public const string PARAM_PAID_DATE = DBConstant.DB_PARAM + "Paid_Date";
        public const string PARAM_CUSTOMER_ID = DBConstant.DB_PARAM + "Customer_ID";
        public const string PARAM_LEFT_AMOUNT = DBConstant.DB_PARAM + "Left_Amount";

        public const string TABLE_NAME = "chits_bidding";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Record_ID,Chit_No,Installment_No,Paid_Amount,Bid_Date,Paid_Date,Customer_ID,Left_Amount) VALUES (" + PARAM_RECORD_ID + "," + PARAM_CHIT_NO + "," + PARAM_INSTALLMENT_NO + "," + PARAM_PAID_AMOUNT + "," + PARAM_BID_DATE + "," + PARAM_PAID_DATE + "," + PARAM_CUSTOMER_ID + "," + PARAM_LEFT_AMOUNT + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Chit_No=" + PARAM_CHIT_NO + ",Installment_No=" + PARAM_INSTALLMENT_NO + ",Paid_Amount=" + PARAM_PAID_AMOUNT + ",Bid_Date=" + PARAM_BID_DATE + ",Paid_Date=" + PARAM_PAID_DATE + ",Customer_ID=" + PARAM_CUSTOMER_ID + ",Left_Amount=" + PARAM_LEFT_AMOUNT + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;

        public const string QUERY_SEARCH = "SELECT Record_ID,Chit_No,Installment_No,Paid_Amount,Bid_Date,Paid_Date,Customer_ID,Left_Amount FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Record_ID,Chit_No,Installment_No,Paid_Amount,Bid_Date,Paid_Date,Customer_ID,Left_Amount FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_SELECT_ALL = "SELECT Record_ID,Chit_No,Installment_No,Paid_Amount,Bid_Date,Paid_Date,Customer_ID,Left_Amount FROM " + TABLE_NAME + " WHERE Chit_No=" + PARAM_CHIT_NO;

        private int recordID;
        private string chitNO;
        private int installmentNO;
        private decimal paidAmount;
        private DateTime bidDate;
        private DateTime paidDate;
        private int customerID;
        private decimal leftAmount;

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

        public int InstallmentNO
        {
            get { return installmentNO; }
            set { installmentNO = value; }
        }

        public decimal PaidAmount
        {
            get { return paidAmount; }
            set { paidAmount = value; }
        }

        public DateTime BidDate
        {
            get { return bidDate; }
            set { bidDate = value; }
        }

        public DateTime PaidDate
        {
            get { return paidDate; }
            set { paidDate = value; }
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public decimal LeftAmount
        {
            get { return leftAmount; }
            set { leftAmount = value; }
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
            parameters.Add(DBManager.GetParameter(PARAM_INSTALLMENT_NO, installmentNO));
            parameters.Add(DBManager.GetParameter(PARAM_PAID_AMOUNT, paidAmount));
            parameters.Add(DBManager.GetParameter(PARAM_BID_DATE, bidDate));
            parameters.Add(DBManager.GetParameter(PARAM_PAID_DATE, paidDate));
            parameters.Add(DBManager.GetParameter(PARAM_CUSTOMER_ID, customerID));
            parameters.Add(DBManager.GetParameter(PARAM_LEFT_AMOUNT, leftAmount));

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
                    case "Installment_No":
                        installmentNO = DBUtils.ConvertInt(reader["Installment_No"]);
                        break;
                    case "Paid_Amount":
                        paidAmount = DBUtils.ConvertDecimal(reader["Paid_Amount"]);
                        break;
                    case "Bid_Date":
                        bidDate = DBUtils.ConvertDate(reader["Bid_Date"]);
                        break;
                    case "Paid_Date":
                        paidDate = DBUtils.ConvertDate(reader["Paid_Date"]);
                        break;
                    case "Customer_ID":
                        customerID = DBUtils.ConvertInt(reader["Customer_ID"]);
                        break;
                    case "Left_Amount":
                        leftAmount = DBUtils.ConvertDecimal(reader["Left_Amount"]);
                        break;

                }
            }
        }
    }
}