namespace _055_Test_AML.UIMapVS2017Classes
{
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
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
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using System.Threading;

    public partial class UIMapVS2017
    {
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        private const string AccessCode = "1234";

        public UIMapVS2017(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nb-NO");
            UICommon = new CommonUIFunctions.UIMap(TestContext);
        }

        public void ChangeAmlAgreementPriority(string priority)
        {
            #region Variable Declarations
            DXTextEdit uISdePriorityEdit = this.UIAMLavtaleWindow.UISdePriorityEdit;
            #endregion

            uISdePriorityEdit.ValueAsString = priority;
            Playback.Wait(500);
        }

        /// <summary>
        /// InsertAmlAccesscode - Use 'InsertAmlAccesscodeParams' to pass parameters into this method.
        /// </summary>
        public void InsertAmlAccesscode()
        {
            #region Variable Declarations
            WinClient uIItemClient = this.UIGTWStilgangskontrollWindow.UIGTWStilgangskontrollClient.UIItemClient;
            WinComboBox uIItemComboBox = this.UIGTWStilgangskontrollWindow.UIItemWindow.UIItemComboBox;
            WinEdit uIItemEdit = this.UIGTWStilgangskontrollWindow.UILocalRolesRosterWebWindow.UIItemEdit;
            WinButton uIOKButton = this.UIGTWStilgangskontrollWindow.UIItemClient.UIOKButton;
            WinClient uIItemClient1 = this.UIGTWStilgangskontrollWindow.UIItemClient1.UIItemClient;
            #endregion

            // Click client
            Mouse.Click(uIItemClient, new Point(31, 30));

            // Select 'CalculateWea' in combo box
            uIItemComboBox.SelectedItem = "CalculateWea";

            // Type '{Tab}' in 'CalculateWea' item
            Keyboard.SendKeys(uIItemComboBox, "{Tab}");

            // Type '1234' in text box
            uIItemEdit.Text = AccessCode;

            // Type '{Tab}' in text box
            Keyboard.SendKeys(uIItemEdit, "{Tab}");

            // Click 'OK' button
            Mouse.Click(uIOKButton, new Point(38, 17));

            // Click client
            Mouse.Click(uIItemClient1, new Point(347, 22));
        }

        /// <summary>
        /// InsertAccessCodeWeaBreakService - Use 'InsertAccessCodeWeaBreakServiceParams' to pass parameters into this method.
        /// </summary>
        public void InsertAccessCodeWeaBreakService()
        {
            #region Variable Declarations
            HtmlEdit uIAccessCodeEdit = this.UIWeaBreakServiceWebSeWindow.UIWeaBreakServiceWebSeDocument.UIAccessCodeEdit;
            #endregion

            // Type '1234' in 'accessCode' text box
            uIAccessCodeEdit.Text = AccessCode;

            // Type '{Tab}' in 'accessCode' text box
            Keyboard.SendKeys(uIAccessCodeEdit, "{Tab}");
        }
    }
}
