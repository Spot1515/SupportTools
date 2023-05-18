using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using Microsoft.Office.Interop.Word;


namespace SupportTools
{
    

    internal static class Program
    {



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Pull all items from App Config file
            AppSettingFileImport();


            //Starts the Windows Form
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
        }

        //App Config Variables
        private static string DailyTempDirectory { get; set; }
        private static string DailyTempArchiveDirectory { get; set; }
        private static string DailyNotesDirectory { get; set; }



        //Program Setup Methods
        private static void AppSettingFileImport()
        {
            //This method will pull in all of the stored vaibles on startup from the App.Config file

            DailyTempDirectory = ConfigurationManager.AppSettings["DailyTempDirectory"];
            DailyTempArchiveDirectory = ConfigurationManager.AppSettings["DailyTempArchiveDirectory"];
            DailyNotesDirectory = ConfigurationManager.AppSettings["DailyNotesDirectory"];

            //MessageBox.Show("Method AppSettingFileImport has not been enabled yet");
        }
        static void TriggerSetup()
        {
            MessageBox.Show("Method TriggerSetup has not been enabled yet");
        }


        //Daily Temp Folder Methods
        private static void CreateDailyTempFolder()
        {
            MessageBox.Show("Method CreateDailyTempFolder has not been enabled yet");
        }
        private static void ArchiveDailyTempFolders()
        {
            MessageBox.Show("Method ArchiveDailyTempFolders has not been enabled yet");
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
    }
}
