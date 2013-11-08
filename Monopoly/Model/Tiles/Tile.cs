
namespace Monopoly.Model.Tiles {

	/// <summary>
	/// Base class to define tiles for a monopoly board.
	/// </summary>
	public abstract class Tile {

		public Game CurrentGame { get; set; }

		public Tile NextTile { get; set; }
		public Tile PreviousTile { get; set; }

		public string Description { get; set; }
		public bool HasOwner { get; set; }
		public bool HasBuildings { get; set; }


		protected Tile(Game game , string description) {
			CurrentGame = game;
			Description = description;
			HasOwner = true;
			HasBuildings = false;
		}

		/// <summary>
		/// Defines what will happen when a player gets on a tile.
		/// </summary>
		/// <param name="player"></param>
		public abstract void DoAction(Player player);

		public abstract string GetCardInformation();
	}
}
