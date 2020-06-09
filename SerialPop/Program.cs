using System;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using shared;
using System.Collections.Generic;

namespace SerialPop
{
    static class Program
    {

        static void UpdateContextMenu(ContextMenuStrip strip, IOrderedEnumerable<string> newer_ports)
        {
            strip.Close();
            strip.Items.Clear();
            foreach (string new_port in newer_ports)
            {
                strip.Items.Add(new_port);
            }
        }

        static void InvokeUpdateContextMenu(NotifyIcon notifyIcon, IOrderedEnumerable<string> newer_ports)
        {
            if (notifyIcon.ContextMenuStrip.InvokeRequired)
            {
                // invoke the update on the UI thread
                notifyIcon.ContextMenuStrip.Invoke((MethodInvoker)delegate
                {
                    UpdateContextMenu(notifyIcon.ContextMenuStrip, newer_ports);
                });
            }
            else
            {
                UpdateContextMenu(notifyIcon.ContextMenuStrip, newer_ports);
            }
        }

        static void PollLoop(object argument)
        {
            NotifyIcon notifyIcon = (NotifyIcon)argument;

            // GetSortedSerialPortNames() can throw, so initialization has to be trivial
            IOrderedEnumerable<string> older_ports = null;
            IOrderedEnumerable<string> newer_ports = null;


            // loop until you get one good WMI query, then initialize everything
            while (older_ports == null)
            {
                try
                {
                    newer_ports = Serial.GetSortedSerialPortNames();
                    older_ports = newer_ports;

                    InvokeUpdateContextMenu(notifyIcon, newer_ports);
                }
                catch (WMIException e)
                {
                    notifyIcon.ShowBalloonTip(5000,
                            "SerialPop ERROR:",
                            e.Message,
                            ToolTipIcon.Error);
                }
                Thread.Sleep(1000);
            }

            while (true)
            {
                try
                {
                    newer_ports = Serial.GetSortedSerialPortNames();

                    if (!Enumerable.SequenceEqual(older_ports, newer_ports))
                    {
                        // the tooltip is limited to 64 characters :(
                        //notifyIcon.Text = Serial.FormatPortSummary(newer_ports).Substring(0, 63);

                        InvokeUpdateContextMenu(notifyIcon, newer_ports);

                        notifyIcon.ShowBalloonTip(5000,
                            "Serial ports changed:",
                            Serial.FormatDiff(older_ports, newer_ports),
                            ToolTipIcon.Info);
                    }

                    older_ports = newer_ports;
                }
                catch (WMIException e)
                {
                    notifyIcon.ShowBalloonTip(5000,
                            "SerialPop ERROR:",
                            e.Message,
                            ToolTipIcon.Error);
                }

                Thread.Sleep(1000);
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
