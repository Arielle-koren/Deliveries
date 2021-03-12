using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deliveries.App_Code
{
    public class AreasLogic
    {
        DAL dal = new DAL();
        public int getRegion(int areaID)
        {
            string sql = "SELECT Region FROM Areas WHERE ID=" + areaID;
            return Int32.Parse(dal.excuteQuery(sql).Tables[0].Rows[0][0].ToString());
        }
    }
}