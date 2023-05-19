using System;
using System.Configuration;
using System.IO.Compression;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SupportTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AppSettingFileImport();
            StartupProcess();

            DailyTempFolderCheckorCreate();
            ArchiveDailyTempFolders("20230510");
            CreateDailyNotesDoc();


        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }



        //App Config Variables
        private static string DailyTempDirectory { get; set; }
        private static string DailyTempArchiveDirectory { get; set; }
        private static string DailyNotesDirectory { get; set; }

        //Other Variables
        private List<string> consoleMessages = new List<string>();
        private int consoleMessageCount = 0;



        //Other Methods
        private void AppSettingFileImport()
        {
            //This method will pull in all of the stored vaibles on startup from the App.Config file
            AddConsoleMessageToLogs("Starting AppSettingFileImport");

            DailyTempDirectory = ConfigurationManager.AppSettings["dailyTempDirectory"];
            DailyTempArchiveDirectory = ConfigurationManager.AppSettings["DailyTempArchiveDirectory"];
            DailyNotesDirectory = ConfigurationManager.AppSettings["DailyNotesDirectory"];

            AddConsoleMessageToLogs("Finished AppSettingFileImport");
        }
        private void StartupProcess()
        {
            DailyTempDirectoryTextBox.Text = DailyTempDirectory;
            DailyTempArchiveDirectoryTextBox.Text = DailyTempArchiveDirectory;
            DailyNotesDirectoryTextBox.Text = DailyNotesDirectory;

            AddConsoleMessageToLogs("Finished StartupProcess");
        }
        private void AddConsoleMessageToLogs(string message)
        {
            consoleMessageCount++;
            var fullMessage = $"{DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss")} Entry {consoleMessageCount}: {message}";
            consoleMessages.Add(fullMessage);

            if (consoleMessages.Count > 50)
            {
                consoleMessages.RemoveAt(1);
            }
            ConsolTextBox.Text = string.Join(Environment.NewLine, consoleMessages);

            ConsolTextBox.SelectionStart = ConsolTextBox.Text.Length;
            ConsolTextBox.ScrollToCaret();
        }
        //Example from ChatGPT for AddConsoleMessageToLogs

        //// Declare a List<string> to store the messages
        //private List<string> messageLog = new List<string>();

        //// Method to add a message to the log and update the TextBox
        //private void AddMessageToLog(string message)
        //{
        //    // Add the new message to the log
        //    messageLog.Add(message);

        //    // Limit the log to the last 100 messages
        //    if (messageLog.Count > 100)
        //    {
        //        messageLog.RemoveAt(0);
        //    }

        //    // Update the TextBox with the log
        //    messageTextBox.Text = string.Join(Environment.NewLine, messageLog);

        //    // Scroll to the end of the TextBox to show the latest message
        //    messageTextBox.SelectionStart = messageTextBox.Text.Length;
        //    messageTextBox.ScrollToCaret();
        //}

        private void TriggerSetup()
        {
            MessageBox.Show("Method TriggerSetup has not been enabled yet");
        }


        //Daily Temp Folder Methods

        private void DailyTempFolderCheckorCreate()
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
                AddConsoleMessageToLogs($"Folder Already Exist {todaysTempFolder}");
                return;
            }
            else
            {
                Directory.CreateDirectory(todaysTempFolder);
                AddConsoleMessageToLogs($"Folder Created: {todaysTempFolder}");
            }
        }
        private void ArchiveDailyTempFolders(string startingDate)
        {
            //This method will archive all of the daily temp folders that are older than the starting date
            AddConsoleMessageToLogs("Starting ArchiveDailyTempFolders");
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
                                    AddConsoleMessageToLogs($"Archive Zip :{zipFolderPath}\n" +
                                                    "Already Exist Please hand archive this file");
                                }
                                else
                                {
                                    ZipFile.CreateFromDirectory(folder, zipFolderPath);
                                    if (File.Exists(zipFolderPath))
                                    {
                                        AddConsoleMessageToLogs($"Archive Zip :{zipFolderPath} Created");
                                        Directory.Delete(folder, true);
                                        if (File.Exists(folder))
                                        {
                                            AddConsoleMessageToLogs($"Failed to Delete Directory\n{folder}");
                                        }
                                    }
                                    else
                                    {
                                        AddConsoleMessageToLogs($"Failed to Zip directory\nFolder delete was cansled\n{folder}");
                                    }
                                }
                            }
                            else
                            {
                                //Will Delete Any folder that are empty with out archiving
                                Directory.Delete(folder, true);
                                if (File.Exists(folder))
                                {
                                    AddConsoleMessageToLogs($"Failed to delete Empty Direcyoty\n{folder}");
                                }
                            }
                        }
                    }
                    else
                    {
                        AddConsoleMessageToLogs($"Folder Does not fit DateSub Format \"yyyyMMdd\"\n{folder}");
                    }
                }


            }
            else
            {
                MessageBox.Show($"Starting Date {startDate} is not between 1/1/1900 and 1/1/2100");
            }
        }


        //Daily Notes Methods
        private void CreateDailyNotesDoc()
        {
            AddConsoleMessageToLogs("Starting DailyNotesDoc");

            object missing = System.Reflection.Missing.Value;

            //check if the file already exist and exit if it does
            DateTime dateTime = DateTime.Now;
            string outputPath = Path.Combine(DailyNotesDirectory, string.Format("DailyNotes_{0}.docx", dateTime.ToString("yyyyMMdd")));
            if (File.Exists(outputPath))
            {
                AddConsoleMessageToLogs($"Daily Notes File {outputPath} already exist");
                return;
            }

            // Create a new instance of Word and make it visible
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = false;

            var todaysNotes = Path.Combine(Directory.GetCurrentDirectory(), "Daily Notes.dotx");

            // Open the template
            Document doc = wordApp.Documents.Add(todaysNotes, ref missing, ref missing, ref missing);

            // Save the new document
            doc.SaveAs2(outputPath, WdSaveFormat.wdFormatXMLDocument, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

            // Close the template
            doc.Close(ref missing, ref missing, ref missing);

            // Quit Word
            wordApp.Quit(ref missing, ref missing, ref missing);
            AddConsoleMessageToLogs($"Daily Notes was added: {outputPath}");
        }
        private void UpdateCheckDailyNotesDoc()
        {
            object missing = System.Reflection.Missing.Value;
            List<string> newTicket = new List<string>();
            List<string> newTicketFolders = new List<string>();
            var currentDate = DateTime.Now;

            // Open the Word document
            string documentPath = Path.Combine(DailyNotesDirectory, string.Format("DailyNotes{0}.docx", currentDate.ToString("yyyyMMdd")));
            //string documentPath = @"E:\TestItems\TempTesting\DailyNotes_20230415.docx";
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Document doc = wordApp.Documents.Open(documentPath, ReadOnly: true);

            //Gets todays temp folder as string

            var todaysTempFolder = Path.Combine(DailyTempDirectory, currentDate.ToString("yyyyMMdd"));
            AddConsoleMessageToLogs($"todays Temp Folder: {todaysTempFolder}");

            //Gets all of todaystemp subfolders
            var todaysTickets = Directory.GetDirectories(todaysTempFolder);

            //Iterates thought Headers looking for Text that matches ticket numbers
            //creates nnew ticket folder if needed
            foreach (Paragraph paragraph in doc.Paragraphs)
            {
                //CHeck if heading style == Heading 3
                Style style = paragraph.get_Style() as Style;
                if (style != null && style.NameLocal.StartsWith("Heading 2"))
                {
                    //Check if the Text matches the ticket format
                    string pattern = @"Ticket #\s?(\d{6})\b";
                    Match match = Regex.Match(paragraph.Range.Text, pattern);
                    if (match.Success)
                    {
                        //get the sub string for check
                        if (!todaysTickets.Any(item => item.Contains(match.Groups[1].Value)))
                        {
                            Console.WriteLine("New TicketNum: " + match.Groups[1].Value);

                            //Creates a folder with the ticket numer and 50 charicters 
                            string patternFolderName = @"(\d{6}.{0,50})";
                            Match matchFolderName = Regex.Match(paragraph.Range.Text, patternFolderName);
                            string cleanedString = Regex.Replace(matchFolderName.Value, @"[^a-zA-Z0-9]+", "_");
                            Directory.CreateDirectory(Path.Combine(todaysTempFolder, cleanedString));

                            //adds ticket number to new ticket list for use latter
                            //newTicket.Add(Path.Combine(todaysTempFolder, cleanedString));
                            newTicket.Add(match.Groups[1].Value);
                            newTicketFolders.Add(Path.Combine(todaysTempFolder, cleanedString));
                        }
                    }
                }
            }
            // Close the Word document
            doc.Close();
            wordApp.Quit();

            //Copy items from ticket folder in other daily temp folders
            //This way everthing from the last days will be avabile with out loooking for it 
            if (newTicket.Count != 0)
            {
                //Get a list of all sub folder in daily temp folder.
                string[] subFolders = Directory.GetDirectories(DailyTempDirectory, "*", SearchOption.AllDirectories);

                foreach (string subFolder in subFolders)
                {
                    //Console.WriteLine($"\nCurrent subFolder: {subFolder}");
                    string folderName = Path.GetFileName(subFolder);
                    foreach (string ticket in newTicket)
                    {
                        //Console.WriteLine($"Current ticket: {ticket}");
                        //Console.WriteLine($"Current todayTempFolder: {Path.GetFileName(todaysTempFolder)}");
                        //Console.WriteLine($"Current folderName: {folderName}");

                        if (!subFolder.Contains(Path.GetFileName(todaysTempFolder))
                            && folderName.Contains(ticket))
                        {
                            Console.WriteLine($"Match: {subFolder}");
                            foreach (string newTicketFolder in newTicketFolders)
                            {
                                if (newTicketFolder.Contains(ticket))
                                {
                                    // Get the source and destination directory paths
                                    //string sourceDirectoryPath = @"C:\SourceDirectory";
                                    DirectoryInfo parentDir = Directory.GetParent(subFolder);
                                    string destinationDirectoryPath = Path.Combine(newTicketFolder, parentDir.Name);
                                    //string destinationDirectoryPath = @"C:\DestinationDirectory";
                                    Console.WriteLine($"Destin Path: {destinationDirectoryPath}");


                                    // Create a new DirectoryInfo instance for the source directory
                                    DirectoryInfo sourceDirectory = new DirectoryInfo(subFolder);

                                    // Get all files and subdirectories in the source directory
                                    FileSystemInfo[] filesAndFolders = sourceDirectory.GetFileSystemInfos("*", SearchOption.AllDirectories);
                                    if (filesAndFolders.Length > 0)
                                    {
                                        //Create dated folder for when the data comes from
                                        Directory.CreateDirectory(destinationDirectoryPath);

                                        // Copy each file and folder to the destination directory
                                        foreach (var fileOrFolder in filesAndFolders)
                                        {
                                            Console.WriteLine($"fileOrFolder: {fileOrFolder}");
                                            // Create the new path to the destination file or folder
                                            string newFilePath = Path.Combine(destinationDirectoryPath, fileOrFolder.FullName.Substring(subFolder.Length + 1));

                                            if (fileOrFolder.Attributes.HasFlag(FileAttributes.Directory))
                                            {
                                                // Create the destination folder if it doesn't exist
                                                Directory.CreateDirectory(newFilePath);
                                            }
                                            else
                                            {
                                                // Copy the file to the destination folder
                                                File.Copy(fileOrFolder.FullName, newFilePath, true);
                                            }
                                        }

                                        Console.WriteLine("Files and folders copied successfully.");
                                    }

                                }
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Method UpdateCheckDailyNotesDoc has not been enabled yet");
        }


        //Form Actions
        private void DailyTempDirectoryUpdateButton_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                DailyTempDirectory = folderBrowserDialog.SelectedPath;
            }
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DailyTempDirectory"].Value = DailyTempDirectory;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            
            DailyTempDirectoryTextBox.Text = DailyTempDirectory;

            AddConsoleMessageToLogs($"Update Daily Temp Directory: {DailyTempDirectory}");
        }
        private void DailyTempArchiveDirectoryUpdateButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                DailyTempArchiveDirectory = folderBrowserDialog.SelectedPath;
            }
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DailyTempArchiveDirectory"].Value = DailyTempArchiveDirectory;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            DailyTempArchiveDirectoryTextBox.Text = DailyTempArchiveDirectory;

            AddConsoleMessageToLogs($"Update Daily Temp Directory: {DailyTempArchiveDirectory}");
        }
        private void DailyNotesDirectoryUpdateButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                DailyNotesDirectory = folderBrowserDialog.SelectedPath;
            }

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DailyNotesDirectory"].Value = DailyNotesDirectory;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            DailyNotesDirectoryTextBox.Text = DailyNotesDirectory;


            AddConsoleMessageToLogs($"Update Daily Temp Directory: {DailyNotesDirectory}");
        }
        //ChatGPT Example Updateing App.config
        //using System.Configuration;

        //// ...

        //// Update a setting in the app.config file
        //private void UpdateAppSetting(string key, string value)
        //    {
        //        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //        config.AppSettings.Settings[key].Value = value;
        //        config.Save(ConfigurationSaveMode.Modified);
        //        ConfigurationManager.RefreshSection("appSettings");
        //    }

        private void DailyTempAutoCreateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Method DailyTempAutoCreateRadioButton_CheckedChanged has not been enabled yet");
        }
        private void DailyTempArchiveAutoCreateRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void DailyNoteAutoCreateRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Method DailyNoteAutoCreateRadioButton3_CheckedChanged has not been enabled yet");
        }


    }
}
