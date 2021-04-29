// Decompiled with JetBrains decompiler
// Type: TouchPark.CCacheData
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using TouchPark.Properties;
using TouchPark.Services;

namespace TouchPark
{
  public class CCacheData
  {
    private static string WEBSERVICE_LIVE_URL = ConfigurationManager.AppSettings["RangerLiveURL"].ToString();
    private static string WEBSERVICE_TEST_URL = ConfigurationManager.AppSettings["RangerTestURL"].ToString();
    private static object _WriteVehicleDataToXmlLocker = new object();
    private static object _TryCopyImageToCacheIfItDoesNotExistLocker = new object();
    private static object _ObtainVehicleDataTableLocker = new object();
    private static object _CacheUserInfoDataFromDatabaseLocker = new object();
    private static object _CreateEmptyVehicleDataCacheIfNotExistsLocker = new object();
    private static object _CreateDefaultUserInfoDataCacheIfNotExistsLocker = new object();
    private static object _FillVehicleDataTableFromXMLLocker = new object();
    private static object _isValidPassCodeLock = new object();
    private static object _WriteParkingPermitToXmlDataTableLocker = new object();
    private static object _GetVehicleInfoListFromXmlDataTableLocker = new object();
    private static object _CreatePermitfileLocker = new object();
    private static object _UploadPermitFilesLocker = new object();
    private static object _TryUploadFileLocker = new object();
    private static object _ClearCacheImagesLocker = new object();
    private static object _DeleteProcessedPermitFolderLock = new object();

    private MovementService _movementService;

    public CCacheData()
    {
      _movementService = new MovementService();
    }

    public static void CacheVehicleDataFromDatabase()
    {
      CCacheData.WriteVehicleDataToXml();
    }

    internal static void WriteVehicleDataToXml()
    {
      CCacheData.WriteVehicleDataToXml(CCacheData.ObtainVehicleDataTable());
    }

