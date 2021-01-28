// Decompiled with JetBrains decompiler
// Type: TouchPark.Controls.PaymentButton
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.Drawing;
using TouchPark.Properties;

namespace TouchPark.Controls
{
  internal class PaymentButton : TouchParkButton
  {
    private PaymentButton.Interval _tariffInterval = PaymentButton.Interval.Weekly;
    private string _title = string.Empty;
    private Decimal _amount = new Decimal(1);
    private DateTime _startDateTime = DateTime.MinValue;

    public PaymentButton.Interval TariffInterval
    {
      get
      {
        return this._tariffInterval;
      }
      set
      {
        this._tariffInterval = value;
        switch (this._tariffInterval)
        {
          case PaymentButton.Interval.Daily:
            this.BackgroundImage = (Image) Resources.BlueKey;
            break;
          case PaymentButton.Interval.Weekly:
            this.BackgroundImage = (Image) Resources.GreenKey;
            break;
        }
      }
    }

    public string Title
    {
      get
      {
        return this._title;
      }
      set
      {
        this._title = value;
      }
    }

    public int Index { get; private set; }

    public TimeSpan DurationOfStay { get; private set; }

    public Decimal Amount
    {
      get
      {
        return this._amount;
      }
      private set
      {
        this._amount = value;
      }
    }

    public void SetText()
    {
      if (this.ForceFixedEndDateTime)
        this.SetText(this.EndDate);
      else
        this.SetText(this._startDateTime.Add(this.DurationOfStay));
    }

    public void SetText(TimeSpan leaveTime)
    {
      this.SetText(DateTime.Today.Add(leaveTime));
    }

    private void SetText(DateTime endTime)
    {
      string shortTimeString = endTime.ToShortTimeString();
      string str = string.Empty;
      if (!string.IsNullOrEmpty(this.Title))
        str = string.Format("{0}\n", (object) this.Title);
      this.Text = string.Format("{0} Stay Until\n\r {1} Hrs\n\r {2}", (object) str, (object) shortTimeString, (object) this._amount.ToString("C"));
    }

    internal PaymentButton(
      int index,
      TimeSpan durationOfStay,
      DateTime startDateTime,
      Decimal amount)
    {
      this._startDateTime = startDateTime;
      this.Index = index;
      this.DurationOfStay = durationOfStay;
      this.Amount = amount;
      this.BackgroundImage = (Image) Resources.GreenKey;
      this.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.Name = string.Format("paymentButton{0}", (object) this.Index);
      this.Size = new Size(115, 115);
      this.TabIndex = this.Index;
      this.SetText();
    }

    public DateTime EndDate { get; set; }

    public bool ForceFixedEndDateTime { get; set; }

    internal enum Interval
    {
      Daily,
      Weekly,
    }
  }
}
