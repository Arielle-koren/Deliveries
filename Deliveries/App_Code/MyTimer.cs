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
        
        public static int s= 0;
        public static System.Timers.Timer _timer;
        public static void startTimer()
        {
            _timer = new System.Timers.Timer(1000*60*60*24);//משך הזמן בין הפעלת הפעולה, כל 1000 שווה לשנייה אחת
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);//
            GC.KeepAlive(_timer);//גורם לטיימר להמשיך לפעול עד שהתוכנית נגמרת
            _timer.Start();
        }
        private static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {

                 OrdersLogic ol = new OrdersLogic();
                  ol.AnotherTryForFindingAgents();
                s++;
            }
            catch(Exception ex)
            {
            }
        }
    }
}