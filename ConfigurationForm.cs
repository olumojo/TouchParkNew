// Decompiled with JetBrains decompiler
// Type: TouchPark.ConfigurationForm
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TouchPark.CardDevices;
using TouchPark.Properties;

namespace TouchPark
{
  public class ConfigurationForm : Form
  {
    private CardDeviceConfig _cardDeviceConfig = new CardDeviceConfig();
    private IContainer components;
    private CheckBox enableTariffCategoryCarCheckBox;
    private TabControl tabControl1;
    private TabPage tariffCategoriesTabPage;
    private TabPage tabPage2;
    private Panel panel1;
    private CheckBox enableTariffCategoryHGVCheckBox;
    private CheckBox enableTariffCategoryGuestCheckBox;
    private CheckBox enableTariffCategoryHGVFoodCheckBox;
    private CheckBox checkBox1;
    private CheckBox enableTariffCategoryStaffCheckBox;
    private CheckBox enableTariffCategoryVisitoCheckBox;
    private Panel panel2;
    private CheckBox cardDeviceEnabledCheckBox;
    private TextBox cardWelcomeMessageTextBox;
    private Label label1;
    private CheckBox coinMachineIsSimulatedCheckBox;
    private Panel panel3;
    private CheckBox generateDemoVehicleDataOnStartupCheckBox;
    private CheckBox useLivePermitUploadWebServiceCheckBox;
    private CheckBox paintVehicleRegistrationMarksCheckBox;
    private TabPage tabPage1;
    private Panel panel4;
    private CheckBox disableUserCodeCheckBox2;
    private CheckBox mousePointerIsVisibleCheckBox;
    private Label label2;
    private TextBox startScreenMessageTextTextBox;
    private TabPage userInterfaceTabPage;
    private Button button1;
    private ColorDialog colorDialog1;
    private TabPage tabPage4;
    private Panel panel5;
    private TextBox vehicleDataCaptureSiteTextBox;
    private Label label3;
    private CheckBox checkBox3;
    private TabPage tabPage5;
    private Panel panel6;
    private CheckBox permitUploadAndCachingDisabledCheckBox;
    private Label label5;
    private Label label4;
    private TextBox databaseConnectionStringTextBox;
    private Label label6;
    private Label label7;
    private TextBox textBox1;
    private Label label8;
    private Label label9;
    private ComboBox applicationTypeComboBox;
    private ComboBox comboBox1;
    private Label label10;
    private Label label11;
    private TextBox rangerTelephoneNumberTextBox;
    private Label label12;
    private Label label13;
    private Label label14;
    private TextBox startScreenBackgroundImagePathTextBox;
    private Button startScreenBackgroundImageButton;
    private OpenFileDialog openFileDialog1;
    private PictureBox startScreenBackgroundPictureBox;
    private PictureBox backgroundImagePictureBox;
    private TextBox backgroundImageTextBox;
    private Button backgroundImageButton;
    private Label label17;
    private Label label16;
    private Label label18;
    private Label label15;
    private Label label19;
    private CustomNumericUpDown customNumericUpDown1;
    private Label label20;
    private TextBox dummyUserCodeDataTextBox;
    private Button dummyUserCodeDataFilePathButton;
    private Label label21;
    private TextBox captureImageDirectoryPathTextBox;
    private Button captureImageDirectoryPathButton;
    private FolderBrowserDialog folderBrowserDialog1;
    private Label label22;
    private TextBox errorMessageTextBox;
    private TextBox cardDeviceConfigTransactionKeyTextBox;
    private TextBox cardDeviceConfigTransactionIDTextBox;
    private Label label23;
    private Label label26;
    private TextBox cardDevicePaymentURLTextBox;
    private Label label25;
    private Label label24;
    private CheckBox showConfigOnStartupCheckBox;
    private NumberBox numberBox1;
    private NumberBox numberBox4;
    private NumberBox numberBox3;
    private NumberBox numberBox2;
    private NumberBox numberBox5;
    private CheckBox checkBox2;
    private CheckBox checkBox4;
    private CheckBox checkBox5;
    private CheckBox enableNumberOfDaysChoiceCheckBox;
    private CheckBox checkBox6;

    public ConfigurationForm()
    {
      this.InitializeComponent();
      this.CreateCardDeviceConfigBindingSources();
      this.Text = string.Format("TouchPark [Version {0}] Configurator", (object) Application.ProductVersion);
    }

    private void CreateCardDeviceConfigBindingSources()
    {
      Binding binding1 = new Binding("Text", (object) this._cardDeviceConfig, "TransactionKey", true, DataSourceUpdateMode.OnPropertyChanged);
      Binding binding2 = new Binding("Text", (object) this._cardDeviceConfig, "TerminalID", true, DataSourceUpdateMode.OnPropertyChanged);
      Binding binding3 = new Binding("Text", (object) this._cardDeviceConfig, "PaymentURL", true, DataSourceUpdateMode.OnPropertyChanged);
      this.cardDeviceConfigTransactionKeyTextBox.DataBindings.Add(binding1);
      this.cardDeviceConfigTransactionIDTextBox.DataBindings.Add(binding2);
      this.cardDevicePaymentURLTextBox.DataBindings.Add(binding3);
    }

    private DialogResult OpenFileDialogFor(
      TextBox textBox,
      ConfigurationForm.OpenFileDialogFilter filter)
    {
      this.openFileDialog1.AddExtension = false;
      this.openFileDialog1.Multiselect = false;
      this.openFileDialog1.ValidateNames = true;
      switch (filter)
      {
        case ConfigurationForm.OpenFileDialogFilter.ImageFiles:
          this.openFileDialog1.Filter = "Image Files (*.bmp, *.jpg, *.png, *.tiff)|*.jpg;*.jpeg;*.png;*.tiff;*.bmp";
          break;
        case ConfigurationForm.OpenFileDialogFilter.XMLFiles:
          this.openFileDialog1.Filter = "XML Files (*.xml)|*.xml";
          break;
      }
      DialogResult dialogResult = this.openFileDialog1.ShowDialog((IWin32Window) this);
      if (dialogResult == DialogResult.OK)
        textBox.Text = this.openFileDialog1.FileName;
      return dialogResult;
    }

    private DialogResult OpenFolderBrowserDialog(string description)
    {
      this.folderBrowserDialog1.ShowNewFolderButton = true;
      this.folderBrowserDialog1.Description = description;
      return this.folderBrowserDialog1.ShowDialog((IWin32Window) this);
    }

    private void TryDrawStartScreenMessage(Graphics g)
    {
      try
      {
        Pen pen = new Pen(Settings.Default.StartScreenFontColour);
        Rectangle bounds = this.startScreenBackgroundPictureBox.Bounds;
        SizeF sizeF = g.MeasureString(this.startScreenMessageTextTextBox.Text, this.startScreenMessageTextTextBox.Font);
        PointF point = new PointF((float) (((double) bounds.Width - (double) sizeF.Width) / 2.0), (float) (((double) bounds.Height - (double) sizeF.Height) / 2.0));
        g.DrawString(this.startScreenMessageTextTextBox.Text, this.startScreenMessageTextTextBox.Font, pen.Brush, point);
      }
      catch (Exception ex)
      {
      }
    }

    private void startScreenMessageButton_Click(object sender, EventArgs e)
    {
      int num = (int) this.colorDialog1.ShowDialog((IWin32Window) this);
      Settings.Default.StartScreenFontColour = this.colorDialog1.Color;
      this.startScreenMessageTextTextBox.ForeColor = Settings.Default.StartScreenFontColour;
      this.startScreenBackgroundPictureBox.Invalidate();
    }

    private void ValidateComboBoxTextInList_TextUpdate(object sender, EventArgs e)
    {
      if (!(sender is ComboBox))
        return;
      ComboBox comboBox = (ComboBox) sender;
      if (comboBox.Items.Contains((object) comboBox.Text))
        return;
      comboBox.Text = comboBox.Items[0].ToString();
    }

    private void startScreenBackgroundImageButton_Click(object sender, EventArgs e)
    {
      if (this.OpenFileDialogFor(this.startScreenBackgroundImagePathTextBox, ConfigurationForm.OpenFileDialogFilter.ImageFiles) != DialogResult.OK)
        return;
      Settings.Default.StartScreenBackgroundImagePath = this.openFileDialog1.FileName;
      this.startScreenBackgroundPictureBox.ImageLocation = Settings.Default.StartScreenBackgroundImagePath;
    }

    private void startScreenBackgroundPictureBox_Paint(object sender, PaintEventArgs e)
    {
      this.TryDrawStartScreenMessage(e.Graphics);
    }

