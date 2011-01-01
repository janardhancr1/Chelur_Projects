using System;
using System.Data;
using System.Data.Common;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable table = DbProviderFactories.GetFactoryClasses();
        providerView.DataSource = table;
        providerView.DataBind();
    }
}
