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


namespace _020_Test_Arbeidsplan_Instillinger_Fane_Plan
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Arbeidsplan_Instillinger_Fane_Plan
    {
        

        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Arbeidsplan_Instillinger_Fane_Plan()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();

            errorList.AddRange(UIMap.Step_1());
            errorList.AddRange(UIMap.Step_2());
            errorList.AddRange(UIMap.Step_3());
            errorList.AddRange(UIMap.Step_4());
            UIMap.Step_5();
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
                                             
            UIMap.CloseGat();

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 Arbeidsplan Instillinger Fane Plan finished OK");
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
