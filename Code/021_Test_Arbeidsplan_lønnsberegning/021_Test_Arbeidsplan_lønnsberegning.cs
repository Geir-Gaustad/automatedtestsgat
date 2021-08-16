using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using System.Diagnostics;
using System.Data;
using _021_Test_Arbeidsplan_lønnsberegning;
using System.Threading;
using System.Globalization;
using CommonTestData;

namespace _021_Test_Arbeidsplan_lønnsberegningTest_021
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_021_Arbeidsplan_lønnsberegning
    {
        private Dictionary<string, string> _salaryTypes;

        [TestMethod, Timeout(6000000)]
        public void SystemTest_021_Step_901_TO_913()
        {
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();

            _salaryTypes = new Dictionary<string, string>();
            var errorList = new List<String>();
            var testNoError = "";

            //PaymentCell_200 = Timelønn
            //PaymentCell_400 = lørdag/søndag
            //PaymentCell_410 = Kveld/Nattillegg
            //PaymentCell_430 = Helligdagstillegg fast
            //PaymentCell_450 = F4 avspasering
            //PaymentCell_510 = Utvidet arbeidstid
            try
            {
                _salaryTypes.Add(".Kode.200TL", "PaymentCell_200");
                _salaryTypes.Add(".Kode.410KN", "PaymentCell_410");
                _salaryTypes.Add(".Kode.430HH", "PaymentCell_430");

                UIMap.StartGat(true);
                
                UIMap.OpenPlan(PlanNames.MainPlan);

                //29.12.2014 og antall uker=3.
                UIMap.CopyLønnsberegningerPlan(UIMap.PlanTypes.RosterPlan, new DateTime(2015, 3, 2), "3"); //"29.12.2014"
    
                this.UIMap.CloseCurrentPlan();
                UIMap.OpenPlan(PlanNames.CopiedPlan);

                TestContext.WriteLine("Minne før iverksetting(Kap. 1): " + UIMap.ReadPhysicalMemoryUsage());
                foreach (var date in UIMap.ChangePeriodFoCurrentLines.GetDateList_92_913())
                {
                    var checkMode = "";
                    var testNoDate = date.Remove(date.IndexOf(";"));
                    var testNo = testNoDate.Replace(".Date", "").Trim();
                    testNoError = testNo;
                    var actualDateString = date.Replace(testNoDate + ";", "");
                    var dataSourceDate = testNoDate + "." + actualDateString.Replace(".", "").Remove(4).Trim();
                    var actualDate = Convert.ToDateTime(actualDateString);
                    

                    switch (testNo)
                    {
                        case "9.6":
                        case "9.7":
                            errorList.AddRange(SystemTest_021_Step_906_907(testNo, dataSourceDate, actualDate));
                            break;

                        case "9.2":
                        case "9.3":
                        case "9.4":
                        case "9.5":
                        case "9.8":
                        case "9.9":
                        case "9.10":
                        case "9.11":
                            checkMode = ".Flest";
                            //UnCheck NightshiftsOnStartDay                           
                            UIMap.SetAndEffectuate(new DateTime(2015, 12, 27) , null, actualDate, null, true); //"03.01.2016"
                            errorList.AddRange(UIMap.CheckGridData("name", dataSourceDate + checkMode, "PaymentCell_430"));

                            this.UIMap.CloseSalaryCalculationWindow();
                            UIMap.DeleteEffectuation();

                            checkMode = ".Start";
                            UIMap.SetAndEffectuate(new DateTime(2015, 12, 27), null, actualDate, null, true, true); //"03.01.2016"
                            errorList.AddRange(UIMap.CheckGridData("name", dataSourceDate + checkMode, "PaymentCell_430"));

                            break;

                        case "9.12":
                            checkMode = ".Flest";
                            //Åpne Ansatt vinduet i arbeidsplanen. Sett inn årsakskode for Timelønnsberegning, benytt 3-4 ulike årsakskoder, 
                            //men se at alle linjer får en årsakskode (se ikon for Timelønnsberegning i kolonne ”T.b.”). Klikk Ok for å lagre.
                            //Åpne Innstillinger å se at det IKKE er kryss for ”Nattevakter på startdagen”.
                            //Gå på iverksett og velg dato 17.5. Se at det gir følgende beregning av timelønn og tillegg pr. linje 
                            //(skriv resultat inn i tabellen og sammenlign med fasit):
                            this.UIMap.ClickRosterplanPlanTab();
                            this.UIMap.ClickEmployeeRosterplanWindow();
                            UIMap.CheckCauseCodesAndCloseWindow();

                            //UnCheck NightshiftsOnStartDay
                            UIMap.SetAndEffectuate(new DateTime(2016, 1, 3), null, actualDate, null, true);
                            foreach (var salaryType in _salaryTypes)
                            {
                                errorList.AddRange(UIMap.CheckGridData("name", dataSourceDate + salaryType.Key + checkMode, salaryType.Value));
                            }

                            break;

                        case "9.13":
                            checkMode = ".Start";
                            //Check NightshiftsOnStartDay
                            UIMap.SetAndEffectuate( new DateTime(2016, 1, 3), null, actualDate, null, true, true);
                            foreach (var salaryType in _salaryTypes)
                            {
                                errorList.AddRange(UIMap.CheckGridData("name", dataSourceDate + salaryType.Key + checkMode, salaryType.Value));
                            }

                            break;

                        default:
                            break;
                    }

                    if (testNo != "9.6" && testNo != "9.7")
                    {
                        this.UIMap.CloseSalaryCalculationWindow();
                        UIMap.DeleteEffectuation();
                    }

                    
                    TestContext.WriteLine("Teststeg: " + testNoDate.Replace("Date", "") + ", Id: " + dataSourceDate);
                }

                TestContext.WriteLine("Minne etter iverksetting og iverksletting(Kap. 1): " + UIMap.ReadPhysicalMemoryUsage());
                this.UIMap.CloseCurrentPlan();
                this.UIMap.CloseGat();
            }
            catch (Exception ex)
            {
                errorList.Add("Error in SystemTest_021_Step_" + testNoError + ": " + ex.Message);
            }

            if (errorList.Count > 0)
                UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter1 finished OK");
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_021_Step_914_TO_919()
        {
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            //PaymentCell_200 = Timelønn
            //PaymentCell_400 = lørdag/søndag
            //PaymentCell_410 = Kveld/Nattillegg
            //PaymentCell_430 = Helligdagstillegg fast
            //PaymentCell_450 = F4 avspasering
            var errorList = new List<String>();
            var testNoError = "";
            try
            {
                _salaryTypes = new Dictionary<string, string>();
                _salaryTypes.Add(".Kode.200TL", "PaymentCell_200");
                _salaryTypes.Add(".Kode.410KN", "PaymentCell_410");
                _salaryTypes.Add(".Kode.430HH", "PaymentCell_430");

                UIMap.StartGat(false);

                TestContext.WriteLine("Minne før iverksetting(Kap. 2): " + UIMap.ReadPhysicalMemoryUsage());
                foreach (var date in UIMap.ChangePeriodFoCurrentLines.GetDateList_914_919())
                {
                    var checkMode = "";
                    var testNoDate = date.Remove(date.IndexOf(";"));
                    var testNo = testNoDate.Replace(".Date", "").Trim();
                    testNoError = testNo;
                    var actualDateString = date.Replace(testNoDate + ";", "");
                    var dataSourceDate = testNoDate + "." + actualDateString.Replace(".", "").Remove(4).Trim();
                    var actualDate = Convert.ToDateTime(actualDateString);
                    

                    switch (testNo)
                    {
                        case "9.14":
                            checkMode = ".Flest";
                            UIMap.SetWednesdayBeforeIsHolydayInTurnus(true, PlanNames.CopiedPlan);
                            //UnCheck NightshiftsOnStartDay
                            UIMap.OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(new DateTime(2016,1,3), null);
                            break;
                        case "9.16":
                            checkMode = ".Flest";
                            this.UIMap.CloseCurrentPlan();
                            UIMap.SetWednesdayBeforeIsHolydayInTurnus(false, PlanNames.CopiedPlan);
                            //UnCheck NightshiftsOnStartDay
                            UIMap.OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(new DateTime(2016, 1, 3), null);
                            break;
                        case "9.18":
                            checkMode = ".Flest";
                            //UnCheck NightshiftsOnStartDay
                            UIMap.OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(new DateTime(2016, 1, 3), null);
                            _salaryTypes.Remove(".Kode.430HH");
                            _salaryTypes.Add(".Kode.400LS", "PaymentCell_400");

                            break;
                        case "9.15":
                        case "9.17":
                        case "9.19":
                            checkMode = ".Start";
                            //Check NightshiftsOnStartDay
                            UIMap.OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(new DateTime(2016, 1, 3), null, true);
                            break;

                        default:
                            break;
                    }

                    UIMap.SetAndEffectuate(new DateTime(2016, 1, 3), null, actualDate);
                    foreach (var salaryType in _salaryTypes)
                    {
                        errorList.AddRange(UIMap.CheckGridData("name", dataSourceDate + salaryType.Key + checkMode, salaryType.Value));
                    }

                    this.UIMap.CloseSalaryCalculationWindow();
                    this.UIMap.DeleteEffectuation();

                    
                    TestContext.WriteLine("Teststeg: " + testNoDate.Replace("Date", "") + ", Id: " + dataSourceDate);
                }

                TestContext.WriteLine("Minne etter iverksetting og iverksletting(Kap. 2): " + UIMap.ReadPhysicalMemoryUsage());
                this.UIMap.CloseCurrentPlan();
                UIMap.DeletePlan(PlanNames.CopiedPlan);

                //Klargjøre for teststeg 9.20
                UIMap.SetWednesdayBeforeIsHolydayInTurnus(true);
                this.UIMap.CloseGat();

            }
            catch (Exception ex)
            {
                errorList.Add("Error in SystemTest_021_Step_" + testNoError + ": " + ex.Message);
            }

            if (errorList.Count > 0)
                UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter2 finished OK");
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_021_Step_920_TO_936()
        {
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;
            var errorList = new List<String>();

            try
            {
                UIMap.StartGat(false);
                UIMap.OpenPlan(PlanNames.MainPlan);
                
                this.UIMap.ClickRosterplanPlanTab();
                this.UIMap.ClickEmployeeRosterplanWindow();

                UIMap.CheckCauseCodesAndCloseWindow(true);

                TestContext.WriteLine("Minne før iverksetting(Kap. 3): " + UIMap.ReadPhysicalMemoryUsage());
                var timeBefore = DateTime.Now;
                UIMap.SetAndEffectuate(new DateTime(2012, 1, 1), null, new DateTime(2011, 4, 18), new DateTime(2011, 5, 1), true, true);
                errorList.AddRange(UIMap.CheckGridData("name", "9.20.Start", "PaymentCell_430"));

                this.UIMap.CloseSalaryCalculationWindow();
                TestContext.WriteLine("Teststeg: " + "9.20" + ", Id: 18.04.2011 - 01.05.2011");

                //9.21
                //18.4.2011 og antall uker=2.
                UIMap.CopyLønnsberegningerPlan(UIMap.PlanTypes.HelpPlan, new DateTime(2011,4,18), "2");
                this.UIMap.CloseCurrentPlan();

                //UIMap.ShowAllPlans();
                UIMap.OpenPlan(PlanNames.HelpPlan);
                this.UIMap.ClickPlanTabHelpPlan();
                UIMap.InsertF4OnMondays();

                //HelpPlan
                UIMap.SetAndEffectuate(new DateTime(2012, 1, 1), null, null);

                //9.21.Date.1804.2weeks.2.Kode.430HH
                errorList.AddRange(UIMap.CheckGridData("name", "9.21.Date.1804.2weeks.2.Kode.430HH", "PaymentCell_430"));
                //9.21.Date.1804.2weeks.2.Kode.450F4
                errorList.AddRange(UIMap.CheckGridData("name", "9.21.Date.1804.2weeks.2.Kode.450F4", "PaymentCell_450"));
                this.UIMap.CloseSalaryCalculationWindow();
                this.UIMap.DeleteEffectuation();
                TestContext.WriteLine("Teststeg: " + "9.21" + ", Id: 18.04.2011 => 2 uker, F4");

                this.UIMap.CloseCurrentPlan();
                UIMap.DeletePlan(PlanNames.HelpPlan);

                UIMap.OpenPlan(PlanNames.MainPlan);
                this.UIMap.ClickRosterplanPlanTab();
                this.UIMap.DeleteEffectuation();

                //9.23.Date..........
                //Check NightshiftsOnStartDay
                UIMap.SetAndEffectuate(new DateTime(2012, 1, 1), new DateTime(2011, 5, 16), new DateTime(2011, 4, 4), new DateTime(2011, 6, 12), true, true);

                //9.23.Date.0404_1206.Start
                errorList.AddRange(UIMap.CheckGridData("name", "9.23.Start", "PaymentCell_430"));

                this.UIMap.CloseSalaryCalculationWindow();
                this.UIMap.DeleteEffectuation();

                TestContext.WriteLine("Minne etter iverksetting og iverksletting(Kap. 3): " + UIMap.ReadPhysicalMemoryUsage());
                TestContext.WriteLine("Teststeg: " + "9.23" + ", 04.04.2011 - 12.06.2011");
                this.UIMap.CloseCurrentPlan();

                //9.24. Åpne arbeidsplan UTJEVNINGSVAKTER.
                UIMap.OpenPlan(PlanNames.EqualizationPlan);
                this.UIMap.ClickRosterplanPlanTab();

                TestContext.WriteLine("Minne før iverksetting(Kap. 3_2): " + UIMap.ReadPhysicalMemoryUsage());
                UIMap.SetAndEffectuate(null, null, new DateTime(2012, 4, 9), new DateTime(2012, 4, 15));

                //9.24.Date.0904-1504.Kode.200TL
                errorList.AddRange(UIMap.CheckGridData("name", "9.24.Date.0904-1504.Kode.200TL", "PaymentCell_200"));
                //9.24.Date.0904-1504.Kode.410KN
                errorList.AddRange(UIMap.CheckGridData("name", "9.24.Date.0904-1504.Kode.410KN", "PaymentCell_410"));
                //9.24.Date.0904-1504.Kode.430HH
                errorList.AddRange(UIMap.CheckGridData("name", "9.24.Date.0904-1504.Kode.430HH", "PaymentCell_430"));

                this.UIMap.CloseSalaryCalculationWindow();
                this.UIMap.DeleteEffectuation();

                TestContext.WriteLine("Minne etter iverksetting og iverksletting(Kap. 3_2): " + UIMap.ReadPhysicalMemoryUsage());
                TestContext.WriteLine("Teststeg: " + "9.24" + ", 09.04.2012-15.04.2012 (1 uke). ");
                this.UIMap.CloseCurrentPlan();

                /*9.25
                 Åpne planen Hjelpeplan for UTJEVINGSVAKTER.
                 Iverksett planen og sjekk at det gir følgende beregning av tillegg og trekk for F4 avspasering pr. linje (skriv resultat inn i tabellen og sammenlign med fasit):
                */

                UIMap.OpenPlan(PlanNames.EqualizationHelpPlan);
                this.UIMap.ClickPlanTabHelpPlan();

                TestContext.WriteLine("Minne før iverksetting(Kap. 3_3): " + UIMap.ReadPhysicalMemoryUsage());
                //Helpplan
                UIMap.SetAndEffectuate(null, null, null);

                //9.25.Kode.430HH
                errorList.AddRange(UIMap.CheckGridData("name", "9.25.Kode.430HH", "PaymentCell_430"));
                //9.25.Kode.450F4
                errorList.AddRange(UIMap.CheckGridData("name", "9.25.Kode.450F4", "PaymentCell_450"));
                this.UIMap.CloseSalaryCalculationWindow();
                this.UIMap.DeleteEffectuation();

                TestContext.WriteLine("Minne etter iverksetting og iverksletting(Kap. 3_3): " + UIMap.ReadPhysicalMemoryUsage());
                TestContext.WriteLine("Teststeg: " + "9.25" + ", Id: F4");
                this.UIMap.CloseCurrentPlan();

                var timeAfter = DateTime.Now;
                errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved iverksetting/slette iverksetting kap.3"));

                errorList.AddRange(SystemTest_021_Step_926_936());
                this.UIMap.CloseGat();
            }
            catch (Exception ex)
            {
                errorList.Add("Error in SystemTest_021_Step_920_TO_936: " + ex.Message);
            }

            if (errorList.Count > 0)
                UIMap.AssertResults(errorList);

            TestContext.WriteLine("Chapter3 finished OK");
        }

        private List<String> SystemTest_021_Step_906_907(string testNo, string dataSourceDate, DateTime actualDate)
        {
            ////Velg å kopiere planen til en hjelpeplan med startdato mandag etter palmesøndag og antall uker = 1.
            //DateList.Add("9.6.Date;01.04.2015 [SelectionStart]0[SelectionLength]10");
            ////Gå til Administrasjon | Regelsett og fjern kryss for ”Onsdag før skjærtorsdag er helligdag i turnusplanen” på regelsett TURNUS.
            //DateList.Add("9.7.Date;01.04.2015 [SelectionStart]0[SelectionLength]10");

            var errorList = new List<String>();
            var checkMode = "";

            try
            {
                if (testNo == "9.6")
                {
                    this.UIMap.CloseCurrentPlan();
                    UIMap.OpenPlan(PlanNames.CopiedPlan);
                    //UnCheck NightshiftsOnStartDay
                    UIMap.OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(new DateTime(2016, 1, 3), null);
                    //30.03.2015 og antall uker=1.
                    UIMap.CopyLønnsberegningerPlan(UIMap.PlanTypes.HelpPlan, new DateTime(2015, 3, 30), "1");
                    this.UIMap.CloseCurrentPlan();
                    UIMap.OpenPlan(PlanNames.HelpPlan);
                    this.UIMap.ClickPlanTabHelpPlan();

                    checkMode = ".Flest";
                    //Helpplan
                    UIMap.SetAndEffectuate(new DateTime(2016, 1, 3), null, actualDate);

                    errorList.AddRange(UIMap.CheckGridData("name", dataSourceDate + checkMode, "PaymentCell_430"));

                    this.UIMap.CloseSalaryCalculationWindow();
                    this.UIMap.DeleteEffectuation();
                    this.UIMap.CloseCurrentPlan();
                    UIMap.DeletePlan(PlanNames.HelpPlan);
                    UIMap.OpenPlan(PlanNames.CopiedPlan);

                    //Check NightshiftsOnStartDay
                    UIMap.OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(new DateTime(2016, 1, 3), null, true);
                    //30.03.2015 og antall uker=1.
                    UIMap.CopyLønnsberegningerPlan(UIMap.PlanTypes.HelpPlan, new DateTime(2015, 3, 30), "1");
                    this.UIMap.CloseCurrentPlan();
                    UIMap.OpenPlan(PlanNames.HelpPlan);
                    this.UIMap.ClickPlanTabHelpPlan();

                    //Bygge denne inn i 
                    //SetAndEffectuate(actualDate, "", true);
                    checkMode = ".Start";
                    //Helpplan
                    UIMap.SetAndEffectuate(new DateTime(2016, 1, 3), null, actualDate);

                    errorList.AddRange(UIMap.CheckGridData("name", dataSourceDate + checkMode, "PaymentCell_430"));

                    this.UIMap.CloseSalaryCalculationWindow();
                    this.UIMap.DeleteEffectuation();
                    this.UIMap.CloseCurrentPlan();
                    UIMap.DeletePlan(PlanNames.HelpPlan);
                }
                else
                {
                    //Gå til Administrasjon | Regelsett og fjern kryss for ”Onsdag før skjærtorsdag er helligdag i turnusplanen” på regelsett TURNUS.
                    UIMap.SetWednesdayBeforeIsHolydayInTurnus(false, PlanNames.CopiedPlan);
                    //Check NightshiftsOnStartDay
                    UIMap.OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(new DateTime(2016, 1, 3), null);
                    //30.03.2015 og antall uker=1.
                    UIMap.CopyLønnsberegningerPlan(UIMap.PlanTypes.HelpPlan, new DateTime(2015, 3, 30), "1");
                    this.UIMap.CloseCurrentPlan();
                    UIMap.OpenPlan(PlanNames.HelpPlan);
                    this.UIMap.ClickPlanTabHelpPlan();

                    checkMode = ".Flest";
                    //Helpplan
                    UIMap.SetAndEffectuate(new DateTime(2016, 1, 3), null, actualDate);

                    errorList.AddRange(UIMap.CheckGridData("name", dataSourceDate + checkMode, "PaymentCell_430"));

                    this.UIMap.CloseSalaryCalculationWindow();
                    this.UIMap.DeleteEffectuation();
                    this.UIMap.CloseCurrentPlan();
                    UIMap.DeletePlan(PlanNames.HelpPlan);

                    //Part 9.7 Start
                    UIMap.OpenPlan(PlanNames.CopiedPlan);

                    //Check NightshiftsOnStartDay
                    UIMap.OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(new DateTime(2016, 1, 3), null, true);
                    //30.03.2015 og antall uker=1.
                    UIMap.CopyLønnsberegningerPlan(UIMap.PlanTypes.HelpPlan, new DateTime(2015, 3, 30), "1");
                    this.UIMap.CloseCurrentPlan();
                    UIMap.OpenPlan(PlanNames.HelpPlan);
                    this.UIMap.ClickPlanTabHelpPlan();

                    checkMode = ".Start";
                    //Helpplan
                    UIMap.SetAndEffectuate(new DateTime(2016, 1, 3), null, actualDate);

                    errorList.AddRange(UIMap.CheckGridData("name", dataSourceDate + checkMode, "PaymentCell_430"));

                    this.UIMap.CloseSalaryCalculationWindow();
                    this.UIMap.DeleteEffectuation();
                    this.UIMap.CloseCurrentPlan();
                    UIMap.DeletePlan(PlanNames.HelpPlan);

                    //Åpner Plankopi for teststeg 9.8
                    UIMap.OpenPlan(PlanNames.CopiedPlan);
                }
            }
            catch (Exception ex)
            {
                errorList.Add("Error in SystemTest_021_Step_" + testNo + ": " + ex.Message);
            }

            return errorList;
        }

        public List<String> SystemTest_021_Step_926_936()
        {
            //List<String>
            var errorList = new List<String>();
            try
            {
                //Gyldig til 28.12.2014
                //9.26
                UIMap.OpenPlan(PlanNames.FixedPaymentsPlan);
                UIMap.EditRosterplanSettings();

                this.UIMap.CloseCurrentPlan();
                UIMap.OpenPlan(PlanNames.FixedPaymentsPlan);
                this.UIMap.ClickRecalculate();

                try
                {
                    //errorList.AddRange(UIMap.UIMapVS2017.CheckFixedPaymentsGridDataStep_9_26());
                    UIMap.UIMapVS2017.CheckFixedPaymentsGridDataStep_9_26();
                }
                catch (Exception e)
                {
                    errorList.Add("Feil i Teststeg: 9.26:" + e.Message);
                }


                TestContext.WriteLine("Teststeg: 9.26");

                //9.27
                //Type '2015-12-14' in 'leDisplayStartDate' LookUpEdit
                UIMap.OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(null, new DateTime(2014, 8, 18));//"2014-08-18"

                this.UIMap.ClickRecalculate();
                //Sjekker at verdier ikke er endret
                try
                {
                    //errorList.AddRange(UIMap.UIMapVS2017.CheckFixedPaymentsGridDataStep_9_26());
                    UIMap.UIMapVS2017.CheckFixedPaymentsGridDataStep_9_26();
                }
                catch (Exception e)
                {
                    errorList.Add("Feil i Teststeg: 9.27:" + e.Message);
                }

                TestContext.WriteLine("Teststeg: 9.27");

                //9.28
                this.UIMap.CheckAveragePrMonth();

                try
                {  //errorList.AddRange(UIMap.UIMapVS2017.CheckFixedPaymentsGridDataStep_9_28());
                    UIMap.UIMapVS2017.CheckFixedPaymentsGridDataStep_9_28();
                }
                catch (Exception e)
                {
                    errorList.Add("Feil i Teststeg: 9.28:" + e.Message);
                }


                TestContext.WriteLine("Teststeg: 9.28");
                this.UIMap.CloseCurrentPlan();

                //9.36
                //Gå på Nivå og velg avdeling ”5110 – Diverse”. 
                //Velg fane Arbeidsplan og åpne planen som heter ”UTA i rullerende plan”. 
                //Sjekk at underfane Tillegg beregner 12,5 timer UTA (Utvidet arbeidstid) totalt for perioden og 18,05 timer gjennomsnitt pr. måned.

                this.UIMap.ClickChangeDepartmentFromRosterPlan();
                UIMap.SelectDepartmentByName(UIMap.DepDiverse);
                UIMap.OpenPlan(PlanNames.ExtendeWorktimePlan);

                this.UIMap.ClickRecalculate();

                errorList.AddRange(UIMap.CheckFixedPaymentsGridData("", "9.36.1", 0)); //"PaymentCell_510"
                this.UIMap.CheckAveragePrMonth();

                errorList.AddRange(UIMap.CheckFixedPaymentsGridData("", "9.36.2", 0)); //"PaymentCell_510"

                TestContext.WriteLine("Teststeg: 9.36");
                this.UIMap.CloseCurrentPlan();
            }
            catch (Exception ex)
            {
                errorList.Add("Error in SystemTest_021_Step_926_936: " + ex.Message);
            }

            return errorList;
        }        

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    //UIMap.LaunchGat(true);
        //}

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            UIMap.KillGatProcess();
        }

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
