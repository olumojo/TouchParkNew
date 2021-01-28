// Decompiled with JetBrains decompiler
// Type: TouchPark.Program
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using TouchPark.CashDevices;
using TouchPark.Properties;

namespace TouchPark
{
  internal static class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      try
      {
        Log.Write("starting touchpark " + DateTime.Now.ToString());
        Log.Write("permit post url LIVE : " + ConfigurationManager.AppSettings["RangerLiveURL"]);
        Log.Write("permit post url TEST : " + ConfigurationManager.AppSettings["RangerTestURL"]);
        Log.Write("Database connection string : " + Settings.Default.DatabaseConnectionString);
        Thread.CurrentThread.Name = "Main Thread";
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.ThreadException += new ThreadExceptionEventHandler(Program.AppThreadException);
        List<string> stringList = new List<string>(args.Length);
        stringList.AddRange((IEnumerable<string>) args);
        if (stringList.Contains("/config"))
          Application.Run((Form) new ConfigurationForm());
        else
          Application.Run((Form) new frmStartScreen());
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    private static void AppThreadException(object source, ThreadExceptionEventArgs e)
    {
      Program.ProcessUnhandledException(e.Exception);
    }

    private static void ProcessUnhandledException(Exception ex)
    {
      Log.Write(ex, true);
      if (!(ex is CoinMechanismException))
        return;
      int num = (int) new OutOfOrderForm(false).ShowDialog();
      Application.ExitThread();
    }
  }
}
