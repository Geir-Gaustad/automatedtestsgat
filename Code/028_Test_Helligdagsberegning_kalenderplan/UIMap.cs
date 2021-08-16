namespace _028_Test_Helligdagsberegning_kalenderplan
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using System;
    using System.Collections.Generic;
    using System.CodeDom.Compiler;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Drawing;
    using System.Windows.Input;
    using System.Text.RegularExpressions;
    using System.Diagnostics;
    using CommonTestData;
    using System.Globalization;
    using System.Threading;

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
        public bool RestoreDatabase()
        {
            return UICommon.RestoreDatabase();
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

        public List<string> CreateKalenderPlan()
        {
            var errorList = new List<string>();
            SelectRosterplanTab(2);
            UICommon.ClickNewRosterplanButton();

            SetCalendarplanName();
            SelectRosterplanTypeCalendarplan();
            SetStartDateCalendarplan(new DateTime(2014, 04, 14));

            SetCalendarplanWeeks();

            var timeBeforeOk = DateTime.Now;
            UICommon.ClickOkRosterplanSettings();
            var timeAfterOk = DateTime.Now;

            errorList.AddRange(TimeLapseInSeconds(timeBeforeOk, timeAfterOk, "Tidsforbruk før bekreftelse på oppretting av kalenderplan vises", 5, 1));

            return errorList;
        }

        /// <summary>
        /// SetStartDateCalendarplan - Use 'SetStartDateCalendarplanParams' to pass parameters into this method.
        /// </summary>
        public void SetStartDateCalendarplan(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEStartDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.DateTime = date;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{Tab}", ModifierKeys.None);
        }

        public void AddEmployeesToPlan()
        {
            UICommon.SelectPlanTabRosterplan();
            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            UIMapVS2017.SelectEmployees_step_2();
            UICommon.ClickOkAddEmployeesWindow();
            UICommon.ClickOkEmployeesWindow();
            Playback.Wait(2000);
        }

        public List<string> ChecksStep2()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.ChecksStep2();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "_Step_2");
            }

            return errorList;
        }

        public void SelectHelgeOgHoytidsbergeningFilter()
        {
            UICommon.ClickRosterplanFilterTab();
            UICommon.UIMapVS2017.SelectViewFilter("Helge");
        }

        public List<string> AddAnderssonShifts()
        {
            var errorList = new List<string>();
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                UIMapVS2017.CheckAnderssonValues();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 3(Andersson)");
            }

            InsertAnderssonDshifts();

            try
            {
                UIMapVS2017.CheckAfterInsertDshifts();                
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 4(Andersson)");
            }
            
            return errorList;
        }
        public List<string> AddDahlinShifts()
        {
            var errorList = new List<string>();

            InsertDahlinLine1Nshifts();
            InsertDahlinLine2Nshifts();
            UIMapVS2017.InsertDahlinLine2Nshifts();

            try
            {
                UIMapVS2017.CheckAfterInsertDahlinNshifts();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 5(Dahlin)");
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();
            return errorList;
        }

        public List<string> EditDahlinValidPeriods()
        {
            var errorList = new List<string>();

            UICommon.SelectPlanTabRosterplan();
            UICommon.ClickEmployeesButtonRosterplan();
            try
            {
                UIMapVS2017.EditDahlinValidPeriod1();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 7(Dahlin)");
            }

            UICommon.ClickOkEmployeesWindow();            
            UICommon.ClickEmployeesButtonRosterplan();

            try
            {
                UIMapVS2017.EditDahlinValidPeriod2();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 7(Dahlin)");
            }

            UICommon.ClickOkEmployeesWindow();

            try
            {
                Playback.Wait(2000);
                UIMapVS2017.CheckAfterEditDahlinValidPeriods();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 7(Dahlin)");
            }

            return errorList;
        }
               

        public List<string> AddRavelliShifts()
        {
            var errorList = new List<string>();

            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                UIMapVS2017.AddRavelliShifts();
                UIMapVS2017.CheckAfterInsertRavelliNshifts();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 8(Ravelli)");
            }
            
            return errorList;
        }

        public List<string> AddRavelliShifts2()
        {
            var errorList = new List<string>();
            
            try
            {
                UIMapVS2017.AddRavelliShifts2();
                UIMapVS2017.CheckAfterInsertRavelliNshifts2();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 9(Ravelli)");
            }
            
            return errorList;
        }

        public List<string> DeleteRavelliShifts()
        {
            var errorList = new List<string>();
            
            try
            {
                UIMapVS2017.RightClickRavelliCell();
                UIMapVS2017.DeleteRavelliShifts();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 10(Ravelli)");
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();
            return errorList;
        }

        public List<string> EditPlanSettings()
        {
            var errorList = new List<string>();

            try
            {
                CheckNightShiftsOnStartDay(true);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }
            
            return errorList;
        }

        public List<string> EditRavelliShifts()
        {
            var errorList = new List<string>();

            CloseRosterplan();
            UICommon.SelectRosterPlan("Kalenderplan");         

            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                UIMapVS2017.EditRavelliShifts();
                UIMapVS2017.CheckAfterEditRavelliNshifts();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 12(Ravelli)");
            }

            return errorList;
        }

        public List<string> DeleteAndInsertRavelliShifts()
        {
            var errorList = new List<string>();

            try
            {
                UIMapVS2017.RightClickRavelliCell();
                UIMapVS2017.DeleteRavelliShifts();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 13(Ravelli)");
            }

            try
            {
                UIMapVS2017.EditRavelliShifts2();
                UIMapVS2017.CheckAfterEditRavelliNshifts2();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 13(Ravelli)");
            }

            return errorList;
        }

        public List<string> DeleteAndInsertRavelliShifts2()
        {
            var errorList = new List<string>();

            try
            {
                UIMapVS2017.RightClickRavelliCell();
                UIMapVS2017.DeleteRavelliShifts();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 14(Ravelli)");
            }

            try
            {
                UIMapVS2017.EditRavelliShifts3();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 14(Ravelli)");
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();

            try
            {
                UIMapVS2017.CheckAfterEditRavelliNshifts3();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ", step 14(Ravelli)");
            }

            return errorList;
        }

        private void CheckNightShiftsOnStartDay(bool check)
        {
            UICommon.OpenRosterplanSettings();

            if (check)
                UICommon.CheckNightShiftsOnStartDay();
            else
                UICommon.UnCheckNightShiftsOnStartDay();

            UICommon.ClickOkRosterplanSettings();
        }
    }
}
