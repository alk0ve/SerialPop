using System.Linq;
using System.Text;
using System.Management;
using System.Collections.Generic;
using System.IO.Ports;
using System;

namespace shared
{
    public class WMIException: System.Exception
    {
        public WMIException()
        { }

        public WMIException(string message)
            : base(message)
        { }

        public WMIException(string message, System.Exception inner)
            : base(message, inner)
        { }

    }

    public struct SerialPortDescriptor
    {
        public SerialPortDescriptor(string address, string description)
        {
            Address = address;
            Description = description;
        }

        public string Address; // COMYY, can be null
        public string Description;
    }


    public class Serial
    {
        // may throw WMIException

        public static IOrderedEnumerable<SerialPortDescriptor> GetSortedSerialPortNames()
        {
            List<string> portDescriptions = new List<string>();

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT Caption FROM Win32_PnPEntity where ConfigManagerErrorCode = 0 and PNPClass=\"Ports\"");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    portDescriptions.Add((string)queryObj["Caption"]);
                }
            }
            catch (ManagementException e)
            {
                throw new WMIException("ERROR while querying WMI: " + e.Message);
            }

            // try to match a COMYY address to each textual entry, so we could use it as 
            IEnumerable<string> comAddresses = SerialPort.GetPortNames();

            // remove anything that doesn't look like COMYY
            comAddresses = comAddresses.Where(addr => (addr.StartsWith("COM", StringComparison.CurrentCultureIgnoreCase))); 

            List<SerialPortDescriptor> portNamesAndDescriptions = new List<SerialPortDescriptor>();

            // find a matching description for each port, using each description once
            // if no matching descriptions are found - use it as its own description
            foreach (string comAddress in comAddresses)
            {
                bool found = false;

                foreach (string portDescription in portDescriptions)
                {
                    if (portDescription.IndexOf(comAddress, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        found = true;
                        portNamesAndDescriptions.Add(new SerialPortDescriptor(comAddress, portDescription));
                        // use each description once
                        portDescriptions.Remove(portDescription);
                        break;
                    }
                }

                if (!found)
                {
                    // use the COM port's address as its own description
                    portNamesAndDescriptions.Add(new SerialPortDescriptor(comAddress, comAddress));
                }
            }


            // if any descriptions are left - add them with no port
            foreach (string portDescription in portDescriptions)
            {
                portNamesAndDescriptions.Add(new SerialPortDescriptor(null, portDescription));
            }


            return portNamesAndDescriptions.OrderBy(t => t.Address);

        }

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
    }
}
