using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class ChitsInfo
    {
        public const string PARAM_CHIT_NO = DBConstant.DB_PARAM + "Chit_No";
        public const string PARAM_CHIT_NAME = DBConstant.DB_PARAM + "Chit_Name";
        public const string PARAM_CHIT_AMOUNT = DBConstant.DB_PARAM + "Chit_Amount";
        public const string PARAM_INSTALLMENT_AMOUNT = DBConstant.DB_PARAM + "Installment_Amount";
        public const string PARAM_NO_INSTALLMENTS = DBConstant.DB_PARAM + "No_Installments";
        public const string PARAM_CLOSED = DBConstant.DB_PARAM + "Closed";

        public const string TABLE_NAME = "chits";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(Chit_No,Chit_Name,Chit_Amount,Installment_Amount,No_Installments,Closed) VALUES (" + PARAM_CHIT_NO + "," + PARAM_CHIT_NAME + "," + PARAM_CHIT_AMOUNT + "," + PARAM_INSTALLMENT_AMOUNT + "," + PARAM_NO_INSTALLMENTS + "," + PARAM_CLOSED + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Chit_Name=" + PARAM_CHIT_NAME + ",Chit_Amount=" + PARAM_CHIT_AMOUNT + ",Installment_Amount=" + PARAM_INSTALLMENT_AMOUNT + ",No_Installments=" + PARAM_NO_INSTALLMENTS + ",Closed=" + PARAM_CLOSED + " WHERE Chit_No=" + PARAM_CHIT_NO;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Chit_No=" + PARAM_CHIT_NO;

        public const string QUERY_SEARCH = "SELECT Chit_No,Chit_Name,Chit_Amount,Installment_Amount,No_Installments,Closed FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT Chit_No,Chit_Name,Chit_Amount,Installment_Amount,No_Installments,Closed FROM " + TABLE_NAME + " WHERE Chit_No=" + PARAM_CHIT_NO;
        public const string QUERY_SELECT_ALL = "SELECT Chit_No,Chit_Name,Chit_Amount,Installment_Amount,No_Installments,Closed FROM " + TABLE_NAME;

        private string chitNO;
        private string chitName;
        private decimal chitAmount;
        private decimal installmentAmount;
        private decimal noInstallments;
        private string closed;

        public string ChitNO
        {
            get { return chitNO; }
            set { chitNO = value; }
        }

        public string ChitName
        {
            get { return chitName; }
            set { chitName = value; }
        }

        public decimal ChitAmount
        {
            get { return chitAmount; }
            set { chitAmount = value; }
        }

        public decimal InstallmentAmount
        {
            get { return installmentAmount; }
            set { installmentAmount = value; }
        }

        public decimal NoInstallments
        {
            get { return noInstallments; }
            set { noInstallments = value; }
        }

        public string Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        public string ClosedType
        {
            get { return closed == DBConstant.TYPE_PENDING ? "Pending" : "Closed"; }
        }


        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_CHIT_NO, chitNO));
            parameters.Add(DBManager.GetParameter(PARAM_CHIT_NAME, chitName));
            parameters.Add(DBManager.GetParameter(PARAM_CHIT_AMOUNT, chitAmount));
            parameters.Add(DBManager.GetParameter(PARAM_INSTALLMENT_AMOUNT, installmentAmount));
            parameters.Add(DBManager.GetParameter(PARAM_NO_INSTALLMENTS, noInstallments));
            parameters.Add(DBManager.GetParameter(PARAM_CLOSED, closed));

            return parameters;
        }
        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Chit_No":
                        chitNO = DBUtils.ConvertString(reader["Chit_No"]);
                        break;
                    case "Chit_Name":
                        chitName = DBUtils.ConvertString(reader["Chit_Name"]);
                        break;
                    case "Chit_Amount":
                        chitAmount = DBUtils.ConvertDecimal(reader["Chit_Amount"]);
                        break;
                    case "Installment_Amount":
                        installmentAmount = DBUtils.ConvertDecimal(reader["Installment_Amount"]);
                        break;
                    case "No_Installments":
                        noInstallments = DBUtils.ConvertDecimal(reader["No_Installments"]);
                        break;
                    case "Closed":
                        closed = DBUtils.ConvertString(reader["Closed"]);
                        break;

                }
            }
        }
    }
}