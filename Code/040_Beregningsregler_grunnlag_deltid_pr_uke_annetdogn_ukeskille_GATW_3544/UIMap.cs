namespace _040_Beregningsregler_grunnlag_deltid_pr_uke_annetdogn_ukeskille_GATW_3544
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
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
        private bool IsDfoBranch
        {
            get
            {
                var branch = TestData.GetStartFolder;
                return branch.Contains("dfo");
            }
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
            return UICommon.RestoreDatabase(false, false, true);
        }
        private void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepLønnsberegninger2100, null, "", logGatInfo, false, false, false);
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

        #endregion

        public List<string> Step_1()
        {
            var errorList = new List<string>();

            StartGat(true);

            UICommon.SelectFromAdministration("Globalt +oppsett +Generelle", true);
            SelectOvertidInGlobalSettings();

            try
            {
                CheckOvertidCalculationTypeValuesStep1();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 1: " + e.Message);
            }

            CloseGlobalSettingsWindow();

            return errorList;
        }
        public List<string> Step_2()
        {
            var errorList = new List<string>();

            //Mandag uke 1
            Playback.Wait(2000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
            GoToShiftDateNew(new DateTime(2023, 10, 23));
            try
            {
                DragAnnetDoynUkeskilleTurnusToExtraStep2();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2: " + e.Message);
            }
            try
            {
                CheckValuesStep2();
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

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            UICommon.UIMapVS2017.GenerateAMLCause();

            //Søndag uke 3
            GoToShiftDateNew(new DateTime(2023, 10, 22));
            try
            {
                DragAnnetDoynUkeskilleTurnusToExtraStep3();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }
            try
            {
                CheckValuesStep3();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            return errorList;
        }
        public List<string> Step_4()
        {
            var errorList = new List<string>();

            //Onsdag uke 1
            GoToShiftDateNew(new DateTime(2023, 10, 25));
            try
            {
                DragAnnetDoynUkeskilleTurnusToExtraStep4();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }
            try
            {
                CheckValuesStep4New();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelInExtraWindow();

            return errorList;
        }

        private void CheckValuesStep4New()
        {
            if (IsDfoBranch)
                CheckValuesStep4Dfo();
            else
                CheckValuesStep4();
        }
        public List<string> Step_5()
        {
            var errorList = new List<string>();

            //Søndag uke 1
            GoToShiftDateNew(new DateTime(2023, 10, 29));
            try
            {
                DragAnnetDoynUkeskilleTurnusToExtraStep5();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }
            try
            {
                CheckValuesStep5New();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }

            return errorList;
        }
        private void CheckValuesStep5New()
        {
            if (IsDfoBranch)
                CheckValuesStep5Dfo();
            else
                CheckValuesStep5();
        }
        public List<string> Step_6()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            UICommon.UIMapVS2017.GenerateAMLCause();

            //Onsdag uke 1
            GoToShiftDateNew(new DateTime(2023, 10, 25));
            try
            {
                DragAnnetDoynUkeskilleTurnusToExtraStep4();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }
            try
            {
                CheckValuesStep6New();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }

            return errorList;
        }
        private void CheckValuesStep6New()
        {
            if (IsDfoBranch)
                CheckValuesStep6Dfo();
            else
                CheckValuesStep6();
        }
        public List<string> Step_7()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            //Fredag uke 1
            GoToShiftDateNew(new DateTime(2023, 10, 27));
            try
            {
                DragAnnetDoynUkeskilleTurnusToRemanageStep7();
                UICommon.UIMapVS2017.SelectDivCauseRemaning();
                UICommon.UIMapVS2017.CreateNewShiftRemanage(new DateTime(2023, 10, 24), "D ");
                UICommon.UIMapVS2017.SelectFirstNewRemanageShift();
                UICommon.UIMapVS2017.RecalculateInRemaningWindow();
                UICommon.UIMapVS2017.ClickOkInRemanningWindow();
                UICommon.UIMapVS2017.GenerateAMLCause();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }
            try
            {                
                CheckValuesStep7New();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        private void CheckValuesStep7New()
        {
            if (IsDfoBranch)
                CheckValuesStep7Dfo();
            else
            {
                CheckShiftDatesStep7();
                CheckValuesStep7();
            }
        }
        public List<string> Step_8()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 10, 24));
            DeleteRemanageShiftStep8();

            try
            {
                CheckValuesStep8New();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        private void CheckValuesStep8New()
        {
            if (IsDfoBranch)
                CheckRecalcValuesStep8Dfo();
            else
                CheckRecalcValuesStep8();
        }
        public List<string> Step_9()
        {
            var errorList = new List<string>();

            //Mandag uke 1
            GoToShiftDateNew(new DateTime(2023, 10, 23));
            try
            {
                CreateAbsenceStep9();
                UICommon.UIMapVS2017.SelectAbsenceCode("90");
                UICommon.UIMapVS2017.RecalculateInAbsenceWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 9: " + e.Message);
            }
            try
            {
                CheckValuesStep9();
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

            UICommon.UIMapVS2017.ClickOkConstuctAbsence();
            try
            {
                CheckRecalcValuesStep10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;

        }

        public List<string> Step_11()
        {
            var errorList = new List<string>();

            DeleteAbsenceStep11();
            UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();
            try
            {
                CheckValuesStep11New();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        private void CheckValuesStep11New()
        {
            if (IsDfoBranch)
                CheckRecalcValuesStep11Dfo();
            else
                CheckRecalcValuesStep11();
        }
        public List<string> Step_12()
        {
            var errorList = new List<string>();

            try
            {
                CreateAbsenceStep9();
                UICommon.UIMapVS2017.SelectAbsenceCode("30 +Avs");
                UICommon.UIMapVS2017.ClickOkConstuctAbsence();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12: " + e.Message);
            }

            try
            {
                if (UICommon.UIMapVS2017.CheckRecalculationWindowExists())
                {
                    errorList.Add("Recalculation is active(Step 12)");
                    UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Recalculation not active(Step 12)");
            }

            return errorList;
        }

        public List<string> Step_13()
        {
            var errorList = new List<string>();

            try
            {
                DeleteAbsenceStep11();
                UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }

            try
            {
                if (UICommon.UIMapVS2017.CheckRecalculationWindowExists())
                {
                    errorList.Add("Recalculation is active(Step 13)");
                    UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Recalculation not active(Step 13)");
            }

            return errorList;
        }
        public List<string> Step_14()
        {
            var errorList = new List<string>();

            UICommon.SelectFromAdministration("Globalt +oppsett +Generelle", true);
            SelectOvertidInGlobalSettings();

            try
            {
                CheckOvertidCalculationTypeValuesStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 1: " + e.Message);
            }

            CloseGlobalSettingsWindow();

            CloseGat();
            StartGat(false);

            return errorList;
        }
        public List<string> Step_15()
        {
            var errorList = new List<string>();

            //Mandag uke 1
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.TimesheetTabBR, false);

            try
            {
                SelectAnnet();
                UICommon.UIMapVS2017.ClickRecalculateInEmpTimesheetTab();
                UICommon.UIMapVS2017.SetRecalculatePeriodInTimesheetTab("Uke 43 2023", "Uke 43 2023");
                UICommon.UIMapVS2017.RecalculateInTimesheetTabRecalcWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15: " + e.Message);
            }
            try
            {
                CheckValuesStep15New();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15: " + e.Message);
            }

            return errorList;
        }
        private void CheckValuesStep15New()
        {
            Playback.Wait(2000);

            if (IsDfoBranch)
                CheckRecalcValuesStep15Dfo();
            else
                CheckRecalcValuesStep15();
        }
        public List<string> Step_16()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
            UICommon.UIMapVS2017.CloseTimesheetTabRecalcWindow();

            try
            {
                OpenShift_1();
                CheckRecalcSavoOkStep16_1();
                UICommon.UIMapVS2017.CloseShiftInEmpTab();

                OpenShift_2();
                CheckRecalcSavoOkStep16_2();
                UICommon.UIMapVS2017.CloseShiftInEmpTab();

                OpenShift_3();
                CheckRecalcSavoOkStep16_3();
                UICommon.UIMapVS2017.CloseShiftInEmpTab();

                OpenShift_4();
                CheckRecalcSavoOkStep16_4();
                UICommon.UIMapVS2017.CloseShiftInEmpTab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 16: " + e.Message);
            }

            return errorList;
        }
    }
}
