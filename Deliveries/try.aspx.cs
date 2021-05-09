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
            ol.AnotherTryForFindingAgents();
            GridView1.DataSource = ol.paymentDetails(1);
            GridView1.DataBind();
        }

        
    }
}