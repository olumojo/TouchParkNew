// Decompiled with JetBrains decompiler
// Type: TouchPark.ParkingExpiredForm
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TouchPark.Properties;

namespace TouchPark
{
  public class ParkingExpiredForm : Form
  {
    private string m_PhoneNumber = Settings.Default.RangerTelephoneNumber;
    private string m_ApplicationType = Settings.Default.ApplicationType;
    private ParkingPermitInfo m_ParkingPermit = new ParkingPermitInfo();
    private IContainer components;
    private Label lblWarning;
    private Label lblTelephone;
    private Timer WarningTimer;
    private Button cmdClose;
    private string m_OverviewLocation;
    private string m_PlateLocation;
    private string m_DisplayOption;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.lblWarning = new Label();
      this.lblTelephone = new Label();
      this.WarningTimer = new Timer(this.components);
      this.cmdClose = new Button();
      this.SuspendLayout();
      this.lblWarning.AutoSize = true;
      this.lblWarning.BackColor = Color.Transparent;
      this.lblWarning.Font = new Font("Arial Black", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblWarning.Location = new Point(101, 165);
      this.lblWarning.Name = "lblWarning";
      this.lblWarning.Size = new Size(317, 23);
      this.lblWarning.TabIndex = 12;
      this.lblWarning.Text = "You Have Exceeded The Maximum";
      this.lblWarning.TextAlign = ContentAlignment.MiddleCenter;
      this.lblWarning.Visible = false;
      this.lblTelephone.AutoSize = true;
      this.lblTelephone.BackColor = Color.Transparent;
      this.lblTelephone.Font = new Font("Arial Black", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblTelephone.Location = new Point(155, 85);
      this.lblTelephone.Name = "lblTelephone";
      this.lblTelephone.Size = new Size(174, 23);
      this.lblTelephone.TabIndex = 13;
      this.lblTelephone.Text = "Call 0845 6024969";
      this.lblTelephone.Visible = false;
      this.WarningTimer.Interval = 60000;
      this.WarningTimer.Tick += new EventHandler(this.WarningTimer_Tick);
      this.cmdClose.BackColor = Color.Transparent;
      this.cmdClose.BackgroundImage = (Image) Resources.GreenKey;
      this.cmdClose.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdClose.FlatAppearance.BorderSize = 0;
      this.cmdClose.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdClose.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdClose.FlatStyle = FlatStyle.Flat;
      this.cmdClose.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdClose.ForeColor = Color.DarkGreen;
      this.cmdClose.Location = new Point(174, 256);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new Size(238, 83);
      this.cmdClose.TabIndex = 18;
      this.cmdClose.Text = "PRESS HERE FOR \r\nNEXT VEHICLE";
      this.cmdClose.UseVisualStyleBackColor = false;
      this.cmdClose.Visible = false;
      this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.BackgroundImage = (Image) Resources.bg;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(598, 410);
      this.ControlBox = false;
      this.Controls.Add((Control) this.cmdClose);
      this.Controls.Add((Control) this.lblTelephone);
      this.Controls.Add((Control) this.lblWarning);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmWarning";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.Load += new EventHandler(this.frmPayment_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    [DllImport("Gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse);

    public ParkingExpiredForm(ParkingPermitInfo parkingPermit, string displayOption)
      : this(parkingPermit, displayOption, (string) null, (string) null)
    {
    }

    public ParkingExpiredForm(
      ParkingPermitInfo parkingPermit,
      string displayOption,
      string overviewLocation,
      string plateLocation)
    {
      this.InitializeComponent();
      this.Region = Region.FromHrgn(ParkingExpiredForm.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
      this.m_ParkingPermit.copy(parkingPermit);
      this.m_DisplayOption = displayOption;
      this.m_PlateLocation = plateLocation;
      this.m_OverviewLocation = overviewLocation;
    }

    private void frmPayment_Load(object sender, EventArgs e)
    {
      this.GetFormWarningSettings();
    }

    private void GetFormWarningSettings()
    {
      this.lblWarning.Text = "You have exceeded the maximum time allowed.\n\rYou will receive notification of the charge by post.";
      this.lblTelephone.Text = "If you have any queries call " + this.m_PhoneNumber;
      int width1 = this.Width;
      int width2 = this.lblWarning.Width;
      this.lblWarning.Location = new Point((width1 - width2) / 2, 111);
      int width3 = this.lblTelephone.Width;
      this.lblTelephone.Location = new Point((width1 - width3) / 2, 231);
      this.lblWarning.Visible = true;
      this.lblTelephone.Visible = true;
      this.cmdClose.Visible = true;
      this.WarningTimer.Enabled = true;
      this.WarningTimer.Start();
    }

    private void WarningTimer_Tick(object sender, EventArgs e)
    {
      this.Close();
    }

    private void cmdClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
