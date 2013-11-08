using Monopoly.Model.Tiles;
using System;
namespace Monopoly.Model.Cards {
	public class CardMove : Card {

		public Tile MovePlace { get; set; }
		public int MovePlaces { get; set; }

		public CardMove(String description , int movePlaces)
			: base(description) {
			MovePlace = null;
			MovePlaces = movePlaces;
		}

		public CardMove(String description , Tile moveTo)
			: base(description) {
			MovePlaces = -1;
			MovePlace = moveTo;
		}

		public override void Use(Player player) {
			if(MovePlace != null) {
				//move directly to the specified tile
				player.MoveTo(MovePlace);

			} else if(MovePlaces != -1) {
				//move spaces forward or backward on the board
				player.MoveTo(MovePlaces);

			} else {
				//can't move because of invalid information given in the constructor
				throw new InvalidOperationException();
			}
		}
	}
}