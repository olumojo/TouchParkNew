// Decompiled with JetBrains decompiler
// Type: TouchPark.PaymentForm
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using MRG.Controls.UI;
using RangerServices.Logging;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using TouchPark.CardDevices;
using TouchPark.CashDevices;
using TouchPark.Properties;

namespace TouchPark
{
  public class PaymentForm : Form
  {
    private string m_AmountOfFreeTime = ConfigurationManager.AppSettings["amountOfFreeTime"];
    private ParkingPermitInfo _parkingPermitInfo = new ParkingPermitInfo();
    private int _acceptTimout = -1;
    private int _rejectTimeout = -1;
    private bool _cancel;
    private string m_errorLogPath;
    private string m_DisplayOption;
    private string m_PaymentAmountAsString;
    private bool _rejectCash;
    private bool _collectCash;
    private CashDevice.CoinEnteredHandler _coinEnteredHandler;
    private CashDevice.ExpectedCashReceivedHandler _expectedCashReceivedHandler;
    private CashDevice.RunningProcessHandler _runningProcessHandler;
    private CashDevice.CashCollectedEventHandler _cashCollectedEventHandler;
    private CardDevice _cardDevice;
    private CardDeviceResponse _cardDeviceResponse;
    private IContainer components;
    private Label amountLabel;
    private Button backButton;
    private Button rejectButton;
    private Label messageLabel;
    private Button acceptButton;
    private Button cardPaymentButton;
    private Label paymentLabel;
    private Panel cardPaymentPanel;
    private Label pinpadLabel;
    private Label stayExpiryLabel;
    private Button cancelCardButton;
    private Panel panel1;
    private LoadingCircle loadingCircle1;
    private System.Windows.Forms.Timer closeFormTimer;

    [DllImport("Gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse);

    public PaymentForm(ParkingPermitInfo parkingPermit, string displayOption)
    {
      Utilites.WriteToLog("frmPayment entered");
      this.InitializeComponent();
      this.SetFormRegion();
      this._parkingPermitInfo.copy(parkingPermit);
      this.m_DisplayOption = displayOption;
      this.stayExpiryLabel.Text = string.Format("Stay Expires\nAt {0}", (object) this._parkingPermitInfo.EndDate.ToShortTimeString());
      this.CoinMechanismSetup();
      Utilites.WriteToLog("frmPayment Exited");
    }

    private void PaymentForm_Closing(object sender, FormClosingEventArgs e)
    {
      this.ResetCardDevice();
      this.ResetCoinMechanism();
    }

    private void PaymentForm_Shown(object sender, EventArgs e)
    {
      this.closeFormTimer.Start();
      this.StartPayment();
    }

    private void SetFormRegion()
    {
      this.Region = Region.FromHrgn(PaymentForm.CreateRoundRectRgn(0, 0, this.Width - 10, this.Height - 10, 20, 20));
    }

    private void GetFormPaymentSettings()
    {
      Utilites.WriteToLog("GetFormPaymentSettings entered");
      this.m_PaymentAmountAsString = this._parkingPermitInfo.Amount.ToString("N2");
      this.amountLabel.Text = "£" + this.m_PaymentAmountAsString;
      Utilites.WriteToLog("GetFormPaymentSettings Exited");
    }

    private void WriteGuestParkPermit()
    {
      Utilites.WriteToLog("WriteGuestParkPermit entered");
      this._parkingPermitInfo.MachineName = Environment.MachineName;
      this._parkingPermitInfo.PermitType = "VALIDATE";
      this._parkingPermitInfo.PaymentType = "";
      this._parkingPermitInfo.AuthCode = "";
      this._parkingPermitInfo.Amount = new Decimal(0);
      this._parkingPermitInfo.EndDate = DateTime.MinValue;
      Utilites.WriteToLog("WriteGuestParkPermit Exited");
    }

    private void RestartCloseFormTimer()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke((Delegate) new ThreadStart(this.RestartCloseFormTimer));
      }
      else
      {
        this.closeFormTimer.Stop();
        this.closeFormTimer.Start();
      }
    }

