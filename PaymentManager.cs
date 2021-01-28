// Decompiled with JetBrains decompiler
// Type: TouchPark.PaymentManager
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;

namespace TouchPark
{
  public static class PaymentManager
  {
    public static DayOfWeek DayOfWeek { get; set; }

    public static bool DebugMode { get; set; }

    public static void ProcessPayment(ParkingPermitInfo parkingPermitInfo)
    {
      PaymentCalculator paymentCalculator = new PaymentCalculator();
      if (PaymentManager.DebugMode)
        paymentCalculator.DayOfWeek = PaymentManager.DayOfWeek;
      paymentCalculator.SetPaymentAmountFor(parkingPermitInfo);
      if (parkingPermitInfo.ShouldIssueParkingChargeNotice)
      {
        if (PaymentManager.DebugMode)
          return;
        new ParkingExpiredForm(parkingPermitInfo, "DisplayPayment").Show();
      }
      else
      {
        if (PaymentManager.DebugMode)
          return;
        int num = (int) new ParkingChargesForm(parkingPermitInfo).ShowDialog();
      }
    }
  }
}
