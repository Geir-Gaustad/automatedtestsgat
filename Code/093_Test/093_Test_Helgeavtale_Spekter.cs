using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.Diagnostics;


namespace _093_Test_Helgeavtale_Spekter
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_093_Test_Helgeavtale_Spekter
    {
        //[TestMethod]
        //public void TestData_093()
        //{
        //    /*             
        //    // Select '2013 (31.12.2012 - 29.12.2013)'
        //    // Select '2014 (30.12.2013 - 28.12.2014)'
        //    // Select '2015 (29.12.2014 - 03.01.2016)'
        //     */

        //    ////uIAnsattClient--
        //    ////Timeliste
        //    ////Hack: DelphiTabkomponent
        //    //UIMap.Select_EmployeeSubTab(UIMap.EmployeeTabs.Timesheet);
        //    //Playback.Wait(2000);

        //    ////Helgeavtale
        //    ////Hack: DelphiTabkomponent
        //    //UIMap.Select_EmployeeSubTab(UIMap.EmployeeTabs.WeekendAgreement);
        //    //Playback.Wait(2000);

        //    ////Stillingsforhold_linje_2
        //    ////Hack: DelphiTabkomponent
        //    //UIMap.Select_EmployeeSubTab(UIMap.EmployeeTabs.EmploymentSecondLine);
        //    //Playback.Wait(2000);

        //    ////Stillingsforhold_line_1
        //    ////Hack: DelphiTabkomponent
        //    //UIMap.Select_EmployeeSubTab(UIMap.EmployeeTabs.Employment);

        //    //UIMap.StartGat(true);

        //    ////Ansattfane  
        //    //UIMap.UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Employee);
        //    ////Rad 1

        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.EmploymentTab);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.EffectuationPeriodsTab);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.ShiftsTab);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.AbsenceTab);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.TimesheetTab);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.EkstraEmployeeTab);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.CalloutsTab);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.EmployeeBanksTab);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.AmlDispTab);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.AmlBrakesTab);
        //    //Playback.Wait(1000);

        //    ////Rad 2
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.DocTab, false);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.DayAndWeekSeparationEmployeeTab, true);
        //    //Playback.Wait(1000);
        //    //UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.WeekendAgreementTab, true);

        //    ////AvdelingsFane
        //    //UIMap.UICommon.SelectMainWindowTab(CommonTestData.SupportFunctions.MainWindowTabs.Department);
        //    ////Rad 1
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Liste, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Stillingsforhold, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Stillingsutlån, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Vaktkoder, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Vaktkodemønster, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Kolonner, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Kompetansebehov, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Grupper, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Oppgave, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Oppgavemønster, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Merknader, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Gjøremål, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.GjøremålsEkskludering, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.BrukereMedTilgang, false);
        //    //Playback.Wait(1000);

        //    ////Rad 2

        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Dok, false);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Fleksitid, true);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Bemanningsplan, true);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Døgnrytmeplan, true);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Timeregistrering, true);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Bemanningsbudsjett, true);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Gjennomsnittsberegning, true);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Aktivitetsplanlegging, true);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Ukeplanperioder, true);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Oppgavedeling, true);
        //    //Playback.Wait(1000);
        //    //UIMap.UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Integrasjon, true);


        //    //var errorList = new List<string>();
        //    //Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
        //    //Playback.PlaybackSettings.DelayBetweenActions = 800;

        //    //foreach (var error in errorList)
        //    //{
        //    //    Debug.WriteLine(error);
        //    //}
        //}

        [TestMethod, Timeout(6000000)]
        public void Test93_Chapter1()
        {
            //Playback.PlaybackSettings.MaximumRetryCount = 5;
            //Playback.PlaybackSettings.ShouldSearchFailFast = false;
            //Playback.PlaybackSettings.SearchTimeout = 15000;

            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;

            UIMap.RestoreDatabase();
            var timeStart = DateTime.Now;

            try
            {
                UIMap.DeleteReportFiles();
                UIMap.StartGat(true);

                UIMap.CreateSpekterPeriods();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1: " + e.Message);
            }

            //Step 1
            // Select '2013 (31.12.2012 - 29.12.2013)'
            // Select '2014 (30.12.2013 - 28.12.2014)'
            // Select '2015 (29.12.2014 - 03.01.2016)'
            errorList.AddRange(SaveReportPeriod("_chapter1_step_1", "2015 (29.12.2014 - 03.01.2016)", false, true));

            //Step 2
            errorList.AddRange(SaveReportPeriod("_chapter1_step_2", "", true));

            //Step 5
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.Click_Extra();
                UIMap.Select_ExtraCause();
                UIMap.Select_ExtraEmployee();//6532168513
                UIMap.Select_ExtraDate(new DateTime(2015, 5, 31));
                UIMap.Select_ExtraShiftCode("D (07:00) - (15:00)");
                UIMap.Click_Ok_Extra();

            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 5): " + e.Message);
            }

            //CheckReportValues("2015 (29.12.2014 - 03.01.2016)", true, false, true);
            //UIMap.ClosePreview();

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesStep5());
            UIMap.ClosePreview();

            //Step 6
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 5, 31));// "31.05.2015");

                UIMap.OpenEdit_Extra("D", "Extra");
                UIMap.EditExtraShift(UIMap.EditExtraShiftParams.UIETime0700, UIMap.EditExtraShiftParams.UIETime1700, "Dag");
                UIMap.Click_Ok_Extra();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 6): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesStep6());
            UIMap.ClosePreview();

            //Step 7
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.DeleteShift("D", "10, Helg");

                UIMap.Click_Extra();
                UIMap.Select_ExtraCause();
                UIMap.Select_ExtraEmployee();//6532168513
                UIMap.Select_ExtraDate(new DateTime(2015, 6, 1));
                UIMap.Select_ExtraShiftCode("D (07:00) - (15:00)");
                UIMap.Click_Ok_Extra();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 7): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesStep7());
            UIMap.ClosePreview();

            //Step 8
            try
            {
                //TODO: sjekke dette mot step 14 i kap4 og se om timene kommer med
                UIMap.Click_EmployeeTab();
                //UIMap.Select_TimeSheetTab();
                //Timeliste
                //Hack: DelphiTabkomponent
                UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.TimesheetTab);
                UIMap.SelectEmp10AndEditKontNew();
                UIMap.Click_NewKontLine();
                UIMap.AddNewKont();
                UIMap.Close_EditNewAccount();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 8): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesStep8());
            UIMap.ClosePreview();

            //Step 9
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 6, 1));
                UIMap.DeleteShift("D", "10, Helg");

                UIMap.Click_Extra();
                UIMap.Select_ExtraCause();
                UIMap.Select_ExtraEmployee();//6532168513
                UIMap.Select_ExtraDate(new DateTime(2015, 6, 5));
                UIMap.Select_ExtraShiftCode("N (23:00) - (07:00)");
                UIMap.Click_Ok_Extra();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 9): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesStep9());
            UIMap.ClosePreview();

            //Step 10
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 6, 5));
                UIMap.OpenEdit_Extra("N", "Extra");
                UIMap.Select_ExtraDate(new DateTime(2015, 6, 7));
                UIMap.EditExtraShift(UIMap.EditExtraShiftParams.UIETime2300, UIMap.EditExtraShiftParams.UIETime0700, "Natt");
                UIMap.Click_Ok_Extra();
                Playback.Wait(2000);
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 10): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesStep10());
            UIMap.ClosePreview();
            try
            {
                Playback.Wait(2000);
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 6, 7));

                UIMap.OpenEdit_Extra("N", "Extra");
                UIMap.Select_ExtraShiftCode("A (15:00) - (23:00)");
                UIMap.Click_Ok_Extra();

                Playback.Wait(2000);
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 10_2): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesStep10_2());
            UIMap.ClosePreview();

            //Step 11
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 6, 7));

                Playback.Wait(2000);
                UIMap.OpenEdit_Extra("A", "Absence");
                UIMap.Select_AbcenceCode("10");
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 11): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesStep11());
            UIMap.ClosePreview();

            //Step 12
            try
            {
                Playback.Wait(2000);
                UIMap.Click_ShiftBook();
                UIMap.OpenEdit_Extra("Abs", "Absence");
                UIMap.Select_AbcenceCode("41");
                UIMap.OkClose_Absence();

                OpenReport96();
                errorList.AddRange(UIMap.CheckReportValuesStep12());
                UIMap.ClosePreview();

                //Slett fravær og ekstraavtale
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 6, 7));
                UIMap.DeleteShift("Abs", "10, Helg");
                Playback.Wait(2000);

                try
                {
                    if (UIMap.UICommon.UIMapVS2017.CheckRecalculationWindowExists())
                    {
                        errorList.Add("Recalculation active in step 12");
                        UIMap.UICommon.UIMapVS2017.ClickOkInRecalculationWindow();
                    }
                }
                catch (Exception)
                {
                    TestContext.WriteLine("Recalculation not active(Step 12)");
                }

                UIMap.DeleteShift("A", "10, Helg");

            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 12): " + e.Message);
            }

            //step 13
            try
            {
                Playback.Wait(2000);
                UIMap.Click_EmployeeTab();
                UIMap.Select_EmpNo15();
                //Helgeavtale
                //Hack: DelphiTabkomponent
                UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.WeekendAgreementTab);

                UIMap.Click_NewAgreement();
                UIMap.Create_Agreement();
                UIMap.Click_OkAgreement();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 13): " + e.Message);
            }


            //step 14
            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesStep14());
            UIMap.ClosePreview();

            //step 15
            try
            {
                UIMap.OpenPlan("Grunnlag Helgerapportering");
                UIMap.Click_Filter();
                UIMap.Select_ShiftDistribution();

                UIMap.EffectuatePlan();
                //29.12.2014 - 22.03.2015
                UIMap.ChangeRosterIntervall("29.12.2014", "22.03.2015");
                UIMap.Select_Employees();
                errorList.AddRange(UIMap.EffectuateLines());
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 15): " + e.Message);
            }


            //step 16
            OpenReport96("");
            errorList.AddRange(UIMap.CheckReportValuesStep16());
            UIMap.ClosePreview();

            try
            {
                Playback.Wait(2000);
                UIMap.OpenPlan("Grunnlag Helgerapportering");

                UIMap.DeleteEffectuationForAllEmps();
                UIMap.EffectuatePlan();
                errorList.AddRange(UIMap.EffectuateLines());
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 17): " + e.Message);
            }
            
            errorList.AddRange(SaveReportPeriod("_chapter1_step_17"));

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 93, kap. 1"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter1 finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void Test93_Chapter2()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;
            
            var timeStart = DateTime.Now;

            //Step 1.
            try
            {
                UIMap.OpenPlan("Grunnlag Helgerapportering");
                UIMap.Click_PlanTab();

                UIMap.ClickNewRosterPlanCopy();
                UIMap.SetRosterStartAndWeeks("31.12.2012", "4");
                UIMap.Uncheck_Kladd();
                UIMap.ClearEmployees();
                UIMap.SelectEmployees_step2_1();

                UIMap.Click_OkNewRosterPlan();
                UIMap.DeleteEffectuation();
                UIMap.Select_Employees16To20();

                var timeDeleteEffetuation = DateTime.Now;
                UIMap.Click_DeleteEffectuationFourLines();
                UIMap.CloseRosterPlan();
                var timeAfterDelete = DateTime.Now;
                errorList.AddRange(UIMap.TimeLapseInSeconds(timeDeleteEffetuation, timeAfterDelete, "Tidsforbruk ved sletting av iverksetting og lukking av plan"));

            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 1): " + e.Message);
            }

            //Step 2
            try
            {
                UIMap.OpenPlan("Kopi av Grunnlag Helgerapportering.");
                UIMap.Click_PlanTab();

                UIMap.Click_PlanSettings();
                UIMap.SetPlanToDate();
                UIMap.Click_OkPlanSettings();
                UIMap.CloseRosterPlan();
                UIMap.OpenPlan("Kopi av Grunnlag Helgerapportering.");
                UIMap.Click_EditRosterPlan();
                UIMap.Clear_RosterPlan();

                UIMap.Click_Sat();
                UIMap.InsertDK6();
                UIMap.InsertDK7();

                UIMap.Click_Ok_EditCalendarPlan();

                UIMap.EffectuatePlan();
                UIMap.ChangeRosterIntervall("31.12.2012", "27.01.2013");
                UIMap.EffectuateLines();
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 2): " + e.Message);
            }

            errorList.AddRange(SaveReportPeriod("_chapter2_step_2", "2013 (31.12.2012 - 29.12.2013)"));

            //Step 3
            try
            {
                UIMap.OpenPlan("Kopi av Grunnlag Helgerapportering.");
                UIMap.DeleteEffectuationForAllEmps();

                UIMap.Click_EditRosterPlan();
                UIMap.Clear_RosterPlan();

                UIMap.Click_Sat();
                UIMap.InsertNSunEmp16();
                UIMap.InsertNSatEmp17();
                UIMap.InsertN1SunEmp18();
                UIMap.InsertNMonEmp20();
                UIMap.Click_Ok_EditCalendarPlan();

                UIMap.EffectuatePlan();
                UIMap.ChangeRosterIntervall("31.12.2012", "27.01.2013");
                UIMap.EffectuateLines();
                UIMap.CloseRosterPlan();

            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 3): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC2Step3());
            UIMap.ClosePreview();

            //Step 4
            try
            {
                UIMap.OpenPlan("Kopi av Grunnlag Helgerapportering.");
                UIMap.DeleteEffectuationForAllEmps();

                UIMap.Click_EditRosterPlan();
                UIMap.Clear_RosterPlan();
                UIMap.Click_Ok_EditCalendarPlan();
                UIMap.CloseRosterPlan();

                UIMap.OpenPlan("Grunnlag Helgerapportering");
                UIMap.Click_PlanTab();

                UIMap.ClickNewRosterPlanCopy();
                UIMap.RenamePlan("Kopi_2 av Grunnlag Helgerapportering. [SelectionStart]6");
                UIMap.SetRosterStartAndWeeks("29.12.2014", "3");
                UIMap.Uncheck_Kladd();
                UIMap.ClearEmployees();
                UIMap.SelectEmployees_step2_1();

                UIMap.Click_OkNewRosterPlan();
                UIMap.CloseRosterPlan();

                UIMap.OpenPlan("Kopi_2 av Grunnlag Helgerapportering.");
                UIMap.Click_PlanTab();

                UIMap.Click_PlanSettings();
                UIMap.SetPlanToDate();
                UIMap.Click_OkPlanSettings();
                UIMap.CloseRosterPlan();
                UIMap.OpenPlan("Kopi_2 av Grunnlag Helgerapportering.");

                UIMap.Click_EditRosterPlan();
                UIMap.Clear_RosterPlan();

                UIMap.Click_Fri();
                UIMap.InsertHJNFriEmp16();
                UIMap.InsertHJN1FriEmp17();
                UIMap.InsertVPVFriEmp18();
                UIMap.InsertVPV2FriEmp20();
                UIMap.Click_Ok_EditCalendarPlan();

                UIMap.EffectuatePlan();
                UIMap.EffectuateLines();
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 4): " + e.Message);
            }


            OpenReport96("2015 (29.12.2014 - 03.01.2016)");
            errorList.AddRange(UIMap.CheckReportValuesC2Step4());
            UIMap.ClosePreview();

            //Step 5
            try
            {
                UIMap.OpenPlan("Kopi_2 av Grunnlag Helgerapportering.");
                UIMap.DeleteEffectuationForAllEmps();

                UIMap.Click_EditRosterPlan();
                UIMap.Clear_RosterPlan();

                UIMap.Click_Fri();
                UIMap.InsertHJNSatEmp16();
                UIMap.InsertHJN1FriEmp17();
                UIMap.InsertVPVFriEmp18();
                UIMap.InsertVPV2FriEmp20();
                UIMap.Click_Ok_EditCalendarPlan();

                UIMap.EffectuatePlan();
                UIMap.EffectuateLines();
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 5): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC2Step5());
            UIMap.ClosePreview();

            //Step 6
            try
            {
                UIMap.OpenPlan("Kopi_2 av Grunnlag Helgerapportering.");
                UIMap.DeleteEffectuationForAllEmps();

                UIMap.Click_EditRosterPlan();
                UIMap.Clear_RosterPlan();

                UIMap.Click_Fri();
                UIMap.InsertHJNSunEmp16();
                UIMap.InsertHJN1FriEmp17();
                UIMap.InsertVPVFriEmp18();
                UIMap.InsertVPV2FriEmp20();
                UIMap.Click_Ok_EditCalendarPlan();

                UIMap.EffectuatePlan();
                UIMap.EffectuateLines();
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 6): " + e.Message);
            }

            OpenReport96("2015 (29.12.2014 - 03.01.2016)");
            errorList.AddRange(UIMap.CheckReportValuesC2Step6());
            UIMap.ClosePreview();

            //Step 7 
            try
            {
                UIMap.OpenPlan("Kopi_2 av Grunnlag Helgerapportering.");
                UIMap.DeleteEffectuationForAllEmps();

                UIMap.Click_EditRosterPlan();
                UIMap.Clear_RosterPlan();
                UIMap.Click_Ok_EditCalendarPlan();
                UIMap.CloseRosterPlan();

                UIMap.Click_EmployeeTab();
                UIMap.Select_Emp16Helg();
                //Stillingsforhold linje 2
                //Hack: DelphiTabkomponent
                UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.EmploymentTab, true);
                UIMap.Click_Edit_EmpPosition();
                UIMap.Set_EmpPercent(UIMap.Set_EmpPercent_Params.UISePositionPercent_0);

                UIMap.Ok_Edit_EmpPosition();

                UIMap.Select_Emp18Helg();
                UIMap.Click_Edit_EmpPosition();
                UIMap.Set_EmpPercent(UIMap.Set_EmpPercent_Params.UISePositionPercent_0);
                UIMap.Ok_Edit_EmpPosition();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 7): " + e.Message);
            }

            //Step 8
            try
            {
                UIMap.OpenPlan("Kopi_2 av Grunnlag Helgerapportering.");
                UIMap.Click_PlanTab();
                UIMap.Click_RosterEmployees();
                UIMap.Set_RosterEmployeePercentTo_0();
                UIMap.Click_RosterEmployeesOk();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 8): " + e.Message);
            }

            //Step 9
            try
            {
                UIMap.SetStartDate_EasterPeriod();
                UIMap.RightClickEmp16HelgLines();
                UIMap.Click_LineSettings("{DOWN 2}");
                UIMap.Select_LineCauseCode(UIMap.Select_CauseCodeVacancyNurseParams.UIVakans);
                var button = UIMap.Get_OkLineSettingsPoint();
                UIMap.Click_OkLineSettings(button);

                UIMap.RightClickEmp18HelgLines();
                UIMap.Click_LineSettings("{DOWN 2}");
                UIMap.Select_LineCauseCode(UIMap.Select_CauseCodeVacancyNurseParams.UISykevikar);
                UIMap.Click_OkLineSettings(button);

                UIMap.Click_EditRosterPlan();
                Playback.Wait(1500);
                UIMap.Click_WedCell();
                UIMap.InsertDWed_Week1();
                UIMap.Click_WedCell_Week2();
                UIMap.InsertDWed_Week2();
                UIMap.Click_Ok_EditCalendarPlan();

                UIMap.Click_PlanTab();
                UIMap.EffectuatePlan();
                UIMap.EffectuateLines();
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 9): " + e.Message);
            }

            OpenReport96("2015 (29.12.2014 - 03.01.2016)");
            errorList.AddRange(UIMap.CheckReportValuesC2Step9());
            UIMap.ClosePreview();

            //Step 10
            try
            {
                UIMap.Click_EmployeeTab();
                UIMap.Select_Emp16Helg();
                UIMap.Click_Edit_EmpPosition();
                UIMap.Set_EmpPercent(UIMap.Set_EmpPercent_Params.UISePositionPercent_75);
                UIMap.Ok_Edit_EmpPosition();

                UIMap.Select_Emp18Helg();
                UIMap.Click_Edit_EmpPosition();
                UIMap.Set_EmpPercent(UIMap.Set_EmpPercent_Params.UISePositionPercent_25);
                UIMap.Ok_Edit_EmpPosition();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 10): " + e.Message);
            }

            //Step 11
            try
            {
                UIMap.OpenPlan("Kopi_2 av Grunnlag Helgerapportering.");
                UIMap.DeleteEffectuationForAllEmps();
                UIMap.CloseRosterPlan();

                UIMap.OpenPlan("Grunnlag Helgerapportering");
                UIMap.EffectuatePlan();
                errorList.AddRange(UIMap.EffectuateLines());
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 11): " + e.Message);
            }

            errorList.AddRange(SaveReportPeriod("_chapter2_step_11"));
            
            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 93, kap. 2"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter2 finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void Test93_Chapter3()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;

            var timeStart = DateTime.Now;

            try
            {
                //Step 1
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 4));
                UIMap.Absence_Helg14();
                UIMap.Select_AbcenceCode("10");
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 1): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step1());
            UIMap.ClosePreview();

            //Step 2
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.OpenEdit_Absence("Abs", "14, Helg");
                UIMap.Select_AbcenceCode("41");
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 2): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step2());
            UIMap.ClosePreview();

            //Step 3
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.OpenEdit_Absence("Abs", "14, Helg");
                //Endre til å ta med søndagsvakt
                UIMap.ChangeAbsPeriod();
                UIMap.OkClose_Absence();

            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 3): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step3());
            UIMap.ClosePreview();

            //Step 4
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.OpenEdit_Absence("Abs", "14, Helg");
                UIMap.Select_AbcenceCode("10");
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 4): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step4());
            UIMap.ClosePreview();

            //Step 5
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.DeleteShift("Abs", "14, Helg");

                UIMap.Absence_Helg14();
                UIMap.Select_AbcenceCode("20");
                UIMap.ChangeAbsPeriod();
                //Tilpass begge vaktene slik at summen blir mer enn 7 timer
                UIMap.Set_AbsTo50Percent();
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 5): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step5());
            UIMap.ClosePreview();

            //Step 6
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.DeleteShift("Abs", "14, Helg", "04.07.2015 - 05.07.2015");

                UIMap.Absence_Helg14();
                UIMap.Select_AbcenceCode("91B");
                UIMap.ChangeAbsPeriod();
                //Tilpass begge vaktene slik at summen blir mer enn 7 timer
                UIMap.Set_AbsTo50Percent();
                UIMap.AdjustAbsenceShifts(UIMap.AdjustShiftSunParams.UIETime1EditValue1000, UIMap.AdjustShiftSatParams.UIETime1EditValue2000);
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 6): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step6());
            UIMap.ClosePreview();

            //Step 7
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.OpenEdit_Absence("Abs", "14, Helg");
                //Tilpass begge vaktene slik at summen blir mindre enn 7 timer
                UIMap.AdjustAbsenceShifts(UIMap.AdjustShiftSunParams.UIETime1EditValue1200, UIMap.AdjustShiftSatParams.UIETime1EditValue1730);
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 7): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step7());
            UIMap.ClosePreview();

            //Step 8
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.DeleteShift("Abs", "14, Helg", "04.07.2015 - 05.07.2015");

                UIMap.Absence_Helg14();
                UIMap.Select_AbcenceCode("20");
                UIMap.ChangeAbsPeriod();
                UIMap.Set_AbsTo50Percent();
                //Ny ekstravakt på 8timer
                UIMap.Click_NewShift();
                UIMap.ContructNewShift();
                Keyboard.SendKeys("{Tab 3}{Enter}");
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 8): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step8());
            UIMap.ClosePreview();

            //Step 9
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.OpenEdit_Absence("Abs", "14, Helg");
                UIMap.Select_AbcenceCode("91B");
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 9): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step9());
            UIMap.ClosePreview();

            //Step 10
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.DeleteShift("Abs", "14, Helg", "04.07.2015 - 05.07.2015");

                UIMap.Absence_Helg14();
                UIMap.Select_AbcenceCode("30");
                //UIMap.SetCompensatoryTimeOff("3"); 
                UIMap.RegHourlyAbsenceWithTimeOff("15:00", "18:00");
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 10): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step10());
            UIMap.ClosePreview();

            //Step 11
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.OpenAbsence();
                UIMap.Select_AbcenceCode("30");

                UIMap.Select_EmployeeAbsence("Etternavn:14");
                UIMap.ChangeAbsPeriod(UIMap.ChangeAbsTodateParams.UIPceDate05072015, UIMap.ChangeAbsTodateParams.UIPceDate05072015);
                //UIMap.SetCompensatoryTimeOff("3");
                UIMap.RegHourlyAbsenceWithTimeOff("07:00", "10:00");
                UIMap.OkClose_Absence();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 11): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step11());
            UIMap.ClosePreview();

            //Step 12
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.OpenEdit_Absence("Abs", "14, Helg", "04.07.2015  A Kl: 15.00-18.00");
                UIMap.RegHourlyAbsenceWithTimeOff("15:00", "20:00");
                //UIMap.SetCompensatoryTimeOff("5");

                UIMap.OkClose_Absence();

                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 5));
                UIMap.OpenEdit_Absence("Abs", "14, Helg", "05.07.2015  D Kl: 07.00-10.00");
                //UIMap.SetCompensatoryTimeOff("5");
                UIMap.RegHourlyAbsenceWithTimeOff("07:00", "12:00");
                UIMap.OkClose_Absence();

            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 12): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC3Step12());
            UIMap.ClosePreview();

            //Step 13
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 4));
                UIMap.DeleteShift("Abs", "14, Helg");
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 5));
                UIMap.DeleteShift("Abs", "14, Helg");
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 13): " + e.Message);
            }

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 93, kap. 3"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter3 finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void Test93_Chapter4()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;

            var timeStart = DateTime.Now;

            //Step 1("04.07.2015")
            try
            {
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 4));
                UIMap.ExchangeEmp("A", "14, Helg");
                UIMap.Select_EmpExchangeTo("80000000");
                UIMap.SelectShiftToExchange();
                UIMap.Click_Ok_Exchange();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 1): " + e.Message);
            }

            OpenReport96("2015 (29.12.2014 - 03.01.2016)");
            errorList.AddRange(UIMap.CheckReportValuesC4Step1());
            UIMap.ClosePreview();

            //Step 2("05.07.2015")
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 5));

                UIMap.ExchangeEmp("D", "14, Helg");
                UIMap.Select_EmpExchangeTo("80000000");
                UIMap.SelectShiftToExchange();
                UIMap.Click_Ok_Exchange();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 2): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step2());
            UIMap.ClosePreview();

            //Step 3("05.07.2015")
            try
            {
                UIMap.Click_ShiftBook();

                UIMap.ExchangeEmp("D", "11, Helg");
                UIMap.Select_EmpExchangeTo("80000000");
                UIMap.SelectShiftToExchange();
                UIMap.SelectShiftT2oExchange();
                UIMap.Click_Ok_Exchange();

                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 4));

                UIMap.ExchangeEmp("A", "11, Helg");
                UIMap.Select_EmpExchangeTo("80000000");
                UIMap.SelectShiftToExchange();
                UIMap.SelectShiftT2oExchange();
                UIMap.Click_Ok_Exchange();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 3): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step3());
            UIMap.ClosePreview();

            //Step 4("04.07.2015")
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 4));
                UIMap.DeleteShifts("11, Helg", "A", false, "b");
                UIMap.DeleteShifts("20, Helg", "A", false, "B");
                UIMap.DeleteShifts("20, Helg", "A", false, "B");

                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 5));
                UIMap.DeleteShifts("11, Helg", "D", false, "b");
                UIMap.DeleteShifts("20, Helg", "D", false, "B");
                UIMap.DeleteShifts("20, Helg", "D", false, "B");
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 4): " + e.Message);
            }

            //Step 5("04.07.2015")
            try
            {
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 4));
                UIMap.ExchangeEmp("A", "14, Helg", false, true);
                UIMap.Select_VacantOnSaturday();
                UIMap.Select_VacantFreeOnSunday();
                UIMap.Click_Ok_DepartmentExchange();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 5): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step5());
            UIMap.ClosePreview();

            //Step 6
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.ExchangeEmp("", "14", true, true);
                UIMap.Select_DepartmentExchangeEmp("80000000");
                UIMap.Select_VacantShiftsToExchange();
                UIMap.Click_Ok_VacantShifts();
                UIMap.Click_Ok_DepartmentExchange();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 6): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step6());
            UIMap.ClosePreview();

            //Step 7(Slett alle bytter)
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.DeleteShifts("20, Helg", "A", true, "BA");
                UIMap.DeleteShifts("14, Helg", "V", true, "ba");
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 7): " + e.Message);
            }


            //Step 8
            try
            {
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 1));
                UIMap.Select_RemanageEmp("N", "20, Helg");
                UIMap.Select_RemanageCause();
                UIMap.Construct_NewShiftForRemanage(new DateTime(2015, 7, 4));
                UIMap.Click_Ok_Remanage();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 8): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step8());
            UIMap.ClosePreview();

            //Step 9
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 2));
                UIMap.Select_RemanageEmp("N", "20, Helg");
                UIMap.Select_RemanageCause();
                UIMap.Construct_NewShiftForRemanage(new DateTime(2015, 7, 5));
                UIMap.Click_Ok_Remanage();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 9): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step9());
            UIMap.ClosePreview();

            //Step 10(Slett forskyvninger)
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 4));
                UIMap.DeleteShifts("20, Helg", "D");
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 5));
                UIMap.DeleteShifts("20, Helg", "D");

                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 4));
                UIMap.Select_RemanageEmp("D", "18, Helg");
                UIMap.Select_RemanageCause();
                UIMap.Construct_NewShiftForRemanage(new DateTime(2015, 7, 6));
                UIMap.Click_Ok_Remanage();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 10): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step10());
            UIMap.ClosePreview();

            //Step 11 (forskyv søndagsvakt)
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 5));
                UIMap.Select_RemanageEmp("D", "18, Helg");
                UIMap.Select_RemanageCause();
                UIMap.Construct_NewShiftForRemanage(new DateTime(2015, 7, 7));
                UIMap.Click_Ok_Remanage();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 11): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step11());
            UIMap.ClosePreview();

            //Step 12(Slett forskyvninger)
            try
            {
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 6));
                UIMap.DeleteShifts("18, Helg", "D");
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 7));
                UIMap.DeleteShifts("18, Helg", "D");
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 12): " + e.Message);
            }


            //Step 13(Registrer overtid)
            try
            {
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 11));
                UIMap.Select_RosterbookEmployee("V", "17, Helg");
                UIMap.Click_Extra();
                UIMap.Select_ExtraCause();
                UIMap.Select_ExtraDate(new DateTime(2015, 7, 11));
                UIMap.Select_ExtraShiftCode("D (07:00) - (15:00)");
                UIMap.Click_Ok_Extra();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 13): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step13());
            UIMap.ClosePreview();

            //Step 14(Endre kontering)
            try
            {
                UIMap.Click_EmployeeTab();
                UIMap.Select_Emp17Helg();
                //Timeliste
                //Hack: DelphiTabkomponent
                UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.TimesheetTab);

                //UIMap.Select_TimeSheetTab();
                UIMap.Select_110715_Overtime_KontLine();
                UIMap.EditKontForPreSelectedEmployee();
                UIMap.Click_NewKontLine();

                UIMap.AddNewKont();
                UIMap.Close_EditNewAccount();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 14): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step14());
            UIMap.ClosePreview();

            //Step 15
            try
            {
                UIMap.Click_EmployeeTab();
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 11));
                UIMap.DeleteShifts("17, Helg", "D");
                UIMap.Select_RosterbookEmployee("V", "11, Helg");
                UIMap.Click_Extra();
                UIMap.Select_ExtraCause();
                UIMap.Select_ExtraDate(new DateTime(2015, 7, 11));
                UIMap.Select_ExtraShiftCode("D (07:00) - (15:00)");
                UIMap.Click_Ok_Extra();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 15): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step15());
            UIMap.ClosePreview();

            //Step 16       
            try
            {
                UIMap.Click_EmployeeTab();
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 11));
                UIMap.DeleteShifts("11, Helg", "D");

                UIMap.Select_RosterbookEmployee("D", "14, Helg");
                UIMap.Click_Extra();
                UIMap.Select_ExtraCause();
                UIMap.Select_ExtraDate(new DateTime(2015, 7, 11));
                UIMap.SetExtraFromToTimeAndColumn("15:00", "20:00", UIMap.SetExtraToTimeAndColumnParams.UICbCrewColumnAften);
                UIMap.Click_Ok_Extra();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 16): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step16());
            UIMap.ClosePreview();

            //Step 17
            try
            {
                UIMap.Click_EmployeeTab();
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 11));
                UIMap.DeleteShifts("14, Helg", "A");

                UIMap.Select_RosterbookEmployee("D", "14, Helg");
                UIMap.Click_Extra();
                UIMap.Select_ExtraCause();
                UIMap.Select_ExtraDate(new DateTime(2015, 7, 11));
                UIMap.SetExtraFromToTimeAndColumn("03:00", "07:00", UIMap.SetExtraToTimeAndColumnParams.UICbCrewColumnNight);
                UIMap.Click_Ok_Extra();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 17): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC4Step17());
            UIMap.ClosePreview();

            //Step 18
            try
            {
                UIMap.Click_EmployeeTab();
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 11));
                UIMap.DeleteShifts("14, Helg", "N");

                UIMap.Click_AdministrationTab();
                
                UIMap.SelectFromAdministration("VAKTKATEGORIER");
                UIMap.AdminVaktkategorierSetUtrykningHjemmevakt(true);

                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 12));
                UIMap.Select_RosterbookEmployee("A", "14, Helg");

                UIMap.Click_Utrykning();
                UIMap.Select_UtrykningsCause();
                UIMap.Set_UtrykningsPeriod();
                UIMap.Click_Ok_Utrykning();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 18): " + e.Message);
            }
            errorList.AddRange(SaveReportPeriod("_chapter4_step_18"));

            //Step 19
            try
            {
                UIMap.Click_EmployeeTab();
                UIMap.Click_ShiftBook();
                UIMap.GoToShiftBookDate(new DateTime(2015, 7, 12));
                UIMap.DeleteShifts("14, Helg", "A", false, "u");

                UIMap.Click_AdministrationTab();

                UIMap.SelectFromAdministration("VAKTKATEGORIER");
                UIMap.AdminVaktkategorierSetUtrykningHjemmevakt(false);
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 19): " + e.Message);
            }

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 93, kap. 4"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter4 finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void Test93_Chapter5()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;

            var timeStart = DateTime.Now;

            UIMap.CloseGat();
            UIMap.StartGat(false);

            //Step_1
            try
            {
                UIMap.Click_RosterplanTab();
                UIMap.Click_NewRosterPlanButton();
                UIMap.NameRosterplan3();
                UIMap.Select_TypeOfPlan();

                UIMap.SetPlanStartdateAndWeeks();
                UIMap.Click_Ok_NewRosterplan();

                UIMap.Select_All_EmployeesAndAddToPlan();
                UIMap.CloseRosterPlan();
                UIMap.OpenPlan("Chapter_5_step_3");

                UIMap.Click_EditRosterPlan();
                UIMap.Clear_RosterPlan();
                UIMap.Click_Ok_EditCalendarPlan();
                UIMap.CloseRosterPlan();

                UIMap.OpenPlan("Grunnlag Helgerapportering");
                UIMap.DeleteEffectuationForAllEmps();

                UIMap.Select_RosterCell();
                Playback.Wait(1000);
                Keyboard.SendKeys("c", ModifierKeys.Control);
                Playback.Wait(1000);
                UIMap.CloseRosterPlan();

                UIMap.Click_NewRosterPlanButton();
                UIMap.NameRosterplan();
                UIMap.Select_TypeOfPlan();

                UIMap.SetPlanStartdateAndWeeks();
                UIMap.Click_Ok_NewRosterplan();

                UIMap.Select_All_EmployeesAndAddToPlan();
                UIMap.Click_HomeTab_CalendarPlan();
                UIMap.Click_EditCalendarPlan();
                UIMap.SelectFirstCell_CalendarPlan();
                Playback.Wait(1000);
                Keyboard.SendKeys("v", ModifierKeys.Control);
                Playback.Wait(500);
                UIMap.Click_Ok_ChangeShiftsCalendarPlan();
                Playback.Wait(500);
                UIMap.SelectLastCell_CalendarPlan("{TAB 20}", false);
                Playback.Wait(1000);
                Keyboard.SendKeys("v", ModifierKeys.Control);
                Playback.Wait(500);
                UIMap.Click_Ok_ChangeShiftsCalendarPlan();
                UIMap.Click_Ok_EditCalendarPlan();
                Clipboard.Clear();

                UIMap.Effectuate_CalendarPlan(true);
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 1): " + e.Message);
            }

            errorList.AddRange(SaveReportPeriod("_chapter5_step_1", "2015 (29.12.2014 - 03.01.2016)", true, true));

            //Step_2
            try
            {
                UIMap.OpenPlan("Chapter_5_step_1");
                UIMap.Click_EditRosterPlanFromHomeTab();
                UIMap.SelectLastCell_CalendarPlan("{TAB 6}");
                UIMap.SetEasterShifts_CalendarPlan();
                UIMap.Click_Ok_EditCalendarPlan();
                UIMap.Effectuate_CalendarPlan();
                UIMap.CloseRosterPlan();

            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 2): " + e.Message);
            }

            errorList.AddRange(SaveReportPeriod("_chapter5_step_2"));

            //Step_3
            try
            {
                UIMap.OpenPlan("Chapter_5_step_1");
                UIMap.Click_PlanTab();
                var timeDeleteEffectuationCalenderplan = DateTime.Now;
                UIMap.DeleteEffectuation_CalendarPlan();
                UIMap.DeleteEffectuation_CalendarPlan();
                UIMap.CloseRosterPlan();
                var timeDeletedEffectuationCalenderplan = DateTime.Now;
                errorList.AddRange(UIMap.TimeLapseInSeconds(timeDeleteEffectuationCalenderplan, timeDeletedEffectuationCalenderplan, "Tidsforbruk ved sletting av iverksetting, kalenderplan"));

                UIMap.OpenPlan("Chapter_5_step_3");

                UIMap.Click_EditRosterPlan();
                UIMap.SelectLastCell_CalendarPlan("{TAB 7}");
                UIMap.SetEasterShiftsDK_CalendarPlan("DK6", "{TAB 6}");
                UIMap.Click_Ok_EditCalendarPlan();
                UIMap.Effectuate_CalendarPlan();
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 3): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC5Step3());
            UIMap.ClosePreview();

            //Step_4
            try
            {
                UIMap.OpenPlan("Chapter_5_step_3");
                UIMap.Click_PlanTab();
                var timeDeleteEffectuationCalenderplan2 = DateTime.Now;
                UIMap.DeleteEffectuation_CalendarPlan();
                UIMap.Click_EditCalendarPlanFromPlanTab();
                var timeDeletedEffectuationCalenderplan2 = DateTime.Now;
                errorList.AddRange(UIMap.TimeLapseInSeconds(timeDeleteEffectuationCalenderplan2, timeDeletedEffectuationCalenderplan2, "Tidsforbruk ved sletting av iverksetting, kalenderplan"));

                UIMap.SelectLastCell_CalendarPlan("{TAB 7}");
                UIMap.SetEasterShiftsDK_CalendarPlan("DK7", "{TAB 6}");

                UIMap.Click_Ok_EditCalendarPlan();
                UIMap.Effectuate_CalendarPlan();
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 4): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC5Step4());
            UIMap.ClosePreview();

            //Step_5
            try
            {
                UIMap.OpenPlan("Chapter_5_step_3");
                UIMap.Click_PlanTab();
                var timeDeleteEffectuationCalenderplan3 = DateTime.Now;
                UIMap.DeleteEffectuation_CalendarPlan();
                UIMap.Click_EditCalendarPlanFromPlanTab();
                var timeDeletedEffectuationCalenderplan3 = DateTime.Now;
                errorList.AddRange(UIMap.TimeLapseInSeconds(timeDeleteEffectuationCalenderplan3, timeDeletedEffectuationCalenderplan3, "Tidsforbruk ved sletting av iverksetting, kalenderplan"));

                UIMap.SelectLastCell_CalendarPlan("{TAB 6}");
                UIMap.SetEasterShiftsDK_CalendarPlan("DK6", "");
                Keyboard.SendKeys("{TAB 4}", ModifierKeys.Shift);
                UIMap.SetEasterShiftsDK_CalendarPlan("DK6", "");

                UIMap.Click_Ok_EditCalendarPlan();
                UIMap.Effectuate_CalendarPlan();
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 5): " + e.Message);
            }

            OpenReport96();
            errorList.AddRange(UIMap.CheckReportValuesC5Step5());
            UIMap.ClosePreview();

            //Step_6
            try
            {
                UIMap.Click_EmployeeTab();
                UIMap.Select_Emp16Helg();
                //Stillingsforhold linje 1
                //Hack: DelphiTabkomponent
                UIMap.Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs.EmploymentTab);
                UIMap.Click_Edit_EmpPosition();
                UIMap.Set_EmpPercent(UIMap.Set_EmpPercent_Params.UISePositionPercent_0);

                UIMap.Ok_Edit_EmpPosition();

                UIMap.Select_Emp18Helg();
                UIMap.Click_Edit_EmpPosition();
                UIMap.Set_EmpPercent(UIMap.Set_EmpPercent_Params.UISePositionPercent_0);
                UIMap.Ok_Edit_EmpPosition();

            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 6): " + e.Message);
            }

            errorList.AddRange(SaveReportPeriod("_chapter5_step_6"));       

            //Step_7
            try
            {
                UIMap.OpenPlan("Chapter_5_step_3");
                UIMap.Click_PlanTab();
                var timeDeleteEffectuationCalenderplan4 = DateTime.Now;
                UIMap.DeleteEffectuation_CalendarPlan();

                UIMap.CloseRosterPlan();
                var timeDeletedEffectuationCalenderplan4 = DateTime.Now;
                errorList.AddRange(UIMap.TimeLapseInSeconds(timeDeleteEffectuationCalenderplan4, timeDeletedEffectuationCalenderplan4, "Tidsforbruk ved sletting av iverksetting, kalenderplan(lukker planen)"));

            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 7): " + e.Message);
            }

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 93, kap. 5"));

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter5 finished OK");
                return;
            }
            UIMap.AssertResults(errorList);
        }

        [TestMethod, Timeout(6000000)]
        public void Test93_Chapter6()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;

            var timeStart = DateTime.Now;

            // Step 1
            try
            {
                UIMap.Click_EmployeeTab();
                UIMap.RegisterNewEmployee();
                UIMap.RegisterNewEmployee_Position();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 6(Step 1): " + e.Message);
            }

            //Step 2
            try
            {
                //Hack: DelphiTabkomponent
                UIMap.Select_DepShiftcodesTabNew();
                UIMap.Create_NewCode_N2();

                UIMap.Click_RosterplanTab();
                UIMap.Click_NewRosterPlanButton();

                //Legg til Ansatt NN i planen
                UIMap.CreateNewPlanFor_NN();
                UIMap.Click_EditRosterPlan();
                UIMap.Click_RosterCell();
                UIMap.InsertN2_for_NN();
                UIMap.Click_Ok_EditCalendarPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 6(Step 2): " + e.Message);
            }

            //Step 3
            try
            {
                UIMap.Click_PlanTab();
                var timeEffectuatuateNNplan = DateTime.Now;
                UIMap.EffectuateWholePlanForNN();

                var timeEffectuatedNNplan = DateTime.Now;
                errorList.AddRange(UIMap.TimeLapseInSeconds(timeEffectuatuateNNplan, timeEffectuatedNNplan, "Tidsforbruk ved iverksetting av arbedsplan(" + UIMap.CreateNewPlanFor_NNParams.UIENameEditValueAsString + ")"));
                UIMap.CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 6(Step 3): " + e.Message);
            }

            //Step 4
            OpenReport96("2013 (31.12.2012 - 29.12.2013)", true, true);
            errorList.AddRange(UIMap.CheckReportValuesC6Step4());
            UIMap.ClosePreview();

            //Step 5
            OpenReport96("2014 (30.12.2013 - 28.12.2014)");
            errorList.AddRange(UIMap.CheckReportValuesC6Step5());
            UIMap.ClosePreview();

            //Step 6
            OpenReport96("2015 (29.12.2014 - 03.01.2016)");
            errorList.AddRange(UIMap.CheckReportValuesC6Step6());
            UIMap.ClosePreview();
            
            UIMap.KillGatProcess();

            var timeEnd = DateTime.Now;
            errorList.AddRange(UIMap.TimeLapseInSeconds(timeStart, timeEnd, "Tidsforbruk ved kjøring av Test 93, kap. 6"));
            errorList.AddRange(UIMap.CompareReportDataFiles_Test093());

            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Chapter6 finished OK");
                return;
            }

            UIMap.AssertResults(errorList);
        }

        private List<string> OpenReport96(string period = "", bool CheckDetaljPrWeek = true, bool selectReport = false)
        {
            var errorList = new List<string>();

            try
            {

                Playback.Wait(2000);
                UIMap.Click_Reports();

                if (selectReport)
                    UIMap.SelectReport96_F3();

                if (period != "")
                    UIMap.SelectSpekterPeriod(period);

                    UIMap.CheckDetaljPrUke(CheckDetaljPrWeek);

                Playback.Wait(1000);
                UIMap.Click_Preview();
                                                         }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }
        
        private List<string> SaveReportPeriod(string step, string period = "", bool CheckDetaljPrWeek = true, bool selectReport = false)
        {
            var errorList = new List<string>();

            try
            {

                Playback.Wait(2000);
                UIMap.Click_Reports();

                if (selectReport)
                    UIMap.SelectReport96_F3();

                if (period != "")
                 UIMap.SelectSpekterPeriod(period);

                UIMap.CheckDetaljPrUke(CheckDetaljPrWeek);

                Playback.Wait(1000);
                UIMap.Click_Preview();

                errorList.AddRange(UIMap.ExportToExcell(step));
                Playback.Wait(1000);

                UIMap.ClosePreview();

            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

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
                if ((map == null))
                {
                    map = new UIMap(TestContext);
                }

                return map;
            }
        }

        private UIMap map;

    }
}
