namespace _023_Test_Omregnet_Tid
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
    using System.Diagnostics;
    using CommonTestData;
    using System.Threading;
    using System.Globalization;

    public partial class UIMap
    {
        private CommonUIFunctions.UIMap UICommon;
        private UIMapVS2017Classes.UIMapVS2017 UIMapVS2017
        {
            get
            {
                if ((this.map1 == null))
                {
                    this.map1 = new UIMapVS2017Classes.UIMapVS2017();
                }

                return this.map1;
            }
        }
        private UIMapVS2017_2Classes.UIMapVS2017_2 UIMapVS2017_2
        {
            get
            {
                if ((this.map2 == null))
                {
                    this.map2 = new UIMapVS2017_2Classes.UIMapVS2017_2();
                }

                return this.map2;
            }
        }

        private UIMapVS2017Classes.UIMapVS2017 map1;
        private UIMapVS2017_2Classes.UIMapVS2017_2 map2;

        private  TestContext TestContext;
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
            UICommon.LoginGatAndSelectDepartment(UICommon.DepOmregnetTid, null, "", logGatInfo);
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

        public void WaitForPlanReady(int interval = 5000)
        {
            Playback.Wait(interval);
            UIArbeidsplanØnskeplanWindow.WaitForControlReady();
        }
       
        public void OpenPlan()
        {
            DXButton uIJAButton = this.UIRPL24006InformasjonWindow.UIJAButton;

            Playback.Wait(1500);
            SelectRosterplanTab();
            UICommon.SelectRosterPlan("Omregnet tid Kalenderplan");

            try
            {
                Mouse.Click(uIJAButton);
            }
            catch (Exception)
            {
                TestContext.WriteLine("Information window not found");
            }            
        }

        /// <summary>
        /// SelectEmplyeeTabBanks
        /// </summary>
        public void SelectEmployeeTabBanks()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
            UICommon.SelectSubTabInEmployeeTab(CommonUIFunctions.UIMap.EmployeeTabs.EmployeeBanksTab, false);
        }


        /// <summary>
        /// OpenChopinSaldotidDetails
        /// </summary>
        public void OpenChopinSaldotidDetails()
        {
            #region Variable Declarations
            var bankBalance = this.UIGatver64339716ASCLAvWindow.UIBankBalanceListContrCustom;
            bankBalance.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            bankBalance.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            bankBalance.SearchProperties[WinWindow.PropertyNames.ControlName] = "BankBalanceListControl";
            DXButton uIAvspaseringButton = bankBalance.UIGcBankBalancesTable.UIAvspaseringButton;
            uIAvspaseringButton.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uIAvspaseringButton.SearchProperties[DXTestControl.PropertyNames.Name] = "gcBankBalancesGridControlCellButton[View]gvBankBalances[Row]0[Column]colBankName1";
            #endregion

            UIMapVS2017_2.SelectChopinInSubTabBanks();

            Playback.Wait(500);
            UICommon.SelectEmpBanksWithTransactions();
            Playback.Wait(500);
            
            // Click 'Avspasering' button
            try
            {
                Mouse.Click(uIAvspaseringButton, new Point(2, 5));
            }
            catch (Exception)
            {
                CheckExpandAllBankDetails();
            }
        }

        /// <summary>
        /// SetWhishplanToPhase1
        /// </summary>
        public void SetWhishplanToPhase1()
        {
            #region Variable Declarations
            DXCell uIØnskeplanforOmregnetCell = this.UIGatver64339511ASCLAvWindow.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIØnskeplanforOmregnetCell;
            #endregion

            SelectWishplan(true);

            // Click 'Ønskeplan for Omregnet tid Baseplan' cell
            Mouse.Click(uIØnskeplanforOmregnetCell, new Point(137, 10));

            SelectSetWhishplanToPhase1();
        }

        private void ShowOldPlans()
        {
            Playback.Wait(1000);
            UICommon.ShowAllPlans();
            Playback.Wait(1000);
        }

        private void ShowOldWishPlans()
        {
            Playback.Wait(1000);
            UICommon.ShowOldWishPlans();
            Playback.Wait(1000);
        }
                
        public void SelectWishplan(bool showOldPlans)
        {
            #region Variable Declarations
            DXTestControl uIXtraTabControlHeaderTabPage = this.UIGatver64339511ASCLAvWindow.UIPcMainPanelClient.UITcPanListsTabList.UIXtraTabControlHeaderTabPage;
            DXButton uIOmregnettidButton = this.UIGatver64339511ASCLAvWindow.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIOmregnettidButton;
            
            #endregion
            if (showOldPlans)
                ShowOldWishPlans();
            
            // Click 'XtraTabControlHeader' tab
            Mouse.Click(uIXtraTabControlHeaderTabPage, new Point(38, 9));

            // Click 'Omregnet tid' button
            Mouse.Click(uIOmregnettidButton, new Point(2, 2));
        }
        
        /// <summary>
        /// SelectRosterplanTab
        /// </summary>
        public void SelectRosterplanTab(int delay = 0)
        {
            Playback.Wait(delay * 1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }

        public void CloseRosterPlan()
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


        /// <summary>
        /// UnlockPlanIfLocked_step_1
        /// </summary>
        public void UnlockPlanAndRestartGat_IfLockedstep_1()
        {
            if (UICommon.UnlockRosterPlanIfLocked())
            {
                try
                {
                    UICommon.CloseRosterplanFromSupportToolsTab();
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

                Playback.Wait(2000);                
                OpenPlan();
            }

            UICommon.ClickRosterplanFilterTab();
            UICommon.UIMapVS2017.SelectViewFilter("arbeidsplan");
            ShowWeeksumTotalOnly();
        }

        /// <summary>
        /// InsertHJ1BachWednesday - Use 'InsertHJ1BachWednesdayParams' to pass parameters into this method.
        /// </summary>
        public void InsertHJ1BachWednesday()
        {
            #region Variable Declarations
            DXCell uIDCell = this.UIArbeidsplanOmregnettWindow3.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIDCell;
            DXGrid uIGcRosterPlanTable = this.UIArbeidsplanOmregnettWindow3.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            DXTextEdit uIRow1ColumnRosterCellEdit = this.UIArbeidsplanOmregnettWindow3.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit;
            #endregion

            // Click 'D' cell
            Mouse.Click(uIDCell);

            // Type 'hj1' in 'gcRosterPlan' table
            Keyboard.SendKeys(uIGcRosterPlanTable, this.InsertHJ1BachWednesdayParams.UIRow1ColumnRosterCellEditValueAsString, ModifierKeys.None);

            // Type '{Tab}' in '[Row]1[Column]RosterCell_2' text box
            Keyboard.SendKeys(uIGcRosterPlanTable, this.InsertHJ1BachWednesdayParams.UIRow1ColumnRosterCellEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// AddUtjevningsVakt
        /// </summary>
        public void AddUtjevningsVakt()
        {
            #region Variable Declarations
            DXCell uIACell = this.UIArbeidsplanOmregnettWindow3.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIACell;
            //DXMenuBaseButtonItem uIBarButtonItemLink2MenuBaseButtonItem = this.UIItemWindow2.UIPopupMenuBarControlMenu.UIBarSubItemLink10MenuItem.UIBarButtonItemLink2MenuBaseButtonItem;
            //DXMenuItem uIAutomatiskinnsettingMenuItem = this.UIItemWindow2.UIPopupMenuBarControlMenu1.UIAutomatiskinnsettingMenuItem;
            #endregion

            // Right-Click 'A' cell
            Mouse.Click(uIACell, MouseButtons.Right);
            Playback.Wait(1500);
            //Keyboard.SendKeys(UIItemWindow2, "{DOWN 10}{ENTER}", ModifierKeys.None);
            Keyboard.SendKeys("{DOWN 10}{ENTER}", ModifierKeys.None);

            Playback.Wait(1000);
            Keyboard.SendKeys("{DOWN 2}{ENTER}", ModifierKeys.None);
            Playback.Wait(1000);
        }



        /// <summary>
        /// ConstructUtjevningsvakt - Use 'ConstructUtjevningsvaktParams' to pass parameters into this method.
        /// </summary>
        public void ConstructUtjevningsvakt()
        {
            #region Variable Declarations
            DXRadioGroup uIRgrpEqualizationModeRadioGroup = this.UIUtjevningsvaktWindow.UIGrpEqualizingClient.UIRgrpEqualizationModeRadioGroup;
            DXRadioGroup uIRgrpEqualizationPlacRadioGroup = this.UIUtjevningsvaktWindow.UIGrpEqualizingClient.UIRgrpEqualizationPlacRadioGroup;
            DXTextEdit uIENumberEdit = this.UIUtjevningsvaktWindow.UIGrpEqualizingClient.UIENumberEdit;
            DXButton uIGSSimpleButtonButton = this.UIUtjevningsvaktWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Type '1' in 'rgrpEqualizationMode' RadioGroup
            //SelectedIndex
            uIRgrpEqualizationModeRadioGroup.SelectedIndex = this.ConstructUtjevningsvaktParams.UIRgrpEqualizationModeRadioGroupSelectedIndex;

            // Type '1' in 'rgrpEqualizationPlacement' RadioGroup
            //SelectedIndex
            uIRgrpEqualizationPlacRadioGroup.SelectedIndex = this.ConstructUtjevningsvaktParams.UIRgrpEqualizationPlacRadioGroupSelectedIndex;

            // Type '120 [SelectionStart]0[SelectionLength]3' in 'eNumber' text box
            //ValueAsString
            uIENumberEdit.ValueAsString = this.ConstructUtjevningsvaktParams.UIENumberEditValueAsString;
            Keyboard.SendKeys(uIENumberEdit, "{TAB}");

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton, new Point(1, 1));
        }

        /// <summary>
        /// CheckBachLine2_step_6_Calculations - Use 'CheckBachLine2_step_6_CalculationsExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckBachLine2Week1_step_7_Calculations()
        {
            UIMapVS2017.CheckBachLine2Week1_step_7_Calculations();
        }

        /// <summary>
        /// SetFilterToShowGrunnlinje
        /// </summary>
        public void SetFilterToShowGrunnlinje()
        {
            #region Variable Declarations
            DXRibbonEditItem uIDdlFilterRibbonEditItem = this.UIArbeidsplanØnskeplanWindow.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIDdlFilterRibbonEditItem;
            //var treeList = this.UIArbeidsplanØnskeplanWindow.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList;
            //var treeList = this.UIArbeidsplanØnskeplanWindow42.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList;
            DXTestControl uINode0TreeListNodeCheckBox = this.UIArbeidsplanØnskeplanWindow.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode2TreeListNode.UINode0TreeListNode.UINode0TreeListNodeCheckBox;
            DXButton uIAvbrytButton = this.UIArbeidsplanØnskeplanWindow.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIAvbrytButton;
            DXRibbonPage uIRpRosterPlanRibbonPage = this.UIArbeidsplanØnskeplanWindow.UIRcMenuRibbon.UIRpRosterPlanRibbonPage;
            #endregion

            UICommon.ClickRosterplanFilterTab();

            // Click 'ddlFilter' RibbonEditItem
            Mouse.Click(uIDdlFilterRibbonEditItem, new Point(165, 9));
            Mouse.Click(uINode0TreeListNodeCheckBox);
            
            // Click 'Avbryt' button
            Mouse.Click(uIAvbrytButton);

            // Click 'rpRosterPlan' RibbonPage
            Mouse.Click(uIRpRosterPlanRibbonPage, new Point(22, 12));
        }

        /// <summary>
        /// InsertN_F2_AshiftMozartPhase2 - Use 'InsertN_F2_AshiftMozartPhase2Params' to pass parameters into this method.
        /// </summary>
        public void InsertN_F2_AshiftMozartPhase2()
        {
            #region Variable Declarations
            DXCell uIACell = this.UIArbeidsplanØnskeplanWindow14.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIACell;
            DXGrid uIGcRosterPlanTable = this.UIArbeidsplanØnskeplanWindow15.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            DXTextEdit uIRow1ColumnRosterCellEdit = this.UIArbeidsplanØnskeplanWindow16.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit;
            DXCell uIACell1 = this.UIArbeidsplanØnskeplanWindow17.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIACell1;
            DXGrid uIGcRosterPlanTable1 = this.UIArbeidsplanØnskeplanWindow18.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            DXTextEdit uIRow1ColumnRosterCellEdit1 = this.UIArbeidsplanØnskeplanWindow19.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit1;
            DXCell uIACell2 = this.UIArbeidsplanØnskeplanWindow20.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIACell2;
            DXGrid uIGcRosterPlanTable2 = this.UIArbeidsplanØnskeplanWindow21.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            DXTextEdit uIRow1ColumnRosterCellEdit2 = this.UIArbeidsplanØnskeplanWindow22.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit2;
            DXCell uIDCell = this.UIArbeidsplanØnskeplanWindow23.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIDCell;
            DXGrid uIGcRosterPlanTable3 = this.UIArbeidsplanØnskeplanWindow24.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            DXTextEdit uIRow1ColumnRosterCellEdit3 = this.UIArbeidsplanØnskeplanWindow25.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit3;
            #endregion

            // Click 'A' cell
            Mouse.Click(uIACell, new Point(20, 9));

            // Type 'n' in 'gcRosterPlan' table
            Keyboard.SendKeys(uIGcRosterPlanTable, this.InsertN_F2_AshiftMozartPhase2Params.UIGcRosterPlanTableSendKeys, ModifierKeys.None);

            // Type '{Tab}' in '[Row]1[Column]RosterCell_1' text box
            Keyboard.SendKeys(uIRow1ColumnRosterCellEdit, this.InsertN_F2_AshiftMozartPhase2Params.UIRow1ColumnRosterCellEditSendKeys, ModifierKeys.None);

            // Click 'A' cell
            Mouse.Click(uIACell1, new Point(21, 8));

            // Type 'n' in 'gcRosterPlan' table
            Keyboard.SendKeys(uIGcRosterPlanTable1, this.InsertN_F2_AshiftMozartPhase2Params.UIGcRosterPlanTableSendKeys1, ModifierKeys.None);

            // Type '{Tab}' in '[Row]1[Column]RosterCell_4' text box
            Keyboard.SendKeys(uIRow1ColumnRosterCellEdit1, this.InsertN_F2_AshiftMozartPhase2Params.UIRow1ColumnRosterCellEdit1SendKeys, ModifierKeys.None);

            // Click 'A' cell
            Mouse.Click(uIACell2, new Point(25, 10));

            // Type 'f2' in 'gcRosterPlan' table
            Keyboard.SendKeys(uIGcRosterPlanTable2, this.InsertN_F2_AshiftMozartPhase2Params.UIRow1ColumnRosterCellEdit2ValueAsString, ModifierKeys.None);

            // Type '{Tab}' in '[Row]1[Column]RosterCell_10' text box
            Keyboard.SendKeys(uIRow1ColumnRosterCellEdit2, this.InsertN_F2_AshiftMozartPhase2Params.UIRow1ColumnRosterCellEdit2SendKeys, ModifierKeys.None);

            // Click 'D' cell
            Mouse.Click(uIDCell, new Point(21, 12));

            // Type 'a' in 'gcRosterPlan' table
            Keyboard.SendKeys(uIGcRosterPlanTable3, this.InsertN_F2_AshiftMozartPhase2Params.UIGcRosterPlanTableSendKeys3, ModifierKeys.None);

            // Type '{Tab}' in '[Row]1[Column]RosterCell_20' text box
            Keyboard.SendKeys(uIRow1ColumnRosterCellEdit3, this.InsertN_F2_AshiftMozartPhase2Params.UIRow1ColumnRosterCellEdit3SendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// InsertF3shiftMozartPhase3 - Use 'InsertF3shiftMozartPhase3Params' to pass parameters into this method.
        /// </summary>
        public void InsertF3shiftMozartPhase3()
        {
            #region Variable Declarations
            DXCell uIItemCell = this.UIArbeidsplanØnskeplanWindow32.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            DXTextEdit uIRow2ColumnRosterCellEdit = this.UIArbeidsplanØnskeplanWindow33.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow2ColumnRosterCellEdit;
            uIItemCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanGridControlCell[View]gvRosterPlan[Row]1[Column]RosterCell_4";
            uIRow2ColumnRosterCellEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanTextEdit[View]gvRosterPlan[Row]1[Column]RosterCell_4";
            #endregion

            // Click cell
            Playback.Wait(1000);
            Mouse.Click(uIItemCell, new Point(17, 10));
            
            // Type 'f3' in '[Row]2[Column]RosterCell_4' text box
            //ValueAsString
            Keyboard.SendKeys(uIRow2ColumnRosterCellEdit, this.InsertF3shiftMozartPhase3Params.UIRow2ColumnRosterCellEditValueAsString, ModifierKeys.None);

            // Type '{Tab}' in '[Row]2[Column]RosterCell_4' text box
            Keyboard.SendKeys(uIRow2ColumnRosterCellEdit, this.InsertF3shiftMozartPhase3Params.UIRow2ColumnRosterCellEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// InsertF4shiftMozartPhase3_step_22 - Use 'InsertF4shiftMozartPhase3_step_22Params' to pass parameters into this method.
        /// </summary>
        public void InsertF4shiftMozartPhase3_step_22()
        {
            #region Variable Declarations
            DXCell uIItemCell = this.UIArbeidsplanØnskeplanWindow37.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            DXTextEdit uIRow2ColumnRosterCellEdit = this.UIArbeidsplanØnskeplanWindow39.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow2ColumnRosterCellEdit;
            uIItemCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanGridControlCell[View]gvRosterPlan[Row]1[Column]RosterCell_1";
            uIRow2ColumnRosterCellEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanTextEdit[View]gvRosterPlan[Row]1[Column]RosterCell_1";
            #endregion

            // Click cell
            Mouse.Click(uIItemCell, new Point(18, 7));

            // Type 'f4' in 'gcRosterPlan' table
            Keyboard.SendKeys(uIRow2ColumnRosterCellEdit, this.InsertF4shiftMozartPhase3_step_22Params.UIRow2ColumnRosterCellEditValueAsString, ModifierKeys.None);

            // Type '{Tab}' in '[Row]2[Column]RosterCell_1' text box
            Keyboard.SendKeys(uIRow2ColumnRosterCellEdit, this.InsertF4shiftMozartPhase3_step_22Params.UIRow2ColumnRosterCellEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// RegisterAbsenceChopinWeek2 - Use 'RegisterAbsenceChopinWeek2Params' to pass parameters into this method.
        /// </summary>
        public List<string> RegisterAbsenceChopinWeek(int week, string bankValue)
        {
            #region Variable Declarations
            string step = "11";
            var errorList = new List<string>();
            DXRibbonButtonItem uIRedigerRibbonBaseButtonItem = this.UIArbeidsplanOmregnettWindow1.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRibbonPageGroup10RibbonPageGroup.UIRedigerRibbonBaseButtonItem;
            DXLookUpEdit uIDdlAbsCodeLookUpEdit = this.UIFraværiarbeidsplanWindow.UINbcAbsenceNavBar.UINavBarGroupControlCoScrollableControl.UIDdlAbsCodeLookUpEdit;
            //DXWindow uIPopupLookUpEditFormWindow = this.UIFraværiarbeidsplanWindow.UINbcAbsenceNavBar.UINavBarGroupControlCoScrollableControl.UIDdlAbsCodeLookUpEdit.UIPopupLookUpEditFormWindow;
            DXButton uIGSSimpleButtonButton = this.UIFraværiarbeidsplanWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            if (week == 2)
            {
                DXCell uIItemCell = this.UIArbeidsplanOmregnettWindow3.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
                DXCell uIItemCell1 = this.UIArbeidsplanOmregnettWindow3.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell1;

                // Click 'Rediger' RibbonBaseButtonItem
                Mouse.Click(uIRedigerRibbonBaseButtonItem, new Point(24, 27));

                // Move cell to cell
                uIItemCell1.EnsureClickable(new Point(9, 4));
                Mouse.StartDragging(uIItemCell, new Point(19, 10));
                Mouse.StopDragging(uIItemCell1, new Point(9, 4));

                // Right-Click cell
                Mouse.Click(uIItemCell1, MouseButtons.Right);
            }
            else
            {
                step = "12";
                DXCell uIItemCell2 = this.UIArbeidsplanOmregnettWindow3.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell2;
                DXCell uIItemCell11 = this.UIArbeidsplanOmregnettWindow3.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell11;

                // Move cell to cell
                uIItemCell11.EnsureClickable(new Point(14, 11));
                Mouse.StartDragging(uIItemCell2, new Point(53, 10));
                Mouse.StopDragging(uIItemCell11, new Point(14, 11));

                // Right-Click cell
                Mouse.Click(uIItemCell11, MouseButtons.Right);
            }

            Playback.Wait(1500);
            Keyboard.SendKeys("{DOWN 10}{ENTER}", ModifierKeys.None);

            Playback.Wait(1000);
            Keyboard.SendKeys("{DOWN}{ENTER}", ModifierKeys.None);
            Playback.Wait(1000);

            UIFraværiarbeidsplanWindow.WaitForControlReady();
            Mouse.Click(uIDdlAbsCodeLookUpEdit);
            Keyboard.SendKeys(uIDdlAbsCodeLookUpEdit, "{DOWN 2}{ENTER}", ModifierKeys.None);

            ShowOtherInAbsenceWindow();
            try
            {
                Playback.Wait(1000);
                CheckBankValueAbsenceWindow(bankValue);
                TestContext.WriteLine("Korrekte verdier i step " + step + "(trekk til bank fraværsvindu)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "step " + step);
            }

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);

            return errorList;
        }

        public void SetWishplanFilter()
        {
            UICommon.ClickRosterplanFilterTab();
            UICommon.UIMapVS2017.SelectViewFilter("test 23");
            ShowWeeksumTotalOnly();
            UICommon.ClickRosterplanHomeTab();
        }

        /// <summary>
        /// CheckBankValueAbsenceWindow - Use 'CheckBankValueAbsenceWindowExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckBankValueAbsenceWindow(string bankValue)
        {
            #region Variable Declarations
            DXTextEdit uIENumberEdit = this.UIFraværiarbeidsplanWindow.UINbcAbsenceNavBar.UINavBarGroupControlCoScrollableControl1.UIGrpBankClient.UIENumberEdit;
            #endregion

            // Verify that the 'ValueAsString' property of 'eNumber' text box equals bankValue
            Assert.AreEqual(bankValue, uIENumberEdit.ValueAsString);
        }



        /// <summary>
        /// CheckMozartBankValues_step_25 - Use 'CheckMozartBankValues_step_25ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckMozartBankValues_step_25()
        {
            try
            {
                UIMapVS2017.CheckMozartBankValues_step_25();
            }
            catch
            {
                UIMapVS2017.CheckMozartBankValues_step_25_2();
            }
        }


        /// <summary>
        /// CheckSchubertBankValues_step_25 - Use 'CheckSchubertBankValues_step_25ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckSchubertBankValues_step_25()
        {
            try
            {
                UIMapVS2017.CheckSchubertBankValues_step_25();
            }
            catch
            {
                UIMapVS2017.CheckSchubertBankValues_step_25_2();
            }           
        }
        public List<string> Step_26()
        {
            var errorList = new List<string>();

            try
            {
                OpenPlan();
                UICommon.ClickRosterplanFilterTab();
                UICommon.UIMapVS2017.SelectViewFilter("Arbeidsplan");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + ": Step26");
            }

            try
            {
              UIMapVS2017_2.CheckBrahmsCalculations();
                TestContext.WriteLine("Korrekte verdier i step 26(Beregninger/Brahms)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 26(Beregninger)");
            }

            return errorList;
        }
        public List<string> Step_27()
        {
            var errorList = new List<string>();

            try
            {
                UIMapVS2017_2.CheckSibeliusCalculations();
                TestContext.WriteLine("Korrekte verdier i step 27(Beregninger/Sibelius)");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + " step 27(Beregninger)");
            }

            return errorList;
        }

        

        public void AssertResults(List<string> errorList)
        {
            UICommon.AssertResults(errorList);
        }


        public virtual InsertHJ1BachWednesdayParams InsertHJ1BachWednesdayParams
        {
            get
            {
                if ((this.mInsertHJ1BachWednesdayParams == null))
                {
                    this.mInsertHJ1BachWednesdayParams = new InsertHJ1BachWednesdayParams();
                }
                return this.mInsertHJ1BachWednesdayParams;
            }
        }

        private InsertHJ1BachWednesdayParams mInsertHJ1BachWednesdayParams;


        public virtual SetFilterToShowGrunnlinjeParams SetFilterToShowGrunnlinjeParams
        {
            get
            {
                if ((this.mSetFilterToShowGrunnlinjeParams == null))
                {
                    this.mSetFilterToShowGrunnlinjeParams = new SetFilterToShowGrunnlinjeParams();
                }
                return this.mSetFilterToShowGrunnlinjeParams;
            }
        }

        private SetFilterToShowGrunnlinjeParams mSetFilterToShowGrunnlinjeParams;


        public virtual InsertN_F2_AshiftMozartPhase2Params InsertN_F2_AshiftMozartPhase2Params
        {
            get
            {
                if ((this.mInsertN_F2_AshiftMozartPhase2Params == null))
                {
                    this.mInsertN_F2_AshiftMozartPhase2Params = new InsertN_F2_AshiftMozartPhase2Params();
                }
                return this.mInsertN_F2_AshiftMozartPhase2Params;
            }
        }

        private InsertN_F2_AshiftMozartPhase2Params mInsertN_F2_AshiftMozartPhase2Params;

        public virtual InsertF3shiftMozartPhase3Params InsertF3shiftMozartPhase3Params
        {
            get
            {
                if ((this.mInsertF3shiftMozartPhase3Params == null))
                {
                    this.mInsertF3shiftMozartPhase3Params = new InsertF3shiftMozartPhase3Params();
                }
                return this.mInsertF3shiftMozartPhase3Params;
            }
        }

        private InsertF3shiftMozartPhase3Params mInsertF3shiftMozartPhase3Params;

        public virtual InsertF4shiftMozartPhase3_step_22Params InsertF4shiftMozartPhase3_step_22Params
        {
            get
            {
                if ((this.mInsertF4shiftMozartPhase3_step_22Params == null))
                {
                    this.mInsertF4shiftMozartPhase3_step_22Params = new InsertF4shiftMozartPhase3_step_22Params();
                }
                return this.mInsertF4shiftMozartPhase3_step_22Params;
            }
        }

        private InsertF4shiftMozartPhase3_step_22Params mInsertF4shiftMozartPhase3_step_22Params;


        public virtual CheckMozartBankValues_step_25ExpectedValues CheckMozartBankValues_step_25ExpectedValues
        {
            get
            {
                if ((this.mCheckMozartBankValues_step_25ExpectedValues == null))
                {
                    this.mCheckMozartBankValues_step_25ExpectedValues = new CheckMozartBankValues_step_25ExpectedValues();
                }
                return this.mCheckMozartBankValues_step_25ExpectedValues;
            }
        }

        private CheckMozartBankValues_step_25ExpectedValues mCheckMozartBankValues_step_25ExpectedValues;


        public virtual CheckSchubertBankValues_step_25ExpectedValues CheckSchubertBankValues_step_25ExpectedValues
        {
            get
            {
                if ((this.mCheckSchubertBankValues_step_25ExpectedValues == null))
                {
                    this.mCheckSchubertBankValues_step_25ExpectedValues = new CheckSchubertBankValues_step_25ExpectedValues();
                }
                return this.mCheckSchubertBankValues_step_25ExpectedValues;
            }
        }

        private CheckSchubertBankValues_step_25ExpectedValues mCheckSchubertBankValues_step_25ExpectedValues;

        public virtual ConstructUtjevningsvaktParams ConstructUtjevningsvaktParams
        {
            get
            {
                if ((this.mConstructUtjevningsvaktParams == null))
                {
                    this.mConstructUtjevningsvaktParams = new ConstructUtjevningsvaktParams();
                }
                return this.mConstructUtjevningsvaktParams;
            }
        }

        private ConstructUtjevningsvaktParams mConstructUtjevningsvaktParams;
    }
    /// <summary>
    /// Parameters to be passed into 'InsertHJ1BachWednesday'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class InsertHJ1BachWednesdayParams
    {

        #region Fields

        /// <summary>
        /// Type 'hj1' in '[Row]1[Column]RosterCell_2' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEditValueAsString = "hj1";

        /// <summary>
        /// Type '{Tab}' in '[Row]1[Column]RosterCell_2' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetFilterToShowGrunnlinje'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetFilterToShowGrunnlinjeParams
    {

        #region Fields
        /// <summary>
        /// Type 'True' in 'Node0' TreeListNode
        /// </summary>
        public bool UINode0TreeListNodeChecked = true;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'InsertN_F2_AshiftMozartPhase2'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class InsertN_F2_AshiftMozartPhase2Params
    {

        #region Fields
        /// <summary>
        /// Type 'n' in 'gcRosterPlan' table
        /// </summary>
        public string UIGcRosterPlanTableSendKeys = "n";

        /// <summary>
        /// Type '{Tab}' in '[Row]1[Column]RosterCell_1' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEditSendKeys = "{Tab}";

        /// <summary>
        /// Type 'n' in 'gcRosterPlan' table
        /// </summary>
        public string UIGcRosterPlanTableSendKeys1 = "n";

        /// <summary>
        /// Type '{Tab}' in '[Row]1[Column]RosterCell_4' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEdit1SendKeys = "{Tab}";

        /// <summary>
        /// Type 'f' in 'gcRosterPlan' table
        /// </summary>
        public string UIGcRosterPlanTableSendKeys2 = "f";

        /// <summary>
        /// Type 'f2' in '[Row]1[Column]RosterCell_10' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEdit2ValueAsString = "f2";

        /// <summary>
        /// Type '{Tab}' in '[Row]1[Column]RosterCell_10' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEdit2SendKeys = "{Tab}";

        /// <summary>
        /// Type 'a' in 'gcRosterPlan' table
        /// </summary>
        public string UIGcRosterPlanTableSendKeys3 = "a";

        /// <summary>
        /// Type '{Tab}' in '[Row]1[Column]RosterCell_20' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEdit3SendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'InsertF3shiftMozartPhase3'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class InsertF3shiftMozartPhase3Params
    {

        #region Fields
        /// <summary>
        /// Type 'f3' in '[Row]2[Column]RosterCell_4' text box
        /// </summary>
        public string UIRow2ColumnRosterCellEditValueAsString = "f3";

        /// <summary>
        /// Type '{Tab}' in '[Row]2[Column]RosterCell_4' text box
        /// </summary>
        public string UIRow2ColumnRosterCellEditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'InsertF4shiftMozartPhase3_step_22'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class InsertF4shiftMozartPhase3_step_22Params
    {

        #region Fields

        /// <summary>
        /// Type 'f4' in '[Row]2[Column]RosterCell_1' text box
        /// </summary>
        public string UIRow2ColumnRosterCellEditValueAsString = "f4";

        /// <summary>
        /// Type '{Tab}' in '[Row]2[Column]RosterCell_1' text box
        /// </summary>
        public string UIRow2ColumnRosterCellEditSendKeys = "{Tab}";
        #endregion
    }

    /// <summary>
    /// Parameters to be passed into 'CheckMozartBankValues_step_25'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckMozartBankValues_step_25ExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Iverksetting av arbeidsplan linje nr #1, plan 'Ønskeplan for Omregnet tid Baseplan'.' cell equals 'Iverksetting av arbeidsplan linje nr #1, plan 'Ønskeplan for Omregnet tid Baseplan'.'
        /// </summary>
        public string saldoTidarbeidValueAsString = "Iverksetting av arbeidsplan linje nr #1, plan \'Ønskeplan for Omregnet tid Basepla" +
            "n\'.";

        /// <summary>
        /// Verify that the 'Text' property of '-4,50' cell equals '-4,50'
        /// </summary>
        public string UIItem450CellText = "-4,50";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Iverksetting av arbeidsplan linje nr #1, plan 'Ønskeplan for Omregnet tid Baseplan'. Overført omregnet tid.' cell equals 'Iverksetting av arbeidsplan linje nr #1, plan 'Ønskeplan for Omregnet tid Baseplan'. Overført omregnet tid.'
        /// </summary>
        public string omregnetTidValueAsString = "Iverksetting av arbeidsplan linje nr #1, plan \'Ønskeplan for Omregnet tid Basepla" +
            "n\'. Overført omregnet tid.";

        /// <summary>
        /// Verify that the 'Text' property of '5,46' cell equals '5,46'
        /// </summary>
        public string UIItem546CellText = "5,46";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckSchubertBankValues_step_25'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckSchubertBankValues_step_25ExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Iverksetting av arbeidsplan linje nr #1, plan 'Ønskeplan for Omregnet tid Baseplan'.' cell equals 'Iverksetting av arbeidsplan linje nr #2, plan 'Ønskeplan for Omregnet tid Baseplan'.'
        /// </summary>
        public string saldoTidValueAsString = "Iverksetting av arbeidsplan linje nr #2, plan \'Ønskeplan for Omregnet tid Basepla" +
            "n\'.";

        /// <summary>
        /// Verify that the 'Text' property of '-4,50' cell equals '1,75'
        /// </summary>
        public string UIItem450CellText = "1,75";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Iverksetting av arbeidsplan linje nr #1, plan 'Ønskeplan for Omregnet tid Baseplan'. Overført omregnet tid.' cell equals 'Iverksetting av arbeidsplan linje nr #2, plan 'Ønskeplan for Omregnet tid Baseplan'. Overført omregnet tid.'
        /// </summary>
        public string omregnetTidValueAsString = "Iverksetting av arbeidsplan linje nr #2, plan \'Ønskeplan for Omregnet tid Basepla" +
            "n\'. Overført omregnet tid.";

        /// <summary>
        /// Verify that the 'Text' property of '5,46' cell equals '1,99'
        /// </summary>
        public string UIItem546CellText = "1,98";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ConstructUtjevningsvakt'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ConstructUtjevningsvaktParams
    {

        #region Fields
        /// <summary>
        /// Type '1' in 'rgrpEqualizationMode' RadioGroup
        /// </summary>
        public int UIRgrpEqualizationModeRadioGroupSelectedIndex = 1;

        /// <summary>
        /// Type '1' in 'rgrpEqualizationPlacement' RadioGroup
        /// </summary>
        public int UIRgrpEqualizationPlacRadioGroupSelectedIndex = 1;

        /// <summary>
        /// Type '120 [SelectionStart]0[SelectionLength]3' in 'eNumber' text box
        /// </summary>
        public string UIENumberEditValueAsString = "120 [SelectionStart]0[SelectionLength]3";
        #endregion
}
}
