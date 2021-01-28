// Decompiled with JetBrains decompiler
// Type: TouchPark.Controls.GrowLabel
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TouchPark.Controls
{
  public class GrowLabel : Label
  {
    private bool mGrowing;

    public GrowLabel()
    {
      this.AutoSize = false;
    }

    private void resizeLabel()
    {
      if (this.mGrowing)
        return;
      try
      {
        this.mGrowing = true;
        Size proposedSize = new Size(this.Width, int.MaxValue);
        proposedSize = TextRenderer.MeasureText(this.Text, this.Font, proposedSize, TextFormatFlags.WordBreak);
        this.Height = proposedSize.Height;
      }
      finally
      {
        this.mGrowing = false;
      }
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      this.resizeLabel();
    }

    protected override void OnFontChanged(EventArgs e)
    {
      base.OnFontChanged(e);
      this.resizeLabel();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.resizeLabel();
    }
  }
}
