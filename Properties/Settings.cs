// Decompiled with JetBrains decompiler
// Type: TouchPark.Properties.Settings
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace TouchPark.Properties
{
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
  [CompilerGenerated]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("AUDIT")]
    public string AuditReportCode
    {
      get
      {
        return (string) this[nameof (AuditReportCode)];
      }
      set
      {
        this[nameof (AuditReportCode)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("True")]
    [DebuggerNonUserCode]
    public bool MousePointerIsVisible
    {
      get
      {
        return (bool) this[nameof (MousePointerIsVisible)];
      }
      set
      {
        this[nameof (MousePointerIsVisible)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool CardDeviceEnabled
    {
      get
      {
        return (bool) this[nameof (CardDeviceEnabled)];
      }
      set
      {
        this[nameof (CardDeviceEnabled)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool CoinMachineIsSimulated
    {
      get
      {
        return (bool) this[nameof (CoinMachineIsSimulated)];
      }
      set
      {
        this[nameof (CoinMachineIsSimulated)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    [UserScopedSetting]
    public bool DisableUserCode
    {
      get
      {
        return (bool) this[nameof (DisableUserCode)];
      }
      set
      {
        this[nameof (DisableUserCode)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("7")]
    [UserScopedSetting]
    public double TestPermitInterval
    {
      get
      {
        return (double) this[nameof (TestPermitInterval)];
      }
      set
      {
        this[nameof (TestPermitInterval)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("0.25")]
    public double UploadPermitInterval
    {
      get
      {
        return (double) this[nameof (UploadPermitInterval)];
      }
      set
      {
        this[nameof (UploadPermitInterval)] = (object) value;
      }
    }

    [DefaultSettingValue("Gold")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public Color StartScreenFontColour
    {
      get
      {
        return (Color) this[nameof (StartScreenFontColour)];
      }
      set
      {
        this[nameof (StartScreenFontColour)] = (object) value;
      }
    }

    [DefaultSettingValue("0845 3032959")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public string RangerTelephoneNumber
    {
      get
      {
        return (string) this[nameof (RangerTelephoneNumber)];
      }
      set
      {
        this[nameof (RangerTelephoneNumber)] = (object) value;
      }
    }

    [DefaultSettingValue("24")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public double MaximumPermitHours
    {
      get
      {
        return (double) this[nameof (MaximumPermitHours)];
      }
      set
      {
        this[nameof (MaximumPermitHours)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("10")]
    [DebuggerNonUserCode]
    public int MaxNumberOfPaymentOptions
    {
      get
      {
        return (int) this[nameof (MaxNumberOfPaymentOptions)];
      }
      set
      {
        this[nameof (MaxNumberOfPaymentOptions)] = (object) value;
      }
    }

    [DefaultSettingValue("C:\\TouchPark\\dummyUserInformation.xml")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string DummyUserCodeDataFilePath
    {
      get
      {
        return (string) this[nameof (DummyUserCodeDataFilePath)];
      }
      set
      {
        this[nameof (DummyUserCodeDataFilePath)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("Please contact support on 0845 3032959")]
    [UserScopedSetting]
    public string ErrorMessageForNoPermitIssused
    {
      get
      {
        return (string) this[nameof (ErrorMessageForNoPermitIssused)];
      }
      set
      {
        this[nameof (ErrorMessageForNoPermitIssused)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool ShowConfigOnStartup
    {
      get
      {
        return (bool) this[nameof (ShowConfigOnStartup)];
      }
      set
      {
        this[nameof (ShowConfigOnStartup)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("0.25")]
    [UserScopedSetting]
    public double GracePeriod
    {
      get
      {
        return (double) this[nameof (GracePeriod)];
      }
      set
      {
        this[nameof (GracePeriod)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [ApplicationScopedSetting]
    [DefaultSettingValue("C:\\TouchPark\\Data\\UploadedPermits\\")]
    public string UploadedPermitFolder
    {
      get
      {
        return (string) this[nameof (UploadedPermitFolder)];
      }
    }

    [ApplicationScopedSetting]
    [DefaultSettingValue("<?xml version=\"1.0\" standalone=\"yes\"?>\r\n<NewDataSet>\r\n\t<xs:schema id=\"NewDataSet\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n\t\t<xs:element name=\"NewDataSet\" msdata:IsDataSet=\"true\" msdata:UseCurrentLocale=\"true\">\r\n\t\t\t<xs:complexType>\r\n\t\t\t\t<xs:choice minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n\t\t\t\t\t<xs:element name=\"Table1\">\r\n\t\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t\t<xs:sequence>\r\n\t\t\t\t\t\t\t\t<xs:element name=\"CarDataID\" type=\"xs:int\" minOccurs=\"0\" />\r\n\t\t\t\t\t\t\t\t<xs:element name=\"VRM\" type=\"xs:string\" minOccurs=\"0\" />\r\n\t\t\t\t\t\t\t\t<xs:element name=\"EventDateTime\" type=\"xs:dateTime\" minOccurs=\"0\" />\r\n\t\t\t\t\t\t\t\t<xs:element name=\"Direction\" type=\"xs:string\" minOccurs=\"0\" />\r\n\t\t\t\t\t\t\t\t<xs:element name=\"PlateImage\" type=\"xs:string\" minOccurs=\"0\" />\r\n\t\t\t\t\t\t\t\t<xs:element name=\"OverviewImage\" type=\"xs:string\" minOccurs=\"0\" />\r\n\t\t\t\t\t\t\t\t<xs:element name=\"isDisplayed\" type=\"xs:string\" minOccurs=\"0\" />\r\n\t\t\t\t\t\t\t</xs:sequence>\r\n\t\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t\t</xs:element>\r\n\t\t\t\t</xs:choice>\r\n\t\t\t</xs:complexType>\r\n\t\t</xs:element>\r\n\t</xs:schema>\r\n\t<Table1>\r\n\t\t<CarDataID>61</CarDataID>\r\n\t\t<VRM>B1014</VRM>\r\n\t\t<EventDateTime>2010-01-07T07:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR1.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>62</CarDataID>\r\n\t\t<VRM>B1015</VRM>\r\n\t\t<EventDateTime>2010-01-07T06:45:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR2.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>60</CarDataID>\r\n\t\t<VRM>B1013</VRM>\r\n\t\t<EventDateTime>2010-01-07T06:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR2.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>334</CarDataID>\r\n\t\t<VRM>A1001</VRM>\r\n\t\t<EventDateTime>2010-01-07T06:25:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR3.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>35</CarDataID>\r\n\t\t<VRM>A1002</VRM>\r\n\t\t<EventDateTime>2010-01-07T06:00:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR4.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>344</CarDataID>\r\n\t\t<VRM>A1012</VRM>\r\n\t\t<EventDateTime>2010-01-07T05:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR5.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>359</CarDataID>\r\n\t\t<VRM>B1012</VRM>\r\n\t\t<EventDateTime>2010-01-07T05:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR6.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>337</CarDataID>\r\n\t\t<VRM>A1004</VRM>\r\n\t\t<EventDateTime>2010-01-07T05:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR1.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>339</CarDataID>\r\n\t\t<VRM>A1006</VRM>\r\n\t\t<EventDateTime>2010-01-07T05:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR2.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>340</CarDataID>\r\n\t\t<VRM>A1007</VRM>\r\n\t\t<EventDateTime>2010-01-07T05:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR3.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>341</CarDataID>\r\n\t\t<VRM>A1008</VRM>\r\n\t\t<EventDateTime>2010-01-07T04:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR4.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>458</CarDataID>\r\n\t\t<VRM>B1011</VRM>\r\n\t\t<EventDateTime>2010-01-07T04:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR5.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>451</CarDataID>\r\n\t\t<VRM>B1003</VRM>\r\n\t\t<EventDateTime>2010-01-07T04:30:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR6.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>455</CarDataID>\r\n\t\t<VRM>B1007</VRM>\r\n\t\t<EventDateTime>2010-01-07T04:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR1.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>457</CarDataID>\r\n\t\t<VRM>B1010</VRM>\r\n\t\t<EventDateTime>2010-01-07T03:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR2.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>454</CarDataID>\r\n\t\t<VRM>B1006</VRM>\r\n\t\t<EventDateTime>2010-01-07T03:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR3.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>450</CarDataID>\r\n\t\t<VRM>B1002</VRM>\r\n\t\t<EventDateTime>2010-01-07T03:00:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR4.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>456</CarDataID>\r\n\t\t<VRM>B1008</VRM>\r\n\t\t<EventDateTime>2010-01-07T02:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR5.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>449</CarDataID>\r\n\t\t<VRM>B1001</VRM>\r\n\t\t<EventDateTime>2010-01-07T02:25:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR6.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>448</CarDataID>\r\n\t\t<VRM>B1000</VRM>\r\n\t\t<EventDateTime>2010-01-07T01:30:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR1.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>453</CarDataID>\r\n\t\t<VRM>B1005</VRM>\r\n\t\t<EventDateTime>2010-01-07T01:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR2.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>452</CarDataID>\r\n\t\t<VRM>B1004</VRM>\r\n\t\t<EventDateTime>2010-01-07T00:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR3.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t\r\n\t<Table1>\r\n\t\t<CarDataID>555</CarDataID>\r\n\t\t<VRM>C1007</VRM>\r\n\t\t<EventDateTime>2010-01-07T04:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR1.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>557</CarDataID>\r\n\t\t<VRM>C1010</VRM>\r\n\t\t<EventDateTime>2010-01-07T03:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR2.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>554</CarDataID>\r\n\t\t<VRM>C1006</VRM>\r\n\t\t<EventDateTime>2010-01-07T03:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR3.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>550</CarDataID>\r\n\t\t<VRM>C1002</VRM>\r\n\t\t<EventDateTime>2010-01-07T03:00:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR4.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>556</CarDataID>\r\n\t\t<VRM>C1008</VRM>\r\n\t\t<EventDateTime>2010-01-07T02:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR5.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>549</CarDataID>\r\n\t\t<VRM>C1001</VRM>\r\n\t\t<EventDateTime>2010-01-07T02:25:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR6.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t\r\n\t<Table1>\r\n\t\t<CarDataID>655</CarDataID>\r\n\t\t<VRM>D1007</VRM>\r\n\t\t<EventDateTime>2010-01-07T04:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR1.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>657</CarDataID>\r\n\t\t<VRM>D1010</VRM>\r\n\t\t<EventDateTime>2010-01-07T03:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR2.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>654</CarDataID>\r\n\t\t<VRM>D1006</VRM>\r\n\t\t<EventDateTime>2010-01-07T03:15:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR3.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>650</CarDataID>\r\n\t\t<VRM>D1002</VRM>\r\n\t\t<EventDateTime>2010-01-07T03:00:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR4.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>656</CarDataID>\r\n\t\t<VRM>D1008</VRM>\r\n\t\t<EventDateTime>2010-01-07T02:35:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR5.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n\t<Table1>\r\n\t\t<CarDataID>649</CarDataID>\r\n\t\t<VRM>D1001</VRM>\r\n\t\t<EventDateTime>2010-01-07T02:25:00+00:00</EventDateTime>\r\n\t\t<Direction>IN</Direction>\r\n\t\t<PlateImage>c:\\touchpark\\data\\cache\\images\\NumberPlateTransparent.png</PlateImage>\r\n\t\t<OverviewImage>c:\\touchpark\\data\\cache\\images\\CAR6.JPG</OverviewImage>\r\n\t\t<isDisplayed>Y</isDisplayed>\r\n\t</Table1>\r\n</NewDataSet>")]
    [DebuggerNonUserCode]
    public string DemoVehicleData
    {
      get
      {
        return (string) this[nameof (DemoVehicleData)];
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("Touch Screen To Start")]
    public string TouchScreenMessage
    {
      get
      {
        return (string) this[nameof (TouchScreenMessage)];
      }
      set
      {
        this[nameof (TouchScreenMessage)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public bool EnableTariffCategoryAccountHolder
    {
      get
      {
        return (bool) this[nameof (EnableTariffCategoryAccountHolder)];
      }
      set
      {
        this[nameof (EnableTariffCategoryAccountHolder)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool EnableTariffCategoryCar
    {
      get
      {
        return (bool) this[nameof (EnableTariffCategoryCar)];
      }
      set
      {
        this[nameof (EnableTariffCategoryCar)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool EnableTariffCategoryHGV
    {
      get
      {
        return (bool) this[nameof (EnableTariffCategoryHGV)];
      }
      set
      {
        this[nameof (EnableTariffCategoryHGV)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("True")]
    public bool EnableTariffCategoryHGVFood
    {
      get
      {
        return (bool) this[nameof (EnableTariffCategoryHGVFood)];
      }
      set
      {
        this[nameof (EnableTariffCategoryHGVFood)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool EnableTariffCategoryHGVTrailer
    {
      get
      {
        return (bool) this[nameof (EnableTariffCategoryHGVTrailer)];
      }
      set
      {
        this[nameof (EnableTariffCategoryHGVTrailer)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("True")]
    [DebuggerNonUserCode]
    public bool EnableNumberOfDaysChoice
    {
      get
      {
        return (bool) this[nameof (EnableNumberOfDaysChoice)];
      }
      set
      {
        this[nameof (EnableNumberOfDaysChoice)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool PaintVehicleRegistrationMarks
    {
      get
      {
        return (bool) this[nameof (PaintVehicleRegistrationMarks)];
      }
      set
      {
        this[nameof (PaintVehicleRegistrationMarks)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("Welcome To\r\nTouchPark")]
    [UserScopedSetting]
    public string CardDeviceWelcomeMessage
    {
      get
      {
        return (string) this[nameof (CardDeviceWelcomeMessage)];
      }
      set
      {
        this[nameof (CardDeviceWelcomeMessage)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("False")]
    [DebuggerNonUserCode]
    public bool GenerateDemoVehicleDataOnStartup
    {
      get
      {
        return (bool) this[nameof (GenerateDemoVehicleDataOnStartup)];
      }
      set
      {
        this[nameof (GenerateDemoVehicleDataOnStartup)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool PermitUploadAndCachingDisabled
    {
      get
      {
        return (bool) this[nameof (PermitUploadAndCachingDisabled)];
      }
      set
      {
        this[nameof (PermitUploadAndCachingDisabled)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public bool EnableTariffCategoryVisitor
    {
      get
      {
        return (bool) this[nameof (EnableTariffCategoryVisitor)];
      }
      set
      {
        this[nameof (EnableTariffCategoryVisitor)] = (object) value;
      }
    }

    [DefaultSettingValue("ServiceStationPark")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string ApplicationType
    {
      get
      {
        return (string) this[nameof (ApplicationType)];
      }
      set
      {
        this[nameof (ApplicationType)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("C:\\TouchPark\\TouchparkBGCPPLUSMOTO.jpg")]
    public string BackgroundImagePath
    {
      get
      {
        return (string) this[nameof (BackgroundImagePath)];
      }
      set
      {
        this[nameof (BackgroundImagePath)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("C:\\TouchPark\\TouchparkBGCPPLUSMOTO.jpg")]
    [DebuggerNonUserCode]
    public string StartScreenBackgroundImagePath
    {
      get
      {
        return (string) this[nameof (StartScreenBackgroundImagePath)];
      }
      set
      {
        this[nameof (StartScreenBackgroundImagePath)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("TRAVELODGE")]
    public string ThankYouCode
    {
      get
      {
        return (string) this[nameof (ThankYouCode)];
      }
      set
      {
        this[nameof (ThankYouCode)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("C:\\Program Files\\Ranger Services Ltd\\EAI\\Images\\")]
    public string CaptureImageDirectoryPath
    {
      get
      {
        return (string) this[nameof (CaptureImageDirectoryPath)];
      }
      set
      {
        this[nameof (CaptureImageDirectoryPath)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool EnableTariffCategoryStaff
    {
      get
      {
        return (bool) this[nameof (EnableTariffCategoryStaff)];
      }
      set
      {
        this[nameof (EnableTariffCategoryStaff)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public bool EnableTariffCategoryGuest
    {
      get
      {
        return (bool) this[nameof (EnableTariffCategoryGuest)];
      }
      set
      {
        this[nameof (EnableTariffCategoryGuest)] = (object) value;
      }
    }

    [DefaultSettingValue("Touch To Begin")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string StartScreenMessage
    {
      get
      {
        return (string) this[nameof (StartScreenMessage)];
      }
      set
      {
        this[nameof (StartScreenMessage)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("Data Source=.\\SQLEXPRESS;Initial Catalog=RangerServices.ParkingActivity;Integrated Security=True;User ID=parkingactivity;Password=P4rk1ng")]
    [DebuggerNonUserCode]
    public string DatabaseConnectionString
    {
      get
      {
        return (string) this[nameof (DatabaseConnectionString)];
      }
      set
      {
        this[nameof (DatabaseConnectionString)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool UseLivePermitUploadWebService
    {
      get
      {
        return (bool) this[nameof (UseLivePermitUploadWebService)];
      }
      set
      {
        this[nameof (UseLivePermitUploadWebService)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("Moto")]
    public string VehicleDataCaptureSite
    {
      get
      {
        return (string) this[nameof (VehicleDataCaptureSite)];
      }
      set
      {
        this[nameof (VehicleDataCaptureSite)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("9")]
    [UserScopedSetting]
    public double MaxNumberOfDaysToPermitFor
    {
      get
      {
        return (double) this[nameof (MaxNumberOfDaysToPermitFor)];
      }
      set
      {
        this[nameof (MaxNumberOfDaysToPermitFor)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    [UserScopedSetting]
    public bool EnableTariffCategoryCoach
    {
      get
      {
        return (bool) this[nameof (EnableTariffCategoryCoach)];
      }
      set
      {
        this[nameof (EnableTariffCategoryCoach)] = (object) value;
      }
    }

    private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
    {
    }

    private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
    {
    }
  }
}
