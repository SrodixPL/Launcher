using CefSharp.Wpf.Experimental.Accessibility;
using System.Windows;
using System.Windows.Input;

namespace Core
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Browser.JavascriptObjectRepository.Register("jsBridge", new Bridge.JSBridge(), isAsync: true);
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