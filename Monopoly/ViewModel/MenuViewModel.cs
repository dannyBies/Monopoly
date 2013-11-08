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

namespace Monopoly.ViewModel {
    /// <summary>
    /// A viewmodel for databinding with a view.
    /// </summary>
    public class MenuViewModel : BasicViewModel {

        public MenuViewModel() {
            MaxPlayers = new List<int> { 2, 3, 4 };
			MessageBox.Show(Thread.CurrentThread.CurrentUICulture.DisplayName);
        }

        # region Commands

        private ICommand _openFilecommand;
        public ICommand OpenFileCommand {
            get {
                return _openFilecommand ??
                       (_openFilecommand =
                        new RelayCommand(p => OpenFile()));
            }
        }

        private ICommand _changeLanguageCommand;
        public ICommand ChangeLanguageCommand {
            get {
                return _changeLanguageCommand ??
                       (_changeLanguageCommand =
                        new RelayCommand(p => ChangeCulture()));
            }
        }

        private ICommand _startNewGameCommand;
        public ICommand StartNewGameCommand {
            get {
                return _startNewGameCommand ??
                       (_startNewGameCommand =
                        new RelayCommand(p => StartGame() , p => CanStartGame()));
            }
        }

        # endregion

        #region Command methods

        public void StartGame() {
            GameView view = new GameView();
            GameViewModel gmv = new GameViewModel(TotalPlayers);
            view.DataContext = gmv;
            view.Closing += gmv.OnWindowClosing;
            view.Show();
            Application.Current.MainWindow.Close();
        } 

        public bool CanStartGame() {
            return TotalPlayers >= 2;
        }

        public void ChangeCulture()
        {
			MessageBox.Show(Thread.CurrentThread.CurrentUICulture.DisplayName);
	       Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
			Properties.Language.Culture = new CultureInfo("en-GB");
	          
        }

        public void OpenFile() {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            string filename = string.Empty;
            
            if (directoryInfo != null) {
                OpenFileDialog dlg = new OpenFileDialog {
                    Filter = "Monopoly Files (*.poly)|*.poly",
                    InitialDirectory = directoryInfo.FullName + @"\Data\Saves\",
                };

                dlg.ShowDialog();
                filename = dlg.FileName;
            }

            if (!string.IsNullOrEmpty(filename) && filename.Contains(".poly")) {
                GameView view = new GameView();
                GameViewModel gmv = new GameViewModel(filename);
                view.DataContext = gmv;
                view.Closing += gmv.OnWindowClosing;
                view.Show();
                Application.Current.MainWindow.Close();
            }
        }
        #endregion

        #region Bind properties

        private List<int> _maxPlayers;
        public List<int> MaxPlayers {
            get { return _maxPlayers; }
            set {
                if (_maxPlayers != value) {
                    _maxPlayers = value;
                    RaisePropertyChanged("MaxPlayers");
                }
            }
        }

        private int _totalPlayers;
        public int TotalPlayers {
            get { return _totalPlayers; }
            set {
                if(_totalPlayers != value) {
                    _totalPlayers = value;
                    RaisePropertyChanged("TotalPlayers");
                }
            }
        }

        #endregion
    }
}
