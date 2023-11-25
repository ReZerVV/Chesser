namespace Chesser.Core.Pieces;

public class Pawn : PieceBase
{
    public Pawn(Color color)
        : base(color)
    {
    }

    public IEnumerable<Coord> GetOffsetsToMove(Coord position)
    {
        List<Coord> coords = new List<Coord>();
        if (Color == Color.White)
        {
            coords.Add(new Coord(0, 1));
            if (position.Rank == 1)
            {
                coords.Add(new Coord(0, 2));
            }
        }
        else
        {
            coords.Add(new Coord(0, -1));
            if (position.Rank == 6)
            {
                coords.Add(new Coord(0, -2));
            }
        }
        return coords;
    }

    public IEnumerable<Coord> GetOffsetsToAttack()
    {
        List<Coord> coords = new List<Coord>();
        if (Color == Color.White)
        {
            coords.Add(new Coord(-1, 1));
            coords.Add(new Coord(1, 1));
        }
        else
        {
            coords.Add(new Coord(-1, -1));
            coords.Add(new Coord(1, -1));
        }
        return coords;
    }

    public override IEnumerable<Move> GetMoves(Board board, Coord position)
    {
        List<Move> moves = new List<Move>();
        foreach (Coord offset in GetOffsetsToMove(position))
        {
            Coord coord = offset + position;
            if (!Board.IsValidCoord(coord))
            {
                continue;
            }
            if (board.Empty(coord))
            {
                moves.Add(new Move(position, coord));
            }
        }
        foreach (Coord offset in GetOffsetsToAttack())
        {
            Coord coord = offset + position;
            if (!Board.IsValidCoord(coord))
            {
                continue;
            }
            if (!board.Empty(coord) && board[coord].Color != Color)
            {
                moves.Add(new Move(position, coord));
            }
        }
        return moves;
    }
}