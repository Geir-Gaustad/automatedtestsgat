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


namespace _023_Test_Omregnet_Tid
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest_023
    {

        [TestMethod, Timeout(6000000)]
        public void SystemTest_023()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            var timeBefore = DateTime.Now;

            UIMap.StartGat(true);
            UIMap.OpenPlan();

            UIMap.UnlockPlanAndRestartGat_IfLockedstep_1();

            try
            {
                this.UIMap.CheckBeethovenLine1_step_1();
                TestContext.WriteLine("Korrekte verdier i step 1(Ingen verdier skal være beregnet)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step_1");
            }

            this.UIMap.EditBeethovenLine1_step_2();

            try
            {
                this.UIMap.CheckBeethovenLine1_step_2();
                TestContext.WriteLine("Korrekte verdier i step 2(Omregnet tid)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step_2");
            }

            try
            {
                this.UIMap.CheckBeethovenLine1_step_3_USum();
                TestContext.WriteLine("Korrekte verdier i step 3(Ukesum)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step_3(Ukesum)");
            }

            try
            {
                this.UIMap.CheckBeethovenLine1_step_3_OT();
                TestContext.WriteLine("Korrekte verdier i step 3(Omregnet tid)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step_3(Omregnet tid)");
            }

            try
            {
                //Check T / O.T values and % d
                this.UIMap.CheckBeethovenLine1_step_4_Calculations();
                TestContext.WriteLine("Korrekte verdier i step 4(Beregninger)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 4(Beregninger)");
            }

            this.UIMap.EditBachLine2_step_5();

            try
            {
                this.UIMap.CheckBachLine2_step_5_USum_OT();
                TestContext.WriteLine("Korrekte verdier i step 5(Ukesum/Omregnet tid)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 5(Ukesum/Omregnet tid)");
            }

            try
            {
                //Check T / O.T values and % d
                this.UIMap.CheckBachLine2_step_6_Calculations();
                TestContext.WriteLine("Korrekte verdier i step 6(Beregninger)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 6(Beregninger)");
            }

            //Rediger step 7
            this.UIMap.ClickEditPlan();
            UIMap.InsertHJ1BachWednesday();
            UIMap.AddUtjevningsVakt();
            this.UIMap.ConstructUtjevningsvakt();
            this.UIMap.ClickOkEdit();

            try
            {
                this.UIMap.CheckBachLine2Week1_step_7_USum_OT();
                TestContext.WriteLine("Korrekte verdier i step 7(Ukesum/Omregnet tid)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 7(Ukesum/Omregnet tid)");
            }
            try
            {
                //Check T / O.T values and % d
                UIMap.CheckBachLine2Week1_step_7_Calculations();
                TestContext.WriteLine("Korrekte verdier i step 7(Beregninger)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 7(Beregninger)");
            }

            //step 8
            this.UIMap.OpenEditLinesettingsChopin();
            try
            {
                this.UIMap.CheckOmregnetTidDisabled();
                TestContext.WriteLine("Omregnet tid er låst(OK) i step 8");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 8");
            }

            this.UIMap.CancelEditLinesettingsChopin();

            //step 9
            try
            {
                this.UIMap.CheckChopinLine3Week1_step_9_USum_OT();
                TestContext.WriteLine("Korrekte verdier i step 9(Ukesum/Omregnet tid)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 9(Ukesum/Omregnet tid)");
            }

            //step 10
            try
            {
                this.UIMap.CheckChopinLine3Week1_step_10_Calculations();
                TestContext.WriteLine("Korrekte verdier i step 10(Beregninger)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 10(Beregninger)");
            }

            //step 11
            errorList.AddRange(UIMap.RegisterAbsenceChopinWeek(2, "7,75"));

            //step 12 sjekk beregninger og ukesum
            try
            {
                this.UIMap.CheckChopinLine3_step_12_USum_OT();
                TestContext.WriteLine("Korrekte verdier i step 12(Ukesum/Omregnet tid)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 12(Ukesum/Omregnet tid)");
            }

            try
            {
                this.UIMap.CheckChopinLine3_step_12_Calculations();
                TestContext.WriteLine("Korrekte verdier i step 12(Beregninger)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 12(Beregninger)");
            }

            //step 13
            errorList.AddRange(UIMap.RegisterAbsenceChopinWeek(4, "11,82"));

            //step 14 iverksett og sjekk bankverdier
            this.UIMap.ClickOkEditStep14();
            this.UIMap.EffectuateChopinLine3_step_14();
            this.UIMap.CloseWishPlan();

            //Hack: DelphiTabkomponent
            this.UIMap.SelectEmployeeTabBanks();

            this.UIMap.OpenChopinSaldotidDetails();
            try
            {
                this.UIMap.CheckChopinDetailsTab_step_14();
                TestContext.WriteLine("Korrekte verdier i step 14(Bankverdier Chopin)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 14(Bankverdier Chopin)");
            }

            //step 15
            this.UIMap.SelectRosterplanTab();
            this.UIMap.SetWhishplanToPhase1();
            this.UIMap.OpenWhishPlan();

            UIMap.WaitForPlanReady();
            this.UIMap.EditMozartLineToUseOmregnetTid();
            this.UIMap.SetWhishplanInEditMode();

            this.UIMap.EditMozartLineUnlockEditShifts();
            this.UIMap.InsertAshiftMozartTuAndThuWeek1();

            try
            {
                this.UIMap.CheckMozartOmregnetTidSumWeek1_step_15();
                TestContext.WriteLine("Korrekte verdier i step 15(Omregnet tid)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 15(Omregnet tid)");
            }

            //step 16
            this.UIMap.InsertDshiftMozartSunWeek3();

            try
            {
                this.UIMap.CheckMozartOmregnetTidSumWeek3_step_16();
                TestContext.WriteLine("Korrekte verdier i step 16(Omregnet tid)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 16(Omregnet tid)");
            }

            try
            {
                UIMap.SetWishplanFilter();
                UIMap.CheckMozartLine1_step_16_Calculations();
                TestContext.WriteLine("Korrekte verdier i step 16(Beregninger)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 16(Beregninger)");
            }

            //step 17
            this.UIMap.InsertNshiftMozartWedWeek3();

            try
            {
                this.UIMap.CheckMozartLine1_step_17_USum_OT();
                TestContext.WriteLine("Korrekte verdier i step 17(Ukesum/Omregnet tid)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 17(Ukesum/Omregnet tid)");
            }

            //step 18
            this.UIMap.ClickOkEditWishPlan();
            this.UIMap.CloseWishPlan();
            this.UIMap.SetWhishplanToPhase2();
            this.UIMap.OpenWhishPlan();

            UIMap.WaitForPlanReady();
            UIMap.SetFilterToShowGrunnlinje();

            //step 19
            this.UIMap.SetWhishplanInEditMode();
            this.UIMap.EditMozartLinePhase2UnlockEditShifts();
            this.UIMap.InsertN_F2_AshiftMozartPhase2();

            try
            {
                this.UIMap.CheckMozartOmregnetTidSumPhase2_step_19();
                TestContext.WriteLine("Korrekte verdier i step 19(Ukesum/Omregnet tid fase2)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 19(Ukesum/Omregnet tid fase2)");
            }

            //step 20
            this.UIMap.ClickOkEditWishPlan();
            this.UIMap.CloseWishPlan();
            UIMap.CloseGat();
            UIMap.StartGat(false);

            this.UIMap.SelectRosterplanTab(3);
            UIMap.SelectWishplan(true);
            this.UIMap.SetWhishplanToPhase3();
            Playback.Wait(1000);
            this.UIMap.OpenWhishPlan();

            UIMap.WaitForPlanReady();
            this.UIMap.EditSchubertLineToUseOmregnetTid();
            this.UIMap.SetWhishplanInEditMode();

            //step 21 
            UIMap.InsertF3shiftMozartPhase3();

            try
            {
                this.UIMap.CheckMozartOmregnetTidSumPhase3_step_21();
                TestContext.WriteLine("Korrekte verdier i step 21(Ukesum/Omregnet tid fase3)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 21(Ukesum/Omregnet tid fase3)");
            }

            UIMap.SelectDetailsTabAndMarkF3shift_step_21();

            try
            {
                this.UIMap.CheckMozartDetailsTabPhase3_step_21();
                TestContext.WriteLine("Korrekte verdier i step 21(Underfane detaljer fase3)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 21(Underfane detaljer fase3)");
            }

            //step 22
            this.UIMap.InsertF4shiftMozartPhase3_step_22();
            this.UIMap.MarkF4shift_step_22();

            try
            {
                this.UIMap.CheckMozartDetailsTabPhase3_step_22();
                TestContext.WriteLine("Korrekte verdier i step 22(Underfane detaljer fase3)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 22(Underfane detaljer fase3)");
            }

            //step 23
            try
            {
                this.UIMap.CheckMozartLine1_step_23_Calculations();
                TestContext.WriteLine("Korrekte verdier i step 23(Beregninger)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 23(Beregninger)");
            }

            //step 24
            try
            {
                this.UIMap.CheckSchubertLine2_step_24_Calculations();
                TestContext.WriteLine("Korrekte verdier i step 24(Beregninger)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 24(Beregninger)");
            }

            this.UIMap.ClickOkEditWishPlan();

            //step 24
            //iverksett linjer
            Playback.Wait(1500);
            this.UIMap.EffectuateTwoLines_step_25();
            this.UIMap.CloseWishPlan();

            //Hack: DelphiTabkomponent
            this.UIMap.SelectEmployeeTabBanks();
            this.UIMap.SelectMozartBank();
            this.UIMap.CheckExpandAllBankDetails();

            try
            {
                this.UIMap.CheckMozartBankValues_step_25();
                TestContext.WriteLine("Korrekte verdier i step 25(Bankverdier Mozart)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 25(Bankverdier Mozart)");
            }

            this.UIMap.SelectSchubertBank();
            //this.UIMap.OpenSchubertSaldotidDetails();
            UIMap.CheckExpandAllBankDetails();

            try
            {
                this.UIMap.CheckSchubertBankValues_step_25();
                TestContext.WriteLine("Korrekte verdier i step 25(Bankverdier Schubert)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 25(Bankverdier Schubert)");
            }

            errorList.AddRange(UIMap.Step_26());
            errorList.AddRange(UIMap.Step_27());

            Playback.Wait(2000);
            this.UIMap.CloseGat();

            var timeAfter = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved kjøring av Test 23"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 23 finished OK");
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
