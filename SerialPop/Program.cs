using System;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using shared;

namespace SerialPop
{
    static class Program
    {
        static void PollLoop(object argument)
        {
            NotifyIcon notifyIcon = (NotifyIcon)argument;

            IOrderedEnumerable<string> older_ports = Serial.GetSortedSerialPortNames();
            IOrderedEnumerable<string> newer_ports = older_ports;

            while (true)
            {
                Thread.Sleep(1000);

                newer_ports = Serial.GetSortedSerialPortNames();

                if (!Enumerable.SequenceEqual(older_ports, newer_ports))
                {
                    notifyIcon.ShowBalloonTip(5000,
                        "Serial ports changed:",
                        Serial.GetDiff(older_ports, newer_ports),
                        ToolTipIcon.Info);
                }

                older_ports = newer_ports;
            }
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                var applicationContext = new CustomApplicationContext();

                Thread pollingThread = new Thread(new ParameterizedThreadStart(PollLoop));
                pollingThread.Start(applicationContext.notifyIcon);

                Application.Run(applicationContext);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Program Terminated Unexpectedly",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
