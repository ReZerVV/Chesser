namespace Chesser.Core;

public class Coord
{
    // Column index
    public int File { get; set; }
    // Rows index
    public int Rank { get; set; }

    public Coord()
    {
        File = 0;
        Rank = 0;
    }

    public Coord(
        int file,
        int rank)
    {
        File = file;
        Rank = rank;
    }

    public static Coord operator +(Coord firstCoord, Coord secondCoord)
        => new Coord(firstCoord.File + secondCoord.File, firstCoord.Rank + secondCoord.Rank);

    public static Coord operator -(Coord firstCoord, Coord secondCoord)
        => new Coord(firstCoord.File - secondCoord.File, firstCoord.Rank - secondCoord.Rank);

    public override bool Equals(object? obj)
    {
        Coord coord = (Coord)obj;
        return (File == coord.File)
            && (Rank == coord.Rank);
    }

    public override string ToString()
    {
        return $"{(char)('a' + File)}{1 + Rank}";
    }
}