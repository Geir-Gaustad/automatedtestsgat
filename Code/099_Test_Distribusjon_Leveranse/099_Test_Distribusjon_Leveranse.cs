using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.Diagnostics;
using Microsoft.Win32;
using System.Globalization;
using System.Threading;

namespace _099_Test_Distribusjon_Leveranse
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_099_Test_Distribusjon_Leveranse
    {

        //[TestMethod, Timeout(TestTimeout.Infinite)]
        //public void SystemTest_099_Test()
        //{
        //    var errorList = new List<string>();
        //    //////HACK rebooter PC!
        //    //////////Process.Start("shutdown.exe", "-r -t 0");
        //    //////////InstallUtil.exe /u "c:\myservice.exe";

        //    Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
        //    Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

        //    UIMap.TestLogin();

        //    //    //try
        //    //    //{
        //    //    //    Microsoft.Web.Administration.ServerManager server = new Microsoft.Web.Administration.ServerManager();
        //    //    //    Microsoft.Web.Administration.SiteCollection sites = server.Sites;
        //    //    //}
        //    //    //catch (Exception e)
        //    //    //{
        //    //    //    Debug.WriteLine(e.Message);
        //    //    //}   

        //}

        [TestMethod, Timeout(6000000)]
        public void SystemTest_099_A_ExtractAllFiles()
        {
            var errorList = new List<string>();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            try
            {
                UIMap.RestoreDatabase();
                UIMap.GetZipFilesFromServer();

                //step 1 - 2 //Gat NOR X.X.X.zip
                UIMap.ExtractGatFiles();
                try
                {
                    UIMap.ConfigureMinGatForIIS();
                }
                catch (Exception e)
                {
                    errorList.Add("Error configuring MinGat: " + e.Message);
                }
                try
                {
                    UIMap.ConfigureWS1ForIIS();
                }
                catch (Exception e)
                {
                    errorList.Add("Error configuring WS1: " + e.Message);
                }
                try
                {
                    UIMap.ConfigureWS2ForIIS();
                }
                catch (Exception e)
                {
                    errorList.Add("Error configuring WS2: " + e.Message);
                }
                try
                {
                    UIMap.ConfigureGatFlexTimeClock();
                }
                catch (Exception e)
                {
                    errorList.Add("Error configuring Flextime clock: " + e.Message);
                }

                if (!UIMap.ConfigureGatTaskScheduler())
                { errorList.Add("Error installing GatTaskScheduler"); }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Extract files: " + e.Message);
            }

            //try
            //{
            //    var fileName = "language.csv";
            //    if (!UIMap.CheckFileExists(fileName, UIMap.CommonPathsAndParams.UIExtractedGatFiles_SE))
            //        errorList.Add("Missing file: " + UIMap.CommonPathsAndParams.UIExtractedGatFiles_SE + "\\" + fileName);
            //    if (!UIMap.CheckFileExists(fileName, UIMap.CommonPathsAndParams.UIExtractedMinGatFiles_SE + "\\App_Data"))
            //        errorList.Add("Missing file: " + UIMap.CommonPathsAndParams.UIExtractedMinGatFiles_SE + "\\App_Data\\" + fileName);
            //    if (!UIMap.CheckFileExists(fileName, UIMap.CommonPathsAndParams.UIExtractedGatFlexTimeClockFiles_SE))
            //        errorList.Add("Missing file: " + UIMap.CommonPathsAndParams.UIExtractedGatFlexTimeClockFiles_SE + "\\" + fileName);
            //}
            //catch (Exception e)
            //{
            //    errorList.Add("Error in checking extracted files exists: " + e.Message);
            //}

            if (errorList.Count > 0)
                UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter A finished OK");
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_099_B_ConfigureIIS()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            try
            {
                UIMap.StartProcess(UIMap.CommonPathsAndParams.IISManager, true);

                //MinGat
                Playback.Wait(2000);
                this.UIMap.AddApplicationPoolIIS(UIMap.AddApplicationPoolMinGatParams.UI_applicationPoolNameEditText);
                Playback.Wait(2000);
                //WS1
                this.UIMap.AddApplicationPoolIIS("GatWs1");
                Playback.Wait(2000);
                //WS2
                this.UIMap.AddApplicationPoolIIS("GatWs2");

                Playback.Wait(2000);
                this.UIMap.ConvertToAppInIIS(UIMap.AddMInGatParams.UIApplicationpoolComboBoxSelectedItem);
                Playback.Wait(2000);
                ////SE

                //WS1
                Playback.Wait(2000);
                //this.UIMap.AddGatWs1();
                this.UIMap.ConvertToAppInIIS("GatWs1");
                Playback.Wait(2000);

                //WS2
                Playback.Wait(2000);
                //this.UIMap.AddGatWs2();
                this.UIMap.ConvertToAppInIIS("GatWs2");
                Playback.Wait(2000);
                this.UIMap.CloseIIS();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Configure IIS: " + e.Message);
            }
            
            //Kill IIS
            UIMap.KillProcessByName("InetMgr");

            if (errorList.Count > 0)
            UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter B finished OK");
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_099_C_InstallGat()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            try
            {
                //Start Gat from extracted catalog
                UIMap.StartGatFromExtractedDir(UIMap.DepFleksavdeling2, true);

                //step 3
                this.UIMap.GoToShiftDate(new DateTime(2016, 5, 9));
                try
                {
                    this.UIMap.CheckShiftbookViewTypes();
                    TestContext.WriteLine("CheckShiftbookViewTypes(chapter_C_step_3): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckShiftbookViewTypes: " + e.Message);
                }

                UIMap.SelectWeekView();

                //step 4 - 5
                UIMap.GenerateExtraSonnySat14052016();

                try
                {
                    this.UIMap.CheckLineWeekViewMon09052016("1");
                    TestContext.WriteLine("CheckLineWeekViewMon09052016(chapter_C_step_4_5): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckLineWeekViewMon09052016: " + e.Message);
                }

                this.UIMap.GenerateAbsenceJohnson09052016();

                try
                {
                    UIMap.CheckJohnsonTuesday();
                    TestContext.WriteLine("CheckJohnsonTuesday(chapter_C_step_4_5): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckJohnsonTuesday: " + e.Message);
                }

                this.UIMap.ExchangeJohnsonNew();
                this.UIMap.ExchangeJohnsonDetails();

                try
                {
                    UIMap.CheckThomasWednesday();
                    TestContext.WriteLine("CheckThomasWednesday(chapter_C_step_4_5): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckThomasWednesday: " + e.Message);
                }

                this.UIMap.DepExchangeThomas();

                try
                {
                    UIMap.CheckJohnsonFriday();
                    TestContext.WriteLine("CheckJohnsonFriday(chapter_C_step_4_5): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckJohnsonFriday: " + e.Message);
                }

                this.UIMap.VaktutlånJohnsenFriday();

                //step 7
                errorList.AddRange(this.UIMap.IterateMainTabs());

                //step 6
                try
                {
                    this.UIMap.SelectDocMalFromAdministration();
                    this.UIMap.ClickShiftbookTab();
                    TestContext.WriteLine("SelectDocMalFromAdministration(chapter_C_step_6): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in SelectDocMalFromAdministration: " + e.Message);
                }

                try
                {
                    UIMap.CheckTubbsthursday();
                    TestContext.WriteLine("CheckTubbsthursday(chapter_C_step_6): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckTubbsthursday: " + e.Message);
                }

                UIMap.GenerateAbsenceWithDocTubbs12052016();

                try
                {
                    this.UIMap.EditAbsenceDoc("{Enter}Direkte etter reg. av fravær", true);
                    UIMap.ClickEmpTab();

                    //Norsk
                    UIMap.SelectDocTabEmployee();

                    this.UIMap.OpenAbsenceDocFromEmpTab();
                    UIMap.EditAbsenceDoc("{Enter}Åpne fra Anstatt | Dok tab", false);

                    this.UIMap.EditAbsenceDocFromEmpTabNew();
                    UIMap.EditAbsenceDoc("{Enter}Endre fra Anstatt | Dok tab", false);

                    this.UIMap.OpenAbsenceDocFromEmpTab();
                    UIMap.Clear_Clipboard();
                    this.UIMap.CopyAbsenceDocText();
                    UIMap.CloseAbsenceDoc();
                    TestContext.WriteLine("Abcencedoc handling(chapter_C_step_6): OK");
                }
                catch (Exception)
                {
                    errorList.Add("Error handling absencedocument(NO)");
                }

                errorList.AddRange(UIMap.Check_AbsenceDocEditText());

                //Step 8            
                this.UIMap.ClickShiftbookTab();
                Playback.Wait(1500);
                UIMap.OpenGatHelpfile();

                errorList.AddRange(UIMap.CheckGatHelpfileVersion());
                this.UIMap.CloseHelpFile();

                try
                {
                    Playback.Wait(1500);
                    UIMap.CloseGat();
                    TestContext.WriteLine("CloseGat(chapter_C_step_8): OK");
                }
                catch
                {
                    errorList.Add("Error closing Gat(NO)");
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in InstallGat: " + e.Message);
            }

            if (errorList.Count > 0)
                UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter C finished OK");
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_099_E_InstallMinGat()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;
            try
            {
                var url = UIMap.GetMinGatPath();
                var wsBrowser = BrowserWindow.Launch(url);
                //UIMap.StartProcess(url, true, "iexplore");

                this.UIMap.LoginMinGat();
                Playback.Wait(2000);
                errorList.AddRange(UIMap.NavigateAndCheckMinGat());                            
                errorList.AddRange(UIMap.CheckMinGatHelpFileVersion());
                try
                {
                    this.UIMap.CheckMinGatHelpfileMainMenuText();
                    TestContext.WriteLine("CheckMinGatHelpfileMainMenuText(chapter_E_step_1): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckMinGatHelpfileMainMenuText: " + e.Message);
                }

                try
                {
                    Playback.Wait(1000);
                    wsBrowser.Close();
                }
                catch (Exception)
                {
                    this.UIMap.CloseIE();
                }
                 UIMap.KillProcessByName("iexplore");
            }
            catch (Exception e)
            {
                errorList.Add("Error in InstallMinGat: " + e.Message);
            }
                     
            if (errorList.Count > 0)
            UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter E finished OK");
        }
        
        [TestMethod, Timeout(6000000)]
        public void SystemTest_099_G_InstallGatWebservices()
        {
            var errorList = new List<string>();

            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            try
            {
                errorList.AddRange(UIMap.CheckGatWebServices());
            }
            catch (Exception e)
            {
                errorList.Add("Error in GatWebservices: " + e.Message);
            }

            UIMap.KillProcessByName("iexplore");

            if (errorList.Count > 0)
            UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter G finished OK");
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_099_I_InstallGatFlexClock()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            try
            {
                //Start Gat from extracted catalog
                UIMap.StartGatFromExtractedDir( "", false);
                this.UIMap.AddEmployeesToFlex();
                this.UIMap.CloseGat();
                Playback.Wait(2000);
                this.UIMap.StartFlexTimeClock();
                Playback.Wait(10000);

                UIMap.UIMapVS2017.StempleInnOgUtJernhammer();
                Playback.Wait(2000);
                UIMap.UIMapVS2015.SeachHansen();
                try
                {
                    UIMap.UIMapVS2015.CheckHeHa();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckHeHa: " + e.Message);
                }

                Playback.Wait(1000);
                this.UIMap.CloseFlexClock();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Install Gatflexclock: " + e.Message);
            }

            UIMap.KillProcessByName("GatFlexTimeClock");

            if (errorList.Count > 0)
                UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter I finished OK");
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_099_K_InstallGatTaskScheduler()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;
            
            try
            {
                //Start Gat from extracted catalog, config Gat for scedulerservice
                UIMap.StartGatFromExtractedDir("", false);

                UIMap.SearchAndSelectFromAdministration("GLOBALT OPPSETT +Generelle", true);
                this.UIMap.AddTaskScedulerPassword();

                UIMap.SearchAndSelectFromAdministration("OPPSETT AV +WEBSERVICE +BASE URL", false);
                this.UIMap.SetupWebServiceBaseTaskScheduler();
                UIMap.SearchAndSelectFromAdministration("OPPSETT AV +SERVICE +JOBBER", false);

                this.UIMap.UIMapVS2017.SetupServiceJobs();
                //UIMap.ClearAdministrationSearchString();

                UIMap.CloseGat();

                errorList.AddRange(UIMap.StartGatTaskScheduler());

                try
                {
                    this.UIMap.CheckGatTaskSchedulerVersion();
                    TestContext.WriteLine("CheckGatTaskSchedulerVersion(chapter_K): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckGatTaskSchedulerVersion: " + e.Message);
                }
                try
                {
                    this.UIMap.CheckServicesRunning();
                    TestContext.WriteLine("CheckServicesRunning(chapter_K): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckServicesRunning: " + e.Message);
                }

                try
                {
                    this.UIMap.CheckServicesRunningStatus();
                    TestContext.WriteLine("CheckServicesRunningStatus(chapter_K): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckServicesRunningStatus: " + e.Message);
                }

                try
                {
                    this.UIMap.CheckServicesRunningError();
                    TestContext.WriteLine("CheckServicesRunningError(chapter_K): OK");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in CheckServicesRunningError: " + e.Message);
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Install GatTaskScheduler: " + e.Message);
            }

            UIMap.KillProcessByName("iexplore");

            if (errorList.Count > 0)
                UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter K finished OK");
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_099_L_Cleanup()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            errorList.AddRange(UIMap.Cleanup());

            if (errorList.Count > 0)
            UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter L(Cleanup) finished OK");
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        private UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap(TestContext);
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
