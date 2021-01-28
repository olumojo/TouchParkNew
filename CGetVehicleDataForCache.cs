// Decompiled with JetBrains decompiler
// Type: TouchPark.CGetVehicleDataForCache
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TouchPark
{
  internal class CGetVehicleDataForCache
  {
    public DataTable GetDataforCache()
    {
      Console.WriteLine("Obtaining vehicle data for cache...");
      if (!Database.IsAccessible())
        return (DataTable) null;
      DataTable dataTable1 = new DataTable();
      CVehicleInfo cvehicleInfo = new CVehicleInfo();
      DataTable dataTable2 = new DataTable();
      try
      {
        SqlConnection databaseConnection = Database.DatabaseConnection;
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = databaseConnection;
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.CommandTimeout = 0;
        sqlCommand.CommandText = "selAllCarDetailsFromDateAndOnlyDisplayedForCache";
        SqlParameter sqlParameter = new SqlParameter();
        sqlParameter.ParameterName = "@DisplayTime";
        sqlParameter.SqlDbType = SqlDbType.DateTime;
        sqlParameter.Value = (object) DateTime.Now;
        sqlParameter.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        sqlDataAdapter.SelectCommand = sqlCommand;
        if (databaseConnection.State != ConnectionState.Open)
          databaseConnection.Open();
        sqlDataAdapter.Fill(dataTable2);
        databaseConnection.Close();
      }
      catch (SqlException ex)
      {
        if (ex.Message.Contains("The server was not found or was not accessible."))
        {
          Log.Write("The server was not found or was not accessible.");
          return (DataTable) null;
        }
        Log.Write((Exception) ex);
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      if (dataTable2 != null)
      {
        Log.Write("...returning {0} rows of Vehicle Data", new object[1]
        {
          (object) dataTable2.Rows.Count
        });
        Console.WriteLine("...returning {0} rows of Vehicle Data", (object) dataTable2.Rows.Count);
      }
      return dataTable2;
    }

    public DataTable GetDataforCacheUserInfo()
    {
      if (!Database.IsAccessible())
        return (DataTable) null;
      DataTable dataTable = (DataTable) null;
      try
      {
        SqlConnection databaseConnection = Database.DatabaseConnection;
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = databaseConnection;
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.CommandTimeout = 0;
        sqlCommand.CommandText = "selAllUserInfo";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        dataTable = new DataTable();
        sqlDataAdapter.SelectCommand = sqlCommand;
        databaseConnection.Open();
        sqlDataAdapter.Fill(dataTable);
        databaseConnection.Close();
      }
      catch (SqlException ex)
      {
        if (ex.Message.Contains("The server was not found or was not accessible."))
          return (DataTable) null;
        Log.Write((Exception) ex);
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      return dataTable;
    }
  }
}
