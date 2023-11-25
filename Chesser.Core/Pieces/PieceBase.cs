namespace Chesser.Core.Pieces;

public abstract class PieceBase
{
    public Color Color { get; set; }
   
    protected PieceBase(Color color)
    {
        Color = color;
    }

    public abstract IEnumerable<Move> GetMoves(Board board, Coord position);
}