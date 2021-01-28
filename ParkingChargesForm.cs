// Decompiled with JetBrains decompiler
// Type: TouchPark.ParkingChargesForm
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TouchPark.Controls;
using TouchPark.Properties;

namespace TouchPark
{
  public class ParkingChargesForm : Form
  {
    private ParkingPermitInfo m_ParkingPermit = new ParkingPermitInfo();
    private Timer _clockTimer = new Timer();
    private IContainer components;
    private Label timeLabel;
    private Label lblSelectInstruction;
    private Label lblVRM;
    private Label lblVehicle;
    private Button cmdCancel;
    private Panel pnlHeader;
    private FlowLayoutPanel flowLayoutPanel1;
    private Label label1;
    private Label arrivalTimeLabel;
    private FlowLayoutPanel flowLayoutPanel2;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.timeLabel = new Label();
      this.lblSelectInstruction = new Label();
      this.lblVRM = new Label();
      this.lblVehicle = new Label();
      this.pnlHeader = new Panel();
      this.arrivalTimeLabel = new Label();
      this.label1 = new Label();
      this.cmdCancel = new Button();
      this.flowLayoutPanel1 = new FlowLayoutPanel();
      this.flowLayoutPanel2 = new FlowLayoutPanel();
      this.pnlHeader.SuspendLayout();
      this.SuspendLayout();
      this.timeLabel.BackColor = Color.Transparent;
      this.timeLabel.Font = new Font("Arial Narrow", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.timeLabel.ForeColor = Color.CornflowerBlue;
      this.timeLabel.Location = new Point(-1, -1);
      this.timeLabel.Name = "timeLabel";
      this.timeLabel.Size = new Size(95, 33);
      this.timeLabel.TabIndex = 1;
      this.timeLabel.Text = "05:59:59";
      this.timeLabel.TextAlign = ContentAlignment.BottomRight;
      this.lblSelectInstruction.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblSelectInstruction.Location = new Point(0, 40);
      this.lblSelectInstruction.Name = "lblSelectInstruction";
      this.lblSelectInstruction.Size = new Size(750, 20);
      this.lblSelectInstruction.TabIndex = 2;
      this.lblSelectInstruction.Text = "Please select one of the listed payment options by touching the screen";
      this.lblSelectInstruction.TextAlign = ContentAlignment.MiddleCenter;
      this.lblVRM.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblVRM.Location = new Point(219, 9);
      this.lblVRM.Name = "lblVRM";
      this.lblVRM.Size = new Size(182, 30);
      this.lblVRM.TabIndex = 11;
      this.lblVRM.Text = "123456789012";
      this.lblVRM.TextAlign = ContentAlignment.MiddleCenter;
      this.lblVehicle.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblVehicle.Location = new Point(112, 9);
      this.lblVehicle.Name = "lblVehicle";
      this.lblVehicle.Size = new Size(110, 30);
      this.lblVehicle.TabIndex = 26;
      this.lblVehicle.Text = "Vehicle:";
      this.lblVehicle.TextAlign = ContentAlignment.MiddleCenter;
      this.pnlHeader.BackColor = Color.Transparent;
      this.pnlHeader.Controls.Add((Control) this.arrivalTimeLabel);
      this.pnlHeader.Controls.Add((Control) this.lblVRM);
      this.pnlHeader.Controls.Add((Control) this.label1);
      this.pnlHeader.Controls.Add((Control) this.lblVehicle);
      this.pnlHeader.Controls.Add((Control) this.lblSelectInstruction);
      this.pnlHeader.Controls.Add((Control) this.timeLabel);
      this.pnlHeader.Dock = DockStyle.Top;
      this.pnlHeader.Location = new Point(0, 0);
      this.pnlHeader.Name = "pnlHeader";
      this.pnlHeader.Size = new Size(750, 64);
      this.pnlHeader.TabIndex = 29;
      this.arrivalTimeLabel.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.arrivalTimeLabel.Location = new Point(551, 9);
      this.arrivalTimeLabel.Name = "arrivalTimeLabel";
      this.arrivalTimeLabel.Size = new Size(100, 30);
      this.arrivalTimeLabel.TabIndex = 28;
      this.arrivalTimeLabel.Text = "HH:MM";
      this.arrivalTimeLabel.TextAlign = ContentAlignment.MiddleLeft;
      this.label1.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(395, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(166, 30);
      this.label1.TabIndex = 27;
      this.label1.Text = "Arrival Time:";
      this.label1.TextAlign = ContentAlignment.MiddleRight;
      this.cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.cmdCancel.BackColor = Color.Transparent;
      this.cmdCancel.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.cmdCancel.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdCancel.FlatAppearance.BorderSize = 0;
      this.cmdCancel.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdCancel.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdCancel.FlatStyle = FlatStyle.Flat;
      this.cmdCancel.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdCancel.ForeColor = Color.White;
      this.cmdCancel.Location = new Point(299, 455);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new Size(142, 45);
      this.cmdCancel.TabIndex = 27;
      this.cmdCancel.Text = "CANCEL";
      this.cmdCancel.UseVisualStyleBackColor = false;
      this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
      this.flowLayoutPanel1.BackColor = Color.Transparent;
      this.flowLayoutPanel1.Dock = DockStyle.Top;
      this.flowLayoutPanel1.Location = new Point(0, 64);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new Size(750, 256);
      this.flowLayoutPanel1.TabIndex = 30;
      this.flowLayoutPanel2.BackColor = Color.Transparent;
      this.flowLayoutPanel2.Location = new Point(0, 326);
      this.flowLayoutPanel2.Name = "flowLayoutPanel2";
      this.flowLayoutPanel2.Size = new Size(750, 128);
      this.flowLayoutPanel2.TabIndex = 31;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.BackgroundImage = (Image) Resources.bg;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(750, 512);
      this.ControlBox = false;
      this.Controls.Add((Control) this.flowLayoutPanel2);
      this.Controls.Add((Control) this.flowLayoutPanel1);
      this.Controls.Add((Control) this.cmdCancel);
      this.Controls.Add((Control) this.pnlHeader);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ParkingChargesForm);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.FormClosing += new FormClosingEventHandler(this.ParkingChargesForm_FormClosing);
      this.Load += new EventHandler(this.frmParkingCharge_Load);
      this.Shown += new EventHandler(this.frmParkingCharges_Shown);
      this.pnlHeader.ResumeLayout(false);
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

    public ParkingChargesForm(ParkingPermitInfo parkingPermit)
    {
      this.InitializeComponent();
      this.SetFormRegion();
      this.m_ParkingPermit.VehicleRegMark = parkingPermit.VehicleRegMark;
      this.m_ParkingPermit.CaptureDate = parkingPermit.CaptureDate;
      this.m_ParkingPermit.StartDate = parkingPermit.StartDate;
      this.timeLabel.Text = DateTime.Now.ToLongTimeString();
      this.arrivalTimeLabel.Text = this.m_ParkingPermit.StartDate.ToShortTimeString();
      this._clockTimer.Interval = 1000;
      this._clockTimer.Tick += new EventHandler(this._clockTimer_Tick);
      this._clockTimer.Start();
    }

    private void _clockTimer_Tick(object sender, EventArgs e)
    {
      this.timeLabel.Text = DateTime.Now.ToLongTimeString();
    }

    private void frmParkingCharge_Load(object sender, EventArgs e)
    {
      this.lblVRM.Text = this.m_ParkingPermit.VehicleRegMark;
    }

    private void BuildParkingChargeOptionButtons()
    {
      this.BuildWeeklyParkingTariffs();
      this.BuildDailyParkingTariffs();
    }

    private void BuildDailyParkingTariffs()
    {
      foreach (DailyParkingTariff dailyParkingTariff in (List<DailyParkingTariff>) DailyParkingTariffList.Tariff)
      {
        TimePeriod activePeriod1 = dailyParkingTariff.ActivePeriod;
        if (((TimePeriod) activePeriod1).IsCurrent())
        {
          TimePeriod activePeriod2 = dailyParkingTariff.ActivePeriod;
          PaymentButton paymentAmountButton = this.GetPaymentAmountButton(18, ((TimePeriod)activePeriod2).Duration, this.m_ParkingPermit.StartDate, dailyParkingTariff.Amount);
          paymentAmountButton.Title = dailyParkingTariff.Name;
          paymentAmountButton.BackgroundImage = (Image) Resources.BlueKeyGloss;
          paymentAmountButton.ForceFixedEndDateTime = dailyParkingTariff.IsFixedEndTime;
          PaymentButton paymentButton = paymentAmountButton;
          DateTime today = DateTime.Today;
          ref DateTime local = ref today;
          TimePeriod activePeriod3 = dailyParkingTariff.ActivePeriod;
          TimeSpan end = ((TimePeriod)activePeriod3).End;
          DateTime dateTime = local.Add(end);
          paymentButton.EndDate = dateTime;
          paymentAmountButton.SetText();
          this.flowLayoutPanel2.Controls.Add((Control) paymentAmountButton);
        }
      }
    }

    private void BuildWeeklyParkingTariffs()
    {
      WeeklyParkingTariff weeklyParkingTariff = new WeeklyParkingTariff(this.m_ParkingPermit.DurationOfStay);
      for (int index = 0; index < weeklyParkingTariff.ValidOptionsCount; ++index)
      {
        TimeSpan durationFromTariff = weeklyParkingTariff.GetDurationFromTariff(index);
        if (durationFromTariff != TimeSpan.MinValue && durationFromTariff != TimeSpan.Zero)
          this.flowLayoutPanel1.Controls.Add((Control) this.GetPaymentAmountButton(index + 1, durationFromTariff, this.m_ParkingPermit.StartDate, weeklyParkingTariff.AmountOptions[index]));
      }
    }

    private PaymentButton GetPaymentAmountButton(
      int index,
      TimeSpan durationOfStay,
      DateTime startDateTime,
      Decimal amount)
    {
      PaymentButton paymentButton = new PaymentButton(index, durationOfStay, startDateTime, amount);
      paymentButton.Click += new EventHandler(this.PaymentOption_click);
      return paymentButton;
    }

    private void PaymentOption_click(object sender, EventArgs e)
    {
      if (!(sender.GetType() == typeof (PaymentButton)))
        return;
      PaymentButton paymentButton = (PaymentButton) sender;
      this.m_ParkingPermit.Amount = paymentButton.Amount;
      this.m_ParkingPermit.EndDate = paymentButton.ForceFixedEndDateTime ? paymentButton.EndDate : this.m_ParkingPermit.StartDate.Add(paymentButton.DurationOfStay);
      this.Visible = false;
      PaymentForm paymentForm = new PaymentForm(this.m_ParkingPermit, "DisplayPayment");
      int num = (int) paymentForm.ShowDialog();
      paymentForm.Dispose();
      this.Close();
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void frmParkingCharges_Shown(object sender, EventArgs e)
    {
      this.BuildParkingChargeOptionButtons();
    }

    private void SetFormRegion()
    {
      this.Region = Region.FromHrgn(ParkingChargesForm.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
    }

    private void ParkingChargesForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      this._clockTimer.Stop();
    }
  }
}
