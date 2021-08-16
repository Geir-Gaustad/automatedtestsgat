namespace _025_Ytelsestest_Arbeidsplan
{
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
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using System.Threading;
    using System.Globalization;
    using System.IO;

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        private string ConfigFilePath;
        #endregion
        private UIMapVS2017Classes.UIMapVS2017 map2017;
        public UIMapVS2017Classes.UIMapVS2017 UIMapVS2017
        {
            get
            {
                if ((this.map2017 == null))
                {
                    this.map2017 = new UIMapVS2017Classes.UIMapVS2017();
                }

                return this.map2017;
            }
        }
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
        public bool RestoreDatabase(bool isPerformance = true)
        {
            return UICommon.RestoreDatabase(isPerformance);
        }
        public void StartGat(bool depYtelse = true)
        {
            UICommon.LaunchGatturnus(false);

            if(depYtelse)
            UICommon.LoginGatAndSelectDepartment(UICommon.DepYtelse);
            else
            UICommon.LoginGatAndSelectDepartment(UICommon.DepOrtopedisk);
        }
        public bool CheckRosterPlanExists(string planName)
        {
            return UICommon.CheckRosterPlanExists(planName);
        }
        public string ReadPhysicalMemoryUsage(bool value)
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.WorkingSet64, value);
        }
        public string ReadPagedMemorySize64(bool value)
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.PagedMemorySize64, value);
        }
        public List<string> OpenCloseRosterplan(int rotations, int checkInterval)
        {
            var errorList = new List<string>();
          
            int check = 0;
            try
            {
                for (int i = 0; i < rotations; i++)
                {
                    OpenPlan("YTELSE - Arbeidsplan", "MemoryCheck", 0, 0, check == 0);
                    UICommon.ClickRosterplanPlanTab();

                    if (!UICommon.UIMapVS2017.CheckCloseButtonRosterPlanTabExists())
                        throw new Exception("Unable to close rosterplan");

                    CloseRosterplanFromPlanTab();

                    if (i == check)
                    {
                        TestContext.WriteLine("Minnebruk ved " + check + ": " + ReadPhysicalMemoryUsage(false));
                        TestContext.WriteLine("PagedMemorySize " + check + ": " + ReadPagedMemorySize64(false));
                        check = check + checkInterval;
                    }
                }

                TestContext.WriteLine("Minnebruk ved " + rotations + ": " + ReadPhysicalMemoryUsage(false));
                TestContext.WriteLine("PagedMemorySize  " + rotations + ": " + ReadPagedMemorySize64(false));
            }
            catch (Exception e)
            {
                var errorText = UICommon.UIMapVS2017.CheckAndCloseGeneralErrorMessage();
                if (errorText.Count > 0)
                    errorList.Add("General Error Message: " + errorText);
                else
                    errorList.Add("Error open/close plan: " + e.Message);
            }

            return errorList;
        }
        public List<string> OpenCloseCoverExtraShift(int rotations, int checkInterval)
        {
            var errorList = new List<string>();

            int check = 0;
            try
            {
                UICommon.GoToShiftbookdate(new DateTime(2016, 01, 02));
                UIMapVS2017.SelectAndCoverShift();

                for (int i = 0; i < rotations; i++)
                {
                    UIMapVS2017.CoverShiftWithExtra();

                    if (i == check)
                    {
                        TestContext.WriteLine("Minnebruk ved " + check + ": " + ReadPhysicalMemoryUsage(false));
                        TestContext.WriteLine("PagedMemorySize " + check + ": " + ReadPagedMemorySize64(false));
                        check = check + checkInterval;
                    }
                }

                TestContext.WriteLine("Minnebruk ved " + rotations + ": " + ReadPhysicalMemoryUsage(false));
                TestContext.WriteLine("PagedMemorySize  " + rotations + ": " + ReadPagedMemorySize64(false));

                UIMapVS2017.CancelAndClose();
            }
            catch (Exception e)
            {
                var errorText = UICommon.UIMapVS2017.CheckAndCloseGeneralErrorMessage();
                if (errorText.Count > 0)
                    errorList.Add("General Error Message: " + errorText);
                else
                    errorList.Add("Error: " + e.Message);
            }

            return errorList;
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
        private List<string> SelectRosterPlan(string planName, string step, int lower, int upper)
        {
            Playback.Wait(1000);
            var timeBefore = DateTime.Now;
            UICommon.SelectRosterPlan(planName);

            var timeAfter = DateTime.Now;
            return TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved åpning av plan i punkt " + step, lower, upper);
        }
        public List<string> OpenPlan(string planName, string step, int lower, int upper, bool selectPlanTab = true)
        {
            if (selectPlanTab)
            {
                Playback.Wait(1500);
                SelectRosterplanTab();
            }

            Playback.Wait(3000);
            return SelectRosterPlan(planName, step, lower, upper);
        }

        public List<string> EffectuateFullplan(bool selectPlanTab, bool closePlan, string step, int lowOpenEffWindow, int highOpenEffWindow, int lowEffectuation, int highEffectuation, int lowOpenPlan, int highOpenPlan, bool revolving = true, bool helpPlan = false)
        { 
            var errorList = new List<string>();
            UICommon.EffectuateFullRosterplan(selectPlanTab);

            var timeBeforeOpenEffectuationWindow = DateTime.Now;
            if (UICommon.EffectuationWindowExists())
            {
                var timeAfterOpenEffectuationWindow = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeOpenEffectuationWindow, timeAfterOpenEffectuationWindow, "Tidsforbruk ved åpning av iverksettingsvindu i punkt " + step, lowOpenEffWindow, highOpenEffWindow));
            }

            UICommon.EffectuateRosterplanLines(revolving);
            var timeBeforeEffectuation = DateTime.Now;

            try
            {
            if (UICommon.SalaryCalculationsWindowExists())
            {
                var timeAfterEffectuation = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved iverksetting av plan i punkt " + step, lowEffectuation, highEffectuation));
            }

            UICommon.CloseSalaryCalculationsWindow();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            var timeWhenClosingSalaryCalcWindow = DateTime.Now;

            if (helpPlan)
            {
                if (!UICommon.CheckDeleteEffectuationHelpPlanButtonEnabled(true))
                    errorList.Add("error checking DeleteEffectuationButton");
            }
            else
            {
                if (!UICommon.CheckDeleteEffectuationButtonEnabled(true))
                    errorList.Add("error checking DeleteEffectuationButton");
            }

            var timePlanReady = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeWhenClosingSalaryCalcWindow, timePlanReady, "Tidsforbruk ved lasting av plan etter iverksetting i punkt " + step, lowOpenPlan, highOpenPlan));

            if (closePlan)
                XCloseRosterplan();

            return errorList;
        }

        public List<string> DeleteEffectuationFullplan(string step, int lowWindowReady, int highWindowReady, int lowDeleteEff, int highDeleteEff, int lowOpenPlan, int highOpenPlan)
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

            var timeAfterEffectuation = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved sletting av iverksetting(plan) i punkt " + step, lowDeleteEff, highDeleteEff));

            try
            {
                Playback.Wait(3000);
                UICommon.CheckDeleteEffectuationButtonEnabled(false);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            var timePlanReady = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeAfterEffectuation, timePlanReady, "Tidsforbruk ved lasting av plan etter sletting av iverksetting i punkt " + step, lowOpenPlan, highOpenPlan));

            return errorList;
        }

        public List<string> AddAndConfigureEmployeesToPlan(string step)
        {
            var errorList = new List<string>();

            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            UICommon.SelectAllEmployeesAddEmployeesWindow();
            UICommon.ClickOkAddEmployeesWindow();

            Select4EmpsInEmployeeWindow();
            SelectCauseCodeF();
            CheckCauseCodeF();
            Select4RemainingEmpsInEmployeeWindow();

            UICommon.SetRevolvingFromDate();
            UICommon.SetRevolvingToDate();

            CheckRevolvingPeriod();

            var timeBeforeAddingEmployees = DateTime.Now;
            UICommon.ClickOkEmployeesWindow();
            try
            {
                CheckEmployeesAddedToPlan();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }
            var timeAfterAddingEmployees = DateTime.Now;

            errorList.AddRange(TimeLapseInSeconds(timeBeforeAddingEmployees, timeAfterAddingEmployees, "Tidsforbruk ved å legge ansatte til i planen i punkt " + step, 10, 20));

            return errorList;
        }

        public void EditViewDateForPlan()
        {
            UICommon.OpenRosterplanSettings();
            ChangeViewdate();
        }

        public void EditValidToDateForPlan()
        {
            #region Variable Declarations
            DXButton uIGSSimpleButtonButton = this.UIItemWindow.UIPaBottomClient.UIGSSimpleButtonButton;
            #endregion

            UICommon.OpenRosterplanSettings();
            UICommon.SetValidToDateRosterplan(new DateTime(2023, 01, 01));

            Mouse.Click(uIGSSimpleButtonButton);
            Playback.Wait(5000);
        }

        public List<string> EditRosterplanShifts(string step)
        {
            var errorList = new List<string>();

            Playback.Wait(3000);
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();

            SelectAndCopyShifts();
            var timeBeforeInsertingShifts = DateTime.Now;
            InsertCopiedShifts();
            UICommon.ClickOKEditRosterPlanFromPlantab();
            var timeAfterInsertingShifts = DateTime.Now;

            errorList.AddRange(TimeLapseInSeconds(timeBeforeInsertingShifts, timeAfterInsertingShifts, "Tidsforbruk ved å lime inn kopierte vakter i planen i punkt " + step, 5, 12));

            return errorList;
        }

        public List<string> CreateHelpPlan(string step)
        {
            var errorList = new List<string>();
            //Hjelpeplan for YTELSE - Arbeidsplan.
            UICommon.SelectNewHelpplan();

            UICommon.SetStartDateNewHelpplan(new DateTime(2022, 06, 27));

            Playback.Wait(1000);

            UICommon.SetHelpPlanWeeks("8");
            var timeBeforeOk = DateTime.Now;
            UICommon.ClickOkCreateHelpPlan();
            var timeAfterOk = DateTime.Now;

            errorList.AddRange(TimeLapseInSeconds(timeBeforeOk, timeAfterOk, "Tidsforbruk før bekreftelse på oppretting av hjelpeplan vises i punkt " + step, 8, 15));

            return errorList;
        }

        public void EditHelpPlanShifts()
        {
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();

            MarkThreeWeeksAndRightClick();

            UICommon.ClickOKEditRosterPlanFromPlantab();
            XCloseRosterplan();
        }


        /// <summary>
        /// MarkThreeWeeksAndRightClick
        /// </summary>
        public void MarkThreeWeeksAndRightClick()
        {
            #region Variable Declarations
            var rowList = new List<string> { "47", "45", "43", "41", "39", "37", "35", "33", "31", "29", "27", "25", "23", "21", "15", "13", "11", "9", "7", "3", "1" };//{"1", "3", "7", "9", "11", "13", "15", "21", "23", "25", "27", "29", "31", "33" };
            var startCell = this.UIArbeidsplanHjelpeplaWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            var stopCell = this.UIArbeidsplanHjelpeplaWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell1;
            #endregion

            foreach (var row in rowList)
            {
                startCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanGridControlCell[View]gvRosterPlan[Row]" + row + "[Column]RosterCell_7";
                stopCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanGridControlCell[View]gvRosterPlan[Row]" + row + "[Column]RosterCell_27";
                // Move cell to cell

                startCell.EnsureClickable(new Point(15, 10));
                Mouse.StartDragging(startCell, new Point(15, 10));
                Mouse.StopDragging(stopCell, new Point(15, 10));

                // Right-Click cell
                Mouse.Click(stopCell, MouseButtons.Right);
                UICommon.SelectAbsenceCode("45B", "7");
            }
        }

        /// <summary>
        /// SelectAndCopyShifts
        /// </summary>
        public void SelectAndCopyShifts()
        {
            #region Variable Declarations
            DXCell uIDCell = this.UIArbeidsplanYTELSEArbWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIDCell;
            DXCell uIF1Cell = this.UIArbeidsplanYTELSEArbWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF1Cell;
            DXMenuBaseButtonItem copyNotInEditMode = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink10MenuBaseButtonItem;
            DXMenuBaseButtonItem copyInEditMode = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIKopierMenuBaseButtonItem;
            #endregion

            uIDCell.EnsureClickable();
            Mouse.StartDragging(uIDCell);
            Mouse.StopDragging(uIF1Cell);

            // Right-Click 'F1' cell
            Mouse.Click(uIF1Cell, MouseButtons.Right);
            Playback.Wait(500);

            // Click 'BarButtonItemLink[10]' MenuBaseButtonItem
            Mouse.Click(copyInEditMode);
        }
        
        /// <summary>
        /// CheckEmployeesAddedToPlan - Use 'CheckEmployeesAddedToPlanExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckEmployeesAddedToPlan()
        {
            UIArbeidsplanWindow.WaitForControlExist(600000);

            var tbl = UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            var tblRows = tbl.Views[0].RowCount;
            Assert.AreEqual(106, tblRows, "Feil antall ansatte i plan(step 17)");
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

        public void XCloseRosterplan()
        {
            try
            {
                Playback.Wait(1000);
                UICommon.XcloseRosterplan();
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e.Message);
            }

            Playback.Wait(1000);
        }
        public void CloseRosterplanFromPlanTab()
        {
            try
            {
                UICommon.CloseRosterplanFromPlanTab();
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e.Message);
                Playback.Wait(1000);
                UICommon.XcloseRosterplan();
            }

            Playback.Wait(1000);
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
        
        public virtual CheckEmployeesAddedToPlanExpectedValues CheckEmployeesAddedToPlanExpectedValues
        {
            get
            {
                if ((this.mCheckEmployeesAddedToPlanExpectedValues == null))
                {
                    this.mCheckEmployeesAddedToPlanExpectedValues = new CheckEmployeesAddedToPlanExpectedValues();
                }
                return this.mCheckEmployeesAddedToPlanExpectedValues;
            }
        }

        private CheckEmployeesAddedToPlanExpectedValues mCheckEmployeesAddedToPlanExpectedValues;
    }
    /// <summary>
    /// Parameters to be passed into 'CheckEmployeesAddedToPlan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class CheckEmployeesAddedToPlanExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Riseth, Vidar' cell equals 'Riseth, Vidar'
        /// </summary>
        public string UIRisethVidarCellValueAsString = "Riseth, Vidar";
        #endregion
    }
}
