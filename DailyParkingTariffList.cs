// Decompiled with JetBrains decompiler
// Type: TouchPark.DailyParkingTariffList
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TouchPark
{
  [XmlRoot("DailyParkingTariffList")]
  public class DailyParkingTariffList : List<DailyParkingTariff>
  {
    private static readonly string FilePath = string.Format("{0}\\DailyParkingTariffList.xml", (object) Environment.CurrentDirectory);

    public static DailyParkingTariffList Tariff
    {
      get
      {
        DailyParkingTariffList parkingTariffList = new DailyParkingTariffList();
        if (File.Exists(DailyParkingTariffList.FilePath))
        {
          try
          {
            TextReader textReader = (TextReader) new StreamReader(DailyParkingTariffList.FilePath);
            parkingTariffList = (DailyParkingTariffList) new XmlSerializer(typeof (DailyParkingTariffList)).Deserialize(textReader);
            textReader.Close();
          }
          catch (FileNotFoundException ex)
          {
          }
        }
        return parkingTariffList;
      }
      set
      {
        TextWriter textWriter = (TextWriter) new StreamWriter(DailyParkingTariffList.FilePath);
        new XmlSerializer(typeof (DailyParkingTariffList)).Serialize(textWriter, (object) value);
        textWriter.Close();
      }
    }
  }
}
