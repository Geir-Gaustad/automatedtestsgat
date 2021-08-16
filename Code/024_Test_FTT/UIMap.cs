namespace _024_Test_FTT
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
    using System.Globalization;
    using System.Threading;


    public partial class UIMap
    {

        #region Fields
        private const int WaitForControl = 3000;
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
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

        public void StartGat(bool logInfo, string user = "")
        {
            UICommon.LaunchGatturnus(false);

            if (user == "")
                UICommon.LoginGatAndSelectDepartment(UICommon.DepFasteTillegg, null, "", logInfo);
            else
                UICommon.LoginGat(user, logInfo);
        }

        public void CloseGat()
        {
            try
            {
                UICommon.CloseGat();
                Playback.Wait(3000);
                SupportFunctions.KillGatProcess(TestContext);
            }
            catch (Exception)
            {
                SupportFunctions.KillGatProcess(TestContext);
            }
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
        public List<string> TimeLapseInSeconds(DateTime timeBefore, DateTime timeAfter, string text)
        {
            List<string> errorList = new List<string>();
            string elapsedTimeOutput = "";

            errorList.AddRange(LoadBalanceTesting.TimeLapseInSeconds(timeBefore, timeAfter, text, out elapsedTimeOutput));
            TestContext.WriteLine(elapsedTimeOutput);

            return errorList;
        }

        /// <summary>
        /// OpenPlan
        /// </summary>
        public void OpenPlan(string planName)
        {
            Playback.Wait(1500);
            SelectRosterplanTab();
            SelectRosterPlan(planName);
        }

        public void OpenPlanPOuser(string planName)
        {
            Playback.Wait(1500);
            SelectMainRosterplanTabPOuser();
            SelectRosterPlan(planName);
        }
        public void ShowInactive()
        {
            UICommon.UIMapVS2017.SelectShowInactiveLinesInFilter();
        }
        /// <summary>
        /// SelectRosterplanTab
        /// </summary>
        private void SelectRosterplanTab(int delay = 0)
        {
            Playback.Wait(delay * 1000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Rosterplan);
        }
        private void SelectRosterPlan(string planName)
        {
            Playback.Wait(1000);
            UICommon.SelectRosterPlan(planName);
        }
        public void SelectFromAdministration(string searchString)
        {
            Playback.Wait(3000);
            UICommon.SelectMainWindowTab(SupportFunctions.MainWindowTabs.Administration);
            UICommon.SelectFromAdministration(searchString);
        }

        #region Set new dates
        /// <summary>
        /// SetRosterplanStartDateNew - Use 'SetRosterplanStartDateNewParams' to pass parameters into this method.
        /// </summary>
        public void SetRosterplanStartDateNew(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIDeStartDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.WaitForControlExist(WaitForControl);

            uIPceDateDateTimeEdit.DateTime = date;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");
        }

        /// <summary>
        /// SetRosterplanValidDateNew - Use 'SetRosterplanValidDateNewParams' to pass parameters into this method.
        /// </summary>
        public void SetRosterplanValidDateNew(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEValidToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.WaitForControlExist(WaitForControl);

            uIPceDateDateTimeEdit.DateTime = date;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");
        }

        /// <summary>
        /// SetCalendarplanStartDateNew - Use 'SetCalendarplanStartDateNewParams' to pass parameters into this method.
        /// </summary>
        public void SetCalendarplanStartDateNew(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UINykalenderplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIDeStartDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.WaitForControlExist(WaitForControl);

            uIPceDateDateTimeEdit.DateTime = date;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");
        }

        /// <summary>
        /// SetBaseplanStartDateNew - Use 'SetBaseplanStartDateNewParams' to pass parameters into this method.
        /// </summary>
        public void SetBaseplanStartDateNew(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UINybaseplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIDeStartDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.WaitForControlExist(WaitForControl);

            uIPceDateDateTimeEdit.DateTime = date;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");
        }

        /// <summary>
        /// SetFTTStopdateNew - Use 'SetFTTStopdateNewParams' to pass parameters into this method.
        /// </summary>
        public void SetFTTStopdateNew(DateTime date)
        {
            #region Variable Declarations
            DXTextEdit uIDeDateEdit = this.UIStopptilleggWindow.UIDeDateEdit;
            #endregion

            uIDeDateEdit.DateTime = date;
            Keyboard.SendKeys(uIDeDateEdit, "{TAB}");
        }


        /// <summary>
        /// SetCreateWhishplanDatesNew - Use 'SetCreateWhishplanDatesNewParams' to pass parameters into this method.
        /// </summary>
        public void SetCreateWhishplanDatesNew()
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIØnskeplanperiodeWindow.UIGrpPeriodClient.UIDeFromDateCustom.UIPceDateDateTimeEdit;
            DXTextEdit uIENumberEdit = this.UIØnskeplanperiodeWindow.UIGrpPeriodClient.UIENumberEdit;
            #endregion

            uIPceDateDateTimeEdit.WaitForControlExist(WaitForControl);

            uIPceDateDateTimeEdit.DateTime = this.SetCreateWhishplanDatesNewParams.UIPceDateDateTimeEditValueAsDate;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");

            uIENumberEdit.ValueAsString = this.SetCreateWhishplanDatesNewParams.UIENumberEditValueAsString;
            Keyboard.SendKeys(uIENumberEdit, "{TAB}");
        }

        /// <summary>
        /// EditLinesettingDatesNew - Use 'EditLinesettingDatesNewParams' to pass parameters into this method.
        /// </summary>
        public void EditLinesettingDatesNew(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UILinjeinnstillingerEkWindow.UIGsPanelControl3Client.UIERevolveFromCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UILinjeinnstillingerEkWindow.UIGsPanelControl3Client.UIERevolveToCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.WaitForControlExist(WaitForControl);

            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, "{TAB}");
            }
        }

        /// <summary>
        /// SetLineSettingsValidPeriod - Use 'SetLineSettingsValidPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetLineSettingsValidPeriod(DateTime? fromDate, DateTime? toDate, bool closeLineSettingsWindow = true)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UISdeFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIGyldighetsperiodeWindow.UIPcContentClient.UISdeToDateCustom.UIPceDateDateTimeEdit;
            DXButton uIGSSimpleButtonButton = this.UIGyldighetsperiodeWindow.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            DXButton uIGSSimpleButtonButton1 = this.UILinjeinnstillingerStWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            uIPceDateDateTimeEdit.WaitForControlExist(WaitForControl);

            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}");
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, "{TAB}");
            }

            Playback.Wait(500);
            Mouse.Click(uIGSSimpleButtonButton);
            Playback.Wait(500);

            if (closeLineSettingsWindow)
                Mouse.Click(uIGSSimpleButtonButton1);
        }

        #endregion

        #region Edit RosterplanDates

        /// <summary>
        /// ExtendRosterperiodWith3Weeks - Use 'ExtendRosterperiodWith3WeeksParams' to pass parameters into this method.
        /// </summary>
        public void ExtendRosterperiodWith3Weeks()
        {
            #region Variable Declarations
            DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanKopiavFTTWindow.UIRcMenuRibbon.UIRpPlanRibbonPage;
            DXRibbonButtonItem uIInnstillingerRibbonBaseButtonItem = this.UIArbeidsplanKopiavFTTWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanRibbonPageGroup.UIInnstillingerRibbonBaseButtonItem;
            DXPopupEdit uIPceDate0PopupEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIPceDate0PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UIArbeidsplanInnstilliWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Click 'rpPlan' RibbonPage
            Mouse.Click(uIRpPlanRibbonPage);

            // Click 'Innstillinger' RibbonBaseButtonItem
            Mouse.Click(uIInnstillingerRibbonBaseButtonItem);

            SetRosterplanValidDateNew(new DateTime(2024, 09, 15));

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);

            CloseCurrentRosterplanFromPlanTab();
            OpenPlan("Kopi av FTT Arbeidsplan.");
            UICommon.ClickRosterplanPlanTab();
        }


        /// <summary>
        /// OpenPlanSettingsAndEditToDate - Use 'OpenPlanSettingsAndEditToDateParams' to pass parameters into this method.
        /// </summary>
        public void OpenPlanSettingsAndEditToDate()
        {
            #region Variable Declarations
            DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanKopiavFTTWindow13.UIRcMenuRibbon.UIRpPlanRibbonPage;
            DXRibbonButtonItem uIInnstillingerRibbonBaseButtonItem = this.UIArbeidsplanKopiavFTTWindow13.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanRibbonPageGroup.UIInnstillingerRibbonBaseButtonItem;
            DXPopupEdit uIPceDate0PopupEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIPceDate0PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UIArbeidsplanInnstilliWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Click 'rpPlan' RibbonPage
            Mouse.Click(uIRpPlanRibbonPage);

            // Click 'Innstillinger' RibbonBaseButtonItem
            Mouse.Click(uIInnstillingerRibbonBaseButtonItem, new Point(29, 20));

            SetRosterplanValidDateNew(this.OpenPlanSettingsAndEditToDateParams.UIPceDate0PopupEditValueAsDate);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        /// <summary>
        /// EditPlanSettingValidToDate - Use 'EditPlanSettingValidToDateParams' to pass parameters into this method.
        /// </summary>
        public void EditPlanSettingValidToDate()
        {
            #region Variable Declarations
            DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanVaktklassWindow14.UIRcMenuRibbon.UIRpPlanRibbonPage;
            DXRibbonButtonItem uIInnstillingerRibbonBaseButtonItem = this.UIArbeidsplanVaktklassWindow14.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanRibbonPageGroup.UIInnstillingerRibbonBaseButtonItem;
            DXPopupEdit uIPceDate0PopupEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIPceDate0PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UIArbeidsplanInnstilliWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Click 'rpPlan' RibbonPage
            Mouse.Click(uIRpPlanRibbonPage, new Point(24, 12));

            // Click 'Innstillinger' RibbonBaseButtonItem
            Mouse.Click(uIInnstillingerRibbonBaseButtonItem, new Point(28, 20));

            SetRosterplanValidDateNew(this.EditPlanSettingValidToDateParams.UIPceDate0PopupEditValueAsDate);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }


        /// <summary>
        /// ChangeTransferFromDate - Use 'ChangeTransferFromDateParams' to pass parameters into this method.
        /// </summary>
        public void ChangeTransferFromDate()
        {
            #region Variable Declarations
            DXTextEdit uIDeExportFromEdit = this.UIOverførtilleggWindow.UIDeExportFromEdit;
            #endregion
            
            uIDeExportFromEdit.DateTime = new DateTime(2024, 01, 23);
            Keyboard.SendKeys(uIDeExportFromEdit, "{TAB}");
        }

        //private DateTime GetNextDayOfWeekDay(DateTime from, DayOfWeek dayOfWeek)
        //{
        //    return SupportFunctions.NextDayOfWeekDay(from, dayOfWeek);
        //}

        /// <summary>
        /// CopyFTTRosterplan - Use 'CopyFTTRosterplanParams' to pass parameters into this method.
        /// </summary>
        public void CopyFTTRosterplan(DateTime date)
        {
            #region Variable Declarations
            DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanFTTArbeidWindow.UIRcMenuRibbon.UIRpPlanRibbonPage;
            DXRibbonButtonItem uINyarbeidsplanRibbonBaseButtonItem = this.UIArbeidsplanFTTArbeidWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanCopyRosterRibbonPageGroup.UINyarbeidsplanRibbonBaseButtonItem;
            DXPopupEdit uIPceDate1PopupEdit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIPceDate1PopupEdit;
            DXCheckBox uIChkKladdCheckBox = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIChkKladdCheckBox;
            DXButton uIOKButton = this.UINyarbeidsplanWindow.UIPnlButtonsClient.UIOKButton;
            DXButton uIGSSimpleButtonButton = this.UIItemWindow.UIGSSimpleButtonButton;
            #endregion

            // Click 'rpPlan' RibbonPage
            Mouse.Click(uIRpPlanRibbonPage, new Point(23, 10));

            // Click 'Ny arbeidsplan...' RibbonBaseButtonItem
            Mouse.Click(uINyarbeidsplanRibbonBaseButtonItem, new Point(54, 9));

            SetRosterplanStartDateNew(date);

            // Clear 'chkKladd' check box
            uIChkKladdCheckBox.Checked = this.CopyFTTRosterplanParams.UIChkKladdCheckBoxChecked;

            // Type '{Tab}' in 'chkKladd' check box
            Keyboard.SendKeys(uIChkKladdCheckBox, this.CopyFTTRosterplanParams.UIChkKladdCheckBoxSendKeys, ModifierKeys.None);

            // Click 'Ok' button
            Mouse.Click(uIOKButton, new Point(1, 1));

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton, new Point(1, 1));
        }


        /// <summary>
        /// CopyAPRosterplan - Use 'CopyAPRosterplanParams' to pass parameters into this method.
        /// </summary>
        public void CopyAPRosterplan()
        {
            #region Variable Declarations
            DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanVaktklassWindow12.UIRcMenuRibbon.UIRpPlanRibbonPage;
            DXRibbonButtonItem uINyarbeidsplanRibbonBaseButtonItem = this.UIArbeidsplanVaktklassWindow12.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanCopyRosterRibbonPageGroup.UINyarbeidsplanRibbonBaseButtonItem;
            DXTextEdit uITxtNameEdit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UITxtNameEdit;
            DXPopupEdit uIPceDate1PopupEdit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIPceDate1PopupEdit;
            DXCheckBox uIChkKladdCheckBox = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIChkKladdCheckBox;
            DXButton uIOKButton = this.UINyarbeidsplanWindow.UIPnlButtonsClient.UIOKButton;
            DXButton uIGSSimpleButtonButton = this.UIItemWindow.UIGSSimpleButtonButton;
            #endregion

            // Click 'rpPlan' RibbonPage
            Mouse.Click(uIRpPlanRibbonPage, new Point(25, 9));

            // Click 'Ny arbeidsplan...' RibbonBaseButtonItem
            Mouse.Click(uINyarbeidsplanRibbonBaseButtonItem, new Point(58, 9));

            // Type 'Vaktklasser AP kopi' in 'txtName' text box
            //ValueAsString
            uITxtNameEdit.ValueAsString = this.CopyAPRosterplanParams.UITxtNameEditValueAsString;

            // Type '{Tab}' in 'txtName' text box
            Keyboard.SendKeys(uITxtNameEdit, this.CopyAPRosterplanParams.UITxtNameEditSendKeys, ModifierKeys.None);

            SetRosterplanStartDateNew(this.CopyFTTKalenderplanParams.UIPceDate1PopupEditValueAsDate);

            // Clear 'chkKladd' check box
            uIChkKladdCheckBox.Checked = this.CopyAPRosterplanParams.UIChkKladdCheckBoxChecked;

            // Click 'Ok' button
            Mouse.Click(uIOKButton);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        /// <summary>
        /// CopyFTTBaseplan - Use 'CopyFTTBaseplanParams' to pass parameters into this method.
        /// </summary>
        public void CopyFTTBaseplan()
        {
            #region Variable Declarations
            DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanFFTBaseplWindow.UIRcMenuRibbon.UIRpPlanRibbonPage;
            DXRibbonButtonItem uINybaseplanRibbonBaseButtonItem = this.UIArbeidsplanFFTBaseplWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanCopyRosterRibbonPageGroup.UINybaseplanRibbonBaseButtonItem;
            DXPopupEdit uIPceDate1PopupEdit = this.UINybaseplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIPceDate1PopupEdit;
            DXCheckBox uIChkKladdCheckBox = this.UINybaseplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIChkKladdCheckBox;
            DXButton uIGSSimpleButtonButton = this.UINybaseplanWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            DXButton uIGSSimpleButtonButton1 = this.UIItemWindow.UIGSSimpleButtonButton;
            #endregion

            // Click 'rpPlan' RibbonPage
            Mouse.Click(uIRpPlanRibbonPage, new Point(26, 9));

            // Click 'Ny baseplan...' RibbonBaseButtonItem
            Mouse.Click(uINybaseplanRibbonBaseButtonItem, new Point(53, 10));

            SetBaseplanStartDateNew(this.CopyFTTBaseplanParams.UIPceDate1PopupEditValueAsDate);

            // Clear 'chkKladd' check box
            uIChkKladdCheckBox.Checked = this.CopyFTTBaseplanParams.UIChkKladdCheckBoxChecked;

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton1);
        }

        public void CopyCalendarplanCommon(string name)
        {
            #region Variable Declarations
            DXRibbonPage uIRpPlanRibbonPage = this.UIArbeidsplanFTTKalendWindow.UIRcMenuRibbon.UIRpPlanRibbonPage;
            DXRibbonButtonItem uINykalenderplanRibbonBaseButtonItem = this.UIArbeidsplanFTTKalendWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRpgPlanCopyRosterRibbonPageGroup.UINykalenderplanRibbonBaseButtonItem;
            DXTextEdit uITxtNameEdit = this.UINykalenderplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UITxtNameEdit;
            DXPopupEdit uIPceDate1PopupEdit = this.UINykalenderplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIPceDate1PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UINykalenderplanWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            DXButton uIGSSimpleButtonButton1 = this.UIItemWindow.UIGSSimpleButtonButton;
            #endregion
            
            // Click 'rpPlan' RibbonPage
            Mouse.Click(uIRpPlanRibbonPage);

            // Click 'Ny kalenderplan...' RibbonBaseButtonItem
            Mouse.Click(uINykalenderplanRibbonBaseButtonItem);

            if (name != "")
                uITxtNameEdit.ValueAsString = name;

            // Type '{Tab}' in 'txtName' text box
            Keyboard.SendKeys(uITxtNameEdit, this.CopyFTTKalenderplanParams.UITxtNameEditSendKeys, ModifierKeys.None);

            SetCalendarplanStartDateNew(this.CopyFTTKalenderplanParams.UIPceDate1PopupEditValueAsDate);
            
            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton1);
        }

        public void EditCalendarplanShift(bool chapter2)
        {
            UICommon.ClickRosterplanPlanTab();
            UICommon.ClickEditRosterPlanFromPlantab();
            if (chapter2)
                InsertAshiftMonday();
            else
                InsertAshiftMondayChapter3();

            UICommon.ClickOKEditRosterPlanFromPlantab();
            UICommon.ClickRosterplanHomeTab();
        }

        private void InsertAshiftMondayChapter3()
        {
            #region Variable Declarations
            DXCell uIF3Cell = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF3Cell;
            DXTextEdit uIRow0ColumnRosterCellEdit = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit;
            DXCell uIF3Cell1 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF3Cell1;
            DXTextEdit uIRow1ColumnRosterCellEdit = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow1ColumnRosterCellEdit;
            DXCell uIF3Cell2 = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIF3Cell2;
            DXTextEdit uIRow2ColumnRosterCellEdit = this.UIArbeidsplanWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow2ColumnRosterCellEdit;
            #endregion

            // Click 'F3' cell
            Mouse.Click(uIF3Cell, new Point(14, 9));

            // Type 'a{Tab}' in '[Row]0[Column]RosterCell_0' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit, this.InsertAshiftMondayParams.UIRow0ColumnRosterCellEditSendKeys, ModifierKeys.None);

            // Click 'F3' cell
            Mouse.Click(uIF3Cell1, new Point(12, 8));

            // Type 'a{Tab}' in '[Row]1[Column]RosterCell_0' text box
            Keyboard.SendKeys(uIRow1ColumnRosterCellEdit, this.InsertAshiftMondayParams.UIRow1ColumnRosterCellEditSendKeys, ModifierKeys.None);

            // Click 'F3' cell
            Mouse.Click(uIF3Cell2, new Point(10, 10));

            // Type '{DELETE}{Tab}' in '[Row]2[Column]RosterCell_0' text box
            Keyboard.SendKeys(uIRow2ColumnRosterCellEdit, this.InsertAshiftMondayParams.UIRow2ColumnRosterCellEditSendKeys, ModifierKeys.None);
        }

        private void ShowOldWishPlans()
        {
            Playback.Wait(1000);
            UICommon.ShowOldWishPlans();
            Playback.Wait(1000);
        }


        /// <summary>
        /// CreateWhishplanEaster2016 - Use 'CreateWhishplanEaster2016Params' to pass parameters into this method.
        /// </summary>
        public void CreateWhishplanEaster2016()
        {
            #region Variable Declarations
            DXTestControl uIXtraTabControlHeaderTabPage = this.UIGatver66041952ASCLAvWindow.UIPcMainPanelClient.UITcPanListsTabList.UIXtraTabControlHeaderTabPage;
            DXRibbonButtonItem uINYRibbonBaseButtonItem = this.UIGatver66041952ASCLAvWindow.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIRibbonControl1Ribbon.UIRpHomeRibbonPage.UIRpgPeriodRibbonPageGroup.UINYRibbonBaseButtonItem;
            DXTextEdit uITxtNameEdit = this.UIØnskeplanperiodeWindow.UIGrpPeriodClient.UITxtNameEdit;
            DXPopupEdit uIPceDate0PopupEdit = this.UIØnskeplanperiodeWindow.UIGrpPeriodClient.UIPceDate0PopupEdit;
            DXTextEdit uIENumberEdit = this.UIØnskeplanperiodeWindow.UIGrpPeriodClient.UIENumberEdit;
            DXPopupEdit uIPceDate1PopupEdit = this.UIØnskeplanperiodeWindow.UIGrpPeriodClient.UIPceDate1PopupEdit;
            DXRadioGroup uIRgrpPhaseControlRadioGroup = this.UIØnskeplanperiodeWindow.UIGrpPhaseControlClient.UIRgrpPhaseControlRadioGroup;
            DXButton uIGSSimpleButtonButton = this.UIØnskeplanperiodeWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Click 'XtraTabControlHeader' tab
            Mouse.Click(uIXtraTabControlHeaderTabPage, new Point(30, 11));

            // Click 'Ny' RibbonBaseButtonItem
            Mouse.Click(uINYRibbonBaseButtonItem, new Point(16, 21));

            // Type 'Påske 2016' in 'txtName' text box
            //ValueAsString
            uITxtNameEdit.ValueAsString = this.CreateWhishplanEaster2016Params.UITxtNameEditValueAsString;

            // Type '{Tab}' in 'txtName' text box
            Keyboard.SendKeys(uITxtNameEdit, this.CreateWhishplanEaster2016Params.UITxtNameEditSendKeys, ModifierKeys.None);

            SetCreateWhishplanDatesNew();

            // Type '0' in 'rgrpPhaseControl' RadioGroup
            //SelectedIndex
            uIRgrpPhaseControlRadioGroup.SelectedIndex = this.CreateWhishplanEaster2016Params.UIRgrpPhaseControlRadioGroupSelectedIndex;

            // Type '{Tab}' in 'rgrpPhaseControl' RadioGroup
            Keyboard.SendKeys(uIRgrpPhaseControlRadioGroup, this.CreateWhishplanEaster2016Params.UIRgrpPhaseControlRadioGroupSendKeys, ModifierKeys.None);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);

            ShowOldWishPlans();
        }

        private void OpenEmployeesFromPlan()
        {
            UICommon.ClickEmployeesButtonRosterplan();
        }

        #endregion

        #region Edit Linesettings

        /// <summary>
        /// OkEditLineSettings
        /// </summary>
        public void OkEditLineSettings()
        {
            #region Variable Declarations
            DXButton uIGSSimpleButtonButton1 = this.UILinjeinnstillingerStWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            OkEditValidPeriod();

            // Wait for 1 seconds for user delay between actions; Click 'GSSimpleButton' button
            Playback.Wait(500);
            Mouse.Click(uIGSSimpleButtonButton1);
        }

        /// <summary>
        /// EditLineSettingsLine2Ekland - Use 'EditLineSettingsLine2EklandParams' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsLine2Ekland()
        {
            #region Variable Declarations
            DXCell uIItemCell = this.UIArbeidsplanKopiavFTTWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXPopupEdit uIPceDate0PopupEdit = this.UILinjeinnstillingerEkWindow.UIGsPanelControl3Client.UIPceDate0PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UILinjeinnstillingerEkWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Wait for 1 seconds for user delay between actions; Right-Click cell
            Playback.Wait(500);
            Mouse.Click(uIItemCell, MouseButtons.Right, ModifierKeys.None, new Point(16, 10));

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(94, 8));

            EditLinesettingDatesNew(new DateTime(2024, 08, 19), null);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton, new Point(1, 1));
        }

        /// <summary>
        /// EditLineSettingsToDateGarbo_C1_S24 - Use 'EditLineSettingsToDateGarbo_C1_S24Params' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsToDateGarbo_C1_S24()
        {
            #region Variable Declarations
            DXCell uIDCell = this.UIArbeidsplanKopiavFTTWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIDCell;
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXPopupEdit uIPceDate1PopupEdit = this.UILinjeinnstillingerGaWindow.UIGsPanelControl3Client.UIPceDate1PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UILinjeinnstillingerGaWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Wait for 1 seconds for user delay between actions; Right-Click 'D' cell
            Playback.Wait(500);
            Mouse.Click(uIDCell, MouseButtons.Right, ModifierKeys.None, new Point(21, 9));

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(113, 10));

            EditLinesettingDatesNew(null, new DateTime(2024, 08, 12));

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        /// <summary>
        /// EditLineSettingsLine2Stormare - Use 'EditLineSettingsLine2StormareParams' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsLine2Stormare()
        {
            #region Variable Declarations
            DXCell uIACell = this.UIArbeidsplanFTTJanuarWindow.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIACell;
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXCheckBox uIChkAvailabilityPerioCheckBox = this.UILinjeinnstillingerStWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UIChkAvailabilityPerioCheckBox;
            DXButton uINYButton = this.UILinjeinnstillingerStWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UINYButton;
            DXPopupEdit uIPceDate1PopupEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UIPceDate1PopupEdit;
            DXPopupEdit uIPceDate0PopupEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UIPceDate0PopupEdit;
            #endregion

            // Wait for 1 seconds for user delay between actions; Right-Click 'A' cell
            Playback.Wait(500);
            Mouse.Click(uIACell, MouseButtons.Right, ModifierKeys.None, new Point(26, 10));

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(57, 16));

            // Select 'chkAvailabilityPeriodsDefined' check box
            uIChkAvailabilityPerioCheckBox.Checked = this.EditLineSettingsLine2StormareParams.UIChkAvailabilityPerioCheckBoxChecked;

            // Click 'Ny' button
            Mouse.Click(uINYButton);

            SetLineSettingsValidPeriod(this.EditLineSettingsLine2StormareParams.UIPceDate1PopupEditValueAsDate, this.EditLineSettingsLine2StormareParams.UIPceDate0PopupEditValueAsDate);
        }


        /// <summary>
        /// EditLine2SettingsStormare_C2_S23 - Use 'EditLine2SettingsStormare_C2_S23Params' to pass parameters into this method.
        /// </summary>
        public void EditLine2SettingsStormare_C2_S23()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXCheckBox uIChkAvailabilityPerioCheckBox = this.UILinjeinnstillingerStWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UIChkAvailabilityPerioCheckBox;
            DXButton uINYButton = this.UILinjeinnstillingerStWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UINYButton;
            #endregion

            // Wait for 1 seconds for user delay between actions; Right-Click cell
            Playback.Wait(500);
            UIMapVS2017.RightClickStormareLine2Cell();

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(134, 11));

            // Select 'chkAvailabilityPeriodsDefined' check box
            uIChkAvailabilityPerioCheckBox.Checked = this.EditLine2SettingsStormare_C2_S23Params.UIChkAvailabilityPerioCheckBoxChecked;

            // Click 'Ny' button
            Mouse.Click(uINYButton);

            SetLineSettingsValidPeriod(this.EditLine2SettingsStormare_C2_S23Params.UIPceDate1PopupEditValueAsDate, this.EditLine2SettingsStormare_C2_S23Params.UIPceDate0PopupEditValueAsDate);
        }


        /// <summary>
        /// EditLineSettingsLine1Persbrandt - Use 'EditLineSettingsLine1PersbrandtParams' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsLine1Persbrandt()
        {
            #region Variable Declarations
            DXCell uIDCell = this.UIArbeidsplanKopiavFTTWindow11.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIDCell;
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXCheckBox uIChkAvailabilityPerioCheckBox = this.UILinjeinnstillingerPeWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UIChkAvailabilityPerioCheckBox;
            DXButton uINYButton = this.UILinjeinnstillingerPeWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UINYButton;
            #endregion

            UIMapVS2017.RightClickPersbandtLine1();

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(147, 6));

            // Select 'chkAvailabilityPeriodsDefined' check box
            uIChkAvailabilityPerioCheckBox.Checked = this.EditLineSettingsLine1PersbrandtParams.UIChkAvailabilityPerioCheckBoxChecked;

            // Click 'Ny' button
            Mouse.Click(uINYButton);

            SetLineSettingsValidPeriod(this.EditLineSettingsLine1PersbrandtParams.UIPceDate1PopupEditValueAsDate, this.EditLineSettingsLine1PersbrandtParams.UIPceDate0PopupEditValueAsDate);
        }

        /// <summary>
        /// EditLineSettingsLine2Persbrandt - Use 'EditLineSettingsLine2PersbrandtParams' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsLine2Persbrandt()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXButton uIEndreButton = this.UILinjeinnstillingerPeWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UIEndreButton;
            #endregion

            UIMapVS2017.RightClickPersbandtLine2();

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(92, 13));

            // Click 'Endre' button
            Mouse.Click(uIEndreButton);

            SetLineSettingsValidPeriod(this.EditLineSettingsLine2PersbrandtParams.UIPceDate1PopupEditValueAsDate, this.EditLineSettingsLine2PersbrandtParams.UIPceDate0PopupEditValueAsDate);
        }

        /// <summary>
        /// EditLineSettingsRheborg_2 - Use 'EditLineSettingsRheborg_2Params' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsRheborg_2()
        {
            #region Variable Declarations
          DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXCheckBox uIChkAvailabilityPerioCheckBox = this.UILinjeinnstillingerRhWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UIChkAvailabilityPerioCheckBox;
            DXButton uINYButton = this.UILinjeinnstillingerRhWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UINYButton;
            DXPopupEdit uIPceDate1PopupEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UIPceDate1PopupEdit;
            DXPopupEdit uIPceDate0PopupEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UIPceDate0PopupEdit;
            #endregion

            // Right-Click 'Rheborg, Johan' cell
            UIMapVS2017.RightClickRheborgCell();

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(102, 12));

            // Select 'chkAvailabilityPeriodsDefined' check box
            uIChkAvailabilityPerioCheckBox.Checked = this.EditLineSettingsRheborg_2Params.UIChkAvailabilityPerioCheckBoxChecked;

            // Click 'Ny' button
            Mouse.Click(uINYButton);

            SetLineSettingsValidPeriod(this.EditLineSettingsRheborg_2Params.UIPceDate1PopupEditValueAsDate, this.EditLineSettingsRheborg_2Params.UIPceDate0PopupEditValueAsDate);
        }

        /// <summary>
        /// EditLineSettingsLine2Rheborg - Use 'EditLineSettingsLine2RheborgParams' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsLine2Rheborg()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXButton uIEndreButton = this.UILinjeinnstillingerRhWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UIEndreButton;
            DXPopupEdit uIPceDate0PopupEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UIPceDate0PopupEdit;
            #endregion

            UIMapVS2017.RightClickRheborgLine2Cell();

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(86, 9));

            // Click 'Endre' button
            Mouse.Click(uIEndreButton);

            SetLineSettingsValidPeriod(null, this.EditLineSettingsLine2RheborgParams.UIPceDate0PopupEditValueAsDate);
        }

        /// <summary>
        /// EditLineSettingsOscarsson_C6_S16 - Use 'EditLineSettingsOscarsson_C6_S16Params' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsOscarsson_C6_S16()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXCheckBox uIChkAvailabilityPerioCheckBox = this.UILinjeinnstillingerOsWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UIChkAvailabilityPerioCheckBox;
            DXButton uINYButton = this.UILinjeinnstillingerOsWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UINYButton;
            DXPopupEdit uIPceDate1PopupEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UIPceDate1PopupEdit;
            DXPopupEdit uIPceDate0PopupEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UIPceDate0PopupEdit;
            #endregion

            // Right-Click 'Oscarsson, Per' cell
            UIMapVS2017.RightClickOscarssonPerCell();

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(133, 12));

            // Select 'chkAvailabilityPeriodsDefined' check box
            uIChkAvailabilityPerioCheckBox.Checked = this.EditLineSettingsOscarsson_C6_S16Params.UIChkAvailabilityPerioCheckBoxChecked;

            // Click 'Ny' button
            Mouse.Click(uINYButton);

            SetLineSettingsValidPeriod(this.EditLineSettingsOscarsson_C6_S16Params.UIPceDate1PopupEditValueAsDate, this.EditLineSettingsOscarsson_C6_S16Params.UIPceDate0PopupEditValueAsDate, false);

            // Click 'Ny' button
            Mouse.Click(uINYButton);

            SetLineSettingsValidPeriod(this.EditLineSettingsOscarsson_C6_S16_1Params.UIPceDate1PopupEditValueAsDate, this.EditLineSettingsOscarsson_C6_S16_1Params.UIPceDate0PopupEditValueAsDate);
        }

        /// <summary>
        /// EditLine2SettingsOscarsson_C6_S21 - Use 'EditLine2SettingsOscarsson_C6_S21Params' to pass parameters into this method.
        /// </summary>
        public void EditLine2SettingsOscarsson_C6_S21()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXButton uIEndreButton = this.UILinjeinnstillingerOsWindow.UIGsPanelControl3Client.UIGcAvailabilityPeriodClient.UIEndreButton;
            DXPopupEdit uIPceDate1PopupEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UIPceDate1PopupEdit;
            DXPopupEdit uIPceDate0PopupEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UIPceDate0PopupEdit;
            #endregion

            // Right-Click cell
            UIMapVS2017.RightClickOscarssonPerCellLine2();

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(82, 7));

            // Click 'Endre' button
            Mouse.Click(uIEndreButton);

            SetLineSettingsValidPeriod(this.EditLine2SettingsOscarsson_C6_S21Params.UIPceDate1PopupEditValueAsDate, this.EditLine2SettingsOscarsson_C6_S21Params.UIPceDate0PopupEditValueAsDate);
        }

        /// <summary>
        /// EditLineSettingsNyquist_C6_S28 - Use 'EditLineSettingsNyquist_C6_S28Params' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsNyquist_C6_S28()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXPopupEdit uIPceDate1PopupEdit = this.UILinjeinnstillingerNyWindow.UIGsPanelControl3Client.UIPceDate1PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UILinjeinnstillingerNyWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Right-Click 'Nyqvist, Mikael' cell
            UIMapVS2017.RightClickNyquistCell();

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(125, 10));
            
            EditLinesettingDatesNew(null, this.EditLineSettingsNyquist_C6_S28Params.UIPceDate1PopupEditValueAsDate);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        /// <summary>
        /// EditLineSettingsKulle_C6_S30 - Use 'EditLineSettingsKulle_C6_S30Params' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsKulle_C6_S30()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXPopupEdit uIPceDate0PopupEdit = this.UILinjeinnstillingerKuWindow.UIGsPanelControl3Client.UIPceDate0PopupEdit;
            DXPopupEdit uIPceDate1PopupEdit = this.UILinjeinnstillingerKuWindow.UIGsPanelControl3Client.UIPceDate1PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UILinjeinnstillingerKuWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Right-Click 'Kulle, Nils' cell
            UIMapVS2017.RightClickKulleCell();

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(123, 9));

            EditLinesettingDatesNew(this.EditLineSettingsKulle_C6_S30Params.UIPceDate0PopupEditValueAsDate, this.EditLineSettingsKulle_C6_S30Params.UIPceDate1PopupEditValueAsDate);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        #endregion


        /// <summary>
        /// ReplaceAshiftWeek1AndDshiftWeek3 - Use 'ReplaceAshiftWeek1AndDshiftWeek3Params' to pass parameters into this method.
        /// </summary>
        public void ReplaceAshiftWeek1AndDshiftWeek3()
        {
            #region Variable Declarations
            DXCell uIACell = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIACell;
            DXTextEdit uIRow0ColumnRosterCellEdit = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit;
            DXCell uIDCell = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIDCell;
            DXTextEdit uIRow0ColumnRosterCellEdit1 = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit1;
            #endregion

            // Click 'A' cell
            Mouse.Click(uIACell, new Point(19, 10));

            // Type '{Tab}' in '[Row]0[Column]RosterCell_1' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit, "D" + this.ReplaceAshiftWeek1AndDshiftWeek3Params.UIRow0ColumnRosterCellEditSendKeys, ModifierKeys.None);

            // Click 'D' cell
            Mouse.Click(uIDCell, new Point(12, 7));

            // Type '{Tab}' in '[Row]0[Column]RosterCell_20' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit1, "f2" + this.ReplaceAshiftWeek1AndDshiftWeek3Params.UIRow0ColumnRosterCellEdit1SendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// ReplaceNshiftsWeek2AndF2shiftWeek3 - Use 'ReplaceNshiftsWeek2AndF2shiftWeek3Params' to pass parameters into this method.
        /// </summary>
        public void ReplaceNshiftsWeek2AndF2shiftWeek3()
        {
            #region Variable Declarations
            DXCell uINCell = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UINCell;
            DXTextEdit uIRow0ColumnRosterCellEdit2 = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit2;
            DXCell uINCell1 = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UINCell1;
            DXTextEdit uIRow0ColumnRosterCellEdit11 = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit11;
            DXCell uINCell2 = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UINCell2;
            DXTextEdit uIRow0ColumnRosterCellEdit21 = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit21;
            DXCell uIDCell = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIDCell;
            DXGrid uIGcRosterPlanTable = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable;
            DXTextEdit uIRow0ColumnRosterCellEdit1 = this.UIArbeidsplanØnskeplanWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIRow0ColumnRosterCellEdit1;
            #endregion

            // Click 'N' cell
            Mouse.Click(uINCell);
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit2, "{Delete}" + this.ReplaceNshiftsWeek2AndF2shiftWeek3Params.UIRow0ColumnRosterCellEdit2SendKeys, ModifierKeys.None);

            // Click 'N' cell
            Mouse.Click(uINCell1);

            // Type '{Tab}' in '[Row]0[Column]RosterCell_10' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit11, "a" + this.ReplaceNshiftsWeek2AndF2shiftWeek3Params.UIRow0ColumnRosterCellEdit11SendKeys, ModifierKeys.None);

            // Click 'N' cell
            Mouse.Click(uINCell2);

            // Type '{Tab}' in '[Row]0[Column]RosterCell_11' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit21, "D" + this.ReplaceNshiftsWeek2AndF2shiftWeek3Params.UIRow0ColumnRosterCellEdit21SendKeys, ModifierKeys.None);

            // Click 'D' cell
            Mouse.Click(uIDCell);

            // Type '{Tab}' in '[Row]0[Column]RosterCell_20' text box
            Keyboard.SendKeys(uIRow0ColumnRosterCellEdit1, "n" + this.ReplaceNshiftsWeek2AndF2shiftWeek3Params.UIRow0ColumnRosterCellEdit1SendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// ClickCalculateFFTButton
        /// </summary>
        public void ClickCalculateFFTButton(bool goToTab = false)
        {
            Playback.Wait(1000);
            UICommon.CalculateFTT(goToTab);
        }
        public void CheckCalculateFTTButtonDisabled()
        {
            UIMapVS2017.CheckCalculateFTTButtonDisabled();
        }

        /// <summary>
        /// SelectAllFTTTEmpsAndClickStop - Use 'SelectAllFTTTEmpsAndClickStopParams' to pass parameters into this method.
        /// </summary>
        public void SelectAllFTTTEmpsAndClickStop()
        {
            #region Variable Declarations
            DXTextEdit uIDeDateEdit = this.UIStopptilleggWindow.UIDeDateEdit;
            DXColumnHeader uIDXCheckboxSelectorCoColumnHeader = this.UIStopptilleggWindow.UIGcEmploymentsTable.UIDXCheckboxSelectorCoColumnHeader;
            DXGrid uIGcEmploymentsTable = this.UIStopptilleggWindow.UIGcEmploymentsTable;
            DXButton uIStoppButton = this.UIStopptilleggWindow.UIStoppButton;
            #endregion


            SetFTTStopdateNew(this.SelectAllFTTTEmpsAndClickStopParams.UIDeDateEditValueAsDate);

            // Click 'DX$CheckboxSelectorColumn' column header
            Mouse.Click(uIDXCheckboxSelectorCoColumnHeader, new Point(34, 9));

            // Type '{Tab}' in 'gcEmployments' table
            Keyboard.SendKeys(uIGcEmploymentsTable, this.SelectAllFTTTEmpsAndClickStopParams.UIGcEmploymentsTableSendKeys, ModifierKeys.None);

            // Click '&Stopp' button
            Mouse.Click(uIStoppButton, new Point(1, 1));
        }

        public List<string> CheckTransferVacantNotInList_C2_S4()
        {
            #region Variable Declarations
            var errorList = new List<string>();
            var checkCell = this.UIOverførtilleggWindow.UIGcExportsTable.UIBergmanIngridCell1;
            checkCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcExportsGridControlCell[View]gvExports[Row]3[Column]colEmployee";
            #endregion

            try
            {
                if (checkCell.Exists)
                    Assert.Fail("List has more records than expected");
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
            }

            checkCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcExportsGridControlCell[View]gvExports[Row]2[Column]colEmployee";
            return errorList;
        }

        public void SelectTuesdayWeek1Stellan()
        {
            DXCell uIACell = this.UIArbeidsplanFTTJanuarWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIACell;

            // Right-Click 'A' cell
            Mouse.Click(uIACell, MouseButtons.Right, ModifierKeys.None, new Point(7, 10));
        }

        public void SetTransfereFromDateFTT()
        {
            UICommon.SetTransfereFromFTT(new DateTime(2024, 01, 08));
        }

        public void AddHaberToPlan()
        {
            OpenEmployeesFromPlan();
            UIMapVS2017.AddHaberToPlan();
            UIMapVS2017.Add50PersentTo5140HaberEmployment();
            UICommon.ClickOkEmployeesWindow();
            //AddPeterHaber();
        }

        /// <summary>
        /// EditNewLineSettingsStormare - Use 'EditNewLineSettingsStormareParams' to pass parameters into this method.
        /// </summary>
        public void EditNewLineSettingsStormare()
        {
            #region Variable Declarations
            DXLookUpEdit uILeAvailablePositionCLookUpEdit = this.UILinjeinnstillingerStWindow.UIGsPanelControl3Client.UILeAvailablePositionCLookUpEdit;
            DXTextEdit uIENumber3Edit = this.UILinjeinnstillingerStWindow.UIGsPanelControl3Client.UIENumber3Edit;
            DXTextEdit uIEInternalPositionNumEdit = this.UILinjeinnstillingerStWindow.UIGsPanelControl3Client.UIEInternalPositionNumEdit;
            DXLookUpEdit uILeAvailableRulesetsLookUpEdit = this.UILinjeinnstillingerStWindow.UIGsPanelControl3Client.UILeAvailableRulesetsLookUpEdit;
            DXWindow uIPopupLookUpEditFormWindow = this.UILinjeinnstillingerStWindow.UIGsPanelControl3Client.UILeAvailableRulesetsLookUpEdit.UIPopupLookUpEditFormWindow;
            DXButton uIGSSimpleButtonButton = this.UILinjeinnstillingerStWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            // Type 'Gatsoft.Gat.DataModel.Simple+PositionCategory' in 'leAvailablePositionCategories' LookUpEdit
            //ValueTypeName
            //uILeAvailablePositionCLookUpEdit.ValueTypeName = this.EditNewLineSettingsStormareParams.UILeAvailablePositionCLookUpEditValueTypeName;
            Mouse.Click(uILeAvailablePositionCLookUpEdit);

            // Type '{Tab}' in 'leAvailablePositionCategories' LookUpEdit
            Keyboard.SendKeys(uILeAvailablePositionCLookUpEdit, this.EditNewLineSettingsStormareParams.UILeAvailablePositionCLookUpEditValueAsString + this.EditNewLineSettingsStormareParams.UILeAvailablePositionCLookUpEditSendKeys, ModifierKeys.None);

            // Type '25,00 [SelectionStart]0[SelectionLength]5' in 'eNumber[3]' text box
            //ValueAsString
            uIENumber3Edit.ValueAsString = this.EditNewLineSettingsStormareParams.UIENumber3EditValueAsString;

            // Type '1' in 'eInternalPositionNumber' text box
            //ValueAsString
            uIEInternalPositionNumEdit.ValueAsString = this.EditNewLineSettingsStormareParams.UIEInternalPositionNumEditValueAsString;

            // Type '{Tab}' in 'eInternalPositionNumber' text box
            Keyboard.SendKeys(uIEInternalPositionNumEdit, this.EditNewLineSettingsStormareParams.UIEInternalPositionNumEditSendKeys, ModifierKeys.None);

            // Type '' in 'leAvailableRulesets' LookUpEdit
            //ValueAsString
            uILeAvailableRulesetsLookUpEdit.ValueAsString = this.EditNewLineSettingsStormareParams.UILeAvailableRulesetsLookUpEditValueAsString;

            // Click 'PopupLookUpEditForm' window
            Mouse.Click(uIPopupLookUpEditFormWindow, new Point(61, 74));

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton, new Point(1, 1));
        }

        /// <summary>
        /// EditLineSettingsLine3Gustavsson - Use 'EditLineSettingsLine3GustavssonParams' to pass parameters into this method.
        /// </summary>
        public void EditLineSettingsLine3Gustavsson()
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow11.UIGSPanelControlClient.UIGSGroupControlClient.UIGSLookUpEditLookUpEdit;
            DXButton uIOKButton = this.UIItemWindow11.UIGsPanelControl2Client.UIOKButton;
            #endregion

            UIMapVS2017.RightClickGustavssonCellLine3();

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem);

            Mouse.Click(uIGSLookUpEditLookUpEdit);
            Keyboard.SendKeys(uIGSLookUpEditLookUpEdit, EditLineSettingsLine3GustavssonParams.UIGSLookUpEditLookUpEditSearchString + "{ENTER}");

            Playback.Wait(1000);

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }

        public void ClickCellToInsertA1shiftTueWeek2Line2Stormare()
        {
            InsertA1shiftTueWeek2Line2Stormare();
        }

        public void ClickCellToInsertA1shiftWedWeek3AndSatWeek4Line2Stormare()
        {
            var targetCell1 = UIArbeidsplanFTTKalendWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell2;
            var targetCell2 = UIArbeidsplanFTTKalendWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell11;

            Playback.Wait(500);
            Mouse.Click(targetCell1);
            InsertA1shiftWedWeek3Line2Stormare();

            Playback.Wait(500);
            Mouse.Click(targetCell2);
            InsertA1shiftSatWeek4Line2Stormare();
        }

        public void CreateEqualizeShiftWeek1Garbo()
        {
            ClickCellToCreateEqualizeShiftWeek1Garbo();
            UICommon.CreateEqualizeShift(true, true, "60");
        }

        public void ClickCellToInsertDshiftMonEkland()
        {  
            InsertDshiftMonEkland();
        }

        public void ClickCellToInsertNshiftMonEkland()
        {
            var targetCell = UIArbeidsplanKopiavFTTWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell1;

            Mouse.Click(targetCell);

            InsertNshiftMonEkland();
        }

        public void ClickCellToInsertNshiftMonWeek2_3_Ekland()
        {
            var targetCell1 = UIArbeidsplanKopiavFTTWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell2;
            var targetCell2 = UIArbeidsplanKopiavFTTWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell11;

            Mouse.Click(targetCell1);
            InsertNshiftMonWeek2Ekland();

            Playback.Wait(500);
            Mouse.Click(targetCell2);
            InsertNshiftMonWeek3Ekland();
        }

        public void ClickCellToInsertNshiftMonWeek3Stormare()
        {
            var targetCell = UIArbeidsplanFTTKalendWindow1.UIPnlRosterPlanClient.UIGcRosterPlanTable.UIItemCell;

            Mouse.Click(targetCell);

            InsertNshiftMonWeek3Stormare();
        }

        public void OkEditValidPeriod()
        {
            #region Variable Declarations
            var uIPceDate0PopupEdit = this.UIGyldighetsperiodeWindow.UIPcContentClient.UIPceDate0PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UIGyldighetsperiodeWindow.UIGSDialogFooterBarCustom.UIGSSimpleButtonButton;
            #endregion

            Keyboard.SendKeys(uIPceDate0PopupEdit, "{TAB}");

            // Wait for 1 seconds for user delay between actions; Click 'GSSimpleButton' button
            Playback.Wait(500);
            Mouse.Click(uIGSSimpleButtonButton);
        }


        public void SelectAllFTTTEmpsAndClickStop_C3_S7()
        {
            #region Variable Declarations
            DXTextEdit uIDeDateEdit = this.UIStopptilleggWindow.UIDeDateEdit;
            DXColumnHeader uIDXCheckboxSelectorCoColumnHeader = this.UIStopptilleggWindow.UIGcEmploymentsTable.UIDXCheckboxSelectorCoColumnHeader;
            DXGrid uIGcEmploymentsTable = this.UIStopptilleggWindow.UIGcEmploymentsTable;
            DXButton uIStoppButton = this.UIStopptilleggWindow.UIStoppButton;
            #endregion

            SetFTTStopdateNew(new DateTime(2024, 03, 18));

            //// Type 'Gatsoft.Date' in 'deDate' text box
            ////ValueTypeName
            //uIDeDateEdit.ValueTypeName = this.SelectAllFTTTEmpsAndClickStopParams.UIDeDateEditValueTypeName;

            //// Type '14.03.2016 [SelectionStart]0' in 'deDate' text box
            ////ValueAsString
            //uIDeDateEdit.ValueAsString = "14.03.2016 [SelectionStart]0";

            // Click 'DX$CheckboxSelectorColumn' column header
            Mouse.Click(uIDXCheckboxSelectorCoColumnHeader, new Point(34, 9));

            // Type '{Tab}' in 'gcEmployments' table
            Keyboard.SendKeys(uIGcEmploymentsTable, this.SelectAllFTTTEmpsAndClickStopParams.UIGcEmploymentsTableSendKeys, ModifierKeys.None);

            // Click '&Stopp' button
            Mouse.Click(uIStoppButton, new Point(1, 1));
        }

        /// <summary>
        /// OpenWhishplanEaster2016
        /// </summary>
        public void OpenWhishplanEaster2016(bool clickDetails)
        {
            #region Variable Declarations
            DXButton uIPåske2016Button = this.UIGatver66041952ASCLAvWindow.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIPåske2016Button;
            DXCell uIØnskeplanforKopiavFFCell = this.UIGatver66041952ASCLAvWindow.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIGcWishPeriodsTable.UIØnskeplanforKopiavFFCell;
            #endregion

            ShowOldWishPlans();
            
            if (clickDetails)
                Mouse.Click(uIPåske2016Button, new Point(3, 0));

            // Double-Click 'Ønskeplan for Kopi av FFT Baseplan' cell
            Mouse.DoubleClick(uIØnskeplanforKopiavFFCell, new Point(100, 9));
            Playback.Wait(3000);
        }

        /// <summary>
        /// AddNewLineFromDepartmentGustavsson - Use 'AddNewLineFromDepartmentGustavssonParams' to pass parameters into this method.
        /// </summary>
        public void AddNewLineFromDepartmentGustavsson()
        {
            #region Variable Declarations
            Playback.Wait(1500);
            DXLookUpEdit uIGSLookUpEditLookUpEdit = this.UIItemWindow11.UIGSPanelControlClient.UIGSLookUpEditLookUpEdit;
            DXTextEdit uIEInternalPositionNumEdit = this.UIItemWindow11.UIGSPanelControlClient.UIEInternalPositionNumEdit;
            DXLookUpEdit uILeAvailableRulesetsLookUpEdit = this.UIItemWindow11.UIGSPanelControlClient.UILeAvailableRulesetsLookUpEdit;
            //DXWindow uIPopupLookUpEditFormWindow = this.UIItemWindow11.UIGSPanelControlClient.UILeAvailableRulesetsLookUpEdit.UIPopupLookUpEditFormWindow;
            DXLookUpEdit uIGSLookUpEditLookUpEdit1 = this.UIItemWindow11.UIGSPanelControlClient.UIGSGroupControlClient.UIGSLookUpEditLookUpEdit;
            DXButton uIOKButton = this.UIItemWindow11.UIGsPanelControl2Client.UIOKButton;
            #endregion

            // Right-Click 'Gustavsson, Robert' cell
            UIMapVS2017.RightClickGustavssonCell();
            Playback.Wait(1000);
            UIMapVS2017.AddNewLineFromDepartment();

            Mouse.Click(uIGSLookUpEditLookUpEdit);
            // Type '{ENTER}{Tab}' in 'GSLookUpEdit' LookUpEdit
            Keyboard.SendKeys(uIGSLookUpEditLookUpEdit, this.AddNewLineFromDepartmentGustavssonParams.UIGSLookUpEditLookUpEditValueAsString + this.AddNewLineFromDepartmentGustavssonParams.UIGSLookUpEditLookUpEditSendKeys, ModifierKeys.None);

            // Type '1' in 'eInternalPositionNumber' text box
            //ValueAsString
            uIEInternalPositionNumEdit.ValueAsString = this.AddNewLineFromDepartmentGustavssonParams.UIEInternalPositionNumEditValueAsString;

            // Type '{Tab}' in 'eInternalPositionNumber' text box
            Keyboard.SendKeys(uIEInternalPositionNumEdit, this.AddNewLineFromDepartmentGustavssonParams.UIEInternalPositionNumEditSendKeys, ModifierKeys.None);

            // Type '' in 'leAvailableRulesets' LookUpEdit
            //ValueAsString
            uILeAvailableRulesetsLookUpEdit.ValueAsString = this.AddNewLineFromDepartmentGustavssonParams.UILeAvailableRulesetsLookUpEditValueAsString;

            Keyboard.SendKeys(uILeAvailableRulesetsLookUpEdit, "L38{TAB}");

            //// Click 'PopupLookUpEditForm' window
            //Mouse.Click(uIPopupLookUpEditFormWindow, new Point(53, 104));

            //// Type '{Tab}' in 'leAvailableRulesets' LookUpEdit
            //Keyboard.SendKeys(uILeAvailableRulesetsLookUpEdit, this.AddNewLineFromDepartmentGustavssonParams.UILeAvailableRulesetsLookUpEditSendKeys, ModifierKeys.None);

            Mouse.Click(uIGSLookUpEditLookUpEdit1);
            // Type '{ENTER}{Tab}' in 'GSLookUpEdit' LookUpEdit
            Keyboard.SendKeys(uIGSLookUpEditLookUpEdit1, "{DOWN 2}{ENTER}");

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }


        public List<string> CheckCalculations_C4_S26()
        {
            var errorList = new List<string>();
            var pivot = UIMapVS2017.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIFixedPaymentViewCustom.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid;

            try
            {
                UIMapVS2017.CheckCalculations_C4_S26();
            }
            catch (Exception e)
            {
                errorList.Add("Chapter_4 step_26: " + e.Message);
            }

            var rows = pivot.RowCount;

            if (rows != 4)
                errorList.Add("Chapter_4 step_26: Unexpected rows");

            if (errorList.Count == 0)
                TestContext.WriteLine("Chapter_4 step_26: OK");

            return errorList;
        }

        public void CheckCurrentRateDate_C6_S34(DateTime currentDate)
        {
            DXPivotGridFieldValue uIItem01012016PivotGridFieldValue = this.UIShiftClassCalculatioWindow.UIPgcResultsPivotGrid.UIItem01012016PivotGridFieldValue;

            var rel = uIItem01012016PivotGridFieldValue.Text;
            var exp = currentDate.ToShortDateString();

            Assert.AreEqual(rel, exp);
        }
               
        public void RightClickLennartLine1()
        {
            UIMapVS2017.RightClickLennartLine1();
        }

        /// <summary>
        /// EditLineSettingsLennartToDate - Use 'EditLineSettingsLennartToDateParams' to pass parameters into this method.
        /// </summary>
        public void EditLineSettings(string fromDate, string toDate)
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow1.UIPopupMenuBarControlMenu.UIBarButtonItemLink1MenuBaseButtonItem;
            DXPopupEdit uIPceDate0PopupEdit = this.UILinjeinnstillingerJäWindow.UIGsPanelControl3Client.UIPceDate0PopupEdit;
            DXPopupEdit uIPceDate1PopupEdit = this.UILinjeinnstillingerJäWindow.UIGsPanelControl3Client.UIPceDate1PopupEdit;
            DXButton uIGSSimpleButtonButton = this.UILinjeinnstillingerJäWindow.UIGSPanelControlClient.UIGSSimpleButtonButton;
            #endregion

            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            if (fromDate != "")
                dateFrom = Convert.ToDateTime(fromDate);

            if (toDate != "")
                dateTo = Convert.ToDateTime(toDate);

            // Click 'BarButtonItemLink[1]' MenuBaseButtonItem
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem, new Point(114, 12));

            EditLinesettingDatesNew(dateFrom, dateTo);

            // Click 'GSSimpleButton' button
            Mouse.Click(uIGSSimpleButtonButton);
        }

        public bool CheckButtonShiftClassesExists()
        {
           return UIMapVS2017.CheckButtonShiftClassesExists();
        }

        public void CloseShiftClassesWindow()
        {
            Keyboard.SendKeys("{F4}", ModifierKeys.Alt);
        }

        /// <summary>
        /// AddNewShiftClassRate - Use 'AddNewShiftClassRateParams' to pass parameters into this method.
        /// </summary>
        public void AddNewShiftClassRate(DateTime date, string val1, string val2, string val3, string val4, string val5, string val6, string val7, string val8, string val9, string val10)
        {
            #region Variable Declarations
            DXButton uINYButton = this.UIVaktklassesatserWindow.UINYButton;
            DXTextEdit uIDeEffectiveFromEdit = this.UINyevaktklassesatserWindow.UIDeEffectiveFromEdit;
            DXCell uIItem100Cell = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIItem100Cell;
            DXGrid uIGcShiftClassRatesTable = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable;
            DXTextEdit uIRow0ColumncolAmountEdit = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIRow0ColumncolAmountEdit;
            DXCell uIItem200Cell = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIItem200Cell;
            DXTextEdit uIRow1ColumncolAmountEdit = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIRow1ColumncolAmountEdit;
            DXCell uIItem300Cell = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIItem300Cell;
            DXTextEdit uIRow2ColumncolAmountEdit = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIRow2ColumncolAmountEdit;
            DXCell uIItem400Cell = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIItem400Cell;
            DXTextEdit uIRow3ColumncolAmountEdit = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIRow3ColumncolAmountEdit;
            DXCell uIItem500Cell = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIItem500Cell;
            DXTextEdit uIRow4ColumncolAmountEdit = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIRow4ColumncolAmountEdit;
            DXCell uIItem600Cell = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIItem600Cell;
            DXTextEdit uIRow5ColumncolAmountEdit = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIRow5ColumncolAmountEdit;
            DXCell uIItem700Cell = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIItem700Cell;
            DXTextEdit uIRow6ColumncolAmountEdit = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIRow6ColumncolAmountEdit;
            DXCell uIItem800Cell = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIItem800Cell;
            DXTextEdit uIRow7ColumncolAmountEdit = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIRow7ColumncolAmountEdit;
            DXCell uIItem900Cell = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIItem900Cell;
            DXTextEdit uIRow8ColumncolAmountEdit = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIRow8ColumncolAmountEdit;
            DXCell uIItem1000Cell = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIItem1000Cell;
            DXTextEdit uIRow9ColumncolAmountEdit = this.UINyevaktklassesatserWindow.UIGcShiftClassRatesTable.UIRow9ColumncolAmountEdit;
            DXButton uICloseButton = this.UINyevaktklassesatserWindow.UICloseButton;
            #endregion

            // Click 'Ny' button
            Mouse.Click(uINYButton);

            UICommon.SetEffectiveFromRateDateNew(date);

            // Click '100' cell
            Mouse.Click(uIItem100Cell);
            //uIRow0ColumncolAmountEdit.ValueAsString = val1;
            Keyboard.SendKeys(uIRow0ColumncolAmountEdit, val1, ModifierKeys.None);

            // Click '200' cell
            Mouse.Click(uIItem200Cell);
            //uIRow1ColumncolAmountEdit.ValueAsString = val2;
            Keyboard.SendKeys(uIRow1ColumncolAmountEdit, val2, ModifierKeys.None);

            Mouse.Click(uIItem300Cell);
            //uIRow2ColumncolAmountEdit.ValueAsString = val3;
            Keyboard.SendKeys(uIRow2ColumncolAmountEdit, val3, ModifierKeys.None);

            Mouse.Click(uIItem400Cell);
            //uIRow3ColumncolAmountEdit.ValueAsString = val4;
            Keyboard.SendKeys(uIRow3ColumncolAmountEdit, val4, ModifierKeys.None);

            Mouse.Click(uIItem500Cell);
            //uIRow4ColumncolAmountEdit.ValueAsString = val5;
            Keyboard.SendKeys(uIRow4ColumncolAmountEdit, val5, ModifierKeys.None);

            Mouse.Click(uIItem600Cell);
            //uIRow5ColumncolAmountEdit.ValueAsString = val6;
            Keyboard.SendKeys(uIRow5ColumncolAmountEdit, val6, ModifierKeys.None);

            Mouse.Click(uIItem700Cell);
            //uIRow6ColumncolAmountEdit.ValueAsString = val7;
            Keyboard.SendKeys(uIRow6ColumncolAmountEdit, val7, ModifierKeys.None);

            Mouse.Click(uIItem800Cell);
            //uIRow7ColumncolAmountEdit.ValueAsString = val8;
            Keyboard.SendKeys(uIRow7ColumncolAmountEdit, val8, ModifierKeys.None);

            Mouse.Click(uIItem900Cell);
            //uIRow8ColumncolAmountEdit.ValueAsString = val9;
            Keyboard.SendKeys(uIRow8ColumncolAmountEdit, val9, ModifierKeys.None);

            Mouse.Click(uIItem1000Cell);
            //uIRow9ColumncolAmountEdit.ValueAsString = val10;
            Keyboard.SendKeys(uIRow9ColumncolAmountEdit, val10, ModifierKeys.None);

            ClickOkInNewShiftClassRateWindow();

            // Click '&Close' button
            try
            {
                Playback.Wait(1000);
                Mouse.Click(uICloseButton);
            }
            catch (Exception)
            {
                Keyboard.SendKeys(UINyevaktklassesatserWindow, "l", ModifierKeys.Alt);
            }

            //UICommon.ClearAdministrationSearchString();
        }

        public virtual EditNewLineSettingsStormareParams EditNewLineSettingsStormareParams
        {
            get
            {
                if ((this.mEditNewLineSettingsStormareParams == null))
                {
                    this.mEditNewLineSettingsStormareParams = new EditNewLineSettingsStormareParams();
                }
                return this.mEditNewLineSettingsStormareParams;
            }
        }

        private EditNewLineSettingsStormareParams mEditNewLineSettingsStormareParams;


        public virtual AddNewLineFromDepartmentGustavssonParams AddNewLineFromDepartmentGustavssonParams
        {
            get
            {
                if ((this.mAddNewLineFromDepartmentGustavssonParams == null))
                {
                    this.mAddNewLineFromDepartmentGustavssonParams = new AddNewLineFromDepartmentGustavssonParams();
                }
                return this.mAddNewLineFromDepartmentGustavssonParams;
            }
        }

        private AddNewLineFromDepartmentGustavssonParams mAddNewLineFromDepartmentGustavssonParams;

        public virtual AddNewShiftClassRateParams AddNewShiftClassRateParams
        {
            get
            {
                if ((this.mAddNewShiftClassRateParams == null))
                {
                    this.mAddNewShiftClassRateParams = new AddNewShiftClassRateParams();
                }
                return this.mAddNewShiftClassRateParams;
            }
        }

        private AddNewShiftClassRateParams mAddNewShiftClassRateParams;


        public virtual CopyFTTRosterplanParams CopyFTTRosterplanParams
        {
            get
            {
                if ((this.mCopyFTTRosterplanParams == null))
                {
                    this.mCopyFTTRosterplanParams = new CopyFTTRosterplanParams();
                }
                return this.mCopyFTTRosterplanParams;
            }
        }

        private CopyFTTRosterplanParams mCopyFTTRosterplanParams;



        public virtual CopyFTTKalenderplanParams CopyFTTKalenderplanParams
        {
            get
            {
                if ((this.mCopyFTTKalenderplanParams == null))
                {
                    this.mCopyFTTKalenderplanParams = new CopyFTTKalenderplanParams();
                }
                return this.mCopyFTTKalenderplanParams;
            }
        }

        private CopyFTTKalenderplanParams mCopyFTTKalenderplanParams;


        public virtual CopyFTTBaseplanParams CopyFTTBaseplanParams
        {
            get
            {
                if ((this.mCopyFTTBaseplanParams == null))
                {
                    this.mCopyFTTBaseplanParams = new CopyFTTBaseplanParams();
                }
                return this.mCopyFTTBaseplanParams;
            }
        }

        private CopyFTTBaseplanParams mCopyFTTBaseplanParams;


        public virtual CopyCalendarplanParams CopyCalendarplanParams
        {
            get
            {
                if ((this.mCopyCalendarplanParams == null))
                {
                    this.mCopyCalendarplanParams = new CopyCalendarplanParams();
                }
                return this.mCopyCalendarplanParams;
            }
        }

        private CopyCalendarplanParams mCopyCalendarplanParams;


        public virtual CopyAPRosterplanParams CopyAPRosterplanParams
        {
            get
            {
                if ((this.mCopyAPRosterplanParams == null))
                {
                    this.mCopyAPRosterplanParams = new CopyAPRosterplanParams();
                }
                return this.mCopyAPRosterplanParams;
            }
        }

        private CopyAPRosterplanParams mCopyAPRosterplanParams;
        public virtual SetRosterplanValidDateNewParams SetRosterplanValidDateNewParams
        {
            get
            {
                if ((this.mSetRosterplanValidDateNewParams == null))
                {
                    this.mSetRosterplanValidDateNewParams = new SetRosterplanValidDateNewParams();
                }
                return this.mSetRosterplanValidDateNewParams;
            }
        }

        private SetRosterplanValidDateNewParams mSetRosterplanValidDateNewParams;

        public virtual EditLineSettingsLine2StormareParams EditLineSettingsLine2StormareParams
        {
            get
            {
                if ((this.mEditLineSettingsLine2StormareParams == null))
                {
                    this.mEditLineSettingsLine2StormareParams = new EditLineSettingsLine2StormareParams();
                }
                return this.mEditLineSettingsLine2StormareParams;
            }
        }

        private EditLineSettingsLine2StormareParams mEditLineSettingsLine2StormareParams;


        public virtual SelectAllFTTTEmpsAndClickStopParams SelectAllFTTTEmpsAndClickStopParams
        {
            get
            {
                if ((this.mSelectAllFTTTEmpsAndClickStopParams == null))
                {
                    this.mSelectAllFTTTEmpsAndClickStopParams = new SelectAllFTTTEmpsAndClickStopParams();
                }
                return this.mSelectAllFTTTEmpsAndClickStopParams;
            }
        }

        private SelectAllFTTTEmpsAndClickStopParams mSelectAllFTTTEmpsAndClickStopParams;


        public virtual SetFTTStopdateNewParams SetFTTStopdateNewParams
        {
            get
            {
                if ((this.mSetFTTStopdateNewParams == null))
                {
                    this.mSetFTTStopdateNewParams = new SetFTTStopdateNewParams();
                }
                return this.mSetFTTStopdateNewParams;
            }
        }

        private SetFTTStopdateNewParams mSetFTTStopdateNewParams;

        public virtual EditLine2SettingsStormare_C2_S23Params EditLine2SettingsStormare_C2_S23Params
        {
            get
            {
                if ((this.mEditLine2SettingsStormare_C2_S23Params == null))
                {
                    this.mEditLine2SettingsStormare_C2_S23Params = new EditLine2SettingsStormare_C2_S23Params();
                }
                return this.mEditLine2SettingsStormare_C2_S23Params;
            }
        }

        private EditLine2SettingsStormare_C2_S23Params mEditLine2SettingsStormare_C2_S23Params;


        public virtual SetBaseplanStartDateNewParams SetBaseplanStartDateNewParams
        {
            get
            {
                if ((this.mSetBaseplanStartDateNewParams == null))
                {
                    this.mSetBaseplanStartDateNewParams = new SetBaseplanStartDateNewParams();
                }
                return this.mSetBaseplanStartDateNewParams;
            }
        }

        private SetBaseplanStartDateNewParams mSetBaseplanStartDateNewParams;


        public virtual CreateWhishplanEaster2016Params CreateWhishplanEaster2016Params
        {
            get
            {
                if ((this.mCreateWhishplanEaster2016Params == null))
                {
                    this.mCreateWhishplanEaster2016Params = new CreateWhishplanEaster2016Params();
                }
                return this.mCreateWhishplanEaster2016Params;
            }
        }

        private CreateWhishplanEaster2016Params mCreateWhishplanEaster2016Params;

        public virtual SetCreateWhishplanDatesNewParams SetCreateWhishplanDatesNewParams
        {
            get
            {
                if ((this.mSetCreateWhishplanDatesNewParams == null))
                {
                    this.mSetCreateWhishplanDatesNewParams = new SetCreateWhishplanDatesNewParams();
                }
                return this.mSetCreateWhishplanDatesNewParams;
            }
        }

        private SetCreateWhishplanDatesNewParams mSetCreateWhishplanDatesNewParams;

        public virtual EditLineSettingsLine1PersbrandtParams EditLineSettingsLine1PersbrandtParams
        {
            get
            {
                if ((this.mEditLineSettingsLine1PersbrandtParams == null))
                {
                    this.mEditLineSettingsLine1PersbrandtParams = new EditLineSettingsLine1PersbrandtParams();
                }
                return this.mEditLineSettingsLine1PersbrandtParams;
            }
        }

        private EditLineSettingsLine1PersbrandtParams mEditLineSettingsLine1PersbrandtParams;


        public virtual EditLineSettingsLine2PersbrandtParams EditLineSettingsLine2PersbrandtParams
        {
            get
            {
                if ((this.mEditLineSettingsLine2PersbrandtParams == null))
                {
                    this.mEditLineSettingsLine2PersbrandtParams = new EditLineSettingsLine2PersbrandtParams();
                }
                return this.mEditLineSettingsLine2PersbrandtParams;
            }
        }

        private EditLineSettingsLine2PersbrandtParams mEditLineSettingsLine2PersbrandtParams;

        public virtual EditLineSettingsRheborg_2Params EditLineSettingsRheborg_2Params
        {
            get
            {
                if ((this.mEditLineSettingsRheborg_2Params == null))
                {
                    this.mEditLineSettingsRheborg_2Params = new EditLineSettingsRheborg_2Params();
                }
                return this.mEditLineSettingsRheborg_2Params;
            }
        }

        private EditLineSettingsRheborg_2Params mEditLineSettingsRheborg_2Params;

        public virtual EditLineSettingsLine2RheborgParams EditLineSettingsLine2RheborgParams
        {
            get
            {
                if ((this.mEditLineSettingsLine2RheborgParams == null))
                {
                    this.mEditLineSettingsLine2RheborgParams = new EditLineSettingsLine2RheborgParams();
                }
                return this.mEditLineSettingsLine2RheborgParams;
            }
        }

        private EditLineSettingsLine2RheborgParams mEditLineSettingsLine2RheborgParams;

        public virtual OpenPlanSettingsAndEditToDateParams OpenPlanSettingsAndEditToDateParams
        {
            get
            {
                if ((this.mOpenPlanSettingsAndEditToDateParams == null))
                {
                    this.mOpenPlanSettingsAndEditToDateParams = new OpenPlanSettingsAndEditToDateParams();
                }
                return this.mOpenPlanSettingsAndEditToDateParams;
            }
        }

        private OpenPlanSettingsAndEditToDateParams mOpenPlanSettingsAndEditToDateParams;

        public virtual EditLineSettingsOscarsson_C6_S16Params EditLineSettingsOscarsson_C6_S16Params
        {
            get
            {
                if ((this.mEditLineSettingsOscarsson_C6_S16Params == null))
                {
                    this.mEditLineSettingsOscarsson_C6_S16Params = new EditLineSettingsOscarsson_C6_S16Params();
                }
                return this.mEditLineSettingsOscarsson_C6_S16Params;
            }
        }

        private EditLineSettingsOscarsson_C6_S16Params mEditLineSettingsOscarsson_C6_S16Params;



        public virtual EditLineSettingsOscarsson_C6_S16_1Params EditLineSettingsOscarsson_C6_S16_1Params
        {
            get
            {
                if ((this.mEditLineSettingsOscarsson_C6_S16_1Params == null))
                {
                    this.mEditLineSettingsOscarsson_C6_S16_1Params = new EditLineSettingsOscarsson_C6_S16_1Params();
                }
                return this.mEditLineSettingsOscarsson_C6_S16_1Params;
            }
        }

        private EditLineSettingsOscarsson_C6_S16_1Params mEditLineSettingsOscarsson_C6_S16_1Params;

        public virtual EditLine2SettingsOscarsson_C6_S21Params EditLine2SettingsOscarsson_C6_S21Params
        {
            get
            {
                if ((this.mEditLine2SettingsOscarsson_C6_S21Params == null))
                {
                    this.mEditLine2SettingsOscarsson_C6_S21Params = new EditLine2SettingsOscarsson_C6_S21Params();
                }
                return this.mEditLine2SettingsOscarsson_C6_S21Params;
            }
        }

        private EditLine2SettingsOscarsson_C6_S21Params mEditLine2SettingsOscarsson_C6_S21Params;



        public virtual EditLineSettingsKulle_C6_S30Params EditLineSettingsKulle_C6_S30Params
        {
            get
            {
                if ((this.mEditLineSettingsKulle_C6_S30Params == null))
                {
                    this.mEditLineSettingsKulle_C6_S30Params = new EditLineSettingsKulle_C6_S30Params();
                }
                return this.mEditLineSettingsKulle_C6_S30Params;
            }
        }

        private EditLineSettingsKulle_C6_S30Params mEditLineSettingsKulle_C6_S30Params;

        public virtual EditLineSettingsNyquist_C6_S28Params EditLineSettingsNyquist_C6_S28Params
        {
            get
            {
                if ((this.mEditLineSettingsNyquist_C6_S28Params == null))
                {
                    this.mEditLineSettingsNyquist_C6_S28Params = new EditLineSettingsNyquist_C6_S28Params();
                }
                return this.mEditLineSettingsNyquist_C6_S28Params;
            }
        }

        private EditLineSettingsNyquist_C6_S28Params mEditLineSettingsNyquist_C6_S28Params;

        public virtual EditPlanSettingValidToDateParams EditPlanSettingValidToDateParams
        {
            get
            {
                if ((this.mEditPlanSettingValidToDateParams == null))
                {
                    this.mEditPlanSettingValidToDateParams = new EditPlanSettingValidToDateParams();
                }
                return this.mEditPlanSettingValidToDateParams;
            }
        }

        private EditPlanSettingValidToDateParams mEditPlanSettingValidToDateParams;

        public virtual ReplaceAshiftWeek1AndDshiftWeek3Params ReplaceAshiftWeek1AndDshiftWeek3Params
        {
            get
            {
                if ((this.mReplaceAshiftWeek1AndDshiftWeek3Params == null))
                {
                    this.mReplaceAshiftWeek1AndDshiftWeek3Params = new ReplaceAshiftWeek1AndDshiftWeek3Params();
                }
                return this.mReplaceAshiftWeek1AndDshiftWeek3Params;
            }
        }

        private ReplaceAshiftWeek1AndDshiftWeek3Params mReplaceAshiftWeek1AndDshiftWeek3Params;


        public virtual ReplaceNshiftsWeek2AndF2shiftWeek3Params ReplaceNshiftsWeek2AndF2shiftWeek3Params
        {
            get
            {
                if ((this.mReplaceNshiftsWeek2AndF2shiftWeek3Params == null))
                {
                    this.mReplaceNshiftsWeek2AndF2shiftWeek3Params = new ReplaceNshiftsWeek2AndF2shiftWeek3Params();
                }
                return this.mReplaceNshiftsWeek2AndF2shiftWeek3Params;
            }
        }

        private ReplaceNshiftsWeek2AndF2shiftWeek3Params mReplaceNshiftsWeek2AndF2shiftWeek3Params;


        public virtual EditLineSettingsLine3GustavssonParams EditLineSettingsLine3GustavssonParams
        {
            get
            {
                if ((this.mEditLineSettingsLine3GustavssonParams == null))
                {
                    this.mEditLineSettingsLine3GustavssonParams = new EditLineSettingsLine3GustavssonParams();
                }
                return this.mEditLineSettingsLine3GustavssonParams;
            }
        }

        private EditLineSettingsLine3GustavssonParams mEditLineSettingsLine3GustavssonParams;
    }

    /// <summary>
    /// Parameters to be passed into 'EditNewLineSettingsStormare'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditNewLineSettingsStormareParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.DataModel.Simple+PositionCategory' in 'leAvailablePositionCategories' LookUpEdit
        /// </summary>
        public string UILeAvailablePositionCLookUpEditValueTypeName = "Gatsoft.Gat.DataModel.Simple+PositionCategory";

        /// <summary>
        /// Type '' in 'leAvailablePositionCategories' LookUpEdit
        /// </summary>
        public string UILeAvailablePositionCLookUpEditValueAsString = "S";

        /// <summary>
        /// Type '{Tab}' in 'leAvailablePositionCategories' LookUpEdit
        /// </summary>
        public string UILeAvailablePositionCLookUpEditSendKeys = "{Tab}";

        /// <summary>
        /// Type '25,00 [SelectionStart]0[SelectionLength]5' in 'eNumber[3]' text box
        /// </summary>
        public string UIENumber3EditValueAsString = "25,00 [SelectionStart]0[SelectionLength]5";

        /// <summary>
        /// Type '1' in 'eInternalPositionNumber' text box
        /// </summary>
        public string UIEInternalPositionNumEditValueAsString = "1";

        /// <summary>
        /// Type '{Tab}' in 'eInternalPositionNumber' text box
        /// </summary>
        public string UIEInternalPositionNumEditSendKeys = "{Tab}";

        /// <summary>
        /// Type '' in 'leAvailableRulesets' LookUpEdit
        /// </summary>
        public string UILeAvailableRulesetsLookUpEditValueAsString = "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddNewLineFromDepartmentGustavsson'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddNewLineFromDepartmentGustavssonParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.DataModel.Simple+PositionCategory' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueTypeName = "Gatsoft.Gat.DataModel.Simple+PositionCategory";

        /// <summary>
        /// Type 'L - Lege' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueAsString = "L - Lege";

        /// <summary>
        /// Type '{ENTER}{Tab}' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditSendKeys = "{ENTER}{Tab}";

        /// <summary>
        /// Type '1' in 'eInternalPositionNumber' text box
        /// </summary>
        public string UIEInternalPositionNumEditValueAsString = "1";

        /// <summary>
        /// Type '{Tab}' in 'eInternalPositionNumber' text box
        /// </summary>
        public string UIEInternalPositionNumEditSendKeys = "{Tab}";

        /// <summary>
        /// Type '' in 'leAvailableRulesets' LookUpEdit
        /// </summary>
        public string UILeAvailableRulesetsLookUpEditValueAsString = "";

        /// <summary>
        /// Type '{Tab}' in 'leAvailableRulesets' LookUpEdit
        /// </summary>
        public string UILeAvailableRulesetsLookUpEditSendKeys = "{Tab}";

        /// <summary>
        /// Type 'Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueTypeName1 = "Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition" +
            "";

        /// <summary>
        /// Type 'Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditValueAsString1 = "Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition" +
            "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddNewShiftClassRate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddNewShiftClassRateParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Date' in 'deEffectiveFrom' text box
        /// </summary>
        public string UIDeEffectiveFromEditValueTypeName = "Gatsoft.Date";

        /// <summary>
        /// Type '18.01.2016' in 'deEffectiveFrom' text box
        /// </summary>
        public string UIDeEffectiveFromEditValueAsString = "18.01.2016";

        /// <summary>
        /// Type '{Tab}' in 'deEffectiveFrom' text box
        /// </summary>
        public string UIDeEffectiveFromEditSendKeys = "{Tab}";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CopyFTTRosterplan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CopyFTTRosterplanParams
    {

        #region Fields
        /// <summary>
        /// Clear 'chkKladd' check box
        /// </summary>
        public bool UIChkKladdCheckBoxChecked = false;

        /// <summary>
        /// Type '{Tab}' in 'chkKladd' check box
        /// </summary>
        public string UIChkKladdCheckBoxSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CopyFTTKalenderplan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CopyFTTKalenderplanParams
    {

        #region Fields
        /// <summary>
        /// Type 'FTT Januar 2016' in 'txtName' text box
        /// </summary>
        public string UITxtNameEditValueAsString = "FTT Januar 2024";

        /// <summary>
        /// Type '{Tab}' in 'txtName' text box
        /// </summary>
        public string UITxtNameEditSendKeys = "{Tab}";

        /// <summary>
        /// Type '04.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 01, 08);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CopyFTTBaseplan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CopyFTTBaseplanParams
    {

        #region Fields
        /// <summary>
        /// Type '14.03.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "14.03.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 03, 18);
        /// <summary>
        /// Clear 'chkKladd' check box
        /// </summary>
        public bool UIChkKladdCheckBoxChecked = false;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CopyCalendarplan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CopyCalendarplanParams
    {

        #region Fields
        /// <summary>
        /// Type '04.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "04.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 01, 01);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CopyAPRosterplan'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CopyAPRosterplanParams
    {

        #region Fields
        /// <summary>
        /// Type 'Vaktklasser AP kopi' in 'txtName' text box
        /// </summary>
        public string UITxtNameEditValueAsString = "Vaktklasser AP kopi";

        /// <summary>
        /// Type '{Tab}' in 'txtName' text box
        /// </summary>
        public string UITxtNameEditSendKeys = "{Tab}";

        /// <summary>
        /// Type '{Tab}' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditSendKeys = "{Tab}";

        /// <summary>
        /// Clear 'chkKladd' check box
        /// </summary>
        public bool UIChkKladdCheckBoxChecked = false;
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetRosterplanValidDateNew'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetRosterplanValidDateNewParams
    {

        #region Fields
        /// <summary>
        /// Type ' [SelectionStart]0' in 'pceDate' DateTimeEdit
        /// </summary>
        public string UIPceDateDateTimeEditValueAsString = " [SelectionStart]0";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLineSettingsLine2Stormare'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLineSettingsLine2StormareParams
    {

        #region Fields
        /// <summary>
        /// Select 'chkAvailabilityPeriodsDefined' check box
        /// </summary>
        public bool UIChkAvailabilityPerioCheckBoxChecked = true;

        /// <summary>
        /// Type '04.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "04.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 01, 08);
        /// <summary>
        /// Type '18.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "18.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 01, 22);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectAllFTTTEmpsAndClickStop'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectAllFTTTEmpsAndClickStopParams
    {

        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Date' in 'deDate' text box
        /// </summary>
        public string UIDeDateEditValueTypeName = "Gatsoft.Date";

        /// <summary>
        /// Type '04.01.2016 [SelectionStart]0' in 'deDate' text box
        /// </summary>
        public string UIDeDateEditValueAsString = "04.01.2016 [SelectionStart]0";
        public DateTime UIDeDateEditValueAsDate = new DateTime(2024, 01, 08);
        /// <summary>
        /// Type '{Tab}' in 'gcEmployments' table
        /// </summary>
        public string UIGcEmploymentsTableSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetFTTStopdateNew'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetFTTStopdateNewParams
    {

        #region Fields
        /// <summary>
        /// Type ' [SelectionStart]0' in 'deDate' text box
        /// </summary>
        public string UIDeDateEditValueAsString = " [SelectionStart]0";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLine2SettingsStormare_C2_S23'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLine2SettingsStormare_C2_S23Params
    {

        #region Fields
        /// <summary>
        /// Select 'chkAvailabilityPeriodsDefined' check box
        /// </summary>
        public bool UIChkAvailabilityPerioCheckBoxChecked = true;

        /// <summary>
        /// Type '19.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "19.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 1, 23);
        /// <summary>
        /// Type '31.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "31.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 2, 04);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetBaseplanStartDateNew'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetBaseplanStartDateNewParams
    {

        #region Fields
        /// <summary>
        /// Type ' [SelectionStart]0' in 'pceDate' DateTimeEdit
        /// </summary>
        public string UIPceDateDateTimeEditValueAsString = " [SelectionStart]0";

        /// <summary>
        /// Type '{NumPad1}{NumPad6}{Tab}' in 'pceDate' DateTimeEdit
        /// </summary>
        public string UIPceDateDateTimeEditSendKeys = "{NumPad1}{NumPad6}{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'CreateWhishplanEaster2016'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class CreateWhishplanEaster2016Params
    {

        #region Fields
        /// <summary>
        /// Type 'Påske 2016' in 'txtName' text box
        /// </summary>
        public string UITxtNameEditValueAsString = "Påske 2016";

        /// <summary>
        /// Type '{Tab}' in 'txtName' text box
        /// </summary>
        public string UITxtNameEditSendKeys = "{Tab}";

        /// <summary>
        /// Type '14.03.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "14.03.2016 [SelectionStart]0[SelectionLength]10";

        /// <summary>
        /// Type '3 [SelectionStart]0[SelectionLength]1' in 'eNumber' text box
        /// </summary>
        public string UIENumberEditValueAsString = "3 [SelectionStart]0[SelectionLength]1";

        /// <summary>
        /// Type '03.04.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "03.04.2016 [SelectionStart]0[SelectionLength]10";

        /// <summary>
        /// Type '0' in 'rgrpPhaseControl' RadioGroup
        /// </summary>
        public int UIRgrpPhaseControlRadioGroupSelectedIndex = 0;

        /// <summary>
        /// Type '{Tab}' in 'rgrpPhaseControl' RadioGroup
        /// </summary>
        public string UIRgrpPhaseControlRadioGroupSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetCreateWhishplanDatesNew'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SetCreateWhishplanDatesNewParams
    {

        #region Fields
        /// <summary>
        /// Type '14.03.2016' in 'pceDate' DateTimeEdit
        /// </summary>
        public DateTime UIPceDateDateTimeEditValueAsDate = new DateTime(2024, 03, 18);

        /// <summary>
        /// Type '3 [SelectionStart]0[SelectionLength]1' in 'eNumber' text box
        /// </summary>
        public string UIENumberEditValueAsString = "3 [SelectionStart]0[SelectionLength]1";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLineSettingsLine1Persbrandt'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLineSettingsLine1PersbrandtParams
    {

        #region Fields
        /// <summary>
        /// Select 'chkAvailabilityPeriodsDefined' check box
        /// </summary>
        public bool UIChkAvailabilityPerioCheckBoxChecked = true;

        /// <summary>
        /// Type '04.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "04.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 01, 08);
        /// <summary>
        /// Type '24.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "24.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 01, 28);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLineSettingsLine2Persbrandt'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLineSettingsLine2PersbrandtParams
    {

        #region Fields
        /// <summary>
        /// Type '25.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "25.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 01, 29);
        /// <summary>
        /// Type '31.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "31.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 02, 04);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLineSettingsRheborg_2'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLineSettingsRheborg_2Params
    {

        #region Fields
        /// <summary>
        /// Select 'chkAvailabilityPeriodsDefined' check box
        /// </summary>
        public bool UIChkAvailabilityPerioCheckBoxChecked = true;

        /// <summary>
        /// Type '11.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "11.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 01, 15);
        /// <summary>
        /// Type '31.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "31.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 02, 04);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLineSettingsLine2Rheborg'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLineSettingsLine2RheborgParams
    {

        #region Fields
        /// <summary>
        /// Type '10.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "10.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 01, 14);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'OpenPlanSettingsAndEditToDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class OpenPlanSettingsAndEditToDateParams
    {

        #region Fields
        /// <summary>
        /// Type '06.03.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "06.03.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 3, 10);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLineSettingsOscarsson_C6_S16'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLineSettingsOscarsson_C6_S16Params
    {

        #region Fields
        /// <summary>
        /// Select 'chkAvailabilityPeriodsDefined' check box
        /// </summary>
        public bool UIChkAvailabilityPerioCheckBoxChecked = true;

        /// <summary>
        /// Type '04.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "04.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 01, 08);
        /// <summary>
        /// Type '17.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "17.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 01, 21);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLineSettingsOscarsson_C6_S16_1'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLineSettingsOscarsson_C6_S16_1Params
    {

        #region Fields
        /// <summary>
        /// Type '25.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "25.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 01, 29);

        /// <summary>
        /// Type '31.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "31.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 02, 04);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLine2SettingsOscarsson_C6_S21'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLine2SettingsOscarsson_C6_S21Params
    {

        #region Fields
        /// <summary>
        /// Type '18.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public string UIPceDate1PopupEditValueAsString = "18.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 01, 22);
        /// <summary>
        /// Type '24.01.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public string UIPceDate0PopupEditValueAsString = "24.01.2016 [SelectionStart]0[SelectionLength]10";
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 01, 28);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLineSettingsKulle_C6_S30'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLineSettingsKulle_C6_S30Params
    {

        #region Fields
        /// <summary>
        /// Type '22.02.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 01, 29);
        /// <summary>
        /// Type '20.03.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 02, 25);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLineSettingsNyquist_C6_S28'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditLineSettingsNyquist_C6_S28Params
    {

        #region Fields
        /// <summary>
        /// Type '21.02.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[1]' PopupEdit
        /// </summary>
        public DateTime UIPceDate1PopupEditValueAsDate = new DateTime(2024, 01, 28);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditPlanSettingValidToDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class EditPlanSettingValidToDateParams
    {

        #region Fields
        /// <summary>
        /// Type '27.03.2016 [SelectionStart]0[SelectionLength]10' in 'pceDate[0]' PopupEdit
        /// </summary>
        public DateTime UIPceDate0PopupEditValueAsDate = new DateTime(2024, 03, 03);
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ReplaceAshiftWeek1AndDshiftWeek3'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class ReplaceAshiftWeek1AndDshiftWeek3Params
    {

        #region Fields
        /// <summary>
        /// Type 'd' in '[Row]0[Column]RosterCell_1' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEditValueAsString = "d";

        /// <summary>
        /// Type '{Tab}' in '[Row]0[Column]RosterCell_1' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEditSendKeys = "{Tab}";

        /// <summary>
        /// Type 'f2' in '[Row]0[Column]RosterCell_20' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEdit1ValueAsString = "f2";

        /// <summary>
        /// Type '{Tab}' in '[Row]0[Column]RosterCell_20' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEdit1SendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ReplaceNshiftsWeek2AndF2shiftWeek3'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class ReplaceNshiftsWeek2AndF2shiftWeek3Params
    {

        #region Fields
        /// <summary>
        /// Type '' in '[Row]0[Column]RosterCell_9' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEdit2ValueAsString = "";

        /// <summary>
        /// Type '{Tab}' in '[Row]0[Column]RosterCell_9' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEdit2SendKeys = "{Tab}";

        /// <summary>
        /// Type 'a' in '[Row]0[Column]RosterCell_10' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEdit11ValueAsString = "a";

        /// <summary>
        /// Type '{Tab}' in '[Row]0[Column]RosterCell_10' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEdit11SendKeys = "{Tab}";

        /// <summary>
        /// Type 'd' in '[Row]0[Column]RosterCell_11' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEdit21ValueAsString = "d";

        /// <summary>
        /// Type '{Tab}' in '[Row]0[Column]RosterCell_11' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEdit21SendKeys = "{Tab}";

        /// <summary>
        /// Type 'n' in 'gcRosterPlan' table
        /// </summary>
        public string UIGcRosterPlanTableSendKeys = "n";

        /// <summary>
        /// Type '{Tab}' in '[Row]0[Column]RosterCell_20' text box
        /// </summary>
        public string UIRow0ColumnRosterCellEdit1SendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'EditLineSettingsLine3Gustavsson'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class EditLineSettingsLine3GustavssonParams
    {
        #region Fields
        /// <summary>
        /// Type 'Gatsoft.Gat.UILogic.Planning.RosterPlanning.EmployeeSelection.SporadicDefintition' in 'GSLookUpEdit' LookUpEdit
        /// </summary>
        public string UIGSLookUpEditLookUpEditSearchString = "ingen";
        #endregion
    }
}
