using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class ATKTInfo
    {
        public const string PARAM_RECORD_ID = DBConstant.DB_PARAM + "Record_ID";
        public const string PARAM_ATKT_NO = DBConstant.DB_PARAM + "ATKT_No";
        public const string PARAM_PARTY_NAME = DBConstant.DB_PARAM + "Party_Name";
        public const string PARAM_ATKT_DATE = DBConstant.DB_PARAM + "ATKT_Date";
        public const string PARAM_TRAN_TYPE = DBConstant.DB_PARAM + "Tran_Type";
        public const string PARAM_AMOUNT = DBConstant.DB_PARAM + "Amount";
        public const string PARAM_REMARKS = DBConstant.DB_PARAM + "Remarks";
        public const string PARAM_CLOSED = DBConstant.DB_PARAM + "Closed";
        public const string PARAM_CLOSED_DATE = DBConstant.DB_PARAM + "Closed_Date";

        public const string TABLE_NAME = "at_kt";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Record_ID,ATKT_No,Party_Name,ATKT_Date,Tran_Type,Amount,Remarks,Closed,Closed_Date) VALUES (" + PARAM_RECORD_ID + "," + PARAM_ATKT_NO + "," + PARAM_PARTY_NAME + "," + PARAM_ATKT_DATE + "," + PARAM_TRAN_TYPE + "," + PARAM_AMOUNT + "," + PARAM_REMARKS + "," + PARAM_CLOSED + "," + PARAM_CLOSED_DATE + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET ATKT_No=" + PARAM_ATKT_NO + ",Party_Name=" + PARAM_PARTY_NAME + ",ATKT_Date=" + PARAM_ATKT_DATE + ",Tran_Type=" + PARAM_TRAN_TYPE + ",Amount=" + PARAM_AMOUNT + ",Remarks=" + PARAM_REMARKS + ",Closed=" + PARAM_CLOSED + ",Closed_Date=" + PARAM_CLOSED_DATE + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;

        public const string QUERY_SEARCH = "SELECT Record_ID,ATKT_No,Party_Name,ATKT_Date,Tran_Type,Amount,Remarks,Closed,Closed_Date FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Record_ID,ATKT_No,Party_Name,ATKT_Date,Tran_Type,Amount,Remarks,Closed,Closed_Date FROM " + TABLE_NAME + " WHERE Record_ID=" + PARAM_RECORD_ID;
        public const string QUERY_SELECT_ALL = "SELECT Record_ID,ATKT_No,Party_Name,ATKT_Date,Tran_Type,Amount,Remarks,Closed,Closed_Date FROM " + TABLE_NAME;

        public const string QUERY_REPORT_ON_CLOSED = "SELECT Record_ID,ATKT_No,Party_Name,ATKT_Date,Tran_Type,Amount,Remarks,Closed,Closed_Date FROM " + TABLE_NAME + " WHERE ATKT_Date>=" + PARAM_FROM_DATE + " AND ATKT_Date<=" + PARAM_TO_DATE + " AND closed=" + PARAM_CLOSED;
        public const string QUERY_REPORT_ALL = "SELECT Record_ID,ATKT_No,Party_Name,ATKT_Date,Tran_Type,Amount,Remarks,Closed,Closed_Date FROM " + TABLE_NAME + " WHERE ATKT_Date>=" + PARAM_FROM_DATE + " AND ATKT_Date<=" + PARAM_TO_DATE;

        public const string QUERY_CLOSE = "UPDATE " + TABLE_NAME + " SET Closed=" + PARAM_CLOSED + ",Closed_Date=" + PARAM_CLOSED_DATE + " WHERE Record_ID=" + PARAM_RECORD_ID;

        public const string PARAM_FROM_DATE = "@fromDate";
        public const string PARAM_TO_DATE = "@toDate";
        public const string PARAM_TRAN_TYPE2 = DBConstant.DB_PARAM + "Tran_Type2";
        public const string QUERY_ACCOUNT_OPENING_DEBIT = "SELECT COALESCE(SUM(Amount), 0) AS Amount FROM at_kt WHERE (Tran_Type=" + PARAM_TRAN_TYPE2 + " AND ATKT_Date < " + PARAM_ATKT_DATE + ") OR (Tran_Type=" + PARAM_TRAN_TYPE + " AND Closed =" + PARAM_CLOSED + " AND Closed_Date < " + PARAM_CLOSED_DATE + ")";
        public const string QUERY_ACCOUNT_OPENING_CREDIT = "SELECT COALESCE(SUM(Amount), 0) AS Amount FROM at_kt WHERE (Tran_Type=" + PARAM_TRAN_TYPE + " AND  ATKT_Date < " + PARAM_ATKT_DATE + ") OR (Tran_Type=" + PARAM_TRAN_TYPE2 + " AND Closed =" + PARAM_CLOSED + " AND Closed_Date < " + PARAM_CLOSED_DATE + ")";
        
        public const string QUERY_SELECT_PERIOD = "SELECT ATKT_No,Party_Name,ATKT_Date,Tran_Type,Amount,Remarks,Closed,Closed_Date FROM at_kt WHERE (ATKT_Date >= " + PARAM_FROM_DATE + " AND ATKT_Date <= " + PARAM_TO_DATE + ") OR (Closed =" + PARAM_CLOSED + " AND Closed_Date >= " + PARAM_FROM_DATE + " AND Closed_Date <= " + PARAM_TO_DATE+ ")";
        public const string QUERY_SELECT_PERIOD_CLOSED = "SELECT ATKT_No,Party_Name,ATKT_Date,Tran_Type,Amount,Remarks,Closed,Closed_Date FROM at_kt WHERE Closed =" + PARAM_CLOSED + " AND Closed_Date >= " + PARAM_FROM_DATE + " AND Closed_Date <= " + PARAM_TO_DATE;
        
        private int recordID;
        private string aTKTNO;
        private string partyName;
        private DateTime aTKTDate;
        private string tranType;
        private decimal amount;
        private string remarks;
        private string closed;
        private DateTime closedDate;

        private string closedType;

        public int RecordID
        {
            get { return recordID; }
            set { recordID = value; }
        }

        public string ATKTNO
        {
            get { return aTKTNO; }
            set { aTKTNO = value; }
        }

        public string PartyName
        {
            get { return partyName; }
            set { partyName = value; }
        }

        public DateTime ATKTDate
        {
            get { return aTKTDate; }
            set { aTKTDate = value; }
        }

        public string TranType
        {
            get { return tranType; }
            set { tranType = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        public string Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        public DateTime ClosedDate
        {
            get { return closedDate; }
            set { closedDate = value; }
        }

        public string ClosedType
        {
            get { return closedType; }
            set { closedType = value; }
        }

        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_RECORD_ID, recordID));
            parameters.Add(DBManager.GetParameter(PARAM_ATKT_NO, aTKTNO));
            parameters.Add(DBManager.GetParameter(PARAM_PARTY_NAME, partyName));
            parameters.Add(DBManager.GetParameter(PARAM_ATKT_DATE, aTKTDate));
            parameters.Add(DBManager.GetParameter(PARAM_TRAN_TYPE, tranType));
            parameters.Add(DBManager.GetParameter(PARAM_AMOUNT, amount));
            parameters.Add(DBManager.GetParameter(PARAM_REMARKS, remarks));
            parameters.Add(DBManager.GetParameter(PARAM_CLOSED, closed));
            parameters.Add(DBManager.GetParameter(PARAM_CLOSED_DATE, closedDate));

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
                    case "ATKT_No":
                        aTKTNO = DBUtils.ConvertString(reader["ATKT_No"]);
                        break;
                    case "Party_Name":
                        partyName = DBUtils.ConvertString(reader["Party_Name"]);
                        break;
                    case "ATKT_Date":
                        aTKTDate = DBUtils.ConvertDate(reader["ATKT_Date"]);
                        break;
                    case "Tran_Type":
                        tranType = DBUtils.ConvertString(reader["Tran_Type"]);
                        break;
                    case "Amount":
                        amount = DBUtils.ConvertDecimal(reader["Amount"]);
                        break;
                    case "Remarks":
                        remarks = DBUtils.ConvertString(reader["Remarks"]);
                        break;
                    case "Closed":
                        closed = DBUtils.ConvertString(reader["Closed"]);
                        break;
                    case "Closed_Date":
                        closedDate = DBUtils.ConvertDate(reader["Closed_Date"]);
                        break;

                }
            }
        }
    }
}