namespace _021_Test_Arbeidsplan_lønnsberegning.UIMapVS2017Classes
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
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

        ///// <summary>
        ///// CheckFixedPaymentsGridDataStep_9_26 - Use 'CheckFixedPaymentsGridDataStep_9_26ExpectedValues' to pass parameters into this method.
        ///// </summary>
        //public List<string> CheckFixedPaymentsGridDataStep_9_26()
        //{
        //    #region Variable Declarations
        //    var errorList = new List<string>();
        //    DXPivotGridFieldValue uIVaktkode1PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkode1PivotGridFieldValue;
        //    DXPivotGridCell uIItem8PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem8PivotGridCell;
        //    DXPivotGridCell uIItemPivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell;
        //    DXPivotGridFieldValue uIVaktkode1234PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkode1234PivotGridFieldValue;
        //    DXPivotGridCell uIItem2333PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2333PivotGridCell;
        //    DXPivotGridCell uIItem30PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkode2PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkode2PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem24102011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem24102011PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem28122014PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem28122014PivotGridFieldValue;
        //    DXPivotGridCell uIItem2PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2PivotGridCell;
        //    DXPivotGridCell uIItemPivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkode3PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkode3PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem31102011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem31102011PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem04112011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem04112011PivotGridFieldValue;
        //    DXPivotGridCell uIItemPivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell2;
        //    DXPivotGridCell uIItem3PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem3PivotGridCell;
        //    DXPivotGridFieldValue uIItem07112011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem07112011PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem28122014PivotGridFieldValue1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem28122014PivotGridFieldValue1;
        //    DXPivotGridCell uIItem6PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem6PivotGridCell;
        //    DXPivotGridCell uIItem15PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem15PivotGridCell;
        //    DXPivotGridFieldValue uIItem24102011PivotGridFieldValue1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem24102011PivotGridFieldValue1;
        //    DXPivotGridFieldValue uIItem30102011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30102011PivotGridFieldValue;
        //    DXPivotGridCell uIItemPivotGridCell3 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell3;
        //    DXPivotGridCell uIItem6PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem6PivotGridCell1;
        //    DXPivotGridFieldValue uIItem05112011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem05112011PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem06112011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem06112011PivotGridFieldValue;
        //    DXPivotGridCell uIItem6PivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem6PivotGridCell2;
        //    DXPivotGridCell uIItem6PivotGridCell3 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem6PivotGridCell3;
        //    DXPivotGridFieldValue uIVaktkode4PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkode4PivotGridFieldValue;
        //    DXPivotGridCell uIItem733PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem733PivotGridCell;
        //    DXPivotGridCell uIItem15PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem15PivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkodeAPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeAPivotGridFieldValue;
        //    DXPivotGridCell uIItem15PivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem15PivotGridCell2;
        //    DXPivotGridCell uIItem30PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30PivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkodeA1PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeA1PivotGridFieldValue;
        //    DXPivotGridCell uIItem15PivotGridCell3 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem15PivotGridCell3;
        //    DXPivotGridCell uIItem25PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem25PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeBPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeBPivotGridFieldValue;
        //    DXPivotGridCell uIItem533PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem533PivotGridCell;
        //    DXPivotGridCell uIItem1333PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1333PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeBXPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeBXPivotGridFieldValue;
        //    DXPivotGridCell uIItem31PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem31PivotGridCell;
        //    DXPivotGridCell uIItem2833PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2833PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeB1PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeB1PivotGridFieldValue;
        //    DXPivotGridCell uIItem267PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem267PivotGridCell;
        //    DXPivotGridCell uIItem5PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem5PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeB1_4PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeB1_4PivotGridFieldValue;
        //    DXPivotGridCell uIItem16PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem16PivotGridCell;
        //    DXPivotGridCell uIItem2833PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2833PivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkodeB2PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeB2PivotGridFieldValue;
        //    DXPivotGridCell uIItem4PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem4PivotGridCell;
        //    DXPivotGridCell uIItem10PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem10PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeB3PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeB3PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem24102011PivotGridFieldValue2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem24102011PivotGridFieldValue2;
        //    DXPivotGridFieldValue uIItem28122014PivotGridFieldValue2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem28122014PivotGridFieldValue2;
        //    DXPivotGridCell uIItem533PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem533PivotGridCell1;
        //    DXPivotGridCell uIItem1333PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1333PivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkodeB4PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeB4PivotGridFieldValue;
        //    DXPivotGridCell uIItem4PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem4PivotGridCell1;
        //    DXPivotGridCell uIItemPivotGridCell4 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell4;
        //    DXPivotGridFieldValue uIVaktkodeDPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeDPivotGridFieldValue;
        //    DXPivotGridCell uIItem16PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem16PivotGridCell1;
        //    DXPivotGridCell uIItemPivotGridCell5 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell5;
        //    DXPivotGridFieldValue uIVaktkodeD3APivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeD3APivotGridFieldValue;
        //    DXPivotGridCell uIItem30PivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30PivotGridCell2;
        //    DXPivotGridCell uIItem30PivotGridCell3 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30PivotGridCell3;
        //    DXPivotGridFieldValue uIVaktkodeDAPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeDAPivotGridFieldValue;
        //    DXPivotGridCell uIItem31PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem31PivotGridCell1;
        //    DXPivotGridCell uIItem30PivotGridCell4 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30PivotGridCell4;
        //    DXPivotGridFieldValue uIVaktkodeD3PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeD3PivotGridFieldValue;
        //    DXPivotGridCell uIItem15PivotGridCell4 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem15PivotGridCell4;
        //    DXPivotGridCell uIItemPivotGridCell6 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell6;
        //    DXPivotGridFieldValue uIItem05032012PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem05032012PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem02122012PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem02122012PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem01102012PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem01102012PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem30092012PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30092012PivotGridFieldValue;
        //    DXPivotGridFieldValue uIVaktkodeFYPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeFYPivotGridFieldValue;
        //    DXPivotGridCell uIItem32PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem32PivotGridCell;
        //    DXPivotGridCell uIItem35PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem35PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeHJVPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeHJVPivotGridFieldValue;
        //    DXPivotGridCell uIItem28PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem28PivotGridCell;
        //    DXPivotGridCell uIItem6PivotGridCell4 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem6PivotGridCell4;
        //    DXPivotGridFieldValue uIVaktkodeHJV2PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeHJV2PivotGridFieldValue;
        //    DXPivotGridCell uIItem1033PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1033PivotGridCell;
        //    DXPivotGridCell uIItem2167PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2167PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeKKPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeKKPivotGridFieldValue;
        //    DXPivotGridCell uIItem3356PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem3356PivotGridCell;
        //    DXPivotGridCell uIItem3555PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem3555PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeKK2PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeKK2PivotGridFieldValue;
        //    DXPivotGridCell uIItem1787PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1787PivotGridCell;
        //    DXPivotGridCell uIItem17PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem17PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeKK3PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeKK3PivotGridFieldValue;
        //    DXPivotGridCell uIItem2073PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2073PivotGridCell;
        //    DXPivotGridCell uIItem1767PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1767PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeKK4PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeKK4PivotGridFieldValue;
        //    DXPivotGridCell uIItem102PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem102PivotGridCell;
        //    DXPivotGridCell uIItem1417PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1417PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeNPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeNPivotGridFieldValue;
        //    DXPivotGridCell uIItem16PivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem16PivotGridCell2;
        //    DXPivotGridCell uIItem35PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem35PivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkodeNXPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeNXPivotGridFieldValue;
        //    DXPivotGridCell uIItem315PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem315PivotGridCell;
        //    DXPivotGridCell uIItem35PivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem35PivotGridCell2;
        //    DXPivotGridFieldValue uIVaktkodeN2PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeN2PivotGridFieldValue;
        //    DXPivotGridCell uIItem20PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem20PivotGridCell;
        //    DXPivotGridCell uIItem40PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem40PivotGridCell;
        //    #endregion

        //    try
        //    {
        //        // Verify that the 'ClassName' property of 'Vaktkode, 1' PivotGridFieldValue equals 'PivotGridFieldValue'
        //        Assert.AreEqual("Vaktkode, 1", uIVaktkode1PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '8' PivotGridCell equals '8'
        //        Assert.AreEqual("8", uIItem8PivotGridCell.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode 1: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'ClassName' property of 'Vaktkode, 1234' PivotGridFieldValue equals 'PivotGridFieldValue'
        //        Assert.AreEqual("Vaktkode, 1234", uIVaktkode1234PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '23,33' PivotGridCell equals '23,33'
        //        Assert.AreEqual("23,33", uIItem2333PivotGridCell.Text);

        //        // Verify that the 'Text' property of '30' PivotGridCell equals '30'
        //        Assert.AreEqual("30", uIItem30PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode 1234: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'ClassName' property of 'Vaktkode, 2' PivotGridFieldValue equals 'PivotGridFieldValue'
        //        Assert.AreEqual("Vaktkode, 2", uIVaktkode2PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '24.10.2011' PivotGridFieldValue equals '24.10.2011'
        //        Assert.AreEqual("24.10.2011", uIItem24102011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '28.12.2014' PivotGridFieldValue equals '28.12.2014'
        //        Assert.AreEqual("28.12.2014", uIItem28122014PivotGridFieldValue.Text);

        //        Assert.AreEqual("2", uIItem2PivotGridCell.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode 2: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, 3' PivotGridFieldValue equals 'Vaktkode, 3'
        //        Assert.AreEqual("Vaktkode, 3", uIVaktkode3PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '31.10.2011' PivotGridFieldValue equals '31.10.2011'
        //        Assert.AreEqual("31.10.2011", uIItem31102011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '04.11.2011' PivotGridFieldValue equals '04.11.2011'
        //        Assert.AreEqual("04.11.2011", uIItem04112011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell2.Text);

        //        // Verify that the 'ClassName' property of '3' PivotGridCell equals 'PivotGridCell'
        //        Assert.AreEqual("3", uIItem3PivotGridCell.Text);

        //        // Verify that the 'Text' property of '07.11.2011' PivotGridFieldValue equals '07.11.2011'
        //        Assert.AreEqual("07.11.2011", uIItem07112011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '28.12.2014' PivotGridFieldValue equals '28.12.2014'
        //        Assert.AreEqual("28.12.2014", uIItem28122014PivotGridFieldValue1.Text);

        //        // Verify that the 'Text' property of '6' PivotGridCell equals '6'
        //        Assert.AreEqual("6", uIItem6PivotGridCell.Text);

        //        // Verify that the 'Text' property of '15' PivotGridCell equals '15'
        //        Assert.AreEqual("15", uIItem15PivotGridCell.Text);

        //        // Verify that the 'Text' property of '24.10.2011' PivotGridFieldValue equals '24.10.2011'
        //        Assert.AreEqual("24.10.2011", uIItem24102011PivotGridFieldValue1.Text);

        //        // Verify that the 'Text' property of '30.10.2011' PivotGridFieldValue equals '30.10.2011'
        //        Assert.AreEqual("30.10.2011", uIItem30102011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell3.Text);

        //        // Verify that the 'Text' property of '6' PivotGridCell equals '6'
        //        Assert.AreEqual("6", uIItem6PivotGridCell1.Text);

        //        // Verify that the 'Text' property of '05.11.2011' PivotGridFieldValue equals '05.11.2011'
        //        Assert.AreEqual("05.11.2011", uIItem05112011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '06.11.2011' PivotGridFieldValue equals '06.11.2011'
        //        Assert.AreEqual("06.11.2011", uIItem06112011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '6' PivotGridCell equals '6'
        //        Assert.AreEqual("6", uIItem6PivotGridCell2.Text);

        //        // Verify that the 'Text' property of '6' PivotGridCell equals '6'
        //        Assert.AreEqual("6", uIItem6PivotGridCell3.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode 3: " + e.Message);
        //    }


        //    try
        //    {
        //        Assert.AreEqual("Vaktkode, 4", uIVaktkode4PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '7,33' PivotGridCell equals '7,33'
        //        Assert.AreEqual("7,33", uIItem733PivotGridCell.Text);

        //        // Verify that the 'Text' property of '15' PivotGridCell equals '15'
        //        Assert.AreEqual("15", uIItem15PivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode 4: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, A' PivotGridFieldValue equals 'Vaktkode, A'
        //        Assert.AreEqual("Vaktkode, A", uIVaktkodeAPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '15' PivotGridCell equals '15'
        //        Assert.AreEqual("15", uIItem15PivotGridCell2.Text);

        //        // Verify that the 'Text' property of '30' PivotGridCell equals '30'
        //        Assert.AreEqual("30", uIItem30PivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode A: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, A1' PivotGridFieldValue equals 'Vaktkode, A1'
        //        Assert.AreEqual("Vaktkode, A1", uIVaktkodeA1PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '15' PivotGridCell equals '15'
        //        Assert.AreEqual("15", uIItem15PivotGridCell3.Text);

        //        // Verify that the 'Text' property of '25' PivotGridCell equals '25'
        //        Assert.AreEqual("25", uIItem25PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode A1: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B' PivotGridFieldValue equals 'Vaktkode, B'
        //        Assert.AreEqual("Vaktkode, B", uIVaktkodeBPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '5,33' PivotGridCell equals '5,33'
        //        Assert.AreEqual("5,33", uIItem533PivotGridCell.Text);

        //        // Verify that the 'Text' property of '13,33' PivotGridCell equals '13,33'
        //        Assert.AreEqual("13,33", uIItem1333PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B: " + e.Message);
        //    }


        //    try
        //    {
        //        Assert.AreEqual("Vaktkode, B X", uIVaktkodeBXPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '31' PivotGridCell equals '31'
        //        Assert.AreEqual("31", uIItem31PivotGridCell.Text);

        //        // Verify that the 'Text' property of '28,33' PivotGridCell equals '28,33'
        //        Assert.AreEqual("28,33", uIItem2833PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode BX: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B1' PivotGridFieldValue equals 'Vaktkode, B1'
        //        Assert.AreEqual("Vaktkode, B1", uIVaktkodeB1PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '2,67' PivotGridCell equals '2,67'
        //        Assert.AreEqual("2,67", uIItem267PivotGridCell.Text);

        //        // Verify that the 'Text' property of '5' PivotGridCell equals '5'
        //        Assert.AreEqual("5", uIItem5PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B1: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B1_4' PivotGridFieldValue equals 'Vaktkode, B1_4'
        //        Assert.AreEqual("Vaktkode, B1_4", uIVaktkodeB1_4PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '16' PivotGridCell equals '16'
        //        Assert.AreEqual("16", uIItem16PivotGridCell.Text);

        //        // Verify that the 'Text' property of '28,33' PivotGridCell equals '28,33'
        //        Assert.AreEqual("28,33", uIItem2833PivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B1_4: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B2' PivotGridFieldValue equals 'Vaktkode, B2'
        //        Assert.AreEqual("Vaktkode, B2", uIVaktkodeB2PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '4' PivotGridCell equals '4'
        //        Assert.AreEqual("4", uIItem4PivotGridCell.Text);

        //        // Verify that the 'Text' property of '10' PivotGridCell equals '10'
        //        Assert.AreEqual("10", uIItem10PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B2: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B3' PivotGridFieldValue equals 'Vaktkode, B3'
        //        Assert.AreEqual("Vaktkode, B3", uIVaktkodeB3PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '24.10.2011' PivotGridFieldValue equals '24.10.2011'
        //        Assert.AreEqual("24.10.2011", uIItem24102011PivotGridFieldValue2.Text);

        //        // Verify that the 'Text' property of '28.12.2014' PivotGridFieldValue equals '28.12.2014'
        //        Assert.AreEqual("28.12.2014", uIItem28122014PivotGridFieldValue2.Text);

        //        // Verify that the 'Text' property of '5,33' PivotGridCell equals '5,33'
        //        Assert.AreEqual("5,33", uIItem533PivotGridCell1.Text);

        //        // Verify that the 'Text' property of '13,33' PivotGridCell equals '13,33'
        //        Assert.AreEqual("13,33", uIItem1333PivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B3: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B4' PivotGridFieldValue equals 'Vaktkode, B4'
        //        Assert.AreEqual("Vaktkode, B4", uIVaktkodeB4PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '4' PivotGridCell equals '4'
        //        Assert.AreEqual("4", uIItem4PivotGridCell1.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell4.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B4: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, D' PivotGridFieldValue equals 'Vaktkode, D'
        //        Assert.AreEqual("Vaktkode, D", uIVaktkodeDPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '16' PivotGridCell equals '16'
        //        Assert.AreEqual("16", uIItem16PivotGridCell1.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell5.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode D: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, D 3 A' PivotGridFieldValue equals 'Vaktkode, D 3 A'
        //        Assert.AreEqual("Vaktkode, D 3 A", uIVaktkodeD3APivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '30' PivotGridCell equals '30'
        //        Assert.AreEqual("30", uIItem30PivotGridCell2.Text);

        //        // Verify that the 'Text' property of '30' PivotGridCell equals '30'
        //        Assert.AreEqual("30", uIItem30PivotGridCell3.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode D3A: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, D A' PivotGridFieldValue equals 'Vaktkode, D A'
        //        Assert.AreEqual("Vaktkode, D A", uIVaktkodeDAPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '31' PivotGridCell equals '31'
        //        Assert.AreEqual("31", uIItem31PivotGridCell1.Text);

        //        // Verify that the 'Text' property of '30' PivotGridCell equals '30'
        //        Assert.AreEqual("30", uIItem30PivotGridCell4.Text);

        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode DA: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, D3' PivotGridFieldValue equals 'Vaktkode, D3'
        //        Assert.AreEqual("Vaktkode, D3", uIVaktkodeD3PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '15' PivotGridCell equals '15'
        //        Assert.AreEqual("15", uIItem15PivotGridCell4.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell6.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode D3: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'Text' property of '05.03.2012' PivotGridFieldValue equals '05.03.2012'
        //        Assert.AreEqual("05.03.2012", uIItem05032012PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '02.12.2012' PivotGridFieldValue equals '02.12.2012'
        //        Assert.AreEqual("02.12.2012", uIItem02122012PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '01.10.2012' PivotGridFieldValue equals '01.10.2012'
        //        Assert.AreEqual("01.10.2012", uIItem01102012PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '30.09.2012' PivotGridFieldValue equals '30.09.2012'
        //        Assert.AreEqual("30.09.2012", uIItem30092012PivotGridFieldValue.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Dates error: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, F Y' PivotGridFieldValue equals 'Vaktkode, F Y'
        //        Assert.AreEqual("Vaktkode, F Y", uIVaktkodeFYPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '32' PivotGridCell equals '32'
        //        Assert.AreEqual("32", uIItem32PivotGridCell.Text);

        //        // Verify that the 'Text' property of '35' PivotGridCell equals '35'
        //        Assert.AreEqual("35", uIItem35PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode FY: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, H J V' PivotGridFieldValue equals 'Vaktkode, H J V'
        //        Assert.AreEqual("Vaktkode, H J V", uIVaktkodeHJVPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '2,8' PivotGridCell equals '2,8'
        //        Assert.AreEqual("2,8", uIItem28PivotGridCell.Text);

        //        // Verify that the 'Text' property of '6' PivotGridCell equals '6'
        //        Assert.AreEqual("6", uIItem6PivotGridCell4.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode HJV: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, H J V 2' PivotGridFieldValue equals 'Vaktkode, H J V 2'
        //        Assert.AreEqual("Vaktkode, H J V 2", uIVaktkodeHJV2PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '10,33' PivotGridCell equals '10,33'
        //        Assert.AreEqual("10,33", uIItem1033PivotGridCell.Text);

        //        // Verify that the 'Text' property of '21,67' PivotGridCell equals '21,67'
        //        Assert.AreEqual("21,67", uIItem2167PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode HJV2: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, K K' PivotGridFieldValue equals 'Vaktkode, K K'
        //        Assert.AreEqual("Vaktkode, K K", uIVaktkodeKKPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '33,56' PivotGridCell equals '33,56'
        //        Assert.AreEqual("33,56", uIItem3356PivotGridCell.Text);

        //        // Verify that the 'Text' property of '35,55' PivotGridCell equals '35,55'
        //        Assert.AreEqual("35,55", uIItem3555PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode KK: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, K K 2' PivotGridFieldValue equals 'Vaktkode, K K 2'
        //        Assert.AreEqual("Vaktkode, K K 2", uIVaktkodeKK2PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '17,87' PivotGridCell equals '17,87'
        //        Assert.AreEqual("17,87", uIItem1787PivotGridCell.Text);

        //        // Verify that the 'Text' property of '17' PivotGridCell equals '17'
        //        Assert.AreEqual("17", uIItem17PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode KK2: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, K K 3' PivotGridFieldValue equals 'Vaktkode, K K 3'
        //        Assert.AreEqual("Vaktkode, K K 3", uIVaktkodeKK3PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '20,73' PivotGridCell equals '20,73'
        //        Assert.AreEqual("20,73", uIItem2073PivotGridCell.Text);

        //        // Verify that the 'Text' property of '17,67' PivotGridCell equals '17,67'
        //        Assert.AreEqual("17,67", uIItem1767PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode KK3: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, K K 4' PivotGridFieldValue equals 'Vaktkode, K K 4'
        //        Assert.AreEqual("Vaktkode, K K 4", uIVaktkodeKK4PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '10,2' PivotGridCell equals '10,2'
        //        Assert.AreEqual("10,2", uIItem102PivotGridCell.Text);

        //        // Verify that the 'Text' property of '14,17' PivotGridCell equals '14,17'
        //        Assert.AreEqual("14,17", uIItem1417PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode KK4: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, N' PivotGridFieldValue equals 'Vaktkode, N'
        //        Assert.AreEqual("Vaktkode, N", uIVaktkodeNPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '16' PivotGridCell equals '16'
        //        Assert.AreEqual("16", uIItem16PivotGridCell2.Text);

        //        // Verify that the 'Text' property of '35' PivotGridCell equals '35'
        //        Assert.AreEqual("35", uIItem35PivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode N: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, N X' PivotGridFieldValue equals 'Vaktkode, N X'
        //        Assert.AreEqual("Vaktkode, N X", uIVaktkodeNXPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '31,5' PivotGridCell equals '31,5'
        //        Assert.AreEqual("31,5", uIItem315PivotGridCell.Text);

        //        // Verify that the 'ValueAsString' property of '35' PivotGridCell equals '35'
        //        Assert.AreEqual("35", uIItem35PivotGridCell2.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode NX: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, N2' PivotGridFieldValue equals 'Vaktkode, N2'
        //        Assert.AreEqual("Vaktkode, N2", uIVaktkodeN2PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '20' PivotGridCell equals '20'
        //        Assert.AreEqual("20", uIItem20PivotGridCell.Text);

        //        // Verify that the 'Text' property of '40' PivotGridCell equals '40'
        //        Assert.AreEqual("40", uIItem40PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode N2: " + e.Message);
        //    }

        //    return errorList;
        //}
        //public List<string> CheckFixedPaymentsGridDataStep_9_28()
        //{
        //    #region Variable Declarations
        //    var errorList = new List<string>();
        //    DXPivotGridFieldValue uIVaktkode1PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkode1PivotGridFieldValue;
        //    DXPivotGridCell uIItem8PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem8PivotGridCell;
        //    DXPivotGridCell uIItemPivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell;
        //    DXPivotGridFieldValue uIVaktkode1234PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkode1234PivotGridFieldValue;
        //    DXPivotGridCell uIItem2333PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2333PivotGridCell;
        //    DXPivotGridCell uIItem30PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkode2PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkode2PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem24102011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem24102011PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem28122014PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem28122014PivotGridFieldValue;
        //    DXPivotGridCell uIItem2PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2PivotGridCell;
        //    DXPivotGridCell uIItemPivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkode3PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkode3PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem31102011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem31102011PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem04112011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem04112011PivotGridFieldValue;
        //    DXPivotGridCell uIItemPivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell2;
        //    DXPivotGridCell uIItem3PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem3PivotGridCell;
        //    DXPivotGridFieldValue uIItem07112011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem07112011PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem28122014PivotGridFieldValue1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem28122014PivotGridFieldValue1;
        //    DXPivotGridCell uIItem6PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem6PivotGridCell;
        //    DXPivotGridCell uIItem15PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem15PivotGridCell;
        //    DXPivotGridFieldValue uIItem24102011PivotGridFieldValue1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem24102011PivotGridFieldValue1;
        //    DXPivotGridFieldValue uIItem30102011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30102011PivotGridFieldValue;
        //    DXPivotGridCell uIItemPivotGridCell3 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell3;
        //    DXPivotGridCell uIItem6PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem6PivotGridCell1;
        //    DXPivotGridFieldValue uIItem05112011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem05112011PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem06112011PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem06112011PivotGridFieldValue;
        //    DXPivotGridCell uIItem6PivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem6PivotGridCell2;
        //    DXPivotGridCell uIItem6PivotGridCell3 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem6PivotGridCell3;
        //    DXPivotGridFieldValue uIVaktkode4PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkode4PivotGridFieldValue;
        //    DXPivotGridCell uIItem733PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem733PivotGridCell;
        //    DXPivotGridCell uIItem15PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem15PivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkodeAPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeAPivotGridFieldValue;
        //    DXPivotGridCell uIItem15PivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem15PivotGridCell2;
        //    DXPivotGridCell uIItem30PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30PivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkodeA1PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeA1PivotGridFieldValue;
        //    DXPivotGridCell uIItem15PivotGridCell3 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem15PivotGridCell3;
        //    DXPivotGridCell uIItem25PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem25PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeBPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeBPivotGridFieldValue;
        //    DXPivotGridCell uIItem533PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem533PivotGridCell;
        //    DXPivotGridCell uIItem1333PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1333PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeBXPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeBXPivotGridFieldValue;
        //    DXPivotGridCell uIItem31PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem31PivotGridCell;
        //    DXPivotGridCell uIItem2833PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2833PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeB1PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeB1PivotGridFieldValue;
        //    DXPivotGridCell uIItem267PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem267PivotGridCell;
        //    DXPivotGridCell uIItem5PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem5PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeB1_4PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeB1_4PivotGridFieldValue;
        //    DXPivotGridCell uIItem16PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem16PivotGridCell;
        //    DXPivotGridCell uIItem2833PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2833PivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkodeB2PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeB2PivotGridFieldValue;
        //    DXPivotGridCell uIItem4PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem4PivotGridCell;
        //    DXPivotGridCell uIItem10PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem10PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeB3PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeB3PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem24102011PivotGridFieldValue2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem24102011PivotGridFieldValue2;
        //    DXPivotGridFieldValue uIItem28122014PivotGridFieldValue2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem28122014PivotGridFieldValue2;
        //    DXPivotGridCell uIItem533PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem533PivotGridCell1;
        //    DXPivotGridCell uIItem1333PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1333PivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkodeB4PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeB4PivotGridFieldValue;
        //    DXPivotGridCell uIItem4PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem4PivotGridCell1;
        //    DXPivotGridCell uIItemPivotGridCell4 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell4;
        //    DXPivotGridFieldValue uIVaktkodeDPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeDPivotGridFieldValue;
        //    DXPivotGridCell uIItem16PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem16PivotGridCell1;
        //    DXPivotGridCell uIItemPivotGridCell5 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell5;
        //    DXPivotGridFieldValue uIVaktkodeD3APivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeD3APivotGridFieldValue;
        //    DXPivotGridCell uIItem30PivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30PivotGridCell2;
        //    DXPivotGridCell uIItem30PivotGridCell3 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30PivotGridCell3;
        //    DXPivotGridFieldValue uIVaktkodeDAPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeDAPivotGridFieldValue;
        //    DXPivotGridCell uIItem31PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem31PivotGridCell1;
        //    DXPivotGridCell uIItem30PivotGridCell4 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30PivotGridCell4;
        //    DXPivotGridFieldValue uIVaktkodeD3PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeD3PivotGridFieldValue;
        //    DXPivotGridCell uIItem15PivotGridCell4 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem15PivotGridCell4;
        //    DXPivotGridCell uIItemPivotGridCell6 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItemPivotGridCell6;
        //    DXPivotGridFieldValue uIItem05032012PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem05032012PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem02122012PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem02122012PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem01102012PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem01102012PivotGridFieldValue;
        //    DXPivotGridFieldValue uIItem30092012PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem30092012PivotGridFieldValue;
        //    DXPivotGridFieldValue uIVaktkodeFYPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeFYPivotGridFieldValue;
        //    DXPivotGridCell uIItem32PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem32PivotGridCell;
        //    DXPivotGridCell uIItem35PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem35PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeHJVPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeHJVPivotGridFieldValue;
        //    DXPivotGridCell uIItem28PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem28PivotGridCell;
        //    DXPivotGridCell uIItem6PivotGridCell4 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem6PivotGridCell4;
        //    DXPivotGridFieldValue uIVaktkodeHJV2PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeHJV2PivotGridFieldValue;
        //    DXPivotGridCell uIItem1033PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1033PivotGridCell;
        //    DXPivotGridCell uIItem2167PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2167PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeKKPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeKKPivotGridFieldValue;
        //    DXPivotGridCell uIItem3356PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem3356PivotGridCell;
        //    DXPivotGridCell uIItem3555PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem3555PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeKK2PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeKK2PivotGridFieldValue;
        //    DXPivotGridCell uIItem1787PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1787PivotGridCell;
        //    DXPivotGridCell uIItem17PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem17PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeKK3PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeKK3PivotGridFieldValue;
        //    DXPivotGridCell uIItem2073PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem2073PivotGridCell;
        //    DXPivotGridCell uIItem1767PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1767PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeKK4PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeKK4PivotGridFieldValue;
        //    DXPivotGridCell uIItem102PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem102PivotGridCell;
        //    DXPivotGridCell uIItem1417PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem1417PivotGridCell;
        //    DXPivotGridFieldValue uIVaktkodeNPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeNPivotGridFieldValue;
        //    DXPivotGridCell uIItem16PivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem16PivotGridCell2;
        //    DXPivotGridCell uIItem35PivotGridCell1 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem35PivotGridCell1;
        //    DXPivotGridFieldValue uIVaktkodeNXPivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeNXPivotGridFieldValue;
        //    DXPivotGridCell uIItem315PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem315PivotGridCell;
        //    DXPivotGridCell uIItem35PivotGridCell2 = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem35PivotGridCell2;
        //    DXPivotGridFieldValue uIVaktkodeN2PivotGridFieldValue = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIVaktkodeN2PivotGridFieldValue;
        //    DXPivotGridCell uIItem20PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem20PivotGridCell;
        //    DXPivotGridCell uIItem40PivotGridCell = this.UIArbeidsplanWindow.UIDpnlVisualizationDockPanel.UIDockPanel3_ContainerCustom.UITcVisualizationTabList.UITpFixedPaymentClient.UIFixedPaymentControlCustom.UIViewHost1Custom.UIPcViewClient.UIFixedPaymentControlCustom.UIPgcCalculationResultPivotGrid.UIItem40PivotGridCell;
        //    #endregion

        //    try
        //    {
        //        // Verify that the 'ClassName' property of 'Vaktkode, 1' PivotGridFieldValue equals 'PivotGridFieldValue'
        //        Assert.AreEqual("Vaktkode, 1", uIVaktkode1PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '8' PivotGridCell equals '8'
        //        Assert.AreEqual("11,89", uIItem8PivotGridCell.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode 1: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'ClassName' property of 'Vaktkode, 1234' PivotGridFieldValue equals 'PivotGridFieldValue'
        //        Assert.AreEqual("Vaktkode, 1234", uIVaktkode1234PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '23,33' PivotGridCell equals '23,33'
        //        Assert.AreEqual("34,68", uIItem2333PivotGridCell.Text);

        //        // Verify that the 'Text' property of '30' PivotGridCell equals '30'
        //        Assert.AreEqual("44,6", uIItem30PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode 1234: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'ClassName' property of 'Vaktkode, 2' PivotGridFieldValue equals 'PivotGridFieldValue'
        //        Assert.AreEqual("Vaktkode, 2", uIVaktkode2PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '24.10.2011' PivotGridFieldValue equals '24.10.2011'
        //        Assert.AreEqual("24.10.2011", uIItem24102011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '28.12.2014' PivotGridFieldValue equals '28.12.2014'
        //        Assert.AreEqual("28.12.2014", uIItem28122014PivotGridFieldValue.Text);

        //        Assert.AreEqual("2,97", uIItem2PivotGridCell.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode 2: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, 3' PivotGridFieldValue equals 'Vaktkode, 3'
        //        Assert.AreEqual("Vaktkode, 3", uIVaktkode3PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '31.10.2011' PivotGridFieldValue equals '31.10.2011'
        //        Assert.AreEqual("31.10.2011", uIItem31102011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '04.11.2011' PivotGridFieldValue equals '04.11.2011'
        //        Assert.AreEqual("04.11.2011", uIItem04112011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell2.Text);

        //        // Verify that the 'ClassName' property of '3' PivotGridCell equals 'PivotGridCell'
        //        Assert.AreEqual("18,73", uIItem3PivotGridCell.Text);

        //        // Verify that the 'Text' property of '07.11.2011' PivotGridFieldValue equals '07.11.2011'
        //        Assert.AreEqual("07.11.2011", uIItem07112011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '28.12.2014' PivotGridFieldValue equals '28.12.2014'
        //        Assert.AreEqual("28.12.2014", uIItem28122014PivotGridFieldValue1.Text);

        //        // Verify that the 'Text' property of '6' PivotGridCell equals '6'
        //        Assert.AreEqual("8,92", uIItem6PivotGridCell.Text);

        //        // Verify that the 'Text' property of '15' PivotGridCell equals '15'
        //        Assert.AreEqual("22,3", uIItem15PivotGridCell.Text);

        //        // Verify that the 'Text' property of '24.10.2011' PivotGridFieldValue equals '24.10.2011'
        //        Assert.AreEqual("24.10.2011", uIItem24102011PivotGridFieldValue1.Text);

        //        // Verify that the 'Text' property of '30.10.2011' PivotGridFieldValue equals '30.10.2011'
        //        Assert.AreEqual("30.10.2011", uIItem30102011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell3.Text);

        //        // Verify that the 'Text' property of '6' PivotGridCell equals '6'
        //        Assert.AreEqual("26,76", uIItem6PivotGridCell1.Text);

        //        // Verify that the 'Text' property of '05.11.2011' PivotGridFieldValue equals '05.11.2011'
        //        Assert.AreEqual("05.11.2011", uIItem05112011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '06.11.2011' PivotGridFieldValue equals '06.11.2011'
        //        Assert.AreEqual("06.11.2011", uIItem06112011PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '6' PivotGridCell equals '6'
        //        Assert.AreEqual("93,66", uIItem6PivotGridCell2.Text);

        //        // Verify that the 'Text' property of '6' PivotGridCell equals '6'
        //        Assert.AreEqual("93,66", uIItem6PivotGridCell3.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode 3: " + e.Message);
        //    }


        //    try
        //    {
        //        Assert.AreEqual("Vaktkode, 4", uIVaktkode4PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '7,33' PivotGridCell equals '7,33'
        //        Assert.AreEqual("10,9", uIItem733PivotGridCell.Text);

        //        // Verify that the 'Text' property of '15' PivotGridCell equals '15'
        //        Assert.AreEqual("22,3", uIItem15PivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode 4: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, A' PivotGridFieldValue equals 'Vaktkode, A'
        //        Assert.AreEqual("Vaktkode, A", uIVaktkodeAPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '15' PivotGridCell equals '15'
        //        Assert.AreEqual("22,3", uIItem15PivotGridCell2.Text);

        //        // Verify that the 'Text' property of '30' PivotGridCell equals '30'
        //        Assert.AreEqual("44,6", uIItem30PivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode A: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, A1' PivotGridFieldValue equals 'Vaktkode, A1'
        //        Assert.AreEqual("Vaktkode, A1", uIVaktkodeA1PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '15' PivotGridCell equals '15'
        //        Assert.AreEqual("22,3", uIItem15PivotGridCell3.Text);

        //        // Verify that the 'Text' property of '25' PivotGridCell equals '25'
        //        Assert.AreEqual("37,17", uIItem25PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode A1: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B' PivotGridFieldValue equals 'Vaktkode, B'
        //        Assert.AreEqual("Vaktkode, B", uIVaktkodeBPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '5,33' PivotGridCell equals '5,33'
        //        Assert.AreEqual("7,92", uIItem533PivotGridCell.Text);

        //        // Verify that the 'Text' property of '13,33' PivotGridCell equals '13,33'
        //        Assert.AreEqual("19,82", uIItem1333PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B: " + e.Message);
        //    }


        //    try
        //    {
        //        Assert.AreEqual("Vaktkode, B X", uIVaktkodeBXPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '31' PivotGridCell equals '31'
        //        Assert.AreEqual("46,09", uIItem31PivotGridCell.Text);

        //        // Verify that the 'Text' property of '28,33' PivotGridCell equals '28,33'
        //        Assert.AreEqual("42,12", uIItem2833PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode BX: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B1' PivotGridFieldValue equals 'Vaktkode, B1'
        //        Assert.AreEqual("Vaktkode, B1", uIVaktkodeB1PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '2,67' PivotGridCell equals '2,67'
        //        Assert.AreEqual("3,97", uIItem267PivotGridCell.Text);

        //        // Verify that the 'Text' property of '5' PivotGridCell equals '5'
        //        Assert.AreEqual("7,43", uIItem5PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B1: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B1_4' PivotGridFieldValue equals 'Vaktkode, B1_4'
        //        Assert.AreEqual("Vaktkode, B1_4", uIVaktkodeB1_4PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '16' PivotGridCell equals '16'
        //        Assert.AreEqual("23,79", uIItem16PivotGridCell.Text);

        //        // Verify that the 'Text' property of '28,33' PivotGridCell equals '28,33'
        //        Assert.AreEqual("42,12", uIItem2833PivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B1_4: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B2' PivotGridFieldValue equals 'Vaktkode, B2'
        //        Assert.AreEqual("Vaktkode, B2", uIVaktkodeB2PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '4' PivotGridCell equals '4'
        //        Assert.AreEqual("5,95", uIItem4PivotGridCell.Text);

        //        // Verify that the 'Text' property of '10' PivotGridCell equals '10'
        //        Assert.AreEqual("14,87", uIItem10PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B2: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B3' PivotGridFieldValue equals 'Vaktkode, B3'
        //        Assert.AreEqual("Vaktkode, B3", uIVaktkodeB3PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '24.10.2011' PivotGridFieldValue equals '24.10.2011'
        //        Assert.AreEqual("24.10.2011", uIItem24102011PivotGridFieldValue2.Text);

        //        // Verify that the 'Text' property of '28.12.2014' PivotGridFieldValue equals '28.12.2014'
        //        Assert.AreEqual("28.12.2014", uIItem28122014PivotGridFieldValue2.Text);

        //        // Verify that the 'Text' property of '5,33' PivotGridCell equals '5,33'
        //        Assert.AreEqual("7,92", uIItem533PivotGridCell1.Text);

        //        // Verify that the 'Text' property of '13,33' PivotGridCell equals '13,33'
        //        Assert.AreEqual("19,82", uIItem1333PivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B3: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, B4' PivotGridFieldValue equals 'Vaktkode, B4'
        //        Assert.AreEqual("Vaktkode, B4", uIVaktkodeB4PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '4' PivotGridCell equals '4'
        //        Assert.AreEqual("5,95", uIItem4PivotGridCell1.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell4.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode B4: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, D' PivotGridFieldValue equals 'Vaktkode, D'
        //        Assert.AreEqual("Vaktkode, D", uIVaktkodeDPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '16' PivotGridCell equals '16'
        //        Assert.AreEqual("23,79", uIItem16PivotGridCell1.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell5.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode D: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, D 3 A' PivotGridFieldValue equals 'Vaktkode, D 3 A'
        //        Assert.AreEqual("Vaktkode, D 3 A", uIVaktkodeD3APivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '30' PivotGridCell equals '30'
        //        Assert.AreEqual("44,6", uIItem30PivotGridCell2.Text);

        //        // Verify that the 'Text' property of '30' PivotGridCell equals '30'
        //        Assert.AreEqual("44,6", uIItem30PivotGridCell3.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode D3A: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, D A' PivotGridFieldValue equals 'Vaktkode, D A'
        //        Assert.AreEqual("Vaktkode, D A", uIVaktkodeDAPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '31' PivotGridCell equals '31'
        //        Assert.AreEqual("46,09", uIItem31PivotGridCell1.Text);

        //        // Verify that the 'Text' property of '30' PivotGridCell equals '30'
        //        Assert.AreEqual("44,6", uIItem30PivotGridCell4.Text);

        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode DA: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, D3' PivotGridFieldValue equals 'Vaktkode, D3'
        //        Assert.AreEqual("Vaktkode, D3", uIVaktkodeD3PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '15' PivotGridCell equals '15'
        //        Assert.AreEqual("22,3", uIItem15PivotGridCell4.Text);

        //        // Verify that the 'Text' property of PivotGridCell equals ''
        //        Assert.AreEqual("", uIItemPivotGridCell6.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode D3: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'Text' property of '05.03.2012' PivotGridFieldValue equals '05.03.2012'
        //        Assert.AreEqual("05.03.2012", uIItem05032012PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '02.12.2012' PivotGridFieldValue equals '02.12.2012'
        //        Assert.AreEqual("02.12.2012", uIItem02122012PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '01.10.2012' PivotGridFieldValue equals '01.10.2012'
        //        Assert.AreEqual("01.10.2012", uIItem01102012PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '30.09.2012' PivotGridFieldValue equals '30.09.2012'
        //        Assert.AreEqual("30.09.2012", uIItem30092012PivotGridFieldValue.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Dates error: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, F Y' PivotGridFieldValue equals 'Vaktkode, F Y'
        //        Assert.AreEqual("Vaktkode, F Y", uIVaktkodeFYPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '32' PivotGridCell equals '32'
        //        Assert.AreEqual("47,57", uIItem32PivotGridCell.Text);

        //        // Verify that the 'Text' property of '35' PivotGridCell equals '35'
        //        Assert.AreEqual("52,03", uIItem35PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode FY: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, H J V' PivotGridFieldValue equals 'Vaktkode, H J V'
        //        Assert.AreEqual("Vaktkode, H J V", uIVaktkodeHJVPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '2,8' PivotGridCell equals '2,8'
        //        Assert.AreEqual("4,16", uIItem28PivotGridCell.Text);

        //        // Verify that the 'Text' property of '6' PivotGridCell equals '6'
        //        Assert.AreEqual("8,92", uIItem6PivotGridCell4.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode HJV: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, H J V 2' PivotGridFieldValue equals 'Vaktkode, H J V 2'
        //        Assert.AreEqual("Vaktkode, H J V 2", uIVaktkodeHJV2PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '10,33' PivotGridCell equals '10,33'
        //        Assert.AreEqual("15,36", uIItem1033PivotGridCell.Text);

        //        // Verify that the 'Text' property of '21,67' PivotGridCell equals '21,67'
        //        Assert.AreEqual("32,22", uIItem2167PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode HJV2: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, K K' PivotGridFieldValue equals 'Vaktkode, K K'
        //        Assert.AreEqual("Vaktkode, K K", uIVaktkodeKKPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '33,56' PivotGridCell equals '33,56'
        //        Assert.AreEqual("49,89", uIItem3356PivotGridCell.Text);

        //        // Verify that the 'Text' property of '35,55' PivotGridCell equals '35,55'
        //        Assert.AreEqual("52,85", uIItem3555PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode KK: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, K K 2' PivotGridFieldValue equals 'Vaktkode, K K 2'
        //        Assert.AreEqual("Vaktkode, K K 2", uIVaktkodeKK2PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '17,87' PivotGridCell equals '17,87'
        //        Assert.AreEqual("26,57", uIItem1787PivotGridCell.Text);

        //        // Verify that the 'Text' property of '17' PivotGridCell equals '17'
        //        Assert.AreEqual("25,27", uIItem17PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode KK2: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, K K 3' PivotGridFieldValue equals 'Vaktkode, K K 3'
        //        Assert.AreEqual("Vaktkode, K K 3", uIVaktkodeKK3PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '20,73' PivotGridCell equals '20,73'
        //        Assert.AreEqual("30,82", uIItem2073PivotGridCell.Text);

        //        // Verify that the 'Text' property of '17,67' PivotGridCell equals '17,67'
        //        Assert.AreEqual("26,27", uIItem1767PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode KK3: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, K K 4' PivotGridFieldValue equals 'Vaktkode, K K 4'
        //        Assert.AreEqual("Vaktkode, K K 4", uIVaktkodeKK4PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '10,2' PivotGridCell equals '10,2'
        //        Assert.AreEqual("15,16", uIItem102PivotGridCell.Text);

        //        // Verify that the 'Text' property of '14,17' PivotGridCell equals '14,17'
        //        Assert.AreEqual("21,07", uIItem1417PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode KK4: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, N' PivotGridFieldValue equals 'Vaktkode, N'
        //        Assert.AreEqual("Vaktkode, N", uIVaktkodeNPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '16' PivotGridCell equals '16'
        //        Assert.AreEqual("23,79", uIItem16PivotGridCell2.Text);

        //        // Verify that the 'Text' property of '35' PivotGridCell equals '35'
        //        Assert.AreEqual("52,03", uIItem35PivotGridCell1.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode N: " + e.Message);
        //    }


        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, N X' PivotGridFieldValue equals 'Vaktkode, N X'
        //        Assert.AreEqual("Vaktkode, N X", uIVaktkodeNXPivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '31,5' PivotGridCell equals '31,5'
        //        Assert.AreEqual("46,83", uIItem315PivotGridCell.Text);

        //        // Verify that the 'ValueAsString' property of '35' PivotGridCell equals '35'
        //        Assert.AreEqual("52,03", uIItem35PivotGridCell2.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode NX: " + e.Message);
        //    }

        //    try
        //    {
        //        // Verify that the 'Text' property of 'Vaktkode, N2' PivotGridFieldValue equals 'Vaktkode, N2'
        //        Assert.AreEqual("Vaktkode, N2", uIVaktkodeN2PivotGridFieldValue.Text);

        //        // Verify that the 'Text' property of '20' PivotGridCell equals '20'
        //        Assert.AreEqual("29,73", uIItem20PivotGridCell.Text);

        //        // Verify that the 'Text' property of '40' PivotGridCell equals '40'
        //        Assert.AreEqual("59,47", uIItem40PivotGridCell.Text);
        //    }
        //    catch (Exception e)
        //    {
        //        errorList.Add("Vaktkode N2: " + e.Message);
        //    }

        //    return errorList;
        //}
    }
}
