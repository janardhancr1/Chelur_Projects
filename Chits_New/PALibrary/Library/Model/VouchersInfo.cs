using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class VouchersInfo
    {
        public const string PARAM_VOUCHER_ID = DBConstant.DB_PARAM + "Voucher_ID";
        public const string PARAM_VOUCHER_DATE = DBConstant.DB_PARAM + "Voucher_Date";
        public const string PARAM_VOUCHER_TYPE = DBConstant.DB_PARAM + "Voucher_Type";
        public const string PARAM_VOUCHER_NO = DBConstant.DB_PARAM + "Voucher_No";
        public const string PARAM_FROM_LEDGER = DBConstant.DB_PARAM + "From_Ledger";
        public const string PARAM_TO_LEDGER = DBConstant.DB_PARAM + "To_Ledger";
        public const string PARAM_AMOUNT = DBConstant.DB_PARAM + "Amount";
        public const string PARAM_NARRATION = DBConstant.DB_PARAM + "Narration";

        public const string TABLE_NAME = "vouchers";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Voucher_ID,Voucher_Date,Voucher_Type,Voucher_No,From_Ledger,To_Ledger,Amount,Narration) VALUES (" + PARAM_VOUCHER_ID + "," + PARAM_VOUCHER_DATE + "," + PARAM_VOUCHER_TYPE + "," + PARAM_VOUCHER_NO + "," + PARAM_FROM_LEDGER + "," + PARAM_TO_LEDGER + "," + PARAM_AMOUNT + "," + PARAM_NARRATION + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Voucher_Date=" + PARAM_VOUCHER_DATE + ",Voucher_Type=" + PARAM_VOUCHER_TYPE + ",Voucher_No=" + PARAM_VOUCHER_NO + ",From_Ledger=" + PARAM_FROM_LEDGER + ",To_Ledger=" + PARAM_TO_LEDGER + ",Amount=" + PARAM_AMOUNT + ",Narration=" + PARAM_NARRATION + " WHERE Voucher_ID=" + PARAM_VOUCHER_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Voucher_ID=" + PARAM_VOUCHER_ID;

        public const string QUERY_SEARCH = "SELECT Voucher_ID,Voucher_Date,Voucher_Type,Voucher_No,From_Ledger,To_Ledger,Amount,Narration FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Voucher_ID,Voucher_Date,Voucher_Type,Voucher_No,From_Ledger,To_Ledger,Amount,Narration FROM " + TABLE_NAME + " WHERE Voucher_ID=" + PARAM_VOUCHER_ID;
        public const string QUERY_SELECT_ALL = "SELECT Voucher_ID,Voucher_Date,Voucher_Type,Voucher_No,From_Ledger,To_Ledger,Amount,Narration FROM " + TABLE_NAME;

        public const string QUERY_NEXT_VOUCHER_NO = "SELECT MAX(Voucher_No) FROM " + TABLE_NAME + " WHERE MONTH(Voucher_Date) = MONTH(" + PARAM_VOUCHER_DATE + ") AND Voucher_Type=" + PARAM_VOUCHER_TYPE;
        public const string QUERY_NEXT_AUTO_NUMBER = "SHOW TABLE STATUS LIKE '" + TABLE_NAME + "'";

        public const string PARAM_FROM_DATE = DBConstant.DB_PARAM + "fromDate";
        public const string PARAM_TO_DATE = DBConstant.DB_PARAM + "toDate";
        public const string PARAM_LEDGER = DBConstant.DB_PARAM + "ledger";

        public const string QUERY_SELECT_OPENING_LEDGER = "SELECT (SELECT Sum(amount) FROM " + TABLE_NAME + " WHERE voucher_date<" + PARAM_VOUCHER_DATE + " AND to_ledger=" + PARAM_LEDGER + ") AS Debit,(SELECT Sum(amount) FROM " + TABLE_NAME + " WHERE voucher_date<" + PARAM_VOUCHER_DATE + " AND from_ledger=" + PARAM_LEDGER + ") AS Crdit";
        public const string QUERY_SELECT_PERIOD = "SELECT Voucher_ID,Voucher_Date,Voucher_Type,Voucher_No,From_Ledger,To_Ledger,Amount,Narration,(SELECT VoucherType_Name FROM voucher_types WHERE VoucherType_ID = Voucher_Type) AS VoucherType_Name, (SELECT Ledger_Name FROM ledgers WHERE Ledger_ID = From_Ledger) AS From_Ledger_Name, (SELECT Ledger_Name FROM ledgers WHERE Ledger_ID = To_Ledger) AS To_Ledger_Name FROM " + TABLE_NAME + " WHERE Voucher_Date>=" + PARAM_FROM_DATE + " AND Voucher_Date<=" + PARAM_TO_DATE;
        public const string QUERY_SELECT_LEDGER = "SELECT Voucher_ID,Voucher_Date,Voucher_Type,Voucher_No,From_Ledger,To_Ledger,Amount,Narration,(SELECT VoucherType_Name FROM voucher_types WHERE VoucherType_ID = Voucher_Type) AS VoucherType_Name, (SELECT Ledger_Name FROM ledgers WHERE Ledger_ID = From_Ledger) AS From_Ledger_Name, (SELECT Ledger_Name FROM ledgers WHERE Ledger_ID = To_Ledger) AS To_Ledger_Name FROM " + TABLE_NAME + " WHERE Voucher_Date>=" + PARAM_FROM_DATE + " AND Voucher_Date<=" + PARAM_TO_DATE + " AND (from_ledger=" + PARAM_LEDGER + " OR to_ledger=" + PARAM_LEDGER + ")";
        

        private int voucherID;
        private DateTime voucherDate;
        private int voucherType;
        private int voucherNO;
        private int fromLedger;
        private int toLedger;
        private decimal amount;
        private string narration;

        private string fromLedgerName;
        private string toLedgerName;
        private string voucherTypeName;

        public int VoucherID
        {
            get { return voucherID; }
            set { voucherID = value; }
        }

        public DateTime VoucherDate
        {
            get { return voucherDate; }
            set { voucherDate = value; }
        }

        public int VoucherType
        {
            get { return voucherType; }
            set { voucherType = value; }
        }

        public int VoucherNO
        {
            get { return voucherNO; }
            set { voucherNO = value; }
        }

        public int FromLedger
        {
            get { return fromLedger; }
            set { fromLedger = value; }
        }

        public int ToLedger
        {
            get { return toLedger; }
            set { toLedger = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string Narration
        {
            get { return narration; }
            set { narration = value; }
        }

        public string FromLedgerName
        {
            get { return fromLedgerName; }
            set { fromLedgerName = value; }
        }

        public string ToLedgerName
        {
            get { return toLedgerName; }
            set { toLedgerName = value; }
        }

        public string VoucherTypeName
        {
            get { return voucherTypeName; }
            set { voucherTypeName = value; }
        }

        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_VOUCHER_ID, voucherID));
            parameters.Add(DBManager.GetParameter(PARAM_VOUCHER_DATE, voucherDate));
            parameters.Add(DBManager.GetParameter(PARAM_VOUCHER_TYPE, voucherType));
            parameters.Add(DBManager.GetParameter(PARAM_VOUCHER_NO, voucherNO));
            parameters.Add(DBManager.GetParameter(PARAM_FROM_LEDGER, fromLedger));
            parameters.Add(DBManager.GetParameter(PARAM_TO_LEDGER, toLedger));
            parameters.Add(DBManager.GetParameter(PARAM_AMOUNT, amount));
            parameters.Add(DBManager.GetParameter(PARAM_NARRATION, narration));

            return parameters;
        }

        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Voucher_ID":
                        voucherID = DBUtils.ConvertInt(reader["Voucher_ID"]);
                        break;
                    case "Voucher_Date":
                        voucherDate = DBUtils.ConvertDate(reader["Voucher_Date"]);
                        break;
                    case "Voucher_Type":
                        voucherType = DBUtils.ConvertInt(reader["Voucher_Type"]);
                        break;
                    case "Voucher_No":
                        voucherNO = DBUtils.ConvertInt(reader["Voucher_No"]);
                        break;
                    case "From_Ledger":
                        fromLedger = DBUtils.ConvertInt(reader["From_Ledger"]);
                        break;
                    case "To_Ledger":
                        toLedger = DBUtils.ConvertInt(reader["To_Ledger"]);
                        break;
                    case "Amount":
                        amount = DBUtils.ConvertDecimal(reader["Amount"]);
                        break;
                    case "Narration":
                        narration = DBUtils.ConvertString(reader["Narration"]);
                        break;
                    case "VoucherType_Name":
                        voucherTypeName = DBUtils.ConvertString(reader["VoucherType_Name"]);
                        break;
                    case "From_Ledger_Name":
                        fromLedgerName = DBUtils.ConvertString(reader["From_Ledger_Name"]);
                        break;
                    case "To_Ledger_Name":
                        toLedgerName = DBUtils.ConvertString(reader["To_Ledger_Name"]);
                        break;

                }
            }
        }
    }
}