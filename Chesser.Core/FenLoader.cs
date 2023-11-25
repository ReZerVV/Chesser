using Chesser.Core.Pieces;

namespace Chesser.Core;

public static class FenLoader
{
    public static Board From(string fenNotation)
    {
        Board board = new Board();
        int rank = Board.Height - 1;
        foreach (string fenLine in fenNotation.Split('/'))
        {
            int file = 0;
            foreach(char symbol in fenLine)
            {
                if (char.IsDigit(symbol))
                {
                    file += int.Parse(symbol.ToString());
                    continue;
                }
                board[file, rank] = PieceFromFen(symbol);
                file++;
            }
            rank--;
        }
        return board;
    }

    public static void From(string fenNotation, Board board)
    {
        int rank = Board.Height - 1;
        foreach (string fenLine in fenNotation.Split('/'))
        {
            int file = 0;
            foreach(char symbol in fenLine)
            {
                if (char.IsDigit(symbol))
                {
                    file += int.Parse(symbol.ToString());
                    continue;
                }
                board[file, rank] = PieceFromFen(symbol);
                file++;
            }
            rank--;
        }
    }

    public static string To(Board board)
    {
        throw new NotImplementedException();
    }

    private static PieceBase PieceFromFen(char symbol)
    {
        switch (symbol)
        {
            case 'p': return new Pawn(Color.Black);
            case 'n': return new Knight(Color.Black);
            case 'b': return new Bishop(Color.Black);
            case 'r': return new Rook(Color.Black);
            case 'q': return new Queen(Color.Black);
            case 'k': return new King(Color.Black);
            case 'P': return new Pawn(Color.White);
            case 'N': return new Knight(Color.White);
            case 'B': return new Bishop(Color.White);
            case 'R': return new Rook(Color.White);
            case 'Q': return new Queen(Color.White);
            case 'K': return new King(Color.White);
            default: throw new NotSupportedException(nameof(symbol));
        }
    }

    private static char FenFromPiece(PieceBase piece)
    {
        throw new NotImplementedException();
    }
}