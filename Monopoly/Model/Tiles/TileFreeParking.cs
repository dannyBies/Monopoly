namespace Monopoly.Model.Tiles {

	/// <summary>
	/// tile that gets all the tax money
	/// </summary>
	public class TileFreeParking : Tile {
		public TileFreeParking(Game game , string description)
			: base(game , description) {
		}

		public override void DoAction(Player player) {
			CurrentGame.GameInfo.Enqueue(player.Name + " moved to " + Description);
			if(CurrentGame.FreeParkingTreasure != 0) {
				CurrentGame.GameInfo.Enqueue(player.Name + " found a hidden bag with $" + CurrentGame.FreeParkingTreasure + " in it");
			}

			player.Money += CurrentGame.FreeParkingTreasure;
			CurrentGame.FreeParkingTreasure = 0;
		}

		public override string GetCardInformation() {
			return "If you land here you get some hidden goodies, you lucky bastard";
		}
	}
}
