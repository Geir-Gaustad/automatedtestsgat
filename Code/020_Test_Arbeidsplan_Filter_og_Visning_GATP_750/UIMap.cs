namespace _020_Test_Arbeidsplan_Filter_og_Visning_GATP_750
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
    using System.IO;

    public partial class UIMap
    {
        #region Fields
        public string ReportFilePath;
        public string ReportFileName = "020_excel";
        public string FileType = ".xls";
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        #endregion

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            UICommon = new CommonUIFunctions.UIMap(TestContext);
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_020_FilterOgVisning\");
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
            UICommon.LoginGatAndSelectDepartment(UICommon.DepFilterVisning, null, "KALLA", logGatInfo);
        }
        private void SelectRosterplanTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
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
        private bool CheckRosterPlanExists(string planName, int countPlans = -1)
        {
            var exists = false;

            if (countPlans > -1)
                exists = UICommon.CheckRosterPlanExists(planName) && UICommon.CheckNumberOfRosterplans() == countPlans;
            else
                exists = UICommon.CheckRosterPlanExists(planName);

            return exists;
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
        public void DeleteReportFiles()
        {
            UICommon.UIMapVS2017.DeleteReportFiles(ReportFilePath);
        }

        private List<string> SaveAsExcel(string stepName)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion
            try
            {
                var fileName = ReportFilePath + ReportFileName + stepName; // + SupportFunctions.HeaderType._Common.ToString();
                UICommon.ExportToExcel(fileName);
            }
            catch (Exception e)
            {
                errorList.Add("Feil ved export til excel(" + stepName + "): " + e.Message);
            }

            return errorList;
        }

        public List<String> CompareReportDataFiles_Test020()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext);
            return errorList;
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

        public List<string> Step_1()
        {
            var errorList = new List<string>();

            try
            {
                StartGat(true);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 1: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_2()
        {
            var errorList = new List<string>();

            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Extrainfo);
            try
            {
                if (!CheckRosterPlanExists("Filter og visning 1", 4))
                    throw new Exception("Filter og visning 1 are not Accessible or more plans than expected are accessible!");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 2: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_3()
        {
            var errorList = new List<string>();

            SelectRosterplan("Filter og visning 1", true, true);
            try
            {
                CheckEmployeeList();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 3(Employees): " + e.Message);
            }
            try
            {
                CheckOnyNameInfoVisible();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 3(Columns showing): " + e.Message);
            }

            return errorList;
        }
        private void CheckOnyNameInfoVisible()
        {
            #region Variable Declarations
            var uIColumnHandle35ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle35ColumnHeader;
            var uIColumnHandle1ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle1ColumnHeader;
            var uIColumnHandle4ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle4ColumnHeader;
            var uIColumnHandle17ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle17ColumnHeader;
            var uIColumnHandle5ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle5ColumnHeader;
            var uIColumnHandle122ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle122ColumnHeader;
            var uIColumnHandle82ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle82ColumnHeader;
            var uIColumnHandle142ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle142ColumnHeader;
            var uIColumnHandle16ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle16ColumnHeader;
            var uIColumnHandle15ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle15ColumnHeader;
            var uIColumnHandle11ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle11ColumnHeader;
            var uIColumnHandle19ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle19ColumnHeader;
            var uIColumnHandle20ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle20ColumnHeader;
            var uIColumnHandle26ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle26ColumnHeader;
            var uIColumnHandle21ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle21ColumnHeader;
            var uIColumnHandle38ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle38ColumnHeader;
            var uIColumnHandle29ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle29ColumnHeader;
            var uIColumnHandle22ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle22ColumnHeader;
            var uIColumnHandle23ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle23ColumnHeader;
            var uIColumnHandle30ColumnHeader = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIColumnHandle30ColumnHeader;
            #endregion

            // Verify that the 'Visible' property of 'ColumnHandle35' column header equals 'True'
            Assert.AreEqual(this.CheckFullInfoVisibleExpectedValues.UIColumnHandle35ColumnHeaderVisible, uIColumnHandle35ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle1' column header equals 'True'
            Assert.AreEqual(this.CheckFullInfoVisibleExpectedValues.UIColumnHandle1ColumnHeaderVisible, uIColumnHandle1ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle4' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle4ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle17' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle17ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle5' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle5ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle12-2' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle122ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle8-2' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle82ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle14-2' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle142ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle16' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle16ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle15' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle15ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle11' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle11ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle19' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle19ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle20' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle20ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle26' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle26ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle21' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle21ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle38' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle38ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle29' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle29ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle22' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle22ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle23' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle23ColumnHeader.Visible);

            // Verify that the 'Visible' property of 'ColumnHandle30' column header equals 'True'
            Assert.AreEqual(false, uIColumnHandle30ColumnHeader.Visible);
        }

        public List<string> Step_4()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.UIMapVS2017.F3SearchInRosterplanGrid("Hellner", false);
                Playback.Wait(1500);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 4: " + e.Message);
            }
            try
            {
                CheckHellnerVisible();
                CheckLinesInPlan(1);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 4: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_5()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.EmptyF3SearchInRosterplanGrid();
            try
            {
                CheckEmptyF3SearchInRosterplanGrid();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 5: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_6()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.CloseF3SearchInRosterplanGrid();
            try
            {
                CheckF3SearchDisabledInRosterplanGrid();
                CheckEmployeeList();
                CheckLinesInPlan(13);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 6: " + e.Message);
            }

            return errorList;
        }

        private void CheckF3SearchDisabledInRosterplanGrid()
        {
            #region Variable Declarations
            var uI_TeFindMRUEdit = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UI_LayoutControl1Custom.UILcGroupMainLayoutGroup.UILciFindLayoutControlItem.UI_TeFindMRUEdit;
            #endregion

            Assert.AreEqual(false, uI_TeFindMRUEdit.Visible);
        }

        public List<string> Step_7()
        {
            var errorList = new List<string>();

            SelectRosterPlanFilterTab();
            UICommon.UIMapVS2017.SelectPartlyInfoFilter();
            Keyboard.SendKeys("{TAB}");
            Playback.Wait(1000);
            try
            {
                CheckPartlyInfoVisible();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 7: " + e.Message);
            }

            return errorList;
        }

        public void ShowAllplansInDepFilter(bool showRosterlines)
        {
            #region Variable Declarations
            DXRibbonEditItem uIDdlOtherDepFilterRibbonEditItem = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIDdlOtherDepFilterRibbonEditItem;
            DXTestControl uINode1TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlOtherDepFilterPopClient.UIDdlOtherDepFilterTreTreeList.UINode1TreeListNode.UINode1TreeListNodeCheckBox;
            uINode1TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node1";
            DXButton uIAvbrytButton = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIAvbrytButton;
            #endregion

            // Click 'ddlOtherDepFilter' RibbonEditItem
            Mouse.Click(uIDdlOtherDepFilterRibbonEditItem, new Point(157, 7));

            if (showRosterlines)
                ShowAllRosterlinesOnAllDepsFilter();

            // Click 'Node1' TreeListNodeCheckBox
            Mouse.Click(uINode1TreeListNodeCheckBox, new Point(5, 5));

            // Click 'Avbryt' button
            Mouse.Click(uIAvbrytButton, new Point(8, 7));
        }

        public List<string> Step_8()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectFullInfoFilter();
            Keyboard.SendKeys("{TAB}");
            Playback.Wait(1000);
            try
            {
                CheckFullInfoVisible();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 8: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_9()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectCustomFilter();
            SelectFilterToDeleteStep9();
            UICommon.UIMapVS2017.DeleteCustomFilter();
            CreateNewFilterStep9();
            Playback.Wait(1000);

            errorList.AddRange(CheckCustomFilterInfoVisible("9"));

            return errorList;
        }
        private void CreateNewFilterStep9()
        {
            UICommon.UIMapVS2017.CreateCustomFilter("Stilling", new int[] { 0, 1, 2, 4, 5, 6, 9, 11 }, false);
        }
        private List<string> CheckCustomFilterInfoVisible(string step)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            var nr = false;

            var ansatt = false;

            var avd = false;

            var ksted = false;

            var type = false;

            var stk = false;

            var percent = false;

            var gr = false;

            var ints = false;

            var rs = false;

            var ot = false;

            var ds = false;

            var us = false;

            var rugga = false;

            var iv = false;

            var tb = false;

            var aaa = false;

            var ft = false;

            var fb = false;

            var eier = false;
            int visibleCount = 0;
            #endregion

            var tbl = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable;
            for (int i = 55; i < tbl.Views[0].Columns.Length; i++)
            {
                var b = tbl.Views[0].Columns[i];
                var e = "";
                try
                {
                    e = b.Text;
                }
                catch (Exception)
                {
                }

                if (e == "Nr.")
                {
                    nr = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "Ansatt")
                {
                    ansatt = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "Avd.")
                {
                    avd = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "K.sted")
                {
                    ksted = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "Type")
                {
                    type = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "St.k.")
                {
                    stk = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "%")
                {
                    percent = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "Gr.")
                {
                    gr = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "I.s.")
                {
                    ints = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "R.s.")
                {
                    rs = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "O.t.")
                {
                    ot = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "D.s.")
                {
                    ds = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "U.s.")
                {
                    us = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "hm")
                {
                    rugga = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "Iv.")
                {
                    iv = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "T.b.")
                {
                    tb = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "Å.a.")
                {
                    aaa = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "F.t.")
                {
                    ft = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "F.b.")
                {
                    fb = b.Visible;
                    visibleCount++;
                    continue;
                }
                if (e == "Eier")
                {
                    eier = b.Visible;
                    visibleCount++;
                    continue;
                }

                //if (e == "Info")
                //{
                //     info = b.Visible;
                //}


            }

            if (step == "9")
            {
                if (nr && ansatt && avd && type && stk && percent && gr && rs && visibleCount == 8)
                {
                    TestContext.WriteLine("Filter Ok, step " + step);
                }
                else
                {
                    errorList.Add("Error in step " + step + ": Unexpected columns are visible");
                }
            }

            return errorList;
        }
        public List<string> Step_10()
        {
            var errorList = new List<string>();

            try
            {
                CheckEmpFilterEnabledStep10();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 10(Employeefilter disabled): " + e.Message);
            }

            return errorList;
        }


        public List<string> Step_11()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickDownArrowOpenEmpFilter();
            //UICommon.UIMapVS2017.ClickUpArrowInEmpFilter();
            //UICommon.UIMapVS2017.ClickDownArrowInEmpFilter();

            try
            {
                CheckEmpFilterExistsStep11();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 11: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_12()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickEditEmpFilter();
            UICommon.UIMapVS2017.SearchInEmpFilterInEditFilterWindow("BLÅ GRUPPE");

            try
            {
                CheckEmpsInFilterListStep12();
                CheckEmpFilterButtonsDisabledStep12();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 12 " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_13()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClearEmpFilterInEditFilterWindow();

            try
            {
                CheckEmpFilterSelectFilterStatusStep13();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 13: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_14()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickNewEmpFilterInEditFilterWindow();
            AddEmpsToNewFilterStep14();

            try
            {
                CheckJohnssonInListStep14();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 14: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_15()
        {
            var errorList = new List<string>();

            AddJohnssonToNewFilterStep15();
            UICommon.UIMapVS2017.ClickAddEmpFilterInEditFilterWindow();
            //UICommon.UIMapVS2017.ClickCancelEmpFilterInEditFilterWindow();

            try
            {
                CheckEmpsInRedFilterStep15();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 15: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_16()
        {
            var errorList = new List<string>();

            SetFilterNameRedStep16();
            UICommon.UIMapVS2017.ClickUseEmpFilterInEditFilterWindow();
            //UICommon.UIMapVS2017.ClickCloseEmpFilterInEditFilterWindow();

            try
            {
                CheckRosterplanEmpsInRedFilterStep16();
                CheckLinesInPlan(3);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 16: " + e.Message);
            }

            return errorList;
        }
        private void CheckLinesInPlan(int rows)
        {
            var linesInPlan = UICommon.UIMapVS2017.CountRosterplanLines();

            if (rows != linesInPlan)
                throw new Exception("Unexpected number of rows in plan, Expected: " + rows + " Actual: " + linesInPlan);
        }

        public List<string> Step_17()
        {
            var errorList = new List<string>();

            SelectBlueFilter();

            try
            {
                CheckRosterplanEmpsInBlueFilterStep17();
                CheckLinesInPlan(4);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 17: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_18()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.SumBehov);
            SelectSumDemandFullInfoWithDetails();

            try
            {
                CheckSumDemandValuesStep18();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 18: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_19()
        {
            var errorList = new List<string>();

            ShowOnlyAssistantFilter();

            try
            {
                CheckOnlyAnderssonLineStep19();

                if (UICommon.UIMapVS2017.CountRosterplanLines() != 1)
                    throw new Exception("Unexpected number of lines in rosterplan");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 19: " + e.Message);
            }

            try
            {
                OpenExcelFromSumTab();
             
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 19(Open excel): " + e.Message);
            }

            errorList.AddRange(SaveAsExcel("_step_19"));

            return errorList;
        }

        private void ShowOnlyAssistantFilter()
        {
            #region Variable Declarations
            DXRibbonEditItem uIDdlFilterRibbonEditItem = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIDdlFilterRibbonEditItem;
            DXTestControl uINode1TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode0TreeListNode.UINode1TreeListNode.UINode1TreeListNodeCheckBox;
            uINode1TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node1";
            DXTestControl uINode2TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode0TreeListNode.UINode2TreeListNode.UINode2TreeListNodeCheckBox;
            uINode2TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node2";
            DXTestControl uINode3TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode0TreeListNode.UINode3TreeListNode.UINode3TreeListNodeCheckBox;
            uINode3TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node3";
            DXTestControl uINode4TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode0TreeListNode.UINode4TreeListNode.UINode4TreeListNodeCheckBox;
            uINode4TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node4";
            DXButton uIAvbrytButton = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIAvbrytButton;
            #endregion

            // Click 'ddlFilter' RibbonEditItem
            Mouse.Click(uIDdlFilterRibbonEditItem, new Point(161, 10));

            // Click 'Node1' TreeListNodeCheckBox
            Mouse.Click(uINode1TreeListNodeCheckBox, new Point(6, 9));

            // Click 'Node2' TreeListNodeCheckBox
            Mouse.Click(uINode2TreeListNodeCheckBox, new Point(6, 7));

            // Click 'Node3' TreeListNodeCheckBox
            Mouse.Click(uINode3TreeListNodeCheckBox, new Point(4, 5));

            // Click 'Node4' TreeListNodeCheckBox
            Mouse.Click(uINode4TreeListNodeCheckBox, new Point(4, 9));

            // Click 'Avbryt' button
            Mouse.Click(uIAvbrytButton, new Point(6, 11));
        }
        public List<string> Step_20()
        {
            var errorList = new List<string>();

            UICommon.CalculateFTT(true);

            try
            {
                CheckOnlyAnderssonFTTStep20();

                if (CountRosterFTTLines() != 1)
                    throw new Exception("Unexpected number of lines in rosterplan");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 20: " + e.Message);
            }

            return errorList;
        }
        private int CountRosterFTTLines()
        {
            #region Variable Declarations
            var pGrid = UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient2.UIFixedPaymentViewCustom.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid;
            #endregion

            return pGrid.RowCount;
        }

        public List<string> Step_21()
        {
            var errorList = new List<string>();

            //Transfere FTT
            UICommon.UIMapVS2017.ClickTransfereFTTButton();

            try
            {
                CheckEmpsInTransfereWindowStep21();

                if (CountEmpLinesInTransfereWindow() != 13)
                    throw new Exception("Unexpected number of lines in transfere window");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 21: " + e.Message);
            }

            return errorList;
        }
        private int CountEmpLinesInTransfereWindow()
        {
            #region Variable Declarations
            var pGrid = UIOverførtilleggWindow.UIGcExportsTable;
            #endregion

            return pGrid.Views[0].RowCount;
        }

        public List<string> Step_22()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickCancelInTransfereWindow();

            DisableEmpFilter();
            ShowOnlyAssistantFilter();

            UICommon.UIMapVS2017.ClickDownArrowOpenEmpFilter();
            UICommon.UIMapVS2017.ClickEditEmpFilter();

            UICommon.UIMapVS2017.ClickNewEmpFilterInEditFilterWindow();
            SelectDep1030Step22();
            AddEmpsToNewFilterStep22();

            UICommon.UIMapVS2017.ClickAddEmpFilterInEditFilterWindow();
            SetFilterName1030Step22();

            try
            {
                CheckFilter1030Step22();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 22: " + e.Message);
            }

            return errorList;
        }


        public List<string> Step_23()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickUseEmpFilterInEditFilterWindow();
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Kostnadssimulering);
            UICommon.UIMapVS2017.ClickCalculateInCostSimulationTab();
            DragCostSimulation();
            UICommon.UIMapVS2017.OpenCostSimulationEmpPanel();

            try
            {
                CheckEmpsInCostSimulationStep23();

                if (CheckLinesInCostTable() != 3)
                    throw new Exception("Unexpected number of lines in cost simulation employee table");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 23: " + e.Message);
            }

            return errorList;
        }

        private int CheckLinesInCostTable()
        {
            #region Variable Declarations
            var empCostTable = UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient1.UICostSimulationViewCustom.UIGsSplitContainerContSplitContainerControl.UISplitGroupPanelClient.UIGsNavBarControl1NavBar.UINavBarGroupControlCoScrollableControl.UIGcEmployeeCostsTable;
            #endregion

            return empCostTable.Views[0].RowCount;
        }
        public List<string> Step_24()
        {
            var errorList = new List<string>();

            ShowOnlyGroupOneFilter();

            try
            {
                CheckRosterplanEmpsInGroupOneFilterStep24();
                CheckLinesInPlan(2);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 23: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_25()
        {
            var errorList = new List<string>();

            DisableEmpFilter();

            try
            {
                CheckRosterplanEmpsInGroupOneFilterStep24();
                CheckLinesInPlan(2);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 24: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_26()
        {
            var errorList = new List<string>();

            ShowAllGroupsFilter();

            try
            {
                CheckEmployeeList();
                CheckLinesInPlan(13);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 26: " + e.Message);
            }

            return errorList;
        }
        public void ShowAllGroupsFilter()
        {
            #region Variable Declarations
            DXRibbonEditItem uIDdlFilterRibbonEditItem = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIDdlFilterRibbonEditItem;
            DXTestControl uINode1TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode1TreeListNode.UINode1TreeListNodeCheckBox;
            uINode1TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node1";
            #endregion

            // Click 'ddlFilter' RibbonEditItem
            Mouse.Click(uIDdlFilterRibbonEditItem, new Point(156, 9));

            // Click 'Node1' TreeListNodeCheckBox
            Mouse.Click(uINode1TreeListNodeCheckBox, new Point(7, 8));
        }

        public List<string> Step_27()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectShowInactiveLinesInFilter();

            try
            {
                CheckJohnssonInListStep27();
                CheckLinesInPlan(14);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 27: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_28()
        {
            var errorList = new List<string>();

            ShowOnly100PercentEmployment();

            try
            {
                Check100PercentEmploymentInListStep28();
                CheckLinesInPlan(7);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 28: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_29()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectShowInactiveLinesInFilter();
            ShowAllEmployments();

            try
            {
                CheckEmployeeList();
                CheckLinesInPlan(13);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 29: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_30()
        {
            var errorList = new List<string>();

            ShowDep5210And1030InFilter();

            try
            {
                CheckEmployeeListStep30();
                CheckLinesInPlan(26);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 30: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_31()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickForwardViewPeriodInFilterTab();

            try
            {
                CheckFilterActiveStep31();
            }
            catch (Exception)
            {
                errorList.Add("Filter disabled when moving forward in rosterplan(step 31)");
                ShowDep5210And1030InFilter();
            }

            try
            {
                CheckEmployeeListStep31();
                CheckLinesInPlan(26);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 31: " + e.Message);
            }

            return errorList;
        }
        private void CheckFilterActiveStep31()
        {
            #region Variable Declarations
            DXCell uIBurmanJensCell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIRosterPlanGrid1Custom.UIGcRosterPlanTable.UIBurmanJensCell;
            #endregion

            // Verify that the 'Text' property of 'Burman, Jens' cell equals 'Andersson, Ebba'
            Assert.AreEqual(this.CheckEmployeeListStep31ExpectedValues.UIBurmanJensCellText, uIBurmanJensCell.Text);
        }
        private void ShowDep5210And1030InFilter()
        {
            #region Variable Declarations
            Playback.Wait(1000);
            DXRibbonEditItem uIDdlOtherDepFilterRibbonEditItem = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIDdlOtherDepFilterRibbonEditItem;
            DXTestControl uINode0TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlOtherDepFilterPopClient.UIDdlOtherDepFilterTreTreeList.UINode1TreeListNode.UINode0TreeListNode.UINode0TreeListNodeCheckBox;
            uINode0TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node0";
            DXTestControl uINode1TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlOtherDepFilterPopClient.UIDdlOtherDepFilterTreTreeList.UINode1TreeListNode.UINode1TreeListNode1.UINode1TreeListNodeCheckBox;
            uINode1TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node1";
            DXButton uIAvbrytButton = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIAvbrytButton;
            #endregion

            // Click 'ddlOtherDepFilter' RibbonEditItem
            Mouse.Click(uIDdlOtherDepFilterRibbonEditItem, new Point(158, 9));

            // Click 'Node0' TreeListNodeCheckBox
            Mouse.Click(uINode0TreeListNodeCheckBox, new Point(9, 9));

            // Click 'Node1' TreeListNodeCheckBox
            Mouse.Click(uINode1TreeListNodeCheckBox, new Point(6, 7));

            // Click 'Avbryt' button
            Mouse.Click(uIAvbrytButton, new Point(8, 5));
        }
        public List<string> Step_32()
        {
            var errorList = new List<string>();

            ShowAllplansInDepFilter(false);
            UICommon.UIMapVS2017.ClickBackViewPeriodInFilterTab();
            UICommon.UIMapVS2017.SelectWeeksumShowTotalOnlyFilter();

            try
            {
                CheckWeeksumVisible();
                CheckEmployeeList();
                CheckLinesInPlan(13);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 32: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_33()
        {
            var errorList = new List<string>();

            UICommon.OpenRosterplanSettings(true);
            UICommon.UIMapVS2017.SetPlanReadyForApproval(true);
            UICommon.ClickOkRosterplanSettings();

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Godkjenning);

            try
            {
                CheckEmployeesInApprovalList();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 33: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_34()
        {
            var errorList = new List<string>();

            Select1030EmpFilter();

            try
            {
                CheckEmployeesInPlanStep34();
                CheckLinesInPlan(3);
                CheckEmployeesInApprovalListStep34();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 34: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_35()
        {
            var errorList = new List<string>();

            SelectRosterPlanFilterTab();
            SelectEmploymentFilterSykepleier();

            try
            {
                CheckEmployeesInPlanStep35();
                CheckLinesInPlan(1);
                CheckEmployeesInApprovalListStep35();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 35: " + e.Message);
            }

            return errorList;
        }

        /// <summary>
        /// SelectEmploymentFilterSykepleier
        /// </summary>
        public void SelectEmploymentFilterSykepleier()
        {
            #region Variable Declarations
            DXRibbonEditItem uIDdlFilterRibbonEditItem = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIDdlFilterRibbonEditItem;
            DXTestControl uINode0TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode0TreeListNode.UINode0TreeListNodeCheckBox;
            uINode0TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node0";
            DXTestControl uINode2TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode0TreeListNode.UINode2TreeListNode.UINode2TreeListNodeCheckBox;
            uINode2TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node2";
            DXButton uIAvbrytButton = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIAvbrytButton;
            #endregion

            // Click 'ddlFilter' RibbonEditItem
            Mouse.Click(uIDdlFilterRibbonEditItem, new Point(157, 10));

            // Click 'Node0' TreeListNodeCheckBox
            Mouse.Click(uINode0TreeListNodeCheckBox, new Point(3, 8));

            // Click 'Node2' TreeListNodeCheckBox
            Mouse.Click(uINode2TreeListNodeCheckBox, new Point(9, 7));

            // Click 'Avbryt' button
            Mouse.Click(uIAvbrytButton, new Point(10, 10));
        }

        public List<string> Step_36()
        {
            var errorList = new List<string>();

            DisableEmpFilter();
            DisableEmploymentFilter();

            try
            {
                CheckEmployeeList();
                CheckLinesInPlan(13);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 36Rosterplan): " + e.Message);
            }

            try
            {
                CheckEmployeesInApprovalList();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 36(Approvallist): " + e.Message);
            }

            return errorList;
        }
        public void DisableEmploymentFilter()
        {
            #region Variable Declarations
            DXRibbonEditItem uIDdlFilterRibbonEditItem = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIDdlFilterRibbonEditItem;
            DXTestControl uINode0TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode0TreeListNode.UINode0TreeListNodeCheckBox;
            uINode0TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node0";
            DXButton uIAvbrytButton = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIAvbrytButton;
            #endregion

            // Click 'ddlFilter' RibbonEditItem
            Mouse.Click(uIDdlFilterRibbonEditItem, new Point(157, 10));

            // Click 'Node0' TreeListNodeCheckBox
            Mouse.Click(uINode0TreeListNodeCheckBox, new Point(3, 8));

            // Click 'Avbryt' button
            Mouse.Click(uIAvbrytButton, new Point(10, 10));
        }
        public List<string> Step_37()
        {
            var errorList = new List<string>();

            SelectBlueFilter();

            UICommon.SelectPlanTabRosterplan();
            UICommon.EffectuateRoasterplanNextPeriod();

            try
            {
                CheckEmployeesInEffecuationWindowStep37();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 37: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_38()
        {
            var errorList = new List<string>();

            UICommon.EffectuateRosterplanLines(false);
            UICommon.CloseSalaryCalculationsWindow();

            CloseRosterplan();
            SelectRosterplan("Overlappende plan", true, true);

            SelectRosterPlanFilterTab();
            ShowAllplansInDepFilter(true);

            try
            {
                CheckVisibleDataTypeStep38();
                CheckVisibleDataStep38();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 38: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_39()
        {
            var errorList = new List<string>();

            CloseRosterplan();
            SelectRosterplan("Kalenderplan", true, true);
            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Gjennomsnittsberegning);
            DragAveraging();
            SelectAveragingPeriod("2024");

            try
            {
                CheckAllEmpsInCalendarPlan();
                CheckLinesInPlan(11);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 39(Rosterplan): " + e.Message);
            }
            try
            {
                CheckAllEmpsInAveragingList();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 39(Averaging): " + e.Message);
            }

            return errorList;
        }
        private void SelectAveragingPeriod(string period)
        {
            #region Variable Declarations
            DXLookUpEdit uILeAverageCalculationLookUpEdit = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient4.UIAverageCalculationViCustom.UIPaControlsHeaderClient.UILeAverageCalculationLookUpEdit;
            #endregion

            Keyboard.SendKeys(uILeAverageCalculationLookUpEdit, period + "{ENTER}");
        }

        public List<string> Step_40()
        {
            var errorList = new List<string>();

            SelectRedFilter();

            try
            {
                CheckEmpsInCalendarPlanStep40();
                CheckLinesInPlan(3);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 40(Rosterplan): " + e.Message);
            }
            try
            {
                CheckEmpsInAveragingListStep40();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 40(Averaging): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_41()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Ukekommentarer);

            try
            {
                CheckEmpsInCommentListStep41();
                if (CheckCommentListLines() != 11)
                    throw new Exception("Unexpected number of lines in commentlist");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 41: " + e.Message);
            }

            return errorList;
        }
        private int CheckCommentListLines()
        {
            #region Variable Declarations
            var grid = UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient5.UIWeekScheduleViewCustom.UIGcCommentsTable;
            #endregion

            var rows = grid.Views[0].RowCount;
            return rows;
        }

        public List<string> Step_42()
        {
            var errorList = new List<string>();

            SelectRosterPlanFilterTab();
            ShowEmploymentFilterLege();

            try
            {
                CheckEmpsInCalendarPlanStep42();
                CheckLinesInPlan(1);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 42(Rosterplan): " + e.Message);
            }
            try
            {
                CheckEmpsInCommentListStep42();
                if (CheckCommentListLines() != 2)
                    throw new Exception("Unexpected number of lines in commentlist");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 42: " + e.Message);
            }

            return errorList;
        }
        private void ShowEmploymentFilterLege()
        {
            #region Variable Declarations
            DXRibbonEditItem uIDdlFilterRibbonEditItem = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIRpFilterRibbonPage.UIRpgFilterRibbonPageGroup.UIDdlFilterRibbonEditItem;
            DXTestControl uINode0TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode0TreeListNode.UINode0TreeListNodeCheckBox;
            uINode0TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node0";
            DXTestControl uINode4TreeListNodeCheckBox = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIDdlFilterPopupContaiClient.UIDdlFilterTreeListTreeList.UINode0TreeListNode.UINode4TreeListNode.UINode4TreeListNodeCheckBox;
            uINode4TreeListNodeCheckBox.SearchProperties[DXTestControl.PropertyNames.Name] = "Node4";
            DXButton uIAvbrytButton = this.UIArbeidsplanWindow.UIRosterPlanGridMenu1Custom.UIRcMenuRibbon.UIPopupContainerEditPopupEdit.UIPopupContainerFormWindow.UIAvbrytButton;
            #endregion

            // Click 'ddlFilter' RibbonEditItem
            Mouse.Click(uIDdlFilterRibbonEditItem, new Point(161, 10));
            Mouse.Click(uINode0TreeListNodeCheckBox);
            Mouse.Click(uINode4TreeListNodeCheckBox);

            // Click 'Avbryt' button
            Mouse.Click(uIAvbrytButton);
        }
        public List<string> Step_43()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Feriebanker);

            try
            {
                CheckEmpsInCalendarPlanStep43();
                CheckLinesInPlan(1);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 43(Rosterplan): " + e.Message);
            }
            try
            {
                CheckEmpsInEmployeesVacationBanksListStep43();
                if (CheckVacationBanksLines() != 3)
                    throw new Exception("Unexpected number of lines in VacationBankslist");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 43(VacationBanks): " + e.Message);
            }

            return errorList;
        }
        private int CheckVacationBanksLines()
        {
            #region Variable Declarations
            var grid = UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient6.UIVacationBankViewCustom.UIVhVacationBanksCustom.UIPcViewClient.UIPlannedEmployeesVacaCustom.UIGcPlannedEmployeesVaTable;
            #endregion

            var rows = grid.Views[0].RowCount;
            return rows;
        }

        public List<string> Step_44()
        {
            var errorList = new List<string>();

            DisableEmpFilter();
            DisableEmploymentFilter();

            try
            {
                CheckAllEmpsInCalendarPlan();
                CheckLinesInPlan(11);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 44(Rosterplan): " + e.Message);
            }

            try
            {
                CheckEmpsInEmployeesVacationBanksListStep44();
                if (CheckVacationBanksLines() != 13)
                    throw new Exception("Unexpected number of lines in VacationBankslist");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 44: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_45()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.ClickDownArrowOpenEmpFilter();
            UICommon.UIMapVS2017.ClickEditEmpFilter();

            UICommon.UIMapVS2017.ClickNewEmpFilterInEditFilterWindow();
            SelectDepInclEmloymentsNotStartedStep45();
            AddEmpsToNewFilterStep45();

            UICommon.UIMapVS2017.ClickAddEmpFilterInEditFilterWindow();
            SetFilterNameLEGERStep45();

            UICommon.UIMapVS2017.ClickUseEmpFilterInEditFilterWindow();

            try
            {
                CheckAllEmpsInCalendarPlanstep45();
                CheckLinesInPlan(3);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 45(Rosterplan): " + e.Message);
            }

            try
            {
                CheckEmpsInEmployeesVacationBanksListStep45();
                if (CheckVacationBanksLines() != 5)
                    throw new Exception("Unexpected number of lines in VacationBankslist");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 45: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_46()
        {
            var errorList = new List<string>();

            UICommon.UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Fravær);

            try
            {
                CheckEmpsInCalendarPlanStep46();
                CheckLinesInPlan(3);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 46(Rosterplan): " + e.Message);
            }
            try
            {
                CheckEmpsInAbsenceListStep46();
                if (CheckAbsenceLines() != 2)
                    throw new Exception("Unexpected number of lines in Absencelist");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 46(Absence): " + e.Message);
            }

            return errorList;
        }
        private int CheckAbsenceLines()
        {
            #region Variable Declarations
            var grid = UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient7.UIAbsenceListViewCustom.UIGcAbsenceListTable;
            #endregion

            var rows = grid.Views[0].RowCount;
            return rows;
        }
        public List<string> Step_47()
        {
            var errorList = new List<string>();

            DisableEmpFilter();

            try
            {
                CheckAllEmpsInCalendarPlan();
                CheckLinesInPlan(11);
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 47(Rosterplan): " + e.Message);
            }

            try
            {
                CheckEmpsInAbsenceListStep47();
                if (CheckAbsenceLines() != 6)
                    throw new Exception("Unexpected number of lines in Absencelist");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 47(Absence): " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_48()
        {
            var errorList = new List<string>();

            CloseRosterplan();

            SelectRosterplan("Filter og visning 1", true, true);
            UICommon.SelectPlanTabRosterplan();
            UICommon.EffectuateRoasterplanNextPeriod();

            try
            {
                CheckEmployeesInEffecuationWindowStep48();
                CheckGroupBlueFilterInEffecuationWindowStep48();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 48: " + e.Message);
            }

            return errorList;
        }

        public List<string> Step_49()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.EffectuateRosterplanLines(false);
                UICommon.CloseSalaryCalculationsWindow();

                CloseRosterplan();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 49: " + e.Message);
            }

            return errorList;
        }
        public List<string> Step_50()
        {
            var errorList = new List<string>();

            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Shiftbook);
            UICommon.GoToShiftbookdate(new DateTime(2024, 01, 01));
            SelectLEGERFilter();

            try
            {
                CheckValuesStep50();
                if (CheckEmpsInDcolStep50() != 2)
                    throw new Exception("Unexpected number of emps in D-column");
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 50: " + e.Message);
            }

            return errorList;
        }

        public List<string> Close()
        {
            var errorList = new List<string>();
            try
            {
                CloseGat();
            }
            catch (Exception e)
            {
                errorList.Add("Error in closing Gat: " + e.Message);
            }

            return errorList;
        }


        public int CheckEmpsInDcolStep50()
        {
            #region Variable Declarations
            var tbl = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIDag00167770770FalseDockPanel.UIControlContainerCustom.UIGcDayColumnTable;
            #endregion

            var rows = tbl.Views[0].RowCount;
            return rows;
        }
    }
}