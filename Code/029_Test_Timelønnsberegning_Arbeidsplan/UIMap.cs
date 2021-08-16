namespace _029_Test_Timelønnsberegning_Arbeidsplan
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
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
    using CommonTestData;
    using System.Diagnostics;

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        TestContext TestContext;
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
            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }

        #region Basic Methods
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
            UICommon.LoginGatAndSelectDepartment(UICommon.DepLønnsberegninger);
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
        public void KillGatProcess()
        {
            SupportFunctions.KillGatProcess(TestContext);
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
        #endregion

        #region Spesific Methods
        private void SelectRosterplanTab(int delay = 0)
        {
            Playback.Wait(delay * 1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }
        private List<string> SelectRosterPlan(string planName, string step)
        {
            Playback.Wait(1000);
            var timeBefore = DateTime.Now;
            UICommon.SelectRosterPlan(planName);

            var timeAfter = DateTime.Now;
            return TimeLapseInSeconds(timeBefore, timeAfter, "Tidsforbruk ved åpning av plan, step " + step, 60, 30);
        }
        public List<string> OpenPlan(string planName, string step)
        {
            Playback.Wait(1500);
            SelectRosterplanTab();
            Playback.Wait(3000);
            return SelectRosterPlan(planName, step);
        }
        public List<string> EffectuateFullplan(bool selectPlanTab, DateTime fromDate, DateTime toDate, int step)
        {
            var errorList = new List<string>();

            UICommon.EffectuateFullRosterplan(selectPlanTab);

            var timeBeforeOpenEffectuationWindow = DateTime.Now;
            if (UICommon.EffectuationWindowExists())
            {
                var timeAfterOpenEffectuationWindow = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeOpenEffectuationWindow, timeAfterOpenEffectuationWindow, "Tidsforbruk ved åpning av iverksettingsvindu, step " + step.ToString(), 5, 0));
            }

            ChangePeriodForActualLines(fromDate, toDate);

            UICommon.EffectuateRosterplanLines(false);

            var timeBeforeEffectuation = DateTime.Now;

            if (UICommon.SalaryCalculationsWindowExists())
            {
                var timeAfterEffectuation = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved iverksetting av plan, step " + step.ToString(), 5, 0));
            }

            errorList.AddRange(SalaryCalculation(step));
          
            UICommon.CloseSalaryCalculationsWindow();
            var timeWhenClosingSalaryCalcWindow = DateTime.Now;
            try
            {
                Playback.Wait(3000);
                UICommon.CheckDeleteEffectuationButtonEnabled(true);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            var timeAfterClosingSalaryCalcWindow = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeWhenClosingSalaryCalcWindow, timeAfterClosingSalaryCalcWindow, "Tidsforbruk ved lasting av plan etter iverksetting, step " + step.ToString(), 8, 2));

            return errorList;
        }

        public List<string> EffectuateRoasterplanNextPeriod(DateTime fromDate, DateTime toDate, int step)
        {
            var errorList = new List<string>();

            UICommon.SelectPlanTabRosterplan();
            UICommon.EffectuateRoasterplanNextPeriod();

            var timeBeforeOpenEffectuationWindow = DateTime.Now;
            if (UICommon.EffectuationWindowExists())
            {
                var timeAfterOpenEffectuationWindow = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeOpenEffectuationWindow, timeAfterOpenEffectuationWindow, "Tidsforbruk ved åpning av iverksettingsvindu, step " + step.ToString(), 5, 0));
            }

            ChangePeriodForActualLines(fromDate, toDate);

            UICommon.EffectuateRosterplanLines(false);

            var timeBeforeEffectuation = DateTime.Now;

            if (UICommon.SalaryCalculationsWindowExists())
            {
                var timeAfterEffectuation = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved iverksetting av plan, step " + step.ToString(), 5, 0));
            }

            errorList.AddRange(SalaryCalculation(step));

            UICommon.CloseSalaryCalculationsWindow();
            var timeWhenClosingSalaryCalcWindow = DateTime.Now;
            try
            {
                Playback.Wait(3000);
                UICommon.CheckDeleteEffectuationButtonEnabled(true);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            var timeAfterClosingSalaryCalcWindow = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeWhenClosingSalaryCalcWindow, timeAfterClosingSalaryCalcWindow, "Tidsforbruk ved lasting av plan etter iverksetting, step " + step.ToString(), 8, 2));

            return errorList;
        }

        public List<string> EffectuateEmpBett_Step19(DateTime fromDate, DateTime toDate, int step)
        {
            var errorList = new List<string>();
            this.EffectuateEmpBett_Step19();

            var timeBeforeOpenEffectuationWindow = DateTime.Now;
            if (UICommon.EffectuationWindowExists())
            {
                var timeAfterOpenEffectuationWindow = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeOpenEffectuationWindow, timeAfterOpenEffectuationWindow, "Tidsforbruk ved åpning av iverksettingsvindu, step " + step.ToString(), 5, 0));
            }

            ChangePeriodForActualLines(fromDate, toDate);

            UICommon.EffectuateRosterplanLines(false);

            var timeBeforeEffectuation = DateTime.Now;

            if (UICommon.SalaryCalculationsWindowExists())
            {
                var timeAfterEffectuation = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved iverksetting av plan, step " + step.ToString(), 5, 0));
            }

            errorList.AddRange(SalaryCalculation(step));

            UICommon.CloseSalaryCalculationsWindow();
            var timeWhenClosingSalaryCalcWindow = DateTime.Now;
            try
            {
                Playback.Wait(3000);
                UICommon.CheckDeleteEffectuationButtonEnabled(true);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            var timeAfterClosingSalaryCalcWindow = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeWhenClosingSalaryCalcWindow, timeAfterClosingSalaryCalcWindow, "Tidsforbruk ved lasting av plan etter iverksetting, step " + step.ToString(), 8, 2));

            return errorList;
        }

        public void ChangePeriodForActualLines(DateTime fromDate, DateTime? toDate)
        {
            UICommon.ChangeEffectuationPeriodForActualLines(fromDate, toDate);
        }

        public List<string> DeleteHolAbsence(string step)
        {
            var errorList = new List<string>();

            CloseRosterplan();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
            try
            {
                DeleteEmpHolAbsence();
            }
            catch (Exception e)
            {
                errorList.Add("Unable to delete absence or absence not exist, step " + step + ": " + e.Message);
            }

            return errorList;
        }

        public List<string> DeleteEffectuationFullplan(string step)
        {
            var errorList = new List<string>();

            UICommon.DeleteEffectuationRosterplan();

            var timeBeforeSelectAll = DateTime.Now;
            UICommon.SelectAllAndWaitForDeleteEffectuationWindowReady();

            var timeAfterSelectAll = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeSelectAll, timeAfterSelectAll, "Tidsforbruk før Slette iverksetting vindu er klart, step " + step, 10, 3));


            var timeBeforeEffectuation = DateTime.Now;
            UICommon.DeleteEffectuatedLines();
            UICommon.CloseDeleteEffectuationOkWindow();

            var timeAfterEffectuation = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved sletting av iverksetting(kalenderplan), step " + step, 5, 0));

            return errorList;
        }

        public List<string> DeleteEffectuationCurrentLines(string step)
        {
            var errorList = new List<string>();

            var timeBeforeSelectAll = DateTime.Now;
            UICommon.SelectAllAndWaitForDeleteEffectuationWindowReady();

            var timeAfterSelectAll = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeSelectAll, timeAfterSelectAll, "Tidsforbruk før Slette iverksetting vindu er klart, step " + step, 5, 0));


            var timeBeforeEffectuation = DateTime.Now;
            UICommon.DeleteEffectuatedLines();
            UICommon.CloseDeleteEffectuationOkWindow();

            var timeAfterEffectuation = DateTime.Now;
            errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved sletting av iverksetting(kalenderplan), step " + step, 5, 0));

            return errorList;
        }

        public void CloseRosterplan()
        {
            UICommon.CloseRosterplanFromPlanTab();
        }

        #endregion

        #region Salary Calculations
        /// <summary>
        /// SalaryCalculation_step4 - Use 'SalaryCalculation_step4ExpectedValues' to pass parameters into this method.
        /// </summary>
        public List<string> SalaryCalculation(int step)
        {
            var errorList = new List<string>();

            if (step == 19)
            {
                errorList.AddRange(CheckSalaryCalculationCode("gcName", empNames.UIBettBrageCellValueAsString, "", "", "", "", "", "", "", "", "", "", 19));

                //kode: 450
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_450", "-8,00", "", "", "", "", "", "", "", "", "", "", step));
                return errorList;
            }
            else if (step == 21)
            {
                errorList.AddRange(CheckSalaryCalculationCode("gcName", empNames.UIJamJalleCellValueAsString, "Kule, Kalle ", "Egg, Ego ", "", "", "", "", "", "", "", "", 21));

                //kode: 200 - 430 - 1010
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_200", "8,00", "0,00", "0,00", "", "", "", "", "", "", "", "", step));
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_430", "4,00", "0,00", "0,00", "", "", "", "", "", "", "", "", step));
                return errorList;
            }
            else
            {
                errorList.AddRange(CheckSalaryCalculationCode("gcName", empNames.UIAdelAlbertCellValueAsString,
                empNames.UIBettBrageCellValueAsString, empNames.UIClueCelesteCellValueAsString,
                empNames.UIClueCelesteCellValueAsString, empNames.UIDunDiegoCellValueAsString,
                empNames.UIFalsFannyCellValueAsString, empNames.UIGucciGustavoCellValueAsString,
                empNames.UIHolHenryCellValueAsString, empNames.UIEggEgoCellValueAsString,
                empNames.UIIsIanCellValueAsString, empNames.UIJamJalleCellValueAsString
                , step));
            }

            if (step == 4)
            {
                //kode: 200 - 400 - 410 - 1010
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_200", "8,50", "10,00", "3,00", "3,50", "15,00", "0,00", "8,00", "7,50", "14,67", "7,25", "0,00",step));
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_400", "0,00", "2,00", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", step));
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_410", "0,00", "8,00", "3,00", "0,00", "5,67", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", step));
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_1010", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "20,00", "8,00", "0,00", step));
            }
            else if (step == 7)
            {
                //kode: 200 - 430 - 410 - 1010
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_200", "9,00", "10,00", "3,00", "3,50", "15,50", "0,00", "8,00", "8,00", "14,67", "7,25", "0,00", step));
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_430", "9,00", "10,00", "3,00", "3,50", "11,50", "0,00", "8,00", "8,00", "10,67", "7,25", "7,50", step));
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_410", "0,00", "8,00", "3,00", "0,00", "5,67", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", step));
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_1010", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "20,00", "8,00", "0,00", step));
            }
            else if (step == 12)
            {
                //kode: 450
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_450", "-8,50", "-9,32", "0,00", "0,00", "0,00", "0,00", "-12,00", "-7,50", "-14,67", "0,00", "0,00", step));
            }
            else if (step == 14)
            {
                //kode: 430 - 450
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_430", "0,00", "0,00", "3,00", "0,00", "11,50", "0,00", "0,00", "0,00", "0,00", "4,00", "7,50", step));
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_450", "-5,50", "0,00", "0,00", "0,00", "0,00", "0,00", "-4,00", "-4,50", "-4,00", "0,00", "0,00", step));
            }
            else if (step == 16)
            {
                //kode: 200 - 430 - 450
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_200", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "8,00", "0,00", "0,00", "0,00", step));
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_430", "0,00", "10,00", "3,00", "0,00", "0,00", "0,00", "8,00", "8,00", "0,00", "7,25", "7,50", step));
                errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_450", "0,00", "0,00", "0,00", "0,00", "-4,00", "0,00", "0,00", "0,00", "-4,00", "0,00", "0,00", step));
            }
            //else if (step == 14)
            //{
            //    //kode: 430 - 450
            //    errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_430", "3,00", "0,00", "3,00", "0,00", "11,50", "0,00", "0,00", "0,00", "10,67", "4,00", "7,50", step));
            //    errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_450", "-8,50", "-9,32", "0,00", "0,00", "0,00", "0,00", "-12,00", "0,00", "-14,67", "0,00", "0,00", step));
            //}
            //else if (step == 16)
            //{
            //    //kode: 200 - 430 - 450
            //    errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_200", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "0,00", "8,00", "0,00", "0,00", "0,00", step));
            //    errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_430", "9,00", "10,00", "3,00", "3,50", "11,50", "0,00", "8,00", "8,00", "10,67", "7,25", "7,50", step));
            //    errorList.AddRange(CheckSalaryCalculationCode("PaymentCell_450", "-9,00", "0,00", "0,00", "-3,50", "-15,50", "0,00", "0,00", "0,00", "-14,67", "0,00", "-7,50", step));
            //}
            
            return errorList;
        }

        #endregion
        private SalaryCalculation_EmpNames empNames = new SalaryCalculation_EmpNames();

        private List<string> CheckSalaryCalculationCode(string code, string lineVal_1, string lineVal_2, string lineVal_3, string lineVal_4, string lineVal_5, string lineVal_6, string lineVal_7, string lineVal_8, string lineVal_9, string lineVal_10, string lineVal_11, int step)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXCell line1Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem000Cell22;
            DXCell line2Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem000Cell23;
            DXCell line3Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem000Cell24;
            DXCell line4Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem000Cell25;
            DXCell line5Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem000Cell26;
            DXCell line6Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem000Cell27;
            DXCell line7Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem000Cell28;
            DXCell line8Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem000Cell29;
            DXCell line9Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem12000Cell;
            DXCell line10Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem800Cell2;
            DXCell line11Cell = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable.UIItem000Cell30;

            line1Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            line2Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            line3Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            line4Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            line5Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            line6Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            line7Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            line8Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            line9Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            line10Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            line11Cell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);

            line1Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]0[Column]" + code;
            line2Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]1[Column]" + code;
            line3Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]2[Column]" + code;
            line4Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]3[Column]" + code;
            line5Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]4[Column]" + code;
            line6Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]5[Column]" + code;
            line7Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]6[Column]" + code;
            line8Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]7[Column]" + code;
            line9Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]8[Column]" + code;
            line10Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]9[Column]" + code;
            line11Cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcReceiptGridControlCell[View]gvReceipt[Row]10[Column]" + code;

            #endregion

            #region Value Check
            var errorMessageLine1 = "Feil på Adel linje";
            var errorMessageLine2 = "Feil på Bett linje";
            var errorMessageLine3 = "Feil på Clue linje1";

            if (step == 19)
            {
                errorMessageLine1 = "Feil på Bett linje";
            }
            else if (step == 21)
            {
                errorMessageLine1 = "Feil på Jam linje";
                errorMessageLine2 = "Feil på Kule linje";
                errorMessageLine3 = "Feil på Egg linje";
            }

            if (!String.IsNullOrEmpty(lineVal_1))
                try
                {
                    Assert.AreEqual(lineVal_1, line1Cell.ValueAsString, errorMessageLine1);
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step.ToString() + "(" + code + ")");
                }

            if (!String.IsNullOrEmpty(lineVal_2))
                try
                {
                    Assert.AreEqual(lineVal_2, line2Cell.ValueAsString, errorMessageLine2);
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step + "(" + code + ")");
                }

            if (!String.IsNullOrEmpty(lineVal_3))
                try
                {
                    Assert.AreEqual(lineVal_3, line3Cell.ValueAsString, errorMessageLine3);
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step + "(" + code + ")");
                }

            if (!String.IsNullOrEmpty(lineVal_4))
                try
                {
                    Assert.AreEqual(lineVal_4, line4Cell.ValueAsString, "Feil på Clue linje2");
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step + "(" + code + ")");
                }

            if (!String.IsNullOrEmpty(lineVal_5))
                try
                {
                    Assert.AreEqual(lineVal_5, line5Cell.ValueAsString, "Feil på Dun linje");
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step + "(" + code + ")");
                }

            if (!String.IsNullOrEmpty(lineVal_6))
                try
                {
                    Assert.AreEqual(lineVal_6, line6Cell.ValueAsString, "Feil på Fals linje");
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step + "(" + code + ")");
                }

            if (!String.IsNullOrEmpty(lineVal_7))
                try
                {
                    Assert.AreEqual(lineVal_7, line7Cell.ValueAsString, "Feil på Gucci linje");
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step + "(" + code + ")");
                }

            if (!String.IsNullOrEmpty(lineVal_8))
                try
                {
                    Assert.AreEqual(lineVal_8, line8Cell.ValueAsString, "Feil på Hol linje");
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step + "(" + code + ")");
                }

            if (!String.IsNullOrEmpty(lineVal_9))
                try
                {
                    Assert.AreEqual(lineVal_9, line9Cell.ValueAsString, "Feil på Egg linje");
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step + "(" + code + ")");
                }

            if (!String.IsNullOrEmpty(lineVal_10))
                try
                {
                    Assert.AreEqual(lineVal_10, line10Cell.ValueAsString, "Feil på Is linje");
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step + "(" + code + ")");
                }

            if (!String.IsNullOrEmpty(lineVal_11))
                try
                {
                    Assert.AreEqual(lineVal_11, line11Cell.ValueAsString, "Feil på Jam linje");
                }
                catch (Exception e)
                {
                    errorList.Add(e.Message + "_step_" + step + "(" + code + ")");
                }


            #endregion

            return errorList;
        }
    }

    /// <summary>
    /// Parameters to be passed into 'SalaryCalculation_step4'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class SalaryCalculation_EmpNames
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Adel, Albert' cell equals 'Adel, Albert '
        /// </summary>
        public string UIAdelAlbertCellValueAsString = "Adel, Albert ";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Bett, Brage' cell equals 'Bett, Brage '
        /// </summary>
        public string UIBettBrageCellValueAsString = "Bett, Brage ";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Clue, Celeste' cell equals 'Clue, Celeste '
        /// </summary>
        public string UIClueCelesteCellValueAsString = "Clue, Celeste ";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Dun, Diego' cell equals 'Dun, Diego '
        /// </summary>
        public string UIDunDiegoCellValueAsString = "Dun, Diego ";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Fals, Fanny' cell equals 'Fals, Fanny '
        /// </summary>
        public string UIFalsFannyCellValueAsString = "Fals, Fanny ";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Gucci, Gustavo' cell equals 'Gucci, Gustavo '
        /// </summary>
        public string UIGucciGustavoCellValueAsString = "Gucci, Gustavo ";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Hol, Henry' cell equals 'Hol, Henry '
        /// </summary>
        public string UIHolHenryCellValueAsString = "Hol, Henry ";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Egg, Ego' cell equals 'Egg, Ego '
        /// </summary>
        public string UIEggEgoCellValueAsString = "Egg, Ego ";

        /// Verify that the 'ValueAsString' property of 'Is, Ian' cell equals 'Is, Ian '
        /// </summary>
        public string UIIsIanCellValueAsString = "Is, Ian ";
        
        /// Verify that the 'ValueAsString' property of 'Jam, Jalle' cell equals 'Jam, Jalle '
        /// </summary>
        public string UIJamJalleCellValueAsString = "Jam, Jalle ";
        #endregion
    }
    
}
