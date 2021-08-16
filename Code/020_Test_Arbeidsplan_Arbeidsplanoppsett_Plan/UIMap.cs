namespace _020_Test_Arbeidsplan_Arbeidsplanoppsett_Plan
{
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
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
    using System.Threading;
    using System.Globalization;
    using System.IO;
    using CommonTestData;
    using System.Diagnostics;

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
            UICommon.LoginGatAndSelectDepartment(UICommon.DepDiverse, null, "", logGatInfo);
        }

        public void SelectFromAdministration(string searchString)
        {
            UICommon.SelectFromAdministration(searchString, true);
        }
        public void SelectRosterplan(string planName, bool open, bool showAllPlans)
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            UICommon.SelectRosterPlan(planName, open, showAllPlans);
        }

        public void CloseRosterplan()
        {
            try
            {
                UICommon.CloseRosterplanFromPlanTab();
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Error closing rosterplan: " + e.Message);
            }
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

        public void InsertSettingValuesStep2()
        {
            SelectDepSettingsFromAdministration("arbeidsplanoppsett +avdeling", true);
            UICommon.UIMapVS2017.SelectADM_AdministrasjonInPlanSettingsWindow();
            InsertSingleSettingsValue("Maks planlagt tid per dag", "8");
            InsertSingleSettingsValue("Maks planlagt tid per uke", "38");
            InsertSingleSettingsValue("Maks antall lørdager på rad", "1");
            InsertSingleSettingsValue("Maks antall vakter på rad", "5");

            UICommon.UIMapVS2017.SaveDepRosterplanSettings();

        }
        public void InsertSettingValuesStep3()
        {
            UICommon.UIMapVS2017.SelectL38InPlanSettingsWindow();
            CheckDepSettingsCheckbox("Krav til F1 hver uke", false);
            InsertSingleSettingsValue("Maks antall søndager på rad", "3");
            InsertSingleSettingsValue("Maks planlagt tid per dag", "20");
            InsertSingleSettingsValue("Maks planlagt tid per uke", "60");

            UICommon.UIMapVS2017.SaveDepRosterplanSettings();

        }
        public void InsertSettingValuesStep4()
        {
            UICommon.UIMapVS2017.SelectTurnusInPlanSettingsWindow();
            InsertWeekSettingValues("Minste daglige arbeidsfri mellom Aften- og Dag", "8", "8", "8", "8", "8", "8", "11");
            InsertWeekSettingValues("Minste daglige arbeidsfri mellom Dag- og Natt", "8", "8", "8", "8", "8", "8", "11");
            InsertWeekSettingValues("Minste daglige arbeidsfri mellom Natt- og Aften", "8", "8", "8", "8", "8", "8", "11");

            UICommon.UIMapVS2017.SaveAndCloseDepRosterplanSettings();
            //UICommon.ClearAdministrationSearchString();
        }

        public void Step8()
        {
            UICommon.UIMapVS2017.CloseDepRosterplanSettings();
            //UICommon.ClearAdministrationSearchString();
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }

        public void Step9_CreateRosterplan()
        {
            UICommon.ClickNewRosterplanButton();
            UICommon.UIMapVS2017.SetRosterPlanName("Oppsett 1");
            UICommon.SetStartDateRosterplan(new DateTime(2023, 01, 02));
            UICommon.UIMapVS2017.SetRosterPlanWeeks("3");
            UICommon.ClickOkRosterplanSettings();
        }

        public List<string> Step10_AddEmployees()
        {
            var errorList = new List<string>();
            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            SelectEmployeesStep10();
            UICommon.ClickOkAddEmployeesWindow();
            try
            {
                CheckEmployeeDataStep10();
            }
            catch (Exception e)
            {
                errorList.Add("Step_10: " + e.Message);
            }

            UICommon.ClickOkEmployeesWindow();

            return errorList;
        }

        public List<string> Step15_AddShifts()
        {
            var errorList = new List<string>();
            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                AddShiftsStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Step_15: " + e.Message);
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();

            return errorList;
        }

        private void AddShiftsStep15()
        {
            #region Variable Declarations
            DXCell uIItemCell5 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell5;
            DXTextEdit uIRow0ColumnRosterCellEdit5 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit5;
            DXTextEdit uIRow0ColumnRosterCellEdit6 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit6;
            DXTextEdit uIRow0ColumnRosterCellEdit7 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit7;
            DXCell uIItemCell24 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell24;
            DXCell uIItemCell110 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell110;
            #endregion

            AddShiftsStep15_1();

            // Click cell
            Mouse.Click(uIItemCell5, new Point(5, 15));
            // Type 'n' in '[Row]0[Column]RosterCell_14' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit5, "n{TAB}");
            Mouse.Click(uIItemCell24, new Point(14, 11));
            // Type 'd' in '[Row]0[Column]RosterCell_17' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit6, "d{TAB}");
            Mouse.Click(uIItemCell110, new Point(11, 11));
            // Type 'n' in '[Row]0[Column]RosterCell_18' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit7, "n{TAB}");

            AddShiftsStep15_2();
        }

        public List<string> Step28_AddShifts()
        {
            var errorList = new List<string>();
            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                AddShiftsStep28();
            }
            catch (Exception e)
            {
                errorList.Add("Step_28: " + e.Message);
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();

            return errorList;
        }

        public List<string> Step19()
        {
            var errorList = new List<string>();
            UICommon.ClickRosterplanLayout();

            try
            {
                CheckDepSettingsCheckbox("Krav til F1 hver uke", false);
                InsertSingleSettingsValue("Maks antall søndager på rad", "2");
                InsertWeekSettingValues("Minste daglige arbeidsfri mellom Aften- og Dag", "8", "8", "8", "8", "8", "8", "8");
                InsertWeekSettingValues("Minste daglige arbeidsfri mellom Dag- og Natt", "8", "8", "8", "8", "8", "8", "8");
                InsertWeekSettingValues("Minste daglige arbeidsfri mellom Natt- og Aften", "8", "8", "8", "8", "8", "8", "8");
            }
            catch (Exception e)
            {
                errorList.Add("Step_19: " + e.Message);
            }

            UICommon.UIMapVS2017.SaveDepRosterplanSettings();

            return errorList;
        }
        public List<string> Step20()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ResizeSettingsWindow();
            SelectRosterPlanLayout_2();
            UICommon.UIMapVS2017.ResizeSettingsWindow();

            try
            {
                InsertSingleSettingsValue("Maks planlagt tid per dag", "12");
                InsertSingleSettingsValue("Maks planlagt tid per uke", "48");
                InsertWeekSettingValues("Minste daglige arbeidsfri mellom Aften- og Dag", "8", "11", "11", "11", "11", "11", "11");
                InsertWeekSettingValues("Minste daglige arbeidsfri mellom Dag- og Natt", "11", "8", "11", "11", "11", "11", "11");
                InsertSingleSettingsValue("Ukentlig arbeidsfri", "40");
                InsertSingleSettingsValue("Maks antall lørdager på rad", "2");
                InsertSingleSettingsValue("Maks antall vakter på rad", "8");
            }
            catch (Exception e)
            {
                errorList.Add("Step_20: " + e.Message);
            }

            UICommon.UIMapVS2017.SaveAndCloseDepRosterplanSettings();

            return errorList;
        }
        public void InsertSettingValuesStep24()
        {
            CheckDepSettingsCheckbox("Krav til F1 hver uke", true);
            InsertSingleSettingsValue("Maks antall søndager på rad", "2");
            InsertSingleSettingsValue("Maks planlagt tid per dag", "12");
            InsertSingleSettingsValue("Maks planlagt tid per uke", "48");
            InsertDayShiftSettingValue("Maksimum antall vakter pr. vaktkategori", "1");

            UICommon.UIMapVS2017.OkClickEmpLayoutWindow();
        }
        public void EditGlobalSettingValuesStep25()
        {
            InsertSingleSettingsValue("Maks antall Aften-vakter", "1", true);
            InsertSingleSettingsValue("Maks antall Dag-vakter", "2", true);
            InsertSingleSettingsValue("Maks antall Natt-vakter", "3", true);
            UICommon.UIMapVS2017.SaveAndCloseGlobalRosterplanSettings();
            //UICommon.ClearAdministrationSearchString();
        }
        private void SelectDepSettingsFromAdministration(string search, bool selectAdminTab = false)
        {
            if (selectAdminTab)
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Administration);

            UICommon.SelectFromAdministration(search);
        }
        public void CheckDepSettingsCheckbox(string filter, bool check)
        {
            #region Variable Declarations
            DXCheckBox uIChkEditCheckBox = this.UIArbeidsplanoppsettfoWindow.UINbcRosterSetupNavBar.UINavBarGroupControlCoScrollableControl.UIPnlEditClient.UIChkEditCheckBox;
            uIChkEditCheckBox.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uIChkEditCheckBox.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            uIChkEditCheckBox.SearchProperties.Remove(DXTestControl.PropertyNames.HierarchyLevel);
            uIChkEditCheckBox.WindowTitles.Remove("Arbeidsplanoppsett for avdeling 5110 - Diverse");

            uIChkEditCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "chkEdit";
            uIChkEditCheckBox.SearchProperties[DXTestControl.PropertyNames.ClassName] = "GSCheckEdit";
            #endregion

            UICommon.UIMapVS2017.SearchInPlanSettingsWindow(filter);
            UICommon.UIMapVS2017.OpenSearchResultLineInPlanSettingsWindow();

            // Select 'chkEdit' check box
            uIChkEditCheckBox.Checked = check;
        }
        private void InsertSingleSettingsValue(string filter, string value, bool lockValue = false)
        {
            #region Variable Declarations
            DXTextEdit uIENumberEdit = this.UIArbeidsplanoppsettfoWindow.UINbcRosterSetupNavBar.UINavBarGroupControlCoScrollableControl.UIPnlEditClient.UIENumberEdit;
            uIENumberEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uIENumberEdit.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            uIENumberEdit.SearchProperties.Remove(DXTestControl.PropertyNames.HierarchyLevel);
            uIENumberEdit.WindowTitles.Remove("Arbeidsplanoppsett for avdeling 5110 - Diverse");

            uIENumberEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eNumber";
            uIENumberEdit.SearchProperties[DXTestControl.PropertyNames.ClassName] = "GSTextEdit";
            #endregion

            UICommon.UIMapVS2017.SearchInPlanSettingsWindow(filter);
            UICommon.UIMapVS2017.OpenSearchResultLineInPlanSettingsWindow();

            //ValueAsString
            uIENumberEdit.ValueAsString = value;

            if (lockValue)
                LockLayoutValue();

            // Type '{Tab}' in 'eNumber' text box
            Keyboard.SendKeys(uIENumberEdit, this.SetMaxPlannedHoursPrWeekTo35Params.UIENumberEditSendKeys, ModifierKeys.None);
        }
        private void LockLayoutValue()
        {
            #region Variable Declarations
            var panel = this.UIGlobaltarbeidsplanopWindow.UINbcRosterSetupNavBar.UINavBarGroupControlCoScrollableControl.UIPnlEditClient;
            panel.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            panel.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            panel.SearchProperties.Remove(DXTestControl.PropertyNames.HierarchyLevel);
            panel.WindowTitles.Remove("Globalt arbeidsplanoppsett");
            panel.SearchProperties[DXTestControl.PropertyNames.Name] = "pnlEdit";
            panel.SearchProperties[DXTestControl.PropertyNames.ClassName] = "GSPanelControl";

            var lockWin = panel.UILbLockWindow;
            lockWin.SearchProperties.Remove(WinWindow.PropertyNames.ControlName);
            lockWin.WindowTitles.Remove("Globalt arbeidsplanoppsett");
            lockWin.SearchProperties[WinWindow.PropertyNames.ControlName] = "lbLock";
            var lockPoint = new Point(lockWin.BoundingRectangle.X + 5, lockWin.BoundingRectangle.Y + 5);
            #endregion

            Mouse.Move(lockPoint);
            Mouse.Click(lockPoint);
        }
        private void InsertDayShiftSettingValue(string filter, string value1)
        {
            #region Variable Declarations
            DXTextEdit uIENumberEdit = this.UIArbeidsplanoppsettfoWindow.UINbcRosterSetupNavBar.UINavBarGroupControlCoScrollableControl.UIPnlEditClient.UIENumberEdit;
            uIENumberEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uIENumberEdit.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            uIENumberEdit.SearchProperties.Remove(DXTestControl.PropertyNames.HierarchyLevel);
            uIENumberEdit.WindowTitles.Remove("Arbeidsplanoppsett for avdeling 5110 - Diverse");

            uIENumberEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eNumber[2]";
            uIENumberEdit.SearchProperties[DXTestControl.PropertyNames.ClassName] = "GSTextEdit";
            #endregion

            UICommon.UIMapVS2017.SearchInPlanSettingsWindow(filter);
            UICommon.UIMapVS2017.OpenSearchResultLineInPlanSettingsWindow();

            //ValueAsString
            uIENumberEdit.ValueAsString = value1;
            Keyboard.SendKeys(uIENumberEdit, this.SetMaxPlannedHoursPrWeekTo35Params.UIENumberEditSendKeys, ModifierKeys.None);
        }
        private void InsertWeekSettingValues(string filter, string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            #region Variable Declarations
            DXTextEdit uIENumberEdit = this.UIArbeidsplanoppsettfoWindow.UINbcRosterSetupNavBar.UINavBarGroupControlCoScrollableControl.UIPnlEditClient.UIENumberEdit;
            uIENumberEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uIENumberEdit.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            uIENumberEdit.SearchProperties.Remove(DXTestControl.PropertyNames.HierarchyLevel);
            uIENumberEdit.WindowTitles.Remove("Arbeidsplanoppsett for avdeling 5110 - Diverse");

            uIENumberEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eNumber[6]";
            uIENumberEdit.SearchProperties[DXTestControl.PropertyNames.ClassName] = "GSTextEdit";
            #endregion

            UICommon.UIMapVS2017.SearchInPlanSettingsWindow(filter);
            UICommon.UIMapVS2017.OpenSearchResultLineInPlanSettingsWindow();

            //ValueAsString
            uIENumberEdit.ValueAsString = value1;
            Keyboard.SendKeys(uIENumberEdit, this.SetMaxPlannedHoursPrWeekTo35Params.UIENumberEditSendKeys, ModifierKeys.None);
            Keyboard.SendKeys(value2 + this.SetMaxPlannedHoursPrWeekTo35Params.UIENumberEditSendKeys);
            Keyboard.SendKeys(value3 + this.SetMaxPlannedHoursPrWeekTo35Params.UIENumberEditSendKeys);
            Keyboard.SendKeys(value4 + this.SetMaxPlannedHoursPrWeekTo35Params.UIENumberEditSendKeys);
            Keyboard.SendKeys(value5 + this.SetMaxPlannedHoursPrWeekTo35Params.UIENumberEditSendKeys);
            Keyboard.SendKeys(value6 + this.SetMaxPlannedHoursPrWeekTo35Params.UIENumberEditSendKeys);
            Keyboard.SendKeys(value7 + this.SetMaxPlannedHoursPrWeekTo35Params.UIENumberEditSendKeys);
        }
        public void Step26_AddEmployees()
        {
            SelectRosterplan("Oppsett 1", true, false);

            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            SelectEmployeesStep26();
            UICommon.ClickOkAddEmployeesWindow();
            UICommon.ClickOkEmployeesWindow();
        }

        #region Checking Values

        public List<string> Step11_CheckSetupWindow()
        {
            var errorList = new List<string>();
            UICommon.ClickRosterplanLayout();
            UICommon.UIMapVS2017.ResizeSettingsWindow();
            UICommon.UIMapVS2017.OpenDropdownInPlanSettingsWindow();
           
            try
            {
                CheckSetupStep11();
            }
            catch (Exception e)
            {
                errorList.Add("Step_11: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step12_CheckEmpEnTurnusSettings()
        {
            var errorList = new List<string>();
            try
            {
                CheckTurnusDepSettingValues();
            }
            catch (Exception e)
            {
                errorList.Add("Step_12: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step13_CheckEmpFemAdmSettings()
        {
            var errorList = new List<string>();
            SelectRosterPlanLayout_2();

            try
            {
                CheckADMDepSettingValues();
            }
            catch (Exception e)
            {
                errorList.Add("Step_2: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step14_CheckEmpTiTurnusSettings()
        {
            var errorList = new List<string>();
            SelectRosterPlanLayout_3();
            try
            {
                CheckL38DepSettingValues();
                UICommon.UIMapVS2017.ResizeSettingsWindow();
                UICommon.UIMapVS2017.CloseDepRosterplanSettings();
            }
            catch (Exception e)
            {
                errorList.Add("Step_3: " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckDepSettingValuesStep5()
        {
            var errorList = new List<string>();
            SelectDepSettingsFromAdministration("arbeidsplanoppsett +avdeling", true);
            UICommon.UIMapVS2017.SelectADM_AdministrasjonInPlanSettingsWindow();

            try
            {
                CheckADMDepSettingValues();
            }
            catch (Exception e)
            {
                errorList.Add("Step_5: " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckDepSettingValuesStep6()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.SelectL38InPlanSettingsWindow();

            try
            {
                CheckL38DepSettingValues();
            }
            catch (Exception e)
            {
                errorList.Add("Step_6: " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckDepSettingValuesStep7()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.SelectTurnusInPlanSettingsWindow();

            try
            {
                CheckTurnusDepSettingValues();
            }
            catch (Exception e)
            {
                errorList.Add("Step_7: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step16_CheckErrorAndWarnings()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Feiladvarsler);
            UICommon.UIMapVS2017.CheckShowOnlyForSelectedEmpInWarningsSubTab();
            SelectEmpEnLine();
            try
            {
                CheckErrorAndWarningsStep16();
            }
            catch (Exception e)
            {
                errorList.Add("Step_16: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step17_CheckErrorAndWarnings()
        {
            var errorList = new List<string>();
            SelectEmpFemLine();
            try
            {
                CheckErrorAndWarningsStep17();
            }
            catch (Exception e)
            {
                errorList.Add("Step_17: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step18_CheckErrorAndWarnings()
        {
            var errorList = new List<string>();
            SelectEmpTiLine();
            try
            {
                CheckErrorAndWarningsStep18();
            }
            catch (Exception e)
            {
                errorList.Add("Step_18: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step21_CheckErrorAndWarnings()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Feiladvarsler);
            UICommon.UIMapVS2017.CheckShowOnlyForSelectedEmpInWarningsSubTab();
            SelectEmpEnLine();

            try
            {
                CheckErrorAndWarningsStep21();
            }
            catch (Exception e)
            {
                errorList.Add("Step_21: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step22_CheckErrorAndWarnings()
        {
            var errorList = new List<string>();
            SelectEmpFemLine();

            try
            {
                CheckErrorAndWarningsStep22();
            }
            catch (Exception e)
            {
                errorList.Add("Step_22: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step24_CheckErrorAndWarnings()
        {
            var errorList = new List<string>();
            SelectEmpTiLine();

            try
            {
                CheckErrorAndWarningsStep24();
            }
            catch (Exception e)
            {
                errorList.Add("Step_24: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step29_CheckErrorAndWarnings()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Feiladvarsler);
            UICommon.UIMapVS2017.CheckShowOnlyForSelectedEmpInWarningsSubTab();
            SelectEmpNiLine();
            try
            {
                CheckErrorAndWarningsStep29();
            }
            catch (Exception e)
            {
                errorList.Add("Step_29: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step30_CheckErrorAndWarnings()
        {
            var errorList = new List<string>();
            SelectEmpSeksLine();
            try
            {
                CheckErrorAndWarningsStep30();
            }
            catch (Exception e)
            {
                errorList.Add("Step_30: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step26_CheckSetupWindow()
        {
            var errorList = new List<string>();
            UICommon.ClickRosterplanLayout();
            UICommon.UIMapVS2017.ResizeSettingsWindow();
            UICommon.UIMapVS2017.OpenDropdownInPlanSettingsWindow();
            try
            {
                CheckSetupStep26();
            }
            catch (Exception e)
            {
                errorList.Add("Step_26: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step27_CheckSetupWindow()
        {
            var errorList = new List<string>();
            SelectRosterPlanLayout_4();
            try
            {
                CheckSetupStep27();
                UICommon.UIMapVS2017.ResizeSettingsWindow();
                UICommon.UIMapVS2017.CloseDepRosterplanSettings();
            }
            catch (Exception e)
            {
                errorList.Add("Step_27: " + e.Message);
            }

            return errorList;
        }

        #endregion

        public virtual SetMaxPlannedHoursPrWeekTo35Params SetMaxPlannedHoursPrWeekTo35Params
        {
            get
            {
                if ((this.mSetMaxPlannedHoursPrWeekTo35Params == null))
                {
                    this.mSetMaxPlannedHoursPrWeekTo35Params = new SetMaxPlannedHoursPrWeekTo35Params();
                }
                return this.mSetMaxPlannedHoursPrWeekTo35Params;
            }
        }

        private SetMaxPlannedHoursPrWeekTo35Params mSetMaxPlannedHoursPrWeekTo35Params;

    }
    /// <summary>
    /// Parameters to be passed into 'SetMaxPlannedHoursPrWeekTo35'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class SetMaxPlannedHoursPrWeekTo35Params
    {

        #region Fields
        /// <summary>
        /// Type '{Tab}' in 'eNumber' text box
        /// </summary>
        public string UIENumberEditSendKeys = "{Tab}";
        #endregion
    }
}
