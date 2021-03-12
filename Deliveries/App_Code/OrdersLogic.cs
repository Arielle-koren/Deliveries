using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Deliveries.App_Code
{
    public class OrdersLogic
    {
        DAL dal = new DAL();
        CitiesLogic cl = new CitiesLogic();
        CostuLogic col = new CostuLogic();
        AreasLogic al = new AreasLogic();
        AgentsLogic agl = new AgentsLogic();
        public double addOrder(String city, String Address, String Phone, int costumerID, int numObjects, int orderID)
        {
            //חישוב מחיר משלוח לפי מרחקים. אותו איזור מחיר-20 , אותו טווח איזורים מחיר- 50 שח, אחרת 100 שח
            int to = cl.getCity(city);// מקבל את שם העיר אליו המשלוח צריך להגיע ומחזיר את האידי של העיר
            int areato = cl.getArea(to);// מקבל אידי של עיר אליה יגיע המשלוח ומחזיר באיזה איזור העיר הזאת
            int regionto = al.getRegion(areato);//טווח איזור ממנו יוצא המשלוח

            string addressFrom = col.getAddress(costumerID);
            int from = col.getCityID(costumerID);// מביא את האידי של העיר שבו ממוקמת החברה ממנה יתבצע המשלוח
            int areaFrom = cl.getArea(from);//מקבל את האידי של העיר שבו ממוקמת החברה ומחזיר את האיזור של החברה
            int regionFrom = al.getRegion(areaFrom);//טווח איזור אליו מגיע המשלוח
            double price;

            if (areato == areaFrom)// אותו איזור
                price = 20;
            else if (regionFrom == regionto)//אותו טווח איזורים
                price = 50;
            else //משלוח בין כמה טווחי איזורים
                price = 100;

            // בדיקת סוג שליח לפי המחיר, אותו טווח איזורים נחשב למרחקים קצרים
            string typeAgent;
            if (regionFrom == regionto)
                typeAgent = "shortDistances";
            else
                typeAgent = "longDistances";

            // בדיקת גודל משלוח, משלוח בינוני יגדיל את מחיר המשלוח ב10 שקלים, משלוח גדול יגדיל את המשלוח ב30 שקלים
            string size;
            if (numObjects <= 10)
            {
                size = "S";
            }
            else if (numObjects <= 30)
            {
                size = "M";
                price += 10;
            }
            else
            {
                size = "L";
                price += 30;
            }
            //רוב הלקוחות הם לקוחות קבועים עם אחוזי הנחה
            //חישוב מחיר לאחר ההנחה
            int salepercent = col.getSale(costumerID);
            price = price - (price * salepercent/100);

            int agentID=agl.findAgent(size, typeAgent, areaFrom, regionFrom);
            if (agentID != 0)// נמצא שליח
            {
                agl.addDeliveryToAgent(agentID);
                string sql = "INSERT INTO Orders (Name1, [From], CityFromID, [To], CityToID, Size1, Price, Date1, AgentID, Status, Phone, CostuID) VALUES ("+orderID +", '" + addressFrom + "', " + from + ", '" + Address + "', " + to + ", '" + size + "', " + price + ", #" + DateTime.Now + "#, " + agentID + " , 1, '" + Phone + "', " + costumerID + ") ";
                dal.excuteQuery(sql);
               
            }
            else// לא נמצא שליח
            {
                string sql = "INSERT INTO Orders (Name1, [From], CityFromID, [To], CityToID, Size1, Price, Date1, AgentID, Status, Phone, CostuID) VALUES (" + orderID + ", '" + addressFrom + "', " + from + ", '" + Address + "', " + to + ", '" + size + "', " + price + ", #" + DateTime.Now + "#, " + agentID + " , 0, '" + Phone + "', " + costumerID + ") ";
                dal.excuteQuery(sql);
            }
            return price;

        }
        public string orderStatus(int OrderID)
        {
            string sql = "SELECT Status From Orders WHERE Name1=" + OrderID;
            int st=Int32.Parse(dal.excuteQuery(sql).Tables[0].Rows[0][0].ToString());
            if (st == 1 || st==0)//  במקרה שעדיין לא נמצא שליח או שהשליח עוד לא יצאה לדרכו, המשתמש יקבל רק את המידע שההזמנה לא יצאה עדיין
                return "ההזמנה עוד לא יצאה";
            if (st == 2)
                return "ההזמנה בדרך";
            else
                return "ההזמנה הגיעה ליעדה";
        }
    }
}