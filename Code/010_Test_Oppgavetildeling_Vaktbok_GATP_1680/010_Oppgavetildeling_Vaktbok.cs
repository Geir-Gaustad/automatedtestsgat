using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;


namespace _010_Test_Oppgavetildeling_Vaktbok_GATP_1680
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_010_Oppgavetildeling_Vaktbok
    {
        

        [TestMethod, Timeout(6000000)]
        public void SystemTest_010_Oppgavetildeling_Vaktbok()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions; ;

            UIMap.RestoreDatabase();
            UIMap.DeleteReportFiles();

            errorList.AddRange(UIMap.Step_1());
            errorList.AddRange(UIMap.Step_2());
            errorList.AddRange(UIMap.Step_3());
            errorList.AddRange(UIMap.Step_4());
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
            UIMap.Step_22();
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

            UIMap.CloseGat();

            errorList.AddRange(UIMap.CompareReportDataFiles_Test010());

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 10 Oppgavetildeling Vaktbok finished OK");
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
