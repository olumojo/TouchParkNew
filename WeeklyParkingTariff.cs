// Decompiled with JetBrains decompiler
// Type: TouchPark.WeeklyParkingTariff
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.Collections.Generic;
using System.Configuration;
using TouchPark.Properties;

namespace TouchPark
{
  internal class WeeklyParkingTariff
  {
    private DayOfWeek _DayOfWeek;

    public TimeSpan ParkingDuration { get; set; }

    internal WeeklyParkingTariff(TimeSpan parkingDuration)
      : this(parkingDuration, DateTime.Today.DayOfWeek)
    {
    }

    internal WeeklyParkingTariff(TimeSpan parkingDuration, DayOfWeek dayOfWeek)
    {
      this.ParkingDuration = parkingDuration;
      this._DayOfWeek = dayOfWeek;
      this.CreatePaymentOptionsByCurrentDay();
    }

    private void CreatePaymentOptionsByCurrentDay()
    {
      switch (this._DayOfWeek)
      {
        case DayOfWeek.Sunday:
        case DayOfWeek.Saturday:
          this.CreatePaymentOptionArrays(WeeklyParkingTariff.TariffType.WeekEnd);
          break;
        case DayOfWeek.Monday:
        case DayOfWeek.Tuesday:
        case DayOfWeek.Wednesday:
        case DayOfWeek.Thursday:
        case DayOfWeek.Friday:
          this.CreatePaymentOptionArrays(WeeklyParkingTariff.TariffType.WeekDay);
          break;
      }
    }

    internal void CreatePaymentOptionArrays(WeeklyParkingTariff.TariffType tariffValidity)
    {
      this.TariffValidity = tariffValidity;
      switch (tariffValidity)
      {
        case WeeklyParkingTariff.TariffType.WeekDay:
          this.CreateWeekDayPaymentOptionArray();
          break;
        case WeeklyParkingTariff.TariffType.WeekEnd:
          this.CreateWeekEndPaymentOptionArray();
          break;
      }
    }

    public string[] HourOptions { get; private set; }

    public TimeSpan[] StayDurationOptions { get; private set; }

    public Decimal[] AmountOptions { get; private set; }

    public int ValidOptionsCount { get; private set; }

    public WeeklyParkingTariff.TariffType TariffValidity { get; private set; }

    public int MAX_PAYMENT_OPTIONS
    {
      get
      {
        return Settings.Default.MaxNumberOfPaymentOptions;
      }
    }

    private void CreateWeekDayPaymentOptionArray()
    {
      this.CreatePaymentOptionArray("paymentOptionWeekDay", "paymentAmountWeekDay");
    }

    private void CreateWeekEndPaymentOptionArray()
    {
      this.CreatePaymentOptionArray("paymentOptionWeekEnd", "paymentAmountWeekEnd");
    }

    private void CreatePaymentOptionArray(string optionString, string paymentString)
    {
      int maxPaymentOptions = this.MAX_PAYMENT_OPTIONS;
      this.HourOptions = new string[maxPaymentOptions];
      this.AmountOptions = new Decimal[maxPaymentOptions];
      this.StayDurationOptions = new TimeSpan[maxPaymentOptions];
      bool flag = true;
      int num = 0;
      int index1 = 0;
      while (flag)
      {
        ++num;
        if (num <= maxPaymentOptions)
        {
          string index2 = string.Format("{0}{1}", (object) optionString, (object) num.ToString());
          string index3 = string.Format("{0}{1}", (object) paymentString, (object) num.ToString());
          string appSetting1 = ConfigurationManager.AppSettings[index2];
          string appSetting2 = ConfigurationManager.AppSettings[index3];
          if (!string.IsNullOrEmpty(appSetting1) && !string.IsNullOrEmpty(appSetting2))
          {
            TimeSpan durationFromTariff = this.GetDurationFromTariff(appSetting1);
            if (durationFromTariff >= this.ParkingDuration)
            {
              this.StayDurationOptions[index1] = durationFromTariff;
              this.HourOptions[index1] = appSetting1;
              this.AmountOptions[index1] = this.GetAmount(appSetting2);
              ++index1;
            }
          }
          else
            flag = false;
        }
        else
          flag = false;
      }
      this.ValidOptionsCount = index1;
    }

    private Decimal GetAmount(string value)
    {
      try
      {
        return Decimal.Parse(value);
      }
      catch (Exception ex)
      {
        return new Decimal(0);
      }
    }

    public Decimal GetFirstParkingCharge()
    {
      return this.AmountOptions[0];
    }

    internal TimeSpan GetDurationFromTariff(string value)
    {
      try
      {
        return new TimeSpan(int.Parse(value.Remove(2).Trim()), 0, 0);
      }
      catch (Exception ex)
      {
        return TimeSpan.MinValue;
      }
    }

    internal TimeSpan GetDurationFromTariff(int index)
    {
      try
      {
        return this.StayDurationOptions[index];
      }
      catch (Exception ex)
      {
        return TimeSpan.Zero;
      }
    }

    internal void SetAmountFor(ParkingPermitInfo parkingPermitInfo)
    {
      parkingPermitInfo.Amount = this.GetParkingChargeFor(parkingPermitInfo.DurationOfStay);
    }

    internal void SetEndDateFor(ParkingPermitInfo parkingPermitInfo)
    {
      parkingPermitInfo.EndDate = parkingPermitInfo.StartDate.AddHours(parkingPermitInfo.DurationOfStayAddGracePeriod);
    }

    private Decimal GetParkingChargeFor(TimeSpan durationOfStay)
    {
      Decimal num = new Decimal(0);
      SortedList<TimeSpan, Decimal> sortedList = new SortedList<TimeSpan, Decimal>();
      for (int index = 0; index < this.MAX_PAYMENT_OPTIONS; ++index)
      {
        TimeSpan durationFromTariff = this.GetDurationFromTariff(index);
        if (durationFromTariff != TimeSpan.MinValue && durationFromTariff != TimeSpan.Zero)
          sortedList.Add(durationFromTariff, this.AmountOptions[index]);
      }
      foreach (KeyValuePair<TimeSpan, Decimal> keyValuePair in sortedList)
      {
        num = keyValuePair.Value;
        if (keyValuePair.Key > durationOfStay)
          break;
      }
      return num;
    }

    internal enum TariffType
    {
      Fixed,
      WeekDay,
      WeekEnd,
    }
  }
}
