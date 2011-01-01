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
    class CityDAO
    {
        public static void AddCityInfo(CityInfo cityInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, CityInfo.QUERY_INSERT, cityInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, CityInfo.QUERY_UPDATE, cityInfo.GetParameters());

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

        public static void DeleteCityInfo(int cityID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(CityInfo.PARAM_CITY_ID, cityID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, CityInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(int cityID, string villageName, string cityName, string state, string pincode)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                if (cityID > 0)
                {
                    conditions.Add("City_ID = " + CityInfo.PARAM_CITY_ID);
                    parameters.Add(DBManager.GetParameter(CityInfo.PARAM_CITY_ID, cityID));
                }

                if (villageName != null)
                {
                    if (villageName.Trim().Length > 0)
                    {
                        conditions.Add("Village_Name LIKE " + CityInfo.PARAM_VILLAGE_NAME);
                        parameters.Add(DBManager.GetParameter(CityInfo.PARAM_VILLAGE_NAME, villageName + "%"));
                    }
                }

                if (cityName != null)
                {
                    if (cityName.Trim().Length > 0)
                    {
                        conditions.Add("City_Name LIKE " + CityInfo.PARAM_CITY_NAME);
                        parameters.Add(DBManager.GetParameter(CityInfo.PARAM_CITY_NAME, cityName + "%"));
                    }
                }

                if (state != null)
                {
                    if (state.Trim().Length > 0)
                    {
                        conditions.Add("State LIKE " + CityInfo.PARAM_STATE);
                        parameters.Add(DBManager.GetParameter(CityInfo.PARAM_STATE, state + "%"));
                    }
                }

                if (pincode != null)
                {
                    if (pincode.Trim().Length > 0)
                    {
                        conditions.Add("Pincode LIKE " + CityInfo.PARAM_PINCODE);
                        parameters.Add(DBManager.GetParameter(CityInfo.PARAM_PINCODE, pincode + "%"));
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

        public static List<CityInfo> SearchCityInfo(SearchHelper searchHelper, int startPage)
        {
            List<CityInfo> cityInfos = new List<CityInfo>();
            IDataReader reader = null;
            try
            {
                string query = CityInfo.QUERY_SEARCH;
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
                    CityInfo cityInfo = new CityInfo();
                    cityInfo.ReadValues(reader);

                    cityInfos.Add(cityInfo);
                }
                return cityInfos;
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

        public static int SearchCityInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = CityInfo.QUERY_COUNT;
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

        public static CityInfo GetCityInfo(int cityID)
        {
            CityInfo cityInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(CityInfo.PARAM_CITY_ID, cityID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, CityInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    cityInfo = new CityInfo();
                    cityInfo.ReadValues(reader);
                }
                return cityInfo;
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

        public static List<CityInfo> GetCityInfos()
        {
            List<CityInfo> cityInfos = new List<CityInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, CityInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    CityInfo cityInfo = new CityInfo();
                    cityInfo.ReadValues(reader);

                    cityInfos.Add(cityInfo);
                }
                return cityInfos;
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