    private void rejectButton_Click(object sender, EventArgs e)
    {
      this._rejectCash = true;
    }

    private void acceptButton_Click(object sender, EventArgs e)
    {
      this._collectCash = true;
    }

    private void cardPayment_Click(object sender, EventArgs e)
    {
      Utilites.WriteToLog("cmdCCard_Click entered");
      this.StartCardPayment();
      Utilites.WriteToLog("cmdCCard_Click Exited");
    }

    private void backButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void CoinMechanismSetup()
    {
      try
      {
        if (Devices.CoinMechanism == null)
          Devices.CoinMechanism = new CoinMechanism();
        Devices.CoinMechanism.CollectOnTimeout = true;
        Devices.CoinMechanism.RejectOnTimeOut = true;
        Devices.CoinMechanism.TimeOut = new TimeSpan(0, 0, 9);
        Devices.CoinMechanism.ExpectedCash = this._parkingPermitInfo.Amount;
        this.AttachCoinMechanismEventDelegates();
      }
      catch (Exception ex)
      {
        OutOfOrderForm outOfOrderForm = new OutOfOrderForm(true);
      }
    }

    private void WaitForCashPayment()
    {
      Utilites.WriteToLog("cashPayment entered");
      try
      {
        this.messageLabel.Text = "PLEASE INSERT THE CORRECT CASH\n\rNO CHANGE OR REFUND IS GIVEN";
        this.amountLabel.Text = this._parkingPermitInfo.Amount.ToString("C");
        this._cancel = false;
        Devices.CoinMechanism.WaitOnMoney();
      }
      catch (JammedCoinException ex)
      {
        Log.Write((Exception) ex);
        this.messageLabel.Text = "A coin appears to be jammed! Press the release button!";
        this.ResetCoinMechanism();
        this.Close();
      }
      Utilites.WriteToLog("cashPayment Exited");
    }

    private void RejectIfCoinsEntered()
    {
      if (!(Devices.CoinMechanism.EscrowCash > new Decimal(0)))
        return;
      Devices.CoinMechanism.RejectCash();
      this.ResetAfterRejectCash();
    }

    private void ResetAfterRejectCash()
    {
      this.acceptButton.Visible = false;
      this.amountLabel.Text = this._parkingPermitInfo.Amount.ToString("C");
      this._rejectCash = false;
      this.rejectButton.Text = "REJECT";
      this.rejectButton.Visible = false;
      this._rejectTimeout = -1;
    }

    private void RejectButtonTextCountDownSet()
    {
      if (this._rejectTimeout > 0)
        this.rejectButton.Text = string.Format("REJECT [{0}]", (object) this._rejectTimeout);
      else
        this.ResetAfterRejectCash();
    }

    private void LogABadValue(Decimal collectedAmount)
    {
      LogData logData = new LogData();
      logData.Title = "Failure";
      logData.Message =(string.Format("{0},{1},{2},ByCash,BAD Cash Value:Collected Cash Less Than ExpectedCash", (object) this._parkingPermitInfo.VehicleRegMark, (object) this._parkingPermitInfo.Amount, (object) Math.Round(collectedAmount, 2)));
      logData.Categories.Clear();
      logData.Categories.Add("Exception");
      logData.Categories.Add("TouchPark Payment");
      Log.Write(logData);
    }

    private void LogFailedCardReaderActivity(TransactionResult result)
    {
      LogData logData = new LogData();
      logData.Title = "Failure";
      logData.Message = (string.Format("{0},{1},0.00,ByCard,EMV Response: {2}", (object) this._parkingPermitInfo.VehicleRegMark, (object) this._parkingPermitInfo.Amount, (object) ((object) result).ToString()));
      logData.Categories.Clear();
      logData.Categories.Add("Exception");
      logData.Categories.Add("TouchPark Payment");
      Log.Write(logData);
    }

