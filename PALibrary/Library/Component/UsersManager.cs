using PALibrary.Library.DAO;
using PALibrary.Library.Model;

namespace PALibrary.Library.Component
{
    public class UsersManager
    {
        public static bool ValidateUser(UsersInfo usersInfo)
        {
            return UsersDAO.ValidateUser(usersInfo);
        }

        public static void ChangePassword(UsersInfo usersInfo)
        {
            UsersDAO.ChangePassword(usersInfo);
        }

    }
}