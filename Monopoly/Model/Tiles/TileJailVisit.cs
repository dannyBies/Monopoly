using System;
namespace Monopoly.Model.Tiles {

    /// <summary>
    /// Watch monkeys in their natural habitat.
    /// </summary>
    public class TileJailVisit : Tile {

        public TileJailVisit(Game game,  string description)
            : base(game,  description) {
        }
        public override void DoAction(Player player) {
            CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.moves, player.Name, Description)); 
        }

		public override string GetCardInformation() {
            return Properties.Language.jailvisit;
		}
    }
}