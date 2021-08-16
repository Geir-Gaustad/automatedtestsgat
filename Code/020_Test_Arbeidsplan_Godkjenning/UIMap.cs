namespace _020_Test_Arbeidsplan_Godkjenning
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
    using System.Threading;
    using System.Globalization;
    using CommonTestData;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        public string ReportFilePath;
        public string ReportFileName = "020_excel";
        public string FileType = ".xls";
        #endregion

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_020_Godkjenning\");

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
        //Step 2
        public List<string> SelectAdministrationAndSetDemands(string searchString, string step)
        {
            var errorList = new List<string>();

            try
            {
                UICommon.SelectFromAdministration(searchString, true);

                if (step == "2")
                {
                    UICommon.CreateRosterplanDemand("5000", "Leder");
                    errorList.AddRange(CheckDemands_Step2());
                }
                else if (step == "18")
                {
                    UICommon.CreateRosterplanDemand("5080", "Leder", true, true, true);
                    UICommon.CreateRosterplanDemand("5080", "Tillitsvalgt", true, true, true);
                    UICommon.CreateRosterplanDemand("5080", "Koordinator", true, true, true);
                    errorList.AddRange(CheckDemands_Step18());

                }
                UICommon.CloseRosterplanDemandWindow();
                //UICommon.ClearAdministrationSearchString();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step_" + step + ": " + e.Message);
            }

            return errorList;
        }

        private List<string> CheckDemands_Step2()
        {               /*@"Gjelder fra avdeling	Representasjonstype
                            1030 - Ortopedisk avdeling	Leder
                            1030 - Ortopedisk avdeling	Tillitsvalgt
                            5030 - Ønskeplan	Tillitsvalgt
                            5040 - Masterplan/masterliste	Leder
                            5040 - Masterplan/masterliste	Tillitsvalgt
                            5110 - Diverse	Tillitsvalgt
                            5110 - Diverse	Leder
                            5175 - Ytelse 2	Tillitsvalgt
                            5175 - Ytelse 2	Leder
                            5000 - ARBEIDSPLANKLINIKKEN	Leder";*/

            var errorList = new List<string>();

            Clipboard.Clear();
            CopyLastLineInDemandWindow();

            if (Clipboard.ContainsText())
            {
                var compareText = @"Gjelder fra avdeling	Representasjonstype
5000 - ARBEIDSPLANKLINIKKEN	Leder";

                var text = Clipboard.GetText(TextDataFormat.Text);
                Clipboard.Clear();

                try
                {
                    Assert.AreEqual(compareText, text);
                    TestContext.WriteLine("Demand step2 Ok!");
                }
                catch (Exception e)
                {
                    errorList.Add("Error checking demand step2: " + e.Message);
                }
            }

            return errorList;
        }

        private List<string> CheckDemands_Step18()
        {               /*@"Gjelder fra avdeling	Representasjonstype
                            1030 - Ortopedisk avdeling	Leder
                            1030 - Ortopedisk avdeling	Tillitsvalgt
                            5030 - Ønskeplan	Tillitsvalgt
                            5040 - Masterplan/masterliste	Leder
                            5040 - Masterplan/masterliste	Tillitsvalgt
                            5110 - Diverse	Tillitsvalgt
                            5110 - Diverse	Leder
                            5175 - Ytelse 2	Tillitsvalgt
                            5175 - Ytelse 2	Leder
                            5000 - ARBEIDSPLANKLINIKKEN	Leder
                            5080 - Godkjenning	Leder
                            5080 - Godkjenning	Tillitsvalgt
                            5080 - Godkjenning	Koordinator*/

            var errorList = new List<string>();

            Clipboard.Clear();
            Copy1of3LastLineInDemandWindow();

            if (Clipboard.ContainsText())
            {
                var compareText = @"Gjelder fra avdeling	Representasjonstype
5080 - Godkjenning	Leder";

                var text = Clipboard.GetText(TextDataFormat.Text);
                Clipboard.Clear();

                try
                {
                    Assert.AreEqual(compareText, text);
                    TestContext.WriteLine("Demand firstline step18 Ok!");
                }
                catch (Exception e)
                {
                    errorList.Add("Error checking demand step18: " + e.Message);
                }
            }

            Copy2of3LastLineInDemandWindow();

            if (Clipboard.ContainsText())
            {
                var compareText = @"Gjelder fra avdeling	Representasjonstype
5080 - Godkjenning	Tillitsvalgt";

                var text = Clipboard.GetText(TextDataFormat.Text);
                Clipboard.Clear();

                try
                {
                    Assert.AreEqual(compareText, text);
                    TestContext.WriteLine("Demand secondline step18 Ok!");
                }
                catch (Exception e)
                {
                    errorList.Add("Error checking demand step18: " + e.Message);
                }
            }

            Copy3of3LastLineInDemandWindow();

            if (Clipboard.ContainsText())
            {
                var compareText = @"Gjelder fra avdeling	Representasjonstype
5080 - Godkjenning	Koordinator";

                var text = Clipboard.GetText(TextDataFormat.Text);
                Clipboard.Clear();

                try
                {
                    Assert.AreEqual(compareText, text);
                    TestContext.WriteLine("Demand thirdline step18 Ok!");
                }
                catch (Exception e)
                {
                    errorList.Add("Error checking demand step18: " + e.Message);
                }
            }

            return errorList;
        }

        public List<string> SelectAdministrationAndSetRepresentations(string searchString, string step)
        {
            var errorList = new List<string>();
            var unionList = new List<string>();
            var repList = new List<string>();

            UICommon.SelectFromAdministration(searchString, true);
            try
            {
                if (step == "3")
                {
                    unionList = new List<string>() { "2", "6", "7" };
                    repList = new List<string>() { "Brynjulf" };
                    UICommon.CreateRosterplanRepresentation("Leder 5000", "5000", "Leder", unionList, repList);
                    CheckRepresentations_Step3();
                }
                else if (step == "19")
                {
                    unionList = new List<string>() { "2", "3", "6", "7" };
                    repList = new List<string>() { "Bastant" };
                    UICommon.CreateRosterplanRepresentation("Leder 5080", "5080", "Leder", unionList, repList);

                    repList = new List<string>() { "Gunnar" };
                    UICommon.CreateRosterplanRepresentation("Koordinator", "5080", "Koordinator", unionList, repList);

                    unionList = new List<string>() { "6", "7" };
                    repList = new List<string>() { "Petronella" };
                    UICommon.CreateRosterplanRepresentation("Tillitsvalgt NSF 5080", "5080", "Tillitsvalgt", unionList, repList);

                    unionList = new List<string>() { "2", "3" };
                    repList = new List<string>() { "Vigdis" };
                    UICommon.CreateRosterplanRepresentation("Tillitsvalgt FAG KFO 5080", "5080", "Tillitsvalgt", unionList, repList);

                    CheckRepresentations_Step19();
                }
                else if (step == "55")
                {
                    UICommon.EditRosterplanRepresentation("Tillitsvalgt FAG KFO 5080", new List<string> { "Mikkel" });
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error representations(step" + step + "): " + e.Message);
            }

            UICommon.CloseRosterplanRepresentationWindow();
            //UICommon.ClearAdministrationSearchString();

            return errorList;
        }

        public List<string> AddEmployeesAndAshiftsToPlan()
        {
            var errorList = new List<string>();

            //Add employees
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            UICommon.SelectAllEmployeesAddEmployeesWindow();
            RemoveBastantFromList();
            UICommon.ClickOkAddEmployeesWindow();
            SetFagforeningGladKFO();
            UICommon.ClickOkEmployeesWindow();

            //Add A-shifts
            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                AddCalendarplanAshifts();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 5: " + e.Message);
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();

            return errorList;
        }

        public void OpenSettingsAndSetReadyForApprival(bool selectPlantab = true)
        {
            UICommon.OpenRosterplanSettings(selectPlantab);
            UICommon.UIMapVS2017.SetPlanReadyForApproval(true);
            UICommon.ClickOkRosterplanSettings();
        }

        public List<string> CalcultateFTTStep7()
        {
            var errorList = new List<string>();
            UICommon.CalculateFTT();

            try
            {
                CheckFTTCalculationsStep7();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 7: " + e.Message);
            }

            return errorList;
        }

        public List<string> ClickTransfereAndCheckTransfereFTTDisabled()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickTransfereFTTButton();
            SelectAllEmpsInTransfereWindow();
            errorList.AddRange(UICommon.UIMapVS2017.CheckTransfereFTTDisabled("8"));

            return errorList;
        }

        public List<string> ClickEffectuateAndCheckEffectuationDisabled()
        {
            var errorList = new List<string>();
            UICommon.UIMapVS2017.ClickCancelInTransfereWindow();
            UICommon.EffectuateRoasterplanNextPeriod();
            UICommon.UIMapVS2017.OpenRegStatusWindow();

            try
            {
                CheckRegStatusStep9();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 9: " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRegStatusWindow();

            try
            {
                UICommon.UIMapVS2017.CheckEffectuationDisabled();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 9: " + e.Message);
            }

            UICommon.UIMapVS2017.ClickCancelEffectuationWindow();

            return errorList;
        }

        public List<string> ClickCellInApprovalLederColumn(bool checkOnlyFirstCell = false)
        {
            #region Variable Declarations
            var errorList = new List<string>();            
            var appTable = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable;
            DXComboBox combo = appTable.UIRow0ColumnreqColLedeComboBox;
            #endregion

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);
            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);


            var rows = appTable.Views[0].RowCount;
            for (int i = 0; i < rows; i++)
            {
                try
                {
                    CheckApprovalDropdown(i.ToString());
                    Mouse.Click(combo);
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step 10: " + e.Message);
                }

                if (checkOnlyFirstCell)
                    break;
            }

            return errorList;
        }

        public List<string> ApproveFjongRejectKaspersen()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                CloseRosterPlan();
                SelectRosterPlan("Godkjenning Leder 5000");
                UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

                Playback.Wait(1000);
                UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
                ApproveFjong();
                RejectKaspersen();

                if (!CheckRowRowToApprove())
                    throw new Exception("Unexpected number of rows");

            }
            catch (Exception e)
            {
                errorList.Add("Error in step 11-13: " + e.Message);
            }

            return errorList;
        }
        public bool CheckRowRowToApprove()
        {
            #region Variable Declarations
            var table = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable;
            #endregion
            var count = table.Views[0].RowCount;

            return count == 7;
        }

        private List<string> RejectKaspersen()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXCell uIItemCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable.UIItemCell;
            //uIItemCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcApprovalGridControlCell[View]gvApproval[Row]1[Column]reqColLeder";

            DXComboBox uIRow1ColumnreqColLedeComboBox = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable.UIRow1ColumnreqColLedeComboBox;
            DXTextEdit uIMeCommentEdit = this.UISkrivenkommentartilaWindow.UIMeCommentEdit;
            DXButton uIOKButton = this.UISkrivenkommentartilaWindow.UIOKButton;
            #endregion

            // Click cell
            Mouse.Click(uIItemCell, new Point(38, 7));

            // Select 'System.Int32' in '[Row]1[Column]reqColLeder' combo box
            //ValueTypeName
            uIRow1ColumnreqColLedeComboBox.ValueTypeName = "System.Int32";

            // Select '2' in '[Row]1[Column]reqColLeder' combo box
            //ValueAsString
            uIRow1ColumnreqColLedeComboBox.ValueAsString = "2";

            try
            {
                UICommon.UIMapVS2017.CheckOkDisabledInRejectCommentWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 13(Ok button enabled): " + e.Message);
            }

            // Type 'Avvises' in 'meComment' text box
            //ValueAsString
            uIMeCommentEdit.ValueAsString = "Avvises";

            // Click 'Ok' button
            Mouse.Click(uIOKButton);

            return errorList;
        }

        public List<string> ApproveRow5And6()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                SelectRow5And6();
                UICommon.UIMapVS2017.ApproveApprovalInApprovalTab();
                CheckRow4And5IsApproved();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 14: " + e.Message);
            }

            return errorList;
        }

        public List<string> RejectRow7To9()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                SelectRow7To9();
                UICommon.UIMapVS2017.RejectApprovalInApprovalTab(false);
                CheckRow7To9IsRejected();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 15: " + e.Message);
            }

            return errorList;
        }

        public List<string> RepealRow8And9()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                SelectRow8And9();
                UICommon.UIMapVS2017.RepealOwnApprovalInApprovalTab(false, false);
                CheckRow8To9IsRepealed();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 16: " + e.Message);
            }

            return errorList;
        }

        public List<string> LoginAsAsclAndSetDemands()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                UICommon.SelectPlanTabRosterplan();
                CloseRosterPlan();
                CloseGat();
                StartGat(false, true, "");
                SelectAdministrationAndSetDemands("Godkjenningskrav + arbeidsplan", "18");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 17-18: " + e.Message);
            }

            return errorList;
        }

        public List<string> SetAsclAndSetRepresentations()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                SelectAdministrationAndSetRepresentations("Representasjoner", "19");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 19: " + e.Message);
            }

            return errorList;
        }

        public List<string> LoginAsBjarneAndGoToRosterplanTab()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                CloseGat();
                StartGat(false, true, "BJARNE");
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 20: " + e.Message);
            }

            return errorList;
        }

        public List<string> AddEmployeesTo2024Plan()
        {
            var errorList = new List<string>();

            //Add employees
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEmployeesButtonRosterplan();
            UICommon.ClickEmployeesButtonInEmployeeWindow();
            AddFiveEmpsFromList();

            UICommon.ClickOkAddEmployeesWindow();
            //RemoveFagforeningSvendsen();
            ChangeFagforeningSvendsen();

            UICommon.ClickOkEmployeesWindow();
            Playback.Wait(3000);

            return errorList;
        }
        public List<string> AddNshiftsTo2024Plan()
        {
            var errorList = new List<string>();

            //Add N-shifts
            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                AddCalendarplanNshifts();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 21: " + e.Message);
            }

            UICommon.ClickOKEditRosterPlanFromPlantab();

            return errorList;
        }
        public List<string> LoginAsVigdisAndGoToRosterplanTab()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            CloseRosterPlan();
            CloseGat();
            StartGat(false, true, "VIGDIS");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
            try
            {
                if (CheckRosterPlanExists("Februar 2024"))
                    throw new Exception("Plan Februar 2024 is Accessible!");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 24: " + e.Message);
            }

            return errorList;
        }
        public List<string> LoginAsBjarneAndGoToRosterplanTabToCheckPlanExists()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            CloseGat();
            StartGat(false, true, "BJARNE");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            try
            {
                if (!CheckRosterPlanExists("Februar 2024") || !CheckRosterPlanExists("Godkjenning Leder 5000"))
                    throw new Exception("Plan Februar 2024/Godkjenning Leder 5000 is not Accessible!");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 25: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step26()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            SelectRosterPlan("Februar 2024");
            OpenSettingsAndSetReadyForApprival(true);
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
            try
            {
                CheckEmpUnionsStep26();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 26: " + e.Message);
            }

            errorList.AddRange(ClickCellInApprovalLederColumn(true));
            errorList.AddRange(CheckApprovalRightsStep26());

            return errorList;
        }
        private List<string> CheckApprovalRightsStep26()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXCell koordinatorCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable.UIItemCell3;
            DXCell tillitsvalgtCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable.UIItemCell11;
            #endregion

            for (int i = 0; i < 5; i++)
            {
                koordinatorCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcApprovalGridControlCell[View]gvApproval[Row]" + i.ToString() + "[Column]reqColKoordinator";
                // Click cell
                Mouse.Click(koordinatorCell);

                try
                {
                    // Verify that the 'ControlType' property of cell equals 'Cell'
                    Assert.AreEqual("Cell", koordinatorCell.ControlType.ToString());

                    // Verify that the 'Text' property of cell equals ''
                    Assert.AreEqual("", koordinatorCell.Text);
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step 26(Koordinator): " + e.Message);
                }

            }

            for (int i = 0; i < 5; i++)
            {
                tillitsvalgtCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcApprovalGridControlCell[View]gvApproval[Row]" + i.ToString() + "[Column]reqColTillitsvalgt";

                Mouse.Click(tillitsvalgtCell);

                try
                {
                    // Verify that the 'ControlType' property of cell equals 'Cell'
                    Assert.AreEqual("Cell", tillitsvalgtCell.ControlType.ToString());

                    // Verify that the 'Text' property of cell equals ''
                    Assert.AreEqual("", tillitsvalgtCell.Text);
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step 26(Tillitsvalgt): " + e.Message);
                }
            }

            return errorList;
        }
        public List<string> ApproveAllLinesStep27()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            UICommon.UIMapVS2017.SelectAllLinesInRosterplanApprovalTab();
            UICommon.UIMapVS2017.ApproveApprovalInApprovalTab();
            try
            {
                CheckAllLederLinesApprovedStep27();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 27: " + e.Message);
            }

            return errorList;
        }
        public List<string> LoginAsOyvindAndGoToRosterplanTab()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            CloseRosterPlan();
            CloseGat();
            StartGat(false, true, "ØØ");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
            try
            {
                if (CheckRosterPlanExists(""))
                    throw new Exception("Plans are Accessible!");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 28: " + e.Message);
            }

            return errorList;
        }

        public List<string> LoginAsGunnarAndOpenPlanFebruar2024()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            CloseGat();
            StartGat(false, true, "GUNNAR");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
            SelectRosterPlan("Februar 2024", false);
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();
            try
            {
                CheckApprovedLinesDisabled();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 29: " + e.Message);
            }
            UICommon.ClickCancelEditRosterPlanFromPlantab();

            return errorList;
        }
        public List<string> CheckEmployeeSettingsDisabled()
        {
            var errorList = new List<string>();

            try
            {
                OpenGustavssonEmpSettings();
                CheckWeekendPatternDisabledInEmployeeSettings();
                ClickRosterplanLayoutTab();
                OpenLinesInEmployeeSettings();
                CheckLinesDisabledInEmployeeSettings();
                UICommon.UIMapVS2017.ClickCancelInRosterplanEmployeeSettings();
                TestContext.WriteLine("Step30 Ok!");
            }
            catch (Exception e)
            {
                errorList.Add("Error step 30: " + e.Message);
            }

            return errorList;
        }
        public List<string> CheckEmployeeLineSettingsDisabled()
        {
            var errorList = new List<string>();

            try
            {
                OpenGustavssonEmpLineSettings();
                CheckLinesDisabledInEmployeeLineSettings();
                UICommon.UIMapVS2017.ClickCancelInRosterplanEmployeeLineSettings();
                TestContext.WriteLine("Step31 Ok!");
            }
            catch (Exception e)
            {
                errorList.Add("Error step 31: " + e.Message);
            }

            return errorList;
        }
        public List<string> LoginAsPetronellaAndGoToRosterplanTab()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            CloseRosterPlan();
            CloseGat();
            StartGat(false, true, "PETRONELLA");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
            try
            {
                if (!CheckRosterPlanExists("Februar 2024", 1))
                    throw new Exception("Plan Februar 2024 are not Accessible or more plans than expected are accessible!");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 32: " + e.Message);
            }

            return errorList;
        }
        public List<string> OpenFebruar2024AndSelectApprovalTab()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            SelectRosterPlan("Februar 2024", false);
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
            try
            {
                CheckApprovalRightsStep33();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 33: " + e.Message);
            }

            return errorList;
        }
        private List<string> CheckApprovalRightsStep33()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXCell tillitsvalgtCell = UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient1.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable.UIItemCell;
            #endregion


            for (int i = 0; i < 2; i++)
            {
                tillitsvalgtCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcApprovalGridControlCell[View]gvApproval[Row]" + i.ToString() + "[Column]reqColTillitsvalgt";
                Mouse.Click(tillitsvalgtCell);

                try
                {
                    // Verify that the 'ControlType' property of cell equals 'Cell'
                    Assert.AreEqual("Cell", tillitsvalgtCell.ControlType.ToString());

                    // Verify that the 'Text' property of cell equals ''
                    Assert.AreEqual("", tillitsvalgtCell.Text);
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step 33(Tillitsvalgt): " + e.Message);
                }
            }

            return errorList;
        }
        public List<string> ApproveLinesStep34()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            var table = UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient1.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable;
            #endregion

            try
            {
                UICommon.SelectRosterplanPlanTab();
                CloseRosterPlan();
                SelectRosterPlan("Februar 2024", false);
                UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

                UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
                Approve2TillitsvalgtLinesStep34();

                if (table.Views[0].RowCount != 2)
                    throw new Exception("Unexpected rowcount");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 33: " + e.Message);
            }

            return errorList;
        }
   
        public List<string> LoginAsVigdisAndOpenRosterplan2024()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            var table = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient1.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable;
            #endregion

            UICommon.CloseRosterplanFromHomeTab();
            CloseGat();
            StartGat(false, true, "VIGDIS");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);

            SelectRosterPlan("Februar 2024", false);
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
            try
            {
                ApproveGustavssonAndSvendsenRejectKaspersenStep36();
                if (table.Views[0].RowCount != 3)
                    throw new Exception("Unexpected rowcount");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 36: " + e.Message);
            }

            return errorList;
        }
        public List<string> LoginAsBjarneAndOpenRosterplan2024()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            UICommon.CloseRosterplanFromHomeTab();
            CloseGat();
            StartGat(false, true, "BJARNE");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);

            SelectRosterPlan("Februar 2024", false);
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
            UICommon.UIMapVS2017.SelectAllLinesInRosterplanApprovalTab();
            UICommon.UIMapVS2017.RepealApprovalInApprovalTab();

            try
            {
                CheckRowsAreRepealed();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 37: " + e.Message);
            }

            return errorList;
        }
        public List<string> AddAshiftsToPlanStep38()
        {
            var errorList = new List<string>();

            //Add A-shifts  
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();

            try
            {
                Playback.Wait(1000);
                AddCalendarplanAshiftsStep38();
                Playback.Wait(1000);
                UICommon.ClickOKEditRosterPlanFromPlantab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 38: " + e.Message);
            }

            return errorList;
        }

        public List<string> RosterplanApprovalLogStep39()
        {
            var errorList = new List<string>();

            //Open log
            Playback.Wait(2000);
            UICommon.UIMapVS2017.OpenRosterplanApprovalLog();

            errorList.AddRange(CheckApprovalInLogStep39());

            //Close log
            UICommon.UIMapVS2017.CloseRosterplanApprovalLog();

            return errorList;
        }
        
        private List<string> CheckApprovalInLogStep39()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            var node = this.UISøkingidatabaseloggWindow.UIPaResultsClient.UITlContextsTreeList.UINode16TreeListNode;
            var nodeCount = this.UISøkingidatabaseloggWindow.UIPaResultsClient.UITlContextsTreeList.Nodes.Length;
            string UIBJARNETreeListCellText = "BJARNE";
            List<string> infoList = new List<string>();
            #endregion
            
            for (int i = (nodeCount - 5); i < nodeCount; i++)
            {
                node.SearchProperties[DXTestControl.PropertyNames.Name] = "Node" + i.ToString();
                node.UIBJARNETreeListCell.SearchProperties[DXTestControl.PropertyNames.Name] = "colContextUser";
                node.UIEndringerforlinjemedTreeListCell.SearchProperties[DXTestControl.PropertyNames.Name] = "colContextDescription";
                var user = node.UIBJARNETreeListCell.ValueAsString;
                var info = node.UIEndringerforlinjemedTreeListCell.ValueAsString;

                try
                {
                    Assert.AreEqual(UIBJARNETreeListCellText, user);
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step 39: " + e.Message);
                }

                infoList.Add(info);
            }

            if(infoList.FindAll(info => info.Contains("Endringer for linje med id") && info.Contains("knyttet til Gustavsson, Robert")).Count < 1)
            {
                errorList.Add("Error in step 39: Endringer for linje knyttet til Gustavsson, Robert");
            }
            if (infoList.FindAll(info => info.Contains("Endringer for linje med id") && info.Contains("knyttet til Kaspersen, Kasper")).Count < 1)
            {
                errorList.Add("Error in step 39: Endringer for linje knyttet til Kaspersen, Kasper");
            }
            if (infoList.FindAll(info => info.Contains("Endringer for linje med id") && info.Contains("knyttet til Lassen, Liselotte")).Count < 1)
            {
                errorList.Add("Error in step 39: Endringer for linje knyttet til Lassen, Liselotte");
            }
            if (infoList.FindAll(info => info.Contains("Endringer for linje med id") && info.Contains("knyttet til Mikkelsen, Mikkel")).Count < 1)
            {
                errorList.Add("Error in step 39: Endringer for linje knyttet til Mikkelsen, Mikkel");
            }
            if (infoList.FindAll(info => info.Contains("Endringer for linje med id") && info.Contains("knyttet til Svendsen, Svanhild")).Count < 1)
            {
                errorList.Add("Error in step 39: Endringer for linje knyttet til Svendsen, Svanhild");
            }

            return errorList;
        }

        public List<string> CalculateAndTransfereFTTStep40()
        {
            var errorList = new List<string>();

            //FTT
            UICommon.CalculateFTT(true);
            //Transfere FTT
            UICommon.UIMapVS2017.ClickTransfereFTTButton();
            UICommon.UIMapVS2017.SetTransfereFTTDate("29.01.2024");
            SelectAllEmpsInTransfereWindow();

            try
            {
                CheckDataInTransefereWindowStep40();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 40: " + e.Message);
            }

            //Step 41
            UICommon.UIMapVS2017.ClickTransfereInTransfereWindow();
            Playback.Wait(1000);
            UICommon.UIMapVS2017.ClickCloseExportLogWindow();

            return errorList;
        }
        public List<string> Effectuate2024PlanStep42()
        {
            var errorList = new List<string>();
            UICommon.EffectuateRoasterplanNextPeriod();

            try
            {
                CheckNoNotifications();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 42: " + e.Message);
            }

            try
            {
                //Step 43
                UICommon.EffectuateRosterplanLines(false);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 43: " + e.Message);
            }

            return errorList;
        }

        public List<string> Effectuate2024PlanStep44()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickDeleteEffectuatuationAndSelectAllEmployeesFromPayrollCalculationWindow();
                CheckNoNotificationsDeleteEffectuationFromPayrollCalculationWindow();
                UICommon.UIMapVS2017.DeleteEffectuationFromPayrollCalculationWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 44: " + e.Message);
            }

            return errorList;
        }

        public List<string> EditApprovals()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);
            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
            try
            {
                EditApprovalsStep45();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 45: " + e.Message);
            }

            return errorList;
        }

        public List<string> RemoveAshiftsFromPlanStep46()
        {
            var errorList = new List<string>();

            //Remove A-shifts  
            UICommon.ClickEditRosterPlanFromPlantab();
            try
            {
                RemoveCalendarplanAshiftsStep46();
                UICommon.ClickOKEditRosterPlanFromPlantab();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 46(Delete A-shifts): " + e.Message);
            }

            //FTT
            UICommon.CalculateFTT(true);
            //Transfere FTT
            UICommon.UIMapVS2017.ClickTransfereFTTButton();
            SelectAllEmpsInTransfereWindow();
            errorList.AddRange(UICommon.UIMapVS2017.CheckTransfereFTTDisabled("46"));

            return errorList;
        }

        public List<string> CancelTransfereAndEffectuateStep47()
        {
            var errorList = new List<string>();

            Playback.Wait(2000);
            UICommon.UIMapVS2017.ClickCancelInTransfereWindow();

            UICommon.EffectuateRoasterplanNextPeriod();
            UICommon.UIMapVS2017.OpenRegStatusWindow();
            try
            {
                UICommon.UIMapVS2017.CheckEffectuationButtonDisabled();
                CheckRegMessagesStep47();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 47: " + e.Message);
            }

            UICommon.UIMapVS2017.CloseRegStatusWindow();

            return errorList;
        }
        public List<string> CloseEffectuationWindowAndCloseGatStep48()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.ClickCancelEffectuationWindow();
                CloseRosterPlan();
                CloseGat();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 48: " + e.Message);
            }

            return errorList;
        }
        public List<string> LoginAsPetronellaAndOpenRosterplan2024()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            StartGat(false, true, "PETRONELLA");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
            try
            {
                SelectRosterPlan("Februar 2024", false);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 49: " + e.Message);
            }

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);
            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);

            try
            {
                //Todo: kun 2 linjer nå pga av ikke rettigheter til å fjerne fagforening
                UICommon.UIMapVS2017.SelectAllLinesInRosterplanApprovalTabPlace4();
                UICommon.UIMapVS2017.ApproveApprovalInApprovalTabPlace4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 49: " + e.Message);
            }

            return errorList;
        }
        public List<string> RepealLine3And4Step50()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                SelectRow1();
                UICommon.UIMapVS2017.RepealOwnApprovalInApprovalTab(false, true);
                SelectRow4AndRepealWithClickInCell();
                CheckRow3And4IsRepealed();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 50: " + e.Message);
            }

            return errorList;
        }
        public List<string> ApproveLine3and4AndLoginAsVigdisStep51()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                UICommon.UIMapVS2017.SelectAllLinesInRosterplanApprovalTabPlace4();
                UICommon.UIMapVS2017.ApproveApprovalInApprovalTabPlace4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 51: " + e.Message);
            }

            UICommon.SelectRosterplanPlanTab();
            CloseRosterPlan();
            CloseGat();
            StartGat(false, true, "VIGDIS");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
            try
            {
                SelectRosterPlan("Februar 2024", false);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 51: " + e.Message);
            }

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);
            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
            try
            {
                SelectGustavssonAndKaspersenLines();
                UICommon.UIMapVS2017.ApproveApprovalInApprovalTabPlace4();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 51: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step52()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                RepealGustavssonLine1();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 52: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step53()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            UICommon.SelectPlanTabRosterplan();
            CloseRosterPlan();
            try
            {
                SelectRosterPlan("Februar 2024", false);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 53: " + e.Message);
            }

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);
            UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
            try
            {
                CheckWarningWhenClicingCaspersenLine2();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 53: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step54()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                UICommon.SelectPlanTabRosterplan();
                CloseRosterPlan();
                CloseGat();
                StartGat(false, true, "");
                ChangeDepartment("5080");
                UICommon.SelectFromAdministration("brukeradministrasjon -opp", true);
                ChangeMikkelGroupToTillitsvalgt();
                //UICommon.ClearAdministrationSearchString();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 54: " + e.Message);
            }

            return errorList;
        }

        /// <summary>
        /// ChangeMikkelGroupToTillitsvalgt - Use 'ChangeMikkelGroupToTillitsvalgtParams' to pass parameters into this method.
        /// </summary>
        public void ChangeMikkelGroupToTillitsvalgt()
        {
            #region Variable Declarations
            //WinClient uIItemClient = this.UIBrukeradministrasjonWindow.UIItemWindow.UIBrukereClient.UIItemClient;
            WinEdit uIItemEdit = this.UIBrukeradministrasjonWindow.UIItemWindow1.UIItemEdit;
            WinButton uIOKButton = this.UIBrukeradministrasjonWindow.UIItemWindow2.UIItemClient.UIOKButton;
            WinClient uIItemClient1 = this.UIBrukeradministrasjonWindow.UIItemWindow3.UIItemClient;
            WinButton uIAlleButton = this.UIMikkelMikkelsenWindow.UIItemClient.UIAlleButton;
            WinClient uIMikkelMikkelsenClient = this.UIMikkelMikkelsenWindow.UIItemWindow1.UIMikkelMikkelsenClient;
            WinButton uILeggtilButton = this.UIMikkelMikkelsenWindow.UIItemClient.UILeggtilButton;
            WinButton uIOKButton1 = this.UIMikkelMikkelsenWindow.UIItemClient1.UIOKButton;
            //WinClient uIItemClient11 = this.UIBrukeradministrasjonWindow.UIItemClient.UIItemClient1;
            #endregion

            // Click client
            Mouse.Click(new Point(412, 60));

            // Type 'MIKKEL' in text box
            uIItemEdit.Text = this.ChangeMikkelGroupToTillitsvalgtParams.UIItemEditText;

            // Click 'OK' button
            Mouse.Click(uIOKButton, new Point(21, 27));

            // Click client
            Mouse.Click(uIItemClient1, new Point(135, 13));

            // Click 'Alle ->>' button
            Mouse.Click(uIAlleButton, new Point(49, 10));

            // Click 'Mikkel Mikkelsen' client
            Mouse.Click(uIMikkelMikkelsenClient, new Point(109, 43));

            // Click '<- Legg til' button
            Mouse.Click(uILeggtilButton, new Point(48, 11));

            // Click 'OK' button
            Mouse.Click(uIOKButton1, new Point(41, 9));

            // Click client
            Mouse.Click(new Point(1900, 60));
        }

        public List<string> Step55()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                SelectAdministrationAndSetRepresentations("Representasjoner", "55");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 55: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step56()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                CloseGat();
                StartGat(false, true, "VIGDIS");
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
                try
                {
                    if (CheckRosterPlanExists(""))
                        throw new Exception("Plans are Accessible!");
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step 56: " + e.Message);
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 56: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step57()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            CloseGat();
            StartGat(false, true, "MIKKEL");
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
            try
            {
                SelectRosterPlan("Februar 2024", false);
                UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

                UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
                SelectGustavssonLine();
                UICommon.UIMapVS2017.ApproveApprovalInApprovalTabPlace4();
                try
                {
                    CheckAllLinesApprovedStep57();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step 57: " + e.Message);
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 24: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step58()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                UICommon.SelectRosterplanPlanTab();
                CloseRosterPlan();
                CloseGat();
                StartGat(false, true, "GUNNAR");
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
                SelectRosterPlan("Februar 2024", false);
                try
                {
                    UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

                    UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
                    UICommon.UIMapVS2017.SelectAllLinesInRosterplanApprovalTabPlace5();
                    UICommon.UIMapVS2017.ApproveApprovalInApprovalTabPlace5();
                    try
                    {
                        CheckAllLinesApprovedStep58();
                    }
                    catch (Exception e)
                    {
                        errorList.Add("Error in step 58: " + e.Message);
                    }
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step 58: " + e.Message);
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 58: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step59()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion

            try
            {
                UICommon.SelectRosterplanPlanTab();
                CloseRosterPlan();
                CloseGat();
                StartGat(false, true, "BJARNE");
                UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
                SelectRosterPlan("Februar 2024");
                try
                {
                    UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

                    UICommon.UIMapVS2017.SetVisualisationGridHeight(500);
                    UICommon.UIMapVS2017.SelectAllLinesInRosterplanApprovalTab();
                    UICommon.UIMapVS2017.ApproveApprovalInApprovalTab();
                    try
                    {
                        CheckAllLinesApprovedStep59();
                    }
                    catch (Exception e)
                    {
                        errorList.Add("Error in step 59: " + e.Message);
                    }
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step 59: " + e.Message);
                }
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 59: " + e.Message);
            }

            return errorList;
        }


        public List<string> Step60()
        {
            var errorList = new List<string>();

            //Hack: Reåpner for å godkjennomg oppdatert 
            CloseRosterPlan();
            SelectRosterPlan("Februar 2024");

            UICommon.CalculateFTT(true);
            //Transfere FTT
            UICommon.UIMapVS2017.ClickTransfereFTTButton();
            UICommon.UIMapVS2017.SetTransfereFTTDate("29.01.2024");

            try
            {
                SelectAllEmpsInTransfereWindow();
                CheckFTTTransfereOkStep60();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 60: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step61()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickTransfereInTransfereWindow();
            Playback.Wait(1000);
            UICommon.UIMapVS2017.ClickCloseExportLogWindow();

            try
            {
                UICommon.SelectRosterplanPlanTab();
                UICommon.EffectuateRoasterplanNextPeriod();
                try
                {
                    CheckRegStatusStep61();
                }
                catch (Exception e)
                {
                    errorList.Add("Error in step 61: " + e.Message);
                }

            }
            catch (Exception e)
            {
                errorList.Add("Error in step 61: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step62()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.EffectuateRosterplanLines(false);
                UICommon.CloseSalaryCalculationsWindow();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 62: " + e.Message);
            }
            try
            {
                CloseRosterPlan();
                CloseGat();
            }
            catch (Exception e)
            {
                errorList.Add("Error closing Rosterplan/Gat in step 62: " + e.Message);
            }

            return errorList;
        }


        public void CheckApprovalDropdown(string row)
        {
            #region Variable Declarations
            var combo = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable.UIRow0ColumnreqColLedeComboBox;
            combo.SearchProperties[DXTestControl.PropertyNames.Name] = "gcApprovalImageComboBoxEdit[View]gvApproval[Row]" + row + "[Column]reqColLeder";
            var imageCombo = combo.UIPopupImageComboBoxEdWindow;
            imageCombo.SearchProperties[DXTestControl.PropertyNames.Name] = "gcApprovalImageComboBoxEdit[View]gvApproval[Row]" + row + "[Column]reqColLederPopupForm";
            var imageComboList = imageCombo.UIPopupImageComboBoxEdList;
            imageComboList.SearchProperties[DXTestControl.PropertyNames.Name] = "gcApprovalImageComboBoxEdit[View]gvApproval[Row]" + row + "[Column]reqColLederPopupFormPopupImageComboBoxEditListBox[0]";
            var godKjenn = imageComboList.UIGodkjennListItem;
            godKjenn.SearchProperties[DXTestControl.PropertyNames.Name] = "gcApprovalImageComboBoxEdit[View]gvApproval[Row]" + row + "[Column]reqColLederPopupFormPopupImageComboBoxEditListBox[0]Item[1]";
           var avvis = imageComboList.UIAvvisListItem;
            avvis.SearchProperties[DXTestControl.PropertyNames.Name] = "gcApprovalImageComboBoxEdit[View]gvApproval[Row]" + row + "[Column]reqColLederPopupFormPopupImageComboBoxEditListBox[0]Item[2]";
            #endregion

            // Verify that the 'Enabled' property of 'Godkjenn' list item equals 'True'
            Assert.AreEqual(true, godKjenn.Enabled);

            // Verify that the 'Enabled' property of 'Avvis' list item equals 'True'
            Assert.AreEqual(true, avvis.Enabled);
        }

        //private void SaveApprovalAsExcel(string step)
        //{
        //    //OpenExcelFromApprovalWindow();
        //    //SaveApprivalDataAsExcel(step);
        //}

        #region Common Functions
    
        public void DeleteReportFiles()
        {
            UICommon.UIMapVS2017.DeleteReportFiles(ReportFilePath);
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
        public void StartGat(bool logGatInfo, bool findBySearch, string userName = "", bool changeDepartment = false)
        {
            var dep = "";
            UICommon.LaunchGatturnus(false);

            if (changeDepartment)
                dep = UICommon.DepArbeidsplanklinikken; ;

            UICommon.LoginGatAndSelectDepartment(dep, null, userName, logGatInfo, false, findBySearch);
        }
        private void ChangeDepartment(string dep, List<string> other = null)
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
            UICommon.ChangeDepartmentFromRosterplan(dep, other, false, true);
        }
        private void SelectRosterPlan(string plan, bool selectRosterplanTab = true)
        {
            var errorList = new List<string>();

            try
            {
                if (selectRosterplanTab)
                    UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);

                UICommon.SelectRosterPlan(plan);
            }
            catch (Exception e)
            {
                errorList.Add("Error selecting rosterplan: " + e.Message);
            }
        }
        private bool CheckRosterPlanExists(string planName, int countPlans = -1)
        {
            var exists = false;

            if (countPlans > -1)
                exists = UICommon.CheckRosterPlanExists(planName) && UICommon.CheckNumberOfRosterplans() == countPlans;
            else
                exists = UICommon.CheckRosterPlanExists(planName);

            return exists;
        }

        public void CreateRosterplan(string name, DateTime startDate, string weeks, int step)
        {
            if (step == 4)
                ChangeDepartment("5080");

            CreateCalendarplan(name, startDate, weeks);
        }
        private void CreateCalendarplan(string name, DateTime startDate, string weeks)
        {
            UICommon.ClickNewRosterplanButton();
            UICommon.UIMapVS2017.SetRosterPlanName(name);
            UICommon.UIMapVS2017.SelectRosterplanType("Kalenderplan");
            UICommon.SetStartDateRosterplan(startDate);
            UICommon.UIMapVS2017.SetRosterPlanWeeks(weeks);
            UICommon.ClickOkRosterplanSettings();
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
        private List<String> SaveApprovalDataAsExcel(string stepName)
        {
           return SaveAsExcel(stepName);
        }

        private List<String> SaveAsExcel(string postfix)
        {
            var errorList = new List<String>();

            try
            {
                var fileName = ReportFilePath + ReportFileName + postfix; // + SupportFunctions.HeaderType._Common.ToString();
                UICommon.ExportToExcel(fileName);
            }
            catch (Exception e)
            {
                errorList.Add("Feil ved export til excel(" + postfix + "): " + e.Message);
            }

            return errorList;
        }
        public List<String> CompareReportDataFiles_Test020()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext, 5);
            return errorList;
        }
        public void AssertResults(List<string> errorList)
        {
            UICommon.AssertResults(errorList);
        }
        #endregion

        public virtual ChangeMikkelGroupToTillitsvalgtParams ChangeMikkelGroupToTillitsvalgtParams
        {
            get
            {
                if ((this.mChangeMikkelGroupToTillitsvalgtParams == null))
                {
                    this.mChangeMikkelGroupToTillitsvalgtParams = new ChangeMikkelGroupToTillitsvalgtParams();
                }
                return this.mChangeMikkelGroupToTillitsvalgtParams;
            }
        }

        private ChangeMikkelGroupToTillitsvalgtParams mChangeMikkelGroupToTillitsvalgtParams;

           }

    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class ChangeMikkelGroupToTillitsvalgtParams
    {

        #region Fields
        /// <summary>
        /// Type 'MIKKEL' in text box
        /// </summary>
        public string UIItemEditText = "MIKKEL";
        #endregion
    }
}
