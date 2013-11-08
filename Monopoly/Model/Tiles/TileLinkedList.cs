namespace Monopoly.Model.Tiles {

    /// <summary>
    /// A linked list that holds all tiles
    /// </summary>
    public class TileLinkedList {
        public Tile Head { get; set; }
        public int Size { get; set; }

        public TileLinkedList() {
            Size = 0;
        }

        public void Add(Tile data) {
            Size++;
            if (Head == null) {
                Head = data;
            } else {
                Tile temp = Head;
                Head = data;
                if (Head.NextTile == null) {
                    Head.NextTile = temp;
                }
                if (temp.PreviousTile == null) {
                    temp.PreviousTile = Head;
                }
            }
        }

        public Tile GetAt(int x) {
            Tile current = Head;
            for(int i = 0; i < x; i++) {
                current = current.NextTile;
            }
            return current;
        }
    }
}
