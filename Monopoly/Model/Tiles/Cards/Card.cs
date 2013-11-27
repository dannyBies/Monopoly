

namespace Monopoly.Model.Cards {

    /// <summary>
    /// Base class for cards
    /// </summary>
	public abstract class Card {
		public string Description { get; set; }

		protected Card(string description) {
			Description = description;
		}

        /// <summary>
        /// Abstract method cards can use to define what it has to do
        /// </summary>
        /// <param name="player"></param>
		public abstract void Use(Player player);
	}
}
