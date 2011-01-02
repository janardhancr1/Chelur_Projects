using System.Data;
using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class GroupsInfo
    {
        public const string PARAM_GROUP_ID = DBConstant.DB_PARAM + "Group_ID";
        public const string PARAM_GROUP_NAME = DBConstant.DB_PARAM + "Group_Name";
        public const string PARAM_MAIN_GROUP = DBConstant.DB_PARAM + "Main_Group";
        public const string PARAM_SUB_GROUP = DBConstant.DB_PARAM + "Sub_Group";
        public const string PARAM_CR_DR = DBConstant.DB_PARAM + "Cr_Dr";

        public const string TABLE_NAME = "Groups";

        public const string QUERY_SEARCH = "SELECT Group_ID,Group_Name,Main_Group,Sub_Group,Cr_Dr FROM " + TABLE_NAME + " ORDER BY Group_Name";
        public const string QUERY_TB_GROUPS = "SELECT DISTINCT(Sub_Group),Cr_Dr,Main_Group FROM " + TABLE_NAME + " ORDER BY Sub_Group";

        public const string QUERY_SELECT =
            "SELECT Group_ID,Group_Name,Main_Group,Sub_Group,Cr_Dr FROM " + TABLE_NAME + " WHERE Group_ID=" +
            PARAM_GROUP_ID;

        private int groupID;
        private string groupName;
        private string mainGroup;
        private string subGroup;
        private string crDr;

        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        public string MainGroup
        {
            get { return mainGroup; }
            set { mainGroup = value; }
        }

        public string SubGroup
        {
            get { return subGroup; }
            set { subGroup = value; }
        }

        public string CrDr
        {
            get { return crDr; }
            set { crDr = value; }
        }

        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Group_ID":
                        groupID = DBUtils.ConvertInt(reader["Group_ID"]);
                        break;
                    case "Group_Name":
                        groupName = DBUtils.ConvertString(reader["Group_Name"]);
                        break;
                    case "Main_Group":
                        mainGroup = DBUtils.ConvertString(reader["Main_Group"]);
                        break;
                    case "Sub_Group":
                        subGroup = DBUtils.ConvertString(reader["Sub_Group"]);
                        break;
                    case "Cr_Dr":
                        crDr = DBUtils.ConvertString(reader["Cr_Dr"]);
                        break;
                }
            }
        }
    }
}
