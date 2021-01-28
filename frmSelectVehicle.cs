// Decompiled with JetBrains decompiler
// Type: TouchPark.frmSelectVehicle
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TouchPark.Properties;

namespace TouchPark
{
  public class frmSelectVehicle : Form
  {
    private List<PictureBox> m_allImgOverview;
    private List<PictureBox> m_allImgVRM;
    private List<CVehicleInfo> m_allVehicleInfo;
    private DateTime m_StartTime;
    private DateTime m_EndTime;
    private CGetVehicleData m_clsVehicleData;
    private bool m_FormActivity;
    private IContainer components;
    private Label lblInstructionHeader;
    private PictureBox imgLogo;
    private Panel pnlImageOverview;
    private Panel pnlTimeDisplay;
    private Button cmdNext;
    private Panel pnlSearch;
    private PictureBox imgOverview1;
    private PictureBox imgOverview2;
    private PictureBox imgOverview4;
    private PictureBox imgOverview3;
    private PictureBox imgOverview6;
    private PictureBox imgOverview5;
    private PictureBox imgOverview8;
    private PictureBox imgOverview7;
    private PictureBox imgOverview16;
    private PictureBox imgOverview15;
    private PictureBox imgOverview14;
    private PictureBox imgOverview13;
    private PictureBox imgOverview12;
    private PictureBox imgOverview11;
    private PictureBox imgOverview10;
    private PictureBox imgOverview9;
    private PictureBox imgVRM4;
    private PictureBox imgVRM3;
    private PictureBox imgVRM2;
    private PictureBox imgVRM1;
    private PictureBox imgVRM16;
    private PictureBox imgVRM15;
    private PictureBox imgVRM14;
    private PictureBox imgVRM13;
    private PictureBox imgVRM12;
    private PictureBox imgVRM11;
    private PictureBox imgVRM10;
    private PictureBox imgVRM9;
    private PictureBox imgVRM8;
    private PictureBox imgVRM7;
    private PictureBox imgVRM6;
    private PictureBox imgVRM5;
    private Button cmdSearch;
    private Label lblSearchIntsruction;
    private Label lblArrivalTime;
    private Label lblArrivalTimeHeader;
    private Timer DisplayTimer;
    private Button cmdPrevious;

    public frmSelectVehicle()
    {
      this.InitializeComponent();
      this.m_allImgOverview = new List<PictureBox>();
      this.m_allImgOverview.Add(this.imgOverview1);
      this.m_allImgOverview.Add(this.imgOverview2);
      this.m_allImgOverview.Add(this.imgOverview3);
      this.m_allImgOverview.Add(this.imgOverview4);
      this.m_allImgOverview.Add(this.imgOverview5);
      this.m_allImgOverview.Add(this.imgOverview6);
      this.m_allImgOverview.Add(this.imgOverview7);
      this.m_allImgOverview.Add(this.imgOverview8);
      this.m_allImgOverview.Add(this.imgOverview9);
      this.m_allImgOverview.Add(this.imgOverview10);
      this.m_allImgOverview.Add(this.imgOverview11);
      this.m_allImgOverview.Add(this.imgOverview12);
      this.m_allImgOverview.Add(this.imgOverview13);
      this.m_allImgOverview.Add(this.imgOverview14);
      this.m_allImgOverview.Add(this.imgOverview15);
      this.m_allImgOverview.Add(this.imgOverview16);
      this.m_allImgVRM = new List<PictureBox>();
      this.m_allImgVRM.Add(this.imgVRM1);
      this.m_allImgVRM.Add(this.imgVRM2);
      this.m_allImgVRM.Add(this.imgVRM3);
      this.m_allImgVRM.Add(this.imgVRM4);
      this.m_allImgVRM.Add(this.imgVRM5);
      this.m_allImgVRM.Add(this.imgVRM6);
      this.m_allImgVRM.Add(this.imgVRM7);
      this.m_allImgVRM.Add(this.imgVRM8);
      this.m_allImgVRM.Add(this.imgVRM9);
      this.m_allImgVRM.Add(this.imgVRM10);
      this.m_allImgVRM.Add(this.imgVRM11);
      this.m_allImgVRM.Add(this.imgVRM12);
      this.m_allImgVRM.Add(this.imgVRM13);
      this.m_allImgVRM.Add(this.imgVRM14);
      this.m_allImgVRM.Add(this.imgVRM15);
      this.m_allImgVRM.Add(this.imgVRM16);
      this.m_clsVehicleData = new CGetVehicleData();
    }

