using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumCalculatorTests
{
    public class CalculatorTests
    {
        //private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        private const string appiumServer = "http://[::1]:4723/wd/hub"; //IPv6
        private const string appLocation = @"C:\Applications\04.Appium-Desktop-Testing-Resources\SummatorDesktopApp.exe";
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions appiumOptions;
        //private AppiumLocalService appiumLocalService;

        [OneTimeSetUp]
        public void OpenApplication()
        {
            //start Desktop using Appium Server
            this.appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", appLocation);
            //appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, appLocation);
            appiumOptions.AddAdditionalCapability("PlatformName", "Windows");
            this.driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptions);

            //start Desktop using AppiumLocalService (headless mode)
            //this.appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            //appiumLocalService.Start();
            //this.appiumOptions = new AppiumOptions();
            //appiumOptions.AddAdditionalCapability("app", appLocation);
            //appiumOptions.AddAdditionalCapability("PlatformName", "Windows");
            //this.driver = new WindowsDriver<WindowsElement>(appiumLocalService, appiumOptions);
        }

        [OneTimeTearDown]
        public void CloseApplication()
        {
            driver.Quit();
           // appiumLocalService.Dispose();//by using local mode
        }

        [Test]
        public void Test_Sum_TwoPositiveNumbers()
        {
            //Arrange
            var firstField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            var secondField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            var resultField = driver.FindElementByAccessibilityId("textBoxSum");
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");

            //Act
            firstField.Clear();
            secondField.Clear();
            firstField.SendKeys("5");
            secondField.SendKeys("15");
            calcButton.Click();

            //Assert
            Assert.That(resultField.Text, Is.EqualTo("20"));
        }

        [Test]
        public void Test_Sum_InvalidNumbers()
        {
            //Arrange
            var firstField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            var secondField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            var resultField = driver.FindElementByAccessibilityId("textBoxSum");
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");

            //Act
            firstField.Clear();
            secondField.Clear();
            firstField.SendKeys("alabala");
            secondField.SendKeys("15");
            calcButton.Click();

            //Assert
            Assert.That(resultField.Text, Is.EqualTo("error"));
        }

        [TestCase("5", "15", "20")]
        [TestCase("-5", "-15", "-20")]
        [TestCase("5", "alabala", "error")]
        public void Test_CalculateNumbers(string firstNumber, string secondNumber, string result)
        {
            //Arrange
            var firstField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            var secondField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            var resultField = driver.FindElementByAccessibilityId("textBoxSum");
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");

            //Act
            firstField.Clear();
            secondField.Clear();
            firstField.SendKeys(firstNumber);
            secondField.SendKeys(secondNumber);
            calcButton.Click();

            //Assert
            Assert.That(resultField.Text, Is.EqualTo(result));
        }
    }
}