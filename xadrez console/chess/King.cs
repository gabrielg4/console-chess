using board;


namespace chess
{
    class King : Piece
    {

        private ChessDeparture match;
        public King(Board chessBoard, Color color, ChessDeparture match) : base(chessBoard, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool canMove(Position pos)
        {
            Piece p = chessBoard.piece(pos);
            return p == null || p.color != color;
        }

        private bool testTower(Position pos)
        {
            Piece p = chessBoard.piece(pos);
            return p != null && p is Tower && p.color == color && p.amountQuantify == 0;
        }

        public override bool[,] possibleMoviments()
        {
            bool[,] mat = new bool[chessBoard.lines, chessBoard.columns];

            Position pos = new Position(0, 0);

            // above
            pos.setValues(position.line - 1, position.column);
            if (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            // northeast
            pos.setValues(position.line - 1, position.column + 1);
            if (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            // right
            pos.setValues(position.line, position.column + 1);
            if (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            // southeast
            pos.setValues(position.line + 1, position.column + 1);
            if (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            // bellow
            pos.setValues(position.line + 1, position.column);
            if (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            // south-west
            pos.setValues(position.line + 1, position.column - 1);
            if (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            //left
            pos.setValues(position.line, position.column - 1);
            if (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            // Nourtwest
            pos.setValues(position.line - 1, position.column - 1);
            if (chessBoard.ValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }


            // # special castling

            if (amountQuantify==0 && !match.check) {
                // jogada especial Roque Pequeno
                Position posT1 = new Position(position.line, position.column + 3);
                if (testTower(posT1))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);
                    if (chessBoard.piece(p1) == null  && chessBoard.piece(p2)==null)
                    {
                        mat[position.line, position.column + 2] = true;
                    }
                }

                // jogada especial Roque Grande
                Position posT2 = new Position(position.line, position.column - 4);
                if (testTower(posT2))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);
                    if (chessBoard.piece(p1) == null && chessBoard.piece(p2) == null && chessBoard.piece(p3) == null)
                    {
                        mat[position.line, position.column - 2] = true;
                    }
                }
            }

            return mat;

        }

    }
}
