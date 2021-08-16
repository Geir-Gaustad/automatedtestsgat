namespace _022_Ytelsestest_Kalenderplan.UIMapVS2017Classes
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
        public bool ChesterApproved()
        {
            #region Variable Declarations
            DXCell uIGodkjennCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable.UIGodkjennCell;
            DXCell uIGodkjennCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcSubtabsTabList.UIViewTabPageClient.UIApprovalViewCustom.UILcMainCustom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem2LayoutControlItem.UIGcApprovalTable.UIGodkjennCell1;
            #endregion

            var cell1OK = uIGodkjennCell.WaitForControlCondition(IsStatusApproved);
            var cell2OK = uIGodkjennCell1.WaitForControlCondition(IsStatusApproved);

            return cell1OK && cell2OK;
        }

        private static bool IsStatusApproved(UITestControl control)
        {
            DXCell uIItemCell = control as DXCell;
            return uIItemCell.ValueAsString == "1";
        }
    }
}
