using CefSharp.Wpf.Experimental.Accessibility;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Core
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += async (s, e) =>
            {
                await AwaitVite();
            };
            Browser.JavascriptObjectRepository.Register("jsBridge", new Bridge.JSBridge(), isAsync: true);
        }

        private async Task AwaitVite()
        {
            var http = new HttpClient();
            while (true)
            {
                try
                {
                    await http.GetAsync("http://localhost:5173");
                    break;
                }
                catch
                {
                    await Task.Delay(100);
                }
            }
            Browser.Address = "http://localhost:5173";
        }

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            // Compensate for WPF overflow when maximized
            RootGrid.Margin = WindowState == WindowState.Maximized
                ? new Thickness(6)
                : new Thickness(0);

            btnMaxMin.Content = WindowState == WindowState.Maximized
                ? "\U0001F5D7"
                : "\U0001F5D6";
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
            => WindowState = WindowState.Minimized;

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
            => Close();
    }
}