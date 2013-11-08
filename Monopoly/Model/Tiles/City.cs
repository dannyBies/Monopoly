using System.Collections.ObjectModel;
using System.Linq;

namespace Monopoly.Model.Tiles {
	/// <summary>
	/// A class that holds properties that belong to a city
	/// </summary>
	public class City {
		public ObservableCollection<TileProperty> Streets { get; set; }

		public City() {
			Streets = new ObservableCollection<TileProperty>();
		}

		/// <summary>
		/// Check if a player owns all properties belonging to this city
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		public bool OwnsAllProperties(Player player) {
			if(Streets.Count == 2) {
				if(Streets.ElementAt(0).HasOwner && Streets.ElementAt(1).HasOwner) {
					return Streets.ElementAt(0).Owner.Equals(player) && Streets.ElementAt(1).Owner.Equals(player);
				}
			} else if(Streets.Count == 3 && (Streets.ElementAt(0).HasOwner && Streets.ElementAt(1).HasOwner && Streets.ElementAt(2).HasOwner)) {
				return Streets.ElementAt(0).Owner.Equals(player) && Streets.ElementAt(1).Owner.Equals(player) &&
					   Streets.ElementAt(2).Owner.Equals(player);
			}
			return false;
		}
	}
}
