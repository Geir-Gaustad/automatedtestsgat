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


namespace _020_Test_Arbeidsplan_Arbeidsplanoppsett_Plan
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Test_Arbeidsplan_Arbeidsplanoppsett_Plan
    {
        
      
        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Arbeidsplanoppsett()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            UIMap.StartGat(true);

            //Step 2
            UIMap.InsertSettingValuesStep2();

            //Step 3
            UIMap.InsertSettingValuesStep3();

            //Step 4
            UIMap.InsertSettingValuesStep4();

            //Step 5
            errorList.AddRange(UIMap.CheckDepSettingValuesStep5());

            //Step 6
            errorList.AddRange(UIMap.CheckDepSettingValuesStep6());

            //Step 7
            errorList.AddRange(UIMap.CheckDepSettingValuesStep7());

            //Step 8
            UIMap.Step8();

            //Step 9
            UIMap.Step9_CreateRosterplan();

            //Step 10
            errorList.AddRange(UIMap.Step10_AddEmployees());

            //Step 11
            //Bug i nedtrekksmeny(Implemented workaround)
            errorList.AddRange(UIMap.Step11_CheckSetupWindow());

            //Step 12
            errorList.AddRange(UIMap.Step12_CheckEmpEnTurnusSettings());

            //Step 13
            //Bug i nedtrekksmeny(Implemented workaround)
            errorList.AddRange(UIMap.Step13_CheckEmpFemAdmSettings());

            //Step 14
            //Bug i nedtrekksmeny(Implemented workaround)
            errorList.AddRange(UIMap.Step14_CheckEmpTiTurnusSettings());

            //Step 15
            errorList.AddRange(UIMap.Step15_AddShifts());

            //Step 16
            errorList.AddRange(UIMap.Step16_CheckErrorAndWarnings());

            //Step 17
            errorList.AddRange(UIMap.Step17_CheckErrorAndWarnings());

            //Step 18
            errorList.AddRange(UIMap.Step18_CheckErrorAndWarnings());

            //Step19
            errorList.AddRange(UIMap.Step19());

            //Step20
            errorList.AddRange(UIMap.Step20());

            //Step 21
            errorList.AddRange(UIMap.Step21_CheckErrorAndWarnings());

            //Step 22
            errorList.AddRange(UIMap.Step22_CheckErrorAndWarnings());

            //Step 23
            this.UIMap.OpenEmpTiLayoutWindow();

            //Step 24
            UIMap.InsertSettingValuesStep24();
            errorList.AddRange(UIMap.Step24_CheckErrorAndWarnings());

            //Step 25
            UIMap.CloseRosterplan();
            UIMap.SelectFromAdministration("globalt +arbeidsplanoppsett globalt");
            UIMap.EditGlobalSettingValuesStep25();

            //Step 26
            UIMap.Step26_AddEmployees();
            errorList.AddRange(UIMap.Step26_CheckSetupWindow());

            //Step 27
            errorList.AddRange(UIMap.Step27_CheckSetupWindow());

            //Step 28
            errorList.AddRange(UIMap.Step28_AddShifts());

            //Step 29
            errorList.AddRange(UIMap.Step29_CheckErrorAndWarnings());

            //Step 30
            errorList.AddRange(UIMap.Step30_CheckErrorAndWarnings());

            //Step 31
            UIMap.CloseRosterplan();
            this.UIMap.CloseGat();
            
            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 Arbeidsplan_Arbeidsplanoppsett_Plan finished OK");
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
