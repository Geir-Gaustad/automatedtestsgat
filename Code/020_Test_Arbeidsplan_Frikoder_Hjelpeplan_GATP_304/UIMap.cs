namespace _020_Test_Arbeidsplan_Frikoder_Hjelpeplan_GATP_304
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
    using CommonTestData;

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
            UICommon.LoginGatAndSelectDepartment(UICommon.DepFrikoder, null, "", logGatInfo);
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
        #endregion

        public List<string> CreateHelpPlan()
        {
            var errorList = new List<string>();
            try
            {
                UICommon.SelectNewHelpplan();
                UICommon.SetStartDateNewHelpplan(new DateTime(2022, 04, 11));
                Playback.Wait(1000);

                UICommon.SetHelpPlanWeeks("6");
                Playback.Wait(1000);

                UICommon.ClickOkCreateHelpPlan();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 2: " + e.Message);
            }

            CloseRosterPlan();

            return errorList;
        }

        public int CheckGridEmpty()
        {
            #region Variable Declarations
            var table = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpValidationMessagesClient.UIGsPanelControl7Client.UIGridValidationMessagTable;
            #endregion

            return table.Views[0].RowCount;
        }
        
        public void Step_1()
        {
            StartGat(true);
        }
        public void Step_2()
        {
            OpenPlan("VARSEL FRIKODER");
            ClickRosterplanPlanTab();

            CreateHelpPlan();
        }
        public void Step_3()
        {
            OpenPlan("Hjelpeplan for VARSEL FRIKODER.");
        }

        public List<string> Step_4()
        {
            var errorList = new List<string>();
            Click_EditRosterPlan();

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Feiladvarsler);

            UICommon.UIMapVS2017.CheckShowOnlyForSelectedEmpInWarningsSubTab();
            UICommon.UIMapVS2017.CheckShowOnlyMessagesOnSelectedDateInWarningsSubTab();

            ClickCellStep_4();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            if (CheckGridEmpty() > 0)
                errorList.Add("Errorlist not empty(Step 4)");

            return errorList;
        }
        public List<string> Step_5()
        {
            var errorList = new List<string>();

            ClickCellStep_5();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            try
            {
                CheckStep5Errors();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 5: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_6()
        {
            var errorList = new List<string>();

            ClickCellStep_6();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            if (CheckGridEmpty() > 0)
                errorList.Add("Errorlist not empty(Step 6)");

            return errorList;
        }
        public List<string> Step_7()
        {
            var errorList = new List<string>();

            ClickCellStep_7();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            try
            {
                CheckStep7Errors();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 7: " + e.Message);
            }

            ClickCellStep_7_2();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            try
            {
                CheckStep7_2Errors();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 7_2: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_8()
        {
            var errorList = new List<string>();

            ClickCellStep_8();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 5}{ENTER}");

            if (CheckGridEmpty() > 0)
                errorList.Add("Errorlist not empty(Step 8)");

            return errorList;
        }
        public List<string> Step_9()
        {
            var errorList = new List<string>();

            ClickCellStep_9();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 3}{ENTER}");

            if (CheckGridEmpty() > 0)
                errorList.Add("Errorlist not empty(Step 9)");

            return errorList;
        }
        public List<string> Step_10()
        {
            var errorList = new List<string>();

            ClickCellStep_10();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            //Todo: endre etter Bug er fixet(denne skal melde feil)
            //CheckStep10Errors();
            if (CheckGridEmpty() < 1)
                errorList.Add("No errors showing(Step 10)");

            return errorList;
        }
        public List<string> Step_11()
        {
            var errorList = new List<string>();

            ClickCellStep_11();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            if (CheckGridEmpty() > 0)
                errorList.Add("Errorlist not empty(Step 11)");

            return errorList;
        }
        public List<string> Step_12()
        {
            var errorList = new List<string>();

            ClickCellStep_12();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            if (CheckGridEmpty() > 0)
                errorList.Add("Errorlist not empty(Step 12)");

            return errorList;
        }
        public List<string> Step_13()
        {
            var errorList = new List<string>();

            ClickCellStep_13();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            if (CheckGridEmpty() > 0)
                errorList.Add("Errorlist not empty(Step 13)");

            return errorList;
        }
        public List<string> Step_14()
        {
            var errorList = new List<string>();

            ClickCellStep_14();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            try
            {
                CheckStep14Errors();
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

            ClickCellStep_15();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 4}{ENTER}");

            if (CheckGridEmpty() > 0)
                errorList.Add("Errorlist not empty(Step 15)");

            return errorList;
        }
        public List<string> Step_16()
        {
            var errorList = new List<string>();

            ClickCellStep_15();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 5}{ENTER}");

            try
            {
                CheckStep16Errors();
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

            ClickCellStep_17();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 5}{ENTER}");

            try
            {
                CheckStep17Errors();
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

            ClickCellStep_18();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 5}{ENTER}");

            try
            {
                CheckStep18Errors();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 18 " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_19()
        {
            var errorList = new List<string>();

            ClickCellStep_19();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 5}{ENTER}");

            try
            {
                CheckStep19Errors();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 19: " + e.Message);
            }

            return errorList;
        }

        public void Step_20()
        {
            UICommon.ClickCancelEditRosterPlanFromPlantab();
            UICommon.ClickDiscardChangesToRosterplan();
            CloseRosterPlan();

            OpenPlan("VARSEL FRIKODER (STARTDAGEN)");
        }

        public void Step_21()
        {
            ClickRosterplanPlanTab();
            CreateHelpPlan();
        }

        public List<string> Step_22()
        {
            var errorList = new List<string>();

            OpenPlan("Hjelpeplan for VARSEL FRIKODER (STARTDAGEN).");
            Click_EditRosterPlan();

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Feiladvarsler);

            UICommon.UIMapVS2017.CheckShowOnlyForSelectedEmpInWarningsSubTab();
            UICommon.UIMapVS2017.CheckShowOnlyMessagesOnSelectedDateInWarningsSubTab();

            ClickCellStep_22();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");

            if (CheckGridEmpty() > 0)
                errorList.Add("Errorlist not empty(Step 22)");

            DeleteShiftStep22();
            return errorList;
        }
        public void DeleteShiftStep22()
        {
            #region Variable Declarations
            var deleteCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell15; 
            #endregion

            Keyboard.SendKeys(deleteCell, "{DELETE}");
        }

        public List<string> Step_23()
        {
            var errorList = new List<string>();

            ClickCellStep_23();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 2}{ENTER}");
            try
            {
                CheckStep23Errors();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 23: " + e.Message);
            }

            if (CheckGridEmpty() > 1)
                errorList.Add("Errorlist not empty(Step 23)");

            DeleteShiftStep23();
            return errorList;
        }

        public void DeleteShiftStep23()
        {
            #region Variable Declarations
            var deleteCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell16;
            #endregion

            Keyboard.SendKeys(deleteCell, "{DELETE}");
        }

        public List<string> Step_24()
        {
            var errorList = new List<string>();

            ClickCellStep_24();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 4}{ENTER}");

            if (CheckGridEmpty() > 0)
                errorList.Add("Errorlist not empty(Step 24)");

            return errorList;
        }

        public List<string> Step_25()
        {
            var errorList = new List<string>();

            ClickCellStep_24();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 5}{ENTER}");
            try
            {
                CheckStep25Errors();
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

            ClickCellStep_26();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 5}{ENTER}");

            try
            {
                CheckStep26Errors();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 26: " + e.Message);
            }

            DeleteShiftStep26();
            return errorList;
        }
        public void DeleteShiftStep26()
        {
            #region Variable Declarations
            var deleteCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell18;
            #endregion

            Keyboard.SendKeys(deleteCell
, "{DELETE}");
        }
        public List<string> Step_27()
        {
            var errorList = new List<string>();

            ClickCellStep_27();
            Keyboard.SendKeys("{DOWN 6}{RIGHT}{DOWN 5}{ENTER}");

            try
            {
                CheckStep27Errors();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 27: " + e.Message);
            }

            return errorList;
        }

        public void EndTest()
        {
            UICommon.ClickCancelEditRosterPlanFromPlantab();
            UICommon.ClickDiscardChangesToRosterplan();
            CloseRosterPlan();
            CloseGat();
        }
    }
}
