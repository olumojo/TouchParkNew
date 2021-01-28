// Decompiled with JetBrains decompiler
// Type: TouchPark.Controls.RegistrationPlate
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TouchPark.Controls
{
  public class RegistrationPlate : UserControl
  {
    private IContainer components;
    private Label registrationLabel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RegistrationPlate));
      this.registrationLabel = new Label();
      this.SuspendLayout();
      this.registrationLabel.BackColor = Color.Transparent;
      this.registrationLabel.Font = new Font("UKNumberPlate", 50f);
      this.registrationLabel.Location = new Point(-23, 20);
      this.registrationLabel.Name = "registrationLabel";
      this.registrationLabel.Size = new Size(493, 57);
      this.registrationLabel.TabIndex = 0;
      this.registrationLabel.Text = "888888888888";
      this.registrationLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Transparent;
      this.BackgroundImage = (Image) componentResourceManager.GetObject("$this.BackgroundImage");
      this.BackgroundImageLayout = ImageLayout.Center;
      this.Controls.Add((Control) this.registrationLabel);
      this.DoubleBuffered = true;
      this.Name = nameof (RegistrationPlate);
      this.Size = new Size(476, 103);
      this.ResumeLayout(false);
    }

    public string VehicleRegistrationMark
    {
      get
      {
        return this.registrationLabel.Text;
      }
      set
      {
        this.registrationLabel.Text = value;
        this.Refresh();
      }
    }

    public RegistrationPlate()
    {
      this.InitializeComponent();
    }
  }
}
