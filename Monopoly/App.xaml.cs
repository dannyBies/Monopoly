using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Windows.Input;
using Monopoly.View;
using System.Windows;
using Monopoly.Properties;

namespace Monopoly {
    /// <summary>
    /// a command layout that implements ICommand.
    /// </summary>
    public partial class App {

        private void Application_Startup(object sender, StartupEventArgs e) {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(Monopoly.Properties.Settings.Default.Language);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(Monopoly.Properties.Settings.Default.Language);
        }
    }
}


