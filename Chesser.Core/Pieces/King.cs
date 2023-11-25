namespace Chesser.Core.Pieces;

public class King : PieceBase
{
    public King(Color color)
        : base(color)
    {
    }
    
    public IEnumerable<Coord> GetOffsets()
    {
        List<Coord> coords = new List<Coord>();
        coords.Add(new Coord(0, 1));
        coords.Add(new Coord(0, -1));
        coords.Add(new Coord(1, 0));
        coords.Add(new Coord(-1, 0));
        coords.Add(new Coord(1, 1));
        coords.Add(new Coord(-1, 1));
        coords.Add(new Coord(1, -1));
        coords.Add(new Coord(-1, -1));
        return coords;
    }

    public override IEnumerable<Move> GetMoves(Board board, Coord position)
    {
        List<Move> moves = new List<Move>();
        foreach (Coord offset in GetOffsets())
        {
            Coord coord = offset + position;
            if (!Board.IsValidCoord(coord))
            {
                continue;
            }
            if (board.Empty(coord) || board[coord].Color != Color)
            {
                moves.Add(new Move(position, coord));
            }
        }
        return moves;
    }
}