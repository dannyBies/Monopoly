namespace Monopoly.Model.Tiles {

	/// <summary>
	/// A tile that can be bought by a player
	/// </summary>
	public abstract class TileBuyable : Tile , IBuyable {

		public Player Owner { get; set; }
		public int[] Rent { get; set; }
		public int Mortage { get; set; }
		public int Price { get; set; }
		public int TotalUpgrades { get; set; }
		public bool OnMortage { get; set; }
		
		public int UpgradeCost { get; set; }
		


		protected TileBuyable(Game game , string description , int[] rent , int mortage , int price)
			: base(game , description) {
			Rent = rent;
			Mortage = mortage;
			Price = price;
			TotalUpgrades = 0;
			Owner = null;
			HasOwner = false;
			OnMortage = false;
		}

		/// <summary>
		/// upgrades an IBuyable object
		/// </summary>
		public void Upgrade() {
			if(TotalUpgrades == 0) {
				if(OnMortage) {
					if(Owner.Money > Mortage) {
						Owner.Money -= Mortage;
						OnMortage = false;
					}
				} else {
					if(Owner.Money > UpgradeCost) {
						TotalUpgrades++;
						Owner.Money -= UpgradeCost;
						HasBuildings = true;
					}
				}
			} else if(TotalUpgrades < 5) {
				if(Owner.Money > UpgradeCost) {
                    CurrentGame.GameInfo.Enqueue(Properties.Language.upgrade);
					TotalUpgrades++;
					Owner.Money -= UpgradeCost;
				}
			}
		}

		/// <summary>
		/// downgrades an IBuyable object
		/// </summary>
		public void Downgrade() {
			if(TotalUpgrades > 1) {
				CurrentGame.GameInfo.Enqueue(Properties.Language.downgrade);
				TotalUpgrades--;
				Owner.Money += UpgradeCost;
			} else if(TotalUpgrades == 1) {
				TotalUpgrades--;
				HasBuildings = false;
				Owner.Money += UpgradeCost;
			} else {
                CurrentGame.GameInfo.Enqueue(Properties.Language.mortaged);
				OnMortage = true;
				Owner.Money += Mortage;
			}
		}
	}
}
