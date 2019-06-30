using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using System;

namespace TheInternet.Tests
{
    public class Tests
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void ShouldPass()
        {
            // Arrange
            var expectedTitle = "A/B Test";


            // Lasciare qui, altrimenti  se lo mettiamo nello StartUp perdimo i riferimenti
            driver.Url = "http://the-internet.herokuapp.com/";

            // Act
            var link = driver.FindElement(By.LinkText("A/B Testing"));

            link.Click();

            var title = driver.FindElement(By.TagName("h3"));

            // Assert
            Assert.That(title.Text.Contains(expectedTitle));

        }

        [Test]
        public void TextShouldBeABTest()
        {
            // Arrange
            var expectedText = "Also known as split testing. This is a way in which businesses are able to simultaneously test and learn different versions of a page to see which text and/or functionality works best towards a desired outcome (e.g. a user action such as a click-through).";

            driver.Url = "http://the-internet.herokuapp.com/";

            // Act
            var link = driver.FindElement(By.LinkText("A/B Testing")); // fragile

            link.Click();

            var text = driver.FindElement(By.TagName("p")); // fragile

            // Assert
            Assert.AreEqual(expectedText, text.Text);
        }

        [Test]
        public void ShouldCheckbox()
        {
            driver.Url = "http://the-internet.herokuapp.com/";

            var link = driver.FindElement(By.LinkText("Checkboxes"));

            link.Click();

            var checkboxes = driver.FindElements(By.CssSelector("input[type=checkbox]"));

            var firstCheckbox = checkboxes[0];

            Assert.IsFalse(firstCheckbox.Selected);

            var secondCheckbox = checkboxes[1];

            Assert.IsTrue(secondCheckbox.Selected);

            firstCheckbox.Click();

            Assert.IsTrue(firstCheckbox.Selected);

            secondCheckbox.Click();

            Assert.IsFalse(secondCheckbox.Selected);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void TestLogin()
        {
            driver.Url = "https://www.euclid-test.euclid.fresenius.de/EuCliD.stable.CZ";

            var loginUsername = driver.FindElement(By.Id("txtUserName"));
            var loginPassword = driver.FindElement(By.Id("txtPassword"));
            var loginButton = driver.FindElement(By.Id("btnSubmit"));

            loginUsername.SendKeys("3l-aarpini");
            loginPassword.SendKeys("4;|u[kX}");

            loginButton.Click();

            var clinicsLink = driver.FindElements(By.LinkText("06603 - Fresenius NephroCare, Sokolov"));

            foreach (IWebElement clinicLink in clinicsLink)
            {
                if (clinicLink.GetAttribute("onclick").Contains("Medical"))
                {
                    clinicLink.Click();
                    break;
                }
            }


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("loading")));

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ShowMsg")));

            var closePopUp = driver.FindElement(By.Id("ShowMsg"));

            var closeButton = closePopUp.FindElement(By.TagName("button"));

            closeButton.Click();

            var euclidLogo = driver.FindElement(By.CssSelector("div[class='login-Log Logo-Test']"));

            Assert.IsNotNull(euclidLogo);

            // closePopUp.Click();
        }
    }
}