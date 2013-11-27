using Monopoly.Model.Tiles;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Monopoly.Model {
	/// <summary>
	/// A class that holds data and methods for a player.
	/// </summary>
	public class Player : INotifyPropertyChanged {

		public string Name { get; set; }
		public Tile CurrentTile { get; set; }
		public Game CurrentGame { get; set; }

		public int DiceEyes { get; set; }
		public int TotalCompanies { get; set; }
		public bool IsInJail { get; set; }
		public int JailCounter { get; set; }
		public int TotalRailRoads { get; set; }
		public ObservableCollection<Tile> Streets { get; set; }

		private string _playerInfo;
		public string PlayerInfo {
			get {
				return _playerInfo;
			}
			set {
				_playerInfo = value;
				RaisePropertyChanged("PlayerInfo");
			}
		}
		private int _money;
		public int Money {
			get {
				return _money;
			}
			set {
				_money = value;
				PlayerInfo = Name + " $" + Money;
			}
		}

		public Player(Game game , string name , int money , Tile currentTile) {
			Name = name;
			Money = money;
			CurrentTile = currentTile;
			TotalRailRoads = 0;
			CurrentGame = game;
			DiceEyes = -1;
			IsInJail = false;
			Streets = new ObservableCollection<Tile>();
		}

        public void PayMoneyTo(Player otherplayer, int moneyToPay) {
            Money-= moneyToPay;
            otherplayer.Money += moneyToPay;
        }

		/// <summary>
		/// moves the player forwards or backwards on the board
		/// </summary>
		/// <param name="positions"></param>
		public void MoveTo(int positions) {
			if(!IsInJail) {
				if(positions < 0) {
					for(int i = positions; i < 0; i++) {
						CurrentTile = CurrentTile.PreviousTile;
					}
				} else {
					for(int i = 0; i < positions; i++) {
						CurrentTile = CurrentTile.NextTile;
						if(CurrentTile.Equals(CurrentGame.Start)) {
							Money += 200;
						}

					}
				}
			}
			CurrentTile.DoAction(this);
		}

		/// <summary>
		/// sets the position of the player equal to the specified tile
		/// </summary>
		/// <param name="tile"></param>
		public void MoveTo(Tile tile) {
			CurrentTile = tile;
			CurrentTile.DoAction(this);
		}


        public void BuyBuilding() {
            TileBuyable street = (TileBuyable)CurrentTile;

            if (Money > street.Price) {
                Money -= street.Price;
                street.HasOwner = true;
                street.Owner = this;
                Streets.Add(street);
                CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.bought, Name, street.Description));
                ;
                if (street is TileCompany) {
                    TotalCompanies++;
                } else if (street is TileRailRoad) {
                    TotalRailRoads++;
                }
            }
        }

		public void RaisePropertyChanged(string prop) {
			if(PropertyChanged != null) {
				PropertyChanged(this , new PropertyChangedEventArgs(prop));
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
