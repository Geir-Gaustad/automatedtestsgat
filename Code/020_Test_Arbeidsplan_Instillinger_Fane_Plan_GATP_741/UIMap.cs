namespace _020_Test_Arbeidsplan_Instillinger_Fane_Plan
{
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
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
    using System.IO;
    using System.Globalization;
    using System.Threading;

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

        private DateTime GetNextDayOfWeekDay(DateTime from, DayOfWeek dayOfWeek)
        {
            return SupportFunctions.NextDayOfWeekDay(from, dayOfWeek);
        }
        private DateTime GetPreviousDayOfWeekDay(DateTime from, DayOfWeek dayOfWeek)
        {
            return SupportFunctions.PreviousDayOfWeekDay(from, dayOfWeek);
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
        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepArbeidsplanOghjelpeplan, null, "", logGatInfo);
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

        public void OpenPlan(string planName, bool openPlan = true)
        {
            Playback.Wait(1500);
            Click_RosterplanTab();
            UICommon.SelectRosterPlan(planName, openPlan);
        }
        public void Click_EditRosterPlan()
        {
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();
        }

        private void Click_RosterplanTab()
        {
            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Rosterplan);
        }
        private bool CheckRosterPlanExists(string planName, int countPlans = -1)
        {
            var exists = false;

            if (countPlans > -1)
                exists = UICommon.CheckRosterPlanExists(planName) && UICommon.CheckNumberOfRosterplans() == countPlans;
            else
                exists = UICommon.CheckRosterPlanExists(planName);

            return exists;
        }
        public void DeleteRosterPlan(string plan)
        {
            UICommon.SelectRosterPlan(plan, false);
            UICommon.ClickDeleteRosterplanButton();
        }

        public void ClickRosterplanPlanTab()
        {
            UICommon.ClickRosterplanPlanTab();
        }

        public void ClickNewRosterPlanCopy()
        {
            UICommon.ClickNewRosterPlanCopy();
        }
        public void CreateRosterplanCopy(string name)
        {
            var errorList = new List<string>();
            UICommon.CreateNewRosterplanCopy(name, new DateTime(2020, 01, 01), "", "12", true, false);
            UICommon.UIMapVS2017.OkCreateRosterplanCopy();
            CloseRosterPlan();
        }
        public void CreateCalendarplan()
        {
            UICommon.ClickNewRosterplanButton();
            UICommon.UIMapVS2017.SetRosterPlanName("KP");
            UICommon.UIMapVS2017.SelectRosterplanType("Kalenderplan");
            UICommon.UIMapVS2017.SetRosterPlanWeeks("10");
            UICommon.CheckNightShiftsOnStartDay();
            UICommon.UIMapVS2017.SetRosterPlanWeekRotation("5");
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

        private int CheckNumberOfLines()
        {
            var tbl = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable;
            return tbl.Views[0].RowCount;
        }

        private void GoToShiftDateNew(DateTime date)
        {
            UICommon.GoToShiftbookdate(date);
        }
        public void OpenFilterPrAvdDropdown()
        {
            #region Variable Declarations
            DXRibbonEditItem uIDdlOtherDepFilterRibbonEditItem = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIDdlOtherDepFilterRibbonEditItem;
            #endregion

            Mouse.Click(uIDdlOtherDepFilterRibbonEditItem, new Point(158, 9));
        }
        public void OpenRegStatusWindow()
        {
            #region Variable Declarations
            DXPopupEdit uILnkDetailPopupEdit = this.UIArbeidsplanInnstilliWindow.UIPaTopClient.UILnkDetailPopupEdit;
            #endregion

            Mouse.Click(uILnkDetailPopupEdit);
        }
        #endregion

        public List<string> Step_1()
        {
            var errorList = new List<string>();

            StartGat(true);
            Click_RosterplanTab();

            try
            {
                if (!CheckRosterPlanExists("Innstillinger - andre planer") || !CheckRosterPlanExists("Innstillinger - vaktbok", 4))
                    throw new Exception("Plan does not exist");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 1: " + e.Message);
            }

            try
            {
                DeleteRosterPlan("Hjelpeplan for Innstillinger - arbeidsplan.");
                DeleteRosterPlan("Innstillinger - arbeidsplan");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 1(Delete plans): " + e.Message);
            }


            return errorList;
        }
        public List<string> Step_2()
        {
            var errorList = new List<string>();

            OpenPlan("Innstillinger - vaktbok");

            try
            {
                UICommon.ClickRosterplanPlanTab();
                UICommon.DeleteEffectuationRosterplan();
                if (UICommon.SelectAllAndWaitForDeleteEffectuationWindowReady())
                {
                    UICommon.DeleteEffectuatedLines();
                    UICommon.CloseDeleteEffectuationOkWindow();
                }

                UICommon.EffectuateFullRosterplan(false);
                UICommon.EffectuateRosterplanLines(false);
                UICommon.CloseSalaryCalculationsWindow();

                CloseRosterPlan();
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

            UICommon.ClickNewRosterplanButton();

            errorList.AddRange(CheckNewPlanDefaultDateData());

            try
            {
                CheckNewPlanDefaultData();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            OpenRegStatusWindow();
            try
            {
                CheckRegStatus();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRegStatusWindow();

            return errorList;
        }
        private List<string> CheckNewPlanDefaultDateData()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEStartDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEStopDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit2 = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEValidToDateCustom.UIPceDateDateTimeEdit;
            DXLookUpEdit uILeDisplayStartDateLookUpEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UILeDisplayStartDateLookUpEdit;

            var startDate = GetPreviousDayOfWeekDay(DateTime.Now, DayOfWeek.Monday);
            var endDate = GetNextDayOfWeekDay(startDate.AddDays(35), DayOfWeek.Sunday).ToShortDateString();
            var startDateShort = startDate.ToShortDateString();
            var startDate2 = startDate.ToShortDateString();
            var startDatePlan = uIPceDateDateTimeEdit.DateTime.ToShortDateString();
            var endDatePlan = uIPceDateDateTimeEdit1.DateTime.ToShortDateString();
            var validDatePlan = uIPceDateDateTimeEdit2.DateTime.ToShortDateString();
            var startViewDatePlan = Convert.ToDateTime(uILeDisplayStartDateLookUpEdit.Value).ToShortDateString();
            #endregion

            try
            {
                Assert.AreEqual(startDateShort, startDatePlan);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3(Start  date): " + e.Message);
            }
            try
            {
                Assert.AreEqual(endDate, endDatePlan);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3(End date): " + e.Message);
            }
            try
            {
                Assert.AreEqual(endDate, validDatePlan);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3(Valid date): " + e.Message);
            }
            try
            {
                Assert.AreEqual(startDateShort, startViewDatePlan);
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3(View date): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_4()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SetRosterPlanName("Innstillinger - arbeidsplan");
            UICommon.SetStartDateRosterplan(new DateTime(2024, 01, 01));
            UICommon.UIMapVS2017.SetRosterPlanWeeks("3");

            UICommon.ClickOkRosterplanSettings();

            try
            {
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            return errorList;
        }
        public void Step_5()
        {
            UICommon.ClickRosterplanPlanTab();

            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            AddEmployeeStep_5();
            UICommon.ClickOkAddEmployeesWindow();
            UICommon.ClickOkEmployeesWindow();
        }

        public List<string> Step_6()
        {
            var errorList = new List<string>();

            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                AddRosterplanShiftStep_6();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 6: " + e.Message);
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();

            UICommon.ClickRosterplanFilterTab();
            ShowAllRosterlinesOnAllDepsFilter();
            Playback.Wait(1000);
            try
            {
                CheckRosterLinesStep6();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 6: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_7()
        {
            var errorList = new List<string>();

            UICommon.OpenRosterplanSettings(true);
            UICommon.UIMapVS2017.SetPlanDoLoadWorkScheduleData(false);

            UICommon.ClickOkRosterplanSettings();

            try
            {
                CheckInfoWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 7: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_8()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkInReloadPlanInfoWindow();

            OpenPlan("Innstillinger - arbeidsplan");
            UICommon.ClickRosterplanFilterTab();
            ClickFilterPrAvdShowShiftBookLines();

            try
            {
                CheckRosterlineStep7();

                if (CheckNumberOfLines() != 1)
                    throw new Exception("Unexpected number of lines");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 8: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_9()
        {
            var errorList = new List<string>();

            ClickFilterPrAvdShowPlanOtherDeps();

            try
            {
                CheckRosterlineStep8();

                if (CheckNumberOfLines() != 3)
                    throw new Exception("Unexpected number of lines");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 9: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_10()
        {
            var errorList = new List<string>();

            UICommon.OpenRosterplanSettings(true);
            UICommon.UIMapVS2017.SetPlanDoLoadOtherRosterplanData(false);

            UICommon.ClickOkRosterplanSettings();

            try
            {
                CheckInfoWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 10: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_11()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickOkInReloadPlanInfoWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 11: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_12()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickRosterplanSettingsInRosterplanTab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 12: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_13()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.CheckRosterPlanNightshiftsOnStartday(true);
                UICommon.UIMapVS2017.SetPlanDoLoadWorkScheduleData(true);
                UICommon.UIMapVS2017.SetPlanDoLoadOtherRosterplanData(true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 13: " + e.Message);
            }

            UICommon.ClickOkRosterplanSettings();

            return errorList;
        }


        public List<string> Step_14()
        {
            var errorList = new List<string>();

            OpenPlan("Innstillinger - arbeidsplan");
            UICommon.ClickRosterplanFilterTab();
            ShowAllRosterlinesOnAllDepsFilter();
            UICommon.UIMapVS2017.CheckShowNightShiftPlacementFilter();

            try
            {
                CheckRosterLinesStep6();

                if (CheckNumberOfLines() != 4)
                    throw new Exception("Unexpected number of lines");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 14: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_15()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectViewFilter("Arbeidsplan");
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Feiladvarsler);

            ExpandCalcCols();
            ExpandBottomDockPanel();

            try
            {
                CheckRosterCalculationsStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 15(Calculation): " + e.Message);
            }
            try
            {
                CheckValidationMessagesStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 15(Validation): " + e.Message);
            }            

            return errorList;
        }
        public List<string> Step_16()
        {
            var errorList = new List<string>();

            UICommon.OpenRosterplanSettings(true);
            UICommon.UIMapVS2017.CheckUseLimitedScopeInSettings(true);

            try
            {
                CheckCheckboxesDisabledStep16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 16: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_17()
        {
            var errorList = new List<string>();

            UICommon.ClickOkRosterplanSettings();

            try
            {
                CheckInfoWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 17: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_18()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickOkInReloadPlanInfoWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 18: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_19()
        {
            var errorList = new List<string>();

            OpenPlan("Innstillinger - arbeidsplan");
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Feiladvarsler);
            UICommon.ClickRosterplanFilterTab();
            ShowAllRosterlinesOnAllDepsFilter();

            try
            {
                CheckRosterLinesStep19();

                if (CheckNumberOfLines() != 1)
                    throw new Exception("Unexpected number of lines");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 19(Line in plan): " + e.Message);
            }
            try
            {
                CheckRosterCalculationsStep19();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 19(Calculations): " + e.Message);
            }
            try
            {
                CheckNoErrorWarnings();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 19(Error/warnings): " + e.Message);
            }

            return errorList;
        }

        private void CheckNoErrorWarnings()
        {
            #region Variable Declarations
            DXGrid uIGridValidationMessagTable = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIValidationViewCustom.UIGridValidationMessagTable;
            #endregion

            Assert.AreEqual(0, uIGridValidationMessagTable.Views[0].RowCount);
        }


        public List<string> Step_20()
        {
            var errorList = new List<string>();

            UICommon.OpenRosterplanSettings(true);
            UICommon.UIMapVS2017.CheckUseLimitedScopeInSettings(false);

            try
            {
                CheckCheckboxesEnsabledStep20();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 20: " + e.Message);
            }

            return errorList;
        }
        private void CheckCheckboxesEnsabledStep20()
        {
            #region Variable Declarations
            DXCheckBox uIChkDoLoadWorkSchedulCheckBox = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkDoLoadWorkSchedulCheckBox;
            DXCheckBox uIChkDoLoadOtherRosterCheckBox = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkDoLoadOtherRosterCheckBox;
            #endregion

            // Verify that the 'Checked' property of 'chkDoLoadWorkScheduleData' check box equals 'False'
            Assert.AreEqual(false, uIChkDoLoadWorkSchedulCheckBox.Checked);

            // Verify that the 'Enabled' property of 'chkDoLoadWorkScheduleData' check box equals 'False'
            Assert.AreEqual(true, uIChkDoLoadWorkSchedulCheckBox.Enabled);

            // Verify that the 'Checked' property of 'chkDoLoadOtherRosterplanData' check box equals 'False'
            Assert.AreEqual(false, uIChkDoLoadOtherRosterCheckBox.Checked);

            // Verify that the 'Enabled' property of 'chkDoLoadOtherRosterplanData' check box equals 'False'
            Assert.AreEqual(true, uIChkDoLoadOtherRosterCheckBox.Enabled);
        }

        public List<string> Step_21()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SetPlanDoLoadWorkScheduleData(true);
            UICommon.UIMapVS2017.SetPlanDoLoadOtherRosterplanData(true);
            UICommon.SetValidToDateRosterplan(new DateTime(2025, 02, 02));

            try
            {
                UICommon.ClickOkRosterplanSettings();
                UICommon.UIMapVS2017.ClickOkInReloadPlanInfoWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 21: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_22()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickRosterplanSettingsInRosterplanTab();
                SetRosterplanDisplayDateStep22();
                //UICommon.SetStartDateRosterplan(new DateTime(2025, 01, 13));
                UICommon.ClickOkRosterplanSettings();
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

            OpenPlan("Innstillinger - arbeidsplan");
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Visualisering);
            try
            {
                CheckPlanStartWeekStep23();
                CheckPlanStartWeekVisualizationStep23();
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

            UICommon.ClickRosterplanFilterTab();
            ShowAllRosterlinesOtherDepsFilter();

            try
            {
                CheckRosterlineStep24();

                if (CheckNumberOfLines() != 3)
                    throw new Exception("Unexpected number of lines");
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

            CloseRosterPlan();

            try
            {
                UICommon.SelectFromAdministration("avdelingsoppsett +Sys", true);
                AddSettingsStep25();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 25: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_26()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.SelectFromAdministration("GLOBALT +Arb");
                AddSettingsStep26();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 26: " + e.Message);
            }

            Click_RosterplanTab();

            return errorList;
        }

        public List<string> Step_27()
        {
            var errorList = new List<string>();

            CreateCalendarplan();

            try
            {
                CheckOKDisabledStep27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 26: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_28()
        {
            var errorList = new List<string>();

            OpenRegStatusWindow();

            try
            {
                CheckRegStatusStep28();
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
                UICommon.UIMapVS2017.CloseRegStatusWindow();
                UICommon.UIMapVS2017.ClickCancelRosterplanSettings();
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

            OpenPlan("Innstillinger - arbeidsplan");
            UICommon.ClickRosterplanPlanTab();
            UICommon.SelectNewHelpplan();
            UICommon.SetStartDateNewHelpplan(new DateTime(2024, 01, 15));
            UICommon.SetHelpPlanWeeks("3");
            Playback.Wait(1000);
            UICommon.ClickOkCreateHelpPlan();

            CloseRosterPlan();

            OpenPlan("Hjelpeplan for Innstillinger - arbeidsplan.", false);
            UICommon.UIMapVS2017.ClickRosterplanSettingsInRosterplanTab();

            try
            {
                CheckPlanSettingsStep30();
                CheckPlanSettingsStep30_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 30: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelRosterplanSettings();

            return errorList;
        }
    }
}
