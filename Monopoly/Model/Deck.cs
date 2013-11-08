using Monopoly.Model.Cards;
using System;
using System.Collections.Generic;


namespace Monopoly.Model {
	/// <summary>
	/// A stack class that holds cards
	/// </summary>
	public class Deck {

		public Card[] Cards { get; set; }
		public Card[] NewCardArray { get; set; }
		public int Top { get; set; }


		public Deck(List<Card> cards) {
			Cards = cards.ToArray();
			NewCardArray = Cards;
			Shuffle();

			Top = cards.Count - 1;
		}

		public void Push(Card card) {
			if(!IsFull()) {
				Top++;
				Cards[Top] = card;
			}
		}

		public bool IsFull() {
			return (Top > Cards.Length);
		}

		public Card Peek() {
			return !IsEmpty() ? Cards[Top] : null;
		}

		public Card Pop() {
			if(!IsEmpty()) {
				Top--;
				return Cards[Top + 1];
			}
			return null;
		}

		public bool IsEmpty() {
			if(Top == -1) {
				Cards = NewCardArray;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Randomise the cards
		/// </summary>
		public void Shuffle() {
			Random rng = new Random();
			int n = Cards.Length;
			while(n > 1) {
				n--;
				int k = rng.Next(n + 1);
				Card value = Cards[k];
				Cards[k] = Cards[n];
				Cards[n] = value;
			}
		}
	}
}