    private void LogAPayment(Decimal amount, PaymentForm.PaymentType paymentType)
    {
      LogData logData = new LogData();
      logData.Title = "Payment";
      logData.Message = (string.Format("{0},{1},{2},{3},Success", (object) this._parkingPermitInfo.VehicleRegMark, (object) this._parkingPermitInfo.Amount, (object) Math.Round(amount, 2), (object) paymentType));
      logData.Categories.Clear();
      logData.Categories.Add("TouchPark Payment");
      Log.Write(logData);
    }

    private void StartPayment()
    {
      this.amountLabel.Visible = true;
      if (Settings.Default.CardDeviceEnabled)
      {
        bool flag = new CardPaymentWebsiteConnectivity().CardPaymentServerIsAccessible();
        if (!flag)
          this.LogFailedCardReaderActivity((TransactionResult) 1002);
        this.cardPaymentButton.Visible = Utilites.InternetAlive() & flag;
      }
      else
        this.cardPaymentButton.Visible = false;
      this.cardPaymentPanel.Visible = false;
      this.ResetCoinMechanism();
      this.CoinMechanismSetup();
      this.WaitForCashPayment();
    }

    private void ResetCoinMechanism()
    {
      this.RejectIfCoinsEntered();
      this._rejectCash = false;
      this._cancel = true;
      Devices.CoinMechanism.Reset();
      this.RemoveCoinMechanismEventDelegates();
    }

    private void RemoveCoinMechanismEventDelegates()
    {
      try
      {
        Devices.CoinMechanism.OnCoinEntered += this._coinEnteredHandler;
        Devices.CoinMechanism.OnExpectedCashReceived += this._expectedCashReceivedHandler;
        Devices.CoinMechanism.OnRunningProcess += this._runningProcessHandler;
        Devices.CoinMechanism.OnCashCollected += this._cashCollectedEventHandler;
      }
      catch
      {
      }
    }

    private void AttachCoinMechanismEventDelegates()
    {
      // ISSUE: method pointer
      this._coinEnteredHandler = new CashDevice.CoinEnteredHandler(Devices_CoinMechanism_OnCoinEntered);
      // ISSUE: method pointer
      this._expectedCashReceivedHandler = new CashDevice.ExpectedCashReceivedHandler(Devices_CoinMechanism_OnExpectedCashReceived);
      // ISSUE: method pointer
      this._runningProcessHandler = new CashDevice.RunningProcessHandler(Devices_CoinMechanism_OnRunningProcess);
      // ISSUE: method pointer
      this._cashCollectedEventHandler = new CashDevice.CashCollectedEventHandler(Devices_CoinMechanism_OnCashCollected);
      Devices.CoinMechanism.OnCoinEntered += this._coinEnteredHandler;
      Devices.CoinMechanism.OnExpectedCashReceived += this._expectedCashReceivedHandler;
      Devices.CoinMechanism.OnRunningProcess += this._runningProcessHandler;
      Devices.CoinMechanism.OnCashCollected += this._cashCollectedEventHandler;
    }

    private void Devices_CoinMechanism_OnRunningProcess(
      object o,
      CashDevice.RunningProcessEventArgs runningProcessEventArgs)
    {
      Application.DoEvents();
      if (Devices.CoinMechanism.EscrowCash > new Decimal(0))
        runningProcessEventArgs.Reject = this._rejectCash;
      runningProcessEventArgs.Cancel = this._cancel;
      if (this._rejectCash || this._cancel)
      {
        this.ResetAfterRejectCash();
      }
      else
      {
        if (!(runningProcessEventArgs.RejectTimoutRemaining != TimeSpan.MinValue) || this._rejectTimeout == runningProcessEventArgs.RejectTimoutRemaining.Seconds)
          return;
        this._rejectTimeout = runningProcessEventArgs.RejectTimoutRemaining.Seconds;
        this.RejectButtonTextCountDownSet();
      }
    }

