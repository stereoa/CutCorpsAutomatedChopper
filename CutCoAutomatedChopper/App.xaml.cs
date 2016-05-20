using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CutCoAutomatedChopper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _window;

        public App(KeyboardHook keyboardHook)
        {
            if (keyboardHook == null) throw new ArgumentNullException("keyboardHook");
            keyboardHook.KeyCombinationPressed += KeyPressed;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _window = new MainWindow();

            _window.Show();
        }

        void KeyPressed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
