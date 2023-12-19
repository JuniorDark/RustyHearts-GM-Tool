using System.Data.SQLite;

namespace RHGMTool.Data
{
    public class SQLiteDBConnection
    {
        private static string GetDatabaseFilePath()
        {
            string resourcesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            return Path.Combine(resourcesFolder, "gmdb.db");
        }

        public static SQLiteConnection OpenDatabaseConnection()
        {
            try
            {
                string dbFilePath = GetDatabaseFilePath();

                if (!File.Exists(dbFilePath))
                {
                    throw new FileNotFoundException($"Database file ({Path.GetFileName(dbFilePath)}) not found in the expected location. Please ensure to create the gmdb and place it in the Resources folder.");
                }

                var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;");
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }

}
