namespace Chesser.Core;

public enum Color
{
    Black = 0,
    White = 1,
}

public static class ColorExtensions
{
    public static Color Reverse(this Color color)
    {
        return color == Color.Black ? Color.White : Color.Black;
    }
}