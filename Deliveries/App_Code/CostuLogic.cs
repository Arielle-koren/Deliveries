using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deliveries.App_Code
{
    
    public class CostuLogic
    {
        DAL dal = new DAL();
        public int getCityID (int CostuID)//מחזירה את האידי של העיר שבו נמצא הלקוח
        {
            string sql = "SELECT City From Costu WHERE ID="+ CostuID;
            return Int32.Parse(dal.excuteQuery(sql).Tables[0].Rows[0][0].ToString());
        }
        public string getAddress(int CostuID)// מחזירה את הכתובת של הלקוח
        {
            string sql = "SELECT Address From Costu WHERE ID=" + CostuID;
            return dal.excuteQuery(sql).Tables[0].Rows[0][0].ToString();
        }
        public int getSale(int CostuID)// מחזירה את אחוז ההנחה שיש ללקוח
        {
            string sql = "SELECT SalePercent From Costu WHERE ID=" + CostuID;
            return Int32.Parse(dal.excuteQuery(sql).Tables[0].Rows[0][0].ToString());
        }

    }
}