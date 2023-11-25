using Chesser.Core.Pieces;

namespace Chesser.Core;

public class MoveGenerator
{
    public static IEnumerable<Move> GetAllMoves(Board board, Color color)
    {
        List<Move> moves = new List<Move>();
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
                moves.AddRange(piece.GetMoves(board, coord));
            }
        }
        return moves;
    }

    public static IEnumerable<Move> GetMoves(Board board, Color color)
    {
        if (Checker.IsCheck(board, color))
        {
            Coord? kingPosition = Checker.GetKingPosition(board, color);
            if (kingPosition == null)
            {
                return new List<Move>();
            }
            return GetMoves(board, kingPosition);
        }
        return GetAllMoves(board, color);
    }

    public static IEnumerable<Move> GetMoves(Board board, Coord position)
    {
        if (board.Empty(position))
        {
            throw new IndexOutOfRangeException(nameof(position));
        }
        return board[position].GetMoves(board, position);
    }
}