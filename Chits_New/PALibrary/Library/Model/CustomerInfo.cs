using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class CustomerInfo
    {
        public const string PARAM_CUSTOMER_ID = DBConstant.DB_PARAM + "Customer_id";
        public const string PARAM_CUSTOMER_NAME = DBConstant.DB_PARAM + "Customer_name";
        public const string PARAM_SON_HUSBAND = DBConstant.DB_PARAM + "Son_husband";
        public const string PARAM_ACCOUNT_NO = DBConstant.DB_PARAM + "Account_no";
        public const string PARAM_RES_ADDRESS = DBConstant.DB_PARAM + "Res_address";
        public const string PARAM_RES_VILLAGE = DBConstant.DB_PARAM + "Res_village";
        public const string PARAM_RES_PHONE = DBConstant.DB_PARAM + "Res_phone";

        public const string TABLE_NAME = "customers";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Customer_id,Customer_name,Son_husband,Account_no,Res_address,Res_village,Res_phone) VALUES (" + PARAM_CUSTOMER_ID + "," + PARAM_CUSTOMER_NAME + "," + PARAM_SON_HUSBAND + "," + PARAM_ACCOUNT_NO + "," + PARAM_RES_ADDRESS + "," + PARAM_RES_VILLAGE + "," + PARAM_RES_PHONE + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Customer_name=" + PARAM_CUSTOMER_NAME + ",Son_husband=" + PARAM_SON_HUSBAND + ",Account_no=" + PARAM_ACCOUNT_NO + ",Res_address=" + PARAM_RES_ADDRESS + ",Res_village=" + PARAM_RES_VILLAGE + ",Res_phone=" + PARAM_RES_PHONE + " WHERE Customer_id=" + PARAM_CUSTOMER_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Customer_id=" + PARAM_CUSTOMER_ID;

        public const string QUERY_SEARCH = "SELECT Customer_id,Customer_name,Son_husband,Account_no,Res_address,Res_village,Res_phone FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Customer_id,Customer_name,Son_husband,Account_no,Res_address,Res_village,Res_phone FROM " + TABLE_NAME + " WHERE Customer_id=" + PARAM_CUSTOMER_ID;
        public const string QUERY_SELECT_ALL = "SELECT Customer_id,Customer_name,Son_husband,Account_no,Res_address,Res_village,Res_phone FROM " + TABLE_NAME + " ORDER BY Customer_name";
        public const string QUERY_HUNDI_LOAN = "SELECT hl_loanno,loan_amount,loan_date,closed FROM hundiloans WHERE customer_id=" + PARAM_CUSTOMER_ID;

        public const string QUERY_MAX_ACCOUNTNO = "SELECT Max(CAST(account_no AS SIGNED)) FROM " + TABLE_NAME;

        private int customerID;
        private string customerName;
        private string sonHusband;
        private int accountNO;
        private string resAddress;
        private int resVillage;
        private string resPhone;

        private string fullAddress;

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string SonHusband
        {
            get { return sonHusband; }
            set { sonHusband = value; }
        }

        public int AccountNO
        {
            get { return accountNO; }
            set { accountNO = value; }
        }

        public string ResAddress
        {
            get { return resAddress; }
            set { resAddress = value; }
        }

        public int ResVillage
        {
            get { return resVillage; }
            set { resVillage = value; }
        }

        public string ResPhone
        {
            get { return resPhone; }
            set { resPhone = value; }
        }

        public string FullAddress
        {
            get { return fullAddress; }
            set { fullAddress = value; }
        }

        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_CUSTOMER_ID, customerID));
            parameters.Add(DBManager.GetParameter(PARAM_CUSTOMER_NAME, customerName));
            parameters.Add(DBManager.GetParameter(PARAM_SON_HUSBAND, sonHusband));
            parameters.Add(DBManager.GetParameter(PARAM_ACCOUNT_NO, accountNO));
            parameters.Add(DBManager.GetParameter(PARAM_RES_ADDRESS, resAddress));
            parameters.Add(DBManager.GetParameter(PARAM_RES_VILLAGE, resVillage));
            parameters.Add(DBManager.GetParameter(PARAM_RES_PHONE, resPhone));

            return parameters;
        }
        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Customer_id":
                        customerID = DBUtils.ConvertInt(reader["Customer_id"]);
                        break;
                    case "Customer_name":
                        customerName = DBUtils.ConvertString(reader["Customer_name"]);
                        break;
                    case "Son_husband":
                        sonHusband = DBUtils.ConvertString(reader["Son_husband"]);
                        break;
                    case "Account_no":
                        accountNO = DBUtils.ConvertInt(reader["Account_no"]);
                        break;
                    case "Res_address":
                        resAddress = DBUtils.ConvertString(reader["Res_address"]);
                        break;
                    case "Res_village":
                        resVillage = DBUtils.ConvertInt(reader["Res_village"]);
                        break;
                    case "Res_phone":
                        resPhone = DBUtils.ConvertString(reader["Res_phone"]);
                        break;

                }
            }
        }
    }
}