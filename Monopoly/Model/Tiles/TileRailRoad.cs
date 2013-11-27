using System;

namespace Monopoly.Model.Tiles {
    public class TileRailRoad : TileBuyable {

        public TileRailRoad(Game game, string description, int[] rent, int mortage, int price)
            : base(game, description, rent, mortage, price) {
        }
        public override void DoAction(Player player) {
            CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.moves, player.Name, Description));

            if (Owner != null && !player.Equals(Owner)) {
                int toPay = Rent[Owner.TotalRailRoads-1];
                player.PayMoneyTo(Owner, toPay);
                CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.pay, player.Name, toPay, Owner.Name));
            }
        }

	    public override string GetCardInformation()
	    {
			string railRoadOwner = (Owner == null) ? Properties.Language.propertyowner : Owner.Name;
            return String.Format(Properties.Language.railroad, Description, Environment.NewLine, railRoadOwner, Price, Rent[0], Rent[0] * 2, Rent[0] * 4, Rent[0] * 8, Mortage);
	    }
    }
}
