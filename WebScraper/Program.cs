using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Pipes;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using WebScraper;
using WebScraper.Configuration;
using Trendyol = WebScraper.Trendyol;
using N11 = WebScraper.n11;
using Amazon = WebScraper.Amazon;
using WebScraper.Custom_Model;

class Program
{
    static async Task Main()
    {
        var alllist = new List<CustomModel>();
        var newconfig = new WebDriverConfiguration(3);
        var driver = newconfig.GetDriver();

        // Task'ları oluştur
        Task<List<CustomModel>> trendyolTask = ProcessTrendyolAsync(driver[0], "telefon");
        Task<List<CustomModel>> n11Task = ProcessN11Async(driver[1], "telefon");
        Task<List<CustomModel>> amazonTask = ProcessAmazonAsync(driver[2], "phone");

        // Tüm task'ların tamamlanmasını bekle
        await Task.WhenAll(n11Task, trendyolTask, amazonTask);//
        // Sonuçları al
        var trendyolList = trendyolTask.Result;
        var n11List = n11Task.Result;
        var amazonList = amazonTask.Result;

        alllist = alllist.Concat(trendyolList).ToList();
        alllist = alllist.Concat(n11List).ToList();
        alllist = alllist.Concat(amazonList).ToList();
        string jsonText = JsonConvert.SerializeObject(alllist, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText("txt.json", jsonText);

        // Diğer işlemleri gerçekleştir
        // ...
    }

    static async Task<List<CustomModel>> ProcessTrendyolAsync(ChromeDriver driver, string query)
    {
        var trendyol = new Trendyol.DataProcess(driver);
        return await trendyol.Process(query);
    }

    static async Task<List<CustomModel>> ProcessN11Async(ChromeDriver driver, string query)
    {
        var n11 = new N11.DataProcess(driver);
        return await n11.Process(query);
    }

    static async Task<List<CustomModel>> ProcessAmazonAsync(ChromeDriver driver, string query)
    {
        var amazon = new Amazon.DataProcess(driver);
        return await amazon.Process(query);
    }

}
