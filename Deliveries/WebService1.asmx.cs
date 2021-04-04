using Deliveries.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace Deliveries
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        OrdersLogic ol = new OrdersLogic();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public double AddOrder(String city, String Address, String Phone, int costumerID, int numObjects, int orderID)
        //הוספת הזמנה (כוללת בתוכה הקצאת שליח וחישוב מחיר משלוח)י
        {
            return ol.addOrder(city, Address, Phone, costumerID, numObjects, orderID);
        }
        [WebMethod]
        public String GetStatus(int OrderID)
        //קבלת סטטוס עדכני על ההזמנה
        {
            return ol.orderStatus(OrderID);
        }
        [WebMethod]
        public DataSet paymentsDetail(int costumerID)
        //קבלת סכום תשלום על ההזמנה
        {
            return ol.paymentDetails(1, "03/04/2021", "04/04/2021");

        }
        [WebMethod]
        public void a()
        {
          //  Timer t = new Timer(TimerCallback,);

            // Figure how much time until 4:00
            DateTime now = DateTime.Now;
            DateTime fourOClock = DateTime.Today.AddHours(16.0);

            // If it's already past 4:00, wait until 4:00 tomorrow    
            if (now > fourOClock)
            {
                fourOClock = fourOClock.AddDays(1.0);
            }

            int msUntilFour = (int)((fourOClock - now).TotalMilliseconds);

            // Set the timer to elapse only once, at 4:00.
         //   t.Change(msUntilFour, Timeout.Infinite);
        }
    }
}
