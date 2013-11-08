namespace Monopoly.Model {
	
    /// <summary>
    /// interface for defining what a tile that can be bought can do.
    /// </summary>
    public interface IBuyable {
		
		Player Owner { get; set; }   
		int[] Rent { get; set; }
		int Mortage { get; set; }
		int Price { get; set; }
		int TotalUpgrades { get; set; }
		bool OnMortage { get; set; }
		
		void Upgrade();
		void Downgrade();
	}
}
