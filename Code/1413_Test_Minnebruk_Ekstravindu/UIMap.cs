namespace _1413_Test_Minnebruk_Ekstravindu
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
    using CommonTestData;
    using System.Diagnostics;

    public partial class UIMap
    {
        #region Fields
        private CommonUIFunctions.UIMap UICommon;
        private TestContext TestContext;
        
        public string FileType = ".xls";
        #endregion

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
        public void StartGat(bool logGatInfo)
        {
            UICommon.LaunchGatturnus(false);
            UICommon.LoginGatAndSelectDepartment("", null, "", logGatInfo);
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
        public string ReadPhysicalMemoryUsage(bool value)
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.WorkingSet64, value);
        }
        public string ReadPagedMemorySize64(bool value)
        {
            return LoadBalanceTesting.ReadMemoryData(LoadBalanceTesting.MemoryType.PagedMemorySize64, value);
        }
        public void OpenCloseExtraWindow(int rotations, int checkInterval)
        {
            int check = 0;
            for (int i = 0; i < rotations; i++)
            {               
                UICommon.UIMapVS2017.ClickExtraRibbonButton();
                Playback.Wait(500);
                UICommon.UIMapVS2017.ClickCancelExtraWindow();
                Playback.Wait(500);

                if(i== check)
                {
                    TestContext.WriteLine("Minnebruk ved " + check + ": " + ReadPhysicalMemoryUsage(false));
                    TestContext.WriteLine("PagedMemorySize " + check + ": " + ReadPagedMemorySize64(false));
                    check = check + checkInterval;
                }               
            }

            TestContext.WriteLine("Minnebruk ved " + rotations + ": " + ReadPhysicalMemoryUsage(false));
            TestContext.WriteLine("PagedMemorySize  " + rotations + ": " + ReadPagedMemorySize64(false));
        }
    }
}
