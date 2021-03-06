namespace _020_Test_Arbeidsplan_Ansatte
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
    using System.Globalization;
    using System.Threading;
    using CommonTestData;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using System.Diagnostics;
    using System.IO;

    public partial class UIMap
    {

        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        public string ReportFilePath;
        public string ReportFileName = "020_excel";
        public string FileType = ".xls";
        #endregion

        public UIMap(TestContext testContest)
        {
            TestContext = testContest;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_020_Ansatte\");

            UICommon = new CommonUIFunctions.UIMap(testContest);
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

        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepArbeidsplanOghjelpeplan, null, "", logGatInfo);
        }
        private void ChangeDepartment(string dep, List<string> other = null)
        {
            UICommon.ChangeDepartmentFromRosterplan(dep, other);
        }

        public void CreateRosterplan(string name, DateTime fromDate, DateTime validDate, string weeks)
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            UICommon.ClickNewRosterplanButton();
            UICommon.UIMapVS2017.SetRosterPlanName(name);
            UICommon.SetStartDateRosterplan(fromDate);
            UICommon.UIMapVS2017.SetRosterPlanWeeks(weeks);
            UICommon.SetValidToDateRosterplan(validDate);

            UICommon.ClickOkRosterplanSettings();
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

        public void SelectRosterPlanFilter(string filter)
        {
            UICommon.ClickRosterplanFilterTab();
            UICommon.UIMapVS2017.SelectViewFilter(filter);
            //UICommon.ClickRosterplanHomeTab();
            //UICommon.ClickRosterplanPlanTab();
            //UICommon.ClickRosterplanPrintTab();
            //UICommon.ClickRosterplanSupportToolTab();

        }

        public List<string> AddAndCheckEmpsInEmpWindow()
        {
            var errorList = new List<string>();

            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            try
            {
                CheckEmpWindow_step_5();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 5: " + e.Message);
            }

            UICommon.ClickEmployeesButtonInEmployeeWindow();
            Add4EmployeesStep_6();

            UICommon.ClickOkAddEmployeesWindow();

            try
            {
                Check4EmployeesAddedStep_8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 8: " + e.Message);
            }

            UICommon.ClickOkEmployeesWindow();
            UICommon.ClickEmployeesButtonRosterplan();
            Playback.Wait(1000);
            UICommon.ClickEmployeesButtonInEmployeeWindow();

            if (!CheckEmployeesNotInList(9))
                errorList.Add("Added employees still in list");

            Add2EmployeesStep_10();
            UICommon.ClickOkAddEmployeesWindow();

            try
            {
                Check2AddedCorretlyToListStep_10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 10: " + e.Message);
            }

            Delete1EmployeeStep_11();

            try
            {
                CheckBarskeRemoved_Step_11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 11: " + e.Message);
            }

            UICommon.ClickOkEmployeesWindow();

            //Step 12            
            try
            {
                CheckPlanHasCorrectEmployees_Step_12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 12: " + e.Message);
            }

            return errorList;
        }

        public List<string> AddVakantFromEmpWindow()
        {
            var errorList = new List<string>();

            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.UIMapVS2017.ClickVakantButtonInEmployeeWindow();

            try
            {
                CheckVakantAddedStep_13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 13: " + e.Message);
            }

            return errorList;
        }

        public List<string> MoveEmpsInListAndAddToPlan()
        {
            var errorList = new List<string>();

            MoveEmployeesStep_14();

            try
            {
                CheckMovedEmployeesStep_14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 14: " + e.Message);
            }

            UICommon.ClickOkEmployeesWindow();

            try
            {
                CheckEmpsAddedCorretlyToPlantStep_14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 14: " + e.Message);
            }

            return errorList;
        }

        public List<string> EditVakantDataInEmpWindow()
        {
            var errorList = new List<string>();

            UICommon.ClickEmployeesButtonRosterplan();

            try
            {
                EditVakantStep_15();
                EditVakantPositionStep_16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 15/16: " + e.Message);
            }

            UICommon.ClickOkEmployeesWindow();
            Playback.Wait(1000);
            errorList.AddRange(ExportToExcell("_step_17"));

            return errorList;
        }

        public List<string> EditEriksroedAndBetongDataInEmpWindow()
        {
            var errorList = new List<string>();

            UICommon.ClickEmployeesButtonRosterplan();

            try
            {
                EditEmpsPositionsStep_17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 17: " + e.Message);
            }

            UICommon.ClickOkEmployeesWindow();
            UICommon.ClickEmployeesButtonRosterplan();

            try
            {
                SelectBetongPosition();
                CheckBetongPositionStep_17();
                SelectEriksroedPosition();
                CheckEriksroedPositionStep_17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 17-2: " + e.Message);
            }

            return errorList;
        }

        public List<string> CreateEriksroedWeekendpattern()
        {
            var errorList = new List<string>();

            try
            {
                SelectEriksroed();
                CreateWeekendpattern_18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 18: " + e.Message);
            }

            try
            {
                CheckWeekendpattern_18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 18-2: " + e.Message);
            }

            return errorList;
        }

        public List<string> CreateNewUtnePosition()
        {
            var errorList = new List<string>();
            UICommon.ClickOkEmployeesWindow();

            CloseRosterPlan();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
            try
            {
                SelectUtne();
                UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.EmploymentTab, false);
                NewEmployment(new DateTime(2023, 01, 12), new DateTime(2099, 12, 31), "25", "Dagtid", "Dagtid", "Dagarbeider", "2");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 19: " + e.Message);
            }

            return errorList;
        }

        public List<string> AddUtneNewPositionFromEmpWindow()
        {
            var errorList = new List<string>();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            UICommon.SelectRosterPlan("Arbeidsplan 2023");
            UICommon.ClickRosterplanPlanTab();

            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            AddEmployeeUtneStep_20();

            UICommon.ClickOkAddEmployeesWindow();

            try
            {
                CheckUtneAddedStep_20();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 20: " + e.Message);
            }

            return errorList;
        }

        public List<string> CheckUtneNotInEmpList()
        {
            var errorList = new List<string>();

            UICommon.ClickEmployeesButtonInEmployeeWindow();
            if (!CheckEmployeesNotInList(21))
                errorList.Add("Emp Ulne still in list: step 21");

            return errorList;
        }

        public List<string> Step_22()
        {
            var errorList = new List<string>();

            SelectIncludeExistingEmployees();

            try
            {
                CheckUtneIsInListStep_22();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 22: " + e.Message);
            }

            return errorList;
        }

        public List<string> AddUtneIPNumber2()
        {
            var errorList = new List<string>();

            SelectEmployeeUtneStep_23();
            Playback.Wait(1000);
            UICommon.ClickOkAddEmployeesWindow();

            try
            {
                CheckUtneAddedStep_23();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 23: " + e.Message);
            }

            return errorList;
        }

        public List<string> AddVakantEmpsFromVariosDeps()
        {
            var errorList = new List<string>();

            AddVakantDep5020Step_24();

            try
            {
                CheckVakant5020AddedStep_24();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 24: " + e.Message);
            }

            AddVakantDep4010Step_25();

            try
            {
                CheckVakant4010AddedStep_25();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 25: " + e.Message);
            }

            AddVakantDep5110Step_26();

            try
            {
                CheckVakant5110AddedStep_26();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 26: " + e.Message);
            }

            return errorList;
        }

        private void AddVakantDep5020Step_24()
        {
            SelectDep5020();
            AddVakantFromSelectedDep();
        }

        private void AddVakantDep4010Step_25()
        {
            SelectDep4010();
            AddVakantFromSelectedDep();
        }

        private void AddVakantDep5110Step_26()
        {
            SelectDep5110();
            AddVakantFromSelectedDep();
        }
        private void AddVakantFromSelectedDep()
        {
            #region Variable Declarations    
            DXButton uIVelgButton = this.UIItemWindow3.UIPopupContainerBarConMenu.UIDepartmentPopupContrClient.UIVelgButton;
            uIVelgButton.SearchProperties[DXTestControl.PropertyNames.Name] = "buttonOkDepartment";
            DXButton uIVakantButton = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UIVakantButton;
            uIVakantButton.SearchProperties[DXTestControl.PropertyNames.Name] = "buttonAddVacant";
            #endregion

            Mouse.Click(uIVelgButton);
            Mouse.Click(uIVakantButton);
        }

        public List<string> EditVakantPositionsAndMoveToTop()
        {
            var errorList = new List<string>();

            try
            {
                EditPositionsAndMoveToTopStep_27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 27: " + e.Message);
            }

            try
            {
                CheckVakantsMovedToTop();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 27-2: " + e.Message);
            }

            return errorList;
        }

        public List<string> SaveChangesAndAdd4Employees()
        {
            var errorList = new List<string>();

            UICommon.ClickOkEmployeesWindow();
            UICommon.ClickEmployeesButtonRosterplan();

            try
            {
                Playback.Wait(1000);
                CheckEmpPositionsInListStep_29();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 29: " + e.Message);
            }

            UICommon.ClickEmployeesButtonInEmployeeWindow();

            Add4EmployeesStep_29();

            UICommon.ClickOkAddEmployeesWindow();

            try
            {
                Check4EmpsAddedStep_29();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 29-2: " + e.Message);
            }

            return errorList;
        }

        public List<string> DeleteRaserPositionLine()
        {
            var errorList = new List<string>();

            DeleteRaserPosition();

            try
            {
                Playback.Wait(1000);
                CheckEmpPositionsInListAfterDeleteRaserPositionLine();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 30: " + e.Message);
            }

            return errorList;
        }

        public List<string> SetBronsonLineInactive()
        {
            var errorList = new List<string>();

            SetBronsonInactive();

            try
            {
                Playback.Wait(1000);
                CheckEmpBronsonInactive();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 31: " + e.Message);
            }

            return errorList;
        }

        public List<string> SaveChangesAndCheckAndCheckRosterplanLines()
        {
            var errorList = new List<string>();

            UICommon.ClickOkEmployeesWindow();


            Playback.Wait(1000);
            errorList.AddRange(ExportToExcell("_step_32"));

            return errorList;
        }

        public List<string> AddShiftcodesInRosterplan()
        {
            var errorList = new List<string>();

            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                AddShifts();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 33: " + e.Message);
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();

            return errorList;
        }

        private void AddShifts()
        {
            AddShiftsStep_33();
            AddF1ShiftStep_33();
            AddShiftsStep_33_1();
            DeleteShifts();
        }

        public List<string> CreateHelpPlan()
        {
            var errorList = new List<string>();
            try
            {
                UICommon.SelectNewHelpplan();
                UICommon.SetStartDateNewHelpplan(new DateTime(2023, 03, 27));
                Playback.Wait(1000);

                UICommon.SetHelpPlanWeeks("3");
                Playback.Wait(1000);
                SetEmployeeF3Calculations();

                UICommon.ClickOkCreateHelpPlan();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 34: " + e.Message);
            }

            return errorList;
        }
        public List<string> OpenHelpPlanAndCheckCalculations()
        {
            var errorList = new List<string>();

            try
            {
                CloseRosterPlan();

                UICommon.SelectRosterPlan("Hjelpeplan for Arbeidsplan 2023.");
                SelectRosterPlanFilter("Helge");
                CheckEmployeeF3bCalculationsStep35();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 35(F3b.): " + e.Message);
            }
            try
            {
                CheckSHCalculations();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 35(SH): " + e.Message);
            }
            try
            {
                CheckF3Calculations();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 35(F3): " + e.Message);
            }

            return errorList;
        }

        public List<string> HelpPlanEditF3()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickRosterplanPlanTab();
                UICommon.ClickEmployeesButtonRosterplan();
                SetEmployeesNewF3Calculations();
                UICommon.ClickOkEmployeesWindow();

                CheckEmployeesF3bCalculationsStep36();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 36(F3b.): " + e.Message);
            }
            try
            {
                CheckSHCalculations();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 36(SH): " + e.Message);
            }
            try
            {
                CheckF3Calculations();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 35(F3): " + e.Message);
            }


            return errorList;
        }

        public void CreateCalendarplan(string name, DateTime startDate, string weeks)
        {
            CloseRosterPlan();
            UICommon.ClickNewRosterplanButton();
            UICommon.UIMapVS2017.SetRosterPlanName(name);
            SetTypeCalendarplan();
            UICommon.SetStartDateRosterplan(startDate);
            UICommon.UIMapVS2017.SetRosterPlanWeeks(weeks);
            UICommon.ClickOkRosterplanSettings();
        }
        public void AddEmployeeToCalendarplan()
        {
            UICommon.ClickRosterplanPlanTab();

            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            AddEmployeeFranzenStep_38();

            UICommon.ClickOkAddEmployeesWindow();
            UICommon.ClickOkEmployeesWindow();
        }

        public List<string> AddShiftsToCalendarplan()
        {
            var errorList = new List<string>();

            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                AddCalendarplanShifts();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 39: " + e.Message);
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();
            CloseRosterPlan();

            return errorList;
        }

        public List<string> Open2023AddHansenAndShifts()
        {
            var errorList = new List<string>();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            UICommon.SelectRosterPlan("Arbeidsplan 2023");
            UICommon.ClickRosterplanPlanTab();

            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            AddEmployeeHansenStep_40();
            UICommon.ClickOkAddEmployeesWindow();
            UICommon.ClickOkEmployeesWindow();
            Playback.Wait(5000);
            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                AddRosterplanShiftsStep_40();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 40: " + e.Message);
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();
            CloseRosterPlan();

            return errorList;
        }

        public void CreateRosterplanInDep5040()
        {
            ChangeDepartment(UICommon.DepMasterplan);
            CreateRosterplan("Annen plan", new DateTime(2023, 02, 27), new DateTime(2023, 04, 30), "6");
        }

        public List<string> Add2EmpsAndShiftsToAnnenPlan()
        {
            var errorList = new List<string>();

            UICommon.ClickRosterplanPlanTab();

            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            AddEmployeesStep_42();
            UICommon.ClickOkAddEmployeesWindow();
            UICommon.ClickOkEmployeesWindow();

            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                AddRosterplanShiftsStep_42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 42: " + e.Message);
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();
            CloseRosterPlan();

            return errorList;
        }

        public List<string> ChangeDepsOpenHelpplanAndCheckEmps()
        {
            var errorList = new List<string>();
            var otherDeps = new List<string>() { UICommon.DepMasterplan };

            ChangeDepartment(UICommon.DepArbeidsplanOghjelpeplan, otherDeps);

            UICommon.SelectRosterPlan("Hjelpeplan for Arbeidsplan 2023.");
            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickAddEmployeesFromBaseplanButtonInEmployeeWindow();

            try
            {
                Playback.Wait(1000);
                CheckListHasExpectedRows(1);
                CheckOnlyHansenInList();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 43: " + e.Message);
            }

            return errorList;
        }

        public List<string> OpenSelectionOptionsSelectAllPlansAndCheckEmpAvailable()
        {
            var errorList = new List<string>();

            OpenSelectionOptionsSelectAllPlans();
            errorList.AddRange(Check4EmpsInList());

            return errorList;
        }

        private void CheckListHasExpectedRows(int rowCount)
        {
            #region Variable Declarations
            var table = this.UILeggtilansatteWindow.UIViewHostCustom.UIPcViewClient.UISelectPlanEmployeesVCustom.UIPcContentContainerClient.UIPcContentClient.UIGcPlanEmployeesTable;
            #endregion

            Assert.AreEqual(rowCount, table.Views[0].RowCount);
        }

        private List<string> Check4EmpsInList()
        {
            #region Variable Declarations

            var errorList = new List<string>();
            var table = this.UILeggtilansatteWindow.UIViewHostCustom.UIPcViewClient.UISelectPlanEmployeesVCustom.UIPcContentContainerClient.UIPcContentClient.UIGcPlanEmployeesTable;

            #endregion

            try
            {
                CheckListHasExpectedRows(4);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 44(unexpected number of rows): " + e.Message);
            }

            var view = table.Views[0];
            for (int i = 0; i < view.RowCount; i++)
            {
                var val = view.GetCellValue("colEmployee", i).ToString().Trim();
                if (val == "Abrahamsen, Trond")
                {
                    var dep = view.GetCellValue("colDepartment", i).ToString().Trim();

                    try
                    {
                        Assert.AreEqual("5040 - Masterplan/masterliste", dep);
                    }
                    catch (Exception e)
                    {
                        errorList.Add("Error in step 44(Abrahamsen, Trond): " + e.Message);
                    }

                }
                else if (val == "Berg, Magnus")
                {
                    var dep = view.GetCellValue("colDepartment", i).ToString().Trim();

                    try
                    {
                        Assert.AreEqual("5040 - Masterplan/masterliste", dep);
                    }
                    catch (Exception e)
                    {
                        errorList.Add("Error in step 44(Berg, Magnus): " + e.Message);
                    }

                }
                else if (val == "Franzen, Finbeck")
                {
                    var dep = view.GetCellValue("colDepartment", i).ToString().Trim();

                    try
                    {
                        Assert.AreEqual("5010 - Arbeidsplan og hjelpeplan", dep);
                    }
                    catch (Exception e)
                    {
                        errorList.Add("Error in step 44(Franzen, Finbeck): " + e.Message);
                    }

                }
                else if (val == "Hansen, Henriette")
                {
                    var dep = view.GetCellValue("colDepartment", i).ToString().Trim();

                    try
                    {
                        Assert.AreEqual("5010 - Arbeidsplan og hjelpeplan", dep);
                    }
                    catch (Exception e)
                    {
                        errorList.Add("Error in step 44(Hansen, Henriette): " + e.Message);
                    }
                }
            }

            return errorList;
        }

        public List<string> SelectAllEmpsAndSetF3Caluculation()
        {
            var errorList = new List<string>();

            try
            {
                SelectAllEmpsStep_45();
                UICommon.ClickOkAddEmployeesFromBaseplanWindow();
                ShowBottomRow();
                SetF3ToNewEmpStep_45();
                UICommon.ClickOkEmployeesWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 45: " + e.Message);
            }

            Playback.Wait(1000);
            errorList.AddRange(ExportToExcell("_step_45"));

            return errorList;
        }

        public List<string> ChangeNewEmployeesValidPeriods()
        {
            var errorList = new List<string>();

            UICommon.ClickEmployeesButtonRosterplan();
            selectNewEmpsStep_46();
            UICommon.UIMapVS2017.CreateValidPeriodForEmpsInPlan(new DateTime(2023, 04, 03), new DateTime(2023, 04, 09));
            UICommon.ClickOkEmployeesWindow();

            Playback.Wait(1000);
            errorList.AddRange(ExportToExcell("_step_46"));

            return errorList;
        } 
        public List<string> SelectEmployeesToCalendarplanStep_48()
        {
            var errorList = new List<string>();

            UICommon.ClickRosterplanPlanTab();

            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            UICommon.SelectAllEmployeesAddEmployeesWindow();
            UICommon.ClickOkAddEmployeesWindow();

            try
            {
                Playback.Wait(1000);
                CheckAllEmpsIsInList();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 48: " + e.Message);
            }

            return errorList;
        }

        public List<string> AddEmployeesToCalendarplanStep_49()
        {
            var errorList = new List<string>();
            UICommon.ClickOkEmployeesWindow();

            Playback.Wait(1000);
            errorList.AddRange(ExportToExcell("_step_49"));

            CloseRosterPlan();

            return errorList;
        }

        public void DeleteRosterplans()
        {
            var rosterplans = new List<string>() { "Hjelpeplan for Arbeidsplan 2023.", "Arbeidsplan 2023", "Annen plan", "Franzen", "Høst 2023" };
            UICommon.DeleteRosterplans(rosterplans);

            CloseGat();
        }

        private void NewEmployment(DateTime fromDate, DateTime toDate, string empPercent, string ruleSet, string posCategory, string aml, string internalPosNumber)
        {
            UICommon.CreateNewEmployment(fromDate, toDate, empPercent, ruleSet, posCategory, aml, internalPosNumber, "","");
        }

        public List<string> ExportToExcell(string postfix)
        {
            var errorList = new List<string>();
            try
            {
                var fileName = ReportFilePath + ReportFileName + postfix;

                UICommon.UIMapVS2017.PreviewFullRosterplan();
                UICommon.UIMapVS2017.ExportToExcelFromRosterplanPreview(fileName);
                UICommon.UIMapVS2017.ClosePrintAndPreviewFullRosterplan();
            }
            catch (Exception e)
            {
                errorList.Add("Feil ved export til excel(" + postfix + "): " + e.Message);
            }

            return errorList;
        }

        public bool CheckEmployeesNotInList(int step)
        {
            #region Variable Declarations
            var leggtilansatteWindow = this.UILeggtilansatteWindow;
            #endregion

            var empTable = leggtilansatteWindow.UIViewHostCustom.UIPcViewClient.UISelectDepartmentEmplCustom.UIPcContentContainerClient.UIPcContentClient.UIGcDepartmentEmployeeTable;

            var view = empTable.Views[0];
            var rowCount = view.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                var val = view.GetCellValue("colEmployee", i).ToString().Trim().ToLower();
                if (step == 9)
                {
                    if (val == "andersen, astrid" || val == "barske, brynjulf" || val == "betong, billy" || val == "andersen, astrid" || rowCount != 15)
                        return false;
                }
                else if (step == 21)
                {
                    if (val == "ulne, ulf")
                        return false;
                }
            }

            return true;
        }

        public void CreateWeekendpattern_18()
        {
            #region Variable Declarations
            DXButton uILaghelgemønsterButton = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient1.UIEmployeeManagerEmploCustom.UILaghelgemønsterButton;
            DXLookUpEdit uILueStandardWeekendPaLookUpEdit = this.UIItemWindow.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UIViewHostCustom.UIPcViewClient.UIEmployeeManagerWeekeCustom.UILueStandardWeekendPaLookUpEdit;
            DXButton uIOKButton = this.UIItemWindow.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UIOKButton;
            #endregion

            //Click 'Lag helgemønster' button
            Mouse.Click(uILaghelgemønsterButton);
            Playback.Wait(500);
            Mouse.Click(uILueStandardWeekendPaLookUpEdit);
            Playback.Wait(500);
            Keyboard.SendKeys(uILueStandardWeekendPaLookUpEdit, "{DOWN 3}{ENTER}");
            Playback.Wait(500);
            Mouse.Click(uIOKButton);
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
        public List<String> CompareReportDataFiles_Test020()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext, 3);
            return errorList;
        }
        public void AssertResults(List<string> errorList)
        {
            UICommon.AssertResults(errorList);
        }
    }
}
