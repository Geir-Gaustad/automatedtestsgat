namespace _032_Test_Rekalkulering_Overfort_Lonn_GATW_4284
{
    using System.CodeDom.Compiler;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using CommonTestData;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        private DateTime CurrentDate;
        #endregion

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            CurrentDate = GetNextDayOfWeekDay(DateTime.Now, DayOfWeek.Wednesday);
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
            return UICommon.RestoreDatabase(false,false,true);
        }
        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepStatistikkavd, null, "", logGatInfo);
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
                UICommon.CloseRosterplanFromHomeTab();
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
        private void EffectuatePeriod(DateTime? effDate, DateTime toDate)
        {
            UICommon.ChangeEffectuationPeriodForActualLines(effDate, toDate);
            UICommon.EffectuateRosterplanLines(false);
        }

        private DateTime GetNextDayOfWeekDay(DateTime from, DayOfWeek dayOfWeek)
        {
            return SupportFunctions.NextDayOfWeekDay(from, dayOfWeek);
        }
        private DateTime GetPreviousDayOfWeekDay(DateTime from, DayOfWeek dayOfWeek)
        {
            return SupportFunctions.PreviousDayOfWeekDay(from, dayOfWeek);
        }
        private void DeleteEffectuation()
        {
            if (UICommon.SelectAllAndWaitForDeleteEffectuationWindowReady())
            {
                UICommon.DeleteEffectuatedLines();
            }
        }
        public void SelectReport(string reportName, bool changeSettings)
        {
            UICommon.SelectReport(reportName, changeSettings, true);
        }
        public void ClickCloseExportLogWindow()
        {
            UICommon.UIMapVS2017.ClickCloseExportLogWindow();
        }
        #endregion

        public List<string> Step_1()
        {
            var errorList = new List<string>();

            try
            {
                StartGat(true);
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

            GoToShiftDateNew(CurrentDate);

            try
            {
                DragFredriksenToExtraStep2();
                UICommon.UIMapVS2017.SelectDivOvertimeCode();
                UICommon.UIMapVS2017.SelectShiftCodeInExtraWindow("A ");
            }
            catch (Exception e)
            {
                errorList.Add("Error creating extra, Step 2: " + e.Message);
            }
            try
            {
                CheckValuesStep2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 2 " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();

            return errorList;
        }
        public List<string> Step_3()
        {
            var errorList = new List<string>();

            OpenPlan("Grunnlag fra 2020");

            try
            {
                OpenEffectuateGundersenLine();
                EffectuatePeriod(null, GetNextDayOfWeekDay(CurrentDate, DayOfWeek.Sunday));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }

            if (UICommon.SalaryCalculationsWindowExists())
            {
                UICommon.CloseSalaryCalculationsWindow();
                TestContext.WriteLine("SalaryCalculationsWindow showing, Step 3");
            }
            else
                TestContext.WriteLine("SalaryCalculationsWindow not showing, Step 3");

            try
            {
                OpenEffectuateFredriksenLine();
                EffectuatePeriod(null, GetNextDayOfWeekDay(CurrentDate, DayOfWeek.Sunday));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 3: " + e.Message);
            }
            try
            {
                CheckValuesStep3();
            }
            catch (Exception e)
            {
                errorList.Add("Error in recalc values Step 3 " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();

            if (UICommon.SalaryCalculationsWindowExists())
            {
                UICommon.CloseSalaryCalculationsWindow();
                TestContext.WriteLine("SalaryCalculationsWindow showing, Step 3_2");
            }
            else
                TestContext.WriteLine("SalaryCalculationsWindow not showing, Step 3_2");

            return errorList;
        }
        public List<string> Step_4()
        {
            var errorList = new List<string>();

            CloseRosterPlan();
            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Employee);
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.TimesheetTabBR, false);

            OpenExtraShiftStep4();

            try
            {
                CheckEmpStep4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4: " + e.Message);
            }

            try
            {
                CopyToClipBoardStep4();
                if (Clipboard.ContainsText())
                {
                    var compareText = @"TT/LTA	Antall	Konto	Ansvar	Avdeling	Prosjekt	Dim 5 test	Dim 6 test	Arbeidssted	EFO - timer	Kommentar	
300 - Overtid 50%	4	5100 - Overtid generelt	1001 - ANSVAR K1	95 - Statistikkavdelingen					6		
310 - Overtid 100%	3,75	5100 - Overtid generelt	1001 - ANSVAR K1	95 - Statistikkavdelingen					7,5		
";

                    var text = Clipboard.GetText(TextDataFormat.Text);
                    Clipboard.Clear();

                    try
                    {
                        Assert.AreEqual(compareText, text);
                    }
                    catch (Exception e)
                    {
                        errorList.Add("Error checking extra step 4: " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 4");
            }

            UICommon.UIMapVS2017.CloseShiftInEmpTab();

            return errorList;
        }
        public List<string> Step_5()
        {
            var errorList = new List<string>();

            OpenPlan("Grunnlag fra 2020");

            try
            {
                OpenDeleteEffectuatuationFredriksenLine();
                DeleteEffectuation();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 5: " + e.Message);
            }

            try
            {
                CheckValuesStep5();
            }
            catch (Exception e)
            {
                errorList.Add("Error in recalc values Step 5 " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
            UICommon.CloseDeleteEffectuationOkWindow();

            return errorList;
        }

        public List<string> Step_6()
        {
            var errorList = new List<string>();

            CloseRosterPlan();
            SelectReport("Timeliste Variabel lønn", true);

            SetReport60Values();
            SetReport60Values2();
            ApproveFredriksenStep6();
            ApproveFredrikseLevel1nStep6();
            ApproveFredrikseLevel2nStep6();
            ClickStartTransfere();

            try
            {
                CheckExtraInList();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 6: " + e.Message);
            }

            ClickStartExport();
            ClickCloseExportLogWindow();

            return errorList;
        }

        private void SetReport60Values()
        {
            #region Variable Declarations

            WinEdit uIItemEdit = this.UIGatWindow.UIItem01042020Window.UIItemEdit;
            WinEdit uIItemEdit1 = this.UIGatWindow.UIItemWindow1.UIItemEdit;
            var from = GetPreviousDayOfWeekDay(CurrentDate, DayOfWeek.Monday);
            var to = GetNextDayOfWeekDay(CurrentDate, DayOfWeek.Sunday);
            #endregion

            // Type '01.05.2020' in text box
            uIItemEdit.Text = from.ToString("ddMMyyyy");

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit, "{Tab}");

            // Type '17.05.2020' in text box
            uIItemEdit1.Text = to.ToString("ddMMyyyy");

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit1, "{Tab}");
        }

        public List<string> Step_7()
        {
            var errorList = new List<string>();

            OpenPlan("Grunnlag fra 2020");

            try
            {
                OpenEffectuateFredriksenLine();
                EffectuatePeriod(null, GetNextDayOfWeekDay(CurrentDate, DayOfWeek.Sunday));
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 7: " + e.Message);
            }

            try
            {
                if (UICommon.UIMapVS2017.CheckRecalculationWindowExists())
                {
                    errorList.Add("Recalculation is active(Step 7)");
                    UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Recalculation not active(Step 7)");
            }

            if (UICommon.SalaryCalculationsWindowExists())
            {
                UICommon.CloseSalaryCalculationsWindow();
                TestContext.WriteLine("SalaryCalculationsWindow showing, Step 7");
            }
            else
                TestContext.WriteLine("SalaryCalculationsWindow not showing, Step 7");

            return errorList;
        }

        public List<string> Step_8()
        {
            var errorList = new List<string>();

            CloseRosterPlan();
            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Shiftbook);
            GoToShiftDateNew(CurrentDate.AddDays(1));

            DragGundersenToExtraStep8();
            UICommon.UIMapVS2017.SelectDivOvertimeCode();
            UICommon.UIMapVS2017.SetExtraShiftPeriod("04:00", "07:00");
            UICommon.UIMapVS2017.SelectShitfBookColumnExtra("Natt");


            try
            {
                CheckOvertimeBeforeStep8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8 " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkNewInExtraWindow();
            UICommon.UIMapVS2017.SetExtraShiftPeriod("15:00", "23:00");
            UICommon.UIMapVS2017.SelectShitfBookColumnExtra("Aften");

            try
            {
                CheckOvertimeBehindStep8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 8 " + e.Message);
            }

            UICommon.UIMapVS2017.ClickOkInExtraWindow();
            UICommon.UIMapVS2017.GenerateAMLCause();

            return errorList;
        }

        public List<string> Step_9()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.SelectFromAdministration("regelsett +Definisjon av +ulike", true);
                ChangeRulesetGundersen(); ;
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 9: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_10()
        {
            #region Variable Declarations
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            var fromWeekDay = CurrentDate.AddDays(-7);
            var toWeekDay = CurrentDate.AddDays(7);
            var fromYear = fromWeekDay.Year;
            var toYear = toWeekDay.Year;

            var fromWeek = "Uke " + cal.GetWeekOfYear(fromWeekDay, dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString() + " " + fromYear.ToString();
            var toWeek = "Uke " + cal.GetWeekOfYear(toWeekDay, dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString() + " " + toYear.ToString();

            #endregion

            var errorList = new List<string>();

            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Employee);
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.TimesheetTabBR, false);
            try
            {
                SelectGunnar();
                UICommon.UIMapVS2017.ClickRecalculateInEmpTimesheetTab();

                UICommon.UIMapVS2017.SetRecalculatePeriodInTimesheetTab(fromWeek, toWeek);
                UICommon.UIMapVS2017.RecalculateInTimesheetTabRecalcWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 10: " + e.Message);
            }

            try
            {
                CheckValuesStep10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 10: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelInRecalculationWindow();
            UICommon.UIMapVS2017.CloseTimesheetTabRecalcWindow();

            return errorList;
        }
        public List<string> Step_11()
        {
            var errorList = new List<string>();

            SelectReport("Timeliste Variabel lønn", true);

            SetReport60Values2();
            ApproveGundersenStep11();
            ApproveLinesLevel1nStep11();

            try
            {
                OpenMenu();
                CheckFourRowsInListStep11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }
            ApproveLinesLevel1nStep11_1();

            ApproveGundersenLevel2nStep11();

            try
            {
                OpenMenu();
                CheckExtraInListStep11_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }
            ApproveGundersenLevel2nStep11_2();
            ClickStartTransfere();

            try
            {
                SortList();
                CheckExtraInListStep11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }

            ClickStartExport();
            try
            {
                CheckLinesTransferedStep11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in Step 11: " + e.Message);
            }
            ClickCloseExportLogWindow();

            return errorList;
        }
        public List<string> Step_12()
        {
            #region Variable Declarations
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            var fromWeekDay = CurrentDate.AddDays(-7);
            var toWeekDay = CurrentDate.AddDays(7);
            var fromYear = fromWeekDay.Year;
            var toYear = toWeekDay.Year;

            var fromWeek = "Uke " + cal.GetWeekOfYear(fromWeekDay, dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString() + " " + fromYear.ToString();
            var toWeek = "Uke " + cal.GetWeekOfYear(toWeekDay, dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString() + " " + toYear.ToString();

            #endregion

            var errorList = new List<string>();

            UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Employee);
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.TimesheetTabBR, false);
            try
            {
                SelectGunnar();
                UICommon.UIMapVS2017.ClickRecalculateInEmpTimesheetTab();

                UICommon.UIMapVS2017.SetRecalculatePeriodInTimesheetTab(fromWeek, toWeek);
                UICommon.UIMapVS2017.RecalculateInTimesheetTabRecalcWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 12: " + e.Message);
            }

            if (UICommon.UIMapVS2017.CheckNoNeedToRecalculateDialogExists())
            {
                TestContext.WriteLine("Recalculation not active(Step 12)");
                UICommon.UIMapVS2017.ClickOkInTimesheetTabNoNeedToRecalculateDialog();
            }
            else
            {
                errorList.Add("Recalculation is active(Step 12)");
                UICommon.UIMapVS2017.ClickCancelInRecalculationWindow();
            }

            UICommon.UIMapVS2017.CloseTimesheetTabRecalcWindow();
            CloseGat();

            return errorList;
        }
    }
}
