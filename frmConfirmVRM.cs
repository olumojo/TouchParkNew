// Decompiled with JetBrains decompiler
// Type: TouchPark.frmConfirmVRM
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
  public class frmConfirmVRM : Form
  {
    private string m_ApplicationType = Settings.Default.ApplicationType;
    private ParkingPermitInfo m_ParkingPermit = new ParkingPermitInfo();
    private string m_OverviewLocation;
    private string m_PlateLocation;
    private bool m_confirmVehicle;
    private IContainer components;
    private Label lblVehicleQuery;
    private Button cmdYes;
    private Button cmdNo;
    private Timer tmrToDisplay;
    private Panel panel1;
    private Label lblVRM;
    private Label lblVRMTObePermitted;

    [DllImport("Gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse);

    public bool ConfirmVehicle
    {
      get
      {
        return this.m_confirmVehicle;
      }
      set
      {
        this.m_confirmVehicle = value;
      }
    }

    public frmConfirmVRM(
      string overviewLocation,
      string VRMLocation,
      ParkingPermitInfo pParkingPermit)
    {
      this.InitializeComponent();
      this.Region = Region.FromHrgn(frmConfirmVRM.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
      this.m_OverviewLocation = overviewLocation;
      this.m_PlateLocation = VRMLocation;
      this.m_ParkingPermit.copy(pParkingPermit);
      this.lblVRM.Text = this.m_ParkingPermit.VehicleRegMark.ToUpper();
    }

    private void cmdYes_Click(object sender, EventArgs e)
    {
      this.ConfirmVehicle = true;
      this.Close();
      this.panel1.Visible = true;
      this.m_ParkingPermit.CaptureDate = DateTime.Now;
      this.m_ParkingPermit.StartDate = DateTime.Now;
      WeeklyParkingTariff weeklyParkingTariff = new WeeklyParkingTariff(this.m_ParkingPermit.DurationOfStay);
      if (weeklyParkingTariff.MAX_PAYMENT_OPTIONS == 1)
      {
        this.m_ParkingPermit.Amount = weeklyParkingTariff.GetFirstParkingCharge();
        this.m_ParkingPermit.EndDate = this.m_ParkingPermit.StartDate.AddHours(Settings.Default.MaximumPermitHours);
        int num = (int) new PaymentForm(this.m_ParkingPermit, "DisplayPayment").ShowDialog();
      }
      else
      {
        switch (this.m_ApplicationType)
        {
          case "ServiceStationPark":
            int num1 = (int) new frmLogin(this.m_ParkingPermit, "ServiceStationPark").ShowDialog();
            break;
          case "PermitPark":
            this.Hide();
            if (this.m_ParkingPermit.PermitType == "STAFF")
            {
              this.m_ParkingPermit.EndDate = DateTime.Parse("9999-12-30 23:59");
              int num2 = (int) new frmLogin(this.m_ParkingPermit, "PermitPark").ShowDialog();
              break;
            }
            this.m_ParkingPermit.EndDate = this.m_ParkingPermit.StartDate.AddDays(1.0);
            CUpdateVehicleData cupdateVehicleData = new CUpdateVehicleData();
            int num3 = (int) new frmThankYou(this.m_ParkingPermit, "PermitPark", CCacheData.WriteParkingPermitToXmlFile(this.m_ParkingPermit)).ShowDialog();
            break;
          case "PermitPark2":
            this.Hide();
            int num4 = (int) new frmLogin(this.m_ParkingPermit, "PermitPark2", this.m_OverviewLocation, this.m_PlateLocation).ShowDialog();
            break;
          default:
            ParkingChargesForm parkingChargesForm = new ParkingChargesForm(this.m_ParkingPermit);
            int num5 = (int) parkingChargesForm.ShowDialog();
            parkingChargesForm.Dispose();
            break;
        }
      }
    }

    private void cmdNo_Click(object sender, EventArgs e)
    {
      this.ConfirmVehicle = false;
      this.Close();
    }

    private void tmrToDisplay_Tick(object sender, EventArgs e)
    {
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.lblVehicleQuery = new Label();
      this.tmrToDisplay = new Timer(this.components);
      this.cmdNo = new Button();
      this.cmdYes = new Button();
      this.panel1 = new Panel();
      this.lblVRM = new Label();
      this.lblVRMTObePermitted = new Label();
      this.SuspendLayout();
      this.lblVehicleQuery.BackColor = Color.Transparent;
      this.lblVehicleQuery.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblVehicleQuery.Location = new Point(132, 315);
      this.lblVehicleQuery.Name = "lblVehicleQuery";
      this.lblVehicleQuery.Size = new Size(400, 40);
      this.lblVehicleQuery.TabIndex = 2;
      this.lblVehicleQuery.Text = "IS THIS YOUR VEHICLE?";
      this.lblVehicleQuery.TextAlign = ContentAlignment.MiddleCenter;
      this.tmrToDisplay.Enabled = true;
      this.tmrToDisplay.Interval = 15000;
      this.tmrToDisplay.Tick += new EventHandler(this.tmrToDisplay_Tick);
      this.cmdNo.BackColor = Color.Transparent;
      this.cmdNo.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.cmdNo.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdNo.FlatAppearance.BorderSize = 0;
      this.cmdNo.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdNo.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdNo.FlatStyle = FlatStyle.Flat;
      this.cmdNo.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdNo.ForeColor = Color.White;
      this.cmdNo.Location = new Point(367, 366);
      this.cmdNo.Name = "cmdNo";
      this.cmdNo.Size = new Size(142, 45);
      this.cmdNo.TabIndex = 4;
      this.cmdNo.Text = "NO";
      this.cmdNo.UseVisualStyleBackColor = false;
      this.cmdNo.Click += new EventHandler(this.cmdNo_Click);
      this.cmdYes.BackColor = Color.Transparent;
      this.cmdYes.BackgroundImage = (Image) Resources.GreenKey;
      this.cmdYes.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdYes.FlatAppearance.BorderSize = 0;
      this.cmdYes.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdYes.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdYes.FlatStyle = FlatStyle.Flat;
      this.cmdYes.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdYes.ForeColor = Color.DarkGreen;
      this.cmdYes.Location = new Point((int) sbyte.MaxValue, 367);
      this.cmdYes.Name = "cmdYes";
      this.cmdYes.Size = new Size(142, 45);
      this.cmdYes.TabIndex = 3;
      this.cmdYes.Text = "YES";
      this.cmdYes.UseVisualStyleBackColor = false;
      this.cmdYes.Click += new EventHandler(this.cmdYes_Click);
      this.panel1.BackgroundImage = (Image) Resources.bg;
      this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel1.Location = new Point(-1, -1);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(654, 480);
      this.panel1.TabIndex = 5;
      this.panel1.Visible = false;
      this.lblVRM.BackColor = Color.Transparent;
      this.lblVRM.Font = new Font("Arial Black", 36f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblVRM.Location = new Point(132, 179);
      this.lblVRM.Name = "lblVRM";
      this.lblVRM.Size = new Size(400, 63);
      this.lblVRM.TabIndex = 6;
      this.lblVRM.Text = "M777MWG";
      this.lblVRM.TextAlign = ContentAlignment.MiddleCenter;
      this.lblVRMTObePermitted.BackColor = Color.Transparent;
      this.lblVRMTObePermitted.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblVRMTObePermitted.Location = new Point(140, 31);
      this.lblVRMTObePermitted.Name = "lblVRMTObePermitted";
      this.lblVRMTObePermitted.Size = new Size(373, 70);
      this.lblVRMTObePermitted.TabIndex = 7;
      this.lblVRMTObePermitted.Text = "THE NUMBER PLATE FOR THE VEHICLE TO BE PERMITTED IS:";
      this.lblVRMTObePermitted.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Wheat;
      this.BackgroundImage = (Image) Resources.bg;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(652, 475);
      this.ControlBox = false;
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.cmdNo);
      this.Controls.Add((Control) this.cmdYes);
      this.Controls.Add((Control) this.lblVehicleQuery);
      this.Controls.Add((Control) this.lblVRM);
      this.Controls.Add((Control) this.lblVRMTObePermitted);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmConfirmVRM);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.ResumeLayout(false);
    }
  }
}
