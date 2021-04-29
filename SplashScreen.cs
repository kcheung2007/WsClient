using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WsClient
{
    public partial class SplashScreen : Form
    {
        // Threading
        static SplashScreen ms_frmSplash = null;
        static Thread ms_oThread = null;
        static string ms_Status = "Loading...";

        // Fade in and out.
        private double m_dblOpacityIncrement = .05;
        private double m_dblOpacityDecrement = .08;
        private const int TIMER_INTERVAL = 50;
        private int m_iActualTicks = 0;

        public SplashScreen()
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();

            this.Opacity = .00;
            timer1.Interval = TIMER_INTERVAL;
            timer1.Start();
            this.ClientSize = this.BackgroundImage.Size;
        }

        // ************* Static Methods *************** //
        // A static method to create the thread and 
        // launch the SplashScreen.
        static public void ShowSplashScreen()
        {
            // Make sure it's only launched once.
            if (ms_frmSplash != null)
                return;
            ms_oThread = new Thread(new ThreadStart(SplashScreen.ShowForm));
            ms_oThread.IsBackground = true;
            ms_oThread.SetApartmentState(ApartmentState.STA);
            ms_oThread.Start();
        }

        // A property returning the splash screen instance
        static public SplashScreen SplashForm
        {
            get
            {
                return ms_frmSplash;
            }
        }

        // A private entry point for the thread.
        static private void ShowForm()
        {
            ms_frmSplash = new SplashScreen();
            Application.Run(ms_frmSplash);
        }

        // A static method to close the SplashScreen
        static public void CloseForm()
        {
            if (ms_frmSplash != null && ms_frmSplash.IsDisposed == false)
            {
                // Make it start going away.
                ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
            }
            ms_oThread = null;	// we don't need these any more.
            ms_frmSplash = null;
        }

        // A static method to set the status and update the reference.
        static public void SetStatus(string newStatus)
        {
            ms_Status = newStatus;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = ms_Status;

            if (m_dblOpacityIncrement > 0)
            {
                m_iActualTicks++;
                if (this.Opacity < 1)
                    this.Opacity += m_dblOpacityIncrement;
            }
            else
            {
                if (this.Opacity > 0)
                    this.Opacity += m_dblOpacityIncrement;
                else
                {
//                    StoreIncrements();
                    this.Close();
                    Debug.WriteLine("Called this.Close()");
                }
            }
            //if (m_bFirstLaunch == false && m_dblLastCompletionFraction < m_dblCompletionFraction)
            //{
            //    m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval;
            //    int width = (int)Math.Floor(pnlStatus.ClientRectangle.Width * m_dblLastCompletionFraction);
            //    int height = pnlStatus.ClientRectangle.Height;
            //    int x = pnlStatus.ClientRectangle.X;
            //    int y = pnlStatus.ClientRectangle.Y;
            //    if (width > 0 && height > 0)
            //    {
            //        m_rProgress = new Rectangle(x, y, width, height);
            //        pnlStatus.Invalidate(m_rProgress);
            //        int iSecondsLeft = 1 + (int)(TIMER_INTERVAL * ((1.0 - m_dblLastCompletionFraction) / m_dblPBIncrementPerTimerInterval)) / 1000;
            //        if (iSecondsLeft == 1)
            //            lblTimeRemaining.Text = string.Format("1 second remaining");
            //        else
            //            lblTimeRemaining.Text = string.Format("{0} seconds remaining", iSecondsLeft);

            //    }
            //}
        }
    }
}