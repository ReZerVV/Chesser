namespace Chesser.Core;

public class Move
{
    public readonly Coord Start;
    public readonly Coord Target;

    public Move(Coord start, Coord target)
    {
        Start = start;
        Target = target;
    }

    public override string ToString()
    {
        return $"[Move] {Start} -> {Target}";
    }

    public override bool Equals(object? obj)
    {
        Move move = (Move)obj;
        return (Start.Equals(move.Start))
            && (Target.Equals(move.Target));
    }
}