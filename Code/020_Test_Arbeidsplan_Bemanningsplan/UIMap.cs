namespace _020_Test_Arbeidsplan_Bemanningsplan
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
    using System.Threading;
    using System.Globalization;
    using System.IO;
    using CommonTestData;
    using System.Diagnostics;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;


    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        public string ReportFilePath;
        public string ReportFileName = "020_excel";
        public string FileType = ".xls";
        private UIMapVS2017Classes.UIMapVS2017 map2017;
        #endregion


        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_020_Bemanningsplan\");

            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }
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
            UICommon.LoginGatAndSelectDepartment(UICommon.DepArbeidsplanOghjelpeplan, null, "", logGatInfo);
        }

        public void SelectDepartmentTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Department);
        }
        public List<string> CreateStaffingplanQ1()
        {
            var errorList = new List<string>();
            SelectStaffingplanTab();
            ClickNewStaffingplan();
            try
            {
                CheckPlanNameEmpty();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            NameStaffingplan("Q1 2022");

            UICommon.SetManningplanPeriod(new DateTime(2022, 01, 03), new DateTime(2022, 03, 27));
            AddComment();

            AddNewStaffingLayer();
            AddGrunnbemanningLayer();
            AddManningShiftCodes();
            AddManning();

            OpenExcelFromTimeYearTab();
            errorList.AddRange(ExportAsExcel("_step_5_1"));
            OpenExcelTotalFromTimeYearTab();
            errorList.AddRange(ExportAsExcel("_step_5_2"));

            ClickOkSaveStaffingLayer();

            //Desom denne trengs
            //UICommon.SetManningLayerFromDate(nextMonday.AddDays(21));

            return errorList;
        }

        public void SelectStaffingplanTab()
        {
            UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Bemanningsplan, false);
        }

        public List<string> CreateStaffingplanNatt()
        {
            var errorList = new List<string>();

            ClickNewStaffingplan();
            try
            {
                CheckPlanNameEmpty();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            NameStaffingplan("Nattevakter SPL");

            UICommon.SetManningplanPeriod(new DateTime(2022, 01, 03), new DateTime(2022, 03, 27));
            AddComment();

            AddNewStaffingLayer();
            AddNattLayer();
            AddNattShiftCodes();
            AddNattManning();

            ClickOkSaveStaffingLayer();

            SelectDraft();
            ClickOkSaveStaffingPlan();


            return errorList;
        }

        public List<string> DuplicateStaffingplan()
        {
            var errorList = new List<string>();

            SelectStaffingPlan("Nattevakter SPL", false);
            ClickDuplicateStaffingplan();
            try
            {
                CheckPlanNameEmptyAndDraft();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            NameStaffingplan("Duplikat");
            try
            {
                CheckOkEnabled();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            ClickOkSaveStaffingPlan();

            return errorList;
        }

        public void DeleteStaffingplan(string name)
        {
            try
            {
                SelectStaffingPlan(name, false);
                DeleteStaffingplan();
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Error deleting plan: " + name + ", " + e.Message);
            }
        }

        public List<string> CheckStaffingplansDeleted()
        {
            var errorList = new List<string>();

            if (SelectStaffingPlan("Duplikat", false) > -1)
                errorList.Add("Duplikat er ikke slettet");

            if (SelectStaffingPlan("Nattevakter SPL", false) > -1)
                errorList.Add("Nattevakter SPL er ikke slettet");

            if (SelectStaffingPlan("Q1 2022", false) > -1)
                errorList.Add("Q1 2022 er ikke slettet");

            return errorList;
        }

        public void NameStaffingplan(string name)
        {
            #region Variable Declarations
            DXTextEdit uIENameEdit = this.UIBemanningsplanWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIENameEdit;
            #endregion

            // Type 'Q1 2022' in 'eName' text box
            //ValueAsString
            uIENameEdit.ValueAsString = name;

            // Type '{Tab}' in 'eName' text box
            Keyboard.SendKeys(uIENameEdit, "{Tab}");
        }

        private void AddManningShiftCodes()
        {
            #region Variable Declarations
            DXCell uIVelgvaktkodeCell = this.UIBemanningsplanlagWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccLayersScrollableControl.UIGcItemsTable.UIVelgvaktkodeCell;
            //DXWindow uIPopupLookUpEditFormWindow = this.UIBemanningsplanlagWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccLayersScrollableControl.UIGcItemsTable.UIRow2147483647ColumncLookUpEdit.UIPopupLookUpEditFormWindow;
            #endregion

            // Click 'Velg vaktkode' cell
            Mouse.Click(uIVelgvaktkodeCell);
            Keyboard.SendKeys("{DOWN}{ENTER}");

            // Click 'Velg vaktkode' cell
            Mouse.Click(uIVelgvaktkodeCell);
            Keyboard.SendKeys("{DOWN 2}{ENTER}");

            // Click 'Velg vaktkode' cell
            Mouse.Click(uIVelgvaktkodeCell);
            Keyboard.SendKeys("{DOWN 4}{ENTER}");

            // Click 'Velg vaktkode' cell
            Mouse.Click(uIVelgvaktkodeCell);
            Keyboard.SendKeys("{DOWN 10}{ENTER}");

            // Click 'Velg vaktkode' cell
            Mouse.Click(uIVelgvaktkodeCell);
            Keyboard.SendKeys("{DOWN 13}{ENTER}");
        }

        private void AddNattShiftCodes()
        {
            #region Variable Declarations
            DXCell uIVelgvaktkodeCell = this.UIBemanningsplanlagWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccLayersScrollableControl.UIGcItemsTable.UIVelgvaktkodeCell;
            #endregion

            Mouse.Click(uIVelgvaktkodeCell);
            Keyboard.SendKeys("{DOWN 13}{ENTER}");
        }

        private void AddLederShiftCodes()
        {
            #region Variable Declarations
            DXCell uIVelgvaktkodeCell = this.UIBemanningsplanlagWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccLayersScrollableControl.UIGcItemsTable.UIVelgvaktkodeCell;
            //DXWindow uIPopupLookUpEditFormWindow = this.UIBemanningsplanlagWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccLayersScrollableControl.UIGcItemsTable.UIRow2147483647ColumncLookUpEdit.UIPopupLookUpEditFormWindow;
            #endregion

            // Click 'Velg vaktkode' cell
            Mouse.Click(uIVelgvaktkodeCell);
            Keyboard.SendKeys("{DOWN 15}{ENTER}");
        }

        public void AddVinterferieShiftCodes()
        {
            #region Variable Declarations
            DXCell uIVelgvaktkodeCell = this.UIBemanningsplanlagWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccLayersScrollableControl.UIGcItemsTable.UIVelgvaktkodeCell;
            #endregion

            Mouse.Click(uIVelgvaktkodeCell);
            Keyboard.SendKeys("{DOWN 5}{ENTER}");

            Mouse.Click(uIVelgvaktkodeCell);
            Keyboard.SendKeys("{DOWN 23}{ENTER}");

            Mouse.Click(uIVelgvaktkodeCell);
            Keyboard.SendKeys("{DOWN 24}{ENTER}");
        }

        public void SelectEnkelMedDetaljer()
        {
            #region Variable Declarations
            DXLookUpEdit uILeSumDemandDataSourcLookUpEdit = this.UIBemanningsplanWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccSumScrollableControl.UITcGraphicsTabList.UITpSumClient.UIPMainPanelClient.UIPTopPanelClient.UILeSumDemandDataSourcLookUpEdit;
            #endregion

            uILeSumDemandDataSourcLookUpEdit.SelectedIndex = 1;
            Keyboard.SendKeys(uILeSumDemandDataSourcLookUpEdit, "{TAB}");
        }

        public void CreateLederLayer()
        {
            AddNewStaffingLayer();
            AddLederLayer();
            SendTab();
            AddLederShiftCodes();
            AddLederLayerManning();
            ClickOkSaveStaffingLayer();
        }

        public void CheckStaffingPlanIsDraft()
        {
            #region Variable Declarations
            DXCell uIJACell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIGcStaffingPlansTable.UIJACell;
            #endregion

            var position = SelectStaffingPlan("Duplikat", false);
            uIJACell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcStaffingPlansGridControlCell[View]gvStaffingPlans[Row]" + position + "[Column]gcolDraft";

            // Verify that the 'ValueAsString' property of 'Ja' cell equals 'Ja'
            Assert.AreEqual("Ja", uIJACell.ValueAsString, "Planen vises ikke som kladd");
        }

        public void CheckPlanIsValid(string step)
        {
            #region Variable Declarations 
            var dayRythmPlanGrid = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIGcStaffingPlansTable;
            #endregion

            var view = dayRythmPlanGrid.Views[0];
            var timeFormat = DateTime.Now.Date.ToString("ddMMyyyy") + "_" + DateTime.Now.TimeOfDay.ToString("hh\\mm");
            try
            {
                dayRythmPlanGrid.CaptureImage().Save(ReportFilePath + "Staffingplan_" + step + "_" + timeFormat + ".jpg");
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Error capturing image: " + step + ": " + e.Message);
            }
        }

        public void AddRequirementsToD1()
        {
            DXCell uIItemCell11 = this.UISettekravtilpersonerWindow.UIGrpRequirementsClient.UIGcRequirementTable.UIItemCell1;

            AddRequirementsToD1_1();

            // Click cell
            Mouse.Click(uIItemCell11, new Point(173, 9));
            Keyboard.SendKeys("{DOWN 12}{ENTER}");

            AddRequirementsToD1_2();

        }

        public int SelectStaffingPlan(string planName, bool openPlan = true)
        {
            #region Variable Declarations 
            var dayRythmPlanGrid = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIGcStaffingPlansTable;
            int retVal = -1;

            var selectCell = new DXCell();
            #endregion

            var view = dayRythmPlanGrid.Views[0];
            planName = planName.Trim().ToLower();
            for (int i = 0; i < view.RowCount; i++)
            {
                var val = view.GetCellValue("gcolPlan", i).ToString().Trim().ToLower();

                if (val == planName)
                {
                    selectCell = view.GetCell("gcolPlan", i);
                    selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcStaffingPlansGridControlCell[View]gvStaffingPlans[Row]" + i + "[Column]gcolPlan";
                    retVal = i;
                    Playback.Wait(1000);
                    break;
                }
            }

            if (retVal < 0)
                return retVal;

            try
            {
                Playback.Wait(1000);
                Mouse.Click(selectCell);
            }
            catch (Exception)
            {
                TestContext.WriteLine("Error selecting plancell");
            }

            if (openPlan)
            {
                Mouse.DoubleClick(selectCell);
            }

            Playback.Wait(2000);
            return retVal;
        }

        public List<string> ExportAsExcel(string stepName)
        {
           return SaveAsExcel(stepName);
        }
        private List<string> SaveAsExcel(string stepName)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion
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

        public List<string> CheckValues_Step_27()
        {
            var errorList = new List<string>();

            SelectHoursYearTab();

            try
            {
                UIMapVS2017.CheckValues_Step_27();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }
    }
}
