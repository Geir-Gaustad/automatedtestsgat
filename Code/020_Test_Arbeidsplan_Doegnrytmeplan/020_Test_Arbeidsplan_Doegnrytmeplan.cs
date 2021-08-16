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


namespace _020_Test_Arbeidsplan_Doegnrytmeplan
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Test_Arbeidsplan_Doegnrytmeplan
    {
        
     
        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Doegnrytmeplan()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            UIMap.DeleteReportFiles();

            UIMap.StartGat(true);

            UIMap.SelectDepartmentTab();
            errorList.AddRange(UIMap.CreateDayRythmplan());


            UIMap.AddTaskToDayRythmplanLayer("RAPPORT1", true);
            UIMap.AddRappport1TaskRequirements();

            UIMap.AddTaskToDayRythmplanLayer("MED 1", true, "1");
            UIMap.AddMed1TaskRequirements();
            UIMap.EditMed1TaskRequirements();

            UIMap.ClickOKAddRequirementsToTask();
            this.UIMap.ClickOKAddLayer();

            //Step 11
            this.UIMap.OpenExcelFromSumTab();
            errorList.AddRange(UIMap.ExportAsExcel("_step_11"));

            //Step 12
            this.UIMap.ClearShowSumDetails();
            this.UIMap.OpenExcelFromSumTab();
            errorList.AddRange(UIMap.ExportAsExcel("_step_12"));

            //Step 13
            this.UIMap.ClickEditLayer();

            //Step 14
            UIMap.AddTaskToDayRythmplanLayer("VISITT", true, "2");
            UIMap.AddVisittTaskRequirements();
            UIMap.AddTaskToDayRythmplanLayer("MED 2", true, "3");
            UIMap.AddMed2TaskRequirements();
            UIMap.AddTaskToDayRythmplanLayer("MED 3", true, "4");
            UIMap.AddMed3TaskRequirements();
            UIMap.AddTaskToDayRythmplanLayer("RAPPORT3", true, "5");
            UIMap.AddRapport3TaskRequirements();

            //Step 15
            this.UIMap.AddRequirementsToVisittTask();
            UIMap.ClickOKAddRequirementsToTask();

            //Step 16
            this.UIMap.DeleteRapport3LayerTask();

            //Step 17
            this.UIMap.ClickOKAddLayer();

            this.UIMap.SelectShowSumDetails();
            this.UIMap.OpenExcelFromSumTab();
            errorList.AddRange(UIMap.ExportAsExcel("_step_17"));

            //Step 18-19
            UIMap.CreateHjelpepleierLayer();

            //Step 20-21
            UIMap.AddTaskToDayRythmplanLayer("STELL1", false);
            this.UIMap.AddStell1TaskRequirements();
            try
            {
                this.UIMap.CheckStell1TaskDuration();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 20-21: " + e.Message);
            }

            //step 22
            UIMap.AddTaskToDayRythmplanLayer("LUNSJ", false, "1");
            this.UIMap.AddLunchTaskRequirements();

            //Step 23
            this.UIMap.ClickOKAddLayer();
            this.UIMap.OpenExcelFromSumTab();
            errorList.AddRange(UIMap.ExportAsExcel("_step_23"));

            //Step 24
            this.UIMap.MoveSykepleierLayerDown();
            try
            {
                this.UIMap.CheckSykelpeierLayerPosition();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 24: " + e.Message);
            }

            //Step 25
            errorList.AddRange(UIMap.EditHjelpepleierLayer());

            //Step 26
            this.UIMap.ClickOKAddLayer();
            try
            {
                this.UIMap.CheckLayerInfo_step_26();
            }
            catch (Exception e)
            {
                errorList.Add("Feil verdier i lag(Step 26): " + e.Message);
            }

            //Step 27
            errorList.AddRange(UIMap.DuplicateSykeplayerLayer("Påske 2022"));
            errorList.AddRange(UIMap.CheckTaskRequirements());
            this.UIMap.OpenExcelFromLayerTask();
            errorList.AddRange(UIMap.ExportAsExcel("_step_27"));

            //Step 28
            errorList.AddRange(UIMap.EditDuplicatedSykeplayerLayer());
            this.UIMap.OpenExcelFromLayerTask();
            errorList.AddRange(UIMap.ExportAsExcel("_step_28"));

            //Step 29
            this.UIMap.DeleteEasterRequirements();
            this.UIMap.DeleteTasksMed2Med3();

            UIMap.AddTaskToDayRythmplanLayer("FROKOST", true, "0", true);
            this.UIMap.AddFrokostTaskRequirements();

            UIMap.AddTaskToDayRythmplanLayer("LUNSJ", true, "1", true);
            this.UIMap.AddNewLunchTaskRequirements();

            UIMap.AddTaskToDayRythmplanLayer("MIDDAG", true, "2", true);
            this.UIMap.AddMiddagTaskRequirements();

            //Step 30
            this.UIMap.EditRequirementsToMiddagTask();        
            UIMap.ClickOKAddRequirementsToTask();
            this.UIMap.ClickOKAddLayer();
            this.UIMap.SelectThirdLayer();
            this.UIMap.OpenExcelFromSumTab();
            errorList.AddRange(UIMap.ExportAsExcel("_step_30"));

            //Step 31-32
            this.UIMap.ClickOKSaveDayRythplan();
            UIMap.SelectDayRythmPlan("2022", false);
            errorList.AddRange(UIMap.DuplicateDayRythmplan("2022 kopi"));

            //Step 33
            UIMap.SelectDayRythmPlan("2022 kopi");

            this.UIMap.UnCheckDraft();
            try
            {
                this.UIMap.CheckOkButtonDisabled();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 33: " + e.Message);
            }
            try
            {
                this.UIMap.CheckRegstatusHasError();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 33_2: " + e.Message);
            }

            //Step 34
            this.UIMap.CheckDraft();
            this.UIMap.ClickOKSaveDayRythplan();
            UIMap.SelectDayRythmPlan("2022 kopi", false);
            this.UIMap.DeleteDayRythmplan();

            //Step 35
            if (UIMap.CheckPlanDeleted("2022 kopi"))
            {
                errorList.Add("Døgnrytmeplan 2022 kopi not deleted");
            }
            else
            { TestContext.WriteLine("Døgnrytmeplan 2022 kopi deleted OK"); }

            UIMap.SelectDayRythmPlan("2022", false, true);
            this.UIMap.SelectFirstLayer();
            this.UIMap.DeleteDayRythmplanLayer();
            this.UIMap.OpenExcelFromSumTab();
            errorList.AddRange(UIMap.ExportAsExcel("_step_35"));

            //Step 36
            this.UIMap.ClickOKSaveDayRythplan();
            UIMap.SelectDayRythmPlan("2022", false);
            this.UIMap.DeleteDayRythmplan();
            if (UIMap.CheckPlanDeleted("2022"))
            {
                errorList.Add("Døgnrytmeplan 2022 not deleted");
            }
            else
            { TestContext.WriteLine("Døgnrytmeplan 2022 deleted OK"); }

            this.UIMap.CloseGat();

            errorList.AddRange(UIMap.CompareReportDataFiles_Test020());

            if (errorList.Count <= 0)
            {
               TestContext.WriteLine("Test 20 Døgnrytmeplan finished OK");
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