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

namespace _022_Ytelsestest_Kalenderplan
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_022_Ytelsestest_Kalenderplan
    {
        
        
        [TestMethod, Timeout(6000000)]
        public void SystemTest_022_A_Chapter1()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeBefore = DateTime.Now;

            try
            {
                UIMap.RestoreDatabase(true);

                //kopier .ini
                UIMap.ReplaceInifile(false);

                UIMap.StartGat(true);
                errorList.AddRange(UIMap.OpenPlan("YTELSE Kalenderplan", "2", 60, 00));
                //lowOpenEffWindow, highOpenEffWindow, lowEffectuation, highEffectuation, lowOpenPlan, highOpenPlan
                errorList.AddRange(UIMap.EffectuateFullplan("3", 15, 30, 70, 90, 25, 35));

                Playback.Wait(1000);
                UIMap.CloseRosterplan();
                Playback.Wait(1000);
                this.UIMap.CloseGat();

                //kopier .ini
                UIMap.ReplaceInifile(true);

                UIMap.StartGat(false);
                errorList.AddRange(UIMap.OpenPlan("YTELSE Kalenderplan", "15", 60, 75));

                //lowWindowReady, highWindowReady, lowDeleteEff, highDeleteEff
                errorList.AddRange(UIMap.DeleteEffectuationFullplan("16", 20, 30, 50, 70, true));

                //step 17-20
                Playback.Wait(2000);
                errorList.AddRange(UIMap.AddEmployeesToPlan("17-20"));

                //step 21 - 24          
                errorList.AddRange(UIMap.AddShiftsToEmployeesInPlan("21-24"));

                Playback.Wait(1000);
                UIMap.CloseRosterplan();
                Playback.Wait(1000);
                this.UIMap.CloseGat();

                var timeAfter = DateTime.Now;
                errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved kjøring av Test 22_chap_1", 1000, 1180));
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }
            finally
            {
                //kopier .ini
                UIMap.ReplaceInifile(false, true);
            }

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 22_1 finished OK");
                return;
            }

            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_022_B_Chapter2()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeBefore = DateTime.Now;

            try
            {
                UIMap.RestoreDatabase(false);

                //Step 1
                UIMap.StartGat(true, false);

                //Step 2
                errorList.AddRange(UIMap.OpenPlan("YTELSE 2023","2, del 2.", 85, 115));

                //step 3 - 5
                Playback.Wait(1000);
                errorList.AddRange(UIMap.AddEmployeesToPlan2("3-5 del 2."));

                //step 6        
                errorList.AddRange(UIMap.AddShiftsToEmployeesInPlan2("6, del 2."));

                //Step 7
                errorList.AddRange(UIMap.ApproveChester("7, del 2."));

                //Step 8 - 11
                //lowOpenEffWindow, highOpenEffWindow, lowEffectuation, highEffectuation, lowOpenPlan, highOpenPlan
                errorList.AddRange(UIMap.EffectuateFullplan("8-11, del 2.", 15, 25, 30, 45, 30, 45, new DateTime(2023, 01, 02), new DateTime(2023, 04, 30)));

                //Step 12 - 15
                //lowWindowReady, highWindowReady, lowDeleteEff, highDeleteEff
                errorList.AddRange(UIMap.DeleteEffectuationFullplan("12-15, del 2.",20, 30, 35, 60, false));

                Playback.Wait(1000);
                UIMap.CloseRosterplan();
                Playback.Wait(1000);
                this.UIMap.CloseGat();

                var timeAfter = DateTime.Now;
                errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved kjøring av Test 22_chap_2", 650, 760));
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 22_2 finished OK");
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
