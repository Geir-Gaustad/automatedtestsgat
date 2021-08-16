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


namespace _060_Test_Security_GatUser_Log_GATP_2465
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_060_Security_GatUser_Log
    {
        [TestMethod, Timeout(6000000)]
        public void SystemTest_060_Security_GatUser_Log()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;

            UIMap.RestoreDatabase();
            UIMap.DeleteReportFiles();

            errorList.AddRange(UIMap.Step_1());
            errorList.AddRange(UIMap.Step_2());

            this.UIMap.CloseGat();
            errorList.AddRange(UIMap.CompareReportDataFiles_Test060());

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 60 Security GatUser Log finished OK");
                return;
            }

            UIMap.AssertResults(errorList);
        }

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
