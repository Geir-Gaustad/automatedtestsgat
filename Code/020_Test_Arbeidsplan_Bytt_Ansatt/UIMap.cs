namespace _020_Test_Arbeidsplan_Bytt_Ansatt
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using System.CodeDom.Compiler;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using CommonTestData;
    using System.Globalization;
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
        #region Common functions
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
        public string ReadPhysicalMemoryUsage()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.WorkingSet64);
        }
        public string ReadPagedMemorySize64()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.PagedMemorySize64);
        }
        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepByttAnsatt, null, "", logGatInfo);
        }

        private void SelectRosterplanTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }
        private void SelectEmployeeTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
        }
        public void SelectRosterplan(string planName, bool open, bool showAllPlans)
        {
            SelectRosterplanTab();
            UICommon.SelectRosterPlan(planName, open, showAllPlans);
        }
        private void CloseRosterplan()
        {
            UICommon.ClickRosterplanPlanTab();
            UICommon.CloseRosterplanFromPlanTab();
        }

        public void SelectRosterPlanFilterTab()
        {
            UICommon.ClickRosterplanFilterTab();
            //UICommon.ClickRosterplanHomeTab();
            //UICommon.ClickRosterplanPlanTab(); 
            //UICommon.ClickRosterplanPrintTab();
            //UICommon.ClickRosterplanSupportToolTab();
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

        #endregion

        public void Step_1()
        {
            StartGat(true);
            SelectRosterplan("Bytt ansatt AP", true, false);
            UICommon.SelectRosterplanPlanTab();
            TestContext.WriteLine("Step 1: OK");
        }
        public List<string> Step_2()
        {
            var errorList = new List<string>();

            RightClickCeciliaCell();
            ExchangeLinesAllerBruse();
            UICommon.UIMapVS2017.OpenRegStatusSwitchEmpWindow();

            try
            {
                CheckSwitchEmpRegStatusStep2();
                CheckSwitchEmpOkButtonDisabled();

            }
            catch (Exception e)
            {
                errorList.Add("Step_2: " + e.Message);
            }
            UICommon.UIMapVS2017.CloseRegStatusWindow();

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 2: OK");

            return errorList;
        }

        public List<string> Step_3()
        {
            var errorList = new List<string>();

            SwitchWholePeriod();

            try
            {
                CheckSwitchEmpRegStatusOkStep3();
            }
            catch (Exception e)
            {
                errorList.Add("Step_3: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 3: OK");

            return errorList;
        }
        public List<string> Step_4()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickOkExchangeEmployeeShift();
            SelectRosterPlanFilterTab();
            ShowInactive();

            try
            {
                CheckRowsStep4Bruse();
                CheckRowsStep4();
            }
            catch (Exception e)
            {
                errorList.Add("Step_4: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 4: OK");

            return errorList;
        }

        public List<string> Step_5()
        {
            var errorList = new List<string>();

            OpenBruseEmpLineSettings();

            try
            {
                CheckOwnerInLineSettings();
            }
            catch (Exception e)
            {
                errorList.Add("Step_5: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelInRosterplanEmployeeLineSettings();

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 5: OK");

            return errorList;
        }

        public List<string> Step_6()
        {
            var errorList = new List<string>();

            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickRosterplanRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();

            try
            {
                CheckReportBruseStep_6();
            }
            catch (Exception e)
            {
                errorList.Add("Step_6: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 6: OK");

            return errorList;
        }

        public void Step_7()
        {
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
        }

        public List<string> Step_8()
        {
            var errorList = new List<string>();

            RightClickCullenCell();
            ExchangeEmpCullenEriksson();
            UICommon.UIMapVS2017.ClickOkExchangeEmployeeShift();
            SelectRosterPlanFilterTab();
            UICommon.UIMapVS2017.ClickBackViewPeriodInFilterTab();
          
            try
            {
                CheckLinesStep8();
            }
            catch (Exception e)
            {
                errorList.Add("Step_8: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 8: OK");

            return errorList;
        }

        public List<string> Step_9()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickForwardViewPeriodInFilterTab();

            try
            {
                CheckLinesStep9();
            }
            catch (Exception e)
            {
                errorList.Add("Step_9: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 9: OK");

            return errorList;
        }

        public List<string> Step_10()
        {
            var errorList = new List<string>();

            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickRosterplanRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentTaskView();

            try
            {
                CheckCullenRolesStep_10();
            }
            catch (Exception e)
            {
                errorList.Add("Step_6: " + e.Message);
            }


            if (errorList.Count == 0)
                TestContext.WriteLine("Step 10: OK");

            return errorList;
        }

        public List<string> Step_11()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();
            UICommon.ClickRosterplanRoleAssignment();
            UICommon.UIMapVS2017.SelectRoleAssignmentEmployeeView();
            UICommon.UIMapVS2017.ClickWorkbookRoleAssignmentRoleListEmployeesTab();

            SelectErikson();

            try
            {
                CheckErikssonNoRolesStep_11();
            }
            catch (Exception e)
            {
                errorList.Add("Step_6: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 11: OK");

            return errorList;
        }

        public void Step_12()
        {
            UICommon.UIMapVS2017.CloseRoleAssignmentMainWindow();

            SelectRosterPlanFilterTab();

            UICommon.UIMapVS2017.SelectViewFilter("Stilling");

            UICommon.UIMapVS2017.ClickBackViewPeriodInFilterTab();
        }

        public List<string> Step_13()
        {
            var errorList = new List<string>();

            ExchangeEmploymentDressmannStep13();

            try
            {
                CheckEmploymentDressmannStep13();
            }
            catch (Exception e)
            {
                errorList.Add("Step_13: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 13: OK");

            return errorList;
        }
        public List<string> Step_14()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkExchangeEmployeeShift();
            Playback.Wait(500);
            OpenLinesettingsDressmannStep14();

            try
            {
                CheckLinesettingsDressmannStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Step_14: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 14: OK");

            return errorList;
        }
        public List<string> Step_15()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickCancelInRosterplanEmployeeLineSettings();
            OpenLinesettingsDressmannStep15();

            try
            {
                CheckLinesettingsDressmannStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Step_15: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 15: OK");

            return errorList;
        }

        public void Step_16()
        {
            UICommon.UIMapVS2017.ClickCancelInRosterplanEmployeeLineSettings();
        }

        public List<string> Step_17()
        {
            var errorList = new List<string>();

            SelectDressmanSecondLine();
            ExchangeEmploymentDressmannStep17();

            try
            {
                CheckLinesettingsDressmannStep17();
            }
            catch (Exception e)
            {
                errorList.Add("Step_17: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 17: OK");

            return errorList;
        }

        public List<string> Step_18()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkExchangeEmployeeShift();

            //Same line as step 15
            OpenLinesettingsDressmannStep15();

            try
            {
                CheckLinesettingsDressmannStep18();
            }
            catch (Exception e)
            {
                errorList.Add("Step_18: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelInRosterplanEmployeeLineSettings();

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 18: OK");

            return errorList;
        }

        public List<string> Step_19()
        {
            var errorList = new List<string>();

            OpenLinesettingsDressmannStep19();

            try
            {
                CheckLinesettingsDressmannStep19();
            }
            catch (Exception e)
            {
                errorList.Add("Step_19: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 19: OK");

            return errorList;
        }

        public List<string> Step_20()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickCancelInRosterplanEmployeeLineSettings();

            try
            {
                UICommon.SelectRosterplanPlanTab();
                UICommon.ClickEmployeesButtonRosterplan();
                SetVakansErikssonDressmanStep20();
            }
            catch (Exception e)
            {
                errorList.Add("Step_20: " + e.Message);
            }

            UICommon.ClickOkEmployeesWindow();

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 20: OK");

            return errorList;
        }

        public List<string> Step_21()
        {
            var errorList = new List<string>();
            
            try
            {
                UICommon.EffectuateRoasterplanNextPeriod();
                UICommon.EffectuateRosterplanLines(false);
                UICommon.CloseSalaryCalculationsWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 21: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 21: OK");

            return errorList;
        }

        public List<string> Step_22()
        {
            var errorList = new List<string>();

            ClickGustavssonCell();
            ExchangeEmploymentGustavssonStep22();

            try
            {
                CheckLinesettingsGustavssonStep22();
            }
            catch (Exception e)
            {
                errorList.Add("Step_22: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 22: OK");

            return errorList;
        }

        public List<string> Step_23()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickOkExchangeEmployeeShift();
            try
            {
                CheckLinesHvemVilJobbeStep23();
                CheckLinesStep23();
            }
            catch (Exception e)
            {
                errorList.Add("Step_23: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 23: OK");

            return errorList;
        }
        public List<string> Step_24()
        {
            var errorList = new List<string>();

            CloseRosterplan();
            SelectEmployeeTab();
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.EffectuationPeriodsTab, false);

            try
            {
                SelectGustavsson();
                CheckEmpGustavssonStep24();
            }
            catch (Exception e)
            {
                errorList.Add("Step_24: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 24: OK");

            return errorList;
        }
        public void Step_25()
        {
            SelectRosterplan("Bytt ansatt AP", true, false);
        }

        public List<string> Step_26_27()
        {
            var errorList = new List<string>();

            ClickVAKANTCell();
            ExchangeEmploymentVakantStep26_27();

            try
            {
                CheckLinesettingsVakantStep26_27();
            }
            catch (Exception e)
            {
                errorList.Add("Step_26_27: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 26_27: OK");

            return errorList;
        }

        public void Step_28()
        {
            UICommon.UIMapVS2017.ClickOkExchangeEmployeeShift();
        }

        public List<string> Step_29()
        {
            var errorList = new List<string>();

            SelectRosterPlanFilterTab();
            SelectDressmanSecondLine();
            ExchangeEmploymentDressmannStep29();

            try
            {
                CheckSwitchEmpOkButtonDisabled();
            }
            catch (Exception e)
            {
                errorList.Add("Step_29: " + e.Message);
            }
            UICommon.UIMapVS2017.OpenRegStatusSwitchEmpWindow();
            try
            {
                CheckSwitchEmpRegStatusStep29();
            }
            catch (Exception e)
            {
                errorList.Add("Step_29_2: " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRegStatusWindow();
            SwitchWholePeriod();

            try
            {
                CheckLinesettingsDressmannStep29();
            }
            catch (Exception e)
            {
                errorList.Add("Step_29_3: " + e.Message);
            }

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 29: OK");

            return errorList;
        }
        public void Step_30()
        {
            UICommon.UIMapVS2017.ClickOkExchangeEmployeeShift();
            CloseRosterplan();
        }


        public List<string> Step_31()
        {
            var errorList = new List<string>();

            SelectRosterplan("Bytt ansatt KP", true, false);

            ClickBruseCell();
            ExchangeEmploymentBruseStep31();

            try
            {
                CheckLinesettingsBruseStep31();
            }
            catch (Exception e)
            {
                errorList.Add("Step_31: " + e.Message);
            }
            UICommon.UIMapVS2017.ClickOkExchangeEmployeeShift();

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 31: OK");

            return errorList;
        }

        public List<string> Step_32()
        {
            var errorList = new List<string>();

            OpenBruseEmpLineSettingsStep32();

            try
            {
                CheckBruseEmpLineSettingsStep32();
            }
            catch (Exception e)
            {
                errorList.Add("Step_32: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelInRosterplanEmployeeLineSettings();
            OpenIngessonEmpLineSettingsStep32();

            try
            {
                CheckIngessonEmpLineSettingsStep32();
            }
            catch (Exception e)
            {
                errorList.Add("Step_32_2: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelInRosterplanEmployeeLineSettings();

            TestContext.WriteLine("Minnebruk før lukking av Gat: " + ReadPhysicalMemoryUsage());
            TestContext.WriteLine("PagedMemorySize før lukking av Gat: " + ReadPagedMemorySize64());

            CloseRosterplan();
            CloseGat();

            if (errorList.Count == 0)
                TestContext.WriteLine("Step 32: OK");

            return errorList;
        }


        public void ShowInactive()
        {
            UICommon.UIMapVS2017.SelectShowInactiveLinesInFilter();
        }
        private void SelectIngesson()
        {
            #region Variable Declarations
            DXCell uIDressmanDennisCell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIDressmanDennisCell2;
            uIDressmanDennisCell2.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanGridControlCell[View]gvRosterPlan[Row]4[Column]EmployeeName";
            #endregion

            // Right-Click 'Dressman, Dennis' cell
            Mouse.Click(uIDressmanDennisCell2, MouseButtons.Right, ModifierKeys.None, new Point(69, 7));
        }

        /// <summary>
        /// ExchangeEmploymentDressmannStep13 - Use 'ExchangeEmploymentDressmannStep13Params' to pass parameters into this method.
        /// </summary>
        private void ExchangeEmploymentDressmannStep13()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIByttansattstillingsfMenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIByttansattstillingsfMenuBaseButtonItem;
            DXCheckBox uIChkChangeEmploymentCheckBox = this.UIByttansattWindow.UIPcContentClient.UIGsPanelControl3Client.UIGsPanelControl2Client.UIChkChangeEmploymentCheckBox;
            DXLookUpEdit uILeNewEmploymentLookUpEdit = this.UIByttansattWindow.UIPcContentClient.UIGsPanelControl3Client.UIGsPanelControl2Client.UILeNewEmploymentLookUpEdit;
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIByttansattWindow.UIPcContentClient.UIGsPanelControl3Client.UIGsPanelControl4Client.UIEFromDateCustom.UIPceDateDateTimeEdit;
            #endregion

            ClickDressmanDennisCell();

            // Click 'Bytt ansatt/stillingsforhold på alle linjer (Aller, Cecilia)' MenuBaseButtonItem
            Mouse.Click(uIByttansattstillingsfMenuBaseButtonItem, new Point(102, 10));

            // Select 'chkChangeEmployment' check box
            uIChkChangeEmploymentCheckBox.Checked = this.ExchangeEmploymentDressmannStep13Params.UIChkChangeEmploymentCheckBoxChecked;

            // Type 'Gatsoft.Gat.BusinessLogic.Planning.RosterPlanning.DataAccessLayers.RosterEmploymentEntity' in 'leNewEmployment' LookUpEdit
            //ValueTypeName
            uILeNewEmploymentLookUpEdit.ValueTypeName = this.ExchangeEmploymentDressmannStep13Params.UILeNewEmploymentLookUpEditValueTypeName;

            // Type 'Dressman, Dennis - TURNUS/H 25 % 15.01.2018-31.12.2099' in 'leNewEmployment' LookUpEdit
            //ValueAsString
            uILeNewEmploymentLookUpEdit.ValueAsString = this.ExchangeEmploymentDressmannStep13Params.UILeNewEmploymentLookUpEditValueAsString;

            // Type '2024-01-15 [SelectionStart]0' in 'pceDate' DateTimeEdit
            //ValueAsString
            uIPceDateDateTimeEdit.ValueAsString = this.ExchangeEmploymentDressmannStep13Params.UIPceDateDateTimeEditValueAsString;

            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");
        }

        public virtual ExchangeEmploymentDressmannStep13Params ExchangeEmploymentDressmannStep13Params
        {
            get
            {
                if ((this.mExchangeEmploymentDressmannStep13Params == null))
                {
                    this.mExchangeEmploymentDressmannStep13Params = new ExchangeEmploymentDressmannStep13Params();
                }
                return this.mExchangeEmploymentDressmannStep13Params;
            }
        }

        private ExchangeEmploymentDressmannStep13Params mExchangeEmploymentDressmannStep13Params;

        private void SelectLinesettings()
        {
            Keyboard.SendKeys("{Down 2}{Enter}");
        }
        /// <summary>
        /// OpenBruseEmpLineSettings
        /// </summary>
        public void OpenBruseEmpLineSettings()
        {
            RightClickBruseCell();

            // Click 'Endre linjeinnstillinger (5200 - Bytt ansatt)' MenuBaseButtonItem
            SelectLinesettings();
        }

        /// <summary>
        /// OpenBruseEmpLineSettingsStep32
        /// </summary>
        public void OpenBruseEmpLineSettingsStep32()
        {
            ClickBruseCell();

            // Click 'Endre linjeinnstillinger (5200 - Bytt ansatt)' MenuBaseButtonItem
            SelectLinesettings();
        }

        /// <summary>
        /// OpenIngessonEmpLineSettingsStep32
        /// </summary>
        public void OpenIngessonEmpLineSettingsStep32()
        {
            SelectIngesson();

            // Click 'Endre linjeinnstillinger (5200 - Bytt ansatt)' MenuBaseButtonItem
            SelectLinesettings();
        }

        /// <summary>
        /// OpenLinesettingsDressmannStep14
        /// </summary>
        public void OpenLinesettingsDressmannStep14()
        {
            SelectDressmanFirstLine();

            // Click 'Endre linjeinnstillinger (5200 - Bytt ansatt)' MenuBaseButtonItem
            SelectLinesettings();
        }

        /// <summary>
        /// OpenLinesettingsDressmannStep15
        /// </summary>
        public void OpenLinesettingsDressmannStep15()
        {
            SelectDressmanSecondLine();

            // Click 'Endre linjeinnstillinger (5200 - Bytt ansatt)' MenuBaseButtonItem
            SelectLinesettings();
        }

        /// <summary>
        /// OpenLinesettingsDressmannStep19
        /// </summary>
        public void OpenLinesettingsDressmannStep19()
        {
            SelectDressmanThirdLine();

            // Click 'Endre linjeinnstillinger (5200 - Bytt ansatt)' MenuBaseButtonItem
            SelectLinesettings();
        }
    }
    /// <summary>
    /// Parameters to be passed into 'ExchangeEmploymentDressmannStep13'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class ExchangeEmploymentDressmannStep13Params
    {

        #region Fields
        /// <summary>
        /// Select 'chkChangeEmployment' check box
        /// </summary>
        public bool UIChkChangeEmploymentCheckBoxChecked = true;

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Planning.RosterPlanning.DataAccessLayers.RosterEmploymentEntity' in 'leNewEmployment' LookUpEdit
        /// </summary>
        public string UILeNewEmploymentLookUpEditValueTypeName = "Gatsoft.Gat.BusinessLogic.Planning.RosterPlanning.DataAccessLayers.RosterEmployme" +
            "ntEntity";

        /// <summary>
        /// Type 'Dressman, Dennis - TURNUS/H 25 % 15.01.2018-31.12.2099' in 'leNewEmployment' LookUpEdit
        /// </summary>
        public string UILeNewEmploymentLookUpEditValueAsString = "Dressman, Dennis - TURNUS/H 25 % 15.01.2018-31.12.2099";

        /// <summary>
        /// Type '2024-01-15 [SelectionStart]0' in 'pceDate' DateTimeEdit
        /// </summary>
        public string UIPceDateDateTimeEditValueAsString = "2024-01-15 [SelectionStart]0";
        #endregion
    }
}