    private void Devices_CoinMechanism_OnExpectedCashReceived(
      object o,
      CashDevice.ExpectedCashReceivedEventArgs expectedCashReceivedEventArgs)
    {
      this.acceptButton.Visible = true;
      this.rejectButton.Text = "REJECT";
      if (Devices.CoinMechanism.EscrowCash > new Decimal(0))
        expectedCashReceivedEventArgs.Collect = this._collectCash;
      if (this._collectCash || this._acceptTimout == expectedCashReceivedEventArgs.TimeOutRemaining.Seconds)
        return;
      this._acceptTimout = expectedCashReceivedEventArgs.TimeOutRemaining.Seconds;
      this.acceptButton.Text = string.Format("ACCEPT {0}", (object) expectedCashReceivedEventArgs.TimeOutRemaining.Seconds);
    }

    private void Devices_CoinMechanism_OnCashCollected(
      object o,
      CashDevice.CashProcessedEventArgs cashProcessedEventArgs)
    {
      this.acceptButton.Visible = false;
      this.rejectButton.Visible = false;
      Decimal totalValue = cashProcessedEventArgs.TotalValue;
      if (totalValue >= this._parkingPermitInfo.Amount)
      {
        this.LogAPayment(totalValue, PaymentForm.PaymentType.ByCoins);
        this.PaidInFull();
      }
      else
        this.LogABadValue(totalValue);
    }

    private void Devices_CoinMechanism_OnCoinEntered(
      object o,
      CashDevice.CoinEnteredEventArgs coinEnteredEventArgs)
    {
      this.rejectButton.Visible = true;
      Decimal num = Devices.CoinMechanism.GetCashToPay();
      if (num <= new Decimal(0))
        num = new Decimal(0);
      this.amountLabel.Text = num.ToString("C");
      this.RestartCloseFormTimer();
    }

    private void OnCardInserted(object o, EventArgs e)
    {
      this.ShowFollowInstructionsMessage_DeactivateLoadingCircle();
    }

    private void OnWaitingForCardRemoval(object o, EventArgs e)
    {
      this.ShowRemoveCardMessage();
    }

    private void OnWaitingForCardInsert(object o, EventArgs e)
    {
      this.ShowInsertCardMessage();
    }

    private void OnVoidTransactionFailure(object o, EventArgs e)
    {
      this.ShowVoidTransactionFailedMessage_LogEvent();
    }

    private void ShowFollowInstructionsMessage_DeactivateLoadingCircle()
    {
      if (!this._cardDevice.Cancel)
        this.pinpadLabel.Text = "Please follow the instructions on the card reader";
      this.loadingCircle1.Active = false;
      ((Control) this.loadingCircle1).Visible = false;
      this.cardPaymentPanel.Refresh();
    }

    private void ShowVoidTransactionFailedMessage_LogEvent()
    {
      this.pinpadLabel.Text = "An attempt to void a transaction failed";
      this.cancelCardButton.Visible = false;
      this.cardPaymentPanel.Refresh();
      LogData logData = new LogData();
      logData.Categories.Clear();
      logData.Categories.Add("Exception");
      logData.Title = "An attempt to void a card transaction has failed";
      logData.Message = (string.Format("VRM:{0},AMOUNT:{1}", (object) this._parkingPermitInfo.VehicleRegMark, (object) this._parkingPermitInfo.Amount));
      Log.Write(logData);
    }

    private void ShowInsertCardMessage()
    {
      this.pinpadLabel.Text = "Please insert your payment card";
      this.cardPaymentPanel.Refresh();
    }

    private void ShowRemoveCardMessage()
    {
      this.pinpadLabel.Text = "Please remove your payment card";
      this.cancelCardButton.Visible = false;
      this.cardPaymentPanel.Refresh();
    }

    private void EnableCardPaymentMode()
    {
      this._cardDeviceResponse = (CardDeviceResponse) null;
      this.cardPaymentPanel.Visible = true;
      this.messageLabel.Text = "Payment by credit or debit card";
      this.pinpadLabel.Text = "Initialising payment, please wait...";
      this.loadingCircle1.Active = true;
      ((Control) this.loadingCircle1).Visible = true;
      this.cancelCardButton.Visible = true;
    }

    private void DisableCardPaymentMode()
    {
      this.cardPaymentPanel.Visible = false;
      this.loadingCircle1.Active = false;
      ((Control) this.loadingCircle1).Visible = false;
      this.cancelCardButton.Visible = true;
    }

