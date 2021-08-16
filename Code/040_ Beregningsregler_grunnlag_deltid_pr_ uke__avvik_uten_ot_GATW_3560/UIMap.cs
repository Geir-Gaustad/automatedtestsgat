namespace _040_Beregningsregler_grunnlag_deltid_pr_uke_avvik_uten_ot_GATW_3560
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
    using System.Globalization;
    using System.Threading;

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

        #region Common Methods
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
            return UICommon.RestoreDatabase(false, false, true);
        }
        private void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepLønnsberegninger2100, null, "", logGatInfo, false, false, false);
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

        private void GoToShiftDateNew(DateTime date)
        {
            UICommon.GoToShiftbookdate(date);
        }

        #endregion

        public List<string> Step_1()
        {
            var errorList = new List<string>();

            StartGat(true);
            //Tirsdag uke 2
            GoToShiftDateNew(new DateTime(2023, 10, 31));
            try
            {
                DragEksklTurnusToExtraStep1();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("N ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 1: " + e.Message);
            }
            try
            {
                CheckValuesStep1();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 1: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            UICommon.UIMapVS2017.GenerateAMLCause();

            return errorList;
        }
        public List<string> Step_2()
        {
            var errorList = new List<string>();

            //Torsdag uke 2
            GoToShiftDateNew(new DateTime(2023, 11, 02));
            try
            {
                DragEksklTurnusToExtraStep2();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("A ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2: " + e.Message);
            }
            try
            {
                CheckValuesStep2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            UICommon.UIMapVS2017.GenerateAMLCause();

            return errorList;
        }
        public List<string> Step_3()
        {
            var errorList = new List<string>();

            //Lørdag uke 2
            GoToShiftDateNew(new DateTime(2023, 11, 04));
            try
            {
                DragEksklTurnusToExtraStep3();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }
            try
            {
                CheckValuesStep3();
                UICommon.UIMapVS2017.SelectVaktTimegrunnlagTabExtraWindow();
                CCheckValuesStep3_2_new();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            return errorList;
        }
        private void CCheckValuesStep3_2_new()
        {

            var check = UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UITcExtraDetailTabList.UITpClockTypeCalculatiClient;

            if (check.Exists)
                CheckValuesStep3_2_org();
            else
                CheckValuesStep3_2();
        }

        public List<string> Step_4()
        {
            var errorList = new List<string>();

            //Søndag uke 2
            GoToShiftDateNew(new DateTime(2023, 11, 05));
            try
            {
                DragEksklTurnusToExtraStep4();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }
            try
            {
                CheckValuesStep4();
                UICommon.UIMapVS2017.SelectVaktTimegrunnlagTabExtraWindow();

                CheckValuesStep4_2_new();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            UICommon.UIMapVS2017.GenerateAMLCause();


            return errorList;
        }
        private void CheckValuesStep4_2_new()
        {

            var check = UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UITcExtraDetailTabList.UITpClockTypeCalculatiClient;

            if (check.Exists)
                CheckValuesStep4_2_org();
            else
                CheckValuesStep4_2();
        }
        
        public List<string> Step_5()
        {
            var errorList = new List<string>();

            //Onsdag uke 2
            GoToShiftDateNew(new DateTime(2023, 11, 1));
            DragEksklTurnusToToAbsenceStep5();
            UICommon.UIMapVS2017.SelectAbsenceCode("90");
            UICommon.UIMapVS2017.RecalculateInAbsenceWindow();

            try
            {
                CheckValuesStep5();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }
            UICommon.UIMapVS2017.ClickOkConstuctAbsence();
            try
            {
                CheckRecalcValuesStep5();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        public List<string> Step_6()
        {
            var errorList = new List<string>();

            //Tirsdag uke 2
            GoToShiftDateNew(new DateTime(2023, 10, 31));
            DeleteShiftStep6();
            UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();
            //Torsdag uke 2
            GoToShiftDateNew(new DateTime(2023, 11, 02));
            DeleteShiftStep6_2();
            UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();

            try
            {
                if (UICommon.UIMapVS2017.CheckRecalculationWindowExists())
                {
                    errorList.Add("Recalculation is active(Step 6)");
                    UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Recalculation not active(Step 6)");
            }

            return errorList;
        }
        public List<string> Step_7()
        {
            var errorList = new List<string>();

            //Onsdag uke 2
            GoToShiftDateNew(new DateTime(2023, 11, 01));
            DeleteAbsenceStep7();
            UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();

            try
            {
                CheckRecalcValuesStep7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }

        public List<string> Step_8()
        {
            var errorList = new List<string>();

            UICommon.SelectFromAdministration("Regelsett +definisjon +ulike", true);
            SetRulesetValuesStep8();

            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
            try
            {
                SelectEkskl();
                UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.TimesheetTabBR, false);

                UICommon.UIMapVS2017.ClickRecalculateInEmpTimesheetTab();
                UICommon.UIMapVS2017.SetRecalculatePeriodInTimesheetTab("Uke 44 2023", "Uke 44 2023");
                UICommon.UIMapVS2017.RecalculateInTimesheetTabRecalcWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 8: " + e.Message);
            }

            try
            {
                CheckRecalcValuesStep8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
            UICommon.UIMapVS2017.CloseTimesheetTabRecalcWindow();

            return errorList;
        }

        public List<string> Step_9()
        {
            var errorList = new List<string>();

            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
            //Søndag uke 2
            GoToShiftDateNew(new DateTime(2023, 10, 30));
            try
            {
                DragEksklTurnusToExtraStep9();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SetExtraShiftPeriod("07:00", "16:00");
                UICommon.UIMapVS2017.SelectShitfBookColumnExtra("Dag");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 9: " + e.Message);
            }
            try
            {
                CheckValuesStep9();
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

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            UICommon.UIMapVS2017.GenerateAMLCause();

            try
            {
                CheckRecalcValuesStep10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        public List<string> Step_11()
        {
            var errorList = new List<string>();

            //Torsdag uke 3
            GoToShiftDateNew(new DateTime(2023, 11, 09));
            try
            {
                DragEksklTurnusToExtraStep11();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }
            try
            {
                CheckValuesStep11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            return errorList;
        }
        public List<string> Step_12()
        {
            var errorList = new List<string>();

            //Tirsdag uke 3
            GoToShiftDateNew(new DateTime(2023, 11, 07));
            DragEksklTurnusToToAbsenceStep12();
            UICommon.UIMapVS2017.SelectAbsenceCode("10 +egen");
            UICommon.UIMapVS2017.ClickOkConstuctAbsence();
            try
            {
                if (UICommon.UIMapVS2017.CheckRecalculationWindowExists())
                {
                    errorList.Add("Recalculation is active(Step 12)");
                    UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Recalculation not active(Step 12)");
            }

            try
            {
                CheckAbsenceAddedStep12();
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

            //Mandag uke 3
            GoToShiftDateNew(new DateTime(2023, 11, 06));
            DragEksklTurnusToToAbsenceStep13();
            UICommon.UIMapVS2017.SelectAbsenceCode("90");
            UICommon.UIMapVS2017.RecalculateInAbsenceWindow();

            try
            {
                CheckValuesStep13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }
            UICommon.UIMapVS2017.ClickOkConstuctAbsence();
            try
            {
                CheckRecalcValuesStep13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 13: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }
        public List<string> Step_14()
        {
            var errorList = new List<string>();

            //Tirsdag uke 3
            GoToShiftDateNew(new DateTime(2023, 11, 07));
            DeleteAbsenceStep14();
            UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();
            try
            {
                if (UICommon.UIMapVS2017.CheckRecalculationWindowExists())
                {
                    errorList.Add("Recalculation is active(Step 14)");
                    UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Recalculation not active(Step 14)");
            }

            return errorList;
        }
        public List<string> Step_15()
        {
            var errorList = new List<string>();

            //Mandag uke 3
            GoToShiftDateNew(new DateTime(2023, 11, 06));
            DeleteAbsenceStep15();
            UICommon.UIMapVS2017.ClickOkInDeleteShiftWindow();

            try
            {
                CheckRecalcValuesStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 15: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            return errorList;
        }

        public List<string> Step_16()
        {
            var errorList = new List<string>();

            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
            try
            {
                SelectEkskl();
                UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.DayAndWeekSeparationEmployeeTab, false);
                //Onsdag 27.09.2023
                UICommon.UIMapVS2017.ClickNewSeparationInDayAndWeekSeparationEmployeeTab();
                CreateWeekSeparationStep16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 16: " + e.Message);
            }

            try
            {
                CloseGat();
                StartGat(false);

                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
                SelectEkskl();
                UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.TimesheetTabBR, false);
                UICommon.UIMapVS2017.ClickRecalculateInEmpTimesheetTab();
                UICommon.UIMapVS2017.SetRecalculatePeriodInTimesheetTab("Uke 43 2023", "Uke 45 2023");
                UICommon.UIMapVS2017.RecalculateInTimesheetTabRecalcWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 16: " + e.Message);
            }

            try
            {
                //Mandag 30.10.2023 (Uke 2) og Søndag 05.11.2023(Uke 2) blir rekalkulert
                Playback.Wait(3000);
                CheckRecalcValuesStep16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 16: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
            UICommon.UIMapVS2017.CloseTimesheetTabRecalcWindow();

            return errorList;
        }
        public List<string> Step_17()
        {
            var errorList = new List<string>();

            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
            //Lørdag uke 2
            GoToShiftDateNew(new DateTime(2023, 11, 08));
            try
            {
                UICommon.UIMapVS2017.ClickExtraRibbonButton();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                SelectEksklTurnus();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 17: " + e.Message);
            }
            try
            {
                CheckValuesStep17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 17: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            UICommon.UIMapVS2017.GenerateAMLCause();

            return errorList;
        }
        public List<string> Step_18()
        {
            var errorList = new List<string>();

            //Lørdag uke 3
            GoToShiftDateNew(new DateTime(2023, 11, 11));
            try
            {
                UICommon.UIMapVS2017.ClickExtraRibbonButton();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                SelectEksklTurnus();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("D ");
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 18: " + e.Message);
            }
            try
            {
                CheckValuesStep18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 18: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            Playback.Wait(2000);

            return errorList;
        }
        public List<string> Step_19()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.SelectFromAdministration("Regelsett +definisjon +ulike", true);
                SetRulesetValuesStep19();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 19. Unable to set ruleset value: " + e.Message);
            }

            CloseGat();

            return errorList;
        }


        /// <summary>
        /// CheckRecalcValuesStep8 - Use 'CheckRecalcValuesStep8ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckRecalcValuesStep8()
        {
            #region Variable Declarations
            var recalcWindow = UIRekalkuleringWindow;
            recalcWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Rekalkulering";
            DXCell uIItem05112023Cell = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem05112023Cell;
            DXCell uIOvertidEkstravaktCell = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIOvertidEkstravaktCell;
            DXCell uIItem200TimelønnCell = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem200TimelønnCell;
            DXCell uIItem800Cell = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem800Cell;
            #endregion

            // Verify that the 'Text' property of '05.11.2023' cell equals '05.11.2023'
            Assert.AreEqual(this.CheckRecalcValuesStep8ExpectedValues.UIItem05112023CellText, uIItem05112023Cell.Text);

            // Verify that the 'Text' property of 'Overtid/Ekstravakt' cell equals 'Overtid/Ekstravakt'
            Assert.AreEqual(this.CheckRecalcValuesStep8ExpectedValues.UIOvertidEkstravaktCellText, uIOvertidEkstravaktCell.Text);

            // Verify that the 'Text' property of '200 - Timelønn' cell equals '200 - Timelønn'
            Assert.AreEqual(this.CheckRecalcValuesStep8ExpectedValues.UIItem200TimelønnCellText, uIItem200TimelønnCell.Text);

            // Verify that the 'Text' property of '8,00' cell equals '8,00'
            Assert.AreEqual(this.CheckRecalcValuesStep8ExpectedValues.UIItem800CellText, uIItem800Cell.Text);
        }
        /// <summary>
        /// CheckRecalcValuesStep16 - Use 'CheckRecalcValuesStep16ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckRecalcValuesStep16()
        {
            #region Variable Declarations
            var recalcWindow = UIRekalkuleringWindow;
            recalcWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Rekalkulering";
            DXCell uIItem05112023Cell = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem05112023Cell;
            DXCell uIOvertidEkstravaktCell = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIOvertidEkstravaktCell;
            DXCell uIItem200TimelønnCell = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem200TimelønnCell;
            DXCell uIItem800Cell = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem800Cell;
            DXCell uIItem400LørSøndagstilCell = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem400LørSøndagstilCell;
            DXCell uIItem800Cell1 = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem800Cell1;
            DXCell uIItem05112023Cell1 = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem05112023Cell1;
            DXCell uIOvertidEkstravaktCell1 = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIOvertidEkstravaktCell1;
            DXCell uIItem200TimelønnCell1 = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem200TimelønnCell1;
            DXCell uIItem800Cell2 = recalcWindow.UIPcContentClient.UIGcRecalculationTable.UIItem800Cell2;
            #endregion

            // Verify that the 'Text' property of '05.11.2023' cell equals '30.10.2023'
            Assert.AreEqual(this.CheckRecalcValuesStep16ExpectedValues.UIItem05112023CellText, uIItem05112023Cell.Text);

            // Verify that the 'Text' property of 'Overtid/Ekstravakt' cell equals 'Overtid/Ekstravakt'
            Assert.AreEqual(this.CheckRecalcValuesStep16ExpectedValues.UIOvertidEkstravaktCellText, uIOvertidEkstravaktCell.Text);

            // Verify that the 'Text' property of '200 - Timelønn' cell equals '200 - Timelønn'
            Assert.AreEqual(this.CheckRecalcValuesStep16ExpectedValues.UIItem200TimelønnCellText, uIItem200TimelønnCell.Text);

            // Verify that the 'Text' property of '8,00' cell equals '8,00'
            Assert.AreEqual(this.CheckRecalcValuesStep16ExpectedValues.UIItem800CellText, uIItem800Cell.Text);

            // Verify that the 'Text' property of '400 - Lør./Søndagstillegg' cell equals '310 - Overtid 100%'
            Assert.AreEqual(this.CheckRecalcValuesStep16ExpectedValues.UIItem400LørSøndagstilCellText, uIItem400LørSøndagstilCell.Text);

            // Verify that the 'Text' property of '8,00' cell equals '1,00'
            Assert.AreEqual(this.CheckRecalcValuesStep16ExpectedValues.UIItem800Cell1Text, uIItem800Cell1.Text);

            // Verify that the 'Text' property of '05.11.2023' cell equals '05.11.2023'
            Assert.AreEqual(this.CheckRecalcValuesStep16ExpectedValues.UIItem05112023Cell1Text, uIItem05112023Cell1.Text);

            // Verify that the 'Text' property of 'Overtid/Ekstravakt' cell equals 'Overtid/Ekstravakt'
            Assert.AreEqual(this.CheckRecalcValuesStep16ExpectedValues.UIOvertidEkstravaktCell1Text, uIOvertidEkstravaktCell1.Text);

            // Verify that the 'Text' property of '200 - Timelønn' cell equals '200 - Timelønn'
            Assert.AreEqual(this.CheckRecalcValuesStep16ExpectedValues.UIItem200TimelønnCell1Text, uIItem200TimelønnCell1.Text);

            // Verify that the 'Text' property of '8,00' cell equals '8,00'
            Assert.AreEqual(this.CheckRecalcValuesStep16ExpectedValues.UIItem800Cell2Text, uIItem800Cell2.Text);
        }

        public virtual CheckRecalcValuesStep8ExpectedValues CheckRecalcValuesStep8ExpectedValues
        {
            get
            {
                if ((this.mCheckRecalcValuesStep8ExpectedValues == null))
                {
                    this.mCheckRecalcValuesStep8ExpectedValues = new CheckRecalcValuesStep8ExpectedValues();
                }
                return this.mCheckRecalcValuesStep8ExpectedValues;
            }
        }

        private CheckRecalcValuesStep8ExpectedValues mCheckRecalcValuesStep8ExpectedValues;


        public virtual CheckRecalcValuesStep16ExpectedValues CheckRecalcValuesStep16ExpectedValues
        {
            get
            {
                if ((this.mCheckRecalcValuesStep16ExpectedValues == null))
                {
                    this.mCheckRecalcValuesStep16ExpectedValues = new CheckRecalcValuesStep16ExpectedValues();
                }
                return this.mCheckRecalcValuesStep16ExpectedValues;
            }
        }

        private CheckRecalcValuesStep16ExpectedValues mCheckRecalcValuesStep16ExpectedValues;
    }
    /// <summary>
    /// Parameters to be passed into 'CheckRecalcValuesStep8'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class CheckRecalcValuesStep8ExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'Text' property of '05.11.2023' cell equals '05.11.2023'
        /// </summary>
        public string UIItem05112023CellText = "05.11.2023";

        /// <summary>
        /// Verify that the 'Text' property of 'Overtid/Ekstravakt' cell equals 'Overtid/Ekstravakt'
        /// </summary>
        public string UIOvertidEkstravaktCellText = "Overtid/Ekstravakt";

        /// <summary>
        /// Verify that the 'Text' property of '200 - Timelønn' cell equals '200 - Timelønn'
        /// </summary>
        public string UIItem200TimelønnCellText = "200 - Timelønn";

        /// <summary>
        /// Verify that the 'Text' property of '8,00' cell equals '8,00'
        /// </summary>
        public string UIItem800CellText = "8,00";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckRecalcValuesStep16'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class CheckRecalcValuesStep16ExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'Text' property of '05.11.2023' cell equals '30.10.2023'
        /// </summary>
        public string UIItem05112023CellText = "30.10.2023";

        /// <summary>
        /// Verify that the 'Text' property of 'Overtid/Ekstravakt' cell equals 'Overtid/Ekstravakt'
        /// </summary>
        public string UIOvertidEkstravaktCellText = "Overtid/Ekstravakt";

        /// <summary>
        /// Verify that the 'Text' property of '200 - Timelønn' cell equals '200 - Timelønn'
        /// </summary>
        public string UIItem200TimelønnCellText = "200 - Timelønn";

        /// <summary>
        /// Verify that the 'Text' property of '8,00' cell equals '8,00'
        /// </summary>
        public string UIItem800CellText = "8,00";

        /// <summary>
        /// Verify that the 'Text' property of '400 - Lør./Søndagstillegg' cell equals '310 - Overtid 100%'
        /// </summary>
        public string UIItem400LørSøndagstilCellText = "310 - Overtid 100%";

        /// <summary>
        /// Verify that the 'Text' property of '8,00' cell equals '1,00'
        /// </summary>
        public string UIItem800Cell1Text = "1,00";

        /// <summary>
        /// Verify that the 'Text' property of '05.11.2023' cell equals '05.11.2023'
        /// </summary>
        public string UIItem05112023Cell1Text = "05.11.2023";

        /// <summary>
        /// Verify that the 'Text' property of 'Overtid/Ekstravakt' cell equals 'Overtid/Ekstravakt'
        /// </summary>
        public string UIOvertidEkstravaktCell1Text = "Overtid/Ekstravakt";

        /// <summary>
        /// Verify that the 'Text' property of '200 - Timelønn' cell equals '200 - Timelønn'
        /// </summary>
        public string UIItem200TimelønnCell1Text = "200 - Timelønn";

        /// <summary>
        /// Verify that the 'Text' property of '8,00' cell equals '8,00'
        /// </summary>
        public string UIItem800Cell2Text = "8,00";
        #endregion
    }
}
