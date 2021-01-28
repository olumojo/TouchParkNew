// Decompiled with JetBrains decompiler
// Type: TouchPark.DailyParkingTariff
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Types;
using System;
using System.Xml.Serialization;

namespace TouchPark
{
  [XmlRoot]
  public struct DailyParkingTariff
  {
    [XmlElement("ActivePeriod")]
    public TimePeriod ActivePeriod;
    [XmlElement("Amount")]
    public Decimal Amount;
    [XmlElement("Name")]
    public string Name;
    [XmlElement("IsFixedEndTime")]
    public bool IsFixedEndTime;
  }
}
