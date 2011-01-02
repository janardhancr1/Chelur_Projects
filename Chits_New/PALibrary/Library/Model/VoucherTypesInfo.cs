using System.Data;
using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class VoucherTypesInfo
    {
        public const string PARAM_VOUCHERTYPE_ID = DBConstant.DB_PARAM + "VoucherType_ID";
        public const string PARAM_VOUCHERTYPE_NAME = DBConstant.DB_PARAM + "VoucherType_Name";

        public const string TABLE_NAME = "voucher_types";

        public const string QUERY_SELECT =
            "SELECT VoucherType_ID,VoucherType_Name FROM " + TABLE_NAME + " WHERE VoucherType_ID=" +
            PARAM_VOUCHERTYPE_ID;

        public const string QUERY_SEARCH = "SELECT VoucherType_ID,VoucherType_Name FROM " + TABLE_NAME;
        public const string QUERY_COUNT = "SELECT Count(*) FROM " + TABLE_NAME;

        private int voucherTypeID;
        private string voucherTypeName;

        public int VoucherTypeID
        {
            get { return voucherTypeID; }
            set { voucherTypeID = value; }
        }

        public string VoucherTypeName
        {
            get { return voucherTypeName; }
            set { voucherTypeName = value; }
        }

        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "VoucherType_ID":
                        voucherTypeID = DBUtils.ConvertInt(reader["VoucherType_ID"]);
                        break;
                    case "VoucherType_Name":
                        voucherTypeName = DBUtils.ConvertString(reader["VoucherType_Name"]);
                        break;
                }
            }
        }
    }

}
