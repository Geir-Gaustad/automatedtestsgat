namespace _010_Test_Vaktbok_Oppgavetildelingsvindu_GATP_1682
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
    using System.Threading;
    using CommonTestData;
    using System.Globalization;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;

    public partial class UIMap
    {
        //public void TestTabs()
        //{
        //    UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentHomeTab();
        //    UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentViewTab();
        //    UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentFilterTab();

        //    UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();
        //    UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();
        //    UICommon.UIMapVS2017.ClickAddRoleAssignmentInDetailsWindow();
        //    UICommon.UIMapVS2017.ClickRemoveRoleAssignmentInDetailsWindow();

        //    UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListRolesTab();
        //    UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListShiftPatternTab();
        //    UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListEmployeesTab();
        //    UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSpecificationTab();
        //    UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationAllocationTab();
        //    UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSearchReplaceTab();

        //    UICommon.UIMapVS2017.ClickUndoRegretInWorkbookRoleAssignment();
        //    UICommon.UIMapVS2017.ClickRestetLayoutInWorkbookRoleAssignmentView();
        //    UICommon.UIMapVS2017.ClickRegretInWorkbookRoleAssignment();
        //    UICommon.UIMapVS2017.ClickSaveInWorkbookRoleAssignment();
        //    UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();

        //    UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();
        //    UICommon.UIMapVS2017.ClickUseFilterInRoleFilterWindow();
        //    UICommon.UIMapVS2017.ClickCloseFilterInRoleFilterWindow();
        //    UICommon.UIMapVS2017.ClickUseEmpFilterInRoleFilterWindow();
        //    UICommon.UIMapVS2017.ClickCloseInEmpFilterInRoleFilterWindow();
        //}


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

        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepOperasjon, null, "", logGatInfo);
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
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
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

        public void Step_1()
        {
            StartGat(true);
        }
        public List<string> Step_2()
        {
            var errorList = new List<string>();
            GoToShiftDateNew(new DateTime(2024, 01, 01));
            try
            {
                CheckShiftbookDateStep_2();
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
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentEmployeeView();
            try
            {
                CheckStep_3();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }
            try
            {
                CheckStep_3_2();
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

            AddRoleToAhlgrenStep_4();
            try
            {
                CheckStep_4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4 " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_5()
        {
            var errorList = new List<string>();
            DoubleClickRoleStep_5();
            try
            {
                CheckStep_5();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }

            return errorList;
        }
        private void CheckStep_5()
        {
            #region Variable Declarations 
            UIOppgavetildelingWindow11.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXTestControl uIOppgavetildelingWindowTitleBar = this.UIOppgavetildelingWindow11.UIOppgavetildelingWindowTitleBar;
            #endregion

            // Verify that the 'FriendlyName' property of 'Oppgavetildeling' WindowTitleBar equals 'Oppgavetildeling'
            Assert.AreEqual("Oppgavetildeling", uIOppgavetildelingWindowTitleBar.FriendlyName);
        }
        public List<string> Step_6()
        {
            var errorList = new List<string>();
            ClickCompTab();
            try
            {
                CheckStep_6();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }

            return errorList;
        }
        private void ClickCompTab()
        {
            #region Variable Declarations
            UIOppgavetildelingWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXTestControl uIXtraTabControlHeaderTabPage = UIOppgavetildelingWindow.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIXtraTabControlHeaderTabPage;
            #endregion

            // Click 'XtraTabControlHeader' tab
            Mouse.Click(uIXtraTabControlHeaderTabPage);
        }
        public List<string> Step_7()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickAddRoleAssignmentInDetailsWindow();
            AddRoleToAhlgrenStep_7();
            Playback.Wait(1000);
            MouseHoover(1);
            try
            {
                CheckStep_7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }
            MouseHoover(2);
            try
            {
                CheckStep_7_1();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            return errorList;
        }
        public void MouseHoover(int ctrl)
        {
            #region Variable Declarations
            WinCell appointment = this.UIOppgavetildelingWindow2.UISchedulerControl1List.UIAppointmentCell;
            if (ctrl == 1)
            {
                appointment.SearchProperties.Add(new PropertyExpression(WinList.PropertyNames.Name, "8:00 a.m.  to 4:00 p.m.  mandag, januar 1, 2024, Subject KOMP KREVD/EKSKL KAN IKKE, Time Ledig, 2 of 3", PropertyExpressionOperator.Contains));
                //appointment.SearchProperties[WinList.PropertyNames.Name] = "Dagvisning, 3 total events 8:00 a.m.  to 4:00 p.m.  mandag, januar 1, 2024, Subject KOMP KREVD/EKSKL KAN IKKE, Time Ledig, 2 of 3";
            }
            else if (ctrl == 2)
            {
                appointment.SearchProperties.Add(new PropertyExpression(WinList.PropertyNames.Name, "8:00 a.m.  to 4:00 p.m.  mandag, januar 1, 2024, Subject 08-16, Time Ledig, 3 of 3", PropertyExpressionOperator.Contains));
                //appointment.SearchProperties[WinList.PropertyNames.Name] = "Dagvisning, 3 total events 8:00 a.m.  to 4:00 p.m.  mandag, januar 1, 2024, Subject 08-16, Time Ledig, 3 of 3";
            }
            #endregion

            Mouse.Hover(appointment, new Point(10, 10));
        }
        public List<string> Step_8()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();
            SelectRoleStep_8();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSpecificationTab();
            try
            {
                CheckStep_8();
                CheckStep_8_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_9()
        {
            var errorList = new List<string>();
            try
            {
                CheckSaveDeactivatedStep_9();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 9: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_10()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickCloseWorkbookRoleAssignment();
            UICommon.UIMapVS2017.ClickYesInWorkbookRoleAssignmentSaveDialog();
            try
            {
                CheckStep_10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_11()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickOkInWorkbookRoleAssignmentErrorMessageDialog();
            SelectRoleStep_11();
            UICommon.UIMapVS2017.ClickDeleteeWorkbookRoleAssignment();
            try
            {
                CheckStep_11_RoleDeleted();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }
            try
            {
                CheckStep_11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }

            return errorList;
        }
        private void CheckStep_11_RoleDeleted()
        {
            #region Variable Declarations
            DXTestControl uILblContentLabel = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer3DockPanel.UIDSpecificationsDockPanel.UIControlContainer2Custom.UISpecificationPanelViCustom.UIFlowControlViewHost1Custom.UISpecificationSectionCustom.UIPcHeaderClient.UILblContentLabel;
            #endregion

            // Verify that the 'Text' property of 'lblContent' label equals 'KOMP KREVD/EKSKL KAN IKKE - Kompetanse påkrevd + Eksklusiv kan ikke'
            Assert.AreEqual("Kompetanser", uILblContentLabel.Text);
        }
        public List<string> Step_12()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickRegretInWorkbookRoleAssignment();
            try
            {
                CheckStep_12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_13()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickUndoRegretInWorkbookRoleAssignment();
            SelectRoleStep_13();

            try
            {
                CheckStep_13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_14()
        {
            var errorList = new List<string>();
            try
            {
                SummarySettingsStep_14();
                var str = @"Sum timer oppdekket / sum timer behov (begrenset til behov";
                UICommon.UIMapVS2017.SummarySettingsSelectSumBehovRoleAssignment(str);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14: " + e.Message);
            }

            Playback.Wait(1000);
            try
            {
                CheckStep_14();
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
            TryToAddRoleStep_15();
            try
            {
                CheckStep_15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_16()
        {
            var errorList = new List<string>();

            AddRoleToDroppStep_16();
            try
            {
                CheckStep_16();
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
            try
            {
                //Får ikke sjekket del som er dekket uten krav(visualisering)
                CheckStep_17();
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
            EditRolePeriodStep_18();
            UICommon.UIMapVS2017.ChangePeriodInRoleAssignmentDetailsWindow("10:00", "12:00");
            try
            {
                CheckStep_18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 18: " + e.Message);
            }

            return errorList;
        }
        private void CheckStep_18()
        {
            #region Variable Declarations
            var win = UIOppgavetildelingWindow1;
            win.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            WinCell uIAppointmentCell = win.UISchedulerControl1List.UIAppointmentCell;
            uIAppointmentCell.SearchProperties.Add(new PropertyExpression(WinList.PropertyNames.Name, "10:00 a.m.  to 12:00 p.m.  onsdag, januar 3, 2024, Subject OVER, Time Ledig, 2 of 2", PropertyExpressionOperator.Contains));
            #endregion

            Mouse.Click(uIAppointmentCell);
        }
        public List<string> Step_19()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickAddRoleAssignmentInDetailsWindow();
            AddRoleToDroppStep_19();
            SelectRoleStep_19();
            Playback.Wait(1000);
            var currentPoint = new Point(Mouse.Location.X, Mouse.Location.Y);
            var newPoint = new Point(Mouse.Location.X + 200, Mouse.Location.Y - 100);
            Mouse.Move(newPoint);
            Mouse.Move(currentPoint);
            Playback.Wait(1000);
            Mouse.Hover(currentPoint);
            try
            {
                CheckRoleWarningStep_19();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 19: " + e.Message);
            }

            ClickCompTab();
            Playback.Wait(1000);
            try
            {
                CheckStep_19();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 19: " + e.Message);
            }

            return errorList;
        }
        private void CheckStep_19()
        {
            #region Variable Declarations
            DXGrid uIGcCompetenceComparisTable = this.UIOppgavetildelingWindow10.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIViewTabPageClient.UIHostedCompetenceCompCustom.UIVhCompetenceCustom.UIPcViewClient.UICompetenceComparisonCustom.UIGcCompetenceComparisTable;
            #endregion

            var view = uIGcCompetenceComparisTable.Views[0];
            string checkVal = view.GetCellValue("colCompetenceName", 0).ToString();

            Assert.AreEqual("STLEG - ST-Lege", checkVal);
        }
        public List<string> Step_20()
        {
            var errorList = new List<string>();
            EditRolePeriodWithDragStep_20();

            try
            {
                CheckStep_20();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 20: " + e.Message);
            }

            return errorList;
        }
        private void CheckStep_20()
        {
            #region Variable Declarations
            WinCell appointment = this.UIOppgavetildelingWindow2.UISchedulerControl1List3.UIAppointmentCell;
            #endregion

            appointment.DrawHighlight();
            // Verify that the 'Name' property of 'Appointment' cell equals '12:00 p.m.  to 4:00 p.m.  onsdag, januar 3, 2024, Subject KOMP ØNSKET/EKSKL BØR IKKE, Time Ledig, 3 of 3'
            Assert.AreEqual("12:00 p.m.  to 4:00 p.m.  onsdag, januar 3, 2024, Subject KOMP ØNSKET/EKSKL BØR IKKE, Time Ledig, 3 of 3", appointment.Name);
        }
        public List<string> Step_21()
        {
            var errorList = new List<string>();
            try
            {
                AddKontStep_21();
                UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 21 : " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_22()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickDetaildAssignmentInWorkbookRoleAssignment();
            AddRoleToDroppStep_22();
            try
            {
                CheckStep_22();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 22 : " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_23()
        {
            var errorList = new List<string>();
            SelectAssignmentStep_23();
            try
            {
                CheckStep_23();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 23 : " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_24()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();
            try
            {
                CheckStep_24();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 24 : " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_25()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListShiftPatternTab();
            ClickCellStep_25();
            AddRolePatternToAnderson();
            try
            {
                CheckStep_25();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 25 : " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_26()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.CloseRoleAssignmentRolePattern(false);
            Playback.Wait(1000);
            try
            {
                CheckUpdateButton(false);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 26 : " + e.Message);
            }
            UICommon.UIMapVS2017.ClickSaveInWorkbookRoleAssignment();
            Playback.Wait(1000);
            UICommon.UIMapVS2017.ChangeRoleAssignWeekView("", "224");
       
            try
            {
                CheckUpdateButton(true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 26 : " + e.Message);
            }
            UICommon.UIMapVS2017.ClickClickUpdateButtonInWorkbookRoleAssignment();

            return errorList;
        }
        private void CheckUpdateButton(bool enabled)
        {
            #region Variable Declarations
            DXRibbonButtonItem uIOppdaterRibbonBaseButtonItem = this.UIOppgavetildelingWindow.UIRcMenuRibbon.UIRpgHomeRibbonPage.UIRpNavigationRibbonPageGroup.UIOppdaterRibbonBaseButtonItem;
            #endregion

            // Verify that the 'Enabled' property of 'Oppdater' RibbonBaseButtonItem equals 'False'
            Assert.AreEqual(enabled, uIOppdaterRibbonBaseButtonItem.Enabled);
        }
        public List<string> Step_27()
        {
            var errorList = new List<string>();
            DecreaseEmpCol();
            AddRolePatternToAndersonStep27();
            try
            {
                CheckStep_27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 27: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_28()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.CreateRoleAssignmentRolePattern(new DateTime(2024, 01, 01), new DateTime(2024, 01, 14));
            try
            {
                CheckStep_28();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 28: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_29()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.CloseRoleAssignmentRolePattern();
            try
            {

                CheckStep_29();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 29: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_30()
        {
            var errorList = new List<string>();
            try
            {
                UICommon.UIMapVS2017.ClickSaveInWorkbookRoleAssignment();
                UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
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
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();
            try
            {
                CheckStep_31();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 31: " + e.Message);
            }

            return errorList;
        }
        private void CheckStep_31()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIAnsattvisningRibbonBaseButtonItem = this.UIOppgavetildelingWindow.UIRcMenuRibbon.UIRpgHomeRibbonPage.UIRpgViewRibbonPageGroup.UIAnsattvisningRibbonBaseButtonItem;
            #endregion

            // Verify that the 'Checked' property of 'Ansattvisning' RibbonBaseButtonItem equals 'True'
            Assert.AreEqual(this.CheckStep_3ExpectedValues.UIAnsattvisningRibbonBaseButtonItemChecked, uIAnsattvisningRibbonBaseButtonItem.Checked, "Employee view not selected");
        }
        public List<string> Step_32()
        {
            var errorList = new List<string>();

            AddRolePatternToAhlgren();
            try
            {
                CheckStep_32();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 32: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_33()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.CloseRoleAssignmentRolePattern();
            try
            {
                CheckPatternStep33();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 33: " + e.Message);
            }
            //MoveViewToShowSunday();

            return errorList;
        }
        public List<string> Step_34()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();
            UICommon.UIMapVS2017.ClickDetaildAssignmentInWorkbookRoleAssignment();
            ResizeDisp();

            try
            {
                CheckStep_34();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 34: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_35()
        {
            var errorList = new List<string>();
            DragDroppToRoleStep_35();
            try
            {
                CheckStep_35();
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

            DragDroppToRoleStep_36();
            try
            {
                CheckStep_36();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 36: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_37()
        {
            var errorList = new List<string>();

            OpenFridayRoleStep_37();
            try
            {
                EditKompRoleStep_37_1();
                EditKompRoleStep_37_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error (EditKompRole) in Step 37: " + e.Message);
            }

            try
            {
                CheckStep_37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37: " + e.Message);
            }

            SelectKontAssignmentStep_37();
            try
            {
                CheckStep_37_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37: " + e.Message);
            }
            return errorList;
        }
        public void CheckStep_37_2()
        {
            #region Variable Declarations
            var win = UIOppgavetildelingWindow1;
            win.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXLookUpEdit uILeCostPlaceLookUpEdit = win.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIViewTabPageClient.UIRoleAccountingViewCustom.UILeCostPlaceLookUpEdit;
            DXLookUpEdit uILeProjectLookUpEdit = win.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIViewTabPageClient.UIRoleAccountingViewCustom.UILeProjectLookUpEdit;
            #endregion

            // Verify that the 'Text' property of 'leCostPlace' LookUpEdit equals '4500 - OPPGAVER'
            Assert.AreEqual("4500 - OPPGAVER", uILeCostPlaceLookUpEdit.Text);

            // Verify that the 'Text' property of 'leProject' LookUpEdit equals 'P2 - Prosjekt 2'
            Assert.AreEqual("P2 - Prosjekt 2", uILeProjectLookUpEdit.Text);
        }
        public List<string> Step_38()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();
            SelectDroppInEmpListStep_38();
            try
            {
                CheckStep_38();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 38: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_39()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickShowShiftsInRoleAssignmentTaskView();
            UICommon.UIMapVS2017.ClickShowTimePeriodsInRoleAssignmentTaskView();

            SelectCockerStep39();
            try
            {
                CheckStep_39();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 39: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_40()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickShowShiftsInRoleAssignmentTaskView();
            UICommon.UIMapVS2017.ClickShowTimePeriodsInRoleAssignmentTaskView();
            try
            {
                CheckStep_40();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 40: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_41()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationAllocationTab();
            Playback.Wait(1000);
            SelectCockerAndRoleStep41();
            try
            {
                CheckStep_41();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 41: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_42()
        {
            var errorList = new List<string>();
            AssignRoleToCockerStep_42();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSpecificationTab();
            SelectRoleStep_42();
            try
            {
                CheckStep_42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_43_44()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXButton uIEditorButton0Button = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer3DockPanel.UIDpSearchAndReplaceDockPanel.UIControlContainer3Custom.UISearchToolViewCustom.UIDdlAssignedRolesLookUpEdit.UIEditorButton0Button;
            DXTextEdit uITeFindEdit = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer3DockPanel.UIDpSearchAndReplaceDockPanel.UIControlContainer3Custom.UISearchToolViewCustom.UIDdlAssignedRolesLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            DXCell uIKOMPØNSKETEKSKLBØRIKCell = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer3DockPanel.UIDpSearchAndReplaceDockPanel.UIControlContainer3Custom.UISearchToolViewCustom.UIDdlAssignedRolesLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILciGridLayoutControlItem.UIGridControlTable.UIKOMPØNSKETEKSKLBØRIKCell;
            DXButton uISøkButton = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer3DockPanel.UIDpSearchAndReplaceDockPanel.UIControlContainer3Custom.UISearchToolViewCustom.UISøkButton;
            #endregion

            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListRolesTab();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSearchTab();

            try
            {
                Mouse.Click(uIEditorButton0Button);
                Keyboard.SendKeys(uITeFindEdit, "ØNSKET");
                Mouse.DoubleClick(uIKOMPØNSKETEKSKLBØRIKCell);
                Mouse.Click(uISøkButton);
                CheckStep_43();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 43(Search roles): " + e.Message);
            }

            try
            {
                SelectRoleToReplaceWithStep43();
                UICommon.UIMapVS2017.ClickReplaceInWorkbookRoleAssignment();
                Playback.Wait(1500);
                Keyboard.SendKeys("{Enter}");

                CheckStep_44();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 44(Replace roles): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_45()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentViewTab();

            UICommon.UIMapVS2017.ClickEmployeesInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(false, "emp"));
            UICommon.UIMapVS2017.ClickEmployeesInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(true, "emp"));

            UICommon.UIMapVS2017.ClickRoleInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(false, "role"));
            UICommon.UIMapVS2017.ClickRoleInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(true, "role"));

            UICommon.UIMapVS2017.ClickRolePatternInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(false, "pattern"));
            UICommon.UIMapVS2017.ClickRolePatternInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(true, "pattern"));

            UICommon.UIMapVS2017.ClickSummaryInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(false, "sum"));
            UICommon.UIMapVS2017.ClickSummaryInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(true, "sum"));

            UICommon.UIMapVS2017.ClickSearchReplaceInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(false, "search"));
            UICommon.UIMapVS2017.ClickSearchReplaceInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(true, "seach"));

            UICommon.UIMapVS2017.ClickSpecificationInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(false, "spec"));
            UICommon.UIMapVS2017.ClickSpecificationInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(true, "spec"));

            UICommon.UIMapVS2017.ClickAssignmentInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(false, "assign"));
            UICommon.UIMapVS2017.ClickAssignmentInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(true, "assign"));

            UICommon.UIMapVS2017.ClickDispInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(false, "disp"));
            UICommon.UIMapVS2017.ClickDispInWorkbookRoleAssignmentView();
            errorList.AddRange(CheckStep_45(true, "disp"));

            return errorList;
        }

        private List<string> CheckStep_45(bool visible, string contr)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXButton uIAnsatteButton = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer1DockPanel.UIAnsatteButton;
            uIAnsatteButton.SearchProperties[DXTestControl.PropertyNames.Name] = "Ansatte";
            DXButton uIOppgaverButton = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer1DockPanel.UIOppgaverButton;
            uIOppgaverButton.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgaver";
            DXButton uIOppgavemønsterButton = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer1DockPanel.UIOppgavemønsterButton;
            uIOppgavemønsterButton.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavemønster";
            DXDockPanel uIDpSummaryDockPanel = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpSummaryDockPanel;
            uIDpSummaryDockPanel.SearchProperties[DXTestControl.PropertyNames.Name] = "dpSummary";
            DXButton uISøkerstattButton = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer3DockPanel.UISøkerstattButton;
            uISøkerstattButton.SearchProperties[DXTestControl.PropertyNames.Name] = "Søk & erstatt";
            DXButton uISpesifikasjonButton = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer3DockPanel.UISpesifikasjonButton;
            uISpesifikasjonButton.SearchProperties[DXTestControl.PropertyNames.Name] = "Spesifikasjon";
            DXButton uITildelingButton = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer3DockPanel.UITildelingButton;
            uITildelingButton.SearchProperties[DXTestControl.PropertyNames.Name] = "Tildeling";
            DXDockPanel uIDpDisponibleDockPanel = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpDisponibleDockPanel;
            uIDpDisponibleDockPanel.SearchProperties[DXTestControl.PropertyNames.Name] = "dpDisponible";
            #endregion

            try
            {
                if (contr == "emp")
                    Assert.AreEqual(visible, uIAnsatteButton.Exists);
                if (contr == "role")
                    Assert.AreEqual(visible, uIOppgaverButton.Exists);
                if (contr == "pattern")
                    Assert.AreEqual(visible, uIOppgavemønsterButton.Exists);
                if (contr == "sum")
                    Assert.AreEqual(visible, uIDpSummaryDockPanel.Exists);
                if (contr == "search")
                    Assert.AreEqual(visible, uISøkerstattButton.Exists);
                if (contr == "spec")
                    Assert.AreEqual(visible, uISpesifikasjonButton.Exists);
                if (contr == "assign")
                    Assert.AreEqual(visible, uITildelingButton.Exists);
                if (contr == "disp")
                    Assert.AreEqual(visible, uIDpDisponibleDockPanel.Exists);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 45(Control does not exist): " + e.Message);
            }


            if (visible)
            {
                try
                {
                    if (contr == "emp")
                    {
                        Assert.AreEqual(2, uIAnsatteButton.Index);
                    }
                    if (contr == "role")
                    {
                        Assert.AreEqual(0, uIOppgaverButton.Index);
                    }
                    if (contr == "pattern")
                    {
                        Assert.AreEqual(1, uIOppgavemønsterButton.Index);
                    }
                    if (contr == "sum")
                        CheckSummaryGridLocation();

                    if (contr == "search")
                    {
                        Assert.AreEqual(2, uISøkerstattButton.Index);
                    }
                    if (contr == "spec")
                    {
                        Assert.AreEqual(0, uISpesifikasjonButton.Index);
                    }
                    if (contr == "assign")
                    {
                        Assert.AreEqual(1, uITildelingButton.Index);
                    }

                    if (contr == "disp")
                        CheckDispGridLocationStep45();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 45(Control is at unexpected location): " + e.Message);
                }
            }

            return errorList;
        }

        public void CheckDispGridLocationStep45()
        {
            #region Variable Declarations
            DXDockPanel uIDpDisponibleDockPanel = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpDisponibleDockPanel;
            #endregion

            // Verify that the 'Name' property of 'dpDisponible' DockPanel equals 'dpDisponible'
            Assert.AreEqual(this.CheckDispGridLocationExpectedValues.UIDpDisponibleDockPanelName, uIDpDisponibleDockPanel.Name);

            // Verify that the 'Index' property of 'dpDisponible' DockPanel equals '2'
            Assert.AreEqual(this.CheckDispGridLocationExpectedValues.UIDpDisponibleDockPanelIndex, uIDpDisponibleDockPanel.Index);

            // Verify that the 'Location' property of 'dpDisponible' DockPanel equals '0, 606'
            Assert.AreEqual("0, 425", uIDpDisponibleDockPanel.GetProperty("Location").ToString());
        }

        public List<string> Step_46()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentHomeTab();
                XClickDispAndSummaryGrid();
                UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
                UICommon.UIMapVS2017.ClickYesInWorkbookRoleAssignmentSaveDialog();

                UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 46: " + e.Message);
            }

            errorList.AddRange(CheckStep_46_47(false));

            return errorList;
        }
        public List<string> CheckStep_46_47(bool visible)
        {
            var errorList = new List<string>();
            #region Variable Declarations
            DXDockPanel uIDpSummaryDockPanel = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpSummaryDockPanel;
            uIDpSummaryDockPanel.SearchProperties[DXTestControl.PropertyNames.Name] = "dpSummary";
            DXDockPanel uIDpDisponibleDockPanel = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpDisponibleDockPanel;
            uIDpDisponibleDockPanel.SearchProperties[DXTestControl.PropertyNames.Name] = "dpDisponible";
            #endregion

            if (visible)
            {
                try
                {
                    CheckSummaryGridLocation();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 46_47: " + e.Message);
                }

                try
                {
                    CheckDispGridLocation();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 46_47: " + e.Message);
                }
            }
            else
            {
                try
                {
                    Assert.AreEqual(visible, uIDpSummaryDockPanel.Exists);
                    Assert.AreEqual(visible, uIDpDisponibleDockPanel.Exists);
                }
                catch (Exception e)
                {
                    errorList.Add("Error in Step 46_47: " + e.Message);
                }
            }

            return errorList;
        }

        public List<string> Step_47()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentViewTab();
                UICommon.UIMapVS2017.ClickRestetLayoutInWorkbookRoleAssignmentView();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 47: " + e.Message);
            }

            errorList.AddRange(CheckStep_46_47(true));
            return errorList;
        }

        public List<string> Step_48()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentHomeTab();
                CreateFilterStep_48();
                Playback.Wait(1500);
                UICommon.UIMapVS2017.ClickUseFilterInRoleFilterWindow();
                Playback.Wait(1000);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 48: " + e.Message);
            }

            try
            {
                CheckStep48();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 48_2: " + e.Message);
            }

            errorList.AddRange(CheckStep48_49());
            return errorList;
        }

        private List<string> CheckStep48_49()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            var tbl1 = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIVhContentCustom.UIPcViewClient.UIRoleFocusedManageVieCustom.UIIgContentCustom.UIDdGridControlCustom.UIGcDragDropGridTable;
            var tbl2 = this.UIOppgavetildelingWindow6.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDockPanelDockPanel.UIDockPanelDockPanel1.UIDpRoleCodesDockPanel.UIDockPanel2_ContainerCustom.UIRoleCodeListViewCustom.UIGcRoleCodesTable;
            #endregion

            var view1 = tbl1.Views[0];
            var view2 = tbl2.Views[0];

            if (view1.RowCount != 3)
            {
                errorList.Add("Feil antall oppgaver(Kalender)");
            }
            if (view2.RowCount != 3)
            {
                errorList.Add("Feil antall oppgaver(Oppgavefane)");
            }

            return errorList;
        }
        public List<string> Step_49()
        {
            var errorList = new List<string>();

            try
            {
                CreateEmpFilterStep_49();
                UICommon.UIMapVS2017.ClickUseEmpFilterInRoleFilterWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 49: " + e.Message);
            }

            try
            {
                CheckStep49();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 49_2: " + e.Message);
            }
            errorList.AddRange(CheckStep48_49());
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListEmployeesTab();
            try
            {
                CheckStep49_1();
                CheckStep49_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 49_2: " + e.Message);
            }

            errorList.AddRange(CheckStep49_3());

            return errorList;
        }

        private void CheckStep49_2()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIPåAvRibbonBaseButtonItem = this.UIOppgavetildelingWindow.UIRcMenuRibbon.UIRpgHomeRibbonPage.UIRpgRoleCodeFilterRibbonPageGroup.UIPåAvRibbonBaseButtonItem;
            DXRibbonButtonItem uIPåAvRibbonBaseButtonItem1 = this.UIOppgavetildelingWindow8.UIRcMenuRibbon.UIRpgHomeRibbonPage.UIRpgEmployeeFilterRibbonPageGroup.UIPåAvRibbonBaseButtonItem;
            DXTestControl uILillaGalleryItem = this.UIOppgavetildelingWindow8.UIRcMenuRibbon.UIRpgHomeRibbonPage.UIRpgRoleCodeFilterRibbonPageGroup.UIRibbonGalleryBarItemRibbonGallery.UISistbrukteGalleryItemGroup.UILillaGalleryItem;
            DXTestControl uICGalleryItem = this.UIOppgavetildelingWindow8.UIRcMenuRibbon.UIRpgHomeRibbonPage.UIRpgEmployeeFilterRibbonPageGroup.UIRibbonGalleryBarItemRibbonGallery.UISistbrukteGalleryItemGroup.UICGalleryItem;
            var roleTbl = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpDisponibleDockPanel.UIControlContainer4Custom.UIDispGridViewCustom.UIIgAvailabilityCustom.UIDdGridControlCustom.UIGcDragDropGridTable;
            DXCell uIGcDragDropGridGridCoCell = roleTbl.UIGcDragDropGridGridCoCell;
            DXCell uICelestonSiggeCell = this.UIOppgavetildelingWindow6.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDockPanelDockPanel.UIDockPanelDockPanel1.UIDpEmployeesDockPanel.UIControlContainer1Custom.UIEmployeeListViewCustom.UIGcEmployeesTable.UICelestonSiggeCell;
            DXCell uICockerJamesCell = this.UIOppgavetildelingWindow6.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDockPanelDockPanel.UIDockPanelDockPanel1.UIDpEmployeesDockPanel.UIControlContainer1Custom.UIEmployeeListViewCustom.UIGcEmployeesTable.UICockerJamesCell;
            #endregion

            // Verify that the 'Checked' property of 'På/Av' RibbonBaseButtonItem equals 'True'
            Assert.AreEqual(this.CheckStep49ExpectedValues.UIPåAvRibbonBaseButtonItemChecked, uIPåAvRibbonBaseButtonItem.Checked);

            // Verify that the 'Checked' property of 'På/Av' RibbonBaseButtonItem equals 'True'
            Assert.AreEqual(this.CheckStep49ExpectedValues.UIPåAvRibbonBaseButtonItemChecked1, uIPåAvRibbonBaseButtonItem1.Checked);

            // Verify that the 'Name' property of 'Lilla' GalleryItem equals 'Lilla'
            Assert.AreEqual(this.CheckStep49ExpectedValues.UILillaGalleryItemName, uILillaGalleryItem.Name);

            // Verify that the 'Name' property of 'C' GalleryItem equals 'C'
            Assert.AreEqual(this.CheckStep49ExpectedValues.UICGalleryItemName, uICGalleryItem.Name);

        }

        private List<string> CheckStep49_3()
        {
            #region Variable Declarations
            var errorList = new List<string>();

            //Sjekker antal linjer i disponible
            var roleTbl = this.UIOppgavetildelingWindow1.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpDisponibleDockPanel.UIControlContainer4Custom.UIDispGridViewCustom.UIIgAvailabilityCustom.UIDdGridControlCustom.UIGcDragDropGridTable;
            var viewDisp = roleTbl.Views[0];

            //Sjekke antall ansatte i lista!
            var tbl1 = this.UIOppgavetildelingWindow6.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDockPanelDockPanel.UIDockPanelDockPanel1.UIDpEmployeesDockPanel.UIControlContainer1Custom.UIEmployeeListViewCustom.UIGcEmployeesTable;
            var viewEmp = tbl1.Views[0];
            #endregion


            // Todo: Lage ordentlig sjekk er på count !=2
            if (viewDisp.RowCount != 2)
            {
                errorList.Add("Feil antall ansatte(Ansattefane");
            }
            if (viewEmp.RowCount != 2)
            {
                errorList.Add("Feil antall disponible");
            }

            return errorList;
        }

        public List<string> Step_50()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickRoleFilterInRoleAssignmentWindow();
            UICommon.UIMapVS2017.ClickEmployeeFilterInRoleAssignmentWindow();

            UICommon.UIMapVS2017.ClickNextWeekInRoleAssignmentWindow();
            UICommon.UIMapVS2017.ClickNextWeekInRoleAssignmentWindow();

            try
            {
                CheckStep50();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 50: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickPreviousWeekInRoleAssignmentWindow();
            UICommon.UIMapVS2017.ClickPreviousWeekInRoleAssignmentWindow();

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            CloseGat();

            return errorList;
        }
    }
}
