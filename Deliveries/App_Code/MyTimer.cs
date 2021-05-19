using Deliveries.App_Code;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Deliveries.App_Code
{
    public class MyTimer
    {
        
        public /*static*/ int s= 0;
        public /*static*/ System.Timers.Timer _timer;
        public /*static*/ void startTimer()
        {
            _timer = new System.Timers.Timer(1000*30);//משך הזמן בין הפעלת הפעולה, כל 1000 שווה לשנייה אחת
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);//
            GC.KeepAlive(_timer);//גורם לטיימר להמשיך לפעול עד שהתוכנית נגמרת
            _timer.Start();
        }
        private /*static*/ void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            try
            {

                //   Parallel.Invoke(params, new object[], ); 
                 OrdersLogic ol = new OrdersLogic();
                  ol.AnotherTryForFindingAgents();
              //  Invoke(action, 1);
                s++;
                
               /* DAL dal2 = new DAL();
                string sql = "SELECT Area FROM Cities WHERE ID=" + 2;
                int a= Int32.Parse(dal2.excuteQuery(sql).Tables[0].Rows[0][0].ToString());*/
            }
            catch(Exception ex)
            {
                int a = 0;
            }
        }
        /*private static void action()
        {
            OrdersLogic ol = new OrdersLogic();
            ol.AnotherTryForFindingAgents().Invoke();
        }*/
    }
}