namespace _020_Test_Arbeidsplan_Beregninger
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
    using System.Threading;
    using System.Globalization;
    using System.IO;
    using System.Diagnostics;
    using System.Windows.Forms;

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        public string ReportFilePath;
        public string ReportFileName = "020_excel";
        public string FileType = ".xls";
        #endregion

        public UIMap(TestContext festContext)
        {
            TestContext = festContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_020_Beregninger\");

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
        public void DeleteReportFiles()
        {
            UICommon.UIMapVS2017.DeleteReportFiles(ReportFilePath);
        }

        public List<String> CompareReportDataFiles_Test020()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext);
            return errorList;
        }

        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepLønnsberegninger, null, "", logGatInfo);
        }

        private void SelectRosterplanTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }
        public void SelectRosterplan(string planName, bool open, bool showAllPlans)
        {
            SelectRosterplanTab();
            UICommon.SelectRosterPlan(planName, open, showAllPlans);
        }
        private void ChangeDepartment(string dep)
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            UICommon.ChangeDepartmentFromRosterplan(dep, null, false, true);
        }
        private void CloseRosterplan()
        {
            UICommon.ClickRosterplanPlanTab();
            UICommon.CloseRosterplanFromPlanTab();
        }

        public void SelectRosterPlanFilterTab()
        {
            UICommon.ClickRosterplanFilterTab();
            //UICommon.ClickRosterplanHomeTab();
            //UICommon.ClickRosterplanPlanTab(); 
            //UICommon.ClickRosterplanPrintTab();
            //UICommon.ClickRosterplanSupportToolTab();
        }

        public void SelectWeeksumLineAndTotalFilter()
        {
            UICommon.UIMapVS2017.SelectWeeksumLineAndTotalFilter();
        }

        public void SelectShowInformationRosterplanCalk()
        {
            UICommon.UIMapVS2017.SelectViewFilter("Arbeidsplan");
        }

        public void SelectShowWeek0AndN1Filter(bool select)
        {
            if (select)
                UICommon.UIMapVS2017.SelectShowWeek0AndN1Filter();
            else
                UICommon.UIMapVS2017.UnSelectShowWeek0AndN1Filter();
        }
        public List<string> NewStep5And6()
        {
            var errorList = new List<string>();

            UICommon.OpenRosterplanSettings();
            UICommon.UIMapVS2017.UnCheckDoUseWeek0AndNPlus1InPlanSettings();
            UICommon.ClickOkRosterplanSettings();

            try
            {
                CheckWeeksumValuesJalle_Step_5();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Jalle weeksum values: " + e.Message);
            }

            UICommon.OpenRosterplanSettings();
            UICommon.UIMapVS2017.CheckDoUseWeek0AndNPlus1InPlanSettings();
            UICommon.ClickOkRosterplanSettings();

            return errorList;
        }
        public void SelectShowInactiveLinesInFilter()
        {
            UICommon.UIMapVS2017.SelectShowInactiveLinesInFilter();
        }

        public void ExchangeVakantShift()
        {
            OpenExchangeVakantShift();
            SelectRallyRogerInExchangeEmpWindow();
            UICommon.SetExchangeDateForLines(new DateTime(2023, 01, 09));
            UICommon.UIMapVS2017.ClickOkExchangeEmployeeShift();
        }

        public void ExchangeVacantShiftCodes()
        {
            CloseRosterplan();
            SelectRosterplan("BEREGNINGER - KP", true, false);
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();
            ExchangeShiftCodesN2AndAddRogerShifts();
            UICommon.ClickOKEditRosterPlanFromPlantab();
        }
        private void ExchangeShiftCodesN2AndAddRogerShifts()
        {
            #region Variable Declarations
            DXCell uIN2Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient1.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIN2Cell;
            DXCell uIItemCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient1.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIItemCell;
            #endregion

            // Click 'N2' cell
            Mouse.Click(uIN2Cell);

            // Type 'kk4{Tab}' in '[Row]8[Column]RosterCell_3' text box
            Keyboard.SendKeys(uIN2Cell, "kk4{Tab}", ModifierKeys.None);

            // Click cell
            Mouse.Click(uIItemCell);

            // Type 'n{Tab}' in '[Row]9[Column]RosterCell_24' text box
            Keyboard.SendKeys(uIItemCell, "n{Tab}", ModifierKeys.None);

            // Type 'n{Tab}' in '[Row]9[Column]RosterCell_25' text box
            Keyboard.SendKeys("n{Tab}", ModifierKeys.None);

            // Type 'n{Tab}' in '[Row]9[Column]RosterCell_26' text box
            Keyboard.SendKeys("n{Tab}", ModifierKeys.None);
        }

        public void InsertJamShiftCodes()
        {
            UICommon.ClickEditRosterPlanFromPlantab();
            UICommon.ClickRosterplanHomeTab();

            InsertJamDHCodes();
            UICommon.ClickRosterplanPlanTab();
        }
        public void DeleteJamShiftCode()
        {
            DeleteJamDHCode();            
            ClickJamCell();
            UICommon.ClickOKEditRosterPlanFromPlantab();
        }

        private void ClickJamCell()
        {
            #region Variable Declarations
            DXCell uIItemCell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell2;
            #endregion

            Mouse.Click(uIItemCell2);
        }

        public void CreateNewFilterStep12()
        {
            SelectRosterPlanFilterTab();
            UICommon.UIMapVS2017.CreateCustomFilter("Beregninger", new int[] { 1, 6, 7, 8, 41, 42 });
        }

        public void CreateNewFilterStep17()
        {
            CloseRosterplan();
            SelectRosterplan("FAKTOR", true, false);
            SelectRosterPlanFilterTab();
            UICommon.UIMapVS2017.CreateCustomFilter("Faktor", new int[] { 1, 53, 54, 55, 56, 57 });
        }

        public void InsertRogerAbsence()
        {
            UICommon.ClickEditRosterPlanFromPlantab();
            AddAbsence41ToRoger();

            UICommon.SelectAbsenceCode("41");
            UICommon.ClickOKEditRosterPlanFromPlantab();
        }

        public void InsertPluttShiftCodeAndchangeFilterView()
        {
            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();
            InsertPluttA3Code();
            UICommon.ClickOKEditRosterPlanFromPlantab();
            SelectRosterPlanFilterTab();
            UICommon.UIMapVS2017.SelectViewFilter("Vaktfordeling");
        }

        public void Step_14()
        {
            CloseRosterplan();
            SelectRosterplan("BEREGNINGER PÅSKE", true, false);
            UICommon.SelectRosterplanPlanTab();
            UICommon.SelectNewHelpplan();
            UICommon.SetStartDateNewHelpplan(new DateTime(2024, 03, 25));
            SetHelpplanEmployeeCalculationTypes();

            //UICommon.SetHelpPlanWeeks("8");
            Playback.Wait(1000);
            UICommon.ClickOkCreateHelpPlan();
        }

        public void Step_15()
        {
            CloseRosterplan();
            SelectRosterplan("Hjelpeplan for BEREGNINGER PÅSKE.", true, false);
            SelectRosterPlanFilterTab();

            UICommon.UIMapVS2017.SelectViewFilter("Helge");
            UICommon.UIMapVS2017.SelectWeeksumShowTotalOnlyFilter();
        }
        public List<string> CheckValues_Step_15()
        {
            var errorList = new List<string>();

            try
            {
                CheckValuesOrminge_Step_15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Orminge values(Step 15): " + e.Message);
            }
            try
            {
                CheckValuesRoger_Step_15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Roger values(Step 15): " + e.Message);
            }
            try
            {
                CheckValuesMagne_Step_15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Magne values(Step 15): " + e.Message);
            }

            return errorList;

        }

        public void Step_16()
        {
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();

            InsertShiftCodesInHelpplan();
            UICommon.ClickOKEditRosterPlanFromPlantab();
        }

        public List<string> CheckValues_Step_16()
        {
            var errorList = new List<string>();

            try
            {
                CheckValuesOrminge_Step_16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Orminge values(Step 16): " + e.Message);
            }
            try
            {
                CheckValuesRoger_Step_16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Roger values(Step 16): " + e.Message);
            }
            try
            {
                CheckValuesMagne_Step_16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Magne values(Step 16): " + e.Message);
            }

            return errorList;
        }

        public List<string> CheckValues_Step_17()
        {
            var errorList = new List<string>();

            try
            {
                CheckValuesLene_Step_17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Lene values(Step 17): " + e.Message);
            }
            try
            {
                CheckValuesMagne_Step_17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Magne values(Step 17): " + e.Message);
            }

            return errorList;

        }

        public void Step_18()
        {
            CloseRosterplan();
            SelectRosterplan("BEREGNINGER - AP", true, false);

            UICommon.OpenRosterplanSettings();
            SetRosterplanDisplayDateStep18();
            UICommon.ClickOkRosterplanSettings();

            SelectRosterPlanFilterTab();

            UICommon.UIMapVS2017.SelectViewFilter("Arbeid");
            UICommon.UIMapVS2017.SelectWeeksumShowTotalOnlyFilter();
        }

        public List<string> CheckValues_Step_18()
        {
            var errorList = new List<string>();

            try
            {
                CheckValuesAdel_Step_18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Adel values(Step 18): " + e.Message);
            }
            try
            {
                CheckValuesClue_Step_18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Clue values(Step 18): " + e.Message);
            }
            try
            {
                CheckValuesSilke_Step_18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Silke values(Step 18): " + e.Message);
            }

            return errorList;

        }

        public List<string> Step_21()
        {
            var errorList = new List<string>();

            CloseRosterplan();
            ChangeDepartment("5180");
            UICommon.SelectFromAdministration("avdelingsoppsett +systeminnstillinger", true);
            SelectDepartmentRosterplanTab();

            try
            {
                CheckF3AndNormalNotCheckedInSelectDepartmentRosterplanTab_step_21();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 21: " + e.Message);
            }

            CheckF3AndNormalSelectDepartmentRosterplanTab();
            //UICommon.ClearAdministrationSearchString();

            return errorList;
        }
        public List<string> Step_22()
        {
            var errorList = new List<string>();

            SelectRosterplan("KP - Beregn normal arbeidstid", true, false);
            SelectRosterPlanFilterTab();
            UICommon.UIMapVS2017.SelectWeeksumShowTotalOnlyFilter();

            try
            {
                CheckWeekSumStep_22();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 22: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_23()
        {
            var errorList = new List<string>();

            CloseRosterplan();
            UICommon.SelectFromAdministration("avdelingsoppsett +systeminnstillinger", true);
            SelectDepartmentRosterplanTab();
            UnCheckF3AndNormalInSelectDepartmentRosterplanTab();
            //UICommon.ClearAdministrationSearchString();
            SelectRosterplan("KP - Beregn normal arbeidstid", true, false);

            try
            {
                CheckWeeksumValuesStep_23();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 23: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_24()
        {
            var errorList = new List<string>();

            CloseRosterplan();
            SelectRosterplan("AP - Beregn normal arbeidstid", true, false);

            try
            {
                CheckWeeksumValuesStep_24_25();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 24: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_25()
        {
            var errorList = new List<string>();

            CloseRosterplan();
            UICommon.SelectFromAdministration("avdelingsoppsett +systeminnstillinger", true);
            SelectDepartmentRosterplanTab();
            CheckF3AndNormalSelectDepartmentRosterplanTab();
            //UICommon.ClearAdministrationSearchString();
            SelectRosterplan("AP - Beregn normal arbeidstid", true, false);

            try
            {
                CheckWeeksumValuesStep_24_25();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 25: " + e.Message);
            }

            CloseRosterplan();
            return errorList;
        }
        public List<string> SaveDataFromRosterplanCalculations(string stepName)
        {
            var errorList = new List<string>();

            OpenExcelFromRosterplanCalculations();
            errorList.AddRange(SaveAsExcel(stepName));

            return errorList;
        }

        private void OpenExcelFromRosterplanCalculations()
        {
            UICommon.UIMapVS2017.OpenExcelFromRosterplanCalculations();
        }

        private List<string> SaveAsExcel(string stepName)
        {
            var errorList = new List<string>();

            try
            {
                var fileName = ReportFilePath + ReportFileName + stepName;
                UICommon.ExportToExcel(fileName);
            }
            catch (Exception e)
            {
                errorList.Add("Feil ved export til excel(" + stepName + "): " + e.Message);
            }

            return errorList;
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

        /// <summary>
        /// OpenExchangeVakantShift
        /// </summary>
        public void OpenExchangeVakantShift()
        {

            RightClickVakant();

            // Click 'Bytt ansatt/stillingsforhold på alle linjer (VAKANT)' MenuBaseButtonItem
            Keyboard.SendKeys("{Down 3}{Enter}");
        }
    }
}
