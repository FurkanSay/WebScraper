using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.Amazon
{
    public class JumpAuthetication
    {
        private readonly HttpClient httpClient;
        private readonly ChromeDriver driver;
        private readonly string X_RapidAPI_Key = "18f6677b0fmsh21557794cd18ac7p1df9a9jsn928a5c1bfa73";
        private readonly string X_RapidAPI_Host = "ocr43.p.rapidapi.com";

        public JumpAuthetication(ChromeDriver driver)
        {
            this.httpClient = new HttpClient();
            this.driver = driver;
        }
        public async Task<(bool,string)> Check()
        {
           try
            {
                IWebElement searchbox = driver.FindElement(By.CssSelector("body > div > div.a-row.a-spacing-double-large > div.a-section > div > div > form > div.a-row.a-spacing-large > div > div > div.a-row.a-text-center > img"));
                Thread.Sleep(2000);
                string srcValue = searchbox.GetAttribute("src");
                if (srcValue != null) { return (true, srcValue); }
                else { return (false, ""); }
            }
            catch (Exception ex)
            {
                return (false, ""); 
            }
        }
        public async Task<bool> Jump(string imgUrl)
        {

            try {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://ocr43.p.rapidapi.com/v1/results"),
                    Headers ={
                            { "X-RapidAPI-Key", X_RapidAPI_Key },
                            { "X-RapidAPI-Host", X_RapidAPI_Host },
                         },
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "url", imgUrl },
                }),
                };
                using (var response = await httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<SolverModel>(body);
                    IWebElement input = driver.FindElement(By.CssSelector("#captchacharacters"));
                    Thread.Sleep(3);
                    input.SendKeys(model.results.First().entities.First().objects.First().entities.First().text);
                    IWebElement button = driver.FindElement(By.CssSelector("body > div > div.a-row.a-spacing-double-large > div.a-section > div > div > form > div.a-section.a-spacing-extra-large > div > span > span > button"));
                    Thread.Sleep(3);
                    button.Click();
                    IWebElement amazonsearch = driver.FindElement(By.CssSelector("#nav-search-bar-form > div.nav-fill > div.nav-search-field"));
                    Thread.Sleep(3);
                    if ((await Check()).Item1) { return await Jump(imgUrl); }
                    else { return true; }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
