using Deliveries.App_Code;
using System;
using System.CodeDom.Compiler;
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
        public double AddOrder(String city, String Address, String Phone, int costumerID, int numObjects, int orderID)
        //הוספת הזמנה (כוללת בתוכה הקצאת שליח וחישוב מחיר משלוח)י
        {
            return ol.addOrder(city, Address, Phone, costumerID, numObjects, orderID);
        }
        [WebMethod]
        public String GetStatus(int OrderID, int costumerID)
        //קבלת סטטוס עדכני על ההזמנה
        {
            return ol.orderStatus(OrderID, costumerID);
        }
        [WebMethod]
        public void AnotherTryForFindingAgents()
        //ניסיון למצוא שליחים לכל ההזמנות שאין להם שליחים
        {

            OrdersLogic ol = new OrdersLogic();
            ol.AnotherTryForFindingAgents();
        }
    }
}
