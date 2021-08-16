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

namespace _020_Test_Arbeidsplan_Redigering_GATP_740
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Arbeidsplan_Redigering
    {
        
        
        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Redigering()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            //Step 1
            UIMap.StartGat(true, true);

            //Step 2
            UIMap.CreateRosterplan("Redigering AP", new DateTime(2024, 01, 01), "3", new DateTime(2024, 03, 03));

            //Step 3
            errorList.AddRange(UIMap.AddEmployeesToRosterPlan());

            //Step 4
            errorList.AddRange(UIMap.CreateWeekendPattern());

            errorList.AddRange(UIMap.Step_5());

            errorList.AddRange(UIMap.Step_6());

            errorList.AddRange(UIMap.Step_7());

            errorList.AddRange(UIMap.Step_8());

            errorList.AddRange(UIMap.Step_9());

            errorList.AddRange(UIMap.Step_10());

            errorList.AddRange(UIMap.Step_11());

            errorList.AddRange(UIMap.Step_12());

            errorList.AddRange(UIMap.Step_13());

            errorList.AddRange(UIMap.Step_14());

            errorList.AddRange(UIMap.Step_15());

            errorList.AddRange(UIMap.Step_16());

            errorList.AddRange(UIMap.Step_17());

            errorList.AddRange(UIMap.Step_18());

            errorList.AddRange(UIMap.Step_19());

            errorList.AddRange(UIMap.Step_20());

            errorList.AddRange(UIMap.Step_21());

            errorList.AddRange(UIMap.Step_22());

            errorList.AddRange(UIMap.Step_23());

            errorList.AddRange(UIMap.Step_24());

            errorList.AddRange(UIMap.Step_25());

            errorList.AddRange(UIMap.Step_26());

            errorList.AddRange(UIMap.Step_27());

            errorList.AddRange(UIMap.Step_28());

            errorList.AddRange(UIMap.Step_29());

            errorList.AddRange(UIMap.Step_30());

            errorList.AddRange(UIMap.Step_31());

            errorList.AddRange(UIMap.Step_32());

            errorList.AddRange(UIMap.Step_33());

            errorList.AddRange(UIMap.Step_34());

            errorList.AddRange(UIMap.Step_35());

            errorList.AddRange(UIMap.Step_36());

            errorList.AddRange(UIMap.Step_37());

            errorList.AddRange(UIMap.Step_38());

            errorList.AddRange(UIMap.Step_39());

            errorList.AddRange(UIMap.Step_40());

            errorList.AddRange(UIMap.Step_41());

            errorList.AddRange(UIMap.Step_42());

            errorList.AddRange(UIMap.Step_43());

            errorList.AddRange(UIMap.Step_44());

            errorList.AddRange(UIMap.Step_45());

            errorList.AddRange(UIMap.Step_46());

            errorList.AddRange(UIMap.Step_47());

            errorList.AddRange(UIMap.Step_48());

            errorList.AddRange(UIMap.Step_49());

            errorList.AddRange(UIMap.Step_50());

            errorList.AddRange(UIMap.Step_51());

            errorList.AddRange(UIMap.Step_52());

            errorList.AddRange(UIMap.Step_53());

            errorList.AddRange(UIMap.Step_54());


            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 Redigering finished OK");
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
