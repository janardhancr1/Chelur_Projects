using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class HundiLoanInfo
    {
        public const string PARAM_HL_LOANNO = DBConstant.DB_PARAM + "Hl_loanno";
        public const string PARAM_CUSTOMER_ID = DBConstant.DB_PARAM + "Customer_id";
        public const string PARAM_CO_OBLIGENT = DBConstant.DB_PARAM + "Co_obligent";
        public const string PARAM_COOBLIGENT_ADDRESS = DBConstant.DB_PARAM + "Coobligent_address";
        public const string PARAM_LOAN_AMOUNT = DBConstant.DB_PARAM + "Loan_amount";
        public const string PARAM_LOAN_DATE = DBConstant.DB_PARAM + "Loan_date";
        public const string PARAM_CLOSED = DBConstant.DB_PARAM + "Closed";
        public const string PARAM_RATE = DBConstant.DB_PARAM + "Rate";
        public const string PARAM_PAY_MODE = DBConstant.DB_PARAM + "Pay_mode";
        public const string PARAM_CHEQUE_NO = DBConstant.DB_PARAM + "Cheque_no";
        public const string PARAM_BANK_ID = DBConstant.DB_PARAM + "Bank_id";

        public const string TABLE_NAME = "hundiloans";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Hl_loanno,Customer_id,Co_obligent,Coobligent_address,Loan_amount,Loan_date,Closed,Rate,Pay_mode,Cheque_no,Bank_id) VALUES (" + PARAM_HL_LOANNO + "," + PARAM_CUSTOMER_ID + "," + PARAM_CO_OBLIGENT + "," + PARAM_COOBLIGENT_ADDRESS + "," + PARAM_LOAN_AMOUNT + "," + PARAM_LOAN_DATE + "," + PARAM_CLOSED + "," + PARAM_RATE + "," + PARAM_PAY_MODE + "," + PARAM_CHEQUE_NO + "," + PARAM_BANK_ID + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Customer_id=" + PARAM_CUSTOMER_ID + ",Co_obligent=" + PARAM_CO_OBLIGENT + ",Coobligent_address=" + PARAM_COOBLIGENT_ADDRESS + ",Loan_amount=" + PARAM_LOAN_AMOUNT + ",Loan_date=" + PARAM_LOAN_DATE + ",Closed=" + PARAM_CLOSED + ",Rate=" + PARAM_RATE + ",Pay_mode=" + PARAM_PAY_MODE + ",Cheque_no=" + PARAM_CHEQUE_NO + ",Bank_id=" + PARAM_BANK_ID + " WHERE Hl_loanno=" + PARAM_HL_LOANNO;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Hl_loanno=" + PARAM_HL_LOANNO;

        public const string QUERY_SEARCH = "SELECT Hl_loanno,h.Customer_id,Co_obligent,Coobligent_address,Loan_amount,Loan_date,Closed,Rate,Pay_mode,Cheque_no,Bank_id,c.customer_name,c.account_no,(SELECT Sum(amount) FROM hunditrans WHERE Hl_loanno=h.Hl_loanno GROUP BY Hl_loanno)AS paid_amount FROM " + TABLE_NAME + " h, customers c WHERE h.customer_id=c.customer_id ";
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME + " h";

        public const string QUERY_SET_CLOSED = "UPDATE " + TABLE_NAME + " SET closed='y' WHERE Hl_loanno=" + PARAM_HL_LOANNO;
        public const string QUERY_SELECT = "SELECT Hl_loanno,h.Customer_id,Co_obligent,Coobligent_address,Loan_amount,Loan_date,Closed,Rate,Pay_mode,Cheque_no,Bank_id,c.customer_name,c.account_no,(SELECT Sum(amount) FROM hunditrans WHERE Hl_loanno=h.Hl_loanno GROUP BY Hl_loanno)AS paid_amount FROM " + TABLE_NAME + " h, customers c WHERE h.customer_id=c.customer_id AND Hl_loanno=" + PARAM_HL_LOANNO;
        public const string QUERY_SELECT_ALL = "SELECT Hl_loanno,h.Customer_id,Co_obligent,Coobligent_address,Loan_amount,Loan_date,Closed,Rate,Pay_mode,Cheque_no,Bank_id FROM " + TABLE_NAME + " h, customers c WHERE h.customer_id=c.customer_id";

        public const string QUERY_REPORT_ON_CLOSED = "SELECT Hl_loanno,h.Customer_id,Co_obligent,Coobligent_address,Loan_amount,Loan_date,Closed,Rate,Pay_mode,Cheque_no,Bank_id,c.customer_name,c.account_no,(SELECT Sum(amount) FROM hunditrans WHERE Hl_loanno=h.Hl_loanno GROUP BY Hl_loanno)AS paid_amount FROM " + TABLE_NAME + " h, customers c WHERE h.customer_id=c.customer_id AND loan_date>=" + PARAM_FROM_DATE + " AND loan_date<=" + PARAM_TO_DATE + " AND closed=" + PARAM_CLOSED;
        public const string QUERY_REPORT_ALL = "SELECT Hl_loanno,h.Customer_id,Co_obligent,Coobligent_address,Loan_amount,Loan_date,Closed,Rate,Pay_mode,Cheque_no,Bank_id,c.customer_name,c.account_no,(SELECT Sum(amount) FROM hunditrans WHERE Hl_loanno=h.Hl_loanno GROUP BY Hl_loanno)AS paid_amount FROM " + TABLE_NAME + " h, customers c WHERE h.customer_id=c.customer_id AND loan_date>=" + PARAM_FROM_DATE + " AND loan_date<=" + PARAM_TO_DATE;

        public const string PARAM_CUSTOMER_NAME = "@customer_name";
        public const string QUERY_SELECT_OPENING = "SELECT Sum(loan_amount) AS Amount FROM " + TABLE_NAME + " WHERE loan_date<" + PARAM_LOAN_DATE;
        public const string QUERY_SELECT_OPENING_CUSTOMER = "SELECT Sum(loan_amount) AS Amount FROM " + TABLE_NAME + " d, customers c WHERE d.loan_date<" + PARAM_LOAN_DATE + " AND d.customer_id=c.customer_id AND c.customer_name=" + PARAM_CUSTOMER_NAME;
        public const string QUERY_SELECT_OPENING_CASH = "SELECT Sum(loan_amount) AS Amount FROM " + TABLE_NAME + " WHERE loan_date<" + PARAM_LOAN_DATE + " AND pay_mode=" + PARAM_PAY_MODE;
        public const string QUERY_SELECT_OPENING_BANK = "SELECT Sum(loan_amount) AS Amount FROM " + TABLE_NAME + " h,bank b WHERE loan_date<" + PARAM_LOAN_DATE + " AND pay_mode=" + PARAM_PAY_MODE + " AND h.bank_id=b.bank_id AND b.bank_name=" + PARAM_CUSTOMER_NAME;

        public const string PARAM_FROM_DATE = "@fromDate";
        public const string PARAM_TO_DATE = "@toDate";
        public const string QUERY_SELECT_PERIOD = "SELECT Hl_loanno,Loan_amount,Loan_date,Pay_mode,Bank_id,Cheque_no,c.customer_name FROM " + TABLE_NAME + " d, customers c WHERE d.customer_id=c.customer_id AND loan_date>=" + PARAM_FROM_DATE + " AND loan_date<=" + PARAM_TO_DATE + " ORDER BY loan_date";
        public const string QUERY_SELECT_LEDGER = "SELECT Hl_loanno,Loan_amount,Loan_date,Pay_mode,Bank_id,Cheque_no,c.customer_name FROM " + TABLE_NAME + " d, customers c WHERE d.customer_id=c.customer_id AND loan_date>=" + PARAM_FROM_DATE + " AND loan_date<=" + PARAM_TO_DATE + " AND c.customer_name=" + PARAM_CUSTOMER_NAME + " ORDER BY loan_date";

        private string hlLoanno;
        private int customerID;
        private string coObligent;
        private string coobligentAddress;
        private decimal loanAmount;
        private DateTime loanDate;
        private string closed;
        private decimal rate;
        private int payMode;
        private string chequeNO;
        private int bankID;

        private string customerName;
        private string accountNo;
        private string customerAddress;
        private decimal balance;
        private decimal paidAmount;
        private DateTime interestPaid;
        private string bankName;

        public string HlLoanno
        {
            get { return hlLoanno; }
            set { hlLoanno = value; }
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string CoObligent
        {
            get { return coObligent; }
            set { coObligent = value; }
        }

        public string CoobligentAddress
        {
            get { return coobligentAddress; }
            set { coobligentAddress = value; }
        }

        public decimal LoanAmount
        {
            get { return loanAmount; }
            set { loanAmount = value; }
        }

        public DateTime LoanDate
        {
            get { return loanDate; }
            set { loanDate = value; }
        }

        public string Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        public decimal Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        public int PayMode
        {
            get { return payMode; }
            set { payMode = value; }
        }

        public string ChequeNO
        {
            get { return chequeNO; }
            set { chequeNO = value; }
        }

        public int BankID
        {
            get { return bankID; }
            set { bankID = value; }
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
            get { return balance; }
            set { balance = value; }
        }

        public decimal PaidAmount
        {
            get { return paidAmount; }
            set { paidAmount = value; }
        }

        public DateTime InterestPaid
        {
            get { return interestPaid; }
            set { interestPaid = value; }
        }

        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }

        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_HL_LOANNO, hlLoanno));
            parameters.Add(DBManager.GetParameter(PARAM_CUSTOMER_ID, customerID));
            parameters.Add(DBManager.GetParameter(PARAM_CO_OBLIGENT, coObligent));
            parameters.Add(DBManager.GetParameter(PARAM_COOBLIGENT_ADDRESS, coobligentAddress));
            parameters.Add(DBManager.GetParameter(PARAM_LOAN_AMOUNT, loanAmount));
            parameters.Add(DBManager.GetParameter(PARAM_LOAN_DATE, loanDate));
            parameters.Add(DBManager.GetParameter(PARAM_CLOSED, closed));
            parameters.Add(DBManager.GetParameter(PARAM_RATE, rate));
            parameters.Add(DBManager.GetParameter(PARAM_PAY_MODE, payMode));
            parameters.Add(DBManager.GetParameter(PARAM_CHEQUE_NO, chequeNO));
            parameters.Add(DBManager.GetParameter(PARAM_BANK_ID, bankID));

            return parameters;
        }
        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Hl_loanno":
                        hlLoanno = DBUtils.ConvertString(reader["Hl_loanno"]);
                        break;
                    case "Customer_id":
                        customerID = DBUtils.ConvertInt(reader["Customer_id"]);
                        break;
                    case "Co_obligent":
                        coObligent = DBUtils.ConvertString(reader["Co_obligent"]);
                        break;
                    case "Coobligent_address":
                        coobligentAddress = DBUtils.ConvertString(reader["Coobligent_address"]);
                        break;
                    case "Loan_amount":
                        loanAmount = DBUtils.ConvertDecimal(reader["Loan_amount"]);
                        break;
                    case "Loan_date":
                        loanDate = DBUtils.ConvertDate(reader["Loan_date"]);
                        break;
                    case "Closed":
                        closed = DBUtils.ConvertString(reader["Closed"]);
                        break;
                    case "Rate":
                        rate = DBUtils.ConvertDecimal(reader["Rate"]);
                        break;
                    case "Pay_mode":
                        payMode = DBUtils.ConvertInt(reader["Pay_mode"]);
                        break;
                    case "Cheque_no":
                        chequeNO = DBUtils.ConvertString(reader["Cheque_no"]);
                        break;
                    case "Bank_id":
                        bankID = DBUtils.ConvertInt(reader["Bank_id"]);
                        break;
                    case "customer_name":
                        customerName = DBUtils.ConvertString(reader["customer_name"]);
                        break;
                    case "account_no":
                        accountNo = DBUtils.ConvertString(reader["account_no"]);
                        break;
                    case "paid_amount":
                        paidAmount = DBUtils.ConvertDecimal(reader["paid_amount"]);
                        break;
                }
            }
        }
    }
}