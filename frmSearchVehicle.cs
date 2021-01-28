// Decompiled with JetBrains decompiler
// Type: TouchPark.frmSearchVehicle
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TouchPark.Controls;
using TouchPark.Properties;
using TouchPark.Reporting;
using TouchPark.Services;

namespace TouchPark
{
  public class frmSearchVehicle : Form
  {
    private string m_ApplicationType = Settings.Default.ApplicationType;
    private List<PictureBox> m_allImgOverviewSearch;
    private List<PictureBox> m_allImgVRMSearch;
    private List<CVehicleInfo> m_allVehicleInfo;
    private CCacheData m_clsVehicleDataXml;
    private int m_NumberOfKeys;
    private ParkingPermitInfo m_parkingPermit;
    private IContainer components;
    private Label lblInstructionHeader;
    private PictureBox imgLogo;
    private Panel pnlDisplayVehicle;
    private PictureBox imgDisplayVehicleBackground;
    private PictureBox imgDisplayVehicleBorder;
    private Panel pnlRegistrationDisplay;
    private Button cmdCancel;
    private Button cmdEnter;
    private Panel pnlKeyboard;
    private Button cmdKeyA;
    private Button cmdKeyB;
    private Button cmdKeyG;
    private Button cmdKeyDel;
    private Button cmdKeyF;
    private Button cmdKeyE;
    private Button cmdKeyD;
    private Button cmdKeyC;
    private Button cmdKey4;
    private Button cmdKey2;
    private Button cmdKey1;
    private Button cmdKeyZ;
    private Button cmdKeyS;
    private Button cmdKeyY;
    private Button cmdKeyX;
    private Button cmdKeyQ;
    private Button cmdKeyV;
    private Button cmdKeyU;
    private Button cmdKeyT;
    private Button cmdKeyO;
    private Button cmdKeyN;
    private Button cmdKeyL;
    private Button cmdKeyK;
    private Button cmdKeyJ;
    private Button cmdKeyI;
    private Button cmdKeyH;
    private Button cmdKeyM;
    private Button cmdKey0;
    private Button cmdKey9;
    private Button cmdKey8;
    private Button cmdKey6;
    private Button cmdKey7;
    private Button cmdKey5;
    private Button cmdKeyP;
    private Button cmdKeyW;
    private Button cmdKeyR;
    private Button cmdKey3;
    private PictureBox imgSearchOverview1;
    private PictureBox imgSearchVRM1;
    private PictureBox imgSearchOverview2;
    private PictureBox imgSearchVRM2;
    private PictureBox imgSearchOverview4;
    private PictureBox imgSearchOverview3;
    private PictureBox imgSearchVRM4;
    private PictureBox imgSearchVRM3;
    private Label lblRegistrationInstruction;
    private Label lblSearchInstruction;
    private Timer DisplayTimer;
    private Label lblInstruactionPopUp;
    private Timer tmrShowTip;
    private Button cmdVehicleNotPresent;
    private RegistrationPlate registrationPlate1;

