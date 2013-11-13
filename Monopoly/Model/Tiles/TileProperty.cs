
using System;

namespace Monopoly.Model.Tiles {

	/// <summary>
	/// a tile that you can buy
	/// </summary>
	public class TileProperty : TileBuyable {
		public City City { get; set; }

		public TileProperty(Game game , string description , int[] rent , int mortage , int price , int upgradeCost , City city)
			: base(game , description , rent , mortage , price) {
			City = city;
			UpgradeCost = upgradeCost;
		}
		public override void DoAction(Player player) {
			CurrentGame.GameInfo.Enqueue(player.Name + " moved to " + Description);

			if(Owner != null && !player.Equals(Owner)) {
				if(City.OwnsAllProperties(Owner)) {
					if(TotalUpgrades == 0 && !OnMortage) {
                        player.PayMoneyTo(Owner, Rent[TotalUpgrades] * 2);
						CurrentGame.GameInfo.Enqueue(player.Name + " paid $" + Rent[TotalUpgrades] * 2 + " to " + Owner.Name);
					} else {
                        player.PayMoneyTo(Owner, Rent[TotalUpgrades]);
						CurrentGame.GameInfo.Enqueue(player.Name + " paid $" + Rent[TotalUpgrades] + " to " + Owner.Name);
					}

				} else {
                    player.PayMoneyTo(Owner, Rent[TotalUpgrades]);
					CurrentGame.GameInfo.Enqueue(player.Name + " paid $" + Rent[TotalUpgrades] + " to " + Owner.Name);
				}

			}
		}

		public bool CanBeUpgraded() {
			if (OnMortage)
			{
				return true;
			}
			bool first = false;
			bool second = false;
			bool third = false;

			for(int x = 0; x < City.Streets.Count; x++) {
				switch(x) {
					case 0:
						if(!Equals(City.Streets[x])) {
							if(City.Streets[x].TotalUpgrades >= TotalUpgrades) {
								first = true;
							}
						}
						break;
					case 1:
						if(!Equals(City.Streets[x])) {
							if(City.Streets[x].TotalUpgrades >= TotalUpgrades) {
								second = true;
							}
						}
						break;
					case 2:
						if(!Equals(City.Streets[x])) {
							if(City.Streets[x].TotalUpgrades > TotalUpgrades) {
								third = true;
							}
						}
						break;
				}
			}
			return (first || second || third) && TotalUpgrades < 5;
		}

		public bool CanBeDowngraded() {
			bool first = false;
			bool second = false;
			bool third = false;

			for(int x = 0; x < City.Streets.Count; x++) {
				switch(x) {
					case 0:
						if(City.Streets[x].TotalUpgrades <= TotalUpgrades) {
							first = true;
						}
						break;
					case 1:
						if(City.Streets[x].TotalUpgrades <= TotalUpgrades) {
							second = true;
						}
						break;
					case 2:
						if(City.Streets[x].TotalUpgrades <= TotalUpgrades) {
							third = true;
						}
						break;
				}
			}
			return (first || second || third) && !OnMortage;
		}

		public override string GetCardInformation() {
			string propertyOwner;
			string houses = string.Empty;
			if(Owner == null) {
				propertyOwner = "nobody";
			} else {
				propertyOwner = Owner.Name;
				houses = "There have been " + TotalUpgrades + " houses built in this street." + Environment.NewLine;
			}
			return Description + Environment.NewLine +
				"this property is currently owned by " + propertyOwner + Environment.NewLine + houses +
				"it costs $" + Price + " to buy this" + Environment.NewLine +
				"Rent $" + Rent[0] + Environment.NewLine +
				"Costs to upgrade your street: $" + UpgradeCost + Environment.NewLine +
				"With 1 house your rent is: $" + Rent[1] + Environment.NewLine +
				"With 2 house your rent is: $" + Rent[2] + Environment.NewLine +
				"With 3 house your rent is: $" + Rent[3] + Environment.NewLine +
				"With 4 house your rent is: $" + Rent[4] + Environment.NewLine +
				"With a hotel your rent is: $" + Rent[5] + Environment.NewLine +
				"Mortgage value: $" + Mortage + Environment.NewLine +
				"If you have the whole city the rent is doubled.";
		}
	}
}
