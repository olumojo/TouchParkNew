// Decompiled with JetBrains decompiler
// Type: TouchPark.CGetUseDetails
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TouchPark
{
  public class CGetUseDetails
  {
    public DataTable selPasscode(string pPasscode)
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
        sqlCommand.CommandText = "selPasscodes";
        SqlParameter sqlParameter = new SqlParameter();
        sqlParameter.ParameterName = "@passcode";
        sqlParameter.SqlDbType = SqlDbType.VarChar;
        sqlParameter.Value = (object) pPasscode;
        sqlParameter.Direction = ParameterDirection.Input;
        sqlCommand.Parameters.Add(sqlParameter);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        dataTable = new DataTable();
        sqlDataAdapter.SelectCommand = sqlCommand;
        databaseConnection.Open();
        sqlDataAdapter.Fill(dataTable);
        databaseConnection.Close();
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      return dataTable;
    }
  }
}
