using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Custom_Model;


namespace WebScraper.n11
{
    public class DataProcess
    {
        private readonly ChromeDriver driver;
        public List<CustomModel> customModels;

        public DataProcess(ChromeDriver driver)
        {
            this.driver = driver;

        }
        public async Task<List<CustomModel>> Process(string searchText)
        {
            customModels = new List<CustomModel>();
            //Urun Ara anahtar-model-marka
            int i = 1;
            while (true)
            {
                driver.Navigate().GoToUrl($"https://www.n11.com/arama?q={searchText}&pg={i}");


                IWebElement productElement = driver.FindElement(By.CssSelector("#listingUl"));
                IList<IWebElement> productElements = productElement.FindElements(By.CssSelector(".column"));
                IWebElement pagination = driver.FindElement(By.CssSelector("#contentListing > div > div.listingHolder > div.productArea > div.pagination"));

                IList<IWebElement> childElements = pagination.FindElements(By.XPath(".//*"));
                var paginationStr = string.Join("", childElements.Select(x => x.GetAttribute("class")).ToList());

               

                foreach (var item in productElements)
                {
                    try
                    {
                        var test = item.Text;
                        var productName = item.FindElement(By.CssSelector("div.columnContent > div > a > h3"));
                        var BuyingPrice = item.FindElement(By.CssSelector("div.columnContent > div > div > div.priceContainer > div > span.newPrice.cPoint.priceEventClick > ins"));
                        var CategoryName = item.FindElement(By.CssSelector("div.columnContent > div > span"));
                        var link = item.FindElement(By.CssSelector("div.columnContent > div > a"));
                        var customModel = new CustomModel()
                        {
                            BrandName = "",
                            BuyingPrice = BuyingPrice.Text,
                            CategoryName = CategoryName.GetAttribute("data-category-name"),
                            ProductName = productName.Text,
                            Website = link.GetAttribute("href")
                        };
                        customModels.Add(customModel);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                if (i == 3) break;
                if (paginationStr.Replace(" ", "").Contains("activelast")) break;
                i++;
            }
            return customModels;
            //Button tıkla
            // IWebElement input = driver.FindElement(By.CssSelector("#captchacharacters"));
            // button.Click();
            //Yazı Yaz
            // input.SendKeys()
            //Seç
            //Attritube value Al
            //string srcValue = searchbox.GetAttribute("src");
            //HTML Al


        }
    }
}
