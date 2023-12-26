using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Custom_Model;
using Helpers = WebScraper.Helpers;
using OpenQA.Selenium.Support.UI;

namespace WebScraper.Amazon
{
    public class DataProcess
    {
        private readonly ChromeDriver driver;
        private readonly JumpAuthetication jump;
        public List<CustomModel> customModels;

        public DataProcess(ChromeDriver driver)
        {
            this.driver = driver;
            jump = new JumpAuthetication(driver);

        }
        public async Task<List<CustomModel>> Process(string searchText)
        {
            customModels = new List<CustomModel>();
            //Urun Ara anahtar-model-marka
            int i = 1;
            while (true)
            {
                driver.Navigate().GoToUrl($"https://www.amazon.com");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

               
                var jumptemp = await jump.Check();
                if (jumptemp.Item1) await jump.Jump(jumptemp.Item2);
                else
                {
                    driver.Navigate().GoToUrl($"https://www.amazon.com/s?k={searchText}&page={i}");
                    IWebElement productElement = driver.FindElement(By.CssSelector("#search > div.s-desktop-width-max.s-desktop-content.s-wide-grid-style-t1.s-opposite-dir.s-wide-grid-style.sg-row > div.sg-col-20-of-24.s-matching-dir.sg-col-16-of-20.sg-col.sg-col-8-of-12.sg-col-12-of-16 > div > span.rush-component.s-latency-cf-section > div.s-main-slot.s-result-list.s-search-results.sg-row"));
                    IList<IWebElement> productElements = productElement.FindElements(By.XPath(".//*"));
                    //*[@id="search"]/div[1]/div[1]/div/span[1]/div[1]/div[11]/div/div/span
                    IWebElement pagination = driver.FindElement(By.ClassName("s-pagination-strip"));

                    IList<IWebElement> childElements = pagination.FindElements(By.XPath(".//*"));
                    var paginationStr = string.Join("", childElements.Select(x => x.GetAttribute("aria-label")).ToList());



                    foreach (var item in productElements)
                    {
                        var check = item.GetAttribute("data-asin");
                        if (check != "" && check != default)
                        {
                            //                                                 div > div > div > div > span > div > div > div > div.puisg-col.puisg-col-4-of-12.puisg-col-8-of-16.puisg-col-12-of-20.puisg-col-12-of-24.puis-list-col-right > div > div > div.a-section.a-spacing-none.puis-padding-right-small.s-title-instructions-style > h2 > a > span
                            try
                            {
                                var productName = item.FindElement(By.CssSelector(".sg-col-inner > div > .rush-component > div > span > div > .a-section > div > div.puisg-col.puisg-col-4-of-12.puisg-col-8-of-16.puisg-col-12-of-20.puisg-col-12-of-24.puis-list-col-right > div > div > div.a-section.a-spacing-none.puis-padding-right-small.s-title-instructions-style > h2 > a > span"));//
                                var BuyingPrice = item.FindElement(By.CssSelector(".sg-col-inner > div > .rush-component > div > span > div > .a-section > div > div.puisg-col.puisg-col-4-of-12.puisg-col-8-of-16.puisg-col-12-of-20.puisg-col-12-of-24.puis-list-col-right > div > div > div.puisg-row > div.puisg-col.puisg-col-4-of-12.puisg-col-4-of-16.puisg-col-4-of-20.puisg-col-4-of-24 > div > div.a-section.a-spacing-none.a-spacing-top-micro.puis-price-instructions-style > div.a-row.a-size-base.a-color-base")).Text;
                                var link = item.FindElement(By.CssSelector(".sg-col-inner > div > .rush-component > div > span > div > .a-section > div > div.puisg-col.puisg-col-4-of-12.puisg-col-8-of-16.puisg-col-12-of-20.puisg-col-12-of-24.puis-list-col-right > div > div > div.puisg-row > div.puisg-col.puisg-col-4-of-12.puisg-col-4-of-16.puisg-col-4-of-20.puisg-col-4-of-24 > div > div.a-section.a-spacing-none.a-spacing-top-micro.puis-price-instructions-style > div.a-row.a-size-base.a-color-base > .a-row > a"));
                                string[] priceParts = BuyingPrice.Replace("\r", "").Replace("\n", " ").Split(' ');
                                string BuyingPricefixed = string.Join(".", priceParts.Take(2));
                                if (BuyingPricefixed == "") continue;
                                var customModel = new CustomModel()
                                {
                                    BrandName = "",
                                    BuyingPrice = BuyingPricefixed,
                                    CategoryName = "",
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
                    }
                    if (i == 3) break;
                    if (!paginationStr.Replace(" ", "").Contains("next")) break;
                    i++;
                }
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
