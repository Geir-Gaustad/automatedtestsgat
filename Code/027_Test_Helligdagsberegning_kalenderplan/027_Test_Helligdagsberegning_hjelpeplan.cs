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

namespace _027_Test_Helligdagsberegning_kalenderplan
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_027_Helligdagsberegning_kalenderplan
    {
        
      
        [TestMethod, Timeout(6000000)]
        public void SystemTest_027()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            //Step 1
            UIMap.StartGat();
            UIMap.OpenPlan("BEREGNING F3 - F5");

            errorList.AddRange(UIMap.CheckEmployees_step_1());

            //Step 2 - 3
            UIMap.CreateHelpPlan(true);

            //Step 4
            UIMap.ClosePlan();
            UIMap.OpenPlan("Hjelpeplan for BEREGNING F3 - F5.");
            UIMap.SelectHelgeOgHoytidsbergeningFilter();

            errorList.AddRange(UIMap.ChecksStep4());

            //Step 5
            UIMap.ClosePlan();
            UIMap.DeleteHelpPlan();

            //Step 6 - 7
            UIMap.OpenPlan("BEREGNING F3 - F5");
            UIMap.CreateHelpPlan(false);
            UIMap.ClosePlan();
            UIMap.OpenPlan("Hjelpeplan for BEREGNING F3 - F5.");

            //Step 8(6 i ny test)
            UIMap.SelectHelgeOgHoytidsbergeningFilter();
            errorList.AddRange(UIMap.ChecksStep8());

            //Step 9(7 i ny test)      
            UIMap.ChangeCalculationTypeEmployees();
            errorList.AddRange(UIMap.ChecksStep9());

            //Step 10 - 12
            UIMap.ClosePlan();
            UIMap.OpenPlan("BEREGNING F3 - F5");
            UIMap.AddEmployeesToRosterPlan();

            //Step 13 - 15
            UIMap.AddShiftsInRosterplan();

            //Step 16
            UIMap.ChangeRevolvingToDateMild();

            //Step 17
            UIMap.ClosePlan();
            UIMap.OpenPlan("Hjelpeplan for BEREGNING F3 - F5.");
            UIMap.SelectHelgeOgHoytidsbergeningFilter();
            UIMap.AddEmployeesToHelpPlan();

            //Step 18
            UIMap.ChangeCalculationTypeIbrahimovicMildAndLarson();

            //Step 19
            errorList.AddRange(UIMap.ChecksStep19());

            //Step 20
            errorList.AddRange(UIMap.ChangeCalculationTypeLarson());
            
            UIMap.ClosePlan();
            UIMap.CloseGat();

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 27 finished OK");
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
