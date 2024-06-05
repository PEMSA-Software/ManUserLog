
using MonitoringWorks.DataAccess;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ManUserLog
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1) { }
            else
            {
                if (!new _ManUserLogLogica().Main()) return;

                General.sysOpCode = "00";
                switch (General.sysOpCode)
                {
                    case "00":
                        Application.Run(new UsuariosConectados());
                        break;
                }
            }
        } 
    }
}