    private void dummyUserCodeDataFilePathButton_Click(object sender, EventArgs e)
    {
      int num = (int) this.OpenFileDialogFor(this.dummyUserCodeDataTextBox, ConfigurationForm.OpenFileDialogFilter.XMLFiles);
    }

    private void backgroundImageButton_Click(object sender, EventArgs e)
    {
      if (this.OpenFileDialogFor(this.backgroundImageTextBox, ConfigurationForm.OpenFileDialogFilter.ImageFiles) != DialogResult.OK)
        return;
      Settings.Default.BackgroundImagePath = this.openFileDialog1.FileName;
      this.backgroundImagePictureBox.ImageLocation = Settings.Default.BackgroundImagePath;
    }

    private void captureImageDirectoryPathButton_Click(object sender, EventArgs e)
    {
      int num = (int) this.OpenFolderBrowserDialog("Capture Image Directory Path");
    }

    private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      Settings.Default.Save();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConfigurationForm));
      this.tabControl1 = new TabControl();
      this.tabPage4 = new TabPage();
      this.panel5 = new Panel();
      this.label21 = new Label();
      this.captureImageDirectoryPathTextBox = new TextBox();
      this.captureImageDirectoryPathButton = new Button();
      this.label20 = new Label();
      this.dummyUserCodeDataTextBox = new TextBox();
      this.dummyUserCodeDataFilePathButton = new Button();
      this.rangerTelephoneNumberTextBox = new TextBox();
      this.label12 = new Label();
      this.comboBox1 = new ComboBox();
      this.applicationTypeComboBox = new ComboBox();
      this.label9 = new Label();
      this.label8 = new Label();
      this.label7 = new Label();
      this.textBox1 = new TextBox();
      this.label6 = new Label();
      this.databaseConnectionStringTextBox = new TextBox();
      this.label3 = new Label();
      this.vehicleDataCaptureSiteTextBox = new TextBox();
      this.tariffCategoriesTabPage = new TabPage();
      this.panel1 = new Panel();
      this.enableTariffCategoryVisitoCheckBox = new CheckBox();
      this.enableTariffCategoryStaffCheckBox = new CheckBox();
      this.checkBox1 = new CheckBox();
      this.enableTariffCategoryHGVFoodCheckBox = new CheckBox();
      this.enableTariffCategoryHGVCheckBox = new CheckBox();
      this.enableTariffCategoryGuestCheckBox = new CheckBox();
      this.enableTariffCategoryCarCheckBox = new CheckBox();
      this.tabPage2 = new TabPage();
      this.panel2 = new Panel();
      this.label26 = new Label();
      this.cardDevicePaymentURLTextBox = new TextBox();
      this.label25 = new Label();
      this.label24 = new Label();
      this.label23 = new Label();
      this.cardDeviceConfigTransactionIDTextBox = new TextBox();
      this.cardDeviceConfigTransactionKeyTextBox = new TextBox();
      this.coinMachineIsSimulatedCheckBox = new CheckBox();
      this.label1 = new Label();
      this.cardWelcomeMessageTextBox = new TextBox();
      this.cardDeviceEnabledCheckBox = new CheckBox();
      this.tabPage1 = new TabPage();
      this.panel3 = new Panel();
      this.checkBox3 = new CheckBox();
      this.paintVehicleRegistrationMarksCheckBox = new CheckBox();
      this.useLivePermitUploadWebServiceCheckBox = new CheckBox();
      this.generateDemoVehicleDataOnStartupCheckBox = new CheckBox();
      this.userInterfaceTabPage = new TabPage();
      this.panel4 = new Panel();
      this.showConfigOnStartupCheckBox = new CheckBox();
      this.label22 = new Label();
      this.errorMessageTextBox = new TextBox();
      this.label15 = new Label();
      this.label18 = new Label();
      this.label17 = new Label();
      this.label16 = new Label();
      this.backgroundImageTextBox = new TextBox();
      this.backgroundImageButton = new Button();
      this.backgroundImagePictureBox = new PictureBox();
      this.startScreenBackgroundPictureBox = new PictureBox();
      this.startScreenBackgroundImagePathTextBox = new TextBox();
      this.startScreenBackgroundImageButton = new Button();
      this.button1 = new Button();
      this.label2 = new Label();
      this.startScreenMessageTextTextBox = new TextBox();
      this.mousePointerIsVisibleCheckBox = new CheckBox();
      this.disableUserCodeCheckBox2 = new CheckBox();
      this.tabPage5 = new TabPage();
      this.panel6 = new Panel();
      this.label19 = new Label();
      this.label14 = new Label();
      this.label13 = new Label();
      this.label11 = new Label();
      this.label10 = new Label();
      this.label4 = new Label();
      this.label5 = new Label();
      this.permitUploadAndCachingDisabledCheckBox = new CheckBox();
      this.colorDialog1 = new ColorDialog();
      this.openFileDialog1 = new OpenFileDialog();
      this.folderBrowserDialog1 = new FolderBrowserDialog();
      this.checkBox2 = new CheckBox();
      this.checkBox4 = new CheckBox();
      this.checkBox5 = new CheckBox();
      this.enableNumberOfDaysChoiceCheckBox = new CheckBox();
      this.checkBox6 = new CheckBox();
      this.numberBox5 = new NumberBox();
      this.numberBox2 = new NumberBox();
      this.numberBox4 = new NumberBox();
      this.numberBox3 = new NumberBox();
      this.numberBox1 = new NumberBox();
      this.customNumericUpDown1 = new CustomNumericUpDown();
      this.tabControl1.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.panel5.SuspendLayout();
      this.tariffCategoriesTabPage.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.panel2.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.panel3.SuspendLayout();
      this.userInterfaceTabPage.SuspendLayout();
      this.panel4.SuspendLayout();
      ((ISupportInitialize) this.backgroundImagePictureBox).BeginInit();
      ((ISupportInitialize) this.startScreenBackgroundPictureBox).BeginInit();
      this.tabPage5.SuspendLayout();
      this.panel6.SuspendLayout();
      this.customNumericUpDown1.BeginInit();
      this.SuspendLayout();
      this.tabControl1.Controls.Add((Control) this.tabPage4);
      this.tabControl1.Controls.Add((Control) this.tariffCategoriesTabPage);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.userInterfaceTabPage);
      this.tabControl1.Controls.Add((Control) this.tabPage5);
      this.tabControl1.Dock = DockStyle.Fill;
      this.tabControl1.Location = new Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(416, 343);
      this.tabControl1.TabIndex = 0;
      this.tabPage4.BackColor = SystemColors.ControlLight;
      this.tabPage4.Controls.Add((Control) this.panel5);
      this.tabPage4.Location = new Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new Padding(3);
      this.tabPage4.Size = new Size(408, 317);
      this.tabPage4.TabIndex = 4;
      this.tabPage4.Text = "Application";
      this.tabPage4.UseVisualStyleBackColor = true;
      this.panel5.BorderStyle = BorderStyle.Fixed3D;
      this.panel5.Controls.Add((Control) this.label21);
      this.panel5.Controls.Add((Control) this.captureImageDirectoryPathTextBox);
      this.panel5.Controls.Add((Control) this.captureImageDirectoryPathButton);
      this.panel5.Controls.Add((Control) this.label20);
      this.panel5.Controls.Add((Control) this.dummyUserCodeDataTextBox);
      this.panel5.Controls.Add((Control) this.dummyUserCodeDataFilePathButton);
      this.panel5.Controls.Add((Control) this.rangerTelephoneNumberTextBox);
      this.panel5.Controls.Add((Control) this.label12);
      this.panel5.Controls.Add((Control) this.comboBox1);
      this.panel5.Controls.Add((Control) this.applicationTypeComboBox);
      this.panel5.Controls.Add((Control) this.label9);
      this.panel5.Controls.Add((Control) this.label8);
      this.panel5.Controls.Add((Control) this.label7);
      this.panel5.Controls.Add((Control) this.textBox1);
      this.panel5.Controls.Add((Control) this.label6);
      this.panel5.Controls.Add((Control) this.databaseConnectionStringTextBox);
      this.panel5.Controls.Add((Control) this.label3);
      this.panel5.Controls.Add((Control) this.vehicleDataCaptureSiteTextBox);
      this.panel5.Location = new Point(37, 6);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(337, 303);
      this.panel5.TabIndex = 0;
      this.label21.AutoSize = true;
      this.label21.Location = new Point(9, 184);
      this.label21.Name = "label21";
      this.label21.Size = new Size(137, 13);
      this.label21.TabIndex = 31;
      this.label21.Text = "CaptureImageDirectoryPath";
      this.captureImageDirectoryPathTextBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "CaptureImageDirectoryPath", true, DataSourceUpdateMode.OnPropertyChanged));
      this.captureImageDirectoryPathTextBox.Location = new Point(12, 200);
      this.captureImageDirectoryPathTextBox.Name = "captureImageDirectoryPathTextBox";
      this.captureImageDirectoryPathTextBox.Size = new Size(273, 20);
      this.captureImageDirectoryPathTextBox.TabIndex = 7;
      this.captureImageDirectoryPathTextBox.Text = Settings.Default.CaptureImageDirectoryPath;
      this.captureImageDirectoryPathButton.Location = new Point(291, 200);
      this.captureImageDirectoryPathButton.Name = "captureImageDirectoryPathButton";
      this.captureImageDirectoryPathButton.Size = new Size(37, 20);
      this.captureImageDirectoryPathButton.TabIndex = 8;
      this.captureImageDirectoryPathButton.Text = "...";
      this.captureImageDirectoryPathButton.UseVisualStyleBackColor = true;
      this.captureImageDirectoryPathButton.Click += new EventHandler(this.captureImageDirectoryPathButton_Click);
      this.label20.AutoSize = true;
      this.label20.Location = new Point(9, 145);
      this.label20.Name = "label20";
      this.label20.Size = new Size(128, 13);
      this.label20.TabIndex = 28;
      this.label20.Text = "DummyUserCodeDataFile";
      this.dummyUserCodeDataTextBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "DummyUserCodeDataFilePath", true, DataSourceUpdateMode.OnPropertyChanged));
      this.dummyUserCodeDataTextBox.Location = new Point(12, 161);
      this.dummyUserCodeDataTextBox.Name = "dummyUserCodeDataTextBox";
      this.dummyUserCodeDataTextBox.Size = new Size(273, 20);
      this.dummyUserCodeDataTextBox.TabIndex = 5;
      this.dummyUserCodeDataTextBox.Text = Settings.Default.DummyUserCodeDataFilePath;
      this.dummyUserCodeDataFilePathButton.Location = new Point(291, 161);
      this.dummyUserCodeDataFilePathButton.Name = "dummyUserCodeDataFilePathButton";
      this.dummyUserCodeDataFilePathButton.Size = new Size(37, 20);
      this.dummyUserCodeDataFilePathButton.TabIndex = 6;
      this.dummyUserCodeDataFilePathButton.Text = "...";
      this.dummyUserCodeDataFilePathButton.UseVisualStyleBackColor = true;
      this.dummyUserCodeDataFilePathButton.Click += new EventHandler(this.dummyUserCodeDataFilePathButton_Click);
      this.rangerTelephoneNumberTextBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "RangerTelephoneNumber", true, DataSourceUpdateMode.OnPropertyChanged));
      this.rangerTelephoneNumberTextBox.Location = new Point(145, 123);
      this.rangerTelephoneNumberTextBox.Name = "rangerTelephoneNumberTextBox";
      this.rangerTelephoneNumberTextBox.Size = new Size(183, 20);
      this.rangerTelephoneNumberTextBox.TabIndex = 4;
      this.rangerTelephoneNumberTextBox.Text = Settings.Default.RangerTelephoneNumber;
      this.label12.AutoSize = true;
      this.label12.Location = new Point(9, 126);
      this.label12.Name = "label12";
      this.label12.Size = new Size(130, 13);
      this.label12.TabIndex = 12;
      this.label12.Text = "RangerTelephoneNumber";
      this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.comboBox1.DataBindings.Add(new Binding("Text", (object) Settings.Default, "ThankYouCode", true, DataSourceUpdateMode.OnPropertyChanged));
      this.comboBox1.FlatStyle = FlatStyle.System;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[3]
      {
        (object) "TYNORMAL",
        (object) "TESCO",
        (object) "TRAVELODGE"
      });
      this.comboBox1.Location = new Point(129, 96);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(199, 21);
      this.comboBox1.TabIndex = 3;
      this.comboBox1.Text = Settings.Default.ThankYouCode;
      this.comboBox1.TextUpdate += new EventHandler(this.ValidateComboBoxTextInList_TextUpdate);
      this.applicationTypeComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
      this.applicationTypeComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.applicationTypeComboBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "ApplicationType", true, DataSourceUpdateMode.OnPropertyChanged));
      this.applicationTypeComboBox.FlatStyle = FlatStyle.System;
      this.applicationTypeComboBox.FormattingEnabled = true;
      this.applicationTypeComboBox.Items.AddRange(new object[4]
      {
        (object) "TouchPark",
        (object) "ServiceStationPark",
        (object) "PermitPark",
        (object) "PermitPark2"
      });
      this.applicationTypeComboBox.Location = new Point(129, 9);
      this.applicationTypeComboBox.Name = "applicationTypeComboBox";
      this.applicationTypeComboBox.Size = new Size(199, 21);
      this.applicationTypeComboBox.TabIndex = 0;
      this.applicationTypeComboBox.Text = Settings.Default.ApplicationType;
      this.applicationTypeComboBox.TextUpdate += new EventHandler(this.ValidateComboBoxTextInList_TextUpdate);
      this.label9.AutoSize = true;
      this.label9.Location = new Point(9, 99);
      this.label9.Name = "label9";
      this.label9.Size = new Size(82, 13);
      this.label9.TabIndex = 9;
      this.label9.Text = "ThankYouCode";
      this.label8.AutoSize = true;
      this.label8.Location = new Point(9, 10);
      this.label8.Name = "label8";
      this.label8.Size = new Size(83, 13);
      this.label8.TabIndex = 7;
      this.label8.Text = "ApplicationType";
      this.label7.AutoSize = true;
      this.label7.Location = new Point(9, 70);
      this.label7.Name = "label7";
      this.label7.Size = new Size(88, 13);
      this.label7.TabIndex = 5;
      this.label7.Text = "AuditReportCode";
      this.textBox1.DataBindings.Add(new Binding("Text", (object) Settings.Default, "AuditReportCode", true, DataSourceUpdateMode.OnPropertyChanged));
      this.textBox1.Location = new Point(129, 67);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(100, 20);
      this.textBox1.TabIndex = 2;
      this.textBox1.Text = Settings.Default.AuditReportCode;
      this.label6.AutoSize = true;
      this.label6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label6.Location = new Point(95, 223);
      this.label6.Name = "label6";
      this.label6.Size = new Size(137, 13);
      this.label6.TabIndex = 3;
      this.label6.Text = "Database connection string";
      this.databaseConnectionStringTextBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "DatabaseConnectionString", true, DataSourceUpdateMode.OnPropertyChanged));
      this.databaseConnectionStringTextBox.Location = new Point(9, 239);
      this.databaseConnectionStringTextBox.Multiline = true;
      this.databaseConnectionStringTextBox.Name = "databaseConnectionStringTextBox";
      this.databaseConnectionStringTextBox.Size = new Size(319, 57);
      this.databaseConnectionStringTextBox.TabIndex = 9;
      this.databaseConnectionStringTextBox.Text = Settings.Default.DatabaseConnectionString;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(9, 42);
      this.label3.Name = "label3";
      this.label3.Size = new Size(120, 13);
      this.label3.TabIndex = 1;
      this.label3.Text = "VehicleDataCaptureSite";
      this.vehicleDataCaptureSiteTextBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "VehicleDataCaptureSite", true, DataSourceUpdateMode.OnPropertyChanged));
      this.vehicleDataCaptureSiteTextBox.Location = new Point(129, 39);
      this.vehicleDataCaptureSiteTextBox.Name = "vehicleDataCaptureSiteTextBox";
      this.vehicleDataCaptureSiteTextBox.Size = new Size(199, 20);
      this.vehicleDataCaptureSiteTextBox.TabIndex = 1;
      this.vehicleDataCaptureSiteTextBox.Text = Settings.Default.VehicleDataCaptureSite;
      this.tariffCategoriesTabPage.BackColor = SystemColors.ControlLight;
      this.tariffCategoriesTabPage.Controls.Add((Control) this.panel1);
      this.tariffCategoriesTabPage.Location = new Point(4, 22);
      this.tariffCategoriesTabPage.Name = "tariffCategoriesTabPage";
      this.tariffCategoriesTabPage.Padding = new Padding(3);
      this.tariffCategoriesTabPage.Size = new Size(408, 317);
      this.tariffCategoriesTabPage.TabIndex = 0;
      this.tariffCategoriesTabPage.Text = "Tariff Categories";
      this.tariffCategoriesTabPage.UseVisualStyleBackColor = true;
      this.panel1.BorderStyle = BorderStyle.Fixed3D;
      this.panel1.Controls.Add((Control) this.enableTariffCategoryVisitoCheckBox);
      this.panel1.Controls.Add((Control) this.enableTariffCategoryStaffCheckBox);
      this.panel1.Controls.Add((Control) this.checkBox1);
      this.panel1.Controls.Add((Control) this.enableTariffCategoryHGVFoodCheckBox);
      this.panel1.Controls.Add((Control) this.enableTariffCategoryHGVCheckBox);
      this.panel1.Controls.Add((Control) this.enableTariffCategoryGuestCheckBox);
      this.panel1.Controls.Add((Control) this.enableTariffCategoryCarCheckBox);
      this.panel1.Location = new Point(112, 64);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(184, 175);
      this.panel1.TabIndex = 1;
      this.enableTariffCategoryVisitoCheckBox.AutoSize = true;
      this.enableTariffCategoryVisitoCheckBox.Checked = Settings.Default.EnableTariffCategoryVisitor;
      this.enableTariffCategoryVisitoCheckBox.CheckState = CheckState.Checked;
      this.enableTariffCategoryVisitoCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "EnableTariffCategoryVisitor", true, DataSourceUpdateMode.OnPropertyChanged));
      this.enableTariffCategoryVisitoCheckBox.Location = new Point(3, 145);
      this.enableTariffCategoryVisitoCheckBox.Name = "enableTariffCategoryVisitoCheckBox";
      this.enableTariffCategoryVisitoCheckBox.Size = new Size(153, 17);
      this.enableTariffCategoryVisitoCheckBox.TabIndex = 6;
      this.enableTariffCategoryVisitoCheckBox.Text = "EnableTariffCategoryVisitor";
      this.enableTariffCategoryVisitoCheckBox.UseVisualStyleBackColor = true;
      this.enableTariffCategoryStaffCheckBox.AutoSize = true;
      this.enableTariffCategoryStaffCheckBox.Checked = Settings.Default.EnableTariffCategoryStaff;
      this.enableTariffCategoryStaffCheckBox.CheckState = CheckState.Checked;
      this.enableTariffCategoryStaffCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "EnableTariffCategoryStaff", true, DataSourceUpdateMode.OnPropertyChanged));
      this.enableTariffCategoryStaffCheckBox.Location = new Point(3, 122);
      this.enableTariffCategoryStaffCheckBox.Name = "enableTariffCategoryStaffCheckBox";
      this.enableTariffCategoryStaffCheckBox.Size = new Size(147, 17);
      this.enableTariffCategoryStaffCheckBox.TabIndex = 5;
      this.enableTariffCategoryStaffCheckBox.Text = "EnableTariffCategoryStaff";
      this.enableTariffCategoryStaffCheckBox.UseVisualStyleBackColor = true;
      this.checkBox1.AutoSize = true;
      this.checkBox1.Checked = Settings.Default.EnableTariffCategoryHGVTrailer;
      this.checkBox1.CheckState = CheckState.Checked;
      this.checkBox1.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "EnableTariffCategoryHGVTrailer", true, DataSourceUpdateMode.OnPropertyChanged));
      this.checkBox1.Location = new Point(3, 76);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(177, 17);
      this.checkBox1.TabIndex = 3;
      this.checkBox1.Text = "EnableTariffCategoryHGVTrailer";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.enableTariffCategoryHGVFoodCheckBox.AutoSize = true;
      this.enableTariffCategoryHGVFoodCheckBox.Checked = Settings.Default.EnableTariffCategoryHGVFood;
      this.enableTariffCategoryHGVFoodCheckBox.CheckState = CheckState.Checked;
      this.enableTariffCategoryHGVFoodCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "EnableTariffCategoryHGVFood", true, DataSourceUpdateMode.OnPropertyChanged));
      this.enableTariffCategoryHGVFoodCheckBox.Location = new Point(3, 53);
      this.enableTariffCategoryHGVFoodCheckBox.Name = "enableTariffCategoryHGVFoodCheckBox";
      this.enableTariffCategoryHGVFoodCheckBox.Size = new Size(172, 17);
      this.enableTariffCategoryHGVFoodCheckBox.TabIndex = 2;
      this.enableTariffCategoryHGVFoodCheckBox.Text = "EnableTariffCategoryHGVFood";
      this.enableTariffCategoryHGVFoodCheckBox.UseVisualStyleBackColor = true;
      this.enableTariffCategoryHGVCheckBox.AutoSize = true;
      this.enableTariffCategoryHGVCheckBox.Checked = Settings.Default.EnableTariffCategoryHGV;
      this.enableTariffCategoryHGVCheckBox.CheckState = CheckState.Checked;
      this.enableTariffCategoryHGVCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "EnableTariffCategoryHGV", true, DataSourceUpdateMode.OnPropertyChanged));
      this.enableTariffCategoryHGVCheckBox.Location = new Point(3, 30);
      this.enableTariffCategoryHGVCheckBox.Name = "enableTariffCategoryHGVCheckBox";
      this.enableTariffCategoryHGVCheckBox.Size = new Size(148, 17);
      this.enableTariffCategoryHGVCheckBox.TabIndex = 1;
      this.enableTariffCategoryHGVCheckBox.Text = "EnableTariffCategoryHGV";
      this.enableTariffCategoryHGVCheckBox.UseVisualStyleBackColor = true;
      this.enableTariffCategoryGuestCheckBox.AutoSize = true;
      this.enableTariffCategoryGuestCheckBox.Checked = Settings.Default.EnableTariffCategoryGuest;
      this.enableTariffCategoryGuestCheckBox.CheckState = CheckState.Checked;
      this.enableTariffCategoryGuestCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "EnableTariffCategoryGuest", true, DataSourceUpdateMode.OnPropertyChanged));
      this.enableTariffCategoryGuestCheckBox.Location = new Point(3, 99);
      this.enableTariffCategoryGuestCheckBox.Name = "enableTariffCategoryGuestCheckBox";
      this.enableTariffCategoryGuestCheckBox.Size = new Size(153, 17);
      this.enableTariffCategoryGuestCheckBox.TabIndex = 4;
      this.enableTariffCategoryGuestCheckBox.Text = "EnableTariffCategoryGuest";
      this.enableTariffCategoryGuestCheckBox.UseVisualStyleBackColor = true;
      this.enableTariffCategoryCarCheckBox.AutoSize = true;
      this.enableTariffCategoryCarCheckBox.Checked = Settings.Default.EnableTariffCategoryCar;
      this.enableTariffCategoryCarCheckBox.CheckState = CheckState.Checked;
      this.enableTariffCategoryCarCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "EnableTariffCategoryCar", true, DataSourceUpdateMode.OnPropertyChanged));
      this.enableTariffCategoryCarCheckBox.Location = new Point(3, 7);
      this.enableTariffCategoryCarCheckBox.Name = "enableTariffCategoryCarCheckBox";
      this.enableTariffCategoryCarCheckBox.Size = new Size(141, 17);
      this.enableTariffCategoryCarCheckBox.TabIndex = 0;
      this.enableTariffCategoryCarCheckBox.Text = "EnableTariffCategoryCar";
      this.enableTariffCategoryCarCheckBox.UseVisualStyleBackColor = true;
      this.tabPage2.BackColor = SystemColors.ControlLight;
      this.tabPage2.Controls.Add((Control) this.panel2);
      this.tabPage2.Location = new Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(408, 317);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Devices";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.panel2.BorderStyle = BorderStyle.Fixed3D;
      this.panel2.Controls.Add((Control) this.label26);
      this.panel2.Controls.Add((Control) this.cardDevicePaymentURLTextBox);
      this.panel2.Controls.Add((Control) this.label25);
      this.panel2.Controls.Add((Control) this.label24);
      this.panel2.Controls.Add((Control) this.label23);
      this.panel2.Controls.Add((Control) this.cardDeviceConfigTransactionIDTextBox);
      this.panel2.Controls.Add((Control) this.cardDeviceConfigTransactionKeyTextBox);
      this.panel2.Controls.Add((Control) this.coinMachineIsSimulatedCheckBox);
      this.panel2.Controls.Add((Control) this.label1);
      this.panel2.Controls.Add((Control) this.cardWelcomeMessageTextBox);
      this.panel2.Controls.Add((Control) this.cardDeviceEnabledCheckBox);
      this.panel2.Location = new Point(20, 18);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(358, 280);
      this.panel2.TabIndex = 2;
      this.label26.AutoSize = true;
      this.label26.Location = new Point(0, 181);
      this.label26.Name = "label26";
      this.label26.Size = new Size(107, 13);
      this.label26.TabIndex = 17;
      this.label26.Text = "Payment Server URL";
      this.cardDevicePaymentURLTextBox.Location = new Point(112, 177);
      this.cardDevicePaymentURLTextBox.Name = "cardDevicePaymentURLTextBox";
      this.cardDevicePaymentURLTextBox.Size = new Size(226, 20);
      this.cardDevicePaymentURLTextBox.TabIndex = 3;
      this.label25.AutoSize = true;
      this.label25.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label25.Location = new Point(126, 161);
      this.label25.Name = "label25";
      this.label25.Size = new Size(131, 13);
      this.label25.TabIndex = 15;
      this.label25.Text = "Card Device Configuration";
      this.label24.AutoSize = true;
      this.label24.Location = new Point(11, 209);
      this.label24.Name = "label24";
      this.label24.Size = new Size(77, 13);
      this.label24.TabIndex = 14;
      this.label24.Text = " TransactionID";
      this.label23.AutoSize = true;
      this.label23.Location = new Point(10, 234);
      this.label23.Name = "label23";
      this.label23.Size = new Size(84, 13);
      this.label23.TabIndex = 13;
      this.label23.Text = " TransactionKey";
      this.cardDeviceConfigTransactionIDTextBox.Location = new Point(111, 205);
      this.cardDeviceConfigTransactionIDTextBox.Name = "cardDeviceConfigTransactionIDTextBox";
      this.cardDeviceConfigTransactionIDTextBox.Size = new Size(226, 20);
      this.cardDeviceConfigTransactionIDTextBox.TabIndex = 4;
      this.cardDeviceConfigTransactionKeyTextBox.Location = new Point(111, 231);
      this.cardDeviceConfigTransactionKeyTextBox.Name = "cardDeviceConfigTransactionKeyTextBox";
      this.cardDeviceConfigTransactionKeyTextBox.Size = new Size(226, 20);
      this.cardDeviceConfigTransactionKeyTextBox.TabIndex = 5;
      this.coinMachineIsSimulatedCheckBox.AutoSize = true;
      this.coinMachineIsSimulatedCheckBox.Checked = Settings.Default.CoinMachineIsSimulated;
      this.coinMachineIsSimulatedCheckBox.CheckState = CheckState.Checked;
      this.coinMachineIsSimulatedCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "CoinMachineIsSimulated", true, DataSourceUpdateMode.OnPropertyChanged));
      this.coinMachineIsSimulatedCheckBox.Location = new Point(129, 12);
      this.coinMachineIsSimulatedCheckBox.Name = "coinMachineIsSimulatedCheckBox";
      this.coinMachineIsSimulatedCheckBox.Size = new Size(142, 17);
      this.coinMachineIsSimulatedCheckBox.TabIndex = 0;
      this.coinMachineIsSimulatedCheckBox.Text = "CoinMachineIsSimulated";
      this.coinMachineIsSimulatedCheckBox.UseVisualStyleBackColor = true;
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(109, 55);
      this.label1.Name = "label1";
      this.label1.Size = new Size(160, 13);
      this.label1.TabIndex = 9;
      this.label1.Text = "Card Device Welcome Message";
      this.cardWelcomeMessageTextBox.BackColor = Color.YellowGreen;
      this.cardWelcomeMessageTextBox.BorderStyle = BorderStyle.FixedSingle;
      this.cardWelcomeMessageTextBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "CardDeviceWelcomeMessage", true, DataSourceUpdateMode.OnPropertyChanged));
      this.cardWelcomeMessageTextBox.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.cardWelcomeMessageTextBox.Location = new Point(119, 71);
      this.cardWelcomeMessageTextBox.Multiline = true;
      this.cardWelcomeMessageTextBox.Name = "cardWelcomeMessageTextBox";
      this.cardWelcomeMessageTextBox.Size = new Size(140, 62);
      this.cardWelcomeMessageTextBox.TabIndex = 2;
      this.cardWelcomeMessageTextBox.Text = Settings.Default.CardDeviceWelcomeMessage;
      this.cardWelcomeMessageTextBox.TextAlign = HorizontalAlignment.Center;
      this.cardDeviceEnabledCheckBox.AutoSize = true;
      this.cardDeviceEnabledCheckBox.Checked = Settings.Default.CardDeviceEnabled;
      this.cardDeviceEnabledCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "CardDeviceEnabled", true, DataSourceUpdateMode.OnPropertyChanged));
      this.cardDeviceEnabledCheckBox.Location = new Point(129, 35);
      this.cardDeviceEnabledCheckBox.Name = "cardDeviceEnabledCheckBox";
      this.cardDeviceEnabledCheckBox.Size = new Size(121, 17);
      this.cardDeviceEnabledCheckBox.TabIndex = 1;
      this.cardDeviceEnabledCheckBox.Text = "CardDeviceEnabled";
      this.cardDeviceEnabledCheckBox.UseVisualStyleBackColor = true;
      this.tabPage1.BackColor = SystemColors.ControlLight;
      this.tabPage1.Controls.Add((Control) this.panel3);
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(408, 317);
      this.tabPage1.TabIndex = 2;
      this.tabPage1.Text = "Demonstration";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.panel3.BorderStyle = BorderStyle.Fixed3D;
      this.panel3.Controls.Add((Control) this.checkBox4);
      this.panel3.Controls.Add((Control) this.checkBox2);
      this.panel3.Controls.Add((Control) this.checkBox3);
      this.panel3.Controls.Add((Control) this.paintVehicleRegistrationMarksCheckBox);
      this.panel3.Controls.Add((Control) this.useLivePermitUploadWebServiceCheckBox);
      this.panel3.Controls.Add((Control) this.generateDemoVehicleDataOnStartupCheckBox);
      this.panel3.Location = new Point(92, 70);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(231, 157);
      this.panel3.TabIndex = 3;
      this.checkBox3.AutoSize = true;
      this.checkBox3.Checked = Settings.Default.MousePointerIsVisible;
      this.checkBox3.CheckState = CheckState.Checked;
      this.checkBox3.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "MousePointerIsVisible", true, DataSourceUpdateMode.OnPropertyChanged));
      this.checkBox3.Location = new Point(3, 83);
      this.checkBox3.Name = "checkBox3";
      this.checkBox3.Size = new Size(129, 17);
      this.checkBox3.TabIndex = 3;
      this.checkBox3.Text = "MousePointerIsVisible";
      this.checkBox3.UseVisualStyleBackColor = true;
      this.paintVehicleRegistrationMarksCheckBox.AutoSize = true;
      this.paintVehicleRegistrationMarksCheckBox.Checked = Settings.Default.PaintVehicleRegistrationMarks;
      this.paintVehicleRegistrationMarksCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "PaintVehicleRegistrationMarks", true, DataSourceUpdateMode.OnPropertyChanged));
      this.paintVehicleRegistrationMarksCheckBox.Location = new Point(3, 37);
      this.paintVehicleRegistrationMarksCheckBox.Name = "paintVehicleRegistrationMarksCheckBox";
      this.paintVehicleRegistrationMarksCheckBox.Size = new Size(170, 17);
      this.paintVehicleRegistrationMarksCheckBox.TabIndex = 1;
      this.paintVehicleRegistrationMarksCheckBox.Text = "PaintVehicleRegistrationMarks";
      this.paintVehicleRegistrationMarksCheckBox.UseVisualStyleBackColor = true;
      this.useLivePermitUploadWebServiceCheckBox.AutoSize = true;
      this.useLivePermitUploadWebServiceCheckBox.Checked = Settings.Default.UseLivePermitUploadWebService;
      this.useLivePermitUploadWebServiceCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "UseLivePermitUploadWebService", true, DataSourceUpdateMode.OnPropertyChanged));
      this.useLivePermitUploadWebServiceCheckBox.Location = new Point(3, 60);
      this.useLivePermitUploadWebServiceCheckBox.Name = "useLivePermitUploadWebServiceCheckBox";
      this.useLivePermitUploadWebServiceCheckBox.Size = new Size(187, 17);
      this.useLivePermitUploadWebServiceCheckBox.TabIndex = 2;
      this.useLivePermitUploadWebServiceCheckBox.Text = "UseLivePermitUploadWebService";
      this.useLivePermitUploadWebServiceCheckBox.UseVisualStyleBackColor = true;
      this.generateDemoVehicleDataOnStartupCheckBox.AutoSize = true;
      this.generateDemoVehicleDataOnStartupCheckBox.Checked = Settings.Default.GenerateDemoVehicleDataOnStartup;
      this.generateDemoVehicleDataOnStartupCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "GenerateDemoVehicleDataOnStartup", true, DataSourceUpdateMode.OnPropertyChanged));
      this.generateDemoVehicleDataOnStartupCheckBox.Location = new Point(3, 14);
      this.generateDemoVehicleDataOnStartupCheckBox.Name = "generateDemoVehicleDataOnStartupCheckBox";
      this.generateDemoVehicleDataOnStartupCheckBox.Size = new Size(204, 17);
      this.generateDemoVehicleDataOnStartupCheckBox.TabIndex = 0;
      this.generateDemoVehicleDataOnStartupCheckBox.Text = "GenerateDemoVehicleDataOnStartup";
      this.generateDemoVehicleDataOnStartupCheckBox.UseVisualStyleBackColor = true;
      this.userInterfaceTabPage.BackColor = SystemColors.ControlLight;
      this.userInterfaceTabPage.Controls.Add((Control) this.panel4);
      this.userInterfaceTabPage.Location = new Point(4, 22);
      this.userInterfaceTabPage.Name = "userInterfaceTabPage";
      this.userInterfaceTabPage.Padding = new Padding(3);
      this.userInterfaceTabPage.Size = new Size(408, 317);
      this.userInterfaceTabPage.TabIndex = 3;
      this.userInterfaceTabPage.Text = "User Interface";
      this.userInterfaceTabPage.UseVisualStyleBackColor = true;
      this.panel4.BorderStyle = BorderStyle.Fixed3D;
      this.panel4.Controls.Add((Control) this.checkBox6);
      this.panel4.Controls.Add((Control) this.showConfigOnStartupCheckBox);
      this.panel4.Controls.Add((Control) this.label22);
      this.panel4.Controls.Add((Control) this.errorMessageTextBox);
      this.panel4.Controls.Add((Control) this.label15);
      this.panel4.Controls.Add((Control) this.label18);
      this.panel4.Controls.Add((Control) this.label17);
      this.panel4.Controls.Add((Control) this.label16);
      this.panel4.Controls.Add((Control) this.backgroundImageTextBox);
      this.panel4.Controls.Add((Control) this.backgroundImageButton);
      this.panel4.Controls.Add((Control) this.backgroundImagePictureBox);
      this.panel4.Controls.Add((Control) this.startScreenBackgroundPictureBox);
      this.panel4.Controls.Add((Control) this.startScreenBackgroundImagePathTextBox);
      this.panel4.Controls.Add((Control) this.startScreenBackgroundImageButton);
      this.panel4.Controls.Add((Control) this.button1);
      this.panel4.Controls.Add((Control) this.label2);
      this.panel4.Controls.Add((Control) this.startScreenMessageTextTextBox);
      this.panel4.Controls.Add((Control) this.mousePointerIsVisibleCheckBox);
      this.panel4.Controls.Add((Control) this.disableUserCodeCheckBox2);
      this.panel4.Location = new Point(13, 8);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(381, 303);
      this.panel4.TabIndex = 0;
      this.showConfigOnStartupCheckBox.AutoSize = true;
      this.showConfigOnStartupCheckBox.Checked = Settings.Default.ShowConfigOnStartup;
      this.showConfigOnStartupCheckBox.CheckState = CheckState.Checked;
      this.showConfigOnStartupCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "ShowConfigOnStartup", true, DataSourceUpdateMode.OnPropertyChanged));
      this.showConfigOnStartupCheckBox.Location = new Point(10, 38);
      this.showConfigOnStartupCheckBox.Name = "showConfigOnStartupCheckBox";
      this.showConfigOnStartupCheckBox.Size = new Size(131, 17);
      this.showConfigOnStartupCheckBox.TabIndex = 2;
      this.showConfigOnStartupCheckBox.Text = "ShowConfigOnStartup";
      this.showConfigOnStartupCheckBox.UseVisualStyleBackColor = true;
      this.label22.AutoSize = true;
      this.label22.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label22.Location = new Point(193, 6);
      this.label22.Name = "label22";
      this.label22.Size = new Size(166, 13);
      this.label22.TabIndex = 30;
      this.label22.Text = "ErrorMessageForNoPermitIssused";
      this.errorMessageTextBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "ErrorMessageForNoPermitIssused", true, DataSourceUpdateMode.OnPropertyChanged));
      this.errorMessageTextBox.Location = new Point(188, 22);
      this.errorMessageTextBox.Multiline = true;
      this.errorMessageTextBox.Name = "errorMessageTextBox";
      this.errorMessageTextBox.Size = new Size(177, 36);
      this.errorMessageTextBox.TabIndex = 3;
      this.errorMessageTextBox.Text = Settings.Default.ErrorMessageForNoPermitIssused;
      this.label15.AutoSize = true;
      this.label15.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label15.Location = new Point(16, 74);
      this.label15.Name = "label15";
      this.label15.Size = new Size(159, 13);
      this.label15.TabIndex = 28;
      this.label15.Text = "Start Screen Background Image";
      this.label18.AutoSize = true;
      this.label18.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label18.Location = new Point(193, 74);
      this.label18.Name = "label18";
      this.label18.Size = new Size(137, 13);
      this.label18.TabIndex = 27;
      this.label18.Text = "General Background Image";
      this.label17.AutoSize = true;
      this.label17.Location = new Point(7, 175);
      this.label17.Name = "label17";
      this.label17.Size = new Size(172, 13);
      this.label17.TabIndex = 26;
      this.label17.Text = "StartScreenBackgroundImagePath";
      this.label16.AutoSize = true;
      this.label16.Location = new Point(7, 221);
      this.label16.Name = "label16";
      this.label16.Size = new Size(94, 13);
      this.label16.TabIndex = 25;
      this.label16.Text = "BackgroundImage";
      this.backgroundImageTextBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "BackgroundImagePath", true, DataSourceUpdateMode.OnPropertyChanged));
      this.backgroundImageTextBox.Location = new Point(109, 219);
      this.backgroundImageTextBox.Name = "backgroundImageTextBox";
      this.backgroundImageTextBox.Size = new Size(215, 20);
      this.backgroundImageTextBox.TabIndex = 6;
      this.backgroundImageTextBox.Text = Settings.Default.BackgroundImagePath;
      this.backgroundImageButton.Location = new Point(329, 219);
      this.backgroundImageButton.Name = "backgroundImageButton";
      this.backgroundImageButton.Size = new Size(37, 20);
      this.backgroundImageButton.TabIndex = 7;
      this.backgroundImageButton.Text = "...";
      this.backgroundImageButton.UseVisualStyleBackColor = true;
      this.backgroundImageButton.Click += new EventHandler(this.backgroundImageButton_Click);
      this.backgroundImagePictureBox.BackgroundImageLayout = ImageLayout.Center;
      this.backgroundImagePictureBox.BorderStyle = BorderStyle.Fixed3D;
      this.backgroundImagePictureBox.DataBindings.Add(new Binding("ImageLocation", (object) Settings.Default, "BackgroundImagePath", true, DataSourceUpdateMode.OnPropertyChanged));
      this.backgroundImagePictureBox.ImageLocation = Settings.Default.BackgroundImagePath;
      this.backgroundImagePictureBox.Location = new Point(189, 90);
      this.backgroundImagePictureBox.Name = "backgroundImagePictureBox";
      this.backgroundImagePictureBox.Size = new Size(177, 79);
      this.backgroundImagePictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
      this.backgroundImagePictureBox.TabIndex = 22;
      this.backgroundImagePictureBox.TabStop = false;
      this.backgroundImagePictureBox.DoubleClick += new EventHandler(this.backgroundImageButton_Click);
      this.startScreenBackgroundPictureBox.BackgroundImageLayout = ImageLayout.Center;
      this.startScreenBackgroundPictureBox.BorderStyle = BorderStyle.Fixed3D;
      this.startScreenBackgroundPictureBox.DataBindings.Add(new Binding("ImageLocation", (object) Settings.Default, "StartScreenBackgroundImagePath", true, DataSourceUpdateMode.OnPropertyChanged));
      this.startScreenBackgroundPictureBox.ImageLocation = Settings.Default.StartScreenBackgroundImagePath;
      this.startScreenBackgroundPictureBox.Location = new Point(11, 90);
      this.startScreenBackgroundPictureBox.Name = "startScreenBackgroundPictureBox";
      this.startScreenBackgroundPictureBox.Size = new Size(176, 79);
      this.startScreenBackgroundPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
      this.startScreenBackgroundPictureBox.TabIndex = 20;
      this.startScreenBackgroundPictureBox.TabStop = false;
      this.startScreenBackgroundPictureBox.DoubleClick += new EventHandler(this.startScreenBackgroundImageButton_Click);
      this.startScreenBackgroundPictureBox.Paint += new PaintEventHandler(this.startScreenBackgroundPictureBox_Paint);
      this.startScreenBackgroundImagePathTextBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "StartScreenBackgroundImagePath", true, DataSourceUpdateMode.OnPropertyChanged));
      this.startScreenBackgroundImagePathTextBox.Location = new Point(109, 191);
      this.startScreenBackgroundImagePathTextBox.Name = "startScreenBackgroundImagePathTextBox";
      this.startScreenBackgroundImagePathTextBox.Size = new Size(215, 20);
      this.startScreenBackgroundImagePathTextBox.TabIndex = 4;
      this.startScreenBackgroundImagePathTextBox.Text = Settings.Default.StartScreenBackgroundImagePath;
      this.startScreenBackgroundImageButton.Location = new Point(329, 191);
      this.startScreenBackgroundImageButton.Name = "startScreenBackgroundImageButton";
      this.startScreenBackgroundImageButton.Size = new Size(37, 20);
      this.startScreenBackgroundImageButton.TabIndex = 5;
      this.startScreenBackgroundImageButton.Text = "...";
      this.startScreenBackgroundImageButton.UseVisualStyleBackColor = true;
      this.startScreenBackgroundImageButton.Click += new EventHandler(this.startScreenBackgroundImageButton_Click);
      this.button1.Location = new Point(285, 258);
      this.button1.Name = "button1";
      this.button1.Size = new Size(80, 38);
      this.button1.TabIndex = 9;
      this.button1.Text = "Start Screen Font Colour";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.startScreenMessageButton_Click);
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label2.Location = new Point(79, 242);
      this.label2.Name = "label2";
      this.label2.Size = new Size(146, 13);
      this.label2.TabIndex = 16;
      this.label2.Text = "Touch Screen Start Message";
      this.startScreenMessageTextTextBox.BackColor = Color.SkyBlue;
      this.startScreenMessageTextTextBox.DataBindings.Add(new Binding("ForeColor", (object) Settings.Default, "StartScreenFontColour", true, DataSourceUpdateMode.OnPropertyChanged));
      this.startScreenMessageTextTextBox.DataBindings.Add(new Binding("Text", (object) Settings.Default, "StartScreenMessage", true, DataSourceUpdateMode.OnPropertyChanged));
      this.startScreenMessageTextTextBox.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.startScreenMessageTextTextBox.ForeColor = Settings.Default.StartScreenFontColour;
      this.startScreenMessageTextTextBox.Location = new Point(5, 258);
      this.startScreenMessageTextTextBox.Multiline = true;
      this.startScreenMessageTextTextBox.Name = "startScreenMessageTextTextBox";
      this.startScreenMessageTextTextBox.Size = new Size(274, 38);
      this.startScreenMessageTextTextBox.TabIndex = 8;
      this.startScreenMessageTextTextBox.Text = Settings.Default.StartScreenMessage;
      this.startScreenMessageTextTextBox.TextAlign = HorizontalAlignment.Center;
      this.mousePointerIsVisibleCheckBox.AutoSize = true;
      this.mousePointerIsVisibleCheckBox.Checked = Settings.Default.MousePointerIsVisible;
      this.mousePointerIsVisibleCheckBox.CheckState = CheckState.Checked;
      this.mousePointerIsVisibleCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "MousePointerIsVisible", true, DataSourceUpdateMode.OnPropertyChanged));
      this.mousePointerIsVisibleCheckBox.Location = new Point(10, 20);
      this.mousePointerIsVisibleCheckBox.Name = "mousePointerIsVisibleCheckBox";
      this.mousePointerIsVisibleCheckBox.Size = new Size(129, 17);
      this.mousePointerIsVisibleCheckBox.TabIndex = 1;
      this.mousePointerIsVisibleCheckBox.Text = "MousePointerIsVisible";
      this.mousePointerIsVisibleCheckBox.UseVisualStyleBackColor = true;
      this.disableUserCodeCheckBox2.AutoSize = true;
      this.disableUserCodeCheckBox2.Checked = Settings.Default.DisableUserCode;
      this.disableUserCodeCheckBox2.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "DisableUserCode", true, DataSourceUpdateMode.OnPropertyChanged));
      this.disableUserCodeCheckBox2.Location = new Point(10, 2);
      this.disableUserCodeCheckBox2.Name = "disableUserCodeCheckBox2";
      this.disableUserCodeCheckBox2.Size = new Size(108, 17);
      this.disableUserCodeCheckBox2.TabIndex = 0;
      this.disableUserCodeCheckBox2.Text = "DisableUserCode";
      this.disableUserCodeCheckBox2.UseVisualStyleBackColor = true;
      this.tabPage5.BackColor = SystemColors.ControlLight;
      this.tabPage5.Controls.Add((Control) this.panel6);
      this.tabPage5.Location = new Point(4, 22);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new Padding(3);
      this.tabPage5.Size = new Size(408, 317);
      this.tabPage5.TabIndex = 5;
      this.tabPage5.Text = "Permits";
      this.tabPage5.UseVisualStyleBackColor = true;
      this.panel6.BorderStyle = BorderStyle.Fixed3D;
      this.panel6.Controls.Add((Control) this.enableNumberOfDaysChoiceCheckBox);
      this.panel6.Controls.Add((Control) this.checkBox5);
      this.panel6.Controls.Add((Control) this.numberBox5);
      this.panel6.Controls.Add((Control) this.numberBox2);
      this.panel6.Controls.Add((Control) this.numberBox4);
      this.panel6.Controls.Add((Control) this.numberBox3);
      this.panel6.Controls.Add((Control) this.numberBox1);
      this.panel6.Controls.Add((Control) this.customNumericUpDown1);
      this.panel6.Controls.Add((Control) this.label19);
      this.panel6.Controls.Add((Control) this.label14);
      this.panel6.Controls.Add((Control) this.label13);
      this.panel6.Controls.Add((Control) this.label11);
      this.panel6.Controls.Add((Control) this.label10);
      this.panel6.Controls.Add((Control) this.label4);
      this.panel6.Controls.Add((Control) this.label5);
      this.panel6.Controls.Add((Control) this.permitUploadAndCachingDisabledCheckBox);
      this.panel6.Location = new Point(102, 18);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(212, 271);
      this.panel6.TabIndex = 1;
      this.label19.AutoSize = true;
      this.label19.Location = new Point(6, 227);
      this.label19.Name = "label19";
      this.label19.Size = new Size(152, 13);
      this.label19.TabIndex = 30;
      this.label19.Text = "MaxNumberOfPaymentOptions";
      this.label14.AutoSize = true;
      this.label14.Location = new Point(2, 202);
      this.label14.Name = "label14";
      this.label14.Size = new Size(156, 13);
      this.label14.TabIndex = 28;
      this.label14.Text = "MaxNumberOfDaysToPermitFor";
      this.label13.AutoSize = true;
      this.label13.Location = new Point(45, 177);
      this.label13.Name = "label13";
      this.label13.Size = new Size(108, 13);
      this.label13.TabIndex = 26;
      this.label13.Text = "MaximumPermitHours";
      this.label11.AutoSize = true;
      this.label11.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.label11.Location = new Point(21, 69);
      this.label11.Name = "label11";
      this.label11.Size = new Size(172, 13);
      this.label11.TabIndex = 25;
      this.label11.Text = "Intervals && Periods (Decimal Hours)";
      this.label10.AutoSize = true;
      this.label10.Location = new Point(87, 151);
      this.label10.Name = "label10";
      this.label10.Size = new Size(66, 13);
      this.label10.TabIndex = 23;
      this.label10.Text = "GracePeriod";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(61, 125);
      this.label4.Name = "label4";
      this.label4.Size = new Size(92, 13);
      this.label4.TabIndex = 21;
      this.label4.Text = "TestPermitInterval";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(48, 100);
      this.label5.Name = "label5";
      this.label5.Size = new Size(105, 13);
      this.label5.TabIndex = 19;
      this.label5.Text = "UploadPermitInterval";
      this.permitUploadAndCachingDisabledCheckBox.AutoSize = true;
      this.permitUploadAndCachingDisabledCheckBox.Checked = Settings.Default.PermitUploadAndCachingDisabled;
      this.permitUploadAndCachingDisabledCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "PermitUploadAndCachingDisabled", true, DataSourceUpdateMode.OnPropertyChanged));
      this.permitUploadAndCachingDisabledCheckBox.Location = new Point(13, 4);
      this.permitUploadAndCachingDisabledCheckBox.Name = "permitUploadAndCachingDisabledCheckBox";
      this.permitUploadAndCachingDisabledCheckBox.Size = new Size(188, 17);
      this.permitUploadAndCachingDisabledCheckBox.TabIndex = 0;
      this.permitUploadAndCachingDisabledCheckBox.Text = "PermitUploadAndCachingDisabled";
      this.permitUploadAndCachingDisabledCheckBox.UseVisualStyleBackColor = true;
      this.openFileDialog1.FileName = "openFileDialog1";
      this.checkBox2.AutoSize = true;
      this.checkBox2.Checked = Settings.Default.PermitUploadAndCachingDisabled;
      this.checkBox2.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "PermitUploadAndCachingDisabled", true, DataSourceUpdateMode.OnPropertyChanged));
      this.checkBox2.Location = new Point(3, 104);
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.Size = new Size(188, 17);
      this.checkBox2.TabIndex = 4;
      this.checkBox2.Text = "PermitUploadAndCachingDisabled";
      this.checkBox2.UseVisualStyleBackColor = true;
      this.checkBox4.AutoSize = true;
      this.checkBox4.Checked = Settings.Default.CoinMachineIsSimulated;
      this.checkBox4.CheckState = CheckState.Checked;
      this.checkBox4.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "CoinMachineIsSimulated", true, DataSourceUpdateMode.OnPropertyChanged));
      this.checkBox4.Location = new Point(4, 125);
      this.checkBox4.Name = "checkBox4";
      this.checkBox4.Size = new Size(142, 17);
      this.checkBox4.TabIndex = 5;
      this.checkBox4.Text = "CoinMachineIsSimulated";
      this.checkBox4.UseVisualStyleBackColor = true;
      this.checkBox5.AutoSize = true;
      this.checkBox5.Checked = Settings.Default.UseLivePermitUploadWebService;
      this.checkBox5.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "UseLivePermitUploadWebService", true, DataSourceUpdateMode.OnPropertyChanged));
      this.checkBox5.Location = new Point(13, 24);
      this.checkBox5.Name = "checkBox5";
      this.checkBox5.Size = new Size(187, 17);
      this.checkBox5.TabIndex = 31;
      this.checkBox5.Text = "UseLivePermitUploadWebService";
      this.checkBox5.UseVisualStyleBackColor = true;
      this.enableNumberOfDaysChoiceCheckBox.AutoSize = true;
      this.enableNumberOfDaysChoiceCheckBox.Checked = Settings.Default.EnableNumberOfDaysChoice;
      this.enableNumberOfDaysChoiceCheckBox.CheckState = CheckState.Checked;
      this.enableNumberOfDaysChoiceCheckBox.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "EnableNumberOfDaysChoice", true, DataSourceUpdateMode.OnPropertyChanged));
      this.enableNumberOfDaysChoiceCheckBox.Location = new Point(13, 45);
      this.enableNumberOfDaysChoiceCheckBox.Name = "enableNumberOfDaysChoiceCheckBox";
      this.enableNumberOfDaysChoiceCheckBox.Size = new Size(164, 17);
      this.enableNumberOfDaysChoiceCheckBox.TabIndex = 32;
      this.enableNumberOfDaysChoiceCheckBox.Text = "EnableNumberOfDaysChoice";
      this.enableNumberOfDaysChoiceCheckBox.UseVisualStyleBackColor = true;
      this.checkBox6.AutoSize = true;
      this.checkBox6.Checked = Settings.Default.EnableNumberOfDaysChoice;
      this.checkBox6.CheckState = CheckState.Checked;
      this.checkBox6.DataBindings.Add(new Binding("Checked", (object) Settings.Default, "EnableNumberOfDaysChoice", true, DataSourceUpdateMode.OnPropertyChanged));
      this.checkBox6.Location = new Point(10, 56);
      this.checkBox6.Name = "checkBox6";
      this.checkBox6.Size = new Size(164, 17);
      this.checkBox6.TabIndex = 33;
      this.checkBox6.Text = "EnableNumberOfDaysChoice";
      this.checkBox6.UseVisualStyleBackColor = true;
      this.numberBox5.DataBindings.Add(new Binding("Number", (object) Settings.Default, "UploadPermitInterval", true, DataSourceUpdateMode.OnPropertyChanged));
      this.numberBox5.Location = new Point(159, 97);
      this.numberBox5.MaxNumber = 99.99;
      this.numberBox5.MinNumber = 0.01;
      this.numberBox5.Name = "numberBox5";
      this.numberBox5.Number = Settings.Default.UploadPermitInterval;
      this.numberBox5.NumberDecimal = new Decimal(new int[4]
      {
        25,
        0,
        0,
        131072
      });
      this.numberBox5.Size = new Size(45, 20);
      this.numberBox5.TabIndex = 1;
      this.numberBox5.Text = "0.25";
      this.numberBox5.TextAlign = HorizontalAlignment.Center;
      this.numberBox2.DataBindings.Add(new Binding("Number", (object) Settings.Default, "TestPermitInterval", true, DataSourceUpdateMode.OnPropertyChanged));
      this.numberBox2.Location = new Point(159, 122);
      this.numberBox2.MaxNumber = 99.99;
      this.numberBox2.MinNumber = 0.01;
      this.numberBox2.Name = "numberBox2";
      this.numberBox2.Number = Settings.Default.TestPermitInterval;
      this.numberBox2.NumberDecimal = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numberBox2.Size = new Size(45, 20);
      this.numberBox2.TabIndex = 2;
      this.numberBox2.Text = "1";
      this.numberBox2.TextAlign = HorizontalAlignment.Center;
      this.numberBox4.DataBindings.Add(new Binding("Number", (object) Settings.Default, "GracePeriod", true, DataSourceUpdateMode.OnPropertyChanged));
      this.numberBox4.Location = new Point(158, 147);
      this.numberBox4.MaxNumber = 99.99;
      this.numberBox4.MinNumber = 0.01;
      this.numberBox4.Name = "numberBox4";
      this.numberBox4.Number = Settings.Default.GracePeriod;
      this.numberBox4.NumberDecimal = new Decimal(new int[4]
      {
        25,
        0,
        0,
        131072
      });
      this.numberBox4.Size = new Size(45, 20);
      this.numberBox4.TabIndex = 3;
      this.numberBox4.Text = "0.25";
      this.numberBox4.TextAlign = HorizontalAlignment.Center;
      this.numberBox3.DataBindings.Add(new Binding("Number", (object) Settings.Default, "MaxNumberOfDaysToPermitFor", true, DataSourceUpdateMode.OnPropertyChanged));
      this.numberBox3.Location = new Point(158, 199);
      this.numberBox3.MaxNumber = 99.99;
      this.numberBox3.MinNumber = 0.25;
      this.numberBox3.Name = "numberBox3";
      this.numberBox3.Number = Settings.Default.MaxNumberOfDaysToPermitFor;
      this.numberBox3.NumberDecimal = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numberBox3.Size = new Size(45, 20);
      this.numberBox3.TabIndex = 5;
      this.numberBox3.Text = "1";
      this.numberBox3.TextAlign = HorizontalAlignment.Center;
      this.numberBox1.DataBindings.Add(new Binding("Number", (object) Settings.Default, "MaximumPermitHours", true, DataSourceUpdateMode.OnPropertyChanged));
      this.numberBox1.Location = new Point(159, 173);
      this.numberBox1.MaxNumber = 99.99;
      this.numberBox1.MinNumber = 0.25;
      this.numberBox1.Name = "numberBox1";
      this.numberBox1.Number = Settings.Default.MaximumPermitHours;
      this.numberBox1.NumberDecimal = new Decimal(new int[4]
      {
        24,
        0,
        0,
        0
      });
      this.numberBox1.Size = new Size(45, 20);
      this.numberBox1.TabIndex = 4;
      this.numberBox1.Text = "24";
      this.numberBox1.TextAlign = HorizontalAlignment.Center;
      this.customNumericUpDown1.DataBindings.Add(new Binding("ValueInteger", (object) Settings.Default, "MaxNumberOfPaymentOptions", true, DataSourceUpdateMode.OnPropertyChanged));
      this.customNumericUpDown1.Location = new Point(159, 225);
      this.customNumericUpDown1.Name = "customNumericUpDown1";
      this.customNumericUpDown1.Size = new Size(45, 20);
      this.customNumericUpDown1.TabIndex = 6;
      this.customNumericUpDown1.Value = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.customNumericUpDown1.ValueInteger = Settings.Default.MaxNumberOfPaymentOptions;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(416, 343);
      this.Controls.Add((Control) this.tabControl1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ConfigurationForm);
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "TouchPark Configurator";
      this.TopMost = true;
      this.FormClosing += new FormClosingEventHandler(this.ConfigurationForm_FormClosing);
      this.tabControl1.ResumeLayout(false);
      this.tabPage4.ResumeLayout(false);
      this.panel5.ResumeLayout(false);
      this.panel5.PerformLayout();
      this.tariffCategoriesTabPage.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.tabPage1.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.userInterfaceTabPage.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      ((ISupportInitialize) this.backgroundImagePictureBox).EndInit();
      ((ISupportInitialize) this.startScreenBackgroundPictureBox).EndInit();
      this.tabPage5.ResumeLayout(false);
      this.panel6.ResumeLayout(false);
      this.panel6.PerformLayout();
      this.customNumericUpDown1.EndInit();
      this.ResumeLayout(false);
    }

    private enum OpenFileDialogFilter
    {
      ImageFiles,
      XMLFiles,
    }
  }
}
