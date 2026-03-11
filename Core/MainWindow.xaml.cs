using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Core
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(this, EdgeMode.Aliased);

            Loaded += async (s, e) =>
            {
                await Browser.EnsureCoreWebView2Async();
                Browser.CoreWebView2.AddHostObjectToScript("jsBridge", new Bridge.JSBridge());

                await AwaitVite();
            };
        }

#if DEBUG 
        private int port = 5173;
#else 
        private int port = 21337;
#endif

        private async Task AwaitVite()
        {
            var http = new HttpClient();
            while (true)
            {
                try
                {
                    await http.GetAsync($"http://localhost:{port}");
                    break;
                }
                catch
                {
                    await Task.Delay(100);
                }
            }
            Browser.Source = new System.Uri($"http://localhost:{port}");
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