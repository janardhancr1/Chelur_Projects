using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.DAO.Helper;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.DAO
{
    class ATKTDAO
    {
        public static void AddATKTInfo(ATKTInfo aTKTInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ATKTInfo.QUERY_INSERT, aTKTInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ATKTInfo.QUERY_UPDATE, aTKTInfo.GetParameters());

                transaction.Commit();
                transaction.Dispose();
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection(connection);
            }
        }

        public static void DeleteATKTInfo(int recordID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_RECORD_ID, recordID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ATKTInfo.QUERY_DELETE, parameters);

                transaction.Commit();
                transaction.Dispose();
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection(connection);
            }
        }

        public static SearchHelper SearchConditions(string aTKTNO, string partyName, DateTime aTKTDate, string tranType, decimal amount, string remarks, string closed, DateTime closedDate)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();

                if (aTKTNO != null)
                {
                    if (aTKTNO.Trim().Length > 0)
                    {
                        conditions.Add("ATKT_No LIKE " + ATKTInfo.PARAM_ATKT_NO);
                        parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_ATKT_NO, aTKTNO + "%"));
                    }
                }

                if (partyName != null)
                {
                    if (partyName.Trim().Length > 0)
                    {
                        conditions.Add("Party_Name LIKE " + ATKTInfo.PARAM_PARTY_NAME);
                        parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_PARTY_NAME, partyName + "%"));
                    }
                }

                if (aTKTDate.Year > 1980)
                {
                    conditions.Add("ATKT_Date = " + ATKTInfo.PARAM_ATKT_DATE);
                    parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_ATKT_DATE, aTKTDate));
                }

                if (tranType != null)
                {
                    if (tranType.Trim().Length > 0)
                    {
                        conditions.Add("Tran_Type LIKE " + ATKTInfo.PARAM_TRAN_TYPE);
                        parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_TRAN_TYPE, tranType + "%"));
                    }
                }

                if (amount > 0)
                {
                    conditions.Add("Amount = " + ATKTInfo.PARAM_AMOUNT);
                    parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_AMOUNT, amount));
                }

                if (remarks != null)
                {
                    if (remarks.Trim().Length > 0)
                    {
                        conditions.Add("Remarks LIKE " + ATKTInfo.PARAM_REMARKS);
                        parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_REMARKS, remarks + "%"));
                    }
                }

                if (closed != null)
                {
                    if (closed.Trim().Length > 0)
                    {
                        conditions.Add("Closed LIKE " + ATKTInfo.PARAM_CLOSED);
                        parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_CLOSED, closed + "%"));
                    }
                }

                if (closedDate.Year > 1980)
                {
                    conditions.Add("Closed_Date = " + ATKTInfo.PARAM_CLOSED_DATE);
                    parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_CLOSED_DATE, closedDate));
                }

                SearchHelper searchHelper = new SearchHelper();
                searchHelper.Conditions = conditions;
                searchHelper.Parameters = parameters;
                return searchHelper;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
        }

        public static List<ATKTInfo> SearchATKTInfo(SearchHelper searchHelper, int startPage)
        {
            List<ATKTInfo> aTKTInfos = new List<ATKTInfo>();
            IDataReader reader = null;
            try
            {
                string query = ATKTInfo.QUERY_SEARCH;
                if (searchHelper.Conditions.Count > 0)
                {
                    query = query + " WHERE " + searchHelper.Conditions[0];
                }
                for (int i = 1; i < searchHelper.Conditions.Count; i++)
                {
                    query = query + " AND " + searchHelper.Conditions[i];
                }

                if (startPage >= 0)
                {
                    int currentPage = DBConstant.PAGE_SIZE * (startPage / DBConstant.PAGE_SIZE);
                    query += " LIMIT " + currentPage + "," + DBConstant.PAGE_SIZE;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, searchHelper.Parameters);
                while (reader.Read())
                {
                    ATKTInfo aTKTInfo = new ATKTInfo();
                    aTKTInfo.ReadValues(reader);
                    aTKTInfo.ClosedType = aTKTInfo.Closed == DBConstant.TYPE_PENDING ? "Pending" : "Closed";
                    aTKTInfos.Add(aTKTInfo);
                }
                return aTKTInfos;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            finally
            {
                DBUtils.CloseReader(reader);
            }
        }

        public static int SearchATKTInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = ATKTInfo.QUERY_COUNT;
                if (searchHelper.Conditions.Count > 0)
                {
                    query = query + " WHERE " + searchHelper.Conditions[0];
                }
                for (int i = 1; i < searchHelper.Conditions.Count; i++)
                {
                    query = query + " AND " + searchHelper.Conditions[i];
                }

                int count = DBUtils.ConvertInt(SQLHelper.ExecuteScalar(CommandType.Text, query, searchHelper.Parameters));
                return count;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
        }

        public static ATKTInfo GetATKTInfo(int recordID)
        {
            ATKTInfo aTKTInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_RECORD_ID, recordID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ATKTInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    aTKTInfo = new ATKTInfo();
                    aTKTInfo.ReadValues(reader);
                }
                return aTKTInfo;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            finally
            {
                DBUtils.CloseReader(reader);
            }
        }

        public static List<ATKTInfo> GetATKTInfos()
        {
            List<ATKTInfo> aTKTInfos = new List<ATKTInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, ATKTInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    ATKTInfo aTKTInfo = new ATKTInfo();
                    aTKTInfo.ReadValues(reader);

                    aTKTInfos.Add(aTKTInfo);
                }
                return aTKTInfos;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            finally
            {
                DBUtils.CloseReader(reader);
            }
        }

        public static void CloseATKT(ATKTInfo aTKTInfo)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ATKTInfo.QUERY_CLOSE, aTKTInfo.GetParameters());

                transaction.Commit();
                transaction.Dispose();
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection(connection);
            }
        }

        public static DayBookInfo GetOpeningATKT(DateTime toDate, string ledgerName, int type)
        {
            DayBookInfo openingBalance = new DayBookInfo();
            openingBalance.Debit = 0;
            openingBalance.Credit = 0;
            IDataReader reader = null;
            IDataReader reader1 = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_ATKT_DATE, toDate));
                parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_CLOSED_DATE, toDate));
                parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_TRAN_TYPE, DBConstant.ATKT_PAY));
                parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_TRAN_TYPE2, DBConstant.ATKT_RECP));
                parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_CLOSED, DBConstant.TYPE_CLOSED));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ATKTInfo.QUERY_ACCOUNT_OPENING_DEBIT, parameters);
                while (reader.Read())
                {
                    openingBalance.Particulars = DBConstant.VOUCHER_ATKTPAY;
                    openingBalance.Credit = DBUtils.ConvertDecimal(reader["Amount"]);
                }

                reader1 = SQLHelper.ExecuteReader(CommandType.Text, ATKTInfo.QUERY_ACCOUNT_OPENING_CREDIT, parameters);
                while (reader1.Read())
                {
                    openingBalance.Particulars = DBConstant.VOUCHER_ATKTRECP;
                    openingBalance.Debit = DBUtils.ConvertDecimal(reader1["Amount"]); 
                }

                return openingBalance;
            }
            catch (PAException pe)
            {
                throw new PAException(pe.Message);
            }
            finally
            {
                DBUtils.CloseReader(reader);
                DBUtils.CloseReader(reader1);
            }
        }

        public static List<ATKTInfo> GetATKTs(DateTime fromDate, DateTime toDate, string customerName, int type)
        {
            List<ATKTInfo> ATKTinfos = new List<ATKTInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_FROM_DATE, fromDate));
                parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_TO_DATE, toDate));
                parameters.Add(DBManager.GetParameter(ATKTInfo.PARAM_CLOSED, DBConstant.TYPE_CLOSED));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ATKTInfo.QUERY_SELECT_PERIOD, parameters);
                while (reader.Read())
                {
                    ATKTInfo ATKTinfo = new ATKTInfo();
                    ATKTinfo.ReadValues(reader);

                    ATKTinfos.Add(ATKTinfo);
                }

                return ATKTinfos;
            }
            catch (PAException pe)
            {
                throw new PAException(pe.Message);
            }
            finally
            {
                DBUtils.CloseReader(reader);
            }
        }
    }
}