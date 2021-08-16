namespace _093_Test_Helgeavtale_Spekter.UIMapVS2015Classes
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
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

    public partial class UIMapVS2015
    {
        public void EditKontForPreSelectedEmployee()
        {

            #region Variable Declarations
            WinClient editCont = this.UIGatver66043393ASCLAvWindow1.UIItemWindow2.UITimelisteClient;
            #endregion

            // Click 'Timeliste' client
            Mouse.Click(editCont, new Point(43, 13));
        }
    }
}
