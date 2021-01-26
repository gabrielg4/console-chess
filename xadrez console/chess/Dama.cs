using board;

namespace chess
{
    class Dama : Piece
    {
        public Dama(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "D";
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

            // Esquerda
            pos.setValues(position.line, position.column - 1);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if(chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line, pos.column - 1);
            }

            // direita
            pos.setValues(position.line, position.column + 1);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line, pos.column + 1);
            }

            // acima
            pos.setValues(position.line - 1, position.column);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line - 1, pos.column);
            }

            // abaixo
            pos.setValues(position.line + 1, position.column);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line + 1, pos.column);
            }

            // NO
            pos.setValues(position.line - 1, position.column);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line - 1, pos.column - 1);
            }

            // NE
            pos.setValues(position.line - 1, position.column + 1);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line - 1, pos.column + 1);
            }

            // SE
            pos.setValues(position.line + 1, position.column + 1);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line + 1, pos.column + 1);
            }

            // SO
            pos.setValues(position.line + 1, position.column - 1);
            while (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (chessBoard.piece(pos) != null && chessBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line + 1, pos.column - 1);
            }

            return mat;
        }
    }
}
