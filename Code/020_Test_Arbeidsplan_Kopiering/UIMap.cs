using System.CodeDom.Compiler;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Input;
using DevExpress.CodedUIExtension.DXTestControls.v19_2;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
using MouseButtons = System.Windows.Forms.MouseButtons;
using CommonTestData;
using Microsoft.VisualStudio.TestTools.UITesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

namespace _020_Test_Arbeidsplan_Kopiering
{

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;

        public string ReportFilePath;
        public string ReportFileName = "020_excel";
        public string FileType = ".xls";
        #endregion

        public UIMap(TestContext testContext)
        {
            TestContext  =testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_020_Kopiering\");

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

        public List<String> CompareReportDataFiles_Test020()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext, 5);
            return errorList;
        }

        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepKopiering, null, "", logGatInfo);
        }
        private void ChangeDepartment(string dep, List<string> other = null)
        {
            UICommon.ChangeDepartmentFromRosterplan(dep, other);
        }
        private int SelectRosterPlan(string plan, bool openPlan = true)
        {
            var errorList = new List<string>();

            try
            {
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
                return UICommon.SelectRosterPlan(plan, openPlan);
            }
            catch (Exception e)
            {
                errorList.Add("Error selecting rosterplan: " + e.Message);
            }

            return -1;
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

        public List<String> SaveEmployeeDataAsExcel(string stepName)
        {
           return SaveAsExcel(stepName);
        }

        private List<String> ExportToExcell(string postfix, bool showMessages = false)
        {
            var errorList = new List<String>();
            try
            {
                var fileName = ReportFilePath + ReportFileName + postfix;

                UICommon.UIMapVS2017.PreviewFullRosterplan(showMessages);
                UICommon.UIMapVS2017.ExportToExcelFromRosterplanPreview(fileName);
                UICommon.UIMapVS2017.ClosePrintAndPreviewFullRosterplan();
            }
            catch (Exception e)
            {
                errorList.Add("Feil ved export til excel(" + postfix + "): " + e.Message);
            }

            return errorList;
        }
        private List<String> SaveAsExcel(string postfix)
        {
            var errorList = new List<String>();
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
        private void SelectRosterPlanShowInfoFilter(string filter)
        {
            UICommon.ClickRosterplanFilterTab();
            UICommon.UIMapVS2017.SelectViewFilter(filter);
        }
        private void SelectWeeksumShowTotalOnlyFilter()
        {
            UICommon.ClickRosterplanFilterTab();
            UICommon.UIMapVS2017.SelectWeeksumShowTotalOnlyFilter();
        }
        private void SelectShowInactiveLinesInFilter()
        {
            UICommon.UIMapVS2017.SelectShowInactiveLinesInFilter();
        }
        public List<string> SelectRosterPlanAndSelectFilter()
        {
            var errorList = new List<string>();

            try
            {
                SelectRosterPlan("Arbeidsplan");
                UICommon.ClickRosterplanFilterTab();
                SelectShowInactiveLinesInFilter();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 2(filters): " + e.Message);
            }

            try
            {
                UICommon.ClickRosterplanPlanTab();
                Playback.Wait(1000);
                ExportToExcell("_step_2");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 2(export to excel): " + e.Message);
            }

            return errorList;
        }

        public List<string> CreateRosterplanCopy(DateTime fromDate, string rosterWeek, string weeks, bool chkTasks, bool chkDraft)
        {
            var errorList = new List<string>();
            UICommon.CreateNewRosterplanCopy("", fromDate, rosterWeek, weeks, chkTasks, chkDraft);

            OpenExcelFromEmpWindow();
            errorList.AddRange(SaveEmployeeDataAsExcel("_step_3"));

            //Step 4
            RemoveLine22Langeide();
            UICommon.UIMapVS2017.OkCreateRosterplanCopy();
            CloseRosterPlan();

            try
            {
                CheckCopiedPlanValues(SelectRosterPlan("Kopi av Arbeidsplan.", false));
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 4: " + e.Message);
            }

            return errorList;
        }

        public void CheckCopiedPlanValues(int row)
        {
            #region Variable Declarations
            DXCell uIKopiavArbeidsplanCell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIKopiavArbeidsplanCell;
            uIKopiavArbeidsplanCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlansGridControlCell[View]gvRosterPlans[Row]" + row + "[Column]gcolPlan";
            DXCell uIArbeidsplanCell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIArbeidsplanCell;
            uIArbeidsplanCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlansGridControlCell[View]gvRosterPlans[Row]" + row + "[Column]gcolRosterPlanType";
            DXCell uIItem24032014Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem24032014Cell;
            uIItem24032014Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlansGridControlCell[View]gvRosterPlans[Row]" + row + "[Column]gcolValidFrom";
            DXCell uIItem04052014Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem04052014Cell;
            uIItem04052014Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlansGridControlCell[View]gvRosterPlans[Row]" + row + "[Column]gcolValidTo";
            DXCell uIItem25Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem25Cell;
            uIItem25Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlansGridControlCell[View]gvRosterPlans[Row]" + row + "[Column]gcolEmployeeCount";
            DXCell uIItem1900Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem1900Cell;
            uIItem1900Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlansGridControlCell[View]gvRosterPlans[Row]" + row + "[Column]gcolManYears";
            DXCell uIItem5190KopieringCell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem5190KopieringCell;
            uIItem5190KopieringCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlansGridControlCell[View]gvRosterPlans[Row]" + row + "[Column]gcolDepartment";
            DXCell uIItemCell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItemCell;
            uIItemCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlansGridControlCell[View]gvRosterPlans[Row]" + row + "[Column]gcolEffectuated";
            #endregion


            // Verify that the 'ValueAsString' property of 'Kopi av Arbeidsplan.' cell equals 'Kopi av Arbeidsplan.'
            Assert.AreEqual(this.CheckCopiedPlanValuesExpectedValues.UIKopiavArbeidsplanCellValueAsString, uIKopiavArbeidsplanCell.ValueAsString);

            // Verify that the 'ValueAsString' property of 'Arbeidsplan' cell equals 'Arbeidsplan'
            Assert.AreEqual(this.CheckCopiedPlanValuesExpectedValues.UIArbeidsplanCellValueAsString, uIArbeidsplanCell.ValueAsString);

            // Verify that the 'ValueAsString' property of '24.03.2014' cell equals '2024-03-25'
            Assert.AreEqual(this.CheckCopiedPlanValuesExpectedValues.UIItem24032014CellValueAsString, uIItem24032014Cell.ValueAsString);

            // Verify that the 'ValueAsString' property of '04.05.2014' cell equals '2024-05-05'
            Assert.AreEqual(this.CheckCopiedPlanValuesExpectedValues.UIItem04052014CellValueAsString, uIItem04052014Cell.ValueAsString);

            // Verify that the 'ValueAsString' property of '25' cell equals '25'
            Assert.AreEqual(this.CheckCopiedPlanValuesExpectedValues.UIItem25CellValueAsString, uIItem25Cell.ValueAsString);

            // Verify that the 'ValueAsString' property of '19,00' cell equals '19'
            Assert.AreEqual(this.CheckCopiedPlanValuesExpectedValues.UIItem1900CellValueAsString, uIItem1900Cell.ValueAsString);

            // Verify that the 'ValueAsString' property of '5190 - Kopiering' cell equals '5190 - Kopiering'
            Assert.AreEqual(this.CheckCopiedPlanValuesExpectedValues.UIItem5190KopieringCellValueAsString, uIItem5190KopieringCell.ValueAsString);

            // Verify that the 'ValueAsString' property of cell equals ''
            Assert.AreEqual(this.CheckCopiedPlanValuesExpectedValues.UIItemCellValueAsString, uIItemCell.ValueAsString);

        }


        public List<string> CreateRosterplanCopyStep_14(DateTime fromDate, string rosterWeek, string weeks, bool chkTasks, bool chkDraft)
        {
            var errorList = new List<string>();

            SelectRosterPlan("Arbeidsplan");
            UICommon.ClickRosterplanPlanTab();
            UICommon.CreateNewRosterplanCopy("2 Kopi av arbeidsplan", fromDate, rosterWeek, weeks, chkTasks, chkDraft);

            UICommon.UIMapVS2017.UncheckAllEmployeesInNewRosterplanWindow();

            OpenExcelFromEmpWindow();
            errorList.AddRange(SaveEmployeeDataAsExcel("_step_15"));

            SelectEmpsToAddToNewPlan();
            UICommon.UIMapVS2017.OkCreateRosterplanCopy();
            CloseRosterPlan();

            errorList.AddRange(SelectRosterPlanCopyAndExportToXls("2 Kopi av arbeidsplan", "_step_16"));

            return errorList;

        }
        public List<string> SelectRosterPlanCopyAndExportToXls(string plan, string step)
        {
            var errorList = new List<string>();

            SelectRosterPlan(plan);

            try
            {
                UICommon.ClickRosterplanPlanTab();
                Playback.Wait(1000);
                ExportToExcell(step);
            }
            catch (Exception e)
            {
                errorList.Add("Error in " + step + " (export to excel): " + e.Message);
            }

            return errorList;
        }

        public List<string> SelectRoleAssignmentsRosterPlanCopy()
        {
            var errorList = new List<string>();

            UICommon.ClickRosterplanRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();

            try
            {
                CheckRolesStep6();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 6(Role assignment): " + e.Message);
            }

            return errorList;
        }

        public List<string> EffectuateRoasterplanNextPeriod()// DateTime fromDate, DateTime toDate, int step)
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            UICommon.EffectuateRoasterplanNextPeriod();
            UICommon.UIMapVS2017.OpenRegStatusWindow();
            try
            {
                UICommon.UIMapVS2017.CheckEffectuationButtonDisabled();
                CheckRegstatusMessages_step_7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 7: " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRegStatusWindow();
            UnselectTreeEmployees();

            try
            {
                CheckRegstatusWarning();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 8: " + e.Message);
            }

            //ChangePeriodForActualLines(fromDate, toDate);
            //Step 9
            UICommon.EffectuateRosterplanLines(false);
            UICommon.CloseSalaryCalculationsWindow();

            return errorList;
        }

        public List<string> CreateHelpPlan()
        {
            var errorList = new List<string>();

            Playback.Wait(1000);
            UICommon.SelectNewHelpplan();

            try
            {
                UICommon.UIMapVS2017.CheckNewHelpplanWindowTasksDraftUnchecked();
                CheckVakantStatusNewHelpplanWindow();
                OpenExcelFromHelpplanEmpWindow();               
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 10: " + e.Message);
            }

            errorList.AddRange(SaveEmployeeDataAsExcel("_step_10"));

            //Step 11
            UICommon.SetStartDateNewHelpplan(new DateTime(2024, 03, 25));
            Playback.Wait(1000);
            UICommon.SetHelpPlanWeeks("2");
            UICommon.UIMapVS2017.SelectTasksAndDraftNewHelpplanWindow(true);
            SelectF3Calculations_step_11();

            //Playback.Wait(1000);
            //UICommon.SetF3CalculationPeriodHelpplan(new DateTime(2021, 05, 03), new DateTime(2021, 05, 30));
            UICommon.ClickOkCreateHelpPlan();
            UICommon.CloseRosterplanFromPlanTab();

            return errorList;
        }
        public List<string> SelectHelpPlanCopyAndSetFilter()
        {
            var errorList = new List<string>();

            SelectRosterPlan("Hjelpeplan for Kopi av Arbeidsplan..");

            try
            {
                SelectRosterPlanShowInfoFilter("Helge");
                UICommon.ClickRosterplanPlanTab();
                Playback.Wait(1000);
                ExportToExcell("_step_12");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 12(export to excel): " + e.Message);
            }

            UICommon.CloseRosterplanFromPlanTab();

            return errorList;
        }

        public List<string> OpenRosterplanSettingsAndUncheckDraft()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.OpenRosterplanSettings(false);
                UICommon.UIMapVS2017.SetRosterPlanIsDraft(false);
                UICommon.ClickOkRosterplanSettings();
                Playback.Wait(1000);
                ExportToExcell("_step_17", true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 17(export to excel): " + e.Message);
            }

            return errorList;
        }

        public List<string> RemoveJansenShiftAndClosePlan()
        {
            var errorList = new List<string>();

            CloseRosterPlan();
            SelectRosterPlan("2 Kopi av arbeidsplan");
            UICommon.ClickRosterplanPlanTab();

            UICommon.ClickEditRosterPlanFromPlantab();
            RemoveJansenDshift();
            UICommon.ClickOKEditRosterPlanFromPlantab();
            try
            {
                Playback.Wait(1000);
                ExportToExcell("_step_18", true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 18(export to excel): " + e.Message);
            }

            CloseRosterPlan();

            return errorList;
        }

        public List<string> SelectKalenderplanAndCreateKalenderplanCopy()
        {
            var errorList = new List<string>();

            SelectRosterPlan("Kalenderplan");
            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickNewRosterPlanCopy();

            try
            {
                Playback.Wait(500);
                CheckCopyCalendarplanValues();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 22(Calendarplan values): " + e.Message);
            }

            UnCheckIsfjellLine1();
            try
            {
                Playback.Wait(500);
                CheckEmpDataStep_22();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 22(Empdata): " + e.Message);
            }

            UICommon.UIMapVS2017.CreateCalendarplanCopy("", null, true, false);
            UICommon.UIMapVS2017.OkCreateCalendarplanCopy();
            CloseRosterPlan();

            SelectRosterPlan("Kopi av Kalenderplan.");
            UICommon.ClickRosterplanPlanTab();

            try
            {
                Playback.Wait(1000);
                ExportToExcell("_step_23", true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 23(export to excel): " + e.Message);
            }

            return errorList;
        }

        public List<string> OpenCalendarplanSettingsAndCheckNightshiftsChecked()
        {
            var errorList = new List<string>();
            UICommon.OpenRosterplanSettings(false);

            try
            {
                if (!UICommon.CheckStatusNightShiftsOnStartDay())
                    throw new Exception("NightShiftsOnStartDay not checked!");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 24-25(CheckNightshiftsChecked): " + e.Message);
            }

            UICommon.ClickOkRosterplanSettings();

            return errorList;
        }
        public List<string> SelectRoleAssignmentsCalendarplanCopy()
        {
            var errorList = new List<string>();

            UICommon.ClickRosterplanRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSpecificationTab();

            try
            {
                CheckRolesCopiedStep26();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 26(Role assignment): " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            return errorList;
        }

        public List<string> AddVictoriaToPlan()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickEmployeesButtonRosterplan();
                UICommon.ClickEmployeesButtonInEmployeeWindow();
                SelectEmpVictoria();
                UICommon.ClickOkAddEmployeesWindow();
                UICommon.ClickOkEmployeesWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 28: " + e.Message);
            }

            return errorList;
        }

        public List<string> AddVictoriaShiftsAndAbsenceToCalendarplan()
        {
            var errorList = new List<string>();

            CloseRosterPlan();

            SelectRosterPlan("Kopi av Kalenderplan.");
            UICommon.ClickRosterplanPlanTab();
            try
            {
                UICommon.ClickEditRosterPlanFromPlantab();
                AddVictoriaShifts();

                SelectDvorakShift();
                UICommon.SelectAbsenceCode("30", "8");

                SelectHusebyWeek1();
                UICommon.SelectAbsenceCode("45B", "8");
                UICommon.ClickOKEditRosterPlanFromPlantab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 27: " + e.Message);
            }

            return errorList;
        }

        public List<string> InsertNewalenderplanValues()
        {
            var errorList = new List<string>();

            UICommon.ClickNewRosterPlanCopy();

            try
            {
                Playback.Wait(500);
                UICommon.UIMapVS2017.CreateCalendarplanCopy("", new DateTime(2024, 12, 09), false, false);
                CheckPlanWeeekAndDates();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 29(Calendarplan values): " + e.Message);
            }

            return errorList;
        }

        public List<string> CreateKalenderplanCopyOfCopy()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.OkCreateCalendarplanCopy();
            CloseRosterPlan();

            SelectRosterPlan("Kopi av Kopi av Kalenderplan..");
            SelectWeeksumShowTotalOnlyFilter();
            UICommon.ClickRosterplanPlanTab();

            try
            {
                Playback.Wait(1000);
                ExportToExcell("_step_30", true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 30(export to excel): " + e.Message);
            }

            return errorList;
        }

        public List<string> OpenBaseplanAndCreateCopy()
        {
            var errorList = new List<string>();

            CloseRosterPlan();
            try
            {
                SelectRosterPlan("Baseplan");
                UICommon.ClickRosterplanPlanTab();
                UICommon.ClickNewBasePlanCopy();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 31: " + e.Message);
            }

            return errorList;
        }

        public List<string> AddBaseplanCopyValues()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.CreateNewBaseplanCopy("", "2", new DateTime(2024, 08, 12), "2", false);
                RemoveGundersenLine3();
                UICommon.UIMapVS2017.OkNewBaseplanCopy();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 32: " + e.Message);
            }

            return errorList;
        }

        public List<string> OpenBaseplanCopy()
        {
            var errorList = new List<string>();

            CloseRosterPlan();
            SelectRosterPlan("Kopi av Baseplan");
            UICommon.ClickRosterplanPlanTab();

            try
            {
                Playback.Wait(1000);
                ExportToExcell("_step_33", true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 33(export to excel): " + e.Message);
            }

            return errorList;
        }

        public List<string> OpenMasterlisteAndCreateCopy()
        {
            var errorList = new List<string>();

            CloseRosterPlan();
            try
            {
                SelectRosterPlan("Masterliste");
                UICommon.ClickRosterplanPlanTab();
                UICommon.ClickNewRosterPlanCopy();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 34: " + e.Message);
            }

            UICommon.UIMapVS2017.OkCreateMasterlistCopy();
            CloseRosterPlan();

            SelectRosterPlan("Kopi av Masterliste.");
            UICommon.ClickRosterplanPlanTab();

            try
            {
                Playback.Wait(1000);
                ExportToExcell("_step_35", true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 23(export to excel): " + e.Message);
            }

            return errorList;
        }

        public virtual CheckCopiedPlanValuesExpectedValues CheckCopiedPlanValuesExpectedValues
        {
            get
            {
                if ((this.mCheckCopiedPlanValuesExpectedValues == null))
                {
                    this.mCheckCopiedPlanValuesExpectedValues = new CheckCopiedPlanValuesExpectedValues();
                }
                return this.mCheckCopiedPlanValuesExpectedValues;
            }
        }

        private CheckCopiedPlanValuesExpectedValues mCheckCopiedPlanValuesExpectedValues;
    }
    /// <summary>
    /// Parameters to be passed into 'CheckCopiedPlanValues'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class CheckCopiedPlanValuesExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Kopi av Arbeidsplan.' cell equals 'Kopi av Arbeidsplan.'
        /// </summary>
        public string UIKopiavArbeidsplanCellValueAsString = "Kopi av Arbeidsplan.";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Arbeidsplan' cell equals 'Arbeidsplan'
        /// </summary>
        public string UIArbeidsplanCellValueAsString = "Arbeidsplan";

        /// <summary>
        /// Verify that the 'ValueAsString' property of '24.03.2014' cell equals '2014-03-24'
        /// </summary>
        public string UIItem24032014CellValueAsString = "2024-03-25";

        /// <summary>
        /// Verify that the 'ValueAsString' property of '04.05.2014' cell equals '2014-05-04'
        /// </summary>
        public string UIItem04052014CellValueAsString = "2024-05-05";

        /// <summary>
        /// Verify that the 'ValueAsString' property of '25' cell equals '25'
        /// </summary>
        public string UIItem25CellValueAsString = "25";

        /// <summary>
        /// Verify that the 'ValueAsString' property of '19,00' cell equals '19'
        /// </summary>
        public string UIItem1900CellValueAsString = "19";

        /// <summary>
        /// Verify that the 'ValueAsString' property of '5190 - Kopiering' cell equals '5190 - Kopiering'
        /// </summary>
        public string UIItem5190KopieringCellValueAsString = "5190 - Kopiering";

        /// <summary>
        /// Verify that the 'ValueAsString' property of cell equals ''
        /// </summary>
        public string UIItemCellValueAsString = "";
        #endregion
    }
}
