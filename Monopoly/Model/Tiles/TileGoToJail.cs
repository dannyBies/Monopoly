using System;
namespace Monopoly.Model.Tiles {
    /// <summary>
    /// Tile that sends you to jail
    /// </summary>
    public class TileGoToJail : Tile {
        public TileGoToJail(Game game, string description)
            : base(game, description) {
        }

        public override void DoAction(Player player) {
            CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.moves, player.Name, Description));
            player.CurrentTile = CurrentGame.Jail;
            player.IsInJail = true;
        }

		public override string GetCardInformation() {
            return Properties.Language.gotojail;
		}
    }
}
