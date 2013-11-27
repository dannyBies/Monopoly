
using System.Windows;
namespace Monopoly.Model.Cards {
	/// <summary>
	/// Card that gives or takes money from the player
	/// </summary>
	public class CardMoney : Card {

		public int MoneyForPlayer { get; set; }

		public CardMoney(string description , int moneyForPlayer)
			: base(description) {
			MoneyForPlayer = moneyForPlayer;
		}

		public override void Use(Player player) {
			player.Money += MoneyForPlayer;
			//negative money goes to free parking
			if(MoneyForPlayer <= 0) {
				player.CurrentGame.FreeParkingTreasure += MoneyForPlayer * -1;
			}
		}
	}
}
