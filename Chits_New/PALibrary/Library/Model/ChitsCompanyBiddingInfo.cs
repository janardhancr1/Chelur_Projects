using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class ChitsCompanyBiddingInfo
    {
        public const string PARAM_RECORD_ID = DBConstant.DB_PARAM + "Record_Id";
        public const string PARAM_CHIT_NO = DBConstant.DB_PARAM + "Chit_No";
        public const string PARAM_INSTALLMENT_NO = DBConstant.DB_PARAM + "Installment_No";
        public const string PARAM_PAID_AMOUNT = DBConstant.DB_PARAM + "Paid_Amount";
        public const string PARAM_PAID_DATE = DBConstant.DB_PARAM + "Paid_Date";
        public const string PARAM_CUSTOMER_ID = DBConstant.DB_PARAM + "Customer_Id";

        public const string TABLE_NAME = "chits_company_bidding";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Record_Id,Chit_No,Installment_No,Paid_Amount,Paid_Date,Customer_Id) VALUES (" + PARAM_RECORD_ID + "," + PARAM_CHIT_NO + "," + PARAM_INSTALLMENT_NO + "," + PARAM_PAID_AMOUNT + "," + PARAM_PAID_DATE + "," + PARAM_CUSTOMER_ID + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Chit_No=" + PARAM_CHIT_NO + ",Installment_No=" + PARAM_INSTALLMENT_NO + ",Paid_Amount=" + PARAM_PAID_AMOUNT + ",Paid_Date=" + PARAM_PAID_DATE + ",Customer_Id=" + PARAM_CUSTOMER_ID + " WHERE Record_Id=" + PARAM_RECORD_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Record_Id=" + PARAM_RECORD_ID;

        public const string QUERY_SEARCH = "SELECT Record_Id,Chit_No,Installment_No,Paid_Amount,Paid_Date,Customer_Id FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(1) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Record_Id,Chit_No,Installment_No,Paid_Amount,Paid_Date,Customer_Id FROM " + TABLE_NAME + " WHERE Record_Id=" + PARAM_RECORD_ID;
        public const string QUERY_SELECT_ALL = "SELECT Record_Id,Chit_No,Installment_No,Paid_Amount,Paid_Date,Customer_Id FROM " + TABLE_NAME;

        public const string PARAM_FROM_DATE = "@fromDate";
        public const string PARAM_TO_DATE = "@toDate";
        public const string QUERY_SELECT_OPENING_COMPBID = "SELECT Sum(Paid_Amount) AS Amount FROM " + TABLE_NAME + " WHERE Paid_Date<" + PARAM_PAID_DATE;
        public const string QUERY_SELECT_PERIOD_COMPBID = "SELECT Record_ID,b.Chit_No,Installment_No,Paid_Amount,Paid_Date,c.Chit_Name FROM " + TABLE_NAME + " b, chits c WHERE b.Chit_No=c.Chit_No AND Paid_Date>=" + PARAM_FROM_DATE + " AND Paid_Date<=" + PARAM_TO_DATE + " ORDER BY Paid_Date";

        private int recordID;
        private string chitNO;
        private int installmentNO;
        private decimal paidAmount;
        private DateTime paidDate;
        private int customerID;

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


        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_RECORD_ID, recordID));
            parameters.Add(DBManager.GetParameter(PARAM_CHIT_NO, chitNO));
            parameters.Add(DBManager.GetParameter(PARAM_INSTALLMENT_NO, installmentNO));
            parameters.Add(DBManager.GetParameter(PARAM_PAID_AMOUNT, paidAmount));
            parameters.Add(DBManager.GetParameter(PARAM_PAID_DATE, paidDate));
            parameters.Add(DBManager.GetParameter(PARAM_CUSTOMER_ID, customerID));

            return parameters;
        }

        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Record_Id":
                        recordID = DBUtils.ConvertInt(reader["Record_Id"]);
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
                    case "Paid_Date":
                        paidDate = DBUtils.ConvertDate(reader["Paid_Date"]);
                        break;
                    case "Customer_Id":
                        customerID = DBUtils.ConvertInt(reader["Customer_Id"]);
                        break;

                }
            }
        }
    }
}