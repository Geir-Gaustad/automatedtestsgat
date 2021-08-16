using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.Diagnostics;

namespace _1414_Test_Minnebruk_Fraværsvindu
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_1414_Minnebruk_Fraværsvindu
    {
        
      
        [TestMethod, Timeout(6000000)]
        public void SystemTest_1414()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            UIMap.StartGat(true);
            var physicalMemoryStart = Convert.ToInt32(UIMap.ReadPhysicalMemoryUsage(true));
            var pagedMemoryStart = Convert.ToInt32(UIMap.ReadPagedMemorySize64(true));
            
            TestContext.WriteLine("Minnebruk etter oppstart av Gat: " + physicalMemoryStart.ToString() + " MB");
            TestContext.WriteLine("PagedMemorySize etter oppstart av Gat: " + pagedMemoryStart.ToString() + " MB");
      
            UIMap.OpenCloseAbsenceWindow(300, 30);

            var physicalMemoryEnd = Convert.ToInt32(UIMap.ReadPhysicalMemoryUsage(true));
            var pagedMemoryEnd = Convert.ToInt32(UIMap.ReadPagedMemorySize64(true));

            var physicalMemoryDev = physicalMemoryEnd - physicalMemoryStart;
            var pagedMemoryDev = pagedMemoryEnd - pagedMemoryStart;

            TestContext.WriteLine("Økning i minnebruk = " + physicalMemoryDev.ToString() + " MB");
            TestContext.WriteLine("Økning i PagedMemorySize = " + pagedMemoryDev.ToString() + " MB");

            if (physicalMemoryEnd > 800)
                errorList.Add("Use of memory  is out of limit: " + physicalMemoryEnd);

            if (pagedMemoryEnd > 800)
                errorList.Add("Use of Pagedmemory is out of limit: " + pagedMemoryEnd);

            if (physicalMemoryDev > 400)
                errorList.Add("Increase of Memory is out of limit: " + physicalMemoryDev);

            if (pagedMemoryDev > 400)
                errorList.Add("Increase of Pagedmemory is out of limit: " + pagedMemoryDev);


            UIMap.CloseGat();

            if (errorList.Count != 0)
                UIMap.AssertResults(errorList);
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap(TestContext);
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
