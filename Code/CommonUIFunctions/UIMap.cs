namespace CommonUIFunctions
{
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System;
    using System.Collections.Generic;
    using System.CodeDom.Compiler;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Drawing;
    using System.Windows.Input;
    using CommonTestData;
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;

    public partial class UIMap
    {
        private TestContext TestContext;   
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
     
        public UIMap(TestContext testContext, bool isPerformance = false)
        {        
            TestContext = testContext;
            ConvertToWinWindow();
            GetIniFile();           
        }
        
        private void ConvertToWinWindow()
        {
            var standaloneBarDockControl_2 = GatMain_02.UIStandaloneBarDockConCustom;
            standaloneBarDockControl_2.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            standaloneBarDockControl_2.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            standaloneBarDockControl_2.SearchProperties[WinWindow.PropertyNames.ControlName] = "standaloneBarDockControl";

            var viewHost = GatMain.UIViewHostCustom;
            viewHost.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            viewHost.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            viewHost.SearchProperties.Add(WinWindow.PropertyNames.ControlName, "ViewHost");
            
            var uIMainPanel9 = GatMain_09.UIPcMainPanelClient;
            uIMainPanel9.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uIMainPanel9.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            uIMainPanel9.SearchProperties[WinWindow.PropertyNames.ControlName] = "pcMainPanel";

            var uIMainPanel10 = this.GatMain_10.UIStandaloneBarDockConCustom;
            uIMainPanel10.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uIMainPanel10.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            uIMainPanel10.SearchProperties.Add(WinWindow.PropertyNames.ControlName, "standaloneBarDockControl");

            var uIMainPanel11 = this.GatMain_11.UIPcMainPanelClient;
            uIMainPanel11.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            uIMainPanel11.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            uIMainPanel11.SearchProperties.Add(WinWindow.PropertyNames.ControlName, "pcMainPanel");

            var bankBalance = this.UIGatver654ASCLAvd5140Window.UIBankBalanceListContrCustom;
            bankBalance.SearchProperties.Remove(DXTestControl.PropertyNames.Name);
            bankBalance.SearchProperties.Remove(DXTestControl.PropertyNames.ClassName);
            bankBalance.SearchProperties[WinWindow.PropertyNames.ControlName] = "BankBalanceListControl";
        }

        private void GetIniFile()
        {
            TestData.GetIniFile();
        }
        public bool RestoreDatabase(bool isPerformance = false, bool overideIniSetting = false, bool br = false)
        {
            if (!overideIniSetting && TestData.GatRestoreDataBase == "0")
                return true;

            KillGatProcess();            
            return DataService.RestoreDatabase(TestContext, isPerformance, br);
        }
         public void LaunchGatRun()
        {
            Playback.Wait(500);
            KillGatProcess();

            ApplicationUnderTest gatRunApplication = ApplicationUnderTest.Launch(TestData.GatRunExePath, TestData.GatRunExePath);//, "/L");
            gatRunApplication.CloseOnPlaybackCleanup = true;

            CheckLoginwindowExists();
            TestContext.WriteLine("GatRun finished OK!");
        }

        public bool CheckLoginwindowExists()
        {
            #region Variable Declarations
            UILoginWindow login = UILoginWindow;//.UIASCLWindow.UIItemEdit;
            #endregion

            try
            {
                login.WaitForControlCondition(IsLoginWindowEnabled);
            }

            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static bool IsLoginWindowEnabled(UITestControl control)
        {
            UILoginWindow loginWindow = control as UILoginWindow;
            //WinEdit uIItemEdit = UILoginWindow.UIASCLWindow.UIItemEdit;
            return loginWindow.Exists;
        }
        public void LaunchGatturnus(bool closeOnPlaybackCleanup)
        {
            Playback.Wait(500);
            KillGatProcess();
            ApplicationUnderTest gATTURNUSApplication = ApplicationUnderTest.Launch(TestData.GatturnusExePath, TestData.GatturnusExePath, TestData.GatturnusExeArgs);
            gATTURNUSApplication.CloseOnPlaybackCleanup = closeOnPlaybackCleanup;
            Playback.Wait(1000);
        }

        public void LoginGat(string userName = "", bool logGatInformation = true, bool checkMalBaseWarning = true)
        {
            #region Variable Declarations
            WinEdit uIItemEdit = this.UILoginWindow.UIASCLWindow.UIItemEdit;
            WinEdit uIItemEdit1 = this.UILoginWindow.UIItemWindow.UIItemEdit;
            WinButton uIOKButton = this.UILoginWindow.UILoginWindow1.UILoginClient.UIOKButton;
            WinClient uIResultaterfrasystemsClient = this.UIResultaterfrasystemsWindow.UIItemWindow.UIResultaterfrasystemsClient;
            #endregion

            if (CheckLoginEnabled())
                TestContext.WriteLine("Login enabled");
            else
                throw new Exception("Login not accessible");

            try
            {
                uIItemEdit.DrawHighlight();

                if (userName == "")
                    Keyboard.SendKeys(uIItemEdit, LoginGatAndSelectDepartmentParams.UIItemEditText);
                else
                    Keyboard.SendKeys(uIItemEdit, userName);
            }
            catch (Exception)
            {
                if (userName == "")
                    uIItemEdit.Text = this.LoginGatAndSelectDepartmentParams.UIItemEditText;
                else
                    uIItemEdit.Text = userName;
            }

            // Type '{Tab}' in text box   
            Playback.Wait(500);
            Keyboard.SendKeys(uIItemEdit, this.LoginGatAndSelectDepartmentParams.UIItemEditSendKeys, ModifierKeys.None);

            try
            {
                uIItemEdit1.WaitForControlReady();
                uIItemEdit1.DrawHighlight();
                Mouse.Click(uIItemEdit1);

                // Type '********' in text box
                Keyboard.SendKeys(uIItemEdit1, this.LoginGatAndSelectDepartmentParams.UIItemEditSendKeys1, true);
            }
            catch (Exception)
            {
                uIItemEdit1.Text = "user";
            }


            // Click 'OK' button
            Mouse.Click(uIOKButton);
            var timeBefore = DateTime.Now;

            // Click 'Resultater fra systemsjekk / faste SQL scripts' client
            if (uIResultaterfrasystemsClient.Exists)
                Mouse.Click(uIResultaterfrasystemsClient, new Point(717, 22));

            if (checkMalBaseWarning)
                CloseSysMessage();

            if (logGatInformation)
                LogRunningGatVersion();

            var timeAfter = DateTime.Now;

            var diff = timeAfter - timeBefore;
            var elapsedTime = Convert.ToInt32(diff.TotalSeconds);

            //var errorList = new List<string>();   
            //if (limit > 0 && (elapsedTime > limit))
            //    errorList.Add("Iverksettingen tar lenger tid enn normalt. Forventet: " + limit + ", målt: " + elapsedTime);

            TestContext.WriteLine("Tidsforbruk ved oppstart av Gat: " + elapsedTime + " sek.");
        }

        /// <summary>
        /// LoginGatAndSelectDepartment - Use 'LoginGatAndSelectDepartmentParams' to pass parameters into this method.
        /// </summary>
        public void LoginGatAndSelectDepartment(string department, List<string> otherDepartments = null, string userName = "", bool logGatInformation = true, bool clearOtherDepartments = true, bool findBySearch = false, bool checkMalBaseWarning = true)
        {
            LoginGat(userName, logGatInformation, checkMalBaseWarning);

            if (!String.IsNullOrEmpty(department))
                ChangeDepartment(department, otherDepartments, clearOtherDepartments, findBySearch);

            Playback.Wait(5000);
        }
        
        public void StartGatFromExtractedDir(string department, string destinationAddressZipFiles, bool logVersion)
        {
            #region Variable Declarations
            string GatturnusPath = destinationAddressZipFiles + @"\Gat_no\GATTURNUS.exe";
            var loginName = LoginGatAndSelectDepartmentParams.UIItemEditText;
            WinEdit uIItemEdit = this.UILoginWindow.UIASCLWindow.UIItemEdit;
            WinEdit uIItemEdit1 = this.UILoginWindow.UIItemWindow.UIItemEdit;
            WinButton uIOKButton = this.UILoginWindow.UILoginWindow1.UILoginClient.UIOKButton;
            WinClient uIResultaterfrasystemsClient = this.UIResultaterfrasystemsWindow.UIItemWindow.UIResultaterfrasystemsClient;
            WinButton uIOKButton1 = this.UISystemmelding1Window.UIItemWindow.UISystemmelding1Client.UIOKButton;
            #endregion

            KillGatProcess();
            Process.Start(GatturnusPath);

            try
            {
                UIMapVS2017.ConnectGatToDataBase();
            }
            catch (Exception)
            {
                TestContext.WriteLine("Unable to connect to database or connection already exists");
            }

            UIMapVS2017.CheckUIGT2004AdvarselWindowExists(TestContext);

            LoginGat(loginName, logVersion);

            if (department != "")
                ChangeDepartment(department);
        }
       

        public bool CheckLoginEnabled()
        {
            #region Variable Declarations
            WinEdit uIItemEdit = this.UILoginWindow.UIASCLWindow.UIItemEdit;
            #endregion

            try
            {
                Playback.Wait(10000);
                var exists = uIItemEdit.WaitForControlCondition(Loginxists);

                if (!exists)
                    exists = uIItemEdit.WaitForControlCondition(Loginxists);

                if (!exists)
                {
                    if (!uIItemEdit.WaitForControlExist())
                        throw new Exception("Login not accessible");
                }
            }
            catch (Exception e)
            {
                TestContext.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        private bool Loginxists(UITestControl control)
        {
            WinEdit uIItemEdit = this.UILoginWindow.UIASCLWindow.UIItemEdit;
            return uIItemEdit.Exists;
        }
        private bool IsLoginEnabled(UITestControl control)
        {
            WinEdit uIItemEdit = this.UILoginWindow.UIASCLWindow.UIItemEdit;
            return uIItemEdit.Enabled;
        }
        /// <summary>
        /// SysMessageCloseButton - Use 'SysMessageCloseButtonExpectedValues' to pass parameters into this method.
        /// </summary>
        private void CloseSysMessage()
        {
            #region Variable Declarations
            WinButton uIOKButton = this.UISystemmelding1Window.UIItemWindow.UISystemmelding1Client.UIOKButton;
            WinButton uICloseButton = this.UISystemmelding1Window.UISystemmelding1TitleBar.UICloseButton;
            #endregion
            try
            {
                UISystemmelding1Window.WaitForControlExist(30000);
                while (UISystemmelding1Window.Exists)
                {
                    try
                    {
                        // Click '&OK' button
                        uIOKButton.WaitForControlReady(30000);

                        if (uIOKButton.Exists)
                            Mouse.Click(uIOKButton);
                    }
                    catch (Exception)
                    {
                        if (uICloseButton.Exists)
                            Mouse.Click(uICloseButton);
                    }

                    Playback.Wait(1000);
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Error closing Systemmessage(1) window");
            }
        }

        public void LogRunningGatVersion()
        {
            try
            {
                Playback.Wait(1000);
                OpenAboutGatWindow();
                TestContext.WriteLine(AboutGatInformation());
                Playback.Wait(1000);
                CloseAboutGatWindow();
            }
            catch (Exception)
            {
                TestContext.WriteLine("Unable to get Gat information");
            }

            Playback.Wait(1000);
        }

        /// <summary>
        /// AboutGatInformation - Use 'AboutGatInformationExpectedValues' to pass parameters into this method.
        /// </summary>
        private string AboutGatInformation()
        {
            #region Variable Declarations
            WinClient uIApplikasjonGatVersjoClient = this.UIOMWindow.UISystemClient.UIApplikasjonGatVersjoClient;
            #endregion

            return uIApplikasjonGatVersjoClient.Name;
        }

        public void ChangeDepartment(string depName, List<string> otherDepartments = null, bool clearOtherDepartments = true, bool findBySearch = false, bool clickDepButton = true)
        {
            if (clickDepButton)
            {
                try
                {
                    ClickChangeDepartmentButton();
                }
                catch (Exception)
                {
                    Keyboard.SendKeys(GatMain, "i", ModifierKeys.Alt);
                }
            }

            if (clearOtherDepartments)
                ClearOtherDepartments();

            if (otherDepartments != null)
                SelectOtherDepartments(otherDepartments);

            if (findBySearch)
                SelectDepartmentBySearch(depName);
            else
                SelectDepartmentByName(depName);
        }    
        

        public void ChangeDepartmentFromRosterplan(string depName, List<string> otherDepartments = null, bool clearOtherDepartments = true, bool findBySearch = false)
        {
            ClickChangeDepartmentFromRosterplanButton();

            if (clearOtherDepartments)
                ClearOtherDepartments();

            if (otherDepartments != null)
                SelectOtherDepartments(otherDepartments);

            if (findBySearch)
                SelectDepartmentBySearch(depName);
            else
                SelectDepartmentByName(depName);
        }               

        private bool SelectDepartmentByName(string depName)
        {
            #region Variable Declarations
            var depTable = UINivåWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayDepartmentsLayoutControlItem.UIGbDepartmentsClient.UIGDepartmentsTable;
            #endregion

            var view = depTable.Views[0];
            for (int i = 0; i < view.RowCount; i++)
            {
                var val = view.GetCellValue("cDepName", i).ToString().Trim();
                if (val == depName)
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

        private bool SelectDepartmentBySearch(string depName)
        {
            try
            {
                UIMapVS2017.SelectDepartmentBySearch(depName);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// SelectOtherDepartments - Use 'SelectOtherDepartmentsParams' to pass parameters into this method.
        /// </summary>
        private void SelectOtherDepartments(List<string> depS)
        {
            #region Variable Declarations
            //var changeDepartmentButton = this.GatMain_01.UIStandaloneBarDockConCustom.UIMainmenuMenuBar.UIBtnDepartmentBarBaseButtonItem;
            var otherDepsTable = this.UINivåWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayOtherDepartmentsLayoutControlItem.UIGbOtherDepartmentsClient.UIGOtherDepartmensTable;
            #endregion

            //Mouse.Click(changeDepartmentButton);

            Playback.Wait(2000);
            var view = otherDepsTable.Views[0];

            foreach (var depName in depS)
            {
                for (int i = 0; i < view.RowCount; i++)
                {
                    var val = view.GetCellValue("cOdName", i).ToString().Trim();
                    if (val == depName)
                    {
                        TestContext.WriteLine("Department found: " + val);
                        var selectCell = view.GetCell("cOdName", i);
                        var selectCheckBox = view.GetCell("cOdChecked", i);
                        try
                        {
                            Mouse.Click(selectCell);
                            Mouse.Click(selectCheckBox);
                            i = view.RowCount;
                        }
                        catch (Exception)
                        {
                            TestContext.WriteLine("Error selecting other department: " + val);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// GoToShiftdateNew - Use 'GoToShiftdateNewParams' to pass parameters into this method.
        /// </summary>
        public void GoToShiftbookdate(DateTime date)
        {
            #region Variable Declarations
            DXMenuBaseButtonItem uIVelgdatoBarBaseButtonItem = this.GatMain_10.UIStandaloneBarDockConCustom.UIMainmenuMenuBar.UIVelgdatoBarBaseButtonItem;
            DXDateTimePicker uIPceDateDateTimeEdit = this.GatMain_10.UIStandaloneBarDockConCustom.UIMainmenuMenuBar.UIVelgdatoBarItem.UIPopupContainerBarConMenu.UIPpcDatePickerClient.UISdeDateCustom.UIPceDateDateTimeEdit;
            var picker = GatMain_10.UIStandaloneBarDockConCustom.UIMainmenuMenuBar.UIVelgdatoBarItem.UIPopupContainerBarConMenu.UIPpcDatePickerClient;
            DXButton uIGåtilButton = picker.UIGåtilButton;
            uIGåtilButton.SearchProperties[DXTestControl.PropertyNames.Name] = "btnGoToDate";
            #endregion

            try
            {
                // Click 'Velg dato' BarBaseButtonItem
                uIVelgdatoBarBaseButtonItem.WaitForControlReady(30000);
                Mouse.Click(uIVelgdatoBarBaseButtonItem);
            }
            catch (Exception)
            {
                try
                {
                    Keyboard.SendKeys("d", ModifierKeys.Control);
                }
                catch (Exception)
                {
                    Keyboard.SendKeys(GatMain_10, "d", ModifierKeys.Control);
                }
            }

            try
            {
                Playback.Wait(1000);
                uIPceDateDateTimeEdit.DateTime = date;
                Keyboard.SendKeys("{RIGHT}{TAB}");
            }
            catch (Exception)
            {
                Keyboard.SendKeys(uIPceDateDateTimeEdit, date.ToShortDateString());
                Keyboard.SendKeys("{RIGHT}{TAB}");
            }

            try
            {
                // Click 'Gå til' button
                Mouse.Click(uIGåtilButton);
            }
            catch
            {
                Keyboard.SendKeys("{ENTER}");
            }
        }

        public bool UnlockRosterPlanIfLocked()
        {
            #region Variable Declarations
            DXRibbonPage uIRpSupportToolsRibbonPage = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpSupportToolsRibbonPage;
            DXRibbonButtonItem uILåsoppplanRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpSupportToolsRibbonPage.UIRpgTestCaseRibbonPageGroup.UILåsoppplanRibbonBaseButtonItem;
            #endregion

            // Click 'rpSupportTools' RibbonPage
            Playback.Wait(3000);
            UIArbeidsplanWindow.WaitForControlReady();
            Mouse.Click(uIRpSupportToolsRibbonPage);

            if (!uILåsoppplanRibbonBaseButtonItem.Exists)
            {
                return false;
            }

            UnlockPlan();

            return true;
        }

        /// <summary>
        /// SetLinesInactiveInSwitchEmployeeWindow - Use 'SetLinesInactiveInSwitchEmployeeWindowParams' to pass parameters into this method.
        /// </summary>
        public void SetLinesInactiveInSwitchEmployeeWindow(bool check)
        {
            #region Variable Declarations
            DXCheckBox uIChkDoSetInactiveCheckBox = this.UIByttansattWindow.UIPcContentClient1.UIGsPanelControl1Client.UIGcOriginalLineClient.UIChkDoSetInactiveCheckBox;
            #endregion

            if (uIChkDoSetInactiveCheckBox.Checked == check)
                return;

            uIChkDoSetInactiveCheckBox.Checked = check;
        }

        public bool EffectuationWindowExists()
        {
            #region Variable Declarations
            DXButton uIIverksett83linjerButton = this.UIIverksetteWindow.UIPnlButtonsClient.UIIverksett83linjerButton;
            UIIverksetteWindow.WaitForControlExist(600000);
            #endregion

            return uIIverksett83linjerButton.Exists;
        }

        public void ChangeEffectuationPeriodForActualLines(DateTime? fromDate, DateTime? toDate)
        {
            ClickChangePeriodForActualLinesButton();
            ChangeEffectuationPeriodRosterplan(fromDate, toDate);
            ClickOkChangePeriodForActualLinesButton();
        }

        /// <summary>
        /// EffectuateRosterplanLines
        /// </summary>
        public void EffectuateRosterplanLines(bool closeRPL4017Window)
        {
            #region Variable Declarations
            DXButton uIIverksett83linjerButton = this.UIIverksetteWindow.UIPnlButtonsClient.UIIverksett83linjerButton;
            DXButton uIGSSimpleButtonButton = this.UIItemWindow3.UIGSSimpleButtonButton;
            DXButton uIJAButton = this.UIRPL4044InformasjonWindow.UIJAButton;
            #endregion

            // Click 'Iverksett 83 linjer' button
            Mouse.Click(uIIverksett83linjerButton);

            if (closeRPL4017Window)
                Mouse.Click(uIGSSimpleButtonButton);

            // Click '&Ja' button
            Mouse.Click(uIJAButton);
        }


        /// <summary>
        /// EffectuateFullRosterplan
        /// </summary>
        public void EffectuateFullRosterplan(bool selectPlanTab)
        {
            #region Variable Declarations
            DXRibbonItem uIIverksettingRibbonItem = this.UIArbeidsplanYTELSEKalWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIGrpEffectuateNoneRevRibbonPageGroup.UIIverksettingRibbonItem;
            DXMenuBaseButtonItem uIBtnNoRevolveWholePerMenuBaseButtonItem = this.UIItemWindow2.UISubMenuBarControlMenu.UIBtnNoRevolveWholePerMenuBaseButtonItem;
            #endregion

            Playback.Wait(2000);

            if (selectPlanTab)
                SelectRosterplanPlanTab();

            // Click 'Iverksetting' RibbonItem
            Mouse.Click(uIIverksettingRibbonItem);//, new Point(49, 11));

            // Click 'btnNoRevolveWholePeriod' MenuBaseButtonItem
            Playback.Wait(500);
            Mouse.Click(uIBtnNoRevolveWholePerMenuBaseButtonItem);//, new Point(122, 9));
        }

        public bool SelectAllAndWaitForDeleteEffectuationWindowReady()
        {
            #region Variable Declarations
            DXButton uIVelgalleButton = this.UISletteiverksettingWindow.UIPnlToppInfoClient.UIVelgalleButton;
            DXButton uISlettiverksettingpå8Button = this.UISletteiverksettingWindow.UIPnlBottomClient.UISlettiverksettingpå8Button;
            #endregion

            Playback.Wait(2000);
            Mouse.Click(uIVelgalleButton);
            uISlettiverksettingpå8Button.WaitForControlEnabled(300000);

            return uISlettiverksettingpå8Button.Enabled;
        }

        public bool SalaryCalculationsWindowExists()
        {
            #region Variable Declarations
            var win = UILønnsberegningvediveWindow;
            UILønnsberegningvediveWindow.WaitForControlExist(600000);
            var ex = false;
            #endregion
            try
            {
                ex = win.Exists;
            }
            catch (Exception)
            {
                TestContext.WriteLine("SalaryCalculationsWindow do not exist");
            }

            return ex;
        }

        public bool CheckRosterPlanSaveChangesWindowExists()
        {
            Playback.Wait(1000);
            return UIItemWindow6.Exists;
        }

        public bool CheckDeleteEffectuationButtonEnabled(bool enabled)
        {
            #region Variable Declarations
            DXRibbonItem uISlettiverksettingRibbonItem = this.UIArbeidsplanYTELSEKalWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIGrpEffectuateNoneRevRibbonPageGroup.UISlettiverksettingRibbonItem;
            #endregion

            try
            {
                uISlettiverksettingRibbonItem.WaitForControlReady(600000);

                if (enabled)
                    uISlettiverksettingRibbonItem.WaitForControlCondition(IsStatusEnabled);
                else
                    uISlettiverksettingRibbonItem.WaitForControlCondition(IsStatusDisabled);

                Assert.AreEqual(enabled, uISlettiverksettingRibbonItem.Enabled, "Slett iverksettingknappen er i en uventet tilstand");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool CheckDeleteEffectuationHelpPlanButtonEnabled(bool enabled)
        {
            #region Variable Declarations
            DXRibbonItem uISlettiverksettingRibbonItem = this.UIArbeidsplanWindow6.UIRcMenuRibbon.UIRpPlanRibbonPage.UIGrpEffectuateRevolviRibbonPageGroup.UISlettiverksettingRibbonItem;
            bool buttonEnabled = !enabled;
            #endregion

            try
            {
                uISlettiverksettingRibbonItem.WaitForControlReady(600000);

                if (enabled)
                    uISlettiverksettingRibbonItem.WaitForControlCondition(IsStatusEnabled);
                else
                    uISlettiverksettingRibbonItem.WaitForControlCondition(IsStatusDisabled);


                Assert.AreEqual(enabled, uISlettiverksettingRibbonItem.Enabled, "Slett iverksettingknappen er i en uventet tilstand");
            }

            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static bool IsStatusEnabled(UITestControl control)
        {
            DXRibbonItem ribbonItem = control as DXRibbonItem;
            return ribbonItem.Enabled;
        }
        private static bool IsStatusDisabled(UITestControl control)
        {
            DXRibbonItem ribbonItem = control as DXRibbonItem;
            return !ribbonItem.Enabled;
        }

        /// <summary>
        /// SetStartDateNewRosterplan - Use 'SetStartDateNewRosterplanParams' to pass parameters into this method.
        /// </summary>
        public void SetStartDateNewRosterplan(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UINyarbeidsplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIDeStartDateCustom.UIPceDateDateTimeEdit;
            #endregion

            // Type ' [SelectionStart]0' in 'pceDate' DateTimeEdit
            //ValueAsString
            uIPceDateDateTimeEdit.DateTime = date;

            // Type '{Tab}' in 'pceDate' DateTimeEdit
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            
        }

        /// <summary>
        /// SetStartDateRosterplan - Use 'SetStartDateRosterplanParams' to pass parameters into this method.
        /// </summary>
        public void SetStartDateRosterplan(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEStartDateCustom.UIPceDateDateTimeEdit;
            #endregion

            // Type ' [SelectionStart]0' in 'pceDate' DateTimeEdit
            //ValueAsString
            uIPceDateDateTimeEdit.DateTime = date;

            // Type '{Tab}' in 'pceDate' DateTimeEdit
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        public void CreateNewRosterplanCopy(string name, DateTime fromDate, string rosterWeek, string weeks, bool chkTasks, bool chkDraft)
        {
            ClickNewRosterPlanCopy();
            SetStartDateNewRosterplan(fromDate);
            UIMapVS2017.CreateRosterplanCopy(name, rosterWeek, weeks, chkTasks, chkDraft);
        }

        public void CheckStartDateRosterplan(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEStartDateCustom.UIPceDateDateTimeEdit;
            #endregion

            // Verify that the 'ValueAsString' property of 'pceDate' DateTimeEdit equals '2016-01-24'
            Assert.AreEqual(date, uIPceDateDateTimeEdit.DateTime, "Feil startdato for arbeidsplan");

        }

        /// <summary>
        /// SetStartDateNewHelpplan - Use 'SetStartDateNewHelpplanParams' to pass parameters into this method.
        /// </summary>
        public void SetStartDateNewHelpplan(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UINyhjelpeplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIDeStartDateCustom.UIPceDateDateTimeEdit;
            #endregion

            // Type ' [SelectionStart]0' in 'pceDate' DateTimeEdit
            //ValueAsString
            uIPceDateDateTimeEdit.DateTime = date;

            // Type '{Tab}' in 'pceDate' DateTimeEdit
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// SetF3CalculationPeriodHelpplan - Use 'SetF3CalculationPeriodHelpplanParams' to pass parameters into this method.
        /// </summary>
        public void SetF3CalculationPeriodHelpplan(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UINyhjelpeplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIGcF3CalcualtionPerioClient.UIEF3FromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UINyhjelpeplanWindow.UIPnlMainClient.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIGcF3CalcualtionPerioClient.UIEF3ToDateCustom.UIPceDateDateTimeEdit;
            #endregion


            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }
        }

        /// <summary>
        /// SetValidToDateRosterplan - Use 'SetValidToDateRosterplanParams' to pass parameters into this method.
        /// </summary>
        public void SetValidToDateRosterplan(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEValidToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            // Type '2017-04-20 [SelectionStart]0[SelectionLength]10' in 'pceDate' DateTimeEdit
            //ValueAsString
            uIPceDateDateTimeEdit.DateTime = date;

            // Type '{Tab}' in 'pceDate' DateTimeEdit
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// FirstRotationDateRosterplan - Use 'FirstRotationDateRosterplanExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckFirstRotationDateRosterplan(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEStopDateCustom.UIPceDateDateTimeEdit;
            #endregion

            // Verify that the 'ValueAsString' property of 'pceDate' DateTimeEdit equals '2017-05-03'
            Assert.AreEqual(date, uIPceDateDateTimeEdit.DateTime, "Wrong date for first rotation");
        }
        public void CheckFirstRotationDateRosterplanDisabled()
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIArbeidsplanInnstilliWindow.UITcDataTabList.UITpConfigurationClient.UIPaCenterClient.UIEStopDateCustom.UIPceDateDateTimeEdit;
            #endregion

            // Verify that the 'ValueAsString' property of 'pceDate' DateTimeEdit equals '2017-05-03'
            Assert.AreEqual(false, uIPceDateDateTimeEdit.Enabled, "First rotation textbox not disabled");
        }

        /// <summary>
        /// SetExtraDate - Use 'SetExtraDateParams' to pass parameters into this method.
        /// </summary>
        public void SetExtraDate(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient.UIDeDateCustom.UIPceDateDateTimeEdit;
            DXTestControl uIGcShiftDetailsClient = this.UIMerarbeidovertidWindow.UIPanClientClient.UITcClientTabList.UITpMainClient.UIGcShiftDetailsClient;
            #endregion

            // Type '2017-04-20 [SelectionStart]0[SelectionLength]10' in 'pceDate' DateTimeEdit
            //ValueAsString
            uIPceDateDateTimeEdit.DateTime = date;

            // Type '{Tab}' in 'pceDate' DateTimeEdit
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }
                
        /// <summary>
        /// SetNewRemanageShiftDate - Use 'SetNewRemanageShiftDateParams' to pass parameters into this method.
        /// </summary>
        public void SetNewRemanageShiftDate(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIForskyvningWindow.UIPanClientPanelClient.UIGpcMainClient.UITcClientTabList.UITpMainClient.UIGpcShiftsClient.UIGcNewShiftsClient.UITcNewShiftTabList.UITpSimpleShiftCtrlClient.UIMainPanelClient.UIDeDateCustom.UIPceDateDateTimeEdit;
            #endregion

            // Type '2017-04-20 [SelectionStart]0[SelectionLength]10' in 'pceDate' DateTimeEdit
            //ValueAsString
            uIPceDateDateTimeEdit.DateTime = date;

            // Type '{Tab}' in 'pceDate' DateTimeEdit
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// SetRevolvingPeriod
        /// </summary>
        public void SetRevolvingPeriodInEmployeeWindow(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            var uIERevolveFromDateTimeEdit = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerRevolCustom.UIERevolveFromDateTimeEdit;
            var uIERevolveToDateTimeEdit = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerRevolCustom.UIERevolveToDateTimeEdit;
            #endregion

            if (fromDate != null)
            {
                uIERevolveToDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIERevolveToDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }

            if (toDate != null)
            {
                uIERevolveToDateTimeEdit.DateTime = toDate.Value;
                Keyboard.SendKeys(uIERevolveToDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }
        }

        /// <summary>
        /// SetHelpPlanWeeks
        /// </summary>
        public void SetHelpPlanWeeks(string weeks)
        {
            #region Variable Declarations
            DXTextEdit uIENumber0Edit = this.UINyhjelpeplanWindow.UIPnlMainClient1.UIGsLayoutControlMainCustom.UILayoutControlGroupNaLayoutGroup.UILayoutControlGroupNeLayoutGroup.UILcMainInfoLayoutControlItem.UIPnlMainInfoClient.UIENumber0Edit;
            #endregion

            // Type '8 [SelectionStart]0' in 'eNumber[0]' text box
            //ValueAsString
            uIENumber0Edit.ValueAsString = weeks;

            // Type '{Tab}' in 'eNumber[0]' text box
            Keyboard.SendKeys(uIENumber0Edit, "{TAB}", ModifierKeys.None);
        }

        /// <summary>
        /// SetFreeVacantShiftsPeriod - Use 'SetFreeVacantShiftsPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetFreeVacantShiftsPeriod(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIItemWindow1.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem1LayoutControlItem.UIGsPanelControl2Client.UISdeUnoccupiedShiftsFCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIItemWindow1.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem1LayoutControlItem.UIGsPanelControl2Client.UISdeUnoccupiedShiftsTCustom.UIPceDateDateTimeEdit;
            #endregion

            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }
        }

        /// <summary>
        /// SetExchangePeriod - Use 'SetExchangePeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetExchangePeriod(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIBytteWindow.UIGsPanelControl1Client.UIGrpPeriodClient.UIDeFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIBytteWindow.UIGsPanelControl1Client.UIGrpPeriodClient.UIDeToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }
        }

        /// <summary>
        /// SetDepartmentExchangePeriod - Use 'SetDepartmentExchangePeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetDepartmentExchangePeriod(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIByttemedavdelingWindow.UIGsPanelControl1Client.UIGrpPeriodClient.UIDeFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIByttemedavdelingWindow.UIGsPanelControl1Client.UIGrpPeriodClient.UIDeToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }
        }

        /// <summary>
        /// SetCalloutDate - Use 'SetCalloutDateParams' to pass parameters into this method.
        /// </summary>
        public void SetCalloutDate(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIUtrykningWindow.UI_layoutControlCustom.UIRootLayoutGroup.UILayoutControlItem3LayoutControlItem.UI_navBarNavBar.UINavBarGroupControlCoScrollableControl.UI_panShiftClient.UI_dtShiftDateCustom.UIPceDateDateTimeEdit;
            #endregion

            // Type '2017-04-20 [SelectionStart]0[SelectionLength]10' in 'pceDate' DateTimeEdit
            //ValueAsString
            uIPceDateDateTimeEdit.DateTime = date;

            // Type '{Tab}' in 'pceDate' DateTimeEdit
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        public void CalculateFTT(bool selectFttTab = true)
        {
            if (selectFttTab)
                UIMapVS2017.SelectRosterplanSubTab(CommonUIFunctions.UIMapVS2017Classes.UIMapVS2017.RosterplanTabs.Fastetillegg);
            
            UIMapVS2017.ClickCalculateFTTButton();
        }


        /// <summary>
        /// SetTransfereFromFTT - Use 'SetTransfereFromFTTParams' to pass parameters into this method.
        /// </summary>
        public void SetTransfereFromFTT(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIDeExportFromDateTimeEdit = this.UIOverførtilleggWindow.UIDeExportFromDateTimeEdit;
            #endregion


            // Type '2017-04-20 [SelectionStart]0[SelectionLength]10' in 'pceDate' DateTimeEdit
            //ValueAsString
            uIDeExportFromDateTimeEdit.DateTime = date;

            // Type '{Tab}' in 'pceDate' DateTimeEdit
            Keyboard.SendKeys(uIDeExportFromDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// SetAmlDispensionPeriod - Use 'SetAmlDispensionPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetAmlDispensionPeriod(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uISdeStartDateTimeEdit = this.UINyAMLdispensasjonWindow.UIViewHostDispensationCustom.UIPcViewClient.UIDispensationDetailsVCustom.UIVhDispInnerDetailsCustom.UIPcViewClient.UIDefaultDispDetailsViCustom.UISdeStartDateTimeEdit;
            DXDateTimePicker uISdeEndDateTimeEdit = this.UINyAMLdispensasjonWindow.UIViewHostDispensationCustom.UIPcViewClient.UIDispensationDetailsVCustom.UIVhDispInnerDetailsCustom.UIPcViewClient.UIDefaultDispDetailsViCustom.UISdeEndDateTimeEdit;
            #endregion

            if (fromDate != null)
            {
                uISdeStartDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uISdeStartDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }

            if (toDate != null)
            {
                uISdeEndDateTimeEdit.DateTime = toDate.Value;
                Keyboard.SendKeys(uISdeEndDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }
        }

        /// <summary>
        /// SetValidFromDateAMLagreement - Use 'SetValidFromDateAMLagreementParams' to pass parameters into this method.
        /// </summary>
        public void SetValidFromDateAMLagreement(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uISdeFromDateDateTimeEdit = this.UIAMLavtaleWindow.UIViewHostDispensationCustom.UIPcViewClient.UILimitContainerViewCustom.UIXtraTabControlLimitDTabList.UIXtraTabPageDetailsClient.UIPcLimitClient.UIViewHostLimitCustom.UIPcViewClient.UILimitDetailsViewCustom.UISdeFromDateDateTimeEdit;
            #endregion

            uISdeFromDateDateTimeEdit.DateTime = date;

            // Type '{Tab}' in 'pceDate' DateTimeEdit
            Keyboard.SendKeys(uISdeFromDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        public void CreateNewEmployment(DateTime? fromDate, DateTime? toDate, string empPercent, string ruleSet, string posCategory, string aml, string internalPosNumber, string dep, string costDep)
        {
            UIMapVS2017.ClickNewEmployment();
            SetEmploymentPeriod(fromDate, toDate);
            UIMapVS2017.CreateNewEmployment(empPercent, ruleSet, posCategory, aml, internalPosNumber, dep, costDep);
        }

        /// <summary>
        /// SetEmploymentPeriod - Use 'SetEmploymentPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetEmploymentPeriod(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIStillingsforholdWindow.UIPcContentClient.UISdeFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIStillingsforholdWindow.UIPcContentClient.UISdeToDateCustom.UIPceDateDateTimeEdit;
            #endregion
            

            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }
        }

        /// <summary>
        /// SetManningLayerPeriod - Use 'SetManningLayerPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetManningplanPeriod(DateTime fromDate, DateTime toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIBemanningsplanWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIBemanningsplanWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.DateTime = fromDate;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);

            uIPceDateDateTimeEdit1.DateTime = toDate;
            Keyboard.SendKeys(uIPceDateDateTimeEdit1, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// SetDayRythmplanPeriod - Use 'SetDayRythmplanPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SetDayRythmplanPeriod(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIDøgnrytmeplanWindow.UIPaNavbarPanelClient.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIDøgnrytmeplanWindow.UIPaNavbarPanelClient.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }
        }

        public void CheckDayRythmplanFromDate(DateTime fromDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIDøgnrytmeplanWindow.UIPaNavbarPanelClient.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtFromDateCustom.UIPceDateDateTimeEdit;
            #endregion

            Assert.AreEqual(fromDate, uIPceDateDateTimeEdit.DateTime, "Feil fradato");           
        }

        public void CheckDayRythmplanToDate(DateTime toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIDøgnrytmeplanWindow.UIPaNavbarPanelClient.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            Assert.AreEqual(toDate, uIPceDateDateTimeEdit1.DateTime, "Feil tildato");
        }

        /// <summary>
        /// SetManningLayerFromDate - Use 'SetManningLayerFromDateParams' to pass parameters into this method.
        /// </summary>
        public void SetManningLayerFromDate(DateTime fromDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIBemanningsplanlagWindow.UIPaNavbarsPanelClient.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtFromDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.DateTime = fromDate;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// SetDayRythmLayerFromDate - Use 'SetDayRythmLayerFromDateParams' to pass parameters into this method.
        /// </summary>
        public void SetDayRythmLayerFromDate(DateTime fromDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtFromDateCustom.UIPceDateDateTimeEdit;
            #endregion
            try
            {
                CheckDayRythmplanFromdate();

                uIPceDateDateTimeEdit.DateTime = fromDate;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Unable to set dayrythmplan fromtime: " + e.Message);
            }
        }

        public void CheckRythmLayerFromDate(DateTime checkDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtFromDateCustom.UIPceDateDateTimeEdit;
            #endregion

            Assert.AreEqual(checkDate, uIPceDateDateTimeEdit.DateTime, "Feil fradato");
        }
        
        /// <summary>
        /// SetLayerRotateToDate - Use 'SetLayerRotateToDateParams' to pass parameters into this method.
        /// </summary>
        public void SetLayerRotateToDate(DateTime toDate)
        {
            #region Variable Declarations
            DXRadioGroup uIGsRotationRadioGroup = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIGsRotationRadioGroup;
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIDøgnrytmelagWindow.UIGsMainControlNavBar.UINbgccDetailsScrollableControl.UIDtToDateCustom.UIPceDateDateTimeEdit;
            #endregion
            
            uIGsRotationRadioGroup.SelectedIndex = this.SetLayerRotateToDateParams.UIGsRotationRadioGroupSelectedIndex;
            uIPceDateDateTimeEdit.DateTime = toDate;

            // Type '{NumPad2}{Tab}' in 'pceDate' DateTimeEdit
            Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetLayerRotateToDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// SetRotationPeriodLineSettings - Use 'SetRotationPeriodLineSettingsParams' to pass parameters into this method.
        /// </summary>
        public void SetRotationPeriodLineSettings(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UILinjeinnstillingerDaWindow.UIGsPanelControl3Client.UIERevolveFromCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UILinjeinnstillingerDaWindow.UIGsPanelControl3Client.UIERevolveToCustom.UIPceDateDateTimeEdit;
            #endregion

            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}", ModifierKeys.None);
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, "{TAB}", ModifierKeys.None);
            }
        }

        /// <summary>
        /// SetEffectiveFromRateDateNew - Use 'SetEffectiveFromRateDateNewParams' to pass parameters into this method.
        /// </summary>
        public void SetEffectiveFromRateDateNew(DateTime fromDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIDeEffectiveFromDateTimeEdit = this.UINyevaktklassesatserWindow.UIDeEffectiveFromDateTimeEdit;
            #endregion

            uIDeEffectiveFromDateTimeEdit.DateTime = fromDate;
            Keyboard.SendKeys(uIDeEffectiveFromDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
        }

        /// <summary>
        /// CreateEqualizeShift - Use 'CreateEqualizeShiftParams' to pass parameters into this method.
        /// </summary>
        public void CreateEqualizeShift(bool extendShift, bool endOfShift, string minutes)
        {
            #region Variable Declarations
            DXMenuItem uIFunksjonerMenuItem = this.UIItemWindow11.UIPopupMenuBarControlMenu.UIFunksjonerMenuItem;
            DXMenuBaseButtonItem uIUtjevningsvaktMenuBaseButtonItem = this.UIItemWindow11.UIPopupMenuBarControlMenu.UIFunksjonerMenuItem.UIUtjevningsvaktMenuBaseButtonItem;
            DXRadioGroup uIRgrpEqualizationModeRadioGroup = this.UIUtjevningsvaktWindow.UIGrpEqualizingClient.UIRgrpEqualizationModeRadioGroup;
            DXRadioGroup uIRgrpEqualizationPlacRadioGroup = this.UIUtjevningsvaktWindow.UIGrpEqualizingClient.UIRgrpEqualizationPlacRadioGroup;
            DXTextEdit uIENumberEdit = this.UIUtjevningsvaktWindow.UIGrpEqualizingClient.UIENumberEdit;
            DXButton uIOKButton = this.UIUtjevningsvaktWindow.UIPnlButtonsClient.UIOKButton;
            #endregion

            // Click 'Funksjoner' menu item
            Playback.Wait(1500);
            Mouse.Click(uIFunksjonerMenuItem, new Point(104, 13));

            // Click 'Utjevningsvakt...' MenuBaseButtonItem
            Playback.Wait(1000);
            Mouse.Click(uIUtjevningsvaktMenuBaseButtonItem, new Point(73, 10));

            uIRgrpEqualizationModeRadioGroup.SelectedIndex = Convert.ToInt16(extendShift);
            uIRgrpEqualizationPlacRadioGroup.SelectedIndex = Convert.ToInt16(endOfShift);

            uIENumberEdit.ValueAsString = minutes;
            Keyboard.SendKeys(uIENumberEdit, "{TAB}", ModifierKeys.None);

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }

        public void DeleteRotationPeriodLineSettingsToDate()
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UILinjeinnstillingerDaWindow.UIGsPanelControl3Client.UIERevolveToCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit1.ValueAsString = "";
            Keyboard.SendKeys(uIPceDateDateTimeEdit1, "{TAB}", ModifierKeys.None);
        }

        /// <summary>
        /// SetExchangeDateForLines - Use 'SetExchangeDateForLinesParams' to pass parameters into this method.
        /// </summary>
        public void SetExchangeDateForLines(DateTime date)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIByttansattWindow.UIPcContentClient.UIGsPanelControl3Client.UIGsPanelControl4Client.UIEFromDateCustom.UIPceDateDateTimeEdit;
            #endregion

            uIPceDateDateTimeEdit.DateTime = date;
            Keyboard.SendKeys(uIPceDateDateTimeEdit, "{TAB}", ModifierKeys.None);
        }

        /// <summary>
        /// ChangeEffectuationPeriodRosterplan - Use 'ChangeEffectuationPeriodRosterplanParams' to pass parameters into this method.
        /// </summary>
        public void ChangeEffectuationPeriodRosterplan(DateTime? fromDate, DateTime? toDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIEndreiverksettingspeWindow.UIEFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIEndreiverksettingspeWindow.UIEToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            //ValueAsString
            if (toDate == null)
                toDate = fromDate;

            if (fromDate != null)
            {
                uIPceDateDateTimeEdit.DateTime = fromDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }

            if (toDate != null)
            {
                uIPceDateDateTimeEdit1.DateTime = toDate.Value;
                Keyboard.SendKeys(uIPceDateDateTimeEdit1, this.SetExtraDateParams.UIPceDateDateTimeEditSendKeys, ModifierKeys.None);
            }
        }

        public DateTime CheckEffectuationPeriodRosterplan(bool returnFromDate)
        {
            #region Variable Declarations
            DXDateTimePicker uIPceDateDateTimeEdit = this.UIEndreiverksettingspeWindow.UIEFromDateCustom.UIPceDateDateTimeEdit;
            DXDateTimePicker uIPceDateDateTimeEdit1 = this.UIEndreiverksettingspeWindow.UIEToDateCustom.UIPceDateDateTimeEdit;
            #endregion

            if (returnFromDate)
                return uIPceDateDateTimeEdit.DateTime;
            else
                return uIPceDateDateTimeEdit1.DateTime;
        }

        /// <summary>
        /// CheckOkEffectuationPeriodEnabled - Use 'CheckOkEffectuationPeriodEnabledExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckOkEffectuationPeriodEnabled(bool enabled)
        {
            #region Variable Declarations
            DXButton uIOKButton = this.UIEndreiverksettingspeWindow.UIPnlButtonsClient.UIOKButton;
            #endregion

            // Verify that the 'Enabled' property of 'Ok' button equals 'True'
            Assert.AreEqual(enabled, uIOKButton.Enabled, "Failed!");
        }

        public void SelectMainWindowTab(SupportFunctions.MainWindowTabs tab, Point point = new Point())
        {
            //SelectShiftBookTab
            #region Variable Declarations
            WinClient gatMainWindow = this.GatMain_02.UIItemWindow.UIGatver64339794ASCLAvClient;
            #endregion

            switch (tab)
            {
                case SupportFunctions.MainWindowTabs.ClickDefinedPoint:
                    Mouse.Click(gatMainWindow, point);
                    break;

                case SupportFunctions.MainWindowTabs.Shiftbook:

                    //Vaktbok
                    Mouse.Click(gatMainWindow, new Point(30, 15));
                    break;
                case SupportFunctions.MainWindowTabs.Extrainfo:

                    /// <summary>
                    /// SelectExtraInfoTab
                    /// </summary>

                    // Click 'Gat ver. 6.4.3.39794 - (ASCL - Avd: 2060-AML avdel...' client
                    Mouse.Click(gatMainWindow, new Point(95, 15));
                    break;
                case SupportFunctions.MainWindowTabs.EFO:

                    /// <summary>
                    /// SelectEFOTab
                    /// </summary>

                    // Click 'Gat ver. 6.4.3.39794 - (ASCL - Avd: 2060-AML avdel...' client
                    Mouse.Click(gatMainWindow, new Point(150, 15));

                    //ClickOkEFOnoBudgetDialog();

                    break;
                case SupportFunctions.MainWindowTabs.LIS:

                    /// <summary>
                    /// SelectLISTab
                    /// </summary>

                    // Click 'Gat ver. 6.4.3.39794 - (ASCL - Avd: 2060-AML avdel...' client
                    Mouse.Click(gatMainWindow, new Point(190, 15));
                    break;
                case SupportFunctions.MainWindowTabs.Turnusplan:

                    /// <summary>
                    /// SelectTurnusplanTab
                    /// </summary>

                    // Click 'Gat ver. 6.4.3.39794 - (ASCL - Avd: 2060-AML avdel...' client
                    Mouse.Click(gatMainWindow, new Point(245, 15));
                    break;
                case SupportFunctions.MainWindowTabs.Rosterplan:

                    /// <summary>
                    /// SelectRosterplanTab
                    /// </summary>
                    Mouse.Click(gatMainWindow, new Point(250, 15));

                    break;
                case SupportFunctions.MainWindowTabs.Grovplan:

                    /// <summary>
                    /// SelectGrovplanTab
                    /// </summary>
                    Mouse.Click(gatMainWindow, new Point(320, 15));

                    break;
                case SupportFunctions.MainWindowTabs.Produksjonsplan:

                    Mouse.Click(gatMainWindow, new Point(405, 15));

                    break;
                case SupportFunctions.MainWindowTabs.Employee:

                    /// <summary>
                    /// SelectEmployeeTab
                    /// </summary>
                    Mouse.Click(gatMainWindow, new Point(485, 15));

                    break;
                case SupportFunctions.MainWindowTabs.Department:

                    /// <summary>
                    /// SelectDepartmentTab
                    /// </summary>
                    Mouse.Click(gatMainWindow, new Point(540, 15));
                    break;
                case SupportFunctions.MainWindowTabs.DepartmentBetong:

                    /// <summary>
                    /// SelectDepartmentTab
                    /// </summary>
                    Mouse.Click(gatMainWindow, new Point(140, 15));
                    break;
                case SupportFunctions.MainWindowTabs.ReportCurrent:

                    /// <summary>
                    /// SelectReportsTab
                    /// </summary>
                    /// 
                    /*
                     Nytt rapportsenter 
                     * if (gat65X)
                        Mouse.Click(gatMainWindow, new Point(735, 15));
                     */
                    Mouse.Click(gatMainWindow, new Point(620, 15));
                    break;
                case SupportFunctions.MainWindowTabs.ReportCurrentSE:

                    /// <summary>
                    /// SelectReportsTab
                    /// </summary>
                    Mouse.Click(gatMainWindow, new Point(425, 15));
                    break;
                case SupportFunctions.MainWindowTabs.Budget:

                    /// <summary>
                    /// SelectBudgetTab
                    /// </summary>
                    Mouse.Click(gatMainWindow, new Point(695, 15));
                    break;
                case SupportFunctions.MainWindowTabs.OtAdmin:

                    /// <summary>
                    /// SelectOtAdminTab
                    /// </summary>

                    // Click 'Gat ver. 6.4.3.39794 - (ASCL - Avd: 2060-AML avdel...' client
                    //725
                    Mouse.Click(gatMainWindow, new Point(845, 15));
                    break;
                case SupportFunctions.MainWindowTabs.Administration:

                    /// <summary>
                    /// SelectAdministrationTab
                    /// </summary>

                    // Click 'Gat ver. 6.4.3.39794 - (ASCL - Avd: 2060-AML avdel...' client
                    Mouse.Click(gatMainWindow, new Point(770, 15));
                    break;
                default:
                    break;
            }
        }

        #region Main Tabs Actions 

        public void SelectSubTabInEmployeeTab(EmployeeTabs tab, bool SwitchLines, bool isSe = false)
        {
            #region Variable Declarations          
            WinClient uIAnsattClient = this.UIGatWindow2.UIItemWindow.UIAnsattClient;
            if (SwitchLines)
                Mouse.Click(uIAnsattClient, new Point(20, 660));
            #endregion

            switch (tab)
            {
                case EmployeeTabs.EmploymentTab:
                    Mouse.Click(uIAnsattClient, new Point(85, 642));
                    break;
                case EmployeeTabs.EffectuationPeriodsTab:
                    Mouse.Click(uIAnsattClient, new Point(200, 642));
                    break;
                case EmployeeTabs.ShiftsTab:
                    Mouse.Click(uIAnsattClient, new Point(290, 642));
                    break;
                case EmployeeTabs.AbsenceTab:
                    if (isSe)
                        Mouse.Click(uIAnsattClient, new Point(529, 642));
                    else
                        Mouse.Click(uIAnsattClient, new Point(565, 642));
                    break;
                case EmployeeTabs.TimesheetTab:
                    Mouse.Click(uIAnsattClient, new Point(765, 642));
                    break;
                case EmployeeTabs.TimesheetTabBR:
                    Mouse.Click(uIAnsattClient, new Point(805, 642));
                    break;
                case EmployeeTabs.EkstraEmployeeTab:
                    Mouse.Click(uIAnsattClient, new Point(815, 642));
                    break;
                case EmployeeTabs.CalloutsTab:
                    Mouse.Click(uIAnsattClient, new Point(980, 642));
                    break;
                case EmployeeTabs.EmployeeBanksTab:
                    Mouse.Click(uIAnsattClient, new Point(1180, 642));
                    break;
                case EmployeeTabs.AmlDispTab:
                    Mouse.Click(uIAnsattClient, new Point(1390, 642));
                    break;
                case EmployeeTabs.AmlBrakesTab:
                    if (isSe)
                        Mouse.Click(uIAnsattClient, new Point(1200, 660));
                    else
                        Mouse.Click(uIAnsattClient, new Point(1460, 642));
                    break;

                case EmployeeTabs.DocTab:
                    if (isSe)
                        Mouse.Click(uIAnsattClient, new Point(250, 660));
                    else
                        Mouse.Click(uIAnsattClient, new Point(160, 660));
                    break;
                case EmployeeTabs.DayAndWeekSeparationEmployeeTab:
                    if (isSe)
                        Mouse.Click(uIAnsattClient, new Point(705, 660));
                    else
                        Mouse.Click(uIAnsattClient, new Point(710, 660));
                    break;

                case EmployeeTabs.WeekendAgreementTab:
                    Mouse.Click(uIAnsattClient, new Point(970, 660));
                    break;
            }
        }

        public enum EmployeeTabs
        {
            TimesheetTab,
            TimesheetTabBR,
            WeekendAgreementTab,
            EmploymentTab,
            EkstraEmployeeTab,
            EmployeeBanksTab,
            EffectuationPeriodsTab,
            AbsenceTab,
            ShiftsTab,
            AmlDispTab,
            AmlBrakesTab,
            CalloutsTab,
            DayAndWeekSeparationEmployeeTab,
            DocTab
        }
        public void SelectSubTabInDepartmentTab(DepartmentTabs tab, bool SwitchLines)
        {
            #region Variable Declarations
            WinClient uIAvdelingClient = UIMapVS2017.UIGatWindow.UIItemWindow.UIAvdelingClient;
            var point = new Point(0, 0);
            Playback.Wait(500);
            if (SwitchLines)
            {
                Mouse.Click(uIAvdelingClient, new Point(20, 875));
                Playback.Wait(1000);
            }
            #endregion

            switch (tab)
            {
                case DepartmentTabs.Liste:
                    point = new Point(40, 855);
                    break;
                case DepartmentTabs.Stillingsforhold:
                    point = new Point(150, 855);
                    break;
                case DepartmentTabs.Stillingsutlån:
                    point = new Point(300, 855);
                    break;
                case DepartmentTabs.Vaktkoder:
                    point = new Point(400, 855);
                    break;
                case DepartmentTabs.Vaktkodemønster:
                    point = new Point(550, 855);
                    break;
                case DepartmentTabs.Kolonner:
                    point = new Point(680, 855);
                    break;
                case DepartmentTabs.Kompetansebehov:
                    point = new Point(820, 855);
                    break;
                case DepartmentTabs.Grupper:
                    point = new Point(960, 855);
                    break;
                case DepartmentTabs.Oppgave:
                    point = new Point(1060, 855);
                    break;
                case DepartmentTabs.Oppgavemønster:
                    point = new Point(1200, 855);
                    break;
                case DepartmentTabs.Merknader:
                    point = new Point(1340, 855);
                    break;
                case DepartmentTabs.Gjøremål:
                    point = new Point(1440, 855);
                    break;
                case DepartmentTabs.GjøremålsEkskludering:
                    point = new Point(1600, 855);
                    break;
                case DepartmentTabs.BrukereMedTilgang:
                    point = new Point(1810, 855);
                    break;
                case DepartmentTabs.Dok:
                    point = new Point(40, 875);
                    break;
                case DepartmentTabs.Fleksitid:
                    point = new Point(130, 875);
                    break;
                case DepartmentTabs.Bemanningsplan:
                    point = new Point(270, 875);
                    break;
                case DepartmentTabs.BemanningsplanBetong:
                    point = new Point(85, 875);
                    break;
                case DepartmentTabs.Døgnrytmeplan:
                    point = new Point(440, 875);
                    break;
                case DepartmentTabs.DøgnrytmeplanBetong:
                    point = new Point(170, 875);
                    break;
                case DepartmentTabs.Timeregistrering:
                    point = new Point(620, 875);
                    break;
                case DepartmentTabs.Bemanningsbudsjett:
                    point = new Point(820, 875);
                    break;
                case DepartmentTabs.Gjennomsnittsberegning:
                    point = new Point(1060, 875);
                    break;
                case DepartmentTabs.Aktivitetsplanlegging:
                    point = new Point(1300, 875);
                    break;
                case DepartmentTabs.Ukeplanperioder:
                    point = new Point(1500, 875);
                    break;
                case DepartmentTabs.Oppgavedeling:
                    point = new Point(1680, 875);
                    break;
                case DepartmentTabs.Integrasjon:
                    point = new Point(1830, 875);
                    break;
                default:
                    break;
            }

            Mouse.Click(uIAvdelingClient, point);
        }

        public enum DepartmentTabs
        {
            Liste,
            Stillingsforhold,
            Stillingsutlån,
            Vaktkoder,
            Vaktkodemønster,
            Kolonner,
            Kompetansebehov,
            Grupper,
            Oppgave,
            Oppgavemønster,
            Merknader,
            Gjøremål,
            GjøremålsEkskludering,
            BrukereMedTilgang,
            Dok,
            Fleksitid,
            Bemanningsplan,
            BemanningsplanBetong,
            Døgnrytmeplan,
            DøgnrytmeplanBetong,
            Timeregistrering,
            Bemanningsbudsjett,
            Gjennomsnittsberegning,
            Aktivitetsplanlegging,
            Ukeplanperioder,
            Oppgavedeling,
            Integrasjon
        }

        #endregion 

        public void SelectPlanTabRosterplan()
        {
            #region Variable Declarations
            Playback.Wait(5000);
            #endregion            

            SelectRosterplanPlanTab();
        }

        public int SelectRosterPlan(string planName, bool openPlan = true, bool showAllPlans = true)
        {
            #region Variable Declarations 
            var rosterPlanGrid = GatMain.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable;
            var selectCell = new DXCell();
            #endregion

            if (showAllPlans)
            {
                try
                {
                    ShowAllPlans();
                }
                catch (Exception)
                {
                    TestContext.WriteLine("Error showing all plans");
                }
            }

            var view = rosterPlanGrid.Views[0];
            planName = planName.Trim().ToLower();
            for (int i = 0; i < view.RowCount; i++)
            {
                var val = view.GetCellValue("gcolPlan", i).ToString().Trim().ToLower();

                if (val == planName)
                {
                    selectCell = view.GetCell("gcolPlan", i);
                    selectCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gcRosterPlansGridControlCell[View]gvRosterPlans[Row]" + i + "[Column]gcolPlan";
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


            if (openPlan)
            {
                try
                {
                    ClickOpenRosterplanButton();
                }
                catch (Exception)
                {
                    try
                    {
                        Mouse.DoubleClick(selectCell);
                    }
                    catch (Exception)
                    {
                        if (RPL2_4007_Informasjon.Exists)
                            IgnoreLockedRosterplan();
                    }
                }

                try
                {
                    if (RPL2_4007_Informasjon.Exists)
                        IgnoreLockedRosterplan();
                }
                catch (Exception)
                {
                    TestContext.WriteLine("Error checking lockedplan dialog");
                }
            }

            Playback.Wait(2000);

            try
            {
                return selectCell.RowHandle;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public bool CheckRosterPlanExists(string planName)
        {
            #region Variable Declarations 
            var rosterPlanGrid = GatMain.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable;
            var selectCell = new DXCell();
            #endregion

            var view = rosterPlanGrid.Views[0];
            var rowCount = view.RowCount;
            if (planName == "")
            {
                return rowCount > 0;
            }

            planName = planName.Trim().ToLower();
            for (int i = 0; i < view.RowCount; i++)
            {
                var val = view.GetCellValue("gcolPlan", i).ToString().Trim().ToLower();

                if (val == planName)
                {
                    return true;                    
                }
            }

            return false;
        }
        public int CheckNumberOfRosterplans()
        {
            #region Variable Declarations 
            var rosterPlanGrid = GatMain.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIGcRosterPlansTable;
            #endregion

            var view = rosterPlanGrid.Views[0];
            return view.RowCount;
        }
        /// <summary>
        /// ShowAllPlans
        /// </summary>
        public void ShowAllPlans(bool newPlans = true, bool oldPlans = true)
        {
            ShowNewPlans(newPlans);
            ShowOldPlans(oldPlans);
        }

        public enum TaskAction
        {
            Save,
            DoNotSave,
            Cancel
        }

        public void SaveRosterplanTasks(TaskAction action)
        {
            try
            {
                if (!UIRPL4055InformasjonWindow.Exists)
                    return;
            }
            catch
            {
                return;
            }

            switch (action)
            {
                case TaskAction.Save:
                    SaveRosterplanTasks();
                    break;
                case TaskAction.DoNotSave:
                    DoNotSaveRosterplanTasks();
                    break;
                case TaskAction.Cancel:
                    CancelSaveRosterplanTasks();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ClickEditRosterPlanFromPlantab
        /// </summary>
        public void ClickEditRosterPlanFromPlantab()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIRedigerRibbonBaseButtonItem = this.UIArbeidsplanYTELSEKalWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRibbonPageGroup9RibbonPageGroup.UIRedigerRibbonBaseButtonItem;
            #endregion

            // Click 'Rediger' RibbonBaseButtonItem
            Playback.Wait(3000);
            uIRedigerRibbonBaseButtonItem.WaitForControlReady(600000);
            uIRedigerRibbonBaseButtonItem.DrawHighlight();
            Mouse.Click(uIRedigerRibbonBaseButtonItem);
        }

        /// <summary>
        /// ShowNewPlans
        /// </summary>
        private void ShowNewPlans(bool activate = true)
        {
            #region Variable Declarations
            DXRibbonButtonItem uINyeRibbonBaseButtonItem = GatMain.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UINyeRibbonBaseButtonItem;
            #endregion

            if (uINyeRibbonBaseButtonItem.Checked != activate)
            {
                // Click 'Nye' RibbonBaseButtonItem
                Mouse.Click(uINyeRibbonBaseButtonItem);
            }
        }

        public bool NewPlansState()
        {
            #region Variable Declarations
            DXRibbonButtonItem uINyeRibbonBaseButtonItem = GatMain.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UINyeRibbonBaseButtonItem;
            #endregion

            return uINyeRibbonBaseButtonItem.Checked;
        }

        private void ShowOldPlans(bool activate = true)
        {
            #region Variable Declarations
            DXRibbonButtonItem uIGamleRibbonBaseButtonItem = GatMain.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UIGamleRibbonBaseButtonItem;
            #endregion

            if (uIGamleRibbonBaseButtonItem.Checked != activate)
            {
                Mouse.Click(uIGamleRibbonBaseButtonItem);
            }
        }

        public bool OldPlansState()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIGamleRibbonBaseButtonItem = GatMain.UIViewHostCustom.UIPcViewClient.UIPcMainPanelClient.UITcPanListsTabList.UITpRosterPlanListClient.UIRosterPlanListCustom.UIPnlGridClient.UIRibbonControlRibbon.UIRpHomeRibbonPage.UIRpgFiltersRibbonPageGroup.UIGamleRibbonBaseButtonItem;
            #endregion

            return uIGamleRibbonBaseButtonItem.Checked;
        }

        /// <summary>
        /// ShowOldWishPlans
        /// </summary>
        public void ShowOldWishPlans()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIGamleRibbonBaseButtonItem = this.GatMain_11.UIPcMainPanelClient.UITcPanListsTabList.UITpWishPlanListClient.UIWishPlanListCustom.UIGsPanelControl2Client.UIRibbonControl1Ribbon.UIRpHomeRibbonPage.UIRpgFilterRibbonPageGroup.UIGamleRibbonBaseButtonItem;
            #endregion

            if (!uIGamleRibbonBaseButtonItem.Checked)
            {
                Mouse.Click(uIGamleRibbonBaseButtonItem);
            }
        }

        /// <summary>
        /// SelectAbsenceCode
        /// </summary>
        public void SelectAbsenceCode(string absCode, string functionListNo = "")
        {
            #region Variable Declarations
            DXMenuItem uIFunksjonerMenuItem = this.UIItemWindow8.UIPopupMenuBarControlMenu.UIFunksjonerMenuItem;

            if(functionListNo != "")
            uIFunksjonerMenuItem.SearchProperties[DXTestControl.PropertyNames.Name] = "BarSubItemLink[" + functionListNo  + "]";

            DXMenuBaseButtonItem uIBarButtonItemLink1MenuBaseButtonItem = this.UIItemWindow8.UIPopupMenuBarControlMenu.UIFunksjonerMenuItem.UIBarButtonItemLink1MenuBaseButtonItem;
            var lookupEdit = this.UIFraværiarbeidsplanWindow.UINbcAbsenceNavBar.UINavBarGroupControlCoScrollableControl.UIDdlAbsCodeLookUpEdit;
            DXWindow uIPopupLookUpEditFormWindow = this.UIFraværiarbeidsplanWindow.UINbcAbsenceNavBar.UINavBarGroupControlCoScrollableControl.UIDdlAbsCodeLookUpEdit.UIPopupLookUpEditFormWindow;
            DXButton uIOKButton = this.UIFraværiarbeidsplanWindow.UIPnlButtonsClient.UIOKButton;
            #endregion

            // Wait for 1 seconds for user delay between actions; Click 'Funksjoner' menu item
            Playback.Wait(1000);
            Mouse.Click(uIFunksjonerMenuItem);
            Mouse.Click(uIBarButtonItemLink1MenuBaseButtonItem);

            // Click 'PopupLookUpEditForm' window
            Mouse.Click(lookupEdit);
            //lookupEdit.ValueAsString = this.SelectAbsenceCodeParams.UIDdlAbsCodeLookUpEditValueAsString;
            Keyboard.SendKeys(lookupEdit, absCode + "{ENTER}");
            //Keyboard.SendKeys(uIPopupLookUpEditFormWindow, selectString + "{ENTER}");

            // Click 'Ok' button
            Mouse.Click(uIOKButton);
        }

        public void SelectFunctionInRosterplanRightClickMenu(string functionListNo)
        {
            #region Variable Declarations
            DXMenuItem uIFunksjonerMenuItem = this.UIItemWindow8.UIPopupMenuBarControlMenu.UIFunksjonerMenuItem;
            uIFunksjonerMenuItem.SearchProperties[DXTestControl.PropertyNames.Name] = "BarSubItemLink[" + functionListNo + "]";
            #endregion

            // Wait for 1 seconds for user delay between actions; Click 'Funksjoner' menu item
            Playback.Wait(1000);
            Mouse.Click(uIFunksjonerMenuItem);
        }

        public void CreateRosterplanDemand(string department, string type, bool effectuate = false, bool transfere = false, bool publish = false)
        {
            UIMapVS2017.CreateRosterplanDemand(UIMapVS2017Classes.UIMapVS2017.ConfirmRegistrationTypes.New, department, type, UIMapVS2017Classes.UIMapVS2017.ConfirmRegistrationTypes.Ok, effectuate, transfere, publish);
        }

        public void CloseRosterplanDemandWindow()
        {UIMapVS2017.CloseRosterplanDemandWindow();}

        public void CreateRosterplanRepresentation(string roleName, string department, string type, List<string> unionList, List<string> repList)
        {
            UIMapVS2017.CreateRosterplanRepresentation(UIMapVS2017Classes.UIMapVS2017.ConfirmRegistrationTypes.New, roleName, department, type,  unionList, repList, UIMapVS2017Classes.UIMapVS2017.ConfirmRegistrationTypes.Ok);
        }
        public void EditRosterplanRepresentation(string repName, List<string> repList)
        {
            UIMapVS2017.EditRosterplanRepresentation(repName, "", null, null,null, repList, UIMapVS2017Classes.UIMapVS2017.ConfirmRegistrationTypes.Ok);
        }
        public void CloseRosterplanRepresentationWindow()
        {UIMapVS2017.CloseRosterplanRepresentationWindow();}

        /// <summary>
        /// SelectFromNewAdministrationTab - Use 'SelectFromNewAdministrationTabParams' to pass parameters into this method.
        /// </summary>
        public void SelectFromAdministration(string searchString, bool selectAdminTab = false)
        {
            #region Variable Declarations
            DXMRUEdit uIScOptionsMRUEdit = this.GatMain.UIViewHostCustom.UIPcViewClient.UIAdministrationViewCustom.UIPanelControl1Client.UIScOptionsMRUEdit;
            DXTreeList uITlOptionsTreeList = this.GatMain.UIViewHostCustom.UIPcViewClient.UIAdministrationViewCustom.UIPanelControl1Client.UITlOptionsTreeList;
            #endregion

            if (selectAdminTab)
                SelectMainWindowTab(SupportFunctions.MainWindowTabs.Administration);

            Playback.Wait(1000);
            searchString = "+" + searchString;
            uIScOptionsMRUEdit.ValueAsString = searchString;

            //UIMapVS2017.ExpandAdministrationTreeList();

            Playback.Wait(2000);
            Keyboard.SendKeys(uITlOptionsTreeList, "{PGDN}");

            Playback.Wait(1000);
            Keyboard.SendKeys("{Enter}");

            Playback.Wait(2000);
            //Keyboard.SendKeys(uITlOptionsTreeList, "{Enter}");
        }

        [Obsolete("DeleteAdministrationSearchString is deprecated", true)]
        public void ClearAdministrationSearchString()
        {
            UIMapVS2017.DeleteAdministrationSearchString();
        }

        /// <summary>
        /// SelectReport - Use 'SelectReportParams' to pass parameters into this method.
        /// </summary>
        public void SelectReport(string reportName, bool changeSettings, bool sortById)
        {
            #region Variable Declarations
            WinClient uIPanel1Client = this.GatMain_04.UIItemWindow.UIPanel1Client;
            WinEdit uIItemEdit = this.UIVelgradWindow.UIItemWindow.UIItemEdit;
            #endregion

            SelectMainWindowTab(SupportFunctions.MainWindowTabs.ReportCurrent);

            if (!changeSettings)
                return;

            SelectReportFixedPoint();

            if (sortById)
                UIMapVS2017.SortReportsById();
            else
                SortReportlistByReportName();



            // Type '{F3}' in 'Panel1' client
            Keyboard.SendKeys(this.SelectReportParams.UIF3, ModifierKeys.None);

            // Type 'reportname' in text box
            //uIItemEdit.Text = reportName(Leasetime error);
            Keyboard.SendKeys(uIItemEdit, reportName);

            // Type '{Enter}' in text box
            Playback.Wait(500);
            Keyboard.SendKeys(uIItemEdit, this.SelectReportParams.UIEnter, ModifierKeys.None);
        }

        private void SelectReportFixedPoint()
        {
            var toClick = new Point(GatMain_04.Width / 4, GatMain_04.Height / 2);
            Mouse.Click(toClick);
        }

        /// <summary>
        /// SortReportlistByID
        /// </summary>
        private void SortReportlistByID()
        {
            #region Variable Declarations
            WinClient uIPanel1Client = this.GatMain_04.UIItemWindow.UIPanel1Client;
            #endregion

            // Click 'Panel1' client
            Mouse.Click(uIPanel1Client, new Point(35, 7));
        }

        /// <summary>
        /// SortReportlistByReportName
        /// </summary>
        private void SortReportlistByReportName()
        {
            #region Variable Declarations
            WinClient uIPanel1Client = this.GatMain_04.UIItemWindow.UIPanel1Client;
            #endregion

            // Click 'Panel1' client
            Mouse.Click(uIPanel1Client, new Point(139, 8));
        }

        public void AmlReport7Settings(string fromDate, string toDate)
        {
            #region Variable Declarations
            WinEdit uIItemEdit1 = this.GatMain_04.UIItemWindow1.UIItemEdit;
            WinEdit uIItemEdit2 = this.GatMain_04.UIItemWindow2.UIItemEdit;
            #endregion

            // Type '05.09.2016' in text box
            uIItemEdit1.DrawHighlight();
            uIItemEdit1.Text = fromDate;

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit1, this.SelectReportParams.UITab, ModifierKeys.None);

            // Type '20.10.2016' in text box
            uIItemEdit2.Text = toDate;

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit2, this.SelectReportParams.UITab, ModifierKeys.None);
        }

        public void ExportToExcelFromPreview(string fileName, bool saveAsXlsx = false)
        {
            #region Variable Declarations
            WinComboBox uIFilenameComboBox = this.UISaveAsWindow.UIDetailsPanePane.UIFilenameComboBox;
            WinButton uISaveButton = this.UISaveAsWindow.UISaveWindow.UISaveButton;
            WinButton uICloseButton = this.UITmp934FtmpExcelWindow.UIItemWindow.UIRibbonPropertyPage.UICloseButton;
            #endregion        

            if (saveAsXlsx)
                UIMapVS2017.ExportToXLSXFromPreview();
            else
                UIMapVS2017.ExportToXLSFromPreview();

            if (!UISaveAsWindow.Exists)
            {
                throw new Exception("Save as Execel window do not exist");
            }

            uIFilenameComboBox.EditableItem = fileName;

            // Click '&Save' button
            try
            {
                Mouse.Click(uISaveButton);
            }
            catch (Exception)
            {
                Keyboard.SendKeys(this.UISaveAsWindow.UISaveWindow, "s", ModifierKeys.Alt);
            }

            try
            {
                // Click 'Close' button
                Mouse.Click(uICloseButton);
            }
            catch (Exception)
            {
                SupportFunctions.KillExcelProcess(TestContext);
            }

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
        /// ExportToExcel - Use 'ExportToExcelParams' to pass parameters into this method.
        /// </summary>
        public void ExportToExcel(string fileName, bool checkCompatibility = false)
        {
            #region Variable Declarations
            WinButton uIFileTabButton = this.UITmp934FtmpExcelWindow.UIItemWindow.UIRibbonPropertyPage.UIFileTabButton;
            WinTabPage uISaveAsTabPage = this.UITmp934FtmpExcelWindow.UIFileMenuBar.UISaveAsTabPage;
            WinButton uIBrowseButton = this.UITmp934FtmpExcelWindow.UIItemGroup.UIBrowseButton;
            WinComboBox uIFilenameComboBox = this.UISaveAsWindow.UIDetailsPanePane.UIFilenameComboBox;
            WinButton uISaveButton = this.UISaveAsWindow.UISaveWindow.UISaveButton;
            WinButton uICloseButton = this.UITmp934FtmpExcelWindow.UIItemWindow.UIRibbonPropertyPage.UICloseButton;
            #endregion
                       
            try
            {
                Mouse.Click(uIFileTabButton);
            }
            catch (Exception)
            {
                if (!SupportFunctions.CheckProcessRunning("Excel"))
                {
                    throw new Exception("Execel process do not exist");
                }
                else
                    Mouse.Click(uIFileTabButton);
            }
            
            // Click 'Save As' tab alt + a
            Mouse.Click(uISaveAsTabPage, new Point(43, 19));

            // Click 'Browse' button alt + b
            Mouse.Click(uIBrowseButton, new Point(83, 24));

            uIFilenameComboBox.EditableItem = fileName;

            // Click '&Save' button
            try
            {
                Mouse.Click(uISaveButton);
            }
            catch (Exception)
            {
                Keyboard.SendKeys(this.UISaveAsWindow.UISaveWindow, "s", ModifierKeys.Alt);
            }

            if (checkCompatibility)
            {
                try
                {
                    UIMapVS2017.CheckCompatibilityWindowWhenExportToExcel();
                }
                catch (Exception)
                {
                    TestContext.WriteLine("Error in CheckCompatibilityWindowWhenExportToExcel");
                }
            } 

            try
            {
                // Click 'Close' button alt + c
                Mouse.Click(uICloseButton);
            }
            catch (Exception)
            {
                SupportFunctions.KillExcelProcess(TestContext);
            }
        }
        
        #region Departments
        public string DepArbeidsplanklinikken
        {
            get { return DepartmentNames.GetArbeidsplanklinikken; }
        }
        public string DepGodkjenning
        {
            get { return DepartmentNames.GetGodkjenning; }
        }
        public string DepByttAnsatt
        {
            get { return DepartmentNames.GetByttAnsatt; }
        }
        public string DepFilterVisning
        {
            get { return DepartmentNames.GetFilterVisning; }
        }        
        public string DepLønnsberegninger
        {
            get { return DepartmentNames.GetLønnsberegninger; }
        }
        public string DepIverksetting
        {
            get { return DepartmentNames.GetIverksetting; }
        }        
        public string DepKopiering
        {
            get { return DepartmentNames.GetKopiering; }
        }
        public string DepHelgeavtale
        {
            get { return DepartmentNames.GetHelgeavtale; }
        }
        public string DepArbeidsplanOghjelpeplan
        {
            get { return DepartmentNames.GetArbeidsplanOghjelpeplan; }
        }
        public string DepMedisinskAvdeling
        {
            get { return DepartmentNames.GetMedisinskAvdeling; }
        }
        public string DepOrtopedisk
        {
            get { return DepartmentNames.GetOrtopedisk; }
        }
        public string DepStatistikkavd
        {
            get { return DepartmentNames.GetStatistikkavd; }
        }
        public string DepFleksavdeling2
        {
            get { return DepartmentNames.GetFleksavdeling2; }
        }
        public string DepOperasjon
        {
            get { return DepartmentNames.GetOperasjon; }
        }
        public string DepDelteOppgaver1
        {
            get { return DepartmentNames.GetDelteOppgaver1; }
        }
        public string DepKirurgi
        {
            get { return DepartmentNames.GetKirurgi; }
        }        
        public string DepDiverse
        {
            get { return DepartmentNames.GetDiverse; }
        }
        public string DepFrikoder
        {
            get { return DepartmentNames.GetFrikoder; }
        }
        public string DepMasterplan
        {
            get { return DepartmentNames.GetMasterplan; }
        }                
        public string DepYtelse
        {
            get { return DepartmentNames.GetYtelse; }
        }
        public string DepYtelse_2
        {
            get { return DepartmentNames.GetYtelse_2; }
        }
        public string DepAnestesi
        {
            get { return DepartmentNames.GetAnestesi; }
        }
        public string DepOppgavetildeling
        {
            get { return DepartmentNames.GetOppgavetildeling; }
        }
        public string DepOmregnetTid
        {
            get { return DepartmentNames.GetOmregnetTid; }
        }
        public string DepFasteTillegg
        {
            get { return DepartmentNames.GetFTT; }
        }
        public string DepDrommeAvdelingen
        {
            get { return DepartmentNames.GetDrommeAvdelingen; }
        }        
        public string DepAML_1
        {
            get { return DepartmentNames.GetAML_1; }
        }
        public string DepAML_2
        {
            get { return DepartmentNames.GetAML_2; }
        }
        public string DepLønnsberegninger2100
        {
            get { return DepartmentNames.GetLønnsberegninger2100; }
        }        
        public string DepATL_1
        {
            get { return DepartmentNames.GetATL_1; }
        }

        #endregion

        public void AssertResults(List<String> errorList)
        {
            Assert.Fail(SupportFunctions.AssertResults(errorList));
        }

        private void KillGatProcess() 
        {
            Playback.Wait(3000);
            SupportFunctions.KillGatProcess(TestContext);

            Playback.Wait(3000);
        }
        
        /// <summary>
        /// ShortCutNewEmployeeFromEmployeeTab - Use 'ShortCutNewEmployeeFromEmployeeTabParams' to pass parameters into this method.
        /// </summary>
        public void ShortCutNewEmployeeFromEmployeeTab()
        {
            #region Variable Declarations
            WinTitleBar uIGatTitleBar = this.UIGatWindow.UIGatTitleBar;
            //WinClient uIDokClient = this.UIGatver66043783ASCLAvWindow.UIItemWindow.UIDokClient;
            #endregion

            // Click 'Gat' title bar
            Playback.Wait(1000);
            Mouse.Click(uIGatTitleBar);//, new Point(48, 15));

            // Type 'Alt + y' in 'Dok&.' client
            Keyboard.SendKeys(this.ShortCutNewEmployeeFromEmployeeTabParams.UIDokClientSendKeys, ModifierKeys.Alt);
        }

        #region IIS Configuration

        /// <summary>
        /// AddApplicationPoolIIS - Use 'AddApplicationPoolIISParams' to pass parameters into this method.
        /// </summary>
        public void AddApplicationPoolIIS(string poolName, string currentFrameWorkVersion)
        {
            #region Variable Declarations
            WinTreeItem treeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem.UIApplicationPoolsTreeItem;
            WinMenuItem uIAddApplicationPoolMenuItem = this.UIItemWindow.UIDropDownMenu.UIAddApplicationPoolMenuItem;
            WinEdit uI_applicationPoolNameEdit = this.UIAddApplicationPoolWindow.UI_applicationPoolNameWindow.UI_applicationPoolNameEdit;
            WinComboBox uINETCLRversionComboBox = this.UIAddApplicationPoolWindow.UI_applicationPoolVersWindow.UINETCLRversionComboBox;
            WinButton uIOKButton = this.UIAddApplicationPoolWindow.UIOKWindow.UIOKButton;
            //treeItem.SearchProperties.Remove(WinTreeItem.PropertyNames.Name);
            //treeItem.SearchProperties.Add(new PropertyExpression(WinTreeItem.PropertyNames.Name, poolName, PropertyExpressionOperator.Contains));
            #endregion

            Mouse.Click(treeItem, MouseButtons.Right, ModifierKeys.None, new Point(16, 8));

            //// Click 'AT-MAN (GATSOFT\geig)' -> 'Application Pools' tree item
            //Mouse.Click(uIApplicationPoolsTreeItem, new Point(52, 10));

            //// Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Application Pools' tree item
            //Mouse.Click(uIApplicationPoolsTreeItem1, MouseButtons.Right, ModifierKeys.None, new Point(52, 10));

            // Click 'Add Application Pool...' menu item
            try
            {
                Mouse.Click(uIAddApplicationPoolMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys(UIItemWindow, "{DOWN}{ENTER}");
            }

            // Type 'MinGat' in '_applicationPoolNameTextBox' text box
            uI_applicationPoolNameEdit.Text = poolName;

            // Select '.NET CLR Version v4.0.30319' in '.NET CLR version:' combo box

            //Mouse.Click(uINETCLRversionComboBox);
            //Keyboard.SendKeys(uINETCLRversionComboBox, "{ENTER}");
            try
            {
                uINETCLRversionComboBox.SelectedItem = currentFrameWorkVersion;
            }
            catch
            {
                uINETCLRversionComboBox.SelectedItem = this.AddApplicationPoolIISParams.UINETCLRversionComboBoxSelectedItem; // this.AddApplicationPoolMinGatParams.UINETWin7;
            }

            // Click 'OK' button
            Mouse.Click(uIOKButton);
        }

        /// <summary>
        /// ConvertToAppInIIS - Use 'ConvertToAppInIISParams' to pass parameters into this method.
        /// </summary>
        public void ConvertToAppInIIS(string poolName)
        {
            #region Variable Declarations
            WinTreeItem treeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIGatWs1TreeItem;
            WinMenuItem uIConverttoApplicationMenuItem = this.UIItemWindow.UIDropDownMenu.UIConverttoApplicationMenuItem;
            WinButton uISelectButton = this.UIAddApplicationWindow.UISelectWindow.UISelectButton;
            WinComboBox uIApplicationpoolComboBox = this.UISelectApplicationPooWindow.UI_applicationPoolNameWindow.UIApplicationpoolComboBox;
            WinButton uIOKButton = this.UISelectApplicationPooWindow.UIOKWindow.UIOKButton;
            WinButton uIOKButton1 = this.UIAddApplicationWindow.UIOKWindow.UIOKButton;
            #endregion

            treeItem.SearchProperties.Remove(WinTreeItem.PropertyNames.Name);
            treeItem.SearchProperties.Add(new PropertyExpression(WinTreeItem.PropertyNames.Name, poolName, PropertyExpressionOperator.Contains));

            // Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'MinGat642' tree item
            Mouse.Click(treeItem, MouseButtons.Right, ModifierKeys.None, new Point(20, 9));

            // Click 'Convert to Application' menu item
            try
            {
                Mouse.Click(uIConverttoApplicationMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys(UIItemWindow, "{DOWN 3}{ENTER}");
            }

            // Click 'S&elect...' button
            Mouse.Click(uISelectButton, new Point(49, 13));

            uIApplicationpoolComboBox.SelectedItem = poolName; // this.AddMInGatParams.UIApplicationpoolComboBoxSelectedItem;

            // Click 'OK' button
            Mouse.Click(uIOKButton);

            // Click 'OK' button
            Mouse.Click(uIOKButton1);
        }

        /// <summary>
        /// CleanUpWsIIS
        /// </summary>
        public void CleanUpWsIIS()
        {
            #region Variable Declarations
            WinTreeItem uIGatWs1TreeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem.UISitesTreeItem.UIDefaultWebSiteTreeItem.UIGatWs1TreeItem;
            WinMenuItem uIRemoveMenuItem = this.UIItemWindow.UIDropDownMenu.UIRemoveMenuItem;
            WinButton uIYesButton = this.UIConfirmRemoveWindow.UIYesWindow.UIYesButton;
            #endregion

            // Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'GatWs1' tree item
            Mouse.Click(uIGatWs1TreeItem, MouseButtons.Right, ModifierKeys.None, new Point(11, 8));

            try
            {
                Mouse.Click(uIRemoveMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{DOWN 7}{ENTER}");
            }

            // Click '&Yes' button
            Mouse.Click(uIYesButton);


            //// Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'GatWs1' tree item
            //Mouse.Click(uIGatWs1TreeItem, new Point(14, 8));

            //// Right-Click 'AT-MAN (GATSOFT\geig)' -> 'Sites' -> 'Default Web Site' -> 'GatWs1' tree item
            //Mouse.Click(uIGatWs1TreeItem1, MouseButtons.Right, ModifierKeys.None, new Point(14, 8));

            // Click 'Remove' menu item


            //// Click '&Yes' button
            //Mouse.Click(uIYesButton, new Point(28, 11));


        }

        /// <summary>
        /// RemoveApplicationPoolsWs
        /// </summary>
        public void RemoveApplicationPoolsWs()
        {
            #region Variable Declarations
            WinTreeItem uIApplicationPoolsTreeItem = this.UIInternetInformationSWindow.UI_treeViewWindow.UIATMANGATSOFTgeigTreeItem.UIApplicationPoolsTreeItem;
            WinListItem uIGatWs1ListItem = this.UIInternetInformationSWindow.UI_listViewWindow.UI_listViewList.UIGatWs1ListItem;
            WinMenuItem uIRemoveMenuItem = this.UIItemWindow.UIDropDownMenu.UIRemoveMenuItem;
            WinButton uIYesButton = this.UIConfirmRemoveWindow.UIYesWindow.UIYesButton;
            #endregion

            // Click 'AT-MAN (GATSOFT\geig)' -> 'Application Pools' tree item
            Mouse.Click(uIApplicationPoolsTreeItem, new Point(-10, 8));

            // Right-Click 'GatWs1' list item
            Mouse.Click(uIGatWs1ListItem, MouseButtons.Right, ModifierKeys.None, new Point(45, 8));

            try
            {
                Mouse.Click(uIRemoveMenuItem);
            }
            catch (Exception)
            {
                Keyboard.SendKeys("{DOWN 10}{ENTER}");
            }

            // Click '&Yes' button
            Mouse.Click(uIYesButton, new Point(40, 12));


            //// Click 'AT-MAN (GATSOFT\geig)' -> 'Application Pools' tree item
            //Mouse.Click(uIApplicationPoolsTreeItem, new Point(34, 14));

            //// Select 'GatWs2' in '_listView' list box
            //uI_listViewList.SelectedItemsAsString = this.CleanUpWsIISParams.UI_listViewListSelectedItemsAsString;

            //// Right-Click 'GatWs2' list item
            //Mouse.Click(uIGatWs2ListItem, MouseButtons.Right, ModifierKeys.None, new Point(91, 8));

            //// Click 'Remove' menu item
        }

        public void CloseIIS()
        {
            try
            {
                UIMapVS2017.CloseIIS();
            }
            catch (Exception)
            {
                SupportFunctions.KillProcessByName("InetMgr", TestContext);
            }
        }

        #endregion

        private LoginGatAndSelectDepartmentParams LoginGatAndSelectDepartmentParams
        {
            get
            {
                if ((this.mLoginGatAndSelectDepartmentParams == null))
                {
                    this.mLoginGatAndSelectDepartmentParams = new LoginGatAndSelectDepartmentParams();
                }
                return this.mLoginGatAndSelectDepartmentParams;
            }
        }

        private LoginGatAndSelectDepartmentParams mLoginGatAndSelectDepartmentParams;

        public virtual SearchAdministrationParams SearchAdministrationParams
        {
            get
            {
                if ((this.mSearchAdministrationParams == null))
                {
                    this.mSearchAdministrationParams = new SearchAdministrationParams();
                }
                return this.mSearchAdministrationParams;
            }
        }

        private SearchAdministrationParams mSearchAdministrationParams;


        public virtual OpenAdministrationSearchWindowParams OpenAdministrationSearchWindowParams
        {
            get
            {
                if ((this.mOpenAdministrationSearchWindowParams == null))
                {
                    this.mOpenAdministrationSearchWindowParams = new OpenAdministrationSearchWindowParams();
                }
                return this.mOpenAdministrationSearchWindowParams;
            }
        }

        private OpenAdministrationSearchWindowParams mOpenAdministrationSearchWindowParams;

        public virtual GoToShiftbookDateParams GoToShiftbookDateParams
        {
            get
            {
                if ((this.mGoToShiftbookDateParams == null))
                {
                    this.mGoToShiftbookDateParams = new GoToShiftbookDateParams();
                }
                return this.mGoToShiftbookDateParams;
            }
        }

        private GoToShiftbookDateParams mGoToShiftbookDateParams;


        public virtual SelectReportParams SelectReportParams
        {
            get
            {
                if ((this.mSelectReportParams == null))
                {
                    this.mSelectReportParams = new SelectReportParams();
                }
                return this.mSelectReportParams;
            }
        }

        private SelectReportParams mSelectReportParams;

        public virtual AddApplicationPoolIISParams AddApplicationPoolIISParams
        {
            get
            {
                if ((this.mAddApplicationPoolIISParams == null))
                {
                    this.mAddApplicationPoolIISParams = new AddApplicationPoolIISParams();
                }
                return this.mAddApplicationPoolIISParams;
            }
        }

        private AddApplicationPoolIISParams mAddApplicationPoolIISParams;

        public virtual SetExtraDateParams SetExtraDateParams
        {
            get
            {
                if ((this.mSetExtraDateParams == null))
                {
                    this.mSetExtraDateParams = new SetExtraDateParams();
                }
                return this.mSetExtraDateParams;
            }
        }

        private SetExtraDateParams mSetExtraDateParams;


        public virtual SelectAbsenceCodeParams SelectAbsenceCodeParams
        {
            get
            {
                if ((this.mSelectAbsenceCodeParams == null))
                {
                    this.mSelectAbsenceCodeParams = new SelectAbsenceCodeParams();
                }
                return this.mSelectAbsenceCodeParams;
            }
        }

        private SelectAbsenceCodeParams mSelectAbsenceCodeParams;


        public virtual ShortCutNewEmployeeFromEmployeeTabParams ShortCutNewEmployeeFromEmployeeTabParams
        {
            get
            {
                if ((this.mShortCutNewEmployeeFromEmployeeTabParams == null))
                {
                    this.mShortCutNewEmployeeFromEmployeeTabParams = new ShortCutNewEmployeeFromEmployeeTabParams();
                }
                return this.mShortCutNewEmployeeFromEmployeeTabParams;
            }
        }

        private ShortCutNewEmployeeFromEmployeeTabParams mShortCutNewEmployeeFromEmployeeTabParams;


        public virtual SetLayerRotateToDateParams SetLayerRotateToDateParams
        {
            get
            {
                if ((this.mSetLayerRotateToDateParams == null))
                {
                    this.mSetLayerRotateToDateParams = new SetLayerRotateToDateParams();
                }
                return this.mSetLayerRotateToDateParams;
            }
        }

        private SetLayerRotateToDateParams mSetLayerRotateToDateParams;
    }
    /// <summary>
    /// Parameters to be passed into 'LoginGatAndSelectDepartment'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class LoginGatAndSelectDepartmentParams
    {

        #region Fields
        /// <summary>
        /// Type 'ASCL' in text box
        /// </summary>
        public string UIItemEditText = "ASCL";

        /// <summary>
        /// Type '{Tab}' in text box
        /// </summary>
        public string UIItemEditSendKeys = "{Tab}";

        /// <summary>
        /// Type '********' in text box
        /// </summary>
        public string UIItemEditSendKeys1 = "VLLA+bJzNf882FWpmiwJPY0v6P7+sGGK";

        /// <summary>
        /// Type '5010' in 'txtFilter' text box
        /// </summary>
        public string UITxtFilterEditValueAsString = "5010";
        #endregion
    }

    /// <summary>
    /// Parameters to be passed into 'SearchAdministration'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SearchAdministrationParams
    {

        #region Fields

        /// <summary>
        /// Type '{Enter}' in text box
        /// </summary>
        public string UISendKeysEnter = "{Enter}";

        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'OpenAdministrationSearchWindow'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class OpenAdministrationSearchWindowParams
    {

        #region Fields
        /// <summary>
        /// Type '{F3}' in 'Administrasjon' client
        /// </summary>
        public string UIAdministrasjonClientSendKeys = "{F3}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'GoToShiftbookDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class GoToShiftbookDateParams
    {

        #region Fields
        /// <summary>
        /// Type '25.10.2016' in 'pceDate' PopupEdit
        /// </summary>
        public string UIPceDatePopupEditValueAsString = "25.10.2016";

        /// <summary>
        /// Type '{Tab}' in 'pceDate' PopupEdit
        /// </summary>
        public string UIPceDatePopupEditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectReport'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class SelectReportParams
    {

        #region Fields
        /// <summary>
        /// Type '{F3}' in 'Panel1' client
        /// </summary>
        public string UIF3 = "{F3}";

        /// <summary>
        /// Type '{Enter}' in text box
        /// </summary>
        public string UIEnter = "{Enter}";

        /// <summary>
        /// Type '{Tab}' in text box
        /// </summary>
        public string UITab = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'AddApplicationPoolIIS'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "12.0.31101.0")]
    public class AddApplicationPoolIISParams
    {

        #region Fields
        /// <summary>
        /// Type 'AppPoolWs1' in '_applicationPoolNameTextBox' text box
        /// </summary>
        public string UI_applicationPoolNameEditText = "AppPoolWs1";

        /// <summary>
        /// Select '.NET CLR Version v4.0.30319' in '.NET CLR version:' combo box
        /// </summary>
        public string UINETCLRversionComboBoxSelectedItem = ".NET CLR Version v4.0.30319";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetExtraDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class SetExtraDateParams
    {

        #region Fields
        /// <summary>
        /// Type '{Tab}' in 'pceDate' DateTimeEdit
        /// </summary>
        public string UIPceDateDateTimeEditSendKeys = "{Tab}";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SelectAbsenceCode'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class SelectAbsenceCodeParams
    {

        #region Fields
        /// <summary>
        /// Type '' in 'ddlAbsCode' LookUpEdit
        /// </summary>
        public string UIDdlAbsCodeLookUpEditValueAsString = "";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'ShortCutNewEmployeeFromEmployeeTab'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class ShortCutNewEmployeeFromEmployeeTabParams
    {

        #region Fields
        /// <summary>
        /// Type 'Alt + y' in 'Dok&.' client
        /// </summary>
        public string UIDokClientSendKeys = "y";
        #endregion
    }
    /// <summary>
    /// Parameters to be passed into 'SetLayerRotateToDate'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class SetLayerRotateToDateParams
    {

        #region Fields
        /// <summary>
        /// Type '1' in 'gsRotation' RadioGroup
        /// </summary>
        public int UIGsRotationRadioGroupSelectedIndex = 1;

        /// <summary>
        /// Type '{NumPad2}{Tab}' in 'pceDate' DateTimeEdit
        /// </summary>
        public string UIPceDateDateTimeEditSendKeys = "{Tab}";
        #endregion
    }
}
