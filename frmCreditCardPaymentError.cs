// Decompiled with JetBrains decompiler
// Type: TouchPark.frmCreditCardPaymentError
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
  public class frmCreditCardPaymentError : Form
  {
    public bool retryPayment;
    private IContainer components;
    private Timer CashReceiptTimer;
    private Label lblCorrectMoney;
    private Button cmdNo;
    private Button cmdYes;

    [DllImport("Gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse);

    public frmCreditCardPaymentError()
    {
      this.InitializeComponent();
      this.Region = Region.FromHrgn(frmCreditCardPaymentError.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
    }

    private void frmPayment_Load(object sender, EventArgs e)
    {
    }

    private void cmdYes_Click(object sender, EventArgs e)
    {
      this.retryPayment = true;
      this.Hide();
    }

    private void cmdNo_Click(object sender, EventArgs e)
    {
      this.retryPayment = false;
      this.Hide();
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
      this.CashReceiptTimer = new Timer(this.components);
      this.lblCorrectMoney = new Label();
      this.cmdNo = new Button();
      this.cmdYes = new Button();
      this.SuspendLayout();
      this.lblCorrectMoney.BackColor = Color.Transparent;
      this.lblCorrectMoney.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblCorrectMoney.Location = new Point(49, 140);
      this.lblCorrectMoney.Name = "lblCorrectMoney";
      this.lblCorrectMoney.Size = new Size(528, 64);
      this.lblCorrectMoney.TabIndex = 18;
      this.lblCorrectMoney.Text = "THE CARD TRANSACTION FAILED\r\nDO YOU WISH TO RETRY PAYMENT?";
      this.lblCorrectMoney.TextAlign = ContentAlignment.MiddleCenter;
      this.cmdNo.BackColor = Color.Transparent;
      this.cmdNo.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.cmdNo.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdNo.FlatAppearance.BorderSize = 0;
      this.cmdNo.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdNo.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdNo.FlatStyle = FlatStyle.Flat;
      this.cmdNo.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdNo.ForeColor = Color.White;
      this.cmdNo.Location = new Point(353, 243);
      this.cmdNo.Name = "cmdNo";
      this.cmdNo.Size = new Size(142, 67);
      this.cmdNo.TabIndex = 19;
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
      this.cmdYes.Location = new Point(131, 243);
      this.cmdYes.Name = "cmdYes";
      this.cmdYes.Size = new Size(142, 67);
      this.cmdYes.TabIndex = 20;
      this.cmdYes.Text = "YES";
      this.cmdYes.UseVisualStyleBackColor = false;
      this.cmdYes.Click += new EventHandler(this.cmdYes_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.BackgroundImage = (Image) Resources.bg;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(622, 452);
      this.ControlBox = false;
      this.Controls.Add((Control) this.cmdYes);
      this.Controls.Add((Control) this.cmdNo);
      this.Controls.Add((Control) this.lblCorrectMoney);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmCreditCardPaymentError);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.Load += new EventHandler(this.frmPayment_Load);
      this.ResumeLayout(false);
    }
  }
}
