using Chesser.Drawing;

namespace Chesser.App.UI.Components;

public class ListComponent
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Size { get; set; } = Vector2.Zero;
    public List<string> Items { get; set; } = new List<string>();

    public void Render(Canvas canvas)
    {
        int yStartPosition = Position.y + Size.y - 1;
        int xStartPosition = Position.x;
        for (int itemIndex = 0, itemPosition = yStartPosition; itemIndex < Items.Count; itemIndex++)
        {
            if (itemPosition < Position.y)
            {
                return;
            }

            string[] words = Items[itemIndex].Split(' ');
            for (int wordIndex = 0, wordPosition = xStartPosition; wordIndex < words.Length; wordPosition += words[wordIndex].Length + 1, wordIndex++)
            {
                if (wordPosition + words[wordIndex].Length >= Position.x + Size.x)
                {
                    itemPosition--;
                    wordPosition = xStartPosition;
                    if (itemPosition < Position.y)
                    {
                        return;
                    }
                }

                canvas.DrawText(
                    text: $"{words[wordIndex]} ",
                    x: wordPosition,
                    y: itemPosition,
                    foreground: AppState.Colors.ForegroundDark,
                    background: AppState.Colors.Background,
                    style: PixelStyle.StyleItalic
                );
            }
            
            itemPosition--;
        }
    }
}