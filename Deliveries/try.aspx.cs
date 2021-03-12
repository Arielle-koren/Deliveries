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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = ol.addOrder("פתח תקווה", "בן 3", "0522944944", 1, 4, 7).ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label2.Text = ol.orderStatus(7);
        }
    }
}