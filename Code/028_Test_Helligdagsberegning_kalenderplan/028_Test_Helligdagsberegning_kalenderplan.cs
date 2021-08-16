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

namespace _028_Test_Helligdagsberegning_kalenderplan
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_028_Helligdagsberegning_hjelpeplan
    {


        [TestMethod, Timeout(6000000)]
        public void SystemTest_028()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            var timeBefore = DateTime.Now;

            //Step 1
            UIMap.StartGat();
            errorList.AddRange(UIMap.CreateKalenderPlan());

            //Step 2
            UIMap.AddEmployeesToPlan();
            errorList.AddRange(UIMap.ChecksStep2());

            //Step 3
            UIMap.SelectHelgeOgHoytidsbergeningFilter();
            errorList.AddRange(UIMap.ChecksStep2());

            //Step 4 - 7
            errorList.AddRange(UIMap.AddAnderssonShifts());

            //Step 5
            errorList.AddRange(UIMap.AddDahlinShifts());

            //Step 6 - 7
            errorList.AddRange(UIMap.EditDahlinValidPeriods());

            //Step 8
            errorList.AddRange(UIMap.AddRavelliShifts());

            //step 9
            errorList.AddRange(UIMap.AddRavelliShifts2());

            //Step 10
            errorList.AddRange(UIMap.DeleteRavelliShifts());

            //Step 11
            errorList.AddRange(UIMap.EditPlanSettings());

            //Step 12
            errorList.AddRange(UIMap.EditRavelliShifts());

            //Step 13
            errorList.AddRange(UIMap.DeleteAndInsertRavelliShifts());

            //Step 14
            errorList.AddRange(UIMap.DeleteAndInsertRavelliShifts2());

            UIMap.CloseRosterplan();
            UIMap.CloseGat();

            var timeAfter = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved kjøring av Test 28", 620, 500));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 28 finished OK");
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
