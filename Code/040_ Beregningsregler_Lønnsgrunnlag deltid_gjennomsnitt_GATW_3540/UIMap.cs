namespace _040__Beregningsregler_Lønnsgrunnlag_deltid_gjennomsnitt_GATW_3540
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
                DragGjennomsnittTurnusToExtraStep1();
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

            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            return errorList;
        }
        public List<string> Step_2()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 10, 28));
            try
            {
                DragGjennomsnittTurnusToExtraStep2();
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

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            UICommon.UIMapVS2017.GenerateAMLCause();

            return errorList;
        }
        public List<string> Step_3()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 10, 31));
            try
            {
                DragGjennomsnittTurnusToExtraStep3();
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

            GoToShiftDateNew(new DateTime(2023, 11, 11));
            try
            {
                DragGjennomsnittTurnusToExtraStep4();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }
            try
            {
                CheckValuesStep4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            return errorList;
        }
        public List<string> Step_5_6()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 11, 05));
            try
            {
                DragGjennomsnittTurnusToExtraStep5();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("N ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }
            try
            {
                CheckValuesStep5();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            try
            {
                CheckRecalcValuesStep5();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }
            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        public List<string> Step_7_8()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 10,26));
            try
            {
                DragGjennomsnittTurnusToRemanageStep7();
                UICommon.UIMapVS2017.SelectDivCauseRemaning();
                UICommon.UIMapVS2017.CreateNewShiftRemanage(new DateTime(2023, 11, 01), "A ");
                UICommon.UIMapVS2017.SelectFirstNewRemanageShift();
                UICommon.UIMapVS2017.RecalculateInRemaningWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }
            try
            {
                CheckValuesStep7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRemanningWindow();
            UICommon.UIMapVS2017.GenerateAMLCause();

            try
            {
                CheckRecalcValuesStep8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }
            
            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        public List<string> Step_9()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 11, 01));
            DeleteRemanageShiftStep9();

            try
            {
                CheckRecalcValuesStep9();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 9: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        public List<string> Step_10()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 11, 2));
            DragGjennomsnittTurnusToAbsenceStep10();           
            UICommon.UIMapVS2017.SelectAbsenceCode("90");
            UICommon.UIMapVS2017.SetAbsencePeriod(null, new DateTime(2023, 11, 3));
            UICommon.UIMapVS2017.RecalculateInAbsenceWindow();

            try
            {
                CheckValuesStep10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10: " + e.Message);
            }
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
               CheckRecalcValuesStep11();
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

            GoToShiftDateNew(new DateTime(2023, 11, 2));
            DragGjennomsnittTurnusToAbsenceStep10();
            UICommon.UIMapVS2017.SelectAbsenceCode("30 av");
            UICommon.UIMapVS2017.SetAbsencePeriod(null, new DateTime(2023, 11, 3));

            try
            {
                UICommon.UIMapVS2017.ClickOkConstuctAbsence();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12: " + e.Message);
            }
            try
            {
                DeleteAbsenceStep11();
                UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();
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

            UICommon.SelectFromAdministration("Globalt +oppsett +Generelle", true);
            SetCalculateRegOvertimeStep13();
            CloseGat();
            StartGat(false);
            GoToShiftDateNew(new DateTime(2023, 10, 24));

            try
            {
                DeleteExtraTueWeek1Step13();
                UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }
            try
            {
                CheckRecalcValuesStep13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
            
            return errorList;
        }
        public List<string> Step_14()
        {
            var errorList = new List<string>();
            GoToShiftDateNew(new DateTime(2023, 10, 28));

            try
            {
                DeleteExtraSatWeek1Step14();
                UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14: " + e.Message);
            }
            try
            {
                CheckRecalcValuesStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
            return errorList;
        }
        public List<string> Step_15()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2023, 10, 24));
            try
            {
                DragGjennomsnittTurnusToExtraStep1();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15: " + e.Message);
            }
            try
            {
               CheckValuesStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            return errorList;
        }
        public void Step_16()
        {
            UICommon.SelectFromAdministration("Globalt +oppsett +Generelle", true);
            SetCalculateKronOvertimeStep16();
            CloseGat();
        }
    }
}