    private void StartCardPayment()
    {
      try
      {
        this.EnableCardPaymentMode();
        this.ResetCoinMechanism();
        this.RestartCloseFormTimer();
        if (Devices.CardDevice == null)
          Devices.CardDevice = new CardDevice();
        this._cardDevice = Devices.CardDevice;
        this._cardDevice.Cancel = false;
        this.ShowInsertCardMessage();
        this._cardDevice.WaitInsertCard();
        Console.WriteLine("After Card Insert");
        if (!this._cardDevice.Cancel)
        {
          this.ShowFollowInstructionsMessage_DeactivateLoadingCircle();
          this._cardDeviceResponse = this._cardDevice.WaitForPayment(this._parkingPermitInfo.Amount);
        }
        Console.WriteLine("After Card Transaction");
        if (!this._cardDevice.CardIsRemoved)
        {
          this.ShowRemoveCardMessage();
          this._cardDevice.WaitRemoveCard();
        }
        Console.WriteLine("After Card Removal");
        if (this._cardDeviceResponse != null)
          this.RespondToCardDeviceActivity();
        else
          this.HandleFailedCardTransaction();
        Console.WriteLine("Card Payment End");
      }
      catch (Exception ex)
      {
        if (this._cardDevice.Cancel)
          return;
        Log.Write(ex);
        this.OpenCreditCardErrorForm();
      }
    }

    private void HandleFailedCardTransaction()
    {
      if (this._cardDeviceResponse != null)
      {
        Console.WriteLine("Card Failing Payment... logging..");
        this.LogFailedCardReaderActivity((TransactionResult) this._cardDeviceResponse.TransactionResult);
      }
      if (!this._cardDevice.Cancel)
      {
        Console.WriteLine("Opening Card Error Form");
        this.OpenCreditCardErrorForm();
      }
      else
      {
        Console.WriteLine("Cancelling: Restarting Coin Payment...");
        this.DisableCardPaymentMode();
        this.StartPayment();
      }
    }

    private void CancelCardTransaction()
    {
      Console.WriteLine("Cancelling Reader...");
      this.pinpadLabel.Text = "Card transcation cancelled.  Please wait...";
      this.cancelCardButton.Visible = false;
      this._cardDevice.Cancel = true;
    }

    private void RespondToCardDeviceActivity()
    {
      Console.WriteLine("Responding To Card Activity:{0}", (object) (TransactionResult) this._cardDeviceResponse.TransactionResult);
      if (this._cardDeviceResponse != null && (this._cardDeviceResponse.TransactionResult == null || this._cardDeviceResponse.TransactionResult == TransactionResult.Trans_OK))
      {
        Console.WriteLine("Completing Payment");
        this.CompleteCardPayment((string) this._cardDeviceResponse.AuthCode);
      }
      else
        this.HandleFailedCardTransaction();
    }

    private void CompleteCardPayment(string authCode)
    {
      this.pinpadLabel.Text = "Payment by card completed.";
      this.Refresh();
      this._parkingPermitInfo.MachineName = Environment.MachineName;
      this._parkingPermitInfo.PermitType = "PAYMENT";
      this._parkingPermitInfo.PaymentType = "CCARD";
      this._parkingPermitInfo.AuthCode = authCode;
      this._parkingPermitInfo.Paid = this._parkingPermitInfo.Amount;
      this.LogAPayment(this._parkingPermitInfo.Paid, PaymentForm.PaymentType.ByCard);
      this.PaidInFullOpenThankyouForm();
    }

