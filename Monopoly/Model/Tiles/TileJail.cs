using System;
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
				CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.jailpeson,player.Name));
			} else {
                CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.moves,player.Name, Description));
			}

			player.JailCounter++;
			if(CurrentGame.PlayerDice.IsDouble() || player.JailCounter == 3) {
				player.MoveTo(CurrentGame.JailVisit);
				player.IsInJail = false;
				player.JailCounter = 0;
			}
		}

		public override string GetCardInformation() {
            return Properties.Language.jail;
		}
	}
}