    internal static void WriteVehicleDataToXml(DataTable dataTable)
    {
      try
      {
        lock (CCacheData._WriteVehicleDataToXmlLocker)
        {
          CCacheData.EnsureCacheDirectoriesExist();
          DataTable dataTable1 = dataTable;
          if (dataTable1 == null)
            return;
          DataTable table = dataTable1.Clone();
          DataSet dataSet = new DataSet();
          string str1 = ConfigurationManager.AppSettings["cacheLocation"].ToString();
          string str2 = ConfigurationManager.AppSettings["cacheImageLocation"].ToString();
          string imageDirectoryPath = Settings.Default.CaptureImageDirectoryPath;
          Console.WriteLine("Source Image Path: {0}\n Dest. Image Path {1}", (object) imageDirectoryPath, (object) str2);
          int index = 0;
          Console.WriteLine("Copying and referencing vehicle images...");
          Console.WriteLine("Begin");
          Log.Write("Copying and referencing vehicle images...");
          Log.Write("num of cars : " + dataTable1.Rows.Count.ToString());
          for (; index < dataTable1.Rows.Count; ++index)
          {
            string fileName1 = dataTable1.Rows[index]["OverviewImage"].ToString().ToUpper().Replace(imageDirectoryPath, "");
            string fileName2 = dataTable1.Rows[index]["PlateImage"].ToString().ToUpper().Replace(imageDirectoryPath, "");
            Console.Write("Overview image is : {0}", (object) fileName1);
            if (CCacheData.TryCopyImageToCacheIfItDoesNotExist(fileName1) & CCacheData.TryCopyImageToCacheIfItDoesNotExist(fileName2))
            {
              DataRow row = dataTable1.Rows[index];
              table.ImportRow(row);
              Console.WriteLine("...referenced ok :-)");
            }
            else
            {
              Log.Write("images not found.");
              Console.WriteLine("...not referenced :-(");
            }
          }
          Console.WriteLine("End");
          if (table.Rows.Count <= 0)
            return;
          dataSet.Tables.Add(table);
          Console.WriteLine("Writing {0} rows of Vehicle Data to {1}DataTable.xml", (object) table.Rows.Count, (object) str1);
          dataSet.WriteXml(str1 + "DataTable.xml", XmlWriteMode.WriteSchema);
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    private static bool TryCopyImageToCacheIfItDoesNotExist(string fileName)
    {
      try
      {
        lock (CCacheData._TryCopyImageToCacheIfItDoesNotExistLocker)
        {
          Console.WriteLine("Trying to copy...");
          if (fileName == null)
          {
            Console.WriteLine("File name is null!");
            return false;
          }
          Console.WriteLine("Trying to copy {0}", (object) fileName);
          string fullPath1 = Path.GetFullPath(string.Format("{0}\\", (object) ConfigurationManager.AppSettings["cacheImageLocation"]));
          string fullPath2 = Path.GetFullPath(string.Format("{0}\\", (object) Settings.Default.CaptureImageDirectoryPath));
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          fileName = fileName.ToUpper();
          string str = !fileName.Contains("\\") ? string.Format("{0}{1}", (object) fullPath1, (object) fileName) : fileName;
          Console.WriteLine("Full path = {0}", (object) str);
          if (!System.IO.File.Exists(str))
          {
            string empty3 = string.Empty;
            string fileName1 = Path.GetFileName(str);
            string sourceFileName = string.Format("{0}{1}", (object) fullPath2, (object) fileName1);
            Console.WriteLine(".        > trying to copy from {0}\n.      ...to {1}", (object) sourceFileName, (object) str);
            System.IO.File.Copy(sourceFileName, str);
          }
          else
            Console.WriteLine("File already exists!");
          return System.IO.File.Exists(str);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception when attempting to copy!\n{0}", (object) ex.Message);
        return false;
      }
    }

    private static DataTable ObtainVehicleDataTable()
    {
      try
      {
        lock (CCacheData._ObtainVehicleDataTableLocker)
          return new CGetVehicleDataForCache().GetDataforCache();
      }
      catch (Exception ex)
      {
        Log.Write(ex);
        return (DataTable) null;
      }
    }

    private static void EnsureCacheDirectoriesExist()
    {
      try
      {
        string path1 = ConfigurationManager.AppSettings["cacheLocation"].ToString();
        string path2 = ConfigurationManager.AppSettings["cacheImageLocation"].ToString();
        string imageDirectoryPath = Settings.Default.CaptureImageDirectoryPath;
        if (!Directory.Exists(path1))
          Directory.CreateDirectory(path1);
        if (!Directory.Exists(path2))
          Directory.CreateDirectory(path2);
        if (Directory.Exists(imageDirectoryPath))
          return;
        Directory.CreateDirectory(imageDirectoryPath);
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    public static void CacheUserInfoDataFromDatabase()
    {
      try
      {
        lock (CCacheData._CacheUserInfoDataFromDatabaseLocker)
        {
          Console.WriteLine("Caching user info data.");          
          // DataTable dataforCacheUserInfo = new CGetVehicleDataForCache().GetDataforCacheUserInfoFromConfig();
          DataTable dataforCacheUserInfo = new CGetVehicleDataForCache().GetDataforCacheUserInfo();
          DataSet dataSet = new DataSet();
          string path = ConfigurationManager.AppSettings["cacheLocation"].ToString();
          if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
          if (dataforCacheUserInfo != null && dataforCacheUserInfo.Rows.Count != 0)
          {
            string str = string.Format("{0}DataUserTable.xml", (object) path);
            if (System.IO.File.Exists(str))
            {
              Console.WriteLine("User info data file exists already. Deleting.");
              System.IO.File.Delete(str);
              Console.WriteLine("Deleted. ");
            }
            dataSet.Tables.Add(dataforCacheUserInfo);
            dataSet.WriteXml(str, XmlWriteMode.WriteSchema);
            Console.WriteLine("Cached User Info Data...OK [{0}] records written to cache [{1}].", (object) dataforCacheUserInfo.Rows.Count, (object) str);
          }
          else
            Console.WriteLine("Cached data not written to, no records found in db to write!");
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    public static DataTable CreateEmptyVehicleDataCacheIfNotExists()
    {
      DataTable table = (DataTable) null;
      DataSet dataSet = (DataSet) null;
      string path = (string) null;
      try
      {
        lock (CCacheData._CreateDefaultUserInfoDataCacheIfNotExistsLocker)
        {
          table = new DataTable("Table1");
          dataSet = new DataSet();
          path = ConfigurationManager.AppSettings["cacheLocation"].ToString();
          table.Columns.Add(new DataColumn("CarDataID", typeof (int)));
          table.Columns.Add(new DataColumn("VRM", typeof (string)));
          table.Columns.Add(new DataColumn("EventDateTime", typeof (DateTime)));
          table.Columns.Add(new DataColumn("Direction", typeof (string)));
          table.Columns.Add(new DataColumn("PlateImage", typeof (string)));
          table.Columns.Add(new DataColumn("OverviewImage", typeof (string)));
          table.Columns.Add(new DataColumn("isDisplayed", typeof (string)));
          if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      try
      {
        if (System.IO.File.Exists(path + "DataTable.xml"))
          return table;
        dataSet.Tables.Add(table);
        dataSet.WriteXml(path + "DataTable.xml", XmlWriteMode.WriteSchema);
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      return table;
    }

        public static void CreateDefaultUserInfoDataCacheIfNotExists()
        {
            try
            {
                lock (CCacheData._CreateDefaultUserInfoDataCacheIfNotExistsLocker)
                {
                    string path = ConfigurationManager.AppSettings["cacheLocation"].ToString();
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string str = string.Format("{0}DataUserTable.xml", (object)path);
                    if (System.IO.File.Exists(str))
                        return;
                    //string codeDataFilePath = Settings.Default.DummyUserCodeDataFilePath;
                    //if (codeDataFilePath != null && System.IO.File.Exists(codeDataFilePath))
                    //{
                    //  System.IO.File.Copy(codeDataFilePath, str, true);
                    //}
                    //else
                    // {
                    DataTable table = new DataTable("Table1");
                    DataSet dataSet = new DataSet();
                    DataTable dataforCacheUserInfo = new CGetVehicleDataForCache().GetDataforCacheUserInfoFromConfig();
                    //table.Columns.Add(new DataColumn("username", typeof (string)));
                    //table.Columns.Add(new DataColumn("passcode", typeof (string)));
                    //table.Columns.Add(new DataColumn("userInformationID", typeof (int)));
                    //DataRow row = table.NewRow();
                    //row["username"] = (object) "Test";
                    //row["passcode"] = (object) "0450";
                    //row["UserInformationID"] = (object) "9999";
                    //table.Rows.Add(row);
                    //if (table.Rows.Count == 0)
                    //  return;
                    dataSet.Tables.Add(dataforCacheUserInfo);
                    dataSet.WriteXml(path + "DataUserTable.xml", XmlWriteMode.WriteSchema);
                    //}
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        public static void CreateTestPermit()
    {
      double testPermitInterval = Settings.Default.TestPermitInterval;
      CCacheData.WriteParkingPermitToXmlFile(new ParkingPermitInfo()
      {
        Amount = new Decimal(0),
        AuthCode = (string) null,
        CaptureDate = DateTime.Now,
        EndDate = DateTime.Now.AddHours(testPermitInterval),
        MachineName = Environment.MachineName,
        Paid = new Decimal(0),
        PassCode = "-2",
        PermitType = "TEST",
        PaymentType = "TEST",
        StartDate = DateTime.Now,
        UserID = 0,
        UserName = "TEST",
        VehicleRegMark = "$HEARTBEAT"
      });
    }

    public DataTable FillVehicleDataTableFromXML()
    {
      DataTable dataTable = (DataTable) null;
      try
      {
        lock (CCacheData._FillVehicleDataTableFromXMLLocker)
        {
          string fileName = ConfigurationManager.AppSettings["cacheLocation"].ToString() + "DataTable.xml";
          DataSet dataSet = new DataSet("Ranger");
          int num = (int) dataSet.ReadXml(fileName);
          DataTable table = dataSet.Tables[0];
          table.DefaultView.Sort = "EventDateTime Asc";
          dataTable = table;
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      return dataTable;
    }

    public static DataRow isValidPassCode(string passcode)
    {
      DataTable dataTable = new DataTable();
      DataRow dataRow = (DataRow) null;
      try
      {
        lock (CCacheData._isValidPassCodeLock)
        {
          string fileName = ConfigurationManager.AppSettings["cacheLocation"].ToString() + "DataUserTable.xml";
          DataSet dataSet = new DataSet("UserInfo");
          int num = (int) dataSet.ReadXml(fileName);
          DataTable table = dataSet.Tables[0];
          table.Constraints.Add("PK", table.Columns[nameof (passcode)], true);
          dataRow = table.Rows.Find((object) passcode);
          return dataRow;
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      return dataRow;
    }

    public static bool WriteParkingPermitToXmlFile(ParkingPermitInfo parkingPermit)
    {
      if (string.IsNullOrEmpty(parkingPermit.PermitType))
        parkingPermit.PermitType = "STAFF";
      XmlTextWriter xmlTextWriter = (XmlTextWriter) null;
      try
      {
        lock (CCacheData._WriteParkingPermitToXmlDataTableLocker)
        {
          string path = ConfigurationManager.AppSettings["cachePermitLocation"].ToString();
          if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
          xmlTextWriter = new XmlTextWriter(path + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml", Encoding.UTF8);
          xmlTextWriter.Formatting = Formatting.Indented;
          xmlTextWriter.WriteStartDocument();
          xmlTextWriter.WriteStartElement("Permit");
          xmlTextWriter.WriteStartElement("userID");
          xmlTextWriter.WriteString(parkingPermit.UserID.ToString());
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("userName");
          xmlTextWriter.WriteString(parkingPermit.UserName);
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("MachineName");
          xmlTextWriter.WriteString(parkingPermit.MachineName);
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("PermitType");
          xmlTextWriter.WriteString(parkingPermit.PermitType);
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("VRM");
          xmlTextWriter.WriteString(parkingPermit.VehicleRegMark);
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("PaymentType");
          xmlTextWriter.WriteString(parkingPermit.PaymentType);
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("CaptureDate");
          xmlTextWriter.WriteString(parkingPermit.CaptureDate.ToString());
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("StartDate");
          xmlTextWriter.WriteString(parkingPermit.StartDate.ToString());
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("AuthCode");
          xmlTextWriter.WriteString(parkingPermit.AuthCode);
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("Amount");
          xmlTextWriter.WriteString(parkingPermit.Amount.ToString());
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("EndDate");
          xmlTextWriter.WriteString(parkingPermit.EndDate.ToString());
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("Paid");
          xmlTextWriter.WriteString(parkingPermit.Paid.ToString());
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteStartElement("Passcode");
          if (parkingPermit.PassCode != null)
            xmlTextWriter.WriteString(parkingPermit.PassCode.ToString());
          else
            xmlTextWriter.WriteString("0");
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteEndElement();
          xmlTextWriter.WriteEndDocument();
          return true;
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
        return false;
      }
      finally
      {
        xmlTextWriter.Close();
      }
    }

        public List<CVehicleInfo> GetMatchingVRMFromService(string vrm)
        {
            List<CVehicleInfo> cvehicleInfoList = new List<CVehicleInfo>();
            try
            {
                var result = _movementService.SearchVehicleMovements(vrm);
                foreach (var item in result)
                {
                    cvehicleInfoList.Add(new CVehicleInfo()
                    {
                        CarDataId = item.ID,
                        overviewImageLocation = item.OverviewImage.ToString(),
                        plateImageLocation = item.PlateImage.ToString(),
                        VRM = item.VRN.ToString(),
                        inwardTime = item.TimeStamp.ToString()
                    });

                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
            return cvehicleInfoList;
        }

        public List<CVehicleInfo> GetVehicleInfoListFromXmlDataTable(
      string timeparameter,
      string Direction)
    {
      List<CVehicleInfo> cvehicleInfoList = new List<CVehicleInfo>();
      CVehicleInfo cvehicleInfo = new CVehicleInfo();
      try
      {
        lock (CCacheData._GetVehicleInfoListFromXmlDataTableLocker)
        {
          DataTable dataTable = this.FillVehicleDataTableFromXML();
          if (dataTable == null)
            return cvehicleInfoList;
          switch (Direction)
          {
            case "HISTORY":
              for (int index = 0; index < dataTable.Rows.Count; ++index)
              {
                if (DateTime.Parse(dataTable.Rows[index]["EventDateTime"].ToString()) > DateTime.Parse(timeparameter))
                {
                  DataRow row = dataTable.Rows[index];
                  dataTable.Rows.Remove(row);
                }
              }
              dataTable.DefaultView.Sort = "EventDateTime DESC";
              break;
            case "FUTURE":
              for (int index = 0; index < dataTable.Rows.Count; ++index)
              {
                if (DateTime.Parse(dataTable.Rows[index]["EventDateTime"].ToString()) < DateTime.Parse(timeparameter))
                {
                  DataRow row = dataTable.Rows[index];
                  dataTable.Rows.Remove(row);
                }
              }
              dataTable.DefaultView.Sort = "EventDateTime ASC";
              break;
          }
          if (dataTable.Rows.Count == 0)
            return cvehicleInfoList;
          int num = 16;
          if (dataTable.Rows.Count < 16)
            num = dataTable.Rows.Count;
          for (int index = 0; index < num; ++index)
            cvehicleInfoList.Add(new CVehicleInfo()
            {
              CarDataId = int.Parse(dataTable.Rows[index]["CarDataID"].ToString()),
              overviewImageLocation = dataTable.Rows[index]["OverviewImage"].ToString(),
              plateImageLocation = dataTable.Rows[index]["PlateImage"].ToString(),
              VRM = dataTable.Rows[index]["VRM"].ToString(),
              inwardTime = dataTable.Rows[index]["EventDateTime"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      return cvehicleInfoList;
    }

    public List<CVehicleInfo> GetMatchingVRMFromXML(string partialPlate)
    {
      List<CVehicleInfo> cvehicleInfoList = new List<CVehicleInfo>();
      DataTable dataTable = this.FillVehicleDataTableFromXML();
      if (dataTable == null)
        return cvehicleInfoList;
      try
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["VRM"].ToString().Contains(partialPlate))
            cvehicleInfoList.Add(new CVehicleInfo()
            {
              CarDataId = int.Parse(row["CarDataID"].ToString()),
              overviewImageLocation = row["OverviewImage"].ToString(),
              plateImageLocation = row["PlateImage"].ToString(),
              VRM = row["VRM"].ToString(),
              inwardTime = row["EventDateTime"].ToString()
            });
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      return cvehicleInfoList;
    }

    public static FileInfo[] FolderFileList(string pSourcePath, string pSearchString)
    {
      try
      {
        DirectoryInfo directoryInfo = new DirectoryInfo(pSourcePath);
        if (!directoryInfo.Exists)
        {
          try
          {
            directoryInfo.Create();
          }
          catch (Exception ex)
          {
            Log.Write(ex);
            return (FileInfo[]) null;
          }
        }
        return directoryInfo.GetFiles(pSearchString);
      }
      catch (Exception ex)
      {
        Log.Write(ex);
        return new FileInfo[0];
      }
    }

    public static bool CreatePermitfile()
    {
      try
      {
        lock (CCacheData._CreatePermitfileLocker)
        {
          string appSetting = ConfigurationManager.AppSettings["exportpath"];
          if (!Directory.Exists(appSetting))
            Directory.CreateDirectory(appSetting);
          string environmentVariable1 = Environment.GetEnvironmentVariable("RSSITECODE");
          string environmentVariable2 = Environment.GetEnvironmentVariable("RSSITENAME");
          List<ParkingPermitInfo> parkingPermitInfoList = new List<ParkingPermitInfo>();
          DataTable dataTable = new DataTable();
          string str1 = ConfigurationManager.AppSettings["cachePermitLocation"].ToString();
          if (!Directory.Exists(str1))
            Directory.CreateDirectory(str1);
          FileInfo[] fileInfoArray = CCacheData.FolderFileList(str1, "*.xml");
          foreach (FileInfo fileInfo in fileInfoArray)
          {
            try
            {
              DataSet dataSet = new DataSet("Ranger");
              int num = (int) dataSet.ReadXml(fileInfo.FullName);
              DataTable table = dataSet.Tables[0];
              for (int index = 0; index < table.Rows.Count; ++index)
              {
                ParkingPermitInfo parkingPermitInfo = new ParkingPermitInfo();
                parkingPermitInfo.UserID = int.Parse(table.Rows[index]["userID"].ToString());
                parkingPermitInfo.UserName = table.Rows[index]["userName"].ToString();
                parkingPermitInfo.MachineName = table.Rows[index]["MachineName"].ToString();
                parkingPermitInfo.PermitType = table.Rows[index]["PermitType"].ToString();
                parkingPermitInfo.PaymentType = table.Rows[index]["PaymentType"].ToString();
                parkingPermitInfo.AuthCode = table.Rows[index]["AuthCode"].ToString();
                parkingPermitInfo.VehicleRegMark = table.Rows[index]["VRM"].ToString();
                parkingPermitInfo.CaptureDate = DateTime.Parse(table.Rows[index]["CaptureDate"].ToString());
                parkingPermitInfo.StartDate = DateTime.Parse(table.Rows[index]["StartDate"].ToString());
                parkingPermitInfo.Amount = Decimal.Parse(table.Rows[index]["Amount"].ToString());
                parkingPermitInfo.EndDate = DateTime.Parse(table.Rows[index]["EndDate"].ToString());
                parkingPermitInfo.Paid = Decimal.Parse(table.Rows[index]["Paid"].ToString());
                if (table.Columns.Contains("passcode"))
                  parkingPermitInfo.PassCode = table.Rows[index]["passcode"].ToString();
                parkingPermitInfoList.Add(parkingPermitInfo);
              }
            }
            catch (Exception ex)
            {
              Log.Write(ex);
            }
          }
          if (parkingPermitInfoList.Count > 0)
          {
            string str2 = string.Format("{0}\\{1}.xml.export", (object) appSetting, (object) DateTime.Now.ToString("yyyyMMddHHmmss"));
            XmlTextWriter xmlTextWriter1 = (XmlTextWriter) null;
            XmlTextWriter xmlTextWriter2 = new XmlTextWriter(str2, Encoding.UTF8);
            xmlTextWriter2.Formatting = Formatting.Indented;
            xmlTextWriter2.Indentation = 4;
            xmlTextWriter2.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            xmlTextWriter2.WriteStartElement("xml");
            xmlTextWriter2.WriteAttributeString("type", "touchpark_kiosk_permit");
            xmlTextWriter2.WriteStartElement("kiosk");
            xmlTextWriter2.WriteAttributeString("site_code", environmentVariable1);
            xmlTextWriter2.WriteAttributeString("site", environmentVariable2);
            xmlTextWriter2.WriteAttributeString("machine_name", Environment.MachineName);
            foreach (ParkingPermitInfo parkingPermitInfo in parkingPermitInfoList)
            {
              xmlTextWriter2.WriteStartElement("permit");
              if (parkingPermitInfo.PermitType != null)
                xmlTextWriter2.WriteAttributeString("type", parkingPermitInfo.PermitType);
              DateTime captureDate = parkingPermitInfo.CaptureDate;
              xmlTextWriter2.WriteAttributeString("capture_date_time", parkingPermitInfo.CaptureDate.ToString("yyyy-MM-dd HH:mm:ss"));
              if (parkingPermitInfo.PaymentType != null)
                xmlTextWriter2.WriteAttributeString("payment_type", parkingPermitInfo.PaymentType);
              if (parkingPermitInfo.VehicleRegMark != null)
                xmlTextWriter2.WriteAttributeString("vrm", parkingPermitInfo.VehicleRegMark);
              DateTime startDate = parkingPermitInfo.StartDate;
              xmlTextWriter2.WriteAttributeString("start_date_time", parkingPermitInfo.StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
              if (parkingPermitInfo.PermitType != null && parkingPermitInfo.PermitType.ToUpper().Equals("STAFF"))
              {
                xmlTextWriter2.WriteAttributeString("end_date_time", "9999-12-31 23:59:59");
              }
              else
              {
                DateTime endDate = parkingPermitInfo.EndDate;
                xmlTextWriter2.WriteAttributeString("end_date_time", parkingPermitInfo.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
              }
              if (parkingPermitInfo.Amount != new Decimal(0))
                xmlTextWriter2.WriteAttributeString("amount_due", parkingPermitInfo.Amount.ToString());
              if (parkingPermitInfo.Paid != new Decimal(0))
                xmlTextWriter2.WriteAttributeString("amount_paid", parkingPermitInfo.Paid.ToString());
              if (parkingPermitInfo.AuthCode != null)
                xmlTextWriter2.WriteAttributeString("auth_code", parkingPermitInfo.AuthCode);
              if (parkingPermitInfo.UserID != 0)
                xmlTextWriter2.WriteAttributeString("user_id", parkingPermitInfo.UserID.ToString());
              if (parkingPermitInfo.PassCode != null)
                xmlTextWriter2.WriteAttributeString("passcode", parkingPermitInfo.PassCode);
              xmlTextWriter2.WriteEndElement();
            }
            xmlTextWriter2.WriteEndElement();
            xmlTextWriter2.WriteEndElement();
            xmlTextWriter2.Flush();
            xmlTextWriter2.Close();
            xmlTextWriter1 = (XmlTextWriter) null;
            new FileInfo(str2).MoveTo(str2.Replace(".xml.export", ".xml"));
            foreach (FileInfo fileInfo in fileInfoArray)
            {
              try
              {
                fileInfo.Delete();
              }
              catch
              {
              }
            }
          }
          return true;
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
        return false;
      }
    }

    private static string HTTPPostXML(string url, string postData)
    {
      string str = "";
      try
      {
        byte[] bytes = new ASCIIEncoding().GetBytes(postData);
        Log.Write("posting data : " + postData);
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.Timeout = 5000;
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "text/xml";
        httpWebRequest.ContentLength = (long) bytes.Length;
        Stream requestStream = httpWebRequest.GetRequestStream();
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Close();
        Console.Write("...POST xml cached permit...");
        HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
        if (response.StatusCode != HttpStatusCode.OK)
        {
          Console.WriteLine("[failed] server responded {0}, {1}", (object) response.StatusCode.ToString(), (object) response.StatusDescription);
          throw new Exception(string.Format("Status Code: {0}\nDescription: {1}", (object) response.StatusCode.ToString(), (object) response.StatusDescription));
        }
        Stream responseStream = response.GetResponseStream();
        StreamReader streamReader = new StreamReader(responseStream);
        char[] buffer = new char[256];
        for (int length = streamReader.Read(buffer, 0, 256); length > 0; length = streamReader.Read(buffer, 0, 256))
          str += new string(buffer, 0, length);
        streamReader.Close();
        responseStream.Close();
        response.Close();
        Console.WriteLine("...success!");
      }
      catch (Exception ex)
      {
        Console.WriteLine("...failed with exceptions:-");
        Console.WriteLine("Exception : {0}", (object) ex.Message);
        Log.Write(ex);
      }
      return str;
    }

    public static void UploadPermitFiles()
    {
      try
      {
        lock (CCacheData._UploadPermitFilesLocker)
        {
          string appSetting = ConfigurationManager.AppSettings["exportpath"];
          if (appSetting == null)
            throw new Exception("'exportpath' AppSettings does not exist.");
          FileInfo[] files = new DirectoryInfo(appSetting).GetFiles("*.xml");
          if (files.Length > 0)
            Console.WriteLine("Uploading [{1}] permit files found in {0}.", (object) appSetting, (object) files.Length);
          foreach (FileInfo fileInfo in files)
            CCacheData.TryUploadFile(fileInfo);
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    private static void TryUploadFile(FileInfo fileInfo)
    {
      try
      {
        lock (CCacheData._TryUploadFileLocker)
        {
          string url = CCacheData.WEBSERVICE_TEST_URL;
          if (Settings.Default.UseLivePermitUploadWebService)
            url = CCacheData.WEBSERVICE_LIVE_URL;
          string fullName = fileInfo.FullName;
          Console.Write(".......Uploading {0} ", (object) fullName);
          StreamReader streamReader = new StreamReader(fullName);
          string end = streamReader.ReadToEnd();
          streamReader.BaseStream.Close();
          streamReader.BaseStream.Dispose();
          string str = CCacheData.HTTPPostXML(url, end);
          if (str.Contains("This is a restricted page!"))
          {
            string destFileName = string.Format("{0}\\{1}", (object) CCacheData.UploadedPermitFolder.FullName, (object) fileInfo.Name);
            Console.WriteLine("Attempting to move {0} to {1}", (object) fileInfo.FullName, (object) destFileName);
            fileInfo.MoveTo(destFileName);
            Console.Write("....OK :-)");
          }
          else
          {
            Console.Write("failed :-( [{0}]", (object) str);
            Log.Write("ErrorEventArgs writing to url : " + url);
            Log.Write("response: " + str);
            Utilites.LogError("UploadPermitFile", "Problem uploading permit\n" + str);
          }
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    public static void ClearCacheImages()
    {
      try
      {
        lock (CCacheData._ClearCacheImagesLocker)
        {
          FileInfo[] fileInfoArray = CCacheData.FolderFileList(ConfigurationManager.AppSettings["cacheImageLocation"].ToString(), "*.*");
          DateTime dateTime = DateTime.Now.AddHours(-36.0);
          foreach (FileInfo fileInfo in fileInfoArray)
          {
            if (fileInfo.CreationTime > dateTime)
            {
              try
              {
                fileInfo.Delete();
              }
              catch
              {
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }

    private static DirectoryInfo ExportFolder
    {
      get
      {
        DirectoryInfo directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["exportpath"]);
        if (!directoryInfo.Exists)
          directoryInfo.Create();
        return directoryInfo;
      }
    }

    private static DirectoryInfo UploadedPermitFolder
    {
      get
      {
        DirectoryInfo directoryInfo = new DirectoryInfo(Settings.Default.UploadedPermitFolder);
        if (!directoryInfo.Exists)
          directoryInfo.Create();
        return directoryInfo;
      }
    }

    internal static void DeleteProcessedPermitFolder()
    {
      try
      {
        lock (CCacheData._DeleteProcessedPermitFolderLock)
          CCacheData.UploadedPermitFolder.Delete();
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
    }
  }
}
