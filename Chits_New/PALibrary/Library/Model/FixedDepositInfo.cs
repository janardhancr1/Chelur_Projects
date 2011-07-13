using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class FixedDepositInfo
    {
        public const string PARAM_FD_NO = DBConstant.DB_PARAM + "FD_No";
        public const string PARAM_CUSTOMER_ID = DBConstant.DB_PARAM + "Customer_ID";
        public const string PARAM_START_DATE = DBConstant.DB_PARAM + "Start_Date";
        public const string PARAM_NOMINEE_NAME = DBConstant.DB_PARAM + "Nominee_Name";
        public const string PARAM_RELATIONSHIP = DBConstant.DB_PARAM + "Relationship";
        public const string PARAM_AMOUNT = DBConstant.DB_PARAM + "Amount";
        public const string PARAM_RATE = DBConstant.DB_PARAM + "Rate";
        public const string PARAM_CLOSED = DBConstant.DB_PARAM + "Closed";

        public const string TABLE_NAME = "fixeddeposit";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(FD_No,Customer_ID,Start_Date,Nominee_Name,Relationship,Amount,Rate,Closed) VALUES (" + PARAM_FD_NO + "," + PARAM_CUSTOMER_ID + "," + PARAM_START_DATE + "," + PARAM_NOMINEE_NAME + "," + PARAM_RELATIONSHIP + "," + PARAM_AMOUNT + "," + PARAM_RATE + "," + PARAM_CLOSED + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Customer_ID=" + PARAM_CUSTOMER_ID + ",Start_Date=" + PARAM_START_DATE + ",Nominee_Name=" + PARAM_NOMINEE_NAME + ",Relationship=" + PARAM_RELATIONSHIP + ",Amount=" + PARAM_AMOUNT + ",Rate=" + PARAM_RATE + ",Closed=" + PARAM_CLOSED + " WHERE FD_No=" + PARAM_FD_NO;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE FD_No=" + PARAM_FD_NO;

        public const string QUERY_SET_CLOSED = "UPDATE " + TABLE_NAME + " SET closed='y' WHERE FD_No=" + PARAM_FD_NO;
        public const string QUERY_SEARCH = "SELECT FD_No,f.Customer_ID,Start_Date,Nominee_Name,Relationship,Amount,Rate,Closed,c.customer_name,c.account_no,(SELECT Sum(amount) FROM FixedTrans WHERE FD_No=f.FD_No GROUP BY FD_No) AS balance FROM " + TABLE_NAME + "  f, customers c WHERE f.customer_id=c.customer_id ";
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME + " f";

        public const string QUERY_SELECT = "SELECT FD_No,f.Customer_ID,Start_Date,Nominee_Name,Relationship,Amount,Rate,Closed,c.customer_name,c.account_no,(SELECT Sum(amount) FROM FixedTrans WHERE FD_No=f.FD_No GROUP BY FD_No) AS balance FROM " + TABLE_NAME + "   f, customers c WHERE f.customer_id=c.customer_id AND FD_No=" + PARAM_FD_NO;
        public const string QUERY_SELECT_ALL = "SELECT FD_No,Customer_ID,Start_Date,Nominee_Name,Relationship,Amount,Rate,Closed FROM " + TABLE_NAME;

        public const string QUERY_REPORT_ON_CLOSED = "SELECT FD_No,f.Customer_ID,Start_Date,Nominee_Name,Relationship,Amount,Rate,Closed,c.customer_name,c.account_no,(SELECT Sum(amount) FROM FixedTrans WHERE FD_No=f.FD_No GROUP BY FD_No) AS balance FROM " + TABLE_NAME + " f, customers c WHERE f.customer_id=c.customer_id AND Start_Date>=" + PARAM_FROM_DATE + " AND Start_Date<=" + PARAM_TO_DATE + " AND closed=" + PARAM_CLOSED;
        public const string QUERY_REPORT_ALL = "SELECT FD_No,f.Customer_ID,Start_Date,Nominee_Name,Relationship,Amount,Rate,Closed,c.customer_name,c.account_no,(SELECT Sum(amount) FROM FixedTrans WHERE FD_No=f.FD_No GROUP BY FD_No) AS balance FROM " + TABLE_NAME + " f, customers c WHERE f.customer_id=c.customer_id AND Start_Date>=" + PARAM_FROM_DATE + " AND Start_Date<=" + PARAM_TO_DATE;

        public const string PARAM_CUSTOMER_NAME = "@customer_name";
        public const string QUERY_SELECT_OPENING = "SELECT Sum(Amount) AS Amount FROM " + TABLE_NAME + " WHERE Start_Date<" + PARAM_START_DATE;
        public const string QUERY_SELECT_OPENING_CUSTOMER = "SELECT Sum(Amount) AS Amount FROM " + TABLE_NAME + " d, customers c WHERE d.Start_Date<" + PARAM_START_DATE + " AND d.customer_id=c.customer_id AND c.customer_name=" + PARAM_CUSTOMER_NAME;

        public const string PARAM_FROM_DATE = "@fromDate";
        public const string PARAM_TO_DATE = "@toDate";
        public const string QUERY_SELECT_PERIOD = "SELECT FD_No,Amount,Start_Date,c.customer_name FROM " + TABLE_NAME + " d, customers c WHERE d.customer_id=c.customer_id AND Start_Date>=" + PARAM_FROM_DATE + " AND Start_Date<=" + PARAM_TO_DATE + " ORDER BY Start_Date";
        public const string QUERY_SELECT_LEDGER = "SELECT Hl_loanno,Amount,Start_Date,c.customer_name FROM " + TABLE_NAME + " d, customers c WHERE d.customer_id=c.customer_id AND Start_Date>=" + PARAM_FROM_DATE + " AND Start_Date<=" + PARAM_TO_DATE + " AND c.customer_name=" + PARAM_CUSTOMER_NAME + " ORDER BY Start_Date";

        private string fDNO;
        private int customerID;
        private DateTime startDate;
        private string nomineeName;
        private string relationship;
        private decimal amount;
        private decimal rate;
        private string closed;

        private string customerName;
        private string accountNo;
        private string customerAddress;
        private decimal balance;
        private DateTime interestPaid;

        public string FDNO
        {
            get { return fDNO; }
            set { fDNO = value; }
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public string NomineeName
        {
            get { return nomineeName; }
            set { nomineeName = value; }
        }

        public string Relationship
        {
            get { return relationship; }
            set { relationship = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public decimal Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        public string Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        public string CustomerAddress
        {
            get { return customerAddress; }
            set { customerAddress = value; }
        }

        public decimal Balance
        {
            get { return amount - balance; }
            set { balance = value; }
        }

        public DateTime InterestPaid
        {
            get { return interestPaid; }
            set { interestPaid = value; }
        }

        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_FD_NO, fDNO));
            parameters.Add(DBManager.GetParameter(PARAM_CUSTOMER_ID, customerID));
            parameters.Add(DBManager.GetParameter(PARAM_START_DATE, startDate));
            parameters.Add(DBManager.GetParameter(PARAM_NOMINEE_NAME, nomineeName));
            parameters.Add(DBManager.GetParameter(PARAM_RELATIONSHIP, relationship));
            parameters.Add(DBManager.GetParameter(PARAM_AMOUNT, amount));
            parameters.Add(DBManager.GetParameter(PARAM_RATE, rate));
            parameters.Add(DBManager.GetParameter(PARAM_CLOSED, closed));

            return parameters;
        }
        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "FD_No":
                        fDNO = DBUtils.ConvertString(reader["FD_No"]);
                        break;
                    case "Customer_ID":
                        customerID = DBUtils.ConvertInt(reader["Customer_ID"]);
                        break;
                    case "Start_Date":
                        startDate = DBUtils.ConvertDate(reader["Start_Date"]);
                        break;
                    case "Nominee_Name":
                        nomineeName = DBUtils.ConvertString(reader["Nominee_Name"]);
                        break;
                    case "Relationship":
                        relationship = DBUtils.ConvertString(reader["Relationship"]);
                        break;
                    case "Amount":
                        amount = DBUtils.ConvertDecimal(reader["Amount"]);
                        break;
                    case "Rate":
                        rate = DBUtils.ConvertDecimal(reader["Rate"]);
                        break;
                    case "Closed":
                        closed = DBUtils.ConvertString(reader["Closed"]);
                        break;
                    case "customer_name":
                        customerName = DBUtils.ConvertString(reader["customer_name"]);
                        break;
                    case "account_no":
                        accountNo = DBUtils.ConvertString(reader["account_no"]);
                        break;
                    case "balance":
                        balance = DBUtils.ConvertDecimal(reader["balance"]);
                        break;
                }
            }
        }
    }
}