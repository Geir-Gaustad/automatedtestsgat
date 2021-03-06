namespace _099_Test_Distribusjon_Leveranse
{
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System;
    using System.Collections.Generic;
    using System.CodeDom.Compiler;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Drawing;
    using System.Windows.Input;
    using System.Diagnostics;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    using System.IO;
    using CommonTestData;
    using System.Text;
    using System.Reflection;
    using System.Xml;
    using System.ServiceProcess;
    using System.IO.Compression;
    using UIMapVS2015Classes;
    using _099_Test_Distribusjon_Leveranse.UIMapVS2017Classes;

    public partial class UIMap
    {
        private string DestinationAddressZipFiles = Path.Combine(TestData.GetWorkingDirectory, @"ZipFiles");
        private CommonUIFunctions.UIMap UICommon;

        private TestContext TestContext;
       

        public UIMapVS2015 UIMapVS2015
        {
            get
            {
                if ((this.map1 == null))
                {
                    this.map1 = new UIMapVS2015(TestContext);
                }

                return this.map1;
            }
        }

        private UIMapVS2015 map1;

        public UIMapVS2017 UIMapVS2017
        {
            get
            {
                if ((this.map2 == null))
                {
                    this.map2 = new UIMapVS2017();
                }

                return this.map2;
            }
        }

        private UIMapVS2017 map2;

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }
        public int DelayBetweenActions
        {
            get
            {
                try
                {
                    return TestData.GetDelayBetweenActions;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get GetDelayBetweenActions: " + ex.Message);
                }

                return 200;
            }
        }
        public bool RestoreDatabase()
        {
            return UICommon.RestoreDatabase();
        }

        public string GetMinGatPath()
        {
           return SetupWebBaseParams.GetWebBaseUrMinGat2(CurrentWSHost);
        }

        private string MinGatWishPlanKey()
        {
            var wishPlan = "";
            try
            {
                wishPlan = CommonPathsAndParams.GetWishPlanKey(CurrentWSHost);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Unable to get wishPlankey: " + ex.Message);
            }

            return wishPlan;
        }

        private string GatFlexKey()
        {
            var gatFlex = "";
            try
            {
                //if (isSE)
                //    gatFlex = CommonPathsAndParams.GetGatFlexKey_SE(CurrentWSHost);
                //else
                    gatFlex = CommonPathsAndParams.GetGatFlexKey(CurrentWSHost);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Unable to get gatFlexkey: " + ex.Message);
            }
            return gatFlex;
        }

        private string GatTaskSchedulerKey
        {
            get
            {
                try
                {
                    var gatTaskSchedulerKey = CommonPathsAndParams.GetGatTaskSchedulerKey(CurrentWSHost);
                    return gatTaskSchedulerKey;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get gatTaskSchedulerKey: " + ex.Message);
                }

                return null;
            }
        }
 

        private string SqlConnection()
        {
            var connection = "";

            try
            {
                 connection = UICommon.UIMapVS2017.SqlConnection();
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Unable to get SqlConnection: " + ex.Message);
            }

            return connection;

        }

        private string CurrentTargetFilesAddress
        {
            get
            {
                try
                {
                    return TestData.GetTargetFiles;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get TargetFiles: " + ex.Message);
                }

                return null;
            }
        }

        private string CurrentFileVersion
        {
            get
            {
                try
                {
                    return TestData.GetFileVersion;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get FileVersion: " + ex.Message);
                }

                return null;
            }
        }

        private string CurrentFrameWorkVersion
        {
            get
            {
                try
                {
                    return TestData.GatFrameWorkVersion;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get FrameWorkVersion: " + ex.Message);
                }

                return null;
            }
        }
                     
        private string CurrentWSHost
        {
            get
            {
                try
                {
                    return TestData.GetExecutingServer;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get WSHost: " + ex.Message);
                }

                return null;
            }
        }

        private string GetGatVersion()
        {
            return TestData.GetFileVersion;
        }
        private string GetMinGatVersion()
        {
            var version = GetGatVersion();
            var minGatVersion = "v." + version.Remove(version.LastIndexOf("."));

            return minGatVersion;
        }

        /// <summary>
        /// StartExplorer
        /// </summary>
        public void StartProcess(string path, bool startExplorer, string fileName = "")
        {
            KillGatProcess();
            SupportFunctions.StartProcess(path, startExplorer, fileName);
        }

        public void KillWinExplorer(int delay, bool restartWinExplorer)
        {
            Playback.Wait(delay);

            var currentProcess = "";
            Process[] ps = Process.GetProcessesByName("explorer");
            foreach (var proc in ps)
            {
                try
                {
                    currentProcess = proc.ProcessName.ToString();
                    proc.Kill();
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Error killing WinExplorer processes: " + currentProcess + ", " + ex.Message);
                }
            }

            if (restartWinExplorer)
            {
                Playback.Wait(2000);
                ProcessStartInfo procS = new ProcessStartInfo();
                procS.UseShellExecute = true;
                procS.WorkingDirectory = Environment.CurrentDirectory;
                procS.FileName = "explorer.exe";
                procS.Verb = "runas";
                Process.Start(procS);
            }
        }

        public void GetZipFilesFromServer()
        {
            /*
                    Gat X.X.X NOR
                    Gat Task Scheduler Service X.X.X
                    Gat Web Services 2  X.X.X NOR
                    Gat Web Services  X.X.X NOR
                    GatFlexTimeClock  X.X.X NOR
                    MinGat 2  X.X.X NOR
              */
            var filesToCopy = new List<string>() { "Gat NOR", "MinGat NOR", "Gat Web Services 1 NOR",
                "Gat Web Services 2 NOR", "GatFlexTimeClock NOR", "Gat Task Scheduler Setup" };

            //var filesToCopy = new List<string>() { "Gat NOR", "Gat SWE", "MinGat NOR", "MinGat SWE", "MinGat 2 NOR", "MinGat 2 SWE", "Gat Web Services NOR", "Gat Web Services SWE",
            //    "Gat Web Services 1 NOR", "Gat Web Services 1 SWE", "Gat Web Services 2 NOR", "Gat Web Services 2 SWE", "GatFlexTimeClock NOR", "GatFlexTimeClock SWE", "Gat Task Scheduler Setup", "Gat Task Scheduler Service" };
            UICommon.UIMapVS2017.GetGatZipFiles(filesToCopy, CommonPathsAndParams.TargetAddressZipFiles + CurrentTargetFilesAddress, DestinationAddressZipFiles, CurrentFileVersion, TestContext);
        }

        public void ExtractGatFiles()
        {
            var filePath = DestinationAddressZipFiles + @"\Gat NOR " + CurrentFileVersion + ".zip";
            UICommon.UIMapVS2017.ExtractGatFiles(filePath, TestContext);
        }

        private void EditGatIniFile(string path)
        {
            var gatIni = new IniFile(path);
            var vm = gatIni.Read("ForceQuitOnVersionMismatch");//=1
            gatIni.Write("ForceQuitOnVersionMismatch", "0");
        }        

        public static bool CheckFileExists(string file, string sourcePath)
        {
            if (!Directory.Exists(sourcePath))
                return false;

            string sourceFile = Path.Combine(sourcePath, file);
            return File.Exists(sourceFile);
        }

        //public static void FileCopy_old(string file, string sourcePath, string targetPath)
        //{
        //    // Use Path class to manipulate file and directory paths.
        //    string sourceFile = System.IO.Path.Combine(sourcePath, file);
        //    string destFile = System.IO.Path.Combine(targetPath, file);

        //    // To copy a folder's contents to a new location:
        //    // Create a new target folder, if necessary.
        //    if (!System.IO.Directory.Exists(targetPath))
        //    {
        //        System.IO.Directory.CreateDirectory(targetPath);
        //    }

        //    // To copy a file to another location and 
        //    // overwrite the destination file if it already exists.
        //    System.IO.File.Copy(sourceFile, destFile, true);
        //}


        public static void EditTextFile_old(string file, string lineToEdit, string textToAdd)
        {
            string text = File.ReadAllText(file);

            if (!text.Contains(lineToEdit))
                lineToEdit = lineToEdit.Replace("default", "Default");

            text = text.Replace(lineToEdit, textToAdd);
            File.WriteAllText(file, text);
        }
        
        /// <summary>
        /// RemoveApplicationPoolMinGat
        /// </summary>
        public void RemoveApplicationPoolMinGat()
        {
            #region Variable Declarations
            var treeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem1;
            WinTreeItem uIApplicationPoolsTreeItem = treeItem.UIApplicationPoolsTreeItem;
            WinListItem uIMinGatListItem = this.UIInternetInformationSWindow.UI_listViewWindow.UI_listViewList.UIMinGatListItem;
            WinMenuItem uIRemoveMenuItem = this.UIItemWindow6.UIDropDownMenu.UIRemoveMenuItem;
            WinButton uIYesButton = this.UIConfirmRemoveWindow.UIYesWindow.UIYesButton;
            #endregion

            // Click 'AT-MAN (GATSOFT\geig)' -> 'Application Pools' tree item
            Mouse.Click(uIApplicationPoolsTreeItem, new Point(9, 5));

            // Right-Click 'MinGat' list item
            Mouse.Click(uIMinGatListItem, MouseButtons.Right, ModifierKeys.None, new Point(43, 11));

            // Wait for 1 seconds for user delay between actions; Click 'Remove' menu item
            //Mouse.Click(uIRemoveMenuItem, new Point(72, 7));
            Keyboard.SendKeys("{DOWN 10}{ENTER}");

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(48, 13));
        }

        public void RemoveApplicationPoolsWs()
        {
            #region Variable Declarations
            var treeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem8;
            WinTreeItem uIApplicationPoolsTreeItem = treeItem.UIApplicationPoolsTreeItem;
            WinList uI_listViewList = this.UIInternetInformationSWindow.UI_listViewWindow.UI_listViewList;
            WinListItem uIGatWs2ListItem = this.UIInternetInformationSWindow.UI_listViewWindow.UI_listViewList.UIGatWs2ListItem;
            WinListItem uIGatWs1ListItem = this.UIInternetInformationSWindow.UI_listViewWindow.UI_listViewList.UIGatWs1ListItem;
            WinMenuItem uIRemoveMenuItem = this.UIItemWindow6.UIDropDownMenu.UIRemoveMenuItem;
            WinButton uIYesButton = this.UIConfirmRemoveWindow.UIYesWindow.UIYesButton;
            uIGatWs2ListItem.SearchProperties[WinTreeItem.PropertyNames.Name] = "GatWs2";
            uIGatWs1ListItem.SearchProperties[WinTreeItem.PropertyNames.Name] = "GatWs1";
            #endregion

            // Click 'AT-MAN (GATSOFT\geig)' -> 'Application Pools' tree item
            Mouse.Click(uIApplicationPoolsTreeItem, new Point(34, 14));

            // Select 'GatWs2' in '_listView' list box
            uI_listViewList.SelectedItemsAsString = this.CleanUpWsIISParams.UI_listViewListSelectedItemsAsString;

            // Right-Click 'GatWs2' list item
            Mouse.Click(uIGatWs2ListItem, MouseButtons.Right, ModifierKeys.None, new Point(91, 8));

            // Click 'Remove' menu item
            try
            {
                Mouse.Click(uIRemoveMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{DOWN 10}{ENTER}");
            }

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(51, 15));

            // Right-Click 'GatWs1' list item
            Mouse.Click(uIGatWs1ListItem, MouseButtons.Right, ModifierKeys.None, new Point(68, 7));

            // Click 'Remove' menu item
            try
            {
                Mouse.Click(uIRemoveMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{DOWN 10}{ENTER}");
            }

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(54, 12));
        }

        public void StartGatFromExtractedDir(string department, bool logVersion)
        {
            UICommon.StartGatFromExtractedDir(department, DestinationAddressZipFiles, logVersion);
        }

        public bool CheckLoginEnabled()
        {
            #region Variable Declarations
            WinEdit uIItemEdit = this.UILoginWindow.UIItemWindow.UIItemEdit;
            #endregion

            try
            {
                Playback.Wait(10000);
                var exists = uIItemEdit.WaitForControlCondition(Loginxists);

                if (!exists)
                     exists = uIItemEdit.WaitForControlCondition(Loginxists);

                if (!exists)
                {
                    if (!uIItemEdit.WaitForControlExist())
                        throw new Exception("Login not accessible");
                }
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        private bool Loginxists(UITestControl control)
        {
            WinEdit uIItemEdit = UILoginWindow.UIItemWindow.UIItemEdit;
            return uIItemEdit.Exists;
        }
        private bool IsLoginEnabled(UITestControl control)
        {
            WinEdit uIItemEdit = UILoginWindow.UIItemWindow.UIItemEdit;
            return uIItemEdit.Enabled;
        }

        public void CloseGat()
        {
            try
            {
                UICommon.CloseGat();
            }
            catch (Exception)
            {
                KillGatProcess();
            }
        }

        private void KillGatProcess()
        {
            Playback.Wait(3000);
            SupportFunctions.KillGatProcess(TestContext);
        }

        public void KillProcessByName(string process)
        {
            SupportFunctions.KillProcessByName(process, TestContext);
        }

        public void AssertResults(List<String> errorList)
        {
            Assert.Fail(SupportFunctions.AssertResults(errorList));
        }

        public List<string> Cleanup()
        {
            var errorList = new List<string>();
            try
            {
                //Kill Browser
                KillGatProcess();
                KillProcessByName("iexplore");
                //KillWinExplorer(1000, true);

                //CleanupGat
                if (!SupportFunctions.DirectoryDelete(CommonPathsAndParams.UIExtractedGatFiles, TestContext))
                    errorList.Add("Error deteting Gat directory: " + CommonPathsAndParams.UIExtractedGatFiles);
                
                //CleanUpMinGat
                StartProcess(CommonPathsAndParams.IISManager, true);
                Playback.Wait(2000);
                try
                {
                    RemoveMInGatNew();
                }
                catch (Exception e)
                {
                    errorList.Add("Error removing MinGat from IIS: " + e.Message);
                }

                Playback.Wait(2000);
                try
                {
                    RemoveApplicationPoolMinGat();
                }
                catch (Exception e)
                {
                    errorList.Add("Error removing MinGat applicationpool from IIS: " + e.Message);
                }

                //WS
                try
                {
                    CleanUpWsIIS();
                }
                catch (Exception e)
                {
                    errorList.Add("Error removing applications from IIS: " + e.Message);
                }

                try
                {
                    RemoveApplicationPoolsWs();
                }
                catch (Exception e)
                {
                    errorList.Add("Error removing WS applicationpools from IIS: " + e.Message);
                }

                try
                {
                    CloseIIS();
                }
                catch (Exception)
                {
                    KillProcessByName("InetMgr");
                }

                //DeleteWWWRootDirectories
                if (!SupportFunctions.DirectoryDelete(CommonPathsAndParams.UIWWWRootMinGatDir, TestContext))
                    errorList.Add("Error deleting folder: " + CommonPathsAndParams.UIWWWRootMinGatDir);


                if (!SupportFunctions.DirectoryDelete(CommonPathsAndParams.UIWWWRootGatWS1Dir, TestContext))
                    errorList.Add("Error deleting folder: " + CommonPathsAndParams.UIWWWRootGatWS1Dir);


                if (!SupportFunctions.DirectoryDelete(CommonPathsAndParams.UIWWWRootGatWS2Dir, TestContext))
                    errorList.Add("Error deleting folder: " + CommonPathsAndParams.UIWWWRootGatWS2Dir);

                //Delete Flextime testdirectory
                if (!SupportFunctions.DirectoryDelete(@"C:\Gatsoft\GatFlex", TestContext))
                    errorList.Add("Error deleting GatFlex folder.");

                //Cleanup GatSchedulerService
                try
                {
                    if (!SupportFunctions.StopService(CommonPathsAndParams.GatTaskSchedulerServiceName, 2000))
                        TestContext.WriteLine("Unable to stop " + CommonPathsAndParams.GatTaskSchedulerServiceName);

                    StartProcess(CommonPathsAndParams.UninstallGatTaskScheduler, false);
                    UninstallGatTaskScheduler();

                    //Delete folder "C:\Program Files (x86)\Gat Task Scheduler Service"
                    var deleted = SupportFunctions.DirectoryDelete(CommonPathsAndParams.GatTaskSchedulerFolder, TestContext);

                    if (!deleted)
                        errorList.Add("Error deleting folder: " + CommonPathsAndParams.GatTaskSchedulerFolder);
;
                }
                catch (Exception e)
                {
                    errorList.Add("Error uninstalling GatTaskScheduler: " + e.Message);
                }

                //Delete extracted files
                if (!SupportFunctions.DirectoryDelete(CommonPathsAndParams.UIExtractedMinGatFiles, TestContext))
                    errorList.Add("Error deleting folder: " + CommonPathsAndParams.UIExtractedMinGatFiles);
                
                if (!SupportFunctions.DirectoryDelete(CommonPathsAndParams.UIExtractedGatWS1Files, TestContext))
                    errorList.Add("Error deleting folder: " + CommonPathsAndParams.UIExtractedGatWS1Files);
                
                if (!SupportFunctions.DirectoryDelete(CommonPathsAndParams.UIExtractedGatWS2Files, TestContext))
                    errorList.Add("Error deleting folder: " + CommonPathsAndParams.UIExtractedGatWS2Files);
                
                if (!SupportFunctions.DirectoryDelete(CommonPathsAndParams.UIExtractedGatFlexTimeClockFiles, TestContext))
                    errorList.Add("Error deleting folder: " + CommonPathsAndParams.UIExtractedGatFlexTimeClockFiles);

                if (!SupportFunctions.DirectoryDelete(CommonPathsAndParams.UIExtractedGatTaskSchedulerFiles, TestContext))
                    errorList.Add("Error deleting folder: " + CommonPathsAndParams.UIExtractedGatTaskSchedulerFiles);
     
                //Delete all sourcefiles
                SupportFunctions.DeleteZipFiles(DestinationAddressZipFiles, TestContext);

            }
            catch (Exception e)
            {
                errorList.Add("Cleanup error: " + e.Message);
            }

            return errorList;
        }

        public void RemoveMInGatNew()
        {
            #region Variable Declarations
            var treeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem;
            WinTreeItem uIApplicationPoolsTreeItem = treeItem.UIApplicationPoolsTreeItem;
            WinTreeItem uIMinGat642TreeItem = treeItem.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIMinGat642TreeItem;
            WinMenuItem uIRemoveMenuItem = this.UIItemWindow6.UIDropDownMenu.UIRemoveMenuItem;
            WinButton uIYesButton = this.UIConfirmRemoveWindow.UIYesWindow.UIYesButton;
            #endregion

            // Click 'AT-MAN (GATSOFT\geig)' -> 'Application Pools' tree item
            Mouse.Click(uIApplicationPoolsTreeItem, new Point(2, 10));

            // Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'MinGat642' tree item
            Mouse.Click(uIMinGat642TreeItem, MouseButtons.Right, ModifierKeys.None, new Point(12, 8));

            // Click 'Remove' menu item
            Mouse.Click(uIRemoveMenuItem, new Point(77, 11));

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(55, 10));
        }

        public void RemoveMInGatSwe()
        {
            #region Variable Declarations
            var treeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem;
            WinTreeItem uIMinGat642TreeItem = treeItem.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIMinGat642TreeItem;
            WinMenuItem uIRemoveMenuItem = this.UIItemWindow6.UIDropDownMenu.UIRemoveMenuItem;
            WinButton uIYesButton = this.UIConfirmRemoveWindow.UIYesWindow.UIYesButton;
            #endregion

            // Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'MinGat642' tree item
            uIMinGat642TreeItem.SearchProperties.Add(new PropertyExpression(WinTreeItem.PropertyNames.Name, "MinGat_se", PropertyExpressionOperator.Contains));
            //uIMinGat642TreeItem.SearchProperties["Value"] = "3";
            Mouse.Click(uIMinGat642TreeItem, MouseButtons.Right, ModifierKeys.None, new Point(12, 8));

            // Click 'Remove' menu item
            try
            {
                Mouse.Click(uIRemoveMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{DOWN 7}{ENTER}");
            }

            // Click '&Yes' button
            Mouse.Click(uIYesButton);
        }


        /// <summary>
        /// CleanUpWsIIS - Use 'CleanUpWsIISParams' to pass parameters into this method.
        /// </summary>
        public void CleanUpWsIIS()
        {
            #region Variable Declarations
            var treeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem5;
            var treeItem2 = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem7;
            WinTreeItem uIGatWs1TreeItem = treeItem.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIGatWs1TreeItem;
            WinTreeItem uIGatWs1TreeItem1 = treeItem.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIGatWs1TreeItem;
            WinMenuItem uIRemoveMenuItem = this.UIItemWindow6.UIDropDownMenu.UIRemoveMenuItem;
            WinButton uIYesButton = this.UIConfirmRemoveWindow.UIYesWindow.UIYesButton;
            WinTreeItem uIGatWs2TreeItem = treeItem2.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIGatWs2TreeItem;
            #endregion

            // Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'GatWs1' tree item
            Mouse.Click(uIGatWs1TreeItem, new Point(14, 8));

            // Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'GatWs1' tree item
            Mouse.Click(uIGatWs1TreeItem1, MouseButtons.Right, ModifierKeys.None, new Point(14, 8));

            // Click 'Remove' menu item
            try
            {
                Mouse.Click(uIRemoveMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{DOWN 7}{ENTER}");
            }

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(28, 11));

            // Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'GatWs2' tree item
            Mouse.Click(uIGatWs2TreeItem, MouseButtons.Right, ModifierKeys.None, new Point(26, 11));

            // Click 'Remove' menu item
            try
            {
                Mouse.Click(uIRemoveMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{DOWN 7}{ENTER}");
            }

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(43, 11));

        }

        public void CleanUpWsSweIIS()
        {
            #region Variable Declarations
            var treeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem5;
            var treeItem2 = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem7;
            WinTreeItem uIGatWs1TreeItem = treeItem.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIGatWs1TreeItem;
            WinTreeItem uIGatWs1TreeItem1 = treeItem.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIGatWs1TreeItem;
            WinMenuItem uIRemoveMenuItem = this.UIItemWindow6.UIDropDownMenu.UIRemoveMenuItem;
            WinButton uIYesButton = this.UIConfirmRemoveWindow.UIYesWindow.UIYesButton;
            WinTreeItem uIGatWs2TreeItem = treeItem2.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIGatWs2TreeItem;
            uIGatWs1TreeItem.SearchProperties[WinTreeItem.PropertyNames.Name] = "GatWs1_se";
            uIGatWs1TreeItem1.SearchProperties[WinTreeItem.PropertyNames.Name] = "GatWs1_se";
            uIGatWs2TreeItem.SearchProperties[WinTreeItem.PropertyNames.Name] = "GatWs2_se";
            #endregion

            // Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'GatWs1' tree item
            Mouse.Click(uIGatWs1TreeItem, new Point(14, 8));

            // Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'GatWs1' tree item
            Mouse.Click(uIGatWs1TreeItem1, MouseButtons.Right, ModifierKeys.None, new Point(14, 8));

            // Click 'Remove' menu item
            try
            {
                Mouse.Click(uIRemoveMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{DOWN 7}{ENTER}");
            }

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(28, 11));

            // Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'GatWs2' tree item
            Mouse.Click(uIGatWs2TreeItem, MouseButtons.Right, ModifierKeys.None, new Point(26, 11));

            // Click 'Remove' menu item
            try
            {
                Mouse.Click(uIRemoveMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{DOWN 7}{ENTER}");
            }

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(43, 11));
        }

        public void SearchAndSelectFromAdministration(string searchString, bool goToAdministration)
        {
            #region Variable Declarations
            WinClient uIGatver64238306ASCLAvClient = this.UIGatver64238306ASCLAvWindow.UIItemWindow.UIGatver64238306ASCLAvClient;
            WinClient uIAdministrasjonClient = this.UIGatver64238306ASCLAvWindow.UIItemWindow1.UIAdministrasjonClient;
            WinEdit uIItemEdit = this.UIVelgradWindow.UIItemWindow.UIItemEdit;
            #endregion

            if (goToAdministration)
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Administration);
            }

            UICommon.SelectFromAdministration(searchString);
        }


        /// <summary>
        /// AddEmployeesToFlex - Use 'AddEmployeesToFlexParams' to pass parameters into this method.
        /// </summary>
        public void AddEmployeesToFlex()
        {
            #region Variable Declarations
            var searchString = "";
            //WinClient uIGatver64238306ASCLAvClient = this.UIGatver64238306ASCLAvWindow.UIItemWindow.UIGatver64238306ASCLAvClient;
            //WinClient uIAdministrasjonClient = this.UIGatver64238306ASCLAvWindow.UIItemWindow1.UIAdministrasjonClient;
            //WinEdit uIItemEdit = this.UIVelgradWindow.UIItemWindow.UIItemEdit;
            WinClient uIOppsettavstemplingskClient = this.UIOppsettavstemplingskWindow.UIItemWindow.UIOppsettavstemplingskClient;
            WinClient uIAnsatteClient = this.UIOppsettavstemplingskWindow.UIItemWindow1.UIAnsatteClient;
            WinButton uIOKButton = this.UIOppsettavstemplingskWindow.UIItemWindow.UIOppsettavstemplingskClient.UIOKButton;
            WinClient uIItemClient = this.UIOppsettavstemplingskWindow.UIItemWindow.UIOppsettavstemplingskClient.UIItemClient;

            //if (isSE)
            //{
            //    UIOppsettavstemplingskWindow.SearchProperties.Remove(WinWindow.PropertyNames.Name);
            //    UIOppsettavstemplingskWindow.SearchProperties[WinWindow.PropertyNames.Name] = "Inställning av stämplingsskärm";
            //    UIOppsettavstemplingskWindow.WindowTitles.Add("Inställning av stämplingsskärm");
            //    UIOppsettavstemplingskWindow.UIItemWindow.WindowTitles.Add("Inställning av stämplingsskärm");
            //    UIOppsettavstemplingskWindow.UIItemWindow1.WindowTitles.Add("Inställning av stämplingsskärm");
            //    UIGatver64238306ASCLAvWindow.UIItemWindow1.UIAdministrasjonClient.WindowTitles.Add("Inställning av stämplingsskärm");
            //    UIOppsettavstemplingskWindow.UIItemWindow.UIOppsettavstemplingskClient.WindowTitles.Add("Inställning av stämplingsskärm");

            //    uIOKButton.WindowTitles.Add("Inställning av stämplingsskärm");
            //    uIItemClient.WindowTitles.Add("Inställning av stämplingsskärm");
            //}            
            #endregion

            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Administration);

            //// Click 'Administrasjon' client
            //Mouse.Click(uIAdministrasjonClient, new Point(327, 50));

         
            // Type 'ENHET OPPSETT' in text box
            //if (isSE)
            //    searchString = this.AddEmployeesToFlexParams.UIIENHET_OPPSETT_se;
            //else
                searchString = this.AddEmployeesToFlexParams.UIIENHET_OPPSETT;

            UICommon.SelectFromAdministration(searchString);

            // Click 'Oppsett av stempling skjerm' client
            Mouse.Click(uIOppsettavstemplingskClient, new Point(75, 18));

            // Double-Click 'Ansatte' client
            Mouse.DoubleClick(uIAnsatteClient, new Point(116, 28));

            // Double-Click 'Ansatte' client
            Mouse.DoubleClick(uIAnsatteClient, new Point(116, 28));

            // Click 'OK' button
            Mouse.Click(uIOKButton, new Point(29, 23));

            // Click client
            Mouse.Click(uIItemClient, new Point(279, 23));

            //ClearAdministrationSearchString();
        }

        public void StartFlexTimeClockSwe()
        {
            #region Variable Declarations
            //Stemple
            var alleAnsatte = UIGATFlexWindow1.UIDocumentsHostTabList.UIAlleansatteCustom;
            alleAnsatte.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            alleAnsatte.SearchProperties[DXTestControl.PropertyNames.Name] = "Alla anställda";

            var ansatt = UIGATFlexWindow1.UIDocumentsHostTabList.UIAnsattCustom;
            ansatt.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            ansatt.SearchProperties[DXTestControl.PropertyNames.Name] = "Anställd";

            var alleAnsatte2 = UIGATFlexWindow11.UIDocumentsHostTabList.UIAnsattCustom;
            alleAnsatte2.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            alleAnsatte2.SearchProperties[DXTestControl.PropertyNames.Name] = "Alla anställda";

            //Søk
            var searchAnsatt = UIGATFlexWindow11.UIDocumentsHostTabList.UISøketteransattCustom;
            searchAnsatt.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            searchAnsatt.SearchProperties[DXTestControl.PropertyNames.Name] = "Sök efter anställd";

            var ExePath = "C:\\gatsoft\\GatFlex_se\\GatFlexTimeClock.exe";
            var AlternateExePath = "C:\\gatsoft\\GatFlex_se\\GatFlexTimeClock.exe";
            #endregion

            // Launch 'C:\gatsoft\GatFlex_se\GatFlexTimeClock.exe'
            ApplicationUnderTest gatFlexTimeClockApplication = ApplicationUnderTest.Launch(ExePath, AlternateExePath);
        }

        /// <summary>
        /// AddApplicationPoolMinGat - Use 'AddApplicationPoolMinGatParams' to pass parameters into this method.
        /// </summary>
        public void AddApplicationPoolIIS(string poolName, bool closeIIS = false)
        {
            // SupportFunctions.StartProcess(IISManager, true);

            Playback.Wait(1000);
            UICommon.AddApplicationPoolIIS(poolName, CurrentFrameWorkVersion);
        }

        /// <summary>
        /// AddMInGat - Use 'AddMInGatParams' to pass parameters into this method.
        /// </summary>
        public void ConvertToAppInIIS(string poolName)
        {

            Playback.Wait(1000);
            UICommon.ConvertToAppInIIS(poolName);
            Playback.Wait(1000);
        }

        public void ConfigureMinGatForIIS()
        {
            //var version = CurrentFileVersion.Remove(CurrentFileVersion.LastIndexOf("."), 2);
            var filePath = DestinationAddressZipFiles + @"\MinGat NOR " + CurrentFileVersion + ".zip";

            UICommon.UIMapVS2017.ConfigureMinGatForIIS(
              filePath, CommonPathsAndParams.UIExtractedMinGatFiles,
              CommonPathsAndParams.MinGatAppsettingsConfig,
              CommonPathsAndParams.LineToFindMinGatAppsettings,
              MinGatWishPlanKey(), CommonPathsAndParams.MinGatConnectionStringsConfig, 
              CommonPathsAndParams.LineToFindConnectionString,
              SqlConnection(), CommonPathsAndParams.UIWWWRootMinGatDir, TestContext);
        }

        public void ConfigureWS1ForIIS()
        {
            var version = CurrentFileVersion.Remove(CurrentFileVersion.LastIndexOf("."), 2);
            var filePath = DestinationAddressZipFiles + @"\Gat Web Services 1 NOR " + CurrentFileVersion + ".zip";
            //var filePath_se = DestinationAddressZipFiles + @"\Gat Web Services 1 SWE " + CurrentFileVersion + ".zip";

            SupportFunctions.ExtractFiles(filePath, CommonPathsAndParams.UIExtractedGatWS1Files, TestContext);

            //WS1
            SupportFunctions.EditTextFile(CommonPathsAndParams.Ws1WebConfig, CommonPathsAndParams.LineToFindConnectionString, SqlConnection());

            //Ny fra 2018.1.1(GDPR) må legge inn brukernavn og passord
            SupportFunctions.EditTextFile(CommonPathsAndParams.Ws1WebConfig, CommonPathsAndParams.LineToFindFlexServiceUser, @"<add key=""FlexServiceUser"" value=""test""/>");
            SupportFunctions.EditTextFile(CommonPathsAndParams.Ws1WebConfig, CommonPathsAndParams.LineToFindFlexServicePassword, @"<add key=""FlexServicePassword"" value=""password""/>");

            //kopiere filer fra template til Ws1
            SupportFunctions.FileCopy("web.config", CommonPathsAndParams.UIExtractedGatWS1Files + @"\Template", CommonPathsAndParams.UIExtractedGatWS1Files, TestContext);

            //Kopiere MinGatkatalog over på wwwroot
            string sourcePath = CommonPathsAndParams.UIExtractedGatWS1Files;
            string targetPath = CommonPathsAndParams.UIWWWRootGatWS1Dir;
            SupportFunctions.DirectoryCopy(sourcePath, targetPath, true);
        }

        public void ConfigureWS2ForIIS()
        {
            var version = CurrentFileVersion.Remove(CurrentFileVersion.LastIndexOf("."), 2);
            var filePath = DestinationAddressZipFiles + @"\Gat Web Services 2 NOR " + CurrentFileVersion + ".zip";
            var filePath_se = DestinationAddressZipFiles + @"\Gat Web Services 2 SWE " + CurrentFileVersion + ".zip";

            //WS2
            SupportFunctions.ExtractFiles(filePath, CommonPathsAndParams.UIExtractedGatWS2Files, TestContext);
            SupportFunctions.EditTextFile(CommonPathsAndParams.Ws2WebConfig, CommonPathsAndParams.LineToFindConnectionString, SqlConnection());

            //kopiere filer fra template til Ws1
            SupportFunctions.FileCopy("web.config", CommonPathsAndParams.UIExtractedGatWS2Files + @"\Template", CommonPathsAndParams.UIExtractedGatWS2Files, TestContext);

            //Kopiere MinGatkatalog over på wwwroot
            string sourcePath = CommonPathsAndParams.UIExtractedGatWS2Files;
            string targetPath = CommonPathsAndParams.UIWWWRootGatWS2Dir;
            SupportFunctions.DirectoryCopy(sourcePath, targetPath, true);
        }

        public void ConfigureGatFlexTimeClock()
        {
            var version = CurrentFileVersion.Remove(CurrentFileVersion.LastIndexOf("."), 2);
            var filePath = DestinationAddressZipFiles + @"\GatFlexTimeClock NOR " + CurrentFileVersion + ".zip";
            var filePath_se = DestinationAddressZipFiles + @"\GatFlexTimeClock SWE " + CurrentFileVersion + ".zip";

            SupportFunctions.ExtractFiles(filePath, CommonPathsAndParams.UIExtractedGatFlexTimeClockFiles, TestContext);

            //kopiere filer fra template til GatFlex
            SupportFunctions.FileCopy("GatFlexTimeClock.exe.config", CommonPathsAndParams.UIExtractedGatFlexTimeClockFiles + @"\Template", CommonPathsAndParams.UIExtractedGatFlexTimeClockFiles, TestContext);

            //Legg inn adresse til webservice i config fil
            SupportFunctions.EditTextFile(CommonPathsAndParams.GatFlexConfig, CommonPathsAndParams.LineToFindGatFlexConfig, GatFlexKey());

            //Kopiere MinGatkatalog over på wwwroot
            string sourcePath = CommonPathsAndParams.UIExtractedGatFlexTimeClockFiles;
            string targetPath = @"C:\Gatsoft\GatFlex";
            SupportFunctions.DirectoryCopy(sourcePath, targetPath, true);
        }

        public bool ConfigureGatTaskScheduler()
        {
            var filePath = DestinationAddressZipFiles + @"\Gat Task Scheduler Setup " + CurrentFileVersion + ".zip";
            SupportFunctions.ExtractFiles(filePath, CommonPathsAndParams.UIExtractedGatTaskSchedulerFiles, TestContext);

            StartProcess(CommonPathsAndParams.InstallGatTaskScheduler, false);
            if (!InstallGatTaskScheduler())
                return false;

            //Legg inn adresse til webservice i config fil
            SupportFunctions.EditTextFile(CommonPathsAndParams.GatTaskSchedulerConfig, CommonPathsAndParams.TextToFindInGatTaskSchedulerConfig, GatTaskSchedulerKey);

            return true;
        }

        private bool InstallGatTaskScheduler()
        {
            #region Variable Declarations
            WinButton uINextButton = this.UISetupGatTaskScheduleWindow.UISetupGatTaskScheduleClient.UINextButton;
            WinButton uIInstallButton = this.UISetupGatTaskScheduleWindow.UISetupGatTaskScheduleClient.UIInstallButton;
            WinButton uIFinishButton = this.UISetupGatTaskScheduleWindow.UISetupGatTaskScheduleClient.UIFinishButton;
            #endregion

            // Wait for 1 seconds for user delay between actions; Click '&Next >' button
            try
            {
                uINextButton.WaitForControlReady(10000);
                Mouse.Click(uINextButton);
            }
            catch (Exception)
            {
                Keyboard.SendKeys(UISetupGatTaskScheduleWindow, "n", ModifierKeys.Alt);
                TestContext.WriteLine("Error clicking Next in GatTaskScheduler installation");
            }

            // Wait for 1 seconds for user delay between actions; Click '&Next >' button
            try
            {
                uINextButton.WaitForControlReady(10000);
                Mouse.Click(uINextButton);
            }
            catch (Exception)
            {
                Keyboard.SendKeys(UISetupGatTaskScheduleWindow, "n", ModifierKeys.Alt);
                TestContext.WriteLine("Error clicking Next in GatTaskScheduler installation");
            }

            // Wait for 1 seconds for user delay between actions; Click '&Install' button
            try
            {
                uIInstallButton.WaitForControlReady(10000);
                Mouse.Click(uIInstallButton);
            }
            catch (Exception)
            {
                Keyboard.SendKeys(UISetupGatTaskScheduleWindow, "i", ModifierKeys.Alt);
                TestContext.WriteLine("Error clicking Install in GatTaskScheduler installation");
            }

            // Wait for 1 seconds for user delay between actions; Click '&Finish' button
            try
            {
                uIFinishButton.WaitForControlReady(10000);
                Mouse.Click(uIFinishButton);
            }
            catch (Exception)
            {
                try
                {
                    Keyboard.SendKeys(UISetupGatTaskScheduleWindow, "f", ModifierKeys.Alt);
                    TestContext.WriteLine("Error clicking Finish in GatTaskScheduler installation");
                }
                catch (Exception)
                {
                    return false;
                }
            }

            var list = StartGatTaskSchedulerService();
            if (list.Count != 0)
                return false;

            return true;
        }

        private List<string> StartGatTaskSchedulerService()
        {
            var errorList = new List<string>();
            var serviceName = CommonPathsAndParams.GatTaskSchedulerServiceName;

            if (!SupportFunctions.CheckServiceRunning(serviceName))
            {
                try
                {
                    if (!SupportFunctions.StartService(serviceName, 1000))
                    {
                        errorList.Add("Unable to start " + serviceName);
                        if (!SupportFunctions.RestartService(serviceName, 1000))
                        {
                            errorList.Add("Unable to restart " + serviceName);
                        }
                    }
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message);
                }
            }

            Playback.Wait(1500);

            if (!SupportFunctions.CheckServiceRunning(serviceName))
                errorList.Add("GatTaskSchedulerService not running");

            return errorList;
        }
        public void CheckGatFlexTimeClockConfig()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("ScreenId", "1");
            dictionary.Add("Username", "test");
            dictionary.Add("Password", "password");
            dictionary.Add("ApplicationSource", "GatFlexTimeClock");
            dictionary.Add("WebServiceUrl", @"http://SERVER/WEBSERVICE/FlexService.asmx");
            dictionary.Add("IsInDebugMode", "False");
            dictionary.Add("LanguageCode", "nb-NO");

            var xDoc = new XmlDocument();
            xDoc.Load(CommonPathsAndParams.GatFlexConfig);

            XmlNodeList elemList = xDoc.GetElementsByTagName("applicationSettings");
            for (int i = 0; i < elemList.Count; i++)
            {
                for (int i2 = 0; i2 < elemList[i].ChildNodes[0].ChildNodes.Count; i2++)
                {
                    var node = elemList[i].ChildNodes[0].ChildNodes[i2];

                    var attrib = node.Attributes[0].Value;
                    var configFileValue = node.InnerText;
                    string checkValue = "";
                    dictionary.TryGetValue(attrib, out checkValue);

                    if (!String.IsNullOrEmpty(checkValue))
                    {
                        Assert.AreEqual(configFileValue, checkValue, "Uventet verdi: " + attrib);
                    }
                }
            }
        }

        public void SetupWebServiceBaseTaskScheduler()
        {
            #region Variable Declarations
            WinClient uIItemClient = this.UIBasisURLforWebservicWindow.UIItemWindow.UIItemClient;
            WinClient uIItemClient1 = this.UIBasisURLforWebservicWindow.UIBasisURLforWebservicClient.UIItemClient;
            WinEdit uIItemEdit = this.UIBasisURLforWebservicWindow.UIHttpatmanWindow.UIItemEdit;
            WinButton uIOKButton = this.UIBasisURLforWebservicWindow.UIItemClient.UIOKButton;
            WinClient uIItemClient2 = this.UIBasisURLforWebservicWindow.UIItemClient1.UIItemClient;
            #endregion

            var gatWs2Path = SetupWebBaseParams.GetWebServiceBaseUrlWs2(CurrentWSHost);
            var gatWs1Path = SetupWebBaseParams.GetWebServiceBaseUrlWs1(CurrentWSHost);
            var gatTaskSchedulerWsPath = SetupWebBaseParams.GetWebServiceBaseGatTaskSchedulerWs(CurrentWSHost);

            // Click client
            Mouse.Click(uIItemClient, new Point(60, 28));

            //Click client
            Mouse.Click(uIItemClient1, new Point(93, 30));

            // Type 'http://at-man/gatws2' in text box
            if (uIItemEdit.Text == gatWs2Path)
                CancelEditWsUrl();
            else
            {
                uIItemEdit.Text = gatWs2Path;
                // Click 'OK' button
                Mouse.Click(uIOKButton, new Point(38, 25));
                Keyboard.SendKeys(uIItemClient, "{DOWN}");
            }

            //Click client
            Mouse.Click(uIItemClient, new Point(60, 44));

            // Click client
            Mouse.Click(uIItemClient1, new Point(95, 31));

            // Type 'http://at-man/gatws1' in text box
            if (uIItemEdit.Text == gatWs1Path)
                CancelEditWsUrl();
            else
            {
                uIItemEdit.Text = gatWs1Path;
                // Click 'OK' button
                Mouse.Click(uIOKButton, new Point(40, 33));
                Keyboard.SendKeys(uIItemClient, "{DOWN}");
            }
            
            //Click client
            Mouse.Click(uIItemClient, new Point(60, 60));

            // Click client
            Mouse.Click(uIItemClient1, new Point(94, 25));

            // Type 'http://at-man:3333' in text box
            if (uIItemEdit.Text == gatTaskSchedulerWsPath)
                CancelEditWsUrl();
            else
            {
                uIItemEdit.Text = gatTaskSchedulerWsPath;
                // Click 'OK' button
                Mouse.Click(uIOKButton, new Point(33, 26));
            }
            
            // Click client
            Mouse.Click(uIItemClient2, new Point(348, 28));

        }

        public List<string> StartGatTaskScheduler()
        {
            var errorList = new List<string>();

            var serviceName = CommonPathsAndParams.GatTaskSchedulerServiceName;
            try
            {
                errorList.AddRange(StartGatTaskSchedulerService());

                Playback.Wait(3000);
                StartProcess(SetupWebBaseParams.GetWebServiceBaseGatTaskSchedulerWs(CurrentWSHost), true, "iexplore");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public void CheckGatTaskSchedulerVersion()
        {
            #region Variable Declarations
            HtmlDiv uIGatScheduler64238741Pane = this.UIGatSchedulerInternetWindow.UIGatSchedulerDocument.UIGatScheduler64238741Pane;
            #endregion

            var expectedVersion = "GatScheduler " + CurrentFileVersion;

            var innerText = uIGatScheduler64238741Pane.InnerText;
            var currentVersion = innerText.Remove(innerText.LastIndexOf("."));
            Assert.AreEqual(expectedVersion, currentVersion, "Feil i  GatTaskScheduler versjonsnr.");
            TestContext.WriteLine("GatTaskScheduler versjon. " + innerText);
        }

        public void CheckServicesRunningStatus()
        {
            #region Variable Declarations
            HtmlDiv service1 = this.UIGatSchedulerInternetWindow.UIGatSchedulerDocument.UIBehandlekømedendringPane;
            HtmlDiv service2 = this.UIGatSchedulerInternetWindow.UIGatSchedulerDocument.UIKalkulerAMLbruddPane;
            HtmlDiv service3 = this.UIGatSchedulerInternetWindow.UIGatSchedulerDocument.UIOppdatereBussinessAnPane;
            HtmlDiv uIErrorPane = this.UIGatSchedulerInternetWindow.UIGatSchedulerDocument.UIErrorPane;
            HtmlDiv uIErrorPane1 = this.UIGatSchedulerInternetWindow.UIGatSchedulerDocument.UIErrorPane1;
            HtmlDiv uIErrorPane2 = this.UIGatSchedulerInternetWindow.UIGatSchedulerDocument.UIErrorPane2;
            #endregion

            TestContext.WriteLine(service1.InnerText + ": " + uIErrorPane.InnerText);
            TestContext.WriteLine(service2.InnerText + ": " + uIErrorPane1.InnerText);
            TestContext.WriteLine(service3.InnerText + ": " + uIErrorPane2.InnerText);
       }
        public string DepFleksavdeling2
        {
            get { return UICommon.DepFleksavdeling2; }
        }

        public bool SelectDepartmentByName_old(string depName)
        {
            #region Variable Declarations
            var depTable = UIItemWindow1.UIGSLayoutControlCustom.UILayoutControlGroup1LayoutGroup.UILayDepartmentsLayoutControlItem.UIGSGroupControlClient.UIGSGridControlTable;
            #endregion

            UICommon.ClearOtherDepartments();
            Playback.Wait(1000);

            var view = depTable.Views[0];
            for (int i = 0; i < view.RowCount; i++)
            {
                var val = view.GetCellValue("cDepName", i).ToString();
                if (val.Contains(depName))
                {
                    TestContext.WriteLine("Department found: " + val);
                    var selectCell = view.GetCell("cDepName", i);
                    try
                    {
                        Mouse.DoubleClick(selectCell);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// CheckGatFlexSweLanguage - Use 'CheckGatFlexSweLanguageExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckGatFlexSweLanguage()
        {
            #region Variable Declarations
            //DXTestControl uILabelControlLabel = this.UIGATFlexWindow11.UIDocumentsHostTabList.UIAllaanställdaCustom.UIEmployeeSelectionConCustom.UILabelControlLabel;
            var sweCheck = this.UIGATFlexWindow11.UIDocumentsHostTabList.UIAllaanställdaCustom;
            #endregion

            if (sweCheck.SearchProperties[DXTestControl.PropertyNames.Name] != "Alla anställda")
                Assert.Fail("Feil i språk(Alla anställda): " + sweCheck.SearchProperties[DXTestControl.PropertyNames.Name].ToString());
        }

        /// <summary>
        /// SeachHeHa - Use 'SeachHeHaParams' to pass parameters into this method.
        /// </summary>
        public void SeachHeHa()
        {
            #region Variable Declarations
            WinClient uIWindowsUIButtonPanelClient = this.UIGATFlexWindow1.UIWindowsUIButtonPanelClient;
            DXTextEdit uITxtSearchEdit = this.UIGATFlexWindow11.UIDocumentsHostTabList.UISøketteransattCustom.UIEmployeeSearchControCustom.UITxtSearchEdit;
            DXScrollableControl uIXscMainScrollableControl = this.UIGATFlexWindow11.UIDocumentsHostTabList.UIAnsattCustom.UIEmployeeClockControlCustom.UIXscMainScrollableControl;
            #endregion

            // Click 'windowsUIButtonPanel1' client
            Mouse.Click(uIWindowsUIButtonPanelClient, new Point(1856, 29));

            // Type 'HEHA' in 'txtSearch' text box
            //ValueAsString
            uITxtSearchEdit.ValueAsString = this.SeachHeHaParams.UITxtSearchEditValueAsString;
            //Keyboard.SendKeys(uITxtSearchEdit, this.SeachHeHaParams.UITxtSearchEditValueAsString);

            // Type 'Control + i' in 'xscMain' ScrollableControl
            Keyboard.SendKeys(uITxtSearchEdit, this.SeachHeHaParams.UIXscMainScrollableControlSendKeys);
        }


        public void OpenGatHelpfile()
        {
            OpenHelpFile();
            SelectOpenGatHelpInDropDown();
        }

        /// <summary>
        /// CheckHelpfileVersion - Use 'CheckHelpfileVersionExpectedValues' to pass parameters into this method.
        /// </summary>
        public List<string> CheckGatHelpfileVersion()
        {
            var errorList = new List<string>();
            #region Variable Declarations
            WinTitleBar uIGatver64HjelpCOPYRIGTitleBar = this.UIGatver64HjelpCOPYRIGWindow.UIGatver64HjelpCOPYRIGTitleBar;

            var helpfileVersion = "Unknown";
            var gatVersion = "Unknown";
            #endregion

            Playback.Wait(2000);
            //if (isSe)
            //    gatVersion = "HTML Help";
            //else
                gatVersion = GetGatVersion();

            try
            {
                Playback.Wait(2000);
                var displayText = uIGatver64HjelpCOPYRIGTitleBar.DisplayText;

                //if (isSe)
                //    helpfileVersion = displayText;
                //else
                //{
                    gatVersion = gatVersion.Remove(gatVersion.LastIndexOf("."));
                    
                    displayText = displayText.Remove(0, displayText.IndexOf(".") + 1).Trim();
                    helpfileVersion = displayText.Remove(6);
               // }
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Error reading helpfile version: " + e.Message);
            }

            try
            {
                // Verify that the 'DisplayText' property of 'Gat ver. 20XX.X.X Hjelp
                Assert.AreEqual(gatVersion, helpfileVersion, "Wrong Helpfile version");
                TestContext.WriteLine("Hjelpefilversjon Ok: " + helpfileVersion);
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av hjelpefilversjon: " + e.Message);
            }

            return errorList;
        }

        public List<string> NavigateAndCheckMinGat()
        {
            var errorList = new List<string>();

            UIMapVS2015.ClickReminders();
            try
            {
                UIMapVS2015.UIStartsidenMinGatv653Window.UIPåminnelserMinGatv65Document.UIPageTitleCustom.DrawHighlight();

                //if (se)
                //    UIMapVS2015.CheckReminders_se();
                //else
                    UIMapVS2015.CheckReminders();
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av Påminnelser: " + e.Message);
            }

            Playback.Wait(1000);
            UIMapVS2015.ClickSelfService();
            try
            {
                UIMapVS2015.UIStartsidenMinGatv653Window.UIForespørslerMinGatv6Document.UIPageTitleCustom.DrawHighlight();
                //if (se)
                //    UIMapVS2015.CheckSelfService_se();
                //else
                    UIMapVS2015.CheckSelfService();
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av Forespørsler: " + e.Message);
            }

            Playback.Wait(1000);
            UIMapVS2015.ClickBanks();
            try
            {
                UIMapVS2015.UIStartsidenMinGatv653Window.UIMinebankerMinGatv653Document.UIPageTitleCustom.DrawHighlight();
                //if (se)
                //    UIMapVS2015.CheckBanks_se();
                //else
                    UIMapVS2015.CheckBanks();
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av Banker: " + e.Message);
            }

            Playback.Wait(1000);
            UIMapVS2015.ClickShowWeek();
            try
            {

                UIMapVS2015.UIStartsidenMinGatv653Window.UIUkevisningMinGatv653Document.UIPageTitleCustom.DrawHighlight();
                //if (se)
                //    UIMapVS2015.CheckShowWeek_se();
                //else
                    UIMapVS2015.CheckShowWeek();
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av Uke: " + e.Message);
            }

            Playback.Wait(1000);
            UIMapVS2015.ClickTasks();
            try
            {
                UIMapVS2015.UIStartsidenMinGatv653Window.UIOppgaveoversiktMinGaDocument.UIPageTitleCustom.DrawHighlight();
                //if (se)
                //    UIMapVS2015.CheckTasks_se();
                //else
                    UIMapVS2015.CheckTasks();
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av Oppgaver: " + e.Message);
            }

            Playback.Wait(1000);
            UIMapVS2015.ClickShiftbook();
            try
            {
                UIMapVS2015.UIStartsidenMinGatv653Window.UIVaktbokMinGatv653438Document.UIPageTitleCustom.DrawHighlight();
                //if (se)
                //    UIMapVS2015.CheckShiftbook_se();
                //else
                    UIMapVS2015.CheckShiftbook();
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av Vaktbok: " + e.Message);
            }

            Playback.Wait(1000);
            UIMapVS2015.ClickCalendar();
            try
            {
                UIMapVS2015.UIStartsidenMinGatv653Window.UIMinkalenderMinGatv65Document.UIPageTitleCustom.DrawHighlight();
                //if (se)
                //    UIMapVS2015.CheckCalendar_se();
                //else
                    UIMapVS2015.CheckCalendar();
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av Kalender: " + e.Message);
            }

            Playback.Wait(1000);
            UIMapVS2015.ClickHome();
            try
            {
                UIMapVS2015.UIStartsidenMinGatv653Window.UIStartsidenMinGatv653Document1.UIPageTitleCustom.DrawHighlight();
                //if (se)
                //    UIMapVS2015.CheckHome_se();
                //else
                    UIMapVS2015.CheckHome();
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av Starsiden: " + e.Message);
            }

            Playback.Wait(1000);
            UIMapVS2015.ClickMySalaryFromMenu();
            try
            {
                UIMapVS2015.UIStartsidenMinGatv653Window.UIMinlønnsoversiktMinGDocument.UIPageTitleCustom.DrawHighlight();
                //if (se)
                //    UIMapVS2015.CheckMySalary_se();
                //else
                    UIMapVS2015.CheckMySalary();
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av Lønnsoversikt: " + e.Message);
            }

            Playback.Wait(1000);

            try
            {
                UIMapVS2015.ClickHome();
                //if (se)
                //    UIMapVS2017.OpenWishplan_se();
                //else
                    UIMapVS2017.OpenWishplan();

                UIMapVS2017.ClickWishPlanLabelToShowOlderWishPlans();

                //if (se)
                //    UIMapVS2017.SelectWishplan_se();
                //else
                    UIMapVS2017.SelectWishplan();


                UIMapVS2015.CheckForCertificateError(false);
                Playback.Wait(3000);
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved åpning av Ønskeplan: " + e.Message);
            }

            try
            {
                Playback.Wait(10000);
                try
                {
                    UIMapVS2017.UIMinønskeplanMinGatv2Window.UIItemWindow.UISilverlightControlWindow.WaitForControlExist();
                    UIMapVS2017.UIMinønskeplanMinGatv2Window.UIItemWindow.UISilverlightControlWindow.UIØnskeplanforBASEPLANText.WaitForControlExist();
                }
                catch (Exception)
                {
                    try
                    {
                        UIMapVS2017.UIMinønskeplanMinGatv2Window.UIItemWindow.UISilverlightControlWindow.UIØnskeplanforBASEPLANText.WaitForControlExist();
                    }
                    catch (Exception)
                    {
                        UIMapVS2017.UIMinønskeplanMinGatv2Window.UIItemWindow.UISilverlightControlWindow.UIØnskeplanforBASEPLANText.WaitForControlExist();
                    }
                }

                UIMapVS2017.CheckWishPlan();
            }
            catch (Exception e)
            {
                errorList.Add("Feilet ved sjekk av Ønskeplan: " + e.Message);
            }

            //try
            //{
            //    Playback.Wait(1000);
            //    UIMapVS2015.WishplanSnapshot();
            //}
            //catch (Exception e)
            //{
            //    errorList.Add("Feilet ved snapshot av Ønskeplan: " + e.Message);
            //}
                      
            try
            {
                UIMapVS2015.CloseWishplan();
                Playback.Wait(2000);
                UIMapVS2015.ClickHome();
            }
            catch
            {
                errorList.Add("Feil ved lukking av Ønskeplan!");
            }

            return errorList;
        }

        /// <summary>
        /// CheckminGatHelpFileVersion - Use 'CheckminGatHelpFileVersionExpectedValues' to pass parameters into this method.
        /// </summary>
        public List<string> CheckMinGatHelpFileVersion()
        {
            // Verify that the 'InnerText' property of 'MinGat Hjelp v.20XX.X
          
            #region Variable Declarations
            var errorList = new List<string>();
            UIMinGatHjelpv64COPYRIWindow.SearchProperties.Remove(WinWindow.PropertyNames.Name);
            UIMinGatHjelpv64COPYRIWindow.SearchProperties.Add(WinWindow.PropertyNames.Name, @"MinGat", PropertyExpressionOperator.Contains);
            UIMinGatHjelpv64COPYRIWindow.WindowTitles.Add("MinGat");

            UIMinGatHjelpv64COPYRIWindow.FilterProperties.Remove(Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Title);
            UIMinGatHjelpv64COPYRIWindow.UIMinGatHjelpv64COPYRIDocument.FilterProperties.Add(Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Title, @"MinGat", PropertyExpressionOperator.Contains);
            UIMinGatHjelpv64COPYRIWindow.UIMinGatHjelpv64COPYRIDocument.WindowTitles.Add("MinGat");
            
            HtmlDiv uIMinGatHjelpv64COPYRIPane = this.UIMinGatHjelpv64COPYRIWindow.UIMinGatHjelpv64COPYRIDocument.UIHmheadboxPane.UIMinGatHjelpv64COPYRIPane;

            var innerText = "";
            var helpfileVersion = "Unknown";
            #endregion

            try
            {
                ClickHelpFileLink();
                Playback.Wait(1000);
                innerText = uIMinGatHjelpv64COPYRIPane.InnerText;

                //if (isSE)
                //    helpfileVersion = innerText.TrimEnd();
                //else
                //{
                    helpfileVersion = innerText.Remove(0, 13).Trim().Replace(" ", ".");
                //}
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Error reading helpfile version: " + e.Message);
            }

            //if (isSE)
            //{
            //    try
            //    {
            //        if (helpfileVersion != "MinGat Hjälp")
            //            Assert.Fail("MinGatHelpfile has wrong text: " + helpfileVersion);

            //        TestContext.WriteLine("Hjelpefiltekst Ok: " + helpfileVersion);
            //        TestContext.WriteLine("Hjelpefilversjon vises ikke i svensk MinGat(" + helpfileVersion + ")");
            //    }
            //    catch (Exception e)
            //    {
            //        errorList.Add("Feilet ved sjekk av hjelpefiltekst: " + e.Message);
            //    }
            //}
            //else
            //{
                try
                {
                    var minGatVersion = GetMinGatVersion().Trim();
                    Assert.AreEqual(minGatVersion, helpfileVersion, "Wrong MinGatHelpfile version");
                    TestContext.WriteLine("Hjelpefilversjon Ok: " + helpfileVersion);
                }
                catch (Exception e)
                {
                    errorList.Add("Feilet ved sjekk av hjelpefilversjon: " + e.Message);
                }
            //}

            return errorList;
        }

        public void CheckMinGatSweHelpfileMainMenuText()
        {
            #region Variable Declarations
            HtmlSpan uIMinGatHovedmenyPane = this.UIMinGatHjelpv64COPYRIWindow.UIMinGatHjelpv64COPYRIDocument.UIContentPageFrame.UIMinGatHjelpDocument.UIMinGatHovedmenyPane;
            #endregion

            // Verify that the 'InnerText' property of 'MinGat Hovedmeny' pane equals 'MinGat Hovedmeny'
            Assert.AreEqual("MinGat Huvudmeny", uIMinGatHovedmenyPane.InnerText, "Feil tekst i MinGat hovedmeny");
            TestContext.WriteLine("Språksjekk OK: " + uIMinGatHovedmenyPane.InnerText);
        }

        /// <summary>
        /// LoginMinGat - Use 'LoginMinGatParams' to pass parameters into this method.
        /// </summary>
        public void LoginMinGat()
        {
            UIMapVS2015.LoginMinGat();
        }

        public void ClickHelpFileLink()
        {
            #region Variable Declarations
            HtmlHyperlink uIMinGatHelpFilePageHyperlink = this.UIStartsidenMinGatv642Window.UIStartsidenMinGatv642Document.UIMinGatHelpFilePageHyperlink;
            #endregion

            Mouse.Click(uIMinGatHelpFilePageHyperlink);
        }

        public List<string> CheckGatWebServices()
        {
            string baseUrlWs1 = "http://localhost/GatWs1/";
            string baseUrlWs2 = "http://localhost/GatWs2/";
            var errorList = new List<string>();

            var ws1Browser = new BrowserWindow();
            var browser = BrowserWindow.Launch(baseUrlWs1);
            LogWs1Version();

            //External services
            try
            {
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/DepartmentService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckDepartmentService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(DepartmentService): " + e.Message);
                }

                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/DepartmentServiceV20191.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckDepartmentServiceV20191();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(DepartmentServiceV20191): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/EmployeeChangeTrackingService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckEmployeeChangeTrackingService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(EmployeeChangeTrackingService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/EmployeeServiceV63.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckEmployeeServiceV63();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(CheckEmployeeServiceV63): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/ExternalFlexService.svc"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckExternalFlexService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(ExternalFlexService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/GatGerica.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckGatGerica();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(GatGerica): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/ImportService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckImportService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(ImportService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/PatientInformationService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckPatientInformationService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(kPatientInformationService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/ReshRosterService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckReshRosterService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(ReshRosterService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/RoleDepartmentServiceV20182.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckRoleDepartmentServiceV20182();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(RoleDepartmentServiceV20182): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/SmsGatewayService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckSmsGatewayService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(SmsGatewayService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/TesService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.TesService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(TesService): " + e.Message);
                }

                browser.NavigateToUrl(new System.Uri(baseUrlWs1));

            }
            catch (Exception e)
            {
                errorList.Add("GatWS1: " + e.Message);
            }

            //Internal services
            try
            {
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/AppService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckAppService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(AppService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/BussinesAnalyzeService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckBussinesAnalyzeService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(BusinessAnalyzeService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/CalendarIntegrationWebService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckCalendarIntegrationWebService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(CalendarIntegrationWebService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/CalendarService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckCalendarService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(CalendarService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/ExportService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckExportService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(ExportService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/FlexExportService.svc"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckFlexExportService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(FlexExportService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/FlexService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckFlexService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(FlexService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/GatTaskSchedulerDataService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckGatTaskSchedulerDataService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(GatTaskSchedulerDataService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/PayslipImportService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckPayslipImportService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(PayslipImportService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/SickleaveMessageImportService.svc"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckSickleaveMessageImportService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(SickleaveMessageImportService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/SmsByMailReader.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckSmsByMailReader();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(SmsByMailReader): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/SmsService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckSmsService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(SmsService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/SystemInformationService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckSystemInformationService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(SystemInformationService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/TaskAgreementService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckTaskAgreementService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(TaskAgreementService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/TestService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckTestService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(TestService): " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs1/WeaBreakService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckWeaBreakService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS1(WeaBreakService): " + e.Message);
                }

                browser.NavigateToUrl(new System.Uri(baseUrlWs1));
            }
            catch (Exception e)
            {
                errorList.Add("GatWS1: " + e.Message);
            }

            try
            {
                Playback.Wait(1000);
                UIMapVS2017.CountServices();
            }
            catch (Exception e)
            {
                errorList.Add("GatWS1: (Antallet webservicer er endret) " + e.Message);
            }

            //WS 2
            browser.NavigateToUrl(new System.Uri(baseUrlWs2));
            LogWs2Version();
            Playback.Wait(1000);
           
            //External services
            try
            {
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs2/IdmService.asmx"));
                try
                {
                    UIMapVS2017.UIGatWebservices2MainPWindow.DrawHighlight();
                    UIMapVS2017.CheckIdmService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS2: " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs2/TimeregImport.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckTimeregImport();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS2: " + e.Message);
                }

                browser.NavigateToUrl(new System.Uri(baseUrlWs2));

            }
            catch (Exception e)
            {
                errorList.Add("GatWS2: " + e.Message);
            }

            //Internal services
            try
            {
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs2/CommunicationQueueService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckCommunicationQueueService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS2: " + e.Message);
                }
                browser.NavigateToUrl(new System.Uri("http://localhost/GatWs2/WishPlanWebService.asmx"));
                try
                {
                    Playback.Wait(1000);
                    UIMapVS2017.CheckWishPlanWebService();
                }
                catch (Exception e)
                {
                    errorList.Add("GatWS2: " + e.Message);
                }

                browser.NavigateToUrl(new System.Uri(baseUrlWs2));
            }
            catch (Exception e)
            {
                errorList.Add("GatWS2: " + e.Message);
            }

            try
            {
                Playback.Wait(1000);
                UIMapVS2017.CountServicesWs2();
            }
            catch(Exception e)
            {
                errorList.Add("GatWS2: (Antallet webservicer er endret): " + e.Message);
            }

            browser.Close();

            return errorList;
        }
        
        /// <summary>
        /// ClickRoleDepartmentServiceV65
        /// </summary>
        private void ClickRoleDepartmentServiceV65New()
        {
            #region Variable Declarations
            HtmlHyperlink uIRoleDepartmentServicHyperlink = this.UIGatWebserviceMainPagWindow.UIGatWebserviceMainPagDocument2.UIRoleDepartmentServicHyperlink;
            #endregion

            // Click 'RoleDepartmentServiceV65' link
            Mouse.Click(uIRoleDepartmentServicHyperlink);
        }

        /// <summary>
        /// CheckWs1Version - Use 'CheckWs1VersionExpectedValues' to pass parameters into this method.
        /// </summary>
        public void LogWs1Version()
        {
            #region Variable Declarations
            HtmlSpan uIGatWebservicesv64238Pane = this.UIGatWebserviceMainPagWindow.UIGatWebserviceMainPagDocument.UIGatWebservicesv64238Pane;
            #endregion

            try
            {
                uIGatWebservicesv64238Pane.DrawHighlight();
                var fullWs1Version = uIGatWebservicesv64238Pane.InnerText;
                TestContext.WriteLine(fullWs1Version);
            }
            catch (Exception)
            {
                TestContext.WriteLine("Unable to get ws1 version");
            }

            // Verify that the 'InnerText' property of 'Gat Webservices v. 6.4.2.38306' pane equals 'Gat Webservices v. 6.4.2.38306'
            //Assert.AreEqual(this.CheckWs1VersionExpectedValues.UIGatWebservicesv64238PaneInnerText, uIGatWebservicesv64238Pane.InnerText, "Feil i ws1 versjon");
        }

        /// <summary>
        /// CheckWs2Version - Use 'CheckWs2VersionExpectedValues' to pass parameters into this method.
        /// </summary>
        public void LogWs2Version()
        {
            #region Variable Declarations
            HtmlSpan uIGatWebservices2v6423Pane = this.UIGatWebservices2MainPWindow.UIGatWebservices2MainPDocument.UIGatWebservices2v6423Pane;
            #endregion

            try
            {
                uIGatWebservices2v6423Pane.DrawHighlight();
                var fullWs2Version = uIGatWebservices2v6423Pane.InnerText;
                TestContext.WriteLine(fullWs2Version);
            }
            catch (Exception)
            {
                TestContext.WriteLine("Unable to get ws2 version");
            }

            // Verify that the 'InnerText' property of 'Gat Webservices2 v.6.4.2.38306' pane equals 'Gat Webservices2 v.6.4.2.38306'
            //Assert.AreEqual(this.CheckWs2VersionExpectedValues.UIGatWebservices2v6423PaneInnerText, uIGatWebservices2v6423Pane.InnerText, "Feil Ws2 versjon");
        }

        public static void DeleteDirectory(string path)
        {
            DeleteDirectory(path, true);
        }

        private static void DeleteDirectory(string path, bool recursive)
        {
            Playback.Wait(3000);
            // Delete all files and sub-folders?
            if (recursive)
            {
                // Yep... Let's do this
                var subfolders = Directory.GetDirectories(path);
                foreach (var s in subfolders)
                {
                    DeleteDirectory(s, recursive);
                }
            }

            // Get all files of the folder
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                // Get the attributes of the file
                var attr = File.GetAttributes(f);

                // Is this file marked as 'read-only'?
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Yes... Remove the 'read-only' attribute, then
                    File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                }

                // Delete the file
                File.Delete(f);
            }

            // When we get here, all the files of the folder were
            // already deleted, so we just delete the empty folder
            Directory.Delete(path);
        }

        public void SelectWeekView()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIUkeRibbonBaseButtonItem = this.UIGatver64138140ASCLAvWindow1.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpViewModeRibbonPageGroup.UIUkeRibbonBaseButtonItem;
            #endregion

            // Click 'Uke' RibbonBaseButtonItem
            Mouse.Click(uIUkeRibbonBaseButtonItem, new Point(37, 9));
        }

        public void GoToShiftDate(DateTime date)
        {
            GoToShiftDateNew(date);
        }

        private void GoToShiftDateNew(DateTime date)
        {
            UICommon.GoToShiftbookdate(date);
        }

        /// <summary>
        /// CheckShiftbookViewTypes
        /// </summary>
        public void CheckShiftbookViewTypes()
        {
            #region Variable Declarations
            var controlRibbon = this.UIGatver64138140ASCLAvWindow1.UIRibbonControlRibbon;
            var clickPoint = new Point(controlRibbon.Left + 100, controlRibbon.Top + 5);
            DXRibbonButtonItem uIGanttdagRibbonBaseButtonItem = controlRibbon.UIRpMainMenuRibbonPage.UIGrpViewModeRibbonPageGroup.UIGanttdagRibbonBaseButtonItem;
            DXRibbonButtonItem uIGanttukeRibbonBaseButtonItem = controlRibbon.UIRpMainMenuRibbonPage.UIGrpViewModeRibbonPageGroup.UIGanttukeRibbonBaseButtonItem;
            DXRibbonButtonItem uIUkeRibbonBaseButtonItem = controlRibbon.UIRpMainMenuRibbonPage.UIGrpViewModeRibbonPageGroup.UIUkeRibbonBaseButtonItem;
            DXRibbonButtonItem uIPeriodeRibbonBaseButtonItem = controlRibbon.UIRpMainMenuRibbonPage.UIGrpViewModeRibbonPageGroup.UIPeriodeRibbonBaseButtonItem;
            //DXButton uIVelgPeriodeButton = this.UIItemWindow2.UIPopupContainerBarConMenu.UIPpcPeriodSelectorClient.UIPnlPeriodSelectorClient.UIVelgPeriodeButton;
            DXRibbonButtonItem uIKalenderRibbonBaseButtonItem = controlRibbon.UIRpMainMenuRibbonPage.UIGrpViewModeRibbonPageGroup.UIKalenderRibbonBaseButtonItem;
            DXRibbonButtonItem uIOppgaveRibbonBaseButtonItem = controlRibbon.UIRpMainMenuRibbonPage.UIGrpViewModeRibbonPageGroup.UIOppgaveRibbonBaseButtonItem;
            DXRibbonButtonItem uIDagRibbonBaseButtonItem = controlRibbon.UIRpMainMenuRibbonPage.UIGrpViewModeRibbonPageGroup.UIDagRibbonBaseButtonItem;
            #endregion
            
            // Click 'Gantt dag' RibbonBaseButtonItem
            Mouse.Click(uIGanttdagRibbonBaseButtonItem, new Point(55, 8));

            // Click 'Gantt uke' RibbonBaseButtonItem
            Mouse.Click(uIGanttukeRibbonBaseButtonItem, new Point(44, 10));

            // Click 'Uke' RibbonBaseButtonItem
            Mouse.Click(uIUkeRibbonBaseButtonItem, new Point(37, 9));

            // Click 'Periode' RibbonBaseButtonItem
            Mouse.Click(uIPeriodeRibbonBaseButtonItem, new Point(36, 9));

            // Click 'Velg Periode' button
            try
            {                
                Mouse.Move(clickPoint);
                UIMapVS2015.ClickChoosePeriodViewButton();
            }
            catch (Exception e)
            {
                Mouse.Click(clickPoint);
                TestContext.WriteLine("Error in CheckShiftbookViewTypes: " + e.Message);
            }
                        
            // Click 'Kalender' RibbonBaseButtonItem
            Mouse.Click(uIKalenderRibbonBaseButtonItem, new Point(43, 11));

            // Click 'Oppgave' RibbonBaseButtonItem
            Mouse.Click(uIOppgaveRibbonBaseButtonItem, new Point(50, 11));

            // Click 'Dag' RibbonBaseButtonItem
            Mouse.Click(uIDagRibbonBaseButtonItem, new Point(29, 8));
        }

        /// <summary>
        /// GenerateExtraSonnySat14052016 - Use 'GenerateExtraSonnySat14052016Params' to pass parameters into this method.
        /// </summary>
        public void GenerateExtraSonnySat14052016()
        {
            #region Variable Declarations
            DXListBox uIPeriodScheduleListBoList = this.UIGatver64138140ASCLAvWindow1.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList;
            DXRibbonButtonItem uIEkstraRibbonBaseButtonItem = this.UIGatver64138140ASCLAvWindow1.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIEkstraRibbonBaseButtonItem;
            DXLookUpEdit uICbOvertimeCodeLookUpEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UINbMenuNavBar.UINavBarGroupControlCoScrollableControl.UICbOvertimeCodeLookUpEdit;
            DXTextEdit uITeFindEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UINbMenuNavBar.UINavBarGroupControlCoScrollableControl.UICbOvertimeCodeLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            DXGrid uIGridControlTable = this.UIMerarbeidovertidWindow.UIPanClientClient.UINbMenuNavBar.UINavBarGroupControlCoScrollableControl.UICbOvertimeCodeLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILciGridLayoutControlItem.UIGridControlTable;
            DXPopupEdit uIPceDatePopupEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UIPceDatePopupEdit;
            DXLookUpEdit uICbShiftCodeLookUpEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UICbShiftCodeLookUpEdit;
            DXWindow uIPopupLookUpEditFormWindow = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UICbShiftCodeLookUpEdit.UIPopupLookUpEditFormWindow;
            DXButton uIOKButton = this.UIMerarbeidovertidWindow.UIGsPanelControl1Client.UIOKButton;
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow2.UIPopupMenuBarControlMenu.UIBiOkMenuBaseButtonItem;
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow11.UIGSLookUpEditLookUpEdit;
            DXTextEdit uIECommentEdit = this.UIItemWindow11.UIECommentEdit;
            DXButton uIOKButton1 = this.UIItemWindow11.UIOKButton;
            #endregion

            // Select '0' in 'PeriodScheduleListBoxControl`1' list box
            //SelectedIndicesAsString
            uIPeriodScheduleListBoList.SelectedIndicesAsString = this.GenerateExtraSonnySat14052016Params.UIPeriodScheduleListBoListSelectedIndicesAsString;

            // Click 'Ekstra' RibbonBaseButtonItem
            Playback.Wait(2000);
            Mouse.Click(uIEkstraRibbonBaseButtonItem);

            Playback.Wait(3000);
            UIMerarbeidovertidWindow.WaitForControlReady(30000);
            UIMerarbeidovertidWindow.DrawHighlight();
            uICbOvertimeCodeLookUpEdit.WaitForControlReady(30000);
            uICbOvertimeCodeLookUpEdit.DrawHighlight();
            Keyboard.SendKeys(uICbOvertimeCodeLookUpEdit, this.GenerateExtraSonnySat14052016Params.UITeSelectCause, ModifierKeys.None);

            UICommon.SetExtraDate(this.GenerateExtraSonnySat14052016Params.UIPceDate14052016);
            
            Keyboard.SendKeys(uICbShiftCodeLookUpEdit, this.GenerateExtraSonnySat14052016Params.UITeSelectShiftcodeD1, ModifierKeys.None);

            // Click '&OK' button
            Mouse.Click(uIOKButton, new Point(1, 1));

            // Click 'biOk' MenuBaseButtonItem
            Mouse.Click(uIBiOkMenuBaseButtonItem, new Point(43, 12));

            // Type 'System.String' in 'GSLookUpEdit' LookUpEdit
            //ValueTypeName
            uIGSLookUpEditLookUpEdit.ValueTypeName = this.GenerateExtraSonnySat14052016Params.UIGSLookUpEditLookUpEditValueTypeName;

            // Type 'SYKDOM' in 'GSLookUpEdit' LookUpEdit
            //ValueAsString
            uIGSLookUpEditLookUpEdit.ValueAsString = this.GenerateExtraSonnySat14052016Params.UIGSLookUpEditLookUpEditValueAsString;

            // Type 'Test 99' in 'eComment' text box
            //ValueAsString
            uIECommentEdit.ValueAsString = this.GenerateExtraSonnySat14052016Params.UIECommentEditValueAsString;
            Keyboard.SendKeys(uIECommentEdit, this.GenerateExtraSonnySat14052016Params.UITab);

            // Click 'Ok' button
            Mouse.Click(uIOKButton1, new Point(1, 1));
        }

        /// <summary>
        /// ConstructAbsenceJohnson09052016 - Use 'ConstructAbsenceJohnson09052016Params' to pass parameters into this method.
        /// </summary>
        public void GenerateAbsenceJohnson09052016()
        {
            #region Variable Declarations
            //DXListBox uIPeriodScheduleListBoList = this.UIGatver64138140ASCLAvWindow1.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList;
            DXRibbonButtonItem uIFraværRibbonBaseButtonItem = this.UIGatver64138140ASCLAvWindow1.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIFraværRibbonBaseButtonItem;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit = this.UIItemWindow3.UIGSNavBarControlNavBar.UINavBarGroupControlCoScrollableControl.UIGSSearchLookUpEditLookUpEdit;
            #endregion


            // Click 'Fravær' RibbonBaseButtonItem
            Playback.Wait(500);
            Mouse.Click(uIFraværRibbonBaseButtonItem);

            Playback.Wait(1500);
            uIGSSearchLookUpEditLookUpEdit.DrawHighlight();
            Keyboard.SendKeys(uIGSSearchLookUpEditLookUpEdit, "{Down}{Enter}");

            UICommon.UIMapVS2017.ClickOkConstuctAbsence();
        }


        /// <summary>
        /// IterateMainTabs
        /// </summary>
        public List<string> IterateMainTabs()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            WinClient uIGatver64238230ASCLAvClient = this.UIGatver64238230ASCLAvWindow.UIItemWindow.UIGatver64238230ASCLAvClient;
            WinButton uIOKButton = this.UIEFO3000GatWindow.UIItemWindow.UIEFO3000GatClient.UIOKButton;
            #endregion

            // Click Ekstrainfo
            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
                CheckEkstrainfoTab();
                Playback.Wait(2000);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.EFO);
                // Click '&OK' button
                Playback.Wait(500);
                Mouse.Click(uIOKButton, new Point(37, 8));
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.LIS);
                //"Avanserte instillinger";
                CheckLISTab();
                Playback.Wait(2000);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
                CheckArbeidsplanTab();
                Playback.Wait(2000);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Grovplan);
                CheckGrovplanTab();
                Playback.Wait(2000);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Produksjonsplan);
                CheckProduksjonsplanTab();
                Playback.Wait(2000);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
                CheckAvdelingTab();
                Playback.Wait(2000);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.ReportCurrent);
                CheckRapportsenterTab();
                Playback.Wait(2000);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Department);
                Playback.Wait(2000);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Budget);
                CheckBudsjetTab();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            Playback.Wait(2000);

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Administration);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            Playback.Wait(3000);

            return errorList;
        }

        /// <summary>
        /// ExchangeJohnson
        /// </summary>
        public void ExchangeJohnsonNew()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIBytteRibbonBaseButtonItem = this.UIGatver65041007ASCLAvWindow.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIBytteRibbonBaseButtonItem;
            DXButton uICloseButton = this.UISammenligningavkompeWindow.UICloseButton;
            #endregion

            // Click 'Bytte' RibbonBaseButtonItem
            Mouse.Click(uIBytteRibbonBaseButtonItem);

            // Click 'Close' button
            Playback.Wait(1000);
            if (UISammenligningavkompeWindow.Exists)
                Mouse.Click(uICloseButton);
        }

        /// <summary>
        /// ExchangeJohnson_1 - Use 'ExchangeJohnson_1Params' to pass parameters into this method.
        /// </summary>
        public void ExchangeJohnsonDetails()
        {
            #region Variable Declarations
            DXLookUpEdit uISleEmployment2LookUpEdit = this.UIItemWindow4.UIGsPanelControl1Client.UIGrpEmployee2Client.UISleEmployment2LookUpEdit;
            DXTextEdit uITeFindEdit = this.UIItemWindow4.UIGsPanelControl1Client.UIGrpEmployee2Client.UISleEmployment2LookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            DXCell uITirsdagCell = this.UIItemWindow4.UIGsPanelControl1Client.UIGsTabTabList.UITpShiftExchangeClient.UIGrpEmployee1ShiftsClient.UIGcEmployment1ShiftsTable.UITirsdagCell;
            DXButton uIBtnGiveEmployee2Button = this.UIItemWindow4.UIGsPanelControl1Client.UIGsTabTabList.UITpShiftExchangeClient.UIBtnGiveEmployee2Button;
            DXCell uITirsdagCell1 = this.UIItemWindow4.UIGsPanelControl1Client.UIGsTabTabList.UITpShiftExchangeClient.UIGrpEmployee2ShiftsClient.UIGcEmployment2ShiftsTable.UITirsdagCell;
            DXButton uIBtnGiveEmployee1Button = this.UIItemWindow4.UIGsPanelControl1Client.UIGsTabTabList.UITpShiftExchangeClient.UIBtnGiveEmployee1Button;
            DXButton uIOKButton1 = this.UIItemWindow4.UIOKButton1;
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow2.UIPopupMenuBarControlMenu1.UIBiOkMenuBaseButtonItem;
            #endregion

            Mouse.Click(uISleEmployment2LookUpEdit);

            // Type 'crockett{Enter}' in 'teFind' text box
            Keyboard.SendKeys(uITeFindEdit, this.ExchangeJohnson_1Params.UITeFindEditSendKeys, ModifierKeys.None);

            // Click 'tirsdag' cell
            Mouse.Click(uITirsdagCell, new Point(16, 11));

            // Click 'btnGiveEmployee2' button
            Mouse.Click(uIBtnGiveEmployee2Button, new Point(1, 1));

            // Click 'tirsdag' cell
            Mouse.Click(uITirsdagCell1, new Point(25, 7));

            // Click 'btnGiveEmployee1' button
            Mouse.Click(uIBtnGiveEmployee1Button, new Point(1, 1));

            // Click '&OK' button
            Mouse.Click(uIOKButton1, new Point(1, 1));

            // Click 'biOk' MenuBaseButtonItem
            Mouse.Click(uIBiOkMenuBaseButtonItem, new Point(46, 9));
        }

        public void GenerateAbsenceWithDocTubbs12052016()
        {
            #region Variable Declarations
            //DXListBox uIPeriodScheduleListBoList = this.UIGatver64138140ASCLAvWindow1.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList;
            DXRibbonButtonItem uIFraværRibbonBaseButtonItem = this.UIGatver64138140ASCLAvWindow1.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIFraværRibbonBaseButtonItem;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit = this.UIItemWindow3.UIGSNavBarControlNavBar.UINavBarGroupControlCoScrollableControl.UIGSSearchLookUpEditLookUpEdit;
            #endregion

            // Click 'Fravær' RibbonBaseButtonItem
            Playback.Wait(500);
            Mouse.Click(uIFraværRibbonBaseButtonItem);

            Playback.Wait(1000);
            Mouse.Click(uIGSSearchLookUpEditLookUpEdit);
            Playback.Wait(1000);
            Keyboard.SendKeys("12{Enter}");
            Mouse.Move(new Point(Mouse.Location.X, Mouse.Location.Y - 30));
            Playback.Wait(500);

            SelectAbsenceDoc();        
            Playback.Wait(1000);

            UICommon.UIMapVS2017.ClickOkConstuctAbsence();
        }

        ///// <summary>
        ///// AddAbsenceDate - Use 'AddAbsenceDateParams' to pass parameters into this method.
        ///// </summary>
        //public void AddAbsenceDate()
        //{
        //    #region Variable Declarations
        //    DXPopupEdit uIPceDatePopupEdit = this.UIItemWindow3.UILcMainCustom.UIRootLayoutGroup.UILciMainInformationLayoutControlItem.UIGrcInformationClient.UINbcInformationNavBar.UINbgcDocumentsScrollableControl.UITcDocumentationTabList.UITabDocumentationClient.UIPnlDocumentationClient.UILcDocumentationCustom.UIPceDatePopupEdit;
        //    #endregion

        //    // Type '12.05.2016 [SelectionStart]0' in 'pceDate' PopupEdit
        //    //ValueAsString
        //    uIPceDatePopupEdit.ValueAsString = this.AddAbsenceDateParams.UIPceDatePopupEditValueAsString;
        //    Keyboard.SendKeys(uIPceDatePopupEdit, "{TAB}");
        //}

        /// <summary>
        /// SelectAbsenceDoc - Use 'SelectAbsenceDocParams' to pass parameters into this method.
        /// </summary>
        public void SelectAbsenceDoc()
        {
            UICommon.UIMapVS2017.CheckDocumentationAbsence();
            UICommon.UIMapVS2017.SelectDocumentAbsence();
        }

        /// <summary>
        /// SelectDonJohnsonMon09052016 - Use 'SelectDonJohnsonMon09052016Params' to pass parameters into this method.
        /// </summary>
        public void SelectDonJohnsonMon09052016()
        {
            #region Variable Declarations
            DXListBox uIPeriodScheduleListBoList = this.UIGatver64138140ASCLAvWindow1.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList;
            #endregion
        }

        /// <summary>
        /// SelectDocMalFromAdministration - Use 'SelectDocMalFromAdministrationParams' to pass parameters into this method.
        /// </summary>
        public void SelectDocMalFromAdministration()
        {

            #region Variable Declarations
            //WinClient uIAdministrasjonClient = this.UIGatver64238230ASCLAvWindow1.UIItemWindow.UIAdministrasjonClient;
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow5.UIGSLookUpEditLookUpEdit;
            DXButton uIGSSimpleButtonButton = this.UIItemWindow5.UIGSGroupControlClient.UIGSSimpleButtonButton;
            #endregion

            SearchAndSelectFromAdministration("DOKUMENTMALER TIL +FRAVÆRSKODER", false);

            //ValueTypeName
            Mouse.Click(uIGSLookUpEditLookUpEdit);
            Keyboard.SendKeys("{DOWN 6}{ENTER}");

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
            //ClearAdministrationSearchString();
        }

        public void ClickEmpTab()
        {
            #region Variable Declarations
            WinClient uIGatver64238230ASCLAvClient = this.UIGatver64238230ASCLAvWindow.UIItemWindow.UIGatver64238230ASCLAvClient;
            #endregion

            Playback.Wait(1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
        }


        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        ///// <summary>
        ///// OpenAbsenceDocFromEmpTab
        ///// </summary>
        //private void SelectDocTabEmployee_SE()
        //{
        //    //Hack: DelphiTabkomponent
        //    UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.DocTab, false, true);
        //}

        public void SelectDocTabEmployee()
        {
            //Hack: DelphiTabkomponent
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.DocTab, false);
        }

        /// <summary>
        /// OpenAbsenceDocFromEmpTab
        /// </summary>
        public void OpenAbsenceDocFromEmpTab()
        {
            UICommon.OpenAbsenceDocFromEmpTab();
        }

        public void EditAbsenceDocFromEmpTabNew()
        {
            UICommon.EditAbsenceDocFromEmpTab();
        }

        /// <summary>
        /// EditAbsenceDoc - Use 'EditAbsenceDocParams' to pass parameters into this method.
        /// </summary>
        public void EditAbsenceDoc(string docText, bool minimize)
        {
            #region Variable Declarations
            var docWin = UIDokumentWindow;
            docWin.SearchProperties.Remove(WinControl.PropertyNames.Name);
            docWin.SearchProperties.Contains(@"EGENMELDING VED SYKEFRAVÆR");
            docWin.SearchProperties.Add(WinWindow.PropertyNames.Name, @"Dokument: ", PropertyExpressionOperator.Contains);
            docWin.SearchProperties[WinWindow.PropertyNames.Name] = @"Dokument: Gat dokument Egenmelding.doc";
            docWin.WindowTitles.Add(@"Dokument: Gat dokument Egenmelding.doc");

            var docControl = docWin.UITextControl1Window;
            docControl.SearchProperties.Contains(@"Dokument: ");
            docControl.WindowTitles.Add(@"Dokument: Gat dokument Egenmelding.doc");

            var docClient = docControl.UIEGENMELDINGVEDSYKEFRClient;
            docClient.SearchProperties.Remove(WinControl.PropertyNames.Name);
            docClient.SearchProperties.Contains(@"EGENMELDING VED SYKEFRAVÆR");
            docWin.SearchProperties.Add(WinWindow.PropertyNames.Name, @"EGENMELDING VED SYKEFRAVÆR", PropertyExpressionOperator.Contains);
            docClient.WindowTitles.Add(@"Dokument: Gat dokument Egenmelding.doc");

            var titleBar = docWin.UIDokumentTitleBar;
            titleBar.SearchProperties.Contains(@"Dokument: ");
            titleBar.WindowTitles.Add(@"Dokument: Gat dokument Egenmelding.doc");

            var uICloseButton = titleBar.UICloseButton;
            uICloseButton.WindowTitles.Add(@"Dokument: Gat dokument Egenmelding.doc");

            var uIYesButton = this.UIQuestionWindow.UIYesWindow.UIYesButton;
            #endregion

            if (minimize)
                UIGatver64138140ASCLAvWindow1.Minimized = minimize;
            docWin.WaitForControlExist(25000);

            try
            {
                docWin.WaitForControlReady();
                Mouse.Click(docClient, new Point(988, 332));
                Keyboard.SendKeys(docClient, docText);
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Edit document error: " + e.Message);
            }

            try
            {
                // Click 'Close' button
                Mouse.Click(uICloseButton, new Point(22, 5));
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{F4}", ModifierKeys.Alt);
            }

            // Click '&Yes' button
            try
            {
                if (uIYesButton.Exists)
                    Mouse.Click(uIYesButton, new Point(44, 12));
            }
            catch (Exception e)
            {
                Keyboard.SendKeys("y", ModifierKeys.Control);
                TestContext.WriteLine("Save error: " + e.Message);
            }

            Playback.Wait(1000);
        }


        /// <summary>
        /// CopyAbsenceDocText
        /// </summary>
        public void CopyAbsenceDocText()
        {
            #region Variable Declarations
            WinClient uIEGENMELDINGVEDSYKEFRClient = this.UIDokumentGatdokumentEWindow.UITextControl1Window.UIEGENMELDINGVEDSYKEFRClient;

            uIEGENMELDINGVEDSYKEFRClient.SearchProperties.Remove(WinControl.PropertyNames.Name);
            uIEGENMELDINGVEDSYKEFRClient.SearchProperties.Contains(@"EGENMELDING VED SYKEFRAVÆR");
            uIEGENMELDINGVEDSYKEFRClient.SearchProperties.Add(WinWindow.PropertyNames.Name, @"EGENMELDING VED SYKEFRAVÆR", PropertyExpressionOperator.Contains);
            uIEGENMELDINGVEDSYKEFRClient.WindowTitles.Add(@"Dokument: ");

            WinMenuItem uICopyMenuItem = this.UIItemWindow6.UIDropDownMenu.UICopyMenuItem;
            #endregion
            try
            {
                UIDokumentGatdokumentEWindow.WaitForControlExist(25000);

                uIEGENMELDINGVEDSYKEFRClient.WaitForControlReady();
                uIEGENMELDINGVEDSYKEFRClient.DrawHighlight();
                // Move 'EGENMELDING VED SYKEFRAVÆRUndertegnedes navn:Fødse...' client
                Mouse.Click(uIEGENMELDINGVEDSYKEFRClient, new Point(914, 339));
                Mouse.StartDragging(uIEGENMELDINGVEDSYKEFRClient, new Point(914, 339));
                Mouse.StopDragging(uIEGENMELDINGVEDSYKEFRClient, 130, 12);

                // Right-Click 'EGENMELDING VED SYKEFRAVÆRUndertegnedes navn:Fødse...' client
                Mouse.Click(uIEGENMELDINGVEDSYKEFRClient, MouseButtons.Right, ModifierKeys.None, new Point(1036, 354));

                // Click 'Copy' menu item
                Mouse.Click(uICopyMenuItem, new Point(86, 10));

            }
            catch (Exception e)
            {
                TestContext.WriteLine("Copy error: " + e.Message);
            }

        }

        /// <summary>
        /// CloseDocWindowFromFileMenu
        /// </summary>
        public void CloseDocWindowFromFileMenu()
        {
            #region Variable Declarations
            WinMenuItem uIAvsluttMenuItem = this.UIDokumentWindow.UIMenuStrip1MenuBar.UIFilMenuItem.UIAvsluttMenuItem;
            #endregion

            // Click 'Fil' -> 'Avslutt' menu item
            Mouse.Click(uIAvsluttMenuItem, new Point(80, 12));
        }

        public void CloseAbsenceDoc()
        {
            #region Variable Declarations
            WinButton uICloseButton = this.UIDokumentWindow.UIDokumentTitleBar.UICloseButton;
            #endregion

            // Click 'Close' button
            Mouse.Click(uICloseButton, new Point(22, 5));
            Playback.Wait(1000);
        }

        public void Clear_Clipboard()
        {
            Clipboard.Clear();
        }

        public List<string> Check_AbsenceDocEditText()
        {
            var errorList = new List<string>();
            if (Clipboard.ContainsText())
            {
                var compareText = "Endre fra Anstatt | Dok tab\r\nDirekte etter reg. av fravær";
                var text = Clipboard.GetText(TextDataFormat.Text);

                try
                {
                    //if(!Convert.ToBoolean(String.Compare(text, compareText)))
                    Assert.AreEqual(compareText, text);
                    TestContext.WriteLine("Endring/Lagring av fraværsdokument Ok!");
                }
                catch (Exception e)
                {
                    errorList.Add("Feilet ved sjekk av fraværsdokument: " + e.Message);
                }
            }

            return errorList;
        }


        /// <summary>
        /// CheckFirstItemWeekViewMon09052016 - Use 'CheckFirstItemWeekViewMon09052016ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckLineWeekViewMon09052016(string lineNo)
        {
            #region Variable Declarations
            var shift = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList.UID108001600ListItem;
            #endregion

            shift.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[0]Item[" + lineNo + "]";
            Mouse.Click(shift);

            // Verify that the 'Text' property of 'D1 - (08.00 - 16.00)' list item equals 'D1 - (08.00 - 16.00)'
            //Assert.AreEqual(this.CheckFirstItemWeekViewMon09052016ExpectedValues.UID108001600ListItemText, uID108001600ListItem.Text);
        }

        public void CheckJohnsonTuesday()
        {
            #region Variable Declarations
            DXListBoxItem johnsonTuesday = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList1.UID08001600ListItem;
            #endregion
            //johnsonTuesday.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[1]Item[1]";
            //thomasWednesday.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[2]Item[2]";

            Mouse.Click(johnsonTuesday);
        }

        public void CheckThomasWednesday()
        {
            #region Variable Declarations
            DXListBoxItem thomasWednesday = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList2.UID08001600ListItem;
            #endregion
            //johnsonTuesday.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[1]Item[1]";
            //thomasWednesday.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[2]Item[2]";

            Mouse.Click(thomasWednesday);
        }

        public void CheckTubbsthursday()
        {
            #region Variable Declarations
            DXListBoxItem tubbsthursday = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList3.UID108001600ListItem;
            #endregion
            //johnsonTuesday.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[1]Item[1]";
            //thomasWednesday.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[2]Item[2]";

            Mouse.Click(tubbsthursday);
        }

        public void CheckJohnsonFriday()
        {
            #region Variable Declarations
            DXListBoxItem johnsonFriday = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList7.UID08001600ListItem;
            #endregion
            //johnsonTuesday.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[1]Item[1]";
            //thomasWednesday.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[2]Item[2]";

            Mouse.Click(johnsonFriday);
        }

        /// <summary>
        /// WeekdayCheck - Use 'WeekdayCheckExpectedValues' to pass parameters into this method.
        /// </summary>
        public void WeekdayCheck()
        {
            #region Variable Declarations
            DXListBoxItem johnsonTuesday = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList1.UID08001600ListItem;
            DXListBoxItem thomasWednesday = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList2.UID08001600ListItem;
            DXListBoxItem tubbsthursday = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList3.UID108001600ListItem;
            DXListBoxItem davidsenThursdayFree = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList6.UIItem00000000ListItem;
            DXListBoxItem johnsonFriday = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList7.UID08001600ListItem;
            DXListBoxItem paulsenSaturdayFree = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList8.UIItem00000000ListItem;
            DXListBoxItem crockettSaturday = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList5.UID08001600ListItem;
            #endregion
            //johnsonTuesday.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[1]Item[1]";
            //thomasWednesday.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[2]Item[2]";

            johnsonTuesday.DrawHighlight();
            thomasWednesday.DrawHighlight();
            tubbsthursday.DrawHighlight();
            davidsenThursdayFree.DrawHighlight();
            johnsonFriday.DrawHighlight();
            paulsenSaturdayFree.DrawHighlight();
            crockettSaturday.DrawHighlight();


            //// Verify that the 'Text' property of 'D - (08.00 - 16.00)' list item equals 'D - (08.00 - 16.00)'
            //Assert.AreEqual(this.WeekdayCheckExpectedValues.UID08001600ListItemText, johnsonTuesday.Text, "Johnson tuesday");

            //// Verify that the 'Text' property of 'D - (08.00 - 16.00)' list item equals 'D - (08.00 - 16.00)'
            //Assert.AreEqual(this.WeekdayCheckExpectedValues.UID08001600ListItemText1, uID08001600ListItem1.Text, "Thomas Wednesday");

            //// Verify that the 'Text' property of 'D1 - (08.00 - 16.00)' list item equals 'D1 - (08.00 - 16.00)'
            //Assert.AreEqual(this.WeekdayCheckExpectedValues.UID108001600ListItemText, uID108001600ListItem.Text, "Tubbs thursday");

            //// Verify that the 'Text' property of '00.00 - 00.00' list item equals '00.00 - 00.00'
            //Assert.AreEqual(this.WeekdayCheckExpectedValues.UIItem00000000ListItemText, uIItem00000000ListItem.Text, "Davidsen thursday free");

            //// Verify that the 'Text' property of 'D - (08.00 - 16.00)' list item equals 'D - (08.00 - 16.00)'
            //Assert.AreEqual(this.WeekdayCheckExpectedValues.UID08001600ListItemText2, uID08001600ListItem2.Text, "Johnson friday");

            //// Verify that the 'Text' property of '00.00 - 00.00' list item equals '00.00 - 00.00'
            //Assert.AreEqual(this.WeekdayCheckExpectedValues.UIItem00000000ListItemText1, uIItem00000000ListItem1.Text, "Paulsen saturday free");

            //// Verify that the 'Text' property of 'D - (08.00 - 16.00)' list item equals 'D - (08.00 - 16.00)'
            //Assert.AreEqual(this.WeekdayCheckExpectedValues.UID08001600ListItemText3, uID08001600ListItem3.Text, "Crockett saturday");
        }

        /// <summary>
        /// Tuesday - Use 'TuesdayParams' to pass parameters into this method.
        /// </summary>
        public void Tuesday()
        {
            #region Variable Declarations
            DXListBox uIPeriodScheduleListBoList1 = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList1;
            #endregion

            // Select '0' in 'PeriodScheduleListBoxControl`1' list box
            //SelectedIndicesAsString
            uIPeriodScheduleListBoList1.SelectedIndicesAsString = this.TuesdayParams.UIPeriodScheduleListBoList1SelectedIndicesAsString;
        }

        /// <summary>
        /// Wednesday - Use 'WednesdayParams' to pass parameters into this method.
        /// </summary>
        public void Wednesday()
        {
            #region Variable Declarations
            DXListBox uIPeriodScheduleListBoList2 = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList2;
            #endregion

            // Select '1' in 'PeriodScheduleListBoxControl`1' list box
            //SelectedIndicesAsString
            uIPeriodScheduleListBoList2.SelectedIndicesAsString = this.WednesdayParams.UIPeriodScheduleListBoList2SelectedIndicesAsString;
        }

        /// <summary>
        /// Thursday - Use 'ThursdayParams' to pass parameters into this method.
        /// </summary>
        public void Thursday()
        {
            #region Variable Declarations
            DXListBox uIPeriodScheduleListBoList3 = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList3;
            #endregion

            // Select '2' in 'PeriodScheduleListBoxControl`1' list box
            //SelectedIndicesAsString
            uIPeriodScheduleListBoList3.SelectedIndicesAsString = this.ThursdayParams.UIPeriodScheduleListBoList3SelectedIndicesAsString;
        }
        /// <summary>
        /// FridayFree - Use 'FridayFreeParams' to pass parameters into this method.
        /// </summary>
        public void FridayFree()
        {
            #region Variable Declarations
            DXListBox uIPeriodScheduleListBoList4 = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList4;
            #endregion

            // Select '0' in 'PeriodScheduleListBoxControl`1' list box
            //SelectedIndicesAsString
            uIPeriodScheduleListBoList4.SelectedIndicesAsString = this.FridayFreeParams.UIPeriodScheduleListBoList4SelectedIndicesAsString;
        }
        /// <summary>
        /// Saturday - Use 'SaturdayParams' to pass parameters into this method.
        /// </summary>
        public void Saturday()
        {
            #region Variable Declarations
            DXListBox uIPeriodScheduleListBoList5 = this.UIGatver64238230ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList5;
            #endregion

            // Select '0' in 'PeriodScheduleListBoxControl`1' list box
            //SelectedIndicesAsString
            uIPeriodScheduleListBoList5.SelectedIndicesAsString = this.SaturdayParams.UIPeriodScheduleListBoList5SelectedIndicesAsString;
        }
        public void ClearAdministrationSearchString()
        { //UICommon.ClearAdministrationSearchString();
        }
       
        public virtual ExtractFilesAndStartGatParams CommonPathsAndParams
        {
            get
            {
                if ((this.mExtractFilesAndStartGatParams == null))
                {
                    this.mExtractFilesAndStartGatParams = new ExtractFilesAndStartGatParams();
                }
                return this.mExtractFilesAndStartGatParams;
            }
        }

        private ExtractFilesAndStartGatParams mExtractFilesAndStartGatParams;


        public virtual SearchExeParams SearchExeParams
        {
            get
            {
                if ((this.mSearchExeParams == null))
                {
                    this.mSearchExeParams = new SearchExeParams();
                }
                return this.mSearchExeParams;
            }
        }

        private SearchExeParams mSearchExeParams;


        public virtual GenerateExtraSonnySat14052016Params GenerateExtraSonnySat14052016Params
        {
            get
            {
                if ((this.mGenerateExtraSonnySat14052016Params == null))
                {
                    this.mGenerateExtraSonnySat14052016Params = new GenerateExtraSonnySat14052016Params();
                }
                return this.mGenerateExtraSonnySat14052016Params;
            }
        }

        private GenerateExtraSonnySat14052016Params mGenerateExtraSonnySat14052016Params;

        public virtual SelectDonJohnsonMon09052016Params SelectDonJohnsonMon09052016Params
        {
            get
            {
                if ((this.mSelectDonJohnsonMon09052016Params == null))
                {
                    this.mSelectDonJohnsonMon09052016Params = new SelectDonJohnsonMon09052016Params();
                }
                return this.mSelectDonJohnsonMon09052016Params;
            }
        }

        private SelectDonJohnsonMon09052016Params mSelectDonJohnsonMon09052016Params;


        public virtual SelectDateParams SelectDateParams
        {
            get
            {
                if ((this.mSelectDateParams == null))
                {
                    this.mSelectDateParams = new SelectDateParams();
                }
                return this.mSelectDateParams;
            }
        }

        private SelectDateParams mSelectDateParams;

        public virtual CheckFirstItemWeekViewMon09052016ExpectedValues CheckFirstItemWeekViewMon09052016ExpectedValues
        {
            get
            {
                if ((this.mCheckFirstItemWeekViewMon09052016ExpectedValues == null))
                {
                    this.mCheckFirstItemWeekViewMon09052016ExpectedValues = new CheckFirstItemWeekViewMon09052016ExpectedValues();
                }
                return this.mCheckFirstItemWeekViewMon09052016ExpectedValues;
            }
        }

        private CheckFirstItemWeekViewMon09052016ExpectedValues mCheckFirstItemWeekViewMon09052016ExpectedValues;

        public virtual TuesdayParams TuesdayParams
        {
            get
            {
                if ((this.mTuesdayParams == null))
                {
                    this.mTuesdayParams = new TuesdayParams();
                }
                return this.mTuesdayParams;
            }
        }

        private TuesdayParams mTuesdayParams;


        public virtual WednesdayParams WednesdayParams
        {
            get
            {
                if ((this.mWednesdayParams == null))
                {
                    this.mWednesdayParams = new WednesdayParams();
                }
                return this.mWednesdayParams;
            }
        }

        private WednesdayParams mWednesdayParams;

        public virtual ThursdayParams ThursdayParams
        {
            get
            {
                if ((this.mThursdayParams == null))
                {
                    this.mThursdayParams = new ThursdayParams();
                }
                return this.mThursdayParams;
            }
        }

        private ThursdayParams mThursdayParams;


        public virtual FridayFreeParams FridayFreeParams
        {
            get
            {
                if ((this.mFridayFreeParams == null))
                {
                    this.mFridayFreeParams = new FridayFreeParams();
                }
                return this.mFridayFreeParams;
            }
        }

        private FridayFreeParams mFridayFreeParams;



        public virtual SaturdayParams SaturdayParams
        {
            get
            {
                if ((this.mSaturdayParams == null))
                {
                    this.mSaturdayParams = new SaturdayParams();
                }
                return this.mSaturdayParams;
            }
        }

        private SaturdayParams mSaturdayParams;

        public virtual WeekdayCheckExpectedValues WeekdayCheckExpectedValues
        {
            get
            {
                if ((this.mWeekdayCheckExpectedValues == null))
                {
                    this.mWeekdayCheckExpectedValues = new WeekdayCheckExpectedValues();
                }
                return this.mWeekdayCheckExpectedValues;
            }
        }

        private WeekdayCheckExpectedValues mWeekdayCheckExpectedValues;

        public virtual SelectDocMalFromAdministrationParams SelectDocMalFromAdministrationParams
        {
            get
            {
                if ((this.mSelectDocMalFromAdministrationParams == null))
                {
                    this.mSelectDocMalFromAdministrationParams = new SelectDocMalFromAdministrationParams();
                }
                return this.mSelectDocMalFromAdministrationParams;
            }
        }

        private SelectDocMalFromAdministrationParams mSelectDocMalFromAdministrationParams;

        public virtual SelectAbsenceDocParams SelectAbsenceDocParams
        {
            get
            {
                if ((this.mSelectAbsenceDocParams == null))
                {
                    this.mSelectAbsenceDocParams = new SelectAbsenceDocParams();
                }
                return this.mSelectAbsenceDocParams;
            }
        }

        private SelectAbsenceDocParams mSelectAbsenceDocParams;

        public virtual AddAbsenceDateParams AddAbsenceDateParams
        {
            get
            {
                if ((this.mAddAbsenceDateParams == null))
                {
                    this.mAddAbsenceDateParams = new AddAbsenceDateParams();
                }
                return this.mAddAbsenceDateParams;
            }
        }

        private AddAbsenceDateParams mAddAbsenceDateParams;

        public virtual LoginMinGatParams LoginMinGatParams
        {
            get
            {
                if ((this.mLoginMinGatParams == null))
                {
                    this.mLoginMinGatParams = new LoginMinGatParams();
                }
                return this.mLoginMinGatParams;
            }
        }

        private LoginMinGatParams mLoginMinGatParams;

        public virtual CheckminGatHelpFileVersionExpectedValues CheckminGatHelpFileVersionExpectedValues
        {
            get
            {
                if ((this.mCheckminGatHelpFileVersionExpectedValues == null))
                {
                    this.mCheckminGatHelpFileVersionExpectedValues = new CheckminGatHelpFileVersionExpectedValues();
                }
                return this.mCheckminGatHelpFileVersionExpectedValues;
            }
        }

        private CheckminGatHelpFileVersionExpectedValues mCheckminGatHelpFileVersionExpectedValues;

        public virtual CheckMinGatVersionExpectedValues CheckMinGatVersionExpectedValues
        {
            get
            {
                if ((this.mCheckMinGatVersionExpectedValues == null))
                {
                    this.mCheckMinGatVersionExpectedValues = new CheckMinGatVersionExpectedValues();
                }
                return this.mCheckMinGatVersionExpectedValues;
            }
        }

        private CheckMinGatVersionExpectedValues mCheckMinGatVersionExpectedValues;

        public virtual AddApplicationPoolMinGatParams AddApplicationPoolMinGatParams
        {
            get
            {
                if ((this.mAddApplicationPoolMinGatParams == null))
                {
                    this.mAddApplicationPoolMinGatParams = new AddApplicationPoolMinGatParams();
                }
                return this.mAddApplicationPoolMinGatParams;
            }
        }

        private AddApplicationPoolMinGatParams mAddApplicationPoolMinGatParams;

        public virtual AddMInGatParams AddMInGatParams
        {
            get
            {
                if ((this.mAddMInGatParams == null))
                {
                    this.mAddMInGatParams = new AddMInGatParams();
                }
                return this.mAddMInGatParams;
            }
        }

        private AddMInGatParams mAddMInGatParams;

        public virtual CheckWs1VersionExpectedValues CheckWs1VersionExpectedValues
        {
            get
            {
                if ((this.mCheckWs1VersionExpectedValues == null))
                {
                    this.mCheckWs1VersionExpectedValues = new CheckWs1VersionExpectedValues();
                }
                return this.mCheckWs1VersionExpectedValues;
            }
        }

        private CheckWs1VersionExpectedValues mCheckWs1VersionExpectedValues;


        public virtual CheckWs2VersionExpectedValues CheckWs2VersionExpectedValues
        {
            get
            {
                if ((this.mCheckWs2VersionExpectedValues == null))
                {
                    this.mCheckWs2VersionExpectedValues = new CheckWs2VersionExpectedValues();
                }
                return this.mCheckWs2VersionExpectedValues;
            }
        }

        private CheckWs2VersionExpectedValues mCheckWs2VersionExpectedValues;


        public virtual SeachHeHaParams SeachHeHaParams
        {
            get
            {
                if ((this.mSeachHeHaParams == null))
                {
                    this.mSeachHeHaParams = new SeachHeHaParams();
                }
                return this.mSeachHeHaParams;
            }
        }

        private SeachHeHaParams mSeachHeHaParams;

        public virtual SetupWebBaseParams SetupWebBaseParams
        {
            get
            {
                if ((this.mSetupWebServiceBaseParams == null))
                {
                    this.mSetupWebServiceBaseParams = new SetupWebBaseParams();
                }
                return this.mSetupWebServiceBaseParams;
            }
        }

        private SetupWebBaseParams mSetupWebServiceBaseParams;

        public virtual CleanUpWsIISParams CleanUpWsIISParams
        {
            get
            {
                if ((this.mCleanUpWsIISParams == null))
                {
                    this.mCleanUpWsIISParams = new CleanUpWsIISParams();
                }
                return this.mCleanUpWsIISParams;
            }
        }

        private CleanUpWsIISParams mCleanUpWsIISParams;


        public virtual AddEmployeesToFlexParams AddEmployeesToFlexParams
        {
            get
            {
                if ((this.mAddEmployeesToFlexParams == null))
                {
                    this.mAddEmployeesToFlexParams = new AddEmployeesToFlexParams();
                }
                return this.mAddEmployeesToFlexParams;
            }
        }

        private AddEmployeesToFlexParams mAddEmployeesToFlexParams;

        public virtual ConnectGatToDataBaseParams ConnectGatToDataBaseParams
        {
            get
            {
                if ((this.mConnectGatToDataBaseParams == null))
                {
                    this.mConnectGatToDataBaseParams = new ConnectGatToDataBaseParams();
                }
                return this.mConnectGatToDataBaseParams;
            }
        }

        private ConnectGatToDataBaseParams mConnectGatToDataBaseParams;

        public virtual CheckGatTaskSchedulerVersionExpectedValues CheckGatTaskSchedulerVersionExpectedValues
        {
            get
            {
                if ((this.mCheckGatTaskSchedulerVersionExpectedValues == null))
                {
                    this.mCheckGatTaskSchedulerVersionExpectedValues = new CheckGatTaskSchedulerVersionExpectedValues();
                }
                return this.mCheckGatTaskSchedulerVersionExpectedValues;
            }
        }

        private CheckGatTaskSchedulerVersionExpectedValues mCheckGatTaskSchedulerVersionExpectedValues;

        public virtual CheckGatFlexSweLanguageExpectedValues CheckGatFlexSweLanguageExpectedValues
        {
            get
            {
                if ((this.mCheckGatFlexSweLanguageExpectedValues == null))
                {
                    this.mCheckGatFlexSweLanguageExpectedValues = new CheckGatFlexSweLanguageExpectedValues();
                }
                return this.mCheckGatFlexSweLanguageExpectedValues;
            }
        }

        private CheckGatFlexSweLanguageExpectedValues mCheckGatFlexSweLanguageExpectedValues;

        public virtual ExchangeJohnson_1Params ExchangeJohnson_1Params
        {
            get
            {
                if ((this.mExchangeJohnson_1Params == null))
                {
                    this.mExchangeJohnson_1Params = new ExchangeJohnson_1Params();
                }
                return this.mExchangeJohnson_1Params;
            }
        }

        private ExchangeJohnson_1Params mExchangeJohnson_1Params;
    }


    //public class IniFile_old   // revision 10
    //{
    //    string Path;
    //    string EXE = Assembly.GetExecutingAssembly().GetName().Name;

    //    [DllImport("kernel32")]
    //    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    //    [DllImport("kernel32")]
    //    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

    //    public IniFile_old(string IniPath = null)
    //    {
    //        Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
    //    }

    //    public string Read(string Key, string Section = null)
    //    {
    //        var RetVal = new StringBuilder(255);
    //        GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
    //        return RetVal.ToString();
    //    }

    //    public void Write(string Key, string Value, string Section = null)
    //    {
    //        WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
    //    }

    //    public void DeleteKey(string Key, string Section = null)
    //    {
    //        Write(Key, null, Section ?? EXE);
    //    }

    //    public void DeleteSection(string Section = null)
    //    {
    //        Write(null, null, Section ?? EXE);
    //    }

    //    public bool KeyExists(string Key, string Section = null)
    //    {
    //        return Read(Key, Section).Length > 0;
    //    }
    //}
    /// <summary>
    /// Parameters to be passed into 'ExtractFilesAndStartGat'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ExtractFilesAndStartGatParams
    {
  
        static string DestinationAddressZipFiles
        {
            get
            {
                var zipFiles = Path.Combine(TestData.GetWorkingDirectory, @"ZipFiles");
                return zipFiles;
            }
        }

        public string GetWishPlanKey(string server)
        {
            var wishPlan = @"<add key=""WishPlanWSAddress"" value=""https://" + server + @".internal.visma.com/gatWs2/WishPlanWebService.asmx""/>";
            return wishPlan;
        }

        //public string GetWishPlanKey_SE(string server)
        //{
        //    var wishPlan = @"<add key=""WishPlanWSAddress"" value=""https://" + server + @".internal.visma.com/gatWs2_se/WishPlanWebService.asmx""/>";
        //    return wishPlan;
        //}

        public string GetGatFlexKey(string server)
        {
            var gatFlex = @"http://" + server + @"/GatWs1/FlexService.asmx";
            return gatFlex;
        }

        //public string GetGatFlexKey_SE(string server)
        //{
        //    var gatFlex = @"http://" + server + @"/GatWs1_se/FlexService.asmx";
        //    return gatFlex;
        //}
        public string GetGatTaskSchedulerKey(string server)
        {
            var gatTaskScheduler = @"<TaskSource name=""GatWS InstanceX"" enabled=""true"" url=""http://" + server + @"/gatws1/GatTaskSchedulerDataService.asmx"" accessCode=""mysecret""/>";
            return gatTaskScheduler;
        }

        #region Fields
        public string TargetAddressZipFiles = @"\\NO-VSW-WS-0030\TeamCity\Gat\";

        /// <summary>
        /// Type 'ASCL' in text box
        /// </summary>
        public string LoginName = "ASCL";
        public string LoginNameSe = "GATSOFT";

        /// <summary>
        /// Type '********' in text box
        /// </summary>
        public string PassWord = "VLLA+bJzNf882FWpmiwJPY0v6P7+sGGK";

        public string GatturnusPath = DestinationAddressZipFiles + @"\Gat_no\GATTURNUS.exe";
        //public string GatturnusPath_SE = DestinationAddressZipFiles + @"\Gat_se\GATTURNUS.exe";

        /// <summary>
        /// Select 'C:\Temp\ZipFiles\Gat_ini' in 'Address' combo box
        /// </summary>
        public string AddressGatIni = DestinationAddressZipFiles + @"\Gat_no\Template";
        //public string AddressGatIni_SE = DestinationAddressZipFiles + @"\Gat_se\Template";


        /// <summary>
        /// Type 'C:\Temp\ZipFiles\Gat_no' in 'Files will be extracted to this folder:' text box
        /// </summary>
        public string UIExtractedGatFiles = DestinationAddressZipFiles + @"\Gat_no";
        public string UIExtractedMinGatFiles = DestinationAddressZipFiles + @"\MinGat_no";
        public string UIExtractedGatWS1Files = DestinationAddressZipFiles + @"\GatWs1Zip_no";
        public string UIExtractedGatWS2Files = DestinationAddressZipFiles + @"\GatWs2Zip_no";
        public string UIExtractedGatFlexTimeClockFiles = DestinationAddressZipFiles + @"\GatFlexTimeClockZip_no";
        public string UIExtractedGatTaskSchedulerFiles = DestinationAddressZipFiles + @"\GatTaskSchedulerZip_no";

        //IIS
        public string UIWWWRootMinGatDir = @"C:\inetpub\wwwroot\MinGat";
        public string UIWWWRootGatWS1Dir = @"C:\inetpub\wwwroot\GatWs1";
        public string UIWWWRootGatWS2Dir = @"C:\inetpub\wwwroot\GatWs2";

        //Common
        public string LineToFindConnectionString = @"<add name=""default"" connectionString=""TYPE=MSSQL2008;HOSTNAME='HOSTNAME';DATABASE='DATABASE';USERNAME='USERNAME';PASSWORD='PASSWORD'"" />";
        public string IISManager = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools\IIS Manager.lnk";

        //WS1 username/pasword
        public string LineToFindFlexServiceUser = @"<add key=""FlexServiceUser"" value=""""/>";
        public string LineToFindFlexServicePassword = @"<add key=""FlexServicePassword"" value=""""/>";

        //MinGat
        public string MinGatAppsettingsConfig = DestinationAddressZipFiles + @"\MinGat_no\Template\appSettings.config";
        public string MinGatConnectionStringsConfig = DestinationAddressZipFiles + @"\MinGat_no\Template\connectionStrings.config";
        public string LineToFindMinGatAppsettings = @"<add key=""WishPlanWSAddress"" value=""http://myserver/gatws2/WishPlanWebService.asmx""/>";

        //MinGat_SE
        public string MinGatAppsettingsConfig_SE = DestinationAddressZipFiles + @"\MinGat_se\Template\appSettings.config";
        public string MinGatConnectionStringsConfig_SE = DestinationAddressZipFiles + @"\MinGat_se\Template\connectionStrings.config";
        //public string LineToFindMinGatAppsettings = @"<add key=""WishPlanWSAddress"" value=""http://myserver/gatws2/WishPlanWebService.asmx""/>";

        //WS1
        public string Ws1WebConfig = DestinationAddressZipFiles + @"\GatWs1Zip_no\Template\Web.config";
        //WS1_SE
        public string Ws1WebConfig_SE = DestinationAddressZipFiles + @"\GatWs1Zip_se\Template\Web.config";

        //WS2
        public string Ws2WebConfig = DestinationAddressZipFiles + @"\GatWs2Zip_no\Template\Web.config";
        //WS2_SE
        public string Ws2WebConfig_SE = DestinationAddressZipFiles + @"\GatWs2Zip_se\Template\Web.config";

        //Flex
        public string GatFlexConfig = DestinationAddressZipFiles + @"\GatFlexTimeClockZip_no\GatFlexTimeClock.exe.config";
        public string LineToFindGatFlexConfig = @"http://SERVER/WEBSERVICE/FlexService.asmx";

        //Flex_SE
        public string GatFlexConfig_SE = DestinationAddressZipFiles + @"\GatFlexTimeClockZip_se\GatFlexTimeClock.exe.config";
        //public string LineToFindGatFlexConfig = @"http://SERVER/WEBSERVICE/FlexService.asmx";

        //GatTaskScheduler
        public string TextToFindInGatTaskSchedulerConfig = @"<!--
      <TaskSource name=""GatWS InstanceX"" enabled=""true"" url=""http://myserver/GatTaskSchedulerDataService.asmx"" accessCode=""mysecret""/>
      -->";

        public string GatTaskSchedulerConfig = @"C:\Program Files (x86)\Gat Task Scheduler Service\GatTaskSchedulerService.exe.config";
        public string GatTaskSchedulerFolder = @"C:\Program Files (x86)\Gat Task Scheduler Service";
        public string UninstallGatTaskScheduler = @"C:\Program Files (x86)\Gat Task Scheduler Service\unins000.exe";
        public string InstallGatTaskScheduler = DestinationAddressZipFiles + @"\GatTaskSchedulerZip_no\setup.exe";
        public string GatTaskSchedulerServiceName = "GatTaskSchedulerService";

        /// Type '{Tab}' in text box
        /// </summary>
        public string UISendTabKey = "{Tab}";
        #endregion
    }

    /// <summary>
    /// Parameters to be passed into 'SearchExe'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SearchExeParams
    {

        #region Fields
        /// <summary>
        /// Type '.' in 'Search Box' text box
        /// </summary>
        public string UISearchBoxEditText = ".exe";

        /// <summary>
        /// Type 'exe{Enter}' in 'Search Gat_641_no' pane
        /// </summary>
        public string UISearchGat_641_noPaneSendKeys = "{Enter}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'GenerateExtraSonnySat14052016'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class GenerateExtraSonnySat14052016Params
    {

        #region Fields
        /// <summary>
        /// Select '0' in 'PeriodScheduleListBoxControl`1' list box
        /// </summary>
        public string UIPeriodScheduleListBoListSelectedIndicesAsString = "0";

        /// <summary>
        /// Type '{Down}' in 'teFind' text box
        /// </summary>
        public string UITeSelectCause = "{Down 2}{Enter}";
        public string UITeSelectShiftcodeD1 = "{Down}{Enter}";

        /// <summary>
        /// Type '14.05.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate' PopupEdit
        /// </summary>
        public DateTime UIPceDate14052016 = new DateTime(2016,5,14);//"20160514";//"14.05.2016"
        /// <summary>
        /// Type '{NumPad2}{NumPad0}{NumPad1}{NumPad6}{Tab}' in 'pceDate' PopupEdit
        /// </summary>
        public string UITab = "{Tab}";

        /// <summary>
        /// Type 'System.String' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueTypeName = "System.String";

        /// <summary>
        /// Type 'SYKDOM' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueAsString = "SYKDOM";

        /// <summary>
        /// Type 'Test 99' in 'eComment' text box
        /// </summary>
        public string UIECommentEditValueAsString = "Test 99";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectDonJohnsonMon09052016'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectDonJohnsonMon09052016Params
    {

        #region Fields
        /// <summary>
        /// Select '1' in 'PeriodScheduleListBoxControl`1' list box
        /// </summary>
        public string UIPeriodScheduleListBoListSelectedIndicesAsString = "1";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectDateParams
    {

        #region Fields
        /// <summary>
        /// Type '09.05.2016 [SelectionStart]0' in 'pceDate' PopupEdit
        /// </summary>
        public string UIPceDatePopupEditValueAsString = "09.05.2016 [SelectionStart]0";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckFirstItemWeekViewMon09052016'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckFirstItemWeekViewMon09052016ExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'Text' property of 'D1 - (08.00 - 16.00)' list item equals 'D1 - (08.00 - 16.00)'
        /// </summary>
        public string UID108001600ListItemText = "D1 - (08.00 - 16.00)";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Tuesday'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class TuesdayParams
    {

        #region Fields
        /// <summary>
        /// Select '0' in 'PeriodScheduleListBoxControl`1' list box
        /// </summary>
        public string UIPeriodScheduleListBoList1SelectedIndicesAsString = "0";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Wednesday'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class WednesdayParams
    {

        #region Fields
        /// <summary>
        /// Select '1' in 'PeriodScheduleListBoxControl`1' list box
        /// </summary>
        public string UIPeriodScheduleListBoList2SelectedIndicesAsString = "1";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Thursday'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ThursdayParams
    {

        #region Fields
        /// <summary>
        /// Select '2' in 'PeriodScheduleListBoxControl`1' list box
        /// </summary>
        public string UIPeriodScheduleListBoList3SelectedIndicesAsString = "2";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'FridayFree'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class FridayFreeParams
    {

        #region Fields
        /// <summary>
        /// Select '0' in 'PeriodScheduleListBoxControl`1' list box
        /// </summary>
        public string UIPeriodScheduleListBoList4SelectedIndicesAsString = "0";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Saturday'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SaturdayParams
    {

        #region Fields
        /// <summary>
        /// Select '0' in 'PeriodScheduleListBoxControl`1' list box
        /// </summary>
        public string UIPeriodScheduleListBoList5SelectedIndicesAsString = "0";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'WeekdayCheck'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class WeekdayCheckExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'Text' property of 'D - (08.00 - 16.00)' list item equals 'D - (08.00 - 16.00)'
        /// </summary>
        public string UID08001600ListItemText = "D - (08.00 - 16.00)";

        /// <summary>
        /// Verify that the 'Text' property of 'D - (08.00 - 16.00)' list item equals 'D - (08.00 - 16.00)'
        /// </summary>
        public string UID08001600ListItemText1 = "D - (08.00 - 16.00)";

        /// <summary>
        /// Verify that the 'Text' property of 'D1 - (08.00 - 16.00)' list item equals 'D1 - (08.00 - 16.00)'
        /// </summary>
        public string UID108001600ListItemText = "D1 - (08.00 - 16.00)";

        /// <summary>
        /// Verify that the 'Text' property of '00.00 - 00.00' list item equals '00.00 - 00.00'
        /// </summary>
        public string UIItem00000000ListItemText = "00.00 - 00.00";

        /// <summary>
        /// Verify that the 'Text' property of 'D - (08.00 - 16.00)' list item equals 'D - (08.00 - 16.00)'
        /// </summary>
        public string UID08001600ListItemText2 = "D - (08.00 - 16.00)";

        /// <summary>
        /// Verify that the 'Text' property of '00.00 - 00.00' list item equals '00.00 - 00.00'
        /// </summary>
        public string UIItem00000000ListItemText1 = "00.00 - 00.00";

        /// <summary>
        /// Verify that the 'Text' property of 'D - (08.00 - 16.00)' list item equals 'D - (08.00 - 16.00)'
        /// </summary>
        public string UID08001600ListItemText3 = "D - (08.00 - 16.00)";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectDocMalFromAdministration'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectDocMalFromAdministrationParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.DataModel.Simple+AbsenceCode' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueTypeName = "Gatsoft.Gat.DataModel.Simple+AbsenceCode";

        /// <summary>
        /// Type '' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueAsString = "";
        #endregion
    }

    /// <summary>
    /// Parameters to be passed into 'SelectAbsenceDoc'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectAbsenceDocParams
    {

        #region Fields
        /// <summary>
        /// Select 'chbRecievedWrittenDocumentation' check box
        /// </summary>
        public bool UIChbRecievedWrittenDoCheckBoxChecked = true;

        /// <summary>
        /// Type '' in 'lueDocuments' LookUpEdit
        /// </summary>
        public string UILueDocumentsLookUpEditValueAsString = "";

        /// <summary>
        /// Type '{Down}' in 'lueDocuments' LookUpEdit
        /// </summary>
        public string UILueDocumentsLookUpEditSendKeys = "{Down}";

        /// <summary>
        /// Type '{Enter}' in 'PopupLookUpEditForm' window
        /// </summary>
        public string UIPopupLookUpEditFormWindowSendKeys = "{Enter}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddAbsenceDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddAbsenceDateParams
    {

        #region Fields
        /// <summary>
        /// Type '12.05.2016 [SelectionStart]0' in 'pceDate' PopupEdit
        /// </summary>
        public string UIPceDatePopupEditValueAsString = "12.05.2016 [SelectionStart]0";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'LoginMinGat'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class LoginMinGatParams
    {

        #region Fields
        /// <summary>
        /// Type 'ascl' in 'Brukernavn*' text box
        /// </summary>
        public string UIBrukernavnEditText = "ascl";

        /// <summary>
        /// Type '{Tab}' in 'Brukernavn*' text box
        /// </summary>
        public string UIBrukernavnEditSendKeys = "{Tab}";

        /// <summary>
        /// Type '********' in 'Passord*' text box
        /// </summary>
        public string UIPassordEditPassword = "VLLA+bJzNf882FWpmiwJPY0v6P7+sGGK";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckminGatHelpFileVersion'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckminGatHelpFileVersionExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'InnerText' property of 'MinGat Hjelp v.6.4 COPYRIGHT © - GA' pane equals 'MinGat Hjelp v.6.4     COPYRIGHT ©  - GATSOFT AS'
        /// </summary>
        public string UIMinGatHjelpv64COPYRIPaneInnerText = "MinGat Hjelp v.6.4     COPYRIGHT ©  - GATSOFT AS";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckMinGatVersion'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckMinGatVersionExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'InnerText' property of 'v.6.4.2.38306' pane equals 'v.6.4.2.38306'
        /// </summary>
        public string UIV64238306PaneInnerText = "v.6.4.2.38306";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddApplicationPoolMinGat'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddApplicationPoolMinGatParams
    {

        #region Fields
        /// <summary>
        /// Type 'MinGat' in '_applicationPoolNameTextBox' text box
        /// </summary>
        public string UI_applicationPoolNameEditText = "MinGat";
        public string UI_applicationPoolNameEditText_SE = "MinGat_se";

        /// <summary>
        /// Select '.NET CLR Version v4.0.30319' in '.NET CLR version:' combo box
        /// </summary>
        public string UINETCLRWin10 = ".NET CLR Version v4.0.30319";
        public string UINETWin7 = ".NET Framework v4.0.30319";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddMInGat'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddMInGatParams
    {

        #region Fields
        /// <summary>
        /// Select 'MinGat' in 'Application pool:' combo box
        /// </summary>
        public string UIApplicationpoolComboBoxSelectedItem = "MinGat";
        public string UIApplicationpoolComboBoxSelectedItem_SE = "MinGat_se";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckWs1Version'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckWs1VersionExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'InnerText' property of 'Gat Webservices v. 6.4.2.38306' pane equals 'Gat Webservices v. 6.4.2.38306'
        /// </summary>
        public string UIGatWebservicesv64238PaneInnerText = "Gat Webservices v. 6.4.2.38306";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckWs2Version'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckWs2VersionExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'InnerText' property of 'Gat Webservices2 v.6.4.2.38306' pane equals 'Gat Webservices2 v.6.4.2.38306'
        /// </summary>
        public string UIGatWebservices2v6423PaneInnerText = "Gat Webservices2 v.6.4.2.38306";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SeachHeHa'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SeachHeHaParams
    {

        #region Fields
        /// <summary>
        /// Type '' in 'txtSearch' text box
        /// </summary>
        public string UITxtSearchEditValueAsString = "HEHA";

        /// <summary>
        /// Type 'Control + i' in 'xscMain' ScrollableControl
        /// </summary>
        public string UIXscMainScrollableControlSendKeys = "{ENTER}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetupWebServiceBase'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetupWebBaseParams
    {
        public string GetWebServiceBaseUrlWs1(string server)
        {
            var url = @"http://" + server + @"/gatws1";
            return url;
        }
        public string GetWebServiceBaseUrlWs2(string server)
        {
            var url = @"http://" + server + @"/gatws2";
            return url;
        }

        public string GetWebBaseUrMinGat2(string server)
        {
            var url = @"https://" + server + @".internal.visma.com/mingat";
            return url;
        }
        public string GetWebBaseUrMinGat2_SE(string server)
        {
            var url = @"https://" + server + @".internal.visma.com/mingat_se";
            return url;
        }

        public string GetWebServiceBaseGatTaskSchedulerWs(string server)
        {
            var url = @"http://" + server + @":3333";
            return url;
        }
    }
    /// <summary>
    /// Parameters to be passed into 'CleanUpWsIIS'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CleanUpWsIISParams
    {

        #region Fields
        /// <summary>
        /// Select 'GatWs2' in '_listView' list box
        /// </summary>
        public string UI_listViewListSelectedItemsAsString = "GatWs2";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddEmployeesToFlex'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddEmployeesToFlexParams
    {

        #region Fields
        /// <summary>
        /// Type '{F3}' in 'Administrasjon' client
        /// </summary>
        public string UIAdministrasjonClientSendKeys = "{F3}";

        /// <summary>
        /// Type 'ENHET OPPSETT' in text box
        /// </summary>
        public string UIIENHET_OPPSETT = "ENHET +OPPSETT";
        public string UIIENHET_OPPSETT_se = "ENHETSINSTÄLLNING";

        /// <summary>
        /// Type '{Enter}' in text box
        /// </summary>
        public string UIItemEditSendKeys = "{Enter}";

        /// <summary>
        /// Type '{Enter}' in 'Administrasjon' client
        /// </summary>
        public string UIAdministrasjonClientSendKeys1 = "{Enter}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ConnectGatToDataBase'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ConnectGatToDataBaseParams
    {

        #region Fields
        /// <summary>
        /// Select 'Microsoft OLE DB Provider for SQL Server' in 'Select the data you want to connect to:' list box
        /// </summary>
        public string UISelectthedatayouwantListSelectedItemsAsString = "Microsoft OLE DB Provider for SQL Server";

        ///// <summary>
        ///// Select 'WIN7ASCL' in '1. Select or enter a server name:' combo box
        ///// </summary>
        //public string UIItem1SelectorenterasComboBoxSelectedItem = "WIN7ASCL";

        /// <summary>
        /// Type '{Tab}' in 'User name:' text box
        /// </summary>
        public string UITab = "{Tab}";

        /// <summary>
        /// Type '********' in 'Password:' text box
        /// </summary>
        

        /// <summary>
        /// Select 'Allow &saving password' check box
        /// </summary>
        public bool UIAllowsavingpasswordCheckBoxChecked = true;

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckGatTaskSchedulerVersion'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckGatTaskSchedulerVersionExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'InnerText' property of 'GatScheduler 6.4.2.38741' pane equals 'GatScheduler 6.4.2.38741'
        /// </summary>
        public string UIGatScheduler64238741PaneInnerText = "GatScheduler 6.4.2.38741";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckGatFlexSweLanguage'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckGatFlexSweLanguageExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'Text' property of 'LabelControl' label equals 'Alla'
        /// </summary>
        public string UILabelControlLabelText = "Alla";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ExchangeJohnson_1'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ExchangeJohnson_1Params
    {

        #region Fields
        /// <summary>
        /// Type 'crockett{Enter}' in 'teFind' text box
        /// </summary>
        public string UITeFindEditSendKeys = "crockett{Enter}";
        #endregion
    }
}
