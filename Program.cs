using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WsClient
{
    static class Program
    {
        public static WsClient.Properties.Settings appSetting = new WsClient.Properties.Settings();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // comment out this for temp fix of 
            // http://support.microsoft.com/default.aspx?scid=kb;en-us;905721
            // Application.EnableVisualStyles();

            SplashScreen.ShowSplashScreen();

            Application.Run(new MainForm());
            
        }
    }
}