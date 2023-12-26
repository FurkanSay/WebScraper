using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebScraper.Custom_Model;
using WebScraper.Trendyol.Models;

namespace WebScraper.Trendyol
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
            for (int i = 1; i < 3; i++)
            {
                try
                {
                    driver.Navigate().GoToUrl($"https://public.trendyol.com/discovery-web-searchgw-service/v2/api/infinite-scroll/sr?q={searchText}&qt={searchText}&st={searchText}&os=1&pi={i}&culture=tr-TR&userGenderId=1&pId=&isLegalRequirementConfirmed=false&searchStrategyType=DEFAULT&productStampType=TypeA&scoringAlgorithmId=2&fixSlotProductAdsIncluded=true&searchAbDecider=&location=null&initialSearchText=telefon&offset=36&channelId=1");
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
                    IWebElement preElement = driver.FindElement(By.CssSelector("pre[style='word-wrap: break-word; white-space: pre-wrap;']"));

                    string jsonContent = preElement.Text;
                    var trendyolmodel = JsonConvert.DeserializeObject<TrendyolProduct>(jsonContent);
                    var customModel = trendyolmodel.result.products.Select(x => new CustomModel()
                    {
                        BrandName = x.brand.name,
                        BuyingPrice = x.price.sellingPrice.ToString(),
                        CategoryName = x.categoryName,
                        ProductName = x.name,
                        Website = "https://trendyol.com" + x.url
                    }).ToList();
                    customModels = customModels.Concat<CustomModel>(customModel).ToList();
                }
                catch (Exception ex)
                {
                    continue;
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
