// Decompiled with JetBrains decompiler
// Type: TouchPark.Database
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using RangerServices.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using TouchPark.Properties;

namespace TouchPark
{
  public static class Database
  {
    private static SqlConnection _DatabaseConnection;

    private static bool DatabaseAccessible { get; set; }

    public static SqlConnection DatabaseConnection
    {
      get
      {
        if (Database._DatabaseConnection == null)
          Database._DatabaseConnection = new SqlConnection(Settings.Default.DatabaseConnectionString);
        return Database._DatabaseConnection;
      }
      set
      {
        Database._DatabaseConnection = value;
      }
    }

    public static bool TryConnection()
    {
      try
      {
        Database.DatabaseConnection.Open();
        Database.DatabaseAccessible = Database.DatabaseConnection.State == ConnectionState.Open;
        Database.DatabaseConnection.Close();
      }
      catch (SqlException ex)
      {
        Database.DatabaseConnection = (SqlConnection) null;
        return false;
      }
      catch (Exception ex)
      {
        Log.Write(ex);
      }
      return Database.DatabaseAccessible;
    }

    public static bool IsAccessible()
    {
      if (Database.DatabaseConnection == null)
        Database.TryConnection();
      return Database.DatabaseAccessible;
    }

    public static string[] AvailableDatabases()
    {
      List<string> stringList = new List<string>();
      stringList.Clear();
      foreach (DataRow dataRow in SqlDataSourceEnumerator.Instance.GetDataSources().Select("Version LIKE '10%' OR Version LIKE '9%'"))
      {
        string str = string.Format("{0}\\{1}", dataRow["ServerName"], dataRow["InstanceName"]);
        stringList.Add(str);
      }
      return stringList.ToArray();
    }
  }
}
