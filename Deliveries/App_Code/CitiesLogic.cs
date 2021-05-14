using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deliveries.App_Code
{
    public class CitiesLogic
    {
        DAL dal = new DAL();
        public int getArea (int cityID)//מחזירה באיזה איזור העיר נמצאת
        {
            string sql = "SELECT Area FROM Cities WHERE ID=" +cityID;
            return Int32.Parse(dal.excuteQuery(sql).Tables[0].Rows[0][0].ToString());
        }
        public int getCity (string city)// מחזירה id של עיר
        {
            string sql = "SELECT ID FROM Cities WHERE Name= '" + city+"'";
            return Int32.Parse(dal.excuteQuery(sql).Tables[0].Rows[0][0].ToString());
        }
    }
}