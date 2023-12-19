namespace RHGMTool
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Check if the database file exists
            string dbFilePath = GetDatabaseFilePath();
            if (!File.Exists(dbFilePath))
            {
                string errorMessage = $"The application cannot start because the database file ({Path.GetFileName(dbFilePath)}) is missing in the expected location.";
                errorMessage += Environment.NewLine;
                errorMessage += "Please ensure you have created the 'gmdb.db' file using 'CreateGMDatabase.exe' and placed it in the 'Resources' folder.";

                MessageBox.Show(errorMessage, "Database Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new MainForm());
        }

        private static string GetDatabaseFilePath()
        {
            string resourcesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

            if (!Directory.Exists(resourcesFolder))
            {
                Directory.CreateDirectory(resourcesFolder);
            }

            return Path.Combine(resourcesFolder, "gmdb.db");
        }

    }
}