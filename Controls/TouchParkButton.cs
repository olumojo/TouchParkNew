// Decompiled with JetBrains decompiler
// Type: TouchPark.Controls.TouchParkButton
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System.Drawing;
using System.Windows.Forms;
using TouchPark.Properties;

namespace TouchPark.Controls
{
  internal class TouchParkButton : Button
  {
    internal TouchParkButton()
    {
      this.BackColor = Color.Transparent;
      this.BackgroundImage = (Image) Resources.BlueKey;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.FlatAppearance.BorderSize = 0;
      this.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.FlatStyle = FlatStyle.Flat;
      this.Font = new Font("Arial", 13f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.ForeColor = Color.White;
      this.Location = new Point(527, 62);
      this.Name = "touchParkButton1";
      this.Size = new Size(113, 89);
      this.TabIndex = 28;
      this.Text = "TP Button";
      this.UseVisualStyleBackColor = false;
    }
  }
}
