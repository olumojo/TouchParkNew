// Decompiled with JetBrains decompiler
// Type: TouchPark.frmPaymentType
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using TouchPark.Properties;

namespace TouchPark
{
  public class frmPaymentType : Form
  {
    private bool _numberOfDaysChoiceEnabled = Settings.Default.EnableNumberOfDaysChoice;
    private ParkingPermitInfo m_ParkingPermit = new ParkingPermitInfo();
    private IContainer components;
    private Button cmdCancel;
    private Label lblStaffMessage;
    private Button categoryCARButton;
    private Button categoryHGVButton;
    private PictureBox imgVRM;
    private PictureBox imgOverview;
    private Button categoryHGVFButton;
    private Button categoryHGVTrailerButton;
    private FlowLayoutPanel categoryButtonFlowLayoutPanel;
    private Button categoryStaffButton;
    private Button categoryVisitorButton;
    private Button categoryGuestButton;
    private Button categoryAccountHolderButton;
    private Button categoryCoachButton;
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
      this.lblStaffMessage = new Label();
      this.categoryHGVFButton = new Button();
      this.imgVRM = new PictureBox();
      this.imgOverview = new PictureBox();
      this.categoryCARButton = new Button();
      this.categoryHGVButton = new Button();
      this.cmdCancel = new Button();
      this.categoryHGVTrailerButton = new Button();
      this.categoryButtonFlowLayoutPanel = new FlowLayoutPanel();
      this.categoryStaffButton = new Button();
      this.categoryVisitorButton = new Button();
      this.categoryGuestButton = new Button();
      this.categoryAccountHolderButton = new Button();
      this.categoryCoachButton = new Button();
      ((ISupportInitialize) this.imgVRM).BeginInit();
      ((ISupportInitialize) this.imgOverview).BeginInit();
      this.categoryButtonFlowLayoutPanel.SuspendLayout();
      this.SuspendLayout();
      this.lblStaffMessage.BackColor = Color.Transparent;
      this.lblStaffMessage.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblStaffMessage.Location = new Point(102, 5);
      this.lblStaffMessage.Name = "lblStaffMessage";
      this.lblStaffMessage.Size = new Size(514, 30);
      this.lblStaffMessage.TabIndex = 16;
      this.lblStaffMessage.Text = "PLEASE SELECT A CATEGORY";
      this.lblStaffMessage.TextAlign = ContentAlignment.TopCenter;
      this.lblStaffMessage.Visible = false;
      this.categoryHGVFButton.BackColor = Color.Transparent;
      this.categoryHGVFButton.BackgroundImage = (Image) Resources.BlueKeyXL;
      this.categoryHGVFButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.categoryHGVFButton.FlatAppearance.BorderSize = 0;
      this.categoryHGVFButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.categoryHGVFButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.categoryHGVFButton.FlatStyle = FlatStyle.Flat;
      this.categoryHGVFButton.Font = new Font("Arial", 18f, FontStyle.Bold);
      this.categoryHGVFButton.ForeColor = Color.Gold;
      this.categoryHGVFButton.Location = new Point(3, 49);
      this.categoryHGVFButton.Name = "categoryHGVFButton";
      this.categoryHGVFButton.Size = new Size(202, 40);
      this.categoryHGVFButton.TabIndex = 58;
      this.categoryHGVFButton.Text = "HGV With Food";
      this.categoryHGVFButton.UseVisualStyleBackColor = false;
      this.categoryHGVFButton.Visible = false;
      this.categoryHGVFButton.Click += new EventHandler(this.categoryHGVFButton_Click);
      this.imgVRM.BackColor = Color.Transparent;
      this.imgVRM.BorderStyle = BorderStyle.Fixed3D;
      this.imgVRM.Location = new Point(77, 245);
      this.imgVRM.Name = "imgVRM";
      this.imgVRM.Size = new Size(250, 50);
      this.imgVRM.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM.TabIndex = 56;
      this.imgVRM.TabStop = false;
      this.imgOverview.BackColor = Color.Transparent;
      this.imgOverview.BorderStyle = BorderStyle.Fixed3D;
      this.imgOverview.Location = new Point(107, 72);
      this.imgOverview.Name = "imgOverview";
      this.imgOverview.Size = new Size(200, 150);
      this.imgOverview.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview.TabIndex = 55;
      this.imgOverview.TabStop = false;
      this.categoryCARButton.BackColor = Color.Transparent;
      this.categoryCARButton.BackgroundImage = (Image) Resources.BlueKeyXL;
      this.categoryCARButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.categoryCARButton.FlatAppearance.BorderSize = 0;
      this.categoryCARButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.categoryCARButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.categoryCARButton.FlatStyle = FlatStyle.Flat;
      this.categoryCARButton.Font = new Font("Arial", 18f, FontStyle.Bold);
      this.categoryCARButton.ForeColor = Color.Gold;
      this.categoryCARButton.Location = new Point(3, 141);
      this.categoryCARButton.Name = "categoryCARButton";
      this.categoryCARButton.Size = new Size(202, 40);
      this.categoryCARButton.TabIndex = 20;
      this.categoryCARButton.Text = "Car";
      this.categoryCARButton.UseVisualStyleBackColor = false;
      this.categoryCARButton.Visible = false;
      this.categoryCARButton.Click += new EventHandler(this.categoryCarButton_Click);
      this.categoryHGVButton.BackColor = Color.Transparent;
      this.categoryHGVButton.BackgroundImage = (Image) Resources.BlueKeyXL;
      this.categoryHGVButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.categoryHGVButton.FlatAppearance.BorderSize = 0;
      this.categoryHGVButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.categoryHGVButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.categoryHGVButton.FlatStyle = FlatStyle.Flat;
      this.categoryHGVButton.Font = new Font("Arial", 15f, FontStyle.Bold);
      this.categoryHGVButton.ForeColor = Color.Gold;
      this.categoryHGVButton.Location = new Point(3, 3);
      this.categoryHGVButton.Name = "categoryHGVButton";
      this.categoryHGVButton.Size = new Size(202, 40);
      this.categoryHGVButton.TabIndex = 19;
      this.categoryHGVButton.Text = "HGV";
      this.categoryHGVButton.UseVisualStyleBackColor = false;
      this.categoryHGVButton.Visible = false;
      this.categoryHGVButton.Click += new EventHandler(this.categoryHGVButton_Click);
      this.cmdCancel.BackColor = Color.Transparent;
      this.cmdCancel.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.cmdCancel.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdCancel.FlatAppearance.BorderSize = 0;
      this.cmdCancel.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdCancel.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdCancel.FlatStyle = FlatStyle.Flat;
      this.cmdCancel.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdCancel.ForeColor = Color.White;
      this.cmdCancel.Location = new Point(283, 446);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new Size(142, 45);
      this.cmdCancel.TabIndex = 9;
      this.cmdCancel.Text = "CANCEL";
      this.cmdCancel.UseVisualStyleBackColor = false;
      this.cmdCancel.Visible = false;
      this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
      this.categoryHGVTrailerButton.BackColor = Color.Transparent;
      this.categoryHGVTrailerButton.BackgroundImage = (Image) Resources.BlueKeyXL;
      this.categoryHGVTrailerButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.categoryHGVTrailerButton.FlatAppearance.BorderSize = 0;
      this.categoryHGVTrailerButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.categoryHGVTrailerButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.categoryHGVTrailerButton.FlatStyle = FlatStyle.Flat;
      this.categoryHGVTrailerButton.Font = new Font("Arial", 18f, FontStyle.Bold);
      this.categoryHGVTrailerButton.ForeColor = Color.Gold;
      this.categoryHGVTrailerButton.Location = new Point(3, 95);
      this.categoryHGVTrailerButton.Name = "categoryHGVTrailerButton";
      this.categoryHGVTrailerButton.Size = new Size(202, 40);
      this.categoryHGVTrailerButton.TabIndex = 59;
      this.categoryHGVTrailerButton.Text = "Trailer";
      this.categoryHGVTrailerButton.UseVisualStyleBackColor = false;
      this.categoryHGVTrailerButton.Visible = false;
      this.categoryHGVTrailerButton.Click += new EventHandler(this.categoryCarAndTrailerButton_Click);
      this.categoryButtonFlowLayoutPanel.BackColor = Color.Transparent;
      this.categoryButtonFlowLayoutPanel.Controls.Add((Control) this.categoryHGVButton);
      this.categoryButtonFlowLayoutPanel.Controls.Add((Control) this.categoryHGVFButton);
      this.categoryButtonFlowLayoutPanel.Controls.Add((Control) this.categoryHGVTrailerButton);
      this.categoryButtonFlowLayoutPanel.Controls.Add((Control) this.categoryCARButton);
      this.categoryButtonFlowLayoutPanel.Controls.Add((Control) this.categoryStaffButton);
      this.categoryButtonFlowLayoutPanel.Controls.Add((Control) this.categoryVisitorButton);
      this.categoryButtonFlowLayoutPanel.Controls.Add((Control) this.categoryGuestButton);
      this.categoryButtonFlowLayoutPanel.Controls.Add((Control) this.categoryAccountHolderButton);
      this.categoryButtonFlowLayoutPanel.Controls.Add((Control) this.categoryCoachButton);
      this.categoryButtonFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
      this.categoryButtonFlowLayoutPanel.Location = new Point(378, 35);
      this.categoryButtonFlowLayoutPanel.Name = "categoryButtonFlowLayoutPanel";
      this.categoryButtonFlowLayoutPanel.Size = new Size(214, 414);
      this.categoryButtonFlowLayoutPanel.TabIndex = 60;
      this.categoryStaffButton.BackColor = Color.Transparent;
      this.categoryStaffButton.BackgroundImage = (Image) Resources.BlueKeyXL;
      this.categoryStaffButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.categoryStaffButton.FlatAppearance.BorderSize = 0;
      this.categoryStaffButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.categoryStaffButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.categoryStaffButton.FlatStyle = FlatStyle.Flat;
      this.categoryStaffButton.Font = new Font("Arial", 18f, FontStyle.Bold);
      this.categoryStaffButton.ForeColor = Color.Gold;
      this.categoryStaffButton.Location = new Point(3, 187);
      this.categoryStaffButton.Name = "categoryStaffButton";
      this.categoryStaffButton.Size = new Size(202, 40);
      this.categoryStaffButton.TabIndex = 60;
      this.categoryStaffButton.Text = "Staff";
      this.categoryStaffButton.UseVisualStyleBackColor = false;
      this.categoryStaffButton.Visible = false;
      this.categoryStaffButton.Click += new EventHandler(this.categoryStaffButton_Click);
      this.categoryVisitorButton.BackColor = Color.Transparent;
      this.categoryVisitorButton.BackgroundImage = (Image) Resources.BlueKeyXL;
      this.categoryVisitorButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.categoryVisitorButton.FlatAppearance.BorderSize = 0;
      this.categoryVisitorButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.categoryVisitorButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.categoryVisitorButton.FlatStyle = FlatStyle.Flat;
      this.categoryVisitorButton.Font = new Font("Arial", 18f, FontStyle.Bold);
      this.categoryVisitorButton.ForeColor = Color.Gold;
      this.categoryVisitorButton.Location = new Point(3, 233);
      this.categoryVisitorButton.Name = "categoryVisitorButton";
      this.categoryVisitorButton.Size = new Size(202, 40);
      this.categoryVisitorButton.TabIndex = 61;
      this.categoryVisitorButton.Text = "Visitor";
      this.categoryVisitorButton.UseVisualStyleBackColor = false;
      this.categoryVisitorButton.Visible = false;
      this.categoryVisitorButton.Click += new EventHandler(this.categoryVisitorButton_Click);
      this.categoryGuestButton.BackColor = Color.Transparent;
      this.categoryGuestButton.BackgroundImage = (Image) Resources.BlueKeyXL;
      this.categoryGuestButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.categoryGuestButton.FlatAppearance.BorderSize = 0;
      this.categoryGuestButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.categoryGuestButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.categoryGuestButton.FlatStyle = FlatStyle.Flat;
      this.categoryGuestButton.Font = new Font("Arial", 18f, FontStyle.Bold);
      this.categoryGuestButton.ForeColor = Color.Gold;
      this.categoryGuestButton.Location = new Point(3, 279);
      this.categoryGuestButton.Name = "categoryGuestButton";
      this.categoryGuestButton.Size = new Size(202, 40);
      this.categoryGuestButton.TabIndex = 62;
      this.categoryGuestButton.Text = "Guest";
      this.categoryGuestButton.UseVisualStyleBackColor = false;
      this.categoryGuestButton.Visible = false;
      this.categoryGuestButton.Click += new EventHandler(this.categoryGuestButton_Click);
      this.categoryAccountHolderButton.BackColor = Color.Transparent;
      this.categoryAccountHolderButton.BackgroundImage = (Image) Resources.BlueKeyXL;
      this.categoryAccountHolderButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.categoryAccountHolderButton.FlatAppearance.BorderSize = 0;
      this.categoryAccountHolderButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.categoryAccountHolderButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.categoryAccountHolderButton.FlatStyle = FlatStyle.Flat;
      this.categoryAccountHolderButton.Font = new Font("Arial", 18f, FontStyle.Bold);
      this.categoryAccountHolderButton.ForeColor = Color.Gold;
      this.categoryAccountHolderButton.Location = new Point(3, 325);
      this.categoryAccountHolderButton.Name = "categoryAccountHolderButton";
      this.categoryAccountHolderButton.Size = new Size(202, 40);
      this.categoryAccountHolderButton.TabIndex = 63;
      this.categoryAccountHolderButton.Text = "Account Holder";
      this.categoryAccountHolderButton.UseVisualStyleBackColor = false;
      this.categoryAccountHolderButton.Visible = false;
      this.categoryAccountHolderButton.Click += new EventHandler(this.categoryAccountHolderButton_Click);
      this.categoryCoachButton.BackColor = Color.Transparent;
      this.categoryCoachButton.BackgroundImage = (Image) Resources.BlueKeyXL;
      this.categoryCoachButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.categoryCoachButton.FlatAppearance.BorderSize = 0;
      this.categoryCoachButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.categoryCoachButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.categoryCoachButton.FlatStyle = FlatStyle.Flat;
      this.categoryCoachButton.Font = new Font("Arial", 18f, FontStyle.Bold);
      this.categoryCoachButton.ForeColor = Color.Gold;
      this.categoryCoachButton.Location = new Point(3, 371);
      this.categoryCoachButton.Name = "categoryCoachButton";
      this.categoryCoachButton.Size = new Size(202, 40);
      this.categoryCoachButton.TabIndex = 64;
      this.categoryCoachButton.Text = "Coach";
      this.categoryCoachButton.UseVisualStyleBackColor = false;
      this.categoryCoachButton.Visible = false;
      this.categoryCoachButton.Click += new EventHandler(this.categoryCoachButton_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.BackgroundImage = (Image) Resources.bg;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(712, 503);
      this.ControlBox = false;
      this.Controls.Add((Control) this.cmdCancel);
      this.Controls.Add((Control) this.categoryButtonFlowLayoutPanel);
      this.Controls.Add((Control) this.imgVRM);
      this.Controls.Add((Control) this.imgOverview);
      this.Controls.Add((Control) this.lblStaffMessage);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmPaymentType);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.Load += new EventHandler(this.frmPayment_Load);
      ((ISupportInitialize) this.imgVRM).EndInit();
      ((ISupportInitialize) this.imgOverview).EndInit();
      this.categoryButtonFlowLayoutPanel.ResumeLayout(false);
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

