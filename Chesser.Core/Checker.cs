using Chesser.Core.Pieces;

namespace Chesser.Core;

public class Checker
{
    public static bool IsCheck(Board board, Color color)
    {
        Coord kingPosition = GetKingPosition(board, color);
        if (kingPosition == null)
        {
            return true;
        }
        foreach (Move move in MoveGenerator.GetAllMoves(board, color.Reverse()))
        {
            if (move.Target.Equals(kingPosition))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsCheckmate(Board board, Color color)
    {
        if (!IsCheck(board, color))
        {
            return false;
        }
        IEnumerable<Move> moves = MoveGenerator.GetMoves(board, color);
        bool checkFlag = true;
        foreach (Move move in moves)
        {
            board.MakeMove(move);
            if (!IsCheck(board, color))
            {
                checkFlag = false;
            }
            board.UnmakeMove(move);
        }
        return checkFlag;
    }

    public static Coord? GetKingPosition(Board board, Color color)
    {
        for (int rank = 0; rank < Board.Height; rank++)
        {
            for (int file = 0; file < Board.Width; file++)
            {
                Coord coord = new Coord(file, rank);
                PieceBase piece = board[coord];
                if (piece is null || piece.Color != color)
                {
                    continue;
                }
                if (piece.GetType() == typeof(King)) // May be bug
                {
                    return coord;
                }
            }
        }
        return null;
    }
}
