using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class CityManager
    {
        public static void AddCityInfo(CityInfo cityInfo)
        {
            CityDAO.AddCityInfo(cityInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateCityInfo(CityInfo cityInfo)
        {
            CityDAO.AddCityInfo(cityInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteCityInfo(int cityID)
        {
            CityDAO.DeleteCityInfo(cityID);
        }

        public static List<CityInfo> SearchCityInfo(int cityID, string villageName, string cityName, string state, string pincode, int startRowIndex, int maximumRows)
        {
            return CityDAO.SearchCityInfo(CityDAO.SearchConditions(cityID, villageName, cityName, state, pincode), startRowIndex);
        }

        public static int SearchCityInfoCount(int cityID, string villageName, string cityName, string state, string pincode, int startRowIndex, int maximumRows)
        {
            return CityDAO.SearchCityInfoCount(CityDAO.SearchConditions(cityID, villageName, cityName, state, pincode));
        }

        public static CityInfo GetCityInfo(int cityID)
        {
            return CityDAO.GetCityInfo(cityID);
        }

        public static List<CityInfo> GetCityInfos()
        {
            return CityDAO.GetCityInfos();
        }

    }
}