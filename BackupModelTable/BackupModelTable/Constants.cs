using System;

namespace BackupModelTable
{
    class Constants
    {
        public static string devConString = "{destination server connection strign}";
        public static string prodConString = "{source server connection string}";
        public static string tableName = "{tablename}_backup_" + String.Format("{0:yyyyMMdd}", DateTime.Now);
        public static string createTableQuery = "CREATE TABLE "  + tableName + 
            @"(
                [Id]                INT IDENTITY (1, 1) NOT NULL,
                [UserName]          VARCHAR (50) NOT NULL,
                [FirstName]         VARCHAR (50) NULL,
                [LastName]          VARCHAR (50) NULL,
            );";
    }
}
