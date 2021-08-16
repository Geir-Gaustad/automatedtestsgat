namespace _093_Test_Helgeavtale_Spekter
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
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
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System.Diagnostics;
    using System.IO;
    using System.Security.Cryptography;
    using System.Data;
    using System.Windows.Forms;
    using CommonTestData;
    using System.Threading;
    using System.Globalization;


    public partial class UIMap
    {
        public CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        private UIMapVS2015Classes.UIMapVS2015 UIMapVS2015
        {
            get
            {
                if ((this.map1 == null))
                {
                    this.map1 = new UIMapVS2015Classes.UIMapVS2015();
                }

                return this.map1;
            }
        }

        private UIMapVS2015Classes.UIMapVS2015 map1;

        private UIMapVS2017Classes.UIMapVS2017 UIMapVS2017
        {
            get
            {
                if ((this.map2 == null))
                {
                    this.map2 = new UIMapVS2017Classes.UIMapVS2017();
                }

                return this.map2;
            }
        }

        private UIMapVS2017Classes.UIMapVS2017 map2;

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }

        #region Common Functions

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
        public void KillGatProcess()
        {
            Playback.Wait(3000);
            SupportFunctions.KillGatProcess(TestContext);
        }

        public string ReadPhysicalMemoryUsage()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.WorkingSet64);
        }
        public string ReadPagedMemorySize64()
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.PagedMemorySize64);
        }
        public List<string> TimeLapseInSeconds(DateTime timeBefore, DateTime timeAfter, string text, int bottonLimit = 0, int upperLimit = 0)
        {
            List<string> errorList = new List<string>();
            string elapsedTimeOutput = "";

            errorList.AddRange(LoadBalanceTesting.TimeLapseInSeconds(timeBefore, timeAfter, text, out elapsedTimeOutput, bottonLimit, upperLimit));
            TestContext.WriteLine(elapsedTimeOutput);

            return errorList;
        }

        public string DepHelgeavtale
        {
            get { return UICommon.DepHelgeavtale; }
        }

        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            //"7001 - Helg"
            UICommon.LoginGatAndSelectDepartment(DepHelgeavtale, null, "", logGatInfo);
        }

        public void CloseGat()
        {
            try
            {
                UICommon.CloseGat();
                Playback.Wait(3000);
            }
            catch (Exception)
            {
                KillGatProcess();
            }
        }

        public void Click_PlanTab()
        {
            UICommon.ClickRosterplanPlanTab();
        }

        public void Select_ShiftDistribution()
        {
            UICommon.UIMapVS2017.SelectViewFilter("Vaktfordeling");
        }

        /// <summary>
        /// Click_RosterEmployees
        /// </summary>
        public void Click_RosterEmployees()
        {
            UICommon.ClickEmployeesButtonRosterplan();
        }

        public List<String> CompareReportDataFiles_Test093()
        {
            var errorList = DataService.CompareReportDataFiles(this.SaveReportParams.ReportFilePath, this.SaveReportParams.FileType, TestContext, 8);
            return errorList;
        }

        public void AssertResults(List<String> errorList)
        {
            Assert.Fail(SupportFunctions.AssertResults(errorList));
        }

        public void DeleteReportFiles()
        {
            UICommon.UIMapVS2017.DeleteReportFiles(SaveReportParams.ReportFilePath);
        }
               
        #endregion

        #region Test 93 Functions

        static bool FilesAreEqual_OneByte(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                for (int i = 0; i < first.Length; i++)
                {
                    if (fs1.ReadByte() != fs2.ReadByte())
                        return false;
                }
            }

            return true;
        }

        static bool FilesAreEqual_Hash(FileInfo first, FileInfo second)
        {
            byte[] firstHash = MD5.Create().ComputeHash(first.OpenRead());
            byte[] secondHash = MD5.Create().ComputeHash(second.OpenRead());

            for (int i = 0; i < firstHash.Length; i++)
            {
                if (firstHash[i] != secondHash[i])
                    return false;
            }
            return true;
        }


        /// <summary>
        /// DeleteShift
        /// </summary>
        public void DeleteShift(string col, string employeeName, string period = "")
        {
            #region Variable Declarations
            //DXCell uIItem10HelgCell = this.UIGatver63031732ASCLAvWindow7.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIDag00167770770FalseDockPanel.UIControlContainerCustom.UIGcDayColumnTable.UIItem10HelgCell;
            DXRibbonButtonItem uISlettRibbonBaseButtonItem = this.UIGatver63031732ASCLAvWindow7.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpMiscRibbonPageGroup.UISlettRibbonBaseButtonItem;
            DXButton uIGSSimpleButtonButton = this.UIItemWindow.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            #endregion

            Select_RosterbookEmployee(col, employeeName, period);

            Mouse.Click(uISlettRibbonBaseButtonItem);

            try
            {
                Playback.Wait(1000);
                if (!UIItemWindow.Exists)
                    Mouse.Click(uISlettRibbonBaseButtonItem);
            }
            catch (Exception)
            {
                Mouse.Click(uISlettRibbonBaseButtonItem);
            }

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        public void DeleteShifts(string empName, string col, bool deleteAll = false, string statusCode = "")
        {
            #region Variable Declarations

            var uISlettRibbonBaseButtonItem = this.UIGatver63031732ASCLAvWindow7.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpMiscRibbonPageGroup.UISlettRibbonBaseButtonItem;
            var uIGSSimpleButtonButton = this.UIItemWindow.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            Playback.Wait(1000);
            #endregion

            Select_RosterbookEmployee(col, empName, "", statusCode);
            Mouse.Click(uISlettRibbonBaseButtonItem);

            try
            {
                Playback.Wait(1000);
                if (!UIItemWindow.Exists)
                    Mouse.Click(uISlettRibbonBaseButtonItem);
            }
            catch (Exception)
            {
                Mouse.Click(uISlettRibbonBaseButtonItem);
            }

            if (deleteAll)
            {
                var uIVelgalleButton = this.UIItemWindow.UIPcContentClient.UIVelgalleButton;
                Mouse.Click(uIVelgalleButton);
            }

            if (!uIGSSimpleButtonButton.Enabled)
            {
                UncheckShiftsToDeleteWithErrors();
            }

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        public void CreateSpekterPeriods()
        {
            UICommon.SelectFromAdministration("spekter perioder", true);
            UIMapVS2017.InsertSpekterPeriodValues("2013", "31.12.2012", "29.12.2013");
            UIMapVS2017.ClickOkNewSpekterPeriod();
            UIMapVS2017.InsertSpekterPeriodValues("2014", "30.12.2013", "28.12.2014");
            UIMapVS2017.ClickOkNewSpekterPeriod();
            UIMapVS2017.InsertSpekterPeriodValues("2015", "29.12.2014", "03.01.2016");

            UIMapVS2017.ClickOkSpekterPeriod();
            UIMapVS2017.CloseSpekterPeriodWindow();
            //UICommon.ClearAdministrationSearchString();
        }

        /// <summary>
        /// GoTo3105_Date2 - Use 'GoTo3105_Date2Params' to pass parameters into this method.
        /// </summary>
        public void GoToShiftBookDate(DateTime date)
        {
            Playback.Wait(1000);
            UICommon.GoToShiftbookdate(date);
        }

        public void Select_EmployeeSubTab(CommonUIFunctions.UIMap.EmployeeTabs tab, bool switchLines = false)
        {
            UICommon.SelectSubTabInEmployeeTab(tab, switchLines);
        }

        /// <summary>
        /// Click_ShiftBook
        /// </summary>
        public void Click_ShiftBook()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
        }

        /// <summary>
        /// Click_RosterplanTab
        /// </summary>
        public void Click_RosterplanTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }

        /// <summary>
        /// Click_EmployeeTab
        /// </summary>
        public void Click_EmployeeTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Employee);
        }

        /// <summary>
        /// Click_DepartmentTab
        /// </summary>
        public void Click_DepartmentTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Department);
        }

        /// <summary>
        /// OpenEdit_Extra
        /// </summary>
        public void OpenEdit_Extra(string type, string operation)
        {
            #region Variable Declarations
            DXRibbonButtonItem ribbonBaseButtonItem = null;

            if (operation == "Extra")
                ribbonBaseButtonItem = this.UIGatver63031732ASCLAvWindow7.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIEkstraRibbonBaseButtonItem;
            else if (operation == "Absence")
                ribbonBaseButtonItem = this.UIGatver63031732ASCLAvWindow7.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIFraværRibbonBaseButtonItem;

            #endregion

            var dragCell = Select_RosterbookEmployee(type, "10, Helg");
            // Click '10, Helg' cell
            if (dragCell == null || ribbonBaseButtonItem == null)
                return;

            Mouse.StartDragging(dragCell);
            Mouse.StopDragging(ribbonBaseButtonItem);
        }

        /// <summary>
        /// SelectReport96_F3 - Use 'SelectReport96_F3Params' to pass parameters into this method.
        /// </summary>
        public void SelectReport96_F3()
        {
            #region Variable Declarations
            WinClient uIPanel1Client = this.UIGatver63031732ASCLAvWindow2.UIItemWindow1.UIPanel1Client;
            WinEdit uIItemEdit = this.UIVelgradWindow.UIItemWindow.UIItemEdit;
            #endregion

            Playback.Wait(500);
            Mouse.Click(new Point(100, UIGatver63031732ASCLAvWindow2.Height / 2));
            Playback.Wait(500);
            // Type '{F3}' in 'Panel1' client
            Keyboard.SendKeys(this.SelectReport96_F3Params.UIPanel1ClientSendKeys, ModifierKeys.None);

            // Type '96' in text box
            //SelectReport96();
            Keyboard.SendKeys(uIItemEdit, this.SelectReport96_F3Params.UIItemEditText, ModifierKeys.None);

            // Type '{Enter}' in text box
            Keyboard.SendKeys(uIItemEdit, this.SelectReport96_F3Params.UIItemEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// SetPeriod - Use 'SetPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetPeriod(string from, string to)
        {
            #region Variable Declarations
            WinEdit uIItemEdit = this.UIGatver63031732ASCLAvWindow1.UIItemWindow1.UIItemEdit;
            //WinEdit uIItemEdit1 = this.UIGatver63031732ASCLAvWindow1.UIItemWindow2.UIItemEdit;
            WinEdit uIItemEdit2 = this.UIGatver63031732ASCLAvWindow1.UIItemWindow11.UIItemEdit;
            #endregion

            // Type date in text box
            uIItemEdit.Text = from; // this.SetPeriodParams.UIItemEditText;

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit, this.SetPeriodParams.UIItemEditSendTabKey, ModifierKeys.None);

            // Type date in text box
            uIItemEdit2.Text = to; // this.SetPeriodParams.UIItemEditText1;

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit2, this.SetPeriodParams.UIItemEditSendTabKey, ModifierKeys.None);
        }

        public void SelectSpekterPeriod(string period)
        {
            UIMapVS2017.SelectSpekterPeriod(period);
        }

        /// <summary>
        /// ZoomPreview
        /// </summary>
        public void ZoomPreview()
        {
            #region Variable Declarations
            DXMenuItem uIBarEditItemLink1BarItem = this.UIForhåndsvisningWindow.UIRibbonStatusBarMenuBar.UIBarEditItemLink1BarItem;
            //DXButton uIEditorButtonButton = this.UIForhåndsvisningWindow.UIRibbonStatusBarMenuBar.UIZoomTrackBarControlTrackBar.UIEditorButtonButton;
            #endregion

            // Click 'BarEditItemLink[1]' BarItem
            Mouse.Click(uIBarEditItemLink1BarItem, new Point(55, 10));

            //for (int i = 0; i < steps; i++)
            //{
            //    Mouse.Click(uIEditorButtonButton);
            //}
        }

        /// <summary>
        /// SaveReport - Use 'SaveReportParams' to pass parameters into this method.
        /// </summary>
        public void SaveReport(string postfix)
        {
            #region Variable Declarations
            DXRibbonButtonItem uILagreRibbonBaseButtonItem = this.UIForhåndsvisningWindow.UIRibbonControlRibbon.UIPrintPreviewRibbonPaRibbonPage.UIDocumentRibbonPageGroup.UILagreRibbonBaseButtonItem;
            WinComboBox uIFilenameComboBox = this.UISaveAsWindow.UIDetailsPanePane.UIFilenameComboBox;
            WinButton uISaveButton = this.UISaveAsWindow.UISaveWindow.UISaveButton;
            #endregion

            // Click 'Lagre' RibbonBaseButtonItem
            Mouse.Click(uILagreRibbonBaseButtonItem);

            // Select 'EmployeeWeekendWorkHoursReport_step2.prnx' in 'File name:' combo box
            uIFilenameComboBox.EditableItem = this.SaveReportParams.ReportFilePath + this.SaveReportParams.ReportFileName + postfix + this.SaveReportParams.FileType;

            // Click '&Save' button
            Mouse.Click(uISaveButton);
        }

        /// <summary>
        /// ExportToExcell
        /// </summary>
        public List<string> ExportToExcell(string postfix)
        {
            var errorList = new List<string>();
            try
            {
                var fileName = SaveReportParams.ReportFilePath + SaveReportParams.ReportFileName + postfix;
                    UICommon.ExportToExcelFromPreview(fileName);
            }
            catch (Exception e)
            {
                errorList.Add("Feil ved export til excel(" + postfix + "): " + e.Message);
            }

            return errorList;
        }

        /// <summary>
        /// SaveReportAsXls - Use 'SaveReportAsXlsParams' to pass parameters into this method.
        /// </summary>
        public List<string> CheckReportData(string postfix)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXPrintingBrick startCell = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl1.UIPage0BrickIndices000PrintControlBrick;
            startCell.SearchProperties[DXTestControl.PropertyNames.Name] = "Page [0].BrickIndices [0].[0].[0].[65]";

            DXPrintingBrick stopCell = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl1.UIPage0BrickIndices010PrintControlBrick1;
            stopCell.SearchProperties[DXTestControl.PropertyNames.Name] = "Page [0].BrickIndices [0].[10].[0].[0]";

            if (postfix == "_chapter6_step_4" || postfix == "_chapter6_step_5" || postfix == "_chapter6_step_6")
                stopCell.SearchProperties[DXTestControl.PropertyNames.Name] = "Page [0].BrickIndices [0].[9].[0].[0]";
            #endregion

            ZoomPreview();

            try
            {
                stopCell.EnsureClickable();
            }
            catch (Exception)
            {
                stopCell.SearchProperties[DXTestControl.PropertyNames.Name] = "Page [0].BrickIndices [0].[8].[0].[0]";
                stopCell.EnsureClickable();
            }

            try
            {
                startCell.EnsureClickable();
            }
            catch (Exception)
            {
                try
                {
                    startCell.SearchProperties[DXTestControl.PropertyNames.Name] = "Page [0].BrickIndices [0].[0].[0].[64]";
                    startCell.EnsureClickable();
                }
                catch (Exception)
                {
                    startCell.SearchProperties[DXTestControl.PropertyNames.Name] = "Page [0].BrickIndices [0].[0].[0].[12]";
                    startCell.EnsureClickable();
                }
            }

            try
            {
                Mouse.StartDragging(startCell);
                Mouse.StopDragging(stopCell);
            }
            catch (Exception e)
            {
                errorList.Add("Error: " + postfix + ", " + e.Message);
                return errorList;
            }

            var clipText = "NA";
            var expectedText = "NA";
            try
            {
                Clipboard.Clear();
                Keyboard.SendKeys("c", ModifierKeys.Control);

                clipText = Clipboard.GetText();
                Clipboard.Clear();

                var reportName = this.SaveReportParams.ReportFilePath + this.SaveReportParams.ReportFileName + postfix + ".txt";

                ////Brukes dersom det skal opprettes nye rapporter
                //File.WriteAllText(reportName, clipText);

                expectedText = File.ReadAllText(reportName);
                Assert.AreEqual(expectedText, clipText);

                TestContext.WriteLine("Verdier sjekket i " + postfix + ": " + clipText);
            }
            catch (Exception e)
            {
                errorList.Add("Error in report: " + postfix + ", " + e.Message + ". Expected: " + expectedText + ". Actual: " + clipText);
            }

            return errorList;
        }


        /// <summary>
        /// OverWriteReport
        /// </summary>
        private void OverWriteReport(bool overWrite)
        {
            Playback.Wait(1000);
            try
            {
                if (this.UIConfirmSaveAsWindow.Exists)
                {
                    #region Variable Declarations
                    WinButton uIYesButton = this.UIConfirmSaveAsWindow.UIConfirmSaveAsPane.UIYesButton;
                    WinButton uINOButton = this.UIConfirmSaveAsWindow.UIConfirmSaveAsPane.UINOButton;
                    #endregion

                    if (overWrite)
                        Mouse.Click(uIYesButton);//Keyboard.SendKeys("y", ModifierKeys.Alt);
                    else
                        Mouse.Click(uINOButton);//Keyboard.SendKeys("n", ModifierKeys.Alt);
                }
            }
            catch (Exception)
            { TestContext.WriteLine("Overwrite report: " + overWrite); }
        }

        /// <summary>
        /// Click_Administration
        /// </summary>
        public void Click_AdministrationTab()
        {
            #region Variable Declarations
            WinClient uIGatver63031732ASCLAvClient = this.UIGatver63031732ASCLAvWindow5.UIItemWindow.UIGatver63031732ASCLAvClient;
            #endregion

            // Click 'Gat ver. 6.3.0.31732 - (ASCL - Avd: 7001-Helgeavta...' client
            //Gat v<6.4 => Click_Administration();
            //Mouse.Click(uIGatver63031732ASCLAvClient, new Point(914, 14));
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Administration);

        }

        public void SelectFromAdministration(string searchString)
        {
            UICommon.SelectFromAdministration(searchString);
        }

        /// <summary>
        /// ActivateBeregningsregel44_410_420 - Use 'ActivateBeregningsregel44_410_420Params' to pass parameters into this method.
        /// </summary>
        public void ActivateBeregningsregel44_410_420(bool activate)
        {
            #region Variable Declarations
            DXButton uIMaximizeButton = this.UIBeregningsreglerWindow.UIMaximizeButton;
            DXCell uIUtrykningpåhjemmevakCell = this.UIBeregningsreglerWindow.UIPcContentClient.UISplitContainerControSplitContainerControl.UISplitGroupPanelClient.UIGcRuleTypesTable.UIUtrykningpåhjemmevakCell;
            DXCheckBox uICeInactiveCheckBox = this.UIEndreberegningsregelWindow.UIGsPanelControl1Client.UIGsPanelControl2Client.UICeInactiveCheckBox;
            DXButton uIOKButton = this.UIEndreberegningsregelWindow.UIOKButton;
            DXCell uIUtrykningpåhjemmevakCell1 = this.UIBeregningsreglerWindow.UIPcContentClient.UISplitContainerControSplitContainerControl.UISplitGroupPanelClient.UIGcRuleTypesTable.UIUtrykningpåhjemmevakCell1;
            DXButton uILukkButton = this.UIBeregningsreglerWindow.UILukkButton;
            #endregion

            // Click 'Maximize' button
            Mouse.Click(uIMaximizeButton, new Point(13, 7));

            uIUtrykningpåhjemmevakCell.DrawHighlight();

            // Double-Click 'Utrykning på hjemmevakt L/S (TEST SPEKTER)' cell
            Mouse.DoubleClick(uIUtrykningpåhjemmevakCell);

            // Clear 'ceInactive' check box
            uICeInactiveCheckBox.Checked = !activate;

            // Click '&OK' button
            Mouse.Click(uIOKButton);

            if (activate)
                // Double-Click 'Utrykning på hjemmevakt L/S (TEST SPEKTER)' cell
                Mouse.DoubleClick(uIUtrykningpåhjemmevakCell1);
            else
                // Double-Click 'Utrykning på hjemmevakt L/S (TEST SPEKTER)' cell
                Mouse.DoubleClick(uIUtrykningpåhjemmevakCell);

            // Clear 'ceInactive' check box
            uICeInactiveCheckBox.Checked = !activate;

            // Click '&OK' button
            Mouse.Click(uIOKButton);

            // Click '&Lukk' button
            Mouse.Click(uILukkButton);

            Playback.Wait(1000);
        }

        /// <summary>
        /// SelectLT_F3 - Use 'SelectLT_F3Params' to pass parameters into this method.
        /// </summary>
        public void SelectLT_F3()
        {
            #region Variable Declarations
            WinClient uIAdministrasjonClient = this.UIGatver63031732ASCLAvWindow6.UIItemWindow.UIAdministrasjonClient;
            WinEdit uIItemEdit = this.UIVelgradWindow.UIItemWindow.UIItemEdit;
            #endregion

            // Type '{F3}' in 'Administrasjon' client
            Keyboard.SendKeys(uIAdministrasjonClient, this.SelectLT_F3Params.UIAdministrasjonClientSendKeys, ModifierKeys.None);

            // Type 'LØNNS- OG TREKKARTER' in text box

            //DevEx < 15.2.4 => uIItemEdit.Text = this.SelectLT_F3Params.UIItemEditText;
            Playback.Wait(1000);
            Keyboard.SendKeys(uIItemEdit, this.SelectLT_F3Params.UIItemEditText, ModifierKeys.None);

            // Type '{Enter}' in text box
            Keyboard.SendKeys(uIItemEdit, this.SelectLT_F3Params.UIItemEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// AdminVaktkategorierSetUtrykningHhjemmevakt - Use 'AdminVaktkategorierSetUtrykningHhjemmevaktParams' to pass parameters into this method.
        /// </summary>
        public void AdminVaktkategorierSetUtrykningHjemmevakt(bool allowUtrykning)
        {
            #region Variable Declarations
            WinClient uIItemShiftCatClient = this.UIVaktkategorierWindow.UIVaktkategorierClient.UIItemClient;
            WinClient uIItemClient = this.UIVaktkategorierWindow.UIItemWindow.UIItemClient;
            WinComboBox uIItemComboBox = this.UIVaktkategorierWindow.UIItemWindow1.UIItemComboBox;
            WinButton uIOKButton = this.UIVaktkategorierWindow.UIItemClient.UIOKButton;
            WinClient uIItemClient1 = this.UIVaktkategorierWindow.UIItemClient1.UIItemClient;
            #endregion

            // Click client
            Mouse.Click(uIItemClient, new Point(98, 78));

            Mouse.Click(uIItemShiftCatClient, new Point(97, 35));

            // Select 'Hjemmevakt' in combo box
            if (allowUtrykning)
                uIItemComboBox.SelectedItem = this.AdminVaktkategorierSetUtrykningHhjemmevaktParams.UIItemComboBoxHjemmevakt;
            else
                uIItemComboBox.SelectedItem = this.AdminVaktkategorierSetUtrykningHhjemmevaktParams.UIItemComboBoxIkkeUtrykning;

            // Click 'OK' button
            Mouse.Click(uIOKButton, new Point(34, 24));

            // Click client
            Mouse.Click(uIItemClient1, new Point(346, 32));

            //UICommon.ClearAdministrationSearchString();
        }

        /// <summary>
        /// Select_ExtraEmployee - Use 'Select_ExtraEmployeeParams' to pass parameters into this method.
        /// </summary>
        public void Select_ExtraEmployee()
        {
            #region Variable Declarations
            DXLookUpEdit uICbEmploymentLookUpEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UINbMenuNavBar.UICbEmploymentLookUpEdit;
            uICbEmploymentLookUpEdit.SearchProperties.Remove(DXTestControl.PropertyNames.HierarchyLevel);
            #endregion

            Mouse.Click(uICbEmploymentLookUpEdit);

            Keyboard.SendKeys("6532168513");
            Keyboard.SendKeys("{ENTER}");
        }

        /// <summary>
        /// Select_ExtraDate - Use 'Select_ExtraDateParams' to pass parameters into this method.
        /// </summary>
        public void Select_ExtraDate(DateTime date)
        {
            Select_ExtraDateNew(date);
        }

        /// <summary>
        /// Select_ExtraDateNew - Use 'Select_ExtraDateNewParams' to pass parameters into this method.
        /// </summary>
        private void Select_ExtraDateNew(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIMerarbeidovertidWindow.UIPanClientClient1.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UIDeDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.DateTime = date;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetPeriodParams.UIItemEditSendTabKey);
        }

        /// <summary>
        /// Select_ExtraShiftCode_D - Use 'Select_ExtraShiftCode_DParams' to pass parameters into this method.
        /// </summary>
        public void Select_ExtraShiftCode(string shiftCode)
        {
            #region Variable Declarations
            DXLookUpEdit uICbShiftCodeLookUpEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UICbShiftCodeLookUpEdit;
            #endregion

            // Type '' in 'cbShiftCode' LookUpEdit
            //ValueAsString
            uICbShiftCodeLookUpEdit.ValueAsString = this.Select_ExtraShiftCode_DParams.UICbShiftCodeLookUpEditValueAsString;

            Mouse.Click(uICbShiftCodeLookUpEdit);
            Keyboard.SendKeys(shiftCode);//("D (07:00) - (15:00)");("N (23:00) - (07:00)");
            Keyboard.SendKeys("{ENTER}");
        }

        /// <summary>
        /// SetExtraFromToTimeAndColumn - Use 'SetExtraFromToTimeAndColumnParams' to pass parameters into this method.
        /// </summary>
        public void SetExtraFromToTimeAndColumn(string fromTime, string toTime, string column)
        {
            #region Variable Declarations
            DXTextEdit TimeEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UIETime0Edit;
            DXLookUpEdit uICbCrewColumnLookUpEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UICbCrewColumnLookUpEdit;
            TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.HierarchyLevel);
            TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            #endregion

            //TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            //if (Configuration.GatVersion == "6.1" || Configuration.GatVersion == "6.2")
            //    TimeEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eTime[1]";
            //else
            //    TimeEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eTime[3]";
            // Type '03:00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
            //ValueAsString

            TimeEdit.SearchProperties[WinWindow.PropertyNames.ControlName] = "tefromTime";
            var rec = TimeEdit.BoundingRectangle;
            var point = new Point(rec.X, rec.Y);
            Mouse.Click(point);
            Keyboard.SendKeys("a", ModifierKeys.Control);
            Keyboard.SendKeys(fromTime);
            //TimeEdit.ValueAsString = fromTime;

            // Type '{NumPad0}{Tab}' in 'eTime[1]' text box
            Keyboard.SendKeys(this.SetExtraToTimeAndColumnParams.TimeEditSendTab, ModifierKeys.None);

            //TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            //if (Configuration.GatVersion == "6.1" || Configuration.GatVersion == "6.2")
            //    TimeEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eTime[0]";
            //else
            //    TimeEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eTime[1]";
            // Type '07:00 [SelectionStart]0[SelectionLength]5' in 'eTime[0]' text box
            //ValueAsString
            TimeEdit.SearchProperties[WinWindow.PropertyNames.ControlName] = "teToTime";
            rec = TimeEdit.BoundingRectangle;
            point = new Point(rec.X, rec.Y);
            Mouse.Click(point);
            Keyboard.SendKeys("a", ModifierKeys.Control);
            Keyboard.SendKeys(toTime);
            //TimeEdit.ValueAsString = toTime;

            // Type '{NumPad0}{Tab}' in 'eTime[0]' text box
            Keyboard.SendKeys(this.SetExtraToTimeAndColumnParams.TimeEditSendTab, ModifierKeys.None);

            // Type 'Gatsoft.Gat.DataModel.CrewColumn' in 'cbCrewColumn' LookUpEdit
            //ValueTypeName
            uICbCrewColumnLookUpEdit.ValueTypeName = this.SetExtraToTimeAndColumnParams.UICbCrewColumnLookUpEditValueTypeName;

            // Type 'CrewColumn(Id=1)' in 'cbCrewColumn' LookUpEdit
            //ValueAsString
            uICbCrewColumnLookUpEdit.ValueAsString = column;
        }

        /// <summary>
        /// EditExtraShift - Use 'EditExtraShiftParams' to pass parameters into this method.
        /// </summary>
        public void EditExtraShift(string fromTime, string toTime, string columnName)
        {
            #region Variable Declarations
            DXButton uIEditorButton1Button = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UICbShiftCodeLookUpEdit.UIEditorButton1Button;
            //DXTextEdit uIETime1Edit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UIETime1Edit;
            DXTextEdit TimeEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UIETime0Edit;
            DXLookUpEdit uICbCrewColumnLookUpEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UITcRegistrationTypeTabList.UITpNewShiftClient.UICbCrewColumnLookUpEdit;
            TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.HierarchyLevel);
            TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            #endregion

            // Click 'EditorButton1' button
            Mouse.Click(uIEditorButton1Button);
            //TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);

            //if (Configuration.GatVersion == "6.1" || Configuration.GatVersion == "6.2")
            //    TimeEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eTime[1]";
            //else
            //    TimeEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eTime[3]";

            // Type '07:00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
            //ValueAsString
            TimeEdit.SearchProperties[WinWindow.PropertyNames.ControlName] = "tefromTime";

            var rec = TimeEdit.BoundingRectangle;
            var point = new Point(rec.X, rec.Y);
            Mouse.Click(point);
            Keyboard.SendKeys("a", ModifierKeys.Control);
            Keyboard.SendKeys(fromTime);
            //TimeEdit.ValueAsString = fromTime;

            // Type '{Tab}' in 'eTime[1]' text box
            Keyboard.SendKeys(this.EditExtraShiftParams.UIETab, ModifierKeys.None);

            //TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);

            //if (Configuration.GatVersion == "6.1" || Configuration.GatVersion == "6.2")
            //    TimeEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eTime[0]";
            //else
            //    TimeEdit.SearchProperties[DXTestControl.PropertyNames.Name] = "eTime[1]";

            // Type '17:00 [SelectionStart]0[SelectionLength]5' in 'eTime[0]' text box
            //ValueAsString
            TimeEdit.SearchProperties[WinWindow.PropertyNames.ControlName] = "teToTime";

            rec = TimeEdit.BoundingRectangle;
            point = new Point(rec.X, rec.Y);
            Mouse.Click(point);
            Keyboard.SendKeys("a", ModifierKeys.Control);
            Keyboard.SendKeys(toTime);

            //TimeEdit.ValueAsString = toTime;

            // Type '{NumPad0}{Tab}' in 'eTime[0]' text box
            Keyboard.SendKeys(this.EditExtraShiftParams.UIETab, ModifierKeys.None);

            Mouse.Click(uICbCrewColumnLookUpEdit);
            Keyboard.SendKeys(uICbCrewColumnLookUpEdit, columnName, ModifierKeys.None);
            Keyboard.SendKeys(uICbCrewColumnLookUpEdit, "{ENTER}", ModifierKeys.None);
        }

        /// <summary>
        /// Click_Ok_Extra
        /// </summary>
        public void Click_Ok_Extra()
        {
            #region Variable Declarations
            DXButton uIOKButton = this.UIMerarbeidovertidWindow.UIGsPanelControl1Client.UIOKButton;
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBiOkMenuBaseButtonItem;
            #endregion

            Playback.Wait(500);
            Mouse.Click(uIOKButton);
            try
            {
                Playback.Wait(1000);
                uIBiOkMenuBaseButtonItem.WaitForControlReady();
                Mouse.Click(uIBiOkMenuBaseButtonItem);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error clicking OK-button(Overtime window): " + ex.Message);
            }
        }

        /// <summary>
        /// Set_UtrykningsPeriod - Use 'Set_UtrykningsPeriodParams' to pass parameters into this method.
        /// </summary>
        public void Set_UtrykningsPeriod()
        {
            #region Variable Declarations
            DXTextEdit uIETime0Edit = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem5LayoutControlItem.UI_tcRightTabList.UI_tpCalloutDetailsClient.UI_periodCreatorCustom.UIETime0Edit;
            DXTextEdit uIETime1Edit = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem5LayoutControlItem.UI_tcRightTabList.UI_tpCalloutDetailsClient.UI_periodCreatorCustom.UIETime1Edit;
            #endregion

            // Type '16:00 [SelectionStart]0[SelectionLength]5' in 'eTime[0]' text box
            //ValueAsString
            uIETime0Edit.ValueAsString = this.Set_UtrykningsPeriodParams.UIETime0EditValueAsString;
            Keyboard.SendKeys(uIETime0Edit, "{TAB}");

            // Type '20:00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
            //ValueAsString
            uIETime1Edit.ValueAsString = this.Set_UtrykningsPeriodParams.UIETime1EditValueAsString;
            Keyboard.SendKeys(uIETime1Edit, "{TAB}");
        }

        /// <summary>
        /// Absence_Helg14
        /// </summary>
        public void OpenAbsence()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIFraværRibbonBaseButtonItem = this.UIGatver63031732ASCLAvWindow7.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIFraværRibbonBaseButtonItem;
            #endregion

            //uIFraværRibbonBaseButtonItem.EnsureClickable();
            Mouse.Click(uIFraværRibbonBaseButtonItem);
        }


        /// <summary>
        /// OpenEdit_Absence
        /// </summary>
        public void OpenEdit_Absence(string col, string emp, string absPeriod = "")
        {

            var selectedCell = Select_RosterbookEmployee(col, emp, absPeriod);

            try
            {
                Mouse.DoubleClick(selectedCell);
            }
            catch
            {
                Keyboard.SendKeys(selectedCell, "{ENTER}");
            }
        }

        /// <summary>
        /// Select_AbcenceCode - Use 'Select_AbcenceCodeParams' to pass parameters into this method.
        /// </summary>
        public void Select_AbcenceCode(string code)
        {
            #region Variable Declarations
            DXLookUpEdit uILueAbsenceCodesLookUpEdit = this.UIFraværsregistreringWindow.UINbcLeftNavBar.UILueAbsenceCodesLookUpEdit;
            #endregion

            // Type '10' in 'lueAbsenceCodes' LookUpEdit
            //ValueAsString
            Mouse.Click(uILueAbsenceCodesLookUpEdit);
            //uILueAbsenceCodesLookUpEdit.ValueAsString = this.Select_AbcenceCodeParams.UILueAbsenceCodesLookUpEditValueAsString;
            Keyboard.SendKeys(code);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("{ENTER}");
        }


        /// <summary>
        /// Select_EmployeeAbsence - Use 'Select_EmployeeAbsenceParams' to pass parameters into this method.
        /// </summary>
        public void Select_EmployeeAbsence(string empCode)
        {
            //SetKeyboardLayout("en");

            #region Variable Declarations
            DXLookUpEdit uILueEmploymentsLookUpEdit = this.UIFraværsregistreringWindow.UINbcLeftNavBar.UILueEmploymentsLookUpEdit;
            #endregion

            Clipboard.SetText(empCode);

            Mouse.Click(uILueEmploymentsLookUpEdit);

            Keyboard.SendKeys("v", ModifierKeys.Control);
            Clipboard.Clear();

            //SetKeyboardLayout("no");

            Playback.Wait(500);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("{ENTER}");

            // Type '{Tab}' in 'lueEmployments' LookUpEdit
            Keyboard.SendKeys(uILueEmploymentsLookUpEdit, this.Select_EmployeeAbsenceParams.UILueEmploymentsLookUpEditSendKeysTab, ModifierKeys.None);
        }

        private static void SetKeyboardLayout(string inputName)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName.ToLower().StartsWith(inputName))
                    InputLanguage.CurrentInputLanguage = lang;

            }

        }

        private static InputLanguage GetInputLanguageByName(string inputName)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName.ToLower().StartsWith(inputName))
                    return lang;
            }
            return null;
        }

        /// <summary>
        /// ChangeAbsTodate - Use 'ChangeAbsTodateParams' to pass parameters into this method.
        /// </summary>
        public void ChangeAbsPeriod(string fromTime = "", string toTime = "")
        {
            DateTime? from = new DateTime();
            var to = new DateTime();

            if (String.IsNullOrEmpty(fromTime))
                from = null;
            else
                from = Convert.ToDateTime(fromTime);

            if (String.IsNullOrEmpty(toTime))
                to = this.ChangeAbsPeriodNewParams.UIPceDate05072015asDateTime;
            else
                to = Convert.ToDateTime(toTime);

            UICommon.UIMapVS2017.SetAbsencePeriod(from, to);
        }

        /// <summary>
        /// RegHourlyAbsenceWithTimeOff - Use 'RegHourlyAbsenceWithTimeOffParams' to pass parameters into this method.
        /// </summary>
        public void RegHourlyAbsenceWithTimeOff(string fromTime, string toTime)
        {
            UICommon.UIMapVS2017.HourlyAbsence(fromTime, toTime, "", "");
        }

        /// <summary>
        /// Set_AbsTo50Percent - Use 'Set_AbsTo50PercentParams' to pass parameters into this method.
        /// </summary>
        public void Set_AbsTo50Percent()
        {
            UICommon.UIMapVS2017.AbsenceRate("50");
        }

        /// <summary>
        /// AdjustShiftSat - Use 'AdjustShiftSatParams' to pass parameters into this method.
        /// </summary>
        public void AdjustAbsenceShifts(string fromTime, string toTime)
        {
            #region Variable Declarations
            DXButton uIEditorButton = this.UIEndrevakterWindow.UIGsPanelControl1Client.UIGcShiftEditClient.UIPnlShiftEditScrollableControl.UICbShiftCodesLookUpEdit.UIEditorButton1Button;
            var TimeEdit = this.UIEndrevakterWindow.UIGsPanelControl1Client.UIGcShiftEditClient.UIPnlShiftEditScrollableControl.UIETime1Edit;
            DXCell uIDCell = this.UIEndrevakterWindow.UIGsPanelControl1Client.UIGcShiftListClient.UIGcShiftsTable.UIDCell;
            DXButton uIGSSimpleButtonButton = this.UIEndrevakterWindow.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.HierarchyLevel);
            TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            TimeEdit.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            #endregion

            // Click 'Valgt' cell
            UIMapVS2017.SelectAbsenceShifts_1();

            // Click 'Endre vakt(er)' button
            UICommon.UIMapVS2017.ClickChangeAbsenceShiftsButton();

            //Shift_1
            // Click 'D' cell
            uIDCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcShiftsGridControlCell[View]gvShifts[Row]0[Column]gccCode";
            Mouse.Click(uIDCell);
            uIDCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcShiftsGridControlCell[View]gvShifts[Row]0[Column]gccFrom";

            // Click 'EditorButton1' button
            Mouse.Click(uIEditorButton);

            TimeEdit.SearchProperties[WinWindow.PropertyNames.ControlName] = "eToTime";
            var rec = TimeEdit.BoundingRectangle;
            var point = new Point(rec.X, rec.Y);
            Mouse.Click(point);
            Keyboard.SendKeys("a", ModifierKeys.Control);
            Keyboard.SendKeys(toTime);

            // Type '{Tab}' in 'eTime[1]' text box
            Keyboard.SendKeys(this.AdjustShiftSunParams.UIETime1EditSendKeys, ModifierKeys.None);

            //Shift_2
            /*  gccCode
                gccFrom
                gccCrewColumn
             */
            // Click 'D' cell     
            Playback.Wait(1000);
            uIDCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcShiftsGridControlCell[View]gvShifts[Row]1[Column]gccCode";
            Mouse.Click(uIDCell);
            uIDCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcShiftsGridControlCell[View]gvShifts[Row]1[Column]gccFrom";
            Mouse.Click(uIDCell);
            // Click 'EditorButton1' button
            Mouse.Click(uIEditorButton);

            TimeEdit.SearchProperties[WinWindow.PropertyNames.ControlName] = "eFromTime";
            rec = TimeEdit.BoundingRectangle;
            point = new Point(rec.X, rec.Y);
            Mouse.Click(point);
            //Playback.Wait(1000);
            Keyboard.SendKeys("a", ModifierKeys.Control);
            //Keyboard.SendKeys("{DELETE}");
            //Playback.Wait(1000);
            Keyboard.SendKeys(fromTime);

            // Type '{Tab}' in 'eTime[1]' text box
            Keyboard.SendKeys(this.AdjustShiftSunParams.UIETime1EditSendKeys, ModifierKeys.None);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);//, new Point(1, 1));
        }

        /// <summary>
        /// ContructNewShift - Use 'ContructNewShiftParams' to pass parameters into this method.
        /// </summary>
        public void ContructNewShift()
        {
            UIMapVS2017.ContructNewDHShift();
        }

        /// <summary>
        /// Click_NewAgreement
        /// </summary>
        public void Click_NewAgreement()
        {
            #region Variable Declarations
            DXButton uINYButton = this.UIGatver63031732ASCLAvWindow11.UINYButton;
            var rec = uINYButton.BoundingRectangle;
            var point = new Point(rec.X + 10, rec.Y + 5);

            Playback.Wait(2000);
            #endregion

            Mouse.Click(point);

            Playback.Wait(2000);
            if (!UIHelgeavtaleWindow.Exists)
            {
                Mouse.Click(point);
            }
        }

        /// <summary>
        /// Click_Edit_EmpPosition
        /// </summary>
        public void Click_Edit_EmpPosition()
        {
            #region Variable Declarations
            DXButton uIEndreButton = this.UIGatver63031732ASCLAvWindow15.UIEndreButton;
            Playback.Wait(1000);
            #endregion

            uIEndreButton.WaitForControlReady(30000);

            var rec = uIEndreButton.BoundingRectangle;
            Point point = new Point(rec.X, rec.Y);
            Mouse.Click(point);
        }
        /// <summary>
        /// Create_Agreement - Use 'Create_AgreementParams' to pass parameters into this method.
        /// </summary>
        public void Create_Agreement()
        {
            #region Variable Declarations
            DXPopupEdit uIPceDatePopupEdit = this.UIHelgeavtaleWindow.UIGsPanelControl1Client.UIWeFromWeekCustom.UIPceDatePopupEdit;
            DXPopupEdit uIPceDatePopupEdit1 = this.UIHelgeavtaleWindow.UIGsPanelControl1Client.UIWeToWeekCustom.UIPceDatePopupEdit;
            DXTextEdit uIENumberEdit = this.UIHelgeavtaleWindow.UIGsPanelControl1Client.UIENumberEdit;
            #endregion

            // Type 'Uke 1 2015 [SelectionStart]0' in 'pceDate' PopupEdit
            Keyboard.SendKeys(uIPceDatePopupEdit, "1 2015" + this.EditExtraShiftParams.UIETab);

            // Type 'Uke 53 2015 [SelectionStart]0[SelectionLength]11' in 'pceDate' PopupEdit
            Keyboard.SendKeys(uIPceDatePopupEdit1, "53 2015" + this.EditExtraShiftParams.UIETab);

            // Type '18 [SelectionStart]0[SelectionLength]2' in 'eNumber' text box
            Keyboard.SendKeys(uIENumberEdit, "18" + this.EditExtraShiftParams.UIETab);
        }

        public void OpenPlan(string planName)
        {//"Grunnlag Helgerapportering"
            Playback.Wait(1500);
            Click_RosterplanTab();
            UICommon.SelectRosterPlan(planName);
        }

        public void ClickRosterplanPlanTab()
        {
            UICommon.ClickRosterplanPlanTab();
        }

        /// <summary>
        /// Click_NewRosterPlan
        /// </summary>
        public void ClickNewRosterPlanCopy()
        {
            UICommon.ClickNewRosterPlanCopy();
        }

        public void CloseRosterPlan()
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


        /// <summary>
        /// Click_EditCalendarPlanFromPlanTab
        /// </summary>
        public void Click_EditCalendarPlanFromPlanTab()
        {
            try
            {
                UIMapVS2015.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRibbonPageGroup9RibbonPageGroup.UIRedigerRibbonBaseButtonItem.WaitForControlExist(600000);
                UIMapVS2015.ClickEditRosterplanChap5_step3();
            }
            catch (Exception)
            {
                try
                {
                    UICommon.ClickEditRosterPlanFromPlantab();
                }
                catch (Exception)
                {
                    CloseRosterPlan();
                    OpenPlan("Chapter_5_step_1");

                    Click_PlanTab();
                    UICommon.ClickEditRosterPlanFromPlantab();
                }
            }
        }

        /// <summary>
        /// ChangeRosterIntervall - Use 'ChangeRosterIntervallParams' to pass parameters into this method.
        /// </summary>
        public void ChangeRosterIntervall(string from, string to)
        {
            #region Variable Declarations
            DXButton uIEndreperiodeforavkryButton = this.UIIverksetteWindow.UIPnlButtonsClient.UIEndreperiodeforavkryButton;
            DXButton uIGSSimpleButtonButton = this.UIEndreiverksettingspeWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Click 'Endre periode for avkryssede linjer' button
            Mouse.Click(uIEndreperiodeforavkryButton);

            ChangeRosterIntervallNew(from, to);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }


        /// <summary>
        /// ChangeRosterIntervallNew - Use 'ChangeRosterIntervallNewParams' to pass parameters into this method.
        /// </summary>
        private void ChangeRosterIntervallNew(string from, string to)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIEndreiverksettingspeWindow.UIEFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIEndreiverksettingspeWindow.UIEToDateCustom.UIPceDateDateTimeEdit;
            Playback.Wait(1000);
            #endregion

            if (from != "")
            {
                uIPceDateDateTimeEdit.DateTime = Convert.ToDateTime(from);
                Keyboard.SendKeys(uIPceDateDateTimeEdit, this.EditExtraShiftParams.UIETab);
            }

            if (to != "")
            {
                uIPceDateDateTimeEdit1.DateTime = Convert.ToDateTime(to);
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, this.EditExtraShiftParams.UIETab);
            }
        }

        ///// <summary>
        ///// Click_RosterEmployeesOk
        ///// </summary>
        //public void Click_RosterEmployeesOk()
        //{
        //    #region Variable Declarations
        //    DXButton uIGSSimpleButtonButton = this.UIAnsatteiArbeidsplanWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
        //    #endregion

        //    // Click 'GSSimpleButton' button
        //    try
        //    {
        //        Playback.Wait(1000);
        //        Mouse.Click(uIGSSimpleButtonButton);
        //    }
        //    catch (Exception)
        //    {
        //        try
        //        {
        //            var rec = uIGSSimpleButtonButton.BoundingRectangle;
        //            Point point = new Point(rec.X, rec.Y);
        //            Mouse.Click(point);
        //        }
        //        catch (Exception)
        //        {
        //            Keyboard.SendKeys("{ENTER}", ModifierKeys.None);
        //        }
        //    }
        //}

        //Chapter 2

        /// <summary>
        /// SetRosterStartAndWeeks - Use 'SetRosterStartAndWeeksParams' to pass parameters into this method.
        /// </summary>
        public void SetRosterStartAndWeeks(string startdate, string weeks)
        {
            #region Variable Declarations
            DXTextEdit uIENumber0Edit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIENumber0Edit;
            #endregion

            SetRosterStartDate(startdate);

            //ValueAsString
            uIENumber0Edit.ValueAsString = weeks; //this.SetRosterStartAndWeeksParams.UIENumber0EditValueAsString;
            Keyboard.SendKeys(uIENumber0Edit, this.EditExtraShiftParams.UIETab, ModifierKeys.None);
        }


        /// <summary>
        /// SetRosterStartDate - Use 'SetRosterStartDateParams' to pass parameters into this method.
        /// </summary>
        private void SetRosterStartDate(string startdate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UINyarbeidsplanWindow.UIPnlMainClient1.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIDeStartDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.DateTime = Convert.ToDateTime(startdate);
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");
        }

        /// <summary>
        /// SetStartDate_EasterPeriod - Use 'SetStartDate_EasterPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetStartDate_EasterPeriod()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIInnstillingerRibbonBaseButtonItem = this.UIArbeidsplanKopi_2avGWindow1.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanRibbonPageGroup.UIInnstillingerRibbonBaseButtonItem;
            DXLookUpEdit uILeDisplayStartDateLookUpEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UILeDisplayStartDateLookUpEdit;
            DXButton uIGSSimpleButtonButton = this.UIArbeidsplanInnstilliWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Click 'Innstillinger' RibbonBaseButtonItem
            Mouse.Click(uIInnstillingerRibbonBaseButtonItem); //, new Point(41, 30));

            // Type '2015-03-23' in 'leDisplayStartDate' LookUpEdit
            //ValueAsString
            //uILeDisplayStartDateLookUpEdit.ValueAsString = this.SetStartDate_EasterPeriodParams.UILeDisplayStartDateLookUpEditValueAsString;
            Keyboard.SendKeys(uILeDisplayStartDateLookUpEdit, "23.03.2015", ModifierKeys.None);
            //Keyboard.SendKeys(uILeDisplayStartDateLookUpEdit, this.EditExtraShiftParams.UIETime1EditSendKeys, ModifierKeys.None);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        /// <summary>
        /// Select_CauseCodeVacancyNurse - Use 'Select_CauseCodeVacancyNurseParams' to pass parameters into this method.
        /// </summary>
        public void Select_LineCauseCode(string causeCode)
        {
            #region Variable Declarations
            DXLookUpEdit uIDrdToSporadicLookUpEdit = this.UILinjeinnstillinger16Window.UIGsPanelControl3Client.UIGcOvertimeClient.UIDrdToSporadicLookUpEdit;
            #endregion
            Playback.Wait(1000);

            //ValueTypeName
            Keyboard.SendKeys(uIDrdToSporadicLookUpEdit, causeCode, ModifierKeys.None);
            Keyboard.SendKeys(uIDrdToSporadicLookUpEdit, "{ENTER}", ModifierKeys.None);
            Playback.Wait(1000);
        }

        ///// <summary>
        ///// ShowAllPlans - Use 'ShowAllPlansParams' to pass parameters into this method.
        ///// </summary>
        //public void ShowAllPlans()
        //{
        //    #region Variable Declarations
        //    DXLookUpEdit uICbRosterPlanFilterLookUpEdit = this.UIGatver63031732ASCLAvWindow13.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIPnlButtonsClient.UICbRosterPlanFilterLookUpEdit;
        //    #endregion

        //    // Type 'Gatsoft.Gat.BusinessLogic.Planning.RosterPlanning.RosterPlanList.RosterPlanFilterType' in 'cbRosterPlanFilter' LookUpEdit
        //    //ValueTypeName
        //    uICbRosterPlanFilterLookUpEdit.ValueTypeName = this.ShowAllPlansParams.UICbRosterPlanFilterLookUpEditValueTypeName;

        //    // Type 'All' in 'cbRosterPlanFilter' LookUpEdit
        //    //ValueAsString
        //    uICbRosterPlanFilterLookUpEdit.ValueAsString = this.ShowAllPlansParams.UICbRosterPlanFilterLookUpEditValueAsString;
        //}

        /// <summary>
        /// RenamePlan - Use 'RenamePlanParams' to pass parameters into this method.
        /// </summary>
        public void RenamePlan(string newName)
        {
            #region Variable Declarations
            DXTextEdit uITxtNameEdit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UITxtNameEdit;
            #endregion

            // Type 'Kopi_2 av Grunnlag Helgerapportering. [SelectionStart]6' in 'txtName' text box
            //ValueAsString
            uITxtNameEdit.ValueAsString = this.RenamePlanParams.UITxtNameEditValueAsString;

            //// Type '{Tab}'
            Keyboard.SendKeys(uITxtNameEdit, this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);

        }

        /// <summary>
        /// SetPlanToDate - Use 'SetPlanToDateParams' to pass parameters into this method.
        /// </summary>
        public void SetPlanToDate(string todate = "")
        {
            SetPlanToDateNew(this.SetPlanToDateNewParams.UIPceDateDateTimeEditValueAsDate);
        }


        /// <summary>
        /// SetPlanToDateNew - Use 'SetPlanToDateNewParams' to pass parameters into this method.
        /// </summary>
        private void SetPlanToDateNew(DateTime toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList1.UITpConfigurationClient.UIPaCenterClient.UIEValidToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            //ValueAsString
            uIPceDateDateTimeEdit.DateTime = toDate;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
        }

        /// <summary>
        /// Clear_RosterPlan - Use 'Clear_RosterPlanParams' to pass parameters into this method.
        /// </summary>
        public void Clear_RosterPlan()
        {
            #region Variable Declarations
            DXGrid uIGcRosterPlanTable = this.UIArbeidsplanKopiavGruWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            var confirmDeleteWindow = this.UIItemWindow3;
            confirmDeleteWindow.SearchProperties[DXTestControl.PropertyNames.Name] = "GT-4001 - Informasjon";
            DXButton uIJAButton = confirmDeleteWindow.UIJAButton;
            uIJAButton.SearchProperties[DXTestControl.PropertyNames.Name] = "btYes";
            #endregion           

            // Type 'Control + a' in 'gcRosterPlan' table
            Select_RosterCell();
            Playback.Wait(1000);

            try
            {
                UICommon.UIMapVS2017.ClickDeleteInRosterplanRightClickMenu();
                Mouse.Click(uIJAButton);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{Delete}");
                Mouse.Click(uIJAButton);
            }
        }

        public void InsertN2_for_NN()
        {
            Keyboard.SendKeys("N2");
            Keyboard.SendKeys("{TAB 5}", ModifierKeys.None);
            Keyboard.SendKeys("N2");
            Keyboard.SendKeys("{TAB}", ModifierKeys.None);
            Keyboard.SendKeys("N2");
            Keyboard.SendKeys("{TAB}", ModifierKeys.Shift);
        }


        /// <summary>
        /// InsertDK6 - Use 'InsertDK6Params' to pass parameters into this method.
        /// </summary>
        public void InsertDK6()
        {
            // Click cell
            //Mouse.Click(uIItemCell1, new Point(27, 6));
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringDK6);
            Keyboard.SendKeys(this.InsertDK6Params.UIRow0ColumnRosterTab7x, ModifierKeys.None);

            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringDK6);
            Keyboard.SendKeys(this.InsertDK6Params.UIRow0ColumnRosterTab7x, ModifierKeys.None);

            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringDK6);
            Keyboard.SendKeys(this.InsertDK6Params.UIRow0ColumnRosterTab7x, ModifierKeys.None);

            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringDK6);
            //Keyboard.SendKeys(this.Insert_DK6_Emp16Params.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);
        }

        public void InsertDK7()
        {
            // Click cell
            //Mouse.Click(uIItemCell1, new Point(27, 6));
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringDK7);
            Keyboard.SendKeys(this.InsertDK6Params.UIRow0ColumnRosterTab7x, ModifierKeys.Shift);

            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringDK7);
            Keyboard.SendKeys(this.InsertDK6Params.UIRow0ColumnRosterTab7x, ModifierKeys.Shift);

            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringDK7);
            Keyboard.SendKeys(this.InsertDK6Params.UIRow0ColumnRosterTab7x, ModifierKeys.Shift);

            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringDK7);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
        }

        public void InsertNSunEmp16()
        {
            // Click cell
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringN);
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);
        }

        public void InsertNSatEmp17()
        {
            // Click cell
            //Mouse.Click(uIItemCell1, new Point(27, 6));
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.Shift);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringN);
        }
        public void InsertN1SunEmp18()
        {
            // Click cell
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);
            Keyboard.SendKeys("N1");

        }

        public void InsertNMonEmp20()
        {
            // Click cell
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringN);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
        }

        public void InsertHJNFriEmp16()
        {
            // Click cell            
            Keyboard.SendKeys("{F2}", ModifierKeys.None);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringHJN);
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);
        }
        public void InsertHJN1FriEmp17()
        {

            // Click cell            
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringHJN1);
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);
        }
        public void InsertVPVFriEmp18()
        {
            // Click cell            
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringVPV);
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);
        }
        public void InsertVPV2FriEmp20()
        {
            // Click cell
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringVPV2);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
        }

        public void InsertHJNSatEmp16()
        {
            // Click cell        
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
            Keyboard.SendKeys("{F2}", ModifierKeys.None);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringHJN);
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);
        }

        public void InsertHJNSunEmp16()
        {
            // Click cell        
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
            Keyboard.SendKeys("{F2}", ModifierKeys.None);
            Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringHJN);
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);
        }

        public void InsertDWed_Week1()
        {
            // Click cell
            Keyboard.SendKeys("{F2}", ModifierKeys.None);
            for (int i = 0; i < 6; i++)
            {
                Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringD);
                if (i < 5)
                    Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
            }
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);

            for (int i = 0; i < 6; i++)
            {
                Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringD);
                Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.Shift);
            }
        }

        public void InsertDWed_Week2()
        {
            // Click cell
            Keyboard.SendKeys("{F2}", ModifierKeys.None);
            for (int i = 0; i < 6; i++)
            {
                Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringD);
                if (i < 5)
                    Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.None);
            }
            Keyboard.SendKeys("{DOWN}", ModifierKeys.None);

            for (int i = 0; i < 6; i++)
            {
                Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRosterCellEditValueAsStringD);
                Keyboard.SendKeys(this.Insert_ShiftCode_EmployeeParams.UIRow0ColumnRosterCellEditTab, ModifierKeys.Shift);
            }
        }


        /// <summary>
        /// Set_EmpPercentTo_0 - Use 'Set_EmpPercentTo_0Params' to pass parameters into this method.
        /// </summary>
        public void Set_EmpPercent(string percent)
        {
            #region Variable Declarations
            DXTextEdit uISePositionPercentEdit = this.UIStillingsforholdWindow.UIPcContentClient.UISePositionPercentEdit;
            #endregion

            //ValueAsString
            uISePositionPercentEdit.ValueAsString = percent;
        }

        public void EffectuatePlan(/*bool checkPlan = true*/)
        {
            Playback.Wait(2000);
            UICommon.EffectuateFullRosterplan(true);
        }

        public void DeleteEffectuationForAllEmps()
        {
            UICommon.DeleteEffectuationRosterplan();

            if (UICommon.SelectAllAndWaitForDeleteEffectuationWindowReady())
            {
                UICommon.DeleteEffectuatedLines();
                UICommon.CloseDeleteEffectuationOkWindow();
            }
        }

        public void DeleteEffectuation()
        {
            UICommon.DeleteEffectuationRosterplan();
        }

        public List<string> EffectuateLines()
        {

            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            UICommon.EffectuateRosterplanLines(false);

            var timeBeforeEffectuation = DateTime.Now;

            if (UICommon.SalaryCalculationsWindowExists())
            {
                var timeAfterEffectuation = DateTime.Now;
                errorList.AddRange(TimeLapseInSeconds(timeBeforeEffectuation, timeAfterEffectuation, "Tidsforbruk ved iverksetting av plan ", 2, 0));
            }

            UICommon.CloseSalaryCalculationsWindow();

            return errorList;
        }

        public Point Get_OkLineSettingsPoint()
        {
            #region Variable Declarations
            DXButton uIGSSimpleButtonButton = this.UILinjeinnstillinger16Window.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            var rec = uIGSSimpleButtonButton.BoundingRectangle;
            return new Point(rec.X, rec.Y);
        }


        /// <summary>
        /// Click_LineSettings
        /// </summary>
        public void Click_LineSettings(string keyString)
        {
            Playback.Wait(500);
            Keyboard.SendKeys(keyString);
            Keyboard.SendKeys("{Enter}");
        }

        /// <summary>
        /// Select_Emp16HelgLines
        /// </summary>
        public void RightClickEmp16HelgLines()
        {
            UIMapVS2017.RightClickEmp16HelgLines();
        }
        public void RightClickEmp18HelgLines()
        {
            UIMapVS2017.RightClickEmp18HelgLines();
        }
        /// <summary>
        /// Click_OkLineSettings
        /// </summary>
        public void Click_OkLineSettings(Point button)
        {
            #region Variable Declarations
            DXButton uIGSSimpleButtonButton = this.UILinjeinnstillinger16Window.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            Playback.Wait(1000);
            try
            {
                uIGSSimpleButtonButton.DrawHighlight();
                Mouse.Click(uIGSSimpleButtonButton);
            }
            catch (Exception)
            {
                Mouse.Click(button);
            }

        }

        /// <summary>
        /// ExcangeEmp14
        /// </summary>
        public void ExchangeEmp(string col, string empName, bool openClean = false, bool departmentExchange = false)
        {

            var ExchangeButton = new DXRibbonButtonItem(); ;

            if (departmentExchange)
                ExchangeButton = this.UIGatver63031732ASCLAvWindow7.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIByttemedavdelingRibbonBaseButtonItem;
            else
                ExchangeButton = this.UIGatver63031732ASCLAvWindow7.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIBytteRibbonBaseButtonItem;

            if (!openClean)
            {
                Select_RosterbookEmployee(col, empName);
            }

            Mouse.Click(ExchangeButton);
        }

        /// <summary>
        /// Select_Emp16Exchange - Use 'Select_Emp16ExchangeParams' to pass parameters into this method.
        /// </summary>
        public void Select_EmpExchangeTo(string keyString)
        {
            #region Variable Declarations
            DXLookUpEdit uISleEmployment2LookUpEdit = this.UIBytteWindow.UIGsPanelControl1Client.UIGrpEmployee2Client.UISleEmployment2LookUpEdit;
            #endregion

            Mouse.Click(uISleEmployment2LookUpEdit);

            Keyboard.SendKeys(keyString);
            //Keyboard.SendKeys("Etternavn:16");
            Keyboard.SendKeys("{Enter}");
        }


        /// <summary>
        /// Select_VacantShiftsToExchange
        /// </summary>
        public void Select_VacantShiftsToExchange()
        {
            UIMapVS2017.Select_VacantShiftsToExchange();
        }

        /// <summary>
        /// Click_Ok_Exchange
        /// </summary>
        public void Click_Ok_Exchange()
        {
            #region Variable Declarations
            DXButton uIOKButton = this.UIBytteWindow.UIOKButton;
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBiOkMenuBaseButtonItem;
            #endregion

            Playback.Wait(2000);
            try
            {
                if (uIOKButton.Enabled)
                    Mouse.Click(uIOKButton);
                else
                    throw new Exception("Ok exchangebutton is disabled");

                if (uIBiOkMenuBaseButtonItem.Exists)
                    Mouse.Click(uIBiOkMenuBaseButtonItem);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error clicking OK-button(Exchange window): " + ex.Message);
            }
        }

        /// <summary>
        /// Click_DepartmentExchangeEmp - Use 'Click_DepartmentExchangeEmpParams' to pass parameters into this method.
        /// </summary>
        public void Select_DepartmentExchangeEmp(string keyString)
        {
            #region Variable Declarations
            DXLookUpEdit uISleEmployment1LookUpEdit = this.UIByttemedavdelingWindow.UIGsPanelControl1Client.UIGrpEmployee1Client.UISleEmployment1LookUpEdit;
            #endregion

            Mouse.Click(uISleEmployment1LookUpEdit);
            Keyboard.SendKeys(keyString);
            //Keyboard.SendKeys("Etternavn:16");
            Keyboard.SendKeys("{Enter}");
        }

        /// <summary>
        /// Click_Ok_DepartmentExchange
        /// </summary>
        public void Click_Ok_DepartmentExchange()
        {
            #region Variable Declarations
            DXButton uIOKButton = this.UIByttemedavdelingWindow.UIOKButton;
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBiOkMenuBaseButtonItem;
            #endregion

            Playback.Wait(2000);
            // Click '&OK' button
            Mouse.Click(uIOKButton);
            try
            {
                if (uIBiOkMenuBaseButtonItem.Exists)
                    Mouse.Click(uIBiOkMenuBaseButtonItem, new Point(35, 10));
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error clicking OK-button(Departmentexchange window): " + ex.Message);
            }


        }



        /// <summary>
        /// Click_Remanage
        /// </summary>
        public void Click_Remanage()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIForskjøvetvaktRibbonBaseButtonItem = this.UIGatver63031732ASCLAvWindow7.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIForskjøvetvaktRibbonBaseButtonItem;
            #endregion

            // Click 'Forskjøvet
            //vakt' RibbonBaseButtonItem
            Mouse.Click(uIForskjøvetvaktRibbonBaseButtonItem);
        }

        /// <summary>
        /// Select_RemanageCause - Use 'Select_RemanageCauseParams' to pass parameters into this method.
        /// </summary>
        public void Select_RemanageCause()
        {
            #region Variable Declarations
            DXLookUpEdit uILeOvertimeCodeLookUpEdit = this.UIForskyvningWindow.UIPanClientPanelClient.UIBnMenuNavBar.UILeOvertimeCodeLookUpEdit;
            #endregion

            //ValueAsString
            Mouse.Click(uILeOvertimeCodeLookUpEdit);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("{ENTER}");
        }


        /// <summary>
        /// Select_NightShift
        /// </summary>
        public void Select_RemanageEmp(string col, string empName)
        {
            #region Variable Declarations
            //var table = new DXGrid();
            DXRibbonButtonItem uIForskjøvetvaktRibbonBaseButtonItem = this.UIGatver63031732ASCLAvWindow7.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIForskjøvetvaktRibbonBaseButtonItem;

            //if (col == "D")
            //    table = this.UIGatver63031732ASCLAvWindow7.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIDag00167770770FalseDockPanel.UIControlContainerCustom.UIGcDayColumnTable;
            //else if (col == "N")
            //    table = this.UIGatver63031732ASCLAvWindow7.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UINatttiltirsdag001677DockPanel.UIControlContainerCustom.UIGcDayColumnTable;

            #endregion

            Select_RosterbookEmployee(col, empName);
            Mouse.Click(uIForskjøvetvaktRibbonBaseButtonItem);
        }

        public DXCell Select_RosterbookEmployee(string col, string empName, string absPeriod = "", string statusCode = "")
        {
            #region Variable Declarations

            /*Absence:
                gccFullName
                gccAbsencePeriod
                gccAbsenceCode
             */

            string nameColumn = "colEmployeeName";
            var table = new DXGrid();
            DXRibbonButtonItem uIForskjøvetvaktRibbonBaseButtonItem = this.UIGatver63031732ASCLAvWindow7.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpDevianceRibbonPageGroup.UIForskjøvetvaktRibbonBaseButtonItem;

            if (col == "D")
                table = this.UIGatver63031732ASCLAvWindow7.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIDag00167770770FalseDockPanel.UIControlContainerCustom.UIGcDayColumnTable;
            else if (col == "A")
                table = this.UIGatver63031732ASCLAvWindow7.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIAften00167770770FalsDockPanel.UIControlContainerCustom.UIGcDayColumnTable;
            else if (col == "N")
                table = this.UIGatver63031732ASCLAvWindow7.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UINatttiltirsdag001677DockPanel.UIControlContainerCustom.UIGcDayColumnTable;
            else if (col == "V")
                table = this.UIGatver63031732ASCLAvWindow7.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIFriVikar00167770770FDockPanel.UIControlContainerCustom.UIGcFreeColumnTable;
            else if (col == "Abs")
            {
                table = this.UIGatver63031732ASCLAvWindow7.UIBottomPanelDockPanel.UIFraværDockPanel.UIControlContainerCustom.UIGcAbsenceColumnTable;
                nameColumn = "gccFullName";
            }
            Playback.Wait(1000);
            #endregion

            var view = table.Views[0];
            DXCell selectCell = view.GetCell(nameColumn, 0);

            string statusCodeGrid = "";
            for (int i = 0; i < view.RowCount; i++)
            {
                //var shiftCode = view.GetCellValue("colShiftCode", i);
                var empNameGrid = view.GetCellValue(nameColumn, i);
                if (empNameGrid == null)
                    continue;

                if (absPeriod != "")
                {
                    var viewAbsPeriod = view.GetCellValue("gccAbsencePeriod", i).ToString();

                    if (absPeriod != viewAbsPeriod)
                        continue;
                }
                else if (statusCode != "")
                {
                    var colStatusCode = view.GetCellValue("colStatusCode", i);
                    if (colStatusCode != null)
                        statusCodeGrid = colStatusCode.ToString();
                }

                if (statusCode == "" || (statusCodeGrid == statusCode))
                {
                    if (empNameGrid.ToString() == empName)
                    {
                        selectCell = view.GetCell(nameColumn, i);
                        break;
                    }
                }
            }

            Mouse.Click(selectCell);
            Playback.Wait(1000);
            return selectCell;
        }

        /// <summary>
        /// Construct_NewShiftForRemanage - Use 'Construct_NewShiftForRemanageParams' to pass parameters into this method.
        /// </summary>
        public void Construct_NewShiftForRemanage(DateTime shiftDate)
        {
            #region Variable Declarations
            DXLookUpEdit uICbShiftCodeLookUpEdit = this.UIForskyvningWindow.UIPanClientPanelClient.UIGpcMainClient.UITcClientTabList.UITpMainClient.UIGpcShiftsClient.UIGcNewShiftsClient.UITcNewShiftTabList.UITpSimpleShiftCtrlClient.UIMainPanelClient.UICbShiftCodeLookUpEdit;
            #endregion

            SetNewRemanningShiftdate(shiftDate);

            // Type '' in 'cbShiftCode' LookUpEdit
            Keyboard.SendKeys(uICbShiftCodeLookUpEdit, this.Construct_NewShiftForRemanageParams.UICbShiftCodeLookUpEditValueAsString);
        }

        /// <summary>
        /// SetNewRemanningShiftdate - Use 'SetNewRemanningShiftdateParams' to pass parameters into this method.
        /// </summary>
        private void SetNewRemanningShiftdate(DateTime shiftDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIForskyvningWindow.UIPanClientPanelClient1.UIGpcMainClient.UITcClientTabList.UITpMainClient.UIGpcShiftsClient.UIGcNewShiftsClient.UITcNewShiftTabList.UITpSimpleShiftCtrlClient.UIMainPanelClient.UIDeDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.DateTime = shiftDate;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetNewRemanningShiftdateParams.Tab);
        }

        /// <summary>
        /// Click_Ok_Remanage
        /// </summary>
        public void Click_Ok_Remanage()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIBiOkMenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBiOkMenuBaseButtonItem;
            DXButton uIGSDropDownButtonButton = this.UIForskyvningWindow.UIGSPanelControlClient.UIGSDropDownButtonButton;
            #endregion

            // Click 'GSDropDownButton' button
            Playback.Wait(2000);
            Mouse.Click(uIGSDropDownButtonButton);
            try
            {
                if (uIBiOkMenuBaseButtonItem.Exists)
                    Mouse.Click(uIBiOkMenuBaseButtonItem, new Point(35, 10));
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error clicking OK-button(Remanage window): " + ex.Message);
            }
        }

        /// <summary>
        /// Select_100715_Overtime_KontLine
        /// </summary>
        public void Select_110715_Overtime_KontLine()
        {
            #region Variable Declarations
            WinClient uITimelisteClient = this.UIGatver63031732ASCLAvWindow16.UIItemWindow.UITimelisteClient;
            #endregion

            // Click 'Timeliste' client
            Mouse.Click(uITimelisteClient, new Point(35, 174));
        }

        public void EditKontForPreSelectedEmployee()
        {
            UIMapVS2015.EditKontForPreSelectedEmployee();
        }

        public void SelectEmp10AndEditKontNew()
        {
            UIMapVS2015.SelectEmp10AndEditKont();
        }

        /// <summary>
        /// AddNewKont - Use 'AddNewKontParams' to pass parameters into this method.
        /// </summary>
        public void AddNewKont()
        {
            #region Variable Declarations
            WinComboBox uIItemComboBox = this.UINykonteringWindow.UIItemWindow.UIItemComboBox;
            WinEdit uIItemEdit = this.UINykonteringWindow.UIItemWindow1.UIItemEdit;
            WinButton uIOKButton = this.UINykonteringWindow.UIPanel2Client.UIOKButton;
            #endregion

            // Select '400 - Lør./Søndagstillegg' in combo box
            uIItemComboBox.SelectedItem = this.AddNewKontParams.Code400_SatSun;

            // Type '3' in text box
            //uIItemEdit.Text = this.AddNewKontParams.UIItemEditText;
            Keyboard.SendKeys(uIItemEdit, this.AddNewKontParams.UIItemEditText);

            // Click '&OK' button
            Mouse.Click(uIOKButton, new Point(45, 28));
        }

        /// <summary>
        /// DeleteTT_400_Reg - Use 'DeleteTT_400_RegParams' to pass parameters into this method.
        /// </summary>
        public void DeleteTT_400_Reg()
        {
            #region Variable Declarations
            WinClient uIEndrekonteringClient = this.UIEndrekonteringWindow.UIItemWindow2.UIEndrekonteringClient;
            WinClient uIEndrekonteringClient1 = this.UIEndrekonteringWindow.UIItemWindow.UIEndrekonteringClient;
            WinButton uIYesButton = this.UIGatWindow.UIGatClient.UIYesButton;
            WinButton uILukkButton = this.UIEndrekonteringWindow.UIItemWindow1.UIEndrekonteringClient.UILukkButton;
            #endregion

            // Click 'Endre kontering' client
            Mouse.Click(uIEndrekonteringClient, new Point(360, 72));

            // Type '{Down}' in 'Endre kontering' client
            Keyboard.SendKeys(uIEndrekonteringClient, this.DeleteTT_400_RegParams.UIEndrekonteringClientSendKeys, ModifierKeys.None);

            // Click 'Endre kontering' client
            Mouse.Click(uIEndrekonteringClient1, new Point(279, 21));

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(32, 8));

            // Click 'L&ukk' button
            Mouse.Click(uILukkButton, new Point(31, 21));
        }

        /// <summary>
        /// DeleteKontLine_400
        /// </summary>
        public void DeleteKontLine_400()
        {
            /*
            #region Variable Declarations
            WinClient uIEndrekonteringClient = this.UIEndrekonteringWindow.UIItemWindow2.UIEndrekonteringClient;
            WinClient uIEndrekonteringClient1 = this.UIEndrekonteringWindow.UIItemWindow.UIEndrekonteringClient;
            WinButton uIYesButton = this.UIGatWindow.UIGatClient.UIYesButton;
            WinButton uILukkButton = this.UIEndrekonteringWindow.UIItemWindow1.UIEndrekonteringClient.UILukkButton;
            #endregion

            // Click 'Endre kontering' client
            Mouse.Click(uIEndrekonteringClient, new Point(360, 72));

            // Type '{Down}' in 'Endre kontering' client
            Keyboard.SendKeys(uIEndrekonteringClient, this.DeleteTT_400_RegParams.UIEndrekonteringClientSendKeys, ModifierKeys.None);

            // Click 'Endre kontering' client
            Mouse.Click(uIEndrekonteringClient1, new Point(279, 21));

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(32, 8));

            // Click 'L&ukk' button
            Mouse.Click(uILukkButton, new Point(31, 21));
             */

            #region Variable Declarations
            WinClient uIEndrekonteringClient = this.UIEndrekonteringWindow.UIItemWindow2.UIEndrekonteringClient;
            WinClient uIEndrekonteringClient1 = this.UIEndrekonteringWindow.UIItemWindow.UIEndrekonteringClient;
            WinButton uIYesButton = this.UIGatWindow.UIGatClient.UIYesButton;
            #endregion

            // Click 'Endre kontering' client
            Mouse.Click(uIEndrekonteringClient, new Point(180, 36));

            // Click 'Endre kontering' client
            Mouse.Click(uIEndrekonteringClient1, new Point(278, 21));

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(41, 9));
        }

        /// <summary>
        /// CheckOk - Use 'CheckOkExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckOk()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIOKRibbonBaseButtonItem = this.UIArbeidsplanKopiavGruWindow1.UIRcMenuRibbon.UIRpcLineToolsRibbonPageCategory.UIRpLineRibbonPage.UIRibbonPageGroup24RibbonPageGroup.UIOKRibbonBaseButtonItem;
            #endregion

            // Verify that the 'ClassName' property of 'OK' RibbonBaseButtonItem equals 'RibbonBaseButtonItem'
            Assert.AreEqual(this.CheckOkExpectedValues.UIOKRibbonBaseButtonItemClassName, uIOKRibbonBaseButtonItem.ClassName);
        }

        public virtual CheckOkExpectedValues CheckOkExpectedValues
        {
            get
            {
                if ((this.mCheckOkExpectedValues == null))
                {
                    this.mCheckOkExpectedValues = new CheckOkExpectedValues();
                }
                return this.mCheckOkExpectedValues;
            }
        }

        private CheckOkExpectedValues mCheckOkExpectedValues;

        /// <summary>
        /// SetPlanStartdateAndWeeks - Use 'SetPlanStartdateAndWeeksParams' to pass parameters into this method.
        /// </summary>
        public void SetPlanStartdateAndWeeks()
        {
            #region Variable Declarations
            DXTextEdit uIENumber1Edit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIENumber1Edit;
            #endregion

            SetPlanStartDate(this.SetPlanStartDateParams.UIPceDate2PopupEditValueAsDateTime);

            // Type '15 [SelectionStart]0[SelectionLength]2' in 'eNumber[1]' text box
            //ValueAsString
            uIENumber1Edit.ValueAsString = this.SetPlanStartdateAndWeeksParams.UIENumber1EditValueAsString;
            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIENumber1Edit, this.SetPeriodParams.UIItemEditSendTabKey, ModifierKeys.None);
        }

        /// <summary>
        /// SetPlanStartDate - Use 'SetPlanStartDateParams' to pass parameters into this method.
        /// </summary>
        private void SetPlanStartDate(DateTime startDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList1.UITpConfigurationClient.UIPaCenterClient.UIEStartDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.DateTime = startDate;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetPeriodParams.UIItemEditSendTabKey);
        }

        /// <summary>
        /// Select_RosterCell
        /// </summary>
        public void Select_RosterCell()
        {
            try
            {
                //UICommon.UIMapVS2017.SelectWholeRosterplanFromEmpCol();
                UICommon.UIMapVS2017.SelectWholeRosterplanCTRLA();
            }
            catch
            {
                UICommon.UIMapVS2017.SelectWholeRosterplan();
            }
        }

        /// <summary>
        /// Select_All_EmployeesAndAddToPlan - Use 'Select_All_EmployeesAndAddToPlanParams' to pass parameters into this method.
        /// </summary>
        public void Select_All_EmployeesAndAddToPlan()
        {
            #region Variable Declarations
            //DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanChapter_5Window.UIRcMenuRibbon1.UIRpPlanRibbonPage;
            //DXRibbonButtonItem uIAnsatteRibbonBaseButtonItem = this.UIArbeidsplanChapter_5Window.UIRcMenuRibbon1.UIRpPlanRibbonPage.UIRpgPlanRibbonPageGroup.UIAnsatteRibbonBaseButtonItem;
            DXButton uIAnsatteButton = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UIAnsatteButton;
            DXCell uIItem10HelgCell = this.UILeggtilansatteWindow.UIViewHostCustom.UIPcViewClient.UISelectDepartmentEmplCustom.UIPcContentContainerClient.UIPcContentClient.UIGcDepartmentEmployeeTable.UIItem10HelgCell;
            DXGrid uIGcDepartmentEmployeeTable = this.UILeggtilansatteWindow.UIViewHostCustom.UIPcViewClient.UISelectDepartmentEmplCustom.UIPcContentContainerClient.UIPcContentClient.UIGcDepartmentEmployeeTable;
            DXButton uISimpleButtonButton = this.UILeggtilansatteWindow.UISimpleButtonButton;
            DXTreeListCell uIItem1100TreeListCell = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode0TreeListNode.UINode0TreeListNode1.UIItem1100TreeListCell;
            DXLookUpEdit uISearchLookUpEditLookUpEdit = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient1.UIEmployeeManagerOvertCustom.UISearchLookUpEditLookUpEdit;
            DXTreeListCell uIItem7001HelgeavtaleaTreeListCell = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode1TreeListNode.UINode0TreeListNode.UIItem7001HelgeavtaleaTreeListCell;
            DXCell uIFCell = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient1.UIEmployeeManagerOvertCustom.UISearchLookUpEditLookUpEdit.UIPopupSearchLookUpEdiWindow.UISearchEditLookUpPopuCustom.UILCCustom.UILcMainLayoutGroup.UILciGridLayoutControlItem.UIGridControlTable.UIFCell;
            DXTreeListCell uIItem125TreeListCell = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode2TreeListNode.UINode0TreeListNode.UIItem125TreeListCell;
            DXTreeListCell uIItem1100TreeListCell1 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode3TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXTreeListCell uIItem1100TreeListCell2 = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode4TreeListNode.UINode0TreeListNode.UIItem1100TreeListCell;
            DXButton uIOKButton = this.UIAnsatteiArbeidsplanWindow.UIOKButton;
            #endregion

            // Click 'rpPlan' RibbonPage
            UICommon.ClickRosterplanPlanTab();

            // Click 'Ansatte' RibbonBaseButtonItem
            UICommon.ClickEmployeesButtonRosterplan();
            //Mouse.Click(uIAnsatteRibbonBaseButtonItem); 

            // Click 'Ansatte...' button
            Mouse.Click(uIAnsatteButton);

            // Click '10, Helg' cell
            Mouse.Click(uIItem10HelgCell);

            // Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
            Keyboard.SendKeys(uIGcDepartmentEmployeeTable, this.Select_All_EmployeesAndAddToPlanParams.UIGcDepartmentEmployeeTableSendKeys, ModifierKeys.Shift);

            // Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
            Keyboard.SendKeys(uIGcDepartmentEmployeeTable, this.Select_All_EmployeesAndAddToPlanParams.UIGcDepartmentEmployeeTableSendKeys1, ModifierKeys.Shift);

            // Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
            Keyboard.SendKeys(uIGcDepartmentEmployeeTable, this.Select_All_EmployeesAndAddToPlanParams.UIGcDepartmentEmployeeTableSendKeys2, ModifierKeys.Shift);

            // Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
            Keyboard.SendKeys(uIGcDepartmentEmployeeTable, this.Select_All_EmployeesAndAddToPlanParams.UIGcDepartmentEmployeeTableSendKeys3, ModifierKeys.Shift);

            // Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
            Keyboard.SendKeys(uIGcDepartmentEmployeeTable, this.Select_All_EmployeesAndAddToPlanParams.UIGcDepartmentEmployeeTableSendKeys4, ModifierKeys.Shift);

            // Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
            Keyboard.SendKeys(uIGcDepartmentEmployeeTable, this.Select_All_EmployeesAndAddToPlanParams.UIGcDepartmentEmployeeTableSendKeys5, ModifierKeys.Shift);

            // Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
            Keyboard.SendKeys(uIGcDepartmentEmployeeTable, this.Select_All_EmployeesAndAddToPlanParams.UIGcDepartmentEmployeeTableSendKeys6, ModifierKeys.Shift);

            // Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
            Keyboard.SendKeys(uIGcDepartmentEmployeeTable, this.Select_All_EmployeesAndAddToPlanParams.UIGcDepartmentEmployeeTableSendKeys7, ModifierKeys.Shift);

            // Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
            Keyboard.SendKeys(uIGcDepartmentEmployeeTable, this.Select_All_EmployeesAndAddToPlanParams.UIGcDepartmentEmployeeTableSendKeys8, ModifierKeys.Shift);

            // Click 'SimpleButton' button
            Mouse.Click(uISimpleButtonButton);

            // Click '1. 100%' TreeListCell
            Mouse.Click(uIItem1100TreeListCell, new Point(37, 7));

            // Type 'Gatsoft.Gat.RosterPlan.EmployeeManager.UI.ViewModels.Data.OvertimeCodeViewModel' in 'SearchLookUpEdit' LookUpEdit
            //ValueTypeName
            uISearchLookUpEditLookUpEdit.ValueTypeName = this.Select_All_EmployeesAndAddToPlanParams.UISearchLookUpEditLookUpEditValueTypeName;

            // Type 'F - Ferievikar' in 'SearchLookUpEdit' LookUpEdit
            //ValueAsString
            uISearchLookUpEditLookUpEdit.ValueAsString = this.Select_All_EmployeesAndAddToPlanParams.UISearchLookUpEditLookUpEditValueAsString;

            // Click '7001 - Helgeavtaleavdelingen' TreeListCell
            Mouse.Click(uIItem7001HelgeavtaleaTreeListCell, new Point(113, 11));
            //ValueTypeName
            uISearchLookUpEditLookUpEdit.ValueTypeName = this.Select_All_EmployeesAndAddToPlanParams.UISearchLookUpEditLookUpEditValueTypeName;
            // Type 'F - Ferievikar' in 'SearchLookUpEdit' LookUpEdit
            //ValueAsString
            uISearchLookUpEditLookUpEdit.ValueAsString = this.Select_All_EmployeesAndAddToPlanParams.UISearchLookUpEditLookUpEditValueAsString1;

            // Click 'F' cell
            //Mouse.Click(uIFCell, new Point(129, 14));

            // Click '1. 25%' TreeListCell
            Mouse.Click(uIItem125TreeListCell, new Point(30, 9));
            //ValueTypeName
            uISearchLookUpEditLookUpEdit.ValueTypeName = this.Select_All_EmployeesAndAddToPlanParams.UISearchLookUpEditLookUpEditValueTypeName;
            // Type 'F - Ferievikar' in 'SearchLookUpEdit' LookUpEdit
            //ValueAsString
            uISearchLookUpEditLookUpEdit.ValueAsString = this.Select_All_EmployeesAndAddToPlanParams.UISearchLookUpEditLookUpEditValueAsString2;

            // Click 'F' cell
            //Mouse.Click(uIFCell, new Point(215, 7));

            // Click '1. 100%' TreeListCell
            Mouse.Click(uIItem1100TreeListCell1, new Point(32, 9));
            //ValueTypeName
            uISearchLookUpEditLookUpEdit.ValueTypeName = this.Select_All_EmployeesAndAddToPlanParams.UISearchLookUpEditLookUpEditValueTypeName;
            // Type 'F - Ferievikar' in 'SearchLookUpEdit' LookUpEdit
            //ValueAsString
            uISearchLookUpEditLookUpEdit.ValueAsString = this.Select_All_EmployeesAndAddToPlanParams.UISearchLookUpEditLookUpEditValueAsString3;

            // Click 'F' cell
            //Mouse.Click(uIFCell, new Point(60, 8));

            // Click '1. 100%' TreeListCell
            Mouse.Click(uIItem1100TreeListCell2, new Point(21, 8));
            //ValueTypeName
            uISearchLookUpEditLookUpEdit.ValueTypeName = this.Select_All_EmployeesAndAddToPlanParams.UISearchLookUpEditLookUpEditValueTypeName;
            // Type 'F - Ferievikar' in 'SearchLookUpEdit' LookUpEdit
            //ValueAsString
            uISearchLookUpEditLookUpEdit.ValueAsString = this.Select_All_EmployeesAndAddToPlanParams.UISearchLookUpEditLookUpEditValueAsString4;

            // Click 'F' cell
            //Mouse.Click(uIFCell, new Point(74, 6));

            // Click 'OK' button
            Mouse.Click(uIOKButton, new Point(1, 1));
        }

        /// <summary>
        /// Select_All_Employees
        /// </summary>
        public void Select_All_Employees()
        {
            #region Variable Declarations
            DXButton uIVelgalleButton = this.UILeggtilansatteWindow.UIPcContentClient.UIVelgalleButton;
            #endregion

            try
            {
                Playback.Wait(2000);
                Mouse.Click(uIVelgalleButton);
            }
            catch (Exception)
            {
                try
                {
                    Playback.Wait(2000);
                    var rec = uIVelgalleButton.BoundingRectangle;
                    Point point = new Point(rec.X, rec.Y);
                    Mouse.Click(point);
                }
                catch (Exception)
                {
                    Keyboard.SendKeys("{ENTER}", ModifierKeys.None);
                }
            }
        }

        /// <summary>
        /// Click_Ok_Employees
        /// </summary>
        public void Click_Ok_Employees()
        {
            #region Variable Declarations
            DXButton uIGSSimpleButtonButton = this.UILeggtilansatteWindow.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            #endregion

            try
            {
                Playback.Wait(2000);
                var rec = uIGSSimpleButtonButton.BoundingRectangle;
                Point point = new Point(rec.X, rec.Y);
                Mouse.Click(point);
            }
            catch (Exception)
            {
                try
                {
                    Playback.Wait(2000);
                    uIGSSimpleButtonButton.WaitForControlReady();
                    Mouse.Click(uIGSSimpleButtonButton);
                }
                catch (Exception)
                {
                    Keyboard.SendKeys("{ENTER}", ModifierKeys.None);
                }
            }
        }

        /// <summary>
        /// Click_HomeTab_CalendarPlan
        /// </summary>
        public void Click_HomeTab_CalendarPlan()
        {
            UICommon.ClickRosterplanHomeTab();
        }

        public void Click_Ok_DeleteShiftsInCalendarplan()
        {
            #region Variable Declarations
            DXButton uIJAButton = this.UIItemWindow3.UIJAButton;
            #endregion

            // Click '&Ja' button
            Mouse.Click(uIJAButton);
        }

        /// <summary>
        /// Select_CauseCodeForFiveEmployees - Use 'Select_CauseCodeForFiveEmployeesParams' to pass parameters into this method.
        /// </summary>
        public void Select_CauseCodeForFiveEmployees()
        {
            #region Variable Declarations
            DXLookUpEdit uIGsleToSporadicLookUpEdit = this.UIAnsatteiArbeidsplanWindow.UIGSPanelControlClient.UIPnlDropDownsClient.UIGsleToSporadicLookUpEdit;
            DXLookUpEdit uIGsleToSporadicLookUpEdit1 = this.UIAnsatteiArbeidsplanWindow.UIGSPanelControlClient.UIPnlDropDownsClient1.UIGsleToSporadicLookUpEdit;
            #endregion
            // Type 'Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition' in 'gsleToSporadic' LookUpEdit
            //ValueAsString
            Mouse.Click(uIGsleToSporadicLookUpEdit1);
            Keyboard.SendKeys(uIGsleToSporadicLookUpEdit1, this.Select_CauseCodeForFiveEmployeesParams.UIGSykevikar);
            Keyboard.SendKeys(uIGsleToSporadicLookUpEdit1, this.SetPeriodParams.UIItemEditSendTabKey, ModifierKeys.None);
        }

        /// <summary>
        /// SelectFirstCell_CalendarPlan
        /// </summary>
        public void SelectFirstCell_CalendarPlan()
        {
            UIMapVS2017.SelectFirstCell_CalendarPlan();
        }

        /// <summary>
        /// SelectLastCell_CalendarPlan
        /// </summary>
        public void SelectLastCell_CalendarPlan(string sendKeys, bool selectFirstCell = true)
        {
            if (selectFirstCell)
                UICommon.UIMapVS2017.SelectFirstCellInPlan();

            Playback.Wait(1000);
            Keyboard.SendKeys("{END}");
            Keyboard.SendKeys(sendKeys, ModifierKeys.Shift);
        }

        public void SetEasterShifts_CalendarPlan()
        {
            Keyboard.SendKeys("{ENTER}", ModifierKeys.None);
            Keyboard.SendKeys("{TAB}", ModifierKeys.Shift);
            Keyboard.SendKeys("A");
            Keyboard.SendKeys("{TAB}", ModifierKeys.Shift);
            Keyboard.SendKeys("D");
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("A");
            Keyboard.SendKeys("{TAB}", ModifierKeys.None);
            Keyboard.SendKeys("D");
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("A");
            Keyboard.SendKeys("{TAB}", ModifierKeys.Shift);
            Keyboard.SendKeys("D");
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("A");
            Keyboard.SendKeys("{TAB}", ModifierKeys.None);
            Keyboard.SendKeys("D");
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("A");
            Keyboard.SendKeys("{TAB}", ModifierKeys.Shift);
            Keyboard.SendKeys("D");
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("A");
            Keyboard.SendKeys("{TAB}", ModifierKeys.None);
            Keyboard.SendKeys("D");
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("A");
            Keyboard.SendKeys("{TAB}", ModifierKeys.Shift);
            Keyboard.SendKeys("D");
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("A");
            Keyboard.SendKeys("{TAB}", ModifierKeys.None);
            Keyboard.SendKeys("D");
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("A");
            Keyboard.SendKeys("{TAB}", ModifierKeys.Shift);
            Keyboard.SendKeys("D");
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys("A");
            Keyboard.SendKeys("{TAB}", ModifierKeys.None);
            Keyboard.SendKeys("D");
            Keyboard.SendKeys("{TAB}", ModifierKeys.None);
        }

        public void SetEasterShiftsDK_CalendarPlan(string shiftCode, string tabCount)
        {
            Keyboard.SendKeys("{ENTER}", ModifierKeys.None);
            Keyboard.SendKeys("{TAB}", ModifierKeys.Shift);
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{DOWN}");
            Keyboard.SendKeys(shiftCode);
            if (tabCount != "")
                Keyboard.SendKeys(tabCount, ModifierKeys.Shift);

            Keyboard.SendKeys("{ENTER}", ModifierKeys.None);
            Keyboard.SendKeys("{TAB}", ModifierKeys.Shift);
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{UP}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{UP}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{UP}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{UP}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{UP}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{UP}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{UP}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{UP}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{UP}");
            Keyboard.SendKeys(shiftCode);
            Keyboard.SendKeys("{UP}");
            Keyboard.SendKeys("{TAB}", ModifierKeys.Shift);
        }

        /// <summary>
        /// Effectuate_CalendarPlan - Use 'Effectuate_CalendarPlanParams' to pass parameters into this method.
        /// </summary>
        public void Effectuate_CalendarPlan(bool changePeriod = false)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXButton uIEndreperiodeforavkryButton = this.UIIverksetteWindow.UIPnlButtonsClient.UIEndreperiodeforavkryButton;
            DXButton uIGSSimpleButtonButton = this.UIEndreiverksettingspeWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            UICommon.EffectuateFullRosterplan(true);

            if (changePeriod)
            {
                // Click 'Endre periode for avkryssede linjer' button
                Mouse.Click(uIEndreperiodeforavkryButton);

                ChangeRosterIntervallNew("", this.Effectuate_CalendarPlanParams.UIPceDate1PopupEditValueAsString);

                // Click 'GSSimpleButton' button
                Mouse.Click(uIGSSimpleButtonButton);
            }

            errorList.AddRange(EffectuateLines());
        }

        /// <summary>
        /// RegisterNewEmployee - Use 'RegisterNewEmployeeParams' to pass parameters into this method.
        /// </summary>
        public void RegisterNewEmployee()
        {
            #region Variable Declarations
            WinEdit uIItemEdit = this.UIGatver63031732ASCLAvWindow18.UIItemWindow1.UIItemEdit;
            WinEdit uIItemEdit1 = this.UIGatver63031732ASCLAvWindow18.UIItemWindow2.UIItemEdit;
            WinEdit uIItemEdit2 = this.UIGatver63031732ASCLAvWindow18.UIItemWindow3.UIItemEdit;
            WinButton uIOKButton = this.UIGatver63031732ASCLAvWindow18.UIItemWindow.UIAnsattClient.UIOKButton;
            #endregion

            // Click 'Ansatt' client
            try
            {
                Playback.Wait(1000);
                UIMapVS2015.ClickNewEmployeeInEmpTab();
                //UICommon.ClickNewEmployeeFromEmployeeTab();
            }
            catch
            {
                UICommon.ShortCutNewEmployeeFromEmployeeTab();
            }

            // Type '123456789' in text box
            Playback.Wait(1000);
            uIItemEdit.Text = this.RegisterNewEmployeeParams.UIItemEditText;

            // Type 'N' in text box
            uIItemEdit1.Text = this.RegisterNewEmployeeParams.UIItemEditText1;

            // Type 'N' in text box
            uIItemEdit2.Text = this.RegisterNewEmployeeParams.UIItemEditText2;

            // Click '&OK' button
            Mouse.Click(uIOKButton, new Point(19, 14));
        }


        /// <summary>
        /// NewEmployeeDates - Use 'NewEmployeeDatesParams' to pass parameters into this method.
        /// </summary>
        private void NewEmployeeDates(string from, string to)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIStillingsforholdWindow.UIPcContentClient1.UISdeFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIStillingsforholdWindow.UIPcContentClient1.UISdeToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            if (from != "")
            {
                uIPceDateDateTimeEdit.DateTime = Convert.ToDateTime(from);
                Keyboard.SendKeys(uIPceDateDateTimeEdit, this.EditExtraShiftParams.UIETab);
            }

            if (to != "")
            {
                uIPceDateDateTimeEdit1.DateTime = Convert.ToDateTime(to);
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, this.EditExtraShiftParams.UIETab);
            }
        }

        /// <summary>
        /// RegisterNewEmployee_Position - Use 'RegisterNewEmployee_PositionParams' to pass parameters into this method.
        /// </summary>
        public void RegisterNewEmployee_Position()
        {
            #region Variable Declarations
            DXPopupEdit uIPceDate1PopupEdit = this.UIStillingsforholdWindow.UIPcContentClient.UIPceDate1PopupEdit;
            DXTextEdit uISePositionPercentEdit = this.UIStillingsforholdWindow.UIPcContentClient.UISePositionPercentEdit;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient.UIGSSearchLookUpEditLookUpEdit;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit1 = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient.UIGSSearchLookUpEditLookUpEdit1;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit2 = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient.UIGSSearchLookUpEditLookUpEdit2;
            DXTextEdit uITeInternalPositionNuEdit = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient.UITeInternalPositionNuEdit;
            DXLookUpEdit uIGSSearchLookUpEditLookUpEdit3 = this.UIStillingsforholdWindow.UIPcContentClient.UIGSPanelControlClient1.UIGSSearchLookUpEditLookUpEdit;
            DXButton uIGSSimpleButtonButton = this.UIStillingsforholdWindow.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            #endregion

            NewEmployeeDates(this.RegisterNewEmployee_PositionParams.UIPceDate1PopupEditValueAsString, ""); // this.RegisterNewEmployee_PositionParams.UIPceDate0PopupEditValueAsString

            // Type '90 [SelectionStart]0[SelectionLength]4' in 'sePositionPercent' text box
            //ValueAsString
            uISePositionPercentEdit.ValueAsString = this.RegisterNewEmployee_PositionParams.UISePositionPercentEditValueAsString;

            // Type 'Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+RuleSetDisplay' in 'GSSearchLookUpEdit' LookUpEdit
            //ValueTypeName
            uIGSSearchLookUpEditLookUpEdit.ValueTypeName = this.RegisterNewEmployee_PositionParams.UIGSSearchLookUpEditLookUpEditValueTypeName;

            // Type 'TURNUS - Turnus 35,5t/uke (35,5 t/uke)' in 'GSSearchLookUpEdit' LookUpEdit
            //ValueAsString
            uIGSSearchLookUpEditLookUpEdit.ValueAsString = this.RegisterNewEmployee_PositionParams.UIGSSearchLookUpEditLookUpEditValueAsString;

            // Type 'Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+PositionCategoryDisplay' in 'GSSearchLookUpEdit' LookUpEdit
            //ValueTypeName
            uIGSSearchLookUpEditLookUpEdit1.ValueTypeName = this.RegisterNewEmployee_PositionParams.UIGSSearchLookUpEditLookUpEdit1ValueTypeName;

            // Type 'S - Sykepleier' in 'GSSearchLookUpEdit' LookUpEdit
            //ValueAsString
            uIGSSearchLookUpEditLookUpEdit1.ValueAsString = this.RegisterNewEmployee_PositionParams.UIGSSearchLookUpEditLookUpEdit1ValueAsString;

            // Type 'Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+WeaAgreementDisplay' in 'GSSearchLookUpEdit' LookUpEdit
            //ValueTypeName
            uIGSSearchLookUpEditLookUpEdit2.ValueTypeName = this.RegisterNewEmployee_PositionParams.UIGSSearchLookUpEditLookUpEdit2ValueTypeName;

            // Type 'Turnus 35,5' in 'GSSearchLookUpEdit' LookUpEdit
            //ValueAsString
            uIGSSearchLookUpEditLookUpEdit2.ValueAsString = this.RegisterNewEmployee_PositionParams.UIGSSearchLookUpEditLookUpEdit2ValueAsString;

            // Type '{Tab}' in 'GSSearchLookUpEdit' LookUpEdit
            Keyboard.SendKeys(uIGSSearchLookUpEditLookUpEdit2, this.RegisterNewEmployee_PositionParams.UIGSSearchLookUpEditLookUpEdit2SendKeys, ModifierKeys.None);

            // Type '12121212' in 'teInternalPositionNumber' text box
            //ValueAsString
            uITeInternalPositionNuEdit.ValueAsString = this.RegisterNewEmployee_PositionParams.UITeInternalPositionNuEditValueAsString;

            // Type '{Tab}' in 'teInternalPositionNumber' text box
            Keyboard.SendKeys(uITeInternalPositionNuEdit, this.RegisterNewEmployee_PositionParams.UITab, ModifierKeys.None);

            UIMapVS2017.SelectUnion();

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        /// <summary>
        /// Select_DepShiftcodesTab
        /// </summary>
        public void Select_DepShiftcodesTabNew()
        {
            // Click 'Avdeling' client
            //Hack: DelphiTabkomponent
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Department);
            UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Vaktkoder, false);
        }

        /// <summary>
        /// Create_NewCode_N2 - Use 'Create_NewCode_N2Params' to pass parameters into this method.
        /// </summary>
        public void Create_NewCode_N2()
        {
            #region Variable Declarations
            var toolBarClient = this.UIGatver63031732ASCLAvWindow22.UIPaToolbarClient;
            var uINyvaktkodeButton = toolBarClient.UINyvaktkodeButton;
            DXTextEdit uITe_NSC_CodeEdit = this.UINyvaktkodeWindow.UIPcContentClient.UIPaNormalShiftcodePanClient.UITcShiftCodeTypesTabList.UITpNormalShiftCodeClient.UITe_NSC_CodeEdit;
            DXTextEdit uIETime2Edit = this.UINyvaktkodeWindow.UIPcContentClient.UIPaNormalShiftcodePanClient.UITcShiftCodeTypesTabList.UITpNormalShiftCodeClient.UIETime2Edit;
            DXTextEdit uIETime5Edit = this.UINyvaktkodeWindow.UIPcContentClient.UIPaNormalShiftcodePanClient.UITcShiftCodeTypesTabList.UITpNormalShiftCodeClient.UIETime5Edit;
            DXButton uIGSSimpleButtonButton = this.UINyvaktkodeWindow.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            #endregion

            toolBarClient.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            toolBarClient.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            toolBarClient.SearchProperties[WinWindow.PropertyNames.ControlName] = "paToolbar";

            var point = new Point();
            Playback.Wait(1000);
            try
            {
                var rec = uINyvaktkodeButton.BoundingRectangle;
                point = new Point(rec.X, rec.Y);

                uINyvaktkodeButton.DrawHighlight();
                // Click 'Ny vaktkode' button  
                Mouse.Click(uINyvaktkodeButton);
            }
            catch (Exception)
            {
                Mouse.Click(point);
            }

            // Type 'N2' in 'te_NSC_Code' text box
            //ValueAsString
            Playback.Wait(1000);
            try
            {
                uITe_NSC_CodeEdit.DrawHighlight();
                Mouse.Click(uITe_NSC_CodeEdit);
                uITe_NSC_CodeEdit.ValueAsString = this.Create_NewCode_N2Params.UITe_NSC_CodeEditValueAsString;
            }
            catch (Exception)
            {
                try
                {
                    Keyboard.SendKeys(uITe_NSC_CodeEdit, this.Create_NewCode_N2Params.UITe_NSC_CodeEditValueAsString, ModifierKeys.None);
                }
                catch (Exception)
                {
                    Keyboard.SendKeys(this.Create_NewCode_N2Params.UITe_NSC_CodeEditValueAsString, ModifierKeys.None);
                }
            }

            // Type '{Tab}' in 'te_NSC_Code' text box
            try
            {
                Keyboard.SendKeys(uITe_NSC_CodeEdit, this.Create_NewCode_N2Params.UITe_NSC_CodeEditSendKeyTab, ModifierKeys.None);
            }
            catch (Exception)
            {
                Keyboard.SendKeys(this.Create_NewCode_N2Params.UITe_NSC_CodeEditSendKeyTab, ModifierKeys.None);
            }


            // Type '22:00 [SelectionStart]0[SelectionLength]5' in 'eTime[2]' text box
            //ValueAsString
            try
            {
                uIETime2Edit.ValueAsString = this.Create_NewCode_N2Params.UIETime2EditValueAsString;
                // Type '{Tab}' in 'uIETime5Edit' text box
                Keyboard.SendKeys(uIETime2Edit, this.RegisterNewEmployee_PositionParams.UITab, ModifierKeys.None);
            }
            catch (Exception)
            {
                Keyboard.SendKeys(this.Create_NewCode_N2Params.UIETime2EditValueAsString, ModifierKeys.None);
                Keyboard.SendKeys(this.RegisterNewEmployee_PositionParams.UITab, ModifierKeys.None);
            }

            // Type '08:00 [SelectionStart]0[SelectionLength]5' in 'eTime[5]' text box
            //ValueAsString
            try
            {
                uIETime5Edit.ValueAsString = this.Create_NewCode_N2Params.UIETime5EditValueAsString;
                // Type '{Tab}' in 'uIETime5Edit' text box
                Keyboard.SendKeys(uIETime5Edit, this.RegisterNewEmployee_PositionParams.UITab, ModifierKeys.None);
            }
            catch (Exception)
            {
                // Type '{Tab}' in 'uIETime5Edit' text box
                Keyboard.SendKeys(this.Create_NewCode_N2Params.UIETime5EditValueAsString, ModifierKeys.None);
                Keyboard.SendKeys(this.RegisterNewEmployee_PositionParams.UITab, ModifierKeys.None);
            }

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        /// <summary>
        /// CreateNewPlanFor_NN - Use 'CreateNewPlanFor_NNParams' to pass parameters into this method.
        /// </summary>
        public void CreateNewPlanFor_NN()
        {
            #region Variable Declarations
            DXTextEdit uIENameEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIENameEdit;
            DXPopupEdit uIPceDate2PopupEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIPceDate2PopupEdit;
            DXTextEdit uIENumber1Edit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIENumber1Edit;
            DXPopupEdit uIPceDate0PopupEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIPceDate0PopupEdit;
            DXTextEdit uIENumber0Edit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIENumber0Edit;
            DXButton uIGSSimpleButtonButton = this.UIArbeidsplanInnstilliWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Type  in 'eName' text box
            //ValueAsString
            uIENameEdit.ValueAsString = this.CreateNewPlanFor_NNParams.UIENameEditValueAsString;

            // Type in 'eName' text box
            //Keyboard.SendKeys(uIENameEdit, this.CreateNewPlanFor_NNParams.UIENameEditValueAsString, ModifierKeys.None);
            Keyboard.SendKeys(uIENameEdit, this.CreateNewPlanFor_NNParams.UIENameEditSendTab, ModifierKeys.None);

            SetPlanStartDate(this.CreateNewPlanFor_NNParams.UIPceDate2PopupEditValueAsDateTime);

            // Type '1' in 'eNumber[1]' text box
            //ValueAsString
            uIENumber1Edit.ValueAsString = this.CreateNewPlanFor_NNParams.UIENumber1EditValueAsString;
            Keyboard.SendKeys(uIENumber1Edit, this.CreateNewPlanFor_NNParams.UIENameEditSendTab, ModifierKeys.None);

            SetPlanToDateNew(this.SetPlanToDateNewParams.UIPceDateDateTimeEditValueAsDate);


            // Type '1 [SelectionStart]0[SelectionLength]1' in 'eNumber[0]' text box
            //ValueAsString
            uIENumber0Edit.ValueAsString = this.CreateNewPlanFor_NNParams.UIENumber0EditValueAsString;
            Keyboard.SendKeys(uIENumber0Edit, this.CreateNewPlanFor_NNParams.UIENameEditSendTab, ModifierKeys.None);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);

            AddNNToRosterplan();

        }

        /// <summary>
        /// AddNNToRosterplan
        /// </summary>
        public void AddNNToRosterplan()
        {
            #region Variable Declarations
            //DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanChapter_6Window1.UIRcMenuRibbon.UIRpPlanRibbonPage;
            //DXRibbonButtonItem uIAnsatteRibbonBaseButtonItem = this.UIArbeidsplanChapter_6Window1.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanRibbonPageGroup.UIAnsatteRibbonBaseButtonItem;
            DXButton uIAnsatteButton = this.UIAnsatteiArbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UIAnsatteButton;
            DXCell uINNCell = this.UILeggtilansatteWindow.UIViewHostCustom.UIPcViewClient.UISelectDepartmentEmplCustom.UIPcContentContainerClient.UIPcContentClient.UIGcDepartmentEmployeeTable.UINNCell;
            DXButton uISimpleButtonButton = this.UILeggtilansatteWindow.UISimpleButtonButton;
            DXButton uIOKButton = this.UIAnsatteiArbeidsplanWindow.UIOKButton;
            DXRibbonPage uIRpRosterPlanRibbonPage = this.UIArbeidsplanChapter_6Window1.UIRcMenuRibbon.UIRpRosterPlanRibbonPage;
            #endregion

            // Click 'rpPlan' RibbonPage
            UICommon.ClickRosterplanPlanTab();

            // Click 'Ansatte' RibbonBaseButtonItem
            UICommon.ClickEmployeesButtonRosterplan();
            //Mouse.Click(uIAnsatteRibbonBaseButtonItem);

            // Click 'Ansatte...' button
            Mouse.Click(uIAnsatteButton, new Point(1, 1));

            // Click 'N, N' cell
            Mouse.Click(uINNCell, new Point(66, 10));

            // Click 'SimpleButton' button
            Mouse.Click(uISimpleButtonButton, new Point(1, 1));

            // Click 'OK' button
            Mouse.Click(uIOKButton, new Point(1, 1));

            // Click 'rpRosterPlan' RibbonPage
            Mouse.Click(uIRpRosterPlanRibbonPage, new Point(29, 11));
        }

        /// <summary>
        /// Click_EditRosterPlan
        /// </summary>
        public void Click_EditRosterPlan()
        {
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();
        }
        public void Click_EditRosterPlanFromHomeTab()
        {
            UICommon.UIMapVS2017.ClickEditRosterplanFromHomeTab();
        }

        /// <summary>
        /// Click_Sat
        /// </summary>
        public void Click_Sat()
        {
            #region Variable Declarations
            DXCell uIItemCell1 = this.UIArbeidsplanKopiavGruWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell1;
            #endregion

            // Click cell
            Mouse.Click(uIItemCell1, new Point(365, 52));
            Playback.Wait(1000);
            Mouse.Click(uIItemCell1, new Point(365, 52));
        }
        public void Click_RosterCell()
        {
            #region Variable Declarations
            DXCell uIItemCell = this.UIArbeidsplanKopiavGruWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            #endregion

            // Click cell
            Mouse.Click(uIItemCell, new Point(160, 55));
        }

        #endregion


        #region Report Values Chapter 1

        public List<string> CheckReportValuesStep5()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep5();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 5): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesStep6()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep6();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 6): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesStep7()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep7();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 7): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesStep8()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep8();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 8): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesStep9()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep9();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 9): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesStep10()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep10();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 10): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesStep10_2()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep10_2();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 10_2): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesStep11()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep11();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 11): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesStep12()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep12();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 12): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesStep14()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep14();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 14): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesStep16()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesStep16();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 1(Step 16): " + e.Message);
            }

            return errorList;
        }

        #endregion

        #region Report Values Chapter 2

        public List<string> CheckReportValuesC2Step3()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC2Step3();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 3): " + e.Message);
            }

            return errorList;
        }

        public List<string> CheckReportValuesC2Step4()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC2Step4();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 4): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC2Step5()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC2Step5();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 5): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC2Step6()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC2Step6();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 6): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC2Step9()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC2Step9();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 2(Step 9): " + e.Message);
            }

            return errorList;
        }

        #endregion

        #region Report Values Chapter 3

        public List<string> CheckReportValuesC3Step1()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step1();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 1): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step2()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step2();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 2): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step3()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step3();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 3): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step4()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step1();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 4): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step5()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step1();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 5): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step6()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step6();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 6): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step7()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step7();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 7): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step8()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step1();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 8): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step9()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step9();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 9): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step10()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step10();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 10): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step11()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step11();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 11): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC3Step12()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC3Step12();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 3(Step 12): " + e.Message);
            }

            return errorList;
        }
        #endregion

        #region Report Values Chapter 4

        public List<string> CheckReportValuesC4Step1()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC4Step1();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 1): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC4Step2()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC4Step2();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 2): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC4Step3()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC4Step3();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 3): " + e.Message);
            }

            return errorList;
        }

        public List<string> CheckReportValuesC4Step5()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC4Step5();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 5): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC4Step6()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC4Step6();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 6): " + e.Message);
            }

            return errorList;
        }

        public List<string> CheckReportValuesC4Step8()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC8Step8();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 8): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC4Step9()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC4Step9();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 9): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC4Step10()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC4Step10();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 10): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC4Step11()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC4Step11();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 11): " + e.Message);
            }

            return errorList;
        }
       
        public List<string> CheckReportValuesC4Step13()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC4Step13();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 13): " + e.Message);
            }

            return errorList;
        }

        public List<string> CheckReportValuesC4Step14()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC4Step14();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 14): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC4Step15()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC4Step15();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 15): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC4Step16()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC4Step16();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 16): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC4Step17()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC4Step16();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 4(Step 17): " + e.Message);
            }

            return errorList;
        }
        #endregion

        #region Report Values Chapter 5

        public List<string> CheckReportValuesC5Step3()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC5Step3();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 3): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC5Step4()
        {
            var errorList = new List<string>();
            try
            {
                 UIMapVS2017.CheckReportValuesC5Step4();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 4): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC5Step5()
        {
            var errorList = new List<string>();
            try
            {
               UIMapVS2017.CheckReportValuesC5Step5();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 5): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC5Step6()
        {
            var errorList = new List<string>();
            try
            {
                //UIMapVS2017.CheckReportValuesC5Step6();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 5(Step 6): " + e.Message);
            }

            return errorList;
        }
        #endregion

        #region Report Values Chapter 6

        public List<string> CheckReportValuesC6Step4()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC6Step45();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 6(Step 4): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC6Step5()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC6Step45();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 6(Step 5): " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckReportValuesC6Step6()
        {
            var errorList = new List<string>();
            try
            {
                UIMapVS2017.CheckReportValuesC6Step6();
            }
            catch (Exception e)
            {
                errorList.Add("An error occured in chaper 6(Step 6): " + e.Message);
            }

            return errorList;
        }

        #endregion


        #region Parameters

        public virtual SelectReport96_F3Params SelectReport96_F3Params
        {
            get
            {
                if ((this.mSelectReport96_F3Params == null))
                {
                    this.mSelectReport96_F3Params = new SelectReport96_F3Params();
                }
                return this.mSelectReport96_F3Params;
            }
        }

        private SelectReport96_F3Params mSelectReport96_F3Params;


        public virtual SaveReportParams SaveReportParams
        {
            get
            {
                if ((this.mSaveReportParams == null))
                {
                    this.mSaveReportParams = new SaveReportParams();
                }
                return this.mSaveReportParams;
            }
        }

        private SaveReportParams mSaveReportParams;

        public virtual SetPeriodParams SetPeriodParams
        {
            get
            {
                if ((this.mSetPeriodParams == null))
                {
                    this.mSetPeriodParams = new SetPeriodParams();
                }
                return this.mSetPeriodParams;
            }
        }

        private SetPeriodParams mSetPeriodParams;


        public virtual SelectLT_F3Params SelectLT_F3Params
        {
            get
            {
                if ((this.mSelectLT_F3Params == null))
                {
                    this.mSelectLT_F3Params = new SelectLT_F3Params();
                }
                return this.mSelectLT_F3Params;
            }
        }

        private SelectLT_F3Params mSelectLT_F3Params;


        public virtual Select400Params Select400Params
        {
            get
            {
                if ((this.mSelect400Params == null))
                {
                    this.mSelect400Params = new Select400Params();
                }
                return this.mSelect400Params;
            }
        }

        private Select400Params mSelect400Params;

        /// <summary>
        /// CheckSpekter - Use 'CheckSpekterExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckSpekter()
        {
            #region Variable Declarations
            WinCheckBox uIBrukkodepåspekterrapCheckBox = this.UILønnsogtrekkarterWindow.UIItemClient.UIBrukkodepåspekterrapCheckBox;
            #endregion

            // Verify that the 'Checked' property of 'Bruk kode på spekterrapport' check box equals 'True'
            Assert.AreEqual(this.CheckSpekterExpectedValues.UIBrukkodepåspekterrapCheckBoxChecked, uIBrukkodepåspekterrapCheckBox.Checked, "Spekter not checked");
        }

        public virtual CheckSpekterExpectedValues CheckSpekterExpectedValues
        {
            get
            {
                if ((this.mCheckSpekterExpectedValues == null))
                {
                    this.mCheckSpekterExpectedValues = new CheckSpekterExpectedValues();
                }
                return this.mCheckSpekterExpectedValues;
            }
        }

        private CheckSpekterExpectedValues mCheckSpekterExpectedValues;

        public virtual Select_ExtraEmployeeParams Select_ExtraEmployeeParams
        {
            get
            {
                if ((this.mSelect_ExtraEmployeeParams == null))
                {
                    this.mSelect_ExtraEmployeeParams = new Select_ExtraEmployeeParams();
                }
                return this.mSelect_ExtraEmployeeParams;
            }
        }

        private Select_ExtraEmployeeParams mSelect_ExtraEmployeeParams;

        public virtual Select_ExtraShiftCode_DParams Select_ExtraShiftCode_DParams
        {
            get
            {
                if ((this.mSelect_ExtraShiftCode_DParams == null))
                {
                    this.mSelect_ExtraShiftCode_DParams = new Select_ExtraShiftCode_DParams();
                }
                return this.mSelect_ExtraShiftCode_DParams;
            }
        }

        private Select_ExtraShiftCode_DParams mSelect_ExtraShiftCode_DParams;


        public virtual Select_ExtraDateParams Select_ExtraDateParams
        {
            get
            {
                if ((this.mSelect_ExtraDateParams == null))
                {
                    this.mSelect_ExtraDateParams = new Select_ExtraDateParams();
                }
                return this.mSelect_ExtraDateParams;
            }
        }

        private Select_ExtraDateParams mSelect_ExtraDateParams;




        /// <summary>
        /// CheckDetaljPrUke - Use 'CheckDetaljPrUkeParams' to pass parameters into this method.
        /// </summary>
        public void CheckDetaljPrUke(bool check)
        {
            #region Variable Declarations
            WinCheckBox uIVisdetaljerprukeCheckBox = this.UIGatver63031732ASCLAvWindow4.UIItemWindow.UIStatistikkforhelgearClient.UIVisdetaljerprukeCheckBox;
            #endregion

            if (uIVisdetaljerprukeCheckBox.Checked != check)
                uIVisdetaljerprukeCheckBox.Checked = check;
        }

        public virtual CheckDetaljPrUkeParams CheckDetaljPrUkeParams
        {
            get
            {
                if ((this.mCheckDetaljPrUkeParams == null))
                {
                    this.mCheckDetaljPrUkeParams = new CheckDetaljPrUkeParams();
                }
                return this.mCheckDetaljPrUkeParams;
            }
        }

        private CheckDetaljPrUkeParams mCheckDetaljPrUkeParams;

        public virtual EditExtraShiftParams EditExtraShiftParams
        {
            get
            {
                if ((this.mEditExtraShiftParams == null))
                {
                    this.mEditExtraShiftParams = new EditExtraShiftParams();
                }
                return this.mEditExtraShiftParams;
            }
        }

        private EditExtraShiftParams mEditExtraShiftParams;

        public virtual GoTo3105_Date2Params GoTo3105_Date2Params
        {
            get
            {
                if ((this.mGoTo3105_Date2Params == null))
                {
                    this.mGoTo3105_Date2Params = new GoTo3105_Date2Params();
                }
                return this.mGoTo3105_Date2Params;
            }
        }

        private GoTo3105_Date2Params mGoTo3105_Date2Params;

        public virtual Select_AbcenceCodeParams Select_AbcenceCodeParams
        {
            get
            {
                if ((this.mSelect_AbcenceCodeParams == null))
                {
                    this.mSelect_AbcenceCodeParams = new Select_AbcenceCodeParams();
                }
                return this.mSelect_AbcenceCodeParams;
            }
        }

        private Select_AbcenceCodeParams mSelect_AbcenceCodeParams;

        public virtual ChangeRosterIntervallParams ChangeRosterIntervallParams
        {
            get
            {
                if ((this.mChangeRosterIntervallParams == null))
                {
                    this.mChangeRosterIntervallParams = new ChangeRosterIntervallParams();
                }
                return this.mChangeRosterIntervallParams;
            }
        }

        private ChangeRosterIntervallParams mChangeRosterIntervallParams;

        public virtual SetRosterStartAndWeeksParams SetRosterStartAndWeeksParams
        {
            get
            {
                if ((this.mSetRosterStartAndWeeksParams == null))
                {
                    this.mSetRosterStartAndWeeksParams = new SetRosterStartAndWeeksParams();
                }
                return this.mSetRosterStartAndWeeksParams;
            }
        }

        private SetRosterStartAndWeeksParams mSetRosterStartAndWeeksParams;


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

        public virtual Insert_ShiftCode_EmployeeParams Insert_ShiftCode_EmployeeParams
        {
            get
            {
                if ((this.mInsert_DK6_Emp16Params == null))
                {
                    this.mInsert_DK6_Emp16Params = new Insert_ShiftCode_EmployeeParams();
                }
                return this.mInsert_DK6_Emp16Params;
            }
        }

        private Insert_ShiftCode_EmployeeParams mInsert_DK6_Emp16Params;



        public virtual Insert_DK7_Emp17Params Insert_DK7_Emp17Params
        {
            get
            {
                if ((this.mInsert_DK7_Emp17Params == null))
                {
                    this.mInsert_DK7_Emp17Params = new Insert_DK7_Emp17Params();
                }
                return this.mInsert_DK7_Emp17Params;
            }
        }

        private Insert_DK7_Emp17Params mInsert_DK7_Emp17Params;

        public virtual SetPlanToDateParams SetPlanToDateParams
        {
            get
            {
                if ((this.mSetPlanToDateParams == null))
                {
                    this.mSetPlanToDateParams = new SetPlanToDateParams();
                }
                return this.mSetPlanToDateParams;
            }
        }

        private SetPlanToDateParams mSetPlanToDateParams;

        public virtual Clear_RosterPlanParams Clear_RosterPlanParams
        {
            get
            {
                if ((this.mClear_RosterPlanParams == null))
                {
                    this.mClear_RosterPlanParams = new Clear_RosterPlanParams();
                }
                return this.mClear_RosterPlanParams;
            }
        }

        private Clear_RosterPlanParams mClear_RosterPlanParams;


        public virtual InsertDK6Params InsertDK6Params
        {
            get
            {
                if ((this.mInsertDK6Params == null))
                {
                    this.mInsertDK6Params = new InsertDK6Params();
                }
                return this.mInsertDK6Params;
            }
        }

        private InsertDK6Params mInsertDK6Params;


        public virtual RenamePlanParams RenamePlanParams
        {
            get
            {
                if ((this.mRenamePlanParams == null))
                {
                    this.mRenamePlanParams = new RenamePlanParams();
                }
                return this.mRenamePlanParams;
            }
        }

        private RenamePlanParams mRenamePlanParams;


        public virtual SetStartDate_EasterPeriodParams SetStartDate_EasterPeriodParams
        {
            get
            {
                if ((this.mSetStartDate_EasterPeriodParams == null))
                {
                    this.mSetStartDate_EasterPeriodParams = new SetStartDate_EasterPeriodParams();
                }
                return this.mSetStartDate_EasterPeriodParams;
            }
        }

        private SetStartDate_EasterPeriodParams mSetStartDate_EasterPeriodParams;

        public virtual Select_CauseCodeVacancyNurseParams Select_CauseCodeVacancyNurseParams
        {
            get
            {
                if ((this.mSelect_CauseCodeVacancyNurseParams == null))
                {
                    this.mSelect_CauseCodeVacancyNurseParams = new Select_CauseCodeVacancyNurseParams();
                }
                return this.mSelect_CauseCodeVacancyNurseParams;
            }
        }

        private Select_CauseCodeVacancyNurseParams mSelect_CauseCodeVacancyNurseParams;

        public virtual Set_EmpPercent_Params Set_EmpPercent_Params
        {
            get
            {
                if ((this.mSet_EmpPercent_Params == null))
                {
                    this.mSet_EmpPercent_Params = new Set_EmpPercent_Params();
                }
                return this.mSet_EmpPercent_Params;
            }
        }

        private Set_EmpPercent_Params mSet_EmpPercent_Params;

        public virtual Check_WinCloseButton_SysCheckWindowExpectedValues Check_WinCloseButton_SysCheckWindowExpectedValues
        {
            get
            {
                if ((this.mCheck_WinCloseButton_SysCheckWindowExpectedValues == null))
                {
                    this.mCheck_WinCloseButton_SysCheckWindowExpectedValues = new Check_WinCloseButton_SysCheckWindowExpectedValues();
                }
                return this.mCheck_WinCloseButton_SysCheckWindowExpectedValues;
            }
        }

        private Check_WinCloseButton_SysCheckWindowExpectedValues mCheck_WinCloseButton_SysCheckWindowExpectedValues;

        public virtual Check_WinCloseButton_SysMessageExpectedValues Check_WinCloseButton_SysMessageExpectedValues
        {
            get
            {
                if ((this.mCheck_WinCloseButton_SysMessageExpectedValues == null))
                {
                    this.mCheck_WinCloseButton_SysMessageExpectedValues = new Check_WinCloseButton_SysMessageExpectedValues();
                }
                return this.mCheck_WinCloseButton_SysMessageExpectedValues;
            }
        }

        private Check_WinCloseButton_SysMessageExpectedValues mCheck_WinCloseButton_SysMessageExpectedValues;


        public virtual Check_CloseButton_SysMessageExpectedValues Check_CloseButton_SysMessageExpectedValues
        {
            get
            {
                if ((this.mCheck_CloseButton_SysMessageExpectedValues == null))
                {
                    this.mCheck_CloseButton_SysMessageExpectedValues = new Check_CloseButton_SysMessageExpectedValues();
                }
                return this.mCheck_CloseButton_SysMessageExpectedValues;
            }
        }

        private Check_CloseButton_SysMessageExpectedValues mCheck_CloseButton_SysMessageExpectedValues;

        public virtual LoginParams LoginParams
        {
            get
            {
                if ((this.mLoginParams == null))
                {
                    this.mLoginParams = new LoginParams();
                }
                return this.mLoginParams;
            }
        }

        private LoginParams mLoginParams;


        public virtual TypeInDeparmentValueParams TypeInDeparmentValueParams
        {
            get
            {
                if ((this.mTypeInDeparmentValueParams == null))
                {
                    this.mTypeInDeparmentValueParams = new TypeInDeparmentValueParams();
                }
                return this.mTypeInDeparmentValueParams;
            }
        }

        private TypeInDeparmentValueParams mTypeInDeparmentValueParams;





        public virtual TypeInUserNameParams TypeInUserNameParams
        {
            get
            {
                if ((this.mTypeInUserNameParams == null))
                {
                    this.mTypeInUserNameParams = new TypeInUserNameParams();
                }
                return this.mTypeInUserNameParams;
            }
        }

        private TypeInUserNameParams mTypeInUserNameParams;

        public virtual TypeInPasswordParams TypeInPasswordParams
        {
            get
            {
                if ((this.mTypeInPasswordParams == null))
                {
                    this.mTypeInPasswordParams = new TypeInPasswordParams();
                }
                return this.mTypeInPasswordParams;
            }
        }

        private TypeInPasswordParams mTypeInPasswordParams;

        public virtual Set_AbsTo50PercentParams Set_AbsTo50PercentParams
        {
            get
            {
                if ((this.mSet_AbsTo50PercentParams == null))
                {
                    this.mSet_AbsTo50PercentParams = new Set_AbsTo50PercentParams();
                }
                return this.mSet_AbsTo50PercentParams;
            }
        }

        private Set_AbsTo50PercentParams mSet_AbsTo50PercentParams;


        public virtual ChangeAbsTodateParams ChangeAbsTodateParams
        {
            get
            {
                if ((this.mChangeAbsTodateParams == null))
                {
                    this.mChangeAbsTodateParams = new ChangeAbsTodateParams();
                }
                return this.mChangeAbsTodateParams;
            }
        }

        private ChangeAbsTodateParams mChangeAbsTodateParams;

        public virtual Check_AbsFromDateExpectedValues Check_AbsFromDateExpectedValues
        {
            get
            {
                if ((this.mCheck_AbsFromDateExpectedValues == null))
                {
                    this.mCheck_AbsFromDateExpectedValues = new Check_AbsFromDateExpectedValues();
                }
                return this.mCheck_AbsFromDateExpectedValues;
            }
        }

        private Check_AbsFromDateExpectedValues mCheck_AbsFromDateExpectedValues;


        public virtual AdjustShiftSatParams AdjustShiftSatParams
        {
            get
            {
                if ((this.mAdjustShiftSatParams == null))
                {
                    this.mAdjustShiftSatParams = new AdjustShiftSatParams();
                }
                return this.mAdjustShiftSatParams;
            }
        }

        private AdjustShiftSatParams mAdjustShiftSatParams;

        public virtual AdjustShiftSunParams AdjustShiftSunParams
        {
            get
            {
                if ((this.mAdjustShiftSunParams == null))
                {
                    this.mAdjustShiftSunParams = new AdjustShiftSunParams();
                }
                return this.mAdjustShiftSunParams;
            }
        }

        private AdjustShiftSunParams mAdjustShiftSunParams;


        public virtual ContructNewShiftParams ContructNewShiftParams
        {
            get
            {
                if ((this.mContructNewShiftParams == null))
                {
                    this.mContructNewShiftParams = new ContructNewShiftParams();
                }
                return this.mContructNewShiftParams;
            }
        }

        private ContructNewShiftParams mContructNewShiftParams;


        public virtual Select_EmployeeAbsenceParams Select_EmployeeAbsenceParams
        {
            get
            {
                if ((this.mSelect_EmployeeAbsenceParams == null))
                {
                    this.mSelect_EmployeeAbsenceParams = new Select_EmployeeAbsenceParams();
                }
                return this.mSelect_EmployeeAbsenceParams;
            }
        }

        private Select_EmployeeAbsenceParams mSelect_EmployeeAbsenceParams;

        public virtual Select_Emp16ExchangeParams Select_Emp16ExchangeParams
        {
            get
            {
                if ((this.mSelect_Emp16ExchangeParams == null))
                {
                    this.mSelect_Emp16ExchangeParams = new Select_Emp16ExchangeParams();
                }
                return this.mSelect_Emp16ExchangeParams;
            }
        }

        private Select_Emp16ExchangeParams mSelect_Emp16ExchangeParams;


        public virtual Click_DepartmentExchangeEmpParams Click_DepartmentExchangeEmpParams
        {
            get
            {
                if ((this.mClick_DepartmentExchangeEmpParams == null))
                {
                    this.mClick_DepartmentExchangeEmpParams = new Click_DepartmentExchangeEmpParams();
                }
                return this.mClick_DepartmentExchangeEmpParams;
            }
        }
        public virtual Construct_NewShiftForRemanageParams Construct_NewShiftForRemanageParams
        {
            get
            {
                if ((this.mConstruct_NewShiftForRemanageParams == null))
                {
                    this.mConstruct_NewShiftForRemanageParams = new Construct_NewShiftForRemanageParams();
                }
                return this.mConstruct_NewShiftForRemanageParams;
            }
        }

        private Click_DepartmentExchangeEmpParams mClick_DepartmentExchangeEmpParams;

        private Construct_NewShiftForRemanageParams mConstruct_NewShiftForRemanageParams;



        public virtual AddNewKontParams AddNewKontParams
        {
            get
            {
                if ((this.mAddNewKontParams == null))
                {
                    this.mAddNewKontParams = new AddNewKontParams();
                }
                return this.mAddNewKontParams;
            }
        }

        private AddNewKontParams mAddNewKontParams;


        public virtual SetExtraToTimeAndColumnParams SetExtraToTimeAndColumnParams
        {
            get
            {
                if ((this.mSetExtraToTimeAndColumnParams == null))
                {
                    this.mSetExtraToTimeAndColumnParams = new SetExtraToTimeAndColumnParams();
                }
                return this.mSetExtraToTimeAndColumnParams;
            }
        }

        private SetExtraToTimeAndColumnParams mSetExtraToTimeAndColumnParams;

        public virtual AdminVaktkategorierSetUtrykningHhjemmevaktParams AdminVaktkategorierSetUtrykningHhjemmevaktParams
        {
            get
            {
                if ((this.mAdminVaktkategorierSetUtrykningHhjemmevaktParams == null))
                {
                    this.mAdminVaktkategorierSetUtrykningHhjemmevaktParams = new AdminVaktkategorierSetUtrykningHhjemmevaktParams();
                }
                return this.mAdminVaktkategorierSetUtrykningHhjemmevaktParams;
            }
        }

        private AdminVaktkategorierSetUtrykningHhjemmevaktParams mAdminVaktkategorierSetUtrykningHhjemmevaktParams;

        public virtual Set_UtrykningsPeriodParams Set_UtrykningsPeriodParams
        {
            get
            {
                if ((this.mSet_UtrykningsPeriodParams == null))
                {
                    this.mSet_UtrykningsPeriodParams = new Set_UtrykningsPeriodParams();
                }
                return this.mSet_UtrykningsPeriodParams;
            }
        }

        private Set_UtrykningsPeriodParams mSet_UtrykningsPeriodParams;

        public virtual SetPlanStartdateAndWeeksParams SetPlanStartdateAndWeeksParams
        {
            get
            {
                if ((this.mSetPlanStartdateAndWeeksParams == null))
                {
                    this.mSetPlanStartdateAndWeeksParams = new SetPlanStartdateAndWeeksParams();
                }
                return this.mSetPlanStartdateAndWeeksParams;
            }
        }

        private SetPlanStartdateAndWeeksParams mSetPlanStartdateAndWeeksParams;
        public virtual Select_CauseCodeForFiveEmployeesParams Select_CauseCodeForFiveEmployeesParams
        {
            get
            {
                if ((this.mSelect_CauseCodeForFiveEmployeesParams == null))
                {
                    this.mSelect_CauseCodeForFiveEmployeesParams = new Select_CauseCodeForFiveEmployeesParams();
                }
                return this.mSelect_CauseCodeForFiveEmployeesParams;
            }
        }

        private Select_CauseCodeForFiveEmployeesParams mSelect_CauseCodeForFiveEmployeesParams;

        public virtual Effectuate_CalendarPlanParams Effectuate_CalendarPlanParams
        {
            get
            {
                if ((this.mEffectuate_CalendarPlanParams == null))
                {
                    this.mEffectuate_CalendarPlanParams = new Effectuate_CalendarPlanParams();
                }
                return this.mEffectuate_CalendarPlanParams;
            }
        }

        private Effectuate_CalendarPlanParams mEffectuate_CalendarPlanParams;


        public virtual RegisterNewEmployeeParams RegisterNewEmployeeParams
        {
            get
            {
                if ((this.mRegisterNewEmployeeParams == null))
                {
                    this.mRegisterNewEmployeeParams = new RegisterNewEmployeeParams();
                }
                return this.mRegisterNewEmployeeParams;
            }
        }

        private RegisterNewEmployeeParams mRegisterNewEmployeeParams;

        public virtual RegisterNewEmployee_PositionParams RegisterNewEmployee_PositionParams
        {
            get
            {
                if ((this.mRegisterNewEmployee_PositionParams == null))
                {
                    this.mRegisterNewEmployee_PositionParams = new RegisterNewEmployee_PositionParams();
                }
                return this.mRegisterNewEmployee_PositionParams;
            }
        }

        private RegisterNewEmployee_PositionParams mRegisterNewEmployee_PositionParams;

        public virtual Create_NewCode_N2Params Create_NewCode_N2Params
        {
            get
            {
                if ((this.mCreate_NewCode_N2Params == null))
                {
                    this.mCreate_NewCode_N2Params = new Create_NewCode_N2Params();
                }
                return this.mCreate_NewCode_N2Params;
            }
        }

        private Create_NewCode_N2Params mCreate_NewCode_N2Params;

        public virtual CreateNewPlanFor_NNParams CreateNewPlanFor_NNParams
        {
            get
            {
                if ((this.mCreateNewPlanFor_NNParams == null))
                {
                    this.mCreateNewPlanFor_NNParams = new CreateNewPlanFor_NNParams();
                }
                return this.mCreateNewPlanFor_NNParams;
            }
        }

        private CreateNewPlanFor_NNParams mCreateNewPlanFor_NNParams;

        public virtual DeleteTT_400_RegParams DeleteTT_400_RegParams
        {
            get
            {
                if ((this.mDeleteTT_400_RegParams == null))
                {
                    this.mDeleteTT_400_RegParams = new DeleteTT_400_RegParams();
                }
                return this.mDeleteTT_400_RegParams;
            }
        }

        private DeleteTT_400_RegParams mDeleteTT_400_RegParams;

        public virtual ActivateBeregningsregel44_410_420Params ActivateBeregningsregel44_410_420Params
        {
            get
            {
                if ((this.mActivateBeregningsregel44_410_420Params == null))
                {
                    this.mActivateBeregningsregel44_410_420Params = new ActivateBeregningsregel44_410_420Params();
                }
                return this.mActivateBeregningsregel44_410_420Params;
            }
        }

        private ActivateBeregningsregel44_410_420Params mActivateBeregningsregel44_410_420Params;

        public virtual RegHourlyAbsenceWithTimeOffParams RegHourlyAbsenceWithTimeOffParams
        {
            get
            {
                if ((this.mRegHourlyAbsenceWithTimeOffParams == null))
                {
                    this.mRegHourlyAbsenceWithTimeOffParams = new RegHourlyAbsenceWithTimeOffParams();
                }
                return this.mRegHourlyAbsenceWithTimeOffParams;
            }
        }

        private RegHourlyAbsenceWithTimeOffParams mRegHourlyAbsenceWithTimeOffParams;

        public virtual Select_All_EmployeesAndAddToPlanParams Select_All_EmployeesAndAddToPlanParams
        {
            get
            {
                if ((this.mSelect_All_EmployeesAndAddToPlanParams == null))
                {
                    this.mSelect_All_EmployeesAndAddToPlanParams = new Select_All_EmployeesAndAddToPlanParams();
                }
                return this.mSelect_All_EmployeesAndAddToPlanParams;
            }
        }

        private Select_All_EmployeesAndAddToPlanParams mSelect_All_EmployeesAndAddToPlanParams;


        public virtual SetPlanToDateNewParams SetPlanToDateNewParams
        {
            get
            {
                if ((this.mSetPlanToDateNewParams == null))
                {
                    this.mSetPlanToDateNewParams = new SetPlanToDateNewParams();
                }
                return this.mSetPlanToDateNewParams;
            }
        }

        private SetPlanToDateNewParams mSetPlanToDateNewParams;

        public virtual ChangeAbsPeriodNewParams ChangeAbsPeriodNewParams
        {
            get
            {
                if ((this.mChangeAbsPeriodNewParams == null))
                {
                    this.mChangeAbsPeriodNewParams = new ChangeAbsPeriodNewParams();
                }
                return this.mChangeAbsPeriodNewParams;
            }
        }

        private ChangeAbsPeriodNewParams mChangeAbsPeriodNewParams;



        public virtual SetNewRemanningShiftdateParams SetNewRemanningShiftdateParams
        {
            get
            {
                if ((this.mSetNewRemanningShiftdateParams == null))
                {
                    this.mSetNewRemanningShiftdateParams = new SetNewRemanningShiftdateParams();
                }
                return this.mSetNewRemanningShiftdateParams;
            }
        }

        private SetNewRemanningShiftdateParams mSetNewRemanningShiftdateParams;

        public virtual SetPlanStartDateParams SetPlanStartDateParams
        {
            get
            {
                if ((this.mSetPlanStartDateParams == null))
                {
                    this.mSetPlanStartDateParams = new SetPlanStartDateParams();
                }
                return this.mSetPlanStartDateParams;
            }
        }

        private SetPlanStartDateParams mSetPlanStartDateParams;


        public virtual NewEmployeeDatesParams NewEmployeeDatesParams
        {
            get
            {
                if ((this.mNewEmployeeDatesParams == null))
                {
                    this.mNewEmployeeDatesParams = new NewEmployeeDatesParams();
                }
                return this.mNewEmployeeDatesParams;
            }
        }

        private NewEmployeeDatesParams mNewEmployeeDatesParams;

    }

    /// <summary>
    /// Parameters to be passed into 'SelectReport96_F3'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectReport96_F3Params
    {

        #region Fields
        /// <summary>
        /// Type '{F3}' in 'Panel1' client
        /// </summary>
        public string UIPanel1ClientSendKeys = "{F3}";

        /// <summary>
        /// Type '96' in text box
        /// </summary>
        public string UIItemEditText = "96";

        /// <summary>
        /// Type '{Enter}' in text box
        /// </summary>
        public string UIItemEditSendKeys = "{Enter}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SaveReport'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SaveReportParams
    {
        #region Fields
        /// <summary>
        /// Select 'EmployeeWeekendWorkHoursReport_step2.prnx' in 'File name:' combo box
        /// </summary>
        public string ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_093\");
        public string ReportFileName = "093_excell";
        public string FileType = ".xls";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetPeriod'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetPeriodParams
    {

        #region Fields
        /// <summary>
        /// Type '29.12.2014' in text box
        /// </summary>
        public string UIItemEditText = "29.12.2014";

        /// <summary>
        /// Type '{Tab}' in text box
        /// </summary>
        public string UIItemEditSendTabKey = "{Tab}";

        /// <summary>
        /// Type '03.01.2016' in text box
        /// </summary>
        public string UIItemEditText1 = "03.01.2016";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectLT_F3'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectLT_F3Params
    {

        #region Fields
        /// <summary>
        /// Type '{F3}' in 'Administrasjon' client
        /// </summary>
        public string UIAdministrasjonClientSendKeys = "{F3}";

        /// <summary>
        /// Type 'LØNNS- OG TREKKARTER' in text box
        /// </summary>
        public string UIItemEditText = "LØNNS- OG TREKKARTER";

        /// <summary>
        /// Type '{Enter}' in text box
        /// </summary>
        public string UIItemEditSendKeys = "{Enter}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Select400'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Select400Params
    {

        #region Fields
        /// <summary>
        /// Type '{F3}' in 'Lønns- og trekkarter' client
        /// </summary>
        public string LønnsogtrekkarterClientSendF3 = "{F3}";
        public string LønnsogtrekkarterClientSendEnter = "{ENTER}";

        /// <summary>
        /// Type '400' in text box
        /// </summary>
        public string UIItemEditText = "400";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckSpekter'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckSpekterExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'Checked' property of 'Bruk kode på spekterrapport' check box equals 'True'
        /// </summary>
        public bool UIBrukkodepåspekterrapCheckBoxChecked = true;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Select_ExtraEmployee'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Select_ExtraEmployeeParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.DataStructures.SimpleEmployment' in 'cbEmployment' LookUpEdit
        /// </summary>
        public string UICbEmploymentLookUpEditValueTypeName = "Gatsoft.Gat.DataStructures.SimpleEmployment";

        /// <summary>
        /// Type '' in 'cbEmployment' LookUpEdit
        /// </summary>
        public string UICbEmploymentLookUpEditValueAsString = "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Select_ExtraShiftCode_D'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Select_ExtraShiftCode_DParams
    {

        #region Fields
        /// <summary>
        /// Type '' in 'cbShiftCode' LookUpEdit
        /// </summary>
        public string UICbShiftCodeLookUpEditValueAsString = "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Select_ExtraDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Select_ExtraDateParams
    {

        #region Fields
        /// <summary>
        /// Type '31.05.2015 [SelectionStart]0[SelectionLength]10' in 'pceDate' PopupEdit
        /// </summary>
        public string UIPceDatePopupEditValueAsString = "31.05.2015 [SelectionStart]0[SelectionLength]10";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckDetaljPrUke'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckDetaljPrUkeParams
    {

        #region Fields
        /// <summary>
        /// Select 'Vis detaljer pr uke' check box
        /// </summary>
        public bool UIVisdetaljerprukeCheckBoxChecked = true;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditExtraShift'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditExtraShiftParams
    {

        #region Fields
        /// <summary>
        /// Type '23:00' in 'eTime[1]' text box
        /// </summary>
        public string UIETime1700 = "17:00";
        public string UIETime2300 = "23:00";

        /// <summary>
        /// Type '{Tab}' in 'eTime[1]' text box
        /// </summary>
        public string UIETab = "{Tab}";

        /// <summary>
        /// Type '17:00' in 'eTime[0]' text box
        /// </summary>
        public string UIETime0700 = "07:00";

        /// <summary>
        /// Type 'CrewColumn(Id=1)' in 'cbCrewColumn' LookUpEdit
        /// </summary>
        public string UICbCrewColumnLookUpEditValueAsString = "CrewColumn(Id=1)";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'GoTo3105_Date2'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class GoTo3105_Date2Params
    {

        #region Fields
        /// <summary>
        /// Type '31.05.2015 [SelectionStart]0' in 'pceDate' PopupEdit
        /// </summary>
        public string UIPceDatePopupEditValueAsString = "31.05.2015 [SelectionStart]0";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Select_AbcenceCode'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Select_AbcenceCodeParams
    {

        #region Fields
        /// <summary>
        /// Type '' in 'lueAbsenceCodes' LookUpEdit
        /// </summary>
        public string UILueAbsenceCodesLookUpEditValueAsString = "10";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ChangeRosterIntervall'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ChangeRosterIntervallParams
    {

        #region Fields
        /// <summary>
        /// Type '29.12.2014 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "29.12.2014 [SelectionStart]0[SelectionLength]10";

        /// <summary>
        /// Type '22.03.2015 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "22.03.2015 [SelectionStart]0[SelectionLength]10";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetRosterStartAndWeeks'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetRosterStartAndWeeksParams
    {

        #region Fields
        /// <summary>
        /// Type '10.12.2012 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "31.12.2012 [SelectionStart]0[SelectionLength]10";

        /// <summary>
        /// Type '4 [SelectionStart]0[SelectionLength]1' in 'eNumber[0]' text box
        /// </summary>
        public string UIENumber0EditValueAsString = "4 [SelectionStart]0[SelectionLength]1";
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
    /// Parameters to be passed into 'Insert_DK6_Emp16'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Insert_ShiftCode_EmployeeParams
    {

        #region Fields
        /// <summary>
        /// Type 'dk6' in '[Row]0[Column]RosterCell_5' text box
        /// </summary>

        public string UIRosterCellEditValueAsStringD = "{D}";

        public string UIRosterCellEditValueAsStringDK6 = "{D}{K}{6}";

        public string UIRosterCellEditValueAsStringDK7 = "{D}{K}{7}";

        public string UIRosterCellEditValueAsStringN = "{N}";

        public string UIRosterCellEditValueAsStringHJN = "{H}{J}{N}";

        public string UIRosterCellEditValueAsStringHJN1 = "{H}{J}{N}{1}";

        public string UIRosterCellEditValueAsStringVPV = "{V}{P}{V}";

        public string UIRosterCellEditValueAsStringVPV2 = "{V}{P}{V}{2}";

        /// <summary>
        /// Type '{Tab}' in '[Row]0[Column]RosterCell_5' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEditTab = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Insert_DK7_Emp17'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Insert_DK7_Emp17Params
    {

        #region Fields

        /// <summary>
        /// Type 'Dk7' in '[Row]1[Column]RosterCell_5' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEditValueAsString = "{D}{K}{7}";

        /// <summary>
        /// Type '{Tab}' in '[Row]1[Column]RosterCell_5' text box
        /// </summary>
        public string UIRow1ColumnRosterCellEditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetPlanToDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetPlanToDateParams
    {

        #region Fields
        /// <summary>
        /// Type '03.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "03.01.2016 [SelectionStart]0[SelectionLength]10";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Clear_RosterPlan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Clear_RosterPlanParams
    {

        #region Fields
        /// <summary>
        /// Type 'Control + a' in 'gcRosterPlan' table
        /// </summary>
        public string UIGcRosterPlanTableSendKeys = "a";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'InsertDK6'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class InsertDK6Params
    {

        #region Fields
        /// <summary>
        /// Type '{Right}' in '[Row]0[Column]RosterCell_5' text box
        /// </summary>
        public string UIRow0ColumnRosterRight7x = "{Right 7}";

        /// <summary>
        /// Type '{Tab}' in '[Row]0[Column]RosterCell_8' text box
        /// </summary>
        public string UIRow0ColumnRosterTab7x = "{Tab 7}";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'RenamePlan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class RenamePlanParams
    {

        #region Fields
        /// <summary>
        /// Type 'Kopi_2 av Grunnlag Helgerapportering. [SelectionStart]6' in 'txtName' text box
        /// </summary>
        public string UITxtNameEditValueAsString = "Kopi_2 av Grunnlag Helgerapportering. [SelectionStart]6";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetStartDate_EasterPeriod'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetStartDate_EasterPeriodParams
    {

        #region Fields
        /// <summary>
        /// Type '2015-03-23' in 'leDisplayStartDate' LookUpEdit
        /// </summary>
        public string UILeDisplayStartDateLookUpEditValueAsString = "23.03.2015";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Select_CauseCodeVacancyNurse'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Select_CauseCodeVacancyNurseParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition' in 'drdToSporadic' LookUpEdit
        /// </summary>
        public string UIVakans = "Vakans";

        /// <summary>
        /// Type 'Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition' in 'drdToSporadic' LookUpEdit
        /// </summary>
        public string UISykevikar = "Sykevikar";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Set_EmpPercentTo_0'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Set_EmpPercent_Params
    {

        #region Fields
        /// <summary>
        /// Type '0 [SelectionStart]0[SelectionLength]3' in 'sePositionPercent' text box
        /// </summary>
        public string UISePositionPercent_0 = "0";
        public string UISePositionPercent_25 = "25";
        public string UISePositionPercent_75 = "75";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Check_WinCloseButton_SysCheckWindow'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Check_WinCloseButton_SysCheckWindowExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ControlType' property of 'Close' button equals 'Button'
        /// </summary>
        public string UICloseButtonControlType = "Button";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Check_WinCloseButton_SysMessage'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Check_WinCloseButton_SysMessageExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ControlType' property of 'Close' button equals 'Button'
        /// </summary>
        public string UICloseButtonControlType = "Button";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Check_CloseButton_SysMessage'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Check_CloseButton_SysMessageExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ControlType' property of '&OK' button equals 'Button'
        /// </summary>
        public string UIOKButtonControlType = "Button";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Login'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class LoginParams
    {

        #region Fields
        /// <summary>
        /// Type '********' in text box
        /// </summary>
        public string UIItemEditSendKeys = "VLLA+bJzNf882FWpmiwJPY0v6P7+sGGK";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'TypeInDeparmentValue'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class TypeInDeparmentValueParams
    {

        #region Fields
        /// <summary>
        /// Type '7001 - ' in 'txtFilter' text box
        /// </summary>
        public string UITxtFilterEditValueAsString = "7001 - Helg";

        /// <summary>
        /// Type ' ' in 'txtFilter' text box
        /// </summary>
        public string UITxtFilterEditSendKeys = "{TAB}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'TypeInUserName'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class TypeInUserNameParams
    {

        #region Fields
        /// <summary>
        /// Type 'ASCL' in text box
        /// </summary>
        public string UIItemEditText = "ASCL";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'TypeInPassword'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class TypeInPasswordParams
    {

        #region Fields
        /// <summary>
        /// Type '{Tab}' in text box
        /// </summary>
        public string UIItemEditSendKeysTab = "{Tab}";

        /// <summary>
        /// Type '********' in text box
        /// </summary>
        public string UIItemEditSendKeysPassWord = "VLLA+bJzNf882FWpmiwJPY0v6P7+sGGK";
        #endregion
    }

    /// <summary>
    /// Parameters to be passed into 'Set_AbsTo50Percent'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Set_AbsTo50PercentParams
    {

        #region Fields
        /// <summary>
        /// Type '1' in 'rgPartialAbsenceType' RadioGroup
        /// </summary>
        public int UIRgPartialAbsenceSelectedIndex = 0;

        /// <summary>
        /// Type '50 [SelectionStart]0[SelectionLength]2' in 'eNumber[1]' text box
        /// </summary>
        public string UIENumber1EditValueAsString = "50";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ChangeAbsTodate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ChangeAbsTodateParams
    {

        #region Fields
        /// <summary>
        /// Type '04.07.2015 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate04072015 = "04.07.2015";

        /// <summary>
        /// Type '05.07.2015 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate05072015 = "05.07.2015";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Check_AbsFromDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Check_AbsFromDateExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'Text' property of 'pceDate[0]' PopupEdit equals '04.07.2015'
        /// </summary>
        public string UIPceDate0PopupEditText = "04.07.2015";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AdjustShiftSat'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AdjustShiftSatParams
    {

        #region Fields
        /// <summary>
        /// Type '20:00' in 'eTime[1]' text box
        /// </summary>
        public string UIETime1EditValue2000 = "20:00";
        public string UIETime1EditValue1730 = "17:30";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AdjustShiftSun'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AdjustShiftSunParams
    {

        #region Fields

        /// <summary>
        /// Type '12:00' in 'eTime[1]' text box
        /// </summary>
        public string UIETime1EditValue1200 = "12:00";
        /// <summary>
        /// Type time in 'eTime[1]' text box
        /// </summary>
        public string UIETime1EditValue1000 = "10:00";
        public string UIETime1EditValue0900 = "09:00";

        /// <summary>
        /// Type '{Tab}' in 'eTime[1]' text box
        /// </summary>
        public string UIETime1EditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ContructNewShift'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ContructNewShiftParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Shifts.Calculator.ShiftCodeData' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditShiftDH = "";

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Shifts.Calculator.ShiftCodeData' in 'GSLookUpEdit' LookUpEdit
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Select_EmployeeAbsence'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Select_EmployeeAbsenceParams
    {

        #region Fields
        /// <summary>
        /// Type '{Tab}' in 'lueEmployments' LookUpEdit
        /// </summary>
        public string UILueEmploymentsLookUpEditSendKeysTab = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Select_Emp16Exchange'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Select_Emp16ExchangeParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment' in 'sleEmployment2' LookUpEdit
        /// </summary>
        public string UISleEmployment2LookUpEditValueTypeName = "Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment";

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment' in 'sleEmployment2' LookUpEdit
        /// </summary>
        public string UISleEmployment2LookUpEditValueAsString = "Gatsoft.Gat.BusinessLogic.Exchange.DataStructure.Employment";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Click_DepartmentExchangeEmp'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Click_DepartmentExchangeEmpParams
    {

        #region Fields
        /// <summary>
        /// Type '' in 'sleEmployment1' LookUpEdit
        /// </summary>
        public string UISleEmployment1LookUpEditValueAsString = "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Construct_NewShiftForRemanage'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Construct_NewShiftForRemanageParams
    {

        #region Fields
        /// <summary>
        /// Type '' in 'cbShiftCode' LookUpEdit
        /// </summary>
        public string UICbShiftCodeLookUpEditValueAsString = "D(07:00 - 15:00)";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddNewKont'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddNewKontParams
    {

        #region Fields
        /// <summary>
        /// Select '400 - Lør./Søndagstillegg' in combo box
        /// </summary>
        public string Code400_SatSun = "400 - Lør./Søndagstillegg";

        /// <summary>
        /// Type '3' in text box
        /// </summary>
        public string UIItemEditText = "3";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetExtraToTimeAndColumn'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetExtraToTimeAndColumnParams
    {

        #region Fields

        /// <summary>
        /// Type '{NumPad0}{NumPad0}{Tab}' in 'eTime[0]' text box
        /// </summary>
        public string TimeEditSendTab = "{Tab}";

        /// <summary>
        /// Type 'Gatsoft.Gat.DataModel.CrewColumn' in 'cbCrewColumn' LookUpEdit
        /// </summary>
        public string UICbCrewColumnLookUpEditValueTypeName = "Gatsoft.Gat.DataModel.CrewColumn";

        /// <summary>
        /// Type 'CrewColumn(Id=2)' in 'cbCrewColumn' LookUpEdit
        /// </summary>
        public string UICbCrewColumnAften = "CrewColumn(Id=2)";
        public string UICbCrewColumnNight = "CrewColumn(Id=3)";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AdminVaktkategorierSetUtrykningHhjemmevakt'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AdminVaktkategorierSetUtrykningHhjemmevaktParams
    {

        #region Fields
        /// <summary>
        /// Select 'Hjemmevakt' in combo box
        /// </summary>
        public string UIItemComboBoxHjemmevakt = "Hjemmevakt";
        public string UIItemComboBoxIkkeUtrykning = "Ikke utrykning";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Set_UtrykningsPeriod'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Set_UtrykningsPeriodParams
    {

        #region Fields
        /// <summary>
        /// Type '16:00 [SelectionStart]0[SelectionLength]5' in 'eTime[0]' text box
        /// </summary>
        public string UIETime0EditValueAsString = "16:00 [SelectionStart]0[SelectionLength]5";

        /// <summary>
        /// Type '20:00 [SelectionStart]0[SelectionLength]5' in 'eTime[1]' text box
        /// </summary>
        public string UIETime1EditValueAsString = "20:00 [SelectionStart]0[SelectionLength]5";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CheckOk'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CheckOkExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ClassName' property of 'OK' RibbonBaseButtonItem equals 'RibbonBaseButtonItem'
        /// </summary>
        public string UIOKRibbonBaseButtonItemClassName = "RibbonBaseButtonItem";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetPlanStartdateAndWeeks'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetPlanStartdateAndWeeksParams
    {

        #region Fields
        /// <summary>
        /// Type '29.12.2014 [SelectionStart]0[SelectionLength]10' in 'pceDate[2]' PopupEdit
        /// </summary>
        public string UIPceDate2PopupEditValueAsString = "29.12.2014";

        /// <summary>
        /// Type '15 [SelectionStart]0[SelectionLength]2' in 'eNumber[1]' text box
        /// </summary>
        public string UIENumber1EditValueAsString = "15";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Select_CauseCodeForFiveEmployees'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Select_CauseCodeForFiveEmployeesParams
    {

        #region Fields

        /// <summary>
        /// Type 'Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition' in 'gsleToSporadic' LookUpEdit
        /// </summary>
        public string UIGSykevikar = "Sykevikar" +
            "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Effectuate_CalendarPlan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Effectuate_CalendarPlanParams
    {

        #region Fields
        /// <summary>
        /// Type '15.03.2015 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "15.03.2015";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'RegisterNewEmployee'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class RegisterNewEmployeeParams
    {

        #region Fields
        /// <summary>
        /// Type '123456789' in text box
        /// </summary>
        public string UIItemEditText = "123456789";

        /// <summary>
        /// Type 'N' in text box
        /// </summary>
        public string UIItemEditText1 = "N";

        /// <summary>
        /// Type 'N' in text box
        /// </summary>
        public string UIItemEditText2 = "N";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'RegisterNewEmployee_Position'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class RegisterNewEmployee_PositionParams
    {

        #region Fields
        /// <summary>
        /// Type '01.01.2012 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "01.01.2012";

        /// <summary>
        /// Type '31.12.2099 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "31.12.2099";

        /// <summary>
        /// Type '90 [SelectionStart]0[SelectionLength]4' in 'sePositionPercent' text box
        /// </summary>
        public string UISePositionPercentEditValueAsString = "90 [SelectionStart]0[SelectionLength]4";

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+RuleSetDisplay' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEditValueTypeName = "Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+Ru" +
            "leSetDisplay";

        /// <summary>
        /// Type 'TURNUS - Turnus 35,5t/uke (35,5 t/uke)' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEditValueAsString = "TURNUS - Turnus 35,5t/uke (35,5 t/uke)";

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+PositionCategoryDisplay' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit1ValueTypeName = "Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+Po" +
            "sitionCategoryDisplay";

        /// <summary>
        /// Type 'S - Sykepleier' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit1ValueAsString = "S - Sykepleier";

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+WeaAgreementDisplay' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit2ValueTypeName = "Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+We" +
            "aAgreementDisplay";

        /// <summary>
        /// Type 'Turnus 35,5' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit2ValueAsString = "Turnus 35,5";

        /// <summary>
        /// Type '{Tab}' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEdit2SendKeys = "{Tab}";

        /// <summary>
        /// Type '12121212' in 'teInternalPositionNumber' text box
        /// </summary>
        public string UITeInternalPositionNuEditValueAsString = "12121212";

        /// <summary>
        /// Type '{Tab}' in 'teInternalPositionNumber' text box
        /// </summary>
        public string UITab = "{Tab}";

        /// <summary>
        /// Type 'Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+UnionDisplay' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEditValueTypeName1 = "Gatsoft.Gat.BusinessLogic.DepartmentEmployment.Controller.EmploymentController+Un" +
            "ionDisplay";

        /// <summary>
        /// Type 'NSF - Norsk sykepleierforbund' in 'GSSearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSSearchLookUpEditLookUpEditValueAsString1 = "NSF - Norsk sykepleierforbund";

        /// <summary>
        /// Type 'System.Double' in 'teBaseSalary' text box
        /// </summary>
        public string UITeBaseSalaryEditValueTypeName = "System.Double";

        /// <summary>
        /// Type '400000 [SelectionStart]0[SelectionLength]13' in 'teBaseSalary' text box
        /// </summary>
        public string UITeBaseSalaryEditValueAsString = "400000 [SelectionStart]0[SelectionLength]13";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Create_NewCode_N2'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Create_NewCode_N2Params
    {

        #region Fields
        /// <summary>
        /// Type 'N2' in 'te_NSC_Code' text box
        /// </summary>
        public string UITe_NSC_CodeEditValueAsString = "N2";

        /// <summary>
        /// Type '{Tab}' in 'te_NSC_Code' text box
        /// </summary>
        public string UITe_NSC_CodeEditSendKeyTab = "{Tab}";

        /// <summary>
        /// Type '22:00 [SelectionStart]0[SelectionLength]5' in 'eTime[2]' text box
        /// </summary>
        public string UIETime2EditValueAsString = "22:00";

        /// <summary>
        /// Type '08:00 [SelectionStart]0[SelectionLength]5' in 'eTime[5]' text box
        /// </summary>
        public string UIETime5EditValueAsString = "08:00";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CreateNewPlanFor_NN'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CreateNewPlanFor_NNParams
    {

        #region Fields

        /// <summary>
        /// Type 'Chapter_6_step_1' in 'eName' text box
        /// </summary>
        public string UIENameEditValueAsString = "Chapter_6_step_1";

        /// <summary>
        /// Type '{Tab}' in 'eName' text box
        /// </summary>
        public string UIENameEditSendTab = "{Tab}";

        /// <summary>
        /// Type '02.01.2012 [SelectionStart]0[SelectionLength]10' in 'pceDate[2]' PopupEdit
        /// </summary>
        public string UIPceDate2PopupEditValueAsString = "02.01.2012";
        public DateTime UIPceDate2PopupEditValueAsDateTime = new DateTime(2012, 1, 2);

        /// <summary>
        /// Type '1' in 'eNumber[1]' text box
        /// </summary>
        public string UIENumber1EditValueAsString = "1";

        /// <summary>
        /// Type '03.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "03.01.2016 [SelectionStart]0[SelectionLength]10";

        /// <summary>
        /// Type '1 [SelectionStart]0[SelectionLength]1' in 'eNumber[0]' text box
        /// </summary>
        public string UIENumber0EditValueAsString = "1 [SelectionStart]0[SelectionLength]1";

        /// <summary>
        /// Type 'Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition' in 'gsleToSporadic' LookUpEdit
        /// </summary>
        public string UIGsleToSporadicLookUpEditValueTypeName = "Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition" +
            "";

        /// <summary>
        /// Type 'Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition' in 'gsleToSporadic' LookUpEdit
        /// </summary>
        public string UIGsleToSporadicLookUpEditValueAsString = "Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition" +
            "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'DeleteTT_400_Reg'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class DeleteTT_400_RegParams
    {

        #region Fields
        /// <summary>
        /// Type '{Down}' in 'Endre kontering' client
        /// </summary>
        public string UIEndrekonteringClientSendKeys = "{Down}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ActivateBeregningsregel44_410_420'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ActivateBeregningsregel44_410_420Params
    {

        #region Fields
        /// <summary>
        /// Clear 'ceInactive' check box
        /// </summary>
        public bool UICeInactiveCheckBoxChecked = false;

        /// <summary>
        /// Clear 'ceInactive' check box
        /// </summary>
        public bool UICeInactiveCheckBoxChecked1 = false;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'RegHourlyAbsenceWithTimeOff'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class RegHourlyAbsenceWithTimeOffParams
    {

        #region Fields
        /// <summary>
        /// Select 'chbPabsHourlyAbsence' check box
        /// </summary>
        public bool UIChbPabsHourlyAbsenceCheckBoxChecked = true;

        /// <summary>
        /// Type '{Tab}' in 'chbPabsHourlyAbsence' check box
        /// </summary>
        public string UITab = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'Select_All_EmployeesAndAddToPlan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class Select_All_EmployeesAndAddToPlanParams
    {

        #region Fields
        /// <summary>
        /// Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
        /// </summary>
        public string UIGcDepartmentEmployeeTableSendKeys = "{Down}";

        /// <summary>
        /// Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
        /// </summary>
        public string UIGcDepartmentEmployeeTableSendKeys1 = "{Down}";

        /// <summary>
        /// Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
        /// </summary>
        public string UIGcDepartmentEmployeeTableSendKeys2 = "{Down}";

        /// <summary>
        /// Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
        /// </summary>
        public string UIGcDepartmentEmployeeTableSendKeys3 = "{Down}";

        /// <summary>
        /// Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
        /// </summary>
        public string UIGcDepartmentEmployeeTableSendKeys4 = "{Down}";

        /// <summary>
        /// Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
        /// </summary>
        public string UIGcDepartmentEmployeeTableSendKeys5 = "{Down}";

        /// <summary>
        /// Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
        /// </summary>
        public string UIGcDepartmentEmployeeTableSendKeys6 = "{Down}";

        /// <summary>
        /// Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
        /// </summary>
        public string UIGcDepartmentEmployeeTableSendKeys7 = "{Down}";

        /// <summary>
        /// Type 'Shift + {Down}' in 'gcDepartmentEmployees' table
        /// </summary>
        public string UIGcDepartmentEmployeeTableSendKeys8 = "{Down}";

        /// <summary>
        /// Type 'Gatsoft.Gat.RosterPlan.EmployeeManager.UI.ViewModels.Data.OvertimeCodeViewModel' in 'SearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UISearchLookUpEditLookUpEditValueTypeName = "Gatsoft.Gat.RosterPlan.EmployeeManager.UI.ViewModels.Data.OvertimeCodeViewModel";

        /// <summary>
        /// Type 'F - Ferievikar' in 'SearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UISearchLookUpEditLookUpEditValueAsString = "F - Ferievikar";

        /// <summary>
        /// Type 'F - Ferievikar' in 'SearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UISearchLookUpEditLookUpEditValueAsString1 = "F - Ferievikar";

        /// <summary>
        /// Type 'F - Ferievikar' in 'SearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UISearchLookUpEditLookUpEditValueAsString2 = "F - Ferievikar";

        /// <summary>
        /// Type 'F - Ferievikar' in 'SearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UISearchLookUpEditLookUpEditValueAsString3 = "F - Ferievikar";

        /// <summary>
        /// Type 'F - Ferievikar' in 'SearchLookUpEdit' LookUpEdit
        /// </summary>
        public string UISearchLookUpEditLookUpEditValueAsString4 = "F - Ferievikar";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetPlanToDateNew'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetPlanToDateNewParams
    {

        #region Fields
        /// <summary>
        /// Type ' [SelectionStart]0' in 'pceDate' DateTimeEdit
        /// </summary>
        public DateTime UIPceDateDateTimeEditValueAsDate = new DateTime(2016, 1, 3);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ChangeAbsPeriodNew'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class ChangeAbsPeriodNewParams
    {

        #region Fields
        public DateTime UIPceDate04072015asDateTime = new DateTime(2015, 7, 4);
        public DateTime UIPceDate05072015asDateTime = new DateTime(2015, 7, 5);

        /// <summary>
        /// Type '{Tab}' in 'pceDate' DateTimeEdit
        /// </summary>
        public string Tab = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetNewRemanningShiftdate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetNewRemanningShiftdateParams
    {

        #region Fields
        /// <summary>
        /// Type '{NumPad2}{NumPad0}{NumPad7}{NumPad2}{NumPad0}{NumPad1}{NumPad6}{Tab}' in 'pceDate' DateTimeEdit
        /// </summary>
        public string Tab = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetPlanStartDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetPlanStartDateParams
    {

        #region Fields
        /// <summary>
        /// Type ' [SelectionStart]0' in 'pceDate' DateTimeEdit
        /// </summary>
        public DateTime UIPceDate2PopupEditValueAsDateTime = new DateTime(2014, 12, 29);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'NewEmployeeDates'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class NewEmployeeDatesParams
    {

        #region Fields
        /// <summary>
        /// Type '2016-12-19 [SelectionStart]0' in 'pceDate' DateTimeEdit
        /// </summary>
        public string UIPceDateDateTimeEditValueAsString = "2016-12-19 [SelectionStart]0";

        /// <summary>
        /// Type '{Tab}' in 'pceDate' DateTimeEdit
        /// </summary>
        public string UIPceDateDateTimeEditSendKeys = "{Tab}";

        /// <summary>
        /// Type ' [SelectionStart]0' in 'pceDate' DateTimeEdit
        /// </summary>
        public string UIPceDateDateTimeEditValueAsString1 = " [SelectionStart]0";
        #endregion
    }
    #endregion
}
