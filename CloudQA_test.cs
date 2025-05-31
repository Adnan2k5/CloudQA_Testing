using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestProject1
{


    public class CloudQA_test
    {
        private ChromeDriver driver;
        private WebDriverWait wait;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");
        }

        [Test]
        public void FnameTesting()
        {
            //Testing First Name field
            var fnameElements = FindElements(FnameElement());
            Assert.That(fnameElements, Is.Not.Null, "First Name field not found");
            fnameElements.Clear();
            fnameElements.SendKeys("John");
            Assert.That(fnameElements.GetAttribute("value"), Is.EqualTo("John"), "First Name value not set correctly");
            Assert.Pass("First Name field tested successfully!");
        }

        [Test]
        public void EmailTesting() 
        {
            //Testing Email field
            var emailElements = FindElements(EmailElement());
            Assert.That(emailElements, Is.Not.Null, "Email field not found");
            emailElements.Clear();
            emailElements.SendKeys("john@gmail.com");
            Assert.That(emailElements.GetAttribute("value"), Is.EqualTo("john@gmail.com"), "Email value not set correctly");
            Assert.Pass("Email field tested successfully!");
        }

        [Test]
        public void RadioButtonTesting()
        {
            //Testing Radio Button
            var radioButtonElements = FindElements(RadioButtonElement());
            Assert.That(radioButtonElements, Is.Not.Null, "Radio Button not found");
            if (!radioButtonElements.Selected)
            {
                radioButtonElements.Click();
            }
            Assert.That(radioButtonElements.Selected, Is.True, "Radio Button not selected");
            Assert.Pass("Radio Button tested successfully!");
        }

        private IWebElement FindElements(List<By> locators)
        {
            foreach (var i in locators)
            {
                try
                {
                    return wait.Until(d =>
                    {
                        var elements = d.FindElements(i);
                        return elements.Count > 0 && elements[0].Displayed && elements[0].Enabled ? elements[0] : null;
                    });
                }
                catch (Exception)
                {
                    continue;
                }
            }
            throw new NoSuchElementException("Element not found!");
        }

        private static List<By> FnameElement()
        {
            return
            [
                By.Name("First Name"),
                By.XPath("//input[@id='fname']"),
                By.XPath("//input[@placeholder='Name']"),
                By.XPath("//input[contains(@class, 'form-control') and @placeholder='Name']"),
                By.CssSelector("input[id='fname']"),
                By.CssSelector("input[name='First Name']"),
                By.XPath("//label[contains(text(), 'First Name')]/following-sibling::input"),
                By.XPath("//label[@for='fname']/following-sibling::input")
            ];
        }

        private static List<By> EmailElement()
        {
            return [
                By.Id("email"),
                By.Name("Email"),
                By.XPath("//input[@id='email']"),
                By.XPath("//input[@placeholder='Email']"),
                By.XPath("//input[contains(@class, 'form-control') and @placeholder='Email']"),
                By.CssSelector("input[id='email']"),
                By.CssSelector("input[name='Email']"),
                By.XPath("//label[contains(text(), 'Email')]/following-sibling::input"),
                By.XPath("//label[@for='email']/following-sibling::input")
            ];
        }

        private static List<By> RadioButtonElement()
        {
            return [
                By.Id("male"),
                By.XPath("//input[@id='male']"),
                By.XPath("//input[@type='radio' and @value='Male']"),
                By.XPath("//input[@name='gender' and @value='Male']"),
                By.CssSelector("input[id='male']"),
                By.CssSelector("input[value='Male']"),
                By.XPath("//span[contains(text(), 'Male')]/preceding-sibling::input"),
                By.XPath("//input[@type='radio'][1]")
            ];
        }
        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}


        
    
    