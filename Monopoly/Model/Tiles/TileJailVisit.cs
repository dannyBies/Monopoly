namespace Monopoly.Model.Tiles {

    /// <summary>
    /// Watch monkeys in their natural habitat.
    /// </summary>
    public class TileJailVisit : Tile {

        public TileJailVisit(Game game,  string description)
            : base(game,  description) {
        }
        public override void DoAction(Player player) {
            CurrentGame.GameInfo.Enqueue(player.Name + " moved to " + Description);
        }

		public override string GetCardInformation() {
			return "You visit jail to give your buddy soap because he somehow lost it";
		}
    }
}