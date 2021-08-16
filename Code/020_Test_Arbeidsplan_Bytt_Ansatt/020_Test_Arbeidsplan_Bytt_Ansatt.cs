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

namespace _020_Test_Arbeidsplan_Bytt_Ansatt
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Arbeidsplan_Bytt_Ansatt
    {
        

        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Bytt_Ansatt()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            //Step 1
            UIMap.Step_1();

            TestContext.WriteLine("Minnebruk etter oppstart av Gat: " + UIMap.ReadPhysicalMemoryUsage());
            TestContext.WriteLine("PagedMemorySize etter oppstart av Gat: " + UIMap.ReadPagedMemorySize64());

            //Step 2
            errorList.AddRange(UIMap.Step_2());

            //Step 3
            errorList.AddRange(UIMap.Step_3());

            //Step 4
            errorList.AddRange(UIMap.Step_4());

            //Step 5
            errorList.AddRange(UIMap.Step_5());

            //Step 6
            errorList.AddRange(UIMap.Step_6());

            //Step 7
            UIMap.Step_7();

            //Step 8
            errorList.AddRange(UIMap.Step_8());

            //Step 9
            errorList.AddRange(UIMap.Step_9());

            //Step 10
            errorList.AddRange(UIMap.Step_10());

            //Step 11
            errorList.AddRange(UIMap.Step_11());

            //Step 12
            UIMap.Step_12();

            //Step 13
            errorList.AddRange(UIMap.Step_13());

            //Step 14
            errorList.AddRange(UIMap.Step_14());

            //Step 15
            errorList.AddRange(UIMap.Step_15());

            //Step 16
            UIMap.Step_16();

            //Step 17
            errorList.AddRange(UIMap.Step_17());

            //Step 18
            errorList.AddRange(UIMap.Step_18());

            //Step 19
            errorList.AddRange(UIMap.Step_19());

            //Step 20
            errorList.AddRange(UIMap.Step_20());

            //Step 21
            errorList.AddRange(UIMap.Step_21());

            //Step 22
            errorList.AddRange(UIMap.Step_22());

            //Step 23
            errorList.AddRange(UIMap.Step_23());

            //Step 24
            errorList.AddRange(UIMap.Step_24());

            UIMap.Step_25();

            errorList.AddRange(UIMap.Step_26_27());

            UIMap.Step_28();

            errorList.AddRange(UIMap.Step_29());

            UIMap.Step_30();

            errorList.AddRange(UIMap.Step_31());

            errorList.AddRange(UIMap.Step_32());

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 bytt ansatt finished OK");
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
