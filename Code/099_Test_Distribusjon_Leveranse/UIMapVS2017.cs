namespace _099_Test_Distribusjon_Leveranse.UIMapVS2017Classes
{
    using DevExpress.CodedUIExtension.DXTestControls.v19_2;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
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
        /// ClickWishPlanLabelToShowOlderWishPlans - Use 'ClickWishPlanLabelToShowOlderWishPlansParams' to pass parameters into this method.
        /// </summary>
        public void ClickWishPlanLabelToShowOlderWishPlans()
        {
            #region Variable Declarations
            Playback.Wait(1000);
            HtmlCheckBox uIMyTimeLogPage_showAlCheckBox = this.UIMinønskeplanMinGatv2Window.UIMinønskeplanMinGatv2Document.UIMyTimeLogPage_showAlCheckBox;
            #endregion

            var point = new Point(uIMyTimeLogPage_showAlCheckBox.BoundingRectangle.X + 5, uIMyTimeLogPage_showAlCheckBox.BoundingRectangle.Y + 5);
            Mouse.Move(point);
            Mouse.Click();
        }

        #region Swedish webservices

        /// <summary>
        /// CheckEmployeeChangeTrackingService - Use 'CheckEmployeeChangeTrackingServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckEmployeeChangeTrackingService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIPushEmployeeDataChanHyperlink = this.UIDepartmentServiceV20Window1.UIEmployeeChangeTrackiDocument.UIContentPane.UIPushEmployeeDataChanHyperlink;
            #endregion

            // Verify that the 'InnerText' property of 'PushEmployeeDataChanges' link equals 'PushEmployeeDataChanges'
            Assert.AreEqual(this.CheckEmployeeChangeTrackingServiceExpectedValues.UIPushEmployeeDataChanHyperlinkInnerText, uIPushEmployeeDataChanHyperlink.InnerText);

            Assert.AreEqual("http://localhost/GatWs1_se/EmployeeChangeTrackingService.asmx?op=PushEmployeeDataChanges", uIPushEmployeeDataChanHyperlink.Href);
        }

        /// <summary>
        /// CheckEmployeeServiceV63 - Use 'CheckEmployeeServiceV63ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckEmployeeServiceV63_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIGetEmployeeHyperlink = this.UIDepartmentServiceV20Window1.UIEmployeeServiceV63WeDocument.UIContentPane.UIGetEmployeeHyperlink;
            HtmlHyperlink uIGetJobAgreementHyperlink = this.UIDepartmentServiceV20Window1.UIEmployeeServiceV63WeDocument.UIContentPane.UIGetJobAgreementHyperlink;
            HtmlHyperlink uIGetWorkHoursHyperlink = this.UIDepartmentServiceV20Window1.UIEmployeeServiceV63WeDocument.UIContentPane.UIGetWorkHoursHyperlink;
            HtmlHyperlink uIUpdateEmployeeHyperlink = this.UIDepartmentServiceV20Window1.UIEmployeeServiceV63WeDocument.UIContentPane.UIUpdateEmployeeHyperlink;
            #endregion

            // Verify that the 'InnerText' property of 'GetEmployee' link equals 'GetEmployee'
            Assert.AreEqual(this.CheckEmployeeServiceV63ExpectedValues.UIGetEmployeeHyperlinkInnerText, uIGetEmployeeHyperlink.InnerText);

            // Verify that the 'Href' property of 'GetEmployee' link equals 'http://localhost/GatWs1_se/EmployeeServiceV63.asmx?op=GetEmployee'
            Assert.AreEqual("http://localhost/GatWs1_se/EmployeeServiceV63.asmx?op=GetEmployee", uIGetEmployeeHyperlink.Href);

            // Verify that the 'InnerText' property of 'GetJobAgreement' link equals 'GetJobAgreement'
            Assert.AreEqual(this.CheckEmployeeServiceV63ExpectedValues.UIGetJobAgreementHyperlinkInnerText, uIGetJobAgreementHyperlink.InnerText);

            // Verify that the 'Href' property of 'GetJobAgreement' link equals 'http://localhost/GatWs1_se/EmployeeServiceV63.asmx?op=GetJobAgreement'
            Assert.AreEqual("http://localhost/GatWs1_se/EmployeeServiceV63.asmx?op=GetJobAgreement", uIGetJobAgreementHyperlink.Href);

            // Verify that the 'InnerText' property of 'GetWorkHours' link equals 'GetWorkHours'
            Assert.AreEqual("GetWorkHours", uIGetWorkHoursHyperlink.InnerText);

            // Verify that the 'Href' property of 'GetWorkHours' link equals 'http://localhost/GatWs1_se/EmployeeServiceV63.asmx?op=GetWorkHours'
            Assert.AreEqual("http://localhost/GatWs1_se/EmployeeServiceV63.asmx?op=GetWorkHours", uIGetWorkHoursHyperlink.Href);

            // Verify that the 'InnerText' property of 'UpdateEmployee' link equals 'UpdateEmployee'
            Assert.AreEqual(this.CheckEmployeeServiceV63ExpectedValues.UIUpdateEmployeeHyperlinkInnerText, uIUpdateEmployeeHyperlink.InnerText);

            // Verify that the 'Href' property of 'UpdateEmployee' link equals 'http://localhost/GatWs1_se/EmployeeServiceV63.asmx?op=UpdateEmployee'
            Assert.AreEqual("http://localhost/GatWs1_se/EmployeeServiceV63.asmx?op=UpdateEmployee", uIUpdateEmployeeHyperlink.Href);
        }

        /// <summary>
        /// CheckExportService - Use 'CheckExportServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckExportService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIPostAfterSystemExporHyperlink = this.UIAppServiceWebServiceWindow.UIExportServiceWebServDocument.UIContentPane.UIPostAfterSystemExporHyperlink;
            HtmlHyperlink uIProcessAfterSystemExHyperlink = this.UIAppServiceWebServiceWindow.UIExportServiceWebServDocument.UIContentPane.UIProcessAfterSystemExHyperlink;
            HtmlHyperlink uIProcessNextAfterSystHyperlink = this.UIAppServiceWebServiceWindow.UIExportServiceWebServDocument.UIContentPane.UIProcessNextAfterSystHyperlink;
            #endregion

            // Verify that the 'Href' property of 'PostAfterSystemExportReceipt' link equals 'http://localhost/GatWs1_se/ExportService.asmx?op=PostAfterSystemExportReceipt'
            Assert.AreEqual("http://localhost/GatWs1_se/ExportService.asmx?op=PostAfterSystemExportReceipt", uIPostAfterSystemExporHyperlink.Href);

            // Verify that the 'Href' property of 'ProcessAfterSystemExport' link equals 'http://localhost/GatWs1_se/ExportService.asmx?op=ProcessAfterSystemExport'
            Assert.AreEqual("http://localhost/GatWs1_se/ExportService.asmx?op=ProcessAfterSystemExport", uIProcessAfterSystemExHyperlink.Href);

            // Verify that the 'Href' property of 'ProcessNextAfterSystemExportJob' link equals 'http://localhost/GatWs1_se/ExportService.asmx?op=ProcessNextAfterSystemExportJob'
            Assert.AreEqual("http://localhost/GatWs1_se/ExportService.asmx?op=ProcessNextAfterSystemExportJob", uIProcessNextAfterSystHyperlink.Href);
        }

        /// <summary>
        /// CheckGatGerica - Use 'CheckGatGericaExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckGatGerica_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIA_PrsGericaWS_GenereHyperlink = this.UIDepartmentServiceV20Window2.UIGatGericaWebServiceDocument.UIContentPane.UIA_PrsGericaWS_GenereHyperlink;
            HtmlHyperlink uIFinnBrukerForIDHyperlink = this.UIDepartmentServiceV20Window2.UIGatGericaWebServiceDocument.UIContentPane.UIFinnBrukerForIDHyperlink;
            HtmlHyperlink uIGetVersionHyperlink = this.UIDepartmentServiceV20Window2.UIGatGericaWebServiceDocument.UIContentPane.UIGetVersionHyperlink;
            HtmlHyperlink uILesAnsettelseHyperlink = this.UIDepartmentServiceV20Window2.UIGatGericaWebServiceDocument.UIContentPane.UILesAnsettelseHyperlink;
            HtmlHyperlink uILesFravaerForPersonHyperlink = this.UIDepartmentServiceV20Window2.UIGatGericaWebServiceDocument.UIContentPane.UILesFravaerForPersonHyperlink;
            HtmlHyperlink uILesOrgenhetHyperlink = this.UIDepartmentServiceV20Window2.UIGatGericaWebServiceDocument.UIContentPane.UILesOrgenhetHyperlink;
            HtmlHyperlink uILesPersonHyperlink = this.UIDepartmentServiceV20Window2.UIGatGericaWebServiceDocument.UIContentPane.UILesPersonHyperlink;
            HtmlHyperlink uILesPlanForPeriodeHyperlink = this.UIDepartmentServiceV20Window2.UIGatGericaWebServiceDocument.UIContentPane.UILesPlanForPeriodeHyperlink;
            HtmlHyperlink uILesVaktForPersonHyperlink = this.UIDepartmentServiceV20Window2.UIGatGericaWebServiceDocument.UIContentPane.UILesVaktForPersonHyperlink;
            #endregion

            // Verify that the 'InnerText' property of 'A_PrsGericaWS_Generell_Info' link equals 'A_PrsGericaWS_Generell_Info'
            Assert.AreEqual(this.CheckGatGericaExpectedValues.UIA_PrsGericaWS_GenereHyperlinkInnerText, uIA_PrsGericaWS_GenereHyperlink.InnerText);

            // Verify that the 'InnerText' property of 'FinnBrukerForID' link equals 'FinnBrukerForID'
            Assert.AreEqual(this.CheckGatGericaExpectedValues.UIFinnBrukerForIDHyperlinkInnerText, uIFinnBrukerForIDHyperlink.InnerText);

            // Verify that the 'InnerText' property of 'GetVersion' link equals 'GetVersion'
            Assert.AreEqual(this.CheckGatGericaExpectedValues.UIGetVersionHyperlinkInnerText, uIGetVersionHyperlink.InnerText);

            // Verify that the 'InnerText' property of 'LesAnsettelse' link equals 'LesAnsettelse'
            Assert.AreEqual(this.CheckGatGericaExpectedValues.UILesAnsettelseHyperlinkInnerText, uILesAnsettelseHyperlink.InnerText);

            // Verify that the 'InnerText' property of 'LesFravaerForPerson' link equals 'LesFravaerForPerson'
            Assert.AreEqual(this.CheckGatGericaExpectedValues.UILesFravaerForPersonHyperlinkInnerText, uILesFravaerForPersonHyperlink.InnerText);

            // Verify that the 'InnerText' property of 'LesOrgenhet' link equals 'LesOrgenhet'
            Assert.AreEqual(this.CheckGatGericaExpectedValues.UILesOrgenhetHyperlinkInnerText, uILesOrgenhetHyperlink.InnerText);

            // Verify that the 'InnerText' property of 'LesPerson' link equals 'LesPerson'
            Assert.AreEqual(this.CheckGatGericaExpectedValues.UILesPersonHyperlinkInnerText, uILesPersonHyperlink.InnerText);

            // Verify that the 'InnerText' property of 'LesPlanForPeriode' link equals 'LesPlanForPeriode'
            Assert.AreEqual(this.CheckGatGericaExpectedValues.UILesPlanForPeriodeHyperlinkInnerText, uILesPlanForPeriodeHyperlink.InnerText);

            // Verify that the 'Href' property of 'LesVaktForPerson' link equals 'http://localhost/GatWs1_se/GatGerica.asmx?op=LesVaktForPerson'
            Assert.AreEqual("http://localhost/GatWs1_se/GatGerica.asmx?op=LesVaktForPerson", uILesVaktForPersonHyperlink.Href);
        }

        /// <summary>
        /// CheckGatTaskSchedulerDataService - Use 'CheckGatTaskSchedulerDataServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckGatTaskSchedulerDataService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIGetScheduledTasksHyperlink = this.UIAppServiceWebServiceWindow.UIGatTaskSchedulerDataDocument.UIContentPane.UIGetScheduledTasksHyperlink;
            #endregion

            // Verify that the 'Href' property of 'GetScheduledTasks' link equals 'http://localhost/GatWs1_se/GatTaskSchedulerDataService.asmx?op=GetScheduledTasks'
            Assert.AreEqual("http://localhost/GatWs1_se/GatTaskSchedulerDataService.asmx?op=GetScheduledTasks", uIGetScheduledTasksHyperlink.Href);
        }

        /// <summary>
        /// CheckIdmService - Use 'CheckIdmServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckIdmService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIActivateUserHyperlink = this.UIGatWebservices2MainPWindow.UIIdmServiceWebServiceDocument.UIContentPane.UIActivateUserHyperlink;
            HtmlHyperlink uIActivateUserByDateHyperlink = this.UIGatWebservices2MainPWindow.UIIdmServiceWebServiceDocument.UIContentPane.UIActivateUserByDateHyperlink;
            HtmlHyperlink uIAdvancedCreateOrUpdaHyperlink = this.UIGatWebservices2MainPWindow.UIIdmServiceWebServiceDocument.UIContentPane.UIAdvancedCreateOrUpdaHyperlink;
            HtmlHyperlink uIAdvancedCreateOrUpdaHyperlink1 = this.UIGatWebservices2MainPWindow.UIIdmServiceWebServiceDocument.UIContentPane.UIAdvancedCreateOrUpdaHyperlink1;
            HtmlHyperlink uIGetUsersHyperlink = this.UIGatWebservices2MainPWindow.UIIdmServiceWebServiceDocument.UIContentPane.UIGetUsersHyperlink;
            HtmlHyperlink uIGetUsers_UserNameHyperlink = this.UIGatWebservices2MainPWindow.UIIdmServiceWebServiceDocument.UIContentPane.UIGetUsers_UserNameHyperlink;
            HtmlHyperlink uIHelloWorldHyperlink = this.UIGatWebservices2MainPWindow.UIIdmServiceWebServiceDocument.UIContentPane.UIHelloWorldHyperlink;
            #endregion

            // Verify that the 'Href' property of 'ActivateUser' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/IdmService.asmx?op=ActivateUser", uIActivateUserHyperlink.Href);

            // Verify that the 'Href' property of 'ActivateUserByDate' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/IdmService.asmx?op=ActivateUserByDate", uIActivateUserByDateHyperlink.Href);

            // Verify that the 'Href' property of 'AdvancedCreateOrUpdateUser' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/IdmService.asmx?op=AdvancedCreateOrUpdateUser", uIAdvancedCreateOrUpdaHyperlink.Href);

            // Verify that the 'Href' property of 'AdvancedCreateOrUpdateUserByToDate' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/IdmService.asmx?op=AdvancedCreateOrUpdateUserByToDate", uIAdvancedCreateOrUpdaHyperlink1.Href);

            // Verify that the 'Href' property of 'GetUsers' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/IdmService.asmx?op=GetUsers", uIGetUsersHyperlink.Href);

            // Verify that the 'Href' property of 'GetUsers_UserName' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/IdmService.asmx?op=GetUsers_UserName", uIGetUsers_UserNameHyperlink.Href);

            // Verify that the 'Href' property of 'HelloWorld' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/IdmService.asmx?op=HelloWorld", uIHelloWorldHyperlink.Href);
        }

        /// <summary>
        /// CheckImportService - Use 'CheckImportServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckImportService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIInsertDepEmp_v2Hyperlink = this.UIDepartmentServiceV20Window3.UIImportServiceWebServDocument.UIContentPane.UIInsertDepEmp_v2Hyperlink;
            HtmlHyperlink uIInsertDepartment_v2Hyperlink = this.UIDepartmentServiceV20Window3.UIImportServiceWebServDocument.UIContentPane.UIInsertDepartment_v2Hyperlink;
            HtmlHyperlink uIInsertEmployeeHyperlink = this.UIDepartmentServiceV20Window3.UIImportServiceWebServDocument.UIContentPane.UIInsertEmployeeHyperlink;
            #endregion

            // Verify that the 'Href' property of 'InsertDepEmp_v2' link equals 'http://localhost/GatWs1_se/ImportService.asmx?op=InsertDepEmp_v2'
            Assert.AreEqual("http://localhost/GatWs1_se/ImportService.asmx?op=InsertDepEmp_v2", uIInsertDepEmp_v2Hyperlink.Href);

            // Verify that the 'Href' property of 'InsertDepartment_v2' link equals 'http://localhost/GatWs1_se/ImportService.asmx?op=InsertDepartment_v2'
            Assert.AreEqual("http://localhost/GatWs1_se/ImportService.asmx?op=InsertDepartment_v2", uIInsertDepartment_v2Hyperlink.Href);

            // Verify that the 'Href' property of 'InsertEmployee' link equals 'http://localhost/GatWs1_se/ImportService.asmx?op=InsertEmployee'
            Assert.AreEqual("http://localhost/GatWs1_se/ImportService.asmx?op=InsertEmployee", uIInsertEmployeeHyperlink.Href);
        }

        /// <summary>
        /// CheckPatientInformationService - Use 'CheckPatientInformationServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckPatientInformationService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIGetDepartmentAppointHyperlink = this.UIDepartmentServiceV20Window4.UIPatientInformationSeDocument.UIContentPane.UIGetDepartmentAppointHyperlink;
            HtmlHyperlink uIGetEmployeeAppointmeHyperlink = this.UIDepartmentServiceV20Window4.UIPatientInformationSeDocument.UIContentPane.UIGetEmployeeAppointmeHyperlink;
            #endregion

            // Verify that the 'InnerText' property of 'GetDepartmentAppointments' link equals 'GetDepartmentAppointments'
            Assert.AreEqual(this.CheckPatientInformationServiceExpectedValues.UIGetDepartmentAppointHyperlinkInnerText, uIGetDepartmentAppointHyperlink.InnerText);

            // Verify that the 'Href' property of 'GetDepartmentAppointments' link equals 'http://localhost/GatWs1_se/PatientInformationService.asmx?op=GetDepartmentAppointments'
            Assert.AreEqual("http://localhost/GatWs1_se/PatientInformationService.asmx?op=GetDepartmentAppointments", uIGetDepartmentAppointHyperlink.Href);

            // Verify that the 'Href' property of 'GetEmployeeAppointments' link equals 'http://localhost/GatWs1_se/PatientInformationService.asmx?op=GetEmployeeAppointments'
            Assert.AreEqual("http://localhost/GatWs1_se/PatientInformationService.asmx?op=GetEmployeeAppointments", uIGetEmployeeAppointmeHyperlink.Href);
        }

        /// <summary>
        /// CheckPayslipImportService - Use 'CheckPayslipImportServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckPayslipImportService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIImportPayslipsHyperlink = this.UIAppServiceWebServiceWindow.UIPayslipImportServiceDocument.UIContentPane.UIImportPayslipsHyperlink;
            #endregion

            // Verify that the 'Href' property of 'ImportPayslips' link equals 'http://localhost/GatWs1_se/PayslipImportService.asmx?op=ImportPayslips'
            Assert.AreEqual("http://localhost/GatWs1_se/PayslipImportService.asmx?op=ImportPayslips", uIImportPayslipsHyperlink.Href);
        }

        /// <summary>
        /// CheckReshRosterService - Use 'CheckReshRosterServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckReshRosterService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIGetReshRosterHyperlink = this.UIDepartmentServiceV20Window5.UIReshRosterServiceWebDocument.UIContentPane.UIGetReshRosterHyperlink;
            #endregion

            // Verify that the 'InnerText' property of 'GetReshRoster' link equals 'GetReshRoster'
            Assert.AreEqual(this.CheckReshRosterServiceExpectedValues.UIGetReshRosterHyperlinkInnerText, uIGetReshRosterHyperlink.InnerText);

            // Verify that the 'Href' property of 'GetReshRoster' link equals 'http://localhost/GatWs1_se/ReshRosterService.asmx?op=GetReshRoster'
            Assert.AreEqual("http://localhost/GatWs1_se/ReshRosterService.asmx?op=GetReshRoster", uIGetReshRosterHyperlink.Href);
        }

        /// <summary>
        /// CheckRoleDepartmentServiceV20182 - Use 'CheckRoleDepartmentServiceV20182ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckRoleDepartmentServiceV20182_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIGetDepartmentRolesHyperlink = this.UIDepartmentServiceV20Window.UIRoleDepartmentServicDocument.UIContentPane.UIGetDepartmentRolesHyperlink;
            HtmlHyperlink uIGetDisplayRolesHyperlink = this.UIDepartmentServiceV20Window.UIRoleDepartmentServicDocument.UIContentPane.UIGetDisplayRolesHyperlink;
            #endregion

            // Verify that the 'InnerText' property of 'GetDepartmentRoles' link equals 'GetDepartmentRoles'
            Assert.AreEqual(this.CheckRoleDepartmentServiceV20182ExpectedValues.UIGetDepartmentRolesHyperlinkInnerText, uIGetDepartmentRolesHyperlink.InnerText);

            // Verify that the 'Href' property of 'GetDepartmentRoles' link equals 'http://localhost/GatWs1_se/RoleDepartmentServiceV20182.asmx?op=GetDepartmentRoles'
            Assert.AreEqual("http://localhost/GatWs1_se/RoleDepartmentServiceV20182.asmx?op=GetDepartmentRoles", uIGetDepartmentRolesHyperlink.Href);

            // Verify that the 'InnerText' property of 'GetDisplayRoles' link equals 'GetDisplayRoles'
            Assert.AreEqual(this.CheckRoleDepartmentServiceV20182ExpectedValues.UIGetDisplayRolesHyperlinkInnerText, uIGetDisplayRolesHyperlink.InnerText);

            // Verify that the 'Href' property of 'GetDisplayRoles' link equals 'http://localhost/GatWs1_se/RoleDepartmentServiceV20182.asmx?op=GetDisplayRoles'
            Assert.AreEqual("http://localhost/GatWs1_se/RoleDepartmentServiceV20182.asmx?op=GetDisplayRoles", uIGetDisplayRolesHyperlink.Href);
        }

        /// <summary>
        /// CheckSmsByMailReader - Use 'CheckSmsByMailReaderExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckSmsByMailReader_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIReadMailQueueAndCreaHyperlink = this.UIAppServiceWebServiceWindow1.UISmsByMailReaderWebSeDocument.UIContentPane.UIReadMailQueueAndCreaHyperlink;
            #endregion

            // Verify that the 'Href' property of 'ReadMailQueueAndCreateSms' link equals ''
            Assert.AreEqual("http://localhost/GatWs1_se/SmsByMailReader.asmx?op=ReadMailQueueAndCreateSms", uIReadMailQueueAndCreaHyperlink.Href);
        }

        /// <summary>
        /// CheckSmsGatewayService - Use 'CheckSmsGatewayServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckSmsGatewayService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIAddToInboxHyperlink = this.UIDepartmentServiceV20Window6.UISmsGatewayServiceWebDocument.UIContentPane.UIAddToInboxHyperlink;
            HtmlHyperlink uIGetMessagesInOutboxHyperlink = this.UIDepartmentServiceV20Window6.UISmsGatewayServiceWebDocument.UIContentPane.UIGetMessagesInOutboxHyperlink;
            HtmlHyperlink uIGetUnsentMessagesHyperlink = this.UIDepartmentServiceV20Window6.UISmsGatewayServiceWebDocument.UIContentPane.UIGetUnsentMessagesHyperlink;
            HtmlHyperlink uIMarkMessageHyperlink = this.UIDepartmentServiceV20Window6.UISmsGatewayServiceWebDocument.UIContentPane.UIMarkMessageHyperlink;
            HtmlHyperlink uIMessageCleanUpHyperlink = this.UIDepartmentServiceV20Window6.UISmsGatewayServiceWebDocument.UIContentPane.UIMessageCleanUpHyperlink;
            #endregion

            // Verify that the 'InnerText' property of 'AddToInbox' link equals 'AddToInbox'
            Assert.AreEqual(this.CheckSmsGatewayServiceExpectedValues.UIAddToInboxHyperlinkInnerText, uIAddToInboxHyperlink.InnerText);

            // Verify that the 'Href' property of 'AddToInbox' link equals '
            Assert.AreEqual("http://localhost/GatWs1_se/SmsGatewayService.asmx?op=AddToInbox", uIAddToInboxHyperlink.Href);

            // Verify that the 'Href' property of 'GetMessagesInOutbox' link equals ''
            Assert.AreEqual("http://localhost/GatWs1_se/SmsGatewayService.asmx?op=GetMessagesInOutbox", uIGetMessagesInOutboxHyperlink.Href);

            // Verify that the 'Href' property of 'GetUnsentMessages' link equals ''
            Assert.AreEqual("http://localhost/GatWs1_se/SmsGatewayService.asmx?op=GetUnsentMessages", uIGetUnsentMessagesHyperlink.Href);

            // Verify that the 'Href' property of 'MarkMessage' link equals ''
            Assert.AreEqual("http://localhost/GatWs1_se/SmsGatewayService.asmx?op=MarkMessage", uIMarkMessageHyperlink.Href);

            // Verify that the 'Href' property of 'MessageCleanUp' link equals ''
            Assert.AreEqual("http://localhost/GatWs1_se/SmsGatewayService.asmx?op=MessageCleanUp", uIMessageCleanUpHyperlink.Href);
        }

        public void TesService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIGetWorkShiftDataHyperlink = this.UIDepartmentServiceV20Window7.UITesServiceWebServiceDocument.UIContentPane.UIGetWorkShiftDataHyperlink;
            #endregion

            // Verify that the 'InnerText' property of 'GetWorkShiftData' link equals 'GetWorkShiftData'
            Assert.AreEqual(this.TesServiceExpectedValues.UIGetWorkShiftDataHyperlinkInnerText, uIGetWorkShiftDataHyperlink.InnerText);

            // Verify that the 'Href' property of 'GetWorkShiftData' link equals 'http://localhost/GatWs1/TesService.asmx?op=GetWorkShiftData'
            Assert.AreEqual("http://localhost/GatWs1_se/TesService.asmx?op=GetWorkShiftData", uIGetWorkShiftDataHyperlink.Href);
        }

        /// <summary>
        /// CheckSmsService - Use 'CheckSmsServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckSmsService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIProcessSmsQueuesHyperlink = this.UIAppServiceWebServiceWindow2.UISmsServiceWebServiceDocument.UIContentPane.UIProcessSmsQueuesHyperlink;
            #endregion

            // Verify that the 'Href' property of 'ProcessSmsQueues' link equals 'http://localhost/GatWs1_se/SmsService.asmx?op=ProcessSmsQueues'
            Assert.AreEqual("http://localhost/GatWs1_se/SmsService.asmx?op=ProcessSmsQueues", uIProcessSmsQueuesHyperlink.Href);
        }

        /// <summary>
        /// CheckSystemInformationService - Use 'CheckSystemInformationServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckSystemInformationService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIGetAliveStatusHyperlink = this.UIAppServiceWebServiceWindow3.UISystemInformationSerDocument.UIContentPane.UIGetAliveStatusHyperlink;
            #endregion

            // Verify that the 'Href' property of 'GetAliveStatus' link equals ''
            Assert.AreEqual("http://localhost/GatWs1_se/SystemInformationService.asmx?op=GetAliveStatus", uIGetAliveStatusHyperlink.Href);
        }

        /// <summary>
        /// CheckTaskAgreementService - Use 'CheckTaskAgreementServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckTaskAgreementService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIUpdateTaskAgreementHyperlink = this.UIAppServiceWebServiceWindow4.UITaskAgreementServiceDocument.UIContentPane.UIUpdateTaskAgreementHyperlink;
            #endregion

            // Verify that the 'Href' property of 'UpdateTaskAgreement' link equals 'http://localhost/GatWs1_se/TaskAgreementService.asmx?op=UpdateTaskAgreement'
            Assert.AreEqual("http://localhost/GatWs1_se/TaskAgreementService.asmx?op=UpdateTaskAgreement", uIUpdateTaskAgreementHyperlink.Href);
        }

        /// <summary>
        /// CheckTestService - Use 'CheckTestServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckTestService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uITestDatabaseConnectiHyperlink = this.UIAppServiceWebServiceWindow5.UITestServiceWebServicDocument.UIContentPane.UITestDatabaseConnectiHyperlink;
            #endregion

            // Verify that the 'Href' property of 'TestDatabaseConnection' link equals ''
            Assert.AreEqual("http://localhost/GatWs1_se/TestService.asmx?op=TestDatabaseConnection", uITestDatabaseConnectiHyperlink.Href);
        }

        /// <summary>
        /// CheckTimeregImport - Use 'CheckTimeregImportExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckTimeregImport_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIImportTimeregRegistrHyperlink = this.UIGatWebservices2MainPWindow.UITimeregImportWebServDocument.UIContentPane.UIImportTimeregRegistrHyperlink;
            #endregion

            // Verify that the 'Href' property of 'ImportTimeregRegistration' link equals 'http://localhost/GatWs2_se/TimeregImport.asmx?op=ImportTimeregRegistration'
            Assert.AreEqual("http://localhost/GatWs2_se/TimeregImport.asmx?op=ImportTimeregRegistration", uIImportTimeregRegistrHyperlink.Href);
        }

        /// <summary>
        /// CheckUniqueService - Use 'CheckUniqueServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckUniqueService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIProfilturnusFaaPersoHyperlink = this.UIGatWebservices2MainPWindow.UIUniqueServiceWebServDocument.UIContentPane.UIProfilturnusFaaPersoHyperlink;
            HtmlHyperlink uIProfilturnusFaaStillHyperlink = this.UIGatWebservices2MainPWindow.UIUniqueServiceWebServDocument.UIContentPane.UIProfilturnusFaaStillHyperlink;
            HtmlHyperlink uIProfilturnusFaaVaktpHyperlink = this.UIGatWebservices2MainPWindow.UIUniqueServiceWebServDocument.UIContentPane.UIProfilturnusFaaVaktpHyperlink;
            HtmlHyperlink uIProfilturnus_functioHyperlink = this.UIGatWebservices2MainPWindow.UIUniqueServiceWebServDocument.UIContentPane.UIProfilturnus_functioHyperlink;
            #endregion

            // Verify that the 'Href' property of 'ProfilturnusFaaPerson' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/UniqueService.asmx?op=ProfilturnusFaaPerson", uIProfilturnusFaaPersoHyperlink.Href);

            // Verify that the 'Href' property of 'ProfilturnusFaaStilling' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/UniqueService.asmx?op=ProfilturnusFaaStilling", uIProfilturnusFaaStillHyperlink.Href);

            // Verify that the 'Href' property of 'ProfilturnusFaaVaktplan' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/UniqueService.asmx?op=ProfilturnusFaaVaktplan", uIProfilturnusFaaVaktpHyperlink.Href);

            // Verify that the 'Href' property of 'profilturnus_function' link equals ''
            Assert.AreEqual("http://localhost/GatWs2_se/UniqueService.asmx?op=profilturnus_function", uIProfilturnus_functioHyperlink.Href);
        }

        /// <summary>
        /// CheckWeaBreakService - Use 'CheckWeaBreakServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckWeaBreakService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uICalculateWeaAndSaveWHyperlink = this.UIAppServiceWebServiceWindow6.UIWeaBreakServiceWebSeDocument.UIContentPane.UICalculateWeaAndSaveWHyperlink;
            HtmlHyperlink uICalculateWeaAndSaveWHyperlink1 = this.UIAppServiceWebServiceWindow6.UIWeaBreakServiceWebSeDocument.UIContentPane.UICalculateWeaAndSaveWHyperlink1;
            HtmlHyperlink uICalculateWeaAndSaveWHyperlink2 = this.UIAppServiceWebServiceWindow6.UIWeaBreakServiceWebSeDocument.UIContentPane.UICalculateWeaAndSaveWHyperlink2;
            #endregion

            // Verify that the 'Href' property of 'CalculateWeaAndSaveWeaBreaksForEmployee' link equals 'http://localhost/GatWs1_se/WeaBreakService.asmx?op=CalculateWeaAndSaveWeaBreaksForEmployee'
            Assert.AreEqual("http://localhost/GatWs1_se/WeaBreakService.asmx?op=CalculateWeaAndSaveWeaBreaksForEmployee", uICalculateWeaAndSaveWHyperlink.Href);

            // Verify that the 'Href' property of 'CalculateWeaAndSaveWeaBreaksOverrideScheduler' link equals 'http://localhost/GatWs1_se/WeaBreakService.asmx?op=CalculateWeaAndSaveWeaBreaksOverrideScheduler'
            Assert.AreEqual("http://localhost/GatWs1_se/WeaBreakService.asmx?op=CalculateWeaAndSaveWeaBreaksOverrideScheduler", uICalculateWeaAndSaveWHyperlink1.Href);

            // Verify that the 'Href' property of 'CalculateWeaAndSaveWeaBreaksUsingScheduler' link equals 'http://localhost/GatWs1_se/WeaBreakService.asmx?op=CalculateWeaAndSaveWeaBreaksUsingScheduler'
            Assert.AreEqual("http://localhost/GatWs1_se/WeaBreakService.asmx?op=CalculateWeaAndSaveWeaBreaksUsingScheduler", uICalculateWeaAndSaveWHyperlink2.Href);
        }

        public void CheckCommunicationQueueService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIFillMessageQueueWithHyperlink = this.UIGatWebservices2MainPWindow.UICommunicationQueueSeDocument.UIContentPane.UIFillMessageQueueWithHyperlink;
            HtmlHyperlink uIFillMessageQueueWithHyperlink1 = this.UIGatWebservices2MainPWindow.UICommunicationQueueSeDocument.UIContentPane.UIFillMessageQueueWithHyperlink1;
            HtmlHyperlink uIProcessMailQueueHyperlink = this.UIGatWebservices2MainPWindow.UICommunicationQueueSeDocument.UIContentPane.UIProcessMailQueueHyperlink;
            #endregion

            // Verify that the 'Href' property of 'FillMessageQueueWithDashboardWarnings' link equals 'http://localhost/GatWs2_se/CommunicationQueueService.asmx?op=FillMessageQueueWithDashboardWarnings'
            Assert.AreEqual("http://localhost/GatWs2_se/CommunicationQueueService.asmx?op=FillMessageQueueWithDashboardWarnings", uIFillMessageQueueWithHyperlink.Href);

            // Verify that the 'Href' property of 'FillMessageQueueWithDashboardWarningsOverrideSched...' link equals 'http://localhost/GatWs2_se/CommunicationQueueService.asmx?op=FillMessageQueueWithDashboardWarningsOverrideScheduler'
            Assert.AreEqual("http://localhost/GatWs2_se/CommunicationQueueService.asmx?op=FillMessageQueueWithDashboardWarningsOverrideScheduler", uIFillMessageQueueWithHyperlink1.Href);

            // Verify that the 'Href' property of 'ProcessMailQueue' link equals 'http://localhost/GatWs2_se/CommunicationQueueService.asmx?op=ProcessMailQueue'
            Assert.AreEqual("http://localhost/GatWs2_se/CommunicationQueueService.asmx?op=ProcessMailQueue", uIProcessMailQueueHyperlink.Href);
        }

        /// <summary>
        /// CheckWishPlanWebService - Use 'CheckWishPlanWebServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckWishPlanWebService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIGetAdditionalDataHyperlink = this.UIGatWebservices2MainPWindow.UIWishPlanWebServiceWeDocument.UIContentPane.UIGetAdditionalDataHyperlink;
            HtmlHyperlink uIGetDemandHyperlink = this.UIGatWebservices2MainPWindow.UIWishPlanWebServiceWeDocument.UIContentPane.UIGetDemandHyperlink;
            HtmlHyperlink uIGetPlanHyperlink = this.UIGatWebservices2MainPWindow.UIWishPlanWebServiceWeDocument.UIContentPane.UIGetPlanHyperlink;
            HtmlHyperlink uISavePlanHyperlink = this.UIGatWebservices2MainPWindow.UIWishPlanWebServiceWeDocument.UIContentPane.UISavePlanHyperlink;
            #endregion

            // Verify that the 'Href' property of 'GetAdditionalData' link equals 'http://localhost/GatWs2_se/WishPlanWebService.asmx?op=GetAdditionalData'
            Assert.AreEqual("http://localhost/GatWs2_se/WishPlanWebService.asmx?op=GetAdditionalData", uIGetAdditionalDataHyperlink.Href);

            // Verify that the 'Href' property of 'GetDemand' link equals 'http://localhost/GatWs2_se/WishPlanWebService.asmx?op=GetDemand'
            Assert.AreEqual("http://localhost/GatWs2_se/WishPlanWebService.asmx?op=GetDemand", uIGetDemandHyperlink.Href);

            // Verify that the 'Href' property of 'GetPlan' link equals 'http://localhost/GatWs2_se/WishPlanWebService.asmx?op=GetPlan'
            Assert.AreEqual("http://localhost/GatWs2_se/WishPlanWebService.asmx?op=GetPlan", uIGetPlanHyperlink.Href);

            // Verify that the 'Href' property of 'SavePlan' link equals 'http://localhost/GatWs2_se/WishPlanWebService.asmx?op=SavePlan'
            Assert.AreEqual("http://localhost/GatWs2_se/WishPlanWebService.asmx?op=SavePlan", uISavePlanHyperlink.Href);
        }
        public void CheckAppService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIProcessAppFlexQueueHyperlink = this.UIAppServiceWebServiceWindow.UIAppServiceWebServiceDocument.UIContentPane.UIProcessAppFlexQueueHyperlink;
            #endregion

            // Verify that the 'InnerText' property of 'ProcessAppFlexQueue' link equals 'ProcessAppFlexQueue'
            Assert.AreEqual(this.CheckAppServiceExpectedValues.UIProcessAppFlexQueueHyperlinkInnerText, uIProcessAppFlexQueueHyperlink.InnerText);

            // Verify that the 'Href' property of 'ProcessAppFlexQueue' link equals 'http://localhost/GatWs1/AppService.asmx?op=ProcessAppFlexQueue'
            Assert.AreEqual("http://localhost/GatWs1_se/AppService.asmx?op=ProcessAppFlexQueue", uIProcessAppFlexQueueHyperlink.Href);
        }
        public void CheckBussinesAnalyzeService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIExecuteBusinessFunctHyperlink = this.UIAppServiceWebServiceWindow.UIBussinesAnalyzeServiDocument.UIContentPane.UIExecuteBusinessFunctHyperlink;
            HtmlHyperlink uIUpdateBussinessAnalyHyperlink = this.UIAppServiceWebServiceWindow.UIBussinesAnalyzeServiDocument.UIContentPane.UIUpdateBussinessAnalyHyperlink;
            #endregion

            // Verify that the 'Href' property of 'ExecuteBusinessFunctionForShiftsOverrideScheduler' link equals 'http://localhost/GatWs1/BussinesAnalyzeService.asmx?op=ExecuteBusinessFunctionForShiftsOverrideScheduler'
            Assert.AreEqual("http://localhost/GatWs1_se/BussinesAnalyzeService.asmx?op=ExecuteBusinessFunctionForShiftsOverrideScheduler", uIExecuteBusinessFunctHyperlink.Href);

            // Verify that the 'Href' property of 'UpdateBussinessAnalyzeData' link equals 'http://localhost/GatWs1/BussinesAnalyzeService.asmx?op=UpdateBussinessAnalyzeData'
            Assert.AreEqual("http://localhost/GatWs1_se/BussinesAnalyzeService.asmx?op=UpdateBussinessAnalyzeData", uIUpdateBussinessAnalyHyperlink.Href);
        }
        public void CheckCalendarIntegrationWebService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIDistributeHyperlink = this.UIAppServiceWebServiceWindow.UICalendarIntegrationWDocument.UIContentPane.UIDistributeHyperlink;
            #endregion

            // Verify that the 'Href' property of 'Distribute' link equals 'http://localhost/GatWs1/CalendarIntegrationWebService.asmx?op=Distribute'
            Assert.AreEqual("http://localhost/GatWs1_se/CalendarIntegrationWebService.asmx?op=Distribute", uIDistributeHyperlink.Href);
        }

        public void CheckCalendarService_SE()
        {
            #region Variable Declarations
            HtmlHyperlink uIGetJourShiftsHyperlink = this.UIAppServiceWebServiceWindow.UICalendarServiceWebSeDocument.UIContentPane.UIGetJourShiftsHyperlink;
            #endregion

            // Verify that the 'Href' property of 'GetJourShifts' link equals 'http://localhost/GatWs1/CalendarService.asmx?op=GetJourShifts'
            Assert.AreEqual("http://localhost/GatWs1_se/CalendarService.asmx?op=GetJourShifts", uIGetJourShiftsHyperlink.Href);
        }
        #endregion
    }
}
