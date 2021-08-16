namespace _022_Ytelsestest_Kalenderplan
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
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
    using System.Diagnostics;
    using System.Threading;
    using System.Globalization;
    using System.IO;

    public partial class UIMap
    {

        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        private string ConfigFilePath;

        public UIMapVS2017Classes.UIMapVS2017 UIMapVS2017
        {
            get
            {
                if ((this.map1 == null))
                {
                    this.map1 = new UIMapVS2017Classes.UIMapVS2017();
                }

                return this.map1;
            }
        }

        private UIMapVS2017Classes.UIMapVS2017 map1;

        #endregion
        
        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            ConfigFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Config");

            UICommon = new CommonUIFunctions.UIMap(TestContext, true);
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
        public bool RestoreDatabase(bool isPerformance)
        {
            return UICommon.RestoreDatabase(isPerformance);
        }
        public void StartGat(bool logGatInfo, bool chap_1 = true)
        {
            UICommon.LaunchGatturnus(false);

            if (chap_1)
                UICommon.LoginGatAndSelectDepartment(UICommon.DepYtelse, null, "", logGatInfo);
            else
                UICommon.LoginGatAndSelectDepartment(UICommon.DepYtelse_2, null, "", logGatInfo);
        }
        
        public string ReadPhysicalMemoryUsage()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.WorkingSet64);
        }
        public string ReadPagedMemorySize64()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.PagedMemorySize64);
        }
        public List<string> TimeLapseInSeconds(DateTime timeBefore, DateTime timeAfter, string text, int bottonLimit, int upperLimit)
        {
            List<string> errorList = new List<string>();
            string elapsedTimeOutput = "";

            errorList.AddRange(LoadBalanceTesting.TimeLapseInSeconds(timeBefore, timeAfter, text, out elapsedTimeOutput, bottonLimit, upperLimit));
            TestContext.WriteLine(elapsedTimeOutput);

            return errorList;
        }

        private void SelectRosterplanTab(int delay = 0)
        {
            Playback.Wait(delay * 1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }
        private List<string> SelectRosterPlan(string planName, string step, int low, int high)
        {
            Playback.Wait(1000);
            var timeBefore = DateTime.Now;
            UICommon.SelectRosterPlan(planName);

            var timeAfter = DateTime.Now;
            return TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved åpning av kalenderplan i punkt " + step, low, high);
        }
        public List<string> OpenPlan(string planName, string step, int low, int high)
        {
            Playback.Wait(1500);
            SelectRosterplanTab();
            return SelectRosterPlan(planName, step, low, high);
        }

        public void ShowAllPlans()
        {
            UICommon.ShowAllPlans();
        }

        public List<string> EffectuateFullplan(string step, int lowOpenEffWindow, int highOpenEffWindow, int lowEffectuation, int highEffectuation, int lowOpenPlan, int highOpenPlan, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var errorList = new List<string>();

            UICommon.EffectuateFullRosterplan(true);

            var timeBeforeOpenEffectuationWindow = DateTime.Now;
            if (UICommon.EffectuationWindowExists())
            {
                var timeAfterOpenEffectuationWindow = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeOpenEffectuationWindow, timeAfterOpenEffectuationWindow, "Tidsforbruk ved åpning av iverksettingsvindu i punkt " + step, lowOpenEffWindow, highOpenEffWindow));
            }

            if (fromDate != null && toDate != null)
            {
                ChangePeriodForActualLines(fromDate.Value, toDate);
                UICommon.EffectuateRosterplanLines(false);
            }
            else
                UICommon.EffectuateRosterplanLines(true);

            var timeBeforeEffectuation = DateTime.Now;

            if (UICommon.SalaryCalculationsWindowExists())
            {
                var timeAfterEffectuation = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved iverksetting av kalenderplan i punkt " + step, lowEffectuation, highEffectuation));
            }

            UICommon.CloseSalaryCalculationsWindow();
            var timeWhenClosingSalaryCalcWindow = DateTime.Now;
            try
            {
                UICommon.CheckDeleteEffectuationButtonEnabled(true);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            var timePlanReady = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeWhenClosingSalaryCalcWindow, timePlanReady, "Tidsforbruk ved lasting av kalenderplan etter iverksetting i punkt " + step, lowOpenPlan, highOpenPlan));

            return errorList;
        }

        public void ChangePeriodForActualLines(DateTime fromDate, DateTime? toDate)
        {
            UICommon.ChangeEffectuationPeriodForActualLines(fromDate, toDate);
        }

        public List<string> DeleteEffectuationFullplan(string step, int lowWindowReady, int highWindowReady, int lowDeleteEff, int highDeleteEff, bool chap_1)
        {
            var errorList = new List<string>();

            UICommon.DeleteEffectuationRosterplan();

            var timeBeforeSelectAll = DateTime.Now;
            UICommon.SelectAllAndWaitForDeleteEffectuationWindowReady();

            var timeAfterSelectAll = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeSelectAll, timeAfterSelectAll, "Tidsforbruk før Slette iverksetting vindu er klart i punkt " + step, lowWindowReady, highWindowReady));

            var timeBeforeEffectuation = DateTime.Now;
            UICommon.DeleteEffectuatedLines();
            UICommon.CloseDeleteEffectuationOkWindow();

            try
            {
                Playback.Wait(3000);
                if (chap_1)
                    CheckShiftExist();
                else
                    UICommon.CheckDeleteEffectuationButtonEnabled(false);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            var timeAfterEffectuation = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved sletting av iverksetting(kalenderplan) i punkt " + step, lowDeleteEff, highDeleteEff));
            
            return errorList;
        }
        
        public void CheckShiftExist()
        {
            #region Variable Declarations
            DXCell uIACell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIACell;            
            #endregion

            uIACell.WaitForControlExist(300000);
            Assert.AreEqual("PlanShift (A)", uIACell.ValueAsString, "Error finding shiftcode");
        }

        public List<string> AddEmployeesToPlan(string step)
        {
            var errorList = new List<string>();

            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            UICommon.SelectAllEmployeesAddEmployeesWindow();
            UICommon.ClickOkAddEmployeesWindow();

            var timeBeforeAddingEmployees = DateTime.Now;
            UICommon.ClickOkEmployeesWindow();

            try
            {
                EditRosterplan(false);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            var timeAfterAddingEmployees = DateTime.Now;

            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingEmployees, timeAfterAddingEmployees, "Tidsforbruk ved å legge ansatte til i planen i punkt " + step, 65, 85));

            return errorList;
        }

        public List<string> AddEmployeesToPlan2(string step)
        {
            var errorList = new List<string>();

            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            UICommon.SelectAllEmployeesAddEmployeesWindow();
            UICommon.ClickOkAddEmployeesWindow();

            try
            {
                UIMapVS2017.CheckEmpAtBottomOfListStep_3();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            UIMapVS2017.MoveChesterToTopOfListStep_3();

            try
            {
                UIMapVS2017.CheckEmpAtTopOfListStep_3();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            var timeBeforeAddingEmployees = DateTime.Now;

            UICommon.ClickOkEmployeesWindow();
            try
            {
                EditRosterplan(false);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            var timeAfterAddingEmployees = DateTime.Now;

            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingEmployees, timeAfterAddingEmployees, "Tidsforbruk ved å legge ansatte til i planen i punkt " + step, 80, 100));

            return errorList;
        }

        public List<string> AddShiftsToEmployeesInPlan(string step)
        {
            var errorList = new List<string>();

            var timeBeforeAddingShifts = DateTime.Now;
            AddDshiftsToLundWeek1FromRightClickMenu();
            var timeAfterAddingShifts = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingShifts, timeAfterAddingShifts, "Tidsforbruk ved å legge til D-vakter(Høyreklikkmeny) i punkt " + step, 20, 30));

            timeBeforeAddingShifts = DateTime.Now;
            AddDshiftsToLundWeek2();
            timeAfterAddingShifts = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingShifts, timeAfterAddingShifts, "Tidsforbruk ved å legge til D-vakter(Skrive rett i cellen) i punkt " + step, 20, 30));

            timeBeforeAddingShifts = DateTime.Now;
            AddDshiftsToLundWeek3();
            timeAfterAddingShifts = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingShifts, timeAfterAddingShifts, "Tidsforbruk ved å legge til D-vakter(Markere man-fre) i punkt " + step, 8, 14));

            timeBeforeAddingShifts = DateTime.Now;
            AddDshiftsToLundWeek4();
            timeAfterAddingShifts = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingShifts, timeAfterAddingShifts, "Tidsforbruk ved å legge til D-vakter(Kopiere vakter fra uke3) i punkt " + step, 5, 12));

            timeBeforeAddingShifts = DateTime.Now;
            AddAshiftsToLundekvamWholePeriod();
            try
            {
                CheckAshiftExist();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }
            timeAfterAddingShifts = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingShifts, timeAfterAddingShifts, "Tidsforbruk ved å legge til A-vakter(hele perioden) i punkt " + step, 15, 28));

            EditRosterplan(true);
            Playback.Wait(2000);
            EditRosterplan(false);

            timeBeforeAddingShifts = DateTime.Now;
            DeleteLundekvamWholePeriod();
            try
            {
                CheckAshiftHasBeenRemoved();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }
            timeAfterAddingShifts = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingShifts, timeAfterAddingShifts, "Tidsforbruk ved å slette A -vakter(hele perioden) i punkt " + step, 10, 20));

            timeBeforeAddingShifts = DateTime.Now;
            CancelEditRosterplan();
            try
            {
                CheckAshiftExist(true);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }
            timeAfterAddingShifts = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingShifts, timeAfterAddingShifts, "Tidsforbruk for å tilbakestille planen etter å ha trykket Avbryt i punkt " + step, 10, 20));

            return errorList;
        }
        
        public List<string> AddShiftsToEmployeesInPlan2(string step)
        {
            var errorList = new List<string>();

            var timeBeforeAddingShifts = DateTime.Now;
            UIMapVS2017.AddChesterShifts();
            var timeAfterAddingShifts = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingShifts, timeAfterAddingShifts, "Tidsforbruk ved å legge til M1-vakter i punkt " + step, 12, 20));

            EditRosterplan(true);

            return errorList;
        }

        public List<string> ApproveChester(string step)
        {
            var errorList = new List<string>();

            var timeBeforeAddingShifts = DateTime.Now;

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
            UIMapVS2017.ApproveChester();
            UIMapVS2017.CheckChesterApproved();
            var approved = UIMapVS2017.ChesterApproved();

            if (approved)
            {
                var timeAfterAddingShifts = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingShifts, timeAfterAddingShifts, "Tidsforbruk ved godkjenning i punkt " + step, 35, 45));
            }
           
            return errorList;
        }

        public void CheckAshiftHasBeenRemoved()
        {
            #region Variable Declarations
            var uIItemCell8 = this.UIArbeidsplanYTELSEKalWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell8;
            #endregion

            // Verify that the 'ValueAsString' property of cell equals ''
            Assert.AreEqual("PlanShift (Empty)", uIItemCell8.ValueAsString, "A vakt er ikke fjernet");
        }

        public void EditRosterplan(bool okEdit)
        {
            Playback.Wait(1000);

            if (okEdit)
                UICommon.ClickOKEditRosterPlanFromPlantab();
            else
                UICommon.ClickEditRosterPlanFromPlantab();
        }

        public void CancelEditRosterplan()
        {
            Playback.Wait(1000);
            UICommon.ClickCancelEditRosterPlanFromPlantab();

            if (UICommon.CheckRosterPlanSaveChangesWindowExists())
                UICommon.ClickDiscardChangesToRosterplan();
        }
        
        public void ReplaceInifile(bool effectuated, bool insertOrgIniFile = false)
        {
            if (insertOrgIniFile)
            {
                SupportFunctions.FileCopy("GATTURNUS.ini", ConfigFilePath + @"\no", ConfigFilePath + @"\CurrentIni", TestContext);
                return;
            }

            if (effectuated)
                SupportFunctions.FileCopy("GATTURNUS.ini", ConfigFilePath + @"\Test_022\2", ConfigFilePath + @"\CurrentIni", TestContext);
            else
                SupportFunctions.FileCopy("GATTURNUS.ini", ConfigFilePath + @"\Test_022\1", ConfigFilePath + @"\CurrentIni", TestContext);
        }

        public void CloseRosterplan()
        {
            try
            {
                UICommon.XcloseRosterplan();
            }
            catch
            {
                try
                {
                    UICommon.CloseRosterplanFromPlanTab();
                }
                catch (Exception e)
                {
                    TestContext.WriteLine(e.Message);
                }
            }
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

        public void AssertResults(List<string> errorList)
        {
            UICommon.AssertResults(errorList);
        }
        
    }
}
