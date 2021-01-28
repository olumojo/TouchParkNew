// Decompiled with JetBrains decompiler
// Type: TouchPark.Utilites
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using TouchPark.Properties;

namespace TouchPark
{
  public class Utilites
  {
    public static bool WriteParkingPermitToXML(ParkingPermitInfo parkingPermit)
    {
      CUpdateVehicleData cupdateVehicleData = new CUpdateVehicleData();
      return CCacheData.WriteParkingPermitToXmlFile(parkingPermit);
    }

    public static void LogError(string method, string message)
    {
      LogData logData = new LogData();
      logData.Severity = TraceEventType.Error;
      logData.Message = message ;
      logData.Categories.Clear();
      logData.Categories.Add("Exception");
      logData.Title = "Unspecifed Error";
      Log.Write(logData);
    }

    internal static void WriteToLog(string message)
    {
      Utilites.WriteToLog(message, TraceEventType.Information);
    }

    internal static void WriteToLog(string message, TraceEventType severity)
    {
      Utilites.WriteToLog("Unspecified Log Entry", message, severity);
    }

    internal static void WriteToLog(string title, string message, TraceEventType severity)
    {
      LogData logData = new LogData();
      logData.Title = title;
      logData.Message = message;
      logData.Severity = severity;
      Log.Write(logData);
    }

    internal static void WriteToPictureBox(PictureBox pictureBox, string data)
    {
      try
      {
        Graphics graphics = Graphics.FromImage(pictureBox.Image);
        int pageUnit = (int) graphics.PageUnit;
        graphics.DrawString(data, new Font("Ariel", 40f), Brushes.Black, new PointF(10f, 10f));
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    internal static void DrawRegistrationMark(PictureBox pictureBox, CVehicleInfo carDataItem)
    {
      if (!Settings.Default.PaintVehicleRegistrationMarks)
        return;
      string vrm = carDataItem.VRM;
      Utilites.WriteToPictureBox(pictureBox, vrm);
    }

    internal static bool InternetAlive()
    {
      string appSetting = ConfigurationManager.AppSettings["InternetAliveCheckSite"];
      if (!string.IsNullOrEmpty(appSetting))
        return Utilites.WebsiteAccessible(appSetting);
      return false;
    }

    internal static bool WebsiteAccessible(string URL)
    {
      try
      {
        return new Ping().Send(URL, 100).Status == IPStatus.Success;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
