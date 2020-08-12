using shared;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPop
{
    public partial class Settings : Form
    {
        public ConfigurationStruct currentConfiguration;

        public Settings(ConfigurationStruct configuration)
        {
            InitializeComponent();
            currentConfiguration = configuration;

            // initialize the form according to the configuration
            txtboxExe.Text = configuration.ExecutablePath;
            txtboxArgs.Text = configuration.Arguments;
            txtboxArgs_TextChanged(this, null);

            foreach (int baudRate in configuration.BaudRates)
            {
                listBaudRates.Items.Add(baudRate);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgBrowse.InitialDirectory = Path.GetDirectoryName(txtboxExe.Text);

            if (dlgBrowse.ShowDialog() == DialogResult.OK)
            {
                currentConfiguration.ExecutablePath = txtboxExe.Text = dlgBrowse.FileName;
            }
        }

        private void txtboxArgs_TextChanged(object sender, EventArgs e)
        {
            currentConfiguration.Arguments = txtboxArgs.Text;
            txtboxSample.Text = txtboxArgs.Text.Replace(Configuration.COM_PORT_PATTERN, "COM6").Replace(Configuration.BAUD_RATE_PATTERN, "115200");
        }

        private void listBaudRates_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeleteBaudRate.Enabled = ((listBaudRates.SelectedIndices.Count > 0) && (listBaudRates.Items.Count > 1));
        }

        private void btnAddBaudRate_Click(object sender, EventArgs e)
        {
            int newBaudRate = 0;
            if (int.TryParse(txtboxNewBaudrate.Text, out newBaudRate) &&
                !currentConfiguration.BaudRates.Contains(newBaudRate))
            {
                currentConfiguration.BaudRates.Add(newBaudRate);
                listBaudRates.Items.Add(newBaudRate);
                listBaudRates.ClearSelected();
            }
        }

        private void btnDeleteBaudRate_Click(object sender, EventArgs e)
        {
            currentConfiguration.BaudRates.RemoveAt(listBaudRates.SelectedIndex);
            listBaudRates.Items.RemoveAt(listBaudRates.SelectedIndex);
            listBaudRates.ClearSelected();

            if (listBaudRates.Items.Count == 1)
            {
                btnDeleteBaudRate.Enabled = false;
            }
        }

        private void txtboxNewBaudrate_TextChanged(object sender, EventArgs e)
        {
            btnAddBaudRate.Enabled = int.TryParse(txtboxNewBaudrate.Text, out _);
        }

        private void txtboxNewBaudrate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddBaudRate_Click(this, new EventArgs());
            }
        }

        private void listBaudRates_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                btnDeleteBaudRate_Click(this, new EventArgs());
            }
        }

        private void txtboxExe_TextChanged(object sender, EventArgs e)
        {
            currentConfiguration.ExecutablePath = txtboxExe.Text;
        }
    }
}
