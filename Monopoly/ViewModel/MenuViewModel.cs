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
            Languages = new string[] { "English", "Nederlands" };
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


        private ICommand _startNewGameCommand;
        public ICommand StartNewGameCommand {
            get {
                return _startNewGameCommand ??
                       (_startNewGameCommand =
                        new RelayCommand(p => StartGame(), p => CanStartGame()));
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

        private string[] _languages;
        public string[] Languages {
            get { return _languages; }
            set {
                if (_languages != value) {
                    _languages = value;
                    RaisePropertyChanged("Languages");
                }
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage {
            get { return _selectedLanguage; }
            set {
                if (_selectedLanguage != value) {
                    _selectedLanguage = value;
                    RaisePropertyChanged("SelectedLanguage");
                    ChangeCulture(value);
                }
            }
        }

        private void ChangeCulture(string value) {
            switch (value.ToLower())
            {
                case "nederlands":
                Properties.Settings.Default.Language = "nl-NL";
                break;
                case "english":
                Properties.Settings.Default.Language = "en-GB";
                break;
                default:
                break;
            }
            Properties.Settings.Default.Save();
            MessageBox.Show("Please restart the application to see your language.");
        }

        private int _totalPlayers;
        public int TotalPlayers {
            get { return _totalPlayers; }
            set {
                if (_totalPlayers != value) {
                    _totalPlayers = value;
                    RaisePropertyChanged("TotalPlayers");
                }
            }
        }

        #endregion
    }
}
