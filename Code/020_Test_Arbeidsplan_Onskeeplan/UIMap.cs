namespace _020_Test_Arbeidsplan_Onskeeplan
{
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using System;
    using System.Collections.Generic;
    using System.CodeDom.Compiler;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System.Drawing;
    using System.Windows.Input;
    using System.Text.RegularExpressions;
    using System.Globalization;
    using System.Diagnostics;
    using System.Threading;
    using System.IO;
    using CommonTestData;


    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        DateTime MondayCurrentWeek;

        public string ReportFilePath;
        public string ReportFileName = "020_excel";
        public string FileType = ".xls";
        private UIMapVS2017Classes.UIMapVS2017 map2017;
        #endregion
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
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nb-NO");
            MondayCurrentWeek = GetPreviousDayOfWeekDay(DateTime.Now, DayOfWeek.Monday);
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_020_Arbeidsplan\");

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
        public void DeleteReportFiles()
        {
            UICommon.UIMapVS2017.DeleteReportFiles(ReportFilePath);
        }

        public List<String> CompareReportDataFiles_Test020()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext);
            return errorList;
        }
        private DateTime GetNextDayOfWeekDay(DateTime from, DayOfWeek dayOfWeek)
        {
            return SupportFunctions.NextDayOfWeekDay(from, dayOfWeek);
        }
        private DateTime GetPreviousDayOfWeekDay(DateTime from, DayOfWeek dayOfWeek)
        {
            return SupportFunctions.PreviousDayOfWeekDay(from, dayOfWeek);
        }

        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepDiverse, null, "", logGatInfo);
        }

        public List<String> Step_1()
        {
            var errorList = new List<String>();

            SelectRosterplanTab();
            if (!CheckRosterPlanExists(""))
                TestContext.WriteLine("Arbeidsplanliste er tom: OK");
            else
                errorList.Add("Arbeidsplanliste er ikke tom!");
            try
            {
                CheckOnlyNewEnabled();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }
        public List<String> Step_2()
        {
            var errorList = new List<String>();

            ActivateFilters(false, false, false, false, true);

            if (CheckRosterPlanExists("UTA i rullerende plan"))
                TestContext.WriteLine("UTA i rullerende plan: OK");
            else
                errorList.Add("UTA i rullerende plan vises ikke!");
            try
            {
                CheckUTAplanData();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }
        public List<String> Step_4()
        {
            var errorList = new List<String>();

            ActivateFilters(true, false, false, true, false);

            if (CheckRosterPlanExists("UTA i rullerende plan"))
                errorList.Add("UTA i rullerende plan vises fortsatt!");

            if (CheckRosterPlanExists("1 år gammel"))
                TestContext.WriteLine("1 år gammel vises: OK");
            else
                errorList.Add("1 år gammel vises ikke!");

            errorList.AddRange(CheckActiveAndNewFilter());

            return errorList;
        }
        public List<String> Step_5()
        {
            var errorList = new List<String>();

            SelectRosterplan("1 år gammel", true, false);

            if (CheckRosterplanEmpty())
                TestContext.WriteLine("Arbeidsplanen er tom: OK");
            else
                errorList.Add("Arbeidsplanen er ikke tom!");

            return errorList;
        }
        public void Step_7()
        {
            AddShiftsRosterPlan();
            CheckPlanData_step_7();
            CheckPlanIsValid("_step_7");
        }
        public void Step_8()
        {
            SelectRosterplan("1 år gammel", true, false);
            UICommon.OpenRosterplanSettings();
            SetReadyForApproval();
        }

        public void Step_9()
        {
            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
            SetAproval();
            UICommon.CloseRosterplanFromPlanTab();
            CheckPlanIsValid("_step_9");
        }

        public void Step_10()
        {
            SelectRosterplan("1 år gammel", true, false);
            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
            SetAproval_step_10();
            UICommon.CloseRosterplanFromPlanTab();
            CheckPlanIsValid("_step_10");
        }

        public List<String> Step_11_12()
        {
            var errorList = new List<String>();

            SelectRosterplan("1 år gammel", true, false);
            UICommon.EffectuateFullRosterplan(true);
            EffectuationCheckStep_11();
            UICommon.EffectuateRosterplanLines(false);
            UICommon.CloseSalaryCalculationsWindow();
            UICommon.CloseRosterplanFromPlanTab();

            try
            {
                CheckPlanEffectutated_Step_12();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            CheckPlanIsValid("_step_12");

            return errorList;
        }

        public List<String> Step_13()
        {
            var errorList = new List<String>();

            CreateRosterPlan_step_13();

            return errorList;
        }

        public void Step_15()
        {
            SelectRosterplan("Kladd", false, false);
            ClickSettingsFromRosterplanTab();
            SetDraft();
            UICommon.ClickOkRosterplanSettings();

            CheckPlanIsValid("_step_15");
        }

        public void Step_16_17()
        {
            CreateRosterPlan_step_16();
        }
        public List<String> Step_19_20()
        {
            var errorList = new List<String>();
            try
            {
                StartGenerator();
                Playback.Wait(1000);
                ClickOkGenerator();
                Playback.Wait(1000);
                UICommon.ClickOKEditRosterPlanFromPlantab();
                UICommon.CloseRosterplanFromPlanTab();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                CheckBasePlanData();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                CheckFirstRosterPlanInListDates();
                CheckPlanIsValid("_step_20");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public List<String> Step_21()
        {
            var errorList = new List<String>();

            SelectRosterplan("Baseplan", false, false);
            ClickSettingsFromRosterplanTab();
            SetPlanSettingsToDate();
            UICommon.ClickOkRosterplanSettings();

            try
            {
                CheckBasePlanNewToDate();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public List<String> Step_22()
        {
            var errorList = new List<String>();

            SelectRosterplan("Kladd", true, false);
            UICommon.SelectRosterplanPlanTab();

            UICommon.ClickNewRosterPlanCopy();
            UICommon.SetStartDateNewRosterplan(MondayCurrentWeek);
            UnCheckDraft();
            ClickOkCopyRosterplanSettings();

            return errorList;
        }

        public List<String> Step_23()
        {
            var errorList = new List<String>();
            UICommon.CloseRosterplanFromPlanTab();
            SelectRosterplan("Kopi av Kladd.", true, false);
            UICommon.SelectRosterplanPlanTab();
            UICommon.OpenRosterplanSettings();
            SetPlanSettingsToDate();
            UICommon.ClickOkRosterplanSettings();

            return errorList;
        }

        public List<String> Step_24()
        {
            var errorList = new List<String>();

            UICommon.CloseRosterplanFromPlanTab();
            SelectRosterplan("Kopi av Kladd.", true, false);
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();

            AddShiftsToKladdKopi();

            UICommon.ClickOKEditRosterPlanFromPlantab();

            return errorList;
        }

        public List<String> Step_25()
        {
            var errorList = new List<String>();

            EffectuateToDate();

            UICommon.EffectuateFullRosterplan(false);

            var startDate = GetPreviousDayOfWeekDay(MondayCurrentWeek.AddMonths(3), DayOfWeek.Monday);
            var endDate = GetPreviousDayOfWeekDay(startDate.AddMonths(1), DayOfWeek.Sunday);
            UICommon.ChangeEffectuationPeriodForActualLines(startDate, endDate);

            UICommon.EffectuateRosterplanLines(false);
            UICommon.CloseSalaryCalculationsWindow();

            UICommon.CloseRosterplanFromPlanTab();

            try
            {
                CheckDraftCopyPlanData();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }
            try
            {
                CheckDraftCopyPlanDates();
                CheckPlanIsValid("_step_25");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public List<String> Step_26()
        {
            var errorList = new List<String>();
            ActivateFilters(false, false, true, false, false);

            if (CheckRosterPlanExists("Baseplan"))
                errorList.Add("Baseplan vises fortsatt!");

            if (CheckRosterPlanExists("Kopi av Kladd."))
                errorList.Add("Kopi av Kladd vises fortsatt!");

            if (CheckRosterPlanExists("1 år gammel"))
                errorList.Add("1 år gammel vises fortsatt!");

            if (CheckRosterPlanExists("Kladd"))
                TestContext.WriteLine("Kladd vises: OK");
            else
                errorList.Add("Kladd vises ikke!");

            return errorList;
        }

        public List<String> Step_27()
        {
            var errorList = new List<String>();
            ActivateFilters(true, false, false, true, false);

            if (CheckRosterPlanExists("Baseplan"))
                TestContext.WriteLine("Baseplan vises: OK");
            else
                errorList.Add("Baseplan vises ikke!");

            if (CheckRosterPlanExists("Kladd"))
                TestContext.WriteLine("Kladd vises: OK");
            else
                errorList.Add("Kladd vises ikke!");

            if (CheckRosterPlanExists("Kopi av Kladd."))
                TestContext.WriteLine("Kopi av Kladd vises: OK");
            else
                errorList.Add("Kopi av Kladd vises ikke!");

            if (CheckRosterPlanExists("1 år gammel"))
                TestContext.WriteLine("1 år gammel vises: OK");
            else
                errorList.Add("1 år gammel vises ikke!");

            return errorList;
        }

        public List<String> Step_28()
        {
            var errorList = new List<String>();
            SelectRosterplan("Baseplan", false, false);
            OpenRosterplanSettingsFromRosterplanTab();
            SetPlanInactive();
            UICommon.ClickOkRosterplanSettings();

            ActivateFilters(true, false, false, true, false);

            if (CheckRosterPlanExists("Baseplan"))
                errorList.Add("Baseplan vises fortsatt!");
            else
                TestContext.WriteLine("Baseplan vises ikke: OK");


            return errorList;
        }

        public List<String> Step_29()
        {
            var errorList = new List<String>();

            ActivateFilters(false, true, false, false, false);

            if (CheckRosterPlanExists("Baseplan"))
                TestContext.WriteLine("Baseplan vises: OK");
            else
                errorList.Add("Baseplan vises ikke!");

            if (CheckRosterPlanExists("Kladd"))
                errorList.Add("Kladd vises fortsatt!");

            if (CheckRosterPlanExists("Kopi av Kladd."))
                errorList.Add("Kopi av Kladd vises fortsatt!");

            if (CheckRosterPlanExists("1 år gammel"))
                errorList.Add("1 år gammel vises fortsatt!");


            return errorList;
        }

        public List<String> Step_30()
        {
            var errorList = new List<String>();

            ActivateFilters(true, true, false, true, false);

            SelectRosterplan("Baseplan", false, false);
            OpenRosterplanSettingsFromRosterplanTab();
            SetPlanActive();
            UICommon.ClickOkRosterplanSettings();

            return errorList;
        }

        public List<String> Step_31()
        {
            var errorList = new List<String>();

            SelectWishplansSubTab();

            try
            {
                CheckWishplanButtonState_step_31();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public List<String> Step_32()
        {
            var errorList = new List<String>();

            CreateNewWishplan_step_32();
            SetWishplanFromDate();
            ClickOkSaveWishplan();

            try
            {
                CheckWishplanData();
                CheckFirstWishPlanInListDates();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public List<String> Step_33_34()
        {
            var errorList = new List<String>();

            ConnectToBaseplan();
            OpenWishplanForBaseplan();

            try
            {
                UICommon.SelectRosterplanPlanTab();
                UIMapVS2017.CheckPlanIsOpened();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public List<String> Step_35_36()
        {
            var errorList = new List<String>();

            UICommon.CloseRosterplanFromPlanTab();
            OpenWishplanSettings();

            try
            {
                UICommon.ClickOkRosterplanSettings();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            SetWishplanToPhase1();
            UICommon.UIMapVS2017.ClickYesInChangeWishplanPhaseConfirmationWindow();

            try
            {
                CheckWishplanPhase();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public List<String> Step_37()
        {
            var errorList = new List<String>();

            SetWishplanToPhase3();
            UICommon.UIMapVS2017.ClickYesInChangeWishplanPhaseConfirmationWindow();

            try
            {
                CheckWishplanPhase3();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public List<String> Step_38()
        {
            var errorList = new List<String>();

            CreateNewWishplan_step_38();
            SetWishplan2Dates();
            ClickOkSaveWishplan();

            try
            {
                CheckWishplan2Data();
                CheckFirstWishPlan2Dates();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public List<String> Step_39()
        {
            var WishplanTable = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable;
            var errorList = new List<String>();
            
            try
            {
               DeletePeriod2Plan();

                var rows = WishplanTable.Views[0].RowCount;
                if (rows == 1)
                {
                    var firstRowName = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIPeriode1Cell;
                    Assert.AreEqual("Periode 1", firstRowName.ValueAsString, "Periode 2 is not deleted");

                    TestContext.WriteLine("Periode 2 slettet: Ok"); 
                }
                else
                    throw new Exception("Periode 2 is not deleted");
             }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }
        public List<String> Step_40_41()
        {
            var WishplanTable = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable;
            var errorList = new List<String>();

            try
            {
                DeletePeriod1Plan();
                DeletePeriod1();
                DeletePeriod2Plan();

                var rows = WishplanTable.Views[0].RowCount;

                if (rows == 0)
                {
                    TestContext.WriteLine("Periode 1 slettet: Ok");
                }
                else
                    throw new Exception("Periode 1 is not deleted");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        public void CheckFirstWishPlan2Dates()
        {
            #region Variable Declarations
            DXCell uIItem05022018Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIItem05022018Cell;
            DXCell uIItem18032018Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIItem18032018Cell;
            #endregion

            var startDate = GetPreviousDayOfWeekDay(MondayCurrentWeek.AddDays(42), DayOfWeek.Sunday);
            startDate = GetPreviousDayOfWeekDay(startDate.AddDays(1), DayOfWeek.Monday);
            var endDate = GetPreviousDayOfWeekDay(startDate.AddDays(42), DayOfWeek.Sunday);

            var phase1Start = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var phase2Start = MondayCurrentWeek.AddDays(7).ToString("yyyy-MM-dd");
            var phase3Start = MondayCurrentWeek.AddDays(14).ToString("yyyy-MM-dd");
            
            Assert.AreEqual(startDate.ToString("yyyy-MM-dd"), uIItem05022018Cell.ValueAsString);
            Assert.AreEqual(endDate.ToString("yyyy-MM-dd"), uIItem18032018Cell.ValueAsString);

            CheckPhaseDates(phase1Start, phase2Start, phase3Start);
        }
        
        private void CheckPhaseDates(string phase1Start, string phase2Start, string phase3Start)
        {
            #region Variable Declarations
            DXCell uIItemCell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIItemCell;
            DXCell uIItemCell1 = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIItemCell1;
            DXCell uIItemCell2 = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIItemCell2;
            #endregion
            
            Assert.AreEqual(phase1Start, uIItemCell.ValueAsString);
            Assert.AreEqual(phase2Start, uIItemCell1.ValueAsString);
            Assert.AreEqual(phase3Start, uIItemCell2.ValueAsString);
        }

        public void SelectRosterplanTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }
        public void SelectRosterplan(string planName, bool open, bool showAllPlans)
        {
            UICommon.SelectRosterPlan(planName, open, showAllPlans);
        }

        public bool CheckRosterPlanExists(string planName)
        {
            return UICommon.CheckRosterPlanExists(planName);
        }

        public void ActivateFilters(bool active, bool inActive, bool draft, bool newPlans, bool oldPlans)
        {
            ClickActive(active);
            ClickInactive(inActive);
            ClickDraft(draft);

            UICommon.ShowAllPlans(newPlans, oldPlans);
        }

        public List<String> CheckAllFiltersAndPlanData_step_3()
        {
            var errorList = new List<String>();

            errorList.AddRange(CheckAllFiltersActive());
            errorList.AddRange(CheckPlanData());

            return errorList;
        }

        private List<String> CheckAllFiltersActive()
        {
            var errorList = new List<String>();

            if (!ActiveState())
                errorList.Add("Aktivfilter er ikke aktivert");

            if (!InactiveState())
                errorList.Add("Inaktivfilter er ikke aktivert");

            if (!DraftState())
                errorList.Add("Kladdfilter er ikke aktivert");

            if (!UICommon.NewPlansState())
                errorList.Add("Nyefilter er ikke aktivert");

            if (!UICommon.OldPlansState())
                errorList.Add("Gamlefilter er ikke aktivert");

            return errorList;
        }
        public List<String> CheckActiveAndNewFilter()
        {
            var errorList = new List<String>();

            if (!ActiveState())
                errorList.Add("Aktivfilter er ikke aktivert");

            if (InactiveState())
                errorList.Add("Inaktivfilter er aktivert");

            if (DraftState())
                errorList.Add("Kladdfilter er aktivert");

            if (!UICommon.NewPlansState())
                errorList.Add("Nyefilter er ikke aktivert");

            if (UICommon.OldPlansState())
                errorList.Add("Gamlefilter er aktivert");

            return errorList;
        }

        private List<String> CheckPlanData()
        {
            var errorList = new List<String>();

            try
            {
                CheckOneYearOldPlanData();
                CheckDates();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                CheckUTAPlanData_step_3();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
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
        
        public List<string> CreateCalendarPlan_step_3()
        {
            var errorList = new List<string>();

            var prevMon = GetPreviousDayOfWeekDay(DateTime.Now.AddYears(-1), DayOfWeek.Monday);
            UICommon.ClickNewRosterplanButton();
            UICommon.SetStartDateRosterplan(prevMon);
            AddCalendarplanData();
            UICommon.ClickOkRosterplanSettings();
            UICommon.SelectRosterplanPlanTab();
            UICommon.CloseRosterplanFromPlanTab();

            return errorList;
        }

        public List<string> CreateRosterPlan_step_13()
        {
            var errorList = new List<string>();

            UICommon.ClickNewRosterplanButton();
            UICommon.SetStartDateRosterplan(MondayCurrentWeek);
            AddRosterplanData();
            UICommon.ClickOkRosterplanSettings();

            return errorList;
        }

        public List<string> CreateRosterPlan_step_16()
        {
            var errorList = new List<string>();

            UICommon.ClickNewRosterplanButton();
            UICommon.SetStartDateRosterplan(MondayCurrentWeek);
            AddBaseplanData();

            ConnectToStaffingplan();
            UICommon.ClickOkRosterplanSettings();

            return errorList;
        }

        public List<String> AddEmployeesToCalendarPlan()
        {
            var errorList = new List<string>();

            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            AddEmployeesOneTwoAndFourToPlan();

            return errorList;
        }
        public List<String> AddEmployeesToRosterPlan()
        {
            var errorList = new List<string>();

            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            AddEmployeesFiveAndSevenToPlan();
            Playback.Wait(3000);
            UICommon.CloseRosterplanFromPlanTab();
            try
            {
                CheckDraftPlanData();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            try
            {
                CheckFirstRosterPlanInListDates();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            CheckPlanIsValid("_step_14");

            return errorList;
        }

        public List<String> AddEmployeesToBasePlan()
        {
            var errorList = new List<string>();

            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            SelectEmployeesToBasePlan();

            return errorList;
        }
        public List<String> AddShiftsRosterPlan()
        {
            var errorList = new List<string>();

            AddShifts();
            UICommon.CloseRosterplanFromPlanTab();

            return errorList;
        }

        public void AddShifts()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIRedigerRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRibbonPageGroup9RibbonPageGroup.UIRedigerRibbonBaseButtonItem;
            DXCell uIItemCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            DXCell uIItemCell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell1;
            DXTextEdit uIRow0ColumnRosterCellEdit = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit;
            DXButton uIJAButton = this.UIGT4003InformasjonWindow.UIJAButton;
            DXCell uIItemCell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell2;
            DXCell uIItemCell3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell3;
            DXTextEdit uIRow0ColumnRosterCellEdit1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit1;
            DXCell uIItemCell4 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell4;
            DXCell uIItemCell5 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell5;
            DXTextEdit uIRow1ColumnRosterCellEdit = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit;
            DXCell uIItemCell6 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell6;
            DXCell uIItemCell7 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell7;
            DXTextEdit uIRow2ColumnRosterCellEdit = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow2ColumnRosterCellEdit;
            DXRibbonButtonItem uIBtnOkRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRibbonPageGroup9RibbonPageGroup.UIBtnOkRibbonBaseButtonItem;
            #endregion

            // Click 'Rediger' RibbonBaseButtonItem
            Mouse.Click(uIRedigerRibbonBaseButtonItem, new Point(31, 28));

            // Move cell to cell
            uIItemCell1.EnsureClickable(new Point(18, 10));
            Mouse.StartDragging(uIItemCell, new Point(14, 9));
            Mouse.StopDragging(uIItemCell1, new Point(18, 10));

            // Type 'a1' in '[Row]0[Column]RosterCell_0' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit, "a1{ENTER}", ModifierKeys.None);

            // Click '&Ja' button
            Mouse.Click(uIJAButton);

            // Move cell to cell
            uIItemCell3.EnsureClickable(new Point(19, 9));
            Mouse.StartDragging(uIItemCell2, new Point(41, 11));
            Mouse.StopDragging(uIItemCell3, new Point(19, 9));

            // Type 'a1' in '[Row]0[Column]RosterCell_5' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit1, "a1{ENTER}", ModifierKeys.None);

            // Click '&Ja' button
            Mouse.Click(uIJAButton);

            // Move cell to cell
            uIItemCell5.EnsureClickable(new Point(23, 11));
            Mouse.StartDragging(uIItemCell4, new Point(15, 8));
            Mouse.StopDragging(uIItemCell5, new Point(23, 11));

            // Type 'n1' in '[Row]1[Column]RosterCell_1' text box
            Keyboard.SendKeys(uIRow1ColumnRosterCellEdit, "n1{ENTER}", ModifierKeys.None);

            // Click '&Ja' button
            Mouse.Click(uIJAButton);

            // Move cell to cell
            uIItemCell7.EnsureClickable(new Point(19, 9));
            Mouse.StartDragging(uIItemCell6, new Point(30, 5));
            Mouse.StopDragging(uIItemCell7, new Point(19, 9));

            // Type 'd1' in '[Row]2[Column]RosterCell_0' text box
            Keyboard.SendKeys(uIRow2ColumnRosterCellEdit, "d1{ENTER}", ModifierKeys.None);

            // Click '&Ja' button
            Mouse.Click(uIJAButton);

            // Click 'btnOk' RibbonBaseButtonItem
            Mouse.Click(uIBtnOkRibbonBaseButtonItem, new Point(25, 9));
        }

        public void SetPlanSettingsToDate()
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEValidToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            var endDate = GetPreviousDayOfWeekDay(MondayCurrentWeek.AddMonths(6), DayOfWeek.Sunday);
            uIPceDateDateTimeEdit.ValueAsString = endDate.ToString("yyyy-MM-dd");
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{Tab}");
        }

        public void SetWishplanFromDate()
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIØnskeplanperiodeWindow.UIGrpPeriodClient.UIDeFromDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.ValueAsString = MondayCurrentWeek.ToString("yyyy-MM-dd");
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{Tab}");
        }

        public void SetWishplan2Dates()
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIØnskeplanperiodeWindow.UIGrpPeriodClient.UIDeFromDateCustom.UIPceDateDateTimeEdit;
            #endregion

            var startDate = GetPreviousDayOfWeekDay(MondayCurrentWeek.AddDays(42), DayOfWeek.Sunday);
            startDate = GetPreviousDayOfWeekDay(startDate.AddDays(1), DayOfWeek.Monday);

            var phase1Start = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var phase2Start = MondayCurrentWeek.AddDays(7).ToString("yyyy-MM-dd");
            var phase3Start = MondayCurrentWeek.AddDays(14).ToString("yyyy-MM-dd");

            uIPceDateDateTimeEdit.ValueAsString = startDate.ToString("yyyy-MM-dd");
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{Tab}");

            SetWishplan2PhaseDates(phase1Start, phase2Start, phase3Start);
        }
        private void SetWishplan2PhaseDates(string phase1Start, string phase2Start, string phase3Start)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIØnskeplanperiodeWindow.UIGrpPhaseControlClient.UIDePhase1StartCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIØnskeplanperiodeWindow.UIGrpPhaseControlClient.UIDePhase2StartCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit2 = this.UIØnskeplanperiodeWindow.UIGrpPhaseControlClient.UIDePhase3StartCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.ValueAsString = phase1Start;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{Tab}");

            uIPceDateDateTimeEdit1.ValueAsString = phase2Start;
            Keyboard.SendKeys(uIPceDateDateTimeEdit1, "{Tab}");

            uIPceDateDateTimeEdit2.ValueAsString = phase3Start;
            Keyboard.SendKeys(uIPceDateDateTimeEdit2, "{Tab}");
        }

        public bool CheckRosterplanEmpty()
        {
            #region Variable Declarations
            DXGrid uIGcRosterPlanTable = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            #endregion

            return uIGcRosterPlanTable.Views[0].RowCount < 1;
        }

        public void CheckPlanIsValid(string step)
        {
            #region Variable Declarations 
            var uIGcRosterPlanTable = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable;
            #endregion

            var view = uIGcRosterPlanTable.Views[0];
            var timeFormat = DateTime.Now.Date.ToString("ddMMyyyy") + "_" + DateTime.Now.TimeOfDay.ToString("hh\\mm");
            try
            {
                uIGcRosterPlanTable.CaptureImage().Save(ReportFilePath + "Rosterplan_" + step + "_" + timeFormat + ".jpg");
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Error capturing image: " + step + ": " + e.Message);
            }
        }

        public void CheckFirstRosterPlanInListDates()
        {
            #region Variable Declarations
            DXCell uIItem31102011Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem31102011Cell;
            DXCell uIItem09122012Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem09122012Cell;
            #endregion

            var endDate = GetPreviousDayOfWeekDay(MondayCurrentWeek.AddDays(42), DayOfWeek.Sunday);

            Assert.AreEqual(MondayCurrentWeek.ToString("yyyy-MM-dd"), uIItem31102011Cell.ValueAsString);
            Assert.AreEqual(endDate.ToString("yyyy-MM-dd"), uIItem09122012Cell.ValueAsString);
        }

        public void CheckFirstWishPlanInListDates()
        {
            #region Variable Declarations
            DXCell uIItem05022018Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIItem05022018Cell;
            DXCell uIItem18032018Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIItem18032018Cell;
            #endregion

            var endDate = GetPreviousDayOfWeekDay(MondayCurrentWeek.AddDays(42), DayOfWeek.Sunday);

            Assert.AreEqual(MondayCurrentWeek.ToString("yyyy-MM-dd"), uIItem05022018Cell.ValueAsString);
            Assert.AreEqual(endDate.ToString("yyyy-MM-dd"), uIItem18032018Cell.ValueAsString);
        }

        public void CheckBasePlanNewToDate()
        {
            #region Variable Declarations
            DXCell uIItem31102011Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem31102011Cell;
            DXCell uIItem09122012Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem09122012Cell;
            #endregion

            var endDate = GetPreviousDayOfWeekDay(MondayCurrentWeek.AddMonths(6), DayOfWeek.Sunday);
            Assert.AreEqual(endDate.ToString("yyyy-MM-dd"), uIItem09122012Cell.ValueAsString);
        }

        //public void CheckDraftPlanDates()
        //{
        //    #region Variable Declarations
        //    DXCell uIItem31102011Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem31102011Cell;
        //    DXCell uIItem09122012Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem09122012Cell;
        //    #endregion

        //    var endDate = GetPreviousDayOfWeekDay(MondayCurrentWeek.AddDays(42), DayOfWeek.Sunday); ;

        //    Assert.AreEqual(MondayCurrentWeek.ToString("yyyy-MM-dd"), uIItem31102011Cell.ValueAsString);
        //    Assert.AreEqual(endDate.ToString("yyyy-MM-dd"), uIItem09122012Cell.ValueAsString);
        //}

        private void CheckDates()
        {
            DXCell uIItem31102011Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem31102011Cell;
            DXCell uIItem09122012Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem09122012Cell;
            
            var prev = GetPreviousDayOfWeekDay(DateTime.Now.AddYears(-1), DayOfWeek.Monday);
            var next = GetNextDayOfWeekDay(prev.AddDays(77), DayOfWeek.Sunday);
            // Verify that the 'ValueAsString' property of '31.10.2011' cell equals '2017-01-30'
            Assert.AreEqual(prev.ToString("yyyy-MM-dd"), uIItem31102011Cell.ValueAsString);

            // Verify that the 'ValueAsString' property of '09.12.2012' cell equals '2017-04-23'
            Assert.AreEqual(next.ToString("yyyy-MM-dd"), uIItem09122012Cell.ValueAsString);
        }

        public void CheckDraftCopyPlanDates()
        {
            #region Variable Declarations
            DXCell uIItem05022018Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem05022018Cell;
            DXCell uIItem05082018Cell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable.UIItem05082018Cell;
            #endregion

            var endDate = GetPreviousDayOfWeekDay(MondayCurrentWeek.AddMonths(6), DayOfWeek.Sunday);

            Assert.AreEqual(MondayCurrentWeek.ToString("yyyy-MM-dd"), uIItem05022018Cell.ValueAsString);
            Assert.AreEqual(endDate.ToString("yyyy-MM-dd"), uIItem05082018Cell.ValueAsString);
        }

        /// <summary>
        /// ClickActive
        /// </summary>
        public void ClickActive(bool activate)
        {
            #region Variable Declarations
            DXRibbonButtonItem uIAktiveRibbonBaseButtonItem = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UIAktiveRibbonBaseButtonItem;
            #endregion

            if (uIAktiveRibbonBaseButtonItem.Checked != activate)
            {
                // Click 'Nye' RibbonBaseButtonItem
                Mouse.Click(uIAktiveRibbonBaseButtonItem);
            }
        }

        public bool ClickState()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIAktiveRibbonBaseButtonItem = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UIAktiveRibbonBaseButtonItem;
            #endregion

            return uIAktiveRibbonBaseButtonItem.Checked;
        }

        /// <summary>
        /// ClickDraft
        /// </summary>
        public void ClickDraft(bool activate)
        {
            #region Variable Declarations
            DXRibbonButtonItem uIKladdRibbonBaseButtonItem = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UIKladdRibbonBaseButtonItem;
            #endregion

            if (uIKladdRibbonBaseButtonItem.Checked != activate)
            {
                // Click 'Nye' RibbonBaseButtonItem
                Mouse.Click(uIKladdRibbonBaseButtonItem);
            }
        }

        /// <summary>
        /// ClickInactive
        /// </summary>
        private void ClickInactive(bool activate)
        {
            #region Variable Declarations
            DXRibbonButtonItem uIInaktiveRibbonBaseButtonItem = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UIInaktiveRibbonBaseButtonItem;
            #endregion

            if (uIInaktiveRibbonBaseButtonItem.Checked != activate)
            {
                // Click 'Nye' RibbonBaseButtonItem
                Mouse.Click(uIInaktiveRibbonBaseButtonItem);
            }
        }

        private bool ActiveState()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIAktiveRibbonBaseButtonItem = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UIAktiveRibbonBaseButtonItem;
            #endregion

            return uIAktiveRibbonBaseButtonItem.Checked;
        }

        private bool DraftState()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIKladdRibbonBaseButtonItem = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UIKladdRibbonBaseButtonItem;
            #endregion

            return uIKladdRibbonBaseButtonItem.Checked;
        }

        private bool InactiveState()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIInaktiveRibbonBaseButtonItem = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UIInaktiveRibbonBaseButtonItem;
            #endregion

            return uIInaktiveRibbonBaseButtonItem.Checked;
        }

        /// <summary>
        /// EffectuateToDate - Use 'EffectuateToDateParams' to pass parameters into this method.
        /// </summary>
        public void EffectuateToDate()
        {
            var endDate = GetPreviousDayOfWeekDay(MondayCurrentWeek.AddMonths(1), DayOfWeek.Sunday);
            UICommon.EffectuateFullRosterplan(false);
            UICommon.ChangeEffectuationPeriodForActualLines(MondayCurrentWeek, endDate);

            UICommon.EffectuateRosterplanLines(false);
            UICommon.CloseSalaryCalculationsWindow();
        }
    }
}
