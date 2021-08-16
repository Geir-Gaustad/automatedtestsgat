using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _010_Test_Vaktbok_Oppgavetildelingsvindu_GATP_1682
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_010_Vaktbok_Oppgavetildelingsvindu
    {    
        

        [TestMethod, Timeout(6000000)]
        public void SystemTest_010_Vaktbok_Oppgavetildelingsvindu()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();

            UIMap.Step_1();
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
            errorList.AddRange(UIMap.Step_43_44());
            errorList.AddRange(UIMap.Step_45());
            errorList.AddRange(UIMap.Step_46());
            errorList.AddRange(UIMap.Step_47());
            errorList.AddRange(UIMap.Step_48());
            errorList.AddRange(UIMap.Step_49());
            errorList.AddRange(UIMap.Step_50());

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 10 Oppgavetildelingsvindu finished OK");
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
