namespace _010_Test_Delte_Oppgaver_I_Vaktbok
{
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using System.Drawing;
    using System.Threading;
    using CommonTestData;
    using System.Globalization;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;

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
        private void ChangeDepartment(string department, bool clickDepButton = false)
        {
            Playback.Wait(2000);
            if (!clickDepButton)
                UICommon.UIMapVS2017.ClickChangeDepFromDepTab();

            Playback.Wait(2000);
            UICommon.ChangeDepartment(department, null, false, false, clickDepButton);
        }

        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepKirurgi);
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
            UICommon.GoToShiftbookdate(date);
        }
        #endregion

        public List<string> Step_1()
        {
            var errorList = new List<string>();

            StartGat(true);
            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Department);
            UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Oppgavedeling, false);

            try
            {
                CheckDisabledButtons();
                CheckNoRolesInList();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 1: " + e.Message);
            }

            return errorList;
        }
        private void CheckNoRolesInList()
        {
            #region Variable Declarations
            var roleTable = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIRoleSharingViewCustom.UIPanelControl1Client.UIGridControlRoleTable;
            #endregion

            if (roleTable.Views[0].RowCount > 0)
                throw new Exception("Roletable not empty!");
        }

        public List<string> Step_2()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickNewRoleFromSubTabRoleAssignment();
            UICommon.UIMapVS2017.ClickRegstatusInAssignNewRoleWindow();
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

            UICommon.UIMapVS2017.AddRemoveRoleRoleToAssignment(true);

            try
            {
                CheckRolesInListStep3();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_4()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectRoleToAssign("", "Delt", true);

            try
            {
                CheckRoleDeltTil4610IsInListStep4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_5()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.AddRemoveDepToAssignTo(true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_6()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectDepToAssignTo("4610 + Ortopedi", true);

            try
            {
                CheckDepWindowOpen();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_7()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.AssignNewRoleFromSubTabRoleAssignment(new DateTime(2024, 01, 01), null, 1, "kommentar step 7");
            UICommon.UIMapVS2017.OkCancelAssignNewRole(true);

            try
            {
                ChecRoleDataInListStep7();
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

            ChangeDepartment("4710 - Kardiologi");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            OpenPlan("4710 Januar 2024");
            UICommon.EffectuateFullRosterplan(true);
            UICommon.EffectuateRosterplanLines(false);
            UICommon.CloseSalaryCalculationsWindow();
            CloseRosterPlan();

            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Department);
            UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Oppgavedeling, false);
            DeleteExcessRolesAssignmentsDep4710();

            AssignRoleStep8();

            try
            {
                ChecRoleDataInListStep8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8: " + e.Message);
            }

            return errorList;
        }

        private void AssignRoleStep8()
        {
            UICommon.UIMapVS2017.ClickNewRoleFromSubTabRoleAssignment();
            UICommon.UIMapVS2017.AddRemoveRoleRoleToAssignment(true);
            UICommon.UIMapVS2017.SelectRoleToAssign("", "4620 + kompetanse", true);

            UICommon.UIMapVS2017.AddRemoveDepToAssignTo(true);
            UICommon.UIMapVS2017.SelectDepToAssignTo("4620 + Kirurgi", true);

            UICommon.UIMapVS2017.AssignNewRoleFromSubTabRoleAssignment(new DateTime(2024, 01, 03), new DateTime(2024, 01, 10), 1, "kommentar step 8");
            UICommon.UIMapVS2017.OkCancelAssignNewRole(true);
        }

        public List<string> Step_9()
        {
            var errorList = new List<string>();

            AssignRoleStep9();

            try
            {
                ChecRoleDataInListStep9();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 9: " + e.Message);
            }

            return errorList;
        }

        private void AssignRoleStep9()
        {
            UICommon.UIMapVS2017.ClickNewRoleFromSubTabRoleAssignment();
            UICommon.UIMapVS2017.AddRemoveRoleRoleToAssignment(true);
            UICommon.UIMapVS2017.SelectRoleToAssign("", "4620 + kun + tirsdag", true);

            UICommon.UIMapVS2017.AddRemoveDepToAssignTo(true);
            UICommon.UIMapVS2017.SelectDepToAssignTo("4620 + Kirurgi", true);

            UICommon.UIMapVS2017.AssignNewRoleFromSubTabRoleAssignment(new DateTime(2024, 01, 01), null, 1, "kommentar step 9");
            UICommon.UIMapVS2017.OkCancelAssignNewRole(true);
        }

        public List<string> Step_10()
        {
            var errorList = new List<string>();

            ChangeDepartment("4810 - Geriatri");
            UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Oppgavedeling, false);
            AssignRoleStep10();

            try
            {
                ChecRoleDataInListStep10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10: " + e.Message);
            }

            return errorList;
        }

        private void AssignRoleStep10()
        {
            UICommon.UIMapVS2017.ClickNewRoleFromSubTabRoleAssignment();
            UICommon.UIMapVS2017.AddRemoveRoleRoleToAssignment(true);
            UICommon.UIMapVS2017.SelectRoleToAssign("", "Delt + til 4620 + LES", true);

            UICommon.UIMapVS2017.AddRemoveDepToAssignTo(true);
            UICommon.UIMapVS2017.SelectDepToAssignTo("4620 + Kirurgi", true);

            UICommon.UIMapVS2017.AssignNewRoleFromSubTabRoleAssignment(new DateTime(2024, 01, 01), new DateTime(2099, 12, 31), 0, "kommentar step 10");
            UICommon.UIMapVS2017.OkCancelAssignNewRole(true);
        }

        public List<string> Step_11()
        {
            var errorList = new List<string>();

            ChangeDepartment("4620 - Kirurgi");
            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Shiftbook);
            GoToShiftDateNew(new DateTime(2024, 01, 01));

            OpenAssignToDShiftDahleStep11();

            try
            {
                CheckRolesInListStep11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_12()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickWorkbookRoleView();
            TryAssignRoleByRightClickStep12();

            try
            {
                CheckUnableToAssignRole();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(Right Click): " + e.Message);
            }

            TryAssignRoleFromDispStep12();
            try
            {
                CheckNoRoleAddedStep12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(From Disp): " + e.Message);
            }

            return errorList;
        }
        private void CheckUnableToAssignRole()
        {
            #region Variable Declarations
            var popUp = UIItemWindow1.UIPopupMenuBarControlMenu;
            #endregion

            // Verify that the 'Exists' property of 'Tildeling av oppgave' menu item equals 'False'
            Assert.AreEqual(false, popUp.Exists);
        }


        public List<string> Step_13()
        {
            var errorList = new List<string>();

            AssignRoleFromDispStep13();

            OpenHintStep13();
            try
            {
                CheckHintStep13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13(Hint): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCompTabInRoleAssignmentDetails();
            try
            {
                CheckRoleCompStep13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13(Comp): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_14()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();

            try
            {
                CheckRoleAssignedStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_15()
        {
            var errorList = new List<string>();

            TryAssignRoleFromDispStep15();

            try
            {
                CheckNoRoleAddedStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15(From Disp): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_16()
        {
            var errorList = new List<string>();

            AssignRoleFromDispStep16();

            try
            {
                CheckRoleAssignedStep16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 16: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_17()
        {
            var errorList = new List<string>();

            ChangeDepartment("4710 - Kardiologi", true);
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentEmployeeView();

            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSpecificationTab();
            SelectRoleStep17();

            try
            {
                ChecRoleAndDepDataStep17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 17: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_18()
        {
            var errorList = new List<string>();

            SelectTwoRoleAssignmentsStep18();
            TryToDeleteRoleAssignmentsStep18();

            try
            {
                ChecRoleAssignmentsNotDeletedStep18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 18: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_19()
        {
            var errorList = new List<string>();

            AssignRoleFromDispStep19();

            UICommon.UIMapVS2017.CheckShowCoverageBreaks();
            var str = @"Sum timer oppdekket / sum timer behov (begrenset til behov";
            UICommon.UIMapVS2017.SummarySettingsSelectSumBehovRoleAssignment(str);

            try
            {
                CheckRoleAssignedStep19();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 19(Role): " + e.Message);
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

        public List<string> Step_20()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();
            AssignRoleWithDragStep20();

            try
            {
                CheckRoleAssignedStep20();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 20(Role): " + e.Message);
            }

            try
            {
                SelectRoleStep20();
                CheckWarnigsStep20();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 20(Warnings): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_21()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickSaveInWorkbookRoleAssignment();
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();

            Playback.Wait(2000);
            AssignRoleStep21();
            Playback.Wait(1000);
            OpenWarnigsStep21();

            try
            {
                CheckWarnigsStep21();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 21: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_22()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();
            try
            {
                CheckRoleAssignedStep22();
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

            OpenRoleAssignmentStep23();
            UICommon.UIMapVS2017.ChangePeriodInRoleAssignmentDetailsWindow("", "16:00");

            TryOpenWarnigStep23();

            try
            {
                CheckNoWarnigStep23();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 23: " + e.Message);
            }

            return errorList;
        }
        private void CheckNoWarnigStep23()
        {
            #region Variable Declarations
            WinToolTip uIOppgaveOVER4700falleToolTip = this.UIItemWindow3.UIOppgaveOVER4700falleToolTip;
            #endregion

            Assert.AreEqual(false, UIItemWindow3.Exists);
        }

        public List<string> Step_24()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 24: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_25()
        {
            var errorList = new List<string>();

            ChangeDepartment("4610 - Ortopedi", true);

            UICommon.UIMapVS2017.SelectRoleAssigmentDashboardWhenUnpinned();
            UICommon.UIMapVS2017.PinRoleAssigmentDashboard();
            UICommon.UIMapVS2017.ClickWorkbookPlanner();

            AssignRoleStep25();

            try
            {
                CheckRoleAssignedStep25();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 25: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_26()
        {
            var errorList = new List<string>();

            TryAssignRoleStep26();

            try
            {
                CheckInfoStep26();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 26(Utføres på avdeling 4620 - Kirurgi): " + e.Message);
            }

            OpenHint1Step26();
            try
            {
                CheckWarningsStep26_1();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 26(Delt til 4610 Ekskl kan ikke): " + e.Message);
            }

            OpenHint2Step26();
            try
            {
                CheckWarningsStep26_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 26(ORT Ekskl): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_27()
        {
            var errorList = new List<string>();

            SelectAssignedRoleStep27();
            UICommon.UIMapVS2017.ClickRemoveRoleAssignmentInDetailsWindow();

            try
            {
                CheckAssignedRoleDeletedStep27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 27(Role Assignment not deleted): " + e.Message);
            }

            return errorList;
        }

        private void SelectAssignedRoleStep27()
        {
            #region Variable Declarations
            WinCell uIAppointmentCell1 = this.UIOppgavetildelingWindow.UISchedulerControl1List2.UIAppointmentCell1;
            #endregion

            Mouse.Click(uIAppointmentCell1, new Point(17, 12));
        }

        public List<string> Step_28()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();
            OpenRoleAssignedStep28();

            try
            {
                CheckRoleDetailsStep28();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 28(Details): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();

            try
            {
                CheckAvailableRolesStep28();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 28(Available): " + e.Message);
            }

            try
            {
                CheckAvailableLinesStep28();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 28(Available lines): " + e.Message);
            }

            return errorList;
        }
        private void OpenRoleAssignedStep28()
        {
            #region Variable Declarations
            Playback.Wait(2000);
            var contList = UIGatWindow.UIViewHostCustom.UIPcViewClient.UICenterPanelRoleSchedDockPanel.UIControlContainerCustom.UISchedulerControlWindow.UISchedulerControlList;
            var x1 = contList.BoundingRectangle.X;
            var y1 = contList.BoundingRectangle.Y;
            #endregion

            Mouse.DoubleClick(new Point(x1 + 100, y1 + 200));
        }
        private void CheckRoleAssignedStep28()
        {
            #region Variable Declarations
            Playback.Wait(1000);
            var contList = UIGatWindow.UIViewHostCustom.UIPcViewClient.UICenterPanelRoleSchedDockPanel.UIControlContainerCustom.UISchedulerControlWindow.UISchedulerControlList;
            WinCell uIAppointmentCell2 = contList.UIAppointmentCell2;
            #endregion

            // Verify that the 'Name' property of 'Appointment' cell contains '8:00 a.m.  to 4:00 p.m.  mandag, januar 1, 2024, Subject , Time Ledig, 1 of 1'
            StringAssert.Contains(uIAppointmentCell2.Name, "8:00 a.m.  to 4:00 p.m.  mandag, januar 1, 2024, Subject , Time Ledig, 1 of 1");
        }

        private void CheckAvailableLinesStep28()
        {
            #region Variable Declarations
            var tblView = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UILeftPanelDockPanel.UIOppgavetildelingDockPanel.UIControlContainerCustom.UIGAvailableRolesTable.Views[0];
            #endregion

            Assert.AreEqual(4, tblView.RowCount);
        }

        public List<string> Step_29()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2024, 01, 08));

            try
            {
                CheckAvailableRolesStep29();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 29(Available): " + e.Message);
            }

            try
            {
                CheckAvailableLinesStep29();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 28(Available lines): " + e.Message);
            }

            return errorList;
        }

        private void CheckAvailableLinesStep29()
        {
            #region Variable Declarations
            var tblView = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UILeftPanelDockPanel.UIOppgavetildelingDockPanel.UIControlContainerCustom.UIGAvailableRolesTable.Views[0];
            #endregion

            Assert.AreEqual(5, tblView.RowCount);
        }

        public List<string> Step_30()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.UnPinRoleAssigmentDashboard();
            UICommon.UIMapVS2017.ClickWorkbookRoleView();
            AssignRoleStep30();

            try
            {
                CheckRoleAssignedStep30();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 30: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_31()
        {
            var errorList = new List<string>();

            ChangeDepartment("4810 - Geriatri", true);
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();

            AssignRoleStep31();

            try
            {
                CheckRoleAssignedStep31();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 31: " + e.Message);
            }

            try
            {
                CheckSumStep31();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 31(Sum): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_32()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickSaveInWorkbookRoleAssignment();
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();

            ChangeDepartment("4710 - Kardiologi", true);
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();

            AssignRoleStep32();
            Playback.Wait(1500);
            OpenHintStep32();

            try
            {
                CheckWarningsStep32();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 32: " + e.Message);
            }

            try
            {
                CheckOkButtonEnabled(true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 32(OkButton): " + e.Message);
            }

            return errorList;
        }
        public void CheckOkButtonEnabled(bool enabled)
        {
            #region Variable Declarations
            var win = UIOppgavetildelingWindow1;
            win.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXButton uIOKButton = win.UIOKButton;
            #endregion

            Assert.AreEqual(enabled, uIOKButton.Enabled);
        }

        public List<string> Step_33()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();
            //UICommon.UIMapVS2017.ClickSaveInWorkbookRoleAssignment();
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            UICommon.UIMapVS2017.ClickWorkbookRoleView();

            AssignRoleStep33();
            OpenHintStep32();

            try
            {
                CheckWarningsStep32();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 33: " + e.Message);
            }

            try
            {
                CheckOkButtonEnabled(false);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 33(OkButton): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_34()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 33(OkButton): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_35()
        {
            var errorList = new List<string>();

            ChangeDepartment("4810 - Geriatri", true);

            Playback.Wait(2000);
            AssignRoleStep35();

            try
            {
                CheckRoleAssignedStep35();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 35: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_36()
        {
            var errorList = new List<string>();

            ChangeDepartment("4620 - Kirurgi", true);
            Playback.Wait(1000);

            try
            {
                CheckRoleAssignedStep36();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 36: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();

            try
            {
                CheckRoleAvailableStep36();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 36: " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            UICommon.UIMapVS2017.ClickWorkbookRoleView();

            return errorList;
        }
        public List<string> Step_37()
        {
            var errorList = new List<string>();

            TryAssignRoleDragFromDispStep37();
            OpenHintStep37();

            try
            {
                CheckRoleErrorsInDetailsWinStep37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(From Disp): " + e.Message);
            }

            try
            {
                CheckOkButtonEnabled(false);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(OkButton): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();
            OpenRolesByRightClickInDispStep37();

            try
            {
                CheckAvailableRolesStep37();
                SubItemCount();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(Right Click in Disp): " + e.Message);
            }

            TryAssignRoleByRightClickStep37();
            OpenHintStep37();

            try
            {
                CheckRoleErrorsInDetailsWinStep37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(Right Click on Role): " + e.Message);
            }

            try
            {
                CheckOkButtonEnabled(false);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(OkButton): " + e.Message);
            }

            return errorList;
        }

        private void SubItemCount()
        {
            #region Variable Declarations
            var menu = UIItemWindow1.UIPopupMenuBarControlMenu.UITildelingavoppgaveMenuItem;
            #endregion

            Assert.AreEqual(3, menu.SubItemCount, "Unexpected number of lines in menu!");
        }

        public List<string> Step_38()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();

            TryAssignRoleFromDispInTaskViewStep38();
            OpenHintStep37();
            try
            {
                CheckRoleErrorsInDetailsWinStep37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 38(TaskView/Disp): " + e.Message);
            }

            try
            {
                CheckOkButtonEnabled(true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(OkButton in TaskView/Disp): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();
            TryAssignRoleFromEmpsInTaskViewStep38();
            OpenHintStep37();

            try
            {
                CheckRoleErrorsInDetailsWinStep37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 38(TaskView/Emps): " + e.Message);
            }

            try
            {
                CheckOkButtonEnabled(true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(OkButton in TaskView/Emps): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();
            UICommon.UIMapVS2017.SelectRoleAssignmentEmployeeView();
            TryAssignRoleFromRolesInEmployeeViewStep38();
            OpenHintStep37();

            try
            {
                CheckRoleErrorsInDetailsWinStep37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 38(EmployeeView): " + e.Message);
            }

            try
            {
                CheckOkButtonEnabled(true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(OkButton in EmployeeView): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();
            TryAssignRoleFromRoleByDragInEmployeeViewStep38();
            OpenHintStep37();

            try
            {
                CheckRoleErrorsInDetailsWinStep37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 38(EmployeeView/Drag): " + e.Message);
            }

            try
            {
                CheckOkButtonEnabled(true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(OkButton in EmployeeView/Drag): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();

            return errorList;
        }

        public List<string> Step_39()
        {
            var roleList = new List<string>
                        { "Delt til 4610 Ekskl kan ikke",
                        "Delt til 4620 Kompetanse bør",
                        "Delt til 4620 Kun tirsdag",
                        "Delt til 4620 LES",
                        "KIR kontering",
                        "OVER 4600"};

            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();

            UICommon.UIMapVS2017.ClickDownArrowOpenRoleFilter();
            UICommon.UIMapVS2017.ClickEditRoleFilter();
            UICommon.UIMapVS2017.SelectAllRolesInEditFilterWindow();

            foreach (var role in roleList)
            {
                if (!CheckRolesInFilterWindowStep39(role))
                    errorList.Add(role + " missing in rolelist Step39");
            }

            UICommon.UIMapVS2017.UnSelectAllRolesInEditFilterWindow();

            return errorList;
        }
        private bool CheckRolesInFilterWindowStep39(string role)
        {
            #region Variable Declarations
            DXCheckedListBox uIClbRowCheckedListBox = this.UIDefinisjonavfilterfoWindow.UIPcRowSelectionClient.UIGcRowSelectorClient.UIClbRowCheckedListBox;
            #endregion

            foreach (var item in uIClbRowCheckedListBox.CheckedItems)
            {
                if (role == item)
                    return true;
            }

            return false;
        }

        public List<string> Step_40()
        {
            {
                var errorList = new List<string>();

                SelectFilterRolesStep40();
                UICommon.UIMapVS2017.ClickSaveAsRoleFilterInEditFilterWindow();
                SetFilterNameTESTStep40();
                UICommon.UIMapVS2017.ClickOkAddRoleFilterInEditFilterWindow();
                UICommon.UIMapVS2017.ClickUseRoleFilterInEditFilterWindow();

                try
                {
                    CheckFilteredRolesStep40();
                    CheckRoleRowsStep40();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 40(RoleView): " + e.Message);
                }

                UICommon.UIMapVS2017.SelectRoleAssignmentEmployeeView();
                try
                {
                    CheckFilteredRolesEmpViewStep40();
                    CheckRoleRowsEmpViewStep40();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 40(EmpView): " + e.Message);
                }

                return errorList;
            }
        }
        private void CheckRoleRowsStep40()
        {
            #region Variable Declarations
            var listView = UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIVhContentCustom.UIPcViewClient.UIRoleFocusedManageVieCustom.UIIgContentCustom.UIDdGridControlCustom.UIGcDragDropGridTable.Views[0];
            #endregion

            Assert.AreEqual(4, listView.RowCount);
        }
        private void CheckRoleRowsEmpViewStep40()
        {
            #region Variable Declarations
            var listView = UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer1DockPanel.UIDpRoleCodesDockPanel.UIDockPanel2_ContainerCustom.UIRoleCodeListViewCustom.UIGcRoleCodesTable.Views[0];
            #endregion

            Assert.AreEqual(4, listView.RowCount);
        }

        public List<string> Step_41()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            UICommon.UIMapVS2017.ClickWorkbookRoleView();

            TestContext.WriteLine("Step 41: Burde TEST være tigjengelig direkte uten å åpne vinduet først?");
            //Todo sjekke at dette ikke er feil. Burde TEST være tigjengelig direkte uten å åpne vinduet først?
            ActivateFilterInShiftBookRoleView();
            //UICommon.UIMapVS2017.ClickUseRoleFilterInShiftBookRoleView();
            UICommon.UIMapVS2017.ClickCloseInRoleFilterInShiftBookRoleView();

            ActivateFilterInDropDownShiftBookRoleView();
            try
            {
                CheckFilterActiveStep41();
                CheckRoleRowsStep41();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 41: " + e.Message);
            }

            return errorList;
        }
        public void CheckRoleRowsStep41()
        {
            #region Variable Declarations
            var listView = UIGatWindow.UIViewHostCustom.UIPcViewClient.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.GetChildren();
            #endregion

            Assert.AreEqual(24, listView.Count);
        }

        public List<string> Step_42()
        {
            var errorList = new List<string>();

            //TODO Slå av filter når dette fungerer!
            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Department);
            UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Oppgavemønster, false);

            try
            {
                UICommon.UIMapVS2017.ClickNewShiftPatternInDepTab();
                CreateShiftPatternStep42();

                UICommon.UIMapVS2017.ClickOkInShiftPatternWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42: " + e.Message);
            }

            return errorList;
        }

        private void CreateShiftPatternStep42()
        {
            #region Variable Declarations
            DXTextEdit uITeNameEdit = this.UIOppgavemønsterWindow.UITeNameEdit;
            DXComboBox uICbeStartDayComboBox = this.UIOppgavemønsterWindow.UITcDetailsCommentTabList.UITpDetailsClient.UICbeStartDayComboBox;
            WinList uISchedulerList1 = this.UIOppgavemønsterWindow1.UIGsPanelControl1Client.UIWeekPlannerCustom.UIPanelControl1Client.UISchedulerCustom.UISchedulerList;
            DXNavBarLink uIDelttil4610EksklkaniNavBarLink = this.UIOppgavemønsterWindow1.UIGsPanelControl1Client.UIWeekPlannerCustom.UIPanelControl1Client.UINbcActivitiesNavBar.UIOppgaverNavBarGroup.UIDelttil4610EksklkaniNavBarLink;
            DXNavBarLink uIOVER4600NavBarLink = this.UIOppgavemønsterWindow1.UIGsPanelControl1Client.UIWeekPlannerCustom.UIPanelControl1Client.UINbcActivitiesNavBar.UIOppgaverNavBarGroup.UIOVER4600NavBarLink;
            DXNavBarGroup uIDelteoppgaverNavBarGroup = this.UIOppgavemønsterWindow.UIGsPanelControl1Client.UIWeekPlannerCustom.UIPanelControl1Client.UINbcActivitiesNavBar.UIDelteoppgaverNavBarGroup;
            WinList uISchedulerList = this.UIOppgavemønsterWindow1.UISchedulerWindow.UISchedulerList;
            DXNavBarLink uIDelttil4620KompetansNavBarLink = this.UIOppgavemønsterWindow.UIGsPanelControl1Client.UIWeekPlannerCustom.UIPanelControl1Client.UINbcActivitiesNavBar.UIDelteoppgaverNavBarGroup.UIDelttil4620KompetansNavBarLink;
            #endregion

            // Type 'TEST' in 'teName' text box
            uITeNameEdit.ValueAsString = "TEST";

            // Type '{Tab}' in 'teName' text box
            Keyboard.SendKeys(uITeNameEdit, "{TAB}");
            uICbeStartDayComboBox.ValueAsString = "mandag [SelectionStart]0";

            // Double-Click 'Delt til 4610 Ekskl kan ikke' NavBarLink
            Mouse.DoubleClick(uIDelttil4610EksklkaniNavBarLink);

            Keyboard.SendKeys(uISchedulerList1, "{RIGHT}");
            // Double-Click 'OVER 4600' NavBarLink
            Mouse.DoubleClick(uIOVER4600NavBarLink);

            // Click 'Delte oppgaver' NavBarGroup
            Mouse.Click(uIDelteoppgaverNavBarGroup, new Point(62, 10));

            // Type '{Right}' in 'scheduler' list box
            Keyboard.SendKeys(uISchedulerList, "{RIGHT}");

            // Double-Click 'Delt til 4620 Kompetanse bør' NavBarLink
            Mouse.DoubleClick(uIDelttil4620KompetansNavBarLink);
        }

        public List<string> Step_43()
        {
            var errorList = new List<string>();

            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Shiftbook);
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentEmployeeView();

            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListShiftPatternTab();
            AssignRolePatternStep43();

            try
            {
                CheckRolePatternAssignedStep43();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 43: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_44()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.CloseRoleAssignmentRolePattern();
            try
            {
                CheckRolePatternAssignedStep44();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 44: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_45()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
                UICommon.UIMapVS2017.ClickYesInWorkbookRoleAssignmentSaveDialog();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 45: " + e.Message);
            }

            return errorList;
        }

    }
}

