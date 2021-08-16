namespace _040__Beregningsregler_Lønnsgrunnlag_deltid_pr__uke
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
            GoToShiftDateNew(new DateTime(2023, 10, 24));
            try
            {
                DragStandardTurnusToExtraStep1();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 1: " + e.Message);
            }
            try
            {
                CheckValuesStep1();
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

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            GoToShiftDateNew(new DateTime(2023, 10, 31));
            try
            {
                DragStandardTurnusToExtraStep2();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("ASCL ");
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

            GoToShiftDateNew(new DateTime(2023, 11, 07));
            try
            {
                DragStandardTurnusToRemanageStep3();
                UICommon.UIMapVS2017.SelectDivCauseRemaning();
                UICommon.UIMapVS2017.CreateNewShiftRemanage(new DateTime(2023, 11, 04), "A ");
                UICommon.UIMapVS2017.SelectFirstNewRemanageShift();
                UICommon.UIMapVS2017.RecalculateInRemaningWindow();
                UICommon.UIMapVS2017.ClickOkInRemanningWindow();
                UICommon.UIMapVS2017.GenerateAMLCause();
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

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        public List<string> Step_4()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 11, 04));
            DeleteRemanageShiftStep4();
            UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();

            try
            {
                CheckValuesStep4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        public List<string> Step_5()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 10, 27));
            CreateAbsenceStep5();
            UICommon.UIMapVS2017.SelectAbsenceCode("90");
            UICommon.UIMapVS2017.RecalculateInAbsenceWindow();

            try
            {
                CheckValuesStep5();
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

            UICommon.UIMapVS2017.ClickOkConstuctAbsence();

            try
            {
                CheckValuesStep6();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        public List<string> Step_7()
        {
            var errorList = new List<string>();

            DeleteAbsenceStep7();
            UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();

            try
            {
                CheckValuesStep7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7(Delete Abs): " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            CreateAbsenceStep7();
            UICommon.UIMapVS2017.SelectAbsenceCode("30 +avspasering");
            UICommon.UIMapVS2017.ClickOkConstuctAbsence();

            return errorList;
        }
        public List<string> Step_8()
        {
            var errorList = new List<string>();

            DeleteAbsenceStep8();
            UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();
            try
            {
                if (UICommon.UIMapVS2017.CheckRecalculationWindowExists())
                {
                    errorList.Add("Recalculation is active(Step 8)");
                    UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Recalculation not active(Step 8)");
            }

            return errorList;
        }
        public List<string> Step_9()
        {
            var errorList = new List<string>();
            GoToShiftDateNew(new DateTime(2023, 11, 10));
            DragStandardTurnusToExtraStep9();
            UICommon.UIMapVS2017.SelectDivOvertimeCode();
            UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            try
            {
                CheckValuesStep9();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 9: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            return errorList;
        }
        public List<string> Step_10()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 11, 6));
            DragStandardTurnusToExtraStep10();
            UICommon.UIMapVS2017.SelectDivOvertimeCode();
            UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            try
            {
                CheckValuesStep10();
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

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            try
            {
                CheckValuesStep11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        public List<string> Step_12()
        {
            var errorList = new List<string>();

            DeleteExtraShiftStep12();
            UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();
            try
            {
                CheckValuesStep12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12: " + e.Message);
            }
            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
            return errorList;
        }
        public List<string> Step_13()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 11, 5));
            DragStandardTurnusToExtraStep13();
            UICommon.UIMapVS2017.SelectDivOvertimeCode();
            UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("N ");
            try
            {
                CheckValuesStep13New();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }

            return errorList;
        }

        private void CheckValuesStep13New()
        {
            #region Variable Declarations
            var uIItem200TimelønnCell2 = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UITcExtraDetailTabList.UITpSalaryCalcClient.UIGcAccountingLinesTable.UIItem200TimelønnCell2;
            var uIItem1250Cell = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UITcExtraDetailTabList.UITpSalaryCalcClient.UIGcAccountingLinesTable.UIItem1250Cell;
            #endregion

            if (uIItem200TimelønnCell2.Text == "" && uIItem1250Cell.Text == "0,00")
                CheckValuesStep13org();
            else
                CheckValuesStep13();
        }

        public List<string> Step_14()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            try
            {
                CheckValuesStep14();
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

            UICommon.UIMapVS2017.ClickCancelInRecalculationWindow();
            UICommon.UIMapVS2017.ClickCancelInExtraWindow();

            UICommon.SelectFromAdministration("Globalt +oppsett +Generelle", true);
            EditOvertimeRadioButtonsStep15();
            CloseGat();
            StartGat(false);

            GoToShiftDateNew(new DateTime(2023, 11, 5));
            DragStandardTurnusToExtraStep13();
            UICommon.UIMapVS2017.SelectDivOvertimeCode();
            UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("N ");
            try
            {
                CheckValuesStep15New();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            return errorList;
        }
        private void CheckValuesStep15New()
        {
            #region Variable Declarations
            var uIItem200TimelønnCell2 = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UITcExtraDetailTabList.UITpSalaryCalcClient.UIGcAccountingLinesTable.UIItem200TimelønnCell2;
            var uIItem1250Cell = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UITcExtraDetailTabList.UITpSalaryCalcClient.UIGcAccountingLinesTable.UIItem1250Cell;
            #endregion

            if (uIItem200TimelønnCell2.Text == "" && uIItem1250Cell.Text == "0,00")
                CheckValuesStep15org();
            else
                CheckValuesStep15();
        }

        public void Step_16()
        {
            UICommon.SelectFromAdministration("Globalt +oppsett +Generelle", true);
            EditOvertimeRadioButtonsStep16();
            CloseGat();
        }
    }
}