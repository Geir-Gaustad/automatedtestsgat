namespace _045_Test_Egengodkjenning_Bytter_Avdelingsbytter_GATW_4850
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
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
    using System.Threading;
    using System.Globalization;
    using System.IO;

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        #endregion

        #region Global Variable Declarations
        //MinGat

        private string TargetAddressZipFiles = @"\\IPVSLTCTFS001\TeamCity\Gat\";
        private static string DestinationAddressZipFiles = Path.Combine(TestData.GetWorkingDirectory, @"ZipFiles");
        private string UIExtractedGatFiles = DestinationAddressZipFiles + @"\Gat_no";
        private string MinGatAppsettingsConfig = DestinationAddressZipFiles + @"\MinGat_no\Template\appSettings.config";
        private string MinGatConnectionStringsConfig = DestinationAddressZipFiles + @"\MinGat_no\Template\connectionStrings.config";
        private string LineToFindMinGatAppsettings = @"<add key=""WishPlanWSAddress"" value=""http://myserver/gatws2/WishPlanWebService.asmx""/>";
        private string UIExtractedMinGatFiles = DestinationAddressZipFiles + @"\MinGat_no";
        private string LineToFindConnectionString = @"<add name=""default"" connectionString=""TYPE=MSSQL2008;HOSTNAME='HOSTNAME';DATABASE='DATABASE';USERNAME='USERNAME';PASSWORD='PASSWORD'"" />";
        private string UIWWWRootMinGatDir = @"C:\inetpub\wwwroot\MinGat";
        private string IISManager = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools\IIS Manager.lnk";
        private BrowserWindow MinGatBrowser;
        #endregion

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }

        #region Common Methods
        #region Properties
        public string Dep2040
        {
            get { return UICommon.DepDrommeAvdelingen; }
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
        private string MinGatWishPlanKey()
        {
            var wishPlan = "";
            try
            {
                wishPlan = GetWishPlanKey(CurrentWSHost);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Unable to get wishPlankey: " + ex.Message);
            }

            return wishPlan;

        }

        private string CurrentServer
        {
            get
            {
                try
                {
                    return TestData.GetDatabaseServer;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get Servername: " + ex.Message);
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

        private string CurrentDBAuth(bool decrypt = true)
        {
            try
            {
                return TestData.GetDBAuth(decrypt);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Unable to get authentication: " + ex.Message);
            }

            return null;
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
        private string GetWishPlanKey(string server)
        {
            var wishPlan = @"<add key=""WishPlanWSAddress"" value=""https://" + server + @".internal.visma.com/gatWs2/WishPlanWebService.asmx""/>";
            return wishPlan;
        }

        #endregion

        public bool RestoreDatabase()
        {
            return UICommon.RestoreDatabase();
        }

        public void GetZipFiles()
        {
            var filesToCopy = new List<string>() { "Gat NOR", "MinGat NOR" };
            UICommon.UIMapVS2017.GetGatZipFiles(filesToCopy, TargetAddressZipFiles + CurrentTargetFilesAddress, DestinationAddressZipFiles, CurrentFileVersion, TestContext);
        }
        public void ExtractGatFiles()
        {
            var filePath = DestinationAddressZipFiles + @"\Gat NOR " + CurrentFileVersion + ".zip";
            UICommon.UIMapVS2017.ExtractGatFiles(filePath, TestContext);
        }
        public string GetMinGatPath()
        {
            var url = @"https://" + CurrentWSHost + @".internal.visma.com/mingat";
            return url;
        }
        public void ConfigureMinGatForIIS()
        {
            var filePath = DestinationAddressZipFiles + @"\MinGat NOR " + CurrentFileVersion + ".zip";

            UICommon.UIMapVS2017.ConfigureMinGatForIIS(
              filePath, UIExtractedMinGatFiles,
              MinGatAppsettingsConfig,
              LineToFindMinGatAppsettings,
              MinGatWishPlanKey(), MinGatConnectionStringsConfig,
              LineToFindConnectionString,
              UICommon.UIMapVS2017.SqlConnection(), UIWWWRootMinGatDir, TestContext);
        }
        public void ConfigureIIS()
        {
            //MinGat
            Playback.Wait(2000);
            AddApplicationPoolIIS("MinGat", true, true);
            Playback.Wait(2000);
        }
        public void UpgradeGatDB()
        {
            StartGatFromExtractedDir("", false);
            CloseGat();
        }
        private List<string> MingatStartAndLogin()
        {
            var errorList = new List<string>();

            KillProcessByName("iexplore");
            MinGatBrowser = BrowserWindow.Launch(GetMinGatPath());

            try
            {
                UICommon.UIMapVS2017.MinGatLogin("EGEN");
            }
            catch (Exception e)
            {
                errorList.Add("Error login MInGat: " + e.Message);
            }

            return errorList;
        }
        private List<string> CloseBrowserWindow()
        {
            var errorList = new List<string>();

            if (MinGatBrowser != null)
            {
                try
                {
                    Playback.Wait(1500);
                    MinGatBrowser.Close();
                }
                catch (Exception e)
                {
                    errorList.Add("Error closing MInGat: " + e.Message);
                    UICommon.UIMapVS2017.CloseIE();
                }
            }

            KillProcessByName("iexplore");
            return errorList;
        }
        public void StartGatFromExtractedDir(string department, bool logVersion)
        {
            UICommon.StartGatFromExtractedDir(department, DestinationAddressZipFiles, logVersion);
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
        public void AssertResults(List<string> errorList)
        {
            UICommon.AssertResults(errorList);
        }
        private void KillProcessByName(string process)
        {
            SupportFunctions.KillProcessByName(process, TestContext);
        }
        private void KillGatProcess()
        {
            SupportFunctions.KillGatProcess(TestContext);
        }

        private void GoToShiftDateNew(DateTime date)
        {
            UICommon.GoToShiftbookdate(date);
        }
        public void AddApplicationPoolIIS(string poolName, bool startIIS, bool closeIIS)
        {
            if (startIIS)
                SupportFunctions.StartProcess(IISManager, true);

            Playback.Wait(1000);
            UICommon.AddApplicationPoolIIS(poolName, CurrentFrameWorkVersion);

            Playback.Wait(1000);
            UICommon.ConvertToAppInIIS(poolName);
            Playback.Wait(1000);

            if (closeIIS)
            {
                UICommon.CloseIIS();
            }

        }
        #endregion

        public List<string> Step_1()
        {
            //UICommon.UIMapVS2017.MinGatSelectReminders();
            //UICommon.UIMapVS2017.MinGatSelectTaskView();
            //UICommon.UIMapVS2017.MinGatSelectShiftbook();
            //UICommon.UIMapVS2017.MinGatSelectCalendar();
            //UICommon.UIMapVS2017.MinGatSelectStartpage();         
            //UICommon.UIMapVS2017.MinGatSelectBanks();
            //UICommon.UIMapVS2017.MinGatSelectWeekView();
            //UICommon.UIMapVS2017.MinGatSelectOpenMenu();
            //UICommon.UIMapVS2017.MinGatLogOut();

            var errorList = new List<string>();

            try
            {
                errorList.AddRange(MingatStartAndLogin());
                try
                {
                    UICommon.UIMapVS2017.MinGatSelectRequests();
                }
                catch
                {
                    Playback.Wait(5000);
                    UICommon.UIMapVS2017.MinGatSelectRequests();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 1: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_2()
        {
            var errorList = new List<string>();


            CreateExchangeRequestStep2();

            try
            {
                try
                {
                    CheckRequestCreatedStep2();
                }
                catch
                {
                    Playback.Wait(5000);
                    CheckRequestCreatedStep2();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2: " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.MinGatSelectOpenMenu();
            }
            catch
            {
                Playback.Wait(5000);
                UICommon.UIMapVS2017.MinGatSelectOpenMenu();
            }
            try
            {
                UICommon.UIMapVS2017.MinGatLogOut();
            }
            catch
            {
                Playback.Wait(5000);
                UICommon.UIMapVS2017.MinGatLogOut();
            }

            errorList.AddRange(CloseBrowserWindow());

            try
            {
                StartGatFromExtractedDir(Dep2040, true);
                GoToShiftDateNew(new DateTime(2020, 10, 7));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2: " + e.Message);
            }

            try
            {
                CheckRequestInGatStep2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2: " + e.Message);
            }

            return errorList;
        }
        private void CreateExchangeRequestStep2()
        {
            #region Variable Declarations
            HtmlSpan uINyforespørselPane1 = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UINyforespørselPane.UINyforespørselPane1;
            HtmlHyperlink uIVaktbytteHyperlink = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UISelfServiceListPagePane.UIVaktbytteHyperlink;
            HtmlComboBox uIByttetypeComboBox = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIByttetypeComboBox;
            HtmlCheckBox uIExchangeSelfServiceRCheckBox = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIExchangeSelfServiceRCheckBox;
            HtmlEdit uIDagjegønskerfriEdit = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIDagjegønskerfriEdit;
            HtmlComboBox uIA16002300ComboBox = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIA16002300ComboBox;
            HtmlSpan uIItemPane = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIExchangeSelfServiceRPane.UIItemPane;
            HtmlCustom uIUiid56Custom = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIUiid56Custom;
            HtmlSpan uILagrePane = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UILagreButton.UILagrePane;
            #endregion

            try
            {
                // Click 'Ny forespørsel' pane
                Mouse.Click(uINyforespørselPane1, new Point(75, 15));
            }
            catch
            {
                Playback.Wait(5000);
                // Click 'Ny forespørsel' pane
                Mouse.Click(uINyforespørselPane1, new Point(75, 15));
            }
            try
            {
                // Click 'Vaktbytte' link
                Mouse.Click(uIVaktbytteHyperlink, new Point(76, 25));
            }
            catch
            {
                Playback.Wait(5000);
                // Click 'Vaktbytte' link
                Mouse.Click(uIVaktbytteHyperlink, new Point(76, 25));
            }

            Playback.Wait(5000);
            this.UIForespørslerMinGatv2Window.DrawHighlight();
            uIByttetypeComboBox.WaitForControlReady();
            try
            {
                // Select 'Vaktbytte: Gi bort vakt til annen ansatt.' in 'Byttetype' combo box
                uIByttetypeComboBox.SelectedItem = "Vaktbytte: Gi bort vakt til annen ansatt.";

            }
            catch
            {
                Playback.Wait(5000);
                // Select 'Vaktbytte: Gi bort vakt til annen ansatt.' in 'Byttetype' combo box
                uIByttetypeComboBox.SelectedItem = "Vaktbytte: Gi bort vakt til annen ansatt.";

            }

            // Select 'exchangeSelfServiceRegistration_selfapproval' check box
            var point = new Point(uIExchangeSelfServiceRCheckBox.BoundingRectangle.X + 3, uIExchangeSelfServiceRCheckBox.BoundingRectangle.Y + 3);

            try
            {
                // uIExchangeSelfServiceRCheckBox.Checked = this.CreateExchangeRequestStep1Params.UIExchangeSelfServiceRCheckBoxChecked;
                Mouse.Click(point);
            }
            catch
            {
                Playback.Wait(5000);
                // uIExchangeSelfServiceRCheckBox.Checked = this.CreateExchangeRequestStep1Params.UIExchangeSelfServiceRCheckBoxChecked;
                Mouse.Click(point);
            }

            try
            {
                // Type '07.10.2020' in 'Dag jeg ønsker fri' text box
                uIDagjegønskerfriEdit.Text = "07.10.2020";
            }
            catch
            {
                Playback.Wait(5000);
                // Type '07.10.2020' in 'Dag jeg ønsker fri' text box
                uIDagjegønskerfriEdit.Text = "07.10.2020";
            }

            try
            {
                // Type '{Tab}' in 'Dag jeg ønsker fri' text box
                Keyboard.SendKeys(uIDagjegønskerfriEdit, "{Tab}");
            }
            catch
            {
                Playback.Wait(5000);
                // Type '{Tab}' in 'Dag jeg ønsker fri' text box
                Keyboard.SendKeys(uIDagjegønskerfriEdit, "{Tab}");
            }
            //    Playback.Wait(3000);
            //    uIItemPane.WaitForControlReady();
            //    uIItemPane.DrawHighlight();
            //    Mouse.Click(uIItemPane, new Point(6, 18));          
            //// Click 'ui-id-56' custom control
            //Mouse.Click(uIUiid56Custom, new Point(227, 16));

            Keyboard.SendKeys("{Tab 3}{ENTER}");
            Playback.Wait(1000);
            Keyboard.SendKeys("{DOWN}{ENTER}");

            try
            {
                // Click 'Lagre' pane
                Mouse.Click(uILagrePane, new Point(56, 10));
            }
            catch
            {
                Playback.Wait(5000);
                // Click 'Lagre' pane
                Mouse.Click(uILagrePane, new Point(56, 10));
            }
        }
        private void CheckRequestCreatedStep2()
        {
            #region Variable Declarations
            HtmlSpan uIVaktbytteØnskerågiboPane = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document1.UIApprovedSelfServicesPane.UIVaktbytteØnskerågiboPane;
            HtmlSpan uIGodkjentPane = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document1.UIApprovedSelfServicesPane.UIGodkjentPane;
            HtmlDiv uIOktPane = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document1.UIUiid14Custom.UIOktPane;
            HtmlDiv uIItem07Pane = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document1.UIUiid14Custom.UIItem07Pane;
            #endregion
            try
            {
                UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document1.DrawHighlight();
                uIVaktbytteØnskerågiboPane.WaitForControlReady();
            }
            catch
            {
                Playback.Wait(5000);
                UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document1.UIApprovedSelfServicesPane.DrawHighlight();
                uIVaktbytteØnskerågiboPane.WaitForControlReady();
            }

            // Verify that the 'InnerText' property of 'Vaktbytte - Ønsker å gi bort min vakt ti' pane equals 'Vaktbytte - Ønsker å gi bort min vakt til Byttes Med, Kan Gjerne'
            Assert.AreEqual("Vaktbytte - Ønsker å gi bort min vakt til Byttes Med, Kan Gjerne", uIVaktbytteØnskerågiboPane.InnerText);

            // Verify that the 'InnerText' property of 'Godkjent' pane equals 'Godkjent'
            Assert.AreEqual("Godkjent", uIGodkjentPane.InnerText);

            // Verify that the 'InnerText' property of 'okt' pane equals 'okt'
            Assert.AreEqual("okt", uIOktPane.InnerText);

            // Verify that the 'InnerText' property of '07' pane equals '07'
            Assert.AreEqual("07", uIItem07Pane.InnerText);
        }

        public List<string> Step_3()
        {
            var errorList = new List<string>();

            try
            {
                errorList.AddRange(MingatStartAndLogin());
                try
                {
                    UICommon.UIMapVS2017.MinGatSelectRequests();
                }
                catch
                {
                    Playback.Wait(5000);
                    UICommon.UIMapVS2017.MinGatSelectRequests();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            CreateExchangeRequestStep3();

            try
            {
                try
                {
                    CheckRequestCreatedStep3_1();
                }
                catch
                {
                    Playback.Wait(5000);
                    CheckRequestCreatedStep3();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.MinGatSelectOpenMenu();
            }
            catch
            {
                Playback.Wait(5000);
                UICommon.UIMapVS2017.MinGatSelectOpenMenu();
            }
            try
            {
                UICommon.UIMapVS2017.MinGatLogOut();
            }
            catch
            {
                Playback.Wait(5000);
                UICommon.UIMapVS2017.MinGatLogOut();
            }

            errorList.AddRange(CloseBrowserWindow());

            //Gat
            try
            {
                GoToShiftDateNew(new DateTime(2020, 10, 9));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            try
            {
                CheckRequestInGatStep3();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            return errorList;
        }
        private void CreateExchangeRequestStep3()
        {
            #region Variable Declarations
            HtmlSpan uINyforespørselPane1 = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UINyforespørselPane.UINyforespørselPane1;
            HtmlHyperlink uIVaktbytteHyperlink = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UISelfServiceListPagePane.UIVaktbytteHyperlink;
            HtmlComboBox uIByttetypeComboBox = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIByttetypeComboBox;
            HtmlCheckBox uIExchangeSelfServiceRCheckBox = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIExchangeSelfServiceRCheckBox;
            HtmlEdit uIDagjegønskerfriEdit = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIDagjegønskerfriEdit;
            HtmlComboBox uIA16002300ComboBox = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIA16002300ComboBox;
            HtmlCustom uIUiid56Custom = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIUiid56Custom;
            HtmlSpan uILagrePane = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UILagreButton.UILagrePane;
            #endregion

            try
            {
                // Click 'Ny forespørsel' pane
                Mouse.Click(uINyforespørselPane1, new Point(75, 15));
            }
            catch
            {
                Playback.Wait(5000);
                // Click 'Ny forespørsel' pane
                Mouse.Click(uINyforespørselPane1, new Point(75, 15));
            }
            try
            {
                // Click 'Vaktbytte' link
                Mouse.Click(uIVaktbytteHyperlink, new Point(76, 25));
            }
            catch
            {
                Playback.Wait(5000);
                // Click 'Vaktbytte' link
                Mouse.Click(uIVaktbytteHyperlink, new Point(76, 25));
            }

            Playback.Wait(5000);
            this.UIForespørslerMinGatv2Window.DrawHighlight();
            uIByttetypeComboBox.WaitForControlReady();
            try
            {
                // Select 'Vaktbytte: Gi bort vakt til annen ansatt.' in 'Byttetype' combo box
                uIByttetypeComboBox.SelectedItem = "Vaktbytte: Få fri vakt (bytte med avdeling).";

            }
            catch
            {
                Playback.Wait(5000);
                // Select 'Vaktbytte: Gi bort vakt til annen ansatt.' in 'Byttetype' combo box
                uIByttetypeComboBox.SelectedItem = "Vaktbytte: Få fri vakt (bytte med avdeling).";
            }

            // Select 'exchangeSelfServiceRegistration_selfapproval' check box
            var point = new Point(uIExchangeSelfServiceRCheckBox.BoundingRectangle.X + 3, uIExchangeSelfServiceRCheckBox.BoundingRectangle.Y + 3);

            try
            {
                // uIExchangeSelfServiceRCheckBox.Checked = this.CreateExchangeRequestStep1Params.UIExchangeSelfServiceRCheckBoxChecked;
                Mouse.Click(point);
            }
            catch
            {
                Playback.Wait(5000);
                // uIExchangeSelfServiceRCheckBox.Checked = this.CreateExchangeRequestStep1Params.UIExchangeSelfServiceRCheckBoxChecked;
                Mouse.Click(point);
            }

            try
            {
                // Type '07.10.2020' in 'Dag jeg ønsker fri' text box
                uIDagjegønskerfriEdit.Text = "09.10.2020";
            }
            catch
            {
                Playback.Wait(5000);
                // Type '07.10.2020' in 'Dag jeg ønsker fri' text box
                uIDagjegønskerfriEdit.Text = "09.10.2020";
            }

            try
            {
                // Type '{Tab}' in 'Dag jeg ønsker fri' text box
                Keyboard.SendKeys(uIDagjegønskerfriEdit, "{Tab}");
            }
            catch
            {
                Playback.Wait(5000);
                // Type '{Tab}' in 'Dag jeg ønsker fri' text box
                Keyboard.SendKeys(uIDagjegønskerfriEdit, "{Tab}");
            }

            try
            {
                // Click 'Lagre' pane
                Mouse.Click(uILagrePane, new Point(56, 10));
            }
            catch
            {
                Playback.Wait(5000);
                // Click 'Lagre' pane
                Mouse.Click(uILagrePane, new Point(56, 10));
            }
        }
        private void CheckRequestCreatedStep3_1()
        {
            #region Variable Declarations
            HtmlDiv uIOktPane = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIUiid11Custom.UIOktPane;
            #endregion
            this.UIForespørslerMinGatv2Window.DrawHighlight();
            uIOktPane.WaitForControlReady();

            CheckRequestCreatedStep3();
        }

        public List<string> Step_4()
        {
            var errorList = new List<string>();

            try
            {
                errorList.AddRange(MingatStartAndLogin());
                try
                {
                    UICommon.UIMapVS2017.MinGatSelectRequests();
                }
                catch
                {
                    Playback.Wait(5000);
                    UICommon.UIMapVS2017.MinGatSelectRequests();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            CreateExchangeRequestStep4();

            return errorList;
        }
        private void CreateExchangeRequestStep4()
        {
            #region Variable Declarations
            HtmlSpan uINyforespørselPane1 = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UINyforespørselPane.UINyforespørselPane1;
            HtmlHyperlink uIVaktbytteHyperlink = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UISelfServiceListPagePane.UIVaktbytteHyperlink;
            HtmlComboBox uIByttetypeComboBox = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIByttetypeComboBox;
            HtmlEdit uIDagjegønskerfriEdit = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIDagjegønskerfriEdit;
            HtmlComboBox uIA16002300ComboBox = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIA16002300ComboBox;
            HtmlCustom uIUiid56Custom = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIUiid56Custom;
            HtmlSpan uILagrePane = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UILagreButton.UILagrePane;
            #endregion

            try
            {
                // Click 'Ny forespørsel' pane
                Mouse.Click(uINyforespørselPane1, new Point(75, 15));
            }
            catch
            {
                Playback.Wait(5000);
                // Click 'Ny forespørsel' pane
                Mouse.Click(uINyforespørselPane1, new Point(75, 15));
            }
            try
            {
                // Click 'Vaktbytte' link
                Mouse.Click(uIVaktbytteHyperlink, new Point(76, 25));
            }
            catch
            {
                Playback.Wait(5000);
                // Click 'Vaktbytte' link
                Mouse.Click(uIVaktbytteHyperlink, new Point(76, 25));
            }

            Playback.Wait(5000);
            this.UIForespørslerMinGatv2Window.DrawHighlight();
            uIByttetypeComboBox.WaitForControlReady();
            try
            {
                // Select 'Vaktbytte: Gi bort vakt til annen ansatt.' in 'Byttetype' combo box
                uIByttetypeComboBox.SelectedItem = "Vaktbytte: Bytte vakt med annen ansatt.";
            }
            catch
            {
                Playback.Wait(5000);
                // Select 'Vaktbytte: Gi bort vakt til annen ansatt.' in 'Byttetype' combo box
                uIByttetypeComboBox.SelectedItem = "Vaktbytte: Bytte vakt med annen ansatt.";
            }

            try
            {
                // Type '07.10.2020' in 'Dag jeg ønsker fri' text box
                uIDagjegønskerfriEdit.Text = "08.10.2020";
            }
            catch
            {
                Playback.Wait(5000);
                // Type '07.10.2020' in 'Dag jeg ønsker fri' text box
                uIDagjegønskerfriEdit.Text = "08.10.2020";
            }

            try
            {
                // Type '{Tab}' in 'Dag jeg ønsker fri' text box
                Keyboard.SendKeys(uIDagjegønskerfriEdit, "{Tab}");
                Keyboard.SendKeys("{Tab}");
                Keyboard.SendKeys("08.10.2020");
                Keyboard.SendKeys("{Tab 3}{ENTER}");
                Playback.Wait(1000);
                Keyboard.SendKeys("{ENTER}");
            }
            catch
            {
                Playback.Wait(5000);
                // Type '{Tab}' in 'Dag jeg ønsker fri' text box
                Keyboard.SendKeys(uIDagjegønskerfriEdit, "{Tab}");
                Keyboard.SendKeys("{Tab}");
                Playback.Wait(1000);
                Keyboard.SendKeys("08.10.2020");
                Playback.Wait(1000);
                Keyboard.SendKeys("{Tab 3}{ENTER}");
                Playback.Wait(1000);
                Keyboard.SendKeys("{ENTER}");
            }

            try
            {
                // Click 'Lagre' pane
                Mouse.Click(uILagrePane, new Point(56, 10));
            }
            catch
            {
                Playback.Wait(5000);
                // Click 'Lagre' pane
                Mouse.Click(uILagrePane, new Point(56, 10));
            }
        }

        public List<string> Step_5()
        {
            var errorList = new List<string>();

            try
            {
                try
                {
                    CheckRequestCreatedStep5_1();
                }
                catch
                {
                    Playback.Wait(5000);
                    CheckRequestCreatedStep5();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }

            return errorList;
        }
        private void CheckRequestCreatedStep5_1()
        {
            #region Variable Declarations
            HtmlDiv uIOktPane = this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIUiid30Custom.UIOktPane;
            #endregion

            UIForespørslerMinGatv2Window.DrawHighlight();
            uIOktPane.WaitForControlReady();

            CheckRequestCreatedStep5();
        }

        public List<string> Step_6()
        {
            var errorList = new List<string>();

            try
            {
                try
                {
                    ApproveRequestStep6();
                }
                catch
                {
                    Playback.Wait(5000);
                    ApproveRequestStep6();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }

            try
            {
                try
                {
                    CheckRequestMovedStep6();
                }
                catch
                {
                    Playback.Wait(5000);
                    CheckRequestMovedStep6();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.MinGatSelectOpenMenu();
            }
            catch
            {
                Playback.Wait(5000);
                UICommon.UIMapVS2017.MinGatSelectOpenMenu();
            }
            try
            {
                UICommon.UIMapVS2017.MinGatLogOut();
            }
            catch
            {
                Playback.Wait(5000);
                UICommon.UIMapVS2017.MinGatLogOut();
            }
            errorList.AddRange(CloseBrowserWindow());

            return errorList;
        }
        public List<string> Step_7()
        {
            var errorList = new List<string>();

            //Gat
            try
            {
                GoToShiftDateNew(new DateTime(2020, 10, 8));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            try
            {
                CheckRequestInGatStep7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            CloseGat();

            try
            {
                errorList.AddRange(MingatStartAndLogin());
                try
                {
                    UICommon.UIMapVS2017.MinGatSelectRequests();
                }
                catch
                {
                    Playback.Wait(5000);
                    UICommon.UIMapVS2017.MinGatSelectRequests();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            try
            {
                try
                {
                    CheckRequestCreatedStep7();
                }
                catch
                {
                    Playback.Wait(5000);
                    CheckRequestCreatedStep7_1();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.MinGatSelectOpenMenu();
            }
            catch
            {
                Playback.Wait(5000);
                UICommon.UIMapVS2017.MinGatSelectOpenMenu();
            }
            try
            {
                UICommon.UIMapVS2017.MinGatLogOut();
            }
            catch
            {
                Playback.Wait(5000);
                UICommon.UIMapVS2017.MinGatLogOut();
            }
            errorList.AddRange(CloseBrowserWindow());

            return errorList;
        }
        private void CheckRequestCreatedStep7_1()
        {
            this.UIForespørslerMinGatv2Window.DrawHighlight();
            this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.DrawHighlight();
            this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIUiid9Custom.DrawHighlight();
            this.UIForespørslerMinGatv2Window.UIForespørslerMinGatv2Document.UIUiid9Custom.UIOktPane.DrawHighlight(); ;

            CheckRequestCreatedStep7();
        }

        public List<string> Cleanup()
        {
            var errorList = new List<string>();
            try
            {
                //Kill Gat & MinGat
                KillGatProcess();
                KillProcessByName("iexplore");

                //Open IIS
                SupportFunctions.StartProcess(IISManager, true);
                Playback.Wait(2000);

                //WS
                try
                {
                    UICommon.UIMapVS2017.CleanUpMinGatIIS();
                }
                catch (Exception e)
                {
                    errorList.Add("Error removing applications from IIS: " + e.Message);
                }

                try
                {
                    UICommon.UIMapVS2017.RemoveApplicationPoolsMinGat();
                }
                catch (Exception e)
                {
                    errorList.Add("Error removing MinaGat applicationpools from IIS: " + e.Message);
                }

                UICommon.CloseIIS();

                //DeleteWWWRootDirectories
                if (!SupportFunctions.DirectoryDelete(UIWWWRootMinGatDir, TestContext))
                    errorList.Add("Error deleting folder: " + UIWWWRootMinGatDir);

                //Delete extracted MinGat files
                if (!SupportFunctions.DirectoryDelete(UIExtractedMinGatFiles, TestContext))
                    errorList.Add("Error deleting folder: " + UIExtractedMinGatFiles);

                //Delete extracted Gat files
                if (!SupportFunctions.DirectoryDelete(UIExtractedGatFiles, TestContext))
                    errorList.Add("Error deleting folder: " + UIExtractedGatFiles);


                //Delete all sourcefiles
                SupportFunctions.DeleteZipFiles(DestinationAddressZipFiles, TestContext);

            }
            catch (Exception e)
            {
                errorList.Add("Cleanup error: " + e.Message);
            }

            return errorList;
        }
    }
}
