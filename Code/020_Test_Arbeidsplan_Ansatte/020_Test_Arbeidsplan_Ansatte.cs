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

namespace _020_Test_Arbeidsplan_Ansatte
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Test_Arbeidsplan_Ansatte
    {


        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Ansatte()
        {
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;
            var errorList = new List<string>();

            UIMap.RestoreDatabase();
            UIMap.DeleteReportFiles();

            //Step 1
            UIMap.StartGat(true);

            //Step 2 - 3
            UIMap.CreateRosterplan("Arbeidsplan 2023", new DateTime(2023, 01, 02), new DateTime(2023, 05, 28), "3");

            //Step 4
            UIMap.SelectRosterPlanFilter("full");

            //Step 5-12
            errorList.AddRange(UIMap.AddAndCheckEmpsInEmpWindow());

            //Step 13
            errorList.AddRange(UIMap.AddVakantFromEmpWindow());

            //Step 14
            errorList.AddRange(UIMap.MoveEmpsInListAndAddToPlan());

            //Step 15-16
            errorList.AddRange(UIMap.EditVakantDataInEmpWindow());

            //Step 17
            errorList.AddRange(UIMap.EditEriksroedAndBetongDataInEmpWindow());

            //Step 18
            errorList.AddRange(UIMap.CreateEriksroedWeekendpattern());

            //Step 19
            errorList.AddRange(UIMap.CreateNewUtnePosition());

            //Step 20
            errorList.AddRange(UIMap.AddUtneNewPositionFromEmpWindow());

            //Step 21
            errorList.AddRange(UIMap.CheckUtneNotInEmpList());

            //Step 22
            errorList.AddRange(UIMap.Step_22());

            //Step 23
            errorList.AddRange(UIMap.AddUtneIPNumber2());

            //Step 24 - 26
            errorList.AddRange(UIMap.AddVakantEmpsFromVariosDeps());

            //Step 27
            errorList.AddRange(UIMap.EditVakantPositionsAndMoveToTop());

            //Step 28-29
            errorList.AddRange(UIMap.SaveChangesAndAdd4Employees());

            //Step 30
            errorList.AddRange(UIMap.DeleteRaserPositionLine());

            //Step 31
            errorList.AddRange(UIMap.SetBronsonLineInactive());

            //Step 32
            errorList.AddRange(UIMap.SaveChangesAndCheckAndCheckRosterplanLines());

            //Step 33
            errorList.AddRange(UIMap.AddShiftcodesInRosterplan());

            //Step 34
            errorList.AddRange(UIMap.CreateHelpPlan());

            //Step 35
            errorList.AddRange(UIMap.OpenHelpPlanAndCheckCalculations());

            //Step 36
            errorList.AddRange(UIMap.HelpPlanEditF3());

            //Step 37
            UIMap.CreateCalendarplan("Franzen", new DateTime(2023, 03, 20), "4");

            //Step 38
            UIMap.AddEmployeeToCalendarplan();

            //Step 39
            errorList.AddRange(UIMap.AddShiftsToCalendarplan());

            //Step 40
            errorList.AddRange(UIMap.Open2023AddHansenAndShifts());

            //Step 41
            UIMap.CreateRosterplanInDep5040();

            //Step 42
            errorList.AddRange(UIMap.Add2EmpsAndShiftsToAnnenPlan());

            //Step 43
            errorList.AddRange(UIMap.ChangeDepsOpenHelpplanAndCheckEmps());

            //Step 44
            errorList.AddRange(UIMap.OpenSelectionOptionsSelectAllPlansAndCheckEmpAvailable());

            //Step 45
            errorList.AddRange(UIMap.SelectAllEmpsAndSetF3Caluculation());

            //Step 46
            errorList.AddRange(UIMap.ChangeNewEmployeesValidPeriods());

            //Step 47
            UIMap.CreateCalendarplan("Høst 2023", new DateTime(2023, 09, 04), "10");

            //Step 48
            errorList.AddRange(UIMap.SelectEmployeesToCalendarplanStep_48());

            //Step 49
            errorList.AddRange(UIMap.AddEmployeesToCalendarplanStep_49());

            //Step 50
            UIMap.DeleteRosterplans();

            errorList.AddRange(UIMap.CompareReportDataFiles_Test020());

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 Ansatte finished OK");
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
