namespace _020_Test_Arbeidsplan_Redigering_GATP_740
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
    using System.Threading;
    using System.Globalization;
    using System.Diagnostics;
    using System.IO;

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
        public void CreateRosterplan(string name, DateTime startDate, string weeks, DateTime validDate)
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            UICommon.ClickNewRosterplanButton();
            UICommon.UIMapVS2017.SetRosterPlanName(name);
            UICommon.SetStartDateRosterplan(startDate);
            UICommon.UIMapVS2017.SetRosterPlanWeeks(weeks);
            UICommon.SetValidToDateRosterplan(validDate);
            UICommon.ClickOkRosterplanSettings();
        }

        public List<String> AddEmployeesToRosterPlan()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.SelectRosterplanPlanTab();
                UICommon.ClickEmployeesButtonRosterplan();
                UICommon.ClickEmployeesButtonInEmployeeWindow();
                SelectFourEmployees();
                UICommon.ClickOkAddEmployeesWindow();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }
        public List<String> CreateWeekendPattern()
        {
            var errorList = new List<string>();

            try
            {
                CreateWeekenPatternThreeLines();
                UICommon.ClickOkEmployeesWindow();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }

        private void CreateWeekenPatternThreeLines()
        {
            #region Variable Declarations
            var uIItem1AndersenAndersTreeListCell = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeeTreeList.UINode0TreeListNode.UIItem1AndersenAndersTreeListCell;
            var uILaghelgemønsterButton = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerEmploCustom.UILaghelgemønsterButton;
            //var uIOKButton = this.UIItemWindow.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UIOKButton;
            var uIItem2AndersenAstridTreeListCell = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeeTreeList.UINode1TreeListNode.UIItem2AndersenAstridTreeListCell;
            #endregion

            // Click '1. Andersen, Anders' TreeListCell
            Mouse.Click(uIItem1AndersenAndersTreeListCell);

            // Click 'Lag helgemønster' button
            Mouse.Click(uILaghelgemønsterButton);
            Keyboard.SendKeys("{TAB}1{TAB}{ENTER}");

            // Click '2. Andersen, Astrid' TreeListCell
            Mouse.Click(uIItem2AndersenAstridTreeListCell);

            // Click 'Lag helgemønster' button
            Mouse.Click(uILaghelgemønsterButton);
            Keyboard.SendKeys("{TAB}2{TAB}{ENTER}");

            SelectLineThree();

            // Click 'Lag helgemønster' button
            Mouse.Click(uILaghelgemønsterButton);

            Keyboard.SendKeys("{TAB}3{TAB}{ENTER}");
        }

        public List<String> Step_5()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickEditRosterPlanFromPlantab();
                AddFreeCodesToPlan();
                UICommon.ClickOKEditRosterPlanFromPlantab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 5: " + e.Message);
            }

            try
            {
                CheckFreeCodesAdded();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 5_2: " + e.Message);
            }

            return errorList;
        }

        public void AddFreeCodesToPlan()
        {
            #region Variable Declarations
            DXColumnHeader uIRosterCell_0ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRosterCell_0ColumnHeader;
            DXCell uIItemCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            #endregion

            // Double-Click 'RosterCell_0' column header
            Mouse.DoubleClick(uIRosterCell_0ColumnHeader, new Point(14, 7));

            // Right-Click cell
            Mouse.Click(uIItemCell, MouseButtons.Right, ModifierKeys.None, new Point(13, 10));

            // Click 'Automatisk innsetting av frikoder' menu item
            UICommon.SelectFunctionInRosterplanRightClickMenu("10");
            // Click 'Sett inn F2/F1 på frihelger.' MenuBaseButtonItem
            Keyboard.SendKeys("{TAB}{ENTER}");
        }

        public List<String> Step_6()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickEditRosterPlanFromPlantab();
                AddShiftsLine1Week1_Step_6();
                AddShiftsLine1Week2_Step_6();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 6: " + e.Message);
            }


            return errorList;
        }

        private void AddShiftsLine1Week2_Step_6()
        {
            #region Variable Declarations
            DXCell uIItemCell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell1;
            DXCell uIItemCell11 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell11;
            DXButton uIJAButton = this.UIGT4003InformasjonWindow.UIJAButton;
            #endregion

            // Move cell to cell
            uIItemCell11.EnsureClickable();
            Mouse.StartDragging(uIItemCell1);
            Mouse.StopDragging(uIItemCell11);

            // Right-Click 'Desktop' client
            Mouse.Click(uIItemCell11, MouseButtons.Right);

            // Click 'Nattvakt' menu item
            UICommon.SelectFunctionInRosterplanRightClickMenu("2");

            // Click 'N2 (22.30 - 07.30)' MenuBaseButtonItem
            Keyboard.SendKeys("{TAB}{DOWN}{ENTER}");

            // Click '&Ja' button
            Mouse.Click(uIJAButton);
        }

        public List<String> Step_7()
        {
            var errorList = new List<string>();

            try
            {
                AddShifts_Step_7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 7: " + e.Message);
            }

            try
            {
                CheckErrorMessage_Step_7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 7_2: " + e.Message);
            }

            return errorList;
        }

        private void AddShifts_Step_7()
        {
            #region Variable Declarations
            var table = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            DXCell uIItemCell2 = table.UIItemCell2;
            DXTextEdit uIRow0ColumnRosterCellEdit7 = table.UIRow0ColumnRosterCellEdit7;
            DXCell uIItemCell12 = table.UIItemCell12;
            DXTextEdit uIRow0ColumnRosterCellEdit41 = table.UIRow0ColumnRosterCellEdit41;

            var tableCell = new DXCell(table);
            tableCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanGridControlCell[View]gvRosterPlan[Row]0[Column]RosterCell_18";
            tableCell.SearchProperties[DXTestControl.PropertyNames.ClassName] = "GridControlCell";
            #endregion

            Mouse.Click(uIItemCell2, MouseButtons.Right);

            // Click 'Kombinasjonskode' menu item
            UICommon.SelectFunctionInRosterplanRightClickMenu("8");
            Keyboard.SendKeys("{TAB}{ENTER}");

            Mouse.Click(uIItemCell12, MouseButtons.Right);

            // Click 'Kombinasjonskode' menu item
            UICommon.SelectFunctionInRosterplanRightClickMenu("8");
            Keyboard.SendKeys("{TAB}{DOWN}{ENTER}");

            Mouse.Click(tableCell);
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit41, "kk5{Tab}");
        }

        public List<String> Step_8()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickOKEditRosterPlanFromPlantab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 8: " + e.Message);
            }

            try
            {
                CheckMessage_Step_8();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 8_2: " + e.Message);
            }

            return errorList;
        }


        public List<String> Step_9()
        {
            var errorList = new List<string>();

            try
            {
                ClickOkMessage_Step_9();
                RemoveKK5InsertKK4();
                UICommon.ClickOKEditRosterPlanFromPlantab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 9: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_10()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickEditRosterPlanFromPlantab();
                UICommon.ClickRosterplanHomeTab();
                AddBVShiftLine2_Step_10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 10: " + e.Message);
            }

            try
            {
                CheckInsertButton_Step_10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 10_2: " + e.Message);
            }

            return errorList;
        }

        private void AddBVShiftLine2_Step_10()
        {
            #region Variable Declarations
            DXCell uIItemCell3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell3;
            DXTextEdit uIRow1ColumnRosterCellEdit = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit;
            //DXMenuItem uIBakvaktMenuItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIBakvaktMenuItem;
            //DXMenuBaseButtonItem uIBV15002200MenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIBakvaktMenuItem.UIBV15002200MenuBaseButtonItem;
            DXRibbonButtonItem uISettinnRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpRosterPlanRibbonPage.UIGrpInsertToolsRibbonPageGroup.UISettinnRibbonBaseButtonItem;
            #endregion

            Mouse.Click(uIItemCell3, MouseButtons.Right);
            Keyboard.SendKeys("{DOWN 5}{RIGHT}{ENTER}");

            //// Click 'Bakvakt' menu item
            //Mouse.Click(uIBakvaktMenuItem);

            //// Click 'BV (15.00 - 22.00)' 
            //Mouse.Click(uIBV15002200MenuBaseButtonItem);

            // Click 'Sett inn' RibbonBaseButtonItem
            Mouse.Click(uISettinnRibbonBaseButtonItem);
        }

        public List<String> Step_11()
        {
            var errorList = new List<string>();

            try
            {
                AddBVShifts_Step_11();
                DeactivateInsertButton();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 11: " + e.Message);
            }

            try
            {
                CheckShiftcodes_Step_11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 11_2: " + e.Message);
            }

            return errorList;
        }
        public List<String> Step_12()
        {
            var errorList = new List<string>();

            try
            {
                CutAndPasteShifts_Step_12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 12: " + e.Message);
            }

            try
            {
                CheckShiftcodes_Step_12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 12_2: " + e.Message);
            }

            return errorList;
        }

        private void CutAndPasteShifts_Step_12()
        {
            #region Variable Declarations
            DXCell uIItemCell3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell3;
            DXCell uIBVCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBVCell;
            //DXMenuBaseButtonItem uIKlipputMenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIKlipputMenuBaseButtonItem;
            DXButton uIJAButton = this.UIGT4001InformasjonWindow.UIJAButton;
            DXCell uIItemCell5 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell5;
            DXRibbonButtonItem uILiminnRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpRosterPlanRibbonPage.UIGrpEditToolsRibbonPageGroup.UILiminnRibbonBaseButtonItem;
            #endregion

            // Move cell to 'BV' cell
            uIBVCell.EnsureClickable();
            Mouse.StartDragging(uIItemCell3);
            Mouse.StopDragging(uIBVCell);

            // Right-Click 'Desktop' client
            Mouse.Click(uIBVCell, MouseButtons.Right);

            Keyboard.SendKeys("{DOWN 13}{ENTER}");

            //// Click 'Klipp ut' MenuBaseButtonItem
            //Mouse.Click(uIKlipputMenuBaseButtonItem);

            // Click '&Ja' button
            Mouse.Click(uIJAButton);

            // Click cell
            Mouse.Click(uIItemCell5);

            // Click 'Lim inn' RibbonBaseButtonItem
            Mouse.Click(uILiminnRibbonBaseButtonItem);
        }

        public List<String> Step_13()
        {
            var errorList = new List<string>();

            try
            {
                RegretCutAndPasteShifts_Step_13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 13: " + e.Message);
            }

            try
            {
                CheckShiftcodes_Step_13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 13_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_14()
        {
            var errorList = new List<string>();

            try
            {
                RegretCutAndPasteShifts_Step_14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 14: " + e.Message);
            }

            try
            {
                CheckShiftcodes_Step_14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 14_2: " + e.Message);
            }

            return errorList;
        }

        private void RegretCutAndPasteShifts_Step_14()
        {
            #region Variable Declarations
            DXCell uIItemCell6 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell6;
            //DXMenuBaseButtonItem uIAngresisteMenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIAngresisteMenuBaseButtonItem;
            #endregion

            // Click cell
            Mouse.Click(uIItemCell6, MouseButtons.Right);

            Keyboard.SendKeys("{DOWN 15}{ENTER}");
            //// Click 'Angre siste' MenuBaseButtonItem
            //Mouse.Click(uIAngresisteMenuBaseButtonItem, new Point(95, 9));
        }

        public List<String> Step_15()
        {
            var errorList = new List<string>();

            try
            {
                DeleteShifts_Step_15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 15: " + e.Message);
            }

            try
            {
                CheckShiftcodes_Step_15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 15_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_16()
        {
            var errorList = new List<string>();

            try
            {
                InsertLShifts_Step_16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 16: " + e.Message);
            }

            try
            {
                CheckSErrorMessage_Step_16();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 16_2: " + e.Message);
            }

            DeleteErrorLShifts_Step_16();

            return errorList;
        }

        private void InsertLShifts_Step_16()
        {
            #region Variable Declarations
            DXCell uIItemCell7 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell7;
            //DXMenuBaseButtonItem uIL08001600MenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UILedervaktMenuItem.UIL08001600MenuBaseButtonItem;
            DXGrid uIGcRosterPlanTable = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            DXTextEdit uIRow1ColumnRosterCellEdit3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit3;
            #endregion

            // Click cell
            Mouse.Click(uIItemCell7, MouseButtons.Right);

            //// Click 'L (08.00 - 16.00)' MenuBaseButtonItem
            //Mouse.Click(uIL08001600MenuBaseButtonItem, new Point(56, 10));
            Keyboard.SendKeys("{DOWN 4}{RIGHT}{ENTER}");

            Keyboard.SendKeys(uIItemCell7, "{Tab}");

            // Type '{Tab}l' in 'gcRosterPlan' table
            Keyboard.SendKeys(uIRow1ColumnRosterCellEdit3, "l{Tab}");
        }
        private void DeleteErrorLShifts_Step_16()
        {
            #region Variable Declarations
            DXTextEdit uIRow1ColumnRosterCellEdit3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit3;
            #endregion

            Keyboard.SendKeys(uIRow1ColumnRosterCellEdit3, "a", ModifierKeys.Control);
            Keyboard.SendKeys(uIRow1ColumnRosterCellEdit3, "{Delete}{Tab}", ModifierKeys.Control);
        }

        public List<String> Step_17()
        {
            var errorList = new List<string>();

            try
            {
                SelectCell_Step_17();
                InsertLShifts_Step_17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 17: " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Feiladvarsler);

                UICommon.UIMapVS2017.CheckShowOnlyForSelectedEmpInWarningsSubTab();
                CheckWarnings_Step_17();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 17_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_18()
        {
            var errorList = new List<string>();

            try
            {
                DeleteErrorLShifts_Step_18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 18: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_19()
        {
            var errorList = new List<string>();

            try
            {
                SelectShift_Step_19();
                ReplaceBVShifts_Step_19();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 19: " + e.Message);
            }

            try
            {
                CheckShiftcodes_Step_19();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 19_2: " + e.Message);
            }

            return errorList;
        }
        private void ReplaceBVShifts_Step_19()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIErstattvaktkoderRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpRosterPlanRibbonPage.UIGrpFunctionsRibbonPageGroup.UIErstattvaktkoderRibbonBaseButtonItem;
            DXComboBox uICbFromCodeComboBox = this.UISøkogErstattWindow.UIGrpFindClient.UICbFromCodeComboBox;
            DXComboBox uICbToCodeComboBox = this.UISøkogErstattWindow.UIGrpReplaceClient.UICbToCodeComboBox;
            DXButton uIOKButton = this.UISøkogErstattWindow.UIPnlButtonsClient.UIOKButton;
            #endregion

            // Click 'Erstatt vaktkoder' RibbonBaseButtonItem
            Mouse.Click(uIErstattvaktkoderRibbonBaseButtonItem);

            // Select 'BV' in 'cbFromCode' combo box
            //ValueAsString
            uICbFromCodeComboBox.ValueAsString = "BV";

            // Type '{Tab}' in 'cbFromCode' combo box
            Keyboard.SendKeys(uICbFromCodeComboBox, "{Tab}");

            // Select 'N2' in 'cbToCode' combo box
            //ValueAsString
            uICbToCodeComboBox.ValueAsString = "N2";

            // Type '{Tab}' in 'cbToCode' combo box
            Keyboard.SendKeys(uICbToCodeComboBox, "{Tab}");

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }

        public List<String> Step_20()
        {
            var errorList = new List<string>();

            try
            {
                ReplaceBVShifts_Step_20();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 20: " + e.Message);
            }

            try
            {
                CheckShiftcodes_Step_20();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 20_2: " + e.Message);
            }

            return errorList;
        }

        private void ReplaceBVShifts_Step_20()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIErstattvaktkoderRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpRosterPlanRibbonPage.UIGrpFunctionsRibbonPageGroup.UIErstattvaktkoderRibbonBaseButtonItem;
            DXComboBox uICbFromCodeComboBox = this.UISøkogErstattWindow.UIGrpFindClient.UICbFromCodeComboBox;
            DXComboBox uICbToCodeComboBox = this.UISøkogErstattWindow.UIGrpReplaceClient.UICbToCodeComboBox;
            DXButton uIOKButton = this.UISøkogErstattWindow.UIPnlButtonsClient.UIOKButton;
            #endregion

            // Click 'Erstatt vaktkoder' RibbonBaseButtonItem
            Mouse.Click(uIErstattvaktkoderRibbonBaseButtonItem);

            // Select 'BV' in 'cbFromCode' combo box
            //ValueAsString
            uICbFromCodeComboBox.ValueAsString = "BV";

            // Type '{Tab}' in 'cbFromCode' combo box
            Keyboard.SendKeys(uICbFromCodeComboBox, "{Tab}");

            // Select 'N2' in 'cbToCode' combo box
            //ValueAsString
            uICbToCodeComboBox.ValueAsString = "A3";

            // Type '{Tab}' in 'cbToCode' combo box
            Keyboard.SendKeys(uICbToCodeComboBox, "{Tab}");

            SelectWholePlan();

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }

        private void SelectWholePlan()
        {
            #region Variable Declarations
            DXLookUpEdit uILeAreaLookUpEdit = this.UISøkogErstattWindow.UIGrpAreaClient.UIPcAreaClient.UILeAreaLookUpEdit;
            #endregion

            Mouse.Click(uILeAreaLookUpEdit);
            Keyboard.SendKeys(uILeAreaLookUpEdit, "Hele{Enter}", ModifierKeys.None);

        }

        public List<String> Step_21()
        {
            var errorList = new List<string>();

            try
            {
                OpenReduceShift_Step_21();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 21: " + e.Message);
            }

            try
            {
                CheckSaveDisabled_Step_21();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 21_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_22()
        {
            var errorList = new List<string>();

            try
            {
                ReduceShift_Step_22();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 22: " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Detaljer);
                UpdateCell_22();
                CheckShiftReduced_Step_22();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 22_2: " + e.Message);
            }

            return errorList;
        }
        private void UpdateCell_22()
        {
            #region Variable Declarations
            DXCell uIItemCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            #endregion

            Keyboard.SendKeys(uIItemCell, "{Right}{Left}");
        }
        public List<String> Step_23()
        {
            var errorList = new List<string>();

            try
            {
                SelectShift_Step_23();
                OpenUtjevning();
                ExtendShift_Step_23();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 23: " + e.Message);
            }

            try
            {
                CheckOkDisabled_Step_23();
                UICommon.UIMapVS2017.OpenRegStatusUtjevning();
                CheckRegStatusMessage_Step_23();
                UICommon.UIMapVS2017.CloseRegStatusUtjevning();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 23_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_24()
        {
            DXButton uIOKButton = this.UIUtjevningsvaktWindow.UIPnlButtonsClient.UIOKButton;
            var errorList = new List<string>();

            try
            {
                ExtendShift_Step_24();
                Mouse.Click(uIOKButton);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 24: " + e.Message);
            }

            try
            {
                UpdateCell_24();
                CheckShiftExtended_Step_24();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 24_2: " + e.Message);
            }

            return errorList;
        }

        private void UpdateCell_24()
        {
            #region Variable Declarations
            DXCell uIN2Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN2Cell;
            #endregion

            Keyboard.SendKeys(uIN2Cell, "{Right}{Left}");
        }

        public List<String> Step_25()
        {
            var errorList = new List<string>();

            try
            {
                EditUtjevning();
                EditExtendeddShift_Step_25();
                OkUtjevning();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 25: " + e.Message);
            }

            try
            {
                UpdateCell_24();
                CheckShiftExtended_Step_25();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 24_2: " + e.Message);
            }

            return errorList;
        }
        public List<String> Step_26()
        {
            var errorList = new List<string>();

            try
            {
                DeleteUtjevning();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 26: " + e.Message);
            }

            try
            {
                UpdateCell_24();
                CheckShiftExtended_Step_26();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 26_2: " + e.Message);
            }

            return errorList;
        }
        public List<String> Step_27()
        {
            var errorList = new List<string>();

            try
            {
                SelectShift_Step_27();
                UICommon.UIMapVS2017.RotateRosterplanLeft();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 27: " + e.Message);
            }

            try
            {
                CheckShift_Step_27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 27_2: " + e.Message);
            }

            return errorList;
        }
        public List<String> Step_28()
        {
            var errorList = new List<string>();

            try
            {
                AddShiftCodePattern_Step_28();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 28: " + e.Message);
            }

            try
            {
                CheckShifts_Step_28();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 28_2: " + e.Message);
            }

            return errorList;
        }
        /// <summary>
        /// AddShiftCodePattern_Step_28
        /// </summary>
        public void AddShiftCodePattern_Step_28()
        {
            #region Variable Declarations
            DXCell uIKK1Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIKK1Cell;
            //DXMenuItem uIVaktkodemønsterMenuItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIVaktkodemønsterMenuItem;
            //DXMenuBaseButtonItem uIGlobalvaktkodeGV12MenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIVaktkodemønsterMenuItem.UIGlobalvaktkodeGV12MenuBaseButtonItem;
            DXCell uID1Cell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UID1Cell1;
            //DXMenuBaseButtonItem uIHelgMenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIVaktkodemønsterMenuItem.UIHelgMenuBaseButtonItem;
            DXCell uIItemCell17 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell17;
            //DXMenuBaseButtonItem uIBakvaktMenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIVaktkodemønsterMenuItem.UIBakvaktMenuBaseButtonItem;
            #endregion

            // Right-Click 'KK1' cell
            Mouse.Click(uIKK1Cell, MouseButtons.Right, ModifierKeys.None, new Point(10, 8));

            //// Click 'Vaktkodemønster' menu item
            //Mouse.Click(uIVaktkodemønsterMenuItem, new Point(276, 10));

            //// Click 'Global vaktkode GV1-2' MenuBaseButtonItem
            //Mouse.Click(uIGlobalvaktkodeGV12MenuBaseButtonItem, new Point(95, 12));
            Keyboard.SendKeys("{DOWN 10}{RIGHT}{DOWN}{ENTER}");
            // Right-Click 'D1' cell
            Mouse.Click(uID1Cell1, MouseButtons.Right, ModifierKeys.None, new Point(26, 11));

            //// Click 'Vaktkodemønster' menu item
            //Mouse.Click(uIVaktkodemønsterMenuItem, new Point(275, 9));

            //// Click 'Helg' MenuBaseButtonItem
            //Mouse.Click(uIHelgMenuBaseButtonItem, new Point(57, 11));
            Keyboard.SendKeys("{DOWN 10}{RIGHT}{DOWN 2}{ENTER}");
            // Right-Click cell
            Mouse.Click(uIItemCell17, MouseButtons.Right, ModifierKeys.None, new Point(1, 0));
            Keyboard.SendKeys("{DOWN 10}{RIGHT}{ENTER}");
            //// Click 'Vaktkodemønster' menu item
            //Mouse.Click(uIVaktkodemønsterMenuItem, new Point(273, 9));

            //// Click 'Bakvakt' MenuBaseButtonItem
            //Mouse.Click(uIBakvaktMenuBaseButtonItem, new Point(63, 10));
        }
        public List<String> Step_29()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickRosterplanPlanTab();
                UICommon.ClickOKEditRosterPlanFromPlantab();
                UICommon.ClickEmployeesButtonRosterplan();
                UICommon.ClickEmployeesButtonInEmployeeWindow();
                AddEmployees_Step_29();
                UICommon.ClickOkAddEmployeesWindow();
                UICommon.ClickOkEmployeesWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 29: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_30()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickEditRosterPlanFromPlantab();
                UICommon.ClickRosterplanHomeTab();
                SelectShift_Step_30();
                UICommon.UIMapVS2017.RotateRosterplanRight();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 30: " + e.Message);
            }

            try
            {
                CheckShifts_Step_28();
                CheckShift_Step_30_1();
                CheckShifts_Step_30_2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 30_2: " + e.Message);
            }

            return errorList;
        }
        public void CheckShift_Step_30_1()
        {
            #region Variable Declarations
            DXCell uIItemCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            DXCell uID2Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UID2Cell;
            DXCell uID3Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UID3Cell;
            DXCell uIGV1Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIGV1Cell;
            DXCell uIF1Cell9 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF1Cell9;
            DXCell uIA1Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIA1Cell;
            DXCell uIA3Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIA3Cell;
            DXCell uIItemCell9 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell9;
            DXCell uIItemCell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell1;
            DXCell uIN2Cell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN2Cell1;
            DXCell uIItemCell11 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell11;
            DXCell uIItemCell10 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell10;
            DXCell uIF2Cell3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF2Cell3;
            DXCell uIF1Cell3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF1Cell3;
            DXCell uIItemCell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell2;
            DXCell uIItemCell13 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell13;
            DXCell uIItemCell12 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell12;
            DXCell uIItemCell14 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell14;
            DXCell uIKK4Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIKK4Cell;
            DXCell uIF2Cell6 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF2Cell6;
            DXCell uIF1Cell6 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF1Cell6;
            DXCell uIItemCell3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell3;
            DXCell uIBVCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBVCell;
            DXCell uIN2Cell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN2Cell2;
            DXCell uIN2Cell3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN2Cell3;
            DXCell uIItemCell15 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell15;
            DXCell uIF2Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF2Cell;
            DXCell uIF1Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF1Cell;
            DXCell uIItemCell8 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell8;
            DXCell uILCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UILCell;
            DXCell uILCell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UILCell1;
            DXCell uILCell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UILCell2;
            DXCell uIItemCell7 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell7;
            DXCell uIF2Cell9 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF2Cell9;
            DXCell uILCell3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UILCell3;
            DXCell uID1Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UID1Cell;
            DXCell uID2Cell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UID2Cell1;
            DXCell uID3Cell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UID3Cell1;
            DXCell uIGV1Cell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIGV1Cell1;
            DXCell uIF1Cell10 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF1Cell10;
            DXCell uIF2Cell7 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF2Cell7;
            DXCell uIF1Cell7 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF1Cell7;
            #endregion

            // Verify that the 'Text' property of cell equals 'D1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCellText, uIItemCell.Text);

            // Verify that the 'Text' property of 'D2' cell equals 'D2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UID2CellText, uID2Cell.Text);

            // Verify that the 'Text' property of 'D3' cell equals 'D3'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UID3CellText, uID3Cell.Text);

            // Verify that the 'Text' property of 'GV1' cell equals 'GV1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIGV1CellText, uIGV1Cell.Text);

            // Verify that the 'Text' property of 'F1' cell equals 'F1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF1Cell9Text, uIF1Cell9.Text);

            // Verify that the 'Text' property of 'A1' cell equals 'A1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIA1CellText, uIA1Cell.Text);

            // Verify that the 'Text' property of 'A3' cell equals 'A3'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIA3CellText, uIA3Cell.Text);

            // Verify that the 'Text' property of cell equals ''
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell9Text, uIItemCell9.Text);

            // Verify that the 'Text' property of cell equals 'N2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell1Text, uIItemCell1.Text);

            // Verify that the 'Text' property of 'N2' cell equals 'N2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIN2Cell1Text, uIN2Cell1.Text);

            // Verify that the 'Text' property of cell equals 'N2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell11Text, uIItemCell11.Text);

            // Verify that the 'Text' property of cell equals ''
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell10Text, uIItemCell10.Text);

            // Verify that the 'Text' property of 'F2' cell equals 'F2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF2Cell3Text, uIF2Cell3.Text);

            // Verify that the 'Text' property of 'F1' cell equals 'F1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF1Cell3Text, uIF1Cell3.Text);

            // Verify that the 'Text' property of cell equals 'KK1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell2Text, uIItemCell2.Text);

            // Verify that the 'Text' property of cell equals ''
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell13Text, uIItemCell13.Text);

            // Verify that the 'Text' property of cell equals 'KK2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell12Text, uIItemCell12.Text);

            // Verify that the 'Text' property of cell equals ''
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell14Text, uIItemCell14.Text);

            // Verify that the 'Text' property of 'KK4' cell equals 'KK4'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIKK4CellText, uIKK4Cell.Text);

            // Verify that the 'Text' property of 'F2' cell equals 'F2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF2Cell6Text, uIF2Cell6.Text);

            // Verify that the 'Text' property of 'F1' cell equals 'F1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF1Cell6Text, uIF1Cell6.Text);

            // Verify that the 'ClassName' property of cell equals 'GridControlCell'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell3ClassName, uIItemCell3.ClassName);

            // Verify that the 'ClassName' property of 'BV' cell equals 'GridControlCell'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIBVCellClassName, uIBVCell.ClassName);

            // Verify that the 'Text' property of 'N2' cell equals 'N2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIN2Cell2Text, uIN2Cell2.Text);

            // Verify that the 'Text' property of 'N2' cell equals 'N2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIN2Cell3Text, uIN2Cell3.Text);

            // Verify that the 'Text' property of cell equals ''
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell15Text, uIItemCell15.Text);

            // Verify that the 'Text' property of 'F2' cell equals 'F2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF2CellText, uIF2Cell.Text);

            // Verify that the 'Text' property of 'F1' cell equals 'F1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF1CellText, uIF1Cell.Text);

            // Verify that the 'Text' property of cell equals 'KK1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell8Text, uIItemCell8.Text);

            // Verify that the 'Text' property of 'L' cell equals ''
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UILCellText, uILCell.Text);

            // Verify that the 'Text' property of 'L' cell equals 'KK2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UILCell1Text, uILCell1.Text);

            // Verify that the 'Text' property of 'L' cell equals ''
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UILCell2Text, uILCell2.Text);

            // Verify that the 'Text' property of cell equals 'KK4'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIItemCell7Text, uIItemCell7.Text);

            // Verify that the 'Text' property of 'F2' cell equals 'F2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF2Cell9Text, uIF2Cell9.Text);

            // Verify that the 'Text' property of 'L' cell equals 'F1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UILCell3Text, uILCell3.Text);

            // Verify that the 'Text' property of 'D1' cell equals 'D1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UID1CellText, uID1Cell.Text);

            // Verify that the 'Text' property of 'D2' cell equals 'D2'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UID2Cell1Text, uID2Cell1.Text);

            // Verify that the 'Text' property of 'D3' cell equals 'D3'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UID3Cell1Text, uID3Cell1.Text);

            // Verify that the 'Text' property of 'GV1' cell equals 'GV1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIGV1Cell1Text, uIGV1Cell1.Text);

            // Verify that the 'Text' property of 'F1' cell equals 'F1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF1Cell10Text, uIF1Cell10.Text);

            // Verify that the 'Text' property of 'F2' cell equals 'A1'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF2Cell7Text, uIF2Cell7.Text);

            // Verify that the 'Text' property of 'F1' cell equals 'A3'
            Assert.AreEqual(this.CheckShift_Step_27ExpectedValues.UIF1Cell7Text, uIF1Cell7.Text);
        }

        public List<String> Step_31()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickRosterplanPlanTab();
                UICommon.ClickOKEditRosterPlanFromPlantab();
                UICommon.ClickEmployeesButtonRosterplan();
                UICommon.ClickEmployeesButtonInEmployeeWindow();
                AddEmployee_Step_31();
                UICommon.ClickOkAddEmployeesWindow();
                SelectEmpPosLine_Step_31();

                SetRotation_Step_31();
                UICommon.ClickOkEmployeesWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 31: " + e.Message);
            }

            return errorList;
        }

        private void SetRotation_Step_31()
        {
            UICommon.UIMapVS2017.SetRosterplanRotation(new DateTime(2024, 01, 22));
        }

        public List<String> Step_32()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickRosterplanFilterTab();
                ChangeViewperiod_Step_32();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 32: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_33()
        {
            var errorList = new List<string>();

            try
            {
                Playback.Wait(1500);
                UICommon.ClickRosterplanPlanTab();
                UICommon.ClickEditRosterPlanFromPlantab();
                Playback.Wait(1500);
                InsertT1Shifts();
                UICommon.ClickOKEditRosterPlanFromPlantab();
                CloseRosterPlan();
                SelectRosterPlan("Redigering AP");
                UICommon.ClickRosterplanFilterTab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 33: " + e.Message);
            }

            try
            {
                CheckT1Shifts_Step_33();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 33_2: " + e.Message);
            }

            return errorList;
        }
        private void InsertT1Shifts()
        {
            SelectLine8AndInsertT1Shifts();
        }

        /// <summary>
        /// SelectLine8AndInsertT1Shifts - Use 'SelectLine8AndInsertT1ShiftsParams' to pass parameters into this method.
        /// </summary>
        public void SelectLine8AndInsertT1Shifts()
        {
            #region Variable Declarations
            DXGrid uIGcRosterPlanTable = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable;
            DXButton uIJAButton = this.UIGT4003InformasjonWindow.UIJAButton;
            #endregion

            try
            {
                // Click cell
                ClickCellLine8();

                // Type 'Control + l' in 'gcRosterPlan'  table
                Keyboard.SendKeys(uIGcRosterPlanTable, "l", ModifierKeys.Control);
            }
            catch (Exception)
            {
                SelectLine8();
            }

            // Type 't1' in 'gcRosterPlan' table
            Keyboard.SendKeys("t");
            Keyboard.SendKeys("1{TAB}");

            // Click '&Ja' button
            Mouse.Click(uIJAButton);
        }

        public List<String> Step_34()
        {
            var errorList = new List<string>();

            try
            {
                ChangeViewperiod_Step_34();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 34: " + e.Message);
            }

            try
            {
                CheckT1Shifts_Step_33();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 34_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_35()
        {
            var errorList = new List<string>();

            try
            {
                ChangeViewperiod_Step_32();
                ChangeViewperiod_Step_32();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 35: " + e.Message);
            }

            try
            {
                CheckT1Shifts_Step_33();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 35_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_36()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickRosterplanPlanTab();
                UICommon.SelectNewHelpplan();
                UICommon.SetStartDateNewHelpplan(new DateTime(2024, 01, 15));
                UICommon.SetHelpPlanWeeks("3");
                Playback.Wait(1000);
                UICommon.ClickOkCreateHelpPlan();
                CloseRosterPlan();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 36: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_37()
        {
            var errorList = new List<string>();

            try
            {
                SelectRosterPlan("Hjelpeplan for Redigering AP.");
                UICommon.ClickRosterplanPlanTab();
                UICommon.ClickEditRosterPlanFromPlantab();
                UICommon.ClickRosterplanHomeTab();
                ReplaceBVShifts_Step_37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 37: " + e.Message);
            }

            try
            {
                CheckReplacedShifts_Step_37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 37_2: " + e.Message);
            }

            return errorList;
        }
        private void ReplaceBVShifts_Step_37()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIErstattvaktkoderRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpRosterPlanRibbonPage.UIGrpFunctionsRibbonPageGroup.UIErstattvaktkoderRibbonBaseButtonItem;
            DXComboBox uICbFromCodeComboBox = this.UISøkogErstattWindow.UIGrpFindClient.UICbFromCodeComboBox;
            DXComboBox uICbToCodeComboBox = this.UISøkogErstattWindow.UIGrpReplaceClient.UICbToCodeComboBox;
            DXButton uIOKButton = this.UISøkogErstattWindow.UIPnlButtonsClient.UIOKButton;
            #endregion

            SelectCell_Step_17();

            // Click 'Erstatt vaktkoder' RibbonBaseButtonItem
            Mouse.Click(uIErstattvaktkoderRibbonBaseButtonItem);

            // Select 'BV' in 'cbFromCode' combo box
            //ValueAsString
            uICbFromCodeComboBox.ValueAsString = "BV3";

            // Type '{Tab}' in 'cbFromCode' combo box
            Keyboard.SendKeys(uICbFromCodeComboBox, "{Tab}");

            // Select 'N2' in 'cbToCode' combo box
            //ValueAsString
            uICbToCodeComboBox.ValueAsString = "N1";

            // Type '{Tab}' in 'cbToCode' combo box
            Keyboard.SendKeys(uICbToCodeComboBox, "{Tab}");

            SeachHelpAndMainLines();
            SelectWholePlan();

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }

        public List<String> Step_38()
        {
            var errorList = new List<string>();

            try
            {
                AddShiftCodePattern_Step_38();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 38: " + e.Message);
            }

            try
            {
                CheckShiftCodePattern_Step_38();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 38_2: " + e.Message);
            }

            return errorList;
        }
        public void AddShiftCodePattern_Step_38()
        {
            #region Variable Declarations
            DXCell uID1Cell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UID1Cell1;
            DXCell uIItemCell17 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell17;
            #endregion

            SelectShift_Step_38();
            UICommon.SelectFunctionInRosterplanRightClickMenu("9");
            Keyboard.SendKeys("{TAB}{DOWN}{ENTER}");
        }

        public List<String> Step_39()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickRosterplanPlanTab();
                UICommon.ClickCancelEditRosterPlanFromPlantab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 39: " + e.Message);
            }

            try
            {
                CheckMessage_Step_39();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 39_2: " + e.Message);
            }

            return errorList;
        }
        public List<String> Step_40()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickDiscardChangesToRosterplan();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 40: " + e.Message);
            }

            try
            {
                CheckShifts_Step_40();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 40_2: " + e.Message);
            }

            return errorList;
        }
        private void CheckShifts_Step_40()
        {
            #region Variable Declarations
            DXCell uIItemCell23 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell23;
            DXCell uIGV1Cell7 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIGV1Cell7;
            DXCell uIItemCell24 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell24;
            DXCell uIItemCell29 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell29;
            DXCell uIItemCell25 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell25;
            DXCell uIGV2Cell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIGV2Cell2;
            DXCell uIA3Cell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIA3Cell1;
            DXCell uIGV2Cell3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIGV2Cell3;
            DXCell uIBV3Cell12 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell12;
            DXCell uIN1Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell;
            DXCell uIBV3Cell13 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell13;
            DXCell uIN1Cell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell1;
            DXCell uIBV3Cell14 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell14;
            DXCell uIN1Cell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell2;
            DXCell uIBV3Cell15 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell15;
            DXCell uIN1Cell3 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell3;
            DXCell uIGV1Cell9 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIGV1Cell9;
            DXCell uIT1Cell7 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIT1Cell7;
            DXCell uIItemCell40 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell40;
            DXCell uIT1Cell8 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIT1Cell8;
            DXCell uIGV2Cell4 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIGV2Cell4;
            DXCell uIT1Cell9 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIT1Cell9;
            DXCell uIGV2Cell5 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIGV2Cell5;
            DXCell uIT1Cell10 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIT1Cell10;
            DXCell uIBV3Cell16 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell16;
            DXCell uIN1Cell4 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell4;
            DXCell uIBV3Cell17 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell17;
            DXCell uIN1Cell5 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell5;
            DXCell uIBV3Cell18 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell18;
            DXCell uIN1Cell6 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell6;
            DXCell uIBV3Cell19 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell19;
            DXCell uIN1Cell7 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell7;
            DXCell uIBV3Cell20 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell20;
            DXCell uIN1Cell8 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell8;
            DXCell uIBV3Cell21 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell21;
            DXCell uIN1Cell9 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell9;
            DXCell uIBV3Cell22 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell22;
            DXCell uIN1Cell10 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell10;
            DXCell uIBV3Cell23 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIBV3Cell23;
            DXCell uIN1Cell11 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIN1Cell11;
            #region Variable Declarations
            DXCell uIF1Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF1Cell;
            DXCell uIItemCell8 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell8;
            DXCell uILCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UILCell;
            DXCell uILCell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UILCell1;
            DXCell uILCell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UILCell2;
            #endregion
            #endregion



            // Verify that the 'Text' property of cell equals 'BV3'
            Assert.AreEqual("BV3", uIItemCell23.Text);

            // Verify that the 'Text' property of 'GV1' cell equals 'N1'
            Assert.AreEqual("", uIGV1Cell7.Text);

            // Verify that the 'Text' property of cell equals 'BV3'
            Assert.AreEqual("BV3", uIItemCell24.Text);

            // Verify that the 'Text' property of cell equals 'N1'
            Assert.AreEqual("", uIItemCell29.Text);

            // Verify that the 'Text' property of cell equals 'BV3'
            Assert.AreEqual("BV3", uIItemCell25.Text);

            // Verify that the 'Text' property of 'GV2' cell equals 'N1'
            Assert.AreEqual("", uIGV2Cell2.Text);

            // Verify that the 'Text' property of 'A3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIA3Cell1.Text);

            // Verify that the 'Text' property of 'GV2' cell equals 'N1'
            Assert.AreEqual("", uIGV2Cell3.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell12.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell13.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell1.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell14.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell2.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell15.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell3.Text);

            // Verify that the 'Text' property of 'GV1' cell equals 'BV3'
            Assert.AreEqual("BV3", uIGV1Cell9.Text);

            // Verify that the 'Text' property of 'T1' cell equals 'N1'
            Assert.AreEqual("", uIT1Cell7.Text);

            // Verify that the 'Text' property of cell equals 'BV3'
            Assert.AreEqual("BV3", uIItemCell40.Text);

            // Verify that the 'Text' property of 'T1' cell equals 'N1'
            Assert.AreEqual("", uIT1Cell8.Text);

            // Verify that the 'Text' property of 'GV2' cell equals 'BV3'
            Assert.AreEqual("BV3", uIGV2Cell4.Text);

            // Verify that the 'Text' property of 'T1' cell equals 'N1'
            Assert.AreEqual("", uIT1Cell9.Text);

            // Verify that the 'Text' property of 'GV2' cell equals 'BV3'
            Assert.AreEqual("BV3", uIGV2Cell5.Text);

            // Verify that the 'Text' property of 'T1' cell equals 'N1'
            Assert.AreEqual("", uIT1Cell10.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell16.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell4.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell17.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell5.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell18.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell6.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell19.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell7.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell20.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell8.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell21.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell9.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell22.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell10.Text);

            // Verify that the 'Text' property of 'BV3' cell equals 'BV3'
            Assert.AreEqual("BV3", uIBV3Cell23.Text);

            // Verify that the 'Text' property of 'N1' cell equals 'N1'
            Assert.AreEqual("", uIN1Cell11.Text);



            //Shiftcodepattern
            // Verify that the 'Text' property of 'F1' cell equals 'GV1'
            Assert.AreEqual("", uIF1Cell.Text);

            // Verify that the 'Text' property of cell equals 'GV1'
            Assert.AreEqual("", uIItemCell8.Text);

            // Verify that the 'Text' property of 'L' cell equals ''
            Assert.AreEqual("", uILCell.Text);

            // Verify that the 'Text' property of 'L' cell equals 'GV2'
            Assert.AreEqual("", uILCell1.Text);

            // Verify that the 'Text' property of 'L' cell equals 'GV2'
            Assert.AreEqual("", uILCell2.Text);

        }
        public List<String> Step_41()
        {
            var errorList = new List<string>();

            try
            {
                CloseRosterPlan();
                UICommon.ClickNewRosterplanButton();
                UICommon.UIMapVS2017.SetRosterPlanName("Redigering KP");
                UICommon.UIMapVS2017.SelectRosterplanType("Kalenderplan");
                UICommon.SetStartDateRosterplan(new DateTime(2024, 04, 29));
                UICommon.UIMapVS2017.SetRosterPlanWeeks("4");
                UICommon.ClickOkRosterplanSettings();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 41: " + e.Message);
            }

            return errorList;
        }
        public List<String> Step_42()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.SelectRosterplanPlanTab();
                UICommon.ClickEmployeesButtonRosterplan();
                UICommon.ClickEmployeesButtonInEmployeeWindow();
                SelectFiveEmployees_Step_42();
                UICommon.ClickOkAddEmployeesWindow();
                UICommon.ClickOkEmployeesWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 42: " + e.Message);
            }

            try
            {
                CheckF3Shifts_Step_42();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 42_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_43()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickEditRosterPlanFromPlantab();
                AddShiftCodePattern_Step_43();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 43: " + e.Message);
            }

            try
            {
                CheckShiftCodePatternAdded_Step_43();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 43_2: " + e.Message);
            }

            return errorList;
        }
        private void AddShiftCodePattern_Step_43()
        {
            SelectWeek18shift();
            UICommon.SelectFunctionInRosterplanRightClickMenu("9");

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Keyboard.SendKeys("{TAB}{ENTER}");

            SelectWeek19shift();
            UICommon.SelectFunctionInRosterplanRightClickMenu("9");

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Keyboard.SendKeys("{TAB}{DOWN 2}{ENTER}");

            SelectWeek20shift();
            UICommon.SelectFunctionInRosterplanRightClickMenu("9");

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Keyboard.SendKeys("{TAB}{DOWN}{ENTER}");
        }

        public List<String> Step_44()
        {
            var errorList = new List<string>();

            try
            {
                CopyPasteShiftCodes_Step_44();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 44: " + e.Message);
            }

            try
            {
                CheckShiftCodesAdded_Step_44();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 44_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_45()
        {
            var errorList = new List<string>();

            try
            {
                CopyPasteLine1_Step_45();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 45: " + e.Message);
            }

            try
            {
                CheckShiftCodesAdded_Step_45();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 45_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_46()
        {
            var errorList = new List<string>();

            try
            {
                AddShiftCodes_Step_46();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 46: " + e.Message);
            }

            try
            {
                CheckShiftCodesAdded_Step_46();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 20_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_47()
        {
            var errorList = new List<string>();

            try
            {
                PasteShifts_Step_47();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 47: " + e.Message);
            }

            try
            {
                CheckShiftCodesAdded_Step_47();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 47_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_48()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.ClickRosterplanHomeTab();
                LockShifts_Step_48();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 48: " + e.Message);
            }

            try
            {
                Playback.Wait(1500);
                CheckShiftsLocked_Step_48();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 48_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_49()
        {
            var errorList = new List<string>();

            try
            {
                TryToAddShift_Step_49();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 49: " + e.Message);
            }

            try
            {
                CheckCellsLocked_Step_49();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 49_2: " + e.Message);
            }

            return errorList;
        }
        public void TryToAddShift_Step_49()
        {
            #region Variable Declarations
            DXCell uID1Cell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UID1Cell2;
            #endregion

            // Click 'D1' cell
            Mouse.Click(uID1Cell2, new Point(13, 9));

            // Right-Click 'D1' cell
            Mouse.Click(uID1Cell2, MouseButtons.Right, ModifierKeys.None, new Point(13, 9));

            UICommon.SelectFunctionInRosterplanRightClickMenu("0");
            Keyboard.SendKeys("{TAB}{ENTER}");
        }
        public List<String> Step_50()
        {
            var errorList = new List<string>();

            try
            {
                UnlockWeek_Step_50();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 50: " + e.Message);
            }

            try
            {
                Playback.Wait(1500);
                CheckWeeksUnLocked_Step_50();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 50_2: " + e.Message);
            }

            return errorList;
        }
        public List<String> Step_51()
        {
            var errorList = new List<string>();

            try
            {
                AddAbsence_Step_51();
                UICommon.SelectAbsenceCode("45 -", "11");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 51: " + e.Message);
            }

            try
            {
                CheckAbsenceAdded_Step_51();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 51_2(Absence added): " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_52()
        {
            var uILåsRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpRosterPlanRibbonPage.UIGrpFunctionsRibbonPageGroup.UILåsRibbonBaseButtonItem;
            var errorList = new List<string>();

            try
            {
                Mouse.Click(uILåsRibbonBaseButtonItem);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 52: " + e.Message);
            }

            try
            {
                Playback.Wait(1000);
                CheckAbsenceAdded_Step_52();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 52_2(Absenceweek locked): " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_53()
        {
            var errorList = new List<string>();

            try
            {
                AddAbsence_Step_53();
                UICommon.SelectAbsenceCode("41", "11");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 53: " + e.Message);
            }

            try
            {
                UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Detaljer);
                SelectAbsenceCellStep53();
                CheckAbsenceAdded_Step_53();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 53_2: " + e.Message);
            }

            return errorList;
        }

        public List<String> Step_54()
        {
            var errorList = new List<string>();
            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickOKEditRosterPlanFromPlantab();
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Fravær);

            try
            {
                CheckAbsencesStep54();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 54: " + e.Message);
            }

            CloseRosterPlan();
            CloseGat();

            return errorList;
        }

        private void OpenUtjevning()
        {
            UICommon.UIMapVS2017.OpenUtjevning();
        }
        private void EditUtjevning()
        {
            UICommon.UIMapVS2017.EditUtjevning();
        }
        private void OkUtjevning()
        {
            UICommon.UIMapVS2017.OkUtjevning();
        }
        private void DeleteUtjevning()
        {
            UICommon.UIMapVS2017.DeleteUtjevning();
        }

        public void StartGat(bool logGatInfo, bool findBySearch)
        {
            var dep = UICommon.DepArbeidsplanOghjelpeplan;
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(dep, null, "", logGatInfo, false, findBySearch);
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

        private void SelectRosterPlan(string plan, bool selectRosterplanTab = true)
        {
            var errorList = new List<string>();

            try
            {
                if (selectRosterplanTab)
                    UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);

                UICommon.SelectRosterPlan(plan, true, false);
            }
            catch (Exception e)
            {
                errorList.Add("Error selecting rosterplan: " + e.Message);
            }
        }
        private void CloseRosterPlan()
        {
            try
            {
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

            Playback.Wait(1000);
        }
    }
}
