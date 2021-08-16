namespace _093_Test_Helgeavtale_Spekter.UIMapVS2017Classes
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
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


    public partial class UIMapVS2017
    {


        /// <summary>
        /// InsertPeriodValues - Use 'InsertPeriodValuesParams' to pass parameters into this method.
        /// </summary>
        public void InsertSpekterPeriodValues(string periodName, string from, string to)
        {
            #region Variable Declarations
            WinClient uIItemClient = this.UISPEKTERperioderWindow.UISPEKTERperioderClient.UIItemClient;
            WinEdit uIItemEdit = this.UISPEKTERperioderWindow.UIItemWindow.UIItemEdit;
            WinEdit uIItemEdit1 = this.UISPEKTERperioderWindow.UIItemWindow1.UIItemEdit;
            WinEdit uIItemEdit2 = this.UISPEKTERperioderWindow.UIItemWindow2.UIItemEdit;
            #endregion

            // Click client
            Mouse.Click(uIItemClient, new Point(31, 29));

            //Name
            Keyboard.SendKeys(uIItemEdit, periodName + "{TAB}");

            //Fromdate
            Keyboard.SendKeys(uIItemEdit1, from + "{TAB}");

            //Todate
            Keyboard.SendKeys(uIItemEdit2, to + "{TAB}");
        }

        /// <summary>
        /// SelectSpekterPeriod - Use 'SelectSpekterPeriodParams' to pass parameters into this method.
        /// </summary>
        public void SelectSpekterPeriod(string period)
        {
            #region Variable Declarations
            WinComboBox uIItemComboBox = this.UIGatWindow.UIItemWindow.UIItemComboBox;
            #endregion

            uIItemComboBox.SelectedItem = period;
        }

        /// <summary>
        /// Select_VacantShiftsToExchange
        /// </summary>
        public void Select_VacantShiftsToExchange()
        {
            #region Variable Declarations
            DXButton uIVelgledigevakterButton = this.UIByttemedavdelingWindow.UIGsPanelControl1Client.UIGsTabTabList.UITpDepartmentExchangeClient.UIVelgledigevakterButton;
            //DXCell uIValgtCell = this.UIVacantShiftsFormWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem1LayoutControlItem.UIGUnoccupiedShiftsTable.UIValgtCell;
            //uIValgtCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gUnoccupiedShiftsGridControlCell[View]gvUnoccupieedShifts[Row]0[Column]gcIsSelected";
            //DXCell uIIkkevalgtCell = this.UIVacantShiftsFormWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem1LayoutControlItem.UIGUnoccupiedShiftsTable.UIIkkevalgtCell;
            //uIIkkevalgtCell.SearchProperties[DXTestControl.PropertyNames.Name] = "gUnoccupiedShiftsGridControlCell[View]gvUnoccupieedShifts[Row]1[Column]gcIsSelected";
            //

            // #region Variable Declarations
            DXCheckBox uICheckEditCheckBox = this.UIVacantShiftsFormWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem1LayoutControlItem.UIGUnoccupiedShiftsTable.UICheckEditCheckBox;
            DXCheckBox uICheckEditCheckBox1 = this.UIVacantShiftsFormWindow.UIGsLayoutControl1Custom.UILayoutControlGroup1LayoutGroup.UILayoutControlItem1LayoutControlItem.UIGUnoccupiedShiftsTable.UICheckEditCheckBox1;
            #endregion

            //// Click 'CheckEdit' check box
            //Mouse.Click(uICheckEditCheckBox, new Point(15, 10));

            //// Click 'CheckEdit' check box
            //Mouse.Click(uICheckEditCheckBox1, new Point(15, 7));
            //#endregion

            // Click '&Velg ledige vakter' button
            Mouse.Click(uIVelgledigevakterButton);

            uICheckEditCheckBox.Checked = true;
            uICheckEditCheckBox1.Checked = true;
        }

        public void CheckReportValuesC6Step6()
        {
            #region Variable Declarations
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick1 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick1;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick2 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick2;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick3 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick3;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick5 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick5;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick10 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick10;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick11 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick11;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick12 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick12;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick13 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick13;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick14 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick14;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick15 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick15;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick16 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick16;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick17 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick17;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick18 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick18;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick19 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick19;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick20 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick20;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick21 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick21;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick8 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick8;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick9 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick9;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick22 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick22;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick23 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick23;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick24 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick24;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick25 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick25;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick26 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick26;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick27 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick27;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick28 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick28;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick29 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick29;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick30 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick30;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick31 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick31;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick32 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick32;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick33 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick33;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick7 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick7;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick34 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick34;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick35 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick35;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick36 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick36;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick37 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick37;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick38 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick38;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick39 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick39;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick40 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick40;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick41 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick41;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick42 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick42;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick43 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick43;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick44 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick44;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick45 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick45;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick46 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick46;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick47 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick47;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick48 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick48;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick49 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick49;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick50 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick50;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick51 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick51;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick52 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick52;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick53 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick53;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick54 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick54;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick55 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick55;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick56 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick56;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick57 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick57;
            DXPrintingBrick uIPage0BrickIndices090PrintControlBrick58 = this.UIForhåndsvisningWindow.UIDocumentViewerPrintViewControl.UIPage0BrickIndices090PrintControlBrick58;
            #endregion

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[1]' PrintControlBrick equals 'N, N'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrickText, uIPage0BrickIndices090PrintControlBrick.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[3]' PrintControlBrick equals '52'
            Assert.AreEqual("53", uIPage0BrickIndices090PrintControlBrick1.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[4]' PrintControlBrick equals '1040,0'
            Assert.AreEqual("1058,0", uIPage0BrickIndices090PrintControlBrick2.Text);
            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[5]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick3Text, uIPage0BrickIndices090PrintControlBrick3.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[8]' PrintControlBrick equals '1040,0'
            Assert.AreEqual("1058,0", uIPage0BrickIndices090PrintControlBrick5.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[13]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick10Text, uIPage0BrickIndices090PrintControlBrick10.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[14]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick11Text, uIPage0BrickIndices090PrintControlBrick11.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[15]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick12Text, uIPage0BrickIndices090PrintControlBrick12.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[16]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick13Text, uIPage0BrickIndices090PrintControlBrick13.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[17]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick14Text, uIPage0BrickIndices090PrintControlBrick14.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[18]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick15Text, uIPage0BrickIndices090PrintControlBrick15.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[19]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick16Text, uIPage0BrickIndices090PrintControlBrick16.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[20]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick17Text, uIPage0BrickIndices090PrintControlBrick17.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[21]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick18Text, uIPage0BrickIndices090PrintControlBrick18.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[22]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick19Text, uIPage0BrickIndices090PrintControlBrick19.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[23]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick20Text, uIPage0BrickIndices090PrintControlBrick20.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[24]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick21Text, uIPage0BrickIndices090PrintControlBrick21.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[25]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick8Text, uIPage0BrickIndices090PrintControlBrick8.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[26]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick9Text, uIPage0BrickIndices090PrintControlBrick9.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[27]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick22Text, uIPage0BrickIndices090PrintControlBrick22.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[28]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick23Text, uIPage0BrickIndices090PrintControlBrick23.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[29]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick24Text, uIPage0BrickIndices090PrintControlBrick24.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[30]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick25Text, uIPage0BrickIndices090PrintControlBrick25.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[31]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick26Text, uIPage0BrickIndices090PrintControlBrick26.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[32]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick27Text, uIPage0BrickIndices090PrintControlBrick27.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[33]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick28Text, uIPage0BrickIndices090PrintControlBrick28.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[34]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick29Text, uIPage0BrickIndices090PrintControlBrick29.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[35]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick30Text, uIPage0BrickIndices090PrintControlBrick30.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[36]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick31Text, uIPage0BrickIndices090PrintControlBrick31.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[37]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick32Text, uIPage0BrickIndices090PrintControlBrick32.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[38]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick33Text, uIPage0BrickIndices090PrintControlBrick33.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[39]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick7Text, uIPage0BrickIndices090PrintControlBrick7.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[40]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick34Text, uIPage0BrickIndices090PrintControlBrick34.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[41]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick35Text, uIPage0BrickIndices090PrintControlBrick35.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[42]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick36Text, uIPage0BrickIndices090PrintControlBrick36.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[43]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick37Text, uIPage0BrickIndices090PrintControlBrick37.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[44]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick38Text, uIPage0BrickIndices090PrintControlBrick38.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[45]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick39Text, uIPage0BrickIndices090PrintControlBrick39.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[46]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick40Text, uIPage0BrickIndices090PrintControlBrick40.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[47]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick41Text, uIPage0BrickIndices090PrintControlBrick41.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[48]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick42Text, uIPage0BrickIndices090PrintControlBrick42.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[49]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick43Text, uIPage0BrickIndices090PrintControlBrick43.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[50]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick44Text, uIPage0BrickIndices090PrintControlBrick44.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[51]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick45Text, uIPage0BrickIndices090PrintControlBrick45.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[52]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick46Text, uIPage0BrickIndices090PrintControlBrick46.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[53]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick47Text, uIPage0BrickIndices090PrintControlBrick47.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[54]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick48Text, uIPage0BrickIndices090PrintControlBrick48.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[55]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick49Text, uIPage0BrickIndices090PrintControlBrick49.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[56]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick50Text, uIPage0BrickIndices090PrintControlBrick50.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[57]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick51Text, uIPage0BrickIndices090PrintControlBrick51.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[58]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick52Text, uIPage0BrickIndices090PrintControlBrick52.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[59]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick53Text, uIPage0BrickIndices090PrintControlBrick53.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[60]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick54Text, uIPage0BrickIndices090PrintControlBrick54.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[61]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick55Text, uIPage0BrickIndices090PrintControlBrick55.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[62]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick56Text, uIPage0BrickIndices090PrintControlBrick56.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[63]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick57Text, uIPage0BrickIndices090PrintControlBrick57.Text);

            // Verify that the 'Text' property of 'Page [0].BrickIndices [0].[9].[0].[64]' PrintControlBrick equals '20,0'
            Assert.AreEqual(this.CheckReportValuesC6Step45ExpectedValues.UIPage0BrickIndices090PrintControlBrick58Text, uIPage0BrickIndices090PrintControlBrick58.Text);

            CheckWeek53();
        }
    }
}
