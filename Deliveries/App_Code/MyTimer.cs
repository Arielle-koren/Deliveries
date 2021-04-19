﻿using System;
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
            _timer = new System.Timers.Timer(5000);//משך הזמן בין הפעלת הפעולה
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);//
            GC.KeepAlive(_timer);//גורם לטיימר להמשיך לפעול עד שהתוכנית נגמרת
            _timer.Start();
        }
        private static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            OrdersLogic ol = new OrdersLogic();
            ol.AnotherTryForFindingAgents();
            s++;
        }
    }
}