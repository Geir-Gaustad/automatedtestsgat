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

namespace _029_Test_Timelønnsberegning_Arbeidsplan
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_029_Timelønnsberegning_Arbeidsplan
    {


        [TestMethod, Timeout(6000000)]
        public void SystemTest_029()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            var timeBefore = DateTime.Now;

            //1
            UIMap.StartGat();

            //2
            errorList.AddRange(UIMap.OpenPlan("TIMELØNN", "2"));

            //3 - 4
            errorList.AddRange(UIMap.EffectuateFullplan(true, new DateTime(2024, 12, 16), new DateTime(2024, 12, 16), 4));

            //5
            errorList.AddRange(UIMap.DeleteHolAbsence("5"));
            errorList.AddRange(UIMap.OpenPlan("TIMELØNN", "5"));

            //6            
            errorList.AddRange(UIMap.DeleteEffectuationFullplan("6"));

            //7
            errorList.AddRange(UIMap.EffectuateFullplan(true, new DateTime(2024, 12, 26), new DateTime(2024, 12, 26), 7));

            //8
            errorList.AddRange(UIMap.DeleteEffectuationFullplan("8"));

            //9
            UIMap.CloseRosterplan();
            errorList.AddRange(UIMap.OpenPlan("Hjelpeplan for TIMELØNN.", "9"));

            //10 - 12
            errorList.AddRange(UIMap.EffectuateRoasterplanNextPeriod(new DateTime(2024, 12, 16), new DateTime(2024, 12, 16), 12));

            //13
            errorList.AddRange(UIMap.DeleteEffectuationFullplan("13"));

            //14
            errorList.AddRange(UIMap.EffectuateRoasterplanNextPeriod(new DateTime(2024, 12, 24), new DateTime(2024, 12, 24), 14));

            //15
            errorList.AddRange(UIMap.DeleteEffectuationFullplan("15"));

            //16
            errorList.AddRange(UIMap.EffectuateRoasterplanNextPeriod(new DateTime(2024, 12, 26), new DateTime(2024, 12, 26), 16));

            //17
            errorList.AddRange(UIMap.DeleteEffectuationFullplan("17"));

            //18
            this.UIMap.ChangeEmpSettingsBett_Step18();

            //19
            errorList.AddRange(UIMap.EffectuateEmpBett_Step19(new DateTime(2024, 12, 27), new DateTime(2024, 12, 27), 19));

            //20
            errorList.AddRange(UIMap.DeleteEffectuationFullplan("20"));

            //21
            UIMap.CloseRosterplan();
            errorList.AddRange(UIMap.OpenPlan("Påske 2024", "21"));
            errorList.AddRange(UIMap.EffectuateFullplan(true, new DateTime(2024, 03, 27), new DateTime(2024, 03, 27), 21));
            
            //22
            UIMap.CloseRosterplan();
            UIMap.CloseGat();

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 29 finished OK");
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
