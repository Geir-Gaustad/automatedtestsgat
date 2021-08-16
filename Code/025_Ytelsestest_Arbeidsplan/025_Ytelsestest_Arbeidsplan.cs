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

namespace _025_Ytelsestest_Arbeidsplan
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_025_Ytelsestest_Arbeidsplan
    {
        
       
        [TestMethod, Timeout(6000000)]
        public void SystemTest_025()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;
            
            UIMap.RestoreDatabase();
            var timeBefore = DateTime.Now;

            try
            {
                //kopier .ini
                UIMap.ReplaceInifile(false);

                //1
                UIMap.StartGat();

                //2
                errorList.AddRange(UIMap.OpenPlan("YTELSE - Arbeidsplan", "2", 35, 60));

                //3 - 7
                //lowOpenEffWindow, highOpenEffWindow, lowEffectuation, highEffectuation, lowOpenPlan, highOpenPlan
                errorList.AddRange(UIMap.EffectuateFullplan(true, false, "3-7", 15, 40, 75, 100, 3, 8));

                //8 - 12
                //lowWindowReady, highWindowReady, lowDeleteEff, highDeleteEff, lowOpenPlan, highOpenPlan
                errorList.AddRange(UIMap.DeleteEffectuationFullplan("8-12", 20, 30, 20, 30, 3, 6));

                //13 - 17
                errorList.AddRange(UIMap.AddAndConfigureEmployeesToPlan("13-17"));

                //18
                UIMap.EditViewDateForPlan();

                //19-22
                UIMap.XCloseRosterplan();
                UIMap.OpenPlan("YTELSE - Arbeidsplan","19-22", 35, 60);
                errorList.AddRange(UIMap.EditRosterplanShifts("22"));

                //23 - 25
                errorList.AddRange(UIMap.CreateHelpPlan("23-25"));

                //26
                UIMap.XCloseRosterplan();

                //27
                errorList.AddRange(UIMap.OpenPlan("Hjelpeplan for YTELSE - Arbeidsplan.", "27", 25, 35));

                //28
                UIMap.EditHelpPlanShifts();

                //29 - 30
                errorList.AddRange(UIMap.OpenPlan("YTELSE - Arbeidsplan", "29-30", 20, 30));
                UIMap.EditValidToDateForPlan();

                //31 - 35
                //lowOpenEffWindow, highOpenEffWindow, lowEffectuation, highEffectuation, lowOpenPlan, highOpenPlan
                errorList.AddRange(UIMap.EffectuateFullplan(false, true, "31-35", 15, 40, 85, 100, 3, 8));
                errorList.AddRange(UIMap.OpenPlan("Hjelpeplan for YTELSE - Arbeidsplan.", "35", 25, 35));

                //36 - 38
                //lowOpenEffWindow, highOpenEffWindow, lowEffectuation, highEffectuation, lowOpenPlan, highOpenPlan
                errorList.AddRange(UIMap.EffectuateFullplan(true, true, "36-38", 15, 40, 15, 25, 15, 25, false, true));
                errorList.AddRange(UIMap.OpenPlan("YTELSE - Arbeidsplan", "38", 20, 30));

                //39 - 42
                //lowWindowReady, highWindowReady, lowDeleteEff, highDeleteEff, lowOpenPlan, highOpenPlan
                errorList.AddRange(UIMap.DeleteEffectuationFullplan("39-42", 20, 30, 20, 30, 3, 8));

                //43 
                UIMap.XCloseRosterplan();

                this.UIMap.CloseGat();

                var timeAfter = DateTime.Now;
                errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved kjøring av Test 25", 1450, 1550));

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
                TestContext.WriteLine("Test 25 finished OK");
                return;
            }

            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_025_MemoryCheck()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions; ;

            UIMap.RestoreDatabase(false);

            try
            {
                UIMap.StartGat();

                var physicalMemoryStart = Convert.ToInt32(UIMap.ReadPhysicalMemoryUsage(true));
                var pagedMemoryStart = Convert.ToInt32(UIMap.ReadPagedMemorySize64(true));

                TestContext.WriteLine("Minnebruk etter oppstart av Gat: " + physicalMemoryStart.ToString() + " MB");
                TestContext.WriteLine("PagedMemorySize etter oppstart av Gat: " + pagedMemoryStart.ToString() + " MB");

                errorList.AddRange(UIMap.OpenCloseRosterplan(50, 5));

                var physicalMemoryEnd = Convert.ToInt32(UIMap.ReadPhysicalMemoryUsage(true));
                var pagedMemoryEnd = Convert.ToInt32(UIMap.ReadPagedMemorySize64(true));

                var physicalMemoryDev = physicalMemoryEnd - physicalMemoryStart;
                var pagedMemoryDev = pagedMemoryEnd - pagedMemoryStart;

                TestContext.WriteLine("Økning i minnebruk = " + physicalMemoryDev.ToString() + " MB");
                TestContext.WriteLine("Økning i PagedMemorySize = " + pagedMemoryDev.ToString() + " MB");

                if (physicalMemoryEnd > 800)
                    errorList.Add("Use of memory is out of limit: " + physicalMemoryEnd);

                if (pagedMemoryEnd > 800)
                    errorList.Add("Use of Pagedmemory is out of limit: " + pagedMemoryEnd);

                if (physicalMemoryDev > 400)
                    errorList.Add("Increase of Memory is out of limit: " + physicalMemoryDev);

                if (pagedMemoryDev > 450)
                    errorList.Add("Increase of Pagedmemory is out of limit: " + pagedMemoryDev);

                UIMap.CloseGat();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 25 Memory finished OK");
                return;
            }

            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_025_Minnebruk_Oppdekning()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions; ;

            UIMap.RestoreDatabase(false);

            try
            {
                UIMap.StartGat(false);

                var physicalMemoryStart = Convert.ToInt32(UIMap.ReadPhysicalMemoryUsage(true));
                var pagedMemoryStart = Convert.ToInt32(UIMap.ReadPagedMemorySize64(true));

                TestContext.WriteLine("Minnebruk etter oppstart av Gat: " + physicalMemoryStart.ToString() + " MB");
                TestContext.WriteLine("PagedMemorySize etter oppstart av Gat: " + pagedMemoryStart.ToString() + " MB");

                errorList.AddRange(UIMap.OpenCloseCoverExtraShift(100, 10));

                var physicalMemoryEnd = Convert.ToInt32(UIMap.ReadPhysicalMemoryUsage(true));
                var pagedMemoryEnd = Convert.ToInt32(UIMap.ReadPagedMemorySize64(true));

                var physicalMemoryDev = physicalMemoryEnd - physicalMemoryStart;
                var pagedMemoryDev = pagedMemoryEnd - pagedMemoryStart;

                TestContext.WriteLine("Økning i minnebruk = " + physicalMemoryDev.ToString() + " MB");
                TestContext.WriteLine("Økning i PagedMemorySize = " + pagedMemoryDev.ToString() + " MB");

                if (physicalMemoryEnd > 800)
                    errorList.Add("Use of memory is out of limit: " + physicalMemoryEnd);

                if (pagedMemoryEnd > 800)
                    errorList.Add("Use of Pagedmemory is out of limit: " + pagedMemoryEnd);

                if (physicalMemoryDev > 400)
                    errorList.Add("Increase of Memory is out of limit: " + physicalMemoryDev);

                if (pagedMemoryDev > 450)
                    errorList.Add("Increase of Pagedmemory is out of limit: " + pagedMemoryDev);

                UIMap.CloseGat();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 25 Memory finished OK");
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
