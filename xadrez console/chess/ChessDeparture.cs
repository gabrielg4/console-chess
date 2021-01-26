using System.Collections.Generic;
using System;
using board;


namespace chess
{
    class ChessDeparture
    {
        public Board board { get; private set; }
        public int shift { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }
        public Piece passant { get; private set; }



        public ChessDeparture()
        {
            board = new Board(8, 8);
            shift = 1;
            CurrentPlayer = Color.White;
            finished = false;
            check = false;
            passant = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            putPieces();
        }

        public Piece PerformMovement(Position source, Position destiny)
        {
            Piece p = board.removePiece(source);
            p.increaseMovements();
            Piece CapturedPiece = board.removePiece(destiny);
            board.putPiece(p, destiny);
            if (CapturedPiece != null)
            {
                captured.Add(CapturedPiece);
            }

            // # jogada especial roque pequeno
            if(p is King && destiny.column == source.column + 2)
            {
                Position sourceT = new Position(source.line, source.column + 3);
                Position DestinyT = new Position(source.line, source.column + 1);
                Piece T = board.removePiece(sourceT);
                T.increaseMovements();
                board.putPiece(T, DestinyT);

            }

            // # jogada especial roque grande
            if (p is King && destiny.column == source.column - 2)
            {
                Position sourceT = new Position(source.line, source.column - 4);
                Position DestinyT = new Position(source.line, source.column - 1);
                Piece T = board.removePiece(sourceT);
                T.increaseMovements();
                board.putPiece(T, DestinyT);

            }

            // #jogada especial passant
            if (p is Peao)
            {
                if (source.column != destiny.column && CapturedPiece == null)
                {
                    Position PosP;
                    if (p.color == Color.White)
                    {
                        PosP = new Position(destiny.line + 1, destiny.column);

                    }
                    else
                    {
                        PosP = new Position(destiny.line - 1, destiny.column);
                    }
                    CapturedPiece = board.removePiece(PosP);
                    captured.Add(CapturedPiece);
                }
            }


            return CapturedPiece;
        }

        public void UndoMovement(Position source, Position destiny, Piece capturedPiece)
        {
            Piece p = board.removePiece(destiny);
            p.decrementMovements();
            if (capturedPiece != null)
            {
                board.putPiece(capturedPiece, destiny);
                captured.Remove(capturedPiece);
            }
            board.putPiece(p, source);

            // roque pequeno
            if (p is King && destiny.column == source.column + 2)
            {
                Position sourceT = new Position(source.line, source.column + 3);
                Position DestinyT = new Position(source.line, source.column + 1);
                Piece T = board.removePiece(DestinyT);
                T.decrementMovements();
                board.putPiece(T, sourceT);

            }

            // roque grande
            if (p is King && destiny.column == source.column - 2)
            {
                Position sourceT = new Position(source.line, source.column - 4);
                Position DestinyT = new Position(source.line, source.column - 1);
                Piece T = board.removePiece(DestinyT);
                T.decrementMovements();
                board.putPiece(T, sourceT);

            }

            // jogada especial passant
            if (p is Peao)
            {
                if (source.column != destiny.column && capturedPiece == passant)
                {
                    Piece peao = board.removePiece(destiny);
                    Position PosP;
                    if (p.color == Color.White)
                    {
                        PosP = new Position(3, destiny.column);
                    }
                    else
                    {
                        PosP = new Position(4, destiny.column);
                    }
                   board.putPiece(peao, PosP);
                }
                 
            }

        }

