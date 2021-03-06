namespace _010_Test_Oppgavetildeling_Arbeidsplan_GATP_1679
{
    using System.Drawing;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;
    using System.Globalization;
    using CommonTestData;
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

        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepAnestesi, null, "", logGatInfo);
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
            OpenPlan("OPPGAVER KALENDERPLAN");

            try
            {
                CheckEmpInPlanStep1();
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

            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickRosterplanRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();
            
            try
            {
                CheckValuesStep2();
                CheckRolesMonroeMadisonStep2();
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

            SelectRoleCellMondayStep3();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListEmployeesTab();
            AddMonroeRoleStep3();
            Playback.Wait(1000);
            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSpecificationTab();

            try
            {
                SelectMonroeDispMondayStep3();
                CheckRoleValuesStep3();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }
            try
            {
                SelectMonroeEsclMondayStep3();
                CheckRoleValuesStep3_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3_2: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_4()
        {
            //UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationAllocationTab();
            //UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSearchReplaceTab();
            //UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSpecificationTab();

            //UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListEmployeesTab();
            //UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListShiftPatternTab();
            //UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListRolesTab();;
            var errorList = new List<string>();

            try
            {
                CheckSaveButtonDisabledStep4();
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
            DeleteMonroeDispMonday();

            try
            {
                CheckMonroeDispMondayDeletedStep5();
                CheckSaveButtonEnabledStep5();
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

            UICommon.UIMapVS2017.ClickSaveInWorkbookRoleAssignment();
            DoubleClickMadisonRoleTuesdayStep6();
            Playback.Wait(1500);

            try
            {
                CheckStep6();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }

            return errorList;
        }
        private void CheckStep6()
        {
            #region Variable Declarations
            var roleDetails = UIOppgavetildelingWindow;
            roleDetails.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXTextEdit uISteToTimeEdit = roleDetails.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UISteToTimeEdit;
            DXTestControl uITabControlViewHost1TabList = roleDetails.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList;
            DXTextEdit uISteFromTimeEdit = roleDetails.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UISteFromTimeEdit;
            DXTextEdit uITxtCommentEdit = roleDetails.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITxtCommentEdit;
            #endregion

            // Verify that the 'Enabled' property of 'steToTime' text box equals 'False'
            Assert.AreEqual(false, uISteToTimeEdit.Enabled);

            // Verify that the 'Enabled' property of 'tabControlViewHost1' tab list equals 'False'
            Assert.AreEqual(false, uITabControlViewHost1TabList.Enabled);

            // Verify that the 'Enabled' property of 'steFromTime' text box equals 'False'
            Assert.AreEqual(false, uISteFromTimeEdit.Enabled);

            // Verify that the 'Enabled' property of 'txtComment' text box equals 'False'
            Assert.AreEqual(false, uITxtCommentEdit.Enabled);
        }
        public List<string> Step_7()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();

            var str = @"Sum timer oppdekket / sum timer behov (begrenset til behov";
            UICommon.UIMapVS2017.SummarySettingsSelectSumBehovRoleAssignment(str);

            try
            {
                CheckStep7();
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
            AssignRoleToJacksonStep8();

            try
            {
                CheckStep8();
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
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationAllocationTab();
            AssignRoleToAdamsStep9();

            try
            {
                CheckStep9();
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
            AssignRoleToAdamsStep10();

            try
            {
                CheckStep10();
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

            ChangeGridHeight();
            AssignRoleToAdamsStep11();

            try
            {
                CheckStep11();
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

            UICommon.UIMapVS2017.SelectRoleAssignmentEmployeeView();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListShiftPatternTab();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentAllocationSpecificationTab();
            AssignRolePatternToTaylorStep12();

            DecreaseTaskViewTab();

            try
            {
                CheckValuesStep12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(Roles): " + e.Message);
            }

            SelectTaylorTuesday();

            try
            {
                CheckSpecsStep12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(Specification): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_13()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickSaveInWorkbookRoleAssignment();
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();

            UICommon.ClickEditRosterPlanFromPlantab();
            EditTaylorRosterLineStep13();
            UICommon.ClickOKEditRosterPlanFromPlantab();

            UICommon.ClickRosterplanRoleAssignment();
            try
            {
                CheckStep13();
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

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();

            UICommon.ClickEditRosterPlanFromPlantab();
            EditTaylorRosterLineStep14();
            UICommon.ClickOKEditRosterPlanFromPlantab();

            UICommon.ClickRosterplanRoleAssignment();
            try
            {
                CheckStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14: " + e.Message);
            }

            return errorList;
        }

        /// <summary>
        /// EditTaylorRosterLineStep14 - Use 'EditTaylorRosterLineStep14Params' to pass parameters into this method.
        /// </summary>
        private void EditTaylorRosterLineStep14()
        {
            #region Variable Declarations
            DXCell uIDCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIDCell;
            DXCell uIItemCell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIItemCell1;
            DXButton uIJAButton = this.UIGT4003InformasjonWindow.UIJAButton;
            #endregion

            // Move 'D' cell to cell
            uIItemCell1.EnsureClickable(new Point(21, 8));
            Mouse.StartDragging(uIDCell, new Point(12, 12));
            Mouse.StopDragging(uIItemCell1, new Point(21, 8));

            Keyboard.SendKeys("d{TAB}");
            // Click '&Ja' button
            Mouse.Click(uIJAButton);
        }

        public List<string> Step_15()
        {
            var errorList = new List<string>();
            AssignRoleToAdamsStep15();
            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();
            SelectRoleAdamsStep15();

            try
            {
                CheckStep15();
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

            UICommon.UIMapVS2017.ClickRegretInWorkbookRoleAssignment();

            try
            {
                CheckStep16();
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

            UICommon.UIMapVS2017.ClickUndoRegretInWorkbookRoleAssignment();
            SelectRoleAdamsStep15();

            try
            {
                CheckStep15();
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

            SelectRoleAdamsStep15();
            UICommon.UIMapVS2017.ClickDeleteeWorkbookRoleAssignment();

            try
            {
                CheckStep16();
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

            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();

            AssignRoleToPolkStep19();
            SelectRolePolktep19();

            try
            {
                CheckValuesStep19();
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

            UICommon.UIMapVS2017.ClickDetaildAssignmentInWorkbookRoleAssignment();
            AddRoleToAdamsStep_20();

            try
            {
                CheckRoleAssignmentDetailsWindowExists();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 20(Detaljevindu vises ikke): " + e.Message);
            }

            return errorList;
        }
        private void CheckRoleAssignmentDetailsWindowExists()
        {
            #region Variable Declarations
            var roleDetails = UIOppgavetildelingWindow;
            roleDetails.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            var content = roleDetails.UIGcContentClient;
            #endregion

            Assert.AreEqual(true, roleDetails.Exists);
            Assert.AreEqual(true, content.Exists);
        }

        public List<string> Step_21()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ChangePeriodInRoleAssignmentDetailsWindow("10:00", "12:00");

            try
            {
                CheckStep21();
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

            AddRoleRoleStep22();
            OpenErrorMessageStep22_1();

            try
            {
                CheckStep22_1();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 22_1: " + e.Message);
            }

            OpenErrorMessageStep22_2();
            try
            {
                CheckStep22_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 22_3: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_23()
        {
            var errorList = new List<string>();

            EditRolePeriodWithDragStep23();

            try
            {
                CheckRolePeriodStep23();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 23: " + e.Message);
            }

            try
            {
                CheckRoleStep23();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 23: " + e.Message);
            }

            return errorList;
        }

        private void CheckRoleStep23()
        {
            var item = UIOppgavetildelingWindow8.UISchedulerControl1List1.Items[0];
            Assert.AreEqual("8:00 a.m.  to 10:00 a.m.  onsdag, januar 3, 2024, 2 Events,  2 Events Ledig", item.Name);
        }

        public List<string> Step_24()
        {
            var errorList = new List<string>();

            AddRoleStep24();
            EditRolePeriod1WithDragStep24();
            EditRolePeriod2WithDragStep24();

            try
            {
                CheckRoleStep24();
                CheckRolePeriodStep24();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 24(Role/Period): " + e.Message);
            }
            try
            {
                //SelectCompTab
                UICommon.UIMapVS2017.ClickCompTabInRoleAssignmentDetails();
                //UICommon.UIMapVS2017.ClickContTabInRoleAssignmentDetails();

                CheckRoleCompStep24();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 24(Competence): " + e.Message);
            }

            return errorList;
        }
        private void EditRolePeriod2WithDragStep24()
        {
            #region Variable Declarations
            var cList = UIOppgavetildelingWindow8.UISchedulerControl1List2;
            #endregion

            var dCell = cList.Items[0];
            Mouse.Move(new Point((dCell.Left + dCell.Width / 2), dCell.Top + dCell.Height - 1));
            Mouse.StartDragging();
            Mouse.StopDragging(new Point((dCell.Left + dCell.Width / 2), dCell.Top + dCell.Height - 150));
        }
        private void CheckRoleStep24()
        {
            var item = UIOppgavetildelingWindow8.UISchedulerControl1List2.Items[0];
            Assert.AreEqual("12:00 p.m.  to 1:00 p.m.  onsdag, januar 3, 2024, 2 Events,  2 Events Ledig", item.Name);
        }

        public List<string> Step_25()
        {
            var errorList = new List<string>();

            AddMultipleRolesRoleStep25();

            try
            {
                CheckRolesStep25_1();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 25(Kontering): " + e.Message);
            }

            OpenErrorMessageStep25_1();
            try
            {
                CheckRoleMessageStep25_1();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 25(Kontering/errormessage): " + e.Message);
            }

            try
            {
                CheckRolesStep25_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 25(Alle må krav): " + e.Message);
            }

            OpenErrorMessageStep25_2();
            try
            {
                CheckRoleMessageStep25_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 25(Alle må krav/errormessage): " + e.Message);
            }
            try
            {
                OpenOldRoleStep25_1();
                CheckOldRoleMessageStep25_1();

                OpenOldRoleStep25_2();
                CheckOldRoleMessageStep25_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 25(Old role/errormessage): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_26()
        {
            var errorList = new List<string>();

            SelectKonteringStep26();
            UICommon.UIMapVS2017.ChangePeriodInRoleAssignmentDetailsWindow("15:00", "");

            try
            {
                CheckStep26();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 26: " + e.Message);
            }

            return errorList;
        }
        private void SelectKonteringStep26()
        {
            #region Variable Declarations
            UIOppgavetildelingWindow8.SearchProperties.Remove(WinWindow.PropertyNames.Name);
            UIOppgavetildelingWindow8.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            WinCell uIAppointmentCell1 = this.UIOppgavetildelingWindow8.UISchedulerControl1List4.UIAppointmentCell1;
            #endregion

            Mouse.Click(uIAppointmentCell1);
        }
        public List<string> Step_27()
        {
            var errorList = new List<string>();

            DragAndPlaceRoleStep27();

            try
            {
                UICommon.UIMapVS2017.SelectRoleAccountingDetails("5000", "P3");
            }
            catch (Exception e)
            {
                Keyboard.SendKeys("{ESC}");
                errorList.Add("Error in Step 27(Set CodtDep/Project): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();
            OpenAdamsRoleStep27();
            SelectRoleStep27();
            try
            {
                #region Variable Declarations
                UIOppgavetildelingWindow.SearchProperties.Remove(WinWindow.PropertyNames.Name);
                UIOppgavetildelingWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
                #endregion

                CheckPeriodStep27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 27(Period): " + e.Message);
            }
            try
            {
                CheckContStep27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 27(Cont): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCompTabInRoleAssignmentDetails();

            try
            {
                CheckCompStep27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 27(Comp): " + e.Message);
            }

            SelectEkslRoleStep27();
            try
            {
                CheckPeriod2Step27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 27(Period): " + e.Message);
            }
            SelectPrioHighRoleStep27();
            try
            {
                CheckPeriod3Step27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 27(Period): " + e.Message);
            }
            SelectCompMustRoleStep27();
            try
            {
                CheckPeriod4Step27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 27(Period): " + e.Message);
            }
            SelectContRoleStep27();
            try
            {
                CheckPeriod5AndContStep27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 27(Period/Cont): " + e.Message);
            }

            return errorList;
        }

        public void DragAndPlaceRoleStep27()
        {
            var startPosition = new Point(Mouse.Location.X, Mouse.Location.Y + 10);
            var stopPosition = new Point(Mouse.Location.X, Mouse.Location.Y + 60);
            Mouse.Click(startPosition);
            
            Mouse.StartDragging();
            Mouse.StopDragging(stopPosition);
        }
        public List<string> Step_28()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();
            MoveSliderRight();
           
            AddRoleToPierceStep28();
            Playback.Wait(1500);

            try
            {
                #region Variable Declarations
                UIOppgavetildelingWindow.SearchProperties.Remove(WinWindow.PropertyNames.Name);
                UIOppgavetildelingWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
                #endregion
                CheckRoleStep28();
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

            UICommon.UIMapVS2017.ChangePeriodInRoleAssignmentDetailsWindow("", "08:00");
            UICommon.UIMapVS2017.ClickOkRoleAssignmentDetails();

            try
            {
                CheckStep29();
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

            UICommon.EffectuateFullRosterplan(false);
            UICommon.EffectuateRosterplanLines(false);
            UICommon.CloseSalaryCalculationsWindow();

            CloseRosterPlan();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
            GoToShiftDateNew(new DateTime(2023, 12, 31));
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();
            try
            {
                CheckAssignments311223Step31();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 31(31.12.2023): " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            GoToShiftDateNew(new DateTime(2024, 01, 01));
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();

            try
            {
                CheckAssignments010124Step31();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 31(01.01.2024): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_32()
        {
            var errorList = new List<string>();
            try
            {
                CheckValuesStep32();
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

            try
            {
                CheckValuesStep33();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 33: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_34()
        {
            var errorList = new List<string>();

            OpenAdamsRoleStep34();

            try
            {
                #region Variable Declarations
                UIOppgavetildelingWindow.SearchProperties.Remove(WinWindow.PropertyNames.Name);
                UIOppgavetildelingWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
                #endregion

                CheckValuesStep34();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 34(Kontering): " + e.Message);
            }

            SelectAlleMaaKravStep34();
            try
            {
                CheckValuesStep34_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 34(Alle må): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_35()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickCancelRoleAssignmentDetails();
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();

            OpenPlan("OPPGAVER KALENDERPLAN");

            try
            {
                UICommon.DeleteEffectuationRosterplan();
                if (UICommon.SelectAllAndWaitForDeleteEffectuationWindowReady())
                {
                    UICommon.DeleteEffectuatedLines();
                    UICommon.CloseDeleteEffectuationOkWindow();
                }
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

            CloseRosterPlan();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
            GoToShiftDateNew(new DateTime(2023, 12, 31));
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();

            try
            {
                CheckAssignmentsStep36();
                CheckNoAssignmentsStep36();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 36: " + e.Message);
            }

            return errorList;
        }
        private void CheckNoAssignmentsStep36()
        {
            #region Variable Declarations
            DXGrid uIGcDragDropGridTable = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIVhContentCustom.UIPcViewClient.UIRoleFocusedManageVieCustom.UIIgContentCustom.UIDdGridControlCustom.UIGcDragDropGridTable;
            DXGrid uIGcEmployeesTable = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer1DockPanel.UIDpEmployeesDockPanel.UIControlContainer1Custom.UIEmployeeListViewCustom.UIGcEmployeesTable;
            DXGrid uIGcDragDropGridTable1 = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpDisponibleDockPanel.UIControlContainer4Custom.UIDispGridViewCustom.UIIgAvailabilityCustom.UIDdGridControlCustom.UIGcDragDropGridTable;
            DXGrid uIGcSumDemandTable = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpSummaryDockPanel.UIDockPanel1_ContainerCustom.UISumDemandHostViewCustom.UIViewHost1Custom.UIPcViewClient.UIGcSumDemandTable;
            #endregion

            // Verify that the 'Name' property of 'gcDragDropGrid' table equals 'gcDragDropGrid'
            Assert.AreEqual(0, uIGcDragDropGridTable.Views[0].RowCount);

            // Verify that the 'Name' property of 'gcEmployees' table equals 'gcEmployees'
            Assert.AreEqual(0, uIGcEmployeesTable.Views[0].RowCount);

            // Verify that the 'Name' property of 'gcDragDropGrid' table equals 'gcDragDropGrid'
            Assert.AreEqual(0, uIGcDragDropGridTable1.Views[0].RowCount);

            // Verify that the 'Name' property of 'gcSumDemand' table equals 'gcSumDemand'
            Assert.AreEqual(0, uIGcSumDemandTable.Views[0].RowCount);
        }
        public List<string> Step_37()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            OpenPlan("OPPGAVER - ARBEIDSPLAN");

            try
            {
                UICommon.EffectuateFullRosterplan(true);
                UICommon.EffectuateRosterplanLines(false);
                UICommon.CloseSalaryCalculationsWindow();

                CloseRosterPlan();
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
                GoToShiftDateNew(new DateTime(2024, 03, 04));
                UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37: " + e.Message);
            }

            try
            {
                CheckAssignmentsWeek1Step37();
                CheckAssignmentsWeek1_2_Step37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(Assignments week 1): " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            GoToShiftDateNew(new DateTime(2024, 03, 11));
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();

            try
            {
                CheckAssignmentsWeek2Step37();
                CheckAssignmentsWeek2_2_Step37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37(Assignments week 2): " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_38()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            OpenPlan("OPPGAVER - ARBEIDSPLAN");

            try
            {
                UICommon.DeleteEffectuationRosterplan();
                if (UICommon.SelectAllAndWaitForDeleteEffectuationWindowReady())
                {
                    UICommon.DeleteEffectuatedLines();
                    UICommon.CloseDeleteEffectuationOkWindow();
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 38: " + e.Message);
            }

            CloseRosterPlan();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
            GoToShiftDateNew(new DateTime(2024, 03, 04));
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();

            try
            {
                CheckAssignmentsStep38();
                CheckNoAssignmentsStep38();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 38(Assignments week 1): " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            GoToShiftDateNew(new DateTime(2024, 03, 11));
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignment();

            try
            {
                CheckAssignmentsStep38();
                CheckNoAssignmentsStep38();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 38(Assignments week 2): " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();

            return errorList;
        }
        private void CheckNoAssignmentsStep38()
        {
            #region Variable Declarations
            DXGrid uIGcDragDropGridTable = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIVhContentCustom.UIPcViewClient.UIRoleFocusedManageVieCustom.UIIgContentCustom.UIDdGridControlCustom.UIGcDragDropGridTable;
            DXGrid uIGcEmployeesTable = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel.UIPanelContainer1DockPanel.UIDpEmployeesDockPanel.UIControlContainer1Custom.UIEmployeeListViewCustom.UIGcEmployeesTable;
            DXGrid uIGcDragDropGridTable1 = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpDisponibleDockPanel.UIControlContainer4Custom.UIDispGridViewCustom.UIIgAvailabilityCustom.UIDdGridControlCustom.UIGcDragDropGridTable;
            DXGrid uIGcSumDemandTable = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpSummaryDockPanel.UIDockPanel1_ContainerCustom.UISumDemandHostViewCustom.UIViewHost1Custom.UIPcViewClient.UIGcSumDemandTable;
            #endregion

            // Verify that the 'Name' property of 'gcDragDropGrid' table equals 'gcDragDropGrid'
            Assert.AreEqual(0, uIGcDragDropGridTable.Views[0].RowCount);

            // Verify that the 'Name' property of 'gcEmployees' table equals 'gcEmployees'
            Assert.AreEqual(0, uIGcEmployeesTable.Views[0].RowCount);

            // Verify that the 'Name' property of 'gcDragDropGrid' table equals 'gcDragDropGrid'
            Assert.AreEqual(0, uIGcDragDropGridTable1.Views[0].RowCount);

            // Verify that the 'Name' property of 'gcSumDemand' table equals 'gcSumDemand'
            Assert.AreEqual(0, uIGcSumDemandTable.Views[0].RowCount);
        }
    }
}
