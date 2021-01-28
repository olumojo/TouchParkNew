// Decompiled with JetBrains decompiler
// Type: TouchPark.ParkingPermitInfo
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using TouchPark.Properties;

namespace TouchPark
{
  public class ParkingPermitInfo
  {
    public ParkingPermitInfo()
    {
      this.CaptureDate = DateTime.Now;
      this.StartDate = DateTime.Now;
      this.EndDate = DateTime.Now;
    }

    public string PermitType { get; set; }

    public DateTime CaptureDate { get; set; }

    public string VehicleRegMark { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string PaymentType { get; set; }

    public Decimal Amount { get; set; }

    public string AuthCode { get; set; }

    public Decimal Paid { get; set; }

    public string MachineName { get; set; }

    public string UserName { get; set; }

    public int UserID { get; set; }

    public string PassCode { get; set; }

    public bool ShouldIssueParkingChargeNotice
    {
      get
      {
        return this.DurationOfStayLessGracePeriod > Settings.Default.MaximumPermitHours;
      }
    }

    public double DurationOfStayLessGracePeriod
    {
      get
      {
        return this.DurationOfStay.TotalHours - Settings.Default.GracePeriod;
      }
    }

    public double DurationOfStayAddGracePeriod
    {
      get
      {
        return this.DurationOfStay.TotalHours + Settings.Default.GracePeriod;
      }
    }

    public TimeSpan DurationOfStay
    {
      get
      {
        return new TimeSpan().Subtract(this.StartDate.Subtract(this.EndDate));
      }
    }

    public ParkingPermitInfo copy(ParkingPermitInfo originalParkingPermitInfo)
    {
      this.PermitType = originalParkingPermitInfo.PermitType;
      this.CaptureDate = originalParkingPermitInfo.CaptureDate;
      this.VehicleRegMark = originalParkingPermitInfo.VehicleRegMark;
      this.StartDate = originalParkingPermitInfo.StartDate;
      this.EndDate = originalParkingPermitInfo.EndDate;
      this.PaymentType = originalParkingPermitInfo.PaymentType;
      this.Amount = originalParkingPermitInfo.Amount;
      this.AuthCode = originalParkingPermitInfo.AuthCode;
      this.Paid = originalParkingPermitInfo.Paid;
      this.MachineName = originalParkingPermitInfo.MachineName;
      this.UserName = originalParkingPermitInfo.UserName;
      this.UserID = originalParkingPermitInfo.UserID;
      this.PassCode = originalParkingPermitInfo.PassCode;
      return this;
    }
  }
}
