using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;


namespace SerialPop
{
    public class CustomApplicationContext : ApplicationContext
    {
        private static readonly string IconFileName = "USB.ico";
        private static readonly string DefaultTooltip = "SerialPop";

		public CustomApplicationContext() 
		{
			InitializeContext();
		}


        private System.ComponentModel.IContainer components;
        public NotifyIcon notifyIcon;

        private void InitializeContext()
        {
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components)
                             {
                                 //ContextMenuStrip = new ContextMenuStrip(),
                                 Icon = new Icon(IconFileName),
                                 Text = DefaultTooltip,
                                 Visible = true
                             };
        }

		protected override void Dispose( bool disposing )
		{
			if( disposing && components != null) { components.Dispose(); }
		}

        protected override void ExitThreadCore()
        {
            notifyIcon.Visible = false; // should remove lingering tray icon
            base.ExitThreadCore();
        }

    }
}
