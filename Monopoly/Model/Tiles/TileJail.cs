namespace Monopoly.Model.Tiles {

	/// <summary>
	/// tile that holds you in jail.
	/// </summary>
	public class TileJail : Tile {

		public TileJail(Game game , string description)
			: base(game , description) {
			NextTile = game.JailVisit;

		}
		public override void DoAction(Player player) {
			if(player.JailCounter != 0) {
				CurrentGame.GameInfo.Enqueue(player.Name + " is still in jail");
			} else {
				CurrentGame.GameInfo.Enqueue(player.Name + " moved to " + Description);
			}

			player.JailCounter++;
			if(CurrentGame.PlayerDice.IsDouble() || player.JailCounter == 3) {
				player.MoveTo(CurrentGame.JailVisit);
				player.IsInJail = false;
				player.JailCounter = 0;
			}
		}

		public override string GetCardInformation() {
			return "Don't drop your soap";
		}
	}
}
