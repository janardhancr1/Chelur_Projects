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
        public const string PARAM_INSTALLMENT_AMOUNT = DBConstant.DB_PARAM + "Installment_Amount";
        public const string PARAM_DISCOUNT_AMOUNT = DBConstant.DB_PARAM + "Discount_Amount";
        public const string PARAM_DATE = DBConstant.DB_PARAM + "Date";

        public const string TABLE_NAME = "chits_trans";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Record_ID,Chit_No,Customer_ID,Installment_No,Installment_Amount,Discount_Amount,Date) VALUES (" + PARAM_RECORD_ID + "," + PARAM_CHIT_NO + "," + PARAM_CUSTOMER_ID + "," + PARAM_INSTALLMENT_NO + "," + PARAM_INSTALLMENT_AMOUNT + "," + PARAM_DISCOUNT_AMOUNT + ","  + PARAM_DATE + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Chit_No=" + PARAM_CHIT_NO + ",Customer_ID=" + PARAM_CUSTOMER_ID + ",Installment_No=" + PARAM_INSTALLMENT_NO + ",Installment_Amount=" + PARAM_INSTALLMENT_AMOUNT + ",Discount_Amount=" + PARAM_DISCOUNT_AMOUNT + ",Date=" + PARAM_DATE + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;

        public const string QUERY_SEARCH = "SELECT Record_ID,Chit_No,Customer_ID,Installment_No,Installment_Amount,Discount_Amount,Date FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Record_ID,Chit_No,Customer_ID,Installment_No,Installment_Amount,Discount_Amount,Date FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_SELECT_ALL = "SELECT Record_ID,Chit_No,Customer_ID,Installment_No,Installment_Amount,Discount_Amount,Date FROM " + TABLE_NAME;

        public const string PARAM_FROM_DATE = "@fromDate";
        public const string PARAM_TO_DATE = "@toDate";
        public const string QUERY_SELECT_OPENING = "SELECT Sum(Installment_Amount) AS Amount FROM " + TABLE_NAME + " WHERE Date<" + PARAM_DATE;
        public const string QUERY_SELECT_PERIOD = "SELECT Record_ID,t.Chit_No,t.Customer_ID,Installment_No,t.Installment_Amount,Date,c.Chit_Name,cu.Customer_Name FROM " + TABLE_NAME + " t, chits c, customers cu WHERE t.Chit_No=c.Chit_No AND t.Customer_id = cu.Customer_id AND Date>=" + PARAM_FROM_DATE + " AND Date<=" + PARAM_TO_DATE + " ORDER BY Date";
        //public const string QUERY_SELECT_LEDGER = "SELECT Hl_loanno,Loan_amount,Loan_date,Pay_mode,Bank_id,Cheque_no,c.customer_name FROM " + TABLE_NAME + " d, customers c WHERE d.customer_id=c.customer_id AND loan_date>=" + PARAM_FROM_DATE + " AND loan_date<=" + PARAM_TO_DATE + " AND c.customer_name=" + PARAM_CUSTOMER_NAME + " ORDER BY loan_date";

        private int recordID;
        private string chitNO;
        private int customerID;
        private int installmentNO;
        private decimal installmentAmount;
        private decimal discountAmount;
        private DateTime date;

        private string chitName;
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

        public decimal InstallmentAmount
        {
            get { return installmentAmount; }
            set { installmentAmount = value; }
        }

        public decimal DiscountAmount
        {
            get { return discountAmount; }
            set { discountAmount = value; }
        }
        
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string ChitName
        {
            get { return chitName; }
            set { chitName = value; }
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
            parameters.Add(DBManager.GetParameter(PARAM_INSTALLMENT_AMOUNT, installmentAmount));
            parameters.Add(DBManager.GetParameter(PARAM_DISCOUNT_AMOUNT, discountAmount));
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
                    case "Installment_Amount":
                        installmentAmount = DBUtils.ConvertDecimal(reader["Installment_Amount"]);
                        break;
                    case "Discount_Amount":
                        discountAmount = DBUtils.ConvertDecimal(reader["Discount_Amount"]);
                        break;
                    case "Date":
                        date = DBUtils.ConvertDate(reader["Date"]);
                        break;
                    case "Chit_Name":
                        chitName = DBUtils.ConvertString(reader["Chit_Name"]);
                        break;

                }
            }
        }
    }
}
