// Decompiled with JetBrains decompiler
// Type: TouchPark.NumberBox
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TouchPark
{
  public class NumberBox : TextBox
  {
    private double _number;

    public NumberBox()
    {
      this.MaxNumber = 99.99;
      this.MinNumber = 0.25;
    }

    public double MinNumber { get; set; }

    public double MaxNumber { get; set; }

    public double Number
    {
      get
      {
        return this._number;
      }
      set
      {
        try
        {
          if (value > this.MaxNumber)
            value = this.MaxNumber;
          if (value < this.MinNumber)
            value = this.MinNumber;
          this.Text = value.ToString();
          this._number = value;
        }
        catch (Exception ex)
        {
        }
      }
    }

    public Decimal NumberDecimal
    {
      get
      {
        return (Decimal) this._number;
      }
      set
      {
        this.Number = (double) value;
      }
    }

    private string AppendZeroToString(string n, NumberBox.Position position)
    {
      if (n.Length < 2)
      {
        switch (position)
        {
          case NumberBox.Position.Before:
            n = string.Format("0{0}", (object) n);
            break;
          case NumberBox.Position.After:
            n = string.Format("{0}0", (object) n);
            break;
        }
      }
      return n;
    }

    protected override void OnValidating(CancelEventArgs e)
    {
      if (!double.TryParse(this.Text, out this._number))
        e.Cancel = true;
      base.OnValidating(e);
    }

    private enum Position
    {
      Before,
      After,
    }
  }
}
