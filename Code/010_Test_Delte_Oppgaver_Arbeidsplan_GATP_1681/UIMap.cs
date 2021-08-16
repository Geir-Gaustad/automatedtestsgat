namespace _010_Test_Delte_Oppgaver_Arbeidsplan_GATP_1681
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

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        #endregion

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }

        #region Common Methods
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
        private void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepDelteOppgaver1, null, "ROVE", logGatInfo, false, false);
        }

        private void ChangeDepartment(string department)
        {
            UICommon.ChangeDepartment(department);
        }
        private void ChangeDepartmentFromRosterplan(string dep, bool goToRoster)
        {
            if (goToRoster)
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);

            UICommon.ChangeDepartmentFromRosterplan(dep, null, false, true);
        }
        public void AddRuleToRoleKardEkskl()
        {
            Playback.Wait(1500);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Department);
            UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Døgnrytmeplan, false);
            UICommon.UIMapVS2017.SelectDayRythmPlan("2024");
            UICommon.UIMapVS2017.ClickEditDayRythmLayer();
            SelectMinRuleKardEkskl();

            UICommon.UIMapVS2017.ClickOKDayRythmplanLayer();
            UICommon.UIMapVS2017.ClickOKDayRythmplan();
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

        private void GoToShiftDateNew(DateTime date)
        {
            UICommon.GoToShiftbookdate(date);
        }
        private void SelectDepartmentTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Department);
        }
        private void OpenRosterPlan(string planName)
        {
            Playback.Wait(1500);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            UICommon.SelectRosterPlan(planName);
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
        #endregion

        public void Step_1()
        {
            StartGat(true);
        }
        public List<string> Step_2()
        {
            var errorList = new List<string>();

            SelectDepartmentTab();
            UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Oppgavedeling, false);
            UICommon.UIMapVS2017.ClickNewRoleSharingInRoleSharingTab();
            UICommon.UIMapVS2017.OpenRoleSharingRegStatus();

            try
            {
                CheckErrorMessagesStep2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_3()
        {
            var errorList = new List<string>();

            Playback.Wait(2000);
            UICommon.UIMapVS2017.ClickAddRolesForSharing();

            try
            {
                CheckValuesStep3And4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3(List is not empty): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_4()
        {
            var errorList = new List<string>();

            SearchOVER4600inRolesForSharing();

            try
            {
                CheckValuesStep3And4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4(List is not empty): " + e.Message);
            }

            return errorList;
        }
        public void Step_5()
        {
            UICommon.UIMapVS2017.ClickCancelInAddRolesForSharingWindow();
            UICommon.UIMapVS2017.ClickCancelRoleSharingInRoleSharingTab();
        }
        public void Step_6()
        {
            ChangeDepartmentFromRosterplan("4720", true);
            OpenRosterPlan("4720 Januar 2024");

            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickRosterplanRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();
        }
        public List<string> Step_7()
        {
            var errorList = new List<string>();

            TryAddRoleToMonkStep_7();
            UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();
            SelectMonkRoleStep_7();

            try
            {
                CheckErrorMessageStep7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_8()
        {
            var errorList = new List<string>();

            DeleteMonkRoleStep_8();
            AddRoleToMonkStep_8();
            SelectMonkRoleStep_8();

            try
            {
                CheckRoleInfoStep8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8(Role info): " + e.Message);
            }
            try
            {
                CheckSumStep8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8(Sum): " + e.Message);
            }
            try
            {
                CheckNoErrorsInSpecTab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8(Spec): " + e.Message);
            }

            return errorList;
        }
        private void CheckNoErrorsInSpecTab()
        {
            var listCust = UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer3DockPanel.UIDSpecificationsDockPanel.UIControlContainer2Custom.UISpecificationPanelViCustom.UIFlowControlViewHost1Custom.UISpecificationSectionCustom.UIVhDetailsCustom.UISpecificationListVieCustom;
            var cust = listCust.UIFcListCustom.UISpecificationItemVieCustom;
            var errorList = new List<string>();

            if (cust.Exists)
            {
                throw new Exception("Errormessages are showing!");
            }
        }

        public void Step_9()
        {
            UICommon.UIMapVS2017.ClickSaveInWorkbookRoleAssignment();
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            CloseRosterPlan();
        }
        public List<string> Step_10()
        {
            var errorList = new List<string>();

            ChangeDepartmentFromRosterplan("4810", false);
            OpenRosterPlan("4810 Januar 2024");

            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickRosterplanRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentEmployeeView();

            AddRoleToEllingsenStep_10();
            SelectEllingsenRoleStep_10();

            try
            {
                CheckRoleInfoStep10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10(Role info): " + e.Message);
            }
            try
            {
                CheckSumStep10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10(Sum): " + e.Message);
            }
            try
            {
                CheckNoErrorsInSpecTab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10(Spec): " + e.Message);
            }

            return errorList;
        }
        public void Step_11()
        {
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            UICommon.UIMapVS2017.ClickYesInWorkbookRoleAssignmentSaveDialog();
            CloseRosterPlan();
        }
        public List<string> Step_12()
        {
            var errorList = new List<string>();

            ChangeDepartmentFromRosterplan("4710", false);
            AddRuleToRoleKardEkskl();

            OpenRosterPlan("4710 Januar 2024");

            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickRosterplanRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();

            AddRoleToJensenStep_12();

            try
            {
                try
                {
                    CheckRoleStep12_2();
                }
                catch
                {
                    CheckRoleStep12_2_Selected();
                }              
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(Role not added by Doubleclick)");
                DragRoleToJensenStep_12();
                TestContext.WriteLine("Role added to Jensen by DragDrop");
            }

            try
            {
                SelectJensenRoleStep_12();
                CheckRoleStep12_2_Selected();
                CheckRoleInfoStep12();              
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(Role info): " + e.Message);
            }

            try
            {
                CheckSumStep12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(Sum): " + e.Message);
            }
            try
            {
                CheckNoErrorsInSpecTab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(Spec): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_13()
        {
            var errorList = new List<string>();

            AddRoleToJensenStep_13();
            SelectJensenRoleStep_13();

            try
            {
                CheckRoleInfoStep13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13(Role info): " + e.Message);
            }
            try
            {
                CheckSumStep13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13(Sum): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_14()
        {
            var errorList = new List<string>();

            AddRoleToJensenStep_14();
            UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();
            SelectJensenRoleStep_14();

            try
            {
                CheckRoleInfoStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14(Role info): " + e.Message);
            }
            try
            {
                CheckSumStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13(Sum): " + e.Message);
            }
            try
            {
                CheckErrorsInStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14(Error message): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_15()
        {
            var errorList = new List<string>();

            DeleteJensenRoleStep_15();
            AddRoleToJensenStep_15();
            SelectJensenRoleStep_15();

            try
            {
                CheckRoleInfoStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15(Role info): " + e.Message);
            }
            try
            {
                CheckSumStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15(Sum): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_16()
        {
            var errorList = new List<string>();

            AddRoleToJensenStep_16();
            SelectJensenRoleStep_16();

            try
            {
                CheckRoleInfoStep16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 16(Role info): " + e.Message);
            }
            try
            {
                CheckSumStep16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15(Sum): " + e.Message);
            }
            try
            {
                CheckErrorsInStep16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 16(warnings): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_17()
        {
            var errorList = new List<string>();

            AddRoleToJensenStep_17();
            SelectJensenRoleStep_17();

            try
            {
                CheckRoleInfoStep17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 17(Role info): " + e.Message);
            }
            try
            {
                CheckSumStep17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 17(Sum): " + e.Message);
            }

            return errorList;
        }
        //public List<string> Step_18()
        //{
        //    //Dette punktet skal mest sannsynlig ikke være med da det er ett duplikat!!
        //    var errorList = new List<string>();

        //    return errorList;
        //}


        public List<string> Step_18()
        {
            var errorList = new List<string>();

            AddRoleToJensenStep_18();
            UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();
            SelectJensenRoleStep_18();

            try
            {
                CheckRoleInfoStep18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 18(Role info): " + e.Message);
            }
            try
            {
                CheckErrorsInStep18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 18(errors): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_19()
        {
            var errorList = new List<string>();

            DeleteJensenRoleStep_19();
            AddRoleToJensenStep_19();
            SelectJensenRoleStep_19();

            try
            {
                CheckRoleInfoStep19();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 19:(Role info) " + e.Message);
            }
            try
            {
                CheckSumStep19();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 19(Sum): " + e.Message);
            }

            return errorList;
        }

        public void Step_20()
        {
            UICommon.UIMapVS2017.ClickSaveInWorkbookRoleAssignment();
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();

            UICommon.EffectuateRoasterplanNextPeriod();
            UICommon.EffectuateRosterplanLines(false);
            UICommon.CloseSalaryCalculationsWindow();
        }

        public void Step_21()
        {
            CloseRosterPlan();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
            GoToShiftDateNew(new DateTime(2024, 01, 01));
            Playback.Wait(2000);
        }

        public List<string> Step_22()
        {
            var errorList = new List<string>();

            try
            {
                CheckShiftBookDateStep_25();
            }
            catch (Exception e)
            {
                errorList.Add("Date error in Step 22: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();
            UICommon.UIMapVS2017.ChangeRoleAssignWeekView("Uke 52 2023", "Uke 6 2024");
            Playback.Wait(1000);

            UICommon.UIMapVS2017.ClickClickUpdateButtonInWorkbookRoleAssignment();

            try
            {
                ChangeViewToShowAllRoles();
                SelectJensenRole1Step_22();
                try
                {
                    CheckJensenRole1Step_22();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 22(31.12.23): " + e.Message);
                }

                try
                {
                    CheckJensenRole2Step_22();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 22(02.01.24): " + e.Message);
                }

                try
                {
                    CheckJensenRole3Step_22();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 22(03.01.24): " + e.Message);
                }

                try
                {
                    CheckJensenRole4Step_22();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 22(05.01.24): " + e.Message);
                }

                try
                {
                    CheckJensenRole5Step_22();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 22(A1 - 07.01.24): " + e.Message);
                }

                try
                {
                    CheckJensenRole6Step_22();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 22(BV1 - 07.01.24): " + e.Message);
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 22: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_23()
        {
            var errorList = new List<string>();

            try
            {
                ChangeViewToShowAllRolesStep23();
                SelectJensenRole1Step_23();
                try
                {
                    CheckJensenRole1Step_23();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 23(31.12.23): " + e.Message);
                }

                try
                {
                    CheckJensenRole2Step_23();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 23(02.01.24): " + e.Message);
                }

                try
                {
                    CheckJensenRole3Step_23();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 23(03.01.24): " + e.Message);
                }

                try
                {
                    CheckJensenRole4Step_23();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 23(05.01.24): " + e.Message);
                }

                try
                {
                    CheckJensenRole5Step_23();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 23(A1 - 07.01.24): " + e.Message);
                }

                try
                {
                    CheckJensenRole6Step_23();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 23(BV1 - 07.01.24): " + e.Message);
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 23: " + e.Message);
            }

            return errorList;
        }

        public void Step_24()
        {
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            OpenRosterPlan("4710 Januar 2024");

            UICommon.ClickRosterplanPlanTab();
            UICommon.DeleteEffectuationRosterplan();
            if (UICommon.SelectAllAndWaitForDeleteEffectuationWindowReady())
            {
                UICommon.DeleteEffectuatedLines();
                UICommon.CloseDeleteEffectuationOkWindow();
            }
        }

        public List<string> Step_25()
        {
            var errorList = new List<string>();
            CloseRosterPlan();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);

            try
            {
                CheckShiftBookDateStep_25();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 25: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();

            return errorList;
        }

        public List<string> Step_26()
        {
            UICommon.UIMapVS2017.ChangeRoleAssignWeekView("Uke 52 2023", "Uke 6 2024");
            Playback.Wait(1000);

            UICommon.UIMapVS2017.ClickClickUpdateButtonInWorkbookRoleAssignment();
            return CheckPlanEmptyStep26(); 
        }

        private List<string> CheckPlanEmptyStep26()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            var roleGrid = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIVhContentCustom.UIPcViewClient.UIRoleFocusedManageVieCustom.UIIgContentCustom.UIDdGridControlCustom.UIGcDragDropGridTable;
            var empGrid = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer1DockPanel.UIDpEmployeesDockPanel.UIControlContainer1Custom.UIEmployeeListViewCustom.UIGcEmployeesTable;
            var dragDrop = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIVhContentCustom.UIPcViewClient.UIRoleFocusedManageVieCustom.UIIgContentCustom.UIDdGridControlCustom.UIGcDragDropGridTable;
            var sumGrid = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpSummaryDockPanel.UIDockPanel1_ContainerCustom.UISumDemandHostViewCustom.UIViewHost1Custom.UIPcViewClient.UIGcSumDemandTable;
            var headerClient = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer3DockPanel.UIDSpecificationsDockPanel.UIControlContainer2Custom.UISpecificationPanelViCustom.UIFlowControlViewHost1Custom.UISpecificationSectionCustom.UIPcHeaderClient;
            #endregion

            if (roleGrid.Views[0].RowCount > 0)
                errorList.Add("Oppgavetabellen er ikke tom!");

            if (empGrid.Views[0].RowCount > 0)
                errorList.Add("Ansattetabellen er ikke tom!");

            if (dragDrop.Views[0].RowCount > 0)
                errorList.Add("Disponibletabellen er ikke tom!");

            if (sumGrid.Views[0].RowCount > 0)
                errorList.Add("Summeringstabellen er ikke tom!");

            if (headerClient.Exists)
                errorList.Add("Spesifikasjonsfeltet er ikke tomt!");

            return errorList;
        }
        public void ShutDown()
        {
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            CloseGat();
        }
    }
}
