using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestData
{
    public static class SupportFunctions
    {
        public static DateTime NextDayOfWeekDay(this DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }

        public static DateTime PreviousDayOfWeekDay(this DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            int delta = start - target;

            return from.AddDays(-delta);
        }

        [Obsolete("HeaderType is deprecated", true)]
        public enum HeaderType
        {
            _Test_093,
            _Vaktkategori,
            _Oppgave,
            _Errors,
            _Common,
            _LockedFile,
            _Fullplan
        }

        public enum MainWindowTabs
        {
            Shiftbook,
            Extrainfo,
            EFO,
            LIS,
            Turnusplan,
            Rosterplan,
            Grovplan,
            Produksjonsplan,
            Employee,
            Department,
            DepartmentBetong,
            ReportCurrent,
            ReportCurrentSE,
            Budget,
            OtAdmin,
            Administration,
            ClickDefinedPoint
        }
        public static bool CheckProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
                return true;
            else
                return false;
        }

        public static void StartProcess(string path, bool startExplorer, string fileName = "")
        {
            if (!startExplorer)
            {
                Process.Start(path);
                return;
            }

            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = Environment.CurrentDirectory;

            if (String.IsNullOrEmpty(fileName))
                proc.FileName = "explorer.exe";
            else
                proc.FileName = fileName;

            proc.Arguments = path;
            proc.Verb = "runas";
            Process.Start(proc);
        }

        public static bool CheckServiceRunning(string serviceName)
        {
            ServiceController sc = new ServiceController(serviceName);

            if (sc.Status == ServiceControllerStatus.Running)
                return true;

            //switch (sc.Status)
            //{
            //    case ServiceControllerStatus.Running:
            //        return "Running";
            //    case ServiceControllerStatus.Stopped:
            //        return "Stopped";
            //    case ServiceControllerStatus.Paused:
            //        return "Paused";
            //    case ServiceControllerStatus.StopPending:
            //        return "Stopping";
            //    case ServiceControllerStatus.StartPending:
            //        return "Starting";
            //    default:
            //        return "Status Changing";
            //}

            return false;
        }
        public static bool StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                if (service.Status != ServiceControllerStatus.Running)
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool RestartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                if (service.Status != ServiceControllerStatus.Running)
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        public static string AssertResults(List<string> errorList)
        {
            string res;
            try
            {
                res = string.Join(Environment.NewLine, errorList.ToArray());
            }
            catch (Exception ex)
            {
                res = "Error doing string join: " + ex.Message;
            }

            return res;
        }

        public static void KillExcelProcess(TestContext testContext)
        {
            Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Exception ex) { testContext.WriteLine("Error killing excel processes: " + p.ProcessName + ", " + ex.Message); }
                }
            }
        }

        public static void KillGatProcess(TestContext testContext)
        {
            string currentProcess = "";
            try
            {
                Process[] procs = Process.GetProcessesByName("gatrun");
                Process[] procs1 = Process.GetProcessesByName("gat");
                Process[] procs2 = Process.GetProcessesByName("gatturnus");

                foreach (var proc in procs)
                {
                    try
                    {
                        currentProcess = proc.ProcessName.ToString();
                        proc.Kill();
                    }
                    catch (Exception ex)
                    {
                        testContext.WriteLine("Error killing Gat processes: " + currentProcess + ", " + ex.Message);
                    }
                }

                foreach (var proc in procs1)
                {
                    try
                    {
                        currentProcess = proc.ProcessName.ToString();
                        proc.Kill();
                    }
                    catch (Exception ex)
                    {
                        testContext.WriteLine("Error killing Gat processes: " + currentProcess + ", " + ex.Message);
                    }
                }

                foreach (var proc in procs2)
                {
                    try
                    {
                        currentProcess = proc.ProcessName.ToString();
                        proc.Kill();
                    }
                    catch (Exception ex)
                    {
                        testContext.WriteLine("Error killing Gat processes: " + currentProcess + ", " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                testContext.WriteLine("Error getting Gat processes: " + ex.Message);
            }
        }

        public static void KillProcessByName(string process, TestContext testContext)
        {
            string currentProcess = "";
            try
            {
                Process[] procs = Process.GetProcessesByName(process);

                foreach (var proc in procs)
                {
                    try
                    {
                        currentProcess = proc.ProcessName.ToString();
                        proc.Kill();
                    }
                    catch (Exception ex)
                    {
                        testContext.WriteLine("Error killing process: " + currentProcess + ", " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                testContext.WriteLine("Error getting Gat processes: " + ex.Message);
            }
        }
        public static bool FileCopy(string file, string sourcePath, string targetPath, TestContext testContext, bool deleteFileIfExists = false)
        {
            // Use Path class to manipulate file and directory paths.
            string sourceFile = Path.Combine(sourcePath, file);
            string destFile = Path.Combine(targetPath, file);
 
            // Create a new target folder, if necessary.
            DirectoryInfo dirInfo = new DirectoryInfo(targetPath);
            if (!dirInfo.Exists)
                Directory.CreateDirectory(targetPath);
           
            // delete the destination file if it already exists.
            if (deleteFileIfExists && File.Exists(destFile))
            {
                try
                {
                    File.Delete(destFile);
                    testContext.WriteLine("File deleted: " + destFile);
                }
                catch (Exception e)
                {
                    testContext.WriteLine("Error deleting file: " + e.Message);
                }
            }

            // overwrite the destination file if it already exists.
            File.Copy(sourceFile, destFile, true);
            testContext.WriteLine("File inserted: " + sourceFile);

            return File.Exists(destFile);
        }

        public static void ExtractFiles(string zipFilePath, string extractPath, TestContext testContext)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(extractPath);
            if (dirInfo.Exists)
                SupportFunctions.DirectoryDelete(extractPath, testContext);

            ZipFile.ExtractToDirectory(zipFilePath, extractPath);
        }

        public static void EditTextFile(string file, string lineToEdit, string textToAdd)
        {
            string text = File.ReadAllText(file);

            if (!text.Contains(lineToEdit))
                lineToEdit = lineToEdit.Replace("default", "Default");

            text = text.Replace(lineToEdit, textToAdd);
            File.WriteAllText(file, text);
        }


        public static void FileCopyAndRenameDestinationFile(string fileName, string NewName, string sourcePath, string targetPath)
        {
            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, NewName);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);
        }

        public static void DeleteZipFiles(string destinationAddressZipFiles, TestContext testContext)
        {
            // Only get files that ends with ".zip"
            var filePaths = Directory.GetFiles(destinationAddressZipFiles, "*.zip", SearchOption.TopDirectoryOnly);
            foreach (string filePath in filePaths)
            {
                FileInfo mFile = new FileInfo(filePath);
                try
                {
                    if (mFile.Extension == ".zip")
                        mFile.Delete();
                }
                catch (Exception e)
                {
                    testContext.WriteLine("Unable to delete file: " + mFile.Name + ", " + e.Message);
                }
            }
        }

        public static bool DirectoryDelete(string path, TestContext testContext)
        {
            if (!Directory.Exists(path))
            {
                testContext.WriteLine("Directory does not exist: " + path);
                return true;
            }

            try
            {
                Directory.Delete(path, true);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

    }

    public static class LoadBalanceTesting
    {
        public static string ReadMemoryData(MemoryType type, bool value = false)
        {
            var privateMemorySize64 = "";
            var workingSet64 = "";
            var peakVirtualMemorySize64 = "";
            var peakPagedMemorySize64 = "";
            var pagedSystemMemorySize64 = "";
            var pagedMemorySize64 = "";
            var nonpagedSystemMemorySize64 = "";

            /*
         PrivateMemorySize
         The number of bytes that the associated process has allocated that cannot be shared with other processes.
         PeakVirtualMemorySize
         The maximum amount of virtual memory that the process has requested.
         PeakPagedMemorySize
         The maximum amount of memory that the associated process has allocated that could be written to the virtual paging file.
         PagedSystemMemorySize
         The amount of memory that the system has allocated on behalf of the associated process that can be written to the virtual memory paging file.
         PagedMemorySize
         The amount of memory that the associated process has allocated that can be written to the virtual memory paging file.
         NonpagedSystemMemorySize
         The amount of memory that the system has allocated on behalf of the associated process that cannot be written to the virtual memory paging file.
        */

            double f = 1048576;
            var workingSet64Value = "";
            var pagedMemorySize64Value = "";
            Process[] localByName = Process.GetProcessesByName("GATTURNUS");
            foreach (Process p in localByName)
            {
                privateMemorySize64 += "Private memory size64: " + (p.PrivateMemorySize64 / f).ToString("#,0") + " MB";
                workingSet64Value = (p.WorkingSet64 / f).ToString("#,0");
                workingSet64 += "Working Set size64: " + workingSet64Value + " MB";
                peakVirtualMemorySize64 += "Peak virtual memory size64: " + (p.PeakVirtualMemorySize64 / f).ToString("#,0") + " MB";
                peakPagedMemorySize64 += "Peak paged memory size64: " + (p.PeakPagedMemorySize64 / f).ToString("#,0") + " MB";
                pagedSystemMemorySize64 += "Paged system memory size64: " + (p.PagedSystemMemorySize64 / f).ToString("#,0") + " MB";
                pagedMemorySize64Value = (p.PagedMemorySize64 / f).ToString("#,0");
                pagedMemorySize64 += "Paged memory size64: " + pagedMemorySize64Value + " MB";
                nonpagedSystemMemorySize64 += "Nonpaged system memory size64: " + (p.NonpagedSystemMemorySize64 / f).ToString("#,0") + " MB";
            }

            switch (type)
            {
                case MemoryType.PrivateMemorySize64:
                    return privateMemorySize64;

                case MemoryType.WorkingSet64:

                    if (value)
                        return workingSet64Value;
                    else
                        return workingSet64;

                case MemoryType.PeakVirtualMemorySize64:
                    return peakVirtualMemorySize64;

                case MemoryType.PeakPagedMemorySize64:
                    return peakPagedMemorySize64;

                case MemoryType.PagedSystemMemorySize64:
                    return pagedSystemMemorySize64;

                case MemoryType.PagedMemorySize64:
                    if (value)
                        return pagedMemorySize64Value;
                    else
                        return pagedMemorySize64;

                case MemoryType.NonpagedSystemMemorySize64:
                    return nonpagedSystemMemorySize64;
                default:
                    return "";
            }
        }

        public static List<string> TimeLapseInSeconds(DateTime timeBefore, DateTime timeAfter, string text, out string elapsedTimeOutput, int bottonLimit = 0, int upperLimit = 0)
        {
            var errorList = new List<string>();

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //// ...
            //sw.Stop();
            ////sw.Reset();
            //Console.WriteLine(text + ": {0}", sw.Elapsed);

            var diff = timeAfter - timeBefore;
            var elapsedTime = Convert.ToInt32(diff.TotalSeconds);

            if (upperLimit > 0 && (elapsedTime > upperLimit))
                errorList.Add(text + " tar lenger tid enn normalt. Forventet: " + upperLimit + ", målt: " + elapsedTime);
            else if (bottonLimit > 0 && (elapsedTime < bottonLimit))
                errorList.Add(text + " tar kortere tid enn normalt. Forventet: " + bottonLimit + ", målt: " + elapsedTime);

            elapsedTimeOutput = (text + ": " + elapsedTime + " sek.");

            return errorList;
        }

        public static TimeSpan Time(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        public enum MemoryType
        {
            PrivateMemorySize64,
            WorkingSet64,
            PeakVirtualMemorySize64,
            PeakPagedMemorySize64,
            PagedSystemMemorySize64,
            PagedMemorySize64,
            NonpagedSystemMemorySize64
        }
    }

    public static class DepartmentNames
    {
        public static string GetATL_1 { get { return "ATL - ATL avdelningen"; } }
        public static string GetStatistikkavd { get { return "95 - Statistikkavdelingen"; } }
        public static string GetMedisinskAvdeling { get { return "1010 - Medisinsk avdeling"; } }
        public static string GetOrtopedisk { get { return "1030 - Ortopedisk avdeling"; } }
        public static string GetDrommeAvdelingen { get { return "2040 - Drømme Avdelingen"; } }
        public static string GetAML_1 { get { return "2060 - AML avdeling 1"; } }
        public static string GetAML_2 { get { return "2065 - AML avdeling 2"; } }
        public static string GetLønnsberegninger2100 { get { return "2100 - Lønnsberegninger"; } }
        public static string GetFleksavdeling2 { get { return "3002 - Fleksavdeling 2"; } }
        public static string GetAnestesi { get { return "4510 - Anestesi"; } }
        public static string GetOperasjon{ get { return "4520 - Operasjon"; } }
        public static string GetDelteOppgaver1 { get { return "4600 - DELTE OPPGAVER 1"; } }
        public static string GetKirurgi { get { return "4620 - Kirurgi"; } }
        public static string GetArbeidsplanklinikken { get { return "5000 - ARBEIDSPLANKLINIKKEN"; } }
        public static string GetArbeidsplanOghjelpeplan { get { return "5010 - Arbeidsplan og hjelpeplan"; } }
        public static string GetMasterplan { get { return "5040 - Masterplan/masterliste"; } }
        public static string GetGodkjenning { get { return "5080 - Godkjenning"; } }
        public static string GetLønnsberegninger { get { return "5090 - Lønnsberegninger"; } }
        public static string GetIverksetting { get { return "5100 - Iverksetting"; } }
        public static string GetDiverse { get { return "5110 - Diverse"; } }
        public static string GetOppgavetildeling { get { return "5130 - Oppgavetildeling"; } }
        public static string GetOmregnetTid { get { return "5140 - Omregnet tid"; } }
        public static string GetFTT { get { return "5150 - Faste tillegg"; } }
        public static string GetFrikoder { get { return "5160 - Frikoder"; } }
        public static string GetYtelse { get { return "5170 - Ytelse"; } }
        public static string GetYtelse_2 { get { return "5175 - Ytelse 2"; } }
        public static string GetKopiering { get { return "5190 - Kopiering"; } }
        public static string GetByttAnsatt { get { return "5200 - Bytt ansatt"; } }
        public static string GetFilterVisning { get { return "5210 - Filter og Visning"; } }
        public static string GetHelgeavtale { get { return "7001 - Helgeavtaleavdelingen"; } }
    }

    public class IniFile
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}
