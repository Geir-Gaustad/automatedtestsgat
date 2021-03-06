namespace _024_Test_FTT.UIMapVS2017Classes
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
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;

    public partial class UIMapVS2017
    {

        /// <summary>
        /// CheckEditShiftClassesButtonExists - Use 'CheckEditShiftClassesButtonExistsExpectedValues' to pass parameters into this method.
        /// </summary>
        public bool CheckButtonShiftClassesExists()
        {
            #region Variable Declarations
            DXButton uIVaktklasserButton = UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIFixedPaymentViewCustom.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIVaktklasserButton;
            #endregion

            try
            {
                if (uIVaktklasserButton.Exists && uIVaktklasserButton.Visible)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}
