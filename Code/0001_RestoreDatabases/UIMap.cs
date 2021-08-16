namespace _0001_RestoreDatabases
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
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        #endregion

        public UIMap(TestContext testContest)
        {
            TestContext = testContest;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nb-NO");
            UICommon = new CommonUIFunctions.UIMap(testContest);
        }
        public bool RestoreGeneralDatabase()
        {
            return UICommon.RestoreDatabase(false, true);
        }
        public bool RestorePerformanceDatabases()
        {
            return UICommon.RestoreDatabase(true, true);
        }
        public bool RestoreBRDatabase()
        {
            return UICommon.RestoreDatabase(false, true, true);
        }
    }
}
