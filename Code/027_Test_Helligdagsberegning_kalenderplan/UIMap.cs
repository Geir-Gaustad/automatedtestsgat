namespace _027_Test_Helligdagsberegning_kalenderplan
{
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
    using CommonTestData;
    using System.Diagnostics;
    using System.Threading;
    using System.Globalization;

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        #endregion

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

        private UIMapVS2017Classes.UIMapVS2017 map2017;

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
        public void StartGat()
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment(UICommon.DepFrikoder);
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

        private void SelectRosterplanTab(int delay = 0)
        {
            Playback.Wait(delay * 1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }

        public void OpenPlan(string planName)
        {
            Playback.Wait(1500);
            SelectRosterplanTab();
            Playback.Wait(3000);
            UICommon.SelectRosterPlan(planName);
        }
        public void ClosePlan()
        {
            try
            {
                Playback.Wait(1000);
                UICommon.XcloseRosterplan();
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e.Message);
            }

            Playback.Wait(2000);
        }
        public List<string> CheckEmployees_step_1()
        {
            var errorList = new List<string>();

            try
            {
                UIMapVS2017.CheckEmployees_step_1();
            }
            catch (Exception e)
            {
                errorList.Add("Error in step 1: " + e.Message);
            }

            return errorList;
        }
      
        public void CreateHelpPlan(bool first)
        {
            var errorList = new List<string>();
            Playback.Wait(3000);
            UICommon.SelectRosterplanPlanTab();
            UICommon.SelectNewHelpplan();

            UICommon.SetStartDateNewHelpplan(new DateTime(2021, 04, 26));

            Playback.Wait(1000);

            UICommon.SetHelpPlanWeeks("5");
            Playback.Wait(1000);

            if (first)
            {
                UICommon.SetF3CalculationPeriodHelpplan(new DateTime(2021, 05, 03), new DateTime(2021, 05, 30));
                SetEmployeeF3Calculations();
            }
            else
            {
                SetEmployeeF3CalculationsStep4();
            }

            UICommon.ClickOkCreateHelpPlan();
        }

        public void DeleteHelpPlan()
        {
            UICommon.SelectRosterPlan("Hjelpeplan for BEREGNING F3 - F5.", false);
            UICommon.ClickDeleteRosterplanButton();
        }
        public void SelectHelgeOgHoytidsbergeningFilter()
        {
            Playback.Wait(3000);
            UICommon.ClickRosterplanFilterTab();
            UICommon.UIMapVS2017.SelectViewFilter("Helge");
        }
        private void OpenEmployeesInPlan()
        {
            UICommon.SelectPlanTabRosterplan();
            UICommon.ClickEmployeesButtonRosterplan();
        }

        public void ChangeCalculationTypeEmployees()
        {
            OpenEmployeesInPlan();
            ChangeEmployeesCalculationType();
            UICommon.ClickOkEmployeesWindow();
        }

        public List<string> ChangeCalculationTypeIbrahimovicMildAndLarson()
        {
            var errorList = new List<string>();

            OpenEmployeesInPlan();
            ChangeIbrahimovicCalculationType();
            ChangeMildCalculationType();
            ChangeLarsonCalculationType();
            UIMapVS2017.SelectVacant();
            Playback.Wait(1000);
            try
            {
                CheckVacantF3Disabled();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            UICommon.ClickOkEmployeesWindow();

            return errorList;
        }

        private void ChangeMildCalculationType()
        {
            UIMapVS2017.ChangeMildCalculationType();
        }

        public List<string> ChangeCalculationTypeLarson()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            var uILueF3CalculationOptiLookUpEdit = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerF3CalCustom.UILueF3CalculationOptiLookUpEdit;
            //var uIItem6LarssonHenrikTreeListCell = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode5TreeListNode.UIItem6LarssonHenrikTreeListCell;
            var uILarssonCell = UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode4TreeListNode.UIItem5MildHåkanTreeListCell;

            var uIItem7VAKANTTreeListCell = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeesTreeList.UINode6TreeListNode.UIItem7VAKANTTreeListCell;
            #endregion

            OpenEmployeesInPlan();

            // Click '6. Larsson, Henrik' TreeListCell
            Mouse.Click(uILarssonCell);

            uILueF3CalculationOptiLookUpEdit.ValueTypeName = this.ChangeIbrahimovicCalculationTypeParams.UILueF3CalculationOptiLookUpEditValueTypeName;

            // Type 'Annenhver' in 'lueF3CalculationOption' LookUpEdit
            uILueF3CalculationOptiLookUpEdit.ValueAsString = this.ChangeIbrahimovicCalculationTypeParams.UILueF3CalculationOptiLookUpEditValueAsString; ;

            UICommon.ClickOkEmployeesWindow();

            try
            {
               UIMapVS2017.CheckLarsonF3();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 20)");
            }
            
            return errorList;
        }

        private void ChangeLarsonCalculationType()
        {
          UIMapVS2017.ChangeLarsonCalculationType();
        }

        public void ChangeRevolvingToDateMild()
        {
            OpenEmployeesInPlan();
            SelectMildInList();

            UICommon.SetRevolvingPeriodInEmployeeWindow(null, new DateTime(2021, 05, 23));
            UICommon.ClickOkEmployeesWindow();
        }

        public void AddEmployeesToRosterPlan()
        {
            OpenEmployeesInPlan();

            UICommon.ClickEmployeesButtonInEmployeeWindow();
            SelectIbrahimovi_Larsson_Mild();
            UICommon.ClickOkAddEmployeesWindow();
            AddVakantToPlan();
            UICommon.ClickOkEmployeesWindow();
        }

        public void AddEmployeesToHelpPlan()
        {
            OpenEmployeesInPlan();
            UICommon.ClickAddEmployeesFromBaseplanButtonInEmployeeWindow();
            CheckSortingInAddEmpsFromBaseplan();
            UICommon.SelectAllEmployeesInAddEmployeesFromBaseplanWindow();
            UICommon.ClickOkAddEmployeesFromBaseplanWindow();

            UICommon.ClickOkEmployeesWindow();
        }

        /// <summary>
        /// CheckSortingInAddEmpsFromBaseplan - Use 'CheckSortingInAddEmpsFromBaseplanExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckSortingInAddEmpsFromBaseplan()
        {
            #region Variable Declarations
            DXCell uIIbrahimovicZlatanCell = this.UILeggtilansatteWindow.UIViewHostCustom.UIPcViewClient.UISelectPlanEmployeesVCustom.UIPcContentContainerClient.UIPcContentClient.UIGcPlanEmployeesTable.UIIbrahimovicZlatanCell;
            DXCell uIVAKANTCell = this.UILeggtilansatteWindow.UIViewHostCustom.UIPcViewClient.UISelectPlanEmployeesVCustom.UIPcContentContainerClient.UIPcContentClient.UIGcPlanEmployeesTable.UIVAKANTCell;
            #endregion

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    CheckEmpFromBaseplanSorting();
                    break;
                }
                catch (Exception)
                {
                    ClickReverseSorting();
                }
            }
        }
                
        public void AddShiftsInRosterplan()
        {
            UICommon.SelectRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();

            InsertIbrahimovicshifts();
            InsertLarssonshifts();
            InsertMildVakantshifts();

            UICommon.ClickOKEditRosterPlanFromPlantab();
        }

        public List<string> ChecksStep4()
        {
            var errorList = new List<string>();

            try
            {
                UIMapVS2017.CheckAnderssonStep4();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 4)");
            }
            try
            {
                UIMapVS2017.CheckBrolinStep4();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 4)");
            }

            try
            {
                UIMapVS2017.CheckDahlinStep4();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 4)");
            }

            try
            {
                UIMapVS2017.CheckElmanderStep4();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 4)");
            }            

            return errorList;
        }

        public List<string> ChecksStep8()
        {
            var errorList = new List<string>();

            try
            {
                UIMapVS2017.CheckAnderssonStep8();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 8)");
            }
            try
            {
                UIMapVS2017.CheckBrolinStep8();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 8)");
            }

            try
            {
                UIMapVS2017.CheckDahlinStep8();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 8)");
            }

            try
            {
                UIMapVS2017.CheckElmanderStep8();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 8)");
            }

            return errorList;
        }

        public List<string> ChecksStep9()
        {
            var errorList = new List<string>();

            try
            {
                UIMapVS2017.CheckAnderssonStep9();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 9)");
            }

            try
            {
                UIMapVS2017.CheckBrolinStep9();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 9)");
            }

            try
            {
                UIMapVS2017.CheckDahlinStep9();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 9)");
            }

            try
            {
                UIMapVS2017.CheckElmanderStep9();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 9)");
            }

            return errorList;
        }
        

        public List<string> ChecksStep19()
        {
            var errorList = new List<string>();

            try
            {
                UIMapVS2017.CheckIbrahimovicCalculationsStep19();                
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 19)");
            }
            
            try
            {
                UIMapVS2017.CheckLarssonCalculationsStep19();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 19)");
            }

            try
            {
                UIMapVS2017.CheckMildCalculationsStep19();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 19)");
            }

            try
            {
                UIMapVS2017.CheckVakantCalculationsStep19();
            }
            catch (Exception e)
            {
                errorList.Add(e.Message + "(step 19)");
            }

            return errorList;
        }

        private void CheckVacantF3Disabled()
        {
            var F3 = UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerF3CalCustom;
            if (F3.Exists)
                throw new Exception("Beregningsperiode for F3 er tilgjengelig");
        }


        public virtual CheckSortingInAddEmpsFromBaseplanExpectedValues CheckSortingInAddEmpsFromBaseplanExpectedValues
        {
            get
            {
                if ((this.mCheckSortingInAddEmpsFromBaseplanExpectedValues == null))
                {
                    this.mCheckSortingInAddEmpsFromBaseplanExpectedValues = new CheckSortingInAddEmpsFromBaseplanExpectedValues();
                }
                return this.mCheckSortingInAddEmpsFromBaseplanExpectedValues;
            }
        }

        private CheckSortingInAddEmpsFromBaseplanExpectedValues mCheckSortingInAddEmpsFromBaseplanExpectedValues;
    }
    /// <summary>
    /// Parameters to be passed into 'CheckSortingInAddEmpsFromBaseplan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class CheckSortingInAddEmpsFromBaseplanExpectedValues
    {

        #region Fields
        /// <summary>
        /// Verify that the 'ValueAsString' property of 'Ibrahimovic, Zlatan' cell equals 'Ibrahimovic, Zlatan'
        /// </summary>
        public string UIIbrahimovicZlatanCellValueAsString = "Ibrahimovic, Zlatan";

        /// <summary>
        /// Verify that the 'ValueAsString' property of 'VAKANT' cell equals 'VAKANT'
        /// </summary>
        public string UIVAKANTCellValueAsString = "VAKANT";
        #endregion
    }
}
