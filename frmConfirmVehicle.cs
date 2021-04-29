// Decompiled with JetBrains decompiler
// Type: TouchPark.frmConfirmVehicle
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TouchPark.Properties;
using TouchPark.Services;

namespace TouchPark
{
  public class frmConfirmVehicle : Form
  {
    private string m_ApplicationType = Settings.Default.ApplicationType;
    private ParkingPermitInfo m_ParkingPermit = new ParkingPermitInfo();
    private string m_OverviewLocation;
    private string m_PlateLocation;
    private bool m_confirmVehicle;
    private IContainer components;
    private PictureBox imgOverview;
    private PictureBox imgVRM;
    private Label lblVehicleQuery;
    private Button cmdYes;
    private Button cmdNo;
    private Timer tmrToDisplay;
    private Panel panel1;

    [DllImport("Gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse);

    public CVehicleInfo VehicleInfo { get; set; }

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

    public frmConfirmVehicle(
      string overviewLocation,
      string VRMLocation,
      ParkingPermitInfo pParkingPermit)
    {
      this.InitializeComponent();
      this.Region = Region.FromHrgn(frmConfirmVehicle.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
      this.m_OverviewLocation = overviewLocation;
      this.m_PlateLocation = VRMLocation;
      this.m_ParkingPermit.copy(pParkingPermit);
    }

    private void SetImageToVRMPictureBox()
    {
      try
      {
        this.imgVRM.Image = this.m_PlateLocation.ToImage();//Image.FromFile(this.m_PlateLocation);
        if (this.VehicleInfo == null)
          return;
        Utilites.DrawRegistrationMark(this.imgVRM, this.VehicleInfo);
      }
      catch (Exception ex)
      {
      }
    }

    private void frmConfirmVehicle_Load(object sender, EventArgs e)
    {
      this.imgOverview.Image = this.m_OverviewLocation.ToImage();
      this.SetImageToVRMPictureBox();
    }

    private void cmdYes_Click(object sender, EventArgs e)
    {
      this.ConfirmVehicle = true;
      this.Close();
      Log.Write("Yes selected after image selection confirmation/vrm confirmation dialog close.");
      this.panel1.Visible = true;
      this.m_ParkingPermit.CaptureDate = DateTime.Now;
      switch (this.m_ApplicationType)
      {
        case "ServiceStationPark":
          this.Hide();
          Log.Write("ServiceStationPark is the Application type in App Config.");
          int num1 = (int) new frmLogin(this.m_ParkingPermit, "ServiceStationPark", this.m_OverviewLocation, this.m_PlateLocation).ShowDialog();
          break;
        case "PermitPark":
          this.Hide();
          string displayOption1 = "PermitPark";
          Log.Write("PermitPark is the Application type in App Config.");
          if (this.m_ParkingPermit.PermitType == "STAFF")
          {
            this.m_ParkingPermit.EndDate = DateTime.Parse("9999-12-30 23:59");
            int num2 = (int) new frmLogin(this.m_ParkingPermit, "PermitPark", this.m_OverviewLocation, this.m_PlateLocation).ShowDialog();
            break;
          }
          this.m_ParkingPermit.EndDate = this.m_ParkingPermit.StartDate.AddDays(1.0);
          CUpdateVehicleData cupdateVehicleData = new CUpdateVehicleData();
          int num3 = (int) new frmThankYou(this.m_ParkingPermit, displayOption1, CCacheData.WriteParkingPermitToXmlFile(this.m_ParkingPermit)).ShowDialog();
          break;
        case "PermitPark2":
          this.Hide();
          int num4 = (int) new frmLogin(this.m_ParkingPermit, "PermitPark2", this.m_OverviewLocation, this.m_PlateLocation).ShowDialog();
          break;
        default:
          this.Hide();
          string displayOption2 = "SelectPayment";
          WeeklyParkingTariff weeklyParkingTariff = new WeeklyParkingTariff(this.m_ParkingPermit.DurationOfStay);
          if (weeklyParkingTariff.MAX_PAYMENT_OPTIONS == 1 || weeklyParkingTariff.ValidOptionsCount == 1)
          {
            this.m_ParkingPermit.Amount = weeklyParkingTariff.GetFirstParkingCharge();
            this.m_ParkingPermit.EndDate = this.m_ParkingPermit.StartDate.Add(weeklyParkingTariff.GetDurationFromTariff(0));
            int num2 = (int) new PaymentForm(this.m_ParkingPermit, displayOption2).ShowDialog();
            break;
          }
          PaymentManager.ProcessPayment(this.m_ParkingPermit);
          break;
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
      this.imgVRM = new PictureBox();
      this.imgOverview = new PictureBox();
      this.panel1 = new Panel();
      ((ISupportInitialize) this.imgVRM).BeginInit();
      ((ISupportInitialize) this.imgOverview).BeginInit();
      this.SuspendLayout();
      this.lblVehicleQuery.BackColor = Color.Transparent;
      this.lblVehicleQuery.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblVehicleQuery.Location = new Point(139, 321);
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
      this.imgVRM.BackColor = Color.Transparent;
      this.imgVRM.BorderStyle = BorderStyle.Fixed3D;
      this.imgVRM.Location = new Point(172, 244);
      this.imgVRM.Name = "imgVRM";
      this.imgVRM.Size = new Size(314, 75);
      this.imgVRM.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM.TabIndex = 1;
      this.imgVRM.TabStop = false;
      this.imgOverview.BackColor = Color.Transparent;
      this.imgOverview.BorderStyle = BorderStyle.Fixed3D;
      this.imgOverview.Location = new Point(184, 12);
      this.imgOverview.Name = "imgOverview";
      this.imgOverview.Size = new Size(297, 219);
      this.imgOverview.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview.TabIndex = 0;
      this.imgOverview.TabStop = false;
      this.panel1.BackgroundImage = (Image) Resources.bg;
      this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel1.Location = new Point(-1, -2);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(654, 480);
      this.panel1.TabIndex = 5;
      this.panel1.Visible = false;
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
      this.Controls.Add((Control) this.imgVRM);
      this.Controls.Add((Control) this.imgOverview);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmConfirmVehicle);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.Load += new EventHandler(this.frmConfirmVehicle_Load);
      ((ISupportInitialize) this.imgVRM).EndInit();
      ((ISupportInitialize) this.imgOverview).EndInit();
      this.ResumeLayout(false);
    }
  }
}
