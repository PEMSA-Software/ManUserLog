using Comun;
using System;
using System.Windows.Forms;

namespace ManUserLog
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      if (!new _ManUserLogLogica().Main())
        return;
      string sysDbBase = Sistema.sysDBBase;
      Sistema.sysDBInUse = Sistema.sysDBBase;
            switch (Sistema.sysOpCode)
            {
                case "00":
                    Application.Run(new ManUserLogPpal());
                    break;
            }
        }
  }
}
