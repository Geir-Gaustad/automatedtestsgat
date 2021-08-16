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

namespace _020_Test_Arbeidsplan_Godkjenning
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class Test_020_Test_Arbeidsplan_Godkjenning
    {

        
       
        [TestMethod, Timeout(6000000)]
        public void SystemTest_020_Godjenning()
        {
            var errorList = new List<string>();
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;
            Playback.PlaybackSettings.DelayBetweenActions = UIMap.DelayBetweenActions;;

            UIMap.RestoreDatabase();
            //Step 1
            UIMap.StartGat(true, true, "GATSOFT", true);

            //Step 2
            UIMap.SelectAdministrationAndSetDemands("Godkjenningskrav + arbeidsplan", "2");

            //Step 3
            UIMap.SelectAdministrationAndSetRepresentations("Representasjoner", "3");

            //Step 4
            UIMap.CreateRosterplan("Godkjenning Leder 5000", new DateTime(2024, 01, 01), "4", 4);

            //Step 5
            errorList.AddRange(UIMap.AddEmployeesAndAshiftsToPlan());

            //Step 6
            UIMap.OpenSettingsAndSetReadyForApprival();

            //Step 7
            errorList.AddRange(UIMap.CalcultateFTTStep7());

            //Step 8
            errorList.AddRange(UIMap.ClickTransfereAndCheckTransfereFTTDisabled());

            //Step 9
            errorList.AddRange(UIMap.ClickEffectuateAndCheckEffectuationDisabled());

            //Step 10
            errorList.AddRange(UIMap.ClickCellInApprovalLederColumn());

            //Step 11 - 13
            errorList.AddRange(UIMap.ApproveFjongRejectKaspersen());

            //Step 14
            errorList.AddRange(UIMap.ApproveRow5And6());

            //Step 15
            errorList.AddRange(UIMap.RejectRow7To9());

            //Step 16
            errorList.AddRange(UIMap.RepealRow8And9());

            //Step 17 - 18
            errorList.AddRange(UIMap.LoginAsAsclAndSetDemands());

            //Step 19
            errorList.AddRange(UIMap.SetAsclAndSetRepresentations());

            //Step 20
            errorList.AddRange(UIMap.LoginAsBjarneAndGoToRosterplanTab());

            //Step 21
            UIMap.CreateRosterplan("Februar 2024", new DateTime(2024, 01, 29), "4", 21);

            //Step 22
            errorList.AddRange(UIMap.AddEmployeesTo2024Plan());

            //Step 23
            errorList.AddRange(UIMap.AddNshiftsTo2024Plan());

            //Step 24
            errorList.AddRange(UIMap.LoginAsVigdisAndGoToRosterplanTab());

            //Step 25
            errorList.AddRange(UIMap.LoginAsBjarneAndGoToRosterplanTabToCheckPlanExists());

            //Step 26
            errorList.AddRange(UIMap.Step26());

            //Step 27
            errorList.AddRange(UIMap.ApproveAllLinesStep27());

            //Step 28
            errorList.AddRange(UIMap.LoginAsOyvindAndGoToRosterplanTab());

            //Step 29
            errorList.AddRange(UIMap.LoginAsGunnarAndOpenPlanFebruar2024());

            //Step 30
            errorList.AddRange(UIMap.CheckEmployeeSettingsDisabled());

            //Step 31
            errorList.AddRange(UIMap.CheckEmployeeLineSettingsDisabled());

            //Step 32
            errorList.AddRange(UIMap.LoginAsPetronellaAndGoToRosterplanTab());

            //Step 33
            errorList.AddRange(UIMap.OpenFebruar2024AndSelectApprovalTab());

            //Step 34
            errorList.AddRange(UIMap.ApproveLinesStep34());

            //Step 35-36
            errorList.AddRange(UIMap.LoginAsVigdisAndOpenRosterplan2024());

            //Step 37
            errorList.AddRange(UIMap.LoginAsBjarneAndOpenRosterplan2024());

            //Step 38
            errorList.AddRange(UIMap.AddAshiftsToPlanStep38());

            //Step 39
            errorList.AddRange(UIMap.RosterplanApprovalLogStep39());

            //Step 40-41
            errorList.AddRange(UIMap.CalculateAndTransfereFTTStep40());

            //Step 42-43
            errorList.AddRange(UIMap.Effectuate2024PlanStep42());

            //Step 44
            errorList.AddRange(UIMap.Effectuate2024PlanStep44());

            //Step 45
            errorList.AddRange(UIMap.EditApprovals());

            //Step 46
            errorList.AddRange(UIMap.RemoveAshiftsFromPlanStep46());

            //Step 47
            errorList.AddRange(UIMap.CancelTransfereAndEffectuateStep47());

            //Step 48
            errorList.AddRange(UIMap.CloseEffectuationWindowAndCloseGatStep48());

            //Step 49
            errorList.AddRange(UIMap.LoginAsPetronellaAndOpenRosterplan2024());

            //Step 50
            errorList.AddRange(UIMap.RepealLine3And4Step50());

            //Step 51
            errorList.AddRange(UIMap.ApproveLine3and4AndLoginAsVigdisStep51());

            //Step 52
            errorList.AddRange(UIMap.Step52());

            //Step 53
            errorList.AddRange(UIMap.Step53());

            //Step 54
            errorList.AddRange(UIMap.Step54());

            //Step 55
            errorList.AddRange(UIMap.Step55());

            //Step 56
            errorList.AddRange(UIMap.Step56());

            //Step 57
            errorList.AddRange(UIMap.Step57());

            //Step 58
            errorList.AddRange(UIMap.Step58());

            //Step 59
            errorList.AddRange(UIMap.Step59());

            //Step 60
            errorList.AddRange(UIMap.Step60());

            //Step 61
            errorList.AddRange(UIMap.Step61());

            //Step 62
            errorList.AddRange(UIMap.Step62());


            if (errorList.Count <= 0)
            {
                TestContext.WriteLine("Test 20 Godkjenning finished OK");
                return;
            }

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
