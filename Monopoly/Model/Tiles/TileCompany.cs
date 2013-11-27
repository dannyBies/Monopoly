
using System;

namespace Monopoly.Model.Tiles {

    /// <summary>
    /// tile that a player can buy
    /// </summary>
    public class TileCompany : TileBuyable {

        public TileCompany(Game game, string description, int mortage, int price)
            : base(game, description, new[] { 0 }, mortage, price) {
        }

        public override void DoAction(Player player) {
            CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.moves, player.Name, Description));
            if (Owner != null && !Owner.Equals(player)) {
                int moneyToPay = (Owner.TotalCompanies >= 2) ? player.DiceEyes * 10 : player.DiceEyes * 4;
                player.PayMoneyTo(Owner, moneyToPay);
                CurrentGame.GameInfo.Enqueue(String.Format(Properties.Language.companypay, player.Name, moneyToPay, Owner.Name));
            }
        }

        public override string GetCardInformation() {
            string companyOwner = Owner == null ? Properties.Language.propertyowner : Owner.Name;
            return String.Format(Properties.Language.company, Description, Environment.NewLine, companyOwner, Price, Mortage);
        }
    }
}
