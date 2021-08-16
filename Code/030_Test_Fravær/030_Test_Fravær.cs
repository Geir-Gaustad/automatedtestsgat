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

namespace _030_Test_Fravær
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest_030
    {
        private const int _DelayBetweenActions = 600;
        private List<string> _ErrorList = new List<string>();
      
        [TestMethod, Timeout(TestTimeout.Infinite)]
        public void SystemTest_030()
        {
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = _DelayBetweenActions;

            try
            {
                UIMap.RestoreDatabase();
                UIMap.KillGatProcess();
                UIMap.DeleteReportFiles();
                UIMap.LaunchAndUpdateGat();

                this.UIMap.ClickWeekViewButton();
                try
                {
                    this.UIMap.GoToShiftDate(new DateTime(2016, 4, 18));
                }
                catch(Exception e)
                {
                    _ErrorList.Add(e.Message);
                }

                //step 2
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_2", true, UIMap.OpenReport95AndSaveToExcelParams.UI1804, UIMap.OpenReport95AndSaveToExcelParams.UI2404));

                //step 5
                Playback.Wait(1000);
                this.UIMap.AddEmpAndEffectuate(true);
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_5", false));
                this.UIMap.GoToShiftBookTab();

                //step 8
                Playback.Wait(1000);
                this.UIMap.CoverShiftsWithEvenE(UIMap.CoverTuesdayVakantShiftEvenEParams.UIPceDate19042016, false);
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_8", false));
                this.UIMap.GoToShiftBookTab();

                //step 11
                Playback.Wait(1000);
                this.UIMap.GenerateFreeShift(GenerateFreeShiftParams.UIMonday);
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_11", false));
                this.UIMap.GoToShiftBookTab();

                //step 14
                Playback.Wait(1000);
                UIMap.CoverShiftsWithEvenE(UIMap.CoverTuesdayVakantShiftEvenEParams.UIPceDate18042016, true);
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_14", false));
                this.UIMap.GoToShiftBookTab();

                //step 17
                Playback.Wait(1000);
                this.UIMap.GenerateFreeShift(GenerateFreeShiftParams.UIWednesday, UIMap.SetFreeShiftPeriodParams.UIETime0600, UIMap.SetFreeShiftPeriodParams.UIETime1400);
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_17", false));
                this.UIMap.GoToShiftBookTab();

                //step 20
                Playback.Wait(1000);
                UIMap.CoverShiftsWithEvenE(UIMap.CoverTuesdayVakantShiftEvenEParams.UIPceDate20042016, true);
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_20", false));
                this.UIMap.GoToShiftBookTab();

                //step 23
                Playback.Wait(1000);
                this.UIMap.GenerateExtraShift(UIMap.GenerateExtraShiftParams.UIPceDate21042016);
                this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_23", false);
                this.UIMap.GoToShiftBookTab();

                //step 26
                Playback.Wait(1000);
                this.UIMap.GenerateExtraShift(UIMap.GenerateExtraShiftParams.UIPceDate22042016, UIMap.SetExtraPeriodParams.UIETime0700, UIMap.SetExtraPeriodParams.UIETime1400);
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_26", false));
                this.UIMap.GoToShiftBookTab();

                //step 29
                Playback.Wait(1000);
                this.UIMap.GenerateFreeShift(GenerateFreeShiftParams.UISaturday, UIMap.SetFreeShiftPeriodParams.UIETime0600, UIMap.SetFreeShiftPeriodParams.UIETime1100);
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_29", false));
                this.UIMap.GoToShiftBookTab();

                //step 32
                Playback.Wait(1000);
                UIMap.CoverShiftsWithEvenE(UIMap.CoverTuesdayVakantShiftEvenEParams.UIPceDate23042016, true, "2");
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_32", false));
                this.UIMap.GoToShiftBookTab();

                //step 34
                Playback.Wait(1000);
                this.UIMap.GenerateExtraShift(UIMap.GenerateExtraShiftParams.UIPceDate24042016, UIMap.SetExtraPeriodParams.UIETime0700, UIMap.SetExtraPeriodParams.UIETime1200);
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_34", false));
                this.UIMap.GoToShiftBookTab();

                //step 37
                _ErrorList.AddRange(this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_37", false, "", UIMap.OpenReport95AndSaveToExcelParams.UI0105));
                this.UIMap.GoToShiftBookTab();

                //step 40
                try
                {
                    this.UIMap.GoToShiftDate(new DateTime(2016, 4, 25));
                }
                catch (Exception e)
                {
                    _ErrorList.Add(e.Message);
                }

                this.UIMap.GenerateFreeShift(GenerateFreeShiftParams.UIMonday, UIMap.SetFreeShiftPeriodParams.UIETime0600, UIMap.SetFreeShiftPeriodParams.UIETime0900);
                this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_40", false);
                this.UIMap.GoToShiftBookTab();

                //step 43
                UIMap.CoverShiftsWithEvenE(UIMap.CoverTuesdayVakantShiftEvenEParams.UIPceDate25042016, true);
                this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_43", false);
                this.UIMap.GoToShiftBookTab();

                //step 46
                this.UIMap.GenerateExtraShift(UIMap.GenerateExtraShiftParams.UIPceDate26042016, UIMap.SetExtraPeriodParams.UIETime0700, UIMap.SetExtraPeriodParams.UIETime1000);
                this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_46", false);
                this.UIMap.GoToShiftBookTab();

                //step 49
                UIMap.AddEmpAndEffectuate(false);
                this.UIMap.OpenReport95AndSaveToExcel("_chapter10_step_49", false);
                this.UIMap.GoToShiftBookTab();
            }
            catch (Exception e)
            {
                _ErrorList.Add(e.Message);
            }

            Playback.Wait(3000);
            UIMap.KillGatProcess();

            _ErrorList.AddRange(UIMap.CompareReportDataFiles_Test030());

            if (_ErrorList.Count <= 0)
            {
                TestContext.WriteLine("Test 30 finished OK");
                return;
            }
            UIMap.AssertResults(_ErrorList);
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
