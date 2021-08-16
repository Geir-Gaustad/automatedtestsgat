namespace _060_Test_Security_GatUser_Log_GATP_2465
{
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
        public string ReportFilePath;
        public string ReportFileName = "060_excel";
        public string FileType = ".xls";
        #endregion

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_60_GatUserLog\");
            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }

        #region Default Functions
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
        public void DeleteReportFiles()
        {
            UICommon.UIMapVS2017.DeleteReportFiles(ReportFilePath);
        }
        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepArbeidsplanklinikken, null, "", logGatInfo);
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
        public void ExportToExcell(string postfix)
        {
            try
            {
                var fileName = ReportFilePath + ReportFileName + postfix; // + SupportFunctions.HeaderType._Common.ToString();
                UICommon.UIMapVS2017.SaveFromGatExcelDialog(fileName);
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Feil ved export til excel(" + postfix + "): " + e.Message);
            }
        }
        public List<String> CompareReportDataFiles_Test060()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext, 0, false, true);
            return errorList;
        }
        public void AssertResults(List<string> errorList)
        {
            UICommon.AssertResults(errorList);
        }

        public void OpenPlan(string planName)
        {
            Playback.Wait(1500);
            Click_RosterplanTab();
            UICommon.SelectRosterPlan(planName);
        }
        public void Click_EditRosterPlan()
        {
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();
        }

        public void Click_RosterplanTab()
        {
            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Rosterplan);
        }

        public void ClickRosterplanPlanTab()
        {
            UICommon.ClickRosterplanPlanTab();
        }

        public void ClickNewRosterPlanCopy()
        {
            UICommon.ClickNewRosterPlanCopy();
        }
        private void CloseRosterPlan()
        {
            try
            {
                UICommon.CloseRosterplanFromPlanTab();
            }
            catch
            {
                try
                {
                    UICommon.XcloseRosterplan();
                }
                catch (Exception e)
                {
                    TestContext.WriteLine(e.Message);
                }
            }

            Playback.Wait(1000);
        }

        private void GoToShiftDateNew(DateTime date)
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
            UICommon.GoToShiftbookdate(date);
        }

        private void EffectuateDate(DateTime effDate)
        {
            UICommon.ChangeEffectuationPeriodForActualLines(effDate, effDate);
            UICommon.EffectuateRosterplanLines(false);
        }
        private void EffectuatePeriod(DateTime effDate, DateTime toDate)
        {
            UICommon.ChangeEffectuationPeriodForActualLines(effDate, toDate);
            UICommon.EffectuateRosterplanLines(false);
        }

        #endregion

        public List<string> Step_1()
        {
            var errorList = new List<string>();

            try
            {
                StartGat(true);
                UICommon.SelectFromAdministration("Brukeradministrasjon + Definisjon", true);
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

            try
            {
                ChangeUserCredentials();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2_1: " + e.Message);
            }

            UICommon.UIMapVS2017.OpenLogForUser();

            try
            {
                //SortTable1();
                OpenTable1();
                ExportToExcell("_Step2_1");
                SortTable2();
                OpenTable2();
                ExportToExcell("_Step2_2");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2_2: " + e.Message);
            }

            UICommon.UIMapVS2017.CloseUserLog();

            return errorList;
        }
    }
}
