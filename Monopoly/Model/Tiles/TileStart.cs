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
            CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.moves, player.Name, Description));
            CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.startmoney, player.Name));
			player.Money += 400;
		}

		public override string GetCardInformation() {
            return string.Format(Properties.Language.start, Description, Environment.NewLine);
		}
	}
}
