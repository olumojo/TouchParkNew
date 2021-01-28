// Decompiled with JetBrains decompiler
// Type: TouchPark.frmLogin
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TouchPark.Properties;

namespace TouchPark
{
  public class frmLogin : Form
  {
    private bool _numberOfDaysChoiceEnabled = Settings.Default.EnableNumberOfDaysChoice;
    private string m_Display = ConfigurationManager.AppSettings["PaymentType"];
    private ParkingPermitInfo m_ParkingPermit = new ParkingPermitInfo();
    private string m_OverviewLocation;
    private string m_PlateLocation;
    private string m_DisplayOption;
    private IContainer components;
    private Label lblTelephone;
    private Panel pnlLogin;
    private Button cmdKey0;
    private Button cmdKey9;
    private Button cmdKey8;
    private Button cmdKey6;
    private Button cmdKey7;
    private Button cmdKey5;
    private Button cmdKey4;
    private Button cmdKey3;
    private Button cmdKey2;
    private Button cmdKey1;
    private TextBox txtPassword;
    private Button cmdLogin;
    private Button cmdDel;
    private Label lblStaffCode;
    private Button cmdCancelLogin;
    private Label lblWrongPasscode;

    [DllImport("Gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse);

    public frmLogin(ParkingPermitInfo parkingPermit, string displayOption)
      : this(parkingPermit, displayOption, (string) null, (string) null)
    {
    }

    public frmLogin(
      ParkingPermitInfo parkingPermit,
      string displayOption,
      string overviewLocation,
      string plateLocation)
    {
      this.InitializeComponent();
      this.Region = Region.FromHrgn(frmLogin.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
      this.m_ParkingPermit.copy(parkingPermit);
      this.m_DisplayOption = displayOption;
      this.m_PlateLocation = plateLocation;
      this.m_OverviewLocation = overviewLocation;
    }

    private void frmPayment_Load(object sender, EventArgs e)
    {
      if (Settings.Default.DisableUserCode)
      {
        this.m_ParkingPermit.UserName = "UNSPECIFIED";
        this.m_ParkingPermit.UserID = -1;
        this.m_ParkingPermit.PassCode = "1";
        this.Hide();
        this.LoginPassedOpenNextForm();
      }
      else
        this.GetFormLoginSetting();
    }

    private void GetFormLoginSetting()
    {
      this.lblTelephone.Visible = false;
      this.lblStaffCode.Location = new Point(this.Width / 2 - this.lblStaffCode.Size.Width / 2 - 5, 40);
      this.pnlLogin.Location = new Point(this.Width / 2 - this.pnlLogin.Size.Width / 2 - 5, 71);
      this.lblStaffCode.Visible = true;
      this.pnlLogin.Visible = true;
    }

    private void WriteTariffCategoryPermit()
    {
      this.m_ParkingPermit.MachineName = Environment.MachineName;
      this.m_ParkingPermit.PermitType = "VISITOR";
      string str = "CAR";
      if (TariffCategorySettings.EnabledTariffCategories().Count == 1)
        str = TariffCategorySettings.EnabledTariffCategories()[0];
      this.m_ParkingPermit.PaymentType = str;
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
        this.WritePermitToXMLAndOpenThankyouForm();
      }
    }

    private void ShowEnteredKeys(object sender, EventArgs e)
    {
      Button button = (Button) sender;
      this.lblWrongPasscode.Visible = false;
      if (button.Text.ToUpper() == "DEL")
      {
        if (this.txtPassword.Text.Length <= 0)
          return;
        this.txtPassword.Text = this.txtPassword.Text.Substring(0, this.txtPassword.Text.Length - 1);
      }
      else
      {
        if (this.txtPassword.Text.Length > 20)
          return;
        this.txtPassword.Text += button.Text;
      }
    }

    private void LoginCheck()
    {
      DataRow dataRow = CCacheData.isValidPassCode(this.txtPassword.Text);
      if (dataRow != null)
      {
        this.m_ParkingPermit.UserName = dataRow["username"].ToString();
        this.m_ParkingPermit.UserID = int.Parse(dataRow["userInformationID"].ToString());
        this.m_ParkingPermit.PassCode = this.txtPassword.Text;
        this.LoginPassedOpenNextForm();
        this.Close();
      }
      else
        this.lblWrongPasscode.Visible = true;
    }

    private void LoginPassedOpenNextForm()
    {
      switch (this.m_DisplayOption)
      {
        case "ServiceStationPark":
          if (TariffCategorySettings.EnabledTariffCategories().Count > 1)
          {
            this.ShowTariffCategoryOptionForm();
            break;
          }
          this.WriteTariffCategoryPermit();
          break;
        case "PermitPark":
          if (this._numberOfDaysChoiceEnabled)
          {
            this.ShowNumberOfDaysForm();
            break;
          }
          this.WritePermitToXMLAndOpenThankyouForm();
          break;
        case "PermitPark2":
          if (this.m_ParkingPermit.PermitType == "VISITOR")
          {
            this.ShowNumberOfDaysForm();
            break;
          }
          this.m_ParkingPermit.EndDate = DateTime.Parse("9999-12-31 23:59:59");
          this.WritePermitToXMLAndOpenThankyouForm();
          break;
        default:
          this.WritePermitToXMLAndOpenThankyouForm();
          break;
      }
      this.lblWrongPasscode.Visible = false;
    }

    private void ShowNumberOfDaysForm()
    {
      frmNumberOfDays frmNumberOfDays = new frmNumberOfDays(this.m_ParkingPermit, this.m_DisplayOption);
      this.Hide();
      int num = (int) frmNumberOfDays.ShowDialog();
      this.Close();
    }

    private void ShowTariffCategoryOptionForm()
    {
      frmPaymentType frmPaymentType = new frmPaymentType(this.m_ParkingPermit, this.m_DisplayOption, this.m_OverviewLocation, this.m_PlateLocation);
      this.Hide();
      int num = (int) frmPaymentType.ShowDialog();
      this.Close();
    }

    private void WritePermitToXMLAndOpenThankyouForm()
    {
      bool xml = Utilites.WriteParkingPermitToXML(this.m_ParkingPermit);
      this.lblTelephone.Visible = false;
      this.pnlLogin.Visible = false;
      this.lblStaffCode.Visible = false;
      this.Hide();
      int num = (int) new frmThankYou(this.m_ParkingPermit, this.m_DisplayOption, xml).ShowDialog();
      this.Close();
    }

    private void cmdLogin_Click(object sender, EventArgs e)
    {
      this.LoginCheck();
    }

    private void cmdCancelLogin_Click(object sender, EventArgs e)
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
      this.lblTelephone = new Label();
      this.lblStaffCode = new Label();
      this.pnlLogin = new Panel();
      this.lblWrongPasscode = new Label();
      this.cmdCancelLogin = new Button();
      this.txtPassword = new TextBox();
      this.cmdLogin = new Button();
      this.cmdDel = new Button();
      this.cmdKey0 = new Button();
      this.cmdKey9 = new Button();
      this.cmdKey8 = new Button();
      this.cmdKey6 = new Button();
      this.cmdKey7 = new Button();
      this.cmdKey5 = new Button();
      this.cmdKey4 = new Button();
      this.cmdKey3 = new Button();
      this.cmdKey2 = new Button();
      this.cmdKey1 = new Button();
      this.pnlLogin.SuspendLayout();
      this.SuspendLayout();
      this.lblTelephone.AutoSize = true;
      this.lblTelephone.BackColor = Color.Transparent;
      this.lblTelephone.Font = new Font("Arial Black", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblTelephone.Location = new Point(176, 197);
      this.lblTelephone.Name = "lblTelephone";
      this.lblTelephone.Size = new Size(174, 23);
      this.lblTelephone.TabIndex = 13;
      this.lblTelephone.Text = "Call 0845 6024969";
      this.lblTelephone.Visible = false;
      this.lblStaffCode.BackColor = Color.Transparent;
      this.lblStaffCode.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblStaffCode.Location = new Point(40, 51);
      this.lblStaffCode.Name = "lblStaffCode";
      this.lblStaffCode.Size = new Size(520, 30);
      this.lblStaffCode.TabIndex = 57;
      this.lblStaffCode.Text = "PLEASE ENTER STAFF PASSCODE";
      this.lblStaffCode.TextAlign = ContentAlignment.TopCenter;
      this.lblStaffCode.Visible = false;
      this.pnlLogin.BackgroundImage = (Image) Resources.bg;
      this.pnlLogin.BackgroundImageLayout = ImageLayout.Stretch;
      this.pnlLogin.Controls.Add((Control) this.lblWrongPasscode);
      this.pnlLogin.Controls.Add((Control) this.cmdCancelLogin);
      this.pnlLogin.Controls.Add((Control) this.txtPassword);
      this.pnlLogin.Controls.Add((Control) this.cmdLogin);
      this.pnlLogin.Controls.Add((Control) this.cmdDel);
      this.pnlLogin.Controls.Add((Control) this.cmdKey0);
      this.pnlLogin.Controls.Add((Control) this.cmdKey9);
      this.pnlLogin.Controls.Add((Control) this.cmdKey8);
      this.pnlLogin.Controls.Add((Control) this.cmdKey6);
      this.pnlLogin.Controls.Add((Control) this.cmdKey7);
      this.pnlLogin.Controls.Add((Control) this.cmdKey5);
      this.pnlLogin.Controls.Add((Control) this.cmdKey4);
      this.pnlLogin.Controls.Add((Control) this.cmdKey3);
      this.pnlLogin.Controls.Add((Control) this.cmdKey2);
      this.pnlLogin.Controls.Add((Control) this.cmdKey1);
      this.pnlLogin.Location = new Point(119, 96);
      this.pnlLogin.Name = "pnlLogin";
      this.pnlLogin.Size = new Size(362, 289);
      this.pnlLogin.TabIndex = 21;
      this.lblWrongPasscode.BackColor = Color.Transparent;
      this.lblWrongPasscode.Font = new Font("Arial Black", 14f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblWrongPasscode.ForeColor = Color.Brown;
      this.lblWrongPasscode.Location = new Point(136, 229);
      this.lblWrongPasscode.Name = "lblWrongPasscode";
      this.lblWrongPasscode.Size = new Size(222, 45);
      this.lblWrongPasscode.TabIndex = 58;
      this.lblWrongPasscode.Text = "WRONG PASSCODE";
      this.lblWrongPasscode.TextAlign = ContentAlignment.TopCenter;
      this.lblWrongPasscode.Visible = false;
      this.cmdCancelLogin.BackColor = Color.Transparent;
      this.cmdCancelLogin.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.cmdCancelLogin.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdCancelLogin.FlatAppearance.BorderSize = 0;
      this.cmdCancelLogin.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdCancelLogin.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdCancelLogin.FlatStyle = FlatStyle.Flat;
      this.cmdCancelLogin.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdCancelLogin.ForeColor = Color.White;
      this.cmdCancelLogin.Location = new Point(203, 174);
      this.cmdCancelLogin.Name = "cmdCancelLogin";
      this.cmdCancelLogin.Size = new Size(142, 45);
      this.cmdCancelLogin.TabIndex = 55;
      this.cmdCancelLogin.Text = "CANCEL";
      this.cmdCancelLogin.UseVisualStyleBackColor = false;
      this.cmdCancelLogin.Click += new EventHandler(this.cmdCancelLogin_Click);
      this.txtPassword.Font = new Font("Microsoft Sans Serif", 27.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txtPassword.Location = new Point(85, 9);
      this.txtPassword.MaxLength = 10;
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new Size(180, 49);
      this.txtPassword.TabIndex = 54;
      this.txtPassword.TextAlign = HorizontalAlignment.Center;
      this.cmdLogin.BackColor = Color.Transparent;
      this.cmdLogin.BackgroundImage = (Image) Resources.BlueKeyL;
      this.cmdLogin.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdLogin.FlatAppearance.BorderSize = 0;
      this.cmdLogin.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdLogin.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdLogin.FlatStyle = FlatStyle.Flat;
      this.cmdLogin.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdLogin.ForeColor = Color.Gold;
      this.cmdLogin.Location = new Point(203, 119);
      this.cmdLogin.Name = "cmdLogin";
      this.cmdLogin.Size = new Size(142, 45);
      this.cmdLogin.TabIndex = 53;
      this.cmdLogin.Text = "ENTER";
      this.cmdLogin.UseVisualStyleBackColor = false;
      this.cmdLogin.Click += new EventHandler(this.cmdLogin_Click);
      this.cmdDel.BackColor = Color.Transparent;
      this.cmdDel.BackgroundImage = (Image) Resources.BlueKeyL;
      this.cmdDel.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdDel.FlatAppearance.BorderSize = 0;
      this.cmdDel.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdDel.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdDel.FlatStyle = FlatStyle.Flat;
      this.cmdDel.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdDel.ForeColor = Color.Gold;
      this.cmdDel.Location = new Point(227, 64);
      this.cmdDel.Name = "cmdDel";
      this.cmdDel.Size = new Size(95, 45);
      this.cmdDel.TabIndex = 52;
      this.cmdDel.Text = "DEL";
      this.cmdDel.UseVisualStyleBackColor = false;
      this.cmdDel.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey0.BackColor = Color.Transparent;
      this.cmdKey0.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKey0.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey0.FlatAppearance.BorderSize = 0;
      this.cmdKey0.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey0.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey0.FlatStyle = FlatStyle.Flat;
      this.cmdKey0.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey0.ForeColor = Color.Gold;
      this.cmdKey0.Location = new Point(77, 229);
      this.cmdKey0.Name = "cmdKey0";
      this.cmdKey0.Size = new Size(50, 45);
      this.cmdKey0.TabIndex = 51;
      this.cmdKey0.Text = "0";
      this.cmdKey0.UseVisualStyleBackColor = false;
      this.cmdKey0.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey9.BackColor = Color.Transparent;
      this.cmdKey9.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKey9.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey9.FlatAppearance.BorderSize = 0;
      this.cmdKey9.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey9.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey9.FlatStyle = FlatStyle.Flat;
      this.cmdKey9.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey9.ForeColor = Color.Gold;
      this.cmdKey9.Location = new Point(142, 174);
      this.cmdKey9.Name = "cmdKey9";
      this.cmdKey9.Size = new Size(50, 45);
      this.cmdKey9.TabIndex = 50;
      this.cmdKey9.Text = "9";
      this.cmdKey9.UseVisualStyleBackColor = false;
      this.cmdKey9.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey8.BackColor = Color.Transparent;
      this.cmdKey8.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKey8.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey8.FlatAppearance.BorderSize = 0;
      this.cmdKey8.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey8.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey8.FlatStyle = FlatStyle.Flat;
      this.cmdKey8.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey8.ForeColor = Color.Gold;
      this.cmdKey8.Location = new Point(77, 174);
      this.cmdKey8.Name = "cmdKey8";
      this.cmdKey8.Size = new Size(50, 45);
      this.cmdKey8.TabIndex = 49;
      this.cmdKey8.Text = "8";
      this.cmdKey8.UseVisualStyleBackColor = false;
      this.cmdKey8.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey6.BackColor = Color.Transparent;
      this.cmdKey6.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKey6.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey6.FlatAppearance.BorderSize = 0;
      this.cmdKey6.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey6.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey6.FlatStyle = FlatStyle.Flat;
      this.cmdKey6.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey6.ForeColor = Color.Gold;
      this.cmdKey6.Location = new Point(142, 119);
      this.cmdKey6.Name = "cmdKey6";
      this.cmdKey6.Size = new Size(50, 45);
      this.cmdKey6.TabIndex = 48;
      this.cmdKey6.Text = "6";
      this.cmdKey6.UseVisualStyleBackColor = false;
      this.cmdKey6.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey7.BackColor = Color.Transparent;
      this.cmdKey7.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKey7.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey7.FlatAppearance.BorderSize = 0;
      this.cmdKey7.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey7.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey7.FlatStyle = FlatStyle.Flat;
      this.cmdKey7.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey7.ForeColor = Color.Gold;
      this.cmdKey7.Location = new Point(12, 174);
      this.cmdKey7.Name = "cmdKey7";
      this.cmdKey7.Size = new Size(50, 45);
      this.cmdKey7.TabIndex = 47;
      this.cmdKey7.Text = "7";
      this.cmdKey7.UseVisualStyleBackColor = false;
      this.cmdKey7.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey5.BackColor = Color.Transparent;
      this.cmdKey5.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKey5.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey5.FlatAppearance.BorderSize = 0;
      this.cmdKey5.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey5.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey5.FlatStyle = FlatStyle.Flat;
      this.cmdKey5.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey5.ForeColor = Color.Gold;
      this.cmdKey5.Location = new Point(77, 119);
      this.cmdKey5.Name = "cmdKey5";
      this.cmdKey5.Size = new Size(50, 45);
      this.cmdKey5.TabIndex = 46;
      this.cmdKey5.Text = "5";
      this.cmdKey5.UseVisualStyleBackColor = false;
      this.cmdKey5.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey4.BackColor = Color.Transparent;
      this.cmdKey4.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKey4.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey4.FlatAppearance.BorderSize = 0;
      this.cmdKey4.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey4.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey4.FlatStyle = FlatStyle.Flat;
      this.cmdKey4.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey4.ForeColor = Color.Gold;
      this.cmdKey4.Location = new Point(12, 119);
      this.cmdKey4.Name = "cmdKey4";
      this.cmdKey4.Size = new Size(50, 45);
      this.cmdKey4.TabIndex = 45;
      this.cmdKey4.Text = "4";
      this.cmdKey4.UseVisualStyleBackColor = false;
      this.cmdKey4.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey3.BackColor = Color.Transparent;
      this.cmdKey3.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKey3.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey3.FlatAppearance.BorderSize = 0;
      this.cmdKey3.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey3.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey3.FlatStyle = FlatStyle.Flat;
      this.cmdKey3.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey3.ForeColor = Color.Gold;
      this.cmdKey3.Location = new Point(142, 64);
      this.cmdKey3.Name = "cmdKey3";
      this.cmdKey3.Size = new Size(50, 45);
      this.cmdKey3.TabIndex = 44;
      this.cmdKey3.Text = "3";
      this.cmdKey3.UseVisualStyleBackColor = false;
      this.cmdKey3.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey2.BackColor = Color.Transparent;
      this.cmdKey2.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKey2.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey2.FlatAppearance.BorderSize = 0;
      this.cmdKey2.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey2.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey2.FlatStyle = FlatStyle.Flat;
      this.cmdKey2.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey2.ForeColor = Color.Gold;
      this.cmdKey2.Location = new Point(77, 64);
      this.cmdKey2.Name = "cmdKey2";
      this.cmdKey2.Size = new Size(50, 45);
      this.cmdKey2.TabIndex = 43;
      this.cmdKey2.Text = "2";
      this.cmdKey2.UseVisualStyleBackColor = false;
      this.cmdKey2.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey1.BackColor = Color.Transparent;
      this.cmdKey1.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKey1.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey1.FlatAppearance.BorderSize = 0;
      this.cmdKey1.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey1.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey1.FlatStyle = FlatStyle.Flat;
      this.cmdKey1.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey1.ForeColor = Color.Gold;
      this.cmdKey1.Location = new Point(12, 64);
      this.cmdKey1.Name = "cmdKey1";
      this.cmdKey1.Size = new Size(50, 45);
      this.cmdKey1.TabIndex = 42;
      this.cmdKey1.Text = "1";
      this.cmdKey1.UseVisualStyleBackColor = false;
      this.cmdKey1.Click += new EventHandler(this.ShowEnteredKeys);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.BackgroundImage = (Image) Resources.bg;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(598, 410);
      this.ControlBox = false;
      this.Controls.Add((Control) this.pnlLogin);
      this.Controls.Add((Control) this.lblStaffCode);
      this.Controls.Add((Control) this.lblTelephone);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmLogin);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.Load += new EventHandler(this.frmPayment_Load);
      this.pnlLogin.ResumeLayout(false);
      this.pnlLogin.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
