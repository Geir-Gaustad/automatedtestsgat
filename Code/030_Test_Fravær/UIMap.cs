namespace _030_Test_Fravær
{
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
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
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using CommonTestData;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Globalization;

    public partial class UIMap
    {
        #region Fields
        private TestContext TestContext;
        public string ReportFilePath;
        public string ReportFileName = "030_excell";
        public string FileType = ".xls";
        private CommonUIFunctions.UIMap UICommon;
        #endregion

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_030\");

            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }
        public bool RestoreDatabase()
        {
            return UICommon.RestoreDatabase();
        }
                
        public void LaunchAndUpdateGat()
        {
            KillGatProcess();
            Playback.Wait(3000);
            UICommon.LaunchGatturnus(true);
            LoginAndStartGat();
            Playback.Wait(5000);
        }

        public bool SelectDepartmentByName(string depName)
        {
            #region Variable Declarations
            var depTable = UINivåWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayDepartmentsLayoutControlItem.UIGbDepartmentsClient.UIGDepartmentsTable;
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
        /// LoginAndStartGat - Use 'LoginAndStartGatParams' to pass parameters into this method.
        /// </summary>
        public void LoginAndStartGat()
        {
            #region Variable Declarations
            WinEdit uIItemEdit = this.UILoginWindow.UIItemWindow.UIItemEdit;
            WinButton uIOKButton = this.UILoginWindow.UILoginWindow1.UILoginClient.UIOKButton;
            WinButton uIOKButton1 = this.UISystemmelding1Window.UIItemWindow.UISystemmelding1Client.UIOKButton;
            DXMenuBaseButtonItem uIBtnDepartmentBarBaseButtonItem = this.UIGatver63437793ASCLAvWindow.UIStandaloneBarDockConCustom.UIMainmenuMenuBar.UIBtnDepartmentBarBaseButtonItem;
            #endregion

            InputUserName();

            // Type '********' in text box
            Keyboard.SendKeys(uIItemEdit, this.LoginAndStartGatParams.UIItemEditSendKeys, true);

            // Click 'OK' button
            Mouse.Click(uIOKButton);

            if (UIResultaterfrasystemsWindow.Exists)
                CloseSysCheckWindow();

            // Click '&OK' button
            if (UISystemmelding1Window.Exists)
                Mouse.Click(uIOKButton1);

            LogRunningGatVersion();

            // Click 'btnDepartment' BarBaseButtonItem
            Mouse.Click(uIBtnDepartmentBarBaseButtonItem, new Point(34, 10));

            SelectDepartmentByName(UICommon.DepStatistikkavd);
        }

        public void LogRunningGatVersion()
        {
            #region Variable Declarations
            var mainWindow = this.UIGatver63437793ASCLAvWindow;
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

            Playback.Wait(1000);
        }

        public void KillGatProcess()
        {
            SupportFunctions.KillGatProcess(TestContext);
        }

        public void DeleteReportFiles()
        {
            System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(ReportFilePath);
            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
            {
                if ((file.Extension != ".prnx" && file.Extension != ".xls" && file.Extension != ".xlsx") || file.Name.Contains("Facit"))
                    continue;

                file.Delete();
            }
        }

        public List<String> CompareReportDataFiles_Test030()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext);
            return errorList;
        }
        public void AssertResults(List<String> errorList)
        {
            Assert.Fail(SupportFunctions.AssertResults(errorList));
        }

        public string ReadPhysicalMemoryUsage()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.WorkingSet64);
        }
        public string ReadPagedMemorySize64()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.PagedMemorySize64);
        }

        /// <summary>
        /// SelectReportFromF3 - Use 'SelectReportFromF3Params' to pass parameters into this method.
        /// </summary>
        public void SelectReportFromF3()
        {
            #region Variable Declarations
            WinEdit uIItemEdit = this.UIVelgradWindow.UIItemWindow.UIItemEdit;
            #endregion

            var toClick = new Point(UIGatver63437793ASCLAvWindow1.Width / 4, UIGatver63437793ASCLAvWindow1.Height / 2);
            Mouse.Click(toClick);

            // Type '{F3}' in 'Panel1' client
            Keyboard.SendKeys(this.SelectReportFromF3Params.UIPanel1ClientSendKeys, ModifierKeys.None);

            // Type '95' in text box
            Keyboard.SendKeys(uIItemEdit, this.SelectReportFromF3Params.UIItemEditText, ModifierKeys.None);

            // Type '{Enter}' in text box
            Playback.Wait(1000);
            Keyboard.SendKeys(uIItemEdit, this.SelectReportFromF3Params.UIItemEditSendKeys, ModifierKeys.None);
        }


        /// <summary>
        /// GenerateVacantShift - Use 'GenerateVacantShiftParams' to pass parameters into this method.
        /// </summary>
        public void GenerateFreeShift(string dayCodeName, string from = "", string to = "")
        {
            #region Variable Declarations
            DXListBox uIPeriodScheduleListBoList = this.UIGatver63438164ASCLAvWindow.UICenterPanelPeriodDockPanel.UIControlContainerCustom.UIPeriodScheduleControCustom.UILayoutPanelSplitContainerControl.UITopPanelClient.UIXtraScrollableControScrollableControl.UIPeriodScheduleListBoList;
            uIPeriodScheduleListBoList.SearchProperties[DXTestControl.PropertyNames.Name] = dayCodeName;
            DXMenuBaseButtonItem uIGenerernyledigvaktMenuBaseButtonItem = this.UIItemWindow2.UIPopupMenuBarControlMenu.UIGenerernyledigvaktMenuBaseButtonItem;
            DXLookUpEdit uILeShiftCodeLookUpEdit = this.UILedigvaktWindow.UIGsPanelControl5Client.UIGsPanelControl2Client.UILeShiftCodeLookUpEdit;
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UILedigvaktWindow.UIGsPanelControl5Client.UIGSPanelControlClient.UIGSLookUpEditLookUpEdit;
            DXButton uIOKButton = this.UILedigvaktWindow.UIOKButton;
            #endregion

            // Right-Click 'PeriodScheduleListBoxControl`1' list box
            Mouse.Click(uIPeriodScheduleListBoList, MouseButtons.Right);

            // Click 'Generer ny ledigvakt...' MenuBaseButtonItem
            Playback.Wait(1000);
            Mouse.Click(uIGenerernyledigvaktMenuBaseButtonItem);

            Playback.Wait(1000);
            if (from != "" && to != "")
            {
                SetFreeShiftPeriod(from, to);
            }
            else
            {
                Mouse.Click(uILeShiftCodeLookUpEdit);
                Keyboard.SendKeys(uILeShiftCodeLookUpEdit, "{DOWN}{ENTER}");
            }

            Playback.Wait(1000);
            Mouse.Click(uIGSLookUpEditLookUpEdit);
            Keyboard.SendKeys(uIGSLookUpEditLookUpEdit, "{DOWN 10}{ENTER}");

            // Click '&OK' button
            Playback.Wait(1000);
            Mouse.Click(uIOKButton);
        }

        /// <summary>
        /// SetFreeShiftPeriod - Use 'SetFreeShiftPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetFreeShiftPeriod(string from, string to)
        {
            #region Variable Declarations
            DXTextEdit uIETime0Edit = this.UILedigvaktWindow.UIGsPanelControl5Client.UIGsPanelControl2Client.UIETime0Edit;
            DXTextEdit uIETime1Edit = this.UILedigvaktWindow.UIGsPanelControl5Client.UIGsPanelControl2Client.UIETime1Edit;
            #endregion

            // Type '06:00 [SelectionStart]0[SelectionLength]5' in 'eTime[0]' text box
            //ValueAsString
            uIETime0Edit.ValueAsString = from;
            Keyboard.SendKeys(uIETime0Edit, "{TAB}");

            // Type '14:00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
            //ValueAsString
            uIETime1Edit.ValueAsString = to;
            Keyboard.SendKeys(uIETime1Edit, "{TAB}");
        }



        /// <summary>
        /// GenerateExtraShift - Use 'GenerateExtraShiftParams' to pass parameters into this method.
        /// </summary>
        public void GenerateExtraShift(DateTime date, string from = "", string to = "")
        {
            #region Variable Declarations
            DXRibbonButtonItem uIEkstraRibbonBaseButtonItem = this.UIGatver63438164ASCLAvWindow.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIEkstraRibbonBaseButtonItem;
            DXLookUpEdit uICbOvertimeCodeLookUpEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UINbMenuNavBar.UICbOvertimeCodeLookUpEdit;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit1 = this.UIMerarbeidovertidWindow.UIPanClientClient.UINbMenuNavBar.UIGSSearchLookUpEditLookUpEdit1;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit11 = this.UIMerarbeidovertidWindow.UIPanClientClient.UINbMenuNavBar.UIGSSearchLookUpEditLookUpEdit11;
            DXPopupEdit uIPceDatePopupEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UIPceDatePopupEdit;
            DXLookUpEdit uICbShiftCodeLookUpEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList1.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UICbShiftCodeLookUpEdit;
            DXButton uIOKButton = this.UIMerarbeidovertidWindow.UIGsPanelControl1Client.UIOKButton;
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow2.UIPopupMenuBarControlMenu.UIBiOkMenuBaseButtonItem;
            #endregion

            // Click 'Ekstra' RibbonBaseButtonItem
            Mouse.Click(uIEkstraRibbonBaseButtonItem);

            // Type 'Gatsoft.Gat.DataModel.OvertimeCode' in 'cbOvertimeCode' LookUpEdit
            //ValueTypeName
            Playback.Wait(1000);
            uICbOvertimeCodeLookUpEdit.ValueTypeName = this.GenerateExtraShiftParams.UICbOvertimeCodeLookUpEditValueTypeName;

            // Type 'OvertimeCode(Id=242481501)' in 'cbOvertimeCode' LookUpEdit
            //ValueAsString
            Playback.Wait(1000);
            uICbOvertimeCodeLookUpEdit.ValueAsString = this.GenerateExtraShiftParams.UICbOvertimeCodeLookUpEditValueAsString;

            Playback.Wait(1000);
            Mouse.Click(uIGSSearchLookUpEditLookUpEdit1);
            Keyboard.SendKeys("{DOWN}{ENTER}");

             UICommon.SetExtraDate(date);
        

            Playback.Wait(1000);
            if (from == "" && to == "")
            {
                Mouse.Click(uICbShiftCodeLookUpEdit);
                Keyboard.SendKeys("{DOWN}{ENTER}");
            }
            else
            {
                SetExtraPeriod(from, to);
                SelectExtraColumn();
            }

            // Click '&OK' button
            Playback.Wait(1000);
            Mouse.Click(uIOKButton);

            // Click 'biOk' MenuBaseButtonItem
            Playback.Wait(1000);
            Mouse.Click(uIBiOkMenuBaseButtonItem);

            AMLCheck();
        }


        /// <summary>
        /// AMLCheck - Use 'AMLCheckParams' to pass parameters into this method.
        /// </summary>
        public void AMLCheck()
        {
            #region Variable Declarations
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow5.UIGSLookUpEditLookUpEdit;
            DXTextEdit uIECommentEdit = this.UIItemWindow5.UIECommentEdit;
            DXButton uIOKButton = this.UIItemWindow5.UIOKButton;
            #endregion

            Playback.Wait(1000);

            try
            {
                if (!UIItemWindow5.Exists)
                    return;
            }
            catch
            {
                return;
            }

            Mouse.Click(uIGSLookUpEditLookUpEdit);
            Keyboard.SendKeys("{DOWN}{ENTER}");

            // Type 'Rapport 95' in 'eComment' text box
            //ValueAsString
            uIECommentEdit.ValueAsString = this.AMLCheckParams.UIECommentEditValueAsString;

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }

        /// <summary>
        /// SetExtraPeriod - Use 'SetExtraPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetExtraPeriod(string from, string to)
        {
            #region Variable Declarations
            DXTextEdit uIETime3Edit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UIETime3Edit;
            DXTextEdit uIETime1Edit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UIETime1Edit;
            #endregion

            //ValueAsString
            uIETime3Edit.ValueAsString = from;
            // Type '{Tab}' in 'eTime[3]' text box
            Keyboard.SendKeys(uIETime3Edit, this.SetExtraPeriodParams.UIETAB, ModifierKeys.None);

            //ValueAsString
            uIETime1Edit.ValueAsString = to;
            // Type '{NumPad0}{Tab}' in 'eTime[1]' text box
            Keyboard.SendKeys(uIETime1Edit, this.SetExtraPeriodParams.UIETAB, ModifierKeys.None);
        }

        public void HighLigtReportPanel()
        {
            #region Variable Declarations
            WinClient uIPanel1Client = this.UIGatver63437793ASCLAvWindow1.UIItemWindow1.UIPanel1Client;
            #endregion

            //uIPanel1Client.DrawHighlight();
            Mouse.Click(uIPanel1Client, new Point(109, 384));
        }

        /// <summary>
        /// OpenReport95AndSaveToExcel - Use 'OpenReport95AndSaveToExcelParams' to pass parameters into this method.
        /// </summary>
        public List<string> OpenReport95AndSaveToExcel(string fileName, bool selectReport, string from = "", string to = "")
        {
            #region Variable Declarations
            var errorList = new List<string>();
            WinClient uIGatver63437793ASCLAvClient = this.UIGatver63437793ASCLAvWindow1.UIItemWindow.UIGatver63437793ASCLAvClient;
            WinClient uIPanel1Client = this.UIGatver63437793ASCLAvWindow1.UIItemWindow1.UIPanel1Client;
            WinEdit uIItemEdit = this.UIGatver63437793ASCLAvWindow1.UIItemWindow2.UIItemEdit;
            WinEdit uIItemEdit1 = this.UIGatver63437793ASCLAvWindow1.UIItemWindow3.UIItemEdit;
            WinClient uIRapportsenterClient = this.UIGatver63437793ASCLAvWindow1.UIItemWindow4.UIRapportsenterClient;
            WinButton uICloseButton = this.UIPrintPreviewWindow.UIPrintPreviewTitleBar.UICloseButton;
            WinClient uIRapportsenterClient1 = this.UIGatver63437793ASCLAvWindow1.UIItemWindow5.UIRapportsenterClient;
            WinButton uIFileTabButton = this.UIBook1_ASCLCompatibilWindow.UIItemWindow.UIRibbonPropertyPage.UIFileTabButton;
            WinTabPage uISaveAsTabPage = this.UIBook1_ASCLCompatibilWindow.UIFileMenuBar.UISaveAsTabPage;
            WinButton uIBrowseButton = this.UIBook1_ASCLCompatibilWindow.UIPickaFolderGroup.UIBrowseButton;
            WinEdit uIFilenameEdit = this.UISaveAsWindow.UIItemWindow.UIFilenameEdit;
            WinTreeItem uILocalDiskCTreeItem = this.UISaveAsWindow.UITreeViewWindow.UITreeViewTree.UIDesktopTreeItem.UIComputerTreeItem.UILocalDiskCTreeItem;
            WinButton uISaveButton = this.UISaveAsWindow.UISaveWindow.UISaveButton;
            WinButton uICloseButton1 = this.UIBook1_ASCLCompatibilWindow.UIItemWindow.UIRibbonPropertyPage.UICloseButton;
            #endregion


            try
            {
                // Click 'Gat ver. 6.3.4.37793 - (ASCL - Avd: 95-Statistikka...' client
                //Mouse.Click(uIGatver63437793ASCLAvClient, new Point(589, 16));
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.ReportCurrent);

                if (selectReport)
                {
                    SelectReportFromF3();
                }


                // Click 'Rapportsenter' client
                Mouse.Click(uIRapportsenterClient1, new Point(250, 18));

                //// Click 'File Tab' button
                //Mouse.Click(uIFileTabButton, new Point(30, 13));

                //// Click 'Save As' tab
                //Mouse.Click(uISaveAsTabPage, new Point(41, 17));

                //// Click 'Browse' button
                //Mouse.Click(uIBrowseButton, new Point(37, 41));

                //// Click 'File name:' text box
                //Mouse.Click(uIFilenameEdit, new Point(155, 13));

                //string filePath = ReportFilePath + ReportFileName + fileName + SupportFunctions.HeaderType._Common.ToString() + FileType;
                //Keyboard.SendKeys(uIFilenameEdit, "a", ModifierKeys.Control);
                //Keyboard.SendKeys(uIFilenameEdit, filePath, ModifierKeys.None);

                //// Click '&Save' button
                //Mouse.Click(uISaveButton, new Point(42, 12));

                //// Click 'Close' button
                //Mouse.Click(uICloseButton1, new Point(4, 13));

                try
                {
                    var filePath = ReportFilePath + ReportFileName + fileName;
                    UICommon.ExportToExcel(filePath);
                }
                catch (Exception e)
                {
                    TestContext.WriteLine("Feil ved export til excel(" + fileName + "): " + e.Message);
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error saving report: " + fileName + ", " + e.Message);
            }

            return errorList;
        }

        /// <summary>
        /// AddVacantAndEffectuate - Use 'AddVacantAndEffectuateParams' to pass parameters into this method.
        /// </summary>
        public void AddEmpAndEffectuate(bool vacant)
        {
            #region Variable Declarations
            WinClient uIGatver63437793ASCLAvClient = this.UIGatver63437793ASCLAvWindow1.UIItemWindow.UIGatver63437793ASCLAvClient;
            DXCell uIRapport95Cell = this.UIGatver63437793ASCLAvWindow1.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIPnlGridClient.UIGcRosterPlansTable.UIRapport95Cell;
            DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanRapport95Window.UIRcMenuRibbon.UIRpPlanRibbonPage;
            //DXRibbonButtonItem uIAnsatteRibbonBaseButtonItem = this.UIArbeidsplanRapport95Window.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanRibbonPageGroup.UIAnsatteRibbonBaseButtonItem;
            DXButton uILeggtilvakantelinjerButton = this.UIAnsatteiArbeidsplanWindow.UIPnlTopButtonsClient.UILeggtilvakantelinjerButton;
            DXCell uIValgtCell = this.UIAnsatteiArbeidsplanWindow.UIPnlControlClient.UIGrpSelectedEmployeesClient.UIGcEmployeesTable.UIValgtCell;
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIAnsatteiArbeidsplanWindow.UIGSPanelControlClient.UIGSPanelControlClient1.UIGSLookUpEditLookUpEdit;
            DXLookUpEdit uIGSLookUpEditLookUpEdit1 = this.UIAnsatteiArbeidsplanWindow.UIGSPanelControlClient.UIGSPanelControlClient1.UIGSLookUpEditLookUpEdit1;
            DXButton uIGSSimpleButtonButton = this.UIAnsatteiArbeidsplanWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            DXRibbonButtonItem uIRedigerRibbonBaseButtonItem = this.UIArbeidsplanRapport95Window.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRibbonPageGroup9RibbonPageGroup.UIRedigerRibbonBaseButtonItem;
            DXCell uIItemCell = this.UIArbeidsplanRapport95Window.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            DXCell uIItemCell1 = this.UIArbeidsplanRapport95Window.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell1;
            DXGrid uIGcRosterPlanTable = this.UIArbeidsplanRapport95Window.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            DXMenuBaseButtonItem uIBarButtonItemLink0MenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIBarSubItemLink0MenuItem.UIBarButtonItemLink0MenuBaseButtonItem;
            DXButton uIGSSimpleButtonButton1 = this.UIItemWindow1.UIGSSimpleButtonButton;
            DXCell uIItemCell2 = this.UIArbeidsplanRapport95Window.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell2;
            DXTextEdit uIRow1ColumnRosterCellEdit = this.UIArbeidsplanRapport95Window.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit;
            DXRibbonButtonItem uIBtnOkRibbonBaseButtonItem = this.UIArbeidsplanRapport95Window.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRibbonPageGroup9RibbonPageGroup.UIBtnOkRibbonBaseButtonItem;
            DXRibbonItem uIIverksettingRibbonItem = this.UIArbeidsplanRapport95Window.UIRcMenuRibbon.UIRpPlanRibbonPage.UIGrpEffectuateRevolviRibbonPageGroup.UIIverksettingRibbonItem;
            DXMenuBaseButtonItem uIBtnEffectuateNextPerMenuBaseButtonItem = this.UIItemWindow2.UISubMenuBarControlMenu.UIBtnEffectuateNextPerMenuBaseButtonItem;
            DXButton uIIverksett1linjerButton = this.UIIverksetteWindow.UIPnlButtonsClient.UIIverksett1linjerButton;
            DXButton uIGSSimpleButtonButton2 = this.UIItemWindow3.UIGSSimpleButtonButton;
            DXButton uIGSSimpleButtonButton3 = this.UIItemWindow4.UIGSPanelControlClient.UIGSSimpleButtonButton;
            //WpfWindow uIWpfWindow = this.UIWpfWindow;
            #endregion

            // Click 'Gat ver. 6.3.4.37793 - (ASCL - Avd: 95-Statistikka...' client
            //Mouse.Click(uIGatver63437793ASCLAvClient, new Point(328, 15));
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);

            // Double-Click 'Rapport 95' cell
            Mouse.DoubleClick(uIRapport95Cell, new Point(121, 11));

            // Click 'rpPlan' RibbonPage
            Mouse.Click(uIRpPlanRibbonPage, new Point(22, 11));

            // Click 'Ansatte' RibbonBaseButtonItem


            Playback.Wait(1000);
            UICommon.ClickEmployeesButtonRosterplan();

            // Click 'Legg til vakante linjer' button
            Playback.Wait(1000);
            if (vacant)
            {
                AddVacantShift();
            }
            else
            {
                AddGGToPlan();
                uIItemCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanGridControlCell[View]gvRosterPlan[Row]2[Column]RosterCell_0";
                uIItemCell1.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanGridControlCell[View]gvRosterPlan[Row]2[Column]RosterCell_4";
                uIItemCell2.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlanGridControlCell[View]gvRosterPlan[Row]2[Column]RosterCell_5";
            }

            // Click 'Rediger' RibbonBaseButtonItem
            Playback.Wait(1000);
            Mouse.Click(uIRedigerRibbonBaseButtonItem, new Point(20, 35));

            // Move cell to cell
            Playback.Wait(1000);
            uIItemCell1.EnsureClickable(new Point(11, 9));
            Mouse.StartDragging(uIItemCell, new Point(22, 9));
            Mouse.StopDragging(uIItemCell1, new Point(11, 9));

            Keyboard.SendKeys(uIItemCell1, "D{TAB}");

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton1);

            Keyboard.SendKeys(uIItemCell2, "F1{TAB}");

            // Click 'btnOk' RibbonBaseButtonItem
            Mouse.Click(uIBtnOkRibbonBaseButtonItem, new Point(37, 12));

            // Click 'Iverksetting' RibbonItem
            Playback.Wait(1000);
            Mouse.Click(uIIverksettingRibbonItem, new Point(56, 14));

            // Click 'btnEffectuateNextPeriod' MenuBaseButtonItem
            Playback.Wait(1000);
            Mouse.Click(uIBtnEffectuateNextPerMenuBaseButtonItem, new Point(64, 11));

            // Click 'Iverksett 1 linjer' button
            Playback.Wait(1000);
            Mouse.Click(uIIverksett1linjerButton);

            // Click 'GSSimpleButton' button
            Playback.Wait(1000);
            Mouse.Click(uIGSSimpleButtonButton2);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton3);

            // Click 'Wpf' window
            //Mouse.Click(uIWpfWindow, new Point(147, 145));
            Playback.Wait(1000);
            CloseRosterPlanFromPlanTab();
        }

        /// <summary>
        /// CoverTuesdayVakantShiftEvenE - Use 'CoverTuesdayVakantShiftEvenEParams' to pass parameters into this method.
        /// </summary>
        public void CoverShiftsWithEvenE(string date, bool selectCause, string lineNo = "1")
        {
            #region Variable Declarations
            DXRibbonButtonItem uIOppdekkingRibbonBaseButtonItem = this.UIGatver63437793ASCLAvWindow1.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpVacancyRibbonPageGroup.UIOppdekkingRibbonBaseButtonItem;
            //DXPopupEdit uIPceDate1PopupEdit = this.UIOppdekkingssenterASCWindow.UIPceDate1PopupEdit;
            //DXPopupEdit uIPceDate0PopupEdit = this.UIOppdekkingssenterASCWindow.UIPceDate0PopupEdit;
            //DXButton uISøkButton = this.UIOppdekkingssenterASCWindow.UISøkButton;
            //DXCell uIItem19042016Cell = this.UIOppdekkingssenterASCWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem1LayoutControlItem.UIGcOrdersTable.UIItem19042016Cell;
            //DXRibbonButtonItem uIDekkoppRibbonBaseButtonItem = this.UIOppdekkingssenterASCWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIRcOrderRibbon.UIRpOrderRibbonPage.UIRpgOtherActionsRibbonPageGroup.UIDekkoppRibbonBaseButtonItem;
            //DXButton uINesteButton = this.UIVelgansattsomskaldekWindow.UINesteButton;
            //DXCell uIEkstravaktEvenCell = this.UIVelgansattsomskaldekWindow.UIGsPanelControl10Client.UIGManualTable.UIEkstravaktEvenCell;
            //uIEkstravaktEvenCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gManualGridControlCell[View]gvManual[Row]" + lineNo + "[Column]gcManual_Lastname";
            //DXButton uIWizardButtonButton = this.UIVelgansattsomskaldekWindow.UIWizardButtonButton;
            //DXButton uIOKButton = this.UIMerarbeidovertidWindow.UIGsPanelControl1Client.UIOKButton;
            //DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow2.UIPopupMenuBarControlMenu.UIBiOkMenuBaseButtonItem;
            //DXButton uICloseButton = this.UIOppdekkingssenterASCWindow.UICloseButton;
            #endregion

            // Click 'Oppdekking' RibbonBaseButtonItem
            Mouse.Click(uIOppdekkingRibbonBaseButtonItem);

            SelectShiftToCover(date);

            CoverShiftWithExtra(selectCause, lineNo);
        }
        
        /// <summary>
        /// SelectShiftToCover - Use 'SelectShiftToCoverParams' to pass parameters into this method.
        /// </summary>
        public void SelectShiftToCover(string date)
        {
            #region Variable Declarations
            DXRibbonEditItem uIBbiFromDateRibbonEditItem = this.UIOppdekkingssenterASCWindow.UIRcOrderRibbon.UIRpOrderRibbonPage.UIRibbonPageGroup2RibbonPageGroup.UIBbiFromDateRibbonEditItem;
            DXPopupEdit uIGSSmartDateEditEditoPopupEdit = this.UIOppdekkingssenterASCWindow.UIRcOrderRibbon.UIGSSmartDateEditEditoPopupEdit;
            DXCell uIItem19042016Cell = this.UIOppdekkingssenterASCWindow.UIGcOrdersTable.UIItem19042016Cell;
            DXRibbonButtonItem uIDekkoppRibbonBaseButtonItem = this.UIOppdekkingssenterASCWindow.UIRcOrderRibbon.UIRpOrderRibbonPage.UIRpgOtherActionsRibbonPageGroup.UIDekkoppRibbonBaseButtonItem;
            #endregion

            // Click 'bbiFromDate' RibbonEditItem
            Mouse.Click(uIBbiFromDateRibbonEditItem, new Point(62, 9));

            // Type 'Gatsoft.Date' in 'GSSmartDateEditEditor' PopupEdit
            //ValueTypeName
            uIGSSmartDateEditEditoPopupEdit.ValueTypeName = this.SelectShiftToCoverParams.UIGSSmartDateEditEditoPopupEditValueTypeName;

            // Type '19.04.2016' in 'GSSmartDateEditEditor' PopupEdit
            //ValueAsString
            uIGSSmartDateEditEditoPopupEdit.ValueAsString = date;

            // Type '{Tab}' in 'GSSmartDateEditEditor' PopupEdit
            Keyboard.SendKeys(uIGSSmartDateEditEditoPopupEdit, this.SelectShiftToCoverParams.UITab, ModifierKeys.None);

            // Type 'Gatsoft.Date' in 'GSSmartDateEditEditor' PopupEdit
            //ValueTypeName
            uIGSSmartDateEditEditoPopupEdit.ValueTypeName = this.SelectShiftToCoverParams.UIGSSmartDateEditEditoPopupEditValueTypeName;

            // Type 'date' in 'GSSmartDateEditEditor' PopupEdit
            //ValueAsString
            uIGSSmartDateEditEditoPopupEdit.ValueAsString = date;

            // Type '{Tab}' in 'GSSmartDateEditEditor' PopupEdit
            Keyboard.SendKeys(uIGSSmartDateEditEditoPopupEdit, this.SelectShiftToCoverParams.UITab, ModifierKeys.None);

            // Click 'selectedrow' cell
            Mouse.Click(uIItem19042016Cell, new Point(28, 9));

            // Click 'Dekk opp...' RibbonBaseButtonItem
            Mouse.Click(uIDekkoppRibbonBaseButtonItem, new Point(38, 31));
        }
        
        /// <summary>
        /// CoverShiftWithExtra
        /// </summary>
        public void CoverShiftWithExtra(bool selectCause, string lineNo = "1")
        {
            #region Variable Declarations
            DXButton uINesteButton = this.UIVelgansattsomskaldekWindow1.UINesteButton;
            DXCell uIEkstravaktEvenCell = this.UIVelgansattsomskaldekWindow1.UIGsPanelControl10Client.UIGManualTable.UIEkstravaktEvenCell;
            DXButton uIWizardButtonButton = this.UIVelgansattsomskaldekWindow1.UIWizardButtonButton;
            DXButton uIOKButton = this.UIMerarbeidovertidWindow.UIGsPanelControl1Client1.UIOKButton;
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow2.UIPopupMenuBarControlMenu1.UIBiOkMenuBaseButtonItem;
            DXRibbonButtonItem uIBtnCloseWindowRibbonBaseButtonItem = this.UIOppdekkingssenterASCWindow.UIRcOrderRibbon.UIRpOrderRibbonPage.UIRpgWindowRibbonPageGroup.UIBtnCloseWindowRibbonBaseButtonItem;
            uIEkstravaktEvenCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gManualGridControlCell[View]gvManual[Row]" + lineNo + "[Column]gcManual_Lastname";
            #endregion

            // Click '&Neste >' button
            Mouse.Click(uINesteButton);

            Playback.Wait(500);
            // Click 'Ekstravakt, Even' cell
            Mouse.Click(uIEkstravaktEvenCell);

            // Click '&Neste >' button
            Mouse.Click(uINesteButton);

            // Click 'WizardButton' button
            Mouse.Click(uIWizardButtonButton);

            Playback.Wait(1000);
            if (selectCause)
                SelectExtraCause();

            // Click '&OK' button
            Mouse.Click(uIOKButton);

            // Click 'biOk' MenuBaseButtonItem
            Mouse.Click(uIBiOkMenuBaseButtonItem);

            // Click 'btnCloseWindow' RibbonBaseButtonItem
            Mouse.Click(uIBtnCloseWindowRibbonBaseButtonItem);
        }

        public void GoToShiftDate(DateTime date)
        {
            GoToShiftDateNew(date); 
        }

        private void GoToShiftDateNew(DateTime date)
        {
            UICommon.GoToShiftbookdate(date);
        }

        public virtual LoginAndStartGatParams LoginAndStartGatParams
        {
            get
            {
                if ((this.mLoginAndStartGatParams == null))
                {
                    this.mLoginAndStartGatParams = new LoginAndStartGatParams();
                }
                return this.mLoginAndStartGatParams;
            }
        }

        private LoginAndStartGatParams mLoginAndStartGatParams;

        public virtual OpenReport95AndSaveToExcelParams OpenReport95AndSaveToExcelParams
        {
            get
            {
                if ((this.mOpenReport95AndSaveToExcelParams == null))
                {
                    this.mOpenReport95AndSaveToExcelParams = new OpenReport95AndSaveToExcelParams();
                }
                return this.mOpenReport95AndSaveToExcelParams;
            }
        }

        private OpenReport95AndSaveToExcelParams mOpenReport95AndSaveToExcelParams;

        public virtual SelectReportFromF3Params SelectReportFromF3Params
        {
            get
            {
                if ((this.mSelectReportFromF3Params == null))
                {
                    this.mSelectReportFromF3Params = new SelectReportFromF3Params();
                }
                return this.mSelectReportFromF3Params;
            }
        }

        private SelectReportFromF3Params mSelectReportFromF3Params;

        public virtual AddVacantAndEffectuateParams AddVacantAndEffectuateParams
        {
            get
            {
                if ((this.mAddVacantAndEffectuateParams == null))
                {
                    this.mAddVacantAndEffectuateParams = new AddVacantAndEffectuateParams();
                }
                return this.mAddVacantAndEffectuateParams;
            }
        }

        private AddVacantAndEffectuateParams mAddVacantAndEffectuateParams;

        public virtual CoverTuesdayVakantShiftEvenEParams CoverTuesdayVakantShiftEvenEParams
        {
            get
            {
                if ((this.mCoverTuesdayVakantShiftEvenEParams == null))
                {
                    this.mCoverTuesdayVakantShiftEvenEParams = new CoverTuesdayVakantShiftEvenEParams();
                }
                return this.mCoverTuesdayVakantShiftEvenEParams;
            }
        }

        private CoverTuesdayVakantShiftEvenEParams mCoverTuesdayVakantShiftEvenEParams;


        public virtual GenerateVacantShiftParams GenerateVacantShiftParams
        {
            get
            {
                if ((this.mGenerateVacantShiftParams == null))
                {
                    this.mGenerateVacantShiftParams = new GenerateVacantShiftParams();
                }
                return this.mGenerateVacantShiftParams;
            }
        }

        private GenerateVacantShiftParams mGenerateVacantShiftParams;

        public virtual SetFreeShiftPeriodParams SetFreeShiftPeriodParams
        {
            get
            {
                if ((this.mSetFreeShiftPeriodParams == null))
                {
                    this.mSetFreeShiftPeriodParams = new SetFreeShiftPeriodParams();
                }
                return this.mSetFreeShiftPeriodParams;
            }
        }

        private SetFreeShiftPeriodParams mSetFreeShiftPeriodParams;

        public virtual GenerateExtraShiftParams GenerateExtraShiftParams
        {
            get
            {
                if ((this.mGenerateExtraShiftParams == null))
                {
                    this.mGenerateExtraShiftParams = new GenerateExtraShiftParams();
                }
                return this.mGenerateExtraShiftParams;
            }
        }

        private GenerateExtraShiftParams mGenerateExtraShiftParams;

        public virtual SetExtraPeriodParams SetExtraPeriodParams
        {
            get
            {
                if ((this.mSetExtraPeriodParams == null))
                {
                    this.mSetExtraPeriodParams = new SetExtraPeriodParams();
                }
                return this.mSetExtraPeriodParams;
            }
        }

        private SetExtraPeriodParams mSetExtraPeriodParams;

        public virtual AMLCheckParams AMLCheckParams
        {
            get
            {
                if ((this.mAMLCheckParams == null))
                {
                    this.mAMLCheckParams = new AMLCheckParams();
                }
                return this.mAMLCheckParams;
            }
        }

        private AMLCheckParams mAMLCheckParams;

        public virtual SelectShiftToCoverParams SelectShiftToCoverParams
        {
            get
            {
                if ((this.mSelectShiftToCoverParams == null))
                {
                    this.mSelectShiftToCoverParams = new SelectShiftToCoverParams();
                }
                return this.mSelectShiftToCoverParams;
            }
        }

        private SelectShiftToCoverParams mSelectShiftToCoverParams;

    }
    /// <summary>
    /// Parameters to be passed into 'LoginAndStartGat'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class LoginAndStartGatParams
    {

        #region Fields
        /// <summary>
        /// Type '********' in text box
        /// </summary>
        public string UIItemEditSendKeys = "VLLA+bJzNf882FWpmiwJPY0v6P7+sGGK";

        /// <summary>
        /// Type '31' in 'VCrkScrollBar' ScrollBarControl
        /// </summary>
        public string UIVCrkScrollBarScrollBarControlValueAsString = "31";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'OpenReport95AndSaveToExcel'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class OpenReport95AndSaveToExcelParams
    {

        #region Fields

        public string UI1804 = "18.04.2016";
        public string UI2404 = "24.04.2016";
        public string UI0105 = "01.05.2016";
        /// <summary>
        /// Type '{Tab}' in text box
        /// </summary>
        public string UIItemEditSendKeys = "{Tab}";



        /// <summary>
        /// Type '_save' in 'Desktop' -> 'Computer' -> 'Local Disk (C:)' tree item
        /// </summary>
        public string UILocalDiskCTreeItemSendKeys = "_save";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectReportFromF3'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectReportFromF3Params
    {

        #region Fields
        /// <summary>
        /// Type '{F3}' in 'Panel1' client
        /// </summary>
        public string UIPanel1ClientSendKeys = "{F3}";

        /// <summary>
        /// Type '95' in text box
        /// </summary>
        public string UIItemEditText = "95";

        /// <summary>
        /// Type '{Enter}' in text box
        /// </summary>
        public string UIItemEditSendKeys = "{Enter}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddVacantAndEffectuate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddVacantAndEffectuateParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.DataModel.Simple+PositionCategory' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueTypeName = "Gatsoft.Gat.DataModel.Simple+PositionCategory";

        /// <summary>
        /// Type '' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueAsString = "";

        /// <summary>
        /// Type 'Gatsoft.Gat.DataModel.Simple+Position' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEdit1ValueTypeName = "Gatsoft.Gat.DataModel.Simple+Position";

        /// <summary>
        /// Type '' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEdit1ValueAsString = "";

        /// <summary>
        /// Type '{Right}' in 'gcRosterPlan' table
        /// </summary>
        public string UIGcRosterPlanTableSendKeys = "{Right}";

        /// <summary>
        /// Type 'f' in 'gcRosterPlan' table
        /// </summary>
        public string UIGcRosterPlanTableSendKeys1 = "f";

        /// <summary>
        /// Type 'f1' in '[Row]1[Column]RosterCell_5' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEditValueAsString = "f1";

        /// <summary>
        /// Type '{Tab}' in '[Row]1[Column]RosterCell_5' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEditSendKeys = "{Tab}";
        #endregion
    }

    public static class GenerateFreeShiftParams
    {

        #region Fields

        public static string UIMonday = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[0]";
        public static string UIWednesday = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[2]";
        public static string UISaturday = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[5]";
        public static string UIMonday_2 = "topPanelXtraScrollableControl[0]PeriodScheduleListBoxControl`1[0]";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CoverTuesdayVakantShiftEvenE'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CoverTuesdayVakantShiftEvenEParams
    {

        #region Fields

        public string UIPceDate18042016 = "18.04.2016";
        public string UIPceDate19042016 = "19.04.2016";
        public string UIPceDate20042016 = "20.04.2016";
        public string UIPceDate23042016 = "23.04.2016";
        public string UIPceDate25042016 = "25.04.2016";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'GenerateVacantShift'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class GenerateVacantShiftParams
    {

        #region Fields
        /// <summary>
        /// Type '' in 'leShiftCode' LookUpEdit
        /// </summary>
        public string UILeShiftCodeLookUpEditValueAsString = "";

        /// <summary>
        /// Type 'Gatsoft.Gat.DataModel.Simple+PositionCategory' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueTypeName = "Gatsoft.Gat.DataModel.Simple+PositionCategory";

        /// <summary>
        /// Type '' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueAsString = "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetFreeShiftPeriod'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetFreeShiftPeriodParams
    {

        #region Fields
        /// <summary>
        /// Type '06:00 [SelectionStart]0[SelectionLength]5' in 'eTime[0]' text box
        /// </summary>
        public string UIETime0600 = "06:00";
        public string UIETime0900 = "09:00";
        public string UIETime1100 = "11:00";
        /// <summary>
        /// Type '14:00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
        /// </summary>
        public string UIETime1400 = "14:00";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'GenerateExtraShift'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class GenerateExtraShiftParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.DataModel.OvertimeCode' in 'cbOvertimeCode' LookUpEdit
        /// </summary>
        public string UICbOvertimeCodeLookUpEditValueTypeName = "Gatsoft.Gat.DataModel.OvertimeCode";

        /// <summary>
        /// Type 'OvertimeCode(Id=242481501)' in 'cbOvertimeCode' LookUpEdit
        /// </summary>
        public string UICbOvertimeCodeLookUpEditValueAsString = "OvertimeCode(Id=242481501)";

        /// <summary>
        /// Type 'Gatsoft.Gat.DataStructures.SimpleEmployment' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit1ValueTypeName = "Gatsoft.Gat.DataStructures.SimpleEmployment";

        /// <summary>
        /// Type '21.04.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate' PopupEdit
        /// </summary>
        public DateTime UIPceDate21042016 = new DateTime(2016,04,21);//"21.04.2016";
        public DateTime UIPceDate22042016 = new DateTime(2016, 04, 22);//"22.04.2016";
        public DateTime UIPceDate24042016 = new DateTime(2016, 04, 24);//"24.04.2016";
        public DateTime UIPceDate26042016 = new DateTime(2016, 04, 26);//"26.04.2016";

        /// <summary>
        /// Type '' in 'cbShiftCode' LookUpEdit
        /// </summary>
        public string UICbShiftCodeLookUpEditValueAsString = "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetExtraPeriod'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetExtraPeriodParams
    {

        #region Fields
        /// <summary>
        /// Type '07:00
        /// </summary>
        public string UIETime0700 = "07:00";
        public string UIETime1000 = "10:00";
        public string UIETime1200 = "12:00";
        public string UIETime1400 = "14:00";

        /// <summary>
        /// Type '{Tab}' in 'eTime[3]' text box
        /// </summary>
        public string UIETAB = "{Tab}";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AMLCheck'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AMLCheckParams
    {

        #region Fields
        /// <summary>
        /// Type 'Rapport 95' in 'eComment' text box
        /// </summary>
        public string UIECommentEditValueAsString = "Rapport 95";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectShiftToCover'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectShiftToCoverParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Date' in 'GSSmartDateEditEditor' PopupEdit
        /// </summary>
        public string UIGSSmartDateEditEditoPopupEditValueTypeName = "Gatsoft.Date";

        /// <summary>
        /// Type '{Tab}' in 'GSSmartDateEditEditor' PopupEdit
        /// </summary>
        public string UITab = "{Tab}";
        #endregion
    }
}
