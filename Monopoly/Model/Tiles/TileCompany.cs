
using System;

namespace Monopoly.Model.Tiles {

	/// <summary>
	/// tile that a player can buy
	/// </summary>
	public class TileCompany : TileBuyable {

		public TileCompany(Game game , string description , int mortage , int price)
			: base(game , description , new[] { 0 } , mortage , price) {
		}

		public override void DoAction(Player player) {
			CurrentGame.GameInfo.Enqueue(player.Name + " moved to " + Description);
			if(Owner != null && !Owner.Equals(player)) {
				int moneyToPay = (Owner.TotalCompanies >= 2) ? player.DiceEyes * 10 : player.DiceEyes * 4;
                player.PayMoneyTo(Owner, moneyToPay);
				CurrentGame.GameInfo.Enqueue(player.Name + " paid $" + moneyToPay + " to " + Owner.Name);
			}
		}

		public override string GetCardInformation() {
			string companyOwner = Owner == null ? "nobody" : Owner.Name;

			return Description + Environment.NewLine +
							"this property is currently owned by " + companyOwner + Environment.NewLine +
							"it costs $" + Price + " to buy this" +
							"If one 'Utility' is owned rent is 4 times the amount shown on dice." + Environment.NewLine +
							"If both 'utilities are owned rent is 10 times amount shown on dice" + Environment.NewLine +
							"You can buy this utility for $" + Price + Environment.NewLine +
							"Mortgage value is $" + Mortage;
		}
	}
}
