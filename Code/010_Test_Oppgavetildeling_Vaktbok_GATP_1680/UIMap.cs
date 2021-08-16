namespace _010_Test_Oppgavetildeling_Vaktbok_GATP_1680
{
    using System.Drawing;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using CommonTestData;
    using System.Threading;
    using System.Globalization;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;

    public partial class UIMap
    {

        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        public string ReportFilePath;
        public string ReportFileName = "010_excel";
        public string FileType = ".xls";
        #endregion

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_010_Oppgaver_Vaktbok\");
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
            UICommon.LoginGatAndSelectDepartment(UICommon.DepOperasjon, null, "ROVE", logGatInfo);
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
        public List<String> CompareReportDataFiles_Test010()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext, 1);
            return errorList;
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
        public List<String> ExportRoleToExcell(string postfix)
        {
            var errorList = new List<String>();
            Playback.Wait(2000);
            try
            {
                var fileName = ReportFilePath + ReportFileName + postfix; 
                UICommon.UIMapVS2017.ExportShiftbookRoleView();
                UICommon.ExportToExcel(fileName);
            }
            catch (Exception e)
            {
                errorList.Add("Feil ved export til excel(" + postfix + "): " + e.Message);
            }

            Playback.Wait(3000);
            return errorList;
        }

        #endregion

        public List<string> Step_1()
        {
            var errorList = new List<string>();

            StartGat(true);
            GoToShiftDateNew(new DateTime(2024, 09, 02));

            try
            {
                CheckDateStep1();
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

            AddRoleToAndersonStep2();

            try
            {
                OpenHint();
                CheckHintStep2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2(Hint): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCompTabInRoleAssignmentDetails();

            try
            {
                CheckCompStep2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2(Comp): " + e.Message);
            }

            try
            {
                CheckOkDisabled();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2(Ok button not disabled): " + e.Message);
            }

            return errorList;
        }

        private void OpenHint()
        {
            #region Variable Declarations
            WinCell uIAppointmentCell = this.UIOppgavetildelingWindow.UISchedulerControl1List.UIAppointmentCell;
            #endregion

            Mouse.Move(uIAppointmentCell, new Point(12, 9));
            Playback.Wait(500);
        }

        public List<string> Step_3()
        {
            var errorList = new List<string>();

            SelectRoleStep3();
            UICommon.UIMapVS2017.ClickRemoveRoleAssignmentInDetailsWindow();

            errorList.AddRange(CheckRoleDeletedStep3());

            return errorList;
        }
        private void SelectRoleStep3()
        {
            #region Variable Declarations
            WinCell uIAppointmentCell = this.UIOppgavetildelingWindow.UISchedulerControl1List.UIAppointmentCell;
            #endregion

            Mouse.Click(uIAppointmentCell, new Point(12, 9));
            Playback.Wait(500);
        }
        private List<string> CheckRoleDeletedStep3()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            WinCell uIAppointmentCell = this.UIOppgavetildelingWindow.UISchedulerControl1List.UIAppointmentCell;
            uIAppointmentCell.SearchProperties.Add(new PropertyExpression(WinCell.PropertyNames.Name, "8:00 a.m.  to 4:00 p.m.  mandag, september 2, 2024, Subject KOMP KREVD/EKSKL KAN IKKE, Time Ledig, 2 of 2", PropertyExpressionOperator.Contains));
            #endregion

            Playback.Wait(1000);
            try
            {
                if (uIAppointmentCell.Exists)
                {
                    errorList.Add("Role not deleted, step 3");
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Role deleted");
            }

            return errorList;
        }

        public List<string> Step_4()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickAddRoleAssignmentInDetailsWindow();
            SelectRolesStep4();
            UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();

            try
            {
                CheckRolesAddedStep4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            return errorList;
        }

        private void CheckRolesAddedStep4()
        {
            #region Variable Declarations
            WinCell uIAppointmentCell = this.UIOppgavetildelingWindow.UISchedulerControl1List1.UIAppointmentCell;
            WinCell uIAppointmentCell1 = this.UIOppgavetildelingWindow.UISchedulerControl1List1.UIAppointmentCell1;
            #endregion

            // Verify that the 'Name' property of 'Appointment' cell equals '8:00 a.m.  to 4:00 p.m.  mandag, september 2, 2024, Subject KOMP ØNSKET/EKSKL BØR IKKE, Time Ledig, 2 of 3'
            Assert.AreEqual("8:00 a.m.  to 4:00 p.m.  mandag, september 2, 2024, Subject KOMP ØNSKET/EKSKL BØR IKKE, Time Ledig, 2 of 3", uIAppointmentCell.Name);

            // Verify that the 'Name' property of 'Appointment' cell equals '8:00 a.m.  to 4:00 p.m.  mandag, september 2, 2024, Subject OVER, Time Ledig, 3 of 3'
            Assert.AreEqual("8:00 a.m.  to 4:00 p.m.  mandag, september 2, 2024, Subject OVER, Time Ledig, 3 of 3", uIAppointmentCell1.Name);
        }

        public List<string> Step_5()
        {
            var errorList = new List<string>();
            try
            {
                SelectKompRoleStep5();
                UICommon.UIMapVS2017.ChangePeriodInRoleAssignmentDetailsWindow("12:00", "14:00");

                SelectOverRoleStep5();
                UICommon.UIMapVS2017.ChangePeriodInRoleAssignmentDetailsWindow("09:00", "11:00");
            }
            catch (Exception e)
            {
                errorList.Add("Error changing role period in Step 5: " + e.Message);
            }

            return errorList;
        }

        private void SelectKompRoleStep5()
        {
            #region Variable Declarations
            WinCell uIAppointmentCell = this.UIOppgavetildelingWindow.UISchedulerControl1List1.UIAppointmentCell;
            #endregion

            Mouse.Click(uIAppointmentCell);
        }

        private void SelectOverRoleStep5()
        {
            Mouse.Click();
        }

        public List<string> Step_6()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();

            try
            {
                CheckRolesAddedStep6();
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

            AddRoleToCockerStep7();

            try
            {
                CheckRoleAddedStep7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_8()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectRoleAssigmentDashboardWhenPinned();

            AddRoleToBruskStep8();
            SelectRolesStep8();

            try
            {
                CheckHintStep8();
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

            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();

            try
            {
                CheckRolesAddedStep9();
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

            ExchangeRolesStep10();

            try
            {
                CheckRolesExcangedStep10();
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

            AddRolesToEldersonStep11();

            try
            {
                CheckRolesAddedStep11();
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

            DeleteRole1622EldersonStep12();

            try
            {
                CheckRoleDeletedStep12();
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

            ExchangeFnattEldersonStep13();
            UICommon.UIMapVS2017.ClickOkInExchangeWindow();
            SelectFnattShiftStep13();

            try
            {
                CheckExcangedStep13();
                CheckSpecRoleStep13();
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

            SelectAndersonShiftStep14();
            UICommon.UIMapVS2017.ClickAbsenceRibbonButton();
            UICommon.UIMapVS2017.SelectAbsenceCode("10 Egen");
            CheckFreeShiftInAbsWin();

            UICommon.UIMapVS2017.ClickOkConstuctAbsence();

            try
            {
                CheckInfoWinStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14(Info window text): " + e.Message);
            }

            try
            {
                ClickOkInfoWinStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14(Closing Info window): " + e.Message);
            }

            SelectFreeShiftStep14();

            try
            {
                CheckAndersonFreeShiftStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14(Anderson/Free shifts): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_15()
        {
            var errorList = new List<string>();

            CoverVacantShiftStep15();
            UICommon.UIMapVS2017.ClickCompleteCoverWizardButton();
            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            SelectGulliShiftStep15();

            try
            {
                CheckAGulliShiftStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15(Gulli shift): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_16()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectShiftBookWeekView();

            AddRoleToCockerStep16();
            SelectRolesStep16();

            try
            {
                CheckHintStep16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 16(Hint): " + e.Message);
            }

            try
            {
                CheckOkDisabled();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 16(Ok button not disabled): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_17()
        {
            var errorList = new List<string>();

            try
            {
                DragMoveRoleStep17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 17(Drag role): " + e.Message);
            }

            try
            {
                CheckRolePeriodStep17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 17(Period): " + e.Message);
            }


            return errorList;
        }

        /// <summary>
        /// DragMoveRoleStep17
        /// </summary>
        private void DragMoveRoleStep17()
        {
            #region Variable Declarations
            WinCell uIAppointmentCell1 = this.UIOppgavetildelingWindow.UISchedulerControl1List3.UIAppointmentCell1;
            #endregion

            // Move 'Appointment' cell
            Mouse.StartDragging(uIAppointmentCell1, new Point(40, 50));
            Mouse.StopDragging(uIAppointmentCell1, new Point(40, 180));
        }

        public List<string> Step_18()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();

            UICommon.UIMapVS2017.SelectShiftBookDayView();
            GoToShiftDateNew(new DateTime(2024, 09, 03));
            SelectCockerAShiftStep18();

            try
            {
                CheckRoleAddedStep18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 18: " + e.Message);
            }

            GoToShiftDateNew(new DateTime(2024, 09, 02));
            UICommon.UIMapVS2017.SelectShiftBookWeekView();

            return errorList;
        }

        public List<string> Step_19()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectDetailedRoleAssignmentInDashboard();
            AddRoleToBerreStep19();

            try
            {
                CheckRoleAddedStep19();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 19: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_20()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickAddRoleAssignmentInDetailsWindow();
            SelectRolesStep20();
            UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();

            try
            {
                CheckRole1AddedStep20();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 20(Morgenmøte): " + e.Message);
            }

            try
            {
                CheckRole2AddedStep20();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 20(Røntgen): " + e.Message);
            }

            try
            {
                CheckRole3AddedStep20();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 20(Administrasjon): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_21()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();
            DeleteBerreRoleStep21();
            ShowRolesStep21();

            try
            {
                CheckRoleDeletedStep21();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 21: " + e.Message);
            }

            return errorList;
        }

        private void CheckRoleDeletedStep21()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIItem1012Røntgen4520MenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIRedigeroppgaveMenuItem.UIItem1012Røntgen4520MenuBaseButtonItem;
            DXMenuBaseButtonItem uIItem1216AdministrasjMenuBaseButtonItem1 = this.UIItemWindow.UIPopupMenuBarControlMenu.UIRedigeroppgaveMenuItem.UIItem1216AdministrasjMenuBaseButtonItem1;
            #endregion

            if (uIItem1012Røntgen4520MenuBaseButtonItem.Text == "08-16 - Dagtid (4520)" || uIItem1216AdministrasjMenuBaseButtonItem1.Exists)
                throw new Exception("Oppgaven 08-16 - Dagtid (4520) er ikke slettet!");
        }

        public void Step_22()
        {
            UICommon.UIMapVS2017.UnPinRoleAssigmentDashboard();
            UICommon.UIMapVS2017.ClickWorkbookRoleView();
            MoveSliderToSeDisp();
            Playback.Wait(1000);
        }

        public List<string> Step_23()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickDetailedInRosterplanRoleAssignmentView();
                Playback.Wait(1000);
                UIGatWindow.UIViewHostCustom.UIPcViewClient.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList2.DrawHighlight();

                AddRoleToEvenstadStep23();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 23: " + e.Message);
            }

            try
            {
                CheckEvenstadRoleAddedStep23();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 23(Added): " + e.Message);
            }

            try
            {
                DeleteRoleEvenstadStep23();
                CheckEvenstadRoleDeletedStep23();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 23(Deleted): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_24()
        {
            var errorList = new List<string>();

            try
            {
                AddRoleToAhlgrenStep24();
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

            try
            {
                AddRoleToAndersonStep25();
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

            try
            {
                OpenEmpListStep26();
                CheckListStep26();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 26: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_27()
        {
            var errorList = new List<string>();

            try
            {
                AddRoleToDroppStep27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 27: " + e.Message);
            }

            try
            {
                CheckDroppRoleStep27();
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

            try
            {
                AddRoleToEldersonStep28();
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

            try
            {
                AddRoleToAndresonStep29();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 29: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCompTabInRoleAssignmentDetails();

            try
            {
                CheckCompStep29();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 29(Comp): " + e.Message);
            }

            try
            {
                CheckOkDisabled();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 29(Ok button not disabled): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_30()
        {
            var errorList = new List<string>();

            SelectRoleStep30();
            UICommon.UIMapVS2017.ClickRemoveRoleAssignmentInDetailsWindow();

            errorList.AddRange(CheckRoleDeletedStep30());

            return errorList;
        }
        private List<string> CheckRoleDeletedStep30()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            WinCell uIAppointmentCell = this.UIOppgavetildelingWindow.UISchedulerControl1List.UIAppointmentCell;
            uIAppointmentCell.SearchProperties.Add(new PropertyExpression(WinCell.PropertyNames.Name, "8: 00 a.m.to 4:00 p.m.torsdag, september 5, 2024, Subject KOMP KREVD / EKSKL KAN IKKE, Time Ledig, 2 of 2", PropertyExpressionOperator.Contains));
            #endregion

            Playback.Wait(1000);
            try
            {
                if (uIAppointmentCell.Exists)
                {
                    errorList.Add("Role not deleted, step 3");
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Role deleted");
            }

            return errorList;
        }
        public List<string> Step_31()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickAddRoleAssignmentInDetailsWindow();
            SelectRoleStep31();
            UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();

            try
            {
                SelectDispRoleStep31();
                UICommon.UIMapVS2017.ChangePeriodInRoleAssignmentDetailsWindow("10:00", "14:00");
            }
            catch (Exception e)
            {
                errorList.Add("Error changing role period in Step 31: " + e.Message);
            }


            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();

            try
            {
                CheckAndersonDispRoleStep31();
            }
            catch (Exception e)
            {
                errorList.Add("Error Step 31: " + e.Message);
            }

            return errorList;
        }

        private void SelectDispRoleStep31()
        {
            Mouse.Click(new Point(UIOppgavetildelingWindow.BoundingRectangle.X + (UIOppgavetildelingWindow.BoundingRectangle.Width / 2), UIOppgavetildelingWindow.BoundingRectangle.Y + (UIOppgavetildelingWindow.BoundingRectangle.Height / 2)));
        }

        public List<string> Step_32()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.OpenFilterWindowInRoleAssignmentShiftbook();

                DeleteBlueFilterStep32();
                ConstructBlueFilterStep32();

                UICommon.UIMapVS2017.ClickUseFilterInRoleAssignmentShiftbook();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 32: " + e.Message);
            }

            try
            {
                CheckContDispRoleStep32();
            }
            catch (Exception e)
            {
                errorList.Add("Error Step 32: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_33()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.SelectResetShiftbookInRoleAssignmentShiftbook();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 33: " + e.Message);
            }

            errorList.AddRange(ExportRoleToExcell("_step_33"));

            return errorList;
        }

        public List<string> Step_34()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickWorkbookPlanner();
            try
            {
                CheckPlannerOpenStep34();
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

            AddRoleInPlannerStep35();

            try
            {
                CheckRoleAddedStep35();
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

            UICommon.UIMapVS2017.SelectRoleAssigmentDashboardWhenUnpinned();
            UICommon.UIMapVS2017.PinRoleAssigmentDashboard();

            AddRoleInPlannerStep36();

            try
            {
                OpenHintStep36();
                CheckHintStep36();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 36(Hint): " + e.Message);
            }

            try
            {
                CheckOkDisabled();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 36(Ok button not disabled): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_37()
        {
            var errorList = new List<string>();

            try
            {
                DragMoveRoleStep37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(Drag role): " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.SelectRoleAccountingDetails("4520", "P3");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(Cont): " + e.Message);
            }

            try
            {
                AddCommentStep37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(Comment): " + e.Message);
            }

            try
            {
                CheckRolePeriodStep37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(Period): " + e.Message);
            }

            return errorList;
        }

        private void DragMoveRoleStep37()
        {
            #region Variable Declarations
            WinCell uIAppointmentCell1 = this.UIOppgavetildelingWindow.UISchedulerControl1List2.UIAppointmentCell1;
            #endregion

            // Move 'Appointment' cell
            Mouse.StartDragging(uIAppointmentCell1, new Point(60, 10));
            Mouse.StopDragging(uIAppointmentCell1, 0, 180);
        }

        public List<string> Step_38()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickAddRoleAssignmentInDetailsWindow();
                SelectRolesStep38();
                UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 38: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();

            return errorList;
        }

        public List<string> Step_39()
        {
            var errorList = new List<string>();

            try
            {
                OpenRoleStep39();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 39(Open role): " + e.Message);
            }

            try
            {
                CheckContStep39();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 39(Cont): " + e.Message);
            }

            try
            {
                CheckCommentStep39();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 39(Comment): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_40()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();

            try
            {
                DragRoleToCelestonStep40();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 40(Move role): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_41()
        {
            var errorList = new List<string>();

            MoveSliderTTopBottom(false);  
            AddRoleToDroppStep41();                  

            try
            {
                DragRoleToEditPeriodStep41();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 41(Period): " + e.Message);
            }

            return errorList;
        }
        private void MoveSliderTTopBottom(bool top)
        {
            #region Variable Declarations
            DXScrollBar uIVScrollBarScrollBarControl = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UICenterPanelRoleSchedDockPanel.UIControlContainerCustom.UIVScrollBarScrollBarControl;
            var point = new Point();
            #endregion

            if (top)
            {
                uIVScrollBarScrollBarControl.ValueAsString = "23";
                point = new Point(uIVScrollBarScrollBarControl.Left, uIVScrollBarScrollBarControl.Top);
            }
            else
            {
                uIVScrollBarScrollBarControl.ValueAsString = "43";
                point = new Point(uIVScrollBarScrollBarControl.Left, uIVScrollBarScrollBarControl.BoundingRectangle.Bottom - 5);
            }

            Mouse.Click(point);           
        }
        private void DragRoleToEditPeriodStep41()
        {
            #region Variable Declarations
            WinCell uIAppointmentCell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UICenterPanelRoleSchedDockPanel.UIControlContainerCustom.UISchedulerControlWindow.UISchedulerControlList3.UIAppointmentCell1;
            #endregion

            // Move 'schedulerControl' list box
            Mouse.StartDragging(uIAppointmentCell, new Point(50, 1));
            Mouse.StopDragging(uIAppointmentCell, 0, 355);
        }
        public List<string> Step_42()
        {
            var errorList = new List<string>();

            try
            {
                ExchangeFnattDroppRolesStep42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42(Exchange roles): " + e.Message);
            }

            try
            {
                CheckRole1RoleStep42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42(Role 1): " + e.Message);
            }
            try
            {
                CheckRole2RoleStep42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42(Role 2): " + e.Message);
            }
            try
            {
                CheckRole3RoleStep42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42(Role 3): " + e.Message);
            }
            try
            {
                CheckRole4RoleStep42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42(Role 4): " + e.Message);
            }
            try
            {
                CheckRole5RoleStep42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42(Role 5): " + e.Message);
            }
            try
            {
                CheckRole6RoleStep42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42(Role 6): " + e.Message);
            }
            try
            {
                CheckRole7RoleStep42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42(Role 7): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_43()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.GoToNextShiftDateArrow();

            try
            {
                OpenForEditBerreRoleStep43();
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

            UICommon.UIMapVS2017.ClickAddRoleAssignmentInDetailsWindow();
            SelectRolesStep44();
            UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();

            try
            {
                SelectRole1Step44();
                CheckRoleError1Step44();
                SelectRole2Step44();
                CheckRoleError2Step44();
                SelectRole3Step44();
                CheckRoleError3Step44();
                SelectRole4Step44();
                CheckRoleError4Step44();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 44: " + e.Message);
            }

            try
            {
                CheckOkDisabled();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 44(Ok button not disabled): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_45()
        {
            var errorList = new List<string>();
            
            try
            {
                SelectRole1Step45();
                UICommon.UIMapVS2017.ClickRemoveRoleAssignmentInDetailsWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 45: " + e.Message);
            }
            
            return errorList;
        }
        private void SelectRole1Step45()
        {
            #region Variable Declarations
            WinCell uIAppointmentCell3 = this.UIOppgavetildelingWindow.UISchedulerControl1List4.UIAppointmentCell3;
            #endregion

            // Mouse hover 'Appointment' cell at (15, 11)
            Mouse.Click(uIAppointmentCell3, new Point(15, 11));
        }
        public List<string> Step_46()

        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickAddRoleAssignmentInDetailsWindow();
            SelectRolesStep46();
            UICommon.UIMapVS2017.ClickOkAddRoleAssignmentInDetailsWindow();

            errorList.AddRange(SelectAndCheckRoleWarningsStep46());

            return errorList;
        }
        private List<string> SelectAndCheckRoleWarningsStep46()
        {
            var errorList = new List<string>();
            var point = new Point(UIOppgavetildelingWindow.BoundingRectangle.X + ((UIOppgavetildelingWindow.BoundingRectangle.Width / 3) + 100), UIOppgavetildelingWindow.BoundingRectangle.Y + ((UIOppgavetildelingWindow.BoundingRectangle.Height / 3)));

            try
            {
                Mouse.Hover(point);
                CheckRoleWarnings1Step46();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 46: " + e.Message);
            }
            try
            {
                Mouse.Hover(new Point(Mouse.Location.X + 120, Mouse.Location.Y));
                CheckRoleWarnings2Step46();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 46: " + e.Message);
            }
            try
            {
                Mouse.Hover(new Point(Mouse.Location.X, Mouse.Location.Y + 90));
                CheckRoleWarnings3Step46();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 46: " + e.Message);
            }
            try
            {
                Mouse.Hover(new Point(Mouse.Location.X, Mouse.Location.Y + 150));
                CheckRoleWarnings4Step46();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 46: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_47()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();

            try
            {
                CheckRole1RoleStep47();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 47(Role 1): " + e.Message);
            }
            try
            {
                CheckRole2RoleStep47();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 47(Role 2): " + e.Message);
            }
            try
            {
                CheckRole3RoleStep47();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 47(Role 3): " + e.Message);
            }
            try
            {
                CheckRole4RoleStep47();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 47(Role 4): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_48()
        {
            var errorList = new List<string>();
            
            try
            {
                CheckInactiveNotInDashboardList();
                CheckInactiveNotInDashboardList2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 48(): " + e.Message);
            }

            return errorList;
        }
        public void CheckInactiveNotInDashboardList2()
        {
            #region Variable Declarations
            var tbl = UIGatWindow.UIViewHostCustom.UIPcViewClient.UILeftPanelDockPanel.UIOppgavetildelingDockPanel.UIControlContainerCustom.UIGAvailableRolesTable;
            #endregion
            var rows = tbl.Views[0].RowCount;

            if (rows != 15)
                throw new Exception("Uventet antall rader i oppgavetildelingsfane!");
        }

        public List<string> Step_49()
        {
            var errorList = new List<string>();

            OpenAddRoleList();

            try
            {
                CheckInactiveNotInAddShiftList();
                CheckInactiveNotInAddShiftList2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 49(): " + e.Message);
            }

            return errorList;
        }
    }
}
