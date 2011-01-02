using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class LedgersInfo
    {
        public const string PARAM_LEDGER_ID = DBConstant.DB_PARAM + "Ledger_ID";
        public const string PARAM_LEDGER_NAME = DBConstant.DB_PARAM + "Ledger_Name";
        public const string PARAM_OPENING_BALANCE = DBConstant.DB_PARAM + "Opening_Balance";
        public const string PARAM_BALANCE_TYPE = DBConstant.DB_PARAM + "Balance_Type";
        public const string PARAM_GROUP_ID = DBConstant.DB_PARAM + "Group_ID";

        public const string TABLE_NAME = "ledgers";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Ledger_ID,Ledger_Name,Opening_Balance,Balance_Type,Group_ID) VALUES (" + PARAM_LEDGER_ID + "," + PARAM_LEDGER_NAME + "," + PARAM_OPENING_BALANCE + "," + PARAM_BALANCE_TYPE + "," + PARAM_GROUP_ID + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Ledger_Name=" + PARAM_LEDGER_NAME + ",Opening_Balance=" + PARAM_OPENING_BALANCE + ",Balance_Type=" + PARAM_BALANCE_TYPE + ",Group_ID=" + PARAM_GROUP_ID + " WHERE Ledger_ID=" + PARAM_LEDGER_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Ledger_ID=" + PARAM_LEDGER_ID;

        public const string QUERY_SEARCH = "SELECT Ledger_ID,Ledger_Name,Opening_Balance,Balance_Type,Group_ID FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Ledger_ID,Ledger_Name,Opening_Balance,Balance_Type,Group_ID FROM " + TABLE_NAME + " WHERE Ledger_ID=" + PARAM_LEDGER_ID;
        public const string QUERY_SELECT_NAME = "SELECT Ledger_ID,Ledger_Name,Opening_Balance,Balance_Type,Group_ID FROM " + TABLE_NAME + " WHERE Ledger_Name=" + PARAM_LEDGER_NAME;
        public const string QUERY_SELECT_ALL = "SELECT Ledger_ID,Ledger_Name,Opening_Balance,Balance_Type,a.Group_ID,Group_Name,Sub_Group,Main_Group FROM " + TABLE_NAME + " a,  Groups b WHERE a.Group_ID=b.Group_ID ORDER BY Ledger_Name";

        public const string QUERY_NEXT_AUTO_NUMBER = "SHOW TABLE STATUS LIKE '" + TABLE_NAME + "'";

        private int ledgerID;
        private string ledgerName;
        private decimal openingBalance;
        private string balanceType;
        private int groupID;

        private string groupName;
        private string subgroupName;
        private string mainGroup;

        public int LedgerID
        {
            get { return ledgerID; }
            set { ledgerID = value; }
        }

        public string LedgerName
        {
            get { return ledgerName; }
            set { ledgerName = value; }
        }

        public decimal OpeningBalance
        {
            get { return openingBalance; }
            set { openingBalance = value; }
        }

        public string BalanceType
        {
            get { return balanceType; }
            set { balanceType = value; }
        }

        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        public string SubGroupName
        {
            get { return subgroupName; }
            set { subgroupName = value; }
        }

        public string MainGroup
        {
            get { return mainGroup; }
            set { mainGroup = value; }
        }

        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_LEDGER_ID, ledgerID));
            parameters.Add(DBManager.GetParameter(PARAM_LEDGER_NAME, ledgerName));
            parameters.Add(DBManager.GetParameter(PARAM_OPENING_BALANCE, openingBalance));
            parameters.Add(DBManager.GetParameter(PARAM_BALANCE_TYPE, balanceType));
            parameters.Add(DBManager.GetParameter(PARAM_GROUP_ID, groupID));

            return parameters;
        }
        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Ledger_ID":
                        ledgerID = DBUtils.ConvertInt(reader["Ledger_ID"]);
                        break;
                    case "Ledger_Name":
                        ledgerName = DBUtils.ConvertString(reader["Ledger_Name"]);
                        break;
                    case "Opening_Balance":
                        openingBalance = DBUtils.ConvertDecimal(reader["Opening_Balance"]);
                        break;
                    case "Balance_Type":
                        balanceType = DBUtils.ConvertString(reader["Balance_Type"]);
                        break;
                    case "Group_ID":
                        groupID = DBUtils.ConvertInt(reader["Group_ID"]);
                        break;
                    case "Group_Name":
                        groupName = DBUtils.ConvertString(reader["Group_Name"]);
                        break;
                    case "Main_Group":
                        mainGroup = DBUtils.ConvertString(reader["Main_Group"]);
                        break;
                    case "Sub_Group":
                        subgroupName = DBUtils.ConvertString(reader["Sub_Group"]);
                        break;

                }
            }
        }
    }
}