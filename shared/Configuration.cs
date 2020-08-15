using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace shared
{
    /*
     * SerialPop's configuration - what to execute through the context menu and with which
     * baud rates.
     */
    public struct ConfigurationStruct
    {
        public string ExecutablePath;
        public string Arguments;
        public List<int> BaudRates;
    }


    /*
     * The Configuration class handles (de)serialization of the configuration struct
     * to/from XML.
     */
    public class Configuration
    {
        public const string CONFIGURATION_FILE_NAME = "config.xml";

        public const string COM_PORT_PATTERN = "{COM}";
        public const string BAUD_RATE_PATTERN = "{BAUDRATE}";

        public const string DEFAULT_EXECUTABLE = "putty.exe";
        public const string DEFAULT_ARGUMENTS = "-serial " + COM_PORT_PATTERN + " -sercfg " + BAUD_RATE_PATTERN + ",8,n,1,N";
        public static readonly List<int> DEFAULT_BAUD_RATES = new List<int>() { 57600, 115200 };

        public static readonly ConfigurationStruct DefaultConfiguration = new ConfigurationStruct() { ExecutablePath = DEFAULT_EXECUTABLE,
            Arguments = DEFAULT_ARGUMENTS, BaudRates = DEFAULT_BAUD_RATES
        };

        // we'll use this path to store the configuration file.
        public static string GetDefaultPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONFIGURATION_FILE_NAME);
        }

        // will throw on error
        public static ConfigurationStruct Deserialize(string configurationPath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ConfigurationStruct));
            using (var reader = new StreamReader(configurationPath))
            {
                return (ConfigurationStruct)ser.Deserialize(reader);
            }
        }

        // will throw on error
        public static void Serialize(string configurationPath, ConfigurationStruct configuration)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ConfigurationStruct));
            using (var writer = new StreamWriter(configurationPath))
            {
                ser.Serialize(writer, configuration);
            }
        }
    }
}
