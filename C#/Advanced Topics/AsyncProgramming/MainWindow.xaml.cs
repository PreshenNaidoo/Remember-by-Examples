using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncProgramming
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {            
            //This will leave the UI unresponsive:
            //DownloadHtml(@"http://www.worldslongestwebsite.com/");
            //Use this Async version instead:
            //DownloadHtmlAsync("https://docs.microsoft.com/en-us/");

            //Async with Task return value:
            //string html = GetHtml("https://docs.microsoft.com/en-us/");
            var htmlTask = GetHtmlAsync("https://docs.microsoft.com/en-us/");
            MessageBox.Show("Do some other work here thats not dependent on operation GetHtmlAsync");

            string html = await htmlTask;
            //We return control to the UI thread here. The code
            //below this line will only execute once the htmlTask
            //is completed.
            MessageBox.Show(html.Substring(0, 10));
        }

        public async Task<string> GetHtmlAsync(string url)
        {
            var webClient = new WebClient();

            //await keyword can only be used in an async method
            return await webClient.DownloadStringTaskAsync(url);
        }

        public string GetHtml(string url)
        {
            var webClient = new WebClient();

            return webClient.DownloadString(url);
        }

        public async Task DownloadHtmlAsync(string url)
        {
            var webClient = new WebClient();
            var html = await webClient.DownloadStringTaskAsync(url);

            using (var streamWriter = new StreamWriter(@".\result.html"))
            {
                await streamWriter.WriteAsync(html);
            }
        }

        public void DownloadHtml(string url)
        {
            var webClient = new WebClient();
            var html = webClient.DownloadString(url);

            using (var streamWriter = new StreamWriter(@".\result.html"))
            {
                streamWriter.Write(html);
            }
        }
    }
}
