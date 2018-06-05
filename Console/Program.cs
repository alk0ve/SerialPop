using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using shared;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrderedEnumerable<string> older_ports = Serial.GetSortedSerialPortNames();
            IOrderedEnumerable<string> newer_ports = older_ports;

            while (true)
            {
                System.Threading.Thread.Sleep(1000);

                newer_ports = Serial.GetSortedSerialPortNames();

                if (!Enumerable.SequenceEqual(older_ports, newer_ports))
                {
                    Console.Write(Serial.GetDiff(older_ports, newer_ports));
                }

                older_ports = newer_ports;
            }
        }
    }
}
