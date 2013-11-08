namespace Monopoly.Model.Tiles {
	/// <summary>
	/// Tile to use a chancecard.
	/// </summary>
	public class TileChance : Tile {
		public TileChance(Game game , string description)
			: base(game , description) {
		}

		public override void DoAction(Player player) {
			CurrentGame.GameInfo.Enqueue(player.Name + " moved to " + Description);

			if(CurrentGame.ChanceCards.Peek() != null) {
				CurrentGame.GameInfo.Enqueue(player.Name + " got " + CurrentGame.ChanceCards.Peek().Description);
				CurrentGame.ChanceCards.Pop().Use(player);
			}
		}

		public override string GetCardInformation() {
			return "You get a chance card if you land here";
		}
	}
}
