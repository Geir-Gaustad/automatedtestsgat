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
using System.Threading;
using System.Globalization;


namespace _055_Test_AML
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest_055
    {

        //[TestMethod]
        //public void Test_055()
        //{
        //    var errorList = new List<string>();
        //    Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
        //    Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

        //    ////2060
        //    //this.UIMap.SelectAmlBrakesEmployeeTab();
        //    //Playback.Wait(2000);
        //    //this.UIMap.SelectEkstraEmployeeTab();
        //    //Playback.Wait(2000);
        //    //this.UIMap.SelectCalloutsEmployeeTab();
        //    //Playback.Wait(2000);
        //    //this.UIMap.SelectEmploymentTabFromEmployee();
        //    //Playback.Wait(2000);
        //    //this.UIMap.SelectAmlDispEmployeeTab();
        //    //Playback.Wait(2000);
        //    //this.UIMap.SelectDayAndWeekSeparationEmployeeTab();
        //    //Playback.Wait(2000);
        //    //this.UIMap.SelectAmlBrakesEmployeeTabWhenOnRow2();

        //    ////swe     
        //    //Thread.CurrentThread.CurrentCulture = new CultureInfo("sv-SE");
        //    //this.UIMap.SelectAmlBrakesEmployeeTabSweLineOne();
        //    //this.UIMap.SelectDayAndWeekSeparationEmployeeTabSe();
        //    //this.UIMap.SelectAmlBrakesEmployeeTabSweLineTwo();


        //    //UIMap.DeleteReportFiles();




        //    if (errorList.Count <= 0) return;
        //    UIMap.AssertResults(errorList);
        //}
       
        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_A_Chapter1()
        {
            var errorList = new List<string>();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            ///*
            //    Arbeidsplaner:
            //    Teodor Tesla
            //    Systemtest Generell AML
            //    Grunnlag AML-TEST 1.
            // */

            UIMap.RestoreDatabase();
            UIMap.DeleteReportFiles();

            var timeBefore = DateTime.Now;

            UIMap.StartGat(true, true);
            UIMap.SelectFromAdministration("GLOBALT OPPSETT +Generelle");

            //Step 1
            this.UIMap.SelectAmlGlobalSettings();
            try
            {
                this.UIMap.CheckAmlSettings();
                TestContext.WriteLine("CheckAmlSettings(Chapter_1 step_1): OK");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            this.UIMap.CancelAmlSettingsGlobalWindow();

            //Step 2
            UIMap.SelectFromAdministration("ÅRSAKSKODER FOR AML +Definisjon +av +ulike");
            this.UIMap.AddCauseCodeStorArbeidsmengde();

            //Setter minimum arbeidsfri før vakt til 9timer
            UIMap.SelectFromAdministration("AML-AVTALER");
            UIMap.ChangeAmlDayWorkerMinBeforeShift();
            UIMap.ChangeAmlDnlf38FreeBeforeShift();

            //Step 3
            this.UIMap.SelectMainWindowEmployeeTab();

            try
            {
                UIMap.CheckAmlEmpList("_chapter1_step_3");
                TestContext.WriteLine("CheckAmlEmpList(Chapter_1 step_3): OK");
            }
            catch (Exception e)
            {
                errorList.Add("chapter1_step_3: " + e.Message);
            }

            //Step 4
            UIMap.SelectMainWindowRosterplanTab();

            UIMap.SelectRosterPlan("Grunnlag AML-TEST 1.");
            UIMap.SelectRosterplanPlanTab();

            //Copy Plan to current year
            UIMap.CreateRosterplanCopy("Grunnlag AML-TEST 1");

            UIMap.SelectRosterPlan("Grunnlag AML-TEST 1");
            UIMap.SetRosterplanValidDate();

            //Effectuate
            var from = UIMap.GetDateRelativeToEffectuationDate(0);
            var to = UIMap.GetDateRelativeToEffectuationEndDate(0);

            errorList.AddRange(UIMap.EffectuatePlanForXXWeeks(from, to));

            //Step 5
            UIMap.SelectReport("AML BRUDD", true);
            UIMap.InsertReport7Data("");
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter1_step_5"));

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.1"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter A finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_B_Chapter2()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeBefore = DateTime.Now;

            //Step 1
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til fredag uke 1
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(4));

            //Step 2
            //Cesar Ekstra
            this.UIMap.SelectFirstLineInDayColumnShiftBook();
            errorList.AddRange(UIMap.CreateExtraShift("_chapter2_step_2", "", "A{SPACE}"));

            //Step 3
            this.UIMap.SelectFirstLineInDayColumnShiftBook();
            errorList.AddRange(UIMap.CreateExtraShift("_chapter2_step_3", "", "D{SPACE}", UIMap.GetDateRelativeToEffectuationDate(5)));

            //Step 4
            //Gå til søndag uke 1
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(6));
            this.UIMap.SelectVakantAshiftSundayWeek1();
            errorList.AddRange(UIMap.CreateExtraShift("_chapter2_step_4", "cesar", ""));

            //Step 5
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter2_step_5"));

            //Step 6
            UIMap.SelectReport("AML BRUDD", false);
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter2_step_6"));

            //Step 7
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectEkstraEmployeeTab();
            this.UIMap.DeleteExtrashiftSundayWeek1FromEmployeeTab();

            //Step 8
            UIMap.SelectReport("AML BRUDD", false);
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter2_step_8"));

            //Step 9
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter2_step_9"));

            //Step 10
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til mandag i uke 3
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(14));

            this.UIMap.SelectFirstLineInDayColumnShiftBook();
            //Eksta D-vakt tirsdag uke 3
            errorList.AddRange(UIMap.CreateExtraShift("", "", "D{SPACE}", UIMap.GetDateRelativeToEffectuationDate(15)));

            this.UIMap.SelectFirstLineInDayColumnShiftBook();
            //Eksta A-vakt lørdag uke 4
            errorList.AddRange(UIMap.CreateExtraShift("_chapter2_step_10", "", "A{SPACE}", UIMap.GetDateRelativeToEffectuationDate(26)));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter2_step_10"));

            //Step 11
            UIMap.SelectReport("AML BRUDD", false);
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter2_step_11"));

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.2"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter B finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_C_Chapter3()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeBefore = DateTime.Now;

            //Fravær
            //Step 1
            //Gå til tirsdag i uke 5
            UIMap.SelectMainWindowShiftBookTab();
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(29));

            this.UIMap.SelectFirstLineInDayColumnShiftBook();
            UIMap.CreateHourlyAbsence("30", "07:00", "08:00");
            this.UIMap.SelectFirstLineInDayColumnShiftBook();
            UIMap.CreateHourlyAbsence("30", "14:00", "15:00");

            //Step 2
            //Gå til lørdag i uke 1
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(5));

            this.UIMap.SelectFirstLineInDayColumnShiftBook();
            UIMap.CreateAbsence("beregning");

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_2"));

            //Step 3 (slett fraværet)
            UIMap.SelectMainWindowShiftBookTab();
            this.UIMap.SelectLineInAbsenceColumnShiftBook("0");

            errorList.AddRange(UIMap.ClickDeleteShiftButton());
            errorList.AddRange(UIMap.CheckRecalculationActive("step 3"));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_3"));

            //Step 4            
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til onsdag i uke 5
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(30));
            this.UIMap.SelectFirstLineInEveningColumnShiftBook();
            UIMap.CreateAbsence("beregning");

            //Step 5
            errorList.AddRange(UIMap.CreateExtraShift("", "cesar", "D{SPACE}", UIMap.GetDateRelativeToEffectuationDate(31)));
            this.UIMap.SelectLineInAbsenceColumnShiftBook("0");
            errorList.AddRange(UIMap.ClickDeleteShiftButton());
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_5"));

            //Step 6
            UIMap.SelectMainWindowShiftBookTab();
            this.UIMap.SelectFirstLineInEveningColumnShiftBook();
            //19.09.2016 - 02.10.2016
            UIMap.CreateAbsence("20{Down}", "", UIMap.GetDateRelativeToEffectuationDate(21), UIMap.GetDateRelativeToEffectuationDate(34));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_6"));

            //Step 7
            UIMap.SelectMainWindowShiftBookTab();
            this.UIMap.SelectLineInAbsenceColumnShiftBook("0");
            errorList.AddRange(UIMap.ClickDeleteShiftButton());
            errorList.AddRange(UIMap.CheckRecalculationActive("step 7"));

            //Step 8
            //Gå til mandag uke 5
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(28));
            //Første forskyvning på Bosse
            this.UIMap.SelectFirstLineInNightColumnShiftBook();
            errorList.AddRange(UIMap.CreateRemannageShift("_chapter3_step_8", "32"));

            UIMap.SelectMainWindowShiftBookTab();
            //Gå til tirsdag uke 5
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(29));
            //Andre forskyvning på Bosse
            this.UIMap.SelectFirstLineInNightColumnShiftBook();
            errorList.AddRange(UIMap.CreateRemannageShift("_chapter3_step_8_2", "30"));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            //Sende inn ansattnavn(linje)
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_8"));

            //Step 9
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til onsdag i uke 6
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(37));
            this.UIMap.SelectFirstLineInNightColumnShiftBook();
            UIMap.CreateAbsence("beregning");

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_9"));

            //Step 10
            UIMap.SelectMainWindowShiftBookTab();
            this.UIMap.SelectLineInAbsenceColumnShiftBook("0");
            errorList.AddRange(UIMap.ClickDeleteShiftButton());

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_10"));

            //Step 11
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til onsdag i uke 6
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(37));
            this.UIMap.SelectFirstLineInNightColumnShiftBook();
            UIMap.CreateAbsence("kurs");

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_11"));

            //Step 12
            UIMap.SelectMainWindowShiftBookTab();
            this.UIMap.SelectLineInAbsenceColumnShiftBook("0");
            errorList.AddRange(UIMap.ClickDeleteShiftButton());

            //Step 13

            //Gå til fredag i uke 6
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(39));
            this.UIMap.SelectFirstLineInEveningColumnShiftBook();

            //03.10.2016 - 09.10.2016
            UIMap.CreateGradedAbsence("20{Down}", "", UIMap.GetDateRelativeToEffectuationDate(35), UIMap.GetDateRelativeToEffectuationDate(41));


            //Timefravær på en av vaktene(A vakt fredg uke 6)
            this.UIMap.SelectFirstLineInEveningColumnShiftBook();
            UIMap.CreateHourlyAbsence("30", "15:00", "16:00");


            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_13"));


            //Step 14
            UIMap.SelectMainWindowShiftBookTab();
            this.UIMap.SelectLineInAbsenceColumnShiftBook("0");
            errorList.AddRange(UIMap.ClickDeleteShiftButton());

            this.UIMap.SelectLineInAbsenceColumnShiftBook("0");
            errorList.AddRange(UIMap.ClickDeleteShiftButton());

            //Step 15
            //Timefravær på en av vaktene(A vakt fredg uke 6)
            this.UIMap.SelectFirstLineInEveningColumnShiftBook();
            UIMap.CreateHourlyAbsence("30", "15:00", "16:00");

            this.UIMap.SelectFirstLineInEveningColumnShiftBook();

            //03.10.2016 - 09.10.2016
            UIMap.CreateGradedAbsence("20{Down}", "", UIMap.GetDateRelativeToEffectuationDate(35), UIMap.GetDateRelativeToEffectuationDate(41));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_15"));

            //Step 16
            //Dette punktet utgår da det nå er sperret for å forskyve en vakt med timefravær

            //Step 17
            UIMap.SelectMainWindowShiftBookTab();
            this.UIMap.SelectLineInAbsenceColumnShiftBook("1");
            errorList.AddRange(UIMap.ClickDeleteShiftButton());

            this.UIMap.SelectLineInAbsenceColumnShiftBook("0");
            errorList.AddRange(UIMap.ClickDeleteShiftButton());

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter3_step_17"));

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.3"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter C finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_D_Chapter4()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeBefore = DateTime.Now;

            //Forskjøvet vakt
            //Step 1
            UIMap.SelectMainWindowShiftBookTab();

            //Gå til torsdag i uke 12
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(80));

            this.UIMap.SelectFirstLineInNightColumnShiftBook();
            errorList.AddRange(UIMap.CreateRemannageShift("_chapter4_step_1", "", null, "N{SPACE}", UIMap.GetDateRelativeToEffectuationDate(67)));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Cesar");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter4_step_1"));

            //Step 2
            UIMap.SelectMainWindowShiftBookTab();

            //Gå til tirsdag i uke 11
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(71));
            this.UIMap.SelectFirstLineInDayColumnShiftBook();

            errorList.AddRange(UIMap.CreateRemannageShift("_chapter4_step_2", "0", UIMap.GetDateRelativeToEffectuationDate(63)));
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(63));
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter4_step_2"));

            //Step 3
            UIMap.SelectMainWindowShiftBookTab();

            //Gå til fredag i uke 10
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(67));
            this.UIMap.SelectFirstLineInNightColumnShiftBook();

            errorList.AddRange(UIMap.CreateRemannageShift("_chapter4_step_3", "10"));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter4_step_3"));

            //Step 4
            UIMap.SelectMainWindowShiftBookTab();

            //Gå til lørdag i uke 11
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(75));
            this.UIMap.SelectFirstLineInDayColumnShiftBook();

            errorList.AddRange(UIMap.CreateRemannageShift("_chapter4_step_4", "1", UIMap.GetDateRelativeToEffectuationDate(67)));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter4_step_4"));

            //Step 5
            UIMap.SelectReport("AML BRUDD", false);
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter4_step_5"));

            //Step 6
            //sett på huk for Juster døgnskille
            UIMap.SelectFromAdministration("GLOBALT OPPSETT +Generelle");

            this.UIMap.SelectAmlGlobalSettings();
            this.UIMap.CheckAmlSettingsAdjustDaySeparation(true);
            this.UIMap.ClickSaveGlobalAmlSettings();

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter4_step_6"));

            //Step 7
            UIMap.SelectReport("AML BRUDD", false);
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter4_step_7"));

            //Step 8
            //Ta bort huk for Juster døgnskille
            UIMap.SelectFromAdministration("GLOBALT OPPSETT +Generelle");

            this.UIMap.SelectAmlGlobalSettings();
            this.UIMap.CheckAmlSettingsAdjustDaySeparation(false);
            this.UIMap.ClickSaveGlobalAmlSettings();

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter4_step_8"));

            //Step 9        
            //Slett alle forskyvinger på Cesar    
            UIMap.SelectMainWindowShiftBookTab();

            //Mandag i uke 10
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(63));
            this.UIMap.SelectFirstLineInEveningColumnShiftBook();
            errorList.AddRange(UIMap.ClickDeleteShiftButton());

            //Fredag i uke 10
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(67));
            this.UIMap.SelectFirstLineInDayColumnShiftBook();
            errorList.AddRange(UIMap.ClickDeleteShiftButton());

            //lørdag i uke 10
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(68));
            this.UIMap.SelectFirstLineInEveningColumnShiftBook();
            errorList.AddRange(UIMap.ClickDeleteShiftButton());

            //Fredag i uke 10
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(67));
            this.UIMap.SelectFirstLineInNightColumnShiftBook();
            errorList.AddRange(UIMap.ClickDeleteShiftButton());

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter4_step_9"));

            //Step 7
            UIMap.SelectReport("AML BRUDD", false);
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter4_step_9_2"));

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.4"));    

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter D finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_E_Chapter5()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeBefore = DateTime.Now;

            //Bytte
            //Step 1
            //Gå til onsdag i uke 12
            UIMap.SelectMainWindowShiftBookTab();
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(79));
            this.UIMap.SelectFirstLineInNightColumnShiftBook();

            //N vakt Cesar
            //A vask Bosse
            errorList.AddRange(UIMap.CreateExchange("", "Bosse", UIMap.GetDateRelativeToEffectuationDate(61), UIMap.GetDateRelativeToEffectuationDate(79), new List<string>() { "10" }, new List<string>() { "0" }, "_chapter5_step_1"));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Cesar");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_1"));

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_1_2"));

            //Step 2
            //Sett på huk for Bruk orginalvakt som grunnlag ved bytte
            UIMap.SelectFromAdministration("GLOBALT OPPSETT +Generelle");
            this.UIMap.SelectAmlGlobalSettings();
            UIMap.CheckAmlSettingsUseOrgShiftExchange(true);
            this.UIMap.ClickSaveGlobalAmlSettings();

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Cesar");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_2"));

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_2_2"));

            //Step 3
            //Sett på huk for Bruk orginalvakt som grunnlag ved bytte
            UIMap.SelectFromAdministration("GLOBALT OPPSETT +Generelle");
            this.UIMap.SelectAmlGlobalSettings();
            this.UIMap.CheckAmlSettingsUseOrgShiftExchange(false);
            this.UIMap.ClickSaveGlobalAmlSettings();

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Cesar");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_3"));

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_3_2"));

            //Step 4
            UIMap.SelectMainWindowShiftBookTab();

            //Mandag i uke 10'
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(63));
            this.UIMap.SelectFirstLineInDayColumnShiftBook();

            //Tirsdag i uke 8
            //A vakt Cesar
            //D vakt Bosse
            errorList.AddRange(UIMap.CreateExchange("", "Cesar", UIMap.GetDateRelativeToEffectuationDate(50), UIMap.GetDateRelativeToEffectuationDate(63), new List<string>() { "7" }, new List<string>() { "0" }, "_chapter5_step_4"));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Cesar");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_4"));

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_4_2"));

            //Step 5
            //Ekstra A vakt 16.10.2016 Bosse        
            //Gå til søndag i uke 7
            UIMap.SelectMainWindowShiftBookTab();
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(48));
            this.UIMap.SelectFirstLineInEveningColumnShiftBook();

            errorList.AddRange(UIMap.CreateExtraShift("_chapter5_step_5", "Bosse", ""));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_5"));

            //Step 6
            //Gå til mandag i uke 7
            UIMap.SelectMainWindowShiftBookTab();
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(42));
            this.UIMap.SelectFirstLineInNightColumnShiftBook();

            //Mandag, tirsdag, fredag N vakter uke 7 Andersen
            //lørdag, søndag vakter A, D uke 3 Bosse
            errorList.AddRange(UIMap.CreateExchange("", "Bosse", UIMap.GetDateRelativeToEffectuationDate(19), UIMap.GetDateRelativeToEffectuationDate(46), new List<string>() { "12", "13", "16" }, new List<string>() { "0", "1" }, "_chapter5_step_6"));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Andre");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_6"));

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter5_step_6_2"));

            //Step 7
            UIMap.SelectReport("AML BRUDD", false);
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter5_step_7"));

            //Step 8
            UIMap.SelectMainWindowShiftBookTab();

            //Marker byttevakt
            this.UIMap.SelectFirstLineInNightColumnShiftBook();

            //slett multibytte
            this.UIMap.SelectShiftsInDeleteShiftsWindow(true);

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.5"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter E finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_F_Chapter6()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeBefore = DateTime.Now;

            //Bytte med avdeling
            //Step 1
            //Gå til fredag i uke 9
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(60));
            this.UIMap.SelectRowInDayColumnShiftBook("4");
            this.UIMap.CreateDepartmentExchange("Andersen", UIMap.GetDateRelativeToEffectuationDate(60), UIMap.GetDateRelativeToEffectuationDate(60), "_chapter6_step_1");

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Andre");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter6_step_1"));

            //Step 2
            //Gå til mandag i uke 11
            UIMap.SelectMainWindowShiftBookTab();
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(70));
            this.UIMap.SelectRowInInEveningColumnShiftBook("1");
            errorList.AddRange(this.UIMap.CreateDepartmentExchange("Andersen", UIMap.GetDateRelativeToEffectuationDate(70), UIMap.GetDateRelativeToEffectuationDate(74), "_chapter6_step_2", new List<string>() { "1", "4", "14" }));
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Andre");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter6_step_2"));

            //Step 3
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til fredag i uke 12
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(81));
            this.UIMap.SelectFirstLineInNightColumnShiftBook();
            //fredag i uke 5 til fredag i uke 12
            errorList.AddRange(this.UIMap.CreateDepartmentExchange("Andersen", UIMap.GetDateRelativeToEffectuationDate(32), UIMap.GetDateRelativeToEffectuationDate(81), "_chapter6_step_3", new List<string>() { "1", "2", "5", "11" }));
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Andre");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter6_step_3"));

            UIMap.SelectReport("AML BRUDD", false);
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter6_step_3_2"));

            //Step 4
            //Sett på huk for Bruk orginalvakt som grunnlag ved avdelingsbytte
            UIMap.SelectFromAdministration("GLOBALT OPPSETT +Generelle");
            this.UIMap.SelectAmlGlobalSettings();
            this.UIMap.CheckAmlSettingsUseOrgShiftDepartmentExchange(true);
            this.UIMap.ClickSaveGlobalAmlSettings();

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Andre");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter6_step_4"));

            //Step 5
            //Sett på huk for Bruk orginalvakt som grunnlag ved avdelingsbytte
            UIMap.SelectFromAdministration("GLOBALT OPPSETT +Generelle");
            this.UIMap.SelectAmlGlobalSettings();
            this.UIMap.CheckAmlSettingsUseOrgShiftDepartmentExchange(false);
            this.UIMap.ClickSaveGlobalAmlSettings();

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Andre");
            errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter6_step_5"));

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.6"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter F finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_G_Chapter7()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeBefore = DateTime.Now;

            //Utrykning
            //Step 1
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til mandag uke 2
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(7));
            //Gaston utrykning
            this.UIMap.SelectRowInHomeShiftColumnShiftBook("0");
            errorList.AddRange(UIMap.CreateCallOut("_chapter7_step_1", null, "00.00", "05.00", true, false));

            //Step 2
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Gaston");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter7_step_2"));

            //Step 3
            //Hack: DelphiTabkomponent
            this.UIMap.SelectCalloutsEmployeeTab();
            this.UIMap.EditCalloutFromEmployeeTab();
            UIMap.EditCalloutAndSetSevereServiceDisruption(true, "_chapter7_step_3");

            //Step 4
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Gaston");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter7_step_4"));
            UIMap.SelectMainWindowShiftBookTab();
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(8));
            this.UIMap.SelectRowInHomeShiftColumnShiftBook("0");
            errorList.AddRange(UIMap.CreateCallOut("_chapter7_step_4", null, "01:00", "06:00", true, true));
            errorList.AddRange(UIMap.CreateCallOut("_chapter7_step_4_2", UIMap.GetDateRelativeToEffectuationDate(9), "01:00", "05:00", true));

            UIMap.SelectReport("AML OVERSIKT", true);
            this.UIMap.InsertReport77Data();
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter7_step_4_2"));

            //Step 5
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til søndag uke 1
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(6));

            this.UIMap.SelectRowInHomeShiftColumnShiftBook("1");
            errorList.AddRange(UIMap.CreateCallOut("_chapter7_step_5", null, "06:00", "07:01", false));

            //Step 6
            //Gå til lørdag uke 1
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(5));
            this.UIMap.SelectRowInHomeShiftColumnShiftBook("1");
            errorList.AddRange(UIMap.CreateCallOut("", null, "16:00", "17:00", true));

            //Step 7
            //Gå til fredag uke 2
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(11));
            this.UIMap.SelectRowInHomeShiftColumnShiftBook("0");
            errorList.AddRange(UIMap.CreateCallOut("", null, "06:00", "09:00", false));

            //Step 8
            //Gå til lørdag i uke 4
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(26));
            this.UIMap.SelectFirstRowInOnCallShiftColumnShiftBook();
            errorList.AddRange(UIMap.CreateCallOut("", null, "13:00", "22:00", true, true));
            errorList.AddRange(UIMap.CreateCallOut("_chapter7_step_8", UIMap.GetDateRelativeToEffectuationDate(27), "14.00", "23.00", true));

            //Step 9
            //Gå til søndag i uke 4
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(27));
            //Utrykning over døgnskille søndag i uke 4
            UIMap.SelectRowInOnCallShiftColumnShiftBook("1");
            errorList.AddRange(UIMap.CreateCallOut("_chapter7_step_9", null, "23:00", "01:00", true));

            //Step 10
            //Gå til tordag i uke 7
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(45));
            UIMap.SelectRowInHomeShiftColumnShiftBook("0");
            errorList.AddRange(UIMap.CreateCallOut("_chapter7_step_10", null, "16:00", "20:30", true, true));
            errorList.AddRange(UIMap.CreateCallOut("_chapter7_step_10_2", UIMap.GetDateRelativeToEffectuationDate(46), "01:00", "06:00 ", true));

            //Step 11
            UIMap.SelectReport("AML OVERSIKT", false);
            this.UIMap.InsertReport77Data();
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter7_step_11"));

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.7"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter G finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_H_Chapter8()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions; ;

            var timeBefore = DateTime.Now;

            //Dispensasjoner
            //Step 1
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlDispEmployeeTab();

            //Sende inn ansattnavn(linje)
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Gaston");


            UIMap.GetDateRelativeToEffectuationEndDate(0);
            this.UIMap.CreateAmlDispension(UIMap.DispTypes.UkentligArbFri, "31", UIMap.GetFirstDateOfCurrentYear(), UIMap.GetLastDateOfCurrentYear());

            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter8_step_1"));

            //Step 2
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlDispEmployeeTab();

            //Sende inn ansattnavn(linje)
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Gaston");
            //Register fra mandag i uke 3
            this.UIMap.CreateAmlDispension(UIMap.DispTypes.SamletTidPrDag, "16", UIMap.GetDateRelativeToEffectuationDate(14), UIMap.GetLastDateOfCurrentYear());

            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter8_step_2"));

            UIMap.SelectReport("AML BRUDD", true);
            UIMap.InsertReport7Data("");

            errorList.AddRange(UIMap.SaveReportAsXls("_chapter8_step_2_2"));

            //Step 3
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            //Sende inn ansattnavn(linje)
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter8_step_3"));

            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlDispEmployeeTab();
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");

            //Register fra mandag i uke 3
            this.UIMap.CreateAmlDispension(UIMap.DispTypes.SøndagerPåRad, "3", UIMap.GetDateRelativeToEffectuationDate(14), UIMap.GetLastDateOfCurrentYear());

            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter8_step_3_2"));

            //Step 4
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlDispEmployeeTab();
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");

            //Register fra mandag i uke 3
            this.UIMap.CreateAmlDispension(UIMap.DispTypes.SamletTidPrDag, "14", UIMap.GetDateRelativeToEffectuationDate(14), UIMap.GetDateRelativeToEffectuationDate(69));

            //Register fra mandag i uke 6
            this.UIMap.CreateAmlDispension(UIMap.DispTypes.SamletTidPrDag, "16", UIMap.GetDateRelativeToEffectuationDate(35), UIMap.GetDateRelativeToEffectuationDate(48));

            //Legge inn ekstravakter på Bosse for å trigge Amlbrudd samlet pr. dag.
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til tirsdag uke 2. D vakt Bosse
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(8));

            //Ekstra
            this.UIMap.SelectFirstLineInDayColumnShiftBook();
            errorList.AddRange(UIMap.CreateExtraShift("_chapter8_step_4", "", "A{SPACE}"));

            //Gå til onsdag uke 3. D vakt Bosse
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(16));
            //Ekstra
            this.UIMap.SelectFirstLineInDayColumnShiftBook();
            errorList.AddRange(UIMap.CreateExtraShift("_chapter8_step_4_2", "", "A{SPACE}"));

            //Gå til tirsdag uke 6. D vakt Bosse
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(36));
            //Ekstra
            this.UIMap.SelectRowInDayColumnShiftBook("1");
            errorList.AddRange(UIMap.CreateExtraShift("", "", "A{SPACE}", null, "", "", false, true));
            errorList.AddRange(UIMap.CreateExtraShift("_chapter8_step_4_3", "", "N8{SPACE}", null, "", "", false, false, true));

            //Gå til tirsdag uke 8. D vakt Bosse
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(50));
            //Ekstra
            this.UIMap.SelectRowInDayColumnShiftBook("0");
            errorList.AddRange(UIMap.CreateExtraShift("_chapter8_step_4_4", "", "N1{SPACE}"));

            //Gå til tirsdag uke 11. N vakt Bosse
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(71));
            //Ekstra
            this.UIMap.SelectFirstLineInNightColumnShiftBook();
            errorList.AddRange(UIMap.CreateExtraShift("_chapter8_step_4_5", "", "D{SPACE}"));

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.8"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter H finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_I_Chapter9()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeBefore = DateTime.Now;

            //Step 1
            //Aml-avtaler
            //Sjekker avtaler
            UIMap.SelectFromAdministration("AML-AVTALER");
            this.UIMap.CheckAmlAgreementsPrio();
            UIMap.CloseAdminAmlAgrementWindow();

            //Step 2
            UIMap.SelectMainWindowShiftBookTab();
            UIMap.ChangeDepartment();

            UIMap.SelectMainWindowRosterplanTab();

            //this.UIMap.OpenRosterplan();
            UIMap.SelectRosterPlan("Teodor Tesla");
            UIMap.SelectRosterplanPlanTab();

            //Copy Plan to current year
            UIMap.CreateRosterplanCopy("Teodor Tesla 1");

            UIMap.SelectRosterPlan("Teodor Tesla 1");
            UIMap.SetRosterplanValidDate();

            var from = UIMap.GetDateRelativeToEffectuationDate(0);
            var to = UIMap.GetDateRelativeToEffectuationEndDate(0);
            errorList.AddRange(UIMap.EffectuatePlanForXXWeeks(from, to));

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            //Sende inn ansattnavn(linje)
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Teodor");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter9_step_2"));

            //Step setter ny grense
            UIMap.SelectFromAdministration("AML-AVTALER");
            //Tirsdag uke 2, "06.09.2016";
            UIMap.ChangeAmlDnlf38(UIMap.GetDateRelativeToEffectuationDate(8));

            //Step 4
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            //Sende inn ansattnavn(linje)
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Teodor");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter9_step_4"));

            //Step 5 -(6 kan ikke sjekke i visualisering)
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til tirsdag uke 2
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(8));
            this.UIMap.SelectRowInInEveningColumnShiftBook("2");
            errorList.AddRange(UIMap.CreateExtraShift("_chapter9_step_5", "", "", null, "09:59", "15:00"));

            //Step 7-(8 kan ikke sjekke i visualisering)
            UIMap.SelectFromAdministration("AML-AVTALER");
            //Turnus 35.5
            UIMap.SelectAmlAgrement("8");
            this.UIMap.ChangeAmlTurnus35_5("0");
            UIMap.CloseAdminAmlAgrementWindow();

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            //Sende inn ansattnavn(linje)
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Teodor");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter9_step_7"));

            //Step 9
            UIMap.SelectFromAdministration("AML-AVTALER");
            //Turnus 35.5
            UIMap.SelectAmlAgrement("0");
            this.UIMap.ChangeAmlTurnus35_5("9");
            UIMap.CloseAdminAmlAgrementWindow();

            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            //Sende inn ansattnavn(linje)
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Teodor");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter9_step_9"));

            //Step 10 - (11 kan ikke sjekke i visualisering)
            //Hack: DelphiTabkomponent
            this.UIMap.SelectEmploymentTabFromEmployee();
            this.UIMap.EditEmployment(UIMap.GetDateRelativeToEffectuationDate(42));
            this.UIMap.NewEmployment(UIMap.GetDateRelativeToEffectuationDate(43));

            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();

            //Sende inn ansattnavn(linje)
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Teodor");
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter9_step_10"));

            //Step 12
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlDispEmployeeTab();

            //Sende inn ansattnavn(linje)
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Teodor");
            this.UIMap.CreateAmlDispension(UIMap.DispTypes.SamletPrUkeSnitt, "51", UIMap.GetDateRelativeToEffectuationDate(29), UIMap.GetDateRelativeToEffectuationDate(57));

            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter9_step_12"));

            //Step 13
            UIMap.SelectMainWindowShiftBookTab();
            //Gå til fredag uke 1
            UIMap.GoToShiftDate(UIMap.GetDateRelativeToEffectuationDate(50));

            //Tesla Ekstra
            this.UIMap.SelectRowInInEveningColumnShiftBook("1");
            errorList.AddRange(UIMap.CreateExtraShift("_chapter9_step_13", "", "N2{SPACE}", null, "", "", true));

            //Step 14
            this.UIMap.SelectMainWindowEmployeeTab();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTab();
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Teodor");

             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter9_step_14"));

            //Step 15
            //Hack: DelphiTabkomponent
            this.UIMap.SelectDayAndWeekSeparationEmployeeTab();
            this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Ian");

            this.UIMap.CreateWeekSeparation();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectAmlBrakesEmployeeTabWhenOnRow2();
             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter9_step_15"));

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.9"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter I finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_J_Chapter10()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeBefore = DateTime.Now;

            //Setter opp IIS for Aml-kalkulasjoner
            UIMap.GetZipFiles();
            UIMap.ConfigureWS1ForIIS();
            UIMap.ConfigureIIS();

            //Oppsett av accesscode til oversyring av AML-scheduler
            UIMap.SelectFromAdministration("GTWS");
            this.UIMap.InsertAmlAccesscode();

            ////Step 1
            ////1 og 2 kjøres direkte via overide webservice
            //UIMap.SelectFromAdministration("OPPSETT FOR KJØRING AV AML-KALKULASJON");

            ////Step 2
            //this.UIMap.CreateAmlCalculation();
            //UICommon.ClearAdministrationSearchString();

            //Step 3
            var endDate = UIMap.GetDateRelativeToEffectuationDate(55).ToString("ddMMyyyy");
            UIMap.SelectReport("AML BRUDD", true);
            UIMap.InsertReport7Data(endDate, true);
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter10_step_3"));

            //Step 4            
            //Kjører Amlkalkulasjon
            this.UIMap.OpenWeaBreakService("2060", UIMap.GetCurrentYearAsInt());
            this.UIMap.InvokeWeaBreakService();

            Playback.Wait(5000);
            try
            {
                UIMap.OpenWebBrowser(false);
                TestContext.WriteLine("OpenWebBrowser(Chapter_10 step_4): OK");
            }
            catch (Exception)
            {
                this.UIMap.CloseIE();
            }

            //Playback.Wait(10000);
            UIMap.SelectReport("AML BRUDD", false);
            UIMap.InsertReport7Data(endDate, true);
            errorList.AddRange(UIMap.SaveReportAsXls("_chapter10_step_4"));

            Playback.Wait(3000);
            this.UIMap.CloseGat();

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.10"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter J finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        //[TestMethod, Timeout(6000000)]
        //public void SystemTest_055_K_Chapter11_SWE()
        //{
        //    var errorList = new List<string>();
        //    Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
        //    Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

        //    Thread.CurrentThread.CurrentCulture = new CultureInfo("sv-SE");
        //    var timeBefore = DateTime.Now;

        //    try
        //    {
        //        this.UIMap.CloseGat();
        //        Playback.Wait(3000);
        //        UIMap.CopyIniFileToGatsoftCatalog(true);

        //        //Step 1
        //        UIMap.StartGat(false, false, true);

        //        //Ansatt
        //        UIMap.SelectDefinedPointTab(new Point(300, 13));

        //        //ATL brakes
        //        //Hack: DelphiTabkomponent
        //        this.UIMap.SelectAmlBrakesEmployeeTabSweLineOne();

        //        try
        //        {
        //             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter11_SWE_step_1");
        //            TestContext.WriteLine("SaveAmlBrakesAsXlsx(Chapter_11_SWE, step_1): OK");
        //        }
        //        catch (Exception e)
        //        {
        //            errorList.Add(e.Message);
        //        }

        //        //Step 2
        //        //Arbeidsplan
        //        UIMap.SelectDefinedPointTab(new Point(175, 15));
        //        UIMap.SelectRosterPlan("ATL test 1");

        //        this.UIMap.SelectRosterplanPlanTab();

        //        //2016-04-25 - 2016-11-06
        //        var from = UIMap.GetDateRelativeToEffectuationDate(-126);
        //        var to = UIMap.GetDateRelativeToEffectuationDate(69);


        //        errorList.AddRange(UIMap.EffectuatePlanForXXWeeks(from, to));
        //        this.UIMap.CloseRosterplanFromPlanTab();

        //        //Step 3
        //        //Ansatt
        //        UIMap.SelectDefinedPointTab(new Point(300, 13));
        //        //ATL brakes
        //        //Hack: DelphiTabkomponent
        //        this.UIMap.SelectAmlBrakesEmployeeTabSweLineOne();

        //        //Sende inn ansattnavn(linje)
        //        this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Fredrik");

        //        try
        //        {
        //             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter11_SWE_step_3");
        //            TestContext.WriteLine("SaveAmlBrakesAsXlsx(Chapter_11_SWE, step_3): OK");
        //        }
        //        catch (Exception e)
        //        {
        //            errorList.Add(e.Message);
        //        }

        //        //Step 4 - 5
        //        UIMap.SelectMainWindowShiftBookTab();

        //        UIMap.GoToShiftDate(new DateTime(2016, 10, 22));
        //        errorList.AddRange(UIMap.CreateExtraShift("", "ingvar", "D{SPACE}"));
        //        errorList.AddRange(UIMap.CreateExtraShift("_chapter11_SWE_step_4_2", "ingvar", "D{SPACE}", new DateTime(2016, 10, 23)));

        //        //Step 6 -7
        //        errorList.AddRange(UIMap.CreateExtraShift("", "ingvar", "E1{SPACE}", new DateTime(2016, 10, 13)));
        //        errorList.AddRange(UIMap.CreateExtraShift("", "ingvar", "D{SPACE}", new DateTime(2016, 10, 15)));
        //        errorList.AddRange(UIMap.CreateExtraShift("_chapter11_SWE_step_6", "ingvar", "D{SPACE}", new DateTime(2016, 10, 16)));

        //        //Ansatt
        //        UIMap.SelectDefinedPointTab(new Point(300, 13));
        //        this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Bosse");
        //        try
        //        {
        //             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter11_SWE_step_7");
        //            TestContext.WriteLine("SaveAmlBrakesAsXlsx(Chapter_11_SWE, step_7): OK");
        //        }
        //        catch (Exception e)
        //        {
        //            errorList.Add(e.Message);
        //        }

        //        //Step 8
        //        UIMap.SelectMainWindowShiftBookTab();
        //        errorList.AddRange(UIMap.CreateExtraShift("", "Palme", "BJ1{SPACE}"));
        //        errorList.AddRange(UIMap.CreateExtraShift("", "Palme", "BJ1{SPACE}", new DateTime(2016, 10, 15)));
        //        errorList.AddRange(UIMap.CreateExtraShift("", "Palme", "BJ1{SPACE}", new DateTime(2016, 10, 16)));
        //        errorList.AddRange(UIMap.CreateExtraShift("", "Palme", "BJ1{SPACE}", new DateTime(2016, 10, 12)));
        //        errorList.AddRange(UIMap.CreateExtraShift("", "Palme", "BJ1{SPACE}", new DateTime(2016, 10, 8)));
        //        errorList.AddRange(UIMap.CreateExtraShift("_chapter11_SWE_step_8", "Palme", "BJ1{SPACE}", new DateTime(2016, 10, 7)));

        //        //Step 9 
        //        errorList.AddRange(UIMap.CreateExtraShift("_chapter11_SWE_step_9", "Bildt", "D{SPACE}", new DateTime(2016, 10, 18)));

        //        //Step 10
        //        //Ansatt
        //        UIMap.SelectDefinedPointTab(new Point(300, 13));
        //        //Hack: DelphiTabkomponent
        //        this.UIMap.SelectDayAndWeekSeparationEmployeeTabSe();
        //        this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Andre");
        //        this.UIMap.CreateWeekSeparationSE();

        //        //Restarter Gat for at ukeskille skal tre i kraft
        //        Playback.Wait(3000);
        //        this.UIMap.CloseGat();

        //        //Step 11
        //        UIMap.StartGat(false, false, true);
        //        errorList.AddRange(UIMap.CreateExtraShift("_chapter11_SWE_step_11", "Bildt", "D{SPACE}", new DateTime(2016, 10, 26)));

        //        //Step 12 slette carl bildt extravakter
        //        UIMap.GoToShiftDate(new DateTime(2016, 10, 18));
        //        this.UIMap.SelectRowInDayColumnShiftBook("0");
        //        errorList.AddRange(UIMap.ClickDeleteShiftButton());

        //        UIMap.GoToShiftDate(new DateTime(2016, 10, 26));
        //        this.UIMap.SelectRowInDayColumnShiftBook("0");
        //        errorList.AddRange(UIMap.ClickDeleteShiftButton());

        //        //Ansatt
        //        UIMap.SelectDefinedPointTab(new Point(300, 13));
        //        //this.UIMap.SelectAmlBrakesEmployeeTabSweLineTwo(Omstart av gat gjør denne overflødig);
        //        this.UIMap.SelectAmlBrakesEmployeeTabSweLineOne();
        //        this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Andre");
        //        try
        //        {
        //             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter11_SWE_step_12");
        //            TestContext.WriteLine("SaveAmlBrakesAsXlsx(Chapter_11_SWE, step_12): OK");
        //        }
        //        catch (Exception e)
        //        {
        //            errorList.Add(e.Message);
        //        }

        //        //Step 13
        //        //Arbeidsplan
        //        UIMap.SelectDefinedPointTab(new Point(175, 15));
        //        UIMap.SelectRosterPlan("ATL test 1");

        //        this.UIMap.SelectRosterplanPlanTab();
        //        this.UIMap.AddCarlBildt();
        //        this.UIMap.AddCarlBildtShifts();

        //        //2016-10-24 - 2016-11-06
        //        errorList.AddRange(UIMap.EffectuatePlanForXXWeeks(new DateTime(2016, 10, 24), UIMap.GetDateRelativeToEffectuationDate(69)));
        //        this.UIMap.CloseRosterplanFromPlanTab();

        //        //Ansatt
        //        UIMap.SelectDefinedPointTab(new Point(300, 13));
        //        this.UIMap.SelectLineInEmlpoyeeListEmpTabAmlBrakes("Andre");
        //        try
        //        {
        //             errorList.AddRange(UIMap.SaveAmlBrakesAsXlsx("_chapter11_SWE_step_13");
        //            TestContext.WriteLine("SaveAmlBrakesAsXlsx(Chapter_11_SWE, step_13): OK");
        //        }
        //        catch (Exception e)
        //        {
        //            errorList.Add(e.Message);
        //        }

        //        //Step 14
        //        UIMap.SelectMainWindowShiftBookTab();
        //        errorList.AddRange(UIMap.CreateExtraShift("_chapter11_SWE_step_14", "Reinfeldt", "E1{SPACE}", new DateTime(2016, 10, 26)));

        //        //Step 15
        //        errorList.AddRange(UIMap.CreateExtraShift("", "Carlsson", "D{SPACE}", new DateTime(2016, 07, 16)));
        //        errorList.AddRange(UIMap.CreateExtraShift("", "Carlsson", "D{SPACE}", new DateTime(2016, 07, 17)));
        //        errorList.AddRange(UIMap.CreateExtraShift("", "Carlsson", "D{SPACE}", new DateTime(2016, 07, 23)));
        //        errorList.AddRange(UIMap.CreateExtraShift("_chapter11_SWE_step_15", "Carlsson", "D{SPACE}", new DateTime(2016, 07, 24)));

        //        try
        //        {
        //            //Step 16
        //            UIMap.SelectReportSwe("ATL-BROTT");
        //            UIMap.InsertReport7DataSwe();
        //            this.UIMap.SaveReportAsXls("_chapter11_SWE_step_16");
        //            TestContext.WriteLine("SaveReportAsXls(Chapter_11_SWE, step_16): OK");
        //        }
        //        catch (Exception e)
        //        {
        //            errorList.Add("Error chapter11_SWE_step_16: " + e.Message);
        //        }

        //        try
        //        {
        //            //Step 17
        //            UIMap.SelectReportSwe("ATL-ÖVERSIKT");
        //            this.UIMap.InsertReport77DataSwe();
        //            this.UIMap.SaveReportAsXls("_chapter11_SWE_step_17");
        //            TestContext.WriteLine("SaveReportAsXls(Chapter_11_SWE, step_17): OK");
        //        }
        //        catch (Exception e)
        //        {
        //            errorList.Add("Error chapter11_SWE_step_17: " + e.Message);
        //        }

        //        Playback.Wait(3000);
        //        this.UIMap.CloseGat();

        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Error in Chapter_11(SWE): " + e.Message);
        //    }
        //    finally
        //    {
        //        Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
        //        UIMap.CopyIniFileToGatsoftCatalog(false);
        //    }

        //    var timeEnd = DateTime.Now;
        //    errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, kap.11"));

        //    if (errorList.Count <= 0)
        //    {
        //        TestContext.WriteLine("Chapter K finished OK");
        //        return;
        //    }

        //    UIMap.AssertResults(errorList);
        //}

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_L_CleanUpIIS()
        {
            var errorList = new List<string>();
            var timeBefore = DateTime.Now;

            //Rydder opp IIS
            errorList.AddRange(UIMap.Cleanup());

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeEnd, "Tidsforbruk ved kjøring av Test 55, Cleanup"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter L(Cleanup) finished OK");
                return;
            }

            UIMap.AssertResults(errorList);        
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_055_M_CheckValues()
        {
            var errorList = new List<string>();

            try
            {
                errorList.AddRange(UIMap.CompareReportDataFiles_Test055());
            }
            catch (Exception e)
            {
                errorList.Add("Error checking datafiles: " + e.Message);
            }

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter M(CheckValues) finished OK");
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
