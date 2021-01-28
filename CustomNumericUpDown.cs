// Decompiled with JetBrains decompiler
// Type: TouchPark.CustomNumericUpDown
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.Windows.Forms;

namespace TouchPark
{
  public class CustomNumericUpDown : NumericUpDown
  {
    public int ValueInteger
    {
      get
      {
        return Convert.ToInt32(this.Value);
      }
      set
      {
        this.Value = Convert.ToDecimal(value);
      }
    }
  }
}
