using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;

namespace _045_Test_Egengodkjenning_Bytter_Avdelingsbytter_GATW_4850
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_045_Test_Egengodkjenning_Bytter_Avdelingsbytter
    {
        [TestMethod, Timeout(6000000)]
        public void SystemTest_045_A_GetFiles()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions; ;

            UIMap.RestoreDatabase();
            UIMap.GetZipFiles();

            //step 1 - 2 //Gat NOR X.X.X.zip
            UIMap.ExtractGatFiles();
            try
            {
                UIMap.ConfigureMinGatForIIS();
            }
            catch (Exception e)
            {
                errorList.Add("Error configuring MinGat: " + e.Message);
            }
            try
            {
                UIMap.ConfigureIIS();
            }
            catch (Exception e)
            {
                errorList.Add("Error configuring IIS: " + e.Message);
            }
            try
            {
                UIMap.UpgradeGatDB();
            }
            catch (Exception e)
            {
                errorList.Add("Error starting Gat " + e.Message);
            }

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 45 Egengodkjenning Bytter/Avdelingsbytter finished OK");
                return;
            }

            if (errorList.Count > 0)
                UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter C finished OK");
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_045_B_StartTest()

        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions; ;

            errorList.AddRange(UIMap.Step_1());
            errorList.AddRange(UIMap.Step_2());
            errorList.AddRange(UIMap.Step_3());
            errorList.AddRange(UIMap.Step_4());
            errorList.AddRange(UIMap.Step_5());
            errorList.AddRange(UIMap.Step_6());
            errorList.AddRange(UIMap.Step_7());

            if (errorList.Count > 0)
                UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter B finished OK");
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_045_C_Cleanup()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions; ;

            errorList.AddRange(UIMap.Cleanup());

            if (errorList.Count > 0)
                UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter C(Cleanup) finished OK");
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
