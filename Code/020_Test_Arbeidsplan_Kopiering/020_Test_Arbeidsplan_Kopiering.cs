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

namespace _020_Test_Arbeidsplan_Kopiering
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Test_Arbeidsplan_Kopiering
    {



        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Kopiering()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            UIMap.DeleteReportFiles();
            UIMap.StartGat(true);

            //Step 2
            errorList.AddRange(UIMap.SelectRosterPlanAndSelectFilter());

            //Step 3-4
            errorList.AddRange(UIMap.CreateRosterplanCopy(new DateTime(2024, 03, 24), "", "6", true, false));

            //Step 5
            errorList.AddRange(UIMap.SelectRosterPlanCopyAndExportToXls("Kopi av Arbeidsplan.", "_step_5"));

            //Step 6
            errorList.AddRange(UIMap.SelectRoleAssignmentsRosterPlanCopy());

            //Step 7 - 9
            errorList.AddRange(UIMap.EffectuateRoasterplanNextPeriod());

            //Step 10 - 11
            errorList.AddRange(UIMap.CreateHelpPlan());

            //Step 12 - 13
            errorList.AddRange(UIMap.SelectHelpPlanCopyAndSetFilter());

            //Step 14 - 16
            errorList.AddRange(UIMap.CreateRosterplanCopyStep_14(new DateTime(2024, 05, 13), "3", "1", false, true));

            //Step 17
            errorList.AddRange(UIMap.OpenRosterplanSettingsAndUncheckDraft());

            //Step 18 - 19
            errorList.AddRange(UIMap.RemoveJansenShiftAndClosePlan());

            //Step 20 - 23
            errorList.AddRange(UIMap.SelectKalenderplanAndCreateKalenderplanCopy());

            //Step 24 - 25
            errorList.AddRange(UIMap.OpenCalendarplanSettingsAndCheckNightshiftsChecked());

            //Step 26
            errorList.AddRange(UIMap.SelectRoleAssignmentsCalendarplanCopy());

            //Step 27
            errorList.AddRange(UIMap.AddVictoriaToPlan());

            //Step 28
            errorList.AddRange(UIMap.AddVictoriaShiftsAndAbsenceToCalendarplan());

            //Step 29
            errorList.AddRange(UIMap.InsertNewalenderplanValues());

            //Step 30
            errorList.AddRange(UIMap.CreateKalenderplanCopyOfCopy());

            //Step 31
            errorList.AddRange(UIMap.OpenBaseplanAndCreateCopy());

            //Step 32
            errorList.AddRange(UIMap.AddBaseplanCopyValues());

            //Step 33
            errorList.AddRange(UIMap.OpenBaseplanCopy());

            //Step 34-35
            errorList.AddRange(UIMap.OpenMasterlisteAndCreateCopy());

            this.UIMap.CloseGat();
            errorList.AddRange(UIMap.CompareReportDataFiles_Test020());

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 Kopiering finished OK");
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