    [DllImport("Gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse);

    public frmSearchVehicle(ParkingPermitInfo parking_permit)
    {
      if (parking_permit != null)
      {
        this.m_parkingPermit = new ParkingPermitInfo();
        this.m_parkingPermit.copy(parking_permit);
      }
      this.InitializeComponent();
      this.m_allImgOverviewSearch = new List<PictureBox>();
      this.m_allImgOverviewSearch.Add(this.imgSearchOverview1);
      this.m_allImgOverviewSearch.Add(this.imgSearchOverview2);
      this.m_allImgOverviewSearch.Add(this.imgSearchOverview3);
      this.m_allImgOverviewSearch.Add(this.imgSearchOverview4);
      this.m_allImgVRMSearch = new List<PictureBox>();
      this.m_allImgVRMSearch.Add(this.imgSearchVRM1);
      this.m_allImgVRMSearch.Add(this.imgSearchVRM2);
      this.m_allImgVRMSearch.Add(this.imgSearchVRM3);
      this.m_allImgVRMSearch.Add(this.imgSearchVRM4);
      this.m_allVehicleInfo = new List<CVehicleInfo>();
      this.m_clsVehicleDataXml = new CCacheData();
    }

    private void TryToSetBackgroundImage()
    {
      try
      {
        if (string.IsNullOrEmpty(Settings.Default.BackgroundImagePath))
          return;
        string empty = string.Empty;
        string str = !Settings.Default.BackgroundImagePath.Contains("\\") ? string.Format("{0}\\{1}", (object) Environment.CurrentDirectory, (object) Settings.Default.BackgroundImagePath) : Settings.Default.BackgroundImagePath;
        if (!File.Exists(str))
          return;
        this.BackgroundImage = (Image) new Bitmap(str);
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    private void frmSearchVehicle_Load(object sender, EventArgs e)
    {
      this.lblInstructionHeader.Text = "";
      this.registrationPlate1.VehicleRegistrationMark = "";
      this.lblRegistrationInstruction.Text = "PLEASE ENTER YOUR VEHICLE REGISTRATION USING THE TOUCHSCREEN.";
      this.m_NumberOfKeys = 0;
      this.TryToSetBackgroundImage();
      if (!(this.m_ApplicationType == "ServiceStationPark") && !(this.m_ApplicationType == "PermitPark") && !(this.m_ApplicationType == "PermitPark2"))
        return;
      this.cmdCancel.Visible = false;
      if (!(this.m_ApplicationType == "PermitPark"))
        return;
      this.cmdEnter.Visible = true;
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      this.ClearImageDisplay();
      this.Hide();
    }

    private void ShowEnteredKeys(object sender, EventArgs e)
    {
      this.DisplayTimer.Stop();
      Button button = (Button) sender;
      if (button.Text == "DEL")
      {
        if (this.m_NumberOfKeys > 0)
        {
          --this.m_NumberOfKeys;
          this.registrationPlate1.VehicleRegistrationMark = this.registrationPlate1.VehicleRegistrationMark.Substring(0, this.m_NumberOfKeys);
          //this.SearchForCars(this.registrationPlate1.VehicleRegistrationMark);
        }
      }
      else if (this.m_NumberOfKeys <= 10)
      {
        ++this.m_NumberOfKeys;
        this.registrationPlate1.VehicleRegistrationMark += button.Text;
        //this.SearchForCars(this.registrationPlate1.VehicleRegistrationMark);
      }
      if (this.registrationPlate1.VehicleRegistrationMark != "")
      {
        this.lblSearchInstruction.Visible = true;
        this.lblRegistrationInstruction.Visible = false;
        if (!(this.m_ApplicationType == "PermitPark"))
          this.cmdVehicleNotPresent.Visible = true;
      }
      else
      {
        this.lblSearchInstruction.Visible = false;
        this.lblRegistrationInstruction.Visible = true;
        if (!(this.m_ApplicationType == "PermitPark"))
          this.cmdVehicleNotPresent.Visible = false;
      }
      this.DisplayTimer.Start();
    }

    private void PrintAuditReport()
    {
      string defaultPrinter = PrinterSettingsExtension.TryGetDefaultPrinter(new PrinterSettings());
      if (!string.IsNullOrEmpty(defaultPrinter))
      {
        try
        {
          ParkingAuditReport parkingAuditReport = new ParkingAuditReport();
          parkingAuditReport.VehicleDataCaptureSite = Settings.Default.VehicleDataCaptureSite;
          parkingAuditReport.Render();
          ((PrintDocument) parkingAuditReport).PrinterSettings.PrinterName = defaultPrinter;
          ((PrintDocument) parkingAuditReport).Print();
          ParkingAuditReport.LastAudit = DateTime.Now;
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
        logData.Message = "Audit report could not be printed as a default printer was not found.";
        Log.Write(logData);
      }
    }

    private void cmdEnter_Click(object sender, EventArgs e)
    {
      this.DisplayTimer.Stop();
      bool flag = false;
      try
      {
        if (!(this.registrationPlate1.VehicleRegistrationMark != ""))
          return;
        if (this.registrationPlate1.VehicleRegistrationMark == Settings.Default.AuditReportCode.ToUpper())
        {
          this.PrintAuditReport();
          this.registrationPlate1.VehicleRegistrationMark = string.Empty;
          this.cmdVehicleNotPresent.Visible = false;
          this.DisplayTimer.Start();
        }
        else
        {
          this.SearchForCars(this.registrationPlate1.VehicleRegistrationMark);
          if (this.m_allVehicleInfo.Count == 0)
          {
            if (this.m_parkingPermit == null)
              this.m_parkingPermit = new ParkingPermitInfo();
            this.m_parkingPermit.VehicleRegMark = this.registrationPlate1.VehicleRegistrationMark;
            this.m_parkingPermit.StartDate = DateTime.Now;
            int num = (int) new frmConfirmVRM((string) null, (string) null, this.m_parkingPermit).ShowDialog();
          }
          else
          {
            int index = 0;
            foreach (CVehicleInfo cvehicleInfo in this.m_allVehicleInfo)
            {
              if (cvehicleInfo.VRM == this.registrationPlate1.VehicleRegistrationMark)
              {
                this.m_allImgOverviewSearch[index].ImageLocation = cvehicleInfo.overviewImageLocation;
                this.m_allImgVRMSearch[index].ImageLocation = cvehicleInfo.plateImageLocation;
                if (this.m_parkingPermit == null)
                  this.m_parkingPermit = new ParkingPermitInfo();
                this.m_parkingPermit.VehicleRegMark = cvehicleInfo.VRM;
                this.m_parkingPermit.StartDate = DateTime.Parse(cvehicleInfo.inwardTime);
                int num = (int) new frmConfirmVehicle(this.m_allImgOverviewSearch[index].ImageLocation, this.m_allImgVRMSearch[index].ImageLocation, this.m_parkingPermit)
                {
                  VehicleInfo = cvehicleInfo
                }.ShowDialog();
                flag = true;
              }
            }
            if (!flag)
            {
              if (this.m_parkingPermit == null)
                this.m_parkingPermit = new ParkingPermitInfo();
              this.m_parkingPermit.VehicleRegMark = this.registrationPlate1.VehicleRegistrationMark;
              this.m_parkingPermit.StartDate = DateTime.Now;
              int num = (int) new frmConfirmVRM((string) null, (string) null, this.m_parkingPermit).ShowDialog();
            }
          }
          if (!(this.m_ApplicationType == "ServiceStationPark"))
          {
            this.Hide();
          }
          else
          {
            this.registrationPlate1.VehicleRegistrationMark = "";
            this.m_NumberOfKeys = 0;
            this.ClearImageDisplay();
            this.DisplayTimer.Start();
          }
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    private void SearchForCars(string searchVRM)
    {
      CGetVehicleData cgetVehicleData = new CGetVehicleData();
      List<CVehicleInfo> cvehicleInfoList = new List<CVehicleInfo>();
    //this.m_allVehicleInfo = this.m_clsVehicleDataXml.GetMatchingVRMFromXML(searchVRM);
      this.m_allVehicleInfo = this.m_clsVehicleDataXml.GetMatchingVRMFromService(searchVRM);
      if (this.m_allVehicleInfo.Count > 0 && !searchVRM.Equals(""))
      {
        this.UpdateImages();
        this.tmrShowTip.Stop();
        this.lblSearchInstruction.Text = "If you can see your vehicle on the screen, please touch the picture.";
        if (this.m_ApplicationType == "PermitPark")
          this.lblInstruactionPopUp.Show();
        this.tmrShowTip.Start();
      }
      else
      {
        this.lblSearchInstruction.Text = "If your vehicle does not appear please type in your full registration below and click the enter button.";
        this.ClearImageDisplay();
        if (!(this.m_ApplicationType == "PermitPark"))
          return;
        this.lblInstruactionPopUp.Hide();
      }
    }

        //private void UpdateImages()
        //{
        //  Console.WriteLine();
        //  Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
        //  Console.WriteLine("Updating image search images...");
        //  Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
        //  this.SuspendLayout();
        //  this.lblRegistrationInstruction.Visible = false;
        //  for (int index = 0; index < 4; ++index)
        //  {
        //    this.m_allImgOverviewSearch[index].Visible = false;
        //    this.m_allImgVRMSearch[index].Visible = false;
        //  }
        //  int index1 = 0;
        //  try
        //  {
        //    string str = string.Format("{0}\\", (object) Path.GetFullPath(ConfigurationManager.AppSettings["cacheImageLocation"]));
        //    Console.WriteLine("Cache image path : {0}", (object) str);
        //    foreach (CVehicleInfo cvehicleInfo in this.m_allVehicleInfo)
        //    {
        //      Console.WriteLine("Found VRM:{0}", (object) cvehicleInfo.VRM);
        //      this.m_allImgOverviewSearch[index1].Visible = false;
        //      this.m_allImgVRMSearch[index1].Visible = false;
        //      if (!cvehicleInfo.overviewImageLocation.Contains("\\"))
        //        cvehicleInfo.overviewImageLocation = string.Format("{0}{1}", (object) str, (object) cvehicleInfo.overviewImageLocation);
        //      this.m_allImgOverviewSearch[index1].ImageLocation = cvehicleInfo.overviewImageLocation;
        //      Console.WriteLine("Overview image set to : {0}", (object) cvehicleInfo.overviewImageLocation);
        //      if (!cvehicleInfo.plateImageLocation.Contains("\\"))
        //        cvehicleInfo.plateImageLocation = string.Format("{0}{1}", (object) str, (object) cvehicleInfo.plateImageLocation);
        //      this.m_allImgVRMSearch[index1].ImageLocation = cvehicleInfo.plateImageLocation;
        //      Console.WriteLine("Plate image set to : {0}", (object) cvehicleInfo.plateImageLocation);
        //      this.m_allImgOverviewSearch[index1].Tag = (object) cvehicleInfo;
        //      this.m_allImgVRMSearch[index1].Tag = (object) cvehicleInfo;
        //      this.m_allImgOverviewSearch[index1].Refresh();
        //      this.m_allImgVRMSearch[index1].Refresh();
        //      this.m_allImgOverviewSearch[index1].Visible = true;
        //      this.m_allImgVRMSearch[index1].Visible = true;
        //      ++index1;
        //      if (index1 >= 4)
        //        break;
        //    }
        //  }
        //  catch (Exception ex)
        //  {
        //    this.m_allImgOverviewSearch[index1].Visible = false;
        //    this.m_allImgVRMSearch[index1].Visible = false;
        //    Log.Write(ex);
        //  }
        //  this.PositionSearchLabel();
        //  this.ResumeLayout();
        //  this.Invalidate();
        //  this.Refresh();
        //  Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
        //  Console.WriteLine("Search images updated!");
        //  Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
        //}

        private void UpdateImages()
        {
            Console.WriteLine();
            Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
            Console.WriteLine("Updating image search images...");
            Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
            this.SuspendLayout();
            this.lblRegistrationInstruction.Visible = false;
            for (int index = 0; index < 4; ++index)
            {
                this.m_allImgOverviewSearch[index].Visible = false;
                this.m_allImgVRMSearch[index].Visible = false;
            }
            int index1 = 0;
            try
            {
                string str = string.Format("{0}\\", (object)Path.GetFullPath(ConfigurationManager.AppSettings["cacheImageLocation"]));
                Console.WriteLine("Cache image path : {0}", (object)str);
                foreach (CVehicleInfo cvehicleInfo in this.m_allVehicleInfo)
                {
                    Console.WriteLine("Found VRM:{0}", (object)cvehicleInfo.VRM);
                    this.m_allImgOverviewSearch[index1].Visible = false;
                    this.m_allImgVRMSearch[index1].Visible = false;
                    //if (!cvehicleInfo.overviewImageLocation.Contains("\\"))
                    //  cvehicleInfo.overviewImageLocation = string.Format("{0}{1}", (object) str, (object) cvehicleInfo.overviewImageLocation);
                    this.m_allImgOverviewSearch[index1].Image = cvehicleInfo.overviewImageLocation.ToImage();
                    Console.WriteLine("Overview image set to : {0}", (object)cvehicleInfo.overviewImageLocation);
                    //if (!cvehicleInfo.plateImageLocation.Contains("\\"))
                    //  cvehicleInfo.plateImageLocation = string.Format("{0}{1}", (object) str, (object) cvehicleInfo.plateImageLocation);
                    this.m_allImgVRMSearch[index1].Image = cvehicleInfo.plateImageLocation.ToImage();
                    Console.WriteLine("Plate image set to : {0}", (object)cvehicleInfo.plateImageLocation);
                    this.m_allImgOverviewSearch[index1].Tag = (object)cvehicleInfo;
                    this.m_allImgVRMSearch[index1].Tag = (object)cvehicleInfo;
                    this.m_allImgOverviewSearch[index1].Refresh();
                    this.m_allImgVRMSearch[index1].Refresh();
                    this.m_allImgOverviewSearch[index1].Visible = true;
                    this.m_allImgVRMSearch[index1].Visible = true;
                    ++index1;
                    if (index1 >= 4)
                        break;
                }
            }
            catch (Exception ex)
            {
                this.m_allImgOverviewSearch[index1].Visible = false;
                this.m_allImgVRMSearch[index1].Visible = false;
                Log.Write(ex);
            }
            this.PositionSearchLabel();
            this.ResumeLayout();
            this.Invalidate();
            this.Refresh();
            Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
            Console.WriteLine("Search images updated!");
            Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
        }

        private void ClearImageDisplay()
    {
      for (int index = 0; index < 4; ++index)
      {
        this.m_allImgOverviewSearch[index].Visible = false;
        this.m_allImgVRMSearch[index].Visible = false;
      }
      this.PositionSearchLabel();
      this.lblRegistrationInstruction.Visible = true;
    }

    private void PositionSearchLabel()
    {
      if (this.m_allVehicleInfo.Count > 0 && !string.IsNullOrEmpty(this.registrationPlate1.VehicleRegistrationMark))
      {
        int num = this.m_allVehicleInfo.Count <= 4 ? 130 * this.m_allVehicleInfo.Count : 520;
        this.lblSearchInstruction.Width = this.imgDisplayVehicleBackground.Width - num - 40;
        this.lblSearchInstruction.Left = this.imgDisplayVehicleBackground.Left + num + 20;
      }
      else
      {
        this.lblSearchInstruction.Width = this.imgDisplayVehicleBackground.Width - 10;
        this.lblSearchInstruction.Left = this.imgDisplayVehicleBackground.Left + 5;
      }
    }

    private void imgSearchOverview_Click(object sender, EventArgs e)
    {
      this.DisplayTimer.Stop();
      CVehicleInfo tag = (CVehicleInfo) ((Control) sender).Tag;
      if (this.m_parkingPermit == null)
        this.m_parkingPermit = new ParkingPermitInfo();
      this.m_parkingPermit.VehicleRegMark = tag.VRM;
      this.m_parkingPermit.StartDate = DateTime.Parse(tag.inwardTime);
      int num = (int) new frmConfirmVehicle(tag.overviewImageLocation, tag.plateImageLocation, this.m_parkingPermit)
      {
        VehicleInfo = tag
      }.ShowDialog();
      if (!(this.m_ApplicationType == "ServiceStationPark"))
      {
        this.registrationPlate1.VehicleRegistrationMark = "";
        this.m_NumberOfKeys = 0;
        this.ClearImageDisplay();
        this.DisplayTimer.Start();
        this.Hide();
      }
      else
      {
        this.registrationPlate1.VehicleRegistrationMark = "";
        this.m_NumberOfKeys = 0;
        this.ClearImageDisplay();
        this.DisplayTimer.Start();
      }
    }

    private void DisplayTimer_Tick(object sender, EventArgs e)
    {
      this.registrationPlate1.VehicleRegistrationMark = "";
      this.m_NumberOfKeys = 0;
      this.ClearImageDisplay();
      this.Hide();
    }

    private void tmrShowTip_Tick(object sender, EventArgs e)
    {
      this.tmrShowTip.Stop();
    }

    private void cmdVehicleNotPresent_Click(object sender, EventArgs e)
    {
      this.DisplayTimer.Stop();
      bool flag = false;
      try
      {
        if (!(this.registrationPlate1.VehicleRegistrationMark != ""))
          return;
        if (this.registrationPlate1.VehicleRegistrationMark == Settings.Default.AuditReportCode.ToUpper())
        {
          this.PrintAuditReport();
          this.registrationPlate1.VehicleRegistrationMark = string.Empty;
          this.cmdVehicleNotPresent.Visible = false;
          this.DisplayTimer.Start();
        }
        else
        {
          this.SearchForCars(this.registrationPlate1.VehicleRegistrationMark);
          if (this.m_allVehicleInfo.Count == 0)
          {
            if (this.m_parkingPermit == null)
              this.m_parkingPermit = new ParkingPermitInfo();
            this.m_parkingPermit.VehicleRegMark = this.registrationPlate1.VehicleRegistrationMark;
            this.m_parkingPermit.StartDate = DateTime.Now;
            int num = (int) new frmConfirmVRM((string) null, (string) null, this.m_parkingPermit).ShowDialog();
          }
          else
          {
            int index = 0;
                        foreach (CVehicleInfo cvehicleInfo in this.m_allVehicleInfo)
                        {
                            if (cvehicleInfo.VRM == this.registrationPlate1.VehicleRegistrationMark)
                            {
                                this.m_allImgOverviewSearch[index].Image = cvehicleInfo.overviewImageLocation.ToImage();
                                this.m_allImgVRMSearch[index].Image = cvehicleInfo.plateImageLocation.ToImage();
                                if (this.m_parkingPermit == null)
                                    this.m_parkingPermit = new ParkingPermitInfo();
                                this.m_parkingPermit.VehicleRegMark = cvehicleInfo.VRM;
                                this.m_parkingPermit.StartDate = DateTime.Parse(cvehicleInfo.inwardTime);
                                //int num = (int)new frmConfirmVehicle(this.m_allImgOverviewSearch[index].ImageLocation, this.m_allImgVRMSearch[index].ImageLocation, this.m_parkingPermit)
                                //{
                                //    VehicleInfo = cvehicleInfo
                                //}.ShowDialog();
                                flag = true;
                            }
                        }
                        //if (!flag)
                        //{
                        //  this.m_parkingPermit = new ParkingPermitInfo();
                        //  this.m_parkingPermit.VehicleRegMark = this.registrationPlate1.VehicleRegistrationMark;
                        //  this.m_parkingPermit.StartDate = DateTime.Now;
                        //  frmConfirmVRM frmConfirmVrm = new frmConfirmVRM((string) null, (string) null, this.m_parkingPermit);
                        //  int num = (int) frmConfirmVrm.ShowDialog();
                        //  frmConfirmVrm.Close();
                        //  frmConfirmVrm.Dispose();
                        //  GC.Collect();
                        //  GC.WaitForPendingFinalizers();
                        //}
                    }
          if (!(this.m_ApplicationType == "ServiceStationPark"))
          {
            this.Hide();
          }
          else
          {
            this.registrationPlate1.VehicleRegistrationMark = "";
            this.m_NumberOfKeys = 0;
            //this.ClearImageDisplay();
            this.DisplayTimer.Start();
          }
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
        this.Hide();
      }
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmSearchVehicle));
      this.lblInstructionHeader = new Label();
      this.pnlDisplayVehicle = new Panel();
      this.lblSearchInstruction = new Label();
      this.lblRegistrationInstruction = new Label();
      this.imgSearchVRM4 = new PictureBox();
      this.imgSearchVRM3 = new PictureBox();
      this.imgSearchVRM2 = new PictureBox();
      this.imgSearchOverview4 = new PictureBox();
      this.imgSearchOverview3 = new PictureBox();
      this.imgSearchOverview2 = new PictureBox();
      this.imgSearchVRM1 = new PictureBox();
      this.imgSearchOverview1 = new PictureBox();
      this.imgDisplayVehicleBackground = new PictureBox();
      this.imgDisplayVehicleBorder = new PictureBox();
      this.pnlRegistrationDisplay = new Panel();
      this.cmdVehicleNotPresent = new Button();
      this.cmdEnter = new Button();
      this.cmdCancel = new Button();
      this.lblInstruactionPopUp = new Label();
      this.registrationPlate1 = new RegistrationPlate();
      this.pnlKeyboard = new Panel();
      this.cmdKeyA = new Button();
      this.cmdKeyB = new Button();
      this.cmdKeyC = new Button();
      this.cmdKeyD = new Button();
      this.cmdKeyE = new Button();
      this.cmdKeyF = new Button();
      this.cmdKeyDel = new Button();
      this.cmdKeyG = new Button();
      this.cmdKeyM = new Button();
      this.cmdKeyH = new Button();
      this.cmdKeyI = new Button();
      this.cmdKeyJ = new Button();
      this.cmdKeyK = new Button();
      this.cmdKeyL = new Button();
      this.cmdKeyN = new Button();
      this.cmdKeyO = new Button();
      this.cmdKeyT = new Button();
      this.cmdKeyU = new Button();
      this.cmdKeyV = new Button();
      this.cmdKeyP = new Button();
      this.cmdKeyW = new Button();
      this.cmdKeyQ = new Button();
      this.cmdKeyX = new Button();
      this.cmdKeyR = new Button();
      this.cmdKeyY = new Button();
      this.cmdKeyS = new Button();
      this.cmdKeyZ = new Button();
      this.cmdKey1 = new Button();
      this.cmdKey2 = new Button();
      this.cmdKey3 = new Button();
      this.cmdKey4 = new Button();
      this.cmdKey5 = new Button();
      this.cmdKey6 = new Button();
      this.cmdKey7 = new Button();
      this.cmdKey8 = new Button();
      this.cmdKey9 = new Button();
      this.cmdKey0 = new Button();
      this.DisplayTimer = new Timer(this.components);
      this.tmrShowTip = new Timer(this.components);
      this.imgLogo = new PictureBox();
      this.pnlDisplayVehicle.SuspendLayout();
      ((ISupportInitialize) this.imgSearchVRM4).BeginInit();
      ((ISupportInitialize) this.imgSearchVRM3).BeginInit();
      ((ISupportInitialize) this.imgSearchVRM2).BeginInit();
      ((ISupportInitialize) this.imgSearchOverview4).BeginInit();
      ((ISupportInitialize) this.imgSearchOverview3).BeginInit();
      ((ISupportInitialize) this.imgSearchOverview2).BeginInit();
      ((ISupportInitialize) this.imgSearchVRM1).BeginInit();
      ((ISupportInitialize) this.imgSearchOverview1).BeginInit();
      ((ISupportInitialize) this.imgDisplayVehicleBackground).BeginInit();
      ((ISupportInitialize) this.imgDisplayVehicleBorder).BeginInit();
      this.pnlRegistrationDisplay.SuspendLayout();
      this.pnlKeyboard.SuspendLayout();
      ((ISupportInitialize) this.imgLogo).BeginInit();
      this.SuspendLayout();
      this.lblInstructionHeader.BackColor = Color.Transparent;
      this.lblInstructionHeader.Dock = DockStyle.Top;
      this.lblInstructionHeader.Font = new Font("Arial", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblInstructionHeader.ForeColor = Color.Gold;
      this.lblInstructionHeader.Location = new Point(0, 0);
      this.lblInstructionHeader.Name = "lblInstructionHeader";
      this.lblInstructionHeader.Size = new Size(798, 40);
      this.lblInstructionHeader.TabIndex = 0;
      this.lblInstructionHeader.Text = "PLEASE TOUCH YOUR VEHICLE";
      this.lblInstructionHeader.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlDisplayVehicle.BackColor = Color.Transparent;
      this.pnlDisplayVehicle.Controls.Add((Control) this.lblSearchInstruction);
      this.pnlDisplayVehicle.Controls.Add((Control) this.lblRegistrationInstruction);
      this.pnlDisplayVehicle.Controls.Add((Control) this.imgSearchVRM4);
      this.pnlDisplayVehicle.Controls.Add((Control) this.imgSearchVRM3);
      this.pnlDisplayVehicle.Controls.Add((Control) this.imgSearchVRM2);
      this.pnlDisplayVehicle.Controls.Add((Control) this.imgSearchOverview4);
      this.pnlDisplayVehicle.Controls.Add((Control) this.imgSearchOverview3);
      this.pnlDisplayVehicle.Controls.Add((Control) this.imgSearchOverview2);
      this.pnlDisplayVehicle.Controls.Add((Control) this.imgSearchVRM1);
      this.pnlDisplayVehicle.Controls.Add((Control) this.imgSearchOverview1);
      this.pnlDisplayVehicle.Controls.Add((Control) this.imgDisplayVehicleBackground);
      this.pnlDisplayVehicle.Controls.Add((Control) this.imgDisplayVehicleBorder);
      this.pnlDisplayVehicle.Dock = DockStyle.Top;
      this.pnlDisplayVehicle.Location = new Point(0, 40);
      this.pnlDisplayVehicle.Name = "pnlDisplayVehicle";
      this.pnlDisplayVehicle.Size = new Size(798, 159);
      this.pnlDisplayVehicle.TabIndex = 3;
      this.lblSearchInstruction.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.lblSearchInstruction.BackColor = Color.Black;
      this.lblSearchInstruction.Font = new Font("Arial", 14f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblSearchInstruction.ForeColor = Color.White;
      this.lblSearchInstruction.Location = new Point(549, 30);
      this.lblSearchInstruction.Name = "lblSearchInstruction";
      this.lblSearchInstruction.Size = new Size(221, 115);
      this.lblSearchInstruction.TabIndex = 12;
      this.lblSearchInstruction.Text = "If you can see your vehicle on the screen, please touch the picture.";
      this.lblSearchInstruction.TextAlign = ContentAlignment.MiddleCenter;
      this.lblSearchInstruction.Visible = false;
      this.lblRegistrationInstruction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblRegistrationInstruction.BackColor = Color.Black;
      this.lblRegistrationInstruction.Font = new Font("Arial", 20.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblRegistrationInstruction.ForeColor = Color.White;
      this.lblRegistrationInstruction.Location = new Point(36, 45);
      this.lblRegistrationInstruction.Name = "lblRegistrationInstruction";
      this.lblRegistrationInstruction.Size = new Size(734, 85);
      this.lblRegistrationInstruction.TabIndex = 11;
      this.lblRegistrationInstruction.Text = "PLEASE ENTER ANY PART OF YOUR VEHICLE REGISTRATION";
      this.lblRegistrationInstruction.TextAlign = ContentAlignment.MiddleCenter;
      this.imgSearchVRM4.BackColor = Color.Black;
      this.imgSearchVRM4.BackgroundImageLayout = ImageLayout.Stretch;
      this.imgSearchVRM4.BorderStyle = BorderStyle.FixedSingle;
      this.imgSearchVRM4.ErrorImage = (Image) null;
      this.imgSearchVRM4.InitialImage = (Image) null;
      this.imgSearchVRM4.Location = new Point(415, 120);
      this.imgSearchVRM4.Name = "imgSearchVRM4";
      this.imgSearchVRM4.Size = new Size(130, 25);
      this.imgSearchVRM4.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgSearchVRM4.TabIndex = 10;
      this.imgSearchVRM4.TabStop = false;
      this.imgSearchVRM4.Visible = false;
      this.imgSearchVRM4.Click += new EventHandler(this.imgSearchOverview_Click);
      this.imgSearchVRM3.BackColor = Color.Black;
      this.imgSearchVRM3.BackgroundImageLayout = ImageLayout.Stretch;
      this.imgSearchVRM3.BorderStyle = BorderStyle.FixedSingle;
      this.imgSearchVRM3.ErrorImage = (Image) null;
      this.imgSearchVRM3.InitialImage = (Image) null;
      this.imgSearchVRM3.Location = new Point(285, 120);
      this.imgSearchVRM3.Name = "imgSearchVRM3";
      this.imgSearchVRM3.Size = new Size(130, 25);
      this.imgSearchVRM3.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgSearchVRM3.TabIndex = 9;
      this.imgSearchVRM3.TabStop = false;
      this.imgSearchVRM3.Visible = false;
      this.imgSearchVRM3.Click += new EventHandler(this.imgSearchOverview_Click);
      this.imgSearchVRM2.BackColor = Color.Black;
      this.imgSearchVRM2.BackgroundImageLayout = ImageLayout.Stretch;
      this.imgSearchVRM2.BorderStyle = BorderStyle.FixedSingle;
      this.imgSearchVRM2.ErrorImage = (Image) null;
      this.imgSearchVRM2.InitialImage = (Image) null;
      this.imgSearchVRM2.Location = new Point(155, 120);
      this.imgSearchVRM2.Name = "imgSearchVRM2";
      this.imgSearchVRM2.Size = new Size(130, 25);
      this.imgSearchVRM2.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgSearchVRM2.TabIndex = 8;
      this.imgSearchVRM2.TabStop = false;
      this.imgSearchVRM2.Visible = false;
      this.imgSearchVRM2.Click += new EventHandler(this.imgSearchOverview_Click);
      this.imgSearchOverview4.BackColor = Color.Black;
      this.imgSearchOverview4.BackgroundImageLayout = ImageLayout.Stretch;
      this.imgSearchOverview4.BorderStyle = BorderStyle.FixedSingle;
      this.imgSearchOverview4.ErrorImage = (Image) null;
      this.imgSearchOverview4.InitialImage = (Image) null;
      this.imgSearchOverview4.Location = new Point(415, 30);
      this.imgSearchOverview4.Name = "imgSearchOverview4";
      this.imgSearchOverview4.Size = new Size(130, 90);
      this.imgSearchOverview4.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgSearchOverview4.TabIndex = 7;
      this.imgSearchOverview4.TabStop = false;
      this.imgSearchOverview4.Visible = false;
      this.imgSearchOverview4.Click += new EventHandler(this.imgSearchOverview_Click);
      this.imgSearchOverview3.BackColor = Color.Black;
      this.imgSearchOverview3.BackgroundImageLayout = ImageLayout.Stretch;
      this.imgSearchOverview3.BorderStyle = BorderStyle.FixedSingle;
      this.imgSearchOverview3.ErrorImage = (Image) null;
      this.imgSearchOverview3.InitialImage = (Image) null;
      this.imgSearchOverview3.Location = new Point(285, 30);
      this.imgSearchOverview3.Name = "imgSearchOverview3";
      this.imgSearchOverview3.Size = new Size(130, 90);
      this.imgSearchOverview3.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgSearchOverview3.TabIndex = 6;
      this.imgSearchOverview3.TabStop = false;
      this.imgSearchOverview3.Visible = false;
      this.imgSearchOverview3.Click += new EventHandler(this.imgSearchOverview_Click);
      this.imgSearchOverview2.BackColor = Color.Black;
      this.imgSearchOverview2.BackgroundImageLayout = ImageLayout.Stretch;
      this.imgSearchOverview2.BorderStyle = BorderStyle.FixedSingle;
      this.imgSearchOverview2.ErrorImage = (Image) null;
      this.imgSearchOverview2.InitialImage = (Image) null;
      this.imgSearchOverview2.Location = new Point(155, 30);
      this.imgSearchOverview2.Name = "imgSearchOverview2";
      this.imgSearchOverview2.Size = new Size(130, 90);
      this.imgSearchOverview2.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgSearchOverview2.TabIndex = 5;
      this.imgSearchOverview2.TabStop = false;
      this.imgSearchOverview2.Visible = false;
      this.imgSearchOverview2.Click += new EventHandler(this.imgSearchOverview_Click);
      this.imgSearchVRM1.BackColor = Color.Black;
      this.imgSearchVRM1.BackgroundImageLayout = ImageLayout.Stretch;
      this.imgSearchVRM1.BorderStyle = BorderStyle.FixedSingle;
      this.imgSearchVRM1.ErrorImage = (Image) null;
      this.imgSearchVRM1.InitialImage = (Image) null;
      this.imgSearchVRM1.Location = new Point(25, 120);
      this.imgSearchVRM1.Name = "imgSearchVRM1";
      this.imgSearchVRM1.Size = new Size(130, 25);
      this.imgSearchVRM1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgSearchVRM1.TabIndex = 4;
      this.imgSearchVRM1.TabStop = false;
      this.imgSearchVRM1.Visible = false;
      this.imgSearchVRM1.Click += new EventHandler(this.imgSearchOverview_Click);
      this.imgSearchOverview1.BackColor = Color.Black;
      this.imgSearchOverview1.BackgroundImageLayout = ImageLayout.Stretch;
      this.imgSearchOverview1.BorderStyle = BorderStyle.FixedSingle;
      this.imgSearchOverview1.ErrorImage = (Image) null;
      this.imgSearchOverview1.InitialImage = (Image) null;
      this.imgSearchOverview1.Location = new Point(25, 30);
      this.imgSearchOverview1.Name = "imgSearchOverview1";
      this.imgSearchOverview1.Size = new Size(130, 90);
      this.imgSearchOverview1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgSearchOverview1.TabIndex = 3;
      this.imgSearchOverview1.TabStop = false;
      this.imgSearchOverview1.Visible = false;
      this.imgSearchOverview1.Click += new EventHandler(this.imgSearchOverview_Click);
      this.imgDisplayVehicleBackground.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.imgDisplayVehicleBackground.BackColor = Color.Black;
      this.imgDisplayVehicleBackground.Location = new Point(25, 30);
      this.imgDisplayVehicleBackground.Name = "imgDisplayVehicleBackground";
      this.imgDisplayVehicleBackground.Size = new Size(750, 115);
      this.imgDisplayVehicleBackground.TabIndex = 1;
      this.imgDisplayVehicleBackground.TabStop = false;
      this.imgDisplayVehicleBorder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.imgDisplayVehicleBorder.BackColor = Color.Ivory;
      this.imgDisplayVehicleBorder.Location = new Point(20, 25);
      this.imgDisplayVehicleBorder.Name = "imgDisplayVehicleBorder";
      this.imgDisplayVehicleBorder.Size = new Size(758, 125);
      this.imgDisplayVehicleBorder.TabIndex = 0;
      this.imgDisplayVehicleBorder.TabStop = false;
      this.pnlRegistrationDisplay.BackColor = Color.Transparent;
      this.pnlRegistrationDisplay.Controls.Add((Control) this.cmdVehicleNotPresent);
      this.pnlRegistrationDisplay.Controls.Add((Control) this.cmdEnter);
      this.pnlRegistrationDisplay.Controls.Add((Control) this.cmdCancel);
      this.pnlRegistrationDisplay.Controls.Add((Control) this.lblInstruactionPopUp);
      this.pnlRegistrationDisplay.Controls.Add((Control) this.registrationPlate1);
      this.pnlRegistrationDisplay.Dock = DockStyle.Top;
      this.pnlRegistrationDisplay.Location = new Point(0, 199);
      this.pnlRegistrationDisplay.Name = "pnlRegistrationDisplay";
      this.pnlRegistrationDisplay.Size = new Size(798, 120);
      this.pnlRegistrationDisplay.TabIndex = 4;
      this.cmdVehicleNotPresent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdVehicleNotPresent.BackColor = Color.Transparent;
      this.cmdVehicleNotPresent.BackgroundImage = (Image) Resources.GreenKey;
      this.cmdVehicleNotPresent.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdVehicleNotPresent.FlatAppearance.BorderSize = 0;
      this.cmdVehicleNotPresent.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.cmdVehicleNotPresent.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdVehicleNotPresent.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdVehicleNotPresent.FlatStyle = FlatStyle.Flat;
      this.cmdVehicleNotPresent.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdVehicleNotPresent.ForeColor = Color.Gold;
      this.cmdVehicleNotPresent.Location = new Point(458, 25);
      this.cmdVehicleNotPresent.Name = "cmdVehicleNotPresent";
      this.cmdVehicleNotPresent.Size = new Size(157, 65);
      this.cmdVehicleNotPresent.TabIndex = 4;
      this.cmdVehicleNotPresent.Text = "ENTER";
      this.cmdVehicleNotPresent.UseVisualStyleBackColor = false;
      this.cmdVehicleNotPresent.Visible = false;
      this.cmdVehicleNotPresent.Click += new EventHandler(this.cmdVehicleNotPresent_Click);
      this.cmdEnter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdEnter.BackColor = Color.Transparent;
      this.cmdEnter.BackgroundImage = (Image) Resources.GreenKeyGloss;
      this.cmdEnter.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdEnter.FlatAppearance.BorderSize = 0;
      this.cmdEnter.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.cmdEnter.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdEnter.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdEnter.FlatStyle = FlatStyle.Flat;
      this.cmdEnter.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdEnter.ForeColor = Color.Gold;
      this.cmdEnter.Location = new Point(458, 25);
      this.cmdEnter.Name = "cmdEnter";
      this.cmdEnter.Size = new Size(157, 65);
      this.cmdEnter.TabIndex = 1;
      this.cmdEnter.Text = "ENTER";
      this.cmdEnter.UseVisualStyleBackColor = false;
      this.cmdEnter.Visible = false;
      this.cmdEnter.Click += new EventHandler(this.cmdEnter_Click);
      this.cmdCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdCancel.BackColor = Color.Transparent;
      this.cmdCancel.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.cmdCancel.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdCancel.FlatAppearance.BorderSize = 0;
      this.cmdCancel.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.cmdCancel.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdCancel.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdCancel.FlatStyle = FlatStyle.Flat;
      this.cmdCancel.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdCancel.ForeColor = Color.White;
      this.cmdCancel.Location = new Point(621, 25);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new Size(149, 65);
      this.cmdCancel.TabIndex = 2;
      this.cmdCancel.Text = "CANCEL";
      this.cmdCancel.UseVisualStyleBackColor = false;
      this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
      this.lblInstruactionPopUp.BackColor = Color.Transparent;
      this.lblInstruactionPopUp.Font = new Font("Arial", 13f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblInstruactionPopUp.ForeColor = Color.Gold;
      this.lblInstruactionPopUp.Location = new Point(607, 0);
      this.lblInstruactionPopUp.Name = "lblInstruactionPopUp";
      this.lblInstruactionPopUp.Size = new Size(181, 117);
      this.lblInstruactionPopUp.TabIndex = 3;
      this.lblInstruactionPopUp.Text = "If your vehicle does not appear, please type in your full registration number and then press Enter";
      this.lblInstruactionPopUp.TextAlign = ContentAlignment.MiddleCenter;
      this.lblInstruactionPopUp.Visible = false;
      this.registrationPlate1.BackColor = Color.Transparent;
      this.registrationPlate1.BackgroundImage = (Image) componentResourceManager.GetObject("registrationPlate1.BackgroundImage");
      this.registrationPlate1.BackgroundImageLayout = ImageLayout.Center;
      this.registrationPlate1.Location = new Point(12, 11);
      this.registrationPlate1.Name = "registrationPlate1";
      this.registrationPlate1.Size = new Size(475, 106);
      this.registrationPlate1.TabIndex = 5;
      this.registrationPlate1.VehicleRegistrationMark = "888888888888";
      this.pnlKeyboard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.pnlKeyboard.BackColor = Color.Transparent;
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyA);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyB);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyC);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyD);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyE);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyF);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyDel);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyG);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyM);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyH);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyI);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyJ);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyK);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyL);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyN);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyO);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyT);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyU);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyV);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyP);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyW);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyQ);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyX);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyR);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyY);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyS);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKeyZ);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKey1);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKey2);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKey3);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKey4);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKey5);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKey6);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKey7);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKey8);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKey9);
      this.pnlKeyboard.Controls.Add((Control) this.cmdKey0);
      this.pnlKeyboard.Location = new Point(0, 337);
      this.pnlKeyboard.Name = "pnlKeyboard";
      this.pnlKeyboard.Size = new Size(798, 248);
      this.pnlKeyboard.TabIndex = 5;
      this.cmdKeyA.BackColor = Color.Transparent;
      this.cmdKeyA.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyA.BackgroundImage");
      this.cmdKeyA.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyA.FlatAppearance.BorderSize = 0;
      this.cmdKeyA.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyA.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyA.FlatStyle = FlatStyle.Flat;
      this.cmdKeyA.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyA.ForeColor = Color.Gold;
      this.cmdKeyA.Location = new Point(60, 3);
      this.cmdKeyA.Name = "cmdKeyA";
      this.cmdKeyA.Size = new Size(50, 45);
      this.cmdKeyA.TabIndex = 0;
      this.cmdKeyA.Text = "A";
      this.cmdKeyA.UseVisualStyleBackColor = false;
      this.cmdKeyA.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyB.BackColor = Color.Transparent;
      this.cmdKeyB.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyB.BackgroundImage");
      this.cmdKeyB.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyB.FlatAppearance.BorderSize = 0;
      this.cmdKeyB.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyB.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyB.FlatStyle = FlatStyle.Flat;
      this.cmdKeyB.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyB.ForeColor = Color.Gold;
      this.cmdKeyB.Location = new Point(125, 3);
      this.cmdKeyB.Name = "cmdKeyB";
      this.cmdKeyB.Size = new Size(50, 45);
      this.cmdKeyB.TabIndex = 1;
      this.cmdKeyB.Text = "B";
      this.cmdKeyB.UseVisualStyleBackColor = false;
      this.cmdKeyB.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyC.BackColor = Color.Transparent;
      this.cmdKeyC.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyC.BackgroundImage");
      this.cmdKeyC.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyC.FlatAppearance.BorderSize = 0;
      this.cmdKeyC.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyC.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyC.FlatStyle = FlatStyle.Flat;
      this.cmdKeyC.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyC.ForeColor = Color.Gold;
      this.cmdKeyC.Location = new Point(190, 3);
      this.cmdKeyC.Name = "cmdKeyC";
      this.cmdKeyC.Size = new Size(50, 45);
      this.cmdKeyC.TabIndex = 2;
      this.cmdKeyC.Text = "C";
      this.cmdKeyC.UseVisualStyleBackColor = false;
      this.cmdKeyC.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyD.BackColor = Color.Transparent;
      this.cmdKeyD.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyD.BackgroundImage");
      this.cmdKeyD.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyD.FlatAppearance.BorderSize = 0;
      this.cmdKeyD.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyD.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyD.FlatStyle = FlatStyle.Flat;
      this.cmdKeyD.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyD.ForeColor = Color.Gold;
      this.cmdKeyD.Location = new Point((int) byte.MaxValue, 3);
      this.cmdKeyD.Name = "cmdKeyD";
      this.cmdKeyD.Size = new Size(50, 45);
      this.cmdKeyD.TabIndex = 3;
      this.cmdKeyD.Text = "D";
      this.cmdKeyD.UseVisualStyleBackColor = false;
      this.cmdKeyD.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyE.BackColor = Color.Transparent;
      this.cmdKeyE.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyE.BackgroundImage");
      this.cmdKeyE.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyE.FlatAppearance.BorderSize = 0;
      this.cmdKeyE.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyE.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyE.FlatStyle = FlatStyle.Flat;
      this.cmdKeyE.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyE.ForeColor = Color.Gold;
      this.cmdKeyE.Location = new Point(320, 3);
      this.cmdKeyE.Name = "cmdKeyE";
      this.cmdKeyE.Size = new Size(50, 45);
      this.cmdKeyE.TabIndex = 4;
      this.cmdKeyE.Text = "E";
      this.cmdKeyE.UseVisualStyleBackColor = false;
      this.cmdKeyE.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyF.BackColor = Color.Transparent;
      this.cmdKeyF.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyF.BackgroundImage");
      this.cmdKeyF.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyF.FlatAppearance.BorderSize = 0;
      this.cmdKeyF.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyF.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyF.FlatStyle = FlatStyle.Flat;
      this.cmdKeyF.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyF.ForeColor = Color.Gold;
      this.cmdKeyF.Location = new Point(385, 3);
      this.cmdKeyF.Name = "cmdKeyF";
      this.cmdKeyF.Size = new Size(50, 45);
      this.cmdKeyF.TabIndex = 5;
      this.cmdKeyF.Text = "F";
      this.cmdKeyF.UseVisualStyleBackColor = false;
      this.cmdKeyF.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyDel.BackColor = Color.Transparent;
      this.cmdKeyDel.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyDel.BackgroundImage");
      this.cmdKeyDel.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyDel.FlatAppearance.BorderSize = 0;
      this.cmdKeyDel.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyDel.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyDel.FlatStyle = FlatStyle.Flat;
      this.cmdKeyDel.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyDel.ForeColor = Color.Gold;
      this.cmdKeyDel.Location = new Point(450, 3);
      this.cmdKeyDel.Name = "cmdKeyDel";
      this.cmdKeyDel.Size = new Size(50, 103);
      this.cmdKeyDel.TabIndex = 6;
      this.cmdKeyDel.Text = "DEL";
      this.cmdKeyDel.UseVisualStyleBackColor = false;
      this.cmdKeyDel.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyG.BackColor = Color.Transparent;
      this.cmdKeyG.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyG.BackgroundImage");
      this.cmdKeyG.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyG.FlatAppearance.BorderSize = 0;
      this.cmdKeyG.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyG.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyG.FlatStyle = FlatStyle.Flat;
      this.cmdKeyG.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyG.ForeColor = Color.Gold;
      this.cmdKeyG.Location = new Point(60, 58);
      this.cmdKeyG.Name = "cmdKeyG";
      this.cmdKeyG.Size = new Size(50, 45);
      this.cmdKeyG.TabIndex = 7;
      this.cmdKeyG.Text = "G";
      this.cmdKeyG.UseVisualStyleBackColor = false;
      this.cmdKeyG.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyM.BackColor = Color.Transparent;
      this.cmdKeyM.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyM.BackgroundImage");
      this.cmdKeyM.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyM.FlatAppearance.BorderSize = 0;
      this.cmdKeyM.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyM.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyM.FlatStyle = FlatStyle.Flat;
      this.cmdKeyM.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyM.ForeColor = Color.Gold;
      this.cmdKeyM.Location = new Point(61, 113);
      this.cmdKeyM.Name = "cmdKeyM";
      this.cmdKeyM.Size = new Size(50, 45);
      this.cmdKeyM.TabIndex = 13;
      this.cmdKeyM.Text = "M";
      this.cmdKeyM.UseVisualStyleBackColor = false;
      this.cmdKeyM.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyH.BackColor = Color.Transparent;
      this.cmdKeyH.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyH.BackgroundImage");
      this.cmdKeyH.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyH.FlatAppearance.BorderSize = 0;
      this.cmdKeyH.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyH.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyH.FlatStyle = FlatStyle.Flat;
      this.cmdKeyH.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyH.ForeColor = Color.Gold;
      this.cmdKeyH.Location = new Point(125, 58);
      this.cmdKeyH.Name = "cmdKeyH";
      this.cmdKeyH.Size = new Size(50, 45);
      this.cmdKeyH.TabIndex = 14;
      this.cmdKeyH.Text = "H";
      this.cmdKeyH.UseVisualStyleBackColor = false;
      this.cmdKeyH.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyI.BackColor = Color.Transparent;
      this.cmdKeyI.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyI.BackgroundImage");
      this.cmdKeyI.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyI.FlatAppearance.BorderSize = 0;
      this.cmdKeyI.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyI.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyI.FlatStyle = FlatStyle.Flat;
      this.cmdKeyI.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyI.ForeColor = Color.Gold;
      this.cmdKeyI.Location = new Point(190, 58);
      this.cmdKeyI.Name = "cmdKeyI";
      this.cmdKeyI.Size = new Size(50, 45);
      this.cmdKeyI.TabIndex = 15;
      this.cmdKeyI.Text = "I";
      this.cmdKeyI.UseVisualStyleBackColor = false;
      this.cmdKeyI.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyJ.BackColor = Color.Transparent;
      this.cmdKeyJ.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyJ.BackgroundImage");
      this.cmdKeyJ.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyJ.FlatAppearance.BorderSize = 0;
      this.cmdKeyJ.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyJ.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyJ.FlatStyle = FlatStyle.Flat;
      this.cmdKeyJ.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyJ.ForeColor = Color.Gold;
      this.cmdKeyJ.Location = new Point((int) byte.MaxValue, 58);
      this.cmdKeyJ.Name = "cmdKeyJ";
      this.cmdKeyJ.Size = new Size(50, 45);
      this.cmdKeyJ.TabIndex = 16;
      this.cmdKeyJ.Text = "J";
      this.cmdKeyJ.UseVisualStyleBackColor = false;
      this.cmdKeyJ.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyK.BackColor = Color.Transparent;
      this.cmdKeyK.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyK.BackgroundImage");
      this.cmdKeyK.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyK.FlatAppearance.BorderSize = 0;
      this.cmdKeyK.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyK.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyK.FlatStyle = FlatStyle.Flat;
      this.cmdKeyK.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyK.ForeColor = Color.Gold;
      this.cmdKeyK.Location = new Point(320, 58);
      this.cmdKeyK.Name = "cmdKeyK";
      this.cmdKeyK.Size = new Size(50, 45);
      this.cmdKeyK.TabIndex = 17;
      this.cmdKeyK.Text = "K";
      this.cmdKeyK.UseVisualStyleBackColor = false;
      this.cmdKeyK.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyL.BackColor = Color.Transparent;
      this.cmdKeyL.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyL.BackgroundImage");
      this.cmdKeyL.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyL.FlatAppearance.BorderSize = 0;
      this.cmdKeyL.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyL.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyL.FlatStyle = FlatStyle.Flat;
      this.cmdKeyL.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyL.ForeColor = Color.Gold;
      this.cmdKeyL.Location = new Point(385, 58);
      this.cmdKeyL.Name = "cmdKeyL";
      this.cmdKeyL.Size = new Size(50, 45);
      this.cmdKeyL.TabIndex = 18;
      this.cmdKeyL.Text = "L";
      this.cmdKeyL.UseVisualStyleBackColor = false;
      this.cmdKeyL.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyN.BackColor = Color.Transparent;
      this.cmdKeyN.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyN.BackgroundImage");
      this.cmdKeyN.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyN.FlatAppearance.BorderSize = 0;
      this.cmdKeyN.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyN.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyN.FlatStyle = FlatStyle.Flat;
      this.cmdKeyN.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyN.ForeColor = Color.Gold;
      this.cmdKeyN.Location = new Point(125, 113);
      this.cmdKeyN.Name = "cmdKeyN";
      this.cmdKeyN.Size = new Size(50, 45);
      this.cmdKeyN.TabIndex = 19;
      this.cmdKeyN.Text = "N";
      this.cmdKeyN.UseVisualStyleBackColor = false;
      this.cmdKeyN.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyO.BackColor = Color.Transparent;
      this.cmdKeyO.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyO.BackgroundImage");
      this.cmdKeyO.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyO.FlatAppearance.BorderSize = 0;
      this.cmdKeyO.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyO.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyO.FlatStyle = FlatStyle.Flat;
      this.cmdKeyO.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyO.ForeColor = Color.Gold;
      this.cmdKeyO.Location = new Point(190, 113);
      this.cmdKeyO.Name = "cmdKeyO";
      this.cmdKeyO.Size = new Size(50, 45);
      this.cmdKeyO.TabIndex = 20;
      this.cmdKeyO.Text = "O";
      this.cmdKeyO.UseVisualStyleBackColor = false;
      this.cmdKeyO.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyT.BackColor = Color.Transparent;
      this.cmdKeyT.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyT.BackgroundImage");
      this.cmdKeyT.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyT.FlatAppearance.BorderSize = 0;
      this.cmdKeyT.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyT.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyT.FlatStyle = FlatStyle.Flat;
      this.cmdKeyT.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyT.ForeColor = Color.Gold;
      this.cmdKeyT.Location = new Point(60, 168);
      this.cmdKeyT.Name = "cmdKeyT";
      this.cmdKeyT.Size = new Size(50, 45);
      this.cmdKeyT.TabIndex = 21;
      this.cmdKeyT.Text = "T";
      this.cmdKeyT.UseVisualStyleBackColor = false;
      this.cmdKeyT.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyU.BackColor = Color.Transparent;
      this.cmdKeyU.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyU.BackgroundImage");
      this.cmdKeyU.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyU.FlatAppearance.BorderSize = 0;
      this.cmdKeyU.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyU.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyU.FlatStyle = FlatStyle.Flat;
      this.cmdKeyU.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyU.ForeColor = Color.Gold;
      this.cmdKeyU.Location = new Point(125, 168);
      this.cmdKeyU.Name = "cmdKeyU";
      this.cmdKeyU.Size = new Size(50, 45);
      this.cmdKeyU.TabIndex = 22;
      this.cmdKeyU.Text = "U";
      this.cmdKeyU.UseVisualStyleBackColor = false;
      this.cmdKeyU.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyV.BackColor = Color.Transparent;
      this.cmdKeyV.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyV.BackgroundImage");
      this.cmdKeyV.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyV.FlatAppearance.BorderSize = 0;
      this.cmdKeyV.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyV.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyV.FlatStyle = FlatStyle.Flat;
      this.cmdKeyV.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyV.ForeColor = Color.Gold;
      this.cmdKeyV.Location = new Point(190, 168);
      this.cmdKeyV.Name = "cmdKeyV";
      this.cmdKeyV.Size = new Size(50, 45);
      this.cmdKeyV.TabIndex = 23;
      this.cmdKeyV.Text = "V";
      this.cmdKeyV.UseVisualStyleBackColor = false;
      this.cmdKeyV.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyP.BackColor = Color.Transparent;
      this.cmdKeyP.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyP.BackgroundImage");
      this.cmdKeyP.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyP.FlatAppearance.BorderSize = 0;
      this.cmdKeyP.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyP.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyP.FlatStyle = FlatStyle.Flat;
      this.cmdKeyP.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyP.ForeColor = Color.Gold;
      this.cmdKeyP.Location = new Point((int) byte.MaxValue, 113);
      this.cmdKeyP.Name = "cmdKeyP";
      this.cmdKeyP.Size = new Size(50, 45);
      this.cmdKeyP.TabIndex = 24;
      this.cmdKeyP.Text = "P";
      this.cmdKeyP.UseVisualStyleBackColor = false;
      this.cmdKeyP.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyW.BackColor = Color.Transparent;
      this.cmdKeyW.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyW.BackgroundImage");
      this.cmdKeyW.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyW.FlatAppearance.BorderSize = 0;
      this.cmdKeyW.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyW.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyW.FlatStyle = FlatStyle.Flat;
      this.cmdKeyW.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyW.ForeColor = Color.Gold;
      this.cmdKeyW.Location = new Point((int) byte.MaxValue, 168);
      this.cmdKeyW.Name = "cmdKeyW";
      this.cmdKeyW.Size = new Size(50, 45);
      this.cmdKeyW.TabIndex = 25;
      this.cmdKeyW.Text = "W";
      this.cmdKeyW.UseVisualStyleBackColor = false;
      this.cmdKeyW.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyQ.BackColor = Color.Transparent;
      this.cmdKeyQ.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyQ.BackgroundImage");
      this.cmdKeyQ.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyQ.FlatAppearance.BorderSize = 0;
      this.cmdKeyQ.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyQ.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyQ.FlatStyle = FlatStyle.Flat;
      this.cmdKeyQ.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyQ.ForeColor = Color.Gold;
      this.cmdKeyQ.Location = new Point(320, 113);
      this.cmdKeyQ.Name = "cmdKeyQ";
      this.cmdKeyQ.Size = new Size(50, 45);
      this.cmdKeyQ.TabIndex = 26;
      this.cmdKeyQ.Text = "Q";
      this.cmdKeyQ.UseVisualStyleBackColor = false;
      this.cmdKeyQ.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyX.BackColor = Color.Transparent;
      this.cmdKeyX.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyX.BackgroundImage");
      this.cmdKeyX.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyX.FlatAppearance.BorderSize = 0;
      this.cmdKeyX.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyX.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyX.FlatStyle = FlatStyle.Flat;
      this.cmdKeyX.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyX.ForeColor = Color.Gold;
      this.cmdKeyX.Location = new Point(320, 168);
      this.cmdKeyX.Name = "cmdKeyX";
      this.cmdKeyX.Size = new Size(50, 45);
      this.cmdKeyX.TabIndex = 27;
      this.cmdKeyX.Text = "X";
      this.cmdKeyX.UseVisualStyleBackColor = false;
      this.cmdKeyX.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyR.BackColor = Color.Transparent;
      this.cmdKeyR.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyR.BackgroundImage");
      this.cmdKeyR.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyR.FlatAppearance.BorderSize = 0;
      this.cmdKeyR.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyR.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyR.FlatStyle = FlatStyle.Flat;
      this.cmdKeyR.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyR.ForeColor = Color.Gold;
      this.cmdKeyR.Location = new Point(385, 113);
      this.cmdKeyR.Name = "cmdKeyR";
      this.cmdKeyR.Size = new Size(50, 45);
      this.cmdKeyR.TabIndex = 28;
      this.cmdKeyR.Text = "R";
      this.cmdKeyR.UseVisualStyleBackColor = false;
      this.cmdKeyR.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyY.BackColor = Color.Transparent;
      this.cmdKeyY.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyY.BackgroundImage");
      this.cmdKeyY.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyY.FlatAppearance.BorderSize = 0;
      this.cmdKeyY.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyY.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyY.FlatStyle = FlatStyle.Flat;
      this.cmdKeyY.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyY.ForeColor = Color.Gold;
      this.cmdKeyY.Location = new Point(385, 168);
      this.cmdKeyY.Name = "cmdKeyY";
      this.cmdKeyY.Size = new Size(50, 45);
      this.cmdKeyY.TabIndex = 29;
      this.cmdKeyY.Text = "Y";
      this.cmdKeyY.UseVisualStyleBackColor = false;
      this.cmdKeyY.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyS.BackColor = Color.Transparent;
      this.cmdKeyS.BackgroundImage = (Image) Resources.BlueKey;
      this.cmdKeyS.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyS.FlatAppearance.BorderSize = 0;
      this.cmdKeyS.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyS.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyS.FlatStyle = FlatStyle.Flat;
      this.cmdKeyS.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyS.ForeColor = Color.Gold;
      this.cmdKeyS.Location = new Point(450, 113);
      this.cmdKeyS.Name = "cmdKeyS";
      this.cmdKeyS.Size = new Size(50, 45);
      this.cmdKeyS.TabIndex = 30;
      this.cmdKeyS.Text = "S";
      this.cmdKeyS.UseVisualStyleBackColor = false;
      this.cmdKeyS.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKeyZ.BackColor = Color.Transparent;
      this.cmdKeyZ.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKeyZ.BackgroundImage");
      this.cmdKeyZ.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKeyZ.FlatAppearance.BorderSize = 0;
      this.cmdKeyZ.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKeyZ.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKeyZ.FlatStyle = FlatStyle.Flat;
      this.cmdKeyZ.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKeyZ.ForeColor = Color.Gold;
      this.cmdKeyZ.Location = new Point(450, 168);
      this.cmdKeyZ.Name = "cmdKeyZ";
      this.cmdKeyZ.Size = new Size(50, 45);
      this.cmdKeyZ.TabIndex = 31;
      this.cmdKeyZ.Text = "Z";
      this.cmdKeyZ.UseVisualStyleBackColor = false;
      this.cmdKeyZ.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey1.BackColor = Color.Transparent;
      this.cmdKey1.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKey1.BackgroundImage");
      this.cmdKey1.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey1.FlatAppearance.BorderSize = 0;
      this.cmdKey1.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey1.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey1.FlatStyle = FlatStyle.Flat;
      this.cmdKey1.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey1.ForeColor = Color.Gold;
      this.cmdKey1.Location = new Point(565, 3);
      this.cmdKey1.Name = "cmdKey1";
      this.cmdKey1.Size = new Size(50, 45);
      this.cmdKey1.TabIndex = 32;
      this.cmdKey1.Text = "1";
      this.cmdKey1.UseVisualStyleBackColor = false;
      this.cmdKey1.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey2.BackColor = Color.Transparent;
      this.cmdKey2.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKey2.BackgroundImage");
      this.cmdKey2.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey2.FlatAppearance.BorderSize = 0;
      this.cmdKey2.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey2.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey2.FlatStyle = FlatStyle.Flat;
      this.cmdKey2.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey2.ForeColor = Color.Gold;
      this.cmdKey2.Location = new Point(630, 3);
      this.cmdKey2.Name = "cmdKey2";
      this.cmdKey2.Size = new Size(50, 45);
      this.cmdKey2.TabIndex = 33;
      this.cmdKey2.Text = "2";
      this.cmdKey2.UseVisualStyleBackColor = false;
      this.cmdKey2.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey3.BackColor = Color.Transparent;
      this.cmdKey3.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKey3.BackgroundImage");
      this.cmdKey3.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey3.FlatAppearance.BorderSize = 0;
      this.cmdKey3.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey3.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey3.FlatStyle = FlatStyle.Flat;
      this.cmdKey3.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey3.ForeColor = Color.Gold;
      this.cmdKey3.Location = new Point(695, 3);
      this.cmdKey3.Name = "cmdKey3";
      this.cmdKey3.Size = new Size(50, 45);
      this.cmdKey3.TabIndex = 34;
      this.cmdKey3.Text = "3";
      this.cmdKey3.UseVisualStyleBackColor = false;
      this.cmdKey3.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey4.BackColor = Color.Transparent;
      this.cmdKey4.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKey4.BackgroundImage");
      this.cmdKey4.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey4.FlatAppearance.BorderSize = 0;
      this.cmdKey4.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey4.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey4.FlatStyle = FlatStyle.Flat;
      this.cmdKey4.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey4.ForeColor = Color.Gold;
      this.cmdKey4.Location = new Point(565, 58);
      this.cmdKey4.Name = "cmdKey4";
      this.cmdKey4.Size = new Size(50, 45);
      this.cmdKey4.TabIndex = 35;
      this.cmdKey4.Text = "4";
      this.cmdKey4.UseVisualStyleBackColor = false;
      this.cmdKey4.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey5.BackColor = Color.Transparent;
      this.cmdKey5.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKey5.BackgroundImage");
      this.cmdKey5.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey5.FlatAppearance.BorderSize = 0;
      this.cmdKey5.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey5.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey5.FlatStyle = FlatStyle.Flat;
      this.cmdKey5.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey5.ForeColor = Color.Gold;
      this.cmdKey5.Location = new Point(630, 58);
      this.cmdKey5.Name = "cmdKey5";
      this.cmdKey5.Size = new Size(50, 45);
      this.cmdKey5.TabIndex = 36;
      this.cmdKey5.Text = "5";
      this.cmdKey5.UseVisualStyleBackColor = false;
      this.cmdKey5.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey6.BackColor = Color.Transparent;
      this.cmdKey6.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKey6.BackgroundImage");
      this.cmdKey6.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey6.FlatAppearance.BorderSize = 0;
      this.cmdKey6.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey6.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey6.FlatStyle = FlatStyle.Flat;
      this.cmdKey6.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey6.ForeColor = Color.Gold;
      this.cmdKey6.Location = new Point(695, 58);
      this.cmdKey6.Name = "cmdKey6";
      this.cmdKey6.Size = new Size(50, 45);
      this.cmdKey6.TabIndex = 38;
      this.cmdKey6.Text = "6";
      this.cmdKey6.UseVisualStyleBackColor = false;
      this.cmdKey6.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey7.BackColor = Color.Transparent;
      this.cmdKey7.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKey7.BackgroundImage");
      this.cmdKey7.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey7.FlatAppearance.BorderSize = 0;
      this.cmdKey7.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey7.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey7.FlatStyle = FlatStyle.Flat;
      this.cmdKey7.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey7.ForeColor = Color.Gold;
      this.cmdKey7.Location = new Point(565, 113);
      this.cmdKey7.Name = "cmdKey7";
      this.cmdKey7.Size = new Size(50, 45);
      this.cmdKey7.TabIndex = 37;
      this.cmdKey7.Text = "7";
      this.cmdKey7.UseVisualStyleBackColor = false;
      this.cmdKey7.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey8.BackColor = Color.Transparent;
      this.cmdKey8.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKey8.BackgroundImage");
      this.cmdKey8.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey8.FlatAppearance.BorderSize = 0;
      this.cmdKey8.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey8.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey8.FlatStyle = FlatStyle.Flat;
      this.cmdKey8.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey8.ForeColor = Color.Gold;
      this.cmdKey8.Location = new Point(630, 113);
      this.cmdKey8.Name = "cmdKey8";
      this.cmdKey8.Size = new Size(50, 45);
      this.cmdKey8.TabIndex = 39;
      this.cmdKey8.Text = "8";
      this.cmdKey8.UseVisualStyleBackColor = false;
      this.cmdKey8.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey9.BackColor = Color.Transparent;
      this.cmdKey9.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKey9.BackgroundImage");
      this.cmdKey9.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey9.FlatAppearance.BorderSize = 0;
      this.cmdKey9.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey9.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey9.FlatStyle = FlatStyle.Flat;
      this.cmdKey9.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey9.ForeColor = Color.Gold;
      this.cmdKey9.Location = new Point(695, 113);
      this.cmdKey9.Name = "cmdKey9";
      this.cmdKey9.Size = new Size(50, 45);
      this.cmdKey9.TabIndex = 40;
      this.cmdKey9.Text = "9";
      this.cmdKey9.UseVisualStyleBackColor = false;
      this.cmdKey9.Click += new EventHandler(this.ShowEnteredKeys);
      this.cmdKey0.BackColor = Color.Transparent;
      this.cmdKey0.BackgroundImage = (Image) componentResourceManager.GetObject("cmdKey0.BackgroundImage");
      this.cmdKey0.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdKey0.FlatAppearance.BorderSize = 0;
      this.cmdKey0.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdKey0.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdKey0.FlatStyle = FlatStyle.Flat;
      this.cmdKey0.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdKey0.ForeColor = Color.Gold;
      this.cmdKey0.Location = new Point(630, 168);
      this.cmdKey0.Name = "cmdKey0";
      this.cmdKey0.Size = new Size(50, 45);
      this.cmdKey0.TabIndex = 41;
      this.cmdKey0.Text = "0";
      this.cmdKey0.UseVisualStyleBackColor = false;
      this.cmdKey0.Click += new EventHandler(this.ShowEnteredKeys);
      this.DisplayTimer.Enabled = true;
      this.DisplayTimer.Interval = 30000;
      this.DisplayTimer.Tick += new EventHandler(this.DisplayTimer_Tick);
      this.tmrShowTip.Interval = 4000;
      this.tmrShowTip.Tick += new EventHandler(this.tmrShowTip_Tick);
      this.imgLogo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.imgLogo.BackColor = Color.SteelBlue;
      this.imgLogo.Image = (Image) Resources.RangerLogo;
      this.imgLogo.Location = new Point(746, 0);
      this.imgLogo.Name = "imgLogo";
      this.imgLogo.Size = new Size(40, 40);
      this.imgLogo.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgLogo.TabIndex = 1;
      this.imgLogo.TabStop = false;
      this.imgLogo.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.SteelBlue;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(798, 598);
      this.ControlBox = false;
      this.Controls.Add((Control) this.pnlKeyboard);
      this.Controls.Add((Control) this.imgLogo);
      this.Controls.Add((Control) this.pnlRegistrationDisplay);
      this.Controls.Add((Control) this.pnlDisplayVehicle);
      this.Controls.Add((Control) this.lblInstructionHeader);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmSearchVehicle);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.frmSearchVehicle_Load);
      this.pnlDisplayVehicle.ResumeLayout(false);
      ((ISupportInitialize) this.imgSearchVRM4).EndInit();
      ((ISupportInitialize) this.imgSearchVRM3).EndInit();
      ((ISupportInitialize) this.imgSearchVRM2).EndInit();
      ((ISupportInitialize) this.imgSearchOverview4).EndInit();
      ((ISupportInitialize) this.imgSearchOverview3).EndInit();
      ((ISupportInitialize) this.imgSearchOverview2).EndInit();
      ((ISupportInitialize) this.imgSearchVRM1).EndInit();
      ((ISupportInitialize) this.imgSearchOverview1).EndInit();
      ((ISupportInitialize) this.imgDisplayVehicleBackground).EndInit();
      ((ISupportInitialize) this.imgDisplayVehicleBorder).EndInit();
      this.pnlRegistrationDisplay.ResumeLayout(false);
      this.pnlKeyboard.ResumeLayout(false);
      ((ISupportInitialize) this.imgLogo).EndInit();
      this.ResumeLayout(false);
    }
  }
}
