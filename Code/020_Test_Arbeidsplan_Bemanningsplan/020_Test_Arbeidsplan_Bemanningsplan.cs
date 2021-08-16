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

namespace _020_Test_Arbeidsplan_Bemanningsplan
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Test_Arbeidsplan_Bemanningsplan
    {
        
      
        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Bemanningsplan()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            UIMap.DeleteReportFiles();

            UIMap.StartGat(true);

            //Step 1
            UIMap.SelectDepartmentTab();

            //Step 2-5
            UIMap.CreateStaffingplanQ1();

            //Step 6
            this.UIMap.SelectEnkelMedDetaljer();
            this.UIMap.OpenExcelFromSumTab();
            errorList.AddRange(UIMap.ExportAsExcel("_step_6"));

            //Step 7-8
            UIMap.CreateLederLayer();
            this.UIMap.OpenExcelFromSumTab();
            errorList.AddRange(UIMap.ExportAsExcel("_step_8"));

            //Step 9
            this.UIMap.SelectHoursYearTab();
            try
            {
                this.UIMap.CheckValues_Step_9();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 9: " + e.Message);
            }

            //Step 10
            this.UIMap.ClickOkSaveStaffingPlan();

            //Step 11-14
            UIMap.CreateStaffingplanNatt();
            try
            {
                UIMap.CheckPlanIsValid("step_14");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 14: " + e.Message);
            }

            //Step 15
            UIMap.SelectStaffingPlan("Nattevakter SPL", true);
            this.UIMap.UncheckDraft();
            this.UIMap.ClickOkSaveStaffingPlan();
            try
            {
                UIMap.CheckPlanIsValid("step_15");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 15: " + e.Message);
            }

            //Step 16
            UIMap.SelectStaffingPlan("Q1 2022", true);

            //Step 17
            this.UIMap.SelectFirstLayer();
            this.UIMap.ClickEditLayer();
            this.UIMap.AddRequirementsToD1();

            //Step 18
            this.UIMap.AddRequirementsToDH();

            //Step 19
            this.UIMap.AddRequirementsToA1();

            //Step 20
            UIMap.ClickOkSaveStaffingLayer();

            //Step 21
            this.UIMap.SelectSecondLayer();
            this.UIMap.ClickEditLayer();
            this.UIMap.AddRequirementsToL();

            //Step 22
            UIMap.ClickOkSaveStaffingLayer();

            //Step 23
            UIMap.AddNewStaffingLayer();

            //Step 24     
            this.UIMap.AddVinterferieLayer();

            //Step 25
            UIMap.AddVinterferieShiftCodes();
            this.UIMap.AddVinterferieLayerManning();
            try
            {
                UIMap.CheckValues_Step_25();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 25: " + e.Message);
            }

            //Step 26
            UIMap.ClickOkSaveStaffingLayer();

            this.UIMap.SelectEnkelMedDetaljer();
            this.UIMap.OpenExcelFromSumTab();
            errorList.AddRange(UIMap.ExportAsExcel("_step_26"));

            //Step 27
            errorList.AddRange(UIMap.CheckValues_Step_27());

            //Step 28
            this.UIMap.ClickOkSaveStaffingPlan();

            //Step 29-30
            errorList.AddRange(UIMap.DuplicateStaffingplan());
            try
            {
                UIMap.CheckStaffingPlanIsDraft();
                this.UIMap.CheckStaffingPlans();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 29-30: " + e.Message);
            }

            UIMap.CheckPlanIsValid("step_30");

            //Step 31
            UIMap.DeleteStaffingplan("Duplikat");

            //Step 32
            UIMap.DeleteStaffingplan("Nattevakter SPL");
            UIMap.DeleteStaffingplan("Q1 2022");

            errorList.AddRange(UIMap.CheckStaffingplansDeleted());

            this.UIMap.CloseGat();

            errorList.AddRange(UIMap.CompareReportDataFiles_Test020());

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 Bemanningsplan finished OK");
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
