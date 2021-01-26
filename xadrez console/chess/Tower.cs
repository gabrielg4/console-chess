using board;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Board chessBoard, Color color) : base(chessBoard, color)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        private bool canMove(Position pos)
        {
            Piece p = chessBoard.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoviments()
        {
            bool[,] mat = new bool[chessBoard.lines, chessBoard.columns];

            Position pos = new Position(0, 0);

            // above
            pos.setValues(position.line - 1, position.column);
            while (chessBoard.ValidPosition(pos) && canMove(pos) )
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.line = pos.line - 1;
            }

            // below
            pos.setValues(position.line + 1, position.column);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.line = pos.line + 1;
            }
          
            // right
            pos.setValues(position.line, position.column + 1);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.column = pos.column + 1;
            }

            //left
            pos.setValues(position.line, position.column - 1);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.column = pos.column - 1;
            }

            return mat;

        }
    }
}
