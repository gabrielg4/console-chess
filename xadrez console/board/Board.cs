namespace board
{
    class Board
    {
        public int lines { get; set; }
        public int columns { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int columns)
        {
            this.lines = lines;
            this.columns = columns;
            pieces = new Piece[lines, columns];
        }

        public Piece piece(int lines, int columns)
        {
            return pieces[lines, columns];
        }

        public Piece piece(Position pos)
        {
            return pieces[pos.line, pos.column];
        }

        public bool ExistingPart(Position pos)
        {
            ValidatePosition(pos);
            return piece(pos) != null;
        }

        public void putPiece(Piece p, Position pos)
        {
            if (ExistingPart(pos))
            {
                throw new BoardExpection("There is already a piece in that position!");
            }
            pieces[pos.line, pos.column] = p;
            p.position = pos; 
        }

        public Piece removePiece(Position pos)
        {
            if (piece(pos) == null)
            {
                return null;
            }
            Piece aux = piece(pos);
            aux.position = null;
            pieces[pos.line, pos.column] = null;
            return aux;
        }

        public bool ValidPosition(Position pos)
        {
            if (pos.line < 0 || pos.line >= lines || pos.column<0 || pos.column >= columns )
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardExpection("Invalid position!");
            }
        }
    }
}
