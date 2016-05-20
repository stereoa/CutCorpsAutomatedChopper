using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Threading;

namespace CutCoAutomatedChopper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void CUT_Click(object sender, RoutedEventArgs e)
        {
            var prc = Process.GetProcessesByName("dota2");
            if (prc.Length > 0)
            {
                SetForegroundWindow(prc[0].MainWindowHandle);
                await Dispatcher.Yield(DispatcherPriority.ApplicationIdle);
                RECT dotaDisplayArea;
                GetWindowRect(GetForegroundWindow(), out dotaDisplayArea);
                var width = dotaDisplayArea.Right;
                var height = dotaDisplayArea.Bottom;

                while (true)
                {
                    for (var x = (int)Math.Round(width * .15); x < width * .66; x += 80)
                    {
                        for (var y = (int)Math.Round(width * .13); y < height * .8; y += 40)
                        {
                            await Dispatcher.Yield(DispatcherPriority.ApplicationIdle);
                            LeftMouseClick(x, y);
                            await Task.Delay(10);
                        }
                    }
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
