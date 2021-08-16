using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestData
{
    public static class TestData
    {
        #region Fields
        private static DataTable TestDataSource;
        private static string DelayBetweenActions;
        private static string TargetFiles;
        private static string GatRunVersion;
        private static string StartFolder;
        private static string StartGatturnusFolder;
        private static string GatExeArgs;
        private static string FileVersion;
        private static string FileVersionShort;
        private static string Server;
        private static string DBLocation;
        private static string MalDataBase;
        private static string DataBase;
        private static string DataBasePerformance;
        private static string DataBasePerformanceMal;
        private static string RestoreDataBase;
        private static string DBUser;
        private static string DBAuth;
        private static string RunningServer;
        private static string FrameWorkVersion;
        private static string DBPassWordString = "rP/aYI8w/Qw=";
        private static string WorkingDirectory;
        #endregion

        public static String GetWorkingDirectory
        {
            get
            {
                if (Environment.GetEnvironmentVariable("TC_BUILDAGENT") == "1")
                {
                    WorkingDirectory = Environment.GetEnvironmentVariable("TC_WORKDIR");
                }
                else
                {
                    WorkingDirectory = Directory.GetParent(".").Parent.Parent.Parent.FullName;
                }

                Debug.Write("Workdirectory: " + WorkingDirectory);
                return WorkingDirectory;
            }
        }
        public static void GetIniFile()
        {
            var configDir = Path.Combine(GetWorkingDirectory, "Config");
            var config = new IniFile(configDir + @"\server.ini");

            //serverIni.Write("DBSERVER", "win7ascl63", "DBSRV");
            //if (Environment.GetEnvironmentVariable("TC_BUILDAGENT") == "1")
            //{
            ////TargetFiles = Environment.GetEnvironmentVariable("TARGETFILES");
            ////StartFolder = Environment.GetEnvironmentVariable("GATSTARTFOLDER");
            ////GatRunVersion = Environment.GetEnvironmentVariable("GETGATRUNVERSION");
            ////FileVersion = Environment.GetEnvironmentVariable("FILEVERSION");
            ////FileVersionShort = FileVersion.Remove(FileVersion.LastIndexOf("."), 2);
            ////Server = Environment.GetEnvironmentVariable("DBSERVER");
            ////DataBase = Environment.GetEnvironmentVariable("DATABASE");
            ////DataBasePerformance = Environment.GetEnvironmentVariable("DATABASEPERFORMANCE");
            ////DBUser = Environment.GetEnvironmentVariable("DBUSER");
            ////DBAuth = Environment.GetEnvironmentVariable("DBAUTHENTICATE");
            ////RunningServer = Environment.MachineName;
            ////FrameWorkVersion = Environment.GetEnvironmentVariable("APPOOLFRAMEWORKNAME");
            //}
            
            TargetFiles = config.Read("TARGETFILES", "TARGET");
            GatRunVersion = config.Read("GATRUNVERSION", "GATSTARTFOLDER"); 
            StartFolder = config.Read("FOLDERNAME", "GATSTARTFOLDER");
            StartGatturnusFolder = Path.Combine(@"C:\Gatsoft\" + StartFolder, "gatturnus.exe");
            GatExeArgs = @" ini " + configDir + @"\CurrentIni\gatturnus.ini";
            FileVersion = config.Read("FILEVERSION", "VERSION");
            FileVersionShort = FileVersion.Remove(FileVersion.LastIndexOf("."), 2);     
            Server = config.Read("DBSERVER", "DBSRV");
            RestoreDataBase = config.Read("RESTOREDATABASE", "DB");
            DBLocation = config.Read("DBFILELOCATION", "DB");
            var dBName = config.Read("MALDATABASE", "DB");
            MalDataBase = dBName + "_MAL";
            var gatVersionDB = config.Read("DATABASEVER", "DB");
            DataBase = dBName + "_" + gatVersionDB;
            DataBasePerformanceMal = config.Read("DATABASEPERFORMANCE", "DB");
            DataBasePerformance = DataBasePerformanceMal + "_" + gatVersionDB;
            DBUser = config.Read("USER", "DB");
            DBAuth = config.Read("AUTHENTICATE", "DB");
            RunningServer = Environment.MachineName;
            DelayBetweenActions = config.Read("DELAY", "SETTINGS");
            FrameWorkVersion = config.Read("FRWN", "APPOOLFRAMEWORKNAME");
        }

        public static void GetTestData(string ConfigurationFilePath, string FileType, TestContext testContext)
        {
            //string fileName = @"DataFiles\" + ConfigurationFileName + FileType;
            string filePath = ConfigurationFilePath + FileType;
            try
            {
                TestDataSource = DataSource.GetXLSData(filePath, testContext);
            }
            catch (Exception e)
            {
                testContext.WriteLine("An error occured getting configfiles: " + e.Message);
            }
        }
        public static DataTable GlobalDataSource(TestContext testContext)
        {
            try
            {
                return TestDataSource;
            }
            catch (Exception ex)
            {
                testContext.WriteLine("Unable to get CurrentDataSource: " + ex.Message);
            }

            return null;
        }
        public static string GatRestoreDataBase
        {
            get
            {
                if (string.IsNullOrEmpty(RestoreDataBase))
                    GetIniFile();

                return RestoreDataBase;
            }
        }
        public static string GatRunExePath
        {
            get
            {
                if (string.IsNullOrEmpty(GatRunVersion))
                    GetIniFile();

                return Path.Combine(@"\\NO-VSW-WS-0030\TeamCity\Gat\" + GatRunVersion, @"Deploy\GatRun.exe");
            }
        }
        public static string GatturnusExePath
        {
            get
            {
                if (string.IsNullOrEmpty(StartGatturnusFolder))
                    GetIniFile();

                return StartGatturnusFolder;
            }
        }
        public static string GatturnusExeArgs
        {
            get
            {
                if (string.IsNullOrEmpty(GatExeArgs))
                    GetIniFile();

                return GatExeArgs;
            }
        }

        //Todo blir kun brukt av AMLtest endre når denne lages ny
        public static string GetStartFolder
        {
            get
            {
                if (string.IsNullOrEmpty(StartFolder))
                    GetIniFile();

                return StartFolder;
            }
        }
        public static int GetDelayBetweenActions
        {
            get
            {
                if (string.IsNullOrEmpty(DelayBetweenActions))
                    GetIniFile();

                var delay = Convert.ToInt32(DelayBetweenActions);

                return delay;
            }
        }        
        public static string GetTargetFiles
        {
            get
            {
                if (string.IsNullOrEmpty(TargetFiles))
                    GetIniFile();

                return TargetFiles;
            }
        }
        public static string GetFileVersion
        {
            get
            {
                if (string.IsNullOrEmpty(FileVersion))
                    GetIniFile();

                return FileVersion;
            }
        }
        public static string GetDatabaseServer
        {
            get
            {
                if (string.IsNullOrEmpty(Server))
                    GetIniFile();

                return Server;
            }
        }
        public static string GetDataBaseFilLocation
        {
            get
            {
                if (string.IsNullOrEmpty(DBLocation))
                    GetIniFile();

                return DBLocation;
            }
        }
        public static string GetMalDataBase
        {
            get
            {
                if (string.IsNullOrEmpty(MalDataBase))
                    GetIniFile();

                return MalDataBase;
            }
        }
        public static string GetDataBase
        {
            get
            {
                if (string.IsNullOrEmpty(DataBase))
                    GetIniFile();

                return DataBase;
            }
        }        
        public static string GetDataBasePerformanceMal
        {
            get
            {
                if (string.IsNullOrEmpty(DataBasePerformanceMal))
                    GetIniFile();

                return DataBasePerformanceMal;
            }
        }
        public static string GetDataBasePerformance
        {
            get
            {
                if (string.IsNullOrEmpty(DataBasePerformance))
                    GetIniFile();

                return DataBasePerformance;
            }
        }        
        public static string GetDBUser
        {
            get
            {
                if (string.IsNullOrEmpty(DBUser))
                    GetIniFile();

                return DBUser;
            }
        }
        public static string GetDBAuth(bool decrypt = true)
        {
            if (string.IsNullOrEmpty(DBAuth))
                GetIniFile();

            if (decrypt)
                return Decrypt(DBAuth, DBPassWordString);
            else
                return DBAuth;
        }
        public static string GetExecutingServer
        {
            get
            {
                if (string.IsNullOrEmpty(RunningServer))
                    GetIniFile();

                return RunningServer;
            }
        }
        public static string GatFrameWorkVersion
        {
            get
            {
                if (string.IsNullOrEmpty(FrameWorkVersion))
                    GetIniFile();

                return FrameWorkVersion;
            }
        }

        #region Crypto Functions

        private static string Encrypt(string plaintext, string ciphertext)
        {
            if (plaintext != string.Empty)
            {
                return CryptoEngine.Encrypt(plaintext, ciphertext);
            }
            return "";
        }

        private static string Decrypt(string ciphertext, string plaintext)
        {
            if (ciphertext != string.Empty)
            {
                return CryptoEngine.Decrypt(plaintext, ciphertext);
            }
            return "";
        }
        private static class CryptoEngine
        {
            public static string Encrypt(string input, string key)
            {
                try
                {
                    TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
                    MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                    byte[] byteHash, byteBuff;
                    string strTempKey = key;
                    byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                    objHashMD5 = null;
                    objDESCrypto.Key = byteHash;
                    objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                    byteBuff = ASCIIEncoding.ASCII.GetBytes(input);
                    return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                        TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                }
                catch (Exception ex)
                {
                    return "Wrong Input. " + ex.Message;
                }
            }
            public static string Decrypt(string input, string key)
            {
                TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = key;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(input);
                string strDecrypted = ASCIIEncoding.ASCII.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
        }
        #endregion
    }
}