    public frmPaymentType(ParkingPermitInfo parkingPermit, string displayOption)
      : this(parkingPermit, displayOption, (string) null, (string) null)
    {
    }

    public frmPaymentType(
      ParkingPermitInfo parkingPermit,
      string displayOption,
      string overviewLocation,
      string plateLocation)
    {
      this.InitializeComponent();
      this.Region = Region.FromHrgn(frmPaymentType.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
      this.m_ParkingPermit.copy(parkingPermit);
      this.m_DisplayOption = displayOption;
      this.m_OverviewLocation = overviewLocation;
      this.m_PlateLocation = plateLocation;
    }

    private void frmPayment_Load(object sender, EventArgs e)
    {
      this.EnableDisableTariffCategories();
      this.ArrangeControls();
    }

    private void ArrangeControls()
    {
      if (this.m_OverviewLocation == null || this.m_PlateLocation == null)
      {
        this.imgOverview.Visible = false;
        this.imgVRM.Visible = false;
        this.categoryButtonFlowLayoutPanel.Left = (this.Width - this.categoryButtonFlowLayoutPanel.Width) / 2;
        this.lblStaffMessage.Visible = true;
      }
      else
      {
        this.imgOverview.ImageLocation = this.m_OverviewLocation;
        this.imgVRM.ImageLocation = this.m_PlateLocation;
        this.imgOverview.Location = new Point(107, 110);
        this.imgVRM.Location = new Point(77, 270);
        this.categoryButtonFlowLayoutPanel.Left = 378;
        this.imgOverview.Visible = true;
        this.imgVRM.Visible = true;
      }
      this.lblStaffMessage.Left = (this.Width - this.lblStaffMessage.Width) / 2;
      this.cmdCancel.Left = (this.Width - this.cmdCancel.Width) / 2;
      this.cmdCancel.Visible = true;
      this.lblStaffMessage.Visible = true;
    }

    private void EnableDisableTariffCategories()
    {
      this.categoryHGVButton.Visible = Settings.Default.EnableTariffCategoryHGV;
      this.categoryHGVFButton.Visible = Settings.Default.EnableTariffCategoryHGVFood;
      this.categoryCARButton.Visible = Settings.Default.EnableTariffCategoryCar;
      this.categoryHGVTrailerButton.Visible = Settings.Default.EnableTariffCategoryHGVTrailer;
      this.categoryStaffButton.Visible = Settings.Default.EnableTariffCategoryStaff;
      this.categoryVisitorButton.Visible = Settings.Default.EnableTariffCategoryVisitor;
      this.categoryGuestButton.Visible = Settings.Default.EnableTariffCategoryGuest;
      this.categoryAccountHolderButton.Visible = Settings.Default.EnableTariffCategoryAccountHolder;
      this.categoryCoachButton.Visible = Settings.Default.EnableTariffCategoryCoach;
    }

    private void GetFormServiceStationPark()
    {
      this.cmdCancel.Visible = false;
      if (this.m_OverviewLocation == null || this.m_PlateLocation == null)
      {
        this.lblStaffMessage.Location = new Point(43, 51);
        this.categoryHGVButton.Location = new Point(188, 96);
        this.categoryHGVFButton.Location = new Point(188, 146);
        this.categoryCARButton.Location = new Point(188, 196);
        this.cmdCancel.Location = new Point(212, 271);
        this.cmdCancel.Visible = true;
        this.lblStaffMessage.Visible = true;
        this.categoryHGVButton.Visible = true;
        this.categoryHGVFButton.Visible = true;
        this.categoryCARButton.Visible = true;
      }
      else
      {
        this.lblStaffMessage.Location = new Point(43, 51);
        this.imgOverview.Location = new Point(68, 110);
        this.imgVRM.Location = new Point(46, 270);
        this.categoryHGVButton.Location = new Point(333, 96);
        this.categoryHGVFButton.Location = new Point(333, 146);
        this.categoryCARButton.Location = new Point(333, 196);
        this.cmdCancel.Location = new Point(353, 271);
        this.lblStaffMessage.Visible = true;
        this.imgOverview.Visible = true;
        this.imgVRM.Visible = true;
        this.categoryHGVButton.Visible = true;
        this.categoryHGVFButton.Visible = true;
        this.categoryCARButton.Visible = true;
        this.cmdCancel.Visible = true;
      }
    }

    private void GetFormPaidInFullSettings()
    {
      bool xml = Utilites.WriteParkingPermitToXML(this.m_ParkingPermit);
      this.cmdCancel.Visible = false;
      this.lblStaffMessage.Visible = false;
      this.categoryCARButton.Visible = false;
      this.categoryHGVButton.Visible = false;
      this.categoryHGVFButton.Visible = false;
      this.imgVRM.Visible = false;
      this.imgOverview.Visible = false;
      this.Hide();
      new frmThankYou(this.m_ParkingPermit, this.m_DisplayOption, xml).Show();
      this.Close();
    }

    private void SetToBlackButton(Button button)
    {
      button.BackgroundImage = (Image) Resources.BlackKey;
      this.Refresh();
      Application.DoEvents();
      Thread.Sleep(100);
    }

    private void SetTariffCategoryAndShowNumberOfDaysForm(string tariffCategory)
    {
      this.m_ParkingPermit.MachineName = Environment.MachineName;
      this.m_ParkingPermit.PermitType = TariffCategorySettings.DecidePermitType(tariffCategory);
      this.m_ParkingPermit.PaymentType = tariffCategory;
      this.m_ParkingPermit.AuthCode = "";
      this.m_ParkingPermit.Amount = new Decimal(0);
      if (this._numberOfDaysChoiceEnabled)
      {
        new frmNumberOfDays(this.m_ParkingPermit, this.m_DisplayOption).Show();
        this.Hide();
      }
      else
      {
        this.m_ParkingPermit.EndDate = this.m_ParkingPermit.StartDate.AddDays(Settings.Default.MaxNumberOfDaysToPermitFor);
        this.GetFormPaidInFullSettings();
      }
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      this.SetToBlackButton(this.cmdCancel);
      this.Close();
    }

    private void cmdClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void categoryHGVButton_Click(object sender, EventArgs e)
    {
      this.SetToBlackButton(this.categoryHGVButton);
      this.SetTariffCategoryAndShowNumberOfDaysForm("HGV");
    }

    private void categoryCarButton_Click(object sender, EventArgs e)
    {
      this.SetToBlackButton(this.categoryCARButton);
      this.SetTariffCategoryAndShowNumberOfDaysForm("CAR");
    }

    private void categoryHGVFButton_Click(object sender, EventArgs e)
    {
      this.SetToBlackButton(this.categoryHGVFButton);
      this.SetTariffCategoryAndShowNumberOfDaysForm("HGVF");
    }

    private void categoryCarAndTrailerButton_Click(object sender, EventArgs e)
    {
      this.SetToBlackButton(this.categoryHGVTrailerButton);
      this.SetTariffCategoryAndShowNumberOfDaysForm("HGVTRAILER");
    }

    private void categoryStaffButton_Click(object sender, EventArgs e)
    {
      this.SetToBlackButton(this.categoryStaffButton);
      this.SetTariffCategoryAndShowNumberOfDaysForm("STAFF");
    }

    private void categoryVisitorButton_Click(object sender, EventArgs e)
    {
      this.SetToBlackButton(this.categoryVisitorButton);
      this.SetTariffCategoryAndShowNumberOfDaysForm("VISITOR");
    }

    private void categoryGuestButton_Click(object sender, EventArgs e)
    {
      this.SetToBlackButton(this.categoryGuestButton);
      this.SetTariffCategoryAndShowNumberOfDaysForm("GUEST");
    }

    private void categoryAccountHolderButton_Click(object sender, EventArgs e)
    {
      this.SetToBlackButton(this.categoryAccountHolderButton);
      this.SetTariffCategoryAndShowNumberOfDaysForm("ACCOUNT_HOLDER");
    }

    private void categoryCoachButton_Click(object sender, EventArgs e)
    {
      this.SetToBlackButton(this.categoryCoachButton);
      this.SetTariffCategoryAndShowNumberOfDaysForm("COACH");
    }
  }
}
