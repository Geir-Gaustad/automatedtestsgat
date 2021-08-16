using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;


namespace _010_Test_Delte_Oppgaver_Arbeidsplan_GATP_1681
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_010_Test_Delte_Oppgaver_Arbeidsplan
    {
        [TestMethod, Timeout(6000000)]
        public void SystemTest_010_Test_Delte_Oppgaver_Arbeidsplan()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions; ;

            UIMap.RestoreDatabase();

            UIMap.Step_1();
            errorList.AddRange(UIMap.Step_2());
            errorList.AddRange(UIMap.Step_3());
            errorList.AddRange(UIMap.Step_4());
            UIMap.Step_5();
            UIMap.Step_6();
            errorList.AddRange(UIMap.Step_7());
            errorList.AddRange(UIMap.Step_8());
            UIMap.Step_9();
            errorList.AddRange(UIMap.Step_10());
            UIMap.Step_11();
            errorList.AddRange(UIMap.Step_12());
            errorList.AddRange(UIMap.Step_13());
            errorList.AddRange(UIMap.Step_14());
            errorList.AddRange(UIMap.Step_15());
            errorList.AddRange(UIMap.Step_16());
            errorList.AddRange(UIMap.Step_17());
            errorList.AddRange(UIMap.Step_18());
            errorList.AddRange(UIMap.Step_19());
            UIMap.Step_20();
            UIMap.Step_21();
            errorList.AddRange(UIMap.Step_22());
            errorList.AddRange(UIMap.Step_23());
            UIMap.Step_24();
            errorList.AddRange(UIMap.Step_25());
            errorList.AddRange(UIMap.Step_26());
            UIMap.ShutDown();

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 10 Oppgavetildeling arbeidsplan finished OK");
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
