using System;

namespace Monopoly.Model.Tiles {

	/// <summary>
	/// When you land on this tile you get extra money.
	/// </summary>
	public class TileStart : Tile {

		public TileStart(Game game , string description)
			: base(game , description) {

		}

		public override void DoAction(Player player) {
			CurrentGame.GameInfo.Enqueue(player.Name + " moved to " + Description);
			CurrentGame.GameInfo.Enqueue(player.Name + " landed on start, he gets an extra $400");
			player.Money += 400;
		}

		public override string GetCardInformation() {
			return Description + Environment.NewLine +
					"If you land on this tile you $400" + Environment.NewLine +
					"Else if you pass this tile you get $200";
		}
	}
}
