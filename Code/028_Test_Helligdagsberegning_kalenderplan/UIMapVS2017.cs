namespace _028_Test_Helligdagsberegning_kalenderplan.UIMapVS2017Classes
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
        /// EditDahlinValidPeriods - Use 'EditDahlinValidPeriodsParams' to pass parameters into this method.
        /// </summary>
        public void EditDahlinValidPeriod1()
        {
            #region Variable Declarations
            DXTreeListCell uIItem150TreeListCell = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeeTreeList.UINode1TreeListNode.UINode0TreeListNode.UIItem150TreeListCell;
            DXButton uINYButton = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerAvailCustom.UINYButton;
            DXDateTimePicker uISmartDateEditToDateDateTimeEdit = this.UIItemWindow1.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UISmartDateEditToDateDateTimeEdit;
            DXButton uIOKButton = this.UIItemWindow1.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UIOKButton;
            DXTreeListCell uIItem225TreeListCell = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeeTreeList.UINode1TreeListNode.UINode1TreeListNode1.UIItem225TreeListCell;
            DXDateTimePicker uISmartDateEditFromDatDateTimeEdit = this.UIItemWindow1.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UISmartDateEditFromDatDateTimeEdit;
            #endregion

            // Click '1. 50%' TreeListCell
            Mouse.Click(uIItem150TreeListCell, new Point(29, 8));

            // Click 'Ny' button
            Mouse.Click(uINYButton);
            uISmartDateEditFromDatDateTimeEdit.DateTime = new DateTime(2014, 04, 14);
            Keyboard.SendKeys(uISmartDateEditFromDatDateTimeEdit, this.EditDahlinValidPeriodsParams.UISmartDateEditFromDatDateTimeEditSendKeys, ModifierKeys.None);

            // Type '27.04.2014{Tab}' in 'smartDateEditToDate' DateTimeEdit
            uISmartDateEditToDateDateTimeEdit.DateTime = new DateTime(2014, 04, 27);
            Keyboard.SendKeys(uISmartDateEditToDateDateTimeEdit, this.EditDahlinValidPeriodsParams.UISmartDateEditToDateDateTimeEditSendKeys, ModifierKeys.None);

            // Click 'OK' button            
            Mouse.Click(uIOKButton);            
        }

        public void EditDahlinValidPeriod2()
        {
            #region Variable Declarations
            DXTreeListCell uIItem150TreeListCell = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeeTreeList.UINode1TreeListNode.UINode0TreeListNode.UIItem150TreeListCell;
            DXButton uINYButton = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlRightClient.UIViewHostCustom.UIPcViewClient.UIEmployeeEditorsViewCustom.UIPcContentClient.UIXscContentScrollableControl.UIGroupControlViewHostCustom.UIGroupControlClient.UIEmployeeManagerAvailCustom.UINYButton;
            DXDateTimePicker uISmartDateEditToDateDateTimeEdit = this.UIItemWindow1.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UISmartDateEditToDateDateTimeEdit;
            DXButton uIOKButton = this.UIItemWindow1.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UIOKButton;
            DXTreeListCell uIItem225TreeListCell = this.UIAnsatteiarbeidsplanWindow.UIPanelControlOuterClient.UIPanelControlLeftClient.UITreeListEmployeeTreeList.UINode1TreeListNode.UINode1TreeListNode1.UIItem225TreeListCell;
            DXDateTimePicker uISmartDateEditFromDatDateTimeEdit = this.UIItemWindow1.UIPopupContainerBarConMenu.UIPopupControlContaineClient.UISmartDateEditFromDatDateTimeEdit;
            #endregion
            
            // Click '2. 25%' TreeListCell
            Mouse.Click(uIItem225TreeListCell, new Point(24, 7));

            // Click 'Ny' button
            Mouse.Click(uINYButton);

            // Type '28.04.2014{Tab}' in 'smartDateEditFromDate' DateTimeEdit
            uISmartDateEditFromDatDateTimeEdit.DateTime = new DateTime(2014, 04, 28);
            Keyboard.SendKeys(uISmartDateEditFromDatDateTimeEdit, this.EditDahlinValidPeriodsParams.UISmartDateEditFromDatDateTimeEditSendKeys, ModifierKeys.None);

            uISmartDateEditToDateDateTimeEdit.DateTime = new DateTime(2014, 05, 18);
            Keyboard.SendKeys(uISmartDateEditToDateDateTimeEdit, this.EditDahlinValidPeriodsParams.UISmartDateEditToDateDateTimeEditSendKeys, ModifierKeys.None);
            
            Mouse.Click(uIOKButton);
        }

        public virtual EditDahlinValidPeriodsParams EditDahlinValidPeriodsParams
        {
            get
            {
                if ((this.mEditDahlinValidPeriodsParams == null))
                {
                    this.mEditDahlinValidPeriodsParams = new EditDahlinValidPeriodsParams();
                }
                return this.mEditDahlinValidPeriodsParams;
            }
        }

        private EditDahlinValidPeriodsParams mEditDahlinValidPeriodsParams;
    }
    /// <summary>
    /// Parameters to be passed into 'EditDahlinValidPeriods'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class EditDahlinValidPeriodsParams
    {

        #region Fields
        /// <summary>
        /// Type '27.04.2014{Tab}' in 'smartDateEditToDate' DateTimeEdit
        /// </summary>
        public string UISmartDateEditToDateDateTimeEditSendKeys = "{Tab}";

        /// <summary>
        /// Type '28.04.2014{Tab}' in 'smartDateEditFromDate' DateTimeEdit
        /// </summary>
        public string UISmartDateEditFromDatDateTimeEditSendKeys = "{Tab}";
        #endregion
    }
}