        public void perfomeMove(Position source, Position destiny)
        {
            Piece capturedPiece =  PerformMovement(source, destiny);

            if (inCheck(CurrentPlayer))
            {
                UndoMovement(source, destiny, capturedPiece);
                throw new BoardExpection("You cannot put yourself in check");
            }

            Piece p = board.piece(destiny);

            // #jogadaespecial promocao

            if (p is Peao)
            {
                if (p.color == Color.White && destiny.line == 0 || (p.color == Color.Black && destiny.line == 7))
                {
                    p = board.removePiece(destiny);
                    pieces.Remove(p);
                    Piece dama = new Dama(board, p.color);
                    board.putPiece(dama, destiny);
                    pieces.Add(dama);
                }
            }

            if (inCheck(Opponent(CurrentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (checkMateTest(Opponent(CurrentPlayer)))
            {
                finished = true;
            }
            else
            {
                shift++;
                changePlayer();
            }

            

            // # jogada especial en Passant
            if(p is Peao && (destiny.line == source.line - 2 || destiny.line == source.line + 2))
            {
                passant = p;
            }
            else
            {
                passant = null;
            }
           
        }



        public void validPositionSource(Position pos)
        {
            if (board.piece(pos) == null)
            {
                throw new BoardExpection("There is no part in the original position");
            }
            if (CurrentPlayer != board.piece(pos).color)
            {
                throw new BoardExpection("The original piece chosen is not yours");
            }
            if (!board.piece(pos).possibleMovements())
            {
                throw new BoardExpection("There are no possible movements for the closen piece of origin");
            }
        }

        public void validPositionDestiny(Position source, Position destiny)
        {
            if (!board.piece(source).canMovefor(destiny))
            {
                throw new BoardExpection("invalid target position!");
            }
        }

        private void changePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PieceInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;   
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in PieceInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool inCheck(Color color)
        {
            Piece R = king(color);
            if (R == null)
            {
                throw new BoardExpection("There is no king of color " + color + "on board!");
            }
;            foreach (Piece x in PieceInGame(Opponent(color)))
            {
                bool[,] mat = x.possibleMoviments();
                if (mat[R.position.line, R.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool checkMateTest(Color color)
        {
            if (!inCheck(color))
            {
                return false;
            }
            foreach (Piece x in PieceInGame(color))
            {
                bool[,] mat = x.possibleMoviments();
                for (int i = 0; i<board.lines; i++)
                {
                    for (int j = 0; j<board.columns; j++)
                    {
                        if (mat[i,j])
                        {
                            Position source = x.position;
                            Position destiny = new Position(i, j);
                            Piece CapturedPiece = PerformMovement(source, destiny);
                            bool checktest = inCheck(color);
                            UndoMovement(source, destiny, CapturedPiece);
                            if (!checktest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void putOnNew(char column, int line, Piece piece)
        {
            board.putPiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }


        private void putPieces()
        {
            putOnNew('a', 1, new Tower(board, Color.White));
            putOnNew('b', 1, new Horse(board, Color.White));
            putOnNew('c', 1, new Bispo(board, Color.White));
            putOnNew('d', 1, new Dama(board, Color.White));
            putOnNew('e', 1, new King(board, Color.White, this));
            putOnNew('f', 1, new Bispo(board, Color.White));
            putOnNew('g', 1, new Horse(board, Color.White));
            putOnNew('h', 1, new Tower(board, Color.White));
            putOnNew('a', 2, new Peao(board, Color.White, this));
            putOnNew('b', 2, new Peao(board, Color.White, this));
            putOnNew('c', 2, new Peao(board, Color.White, this));
            putOnNew('d', 2, new Peao(board, Color.White, this));
            putOnNew('e', 2, new Peao(board, Color.White, this));
            putOnNew('f', 2, new Peao(board, Color.White, this));
            putOnNew('g', 2, new Peao(board, Color.White, this));
            putOnNew('h', 2, new Peao(board, Color.White, this));


            putOnNew('a', 8, new Tower(board, Color.Black));
            putOnNew('b', 8, new Horse(board, Color.Black));
            putOnNew('c', 8, new Bispo(board, Color.Black));
            putOnNew('d', 8, new Dama(board, Color.Black));
            putOnNew('e', 8, new King(board, Color.Black, this));
            putOnNew('f', 8, new Bispo(board, Color.Black));
            putOnNew('g', 8, new Horse(board, Color.Black));
            putOnNew('h', 8, new Tower(board, Color.Black));
            putOnNew('a', 7, new Peao(board, Color.Black, this));
            putOnNew('b', 7, new Peao(board, Color.Black, this));
            putOnNew('c', 7, new Peao(board, Color.Black, this));
            putOnNew('d', 7, new Peao(board, Color.Black, this));
            putOnNew('e', 7, new Peao(board, Color.Black, this));
            putOnNew('f', 7, new Peao(board, Color.Black, this));
            putOnNew('g', 7, new Peao(board, Color.Black, this));
            putOnNew('h', 7, new Peao(board, Color.Black, this));




        }
    }
}
