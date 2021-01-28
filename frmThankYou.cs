// Decompiled with JetBrains decompiler
// Type: TouchPark.frmThankYou
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TouchPark.CardDevices;
using TouchPark.Controls;
using TouchPark.Properties;
using TouchPark.Reporting;

namespace TouchPark
{
  public class frmThankYou : Form
  {
    private string m_PhoneNumber = Settings.Default.RangerTelephoneNumber;
    private string m_ApplicationType = Settings.Default.ApplicationType;
    public string ThankYouCode = string.Empty;
    private ParkingPermitInfo m_ParkingPermit = new ParkingPermitInfo();
    private bool _linesOn = true;
    private const string THANK_YOU_MESSAGE = "Thank you for parking at ";
    private IContainer components;
    private Label lblThankYou;
    private Label lblForParking;
    private Timer PaidInFullTimer;
    private Button cmdClose;
    private Label lblLeavingMessage;
    private Label lblVehicleAdded;
    private Label lblDate;
    private Label lblStaff;
    private Button cmdPrint;
    private Label printLabel;
    private PrintDocument printDocumentReceipt;
    private GrowLabel lblMessageWarning;
    private string m_receiptID;
    private CardDeviceResponse m_responseFile;
    private string m_DisplayOption;
    private bool m_PaidInFullDisplayed;
    private bool m_PaidInFull;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmThankYou));
      this.lblThankYou = new Label();
      this.lblForParking = new Label();
      this.PaidInFullTimer = new Timer(this.components);
      this.printDocumentReceipt = new PrintDocument();
      this.cmdClose = new Button();
      this.lblLeavingMessage = new Label();
      this.lblVehicleAdded = new Label();
      this.lblDate = new Label();
      this.lblStaff = new Label();
      this.cmdPrint = new Button();
      this.printLabel = new Label();
      this.lblMessageWarning = new GrowLabel();
      this.SuspendLayout();
      this.lblThankYou.BackColor = Color.Transparent;
      this.lblThankYou.Font = new Font("Arial", 36f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblThankYou.Location = new Point(153, 9);
      this.lblThankYou.Name = "lblThankYou";
      this.lblThankYou.Size = new Size(320, 60);
      this.lblThankYou.TabIndex = 5;
      this.lblThankYou.Text = "THANK YOU";
      this.lblThankYou.TextAlign = ContentAlignment.MiddleCenter;
      this.lblThankYou.Visible = false;
      this.lblForParking.BackColor = Color.Transparent;
      this.lblForParking.Font = new Font("Arial Black", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblForParking.Location = new Point(19, 89);
      this.lblForParking.Name = "lblForParking";
      this.lblForParking.Size = new Size(584, 30);
      this.lblForParking.TabIndex = 6;
      this.lblForParking.Text = "FOR PARKING AT SITENAME";
      this.lblForParking.TextAlign = ContentAlignment.MiddleCenter;
      this.lblForParking.Visible = false;
      this.PaidInFullTimer.Interval = 15000;
      this.PaidInFullTimer.Tick += new EventHandler(this.PaidInFullTimer_Tick);
      this.printDocumentReceipt.PrintPage += new PrintPageEventHandler(this.printDocumentReceipt_PrintPage);
      this.cmdClose.BackColor = Color.Transparent;
      this.cmdClose.BackgroundImage = (Image) Resources.GreenKey;
      this.cmdClose.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdClose.FlatAppearance.BorderSize = 0;
      this.cmdClose.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdClose.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdClose.FlatStyle = FlatStyle.Flat;
      this.cmdClose.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdClose.ForeColor = Color.DarkGreen;
      this.cmdClose.Location = new Point(193, 300);
      this.cmdClose.Name = "cmdClose";
      this.cmdClose.Size = new Size(238, 83);
      this.cmdClose.TabIndex = 17;
      this.cmdClose.Text = "PRESS HERE FOR \r\nNEXT VEHICLE";
      this.cmdClose.UseVisualStyleBackColor = false;
      this.cmdClose.Visible = false;
      this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
      this.lblLeavingMessage.BackColor = Color.Transparent;
      this.lblLeavingMessage.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblLeavingMessage.Location = new Point(19, 232);
      this.lblLeavingMessage.Name = "lblLeavingMessage";
      this.lblLeavingMessage.Size = new Size(584, 30);
      this.lblLeavingMessage.TabIndex = 18;
      this.lblLeavingMessage.Text = "ARE YOU LEAVING THE CAR PARK NOW?";
      this.lblLeavingMessage.TextAlign = ContentAlignment.TopCenter;
      this.lblLeavingMessage.Visible = false;
      this.lblVehicleAdded.BackColor = Color.Transparent;
      this.lblVehicleAdded.Font = new Font("Arial Black", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblVehicleAdded.Location = new Point(15, 59);
      this.lblVehicleAdded.Name = "lblVehicleAdded";
      this.lblVehicleAdded.Size = new Size(588, 30);
      this.lblVehicleAdded.TabIndex = 19;
      this.lblVehicleAdded.Text = "THIS VEHICLE HAS BEEN ADDED TO THE AUTHORISED LIST";
      this.lblVehicleAdded.TextAlign = ContentAlignment.MiddleCenter;
      this.lblVehicleAdded.Visible = false;
      this.lblDate.BackColor = Color.Transparent;
      this.lblDate.Font = new Font("Arial Black", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblDate.Location = new Point(17, 119);
      this.lblDate.Name = "lblDate";
      this.lblDate.Size = new Size(593, 30);
      this.lblDate.TabIndex = 21;
      this.lblDate.Text = "FOR TODAY ONLY 12th November 2008";
      this.lblDate.TextAlign = ContentAlignment.MiddleCenter;
      this.lblDate.Visible = false;
      this.lblStaff.BackColor = Color.Transparent;
      this.lblStaff.Font = new Font("Arial Black", 14f);
      this.lblStaff.Location = new Point(12, 149);
      this.lblStaff.Name = "lblStaff";
      this.lblStaff.Size = new Size(598, 83);
      this.lblStaff.TabIndex = 22;
      this.lblStaff.Text = "IF YOU HAVE MORE THAN 2 VEHICLES REGISTERED UNDER YOUR NAME YOU WILL NEED TO GET AUTHORISATION FROM THE STORE MANAGER";
      this.lblStaff.TextAlign = ContentAlignment.MiddleCenter;
      this.lblStaff.Visible = false;
      this.cmdPrint.BackColor = Color.Transparent;
      this.cmdPrint.BackgroundImage = (Image) Resources.BlueKeyL;
      this.cmdPrint.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdPrint.FlatAppearance.BorderSize = 0;
      this.cmdPrint.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdPrint.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdPrint.FlatStyle = FlatStyle.Flat;
      this.cmdPrint.Font = new Font("Arial", 10f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdPrint.ForeColor = Color.Gold;
      this.cmdPrint.Location = new Point(221, 389);
      this.cmdPrint.Name = "cmdPrint";
      this.cmdPrint.Size = new Size(190, 60);
      this.cmdPrint.TabIndex = 23;
      this.cmdPrint.Text = "PRINT RECEIPT";
      this.cmdPrint.UseVisualStyleBackColor = false;
      this.cmdPrint.Visible = false;
      this.cmdPrint.Click += new EventHandler(this.cmdPrint_Click);
      this.printLabel.AutoSize = true;
      this.printLabel.BackColor = Color.White;
      this.printLabel.Location = new Point(437, 279);
      this.printLabel.Name = "printLabel";
      this.printLabel.Size = new Size(233, 299);
      this.printLabel.TabIndex = 24;
      this.printLabel.Text = componentResourceManager.GetString("printLabel.Text");
      this.printLabel.Visible = false;
      this.lblMessageWarning.AutoSize = true;
      this.lblMessageWarning.BackColor = Color.Transparent;
      this.lblMessageWarning.Font = new Font("Arial Black", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblMessageWarning.ForeColor = Color.DarkRed;
      this.lblMessageWarning.Location = new Point(14, 267);
      this.lblMessageWarning.Name = "lblMessageWarning";
      this.lblMessageWarning.Size = new Size(169, 22);
      this.lblMessageWarning.TabIndex = 25;
      this.lblMessageWarning.Text = "lblMessageWarning";
      this.lblMessageWarning.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.BackgroundImage = (Image) Resources.bg;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(622, 452);
      this.ControlBox = false;
      this.Controls.Add((Control) this.lblMessageWarning);
      this.Controls.Add((Control) this.printLabel);
      this.Controls.Add((Control) this.cmdPrint);
      this.Controls.Add((Control) this.lblDate);
      this.Controls.Add((Control) this.lblVehicleAdded);
      this.Controls.Add((Control) this.lblLeavingMessage);
      this.Controls.Add((Control) this.cmdClose);
      this.Controls.Add((Control) this.lblForParking);
      this.Controls.Add((Control) this.lblThankYou);
      this.Controls.Add((Control) this.lblStaff);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmThankYou);
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

    public frmThankYou(ParkingPermitInfo parkingPermit, string displayOption)
      : this(parkingPermit, displayOption, false, (CardDeviceResponse) null)
    {
    }

    public frmThankYou(
      ParkingPermitInfo parkingPermit,
      string displayOption,
      bool updateSuccessful)
      : this(parkingPermit, displayOption, updateSuccessful, (CardDeviceResponse) null)
    {
    }

    public frmThankYou(
      ParkingPermitInfo parkingPermit,
      string displayOption,
      bool updateSuccessful,
      CardDeviceResponse response)
    {
      Utilites.WriteToLog("frmThankYou entered");
      this.m_receiptID = "A" + DateTime.Now.ToString("MMHH") + Environment.GetEnvironmentVariable("RSSITECODE") + DateTime.Now.ToString("ddmmyy");
      if (string.IsNullOrEmpty(this.ThankYouCode))
        this.ThankYouCode = Settings.Default.ThankYouCode;
      this.InitializeComponent();
      this.Region = Region.FromHrgn(frmThankYou.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
      this.m_ParkingPermit.copy(parkingPermit);
      this.m_DisplayOption = displayOption;
      this.m_PaidInFull = updateSuccessful;
      if (this.ThankYouCode == null)
        this.ThankYouCode = "NONE";
      this.m_responseFile = response;
      Utilites.WriteToLog("frmThankYou exited");
    }

    private void PlaceWarningMessageOnScreen()
    {
      this.lblMessageWarning.Visible = false;
      if (!(this.m_ParkingPermit.PaymentType == "ACCOUNT_HOLDER"))
        return;
      this.lblMessageWarning.Text = "If you are not a registered account holder \r\n you will still receive a Parking Charge Notice.";
      this.lblMessageWarning.Visible = true;
      this.lblMessageWarning.Location = new Point((this.Width - this.lblMessageWarning.Width) / 2, 250);
    }

    private string FormatThankYouMessage()
    {
      return string.Format("{0}{1}", (object) "Thank you for parking at ", (object) Settings.Default.VehicleDataCaptureSite);
    }

    private void displayNormal()
    {
      Utilites.WriteToLog("displayNormal entered");
      string str = this.FormatThankYouMessage();
      int width1 = this.Width;
      int width2 = this.lblThankYou.Width;
      this.lblThankYou.Location = new Point((width1 - width2) / 2, 40);
      this.lblForParking.Text = str;
      int width3 = this.lblForParking.Width;
      this.lblForParking.Location = new Point((width1 - width3) / 2, 110);
      int width4 = this.cmdPrint.Width;
      this.cmdPrint.Location = new Point((width1 - width4) / 2, 250);
      this.cmdClose.Location = new Point((width1 - this.cmdClose.Width) / 2, 150);
      this.lblThankYou.Visible = true;
      this.lblForParking.Visible = true;
      this.cmdClose.Visible = true;
      if (this.m_ApplicationType.ToUpper() == "TOUCHPARK")
        this.cmdPrint.Visible = true;
      this.m_PaidInFullDisplayed = true;
      this.PaidInFullTimer.Enabled = true;
      Utilites.WriteToLog("displayNormal exited");
    }

    private void displayTescoStaff()
    {
      Utilites.WriteToLog("displayTescoStaff entered");
      this.FormatThankYouMessage();
      int width1 = this.Width;
      int width2 = this.lblThankYou.Width;
      this.lblThankYou.Location = new Point((width1 - width2) / 2, 40);
      int width3 = this.lblVehicleAdded.Width;
      this.lblVehicleAdded.Location = new Point((width1 - width3) / 2, 95);
      int width4 = this.lblStaff.Width;
      this.lblStaff.Location = new Point((width1 - width4) / 2, 130);
      this.cmdClose.Location = new Point((width1 - this.cmdClose.Width) / 2, 260);
      this.lblThankYou.Visible = true;
      this.lblVehicleAdded.Visible = true;
      this.cmdClose.Visible = true;
      this.lblStaff.Visible = true;
      this.m_PaidInFullDisplayed = true;
      this.PaidInFullTimer.Enabled = true;
      Utilites.WriteToLog("displayTescoStaff exited");
    }

    private void displayTescoVistor()
    {
      Utilites.WriteToLog("displayTescoVistor entered");
      this.FormatThankYouMessage();
      int width1 = this.Width;
      int width2 = this.lblThankYou.Width;
      this.lblThankYou.Location = new Point((width1 - width2) / 2, 40);
      int width3 = this.lblVehicleAdded.Width;
      this.lblVehicleAdded.Location = new Point((width1 - width3) / 2, 95);
      this.lblDate.Text = "FOR TODAY ONLY " + DateTime.Now.ToLongDateString();
      int width4 = this.lblDate.Width;
      this.lblDate.Location = new Point((width1 - width3) / 2, 120);
      this.cmdClose.Location = new Point((width1 - this.cmdClose.Width) / 2, 250);
      this.lblThankYou.Visible = true;
      this.lblVehicleAdded.Visible = true;
      this.cmdClose.Visible = true;
      this.lblDate.Visible = true;
      this.m_PaidInFullDisplayed = true;
      this.PaidInFullTimer.Enabled = true;
      Utilites.WriteToLog("displayTescoVistor exited");
    }

    private void displayTravelodge()
    {
      Utilites.WriteToLog("displayTravelodge entered");
      string str = this.FormatThankYouMessage();
      int width1 = this.Width;
      int width2 = this.lblThankYou.Width;
      this.lblThankYou.Location = new Point((width1 - width2) / 2, 40);
      this.lblForParking.Text = str;
      int width3 = this.lblForParking.Width;
      this.lblForParking.Location = new Point((width1 - width3) / 2, 95);
      int width4 = this.lblVehicleAdded.Width;
      this.lblVehicleAdded.Location = new Point((width1 - width4) / 2, 130);
      this.lblDate.Text = "FROM " + this.m_ParkingPermit.StartDate.ToString("dd/MM/yyyy") + " TO " + this.m_ParkingPermit.EndDate.ToString("dd/MM/yyyy") + " (inclusive)";
      int width5 = this.lblDate.Width;
      this.lblDate.Location = new Point((width1 - width5) / 2, 165);
      this.PlaceWarningMessageOnScreen();
      this.cmdClose.Location = new Point((width1 - this.cmdClose.Width) / 2, 300);
      this.lblThankYou.Visible = true;
      this.lblVehicleAdded.Visible = true;
      this.cmdClose.Visible = true;
      this.lblDate.Visible = true;
      this.lblForParking.Visible = true;
      this.m_PaidInFullDisplayed = true;
      this.PaidInFullTimer.Enabled = true;
      Utilites.WriteToLog("displayTravelodge exited");
    }

    private void frmPayment_Load(object sender, EventArgs e)
    {
      Utilites.WriteToLog("frmPayment_Load entered");
      if (this.m_PaidInFull)
      {
        if (this.m_DisplayOption.ToUpper().Equals("PERMITPARK2"))
        {
          switch (this.m_ParkingPermit.PermitType)
          {
            case "STAFF":
              this.displayNormal();
              break;
            case "VISITOR":
              this.displayTravelodge();
              break;
            default:
              this.displayNormal();
              break;
          }
        }
        else if (this.ThankYouCode.ToString().ToUpper().Equals("TESCO"))
        {
          switch (this.m_ParkingPermit.PermitType)
          {
            case "STAFF":
              this.displayTescoStaff();
              break;
            case "VISITOR":
              this.displayTescoVistor();
              break;
            default:
              this.displayNormal();
              break;
          }
        }
        else if (this.ThankYouCode.ToUpper().Equals("TRAVELODGE"))
          this.displayTravelodge();
        else if (this.ThankYouCode.ToString().ToUpper().Equals("TYNORMAL"))
        {
          switch (this.m_ParkingPermit.PermitType)
          {
            case "STAFF":
              this.displayNormal();
              break;
            case "VISITOR":
              this.displayTescoVistor();
              break;
            default:
              this.displayNormal();
              break;
          }
        }
        else
          this.displayNormal();
      }
      else
      {
        int width1 = this.Width;
        this.lblLeavingMessage.Text = "There was a problem issusing a permit";
        int width2 = this.lblLeavingMessage.Width;
        this.lblLeavingMessage.Location = new Point((width1 - width2) / 2 - 10, 40);
        this.lblForParking.Text = ConfigurationManager.AppSettings["PaymentErrorMessage"].ToString();
        int width3 = this.lblForParking.Width;
        this.lblForParking.Location = new Point((width1 - width3) / 2 - 10, 120);
        this.cmdClose.Location = new Point((width1 - this.cmdClose.Width) / 2 - 10, 150);
        this.lblLeavingMessage.Visible = true;
        this.lblForParking.Visible = true;
        this.cmdClose.Visible = true;
      }
      this.m_PaidInFull = false;
      Utilites.WriteToLog("frmPayment_Load exited");
    }

    private void PaidInFullTimer_Tick(object sender, EventArgs e)
    {
      Utilites.WriteToLog("PaidInFullTimer_Tick entered");
      if (this.m_PaidInFullDisplayed)
        this.Close();
      Utilites.WriteToLog("PaidInFullTimer_Tick exited");
    }

    private void PrintStringInMiddle(
      string stringToPrint,
      Font printFont,
      float middleOfPage,
      Graphics gdiPage,
      float linePos)
    {
      float x = middleOfPage - gdiPage.MeasureString(stringToPrint, printFont).Width / 2f;
      gdiPage.DrawString(stringToPrint, printFont, Brushes.Black, x, linePos);
    }

    private void PrintStringOnLeft(
      string stringToPrint,
      Font printFont,
      float margin,
      Graphics gdiPage,
      float linePos)
    {
      float x = margin;
      gdiPage.DrawString(stringToPrint, printFont, Brushes.Black, x, linePos);
    }

    private void PrintLine(Graphics gdiPage, float yPos)
    {
      if (!this._linesOn)
        return;
      string s = "".PadRight(40, '_');
      gdiPage.DrawString(s, new Font("Ariel", 12f), Brushes.Black, 0.0f, yPos);
    }

    public void DrawReceiptToGraphic(
      Graphics gdiPage,
      Font printFont,
      float rightMargin,
      float lineHeight,
      float middleOfPage,
      float middleOfPageLogoSection,
      int pageWidth)
    {
      int num1 = 0;
      float margin = 8f;
      string appSetting1 = ConfigurationManager.AppSettings["receiptLogoImageLeft"];
      string appSetting2 = ConfigurationManager.AppSettings["receiptLogoImageRight"];
      if (File.Exists(appSetting1))
      {
        Image image = (Image) new Bitmap(appSetting1);
        gdiPage.DrawImage(image, 10, 0, 49, 72);
      }
      if (File.Exists(appSetting2))
      {
        Image image = (Image) new Bitmap(appSetting2);
        int x = pageWidth - image.Width - 20;
        gdiPage.DrawImage(image, x, 0, 49, 72);
      }
      float num2 = middleOfPageLogoSection - gdiPage.MeasureString("THANK YOU FOR", printFont).Width / 2f;
      num2 = middleOfPageLogoSection - gdiPage.MeasureString("PARKING AT", printFont).Width / 2f;
      int num3 = num1 + 1 + 1;
      string vehicleDataCaptureSite = Settings.Default.VehicleDataCaptureSite;
      Font printFont1 = printFont;
      double num4 = (double) middleOfPage;
      Graphics gdiPage1 = gdiPage;
      int num5 = num3;
      int num6 = num5 + 1;
      double num7 = (double) num5 * (double) lineHeight;
      this.PrintStringInMiddle(vehicleDataCaptureSite, printFont1, (float) num4, gdiPage1, (float) num7);
      int num8 = num6 + 1 + 1;
      int num9 = num8 + 1;
      float yPos1 = (float) num8 * lineHeight;
      this.PrintLine(gdiPage, yPos1);
      this.PrintLine(gdiPage, yPos1 + 2f);
      int num10 = num9 + 1;
      Font printFont2 = printFont;
      double num11 = (double) middleOfPage;
      Graphics gdiPage2 = gdiPage;
      int num12 = num10;
      int num13 = num12 + 1;
      double num14 = (double) num12 * (double) lineHeight;
      this.PrintStringInMiddle("Parking Details", printFont2, (float) num11, gdiPage2, (float) num14);
      int num15 = num13 + 1;
      string stringToPrint1 = string.Format("VRM: {0}", (object) this.m_ParkingPermit.VehicleRegMark);
      Font printFont3 = printFont;
      double num16 = (double) margin;
      Graphics gdiPage3 = gdiPage;
      int num17 = num15;
      int num18 = num17 + 1;
      double num19 = (double) num17 * (double) lineHeight;
      this.PrintStringOnLeft(stringToPrint1, printFont3, (float) num16, gdiPage3, (float) num19);
      string stringToPrint2 = string.Format("VALID FROM: {0}", (object) this.m_ParkingPermit.StartDate.ToString("dd/MM/yyyy HH:mm:ss"));
      Font printFont4 = printFont;
      double num20 = (double) margin;
      Graphics gdiPage4 = gdiPage;
      int num21 = num18;
      int num22 = num21 + 1;
      double num23 = (double) num21 * (double) lineHeight;
      this.PrintStringOnLeft(stringToPrint2, printFont4, (float) num20, gdiPage4, (float) num23);
      string stringToPrint3 = string.Format("VALID TO: {0}", (object) this.m_ParkingPermit.EndDate.ToString("dd/MM/yyyy HH:mm:ss"));
      Font printFont5 = printFont;
      double num24 = (double) margin;
      Graphics gdiPage5 = gdiPage;
      int num25 = num22;
      int num26 = num25 + 1;
      double num27 = (double) num25 * (double) lineHeight;
      this.PrintStringOnLeft(stringToPrint3, printFont5, (float) num24, gdiPage5, (float) num27);
      TimeSpan timeSpan = this.m_ParkingPermit.EndDate.Subtract(this.m_ParkingPermit.StartDate);
      string str1 = "";
      if (timeSpan.Days > 0)
        str1 = timeSpan.Days != 1 ? timeSpan.Days.ToString() + " DAYS, " : "1 DAY, ";
      if (timeSpan.Hours > 0 || timeSpan.Days > 0 && timeSpan.Minutes > 0)
        str1 = timeSpan.Hours != 1 ? str1 + (object) timeSpan.Hours + " HOURS, " : str1 + "1 HOUR, ";
      string stringToPrint4 = string.Format("DURATION: {0}", timeSpan.Minutes <= 0 ? (object) str1.Substring(0, str1.Length - 2) : (timeSpan.Minutes != 1 ? (object) (str1 + (object) timeSpan.Minutes + " MINUTES") : (object) (str1 + "1 MINUTE")));
      Font printFont6 = printFont;
      double num28 = (double) margin;
      Graphics gdiPage6 = gdiPage;
      int num29 = num26;
      int num30 = num29 + 1;
      double num31 = (double) num29 * (double) lineHeight;
      this.PrintStringOnLeft(stringToPrint4, printFont6, (float) num28, gdiPage6, (float) num31);
      int num32 = num30;
      int num33 = num32 + 1;
      float yPos2 = (float) num32 * lineHeight;
      this.PrintLine(gdiPage, yPos2);
      int num34 = num33 + 1;
      string stringToPrint5 = "AMOUNT: " + this.m_ParkingPermit.Paid.ToString("C") + " inc VAT";
      int num35 = num34;
      int num36 = num35 + 1;
      float linePos1 = (float) num35 * lineHeight;
      this.PrintStringOnLeft(stringToPrint5, printFont, margin, gdiPage, linePos1);
      string stringToPrint6 = string.Format("TRANS NO:{0}", (object) this.m_receiptID);
      int num37 = num36;
      int num38 = num37 + 1;
      float linePos2 = (float) num37 * lineHeight;
      this.PrintStringOnLeft(stringToPrint6, printFont, margin, gdiPage, linePos2);
      float linePos3 = (float) num38 * lineHeight;
      int num39 = num38 + 1;
      this.PrintStringOnLeft(string.Format("TRANS TIME: {0}", (object) DateTime.Now.ToString("dd-MM-yyyy HH:mm")), printFont, margin, gdiPage, linePos3);
      float yPos3 = (float) num39 * lineHeight;
      this.PrintLine(gdiPage, yPos3);
      if (this.m_ParkingPermit.PaymentType.ToUpper() == "CCARD")
      {
        int num40 = num39 + 1 + 1;
        float num41 = 0.0f;
        string str2 = (string) this.m_responseFile.MerchantID + "     " + (string) this.m_responseFile.TerminalID;
        Graphics graphics1 = gdiPage;
        string s1 = str2;
        Font font1 = printFont;
        Brush black1 = Brushes.Black;
        double num42 = (double) num41;
        int num43 = num40;
        int num44 = num43 + 1;
        double num45 = (double) num43 * (double) lineHeight;
        graphics1.DrawString(s1, font1, black1, (float) num42, (float) num45);
        string aid = (string) this.m_responseFile.AID;
        Graphics graphics2 = gdiPage;
        string s2 = aid;
        Font font2 = printFont;
        Brush black2 = Brushes.Black;
        double num46 = (double) num41;
        int num47 = num44;
        int num48 = num47 + 1;
        double num49 = (double) num47 * (double) lineHeight;
        graphics2.DrawString(s2, font2, black2, (float) num46, (float) num49);
        string applicationName = (string) this.m_responseFile.ApplicationName;
        Graphics graphics3 = gdiPage;
        string s3 = applicationName;
        Font font3 = printFont;
        Brush black3 = Brushes.Black;
        double num50 = (double) num41;
        int num51 = num48;
        int num52 = num51 + 1;
        double num53 = (double) num51 * (double) lineHeight;
        graphics3.DrawString(s3, font3, black3, (float) num50, (float) num53);
        string str3 = "xxxx xxxx xxxx " + ((string) this.m_responseFile.PANMASKED).Substring(12, 4);
        Graphics graphics4 = gdiPage;
        string s4 = str3;
        Font font4 = printFont;
        Brush black4 = Brushes.Black;
        double num54 = (double) num41;
        int num55 = num52;
        int num56 = num55 + 1;
        double num57 = (double) num55 * (double) lineHeight;
        graphics4.DrawString(s4, font4, black4, (float) num54, (float) num57);
        string str4 = "Pan Seq No " + (string) this.m_responseFile.SeqNo;
        Graphics graphics5 = gdiPage;
        string s5 = str4;
        Font font5 = printFont;
        Brush black5 = Brushes.Black;
        double num58 = (double) num41;
        int num59 = num56;
        int num60 = num59 + 1;
        double num61 = (double) num59 * (double) lineHeight;
        graphics5.DrawString(s5, font5, black5, (float) num58, (float) num61);
        string str5 = "Expiry    " + ((string) this.m_responseFile.ExpiryDate).Substring(2, 2) + "/" + ((string) this.m_responseFile.ExpiryDate).Substring(0, 2);
        Graphics graphics6 = gdiPage;
        string s6 = str5;
        Font font6 = printFont;
        Brush black6 = Brushes.Black;
        double num62 = (double) num41;
        int num63 = num60;
        int num64 = num63 + 1;
        double num65 = (double) num63 * (double) lineHeight;
        graphics6.DrawString(s6, font6, black6, (float) num62, (float) num65);
        int num66 = num64 + 1;
        string str6 = "Verified by PIN";
        Graphics graphics7 = gdiPage;
        string s7 = str6;
        Font font7 = printFont;
        Brush black7 = Brushes.Black;
        double num67 = (double) num41;
        int num68 = num66;
        int num69 = num68 + 1;
        double num70 = (double) num68 * (double) lineHeight;
        graphics7.DrawString(s7, font7, black7, (float) num67, (float) num70);
        string str7 = "Please Debit MY account";
        Graphics graphics8 = gdiPage;
        string s8 = str7;
        Font font8 = printFont;
        Brush black8 = Brushes.Black;
        double num71 = (double) num41;
        int num72 = num69;
        int num73 = num72 + 1;
        double num74 = (double) num72 * (double) lineHeight;
        graphics8.DrawString(s8, font8, black8, (float) num71, (float) num74);
        string str8 = "Thank you";
        Graphics graphics9 = gdiPage;
        string s9 = str8;
        Font font9 = printFont;
        Brush black9 = Brushes.Black;
        double num75 = (double) num41;
        int num76 = num73;
        int num77 = num76 + 1;
        double num78 = (double) num76 * (double) lineHeight;
        graphics9.DrawString(s9, font9, black9, (float) num75, (float) num78);
        string str9 = "Please keep this receipt";
        Graphics graphics10 = gdiPage;
        string s10 = str9;
        Font font10 = printFont;
        Brush black10 = Brushes.Black;
        double num79 = (double) num41;
        int num80 = num77;
        int num81 = num80 + 1;
        double num82 = (double) num80 * (double) lineHeight;
        graphics10.DrawString(s10, font10, black10, (float) num79, (float) num82);
        string str10 = "for your records";
        Graphics graphics11 = gdiPage;
        string s11 = str10;
        Font font11 = printFont;
        Brush black11 = Brushes.Black;
        double num83 = (double) num41;
        int num84 = num81;
        int num85 = num84 + 1;
        double num86 = (double) num84 * (double) lineHeight;
        graphics11.DrawString(s11, font11, black11, (float) num83, (float) num86);
        string str11 = "AUTH CODE:     " + (string) this.m_responseFile.AuthCode;
        Graphics graphics12 = gdiPage;
        string s12 = str11;
        Font font12 = printFont;
        Brush black12 = Brushes.Black;
        double num87 = (double) num41;
        int num88 = num85;
        int num89 = num88 + 1;
        double num90 = (double) num88 * (double) lineHeight;
        graphics12.DrawString(s12, font12, black12, (float) num87, (float) num90);
        string str12 = (string) this.m_responseFile.ATC + "     " + (string) this.m_responseFile.CID;
        Graphics graphics13 = gdiPage;
        string s13 = str12;
        Font font13 = printFont;
        Brush black13 = Brushes.Black;
        double num91 = (double) num41;
        int num92 = num89;
        int num93 = num92 + 1;
        double num94 = (double) num92 * (double) lineHeight;
        graphics13.DrawString(s13, font13, black13, (float) num91, (float) num94);
        int num95 = num93;
        num39 = num95 + 1;
        float yPos4 = (float) num95 * lineHeight;
        this.PrintLine(gdiPage, yPos4);
      }
      int num96 = num39 + 1 + 1;
      string text1 = "For more information";
      float num97 = middleOfPage - gdiPage.MeasureString(text1, printFont).Width / 2f;
      Graphics graphics14 = gdiPage;
      string s14 = text1;
      Font font14 = printFont;
      Brush black14 = Brushes.Black;
      double num98 = (double) num97;
      int num99 = num96;
      int num100 = num99 + 1;
      double num101 = (double) num99 * (double) lineHeight;
      graphics14.DrawString(s14, font14, black14, (float) num98, (float) num101);
      string text2 = "please contact";
      float num102 = middleOfPage - gdiPage.MeasureString(text2, printFont).Width / 2f;
      Graphics graphics15 = gdiPage;
      string s15 = text2;
      Font font15 = printFont;
      Brush black15 = Brushes.Black;
      double num103 = (double) num102;
      int num104 = num100;
      int num105 = num104 + 1;
      double num106 = (double) num104 * (double) lineHeight;
      graphics15.DrawString(s15, font15, black15, (float) num103, (float) num106);
      string phoneNumber = this.m_PhoneNumber;
      float num107 = middleOfPage - gdiPage.MeasureString(phoneNumber, printFont).Width / 2f;
      Graphics graphics16 = gdiPage;
      string s16 = phoneNumber;
      Font font16 = printFont;
      Brush black16 = Brushes.Black;
      double num108 = (double) num107;
      int num109 = num105;
      int num110 = num109 + 1;
      double num111 = (double) num109 * (double) lineHeight;
      graphics16.DrawString(s16, font16, black16, (float) num108, (float) num111);
      int num112 = num110;
      int num113 = num112 + 1;
      float yPos5 = (float) num112 * lineHeight;
      this.PrintLine(gdiPage, yPos5);
    }

    private void printDocumentReceipt_PrintPage(object sender, PrintPageEventArgs e)
    {
      Utilites.WriteToLog("printDocumentReceipt_PrintPage entered");
      Graphics graphics = e.Graphics;
      Font font = this.printLabel.Font;
      float right = (float) e.MarginBounds.Right;
      float lineHeight = font.GetHeight(graphics) + 2f;
      float middleOfPage = (float) (e.PageBounds.Width / 2);
      float middleOfPageLogoSection = (float) ((e.PageBounds.Width - 49) / 2 + 49);
      int width = e.PageBounds.Width;
      this.DrawReceiptToGraphic(graphics, font, right, lineHeight, middleOfPage, middleOfPageLogoSection, width);
      Utilites.WriteToLog("printDocumentReceipt_PrintPage exited");
    }

    private void cmdClose_Click(object sender, EventArgs e)
    {
      Utilites.WriteToLog("cmdClose_Click entered");
      this.Close();
      Utilites.WriteToLog("cmdClose_Click exited");
    }

    private void cmdPrint_Click(object sender, EventArgs e)
    {
      Utilites.WriteToLog("cmdPrint_Click entered");
      this.PrintReceipt();
      Utilites.WriteToLog("cmdPrint_Click exited");
    }

    private void PrintReceipt()
    {
      string defaultPrinter = PrinterSettingsExtension.TryGetDefaultPrinter(new PrinterSettings());
      if (!string.IsNullOrEmpty(defaultPrinter))
      {
        try
        {
          this.cmdPrint.BackgroundImage = (Image) Resources.BlackKey;
          this.cmdPrint.Enabled = false;
          this.printDocumentReceipt.PrinterSettings.PrinterName = defaultPrinter;
          this.printDocumentReceipt.PrintController = (PrintController) new StandardPrintController();
          this.printDocumentReceipt.Print();
        }
        catch (Exception ex)
        {
          Log.Write(ex);
        }
      }
      else
      {
        LogData logData = new LogData();
        logData.Categories.Clear();
        logData.Categories.Add("Exception");
        logData.Title = "Default printer not found.";
        logData.Message = "Receipt could not be printed as a default printer was not found.";
        Log.Write(logData);
      }
    }
  }
}
