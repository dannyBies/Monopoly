using System;

namespace Monopoly.Model.Tiles {
    public class TileRailRoad : TileBuyable {

        public TileRailRoad(Game game, string description, int[] rent, int mortage, int price)
            : base(game, description, rent, mortage, price) {
        }
        public override void DoAction(Player player) {
            CurrentGame.GameInfo.Enqueue(player.Name + " moved to " + Description);

            if (Owner != null && !player.Equals(Owner)) {
                int toPay = Rent[Owner.TotalRailRoads-1]; 
                player.Money -= toPay;
                Owner.Money += toPay;
                CurrentGame.GameInfo.Enqueue(player.Name + " paid $" + toPay + " to " + Owner.Name);
            }
        }

	    public override string GetCardInformation()
	    {
			string railRoadOwner = (Owner == null) ? "nobody" : Owner.Name;
			return Description + Environment.NewLine +
				"this railroad is currently owned by " + railRoadOwner + Environment.NewLine +
				"it costs $" + Price + " to buy this" + Environment.NewLine +
				"Rent when you have 1 railroad: $" + Rent[0] + Environment.NewLine +
				"Rent when you have 2 railroad: $" + Rent[0] * 2 + Environment.NewLine +
				"Rent when you have 3 railroad: $" + Rent[0] * 4 + Environment.NewLine +
				"Rent when you have 4 railroad: $" + Rent[0] * 8 + Environment.NewLine +
				"Mortgage value: $" + Mortage;
	    }
    }
}
