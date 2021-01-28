// Decompiled with JetBrains decompiler
// Type: TouchPark.CGetVehicleData
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TouchPark
{
  public class CGetVehicleData
  {
    public List<CVehicleInfo> GetTopSixteen(string timeparameter, string direction)
    {
      List<CVehicleInfo> cvehicleInfoList = new List<CVehicleInfo>();
      if (!Database.IsAccessible())
        return cvehicleInfoList;
      CVehicleInfo cvehicleInfo = new CVehicleInfo();
      try
      {
        SqlConnection databaseConnection = Database.DatabaseConnection;
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = databaseConnection;
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.CommandTimeout = 0;
        sqlCommand.CommandText = "selAllCarDetailsFromDateAndOnlyDisplayed";
        SqlParameter sqlParameter1 = new SqlParameter();
        sqlParameter1.ParameterName = "@DisplayTime";
        sqlParameter1.SqlDbType = SqlDbType.DateTime;
        sqlParameter1.Value = (object) timeparameter;
        sqlParameter1.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter1);
        SqlParameter sqlParameter2 = new SqlParameter();
        sqlParameter2.ParameterName = "@Direction";
        sqlParameter2.SqlDbType = SqlDbType.VarChar;
        sqlParameter2.Value = (object) direction;
        sqlParameter2.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter2);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        DataTable dataTable = new DataTable();
        sqlDataAdapter.SelectCommand = sqlCommand;
        databaseConnection.Open();
        sqlDataAdapter.Fill(dataTable);
        databaseConnection.Close();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          cvehicleInfoList.Add(new CVehicleInfo()
          {
            CarDataId = int.Parse(dataTable.Rows[index]["CarDataId"].ToString()),
            overviewImageLocation = dataTable.Rows[index]["OverviewImage"].ToString(),
            plateImageLocation = dataTable.Rows[index]["PlateImage"].ToString(),
            VRM = dataTable.Rows[index]["VRM"].ToString(),
            inwardTime = dataTable.Rows[index]["EventDateTime"].ToString()
          });
      }
      catch (SqlException ex)
      {
        if (ex.Message.Contains("The server was not found or was not accessible."))
          return cvehicleInfoList;
        Log.Write((Exception) ex);
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      return cvehicleInfoList;
    }

    public List<CVehicleInfo> GetMatchingVRM(string partialPlate)
    {
      List<CVehicleInfo> cvehicleInfoList = new List<CVehicleInfo>();
      if (!Database.IsAccessible())
        return cvehicleInfoList;
      CVehicleInfo cvehicleInfo = new CVehicleInfo();
      try
      {
        SqlConnection databaseConnection = Database.DatabaseConnection;
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = databaseConnection;
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.CommandTimeout = 0;
        sqlCommand.CommandText = "selAllCarDetailsPartVRM";
        SqlParameter sqlParameter = new SqlParameter();
        sqlParameter.ParameterName = "@VRM";
        sqlParameter.SqlDbType = SqlDbType.VarChar;
        sqlParameter.Value = (object) partialPlate;
        sqlParameter.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        DataTable dataTable = new DataTable();
        sqlDataAdapter.SelectCommand = sqlCommand;
        databaseConnection.Open();
        sqlDataAdapter.Fill(dataTable);
        databaseConnection.Close();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          cvehicleInfoList.Add(new CVehicleInfo()
          {
            CarDataId = int.Parse(dataTable.Rows[index]["CarDataId"].ToString()),
            overviewImageLocation = dataTable.Rows[index]["OverviewImage"].ToString(),
            plateImageLocation = dataTable.Rows[index]["PlateImage"].ToString(),
            VRM = dataTable.Rows[index]["VRM"].ToString(),
            inwardTime = dataTable.Rows[index]["EventDateTime"].ToString()
          });
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      return cvehicleInfoList;
    }
  }
}
