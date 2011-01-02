using PALibrary.Library.DAO;
using PALibrary.Library.Model;

namespace PALibrary.Library.Component
{
    public class UtilManager
    {
        public static string GetNextSerial(string tableName, string fieldName)
        {
            return UtilDAO.GetNextSerial(tableName, fieldName);
        }
    }
}
