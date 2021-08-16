namespace LaunchGatRun
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
    using System.Threading;
    using System.Globalization;

    public partial class UIMap
    {
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;

        public UIMap(TestContext testContext)
        {
            TestContext = testContext;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");

            UICommon = new CommonUIFunctions.UIMap(testContext);
        }

        public void LaunchGatRun()
        {
            try
            {
                UICommon.LaunchGatRun();
            }
            catch (Exception e)
            {
                Assert.Fail("Error starting GatRun: " + e.Message);
            }
        }
    }
}
