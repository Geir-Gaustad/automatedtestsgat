using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace _0001_RestoreDatabases
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class RestoreDatabases
    {
        [TestMethod]
        public void RestoreGeneralDatabase()
        {
            if (!UIMap.RestoreGeneralDatabase())
                Assert.Fail("Error restoring GeneralDatabase");
        }

        [TestMethod]
        public void RestorePerformanceDatabases()
        {
            if(!UIMap.RestorePerformanceDatabases())
                Assert.Fail("Error restoring PerformanceDatabases");
        }

        [TestMethod]
        public void RestoreBRDatabase()
        {
            if (!UIMap.RestoreBRDatabase())
                Assert.Fail("Error restoring BRDatabase");
        }

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