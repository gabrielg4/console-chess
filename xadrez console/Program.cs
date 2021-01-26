using System;
using board;
using chess;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessDeparture match = new ChessDeparture();

                while (!match.finished)
                {

                    try
                    {
                        Console.Clear();
                        Screen.printParty(match);

                        Console.WriteLine();
                        Console.Write("Enter the origin position: ");
                        Position source = Screen.ReadChessPosition().toPosition();
                        match.validPositionSource(source);


                        bool[,] PossiblePossibilities = match.board.piece(source).possibleMoviments();

                        Console.Clear();
                        Screen.printBoard(match.board, PossiblePossibilities);

                        Console.WriteLine();
                        Console.Write("Enter the destiny position: ");
                        Position destiny = Screen.ReadChessPosition().toPosition();
                        match.validPositionDestiny(source, destiny);

                        match.perfomeMove(source, destiny);

                    }
                    catch (BoardExpection e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.printParty(match);
            }
            catch (BoardExpection e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
