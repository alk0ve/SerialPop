using System.Linq;
using System.Text;
using System.IO.Ports;

namespace shared
{
    public class Serial
    {
        public static IOrderedEnumerable<string> GetSortedSerialPortNames()
        {
            return SerialPort.GetPortNames().OrderBy(t => t); // returns COM1, COM5, etc.
            
        }

        public static string GetDiff(IOrderedEnumerable<string> old, IOrderedEnumerable<string> newer)
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
