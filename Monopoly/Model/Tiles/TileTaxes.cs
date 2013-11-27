using System;

namespace Monopoly.Model.Tiles {

	/// <summary>
	/// Tile where you have to pay taxes.
	/// </summary>
	public class TileTaxes : Tile {
		public TileTaxes(Game game , string description)
			: base(game , description) {
		}

		public override void DoAction(Player player) {
            CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.moves, player.Name, Description));

			if(player.Money > 1000) {
				CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.taxespay200,player.Name));
				CurrentGame.FreeParkingTreasure += 200;
				player.Money -= 200;
			} else {
				CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.taxespay10,player.Name,player.Money/10));
				CurrentGame.FreeParkingTreasure += player.Money / 10;
				player.Money -= player.Money / 10;
			}

		}

		public override string GetCardInformation()
		{
            return String.Format(Properties.Language.taxes, Environment.NewLine);					
		}
	}
}
