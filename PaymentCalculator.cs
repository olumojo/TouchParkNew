// Decompiled with JetBrains decompiler
// Type: TouchPark.PaymentCalculator
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;

namespace TouchPark
{
  public class PaymentCalculator
  {
    private WeeklyParkingTariff _parkingCharges;
    private ParkingPermitInfo _ParkingPermit;

    public DayOfWeek DayOfWeek { get; set; }

    public PaymentCalculator()
    {
      this.DayOfWeek = DateTime.Today.DayOfWeek;
    }

    public ParkingPermitInfo ParkingPermit
    {
      get
      {
        return this._ParkingPermit;
      }
      set
      {
        this._ParkingPermit = value;
      }
    }

    public void SetPaymentAmountFor(ParkingPermitInfo parkingPermit)
    {
      this._ParkingPermit = parkingPermit;
      this._parkingCharges = new WeeklyParkingTariff(this._ParkingPermit.DurationOfStay, this.DayOfWeek);
      if (this._ParkingPermit.ShouldIssueParkingChargeNotice)
        return;
      this.SetPaymentAmountAndEndDate();
    }

    private void SetPaymentAmountAndEndDate()
    {
      this._parkingCharges.SetAmountFor(this._ParkingPermit);
      this._parkingCharges.SetEndDateFor(this._ParkingPermit);
    }
  }
}
