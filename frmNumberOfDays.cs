// Decompiled with JetBrains decompiler
// Type: TouchPark.frmNumberOfDays
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TouchPark.Properties;

namespace TouchPark
{
  public class frmNumberOfDays : Form
  {
    private ParkingPermitInfo m_ParkingPermit = new ParkingPermitInfo();
    private string m_DisplayOption;
    private IContainer components;
    private Label lblNumberOfDays;
    private Panel pnlDays;
    private Button cmdCancel;
    private TextBox txtDays;
    private Button cmdEnter;
    private Button cmdDel;
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
    private Label lblDays;
    private Label lblError;
    private Label lblTooManyDays;

    [DllImport("Gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse);

    public frmNumberOfDays(ParkingPermitInfo parkingPermit, string displayOption)
    {
      this.InitializeComponent();
      this.Region = Region.FromHrgn(frmNumberOfDays.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
      this.m_ParkingPermit.copy(parkingPermit);
      this.m_DisplayOption = displayOption;
    }

    private void frmNumberOfDays_Load(object sender, EventArgs e)
    {
    }

    private void ShowEnteredKeys(object sender, EventArgs e)
    {
      Button button = (Button) sender;
      if (button.Text.ToUpper() == "DEL")
      {
        if (this.txtDays.Text.Length <= 0)
          return;
        this.txtDays.Text = this.txtDays.Text.Substring(0, this.txtDays.Text.Length - 1);
      }
      else
      {
        if (this.txtDays.Text.Length > 4)
          return;
        this.txtDays.Text += button.Text;
      }
    }

    private void cmdEnter_Click(object sender, EventArgs e)
    {
      if (StringTools.IsNumeric(this.txtDays.Text))
      {
        if (Settings.Default.MaxNumberOfDaysToPermitFor >= double.Parse(this.txtDays.Text + ".0"))
        {
          this.m_ParkingPermit.EndDate = this.m_ParkingPermit.StartDate.AddDays(double.Parse(this.txtDays.Text + ".0"));
          bool xml = Utilites.WriteParkingPermitToXML(this.m_ParkingPermit);
          this.Hide();
          new frmThankYou(this.m_ParkingPermit, this.m_DisplayOption, xml).Show();
          this.Close();
        }
        else
        {
          this.lblTooManyDays.Visible = true;
          this.lblError.Visible = true;
        }
      }
      else
        this.lblError.Visible = true;
    }

    private void cmdCancel_Click(object sender, EventArgs e)
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
      this.lblNumberOfDays = new Label();
      this.pnlDays = new Panel();
      this.lblError = new Label();
      this.lblDays = new Label();
      this.cmdCancel = new Button();
      this.txtDays = new TextBox();
      this.cmdEnter = new Button();
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
      this.lblTooManyDays = new Label();
      this.pnlDays.SuspendLayout();
      this.SuspendLayout();
      this.lblNumberOfDays.BackColor = Color.Transparent;
      this.lblNumberOfDays.Font = new Font("Arial Black", 15f);
      this.lblNumberOfDays.Location = new Point(155, 23);
      this.lblNumberOfDays.Name = "lblNumberOfDays";
      this.lblNumberOfDays.Size = new Size(332, 67);
      this.lblNumberOfDays.TabIndex = 58;
      this.lblNumberOfDays.Text = "PLEASE ENTER NUMBER OF DAYS PARKING REQUIRED";
      this.lblNumberOfDays.TextAlign = ContentAlignment.TopCenter;
      this.pnlDays.BackgroundImage = (Image) Resources.bg;
      this.pnlDays.BackgroundImageLayout = ImageLayout.Stretch;
      this.pnlDays.Controls.Add((Control) this.lblTooManyDays);
      this.pnlDays.Controls.Add((Control) this.lblError);
      this.pnlDays.Controls.Add((Control) this.lblDays);
      this.pnlDays.Controls.Add((Control) this.cmdCancel);
      this.pnlDays.Controls.Add((Control) this.txtDays);
      this.pnlDays.Controls.Add((Control) this.cmdEnter);
      this.pnlDays.Controls.Add((Control) this.cmdDel);
      this.pnlDays.Controls.Add((Control) this.cmdKey0);
      this.pnlDays.Controls.Add((Control) this.cmdKey9);
      this.pnlDays.Controls.Add((Control) this.cmdKey8);
      this.pnlDays.Controls.Add((Control) this.cmdKey6);
      this.pnlDays.Controls.Add((Control) this.cmdKey7);
      this.pnlDays.Controls.Add((Control) this.cmdKey5);
      this.pnlDays.Controls.Add((Control) this.cmdKey4);
      this.pnlDays.Controls.Add((Control) this.cmdKey3);
      this.pnlDays.Controls.Add((Control) this.cmdKey2);
      this.pnlDays.Controls.Add((Control) this.cmdKey1);
      this.pnlDays.Location = new Point(140, 93);
      this.pnlDays.Name = "pnlDays";
      this.pnlDays.Size = new Size(362, 289);
      this.pnlDays.TabIndex = 59;
      this.lblError.BackColor = Color.Transparent;
      this.lblError.Font = new Font("Arial Black", 10f);
      this.lblError.ForeColor = Color.Brown;
      this.lblError.Location = new Point(137, 241);
      this.lblError.Name = "lblError";
      this.lblError.Size = new Size(225, 45);
      this.lblError.TabIndex = 60;
      this.lblError.Text = "PLEASE ENTER A VALID NUMBER";
      this.lblError.TextAlign = ContentAlignment.TopCenter;
      this.lblError.Visible = false;
      this.lblDays.BackColor = Color.Transparent;
      this.lblDays.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblDays.Location = new Point(214, 20);
      this.lblDays.Name = "lblDays";
      this.lblDays.Size = new Size(100, 38);
      this.lblDays.TabIndex = 59;
      this.lblDays.Text = "DAYS";
      this.lblDays.TextAlign = ContentAlignment.TopCenter;
      this.cmdCancel.BackColor = Color.Transparent;
      this.cmdCancel.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.cmdCancel.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdCancel.FlatAppearance.BorderSize = 0;
      this.cmdCancel.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdCancel.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdCancel.FlatStyle = FlatStyle.Flat;
      this.cmdCancel.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdCancel.ForeColor = Color.White;
      this.cmdCancel.Location = new Point(203, 174);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new Size(142, 45);
      this.cmdCancel.TabIndex = 55;
      this.cmdCancel.Text = "CANCEL";
      this.cmdCancel.UseVisualStyleBackColor = false;
      this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
      this.txtDays.Font = new Font("Microsoft Sans Serif", 27.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txtDays.Location = new Point(33, 9);
      this.txtDays.MaxLength = 10;
      this.txtDays.Name = "txtDays";
      this.txtDays.Size = new Size(180, 49);
      this.txtDays.TabIndex = 54;
      this.txtDays.TextAlign = HorizontalAlignment.Center;
      this.cmdEnter.BackColor = Color.Transparent;
      this.cmdEnter.BackgroundImage = (Image) Resources.BlueKeyL;
      this.cmdEnter.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdEnter.FlatAppearance.BorderSize = 0;
      this.cmdEnter.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdEnter.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdEnter.FlatStyle = FlatStyle.Flat;
      this.cmdEnter.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdEnter.ForeColor = Color.Gold;
      this.cmdEnter.Location = new Point(203, 119);
      this.cmdEnter.Name = "cmdEnter";
      this.cmdEnter.Size = new Size(142, 45);
      this.cmdEnter.TabIndex = 53;
      this.cmdEnter.Text = "ENTER";
      this.cmdEnter.UseVisualStyleBackColor = false;
      this.cmdEnter.Click += new EventHandler(this.cmdEnter_Click);
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
      this.lblTooManyDays.BackColor = Color.Transparent;
      this.lblTooManyDays.Font = new Font("Arial Black", 10f);
      this.lblTooManyDays.ForeColor = Color.Brown;
      this.lblTooManyDays.Location = new Point(138, 224);
      this.lblTooManyDays.Name = "lblTooManyDays";
      this.lblTooManyDays.Size = new Size(225, 17);
      this.lblTooManyDays.TabIndex = 61;
      this.lblTooManyDays.Text = "TOO MANY DAYS ENTERED";
      this.lblTooManyDays.TextAlign = ContentAlignment.TopCenter;
      this.lblTooManyDays.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Wheat;
      this.BackgroundImage = (Image) Resources.bg;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(652, 475);
      this.ControlBox = false;
      this.Controls.Add((Control) this.pnlDays);
      this.Controls.Add((Control) this.lblNumberOfDays);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmNumberOfDays);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (frmNumberOfDays);
      this.TopMost = true;
      this.Load += new EventHandler(this.frmNumberOfDays_Load);
      this.pnlDays.ResumeLayout(false);
      this.pnlDays.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
