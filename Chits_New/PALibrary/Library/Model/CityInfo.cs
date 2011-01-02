using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class CityInfo
    {
        public const string PARAM_CITY_ID = DBConstant.DB_PARAM + "City_ID";
        public const string PARAM_VILLAGE_NAME = DBConstant.DB_PARAM + "Village_Name";
        public const string PARAM_CITY_NAME = DBConstant.DB_PARAM + "City_Name";
        public const string PARAM_STATE = DBConstant.DB_PARAM + "State";
        public const string PARAM_PINCODE = DBConstant.DB_PARAM + "Pincode";

        public const string TABLE_NAME = "cities";

        public const string QUERY_INSERT = "INSERT INTO " + TABLE_NAME + "(City_ID,Village_Name,City_Name,State,Pincode) VALUES (" + PARAM_CITY_ID + "," + PARAM_VILLAGE_NAME + "," + PARAM_CITY_NAME + "," + PARAM_STATE + "," + PARAM_PINCODE + ")";
        public const string QUERY_UPDATE = "UPDATE " + TABLE_NAME + " SET Village_Name=" + PARAM_VILLAGE_NAME + ",City_Name=" + PARAM_CITY_NAME + ",State=" + PARAM_STATE + ",Pincode=" + PARAM_PINCODE + " WHERE City_ID=" + PARAM_CITY_ID;
        public const string QUERY_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE City_ID=" + PARAM_CITY_ID;

        public const string QUERY_SEARCH = "SELECT City_ID,Village_Name,City_Name,State,Pincode FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        public const string QUERY_SELECT = "SELECT City_ID,Village_Name,City_Name,State,Pincode FROM " + TABLE_NAME + " WHERE City_ID=" + PARAM_CITY_ID;
        public const string QUERY_SELECT_ALL = "SELECT City_ID,Village_Name,City_Name,State,Pincode FROM " + TABLE_NAME;

        private int cityID;
        private string villageName;
        private string cityName;
        private string state;
        private string pincode;

        public int CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }

        public string VillageName
        {
            get { return villageName; }
            set { villageName = value; }
        }

        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string Pincode
        {
            get { return pincode; }
            set { pincode = value; }
        }


        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_CITY_ID, cityID));
            parameters.Add(DBManager.GetParameter(PARAM_VILLAGE_NAME, villageName));
            parameters.Add(DBManager.GetParameter(PARAM_CITY_NAME, cityName));
            parameters.Add(DBManager.GetParameter(PARAM_STATE, state));
            parameters.Add(DBManager.GetParameter(PARAM_PINCODE, pincode));

            return parameters;
        }
        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "City_ID":
                        cityID = DBUtils.ConvertInt(reader["City_ID"]);
                        break;
                    case "Village_Name":
                        villageName = DBUtils.ConvertString(reader["Village_Name"]);
                        break;
                    case "City_Name":
                        cityName = DBUtils.ConvertString(reader["City_Name"]);
                        break;
                    case "State":
                        state = DBUtils.ConvertString(reader["State"]);
                        break;
                    case "Pincode":
                        pincode = DBUtils.ConvertString(reader["Pincode"]);
                        break;

                }
            }
        }
    }
}