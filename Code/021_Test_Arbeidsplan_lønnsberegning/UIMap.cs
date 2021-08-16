namespace _021_Test_Arbeidsplan_lønnsberegning
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
    using System.Drawing;
    using System.Windows.Input;
    using System.Text.RegularExpressions;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System.Diagnostics;
    using System.Data;
    using CommonTestData;
    using System.Threading;
    using System.Globalization;
    using System.IO;

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        string FacitFilePath;
        string FacitFileName = "Data";
        string FileType = ".xls";
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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            var executingDir = TestData.GetWorkingDirectory;
            FacitFilePath = Path.Combine(executingDir, @"Reports\Test_021\");

            UICommon = new CommonUIFunctions.UIMap(TestContext);
            GetTestData();           
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
        private void GetTestData()
        {
            TestData.GetTestData(FacitFilePath + FacitFileName, FileType, TestContext);
        }
        
        #region LaunchGat & Login
        
        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(DepLønnsberegninger, null, "", logGatInfo);
        }

        /// <summary>
        /// CloseGat
        /// </summary>
        public void CloseGat()
        {
            try
            {
                UICommon.CloseGat();
                Playback.Wait(3000);
            }
            catch (Exception)
            {
                SupportFunctions.KillGatProcess(TestContext);
            }
        }
        #endregion
        
        public List<string> SetAndEffectuate(DateTime? planValidToDate, DateTime? startDateForDisplay, DateTime? actualDate, DateTime? toDate = null, bool useNightShiftOnStartDay = false, bool nightshiftsOnStartDay = false)
        {
            var errorList = new List<String>();
            var timeBeforeOpenEffectuationWindow = DateTime.Now;
            
            if (useNightShiftOnStartDay)
                OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(planValidToDate, startDateForDisplay, nightshiftsOnStartDay);

            EffectuatePlan();

            if (UICommon.EffectuationWindowExists())
            {
                var timeAfterOpenEffectuationWindow = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeOpenEffectuationWindow, timeAfterOpenEffectuationWindow, "Tidsforbruk ved åpning av iverksettingsvindu"));
            }

            if (startDateForDisplay != null)
                 OpenAndClosePreview();

            if (actualDate != null)
                ChangePeriodForActualLines(actualDate.Value, toDate);

            var timeBeforeEffectuation = DateTime.Now;

            EffectuateLines();

            if (UICommon.SalaryCalculationsWindowExists())
            {
                var timeAfterEffectuation = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved iverksetting"));
            }
                                  
            return errorList;
        }


        /// <summary>
        /// CheckButtonToShowPopup - Use 'CheckButtonToShowPopupExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckButtonToShowPopup()
        {
            #region Variable Declarations
            DXButton uIEditorButton0Button = this.UIArbeidsplanInnstilliWindow1.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UILeDisplayStartDateLookUpEdit.UIEditorButton0Button;
            #endregion

            Mouse.Click(uIEditorButton0Button);

            // Verify that the 'ClassName' property of 'EditorButton0' button equals 'EditorButton'
            //Assert.AreEqual(this.CheckButtonToShowPopupExpectedValues.UIEditorButton0ButtonClassName, uIEditorButton0Button.ClassName);
        }

        /// <summary>
        /// ClickToShowDropdown - Use 'ClickToShowDropdownParams' to pass parameters into this method.
        /// </summary>
        public void ClickToShowDropdown()
        {
            #region Variable Declarations
            DXLookUpEdit uILeDisplayStartDateLookUpEdit = this.UIArbeidsplanInnstilliWindow1.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UILeDisplayStartDateLookUpEdit;
            #endregion

            // Type '2015-06-29' in 'leDisplayStartDate' LookUpEdit
            //ValueAsString
            //uILeDisplayStartDateLookUpEdit.ValueAsString = this.ClickToShowDropdownParams.UILeDisplayStartDateLookUpEditValueAsString;

            uILeDisplayStartDateLookUpEdit.SetFocus();

            #region Variable Declarations
            DXButton uIEditorButton0Button = this.UIArbeidsplanInnstilliWindow1.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UILeDisplayStartDateLookUpEdit.UIEditorButton0Button;
            #endregion

            Mouse.Click(uIEditorButton0Button);

            //uILeDisplayStartDateLookUpEdit.ValueAsString = "05.12.2016";

            Keyboard.SendKeys("{0}{5}{.}{1}{2}{.}{2}{0}{1}{6}");
            //Keyboard.SendKeys(uILeDisplayStartDateLookUpEdit, "{05.12.2016}");
            Keyboard.SendKeys(uILeDisplayStartDateLookUpEdit, "{ENTER}");
            Keyboard.SendKeys("{ENTER}");
        }


        /// <summary>
        /// CheckDropDownValues - Use 'CheckDropDownValuesExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckDropDownValues()
        {
            #region Variable Declarations
            DXWindow uIPopupLookUpEditFormWindow = this.UIArbeidsplanInnstilliWindow1.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UILeDisplayStartDateLookUpEdit.UIPopupLookUpEditFormWindow;
            #endregion

            // Verify that the 'ClassName' property of 'PopupLookUpEditForm' window equals 'PopupLookUpEditForm'
            //Assert.AreEqual(this.CheckDropDownValuesExpectedValues.UIPopupLookUpEditFormWindowClassName, uIPopupLookUpEditFormWindow.ClassName);
        }


        /// <summary>
        /// CloseSysMessage
        /// </summary>
        public void CloseSysMessage()
        {
            #region Variable Declarations
            WinButton uIOKButton = this.UISystemmelding1Window.UIItemWindow.UISystemmelding1Client.UIOKButton;
            WinButton uICloseButton = this.UISystemmelding1Window.UISystemmelding1TitleBar.UICloseButton;
            #endregion

            try
            {
                // Click 'Resultater fra systemsjekk / faste SQL scripts' client
                Playback.Wait(1000);
                uIOKButton.WaitForControlReady();
                uIOKButton.DrawHighlight();
                Mouse.Click(uIOKButton);
            }
            catch (Exception)
            {
                uICloseButton.DrawHighlight();
                Mouse.Click(uICloseButton);
            }
        }

        public void LogRunningGatVersion()
        {
            #region Variable Declarations
            var mainWindow = this.UIGatver62030497ASCLAvWindow;
            #endregion

            try
            {
                var windowName = mainWindow.Name;
                windowName = windowName.Remove(windowName.IndexOf(" - "));

                TestContext.WriteLine(windowName);
            }
            catch (Exception)
            {
                TestContext.WriteLine("Unable to get Gat version");
            }
        }
        public string DepLønnsberegninger
        {
            get { return UICommon.DepLønnsberegninger; }
        }
        public string DepDiverse
        {
            get { return UICommon.DepDiverse; }
        }

        public bool SelectDepartmentByName(string depName)
        {
            #region Variable Declarations
            var depTable = this.UINivåWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayDepartmentsLayoutControlItem.UIGbDepartmentsClient.UIGDepartmentsTable;
            #endregion

            UICommon.ClearOtherDepartments();
            Playback.Wait(1000);

            var view = depTable.Views[0];
            for (int i = 0; i < view.RowCount; i++)
            {
                var val = view.GetCellValue("cDepName", i).ToString();
                if (val.Contains(depName))
                {
                    TestContext.WriteLine("Department found: " + val);
                    var selectCell = view.GetCell("cDepName", i);
                    try
                    {
                        Mouse.DoubleClick(selectCell);
                        Playback.Wait(3000);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// CopyLønnsberegningerPlan - Use 'CopyLønnsberegningerPlanParams' to pass parameters into this method.
        /// </summary>
        public void CopyLønnsberegningerPlan(PlanTypes plan, DateTime startDate, string weeks)
        {
            Playback.Wait(2000);
            switch (plan)
            {
                case PlanTypes.RosterPlan:
                    CreateRosterPlan(startDate, weeks);
                    break;
                case PlanTypes.HelpPlan:
                    CreateHelpPlan(startDate, weeks);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// OpenAdministrationRuleset - Use 'OpenAdministrationRulesetParams' to pass parameters into this method.
        /// </summary>
        public void OpenAdministrationRuleset()
        {
            UICommon.SelectFromAdministration("Definisjon +av +ulike +regelsett");
        }

        public bool IsWedBeforeIsHolydayOnTurnusCheckboxChecked()
        {
            #region Variable Declarations
            WinCheckBox uIOnsdagførskjærtorsdaCheckBox1 = this.UIRegelsettWindow.UIItemWindow1.UIItemClient.UIOnsdagførskjærtorsdaCheckBox1;
            #endregion

            return uIOnsdagførskjærtorsdaCheckBox1.Checked;
        }

        public void CreateRosterPlan(DateTime startDate, string weeks)
        {
            #region Variable Declarations
            var uITxtNameEdit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UITxtNameEdit;
            #endregion

            #region Variable Declarations
            DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanLØNNSBEREWindow.UIRcMenuRibbon.UIRpPlanRibbonPage;
            DXRibbonButtonItem uINyarbeidsplanRibbonBaseButtonItem = this.UIArbeidsplanLØNNSBEREWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanCopyRosterRibbonPageGroup.UINyarbeidsplanRibbonBaseButtonItem;
            DXPopupEdit uIPceDate1PopupEdit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIPceDate1PopupEdit;
            //DXTextEdit uIENumber0Edit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIENumber0Edit;
            DXButton uIOKButton = this.UINyarbeidsplanWindow.UIPnlButtonsClient.UIOKButton;
            DXButton uIOKButton1 = this.UIGT3999InformasjonWindow.UIOKButton;
            #endregion

            // Click 'rpPlan' RibbonPage
            Mouse.Click(uIRpPlanRibbonPage);

            // Click 'Ny arbeidsplan...' RibbonBaseButtonItem
            Mouse.Click(uINyarbeidsplanRibbonBaseButtonItem);

            uITxtNameEdit.ValueAsString = PlanNames.CopiedPlan;
            Keyboard.SendKeys(uITxtNameEdit, "{TAB}");

            UICommon.SetStartDateNewRosterplan(startDate);
            RemoveCheckForDraftRosterplanCopy();

            ////ValueAsString
            //uIENumber0Edit.ValueAsString = weeks;
            //Keyboard.SendKeys(uIENumber0Edit, "{TAB}");

            // Click 'Ok' button
            Mouse.Click(uIOKButton);

            // Click '&OK' button
            Mouse.Click(uIOKButton1);
        }

        /// <summary>
        /// CreateHelpPlan - Use 'CreateHelpPlanParams' to pass parameters into this method.
        /// </summary>
        public void CreateHelpPlan(DateTime startDate, string weeks)
        {
            #region Variable Declarations
            DXRibbonButtonItem uINyhjelpeplanRibbonBaseButtonItem = this.UIArbeidsplanKopiavLØNWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanCopyRosterRibbonPageGroup.UINyhjelpeplanRibbonBaseButtonItem;
            DXTextEdit uITxtNameEdit = this.UINyhjelpeplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UITxtNameEdit;
            DXPopupEdit uIPceDate1PopupEdit = this.UINyhjelpeplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIPceDate1PopupEdit;
            DXTextEdit uIENumber0Edit = this.UINyhjelpeplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIENumber0Edit;
            DXButton uIOKButton = this.UINyhjelpeplanWindow.UIPnlButtonsClient.UIOKButton;
            DXButton uIOKButton1 = this.UIGT3999InformasjonWindow.UIOKButton;
            #endregion

            // Click 'Ny hjelpeplan...' RibbonBaseButtonItem
            Mouse.Click(uINyhjelpeplanRibbonBaseButtonItem);

            uITxtNameEdit.ValueAsString = PlanNames.HelpPlan;
            Keyboard.SendKeys(uITxtNameEdit, "{TAB}");

            UICommon.SetStartDateNewHelpplan(startDate);

            //ValueAsString
            uIENumber0Edit.ValueAsString = weeks;
            Keyboard.SendKeys(uIENumber0Edit, "{TAB}");

            // Click 'Ok' button
            Mouse.Click(uIOKButton);

            // Click '&OK' button
            Mouse.Click(uIOKButton1);
        }


        /// <summary>
        /// InsertF4OnMondays - Use 'InsertF4OnMondaysParams' to pass parameters into this method.
        /// </summary>
        public void InsertF4OnMondays()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIRedigerRibbonBaseButtonItem = this.UIArbeidsplanHjelpeplaWindow1.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRibbonPageGroup9RibbonPageGroup.UIRedigerRibbonBaseButtonItem;
            DXCell uIItemCell = this.UIArbeidsplanHjelpeplaWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            DXGrid uIGcRosterPlanTable = this.UIArbeidsplanHjelpeplaWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            DXTextEdit uIRow1ColumnRosterCellEdit = this.UIArbeidsplanHjelpeplaWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit;
            DXButton uIGSSimpleButtonButton = this.UIItemWindow4.UIGSSimpleButtonButton;
            DXRibbonButtonItem uIBtnOkRibbonBaseButtonItem = this.UIArbeidsplanHjelpeplaWindow1.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRibbonPageGroup9RibbonPageGroup.UIBtnOkRibbonBaseButtonItem;
            #endregion

            // Click 'Rediger' RibbonBaseButtonItem
            Mouse.Click(uIRedigerRibbonBaseButtonItem, new Point(24, 32));

            // Click cell
            Mouse.Click(uIItemCell, new Point(167, 75));

            // Type 'Control + k' in 'gcRosterPlan' table
            Keyboard.SendKeys(uIGcRosterPlanTable, this.InsertF4OnMondaysParams.UIGcRosterPlanTableSendKeys, ModifierKeys.Control);

            // Type 'f4' in 'gcRosterPlan' table
            Keyboard.SendKeys(this.InsertF4OnMondaysParams.UIGcRosterPlanTableSendKeys1);
            Playback.Wait(1000);
            Keyboard.SendKeys("{ENTER}");

            // Click 'GSSimpleButton' button
            Playback.Wait(3000);
            Mouse.Click(uIGSSimpleButtonButton);

            // Click 'btnOk' RibbonBaseButtonItem
            Mouse.Click(uIBtnOkRibbonBaseButtonItem, new Point(27, 12));
        }


        /// <summary>
        /// ClickAdministratonTab
        /// </summary>
        public void ClickAdministratonTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Administration);
        }

        /// <summary>
        /// ClickRosterplanTab
        /// </summary>
        private void ClickRosterplanTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }

        public void ClickRosterplanPlanTab()
        {
            UICommon.ClickRosterplanPlanTab();
        }

        public void OpenPlan(string planName)
        {
            Playback.Wait(1500);
            ClickRosterplanTab();
            UICommon.SelectRosterPlan(planName);
        }

        public void CloseCurrentPlan()
        {
            try
            {
                UICommon.ClickRosterplanPlanTab();
                UICommon.CloseRosterplanFromPlanTab();
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
        }

        public void ClickEmployeeRosterplanWindow()
        {
            UICommon.ClickEmployeesButtonRosterplan();
        }

        /// <summary>
        /// DeleteRosterplanCopy
        /// </summary>
        public void DeletePlan(string planName)
        {
            #region Variable Declarations
            var uIItem5090Table = this.UIGatver62030497ASCLAvWindow1.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIPnlGridClient.UIGcRosterPlansTable;
            #endregion

            var view = uIItem5090Table.Views[0];
            for (int i = 0; i < view.RowCount; i++)
            {
                var val = view.GetCellValue("gcolPlan", i).ToString();
                if (val == planName)
                {
                    var selectCell = view.GetCell("gcolPlan", i);

                    selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlansGridControlCell[View]gvRosterPlans[Row]" + i + "[Column]gcolPlan";
                    Mouse.Click(selectCell);

                    Playback.Wait(1000);
                    DeleteSelectedPlan();

                    return;
                }
            }
        }

        /// <summary>
        /// OpenAndClosePreview
        /// </summary>
        public void OpenAndClosePreview()
        {
            #region Variable Declarations
            DXButton uIForhåndsvisningaviveButton = this.UIIverksetteWindow.UIPnlButtonsClient.UIForhåndsvisningaviveButton;
            DXButton uIGSSimpleButtonButton = this.UIItemWindow5.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Click 'Forhåndsvisning av iverksettingsdata' button
            Mouse.Click(uIForhåndsvisningaviveButton);

            try
            {
                Playback.Wait(2000);
                // Click 'GSSimpleButton' button
                uIGSSimpleButtonButton.DrawHighlight();
                Mouse.Click(uIGSSimpleButtonButton);
            }
            catch (Exception)
            {
                #region Variable Declarations
                DXButton uICloseButton = this.UIItemWindow5.UICloseButton;
                #endregion

                // Click 'Close' button
                uICloseButton.DrawHighlight();
                Mouse.Click(uICloseButton);
            }
        }

        /// <summary>
        /// CloseSalaryCalculationWindow
        /// </summary>
        public void CloseSalaryCalculationWindow()
        {
            #region Variable Declarations
            DXButton uIOKButton = this.UILønnsberegningvediveWindow.UIPcButtonPanelClient.UIOKButton;
            #endregion

            Mouse.Click(uIOKButton);
        }

        public void OpensSettingsChangePlanToDateAndCheckNightshiftsOnStartDay(DateTime? planValidToDate, DateTime? startDateForDisplay, bool checkNightshifts = false /*, bool calculateStartDateAuto = false*/)
        {
            #region Variable Declarations
            DXCheckBox uIChkNighShiftOnStartDCheckBox = this.UIArbeidsplanInnstilliWindow1.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkNighShiftOnStartDCheckBox;
            DXButton uIGSSimpleButtonButton = this.UIArbeidsplanInnstilliWindow1.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion
            
            // Click 'Innstillinger' RibbonBaseButtonItem
            UICommon.OpenRosterplanSettings();

            if (planValidToDate != null)
            {
                #region Variable Declarations
                DXPopupEdit uIPceDate0PopupEdit = this.UIArbeidsplanInnstilliWindow1.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIPceDate0PopupEdit;
                #endregion

                UICommon.SetValidToDateRosterplan(planValidToDate.Value);
            }

            // Check 'chkDoCalculateDisplayAutomatically' check box
            //uIChkDoCalculateDisplaCheckBox.Checked = calculateStartDateAuto;

            if (startDateForDisplay != null)
            {

                #region Variable Declarations
                DXLookUpEdit uILeDisplayStartDateLookUpEdit = this.UIArbeidsplanInnstilliWindow1.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UILeDisplayStartDateLookUpEdit;
                #endregion

                // Type 'System.DateTime' in 'leDisplayStartDate' LookUpEdit
                //ValueTypeName
                uILeDisplayStartDateLookUpEdit.ValueTypeName = this.SetStartdayToDisplayParams.UILeDisplayStartDateLookUpEditValueTypeName;

                // Type '2011-05-16' in 'leDisplayStartDate' LookUpEdit
                //ValueAsString
                uILeDisplayStartDateLookUpEdit.WaitForControlReady();
                uILeDisplayStartDateLookUpEdit.ValueAsString = startDateForDisplay.Value.ToString("yyyy-MM-dd"); ; //'2011-05-16'
                //Keyboard.SendKeys(uILeDisplayStartDateLookUpEdit, "{TAB}");
            }

            // Select 'chkNighShiftOnStartDay' check box
            uIChkNighShiftOnStartDCheckBox.Checked = checkNightshifts;


            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        public void ChangePeriodForActualLines(DateTime fromDate, DateTime? toDate)
        {
            UICommon.ChangeEffectuationPeriodForActualLines(fromDate, toDate);
        }

        public void EffectuatePlan()
        {
            Playback.Wait(2000);

            try
            {
                UICommon.EffectuateFullRosterplan(true);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{DOWN 2}{ENTER}");
            }
        }

        /// <summary>
        /// EffectuateLines_2412_StartDay
        /// </summary>
        /// 
        public void EffectuateLines()
        {
            UICommon.EffectuateRosterplanLines(false);
        }

        public void DeleteEffectuation()
        {
            UICommon.DeleteEffectuationRosterplan();

            if (UICommon.SelectAllAndWaitForDeleteEffectuationWindowReady())
            {
                UICommon.DeleteEffectuatedLines();
                UICommon.CloseDeleteEffectuationOkWindow();
            }
        }

        public string ReadPhysicalMemoryUsage()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.WorkingSet64);
        }
        public string ReadPagedMemorySize64()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.PagedMemorySize64);
        }
        public List<string> TimeLapseInSeconds(DateTime timeBefore, DateTime timeAfter, string text)
        {
            List<string> errorList = new List<string>();
            string elapsedTimeOutput = "";

            errorList.AddRange(LoadBalanceTesting.TimeLapseInSeconds(timeBefore, timeAfter, text, out elapsedTimeOutput));
            TestContext.WriteLine(elapsedTimeOutput);

            return errorList;
        }


        public List<String> CheckGridData(string name, string value, string salaryType)
        {
            #region Variable Declarations
            DXGrid uIGcReceiptTable = this.UILønnsberegningvediveWindow.UIPcReceiptPanelClient.UIGcReceiptTable;
            var view = uIGcReceiptTable.Views[0];
            #endregion

            return GridDataResults(view, "gcName", name, value, salaryType);
        }
        public void SetWednesdayBeforeIsHolydayInTurnus(bool wednesdayIsholyday, string selectPlan = "")
        {
            ClickAdministratonTab();
            OpenAdministrationRuleset();
            SelectWedBeforeIsHolydayOnTurnus(wednesdayIsholyday);
            CloseAdministrationRuleset();
            //UICommon.ClearAdministrationSearchString();

            if (!string.IsNullOrEmpty(selectPlan))
                 OpenPlan(selectPlan);
        }
        /// <summary>
        /// SelectWedBeforeIsHolydayOnTurnus - Use 'SelectWedBeforeIsHolydayOnTurnusParams' to pass parameters into this method.
        /// </summary>
        public void SelectWedBeforeIsHolydayOnTurnus(bool checkWednesdayBefore)
        {
            #region Variable Declarations
            WinClient uIItemClient = this.UIRegelsettWindow.UIItemWindow.UIItemClient;
            WinClient uIItemClient1 = this.UIRegelsettWindow.UIRegelsettClient.UIItemClient;
            WinButton uIAvbrytButton = this.UIRegelsettWindow.UIItemClient.UIAvbrytButton;
            WinButton uIOKButton = this.UIRegelsettWindow.UIItemClient.UIOKButton;
            #endregion

            // Click client
            Mouse.Click(uIItemClient, new Point(41, 116));

            // Click client
            Mouse.Click(uIItemClient1, new Point(91, 26));


            // Select 'Onsdag før skjærtorsdag er helligdag i turnusplane...' check box
            if (checkWednesdayBefore)
            {
                if (IsWedBeforeIsHolydayOnTurnusCheckboxChecked())
                {
                    Mouse.Click(uIAvbrytButton);
                }
                else
                {
                    CheckWedBeforeIsHolydayOnTurnus();
                    Mouse.Click(uIOKButton);
                }
            }
            else
            {
                if (!IsWedBeforeIsHolydayOnTurnusCheckboxChecked())
                {
                    Mouse.Click(uIAvbrytButton);
                }
                else
                {
                    ClearWedBeforeIsHolydayOnTurnus();
                    Mouse.Click(uIOKButton);
                }
            }
        }

        /// <summary>
        /// UncheckStartDate - Use 'UncheckStartDateParams' to pass parameters into this method.
        /// </summary>
        public void UncheckStartDate()
        {
            #region Variable Declarations
            DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanKopiavLØNWindow.UIRcMenuRibbon.UIRpPlanRibbonPage;
            DXRibbonButtonItem uIInnstillingerRibbonBaseButtonItem = this.UIArbeidsplanKopiavLØNWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanRibbonPageGroup.UIInnstillingerRibbonBaseButtonItem;
            DXCheckBox uIChkDoCalculateDisplaCheckBox = this.UIArbeidsplanInnstilliWindow1.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkDoCalculateDisplaCheckBox;
            #endregion

            Mouse.Click(uIRpPlanRibbonPage);
            Mouse.Click(uIInnstillingerRibbonBaseButtonItem);

            // Clear 'chkDoCalculateDisplayAutomatically' check box
            uIChkDoCalculateDisplaCheckBox.Checked = this.UncheckStartDateParams.UIChkDoCalculateDisplaCheckBoxChecked;
        }

        public void CheckCauseCodesAndCloseWindow(bool clearAll = false)
        {

            #region Variable Declarations
            DXButton uIMaximizeButton = this.UIAnsatteiArbeidsplanWindow.UIMaximizeButton;
            DXTreeListCell uIItem1100TreeListCell = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode0TreeListNode.UINode0TreeListNode1.UIItem1100TreeListCell;
            DXLookUpEdit uIEOvertimeCodeLookUpEdit = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerOvertCustom.UIEOvertimeCodeLookUpEdit;
            DXTreeListCell uIItem1100TreeListCell1 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode1TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItem1100TreeListCell2 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode2TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItem1100TreeListCell3 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode3TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItemTreeListCell = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode4TreeListNode.UINode0TreeListNode.UIItemTreeListCell;
            DXTreeListCell uIItem1100TreeListCell4 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode5TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItem1100TreeListCell5 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode6TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItem1100TreeListCell6 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode7TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItem1100TreeListCell7 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode8TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItem1100TreeListCell8 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode9TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItem1100TreeListCell9 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode10TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItem1100TreeListCell10 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode11TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItem1100TreeListCell11 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode12TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXButton uIOKButton = this.UIAnsatteiArbeidsplanWindow.UIOKButton;
            #endregion

            // Click 'Maximize' button
            Mouse.Click(uIMaximizeButton);

            // Click '1. 100%' TreeListCell
            Mouse.Click(uIItem1100TreeListCell);
            Keyboard.SendKeys("a", ModifierKeys.Control);
            Playback.Wait(1500);

            // Type 'Gatsoft.Gat.RosterPlan.EmployeeManager.UI.ViewModels.Data.OvertimeCodeViewModel' in 'eOvertimeCode' LookUpEdit
            //ValueTypeName
            uIEOvertimeCodeLookUpEdit.ValueTypeName = this.CheckCauseCodesAndCloseWindowNewParams.UIEOvertimeCodeViewModel;


            if (clearAll)
            {

                // Type 'S - Sykevikar' in 'eOvertimeCode' LookUpEdit
                //ValueAsString
                uIEOvertimeCodeLookUpEdit.ValueAsString = "";
                Keyboard.SendKeys(uIEOvertimeCodeLookUpEdit, "{TAB}");
                Playback.Wait(1500);

                // Click 'OK' button
                Mouse.Click(uIOKButton);
                return;
            }

            // Type 'S - Sykevikar' in 'eOvertimeCode' LookUpEdit
            //ValueAsString
            uIEOvertimeCodeLookUpEdit.ValueAsString = this.CheckCauseCodesAndCloseWindowNewParams.UIEOvertimeCodeSykevikar;
            Keyboard.SendKeys(uIEOvertimeCodeLookUpEdit, "{TAB}");
            Playback.Wait(1500);

            Mouse.Click(uIItem1100TreeListCell);
            Mouse.Click(uIItem1100TreeListCell1, ModifierKeys.Control);
            Mouse.Click(uIItem1100TreeListCell2, ModifierKeys.Control);
            Mouse.Click(uIItem1100TreeListCell3, ModifierKeys.Control);

            // Type 'Gatsoft.Gat.RosterPlan.EmployeeManager.UI.ViewModels.Data.OvertimeCodeViewModel' in 'eOvertimeCode' LookUpEdit
            //ValueTypeName
            uIEOvertimeCodeLookUpEdit.ValueTypeName = this.CheckCauseCodesAndCloseWindowNewParams.UIEOvertimeCodeViewModel;

            // Type 'F - Ferievikar' in 'eOvertimeCode' LookUpEdit
            //ValueAsString
            uIEOvertimeCodeLookUpEdit.ValueAsString = this.CheckCauseCodesAndCloseWindowNewParams.UIEOvertimeFerieVikar;
            Keyboard.SendKeys(uIEOvertimeCodeLookUpEdit, "{TAB}");

            Mouse.Click(uIItem1100TreeListCell4);
            Mouse.Click(uIItem1100TreeListCell5, ModifierKeys.Control);
            Mouse.Click(uIItem1100TreeListCell6, ModifierKeys.Control);
            Mouse.Click(uIItem1100TreeListCell7, ModifierKeys.Control);
            // Type 'Gatsoft.Gat.RosterPlan.EmployeeManager.UI.ViewModels.Data.OvertimeCodeViewModel' in 'eOvertimeCode' LookUpEdit
            //ValueTypeName
            uIEOvertimeCodeLookUpEdit.ValueTypeName = this.CheckCauseCodesAndCloseWindowNewParams.UIEOvertimeCodeViewModel;

            // Type 'V - Vakans' in 'eOvertimeCode' LookUpEdit
            //ValueAsString
            uIEOvertimeCodeLookUpEdit.ValueAsString = this.CheckCauseCodesAndCloseWindowNewParams.UIEOvertimeCodeVakans;
            Keyboard.SendKeys(uIEOvertimeCodeLookUpEdit, "{TAB}");

            Mouse.Click(uIItem1100TreeListCell8);
            Mouse.Click(uIItem1100TreeListCell9, ModifierKeys.Control);
            Mouse.Click(uIItem1100TreeListCell10, ModifierKeys.Control);
            Mouse.Click(uIItem1100TreeListCell11, ModifierKeys.Control);

            // Type 'Gatsoft.Gat.RosterPlan.EmployeeManager.UI.ViewModels.Data.OvertimeCodeViewModel' in 'eOvertimeCode' LookUpEdit
            //ValueTypeName
            uIEOvertimeCodeLookUpEdit.ValueTypeName = this.CheckCauseCodesAndCloseWindowNewParams.UIEOvertimeCodeViewModel;

            // Type 'P - Permisjon' in 'eOvertimeCode' LookUpEdit
            //ValueAsString
            uIEOvertimeCodeLookUpEdit.ValueAsString = this.CheckCauseCodesAndCloseWindowNewParams.UIEOvertimeCodePermisjon;
            Keyboard.SendKeys(uIEOvertimeCodeLookUpEdit, "{TAB}");

            // Click 'OK' button
            Mouse.Click(uIOKButton);
        }

        public void TestReadTitle()
        {

            //ApplicationUnderTest gATTURNUSApplication = ApplicationUnderTest.Launch(ExePathGatTurnus, AlternateExePath);

            WinEdit uIItemEdit = this.UILoginWindow.UIItemWindow.UIItemEdit;
            WinButton uIOKButton = this.UILoginWindow.UILoginWindow1.UILoginClient.UIOKButton;

            // Click text box
            Mouse.Click(uIItemEdit);

            // Type '********' in text box
            Keyboard.SendKeys(uIItemEdit, this.LoginParams.UIItemEditSendKeys, true);

            // Click 'OK' button
            Mouse.Click(uIOKButton);

            CloseSysMessage();

            #region Variable Declarations
            var mainWindow = this.UIGatver62030497ASCLAvWindow;
            #endregion

            var windowName = mainWindow.Name;
            windowName = windowName.Remove(windowName.IndexOf(" - "));

            TestContext.WriteLine(windowName);
        }

        public List<String> TestExcelGridData(string name, string value)
        {
            var replacedValue = value.Replace(".", "#");
            var dataList = new List<String>();
                        
            //Lage en enum og ta den inn som parameter!
            var dataSource = TestData.GlobalDataSource(TestContext);
            foreach (DataRow row in dataSource.Rows)
            {
                var shiftCodeName = "";
                var shiftCodeValue = "";

                try
                {
                    shiftCodeName = row[name].ToString();
                    shiftCodeValue = row[replacedValue].ToString();
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Error: " + ex.Message);
                }

                if (!String.IsNullOrEmpty(shiftCodeName))
                    dataList.Add(shiftCodeName + " - " + shiftCodeValue);
            }

            return dataList;
        }

        public List<String> CheckFixedPaymentsGridData(string name, string value, int salaryType)
        {
            if (name == "")
                return GridDataResultsTest936New(value, salaryType);


            return GridDataResultsFixedPayments(name, value, salaryType);
        }


        private List<String> GridDataResultsFixedPayments(string name, string value, int salaryType)
        {
            var errorList = new List<String>();
            var expectedShiftCodePeriodsDict = new Dictionary<string, string>();
            var expectedValuesDict = new Dictionary<string, string>();
            var actualValuesDict = new Dictionary<string, string>();
            var uiPivotGrid = this.UIArbeidsplanFASTETILLWindow1.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid;

            var replacedValue = value.Replace(".", "#");
            var shiftCodeNamePeriodData = "";
            var shiftCodeValue = "";
            var dataSource = TestData.GlobalDataSource(TestContext);

            for (int i = 0; i < dataSource.Rows.Count; i++)
            {
                shiftCodeNamePeriodData = dataSource.Rows[i][name].ToString();
                if (String.IsNullOrEmpty(shiftCodeNamePeriodData))
                    continue;

                shiftCodeValue = dataSource.Rows[i][replacedValue].ToString();
                expectedValuesDict.Add(i.ToString(), shiftCodeValue);
            }

            uiPivotGrid.DrawHighlight();
            for (var i = 0; i < uiPivotGrid.RowCount; i++)
            {
                var actualValue = "NA";
                try
                {
                    var cellRes = uiPivotGrid.GetCellValue(salaryType, i);
                    if (cellRes != null)
                    {
                        var inputValue = Convert.ToDouble(cellRes);
                        inputValue = Math.Round(inputValue, 2);
                        actualValue = inputValue.ToString().Trim();
                    }

                    actualValuesDict.Add(i.ToString(), actualValue);
                }
                catch (Exception)
                {
                    TestContext.WriteLine("Error, unable to find salarytype: " + value + "(" + salaryType + ")");
                }
            }

            //PaymentCell_200 = Timelønn
            //PaymentCell_400 = lørdag/søndag
            //PaymentCell_410 = Kveld/Nattillegg
            //PaymentCell_430 = Helligdagstillegg fast
            //PaymentCell_450 = F4 avspasering
            foreach (var key in actualValuesDict.Keys)
            {
                if (expectedValuesDict.ContainsKey(key))
                {
                    var expectedValue = "";
                    var actualValue = "";

                    expectedValuesDict.TryGetValue(key, out expectedValue);
                    actualValuesDict.TryGetValue(key, out actualValue);

                    if (actualValue.Contains(","))
                        actualValue = actualValue.TrimEnd('0').TrimEnd(',');

                    expectedValue = expectedValue.Replace(".", ",");

                    try
                    {
                        Assert.AreEqual(expectedValue, actualValue, key);
                        TestContext.WriteLine(key + ":" + Environment.NewLine + "Forventet: " + expectedValue + ", Resultat: " + actualValue + "- Lønnstype: " + value);
                    }
                    catch (Exception ex)
                    {
                        errorList.Add(Environment.NewLine + "Uventet verdi (Kode(Radnr.): " + key + " - " + value + "), " + ex.Message);
                    }
                }
                else
                {
                    errorList.Add(Environment.NewLine + "Finner ikke vaktkode: " + key + " - " + value + ")");
                }
            }

            return errorList;

            #region Kode som henter ut ansatt og dato verdier

            //foreach (DataRow row in Configuration.GlobalDataSource.Rows)
            //{
            //    try
            //    {
            //        shiftCodeNamePeriodData = row[name].ToString();
            //        if (String.IsNullOrEmpty(shiftCodeNamePeriodData))
            //            continue;
            //        shiftCodeValue = row[replacedValue].ToString();

            //        if (userDatePeriod)
            //            shiftCodeNamePeriodData = shiftCodeNamePeriodData + ";" + row["DatePeriod"].ToString();

            //    }
            //    catch (Exception ex)
            //    {
            //        TestContext.WriteLine("Error BASEDATA: " + ex.Message);
            //        return errorList;
            //    }
            //    expectedValuesDict.Add(shiftCodeNamePeriodData, shiftCodeValue);
            //}


            //for (var i = 0; i < uiPivotGrid.RowCount; i++)
            //{
            //    var shiftCode = uiPivotGrid.GetFieldValueItem(column, i).Value.ToString();
            //    if (String.IsNullOrEmpty(shiftCode))
            //        continue;

            //    var trimChk = shiftCode.Replace(" ", "");
            //    var shiftCodeNamePeriodView = trimChk.Replace(",", " ");

            //    if (userDatePeriod)
            //    {
            //        var period = uiPivotGrid.GetFieldValueItem("Start", i).Text + "-" + uiPivotGrid.GetFieldValueItem("End", i).Text;
            //        shiftCodeNamePeriodView = shiftCodeNamePeriodView + ";" + period;
            //    }

            //    var actualValue = "";
            //    try
            //    {
            //        var cellRes = uiPivotGrid.GetCellValue(salaryType, i);
            //        if (cellRes != null)
            //            actualValue = cellRes.ToString().Trim();

            //        actualValuesDict.Add(shiftCodeNamePeriodView, actualValue);
            //    }
            //    catch (Exception)
            //    {
            //        TestContext.WriteLine("Error, unable to find salarytype: " + salaryType);
            //        actualValue = "no value found";
            //    }
            //}

            #endregion
        }

        public List<String> GridDataResultsTest936New(string value, int salaryType)
        {
            var uiPivotGrid = this.UIArbeidsplanFASTETILLWindow1.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid;

            var errorList = new List<String>();
            var replacedValue = value.Replace(".", "#");
            var shiftCodeValue = "";
            var dataSource = TestData.GlobalDataSource(TestContext);

            try
            {
                shiftCodeValue = dataSource.Rows[0][replacedValue].ToString();
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error BASEDATA: " + ex.Message);
                return errorList;
            }

            for (var i = 0; i < uiPivotGrid.RowCount; i++)
            {
                var actualValue = "NA";
                try
                {
                    var cellRes = uiPivotGrid.GetCellValue(salaryType, i);
                    if (cellRes == null || cellRes.ToString() == String.Empty)
                        continue;

                    var inputValue = Convert.ToDouble(cellRes);
                    inputValue = Math.Round(inputValue, 2);
                    actualValue = inputValue.ToString().Trim();

                    var expectedValue = shiftCodeValue.Replace(".", ",");

                    if (actualValue.Contains(","))
                        actualValue = actualValue.TrimEnd('0').TrimEnd(',');

                    try
                    {
                        Assert.AreEqual(expectedValue, actualValue, "Totalt for perioden");
                        TestContext.WriteLine("Forventet: " + expectedValue + ", Resultat: " + actualValue + "- Lønnstype: " + value);
                        break;
                    }
                    catch (Exception ex)
                    {
                        errorList.Add(Environment.NewLine + "Uventet verdi (Kode: " + value + "), " + ex.Message);
                    }
                }
                catch (Exception)
                {
                    TestContext.WriteLine("Error, unable to find salarytype: " + value + "(" + salaryType + ")");
                }
            }

            return errorList;
        }

        private List<String> GridDataResults(DXGrid.View view, string columnName, string name, string value, string salaryType, bool userDatePeriod = false)
        {
            var errorList = new List<String>();
            var expectedValuesDict = new Dictionary<string, string>();
            var actualValuesDict = new Dictionary<string, string>();

            var replacedValue = value.Replace(".", "#");
            var shiftCodeNamePeriodData = "";
            var shiftCodeValue = "";
            var dataSource = TestData.GlobalDataSource(TestContext);

            foreach (DataRow row in dataSource.Rows)
            {
                try
                {
                    shiftCodeNamePeriodData = row[name].ToString();
                    if (String.IsNullOrEmpty(shiftCodeNamePeriodData))
                        continue;
                    shiftCodeValue = row[replacedValue].ToString();

                    if (userDatePeriod)
                        shiftCodeNamePeriodData = shiftCodeNamePeriodData + ";" + row["DatePeriod"].ToString();

                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Error BASEDATA: " + ex.Message);
                    return errorList;
                }
                expectedValuesDict.Add(shiftCodeNamePeriodData, shiftCodeValue);
            }

            for (var i = 0; i < view.RowCount; i++)
            {
                var shiftCode = view.GetCellValue(columnName, i).ToString();
                if (String.IsNullOrEmpty(shiftCode))
                    continue;

                var trimChk = shiftCode.Replace(" ", "");
                var shiftCodeNamePeriodView = trimChk.Replace(",", " ");

                if (userDatePeriod)
                {
                    var period = view.GetCellValue("colTransferFromDate", i).ToString() + "-" + view.GetCellValue("colTransferToDate", i).ToString();
                    shiftCodeNamePeriodView = shiftCodeNamePeriodView + ";" + period;
                }

                var actualValue = "NA";
                try
                {
                    var cellRes = view.GetCellValue(salaryType, i);
                    if (cellRes != null)
                        actualValue = cellRes.ToString().Trim();

                    actualValuesDict.Add(shiftCodeNamePeriodView, actualValue);
                }
                catch (Exception)
                {
                    TestContext.WriteLine("Error, unable to find salarytype: " + salaryType);
                }
            }

            //PaymentCell_200 = Timelønn
            //PaymentCell_400 = lørdag/søndag
            //PaymentCell_410 = Kveld/Nattillegg
            //PaymentCell_430 = Helligdagstillegg fast
            //PaymentCell_450 = F4 avspasering
            foreach (var key in actualValuesDict.Keys)
            {
                if (expectedValuesDict.ContainsKey(key))
                {
                    var expectedValue = "";
                    var actualValue = "";

                    expectedValuesDict.TryGetValue(key, out expectedValue);
                    actualValuesDict.TryGetValue(key, out actualValue);

                    if (actualValue.Contains(","))
                        actualValue = actualValue.TrimEnd('0').TrimEnd(',');

                    expectedValue = expectedValue.Replace(".", ",");

                    try
                    {
                        Assert.AreEqual(expectedValue, actualValue, key);
                        TestContext.WriteLine(key + ":" + Environment.NewLine + "Forventet: " + expectedValue + ", Resultat: " + actualValue + "- Lønnstype: " + salaryType);
                    }
                    catch (Exception ex)
                    {
                        errorList.Add(Environment.NewLine + "Uventet verdi (Kode: " + key + " - " + value + "), " + ex.Message);
                    }
                }
                else
                {
                    errorList.Add(Environment.NewLine + "Finner ikke vaktkode: " + key + " - " + value + ")");
                }
            }

            return errorList;
        }

        public void KillGatProcess()
        {
            SupportFunctions.KillGatProcess(TestContext);
        }

        public void AssertResults(List<String> errorList)
        {
            Assert.Fail(SupportFunctions.AssertResults(errorList));
        }

        public enum PlanTypes
        {
            RosterPlan,
            HelpPlan
        }

        #region Parameters

        public ChangePeriodFoCurrentLines ChangePeriodFoCurrentLines
        {
            get
            {
                if ((this.mChangePeriodFoCurrentLines == null))
                {
                    this.mChangePeriodFoCurrentLines = new ChangePeriodFoCurrentLines();
                }
                return this.mChangePeriodFoCurrentLines;
            }
        }

        private ChangePeriodFoCurrentLines mChangePeriodFoCurrentLines;

        #endregion

        public virtual InsertF4OnMondaysParams InsertF4OnMondaysParams
        {
            get
            {
                if ((this.mInsertF4OnMondaysParams == null))
                {
                    this.mInsertF4OnMondaysParams = new InsertF4OnMondaysParams();
                }
                return this.mInsertF4OnMondaysParams;
            }
        }

        private InsertF4OnMondaysParams mInsertF4OnMondaysParams;

        public virtual SetStartdayToDisplayParams SetStartdayToDisplayParams
        {
            get
            {
                if ((this.mSetStartdayToDisplayParams == null))
                {
                    this.mSetStartdayToDisplayParams = new SetStartdayToDisplayParams();
                }
                return this.mSetStartdayToDisplayParams;
            }
        }

        private SetStartdayToDisplayParams mSetStartdayToDisplayParams;

        public virtual ShowAllPlansParams ShowAllPlansParams
        {
            get
            {
                if ((this.mShowAllPlansParams == null))
                {
                    this.mShowAllPlansParams = new ShowAllPlansParams();
                }
                return this.mShowAllPlansParams;
            }
        }

        private ShowAllPlansParams mShowAllPlansParams;

        public virtual SelectStartDateParams SelectStartDateParams
        {
            get
            {
                if ((this.mSelectStartDateParams == null))
                {
                    this.mSelectStartDateParams = new SelectStartDateParams();
                }
                return this.mSelectStartDateParams;
            }
        }

        private SelectStartDateParams mSelectStartDateParams;

        public virtual UncheckStartDateParams UncheckStartDateParams
        {
            get
            {
                if ((this.mUncheckStartDateParams == null))
                {
                    this.mUncheckStartDateParams = new UncheckStartDateParams();
                }
                return this.mUncheckStartDateParams;
            }
        }

        private UncheckStartDateParams mUncheckStartDateParams;

        public virtual CheckButtonToShowPopupExpectedValues CheckButtonToShowPopupExpectedValues
        {
            get
            {
                if ((this.mCheckButtonToShowPopupExpectedValues == null))
                {
                    this.mCheckButtonToShowPopupExpectedValues = new CheckButtonToShowPopupExpectedValues();
                }
                return this.mCheckButtonToShowPopupExpectedValues;
            }
        }

        private CheckButtonToShowPopupExpectedValues mCheckButtonToShowPopupExpectedValues;

        public virtual ClickToShowDropdownParams ClickToShowDropdownParams
        {
            get
            {
                if ((this.mClickToShowDropdownParams == null))
                {
                    this.mClickToShowDropdownParams = new ClickToShowDropdownParams();
                }
                return this.mClickToShowDropdownParams;
            }
        }

        private ClickToShowDropdownParams mClickToShowDropdownParams;

        public virtual CheckDropDownValuesExpectedValues CheckDropDownValuesExpectedValues
        {
            get
            {
                if ((this.mCheckDropDownValuesExpectedValues == null))
                {
                    this.mCheckDropDownValuesExpectedValues = new CheckDropDownValuesExpectedValues();
                }
                return this.mCheckDropDownValuesExpectedValues;
            }
        }

        private CheckDropDownValuesExpectedValues mCheckDropDownValuesExpectedValues;

        public virtual CheckCloseButton_SysCheckWindowExpectedValues CheckCloseButton_SysCheckWindowExpectedValues
        {
            get
            {
                if ((this.mCheckCloseButton_SysCheckWindowExpectedValues == null))
                {
                    this.mCheckCloseButton_SysCheckWindowExpectedValues = new CheckCloseButton_SysCheckWindowExpectedValues();
                }
                return this.mCheckCloseButton_SysCheckWindowExpectedValues;
            }
        }

        private CheckCloseButton_SysCheckWindowExpectedValues mCheckCloseButton_SysCheckWindowExpectedValues;

        public virtual OpenAdministrationRulesetParams OpenAdministrationRulesetParams
        {
            get
            {
                if ((this.mOpenAdministrationRulesetParams == null))
                {
                    this.mOpenAdministrationRulesetParams = new OpenAdministrationRulesetParams();
                }
                return this.mOpenAdministrationRulesetParams;
            }
        }

        private OpenAdministrationRulesetParams mOpenAdministrationRulesetParams;


        public virtual CheckCauseCodesAndCloseWindowNewParams CheckCauseCodesAndCloseWindowNewParams
        {
            get
            {
                if ((this.mCheckCauseCodesAndCloseWindowNewParams == null))
                {
                    this.mCheckCauseCodesAndCloseWindowNewParams = new CheckCauseCodesAndCloseWindowNewParams();
                }
                return this.mCheckCauseCodesAndCloseWindowNewParams;
            }
        }

        private CheckCauseCodesAndCloseWindowNewParams mCheckCauseCodesAndCloseWindowNewParams;


        public virtual CheckRosterplanFixedPaymentsGridDataExpectedValues CheckRosterplanFixedPaymentsGridDataExpectedValues
        {
            get
            {
                if ((this.mCheckRosterplanFixedPaymentsGridDataExpectedValues == null))
                {
                    this.mCheckRosterplanFixedPaymentsGridDataExpectedValues = new CheckRosterplanFixedPaymentsGridDataExpectedValues();
                }
                return this.mCheckRosterplanFixedPaymentsGridDataExpectedValues;
            }
        }

        private CheckRosterplanFixedPaymentsGridDataExpectedValues mCheckRosterplanFixedPaymentsGridDataExpectedValues;
    }
    public static class PlanNames
    {
        public static String MainPlan { get { return "LØNNSBEREGNINGER"; } }
        public static String CopiedPlan { get { return "LØNNSBEREGNINGER_KOPI"; } }
        public static String HelpPlan { get { return "HjelpeplanLØNNSBEREGNINGER"; } }
        public static String EqualizationPlan { get { return "UTJEVNINGSVAKTER"; } }
        public static String EqualizationHelpPlan { get { return "Hjelpeplan for UTJEVNINGSVAKTER"; } }
        public static String FixedPaymentsPlan { get { return "FASTE TILLEGG"; } }
        public static String ExtendeWorktimePlan { get { return "UTA i rullerende plan"; } }
    }

    #region Class Parameters

    /// <summary>
    /// Parameters to be passed into 'ChangePeriodForAlLines_2412_MostHours'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ChangePeriodFoCurrentLines
    {
        #region Fields
        public List<String> GetDateList_92_913()
        {
            //Denne bør ligge i Excelark
            List<String> DateList = new List<String>();
            //Administrasjon | Regelsett kryss for ”Onsdag før skjærtorsdag er helligdag i turnusplanen” på regelsett TURNUS skal være huket av.
            DateList.Add("9.2.Date;24.12.2015");
            DateList.Add("9.3.Date;25.12.2015");
            DateList.Add("9.4.Date;26.12.2015");
            //Onsdag før skjærtorsdag
            DateList.Add("9.5.Date;01.04.2015");
            //Velg å kopiere planen til en hjelpeplan med startdato mandag etter palmesøndag og antall uker = 1.
            DateList.Add("9.6.Date;01.04.2015");
            //Gå til Administrasjon | Regelsett og fjern kryss for ”Onsdag før skjærtorsdag er helligdag i turnusplanen” på regelsett TURNUS.
            DateList.Add("9.7.Date;01.04.2015");
            //Gå på iverksett og velg datoen for Påskeaften.
            DateList.Add("9.8.Date;04.04.2015");
            //Gå på iverksett og velg dato 30.4
            DateList.Add("9.9.Date;30.04.2015");
            //Gå på iverksett og velg dato 1.5.
            DateList.Add("9.10.Date;01.05.2015");
            //Gå på iverksett og velg datoen for Pinseaften. 
            DateList.Add("9.11.Date;23.05.2015");
            //Åpne Ansatt vinduet i arbeidsplanen. Sett inn årsakskode for Timelønnsberegning, 
            //benytt 3-4 ulike årsakskoder, men se at alle linjer får en årsakskode 
            //(se ikon for Timelønnsberegning i kolonne ”T.b.”). Klikk Ok for å lagre.
            //Åpne Innstillinger å se at det IKKE er kryss for ”Nattevakter på startdagen”.
            //Gå på iverksett og velg dato 17.5. 
            DateList.Add("9.12.Date;17.05.2015");
            //Åpne Innstillinger og sett kryss for ”Nattevakter på startdagen”.
            DateList.Add("9.13.Date;17.05.2015");

            return DateList;
        }
        public List<String> GetDateList_914_919()
        {
            //Denne bør ligge i Excelark
            List<String> DateList = new List<String>();
            // Administrasjon | Regelsett. Sett kryss på ”Onsdag før skjærtorsdag er helligdag i turnusplanen” på regelsett TURNUS.

            //Gå på iverksett og velg dato 01.04. Ikke kryss for ”Nattevakter på startdagen”.
            DateList.Add("9.14.Date;01.04.2015");
            //Åpne Innstillinger og sett kryss for ”Nattevakter på startdagen”.
            DateList.Add("9.15.Date;01.04.2015");
            //Administrasjon | Regelsett. Ta bort kryss på ”Onsdag før skjærtorsdag er helligdag i turnusplanen” på regelsett TURNUS.
            //Gå på iverksett og velg dato 01.04. Ikke kryss for ”Nattevakter på startdagen”.
            DateList.Add("9.16.Date;01.04.2015");
            //Åpne Innstillinger og sett kryss for ”Nattevakter på startdagen”.
            DateList.Add("9.17.Date;01.04.2015");
            //Gå på iverksett og velg dato 01.04. Ikke kryss for ”Nattevakter på startdagen”.
            DateList.Add("9.18.Date;11.04.2015");
            //Åpne Innstillinger og sett kryss for ”Nattevakter på startdagen”.
            DateList.Add("9.19.Date;12.04.2015");

            return DateList;
        }

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectWedBeforeIsHolydayOnTurnus'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectWedBeforeIsHolydayOnTurnusParams
    {

        #region Fields
        /// <summary>
        /// Select 'Onsdag før skjærtorsdag er helligdag i turnusplane...' check box
        /// </summary>
        public bool UIOnsdagførskjærtorsdaCheckBoxChecked = true;
        #endregion
    }

    #endregion
    /// <summary>
    /// Parameters to be passed into 'InsertF4OnMondays'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class InsertF4OnMondaysParams
    {

        #region Fields
        /// <summary>
        /// Type 'Control + k' in 'gcRosterPlan' table
        /// </summary>
        public string UIGcRosterPlanTableSendKeys = "k";

        /// <summary>
        /// Type 'F' in 'gcRosterPlan' table
        /// </summary>
        public string UIGcRosterPlanTableSendKeys1 = "{F}{4}";
        /// <summary>
        /// Type '4' in 'gcRosterPlan' table
        /// </summary>
        //public string UIGcRosterPlanTableSendKeys2= "";

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Planning.RosterPlanning.DataStructures.PlanShift' in '[Row]1[Column]RosterCell_0' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEditValueTypeName = "Gatsoft.Gat.BusinessLogic.Planning.RosterPlanning.DataStructures.PlanShift";

        /// <summary>
        /// Type '' in '[Row]1[Column]RosterCell_0' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEditValueAsString = "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetStartdayToDisplay'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetStartdayToDisplayParams
    {

        #region Fields
        /// <summary>
        /// Type 'System.DateTime' in 'leDisplayStartDate' LookUpEdit
        /// </summary>
        public string UILeDisplayStartDateLookUpEditValueTypeName = "System.DateTime";

        /// <summary>
        /// Type '2011-05-16' in 'leDisplayStartDate' LookUpEdit
        /// </summary>
        public string UILeDisplayStartDateLookUpEditValueAsString = "2014-12-15";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ShowAllPlans'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ShowAllPlansParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Planning.RosterPlanning.RosterPlanList.RosterPlanFilterType' in 'cbRosterPlanFilter' LookUpEdit
        /// </summary>
        public string UICbRosterPlanFilterLookUpEditValueTypeName = "Gatsoft.Gat.BusinessLogic.Planning.RosterPlanning.RosterPlanList.RosterPlanFilter" +
            "Type";

        /// <summary>
        /// Type 'All' in 'cbRosterPlanFilter' LookUpEdit
        /// </summary>
        public string UICbRosterPlanFilterLookUpEditValueAsString = "All";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectStartDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectStartDateParams
    {

        #region Fields
        /// <summary>
        /// Type '2016-12-05' in 'leDisplayStartDate' LookUpEdit
        /// </summary>
        public string UILeDisplayStartDateLookUpEditValueAsString = "2016-12-05";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'UncheckStartDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class UncheckStartDateParams
    {

        #region Fields
        /// <summary>
        /// Clear 'chkDoCalculateDisplayAutomatically' check box
        /// </summary>
        public bool UIChkDoCalculateDisplaCheckBoxChecked = false;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckButtonToShowPopup'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckButtonToShowPopupExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ClassName' property of 'EditorButton0' button equals 'EditorButton'
        /// </summary>
        public string UIEditorButton0ButtonClassName = "EditorButton";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ClickToShowDropdown'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ClickToShowDropdownParams
    {

        #region Fields
        /// <summary>
        /// Type '2015-06-29' in 'leDisplayStartDate' LookUpEdit
        /// </summary>
        public string UILeDisplayStartDateLookUpEditValueAsString = "2015-06-29";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckDropDownValues'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckDropDownValuesExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ClassName' property of 'PopupLookUpEditForm' window equals 'PopupLookUpEditForm'
        /// </summary>
        public string UIPopupLookUpEditFormWindowClassName = "PopupLookUpEditForm";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'VelgIngen'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class VelgIngenExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ClassName' property of 'Velg ingen' button equals 'GSSimpleButton'
        /// </summary>
        public string UIVelgingenButtonClassName = "GSSimpleButton";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckCloseButton_SysCheckWindow'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckCloseButton_SysCheckWindowExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ControlType' property of 'Close' button equals 'Button'
        /// </summary>
        public string UICloseButtonControlType = "Button";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'OpenAdministrationRuleset'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class OpenAdministrationRulesetParams
    {

        #region Fields
        /// <summary>
        /// Type '{F3}' in 'Administrasjon' client
        /// </summary>
        public string UIAdministrasjonClientSendKeys = "{F3}";

        /// <summary>
        /// Type 'REGELSETT' in text box
        /// </summary>
        public string UIItemEditText = "REGELSETT";

        /// <summary>
        /// Type '{Enter}' in client
        /// </summary>
        public string UIItemClientSendKeys = "{Enter}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckCauseCodesAndCloseWindowNew'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckCauseCodesAndCloseWindowNewParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.RosterPlan.EmployeeManager.UI.ViewModels.Data.OvertimeCodeViewModel' in 'eOvertimeCode' LookUpEdit
        /// </summary>
        public string UIEOvertimeCodeViewModel = "Gatsoft.Gat.RosterPlan.EmployeeManager.UI.ViewModels.Data.OvertimeCodeViewModel";

        /// <summary>
        /// Type 'S - Sykevikar' in 'eOvertimeCode' LookUpEdit
        /// </summary>
        public string UIEOvertimeCodeSykevikar = "S - Sykevikar";

        /// <summary>
        /// Type 'F - Ferievikar' in 'eOvertimeCode' LookUpEdit
        /// </summary>
        public string UIEOvertimeFerieVikar = "F - Ferievikar";

        /// <summary>
        /// Type 'P - Permisjon' in 'eOvertimeCode' LookUpEdit
        /// </summary>
        public string UIEOvertimeCodePermisjon = "P - Permisjon";

        /// <summary>
        /// Type 'V - Vakans' in 'eOvertimeCode' LookUpEdit
        /// </summary>
        public string UIEOvertimeCodeVakans = "V - Vakans";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckRosterplanFixedPaymentsGridData'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckRosterplanFixedPaymentsGridDataExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Vaktkode, 1' PivotGridFieldValue equals 'Vaktkode, 1'
        /// </summary>
        public string UIVaktkode1PivotGridFieldValueValueAsString = "Vaktkode, 1";

        /// <summary>
        /// Verify that the 'Text' property of '24.10.2011' PivotGridFieldValue equals '24.10.2011'
        /// </summary>
        public string UIItem24102011PivotGridFieldValueText = "24.10.2011";

        /// <summary>
        /// Verify that the 'Text' property of '28.12.2014' PivotGridFieldValue equals '28.12.2014'
        /// </summary>
        public string UIItem28122014PivotGridFieldValueText = "28.12.2014";

        /// <summary>
        /// Verify that the 'ValueAsString' property of '8' PivotGridCell equals '8'
        /// </summary>
        public string UIItem8PivotGridCellValueAsString = "8";

        /// <summary>
        /// Verify that the 'ValueAsString' property of PivotGridCell equals ''
        /// </summary>
        public string UIItemPivotGridCellValueAsString = null;
        #endregion
    }
}