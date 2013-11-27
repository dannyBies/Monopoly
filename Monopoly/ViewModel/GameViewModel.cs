using System.ComponentModel;
using System.IO;
using Microsoft.Win32;
using Monopoly.Model;
using Monopoly.Model.Tiles;
using Monopoly.View;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Monopoly.ViewModel {

	/// <summary>
	/// A viewmodel for databinding with a view.
	/// </summary>
	public class GameViewModel : BasicViewModel {

		public Game MyGame { get; set; }
		public Timer UpdatePlayersTimer { get; set; }
		public bool GameIsFinished { get; set; }

		public GameViewModel(int totalPlayers) {
			GameIsFinished = false;
			MyGame = new Game(totalPlayers);
			BuildingPositions = MyGame.ToBoardWithBuildingArray();
			DiceAmount = new[] { 1 , 2 , 3 , 4 , 5 , 6 , 7 , 8 , 9 , 10 , 11 , 12 };
			PlayerList = new string[MyGame.Players.Count];
			for(int x = 0; x < MyGame.Players.Count; x++) {
				PlayerList[x] = MyGame.Players[x].Name;
			}

			UpdatePlayersTimer = new Timer(Timer_Tick , null , 0 , 1000);
			Cheat = true;
		}

		public GameViewModel(string file) {
			MyGame = new Game(file);
			BuildingPositions = MyGame.ToBoardWithBuildingArray();
			DiceAmount = new[] { 1 , 2 , 3 , 4 , 5 , 6 , 7 , 8 , 9 , 10 , 11 , 12 };
			PlayerList = new string[MyGame.Players.Count];

			for(int x = 0; x < MyGame.Players.Count; x++) {
				PlayerList[x] = MyGame.Players[x].Name;
			}
			UpdatePlayersTimer = new Timer(Timer_Tick , null , 0 , 1000);
			Cheat = true;
		}

		private void Timer_Tick(object sender) {
			BuildingPositions = MyGame.ToBoardWithBuildingArray();
			PlayerPostions = MyGame.ToBoardWithPlayersArray();
			ToolTipForTiles = MyGame.GetToolTipInfo();
		}

		# region Commands


		private ICommand _throwDiceCommand;

		public ICommand ThrowDiceCommand {
			get {
				return _throwDiceCommand ??
					   (_throwDiceCommand =
						   new RelayCommand(p => MyGame.ThrowDiceAndMovePlayer() ,
							   p => !MyGame.PlayerDice.HasBeenThrown));
			}
		}

		private ICommand _changePlayerCheatCommand;

		public ICommand ChangePlayerCheatCommand {
			get {
				return _changePlayerCheatCommand ??
					   (_changePlayerCheatCommand =
						   new RelayCommand(p => ChangePlayer(SelectedPlayer)));
			}
		}

		private ICommand _throwCheatDiceCommand;

		public ICommand ThrowCheatDiceCommand {
			get {
				return _throwCheatDiceCommand ??
					   (_throwCheatDiceCommand =
						   new RelayCommand(p => MyGame.ThrowDiceAndMovePlayer(CheatSelected)));
			}
		}

		private ICommand _endTurnCommand;

		public ICommand EndTurnCommand {
			get {
				return _endTurnCommand ??
					   (_endTurnCommand =
						   new RelayCommand(p => EndTurn() , p => CanEndTurn()));
			}
		}

		private ICommand _manageCommand;

		public ICommand ManageCommand {
			get {
				return _manageCommand ??
					   (_manageCommand =
						   new RelayCommand(p => ShowManageWindow()));
			}
		}

		private ICommand _buyCommand;

		public ICommand BuyCommand {
			get {
				return _buyCommand ??
					   (_buyCommand =
						   new RelayCommand(p => MyGame.CurrentPlayer.BuyBuilding() , p => CanBuy()));
			}
		}

		private ICommand _bankruptCommand;

		public ICommand BankruptCommand {
			get {
				return _bankruptCommand ??
					   (_bankruptCommand =
						   new RelayCommand(p => EndGame()));
			}
		}

		# endregion

		# region Command methods

		private void EndGame() {
			GameIsFinished = true;
			EndScreen end = new EndScreen {
				DataContext = new EndScreenViewModel(MyGame)
			};
			end.Show();

			Window window = Application.Current.Windows[0];
			if(window != null) {
				window.Close();
			}

		}

		private void EndTurn() {
			if(Application.Current.Windows.Count == 2) {
				Window window = Application.Current.Windows[1];
				if(window != null) {
					window.Close();
				}
			}
			MyGame.EndTurn();
		}


		private void ShowManageWindow() {
			if(Application.Current.Windows.Count == 1) {
				ManageStreets manageWindow = new ManageStreets { DataContext = new ManageStreetsViewModel(MyGame) };

				manageWindow.Show();
			} else {
				Window window = Application.Current.Windows[1];
				if(window != null) {
					window.Focus();
				}
			}
		}

		public bool CanBuy() {
			if(!MyGame.CurrentPlayer.CurrentTile.HasOwner) {
				TileBuyable buyableTile = (TileBuyable)MyGame.CurrentPlayer.CurrentTile;
				return (buyableTile != null) && MyGame.CurrentPlayer.Money > buyableTile.Price;
			}
			return false;
		}


		public bool CanEndTurn() {
			if(MyGame.PlayerDice.HasBeenThrown && !MyGame.PlayerDice.IsDouble() && MyGame.CurrentPlayer.Money >= 0) {
				return true;
			}
			if(MyGame.PlayerDice.IsDouble()) {
				MyGame.PlayerDice.HasBeenThrown = false;
				return false;
			}
			return false;
		}

		private void ChangePlayer(int index) {
			MyGame.CurrentPlayer = MyGame.Players[index];
		}

		#endregion

		#region bind properties

		private ObservableCollection<string> _toolTipForTiles;

		public ObservableCollection<string> ToolTipForTiles {
			get { return _toolTipForTiles; }
			set {
				if(_toolTipForTiles != value) {
					_toolTipForTiles = value;
					RaisePropertyChanged("ToolTipForTiles");
				}
			}
		}



		private ObservableCollection<ObservableCollection<bool>> _playerPostions;

		public ObservableCollection<ObservableCollection<bool>> PlayerPostions {
			get { return _playerPostions; }
			set {
				if(_playerPostions != value) {
					_playerPostions = value;
					RaisePropertyChanged("PlayerPostions");
				}
			}
		}

		private ObservableCollection<bool> _buildingPositions;

		public ObservableCollection<bool> BuildingPositions {
			get { return _buildingPositions; }
			set {
				if(_buildingPositions != value) {
					_buildingPositions = value;
					RaisePropertyChanged("BuildingPositions");
				}
			}
		}

		private int[] _diceAmount;

		public int[] DiceAmount {
			get { return _diceAmount; }
			set {
				if(_diceAmount != value) {
					_diceAmount = value;
					RaisePropertyChanged("DiceAmount");
				}
			}
		}

		private int _selectedPlayer;

		public int SelectedPlayer {
			get { return _selectedPlayer; }
			set {
				if(_selectedPlayer != value) {
					_selectedPlayer = value;
					RaisePropertyChanged("DiceAmount");
				}
			}
		}

		private string[] _playerList;

		public string[] PlayerList {
			get { return _playerList; }
			set {
				if(_playerList != value) {
					_playerList = value;
					RaisePropertyChanged("PlayerList");
				}
			}
		}

		private int _cheatSelected;

		public int CheatSelected {
			get { return _cheatSelected; }
			set {
				if(_cheatSelected != value) {
					_cheatSelected = value;
					RaisePropertyChanged("CheatSelected");
				}
			}
		}

		private bool _cheat;

		public bool Cheat {
			get { return _cheat; }
			set {
				if(_cheat != value) {
					_cheat = value;
					RaisePropertyChanged("Cheat");
				}
			}
		}

		#endregion


		public void OnWindowClosing(object sender , CancelEventArgs e) {
			if(!GameIsFinished) {
				var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
				string filename = string.Empty;
				if(directoryInfo != null) {
					SaveFileDialog dlg = new SaveFileDialog {
						Filter = "Monopoly Files (*.poly)|*.poly" ,
						InitialDirectory = directoryInfo.FullName + @"\Data\Saves\" ,
					};
					dlg.ShowDialog();
					filename = dlg.FileName;
				}
				if(!string.IsNullOrEmpty(filename) && filename.Contains(".poly")) {
					MyGame.SaveData(filename);
				}
			}
		}
	}
}

