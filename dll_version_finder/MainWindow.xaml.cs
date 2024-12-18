using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace dll_version_finder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnScan_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = txtFolderPath.Text;
            if (Directory.Exists(folderPath))
            {
                var dllData = new System.Data.DataTable();
                dllData.Columns.Add("DLL Name");
                dllData.Columns.Add("Version");

                string[] dllFiles = Directory.GetFiles(folderPath, "*.dll");
                foreach (string dllFile in dllFiles)
                {
                    string fileName = Path.GetFileName(dllFile);
                    string version = GetDllVersion(dllFile);
                    dllData.Rows.Add(fileName, version);
                }

                dgvResults.ItemsSource = dllData.DefaultView;
            }
            else
            {
                MessageBox.Show("Please enter a valid folder path.", "Invalid Path", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private string GetDllVersion(string filePath)
        {
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);
            return versionInfo.FileVersion ?? "Unknown";
        }
    }
}
