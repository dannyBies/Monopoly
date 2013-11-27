using System;
namespace Monopoly.Model.Tiles {

	/// <summary>
	/// tile that gets all the tax money
	/// </summary>
	public class TileFreeParking : Tile {
		public TileFreeParking(Game game , string description)
			: base(game , description) {
		}

		public override void DoAction(Player player) {
            CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.moves, player.Name, Description));
            if (CurrentGame.FreeParkingTreasure != 0) {
                CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.freeparkingpay, player.Name, CurrentGame.FreeParkingTreasure));
			}

			player.Money += CurrentGame.FreeParkingTreasure;
			CurrentGame.FreeParkingTreasure = 0;
		}

		public override string GetCardInformation() {
            return Properties.Language.freeparking;
		}
	}
}