    private void frmSelectVehicle_Load(object sender, EventArgs e)
    {
      this.TryToSetBackgroundImage();
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

    public void initializeData()
    {
      this.m_allVehicleInfo = new CCacheData().GetVehicleInfoListFromXmlDataTable(DateTime.Now.ToString(), "HISTORY");
      this.ShowSomeCars();
    }

    private void ShowSomeCars()
    {
      if (this.m_allVehicleInfo.Count > 0)
      {
        this.UpdateImages();
        this.GetTimeParameters();
        this.ChangeTimeDisplay(this.m_StartTime.ToShortTimeString(), this.m_EndTime.ToShortTimeString());
      }
      else
        this.cmdSearch_Click((object) null, (EventArgs) null);
    }

    private void cmdSearch_Click(object sender, EventArgs e)
    {
      this.m_FormActivity = true;
      frmSearchVehicle frmSearchVehicle = new frmSearchVehicle(new ParkingPermitInfo());
      int num = (int) frmSearchVehicle.ShowDialog();
      frmSearchVehicle.Close();
      frmSearchVehicle.Dispose();
      this.refreshImagesNext();
      this.m_FormActivity = false;
    }

    private void cmdPrevious_Click(object sender, EventArgs e)
    {
      this.DisplayTimer.Enabled = false;
      this.m_allVehicleInfo = new CCacheData().GetVehicleInfoListFromXmlDataTable(this.m_EndTime.ToString(), "HISTORY");
      if (this.m_allVehicleInfo.Count > 0)
      {
        this.UpdateImages();
        this.GetTimeParameters();
        this.ChangeTimeDisplay(this.m_StartTime.ToShortTimeString(), this.m_EndTime.ToShortTimeString());
      }
      this.DisplayTimer.Enabled = true;
    }

    private void cmdNext_Click(object sender, EventArgs e)
    {
      this.refreshImagesNext();
    }

    private void refreshImagesNext()
    {
      this.DisplayTimer.Enabled = false;
      if (this.m_StartTime < DateTime.Now.AddYears(-1))
        this.m_StartTime = DateTime.Now;
      this.m_allVehicleInfo = new CCacheData().GetVehicleInfoListFromXmlDataTable(this.m_StartTime.ToString(), "FUTURE");
      if (this.m_allVehicleInfo.Count < 16)
        this.m_allVehicleInfo = new CCacheData().GetVehicleInfoListFromXmlDataTable(DateTime.Now.ToString(), "HISTORY");
      this.UpdateImages();
      if (this.m_allVehicleInfo.Count > 0)
      {
        this.GetTimeParameters();
        this.ChangeTimeDisplay(this.m_StartTime.ToShortTimeString(), this.m_EndTime.ToShortTimeString());
      }
      else
        this.Close();
      this.DisplayTimer.Enabled = true;
    }

    private void UpdateImages()
    {
      Console.WriteLine();
      Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
      Console.WriteLine("Updating image selection...");
      Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
      for (int index = 0; index < 16; ++index)
      {
        this.m_allImgOverview[index].Visible = false;
        this.m_allImgVRM[index].Visible = false;
      }
      if (this.m_allVehicleInfo.Count > 0)
      {
        int index = 0;
        try
        {
          string str = string.Format("{0}\\", (object) Path.GetFullPath(ConfigurationManager.AppSettings["cacheImageLocation"]));
          Console.WriteLine("Cache image path : {0}", (object) str);
          foreach (CVehicleInfo carDataItem in this.m_allVehicleInfo)
          {
            if (!carDataItem.overviewImageLocation.Contains("\\"))
              carDataItem.overviewImageLocation = string.Format("{0}{1}", (object) str, (object) carDataItem.overviewImageLocation);
            this.m_allImgOverview[index].ImageLocation = carDataItem.overviewImageLocation;
            Console.WriteLine("Overview image set to : {0}", (object) carDataItem.overviewImageLocation);
            if (!carDataItem.plateImageLocation.Contains("\\"))
              carDataItem.plateImageLocation = string.Format("{0}{1}", (object) str, (object) carDataItem.plateImageLocation);
            Image image = Image.FromFile(carDataItem.plateImageLocation);
            Console.WriteLine("Plate image set to : {0}", (object) carDataItem.plateImageLocation);
            this.m_allImgVRM[index].Image = image;
            Utilites.DrawRegistrationMark(this.m_allImgVRM[index], carDataItem);
            this.m_allImgOverview[index].Tag = (object) carDataItem;
            this.m_allImgVRM[index].Tag = (object) carDataItem;
            this.m_allImgOverview[index].Visible = true;
            this.m_allImgVRM[index].Visible = true;
            ++index;
          }
        }
        catch (Exception ex)
        {
          Log.Write(ex);
        }
      }
      this.Refresh();
      Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
      Console.WriteLine("Image selection updated!");
      Console.WriteLine("/\\/\\/\\/\\/\\---------------------------------------------------/\\/\\/\\/\\/\\");
    }

    private void ChangeTimeDisplay(string timeStart, string timeEnd)
    {
      this.lblArrivalTime.Text = timeEnd + " - " + timeStart;
    }

    private void imgOverview_Click(object sender, EventArgs e)
    {
      this.m_FormActivity = true;
      this.DisplayTimer.Stop();
      this.DisplayTimer.Enabled = false;
      CVehicleInfo tag = (CVehicleInfo) ((Control) sender).Tag;
      frmConfirmVehicle frmConfirmVehicle = new frmConfirmVehicle(tag.overviewImageLocation, tag.plateImageLocation, new ParkingPermitInfo()
      {
        VehicleRegMark = tag.VRM,
        StartDate = DateTime.Parse(tag.inwardTime)
      });
      frmConfirmVehicle.VehicleInfo = tag;
      int num = (int) frmConfirmVehicle.ShowDialog();
      this.DisplayTimer.Enabled = true;
      this.DisplayTimer.Start();
      this.m_FormActivity = false;
      if (!frmConfirmVehicle.ConfirmVehicle)
        return;
      this.refreshImagesNext();
    }

    private void GetTimeParameters()
    {
      if (this.m_allVehicleInfo.Count > 0)
      {
        this.m_StartTime = DateTime.Parse(this.m_allVehicleInfo[0].inwardTime);
        this.m_EndTime = DateTime.Parse(this.m_allVehicleInfo[this.m_allVehicleInfo.Count - 1].inwardTime);
      }
      else
        this.m_StartTime = this.m_EndTime = DateTime.Now;
    }

    private void DisplayTimer_Tick(object sender, EventArgs e)
    {
      if (this.m_FormActivity)
        return;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmSelectVehicle));
      this.cmdPrevious = new Button();
      this.lblInstructionHeader = new Label();
      this.imgLogo = new PictureBox();
      this.pnlImageOverview = new Panel();
      this.imgVRM1 = new PictureBox();
      this.imgVRM2 = new PictureBox();
      this.imgVRM3 = new PictureBox();
      this.imgVRM4 = new PictureBox();
      this.imgVRM5 = new PictureBox();
      this.imgVRM6 = new PictureBox();
      this.imgVRM7 = new PictureBox();
      this.imgVRM8 = new PictureBox();
      this.imgVRM9 = new PictureBox();
      this.imgVRM10 = new PictureBox();
      this.imgVRM11 = new PictureBox();
      this.imgVRM12 = new PictureBox();
      this.imgVRM13 = new PictureBox();
      this.imgVRM14 = new PictureBox();
      this.imgVRM15 = new PictureBox();
      this.imgVRM16 = new PictureBox();
      this.imgOverview1 = new PictureBox();
      this.imgOverview2 = new PictureBox();
      this.imgOverview3 = new PictureBox();
      this.imgOverview4 = new PictureBox();
      this.imgOverview5 = new PictureBox();
      this.imgOverview6 = new PictureBox();
      this.imgOverview7 = new PictureBox();
      this.imgOverview8 = new PictureBox();
      this.imgOverview9 = new PictureBox();
      this.imgOverview10 = new PictureBox();
      this.imgOverview11 = new PictureBox();
      this.imgOverview12 = new PictureBox();
      this.imgOverview13 = new PictureBox();
      this.imgOverview14 = new PictureBox();
      this.imgOverview15 = new PictureBox();
      this.imgOverview16 = new PictureBox();
      this.pnlTimeDisplay = new Panel();
      this.lblArrivalTime = new Label();
      this.lblArrivalTimeHeader = new Label();
      this.cmdNext = new Button();
      this.pnlSearch = new Panel();
      this.lblSearchIntsruction = new Label();
      this.cmdSearch = new Button();
      this.DisplayTimer = new Timer(this.components);
      ((ISupportInitialize) this.imgLogo).BeginInit();
      this.pnlImageOverview.SuspendLayout();
      ((ISupportInitialize) this.imgVRM1).BeginInit();
      ((ISupportInitialize) this.imgVRM2).BeginInit();
      ((ISupportInitialize) this.imgVRM3).BeginInit();
      ((ISupportInitialize) this.imgVRM4).BeginInit();
      ((ISupportInitialize) this.imgVRM5).BeginInit();
      ((ISupportInitialize) this.imgVRM6).BeginInit();
      ((ISupportInitialize) this.imgVRM7).BeginInit();
      ((ISupportInitialize) this.imgVRM8).BeginInit();
      ((ISupportInitialize) this.imgVRM9).BeginInit();
      ((ISupportInitialize) this.imgVRM10).BeginInit();
      ((ISupportInitialize) this.imgVRM11).BeginInit();
      ((ISupportInitialize) this.imgVRM12).BeginInit();
      ((ISupportInitialize) this.imgVRM13).BeginInit();
      ((ISupportInitialize) this.imgVRM14).BeginInit();
      ((ISupportInitialize) this.imgVRM15).BeginInit();
      ((ISupportInitialize) this.imgVRM16).BeginInit();
      ((ISupportInitialize) this.imgOverview1).BeginInit();
      ((ISupportInitialize) this.imgOverview2).BeginInit();
      ((ISupportInitialize) this.imgOverview3).BeginInit();
      ((ISupportInitialize) this.imgOverview4).BeginInit();
      ((ISupportInitialize) this.imgOverview5).BeginInit();
      ((ISupportInitialize) this.imgOverview6).BeginInit();
      ((ISupportInitialize) this.imgOverview7).BeginInit();
      ((ISupportInitialize) this.imgOverview8).BeginInit();
      ((ISupportInitialize) this.imgOverview9).BeginInit();
      ((ISupportInitialize) this.imgOverview10).BeginInit();
      ((ISupportInitialize) this.imgOverview11).BeginInit();
      ((ISupportInitialize) this.imgOverview12).BeginInit();
      ((ISupportInitialize) this.imgOverview13).BeginInit();
      ((ISupportInitialize) this.imgOverview14).BeginInit();
      ((ISupportInitialize) this.imgOverview15).BeginInit();
      ((ISupportInitialize) this.imgOverview16).BeginInit();
      this.pnlTimeDisplay.SuspendLayout();
      this.pnlSearch.SuspendLayout();
      this.SuspendLayout();
      this.cmdPrevious.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.cmdPrevious.BackColor = Color.Transparent;
      this.cmdPrevious.BackgroundImage = (Image) componentResourceManager.GetObject("cmdPrevious.BackgroundImage");
      this.cmdPrevious.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdPrevious.FlatAppearance.BorderSize = 0;
      this.cmdPrevious.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdPrevious.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdPrevious.FlatStyle = FlatStyle.Flat;
      this.cmdPrevious.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdPrevious.ForeColor = Color.Gold;
      this.cmdPrevious.Location = new Point(5, 6);
      this.cmdPrevious.Name = "cmdPrevious";
      this.cmdPrevious.Size = new Size(142, 45);
      this.cmdPrevious.TabIndex = 0;
      this.cmdPrevious.Text = "Previous";
      this.cmdPrevious.UseVisualStyleBackColor = false;
      this.cmdPrevious.Click += new EventHandler(this.cmdPrevious_Click);
      this.lblInstructionHeader.BackColor = SystemColors.Desktop;
      this.lblInstructionHeader.Dock = DockStyle.Top;
      this.lblInstructionHeader.Font = new Font("Arial", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblInstructionHeader.ForeColor = Color.Gold;
      this.lblInstructionHeader.Location = new Point(0, 0);
      this.lblInstructionHeader.Name = "lblInstructionHeader";
      this.lblInstructionHeader.Size = new Size(800, 40);
      this.lblInstructionHeader.TabIndex = 0;
      this.lblInstructionHeader.Text = "PLEASE TOUCH YOUR VEHICLE";
      this.lblInstructionHeader.TextAlign = ContentAlignment.MiddleLeft;
      this.imgLogo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.imgLogo.BackgroundImageLayout = ImageLayout.None;
      this.imgLogo.Image = (Image) Resources.RangerLogo;
      this.imgLogo.Location = new Point(748, 0);
      this.imgLogo.Name = "imgLogo";
      this.imgLogo.Size = new Size(40, 40);
      this.imgLogo.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgLogo.TabIndex = 1;
      this.imgLogo.TabStop = false;
      this.imgLogo.Visible = false;
      this.pnlImageOverview.BackColor = Color.Transparent;
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM1);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM2);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM3);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM4);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM5);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM6);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM7);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM8);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM9);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM10);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM11);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM12);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM13);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM14);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM15);
      this.pnlImageOverview.Controls.Add((Control) this.imgVRM16);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview1);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview2);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview3);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview4);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview5);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview6);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview7);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview8);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview9);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview10);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview11);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview12);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview13);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview14);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview15);
      this.pnlImageOverview.Controls.Add((Control) this.imgOverview16);
      this.pnlImageOverview.Location = new Point(0, 45);
      this.pnlImageOverview.Name = "pnlImageOverview";
      this.pnlImageOverview.Size = new Size(570, 495);
      this.pnlImageOverview.TabIndex = 2;
      this.imgVRM1.BackColor = Color.Transparent;
      this.imgVRM1.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM1.ErrorImage = (Image) null;
      this.imgVRM1.InitialImage = (Image) null;
      this.imgVRM1.Location = new Point(10, 100);
      this.imgVRM1.Name = "imgVRM1";
      this.imgVRM1.Size = new Size(130, 25);
      this.imgVRM1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM1.TabIndex = 40;
      this.imgVRM1.TabStop = false;
      this.imgVRM1.Visible = false;
      this.imgVRM1.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM2.BackColor = Color.Transparent;
      this.imgVRM2.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM2.ErrorImage = (Image) null;
      this.imgVRM2.InitialImage = (Image) null;
      this.imgVRM2.Location = new Point(150, 100);
      this.imgVRM2.Name = "imgVRM2";
      this.imgVRM2.Size = new Size(130, 25);
      this.imgVRM2.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM2.TabIndex = 41;
      this.imgVRM2.TabStop = false;
      this.imgVRM2.Visible = false;
      this.imgVRM2.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM3.BackColor = Color.Transparent;
      this.imgVRM3.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM3.ErrorImage = (Image) null;
      this.imgVRM3.InitialImage = (Image) null;
      this.imgVRM3.Location = new Point(290, 100);
      this.imgVRM3.Name = "imgVRM3";
      this.imgVRM3.Size = new Size(130, 25);
      this.imgVRM3.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM3.TabIndex = 42;
      this.imgVRM3.TabStop = false;
      this.imgVRM3.Visible = false;
      this.imgVRM3.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM4.BackColor = Color.Transparent;
      this.imgVRM4.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM4.ErrorImage = (Image) null;
      this.imgVRM4.InitialImage = (Image) null;
      this.imgVRM4.Location = new Point(430, 100);
      this.imgVRM4.Name = "imgVRM4";
      this.imgVRM4.Size = new Size(130, 25);
      this.imgVRM4.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM4.TabIndex = 43;
      this.imgVRM4.TabStop = false;
      this.imgVRM4.Visible = false;
      this.imgVRM4.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM5.BackColor = Color.Transparent;
      this.imgVRM5.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM5.ErrorImage = (Image) null;
      this.imgVRM5.InitialImage = (Image) null;
      this.imgVRM5.Location = new Point(10, 220);
      this.imgVRM5.Name = "imgVRM5";
      this.imgVRM5.Size = new Size(130, 25);
      this.imgVRM5.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM5.TabIndex = 44;
      this.imgVRM5.TabStop = false;
      this.imgVRM5.Visible = false;
      this.imgVRM5.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM6.BackColor = Color.Transparent;
      this.imgVRM6.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM6.ErrorImage = (Image) null;
      this.imgVRM6.InitialImage = (Image) null;
      this.imgVRM6.Location = new Point(150, 220);
      this.imgVRM6.Name = "imgVRM6";
      this.imgVRM6.Size = new Size(130, 25);
      this.imgVRM6.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM6.TabIndex = 45;
      this.imgVRM6.TabStop = false;
      this.imgVRM6.Visible = false;
      this.imgVRM6.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM7.BackColor = Color.Transparent;
      this.imgVRM7.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM7.ErrorImage = (Image) null;
      this.imgVRM7.InitialImage = (Image) null;
      this.imgVRM7.Location = new Point(290, 220);
      this.imgVRM7.Name = "imgVRM7";
      this.imgVRM7.Size = new Size(130, 25);
      this.imgVRM7.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM7.TabIndex = 46;
      this.imgVRM7.TabStop = false;
      this.imgVRM7.Visible = false;
      this.imgVRM7.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM8.BackColor = Color.Transparent;
      this.imgVRM8.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM8.ErrorImage = (Image) null;
      this.imgVRM8.InitialImage = (Image) null;
      this.imgVRM8.Location = new Point(430, 220);
      this.imgVRM8.Name = "imgVRM8";
      this.imgVRM8.Size = new Size(130, 25);
      this.imgVRM8.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM8.TabIndex = 47;
      this.imgVRM8.TabStop = false;
      this.imgVRM8.Visible = false;
      this.imgVRM8.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM9.BackColor = Color.Transparent;
      this.imgVRM9.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM9.ErrorImage = (Image) null;
      this.imgVRM9.InitialImage = (Image) null;
      this.imgVRM9.Location = new Point(10, 340);
      this.imgVRM9.Name = "imgVRM9";
      this.imgVRM9.Size = new Size(130, 25);
      this.imgVRM9.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM9.TabIndex = 48;
      this.imgVRM9.TabStop = false;
      this.imgVRM9.Visible = false;
      this.imgVRM9.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM10.BackColor = Color.Transparent;
      this.imgVRM10.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM10.ErrorImage = (Image) null;
      this.imgVRM10.InitialImage = (Image) null;
      this.imgVRM10.Location = new Point(150, 340);
      this.imgVRM10.Name = "imgVRM10";
      this.imgVRM10.Size = new Size(130, 25);
      this.imgVRM10.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM10.TabIndex = 49;
      this.imgVRM10.TabStop = false;
      this.imgVRM10.Visible = false;
      this.imgVRM10.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM11.BackColor = Color.Transparent;
      this.imgVRM11.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM11.ErrorImage = (Image) null;
      this.imgVRM11.InitialImage = (Image) null;
      this.imgVRM11.Location = new Point(290, 340);
      this.imgVRM11.Name = "imgVRM11";
      this.imgVRM11.Size = new Size(130, 25);
      this.imgVRM11.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM11.TabIndex = 50;
      this.imgVRM11.TabStop = false;
      this.imgVRM11.Visible = false;
      this.imgVRM11.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM12.BackColor = Color.Transparent;
      this.imgVRM12.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM12.ErrorImage = (Image) null;
      this.imgVRM12.InitialImage = (Image) null;
      this.imgVRM12.Location = new Point(430, 340);
      this.imgVRM12.Name = "imgVRM12";
      this.imgVRM12.Size = new Size(130, 25);
      this.imgVRM12.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM12.TabIndex = 51;
      this.imgVRM12.TabStop = false;
      this.imgVRM12.Visible = false;
      this.imgVRM12.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM13.BackColor = Color.Transparent;
      this.imgVRM13.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM13.ErrorImage = (Image) null;
      this.imgVRM13.InitialImage = (Image) null;
      this.imgVRM13.Location = new Point(10, 460);
      this.imgVRM13.Name = "imgVRM13";
      this.imgVRM13.Size = new Size(130, 25);
      this.imgVRM13.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM13.TabIndex = 52;
      this.imgVRM13.TabStop = false;
      this.imgVRM13.Visible = false;
      this.imgVRM13.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM14.BackColor = Color.Transparent;
      this.imgVRM14.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM14.ErrorImage = (Image) null;
      this.imgVRM14.InitialImage = (Image) null;
      this.imgVRM14.Location = new Point(150, 460);
      this.imgVRM14.Name = "imgVRM14";
      this.imgVRM14.Size = new Size(130, 25);
      this.imgVRM14.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM14.TabIndex = 53;
      this.imgVRM14.TabStop = false;
      this.imgVRM14.Visible = false;
      this.imgVRM14.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM15.BackColor = Color.Transparent;
      this.imgVRM15.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM15.ErrorImage = (Image) null;
      this.imgVRM15.InitialImage = (Image) null;
      this.imgVRM15.Location = new Point(290, 460);
      this.imgVRM15.Name = "imgVRM15";
      this.imgVRM15.Size = new Size(130, 25);
      this.imgVRM15.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM15.TabIndex = 54;
      this.imgVRM15.TabStop = false;
      this.imgVRM15.Visible = false;
      this.imgVRM15.Click += new EventHandler(this.imgOverview_Click);
      this.imgVRM16.BackColor = Color.Transparent;
      this.imgVRM16.BorderStyle = BorderStyle.FixedSingle;
      this.imgVRM16.ErrorImage = (Image) null;
      this.imgVRM16.InitialImage = (Image) null;
      this.imgVRM16.Location = new Point(430, 460);
      this.imgVRM16.Name = "imgVRM16";
      this.imgVRM16.Size = new Size(130, 25);
      this.imgVRM16.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgVRM16.TabIndex = 55;
      this.imgVRM16.TabStop = false;
      this.imgVRM16.Visible = false;
      this.imgVRM16.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview1.BackColor = Color.Transparent;
      this.imgOverview1.BackgroundImageLayout = ImageLayout.None;
      this.imgOverview1.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview1.ErrorImage = (Image) null;
      this.imgOverview1.InitialImage = (Image) null;
      this.imgOverview1.Location = new Point(10, 10);
      this.imgOverview1.Name = "imgOverview1";
      this.imgOverview1.Size = new Size(130, 90);
      this.imgOverview1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview1.TabIndex = 16;
      this.imgOverview1.TabStop = false;
      this.imgOverview1.Visible = false;
      this.imgOverview1.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview2.BackColor = Color.Transparent;
      this.imgOverview2.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview2.ErrorImage = (Image) null;
      this.imgOverview2.InitialImage = (Image) null;
      this.imgOverview2.Location = new Point(150, 10);
      this.imgOverview2.Name = "imgOverview2";
      this.imgOverview2.Size = new Size(130, 90);
      this.imgOverview2.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview2.TabIndex = 18;
      this.imgOverview2.TabStop = false;
      this.imgOverview2.Visible = false;
      this.imgOverview2.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview3.BackColor = Color.Transparent;
      this.imgOverview3.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview3.ErrorImage = (Image) null;
      this.imgOverview3.InitialImage = (Image) null;
      this.imgOverview3.Location = new Point(290, 10);
      this.imgOverview3.Name = "imgOverview3";
      this.imgOverview3.Size = new Size(130, 90);
      this.imgOverview3.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview3.TabIndex = 19;
      this.imgOverview3.TabStop = false;
      this.imgOverview3.Visible = false;
      this.imgOverview3.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview4.BackColor = Color.Transparent;
      this.imgOverview4.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview4.ErrorImage = (Image) null;
      this.imgOverview4.InitialImage = (Image) null;
      this.imgOverview4.Location = new Point(430, 10);
      this.imgOverview4.Name = "imgOverview4";
      this.imgOverview4.Size = new Size(130, 90);
      this.imgOverview4.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview4.TabIndex = 20;
      this.imgOverview4.TabStop = false;
      this.imgOverview4.Visible = false;
      this.imgOverview4.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview5.BackColor = Color.Transparent;
      this.imgOverview5.BackgroundImageLayout = ImageLayout.None;
      this.imgOverview5.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview5.ErrorImage = (Image) null;
      this.imgOverview5.InitialImage = (Image) null;
      this.imgOverview5.Location = new Point(10, 130);
      this.imgOverview5.Name = "imgOverview5";
      this.imgOverview5.Size = new Size(130, 90);
      this.imgOverview5.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview5.TabIndex = 24;
      this.imgOverview5.TabStop = false;
      this.imgOverview5.Visible = false;
      this.imgOverview5.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview6.BackColor = Color.Transparent;
      this.imgOverview6.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview6.ErrorImage = (Image) null;
      this.imgOverview6.InitialImage = (Image) null;
      this.imgOverview6.Location = new Point(150, 130);
      this.imgOverview6.Name = "imgOverview6";
      this.imgOverview6.Size = new Size(130, 90);
      this.imgOverview6.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview6.TabIndex = 25;
      this.imgOverview6.TabStop = false;
      this.imgOverview6.Visible = false;
      this.imgOverview6.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview7.BackColor = Color.Transparent;
      this.imgOverview7.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview7.ErrorImage = (Image) null;
      this.imgOverview7.InitialImage = (Image) null;
      this.imgOverview7.Location = new Point(290, 130);
      this.imgOverview7.Name = "imgOverview7";
      this.imgOverview7.Size = new Size(130, 90);
      this.imgOverview7.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview7.TabIndex = 26;
      this.imgOverview7.TabStop = false;
      this.imgOverview7.Visible = false;
      this.imgOverview7.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview8.BackColor = Color.Transparent;
      this.imgOverview8.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview8.ErrorImage = (Image) null;
      this.imgOverview8.InitialImage = (Image) null;
      this.imgOverview8.Location = new Point(430, 130);
      this.imgOverview8.Name = "imgOverview8";
      this.imgOverview8.Size = new Size(130, 90);
      this.imgOverview8.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview8.TabIndex = 27;
      this.imgOverview8.TabStop = false;
      this.imgOverview8.Visible = false;
      this.imgOverview8.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview9.BackColor = Color.Transparent;
      this.imgOverview9.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview9.ErrorImage = (Image) null;
      this.imgOverview9.InitialImage = (Image) null;
      this.imgOverview9.Location = new Point(10, 250);
      this.imgOverview9.Name = "imgOverview9";
      this.imgOverview9.Size = new Size(130, 90);
      this.imgOverview9.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview9.TabIndex = 32;
      this.imgOverview9.TabStop = false;
      this.imgOverview9.Visible = false;
      this.imgOverview9.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview10.BackColor = Color.Transparent;
      this.imgOverview10.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview10.ErrorImage = (Image) null;
      this.imgOverview10.InitialImage = (Image) null;
      this.imgOverview10.Location = new Point(150, 250);
      this.imgOverview10.Name = "imgOverview10";
      this.imgOverview10.Size = new Size(130, 90);
      this.imgOverview10.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview10.TabIndex = 33;
      this.imgOverview10.TabStop = false;
      this.imgOverview10.Visible = false;
      this.imgOverview10.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview11.BackColor = Color.Transparent;
      this.imgOverview11.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview11.ErrorImage = (Image) null;
      this.imgOverview11.InitialImage = (Image) null;
      this.imgOverview11.Location = new Point(290, 250);
      this.imgOverview11.Name = "imgOverview11";
      this.imgOverview11.Size = new Size(130, 90);
      this.imgOverview11.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview11.TabIndex = 34;
      this.imgOverview11.TabStop = false;
      this.imgOverview11.Visible = false;
      this.imgOverview11.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview12.BackColor = Color.Transparent;
      this.imgOverview12.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview12.ErrorImage = (Image) null;
      this.imgOverview12.InitialImage = (Image) null;
      this.imgOverview12.Location = new Point(430, 250);
      this.imgOverview12.Name = "imgOverview12";
      this.imgOverview12.Size = new Size(130, 90);
      this.imgOverview12.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview12.TabIndex = 35;
      this.imgOverview12.TabStop = false;
      this.imgOverview12.Visible = false;
      this.imgOverview12.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview13.BackColor = Color.Transparent;
      this.imgOverview13.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview13.ErrorImage = (Image) null;
      this.imgOverview13.InitialImage = (Image) null;
      this.imgOverview13.Location = new Point(10, 370);
      this.imgOverview13.Name = "imgOverview13";
      this.imgOverview13.Size = new Size(130, 90);
      this.imgOverview13.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview13.TabIndex = 36;
      this.imgOverview13.TabStop = false;
      this.imgOverview13.Visible = false;
      this.imgOverview13.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview14.BackColor = Color.Transparent;
      this.imgOverview14.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview14.ErrorImage = (Image) null;
      this.imgOverview14.InitialImage = (Image) null;
      this.imgOverview14.Location = new Point(150, 370);
      this.imgOverview14.Name = "imgOverview14";
      this.imgOverview14.Size = new Size(130, 90);
      this.imgOverview14.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview14.TabIndex = 37;
      this.imgOverview14.TabStop = false;
      this.imgOverview14.Visible = false;
      this.imgOverview14.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview15.BackColor = Color.Transparent;
      this.imgOverview15.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview15.ErrorImage = (Image) null;
      this.imgOverview15.InitialImage = (Image) null;
      this.imgOverview15.Location = new Point(290, 370);
      this.imgOverview15.Name = "imgOverview15";
      this.imgOverview15.Size = new Size(130, 90);
      this.imgOverview15.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview15.TabIndex = 38;
      this.imgOverview15.TabStop = false;
      this.imgOverview15.Visible = false;
      this.imgOverview15.Click += new EventHandler(this.imgOverview_Click);
      this.imgOverview16.BackColor = Color.Transparent;
      this.imgOverview16.BorderStyle = BorderStyle.FixedSingle;
      this.imgOverview16.ErrorImage = (Image) null;
      this.imgOverview16.InitialImage = (Image) null;
      this.imgOverview16.Location = new Point(430, 370);
      this.imgOverview16.Name = "imgOverview16";
      this.imgOverview16.Size = new Size(130, 90);
      this.imgOverview16.SizeMode = PictureBoxSizeMode.StretchImage;
      this.imgOverview16.TabIndex = 39;
      this.imgOverview16.TabStop = false;
      this.imgOverview16.Visible = false;
      this.imgOverview16.Click += new EventHandler(this.imgOverview_Click);
      this.pnlTimeDisplay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pnlTimeDisplay.BackColor = Color.Transparent;
      this.pnlTimeDisplay.Controls.Add((Control) this.lblArrivalTime);
      this.pnlTimeDisplay.Controls.Add((Control) this.lblArrivalTimeHeader);
      this.pnlTimeDisplay.Controls.Add((Control) this.cmdNext);
      this.pnlTimeDisplay.Controls.Add((Control) this.cmdPrevious);
      this.pnlTimeDisplay.Location = new Point(0, 545);
      this.pnlTimeDisplay.Name = "pnlTimeDisplay";
      this.pnlTimeDisplay.Size = new Size(800, 55);
      this.pnlTimeDisplay.TabIndex = 3;
      this.lblArrivalTime.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.lblArrivalTime.BackColor = Color.Transparent;
      this.lblArrivalTime.Font = new Font("Arial", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblArrivalTime.ForeColor = Color.Ivory;
      this.lblArrivalTime.Location = new Point(200, 28);
      this.lblArrivalTime.Name = "lblArrivalTime";
      this.lblArrivalTime.Size = new Size(400, 25);
      this.lblArrivalTime.TabIndex = 6;
      this.lblArrivalTime.Text = "HH:MM  -  HH:MM";
      this.lblArrivalTime.TextAlign = ContentAlignment.MiddleCenter;
      this.lblArrivalTimeHeader.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.lblArrivalTimeHeader.BackColor = Color.Transparent;
      this.lblArrivalTimeHeader.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblArrivalTimeHeader.ForeColor = Color.Ivory;
      this.lblArrivalTimeHeader.Location = new Point(200, 14);
      this.lblArrivalTimeHeader.Name = "lblArrivalTimeHeader";
      this.lblArrivalTimeHeader.Size = new Size(400, 15);
      this.lblArrivalTimeHeader.TabIndex = 5;
      this.lblArrivalTimeHeader.Text = "Vehicles arriving between:";
      this.lblArrivalTimeHeader.TextAlign = ContentAlignment.BottomCenter;
      this.cmdNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.cmdNext.BackColor = Color.Transparent;
      this.cmdNext.BackgroundImage = (Image) componentResourceManager.GetObject("cmdNext.BackgroundImage");
      this.cmdNext.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdNext.FlatAppearance.BorderSize = 0;
      this.cmdNext.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdNext.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdNext.FlatStyle = FlatStyle.Flat;
      this.cmdNext.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdNext.ForeColor = Color.Gold;
      this.cmdNext.Location = new Point(653, 7);
      this.cmdNext.Name = "cmdNext";
      this.cmdNext.Size = new Size(142, 45);
      this.cmdNext.TabIndex = 1;
      this.cmdNext.Text = "Next";
      this.cmdNext.UseVisualStyleBackColor = false;
      this.cmdNext.Click += new EventHandler(this.cmdNext_Click);
      this.pnlSearch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.pnlSearch.BackColor = Color.Transparent;
      this.pnlSearch.Controls.Add((Control) this.lblSearchIntsruction);
      this.pnlSearch.Controls.Add((Control) this.cmdSearch);
      this.pnlSearch.Location = new Point(576, 45);
      this.pnlSearch.Name = "pnlSearch";
      this.pnlSearch.Size = new Size(224, 496);
      this.pnlSearch.TabIndex = 4;
      this.lblSearchIntsruction.BackColor = Color.Transparent;
      this.lblSearchIntsruction.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblSearchIntsruction.ForeColor = Color.Ivory;
      this.lblSearchIntsruction.Location = new Point(10, 50);
      this.lblSearchIntsruction.Name = "lblSearchIntsruction";
      this.lblSearchIntsruction.Size = new Size(200, 80);
      this.lblSearchIntsruction.TabIndex = 1;
      this.lblSearchIntsruction.Text = "If you cannot see your vehicle, please press Search";
      this.lblSearchIntsruction.TextAlign = ContentAlignment.MiddleCenter;
      this.cmdSearch.BackColor = Color.Transparent;
      this.cmdSearch.BackgroundImage = (Image) componentResourceManager.GetObject("cmdSearch.BackgroundImage");
      this.cmdSearch.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdSearch.FlatAppearance.BorderSize = 0;
      this.cmdSearch.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cmdSearch.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cmdSearch.FlatStyle = FlatStyle.Flat;
      this.cmdSearch.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdSearch.ForeColor = Color.Gold;
      this.cmdSearch.Location = new Point(45, 3);
      this.cmdSearch.Name = "cmdSearch";
      this.cmdSearch.Size = new Size(142, 45);
      this.cmdSearch.TabIndex = 0;
      this.cmdSearch.Text = "Search";
      this.cmdSearch.UseVisualStyleBackColor = false;
      this.cmdSearch.Click += new EventHandler(this.cmdSearch_Click);
      this.DisplayTimer.Enabled = true;
      this.DisplayTimer.Interval = 60000;
      this.DisplayTimer.Tick += new EventHandler(this.DisplayTimer_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.SteelBlue;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(800, 600);
      this.ControlBox = false;
      this.Controls.Add((Control) this.imgLogo);
      this.Controls.Add((Control) this.pnlImageOverview);
      this.Controls.Add((Control) this.pnlSearch);
      this.Controls.Add((Control) this.pnlTimeDisplay);
      this.Controls.Add((Control) this.lblInstructionHeader);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmSelectVehicle);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.frmSelectVehicle_Load);
      ((ISupportInitialize) this.imgLogo).EndInit();
      this.pnlImageOverview.ResumeLayout(false);
      ((ISupportInitialize) this.imgVRM1).EndInit();
      ((ISupportInitialize) this.imgVRM2).EndInit();
      ((ISupportInitialize) this.imgVRM3).EndInit();
      ((ISupportInitialize) this.imgVRM4).EndInit();
      ((ISupportInitialize) this.imgVRM5).EndInit();
      ((ISupportInitialize) this.imgVRM6).EndInit();
      ((ISupportInitialize) this.imgVRM7).EndInit();
      ((ISupportInitialize) this.imgVRM8).EndInit();
      ((ISupportInitialize) this.imgVRM9).EndInit();
      ((ISupportInitialize) this.imgVRM10).EndInit();
      ((ISupportInitialize) this.imgVRM11).EndInit();
      ((ISupportInitialize) this.imgVRM12).EndInit();
      ((ISupportInitialize) this.imgVRM13).EndInit();
      ((ISupportInitialize) this.imgVRM14).EndInit();
      ((ISupportInitialize) this.imgVRM15).EndInit();
      ((ISupportInitialize) this.imgVRM16).EndInit();
      ((ISupportInitialize) this.imgOverview1).EndInit();
      ((ISupportInitialize) this.imgOverview2).EndInit();
      ((ISupportInitialize) this.imgOverview3).EndInit();
      ((ISupportInitialize) this.imgOverview4).EndInit();
      ((ISupportInitialize) this.imgOverview5).EndInit();
      ((ISupportInitialize) this.imgOverview6).EndInit();
      ((ISupportInitialize) this.imgOverview7).EndInit();
      ((ISupportInitialize) this.imgOverview8).EndInit();
      ((ISupportInitialize) this.imgOverview9).EndInit();
      ((ISupportInitialize) this.imgOverview10).EndInit();
      ((ISupportInitialize) this.imgOverview11).EndInit();
      ((ISupportInitialize) this.imgOverview12).EndInit();
      ((ISupportInitialize) this.imgOverview13).EndInit();
      ((ISupportInitialize) this.imgOverview14).EndInit();
      ((ISupportInitialize) this.imgOverview15).EndInit();
      ((ISupportInitialize) this.imgOverview16).EndInit();
      this.pnlTimeDisplay.ResumeLayout(false);
      this.pnlSearch.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
