namespace _099_Test_Distribusjon_Leveranse.UIMapVS2015Classes
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
    using System.Diagnostics;
    using System.Drawing.Imaging;
    using System.IO;
    using CommonTestData;

    public partial class UIMapVS2015
    {
        private string ImagePath;
        private TestContext TestContext;
        
        public UIMapVS2015(TestContext testContext)
        {
            TestContext = testContext;
            ImagePath = Path.Combine(TestData.GetWorkingDirectory, @"Reports\Test99\");
        }

        public void LoginMinGat()
        {
            Playback.Wait(3000);
            CheckForCertificateError(true);

            UIMinGatMinGatv6604403Window.WaitForControlExist(600000);
            LoginNew();
        }

        public void CheckForCertificateError(bool first)
        {
            Playback.Wait(3000);
            try
            {
                if (UICertificateErrorNaviWindow.Exists)
                {
                    if (first)
                        IgnoreCertificateError();
                    else
                        IgnoreCertificateError2();
                }
            }
            catch (Exception)
            {
                TestContext.WriteLine("Unable to find CertificateError window");
            }

        }

        /// <summary>
        /// CheckHome - Use 'CheckHomeExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckHome_se()
        {
            #region Variable Declarations
            HtmlCustom uIPageTitleCustom = this.UIStartsidenMinGatv653Window.UIStartsidenMinGatv653Document1.UIPageTitleCustom;
            #endregion

            // Verify that the 'InnerText' property of 'pageTitle' custom control equals 'Startsiden'
            Assert.AreEqual("Startsidan", uIPageTitleCustom.InnerText, "Feil tekst");
        }

        /// <summary>
        /// CheckMySalary - Use 'CheckMySalaryExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckMySalary_se()
        {
            #region Variable Declarations
            HtmlCustom uIPageTitleCustom = this.UIStartsidenMinGatv653Window.UIMinlønnsoversiktMinGDocument.UIPageTitleCustom;
            #endregion

            // Verify that the 'InnerText' property of 'pageTitle' custom control equals 'Min lønnsoversikt'
            Assert.AreEqual("Min löneöversikt", uIPageTitleCustom.InnerText, "Feil tekst");
        }

        /// <summary>
        /// CheckReminders - Use 'CheckRemindersExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckReminders_se()
        {
            #region Variable Declarations
            HtmlCustom uIPageTitleCustom = this.UIStartsidenMinGatv653Window.UIPåminnelserMinGatv65Document.UIPageTitleCustom;
            #endregion

            // Verify that the 'InnerText' property of 'pageTitle' custom control equals 'Påminnelser'
            Assert.AreEqual(this.CheckRemindersExpectedValues.UIPageTitleCustomInnerText, uIPageTitleCustom.InnerText, "Feil text");
        }

        /// <summary>
        /// CheckSelfService - Use 'CheckSelfServiceExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckSelfService_se()
        {
            #region Variable Declarations
            HtmlCustom uIPageTitleCustom = this.UIStartsidenMinGatv653Window.UIForespørslerMinGatv6Document.UIPageTitleCustom;
            #endregion

            // Verify that the 'InnerText' property of 'pageTitle' custom control equals 'Forespørsler'
            Assert.AreEqual("Förfrågan", uIPageTitleCustom.InnerText, "Feil navn");
        }

        public void CheckBanks_se()
        {
            #region Variable Declarations
            HtmlCustom uIPageTitleCustom = this.UIStartsidenMinGatv653Window.UIMinebankerMinGatv653Document.UIPageTitleCustom;
            #endregion

            // Verify that the 'InnerText' property of 'pageTitle' custom control equals 'Mine banker'
            Assert.AreEqual("Mina banker", uIPageTitleCustom.InnerText, "Feil tekst");
        }

        /// <summary>
        /// CheckShiftbook - Use 'CheckShiftbookExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckShiftbook_se()
        {
            #region Variable Declarations
            HtmlCustom uIPageTitleCustom = this.UIStartsidenMinGatv653Window.UIVaktbokMinGatv653438Document.UIPageTitleCustom;
            #endregion

            // Verify that the 'InnerText' property of 'pageTitle' custom control equals 'Vaktbok'
            Assert.AreEqual("Personalbok", uIPageTitleCustom.InnerText, "Feil tekst");
        }

        public void CheckCalendar_se()
        {
            #region Variable Declarations
            HtmlCustom uIPageTitleCustom = this.UIStartsidenMinGatv653Window.UIMinkalenderMinGatv65Document.UIPageTitleCustom;
            #endregion

            // Verify that the 'InnerText' property of 'pageTitle' custom control equals 'Min kalender'
            Assert.AreEqual(this.CheckCalendarExpectedValues.UIPageTitleCustomInnerText, uIPageTitleCustom.InnerText, "Feil tekst");
        }

        /// <summary>
        /// CheckShowWeek - Use 'CheckShowWeekExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckShowWeek_se()
        {
            #region Variable Declarations
            HtmlCustom uIPageTitleCustom = this.UIStartsidenMinGatv653Window.UIUkevisningMinGatv653Document.UIPageTitleCustom;
            #endregion

            // Verify that the 'InnerText' property of 'pageTitle' custom control equals 'Ukevisning'
            Assert.AreEqual("Veckovisning", uIPageTitleCustom.InnerText, "Feil tekst");
        }

        /// <summary>
        /// CheckTasks - Use 'CheckTasksExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckTasks_se()
        {
            #region Variable Declarations
            HtmlCustom uIPageTitleCustom = this.UIStartsidenMinGatv653Window.UIOppgaveoversiktMinGaDocument.UIPageTitleCustom;
            #endregion

            // Verify that the 'InnerText' property of 'pageTitle' custom control equals 'Oppgaveoversikt'
            Assert.AreEqual("Uppgifter-tid", uIPageTitleCustom.InnerText, "Feil tekst");
        }

        /// <summary>
        /// CheckWishplan - Use 'CheckWishplanExpectedValues' to pass parameters into this method.
        /// </summary>
        public void CheckWishplan_se()
        {
            #region Variable Declarations
            WinText uIØnskeplanforBASEPLANText = this.UIMinønskeplanMinGatv6Window.UIItemWindow.UISilverlightControlWindow.UIØnskeplanforBASEPLANText;
            WinClient uIFerie5dgrukeClient = this.UIMinønskeplanMinGatv6Window.UIItemWindow.UIItemListItem.UIFerie5dgrukeClient;
            #endregion

            // Verify that the 'Name' property of 'Ønskeplan for BASEPLAN TIL AUTOTEST' label equals 'Ønskeplan for BASEPLAN TIL AUTOTEST'
            Assert.AreEqual(this.CheckWishplanExpectedValues.UIØnskeplanforBASEPLANTextName, uIØnskeplanforBASEPLANText.Name, "Feil i baseplan");

            // Verify that the 'Name' property of 'Ferie - 5 dgr/uke' client equals 'Ferie - 5 dgr/uke'
            Assert.AreEqual(this.CheckWishplanExpectedValues.UIFerie5dgrukeClientName, uIFerie5dgrukeClient.Name, "Feil i feriebank");
        }

        public void WishplanSnapshot()
        {
            #region Variable Declarations
            WinList uIWishPlanCalendarItemList = this.UIMinønskeplanMinGatv6Window.UIItemWindow3.UISilverlightControlWindow.UIWishPlanCalendarItemList;
            #endregion
            
            var timeFormat = DateTime.Now.Date.ToString("ddMMyyyy") + "_" + DateTime.Now.TimeOfDay.ToString("hh\\mm");

            //if (!CaptureControlScreenDump(uIWishPlanCalendarItemList, ImagePath + "WhishPlan_" + timeFormat + ".jpg"))
                CaptureScreenDump(ImagePath + "WhishPlanWindow_" + timeFormat + ".jpg");

        }
        
        private bool CaptureControlScreenDump(WinList control, string image)
        {
            try
            {
                control.WaitForControlExist(30000);
                Image controlImage = control.CaptureImage();
                controlImage.Save(image, ImageFormat.Jpeg);
                TestContext.WriteLine("Controlscreendump: OK");

                return true;
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error capturing image: " + image + ": " + ex.Message);
                return false;
            }
        }

        private bool CaptureScreenDump(string image)
        {
            try
            {
                Playback.Wait(5000);
                Image desktopImage = UITestControl.Desktop.CaptureImage();
                desktopImage.Save(image, ImageFormat.Jpeg);
                TestContext.WriteLine("Fullscreendump: OK");

                return true;
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Error capturing image: " + image + ": " + ex.Message);
                return false;
            }
        }
    }
}
