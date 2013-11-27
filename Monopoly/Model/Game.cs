using Monopoly.Model.Cards;
using Monopoly.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Monopoly.Model {
	/// <summary>
	/// A class that holds data of a game and controls the enviroment of the game itself.
	/// </summary>
	public class Game : INotifyPropertyChanged {
		public ObservableCollection<Player> Players { get; set; }
		public Configuration CurrentConfigFile { get; set; }
		public Deck ChanceCards { get; set; }
		public Deck CommunityCards { get; set; }
		public Player CurrentPlayer { get; set; }
		public int PlayerTurn { get; set; }
		public Dice PlayerDice { get; set; }
		
		public QueueLimit<string> GameInfo { get; set; }

		public TileLinkedList Board { get; set; }
		public TileJailVisit JailVisit { get; set; }
		public TileJail Jail { get; set; }
		public TileGoToJail GoToJail { get; set; }
		public TileStart Start { get; set; }
		public TileProperty Boardwalk { get; set; }

		private int _freeParkingTreasure;
		public int FreeParkingTreasure {
			get {
				return _freeParkingTreasure;
			}
			set {
				_freeParkingTreasure = value;
				RaisePropertyChanged("FreeParkingTreasure");
			}
		}


		private Game() {
			CurrentConfigFile = new Configuration(this);
			GameInfo = new QueueLimit<string>(70);
			PlayerDice = new Dice();
			PlayerTurn = 0;
		}
		/// <summary>
		/// Constructor for loading an existing game
		/// </summary>
		public Game(string saveFile)
			: this() {
			//file komt uit filechooser
			InitSavedGame(saveFile);
		}

		/// <summary>
		/// Constructor for creating a new game
		/// </summary>
		/// <param name="totalPlayers"></param>
		public Game(int totalPlayers)
			: this() {
			InitNewGame(totalPlayers);
		}

		#region Initiation of Games

		/// <summary>
		/// initialise a new game
		/// </summary>
		/// <param name="maxplayers"></param>
		public void InitNewGame(int maxplayers) {
			Board = CurrentConfigFile.LoadDefauldBoard();
			LoadCards();

			Players = new ObservableCollection<Player>();
			for(int index = 0; index < maxplayers; index++) {
				Players.Add(new Player(this , "Player " + (index + 1) , 1500 , Start));
			}
			CurrentPlayer = Players.First();

		}

		/// <summary>
		/// initialise a saved game
		/// </summary>
		/// <param name="savedGame"></param>
		public void InitSavedGame(string savedGame) {
			Board = CurrentConfigFile.LoadDefauldBoard();
			LoadCards();
			LoadPlayers(savedGame);
		}

		/// <summary>
		/// Loads the players from a save file
		/// </summary>
		/// <param name="savedGame"></param>
		public void LoadPlayers(string savedGame) {
			Players = CurrentConfigFile.GetAllPlayers(savedGame);
			CurrentPlayer = Players.ElementAt(0);
		}

		/// <summary>
		/// Loads all the cards from a file and fills 2 decks with it
		/// </summary>
		private void LoadCards() {
			List<Card> cards = CurrentConfigFile.GetAllCards(@"Config\CardDescriptions").ToList();
			ChanceCards = new Deck(cards.GetRange(0 , (cards.Count / 2)));
			cards.RemoveRange(0 , cards.Count / 2);
			CommunityCards = new Deck(cards);
		}

		#endregion

		#region Game Mechanics

		public void SaveData(string filename) {
			CurrentConfigFile.SaveData(filename);
		}

		public void ThrowDiceAndMovePlayer() {
			CurrentPlayer.DiceEyes = PlayerDice.ThrowDice();
			PlayerDice.HasBeenThrown = true;
			GameInfo.Enqueue(String.Format(Properties.Language.throwdice,CurrentPlayer.Name,PlayerDice.FirstDice,PlayerDice.SecondDice));
			CurrentPlayer.MoveTo(CurrentPlayer.DiceEyes);
		}

		public void ThrowDiceAndMovePlayer(int value) {
			CurrentPlayer.DiceEyes = value;
			PlayerDice.HasBeenThrown = true;
			GameInfo.Enqueue(String.Format(Properties.Language.cheatdice,CurrentPlayer.Name,value));
            
			CurrentPlayer.MoveTo(CurrentPlayer.DiceEyes);
		}

        public void NextTurn() {
            PlayerTurn++;
            if ((PlayerTurn % Players.Count) == 0) {
                PlayerTurn = 0;
            }
            CurrentPlayer = Players[PlayerTurn];
        }
		public void EndTurn() {
			if(!PlayerDice.IsDouble()) {
				NextTurn();
			}
            PlayerDice.HasBeenThrown = false;
		}

		#endregion

		#region View Bindings
		public ObservableCollection<ObservableCollection<bool>> ToBoardWithPlayersArray() {
			ObservableCollection<ObservableCollection<bool>> array = new ObservableCollection<ObservableCollection<bool>>();
			for(int i = 0; i < Players.Count; i++) {
				Tile current = Board.Head;
				ObservableCollection<bool> sublist = new ObservableCollection<bool>();

				for(int j = 0; j < Board.Size; j++) {
					sublist.Add(current.Equals(Players.ElementAt(i).CurrentTile));
					current = current.NextTile;
				}
				sublist.Add(Players.ElementAt(i).IsInJail);
				array.Add(sublist);
			}
			for(int i = 0; i < 4 - Players.Count; i++) {
				ObservableCollection<bool> empty = new ObservableCollection<bool>();
				for(int j = 0; j < Board.Size + 1; j++) {
					empty.Add(false);
				}
				array.Add(empty);
			}
			return array;
		}

		public ObservableCollection<bool> ToBoardWithBuildingArray() {
			ObservableCollection<bool> buildingVisibility = new ObservableCollection<bool>();
			for(int x = 0; x < Board.Size; x++) {
				buildingVisibility.Add(Board.GetAt(x).HasBuildings);
			}
			return buildingVisibility;
		}

		/// <summary>
		/// gets information that will be used in tooltips
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<string> GetToolTipInfo() {
			ObservableCollection<string> info = new ObservableCollection<string>();

			Tile current = Board.Head;
			for(int i = 0; i < Board.Size; i++) {
				info.Add(current.GetCardInformation());
				current = current.NextTile;
			}
			info.Add(Jail.GetCardInformation());
			return info;
		}

		public void RaisePropertyChanged(string prop) {
			if(PropertyChanged != null) {
				PropertyChanged(this , new PropertyChangedEventArgs(prop));
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