    private void ResetCardDevice()
    {
      try
      {
        if (this._cardDevice == null)
          return;
        this._cardDevice.VoidPendingTransactions();
        if (this._cardDevice.TransactionInProgress)
          this._cardDevice.Cancel = true;
        if (!this._cardDevice.CardIsRemoved)
          this._cardDevice.WaitForCardRemoval();
        this.ResetCardDeviceWelcomeMessage();
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    private void ResetCardDeviceWelcomeMessage()
    {
      if (this._cardDevice == null || !this._cardDevice.IsConnected)
        return;
      this._cardDevice.DisplayWelcomeScreenMessage(Settings.Default.CardDeviceWelcomeMessage);
    }

    private void OpenCreditCardErrorForm()
    {
      this.Hide();
      frmCreditCardPaymentError cardPaymentError = new frmCreditCardPaymentError();
      int num = (int) cardPaymentError.ShowDialog();
      this.Show();
      if (cardPaymentError.retryPayment)
        this.StartPayment();
      else
        this.Close();
    }

    private void PaidInFull()
    {
      this._parkingPermitInfo.MachineName = Environment.MachineName;
      this._parkingPermitInfo.PermitType = "PAYMENT";
      this._parkingPermitInfo.PaymentType = "CASH";
      this._parkingPermitInfo.AuthCode = "";
      this._parkingPermitInfo.Paid = Devices.CoinMechanism.CollectedCash;
      this.PaidInFullOpenThankyouForm();
      this.ResetCoinMechanism();
    }

    private void PaidInFullOpenThankyouForm()
    {
      Utilites.WriteToLog("GetFormPaidInFullSettings entered");
      bool xml = Utilites.WriteParkingPermitToXML(this._parkingPermitInfo);
      this.Hide();
      frmThankYou frmThankYou = new frmThankYou(this._parkingPermitInfo, this.m_DisplayOption, xml, this._cardDeviceResponse);
      int num = (int) frmThankYou.ShowDialog();
      frmThankYou.Close();
      frmThankYou.Dispose();
      this.Close();
      Utilites.WriteToLog("GetFormPaidInFullSettings Exited");
    }

    private void cancelCardButton_Click(object sender, EventArgs e)
    {
      this.CancelCardTransaction();
    }

    private void closeFormTimer_Tick(object sender, EventArgs e)
    {
      this.closeFormTimer.Stop();
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
      this.amountLabel = new Label();
      this.messageLabel = new Label();
      this.rejectButton = new Button();
      this.backButton = new Button();
      this.acceptButton = new Button();
      this.cardPaymentButton = new Button();
      this.paymentLabel = new Label();
      this.cardPaymentPanel = new Panel();
      this.cancelCardButton = new Button();
      this.loadingCircle1 = new LoadingCircle();
      this.pinpadLabel = new Label();
      this.stayExpiryLabel = new Label();
      this.panel1 = new Panel();
      this.closeFormTimer = new System.Windows.Forms.Timer(this.components);
      this.cardPaymentPanel.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.amountLabel.BackColor = Color.LightSteelBlue;
      this.amountLabel.BorderStyle = BorderStyle.FixedSingle;
      this.amountLabel.Font = new Font("Impact", 36f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.amountLabel.Location = new Point(48, 62);
      this.amountLabel.Name = "amountLabel";
      this.amountLabel.Size = new Size(238, 61);
      this.amountLabel.TabIndex = 2;
      this.amountLabel.Text = "£0.00";
      this.amountLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.messageLabel.BackColor = Color.LightSteelBlue;
      this.messageLabel.Font = new Font("Arial Black", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.messageLabel.Location = new Point(17, 23);
      this.messageLabel.Name = "messageLabel";
      this.messageLabel.Size = new Size(582, 69);
      this.messageLabel.TabIndex = 18;
      this.messageLabel.Text = "PLEASE INSERT THE CORRECT CASH\r\nNO CHANGE OR REFUND IS GIVEN";
      this.messageLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.rejectButton.BackColor = Color.Transparent;
      this.rejectButton.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.rejectButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.rejectButton.FlatAppearance.BorderSize = 0;
      this.rejectButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.rejectButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.rejectButton.FlatStyle = FlatStyle.Flat;
      this.rejectButton.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.rejectButton.ForeColor = Color.White;
      this.rejectButton.Location = new Point(210, 322);
      this.rejectButton.Name = "rejectButton";
      this.rejectButton.Size = new Size(195, 52);
      this.rejectButton.TabIndex = 9;
      this.rejectButton.Text = "REJECT";
      this.rejectButton.UseVisualStyleBackColor = false;
      this.rejectButton.Visible = false;
      this.rejectButton.Click += new EventHandler(this.rejectButton_Click);
      this.backButton.BackColor = Color.Transparent;
      this.backButton.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.backButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.backButton.FlatAppearance.BorderSize = 0;
      this.backButton.FlatStyle = FlatStyle.Flat;
      this.backButton.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.backButton.ForeColor = Color.White;
      this.backButton.Location = new Point(36, 385);
      this.backButton.Name = "backButton";
      this.backButton.Size = new Size(142, 55);
      this.backButton.TabIndex = 8;
      this.backButton.Text = "< BACK";
      this.backButton.UseVisualStyleBackColor = false;
      this.backButton.Click += new EventHandler(this.backButton_Click);
      this.acceptButton.BackColor = Color.Transparent;
      this.acceptButton.BackgroundImage = (Image) Resources.GreenKeyGloss;
      this.acceptButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.acceptButton.FlatAppearance.BorderSize = 0;
      this.acceptButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.acceptButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.acceptButton.FlatStyle = FlatStyle.Flat;
      this.acceptButton.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.acceptButton.ForeColor = Color.DarkGreen;
      this.acceptButton.Location = new Point(196, 259);
      this.acceptButton.Name = "acceptButton";
      this.acceptButton.Size = new Size(221, 57);
      this.acceptButton.TabIndex = 19;
      this.acceptButton.Text = "ACCEPT";
      this.acceptButton.UseVisualStyleBackColor = false;
      this.acceptButton.Visible = false;
      this.acceptButton.Click += new EventHandler(this.acceptButton_Click);
      this.cardPaymentButton.BackColor = Color.Transparent;
      this.cardPaymentButton.BackgroundImage = (Image) Resources.BlueKeyL;
      this.cardPaymentButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.cardPaymentButton.FlatAppearance.BorderSize = 0;
      this.cardPaymentButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.cardPaymentButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.cardPaymentButton.FlatStyle = FlatStyle.Flat;
      this.cardPaymentButton.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cardPaymentButton.ForeColor = Color.Gold;
      this.cardPaymentButton.Location = new Point(187, 385);
      this.cardPaymentButton.Name = "cardPaymentButton";
      this.cardPaymentButton.Size = new Size(388, 55);
      this.cardPaymentButton.TabIndex = 20;
      this.cardPaymentButton.Text = "Pay By Credit / Debit Card";
      this.cardPaymentButton.UseVisualStyleBackColor = false;
      this.cardPaymentButton.Click += new EventHandler(this.cardPayment_Click);
      this.paymentLabel.BackColor = Color.Transparent;
      this.paymentLabel.Font = new Font("Arial Black", 20f, FontStyle.Bold);
      this.paymentLabel.Location = new Point(50, 24);
      this.paymentLabel.Name = "paymentLabel";
      this.paymentLabel.Size = new Size(236, 38);
      this.paymentLabel.TabIndex = 22;
      this.paymentLabel.Text = "AMOUNT DUE";
      this.paymentLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.cardPaymentPanel.BackgroundImage = (Image) Resources.bg;
      this.cardPaymentPanel.BackgroundImageLayout = ImageLayout.Stretch;
      this.cardPaymentPanel.Controls.Add((Control) this.cancelCardButton);
      this.cardPaymentPanel.Controls.Add((Control) this.loadingCircle1);
      this.cardPaymentPanel.Controls.Add((Control) this.pinpadLabel);
      this.cardPaymentPanel.Location = new Point(17, 259);
      this.cardPaymentPanel.Name = "cardPaymentPanel";
      this.cardPaymentPanel.Size = new Size(582, 181);
      this.cardPaymentPanel.TabIndex = 23;
      this.cardPaymentPanel.Visible = false;
      this.cancelCardButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.cancelCardButton.BackColor = Color.Transparent;
      this.cancelCardButton.BackgroundImage = (Image) Resources.SquareLargeRed;
      this.cancelCardButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.cancelCardButton.FlatAppearance.BorderSize = 0;
      this.cancelCardButton.FlatStyle = FlatStyle.Flat;
      this.cancelCardButton.Font = new Font("Arial", 18f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cancelCardButton.ForeColor = Color.White;
      this.cancelCardButton.Location = new Point(19, 126);
      this.cancelCardButton.Name = "cancelCardButton";
      this.cancelCardButton.Size = new Size(142, 55);
      this.cancelCardButton.TabIndex = 9;
      this.cancelCardButton.Text = "< BACK";
      this.cancelCardButton.UseVisualStyleBackColor = false;
      this.cancelCardButton.Click += new EventHandler(this.cancelCardButton_Click);
      this.loadingCircle1.Active = false;
      ((Control) this.loadingCircle1).BackColor = Color.Transparent;
      this.loadingCircle1.Color = Color.Azure;
      this.loadingCircle1.InnerCircleRadius = 17;
      ((Control) this.loadingCircle1).Location = new Point(231, 0);
      ((Control) this.loadingCircle1).Name = "loadingCircle1";
      this.loadingCircle1.NumberSpoke = 15;
      this.loadingCircle1.OuterCircleRadius = 28;
      this.loadingCircle1.RotationSpeed = 100;
      ((Control) this.loadingCircle1).Size = new Size(108, 77);
      this.loadingCircle1.SpokeThickness = 4;
      ((Control) this.loadingCircle1).TabIndex = 10;
      ((Control) this.loadingCircle1).Text = "loadingCircle1";
      ((Control) this.loadingCircle1).Visible = false;
      this.pinpadLabel.BackColor = Color.Transparent;
      this.pinpadLabel.Dock = DockStyle.Fill;
      this.pinpadLabel.Font = new Font("Arial Black", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.pinpadLabel.Location = new Point(0, 0);
      this.pinpadLabel.Name = "pinpadLabel";
      this.pinpadLabel.Size = new Size(582, 181);
      this.pinpadLabel.TabIndex = 0;
      this.pinpadLabel.Text = "Please follow the instructions on the pin pad";
      this.pinpadLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.stayExpiryLabel.BackColor = Color.Transparent;
      this.stayExpiryLabel.Font = new Font("Arial Black", 14f);
      this.stayExpiryLabel.Image = (Image) Resources.transbuttonimg;
      this.stayExpiryLabel.Location = new Point(346, 13);
      this.stayExpiryLabel.Name = "stayExpiryLabel";
      this.stayExpiryLabel.Size = new Size(135, 121);
      this.stayExpiryLabel.TabIndex = 24;
      this.stayExpiryLabel.Text = "Stay Expires: 14:00 Hrs";
      this.stayExpiryLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.panel1.BackgroundImage = (Image) Resources.bg;
      this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel1.Controls.Add((Control) this.paymentLabel);
      this.panel1.Controls.Add((Control) this.amountLabel);
      this.panel1.Controls.Add((Control) this.stayExpiryLabel);
      this.panel1.Location = new Point(17, 104);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(582, 149);
      this.panel1.TabIndex = 25;
      this.closeFormTimer.Interval = 120000;
      this.closeFormTimer.Tick += new EventHandler(this.closeFormTimer_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.BackgroundImage = (Image) Resources.bg;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.ClientSize = new Size(622, 452);
      this.ControlBox = false;
      this.Controls.Add((Control) this.cardPaymentPanel);
      this.Controls.Add((Control) this.backButton);
      this.Controls.Add((Control) this.cardPaymentButton);
      this.Controls.Add((Control) this.acceptButton);
      this.Controls.Add((Control) this.messageLabel);
      this.Controls.Add((Control) this.rejectButton);
      this.Controls.Add((Control) this.panel1);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (PaymentForm);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopMost = true;
      this.Shown += new EventHandler(this.PaymentForm_Shown);
      this.FormClosing += new FormClosingEventHandler(this.PaymentForm_Closing);
      this.cardPaymentPanel.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private enum PaymentType
    {
      ByCoins,
      ByCard,
    }
  }
}
