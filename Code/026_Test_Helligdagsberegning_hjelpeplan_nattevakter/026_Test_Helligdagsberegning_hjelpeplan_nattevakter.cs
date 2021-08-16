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

namespace _026_Test_Helligdagsberegning_hjelpeplan_nattevakter
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_026_Helligdagsberegning_hjelpeplan_nattevakter
    {
        
        
       [TestMethod, Timeout(6000000)]
        public void SystemTest_026()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            var timeBefore = DateTime.Now;

            //Step 1
            UIMap.StartGat();
            errorList.AddRange(UIMap.OpenPlan("AP - NV PÅ STARTDAGEN"));

            //Step 2
            errorList.AddRange(UIMap.CreateHelpPlan());
            UIMap.CloseRosterplan();

            //Step 3
            errorList.AddRange(UIMap.OpenPlan("Hjelpeplan for AP - NV PÅ STARTDAGEN."));
            this.UIMap.SelectHelgeOgHoytidsbergeningFilter();

            //Step 4
            UIMap.CheckHelpplanValues();

            //Step 5
            UIMap.CloseRosterplan();
            errorList.AddRange(UIMap.OpenPlan("AP - NV PÅ STARTDAGEN"));
            UIMap.AddEmployeesToRosterPlan();
            UIMap.AddShiftsInRosterplan();
            UIMap.CloseRosterplan();

            //Step 6
            errorList.AddRange(UIMap.OpenPlan("Hjelpeplan for AP - NV PÅ STARTDAGEN."));

            //Step 7 - 8
            UIMap.AddEmployeesToHelpPlan();
            UIMap.SelectHelgeOgHoytidsbergeningFilter(true);
            errorList.AddRange(UIMap.ChecksStep8());

            //Step 9
            UIMap.AddAbsenceInRosterplan();
            errorList.AddRange(UIMap.ChecksStep9());

            //Step 10
            UIMap.ChangeElmanderF3Calc();
            errorList.AddRange(UIMap.ChecksStep10());

            //Step 11
            UIMap.AddF3ShiftsLarson();
            errorList.AddRange(UIMap.ChecksStep11());
            UIMap.CloseRosterplan();
            UIMap.CloseGat();

            var timeAfter = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved kjøring av Test 26",620, 600));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 26 finished OK");
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

