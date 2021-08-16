namespace CommonUIFunctions.UIMapVS2017Classes
{
    using System.Text.RegularExpressions;
    using System.CodeDom.Compiler;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Drawing;
    using System.Windows.Input;
    using System.IO;
    using CommonTestData;

    public partial class UIMapVS2017
    {
        private static TestContext TestContext
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
        private static TestContext testContextInstance;



        #region Login Functions

        public void InsertServerName(string serverName)
        {
            #region Variable Declarations
            WinEdit uIItem1SelectorenterasEdit = this.UIDataLinkPropertiesWindow.UIItemWindow.UIItem1SelectorenterasEdit;
            #endregion

            Keyboard.SendKeys(uIItem1SelectorenterasEdit, serverName + "{TAB}");
        }
        #endregion

        #region Absence Functions

        /// <summary>
        /// SelectAbsenceCode - Use 'SelectAbsenceCodeParams' to pass parameters into this method.
        /// </summary>
        public void SelectAbsenceCode(string absenceCode)
        {
            #region Variable Declarations
            DXLookUpEdit uILueAbsenceCodesLookUpEdit = this.UIFraværsregistreringWindow.UINbcLeftNavBar.UINbgcAbsenceCodesScrollableControl.UILueAbsenceCodesLookUpEdit;
            DXTextEdit uITeFindEdit = this.UIFraværsregistreringWindow.UINbcLeftNavBar.UINbgcAbsenceCodesScrollableControl.UILueAbsenceCodesLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            #endregion

            Mouse.Click(uILueAbsenceCodesLookUpEdit);
            absenceCode = "+" + absenceCode;
            uITeFindEdit.ValueAsString = absenceCode;
            Keyboard.SendKeys(uITeFindEdit, "{ENTER}");
            //Keyboard.SendKeys(uITeFindEdit, absenceCode + "{ENTER}");
        }
        /// <summary>
        /// SetAbsencePeriod - Use 'SetAbsencePeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetAbsencePeriod(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIFraværsregistreringWindow.UINbcLeftNavBar.UINgbcPeriodScrollableControl.UIPnlPeriodClient.UISdePeriodFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIFraværsregistreringWindow.UINbcLeftNavBar.UINgbcPeriodScrollableControl.UIPnlPeriodClient.UISdePeriodToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, "{Tab}", ModifierKeys.None);
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, "{Tab}", ModifierKeys.None);
            }
        }
        /// <summary>
        /// HourlyAbsence - Use 'HourlyAbsenceParams' to pass parameters into this method.
        /// </summary>
        public void HourlyAbsence(string fromTime, string toTime, string hours, string alias)
        {
            #region Variable Declarations
            DXCheckBox uIChbPabsHourlyAbsenceCheckBox = this.UIFraværsregistreringWindow.UIPnlAbsenceInformatioClient.UIGrcInformationClient.UINbcInformationNavBar.UINbgcHourAbsenceScrollableControl.UIPnlHourAbsenceClient.UIChbPabsHourlyAbsenceCheckBox;
            DXTextEdit uIETimeEdit = this.UIFraværsregistreringWindow.UIPnlAbsenceInformatioClient.UIGrcInformationClient.UINbcInformationNavBar.UINbgcHourAbsenceScrollableControl.UIPnlHourAbsenceClient.UIPnlHourAbsenceTimeClient.UIPnlPabsFromTimeClient.UIETimeEdit;
            DXTextEdit uIETimeEdit1 = this.UIFraværsregistreringWindow.UIPnlAbsenceInformatioClient.UIGrcInformationClient.UINbcInformationNavBar.UINbgcHourAbsenceScrollableControl.UIPnlHourAbsenceClient.UIPnlHourAbsenceTimeClient.UIPnlPabsToDateClient.UIETimeEdit;
            DXTextEdit uIENumberEdit = this.UIFraværsregistreringWindow.UIPnlAbsenceInformatioClient.UIGrcInformationClient.UINbcInformationNavBar.UINbgcHourAbsenceScrollableControl.UIPnlHourAbsenceClient.UIPnlHourAbsenceTimeClient.UIENumberEdit;
            DXTextEdit uITxtHourAbsenceShiftCEdit = this.UIFraværsregistreringWindow.UIPnlAbsenceInformatioClient.UIGrcInformationClient.UINbcInformationNavBar.UINbgcHourAbsenceScrollableControl.UIPnlHourAbsenceClient.UITxtHourAbsenceShiftCEdit;
            #endregion

            // Select 'chbPabsHourlyAbsence' check box
            uIChbPabsHourlyAbsenceCheckBox.Checked = true;
            Keyboard.SendKeys(uIChbPabsHourlyAbsenceCheckBox, "{Tab}", ModifierKeys.None);

            if (fromTime != "")
                Keyboard.SendKeys(uIETimeEdit, fromTime + "{Tab}", ModifierKeys.None);
            if (toTime != "")
                Keyboard.SendKeys(uIETimeEdit1, toTime + "{Tab}", ModifierKeys.None);

            if (hours != "")
                Keyboard.SendKeys(uIENumberEdit, hours + "{Tab}", ModifierKeys.None);

            if (alias != "")
                Keyboard.SendKeys(uITxtHourAbsenceShiftCEdit, alias + "{Tab}", ModifierKeys.None);
        }
        /// <summary>
        /// AbsenceRate - Use 'AbsenceRateParams' to pass parameters into this method.
        /// </summary>
        public void AbsenceRate(string rate)
        {
            #region Variable Declarations
            DXRadioGroup uIRgPartialAbsenceTypeRadioGroup = this.UIFraværsregistreringWindow.UIPnlAbsenceInformatioClient.UIGrcInformationClient.UINbcInformationNavBar.UINbgcPartialAbsenceScrollableControl.UIPnlPartialAbsenceClient.UIRgPartialAbsenceTypeRadioGroup;
            DXTextEdit uIENumber0Edit = this.UIFraværsregistreringWindow.UIPnlAbsenceInformatioClient.UIGrcInformationClient.UINbcInformationNavBar.UINbgcPartialAbsenceScrollableControl.UIPnlPartialAbsenceClient.UIENumber0Edit;
            DXTextEdit uIENumber1Edit = this.UIFraværsregistreringWindow.UIPnlAbsenceInformatioClient.UIGrcInformationClient.UINbcInformationNavBar.UINbgcPartialAbsenceScrollableControl.UIPnlPartialAbsenceClient.UIENumber1Edit;
            #endregion

            // Type '1' in 'rgPartialAbsenceType' RadioGroup
            //SelectedIndex
            uIRgPartialAbsenceTypeRadioGroup.SelectedIndex = 1;

            if (rate != "")
                Keyboard.SendKeys(uIENumber0Edit, rate + "{Tab}", ModifierKeys.None);

        }

        public void CheckCompatibilityWindowWhenExportToExcel()
        {
            Playback.Wait(2000);
            if (UIMicrosoftExcelWindow.Exists)
            {
                try
                {
                    ClickYesInCompatibilityWindowWhenExportToExcel();
                }
                catch (Exception)
                {
                    Keyboard.SendKeys(UIMicrosoftExcelWindow, "y", ModifierKeys.Alt);
                }
            }
        }

        #endregion

        #region Extra Functions

        public void SetExtraShiftPeriod(string fromDate, string toDate)
        {
            #region Variable Declarations
            DXTextEdit uIETime3Edit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UIETime3Edit;
            DXTextEdit uIETime1Edit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UIETime1Edit;
            #endregion

            uIETime3Edit.ValueAsString = fromDate;
            Keyboard.SendKeys(uIETime3Edit, "{Tab}");

            uIETime1Edit.ValueAsString = toDate;
            Keyboard.SendKeys(uIETime1Edit, "{Tab}");
        }

        public void ClickOkInRecalculationWindow()
        {
            #region Variable Declarations
            var recalcWindow = UIRekalkuleringWindow;
            recalcWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Rekalkulering";
            DXButton uIOKButton = recalcWindow.UIDbFooterCustom.UIOKButton;
            uIOKButton.SearchProperties[DXTestControl.PropertyNames.Name] = "btnOk";
            #endregion

            // Click 'OK' button
            Mouse.Click(uIOKButton);
        }

        public void SelectShitfBookColumnExtra(string column)
        {
            #region Variable Declarations
            DXLookUpEdit uICbCrewColumnLookUpEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UICbCrewColumnLookUpEdit;
            #endregion

            Mouse.Click(uICbCrewColumnLookUpEdit);
            Keyboard.SendKeys(uICbCrewColumnLookUpEdit, column);
            Keyboard.SendKeys(uICbCrewColumnLookUpEdit, "{Tab}");
        }

        #endregion

        #region Callout Functions

        public void SetCalloutCause(string cause)
        {
            #region Variable Declarations
            DXLookUpEdit uI_cbCauseCodeLookUpEdit = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UI_navBarNavBar.UINavBarGroupControlCoScrollableControl.UI_cbCauseCodeLookUpEdit;
            #endregion

            Mouse.Click(uI_cbCauseCodeLookUpEdit);
            Keyboard.SendKeys(uI_cbCauseCodeLookUpEdit, cause);
            Keyboard.SendKeys(uI_cbCauseCodeLookUpEdit, "{Tab}");
        }

        public void SetCalloutPeriod(string from, string to)
        {
            #region Variable Declarations
            var UtrykningsWindow = UIUtrykningWindow;
            UtrykningsWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Utrykning";
            int f = 0;
            int t = 0;
            Playback.Wait(1500);
            DXTextEdit uIETime0Edit = UtrykningsWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem5LayoutControlItem.UI_tcRightTabList.UI_tpCalloutDetailsClient.UI_periodCreatorCustom.UIETime0Edit;
            uIETime0Edit.SearchProperties[DXTestControl.PropertyNames.Name] = "eTime[0]";
            DXTextEdit uIETime1Edit = UtrykningsWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem5LayoutControlItem.UI_tcRightTabList.UI_tpCalloutDetailsClient.UI_periodCreatorCustom.UIETime1Edit;
            uIETime1Edit.SearchProperties[DXTestControl.PropertyNames.Name] = "eTime[1]";
            #endregion

            while (uIETime0Edit.ValueAsString != from && f < 3)
            {
                uIETime0Edit.ValueAsString = from;
                Keyboard.SendKeys(uIETime0Edit, "{Tab}");
                f++;
                Playback.Wait(1000);
            }

            while (uIETime1Edit.ValueAsString != to && t < 3)
            {
                uIETime1Edit.ValueAsString = to;
                Keyboard.SendKeys(uIETime1Edit, "{Tab}");
                Playback.Wait(1000);
                t++;
            }
        }

        public void SetTimeoff(string timeOff)
        {
            #region Variable Declarations
            DXTextEdit uIENumberEdit = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem5LayoutControlItem.UI_tcRightTabList.UI_tpCalloutDetailsClient.UIENumberEdit;
            DXTestControl uI_layoutControlCustom = this.UIUtrykningWindow.UI_layoutControlCustom;
            #endregion

            uIENumberEdit.ValueAsString = timeOff;
            Keyboard.SendKeys(uIENumberEdit, "{Tab}");
        }

        #endregion

        #region Filter Functions
        public void SelectShowInactiveLinesInFilter()
        {
            #region Variable Declarations
            DXRibbonEditItem uIDdlFilterRibbonEditItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIDdlFilterRibbonEditItem;
            uIDdlFilterRibbonEditItem.SearchProperties[DXTestControl.PropertyNames.Name] = "ddlFilter";
            DXTestControl uINode1TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode2TreeListNode.UINode1TreeListNode.UINode1TreeListNodeCheckBox;
            uINode1TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node1";
            #endregion

            Mouse.Click(uIDdlFilterRibbonEditItem, new Point(158, 10));
            Playback.Wait(1000);
            Mouse.Click(uINode1TreeListNodeCheckBox);
        }
        public void SelectViewFilter(string filterName)
        {
            SelectFilter(filterName);
        }

        public void CreateCustomFilter(string name, int[] values, bool openCustumFilterWindow = true)
        {
            if (openCustumFilterWindow)
                SelectCustomFilter();

            SelectNewCustomFilter();
            InsertFilterContent(name, values);
            ClickOkAndUseNewCustomFilter();
        }

        private void InsertFilterContent(string name, int[] values)
        {
            #region Variable Declarations
            DXTextEdit uITxtNameEdit = this.UITilpassetvisningWindow.UIGcSelectionClient.UITxtNameEdit;
            DXCheckedListBox uIChkListColumnsCheckedListBox = this.UITilpassetvisningWindow.UIGcSelectionClient.UIChkListColumnsCheckedListBox;
            DXComboBox uICbeSumFilterComboBox = this.UITilpassetvisningWindow.UIGcSelectionClient.UICbeSumFilterComboBox;
            DXCheckBox uIChkDisplaySummaryLinCheckBox = this.UITilpassetvisningWindow.UIGcSelectionClient.UIChkDisplaySummaryLinCheckBox;
            DXCheckBox uIChkShowExtendedWeeksCheckBox = this.UITilpassetvisningWindow.UIGcSelectionClient.UIChkShowExtendedWeeksCheckBox;
            DXCheckBox uIChkHighlightWeekendsCheckBox = this.UITilpassetvisningWindow.UIGcSelectionClient.UIChkHighlightWeekendsCheckBox;
            DXRadioGroup uIRgHighlightModeRadioGroup = this.UITilpassetvisningWindow.UIGcSelectionClient.UIRgHighlightModeRadioGroup;
            #endregion

            // Type 'Skrive inn navn' in 'txtName' text box
            //ValueAsString
            uITxtNameEdit.ValueAsString = name;
            Keyboard.SendKeys(uITxtNameEdit, "{TAB}");

            //CheckedIndices
            uIChkListColumnsCheckedListBox.CheckedIndices = values;

            #region Optional settings

            //// Select 'Gatsoft.Gat.BusinessLogic.Planning.RosterPlanning.ColumnSetup.PlanSumFilterOptionItem' in 'cbeSumFilter' combo box
            ////ValueTypeName
            //uICbeSumFilterComboBox.ValueTypeName = this.InsertFilterContentParams.UICbeSumFilterComboBoxValueTypeName;

            //// Select 'Vis for linje og totalt [SelectionStart]0' in 'cbeSumFilter' combo box
            ////ValueAsString
            //uICbeSumFilterComboBox.ValueAsString = this.InsertFilterContentParams.UICbeSumFilterComboBoxValueAsString;

            //// Select 'chkDisplaySummaryLine' check box
            //uIChkDisplaySummaryLinCheckBox.Checked = this.InsertFilterContentParams.UIChkDisplaySummaryLinCheckBoxChecked;

            //// Select 'chkShowExtendedWeeks' check box
            //uIChkShowExtendedWeeksCheckBox.Checked = this.InsertFilterContentParams.UIChkShowExtendedWeeksCheckBoxChecked;

            //// Select 'chkHighlightWeekends' check box
            //uIChkHighlightWeekendsCheckBox.Checked = this.InsertFilterContentParams.UIChkHighlightWeekendsCheckBoxChecked;

            //// Type '0' in 'rgHighlightMode' RadioGroup
            ////SelectedIndex
            //uIRgHighlightModeRadioGroup.SelectedIndex = this.InsertFilterContentParams.UIRgHighlightModeRadioGroupSelectedIndex;

            //// Clear 'chkHighlightWeekends' check box
            //uIChkHighlightWeekendsCheckBox.Checked = this.InsertFilterContentParams.UIChkHighlightWeekendsCheckBoxChecked1;

            //// Clear 'chkShowExtendedWeeks' check box
            //uIChkShowExtendedWeeksCheckBox.Checked = this.InsertFilterContentParams.UIChkShowExtendedWeeksCheckBoxChecked1;

            //// Clear 'chkDisplaySummaryLine' check box
            //uIChkDisplaySummaryLinCheckBox.Checked = this.InsertFilterContentParams.UIChkDisplaySummaryLinCheckBoxChecked1;

            #endregion
        }

        private void ClickOkAndUseNewCustomFilter()
        {
            ClickOkNewCustomFilter();
            ClickUseNewCustomFilter();
        }
        private void SelectFilter(string filter)
        {
            #region Variable Declarations
            DXRibbonEditItem uIBeiRosterPlanColumnSRibbonEditItem = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIBeiRosterPlanColumnSRibbonEditItem;
            #endregion
            Mouse.Click(uIBeiRosterPlanColumnSRibbonEditItem);
            Keyboard.SendKeys(filter + "{ENTER}");
        }

        public void SearchInEmpFilterInEditFilterWindow(string search)
        {
            #region Variable Declarations
            DXLookUpEdit uIGleSavedFiltersLookUpEdit = this.UIAnsattfilterWindow.UIGleSavedFiltersLookUpEdit;
            //DXGrid uIGridControlTable = this.UIAnsattfilterWindow.UIGleSavedFiltersLookUpEdit.UIPopupGridLookUpEditFWindow.UIGridControlTable;
            //DXGrid uIGcEmployeesTable = this.UIAnsattfilterWindow.UIPanelControl1Client.UIViewHost1Custom.UIPcViewClient.UIEmployeeFilterEditViCustom.UIGcEmployeesTable;
            #endregion

            Keyboard.SendKeys(uIGleSavedFiltersLookUpEdit, search + "{TAB}");
        }

        //public void ExpandAdministrationTreeList()
        //{
        //    #region Variable Declarations
        //    DXTestControl uINode1TreeListNodeButton = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIAdministrationViewCustom.UIPanelControl1Client.UITlOptionsTreeList.UINode1TreeListNode.UINode1TreeListNodeButton;
        //    DXMenuBaseButtonItem uIFullExpandMenuBaseButtonItem = this.UIItemWindow12.UIPopupMenuBarControlMenu.UIFullExpandMenuBaseButtonItem;
        //    #endregion

        //    try
        //    {
        //        if (uINode1TreeListNodeButton.Exists)
        //        {
        //            // Right-Click 'Node1' TreeListNodeButton
        //            Mouse.Click(uINode1TreeListNodeButton, MouseButtons.Right, ModifierKeys.None, new Point(4, 6));

        //            // Click 'Full Expand' MenuBaseButtonItem
        //            Mouse.Click(uIFullExpandMenuBaseButtonItem, new Point(64, 11));
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        TestContext.WriteLine("Node not found");
        //    }
        //}
        #endregion

        #region Role Assignment Functions
        public void SummarySettingsSelectSumBehovRoleAssignment(string filter)
        {
            #region Variable Declarations
            DXLookUpEdit uILeSumDemandDataSourcLookUpEdit = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIDpSummaryDockPanel.UIDockPanel1_ContainerCustom.UISumDemandHostViewCustom.UIViewHost1Custom.UIPcViewClient.UIPanFilterClient.UILeSumDemandDataSourcLookUpEdit;
            #endregion

            Mouse.Click(uILeSumDemandDataSourcLookUpEdit);
            Keyboard.SendKeys(uILeSumDemandDataSourcLookUpEdit, filter);
            Keyboard.SendKeys(uILeSumDemandDataSourcLookUpEdit, "{Enter}");
        }
        public void ClickCompTabInRoleAssignmentDetails()
        {
            #region Variable Declarations
            var RoleWindow = this.UIOppgavetildelingWindow;
            RoleWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXTestControl uIXtraTabControlHeaderTabPage = RoleWindow.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIXtraTabControlHeaderTabPage;
            #endregion

            // Click 'XtraTabControlHeader' tab
            Mouse.Click(uIXtraTabControlHeaderTabPage);
        }

        public void ClickAddRoleAssignmentInDetailsWindow()
        {
            #region Variable Declarations
            UIOppgavetildelingWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXButton uILeggtilButton = this.UIOppgavetildelingWindow.UIGcContentClient.UILeggtilButton;
            #endregion

            // Click 'Legg til' button
            Mouse.Click(uILeggtilButton, new Point(1, 1));
        }
        public void ClickContTabInRoleAssignmentDetails()
        {
            #region Variable Declarations
            var RoleWindow = this.UIOppgavetildelingWindow;
            RoleWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXTestControl uIXtraTabControlHeaderTabPage1 = RoleWindow.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIXtraTabControlHeaderTabPage1;
            #endregion

            // Click 'XtraTabControlHeader' tab
            Mouse.Click(uIXtraTabControlHeaderTabPage1);
        }

        public void SelectResetShiftbookInRoleAssignmentShiftbook()
        {
            #region Variable Declarations
            DXButton uIGSDropDownButtonButton = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraPanelScrollableControl.UIGSDropDownButtonButton;
            DXMenuBaseButtonItem uITilbakestillvaktbokMenuBaseButtonItem = this.UIItemWindow12.UIPopupMenuBarControlMenu.UITilbakestillvaktbokMenuBaseButtonItem;
            uIGSDropDownButtonButton.SearchProperties[DXTestControl.PropertyNames.Name] = "topPanelXtraPanel[0]GSDropDownButton[0]";
            uITilbakestillvaktbokMenuBaseButtonItem.SearchProperties[DXTestControl.PropertyNames.Name] = "BarButtonItemLink[0]";
            #endregion

            //uIGSDropDownButtonButton.DrawHighlight();

            var x = uIGSDropDownButtonButton.BoundingRectangle.Width - 10;
            var y = uIGSDropDownButtonButton.BoundingRectangle.Height - 10;

            // Click 'GSDropDownButton' button
            Mouse.Click(new Point(uIGSDropDownButtonButton.BoundingRectangle.X + x, uIGSDropDownButtonButton.BoundingRectangle.Y + y));

            //uITilbakestillvaktbokMenuBaseButtonItem.DrawHighlight();
            // Click 'Tilbakestill vaktbok' MenuBaseButtonItem
            Mouse.Click(uITilbakestillvaktbokMenuBaseButtonItem);
        }

        /// <summary>
        /// SelectRoleAccountingDetails - Use 'SelectRoleAccountingDetailsParams' to pass parameters into this method.
        /// </summary>
        public void SelectRoleAccountingDetails(string costDep, string project)
        {
            #region Variable Declarations
            DXTextEdit uITeFindEdit = this.UIOppgavetildelingWindow.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIViewTabPageClient.UIRoleAccountingViewCustom.UILeCostPlaceLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            DXCell selectCostCell = this.UIOppgavetildelingWindow.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIViewTabPageClient.UIRoleAccountingViewCustom.UILeCostPlaceLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILciGridLayoutControlItem.UIGridControlTable.UIItem5000ARBEIDSPLANKCell;
            DXLookUpEdit uILeProjectLookUpEdit = this.UIOppgavetildelingWindow.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIViewTabPageClient.UIRoleAccountingViewCustom.UILeProjectLookUpEdit;
            DXTextEdit uITeFindEdit1 = this.UIOppgavetildelingWindow.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIViewTabPageClient.UIRoleAccountingViewCustom.UILeProjectLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            DXCell selectProjectCell = this.UIOppgavetildelingWindow.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIViewTabPageClient.UIRoleAccountingViewCustom.UILeProjectLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILciGridLayoutControlItem.UIGridControlTable.UIP1Prosjekt1Cell;
            DXLookUpEdit uILeCostPlaceLookUpEdit = this.UIOppgavetildelingWindow.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UITabControlViewHost1TabList.UIViewTabPageClient.UIRoleAccountingViewCustom.UILeCostPlaceLookUpEdit;
            #endregion

            ClickContTabInRoleAssignmentDetails();

            if (!String.IsNullOrEmpty(costDep))
            {
                Mouse.Click(uILeCostPlaceLookUpEdit);
                Keyboard.SendKeys(uITeFindEdit, costDep);
                //// Click Cost cell
                //Mouse.Click(selectCostCell);
                Playback.Wait(500);
                Keyboard.SendKeys("{DOWN}{ENTER}");
            }

            if (!String.IsNullOrEmpty(project))
            {
                Mouse.Click(uILeProjectLookUpEdit);
                Keyboard.SendKeys(uITeFindEdit1, project);
                //// Click Prosjekt cell
                //Mouse.Click(selectProjectCell);
                Playback.Wait(500);
                Keyboard.SendKeys("{DOWN}{ENTER}");
            }
        }
        public void AddRemoveRoleRoleToAssignment(bool add)
        {
            #region Variable Declarations
            DXButton uILeggtilButton = this.UIOppgavedelingWindow.UIGroupControl1Client.UILeggtilButton;
            DXButton uIFjernButton = this.UIOppgavedelingWindow.UIGroupControl1Client.UIFjernButton;
            #endregion

            if (add)
            {
                //Legg til oppgave som skal deles
                Mouse.Click(uILeggtilButton);
            }
            else
            {
                //Fjern til oppgave som skal deles
                Mouse.Click(uIFjernButton);
            }
        }
        public void AddRemoveDepToAssignTo(bool add)
        {
            #region Variable Declarations
            DXButton uILeggtilButton = this.UIOppgavedelingWindow.UIGroupControl2Client.UILeggtilButton;
            DXButton uIFjernButton = this.UIOppgavedelingWindow.UIGroupControl2Client.UIFjernButton;
            #endregion

            if (add)
            {
                //Legg til avdeling som skal deles til
                Mouse.Click(uILeggtilButton);
            }
            else
            {
                //Fjern til avdeling som skal deles til
                Mouse.Click(uIFjernButton);
            }
        }
        public void OkCancelAssignNewRole(bool ok)
        {
            #region Variable Declarations
            DXButton uIOKButton = this.UIOppgavedelingWindow.UIOKButton;
            DXButton uIAvbrytButton = this.UIOppgavedelingWindow.UIAvbrytButton;
            #endregion

            if (ok)
            {
                // Click 'Ok' button
                Mouse.Click(uIOKButton);
            }
            else
            {
                // Click 'Avbryt' button
                Mouse.Click(uIAvbrytButton);
            }
        }
        public void AssignNewRoleFromSubTabRoleAssignment(DateTime fromDate, DateTime? toDate, int tilgang, string comment)
        {
            #region Variable Declarations
            DXDateTimePicker uISdeFromDateDateTimeEdit = this.UIOppgavedelingWindow.UIPanelControl1Client.UISdeFromDateDateTimeEdit;
            DXDateTimePicker uISdeToDateDateTimeEdit = this.UIOppgavedelingWindow.UIPanelControl1Client.UISdeToDateDateTimeEdit;
            DXRadioGroup uIRadioGroupRadioGroup = this.UIOppgavedelingWindow.UIPanelControl1Client.UIRadioGroupRadioGroup;
            DXTextEdit uIMeCommentEdit = this.UIOppgavedelingWindow.UIPanelControl1Client.UIMeCommentEdit;
            #endregion

            // Fra dato
            if (fromDate != null)
                uISdeFromDateDateTimeEdit.ValueAsString = fromDate.ToString("yyyy-MM-dd");

            // Til dato     
            if (toDate != null)
                uISdeToDateDateTimeEdit.ValueAsString = toDate.Value.ToString("yyyy-MM-dd");

            // tilgang
            uIRadioGroupRadioGroup.SelectedIndex = tilgang;

            // Kommentar
            if (!String.IsNullOrEmpty(comment))
                uIMeCommentEdit.ValueAsString = comment;
        }


        /// <summary>
        /// SelectRoleToAssign - Use 'SelectRoleToAssignParams' to pass parameters into this method.
        /// </summary>
        public void SelectRoleToAssign(string dep, string role, bool ok)
        {
            #region Variable Declarations
            DXLookUpEdit uITlueDepartmentsLookUpEdit = this.UIVelgoppgaverWindow.UITlueDepartmentsLookUpEdit;
            DXMRUEdit uITeFindMRUEdit = this.UIVelgoppgaverWindow.UITlueDepartmentsLookUpEdit.UITreeListLookUpEditPoWindow.UITreeListLookUpEdit1TTreeList.UIFindControlCoreCustom.UILayoutControl1Custom.UIRootLayoutGroup.UILciFindLayoutControlItem.UITeFindMRUEdit;
            DXTreeList uITreeListLookUpEdit1TTreeList = this.UIVelgoppgaverWindow.UITlueDepartmentsLookUpEdit.UITreeListLookUpEditPoWindow.UITreeListLookUpEdit1TTreeList;
            DXMRUEdit uITeFindMRUEdit1 = this.UIVelgoppgaverWindow.UIGcRolesTable.UIFindControlCoreCustom.UILayoutControl1Custom.UIRootLayoutGroup.UILciFindLayoutControlItem.UITeFindMRUEdit;
            DXTestControl uILayoutControl1Custom = this.UIVelgoppgaverWindow.UIGcRolesTable.UIFindControlCoreCustom.UILayoutControl1Custom;
            DXGrid uIGcRolesTable = this.UIVelgoppgaverWindow.UIGcRolesTable;
            DXButton uISøkButton = this.UIVelgoppgaverWindow.UIGcRolesTable.UIFindControlCoreCustom.UILayoutControl1Custom.UIRootLayoutGroup.UILciFindButtonLayoutControlItem.UISøkButton;
            DXCell firstRowInList = this.UIVelgoppgaverWindow.UIGcRolesTable.UIDeltmed47204810TirsdCell;
            DXButton uIOKButton = this.UIVelgoppgaverWindow.UIOKButton;
            DXButton uIAvbrytButton = this.UIVelgoppgaverWindow.UIAvbrytButton;
            //// Type '47' in 'teFind' MRUEdit
            //Keyboard.SendKeys(uITeFindMRUEdit, "4620 - Kirurgi");

            //// Type '{Enter}' in 'treeListLookUpEdit1TreeList' TreeList
            //Keyboard.SendKeys(uITreeListLookUpEdit1TTreeList, "{ENTER}");

            //uITeF
            //// Type '{Enter}' in 'gcRoles' table
            //Keyboard.SendKeys(uIGcRolesTable, "{ENTER}");

            //// Click 'Søk' button
            //Mouse.Click(uISøkButton);

            // Click 'Delt med 4720/4810 - Tirsdag' cellindMRUEdit1.ValueAsString = "";
            #endregion

            if (!String.IsNullOrEmpty(dep))
            {
                // Type '4620 - Kirurgi' in 'tlueDepartments' LookUpEdit
                Keyboard.SendKeys(uITlueDepartmentsLookUpEdit, dep);
                Keyboard.SendKeys(uITlueDepartmentsLookUpEdit, "{ENTER}");
            }

            // Type 'd' in 'layoutControl1' custom control
            Keyboard.SendKeys(uILayoutControl1Custom, role);

            Mouse.Click(firstRowInList);

            if (ok)
            {
                // Click 'Ok' button
                Mouse.Click(uIOKButton);
            }
            else
            {
                // Click 'Avbryt' button
                Mouse.Click(uIAvbrytButton);
            }
        }

        public void SelectDepToAssignTo(string dep, bool ok)
        {
            #region Variable Declarations
            DXMRUEdit uITeFindMRUEdit = this.UIVelgavdelingerWindow.UITlDepartmentsTreeList.UIFindControlCoreCustom.UILayoutControl1Custom.UIRootLayoutGroup.UILciFindLayoutControlItem.UITeFindMRUEdit;
            DXTestControl uILayoutControl1Custom = this.UIVelgavdelingerWindow.UITlDepartmentsTreeList.UIFindControlCoreCustom.UILayoutControl1Custom;
            DXTreeListCell uIItem4610OrtopediTreeListCell = this.UIVelgavdelingerWindow.UITlDepartmentsTreeList.UINode0TreeListNode.UINode9TreeListNode.UINode0TreeListNode.UIItem4610OrtopediTreeListCell;
            DXButton uIOKButton = this.UIVelgavdelingerWindow.UIOKButton;
            DXButton uIAvbrytButton = this.UIVelgavdelingerWindow.UIAvbrytButton;
            // Type '' in 'teFind' MRUEdit
            ////ValueAsString
            //uITeFindMRUEdit.ValueAsString = "4610 - Ortopedi";
            //// Click '4610 - Ortopedi' TreeListCell
            //Mouse.Click(uIItem4610OrtopediTreeListCell, new Point(125, 7));
            // Type '46' in 'layoutControl1' custom control
            #endregion

            Keyboard.SendKeys(uILayoutControl1Custom, dep);
            Keyboard.SendKeys(uILayoutControl1Custom, "{TAB}");
            ClickSearchDepToAssignTo();

            if (ok)
            {
                // Click 'Ok' button
                Mouse.Click(uIOKButton);
            }
            else
            {
                // Click 'Avbryt' button
                Mouse.Click(uIAvbrytButton, new Point(1, 1));
            }
        }
        #endregion

        #region Shiftbook Functions

        public void ChangePeriodInRoleAssignmentDetailsWindow(string from, string to)
        {
            #region Variable Declarations
            var roleWin = UIOppgavetildelingWindow;
            roleWin.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXTextEdit uISteFromTimeEdit = roleWin.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UISteFromTimeEdit;
            DXTextEdit uISteToTimeEdit = roleWin.UIGcContentClient.UIGcDetailsClient.UIVhDetailsCustom.UIPcViewClient.UIRoleDetailsViewCustom.UIPanelControl1Client.UISteToTimeEdit;
            #endregion

            //if (from!= "")
            //    Keyboard.SendKeys(uISteFromTimeEdit, from + "{Tab}");
            //if (to!= "")
            //    Keyboard.SendKeys(uISteToTimeEdit, to + "{Tab}");

            if (from != "")
            {
                uISteFromTimeEdit.ValueAsString = from;
                Keyboard.SendKeys(uISteFromTimeEdit, "{Tab}");
            }
            if (to != "")
            {
                uISteToTimeEdit.ValueAsString = to;
                Keyboard.SendKeys(uISteToTimeEdit, "{Tab}");
            }
        }
        public void CreateRoleAssignmentRolePattern(DateTime from, DateTime to)
        {
            #region Variable Declarations
            DXDateTimePicker uIDeFromDateDateTimeEdit = this.UITildeloppgavemønsterWindow.UIPanelControl1Client.UIViewHost1Custom.UIPcViewClient.UIRoleCodePatternAssigCustom.UIPnlTopClient.UIPnlTimePeriodClient.UIViewHost1Custom.UIPcViewClient.UIDatePeriodSelectorViCustom.UIPanelControl1Client.UIDeFromDateDateTimeEdit;
            DXDateTimePicker uIDeToDateDateTimeEdit = this.UITildeloppgavemønsterWindow.UIPanelControl1Client.UIViewHost1Custom.UIPcViewClient.UIRoleCodePatternAssigCustom.UIPnlTopClient.UIPnlTimePeriodClient.UIViewHost1Custom.UIPcViewClient.UIDatePeriodSelectorViCustom.UIPanelControl1Client.UIDeToDateDateTimeEdit;
            #endregion

            uIDeFromDateDateTimeEdit.DateTime = from;
            Keyboard.SendKeys(uIDeFromDateDateTimeEdit, "{Tab}");

            uIDeToDateDateTimeEdit.DateTime = to;
            Keyboard.SendKeys(uIDeToDateDateTimeEdit, "{Tab}");

        }
        public void CloseRoleAssignmentRolePattern(bool clickOk = true)
        {
            #region Variable Declarations
            DXButton uIAvbrytButton = this.UITildeloppgavemønsterWindow.UIPanelControl1Client.UIViewHost1Custom.UIPcViewClient.UIRoleCodePatternAssigCustom.UIPnlBottomClient.UIAvbrytButton;
            DXButton uIOKButton = this.UITildeloppgavemønsterWindow.UIPanelControl1Client.UIViewHost1Custom.UIPcViewClient.UIRoleCodePatternAssigCustom.UIPnlBottomClient.UIOKButton;
            #endregion

            if (clickOk)
                Mouse.Click(uIOKButton);
            else
                Mouse.Click(uIAvbrytButton);
        }
        public void ChangeRoleAssignWeekView(string fromWeek, string toWeek)
        {
            #region Variable Declarations
            DXRibbonEditItem uIBeFromWeekRibbonEditItem = this.UIOppgavetildelingWindow.UIRcMenuRibbon.UIRpgHomeRibbonPage.UIRpNavigationRibbonPageGroup.UIBeFromWeekRibbonEditItem;
            DXRibbonEditItem uIBeToWeekRibbonEditItem = this.UIOppgavetildelingWindow.UIRcMenuRibbon.UIRpgHomeRibbonPage.UIRpNavigationRibbonPageGroup.UIBeToWeekRibbonEditItem;
            DXPopupEdit uIGSWeekEditEditorPopupEdit = this.UIOppgavetildelingWindow.UIRcMenuRibbon.UIGSWeekEditEditorPopupEdit;
            #endregion

            if (fromWeek != "")
            {
                Mouse.Click(uIBeFromWeekRibbonEditItem);
                Keyboard.SendKeys(uIGSWeekEditEditorPopupEdit, "a", ModifierKeys.Control);
                Keyboard.SendKeys(uIGSWeekEditEditorPopupEdit, fromWeek + "{Tab}");
            }

            if (toWeek != "")
            {
                Mouse.Click(uIBeToWeekRibbonEditItem);
                Keyboard.SendKeys(uIGSWeekEditEditorPopupEdit, "a", ModifierKeys.Control);
                Keyboard.SendKeys(uIGSWeekEditEditorPopupEdit, toWeek + "{Tab}");
            }
        }
        public void ClickOkRoleAssignmentDetails()
        {
            #region Variable Declarations
            var win = this.UIOppgavetildelingWindow;
            win.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXButton uISimpleButtonButton = win.UISimpleButtonButton;
            #endregion

            // Click 'SimpleButton' button
            Mouse.Click(uISimpleButtonButton);
        }

        public void ClickCancelRoleAssignmentDetails()
        {
            #region Variable Declarations
            var win = this.UIOppgavetildelingWindow;
            win.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";
            DXButton uIAvbrytButton = win.UIAvbrytButton;
            #endregion

            // Click 'Avbryt' button
            Mouse.Click(uIAvbrytButton);
        }
        public void SelectShiftCodeInExtraWindow(string shiftCode)
        {
            #region Variable Declarations
            DXLookUpEdit uICbShiftCodeLookUpEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UICbShiftCodeLookUpEdit;
            #endregion

            Mouse.Click(uICbShiftCodeLookUpEdit);
            Keyboard.SendKeys(uICbShiftCodeLookUpEdit, shiftCode);
            Keyboard.SendKeys(uICbShiftCodeLookUpEdit, "{Enter}");
        }
        public void CreateNewShiftRemanage(DateTime shiftDate, string shiftCode)
        {
            #region Variable Declarations
            DXButton uINyendrevakterButton = UIForskyvningWindow.UIPanClientPanelClient.UIGpcMainClient.UITcClientTabList.UITpMainClient.UIGpcShiftsClient.UIGcNewShiftsClient.UINyendrevakterButton;
            DXDateTimePicker uIPceDateDateTimeEdit = UIRemanageCreateNewShiWindow.UIGsPanelControl1Client.UIMainPanelClient.UIDeDateCustom.UIPceDateDateTimeEdit;
            DXLookUpEdit uICbShiftCodeLookUpEdit = UIRemanageCreateNewShiWindow.UIGsPanelControl1Client.UIMainPanelClient.UICbShiftCodeLookUpEdit;
            DXButton uILeggtilButton = UIRemanageCreateNewShiWindow.UIGsPanelControl1Client.UILeggtilButton;
            DXButton uIOKButton = UIRemanageCreateNewShiWindow.UIGsPanelControl2Client.UIOKButton;
            #endregion

            // Click 'Ny / &endre vakter' button
            Mouse.Click(uINyendrevakterButton);

            uIPceDateDateTimeEdit.DateTime = shiftDate;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{Tab}");

            Mouse.Click(uICbShiftCodeLookUpEdit);
            Keyboard.SendKeys(uICbShiftCodeLookUpEdit, shiftCode + "{Enter}");

            // Click 'Legg til' button
            Mouse.Click(uILeggtilButton);

            // Click 'OK' button
            Mouse.Click(uIOKButton);
        }
        public bool CheckRecalculationWindowExists()
        {
            #region Variable Declarations
            var window = UIRekalkuleringWindow;
            #endregion

            return window.Exists;
        }
        public bool CheckNoNeedToRecalculateDialogExists()
        {
            #region Variable Declarations
            var diag = this.UIREC3001InformasjonWindow;
            #endregion

            return diag.Exists;
        }

        public void SelectFreeDepExchangeShifts(List<string> shiftRows)
        {
            #region Variable Declarations
            var vacantShiftsForm = UIVacantShiftsFormWindow;
            vacantShiftsForm.SearchProperties.Add(new PropertyExpression(DXTestControl.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            vacantShiftsForm.SearchProperties.Add(new PropertyExpression("ControlName", "VacantShiftsForm", PropertyExpressionOperator.Contains));
            vacantShiftsForm.SearchProperties.Add(new PropertyExpression(DXTestControl.PropertyNames.FriendlyName, "VacantShiftsForm", PropertyExpressionOperator.Contains));

            DXCell shiftCell = vacantShiftsForm.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem1LayoutControlItem.UIGUnoccupiedShiftsTable.UIIkkevalgtCell;
            DXCheckBox uICheckEditCheckBox = vacantShiftsForm.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem1LayoutControlItem.UIGUnoccupiedShiftsTable.UICheckEditCheckBox;
            DXButton uIOKButton = vacantShiftsForm.UIGsLayoutControl1Custom.UIOKButton;
            #endregion

            foreach (var shiftRow in shiftRows)
            {
                //shiftCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gUnoccupiedShiftsGridControlCell[View]gvUnoccupieedShifts[Row]" + shiftRow + "[Column]gcIsSelected";
                //Mouse.Click(shiftCell);

                uICheckEditCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "gUnoccupiedShiftsCheckEdit[View]gvUnoccupieedShifts[Row]" + shiftRow + "[Column]gcIsSelected";
                uICheckEditCheckBox.Checked = true;

                shiftCell.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
                uICheckEditCheckBox.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            }

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }

        #endregion

        #region DayRythmplan Functions

        public void SelectDayRythmPlan(string planName, bool openPlan = true, bool doubleClickOpenPlan = false)
        {
            #region Variable Declarations 
            var dayRythmPlanGrid = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIGcDayrhythmPlansTable;

            var selectCell = new DXCell();
            #endregion

            var view = dayRythmPlanGrid.Views[0];
            planName = planName.Trim().ToLower();
            for (int i = 0; i < view.RowCount; i++)
            {
                var val = view.GetCellValue("gcolPlan", i).ToString().Trim().ToLower();

                if (val == planName)
                {
                    selectCell = view.GetCell("gcolPlan", i);
                    selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcDayrhythmPlansGridControlCell[View]gvDayrhythmPlans[Row]" + i + "[Column]gcolPlan";
                    Playback.Wait(1000);
                    break;
                }
            }

            try
            {
                Playback.Wait(1000);
                Mouse.Click(selectCell);
            }
            catch (Exception)
            {
                TestContext.WriteLine("Error selecting plancell");
            }

            if (!doubleClickOpenPlan)
            {
                if (openPlan)
                {
                    try
                    {
                        EditDayRytmplan();
                    }
                    catch (Exception)
                    {
                        Mouse.DoubleClick(selectCell);
                    }
                }
            }
            else
                Mouse.DoubleClick(selectCell);

            Playback.Wait(3000);
        }
        public void AddTaskToDayRythmplanLayer(string row, string task, bool replace = false)
        {
            #region Variable Declarations            
            DXCell uIMorgenmøteCell = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccLayersScrollableControl.UIGcItemsTable.UIMorgenmøteCell;
            uIMorgenmøteCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcItemsGridControlCell[View]gvItems[Row]-2147483647[Column]cItemNameDayR";
            uIMorgenmøteCell.SearchProperties[DXTestControl.PropertyNames.ClassName] = "GridControlCell";
            DXWindow uIPopupLookUpEditFormWindow = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccLayersScrollableControl.UIGcItemsTable.UIRow2147483647ColumncLookUpEdit.UIPopupLookUpEditFormWindow;
            uIPopupLookUpEditFormWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "gcItemsLookUpEdit[View]gvItems[Row]-2147483647[Column]cItemNameDayRPopupForm";
            uIPopupLookUpEditFormWindow.SearchProperties[DXTestControl.PropertyNames.ClassName] = "PopupLookUpEditForm";
            DXLookUpEdit uIRow0ColumncItemNameDLookUpEdit = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccLayersScrollableControl.UIGcItemsTable.UIRow0ColumncItemNameDLookUpEdit;
            #endregion

            if (!replace)
            {
                Mouse.Click(uIMorgenmøteCell);
                Mouse.Click(uIPopupLookUpEditFormWindow, new Point(77, 30));
            }

            uIMorgenmøteCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcItemsLookUpEdit[View]gvItems[Row]" + row + "[Column]cItemNameDayR";
            uIMorgenmøteCell.SearchProperties[DXTestControl.PropertyNames.ClassName] = "LookUpEdit";

            Mouse.Click(uIMorgenmøteCell);

            Keyboard.SendKeys(uIMorgenmøteCell, task);
            Keyboard.SendKeys(uIRow0ColumncItemNameDLookUpEdit, "{Enter}");
        }

        public void SetTotalTaskRequirements(string amount)
        {
            #region Variable Declarations
            DXTextEdit uIGsseAmountEdit = this.UISettekravtilpersonerWindow.UIGrpRequirementsClient.UIGsseAmountEdit;
            #endregion

            uIGsseAmountEdit.ValueAsString = amount;
            Keyboard.SendKeys(uIGsseAmountEdit, "{TAB}");
        }

        public void CheckReserved(string row, bool check = true)
        {
            #region Variable Declarations
            DXCell uIIkkevalgtCell = this.UISettekravtilpersonerWindow1.UIGrpRequirementsClient.UIGcRequirementTable.UIIkkevalgtCell;
            DXCheckBox uICheckCheckBox = this.UISettekravtilpersonerWindow1.UIGrpRequirementsClient.UIGcRequirementTable.UICheckCheckBox;
            uIIkkevalgtCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRequirementGridControlCell[View]gvRequirement[Row]" + row + "[Column]gcolReserved";
            uICheckCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRequirementCheckEdit[View]gvRequirement[Row]" + row + "[Column]gcolReserved";
            #endregion

            // Select 'Check' check box
            uICheckCheckBox.Checked = check;
        }

        #endregion

        #region Rosterplan Functions

        public void SetRosterPlanName(string name)
        {
            #region Variable Declarations
            DXTextEdit uIENameEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIENameEdit;
            #endregion

            Keyboard.SendKeys(uIENameEdit, name + "{TAB}");
        }

        public void SelectRosterplanType(string type)
        {
            #region Variable Declarations
            DXLookUpEdit uILeRosterplanTypeLookUpEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UILeRosterplanTypeLookUpEdit;
            #endregion

            // Type '{Tab}' in 'leRosterplanType' LookUpEdit
            Keyboard.SendKeys(uILeRosterplanTypeLookUpEdit, type + "{Tab}");
        }

        public void SetRosterPlanWeeks(string weeks)
        {
            #region Variable Declarations
            DXTextEdit uIENumber1Edit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIENumber1Edit;
            #endregion

            Keyboard.SendKeys(uIENumber1Edit, weeks + "{TAB}", ModifierKeys.None);
        }

        public void SetRosterPlanWeekRotation(string weeks)
        {
            #region Variable Declarations
            DXTextEdit uIENumber0Edit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIENumber0Edit;
            #endregion

            Keyboard.SendKeys(uIENumber0Edit, weeks + "{TAB}", ModifierKeys.None);
        }

        public void SetRosterPlanIsDraft(bool check)
        {
            #region Variable Declarations
            DXCheckBox uIChkDraftCheckBox = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkDraftCheckBox;
            #endregion

            // Clear 'chkDraft' check box
            uIChkDraftCheckBox.Checked = check;
        }

        public void SetPlanReadyForApproval(bool checkApproval)
        {
            #region Variable Declarations
            DXCheckBox uIChkIsReadyForApprovaCheckBox = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkIsReadyForApprovaCheckBox;
            #endregion

            // Clear 'chkIsReadyForApproval' check box
            if (uIChkIsReadyForApprovaCheckBox.Checked != checkApproval)
                uIChkIsReadyForApprovaCheckBox.Checked = checkApproval;
        }

        public void SetPlanForPublishInMyGat(bool checkPublishMyGat)
        {
            #region Variable Declarations
            DXCheckBox uIChkPublishInMyGatCheckBox = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkPublishInMyGatCheckBox;
            #endregion


            // Clear 'chkPublishInMyGat' check box
            if (uIChkPublishInMyGatCheckBox.Checked != checkPublishMyGat)
                uIChkPublishInMyGatCheckBox.Checked = checkPublishMyGat;
        }

        public void CheckCalkulateRosterPlanViewDate(bool check)
        {
            #region Variable Declarations
            DXCheckBox uIChkDoCalculateDisplaCheckBox = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkDoCalculateDisplaCheckBox;
            #endregion

            uIChkDoCalculateDisplaCheckBox.Checked = check;
        }

        public void CheckRosterPlanNightshiftsOnStartday(bool check)
        {
            #region Variable Declarations
            DXCheckBox uIChkNighShiftOnStartDCheckBox = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkNighShiftOnStartDCheckBox;
            #endregion

            // Clear 'chkNighShiftOnStartDay' check box
            uIChkNighShiftOnStartDCheckBox.Checked = check;
        }
        public void CheckUseLimitedScopeInSettings(bool check)
        {
            #region Variable Declarations
            DXCheckBox uIChkUseLimitedScopeCheckBox = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkUseLimitedScopeCheckBox;
            #endregion

            uIChkUseLimitedScopeCheckBox.Checked = check;
        }
        public void SetPlanDoLoadOtherRosterplanData(bool check)
        {
            #region Variable Declarations
            DXCheckBox uIChkDoLoadOtherRosterCheckBox = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkDoLoadOtherRosterCheckBox;
            #endregion

            uIChkDoLoadOtherRosterCheckBox.Checked = check;
        }


        public void SetPlanDoLoadWorkScheduleData(bool check)
        {
            #region Variable Declarations
            DXCheckBox uIChkDoLoadWorkSchedulCheckBox = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIChkDoLoadWorkSchedulCheckBox;
            #endregion

            uIChkDoLoadWorkSchedulCheckBox.Checked = check;
        }

        public void CreateValidPeriodForEmpsInPlan(DateTime fromDate, DateTime toDate)
        {
            #region Variable Declarations
            DXButton uINYButton = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerAvailCustom.UINYButton;
            DXDateTimePicker uISmartDateEditFromDatDateTimeEdit = this.UIItemWindow.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UISmartDateEditFromDatDateTimeEdit;
            DXDateTimePicker uISmartDateEditToDateDateTimeEdit = this.UIItemWindow.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UISmartDateEditToDateDateTimeEdit;
            DXButton uIOKButton = this.UIItemWindow.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UIOKButton;
            #endregion

            // Click 'Ny' button
            Mouse.Click(uINYButton);

            // Type '28.04.2014{Tab}' in 'smartDateEditFromDate' DateTimeEdit
            uISmartDateEditFromDatDateTimeEdit.DateTime = fromDate;
            Keyboard.SendKeys(uISmartDateEditFromDatDateTimeEdit, "{Tab}");

            uISmartDateEditToDateDateTimeEdit.DateTime = toDate;
            Keyboard.SendKeys(uISmartDateEditToDateDateTimeEdit, "{Tab}");

            // Click 'OK' button
            Mouse.Click(uIOKButton);
        }

        /// <summary>
        /// CreateRosterplanCopy - Use 'CreateRosterplanCopyParams' to pass parameters into this method.
        /// </summary>
        internal void CreateRosterplanCopy(string name, string rosterWeek, string weeks, bool chkTasks, bool chkDraft)
        {
            #region Variable Declarations
            DXTextEdit uIENumber1Edit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIENumber1Edit;
            DXTextEdit uIENumber0Edit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIENumber0Edit;
            DXCheckBox uIChkTasksCheckBox = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIGcIncludeClient.UIChkTasksCheckBox;
            DXCheckBox uIChkKladdCheckBox = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIChkKladdCheckBox;
            #endregion

            if (name != "")
            {
                NameCopyOfRosterPlan(name);
            }

            if (rosterWeek != "")
            {
                uIENumber1Edit.ValueAsString = rosterWeek;
                Keyboard.SendKeys(uIENumber1Edit, "{TAB}");
            }

            if (weeks != "")
            {
                //Roster weeks
                Keyboard.SendKeys(uIENumber0Edit, weeks + "{TAB}");
            }

            // Select 'chkTasks' check box
            if (uIChkTasksCheckBox.Checked != chkTasks)
                uIChkTasksCheckBox.Checked = chkTasks;

            // Select 'chkKladd' check box
            if (uIChkTasksCheckBox.Checked != chkDraft)
                uIChkKladdCheckBox.Checked = chkDraft;
        }

        private void NameCopyOfRosterPlan(string name)
        {
            #region Variable Declarations
            DXTextEdit uITxtNameEdit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UITxtNameEdit;
            #endregion

            Keyboard.SendKeys(uITxtNameEdit, name + "{Tab}", ModifierKeys.None);
        }

        public void OkCreateRosterplanCopy()
        {
            #region Variable Declarations
            DXButton uIOKButton = this.UINyarbeidsplanWindow.UIPnlButtonsClient.UIOKButton;
            DXButton uIOKButton1 = this.UIGT3999InformasjonWindow.UIOKButton;
            #endregion

            // Click 'Ok' button
            Mouse.Click(uIOKButton);

            // Click '&OK' button
            Mouse.Click(uIOKButton1);
        }


        /// <summary>
        /// CreateCalendarplanCopy - Use 'CreateCalendarplanCopyParams' to pass parameters into this method.
        /// </summary>
        public void CreateCalendarplanCopy(string name, DateTime? date, bool chkTasks, bool chkDraft)
        {
            #region Variable Declarations
            DXTextEdit uITxtNameEdit = this.UIItemWindow2.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UITxtNameEdit;
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIItemWindow2.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIDeStartDateCustom.UIPceDateDateTimeEdit;
            DXCheckBox uIChkTasksCheckBox = this.UIItemWindow2.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIGcIncludeClient.UIChkTasksCheckBox;
            DXCheckBox uIChkKladdCheckBox = this.UIItemWindow2.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIChkKladdCheckBox;
            #endregion


            if (name != "")
            {
                Keyboard.SendKeys(uITxtNameEdit, name + "{TAB}");
            }

            if (date != null)
            {
                uIPceDateDateTimeEdit.DateTime = date.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");
            }
            // Select 'chkTasks' check box
            if (uIChkTasksCheckBox.Checked != chkTasks)
                uIChkTasksCheckBox.Checked = chkTasks;

            // Select 'chkKladd' check box
            if (uIChkTasksCheckBox.Checked != chkDraft)
                uIChkKladdCheckBox.Checked = chkDraft;
        }

        public void ClickDeleteEffectuatuationAndSelectAllEmployeesFromPayrollCalculationWindow()
        {
            #region Variable Declarations
            DXButton uIGSSimpleButtonButton = this.UIItemWindow7.UIGSPanelControlClient.UIGSSimpleButtonButton;
            DXButton uIVelgalleButton = this.UISletteiverksettingWindow.UIPnlToppInfoClient.UIVelgalleButton;
            #endregion

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);

            // Click 'Velg alle' button
            Mouse.Click(uIVelgalleButton);

        }
        public void DeleteEffectuationFromPayrollCalculationWindow()
        {
            #region Variable Declarations
            DXButton uISlettiverksettingpå5Button = this.UISletteiverksettingWindow.UIPnlBottomClient.UISlettiverksettingpå5Button;
            DXButton uIJAButton = this.UIEXC_4004InformasjonWindow.UIJAButton;
            DXButton uIOKButton = this.UIRPL24003InformasjonWindow.UIOKButton;
            #endregion

            // Click 'Slett iverksetting på 5 linjer' button
            Mouse.Click(uISlettiverksettingpå5Button);

            // Click '&Ja' button
            Mouse.Click(uIJAButton);

            // Click '&OK' button
            Mouse.Click(uIOKButton);
        }
        /// <summary>
        /// CheckRosterassignment
        /// </summary>
        public List<string> CheckRosterassignment(string emp, string roleAssignment, string period)
        {
            #region Variable Declarations
            var errorList = new List<string>();

            Playback.Wait(1000);
            var taskWindow = UIOppgavetildelingWindow;
            taskWindow.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            taskWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Oppgavetildeling";

            DXTestControl uILblAdvancedRoleNameCLabel = taskWindow.UILcMainCustom.UIRootLayoutGroup.UILciCenterLayoutControlItem.UIPcCenterClient.UILcInnerCustom.UILcgInnerLayoutGroup.UILciInnerTopLayoutControlItem.UIPcInnerTopClient.UILcContentCustom.UILcgContentLayoutGroup.UILciInfoLayoutControlItem.UINbcInfoNavBar.UINbgccAdvancedRoleInfScrollableControl.UIPcAdvancedRoleInfoClient.UILblAdvancedRoleNameCLabel;
            DXTestControl uILblEmployeeNameValueLabel = taskWindow.UILcMainCustom.UIRootLayoutGroup.UILciCenterLayoutControlItem.UIPcCenterClient.UILcInnerCustom.UILcgInnerLayoutGroup.UILciInnerTopLayoutControlItem.UIPcInnerTopClient.UILcContentCustom.UILcgContentLayoutGroup.UILciInfoLayoutControlItem.UINbcInfoNavBar.UINbgccEmployeeInfoScrollableControl.UIPcEmployeeInfoClient.UILblEmployeeNameValueLabel;
            DXTestControl uILblAdvancedRoleFromTLabel = taskWindow.UILcMainCustom.UIRootLayoutGroup.UILciCenterLayoutControlItem.UIPcCenterClient.UILcInnerCustom.UILcgInnerLayoutGroup.UILciInnerTopLayoutControlItem.UIPcInnerTopClient.UILcContentCustom.UILcgContentLayoutGroup.UILciInfoLayoutControlItem.UINbcInfoNavBar.UINbgccAdvancedRoleInfScrollableControl.UIPcAdvancedRoleInfoClient.UILblAdvancedRoleFromTLabel;
            #endregion

            try
            {
                Assert.AreEqual(emp, uILblEmployeeNameValueLabel.Text);
            }
            catch (Exception e)
            {
                errorList.Add("Wrong employee: " + e.Message);
            }

            try
            {
                Assert.AreEqual(roleAssignment, uILblAdvancedRoleNameCLabel.Text);
            }
            catch (Exception e)
            {
                errorList.Add("Wrong Role assignment: " + e.Message);
            }

            try
            {
                Assert.AreEqual(period, uILblAdvancedRoleFromTLabel.Text);
            }
            catch (Exception e)
            {
                errorList.Add("Wrong Role assignment period: " + e.Message);
            }

            return errorList;
        }

        /// <summary>
        /// OpenRegStatusWindow
        /// </summary>
        public void OpenRegStatusWindow()
        {
            #region Variable Declarations
            DXPopupEdit uILnkDetailPopupEdit = this.UIIverksetteWindow.UIPnlTopClient.UILnkDetailPopupEdit;
            #endregion

            Mouse.Click(uILnkDetailPopupEdit);
        }
        /// <summary>
        /// OpenRegStatusUtjevning - Use 'OpenRegStatusUtjevningExpectedValues' to pass parameters into this method.
        /// </summary>
        public void OpenRegStatusUtjevning()
        {
            #region Variable Declarations
            DXPopupEdit uILnkDetailPopupEdit = this.UIUtjevningsvaktWindow.UILnkDetailPopupEdit;
            #endregion

            Mouse.Click(uILnkDetailPopupEdit);
        }

        public void SelectTasksAndDraftNewHelpplanWindow(bool checkTasks, bool checkDraft = false)
        {
            #region Variable Declarations
            DXCheckBox uIChkTasksCheckBox = this.UINyhjelpeplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIGcIncludeClient.UIChkTasksCheckBox;
            DXCheckBox uIChkKladdCheckBox = this.UINyhjelpeplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIChkKladdCheckBox;
            #endregion

            uIChkTasksCheckBox.Checked = checkTasks;
            uIChkKladdCheckBox.Checked = checkDraft;
        }



        /// <summary>
        /// PreviewFullRosterplan - Use 'PreviewFullRosterplanParams' to pass parameters into this method.
        /// </summary>
        public void PreviewFullRosterplan(bool messages = false, bool sumDemand = false, bool signLines = false, bool extraLine = false, bool rules = false, bool shiftDetails = false)
        {
            #region Variable Declarations
            DXRibbonButtonItem uIArbeidsplanRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanPrintRibbonPageGroup.UIArbeidsplanRibbonBaseButtonItem;
            DXCheckBox uICbUseColorsCheckBox = this.UIUtskriftavarbeidsplaWindow.UIGsPanelControl2Client.UIGrpShiftCellTypeClient.UICbUseColorsCheckBox;
            DXCheckBox uICbOptimizedForExportCheckBox = this.UIUtskriftavarbeidsplaWindow.UIGsTabControl1TabList.UIXtraTabPage1Client.UIGrpPropertiesClient.UICbOptimizedForExportCheckBox;
            DXCheckBox uICbShiftCodesCheckBox = this.UIUtskriftavarbeidsplaWindow.UIGsTabControl1TabList.UIXtraTabPage1Client.UIGrpAdditionalInfoClient.UICbShiftCodesCheckBox;
            DXRadioGroup uIRgrpPageSizeRadioGroup = this.UIUtskriftavarbeidsplaWindow.UIGsPanelControl2Client.UIGrpPageSizeClient.UIRgrpPageSizeRadioGroup;
            DXCheckBox uICbSumDemandCheckBox = this.UIUtskriftavarbeidsplaWindow.UIGsTabControl1TabList.UIXtraTabPage1Client.UIGrpPropertiesClient.UICbSumDemandCheckBox;
            DXCheckBox uICbSignLinesCheckBox = this.UIUtskriftavarbeidsplaWindow.UIGsTabControl1TabList.UIXtraTabPage1Client.UIGrpPropertiesClient.UICbSignLinesCheckBox;
            DXCheckBox uICbExtraLineCheckBox = this.UIUtskriftavarbeidsplaWindow.UIGsTabControl1TabList.UIXtraTabPage1Client.UIGrpPropertiesClient.UICbExtraLineCheckBox;
            DXCheckBox uICbRulesCheckBox = this.UIUtskriftavarbeidsplaWindow.UIGsTabControl1TabList.UIXtraTabPage1Client.UIGrpAdditionalInfoClient.UICbRulesCheckBox;
            DXCheckBox uICbMessagesCheckBox = this.UIUtskriftavarbeidsplaWindow.UIGsTabControl1TabList.UIXtraTabPage1Client.UIGrpAdditionalInfoClient.UICbMessagesCheckBox;
            DXCheckBox uICbShiftDetailsCheckBox = this.UIUtskriftavarbeidsplaWindow.UIGsTabControl1TabList.UIXtraTabPage1Client.UIGrpAdditionalInfoClient.UICbShiftDetailsCheckBox;

            DXButton uIForhåndsvisButton = this.UIUtskriftavarbeidsplaWindow.UIPnlButtonsClient.UIForhåndsvisButton;
            #endregion

            // Click 'Arbeidsplan' RibbonBaseButtonItem
            Mouse.Click(uIArbeidsplanRibbonBaseButtonItem, new Point(31, 34));

            // Select 'cbUseColors' check box
            uICbUseColorsCheckBox.Checked = true;

            // Select 'cbOptimizedForExport' check box
            uICbOptimizedForExportCheckBox.Checked = true;

            // Select 'cbShiftCodes' check box
            uICbShiftCodesCheckBox.Checked = true;
            uIRgrpPageSizeRadioGroup.SelectedIndex = 1;

            #region Variable Declarations
            #endregion

            // Select 'cbSumDemand' check box
            if (uICbSumDemandCheckBox.Checked != sumDemand)
                uICbSumDemandCheckBox.Checked = sumDemand;

            // Select 'cbSignLines' check box
            if (uICbSignLinesCheckBox.Checked != signLines)
                uICbSignLinesCheckBox.Checked = signLines;

            // Select 'cbExtraLine' check box
            if (uICbExtraLineCheckBox.Checked != extraLine)
                uICbExtraLineCheckBox.Checked = extraLine;

            // Select 'cbRules' check box
            if (uICbRulesCheckBox.Checked != rules)
                uICbRulesCheckBox.Checked = rules;

            // Select 'cbMessages' check box
            if (uICbMessagesCheckBox.Checked != messages)
                uICbMessagesCheckBox.Checked = messages;

            // Select 'cbShiftDetails' check box
            if (uICbShiftDetailsCheckBox.Checked != shiftDetails)
                uICbShiftDetailsCheckBox.Checked = shiftDetails;

            // Click 'Forhåndsvis' button
            Mouse.Click(uIForhåndsvisButton);
        }


        /// <summary>
        /// CreateNewBaseplanCopy - Use 'CreateNewBaseplanCopyParams' to pass parameters into this method.
        /// </summary>
        public void CreateNewBaseplanCopy(string name, string fromWeek, DateTime fromDate, string weeks, bool draft)
        {
            #region Variable Declarations
            DXTextEdit uITxtNameEdit = this.UINybaseplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UITxtNameEdit;
            DXTextEdit uIENumber1Edit = this.UINybaseplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIENumber1Edit;
            DXDateTimePicker uIPceDateDateTimeEdit = this.UINybaseplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIDeStartDateCustom.UIPceDateDateTimeEdit;
            DXTextEdit uIENumber0Edit = this.UINybaseplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIENumber0Edit;
            DXCheckBox uIChkKladdCheckBox = this.UINybaseplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIChkKladdCheckBox;
            #endregion

            if (name != "")
            {
                Keyboard.SendKeys(uITxtNameEdit, "a", ModifierKeys.Control);
                Keyboard.SendKeys(uITxtNameEdit, name + "{TAB}");
            }

            if (fromWeek != "")
            {
                Keyboard.SendKeys(uIENumber1Edit, "a", ModifierKeys.Control);
                Keyboard.SendKeys(uIENumber1Edit, fromWeek + "{TAB}");
            }

            uIPceDateDateTimeEdit.DateTime = fromDate;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");

            if (weeks != "")
            {
                Keyboard.SendKeys(uIENumber0Edit, "a", ModifierKeys.Control);
                Keyboard.SendKeys(uIENumber0Edit, weeks + "{TAB}");
            }

            // Select 'cbSumDemand' check box
            if (uIChkKladdCheckBox.Checked != draft)
                uIChkKladdCheckBox.Checked = draft;
        }

        public void OkNewBaseplanCopy()
        {
            #region Variable Declarations
            DXButton uIOKButton = this.UINybaseplanWindow.UIPnlButtonsClient.UIOKButton;
            DXButton uIOKButton1 = this.UIGT3999InformasjonWindow.UIOKButton;
            #endregion


            // Click 'Ok' button
            Mouse.Click(uIOKButton);

            // Click '&OK' button
            Mouse.Click(uIOKButton1);
        }


        /// <summary>
        /// CreateRosterplanDemand - Use 'CreateRosterplanDemandParams' to pass parameters into this method.
        /// </summary>
        internal void CreateRosterplanDemand(ConfirmRegistrationTypes regTypeIn, string department, string type, ConfirmRegistrationTypes regTypeOut, bool effectuate = false, bool transfere = false, bool publish = false, bool inactive = false)
        {
            #region Variable Declarations
            WinClient uIItemClient = this.UIGodkjenningskravarbeWindow.UIGodkjenningskravarbeClient.UIItemClient;
            WinComboBox uIItemComboBox = this.UIGodkjenningskravarbeWindow.UIItemWindow.UIItemComboBox;
            WinListItem uIItem5000ARBEIDSPLANKListItem = this.UIItemWindow11.UIItemWindow1.UIItemList.UIItem5000ARBEIDSPLANKListItem;
            WinListItem uIItem01VikarpoolListItem = this.UIItemWindow11.UIItemWindow1.UIItemList.UIItem01VikarpoolListItem;
            WinComboBox uIItemComboBox1 = this.UIGodkjenningskravarbeWindow.UIItemWindow1.UIItemComboBox;
            WinListItem uILederListItem = this.UIItemWindow11.UIItemWindow1.UIItemList.UILederListItem;
            WinCheckBox uITillativerksettingutCheckBox = this.UIGodkjenningskravarbeWindow.UIItemWindow2.UIItemClient.UITillativerksettingutCheckBox;
            WinCheckBox uITillatoverføringavfaCheckBox = this.UIGodkjenningskravarbeWindow.UIItemWindow2.UIItemClient.UITillatoverføringavfaCheckBox;
            WinCheckBox uITillatpubliseringavaCheckBox = this.UIGodkjenningskravarbeWindow.UIItemWindow2.UIItemClient.UITillatpubliseringavaCheckBox;
            WinCheckBox uIInaktivCheckBox = this.UIGodkjenningskravarbeWindow.UIItemWindow2.UIItemClient.UIInaktivCheckBox;
            WinButton uIOKButton = this.UIGodkjenningskravarbeWindow.UIItemClient.UIOKButton;
            WinButton uIAvbrytButton = this.UIGodkjenningskravarbeWindow.UIItemClient.UIAvbrytButton;
            WinButton uIOKNyButton = this.UIGodkjenningskravarbeWindow.UIItemClient.UIOKNyButton;
            WinButton uIJAButton = this.UIBekreftWindow.UIBekreftClient.UIJAButton;
            #endregion

            switch (regTypeIn)
            {
                case ConfirmRegistrationTypes.New:
                    //Click NewButton
                    Mouse.Click(uIItemClient, new Point(29, 25));
                    break;
                case ConfirmRegistrationTypes.Edit:
                    //Click EditButton
                    Mouse.Click(uIItemClient, new Point(93, 27));
                    break;
                case ConfirmRegistrationTypes.Delete:
                    // Click DeleteButton
                    Mouse.Click(uIItemClient, new Point(152, 32));
                    // Click '&Ja' Delete button
                    Mouse.Click(uIJAButton, new Point(39, 12));
                    break;
                case ConfirmRegistrationTypes.Duplicate:
                    //Click DuplicateButton
                    Mouse.Click(uIItemClient, new Point(210, 34));
                    break;
                case ConfirmRegistrationTypes.Cancel:
                    // Click 'Avbryt' button
                    Mouse.Click(uIAvbrytButton, new Point(33, 29));
                    break;
                default:
                    break;
            }


            // Click DepartmentCombo
            Mouse.Click(uIItemComboBox, new Point(192, 10));

            // Type 'Department' in DepartmentCombo
            Keyboard.SendKeys(uIItemComboBox, department + "{ENTER}"); //department

            //// Type '0' in '5000 - ARBEIDSPLANKLINIKKEN' list item
            //Keyboard.SendKeys(uIItem5000ARBEIDSPLANKListItem, "0");

            // Click TypeCombo
            Mouse.Click(uIItemComboBox1, new Point(184, 8));

            // Type 'Type' in TypeCombo
            Keyboard.SendKeys(uIItemComboBox1, type + "{ENTER}"); //type


            // Select 'Tillat iverksetting uten godkjenning' check box
            if (uITillativerksettingutCheckBox.Checked != effectuate)
                uITillativerksettingutCheckBox.Checked = effectuate;

            // Select 'Tillat overføring av faste tillegg uten godkjennin...' check box
            if (uITillatoverføringavfaCheckBox.Checked != transfere)
                uITillatoverføringavfaCheckBox.Checked = transfere;

            // Select 'Tillat publisering av arbeidsplan i MinGat uten go...' check box
            if (uITillatpubliseringavaCheckBox.Checked != publish)
                uITillatpubliseringavaCheckBox.Checked = publish;

            // Select 'Inaktiv' check box
            if (uIInaktivCheckBox.Checked != inactive)
                uIInaktivCheckBox.Checked = inactive;


            switch (regTypeOut)
            {
                case ConfirmRegistrationTypes.Ok:
                    // Click 'OK' button
                    Mouse.Click(uIOKButton, new Point(19, 24));
                    break;
                case ConfirmRegistrationTypes.OkNew:
                    // Click 'OK - Ny' button
                    Mouse.Click(uIOKNyButton, new Point(31, 25));
                    break;

                default:
                    break;
            }
        }

        internal void CloseRosterplanDemandWindow(bool xClose = false)
        {
            WinButton XCloseButton = this.UIGodkjenningskravarbeWindow.UIGodkjenningskravarbeTitleBar.UICloseButton;
            WinClient CloseButton = this.UIGodkjenningskravarbeWindow.UIItemClient1.UIItemClient;

            if (xClose)
                Mouse.Click(XCloseButton, new Point(22, 13));
            else
                Mouse.Click(CloseButton, new Point(346, 24));
        }

        /// <summary>
        /// CreateRosterplanRepresentation - Use 'CreateRosterplanRepresentationParams' to pass parameters into this method.
        /// </summary>
        internal void CreateRosterplanRepresentation(ConfirmRegistrationTypes regTypeIn, string roleName, string department, string type, List<string> unionList, List<string> repList, ConfirmRegistrationTypes regTypeOut)
        {
            #region Variable Declarations
            DXButton uINYButton = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UINYButton;
            DXTextEdit uITxtNameEdit = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UIGrpRepresentationClient.UIGsLayoutControl2Custom.UIRootLayoutGroup.UILayoutControlItem5LayoutControlItem.UIGrpRoleClient.UITxtNameEdit;
            DXPopupEdit uIPceDepartmentPopupEdit = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UIGrpRepresentationClient.UIGsLayoutControl2Custom.UIRootLayoutGroup.UILayoutControlItem6LayoutControlItem.UIGrpDepartmentClient.UIPceDepartmentPopupEdit;
            DXTreeList uITlDepartmentsTreeList = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UIGrpRepresentationClient.UIGsLayoutControl2Custom.UIRootLayoutGroup.UILayoutControlItem6LayoutControlItem.UIGrpDepartmentClient.UIPceDepartmentPopupEdit.UIPopupContainerFormWindow.UIPccDepartmentsClient.UITlDepartmentsTreeList;
            uITlDepartmentsTreeList.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uITlDepartmentsTreeList.SearchProperties[DXTestControl.PropertyNames.Name] = "tlDepartments";
            DXLookUpEdit uILueTypeLookUpEdit = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UIGrpRepresentationClient.UIGsLayoutControl2Custom.UIRootLayoutGroup.UILayoutControlItem13LayoutControlItem.UIGrpRepresentationTypClient.UILueTypeLookUpEdit;
            uILueTypeLookUpEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uILueTypeLookUpEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "lueType";
            DXButton uIOKButton = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIOKButton;
            DXButton uIAvbrytButton = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIAvbrytButton;
            DXButton uIEndreButton = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIEndreButton;
            DXButton uISlettButton = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UISlettButton;
            DXButton uIJAButton = this.UIGT4001InformasjonWindow.UIJAButton;
            #endregion

            switch (regTypeIn)
            {
                case ConfirmRegistrationTypes.New:
                    // Click '&Ny' button
                    Mouse.Click(uINYButton);
                    break;
                case ConfirmRegistrationTypes.Edit:
                    // Click '&Endre' button
                    Mouse.Click(uIEndreButton);
                    break;
                case ConfirmRegistrationTypes.Delete:
                    // Click 'Slett' button
                    Mouse.Click(uISlettButton);
                    // Click '&Ja' button
                    Mouse.Click(uIJAButton);
                    break;
                case ConfirmRegistrationTypes.Cancel:
                    // Click 'Avbryt' button
                    Mouse.Click(uIAvbrytButton);
                    break;
                default:
                    break;
            }

            // Type '{Tab}' in 'txtName' text box
            uITxtNameEdit.Text = roleName;
            Keyboard.SendKeys(uITxtNameEdit, "{TAB}");

            //// Type '5000{Tab}{Enter}' in 'tlDepartments' TreeList
            //Mouse.Click(uIPceDepartmentPopupEdit);            
            Keyboard.SendKeys(uITlDepartmentsTreeList, department + "{Enter}");

            Keyboard.SendKeys(uILueTypeLookUpEdit, type + "{Enter}");

            AddUnionsToRosterplanRepresentation(unionList);
            AddRepresentativesToRosterplanRepresentation(repList);

            switch (regTypeOut)
            {
                case ConfirmRegistrationTypes.Ok:
                    // Click '&OK' button
                    Mouse.Click(uIOKButton);
                    break;
                case ConfirmRegistrationTypes.Cancel:
                    // Click '&Cancel' button
                    Mouse.Click(uIAvbrytButton);
                    break;
                default:
                    break;
            }
        }
        internal void EditRosterplanRepresentation(string repName, string roleName, string department, string type, List<string> unionList, List<string> repList, ConfirmRegistrationTypes regTypeOut)
        {
            #region Variable Declarations
            DXTextEdit uITxtNameEdit = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UIGrpRepresentationClient.UIGsLayoutControl2Custom.UIRootLayoutGroup.UILayoutControlItem5LayoutControlItem.UIGrpRoleClient.UITxtNameEdit;
            DXTreeList uITlDepartmentsTreeList = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UIGrpRepresentationClient.UIGsLayoutControl2Custom.UIRootLayoutGroup.UILayoutControlItem6LayoutControlItem.UIGrpDepartmentClient.UIPceDepartmentPopupEdit.UIPopupContainerFormWindow.UIPccDepartmentsClient.UITlDepartmentsTreeList;
            uITlDepartmentsTreeList.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uITlDepartmentsTreeList.SearchProperties[DXTestControl.PropertyNames.Name] = "tlDepartments";
            DXLookUpEdit uILueTypeLookUpEdit = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UIGrpRepresentationClient.UIGsLayoutControl2Custom.UIRootLayoutGroup.UILayoutControlItem13LayoutControlItem.UIGrpRepresentationTypClient.UILueTypeLookUpEdit;
            uILueTypeLookUpEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uILueTypeLookUpEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "lueType";
            DXButton uIOKButton = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIOKButton;
            DXButton uIEndreButton = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIEndreButton;
            DXButton uIAvbrytButton = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIAvbrytButton;
            #endregion

            F3SearchInRepresentationGrid(repName);
            Mouse.Click(uIEndreButton);

            // Type '{Tab}' in 'txtName' text box
            if (!String.IsNullOrEmpty(roleName))
            {
                uITxtNameEdit.Text = roleName;
                Keyboard.SendKeys(uITxtNameEdit, "{TAB}");
            }

            if (!String.IsNullOrEmpty(department))
                Keyboard.SendKeys(uITlDepartmentsTreeList, department + "{Enter}"); //department

            if (!String.IsNullOrEmpty(type))
                Keyboard.SendKeys(uILueTypeLookUpEdit, type + "{Enter}"); //type

            if (unionList != null)
                AddUnionsToRosterplanRepresentation(unionList);

            if (repList != null)
            {
                AddRepresentativesToRosterplanRepresentation(repList, true);
            }

            switch (regTypeOut)
            {
                case ConfirmRegistrationTypes.Ok:
                    // Click '&OK' button
                    Mouse.Click(uIOKButton);
                    break;
                case ConfirmRegistrationTypes.Cancel:
                    // Click '&Cancel' button
                    Mouse.Click(uIAvbrytButton);
                    break;
                default:
                    break;
            }
        }

        public void AddUnionsToRosterplanRepresentation(List<string> unions)
        {
            #region Variable Declarations
            DXButton uILeggtilendreButton = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UIGrpRepresentationClient.UIGsLayoutControl2Custom.UIRootLayoutGroup.UILayoutControlItem7LayoutControlItem.UIGsGroupControl1Client.UIGsLayoutControl3Custom.UILeggtilendreButton;
            var selectCell = this.UIVelgfagforeningerWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGsGroupControl2Client.UIGcAvailableUnionsTable.UIDELTADeltaCell;
            DXButton uIBtnAddButton = this.UIVelgfagforeningerWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlGroup5LayoutGroup.UILayoutControlItem4LayoutControlItem.UIBtnAddButton;
            DXButton uIOKButton = this.UIVelgfagforeningerWindow.UIGsLayoutControl1Custom.UIOKButton;
            #endregion

            // Click 'Legg til/endre' button
            Mouse.Click(uILeggtilendreButton);

            bool firstRow = true;
            foreach (var unionRowNo in unions)
            {
                selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcAvailableUnionsGridControlCell[View]gvAvailableunions[Row]" + unionRowNo + "[Column]gridColumn1";

                if (firstRow)
                {
                    Mouse.Click(selectCell);
                    firstRow = false;
                }
                else
                    Mouse.Click(selectCell, MouseButtons.Left, ModifierKeys.Control, new Point(38, 19));
            }

            // Click 'btnAdd' button
            Mouse.Click(uIBtnAddButton);

            // Click 'OK' button
            Mouse.Click(uIOKButton);
        }
        public void AddRepresentativesToRosterplanRepresentation(List<string> representatives, bool removeExisting = false)
        {
            #region Variable Declarations
            DXButton uILeggtilendreButton1 = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UIGrpRepresentationClient.UIGsLayoutControl2Custom.UIRootLayoutGroup.UILayoutControlItem8LayoutControlItem.UIGsGroupControl2Client.UIGsLayoutControl4Custom.UILeggtilendreButton;
            //var selectCell = this.UIVelgrepresentanterWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGrpAvailableEmployeeClient.UIGcAvailableEmployeesTable.UIItem1321Cell;
            DXButton uIBtnAddButton1 = this.UIVelgrepresentanterWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlGroup2LayoutGroup.UILayoutControlItem5LayoutControlItem.UIBtnAddButton;
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIVelgperiodeWindow.UIGsPanelControl1Client.UIDeFromDateCustom.UIPceDateDateTimeEdit;
            DXButton uIOKButton1 = this.UIVelgperiodeWindow.UIOKButton;
            DXButton uIOKButton2 = this.UIVelgrepresentanterWindow.UIGsLayoutControl1Custom.UIOKButton;
            #endregion

            // Click 'Legg til/endre' button
            Mouse.Click(uILeggtilendreButton1);

            if (removeExisting)
                RemoveAllRepresentations();

            //bool firstRow = true;
            foreach (var rep in representatives)
            {
                F3SearchInRepresentationRepresentativesGrid(rep);
                //selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcAvailableEmployeesGridControlCell[View]gvAvailableEmployees[Row]" + repRowNo + "[Column]colEmployeeCode";

                //if (firstRow)
                //{
                //    Mouse.Click(selectCell);
                //    firstRow = false;
                //}
                //else
                //    Mouse.Click(selectCell, MouseButtons.Left, ModifierKeys.Control, new Point(38, 19));


                // Click 'btnAdd' button
                Mouse.Click(uIBtnAddButton1);

                uIPceDateDateTimeEdit.DateTime = DateTime.Now;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, "{Tab}");

                // Click 'OK' button
                Mouse.Click(uIOKButton1);

            }

            // Click 'OK' button
            Mouse.Click(uIOKButton2);
        }

        internal void CloseRosterplanRepresentationWindow(bool xClose = false)
        {
            DXButton CloseButton = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UILukkButton;
            DXButton XCloseButton = this.UIRepresentasjonerWindow.UICloseButton;

            if (xClose)
                Mouse.Click(XCloseButton, new Point(22, 13));
            else
                Mouse.Click(CloseButton, new Point(346, 24));
        }

        public enum ConfirmRegistrationTypes
        {
            New,
            Ok,
            OkNew,
            Edit,
            Duplicate,
            Delete,
            Cancel
        }


        /// <summary>
        /// F3SearchInGrid - Use 'F3SearchInGridParams' to pass parameters into this method.
        /// </summary>
        private void F3SearchInGrid(string searchString, bool useAndOperation = false, bool cancel = false)
        {
            #region Variable Declarations
            DXTextEdit uITxtSearchEdit = this.UISøkilisteWindow.UIPnlSearchClient.UITxtSearchEdit;
            DXButton uIVelgButton = this.UISøkilisteWindow.UIPnlButtonsClient.UIVelgButton;
            DXButton uIAvbrytButton = this.UISøkilisteWindow.UIPnlButtonsClient.UIAvbrytButton;
            #endregion

            if (useAndOperation)
                SelectAndOperationInF3SearchWindow();

            //ValueAsString
            uITxtSearchEdit.ValueAsString = searchString;

            // Type '{Tab}' in 'txtSearch' text box
            Keyboard.SendKeys(uITxtSearchEdit, "{Tab}");

            if (cancel)
                Mouse.Click(uIAvbrytButton);
            else
                Mouse.Click(uIVelgButton);
        }
        public void F3SearchInRepresentationRepresentativesGrid(string searchString, bool cancel = false)
        {
            #region Variable Declarations
            var availableEmployeesTable = this.UIVelgrepresentanterWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGrpAvailableEmployeeClient.UIGcAvailableEmployeesTable;
            #endregion

            // Type '{F3}' in 'gcAvailableEmployees' table
            Keyboard.SendKeys(availableEmployeesTable, "{F3}");

            F3SearchInGrid(searchString, cancel);
        }
        public void F3SearchInRepresentationGrid(string searchString, bool cancel = false)
        {
            #region Variable Declarations
            var representationsTable = this.UIRepresentasjonerWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILayoutControlItem1LayoutControlItem.UIGrpRepresentationsClient.UIGcRepresentationsTable; ;
            #endregion

            // Type '{F3}' in 'gcAvailableEmployees' table
            Keyboard.SendKeys(representationsTable, "{F3}");

            F3SearchInGrid(searchString, true, cancel);
        }

        /// <summary>
        /// SetApprovalVisualisationGridHeight - Use 'SetApprovalVisualisationGridHeightExpectedValues' to pass parameters into this method.
        /// </summary>
        public void SetVisualisationGridHeight(int height)
        {
            #region Variable Declarations
            DXTestControl uITopSeparator = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel1.UITopSeparator;
            DXGrid uIGcRosterPlanTable = this.UIArbeidsplanWindow.UIPnlRosterPlanClient1.UIGcRosterPlanTable;
            #endregion

            // Move 'Top' separator to 'gcRosterPlan' table
            uIGcRosterPlanTable.EnsureClickable(new Point(825, height));
            Mouse.StartDragging(uITopSeparator, new Point(826, 1));
            Mouse.StopDragging(uIGcRosterPlanTable, new Point(825, height));
        }

        public void OpenRegStatusSwitchEmpWindow()
        {
            #region Variable Declarations
            DXPopupEdit uILnkDetailPopupEdit = this.UIByttansattWindow.UILnkDetailPopupEdit;
            #endregion

            Mouse.Click(uILnkDetailPopupEdit);
        }

        [Obsolete("DeleteAdministrationSearchString is deprecated", true)]
        public void DeleteAdministrationSearchString()
        {
            #region Variable Declarations
            DXButton uIEditorButton0Button = this.UIGatWindow.UIViewHostCustom1.UIPcViewClient.UIAdministrationViewCustom.UIPanelControl1Client.UIScOptionsMRUEdit.UIEditorButton0Button;
            #endregion

            Mouse.Click(uIEditorButton0Button);
            Playback.Wait(2000);
        }

        public List<string> CheckTransfereFTTDisabled(string step)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            var table = this.UIOverførtilleggWindow.UIGcExportsTable;
            DXCell cell = this.UIOverførtilleggWindow.UIGcExportsTable.UIVrienVigdisCell;
            DXButton uIOverførButton = this.UIOverførtilleggWindow.UIOverførButton;

            string CellErrorText = "Kan ikke eksportere ved manglende godkjenning.";
            bool UIOverførButtonEnabled = false;
            #endregion

            for (int i = 0; i < table.Views[0].RowCount; i++)
            {
                cell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcExportsGridControlCell[View]gvExports[Row]" + i + "[Column]colEmployee";
                try
                {
                    Assert.AreEqual(CellErrorText, cell.ErrorText, "Unexpected message!");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step " + step + ", row " + (i + 1) + "(" + cell.ValueAsString + "): " + e.Message);
                }
            }

            try
            {
                // Verify that the 'Enabled' property of '&Overfør' button equals 'False'
                Assert.AreEqual(UIOverførButtonEnabled, uIOverførButton.Enabled, "Transfere button is enabled!");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step " + step + ": " + e.Message);
            }


            return errorList;
        }

        /// <summary>
        /// RejectApprovalInApprovalTab - Use 'RejectApprovalInApprovalTabParams' to pass parameters into this method.
        /// </summary>
        public void RejectApprovalInApprovalTab(bool isRejectedInCell)
        {
            #region Variable Declarations
            DXButton uIAvvisButton = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient1.UIApprovalViewCustom.UILcMainCustom.UIAvvisButton;
            DXButton uIJAButton = this.UIGT4006InformasjonWindow.UIJAButton;
            DXTextEdit uIMeCommentEdit = this.UISkrivenkommentartilaWindow.UIMeCommentEdit;
            DXButton uIOKButton = this.UISkrivenkommentartilaWindow.UIOKButton;
            #endregion

            if (!isRejectedInCell)
            {
                Mouse.Click(uIAvvisButton);
                Mouse.Click(uIJAButton);
            }

            uIMeCommentEdit.ValueAsString = "Avviser";

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }

        /// <summary>
        /// RepealOwnApprovalInApprovalTab - Use 'RepealOwnApprovalInApprovalTabParams' to pass parameters into this method.
        /// </summary>
        public void RepealOwnApprovalInApprovalTab(bool comment, bool placa4)
        {
            #region Variable Declarations
            DXButton uIOpphevegneButton = new DXButton();
            if (placa4)
            {
                uIOpphevegneButton = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIApprovalViewCustom.UILcMainCustom.UIOpphevegneButton;
            }
            else
            {
                uIOpphevegneButton = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient1.UIApprovalViewCustom.UILcMainCustom.UIOpphevegneButton;
            }

            DXButton uIJAButton1 = this.UIGT4006InformasjonWindow.UIJAButton1;
            #endregion

            // Click 'Opphev egne' button
            Mouse.Click(uIOpphevegneButton);

            // Click '&Ja' button
            Mouse.Click(uIJAButton1);

            if (comment)
                RepealApprovalMessage();

        }
        public void RepealApprovalMessage()
        {
            #region Variable Declarations
            DXTextEdit uIMeCommentEdit = this.UISkrivenkommentartiloWindow.UIMeCommentEdit;
            DXButton uIOKButton = this.UISkrivenkommentartiloWindow.UIOKButton;
            #endregion

            try
            {
                if (UISkrivenkommentartiloWindow.Exists)
                {
                    // Type 'Opphever' in 'meComment' text box
                    //ValueAsString
                    uIMeCommentEdit.ValueAsString = "Opphever";

                    // Click 'Ok' button
                    Mouse.Click(uIOKButton);
                }
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Error in Approvalmessage window: " + e.Message);
            }
        }
        /// <summary>
        /// ClickCloseExportLogWindow
        /// </summary>
        public void ClickCloseExportLogWindow()
        {
            #region Variable Declarations
            DXButton uIGSSimpleButtonButton = this.UIItemWindow6.UIGSLayoutControlCustom.UI_LayoutGroup_RootLayoutGroup.UILayoutControlItem2LayoutControlItem.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion
            try
            {
                XCloseExportLogWindow();
            }
            catch
            {
                // Click 'GSSimpleButton' button
                Mouse.Click(uIGSSimpleButtonButton);
            }
        }
        public void SelectWholeRosterplanCTRLA()
        {
            #region Variable Declarations
            var tbl = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable;
            #endregion

            SelectFirstCellInPlan();
            Playback.Wait(500);
            Keyboard.SendKeys(tbl, "a", ModifierKeys.Control);
            Playback.Wait(1000);
        }

        public void ClickWorkbookRoleAssignmentRoleListEmployeesTab()
        {
            #region Variable Declarations
            var dockPanel = this.UIOppgavetildelingWindow.UIVhContentCustom.UIPcViewClient.UIRoleAssignmentOverviCustom.UIPanelContainer2DockPanel;
            dockPanel.SearchProperties[DXTestControl.PropertyNames.Name] = "panelContainer2";
            var panel = dockPanel.UIPanelContainer1DockPanel;
            panel.SearchProperties[DXTestControl.PropertyNames.Name] = "panelContainer1";
            var tabEmp = panel.UIAnsatteButton;
            tabEmp.SearchProperties[DXTestControl.PropertyNames.Name] = "Ansatte";
            #endregion

            // Click 'Ansatte' button
            Mouse.Click(tabEmp);
        }
        public bool CheckCloseRosterplanButtonHomeTabExists()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIBtnCloseRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpRosterPlanRibbonPage.UIGrpCloseRibbonPageGroup.UIBtnCloseRibbonBaseButtonItem;
            uIBtnCloseRibbonBaseButtonItem.SearchProperties[DXTestControl.PropertyNames.Name] = "btnClose";
            #endregion

            try
            {
                if (uIBtnCloseRibbonBaseButtonItem.Exists)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
        public bool CheckCloseButtonRosterPlanTabExists()
        {
            try
            {
                CheckCloseRosterplanButtonPlanTabExists();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void F3SearchInRosterplanGrid(string searchValue, bool search = true)
        {
            #region Variable Declarations
            var firstCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIItem10HelgCell;
            var uIGcRosterPlanTable = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable;
            var uI_TeFindMRUEdit = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UI_LayoutControl1Custom.UILcGroupMainLayoutGroup.UILciFindLayoutControlItem.UI_TeFindMRUEdit;
            var uISøkButton = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UI_LayoutControl1Custom.UILcGroupMainLayoutGroup.UILciFindButtonLayoutControlItem.UISøkButton;
            #endregion

            Mouse.Click(firstCell);

            // Type '{F3}' in 'gcRosterPlan' table
            Keyboard.SendKeys(uIGcRosterPlanTable, "{F3}");
            Keyboard.SendKeys(uI_TeFindMRUEdit, searchValue + "{TAB}");

            if (search)
                Mouse.Click(uISøkButton);
        }
        public void EmptyF3SearchInRosterplanGrid()
        {
            #region Variable Declarations
            var uITømButton = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UI_LayoutControl1Custom.UILcGroupMainLayoutGroup.UILciClearButtonLayoutControlItem.UITømButton;
            #endregion

            // Click 'Tøm' button
            Mouse.Click(uITømButton);
        }
        public int CountRosterplanLines()
        {
            #region Variable Declarations
            var tbl = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable;
            #endregion

            return tbl.Views[0].RowCount;
        }
        public void SelectRosterplanSubTab(RosterplanTabs value)
        {
            var tabName = RosterplanTabsToAlias(value);
            var tab = GetTabPageHeader(tabName);
            //Mouse.Click(tab);
        }
        private bool GetTabPageHeader(string text)
        {
            #region Variable Declarations
            DXTestControl tabList = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList;
            #endregion

            int pageCount = (int)tabList.GetProperty(DXTestControl.PropertyNames.TabPageCount);
            for (int i = 0; i < pageCount; i++)
            {
                DXTestControl header = new DXTestControl(tabList);
                header.SearchProperties.Add(DXTestControl.PropertyNames.ClassName, "XtraTabControlHeader");
                header.SearchProperties.Add(DXTestControl.PropertyNames.Name, string.Format("{0}PageHeader{1}", tabList.SearchProperties[DXTestControl.PropertyNames.Name], i));

                Mouse.Click(header);

                if (header.Text == text)
                    return true;
            }

            return false;
        }
        private string RosterplanTabsToAlias(RosterplanTabs value)
        {
            switch (value)
            {
                case RosterplanTabs.SumBehov: return "Sum / Behov";
                case RosterplanTabs.Fastetillegg: return "Faste tillegg";
                case RosterplanTabs.Feiladvarsler: return "Feil/advarsler";
                default: return value.ToString();
            }
        }
        public enum RosterplanTabs
        {
            SumBehov,
            Visualisering,
            Fastetillegg,
            Timelønnsberegning,
            Kostnadssimulering,
            Godkjenning,
            Gjennomsnittsberegning,
            Fravær,
            Feriebanker,
            Detaljer,
            Feiladvarsler,
            Ukekommentarer,
            Summeringer,
            Tillegg,
            Debug
        }

        public void SetTransfereFTTDate(string transfereDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIDeExportFromDateTimeEdit = this.UIOverførtilleggWindow.UIDeExportFromDateTimeEdit;
            #endregion

            //uIDeExportFromDateTimeEdit.ValueAsString = transfereDate; ;
            Keyboard.SendKeys(uIDeExportFromDateTimeEdit, transfereDate + "{Tab}");
        }
        #endregion

        #region Report Functions

        public void CheckAmlReport7UseCalculatedAmlViolation(bool checkAmlCalculations)
        {
            if (checkAmlCalculations)
                CheckAmlReport7UseCalculatedAmlViolation();
            else
                UnCheckAmlReport7UseCalculatedAmlViolation();
        }

        #endregion

        #region Employment Functions

        internal void ClickNewEmployment()
        {
            DXButton uINYButton = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIEmploymentListControCustom.UINYButton;
            // Click 'Ny' button
            Mouse.Click(uINYButton);
        }

        internal void CreateNewEmployment(string empPercent, string ruleSet, string posCategory, string aml, string internalPosNumber, string dep, string costDep)
        {
            #region Variable Declarations
            DXTextEdit uISePositionPercentEdit = this.UIStillingsforholdWindow.UIPcContentClient.UISePositionPercentEdit;
            DXLookUpEdit uITlleDepartment1LookUpEdit = this.UIStillingsforholdWindow.UIPcContentClient.UITlleDepartment1LookUpEdit;
            DXLookUpEdit uITlleDepartment0LookUpEdit = this.UIStillingsforholdWindow.UIPcContentClient.UITlleDepartment0LookUpEdit;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient.UIGSSearchLookUpEditLookUpEdit;
            DXTextEdit uITeFindEdit = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient.UIGSSearchLookUpEditLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit1 = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient.UIGSSearchLookUpEditLookUpEdit1;
            DXTextEdit uITeFindEdit1 = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient.UIGSSearchLookUpEditLookUpEdit1.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILcgFindLayoutGroup.UILciLabelFindLayoutControlItem.UITeFindEdit;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit2 = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient.UIGSSearchLookUpEditLookUpEdit2;
            DXTextEdit uITeInternalPositionNuEdit = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient.UITeInternalPositionNuEdit;
            #endregion

            if (empPercent != "")
            {
                uISePositionPercentEdit.ValueAsString = empPercent;
                Keyboard.SendKeys(uISePositionPercentEdit, "{Tab}");
            }

            if (dep != "")
            {
                Mouse.Click();
                Keyboard.SendKeys(uITlleDepartment1LookUpEdit, dep + "{ENTER}");
            }

            if (costDep != "")
            {
                Mouse.Click();
                Keyboard.SendKeys(uITlleDepartment0LookUpEdit, costDep + "{ENTER}");
            }

            if (ruleSet != "")
            {
                Mouse.Click(uIGSSearchLookUpEditLookUpEdit);
                Keyboard.SendKeys(uITeFindEdit, ruleSet + "{ENTER}");
            }

            if (posCategory != "")
            {
                Mouse.Click(uIGSSearchLookUpEditLookUpEdit1);
                Keyboard.SendKeys(uITeFindEdit1, posCategory + "{ENTER}");
            }

            if (aml != "")
            {
                Mouse.Click(uIGSSearchLookUpEditLookUpEdit2);
                Keyboard.SendKeys(aml + "{ENTER}");
            }

            if (internalPosNumber != "")
            {
                uITeInternalPositionNuEdit.ValueAsString = internalPosNumber;
                Keyboard.SendKeys(uITeInternalPositionNuEdit, "{Tab}");
            }

            // Click 'OK' button
            Click_OkNewPosition();
        }

        private void Click_OkNewPosition()
        {
            #region Variable Declarations
            DXButton uIOKButton = this.UIStillingsforholdWindow.UIDbFooterCustom.UIOKButton;
            #endregion

            var rec = uIOKButton.BoundingRectangle;
            Point point = new Point(rec.X, rec.Y);
            try
            {
                Playback.Wait(1000);
                Mouse.Click(uIOKButton);
            }
            catch (Exception)
            {
                Mouse.Click(point);
            }
        }

        /// <summary>
        /// SetRecalculatePeriodInTimesheetTab - Use 'SetRecalculatePeriodInTimesheetTabParams' to pass parameters into this method.
        /// </summary>
        public void SetRecalculatePeriodInTimesheetTab(string fromWeek, string toWeek)
        {
            #region Variable Declarations
            DXPopupEdit uIPceDatePopupEdit = this.UIRekalkuleringWindow.UIPcContentClient.UIWeFromWeekCustom.UIPceDatePopupEdit;
            DXPopupEdit uIPceDatePopupEdit1 = this.UIRekalkuleringWindow.UIPcContentClient.UIWeToWeekCustom.UIPceDatePopupEdit;
            #endregion


            //ValueAsString
            uIPceDatePopupEdit.ValueAsString = fromWeek;

            // Type '{Tab}' in 'pceDate' PopupEdit
            Keyboard.SendKeys(uIPceDatePopupEdit, "{Tab}");

            //ValueAsString
            uIPceDatePopupEdit1.ValueAsString = toWeek;

            // Type '{Tab}' in 'pceDate' PopupEdit
            Keyboard.SendKeys(uIPceDatePopupEdit1, "{Tab}");
        }

        public void ClickCancelInRecalculationWindow()
        {
            #region Variable Declarations
            UIRekalkuleringWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "Rekalkulering";
            DXButton uIAvbrytButton = this.UIRekalkuleringWindow.UIDbFooterCustom.UIAvbrytButton;
            #endregion

            // Click 'Avbryt' button
            Mouse.Click(uIAvbrytButton, new Point(1, 1));
        }

        #endregion

        #region Settings Window Functions

        public void SearchInPlanSettingsWindow(string searchString)
        {
            #region Variable Declarations
            DXTextEdit uITxtFilterEdit = this.UIArbeidsplanoppsettfoWindow.UIPnlFilterClient.UIGrpFilterClient.UITxtFilterEdit;
            #endregion

            uITxtFilterEdit.ValueAsString = searchString;
            Playback.Wait(1000);
            Keyboard.SendKeys(uITxtFilterEdit, "{Tab}");
        }

        public void OpenSearchResultLineInPlanSettingsWindow()
        {
            #region Variable Declarations
            Playback.Wait(2000);
            var uIMaksplanlagttidperdaNavBarGroupButton = this.UIArbeidsplanoppsettfoWindow.UINbcRosterSetupNavBar;
            var point = new Point(uIMaksplanlagttidperdaNavBarGroupButton.BoundingRectangle.X + 50, uIMaksplanlagttidperdaNavBarGroupButton.BoundingRectangle.Y + 10);
            #endregion

            Mouse.DoubleClick(point);
        }


        public void SetRosterplanRotation(DateTime? from, DateTime? to = null)
        {
            #region Variable Declarations
            DXDateTimePicker uIERevolveFromDateTimeEdit = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient1.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerRevolCustom.UIERevolveFromDateTimeEdit;
            DXDateTimePicker uIERevolveToDateTimeEdit = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient1.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerRevolCustom.UIERevolveToDateTimeEdit;
            #endregion

            if (from != null)
            {
                uIERevolveFromDateTimeEdit.DateTime = new DateTime(2024, 01, 22);
                Keyboard.SendKeys(uIERevolveFromDateTimeEdit, "{Tab}");
            }
            if (to != null)
            {
                uIERevolveToDateTimeEdit.DateTime = new DateTime(2024, 01, 22);
                Keyboard.SendKeys(uIERevolveToDateTimeEdit, "{Tab}");
            }
        }

        #endregion

        #region Progressbar functions
        public bool CheckProgressbarActive(bool active)
        {
            #region Variable Declarations
            DXTestControl uIPaBorderClient = this.UINewProgressBarFormWindow.UIPaBorderClient;
            Playback.Wait(3000);
            #endregion

            try
            {
                if (active)
                    return uIPaBorderClient.WaitForControlCondition(IsProgressbarVisible);
                else
                    return uIPaBorderClient.WaitForControlCondition(IsProgressbarNotVisible);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool IsProgressbarVisible(UITestControl control)
        {
            var progressbar = control as DXTestControl;
            while (progressbar.Visible)
            {
                Playback.Wait(1000);
            }

            return true;
        }

        private static bool IsProgressbarNotVisible(UITestControl control)
        {
            var progressbar = control as DXTestControl;
            while (!progressbar.Visible)
            {
                Playback.Wait(1000);
            }

            return true;
        }
        #endregion

        #region Gat&MinGat Functions from zipfiles
        private static string DestinationAddressZipFiles = Path.Combine(TestData.GetWorkingDirectory, @"ZipFiles");
        private string UIExtractedGatFiles = DestinationAddressZipFiles + @"\Gat_no";
        private string AddressGatIni = DestinationAddressZipFiles + @"\Gat_no\Template";

        private string CurrentServer
        {
            get
            {
                try
                {
                    return TestData.GetDatabaseServer;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get Servername: " + ex.Message);
                }

                return null;
            }
        }
        private string CurrentDataBase
        {
            get
            {
                try
                {
                    return TestData.GetDataBase;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get Databasename: " + ex.Message);
                }

                return null;
            }
        }
        private string CurrentDBUser
        {
            get
            {
                try
                {
                    return TestData.GetDBUser;
                }
                catch (Exception ex)
                {
                    TestContext.WriteLine("Unable to get username: " + ex.Message);
                }

                return null;
            }
        }
        private string CurrentDBAuth(bool decrypt = true)
        {
            try
            {
                return TestData.GetDBAuth(decrypt);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Unable to get authentication: " + ex.Message);
            }

            return null;
        }
        public string SqlConnection()
        {
            var connection = "";

            try
            {
                connection = GetConnectionString(CurrentServer, CurrentDataBase, CurrentDBUser, CurrentDBAuth());
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Unable to get SqlConnection: " + ex.Message);
            }

            return connection;

        }
        private string GetConnectionString(string server, string database, string user, string passWord)
        {
            //var connection_org = @"<add name=""default"" connectionString=""TYPE=MSSQL2008;HOSTNAME=" + server + @";DATABASE=" + database + @";USERNAME=sa;PASSWORD=tellus"" />";
            var connection = @"<add name=""default"" connectionString=""TYPE=MSSQL2008;HOSTNAME=" + server + @";DATABASE=" + database + @";USERNAME=" + user + @";PASSWORD=" + passWord + @"""/>";

            return connection;
        }
        public void ExtractGatFiles(string filePath, TestContext testContext)
        {
            SupportFunctions.ExtractFiles(filePath, UIExtractedGatFiles, testContext);
            SupportFunctions.FileCopy("GATTURNUS.ini", AddressGatIni, UIExtractedGatFiles, testContext);
        }
        public static bool CheckFileExists(string file, string sourcePath)
        {
            if (!Directory.Exists(sourcePath))
                return false;

            string sourceFile = Path.Combine(sourcePath, file);
            return File.Exists(sourceFile);
        }
        public void GetGatZipFiles(List<string> files, string sourcePath, string destinationAddressZipFiles, string currentFileVersion, TestContext testContext)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(destinationAddressZipFiles);

            // Only get files that contains "file"
            var filePaths = Directory.GetFiles(sourcePath, "*" + currentFileVersion + "*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                foreach (string filePath in filePaths)
                {
                    FileInfo mFile = new FileInfo(filePath);
                    var fileName = mFile.Name.Replace(currentFileVersion + ".zip", "").TrimEnd();

                    try
                    {
                        if (file == fileName)
                        {
                            CommonTestData.SupportFunctions.FileCopy(mFile.Name, sourcePath, destinationAddressZipFiles, testContext);
                            var fullName = mFile.FullName;
                            var created = mFile.CreationTime;

                            testContext.WriteLine("Fil kopiert fra server: " + fullName + ", Produsert:  " + created);
                        }
                    }
                    catch (Exception e)
                    {
                        testContext.WriteLine("Unable to copy file: " + mFile.Name + ", " + e.Message);
                    }
                }
            }
        }
        public void ConfigureMinGatForIIS(string filePath, string extractedMinGatFiles, string minGatAppsettingsConfig, string lineToFindMinGatAppsettings, string minGatWishPlanKey, string minGatConnectionStringsConfig, string lineToFindConnectionString, string sqlConnection, string wwwRootMinGatDir, TestContext testContext)
        {

            SupportFunctions.ExtractFiles(filePath, extractedMinGatFiles, testContext);

            SupportFunctions.EditTextFile(minGatAppsettingsConfig, lineToFindMinGatAppsettings, minGatWishPlanKey);
            SupportFunctions.EditTextFile(minGatConnectionStringsConfig, lineToFindConnectionString, sqlConnection);

            //kopiere filer fra template til Mingat
            var filesToCopy = new List<string>() { "appSettings.config", "connectionStrings.config", "globalization.config", "machineKey.config", "saml.config", "session.config" };

            foreach (var fileToCopy in filesToCopy)
            {
                SupportFunctions.FileCopy(fileToCopy, extractedMinGatFiles + @"\Template", extractedMinGatFiles, testContext);
            }

            //Kopiere MinGatkatalog over på wwwroot
            string sourcePath = extractedMinGatFiles;
            string targetPath = wwwRootMinGatDir;
            SupportFunctions.DirectoryCopy(sourcePath, targetPath, true);
        }

        /// <summary>
        /// RemoveApplicationPoolsMinGat
        /// </summary>
        public void RemoveApplicationPoolsMinGat()
        {
            #region Variable Declarations
            WinTreeItem uIApplicationPoolsTreeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UINOLABWS0018INTERNALgTreeItem2.UIApplicationPoolsTreeItem;
            WinListItem uIMinGatListItem = this.UIInternetInformationSWindow.UI_listViewWindow.UI_listViewList.UIMinGatListItem;
            WinMenuItem uIRemoveMenuItem = this.UIItemWindow16.UIDropDownMenu.UIRemoveMenuItem;
            WinButton uIYesButton = this.UIConfirmRemoveWindow.UIYesWindow.UIYesButton;
            #endregion

            // Click 'NO-LAB-WS-0018 (INTERNAL\geir.gaustad)' -> 'Application Pools' tree item
            Mouse.Click(uIApplicationPoolsTreeItem, new Point(72, 9));

            // Right-Click 'MinGat' list item
            Mouse.Click(uIMinGatListItem, MouseButtons.Right);

            //try
            //{
            //    // Click 'Remove' menu item
            //    Mouse.Click(uIRemoveMenuItem);
            //}
            //catch (Exception)
            //{
                Keyboard.SendKeys("{DOWN 10}{ENTER}");
           // }         

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(29, 12));
        }
        public void ConnectGatToDataBase()
        {
            #region Variable Declarations
            string defaultPassword = "0EtxjE8Yeo50s1ZD4LvyTLMfio+bDxFx";
            WinButton uIAddButton = this.UIDatabaseLoginWindow.UIStandardClient.UIAddButton;
            WinButton uIItemButton = this.UIConnectionparametersWindow.UIItemWindow.UIItemButton;
            WinListItem uIMicrosoftOLEDBProvidListItem = this.UIDataLinkPropertiesWindow.UISelectthedatayouwantList.UIMicrosoftOLEDBProvidListItem;
            WinComboBox uIItem1SelectorenterasComboBox = this.UIDataLinkPropertiesWindow.UIItemWindow1.UIItem1SelectorenterasComboBox;
            WinEdit uIUsernameEdit = this.UIDataLinkPropertiesWindow.UIItemWindow11.UIUsernameEdit;
            WinEdit uIUsernameEdit1 = this.UIDataLinkPropertiesWindow.UIItemWindow12.UIUsernameEdit;
            WinEdit uIPasswordEdit = this.UIDataLinkPropertiesWindow.UIItemWindow2.UIPasswordEdit;
            WinCheckBox uIAllowsavingpasswordCheckBox = this.UIDataLinkPropertiesWindow.UIAllowsavingpasswordWindow.UIAllowsavingpasswordCheckBox;
            WinComboBox uIItemComboBox = this.UIDataLinkPropertiesWindow.UIItemWindow3.UIItemComboBox;
            WinEdit uIItemEdit = this.UIDataLinkPropertiesWindow.UIItemWindow4.UIItemEdit;
            WinMenuItem uICopyMenuItem = this.UIItemWindow14.UIContextMenu.UICopyMenuItem;
            WinButton uIOKButton = this.UIDataLinkPropertiesWindow.UIOKWindow.UIOKButton;
            WinEdit uIItemEdit1 = this.UIConnectionparametersWindow.UIItemWindow1.UIItemEdit;
            WinButton uIOKButton1 = this.UIConnectionparametersWindow.UIConnectionparametersClient.UIOKButton;
            WinButton uIOKButton2 = this.UIDatabaseLoginWindow.UIStandardClient.UIOKButton;
            #endregion

            try
            {
                Playback.Wait(3000);
                if (!UIDatabaseLoginWindow.Exists)
                    return;
            }
            catch (Exception)
            {
                return;
            }

            var dBase = CurrentDataBase;

            // Click 'Add' button
            Mouse.Click(uIAddButton);

            // Click button
            Mouse.Click(uIItemButton);

            // Double-Click 'Microsoft OLE DB Provider for SQL Server' list item
            Mouse.DoubleClick(uIMicrosoftOLEDBProvidListItem, new Point(193, 9));

            // Insert servername
            InsertServerName(CurrentServer);

            // Type 'username' in 'User name:' text box
            uIUsernameEdit.Text = CurrentDBUser;

            // Type '{Tab}' in 'User name:' text box
            Keyboard.SendKeys(uIUsernameEdit1, "{TAB}", ModifierKeys.None);

            // Type '********' in 'Password:' text box
            Keyboard.SendKeys(uIPasswordEdit, CurrentDBAuth(false), true);
            //uIPasswordEdit.Password = defaultPassword;

            // Select 'Allow &saving password' check box
            uIAllowsavingpasswordCheckBox.Checked = true;

            // Select 'DB in combo box
            uIItemComboBox.SelectedItem = dBase;

            // Click 'OK' button
            Mouse.Click(uIOKButton);

            // Click text box
            Keyboard.SendKeys(uIItemEdit1, dBase);
            Keyboard.SendKeys(uIItemEdit1, "{TAB}");

            // Click 'OK' button
            Mouse.Click(uIOKButton1);

            // Click 'OK' button
            Mouse.Click(uIOKButton2);
        }
        public void CheckUIGT2004AdvarselWindowExists(TestContext testContext)
        {
            // Click '&Ja' button
            try
            {
                if (UIGT2004AdvarselWindow.Exists)
                    ClickOkUpgradeGatDB();
            }
            catch
            { testContext.WriteLine("No update of database"); }
        }
        public void ClickOkUpgradeGatDB()
        {
            #region Variable Declarations
            WinButton uIJAButton = this.UIGT2004AdvarselWindow.UIItemWindow.UIGT2004AdvarselClient.UIJAButton;
            #endregion

            // Click '&Ja' button
            Mouse.Click(uIJAButton, new Point(44, 10));
        }
        public void CheckAndCloseMinGatCookieMessage(TestContext testContext)
        {
            #region Variable Declarations
            var CookieDisclaimer = this.UIStartsidenMinGatv201Window.UIStartsidenMinGatv201Document.UICookieDisclaimerCustom;
            #endregion
            try
            {
                if (CookieDisclaimer.Exists)
                    CloseMinGatCookieMessage();
            }
            catch (Exception e)
            {
                testContext.WriteLine("Cookiedisclaimererror: " + e.Message);
            }

        }
        public void MinGatLogin(string userName)
        {
            #region Variable Declarations
            string defaultPassword = "VLLA+bJzNf882FWpmiwJPY0v6P7+sGGK";
            HtmlEdit uIBrukernavnEdit = this.UIMinGatMinGatv2020120Window.UIMinGatMinGatv2020120Document.UIBrukernavnEdit;
            HtmlEdit uIPassordEdit = this.UIMinGatMinGatv2020120Window.UIMinGatMinGatv2020120Document.UIPassordEdit;
            HtmlSpan uILogginnPane = this.UIMinGatMinGatv2020120Window.UIMinGatMinGatv2020120Document.UILogginnButton.UILogginnPane;
            #endregion

            // Type 'user' in 'Brukernavn*' text box
            Playback.Wait(1500);
            uIBrukernavnEdit.WaitForControlReady();
            uIBrukernavnEdit.Text = userName;

            // Type '{Tab}' in 'Brukernavn*' text box
            Keyboard.SendKeys(uIBrukernavnEdit, "{Tab}");

            // Type '********' in 'Passord*' text box
            Playback.Wait(1500);
            uIPassordEdit.WaitForControlReady();
            uIPassordEdit.Password = defaultPassword;
            Keyboard.SendKeys(uIPassordEdit, "{Tab}");

            // Click 'Logg inn' pane
            Playback.Wait(1000);
            Mouse.Click(uILogginnPane, new Point(148, 13));
        }

        #endregion

        #region ExportToExcel Functions

        public void DeleteReportFiles(string reportFilePath)
        {
            DirectoryInfo downloadedMessageInfo = new DirectoryInfo(reportFilePath);
            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
            {
                if ((file.Extension != ".prnx" && file.Extension != ".xls" && file.Extension != ".xlsx") || file.Name.Contains("Facit"))
                    continue;

                file.Delete();
            }
        }

        public void ExportToExcelFromRosterplanPreview(string name)
        {
            #region Variable Declarations
            DXMenuItem uIFilBarItem = this.UIForhåndsvisningWindow.UIBarDockControlCustom.UIMainMenuMenuBar.UIFilBarItem;
            DXButton uIBarItemOpenArrowButton = this.UIForhåndsvisningWindow.UIBarDockControlCustom.UIMainMenuMenuBar.UIFilBarItem.UIEksporterdokumentMenuItem.UIBarItemOpenArrowButton;
            DXMenuBaseButtonItem uIXLSfilMenuBaseButtonItem = this.UIForhåndsvisningWindow.UIBarDockControlCustom.UIMainMenuMenuBar.UIFilBarItem.UIEksporterdokumentMenuItem.UIXLSfilMenuBaseButtonItem;
            DXButton uIOKButton = this.UIEksportertilXLSWindow.UILayoutControl1Custom.UIRootLayoutGroup.UIGrpButtonsLayoutGroup.UILayoutControlItemOKLayoutControlItem.UIOKButton;
            WinComboBox uIFilenameComboBox = this.UISaveAsWindow.UIDetailsPanePane.UIFilenameComboBox;
            WinEdit uIFilenameEdit = this.UISaveAsWindow.UIItemWindow.UIFilenameEdit;
            WinButton uISaveButton = this.UISaveAsWindow.UISaveWindow.UISaveButton;
            DXButton uINeiButton = this.UIItemWindow11.UIEksportWindow.UINeiButton;
            #endregion

            // Click '&Fil' BarItem
            Mouse.Click(uIFilBarItem);
            Playback.Wait(500);
            Keyboard.SendKeys("{DOWN 4}{RIGHT}{DOWN}{ENTER}");

            Mouse.Click(uIOKButton);

            // Type in 'File name:' text box
            Keyboard.SendKeys(uIFilenameEdit, name + "{Tab}");

            // Click '&Save' button
            Mouse.Click(uISaveButton);

            try
            {
                Playback.Wait(1000);
                Keyboard.SendKeys("{TAB}");
                Keyboard.SendKeys("{ENTER}");
            }
            catch (Exception)
            {
                Keyboard.SendKeys("n", ModifierKeys.Alt);
                TestContext.WriteLine("Error closing Export/Open reportfile dialogbox");
            }
        }

        /// <summary>
        /// SaveFromGatExcelDialog - Use 'SaveFromGatExcelDialogParams' to pass parameters into this method.
        /// </summary>
        public void SaveFromGatExcelDialog(string name)
        {
            #region Variable Declarations
            WinMenuItem uIItemMenuItem = this.UIItemWindow14.UIContextMenu.UIItemMenuItem;
            WinComboBox uIFilenameComboBox = this.UIExcelWindow.UIDetailsPanePane.UIFilenameComboBox;
            WinEdit uIFilenameEdit = this.UIExcelWindow.UIItemWindow.UIFilenameEdit;
            WinButton uISaveButton = this.UIExcelWindow.UISaveWindow.UISaveButton;
            #endregion

            // Click menu item numbered 2 in 'Context' menu item
            Mouse.Click(uIItemMenuItem);

            uIFilenameComboBox.EditableItem = name;
            Keyboard.SendKeys(uIFilenameEdit, "{Tab}");

            // Click '&Save' button
            Mouse.Click(uISaveButton);
        }
        #endregion

        #region Select Department Functions
        /// <summary>
        /// SelectDepartmentBySearch - Use 'SelectDepartmentBySearchParams' to pass parameters into this method.
        /// </summary>
        public void SelectDepartmentBySearch(string depName)
        {
            #region Variable Declarations
            DXTextEdit uITxtFilterEdit = this.UINivåWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem1LayoutControlItem.UIGsPanelControl1Client.UITxtFilterEdit;
            #endregion

            Mouse.Click(uITxtFilterEdit);
            uITxtFilterEdit.ValueAsString = depName;
            Playback.Wait(500);
            Keyboard.SendKeys(uITxtFilterEdit, "{ENTER}");
        }

        #endregion

        #region General Error Messages

        public List<string> CheckAndCloseGeneralErrorMessage()
        {
            #region Variable Declarations          
            var errorList = new List<string>();
            DXTestControl uIDetharoppståttenfeilWindowTitleBar = this.UIDetharoppståttenfeilWindow.UIDetharoppståttenfeilWindowTitleBar;
            DXCheckBox uIRbExitApplicationCheckBox = this.UIDetharoppståttenfeilWindow.UITcWizardPagesTabList.UITpCompleteClient.UIRbExitApplicationCheckBox;
            DXCheckBox uIRbReturnToApplicatioCheckBox = this.UIDetharoppståttenfeilWindow.UITcWizardPagesTabList.UITpCompleteClient.UIRbReturnToApplicatioCheckBox;
            WinClient uIPbTextInformationClient = this.UIDetharoppståttenfeilWindow.UITcWizardPagesTabList.UITpCompleteClient.UIPbTextInformationWindow.UIPbTextInformationClient;
            WinEdit uIETextEdit = this.UITekstinformasjonforsWindow.UIETextEdit;
            DXButton uILukkButton = this.UITekstinformasjonforsWindow.UIPaButtonsClient.UILukkButton;
            DXButton uIKopierButton = this.UITekstinformasjonforsWindow.UIPaButtonsClient.UIKopierButton;
            DXButton uIUtførButton = this.UIDetharoppståttenfeilWindow.UIUtførButton;
            //Mouse.Click(uIKopierButton);
            //var checkReturToApp = uIRbReturnToApplicatioCheckBox.CheckStateAsString;
            //uIRbReturnToApplicatioCheckBox.Checked = true;
            //var checkExitApp = uIRbExitApplicationCheckBox.CheckStateAsString;
            Playback.Wait(5000);
            #endregion

            try
            {
                if (!uIDetharoppståttenfeilWindowTitleBar.Exists)
                    return errorList;
            }
            catch
            {
                return errorList;
            }

            var text = uIDetharoppståttenfeilWindowTitleBar.GetProperty("ControlName").ToString();
            errorList.Add(text);

            Mouse.Click(uIPbTextInformationClient);
            errorList.Add(uIETextEdit.Text);

            Mouse.Click(uILukkButton);

            uIRbExitApplicationCheckBox.Checked = true;
            Mouse.Click(uIUtførButton);
            ClickOkTerminateApplication();

            return errorList;
        }

        #endregion



    }

}
