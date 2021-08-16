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


namespace _020_Test_Arbeidsplan_Filter_og_Visning_GATP_750
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Filter_og_Visning
    {

        

        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Filter_og_Visning()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.DeleteReportFiles();

            UIMap.RestoreDatabase();
            //Step 1
            errorList.AddRange(UIMap.Step_1());

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
            errorList.AddRange(UIMap.Step_7());

            //Step 8
            errorList.AddRange(UIMap.Step_8());

            //Step 9
            errorList.AddRange(UIMap.Step_9());

            //Step 10
            errorList.AddRange(UIMap.Step_10());

            //Step 11
            errorList.AddRange(UIMap.Step_11());

            //Step 12
            errorList.AddRange(UIMap.Step_12());

            //Step 13
            errorList.AddRange(UIMap.Step_13());

            //Step 14
            errorList.AddRange(UIMap.Step_14());

            //Step 15
            errorList.AddRange(UIMap.Step_15());

            //Step 16
            errorList.AddRange(UIMap.Step_16());

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

            //Step 25
            errorList.AddRange(UIMap.Step_25());

            //Step 26
            errorList.AddRange(UIMap.Step_26());

            //Step 27
            errorList.AddRange(UIMap.Step_27());

            //Step 28
            errorList.AddRange(UIMap.Step_28());

            //Step 29
            errorList.AddRange(UIMap.Step_29());

            //Step 30
            errorList.AddRange(UIMap.Step_30());

            //Step 31
            errorList.AddRange(UIMap.Step_31());

            //Step 32
            errorList.AddRange(UIMap.Step_32());

            //Step 33
            errorList.AddRange(UIMap.Step_33());

            //Step 34
            errorList.AddRange(UIMap.Step_34());

            //Step 35
            errorList.AddRange(UIMap.Step_35());

            //Step 36
            errorList.AddRange(UIMap.Step_36());

            //Step 37
            errorList.AddRange(UIMap.Step_37());

            //Step 38
            errorList.AddRange(UIMap.Step_38());

            //Step 39
            errorList.AddRange(UIMap.Step_39());

            //Step 40
            errorList.AddRange(UIMap.Step_40());

            //Step 41
            errorList.AddRange(UIMap.Step_41());

            //Step 42
            errorList.AddRange(UIMap.Step_42());

            //Step 43
            errorList.AddRange(UIMap.Step_43());

            //Step 44
            errorList.AddRange(UIMap.Step_44());

            //Step 45
            errorList.AddRange(UIMap.Step_45());

            //Step 46
            errorList.AddRange(UIMap.Step_46());

            //Step 47
            errorList.AddRange(UIMap.Step_47());

            //Step 48
            errorList.AddRange(UIMap.Step_48());

            //Step 49
            errorList.AddRange(UIMap.Step_49());

            //Step 50
            errorList.AddRange(UIMap.Step_50());
            errorList.AddRange(UIMap.Close());

            errorList.AddRange(UIMap.CompareReportDataFiles_Test020());

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 Filter og Visning finished OK");
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
