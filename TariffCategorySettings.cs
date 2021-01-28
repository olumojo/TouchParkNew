// Decompiled with JetBrains decompiler
// Type: TouchPark.TariffCategorySettings
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System.Collections.Generic;
using TouchPark.Properties;

namespace TouchPark
{
  internal static class TariffCategorySettings
  {
    internal static List<string> EnabledTariffCategories()
    {
      List<string> stringList = new List<string>();
      if (Settings.Default.EnableTariffCategoryCar)
        stringList.Add("CAR");
      if (Settings.Default.EnableTariffCategoryHGV)
        stringList.Add("HGV");
      if (Settings.Default.EnableTariffCategoryHGVFood)
        stringList.Add("HGVF");
      if (Settings.Default.EnableTariffCategoryHGVTrailer)
        stringList.Add("HGVTRAILER");
      if (Settings.Default.EnableTariffCategoryStaff)
        stringList.Add("STAFF");
      if (Settings.Default.EnableTariffCategoryVisitor)
        stringList.Add("VISITOR");
      if (Settings.Default.EnableTariffCategoryGuest)
        stringList.Add("GUEST");
      if (Settings.Default.EnableTariffCategoryAccountHolder)
        stringList.Add("ACCOUNT_HOLDER");
      if (Settings.Default.EnableTariffCategoryCoach)
        stringList.Add("COACH");
      return stringList;
    }

    internal static string DecidePermitType(string paymentType)
    {
      switch (paymentType)
      {
        case "CAR":
        case "HGV":
        case "HGVF":
        case "HGVTRAILER":
          return "VISITOR";
        case "STAFF":
          return "STAFF";
        case "VISITOR":
        case "GUEST":
          return "VISITOR";
        case "ACCOUNT_HOLDER":
          return "VISITOR";
        case "COACH":
          return "VISITOR";
        default:
          return "VISITOR";
      }
    }
  }
}
