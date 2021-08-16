using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.Diagnostics;

namespace _020_Test_Arbeidsplan_Beregninger
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Test_Arbeidsplan_Beregninger
    {


        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Beregninger()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions; ;

            UIMap.RestoreDatabase();
            UIMap.DeleteReportFiles();

            //Step 1
            UIMap.StartGat(true);

            //Step 2
            UIMap.SelectRosterplan("BEREGNINGER - KP", true, false);
            UIMap.SelectRosterPlanFilterTab();
            UIMap.SelectWeeksumLineAndTotalFilter();
            try
            {
                this.UIMap.CheckValues_Step_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 2: " + e.Message);
            }

            //Step 3
            UIMap.SelectShowInformationRosterplanCalk();
            errorList.AddRange(UIMap.SaveDataFromRosterplanCalculations("_step_3"));

            //Step 4
            UIMap.SelectShowWeek0AndN1Filter(true);
            try
            {
                this.UIMap.CheckValues_Step_4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 4: " + e.Message);
            }

            //New step 5-6
            errorList.AddRange(UIMap.NewStep5And6());

            //Step 5(7)
            UIMap.SelectRosterPlanFilterTab();
            UIMap.SelectShowWeek0AndN1Filter(false);

            //Step 6(8)
            this.UIMap.SetPlutt2LineInactive();

            //Step 7(9)
            UIMap.SelectShowInactiveLinesInFilter();
            try
            {
                this.UIMap.CheckValues_Step_7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 7(9): " + e.Message);
            }

            UIMap.SelectShowInactiveLinesInFilter();

            //Step 8(10)
            UIMap.ExchangeVakantShift();
            try
            {
                this.UIMap.CheckValues_Step_8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 8(10): " + e.Message);
            }

            //Step 9(11)
            UIMap.ExchangeVacantShiftCodes();
            try
            {
                UIMap.CheckValuesVakant_Step_9();
                UIMap.CheckValues_Step_9();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 9(11): " + e.Message);
            }

            //Step 10(12)
            UIMap.InsertJamShiftCodes();
            try
            {
                this.UIMap.CheckValues_Step_10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 10(12): " + e.Message);
            }

            UIMap.DeleteJamShiftCode();
            try
            {
                this.UIMap.CheckValues_Step_10_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 10_2(12): " + e.Message);
            }

            //Step 11(13)            
            try
            {
                UIMap.InsertRogerAbsence();
                this.UIMap.CheckValues_Step_11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 11(13): " + e.Message);
            }

            //Step 12(14)
            UIMap.CreateNewFilterStep12();
            try
            {
                this.UIMap.CheckValues_Step_12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 12(14): " + e.Message);
            }

            //Step 13(15)
            UIMap.InsertPluttShiftCodeAndchangeFilterView();
            errorList.AddRange(UIMap.SaveDataFromRosterplanCalculations("_step_13"));

            //Step 14(16)
            UIMap.Step_14();

            //Step 15(17)
            UIMap.Step_15();
            errorList.AddRange(UIMap.CheckValues_Step_15());

            //Step 16(18)
            UIMap.Step_16();
            errorList.AddRange(UIMap.CheckValues_Step_16());

            //Step 17(19)
            UIMap.CreateNewFilterStep17();
            errorList.AddRange(UIMap.CheckValues_Step_17());

            //Step 18(20)
            UIMap.Step_18();
            errorList.AddRange(UIMap.CheckValues_Step_18());

            //Step 21
            errorList.AddRange(UIMap.Step_21());

            //Step 22
            errorList.AddRange(UIMap.Step_22());

            //Step 23
            errorList.AddRange(UIMap.Step_23());

            //Step 24
            errorList.AddRange(UIMap.Step_24());

            //Step 25
            errorList.AddRange(UIMap.Step_25());

            this.UIMap.CloseGat();
            errorList.AddRange(UIMap.CompareReportDataFiles_Test020());

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 Beregninger finished OK");
                return;
            }

            UIMap.AssertResults(errorList);
        }
        
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap(TestContext);
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
