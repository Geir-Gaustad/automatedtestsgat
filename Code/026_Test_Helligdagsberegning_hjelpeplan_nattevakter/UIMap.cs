using CommonTestData;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace _026_Test_Helligdagsberegning_hjelpeplan_nattevakter
{


    public partial class UIMap
    {

        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        #endregion

        private UIMapVS2017Classes.UIMapVS2017 map2017;
        public UIMapVS2017Classes.UIMapVS2017 UIMapVS2017
        {
            get
            {
                if ((this.map2017 == null))
                {
                    this.map2017 = new UIMapVS2017Classes.UIMapVS2017();
                }

                return this.map2017;
            }
        }

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }
        public int DelayBetweenActions
        {
            get
            {
                try
                {
                    return TestData.GetDelayBetweenActions;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get GetDelayBetweenActions: " + ex.Message);
                }

                return 200;
            }
        }
        public bool RestoreDatabase()
        {
            return UICommon.RestoreDatabase();
        }
        public void StartGat()
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepFrikoder);
        }
        
        public void CloseGat()
        {
            try
            {
                UICommon.CloseGat();
            }
            catch (Exception)
            {
                SupportFunctions.KillGatProcess(TestContext);
            }
        }

        public void AssertResults(List<string> errorList)
        {
            UICommon.AssertResults(errorList);
        }

        public List<string> TimeLapseInSeconds(DateTime timeBefore, DateTime timeAfter, string text, int upperLimit, int bottonLimit)
        {
            List<string> errorList = new List<string>();
            string elapsedTimeOutput = "";

            errorList.AddRange(LoadBalanceTesting.TimeLapseInSeconds(timeBefore, timeAfter, text, out elapsedTimeOutput, bottonLimit, upperLimit));
            TestContext.WriteLine(elapsedTimeOutput);

            return errorList;
        }

        private void SelectRosterplanTab(int delay = 0)
        {
            Playback.Wait(delay * 1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }
        private List<string> SelectRosterPlan(string planName)
        {
            Playback.Wait(1000);
            var timeBefore = DateTime.Now;
            UICommon.SelectRosterPlan(planName);
            var timeAfter = DateTime.Now;
            return TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved åpning av plan", 50, 30);
        }
        public List<string> OpenPlan(string planName)
        {
            Playback.Wait(1500);
            SelectRosterplanTab();
            Playback.Wait(3000);
            return SelectRosterPlan(planName);
        }
        public void CloseRosterplan()
        {
            try
            {
                Playback.Wait(1000);
                UICommon.XcloseRosterplan();
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e.Message);
            }

            Playback.Wait(1000);
        }

        public void AddEmployeesToRosterPlan()
        {
            OpenEmployeesInPlan();

            UICommon.ClickEmployeesButtonInEmployeeWindow();
            UIMapVS2017.Select_Larsson_Nilsson();
            UICommon.ClickOkAddEmployeesWindow();
            UICommon.ClickOkEmployeesWindow();
        }

        public void AddEmployeesToHelpPlan()
        {
            OpenEmployeesInPlan();

            UICommon.ClickAddEmployeesFromBaseplanButtonInEmployeeWindow();
            UICommon.SelectAllEmployeesInAddEmployeesFromBaseplanWindow();
            UICommon.ClickOkAddEmployeesFromBaseplanWindow();
            UIMapVS2017.AddF3_Larsson_Nilsson();
            UICommon.ClickOkEmployeesWindow();
        }
        public void ChangeElmanderF3Calc()
        {
            OpenEmployeesInPlan();
            
            UIMapVS2017.ChangeElmanderF3Calc();
            UICommon.ClickOkEmployeesWindow();
        }

        private void OpenEmployeesInPlan()
        {
            UICommon.SelectPlanTabRosterplan();
            UICommon.ClickEmployeesButtonRosterplan();
        }

        public void AddShiftsInRosterplan()
        {
            //if(selectPlanTab)
            //UICommon.SelectRosterplanPlanTab();

            UICommon.ClickEditRosterPlanFromPlantab();

            UIMapVS2017.InsertLarssonshifts();
            UIMapVS2017.InsertNilssonshifts();

            UICommon.ClickOKEditRosterPlanFromPlantab();
        }

        public void AddAbsenceInRosterplan()
        {
            //UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();
            UIMapVS2017.AddAbsenceToElmander();
            UICommon.SelectAbsenceCode("45B", "9");
            UICommon.ClickOKEditRosterPlanFromPlantab();
        }

        public void AddF3ShiftsLarson()
        {
            UICommon.ClickEditRosterPlanFromPlantab();
            UIMapVS2017.AddF3ShiftsLarson();
            UICommon.ClickOKEditRosterPlanFromPlantab();
        }

        public List<string> CreateHelpPlan()
        {
            var errorList = new List<string>();
            Playback.Wait(3000);
            UICommon.SelectRosterplanPlanTab();
            UICommon.SelectNewHelpplan();

            UICommon.SetStartDateNewHelpplan(new DateTime(2021, 04, 26)); 
            Playback.Wait(1000);

            UICommon.SetHelpPlanWeeks("5");
            Playback.Wait(1000);
            SetEmployeeF3Calculations();

            var timeBeforeOk = DateTime.Now;
            UICommon.ClickOkCreateHelpPlan();
            var timeAfterOk = DateTime.Now;

            errorList.AddRange(TimeLapseInSeconds(timeBeforeOk, timeAfterOk, "Tidsforbruk før bekreftelse på oppretting av hjelpeplan vises", 6, 2));

            return errorList;
        }

        public List<string> CheckHelpplanValues()
        {
            var errorList = new List<string>();
            Playback.Wait(3000);
            //UICommon.SelectRosterplanPlanTab();
            UICommon.OpenRosterplanSettings();

            try
            {
                CheckHelpplanSettings();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", Instillinger");
            }

            UICommon.ClickOkRosterplanSettings();

            try
            {
                UIMapVS2017.CheckEmployeeF3bCalculations();
                UIMapVS2017.CheckAnderssonCalculationsStep4();
                UIMapVS2017.CheckBrolinCalculationsStep4();
                UIMapVS2017.CheckElmanderCalculationsStep4();
                UIMapVS2017.CheckHellstromCalculationsStep4();
                UIMapVS2017.CheckIbrahimovicCalculationsStep4();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", Kalkulasjoner");
            }

            return errorList;
        }

        public List<string> ChecksStep8()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.ChecksStep8();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "_Step_8");
            }

            return errorList;
        }
        public List<string> ChecksStep9()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.ChecksStep9();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "_Step_9");
            }

            return errorList;
        }
        public List<string> ChecksStep10()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.ChecksStep10();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "_Step_10");
            }

            return errorList;
        }
        public List<string> ChecksStep11()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.ChecksStep11();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "_Step_11");
            }

            return errorList;
        }
    }
}
