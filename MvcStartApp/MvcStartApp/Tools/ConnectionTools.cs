//-
using System;
using System.IO;
using System.Linq;


namespace MvcStartApp.Tools;

public static class ConnectionTools
{
    /// <summary>
    /// Получаем значение строки подключения к серверу базы данных
    ///  из приватной области репозитория
    /// </summary>
    public static string GetConnectionString()
    {
        // получаем значение строки подключения из файла
        string connectionString = string.Empty;
        string privateDataFolder = DirectoryTools.GetRootForFolderName("private-data");
        if (!string.IsNullOrWhiteSpace(privateDataFolder))
        {
            string connectionStringFilePath = Path.Combine(privateDataFolder, "private-data", "SQL_SERVER_CONNECTION_STRING");
            if (File.Exists(connectionStringFilePath))
            {
                connectionString = File.ReadLines(connectionStringFilePath).First();
            }
        }

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("connection string is not found");

        return connectionString;
    }
}
