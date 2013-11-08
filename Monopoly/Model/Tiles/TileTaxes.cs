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
			CurrentGame.GameInfo.Enqueue(player.Name + " moved to " + Description);

			if(player.Money > 1000) {
				CurrentGame.GameInfo.Enqueue(player.Name + " had to pay $200 in taxes");
				CurrentGame.FreeParkingTreasure += 200;
				player.Money -= 200;
			} else {
				CurrentGame.GameInfo.Enqueue(player.Name + " had to pay $ " + player.Money / 10);
				CurrentGame.FreeParkingTreasure += player.Money / 10;
				player.Money -= player.Money / 10;
			}

		}

		public override string GetCardInformation()
		{
			return "Unfortunately you have to pay taxes but don't worry " + Environment.NewLine +
							"in real life you won't have to pay 10% of your total net worth!";
						
		}
	}
}
