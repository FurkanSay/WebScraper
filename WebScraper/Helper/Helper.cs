using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.Helpers
{
    public static class Helpers
    {
        //Attirube
        //Class
        //TagName
        //CssSelector
        //
        public static IWebElement SearchChildElement(ChromeDriver driver,IWebElement main,By by)
        {
            try
            {
                var webElements = main.FindElements(By.XPath(".//*"));
                var webElement = webElements.Select(x => x.FindElement(by)).FirstOrDefault();
                if (webElement == null) return SearchChildElement(driver, webElement, by);
                else return webElement;
            }catch (Exception ex)
            {
                return SearchChildElement(driver, null, by);
            }
        }
    }
}
