using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ConsoleApp1 <email> <password>");
                return;
            }

            string email = args[0];
            string password = args[1];

            // Ensure the new user data directory exists
            string userDataDir = @"D:\ttt";
            Directory.CreateDirectory(userDataDir);

            // Define the command to run Chrome with the specified options
            //string userName = Environment.UserName;
            //string chromePath = $@"C:\Users\{userName}\AppData\Local\Google\Chrome\Application\chrome.exe";
            //Console.WriteLine("Chrome path: " + chromePath);
            string chromePath = @"D:\work\_google_login\GoogleChromePortable\my_chrome.exe";

            string[] command = new string[]
            {
            chromePath,
            "--remote-debugging-port=9222",
            //$"--user-data-dir={userDataDir}",
            "https://accounts.google.com"
            };

            // Run the command
            Process process = new Process();
            process.StartInfo.FileName = chromePath;
            process.StartInfo.Arguments = string.Join(" ", command, 1, command.Length - 1);
            process.StartInfo.UseShellExecute = true;
            process.Start();

            // Wait a bit to ensure Chrome has started
            Thread.Sleep(3000);

            // Set up Chrome options to connect to the running instance
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.DebuggerAddress = "127.0.0.1:9222";

            // Initialize WebDriver with the specified Chrome options
            new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
            IWebDriver driver = new ChromeDriver(chromeOptions);

            // Test connection by opening a webpage
            driver.Navigate().GoToUrl("https://accounts.google.com");

            // Perform a search on Google
            var searchBox = driver.FindElement(By.Name("identifier"));
            searchBox.SendKeys(email);
            Thread.Sleep(3000);
            driver.FindElement(By.Id("identifierNext")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Name("Passwd")).SendKeys(password);
            Thread.Sleep(3000);
            driver.FindElement(By.Id("passwordNext")).Click();

            // Keep the browser open for 10 seconds to see the result
            Thread.Sleep(10000);

            // Close the browser
            driver.Quit();
        }
    }
}
