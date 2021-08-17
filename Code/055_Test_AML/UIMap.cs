namespace _055_Test_AML
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System;
    using System.Collections.Generic;
    using System.CodeDom.Compiler;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Drawing;
    using System.Windows.Input;
    using System.Text.RegularExpressions;
    using CommonTestData;
    using System.IO;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.Threading;
    using System.Globalization;


    public partial class UIMap
    {
        private CommonUIFunctions.UIMap UICommon;

        #region Fields
        TestContext TestContext;

        private int CurrentYear;
        private DateTime FirstDateOfCurrentYear;
        private DateTime LastDateOfCurrentYear;
        
        private DateTime RosterplanStartDate;
        private DateTime RosterplanValidDate;

        private DateTime DateRelativeToEffectuationDate;
        private DateTime DateRelativeToEffectuationEndDate;
        
        public string ReportFilePath;
        public string ConfigFilePath;
        public string ReportFileName = "055_excell";
        public string FileType = ".xls";
        #endregion

        private UIMapVS2017Classes.UIMapVS2017 UIMapVS2017
        {
            get
            {
                if ((this.map1 == null))
                {
                    this.map1 = new UIMapVS2017Classes.UIMapVS2017(TestContext);
                }

                return this.map1;
            }
        }
        private UIMapVS2017Classes.UIMapVS2017 map1;

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            var executingDir = TestData.GetWorkingDirectory;
            ReportFilePath = Path.Combine(executingDir, @"Reports\Test_055\");
            ConfigFilePath = Path.Combine(executingDir, @"Config");

            UICommon = new CommonUIFunctions.UIMap(TestContext);
            ConfiguratedDates();
        }

        #region Common functions
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

        public string ReadPhysicalMemoryUsage()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.WorkingSet64);
        }
        public string ReadPagedMemorySize64()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.PagedMemorySize64);
        }
        public List<string> TimeLapseInSeconds(DateTime timeBefore, DateTime timeAfter, string text)
        {
            List<string> errorList = new List<string>();
            string elapsedTimeOutput = "";

            errorList.AddRange(LoadBalanceTesting.TimeLapseInSeconds(timeBefore, timeAfter, text, out elapsedTimeOutput));
            TestContext.WriteLine(elapsedTimeOutput);

            return errorList;
        }

        private void ConfiguratedDates()
        {
            //var currentDate = new DateTime(2024, 8, 28);
            CurrentYear = DateTime.Now.Year;

            DateTime firstDayOfCurrentYear = new DateTime(CurrentYear, 1, 1);
            DateTime lastDayOfCurrentYear = new DateTime(CurrentYear, 12, 31);
            DateTime senestePinse = new DateTime(CurrentYear, 6, 13);

            RosterplanStartDate = GetNextDayOfWeekDay(senestePinse, DayOfWeek.Monday);
            FirstDateOfCurrentYear = firstDayOfCurrentYear;
            LastDateOfCurrentYear = lastDayOfCurrentYear;
            RosterplanValidDate = lastDayOfCurrentYear.AddYears(1);
            DateRelativeToEffectuationDate = RosterplanStartDate;
            DateRelativeToEffectuationEndDate = GetPreviousDayOfWeekDay(DateRelativeToEffectuationDate.AddDays(84), DayOfWeek.Sunday);         
        }
        public int GetCurrentYearAsInt()
        {
            return CurrentYear;
        }
        public DateTime GetFirstDateOfCurrentYear()
        {
            return FirstDateOfCurrentYear;
        }
        public DateTime GetLastDateOfCurrentYear()
        {
            return LastDateOfCurrentYear;
        }
        public DateTime GetDateRelativeToEffectuationDate(int daysToAdd)
        {
            return DateRelativeToEffectuationDate.AddDays(daysToAdd);
        }
        public DateTime GetDateRelativeToEffectuationEndDate(int daysToAdd)
        {
            return DateRelativeToEffectuationEndDate.AddDays(daysToAdd);
        }
        private DateTime GetNextDayOfWeekDay(DateTime from, DayOfWeek dayOfWeek)
        {
            return SupportFunctions.NextDayOfWeekDay(from, dayOfWeek);
        }
        private DateTime GetPreviousDayOfWeekDay(DateTime from, DayOfWeek dayOfWeek)
        {
            return SupportFunctions.PreviousDayOfWeekDay(from, dayOfWeek);
        }
      
        #region Gat Webservices ConfigData

        #region Global Variable Declarations
        private static string zipFiles = Path.Combine(TestData.GetWorkingDirectory, @"ZipFiles");
        private static string DestinationAddressZipFiles = Path.Combine(zipFiles, @"Test_055");
    
        public string IISManager = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools\IIS Manager.lnk";
        
        public string UIExtractedGatWS1Files = DestinationAddressZipFiles + @"\GatWs1Zip_no";
        public string Ws1WebConfig = DestinationAddressZipFiles + @"\GatWs1Zip_no\Template\Web.config";
        public string LineToFindConnectionString = @"<add name=""default"" connectionString=""TYPE=MSSQL2008;HOSTNAME='HOSTNAME';DATABASE='DATABASE';USERNAME='USERNAME';PASSWORD='PASSWORD'"" />";
        public string UIWWWRootGatWS1Dir = @"C:\inetpub\wwwroot\GatWs1";

        #endregion

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
        private string CurrentStartFolderName
        {
            get
            {
                try
                {
                    return TestData.GetStartFolder;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get StartFolder: " + ex.Message);
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

        private string CurrentDBUser
        {
            get
            {
                try
                {
                    return TestData.GetDBUser;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get username: " + ex.Message);
                }

                return null;
            }
        }

        private string CurrentDBAuth
        {
            get
            {
                try
                {
                    return TestData.GetDBAuth();
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get authentication: " + ex.Message);
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
        
        public void GetZipFiles()
        {
            string TargetAddressZipFiles = @"\\IPVSLTCTFS001\TeamCity\Gat\";
            var filesToCopy = new List<string>() { "Gat Web Services 1 NOR" };//, "Gat Web Services 2 NOR" };
            UICommon.UIMapVS2017.GetGatZipFiles(filesToCopy, TargetAddressZipFiles + CurrentTargetFilesAddress, DestinationAddressZipFiles, CurrentFileVersion, TestContext);
        }

        //private void GetGatZipFiles(List<string> files, string sourcePath, string targetPath)
        //{
        //    DirectoryInfo dirInfo = new DirectoryInfo(targetPath);
        //    if (!dirInfo.Exists)
        //        Directory.CreateDirectory(targetPath);

        //    // Only get files that contains "file"
        //    var filePaths = Directory.GetFiles(sourcePath, "*" + CurrentFileVersion + "*", SearchOption.AllDirectories);
        //    foreach (string filePath in filePaths)
        //    {
        //        FileInfo mFile = new FileInfo(filePath);
        //        var fileName = "";
        //        var version = CurrentFileVersion.Remove(CurrentFileVersion.LastIndexOf("."), 2);

        //        fileName = mFile.Name.Replace(CurrentFileVersion + ".zip", "").TrimEnd();

        //        try
        //        {
        //            if (files.Exists(p => p == fileName))
        //            {
        //                SupportFunctions.FileCopy(mFile.Name, sourcePath, targetPath, TestContext);
        //                var fullName = mFile.FullName;
        //                var created = mFile.CreationTime;

        //                TestContext.WriteLine("Fil kopiert fra server: " + fullName + ", Produsert:  " + created);
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            TestContext.WriteLine("Unable to copy file: " + mFile.Name + ", " + e.Message);
        //        }
        //    }
        //}

        public void ConfigureWS1ForIIS()
        {
            var version = CurrentFileVersion.Remove(CurrentFileVersion.LastIndexOf("."), 2);
            var filePath = DestinationAddressZipFiles + @"\Gat Web Services 1 NOR " + CurrentFileVersion + ".zip";

            SupportFunctions.ExtractFiles(filePath, UIExtractedGatWS1Files, TestContext);

            //WS1
            SupportFunctions.EditTextFile(Ws1WebConfig, LineToFindConnectionString, SqlConnection());

            //kopiere filer fra template til Ws1
            SupportFunctions.FileCopy("web.config", UIExtractedGatWS1Files + @"\Template", UIExtractedGatWS1Files, TestContext);

            //Kopiere MinGatkatalog over på wwwroot
            string sourcePath = UIExtractedGatWS1Files;
            string targetPath = UIWWWRootGatWS1Dir;
            SupportFunctions.DirectoryCopy(sourcePath, targetPath, true);
        }

        public void ConfigureIIS()
        {
            SupportFunctions.StartProcess(IISManager, true);      

            Playback.Wait(1000);
            UICommon.AddApplicationPoolIIS("GatWs1", CurrentFrameWorkVersion);

            Playback.Wait(1000);
            UICommon.ConvertToAppInIIS("GatWs1");
            Playback.Wait(1000);

            UICommon.CloseIIS();

            //Kill IIS
            KillProcessByName("InetMgr");
        }

        /// <summary>
        /// BrowseWebSite
        /// </summary>
        public void BrowseWebSite()
        {
            #region Variable Declarations
            WinTreeItem uIGatWs1TreeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIGatWs1TreeItem;
            WinMenuItem uIBrowseMenuItem = this.UIItemWindow9.UIManageApplicationMenuItem.UIBrowseMenuItem;
            #endregion

            SupportFunctions.StartProcess(IISManager, true);
            Playback.Wait(1000);

            // Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'GatWs1' tree item
            Mouse.Click(uIGatWs1TreeItem, MouseButtons.Right, ModifierKeys.None, new Point(14, 7));

            // Click 'Manage Application' -> 'Browse' menu item
            Mouse.Click(uIBrowseMenuItem, new Point(62, 12));

            Playback.Wait(2000);

            UICommon.CloseIIS();

            //Kill IIS
            KillProcessByName("InetMgr");
        }

        /// <summary>
        /// OpenWeaBreakService - Use 'OpenWeaBreakServiceParams' to pass parameters into this method.
        /// </summary>
        public void OpenWeaBreakService(string depCode, int year, string accessCode = "")
        {
            #region Variable Declarations
         HtmlEdit uIDepartmentCodeEdit = this.UIGatWebserviceMainPagWindow.UIWeaBreakServiceWebSeDocument1.UIDepartmentCodeEdit;
            HtmlEdit uICalculatingYearEdit = this.UIGatWebserviceMainPagWindow.UIWeaBreakServiceWebSeDocument1.UICalculatingYearEdit;
            #endregion

            OpenWebBrowser(true);

            UIGatWebserviceMainPagWindow.UIWeaBreakServiceWebSeDocument1.WaitForControlExist();
            Playback.Wait(3000);
            
            // Type 'depCode' in 'departmentCode' text box
            uIDepartmentCodeEdit.Text = depCode;

            // Type '{Tab}' in 'departmentCode' text box
            Keyboard.SendKeys(uIDepartmentCodeEdit, this.OpenWeaBreakServiceParams.UITab, ModifierKeys.None);

            // Type 'year' in 'calculatingYear' text box
            uICalculatingYearEdit.Text = year.ToString();

            // Type '{Tab}' in 'calculatingYear' text box
            Keyboard.SendKeys(uICalculatingYearEdit, this.OpenWeaBreakServiceParams.UITab, ModifierKeys.None);

            UIMapVS2017.InsertAccessCodeWeaBreakService();
        }

        public void OpenWebBrowser(bool launch)
        {
            Process proc = null;
            if (launch)
            {
                //http://localhost/GatWs1/
                var _bw = BrowserWindow.Launch(new Uri("http://localhost/GatWs1/WeaBreakService.asmx?op=CalculateWeaAndSaveWeaBreaksOverrideScheduler"));
                proc = _bw.Process;
            }
            else
            {
                var _bw = BrowserWindow.FromProcess(proc);
                _bw.Close();

                KillProcessByName("iexplore");
            }

            Playback.Wait(1000);
        }

        public void CopyIniFileToGatsoftCatalog(bool se)
        { 
            //Endre dette når lager ny test
            var filePath = ConfigFilePath +@"\no";
            var fileDestinationPath = @"C:\Gatsoft\" + CurrentStartFolderName;

            if (!Directory.Exists(fileDestinationPath))
                throw new Exception("Unable to copy .ini-file. Directory does not exist!");

            if (se)
                filePath = ConfigFilePath + @"\se";

            SupportFunctions.FileCopy("GATTURNUS.ini", filePath, fileDestinationPath, TestContext);
        }

        public List<string> Cleanup()
        {
            var errorList = new List<string>();
            try
            {
                //Open IIS
                SupportFunctions.StartProcess(IISManager, true);
                Playback.Wait(2000);

                //WS
                try
                {
                    UICommon.CleanUpWsIIS();
                }
                catch (Exception e)
                {
                    errorList.Add("Error removing applications from IIS: " + e.Message);
                }

                try
                {
                    UICommon.RemoveApplicationPoolsWs();
                }
                catch (Exception e)
                {
                    errorList.Add("Error removing WS applicationpools from IIS: " + e.Message);
                }

                UICommon.CloseIIS();

                //DeleteWWWRootDirectories
                if (!SupportFunctions.DirectoryDelete(UIWWWRootGatWS1Dir, TestContext))
                    errorList.Add("Error deleting folder: " + UIWWWRootGatWS1Dir);

                //Delete extracted GatWs1 files
                if (!SupportFunctions.DirectoryDelete(UIExtractedGatWS1Files, TestContext))
                    errorList.Add("Error deleting folder: " + UIExtractedGatWS1Files);

                //Delete all sourcefiles
                SupportFunctions.DeleteZipFiles(DestinationAddressZipFiles, TestContext);

            }
            catch (Exception e)
            {
                errorList.Add("Cleanup error: " + e.Message);
            }

            return errorList;
        }

        #endregion


        public void KillProcessByName(string process)
        {
            SupportFunctions.KillProcessByName(process, TestContext);
        }
        public void DeleteReportFiles()
        {
            UICommon.UIMapVS2017.DeleteReportFiles(ReportFilePath);
        }

        public void CloseGat()
        {
            try
            {
                UICommon.CloseGat();
            }
            catch (Exception)
            {
                SupportFunctions.KillGatProcess(TestContext);
            }
        }

        public void StartGat(bool selectOtherDeps, bool logInfo, bool se = false)
        {
            UICommon.LaunchGatturnus(false);

            Playback.Wait(5000);

            if (se)
                UICommon.LoginGatAndSelectDepartment(UICommon.DepATL_1, null, "GATSOFT", logInfo);
            else
                UICommon.LoginGatAndSelectDepartment(UICommon.DepAML_1, null, "", logInfo);
        }

        public void ChangeDepartment()
        {
            Playback.Wait(2000);
            var otherDeps = new List<string>() { UICommon.DepAML_1 };//, UICommon.DepAML_2 };
            UICommon.ChangeDepartment(UICommon.DepAML_2, otherDeps);
        }

        #endregion

        public void CreateRosterplanCopy(string name)
        {
            var errorList = new List<string>();
            UICommon.CreateNewRosterplanCopy(name, RosterplanStartDate, "", "12", true, false);
            UICommon.UIMapVS2017.OkCreateRosterplanCopy();
            CloseRosterplanFromPlanTab();
        }
        public void SetRosterplanValidDate()
        {
            UICommon.OpenRosterplanSettings(true);
            UICommon.SetValidToDateRosterplan(RosterplanValidDate);
            UICommon.ClickOkRosterplanSettings();
        }
 
        /// <summary>
        /// ChangeAmlDayWorkerMinBeforeShift - Use 'ChangeAmlDayWorkerMinBeforeShiftParams' to pass parameters into this method.
        /// </summary>
        public void ChangeAmlDayWorkerMinBeforeShift()
        {
            #region Variable Declarations
            DXCell uIDagarbeiderCell = this.UIVacantShiftsFormWindow.UIPanelControl1Client.UIGcAgreementsTable.UIDagarbeiderCell;
            DXButton uIEndreButton = this.UIVacantShiftsFormWindow.UIPanelControl1Client.UIEndreButton;
            DXTextEdit uISdeMinFreeBeforeShifEdit = this.UIAMLavtaleWindow.UIPcAgreementClient.UIXtabCtrAgreementTabList.UIXtpLimitsClient.UIPcLimitClient.UIVhLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient.UIOffDutyLimitViewCustom.UIPanelControl1Client.UIPFreeBeforeShiftClient.UISdeMinFreeBeforeShifEdit;
            DXButton uISimpleButtonButton = this.UIAMLavtaleWindow.UISimpleButtonButton;
            //DXButton uISimpleButtonButton1 = this.UIVacantShiftsFormWindow.UISimpleButtonButton;
            #endregion

            // Click 'Dagarbeider' cell
            Mouse.Click(uIDagarbeiderCell, new Point(43, 9));

            // Click 'Endre' button
            Mouse.Click(uIEndreButton);

            SetMinBeforeShift();

        }

        /// <summary>
        /// ChangeAmlDnlf38FreeBeforeShift - Use 'ChangeAmlDnlf38FreeBeforeShiftParams' to pass parameters into this method.
        /// </summary>
        public void ChangeAmlDnlf38FreeBeforeShift()
        {
            #region Variable Declarations
            DXCell uIAMLDNLF38Cell = this.UIVacantShiftsFormWindow.UIPanelControl1Client.UIGcAgreementsTable.UIAMLDNLF38Cell;
            DXButton uIEndreButton = this.UIVacantShiftsFormWindow.UIPanelControl1Client.UIEndreButton;
            #endregion

            // Click 'AML DNLF 38' cell
            Mouse.Click(uIAMLDNLF38Cell, new Point(29, 10));

            // Click 'Endre' button
            Mouse.Click(uIEndreButton);

            SetAmlDnlf38FreeBeforeShift();
            CloseAdminAmlAgrementWindow();
        }


        /// <summary>
        /// SetAmlDnlf38FreeBeforeShift - Use 'SetAmlDnlf38FreeBeforeShiftParams' to pass parameters into this method.
        /// </summary>
        public void SetAmlDnlf38FreeBeforeShift()
        {
            #region Variable Declarations
            DXButton uIMaximizeButton = this.UIAMLavtaleWindow.UIMaximizeButton;
            DXCheckBox uIChkFreeBeforeShiftCheckBox = this.UIAMLavtaleWindow.UIViewHostDispensationCustom.UIPcViewClient.UILimitContainerViewCustom.UIXtraTabControlLimitDTabList.UIXtraTabPageDetailsClient.UIPcLimitClient.UIViewHostLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient.UIOffDutyLimitViewCustom.UIGcBreakTypesClient.UIChkFreeBeforeShiftCheckBox;
            DXTextEdit uISdeMinFreeBeforeShifEdit = this.UIAMLavtaleWindow.UIViewHostDispensationCustom.UIPcViewClient.UILimitContainerViewCustom.UIXtraTabControlLimitDTabList.UIXtraTabPageDetailsClient.UIPcLimitClient.UIViewHostLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient.UIOffDutyLimitViewCustom.UIPanelControl1Client.UIPFreeBeforeShiftClient.UISdeMinFreeBeforeShifEdit;
            DXLookUpEdit uILueOvertimeSsdLookUpEdit = this.UIAMLavtaleWindow.UIViewHostDispensationCustom.UIPcViewClient.UILimitContainerViewCustom.UIXtraTabControlLimitDTabList.UIXtraTabPageDetailsClient.UIPcLimitClient.UIViewHostLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient.UIOffDutyLimitViewCustom.UIPanelControl1Client.UIPFreeBeforeShiftClient.UILueOvertimeSsdLookUpEdit;
            DXWindow uIPopupLookUpEditFormWindow = this.UIAMLavtaleWindow.UIViewHostDispensationCustom.UIPcViewClient.UILimitContainerViewCustom.UIXtraTabControlLimitDTabList.UIXtraTabPageDetailsClient.UIPcLimitClient.UIViewHostLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient.UIOffDutyLimitViewCustom.UIPanelControl1Client.UIPFreeBeforeShiftClient.UILueOvertimeSsdLookUpEdit.UIPopupLookUpEditFormWindow;
            DXLookUpEdit uILueTurnoutSsdLookUpEdit = this.UIAMLavtaleWindow.UIViewHostDispensationCustom.UIPcViewClient.UILimitContainerViewCustom.UIXtraTabControlLimitDTabList.UIXtraTabPageDetailsClient.UIPcLimitClient.UIViewHostLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient.UIOffDutyLimitViewCustom.UIPanelControl1Client.UIPFreeBeforeShiftClient.UILueTurnoutSsdLookUpEdit;
            DXWindow uIPopupLookUpEditFormWindow1 = this.UIAMLavtaleWindow.UIViewHostDispensationCustom.UIPcViewClient.UILimitContainerViewCustom.UIXtraTabControlLimitDTabList.UIXtraTabPageDetailsClient.UIPcLimitClient.UIViewHostLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient.UIOffDutyLimitViewCustom.UIPanelControl1Client.UIPFreeBeforeShiftClient.UILueTurnoutSsdLookUpEdit.UIPopupLookUpEditFormWindow;
            DXButton uIOKButton = this.UIAMLavtaleWindow.UIOKButton;
            #endregion

            // Click 'Maximize' button
            Mouse.Click(uIMaximizeButton, new Point(13, 10));

            // Select 'chkFreeBeforeShift' check box
            uIChkFreeBeforeShiftCheckBox.Checked = this.SetAmlDnlf38FreeBeforeShiftParams.UIChkFreeBeforeShiftCheckBoxChecked;

            // Type 'System.Double' in 'sdeMinFreeBeforeShift' text box
            //ValueTypeName
            uISdeMinFreeBeforeShifEdit.ValueTypeName = this.SetAmlDnlf38FreeBeforeShiftParams.UISdeMinFreeBeforeShifEditValueTypeName;

            // Type '9' in 'sdeMinFreeBeforeShift' text box
            //ValueAsString
            uISdeMinFreeBeforeShifEdit.ValueAsString = this.SetAmlDnlf38FreeBeforeShiftParams.UISdeMinFreeBeforeShifEditValueAsString;
            Keyboard.SendKeys(uISdeMinFreeBeforeShifEdit, this.SetAmlDnlf38FreeBeforeShiftParams.UILueOvertimeTab, ModifierKeys.None);

            Playback.Wait(500);
            Mouse.Click(uILueOvertimeSsdLookUpEdit);
            Keyboard.SendKeys(uILueOvertimeSsdLookUpEdit, "{DOWN 4}{ENTER}{TAB}", ModifierKeys.None);

            Mouse.Click(uILueTurnoutSsdLookUpEdit);
            Keyboard.SendKeys(uILueTurnoutSsdLookUpEdit, "{DOWN 4}{ENTER}{TAB}", ModifierKeys.None);

            // Click 'OK' button
            Mouse.Click(uIOKButton, new Point(41, 10));
        }


        public void SelectAmlAgrement(string rowNo)
        {
            #region Variable Declarations
            DXCell agreementCell = this.UIVacantShiftsFormWindow.UIPanelControl1Client.UIGcAgreementsTable.UIAMLDNLF38Cell;
            agreementCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcAgreementsGridControlCell[View]gvAgreements[Row]" + rowNo + "[Column]colName";
            DXButton uIEndreButton = this.UIVacantShiftsFormWindow.UIPanelControl1Client.UIEndreButton;
            #endregion

            // Click cell
            Mouse.Click(agreementCell);

            // Click 'Endre' button
            Mouse.Click(uIEndreButton);
        }

        /// <summary>
        /// ChangeAmlTurnus35_5 - Use 'ChangeAmlTurnus35_5Params' to pass parameters into this method.
        /// </summary>
        public void ChangeAmlTurnus35_5(string priority)
        {
            #region Variable Declarations
            DXButton uISimpleButtonButton = this.UIAMLavtaleWindow.UISimpleButtonButton;
            #endregion

            UIMapVS2017.ChangeAmlAgreementPriority(priority);
            Mouse.Click(uISimpleButtonButton);
        }

        public void ChangeAmlDnlf38(DateTime fromDate)
        {
            #region Variable Declarations
            DXCell uIAMLDNLF38Cell = this.UIVacantShiftsFormWindow.UIPanelControl1Client.UIGcAgreementsTable.UIAMLDNLF38Cell;
            DXButton uIEndreButton = this.UIVacantShiftsFormWindow.UIPanelControl1Client.UIEndreButton;
            #endregion

            // Click 'AML DNLF 38' cell
            Mouse.Click(uIAMLDNLF38Cell, new Point(29, 10));

            // Click 'Endre' button
            Mouse.Click(uIEndreButton);

            AddNewAmlLimit(fromDate);
            CloseAdminAmlAgrementWindow();
        }



        /// <summary>
        /// AddNewAmlLimit - Use 'AddNewAmlLimitParams' to pass parameters into this method.
        /// </summary>
        public void AddNewAmlLimit(DateTime fromDate)
        {
            #region Variable Declarations
            DXButton uIMaximizeButton = this.UIAMLavtaleWindow.UIMaximizeButton;
            DXButton uINYButton = this.UIAMLavtaleWindow.UIViewHostDispensationCustom.UIPcViewClient.UILimitContainerViewCustom.UINYButton;
            DXTextEdit uISdeFromDateEdit = this.UIAMLavtaleWindow.UIViewHostDispensationCustom.UIPcViewClient.UILimitContainerViewCustom.UIXtraTabControlLimitDTabList.UIXtraTabPageDetailsClient.UIPcLimitClient.UIViewHostLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UISdeFromDateEdit;
            DXCheckBox uIChkFreeBeforeShiftCheckBox = this.UIAMLavtaleWindow.UIViewHostDispensationCustom.UIPcViewClient.UILimitContainerViewCustom.UIXtraTabControlLimitDTabList.UIXtraTabPageDetailsClient.UIPcLimitClient.UIViewHostLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient.UIOffDutyLimitViewCustom.UIGcBreakTypesClient.UIChkFreeBeforeShiftCheckBox;
            DXButton uIOKButton = this.UIAMLavtaleWindow.UIOKButton;

            //SelectNewLimit();
            //ShowAmlAgreementHistory();
            #endregion

            // Click 'Maximize' button
            Mouse.Click(uIMaximizeButton);

            // Click 'Ny' button
            Mouse.Click(uINYButton);

            UICommon.SetValidFromDateAMLagreement(fromDate);

            AmLAgrementDataC9_S3();

            // Clear 'chkFreeBeforeShift' check box
            uIChkFreeBeforeShiftCheckBox.Checked = false;

            // Type '{Tab}' in 'chkFreeBeforeShift' check box
            Keyboard.SendKeys(uIChkFreeBeforeShiftCheckBox, this.AddNewAmlLimitParams.UITab, ModifierKeys.None);

            // Click 'OK' button
            Mouse.Click(uIOKButton);
        }
        
        /// <summary>
        /// SetAmlTimerPrUke - Use 'SetAmlTimerPrUkeParams' to pass parameters into this method.
        /// </summary>
        private void SetAmlTimerPrUke()
        {
            #region Variable Declarations
            DXTextEdit uISdeWeaHoursPerWeekEdit = this.UIAMLavtaleWindow.UIPcAgreementClient.UIXtabCtrAgreementTabList.UIXtpLimitsClient.UIPcLimitClient.UIVhLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient4.UIOvertimeLimitViewCustom.UIPanelControl1Client.UIFlpWeaHoursClient.UIPWeaHoursPerWeekClient.UISdeWeaHoursPerWeekEdit;
            DXTextEdit uISdeWeaHoursPerWeekEdit1 = this.UIAMLavtaleWindow.UIPcAgreementClient.UIXtabCtrAgreementTabList.UIXtpLimitsClient.UIPcLimitClient.UIVhLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient5.UIOvertimeLimitViewCustom.UIPanelControl1Client.UIFlpWeaHoursClient.UIPWeaHoursPerWeekClient.UISdeWeaHoursPerWeekEdit;
            DXTextEdit uISdeWeaHoursPerWeekEdit2 = this.UIAMLavtaleWindow.UIPcAgreementClient.UIXtabCtrAgreementTabList.UIXtpLimitsClient.UIPcLimitClient.UIVhLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient6.UIOvertimeLimitViewCustom.UIPanelControl1Client.UIFlpWeaHoursClient.UIPWeaHoursPerWeekClient.UISdeWeaHoursPerWeekEdit;
            #endregion

            // Type 'System.Double' in 'sdeWeaHoursPerWeek' text box
            //ValueTypeName
            uISdeWeaHoursPerWeekEdit.ValueTypeName = this.SetAmlTimerPrUkeParams.UISdeWeaHoursPerWeekEditValueTypeName;

            // Type '{Tab}' in 'sdeWeaHoursPerWeek' text box
            Keyboard.SendKeys(uISdeWeaHoursPerWeekEdit1, this.SetAmlTimerPrUkeParams.UISdeWeaHoursPerWeekEditValueAsString + this.SetAmlTimerPrUkeParams.UISdeWeaHoursPerWeekEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// SetSamletTidPrUke - Use 'SetSamletTidPrUkeParams' to pass parameters into this method.
        /// </summary>
        private void SetSamletTidPrUke()
        {
            #region Variable Declarations
            DXCheckBox uIChkBreakTypeTotalHouCheckBox = this.UIAMLavtaleWindow.UIPcAgreementClient.UIXtabCtrAgreementTabList.UIXtpLimitsClient.UIPcLimitClient.UIVhLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient1.UITotalHoursLimitViewCustom.UIGroupControlBreakTypClient.UIChkBreakTypeTotalHouCheckBox;
            DXTextEdit uISdeMaxTotalHoursPerWEdit = this.UIAMLavtaleWindow.UIPcAgreementClient.UIXtabCtrAgreementTabList.UIXtpLimitsClient.UIPcLimitClient.UIVhLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient1.UITotalHoursLimitViewCustom.UIPTotalWorkClient.UIPTotalHoursPerWeekClient.UISdeMaxTotalHoursPerWEdit;
            DXTextEdit uISdeMaxTotalHoursPerWEdit1 = this.UIAMLavtaleWindow.UIPcAgreementClient.UIXtabCtrAgreementTabList.UIXtpLimitsClient.UIPcLimitClient.UIVhLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient1.UITotalHoursLimitViewCustom.UIPTotalWorkClient.UIPTotalHoursPerWeekClient1.UISdeMaxTotalHoursPerWEdit;
            //DXTextEdit uISdeMaxTotalHoursPerWEdit2 = this.UIAMLavtaleWindow.UIPcAgreementClient.UIXtabCtrAgreementTabList.UIXtpLimitsClient.UIPcLimitClient.UIVhLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UIPanelControl1Client.UIGcvhLimitDetailsCustom.UIGroupControlClient1.UITotalHoursLimitViewCustom.UIPTotalWorkClient.UIPTotalHoursPerWeekClient2.UISdeMaxTotalHoursPerWEdit;
            #endregion

            // Select 'chkBreakTypeTotalHoursPerWeek' check box
            uIChkBreakTypeTotalHouCheckBox.Checked = this.SetSamletTidPrUkeParams.UIChkBreakTypeTotalHouCheckBoxChecked;

            // Type 'System.Double' in 'sdeMaxTotalHoursPerWeek' text box
            //ValueTypeName
            uISdeMaxTotalHoursPerWEdit.ValueTypeName = this.SetSamletTidPrUkeParams.UISdeMaxTotalHoursPerWEditValueTypeName;

            // Type '{Tab}' in 'sdeMaxTotalHoursPerWeek' text box
            Keyboard.SendKeys(uISdeMaxTotalHoursPerWEdit1, this.SetSamletTidPrUkeParams.UISdeMaxTotalHoursPerWEditValueAsString + this.SetSamletTidPrUkeParams.UISdeMaxTotalHoursPerWEditSendKeys, ModifierKeys.None);
        }


        /// <summary>
        /// KillAdminAmlAgrementWindow
        /// </summary>
        public void KillAdminAmlAgrementWindow()
        {
            try
            {
                Keyboard.SendKeys("{F4}", ModifierKeys.Alt);
            }
            catch
            {
                Keyboard.SendKeys("{TAB}{ENTER}");
            }
        }

        public void CloseAdminAmlAgrementWindow()
        {           
            #region Variable Declarations
            //DXButton uIXcloseButton = this.UIVacantShiftsFormWindow.UICloseButton1;
            #endregion

            try
            {
                UIMapVS2017.ClickCloseAmlAgrementWindow();
            }
            catch (Exception)
            {
                KillAdminAmlAgrementWindow();
            }

            Playback.Wait(500);
            //UICommon.ClearAdministrationSearchString();
        }

        public void AddCarlBildt()
        {
            UICommon.ClickEmployeesButtonRosterplan();
            AddCarlBildtToATL1Plan();
        }

        /// <summary>
        /// EditEmployment - Use 'EditEmploymentParams' to pass parameters into this method.
        /// </summary>
        public void EditEmployment(DateTime toDate)
        {
            #region Variable Declarations
            DXCell uIItem2060AMLavdeling1Cell = this.UIGatver64339794ASCLAvWindow9.UIEmploymentListControCustom.UIScContentSplitContainerControl.UISplitGroupPanelClient.UIEmploymentControlCustom.UIGcEmploymentsTable.UIItem2060AMLavdeling1Cell;
            DXButton uIEndreButton = this.UIGatver64339794ASCLAvWindow9.UIEmploymentListControCustom.UIEndreButton;
            DXPopupEdit uIPceDate0PopupEdit = this.UIStillingsforholdWindow.UIPcContentClient.UIPceDate0PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UIStillingsforholdWindow.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            #endregion

            // Click '2060 - AML avdeling 1' cell
            Mouse.Click(uIItem2060AMLavdeling1Cell, new Point(76, 11));

            // Click 'Endre' button
            Mouse.Click(uIEndreButton);

            UICommon.SetEmploymentPeriod(null, toDate);

            // Click 'GSSimpleButton' button
            Playback.Wait(1000);
            uIGSSimpleButtonButton.WaitForControlReady(30000);
            Mouse.Click(uIGSSimpleButtonButton);
        }

        /// <summary>
        /// NewEmployment - Use 'NewEmploymentParams' to pass parameters into this method.
        /// </summary>
        public void NewEmployment(DateTime fromDate)
        {
            UICommon.CreateNewEmployment(fromDate, null, "40", "L38 ", "L + Lege", "AML 38t", "1234","2060","");
        }

        public void SelectMainWindowShiftBookTab()
        {
            Playback.Wait(1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
        }
        public void SelectMainWindowEmployeeTab()
        {
            Playback.Wait(1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
        }
        public void SelectMainWindowReportsTab()
        {
            Playback.Wait(1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.ReportCurrent);
        }
        public void SelectMainWindowRosterplanTab()
        {
            Playback.Wait(1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }

        public void SelectDefinedPointTab(Point point)
        {
            Playback.Wait(1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.ClickDefinedPoint, point);
        }

        public void SelectFromAdministration(string searchString)
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Administration);
            UICommon.SelectFromAdministration(searchString);
        }

        public void InsertAmlAccesscode()
        {
            UIMapVS2017.InsertAmlAccesscode();
        }        

        public void SelectRosterPlan(string planName)
        {
            Playback.Wait(1000);
            UICommon.SelectRosterPlan(planName);
        }

        public void SelectReport(string reportName, bool changeSettings)
        {
            UICommon.SelectReport(reportName, changeSettings, true);
        }
        
        /// <summary>
        /// SelectAmlBrakesEmployeeTab
        /// </summary>
        public void SelectAmlBrakesEmployeeTab()
        {
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.AmlBrakesTab, false);
        }

        /// <summary>
        /// SelectAmlDispEmployeeTab
        /// </summary>
        public void SelectAmlDispEmployeeTab()
        {
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.AmlDispTab, false);
        }

        /// <summary>
        /// SelectDayAndWeekSeparationEmployeeTab
        /// </summary>
        public void SelectDayAndWeekSeparationEmployeeTab()
        {
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.DayAndWeekSeparationEmployeeTab, false);
        }

        /// <summary>
        /// SelectAmlBrakesEmployeeTabWhenOnRow2
        /// </summary>
        public void SelectAmlBrakesEmployeeTabWhenOnRow2()
        {
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.DayAndWeekSeparationEmployeeTab, true);
        }

        public void SelectEmploymentTabFromEmployee()
        {
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.EmploymentTab, false);
        }
        
        /// <summary>
        /// SelectCalloutsEmployeeTab
        /// </summary>
        public void SelectCalloutsEmployeeTab()
        {
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.CalloutsTab, false);
        }

        public void SelectEkstraEmployeeTab()
        {
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.EkstraEmployeeTab, false);
        }

        /// <summary>
        /// SelectAmlBrakesEmployeeTabSweLineOne
        /// </summary>
        public void SelectAmlBrakesEmployeeTabSweLineOne()
        {
            UICommon.SelectAtlBrakesEmployeeTabSweLineOne();
        }

        /// <summary>
        /// SelectDayAndWeekSeparationEmployeeTabSe
        /// </summary>
        public void SelectDayAndWeekSeparationEmployeeTabSe()
        {
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.DayAndWeekSeparationEmployeeTab, false, true);
        }

        /// <summary>
        /// InsertReport77Data - Use 'InsertReport77DataParams' to pass parameters into this method.
        /// </summary>
        public void InsertReport77Data()
        {
            #region Variable Declarations
            WinEdit uIItemEdit = this.UIGatver64339794ASCLAvWindow3.UIItemWindow4.UIItemEdit;
            WinEdit uIItemEdit1 = this.UIGatver64339794ASCLAvWindow3.UIItemWindow5.UIItemEdit;
            WinEdit uIItemEdit2 = this.UIGatver64339794ASCLAvWindow3.UIItemWindow12.UIItemEdit;
            WinEdit uIItemEdit3 = this.UIGatver64339794ASCLAvWindow3.UIItemWindow13.UIItemEdit;
            WinEdit uIItemEdit4 = this.UIGatver64339794ASCLAvWindow3.UIItemWindow21.UIItemEdit;
            WinCheckBox uIVelgalleingenCheckBox = this.UIGatver64339794ASCLAvWindow3.UIItemWindow31.UIItem77Client.UIVelgalleingenCheckBox;
            #endregion

            // Type '05.09.2016' in text box
            //RosterplanStartDate.ToShortDateString()
            uIItemEdit.Text = DateRelativeToEffectuationDate.ToString("ddMMyyyy");

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit1, "{TAB}", ModifierKeys.None);

            // Type '11.09.2016' in text box
            //RosterplanEndDate.ToShortDateString()
            uIItemEdit2.Text = DateRelativeToEffectuationEndDate.ToString("ddMMyyyy");

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit3, "{TAB}", ModifierKeys.None);

            // Type '11.09.2016' in text box
            uIItemEdit4.Text = DateRelativeToEffectuationEndDate.ToString("ddMMyyyy");
            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit4, "{TAB}", ModifierKeys.None);

            // Select 'Velg alle/ingen:' check box
            uIVelgalleingenCheckBox.Checked = true;
        }

        public void InsertReport7Data(string toDate, bool checkAmlCalculations = false)
        {
            if (toDate == "")
                toDate = DateRelativeToEffectuationEndDate.ToString("ddMMyyyy");

            UICommon.AmlReport7Settings(DateRelativeToEffectuationDate.ToString("ddMMyyyy"), toDate);

            Playback.Wait(1500);
            UICommon.UIMapVS2017.CheckAmlReport7UseCalculatedAmlViolation(checkAmlCalculations);
        }

        //public void InsertReport7DataSwe()
        //{
        //    var from = GetDateRelativeToStartDate(-126).ToString("yyyyMMdd");
        //    var to = GetDateRelativeToStartDate(69).ToString("yyyyMMdd");
        //    UICommon.AmlReport7Settings(from, to);
        //    //CheckAmlReport7UseCalculatedAmlViolationSwe(checkAmlCalculations);
        //}


        //public void InsertReport77DataSwe()
        //{
        //    #region Variable Declarations
        //    WinEdit uIItemEdit = this.UIGatver64339794ASCLAvWindow3.UIItemWindow4.UIItemEdit;
        //    WinEdit uIItemEdit1 = this.UIGatver64339794ASCLAvWindow3.UIItemWindow5.UIItemEdit;
        //    WinEdit uIItemEdit2 = this.UIGatver64339794ASCLAvWindow3.UIItemWindow12.UIItemEdit;
        //    WinEdit uIItemEdit3 = this.UIGatver64339794ASCLAvWindow3.UIItemWindow13.UIItemEdit;
        //    WinEdit uIItemEdit4 = this.UIGatver64339794ASCLAvWindow3.UIItemWindow21.UIItemEdit;
        //    WinCheckBox uIVelgalleingenCheckBox = this.UIGatver64339794ASCLAvWindow3.UIItemWindow31.UIItem77Client.UIVelgalleingenCheckBox;
        //    #endregion

        //    var from = GetDateRelativeToStartDate(-126).ToString("yyyyMMdd");
        //    var to = GetDateRelativeToStartDate(69).ToString("yyyyMMdd");

        //    uIItemEdit.Text = from;

        //    // Type '{Tab}' in text box
        //    Keyboard.SendKeys(uIItemEdit1, "{TAB}", ModifierKeys.None);

        //    uIItemEdit2.Text = to;

        //    // Type '{Tab}' in text box
        //    Keyboard.SendKeys(uIItemEdit3, "{TAB}", ModifierKeys.None);

        //    uIItemEdit4.Text = to;
        //    // Type '{Tab}' in text box
        //    Keyboard.SendKeys(uIItemEdit4, "{TAB}", ModifierKeys.None);

        //    // Select 'Velg alle/ingen:' check box
        //    CheckAllNoneCheckBoxReport77Swe();
        //}

        public void GoToShiftDate(DateTime date)
        {
            UICommon.GoToShiftbookdate(date);
        }

        /// <summary>
        /// ClickDeleteShiftButton
        /// </summary>
        public List<string> ClickDeleteShiftButton()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXRibbonButtonItem uISlettRibbonBaseButtonItem = this.UIGatver64339794ASCLAvWindow4.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpMiscRibbonPageGroup.UISlettRibbonBaseButtonItem;
            DXButton uIGSSimpleButtonButton = this.UIItemWindow5.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            #endregion

            // Wait for 1 seconds for user delay between actions; Click 'Slett' RibbonBaseButtonItem
            Mouse.Click(uISlettRibbonBaseButtonItem);

            try
            {
                Playback.Wait(1000);
                if (!UIItemWindow5.Exists)
                    Mouse.Click(uISlettRibbonBaseButtonItem);
            }
            catch (Exception)
            {
                errorList.Add("Error clicking Deletebutton(Reclicked to activate)");
                Mouse.Click(uISlettRibbonBaseButtonItem);
            }

            // Wait for 1 seconds for user delay between actions; Click 'GSSimpleButton' button
            Playback.Wait(1000);
            Mouse.Click(uIGSSimpleButtonButton);

            return errorList;
        }
        public List<string> CheckRecalculationActive(string step)
        {
            var errorList = new List<string>();
            try
            {
                if (UICommon.UIMapVS2017.CheckRecalculationWindowExists())
                {
                    errorList.Add("Recalculation active: " + step);
                    UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Recalculation not active(" + step + ")");
            }

            return errorList;
        }


        /// <summary>
        /// SelectLineInDayColumnShiftBook
        /// </summary>
        public void SelectRowInDayColumnShiftBook(string rowNo)
        {
            #region Variable Declarations
            DXCell selectCell = this.UIGatver64339794ASCLAvWindow4.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIDag00167770770FalseDockPanel.UIControlContainerCustom.UIGcDayColumnTable.UICelsiusCesarCell;
            selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcDayColumnGridControlCell[View]gvDayColumn[Row]" + rowNo + "[Column]colEmployeeName";

            #endregion

            // Click 'select' cell
            Mouse.Click(selectCell);
        }

        /// <summary>
        /// SelectFirstLineInEveningColumnShiftBook
        /// </summary>
        public void SelectRowInInEveningColumnShiftBook(string rowNo)
        {
            #region Variable Declarations
            DXCell selectCell = this.UIGatver64339794ASCLAvWindow4.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIAften00167770770FalsDockPanel.UIControlContainerCustom.UIGcDayColumnTable.UIVAKANTCell;
            selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcDayColumnGridControlCell[View]gvDayColumn[Row]" + rowNo + "[Column]colEmployeeName";

            #endregion

            // Click 'VAKANT' cell
            Mouse.Click(selectCell);
        }

        /// <summary>
        /// SelectFirstLineInNightColumnShiftBook
        /// </summary>
        public void SelectRowInNightColumnShiftBook(string rowNo)
        {
            #region Variable Declarations
            DXCell selectCell = this.UIGatver64339794ASCLAvWindow4.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIContainerPanel0DockPanel.UINatttiltirsdag001677DockPanel.UIControlContainerCustom.UIGcDayColumnTable.UIBrunBosseCell;
            selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcDayColumnGridControlCell[View]gvDayColumn[Row]" + rowNo + "[Column]colEmployeeName";

            #endregion

            // Click 'Brun, Bosse' cell
            Mouse.Click(selectCell);
        }

        /// <summary>
        /// SelectLineInHomeShiftColumnShiftBook
        /// </summary>
        public void SelectRowInHomeShiftColumnShiftBook(string rowNo)
        {
            #region Variable Declarations
            DXCell selectCell = this.UIGatver64339794ASCLAvWindow4.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIContainerPanel0DockPanel.UIHjemmevakt0016777077DockPanel.UIControlContainerCustom.UIGcDayColumnTable.UIGareGastonCell;
            selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcDayColumnGridControlCell[View]gvDayColumn[Row]" + rowNo + "[Column]colEmployeeName";
            #endregion

            // Click 'Gare, Gaston' cell
            Mouse.Click(selectCell);
        }

        /// <summary>
        /// SelectFirstRowInOnCallShiftColumnShiftBook
        /// </summary>
        public void SelectRowInOnCallShiftColumnShiftBook(string rowNo)
        {
            #region Variable Declarations
            DXCell selectCell = this.UIGatver64339794ASCLAvWindow4.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIContainerPanel1DockPanel.UIBakvakt00167770770FaDockPanel.UIControlContainerCustom.UIGcDayColumnTable.UIJensenJuliusCell;
            selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcDayColumnGridControlCell[View]gvDayColumn[Row]" + rowNo + "[Column]colEmployeeName";

            #endregion

            // Click 'Jensen, Julius' cell
            Mouse.Click(selectCell);
        }
        public void EditCalloutFromEmployeeTab()
        {
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.CalloutsTab, false);

            try
            {
                EditCalloutNew();
            }
            catch (Exception)
            {
                EditCallout();
            }
        }


        /// <summary>
        /// SaveEmpList - Use 'SaveEmpListParams' to pass parameters into this method.
        /// </summary>
        public void CheckAmlEmpList(string postfix)
        {
            #region Variable Declarations
            WinClient uIAnsatteClient = this.UIGatver64339794ASCLAvWindow1.UIItemWindow.UIAnsatteClient;
            WinMenuItem uIItemMenuItem = this.UIItemWindow.UIContextMenu.UIItemMenuItem;
            //WinComboBox uIFilenameComboBox = this.UIExcelWindow.UIDetailsPanePane.UIFilenameComboBox;
            //WinButton uISaveButton = this.UIExcelWindow.UISaveWindow.UISaveButton;
            /*
                Navn	Kode
            Andersen, Andre	843216854
            Brun, Bosse	846351321
            Celsius, Cesar	98421
            Drulle, Daniel	45434
            Eriksen, Erik	853213520
            Flyndre, Fredrik	4646546
            Gare, Gaston	685135148
            Hedning, Herman	125884
            Idre, Ian	112233
            Jensen, Julius	111222
            Sist, Ansatt	ABC
            Tesla, Teodor	332211
                */
            var expectedText = "Navn\tKode\r\nAndersen, Andre\t843216854\r\nBrun, Bosse\t846351321\r\nCelsius, Cesar\t98421\r\nDrulle, Daniel\t45434\r\nEriksen, Erik\t853213520\r\nFlyndre, Fredrik\t4646546\r\nGare, Gaston\t685135148\r\nHedning, Herman\t125884\r\nIdre, Ian\t112233\r\nJensen, Julius\t111222\r\nSist, Ansatt\tABC\r\nTesla, Teodor\t332211";
            #endregion

            Clipboard.Clear();
            // Right-Click 'Ansatte' client
            Mouse.Click(uIAnsatteClient, MouseButtons.Right, ModifierKeys.None, new Point(42, 23));
            Keyboard.SendKeys(uIItemMenuItem, "{DOWN}{ENTER}");
            Playback.Wait(1000);

            expectedText = expectedText.Trim();
            var empText = Clipboard.GetText().Trim();
            Clipboard.Clear();

            //var sjekk = String.Compare(expectedText, empText, true);
            Assert.AreEqual(expectedText, empText, "Ansattelisten avviker fra forventet liste. step: " + postfix);
        }

        public void SelectLineInEmlpoyeeListEmpTabAmlBrakes(string employee)
        {
            #region Variable Declarations
            /*
            Andre
            Bosse
            Cesar
            Daniel
            Erik
            Fredrik
            Gaston
            Herman
            Ian
            Julius
            Ansatt
            Teodor
             */
            WinClient uIAnsatteClient = this.UIGatver64339794ASCLAvWindow6.UIItemWindow.UIAnsatteClient;
            #endregion

            // Click 'Ansatte' client
            if (employee == "Andre")
                Mouse.Click(uIAnsatteClient, new Point(15, 25));
            else if (employee == "Bosse")
                Mouse.Click(uIAnsatteClient, new Point(15, 45));
            else if (employee == "Cesar")
                Mouse.Click(uIAnsatteClient, new Point(15, 65));
            else if (employee == "Daniel")
                Mouse.Click(uIAnsatteClient, new Point(15, 80));
            else if (employee == "Erik")
                Mouse.Click(uIAnsatteClient, new Point(15, 95));
            else if (employee == "Fredrik")
                Mouse.Click(uIAnsatteClient, new Point(15, 115));
            else if (employee == "Gaston")
                Mouse.Click(uIAnsatteClient, new Point(15, 130));
            else if (employee == "Herman")
                Mouse.Click(uIAnsatteClient, new Point(15, 145));
            else if (employee == "Ian")
                Mouse.Click(uIAnsatteClient, new Point(15, 165));
            else if (employee == "Julius")
                Mouse.Click(uIAnsatteClient, new Point(15, 180));
            else if (employee == "Ansatt")
                Mouse.Click(uIAnsatteClient, new Point(15, 200));
            else if (employee == "Teodor")
                Mouse.Click(uIAnsatteClient, new Point(15, 215));
        }

        public List<string> SaveAmlBrakesAsXlsx(string postfix)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            WinClient uIPanel49Client = this.UIGatver64339794ASCLAvWindow5.UIItemWindow.UIPanel49Client;
            WinMenuItem uIItemMenuItem1 = this.UIItemWindow.UIContextMenu.UIItemMenuItem1;
            WinButton uIFileTabButton = this.UIBook1ExcelWindow.UIItemWindow.UIRibbonPropertyPage.UIFileTabButton;
            WinTabPage uISaveAsTabPage = this.UIBook1ExcelWindow.UIFileMenuBar.UISaveAsTabPage;
            WinButton uIBrowseButton = this.UIBook1ExcelWindow.UIItemGroup.UIBrowseButton;
            WinComboBox uIFilenameComboBox = this.UISaveAsWindow.UIDetailsPanePane.UIFilenameComboBox;
            //WinEdit uIFilenameEdit = this.UISaveAsWindow.UIItemWindow.UIFilenameEdit;
            WinButton uISaveButton = this.UISaveAsWindow.UISaveWindow.UISaveButton;
            WinButton uICloseButton = this.UIBook1ExcelWindow.UIItemWindow.UIRibbonPropertyPage.UICloseButton;
            #endregion

            Playback.Wait(1500);

            try
            {
                uIPanel49Client.WaitForControlReady(20000);
                Mouse.Click(uIPanel49Client, MouseButtons.Right, ModifierKeys.None, new Point(26, 22));
            }
            catch (Exception)
            {
                ClickforSaveAmlBrakes();
            }

            // Click menu item numbered 6 in 'Context' menu item
            if (uIItemMenuItem1.Exists)
                Mouse.Click(uIItemMenuItem1, new Point(71, 8));
            else
                ClickforSaveAmlBrakes();

            try
            {
                var fileName = ReportFilePath + ReportFileName + postfix;
                UICommon.ExportToExcel(fileName, true);
            }
            catch (Exception e)
            {
                errorList.Add("Feil ved export til excel(" + postfix + "): " + e.Message);
            }

            return errorList;
        }

        /// <summary>
        /// SaveReportAsXls - Use 'SaveReportAsXlsParams' to pass parameters into this method.
        /// </summary>
        public List<string> SaveReportAsXls(string postfix)
        {
            var errorList = new List<string>();
            try
            {
                Playback.Wait(2000);
                UIGatver64339794ASCLAvWindow3.WaitForControlReady(30000);
                Keyboard.SendKeys(UIGatver64339794ASCLAvWindow3, "x", ModifierKeys.Alt);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("x", ModifierKeys.Alt);
            }
            
            try
            {
                var fileName = ReportFilePath + ReportFileName + postfix;
                UICommon.ExportToExcel(fileName);
            }
            catch (Exception e)
            {
                errorList.Add("Feil ved export til excel(" + postfix + "): " + e.Message);
            }

            return errorList;
        }

        /// <summary>
        /// EffectuatePlan12Weeks - Use 'EffectuatePlan12WeeksParams' to pass parameters into this method.
        /// </summary>
        public List<string> EffectuatePlanForXXWeeks(DateTime from, DateTime to)
        {
            UICommon.EffectuateFullRosterplan(true);

            UICommon.ChangeEffectuationPeriodForActualLines(from, to);

            UICommon.EffectuateRosterplanLines(false);
            var timeStart = DateTime.Now;
            UICommon.CloseSalaryCalculationsWindow();
            var timeAfter = DateTime.Now;
            UICommon.CloseRosterplanFromPlanTab();
            
            return TimeLapseInSeconds(timeStart, timeAfter, "Tidsforbruk ved iverksetting");
        }

        public void DeleteExtrashiftSundayWeek1FromEmployeeTab()
        {

            SelectExtrashiftToDelete();
            DeleteExtrashiftSundayWeek1New();
        }

        public void SelectEriksenFridayWeek1()
        {
            DXCell uICelsiusCesarCell = this.UIGatver64339794ASCLAvWindow4.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIFriVikar00167770770FDockPanel.UIControlContainerCustom.UIGcFreeColumnTable.UICelsiusCesarCell;

            // Click 'Celsius, Cesar' cell
            Mouse.Click(uICelsiusCesarCell);
        }

        /// <summary>
        /// SelectExtraEmployee - Use 'SelectExtraEmployeeParams' to pass parameters into this method.
        /// </summary>
        public void SelectExtraEmployee(string searchString)
        {
            #region Variable Declarations
            DXLookUpEdit uICbEmploymentLookUpEdit = this.UIItemWindow3.UIGSPanelControlClient.UIGSNavBarControlNavBar.UINbgccEmployeeScrollableControl.UICbEmploymentLookUpEdit;
            #endregion

            Mouse.Click(uICbEmploymentLookUpEdit);
            Keyboard.SendKeys(searchString + "{ENTER}{TAB}");
        }

        /// <summary>
        /// SetDateForExtrashift - Use 'SetDateForExtrashiftParams' to pass parameters into this method.
        /// </summary>
        private void SetDateForExtrashift(DateTime date)
        {
            #region Variable Declarations
            DXPopupEdit uIPceDatePopupEdit = this.UIItemWindow3.UIGSPanelControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIGSGroupControlClient.UIPceDatePopupEdit;
            #endregion

            UICommon.SetExtraDate(date);
        }

        /// <summary>
        /// OpenAmlStatusExtra - Use 'OpenAmlStatusExtraParams' to pass parameters into this method.
        /// </summary>
        public void OpenAmlStatusExtra()
        {
            #region Variable Declarations
            DXPopupEdit uILnkDetail0PopupEdit = this.UIItemWindow3.UILnkDetail0PopupEdit;
            #endregion

            Mouse.Click(uILnkDetail0PopupEdit);
        }

        private List<string> CheckAmlWarning(string step)
        {
            var errorList = new List<string>();

            try
            {
                if (step == "_chapter2_step_2")
                    CheckAmlStatusExtraCesarFridayWeek1();
                else if (step == "_chapter2_step_3")
                    CheckAmlStatusExtraCesarSaturdayWeek1();
                else if (step == "_chapter2_step_4")
                    CheckAmlStatusExtraCesarSundayWeek1();
                else if (step == "_chapter2_step_0")
                    CheckAmlStatusExtraCesarSaturdayWeek3();
                else if (step == "_chapter5_step_5")
                    CheckAmlStatusExtraBosseSundayWeek7();
                else if (step == "_chapter8_step_4")
                    CheckAmlStatusExtraBosseModayWeek2();
                else if (step == "_chapter8_step_4_2")
                    CheckAmlStatusExtraBosseModayWeek3();
                else if (step == "_chapter8_step_4_3")
                    CheckAmlStatusExtraBosseTuesdayWeek6();
                else if (step == "_chapter8_step_4_4")
                    CheckAmlStatusExtraBosseTuesdayWeek8();
                else if (step == "_chapter8_step_4_5")
                    CheckAmlStatusExtraBosseTuesdayWeek11();
                else if (step == "_chapter9_step_5")
                    CheckAmlStatusExtraTeslaTuesdayWeek2();
                else if (step == "_chapter9_step_13")
                    CheckAmlStatusExtraTeslaTuesdayWeek8();
                else if (step == "_chapter11_SWE_step_4_2")
                    CheckAmlStatusExtraIngvarSundayC11Step4_2();
                else if (step == "_chapter11_SWE_step_6")
                    CheckAmlStatusExtraIngvarC11Step6();
                else if (step == "_chapter11_SWE_step_8")
                    CheckAmlStatusExtraIngvarC11Step8();
                else if (step == "_chapter11_SWE_step_9")
                    CheckAmlStatusExtraIngvarC11Step9();
                else if (step == "_chapter11_SWE_step_11")
                    CheckAmlStatusExtraIngvarC11Step11();
                else if (step == "_chapter11_SWE_step_14")
                    CheckAmlStatusExtraIngvarC11Step14();
                else if (step == "_chapter11_SWE_step_15")
                    CheckAmlStatusExtraIngvarC11Step15();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step" + step);
            }

            return errorList;
        }

        /// <summary>
        /// ExtraCesarFridayWeek1 - Use 'ExtraCesarFridayWeek1Params' to pass parameters into this method.
        /// </summary>
        public List<string> CreateExtraShift(string step, string employee, string shiftType, DateTime? date = null, string fromTime = "", string toTime = "", bool selectDepartment = false, bool okNew = false, bool isNew = false)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXRibbonButtonItem uIEkstraRibbonBaseButtonItem = this.UIGatver64339794ASCLAvWindow4.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIEkstraRibbonBaseButtonItem;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit = this.UIItemWindow3.UIGSPanelControlClient.UIGSNavBarControlNavBar.UINavBarGroupControlCoScrollableControl.UIGSSearchLookUpEditLookUpEdit;
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow3.UIGSPanelControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIGSGroupControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIGSLookUpEditLookUpEdit;
            DXButton uIAvbrytButton = this.UIItemWindow3.UIGsPanelControl1Client.UIAvbrytButton;
            #endregion

          
            if(!isNew)
             Mouse.Click(uIEkstraRibbonBaseButtonItem);

            if (CultureInfo.CurrentCulture.Name == "sv-SE")
            {
                try
                {
                    Mouse.Click(uIGSSearchLookUpEditLookUpEdit);
                    Keyboard.SendKeys("8{ENTER}");
                }
                catch (Exception)
                {
                    Keyboard.SendKeys(uIGSSearchLookUpEditLookUpEdit, "{DOWN 13}{ENTER}");
                }
            }
            else
            {
                // Type 'Gatsoft.Gat.DataModel.OvertimeCode' in 'GSSearchLookUpEdit' LookUpEdit
                //ValueTypeName
                uIGSSearchLookUpEditLookUpEdit.ValueTypeName = this.ExtraCesarFridayWeek1Params.UIGSSearchLookUpEditLookUpEditValueTypeName;

                // Type 'OvertimeCode(Id=242481501)' in 'GSSearchLookUpEdit' LookUpEdit
                //ValueAsString
                uIGSSearchLookUpEditLookUpEdit.ValueAsString = this.ExtraCesarFridayWeek1Params.UIGSSearchLookUpEditLookUpEditValueAsString;
            }


            Keyboard.SendKeys(uIGSSearchLookUpEditLookUpEdit, "{TAB}");

            if (employee != "")
                SelectExtraEmployee(employee);

            if (date != null)
                SetDateForExtrashift(date.Value);

            if (shiftType != "")
            {
                if (selectDepartment)
                    if (step == "_chapter9_step_13")
                        SetExtraDepartment2060();
                    else
                        SetExtraDepartment();

                Mouse.Click(uIGSLookUpEditLookUpEdit);
                Keyboard.SendKeys(uIGSLookUpEditLookUpEdit, shiftType + "{ENTER}{TAB}");
            }
            else
            {
                if (step == "_chapter9_step_5")
                    SetExtraDepartment2060();
                else
                    SetExtraDepartment();

                SetExtraPeriod(fromTime, toTime);
            }


            if (step != "")
            {
                OpenAmlStatusExtra();
                errorList.AddRange(CheckAmlWarning(step));
                ClickReturnToExtraRegistration();
            }

            if (!okNew)
                UICommon.UIMapVS2017.ClickOkInExtraWindow();
            else
                UICommon.UIMapVS2017.ClickOkNewInExtraWindow();

            try
            {
                ApproveAmlWarningDialogExtra();
            }
            catch (Exception)
            {
                TestContext.WriteLine("AML Brudd - Egen kommentar dialog ikke vist");
            }
            
            return errorList;
        }

        public void SetExtraDepartment2060()
        {
            #region Variable Declarations
            DXButton uIEditorButton1Button = this.UIItemWindow3.UIGSPanelControlClient.UIGSNavBarControlNavBar.UINavBarGroupControlCoScrollableControl1.UIPceDepartmentPopupEdit.UIEditorButton1Button;
            DXTreeList uITlDepartmentsTreeList = this.UIVelgavdelingWindow.UITlDepartmentsTreeList;
            //DXButton uIOKButton = this.UIVelgavdelingWindow.UIOKButton;
            #endregion

            // Click 'EditorButton1' button
            Mouse.Click(uIEditorButton1Button, new Point(7, 10));

            // Type '{Down}' in 'tlDepartments' TreeList
            Keyboard.SendKeys(uITlDepartmentsTreeList, "{ENTER}", ModifierKeys.None);

            // Click 'Ok' button
            //Mouse.Click(uIOKButton);
        }
        public void SetExtraDepartment()
        {
            #region Variable Declarations
            DXButton uIEditorButton1Button = this.UIItemWindow3.UIGSPanelControlClient.UIGSNavBarControlNavBar.UINavBarGroupControlCoScrollableControl1.UIPceDepartmentPopupEdit.UIEditorButton1Button;
            DXTreeList uITlDepartmentsTreeList = this.UIVelgavdelingWindow.UITlDepartmentsTreeList;
            DXButton uIOKButton = this.UIVelgavdelingWindow.UIOKButton;
            #endregion

            // Click 'EditorButton1' button
            Mouse.Click(uIEditorButton1Button, new Point(7, 10));

            // Type '{Down}' in 'tlDepartments' TreeList
            Keyboard.SendKeys(uITlDepartmentsTreeList, "{DOWN}", ModifierKeys.None);

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }

        /// <summary>
        /// SetExtraDepartmentAndPeriod - Use 'SetExtraDepartmentAndPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetExtraPeriod(string fromTime, string toTime)
        {
            #region Variable Declarations
            DXTextEdit uIETime3Edit = this.UIItemWindow3.UIGSPanelControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIGSGroupControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIETime3Edit;
            DXTextEdit uIETime3Edit1 = this.UIItemWindow3.UIGSPanelControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIGSGroupControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIETime3Edit1;
            DXTextEdit uIETime1Edit = this.UIItemWindow3.UIGSPanelControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIGSGroupControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIETime1Edit;
            DXTextEdit uIETime1Edit1 = this.UIItemWindow3.UIGSPanelControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIGSGroupControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIETime1Edit1;
            DXLookUpEdit uICbCrewColumnLookUpEdit = this.UIItemWindow3.UIGSPanelControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UIGSGroupControlClient.UIGSTabControlTabList.UIXtraTabPageClient.UICbCrewColumnLookUpEdit;
            #endregion


            // Type '09.59 [SelectionStart]0[SelectionLength]5' in 'eTime[3]' text box
            //ValueAsString
            if (fromTime != "")
            {
                uIETime3Edit.ValueAsString = fromTime;

                // Type '{Tab}' in 'eTime[3]' text box
                Keyboard.SendKeys(uIETime3Edit1, "{Tab}", ModifierKeys.None);
            }

            // Type '15.00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
            //ValueAsString
            if (toTime != "")
            {
                uIETime1Edit.ValueAsString = toTime;

                // Type '{Tab}' in 'eTime[1]' text box
                Keyboard.SendKeys(uIETime1Edit1, "{Tab}", ModifierKeys.None);
            }


            Mouse.Click(uICbCrewColumnLookUpEdit);

            Keyboard.SendKeys(uICbCrewColumnLookUpEdit, "{DOWN}{ENTER}");
        }


        /// <summary>
        /// ApproveAmlWarningDialogExtra - Use 'ApproveAmlWarningDialogExtraParams' to pass parameters into this method.
        /// </summary>
        public void ApproveAmlWarningDialogExtra()
        {
            #region Variable Declarations
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow4.UIGSLookUpEditLookUpEdit;
            DXTextEdit uIECommentEdit = this.UIItemWindow4.UIECommentEdit;
            DXButton uIOKButton = this.UIItemWindow4.UIOKButton;
            #endregion

            if (uIGSLookUpEditLookUpEdit.Enabled == true)
            {
                // Type 'System.String' in 'GSLookUpEdit' LookUpEdit
                //ValueTypeName
                uIGSLookUpEditLookUpEdit.ValueTypeName = this.ApproveAmlWarningDialogExtraParams.UIGSLookUpEditLookUpEditValueTypeName;

                // Type 'MENGDE' in 'GSLookUpEdit' LookUpEdit
                //ValueAsString
                uIGSLookUpEditLookUpEdit.ValueAsString = this.ApproveAmlWarningDialogExtraParams.UIGSLookUpEditLookUpEditValueAsString;

                // Type '{Enter}{Tab}' in 'GSLookUpEdit' LookUpEdit
                Keyboard.SendKeys(uIGSLookUpEditLookUpEdit, this.ApproveAmlWarningDialogExtraParams.UIGSLookUpEditLookUpEditSendKeys, ModifierKeys.None);
            }

            // Type 'Test 55 Aml, Extra.' in 'eComment' text box
            //ValueAsString
            uIECommentEdit.ValueAsString = this.ApproveAmlWarningDialogExtraParams.UIECommentEditValueAsString;

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }


        /// <summary>
        /// AddCarlBildtShifts - Use 'AddCarlBildtShiftsParams' to pass parameters into this method.
        /// </summary>
        public void AddCarlBildtShifts()
        {
            #region Variable Declarations
            //// uIRedigeraRibbonBaseButtonItem = this.UIArbeidsplanATLtest1PWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRibbonPageGroup9RibbonPageGroup.UIRedigeraRibbonBaseButtonItem;
            DXCell uIItemCell = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            DXCell uIItemCell1 = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell1;
            DXTextEdit uIRow9ColumnRosterCellEdit = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow9ColumnRosterCellEdit;
            DXButton uIJAButton = this.UIGT4003InformationWindow.UIJAButton;
            DXCell uIItemCell2 = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell2;
            DXCell uIItemCell3 = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell3;
            DXTextEdit uIRow9ColumnRosterCellEdit1 = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow9ColumnRosterCellEdit1;
            DXCell uIItemCell4 = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell4;
            DXCell uIItemCell5 = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell5;
            DXTextEdit uIRow9ColumnRosterCellEdit2 = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow9ColumnRosterCellEdit2;
            DXCell uIItemCell6 = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell6;
            DXCell uIItemCell7 = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell7;
            DXTextEdit uIRow9ColumnRosterCellEdit3 = this.UIArbeidsplanATLtest1RWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow9ColumnRosterCellEdit3;
            DXButton uIGSSimpleButtonButton = this.UIGT4003InformationWindow.UIGSSimpleButtonButton;
            #endregion

            // Click 'Redigera' RibbonBaseButtonItem
            //Mouse.Click(uIRedigeraRibbonBaseButtonItem, new Point(25, 28));

            UICommon.ClickEditRosterPlanFromPlantab();

            // Move cell to cell
            uIItemCell1.EnsureClickable();
            Mouse.StartDragging(uIItemCell);
            Mouse.StopDragging(uIItemCell1);

            // Type 'e1' in '[Row]9[Column]RosterCell_0' text box
            //ValueAsString
            Keyboard.SendKeys(uIRow9ColumnRosterCellEdit, this.AddCarlBildtShiftsParams.UIRow9ColumnRosterCellEditValueAsString, ModifierKeys.None);
            // Type '{Tab}' in '[Row]9[Column]RosterCell_0' text box
            Keyboard.SendKeys(uIRow9ColumnRosterCellEdit, this.AddCarlBildtShiftsParams.UIRow9ColumnRosterCellEditSendKeys, ModifierKeys.None);

            // Click '&Ja' button
            Mouse.Click(uIJAButton);

            // Move cell to cell
            uIItemCell3.EnsureClickable();
            Mouse.StartDragging(uIItemCell2);
            Mouse.StopDragging(uIItemCell3);

            // Type 'e1' in '[Row]9[Column]RosterCell_7' text box
            //ValueAsString
            Keyboard.SendKeys(uIRow9ColumnRosterCellEdit1, this.AddCarlBildtShiftsParams.UIRow9ColumnRosterCellEdit1ValueAsString);

            // Type '{Tab}' in '[Row]9[Column]RosterCell_7' text box
            Keyboard.SendKeys(uIRow9ColumnRosterCellEdit1, this.AddCarlBildtShiftsParams.UIRow9ColumnRosterCellEdit1SendKeys, ModifierKeys.None);

            // Click '&Ja' button
            Mouse.Click(uIJAButton);

            // Move cell to cell
            uIItemCell5.EnsureClickable();
            Mouse.StartDragging(uIItemCell4);
            Mouse.StopDragging(uIItemCell5);

            // Type 'e1' in '[Row]9[Column]RosterCell_14' text box
            //ValueAsString
            Keyboard.SendKeys(uIRow9ColumnRosterCellEdit2, this.AddCarlBildtShiftsParams.UIRow9ColumnRosterCellEdit2ValueAsString);

            // Type '{Tab}' in '[Row]9[Column]RosterCell_14' text box
            Keyboard.SendKeys(uIRow9ColumnRosterCellEdit2, this.AddCarlBildtShiftsParams.UIRow9ColumnRosterCellEdit2SendKeys, ModifierKeys.None);

            // Click '&Ja' button
            Mouse.Click(uIJAButton);

            // Move cell to cell
            uIItemCell7.EnsureClickable();
            Mouse.StartDragging(uIItemCell6);
            Mouse.StopDragging(uIItemCell7);

            // Type 'e1' in '[Row]9[Column]RosterCell_21' text box
            //ValueAsString
            Keyboard.SendKeys(uIRow9ColumnRosterCellEdit3, this.AddCarlBildtShiftsParams.UIRow9ColumnRosterCellEdit3ValueAsString);

            // Type '{Tab}' in '[Row]9[Column]RosterCell_21' text box
            Keyboard.SendKeys(uIRow9ColumnRosterCellEdit3, this.AddCarlBildtShiftsParams.UIRow9ColumnRosterCellEdit3SendKeys, ModifierKeys.None);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);

            ClickOkEditRosterplan();
        }

        /// <summary>
        /// ConstructAbsence - Use 'ConstructAbsenceParams' to pass parameters into this method.
        /// </summary>
        public void CreateAbsence(string absenceType, string emlployee = "", DateTime? fromDate = null, DateTime? toDate = null)
        {
            #region Variable Declarations
            DXRibbonButtonItem uIFraværRibbonBaseButtonItem = this.UIGatver64339794ASCLAvWindow4.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIFraværRibbonBaseButtonItem;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINavBarGroupControlCoScrollableControl.UIGSSearchLookUpEditLookUpEdit;
            //DXGrid uIGridControlTable = this.UIItemWindow6.UIGSNavBarControlNavBar.UINavBarGroupControlCoScrollableControl.UIGSSearchLookUpEditLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILciGridLayoutControlItem.UIGridControlTable;
            //DXPopupEdit uIPceDate0PopupEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINgbcPeriodScrollableControl.UIPnlPeriodClient.UILcPeriodCustom.UIPceDate0PopupEdit;
            //DXPopupEdit uIPceDate1PopupEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINgbcPeriodScrollableControl.UIPnlPeriodClient.UILcPeriodCustom.UIPceDate1PopupEdit;
            DXLookUpEdit uILueEmploymentsLookUpEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINgbcEmployeesScrollableControl.UILueEmploymentsLookUpEdit;
            DXTextEdit uITeFindEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINgbcEmployeesScrollableControl.UILueEmploymentsLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            #endregion

            // Click 'Fravær' RibbonBaseButtonItem
            uIFraværRibbonBaseButtonItem.WaitForControlReady(30000);
            Mouse.Click(uIFraværRibbonBaseButtonItem);

            Mouse.Click(uIGSSearchLookUpEditLookUpEdit);
            Keyboard.SendKeys(absenceType + "{ENTER}{TAB}");

            // Type 'EMPLOYEE' in 'teFind' text box
            if (emlployee != "")
            {
                Mouse.Click(uILueEmploymentsLookUpEdit);
                Keyboard.SendKeys(uITeFindEdit, emlployee + "{ENTER}", ModifierKeys.None);
            }

            UICommon.UIMapVS2017.SetAbsencePeriod(fromDate, toDate);
            UICommon.UIMapVS2017.ClickOkConstuctAbsence();
            try
            {
                if (UISletteforskyvningeroWindow.Exists)
                {
                    ClickOkWarningAbsenceDialog();
                    UICommon.UIMapVS2017.ClickOkConstuctAbsence();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Slette forskyvninger og ekstravakter dialog ikke vist");
            }

            Playback.Wait(1500);
        }

        public void CreateHourlyAbsence(string absenceType, string fromTime, string toTime)
        {
            #region Variable Declarations
            DXRibbonButtonItem uIFraværRibbonBaseButtonItem = this.UIGatver64339794ASCLAvWindow4.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIFraværRibbonBaseButtonItem;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINavBarGroupControlCoScrollableControl.UIGSSearchLookUpEditLookUpEdit;
            //DXLookUpEdit uILueEmploymentsLookUpEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINgbcEmployeesScrollableControl.UILueEmploymentsLookUpEdit;
            //DXCheckBox uIChbPabsHourlyAbsenceCheckBox = this.UIItemWindow6.UILcMainCustom.UIRootLayoutGroup.UILciMainInformationLayoutControlItem.UIGrcInformationClient.UINbcInformationNavBar.UINbgcHourAbsenceScrollableControl.UIPnlHourAbsenceClient.UILcHourAbsenceCustom.UILayoutControlGroup3LayoutGroup.UILayoutControlItem2LayoutControlItem.UIChbPabsHourlyAbsenceCheckBox;
            //DXTextEdit uIETime0Edit = this.UIItemWindow6.UILcMainCustom.UIRootLayoutGroup.UILciMainInformationLayoutControlItem.UIGrcInformationClient.UINbcInformationNavBar.UINbgcHourAbsenceScrollableControl.UIPnlHourAbsenceClient.UILcHourAbsenceCustom.UIETime0Edit;
            //DXTextEdit uIETime1Edit = this.UIItemWindow6.UILcMainCustom.UIRootLayoutGroup.UILciMainInformationLayoutControlItem.UIGrcInformationClient.UINbcInformationNavBar.UINbgcHourAbsenceScrollableControl.UIPnlHourAbsenceClient.UILcHourAbsenceCustom.UIETime1Edit;
            #endregion

            // Click 'Fravær' RibbonBaseButtonItem
            Mouse.Click(uIFraværRibbonBaseButtonItem);

            Mouse.Click(uIGSSearchLookUpEditLookUpEdit);
            Keyboard.SendKeys(absenceType + "{ENTER}{TAB}");

            UICommon.UIMapVS2017.HourlyAbsence(fromTime, toTime, "", "");
            UICommon.UIMapVS2017.ClickOkConstuctAbsence();
            
            //// Select 'chbPabsHourlyAbsence' check box
            //uIChbPabsHourlyAbsenceCheckBox.Checked = this.ConstructAbsenceParams.UIChbPabsHourlyAbsenceCheckBoxChecked;

            ////uIETime0Edit.ValueAsString = fromTime;
            //Playback.Wait(1000);
            //Keyboard.SendKeys(uIETime0Edit, fromTime + "{TAB}");

            ////uIETime1Edit.ValueAsString = toTime;
            //Playback.Wait(1000);
            //Keyboard.SendKeys(uIETime1Edit, toTime + "{TAB}");

            //// Click 'GSSimpleButton' button
            //Playback.Wait(1000);
            //Mouse.Click(uIGSSimpleButtonButton);
            //Playback.Wait(1500);
        }

        public void CreateGradedAbsence(string absenceType, string emlployee = "", DateTime? fromDate = null, DateTime? toDate = null)
        {
            #region Variable Declarations
            DXRibbonButtonItem uIFraværRibbonBaseButtonItem = this.UIGatver64339794ASCLAvWindow4.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIFraværRibbonBaseButtonItem;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINavBarGroupControlCoScrollableControl.UIGSSearchLookUpEditLookUpEdit;
            DXPopupEdit uIPceDate0PopupEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINgbcPeriodScrollableControl.UIPnlPeriodClient.UILcPeriodCustom.UIPceDate0PopupEdit;
            DXPopupEdit uIPceDate1PopupEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINgbcPeriodScrollableControl.UIPnlPeriodClient.UILcPeriodCustom.UIPceDate1PopupEdit;
            DXLookUpEdit uILueEmploymentsLookUpEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINgbcEmployeesScrollableControl.UILueEmploymentsLookUpEdit;
            DXTextEdit uITeFindEdit = this.UIItemWindow6.UIGSNavBarControlNavBar.UINgbcEmployeesScrollableControl.UILueEmploymentsLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            #endregion

            // Click 'Fravær' RibbonBaseButtonItem
            uIFraværRibbonBaseButtonItem.WaitForControlReady(30000);
            Mouse.Click(uIFraværRibbonBaseButtonItem);

            Mouse.Click(uIGSSearchLookUpEditLookUpEdit);
            Keyboard.SendKeys(absenceType + "{ENTER}{TAB}");

            // Type 'EMPLOYEE' in 'teFind' text box
            if (emlployee != "")
            {
                Mouse.Click(uILueEmploymentsLookUpEdit);
                Keyboard.SendKeys(uITeFindEdit, emlployee + "{ENTER}", ModifierKeys.None);
            }

            UICommon.UIMapVS2017.SetAbsencePeriod(fromDate, toDate);

            SetGradedData();

            UICommon.UIMapVS2017.ClickOkConstuctAbsence();
            try
            {
                if (UISletteforskyvningeroWindow.Exists)
                {
                    ClickOkWarningAbsenceDialog();
                    UICommon.UIMapVS2017.ClickOkConstuctAbsence();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Slette forskyvninger og ekstravakter dialog ikke vist");
            }

            Playback.Wait(1500);
        }

        /// <summary>
        /// SetGradedData - Use 'SetGradedDataParams' to pass parameters into this method.
        /// </summary>
        public void SetGradedData()
        {
            #region Variable Declarations
            DXButton uIEditorButton1Button = this.UIEndrevakterWindow.UIGsPanelControl1Client.UIGcShiftEditClient.UIPnlShiftEditScrollableControl.UICbShiftCodesLookUpEdit.UIEditorButton1Button;
            DXTextEdit uIETime1Edit = this.UIEndrevakterWindow.UIGsPanelControl1Client.UIGcShiftEditClient.UIPnlShiftEditScrollableControl.UIETime1Edit;
            DXButton uIGSSimpleButtonButton = this.UIEndrevakterWindow.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            DXCell uIItem2060AMLavdeling1Cell = this.UIEndrevakterWindow.UIGsPanelControl1Client.UIGcShiftListClient.UIGcShiftsTable.UIItem2060AMLavdeling1Cell;
            #endregion

            UICommon.UIMapVS2017.AbsenceRate("50");

            UIMapVS2017.SelectAbsenceShiftsForRating_1();


            // Click 'Endre vakt(er)' button
            UICommon.UIMapVS2017.ClickChangeAbsenceShiftsButton();

            // Click 'EditorButton1' button
            Mouse.Click(uIEditorButton1Button);

            // Type '13.00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
            //ValueAsString
            uIETime1Edit.ValueAsString = this.SetGradedDataParams.UIETime1EditValueAsString;
            //Keyboard.SendKeys(uIETime1Edit, this.SetGradedDataParams.UIETime1EditValueAsString, ModifierKeys.None);'
            Keyboard.SendKeys(uIETime1Edit, this.SetGradedDataParams.UITab, ModifierKeys.None);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);

            UIMapVS2017.SelectAbsenceShiftsForRating_2();

            // Click 'Endre vakt(er)' button
            UICommon.UIMapVS2017.ClickChangeAbsenceShiftsButton();

            // Click '2060 - AML avdeling 1' cell
            Mouse.Click(uIItem2060AMLavdeling1Cell);

            // Click 'EditorButton1' button
            Mouse.Click(uIEditorButton1Button);

            // Type '12.00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
            //ValueAsString
            uIETime1Edit.ValueAsString = this.SetGradedDataParams.UIETime1EditValueAsString1;
            //Keyboard.SendKeys(uIETime1Edit, this.SetGradedDataParams.UIETime1EditValueAsString1, ModifierKeys.None);
            Keyboard.SendKeys(uIETime1Edit, this.SetGradedDataParams.UITab, ModifierKeys.None);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        /// <summary>
        /// CheckAmlSettingsAdjustDaySeparation - Use 'CheckAmlSettingsAdjustDaySeparationParams' to pass parameters into this method.
        /// </summary>
        public void CheckAmlSettingsAdjustDaySeparation(bool justerDaySeparation)
        {
            #region Variable Declarations
            WinRadioButton uIJusterdøgnskilletilvRadioButton = this.UIGlobaltoppsettWindow.UIDøgnskilleClient.UIJusterdøgnskilletilvRadioButton;
            WinRadioButton uIBeholdvalgtdøgnskillRadioButton = this.UIGlobaltoppsettWindow.UIDøgnskilleClient.UIBeholdvalgtdøgnskillRadioButton;
            #endregion

            if (justerDaySeparation)
                uIJusterdøgnskilletilvRadioButton.Selected = true;
            else
                uIBeholdvalgtdøgnskillRadioButton.Selected = true;

            Keyboard.SendKeys("{TAB}");
        }

        /// <summary>
        /// CheckAmlSettingsUseOrgShiftExchange - Use 'CheckAmlSettingsUseOrgShiftExchangeParams' to pass parameters into this method.
        /// </summary>
        public void CheckAmlSettingsUseOrgShiftExchange(bool useOrgShift)
        {
            #region Variable Declarations
            WinRadioButton uIBrukoriginalvaktsomgRadioButton = this.UIGlobaltoppsettWindow.UIBytteClient.UIBrukoriginalvaktsomgRadioButton;
            WinRadioButton uIBrukaktivvaktsomgrunRadioButton = this.UIGlobaltoppsettWindow.UIBytteClient.UIBrukaktivvaktsomgrunRadioButton;
            #endregion

            if (useOrgShift)
                uIBrukoriginalvaktsomgRadioButton.Selected = true;
            else
                uIBrukaktivvaktsomgrunRadioButton.Selected = true;

            Keyboard.SendKeys("{TAB}");
        }

        /// <summary>
        /// CheckAmlSettingsUseOrgShiftDepartmentExchange - Use 'CheckAmlSettingsUseOrgShiftDepartmentExchangeParams' to pass parameters into this method.
        /// </summary>
        public void CheckAmlSettingsUseOrgShiftDepartmentExchange(bool useOrgShift)
        {
            #region Variable Declarations
            WinRadioButton uIBrukoriginalvaktsomgRadioButton1 = this.UIGlobaltoppsettWindow.UIZUP_WEA_ORIGINAL_DEPClient.UIBrukoriginalvaktsomgRadioButton;
            WinRadioButton uIBrukaktivvaktsomgrunRadioButton1 = this.UIGlobaltoppsettWindow.UIZUP_WEA_ORIGINAL_DEPClient.UIBrukaktivvaktsomgrunRadioButton;
            #endregion

            if (useOrgShift)
                uIBrukoriginalvaktsomgRadioButton1.Selected = true;
            else
                uIBrukaktivvaktsomgrunRadioButton1.Selected = true;

            Keyboard.SendKeys("{TAB}");
        }

        public List<string> CreateRemannageShift(string step, string shiftRow, DateTime? dateForCurrentShiftFromDate = null, string shiftType = "", DateTime? dateForNewShift = null)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXRibbonButtonItem uIForskjøvetvaktRibbonBaseButtonItem = this.UIGatver64339794ASCLAvWindow4.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIForskjøvetvaktRibbonBaseButtonItem;
            DXLookUpEdit uILeOvertimeCodeLookUpEdit = this.UIForskyvningWindow.UIPanClientPanelClient.UIBnMenuNavBar.UINbgcOvertimeCodeScrollableControl.UILeOvertimeCodeLookUpEdit;
            DXWindow uIPopupLookUpEditFormWindow = this.UIForskyvningWindow.UIPanClientPanelClient.UIBnMenuNavBar.UINbgcOvertimeCodeScrollableControl.UILeOvertimeCodeLookUpEdit.UIPopupLookUpEditFormWindow;
            DXButton uIVelgledigevakterButton = this.UIForskyvningWindow.UIPanClientPanelClient.UIGpcMainClient.UITcClientTabList.UITpMainClient.UIGpcShiftsClient.UIGcNewShiftsClient.UIVelgledigevakterButton;
            DXButton uIGSSimpleButtonButton = this.UIItemWindow7.UIGsLayoutControl1Custom.UIGSSimpleButtonButton;
            DXButton uILønnButton = this.UIForskyvningWindow.UIPanBottomPanelClient.UILønnButton;
            DXPopupEdit uILnkDetail0PopupEdit = this.UIForskyvningWindow.UIPanHeaderPanelClient.UILnkDetail0PopupEdit;
            #endregion

            // Click 'Forskjøvet
            //vakt' RibbonBaseButtonItem
            Mouse.Click(uIForskjøvetvaktRibbonBaseButtonItem, new Point(28, 22));

            // Type 'OvertimeCode(Id=242481501)' in 'leOvertimeCode' LookUpEdit
            //ValueAsString
            uILeOvertimeCodeLookUpEdit.ValueAsString = this.ConstructRemanageShiftParams.UILeOvertimeCodeLookUpEditValueAsString;

            // Click 'PopupLookUpEditForm' window
            Mouse.Click(uIPopupLookUpEditFormWindow, new Point(46, 28));

            if (dateForNewShift != null && shiftType != "")
                ConstuctNewRemanageShift(dateForNewShift.Value, shiftType); //"18.10.2016", "N{SPACE}");
            else
            {
                // Click '&Velg ledige vakter' button
                Mouse.Click(uIVelgledigevakterButton);
                SelectFreeShiftToRemanning(step, shiftRow, dateForCurrentShiftFromDate);
                // Click 'GSSimpleButton' button
                Mouse.Click(uIGSSimpleButtonButton);
            }

            // Click 'lønn' button
            Playback.Wait(1500);
            Mouse.Click(uILønnButton);

            //Åpne Type '2 brudd (klikk her for detaljer)' PopupEdit
            //ValueAsString
            Mouse.Click(uILnkDetail0PopupEdit);

            try
            {

                if (step == "_chapter3_step_8")
                {
                    CheckAmlStatusRemanageBosseWeek5Monday();
                }
                else if (step == "_chapter3_step_8_2")
                {
                    CheckAmlStatusRemanageBosseWeek5Tuesday();
                }
                else if (step == "_chapter4_step_1")
                {
                    CheckAmlStatusRemanageCesarWeek10Friday();
                }
                else if (step == "_chapter4_step_2")
                {
                    CheckAmlStatusRemanageCesarWeek10Monday();
                }
                else if (step == "_chapter4_step_3")
                {
                    CheckAmlStatusRemanageCesarWeek10Saturday();
                }
                else if (step == "_chapter4_step_4")
                {
                    CheckAmlStatusRemanageCesarWeek10FridayDSH();
                }
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(" + step + ")");
            }

            ClickOkRemanning();
            Playback.Wait(1500);
            return errorList;


        }

        /// <summary>
        /// ConstuctNewRemanageShift - Use 'ConstuctNewRemanageShiftParams' to pass parameters into this method.
        /// </summary>
        public void ConstuctNewRemanageShift(DateTime date, string shiftType)
        {
            #region Variable Declarations
            DXPopupEdit uIPceDatePopupEdit = this.UIForskyvningWindow.UIPanClientPanelClient.UIGpcMainClient.UITcClientTabList.UITpMainClient.UIGpcShiftsClient.UIGcNewShiftsClient.UITcNewShiftTabList.UITpSimpleShiftCtrlClient.UIMainPanelClient.UIPceDatePopupEdit;
            DXLookUpEdit uICbShiftCodeLookUpEdit = this.UIForskyvningWindow.UIPanClientPanelClient.UIGpcMainClient.UITcClientTabList.UITpMainClient.UIGpcShiftsClient.UIGcNewShiftsClient.UITcNewShiftTabList.UITpSimpleShiftCtrlClient.UIMainPanelClient.UICbShiftCodeLookUpEdit;
            //DXWindow uIPopupLookUpEditFormWindow = this.UIForskyvningWindow.UIPanClientPanelClient.UIGpcMainClient.UITcClientTabList.UITpMainClient.UIGpcShiftsClient.UIGcNewShiftsClient.UITcNewShiftTabList.UITpSimpleShiftCtrlClient.UIMainPanelClient.UICbShiftCodeLookUpEdit.UIPopupLookUpEditFormWindow;
            #endregion



            UICommon.SetNewRemanageShiftDate(date);

            if (shiftType != "")
            {
                Mouse.Click(uICbShiftCodeLookUpEdit);
                Keyboard.SendKeys(uICbShiftCodeLookUpEdit, shiftType + "{ENTER}{TAB}");
            }
        }

        public void SelectLineInAbsenceColumnShiftBook(string lineNo)
        {
            #region Variable Declarations
            DXCell shiftRowToDelete = this.UIGatver64339794ASCLAvWindow4.UIBottomPanelDockPanel.UIFraværDockPanel.UIControlContainerCustom.UIGcAbsenceColumnTable.UIItem10EgenmeldingIAbCell;
            shiftRowToDelete.SearchProperties[DXTestControl.PropertyNames.Name] = "gcAbsenceColumnGridControlCell[View]gvAbsenceColumn[Row]" + lineNo + "[Column]gccAbsenceCode";

            #endregion

            // Click 'abcence' cell
            Mouse.Click(shiftRowToDelete);
        }

        /// <summary>
        /// SelectFreeShiftToRemanning - Use 'SelectFreeShiftToRemanningParams' to pass parameters into this method.
        /// </summary>
        public void SelectFreeShiftToRemanning(string step, string shiftRow, DateTime? dateForCurrentShiftFromDate = null)
        {
            #region Variable Declarations
            DXCell selectShiftCell = this.UIItemWindow7.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem3LayoutControlItem.UIGUnoccupiedShiftsTable.UIIkkevalgtCell;
            DXCheckBox selectShiftCheckBox = this.UIItemWindow7.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem3LayoutControlItem.UIGUnoccupiedShiftsTable.UICheckEditCheckBox;

            selectShiftCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gUnoccupiedShiftsGridControlCell[View]gridView1[Row]" + shiftRow + "[Column]gcSeleced";
            selectShiftCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "gUnoccupiedShiftsCheckEdit[View]gridView1[Row]" + shiftRow + "[Column]gcSeleced";
            #endregion


            if (dateForCurrentShiftFromDate != null)
                SetDatePeriodInShiftListControl(dateForCurrentShiftFromDate, null);

            // Click 'Ikke valgt' cell 
            Mouse.Click(selectShiftCell);

            // Select 'CheckEdit' check box
            selectShiftCheckBox.Checked = this.SelectFreeShiftToRemanningParams.UICheckEditCheckBox2Checked;
        }

        /// <summary>
        /// SetDatePeriodInShiftListControl - Use 'SetDatePeriodInShiftListControlParams' to pass parameters into this method.
        /// </summary>
        public void SetDatePeriodInShiftListControl(DateTime? fromDate, DateTime? toDate)
        {
            UICommon.SetFreeVacantShiftsPeriod(fromDate, toDate);
        }

        /// <summary>
        /// ClickOkRemaning - Use 'ClickOkRemaningParams' to pass parameters into this method.
        /// </summary>
        public void ClickOkRemanning()
        {
            #region Variable Declarations
            DXButton uIOKButton = this.UIForskyvningWindow.UIPanBottomPanelClient.UIOKButton;
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBiOkMenuBaseButtonItem;
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow4.UIGSLookUpEditLookUpEdit;
            DXTextEdit uIECommentEdit = this.UIItemWindow4.UIECommentEdit;
            DXButton uIOKButton1 = this.UIItemWindow4.UIOKButton;
            #endregion

            // Click '&OK' button
            Mouse.Click(uIOKButton);

            // Click 'biOk' MenuBaseButtonItem
            Mouse.Click(uIBiOkMenuBaseButtonItem, new Point(40, 12));
            try
            {
                if (UIItemWindow4.Exists)
                {
                    // Type 'System.String' in 'GSLookUpEdit' LookUpEdit
                    //ValueTypeName
                    uIGSLookUpEditLookUpEdit.ValueTypeName = this.ClickOkRemaningParams.UIGSLookUpEditLookUpEditValueTypeName;

                    // Type 'MENGDE' in 'GSLookUpEdit' LookUpEdit
                    //ValueAsString
                    uIGSLookUpEditLookUpEdit.ValueAsString = this.ClickOkRemaningParams.UIGSLookUpEditLookUpEditValueAsString;

                    // Type '{Enter}{Tab}' in 'GSLookUpEdit' LookUpEdit
                    Keyboard.SendKeys(uIGSLookUpEditLookUpEdit, this.ApproveAmlWarningDialogExtraParams.UIGSLookUpEditLookUpEditSendKeys, ModifierKeys.None);

                    // Type 'Aml test 055' in 'eComment' text box
                    //ValueAsString
                    uIECommentEdit.ValueAsString = this.ClickOkRemaningParams.UIECommentEditValueAsString;

                    // Click 'Ok' button
                    Mouse.Click(uIOKButton1);
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("AML Brudd - Egen kommentar dialog ikke vist");
            }

        }

        /// <summary>
        /// SelectEmp1ExchangeFromSearch - Use 'SelectEmp1ExchangeFromSearchParams' to pass parameters into this method.
        /// </summary>
        public void SelectEmp1ExchangeFromSearch(string emp1)
        {
            #region Variable Declarations
            DXLookUpEdit uISleEmployment1LookUpEdit = this.UIBytteWindow.UIGsPanelControl1Client1.UIGrpEmployee1Client.UISleEmployment1LookUpEdit;
            DXTextEdit uITeFindEdit = this.UIBytteWindow.UIGsPanelControl1Client1.UIGrpEmployee1Client.UISleEmployment1LookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            #endregion

            // Type 'Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment' in 'sleEmployment1' LookUpEdit
            //ValueAsString
            uISleEmployment1LookUpEdit.ValueTypeName = this.SelectEmp1ExchangeFromSearchParams.UISleEmployment1LookUpEditValueAsString;

            // Click 'teFind' text box
            Mouse.Click(uITeFindEdit);

            // Type 'celsius{Enter}' in 'teFind' text box
            Keyboard.SendKeys(uITeFindEdit, emp1 + "{ENTER}", ModifierKeys.None);
        }

        /// <summary>
        /// SelectEmp2ExchangeFromSearch - Use 'SelectEmp2ExchangeFromSearchParams' to pass parameters into this method.
        /// </summary>
        public void SelectEmp2ExchangeFromSearch(string emp2)
        {
            #region Variable Declarations
            DXLookUpEdit uISleEmployment2LookUpEdit = this.UIBytteWindow.UIGsPanelControl1Client1.UIGrpEmployee2Client.UISleEmployment2LookUpEdit;
            DXTextEdit uITeFindEdit = this.UIBytteWindow.UIGsPanelControl1Client1.UIGrpEmployee2Client.UISleEmployment2LookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            #endregion

            // Type 'Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment' in 'sleEmployment2' LookUpEdit
            //ValueAsString
            uISleEmployment2LookUpEdit.ValueTypeName = this.SelectEmp2ExchangeFromSearchParams.UISleEmployment2LookUpEditValueAsString;

            // Click 'teFind' text box
            Mouse.Click(uITeFindEdit);

            // Type 'brun{Enter}' in 'teFind' text box
            Keyboard.SendKeys(uITeFindEdit, emp2 + "{ENTER}", ModifierKeys.None);
        }

        /// <summary>
        /// CreateExchange - Use 'CreateExchangeParams' to pass parameters into this method.
        /// </summary>
        public List<string> CreateExchange(string emp1, string emp2, DateTime fromDate, DateTime toDate, List<string> emp1Shifts, List<string> emp2Shifts, string step)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXRibbonButtonItem uIBytteRibbonBaseButtonItem = this.UIGatver64339794ASCLAvWindow4.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIBytteRibbonBaseButtonItem;
            DXPopupEdit uIPceDate0PopupEdit = this.UIBytteWindow.UIGsPanelControl1Client.UIGrpPeriodClient.UIPceDate0PopupEdit;
            DXPopupEdit uIPceDate1PopupEdit = this.UIBytteWindow.UIGsPanelControl1Client.UIGrpPeriodClient.UIPceDate1PopupEdit;
            //DXLookUpEdit uISleEmployment1LookUpEdit = this.UIBytteWindow.UIGsPanelControl1Client.UIGrpEmployee1Client.UISleEmployment1LookUpEdit;
            DXTextEdit uITeFindEdit = this.UIBytteWindow.UIGsPanelControl1Client.UIGrpEmployee1Client.UISleEmployment1LookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            //DXLookUpEdit uIGSSearchLookUpEditLookUpEdit = this.UIBytteWindow.UIGsPanelControl1Client.UIGSGroupControlClient.UIGSSearchLookUpEditLookUpEdit;
            DXCell shiftEmp1 = this.UIBytteWindow.UIGsPanelControl1Client.UIGsTabTabList.UITpShiftExchangeClient.UIGrpEmployee2ShiftsClient.UIGcEmployment2ShiftsTable.UILørdagCell;
            DXButton uIBtnGiveEmployee1Button = this.UIBytteWindow.UIGsPanelControl1Client.UIGsTabTabList.UITpShiftExchangeClient.UIBtnGiveEmployee1Button;
            DXCell shiftEmp2 = this.UIBytteWindow.UIGsPanelControl1Client.UIGsTabTabList.UITpShiftExchangeClient.UIGrpEmployee1ShiftsClient.UIGcEmployment1ShiftsTable.UIItemCell;
            DXButton uIBtnGiveEmployee2Button = this.UIBytteWindow.UIGsPanelControl1Client.UIGsTabTabList.UITpShiftExchangeClient.UIBtnGiveEmployee2Button;
            DXPopupEdit uILnkDetail0PopupEdit = this.UIBytteWindow.UILnkDetail0PopupEdit;
            DXTestControl uIXtraTabControlHeaderTabPage = this.UIBytteWindow.UIGsPanelControl1Client.UIGsTabTabList.UITpWeaClient.UITcWeaTabList.UIXtraTabControlHeaderTabPage;
            DXButton uIOKButton = this.UIBytteWindow.UIOKButton;
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBiOkMenuBaseButtonItem;
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow4.UIGSLookUpEditLookUpEdit;
            DXTextEdit uIECommentEdit = this.UIItemWindow4.UIECommentEdit;
            DXButton uIOKButton1 = this.UIItemWindow4.UIOKButton;


            #endregion

            // Click 'Bytte' RibbonBaseButtonItem
            Mouse.Click(uIBytteRibbonBaseButtonItem);

            if (UISammenligningavkompeWindow.Exists)
                CloseCompetenceComparison();

            UICommon.SetExchangePeriod(fromDate, toDate);

            if (emp1 != "")
            {
                SelectEmp1ExchangeFromSearch(emp1);
            }

            if (emp2 != "")
            {
                SelectEmp2ExchangeFromSearch(emp2);
            }

            foreach (var emp1Shift in emp1Shifts)
            {
                shiftEmp1.SearchProperties[DXTestControl.PropertyNames.Name] = "gcEmployment1ShiftsGridControlCell[View]gvEmployment1Shifts[Row]" + emp1Shift + "[Column]colStatusCode";
                try
                {
                    Mouse.DoubleClick(shiftEmp1);
                }
                catch (Exception)
                {
                    Mouse.Click(uIBtnGiveEmployee2Button);
                }
            }

            foreach (var emp2Shift in emp2Shifts)
            {
                shiftEmp2.SearchProperties[DXTestControl.PropertyNames.Name] = "gcEmployment2ShiftsGridControlCell[View]gvEmployment2Shifts[Row]" + emp2Shift + "[Column]colDayOfWeek2";

                try
                {
                    Mouse.DoubleClick(shiftEmp2);
                }
                catch (Exception)
                {
                    Mouse.Click(uIBtnGiveEmployee1Button);
                }
            }

            Playback.Wait(1000);

            // Type '3 brudd (klikk her for detaljer) [SelectionStart]0' in 'lnkDetail[0]' PopupEdit
            //Vis brudd for Emp1
            Mouse.Click(uILnkDetail0PopupEdit);

            //Sjekk Amlbrudd Emp1
            try
            {
                if (step == "_chapter5_step_1")
                    CheckAmlStatusExchangeCesarC5Step1();
                else if (step == "_chapter5_step_4")
                    CheckAmlStatusExchangeBosseC5Step4();
                else if (step == "_chapter5_step_6")
                    CheckAmlStatusExchangeAndersenC5Step6();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(" + step + ")");
            }

            //Velg Emp2
            Mouse.Click(uIXtraTabControlHeaderTabPage, new Point(36, 9));
            Playback.Wait(1000);

            //Sjekk Amlbrudd Emp2
            try
            {
                if (step == "_chapter5_step_1")
                    CheckAmlStatusExchangeBosseC5Step1();
                else if (step == "_chapter5_step_6")
                    CheckAmlStatusExchangeBosseC5Step6();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(" + step + ")");
            }

            // Click '&OK' button
            Mouse.Click(uIOKButton);

            Playback.Wait(2000);

            // Click 'biOk' MenuBaseButtonItem
            Mouse.Click(uIBiOkMenuBaseButtonItem, new Point(49, 13));

            // Type 'System.String' in 'GSLookUpEdit' LookUpEdit
            //ValueTypeName
            uIGSLookUpEditLookUpEdit.ValueTypeName = this.CreateExchangeParams.UIGSLookUpEditLookUpEditValueTypeName;

            // Type 'MENGDE' in 'GSLookUpEdit' LookUpEdit
            //ValueAsString
            uIGSLookUpEditLookUpEdit.ValueAsString = this.CreateExchangeParams.UIGSLookUpEditLookUpEditValueAsString;

            // Type 'Test 55 Aml, Bytte.' in 'eComment' text box
            //ValueAsString
            uIECommentEdit.ValueAsString = this.CreateExchangeParams.UIECommentEditValueAsString;

            // Click 'Ok' button
            Mouse.Click(uIOKButton1);
            if (UIRekalkuleringWindow.Exists)
                ClickOkRecalculateShifts();

            Playback.Wait(1500);
            return errorList;
        }

        /// <summary>
        /// CreateDepartmentExchange - Use 'CreateDepartmentExchangeParams' to pass parameters into this method.
        /// </summary>
        public List<string> CreateDepartmentExchange(string employee, DateTime fromDate, DateTime toDate, string step, List<string> shiftRows = null)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXRibbonButtonItem uIByttemedavdelingRibbonBaseButtonItem = this.UIGatver64339794ASCLAvWindow4.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIByttemedavdelingRibbonBaseButtonItem;
            DXPopupEdit uIPceDate0PopupEdit = this.UIByttemedavdelingWindow.UIGsPanelControl1Client.UIGrpPeriodClient.UIPceDate0PopupEdit;
            DXPopupEdit uIPceDate1PopupEdit = this.UIByttemedavdelingWindow.UIGsPanelControl1Client.UIGrpPeriodClient.UIPceDate1PopupEdit;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit = this.UIByttemedavdelingWindow.UIGsPanelControl1Client.UIGSGroupControlClient.UIGSSearchLookUpEditLookUpEdit;
            DXButton uIOKButton = this.UIByttemedavdelingWindow.UIOKButton;
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBiOkMenuBaseButtonItem;
            #endregion

            // Click 'Bytte med
            // avdeling' RibbonBaseButtonItem
            Mouse.Click(uIByttemedavdelingRibbonBaseButtonItem);

            UICommon.SetDepartmentExchangePeriod(fromDate, toDate);

            Mouse.Click(uIGSSearchLookUpEditLookUpEdit);

            // Type 'employee' in 'teFind' text box
            Keyboard.SendKeys(employee + "{ENTER}", ModifierKeys.None);

            //Velg ledige vakter
            if (shiftRows != null)
                SelectFreeDepExchangeShifts(shiftRows);

            try
            {
                if (step == "_chapter6_step_3")
                {
                    ClickAmlStatusDepExcange();
                    CheckAmlStatusDepartmentExchangeAndersenC6Step3();
                }
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step" + step);
            }

            // Click '&OK' button
            Mouse.Click(uIOKButton);

            // Click 'biOk' MenuBaseButtonItem
            Mouse.Click(uIBiOkMenuBaseButtonItem);//, new Point(43, 13));

            try
            {
                if (UIItemWindow4.Exists)
                {
                    DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow4.UIGSLookUpEditLookUpEdit;
                    DXTextEdit uIECommentEdit = this.UIItemWindow4.UIECommentEdit;
                    DXButton uIOKButton1 = this.UIItemWindow4.UIOKButton;

                    // Type 'System.String' in 'GSLookUpEdit' LookUpEdit
                    //ValueTypeName
                    uIGSLookUpEditLookUpEdit.ValueTypeName = this.ClickOkRemaningParams.UIGSLookUpEditLookUpEditValueTypeName;

                    // Type 'MENGDE' in 'GSLookUpEdit' LookUpEdit
                    //ValueAsString
                    uIGSLookUpEditLookUpEdit.ValueAsString = this.ClickOkRemaningParams.UIGSLookUpEditLookUpEditValueAsString;

                    // Type '{Enter}{Tab}' in 'GSLookUpEdit' LookUpEdit
                    Keyboard.SendKeys(uIGSLookUpEditLookUpEdit, this.ApproveAmlWarningDialogExtraParams.UIGSLookUpEditLookUpEditSendKeys, ModifierKeys.None);

                    // Type 'Aml test 055' in 'eComment' text box
                    //ValueAsString
                    uIECommentEdit.ValueAsString = "Test 55 Aml, Avdelingsbytte.";

                    // Click 'Ok' button
                    Mouse.Click(uIOKButton1);
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("AML Brudd - Egen kommentar dialog ikke vist");
            }

            Playback.Wait(1500);
            return errorList;
        }

        /// <summary>
        /// ClickAmlStatusDepExcange - Use 'ClickAmlStatusDepExcangeParams' to pass parameters into this method.
        /// </summary>
        public void ClickAmlStatusDepExcange()
        {
            #region Variable Declarations
            DXPopupEdit uILnkDetail0PopupEdit = this.UIByttemedavdelingWindow.UILnkDetail0PopupEdit;
            #endregion

            Mouse.Click(uILnkDetail0PopupEdit);
        }

        /// <summary>
        /// SelectFreeDepExchangeShifts - Use 'SelectFreeDepExchangeShiftsParams' to pass parameters into this method.
        /// </summary>
        public void SelectFreeDepExchangeShifts(List<string> shiftRows)
        {
            #region Variable Declarations
            DXButton uIVelgledigevakterButton = this.UIByttemedavdelingWindow.UIGsPanelControl1Client.UIGsTabTabList.UITpDepartmentExchangeClient.UIVelgledigevakterButton;
            #endregion

            // Click '&Velg ledige vakter' button
            Mouse.Click(uIVelgledigevakterButton);

            Playback.Wait(2000);
            UICommon.UIMapVS2017.SelectFreeDepExchangeShifts(shiftRows);           
        }

        /// <summary>
        /// CreateCallOut - Use 'CreateCallOutParams' to pass parameters into this method.
        /// </summary>
        public List<string> CreateCallOut(string step, DateTime? shiftDate, string fromTime, string toTime, bool? beforeMidnight = null, bool okNew = false)
        {
            #region Variable Declarations
            var errorList = new List<string>(); 
            var UtrykningsWindow = UIUtrykningWindow;
            UtrykningsWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Utrykning";
            DXRibbonButtonItem uIUtrykningRibbonBaseButtonItem = this.UIGatver64339794ASCLAvWindow4.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIUtrykningRibbonBaseButtonItem;
            #endregion

            // Click 'Utrykning' 
            try
            {
                if (!UtrykningsWindow.Exists)
                    Mouse.Click(uIUtrykningRibbonBaseButtonItem);
            }
            catch (Exception)
            {
                Mouse.Click(uIUtrykningRibbonBaseButtonItem);
            }

            UICommon.UIMapVS2017.SetCalloutCause("Div");
           if (shiftDate != null)
                SelectCallOutShiftDateAndShift(shiftDate.Value);

            if (beforeMidnight != null)
                SelectBeforeOrAfterMidnightCallOut(beforeMidnight.Value); 

            UICommon.UIMapVS2017.SetCalloutPeriod(fromTime, toTime);
            errorList.AddRange(AmlCheckCalloutWindow(step));
            CloseCalloutWindow(okNew);

            Playback.Wait(1500);
            return errorList;
        }

        /// <summary>
        /// SelectCallOutShiftDateAndShift - Use 'SelectCallOutShiftDateAndShiftParams' to pass parameters into this method.
        /// </summary>
        public void SelectCallOutShiftDateAndShift(DateTime shiftDate)
        {
            #region Variable Declarations
            DXPopupEdit uIPceDatePopupEdit = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UI_navBarNavBar.UINavBarGroupControlCoScrollableControl2.UI_panShiftClient.UIPceDatePopupEdit;
            DXPopupEdit uIPceDatePopupEdit1 = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UI_navBarNavBar.UINavBarGroupControlCoScrollableControl3.UI_panShiftClient.UIPceDatePopupEdit;
            DXLookUpEdit uI_cbCalloutShiftsLookUpEdit = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UI_navBarNavBar.UINavBarGroupControlCoScrollableControl4.UI_panShiftClient.UI_cbCalloutShiftsLookUpEdit;
            DXLookUpEdit uI_cbCalloutShiftsLookUpEdit1 = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UI_navBarNavBar.UINavBarGroupControlCoScrollableControl5.UI_panShiftClient.UI_cbCalloutShiftsLookUpEdit;
            #endregion


            UICommon.SetCalloutDate(shiftDate);

            // Type 'Gatsoft.Gat.BusinessLogic.Callout.CalloutDTO+CombinationShift' in '_cbCalloutShifts' LookUpEdit
            //ValueTypeName
            Mouse.Click(uI_cbCalloutShiftsLookUpEdit);

            // Type 'H2 (00.00 - 23.59)' in '_cbCalloutShifts' LookUpEdit
            //ValueAsString
            Keyboard.SendKeys(uI_cbCalloutShiftsLookUpEdit1, "{DOWN}{ENTER}", ModifierKeys.None);
        }

        /// <summary>
        /// SelectBeforeOrAfterMidnightCallOut - Use 'SelectBeforeOrAfterMidnightCallOutParams' to pass parameters into this method.
        /// </summary>
        public void SelectBeforeOrAfterMidnightCallOut(bool beforeMidnight)
        {
            if (beforeMidnight)
                UICommon.UIMapVS2017.SelectCalloutDay1();           
            else
                 UICommon.UIMapVS2017.SelectCalloutDay2();
        }

        public void EditCalloutAndSetSevereServiceDisruption(bool checkSevereDisruption, string step)
        {
            CheckSevereServiceDisruptionCheckBox(checkSevereDisruption);
            AmlCheckCalloutWindow(step);
            CloseCalloutWindow(false);
        }

        private List<string> AmlCheckCalloutWindow(string step)
        {
            var errorList = new List<string>();
            DXPopupEdit uILnkDetail0PopupEdit = this.UIUtrykningWindow.UILnkDetail0PopupEdit;

            Mouse.Click(uILnkDetail0PopupEdit);
            try
            {
                if (step == "_chapter7_step_1")
                {
                    CheckAmlStatusCalloutGareC7Step1();
                }
                else if (step == "_chapter7_step_3")
                {
                    CheckAmlStatusCalloutGareC7Step3();
                }
                else if (step == "_chapter7_step_4")
                {
                    CheckAmlStatusCalloutGareC7Step4();
                }
                else if (step == "_chapter7_step_4_2")
                {
                    CheckAmlStatusCalloutGareC7Step4_2();
                }
                else if (step == "_chapter7_step_5")
                {
                    CheckAmlStatusCalloutGareC7Step5();
                }
                else if (step == "_chapter7_step_8")
                {
                    CheckAmlStatusCalloutGareC7Step8();
                }
                else if (step == "_chapter7_step_9")
                {
                    CheckAmlStatusCalloutGareC7Step9();
                }
                else if (step == "_chapter7_step_10")
                {
                    CheckAmlStatusCalloutGareC7Step10();
                }
                else if (step == "_chapter7_step_10_2")
                {
                    CheckAmlStatusCalloutGareC7Step10_2();
                }
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step" + step);
            }

            BackToRegistrationCallOut();

            return errorList;
        }

        private void CloseCalloutWindow(bool okNew)
        {
            //DXButton uIOKButton = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UI_liOKLayoutControlItem.UIOKButton;
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow4.UIGSLookUpEditLookUpEdit;
            DXTextEdit uIECommentEdit = this.UIItemWindow4.UIECommentEdit;
            DXButton uIOKButton1 = this.UIItemWindow4.UIOKButton;

            // Click 'Ok' button
            if (okNew)
                ClickOkNewCallOut();
            else
                UICommon.UIMapVS2017.ClickOkCallout();

            try
            {
                Playback.Wait(1000);
                if (UIItemWindow4.Exists)
                {
                    // Type 'System.String' in 'GSLookUpEdit' LookUpEdit
                    //ValueTypeName
                    uIGSLookUpEditLookUpEdit.ValueTypeName = this.CreateCallOutParams.UIGSLookUpEditLookUpEditValueTypeName;

                    // Type 'MENGDE' in 'GSLookUpEdit' LookUpEdit
                    //ValueAsString
                    uIGSLookUpEditLookUpEdit.ValueAsString = this.CreateCallOutParams.UIGSLookUpEditLookUpEditValueAsString;

                    // Type '{Enter}{Tab}' in 'GSLookUpEdit' LookUpEdit
                    Keyboard.SendKeys(uIGSLookUpEditLookUpEdit, this.ApproveAmlWarningDialogExtraParams.UIGSLookUpEditLookUpEditSendKeys, ModifierKeys.None);

                    // Type 'Test 55 Aml, Utrykning.' in 'eComment' text box
                    //ValueAsString
                    uIECommentEdit.ValueAsString = this.CreateCallOutParams.UIECommentEditValueAsString;

                    // Click 'Ok' button
                    Playback.Wait(1000);
                    Mouse.Click(uIOKButton1);
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("AML Brudd - Egen kommentar dialog ikke vist");
            }
        }

        /// <summary>
        /// CheckSevereServiceDisruptionCheckBox - Use 'CheckSevereServiceDisruptionCheckBoxParams' to pass parameters into this method.
        /// </summary>
        private void CheckSevereServiceDisruptionCheckBox(bool check)
        {
            #region Variable Declarations
            DXCheckBox uIGsChkBxWeaSevereServCheckBox = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UI_navBarNavBar.UINavBarGroupControlCoScrollableControl1.UIGsChkBxWeaSevereServCheckBox;
            #endregion

            // Select 'gsChkBxWeaSevereServiceDisruption' check box
            uIGsChkBxWeaSevereServCheckBox.Checked = check;
        }

        public enum DispTypes
        {
            SamletTidPrDag,
            AmlTimerUke,
            AmlTimerDag,
            AmlTimerÅr,
            SøndagerPåRad,
            SamletPrUkeSnitt,
            PlanlagtTidPrUke,
            UkentligArbFri,
            SøndagerPåRadSnitt,
            ArbFriFørVakt
        }

        /// <summary>
        /// CreateAmlDispension - Use 'CreateAmlDispensionParams' to pass parameters into this method.
        /// </summary>
        public void CreateAmlDispension(DispTypes dispType, string limit, DateTime fromDate, DateTime toDate)
        {
            #region Variable Declarations
            DXLookUpEdit uIDropdwnDispensationTLookUpEdit = this.UINyAMLdispensasjonWindow.UIViewHostDispensationCustom.UIPcViewClient.UIDispensationDetailsVCustom.UIDropdwnDispensationTLookUpEdit;
            DXWindow uIPopupLookUpEditFormWindow = this.UINyAMLdispensasjonWindow.UIViewHostDispensationCustom.UIPcViewClient.UIDispensationDetailsVCustom.UIDropdwnDispensationTLookUpEdit.UIPopupLookUpEditFormWindow;
            DXTextEdit uISdeStartEdit = this.UINyAMLdispensasjonWindow.UIViewHostDispensationCustom.UIPcViewClient.UIDispensationDetailsVCustom.UIVhDispInnerDetailsCustom.UIPcViewClient.UIDefaultDispDetailsViCustom.UISdeStartEdit;
            DXTextEdit uISdeEndEdit = this.UINyAMLdispensasjonWindow.UIViewHostDispensationCustom.UIPcViewClient.UIDispensationDetailsVCustom.UIVhDispInnerDetailsCustom.UIPcViewClient.UIDefaultDispDetailsViCustom.UISdeEndEdit;
            DXTextEdit uISdeLimitEdit = this.UINyAMLdispensasjonWindow.UIViewHostDispensationCustom.UIPcViewClient.UIDispensationDetailsVCustom.UIVhDispInnerDetailsCustom.UIPcViewClient.UIDefaultDispDetailsViCustom.UISdeLimitEdit;
            DXTextEdit uIMemoCommentEdit = this.UINyAMLdispensasjonWindow.UIViewHostDispensationCustom.UIPcViewClient.UIDispensationDetailsVCustom.UIVhDispInnerDetailsCustom.UIPcViewClient.UIDefaultDispDetailsViCustom.UIMemoCommentEdit;
            DXButton uISimpleButtonButton = this.UINyAMLdispensasjonWindow.UISimpleButtonButton;
            #endregion

            ClickNewAmlDispension();

            SelectAmlDispType(dispType);

            UICommon.SetAmlDispensionPeriod(fromDate, toDate);

            uISdeLimitEdit.ValueAsString = limit;
            Keyboard.SendKeys(uISdeLimitEdit, this.CreateWeekSeparationParams.UITab, ModifierKeys.None);

            // Type 'Automated test 55' in 'memoComment' text box
            Keyboard.SendKeys(uIMemoCommentEdit, this.CreateAmlDispensionParams.UIMemoCommentEditSendKeys, ModifierKeys.None);
            Keyboard.SendKeys(uIMemoCommentEdit, this.CreateWeekSeparationParams.UITab, ModifierKeys.None);
            Keyboard.SendKeys(this.CreateWeekSeparationParams.UITab, ModifierKeys.None);
            Keyboard.SendKeys(this.CreateWeekSeparationParams.UITab, ModifierKeys.None);

            // Click 'SimpleButton' button
            Mouse.Click(uISimpleButtonButton);
        }

        /// <summary>
        /// SelectAmlDispType - Use 'SelectAmlDispTypeParams' to pass parameters into this method.
        /// </summary>
        public void SelectAmlDispType(DispTypes dispType)
        {
            #region Variable Declarations
            DXLookUpEdit uIDropdwnDispensationTLookUpEdit = this.UINyAMLdispensasjonWindow.UIViewHostDispensationCustom.UIPcViewClient.UIDispensationDetailsVCustom.UIDropdwnDispensationTLookUpEdit;
            DXWindow uIPopupLookUpEditFormWindow = this.UINyAMLdispensasjonWindow.UIViewHostDispensationCustom.UIPcViewClient.UIDispensationDetailsVCustom.UIDropdwnDispensationTLookUpEdit.UIPopupLookUpEditFormWindow;
            var selectDisp = new Point();
            #endregion

            // Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
            //ValueAsString
            uIDropdwnDispensationTLookUpEdit.ValueTypeName = this.CreateAmlDispensionParams.UIDropdwnDispensationTLookUpEditViewModel;

            switch (dispType)
            {
                case DispTypes.SamletTidPrDag:
                    selectDisp = new Point(50, 9);
                    break;
                case DispTypes.AmlTimerUke:
                    selectDisp = new Point(50, 22);
                    break;
                case DispTypes.AmlTimerDag:
                    selectDisp = new Point(50, 37);
                    break;
                case DispTypes.AmlTimerÅr:
                    selectDisp = new Point(50, 54);
                    break;
                case DispTypes.SøndagerPåRad:
                    selectDisp = new Point(50, 67);
                    break;
                case DispTypes.SamletPrUkeSnitt:
                    selectDisp = new Point(50, 83);
                    break;
                case DispTypes.PlanlagtTidPrUke:
                    selectDisp = new Point(50, 99);
                    break;
                case DispTypes.UkentligArbFri:
                    selectDisp = new Point(50, 113);
                    break;
                case DispTypes.SøndagerPåRadSnitt:
                    selectDisp = new Point(50, 128);
                    break;
                case DispTypes.ArbFriFørVakt:
                    selectDisp = new Point(50, 144);
                    break;
                default:
                    break;
            }

            // Click 'PopupLookUpEditForm' window
            Mouse.Click(uIPopupLookUpEditFormWindow, selectDisp);
            Keyboard.SendKeys(uIPopupLookUpEditFormWindow, this.CreateWeekSeparationParams.UITab, ModifierKeys.None);
        }

        /// <summary>
        /// CreateWeekSeparation - Use 'CreateWeekSeparationParams' to pass parameters into this method.
        /// </summary>
        public void CreateWeekSeparation()
        {
            #region Variable Declarations
            WinClient uIDøgnogukeskilleClient = this.UIGatver64339794ASCLAvWindow11.UIItemWindow.UIDøgnogukeskilleClient;
            WinEdit uIItemEdit = this.UIDøgnogukeskilleWindow.UIItemWindow.UIItemEdit;
            WinEdit uIItemEdit1 = this.UIDøgnogukeskilleWindow.UIItemWindow1.UIItemEdit;
            WinClient uIDøgnogukeskilleClient1 = this.UIDøgnogukeskilleWindow.UIItemWindow2.UIDøgnogukeskilleClient;
            WinButton uIOKButton = this.UIDøgnogukeskilleWindow.UIItemClient.UIOKButton;
            #endregion

            // Click 'Døgn- og ukeskille' client
            Playback.Wait(500);
            try
            {
                ClickNewWeekSeparation();
            }
            catch (Exception)
            {
                Mouse.Click(uIDøgnogukeskilleClient, new Point(53, 18));
            }

            try
            {
                Keyboard.SendKeys(uIItemEdit, this.CreateWeekSeparationParams.UIItemEditText, ModifierKeys.None);
            }
            catch (Exception)
            {
                // Type '25.10.2016' in text box
                uIItemEdit.Text = this.CreateWeekSeparationParams.UIItemEditText;
            }

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit, this.CreateWeekSeparationParams.UITab, ModifierKeys.None);

            try
            {
                Keyboard.SendKeys(uIItemEdit1, this.CreateWeekSeparationParams.UIItemEditText1, ModifierKeys.None);
            }
            catch (Exception)
            {
                // Type '00:00' in text box
                uIItemEdit1.Text = this.CreateWeekSeparationParams.UIItemEditText1;
            }

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit1, this.CreateWeekSeparationParams.UIItemEditSendKeys1, ModifierKeys.None);

            // Type 'Test{Space}55{Tab}' in 'Døgn- og ukeskille' client
            Keyboard.SendKeys(uIDøgnogukeskilleClient1, this.CreateWeekSeparationParams.UIDøgnogukeskilleClientSendKeys, ModifierKeys.None);

            // Click '&OK' button
            Mouse.Click(uIOKButton, new Point(28, 17));
        }

        public void CreateWeekSeparationSE()
        {
            #region Variable Declarations
            WinClient uIDøgnogukeskilleClient = this.UIGatver64339794ASCLAvWindow11.UIItemWindow.UIDøgnogukeskilleClient;
            WinEdit uIItemEdit = this.UIDøgnogukeskilleWindow.UIItemWindow.UIItemEdit;
            WinEdit uIItemEdit1 = this.UIDøgnogukeskilleWindow.UIItemWindow1.UIItemEdit;
            WinClient uIDøgnogukeskilleClient1 = this.UIDøgnogukeskilleWindow.UIItemWindow2.UIDøgnogukeskilleClient;
            WinButton uIOKButton = this.UIDøgnogukeskilleWindow.UIItemClient.UIOKButton;
            #endregion

            // Click 'Døgn- og ukeskille' client
            Playback.Wait(500);
            try
            {
                ClickNewWeekSeparation();
            }
            catch (Exception)
            {
                Mouse.Click(uIDøgnogukeskilleClient, new Point(53, 18));
            }

            try
            {
                Playback.Wait(500);
                Keyboard.SendKeys(uIItemEdit, "2016-10-24", ModifierKeys.None);

            }
            catch (Exception)
            {
                uIItemEdit.Text = "2016-10-24";

            }
            Playback.Wait(500);
            Keyboard.SendKeys("{TAB}");
            try
            {
                Keyboard.SendKeys(uIItemEdit1, "07:00", ModifierKeys.None);
            }
            catch (Exception)
            {
                uIItemEdit1.Text = "07:00";

            }
            Playback.Wait(500);
            Keyboard.SendKeys("{TAB}");

            Playback.Wait(500);
            Keyboard.SendKeys(uIDøgnogukeskilleClient1, this.CreateWeekSeparationParams.UIDøgnogukeskilleClientSendKeys, ModifierKeys.None);

            // Click '&OK' button
            Mouse.Click(uIOKButton, new Point(28, 17));
        }

        /// <summary>
        /// CreateAmlCalculation - Use 'CreateAmlCalculationParams' to pass parameters into this method.
        /// </summary>
        public void CreateAmlCalculation()
        {
            #region Variable Declarations
            var culture = new System.Globalization.CultureInfo("nb-NO");
            var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            var checkToday = this.UIOppsettforAMLkalkuleWindow.UIItemWindow1.UIItemClient.UIMandagCheckBox;
            checkToday.SearchProperties[WinCheckBox.PropertyNames.Name] = day;
            WinClient uIItemClient = this.UIOppsettforAMLkalkuleWindow.UIOppsettforAMLkalkuleClient.UIItemClient;
            WinComboBox uIItemComboBox = this.UIOppsettforAMLkalkuleWindow.UIItemWindow.UIItemComboBox;
            WinEdit uIItemEdit = this.UIOppsettforAMLkalkuleWindow.UIItem1200Window.UIItemEdit;
            WinButton uIOKButton = this.UIOppsettforAMLkalkuleWindow.UIItemClient.UIOKButton;
            #endregion

            // Click client
            Mouse.Click(uIItemClient, new Point(34, 26));

            // Select '2060 - AML avdeling 1' in combo box
            uIItemComboBox.SelectedItem = this.CreateAmlCalculationParams.UIItemComboBoxSelectedItem;

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemComboBox, this.CreateAmlCalculationParams.UIItemEditSendKeys, ModifierKeys.None);

            // Type 'calcTime' in text box
            uIItemEdit.Text = this.CreateAmlCalculationParams.UICalcTime;

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit, this.CreateAmlCalculationParams.UIItemEditSendKeys, ModifierKeys.None);

            checkToday.Checked = true;
            // Type '{Tab}' in text box
            Keyboard.SendKeys(checkToday, this.CreateAmlCalculationParams.UIItemEditSendKeys, ModifierKeys.None);

            // Click 'OK' button
            Mouse.Click(uIOKButton, new Point(33, 27));

            Playback.Wait(1000);
            CloseAmlCalculationWindow();
        }

        /// <summary>
        /// SelectShiftsInDeleteShiftsWindow - Use 'SelectShiftsInDeleteShiftsWindowParams' to pass parameters into this method.
        /// </summary>
        public void SelectShiftsInDeleteShiftsWindow(bool selectAll)
        {
            #region Variable Declarations
            DXRibbonButtonItem uISlettRibbonBaseButtonItem = this.UIGatver64339794ASCLAvWindow4.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpMiscRibbonPageGroup.UISlettRibbonBaseButtonItem;
            //DXTreeListNode uINode1TreeListNode = this.UIItemWindow5.UIPcContentClient.UITlOperationsTreeList.UINode1TreeListNode;
            //DXTreeListNode uINode2TreeListNode = this.UIItemWindow5.UIPcContentClient.UITlOperationsTreeList.UINode2TreeListNode;
            //DXTreeListNode uINode3TreeListNode = this.UIItemWindow5.UIPcContentClient.UITlOperationsTreeList.UINode3TreeListNode;
            //DXTreeListNode uINode4TreeListNode = this.UIItemWindow5.UIPcContentClient.UITlOperationsTreeList.UINode4TreeListNode;
            //DXTreeListNode uINode0TreeListNode = this.UIItemWindow5.UIPcContentClient.UITlOperationsTreeList.UINode0TreeListNode;
            DXButton uIVelgingenButton = this.UIItemWindow5.UIPcContentClient.UIVelgingenButton;
            DXButton uIVelgalleButton = this.UIItemWindow5.UIPcContentClient.UIVelgalleButton;
            DXButton uIGSSimpleButtonButton = this.UIItemWindow5.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            #endregion

            // Click 'Slett' RibbonBaseButtonItem
            Mouse.Click(uISlettRibbonBaseButtonItem);

            #region Select individual shifts
            //// Type 'True' in 'Node1' TreeListNode
            ////Checked
            //uINode1TreeListNode.Checked = true;

            //// Type 'True' in 'Node2' TreeListNode
            ////Checked
            //uINode2TreeListNode.Checked = true;

            //// Type 'True' in 'Node3' TreeListNode
            ////Checked
            //uINode3TreeListNode.Checked = true;

            //// Type 'True' in 'Node4' TreeListNode
            ////Checked
            //uINode4TreeListNode.Checked = true;

            //// Type 'False' in 'Node0' TreeListNode
            ////Checked
            //uINode0TreeListNode.Checked = true;
            #endregion

            if (selectAll)
                Mouse.Click(uIVelgalleButton);
            else
                Mouse.Click(uIVelgingenButton);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);

            if (UIRekalkuleringWindow.Exists)
                ClickOkRecalculateShifts();
        }

        public List<String> CompareReportDataFiles_Test055()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext, 0, true);
            return errorList;
        }

        public void AssertResults(List<string> errorList)
        {
            UICommon.AssertResults(errorList);
        }


        public virtual ExtraCesarFridayWeek1Params ExtraCesarFridayWeek1Params
        {
            get
            {
                if ((this.mExtraCesarFridayWeek1Params == null))
                {
                    this.mExtraCesarFridayWeek1Params = new ExtraCesarFridayWeek1Params();
                }
                return this.mExtraCesarFridayWeek1Params;
            }
        }

        private ExtraCesarFridayWeek1Params mExtraCesarFridayWeek1Params;


        public virtual SaveAmlBrakesAsXlsParams SaveAmlBrakesAsXlsParams
        {
            get
            {
                if ((this.mSaveAmlBrakesAsXlsParams == null))
                {
                    this.mSaveAmlBrakesAsXlsParams = new SaveAmlBrakesAsXlsParams();
                }
                return this.mSaveAmlBrakesAsXlsParams;
            }
        }

        private SaveAmlBrakesAsXlsParams mSaveAmlBrakesAsXlsParams;

        public virtual ConstructAbsenceParams ConstructAbsenceParams
        {
            get
            {
                if ((this.mConstructAbsenceParams == null))
                {
                    this.mConstructAbsenceParams = new ConstructAbsenceParams();
                }
                return this.mConstructAbsenceParams;
            }
        }

        private ConstructAbsenceParams mConstructAbsenceParams;

        /// <summary>
        /// ConstructRemanageShift - Use 'ConstructRemanageShiftParams' to pass parameters into this method.
        /// </summary>


        public virtual ConstructRemanageShiftParams ConstructRemanageShiftParams
        {
            get
            {
                if ((this.mConstructRemanageShiftParams == null))
                {
                    this.mConstructRemanageShiftParams = new ConstructRemanageShiftParams();
                }
                return this.mConstructRemanageShiftParams;
            }
        }

        private ConstructRemanageShiftParams mConstructRemanageShiftParams;


        public virtual ClickOkRemaningParams ClickOkRemaningParams
        {
            get
            {
                if ((this.mClickOkRemaningParams == null))
                {
                    this.mClickOkRemaningParams = new ClickOkRemaningParams();
                }
                return this.mClickOkRemaningParams;
            }
        }

        private ClickOkRemaningParams mClickOkRemaningParams;


        public virtual SelectFreeShiftToRemanningParams SelectFreeShiftToRemanningParams
        {
            get
            {
                if ((this.mSelectFreeShiftToRemanningParams == null))
                {
                    this.mSelectFreeShiftToRemanningParams = new SelectFreeShiftToRemanningParams();
                }
                return this.mSelectFreeShiftToRemanningParams;
            }
        }

        private SelectFreeShiftToRemanningParams mSelectFreeShiftToRemanningParams;


        public virtual SetGradedDataParams SetGradedDataParams
        {
            get
            {
                if ((this.mSetGradedDataParams == null))
                {
                    this.mSetGradedDataParams = new SetGradedDataParams();
                }
                return this.mSetGradedDataParams;
            }
        }

        private SetGradedDataParams mSetGradedDataParams;


        public virtual SetDatePeriodInShiftListControlParams SetDatePeriodInShiftListControlParams
        {
            get
            {
                if ((this.mSetDatePeriodInShiftListControlParams == null))
                {
                    this.mSetDatePeriodInShiftListControlParams = new SetDatePeriodInShiftListControlParams();
                }
                return this.mSetDatePeriodInShiftListControlParams;
            }
        }

        private SetDatePeriodInShiftListControlParams mSetDatePeriodInShiftListControlParams;


        public virtual CreateExchangeParams CreateExchangeParams
        {
            get
            {
                if ((this.mCreateExchangeParams == null))
                {
                    this.mCreateExchangeParams = new CreateExchangeParams();
                }
                return this.mCreateExchangeParams;
            }
        }

        private CreateExchangeParams mCreateExchangeParams;

        public virtual CheckAmlSettingsUseOrgShiftDepartmentExchangeParams CheckAmlSettingsUseOrgShiftDepartmentExchangeParams
        {
            get
            {
                if ((this.mCheckAmlSettingsUseOrgShiftDepartmentExchangeParams == null))
                {
                    this.mCheckAmlSettingsUseOrgShiftDepartmentExchangeParams = new CheckAmlSettingsUseOrgShiftDepartmentExchangeParams();
                }
                return this.mCheckAmlSettingsUseOrgShiftDepartmentExchangeParams;
            }
        }

        private CheckAmlSettingsUseOrgShiftDepartmentExchangeParams mCheckAmlSettingsUseOrgShiftDepartmentExchangeParams;

        public virtual CreateCallOutParams CreateCallOutParams
        {
            get
            {
                if ((this.mCreateCallOutParams == null))
                {
                    this.mCreateCallOutParams = new CreateCallOutParams();
                }
                return this.mCreateCallOutParams;
            }
        }

        private CreateCallOutParams mCreateCallOutParams;

        public virtual ChangeAmlDayWorkerMinBeforeShiftParams ChangeAmlDayWorkerMinBeforeShiftParams
        {
            get
            {
                if ((this.mChangeAmlDayWorkerMinBeforeShiftParams == null))
                {
                    this.mChangeAmlDayWorkerMinBeforeShiftParams = new ChangeAmlDayWorkerMinBeforeShiftParams();
                }
                return this.mChangeAmlDayWorkerMinBeforeShiftParams;
            }
        }

        private ChangeAmlDayWorkerMinBeforeShiftParams mChangeAmlDayWorkerMinBeforeShiftParams;

        public virtual CreateAmlDispensionParams CreateAmlDispensionParams
        {
            get
            {
                if ((this.mCreateAmlDispensionParams == null))
                {
                    this.mCreateAmlDispensionParams = new CreateAmlDispensionParams();
                }
                return this.mCreateAmlDispensionParams;
            }
        }

        private CreateAmlDispensionParams mCreateAmlDispensionParams;

        public virtual ChangeAmlDnlf38FreeBeforeShiftParams ChangeAmlDnlf38FreeBeforeShiftParams
        {
            get
            {
                if ((this.mChangeAmlDnlf38FreeBeforeShiftParams == null))
                {
                    this.mChangeAmlDnlf38FreeBeforeShiftParams = new ChangeAmlDnlf38FreeBeforeShiftParams();
                }
                return this.mChangeAmlDnlf38FreeBeforeShiftParams;
            }
        }

        private ChangeAmlDnlf38FreeBeforeShiftParams mChangeAmlDnlf38FreeBeforeShiftParams;


        public virtual SetAmlTimerPrUkeParams SetAmlTimerPrUkeParams
        {
            get
            {
                if ((this.mSetAmlTimerPrUkeParams == null))
                {
                    this.mSetAmlTimerPrUkeParams = new SetAmlTimerPrUkeParams();
                }
                return this.mSetAmlTimerPrUkeParams;
            }
        }

        private SetAmlTimerPrUkeParams mSetAmlTimerPrUkeParams;


        public virtual SetSamletTidPrUkeParams SetSamletTidPrUkeParams
        {
            get
            {
                if ((this.mSetSamletTidPrUkeParams == null))
                {
                    this.mSetSamletTidPrUkeParams = new SetSamletTidPrUkeParams();
                }
                return this.mSetSamletTidPrUkeParams;
            }
        }

        private SetSamletTidPrUkeParams mSetSamletTidPrUkeParams;

        public virtual ChangeAmlTurnus35_5Params ChangeAmlTurnus35_5Params
        {
            get
            {
                if ((this.mChangeAmlTurnus35_5Params == null))
                {
                    this.mChangeAmlTurnus35_5Params = new ChangeAmlTurnus35_5Params();
                }
                return this.mChangeAmlTurnus35_5Params;
            }
        }

        private ChangeAmlTurnus35_5Params mChangeAmlTurnus35_5Params;

        public virtual NewEmploymentParams NewEmploymentParams
        {
            get
            {
                if ((this.mNewEmploymentParams == null))
                {
                    this.mNewEmploymentParams = new NewEmploymentParams();
                }
                return this.mNewEmploymentParams;
            }
        }

        private NewEmploymentParams mNewEmploymentParams;

        public virtual CreateAmlCalculationParams CreateAmlCalculationParams
        {
            get
            {
                if ((this.mCreateAmlCalculationParams == null))
                {
                    this.mCreateAmlCalculationParams = new CreateAmlCalculationParams();
                }
                return this.mCreateAmlCalculationParams;
            }
        }

        private CreateAmlCalculationParams mCreateAmlCalculationParams;

        public virtual OpenWeaBreakServiceParams OpenWeaBreakServiceParams
        {
            get
            {
                if ((this.mOpenWeaBreakServiceParams == null))
                {
                    this.mOpenWeaBreakServiceParams = new OpenWeaBreakServiceParams();
                }
                return this.mOpenWeaBreakServiceParams;
            }
        }

        private OpenWeaBreakServiceParams mOpenWeaBreakServiceParams;

        public virtual CreateWeekSeparationParams CreateWeekSeparationParams
        {
            get
            {
                if ((this.mCreateWeekSeparationParams == null))
                {
                    this.mCreateWeekSeparationParams = new CreateWeekSeparationParams();
                }
                return this.mCreateWeekSeparationParams;
            }
        }

        private CreateWeekSeparationParams mCreateWeekSeparationParams;

        public virtual ApproveAmlWarningDialogExtraParams ApproveAmlWarningDialogExtraParams
        {
            get
            {
                if ((this.mApproveAmlWarningDialogExtraParams == null))
                {
                    this.mApproveAmlWarningDialogExtraParams = new ApproveAmlWarningDialogExtraParams();
                }
                return this.mApproveAmlWarningDialogExtraParams;
            }
        }

        private ApproveAmlWarningDialogExtraParams mApproveAmlWarningDialogExtraParams;

        public virtual AddCarlBildtShiftsParams AddCarlBildtShiftsParams
        {
            get
            {
                if ((this.mAddCarlBildtShiftsParams == null))
                {
                    this.mAddCarlBildtShiftsParams = new AddCarlBildtShiftsParams();
                }
                return this.mAddCarlBildtShiftsParams;
            }
        }

        private AddCarlBildtShiftsParams mAddCarlBildtShiftsParams;

        public virtual SetAmlDnlf38FreeBeforeShiftParams SetAmlDnlf38FreeBeforeShiftParams
        {
            get
            {
                if ((this.mSetAmlDnlf38FreeBeforeShiftParams == null))
                {
                    this.mSetAmlDnlf38FreeBeforeShiftParams = new SetAmlDnlf38FreeBeforeShiftParams();
                }
                return this.mSetAmlDnlf38FreeBeforeShiftParams;
            }
        }

        private SetAmlDnlf38FreeBeforeShiftParams mSetAmlDnlf38FreeBeforeShiftParams;

        public virtual SelectBosseEmp2ExchangeParams SelectBosseEmp2ExchangeParams
        {
            get
            {
                if ((this.mSelectBosseEmp2ExchangeParams == null))
                {
                    this.mSelectBosseEmp2ExchangeParams = new SelectBosseEmp2ExchangeParams();
                }
                return this.mSelectBosseEmp2ExchangeParams;
            }
        }

        private SelectBosseEmp2ExchangeParams mSelectBosseEmp2ExchangeParams;

        public virtual SelectEmp1ExchangeParams SelectEmp1ExchangeParams
        {
            get
            {
                if ((this.mSelectEmp1ExchangeParams == null))
                {
                    this.mSelectEmp1ExchangeParams = new SelectEmp1ExchangeParams();
                }
                return this.mSelectEmp1ExchangeParams;
            }
        }

        private SelectEmp1ExchangeParams mSelectEmp1ExchangeParams;

        public virtual SelectEmp1ExchangeFromSearchParams SelectEmp1ExchangeFromSearchParams
        {
            get
            {
                if ((this.mSelectEmp1ExchangeFromSearchParams == null))
                {
                    this.mSelectEmp1ExchangeFromSearchParams = new SelectEmp1ExchangeFromSearchParams();
                }
                return this.mSelectEmp1ExchangeFromSearchParams;
            }
        }

        private SelectEmp1ExchangeFromSearchParams mSelectEmp1ExchangeFromSearchParams;


        public virtual SelectEmp2ExchangeFromSearchParams SelectEmp2ExchangeFromSearchParams
        {
            get
            {
                if ((this.mSelectEmp2ExchangeFromSearchParams == null))
                {
                    this.mSelectEmp2ExchangeFromSearchParams = new SelectEmp2ExchangeFromSearchParams();
                }
                return this.mSelectEmp2ExchangeFromSearchParams;
            }
        }

        private SelectEmp2ExchangeFromSearchParams mSelectEmp2ExchangeFromSearchParams;

        public virtual AddNewAmlLimitParams AddNewAmlLimitParams
        {
            get
            {
                if ((this.mAddNewAmlLimitParams == null))
                {
                    this.mAddNewAmlLimitParams = new AddNewAmlLimitParams();
                }
                return this.mAddNewAmlLimitParams;
            }
        }

        private AddNewAmlLimitParams mAddNewAmlLimitParams;
    }
