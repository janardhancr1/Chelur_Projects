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
    class ChitsDAO
    {
        public static void AddChitsInfo(ChitsInfo chitsInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsInfo.QUERY_INSERT, chitsInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsInfo.QUERY_UPDATE, chitsInfo.GetParameters());

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

        public static void DeleteChitsInfo(string chitNO)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_CHIT_NO, chitNO));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(string chitNO, string chitName, decimal chitAmount, int bidDate, decimal installmentAmount, decimal noInstallments, string closed)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                if (chitNO != null)
                {
                    if (chitNO.Trim().Length > 0)
                    {
                        conditions.Add("Chit_No LIKE " + ChitsInfo.PARAM_CHIT_NO);
                        parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_CHIT_NO, chitNO + "%"));
                    }
                }

                if (chitName != null)
                {
                    if (chitName.Trim().Length > 0)
                    {
                        conditions.Add("Chit_Name LIKE " + ChitsInfo.PARAM_CHIT_NAME);
                        parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_CHIT_NAME, chitName + "%"));
                    }
                }

                if (chitAmount > 0)
                {
                    conditions.Add("Chit_Amount = " + ChitsInfo.PARAM_CHIT_AMOUNT);
                    parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_CHIT_AMOUNT, chitAmount));
                }

                if (bidDate > 0)
                {
                    conditions.Add("Bid_Date = " + ChitsInfo.PARAM_BID_DATE);
                    parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_BID_DATE, bidDate));
                }

                if (installmentAmount > 0)
                {
                    conditions.Add("Installment_Amount = " + ChitsInfo.PARAM_INSTALLMENT_AMOUNT);
                    parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_INSTALLMENT_AMOUNT, installmentAmount));
                }

                if (noInstallments > 0)
                {
                    conditions.Add("No_Installments = " + ChitsInfo.PARAM_NO_INSTALLMENTS);
                    parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_NO_INSTALLMENTS, noInstallments));
                }

                if (closed != null)
                {
                    if (closed.Trim().Length > 0)
                    {
                        conditions.Add("Closed LIKE " + ChitsInfo.PARAM_CLOSED);
                        parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_CLOSED, closed + "%"));
                    }
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

        public static List<ChitsInfo> SearchChitsInfo(SearchHelper searchHelper, int startPage)
        {
            List<ChitsInfo> chitsInfos = new List<ChitsInfo>();
            IDataReader reader = null;
            try
            {
                string query = ChitsInfo.QUERY_SEARCH;
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
                    ChitsInfo chitsInfo = new ChitsInfo();
                    chitsInfo.ReadValues(reader);

                    chitsInfos.Add(chitsInfo);
                }
                return chitsInfos;
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

        public static int SearchChitsInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = ChitsInfo.QUERY_COUNT;
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

        public static ChitsInfo GetChitsInfo(string chitNO)
        {
            ChitsInfo chitsInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_CHIT_NO, chitNO));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    chitsInfo = new ChitsInfo();
                    chitsInfo.ReadValues(reader);
                }
                return chitsInfo;
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

        public static List<ChitsInfo> GetChitsInfos()
        {
            List<ChitsInfo> chitsInfos = new List<ChitsInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    ChitsInfo chitsInfo = new ChitsInfo();
                    chitsInfo.ReadValues(reader);

                    chitsInfos.Add(chitsInfo);
                }
                return chitsInfos;
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

        public static List<ChitsInfo> GetChitsInfos(DateTime fromDate, DateTime toDate, string closed)
        {
            List<ChitsInfo> chitsInfos = new List<ChitsInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_FROM_DATE, fromDate));
                parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_TO_DATE, toDate));
                parameters.Add(DBManager.GetParameter(ChitsInfo.PARAM_CLOSED, closed));

                string query = "";
                if (closed.Equals(DBConstant.TYPE_CLOSED) || closed.Equals(DBConstant.TYPE_PENDING))
                    query = ChitsInfo.QUERY_REPORT_ON_CLOSED;
                else if (closed.Equals(DBConstant.TYPE_ALL))
                    query = ChitsInfo.QUERY_REPORT_ALL;

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    ChitsInfo chitsInfo = new ChitsInfo();
                    chitsInfo.ReadValues(reader);

                    chitsInfos.Add(chitsInfo);
                }
                return chitsInfos;
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

        public static List<ChitsInfo> GetChitsMonhlyDueStatement()
        {
            List<ChitsInfo> chitsInfos = GetChitsInfos();

            foreach (ChitsInfo chitInfo in chitsInfos)
            {
                int currentInstallment = ChitsBiddingDAO.GetLastBiddingInstallment(chitInfo.ChitNO);
                List<ChitsBiddingInfo> lastBidding = ChitsBiddingDAO.SearchChitsBiddingInfo(ChitsBiddingDAO.SearchConditions(chitInfo.ChitNO, currentInstallment - 1, 0, new DateTime(), new DateTime(), 0, 0), -1);
                if (lastBidding.Count > 0)
                {
                    decimal comm = chitInfo.ChitAmount * chitInfo.ChitCommission / 100;
                    decimal leftAmount = lastBidding[0].LeftAmount - comm;
                    decimal installAmount = (chitInfo.InstallmentAmount - (leftAmount / chitInfo.NoInstallments));
                    chitInfo.InstallmentAmount = installAmount;
                    chitInfo.NoInstallments = currentInstallment;
                }
            }

            return chitsInfos;
        }

    }
}