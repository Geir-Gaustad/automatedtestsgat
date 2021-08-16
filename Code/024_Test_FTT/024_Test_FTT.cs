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


namespace _024_Test_FTT
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest_024
    {
        private DateTime _CurrentDate = DateTime.Now.Date;


        //[TestMethod]
        //public void Test_024()
        //{
        //    var errorList = new List<string>();
        //    Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
        //    Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

        //    //this.UIMap.SetRosterplanStartDateNew(new DateTime(2016, 1, 4));
        //    //this.UIMap.SetCalendarplanStartDateNew("04.01.2016");
        //    //this.UIMap.SetBaseplanStartDateNew("04.01.2016");
        //    //this.UIMap.EditLinesettingDatesNew(null, null);
        //    //this.UIMap.SetRosterplanValidDateNew();
        //    //this.UIMap.SetCalendarplanStartDateNew();
        //    //this.UIMap.SetLineSettingsValidPeriod(null, null);


        //    foreach (var error in errorList)
        //    {
        //        Debug.WriteLine(error);
        //    }
        //}

        [TestMethod, Timeout(6000000)]
        public void SystemTest_024_Chapter1()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            var timeStart = DateTime.Now;

            //Step 1
            UIMap.StartGat(true);

            //Step 2
            UIMap.OpenPlan("FTT Arbeidsplan");
            this.UIMap.CopyFTTRosterplan(new DateTime(2024, 08, 05));

            //Step 3
            this.UIMap.CloseCurrentRosterplanFromPlanTab();
            UIMap.OpenPlan("Kopi av FTT Arbeidsplan.");

            //Step 4
            this.UIMap.SelectFTTTab();
            try
            {
                this.UIMap.CheckFTThasEmptyList();
                TestContext.WriteLine("Chapter_1 step_4: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_4: " + e.Message);
            }

            //Step 5
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                UIMap.CheckFTTcode400_C1_S5();
                TestContext.WriteLine("Chapter_1 step_5: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_5: " + e.Message);
            }

            try
            {
                UIMap.CheckFTTcode410_C1_S5();
                TestContext.WriteLine("Chapter_1 step_5_2: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_5_2: " + e.Message);
            }

            try
            {
                UIMap.CheckFTTPeriod_C1_S5();
                TestContext.WriteLine("Chapter_1 step_5_3: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_5_3: " + e.Message);
            }

            try
            {
                UIMap.CheckCalculateFTTButtonDisabled();
                TestContext.WriteLine("Chapter_1 step_5_4: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_5_4: " + e.Message);
            }

            //Step 6
            this.UIMap.ClickEditRosterplanFromHomeTab();
            Playback.Wait(500);
            this.UIMap.ClickCellToInsertDshiftMonEkland();

            //Step 7
            this.UIMap.ClickCalculateFFTButton();

            errorList.AddRange(UIMap.CheckNoCalculationEkland_C1_S7());

            //Step 8
            this.UIMap.ClickCellToInsertNshiftMonEkland();
            Playback.Wait(500);
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculationEkland_C1_S8();
                TestContext.WriteLine("Chapter_1 step_8: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_8: " + e.Message);
            }

            //Step 9
            this.UIMap.ClickCellToInsertNshiftMonWeek2_3_Ekland();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculationEkland_C1_S9();
                TestContext.WriteLine("Chapter_1 step_9: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_9: " + e.Message);
            }

            //Step 10
            this.UIMap.ClickOkEditFromHometab();
            this.UIMap.EditLineSettingsLine2Ekland();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                //Verdiene skal være lik step 8
                this.UIMap.CheckCalculationEkland_C1_S8();
                TestContext.WriteLine("Chapter_1 step_10: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_10: " + e.Message);
            }

            //Step 11
            this.UIMap.ClickEditRosterplanFromHomeTab();
            Playback.Wait(500);
            this.UIMap.ChangeDshiftMonEklandToN();
            try
            {
                this.UIMap.CheckTransAndStopIsDisabled_C1_S11();
                TestContext.WriteLine("Chapter_1 step_11: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_11: " + e.Message);
            }

            this.UIMap.ClickOkEditFromHometab();

            //Step 12
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculationEklandTwoLines_C1_S12();
                TestContext.WriteLine("Chapter_1 step_12: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_12: " + e.Message);
            }

            //Step 13
            this.UIMap.ClickTransfereFTT("05.08.2024");
            try
            {
                this.UIMap.CheckFTTEmployeesToTransfere();
                TestContext.WriteLine("Chapter_1 step_13: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_13: " + e.Message);
            }

            //Step 14
            try
            {
                this.UIMap.CheckTransferePeriod();
                TestContext.WriteLine("Chapter_1 step_14: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_14: " + e.Message);
            }

            this.UIMap.ClickTransfereFFTDialogButton();
            try
            {
                this.UIMap.CheckFTT1IsReadyInExportLog();
                TestContext.WriteLine("Chapter_1 step_14_2: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_14_2: " + e.Message);
            }

            //Step 15
            this.UIMap.ClickProcessingLink();
            try
            {

                this.UIMap.CheckTransferedFTTValues();
                TestContext.WriteLine("Chapter_1 step_15: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_15: " + e.Message);
            }

            //Step 16
            this.UIMap.ClickCloseExportDetailsWindow();
            this.UIMap.ClickCloseExportLogWindow();

            //Step 17
            this.UIMap.ExtendRosterperiodWith3Weeks();

            //Step 18
            this.UIMap.SetBergman25PersentLineInactive();

            //Step 19
            this.UIMap.ClickCalculateFFTButton(true);
            try
            {
                this.UIMap.CheckCalculations_C1_S19();
                TestContext.WriteLine("Chapter_1 step_19: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_19: " + e.Message);
            }

            //Step 20
            this.UIMap.ClickTransfereFTT("05.08.2024");
            try
            {
                this.UIMap.CheckTransferPeriod_C1_S20();
                TestContext.WriteLine("Chapter_1 step_20: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_20: " + e.Message);
            }

            //Step 21
            this.UIMap.OpenAdamsMasterDetailsPreviousTransfers();
            try
            {
                this.UIMap.CheckAdamsMasterDetailsPreviousTransfers_C1_S21();
                TestContext.WriteLine("Chapter_1 step_21: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_21: " + e.Message);
            }

            //Step 22
            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickCloseExportLogWindow();

            //Step 23
            this.UIMap.ClickEditRosterplanFromPlanTab();
            Playback.Wait(500);
            this.UIMap.InsertDshift3WeeksEndreGarbo();
            this.UIMap.ClickOkEditFromPlantab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckUTACalculationEndre_Garbo_C1_S23();
                TestContext.WriteLine("Chapter_1 step_23: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_23: " + e.Message);
            }

            //Step 24   
            this.UIMap.EditLineSettingsToDateGarbo_C1_S24();
            this.UIMap.ClickEditRosterplanFromPlanTab();
            Playback.Wait(500);
            this.UIMap.CreateEqualizeShiftWeek1Garbo();
            try
            {
                this.UIMap.ClickOkEditFromPlantab();
            }
            catch (Exception)
            {
                this.UIMap.ClickOkEditFromHometab();
            }

            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckUTACalculationGarbo_C1_S24();
                TestContext.WriteLine("Chapter_1 step_24: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_24: " + e.Message);
            }

            this.UIMap.ShowFTTAsAveragePrMnd();
            try
            {
                this.UIMap.CheckUTCalculationEndre_Garbo_C1_S24();
                TestContext.WriteLine("Chapter_1 step_24_2: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_24_2: " + e.Message);
            }

            //Step 25
            this.UIMap.ClickTransfereFTT("05.08.2024");
            this.UIMap.ShowMasterDetailsEndre_Garbo();
            try
            {
                this.UIMap.CheckTransferUTAandPeriod_C1_S25();
                TestContext.WriteLine("Chapter_1 step_25: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_25: " + e.Message);
            }

            //Step 26
            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickProcessingLink();
            try
            {
                this.UIMap.CheckTransferedFTTValues_C1_S26();
                TestContext.WriteLine("Chapter_1 step_26: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_1 step_26: " + e.Message);
            }

            this.UIMap.ClickCloseExportDetailsWindow();
            this.UIMap.ClickCloseExportLogWindow();

            Playback.Wait(1000);
            try
            {
                this.UIMap.CloseCurrentRosterplanFromPlanTab();
            }
            catch (Exception)
            {
                this.UIMap.CloseCurrentRosterplanFromHomeTab();
            }

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 24, kap. 1"));


            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter1 finished OK");
                return;
            }

            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_024_Chapter2()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeStart = DateTime.Now;

            //Step 1
            UIMap.OpenPlan("FTT Kalenderplan");
            UIMap.CopyCalendarplanCommon(UIMap.CopyFTTKalenderplanParams.UITxtNameEditValueAsString);

            //Step 2
            this.UIMap.CloseCurrentRosterplanFromPlanTab();
            UIMap.OpenPlan(UIMap.CopyFTTKalenderplanParams.UITxtNameEditValueAsString);
            UIMap.EditCalendarplanShift(true);

            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                UIMap.CheckCalculations_C2_S2();
                TestContext.WriteLine("Chapter_2 step_2: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_2: " + e.Message);
            }

            //Step 3
            this.UIMap.ShowFTTAsAveragePrMnd();
            try
            {
                this.UIMap.CheckCalculations_C2_S3();
                TestContext.WriteLine("Chapter_2 step_3: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_3: " + e.Message);
            }

            //Step 4
            this.UIMap.ClickTransfereFTT("08.01.2024");
            try
            {
                this.UIMap.CheckTransferPeriodAndEmployees_C2_S4();
                TestContext.WriteLine("Chapter_2 step_4: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_4: " + e.Message);
            }

            try
            {
                errorList.AddRange(UIMap.CheckTransferVacantNotInList_C2_S4());
                TestContext.WriteLine("Chapter_2 step_4_2: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_4_2: " + e.Message);
            }

            //Step 5
            this.UIMap.ShowMasterDetailsStellan();
            try
            {
                this.UIMap.CheckCalculations_C2_S5();
                TestContext.WriteLine("Chapter_2 step_5: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_5: " + e.Message);
            }

            //Step 6
            this.UIMap.ShowMasterDetailsStellan();
            UIMap.SetTransfereFromDateFTT();
            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickProcessingLink();
            try
            {
                this.UIMap.CheckTransferedFTTValues_C1_S6();
                TestContext.WriteLine("Chapter_2 step_6: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_6: " + e.Message);
            }

            //Step 7
            this.UIMap.ClickCloseExportDetailsWindow();
            this.UIMap.ClickCloseExportLogWindow();

            //Step 8
            this.UIMap.ClickEditRosterplanFromHomeTab();
            Playback.Wait(500);
            UIMap.SelectTuesdayWeek1Stellan();
            this.UIMap.CreateAbsence("45-2");

            //Step 9
            this.UIMap.ShowFTTAsTotalCalendarPlan();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckStellanCalculations_C2_S9();
                TestContext.WriteLine("Chapter_2 step_9: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_9: " + e.Message);
            }

            //Step 10
            this.UIMap.ShowFTTAsAveragePrMnd();
            try
            {
                this.UIMap.CheckStellanCalculations_C2_S10();
                TestContext.WriteLine("Chapter_2 step_10: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_10: " + e.Message);
            }

            //Step 11
            this.UIMap.DeleteAbsenceStellanTuesday();
            this.UIMap.SelectWeek1Stellan();
            this.UIMap.CreateAbsence("45-2");

            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckStellanCalculations_C2_S11();
                TestContext.WriteLine("Chapter_2 step_11: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_11: " + e.Message);
            }

            //Step 12
            this.UIMap.ClickTransfereFTT("08.01.2024");
            this.UIMap.ShowMasterDetailsStellan();
            try
            {
                this.UIMap.CheckTransferedFTTValuesStellanCurrent_C2_S12();
                TestContext.WriteLine("Chapter_2 step_12_1: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_12_1: " + e.Message);
            }

            try
            {
                this.UIMap.ShowStellanPreviousTransfers();
                this.UIMap.CheckTransferedFTTValuesStellanPrevious_C2_S12();
                TestContext.WriteLine("Chapter_2 step_12_2: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_12_2: " + e.Message);
            }

            //Step 13
            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickCloseExportLogWindow();

            //Step 14
            this.UIMap.ClickCellToInsertNshiftMonWeek3Stormare();
            this.UIMap.ClickOkEditFromHometab();
            this.UIMap.EditLineSettingsLine2Stormare();
            this.UIMap.ShowFTTAsAveragePrMnd();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckStormareCalculations_C2_S14();
                TestContext.WriteLine("Chapter_2 step_14: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_14: " + e.Message);
            }

            //Step 15
            this.UIMap.ClickTransfereFTT("08.01.2024");
            this.UIMap.ShowMasterDetailsStormare();
            try
            {
                this.UIMap.CheckTransferedFTTValuesStormare_C2_S15();
                TestContext.WriteLine("Chapter_2 step_15: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_15: " + e.Message);
            }

            //Step 16
            this.UIMap.SelectShowAllEmpsInExportFTTWindow();
            try
            {
                this.UIMap.CheckStellanEdwallIsExported_C2_S16();
                TestContext.WriteLine("Chapter_2 step_16: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_16: " + e.Message);
            }

            //Step 17
            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickProcessingLink();
            try
            {
                this.UIMap.CheckTransferedFTTValuesStormare_C2_S17();
                TestContext.WriteLine("Chapter_2 step_17: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_17: " + e.Message);
            }

            //Step 18
            this.UIMap.ClickCloseExportDetailsWindow();
            this.UIMap.ClickCloseExportLogWindow();
            this.UIMap.ClickCalculateFFTButton();
            this.UIMap.ClickStopFFTButton();
            this.UIMap.SelectAllFTTTEmpsAndClickStop();

            //Step 19
            this.UIMap.ClickCloseExportLogWindow();

            //Step 20
            this.UIMap.AddNewLineStormare();
            this.UIMap.EditNewLineSettingsStormare();

            //Step 21
            this.UIMap.ClickEditRosterplanFromHomeTab();
            Playback.Wait(500);

            //Todo: fixe dette når basen er iorden A1 er ikke tilgjengelig
            this.UIMap.ClickCellToInsertA1shiftTueWeek2Line2Stormare();
            this.UIMap.ClickOkEditFromHometab();
            this.UIMap.ClickCalculateFFTButton();

            errorList.AddRange(UIMap.CheckStormareNoCalculations_C2_S21());

            //Step 22
            this.UIMap.ClickEditRosterplanFromHomeTab();
            Playback.Wait(500);
            this.UIMap.DeleteA1shiftTueWeek2Line2Stormare();
            this.UIMap.ClickCellToInsertA1shiftWedWeek3AndSatWeek4Line2Stormare();
            this.UIMap.ClickOkEditFromHometab();

            //Step 23
            this.UIMap.EditLine2SettingsStormare_C2_S23();
            this.UIMap.ShowFTTAsTotalCalendarPlan();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckStormareCalculations_C2_S23();
                TestContext.WriteLine("Chapter_2 step_23: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_23: " + e.Message);
            }

            //Step 24
            this.UIMap.ClickTransfereFTT("08.01.2024");
            try
            {
                this.UIMap.CheckFTTEmployeesToTransfere_C2_S24();
                TestContext.WriteLine("Chapter_2 step_24: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_24: " + e.Message);
            }

            //Step 25
            this.UIMap.ChangeTransferFromDate();
            try
            {
                this.UIMap.CheckFTTransfereStormareHasOneOkLine();
                this.UIMap.CheckFTTransfereButtonIsDisabled();

                TestContext.WriteLine("Chapter_2 step_25(Transfere disabled): OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_25(Transfere disabled): " + e.Message);
            }

            this.UIMap.UncheckStormareErrorLine();

            try
            {
                this.UIMap.CheckFTTransfereButtonIsEnabled();
                TestContext.WriteLine("Chapter_2 step_25(Transfere enabled): OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_25(Transfere enabled): " + e.Message);
            }

            //Step 26
            this.UIMap.OpenStormareMasterDetailsPreviousTransfers();

            try
            {
                this.UIMap.CheckStormareHasTwoPreviousTransferes_C2_S26();
                TestContext.WriteLine("Chapter_2 step_26: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_2 step_26: " + e.Message);
            }

            this.UIMap.ClickCancelExportLogDialog();
            this.UIMap.CloseCurrentRosterplanFromHomeTab();

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 24, kap. 2"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter2 finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_024_Chapter3()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeStart = DateTime.Now;

            //Step 1
            UIMap.OpenPlan("FFT Baseplan");
            this.UIMap.CopyFTTBaseplan();

            //Step 2
            this.UIMap.CloseCurrentRosterplanFromPlanTab();
            UIMap.OpenPlan("Kopi av FFT Baseplan");

            //Step 3
            this.UIMap.SelectFTTTab();
            try
            {
                this.UIMap.CheckButtonEnabled_C3_S3();
                TestContext.WriteLine("Chapter_3 step_3(Button ebnabled): OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_3(Button ebnabled): " + e.Message);
            }

            //Step 4
            this.UIMap.ShowFTTAsAveragePrMnd();
            this.UIMap.ClickCalculateFFTButton();

            try
            {
                this.UIMap.CheckCalculations_C3_S3();
                TestContext.WriteLine("Chapter_3 step_4: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_4: " + e.Message);
            }

            //Step 5
            this.UIMap.ClickTransfereFTT("18.03.2024");
            this.UIMap.OpenMaxMasterDetails();
            try
            {
                this.UIMap.CheckMaxMasterDetails_C3_S5();
                TestContext.WriteLine("Chapter_3 step_5: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_5: " + e.Message);
            }

            //Step 6
            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickProcessingLink();
            try
            {
                this.UIMap.CheckTransferedFTTValues_C3_S6();
                TestContext.WriteLine("Chapter_3 step_6: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_6: " + e.Message);
            }

            //Step 7
            this.UIMap.ClickCloseExportDetailsWindow();
            this.UIMap.ClickCloseExportLogWindow();
            this.UIMap.ClickStopFFTButton();
            this.UIMap.SelectAllFTTTEmpsAndClickStop_C3_S7();
            this.UIMap.ClickCloseExportLogWindow();
            this.UIMap.CloseCurrentRosterplanFromHomeTab();

            //Step 8
            this.UIMap.CreateWhishplanEaster2016();

            //Step 9
            this.UIMap.ConnectWhishplanEaster2016ToCopyOfFFTBaseplan();

            //Step 10
            this.UIMap.OpenWhishplanEaster2016(true);

            //Step 11
            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C3_S11();
                TestContext.WriteLine("Chapter_3 step_11: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_11: " + e.Message);
            }

            try
            {
                this.UIMap.CheckTransferFTTButtonDisabled();
                TestContext.WriteLine("Chapter_3 step_11(Button disabled): OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_11(Button disabled): " + e.Message);
            }

            //Step 12
            this.UIMap.CloseCurrentRosterplanFromHomeTab();
            this.UIMap.SetWhishplanEaster2016ToPhase1();
            this.UIMap.OpenWhishplanEaster2016(false);

            //Step 13
            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckTransferFTTButtonDisabled();
                TestContext.WriteLine("Chapter_3 step_13(Button disabled): OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_13(Button disabled): " + e.Message);
            }
            try
            {
                //Kalkulasjoner skal være lik step 11
                this.UIMap.CheckCalculations_C3_S11();
                TestContext.WriteLine("Chapter_3 step_13: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_13: " + e.Message);
            }

            //Step 14
            this.UIMap.CloseCurrentRosterplanFromHomeTab();
            this.UIMap.SetWhishplanEaster2016ToPhase2();
            this.UIMap.OpenWhishplanEaster2016(false);

            //Step 15
            Playback.Wait(3000);
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.UnlockAndEditShiftOnLineMax();
            this.UIMap.ReplaceAshiftWeek1AndDshiftWeek3();
            this.UIMap.ClickOkEditFromHometab();

            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckTransferFTTButtonDisabled();
                TestContext.WriteLine("Chapter_3 step_15(Button disabled): OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_15(Button disabled): " + e.Message);
            }
            try
            {
                this.UIMap.CheckCalculations_C3_S15();
                TestContext.WriteLine("Chapter_3 step_15: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_15: " + e.Message);
            }

            //Step 16
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.UnlockAndEditShiftOnLineMax();
            this.UIMap.ReplaceNshiftsWeek2AndF2shiftWeek3();
            this.UIMap.ClickOkEditFromHometab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckTransferFTTButtonDisabled();
                TestContext.WriteLine("Chapter_3 step_16(Button disabled): OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_16(Button disabled): " + e.Message);
            }
            try
            {
                this.UIMap.CheckCalculations_C3_S16();
                TestContext.WriteLine("Chapter_3 step_16: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_16: " + e.Message);
            }

            //Step 17
            this.UIMap.CloseCurrentRosterplanFromHomeTab();
            this.UIMap.SetWhishplanEaster2016ToPhase3();
            this.UIMap.OpenWhishplanEaster2016(false);

            //Step 18
            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckTransferFTTButtonEnabled();
                TestContext.WriteLine("Chapter_3 step_18(Button enabled): OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_18(Button enabled): " + e.Message);
            }

            //Step 19
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.InsertShiftsWeek2AndshiftWeek3_C3_S19();
            this.UIMap.ClickOkEditFromHometab();

            //Step 20
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                //Verdier skal være lik step 16
                this.UIMap.CheckCalculations_C3_S16();
                TestContext.WriteLine("Chapter_3 step_20: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_20: " + e.Message);
            }
            try
            {
                this.UIMap.CheckTransferFTTButtonEnabled();
                TestContext.WriteLine("Chapter_3 step_20(Button enabled): OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_20(Button enabled): " + e.Message);
            }

            //Step 21
            this.UIMap.ClickTransfereFTT("18.03.2024");
            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickProcessingLink();

            try
            {
                this.UIMap.CheckTransferedFTTValues_C3_S21();
                TestContext.WriteLine("Chapter_3 step_21: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_3 step_21: " + e.Message);
            }

            this.UIMap.ClickCloseExportDetailsWindow();
            this.UIMap.ClickCloseExportLogWindow();

            Playback.Wait(1000);
            this.UIMap.CloseCurrentRosterplanFromHomeTab();
            this.UIMap.SelectSubTabRosterplans();

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 24, kap. 3"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter3 finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_024_Chapter4()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeStart = DateTime.Now;

            //Step 1
            UIMap.OpenPlan("FTT Ukeregler KP");
            this.UIMap.CopyCalendarplanCommon("");

            //Step 2
            this.UIMap.CloseCurrentRosterplanFromPlanTab();
            UIMap.OpenPlan("Kopi av FTT Ukeregler KP.");

            //Step 3
            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S3();
                TestContext.WriteLine("Chapter_4 step_3: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_3: " + e.Message);
            }

            //Step 4
            this.UIMap.AddNewLineFromPositionGustavsson();

            //Step 5
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.InsertDshiftWeek1And2Gustavsson();
            this.UIMap.ClickOkEditFromHometab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S5();
                TestContext.WriteLine("Chapter_4 step_5: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_5: " + e.Message);
            }

            //Step 6
            this.UIMap.AddNewLineFromDepartmentGustavsson();

            //Step 7
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.InsertDHshiftWeek3Line3Gustavsson();
            this.UIMap.ClickOkEditFromHometab();

            //Step 8
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                //Verdier skal være likt step 5
                this.UIMap.CheckCalculations_C4_S5();
                TestContext.WriteLine("Chapter_4 step_8: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_8: " + e.Message);
            }

            //Step 9
            this.UIMap.EditLineSettingsLine3Gustavsson();

            //Step 10
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S10();
                TestContext.WriteLine("Chapter_4 step_10: OK");

            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_10: " + e.Message);
            }

            //Step 11
            this.UIMap.AddNewLineFromPositionGustavsson_2();

            //Step 12
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.InsertDshiftWeek1Line4Gustavsson();
            this.UIMap.ClickOkEditFromHometab();

            //Step 13
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S13();
                TestContext.WriteLine("Chapter_4 step_13: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_13: " + e.Message);
            }

            //Step 14
            this.UIMap.AddNewLineFromPositionPersbrandt();

            //Step 15
            this.UIMap.ClickCalculateFFTButton();

            errorList.AddRange(UIMap.CheckCalculations_C4_S15());

            //Step 16
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.InsertAandDshiftsWeek4Line2Persbrandt();
            this.UIMap.ClickOkEditFromHometab();

            //Step 17
            this.UIMap.EditLineSettingsLine1Persbrandt();

            //Step 18
            this.UIMap.EditLineSettingsLine2Persbrandt();

            //Step 19
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S19();
                TestContext.WriteLine("Chapter_4 step_19: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_19: " + e.Message);
            }

            //Step 20
            this.UIMap.EditLineSettingsRheborg();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S20();
                TestContext.WriteLine("Chapter_4 step_20: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_20: " + e.Message);
            }

            //Step 21
            this.UIMap.EditLineSettingsRheborg_2();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S21();
                TestContext.WriteLine("Chapter_4 step_21: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_21: " + e.Message);
            }

            //Step 22
            this.UIMap.AddNewLineFromPositionRheborg();

            //Step 23
            this.UIMap.EditLineSettingsLine2Rheborg();

            //Step 24
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.InsertD4shiftsLine2Rheborg();
            this.UIMap.ClickOkEditFromHometab();

            this.UIMap.ClickCalculateFFTButton();
            try
            {
                //Verdier skal være lik step 21
                this.UIMap.CheckCalculations_C4_S21();
                TestContext.WriteLine("Chapter_4 step_24: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_24: " + e.Message);
            }

            //Step 25
            this.UIMap.EditF3PlanSettings();
            this.UIMap.ClickEditRosterplanFromPlanTab();
            this.UIMap.InsertF3shiftLine2Rheborg();
            this.UIMap.ClickOkEditFromPlantab();

            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S25();
                TestContext.WriteLine("Chapter_4 step_25: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_25: " + e.Message);
            }

            //Step 26
            this.UIMap.SetLine2RheborgInactive();
            this.UIMap.ClickCalculateFFTButton();

            errorList.AddRange(UIMap.CheckCalculations_C4_S26());

            //Step 27
            this.UIMap.AddHaberToPlan();

            try
            {
                Playback.Wait(3000);
                this.UIMap.CloseCurrentRosterplanFromPlanTab();
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Plan closed after adding Haber(Chapter_4 step_27)");
            }

            UIMap.OpenPlan("Kopi av FTT Ukeregler KP.");

            //Step 28
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.InsertD1shiftsLine1And2Haber();
            this.UIMap.ClickOkEditFromHometab();

            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S28();
                TestContext.WriteLine("Chapter_4 step_28: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_28: " + e.Message);
            }

            //Step 29
            this.UIMap.ClickEditRosterplanFromHomeTab();
            Playback.Wait(500);
            this.UIMap.SelectWeek1Gustavsson();
            this.UIMap.CreateAbsence("45-1");
            this.UIMap.ClickOkEditFromHometab();

            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S29();
                TestContext.WriteLine("Chapter_4 step_29: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_29: " + e.Message);
            }

            //Step 30
            this.UIMap.ClickEditRosterplanFromHomeTab();
            Playback.Wait(500);
            this.UIMap.DeleteAbsenceWeek1Gustavsson();
            this.UIMap.SelectWeek1Gustavsson();
            this.UIMap.CreateAbsence("45-2");
            this.UIMap.ClickOkEditFromHometab();

            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C4_S30();
                TestContext.WriteLine("Chapter_4 step_30: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_30: " + e.Message);
            }

            //Step 31
            this.UIMap.ClickTransfereFTT("08.01.2024");
            this.UIMap.ExpandMasterDetailsForAllEmployees();
            try
            {
                this.UIMap.CheckFTTEmployeesToTransfereValues();
                TestContext.WriteLine("Chapter_4 step_31: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_31: " + e.Message);
            }

            //Step 32
            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickCloseExportLogWindow();

            Playback.Wait(1000);
            this.UIMap.CloseCurrentRosterplanFromHomeTab();

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 24, kap. 4"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter4 finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_024_Chapter5()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeStart = DateTime.Now;

            //Step 1
            UIMap.OpenPlan("FTT Ukeregler AP");
            this.UIMap.CopyFTTRosterplan(new DateTime(2024, 01, 01));

            //Step 2
            this.UIMap.CloseCurrentRosterplanFromPlanTab();
            UIMap.OpenPlan("Kopi av FTT Ukeregler AP.");

            this.UIMap.OpenPlanSettingsAndEditToDate();
            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C5_S2();
                TestContext.WriteLine("Chapter_5 step_2: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_5 step_2: " + e.Message);
            }

            this.UIMap.RightClickLennartLine1();
            this.UIMap.EditLineSettings("", "28.01.2024");
            this.UIMap.ClickCalculateFFTButton(true);
            try
            {
                //Verdier skal være lik step 2
                this.UIMap.CheckCalculations_C5_S2();
                TestContext.WriteLine("Chapter_5 step_3: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_5 step_3: " + e.Message);
            }

            //Step 4
            this.UIMap.RightClickLennartLine1();
            this.UIMap.EditLineSettings("", "14.01.2024");
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C5_S4();
                TestContext.WriteLine("Chapter_5 step_4: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_5 step_4: " + e.Message);
            }

            //Step 5 
            UIMap.CloseCurrentRosterplanFromPlanTab();
            UIMap.OpenPlan("Kopi av FTT Ukeregler AP.");
            this.UIMap.RightClickRolfLine1();
            this.UIMap.EditLineSettings("", "17.01.2024");
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C5_S5();
                TestContext.WriteLine("Chapter_5 step_5: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_5 step_5: " + e.Message);
            }

            //Step 6 
            this.UIMap.RightClickRolfLine2();
            this.UIMap.EditLineSettings("18.01.2024", "");
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C5_S6();
                TestContext.WriteLine("Chapter_5 step_6: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_5 step_6: " + e.Message);
            }

            //Step 7
            this.UIMap.ClickTransfereFTT("01.01.2024");
            this.UIMap.ExpandMasterDetailsForAllEmployees_C5_S7();
            try
            {
                this.UIMap.CheckFTTEmployeesToTransfereValues_C5_S7();
                TestContext.WriteLine("Chapter_5 step_7: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_5 step_7: " + e.Message);
            }

            //Step 8
            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickProcessingLink();
            try
            {
                this.UIMap.CheckTransferedFTTValues_C5_S7();
                TestContext.WriteLine("Chapter_5 step_8: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_5 step_8: " + e.Message);
            }

            this.UIMap.ClickCloseExportDetailsWindow();
            this.UIMap.ClickCloseExportLogWindow();
            this.UIMap.CloseCurrentRosterplanFromPlanTab();

            this.UIMap.CloseGat();

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 24, kap. 5"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter5 finished OK");
                return;
            }

            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void SystemTest_024_Chapter6()
        {
            //this.UIMap.SelectMainShiftbookTabPOuser();
            //this.UIMap.SelectMainRosterplanTabPOuser();
            //this.UIMap.SelectMainEmpTabPOuser();
            //this.UIMap.SelectMainDepTabPOuser();
            //this.UIMap.SelectMainAdminTabPOuser();

            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            var timeStart = DateTime.Now;

            //Step 1 (Login PO/user)
            UIMap.StartGat(false, "PO");
            UIMap.OpenPlanPOuser("Vaktklasser KP");

            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            if (UIMap.CheckButtonShiftClassesExists())
            {
                errorList.Add("Error in Chapter_6 step_1: Shiftclasses button is visible");
            }
            try
            {
                TestContext.WriteLine("Chapter_6 step_1: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_1: " + e.Message);
            }

            //Step 2
            this.UIMap.CloseCurrentRosterplanFromHomeTab();
            this.UIMap.SelectMainDepTabPOuser();
            //Hack: DelphiTabkomponent
            this.UIMap.SelectSubTabShiftCodes();
            this.UIMap.SelectShiftCodeD2();
            try
            {
                this.UIMap.CheckEditShiftClassesDisabled_C6_S2();
                TestContext.WriteLine("Chapter_6 step_2: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Vaktklasser kan endres(C6_S2): " + e.Message);
            }

            this.UIMap.CancelEditShiftCodeD2();

            //Step 3
            this.UIMap.CloseGat();
            UIMap.StartGat(false);
            UIMap.OpenPlan("Vaktklasser KP");

            //Step 4
            this.UIMap.CopyCalendarplanCommon("Vaktklasser KP Kopi");

            //Step 5
            this.UIMap.CloseCurrentRosterplanFromPlanTab();
            UIMap.OpenPlan("Vaktklasser KP Kopi");
            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();

            if (!UIMap.CheckButtonShiftClassesExists())
            {
                errorList.Add(">Error in Chapter_6 step_5: Shiftclasses button not visible");
            }

            try
            {
                this.UIMap.CheckCalculations_C6_S5();
                TestContext.WriteLine("Chapter_6 step_5: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_5: " + e.Message);
            }

            //Step 6
            this.UIMap.ClickButtonShiftClasses();
            try
            {
                this.UIMap.CheckShiftClassesData_C6_S6();
                TestContext.WriteLine("Chapter_6 step_6: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_6: " + e.Message);
            }

            //Step 7            
            this.UIMap.CloseShiftClassesWindow();
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.InsertD2SunOscarsson();
            this.UIMap.InsertHJ2ThuVollter();
            this.UIMap.ClickOkEditFromHometab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C6_S7();
                TestContext.WriteLine("Chapter_6 step_7: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_7: " + e.Message);
            }

            //Step 8
            this.UIMap.ClickButtonShiftClasses();
            try
            {
                this.UIMap.CheckShiftClassesData_C6_S8();
                TestContext.WriteLine("Chapter_6 step_8: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_8: " + e.Message);
            }

            //Step 9
            this.UIMap.CloseShiftClassesWindow();
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.EditVollterLine_C6_9();
            this.UIMap.ClickOkEditFromHometab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C6_S9();
                TestContext.WriteLine("Chapter_6 step_9: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_9: " + e.Message);
            }

            //Step 10
            this.UIMap.ClickButtonShiftClasses();
            try
            {
                this.UIMap.CheckShiftClassesData_C6_S10();
                TestContext.WriteLine("Chapter_6 step_10: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_10: " + e.Message);
            }

            //Step 11
            this.UIMap.CloseShiftClassesWindow();
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.EditOscarssonLine_C6_11();
            this.UIMap.ClickOkEditFromHometab();
            this.UIMap.CloseCurrentRosterplanFromHomeTab();

            //Step 12
            UIMap.SelectFromAdministration("Vaktklasse +satser");
            this.UIMap.AddNewShiftClassRate(new DateTime(2024, 1, 15), "100", "200", "300", "400", "500", "600", "700", "800", "900", "1000");
            UIMap.OpenPlan("Vaktklasser KP Kopi");

            //Step 13
            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C6_S13();
                TestContext.WriteLine("Chapter_6 step_13: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_13: " + e.Message);
            }

            //Step 14
            this.UIMap.ClickButtonShiftClasses();
            try
            {
                this.UIMap.CheckShiftClassesData_C6_S14();
                TestContext.WriteLine("Chapter_6 step_14: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_14: " + e.Message);
            }

            //Step 15
            this.UIMap.CloseShiftClassesWindow();
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.EditOscarssonLine_C6_15();
            this.UIMap.ClickOkEditFromHometab();

            //Step 16
            this.UIMap.EditLineSettingsOscarsson_C6_S16();

            //Step 17
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C6_S17();
                TestContext.WriteLine("Chapter_6 step_17: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_17: " + e.Message);
            }

            //Step 18
            this.UIMap.ClickButtonShiftClasses();
            try
            {
                this.UIMap.CheckShiftClassesData_C6_S18();
                TestContext.WriteLine("Chapter_6 step_18: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_18: " + e.Message);
            }

            //Step 19
            this.UIMap.CloseShiftClassesWindow();
            this.UIMap.AddNewLineFromPositionOscarsson();

            //Step 20
            this.UIMap.ClickEditRosterplanFromHomeTab();
            this.UIMap.EditOscarssonLine2_C6_20();
            this.UIMap.ClickOkEditFromHometab();

            //Step 21
            this.UIMap.EditLine2SettingsOscarsson_C6_S21();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C6_S21();
                TestContext.WriteLine("Chapter_6 step_21: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_21: " + e.Message);
            }

            //Step 22
            this.UIMap.ClickButtonShiftClasses();
            try
            {
                this.UIMap.CheckShiftClassesData_C6_S22();
                TestContext.WriteLine("Chapter_6 step_22: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_22: " + e.Message);
            }

            //Step 23
            this.UIMap.CloseShiftClassesWindow();
            this.UIMap.ClickTransfereFTT("08.01.2024");
            try
            {
                this.UIMap.CheckFTTEmployeesToTransfere_C6_S23();
                TestContext.WriteLine("Chapter_6 step_23: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_23: " + e.Message);
            }

            //Step 24
            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickProcessingLink();
            try
            {
                this.UIMap.CheckTransferedFTTValues_C6_S24();
                TestContext.WriteLine("Chapter_6 step_24: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_24: " + e.Message);
            }

            this.UIMap.ClickCloseExportDetailsWindow();
            this.UIMap.ClickCloseExportLogWindow();

            //Step 25
            this.UIMap.CloseCurrentRosterplanFromHomeTab();

            UIMap.OpenPlan("Vaktklasser AP");
            this.UIMap.CopyAPRosterplan();

            this.UIMap.CloseCurrentRosterplanFromPlanTab();
            UIMap.OpenPlan("Vaktklasser AP kopi");

            //Step 26
            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C6_S26();
                TestContext.WriteLine("Chapter_6 step_26: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_26: " + e.Message);
            }

            //Step 27
            this.UIMap.ClickButtonShiftClasses();
            try
            {
                this.UIMap.CheckShiftClassesData_C6_S27();
                TestContext.WriteLine("Chapter_6 step_27: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_27: " + e.Message);
            }

            //Step 28
            this.UIMap.CloseShiftClassesWindow();
            this.UIMap.EditLineSettingsNyquist_C6_S28();

            //Step 29
            this.UIMap.ClickCalculateFFTButton();
            this.UIMap.ClickButtonShiftClasses();
            try
            {
                this.UIMap.CheckShiftClassesData_C6_S29();
                TestContext.WriteLine("Chapter_6 step_29: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_29: " + e.Message);
            }

            //Step 30
            this.UIMap.CloseShiftClassesWindow();
            this.UIMap.EditPlanSettingValidToDate();
            this.UIMap.EditLineSettingsKulle_C6_S30();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C6_S30();
                TestContext.WriteLine("Chapter_6 step_30: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_30: " + e.Message);
            }

            //Step 31
            this.UIMap.CloseCurrentRosterplanFromPlanTab();
            UIMap.SelectFromAdministration("Vaktklasse +satser");
            this.UIMap.AddNewShiftClassRate(_CurrentDate, "10000", "11000", "12000", "13000", "14000", "15000", "16000", "17000", "18000", "19000");

            //Step 32 - 33          
            UIMap.OpenPlan("Vaktklasser AP kopi");
            this.UIMap.SelectFTTTab();
            this.UIMap.ClickCalculateFFTButton();
            try
            {
                this.UIMap.CheckCalculations_C6_S33();
                TestContext.WriteLine("Chapter_6 step_33: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_33: " + e.Message);
            }

            //Step 34
            this.UIMap.ClickButtonShiftClasses();
            try
            {
                //Todo: legge inn sjekk for datoene på toppen
                this.UIMap.CheckShiftClassesData_C6_S34();
                UIMap.CheckCurrentRateDate_C6_S34(_CurrentDate);
                TestContext.WriteLine("Chapter_6 step_34: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_34: " + e.Message);
            }

            //Step 35
            this.UIMap.CloseShiftClassesWindow();
            this.UIMap.ClickTransfereFTT("08.01.2024");
            this.UIMap.ShowMasterDetailsNyquist();

            try
            {
                this.UIMap.CheckFTTEmployeesToTransfere_C6_S35();
                TestContext.WriteLine("Chapter_6 step_35: OK");
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_6 step_35: " + e.Message);
            }

            this.UIMap.ClickTransfereFFTDialogButton();
            this.UIMap.ClickCloseExportLogWindow();

            this.UIMap.CloseCurrentRosterplanFromHomeTab();
            this.UIMap.CloseGat();

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 24, kap. 6"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter6 finished OK");
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
