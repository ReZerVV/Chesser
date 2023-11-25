using Chesser.Core.Pieces;

namespace Chesser.Core;

public static class Evaluator
{
    private const int PointsForPawnPiece = 10;
    private const int PointsForKnightPiece = 30;
    private const int PointsForBishopPiece = 30;
    private const int PointsForRookPiece = 50;
    private const int PointsForQueenPiece = 90;
    private const int PointsForKingPiece = 900;
    public static int Evaluate(Board board)
    {
        int points = 0;
        foreach (PieceBase piece in board.GetPieces())
        {
            if (piece == null)
            {
                continue;
            }
            if (piece.Color == Color.White)
            {
                switch (piece)
                {
                    case Pawn: points += PointsForPawnPiece; break;
                    case Knight: points += PointsForKnightPiece; break;
                    case Bishop: points += PointsForBishopPiece; break;
                    case Rook: points += PointsForRookPiece; break;
                    case Queen: points += PointsForQueenPiece; break;
                    case King: points += PointsForKingPiece; break;
                    default: throw new NotSupportedException(piece.GetType().ToString());
                }
            } 
            else
            {
                switch (piece)
                {
                    case Pawn: points -= PointsForPawnPiece; break;
                    case Knight: points -= PointsForKnightPiece; break;
                    case Bishop: points -= PointsForBishopPiece; break;
                    case Rook: points -= PointsForRookPiece; break;
                    case Queen: points -= PointsForQueenPiece; break;
                    case King: points -= PointsForKingPiece; break;
                    default: throw new NotSupportedException(piece.GetType().ToString());
                }
            }
        }
        return points;
    }
}