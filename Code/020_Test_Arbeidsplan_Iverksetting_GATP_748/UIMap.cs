namespace _020_Test_Arbeidsplan_Iverksetting
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using CommonTestData;
    using System.Threading;
    using System.Globalization;

    public partial class UIMap
    {

        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        #endregion

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }

        #region Default Functions
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
        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepIverksetting, null, "", logGatInfo);
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

        public void OpenPlan(string planName)
        {
            Playback.Wait(1500);
            Click_RosterplanTab();
            UICommon.SelectRosterPlan(planName);
        }
        public void Click_EditRosterPlan()
        {
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();
        }

        public void Click_RosterplanTab()
        {
            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Rosterplan);
        }

        public void ClickRosterplanPlanTab()
        {
            UICommon.ClickRosterplanPlanTab();
        }

        public void ClickNewRosterPlanCopy()
        {
            UICommon.ClickNewRosterPlanCopy();
        }
        private void CloseRosterPlan()
        {
            try
            {
                UICommon.CloseRosterplanFromPlanTab();
            }
            catch
            {
                try
                {
                    UICommon.XcloseRosterplan();
                }
                catch (Exception e)
                {
                    TestContext.WriteLine(e.Message);
                }
            }

            Playback.Wait(1000);
        }

        private void GoToShiftDateNew(DateTime date)
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
            UICommon.GoToShiftbookdate(date);
        }

        private void EffectuateDate(DateTime effDate)
        {
            UICommon.ChangeEffectuationPeriodForActualLines(effDate, effDate);
            UICommon.EffectuateRosterplanLines(false);
        }
        private void EffectuatePeriod(DateTime effDate, DateTime toDate)
        {
            UICommon.ChangeEffectuationPeriodForActualLines(effDate, toDate);
            UICommon.EffectuateRosterplanLines(false);
        }

        #endregion

        public List<string> Step_1()
        {
            var errorList = new List<string>();

            try
            {
                StartGat(true);
                OpenPlan("Overlapp plan");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 1: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_2()
        {
            var errorList = new List<string>();

            OpenEffLine1();
            EffectuateDate(new DateTime(2024, 01, 01));

            try
            {
                CheckMessageStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_3()
        {
            var errorList = new List<string>();

            CloseOverlapWarningDialog();
            try
            {
                CheckPlanStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_4()
        {
            var errorList = new List<string>();

            OpenEffLine1();
            EffectuateDate(new DateTime(2024, 01, 02));

            try
            {
                CheckMessageStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_5()
        {
            var errorList = new List<string>();

            CloseOverlapWarningDialog();

            try
            {
                CheckPlanStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_6()
        {
            var errorList = new List<string>();
            //Legge inn overtid på vakter frem til hit i testen
            OpenEffLine1();
            EffectuateDate(new DateTime(2024, 01, 03));

            try
            {
                CheckMessageStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_7()
        {
            var errorList = new List<string>();

            CloseOverlapWarningDialog();

            try
            {
                CheckPlanStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_8()
        {
            var errorList = new List<string>();

            OpenEffLine1();
            EffectuateDate(new DateTime(2024, 01, 04));

            try
            {
                CheckMessageStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_9()
        {
            var errorList = new List<string>();

            CloseOverlapWarningDialog();

            try
            {
                CheckPlanStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 9: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_10()
        {
            var errorList = new List<string>();

            OpenEffLine1();
            EffectuateDate(new DateTime(2024, 01, 05));

            try
            {
                CheckMessageStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_11()
        {
            var errorList = new List<string>();

            CloseOverlapWarningDialog();

            try
            {
                CheckPlanStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_12()
        {
            var errorList = new List<string>();
            //Legge in forskjøvet vakt

            OpenEffLine3();
            EffectuateDate(new DateTime(2024, 01, 02));

            try
            {
                CheckMessageStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 12: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_13()
        {
            var errorList = new List<string>();

            CloseOverlapWarningDialog();

            try
            {
                CheckPlanStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_14()
        {
            var errorList = new List<string>();
            //Legge in forskjøvet vakt
            OpenEffLine3();
            EffectuateDate(new DateTime(2024, 01, 03));
            try
            {
                CheckMessageStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 14: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_15()
        {
            var errorList = new List<string>();

            CloseOverlapWarningDialog();

            try
            {
                CheckPlanStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_16()
        {
            var errorList = new List<string>();
            //Bytte med ansatt 20, 09.01
            OpenEffLine4();
            EffectuateDate(new DateTime(2024, 01, 08));
            try
            {
                CheckMessageStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 16: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_17()
        {
            var errorList = new List<string>();

            CloseOverlapWarningDialog();

            try
            {
                CheckPlanStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 17: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_18()
        {
            var errorList = new List<string>();
            //Legge inn ansatt 21 på O vakt 9.1 08-16
            //Person 4, Bytte med ansatt 21, 10.01
            OpenEffLine4();
            EffectuateDate(new DateTime(2024, 01, 09));
            try
            {
                CheckMessageStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 18: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_19()
        {
            var errorList = new List<string>();

            CloseOverlapWarningDialog();

            try
            {
                CheckPlanStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 19: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_20()
        {
            var errorList = new List<string>();
            //Opprette BA ansatt 5, ny vakt kode 19(08.01.2024).
            OpenEffLine5();
            EffectuateDate(new DateTime(2024, 01, 09));
            try
            {
                CheckMessageStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 19: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_21()
        {
            var errorList = new List<string>();

            CloseOverlapWarningDialog();

            try
            {
                CheckPlanStep();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 21: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_22()
        {
            var errorList = new List<string>();

            try
            {
                OpenEffLine6();
                EffectuateDate(new DateTime(2024, 01, 15));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 22: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_23_24()
        {
            var errorList = new List<string>();

            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 23_24: " + e.Message);
            }

            UICommon.CloseSalaryCalculationsWindow();

            return errorList;
        }
        public List<string> Step_25()
        {
            var errorList = new List<string>();

            try
            {
                OpenEffLine6();
                EffectuateDate(new DateTime(2024, 01, 16));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 25: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_26_27()
        {
            var errorList = new List<string>();

            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 26_27: " + e.Message);
            }

            UICommon.CloseSalaryCalculationsWindow();

            return errorList;
        }

        public List<string> Step_28()
        {
            var errorList = new List<string>();

            try
            {
                OpenEffLine7();
                EffectuateDate(new DateTime(2024, 01, 17));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 28: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_29_30()
        {
            var errorList = new List<string>();

            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 29_30: " + e.Message);
            }

            UICommon.CloseSalaryCalculationsWindow();

            return errorList;
        }

        public List<string> Step_31()
        {
            var errorList = new List<string>();

            try
            {
                OpenEffLine7();
                EffectuateDate(new DateTime(2024, 01, 19));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 31: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_32_33()
        {
            var errorList = new List<string>();

            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 32_33: " + e.Message);
            }

            UICommon.CloseSalaryCalculationsWindow();

            return errorList;
        }
        public List<string> Step_34()
        {
            var errorList = new List<string>();
  
            try
            {
                OpenEffLine7();
                EffectuateDate(new DateTime(2024, 01, 22));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 34: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_35()
        {
            var errorList = new List<string>();
            
            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 35: " + e.Message);
            }

            UICommon.CloseSalaryCalculationsWindow();        

            return errorList;
        }

        public List<string> Step_36()
        {
            var errorList = new List<string>();

            try
            {
                OpenEffLine7();
                EffectuateDate(new DateTime(2024, 01, 23));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 36: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_37()
        {
            var errorList = new List<string>();

            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 37: " + e.Message);
            }

            UICommon.CloseSalaryCalculationsWindow();

            return errorList;
        }
        public List<string> Step_38()
        {
            var errorList = new List<string>();

            try
            {
                OpenEffLine7();
                EffectuateDate(new DateTime(2024, 01, 24));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 38: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_39()
        {
            var errorList = new List<string>();

            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 39: " + e.Message);
            }

            UICommon.CloseSalaryCalculationsWindow();

            return errorList;
        }
        public List<string> Step_40()
        {
            var errorList = new List<string>();

            try
            {
                OpenEffLine7();
                EffectuateDate(new DateTime(2024, 01, 25));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 48: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_41()
        {
            var errorList = new List<string>();

            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 41: " + e.Message);
            }

            UICommon.CloseSalaryCalculationsWindow();

            return errorList;
        }
        public List<string> Step_42()
        {
            var errorList = new List<string>();

            try
            {
                OpenEffLine2();
                EffectuateDate(new DateTime(2024, 01, 03));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 42: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_43()
        {
            var errorList = new List<string>();

            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 43: " + e.Message);
            }

            UICommon.CloseSalaryCalculationsWindow();
            CloseRosterPlan();

            GoToShiftDateNew(new DateTime(2024, 01, 03));

            return errorList;
        }
        public List<string> Step_44()
        {
            var errorList = new List<string>();

            OpenAbsenceStep44();

            //Endret i Gat v2020.1.2. Ref. GATW-4815
            //<-- Gat v2020.1.2
            //SelectAbsRadioButtonStep44();

            //try
            //{
            //    CheckAbsenceStep44();
            //}
            //catch (Exception e)
            //{
            //    errorList.Add("Error in Step 44: " + e.Message);
            //}

            return errorList;
        }
        public List<string> Step_45()
        {
            var errorList = new List<string>();
            
            try
            {
                UICommon.UIMapVS2017.ClickOkConstuctAbsence();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 45: " + e.Message);
            }
            
            return errorList;
        }
        
        public List<string> Step_46()
        {
            var errorList = new List<string>();

            OpenPlan("Overlapp plan");
            OpenEffLine2();
            UICommon.UIMapVS2017.OpenRegStatusWindow();

            try
            {
                CheckRegStatusStep46();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 46: " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRegStatusWindow();                  
            EffectuateDate(new DateTime(2024, 01, 04));

            return errorList;
        }
        public List<string> Step_47()
        {
            var errorList = new List<string>();

            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 47: " + e.Message);
            }

            UICommon.CloseSalaryCalculationsWindow();
            CloseRosterPlan();

            GoToShiftDateNew(new DateTime(2024, 01, 04));
            UICommon.UIMapVS2017.SelectDashboardWhenUnpinned();
            UICommon.UIMapVS2017.PinRoleAssigmentDashboard();

            return errorList;
        }
        public List<string> Step_48()
        {
            var errorList = new List<string>();

            OpenEmp2AbsenceStep48();
            //Endret i Gat v2020.1.2. Ref. GATW-4815
            //SelectTSCheckBoxStep44(); <-- Gat v2020.1.2
            SelectTSCheckBoxStep44New();
            UICommon.UIMapVS2017.ClickOkConstuctAbsence();
      
            try
            {
                
               CheckAbsInDashStep48();
               CheckNoAbsInDashStep48();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 48: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_49()
        {
            var errorList = new List<string>();

            OpenPlan("Overlapp plan");
            OpenEffLine2();
            EffectuatePeriod(new DateTime(2024, 01, 08), new DateTime(2024, 01, 14));

            try
            {
                CheckNoPaymentCalculation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 49: " + e.Message);
            }
            UICommon.CloseSalaryCalculationsWindow();

            return errorList;
        }
        public List<string> Step_50()
        {
            var errorList = new List<string>();

            CloseRosterPlan();

            GoToShiftDateNew(new DateTime(2024, 01, 08));
            UICommon.UIMapVS2017.SelectShiftBookWeekView();
            UICommon.UIMapVS2017.RefreshDashboard();
            UICommon.UIMapVS2017.SelectDashboardAbsences();

            try
            {  
                CheckAbsInDashStep50();
                CheckAbsStep50();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 50: " + e.Message);
            }

            return errorList;
        }
    }
}
