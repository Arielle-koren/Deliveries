using Deliveries.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Deliveries
{
    public partial class _try : System.Web.UI.Page
    {
        OrdersLogic ol = new OrdersLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = ol.paymentDetails(1, "03/04/2020", "04/04/2023");
            GridView1.DataBind();
        }

        
    }
}