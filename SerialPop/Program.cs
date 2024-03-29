﻿using System;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using shared;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace SerialPop
{
    static class Program
    {
        static ConfigurationStruct currentConfiguration = Configuration.DefaultConfiguration;
        static bool configurationOutOfSync = false; // this is slightly race-condition-y

        static Action<string, string, ToolTipIcon> displayPopUp = null;

        const int POLLING_INTERVAL_MS = 1000; // test for new ports every second

        // used to signal the end of the polling loop
        static ManualResetEvent stopEvent = new ManualResetEvent(false);

        static Thread pollingThread = null;

        /*
         * Prepare the text for the pop-up by comparing old and new port entries.
         */
        public static string FormatDiff(IOrderedEnumerable<SerialPortDescriptor> old_pairs, IOrderedEnumerable<SerialPortDescriptor> newer_pairs)
        {
            // ignore the ports, only process descriptions
            IEnumerable<string> old = old_pairs.Select(t => t.Description);
            IEnumerable<string> newer = newer_pairs.Select(t => t.Description);

            StringBuilder sb = new StringBuilder();
            foreach (string s in old.Except(newer))
            {
                // removed devices
                sb.AppendFormat(" - {0}\n", s);
            }

            foreach (string s in newer.Except(old))
            {
                // new devices
                sb.AppendFormat(" + {0}\n", s);
            }

            return sb.ToString();
        }

        /*
         * Invoke the executable the user specified in Settings by replacing the placeholders 
         * in the arguments with the values according to the menu entry chosen.
         */
        static void InvokeExecutable(string comPort, string baudRate)
        {
            Debug.Assert(int.TryParse(baudRate, out _));

            try
            {
                // replace placeholders with their values
                string arguments = currentConfiguration.Arguments.Replace(
                    Configuration.COM_PORT_PATTERN, comPort).Replace(
                    Configuration.BAUD_RATE_PATTERN, baudRate);

                Process.Start(currentConfiguration.ExecutablePath, arguments);
            }
            catch (Exception e)
            {
                displayPopUp("SerialPop execution ERROR:", e.Message, ToolTipIcon.Error);
            }
        }


        /*
         * Dynamically populate the context menu to reflect the current COM ports.
         * Each menu entry represents a single port.
         * If more than one baud rate is specified, the port entry is nested, with
         * the next level of the menu showing all the available baud rates.
         */
        static void UpdateContextMenu(ContextMenuStrip strip, IOrderedEnumerable<SerialPortDescriptor> newerPorts)
        {
            strip.Close();
            strip.Items.Clear();

            string baudRateSuffix = "";

            // if there's only a single baud rate specified - add it to the description
            if (currentConfiguration.BaudRates.Count == 1)
            {
                baudRateSuffix = String.Format(" [{0}]", currentConfiguration.BaudRates[0]);
            }


            // key is COMYY address (might be null), value is description
            foreach (SerialPortDescriptor newPort in newerPorts)
            {
                ToolStripMenuItem portEntry = new ToolStripMenuItem
                {
                    AutoToolTip = false, // the tooltip lingers annoyingly
                    Text = newPort.Description + baudRateSuffix,
                    Image = Properties.Resources.usb,
                    Tag = newPort.Address
                };

                if (newPort.Address == null)
                {
                    // disable if there's no address
                    portEntry.Enabled = false;
                }
                else
                {
                    if (currentConfiguration.BaudRates.Count == 1)
                    {
                        // define a handler for the current item
                        portEntry.Click += PortEntry_Click;
                    }
                    else
                    {
                        // create several entries for baud rates, each with COM 
                        // port address as tag, and define a handler for them
                        foreach (int baudRate in currentConfiguration.BaudRates)
                        {
                            ToolStripMenuItem baudRateItem = new ToolStripMenuItem
                            {
                                AutoToolTip = false, // the tooltip lingers annoyingly
                                Text = baudRate.ToString(),
                                Image = Properties.Resources.speedometer,
                                Tag = newPort.Address
                            };
                            baudRateItem.Click += BaudRateItem_Click;

                            portEntry.DropDownItems.Add(baudRateItem);
                        }
                    }
                }

                strip.Items.Add(portEntry);
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

        private static void BaudRateItem_Click(object sender, EventArgs e)
        {
            // recall that the tag of each baud rateentry is the COM port,
            // and the text is the baud rate itself
            ToolStripMenuItem baudRateEntry = (ToolStripMenuItem)sender;
            InvokeExecutable((string)baudRateEntry.Tag,
                baudRateEntry.Text);
        }

        private static void PortEntry_Click(object sender, EventArgs e)
        {
            // should only be invoked if there's a single baud rate entry
            Debug.Assert(currentConfiguration.BaudRates.Count == 1);

            // recall that the tag of each port entry is the COM port
            ToolStripMenuItem portEntry = (ToolStripMenuItem)sender;
            InvokeExecutable((string)portEntry.Tag,
                currentConfiguration.BaudRates[0].ToString());
        }

        private static void SettingsButton_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(currentConfiguration);
            DialogResult result = settings.ShowDialog();

            if (result == DialogResult.OK)
            {
                // update using settings.currentConfiguration
                currentConfiguration = settings.currentConfiguration;
                configurationOutOfSync = true;
                try
                {
                    Configuration.Serialize(Configuration.GetDefaultPath(),
                        currentConfiguration);
                }
                catch (Exception ex)
                {
                    displayPopUp("SerialPop configuration ERROR:", ex.Message, ToolTipIcon.Error);
                }
            }
        }

        private static void QuitButton_Click(object sender, EventArgs e)
        {
            // signal the polling thread to quit
            stopEvent.Set();

            // wait for thread to end
            if (pollingThread != null)
            {
                pollingThread.Join();
            }

            // close the application in an orderly fashion
            Application.Exit();
        }

        static void InvokeUpdateContextMenu(NotifyIcon notifyIcon, IOrderedEnumerable<SerialPortDescriptor> newerPorts)
        {
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

        /*
         * If the configuration file is missing - try to create it (with the default configuration),
         * otherwise try to read an existing configuration.
         * 
         * On error display the error message and use the default configuration
         */
        static void LoadConfiguration()
        {
            try
            {
                if (!File.Exists(Configuration.GetDefaultPath()))
                {
                    currentConfiguration = Configuration.DefaultConfiguration;
                    Configuration.Serialize(Configuration.GetDefaultPath(), Configuration.DefaultConfiguration);
                }
                else
                {
                    currentConfiguration = Configuration.Deserialize(Configuration.GetDefaultPath());
                }

            }
            catch (Exception e)
            {
                displayPopUp("SerialPop configuration ERROR:", e.Message, ToolTipIcon.Error);
            }

            // validate the configuration
            if ((currentConfiguration.BaudRates.Count == 0) || (currentConfiguration.ExecutablePath.Length == 0))
            {
                displayPopUp("SerialPop configuration ERROR:", "The configuration appears invalid, using default configuration instead", ToolTipIcon.Error);
                currentConfiguration = Configuration.DefaultConfiguration;
            }
        }

        static void PollingLoop(object argument)
        {
            NotifyIcon notifyIcon = (NotifyIcon)argument;

            // GetSortedSerialPortNames() can throw, so initialization has to be trivial
            IOrderedEnumerable<SerialPortDescriptor> olderPorts = null;
            IOrderedEnumerable<SerialPortDescriptor> newerPorts = null;

            void notify(string title, string text, ToolTipIcon iconType)
            {
                notifyIcon.ShowBalloonTip(5000,
                            title,
                            text,
                            iconType);
            };

            displayPopUp = notify;

            LoadConfiguration();

            // populate the context menu with default values
            InvokeUpdateContextMenu(notifyIcon, new List<SerialPortDescriptor>().OrderBy(t => t));

            // loop until you get one good WMI query, then initialize everything
            while ((olderPorts == null) &&
                (false == stopEvent.WaitOne(POLLING_INTERVAL_MS)))
            {
                try
                {
                    newerPorts = Serial.GetSortedSerialPortNames();
                    olderPorts = newerPorts;

                    InvokeUpdateContextMenu(notifyIcon, newerPorts);
                }
                catch (WMIException e)
                {
                    displayPopUp("SerialPop ERROR:", e.Message, ToolTipIcon.Error);
                }
            }

            // keep polling until the event is signaled
            while (false == stopEvent.WaitOne(POLLING_INTERVAL_MS))
            {
                try
                {
                    newerPorts = Serial.GetSortedSerialPortNames();

                    if (!Enumerable.SequenceEqual(olderPorts, newerPorts))
                    {
                        InvokeUpdateContextMenu(notifyIcon, newerPorts);

                        displayPopUp("Serial ports changed:", FormatDiff(olderPorts, newerPorts), ToolTipIcon.Info);
                    }

                    olderPorts = newerPorts;
                }
                catch (WMIException e)
                {
                    displayPopUp("SerialPop ERROR:", e.Message, ToolTipIcon.Error);
                }

                if (configurationOutOfSync)
                {
                    configurationOutOfSync = false;
                    InvokeUpdateContextMenu(notifyIcon, newerPorts);
                }
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

                pollingThread = new Thread(new ParameterizedThreadStart(PollingLoop));
                pollingThread.IsBackground = false;
                pollingThread.Start(applicationContext.notifyIcon);

                Application.Run(applicationContext);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "SerialPop Caught an exception",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