/// <summary>
/// Parameters to be passed into 'ExtraCesarFridayWeek1'
/// </summary>
[GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ExtraCesarFridayWeek1Params
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.DataModel.OvertimeCode' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEditValueTypeName = "Gatsoft.Gat.DataModel.OvertimeCode";

        /// <summary>
        /// Type 'OvertimeCode(Id=242481501)' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEditValueAsString = "OvertimeCode(Id=242481501)";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SaveAmlBrakesAsXls'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SaveAmlBrakesAsXlsParams
    {

        #region Fields

        /// <summary>
        /// Type '{Tab}' in 'File name:' text box
        /// </summary>
        public string UITab = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ConstructAbsence'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ConstructAbsenceParams
    {

        #region Fields

        /// <summary>
        /// Select 'chbPabsHourlyAbsence' check box
        /// </summary>
        public bool UIChbPabsHourlyAbsenceCheckBoxChecked = true;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ConstructRemanageShift'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ConstructRemanageShiftParams
    {

        #region Fields
        /// <summary>
        /// Type 'OvertimeCode(Id=242481501)' in 'leOvertimeCode' LookUpEdit
        /// </summary>
        public string UILeOvertimeCodeLookUpEditValueAsString = "OvertimeCode(Id=242481501)";

        /// <summary>
        /// Select 'CheckEdit' check box
        /// </summary>
        public bool UICheckEditCheckBoxChecked = true;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ClickOkRemaning'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ClickOkRemaningParams
    {

        #region Fields
        /// <summary>
        /// Type 'System.String' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueTypeName = "System.String";

        /// <summary>
        /// Type 'MENGDE' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueAsString = "MENGDE";

        /// <summary>
        /// Type 'Aml test 055' in 'eComment' text box
        /// </summary>
        public string UIECommentEditValueAsString = "Test 55 Aml, Forskjøvetvakt.";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectFreeShiftToRemanning'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectFreeShiftToRemanningParams
    {

        #region Fields
        /// <summary>
        /// Select 'CheckEdit' check box
        /// </summary>
        public bool UICheckEditCheckBox2Checked = true;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetGradedData'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetGradedDataParams
    {

        #region Fields
        /// <summary>
        /// Type '1' in 'rgPartialAbsenceType' RadioGroup
        /// </summary>
        public int UIRgPartialAbsenceTypeRadioGroupSelectedIndex = 1;

        /// <summary>
        /// Type '50 [SelectionStart]0[SelectionLength]2' in 'eNumber[1]' text box
        /// </summary>
        public string UIENumber1EditValueAsString = "50 [SelectionStart]0[SelectionLength]2";

        /// <summary>
        /// Type '{NumPad0}{Tab}' in 'eNumber[1]' text box
        /// </summary>
        public string UITab = "{Tab}";

        /// <summary>
        /// Type '13.00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
        /// </summary>
        public string UIETime1EditValueAsString = "13.00 [SelectionStart]0[SelectionLength]5";

        /// <summary>
        /// Type '12.00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
        /// </summary>
        public string UIETime1EditValueAsString1 = "12.00 [SelectionStart]0[SelectionLength]5";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetDatePeriodInShiftListControl'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetDatePeriodInShiftListControlParams
    {

        #region Fields
        /// <summary>
        /// Type '{Tab}' in 'gUnoccupiedShifts' table
        /// </summary>
        public string UITab = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CreateExchange'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CreateExchangeParams
    {

        #region Fields
        /// <summary>
        /// Type '{NumPad1}{NumPad6}{Tab}' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UITab = "{Tab}";

        /// <summary>
        /// Type 'System.String' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueTypeName = "System.String";

        /// <summary>
        /// Type 'MENGDE' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueAsString = "MENGDE";

        /// <summary>
        /// Type 'Test 55 Aml, Bytte.' in 'eComment' text box
        /// </summary>
        public string UIECommentEditValueAsString = "Test 55 Aml, Bytte.";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckAmlSettingsUseOrgShiftDepartmentExchange'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckAmlSettingsUseOrgShiftDepartmentExchangeParams
    {

        #region Fields
        /// <summary>
        /// Clear 'Bruk orginalvakt som grunnlag ved avdelingsbytte' check box
        /// </summary>
        public bool UIBrukorginalvaktsomgrCheckBox1Checked = false;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CreateCallOut'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CreateCallOutParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Callout.CalloutDTO+CauseCode' in '_cbCauseCode' LookUpEdit
        /// </summary>
        public string UI_cbCauseCodeLookUpEditValueAsString = "Gatsoft.Gat.BusinessLogic.Callout.CalloutDTO+CauseCode";

        /// <summary>
        /// Type '{Down}' in '_cbCauseCode' LookUpEdit
        /// </summary>
        public string UI_cbCauseCodeLookUpEditSendKeys = "{Down}";

        /// <summary>
        /// Type '{Enter}' in 'PopupLookUpEditForm' window
        /// </summary>
        public string UIPopupLookUpEditFormWindowSendKeys = "{Enter}";

        /// <summary>
        /// Type '{Tab}' in 'eTime[0]' text box
        /// </summary>
        public string UIETime0EditSendKeys = "{Tab}";

        /// <summary>
        /// Type '{Tab}' in 'eTime[1]' text box
        /// </summary>
        public string UIETime1EditSendKeys = "{Tab}";

        /// <summary>
        /// Type '2 brudd (klikk her for detaljer) [SelectionStart]0' in 'lnkDetail[0]' PopupEdit
        /// </summary>
        public string UILnkDetail0PopupEditValueAsString = "2 brudd (klikk her for detaljer) [SelectionStart]0";

        /// <summary>
        /// Type 'System.String' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueTypeName = "System.String";

        /// <summary>
        /// Type 'MENGDE' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueAsString = "MENGDE";

        /// <summary>
        /// Type 'Test 55 Aml, Utrykning.' in 'eComment' text box
        /// </summary>
        public string UIECommentEditValueAsString = "Test 55 Aml, Utrykning.";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ChangeAmlDayWorkerMinBeforeShift'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ChangeAmlDayWorkerMinBeforeShiftParams
    {

        #region Fields
        /// <summary>
        /// Type 'System.Double' in 'sdeMinFreeBeforeShift' text box
        /// </summary>
        public string UISdeMinFreeBeforeShifEditValueTypeName = "System.Double";

        /// <summary>
        /// Type '9' in 'sdeMinFreeBeforeShift' text box
        /// </summary>
        public string UISdeMinFreeBeforeShifEditValueAsString = "9";

        /// <summary>
        /// Type '{Tab}' in 'sdeMinFreeBeforeShift' text box
        /// </summary>
        public string UISdeMinFreeBeforeShifEditSendKeys = "{Tab}";
        #endregion
    }

    /// <summary>
    /// Parameters to be passed into 'ChangeAmlDnlf38FreeBeforeShift'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ChangeAmlDnlf38FreeBeforeShiftParams
    {

        #region Fields
        /// <summary>
        /// Select 'chkFreeBeforeShift' check box
        /// </summary>
        public bool UIChkFreeBeforeShiftCheckBoxChecked = true;

        /// <summary>
        /// Type 'System.Double' in 'sdeMinFreeBeforeShift' text box
        /// </summary>
        public string UISdeMinFreeBeforeShifEditValueTypeName = "System.Double";

        /// <summary>
        /// Type '9' in 'sdeMinFreeBeforeShift' text box
        /// </summary>
        public string UISdeMinFreeBeforeShifEditValueAsString = "9";

        /// <summary>
        /// Type '{Tab}' in 'lueOvertimeSsd' LookUpEdit
        /// </summary>
        public string UITab = "{Tab}";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetAmlTimerPrUke'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetAmlTimerPrUkeParams
    {

        #region Fields
        /// <summary>
        /// Type 'System.Double' in 'sdeWeaHoursPerWeek' text box
        /// </summary>
        public string UISdeWeaHoursPerWeekEditValueTypeName = "System.Double";

        /// <summary>
        /// Type '5' in 'sdeWeaHoursPerWeek' text box
        /// </summary>
        public string UISdeWeaHoursPerWeekEditValueAsString = "5";

        /// <summary>
        /// Type '{NumPad5}{Tab}' in 'sdeWeaHoursPerWeek' text box
        /// </summary>
        public string UISdeWeaHoursPerWeekEditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetSamletTidPrUke'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetSamletTidPrUkeParams
    {

        #region Fields
        /// <summary>
        /// Select 'chkBreakTypeTotalHoursPerWeek' check box
        /// </summary>
        public bool UIChkBreakTypeTotalHouCheckBoxChecked = true;

        /// <summary>
        /// Type 'System.Double' in 'sdeMaxTotalHoursPerWeek' text box
        /// </summary>
        public string UISdeMaxTotalHoursPerWEditValueTypeName = "System.Double";

        /// <summary>
        /// Type '15' in 'sdeMaxTotalHoursPerWeek' text box
        /// </summary>
        public string UISdeMaxTotalHoursPerWEditValueAsString = "15";

        /// <summary>
        /// Type '{NumPad5}{Tab}' in 'sdeMaxTotalHoursPerWeek' text box
        /// </summary>
        public string UISdeMaxTotalHoursPerWEditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddNewAmlLimit'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddNewAmlLimitParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Date' in 'sdeFromDate' text box
        /// </summary>
        public string UISdeFromDateEditValueTypeName = "Gatsoft.Date";

        /// <summary>
        /// Type 'Test 55' in 'teComment' text box
        /// </summary>
        public string UITeCommentEditValueAsString = "Test 55";

        /// <summary>
        /// Type '5{Tab}' in 'teComment' text box
        /// </summary>
        public string UITab = "{Tab}";

        /// <summary>
        /// Type 'System.Double' in 'sdeMaxTotalHoursPerDay' text box
        /// </summary>
        public string UISdeMaxTotalHoursPerDEditValueTypeName = "System.Double";

        /// <summary>
        /// Type '15' in 'sdeMaxTotalHoursPerDay' text box
        /// </summary>
        public string UISdeMaxTotalHoursPerDEditValueAsString = "5";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ChangeAmlTurnus35_5'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ChangeAmlTurnus35_5Params
    {

        #region Fields
        /// <summary>
        /// Type 'System.Int32' in 'sdePriority' text box
        /// </summary>
        public string UISdePriorityEditValueTypeName = "System.Int32";

        /// <summary>
        /// Type '{Tab}' in 'sdePriority' text box
        /// </summary>
        public string UISdePriorityEditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'NewEmployment'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class NewEmploymentParams
    {

        #region Fields
        /// <summary>
        /// Type '21.10.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "21.10.2016 [SelectionStart]0[SelectionLength]10";

        /// <summary>
        /// Type '31.12.2099 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "31.12.2099 [SelectionStart]0[SelectionLength]10";

        /// <summary>
        /// Type '40 [SelectionStart]0[SelectionLength]4' in 'sePositionPercent' text box
        /// </summary>
        public string UISePositionPercentEditValueAsString = "40 [SelectionStart]0[SelectionLength]4";

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+RuleSetDisplay' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEditValueTypeName = "Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+Ru" +
            "leSetDisplay";

        /// <summary>
        /// Type 'L38 - Lege 38t(35.5+2.5) (38 t/uke)' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEditValueAsString = "L38 - Lege 38t(35.5+2.5) (38 t/uke)";

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+PositionCategoryDisplay' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit1ValueTypeName = "Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+Po" +
            "sitionCategoryDisplay";

        /// <summary>
        /// Type 'L - Lege' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit1ValueAsString = "L - Lege";

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+WeaAgreementDisplay' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit2ValueTypeName = "Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+We" +
            "aAgreementDisplay";

        /// <summary>
        /// Type 'AML 38t' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit2ValueAsString = "AML 38t";

        /// <summary>
        /// Type '{Tab}' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit2SendKeys = "{Tab}";

        /// <summary>
        /// Type '1234' in 'teInternalPositionNumber' text box
        /// </summary>
        public string UITeInternalPositionNuEditValueAsString = "1234";

        /// <summary>
        /// Type '{Tab}' in 'teInternalPositionNumber' text box
        /// </summary>
        public string UITeInternalPositionNuEditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CreateAmlCalculation'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CreateAmlCalculationParams
    {

        #region Fields
        /// <summary>
        /// Select '2060 - AML avdeling 1' in combo box
        /// </summary>
        public string UIItemComboBoxSelectedItem = "2060 - AML avdeling 1";

        /// <summary>
        /// Type '10:00' in text box
        /// </summary>
        public string UICalcTime = DateTime.Now.AddMinutes(3).ToShortTimeString();

        /// <summary>
        /// Type '{Tab}' in text box
        /// </summary>
        public string UIItemEditSendKeys = "{Tab}";

        /// <summary>
        /// Select 'Mandag' check box
        /// </summary>
        public bool UIMandagCheckBoxChecked = true;

        /// <summary>
        /// Select 'Tirsdag' check box
        /// </summary>
        public bool UITirsdagCheckBoxChecked = true;

        /// <summary>
        /// Select 'Onsdag' check box
        /// </summary>
        public bool UIOnsdagCheckBoxChecked = true;

        /// <summary>
        /// Select 'Torsdag' check box
        /// </summary>
        public bool UITorsdagCheckBoxChecked = true;

        /// <summary>
        /// Select 'Fredag' check box
        /// </summary>
        public bool UIFredagCheckBoxChecked = true;

        /// <summary>
        /// Select 'Lørdag' check box
        /// </summary>
        public bool UILørdagCheckBoxChecked = true;

        /// <summary>
        /// Select 'Søndag' check box
        /// </summary>
        public bool UISøndagCheckBoxChecked = true;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'OpenWeaBreakService'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class OpenWeaBreakServiceParams
    {

        #region Fields
        /// <summary>
        /// Type '{Tab}' in 'calculatingYear' text box
        /// </summary>
        public string UITab = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CreateWeekSeparation'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CreateWeekSeparationParams
    {

        #region Fields
        /// <summary>
        /// Type '25.10.2016' in text box
        /// </summary>
        public string UIItemEditText = "25.10.2016";

        /// <summary>
        /// Type '{Tab}' in text box
        /// </summary>
        public string UITab = "{Tab}";

        /// <summary>
        /// Type '00:00' in text box
        /// </summary>
        public string UIItemEditText1 = "00:00";

        /// <summary>
        /// Type '{Tab}' in text box
        /// </summary>
        public string UIItemEditSendKeys1 = "{Tab}";

        /// <summary>
        /// Type 'Test{Space}55{Tab}' in 'Døgn- og ukeskille' client
        /// </summary>
        public string UIDøgnogukeskilleClientSendKeys = "Test{Space}55{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ApproveAmlWarningDialogExtra'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ApproveAmlWarningDialogExtraParams
    {

        #region Fields
        /// <summary>
        /// Type 'System.String' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueTypeName = "System.String";

        /// <summary>
        /// Type 'MENGDE' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueAsString = "MENGDE";

        /// <summary>
        /// Type '{Enter}{Tab}' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditSendKeys = "{Enter}{Tab}";

        /// <summary>
        /// Type 'Test 55 Aml, Extra.' in 'eComment' text box
        /// </summary>
        public string UIECommentEditValueAsString = "Test 55 Aml, Extra.";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddCarlBildtShifts'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddCarlBildtShiftsParams
    {

        #region Fields
        /// <summary>
        /// Type 'e1' in '[Row]9[Column]RosterCell_0' text box
        /// </summary>
        public string UIRow9ColumnRosterCellEditValueAsString = "e1";

        /// <summary>
        /// Type '{Tab}' in '[Row]9[Column]RosterCell_0' text box
        /// </summary>
        public string UIRow9ColumnRosterCellEditSendKeys = "{Tab}";

        /// <summary>
        /// Type 'e1' in '[Row]9[Column]RosterCell_7' text box
        /// </summary>
        public string UIRow9ColumnRosterCellEdit1ValueAsString = "e1";

        /// <summary>
        /// Type '{Tab}' in '[Row]9[Column]RosterCell_7' text box
        /// </summary>
        public string UIRow9ColumnRosterCellEdit1SendKeys = "{Tab}";

        /// <summary>
        /// Type 'e1' in '[Row]9[Column]RosterCell_14' text box
        /// </summary>
        public string UIRow9ColumnRosterCellEdit2ValueAsString = "e1";

        /// <summary>
        /// Type '{Tab}' in '[Row]9[Column]RosterCell_14' text box
        /// </summary>
        public string UIRow9ColumnRosterCellEdit2SendKeys = "{Tab}";

        /// <summary>
        /// Type 'e1' in '[Row]9[Column]RosterCell_21' text box
        /// </summary>
        public string UIRow9ColumnRosterCellEdit3ValueAsString = "e1";

        /// <summary>
        /// Type '{Tab}' in '[Row]9[Column]RosterCell_21' text box
        /// </summary>
        public string UIRow9ColumnRosterCellEdit3SendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetAmlDnlf38FreeBeforeShift'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetAmlDnlf38FreeBeforeShiftParams
    {

        #region Fields
        /// <summary>
        /// Select 'chkFreeBeforeShift' check box
        /// </summary>
        public bool UIChkFreeBeforeShiftCheckBoxChecked = true;

        /// <summary>
        /// Type 'System.Double' in 'sdeMinFreeBeforeShift' text box
        /// </summary>
        public string UISdeMinFreeBeforeShifEditValueTypeName = "System.Double";

        /// <summary>
        /// Type '9{TAB}' in 'sdeMinFreeBeforeShift' text box
        /// </summary>
        public string UISdeMinFreeBeforeShifEditValueAsString = "9";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Agreement.ViewModels.SevereDisruptionServiceTypeViewModel' in 'lueOvertimeSsd' LookUpEdit
        /// </summary>
        public string UILueOvertimeSsdLookUpEditValueAsString = "Gatsoft.Gat.Wea.UI.Agreement.ViewModels.SevereDisruptionServiceTypeViewModel";

        /// <summary>
        /// Type '{Tab}' in 'lueOvertimeSsd' LookUpEdit
        /// </summary>
        public string UILueOvertimeTab = "{Tab}";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Agreement.ViewModels.SevereDisruptionServiceTypeViewModel' in 'lueTurnoutSsd' LookUpEdit
        /// </summary>
        public string UILueTurnoutSsdLookUpEditValueAsString = "Gatsoft.Gat.Wea.UI.Agreement.ViewModels.SevereDisruptionServiceTypeViewModel";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectBosseEmp2Exchange'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectBosseEmp2ExchangeParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment' in 'sleEmployment2' LookUpEdit
        /// </summary>
        public string UISleEmployment2LookUpEditValueAsString = "Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectEmp1Exchange'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectEmp1ExchangeParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment' in 'sleEmployment1' LookUpEdit
        /// </summary>
        public string UISleEmployment1LookUpEditValueAsString = "Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectEmp1ExchangeFromSearch'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectEmp1ExchangeFromSearchParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment' in 'sleEmployment1' LookUpEdit
        /// </summary>
        public string UISleEmployment1LookUpEditValueAsString = "Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment";

        /// <summary>
        /// Type 'celsius{Enter}' in 'teFind' text box
        /// </summary>
        public string UITeFindEditSendKeys = "celsius{Enter}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectEmp2ExchangeFromSearch'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectEmp2ExchangeFromSearchParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment' in 'sleEmployment2' LookUpEdit
        /// </summary>
        public string UISleEmployment2LookUpEditValueAsString = "Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment";

        /// <summary>
        /// Type 'brun{Enter}' in 'teFind' text box
        /// </summary>
        public string UITeFindEditSendKeys = "brun{Enter}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CreateAmlDispension'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CreateAmlDispensionParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditViewModel = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";

        /// <summary>
        /// Type 'Gatsoft.Date' in 'sdeStart' text box
        /// </summary>
        public string UISdeStartEditValueTypeName = "Gatsoft.Date";


        /// <summary>
        /// Type 'Automated test 55' in 'memoComment' text box
        /// </summary>
        public string UIMemoCommentEditSendKeys = "Automated test 55";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectAmlDispType'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectAmlDispTypeParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditValueAsString = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditValueAsString1 = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditValueAsString2 = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditValueAsString3 = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditValueAsString4 = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditValueAsString5 = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditValueAsString6 = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditValueAsString7 = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditValueAsString8 = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";

        /// <summary>
        /// Type 'Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel' in 'dropdwnDispensationType' LookUpEdit
        /// </summary>
        public string UIDropdwnDispensationTLookUpEditValueAsString9 = "Gatsoft.Gat.Wea.UI.Dispensation.ViewModels.DispensationTypeViewModel";
        #endregion
    }
}

