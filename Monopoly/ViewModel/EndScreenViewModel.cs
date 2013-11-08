using Monopoly.Model;
using Monopoly.Model.Tiles;

namespace Monopoly.ViewModel {
	public class EndScreenViewModel {
		public Game MyGame { get; set; }
		public string[] Players { get; set; }
		public int[] PlayerMoneys { get; set; }
		public int Winner { get; set; }
		public string WinningPlayer { get; set; }

		public EndScreenViewModel(Game game) {
			MyGame = game;
			Players = new string[MyGame.Players.Count];
			for(int x = 0; x < MyGame.Players.Count; x++) {
				Players[x] = MyGame.Players[x].Name;
			}
			PlayerMoneys = new int[MyGame.Players.Count];
			CalculateScores();
			CalculateWinner();
			WinningPlayer = Players[Winner];
		}

		private void CalculateWinner() {

			Winner = 0;

			if(PlayerMoneys[1] > PlayerMoneys[Winner]) {
				Winner = 1;
			}
			if(MyGame.Players.Count > 2) {
				if(PlayerMoneys[2] > PlayerMoneys[Winner]) {
					Winner = 2;
				}
			}
			if(MyGame.Players.Count > 3) {
				if(PlayerMoneys[3] > PlayerMoneys[Winner]) {
					Winner = 3;
				}
			}
		}

		private void CalculateScores() {
			for(int index = 0; index < MyGame.Players.Count; index++) {
				foreach(Tile tile in MyGame.Players[index].Streets) {
					TileProperty tileProp = tile as TileProperty;
					TileRailRoad tileRailroad = tile as TileRailRoad;
					TileCompany tileComp = tile as TileCompany;

					if(tileProp != null) {
						if(tileProp.TotalUpgrades > 0) {
							for(int downgrade = 0; downgrade < tileProp.TotalUpgrades; downgrade++) {
								tileProp.Downgrade();
							}
							tileProp.Downgrade();
						}
					} else if(tileRailroad != null) {
						tileRailroad.Downgrade();
					} else if(tileComp != null) {
						tileComp.Downgrade();
					}
				}
				PlayerMoneys[index] = MyGame.Players[index].Money;
			}
		}
	}
}
