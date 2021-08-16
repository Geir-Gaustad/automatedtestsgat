namespace _020_Test_Arbeidsplan_Doegnrytmeplan
{
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
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System.Diagnostics;
    using System.Threading;
    using System.Globalization;
    using System.IO;

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
            ReportFilePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test_020_Doeynrytmeplan\");

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
        public void DeleteReportFiles()
        {
            UICommon.UIMapVS2017.DeleteReportFiles(ReportFilePath);
        }

        public List<String> CompareReportDataFiles_Test020()
        {
            var errorList = DataService.CompareReportDataFiles(ReportFilePath, FileType, TestContext);
            return errorList;
        }

        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepArbeidsplanOghjelpeplan, null, "", logGatInfo);
        }

        public void SelectDepartmentTab()
        {
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Department);
        }

        public List<string> CreateDayRythmplan()
        {
            var errorList = new List<string>();
            UICommon.SelectSubTabInDepartmentTab(CommonUIFunctions.UIMap.DepartmentTabs.Døgnrytmeplan, false);
            ClickNewDayRythmplan();
            NameDayRythmplan("2022");

            UICommon.SetDayRythmplanPeriod(new DateTime(2022, 01, 03), new DateTime(2023, 01, 01));
            AddDescription();

            errorList.AddRange(CreateSykepleierLayer());

            return errorList;
        }

        public List<string> DuplicateDayRythmplan(string name)
        {
            var errorList = new List<string>();
            
            DuplicateDayRythmplan();

            try
            {
                CheckDayRosterplanNameIsEmpty();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            errorList.AddRange(CheckDayRythmplanDates(new DateTime(2022, 01 ,03), new DateTime(2023, 01, 01)));

            try
            {
                CheckForDraft();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }
            
            NameDayRythmplan("2022 kopi");

            ClickOKSaveDayRythplan();
            Playback.Wait(2000);

            return errorList;
        }

        public List<string> CheckDayRythmplanDates(DateTime fromDate, DateTime toDate)
        {
            var errorList = new List<string>();
            try
            {
                UICommon.CheckDayRythmplanFromDate(fromDate);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }
            try
            {
                UICommon.CheckDayRythmplanToDate(toDate);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            return errorList;
        }
        
        public void ClickNewDayRythmplan()
        {
            #region Variable Declarations
            DXButton uINYButton = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UINYButton;
            #endregion

            // Click '&Ny' button
            Mouse.Click(uINYButton, new Point(1, 1));
        }
        public void NameDayRythmplan(string name)
        {
            #region Variable Declarations
            DXTextEdit uIENameEdit = this.UIDøgnrytmeplanWindow.UIPaNavbarPanelClient.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIENameEdit;
            #endregion
            
            uIENameEdit.ValueAsString = name;
            Keyboard.SendKeys(uIENameEdit, "{TAB}");
        }

        private List<string> CreateSykepleierLayer()
        {
            var errorList = new List<string>();

            ClickNewLayer();
            AddLayerName("Sykepleier");

            try
            {
                UICommon.CheckRythmLayerFromDate(new DateTime(2022, 01, 03));
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer fromdate: " + e.Message);
            }

            try
            {
                CheckRythmLayerToDate();
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer todate: " + e.Message);
            }

            try
            {
                CheckRythmLayerDays();
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer days: " + e.Message);
            }

            try
            {
                CheckRythmLayerType();
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer type: " + e.Message);
            }

            try
            {
                SelectLayerName();
                Keyboard.SendKeys("{TAB 4}");
            }
            catch (Exception e)
            {
                errorList.Add("Error using Tab to navigate: " + e.Message);
            }

            SelectRotateWholePeriod();

            return errorList;
        }

        public List<string> CreateHjelpepleierLayer()
        {
            var errorList = new List<string>();

            ClickNewLayer();
            AddLayerName("Hjelpepleier");

            try
            {
                UICommon.SetDayRythmLayerFromDate(new DateTime(2022, 01, 10));
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer fromdate: " + e.Message);
            }

            try
            {
                CheckLayerToDate(new DateTime(2022, 01, 16));
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer todate: " + e.Message);
            }

            try
            {
                CheckRythmLayerDays();
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer days: " + e.Message);
            }

            try
            {
                CheckRythmLayerType();
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer type: " + e.Message);
            }

            try
            {
                SelectLayerName();
                Keyboard.SendKeys("{TAB 4}");
            }
            catch (Exception e)
            {
                errorList.Add("Error using Tab to navigate: " + e.Message);
            }

            SelectRotate4Times();
            SendTabToRotateXtimesTextBox();

            try
            {
                CheckToDateRotate4Times();
            }
            catch (Exception e)
            {
                errorList.Add("Error in rotatelayer toDate: " + e.Message);
            }

            return errorList;
        }

        public List<string> EditHjelpepleierLayer()
        {
            var errorList = new List<string>();

            SelectFirstLayer();
            ClickEditLayer();

            try
            {
                UICommon.SetLayerRotateToDate(new DateTime(2022, 05, 01));
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer rotate todate: " + e.Message);
            }

            try
            {
                UICommon.CheckLayerRotateToDays();
            }
            catch (Exception e)
            {
                errorList.Add("Error in days to rotate: " + e.Message);
            }

            return errorList;
        }

        public List<string> DuplicateSykeplayerLayer(string layerName)
        {
            var errorList = new List<string>();
            SelectSecondLayer();
            DuplicateLayer();
            AddLayerName(layerName);


            try
            {
                UICommon.CheckRythmLayerFromDate(new DateTime(2022, 01, 03));
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer fromdate: " + e.Message);
            }

            try
            {
                CheckLayerToDate(new DateTime(2022, 01, 09));
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer todate: " + e.Message);
            }

            try
            {
                CheckRythmLayerDays();
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer days: " + e.Message);
            }

            try
            {
                CheckRythmLayerType();
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer type: " + e.Message);
            }

            try
            {
                CheckRotateWholePeriodSelected();
            }
            catch (Exception e)
            {
                errorList.Add("Error in rotation type: " + e.Message);
            }


            return errorList;
        }

        public List<string> EditDuplicatedSykeplayerLayer()
        {
            var errorList = new List<string>();

            try
            {
                UICommon.SetDayRythmLayerFromDate(new DateTime(2022, 04, 11));
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer fromdate: " + e.Message);
            }

            SetLayerDays();

            try
            {
                CheckLayerToDate(new DateTime(2022, 04, 24));
            }
            catch (Exception e)
            {
                errorList.Add("Error in layer todate: " + e.Message);
            }

            SelectLayerType();

            SelectNoRotation();

            return errorList;
        }

        public void AddLayerName(string name)
        {
            #region Variable Declarations
            DXTextEdit uIENameEdit = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIENameEdit;
            #endregion

            // Type 'Sykepleier' in 'eName' text box
            //ValueAsString
            uIENameEdit.ValueAsString = name;
            Keyboard.SendKeys(uIENameEdit, "{TAB}");
        }

        private void SelectLayerName()
        {
            #region Variable Declarations
            DXTextEdit uIENameEdit = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIENameEdit;
            #endregion

            Mouse.Click(uIENameEdit);
        }

        public void SetLayerDays()
        {
            #region Variable Declarations
            DXTextEdit uIENumber1Edit = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIENumber1Edit;
            #endregion

            Keyboard.SendKeys(uIENumber1Edit, "14");
            Keyboard.SendKeys(uIENumber1Edit, "{TAB}");
        }

        public void SelectLayerType()
        {
            #region Variable Declarations
            DXLookUpEdit uIDrdLayerTypeLookUpEdit = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDrdLayerTypeLookUpEdit;
            DXWindow uIPopupLookUpEditFormWindow = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDrdLayerTypeLookUpEdit.UIPopupLookUpEditFormWindow;
            #endregion

            uIDrdLayerTypeLookUpEdit.SelectedIndex = 1;
            Keyboard.SendKeys(uIDrdLayerTypeLookUpEdit, "{TAB}");
        }

        public void SelectNoRotation()
        {
            #region Variable Declarations
            DXRadioGroup uIGsRotationRadioGroup = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIGsRotationRadioGroup;
            #endregion

            // Type '3' in 'gsRotation' RadioGroup
            //SelectedIndex
            uIGsRotationRadioGroup.SelectedIndex = 0;
            Keyboard.SendKeys(uIGsRotationRadioGroup, "{TAB}");
        }

        public void CheckLayerToDate(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtCalculatedToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            Assert.AreEqual(date, uIPceDateDateTimeEdit.DateTime, "Feil tildato");
        }

        public void CheckRotateWholePeriodSelected()
        {
            #region Variable Declarations
            DXRadioGroup uIGsRotationRadioGroup = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIGsRotationRadioGroup;
            #endregion

            Assert.AreEqual(this.SelectRotateWholePeriodParams.UIGsRotationRadioGroupSelectedIndex, uIGsRotationRadioGroup.SelectedIndex, "Feil rulleringstype");
        }

        public List<string> CheckTaskRequirements()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            DXCell uIItemCell11 = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccLayersScrollableControl.UIGcItemsTable.UIItemCell11;
            DXMenuBaseButtonItem uIBarButtonItemLink3MenuBaseButtonItem = this.UIItemWindow.UIPopupMenuBarControlMenu.UIBarButtonItemLink3MenuBaseButtonItem;
            #endregion

            // Right-Click cell
            Mouse.Click(uIItemCell11, MouseButtons.Right);

            // Click 'BarButtonItemLink[3]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink3MenuBaseButtonItem, new Point(74, 9));
            try
            {
                CheckMed1ReqDetails();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            ClickCancelRequirements();

            return errorList;
        }

        private void SendTabToRotateXtimesTextBox()
        {
            DXTextEdit uIENumber0Edit = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIENumber0Edit;
            Keyboard.SendKeys(uIENumber0Edit, "{TAB}");
        }

        public void SelectDayRythmPlan(string planName, bool openPlan = true, bool doubleClickOpenPlan = false)
        {
            UICommon.UIMapVS2017.SelectDayRythmPlan(planName, openPlan, doubleClickOpenPlan);
        }

        public bool CheckPlanDeleted(string plan)
        {
            #region Variable Declarations 
            var dayRythmPlanGrid = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIGcDayrhythmPlansTable;
            #endregion

            var view = dayRythmPlanGrid.Views[0];
            plan = plan.Trim().ToLower();
            for (int i = 0; i < view.RowCount; i++)
            {
                var val = view.GetCellValue("gcolPlan", i).ToString().Trim().ToLower();

                if (val == plan)
                {
                    return true;
                }
            }

            return false;
        }

        public List<string> ExportAsExcel(string stepName)
        {
           return  SaveAsExcel(stepName);
        }
        private List<string> SaveAsExcel(string stepName)
        {
            #region Variable Declarations
            var errorList = new List<string>();
            #endregion
            try
            {
                var fileName = ReportFilePath + ReportFileName + stepName;
                UICommon.ExportToExcel(fileName);
            }
            catch (Exception e)
            {
                errorList.Add("Feil ved export til excel(" + stepName + "): " + e.Message);
            }

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
    }
}
