using System;

namespace Monopoly.Model {
	/// <summary>
	/// class that throws two dices in the game
	/// </summary>
	public class Dice {
		public int FirstDice { get; set; }
		public int SecondDice { get; set; }
		public bool HasBeenThrown { get; set; }
		public Random Random { get; set; }


		public Dice() {
			HasBeenThrown = false;
			Random = new Random();
		}

		public int ThrowDice() {
			FirstDice = Random.Next(1 , 6);
			SecondDice = Random.Next(1 , 6);
			return (FirstDice + SecondDice);
		}

		public bool IsDouble() {
			return (FirstDice == SecondDice);
		}
	}
}
