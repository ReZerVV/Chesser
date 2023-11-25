namespace Chesser.Core.Pieces;

public class Bishop : PieceBase
{
    public Bishop(Color color) : base(color)
    {
    }

    public override IEnumerable<Move> GetMoves(Board board, Coord position)
    {
        List<Move> moves = new List<Move>();
        for (int moveIndex = 1; moveIndex <= 7; moveIndex++)
        {
            Coord coord = new Coord(moveIndex, moveIndex) + position;
            if (!Board.IsValidCoord(coord))
            {
                continue;
            }
            if (board.Empty(coord))
            {
                moves.Add(new Move(position, coord));
            }
            else if (board[coord].Color != Color)
            {
                moves.Add(new Move(position, coord));
                break;
            }
            else
            {
                break;
            }
        }
        for (int moveIndex = -1; moveIndex >= -7; moveIndex--)
        {
            Coord coord = new Coord(moveIndex, moveIndex) + position;
            if (!Board.IsValidCoord(coord))
            {
                continue;
            }
            if (board.Empty(coord))
            {
                moves.Add(new Move(position, coord));
            }
            else if (board[coord].Color != Color)
            {
                moves.Add(new Move(position, coord));
                break;
            }
            else
            {
                break;
            }
        }
        for (int moveIndex = 1; moveIndex <= 7; moveIndex++)
        {
            Coord coord = new Coord(moveIndex, -moveIndex) + position;
            if (!Board.IsValidCoord(coord))
            {
                continue;
            }
            if (board.Empty(coord))
            {
                moves.Add(new Move(position, coord));
            }
            else if (board[coord].Color != Color)
            {
                moves.Add(new Move(position, coord));
                break;
            }
            else
            {
                break;
            }
        }
        for (int moveIndex = -1; moveIndex >= -7; moveIndex--)
        {
            Coord coord = new Coord(moveIndex, -moveIndex) + position;
            if (!Board.IsValidCoord(coord))
            {
                continue;
            }
            if (board.Empty(coord))
            {
                moves.Add(new Move(position, coord));
            }
            else if (board[coord].Color != Color)
            {
                moves.Add(new Move(position, coord));
                break;
            }
            else
            {
                break;
            }
        }
        return moves;
    }
}