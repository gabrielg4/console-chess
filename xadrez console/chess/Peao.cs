using board;
namespace chess
{
    class Peao : Piece
    {

        private ChessDeparture match;
        public Peao(Board board, Color color, ChessDeparture match) : base(board, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool isEnemy(Position pos)
        {
            Piece p = chessBoard.piece(pos);
            return p != null && p.color != color;
        }

        private bool free(Position pos)
        {
            return chessBoard.piece(pos) == null;
        }

        public override bool[,] possibleMoviments()
        {
            bool[,] mat = new bool[chessBoard.lines, chessBoard.columns];

            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.setValues(position.line - 1, position.column);
                if (chessBoard.ValidPosition(pos) && free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line - 2, position.column);
                if (chessBoard.ValidPosition(pos) && amountQuantify == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line - 1, position.column - 1);
                if (chessBoard.ValidPosition(pos) && isEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line - 1, position.column + 1);
                if (chessBoard.ValidPosition(pos) && isEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }


                // # jogada especial en passant
                if (position.line == 3 )
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (chessBoard.ValidPosition(left) && isEnemy(left) && chessBoard.piece(left) == match.passant)
                    {
                        mat[left.line - 1, left.column] = true;
                    }

                    Position right = new Position(position.line, position.column + 1);
                    if (chessBoard.ValidPosition(right) && isEnemy(right) && chessBoard.piece(right) == match.passant)
                    {
                        mat[right.line - 1, right.column] = true;
                    }
                }
              
            }
            else
            {
                pos.setValues(position.line + 1, position.column);
                if (chessBoard.ValidPosition(pos) && free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line + 2, position.column);
                if (chessBoard.ValidPosition(pos) && free(pos) && amountQuantify == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line + 1, position.column - 1);
                if (chessBoard.ValidPosition(pos) && isEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line + 1, position.column + 1);
                if (chessBoard.ValidPosition(pos) && isEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // # jogada especial en passant
                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (chessBoard.ValidPosition(left) && isEnemy(left) && chessBoard.piece(left) == match.passant)
                    {
                        mat[left.line + 1, left.column] = true;
                    }

                    Position right = new Position(position.line, position.column + 1);
                    if (chessBoard.ValidPosition(right) && isEnemy(right) && chessBoard.piece(right) == match.passant)
                    {
                        mat[right.line + 1, right.column] = true;
                    }
                }
            }
            return mat;
        }
    }
}
