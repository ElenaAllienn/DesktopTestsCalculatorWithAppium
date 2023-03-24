using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System.Text.RegularExpressions;

namespace AppiumCalculatorTests
{
    public class WindowsCalculatorTests
    {
        //private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        private const string appiumServer = "http://[::1]:4723/wd/hub"; //IPv6
       // private const string appLocation = @"C:\Windows\System32\calc.exe";
        private const string appLocation = @"Microsoft.WindowsCalculator_8wekyb3d8bbwe!App"; //using Appname
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions appiumOptions;

        [OneTimeSetUp]
        public void OpenApplication()
        {
            //start Desktop using Appium Server
            this.appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, appLocation);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            //appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, appLocation);
            //appiumOptions.AddAdditionalCapability("PlatformName", "Windows");

            this.driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptions);

        }

        [OneTimeTearDown]
        public void CloseApplication()
        {
            driver.Quit();
        }

        [Test]
        public void Test_Sum_TwoPositiveNumbers()
        {
            //Arrange
            var buttonOne = driver.FindElementByAccessibilityId("num1Button");
            var buttonTwo = driver.FindElementByAccessibilityId("num2Button");
            var buttonPlus = driver.FindElementByAccessibilityId("plusButton");
            var buttonEqual = driver.FindElementByAccessibilityId("equalButton");
            var resultField = driver.FindElementByAccessibilityId("CalculatorResults");

            //Act
            buttonOne.Clear();
            buttonPlus.Clear();
            buttonTwo.Clear();
            buttonOne.Click();
            buttonPlus.Click();
            buttonTwo.Click();
            buttonEqual.Click();

            var result = resultField.Text;
            var resultValue = Regex.Match(result, @"\d+").Value;

            //Assert
            Assert.That(resultValue, Is.EqualTo("3"));
        }

    }
}