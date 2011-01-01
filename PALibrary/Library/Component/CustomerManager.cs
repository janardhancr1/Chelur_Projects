using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class CustomerManager
    {
        public static long GetNextAccountNo()
        {
            return CustomerDAO.GetNextAccountNo();
        }

        public static void AddCustomerInfo(CustomerInfo customerInfo)
        {
            CustomerDAO.AddCustomerInfo(customerInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateCustomerInfo(CustomerInfo customerInfo)
        {
            CustomerDAO.AddCustomerInfo(customerInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteCustomerInfo(int customerID)
        {
            CustomerDAO.DeleteCustomerInfo(customerID);
        }

        public static List<CustomerInfo> SearchCustomerInfo(int customerID, string customerName, string sonHusband, int accountNO, string resAddress, int resVillage, string resPhone, int startRowIndex, int maximumRows)
        {
            return CustomerDAO.SearchCustomerInfo(CustomerDAO.SearchConditions(customerID, customerName, sonHusband, accountNO, resAddress, resVillage, resPhone), startRowIndex);
        }

        public static int SearchCustomerInfoCount(int customerID, string customerName, string sonHusband, int accountNO, string resAddress, int resVillage, string resPhone, int startRowIndex, int maximumRows)
        {
            return CustomerDAO.SearchCustomerInfoCount(CustomerDAO.SearchConditions(customerID, customerName, sonHusband, accountNO, resAddress, resVillage, resPhone));
        }

        public static CustomerInfo GetCustomerInfo(int customerID)
        {
            return CustomerDAO.GetCustomerInfo(customerID);
        }

        public static List<CustomerInfo> GetCustomerInfos()
        {
            return CustomerDAO.GetCustomerInfos();
        }

        public static List<DayBookInfo> GetCustomerLoans(int customerID)
        {
            List<DayBookInfo> customerLoans = new List<DayBookInfo>();
            CustomerDAO custDao = new CustomerDAO();

            List<DayBookInfo> loans = CustomerDAO.HundiLoans(customerID);
            foreach (DayBookInfo loan in loans)
            {
                customerLoans.Add(loan);
            }

            return customerLoans;
        }
    }
}