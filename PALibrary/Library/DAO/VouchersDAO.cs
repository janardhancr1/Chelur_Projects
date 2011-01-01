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
    class VouchersDAO
    {
        public static string GetNextVoucherID()
        {
            string nextVoucherID = "";
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, VouchersInfo.QUERY_NEXT_AUTO_NUMBER, null);
                while (reader.Read())
                {
                    nextVoucherID = DBUtils.ConvertString(reader["AUTO_INCREMENT"]);
                }
                return nextVoucherID;
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

        public static void AddVouchersInfo(VouchersInfo vouchersInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, VouchersInfo.QUERY_INSERT, vouchersInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, VouchersInfo.QUERY_UPDATE, vouchersInfo.GetParameters());

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

        public static void DeleteVouchersInfo(int voucherID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_VOUCHER_ID, voucherID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, VouchersInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(DateTime voucherDate, int voucherType, int voucherNO, int fromLedger, int toLedger, string narration)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                
                if (voucherDate.Year > 1980)
                {
                    conditions.Add("Voucher_Date = " + VouchersInfo.PARAM_VOUCHER_DATE);
                    parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_VOUCHER_DATE, voucherDate));
                }

                if (voucherType > 0)
                {
                    conditions.Add("Voucher_Type = " + VouchersInfo.PARAM_VOUCHER_TYPE);
                    parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_VOUCHER_TYPE, voucherType));
                }

                if (voucherNO > 0)
                {
                    conditions.Add("Voucher_No = " + VouchersInfo.PARAM_VOUCHER_NO);
                    parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_VOUCHER_NO, voucherNO));
                }

                if (fromLedger > 0)
                {
                    conditions.Add("From_Ledger = " + VouchersInfo.PARAM_FROM_LEDGER);
                    parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_FROM_LEDGER, fromLedger));
                }

                if (toLedger > 0)
                {
                    conditions.Add("To_Ledger = " + VouchersInfo.PARAM_TO_LEDGER);
                    parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_TO_LEDGER, toLedger));
                }

                if (narration != null)
                {
                    if (narration.Trim().Length > 0)
                    {
                        conditions.Add("Narration LIKE " + VouchersInfo.PARAM_NARRATION);
                        parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_NARRATION, narration + "%"));
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

        public static List<VouchersInfo> SearchVouchersInfo(SearchHelper searchHelper, int startPage)
        {
            List<VouchersInfo> vouchersInfos = new List<VouchersInfo>();
            IDataReader reader = null;
            try
            {
                string query = VouchersInfo.QUERY_SEARCH;
                if (searchHelper.Conditions.Count > 0)
                {
                    query = query + " WHERE " + searchHelper.Conditions[0];
                }
                for (int i = 1; i < searchHelper.Conditions.Count; i++)
                {
                    query = query + " AND " + searchHelper.Conditions[i];
                }
                query = query + " ORDER BY Voucher_Date DESC,Voucher_No DESC ";

                if (startPage >= 0)
                {
                    int currentPage = DBConstant.PAGE_SIZE * (startPage / DBConstant.PAGE_SIZE);
                    query += " LIMIT " + currentPage + "," + DBConstant.PAGE_SIZE;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, searchHelper.Parameters);
                while (reader.Read())
                {
                    VouchersInfo vouchersInfo = new VouchersInfo();
                    vouchersInfo.ReadValues(reader);
                    vouchersInfo.FromLedgerName = LedgersDAO.GetLedgersInfo(vouchersInfo.FromLedger).LedgerName;
                    vouchersInfo.ToLedgerName = LedgersDAO.GetLedgersInfo(vouchersInfo.ToLedger).LedgerName;
                    vouchersInfo.VoucherTypeName = AccountsDAO.GetVoucherTypeName(vouchersInfo.VoucherType);

                    vouchersInfos.Add(vouchersInfo);
                }
                return vouchersInfos;
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

        public static int SearchVouchersInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = VouchersInfo.QUERY_COUNT;
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

        public static VouchersInfo GetVouchersInfo(int voucherID)
        {
            VouchersInfo vouchersInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_VOUCHER_ID, voucherID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, VouchersInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    vouchersInfo = new VouchersInfo();
                    vouchersInfo.ReadValues(reader);
                    vouchersInfo.FromLedgerName = LedgersDAO.GetLedgersInfo(vouchersInfo.FromLedger).LedgerName;
                    vouchersInfo.ToLedgerName = LedgersDAO.GetLedgersInfo(vouchersInfo.ToLedger).LedgerName;
                    vouchersInfo.VoucherTypeName = AccountsDAO.GetVoucherTypeName(vouchersInfo.VoucherType);
                }
                return vouchersInfo;
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

        public static List<VouchersInfo> GetVouchersInfos()
        {
            List<VouchersInfo> vouchersInfos = new List<VouchersInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, VouchersInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    VouchersInfo vouchersInfo = new VouchersInfo();
                    vouchersInfo.ReadValues(reader);
                    vouchersInfo.FromLedgerName = LedgersDAO.GetLedgersInfo(vouchersInfo.FromLedger).LedgerName;
                    vouchersInfo.ToLedgerName = LedgersDAO.GetLedgersInfo(vouchersInfo.ToLedger).LedgerName;
                    vouchersInfo.VoucherTypeName = AccountsDAO.GetVoucherTypeName(vouchersInfo.VoucherType);

                    vouchersInfos.Add(vouchersInfo);
                }
                return vouchersInfos;
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

        public static List<VouchersInfo> GetVouchers(DateTime fromDate, DateTime toDate, int ledgerID)
        {
            List<VouchersInfo> vouchers = new List<VouchersInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_FROM_DATE, fromDate));
                parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_TO_DATE, toDate));
                parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_LEDGER, ledgerID));

                string query = ledgerID > 0 ? VouchersInfo.QUERY_SELECT_LEDGER : VouchersInfo.QUERY_SELECT_PERIOD;

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    VouchersInfo voucher = new VouchersInfo();
                    voucher.ReadValues(reader);

                    vouchers.Add(voucher);
                }
                return vouchers;
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

        public static DayBookInfo GetOpeningVoucher(DateTime toDate, int ledgerID)
        {
            DayBookInfo openingBalance = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_VOUCHER_DATE, toDate));
                parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_LEDGER, ledgerID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, VouchersInfo.QUERY_SELECT_OPENING_LEDGER, parameters);
                while (reader.Read())
                {
                    openingBalance = new DayBookInfo();
                    openingBalance.Particulars = LedgersDAO.GetLedgersInfo(ledgerID).LedgerName;
                    openingBalance.Debit = DBUtils.ConvertDecimal(reader["Debit"]);
                    openingBalance.Credit = DBUtils.ConvertDecimal(reader["Crdit"]);
                }
                return openingBalance;
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
    }
}