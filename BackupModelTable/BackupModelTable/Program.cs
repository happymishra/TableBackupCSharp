using System;
using System.Data;
using System.Data.SqlClient;

namespace BackupModelTable
{
    public class Program
    {
        public static void Main()
        {
            DataTable tableData = new DataTable();
            using (SqlConnection prodCon = new SqlConnection(Constants.prodConString))
            {
                prodCon.Open();
                SqlDataAdapter query = new SqlDataAdapter("Select * From {tablename}", prodCon);
                query.Fill(tableData);
                createModelBackup(tableData);
            }
        }

        public static void createModelBackup(DataTable modelMetadata)
        {
            
            using (SqlConnection devCon = new SqlConnection(Constants.devConString))
            {
                devCon.Open();
                string createTableQuery = Constants.createTableQuery;
                SqlCommand cmd = new SqlCommand(createTableQuery, devCon);
                cmd.ExecuteNonQuery();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(devCon))
                {
                    bulkCopy.DestinationTableName = Constants.tableName;
                    try
                    {
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(modelMetadata);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
