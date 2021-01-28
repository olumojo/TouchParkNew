// Decompiled with JetBrains decompiler
// Type: TouchPark.VehicleMovementDataGenerator
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.Data;
using System.IO;
using System.Xml;
using TouchPark.Properties;

namespace TouchPark
{
  public class VehicleMovementDataGenerator
  {
    private Random rnd = new Random();
    private DataTable _vehicleMovementData;

    public VehicleMovementDataGenerator()
    {
      this._vehicleMovementData = CCacheData.CreateEmptyVehicleDataCacheIfNotExists();
      int num = (int) this._vehicleMovementData.ReadXml(XmlReader.Create((TextReader) new StringReader(Settings.Default.DemoVehicleData)));
    }

    private double RandomHour()
    {
      return Math.Round(this.rnd.NextDouble() * 10.0, 2);
    }

    public void ResetDatesAndTimes()
    {
      foreach (DataRow row in (InternalDataCollectionBase) this._vehicleMovementData.Rows)
        row["EventDateTime"] = (object) DateTime.Now.AddHours(0.0 - this.RandomHour());
    }

    public void Save()
    {
      CCacheData.WriteVehicleDataToXml(this._vehicleMovementData);
    }
  }
}
