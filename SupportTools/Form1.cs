using System;
using System.Configuration;
using System.IO.Compression;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Linq;
using System.Text;

namespace SupportTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AppSettingFileImport();
            StartupProcess();

            
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }



        //App Config Variables
        private static string DailyTempDirectory { get; set; }
        private static string DailyTempArchiveDirectory { get; set; }
        private static string DailyNotesDirectory { get; set; }

        //Other Variables
        private StringBuilder ConsolLogMessage = new StringBuilder();



        //Other Methods
        private static void AppSettingFileImport()
        {
            //This method will pull in all of the stored vaibles on startup from the App.Config file
            Console.WriteLine("Getting App Files");

            DailyTempDirectory = ConfigurationManager.AppSettings["dailyTempDirectory"];
            DailyTempArchiveDirectory = ConfigurationManager.AppSettings["DailyTempArchiveDirectory"];
            DailyNotesDirectory = ConfigurationManager.AppSettings["DailyNotesDirectory"];

            Console.WriteLine("Getting App Files end");
            //MessageBox.Show("Method AppSettingFileImport has not been enabled yet");
        }//outlined
        private void TriggerSetup()
        {
            MessageBox.Show("Method TriggerSetup has not been enabled yet");
        }
        private void StartupProcess()
        {
            DailyTempDirectoryTextBox.Text = DailyTempDirectory;
            MessageBox.Show("Method StartupProcess has not been enabled yet");
        }
        private void AddConsoleMessageToLogs(string message)
        {
            ConsolLogMessage.AppendLine(message);

            if (ConsolLogMessage.Length > 100)
            {
                ConsolLogMessage.Remove(0,);
            }
        }
        //Example from ChatGPT for AddConsoleMessageToLogs

        //using System.Text;

        //// Declare a StringBuilder to store the messages
        //private StringBuilder messageLog = new StringBuilder();

        //    // Method to add a message to the log and update the TextBox
        //    private void AddMessageToLog(string message)
        //    {
        //        // Append the new message to the log
        //        messageLog.AppendLine(message);

        //        // Limit the log to the last 100 messages
        //        if (messageLog.Lines.Length > 100)
        //        {
        //            messageLog.Remove(0, messageLog.GetLineLength(0));
        //        }

        //        // Update the TextBox with the log
        //        messageTextBox.Text = messageLog.ToString();

        //        // Scroll to the end of the TextBox to show the latest message
        //        messageTextBox.SelectionStart = messageTextBox.Text.Length;
        //        messageTextBox.ScrollToCaret();
        //    }




        //Daily Temp Folder Methods


        private static void DailyTempFolderCheckorCreate()
        {
            //THis methiod will check if there is already a daily temp folder for today
            //If not then it will create one

            //Check if there is a file path in the DailyTempDirectory Vaible
            if (DailyTempDirectory == "")
            {
                MessageBox.Show("Please Setup Daily Temp Directory\nBefor this can be completed", "Error");
                return;
            }

            //Check if the todays folder alreay exist or creates it
            var todaysTempFolder = Path.Combine(DailyTempDirectory, DateTime.Now.ToString("yyyyMMdd"));
            if (Directory.Exists(todaysTempFolder))
            {
                Console.WriteLine($"Folder Already Exist {todaysTempFolder}");
                return;
            }
            else
            {
                Directory.CreateDirectory(todaysTempFolder);
                Console.WriteLine($"Folder Created: {todaysTempFolder}");
            }
        }//outlined
        private static void ArchiveDailyTempFolders(string startingDate)//started
        {
            //Check if the needed folders exist
            if (DailyTempDirectory == "" || DailyTempArchiveDirectory == "")
            {
                MessageBox.Show("Please check that both the Daiily Temp Directory\n and Daily Temp Archive Directory\nare setup");
                return;
            }
            //Main Daily Temp Archive process
            if (!DateTime.TryParseExact(startingDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime startDate))
            {
                throw new ArgumentNullException(nameof(startingDate));
            }
            if (new DateTime(1900, 1, 1) < startDate && startDate < new DateTime(2100, 1, 1))
            {
                var folders = Directory.GetDirectories(DailyTempDirectory).OrderBy(x => x).ToList();

                foreach (var folder in folders)
                {
                    if (DateTime.TryParseExact(Path.GetFileName(folder), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime folderDate))
                    {
                        if (folderDate < startDate)
                        {
                            if (Directory.EnumerateFileSystemEntries(folder).Any())
                            {
                                //Will Archive any folder that have files
                                var zipFolderPath = Path.Combine(DailyTempArchiveDirectory, Path.GetFileName(folder) + ".zip");
                                if (File.Exists(zipFolderPath))
                                {
                                    Console.WriteLine($"Archive Zip :{zipFolderPath}\n" +
                                                    "Already Exist Please hand archive this file");
                                }
                                else
                                {
                                    ZipFile.CreateFromDirectory(folder, zipFolderPath);
                                    if (File.Exists(zipFolderPath))
                                    {
                                        Directory.Delete(folder, true);
                                        if (File.Exists(folder))
                                        {
                                            Console.WriteLine($"Failed to Delete Directory\n{folder}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Failed to Zip directory\nFolder delete was cansled\n{folder}");
                                    }
                                }
                            }
                            else
                            {
                                //Will Delete Any folder that are empty with out archiving
                                Directory.Delete(folder, true);
                                if (File.Exists(folder))
                                {
                                    Console.WriteLine($"Failed to delete Empty Direcyoty\n{folder}");
                                }
                            }
                        }
                    }
                }


            }
            else
            {
                MessageBox.Show($"Starting Date {startDate} is not between 1/1/1900 and 1/1/2100");
            }

            //MessageBox.Show("Method ArchiveDailyTempFolders has not been enabled yet");
        }


        //Daily Notes Methods
        private static void CreateDailyNotesDoc()
        {
            MessageBox.Show("Method CreateDailyNotesDoc has not been enabled yet");
        }
        private static void UpdateCheckDailyNotesDoc()
        {
            MessageBox.Show("Method UpdateCheckDailyNotesDoc has not been enabled yet");
        }


        //Form Actions
        private void DailyTempDirectoryUpdateButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DailyTempDirectory = openFileDialog.FileName;
                DailyTempDirectoryTextBox.Text = DailyTempDirectory;
            }
        }

    }
}
