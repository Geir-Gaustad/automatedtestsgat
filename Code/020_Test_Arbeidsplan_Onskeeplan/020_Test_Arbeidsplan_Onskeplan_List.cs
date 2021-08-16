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


namespace _020_Test_Arbeidsplan_Onskeeplan
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Test_Arbeidsplan_Onskeplan_Liste
    {
        
       
        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Liste()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            UIMap.StartGat(true);

            //Step 1
            errorList.AddRange(UIMap.Step_1());

            //Step 2
            errorList.AddRange(UIMap.Step_2());

            //Step 3
            errorList.AddRange(UIMap.CreateCalendarPlan_step_3());
            errorList.AddRange(UIMap.CheckAllFiltersAndPlanData_step_3());

            //Step 4
            errorList.AddRange(UIMap.Step_4());

            //Step 5
            errorList.AddRange(UIMap.Step_5());

            //Step 6
            UIMap.AddEmployeesToCalendarPlan();

            //Step 7
            try
            {
                UIMap.Step_7();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            //Step 8
            try
            {
                UIMap.Step_8();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            //Step 9
            try
            {
                UIMap.Step_9();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            //Step 10
            try
            {
                UIMap.Step_10();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            //Step 11-12
            errorList.AddRange(UIMap.Step_11_12());

            //Step 13
            errorList.AddRange(UIMap.Step_13());

            //Step 14
            errorList.AddRange(UIMap.AddEmployeesToRosterPlan());

            //Step 15
            try
            {
                UIMap.Step_15();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            //Step 16-17
            UIMap.Step_16_17();

            //Step 18
            UIMap.AddEmployeesToBasePlan();

            //Step 19-20
            errorList.AddRange(UIMap.Step_19_20());

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

            //Step 26
            errorList.AddRange(UIMap.Step_26());

            //Step 27
            errorList.AddRange(UIMap.Step_27());

            //Step 28
            errorList.AddRange(UIMap.Step_28());

            //step 29
            errorList.AddRange(UIMap.Step_29());

            //step 30
            errorList.AddRange(UIMap.Step_30());

            //step 31
            errorList.AddRange(UIMap.Step_31());

            //step 32
            errorList.AddRange(UIMap.Step_32());

            //step 33-34
            errorList.AddRange(UIMap.Step_33_34());

            //step 35_36
            errorList.AddRange(UIMap.Step_35_36());

            //step 37
            errorList.AddRange(UIMap.Step_37());

            //step 38
            errorList.AddRange(UIMap.Step_38());

            //step 39
            errorList.AddRange(UIMap.Step_39());

            //step 40-41
            errorList.AddRange(UIMap.Step_40_41());
            
            this.UIMap.CloseGat();
                        

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 Arbedsplan/ønskeplan-liste finished OK");
                return;
            }

            UIMap.AssertResults(errorList);


        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

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
                if ((this.map == null))
                {
                    this.map = new UIMap(TestContext);
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
