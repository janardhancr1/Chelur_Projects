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
    class CustomerDAO
    {
        public static long GetNextAccountNo()
        {
            try
            {
                object result = SQLHelper.ExecuteScalar(CommandType.Text, CustomerInfo.QUERY_MAX_ACCOUNTNO, null);
                int count = DBUtils.ConvertInt(result);
                if (count > 0) count++;

                return count;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
        }

        public static void AddCustomerInfo(CustomerInfo customerInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, CustomerInfo.QUERY_INSERT, customerInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, CustomerInfo.QUERY_UPDATE, customerInfo.GetParameters());

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

        public static void DeleteCustomerInfo(int customerID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(CustomerInfo.PARAM_CUSTOMER_ID, customerID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, CustomerInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(int customerID, string customerName, string sonHusband, int accountNO, string resAddress, int resVillage, string resPhone)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                if (customerID > 0)
                {
                    conditions.Add("Customer_id = " + CustomerInfo.PARAM_CUSTOMER_ID);
                    parameters.Add(DBManager.GetParameter(CustomerInfo.PARAM_CUSTOMER_ID, customerID));
                }

                if (customerName != null)
                {
                    if (customerName.Trim().Length > 0)
                    {
                        conditions.Add("Customer_name LIKE " + CustomerInfo.PARAM_CUSTOMER_NAME);
                        parameters.Add(DBManager.GetParameter(CustomerInfo.PARAM_CUSTOMER_NAME, customerName + "%"));
                    }
                }

                if (sonHusband != null)
                {
                    if (sonHusband.Trim().Length > 0)
                    {
                        conditions.Add("Son_husband LIKE " + CustomerInfo.PARAM_SON_HUSBAND);
                        parameters.Add(DBManager.GetParameter(CustomerInfo.PARAM_SON_HUSBAND, sonHusband + "%"));
                    }
                }

                if (accountNO > 0)
                {
                    conditions.Add("Account_no LIKE " + CustomerInfo.PARAM_ACCOUNT_NO);
                    parameters.Add(DBManager.GetParameter(CustomerInfo.PARAM_ACCOUNT_NO, accountNO + "%"));
                }

                if (resAddress != null)
                {
                    if (resAddress.Trim().Length > 0)
                    {
                        conditions.Add("Res_address LIKE " + CustomerInfo.PARAM_RES_ADDRESS);
                        parameters.Add(DBManager.GetParameter(CustomerInfo.PARAM_RES_ADDRESS, resAddress + "%"));
                    }
                }

                if (resVillage > 0)
                {
                    conditions.Add("Res_village = " + CustomerInfo.PARAM_RES_VILLAGE);
                    parameters.Add(DBManager.GetParameter(CustomerInfo.PARAM_RES_VILLAGE, resVillage));
                }

                if (resPhone != null)
                {
                    if (resPhone.Trim().Length > 0)
                    {
                        conditions.Add("Res_phone LIKE " + CustomerInfo.PARAM_RES_PHONE);
                        parameters.Add(DBManager.GetParameter(CustomerInfo.PARAM_RES_PHONE, resPhone + "%"));
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

        public static List<CustomerInfo> SearchCustomerInfo(SearchHelper searchHelper, int startPage)
        {
            List<CustomerInfo> customerInfos = new List<CustomerInfo>();
            IDataReader reader = null;
            try
            {
                string query = CustomerInfo.QUERY_SEARCH;
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
                    CustomerInfo customerInfo = new CustomerInfo();
                    customerInfo.ReadValues(reader);

                    CityInfo city = CityDAO.GetCityInfo(customerInfo.ResVillage);
                    customerInfo.FullAddress = city.VillageName + "," + city.CityName + " - " + city.Pincode;
                    customerInfos.Add(customerInfo);
                }
                return customerInfos;
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

        public static int SearchCustomerInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = CustomerInfo.QUERY_COUNT;
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

        public static CustomerInfo GetCustomerInfo(int customerID)
        {
            CustomerInfo customerInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(CustomerInfo.PARAM_CUSTOMER_ID, customerID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, CustomerInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    customerInfo = new CustomerInfo();
                    customerInfo.ReadValues(reader);

                    CityInfo city = CityDAO.GetCityInfo(customerInfo.ResVillage);
                    customerInfo.FullAddress = city.VillageName + "," + city.CityName + " - " + city.Pincode;
                }
                return customerInfo;
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

        public static List<CustomerInfo> GetCustomerInfos()
        {
            List<CustomerInfo> customerInfos = new List<CustomerInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, CustomerInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    CustomerInfo customerInfo = new CustomerInfo();
                    customerInfo.ReadValues(reader);

                    CityInfo city = CityDAO.GetCityInfo(customerInfo.ResVillage);
                    customerInfo.FullAddress = city.VillageName + "," + city.CityName + " - " + city.Pincode;

                    customerInfos.Add(customerInfo);
                }
                return customerInfos;
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

        public static List<DayBookInfo> HundiLoans(int customerID)
        {
            List<DayBookInfo> loans = new List<DayBookInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(CustomerInfo.PARAM_CUSTOMER_ID, customerID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, CustomerInfo.QUERY_HUNDI_LOAN, parameters);
                while (reader.Read())
                {
                    DayBookInfo loan = new DayBookInfo();
                    loan.CurrentDate = DBUtils.ConvertDate(reader["loan_date"]);
                    loan.VoucherType = DBConstant.HUNDI_LOAN;
                    loan.Narration = DBUtils.ConvertString(reader["hl_loanno"]);
                    loan.Debit = DBUtils.ConvertDecimal(reader["loan_amount"]);
                    loan.Particulars = DBUtils.ConvertString(reader["closed"]);

                    loans.Add(loan);
                }
                return loans;
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