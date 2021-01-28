// Decompiled with JetBrains decompiler
// Type: TouchPark.CUpdateVehicleData
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace TouchPark
{
  public class CUpdateVehicleData
  {
    public int WriteTouchParkPermit(ParkingPermitInfo parkingPermit)
    {
      if (!Database.IsAccessible())
        return 0;
      try
      {
        SqlConnection databaseConnection = Database.DatabaseConnection;
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = databaseConnection;
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.CommandTimeout = 0;
        sqlCommand.CommandText = "insNewTouchParkPermit";
        SqlParameter sqlParameter1 = new SqlParameter();
        sqlParameter1.ParameterName = "@TouchParkPermitType";
        sqlParameter1.SqlDbType = SqlDbType.VarChar;
        sqlParameter1.Value = (object) parkingPermit.PermitType;
        sqlParameter1.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter1);
        SqlParameter sqlParameter2 = new SqlParameter();
        sqlParameter2.ParameterName = "@CaptureDateTime";
        sqlParameter2.SqlDbType = SqlDbType.DateTime;
        sqlParameter2.Value = (object) parkingPermit.CaptureDate;
        sqlParameter2.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter2);
        SqlParameter sqlParameter3 = new SqlParameter();
        sqlParameter3.ParameterName = "@VRM";
        sqlParameter3.SqlDbType = SqlDbType.VarChar;
        sqlParameter3.Value = (object) parkingPermit.VehicleRegMark;
        sqlParameter3.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter3);
        SqlParameter sqlParameter4 = new SqlParameter();
        sqlParameter4.ParameterName = "@StartDateTime";
        sqlParameter4.SqlDbType = SqlDbType.DateTime;
        sqlParameter4.Value = (object) parkingPermit.StartDate;
        sqlParameter4.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter4);
        SqlParameter sqlParameter5 = new SqlParameter();
        sqlParameter5.ParameterName = "@EndDateTime";
        sqlParameter5.SqlDbType = SqlDbType.DateTime;
        if (parkingPermit.EndDate == DateTime.MinValue)
          sqlParameter5.Value = (object) SqlDateTime.Null;
        else
          sqlParameter5.Value = (object) parkingPermit.EndDate;
        sqlParameter5.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter5);
        SqlParameter sqlParameter6 = new SqlParameter();
        sqlParameter6.ParameterName = "@PaymentType";
        sqlParameter6.SqlDbType = SqlDbType.VarChar;
        if (parkingPermit.PaymentType == "")
          sqlParameter6.Value = (object) SqlChars.Null;
        else
          sqlParameter6.Value = (object) parkingPermit.PaymentType;
        sqlParameter6.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter6);
        SqlParameter sqlParameter7 = new SqlParameter();
        sqlParameter7.ParameterName = "@Amount";
        sqlParameter7.SqlDbType = SqlDbType.Decimal;
        if (parkingPermit.Amount == new Decimal(0))
          sqlParameter7.Value = (object) SqlDecimal.Null;
        else
          sqlParameter7.Value = (object) parkingPermit.Amount;
        sqlParameter7.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter7);
        SqlParameter sqlParameter8 = new SqlParameter();
        sqlParameter8.ParameterName = "@Paid";
        sqlParameter8.SqlDbType = SqlDbType.Decimal;
        if (parkingPermit.Paid == new Decimal(0))
          sqlParameter8.Value = (object) SqlDecimal.Null;
        else
          sqlParameter8.Value = (object) parkingPermit.Paid;
        sqlParameter8.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter8);
        SqlParameter sqlParameter9 = new SqlParameter();
        sqlParameter9.ParameterName = "@AuthCode";
        sqlParameter9.SqlDbType = SqlDbType.VarChar;
        if (parkingPermit.AuthCode == "")
          sqlParameter9.Value = (object) SqlChars.Null;
        else
          sqlParameter9.Value = (object) parkingPermit.AuthCode;
        sqlParameter9.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter9);
        SqlParameter sqlParameter10 = new SqlParameter();
        sqlParameter10.ParameterName = "@PermitID";
        sqlParameter10.SqlDbType = SqlDbType.Int;
        sqlParameter10.Direction = ParameterDirection.Output;
        sqlCommand.Parameters.Add(sqlParameter10);
        SqlParameter sqlParameter11 = new SqlParameter();
        sqlParameter11.ParameterName = "@MachineName";
        sqlParameter11.SqlDbType = SqlDbType.VarChar;
        sqlParameter11.Direction = ParameterDirection.Input;
        if (parkingPermit.MachineName == "")
          sqlParameter11.Value = (object) SqlChars.Null;
        else
          sqlParameter11.Value = (object) parkingPermit.MachineName;
        sqlCommand.Parameters.Add(sqlParameter11);
        SqlParameter sqlParameter12 = new SqlParameter();
        sqlParameter12.ParameterName = "@userName";
        sqlParameter12.SqlDbType = SqlDbType.VarChar;
        sqlParameter12.Direction = ParameterDirection.Input;
        if (parkingPermit.UserName == "")
          sqlParameter12.Value = (object) SqlChars.Null;
        else
          sqlParameter12.Value = (object) parkingPermit.UserName;
        sqlCommand.Parameters.Add(sqlParameter12);
        SqlParameter sqlParameter13 = new SqlParameter();
        sqlParameter13.ParameterName = "@userInformationID";
        sqlParameter13.SqlDbType = SqlDbType.Int;
        sqlParameter13.Direction = ParameterDirection.Input;
        sqlParameter13.Value = (object) parkingPermit.UserID;
        sqlCommand.Parameters.Add(sqlParameter13);
        databaseConnection.Open();
        sqlCommand.ExecuteNonQuery();
        databaseConnection.Close();
        return (int) sqlCommand.Parameters["@PermitID"].Value;
      }
      catch (SqlException ex)
      {
        if (ex.Message.Contains("The server was not found or was not accessible."))
          return 0;
        if (ex.Message.Contains("The INSERT statement conflicted with the CHECK constraint"))
        {
          LogData logData = new LogData();
          logData.Title = "Permit Update Failed.";
          logData.Message = (string.Format("Check constraint failed when trying to insert a new Permit into the database.\n\rPermitType: '{0}'Payment Type: {1}\n\rException Message:{2}\n\rStack Trace:{3}\n\r", (object) parkingPermit.PermitType, (object) parkingPermit.PaymentType, (object) ex.Message, (object) ex.StackTrace));
          logData.Categories.Clear();
          logData.Categories.Add("Exception");
          Log.Write(logData);
          return 0;
        }
        Log.Write((Exception) ex);
        return 0;
      }
      catch (Exception ex)
      {
        Log.Write(ex);
        return 0;
      }
    }

    public bool WriteUpdateDisplayRecord(ParkingPermitInfo parkingPermit)
    {
      if (!Database.IsAccessible())
        return false;
      try
      {
        SqlConnection databaseConnection = Database.DatabaseConnection;
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = databaseConnection;
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.CommandTimeout = 0;
        sqlCommand.CommandText = "updCarsToNotDisplay";
        SqlParameter sqlParameter = new SqlParameter();
        sqlParameter.ParameterName = "@VRM";
        sqlParameter.SqlDbType = SqlDbType.VarChar;
        sqlParameter.Value = (object) parkingPermit.VehicleRegMark;
        sqlParameter.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter);
        databaseConnection.Open();
        sqlCommand.ExecuteNonQuery();
        databaseConnection.Close();
        return true;
      }
      catch (Exception ex)
      {
        Log.Write(ex);
        return false;
      }
    }
  }
}
