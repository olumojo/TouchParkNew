// Decompiled with JetBrains decompiler
// Type: TouchPark.OutOfOrderForm
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using TouchPark.CashDevices;

namespace TouchPark
{
  public class OutOfOrderForm : Form
  {
    private IContainer components;
    private Label messageLabel;
    private Timer coinMechanismCheckTimer;
    private Panel panel1;
    private Button btnRestart;
    private Label lblRestarting;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.messageLabel = new Label();
      this.coinMechanismCheckTimer = new Timer(this.components);
      this.panel1 = new Panel();
      this.btnRestart = new Button();
      this.lblRestarting = new Label();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.messageLabel.Dock = DockStyle.Fill;
      this.messageLabel.Font = new Font("Microsoft Sans Serif", 24f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.messageLabel.ForeColor = Color.White;
      this.messageLabel.Location = new Point(0, 0);
      this.messageLabel.Name = "messageLabel";
      this.messageLabel.Size = new Size(800, 600);
      this.messageLabel.TabIndex = 0;
      this.messageLabel.Text = "This machine is out of order Please phone 0845 6024946";
      this.messageLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.messageLabel.Click += new EventHandler(this.messageLabel_Click);
      this.coinMechanismCheckTimer.Enabled = true;
      this.coinMechanismCheckTimer.Interval = 20000;
      this.coinMechanismCheckTimer.Tick += new EventHandler(this.tmeCheckIfStillError_Tick);
      this.panel1.Controls.Add((Control) this.btnRestart);
      this.panel1.Controls.Add((Control) this.lblRestarting);
      this.panel1.Dock = DockStyle.Bottom;
      this.panel1.Location = new Point(0, 500);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(800, 100);
      this.panel1.TabIndex = 4;
      this.btnRestart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.btnRestart.BackColor = SystemColors.ButtonFace;
      this.btnRestart.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btnRestart.Location = new Point(363, 39);
      this.btnRestart.Name = "btnRestart";
      this.btnRestart.Size = new Size(93, 35);
      this.btnRestart.TabIndex = 4;
      this.btnRestart.Text = "RESTART";
      this.btnRestart.UseVisualStyleBackColor = false;
      this.btnRestart.Click += new EventHandler(this.btnRestart_Click);
      this.lblRestarting.Dock = DockStyle.Top;
      this.lblRestarting.Font = new Font("Microsoft Sans Serif", 24f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblRestarting.ForeColor = Color.White;
      this.lblRestarting.Location = new Point(0, 0);
      this.lblRestarting.Name = "lblRestarting";
      this.lblRestarting.Size = new Size(800, 37);
      this.lblRestarting.TabIndex = 3;
      this.lblRestarting.Text = "Restarting System ...";
      this.lblRestarting.TextAlign = ContentAlignment.MiddleCenter;
      this.lblRestarting.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.SteelBlue;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(800, 600);
      this.ControlBox = false;
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.messageLabel);
      this.DoubleBuffered = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (OutOfOrderForm);
      this.ShowIcon = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.WindowState = FormWindowState.Maximized;
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public OutOfOrderForm()
      : this(true)
    {
    }

    public OutOfOrderForm(bool checkCoinMechanism)
    {
      this.InitializeComponent();
      this.coinMechanismCheckTimer.Enabled = checkCoinMechanism;
      Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
      this.Size = new Size(workingArea.Width, workingArea.Height);
      this.Location = new Point(0, 0);
      if (ConfigurationManager.AppSettings["PaymentErrorMessage"] == null || !(ConfigurationManager.AppSettings["PaymentErrorMessage"] != ""))
        return;
      this.messageLabel.Text = ConfigurationManager.AppSettings["PaymentErrorMessage"];
    }

    private void tmeCheckIfStillError_Tick(object sender, EventArgs e)
    {
      try
      {
        Devices.CoinMechanism = new CoinMechanism();
        this.Close();
      }
      catch (CoinMechanismException ex)
      {
      }
    }

    private void messageLabel_Click(object sender, EventArgs e)
    {
      this.Refresh();
    }

    private void btnRestart_Click(object sender, EventArgs e)
    {
      this.lblRestarting.Visible = true;
      this.Cursor = Cursors.WaitCursor;
      this.Refresh();
      Application.Restart();
    }
  }
}
