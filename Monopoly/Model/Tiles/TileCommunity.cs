using System;
namespace Monopoly.Model.Tiles {
    /// <summary>
    /// Tile to use a communitycard.
    /// </summary>
    public class TileCommunity : Tile {

        public TileCommunity(Game game, string description)
            : base(game, description) {
        }

        public override void DoAction(Player player) {
            CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.moves, player.Name, Description));

            if (CurrentGame.CommunityCards.Peek() != null) {
                CurrentGame.GameInfo.Enqueue(player.Name + " "+ Properties.Language.got+" " + CurrentGame.CommunityCards.Peek().Description);
                CurrentGame.CommunityCards.Pop().Use(player);
            }
        }

		public override string GetCardInformation() {
            return Properties.Language.community;
		}
    }
}
