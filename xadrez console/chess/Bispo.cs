using board;

namespace chess
{
    class Bispo : Piece
    {
        public Bispo(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "B";
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

            // NO
            pos.setValues(position.line - 1, position.column - 1);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                {
                    mat[pos.line, pos.column] = true;
                    if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                    {
                        break;
                    }
                    pos.setValues(pos.line - 1, pos.column - 1);
                }
            }

            // NE
            pos.setValues(position.line - 1, position.column + 1);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                {
                    mat[pos.line, pos.column] = true;
                    if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                    {
                        break;
                    }
                    pos.setValues(pos.line - 1, pos.column + 1);
                }

                // SO
                pos.setValues(position.line + 1, position.column - 1);
                while (chessBoard.ValidPosition(pos) && canMove(pos))
                {
                    {
                        mat[pos.line, pos.column] = true;
                        if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                        {
                            break;
                        }
                        pos.setValues(pos.line + 1, pos.column + 1);
                    }
                }
                
            }
            return mat;
        }
    }
}
