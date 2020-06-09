using System;
using System.Linq;
using System.Threading;
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
                Thread.Sleep(1000);

                newer_ports = Serial.GetSortedSerialPortNames();

                if (!Enumerable.SequenceEqual(older_ports, newer_ports))
                {
                    Console.Write(Serial.FormatDiff(older_ports, newer_ports));
                }

                older_ports = newer_ports;
            }
        }
    }
}
