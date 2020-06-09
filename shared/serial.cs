using System.Linq;
using System.Text;
using System.Management;
using System.Collections;
using System.Collections.Generic;

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


    public class Serial
    {
        // may throw WMIException
        public static IOrderedEnumerable<string> GetSortedSerialPortNames()
        {
            List<string> portNames = new List<string>();

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT Caption FROM Win32_PnPEntity where ConfigManagerErrorCode = 0 and PNPClass=\"Ports\"");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    portNames.Add((string)queryObj["Caption"]);
                }
            }
            catch (ManagementException e)
            {
                throw new WMIException("ERROR while querying WMI: " + e.Message);
            }

            return portNames.OrderBy(t => t);
            
        }

        public static string FormatPortSummary(IOrderedEnumerable<string> currentPorts)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string port in currentPorts)
            {
                sb.AppendLine(port);
            }

            return sb.ToString();
        }

        public static string FormatDiff(IOrderedEnumerable<string> old, IOrderedEnumerable<string> newer)
        {
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
