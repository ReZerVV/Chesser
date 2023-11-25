using Chesser.Core.Pieces;

namespace Chesser.Core;

public class Board
{
    private PieceBase[] pieces;

    public const int Width = 8;
    public const int Height = 8;

    public Board()
    {
        pieces = new PieceBase[Height * Width];
    }

    #region Statis functions
    private static int CoordToIndex(Coord coord)
    {
        return coord.Rank * Board.Height + coord.File;
    }
    private static Coord IndexToCoord(int index)
    {
        return new Coord((int)(index / Height), index % Height);
    }
    public static bool IsValidCoord(Coord coord)
    {
        return coord.File >= 0
            && coord.File < Width
            && coord.Rank >= 0
            && coord.Rank < Height;
    }
    public static Board CreateBoardFromFen(string fenNotation)
        => FenLoader.From(fenNotation);
    #endregion

    public void FromFen(string fenNotation)
        => FenLoader.From(fenNotation, this);

    public PieceBase? this[Coord coord]
    {
        get
        {
            if (!IsValidCoord(coord))
            {
                return null;
            }
            return pieces[CoordToIndex(coord)];
        }
        set
        {
            if (IsValidCoord(coord))
            {
                pieces[CoordToIndex(coord)] = value;
            }
        }
    }
    public PieceBase? this[int file, int rank]
    {
        get
        {
            return this[new Coord(file, rank)];
        }
        set
        {
            this[new Coord(file, rank)] = value;
        }
    }

    public bool Empty(Coord coord)
    {
        if (!IsValidCoord(coord))
        {
            throw new IndexOutOfRangeException(nameof(coord));
        }
        return this[coord] == null;
    }

    public IEnumerable<Move> GetMoves(Color color)
        => MoveGenerator.GetMoves(this, color);

    public IEnumerable<PieceBase> GetPieces()
        => pieces;

    public IEnumerable<PieceBase> GetPieces(Color color)
    {
        List<PieceBase> pieces = new List<PieceBase>();
        foreach(PieceBase piece in GetPieces())
        {
            if (piece == null)
            {
                continue;
            }
            if (piece.Color == color)
            {
                pieces.Add(piece);
            }
        }
        return pieces;
    }

    public void MakeMove(Move move)
    {
        if (Empty(move.Start))
        {
            throw new ArgumentNullException(nameof(move.Start));
        }
        lastTarget = this[move.Target];
        this[move.Target] = this[move.Start];
        lastStart = this[move.Start];
        this[move.Start] = null;
    }

    private PieceBase lastStart;
    private PieceBase lastTarget;
    public void UnmakeMove(Move move)
    {
        if (Empty(move.Target))
        {
            throw new ArgumentNullException(nameof(move.Target));
        }
        this[move.Start] = lastStart;
        this[move.Target] = lastTarget;
    }
}