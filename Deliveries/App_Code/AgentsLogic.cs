using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Deliveries.App_Code
{
    
    public class AgentsLogic
    {
        DAL dal = new DAL();

        public int findAgent(string size, string typeAgent, int areaFrom, int regionFrom)
        {
            // הקצאת שליח. הוא צריך להיות בסוג המתאים ורכבו צריך להתאים לגודל המשלוח
            // רצוי שהשליח יהיה מאותו איזור ממנו המשלוח יוצא ואם לא אז מאותו טווח איזורים
            //מקסימום משלוחים שיכול להיות לשליח הוא 20
            int agentID = 0;
            string sql = "SELECT ID FROM Agents WHERE CarType='" + size + "' AND Type='" + typeAgent + "' AND NumOrders<20 AND Area=" + areaFrom + " ORDER BY NumOrders";
            DataSet ds = dal.excuteQuery(sql);
            if (ds.Tables[0].Rows.Count > 0)// יש שליחים שעונים על כל התנאים
            {
                agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
            }
            else if (size.Equals("S"))// אין אף שליח פנוי שגר באיזור עם נתונים מתאימים ולכן מנסים לאתר שליח עם רכב יותר גדול
            {
                sql = "SELECT ID FROM Agents WHERE CarType='M' AND Type='" + typeAgent + "' AND NumOrders<20 AND Area=" + areaFrom + " ORDER BY NumOrders";// בדיקה אם יש רכב M
                ds = dal.excuteQuery(sql);
                if (ds.Tables[0].Rows.Count > 0)// יש שליח
                    agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
                else
                {
                    sql = "SELECT ID FROM Agents WHERE CarType='L' AND Type='" + typeAgent + "' AND NumOrders<20 AND Area=" + areaFrom + " ORDER BY NumOrders";// בדיקה אם יש רכב L
                    ds = dal.excuteQuery(sql);
                    if (ds.Tables[0].Rows.Count > 0)// יש שליח
                        agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
                }

            }
            else if (size.Equals("M"))// אין אף שליח פנוי שגר באיזור עם נתונים מתאימים ולכן מנסים לאתר שליח עם רכב יותר גדול
            {
                sql = "SELECT ID FROM Agents WHERE CarType='L' AND Type='" + typeAgent + "' AND NumOrders<20 AND Area=" + areaFrom + " ORDER BY NumOrders";// בדיקה אם יש רכב L
                ds = dal.excuteQuery(sql);
                if (ds.Tables[0].Rows.Count > 0)// יש שליח
                    agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
            }

            // אין אף שליח פנוי שגר באיזור עם נתונים מתאימים ולכן מנסים לאתר שליח מאותו טווח איזורים
            if (agentID == 0)
            {
                sql = "SELECT Agents.ID FROM Agents INNER JOIN Areas ON Agents.Area=Areas.ID WHERE CarType='" + size + "' AND Type='" + typeAgent + "' AND NumOrders<20 AND Areas.Region=" + regionFrom + " ORDER BY NumOrders";
                ds = dal.excuteQuery(sql);
                if (ds.Tables[0].Rows.Count > 0)// יש שליח
                    agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח

                else if (size.Equals("S"))// אין אף שליח פנוי שגר בטווח איזורים עם נתונים מתאימים ולכן מנסים לאתר שליח עם רכב יותר גדול
                {
                    sql = "SELECT Agents.ID FROM Agents INNER JOIN Areas ON Agents.Area=Areas.ID WHERE CarType='M' AND Type='" + typeAgent + "' AND NumOrders<20 AND Areas.Region=" + regionFrom + " ORDER BY NumOrders";// בדיקה אם יש רכב M
                    ds = dal.excuteQuery(sql);
                    if (ds.Tables[0].Rows.Count > 0)// יש שליח
                        agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
                    else
                    {
                        sql = "SELECT Agents.ID FROM Agents INNER JOIN Areas ON Agents.Area=Areas.ID WHERE CarType='L' AND Type='" + typeAgent + "' AND NumOrders<20 AND Areas.Region=" + regionFrom + " ORDER BY NumOrders";// בדיקה אם יש רכב L
                        ds = dal.excuteQuery(sql);
                        if (ds.Tables[0].Rows.Count > 0)// יש שליח
                            agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
                    }

                }
                else if (size.Equals("M"))// אין אף שליח פנוי שגר בטווח איזורים עם נתונים מתאימים ולכן מנסים לאתר שליח עם רכב יותר גדול
                {
                    sql = "SELECT Agents.ID FROM Agents INNER JOIN Areas ON Agents.Area=Areas.ID WHERE CarType='L' AND Type='" + typeAgent + "' AND NumOrders<20 AND Areas.Region=" + regionFrom + " ORDER BY NumOrders";// בדיקה אם יש רכב L
                    ds = dal.excuteQuery(sql);
                    if (ds.Tables[0].Rows.Count > 0)// יש שליח
                        agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
                }
            }

                //אין אף שליח פנוי עם נתונים מתאימים שגר בטווח איזורים ולכן מאתרים שליח מסוג מרחקים ארוכים עם נתונים מתאימים
           if (agentID == 0)
           {
                    sql = "SELECT ID FROM Agents WHERE CarType='" + size + "' AND Type='longDistances' AND NumOrders<20 ORDER BY NumOrders";
                    ds = dal.excuteQuery(sql);
                    if (ds.Tables[0].Rows.Count > 0)// יש שליחים שעונים על כל התנאים
                    {
                        agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
                    }
                    else if (size.Equals("S"))// אין אף שליח פנוי ולכן מנסים לאתר שליח עם רכב יותר גדול
                    {
                        sql = "SELECT ID FROM Agents WHERE CarType='M' AND Type='longDistances' AND NumOrders<20 ORDER BY NumOrders";// בדיקה אם יש רכב M
                        ds = dal.excuteQuery(sql);
                        if (ds.Tables[0].Rows.Count > 0)// יש שליח
                            agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
                        else
                        {
                            sql = "SELECT ID FROM Agents WHERE CarType='L' AND Type='longDistances' AND NumOrders<20 ORDER BY NumOrders";// בדיקה אם יש רכב L
                            ds = dal.excuteQuery(sql);
                            if (ds.Tables[0].Rows.Count > 0)// יש שליח
                                agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
                        }

                    }
                    else if (size.Equals("M"))// אין אף שליח פנוי ולכן מנסים לאתר שליח עם רכב יותר גדול
                    {
                        sql = "SELECT ID FROM Agents WHERE CarType='L' AND Type='longDistances' AND NumOrders<20 ORDER BY NumOrders";// בדיקה אם יש רכב L
                        ds = dal.excuteQuery(sql);
                        if (ds.Tables[0].Rows.Count > 0)// יש שליח
                            agentID = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());// השליח עם הכי פחות משלוחים יהיה זה שיבצע את המשלוח
                    }

                
           }
            return agentID;
        }
        public void addDeliveryToAgent(int AgentID)
        {
            string sql = "Update Agents SET NumOrders=NumOrders+1 WHERE ID=" + AgentID;
            dal.excuteQuery(sql);
        }
        public void findAgent2(DataSet ds)//מציאת שליחים לכל מי שלא נמצא להם פעם קודמת
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string typeAgent;
                if (ds.Tables[0].Rows[i]["From"] == ds.Tables[0].Rows[i]["To"])
                    typeAgent = "shortDistances";
                else
                    typeAgent = "longDistances";

                int agentID = this.findAgent(ds.Tables[0].Rows[i]["Size1"].ToString(), typeAgent, Int32.Parse(ds.Tables[0].Rows[i]["From"].ToString()), Int32.Parse(ds.Tables[0].Rows[i]["to"].ToString()));
                if (agentID != 0)//נמצא שליח
                {
                    this.addDeliveryToAgent(agentID);
                    string sql = "UPDATE Orders SET AgentID= "+agentID+ ", Status= 1 WHERE ID="+ ds.Tables[0].Rows[i]["ID"].ToString();
                    dal.excuteQuery(sql);
                }
            }
        }
    }
}