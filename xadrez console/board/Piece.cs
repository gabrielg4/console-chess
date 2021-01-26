
namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int amountQuantify { get; protected set; }
        public Board chessBoard { get; protected set; }

        public Piece(Board ChessBoard, Color color)
        {
            this.position = null;
            this.color = color;
            chessBoard = ChessBoard;
            amountQuantify = 0;
        }

        public void increaseMovements()
        {
            amountQuantify++;
        }

        public void decrementMovements()
        {
            amountQuantify--;
        }

        public bool possibleMovements()
        {
            bool[,] mat = possibleMoviments();
            for (int i = 0; i<chessBoard.lines; i++)
            {
                for (int j = 0; j<chessBoard.columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canMovefor(Position pos)
        {
            return possibleMoviments()[pos.line, pos.column];
        }

        public abstract bool[,] possibleMoviments();
      

    }
}
