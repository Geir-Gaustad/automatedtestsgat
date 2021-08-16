namespace _025_Ytelsestest_Arbeidsplan.UIMapVS2017Classes
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


    public partial class UIMapVS2017
    {
        public void SelectAndCoverShift()
        {
            #region Variable Declarations
            DXCell uIVAKANTCell = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UICenterPanelDayDockPanel.UIControlContainerCustom.UICenterPanelDayWrappeDockPanel.UIHjemmevakt0016777077DockPanel.UIControlContainerCustom.UIGcDayColumnTable.UIVAKANTCell;
            DXRibbonButtonItem uIOppdekkingRibbonBaseButtonItem = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpVacancyRibbonPageGroup.UIOppdekkingRibbonBaseButtonItem;
            DXRibbonButtonItem uIBestillRibbonBaseButtonItem = this.UIOppdekkingssenterASCWindow.UIRcOrderRibbon.UIRpOrderRibbonPage.UIRpgOrderActionRibbonPageGroup.UIBestillRibbonBaseButtonItem;
            DXButton uIBestillButton = this.UISendbestillingWindow.UIGsLayoutControl1Custom.UIRootLayoutGroup.UILciSubmitOrderLayoutControlItem.UIBestillButton;
            DXRibbonButtonItem uILukkRibbonBaseButtonItem = this.UIOppdekkingssenterASCWindow.UIRcOrderRibbon.UIRpOrderRibbonPage.UIRpgWindowRibbonPageGroup.UILukkRibbonBaseButtonItem;
            DXRibbonButtonItem uIFormidlingRibbonBaseButtonItem = this.UIGatWindow.UIViewHostCustom.UIPcViewClient.UIRibbonControlRibbon.UIRpMainMenuRibbonPage.UIGrpVacancyRibbonPageGroup.UIFormidlingRibbonBaseButtonItem;
            DXRibbonEditItem uIBeiFromDateRibbonEditItem = UIFormidlingssenterWindow.UIRcMenuRibbon.UIRpHomeRibbonPage.UIRpgPeriodRibbonPageGroup.UIBeiFromDateRibbonEditItem;
            DXPopupEdit uIGSSmartDateEditEditoPopupEdit = UIFormidlingssenterWindow.UIRcMenuRibbon.UIGSSmartDateEditEditoPopupEdit;
            DXCell uIItem02012016Cell = UIFormidlingssenterWindow.UIScccContentCustom.UIGcShiftsTable.UIItem02012016Cell;
            DXRibbonButtonItem uITabestillingRibbonBaseButtonItem = UIFormidlingssenterWindow.UIRcMenuRibbon.UIRpHomeRibbonPage.UIRpgActionsRibbonPageGroup.UITabestillingRibbonBaseButtonItem;
            #endregion

            // Click 'VAKANT' cell
            Mouse.Click(uIVAKANTCell);

            // Click 'Oppdekking' RibbonBaseButtonItem
            Mouse.Click(uIOppdekkingRibbonBaseButtonItem);

            // Click 'Bestill...' RibbonBaseButtonItem
            Mouse.Click(uIBestillRibbonBaseButtonItem);

            // Click '&Bestill' button
            Mouse.Click(uIBestillButton);

            // Click 'Lukk' RibbonBaseButtonItem
            Mouse.Click(uILukkRibbonBaseButtonItem);

            // Click 'Formidling' RibbonBaseButtonItem
            Mouse.Click(uIFormidlingRibbonBaseButtonItem);

            // Click 'beiFromDate' RibbonEditItem
            Mouse.Click(uIBeiFromDateRibbonEditItem);

            Keyboard.SendKeys(uIGSSmartDateEditEditoPopupEdit, "01.01.2016" + "{Tab}");
            Playback.Wait(1000);

            // Click '02.01.2016' cell
            Mouse.Click(uIItem02012016Cell);

            // Click 'Ta bestilling' RibbonBaseButtonItem
            Mouse.Click(uITabestillingRibbonBaseButtonItem);
        }
      
        public void CoverShiftWithExtra()
        {
            #region Variable Declarations
            DXButton uINesteButton = this.UIVelgansattsomskaldekWindow.UINesteButton;
            DXButton uIFullførButton = this.UIVelgansattsomskaldekWindow.UIFullførButton;
            DXButton uIAvbrytButton = this.UIMerarbeidovertidWindow.UIGsPanelControl1Client.UIAvbrytButton;
            DXRibbonButtonItem uIDekkoppRibbonBaseButtonItem = this.UIFormidlingssenterWindow.UIRcMenuRibbon.UIRpHomeRibbonPage.UIRpgCoverRibbonPageGroup.UIDekkoppRibbonBaseButtonItem;
            #endregion

            // Click 'Dekk opp...' RibbonBaseButtonItem
            Mouse.Click(uIDekkoppRibbonBaseButtonItem);

            // Click '&Neste >' button
            Mouse.Click(uINesteButton);

            // Click '&Neste >' button
            Mouse.Click(uINesteButton);

            // Click '&Fullfør' button
            Mouse.Click(uIFullførButton);

            // Click '&Avbryt' button
            Mouse.Click(uIAvbrytButton);
        }
    }
}
