// Decompiled with JetBrains decompiler
// Type: TouchPark.frmStartScreen
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TouchPark.CardDevices;
using TouchPark.CashDevices;
using TouchPark.Properties;

namespace TouchPark
{
  public class frmStartScreen : Form
  {
    private string m_ApplicationType = string.Empty;
    private DateTime nextPermitUploadPermitDateTime = DateTime.MinValue;
    private DateTime nextHeartBeatPermitCreationDateTime = DateTime.MinValue;
    private DateTime nextProcessedPermitsPurge = DateTime.MinValue;
    private IContainer components;
    private System.Threading.Timer cachetimer;
    private Button messageButton;
    private ThreadStart method;
    private Thread _cachingThread;
    private frmSelectVehicle formSelect;
    private frmSearchVehicle formSearch;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmStartScreen));
      this.cachetimer = new System.Threading.Timer(new TimerCallback(this.cachetimer_Tick));
      this.messageButton = new Button();
      this.SuspendLayout();
      this.cachetimer.Change(60000, 60000);
      this.messageButton.Anchor = AnchorStyles.None;
      this.messageButton.BackColor = Color.Transparent;
      this.messageButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.messageButton.FlatAppearance.BorderSize = 0;
      this.messageButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.messageButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.messageButton.FlatStyle = FlatStyle.Flat;
      this.messageButton.Font = new Font("Arial", 21.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.messageButton.ForeColor = Color.Gold;
      this.messageButton.Location = new Point(99, 195);
      this.messageButton.Name = "messageButton";
      this.messageButton.Size = new Size(582, 201);
      this.messageButton.TabIndex = 0;
      this.messageButton.Text = "TOUCH HERE TO PAY FOR PARKING ";
      this.messageButton.UseVisualStyleBackColor = false;
      this.messageButton.MouseLeave += new EventHandler(this.cmdStart_MouseLeave);
      this.messageButton.Click += new EventHandler(this.cmdStart_Click);
      this.messageButton.MouseEnter += new EventHandler(this.cmdStart_MouseEnter);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Desktop;
      this.BackgroundImage = (Image) Resources.newBackground;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(792, 592);
      this.ControlBox = false;
      this.Controls.Add((Control) this.messageButton);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmStartScreen);
      this.RightToLeftLayout = true;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.WindowState = FormWindowState.Maximized;
      this.Load += new EventHandler(this.frmStartScreen_Load);
      this.Shown += new EventHandler(this.frmStartScreen_Shown);
      this.Click += new EventHandler(this.cmdStart_Click);
      this.FormClosing += new FormClosingEventHandler(this.frmStartScreen_FormClosing);
      this.ResumeLayout(false);
    }

    public frmStartScreen()
    {
      this.InitializeComponent();
      Cursor.Current = Cursors.Hand;
      this.m_ApplicationType = Settings.Default.ApplicationType;
      this.messageButton.Text = Settings.Default.StartScreenMessage;
    }

    private void TryToSetMessageForeColor()
    {
      try
      {
        this.messageButton.ForeColor = Settings.Default.StartScreenFontColour;
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    private void frmStartScreen_Load(object sender, EventArgs e)
    {
      this.TryToSetBackgroundImage();
      this.TryToSetMessageForeColor();
      Rectangle bounds = Screen.PrimaryScreen.Bounds;
      this.Size = new Size(800, 600);
      this.Location = new Point(0, 0);
      if (Settings.Default.MousePointerIsVisible)
        Cursor.Show();
      else
        Cursor.Hide();
      if (!Settings.Default.GenerateDemoVehicleDataOnStartup)
        return;
      this.GenerateDemoVehicleMovementData();
    }

    private void StartCachingThread()
    {
      Log.Write("Starting background caching thread");
      Console.WriteLine("Starting background caching thread");
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

    private void cmdStart_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.m_ApplicationType == "PermitPark" || this.m_ApplicationType == "PermitPark2")
          new frmStaffOrVistor().Show();
        else if (this.m_ApplicationType == "ServiceStationPark")
        {
          if (this.formSearch == null)
            this.formSearch = new frmSearchVehicle(new ParkingPermitInfo());
          if (this.formSearch.IsDisposed | this.formSearch.Disposing)
            this.formSearch = new frmSearchVehicle(new ParkingPermitInfo());
          int num = (int) this.formSearch.ShowDialog();
        }
        else
        {
          if (this.formSelect == null)
            this.formSelect = new frmSelectVehicle();
          if (this.formSelect.IsDisposed | this.formSelect.Disposing)
            this.formSelect = new frmSelectVehicle();
          if (!(this.formSelect != null & !this.formSelect.IsDisposed & !this.formSelect.Disposing))
            return;
          this.formSelect.initializeData();
          if (!(this.formSelect != null & !this.formSelect.IsDisposed & !this.formSelect.Disposing))
            return;
          int num = (int) this.formSelect.ShowDialog();
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    private void cmdStart_MouseEnter(object sender, EventArgs e)
    {
    }

    private void cmdStart_MouseLeave(object sender, EventArgs e)
    {
    }

    private void cachetimer_Tick(object state)
    {
      this.cachetimer.Change(-1, -1);
      Log.Write("Starting background caching thread");
      this.DoCacheWork();
      this.cachetimer.Change(60000, 60000);
    }

    private void frmStartScreen_Shown(object sender, EventArgs e)
    {
      Console.WriteLine("Start Shown.");
      this.OpenDevices();
      Console.WriteLine("Attempting to initialise caching...");
      if (!Settings.Default.PermitUploadAndCachingDisabled)
      {
        Log.Write("attemt start cacheing thread");
        this.StartCachingThread();
      }
      else
      {
        Log.Write("Caching not started.  Permit Upload & Caching Disabled.");
        Console.WriteLine("Caching not started.  Permit Upload & Caching Disabled.");
      }
    }

    private void frmStartScreen_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.messageButton.Text = "Closing, please wait...";
      this.Refresh();
      this.CloseDevices();
      Cursor.Show();
      this.WaitForCacheThreadToFinish();
    }

    private void DoCacheWork()
    {
      try
      {
        this.CreateCachedVehicleData_And_UserCodeData();
        this.CreateHeartBeatPermit_If_Due();
        this.UploadPermits_If_Due();
        this.OvernightCleanCacheFolders_If_Due();
        this.ProcessedPermitDeleteFolder_If_Due();
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    private void CreateCachedVehicleData_And_UserCodeData()
    {
      Console.Write("Writing data to cache...");
      Database.TryConnection();
      if (!Database.IsAccessible())
      {
        Log.Write("Database is not accessible, creating empty vehicle data and default user data cache.");
        Console.WriteLine("Database is not accessible, creating empty vehicle data and default user data cache.");
        CCacheData.CreateDefaultUserInfoDataCacheIfNotExists();
        CCacheData.CreateEmptyVehicleDataCacheIfNotExists();
      }
      else
      {
        Console.WriteLine("Database accessible, caching data");
        Log.Write("Database accessible, caching data");
        CCacheData.CacheVehicleDataFromDatabase();
        CCacheData.CacheUserInfoDataFromDatabase();
      }
    }

    private void CreateHeartBeatPermit_If_Due()
    {
      if (!(this.nextHeartBeatPermitCreationDateTime <= DateTime.Now))
        return;
      Console.WriteLine("_______");
      Console.WriteLine("/\\/\\/\\/\\ ****Creating Test Permit [Heartbeat] {0}**** /\\/\\/\\/\\", (object) DateTime.Now);
      CCacheData.CreateTestPermit();
      this.nextHeartBeatPermitCreationDateTime = DateTime.Now.AddHours(Settings.Default.TestPermitInterval);
      Console.WriteLine("Next HeartBeat: {0}", (object) this.nextHeartBeatPermitCreationDateTime.ToString());
      Console.WriteLine("_______");
    }

    private void UploadPermits_If_Due()
    {
      if (!(this.nextPermitUploadPermitDateTime <= DateTime.Now))
        return;
      Console.WriteLine("Attempting to upload permit files...");
      this.nextPermitUploadPermitDateTime = DateTime.Now.AddHours(Settings.Default.UploadPermitInterval);
      CCacheData.CreatePermitfile();
      CCacheData.UploadPermitFiles();
      Console.WriteLine("Upload attempt complete. Next attempt: {0}", (object) this.nextPermitUploadPermitDateTime.ToString());
    }

    private void OvernightCleanCacheFolders_If_Due()
    {
      if (!(DateTime.Now.TimeOfDay > new TimeSpan(3, 0, 0)) || !(DateTime.Now.TimeOfDay < new TimeSpan(4, 0, 0)))
        return;
      CCacheData.ClearCacheImages();
    }

    private void ProcessedPermitDeleteFolder_If_Due()
    {
      if (!(this.nextProcessedPermitsPurge >= DateTime.Now) || !(DateTime.Now.TimeOfDay > new TimeSpan(1, 0, 0)) || !(DateTime.Now.TimeOfDay < new TimeSpan(2, 0, 0)))
        return;
      CCacheData.DeleteProcessedPermitFolder();
      this.nextProcessedPermitsPurge = DateTime.Now.AddDays(5.0);
    }

    private void WaitForCacheThreadToFinish()
    {
      try
      {
        this.UseWaitCursor = true;
        this.cachetimer.Change(-1, -1);
        this.UseWaitCursor = false;
      }
      catch (Exception ex)
      {
      }
    }

    private void OpenDevices()
    {
      try
      {
        if (this.m_ApplicationType.ToUpper() == "TOUCHPARK")
        {
          this.UseWaitCursor = true;
          string text = this.messageButton.Text;
          this.messageButton.Text = "Initialising Payment Devices, Please Wait...";
          this.Refresh();
          Application.DoEvents();
          Devices.CoinMechanism = !Settings.Default.CoinMachineIsSimulated ? new CoinMechanism() : (CoinMechanism) new SimulatedCoinMechanism();
          if (Settings.Default.CardDeviceEnabled)
          {
            Devices.CardDevice = new CardDevice();
            Devices.CardDevice.DisplayWelcomeScreenMessage(Settings.Default.CardDeviceWelcomeMessage);
          }
          this.messageButton.Text = text;
          this.Refresh();
          Application.DoEvents();
          this.UseWaitCursor = false;
        }
        else
          Devices.CoinMechanism = (CoinMechanism) new SimulatedCoinMechanism();
      }
      catch (CashDeviceCouldNotBeFoundException ex)
      {
        Log.Write((Exception) ex);
        OutOfOrderForm outOfOrderForm = new OutOfOrderForm();
        outOfOrderForm.Refresh();
        Application.DoEvents();
        int num = (int) outOfOrderForm.ShowDialog();
      }
    }

    private void CloseDevices()
    {
      try
      {
        if (this.m_ApplicationType.ToUpper() == "TOUCHPARK")
        {
          if (Devices.CardDevice != null)
            Devices.CardDevice.CloseConnection();
          if (Devices.CoinMechanism == null)
            return;
          Devices.CoinMechanism = (CoinMechanism) null;
        }
        else
          Devices.CoinMechanism = (CoinMechanism) new SimulatedCoinMechanism();
      }
      catch (CashDeviceCouldNotBeFoundException ex)
      {
        Log.Write((Exception) ex);
        OutOfOrderForm outOfOrderForm = new OutOfOrderForm();
        outOfOrderForm.Refresh();
        Application.DoEvents();
        int num = (int) outOfOrderForm.ShowDialog();
      }
    }

    private void GenerateDemoVehicleMovementData()
    {
      VehicleMovementDataGenerator movementDataGenerator = new VehicleMovementDataGenerator();
      movementDataGenerator.ResetDatesAndTimes();
      movementDataGenerator.Save();
    }
  }
}
