// Decompiled with JetBrains decompiler
// Type: TouchPark.frmStaffOrVistor
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
  public class frmStaffOrVistor : Form
  {
    private ParkingPermitInfo m_ParkingPermit = new ParkingPermitInfo();
    private IContainer components;
    private Timer DisplayTimer;
    private Button cmdStaff;
    private Button cmdVisitor;
    private Label lblStaffMessage;
    private Button cmdCancel;
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
      this.DisplayTimer = new Timer(this.components);
      this.lblStaffMessage = new Label();
      this.cmdVisitor = new Button();
      this.cmdStaff = new Button();
      this.cmdCancel = new Button();
      this.SuspendLayout();
      this.DisplayTimer.Enabled = true;
      this.DisplayTimer.Interval = 15000;
      this.DisplayTimer.Tick += new EventHandler(this.DisplayTimer_Tick);
      this.lblStaffMessage.BackColor = Color.Transparent;
      this.lblStaffMessage.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblStaffMessage.Location = new Point(117, 76);
      this.lblStaffMessage.Name = "lblStaffMessage";
      this.lblStaffMessage.Size = new Size(365, 30);
      this.lblStaffMessage.TabIndex = 16;
      this.lblStaffMessage.Text = "PLEASE SELECT A CATEGORY";
      this.lblStaffMessage.TextAlign = ContentAlignment.TopCenter;
      this.lblStaffMessage.Visible = false;
      this.cmdVisitor.BackColor = Color.Transparent;
      this.cmdVisitor.BackgroundImage = (Image) Resources.BlueKeyL;
      this.cmdVisitor.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdVisitor.FlatAppearance.BorderSize = 0;
      this.cmdVisitor.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdVisitor.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdVisitor.FlatStyle = FlatStyle.Flat;
      this.cmdVisitor.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdVisitor.ForeColor = Color.Gold;
      this.cmdVisitor.Location = new Point(346, 214);
      this.cmdVisitor.Name = "cmdVisitor";
      this.cmdVisitor.Size = new Size(142, 45);
      this.cmdVisitor.TabIndex = 15;
      this.cmdVisitor.Text = "VISITOR";
      this.cmdVisitor.UseVisualStyleBackColor = false;
      this.cmdVisitor.Visible = false;
      this.cmdVisitor.Click += new EventHandler(this.cmdVisitor_Click);
      this.cmdStaff.BackColor = Color.Transparent;
      this.cmdStaff.BackgroundImage = (Image) Resources.BlueKeyL;
      this.cmdStaff.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdStaff.FlatAppearance.BorderSize = 0;
      this.cmdStaff.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdStaff.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdStaff.FlatStyle = FlatStyle.Flat;
      this.cmdStaff.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdStaff.ForeColor = Color.Gold;
      this.cmdStaff.Location = new Point(346, 138);
      this.cmdStaff.Name = "cmdStaff";
      this.cmdStaff.Size = new Size(142, 67);
      this.cmdStaff.TabIndex = 14;
      this.cmdStaff.Text = "STORE STAFF";
      this.cmdStaff.UseVisualStyleBackColor = false;
      this.cmdStaff.Visible = false;
      this.cmdStaff.Click += new EventHandler(this.cmdStaff_Click);
      this.cmdCancel.BackColor = Color.Transparent;
      this.cmdCancel.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.cmdCancel.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdCancel.FlatAppearance.BorderSize = 0;
      this.cmdCancel.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdCancel.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdCancel.FlatStyle = FlatStyle.Flat;
      this.cmdCancel.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdCancel.ForeColor = Color.White;
      this.cmdCancel.Location = new Point(346, 277);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new Size(142, 45);
      this.cmdCancel.TabIndex = 17;
      this.cmdCancel.Text = "CANCEL";
      this.cmdCancel.UseVisualStyleBackColor = false;
      this.cmdCancel.Visible = false;
      this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click_1);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.BackgroundImage = (Image) Resources.bg;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(598, 410);
      this.ControlBox = false;
      this.Controls.Add((Control) this.cmdCancel);
      this.Controls.Add((Control) this.lblStaffMessage);
      this.Controls.Add((Control) this.cmdVisitor);
      this.Controls.Add((Control) this.cmdStaff);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmStaffOrVistor);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.Load += new EventHandler(this.frmPayment_Load);
      this.ResumeLayout(false);
    }

    [DllImport("Gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse);

    public frmStaffOrVistor(ParkingPermitInfo parkingPermit, string displayOption)
      : this(parkingPermit, displayOption, (string) null, (string) null)
    {
    }

    public frmStaffOrVistor()
      : this(new ParkingPermitInfo(), "PermitPark")
    {
    }

    public frmStaffOrVistor(
      ParkingPermitInfo parkingPermit,
      string displayOption,
      string overviewLocation,
      string plateLocation)
    {
      this.InitializeComponent();
      this.Region = Region.FromHrgn(frmStaffOrVistor.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
      this.m_ParkingPermit.VehicleRegMark = parkingPermit.VehicleRegMark;
      this.m_ParkingPermit.CaptureDate = parkingPermit.CaptureDate;
      this.m_ParkingPermit.StartDate = parkingPermit.StartDate;
      this.m_ParkingPermit.EndDate = parkingPermit.EndDate;
      this.m_ParkingPermit.Amount = parkingPermit.Amount;
      this.m_DisplayOption = displayOption;
      this.m_PlateLocation = plateLocation;
      this.m_OverviewLocation = overviewLocation;
    }

    private void frmPayment_Load(object sender, EventArgs e)
    {
      switch (this.m_DisplayOption)
      {
        case "PermitPark":
          this.GetFormStaffOrVisitorSettings();
          break;
      }
    }

    private void GetFormStaffOrVisitorSettings()
    {
      this.lblStaffMessage.Location = new Point(106, 51);
      this.cmdStaff.Location = new Point(215, 109);
      this.cmdVisitor.Location = new Point(215, 196);
      this.cmdCancel.Location = new Point(215, 271);
      this.cmdCancel.Visible = true;
      this.lblStaffMessage.Visible = true;
      this.cmdStaff.Visible = true;
      this.cmdVisitor.Visible = true;
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

    private void cmdStaff_Click(object sender, EventArgs e)
    {
      this.m_ParkingPermit.MachineName = Environment.MachineName;
      this.m_ParkingPermit.PermitType = "STAFF";
      this.m_ParkingPermit.PaymentType = "";
      this.m_ParkingPermit.AuthCode = "";
      this.m_ParkingPermit.Amount = new Decimal(0);
      this.m_ParkingPermit.EndDate = DateTime.MinValue;
      this.Hide();
      int num = (int) new frmSearchVehicle(this.m_ParkingPermit).ShowDialog();
    }

    private void cmdVisitor_Click(object sender, EventArgs e)
    {
      this.m_ParkingPermit.MachineName = Environment.MachineName;
      this.m_ParkingPermit.PermitType = "VISITOR";
      this.m_ParkingPermit.PaymentType = "";
      this.m_ParkingPermit.AuthCode = "";
      this.m_ParkingPermit.Amount = new Decimal(0);
      this.m_ParkingPermit.EndDate = this.m_ParkingPermit.StartDate.AddDays(Settings.Default.MaxNumberOfDaysToPermitFor);
      this.Hide();
      int num = (int) new frmSearchVehicle(this.m_ParkingPermit).ShowDialog();
    }

    private void DisplayTimer_Tick(object sender, EventArgs e)
    {
      this.Hide();
    }

    private void cmdCancel_Click_1(object sender, EventArgs e)
    {
      this.Hide();
    }
  }
}
