using System;
using System.Linq;
using System.Threading;
using System.Management;
using shared;
using System.Collections.Generic;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrderedEnumerable<SerialPortDescriptor> olderPorts = Serial.GetSortedSerialPortNames();
            IOrderedEnumerable<SerialPortDescriptor> newerPorts = olderPorts;

            while (true)
            {
                Thread.Sleep(1000);

                newerPorts = Serial.GetSortedSerialPortNames();

                if (!Enumerable.SequenceEqual(olderPorts, newerPorts))
                {
                    Console.Write(Serial.FormatDiff(olderPorts, newerPorts));
                }

                olderPorts = newerPorts;
            }
        }
    }
}
