
using System;

namespace Monopoly.Model.Tiles {

    /// <summary>
    /// a tile that you can buy
    /// </summary>
    public class TileProperty : TileBuyable {
        public City City { get; set; }

        public TileProperty(Game game, string description, int[] rent, int mortage, int price, int upgradeCost, City city)
            : base(game, description, rent, mortage, price) {
            City = city;
            UpgradeCost = upgradeCost;
        }
        public override void DoAction(Player player) {
            CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.moves, player.Name, Description));

            if (Owner != null && !player.Equals(Owner)) {
                if (City.OwnsAllProperties(Owner)) {
                    if (TotalUpgrades == 0 && !OnMortage) {
                        player.PayMoneyTo(Owner, Rent[TotalUpgrades] * 2);
                        CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.pay,player.Name,Rent[TotalUpgrades]*2, Owner.Name));
                    } else {
                        player.PayMoneyTo(Owner, Rent[TotalUpgrades]);
                        CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.pay, player.Name, Rent[TotalUpgrades], Owner.Name));
                    }

                } else {
                    player.PayMoneyTo(Owner, Rent[TotalUpgrades]);
                    CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.pay, player.Name, Rent[TotalUpgrades], Owner.Name));
                }

            }
        }

        public bool CanBeUpgraded() {
            if (OnMortage) {
                return true;
            }
            bool first = false;
            bool second = false;
            bool third = false;

            for (int x = 0; x < City.Streets.Count; x++) {
                switch (x) {
                    case 0:
                    if (!Equals(City.Streets[x])) {
                        if (City.Streets[x].TotalUpgrades >= TotalUpgrades) {
                            first = true;
                        }
                    }
                    break;
                    case 1:
                    if (!Equals(City.Streets[x])) {
                        if (City.Streets[x].TotalUpgrades >= TotalUpgrades) {
                            second = true;
                        }
                    }
                    break;
                    case 2:
                    if (!Equals(City.Streets[x])) {
                        if (City.Streets[x].TotalUpgrades > TotalUpgrades) {
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

            for (int x = 0; x < City.Streets.Count; x++) {
                switch (x) {
                    case 0:
                    if (City.Streets[x].TotalUpgrades <= TotalUpgrades) {
                        first = true;
                    }
                    break;
                    case 1:
                    if (City.Streets[x].TotalUpgrades <= TotalUpgrades) {
                        second = true;
                    }
                    break;
                    case 2:
                    if (City.Streets[x].TotalUpgrades <= TotalUpgrades) {
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
            if (Owner == null) {
                propertyOwner = Properties.Language.propertyowner;
            } else {
                propertyOwner = Owner.Name;
                houses = string.Format(Properties.Language.propertyhouses, TotalUpgrades, Environment.NewLine);
            }
            return String.Format(Properties.Language.property, Description, propertyOwner, houses, Price, Rent[0], UpgradeCost, Rent[1], Rent[2], Rent[3], Rent[4], Rent[5], Mortage, Environment.NewLine);
        }
    }
}
