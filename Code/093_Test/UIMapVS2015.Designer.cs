﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 14.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace _093_Test_Helgeavtale_Spekter.UIMapVS2015Classes
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public partial class UIMapVS2015
    {
        
        /// <summary>
        /// ClickNewEmployeeInEmpTab
        /// </summary>
        public void ClickNewEmployeeInEmpTab()
        {
            #region Variable Declarations
            WinClient uIAnsattClient = this.UIGatver66043650ASCLAvWindow.UIItemWindow.UIAnsattClient;
            #endregion

            // Click 'Ansatt' client
            Mouse.Click(uIAnsattClient, new Point(21, 19));
        }
        
        /// <summary>
        /// SelectEmp10AndEditKont
        /// </summary>
        public void SelectEmp10AndEditKont()
        {
            #region Variable Declarations
            WinClient uIAnsatteClient = this.UIGatver66043393ASCLAvWindow1.UIItemWindow.UIAnsatteClient;
            WinClient uITimelisteClient = this.UIGatver66043393ASCLAvWindow1.UIItemWindow1.UITimelisteClient;
            WinClient uITimelisteClient1 = this.UIGatver66043393ASCLAvWindow1.UIItemWindow2.UITimelisteClient;
            #endregion

            // Click 'Ansatte' client
            Mouse.Click(uIAnsatteClient, new Point(73, 26));

            // Click 'Timeliste' client
            Mouse.Click(uITimelisteClient, new Point(93, 26));

            // Click 'Timeliste' client
            Mouse.Click(uITimelisteClient1, new Point(43, 13));
        }
        
        /// <summary>
        /// ClickEditRosterplanChap5_step3
        /// </summary>
        public void ClickEditRosterplanChap5_step3()
        {
            #region Variable Declarations
            DXRibbonButtonItem uIRedigerRibbonBaseButtonItem = this.UIArbeidsplanWindow.UIRcMenuRibbon.UIRpPlanRibbonPage.UIRibbonPageGroup9RibbonPageGroup.UIRedigerRibbonBaseButtonItem;
            #endregion

            // Click 'Rediger' RibbonBaseButtonItem
            Mouse.Click(uIRedigerRibbonBaseButtonItem, new Point(24, 25));
        }
        
        #region Properties
        public UIGatver66043393ASCLAvWindow1 UIGatver66043393ASCLAvWindow1
        {
            get
            {
                if ((this.mUIGatver66043393ASCLAvWindow1 == null))
                {
                    this.mUIGatver66043393ASCLAvWindow1 = new UIGatver66043393ASCLAvWindow1();
                }
                return this.mUIGatver66043393ASCLAvWindow1;
            }
        }
        
        public UIGatver66043650ASCLAvWindow UIGatver66043650ASCLAvWindow
        {
            get
            {
                if ((this.mUIGatver66043650ASCLAvWindow == null))
                {
                    this.mUIGatver66043650ASCLAvWindow = new UIGatver66043650ASCLAvWindow();
                }
                return this.mUIGatver66043650ASCLAvWindow;
            }
        }
        
        public UIArbeidsplanWindow UIArbeidsplanWindow
        {
            get
            {
                if ((this.mUIArbeidsplanWindow == null))
                {
                    this.mUIArbeidsplanWindow = new UIArbeidsplanWindow();
                }
                return this.mUIArbeidsplanWindow;
            }
        }
        #endregion
        
        #region Fields
        private UIGatver66043393ASCLAvWindow1 mUIGatver66043393ASCLAvWindow1;
        
        private UIGatver66043650ASCLAvWindow mUIGatver66043650ASCLAvWindow;
        
        private UIArbeidsplanWindow mUIArbeidsplanWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIGatver66043393ASCLAvWindow1 : WinWindow
    {
        
        public UIGatver66043393ASCLAvWindow1()
        {
            #region Search Criteria
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "Gat", PropertyExpressionOperator.Contains));
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TfrmMain";
            this.WindowTitles.Add("Gat");
            #endregion
        }
        
        #region Properties
        public UIItemWindow UIItemWindow
        {
            get
            {
                if ((this.mUIItemWindow == null))
                {
                    this.mUIItemWindow = new UIItemWindow(this);
                }
                return this.mUIItemWindow;
            }
        }
        
        public UIItemWindow1 UIItemWindow1
        {
            get
            {
                if ((this.mUIItemWindow1 == null))
                {
                    this.mUIItemWindow1 = new UIItemWindow1(this);
                }
                return this.mUIItemWindow1;
            }
        }
        
        public UIItemWindow2 UIItemWindow2
        {
            get
            {
                if ((this.mUIItemWindow2 == null))
                {
                    this.mUIItemWindow2 = new UIItemWindow2(this);
                }
                return this.mUIItemWindow2;
            }
        }
        #endregion
        
        #region Fields
        private UIItemWindow mUIItemWindow;
        
        private UIItemWindow1 mUIItemWindow1;
        
        private UIItemWindow2 mUIItemWindow2;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIItemWindow : WinWindow
    {
        
        public UIItemWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TcxGridSite";
            this.WindowTitles.Add("Gat");
            #endregion
        }
        
        #region Properties
        public WinClient UIAnsatteClient
        {
            get
            {
                if ((this.mUIAnsatteClient == null))
                {
                    this.mUIAnsatteClient = new WinClient(this);
                    #region Search Criteria
                    this.mUIAnsatteClient.WindowTitles.Add("Gat");
                    #endregion
                }
                return this.mUIAnsatteClient;
            }
        }
        #endregion
        
        #region Fields
        private WinClient mUIAnsatteClient;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIItemWindow1 : WinWindow
    {
        
        public UIItemWindow1(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TcxGridSite";
            this.SearchProperties[WinWindow.PropertyNames.Instance] = "2";
            this.WindowTitles.Add("Gat");
            #endregion
        }
        
        #region Properties
        public WinClient UITimelisteClient
        {
            get
            {
                if ((this.mUITimelisteClient == null))
                {
                    this.mUITimelisteClient = new WinClient(this);
                    #region Search Criteria
                    this.mUITimelisteClient.WindowTitles.Add("Gat");
                    #endregion
                }
                return this.mUITimelisteClient;
            }
        }
        #endregion
        
        #region Fields
        private WinClient mUITimelisteClient;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIItemWindow2 : WinWindow
    {
        
        public UIItemWindow2(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TPanel";
            this.SearchProperties[WinWindow.PropertyNames.Instance] = "8";
            this.WindowTitles.Add("Gat");
            #endregion
        }
        
        #region Properties
        public WinClient UITimelisteClient
        {
            get
            {
                if ((this.mUITimelisteClient == null))
                {
                    this.mUITimelisteClient = new WinClient(this);
                    #region Search Criteria
                    this.mUITimelisteClient.WindowTitles.Add("Gat");
                    #endregion
                }
                return this.mUITimelisteClient;
            }
        }
        #endregion
        
        #region Fields
        private WinClient mUITimelisteClient;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIGatver66043650ASCLAvWindow : WinWindow
    {
        
        public UIGatver66043650ASCLAvWindow()
        {
            #region Search Criteria
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "Gat", PropertyExpressionOperator.Contains));
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TfrmMain";
            this.WindowTitles.Add("Gat");
            #endregion
        }
        
        #region Properties
        public UIItemWindow3 UIItemWindow
        {
            get
            {
                if ((this.mUIItemWindow == null))
                {
                    this.mUIItemWindow = new UIItemWindow3(this);
                }
                return this.mUIItemWindow;
            }
        }
        #endregion
        
        #region Fields
        private UIItemWindow3 mUIItemWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIItemWindow3 : WinWindow
    {
        
        public UIItemWindow3(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TPanel";
            this.SearchProperties[WinWindow.PropertyNames.Instance] = "11";
            this.WindowTitles.Add("Gat");
            #endregion
        }
        
        #region Properties
        public WinClient UIAnsattClient
        {
            get
            {
                if ((this.mUIAnsattClient == null))
                {
                    this.mUIAnsattClient = new WinClient(this);
                    #region Search Criteria
                    this.mUIAnsattClient.WindowTitles.Add("Gat");
                    #endregion
                }
                return this.mUIAnsattClient;
            }
        }
        #endregion
        
        #region Fields
        private WinClient mUIAnsattClient;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIArbeidsplanWindow : DXWindow
    {
        
        public UIArbeidsplanWindow()
        {
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = "Arbeidsplan";
            this.SearchProperties.Add(new PropertyExpression(DXTestControl.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("Arbeidsplan");
            #endregion
        }
        
        #region Properties
        public UIRcMenuRibbon UIRcMenuRibbon
        {
            get
            {
                if ((this.mUIRcMenuRibbon == null))
                {
                    this.mUIRcMenuRibbon = new UIRcMenuRibbon(this);
                }
                return this.mUIRcMenuRibbon;
            }
        }
        #endregion
        
        #region Fields
        private UIRcMenuRibbon mUIRcMenuRibbon;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIRcMenuRibbon : DXRibbon
    {
        
        public UIRcMenuRibbon(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = "rcMenu";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonControl";
            this.SearchProperties[DXTestControl.PropertyNames.HierarchyLevel] = "2";
            this.WindowTitles.Add("Arbeidsplan");
            #endregion
        }
        
        #region Properties
        public UIRpPlanRibbonPage UIRpPlanRibbonPage
        {
            get
            {
                if ((this.mUIRpPlanRibbonPage == null))
                {
                    this.mUIRpPlanRibbonPage = new UIRpPlanRibbonPage(this);
                }
                return this.mUIRpPlanRibbonPage;
            }
        }
        #endregion
        
        #region Fields
        private UIRpPlanRibbonPage mUIRpPlanRibbonPage;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIRpPlanRibbonPage : DXRibbonPage
    {
        
        public UIRpPlanRibbonPage(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = "rpPlan";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonPage";
            this.WindowTitles.Add("Arbeidsplan");
            #endregion
        }
        
        #region Properties
        public UIRibbonPageGroup9RibbonPageGroup UIRibbonPageGroup9RibbonPageGroup
        {
            get
            {
                if ((this.mUIRibbonPageGroup9RibbonPageGroup == null))
                {
                    this.mUIRibbonPageGroup9RibbonPageGroup = new UIRibbonPageGroup9RibbonPageGroup(this);
                }
                return this.mUIRibbonPageGroup9RibbonPageGroup;
            }
        }
        #endregion
        
        #region Fields
        private UIRibbonPageGroup9RibbonPageGroup mUIRibbonPageGroup9RibbonPageGroup;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "14.0.23107.0")]
    public class UIRibbonPageGroup9RibbonPageGroup : DXRibbonPageGroup
    {
        
        public UIRibbonPageGroup9RibbonPageGroup(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[DXTestControl.PropertyNames.Name] = "ribbonPageGroup9";
            this.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonPageGroup";
            this.WindowTitles.Add("Arbeidsplan");
            #endregion
        }
        
        #region Properties
        public DXRibbonButtonItem UIRedigerRibbonBaseButtonItem
        {
            get
            {
                if ((this.mUIRedigerRibbonBaseButtonItem == null))
                {
                    this.mUIRedigerRibbonBaseButtonItem = new DXRibbonButtonItem(this);
                    #region Search Criteria
                    this.mUIRedigerRibbonBaseButtonItem.SearchProperties[DXTestControl.PropertyNames.Name] = "btnEditMode";
                    this.mUIRedigerRibbonBaseButtonItem.SearchProperties[DXTestControl.PropertyNames.ClassName] = "RibbonBaseButtonItem";
                    this.mUIRedigerRibbonBaseButtonItem.WindowTitles.Add("Arbeidsplan");
                    #endregion
                }
                return this.mUIRedigerRibbonBaseButtonItem;
            }
        }
        #endregion
        
        #region Fields
        private DXRibbonButtonItem mUIRedigerRibbonBaseButtonItem;
        #endregion
    }
}
