namespace _040_Beregningsregler_Utrykning_GATW_3536
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
            return UICommon.RestoreDatabase(false, false, true);
        }
        private void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepMedisinskAvdeling, null, "", logGatInfo, false, false, false);
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
            GoToShiftDateNew(new DateTime(2019, 08, 27));
            try
            {
                DragHarryHoleToTurnoutStep1();
                UICommon.UIMapVS2017.SetCalloutCause("UTR");
                UICommon.UIMapVS2017.SelectCalloutDay1();
                //UICommon.UIMapVS2017.SelectCalloutDay2();
                UICommon.UIMapVS2017.SetCalloutPeriod("17:00", "17:30");
                //UICommon.SetCalloutDate(new DateTime(2019, 08, 27));
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

            try
            {

                UICommon.UIMapVS2017.ClickOkCallout();
                //UICommon.UIMapVS2017.ClickOkNewCallout();
                //UICommon.UIMapVS2017.ClickCancelCallout();
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

            try
            {
                DragHarryHoleToTurnoutStep2();
                UICommon.UIMapVS2017.SetTimeoff("0,5");
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

            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
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

            try
            {
                DragHarryHoleToTurnoutStep3();
                UICommon.UIMapVS2017.SetCalloutCause("UTR");
                UICommon.UIMapVS2017.SelectCalloutDay1();
                UICommon.UIMapVS2017.SetCalloutPeriod("17:30", "19:00");
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

            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
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

            GoToShiftDateNew(new DateTime(2019, 12, 25));
            try
            {
                DragHarryHoleToTurnoutStep4();
                UICommon.UIMapVS2017.SetCalloutCause("UTR");
                UICommon.UIMapVS2017.SetCalloutPeriod("01:00", "02:00");
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

            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
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
                DragHarryHoleToTurnoutStep4();
                UICommon.UIMapVS2017.SetTimeoff("1,0");
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

            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
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

            GoToShiftDateNew(new DateTime(2019, 08, 27));
            try
            {
                DragVargVeumToTurnoutStep6();
                UICommon.UIMapVS2017.SetCalloutCause("UTR");
                UICommon.UIMapVS2017.SelectCalloutDay1();
                UICommon.UIMapVS2017.SetCalloutPeriod("23:00", "03:00");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }
            try
            {
                CheckValuesStep6();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
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

            try
            {
                DragVargVeumToTurnoutStep7();
                UICommon.UIMapVS2017.SetTimeoff("3,0");
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

            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
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

            GoToShiftDateNew(new DateTime(2019, 12, 26));
            try
            {
                DragVargVeumToTurnoutStep8();
                UICommon.UIMapVS2017.SetCalloutCause("UTR");
                UICommon.UIMapVS2017.SelectCalloutDay1();
                UICommon.UIMapVS2017.SetCalloutPeriod("23:00", "04:00");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8: " + e.Message);
            }
            try
            {
                CheckValuesStep8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8: " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
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
                DragVargVeumToTurnoutStep9();
                UICommon.UIMapVS2017.SetTimeoff("3,0");
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

            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
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

            GoToShiftDateNew(new DateTime(2019, 08, 28));
            try
            {
                DragVargVeumToExtraStep10();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SetExtraShiftPeriod("08:00", "12:00");
                UICommon.UIMapVS2017.SelectShitfBookColumnExtra("Dag");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10: " + e.Message);
            }
            try
            {
                CheckValuesStep10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            return errorList;
        }
        public List<string> Step_11()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2019, 08, 27));
            try
            {
                DragVargVeumToTurnoutStep11();
                UICommon.UIMapVS2017.SetCalloutCause("UTR");
                UICommon.UIMapVS2017.SelectCalloutDay2();
                UICommon.UIMapVS2017.SetCalloutPeriod("05:30", "08:00");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }
            try
            {
                CheckValuesStep11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
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

            try
            {
                DragVargVeumToTurnoutStep12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12: " + e.Message);
            }
            try
            {
                CheckValuesStep12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(Opened): " + e.Message);
            }
            try
            {
                UICommon.UIMapVS2017.RecalculateCallout();
                CheckRecalcValuesStep12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(After recalc): " + e.Message);
            }
            try
            {
                UICommon.UIMapVS2017.SetCalloutPeriod("22:00", "03:00");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12: " + e.Message);
            }
            try
            {
              CheckValuesAfterChangeStep12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12(After change): " + e.Message);
            }
            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12: " + e.Message);
            }

            return errorList;
        }
        private List<string> CheckValuesStep12()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            var tbl = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem5LayoutControlItem.UI_tcRightTabList.UI_tpCalloutDetailsClient.UI_tcMainTabList.UI_tpSalaryClient.UIGcLinesTable;
            #endregion

            var view = tbl.Views[0];
            for (int i = 0; i < view.RowCount; i++)
            {
                var type = view.GetCellValue("colCostType", i).ToString();
                var value = view.GetCellValue("colAmount", i).ToString();

                switch (type)
                {
                    case "CostType(Code=300komp)":
                        if (value != "2")
                        {
                            errorList.Add("Unexpected value for type(300komp - Avspasering Overtid 50%). Expected 2, actual: " + value);
                        }
                        break;
                    case "CostType(Code=310komp)":
                        if (value != "1")
                        {
                            errorList.Add("Unexpected value for type(310komp - Avspasering Overtid 100%). Expected 1, actual: " + value);
                        }
                        break;

                    case "CostType(Code=300)":
                        if (value != "1")
                        {
                            errorList.Add("Unexpected value for type(300 - Overtid 50%). Expected 1, actual: " + value);
                        }
                        break;
                }
            }

            return errorList;
        }
        public List<string> Step_13()
        {
            var errorList = new List<string>();

            GoToShiftDateNew(new DateTime(2019, 08, 27));
            try
            {
                DragVargVeumToTurnoutStep13();          
                UICommon.UIMapVS2017.RecalculateCallout();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }
            try
            {
              CheckValuesStep13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }
            try
            {
                UICommon.UIMapVS2017.ClickOkCallout();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }

            return errorList;
        }

        public void Step_14()
        {
            CloseGat();
        }
    }
}
