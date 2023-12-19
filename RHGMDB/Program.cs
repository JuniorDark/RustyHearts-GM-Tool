using OfficeOpenXml;
using System.Data.SQLite;

namespace CreateDatabase
{
    class Program
    {
        static async Task Main()
        {
            string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xlsx");
            string resourcesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            string database = Path.Combine(resourcesFolder, "gmdb.db");

            try
            {
                if (!Directory.Exists(dataFolder))
                {
                    Directory.CreateDirectory(dataFolder);
                }

                string[] files = Directory.GetFiles(dataFolder, "*.xlsx");

                if (files.Length == 0)
                {
                    Console.WriteLine($"The xlsx folder is empty, there is nothing to do.\nPress any key to exit...");
                    Console.ReadKey();
                    return;
                }

                if (!Directory.Exists(resourcesFolder))
                {
                    Directory.CreateDirectory(resourcesFolder);
                }

                Console.WriteLine("Creating gmdb...");

                if (File.Exists(database))
                {
                    File.Delete(database);
                    Console.WriteLine($"Existing database file deleted.");
                }

                foreach (var file in files)
                {
                    string tableName = Path.GetFileNameWithoutExtension(file.Replace(".rh", ""));
                    string databaseName = Path.Combine(resourcesFolder, "gmdb.db");

                    await CreateTableFromXlsxAsync(file, databaseName, tableName);
                }

                Console.WriteLine("\nConversion complete.\n\nPress any key to exit...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                Console.ReadKey();
            }
        }

        static async Task CreateTableFromXlsxAsync(string fileName, string dbName, string tableName)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            string connectionString = $"Data Source={dbName};Version=3;";

            using SQLiteConnection connection = new(connectionString);
            connection.Open();

            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                CreateTableFromXlsx(connection, worksheet, tableName);

                await InsertDataFromXlsxAsync(connection, worksheet, tableName);
            }

            connection.Close();
        }

        static void CreateTableFromXlsx(SQLiteConnection connection, ExcelWorksheet worksheet, string tableName)
        {
            int columnCount = worksheet.Dimension.Columns;

            string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} (";
            for (int col = 1; col <= columnCount; col++)
            {
                string attributeName = worksheet.Cells[1, col].Text;
                string excelDataType = worksheet.Cells[2, col].Text;
                string sqliteDataType = MapExcelDataTypeToSqlite(excelDataType);

                createTableQuery += $"{attributeName} {sqliteDataType}, ";
            }

            createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")";

            using SQLiteCommand command = new(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        static async Task InsertDataFromXlsxAsync(SQLiteConnection connection, ExcelWorksheet worksheet, string tableName)
        {
            int rowCount = worksheet.Dimension.Rows;

            string insertDataQuery = $"INSERT INTO {tableName} VALUES (";
            int currentRow = 0;

            for (int row = 3; row <= rowCount; row++)
            {
                for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                {
                    string attributeName = worksheet.Cells[1, col].Text;
                    string attributeValue = worksheet.Cells[row, col].Text;

                    insertDataQuery += $"@{attributeName}, ";
                }

                insertDataQuery = insertDataQuery.TrimEnd(',', ' ') + ")";

                using (SQLiteCommand command = new(insertDataQuery, connection))
                {
                    for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                    {
                        string attributeName = worksheet.Cells[1, col].Text;
                        string attributeValue = worksheet.Cells[row, col].Text;
                        command.Parameters.AddWithValue($"@{attributeName}", attributeValue);
                    }

                    await Task.Run(command.ExecuteNonQuery);
                }

                insertDataQuery = $"INSERT INTO {tableName} VALUES (";

                // Report progress
                currentRow++;
                int progressPercentage = (int)((double)currentRow / (rowCount - 2) * 100);
                Console.Write($"\rInserting data into {tableName} table: {progressPercentage}%");

                if (currentRow == rowCount - 2)
                {
                    Console.WriteLine();
                }
            }
        }

        static string MapExcelDataTypeToSqlite(string excelDataType)
        {
            // Map Excel data types to SQLite data types
            return excelDataType.ToLower() switch
            {
                "int32" => "INTEGER",
                "string" or "string2" => "TEXT",
                "float" => "REAL",
                _ => "TEXT",
            };
        }
    }

}
