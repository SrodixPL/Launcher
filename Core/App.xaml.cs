using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
#if DEBUG
        private Process _viteProcess;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _viteProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c npm run dev",
                    WorkingDirectory = System.IO.Path.GetFullPath(@"..\..\..\Client\"),
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            _viteProcess.Start();
            new MainWindow().Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _viteProcess?.Kill();
            _viteProcess?.Dispose();
            base.OnExit(e);
        }
#else
        private FileServer _fileServer;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            System.Windows.Media.Animation.Timeline.DesiredFrameRateProperty.OverrideMetadata(
                typeof(System.Windows.Media.Animation.Timeline),
                new FrameworkPropertyMetadata(144)
            );

            // Serve dist folder over HTTP
            var distPath = System.IO.Path.GetFullPath(@"..\..\..\Client\dist");
            _fileServer = new FileServer(distPath, 21337);
            _fileServer.Start();
            new MainWindow().Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _fileServer?.Stop();
            base.OnExit(e);
        }
#endif
    }
}
