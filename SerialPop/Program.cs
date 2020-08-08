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

        static void UpdateContextMenu(ContextMenuStrip strip, IOrderedEnumerable<SerialPortDescriptor> newerPorts)
        {
            strip.Close();
            strip.Items.Clear();

            // key is COMYY address (might be null), value is description
            foreach (SerialPortDescriptor newPort in newerPorts)
            {
                strip.Items.Add(new ToolStripMenuItem
                {
                    AutoToolTip = false, // the tooltip lingers annoyingly
                    Text = newPort.Description,
                    Image = Properties.Resources.usb,
                    Tag = newPort.Address,
                    Enabled = (newPort.Address != null) // disable if there's no address

                    // TODO use .DropDownItems to nest items 
                });
            }

            // add a separator
            strip.Items.Add(new ToolStripSeparator());

            // add a settings button
            ToolStripMenuItem settingsButton = new ToolStripMenuItem
            {
                AutoToolTip = false, // the tooltip lingers annoyingly
                Text = "Settings",
                Image = Properties.Resources.coding
            };
            settingsButton.Click += SettingsButton_Click;
            strip.Items.Add(settingsButton);

            // add a quit button
            ToolStripMenuItem quitButton = new ToolStripMenuItem
            {
                AutoToolTip = false, // the tooltip lingers annoyingly
                Text = "Quit",
                Image = Properties.Resources.exit
            };
            quitButton.Click += QuitButton_Click;
            strip.Items.Add(quitButton);

        }

        private static void SettingsButton_Click(object sender, EventArgs e)
        {
            // TODO implement
        }

        private static void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        static void InvokeUpdateContextMenu(NotifyIcon notifyIcon, IOrderedEnumerable<SerialPortDescriptor> newerPorts)
        {
            //notifyIcon.ContextMenuStrip.


            if (notifyIcon.ContextMenuStrip.InvokeRequired)
            {
                // invoke the update on the UI thread
                notifyIcon.ContextMenuStrip.Invoke((MethodInvoker)delegate
                {
                    UpdateContextMenu(notifyIcon.ContextMenuStrip, newerPorts);
                });
            }
            else
            {
                UpdateContextMenu(notifyIcon.ContextMenuStrip, newerPorts);
            }
        }

        static void PollLoop(object argument)
        {
            NotifyIcon notifyIcon = (NotifyIcon)argument;

            // GetSortedSerialPortNames() can throw, so initialization has to be trivial
            IOrderedEnumerable<SerialPortDescriptor> olderPorts = null;
            IOrderedEnumerable<SerialPortDescriptor> newerPorts = null;


            // loop until you get one good WMI query, then initialize everything
            while (olderPorts == null)
            {
                try
                {
                    newerPorts = Serial.GetSortedSerialPortNames();
                    olderPorts = newerPorts;

                    InvokeUpdateContextMenu(notifyIcon, newerPorts);
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
                    newerPorts = Serial.GetSortedSerialPortNames();

                    if (!Enumerable.SequenceEqual(olderPorts, newerPorts))
                    {
                        // the tooltip is limited to 64 characters :(
                        //notifyIcon.Text = Serial.FormatPortSummary(newer_ports).Substring(0, 63);

                        InvokeUpdateContextMenu(notifyIcon, newerPorts);

                        notifyIcon.ShowBalloonTip(5000,
                            "Serial ports changed:",
                            Serial.FormatDiff(olderPorts, newerPorts),
                            ToolTipIcon.Info);
                    }

                    olderPorts = newerPorts;
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
