using RHGMTool.Data;
using RHGMTool.Helper;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RHGMTool
{
    public partial class MainForm : Form
    {
        public string Url = "https://github.com/JuniorDark/RustyHearts-GM-Tool";

        #region Configuration Variables
        public string? SQLServer { get; set; }
        public string? SQLUser { get; set; }
        public string? SQLPwd { get; set; }
        #endregion

        private ItemForm? itemForm;

        public MainForm()
        {
            InitializeComponent();
            InitializeItemDataTables();
            tbSQLAddress.BackColor = ColorTranslator.FromHtml("#1A1917");
            tbSQLAccount.BackColor = ColorTranslator.FromHtml("#1A1917");
            tbSQLPassword.BackColor = ColorTranslator.FromHtml("#1A1917");
        }

        private static void InitializeItemDataTables()
        {
            // Create an instance of DatatableManager and fetch the DataTable
            ItemDataTable.CachedDataTable = DataTableManager.GetCachedDataTable();
        }

        #region Form Event Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            string currentVersion = GetVersion();
            lbVersion.Text = $"Version: {currentVersion}";
            LoadConfiguration();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfiguration();
        }

        #endregion

        #region Configuration
        private const string ConfigFileName = "Config.ini";

        private void LoadConfiguration()
        {
            try
            {
                IniFile ini = new(ConfigFileName);
                SQLServer = string.IsNullOrEmpty(ini.ReadValue("Option", "SqlServer")) ? "" : ini.ReadValue("Option", "SqlServer");
                SQLUser = string.IsNullOrEmpty(ini.ReadValue("Option", "SqlUser")) ? "sa" : ini.ReadValue("Option", "SqlUser");
                SQLPwd = ini.ReadValue("Option", "SqlPasswd");

                tbSQLAddress.Text = SQLServer;
                tbSQLAccount.Text = SQLUser;
                tbSQLPassword.Text = SQLPwd;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading configuration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveConfiguration()
        {
            try
            {
                IniFile ini = new(ConfigFileName);
                ini.WriteValue("Option", "SqlServer", tbSQLAddress.Text);
                ini.WriteValue("Option", "SqlUser", tbSQLAccount.Text);
                ini.WriteValue("Option", "SqlPasswd", tbSQLPassword.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving configuration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string GetVersion()
        {
            // Get the version information of the application
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

            // Extract the version number
            string version = $"{versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}.{versionInfo.FileBuildPart}";

            return version;
        }
        #endregion

        #region Buttons

        private void BtnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                SQLServer = tbSQLAddress.Text.Trim();
                SQLUser = tbSQLAccount.Text.Trim();
                SQLPwd = tbSQLPassword.Text.Trim();

                if (string.IsNullOrEmpty(SQLServer) || string.IsNullOrEmpty(SQLUser) || string.IsNullOrEmpty(SQLPwd))
                {
                    MessageBox.Show("Server address, SQL account, and SQL password cannot be empty!", "SQL Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using MailForm mailForm = new(SQLServer, SQLUser, SQLPwd);
                mailForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSQLConnection_Click(object sender, EventArgs e)
        {
            SQLServer = tbSQLAddress.Text.Trim();
            SQLUser = tbSQLAccount.Text.Trim();
            SQLPwd = tbSQLPassword.Text.Trim();

            if (string.IsNullOrEmpty(SQLServer) || string.IsNullOrEmpty(SQLUser) || string.IsNullOrEmpty(SQLPwd))
            {
                MessageBox.Show("Server address, SQL account, and SQL password cannot be empty!", "SQL Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSQLConnection.Enabled = false;
            btnSQLConnection.Text = "Connecting...";
            bool connectionTestResult = await TestDatabaseConnectionAsync();

            btnSQLConnection.Enabled = true;
            btnSQLConnection.Text = "Test Connection";

            if (!connectionTestResult)
            {
                MessageBox.Show("Failed to establish a connection with the sql server. Check the server/your credentials and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Connection success!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task<bool> TestDatabaseConnectionAsync()
        {
            string connectionString = $"Data Source={SQLServer};User ID={SQLUser};Password={SQLPwd};";

            try
            {
                return await Task.Run(() =>
                {
                    using SqlConnection connection = new(connectionString);

                    try
                    {
                        connection.Open();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async void BtnItemDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (itemForm == null || itemForm.IsDisposed)
                    {
                        itemForm = new ItemForm(null, 1, true);
                    }
                });

                await Task.Run(() =>
                {
                    if (itemForm != null)
                    {
                        if (itemForm.InvokeRequired)
                        {
                            itemForm.Invoke(new Action(() =>
                            {
                                if (itemForm.WindowState == FormWindowState.Minimized)
                                {
                                    itemForm.WindowState = FormWindowState.Normal;
                                }

                                if (!itemForm.Visible)
                                {
                                    itemForm.ShowDialog();
                                }
                            }));
                        }
                        else
                        {
                            if (itemForm.WindowState == FormWindowState.Minimized)
                            {
                                itemForm.WindowState = FormWindowState.Normal;
                            }

                            if (!itemForm.Visible)
                            {
                                itemForm.ShowDialog();
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VersionLabel_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = Url, UseShellExecute = true });
        }
        #endregion

        #region Button Event Handlers

        private void PictureBoxEyeSqlPassword_MouseDown(object sender, MouseEventArgs e)
        {
            tbSQLPassword.UseSystemPasswordChar = false;
        }

        private void PictureBoxEyeSqlPassword_MouseUp(object sender, MouseEventArgs e)
        {
            tbSQLPassword.UseSystemPasswordChar = true;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FormUtils.MoveForm(Handle);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.ImageIndex = 1;
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.ImageIndex = 0;
        }

        private void BtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            btnClose.ImageIndex = 2;
        }
        private void Button_MouseHover(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 2;
            }
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 1;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.ImageIndex = 0;
            }
        }
        #endregion

    }
}
