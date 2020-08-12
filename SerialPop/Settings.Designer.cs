namespace SerialPop
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblExe = new System.Windows.Forms.Label();
            this.txtboxExe = new System.Windows.Forms.TextBox();
            this.dlgBrowse = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtboxArgs = new System.Windows.Forms.TextBox();
            this.lblArgs = new System.Windows.Forms.Label();
            this.lblSample = new System.Windows.Forms.Label();
            this.txtboxSample = new System.Windows.Forms.TextBox();
            this.listBaudRates = new System.Windows.Forms.ListBox();
            this.lblBaudRates = new System.Windows.Forms.Label();
            this.txtboxNewBaudrate = new System.Windows.Forms.TextBox();
            this.btnDeleteBaudRate = new System.Windows.Forms.Button();
            this.btnAddBaudRate = new System.Windows.Forms.Button();
            this.settingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.settingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(16, 390);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(225, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(295, 390);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(216, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblExe
            // 
            this.lblExe.AutoSize = true;
            this.lblExe.Location = new System.Drawing.Point(13, 13);
            this.lblExe.Name = "lblExe";
            this.lblExe.Size = new System.Drawing.Size(146, 13);
            this.lblExe.TabIndex = 2;
            this.lblExe.Text = "Choose an executable to run:";
            // 
            // txtboxExe
            // 
            this.txtboxExe.Location = new System.Drawing.Point(16, 30);
            this.txtboxExe.Name = "txtboxExe";
            this.txtboxExe.Size = new System.Drawing.Size(495, 20);
            this.txtboxExe.TabIndex = 3;
            this.txtboxExe.TextChanged += new System.EventHandler(this.txtboxExe_TextChanged);
            // 
            // dlgBrowse
            // 
            this.dlgBrowse.FileName = "openFileDialog1";
            this.dlgBrowse.Filter = "Executable files (*.exe)|*.exe|Batch files (*.bat)|*.bat|All files (*.*)|*.*";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(16, 56);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtboxArgs
            // 
            this.txtboxArgs.Location = new System.Drawing.Point(16, 108);
            this.txtboxArgs.Name = "txtboxArgs";
            this.txtboxArgs.Size = new System.Drawing.Size(495, 20);
            this.txtboxArgs.TabIndex = 3;
            this.txtboxArgs.TextChanged += new System.EventHandler(this.txtboxArgs_TextChanged);
            // 
            // lblArgs
            // 
            this.lblArgs.AutoSize = true;
            this.lblArgs.Location = new System.Drawing.Point(13, 92);
            this.lblArgs.Name = "lblArgs";
            this.lblArgs.Size = new System.Drawing.Size(419, 13);
            this.lblArgs.TabIndex = 2;
            this.lblArgs.Text = "Specify arguments for the executable, using {COM} and {BAUDRATE} as placeholders:" +
    "";
            // 
            // lblSample
            // 
            this.lblSample.AutoSize = true;
            this.lblSample.Location = new System.Drawing.Point(13, 136);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(246, 13);
            this.lblSample.TabIndex = 2;
            this.lblSample.Text = "Arguments with COM6 and 115200 as an example:";
            // 
            // txtboxSample
            // 
            this.txtboxSample.Location = new System.Drawing.Point(16, 152);
            this.txtboxSample.Name = "txtboxSample";
            this.txtboxSample.ReadOnly = true;
            this.txtboxSample.Size = new System.Drawing.Size(495, 20);
            this.txtboxSample.TabIndex = 3;
            // 
            // listBaudRates
            // 
            this.listBaudRates.FormattingEnabled = true;
            this.listBaudRates.Location = new System.Drawing.Point(16, 216);
            this.listBaudRates.Name = "listBaudRates";
            this.listBaudRates.Size = new System.Drawing.Size(95, 121);
            this.listBaudRates.TabIndex = 5;
            this.listBaudRates.SelectedIndexChanged += new System.EventHandler(this.listBaudRates_SelectedIndexChanged);
            this.listBaudRates.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBaudRates_KeyDown);
            // 
            // lblBaudRates
            // 
            this.lblBaudRates.AutoSize = true;
            this.lblBaudRates.Location = new System.Drawing.Point(13, 189);
            this.lblBaudRates.Name = "lblBaudRates";
            this.lblBaudRates.Size = new System.Drawing.Size(98, 13);
            this.lblBaudRates.TabIndex = 2;
            this.lblBaudRates.Text = "Specify baud rates:";
            // 
            // txtboxNewBaudrate
            // 
            this.txtboxNewBaudrate.Location = new System.Drawing.Point(141, 216);
            this.txtboxNewBaudrate.Name = "txtboxNewBaudrate";
            this.txtboxNewBaudrate.Size = new System.Drawing.Size(100, 20);
            this.txtboxNewBaudrate.TabIndex = 6;
            this.txtboxNewBaudrate.TextChanged += new System.EventHandler(this.txtboxNewBaudrate_TextChanged);
            this.txtboxNewBaudrate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtboxNewBaudrate_KeyDown);
            // 
            // btnDeleteBaudRate
            // 
            this.btnDeleteBaudRate.Enabled = false;
            this.btnDeleteBaudRate.Location = new System.Drawing.Point(16, 344);
            this.btnDeleteBaudRate.Name = "btnDeleteBaudRate";
            this.btnDeleteBaudRate.Size = new System.Drawing.Size(95, 23);
            this.btnDeleteBaudRate.TabIndex = 7;
            this.btnDeleteBaudRate.Text = "Delete baud rate";
            this.btnDeleteBaudRate.UseVisualStyleBackColor = true;
            this.btnDeleteBaudRate.Click += new System.EventHandler(this.btnDeleteBaudRate_Click);
            // 
            // btnAddBaudRate
            // 
            this.btnAddBaudRate.Enabled = false;
            this.btnAddBaudRate.Location = new System.Drawing.Point(141, 242);
            this.btnAddBaudRate.Name = "btnAddBaudRate";
            this.btnAddBaudRate.Size = new System.Drawing.Size(100, 23);
            this.btnAddBaudRate.TabIndex = 7;
            this.btnAddBaudRate.Text = "Add baud rate";
            this.btnAddBaudRate.UseVisualStyleBackColor = true;
            this.btnAddBaudRate.Click += new System.EventHandler(this.btnAddBaudRate_Click);
            // 
            // settingsBindingSource
            // 
            this.settingsBindingSource.DataSource = typeof(SerialPop.Settings);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 427);
            this.Controls.Add(this.btnAddBaudRate);
            this.Controls.Add(this.btnDeleteBaudRate);
            this.Controls.Add(this.txtboxNewBaudrate);
            this.Controls.Add(this.listBaudRates);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtboxSample);
            this.Controls.Add(this.txtboxArgs);
            this.Controls.Add(this.txtboxExe);
            this.Controls.Add(this.lblBaudRates);
            this.Controls.Add(this.lblSample);
            this.Controls.Add(this.lblArgs);
            this.Controls.Add(this.lblExe);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.settingsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblExe;
        private System.Windows.Forms.TextBox txtboxExe;
        private System.Windows.Forms.OpenFileDialog dlgBrowse;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtboxArgs;
        private System.Windows.Forms.Label lblArgs;
        private System.Windows.Forms.Label lblSample;
        private System.Windows.Forms.TextBox txtboxSample;
        private System.Windows.Forms.ListBox listBaudRates;
        private System.Windows.Forms.Label lblBaudRates;
        private System.Windows.Forms.TextBox txtboxNewBaudrate;
        private System.Windows.Forms.Button btnDeleteBaudRate;
        private System.Windows.Forms.Button btnAddBaudRate;
        private System.Windows.Forms.BindingSource settingsBindingSource;
    }
}